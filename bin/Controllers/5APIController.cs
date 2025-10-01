// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APISensorCalibration
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APISensorCalibration
{
  public APISensorCalibration()
  {
  }

  public APISensorCalibration(Sensor sensor)
  {
    this.SensorID = sensor.SensorID;
    this.MonnitApplicationID = sensor.ApplicationID;
    this.ApplicationID = sensor.ApplicationID;
    this.CSNetID = sensor.CSNetID;
    this.SensorName = sensor.SensorName;
    this.Calibration1 = sensor.Calibration1;
    this.Calibration2 = sensor.Calibration2;
    this.Calibration3 = sensor.Calibration3;
    this.Calibration4 = sensor.Calibration4;
    this.EventDetectionType = sensor.EventDetectionType;
    this.EventDetectionPeriod = sensor.EventDetectionPeriod;
    this.EventDetectionCount = sensor.EventDetectionCount;
    this.RearmTime = sensor.RearmTime;
    this.BiStable = sensor.BiStable;
    this.TransmitOffset = sensor.TransmitOffset;
    this.Recovery = sensor.Recovery;
    this.PushProfileConfig1 = sensor.ProfileConfigDirty;
    this.PushProfileConfig2 = sensor.ProfileConfig2Dirty;
    this.PushAutoCalibrateCommand = sensor.PendingActionControlCommand;
  }

  public long SensorID { get; set; }

  public long ApplicationID { get; set; }

  public long CSNetID { get; set; }

  public string SensorName { get; set; }

  public long Calibration1 { get; set; }

  public long Calibration2 { get; set; }

  public long Calibration3 { get; set; }

  public long Calibration4 { get; set; }

  public int EventDetectionType { get; set; }

  public int EventDetectionPeriod { get; set; }

  public int EventDetectionCount { get; set; }

  public int RearmTime { get; set; }

  public int BiStable { get; set; }

  public int TransmitOffset { get; set; }

  public int Recovery { get; set; }

  public bool PushProfileConfig1 { get; set; }

  public bool PushProfileConfig2 { get; set; }

  public bool PushAutoCalibrateCommand { get; set; }

  public long MonnitApplicationID { get; set; }
}
