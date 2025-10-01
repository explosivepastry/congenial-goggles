// Decompiled with JetBrains decompiler
// Type: Monnit.ReleaseNote
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

#nullable disable
namespace Monnit;

[DBClass("ReleaseNote")]
public class ReleaseNote : BaseDBObject
{
  public Dictionary<long, AccountThemeReleaseNoteLink> dic = new Dictionary<long, AccountThemeReleaseNoteLink>();
  private long _ReleaseNoteID = long.MinValue;
  private string _Version = string.Empty;
  private string _Note = string.Empty;
  private string _Description = string.Empty;
  private DateTime _ReleaseDate = DateTime.MinValue;
  private long _AccountThemeID = long.MinValue;
  private string _Subject = string.Empty;
  private string _Image = string.Empty;
  private string _Link = string.Empty;
  private DateTime _UpdateReleaseDate;
  private bool _IsDeleted = false;

  [DBProp("ReleaseNoteID", IsPrimaryKey = true)]
  public long ReleaseNoteID
  {
    get => this._ReleaseNoteID;
    set => this._ReleaseNoteID = value;
  }

  [DBProp("Version", AllowNull = false, MaxLength = 255 /*0xFF*/)]
  public string Version
  {
    get => this._Version;
    set => this._Version = value;
  }

  [AllowHtml]
  [DBProp("Note", AllowNull = false, MaxLength = 8000)]
  public string Note
  {
    get => this._Note;
    set => this._Note = value;
  }

  [DBProp("Description", AllowNull = true, MaxLength = 8000)]
  public string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [DBProp("ReleaseDate", AllowNull = false)]
  public DateTime ReleaseDate
  {
    get => this._ReleaseDate;
    set => this._ReleaseDate = value;
  }

  [DBProp("UpdateReleaseDate", AllowNull = true)]
  public DateTime UpdateReleaseDate
  {
    get => this._UpdateReleaseDate;
    set => this._UpdateReleaseDate = value;
  }

  [DBForeignKey("AccountTheme", "AccountThemeID")]
  [DBProp("AccountThemeID", AllowNull = true)]
  public long AccountThemeID
  {
    get => this._AccountThemeID;
    set => this._AccountThemeID = value;
  }

  [AllowHtml]
  [DBProp("Subject", AllowNull = true, MaxLength = 500)]
  public string Subject
  {
    get => this._Subject;
    set => this._Subject = value;
  }

  [AllowHtml]
  [DBProp("Image", AllowNull = true, MaxLength = 500)]
  public string Image
  {
    get => this._Image;
    set => this._Image = value;
  }

  [AllowHtml]
  [DBProp("Link", AllowNull = true, MaxLength = 500)]
  public string Link
  {
    get => this._Link;
    set => this._Link = value;
  }

  [DBProp("IsDeleted", DefaultValue = false)]
  public bool IsDeleted
  {
    get => this._IsDeleted;
    set => this._IsDeleted = value;
  }

  public override void Delete()
  {
    this.IsDeleted = true;
    this.Save();
  }

  public static ReleaseNote Load(long ID) => BaseDBObject.Load<ReleaseNote>(ID);

  public static List<ReleaseNote> LoadAll()
  {
    return BaseDBObject.LoadAll<ReleaseNote>().ToList<ReleaseNote>();
  }

  public static DataTable LoadAllBasedOnReleaseDateAndThemeID(long accountThemeID, long customerID)
  {
    return new Monnit.Data.ReleaseNote.LoadAllBasedOnReleaseDateAndThemeID(accountThemeID, customerID).Result;
  }

  public static List<ReleaseNote> LoadAllReleaseNotesByVersionTest(
    System.Version version,
    DateTime dateTime)
  {
    return new Monnit.Data.ReleaseNote.LoadAllReleaseNotesByDateAndVersion(version, dateTime, true).Result;
  }

  public static List<ReleaseNote> LoadAllReleaseNotesByDateAndVersion(
    System.Version version,
    DateTime dateTime)
  {
    List<ReleaseNote> releaseNoteList = TimedCache.RetrieveObject<List<ReleaseNote>>("CachedReleaseNoteByDateAndVersion");
    if (releaseNoteList == null || releaseNoteList.Count == 0)
    {
      releaseNoteList = new Monnit.Data.ReleaseNote.LoadAllReleaseNotesByDateAndVersion(version, dateTime, false).Result;
      TimedCache.AddObjectToCach("CachedReleaseNoteByDateAndVersion", (object) releaseNoteList, new TimeSpan(1, 0, 0));
    }
    return releaseNoteList;
  }

  public static List<ReleaseNote> LoadActiveReleaseNotesByDateAndVersion(
    System.Version version,
    DateTime dateTime)
  {
    List<ReleaseNote> releaseNoteList = TimedCache.RetrieveObject<List<ReleaseNote>>("CachedReleaseNoteByDateAndVersion");
    if (releaseNoteList == null || releaseNoteList.Count == 0)
    {
      releaseNoteList = new Monnit.Data.ReleaseNote.LoadActiveReleaseNotesByDateAndVersion(version, dateTime, false).Result;
      TimedCache.AddObjectToCach("CachedReleaseNoteByDateAndVersion", (object) releaseNoteList, new TimeSpan(1, 0, 0));
    }
    return releaseNoteList;
  }
}
