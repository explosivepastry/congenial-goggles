// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.NotificationController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.ControllerBase;
using iMonnit.Models;
using Microsoft.CSharp.RuntimeBinder;
using Monnit;
using Newtonsoft.Json;
using RedefineImpossible;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

[NoCache]
public class NotificationController : NotificationControllerBase
{
  [AuthorizeDefault]
  public ActionResult Details(long id)
  {
    Notification model = Notification.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.PartialView("Unauthorized");
    this.ViewData["Notifications"] = (object) NotificationRecorded.LoadRecentByNotificationID(id, 5);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult Test(long id)
  {
    Notification notification = Notification.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(notification.AccountID))
      return (ActionResult) this.PartialView("Unauthorized");
    string empty = string.Empty;
    PacketCache cache = new PacketCache();
    if (!this.RecordTestNotification(notification, cache))
      return (ActionResult) this.Content("Please assign a device to this notification first");
    this.SendTestNotification(cache);
    return (ActionResult) this.Content($"{cache.RecordedNotifications.Count} Notifications sent");
  }

  [AuthorizeDefault]
  public ActionResult Delete(long id)
  {
    Notification DBObject = Notification.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(DBObject.AccountID))
      return (ActionResult) this.PartialView("Unauthorized");
    try
    {
      Account account = Account.Load(DBObject.AccountID);
      if (account != null)
        DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Deleted a notification");
      DBObject.Delete();
    }
    catch (Exception ex)
    {
      ex.Log($"NotificationController.Delete ID: {id.ToString()} unable to delete notification  ");
    }
    int totalNotifications;
    System.Collections.Generic.List<Notification> notificationList = this.GetNotificationList(out totalNotifications);
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalNotifications", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = NotificationController.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) NotificationController.\u003C\u003Eo__2.\u003C\u003Ep__0, this.ViewBag, totalNotifications);
    return (ActionResult) this.View("MainList", (object) notificationList);
  }

  [AuthorizeDefault]
  public ActionResult GetDatumsForApplication(long id)
  {
    System.Collections.Generic.List<eDatumStruct> eDatumStructList = new System.Collections.Generic.List<eDatumStruct>();
    int di = 0;
    foreach (AppDatum appDatum in MonnitApplicationBase.GetAppDatums(id))
    {
      AppDatum adat = appDatum;
      if (eDatumStructList.Where<eDatumStruct>((Func<eDatumStruct, bool>) (s => s.val == ((int) adat.etype).ToString() && s.customname.ToLower() == adat.type.ToLower())).Count<eDatumStruct>() == 0)
        eDatumStructList.Add(new eDatumStruct(adat.type, adat.type, di, ((int) adat.etype).ToString()));
      ++di;
    }
    return (ActionResult) this.Json((object) eDatumStructList, JsonRequestBehavior.AllowGet);
  }

  [AuthorizeDefault]
  public ActionResult CreateApplicationByDatumID(long id, long? sensorID, int datumindex = 0)
  {
    Notification notification = new Notification();
    notification.HasUserNotificationAction = true;
    Sensor sensor = Sensor.Load(sensorID ?? long.MinValue);
    if (sensor != null && sensor.SensorID != long.MinValue)
    {
      notification.SensorID = sensor.SensorID;
      // ISSUE: reference to a compiler-generated field
      if (NotificationController.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NotificationController.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "DatumIndex", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = NotificationController.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) NotificationController.\u003C\u003Eo__4.\u003C\u003Ep__0, this.ViewBag, datumindex);
    }
    notification.eDatumType = (eDatumType) id;
    notification.NotificationClass = eNotificationClass.Application;
    notification.DatumIndex = datumindex;
    notification.AccountID = MonnitSession.CurrentCustomer.AccountID;
    NotificationController.AddTimeDropDowns(notification, this.ViewData);
    Account account = Account.Load(notification.AccountID);
    if (account != null)
      notification.LogAuditData(eAuditAction.Create, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Created application notification");
    return (ActionResult) this.View("Edit", (object) notification);
  }

  [AuthorizeDefault]
  public ActionResult CreateBattery()
  {
    Notification notification = new Notification();
    notification.HasUserNotificationAction = true;
    notification.AccountID = MonnitSession.CurrentCustomer.AccountID;
    notification.NotificationClass = eNotificationClass.Low_Battery;
    notification.CompareValue = "15";
    notification.CompareType = eCompareType.Less_Than;
    NotificationController.AddTimeDropDowns(notification, this.ViewData);
    Account account = Account.Load(notification.AccountID);
    if (account != null)
      notification.LogAuditData(eAuditAction.Create, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Created battery notification");
    return (ActionResult) this.View("Edit", (object) notification);
  }

  [AuthorizeDefault]
  public ActionResult CreateInactivity()
  {
    Notification notification = new Notification();
    notification.HasUserNotificationAction = true;
    notification.AccountID = MonnitSession.CurrentCustomer.AccountID;
    notification.NotificationClass = eNotificationClass.Inactivity;
    notification.CompareValue = "245";
    notification.CompareType = eCompareType.Greater_Than;
    NotificationController.AddTimeDropDowns(notification, this.ViewData);
    Account account = Account.Load(notification.AccountID);
    if (account != null)
      notification.LogAuditData(eAuditAction.Create, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Created inactivity notification");
    return (ActionResult) this.View("Edit", (object) notification);
  }

  [AuthorizeDefault]
  public ActionResult CreateTimed()
  {
    Notification notification = new Notification();
    notification.HasUserNotificationAction = true;
    notification.AccountID = MonnitSession.CurrentCustomer.AccountID;
    notification.NotificationClass = eNotificationClass.Timed;
    notification.CompareValue = "0:00";
    notification.CompareType = eCompareType.Equal;
    NotificationController.AddTimeDropDowns(notification, this.ViewData);
    Account account = Account.Load(notification.AccountID);
    if (account != null)
      notification.LogAuditData(eAuditAction.Create, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Created timed notification");
    return (ActionResult) this.View("Edit", (object) notification);
  }

  [AuthorizeDefault]
  public ActionResult CreateAdvanced(long id)
  {
    Notification notification = new Notification();
    notification.HasUserNotificationAction = true;
    notification.AccountID = MonnitSession.CurrentCustomer.AccountID;
    notification.NotificationClass = eNotificationClass.Advanced;
    notification.AdvancedNotificationID = id;
    notification.CompareValue = "0";
    notification.CompareType = eCompareType.Equal;
    NotificationController.AddTimeDropDowns(notification, this.ViewData);
    Account account = Account.Load(notification.AccountID);
    if (account != null)
      notification.LogAuditData(eAuditAction.Create, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Created advanced notification");
    this.ViewData["AdvancedNotification"] = (object) AdvancedNotification.Load(notification.AdvancedNotificationID);
    this.ViewData["Params"] = (object) AdvancedNotificationParameter.LoadByAdvancedNotificationID(notification.AdvancedNotificationID);
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    System.Collections.Generic.List<AdvancedNotificationParameterValue> notificationParameterValueList = AdvancedNotificationParameterValue.LoadByNotificationID(notification.NotificationID);
    foreach (AdvancedNotificationParameter notificationParameter in AdvancedNotificationParameter.LoadByAdvancedNotificationID(notification.AdvancedNotificationID))
    {
      AdvancedNotificationParameter p = notificationParameter;
      if (!(p.ParameterName.ToLower() == "sensorid"))
      {
        try
        {
          string key = p.AdvancedNotificationParameterID.ToString();
          dictionary.Add(key, notificationParameterValueList.Find((Predicate<AdvancedNotificationParameterValue>) (v => v.AdvancedNotificationParameterID == p.AdvancedNotificationParameterID)).ParameterValue);
        }
        catch
        {
        }
      }
    }
    this.Session["AdvancedNotificationParams"] = (object) dictionary;
    return (ActionResult) this.View("Edit", (object) notification);
  }

  [AuthorizeDefault]
  public ActionResult ExistingNotification(long? sensorID, long? gatewayID)
  {
    Sensor sensor = (Sensor) null;
    Gateway gateway = (Gateway) null;
    long ID = long.MinValue;
    long? nullable1 = sensorID;
    long num1 = 0;
    if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
      sensor = Sensor.Load(sensorID ?? long.MinValue);
    long? nullable2 = gatewayID;
    long num2 = 0;
    if (nullable2.GetValueOrDefault() > num2 & nullable2.HasValue)
      gateway = Gateway.Load(gatewayID ?? long.MinValue);
    if (sensor != null)
      ID = sensor.CSNetID;
    if (gateway != null)
      ID = gateway.CSNetID;
    if (ID <= 0L)
      return (ActionResult) this.PartialView("Unauthorized");
    if (!MonnitSession.IsAuthorizedForAccount(CSNet.Load(ID).AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__9.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__9.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Sensor, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Sensor", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NotificationController.\u003C\u003Eo__9.\u003C\u003Ep__0.Target((CallSite) NotificationController.\u003C\u003Eo__9.\u003C\u003Ep__0, this.ViewBag, sensor);
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__9.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__9.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Gateway, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Gateway", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = NotificationController.\u003C\u003Eo__9.\u003C\u003Ep__1.Target((CallSite) NotificationController.\u003C\u003Eo__9.\u003C\u003Ep__1, this.ViewBag, gateway);
    if (sensor != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (NotificationController.\u003C\u003Eo__9.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NotificationController.\u003C\u003Eo__9.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, System.Collections.Generic.List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingNotificaitons", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = NotificationController.\u003C\u003Eo__9.\u003C\u003Ep__2.Target((CallSite) NotificationController.\u003C\u003Eo__9.\u003C\u003Ep__2, this.ViewBag, Notification.LoadBySensorID(sensor.SensorID));
    }
    if (gateway != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (NotificationController.\u003C\u003Eo__9.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NotificationController.\u003C\u003Eo__9.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, System.Collections.Generic.List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingNotificaitons", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = NotificationController.\u003C\u003Eo__9.\u003C\u003Ep__3.Target((CallSite) NotificationController.\u003C\u003Eo__9.\u003C\u003Ep__3, this.ViewBag, Notification.LoadByGatewayID(gateway.GatewayID));
    }
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public ActionResult ExistingNotificationList(
    long? sensorID,
    long? gatewayID,
    string q,
    int datumindex = 0)
  {
    Sensor sensor = (Sensor) null;
    Gateway gateway = (Gateway) null;
    long ID = long.MinValue;
    long? nullable1 = sensorID;
    long num1 = 0;
    if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
      sensor = Sensor.Load(sensorID ?? long.MinValue);
    long? nullable2 = gatewayID;
    long num2 = 0;
    if (nullable2.GetValueOrDefault() > num2 & nullable2.HasValue)
      gateway = Gateway.Load(gatewayID ?? long.MinValue);
    if (sensor != null)
      ID = sensor.CSNetID;
    if (gateway != null)
      ID = gateway.CSNetID;
    if (ID <= 0L)
      return (ActionResult) this.PartialView("Unauthorized");
    CSNet csNet = CSNet.Load(ID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    System.Collections.Generic.List<Notification> second = (System.Collections.Generic.List<Notification>) null;
    if (sensor != null)
      second = new System.Collections.Generic.List<Notification>(Notification.NotificationsForDatum(sensor.SensorID, datumindex).Union<Notification>(Notification.LoadBySensorID(sensor.SensorID).Where<Notification>((Func<Notification, bool>) (note => note.NotificationClass != eNotificationClass.Application))));
    if (gateway != null)
      second = Notification.LoadByGatewayID(gateway.GatewayID);
    System.Collections.Generic.List<Notification> model = new System.Collections.Generic.List<Notification>();
    IEnumerable<Notification> notifications = Notification.LoadByAccountID(csNet.AccountID).Where<Notification>((Func<Notification, bool>) (n => n.Name.ToLower().Contains(q.ToLower()) || n.NotificationText.ToLower().Contains(q.ToLower()))).Except<Notification>((IEnumerable<Notification>) second);
    System.Collections.Generic.List<eDatumType> eDatumTypeList = (System.Collections.Generic.List<eDatumType>) null;
    if (sensor != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (NotificationController.\u003C\u003Eo__10.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NotificationController.\u003C\u003Eo__10.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Sensor, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Sensor", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = NotificationController.\u003C\u003Eo__10.\u003C\u003Ep__0.Target((CallSite) NotificationController.\u003C\u003Eo__10.\u003C\u003Ep__0, this.ViewBag, sensor);
      eDatumTypeList = sensor.GetDatumTypes();
    }
    foreach (Notification notification in notifications)
    {
      if (notification.NotificationClass == eNotificationClass.Application && sensor != null && sensor.getDatumType(datumindex) == notification.eDatumType || notification.NotificationClass == eNotificationClass.Low_Battery && sensor != null && sensor.PowerSource.VoltageForZeroPercent != sensor.PowerSource.VoltageForOneHundredPercent || notification.NotificationClass == eNotificationClass.Advanced && sensor != null && !AdvancedNotification.Load(notification.AdvancedNotificationID).HasGatewayList || notification.NotificationClass == eNotificationClass.Low_Battery && gateway != null && gateway.PowerSource != null && gateway.PowerSource.VoltageForZeroPercent != gateway.PowerSource.VoltageForOneHundredPercent || notification.NotificationClass == eNotificationClass.Advanced && gateway != null && AdvancedNotification.Load(notification.AdvancedNotificationID).HasGatewayList || notification.NotificationClass == eNotificationClass.Credit || notification.NotificationClass == eNotificationClass.Inactivity)
        model.Add(notification);
    }
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult AddExistingNotification(
    long? sensorID,
    long? gatewayID,
    long[] notificationIDs,
    int[] datumindex = null)
  {
    if (datumindex == null)
    {
      datumindex = new int[notificationIDs.Length];
      for (int index = 0; index < notificationIDs.Length; ++index)
        datumindex[index] = 0;
    }
    Sensor sensor = (Sensor) null;
    Gateway gateway = (Gateway) null;
    long ID = long.MinValue;
    long? nullable = sensorID;
    long num1 = 0;
    if (nullable.GetValueOrDefault() > num1 & nullable.HasValue)
      sensor = Sensor.Load(sensorID ?? long.MinValue);
    nullable = gatewayID;
    long num2 = 0;
    if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
      gateway = Gateway.Load(gatewayID ?? long.MinValue);
    if (sensor != null)
      ID = sensor.CSNetID;
    if (gateway != null)
      ID = gateway.CSNetID;
    if (ID <= 0L)
      return (ActionResult) this.PartialView("Unauthorized");
    CSNet csNet = CSNet.Load(ID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    Account account = Account.Load(csNet.AccountID);
    int num3 = 0;
    foreach (long notificationId in notificationIDs)
    {
      Notification DBObject = Notification.Load(notificationId);
      if (DBObject != null && DBObject.AccountID == csNet.AccountID)
      {
        if (sensor != null && DBObject.NotificationClass == eNotificationClass.Application)
        {
          string changeRecord = $"{{\"SensorID\": \"{sensor.SensorID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
          DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned sensor datum to notification");
          DBObject.AddSensor(sensor, datumindex[num3++]);
        }
        else if (sensor != null)
        {
          string changeRecord = $"{{\"SensorID\": \"{sensor.SensorID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
          DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned sensor to notification");
          DBObject.AddSensor(sensor);
        }
        if (gateway != null)
        {
          string changeRecord = $"{{\"GatewayID\": \"{gateway.GatewayID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
          DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned gateway to notification");
          DBObject.AddGateway(gateway);
        }
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__11.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__11.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Sensor, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Sensor", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NotificationController.\u003C\u003Eo__11.\u003C\u003Ep__0.Target((CallSite) NotificationController.\u003C\u003Eo__11.\u003C\u003Ep__0, this.ViewBag, sensor);
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__11.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__11.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Gateway, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Gateway", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = NotificationController.\u003C\u003Eo__11.\u003C\u003Ep__1.Target((CallSite) NotificationController.\u003C\u003Eo__11.\u003C\u003Ep__1, this.ViewBag, gateway);
    if (sensor != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (NotificationController.\u003C\u003Eo__11.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NotificationController.\u003C\u003Eo__11.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, System.Collections.Generic.List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingNotificaitons", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = NotificationController.\u003C\u003Eo__11.\u003C\u003Ep__2.Target((CallSite) NotificationController.\u003C\u003Eo__11.\u003C\u003Ep__2, this.ViewBag, Notification.LoadBySensorID(sensor.SensorID));
    }
    if (gateway != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (NotificationController.\u003C\u003Eo__11.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NotificationController.\u003C\u003Eo__11.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, System.Collections.Generic.List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingNotificaitons", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = NotificationController.\u003C\u003Eo__11.\u003C\u003Ep__3.Target((CallSite) NotificationController.\u003C\u003Eo__11.\u003C\u003Ep__3, this.ViewBag, Notification.LoadByGatewayID(gateway.GatewayID));
    }
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public ActionResult AddNotificationTemplate(
    long? sensorID,
    long? gatewayID,
    long[] notificationTemplateIndexes)
  {
    Sensor sensor = (Sensor) null;
    Gateway gateway = (Gateway) null;
    long ID = long.MinValue;
    System.Collections.Generic.List<Notification> notificationList = (System.Collections.Generic.List<Notification>) null;
    long? nullable1 = sensorID;
    long num1 = 0;
    if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
      sensor = Sensor.Load(sensorID ?? long.MinValue);
    long? nullable2 = gatewayID;
    long num2 = 0;
    if (nullable2.GetValueOrDefault() > num2 & nullable2.HasValue)
      gateway = Gateway.Load(gatewayID ?? long.MinValue);
    if (sensor != null)
    {
      ID = sensor.CSNetID;
      notificationList = sensor.DefaultNotifications();
    }
    if (gateway != null)
    {
      ID = gateway.CSNetID;
      notificationList = new System.Collections.Generic.List<Notification>();
    }
    if (ID <= 0L)
      return (ActionResult) this.PartialView("Unauthorized");
    if (!MonnitSession.IsAuthorizedForAccount(CSNet.Load(ID).AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    foreach (int notificationTemplateIndex in notificationTemplateIndexes)
    {
      Notification notification = notificationList[notificationTemplateIndex];
      notification.Save();
      if (sensor != null)
        notification.AddSensor(sensor);
      if (gateway != null)
        notification.AddGateway(gateway);
      notification.AddCustomer(MonnitSession.CurrentCustomer, eNotificationType.Email);
    }
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__12.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__12.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Sensor, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Sensor", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NotificationController.\u003C\u003Eo__12.\u003C\u003Ep__0.Target((CallSite) NotificationController.\u003C\u003Eo__12.\u003C\u003Ep__0, this.ViewBag, sensor);
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__12.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__12.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Gateway, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Gateway", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = NotificationController.\u003C\u003Eo__12.\u003C\u003Ep__1.Target((CallSite) NotificationController.\u003C\u003Eo__12.\u003C\u003Ep__1, this.ViewBag, gateway);
    if (sensor != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (NotificationController.\u003C\u003Eo__12.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NotificationController.\u003C\u003Eo__12.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, System.Collections.Generic.List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingNotificaitons", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = NotificationController.\u003C\u003Eo__12.\u003C\u003Ep__2.Target((CallSite) NotificationController.\u003C\u003Eo__12.\u003C\u003Ep__2, this.ViewBag, Notification.LoadBySensorID(sensor.SensorID));
    }
    if (gateway != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (NotificationController.\u003C\u003Eo__12.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NotificationController.\u003C\u003Eo__12.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, System.Collections.Generic.List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingNotificaitons", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = NotificationController.\u003C\u003Eo__12.\u003C\u003Ep__3.Target((CallSite) NotificationController.\u003C\u003Eo__12.\u003C\u003Ep__3, this.ViewBag, Notification.LoadByGatewayID(gateway.GatewayID));
    }
    return (ActionResult) this.View("AddExistingNotification");
  }

  [AuthorizeDefault]
  public ActionResult RemoveExistingNotification(
    long? sensorID,
    long? gatewayID,
    long notificationID,
    bool? showNotificationEdit,
    int datumindex = 0)
  {
    Notification DBObject = Notification.Load(notificationID);
    if (!MonnitSession.IsAuthorizedForAccount(DBObject.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    Sensor sensor = Sensor.Load(sensorID ?? long.MinValue);
    Gateway gateway = Gateway.Load(gatewayID ?? long.MinValue);
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Sensor, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Sensor", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__0.Target((CallSite) NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__0, this.ViewBag, sensor);
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Gateway, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Gateway", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__1.Target((CallSite) NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__1, this.ViewBag, gateway);
    Account account = Account.Load(DBObject.AccountID);
    if (sensor != null)
    {
      string changeRecord = $"{{\"SensorID\": \"{sensor.SensorID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
      DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Removed sensor from notification");
      DBObject.RemoveSensor(sensor, datumindex);
      // ISSUE: reference to a compiler-generated field
      if (NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, System.Collections.Generic.List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingNotificaitons", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__2.Target((CallSite) NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__2, this.ViewBag, Notification.LoadBySensorID(sensor.SensorID));
    }
    if (gateway != null)
    {
      string changeRecord = $"{{\"GatewayID\": \"{gateway.GatewayID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
      DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Removed gateway from notification");
      DBObject.RemoveGateway(gateway);
      // ISSUE: reference to a compiler-generated field
      if (NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, System.Collections.Generic.List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingNotificaitons", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__3.Target((CallSite) NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__3, this.ViewBag, Notification.LoadByGatewayID(gateway.GatewayID));
    }
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ShowNotificationEdit", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj5 = NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__4.Target((CallSite) NotificationController.\u003C\u003Eo__13.\u003C\u003Ep__4, this.ViewBag, showNotificationEdit.GetValueOrDefault());
    return (ActionResult) this.View("AddExistingNotification");
  }

  [AuthorizeDefault]
  public ActionResult History(long id)
  {
    if (!MonnitSession.IsAuthorizedForAccount(Notification.Load(id).AccountID))
      return (ActionResult) this.PartialView("Unauthorized");
    long notificationID = id;
    DateTime utcFromLocalById1 = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate.Date, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime dateTime = MonnitSession.HistoryToDate;
    dateTime = dateTime.Date;
    DateTime utcFromLocalById2 = Monnit.TimeZone.GetUTCFromLocalById(dateTime.AddDays(1.0), MonnitSession.CurrentCustomer.Account.TimeZoneID);
    return (ActionResult) this.View((object) NotificationRecorded.LoadByNotificationAndDateRange(notificationID, utcFromLocalById1, utcFromLocalById2, 15));
  }

  [AuthorizeDefault]
  [HttpGet]
  public ActionResult NotificationValueEdit(long ScaleID, string compareValue, eDatumType edt)
  {
    if (!MonnitSession.IsAuthorizedForAccount(MonnitSession.CurrentCustomer.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    double x = compareValue.ToDouble();
    Sensor sens = Sensor.Load(ScaleID);
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    NotificationScaleInfoModel notificationScaleInfoModel = (NotificationScaleInfoModel) null;
    if (sens != null)
      notificationScaleInfoModel = MonnitApplicationBase.GetScalingInfo(sens);
    else if (AppDatum.HasStaticMethod(edt, "GetScalingInfo"))
      notificationScaleInfoModel = AppDatum.GetScalingInfo(edt, (long) (int) ScaleID);
    if (notificationScaleInfoModel != null)
      return (ActionResult) this.Json((object) JsonConvert.SerializeObject((object) new
      {
        Label = notificationScaleInfoModel.CustomLabel,
        realVal = x.LinearInterpolation(notificationScaleInfoModel.profileLow, notificationScaleInfoModel.baseLow, notificationScaleInfoModel.profileHigh, notificationScaleInfoModel.baseHigh, 2)
      }), JsonRequestBehavior.AllowGet);
    Notification notification = new Notification();
    notification.CompareValue = compareValue;
    MonnitApplicationBase.VerifyNotificationValues(notification, notificationScaleInfoModel.Label);
    double num = notification.CompareValue.ToDouble();
    return (ActionResult) this.Json((object) JsonConvert.SerializeObject((object) new
    {
      Label = notificationScaleInfoModel.Label,
      realVal = num
    }), JsonRequestBehavior.AllowGet);
  }

  [AuthorizeDefault]
  public ActionResult ScaleLabel(eDatumType edt, int ScaleID)
  {
    if (!MonnitSession.IsAuthorizedForAccount(MonnitSession.CurrentCustomer.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    string str = "None";
    if (AppDatum.HasStaticMethod(edt, "GetScalingInfo"))
      str = AppDatum.GetScalingInfo(edt, (long) ScaleID).Label;
    return (ActionResult) this.Json((object) JsonConvert.SerializeObject((object) new
    {
      ScaleName = str
    }), JsonRequestBehavior.AllowGet);
  }

  [AuthorizeDefault]
  public ActionResult Edit(long id)
  {
    Notification notification = Notification.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(notification.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    NotificationController.AddTimeDropDowns(notification, this.ViewData);
    if (notification.AdvancedNotificationID > long.MinValue)
    {
      this.ViewData["AdvancedNotification"] = (object) AdvancedNotification.Load(notification.AdvancedNotificationID);
      System.Collections.Generic.List<AdvancedNotificationParameter> notificationParameterList = AdvancedNotificationParameter.LoadByAdvancedNotificationID(notification.AdvancedNotificationID);
      this.ViewData["Params"] = (object) notificationParameterList;
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      System.Collections.Generic.List<AdvancedNotificationParameterValue> notificationParameterValueList = AdvancedNotificationParameterValue.LoadByNotificationID(notification.NotificationID);
      foreach (AdvancedNotificationParameter notificationParameter in notificationParameterList)
      {
        AdvancedNotificationParameter p = notificationParameter;
        if (!(p.ParameterName.ToLower() == "sensorid"))
        {
          try
          {
            string key = p.AdvancedNotificationParameterID.ToString();
            dictionary.Add(key, notificationParameterValueList.Find((Predicate<AdvancedNotificationParameterValue>) (v => v.AdvancedNotificationParameterID == p.AdvancedNotificationParameterID)).ParameterValue);
          }
          catch
          {
          }
        }
      }
      this.Session["AdvancedNotificationParams"] = (object) dictionary;
    }
    return (ActionResult) this.View((object) notification);
  }

  [AuthorizeDefault]
  public ActionResult NotificationTextOverride() => (ActionResult) this.View();

  [AuthorizeDefault]
  [HttpPost]
  [ValidateInput(false)]
  public ActionResult Edit(long id, FormCollection collection, string returnPage)
  {
    Notification notification = Notification.Load(id);
    if (notification != null && !MonnitSession.IsAuthorizedForAccount(notification.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    if (!((IEnumerable<string>) collection.AllKeys).Contains<string>("Name") || string.IsNullOrEmpty(collection["Name"]))
      this.ModelState.AddModelError("Name", "Title field is required.");
    collection["Name"] = collection["Name"].SanitizeHTMLEvent();
    collection["Subject"] = collection["Subject"].SanitizeHTMLEvent();
    collection["NotificationText"] = collection["NotificationText"].SanitizeHTMLEvent();
    if (!this.ModelState.IsValid)
      return (ActionResult) this.View((object) notification);
    if (notification == null)
      notification = new Notification();
    notification.HasUserNotificationAction = true;
    this.UpdateModel<Notification>(notification);
    Account account = Account.Load(notification.AccountID);
    if (account != null)
      notification.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited notification settings");
    notification.CanAutoAcknowledge = ((IEnumerable<string>) collection.AllKeys).Contains<string>("CanAutoAcknowledge");
    notification.SMSText = ((IEnumerable<string>) collection.AllKeys).Contains<string>("showSMS") ? collection["SMSText"].SanitizeHTMLEvent() : string.Empty;
    if (!string.IsNullOrEmpty(notification.SMSText) && notification.SMSText.HasScriptTag())
      this.ModelState.AddModelError("SMSText", "Script tags not permitted.");
    notification.VoiceText = ((IEnumerable<string>) collection.AllKeys).Contains<string>("showVoice") ? collection["VoiceText"].SanitizeHTMLEvent() : string.Empty;
    if (!string.IsNullOrEmpty(notification.VoiceText) && notification.VoiceText.HasScriptTag())
      this.ModelState.AddModelError("VoiceText", "Script tags not permitted.");
    notification.PushMsgText = ((IEnumerable<string>) collection.AllKeys).Contains<string>("showPushMsg") ? collection["PushMsgText"].SanitizeHTMLEvent() : string.Empty;
    if (!string.IsNullOrEmpty(notification.PushMsgText) && notification.PushMsgText.HasScriptTag())
      this.ModelState.AddModelError("PushMsgText", "Script tags not permitted.");
    if (notification.NotificationClass == eNotificationClass.Application)
    {
      try
      {
        if (!MonnitApplicationBase.VerifyNotificationValues(notification, notification.Scale))
          DataTypeBase.VerifyNotificationValues(notification, notification.Scale);
      }
      catch
      {
      }
    }
    if (notification.NotificationClass == eNotificationClass.Inactivity)
    {
      notification.CompareType = eCompareType.Greater_Than;
      double num = notification.CompareValue.ToDouble();
      if (num < 1.0 || num > 525600.0)
        this.ModelState.AddModelError("CompareValue", "Invalid Inactive Period");
    }
    if (notification.NotificationClass == eNotificationClass.Low_Battery)
    {
      notification.CompareType = eCompareType.Less_Than;
      int num = notification.CompareValue.ToInt();
      if (num < 1)
        notification.CompareValue = "1";
      if (num > 100)
        notification.CompareValue = "100";
    }
    System.Collections.Generic.List<AdvancedNotificationParameterValue> notificationParameterValueList = AdvancedNotificationParameterValue.LoadByNotificationID(id);
    if (notification.NotificationClass == eNotificationClass.Advanced && notification.AdvancedNotificationID > long.MinValue)
    {
      this.ViewData["AdvancedNotification"] = (object) AdvancedNotification.Load(notification.AdvancedNotificationID);
      System.Collections.Generic.List<AdvancedNotificationParameter> notificationParameterList = AdvancedNotificationParameter.LoadByAdvancedNotificationID(notification.AdvancedNotificationID);
      this.ViewData["Params"] = (object) notificationParameterList;
      Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
      foreach (AdvancedNotificationParameter notificationParameter in notificationParameterList)
      {
        AdvancedNotificationParameter p = notificationParameter;
        if (!(p.ParameterName.ToLower() == "sensorid"))
        {
          FormCollection formCollection = collection;
          long notificationParameterId = p.AdvancedNotificationParameterID;
          string name = notificationParameterId.ToString();
          string parameterValue = formCollection[name].Split(',')[0];
          AdvancedNotificationParameterValue notificationParameterValue = notificationParameterValueList.Find((Predicate<AdvancedNotificationParameterValue>) (v => v.NotificationID == id && v.AdvancedNotificationParameterID == p.AdvancedNotificationParameterID));
          if (notificationParameterValue == null)
          {
            notificationParameterValue = new AdvancedNotificationParameterValue(p.AdvancedNotificationParameterID, id, parameterValue);
            notificationParameterValueList.Add(notificationParameterValue);
          }
          if (p.IsValid(notificationParameterValue, parameterValue))
          {
            notificationParameterValue.ParameterValue = parameterValue;
            Dictionary<string, string> dictionary2 = dictionary1;
            notificationParameterId = p.AdvancedNotificationParameterID;
            string key = notificationParameterId.ToString();
            string str = parameterValue;
            dictionary2.Add(key, str);
          }
          else
            this.ViewData.ModelState.AddModelError("", $"{p.ParameterName.Replace("_", " ")} is not valid.");
        }
      }
      this.Session["AdvancedNotificationParams"] = (object) dictionary1;
    }
    if (notification.NotificationClass == eNotificationClass.Timed)
    {
      string compareValue = notification.CompareValue;
      notification.NotificationByTime.ScheduledHour = collection["ScheduledHour"].ToInt() + collection["AMorPM"].ToInt();
      notification.NotificationByTime.ScheduledMinute = collection["ScheduledMinute"].ToInt();
      string str = $"{notification.NotificationByTime.ScheduledHour}:{notification.NotificationByTime.ScheduledMinute}";
      if (compareValue != str)
        notification.NotificationByTime.NextEvaluationDate = DateTime.MinValue;
      notification.CompareValue = str;
    }
    if (notification.SnoozeDuration < 0 || notification.SnoozeDuration > 720)
      this.ModelState.AddModelError("SnoozeDuration", "Invalid Snooze");
    if (!this.ModelState.IsValid)
      return (ActionResult) this.View((object) notification);
    try
    {
      if (notification.MondayScheduleID == long.MinValue)
      {
        NotificationController.SetActivePeriod(notification, collection, this.ModelState);
        notification.MondaySchedule.Save();
        notification.MondayScheduleID = notification.MondaySchedule.NotificationScheduleID;
        notification.TuesdaySchedule.Save();
        notification.TuesdayScheduleID = notification.TuesdaySchedule.NotificationScheduleID;
        notification.WednesdaySchedule.Save();
        notification.WednesdayScheduleID = notification.WednesdaySchedule.NotificationScheduleID;
        notification.ThursdaySchedule.Save();
        notification.ThursdayScheduleID = notification.ThursdaySchedule.NotificationScheduleID;
        notification.FridaySchedule.Save();
        notification.FridayScheduleID = notification.FridaySchedule.NotificationScheduleID;
        notification.SaturdaySchedule.Save();
        notification.SaturdayScheduleID = notification.SaturdaySchedule.NotificationScheduleID;
        notification.SundaySchedule.Save();
        notification.SundayScheduleID = notification.SundaySchedule.NotificationScheduleID;
      }
      notification.Version = "1";
      if (notification.NotificationClass == eNotificationClass.Timed)
      {
        notification.NotificationByTime.Save();
        notification.NotificationByTimeID = notification.NotificationByTime.NotificationByTimeID;
      }
      notification.Save();
      if (notification.NotificationClass == eNotificationClass.Advanced && notification.AdvancedNotificationID > long.MinValue)
      {
        foreach (AdvancedNotificationParameterValue notificationParameterValue in notificationParameterValueList)
        {
          notificationParameterValue.NotificationID = notification.NotificationID;
          notificationParameterValue.Save();
        }
      }
      if (string.IsNullOrEmpty(returnPage))
        return (ActionResult) this.View((object) notification);
      string str = "";
      long notificationId;
      if (returnPage.Contains("Wizard"))
      {
        try
        {
          long ID1 = returnPage.Replace("Wizard|QuickSensor|", "").Replace("Wizard|AssignSensor|", "").ToLong();
          long ID2 = returnPage.Replace("Wizard|QuickGateway|", "").Replace("Wizard|AssignGateway|", "").ToLong();
          if (ID1 > 0L)
            notification.AddSensor(Sensor.Load(ID1), notification.DatumIndex);
          if (ID2 > 0L)
            notification.AddGateway(Gateway.Load(ID2));
          notification.AddCustomer(MonnitSession.CurrentCustomer, eNotificationType.Email);
        }
        catch
        {
        }
        notificationId = notification.NotificationID;
        str = $"/Notification/Wizard/{notificationId.ToString()}?tab=Recipient&returnPage={returnPage.Replace("Wizard|", "")}";
      }
      if (string.IsNullOrEmpty(str))
      {
        notificationId = notification.NotificationID;
        str = $"/Notification?notification={notificationId.ToString()}&tab=From";
      }
      return (ActionResult) this.Content($"<script>window.location.href = '{str}'</script>");
    }
    catch (Exception ex)
    {
      this.ViewData.ModelState.AddModelError("", ex.Message);
      return (ActionResult) this.View((object) notification);
    }
  }

  [AuthorizeDefault]
  public ActionResult Calendar(long id)
  {
    NotificationScheduleDisableModel model = new NotificationScheduleDisableModel()
    {
      notification = Notification.Load(id),
      notificationScheduleDisabledList = NotificationScheduleDisabled.LoadByNotificationID(id)
    };
    model.dic = NotificationScheduleDisabled.fillMonthDayDic(model.notificationScheduleDisabledList);
    this.ViewData["ShowAdvancedScheduleSchedule"] = model.notificationScheduleDisabledList.Count > 0 ? (object) "false" : (object) "true";
    NotificationController.AddTimeDropDowns(model.notification, this.ViewData);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateInput(false)]
  public ActionResult Calendar(
    FormCollection collection,
    string[] Jan,
    string[] Feb,
    string[] Mar,
    string[] Apr,
    string[] May,
    string[] June,
    string[] July,
    string[] Aug,
    string[] Sept,
    string[] Oct,
    string[] Nov,
    string[] Dec)
  {
    try
    {
      long notificationID = collection["id"].ToLong();
      if (notificationID > 0L)
      {
        NotificationScheduleDisabled.DeleteByNotificationID(notificationID);
        if (Jan != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Jan), 1);
        if (Feb != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Feb), 2);
        if (Mar != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Mar), 3);
        if (Apr != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Apr), 4);
        if (May != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(May), 5);
        if (June != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(June), 6);
        if (July != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(July), 7);
        if (Aug != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Aug), 8);
        if (Sept != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Sept), 9);
        if (Oct != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Oct), 10);
        if (Nov != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Nov), 11);
        if (Dec != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Dec), 12);
        NotificationScheduleDisableModel scheduleDisableModel = new NotificationScheduleDisableModel()
        {
          notification = Notification.Load(collection["id"].ToLong())
        };
        scheduleDisableModel.notificationScheduleDisabledList = NotificationScheduleDisabled.LoadByNotificationID(scheduleDisableModel.notification.NotificationID);
        scheduleDisableModel.dic = NotificationScheduleDisabled.fillMonthDayDic(scheduleDisableModel.notificationScheduleDisabledList);
        this.ViewData["ShowAdvancedScheduleSchedule"] = (object) (((IEnumerable<string>) collection.AllKeys).Contains<string>("ShowAdvancedScheduleSchedule") && collection["ShowAdvancedScheduleSchedule"].ToLower() == "true,false");
        scheduleDisableModel.notification.AlwaysSend = ((IEnumerable<string>) collection.AllKeys).Contains<string>("AlwaysSend") && collection["AlwaysSend"].ToLower().Contains("true");
        NotificationController.SetActivePeriod(scheduleDisableModel.notification, collection, this.ModelState);
        NotificationController.AddTimeDropDowns(scheduleDisableModel.notification, this.ViewData);
        scheduleDisableModel.notification.MondaySchedule.Save();
        scheduleDisableModel.notification.TuesdaySchedule.Save();
        scheduleDisableModel.notification.WednesdaySchedule.Save();
        scheduleDisableModel.notification.ThursdaySchedule.Save();
        scheduleDisableModel.notification.FridaySchedule.Save();
        scheduleDisableModel.notification.SaturdaySchedule.Save();
        scheduleDisableModel.notification.SundaySchedule.Save();
        scheduleDisableModel.notification.Save();
        string str = "/Notification/Calendar/" + scheduleDisableModel.notification.NotificationID.ToString();
        // ISSUE: reference to a compiler-generated field
        if (NotificationController.\u003C\u003Eo__21.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NotificationController.\u003C\u003Eo__21.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = NotificationController.\u003C\u003Eo__21.\u003C\u003Ep__0.Target((CallSite) NotificationController.\u003C\u003Eo__21.\u003C\u003Ep__0, this.ViewBag, "Notification Schedule Edit Success");
        // ISSUE: reference to a compiler-generated field
        if (NotificationController.\u003C\u003Eo__21.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NotificationController.\u003C\u003Eo__21.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = NotificationController.\u003C\u003Eo__21.\u003C\u003Ep__1.Target((CallSite) NotificationController.\u003C\u003Eo__21.\u003C\u003Ep__1, this.ViewBag, str);
        return (ActionResult) this.PartialView("EditConfirmation");
      }
    }
    catch (Exception ex)
    {
    }
    return (ActionResult) this.Content("Failed");
  }

  public static NotificationSchedule SetDaySchedule(
    Notification notification,
    FormCollection collection,
    DayOfWeek day,
    ModelStateDictionary modelstate)
  {
    NotificationSchedule notificationSchedule = notification.GetNotificationSchedule(day);
    int hours1 = 0;
    int minutes1 = 0;
    int hours2 = 0;
    int minutes2 = 0;
    if (notification.AlwaysSend)
    {
      notificationSchedule.NotificationDaySchedule = eNotificationDaySchedule.All_Day;
    }
    else
    {
      switch (collection[day.ToString() + "Schedule.NotificationDaySchedule"])
      {
        case "All_Day":
          notificationSchedule.NotificationDaySchedule = eNotificationDaySchedule.All_Day;
          break;
        case "Off":
          notificationSchedule.NotificationDaySchedule = eNotificationDaySchedule.Off;
          break;
        case "Between":
          notificationSchedule.NotificationDaySchedule = eNotificationDaySchedule.Between;
          hours1 = collection[day.ToString() + "StartTimeHour"].ToInt();
          if (hours1 == 12 && collection[day.ToString() + "StartTimeAM"] == "PM")
            hours1 = hours1;
          else if (hours1 == 12 && collection[day.ToString() + "StartTimeAM"] == "AM")
            hours1 -= 12;
          else if (collection[day.ToString() + "StartTimeAM"] == "PM")
            hours1 += 12;
          minutes1 = collection[day.ToString() + "StartTimeMinute"].ToInt();
          hours2 = collection[day.ToString() + "EndTimeHour"].ToInt();
          if (hours2 == 12 && collection[day.ToString() + "EndTimeAM"] == "PM")
            hours2 = hours2;
          else if (hours2 == 12 && collection[day.ToString() + "EndTimeAM"] == "AM")
            hours2 -= 12;
          else if (collection[day.ToString() + "EndTimeAM"] == "PM")
            hours2 += 12;
          minutes2 = collection[day.ToString() + "EndTimeMinute"].ToInt();
          if (hours1 == hours2 && minutes1 == minutes2 && collection[day.ToString() + "EndTimeAM"].Equals(collection[day.ToString() + "StartTimeAM"]))
          {
            modelstate.AddModelError(day.ToString() + "Schedule.NotificationDaySchedule", "invalid schedule");
            break;
          }
          if (hours1 >= hours2 && minutes1 >= minutes2 && collection[day.ToString() + "EndTimeAM"].Equals(collection[day.ToString() + "StartTimeAM"]))
          {
            modelstate.AddModelError(day.ToString() + "Schedule.NotificationDaySchedule", "invalid schedule");
            break;
          }
          break;
        case "Before_and_After":
          notificationSchedule.NotificationDaySchedule = eNotificationDaySchedule.Before_and_After;
          hours1 = collection[day.ToString() + "StartTimeHour"].ToInt();
          if (hours1 == 12 && collection[day.ToString() + "StartTimeAM"] == "PM")
            hours1 = hours1;
          else if (hours1 == 12 && collection[day.ToString() + "StartTimeAM"] == "AM")
            hours1 -= 12;
          else if (collection[day.ToString() + "StartTimeAM"] == "PM")
            hours1 += 12;
          minutes1 = collection[day.ToString() + "StartTimeMinute"].ToInt();
          hours2 = collection[day.ToString() + "EndTimeHour"].ToInt();
          if (hours2 == 12 && collection[day.ToString() + "EndTimeAM"] == "PM")
            hours2 = hours2;
          else if (hours2 == 12 && collection[day.ToString() + "EndTimeAM"] == "AM")
            hours2 -= 12;
          else if (collection[day.ToString() + "EndTimeAM"] == "PM")
            hours2 += 12;
          minutes2 = collection[day.ToString() + "EndTimeMinute"].ToInt();
          if (hours1 == hours2 && minutes1 == minutes2 && collection[day.ToString() + "EndTimeAM"].Equals(collection[day.ToString() + "StartTimeAM"]))
          {
            modelstate.AddModelError(day.ToString() + "Schedule.NotificationDaySchedule", string.Format("invalid schedule"));
            break;
          }
          if (hours1 >= hours2 && minutes1 >= minutes2 && collection[day.ToString() + "EndTimeAM"].Equals(collection[day.ToString() + "StartTimeAM"]))
          {
            modelstate.AddModelError(day.ToString() + "Schedule.NotificationDaySchedule", "invalid schedule");
            break;
          }
          if (hours2 == 0 && minutes2 == 0 && collection[day.ToString() + "EndTimeAM"] == "AM" || hours1 == 0 && minutes1 == 0 && collection[day.ToString() + "StartTimeAM"] == "AM")
          {
            modelstate.AddModelError(day.ToString() + "Schedule.NotificationDaySchedule", string.Format("invalid schedule"));
            modelstate.AddModelError(day.ToString() + "Schedule.NotificationDaySchedule", string.Format("invalid schedule"));
            break;
          }
          break;
        case "Before":
          notificationSchedule.NotificationDaySchedule = eNotificationDaySchedule.Before;
          hours2 = 0;
          minutes2 = 0;
          hours1 = collection[day.ToString() + "StartTimeHour"].ToInt();
          if (hours1 == 12 && collection[day.ToString() + "StartTimeAM"] == "PM")
            hours1 = hours1;
          else if (hours1 == 12 && collection[day.ToString() + "StartTimeAM"] == "AM")
            hours1 -= 12;
          else if (collection[day.ToString() + "StartTimeAM"] == "PM")
            hours1 += 12;
          minutes1 = collection[day.ToString() + "StartTimeMinute"].ToInt();
          if (hours1 == 0 && minutes1 == 0 && collection[day.ToString() + "StartTimeAM"] == "AM")
          {
            modelstate.AddModelError(day.ToString() + "Schedule.NotificationDaySchedule", string.Format("invalid schedule"));
            break;
          }
          break;
        case "After":
          notificationSchedule.NotificationDaySchedule = eNotificationDaySchedule.After;
          hours1 = 0;
          minutes1 = 0;
          hours2 = collection[day.ToString() + "EndTimeHour"].ToInt();
          if (hours2 == 12 && collection[day.ToString() + "EndTimeAM"] == "PM")
            hours2 = hours2;
          else if (hours2 == 12 && collection[day.ToString() + "EndTimeAM"] == "AM")
            hours2 -= 12;
          else if (collection[day.ToString() + "EndTimeAM"] == "PM")
            hours2 += 12;
          minutes2 = collection[day.ToString() + "EndTimeMinute"].ToInt();
          if (hours2 == 0 && minutes2 == 0 && collection[day.ToString() + "EndTimeAM"] == "AM")
          {
            modelstate.AddModelError(day.ToString() + "Schedule.NotificationDaySchedule", string.Format("invalid schedule"));
            break;
          }
          break;
      }
    }
    notificationSchedule.FirstTime = new TimeSpan(hours1, minutes1, 0);
    notificationSchedule.SecondTime = new TimeSpan(hours2, minutes2, 0);
    return notificationSchedule;
  }

  public static void SetActivePeriod(
    Notification notification,
    FormCollection collection,
    ModelStateDictionary modelstate)
  {
    Account account = Account.Load(notification.AccountID);
    if (account != null)
      notification.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited notification schedule");
    notification.MondaySchedule = NotificationController.SetDaySchedule(notification, collection, DayOfWeek.Monday, modelstate);
    notification.TuesdaySchedule = NotificationController.SetDaySchedule(notification, collection, DayOfWeek.Tuesday, modelstate);
    notification.WednesdaySchedule = NotificationController.SetDaySchedule(notification, collection, DayOfWeek.Wednesday, modelstate);
    notification.ThursdaySchedule = NotificationController.SetDaySchedule(notification, collection, DayOfWeek.Thursday, modelstate);
    notification.FridaySchedule = NotificationController.SetDaySchedule(notification, collection, DayOfWeek.Friday, modelstate);
    notification.SaturdaySchedule = NotificationController.SetDaySchedule(notification, collection, DayOfWeek.Saturday, modelstate);
    notification.SundaySchedule = NotificationController.SetDaySchedule(notification, collection, DayOfWeek.Sunday, modelstate);
    if (notification.MondaySchedule.NotificationDaySchedule == eNotificationDaySchedule.All_Day && notification.TuesdaySchedule.NotificationDaySchedule == eNotificationDaySchedule.All_Day && notification.WednesdaySchedule.NotificationDaySchedule == eNotificationDaySchedule.All_Day && notification.ThursdaySchedule.NotificationDaySchedule == eNotificationDaySchedule.All_Day && notification.FridaySchedule.NotificationDaySchedule == eNotificationDaySchedule.All_Day && notification.SaturdaySchedule.NotificationDaySchedule == eNotificationDaySchedule.All_Day && notification.SundaySchedule.NotificationDaySchedule == eNotificationDaySchedule.All_Day)
      notification.AlwaysSend = true;
    else
      notification.AlwaysSend = false;
  }

  public static void AddTimeDropDowns(Notification notification, ViewDataDictionary viewData)
  {
    viewData["ShowSchedule"] = (object) true;
    viewData["ShowTimeOfDay"] = (object) true;
    foreach (int day in System.Enum.GetValues(typeof (DayOfWeek)))
    {
      try
      {
        NotificationSchedule notificationSchedule = notification.GetNotificationSchedule((DayOfWeek) day);
        viewData[day.ToString() + "Schedule.NotificationDaySchedule"] = (object) notificationSchedule.NotificationDaySchedule.ToString();
        TimeSpan timeSpan1 = notificationSchedule.FirstTime != TimeSpan.MinValue ? notificationSchedule.FirstTime : new TimeSpan(0L);
        TimeSpan timeSpan2 = notificationSchedule.SecondTime != TimeSpan.MinValue ? notificationSchedule.SecondTime : new TimeSpan(0L);
        string[] items = new string[12]
        {
          "1",
          "2",
          "3",
          "4",
          "5",
          "6",
          "7",
          "8",
          "9",
          "10",
          "11",
          "12"
        };
        viewData[((DayOfWeek) day).ToString() + "StartHours"] = (object) new SelectList((IEnumerable) items, (object) (timeSpan1.Hours > 12 ? timeSpan1.Hours - 12 : (timeSpan1.Hours == 0 ? 12 : timeSpan1.Hours)).ToString());
        viewData[((DayOfWeek) day).ToString() + "StartMinutes"] = (object) new SelectList((IEnumerable) new string[4]
        {
          "00",
          "15",
          "30",
          "45"
        }, (object) timeSpan1.Minutes.ToString());
        viewData[((DayOfWeek) day).ToString() + "StartAM"] = (object) new SelectList((IEnumerable) new string[2]
        {
          "AM",
          "PM"
        }, timeSpan1.Hours < 12 ? (object) "AM" : (object) "PM");
        ViewDataDictionary viewDataDictionary1 = viewData;
        DayOfWeek dayOfWeek = (DayOfWeek) day;
        string key1 = dayOfWeek.ToString() + "EndHours";
        SelectList selectList1 = new SelectList((IEnumerable) items, (object) (timeSpan2.Hours > 12 ? timeSpan2.Hours - 12 : (timeSpan2.Hours == 0 ? 12 : timeSpan2.Hours)).ToString());
        viewDataDictionary1[key1] = (object) selectList1;
        ViewDataDictionary viewDataDictionary2 = viewData;
        dayOfWeek = (DayOfWeek) day;
        string key2 = dayOfWeek.ToString() + "EndMinutes";
        SelectList selectList2 = new SelectList((IEnumerable) new string[4]
        {
          "00",
          "15",
          "30",
          "45"
        }, (object) timeSpan2.Minutes.ToString());
        viewDataDictionary2[key2] = (object) selectList2;
        ViewDataDictionary viewDataDictionary3 = viewData;
        dayOfWeek = (DayOfWeek) day;
        string key3 = dayOfWeek.ToString() + "EndAM";
        SelectList selectList3 = new SelectList((IEnumerable) new string[2]
        {
          "AM",
          "PM"
        }, timeSpan2.Hours < 12 ? (object) "AM" : (object) "PM");
        viewDataDictionary3[key3] = (object) selectList3;
      }
      catch (Exception ex)
      {
        ExceptionLog.Log(ex);
      }
    }
  }

  [AuthorizeDefault]
  public ActionResult SentFrom(long id)
  {
    Notification model = Notification.Load(id);
    return !MonnitSession.IsAuthorizedForAccount(model.AccountID) || !MonnitSession.CustomerCan("Notification_Edit") ? (ActionResult) this.PartialView("Unauthorized") : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ToggleSensor(long id, long sensorID, bool add, int datumindex)
  {
    try
    {
      Notification DBObject = Notification.Load(id);
      if (DBObject == null)
        return (ActionResult) this.Content("Notification not found");
      Sensor sensor = Sensor.Load(sensorID);
      if (sensor == null)
        return (ActionResult) this.Content("Sensor not found");
      CSNet csNet = CSNet.Load(sensor.CSNetID);
      if (DBObject.AccountID != csNet.AccountID)
        return (ActionResult) this.Content("Sensor not found");
      if (add)
      {
        Account account = Account.Load(DBObject.AccountID);
        if (account != null)
        {
          string changeRecord = $"{{\"SensorID\": \"{sensor.SensorID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
          DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned sensor datum to notification");
        }
        DBObject.AddSensor(sensor, datumindex);
      }
      else
      {
        Account account = Account.Load(DBObject.AccountID);
        if (account != null)
        {
          string changeRecord = $"{{\"SensorID\": \"{sensor.SensorID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
          DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Removed sensor datum from notification");
        }
        DBObject.RemoveSensor(sensor, datumindex);
      }
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Error updating notification.");
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ToggleGateway(long id, long gatewayID, bool add)
  {
    try
    {
      Notification DBObject = Notification.Load(id);
      if (DBObject == null || !MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
        return (ActionResult) this.Content("Notification not found");
      Gateway gateway = Gateway.Load(gatewayID);
      if (gateway == null)
        return (ActionResult) this.Content("Gateway not found");
      CSNet csNet = CSNet.Load(gateway.CSNetID);
      if (DBObject.AccountID != csNet.AccountID)
        return (ActionResult) this.Content("Gateway not found");
      if (add)
      {
        Account account = Account.Load(DBObject.AccountID);
        if (account != null)
        {
          string changeRecord = $"{{\"GatewayID\": \"{gateway.GatewayID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
          DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned gateway to notification");
        }
        DBObject.AddGateway(gateway);
      }
      else
      {
        Account account = Account.Load(DBObject.AccountID);
        if (account != null)
        {
          string changeRecord = $"{{\"GatewayID\": \"{gateway.GatewayID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
          DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Removed gateway from notification");
        }
        DBObject.RemoveGateway(gateway);
      }
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Error updating notification.");
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult AddSensor(long id, long[] sensorIDs, int[] datumindex = null)
  {
    try
    {
      if (datumindex == null)
      {
        datumindex = new int[sensorIDs.Length];
        for (int index = 0; index < sensorIDs.Length; ++index)
          datumindex[index] = 0;
      }
      Notification noti = Notification.Load(id);
      if (noti == null)
        return (ActionResult) this.Content("Notification not found");
      int index1 = -1;
      foreach (long sensorId in sensorIDs)
      {
        ++index1;
        Sensor sensor = Sensor.Load(sensorId);
        if (sensor != null)
        {
          CSNet csNet = CSNet.Load(sensor.CSNetID);
          if (noti.AccountID == csNet.AccountID && !noti.SensorsThatNotify.Contains(new SensorDatum(sensor, datumindex[index1], long.MinValue)))
            noti.AddSensor(sensor, datumindex[index1]);
        }
      }
      return (ActionResult) this.PartialView((object) new ConfigureSensorNotificationModel(noti));
    }
    catch (Exception ex)
    {
      ex.Log("NotificationController.AddSensor " + ex.ToString());
      return (ActionResult) this.Content("Unable to add sensor(s)");
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult AddGateway(long id, long[] gatewayIDs)
  {
    try
    {
      Notification noti = Notification.Load(id);
      if (noti == null)
        return (ActionResult) this.Content("Notification not found");
      foreach (long gatewayId in gatewayIDs)
      {
        Gateway gateway = Gateway.Load(gatewayId);
        if (gateway != null)
        {
          CSNet csNet = CSNet.Load(gateway.CSNetID);
          if (noti.AccountID == csNet.AccountID && noti.GatewaysThatNotify.Where<Gateway>((Func<Gateway, bool>) (g => g.GatewayID == gateway.GatewayID)).Count<Gateway>() <= 0)
            noti.AddGateway(gateway);
        }
      }
      return (ActionResult) this.PartialView((object) new ConfigureSensorNotificationModel(noti));
    }
    catch
    {
      return (ActionResult) this.Content("Unable to add gateway(s)");
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult RemoveSensor(long id, long sensorID, int datumindex = 0)
  {
    try
    {
      Notification notification = Notification.Load(id);
      if (notification == null)
        return (ActionResult) this.Content("Notification not found");
      Sensor sensor = Sensor.Load(sensorID);
      if (sensor == null)
        return (ActionResult) this.Content("Sensor not found");
      notification.RemoveSensor(sensor, datumindex);
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Unable to remove sensor");
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult RemoveGateway(long id, long gatewayID)
  {
    try
    {
      Notification notification = Notification.Load(id);
      if (notification == null)
        return (ActionResult) this.Content("Notification not found");
      Gateway gateway = Gateway.Load(gatewayID);
      if (gateway == null)
        return (ActionResult) this.Content("Gateway not found");
      notification.RemoveGateway(gateway);
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Unable to remove gateway(s).");
    }
  }

  [AuthorizeDefault]
  public ActionResult SentFromSensorList(long id, string q, long? networkID)
  {
    Notification noti = Notification.Load(id);
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__32.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__32.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AdvancedNoti", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NotificationController.\u003C\u003Eo__32.\u003C\u003Ep__0.Target((CallSite) NotificationController.\u003C\u003Eo__32.\u003C\u003Ep__0, this.ViewBag, (object) null);
    ConfigureSensorNotificationModel notificationModel = new ConfigureSensorNotificationModel(noti);
    AdvancedNotification advancedNotification = AdvancedNotification.Load(noti.AdvancedNotificationID);
    if (advancedNotification != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (NotificationController.\u003C\u003Eo__32.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NotificationController.\u003C\u003Eo__32.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, AdvancedNotification, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AdvancedNoti", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = NotificationController.\u003C\u003Eo__32.\u003C\u003Ep__1.Target((CallSite) NotificationController.\u003C\u003Eo__32.\u003C\u003Ep__1, this.ViewBag, advancedNotification);
    }
    System.Collections.Generic.List<SensorNoficationModel> source = new System.Collections.Generic.List<SensorNoficationModel>();
    foreach (SensorNoficationModel sensor in notificationModel.SensorList)
    {
      if (advancedNotification != null)
      {
        if (advancedNotification.CanAdd((object) sensor.Sensor))
        {
          int num1;
          if (networkID.HasValue)
          {
            long? nullable = networkID;
            long num2 = 0;
            if (!(nullable.GetValueOrDefault() < num2 & nullable.HasValue))
            {
              long csNetId = sensor.Sensor.CSNetID;
              nullable = networkID;
              long valueOrDefault = nullable.GetValueOrDefault();
              num1 = csNetId == valueOrDefault & nullable.HasValue ? 1 : 0;
              goto label_14;
            }
          }
          num1 = 1;
label_14:
          if (num1 != 0)
            source.Add(sensor);
        }
      }
      else
        source.Add(sensor);
    }
    return (ActionResult) this.PartialView((object) source.Where<SensorNoficationModel>((Func<SensorNoficationModel, bool>) (m => m.Sensor.SensorName.ToLower().Contains(q.ToLower()) || m.Sensor.SensorID.ToString().Contains(q))));
  }

  [AuthorizeDefault]
  public ActionResult SentFromGatewayList(long id, string q, long? networkID)
  {
    System.Collections.Generic.List<GatewayNoficationModel> source = new System.Collections.Generic.List<GatewayNoficationModel>();
    foreach (GatewayNoficationModel gateway in new ConfigureSensorNotificationModel(Notification.Load(id)).GatewayList)
    {
      int num1;
      if (networkID.HasValue)
      {
        long? nullable = networkID;
        long num2 = -1;
        if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
        {
          nullable = networkID;
          long num3 = 0;
          if (nullable.GetValueOrDefault() > num3 & nullable.HasValue)
          {
            long csNetId = gateway.Gateway.CSNetID;
            nullable = networkID;
            long valueOrDefault = nullable.GetValueOrDefault();
            num1 = csNetId == valueOrDefault & nullable.HasValue ? 1 : 0;
            goto label_8;
          }
          num1 = 0;
          goto label_8;
        }
      }
      num1 = 1;
label_8:
      if (num1 != 0)
        source.Add(gateway);
    }
    return (ActionResult) this.PartialView((object) source.Where<GatewayNoficationModel>((Func<GatewayNoficationModel, bool>) (m => m.Gateway.Name.ToLower().Contains(q.ToLower()) || m.Gateway.GatewayID.ToString().Contains(q))));
  }

  [AuthorizeDefault]
  public ActionResult RetrieveDevices(long id)
  {
    return (ActionResult) this.PartialView((object) new ConfigureSensorNotificationModel(Notification.Load(id)));
  }

  [AuthorizeDefault]
  public ActionResult Recipient(long id)
  {
    Notification model = Notification.Load(id);
    return !MonnitSession.IsAuthorizedForAccount(model.AccountID) || !MonnitSession.CustomerCan("Notification_Edit") ? (ActionResult) this.PartialView("Unauthorized") : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult UserList(long id, string q)
  {
    Notification notification = Notification.Load(id);
    if (notification == null)
      return (ActionResult) this.Content("Notification not found");
    System.Collections.Generic.List<PotentialNotificationRecipient> model = PotentialNotificationRecipient.Load(MonnitSession.CurrentCustomer.CustomerID, notification.NotificationID, q);
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__36.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__36.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, System.Collections.Generic.List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationRecipients", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = NotificationController.\u003C\u003Eo__36.\u003C\u003Ep__0.Target((CallSite) NotificationController.\u003C\u003Eo__36.\u003C\u003Ep__0, this.ViewBag, notification.NotificationRecipients);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ToggleRecipient(
    long id,
    long customerID,
    eNotificationType notificationType,
    bool add)
  {
    try
    {
      Notification DBObject = Notification.Load(id);
      if (DBObject == null)
        return (ActionResult) this.Content("Notification not found");
      if (add)
      {
        Customer customer = Customer.Load(customerID);
        if (customer == null)
          return (ActionResult) this.Content("User not found");
        if (DBObject.AccountID != customer.AccountID && CustomerAccountLink.Load(customer.CustomerID, DBObject.AccountID) == null)
        {
          bool flag = false;
          foreach (Account ancestor in Account.Ancestors(MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID))
          {
            if (ancestor.AccountID == customer.AccountID)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
            return (ActionResult) this.Content("User not found");
        }
        Account account = Account.Load(DBObject.AccountID);
        if (account != null)
        {
          string changeRecord = $"{{\"CustomerID\": \"{customer.CustomerID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\", \"NotifcationType\" : \"{notificationType.ToString()}\" }} ";
          DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned recipient to notification");
        }
        DBObject.AddCustomer(customer, notificationType);
      }
      else
      {
        Account account = Account.Load(DBObject.AccountID);
        foreach (NotificationRecipient recipient in DBObject.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (nr => nr.CustomerToNotifyID == customerID && nr.NotificationType == notificationType)).ToList<NotificationRecipient>())
        {
          if (account != null)
          {
            string changeRecord = $"{{\"CustomerID\": \"{recipient.CustomerToNotifyID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
            DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Removed recipient from notification");
          }
          DBObject.RemoveRecipient(recipient);
        }
      }
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Unable to remove");
    }
  }

  [AuthorizeDefault]
  public ActionResult SetDelayMinutes(long notificationRecipientID, int delayMinutes)
  {
    try
    {
      NotificationRecipient notificationRecipient = NotificationRecipient.Load(notificationRecipientID);
      Account account = (Account) null;
      Notification notification = Notification.Load(notificationRecipient.NotificationID);
      if (notification != null)
        account = Account.Load(notification.AccountID);
      if (account != null)
      {
        string record = $"{{\"CustomerID\": \"{notificationRecipient.CustomerToNotifyID}\", \"NotifcationID\" : \"{notificationRecipient.NotificationID}\", \"DelayMinutes\" : \"{delayMinutes}\", \"NotifcationType\" : \"{notificationRecipient.NotificationType.ToString()}\" }} ";
        AuditLog.LogAuditData(notificationRecipient.CustomerToNotifyID, notificationRecipient.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, record, account.AccountID, "Edited recipient delay minutes on notification");
      }
      notificationRecipient.DelayMinutes = delayMinutes;
      notificationRecipient.Save();
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Unable to set delay minutes");
    }
  }

  [AuthorizeDefault]
  public ActionResult RecipientDevices(long id)
  {
    Notification model = Notification.Load(id);
    return !MonnitSession.IsAuthorizedForAccount(model.AccountID) || !MonnitSession.CustomerCan("Notification_Edit") ? (ActionResult) this.PartialView("Unauthorized") : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult RecipientDeviceList(long id, string q)
  {
    Notification notification = Notification.Load(id);
    if (notification == null)
      return (ActionResult) this.Content("Notification not found");
    IEnumerable<Sensor> model = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where<Sensor>((Func<Sensor, bool>) (s => (s.ApplicationID == 12L || s.ApplicationID == 13L || s.ApplicationID == 76L || s.ApplicationID == 97L) && (s.SensorName.ToLower().Contains(q.ToLower()) || s.SensorID.ToString().ToLower().Contains(q.ToLower()))));
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__40.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__40.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Notification, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Notification", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = NotificationController.\u003C\u003Eo__40.\u003C\u003Ep__0.Target((CallSite) NotificationController.\u003C\u003Eo__40.\u003C\u003Ep__0, this.ViewBag, notification);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ToggleDevice(long id, long deviceID, string deviceType, bool add)
  {
    try
    {
      Notification notification = Notification.Load(id);
      if (notification == null)
        return (ActionResult) this.Content("Notification not found");
      if (add)
      {
        Sensor sensor = Sensor.Load(deviceID);
        if (sensor == null)
          return (ActionResult) this.Content("Device not found");
        if (notification.AccountID != sensor.AccountID)
          return (ActionResult) this.Content("Device not found");
        Account account = Account.Load(notification.AccountID);
        if (account != null)
        {
          string changeRecord = $"{{\"NotificationID\" : \"{notification.NotificationID}\", \"DeviceID\": \"{deviceID}\", \"DeviceType\" : \"{deviceType}\", \"Add\" : \"{(ValueType) true}\" }} ";
          notification.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned device to notification");
        }
        NotificationRecipient notificationRecipient;
        switch (deviceType)
        {
          case "Notifier":
            notificationRecipient = notification.AddSensorRecipient(sensor, Attention.CreateSerializedRecipientProperties(true, true, true, true, sensor.SensorName));
            notification.HasLocalAlertAction = true;
            break;
          case "Control1":
            notificationRecipient = notification.AddSensorRecipient(sensor, Control_1.CreateSerializedRecipientProperties(1, 0, 0, 0));
            notification.HasControlAction = true;
            break;
          case "Control2":
            notificationRecipient = notification.AddSensorRecipient(sensor, Control_1.CreateSerializedRecipientProperties(0, 1, 0, 0));
            notification.HasControlAction = true;
            break;
          case "BasicControl":
            notificationRecipient = notification.AddSensorRecipient(sensor, BasicControl.CreateSerializedRecipientProperties(1, 0));
            notification.HasControlAction = true;
            break;
          case "Thermostat":
            notificationRecipient = notification.AddSensorRecipient(sensor, Thermostat.CreateSerializedRecipientProperties(1, (ushort) 30));
            notification.HasThermostatAction = true;
            break;
        }
        notification.Save();
      }
      else
      {
        NotificationRecipient notificationRecipient = NotificationControllerBase.FindNotificationRecipient(deviceID, deviceType, notification);
        if (notificationRecipient == null)
          return (ActionResult) this.Content("Not found");
        Account account = Account.Load(notification.AccountID);
        if (account != null)
        {
          string changeRecord = $"{{\"NotificationID\" : \"{notification.NotificationID}\", \"DeviceID\": \"{deviceID}\", \"DeviceType\" : \"{deviceType}\", \"Add\" : \"{(ValueType) false}\" }} ";
          notification.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Removed device from notification");
        }
        notification.RemoveRecipient(notificationRecipient);
      }
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Unable to add device");
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult AddRecipientDevice(
    long id,
    long[] attentionIDs,
    long[] controlID1s,
    long[] controlID2s,
    long[] basicIDs,
    long[] thermoIDs)
  {
    try
    {
      Notification DBObject = Notification.Load(id);
      if (DBObject == null)
        return (ActionResult) this.Content("Notification not found");
      Account account = Account.Load(DBObject.AccountID);
      if (account != null && attentionIDs.Length != 0)
      {
        foreach (long attentionId in attentionIDs)
        {
          Sensor sensor = Sensor.Load(attentionId);
          if (sensor != null && DBObject.AccountID == sensor.AccountID)
          {
            string changeRecord = string.Format("{{\"SensorID\": \"{0}\",  LocalNotifier : {{ \"LED ON\" : \"{2}\", \"Buzzer ON\" : \"{3}\", \"AutoScroll ON\" : \"{4}\", \"BackLight ON\" : \"{5}\", \"Sensor Name\" : \"{6}\" }}  , \"NotifcationID\" : \"{1}\" }} ", (object) sensor.SensorID, (object) DBObject.NotificationID, (object) true, (object) true, (object) true, (object) true, (object) sensor.SensorName);
            DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned local notifier to notification");
            DBObject.AddSensorRecipient(sensor, Attention.CreateSerializedRecipientProperties(true, true, true, true, sensor.SensorName));
            DBObject.HasLocalAlertAction = true;
            DBObject.Save();
          }
        }
      }
      if (controlID1s.Length != 0)
      {
        foreach (long controlId1 in controlID1s)
        {
          Sensor sensor = Sensor.Load(controlId1);
          if (sensor != null && DBObject.AccountID == sensor.AccountID)
          {
            string changeRecord = string.Format("{{\"SensorID\": \"{0}\",  ControlUnit : {{ \"State1\" : \"{2}\", \"State2\" : \"{3}\", \"Relay1Time\" : \"{4}\", \"Relay2Time\" : \"{5}\" }}  , \"NotifcationID\" : \"{1}\" }} ", (object) sensor.SensorID, (object) DBObject.NotificationID, (object) 1, (object) 0, (object) 0, (object) 0);
            DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned control  unit 1 to notification");
            DBObject.AddSensorRecipient(sensor, Control_1.CreateSerializedRecipientProperties(1, 0, 0, 0));
            DBObject.HasControlAction = true;
            DBObject.Save();
          }
        }
      }
      if (controlID2s.Length != 0)
      {
        foreach (long controlId2 in controlID2s)
        {
          Sensor sensor = Sensor.Load(controlId2);
          if (sensor != null && DBObject.AccountID == sensor.AccountID)
          {
            string changeRecord = string.Format("{{\"SensorID\": \"{0}\",  ControlUnit : {{ \"State1\" : \"{2}\", \"State2\" : \"{3}\", \"Relay1Time\" : \"{4}\", \"Relay2Time\" : \"{5}\" }}  , \"NotifcationID\" : \"{1}\" }} ", (object) sensor.SensorID, (object) DBObject.NotificationID, (object) 1, (object) 0, (object) 0, (object) 0);
            DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned control  unit 2 to notification");
            DBObject.AddSensorRecipient(sensor, Control_1.CreateSerializedRecipientProperties(0, 1, 0, 0));
            DBObject.HasControlAction = true;
            DBObject.Save();
          }
        }
      }
      if (basicIDs.Length != 0)
      {
        foreach (long basicId in basicIDs)
        {
          Sensor sensor = Sensor.Load(basicId);
          if (sensor != null && DBObject.AccountID == sensor.AccountID)
          {
            string changeRecord = string.Format("{{\"SensorID\": \"{0}\",  BasicControl : {{ \"State1\" : \"{2}\", \"Relay1Time\" : \"{3}\" }}  , \"NotifcationID\" : \"{1}\" }} ", (object) sensor.SensorID, (object) DBObject.NotificationID, (object) 1, (object) 0);
            DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned basic control to notification");
            DBObject.AddSensorRecipient(sensor, BasicControl.CreateSerializedRecipientProperties(1, 0));
            DBObject.HasControlAction = true;
            DBObject.Save();
          }
        }
        return (ActionResult) this.View((object) DBObject.NotificationRecipients);
      }
      if (thermoIDs.Length != 0)
      {
        foreach (long thermoId in thermoIDs)
        {
          Sensor sensor = Sensor.Load(thermoId);
          if (sensor != null && DBObject.AccountID == sensor.AccountID)
          {
            string changeRecord = string.Format("{{\"SensorID\": \"{0}\",  Thermostat : {{ \"Occupancy\" : \"{2}\", \"Duration\" : \"{3}\" }}  , \"NotifcationID\" : \"{1}\" }} ", (object) sensor.SensorID, (object) DBObject.NotificationID, (object) 1, (object) 0);
            DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned thermostat to notification");
            DBObject.AddSensorRecipient(sensor, Thermostat.CreateSerializedRecipientProperties(1, (ushort) 0));
            DBObject.HasThermostatAction = true;
            DBObject.Save();
          }
        }
        return (ActionResult) this.View((object) DBObject.NotificationRecipients);
      }
    }
    catch
    {
      return (ActionResult) this.Content("<script type='text/javascript'>alert('Unable to add device(s).');</script>");
    }
    return (ActionResult) this.Content("<script type='text/javascript'>alert('Unable to add device(s).');</script>");
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult RemoveRecipientDevice(long id, long notificationRecipientID)
  {
    try
    {
      Notification DBObject = Notification.Load(id);
      if (DBObject == null)
        return (ActionResult) this.Content("Notification not found");
      NotificationRecipient recipient = NotificationRecipient.Load(notificationRecipientID);
      if (recipient == null)
        return (ActionResult) this.Content("Not found");
      Account account = Account.Load(DBObject.AccountID);
      if (account != null)
      {
        string changeRecord = $"{{\"SensorID\": \"{recipient.DeviceToNotifyID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
        DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Removed recipient device from notification ");
      }
      DBObject.RemoveRecipient(recipient);
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Unable to remove recipient.");
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult RecipientDeviceState(
    long id,
    long deviceID,
    string deviceType,
    string field,
    string state)
  {
    try
    {
      Notification notification = Notification.Load(id);
      if (notification == null)
        return (ActionResult) this.Content("Notification not found");
      NotificationRecipient notificationRecipient = NotificationControllerBase.FindNotificationRecipient(deviceID, deviceType, notification);
      Account account = Account.Load(notification.AccountID);
      if (account != null)
      {
        switch (notificationRecipient.NotificationType)
        {
          case eNotificationType.Local_Notifier:
            bool led;
            bool buzzer;
            bool autoScroll;
            bool backLight;
            string deviceName;
            Attention.ParseSerializedRecipientProperties(notificationRecipient.SerializedRecipientProperties, out led, out buzzer, out autoScroll, out backLight, out deviceName);
            string changeRecord1 = string.Format("{{\"SensorID\": \"{0}\",  LocalNotifier : {{ \"LED ON\" : \"{2}\", \"Buzzer ON\" : \"{3}\", \"AutoScroll ON\" : \"{4}\", \"BackLight ON\" : \"{5}\", \"Sensor Name\" : \"{6}\" }}  , \"NotifcationID\" : \"{1}\" }} ", (object) deviceID, (object) notification.NotificationID, (object) led, (object) buzzer, (object) autoScroll, (object) backLight, (object) deviceName);
            notification.LogAuditData(eAuditAction.Related_Modify, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord1, account.AccountID, "Edited local notifier settings");
            switch (field.ToLower())
            {
              case "led":
                led = state.ToLower() != "off";
                break;
              case "buzzer":
                buzzer = state.ToLower() != "off";
                break;
              case "screen":
                switch (state.ToLower())
                {
                  case "off":
                    autoScroll = false;
                    backLight = false;
                    break;
                  case "on":
                    autoScroll = true;
                    backLight = true;
                    break;
                  case "scroll":
                    autoScroll = true;
                    backLight = false;
                    break;
                }
                break;
            }
            notificationRecipient.SerializedRecipientProperties = Attention.CreateSerializedRecipientProperties(led, buzzer, autoScroll, backLight, string.IsNullOrWhiteSpace(deviceName) ? deviceID.ToStringSafe() : deviceName);
            notificationRecipient.Save();
            break;
          case eNotificationType.Control:
            if (deviceType == "BasicControl")
            {
              int state1;
              ushort time1;
              BasicControl.ParseSerializedRecipientProperties(notificationRecipient.SerializedRecipientProperties, out state1, out time1);
              string changeRecord2 = string.Format("{{\"SensorID\": \"{0}\",  BasicControl : {{ \"State1\" : \"{2}\", \"Relay1Time\" : \"{3}\" }}  , \"NotifcationID\" : \"{1}\" }} ", (object) deviceID, (object) notification.NotificationID, (object) state1, (object) time1);
              notification.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord2, account.AccountID, "Edited basic control settings");
              switch (field.ToLower())
              {
                case "time1":
                  time1 = Convert.ToUInt16(state);
                  break;
                case "relay1":
                case "state1":
                  switch (state.ToLower())
                  {
                    case "off":
                      state1 = 1;
                      break;
                    case "on":
                      state1 = 2;
                      break;
                    case "toggle":
                      state1 = 3;
                      break;
                    default:
                      state1 = 1;
                      break;
                  }
                  break;
              }
              notificationRecipient.SerializedRecipientProperties = BasicControl.CreateSerializedRecipientProperties(state1, (int) time1);
              notificationRecipient.Save();
              break;
            }
            int state1_1;
            int state2;
            ushort time1_1;
            ushort time2;
            Control_1.ParseSerializedRecipientProperties(notificationRecipient.SerializedRecipientProperties, out state1_1, out state2, out time1_1, out time2);
            string changeRecord3 = string.Format("{{\"SensorID\": \"{0}\",  ControlUnit : {{ \"State1\" : \"{2}\", \"State2\" : \"{3}\", \"Relay1Time\" : \"{4}\", \"Relay2Time\" : \"{5}\" }}  , \"NotifcationID\" : \"{1}\" }} ", (object) deviceID, (object) notification.NotificationID, (object) state1_1, (object) state2, (object) time1_1, (object) time2);
            notification.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord3, account.AccountID, "Edited control unit settings");
            switch (field.ToLower())
            {
              case "time1":
                time1_1 = Convert.ToUInt16(state);
                break;
              case "time2":
                time2 = Convert.ToUInt16(state);
                break;
              case "relay1":
              case "state1":
                switch (state.ToLower())
                {
                  case "off":
                    state1_1 = 1;
                    break;
                  case "on":
                    state1_1 = 2;
                    break;
                  case "toggle":
                    state1_1 = 3;
                    break;
                  default:
                    state1_1 = 1;
                    break;
                }
                break;
              case "relay2":
              case "state2":
                switch (state.ToLower())
                {
                  case "off":
                    state2 = 1;
                    break;
                  case "on":
                    state2 = 2;
                    break;
                  case "toggle":
                    state2 = 3;
                    break;
                  default:
                    state2 = 1;
                    break;
                }
                break;
            }
            notificationRecipient.SerializedRecipientProperties = Control_1.CreateSerializedRecipientProperties(state1_1, state2, (int) time1_1, (int) time2);
            notificationRecipient.Save();
            break;
          case eNotificationType.Thermostat:
            if (deviceType == "Thermostat")
            {
              int Occupancy;
              ushort Duration;
              Thermostat.ParseSerializedRecipientProperties(notificationRecipient.SerializedRecipientProperties, out Occupancy, out Duration);
              string changeRecord4 = string.Format("{{\"SensorID\": \"{0}\",  Thermostat : {{ \"Occupancy\" : \"{2}\", \"Duration\" : \"{3}\" }}  , \"NotifcationID\" : \"{1}\" }} ", (object) deviceID, (object) notification.NotificationID, (object) Occupancy, (object) Duration);
              notification.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord4, account.AccountID, "Edited thermostat settings");
              switch (field.ToLower())
              {
                case "duration":
                  Duration = Convert.ToUInt16(state);
                  break;
                case "occupancy":
                  switch (state.ToLower())
                  {
                    case "off":
                      Occupancy = 0;
                      break;
                    case "on":
                      Occupancy = 1;
                      break;
                    default:
                      Occupancy = 0;
                      break;
                  }
                  break;
              }
              notificationRecipient.SerializedRecipientProperties = Thermostat.CreateSerializedRecipientProperties(Occupancy, Duration);
              notificationRecipient.Save();
              break;
            }
            break;
        }
      }
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Unable to add device");
    }
  }

  [AuthorizeDefault]
  public ActionResult SystemAction(long id)
  {
    Notification model = Notification.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.PartialView("Unauthorized");
    int totalNotifications;
    System.Collections.Generic.List<Notification> notificationList = this.GetNotificationList(out totalNotifications);
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__45.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__45.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalNotifications", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NotificationController.\u003C\u003Eo__45.\u003C\u003Ep__0.Target((CallSite) NotificationController.\u003C\u003Eo__45.\u003C\u003Ep__0, this.ViewBag, totalNotifications);
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__45.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__45.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, System.Collections.Generic.List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationList", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = NotificationController.\u003C\u003Eo__45.\u003C\u003Ep__1.Target((CallSite) NotificationController.\u003C\u003Eo__45.\u003C\u003Ep__1, this.ViewBag, notificationList);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult SystemAction(FormCollection collection, long id)
  {
    Notification model = Notification.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.PartialView("Unauthorized");
    new NotificationRecipient()
    {
      NotificationID = id,
      ActionToExecuteID = collection["action"].ToLong(),
      SerializedRecipientProperties = collection["ActionTarget"],
      NotificationType = eNotificationType.SystemAction,
      DelayMinutes = 0
    }.Save();
    model.HasSystemAction = true;
    model.Save();
    int totalNotifications;
    System.Collections.Generic.List<Notification> notificationList = this.GetNotificationList(out totalNotifications);
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__46.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__46.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalNotifications", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NotificationController.\u003C\u003Eo__46.\u003C\u003Ep__0.Target((CallSite) NotificationController.\u003C\u003Eo__46.\u003C\u003Ep__0, this.ViewBag, totalNotifications);
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__46.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__46.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, System.Collections.Generic.List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationList", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = NotificationController.\u003C\u003Eo__46.\u003C\u003Ep__1.Target((CallSite) NotificationController.\u003C\u003Eo__46.\u003C\u003Ep__1, this.ViewBag, notificationList);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult ActionDelete(long id)
  {
    NotificationRecipient recipient = NotificationRecipient.Load(id);
    Notification notification = Notification.Load(recipient.NotificationID);
    if (!MonnitSession.IsAuthorizedForAccount(notification.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    try
    {
      notification.RemoveRecipient(recipient);
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Unable to remove");
    }
  }

  [AuthorizeDefault]
  public ActionResult AlertDetails(long id)
  {
    return (ActionResult) this.View((object) NotificationTriggered.LoadActiveByNotificationID(id).OrderByDescending<NotificationTriggered, DateTime>((Func<NotificationTriggered, DateTime>) (m => m.StartTime)).ToList<NotificationTriggered>());
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult AcknowledgeTriggeredNotification(long triggeredID, string AckMethod)
  {
    if (!MonnitSession.CustomerCan("Notification_Can_Acknowledge"))
      return (ActionResult) this.Content("Unauthorized");
    Account account = MonnitSession.CurrentCustomer.Account;
    NotificationTriggered notificationTriggered = NotificationTriggered.Load(triggeredID);
    notificationTriggered.AcknowledgeMethod = AckMethod ?? "Unknown";
    notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
    notificationTriggered.AcknowledgedBy = MonnitSession.CurrentCustomer.CustomerID;
    notificationTriggered.Save();
    if (account != null)
      AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, notificationTriggered.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, notificationTriggered.JsonStringify(), account.AccountID, "Acknowledged rule");
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult FullReset(long triggeredID, string AckMethod)
  {
    if (!MonnitSession.CustomerCan("Notification_Can_Acknowledge"))
      return (ActionResult) this.Content("Unauthorized");
    Account account = MonnitSession.CurrentCustomer.Account;
    NotificationTriggered notificationTriggered = NotificationTriggered.Load(triggeredID);
    notificationTriggered.AcknowledgeMethod = AckMethod ?? "Unknown";
    notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
    notificationTriggered.AcknowledgedBy = MonnitSession.CurrentCustomer.CustomerID;
    notificationTriggered.resetTime = DateTime.UtcNow;
    notificationTriggered.Save();
    if (account != null)
      AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, notificationTriggered.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, notificationTriggered.JsonStringify(), account.AccountID, "Acknowledged rule");
    return (ActionResult) this.Content("Success");
  }

  [HttpGet]
  public ActionResult TwilioCall(long id, long notificationRecordedID)
  {
    NotificationRecorded model = NotificationRecorded.Load(notificationRecordedID);
    if (model == null || model.CustomerID != id)
      return (ActionResult) this.Content("<?xml version='1.0' encoding='UTF-8'?> <Response> <Say voice='woman'>This is a Notification from your sensor portal. Please login to view status.</Say> </Response>");
    try
    {
      model.Status = "Call Answered";
      model.Save();
    }
    catch (Exception ex)
    {
      ex.Log("Notification.TwilioCall- Unable to update status ");
    }
    return (ActionResult) this.PartialView((object) model);
  }

  [HttpPost]
  public ActionResult TwilioCall(
    long id,
    long notificationRecordedID,
    string digits,
    string Repeat)
  {
    if (digits == "1")
    {
      NotificationRecorded notificationRecorded = NotificationRecorded.Load(notificationRecordedID);
      NotificationTriggered notificationTriggered = NotificationTriggered.LoadByNotificationRecordedID(notificationRecordedID);
      if (notificationRecorded != null && notificationRecorded.CustomerID == id)
      {
        try
        {
          Customer customer = Customer.Load(id);
          if (customer != null && customer.Can("Notification_Can_Acknowledge"))
          {
            notificationTriggered.AcknowledgeMethod = "Phone";
            notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
            notificationTriggered.AcknowledgedBy = id;
            notificationTriggered.Save();
            return (ActionResult) this.Content("<?xml version='1.0' encoding='UTF-8'?> <Response> <Say voice='woman'>Notification acknowledged. Goodbye</Say> </Response>");
          }
        }
        catch (Exception ex)
        {
          ex.Log("Notification.TwilioCall-unable to acknowledge notification ");
        }
      }
      return (ActionResult) this.Content("<?xml version='1.0' encoding='UTF-8'?> <Response> <Say voice='woman'>Unable to acknowledged notification. Please log in to portal.</Say> </Response>");
    }
    return !Repeat.ToBool() || digits == "2" ? this.TwilioCall(id, notificationRecordedID) : (ActionResult) this.Content("<?xml version='1.0' encoding='UTF-8'?> <Response> <Say voice='woman'>Goodbye</Say> </Response>");
  }

  [HttpGet]
  public ActionResult PhoneVerification(long id, long validationID)
  {
    Validation model = Validation.Load(validationID);
    return model != null && model.CustomerID == id ? (ActionResult) this.PartialView((object) model) : (ActionResult) this.Content("<?xml version='1.0' encoding='UTF-8'?> <Response> <Say voice='woman'>You have choosen to Opt in for Voice Notifications.</Say> </Response>");
  }

  [HttpPost]
  public ActionResult PhoneVerification(long id, long validationID, string digits, string Repeat)
  {
    if (digits == "1")
    {
      Validation validation = Validation.Load(validationID);
      if (validation != null && validation.CustomerID == id)
      {
        try
        {
          this.NotificationOptIn(validationID);
          return (ActionResult) this.Content("<?xml version='1.0' encoding='UTF-8'?> <Response> <Say voice='woman'>Voice Noticications opted in successfully. Goodbye</Say> </Response>");
        }
        catch (Exception ex)
        {
          ex.Log("Notification.TwilioCall-unable to acknowledge notification ");
        }
      }
      return (ActionResult) this.Content("<?xml version='1.0' encoding='UTF-8'?> <Response> <Say voice='woman'>Unable to opt in for Phone Verification.</Say> </Response>");
    }
    return !Repeat.ToBool() ? this.PhoneVerification(id, validationID) : (ActionResult) this.Content("<?xml version='1.0' encoding='UTF-8'?> <Response> <Say voice='woman'>Goodbye</Say> </Response>");
  }

  [HttpPost]
  public ActionResult AssignCredits(long id, int notificationCreditsToAssign, DateTime? expiration)
  {
    Account account = Account.Load(id);
    if (account == null)
      return (ActionResult) this.Content("Not Authorized.");
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
      return (ActionResult) this.Content("Not Authorized.");
    long num = long.MinValue;
    foreach (NotificationCreditType notificationCreditType in NotificationCreditType.LoadAll())
    {
      if (notificationCreditType.Name.ToLower() == "credit" && expiration.HasValue)
      {
        num = notificationCreditType.NotificationCreditTypeID;
        break;
      }
      if (notificationCreditType.Name.ToLower() == "permanent credit" && !expiration.HasValue)
      {
        num = notificationCreditType.NotificationCreditTypeID;
        break;
      }
    }
    if (num == long.MinValue)
      return (ActionResult) this.Content("Notification credit type not found.");
    if (notificationCreditsToAssign > 0)
      new NotificationCredit()
      {
        NotificationCreditTypeID = num,
        AccountID = account.AccountID,
        ActivatedByCustomerID = MonnitSession.CurrentCustomer.CustomerID,
        ActivationDate = DateTime.UtcNow,
        ActivationCode = "",
        ActivatedCredits = notificationCreditsToAssign,
        ExpirationDate = (expiration ?? new DateTime(2099, 1, 1))
      }.Save();
    return (ActionResult) this.Content("Success");
  }

  [HttpPost]
  public ActionResult RemoveCredits(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
      return (ActionResult) this.Content("Not Authorized.");
    NotificationCredit notificationCredit = NotificationCredit.Load(id);
    if (notificationCredit == null)
      return (ActionResult) this.Content("Not Found.");
    notificationCredit.Delete();
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult List(long id)
  {
    Sensor sensor = Sensor.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.PartialView("Unauthorized");
    this.ViewData["Sensor"] = (object) sensor;
    ViewDataDictionary viewData = this.ViewData;
    long sensorID = id;
    DateTime dateTime = MonnitSession.HistoryFromDate;
    DateTime utcFromLocalById1 = Monnit.TimeZone.GetUTCFromLocalById(dateTime.Date, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    dateTime = MonnitSession.HistoryToDate;
    dateTime = dateTime.Date;
    DateTime utcFromLocalById2 = Monnit.TimeZone.GetUTCFromLocalById(dateTime.AddDays(1.0), MonnitSession.CurrentCustomer.Account.TimeZoneID);
    System.Collections.Generic.List<NotificationRecorded> notificationRecordedList = NotificationRecorded.LoadBySensorAndDateRange(sensorID, utcFromLocalById1, utcFromLocalById2, 100);
    viewData["Notifications"] = (object) notificationRecordedList;
    return (ActionResult) this.View((object) Notification.LoadBySensorID(id).Where<Notification>((Func<Notification, bool>) (noti => noti.AccountID == sensor.AccountID)).ToList<Notification>());
  }

  [AuthorizeDefault]
  public ActionResult GatewayList(long id)
  {
    Gateway gateway = Gateway.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(CSNet.Load(gateway.CSNetID).AccountID))
      return (ActionResult) this.PartialView("Unauthorized");
    this.ViewData["Gateway"] = (object) gateway;
    ViewDataDictionary viewData = this.ViewData;
    long gatewayID = id;
    DateTime utcFromLocalById1 = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate.Date, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime dateTime = MonnitSession.HistoryToDate;
    dateTime = dateTime.Date;
    DateTime utcFromLocalById2 = Monnit.TimeZone.GetUTCFromLocalById(dateTime.AddDays(1.0), MonnitSession.CurrentCustomer.Account.TimeZoneID);
    System.Collections.Generic.List<NotificationRecorded> notificationRecordedList = NotificationRecorded.LoadByGatewayAndDateRange(gatewayID, utcFromLocalById1, utcFromLocalById2, 10);
    viewData["Notifications"] = (object) notificationRecordedList;
    return (ActionResult) this.View((object) Notification.LoadByGatewayID(id));
  }

  [AuthorizeDefault]
  public ActionResult AcknowledgeActiveNotifications(
    long notificationID,
    long userID,
    string AckMethod)
  {
    if (!MonnitSession.CustomerCan("Notification_Can_Acknowledge"))
      return (ActionResult) this.Content("Unauthorized");
    Account account = MonnitSession.CurrentCustomer.Account;
    if (account != null)
    {
      foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveByNotificationID(notificationID))
      {
        notificationTriggered.AcknowledgeMethod = AckMethod ?? "Unknown";
        notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
        notificationTriggered.AcknowledgedBy = userID;
        notificationTriggered.Save();
        if (account != null)
          AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, notificationTriggered.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, notificationTriggered.JsonStringify(), account.AccountID, "Acknowledged rule");
      }
    }
    return (ActionResult) this.Content("Success");
  }

  [HttpPost]
  [AuthorizeDefault]
  public string AcknowledgeActiveNotificationsOnlyReturnString(
    long notificationID,
    long userID,
    string AckMethod)
  {
    if (!MonnitSession.CustomerCan("Notification_Can_Acknowledge"))
      return "Unauthorized";
    Account account = MonnitSession.CurrentCustomer.Account;
    if (account != null)
    {
      foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveByNotificationID(notificationID))
      {
        notificationTriggered.AcknowledgeMethod = AckMethod ?? "Unknown";
        notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
        notificationTriggered.AcknowledgedBy = userID;
        notificationTriggered.Save();
        if (account != null)
          AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, notificationTriggered.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, notificationTriggered.JsonStringify(), account.AccountID, "Acknowledged rule");
      }
    }
    return "Success";
  }

  [AuthorizeDefault]
  public ActionResult ResetPendingConditions(long notificationID, long userID)
  {
    if (!MonnitSession.CustomerCan("Notification_Can_Acknowledge"))
      return (ActionResult) this.Content("Unauthorized");
    try
    {
      Account account = MonnitSession.CurrentCustomer.Account;
      if (account != null)
      {
        foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveByNotificationID(notificationID))
        {
          notificationTriggered.resetTime = DateTime.UtcNow;
          notificationTriggered.Save();
          AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, notificationTriggered.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, notificationTriggered.JsonStringify(), account.AccountID, "Reset notification with conditions still pending");
        }
      }
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Error occurred.");
    }
  }

  [AuthorizeDefault]
  public ActionResult ResetPendingCondition(long notificationRecordedID, long userID)
  {
    return this.ResetPendingConditionGeneric(NotificationTriggered.LoadByNotificationRecordedID(notificationRecordedID));
  }

  private ActionResult ResetPendingConditionGeneric(NotificationTriggered triggered)
  {
    if (!MonnitSession.CustomerCan("Notification_Can_Acknowledge"))
      return (ActionResult) this.Content("Unauthorized");
    try
    {
      if (triggered == null)
        return (ActionResult) this.Content("Unauthorized");
      triggered.resetTime = DateTime.UtcNow;
      triggered.Save();
      Account account = Account.Load(triggered.NotificationID);
      if (account != null)
        AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, triggered.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, triggered.JsonStringify(), account.AccountID, "Reset notification with conditions still pending");
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Error occurred.");
    }
  }

  [HttpPost]
  [AuthorizeDefault]
  public ActionResult ResetPendingConditionByNotificationTrigger(
    long notificationTriggeredID,
    long userID)
  {
    return this.ResetPendingConditionGeneric(NotificationTriggered.Load(notificationTriggeredID));
  }

  [HttpPost]
  [AuthorizeDefault]
  public ActionResult AcknowledgeNotification(
    long notificationRecordedID,
    long userID,
    string AckMethod)
  {
    return this.AcknowledgeByGeneric(NotificationTriggered.LoadByNotificationRecordedID(notificationRecordedID), userID, AckMethod);
  }

  private ActionResult AcknowledgeByGeneric(
    NotificationTriggered triggered,
    long userID,
    string AckMethod)
  {
    if (!MonnitSession.CustomerCan("Notification_Can_Acknowledge"))
      return (ActionResult) this.Content("Unauthorized");
    if (triggered == null)
      return (ActionResult) this.Content("Unauthorized");
    Notification notification = Notification.Load(triggered.NotificationID);
    if (notification == null || !MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      return (ActionResult) this.Content("Unauthorized");
    triggered.AcknowledgeMethod = AckMethod ?? "Unknown";
    triggered.AcknowledgedTime = DateTime.UtcNow;
    triggered.AcknowledgedBy = userID;
    triggered.Save();
    Account account = Account.Load(triggered.NotificationID);
    if (account != null)
      AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, triggered.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, triggered.JsonStringify(), account.AccountID, "Acknowledged rule");
    return (ActionResult) this.Content("Success");
  }

  [HttpPost]
  [AuthorizeDefault]
  public ActionResult AcknowledgeByNotificationTrigger(
    long notificationTriggeredID,
    long userID,
    string AckMethod)
  {
    return this.AcknowledgeByGeneric(NotificationTriggered.Load(notificationTriggeredID), userID, AckMethod);
  }

  [AuthorizeDefault]
  public ActionResult AckLandingRedirect() => (ActionResult) this.View();

  public ActionResult Ack(long id, Guid? guid)
  {
    bool enableDashboard = MonnitSession.CurrentTheme.EnableDashboard;
    NotificationRecorded model = NotificationRecorded.Load(id);
    int num;
    if (model != null)
    {
      Guid notificationGuid = model.NotificationGUID;
      Guid? nullable = guid;
      num = nullable.HasValue ? (notificationGuid != nullable.GetValueOrDefault() ? 1 : 0) : 1;
    }
    else
      num = 1;
    if (num != 0)
      return (ActionResult) this.Redirect("/Overview");
    Customer customer = MonnitSession.CurrentCustomer != null ? MonnitSession.CurrentCustomer : Customer.Load(model.CustomerID);
    Notification notification = Notification.Load(model.NotificationID);
    if (notification == null || customer == null || !customer.CanSeeAccount(notification.AccountID))
      return (ActionResult) this.Redirect("/Overview");
    // ISSUE: reference to a compiler-generated field
    if (NotificationController.\u003C\u003Eo__69.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NotificationController.\u003C\u003Eo__69.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Customer, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Customer", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = NotificationController.\u003C\u003Eo__69.\u003C\u003Ep__0.Target((CallSite) NotificationController.\u003C\u003Eo__69.\u003C\u003Ep__0, this.ViewBag, customer);
    return (ActionResult) this.View(nameof (Ack), (object) model);
  }

  [HttpPost]
  public ActionResult Ack(long id, Guid guid)
  {
    NotificationRecorded notificationRecorded = NotificationRecorded.Load(id);
    if (notificationRecorded == null || notificationRecorded.NotificationGUID != guid)
      return (ActionResult) this.Content("Failed");
    Customer customer = MonnitSession.CurrentCustomer != null ? MonnitSession.CurrentCustomer : Customer.Load(notificationRecorded.CustomerID);
    Notification notification = Notification.Load(notificationRecorded.NotificationID);
    if (notification == null || customer == null || !customer.CanSeeAccount(notification.AccountID))
      return (ActionResult) this.Content("Failed");
    Account account = customer.Account;
    if (account != null)
    {
      foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveByNotificationID(notification.NotificationID))
      {
        notificationTriggered.AcknowledgeMethod = "Email Link";
        notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
        notificationTriggered.AcknowledgedBy = customer.CustomerID;
        notificationTriggered.Save();
        if (account != null)
          AuditLog.LogAuditData(customer.CustomerID, notificationTriggered.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, notificationTriggered.JsonStringify(), account.AccountID, "Acknowledged rule");
      }
    }
    return (ActionResult) this.Content("Success");
  }

  public ActionResult AckTriggered(long id, Guid guid)
  {
    NotificationRecorded notificationRecorded = NotificationRecorded.Load(id);
    if (notificationRecorded == null || notificationRecorded.NotificationGUID != guid)
      return (ActionResult) this.Content("Failed");
    Customer customer = MonnitSession.CurrentCustomer != null ? MonnitSession.CurrentCustomer : Customer.Load(notificationRecorded.CustomerID);
    Notification notification = Notification.Load(notificationRecorded.NotificationID);
    if (notification == null || customer == null || !customer.CanSeeAccount(notification.AccountID))
      return (ActionResult) this.Content("Failed");
    Account account = customer.Account;
    if (account != null)
    {
      NotificationTriggered notificationTriggered = NotificationTriggered.LoadByNotificationRecordedID(notificationRecorded.NotificationRecordedID);
      notificationTriggered.AcknowledgeMethod = "Email Link";
      notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
      notificationTriggered.AcknowledgedBy = customer.CustomerID;
      notificationTriggered.Save();
      if (account != null)
        AuditLog.LogAuditData(customer.CustomerID, notificationTriggered.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, notificationTriggered.JsonStringify(), account.AccountID, "Acknowledged Rule");
    }
    return (ActionResult) this.Content("Success");
  }

  public ActionResult SendPushNotificationTest(
    string serverApiKey,
    string senderId,
    string deviceId,
    string message)
  {
    new Notification().SendPushNotification(serverApiKey, senderId, deviceId, message);
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public string PauseNotification(
    long id,
    string date,
    int hour,
    int minute,
    string ampm,
    string deviceType,
    string button)
  {
    try
    {
      eAuditObject Auditobj = eAuditObject.Sensor;
      BaseDBObject DBObject;
      long csNetId;
      if (deviceType == "Sensor")
      {
        DBObject = (BaseDBObject) Sensor.Load(id);
        csNetId = ((Sensor) DBObject).CSNetID;
      }
      else
      {
        DBObject = (BaseDBObject) Gateway.Load(id);
        csNetId = ((Gateway) DBObject).CSNetID;
        Auditobj = eAuditObject.Gateway;
      }
      CSNet csNet = CSNet.Load(csNetId);
      Account account = Account.Load(csNet.AccountID);
      if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
        return "Unauthorized";
      DateTime UTCTime = DateTime.UtcNow;
      switch (button)
      {
        case "Custom":
          string[] strArray = date.Split('/');
          int int32_1 = Convert.ToInt32(strArray[0]);
          int int32_2 = Convert.ToInt32(strArray[1]);
          int int32_3 = Convert.ToInt32(strArray[2]);
          if (ampm == "PM")
            hour += 12;
          UTCTime = Monnit.TimeZone.GetUTCFromLocalById(new DateTime(int32_3, int32_1, int32_2, hour, minute, 0), account.TimeZoneID);
          break;
        case "Hour":
          UTCTime = DateTime.UtcNow.AddHours(1.0);
          break;
        case "Manual":
          UTCTime = new DateTime(2099, 1, 1);
          break;
      }
      string changeRecord = $"{{\"DeviceID\" : \"{id}\", \"DeviceType\": \"{deviceType}\", \"ResumeNotificationDate\": \"{UTCTime}\" }} ";
      DBObject.LogAuditData(eAuditAction.Update, Auditobj, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Paused devices effect on notification");
      if (deviceType == "Sensor")
      {
        ((Sensor) DBObject).resumeNotificationDate = UTCTime;
        if (button.ToLower() != "unpause")
        {
          foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveBySensorID(id))
          {
            notificationTriggered.AcknowledgedBy = MonnitSession.CurrentCustomer.CustomerID;
            notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
            notificationTriggered.AcknowledgeMethod = "Browser_Pause";
            notificationTriggered.resetTime = DateTime.UtcNow;
            notificationTriggered.Save();
            AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, notificationTriggered.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, notificationTriggered.JsonStringify(), account.AccountID, "Acknowledged rule attached to sensor");
          }
        }
      }
      else
      {
        ((Gateway) DBObject).resumeNotificationDate = UTCTime;
        if (button.ToLower() != "unpause")
        {
          foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveByGatewayID(id))
          {
            notificationTriggered.AcknowledgedBy = MonnitSession.CurrentCustomer.CustomerID;
            notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
            notificationTriggered.AcknowledgeMethod = "Browser_Pause";
            notificationTriggered.resetTime = DateTime.UtcNow;
            notificationTriggered.Save();
            AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, notificationTriggered.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, notificationTriggered.JsonStringify(), account.AccountID, "Acknowledged rule attached to gateway");
          }
        }
      }
      DBObject.Save();
      if (UTCTime < DateTime.UtcNow)
        return "Unpause";
      return UTCTime == new DateTime(2099, 1, 1) ? "Paused" : Monnit.TimeZone.GetLocalTimeById(UTCTime, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToString();
    }
    catch
    {
      return "Error occurred.";
    }
  }

  [AuthorizeDefault]
  public ActionResult NotificationDisabled(long notificationID)
  {
    try
    {
      Account account = (Account) null;
      Notification notification = Notification.Load(notificationID);
      if (notification != null)
        account = Account.Load(notification.AccountID);
      if (account != null)
      {
        string record = $"{{\"NotifcationID\" : \"{notificationID}\", \"NotificationScheduleDisabled\": \"{(ValueType) true}\"}} ";
        AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, notificationID, eAuditAction.Delete, eAuditObject.Notification, record, account.AccountID, "Disabled notification by schedule");
      }
      NotificationScheduleDisabled.DeleteByNotificationID(notificationID);
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
    }
    return (ActionResult) this.Content("Failed!");
  }

  private System.Collections.Generic.List<int> DayList(string[] days)
  {
    System.Collections.Generic.List<int> source = new System.Collections.Generic.List<int>();
    for (int index = 0; index < days.Length; ++index)
    {
      if (source.Count > 0 && days[index].ToInt() - 1 != source.Last<int>())
        source.Add(-1);
      source.Add(days[index].ToInt());
      if (days.Length - 1 == index)
        source.Add(-1);
    }
    return source;
  }

  private void AddNotificationScheduleDisabled(long notificationID, System.Collections.Generic.List<int> monthList, int month)
  {
    int index1 = 0;
    bool flag1 = true;
    bool flag2 = false;
    NotificationScheduleDisabled scheduleDisabled = new NotificationScheduleDisabled();
    for (int index2 = 0; index2 < monthList.Count; ++index2)
    {
      if (monthList[index2] != -1)
        index1 = index2;
      else
        flag2 = true;
      if (flag1)
      {
        scheduleDisabled.NotificationID = notificationID;
        scheduleDisabled.StartMonth = month;
        scheduleDisabled.StartDay = monthList[index2];
        flag1 = false;
      }
      if (flag2)
      {
        scheduleDisabled.EndMonth = month;
        scheduleDisabled.EndDay = monthList[index1];
        scheduleDisabled.Save();
        scheduleDisabled = new NotificationScheduleDisabled();
        flag1 = true;
        flag2 = false;
      }
    }
  }

  [AuthorizeDefault]
  public ActionResult SetMobileDevice(
    string deviceId,
    string deviceName,
    string custId,
    string deviceType)
  {
    Customer customer = Customer.Load(custId.ToLong());
    System.Collections.Generic.List<CustomerMobileDevice> customerMobileDeviceList1 = new System.Collections.Generic.List<CustomerMobileDevice>();
    CustomerMobileDevice customerMobileDevice = new CustomerMobileDevice();
    System.Collections.Generic.List<CustomerMobileDevice> customerMobileDeviceList2 = CustomerMobileDevice.LoadByCustomerID(customer.CustomerID);
    if (customer == null)
      customer = MonnitSession.CurrentCustomer;
    if (customer.CustomerID != MonnitSession.CurrentCustomer.CustomerID && (!MonnitSession.CurrentCustomer.CanSeeAccount(customer.AccountID) || !MonnitSession.CustomerCan("Customer_Edit_Other")))
      return (ActionResult) this.Content("Failed");
    string userAgent = this.Request.UserAgent;
    if (string.IsNullOrEmpty(userAgent))
      return (ActionResult) this.Content("Failed");
    if (!userAgent.Contains("$"))
      return (ActionResult) this.Content("Failed");
    char ch = '$';
    string[] strArray = userAgent.Split(ch);
    string dName = strArray[1];
    string dId = strArray[2];
    bool flag = true;
    if (customerMobileDeviceList2.Count != 0)
    {
      foreach (CustomerMobileDevice cmd in customerMobileDeviceList2)
      {
        if (this.SameDevice(cmd, dId, dName))
        {
          flag = false;
          break;
        }
      }
    }
    if (customer.AllowPushNotification & flag)
    {
      customerMobileDevice.CustomerID = customer.CustomerID;
      customerMobileDevice.IsPrimary = customerMobileDeviceList2.Count == 0;
      customerMobileDevice.SendToDevice = false;
      customerMobileDevice.MobileDeviceType = deviceType;
      customerMobileDevice.MobileDeviceName = deviceName;
      customerMobileDevice.MobileDisplayName = deviceName;
      customerMobileDevice.MobileDeviceId = deviceId;
      customerMobileDevice.CreateDate = DateTime.UtcNow;
      customerMobileDevice.LastModifyDate = DateTime.UtcNow;
      customerMobileDevice.Save();
    }
    return (ActionResult) this.Content("Success");
  }

  private bool SameDevice(CustomerMobileDevice cmd, string dId, string dName)
  {
    return dId == cmd.MobileDeviceId & dName == cmd.MobileDeviceName;
  }

  public ActionResult RemoveMobileDevice(long cmdID)
  {
    CustomerMobileDevice.Load(cmdID).Delete();
    return (ActionResult) this.Content("success");
  }

  public ActionResult SavePreferredDevice(bool isPreferred, long cmdID)
  {
    CustomerMobileDevice customerMobileDevice1 = CustomerMobileDevice.Load(cmdID);
    Customer model = Customer.Load(customerMobileDevice1.CustomerID);
    Account.Load(model.AccountID);
    System.Collections.Generic.List<CustomerMobileDevice> customerMobileDeviceList = CustomerMobileDevice.LoadByCustomerID(model.CustomerID);
    if (isPreferred)
    {
      foreach (CustomerMobileDevice customerMobileDevice2 in customerMobileDeviceList)
      {
        customerMobileDevice2.IsPrimary = customerMobileDevice2.CustomerMobileDeviceID == cmdID;
        customerMobileDevice2.Save();
      }
    }
    else
    {
      customerMobileDevice1.IsPrimary = isPreferred;
      customerMobileDevice1.Save();
    }
    return (ActionResult) this.PartialView("/Views/Settings/_MobileDeviceList.ascx", (object) model);
  }

  public ActionResult SendToDevice(bool send, long cmdID)
  {
    CustomerMobileDevice customerMobileDevice1 = CustomerMobileDevice.Load(cmdID);
    Customer model = Customer.Load(customerMobileDevice1.CustomerID);
    Account account = Account.Load(model.AccountID);
    System.Collections.Generic.List<CustomerMobileDevice> customerMobileDeviceList = CustomerMobileDevice.LoadByCustomerID(model.CustomerID);
    if (!account.CurrentSubscription.Can(Feature.Find("use_push_notifications")) && send)
    {
      foreach (CustomerMobileDevice customerMobileDevice2 in customerMobileDeviceList)
      {
        customerMobileDevice2.SendToDevice = customerMobileDevice2.CustomerMobileDeviceID == cmdID;
        customerMobileDevice2.Save();
      }
    }
    else
    {
      customerMobileDevice1.SendToDevice = send;
      customerMobileDevice1.Save();
    }
    return (ActionResult) this.PartialView("/Views/Settings/_MobileDeviceList.ascx", (object) model);
  }

  public ActionResult SaveDisplayName(string name, long cmdID)
  {
    CustomerMobileDevice customerMobileDevice = CustomerMobileDevice.Load(cmdID);
    if (name == " " || name == "")
      name = customerMobileDevice.MobileDeviceName;
    customerMobileDevice.MobileDisplayName = name;
    customerMobileDevice.Save();
    return (ActionResult) this.Content("success");
  }

  public ActionResult CreateTemporaryValidation(long id, string type, string typeValue)
  {
    Validation validation1 = new Validation();
    foreach (Validation validation2 in Validation.LoadByCustomerID(id))
    {
      Validation validation3 = Validation.Load(validation2.ValidationID);
      if (validation3.Type == type)
        validation3.Delete();
    }
    Validation validation4 = new Validation();
    validation4.CustomerID = Customer.Load(id).CustomerID;
    validation4.Type = type;
    validation4.TypeValue = typeValue;
    validation4.Token = type.ToLower() == "email" ? this.GenerateValidationToken(10) : this.GenerateValidationToken(6);
    validation4.ExpirationDate = DateTime.UtcNow.AddHours(72.0);
    validation4.Save();
    if (type.ToLower() == "email")
      NotificationController.sendEmailVerification(id, validation4.Token);
    else if (type.ToLower() == "sms")
      NotificationController.sendSMSVerification(id, validation4.Token);
    else if (type.ToLower() == "voice")
    {
      try
      {
        NotificationController.sendVoiceVerification(id, validation4.Token);
      }
      catch
      {
        return (ActionResult) this.Content("Failed");
      }
    }
    return (ActionResult) this.Content("Success");
  }

  public ActionResult NotificationValidation(string token)
  {
    bool flag = false;
    try
    {
      Validation validation = Validation.LoadByTokenKey(token);
      if (validation.Type.ToLower() == "email" && validation.CustomerID == MonnitSession.CurrentCustomer.CustomerID && validation.Token == token.ToUpper() && validation.TypeValue == MonnitSession.CurrentCustomer.NotificationEmail)
        flag = true;
      if (validation.Type.ToLower() == "sms" && validation.CustomerID == MonnitSession.CurrentCustomer.CustomerID && validation.Token == token.ToUpper() && validation.TypeValue == MonnitSession.CurrentCustomer.NotificationPhone)
        flag = true;
      if (validation.Type.ToLower() == "voice" && validation.CustomerID == MonnitSession.CurrentCustomer.CustomerID && validation.Token == token.ToUpper() && validation.TypeValue == MonnitSession.CurrentCustomer.NotificationPhone)
        flag = true;
      if (!flag)
        return (ActionResult) this.Content("Failed");
      this.NotificationOptIn(validation.ValidationID);
      return validation.Type.ToLower() == "email" ? (ActionResult) this.RedirectPermanent("/Settings/UserNotification/" + validation.CustomerID.ToString()) : (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("InvalidCode");
    }
  }

  [AuthorizeDefault]
  public static void sendEmailVerification(long id, string token)
  {
    Customer customer = Customer.Load(id);
    Account account = Account.Load(customer.AccountID);
    EmailTemplate emailTemplate = EmailTemplate.LoadBest(account, eEmailTemplateFlag.Generic);
    AccountTheme accountTheme = account.getTheme();
    if (accountTheme == null)
    {
      accountTheme = new AccountTheme();
      accountTheme.Domain = "localhost:64526";
    }
    string str1 = $"http://{accountTheme.Domain}/Notification/NotificationValidation?token={token.ToUpper()}";
    PacketCache localCache = new PacketCache();
    NotificationRecorded notificationRecorded = new NotificationRecorded();
    notificationRecorded.NotificationType = eNotificationType.Email;
    notificationRecorded.CustomerID = customer.CustomerID;
    notificationRecorded.NotificationDate = DateTime.UtcNow;
    string str2 = "Notification Opt In Verification!";
    string newValue = $"By clicking on the following link, you are deciding to Opt In to notification. If you don't want to recieve notifications do not click on the link and it will expire within 72 hours.<br/><br/>{str1}";
    string str3 = emailTemplate.Template.Replace("{Content}", newValue);
    notificationRecorded.NotificationText = str3;
    notificationRecorded.NotificationSubject = str2;
    notificationRecorded.Save();
    Notification.SendNotification(notificationRecorded, localCache);
  }

  [AuthorizeDefault]
  public static void sendSMSVerification(long id, string token)
  {
    NotificationRecorded notificationRecorded = new NotificationRecorded();
    PacketCache packetCache = new PacketCache();
    Customer cust = Customer.Load(id);
    Account account = Account.Load(cust.AccountID);
    notificationRecorded.NotificationType = eNotificationType.SMS;
    notificationRecorded.CustomerID = cust.CustomerID;
    notificationRecorded.NotificationDate = DateTime.UtcNow;
    notificationRecorded.NotificationText = $"SMS Notification Verification code: {token.ToUpper()}";
    notificationRecorded.NotificationSubject = "SMS Notification Opt In Verification";
    if (string.IsNullOrEmpty(cust.NotificationPhone))
      return;
    if (cust.DirectSMS && !string.IsNullOrEmpty(ConfigData.AppSettings("TwilioAccountSid")))
    {
      Country byIsoCodeOrNumber = Country.FindByISOCodeOrNumber(cust.NotificationPhoneISOCode, cust.NotificationPhone);
      MonnitUtil.SendTwilioSMS(MonnitUtil.GetFromNumber(account, byIsoCodeOrNumber), cust.NotificationPhone.Replace(" ", "").Replace("-", ""), notificationRecorded.NotificationText);
    }
    else
    {
      string emailAddress = string.Empty;
      SMSCarrier smsCarrier = SMSCarrier.Load(cust.SMSCarrierID);
      if (smsCarrier != null)
      {
        string str = smsCarrier.SMSAddress(cust.NotificationPhone);
        if (str != string.Empty)
          emailAddress = str;
      }
      if (!NotificationController.sendSMTPNotification(cust, emailAddress, false, notificationRecorded))
        ;
    }
  }

  [AuthorizeDefault]
  public static void sendVoiceVerification(long id, string token)
  {
    Customer customer = Customer.Load(id);
    Account account = Account.Load(customer.AccountID);
    Validation v = Validation.LoadByTokenKey(token.ToUpper());
    if (string.IsNullOrEmpty(customer.NotificationPhone2))
      return;
    string fromNumberForVoice = MonnitUtil.GetFromNumberForVoice(account);
    string notificationPhone2 = customer.NotificationPhone2;
    MonnitUtil.SendTwilioPhoneVerification(v, account, customer.CustomerID, fromNumberForVoice, notificationPhone2);
  }

  private static bool sendSMTPNotification(
    Customer cust,
    string emailAddress,
    bool isHtml,
    NotificationRecorded notificationRecorded)
  {
    try
    {
      using (MailMessage mail = new MailMessage())
      {
        using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, cust.Account))
        {
          mail.To.SafeAdd(emailAddress, $"{cust.FirstName} {cust.LastName}");
          mail.Subject = WebUtility.HtmlDecode(notificationRecorded.NotificationSubject);
          mail.Body = WebUtility.HtmlDecode(notificationRecorded.NotificationText);
          mail.IsBodyHtml = isHtml;
          string[] strArray = new string[5]
          {
            "{\"metadata\":{ \"NotificationRecordedID\": \"",
            null,
            null,
            null,
            null
          };
          long num = notificationRecorded.NotificationRecordedID;
          strArray[1] = num.ToString();
          strArray[2] = "\", \"CustomerID\": \"";
          num = notificationRecorded.CustomerID;
          strArray[3] = num.ToString();
          strArray[4] = "\"}}";
          string str = string.Concat(strArray);
          mail.Headers.Add("X-MSYS-API", str);
          mail.Headers.Add("X-SES-CONFIGURATION-SET", "Webhook");
          if (string.IsNullOrWhiteSpace(emailAddress) || mail.To.Count != 0)
            return MonnitUtil.SendMail(mail, smtpClient);
          notificationRecorded.Status = "Opted Out";
          notificationRecorded.DoRetry = false;
          return false;
        }
      }
    }
    catch (Exception ex)
    {
      notificationRecorded.Status = ex.Message;
      return false;
    }
  }

  [AuthorizeDefault]
  public ActionResult NotificationOptIn(long id)
  {
    try
    {
      Customer customer = Customer.Load(id);
      customer.NotificationOptIn = DateTime.UtcNow;
      customer.NotificationOptOut = DateTime.MinValue;
      customer.SendSensorNotificationToText = true;
      customer.SendSensorNotificationToVoice = true;
      customer.Save();
      if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
        MonnitSession.CurrentCustomer = customer;
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult NotificationOptOut(long id)
  {
    try
    {
      Customer customer = Customer.Load(id);
      Account account = Account.Load(customer.AccountID);
      System.Collections.Generic.List<Validation> validationList = Validation.LoadByCustomerID(id);
      System.Collections.Generic.List<NotificationRecipient> notifyId = NotificationRecipient.LoadByCustomerToNotifyID(id);
      customer.SendSensorNotificationToText = false;
      customer.SendSensorNotificationToVoice = false;
      customer.AllowPushNotification = false;
      customer.NotificationOptOut = DateTime.UtcNow;
      foreach (NotificationRecipient DBObject in notifyId)
      {
        string changeRecord = $"{{\"CustomerID\": \"{DBObject.CustomerToNotifyID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
        DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Removed recipient from notification");
        DBObject.Delete();
      }
      customer.Save();
      if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
        MonnitSession.CurrentCustomer = customer;
      foreach (BaseDBObject baseDbObject in validationList)
        baseDbObject.Delete();
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public string GenerateValidationToken(int length)
  {
    string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    char[] chArray = new char[length];
    Random random = new Random();
    for (int index = 0; index < chArray.Length; ++index)
      chArray[index] = str[random.Next(str.Length)];
    return new string(chArray);
  }

  [AuthorizeDefault]
  public ActionResult NotificationNote(long id)
  {
    if (!MonnitSession.CustomerCan("Notification_Can_Acknowledge"))
      return (ActionResult) this.Content("Unauthorized");
    NotificationTriggered model = NotificationTriggered.Load(id);
    if (model != null)
    {
      Notification notification = Notification.Load(model.NotificationID);
      if (notification != null && MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      {
        // ISSUE: reference to a compiler-generated field
        if (NotificationController.\u003C\u003Eo__92.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NotificationController.\u003C\u003Eo__92.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Notification, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Notification", typeof (NotificationController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = NotificationController.\u003C\u003Eo__92.\u003C\u003Ep__0.Target((CallSite) NotificationController.\u003C\u003Eo__92.\u003C\u003Ep__0, this.ViewBag, notification);
        return (ActionResult) this.PartialView(nameof (NotificationNote), (object) model);
      }
    }
    return (ActionResult) this.Content("Inavlid NotificationTriggeredID");
  }

  [AuthorizeDefault]
  public ActionResult MessageNoteList(long id)
  {
    if (!MonnitSession.CustomerCan("Notification_Can_Acknowledge"))
      return (ActionResult) this.Content("Not Authorized");
    System.Collections.Generic.List<Monnit.NotificationNote> model = Monnit.NotificationNote.LoadByNotificationTriggeredID(id);
    return model == null ? (ActionResult) this.Content("Not found") : (ActionResult) this.PartialView(nameof (MessageNoteList), (object) model);
  }

  [AuthorizeDefault]
  public ActionResult NotificationNoteList(long id)
  {
    if (!MonnitSession.CustomerCan("Notification_Can_Acknowledge"))
      return (ActionResult) this.Content("Not Authorized");
    System.Collections.Generic.List<Monnit.NotificationNote> model = Monnit.NotificationNote.LoadByNotificationTriggeredID(id);
    return model == null ? (ActionResult) this.Content("Not found") : (ActionResult) this.PartialView(nameof (NotificationNoteList), (object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult AddMessageNote(long id, FormCollection collection)
  {
    if (!MonnitSession.CustomerCan("Notification_Can_Acknowledge"))
      return (ActionResult) this.Content("Unauthorized");
    if (string.IsNullOrEmpty(collection["note"].ToString()))
      return (ActionResult) this.Content("No Note!");
    string str = collection["note"].ToString();
    if (collection["note"].HasScriptTag())
      return (ActionResult) this.Content("Failed: Notes may not contain script tags.");
    NotificationTriggered notificationTriggered = NotificationTriggered.Load(id);
    if (notificationTriggered != null)
    {
      Notification notification = Notification.Load(notificationTriggered.NotificationID);
      if (notification != null && MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      {
        Monnit.NotificationNote DBObject = new Monnit.NotificationNote();
        DBObject.Note = str;
        if (string.IsNullOrWhiteSpace(str))
          return (ActionResult) this.Content("Success");
        DBObject.NotificationTriggeredID = id;
        DBObject.CustomerID = MonnitSession.CurrentCustomer.CustomerID;
        DBObject.CreateDate = DateTime.UtcNow;
        DBObject.Save();
        if (!notificationTriggered.HasNote)
        {
          notificationTriggered.HasNote = true;
          notificationTriggered.Save();
        }
        DBObject.LogAuditData(eAuditAction.Create, eAuditObject.NotificationNote, MonnitSession.CurrentCustomer.CustomerID, notification.AccountID, "Created a Notification Note");
        return (ActionResult) this.Content("Success");
      }
    }
    return (ActionResult) this.Content("Unable to Save Note!");
  }

  [AuthorizeDefault]
  public ActionResult DeleteNotificationNote(long id)
  {
    if (MonnitSession.CustomerCan("Notification_Can_Acknowledge"))
    {
      try
      {
        Monnit.NotificationNote DBObject = Monnit.NotificationNote.Load(id);
        if (DBObject != null)
        {
          NotificationTriggered notificationTriggered = NotificationTriggered.Load(DBObject.NotificationTriggeredID);
          if (notificationTriggered == null)
            return (ActionResult) this.Content("Failed");
          Notification notification = Notification.Load(notificationTriggered.NotificationID);
          if (notification == null)
            return (ActionResult) this.Content("Failed");
          if (!MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
            return (ActionResult) this.Content("Unauthorized");
          DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.NotificationTriggered, MonnitSession.CurrentCustomer.CustomerID, notification.AccountID, "Deleted Notification Note");
          DBObject.Delete();
          if (Monnit.NotificationNote.LoadByNotificationTriggeredID(DBObject.NotificationTriggeredID).Count == 0 && notificationTriggered != null)
          {
            notificationTriggered.HasNote = false;
            notificationTriggered.Save();
          }
          return (ActionResult) this.Content("Success");
        }
      }
      catch (Exception ex)
      {
        ex.Log($"NotificationNote.Delete ID: {id.ToString()} unable to delete note  ");
      }
    }
    return (ActionResult) this.Content("Failed");
  }

  [AuthorizeDefault]
  public string PauseDeviceNotification(long id, string deviceType, string button)
  {
    try
    {
      eAuditObject Auditobj = eAuditObject.Sensor;
      BaseDBObject DBObject;
      long csNetId;
      if (deviceType == "Sensor")
      {
        DBObject = (BaseDBObject) Sensor.Load(id);
        csNetId = ((Sensor) DBObject).CSNetID;
      }
      else
      {
        DBObject = (BaseDBObject) Gateway.Load(id);
        csNetId = ((Gateway) DBObject).CSNetID;
        Auditobj = eAuditObject.Gateway;
      }
      CSNet csNet = CSNet.Load(csNetId);
      Account account = Account.Load(csNet.AccountID);
      if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
        return "Unauthorized";
      DateTime UTCTime = DateTime.UtcNow;
      switch (button)
      {
        case "14Day":
          UTCTime = DateTime.UtcNow.AddDays(14.0);
          break;
        case "2Day":
          UTCTime = DateTime.UtcNow.AddDays(2.0);
          break;
        case "2Hour":
          UTCTime = DateTime.UtcNow.AddHours(2.0);
          break;
        case "3Day":
          UTCTime = DateTime.UtcNow.AddDays(3.0);
          break;
        case "3Hour":
          UTCTime = DateTime.UtcNow.AddHours(3.0);
          break;
        case "4Hour":
          UTCTime = DateTime.UtcNow.AddHours(4.0);
          break;
        case "7Day":
          UTCTime = DateTime.UtcNow.AddDays(7.0);
          break;
        case "Day":
          UTCTime = DateTime.UtcNow.AddDays(1.0);
          break;
        case "Hour":
          UTCTime = DateTime.UtcNow.AddHours(1.0);
          break;
        case "Manual":
          UTCTime = new DateTime(2099, 1, 1);
          break;
        case "Month":
          UTCTime = DateTime.UtcNow.AddDays(30.0);
          break;
      }
      string changeRecord = $"{{\"DeviceID\" : \"{id}\", \"DeviceType\": \"{deviceType}\", \"ResumeNotificationDate\": \"{UTCTime}\" }} ";
      DBObject.LogAuditData(eAuditAction.Update, Auditobj, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Paused sensors effect on  notification");
      if (deviceType == "Sensor")
      {
        ((Sensor) DBObject).resumeNotificationDate = UTCTime;
        if (button.ToLower() != "unpause" && button.ToLower() != "resume")
        {
          foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveBySensorID(id))
          {
            notificationTriggered.AcknowledgedBy = MonnitSession.CurrentCustomer.CustomerID;
            notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
            notificationTriggered.AcknowledgeMethod = "Mobile_Pause";
            notificationTriggered.resetTime = DateTime.UtcNow;
            notificationTriggered.Save();
          }
        }
      }
      else
      {
        ((Gateway) DBObject).resumeNotificationDate = UTCTime;
        if (button.ToLower() != "unpause" && button.ToLower() != "resume")
        {
          foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveByGatewayID(id))
          {
            notificationTriggered.AcknowledgedBy = MonnitSession.CurrentCustomer.CustomerID;
            notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
            notificationTriggered.AcknowledgeMethod = "Mobile_Pause";
            notificationTriggered.resetTime = DateTime.UtcNow;
            notificationTriggered.Save();
          }
        }
      }
      DBObject.Save();
      if (UTCTime < DateTime.UtcNow)
        return "Unpaused";
      return UTCTime == new DateTime(2099, 1, 1) ? "Paused" : Monnit.TimeZone.GetLocalTimeById(UTCTime, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToString();
    }
    catch
    {
      return "Error occurred.";
    }
  }

  [AuthorizeDefault]
  public ActionResult AutomatedSchedule(long id)
  {
    Notification notification = Notification.Load(id);
    AutomatedNotification model = AutomatedNotification.LoadByExternalIDAndNotificationID(notification.AdvancedNotificationID, notification.NotificationID);
    if (model == null)
    {
      model = new AutomatedNotification();
      model.ExternalID = notification.AdvancedNotificationID;
      model.NotificationID = notification.NotificationID;
      model.Description = notification.Name;
      model.Save();
    }
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult AutomatedSchedule(FormCollection collection)
  {
    AutomatedNotification automatedNotification = collection["AutomatedNotificationID"].ToLong() != long.MinValue ? AutomatedNotification.Load(collection["AutomatedNotificationID"].ToLong()) : new AutomatedNotification();
    try
    {
      automatedNotification.Description = !string.IsNullOrWhiteSpace(collection["Description"]) ? collection["Description"] : Notification.Load(collection["id"].ToLong()).Name;
      Notification notification = Notification.Load(automatedNotification.NotificationID);
      Account account = (Account) null;
      if (notification != null)
        account = Account.Load(notification.AccountID);
      if (account != null)
      {
        string record = $"{{ \"NotifcationID\" : \"{automatedNotification.NotificationID}\",\"ProcessFrequency\": \"{collection["proccessFrequency"].ToStringSafe()}\" }} ";
        AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, automatedNotification.NotificationID, eAuditAction.Create, eAuditObject.Notification, record, account.AccountID, "Edited process frequency on advanced notification");
      }
      automatedNotification.NotificationID = collection["id"].ToLong();
      automatedNotification.ExternalID = collection["ExternalID"].ToLong();
      automatedNotification.ProcessFrequency = collection["proccessFrequency"].ToInt();
      automatedNotification.Save();
      return (ActionResult) this.Content("Successfully saved schedule.");
    }
    catch (Exception ex)
    {
    }
    return (ActionResult) this.Content("Failed to save schedule.");
  }

  [AuthorizeDefault]
  public ActionResult TopBarNotiList()
  {
    return (ActionResult) this.PartialView("TopBarEventList", (object) NotificationTriggered.LoadActiveByAccountID(MonnitSession.CurrentCustomer.AccountID));
  }

  [AuthorizeDefault]
  public static Dictionary<int, string> GetNotificationClassFilter()
  {
    Dictionary<int, string> dictionary = System.Enum.GetValues(typeof (eNotificationClass)).Cast<eNotificationClass>().ToDictionary<eNotificationClass, int, string>((Func<eNotificationClass, int>) (x => x.ToInt()), (Func<eNotificationClass, string>) (x => x.ToString()));
    if (dictionary[1] != null)
      dictionary[1] = "Sensor Reading";
    if (dictionary[2] != null)
      dictionary[2] = "Device Inactivity";
    if (dictionary[3] != null)
      dictionary[3] = "Battery Level";
    if (dictionary[6] != null)
      dictionary.Remove(6);
    if (dictionary[7] != null)
      dictionary[7] = "Scheduled";
    if (dictionary[8] != null)
      dictionary.Remove(8);
    return dictionary;
  }
}
