// Decompiled with JetBrains decompiler
// Type: Monnit.OTARequestSensor
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("OTARequestSensor")]
public class OTARequestSensor : BaseDBObject
{
  private long _OTARequestSensorID = long.MinValue;
  private long _OTARequestID = long.MinValue;
  private long _SensorID = long.MinValue;
  private int _AttemptCount = 0;

  [DBProp("OTARequestSensorID", IsPrimaryKey = true)]
  public long OTARequestSensorID
  {
    get => this._OTARequestSensorID;
    set => this._OTARequestSensorID = value;
  }

  [DBProp("OTARequestID")]
  [DBForeignKey("OTARequest", "OTARequestID")]
  public long OTARequestID
  {
    get => this._OTARequestID;
    set => this._OTARequestID = value;
  }

  [DBProp("SensorID")]
  [DBForeignKey("Sensor", "SensorID")]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("Status", AllowNull = false, DefaultValue = 0)]
  public eOTAStatus Status { get; set; }

  [DBProp("StartDate")]
  public DateTime StartDate { get; set; } = DateTime.MinValue;

  [DBProp("DownloadedDate")]
  public DateTime DownloadedDate { get; set; } = DateTime.MinValue;

  [DBProp("CompletedDate")]
  public DateTime CompletedDate { get; set; } = DateTime.MinValue;

  [DBProp("AttemptCount")]
  public int AttemptCount
  {
    get => this._AttemptCount >= 0 ? this._AttemptCount : 0;
    set => this._AttemptCount = value;
  }

  public static OTARequestSensor Load(long id) => BaseDBObject.Load<OTARequestSensor>(id);

  public static List<OTARequestSensor> LoadByOTARequestID(long OTARequestID)
  {
    return BaseDBObject.LoadByForeignKey<OTARequestSensor>(nameof (OTARequestID), (object) OTARequestID);
  }

  public static List<OTARequestSensor> LoadActiveBySensorID(long sensorID)
  {
    return Monnit.Data.LoadActiveBySensorID.Exec(sensorID);
  }
}
