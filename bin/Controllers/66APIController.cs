// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIHHTPowerEventDetail
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System.Data;

#nullable disable
namespace iMonnit.API;

public class APIHHTPowerEventDetail
{
  public APIHHTPowerEventDetail()
  {
  }

  public APIHHTPowerEventDetail(DataRow dataRow)
  {
    this.Location = dataRow["LocationName"].ToString();
    this.Start = dataRow["StartTime"].ToDateTime().ToString();
    this.End = dataRow["EndTime"].ToDateTime().ToString();
    this.PowerEventMinutes = dataRow["TotalTime"].ToString();
    this.MinPower = dataRow[nameof (MinPower)].ToString();
    this.MaxPower = dataRow[nameof (MaxPower)].ToString();
    this.SensorID = dataRow[nameof (SensorID)].ToString();
  }

  public string Location { get; set; }

  public string Start { get; set; }

  public string End { get; set; }

  public string PowerEventMinutes { get; set; }

  public string MinPower { get; set; }

  public string MaxPower { get; set; }

  public string SensorID { get; set; }
}
