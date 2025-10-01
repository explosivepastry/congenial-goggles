// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APICustomCompanySensor
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APICustomCompanySensor
{
  public APICustomCompanySensor()
  {
  }

  public APICustomCompanySensor(CustomCompanySensor ccs)
  {
    this.CustomCompanySensorID = ccs.CustomCompanySensorID;
    this.CompanyID = ccs.CompanyID;
    this.MonnitApplicationID = ccs.ApplicationID;
    this.Name = ccs.Name;
    this.Value = ccs.Value;
  }

  public long CustomCompanySensorID { get; set; }

  public long CompanyID { get; set; }

  public long MonnitApplicationID { get; set; }

  public string Name { get; set; }

  public string Value { get; set; }
}
