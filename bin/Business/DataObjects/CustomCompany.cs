// Decompiled with JetBrains decompiler
// Type: Monnit.CustomCompany
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("CustomCompany")]
public class CustomCompany : BaseDBObject
{
  private long _CompanyID = long.MinValue;
  private string _CompanyName = string.Empty;

  [DBProp("CompanyID", IsPrimaryKey = true)]
  public long CompanyID
  {
    get => this._CompanyID;
    set => this._CompanyID = value;
  }

  [DBProp("CompanyName", MaxLength = 255 /*0xFF*/, AllowNull = false)]
  public string CompanyName
  {
    get => this._CompanyName;
    set => this._CompanyName = value;
  }

  public static List<CustomCompany> LoadAll() => BaseDBObject.LoadAll<CustomCompany>();
}
