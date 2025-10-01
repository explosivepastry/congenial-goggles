// Decompiled with JetBrains decompiler
// Type: Monnit.NotificationRecorded
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit;

[DBClass("NotificationRecorded")]
public class NotificationRecorded : BaseDBObject
{
  private long _NotificationRecordedID = long.MinValue;
  private Guid _NotificationGUID = Guid.NewGuid();
  private long _NotificationID = long.MinValue;
  private long _SensorID = long.MinValue;
  private long _GatewayID = long.MinValue;
  private eNotificationType _NotificationType;
  private long _CustomerID = long.MinValue;
  private string _Reading = string.Empty;
  private string _NotificationSubject = string.Empty;
  private string _NotificationText = string.Empty;
  private string _NotificationContent = string.Empty;
  private DateTime _NotificationDate = DateTime.MinValue;
  private long _SentToDeviceID = long.MinValue;
  private int _QueueID = int.MinValue;
  private string _SerializedRecipientProperties = string.Empty;
  private bool _Delivered = false;
  private DateTime _AcknowledgedDate = DateTime.MinValue;
  private string _Status = string.Empty;
  private long _NotificationCreditID = long.MinValue;
  private int _NotificaitonCreditCount = 0;
  private string _NotifyingOn = string.Empty;
  private string _SentTo = string.Empty;
  private int _RetryCount = 0;
  private bool _DoRetry = false;
  private long _NotificationTriggeredID = long.MinValue;

  [DBProp("NotificationRecordedID", IsPrimaryKey = true)]
  public long NotificationRecordedID
  {
    get => this._NotificationRecordedID;
    set => this._NotificationRecordedID = value;
  }

  [DBProp("NotificationGUID")]
  public Guid NotificationGUID
  {
    get => this._NotificationGUID;
    set => this._NotificationGUID = value;
  }

  [DBForeignKey("Notification", "NotificationID")]
  [DBProp("NotificationID", AllowNull = true)]
  public long NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [DBForeignKey("Sensor", "SensorID")]
  [DBProp("SensorID", AllowNull = true)]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("GatewayID")]
  [DBForeignKey("Gateway", "GatewayID")]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("eNotificationType", AllowNull = false)]
  public eNotificationType NotificationType
  {
    get => this._NotificationType;
    set => this._NotificationType = value;
  }

  [DBForeignKey("Customer", "CustomerID")]
  [DBProp("CustomerID", AllowNull = true)]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("Reading", MaxLength = 200, International = true)]
  public string Reading
  {
    get => this._Reading;
    set
    {
      if (value == null)
        this._Reading = string.Empty;
      else
        this._Reading = value;
    }
  }

  [DBProp("NotificationSubject", MaxLength = 255 /*0xFF*/, AllowNull = true, International = true)]
  public string NotificationSubject
  {
    get => this._NotificationSubject;
    set => this._NotificationSubject = value;
  }

  [DBProp("NotificationText", MaxLength = 2147483647 /*0x7FFFFFFF*/, International = true)]
  public string NotificationText
  {
    get => this._NotificationText;
    set
    {
      if (value == null)
        this._NotificationText = string.Empty;
      else
        this._NotificationText = value;
    }
  }

  [DBProp("NotificationContent", MaxLength = 4000, AllowNull = true, International = true)]
  public string NotificationContent
  {
    get => this._NotificationContent;
    set => this._NotificationContent = value;
  }

  [DBProp("NotificationDate", AllowNull = false)]
  public DateTime NotificationDate
  {
    get => this._NotificationDate;
    set => this._NotificationDate = value;
  }

  [DBForeignKey("Sensor", "SensorID")]
  [DBProp("SentToDeviceID", AllowNull = true)]
  public long SentToDeviceID
  {
    get => this._SentToDeviceID;
    set => this._SentToDeviceID = value;
  }

  [DBProp("QueueID", AllowNull = true)]
  public int QueueID
  {
    get => this._QueueID;
    set => this._QueueID = value;
  }

  [DBProp("SerializedRecipientProperties", MaxLength = 2000, AllowNull = true)]
  public string SerializedRecipientProperties
  {
    get => this._SerializedRecipientProperties;
    set => this._SerializedRecipientProperties = value;
  }

  [DBProp("Delivered", AllowNull = true)]
  public bool Delivered
  {
    get => this._Delivered;
    set => this._Delivered = value;
  }

  [DBProp("AcknowledgedDate", AllowNull = true)]
  public DateTime AcknowledgedDate
  {
    get => this._AcknowledgedDate;
    set => this._AcknowledgedDate = value;
  }

  [DBProp("Status", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [DBProp("NotificationCreditID")]
  [DBForeignKey("NotificationCredit", "NotificationCreditID")]
  public long NotificationCreditID
  {
    get => this._NotificationCreditID;
    set => this._NotificationCreditID = value;
  }

  [DBProp("NotificaitonCreditCount")]
  public int NotificaitonCreditCount
  {
    get => this._NotificaitonCreditCount;
    set => this._NotificaitonCreditCount = value;
  }

  [DBProp("NotifyingOn", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string NotifyingOn
  {
    get => this._NotifyingOn;
    set => this._NotifyingOn = value;
  }

  [DBProp("SentTo", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string SentTo
  {
    get => this._SentTo;
    set => this._SentTo = value;
  }

  [DBProp("RetryCount", DefaultValue = 0)]
  public int RetryCount
  {
    get => this._RetryCount;
    set => this._RetryCount = value;
  }

  [DBProp("DoRetry", DefaultValue = false)]
  public bool DoRetry
  {
    get => this._DoRetry;
    set => this._DoRetry = value;
  }

  [DBProp("NotificationTriggeredID")]
  public long NotificationTriggeredID
  {
    get => this._NotificationTriggeredID;
    set => this._NotificationTriggeredID = value;
  }

  public static NotificationRecorded Load(long ID) => BaseDBObject.Load<NotificationRecorded>(ID);

  public DateTime NotificationDateLocalTime(long TimeZoneID)
  {
    return TimeZone.GetLocalTimeById(this.NotificationDate, TimeZoneID);
  }

  public static List<NotificationRecorded> LoadRecentByNotificationID(
    long notificationID,
    int count)
  {
    return new Monnit.Data.NotificationRecorded.LoadLastByNotificationID(notificationID, count).Result;
  }

  public static List<NotificationRecorded> LoadBySensorID(long sensorID)
  {
    return BaseDBObject.LoadByForeignKey<NotificationRecorded>("SensorID", (object) sensorID);
  }

  public static List<NotificationRecorded> LoadRecentBySensorID(long sensorID, int count)
  {
    return new Monnit.Data.NotificationRecorded.LoadLastBySensorID(sensorID, count).Result;
  }

  public static List<NotificationRecorded> LoadRecentBySensorIDAndNotificationID(
    long sensorID,
    long notificationID,
    int count)
  {
    return new Monnit.Data.NotificationRecorded.LoadRecentBySensorIDAndNotificationID(sensorID, notificationID, count).Result;
  }

  public static List<NotificationRecorded> LoadBySensorAndDateRange(
    long sensorID,
    DateTime fromDate,
    DateTime toDate,
    int count)
  {
    return new Monnit.Data.NotificationRecorded.LoadBySensorAndDateRange(sensorID, fromDate, toDate, count).Result;
  }

  public static List<NotificationRecorded> LoadByNotificationAndDateRange(
    long notificationID,
    DateTime fromDate,
    DateTime toDate,
    int count)
  {
    return new Monnit.Data.NotificationRecorded.LoadByNotificationAndDateRange(notificationID, fromDate, toDate, count).Result;
  }

  public static List<NotificationRecorded> LoadRecentByGatewayID(long gatewayID, int count)
  {
    return new Monnit.Data.NotificationRecorded.LoadLastByGatewayID(gatewayID, count).Result;
  }

  public static List<NotificationRecorded> LoadRecentByGatewayIDAndNotificationID(
    long gatewayID,
    long notificationID,
    int count)
  {
    return new Monnit.Data.NotificationRecorded.LoadRecentByGatewayIDAndNotificationID(gatewayID, notificationID, count).Result;
  }

  public static List<NotificationRecorded> LoadByGatewayAndDateRange(
    long gatewayID,
    DateTime fromDate,
    DateTime toDate,
    int count)
  {
    return new Monnit.Data.NotificationRecorded.LoadByGatewayAndDateRange(gatewayID, fromDate, toDate, count).Result;
  }

  public static List<NotificationRecorded> LoadByCustomerID(long customerID)
  {
    return BaseDBObject.LoadByForeignKey<NotificationRecorded>("CustomerID", (object) customerID);
  }

  public static bool ClearMessagesBySentToDeviceID(long deviceID)
  {
    return new Monnit.Data.NotificationRecorded.ClearMessagesBySentToDeviceID(deviceID).Result;
  }

  public static List<NotificationRecorded> LoadRecentByAccountID(long accountID, int count)
  {
    return new Monnit.Data.NotificationRecorded.LoadLastByAccountID(accountID, count).Result;
  }

  public static List<NotificationRecorded> LoadByAccountAndDateRange(
    long accountID,
    DateTime fromDate,
    DateTime toDate,
    int count)
  {
    return new Monnit.Data.NotificationRecorded.LoadByAccountAndDateRange(accountID, fromDate, toDate, count).Result;
  }

  public static List<NotificationRecorded> LoadRecentByCSNetID(long csnetID, int count)
  {
    return new Monnit.Data.NotificationRecorded.LoadRecentByCSNetID(csnetID, count).Result;
  }

  public static List<NotificationRecorded> LoadEmailRetries()
  {
    return new Monnit.Data.NotificationRecorded.LoadEmailRetries().Result;
  }

  public static List<NotificationRecorded> LoadGetMessageForLocalNotifier(long sensorID)
  {
    return new Monnit.Data.NotificationRecorded.GetMessageForDevice(sensorID).Result;
  }

  public static NotificationRecorded LoadNotificationForDeviceByQueueID(long sensorID, int queueID)
  {
    return new Monnit.Data.NotificationRecorded.LoadNotificationForDeviceByQueueID(sensorID, queueID).Result;
  }

  public static List<NotificationRecorded> LoadGetMessageForControl(long sensorID)
  {
    return new Monnit.Data.NotificationRecorded.GetMessageForDevice(sensorID).Result;
  }

  public static List<NotificationRecorded> LoadGetNoneDevliveredMessagesForControl(long sensorID)
  {
    return new Monnit.Data.NotificationRecorded.GetNoneDeliveredMessages(sensorID).Result;
  }

  public static List<NotificationRecorded> LoadNonAcknowledgedNotificationForDevice(long sensorID)
  {
    return new Monnit.Data.NotificationRecorded.LoadNonAcknowledgedNotificationForDevice(sensorID).Result;
  }

  public static List<NotificationRecorded> SentNotificationsLoadedBySensorID(
    long sensorID,
    DateTime from,
    DateTime to,
    int limit)
  {
    return new Monnit.Data.NotificationRecorded.LoadRangeForNotificationRecordedBySensor(sensorID, from, to, limit).Result;
  }

  public static DataTable Last7Days(long accountID, TimeSpan offset)
  {
    return new Monnit.Data.NotificationRecorded.Last7Days(accountID, offset.Hours).Result;
  }

  public static DataTable Last7DaysByCSNetID(long csnetID, TimeSpan offset)
  {
    return new Monnit.Data.NotificationRecorded.Last7DaysByCSNetID(csnetID, offset.Hours).Result;
  }

  public static int NextQueueID(long sensorID)
  {
    int result = new Monnit.Data.NotificationRecorded.LastQueueID(sensorID).Result;
    return result <= 0 || result >= (int) byte.MaxValue ? 1 : result + 1;
  }

  public static List<NotificationRecorded> LoadRecentForReseller(
    long resellerAccountID,
    int minutes,
    long lastRecordedID)
  {
    return new Monnit.Data.NotificationRecorded.LoadRecentForReseller(resellerAccountID, minutes, lastRecordedID).Result;
  }
}
