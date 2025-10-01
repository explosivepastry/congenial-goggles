// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APICustomCompanyGateway
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APICustomCompanyGateway
{
  public APICustomCompanyGateway()
  {
  }

  public APICustomCompanyGateway(CustomCompanyGateway ccs)
  {
    this.CustomCompanyGatewayID = ccs.CustomCompanyGatewayID;
    this.CompanyID = ccs.CompanyID;
    this.GatewayTypeID = ccs.GatewayTypeID;
    this.Name = ccs.Name;
    this.Value = ccs.Value;
  }

  public long CustomCompanyGatewayID { get; set; }

  public long CompanyID { get; set; }

  public long GatewayTypeID { get; set; }

  public string Name { get; set; }

  public string Value { get; set; }
}
