// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APISensorGroupSensor
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;

#nullable disable
namespace iMonnit.API;

public class APISensorGroupSensor
{
  public APISensorGroupSensor()
  {
  }

  public APISensorGroupSensor(SensorGroupSensor sensor)
  {
    Sensor sensor1 = Sensor.Load(sensor.SensorID);
    this.SensorGroupID = sensor.SensorGroupID;
    this.SensorID = sensor.SensorID;
    this.Position = sensor.Position == int.MinValue ? 0 : sensor.Position;
    this.MonnitApplicationID = sensor1.ApplicationID;
    this.ApplicationID = sensor1.ApplicationID;
    this.CSNetID = sensor1.CSNetID;
    this.SensorName = sensor1.SensorName;
    this.LastCommunicationDate = DateTime.SpecifyKind(sensor1.LastCommunicationDate, DateTimeKind.Utc);
    this.NextCommunicationDate = DateTime.SpecifyKind(sensor1.NextCommunicationDate, DateTimeKind.Utc);
    this.LastDataMessageMessageGUID = sensor1.LastDataMessageGUID;
    this.PowerSourceID = sensor1.PowerSourceID;
    this.Status = (int) sensor1.Status;
    this.CanUpdate = sensor1.CanUpdate;
    this.AlertsActive = sensor1.SendNotification;
    this.AccountID = sensor1.AccountID;
  }

  public long SensorID { get; set; }

  public long SensorGroupID { get; set; }

  public int Position { get; set; }

  public long ApplicationID { get; set; }

  public long CSNetID { get; set; }

  public string SensorName { get; set; }

  public DateTime LastCommunicationDate { get; set; }

  public DateTime NextCommunicationDate { get; set; }

  public Guid LastDataMessageMessageGUID { get; set; }

  public long PowerSourceID { get; set; }

  public int Status { get; set; }

  public bool CanUpdate { get; set; }

  public double BatteryLevel { get; set; }

  public double SignalStrength { get; set; }

  public bool AlertsActive { get; set; }

  public string CheckDigit => MonnitUtil.CheckDigit(this.SensorID);

  public long AccountID { get; set; }

  public long MonnitApplicationID { get; set; }
}
