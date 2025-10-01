// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.RefreshSensorModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

#nullable disable
namespace iMonnit.Models;

public class RefreshSensorModel
{
  public RefreshSensorModel(long sensorID)
  {
    this.SensorID = sensorID;
    this.StatusImageUrl = "";
    this.Reading = "";
    this.Date = "";
    this.SignalImageUrl = "";
    this.PowerImageUrl = "";
    this.Voltage = 0.0;
    this.voltageString = "";
  }

  public long SensorID { get; set; }

  public string StatusImageUrl { get; set; }

  public string Reading { get; set; }

  public string Date { get; set; }

  public string SignalImageUrl { get; set; }

  public string PowerImageUrl { get; set; }

  public double Voltage { get; set; }

  public bool notificationPaused { get; set; }

  public bool isDirty { get; set; }

  public string voltageString { get; set; }
}
