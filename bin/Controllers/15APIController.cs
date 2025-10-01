// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APILookUpSensor
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;

#nullable disable
namespace iMonnit.API;

public class APILookUpSensor
{
  public APILookUpSensor()
  {
  }

  public APILookUpSensor(Sensor sensor)
  {
    this.ApplicationID = sensor.ApplicationID;
    this.SensorID = sensor.SensorID;
    this.SensorName = sensor.SensorName;
    this.SensorTypeID = sensor.SensorTypeID;
    this.FirmwareVersion = sensor.FirmwareVersion;
    this.PowerSourceID = sensor.PowerSourceID;
    this.RadioBand = sensor.RadioBand;
    this.ReportInterval = sensor.ReportInterval;
    this.ActiveStateInterval = sensor.ActiveStateInterval;
    this.CalibrationCertification = sensor.CalibrationCertification;
    this.MeasurementsPerTransmission = sensor.MeasurementsPerTransmission;
    this.TransmitOffset = sensor.TransmitOffset;
    this.Hysteresis = sensor.Hysteresis;
    this.MinimumThreshold = sensor.MinimumThreshold;
    this.MaximumThreshold = sensor.MaximumThreshold;
    this.EventDetectionType = sensor.EventDetectionType;
    this.EventDetectionPeriod = sensor.EventDetectionPeriod;
    this.EventDetectionCount = sensor.EventDetectionCount;
    this.RearmTime = sensor.RearmTime;
    this.BiStable = sensor.BiStable;
    this.Calibration1 = sensor.Calibration1;
    this.Calibration2 = sensor.Calibration2;
    this.Calibration3 = sensor.Calibration3;
    this.Calibration4 = sensor.Calibration4;
    this.GenerationType = string.IsNullOrWhiteSpace(sensor.GenerationType) ? "Gen1" : sensor.GenerationType;
    this.SKU = sensor.SKU;
    this.IsCableEnabled = sensor.IsCableEnabled;
    this.CableID = sensor.CableID;
  }

  public long ApplicationID { get; set; }

  public long SensorID { get; set; }

  public string SensorName { get; set; }

  public long SensorTypeID { get; set; }

  public string FirmwareVersion { get; set; }

  public long PowerSourceID { get; set; }

  public string RadioBand { get; set; }

  public double ReportInterval { get; set; }

  public double ActiveStateInterval { get; set; }

  public string CalibrationCertification { get; set; }

  public int MeasurementsPerTransmission { get; set; }

  public int TransmitOffset { get; set; }

  public long Hysteresis { get; set; }

  public long MinimumThreshold { get; set; }

  public long MaximumThreshold { get; set; }

  public int EventDetectionType { get; set; }

  public int EventDetectionPeriod { get; set; }

  public int EventDetectionCount { get; set; }

  public int RearmTime { get; set; }

  public int BiStable { get; set; }

  public long Calibration1 { get; set; }

  public long Calibration2 { get; set; }

  public long Calibration3 { get; set; }

  public long Calibration4 { get; set; }

  public DateTime CalibrationCertificationValidUntil { get; set; }

  public string GenerationType { get; set; }

  public string SKU { get; set; }

  public long CableID { get; set; }

  public bool IsCableEnabled { get; set; }
}
