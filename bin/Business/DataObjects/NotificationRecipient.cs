// Decompiled with JetBrains decompiler
// Type: Monnit.NotificationRecipient
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit;

[DBClass("NotificationRecipient")]
public class NotificationRecipient : BaseDBObject
{
  private long _NotificationRecipientID = long.MinValue;
  private long _NotificationID = long.MinValue;
  private eNotificationType _NotificationType;
  private long _CustomerToNotifyID = long.MinValue;
  private int _DelayMinutes = int.MinValue;
  private DateTime _LastSentTime = DateTime.MinValue;
  private long _CustomerGroupID = long.MinValue;
  private Customer _CustomerToNotify;
  private long _DeviceToNotifyID = long.MinValue;
  private Sensor _DeviceToNotify;
  private string _SerializedRecipientProperties = string.Empty;
  private long _ActionToExecuteID = long.MinValue;

  [DBProp("NotificationRecipientID", IsPrimaryKey = true)]
  public long NotificationRecipientID
  {
    get => this._NotificationRecipientID;
    set => this._NotificationRecipientID = value;
  }

  [DBForeignKey("Notification", "NotificationID")]
  [DBProp("NotificationID", AllowNull = false)]
  public long NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [DBProp("DelayMinutes", AllowNull = true)]
  public int DelayMinutes
  {
    get => this._DelayMinutes;
    set => this._DelayMinutes = value;
  }

  [DBProp("LastSentTime")]
  public DateTime LastSentTime
  {
    get => this._LastSentTime;
    set => this._LastSentTime = value;
  }

  [DBProp("eNotificationType", AllowNull = false)]
  public eNotificationType NotificationType
  {
    get => this._NotificationType;
    set => this._NotificationType = value;
  }

  [DBProp("CustomerGroupID")]
  [DBForeignKey("CustomerGroup", "CustomerGroupID")]
  public long CustomerGroupID
  {
    get => this._CustomerGroupID;
    set => this._CustomerGroupID = value;
  }

  [DBForeignKey("Customer", "CustomerID")]
  [DBProp("CustomerToNotifyID", AllowNull = true)]
  public long CustomerToNotifyID
  {
    get => this._CustomerToNotifyID;
    set => this._CustomerToNotifyID = value;
  }

  public Customer CustomerToNotify
  {
    get
    {
      if (this._CustomerToNotify == null || this._CustomerToNotify.CustomerID != this.CustomerToNotifyID)
        this._CustomerToNotify = Customer.Load(this.CustomerToNotifyID);
      return this._CustomerToNotify;
    }
    set => this._CustomerToNotify = value;
  }

  [DBForeignKey("Sensor", "SensorID")]
  [DBProp("DeviceToNotifyID", AllowNull = true)]
  public long DeviceToNotifyID
  {
    get => this._DeviceToNotifyID;
    set => this._DeviceToNotifyID = value;
  }

  public Sensor DeviceToNotify
  {
    get
    {
      if (this._DeviceToNotify == null)
        this._DeviceToNotify = Sensor.Load(this.DeviceToNotifyID);
      return this._DeviceToNotify;
    }
    set => this._DeviceToNotify = value;
  }

  [DBProp("SerializedRecipientProperties", MaxLength = 2000, AllowNull = true)]
  public string SerializedRecipientProperties
  {
    get => this._SerializedRecipientProperties;
    set => this._SerializedRecipientProperties = value;
  }

  [DBProp("ActionToExecuteID")]
  [DBForeignKey("ActionToExecute", "ActionToExecuteID")]
  public long ActionToExecuteID
  {
    get => this._ActionToExecuteID;
    set => this._ActionToExecuteID = value;
  }

  public static NotificationRecipient Load(long ID) => BaseDBObject.Load<NotificationRecipient>(ID);

  public static List<NotificationRecipient> LoadByNotificationID(long notificationID)
  {
    return new Monnit.Data.NotificationRecipient.LoadByNotificationID(notificationID).Result;
  }

  public static List<NotificationRecipient> LoadByCustomerToNotifyID(long CustomerToNotifyID)
  {
    return BaseDBObject.LoadByForeignKey<NotificationRecipient>(nameof (CustomerToNotifyID), (object) CustomerToNotifyID);
  }

  internal static NotificationRecipient AddCustomer(
    Notification notification,
    Customer customer,
    eNotificationType notificationType)
  {
    NotificationRecipient notificationRecipient = NotificationRecipient.AddCustomer(notification.NotificationID, customer.CustomerID, notificationType);
    notificationRecipient.CustomerToNotify = customer;
    return notificationRecipient;
  }

  internal static NotificationRecipient AddCustomer(
    long notificationID,
    long customerID,
    eNotificationType notificationType)
  {
    NotificationRecipient notificationRecipient = new NotificationRecipient();
    notificationRecipient.NotificationID = notificationID;
    notificationRecipient.CustomerToNotifyID = customerID;
    notificationRecipient.NotificationType = notificationType;
    notificationRecipient.Save();
    return notificationRecipient;
  }

  internal static void RemoveCustomer(Notification notification, Customer customer)
  {
    Monnit.Data.NotificationRecipient.RemoveCustomer removeCustomer = new Monnit.Data.NotificationRecipient.RemoveCustomer(notification.NotificationID, customer.CustomerID);
  }

  internal static NotificationRecipient AddDevice(
    long notificationID,
    long deviceID,
    eNotificationType notificationType,
    string defaultSerializedRecipientProperties)
  {
    NotificationRecipient notificationRecipient = new NotificationRecipient();
    notificationRecipient.NotificationID = notificationID;
    notificationRecipient.DeviceToNotifyID = deviceID;
    notificationRecipient.NotificationType = notificationType;
    notificationRecipient.SerializedRecipientProperties = defaultSerializedRecipientProperties;
    notificationRecipient.Save();
    return notificationRecipient;
  }

  public static List<NotificationRecipient> LoadVisibleByNotificationID(
    long customerID,
    long notificationID)
  {
    return new Monnit.Data.NotificationRecipient.LoadVisibleByNotificationID(customerID, notificationID).Result;
  }

  public static DataTable LoadActionToExecute(long notificationID)
  {
    return Monnit.Data.NotificationRecipient.LoadActionToExecute.Exec(notificationID);
  }

  public static List<NotificationRecipient> NotificationWebHookRecipient_LoadByAccountID(
    long accountID)
  {
    return new Monnit.Data.NotificationRecipient.NotificationWebHookRecipient_LoadByAccountID(accountID).Result;
  }
}
