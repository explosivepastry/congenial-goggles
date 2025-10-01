// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.AvailableNotificationByGatewayModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace iMonnit.Models;

public class AvailableNotificationByGatewayModel
{
  public Notification Notification { get; set; }

  public long GatewayNotificationID { get; set; }

  public static List<AvailableNotificationByGatewayModel> Load(Gateway gtwy)
  {
    if (gtwy == null)
      return (List<AvailableNotificationByGatewayModel>) null;
    List<AvailableNotificationByGatewayModel> source = new List<AvailableNotificationByGatewayModel>();
    CSNet csNet = CSNet.Load(gtwy.CSNetID);
    if (csNet == null)
      return (List<AvailableNotificationByGatewayModel>) null;
    Account account = Account.Load(csNet.AccountID);
    if (account == null)
      return (List<AvailableNotificationByGatewayModel>) null;
    foreach (Notification notification in Notification.LoadByAccountID(account.AccountID))
    {
      switch (notification.NotificationClass)
      {
        case eNotificationClass.Inactivity:
          GatewayNotification gatewayNotification1 = GatewayNotification.LoadByGatewayIDAndNotificationID(gtwy.GatewayID, notification.NotificationID);
          source.Add(new AvailableNotificationByGatewayModel()
          {
            Notification = notification,
            GatewayNotificationID = gatewayNotification1 != null ? gatewayNotification1.GatewayNotificationID : long.MinValue
          });
          break;
        case eNotificationClass.Low_Battery:
          if (gtwy.PowerSource.VoltageForZeroPercent != gtwy.PowerSource.VoltageForOneHundredPercent)
          {
            GatewayNotification gatewayNotification2 = GatewayNotification.LoadByGatewayIDAndNotificationID(gtwy.GatewayID, notification.NotificationID);
            source.Add(new AvailableNotificationByGatewayModel()
            {
              Notification = notification,
              GatewayNotificationID = gatewayNotification2 != null ? gatewayNotification2.GatewayNotificationID : long.MinValue
            });
            break;
          }
          break;
        case eNotificationClass.Advanced:
          AdvancedNotification advancedNotification = AdvancedNotification.Load(notification.AdvancedNotificationID);
          AvailableNotificationByGatewayModel notificationByGatewayModel = new AvailableNotificationByGatewayModel();
          if (advancedNotification.CanAdd((object) gtwy))
          {
            GatewayNotification gatewayNotification3 = GatewayNotification.LoadByGatewayIDAndNotificationID(gtwy.GatewayID, notification.NotificationID);
            notificationByGatewayModel.Notification = notification;
            notificationByGatewayModel.GatewayNotificationID = gatewayNotification3 != null ? gatewayNotification3.GatewayNotificationID : long.MinValue;
            source.Add(notificationByGatewayModel);
            break;
          }
          break;
        case eNotificationClass.Timed:
          GatewayNotification gatewayNotification4 = GatewayNotification.LoadByGatewayIDAndNotificationID(gtwy.GatewayID, notification.NotificationID);
          source.Add(new AvailableNotificationByGatewayModel()
          {
            Notification = notification,
            GatewayNotificationID = gatewayNotification4 != null ? gatewayNotification4.GatewayNotificationID : long.MinValue
          });
          break;
      }
    }
    return source.OrderByDescending<AvailableNotificationByGatewayModel, bool>((Func<AvailableNotificationByGatewayModel, bool>) (m1 => m1.GatewayNotificationID > 0L)).ThenBy<AvailableNotificationByGatewayModel, string>((Func<AvailableNotificationByGatewayModel, string>) (m2 => m2.Notification.Name)).ToList<AvailableNotificationByGatewayModel>();
  }
}
