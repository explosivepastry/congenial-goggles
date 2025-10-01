// Decompiled with JetBrains decompiler
// Type: Monnit.AccountThemeApplicationLink
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;

#nullable disable
namespace Monnit;

[DBClass("AccountThemeApplicationLink")]
public class AccountThemeApplicationLink : BaseDBObject
{
  private long _AccountThemeApplicationLinkID = long.MinValue;
  private long _AccountThemeID = long.MinValue;
  private long _MonnitApplicationID = long.MinValue;
  private string _Alias = string.Empty;

  [DBProp("AccountThemeApplicationLinkID", IsPrimaryKey = true)]
  public long AccountThemeApplicationLinkID
  {
    get => this._AccountThemeApplicationLinkID;
    set => this._AccountThemeApplicationLinkID = value;
  }

  [DBProp("AccountThemeID")]
  [DBForeignKey("AccountTheme", "AccountThemeID")]
  public long AccountThemeID
  {
    get => this._AccountThemeID;
    set => this._AccountThemeID = value;
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

  [DBProp("Alias", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Alias
  {
    get => this._Alias;
    set => this._Alias = value;
  }

  public static AccountThemeApplicationLink Load(long id)
  {
    return BaseDBObject.Load<AccountThemeApplicationLink>(id);
  }
}
