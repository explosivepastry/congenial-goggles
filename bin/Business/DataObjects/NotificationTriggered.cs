// Decompiled with JetBrains decompiler
// Type: Monnit.NotificationTriggered
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit;

[DBClass("NotificationTriggered")]
public class NotificationTriggered : BaseDBObject
{
  private long _NotificationTriggeredID = long.MinValue;
  private long _NotificationID = long.MinValue;
  private DateTime _StartTime = DateTime.MinValue;
  private long _SensorNotificationID = long.MinValue;
  private long _GatewayNotificationID = long.MinValue;
  private DateTime _AcknowledgedTime = DateTime.MinValue;
  private long _AcknowledgedBy = long.MinValue;
  private string _Reading = string.Empty;
  private DateTime _ReadingDate = DateTime.MinValue;
  private string _AcknowledgeMethod = string.Empty;
  private DateTime _resetTime = DateTime.MinValue;
  private string _OriginalReading = string.Empty;
  private DateTime _OriginalReadingDate = DateTime.MinValue;
  private bool _HasNote = false;

  [DBProp("NotificationTriggeredID", IsPrimaryKey = true)]
  public long NotificationTriggeredID
  {
    get => this._NotificationTriggeredID;
    set => this._NotificationTriggeredID = value;
  }

  [DBForeignKey("Notification", "NotificationID")]
  [DBProp("NotificationID", AllowNull = false)]
  public long NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [DBProp("StartTime", AllowNull = false)]
  public DateTime StartTime
  {
    get => this._StartTime;
    set => this._StartTime = value;
  }

  [DBProp("SensorNotificationID")]
  public long SensorNotificationID
  {
    get => this._SensorNotificationID;
    set => this._SensorNotificationID = value;
  }

  [DBProp("GatewayNotificationID")]
  public long GatewayNotificationID
  {
    get => this._GatewayNotificationID;
    set => this._GatewayNotificationID = value;
  }

  [DBProp("AcknowledgedTime", AllowNull = true)]
  public DateTime AcknowledgedTime
  {
    get => this._AcknowledgedTime;
    set => this._AcknowledgedTime = value;
  }

  [DBProp("AcknowledgedBy", AllowNull = true)]
  [DBForeignKey("Customer", "CustomerID")]
  public long AcknowledgedBy
  {
    get => this._AcknowledgedBy;
    set => this._AcknowledgedBy = value;
  }

  [DBProp("Reading", MaxLength = 200, AllowNull = true)]
  public string Reading
  {
    get => this._Reading;
    set => this._Reading = value;
  }

  [DBProp("ReadingDate")]
  public DateTime ReadingDate
  {
    get => this._ReadingDate;
    set => this._ReadingDate = value;
  }

  [DBProp("AcknowledgeMethod", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string AcknowledgeMethod
  {
    get => this._AcknowledgeMethod;
    set => this._AcknowledgeMethod = value;
  }

  [DBProp("resetTime", AllowNull = true)]
  public DateTime resetTime
  {
    get => this._resetTime;
    set => this._resetTime = value;
  }

  [DBProp("OriginalReading", MaxLength = 200, AllowNull = true)]
  public string OriginalReading
  {
    get => this._OriginalReading;
    set => this._OriginalReading = value;
  }

  [DBProp("OriginalReadingDate")]
  public DateTime OriginalReadingDate
  {
    get => this._OriginalReadingDate;
    set => this._OriginalReadingDate = value;
  }

  [DBProp("HasNote", DefaultValue = 0)]
  public bool HasNote
  {
    get => this._HasNote;
    set => this._HasNote = value;
  }

  public static NotificationTriggered Load(long ID) => BaseDBObject.Load<NotificationTriggered>(ID);

  public static List<NotificationTriggered> LoadActiveByNotificationID(long notiID)
  {
    return new Monnit.Data.NotificationTriggered.LoadActiveByNotificationID(notiID).Result;
  }

  public static List<NotificationTriggered> LoadActiveByDeviceIDandNotificationID(
    long notiID,
    long? sensorID,
    long? gatewayID)
  {
    return new Monnit.Data.NotificationTriggered.LoadActiveByDeviceIDandNotificationID(notiID, sensorID, gatewayID).Result;
  }

  public static List<NotificationTriggered> LoadActiveBySensorID(long sensorID)
  {
    return new Monnit.Data.NotificationTriggered.LoadActiveBySensorID(sensorID).Result;
  }

  public static List<NotificationTriggered> LoadActiveByAccountID(long accountID)
  {
    return new Monnit.Data.NotificationTriggered.LoadActiveByAccountID(accountID).Result;
  }

  public static List<NotificationTriggered> LoadActiveByGatewayID(long gatewayID)
  {
    return new Monnit.Data.NotificationTriggered.LoadActiveByGatewayID(gatewayID).Result;
  }

  public static List<NotificationTriggered> LoadLastByNotificationID(long notificationID, int count)
  {
    return new Monnit.Data.NotificationTriggered.LoadLastByNotificationID(notificationID, count).Result;
  }

  public static NotificationTriggered LoadByNotificationRecordedID(long notificationRecordedID)
  {
    return new Monnit.Data.NotificationTriggered.LoadByNotificationRecordedID(notificationRecordedID).Result;
  }

  public static DataSet LoadReady() => Monnit.Data.NotificationTriggered.LoadReady.Exec();

  public static DataSet LoadImmediate(long notificationID)
  {
    return Monnit.Data.NotificationTriggered.LoadImmediate.Exec(notificationID);
  }
}
