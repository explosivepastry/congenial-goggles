// Decompiled with JetBrains decompiler
// Type: Monnit.CustomCompanyDevice
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("CustomCompanyDevice")]
public class CustomCompanyDevice : BaseDBObject
{
  private long _CustomCompanyDeviceID = long.MinValue;
  private long _CompanyID = long.MinValue;
  private string _Sku = string.Empty;
  private string _Name = string.Empty;
  private string _Value = string.Empty;
  private long _ApplicationID = long.MinValue;
  private long _GatewayTypeID = long.MinValue;
  private bool _IgnoreOnClose = false;

  [DBProp("CustomCompanyDeviceID", IsPrimaryKey = true)]
  public long CustomCompanyDeviceID
  {
    get => this._CustomCompanyDeviceID;
    set => this._CustomCompanyDeviceID = value;
  }

  [DBProp("CompanyID", AllowNull = false)]
  [DBForeignKey("CustomCompany", "CompanyID")]
  public long CompanyID
  {
    get => this._CompanyID;
    set => this._CompanyID = value;
  }

  [DBProp("Sku", MaxLength = 300, AllowNull = false)]
  public string Sku
  {
    get => this._Sku;
    set => this._Sku = value;
  }

  [DBProp("Name", MaxLength = 300, AllowNull = false)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Value", MaxLength = 300, AllowNull = false)]
  public string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  [DBProp("ApplicationID", AllowNull = true)]
  [DBForeignKey("Application", "ApplicationID")]
  public long ApplicationID
  {
    get => this._ApplicationID;
    set => this._ApplicationID = value;
  }

  [DBProp("GatewayTypeID", AllowNull = true)]
  [DBForeignKey("GatewayType", "GatewayTypeID")]
  public long GatewayTypeID
  {
    get => this._GatewayTypeID;
    set => this._GatewayTypeID = value;
  }

  [DBProp("IgnoreOnClose", AllowNull = false, DefaultValue = false)]
  public bool IgnoreOnClose
  {
    get => this._IgnoreOnClose;
    set => this._IgnoreOnClose = value;
  }

  public static List<CustomCompanyDevice> LoadSettings(long deviceID, bool doUpdate)
  {
    return new Monnit.Data.CustomCompanyDevice.LoadSettings(deviceID, doUpdate).Result;
  }
}
