// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APISensorExteded
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APISensorExteded : APISensor
{
  public APISensorExteded()
  {
  }

  public APISensorExteded(Sensor sensor)
    : base(sensor)
  {
    this.ReportInterval = sensor.ReportInterval;
    this.ActiveStateInterval = sensor.ActiveStateInterval;
    this.InactivityAlert = sensor.MinimumCommunicationFrequency;
    this.MeasurementsPerTransmission = sensor.MeasurementsPerTransmission;
    this.MinimumThreshold = sensor.MinimumThreshold;
    this.MaximumThreshold = sensor.MaximumThreshold;
    this.Hysteresis = sensor.Hysteresis;
    this.Tag = sensor.TagString;
    this.TransmitOffset = sensor.TransmitOffset;
    this.Recovery = sensor.Recovery;
    this.Calibration1 = sensor.Calibration1;
    this.Calibration2 = sensor.Calibration2;
    this.Calibration3 = sensor.Calibration3;
    this.Calibration4 = sensor.Calibration4;
  }

  public double ReportInterval { get; set; }

  public double ActiveStateInterval { get; set; }

  public int InactivityAlert { get; set; }

  public int MeasurementsPerTransmission { get; set; }

  public long MinimumThreshold { get; set; }

  public long MaximumThreshold { get; set; }

  public long Hysteresis { get; set; }

  public string Tag { get; set; }

  public int TransmitOffset { get; set; }

  public int Recovery { get; set; }

  public long Calibration1 { get; set; }

  public long Calibration2 { get; set; }

  public long Calibration3 { get; set; }

  public long Calibration4 { get; set; }
}
