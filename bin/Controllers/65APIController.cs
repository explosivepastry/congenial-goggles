// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIHHTHumidityDeviceData
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System.Data;

#nullable disable
namespace iMonnit.API;

public class APIHHTHumidityDeviceData
{
  public APIHHTHumidityDeviceData()
  {
  }

  public APIHHTHumidityDeviceData(DataRow dataRow)
  {
    this.SensorName = dataRow["Sensor Name"].ToString();
    this.Date = dataRow[nameof (Date)].ToDateTime().ToString();
    this.MinTemp = dataRow["Min Temp"].ToString();
    this.MaxTemp = dataRow["Max Temp"].ToString();
    this.AvgTemp = dataRow["Avg Temp"].ToString();
    this.MinHumidity = dataRow["Min Humidity"].ToString();
    this.MaxHumidity = dataRow["Max Humidity"].ToString();
    this.AvgHumidity = dataRow["Avg Humidity"].ToString();
    this.SensorID = dataRow[nameof (SensorID)].ToString();
  }

  public string SensorName { get; set; }

  public string Date { get; set; }

  public string MinTemp { get; set; }

  public string MaxTemp { get; set; }

  public string AvgTemp { get; set; }

  public string MinHumidity { get; set; }

  public string MaxHumidity { get; set; }

  public string AvgHumidity { get; set; }

  public string SensorID { get; set; }
}
