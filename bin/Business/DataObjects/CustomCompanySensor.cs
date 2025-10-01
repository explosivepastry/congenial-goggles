// Decompiled with JetBrains decompiler
// Type: Monnit.CustomCompanySensor
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("CustomCompanySensor")]
public class CustomCompanySensor : BaseDBObject
{
  private long _CustomCompanySensorID = long.MinValue;
  private long _CompanyID = long.MinValue;
  private long _MonnitApplicationID = long.MinValue;
  private string _Name = string.Empty;
  private string _Value = string.Empty;

  [DBProp("CustomCompanySensorID", IsPrimaryKey = true)]
  public long CustomCompanySensorID
  {
    get => this._CustomCompanySensorID;
    set => this._CustomCompanySensorID = value;
  }

  [DBProp("CompanyID")]
  [DBForeignKey("CustomCompany", "CompanyID")]
  public long CompanyID
  {
    get => this._CompanyID;
    set => this._CompanyID = value;
  }

  [DBProp("ApplicationID")]
  [DBForeignKey("Application", "ApplicationID")]
  public long MonnitApplicationID
  {
    get => this._MonnitApplicationID;
    set => this._MonnitApplicationID = value;
  }

  public long ApplicationID
  {
    get => this.MonnitApplicationID;
    set => this.MonnitApplicationID = value;
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

  public static List<CustomCompanySensor> LoadSensorByCompanyAndApplicationID(
    long companyID,
    long monnitApplicationID)
  {
    return BaseDBObject.LoadByForeignKeys<CustomCompanySensor>(new string[2]
    {
      "CompanyID",
      "ApplicationID"
    }, new object[2]
    {
      (object) companyID,
      (object) monnitApplicationID
    });
  }

  public static List<CustomCompanySensor> LoadSensorByCompanyID(long companyID)
  {
    return BaseDBObject.LoadByForeignKey<CustomCompanySensor>("CompanyID", (object) companyID);
  }
}
