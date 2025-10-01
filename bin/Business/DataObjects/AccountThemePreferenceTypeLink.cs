// Decompiled with JetBrains decompiler
// Type: Monnit.AccountThemePreferenceTypeLink
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("AccountThemePreferenceTypeLink")]
public class AccountThemePreferenceTypeLink : BaseDBObject
{
  private long _AccountThemePreferenceTypeLinkID = long.MinValue;
  private long _AccountThemeID = long.MinValue;
  private long _PreferenceTypeID = long.MinValue;
  private string _DefaultValue = string.Empty;
  private bool _AccountCanOverride = false;
  private bool _CustomerCanOverride = false;

  [DBProp("AccountThemePreferenceTypeLinkID", IsPrimaryKey = true)]
  public long AccountThemePreferenceTypeLinkID
  {
    get => this._AccountThemePreferenceTypeLinkID;
    set => this._AccountThemePreferenceTypeLinkID = value;
  }

  [DBProp("AccountThemeID")]
  [DBForeignKey("AccountTheme", "AccountThemeID")]
  public long AccountThemeID
  {
    get => this._AccountThemeID;
    set => this._AccountThemeID = value;
  }

  [DBProp("PreferenceTypeID")]
  [DBForeignKey("PreferenceType", "PreferenceTypeID")]
  public long PreferenceTypeID
  {
    get => this._PreferenceTypeID;
    set => this._PreferenceTypeID = value;
  }

  [DBProp("DefaultValue", MaxLength = 2000, AllowNull = true)]
  public string DefaultValue
  {
    get => this._DefaultValue;
    set => this._DefaultValue = value;
  }

  [DBProp("AccountCanOverride")]
  public bool AccountCanOverride
  {
    get => this._AccountCanOverride;
    set => this._AccountCanOverride = value;
  }

  [DBProp("CustomerCanOverride")]
  public bool CustomerCanOverride
  {
    get => this._CustomerCanOverride;
    set => this._CustomerCanOverride = value;
  }

  public static AccountThemePreferenceTypeLink Load(long id)
  {
    return BaseDBObject.Load<AccountThemePreferenceTypeLink>(id);
  }

  public static List<AccountThemePreferenceTypeLink> LoadByAccountThemeID(long accountThemeID)
  {
    return BaseDBObject.LoadByForeignKey<AccountThemePreferenceTypeLink>("AccountThemeID", (object) accountThemeID);
  }

  public static List<AccountThemePreferenceTypeLink> LoadByPreferenceTypeID(long preferenceTypeID)
  {
    return BaseDBObject.LoadByForeignKey<AccountThemePreferenceTypeLink>("PreferenceTypeID", (object) preferenceTypeID);
  }

  public static AccountThemePreferenceTypeLink LoadByPreferenceTypeIDandAccountThemeID(
    long preferenceTypeID,
    long accountThemeID)
  {
    return BaseDBObject.LoadByForeignKeys<AccountThemePreferenceTypeLink>(new string[2]
    {
      "PreferenceTypeID",
      "AccountThemeID"
    }, new object[2]
    {
      (object) preferenceTypeID,
      (object) accountThemeID
    }).FirstOrDefault<AccountThemePreferenceTypeLink>();
  }
}
