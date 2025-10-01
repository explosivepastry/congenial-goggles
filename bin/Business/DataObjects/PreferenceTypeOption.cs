// Decompiled with JetBrains decompiler
// Type: Monnit.PreferenceTypeOption
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("PreferenceTypeOption")]
public class PreferenceTypeOption : BaseDBObject
{
  private long _PreferenceTypeOptionID = long.MinValue;
  private long _PreferenceTypeID = long.MinValue;
  private long _AccountThemeID = long.MinValue;
  private string _Name = string.Empty;
  private string _Value = string.Empty;

  [DBProp("PreferenceTypeOptionID", IsPrimaryKey = true)]
  public long PreferenceTypeOptionID
  {
    get => this._PreferenceTypeOptionID;
    set => this._PreferenceTypeOptionID = value;
  }

  [DBProp("PreferenceTypeID")]
  [DBForeignKey("PreferenceType", "PreferenceTypeID")]
  public long PreferenceTypeID
  {
    get => this._PreferenceTypeID;
    set => this._PreferenceTypeID = value;
  }

  [DBProp("AccountThemeID")]
  [DBForeignKey("AccountTheme", "AccountThemeID")]
  public long AccountThemeID
  {
    get => this._AccountThemeID;
    set => this._AccountThemeID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Value", MaxLength = 2000, AllowNull = true)]
  public string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  public static List<PreferenceTypeOption> LoadByAccountThemeID(long accountThemeID)
  {
    return BaseDBObject.LoadByForeignKey<PreferenceTypeOption>("AccountThemeID", (object) accountThemeID);
  }

  public static List<PreferenceTypeOption> LoadByPreferenceTypeID(long preferenceTypeID)
  {
    return BaseDBObject.LoadByForeignKey<PreferenceTypeOption>("PreferenceTypeID", (object) preferenceTypeID);
  }

  public static PreferenceTypeOption Load(long id) => BaseDBObject.Load<PreferenceTypeOption>(id);
}
