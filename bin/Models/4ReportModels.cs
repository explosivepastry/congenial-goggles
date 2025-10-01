// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.SensorInformation
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System.ComponentModel;

#nullable disable
namespace iMonnit.Models;

public class SensorInformation
{
  public Sensor Sensor { get; set; }

  public Account Account { get; set; }

  public CSNet Network { get; set; }

  [DisplayName("SensorID")]
  public long SensorID { get; set; }

  [DisplayName("Name")]
  public string Name { get; set; }

  public SensorInformation(long id)
  {
    this.SensorID = id;
    this.Name = "Not Found";
  }

  public SensorInformation(Sensor sensor)
  {
    this.Sensor = sensor;
    this.SensorID = this.Sensor.SensorID;
    this.Name = this.Sensor.SensorName;
    this.Account = Account.Load(this.Sensor.AccountID);
    this.Network = CSNet.Load(this.Sensor.CSNetID);
  }
}
