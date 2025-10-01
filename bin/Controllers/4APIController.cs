// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APISensorGetExtendedForFulfillment
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APISensorGetExtendedForFulfillment : APISensor
{
  public APISensorGetExtendedForFulfillment(Sensor sensor)
    : base(sensor)
  {
    this.ApplicationID = sensor.ApplicationID;
    this.SensorID = sensor.SensorID;
    this.SensorName = sensor.SensorName;
    this.SensorTypeID = sensor.SensorTypeID;
    this.FirmwareVersion = sensor.FirmwareVersion;
    this.PowerSourceID = sensor.PowerSourceID;
    this.RadioBand = sensor.RadioBand;
    this.MinimumThreshold = sensor.MinimumThreshold;
    this.MaximumThreshold = sensor.MaximumThreshold;
    this.Hysteresis = sensor.Hysteresis;
    this.Calibration1 = sensor.Calibration1;
    this.Calibration2 = sensor.Calibration2;
    this.Calibration3 = sensor.Calibration3;
    this.Calibration4 = sensor.Calibration4;
  }

  public new long ApplicationID { get; set; }

  public new long SensorID { get; set; }

  public new string SensorName { get; set; }

  public long SensorTypeID { get; set; }

  public string FirmwareVersion { get; set; }

  public new long PowerSourceID { get; set; }

  public string RadioBand { get; set; }

  public long MinimumThreshold { get; set; }

  public long MaximumThreshold { get; set; }

  public long Hysteresis { get; set; }

  public long Calibration1 { get; set; }

  public long Calibration2 { get; set; }

  public long Calibration3 { get; set; }

  public long Calibration4 { get; set; }
}
