// Decompiled with JetBrains decompiler
// Type: Monnit.GatewayNotification
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("GatewayNotification")]
public class GatewayNotification : BaseDBObject
{
  private long _GatewayNotificationID = long.MinValue;
  private long _GatewayID = long.MinValue;
  private long _NotificationID = long.MinValue;
  private bool _AutoReset = true;
  private DateTime _TriggerDate = DateTime.MinValue;

  [DBProp("GatewayNotificationID", IsPrimaryKey = true)]
  public long GatewayNotificationID
  {
    get => this._GatewayNotificationID;
    set => this._GatewayNotificationID = value;
  }

  [DBForeignKey("Gateway", "GatewayID")]
  [DBProp("GatewayID", AllowNull = false)]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBForeignKey("Notification", "NotificationID")]
  [DBProp("NotificationID", AllowNull = false)]
  public long NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [DBProp("AutoReset", AllowNull = true)]
  public bool AutoReset
  {
    get => this._AutoReset;
    set => this._AutoReset = value;
  }

  [DBProp("TriggerDate", AllowNull = true)]
  public DateTime TriggerDate
  {
    get => this._TriggerDate;
    set => this._TriggerDate = value;
  }

  public static List<Gateway> LoadByNotificationID(long notificationID)
  {
    return new Monnit.Data.GatewayNotification.LoadByNotificationID(notificationID).Result;
  }

  public static void AddGateway(long notificationID, long gatewayID)
  {
    Monnit.Data.GatewayNotification.AddGateway addGateway = new Monnit.Data.GatewayNotification.AddGateway(notificationID, gatewayID);
  }

  public static void RemoveGateway(long notificationID, long gatewayID)
  {
    Monnit.Data.GatewayNotification.RemoveGateway removeGateway = new Monnit.Data.GatewayNotification.RemoveGateway(notificationID, gatewayID);
  }

  public static List<Notification> LoadByGatewayID(long gatewayID)
  {
    return new Monnit.Data.GatewayNotification.LoadByGatewayID(gatewayID).Result;
  }

  public static GatewayNotification LoadByGatewayIDAndNotificationID(
    long gatewayID,
    long notificationID)
  {
    return BaseDBObject.LoadByForeignKeys<GatewayNotification>(new string[2]
    {
      "NotificationID",
      "GatewayID"
    }, new object[2]
    {
      (object) notificationID,
      (object) gatewayID
    }).FirstOrDefault<GatewayNotification>();
  }

  public static GatewayNotification Load(long ID) => BaseDBObject.Load<GatewayNotification>(ID);
}
