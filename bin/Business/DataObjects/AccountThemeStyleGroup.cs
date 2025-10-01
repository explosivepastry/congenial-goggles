// Decompiled with JetBrains decompiler
// Type: Monnit.AccountThemeStyleGroup
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("AccountThemeStyleGroup")]
public class AccountThemeStyleGroup : BaseDBObject
{
  private long _AccountThemeStyleGroupID = long.MinValue;
  private long _AccountThemeID = long.MinValue;
  private string _Name = string.Empty;
  private bool _IsActive = true;
  private List<AccountThemeStyle> _Styles = (List<AccountThemeStyle>) null;

  [DBProp("AccountThemeStyleGroupID", IsPrimaryKey = true)]
  public long AccountThemeStyleGroupID
  {
    get => this._AccountThemeStyleGroupID;
    set => this._AccountThemeStyleGroupID = value;
  }

  [DBProp("AccountThemeID")]
  [DBForeignKey("AccountTheme", "AccountThemeID")]
  public long AccountThemeID
  {
    get => this._AccountThemeID;
    set => this._AccountThemeID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = false)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("IsActive", AllowNull = false, DefaultValue = false)]
  public bool IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  public List<AccountThemeStyle> Styles
  {
    get
    {
      if (this._Styles == null)
        this._Styles = AccountThemeStyle.LoadByAccountThemeStyleGroupID(this.AccountThemeStyleGroupID);
      return this._Styles;
    }
  }

  public static AccountThemeStyleGroup Load(long id)
  {
    return BaseDBObject.Load<AccountThemeStyleGroup>(id);
  }

  public static List<AccountThemeStyleGroup> LoadByAccountThemeID(
    long accountThemeID,
    bool IncludeInActive)
  {
    return new Monnit.Data.AccountThemeStyleGroup.LoadByAccountThemeID(accountThemeID, IncludeInActive).Result;
  }
}
