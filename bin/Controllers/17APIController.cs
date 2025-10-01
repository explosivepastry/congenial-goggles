// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APISensorMetaData
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APISensorMetaData
{
  public APISensorMetaData()
  {
  }

  public APISensorMetaData(Sensor sensor)
  {
    this.ApplicationID = sensor.ApplicationID;
    this.SensorID = sensor.SensorID;
    this.SensorTypeID = sensor.SensorTypeID;
    this.FirmwareVersion = sensor.FirmwareVersion;
    this.PowerSourceID = sensor.PowerSourceID;
    this.RadioBand = sensor.RadioBand;
    this.Calibration1 = sensor.Calibration1;
    this.Calibration2 = sensor.Calibration2;
    this.Calibration3 = sensor.Calibration3;
    this.Calibration4 = sensor.Calibration4;
    this.GenerationType = string.IsNullOrWhiteSpace(sensor.GenerationType) ? "Gen1" : sensor.GenerationType;
  }

  public long ApplicationID { get; set; }

  public long SensorID { get; set; }

  public long SensorTypeID { get; set; }

  public string FirmwareVersion { get; set; }

  public long PowerSourceID { get; set; }

  public string RadioBand { get; set; }

  public long Calibration1 { get; set; }

  public long Calibration2 { get; set; }

  public long Calibration3 { get; set; }

  public long Calibration4 { get; set; }

  public string GenerationType { get; set; }

  public long CableID { get; set; }

  public bool IsCableEnabled { get; set; }
}
