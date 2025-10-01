// Decompiled with JetBrains decompiler
// Type: Monnit.AccountThemeProperty
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("AccountThemeProperty")]
public class AccountThemeProperty : BaseDBObject
{
  private long _AccountThemePropertyID = long.MinValue;
  private long _AccountThemeID = long.MinValue;
  private long _AccountThemePropertyTypeID = long.MinValue;
  private string _Value = string.Empty;

  [DBProp("AccountThemePropertyID", IsPrimaryKey = true)]
  public long AccountThemePropertyID
  {
    get => this._AccountThemePropertyID;
    set => this._AccountThemePropertyID = value;
  }

  [DBProp("AccountThemeID", AllowNull = false)]
  [DBForeignKey("AccountTheme", "AccountThemeID")]
  public long AccountThemeID
  {
    get => this._AccountThemeID;
    set => this._AccountThemeID = value;
  }

  [DBProp("AccountThemePropertyTypeID", AllowNull = true)]
  [DBForeignKey("AccountThemePropertyType", "AccountThemePropertyTypeID")]
  public long AccountThemePropertyTypeID
  {
    get => this._AccountThemePropertyTypeID;
    set => this._AccountThemePropertyTypeID = value;
  }

  public AccountThemePropertyType Type
  {
    get => AccountThemePropertyType.Find(this.AccountThemePropertyTypeID);
  }

  [DBProp("Value", AllowNull = false, International = true)]
  public string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  public static List<AccountThemeProperty> LoadByAccountThemeID(long accountThemeID)
  {
    return new Monnit.Data.AccountThemeProperty.LoadByAccountThemeID(accountThemeID).Result;
  }
}
