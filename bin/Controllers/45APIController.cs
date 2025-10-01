// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIExpressSensor
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APIExpressSensor
{
  public APIExpressSensor()
  {
  }

  public APIExpressSensor(Sensor s)
    : this(s, s.CSNetID)
  {
  }

  public APIExpressSensor(Sensor sensor, long networkID)
  {
    this.SensorID = sensor.SensorID;
    this.MonnitApplicationID = sensor.ApplicationID;
    this.SensorName = sensor.SensorName;
    this.FirmwareVersion = sensor.FirmwareVersion;
    this.RadioBand = sensor.RadioBand;
    this.PowerSourceID = sensor.PowerSourceID;
    this.CSNetID = networkID;
  }

  public long SensorID { get; set; }

  public long MonnitApplicationID { get; set; }

  public long CSNetID { get; set; }

  public string SensorName { get; set; }

  public string FirmwareVersion { get; set; }

  public string RadioBand { get; set; }

  public long PowerSourceID { get; set; }
}
