// Decompiled with JetBrains decompiler
// Type: Monnit.AccountThemeContact
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace Monnit;

[DBClass("AccountThemeContact")]
public class AccountThemeContact : BaseDBObject
{
  private long _AccountThemeContactID = long.MinValue;
  private long _AccountThemeID = long.MinValue;
  private string _FirstName = string.Empty;
  private string _LastName = string.Empty;
  private string _Email = string.Empty;
  private string _Other = string.Empty;

  [DBProp("AccountThemeContactID", IsPrimaryKey = true)]
  public long AccountThemeContactID
  {
    get => this._AccountThemeContactID;
    set => this._AccountThemeContactID = value;
  }

  [DBProp("AccountThemeID")]
  [DBForeignKey("AccountTheme", "AccountThemeID")]
  public long AccountThemeID
  {
    get => this._AccountThemeID;
    set => this._AccountThemeID = value;
  }

  [Required]
  [DBProp("FirstName", MaxLength = 255 /*0xFF*/, AllowNull = false)]
  public string FirstName
  {
    get => this._FirstName;
    set => this._FirstName = value;
  }

  [Required]
  [DBProp("LastName", MaxLength = 255 /*0xFF*/, AllowNull = false)]
  public string LastName
  {
    get => this._LastName;
    set => this._LastName = value;
  }

  [Required]
  [DataType(DataType.EmailAddress)]
  [EmailAddress]
  [DBProp("Email", MaxLength = 255 /*0xFF*/, AllowNull = false)]
  public string Email
  {
    get => this._Email;
    set => this._Email = value;
  }

  [DBProp("Other", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Other
  {
    get => this._Other;
    set => this._Other = value;
  }

  public static AccountThemeContact Load(long id) => BaseDBObject.Load<AccountThemeContact>(id);

  public static List<AccountThemeContact> LoadByAccountThemeID(long accountThemeID)
  {
    return BaseDBObject.LoadByForeignKey<AccountThemeContact>("AccountThemeID", (object) accountThemeID);
  }
}
