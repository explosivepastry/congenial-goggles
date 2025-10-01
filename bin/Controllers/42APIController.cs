// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APICustomCompany
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APICustomCompany
{
  public APICustomCompany()
  {
  }

  public APICustomCompany(CustomCompany cc)
  {
    this.CompanyID = cc.CompanyID;
    this.CompanyName = cc.CompanyName;
  }

  public long CompanyID { get; set; }

  public string CompanyName { get; set; }
}
