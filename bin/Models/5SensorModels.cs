// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.WIFISensorEditModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.Models;

public class WIFISensorEditModel
{
  public WIFISensorEditModel()
  {
  }

  public WIFISensorEditModel(Gateway gateway, Sensor sensor)
  {
    this.Gateway = gateway;
    this.Sensor = sensor;
  }

  public Gateway Gateway { get; set; }

  public Sensor Sensor { get; set; }

  public enum SortSensor
  {
    Name,
    LastCheckIn,
    Type,
    Battery,
    SignalStrength,
    CurrentReading,
  }

  public enum SortOrder
  {
    Asc,
    Desc,
  }
}
