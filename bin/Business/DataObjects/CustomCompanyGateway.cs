// Decompiled with JetBrains decompiler
// Type: Monnit.CustomCompanyGateway
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("CustomCompanyGateway")]
public class CustomCompanyGateway : BaseDBObject
{
  private long _CustomCompanyGatewayID = long.MinValue;
  private long _CompanyID = long.MinValue;
  private long _GatewayTypeID = long.MinValue;
  private string _Name = string.Empty;
  private string _Value = string.Empty;

  [DBProp("CustomCompanyGatewayID", IsPrimaryKey = true)]
  public long CustomCompanyGatewayID
  {
    get => this._CustomCompanyGatewayID;
    set => this._CustomCompanyGatewayID = value;
  }

  [DBProp("CompanyID")]
  [DBForeignKey("CustomCompany", "CompanyID")]
  public long CompanyID
  {
    get => this._CompanyID;
    set => this._CompanyID = value;
  }

  [DBProp("GatewayTypeID")]
  [DBForeignKey("GatewayType", "GatewayTypeID")]
  public long GatewayTypeID
  {
    get => this._GatewayTypeID;
    set => this._GatewayTypeID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Value", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  public static List<CustomCompanyGateway> LoadGatewayByCompanyAndTypeID(
    long companyID,
    long gatewayTypeID)
  {
    return BaseDBObject.LoadByForeignKeys<CustomCompanyGateway>(new string[2]
    {
      "CompanyID",
      "GatewayTypeID"
    }, new object[2]
    {
      (object) companyID,
      (object) gatewayTypeID
    });
  }

  public static List<CustomCompanyGateway> LoadGatewayByCompanyID(long companyID)
  {
    return BaseDBObject.LoadByForeignKey<CustomCompanyGateway>("CompanyID", (object) companyID);
  }
}
