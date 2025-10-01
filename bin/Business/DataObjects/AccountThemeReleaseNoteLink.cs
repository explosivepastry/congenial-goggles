// Decompiled with JetBrains decompiler
// Type: Monnit.AccountThemeReleaseNoteLink
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("AccountThemeReleaseNoteLink")]
public class AccountThemeReleaseNoteLink : BaseDBObject
{
  private long _AccountThemeReleaseNoteLinkID = long.MinValue;
  private long _AccountThemeID = long.MinValue;
  private long _ReleaseNoteID = long.MinValue;
  private string _OverriddenNote = string.Empty;
  private bool _IsHidden = false;

  [DBProp("AccountThemeReleaseNoteLinkID", IsPrimaryKey = true)]
  public long AccountThemeReleaseNoteLinkID
  {
    get => this._AccountThemeReleaseNoteLinkID;
    set => this._AccountThemeReleaseNoteLinkID = value;
  }

  [DBProp("AccountThemeID")]
  [DBForeignKey("AccountTheme", "AccountThemeID")]
  public long AccountThemeID
  {
    get => this._AccountThemeID;
    set => this._AccountThemeID = value;
  }

  [DBProp("ReleaseNoteID")]
  [DBForeignKey("ReleaseNote", "ReleaseNoteID")]
  public long ReleaseNoteID
  {
    get => this._ReleaseNoteID;
    set => this._ReleaseNoteID = value;
  }

  [DBProp("OverriddenNote")]
  public string OverriddenNote
  {
    get => this._OverriddenNote;
    set => this._OverriddenNote = value;
  }

  [DBProp("IsHidden")]
  public bool IsHidden
  {
    get => this._IsHidden;
    set => this._IsHidden = value;
  }

  public static List<AccountThemeReleaseNoteLink> LoadByAccountThemeIDAndReleaseNoteID(
    long accountThemeID,
    long ReleaseNoteID)
  {
    return new Monnit.Data.AccountThemeReleaseNoteLink.LoadByAccountThemeIDAndReleaseNoteID(accountThemeID, ReleaseNoteID).Result;
  }

  public static List<AccountThemeReleaseNoteLink> LoadAll()
  {
    return BaseDBObject.LoadAll<AccountThemeReleaseNoteLink>().ToList<AccountThemeReleaseNoteLink>();
  }
}
