// Decompiled with JetBrains decompiler
// Type: Monnit.SensorNotification
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("SensorNotification")]
public class SensorNotification : BaseDBObject
{
  private long _SensorNotificationID = long.MinValue;
  private long _SensorID = long.MinValue;
  private long _NotificationID = long.MinValue;
  private DateTime _TriggerDate = DateTime.MinValue;
  private bool _AutoReset = true;
  private int _DatumIndex = int.MinValue;

  [DBProp("SensorNotificationID", IsPrimaryKey = true)]
  public long SensorNotificationID
  {
    get => this._SensorNotificationID;
    set => this._SensorNotificationID = value;
  }

  [DBForeignKey("Sensor", "SensorID")]
  [DBProp("SensorID", AllowNull = false)]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
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

  [DBForeignKey("Notification", "NotificationID")]
  [DBProp("NotificationID", AllowNull = false)]
  public long NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [DBProp("DatumIndex", AllowNull = false, DefaultValue = 0)]
  public int DatumIndex
  {
    get => this._DatumIndex;
    set => this._DatumIndex = value;
  }

  public static List<SensorDatum> LoadByNotificationID(long notificationID)
  {
    long minValue = long.MinValue;
    bool snoozeTrigger = false;
    return new Monnit.Data.SensorNotification.LoadByNotificationID(notificationID, minValue, snoozeTrigger).Result;
  }

  public static List<SensorDatum> LoadByNotificationID(
    long notificationID,
    long sensorID,
    bool snoozeTrigger)
  {
    return new Monnit.Data.SensorNotification.LoadByNotificationID(notificationID, sensorID, snoozeTrigger).Result;
  }

  public static void AddSensor(long notificationID, long sensorID, int datumindex = 0)
  {
    Monnit.Data.SensorNotification.AddSensor addSensor = new Monnit.Data.SensorNotification.AddSensor(notificationID, sensorID, datumindex);
  }

  public static void RemoveSensor(long notificationID, long sensorID, int datumindex = 0)
  {
    Monnit.Data.SensorNotification.RemoveSensor removeSensor = new Monnit.Data.SensorNotification.RemoveSensor(notificationID, sensorID, datumindex);
  }

  public static List<int> DatumsForSensor(long notificationID, long sensorID)
  {
    return new Monnit.Data.SensorNotification.DatumsForSensor(notificationID, sensorID).Result;
  }

  public static SensorNotification LoadBySensorIDAndNotificationID(
    long sensorID,
    long notificationID)
  {
    return BaseDBObject.LoadByForeignKeys<SensorNotification>(new string[2]
    {
      "NotificationID",
      "SensorID"
    }, new object[2]
    {
      (object) notificationID,
      (object) sensorID
    }).FirstOrDefault<SensorNotification>();
  }

  public static SensorNotification Load(long ID) => BaseDBObject.Load<SensorNotification>(ID);
}
