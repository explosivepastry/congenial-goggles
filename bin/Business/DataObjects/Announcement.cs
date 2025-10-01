// Decompiled with JetBrains decompiler
// Type: Monnit.Announcement
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

[DBClass("Announcement")]
public class Announcement : BaseDBObject
{
  private long _AnnouncementID = long.MinValue;
  private long _AccountThemeID = long.MinValue;
  private string _Subject = string.Empty;
  private string _Title = string.Empty;
  private string _Content = string.Empty;
  private string _Image = string.Empty;
  private string _Link = string.Empty;
  private DateTime _ReleaseDate = DateTime.MinValue;
  private bool _IsDeleted = false;

  [DBProp("AnnouncementID", IsPrimaryKey = true)]
  public long AnnouncementID
  {
    get => this._AnnouncementID;
    set => this._AnnouncementID = value;
  }

  [DBForeignKey("AccountTheme", "AccountThemeID")]
  [DBProp("AccountThemeID", AllowNull = true)]
  public long AccountThemeID
  {
    get => this._AccountThemeID;
    set => this._AccountThemeID = value;
  }

  [AllowHtml]
  [DBProp("Subject", AllowNull = false, MaxLength = 255 /*0xFF*/)]
  public string Subject
  {
    get => this._Subject;
    set => this._Subject = value;
  }

  [DBProp("Title", AllowNull = false, MaxLength = 255 /*0xFF*/)]
  public string Title
  {
    get => this._Title;
    set => this._Title = value;
  }

  [AllowHtml]
  [DBProp("Content", AllowNull = false, MaxLength = 8000)]
  public string Content
  {
    get => this._Content;
    set => this._Content = value;
  }

  [AllowHtml]
  [DBProp("Image", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string Image
  {
    get => this._Image;
    set => this._Image = value;
  }

  [AllowHtml]
  [DBProp("Link", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string Link
  {
    get => this._Link;
    set => this._Link = value;
  }

  [DBProp("ReleaseDate", AllowNull = false)]
  public DateTime ReleaseDate
  {
    get => this._ReleaseDate;
    set => this._ReleaseDate = value;
  }

  [DBProp("IsDeleted", AllowNull = false, DefaultValue = false)]
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

  public static Announcement Load(long ID) => BaseDBObject.Load<Announcement>(ID);

  public static List<Announcement> LoadAll()
  {
    return BaseDBObject.LoadAll<Announcement>().ToList<Announcement>();
  }

  public static DataTable MarkReadByCustomerID(long customerID, long accountThemeID)
  {
    return new Monnit.Data.Announcement.MarkReadByCustomerID(customerID, accountThemeID).Result;
  }

  public static AnnouncementPopup LoadByCustomerIDProc(
    long customerID,
    long accountThemeID,
    long announcementid)
  {
    return new Monnit.Data.Announcement.LoadByCustomerID(customerID, accountThemeID, announcementid).Result;
  }

  public static List<Announcement> LoadByThemeID(long id)
  {
    return BaseDBObject.LoadByForeignKey<Announcement>("AccountThemeID", (object) id);
  }
}
