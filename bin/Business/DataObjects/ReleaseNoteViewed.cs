// Decompiled with JetBrains decompiler
// Type: Monnit.ReleaseNoteViewed
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("ReleaseNoteViewed")]
public class ReleaseNoteViewed : BaseDBObject
{
  private long _ReleaseNoteViewedID = long.MinValue;
  private long _ReleaseNoteID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private bool _Viewed = false;
  private DateTime _ViewDate = DateTime.MinValue;

  [DBProp("ReleaseNoteViewedID", IsPrimaryKey = true)]
  public long ReleaseNoteViewedID
  {
    get => this._ReleaseNoteViewedID;
    set => this._ReleaseNoteViewedID = value;
  }

  [DBProp("ReleaseNoteID")]
  [DBForeignKey("ReleaseNote", "ReleaseNoteID")]
  public long ReleaseNoteID
  {
    get => this._ReleaseNoteID;
    set => this._ReleaseNoteID = value;
  }

  [DBProp("CustomerID")]
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

  public static ReleaseNote Load(long ID) => BaseDBObject.Load<ReleaseNote>(ID);

  public static List<ReleaseNoteViewed> LoadAll() => BaseDBObject.LoadAll<ReleaseNoteViewed>();

  internal static List<ReleaseNoteViewed> LoadByCustomerID(long customerID)
  {
    return BaseDBObject.LoadByForeignKey<ReleaseNoteViewed>("CustomerID", (object) customerID);
  }
}
