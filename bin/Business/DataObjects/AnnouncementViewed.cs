// Decompiled with JetBrains decompiler
// Type: Monnit.AnnouncementViewed
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("AnnouncementViewed")]
public class AnnouncementViewed : BaseDBObject
{
  private long _AnnouncementViewedID = long.MinValue;
  private long _AnnouncementID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private bool _Viewed = false;
  private DateTime _ViewDate = DateTime.MinValue;

  [DBProp("AnnouncementViewedID", IsPrimaryKey = true)]
  public long AnnouncementViewedID
  {
    get => this._AnnouncementViewedID;
    set => this._AnnouncementViewedID = value;
  }

  [DBProp("AnnouncementID", AllowNull = false)]
  [DBForeignKey("Announcement", "AnnouncementID")]
  public long AnnouncementID
  {
    get => this._AnnouncementID;
    set => this._AnnouncementID = value;
  }

  [DBProp("CustomerID", AllowNull = false)]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("Viewed", AllowNull = false)]
  public bool Viewed
  {
    get => this._Viewed;
    set => this._Viewed = value;
  }

  [DBProp("ViewDate", AllowNull = false)]
  public DateTime ViewDate
  {
    get => this._ViewDate;
    set => this._ViewDate = value;
  }

  public static AnnouncementViewed Load(long ID) => BaseDBObject.Load<AnnouncementViewed>(ID);

  public static List<AnnouncementViewed> LoadAll() => BaseDBObject.LoadAll<AnnouncementViewed>();

  public static List<AnnouncementViewed> LoadByCustomerID(long customerID)
  {
    return BaseDBObject.LoadByForeignKey<AnnouncementViewed>("CustomerID", (object) customerID);
  }

  public static List<AnnouncementViewed> LoadAllByAnnouncementID(long announcementID)
  {
    return BaseDBObject.LoadByForeignKey<AnnouncementViewed>("AnnouncementID", (object) announcementID);
  }
}
