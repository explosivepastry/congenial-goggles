// Decompiled with JetBrains decompiler
// Type: Monnit.AccountThemeMaintenanceLink
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("AccountThemeMaintenanceLink")]
public class AccountThemeMaintenanceLink : BaseDBObject
{
  private long _AccountThemeMaintenanceLinkID = long.MinValue;
  private long _AccountThemeID = long.MinValue;
  private long _MaintenanceWindowID = long.MinValue;
  private string _OverriddenNote = string.Empty;
  private string _OverriddenSMS = string.Empty;
  private bool _IsHidden = false;
  private string _OverriddenEmail = string.Empty;

  [DBProp("AccountThemeMaintenanceLinkID", IsPrimaryKey = true)]
  public long AccountThemeMaintenanceLinkID
  {
    get => this._AccountThemeMaintenanceLinkID;
    set => this._AccountThemeMaintenanceLinkID = value;
  }

  [DBProp("AccountThemeID")]
  [DBForeignKey("AccountTheme", "AccountThemeID")]
  public long AccountThemeID
  {
    get => this._AccountThemeID;
    set => this._AccountThemeID = value;
  }

  [DBProp("MaintenanceWindowID")]
  [DBForeignKey("MaintenanceWindow", "MaintenanceWindowID")]
  public long MaintenanceWindowID
  {
    get => this._MaintenanceWindowID;
    set => this._MaintenanceWindowID = value;
  }

  [DBProp("OverriddenNote", MaxLength = 2147483647 /*0x7FFFFFFF*/)]
  public string OverriddenNote
  {
    get => this._OverriddenNote;
    set => this._OverriddenNote = value;
  }

  [DBProp("OverriddenSMS", MaxLength = 160 /*0xA0*/, AllowNull = true)]
  public string OverriddenSMS
  {
    get => this._OverriddenSMS;
    set => this._OverriddenSMS = value;
  }

  [DBProp("IsHidden")]
  public bool IsHidden
  {
    get => this._IsHidden;
    set => this._IsHidden = value;
  }

  [DBProp("OverriddenEmail", MaxLength = 2147483647 /*0x7FFFFFFF*/)]
  public string OverriddenEmail
  {
    get => this._OverriddenEmail;
    set => this._OverriddenEmail = value;
  }

  public AccountTheme LoadTheme => AccountTheme.Load(this.AccountThemeID);

  public static AccountThemeMaintenanceLink Load(long id)
  {
    return BaseDBObject.Load<AccountThemeMaintenanceLink>(id);
  }

  public static AccountThemeMaintenanceLink LoadByAccountThemeIDAndMaintenanceID(
    long accountThemeID,
    long maintenanceWindowID)
  {
    return new Monnit.Data.AccountThemeMaintenanceLink.LoadByAccountThemeIDAndMaintenanceID(accountThemeID, maintenanceWindowID).Result;
  }

  public static List<AccountThemeMaintenanceLink> LoadByMaintenanceID(long MaintenanceWindowID)
  {
    return BaseDBObject.LoadByForeignKey<AccountThemeMaintenanceLink>(nameof (MaintenanceWindowID), (object) MaintenanceWindowID);
  }
}
