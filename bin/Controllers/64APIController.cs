// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIHHTDeviceUtilization
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System.Data;

#nullable disable
namespace iMonnit.API;

public class APIHHTDeviceUtilization
{
  public APIHHTDeviceUtilization()
  {
  }

  public APIHHTDeviceUtilization(DataRow dataRow)
  {
    this.Location = dataRow["LocationName"].ToString();
    this.DeviceName = dataRow["Make"].ToString();
    this.AssetTag = dataRow["SerialNumber"].ToString();
    this.Date = dataRow[nameof (Date)].ToDateTime().ToString();
    this.HoursOn = dataRow[nameof (HoursOn)].ToString();
    this.SensorID = dataRow[nameof (SensorID)].ToString();
  }

  public string Location { get; set; }

  public string DeviceName { get; set; }

  public string AssetTag { get; set; }

  public string Date { get; set; }

  public string HoursOn { get; set; }

  public string SensorID { get; set; }
}
