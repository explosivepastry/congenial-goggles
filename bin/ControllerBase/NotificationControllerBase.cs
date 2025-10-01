// Decompiled with JetBrains decompiler
// Type: iMonnit.ControllerBase.NotificationControllerBase
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.ControllerBase;

public class NotificationControllerBase : ThemeController
{
  public List<Notification> GetNotificationList(out int totalNotifications)
  {
    int Class = MonnitSession.NotificationListFilters.Class;
    int Status = MonnitSession.NotificationListFilters.Status;
    long AppID = MonnitSession.NotificationListFilters.ApplicationID;
    string Name = MonnitSession.NotificationListFilters.Name;
    List<Notification> source1 = Notification.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
    totalNotifications = source1.Count<Notification>();
    IEnumerable<Notification> source2 = source1.Where<Notification>((Func<Notification, bool>) (n =>
    {
      if (n.NotificationClass.ToInt() != Class && Class != int.MinValue || n.IsActive.ToInt() != Status && Status != int.MinValue || n.ApplicationID != AppID && AppID != long.MinValue)
        return false;
      return Name == "" || n.Name.ToLower().Contains(Name.ToLower());
    }));
    IEnumerable<Notification> source3;
    switch (MonnitSession.NotificationListSort.Column)
    {
      case "Type":
        source3 = !(MonnitSession.NotificationListSort.Direction == "Desc") ? (IEnumerable<Notification>) source2.OrderBy<Notification, string>((Func<Notification, string>) (n => n.NotificationClass.ToString())) : (IEnumerable<Notification>) source2.OrderByDescending<Notification, string>((Func<Notification, string>) (n => n.NotificationClass.ToString()));
        break;
      case "Last Sent":
        source3 = !(MonnitSession.NotificationListSort.Direction == "Desc") ? (IEnumerable<Notification>) source2.OrderBy<Notification, DateTime>((Func<Notification, DateTime>) (n => n.LastSent)) : (IEnumerable<Notification>) source2.OrderByDescending<Notification, DateTime>((Func<Notification, DateTime>) (n => n.LastSent));
        break;
      default:
        source3 = !(MonnitSession.NotificationListSort.Direction == "Desc") ? (IEnumerable<Notification>) source2.OrderBy<Notification, string>((Func<Notification, string>) (n => n.Name)) : (IEnumerable<Notification>) source2.OrderByDescending<Notification, string>((Func<Notification, string>) (n => n.Name));
        break;
    }
    return source3.ToList<Notification>();
  }

  public ActionResult FilterStatus(string status)
  {
    try
    {
      MonnitSession.NotificationListFilters.Status = string.IsNullOrEmpty(status) ? int.MinValue : status.ToInt();
    }
    catch
    {
      MonnitSession.NotificationListFilters.Status = int.MinValue;
    }
    return (ActionResult) this.Content("Success");
  }

  public ActionResult FilterClass(string notificationClass)
  {
    try
    {
      MonnitSession.NotificationListFilters.Class = string.IsNullOrEmpty(notificationClass) ? int.MinValue : notificationClass.ToInt();
    }
    catch
    {
      MonnitSession.NotificationListFilters.Class = int.MinValue;
    }
    return (ActionResult) this.Content("Success");
  }

  public ActionResult FilterName(string name)
  {
    try
    {
      MonnitSession.NotificationListFilters.Name = name;
    }
    catch
    {
      MonnitSession.NotificationListFilters.Name = "";
    }
    return (ActionResult) this.Content("Success");
  }

  public ActionResult SortBy(string column, string direction)
  {
    MonnitSession.NotificationListSort.Column = column.ToStringSafe();
    MonnitSession.NotificationListSort.Direction = direction.ToStringSafe();
    return (ActionResult) this.Content("Success");
  }

  public ActionResult SetActive(long id, bool active)
  {
    Notification DBObject = Notification.Load(id);
    if (!this.HttpContext.User.Identity.IsAuthenticated || !MonnitSession.IsAuthorizedForAccount(DBObject.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.Content("Unauthorized");
    string str = " ";
    if (!active)
      str = " not ";
    Account account = Account.Load(DBObject.AccountID);
    if (account != null)
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, $"Notification set to{str}active");
    DBObject.IsActive = active;
    DBObject.Save();
    return (ActionResult) this.Content("Success");
  }

  public bool RecordTestNotification(Notification notification, PacketCache cache)
  {
    if (notification.SensorsThatNotify.Count > 0)
      notification.RecordNotification(cache, true, "Notification Test", DateTime.UtcNow, notification.SensorsThatNotify[0].sens, (Gateway) null, notification.SensorsThatNotify[0].SensorNotificationID, long.MinValue, (DataMessage) null);
    else if (notification.GatewaysThatNotify.Count > 0)
    {
      GatewayNotification gatewayNotification = GatewayNotification.LoadByGatewayIDAndNotificationID(notification.GatewaysThatNotify[0].GatewayID, notification.NotificationID);
      if (gatewayNotification != null)
        notification.RecordNotification(cache, true, "Notification Test", DateTime.UtcNow, (Sensor) null, notification.GatewaysThatNotify[0], long.MinValue, gatewayNotification.GatewayNotificationID, (DataMessage) null);
    }
    else
    {
      if (!this.isAutomatedTest(notification))
        return false;
      notification.RecordNotification(cache, true, "Automated Notification Test", DateTime.UtcNow, (Sensor) null, (Gateway) null, long.MinValue, long.MinValue, (DataMessage) null);
    }
    return true;
  }

  public bool isAutomatedTest(Notification noti)
  {
    return AutomatedNotification.LoadByExternalIDAndNotificationID(noti.AdvancedNotificationID, noti.NotificationID) != null;
  }

  public void SendTestNotification(PacketCache cache)
  {
    foreach (NotificationRecorded recordedNotification in cache.RecordedNotifications)
    {
      try
      {
        Notification.SendNotification(recordedNotification, cache);
      }
      catch (Exception ex)
      {
        ex.Log("NotifyOfNonActivity.Run() SendSMTPNotification.NotificationRecordedID:" + recordedNotification.NotificationRecordedID.ToString());
      }
    }
  }

  public static NotificationRecipient FindNotificationRecipient(
    long deviceID,
    string deviceType,
    Notification notification)
  {
    NotificationRecipient notificationRecipient1 = (NotificationRecipient) null;
    foreach (NotificationRecipient notificationRecipient2 in notification.NotificationRecipients)
    {
      if (notificationRecipient2.DeviceToNotifyID == deviceID)
      {
        if (notificationRecipient2.NotificationType == eNotificationType.Local_Notifier)
        {
          notificationRecipient1 = notificationRecipient2;
          break;
        }
        if (notificationRecipient2.NotificationType == eNotificationType.Thermostat)
        {
          notificationRecipient1 = notificationRecipient2;
          break;
        }
        if (notificationRecipient2.NotificationType == eNotificationType.Control)
        {
          int state1 = 0;
          int state2 = 0;
          ushort time1 = 0;
          ushort time2 = 0;
          if (deviceType == "BasicControl")
          {
            BasicControl.ParseSerializedRecipientProperties(notificationRecipient2.SerializedRecipientProperties, out state1, out time1);
            notificationRecipient1 = notificationRecipient2;
            break;
          }
          Control_1.ParseSerializedRecipientProperties(notificationRecipient2.SerializedRecipientProperties, out state1, out state2, out time1, out time2);
          if (deviceType == "Control1" && state1 > 0)
          {
            notificationRecipient1 = notificationRecipient2;
            break;
          }
          if (deviceType == "Control2" && state2 > 0)
          {
            notificationRecipient1 = notificationRecipient2;
            break;
          }
        }
        if (notificationRecipient2.NotificationType == eNotificationType.ResetAccumulator)
        {
          int state1 = 0;
          int state2 = 0;
          int acc;
          switch (deviceType)
          {
            case "CurrentZeroTo500Amp":
              CurrentZeroTo500Amp.ParseSerializedRecipientProperties(notificationRecipient2.SerializedRecipientProperties, out acc);
              notificationRecipient1 = notificationRecipient2;
              break;
            case "CurrentZeroToOneFiftyAmp":
              CurrentZeroToOneFiftyAmp.ParseSerializedRecipientProperties(notificationRecipient2.SerializedRecipientProperties, out acc);
              notificationRecipient1 = notificationRecipient2;
              break;
            case "CurrentZeroToTwentyAmp":
              CurrentZeroToTwentyAmp.ParseSerializedRecipientProperties(notificationRecipient2.SerializedRecipientProperties, out acc);
              notificationRecipient1 = notificationRecipient2;
              break;
            case "FilteredPulse":
              FilteredPulseCounter.ParseSerializedRecipientProperties(notificationRecipient2.SerializedRecipientProperties, out acc);
              notificationRecipient1 = notificationRecipient2;
              break;
            case "FilteredPulse64":
              FilteredPulseCounter64.ParseSerializedRecipientProperties(notificationRecipient2.SerializedRecipientProperties, out acc);
              notificationRecipient1 = notificationRecipient2;
              break;
            case "TwoInputPulseRelay1":
              TwoInputPulseCounter.ParseSerializedRecipientProperties(notificationRecipient2.SerializedRecipientProperties, out state1, out state2, out acc);
              notificationRecipient1 = notificationRecipient2;
              break;
            case "TwoInputPulseRelay2":
              TwoInputPulseCounter.ParseSerializedRecipientProperties(notificationRecipient2.SerializedRecipientProperties, out state1, out state2, out acc);
              notificationRecipient1 = notificationRecipient2;
              break;
          }
        }
      }
    }
    return notificationRecipient1;
  }
}
