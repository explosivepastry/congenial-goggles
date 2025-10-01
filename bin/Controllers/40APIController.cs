// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APISensorEquipment
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APISensorEquipment
{
  public APISensorEquipment()
  {
  }

  public APISensorEquipment(Sensor sensor)
  {
    this.SensorID = sensor.SensorID;
    this.Make = sensor.Make;
    this.Model = sensor.Model;
    this.SerialNumber = sensor.SerialNumber;
    this.SensorLocation = sensor.Location;
    this.SensorDescription = sensor.Description;
    this.Note = sensor.Note;
  }

  public long SensorID { get; set; }

  public string Make { get; set; }

  public string Model { get; set; }

  public string SerialNumber { get; set; }

  public string SensorLocation { get; set; }

  public string SensorDescription { get; set; }

  public string Note { get; set; }
}
