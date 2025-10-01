// Decompiled with JetBrains decompiler
// Type: Monnit.ChangeLog
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("ChangeLog")]
public class ChangeLog : BaseDBObject
{
  private long _ChangeLogID = long.MinValue;
  private bool _isPublished = false;
  public string _Version = string.Empty;
  private DateTime _ReleaseDate = DateTime.MinValue;

  [DBProp("ChangeLogID", IsPrimaryKey = true)]
  public long ChangeLogID
  {
    get => this._ChangeLogID;
    set => this._ChangeLogID = value;
  }

  [DBProp("isPublished", MaxLength = 50, AllowNull = false)]
  public bool isPublished
  {
    get => this._isPublished;
    set => this._isPublished = value;
  }

  [DBProp("Version", MaxLength = 50)]
  public string Version
  {
    get => this._Version;
    set => this._Version = value;
  }

  [DBProp("ReleaseDate")]
  public DateTime ReleaseDate
  {
    get => this._ReleaseDate;
    set => this._ReleaseDate = value;
  }

  public static ChangeLog Load(long id) => BaseDBObject.Load<ChangeLog>(id);

  public static List<ChangeLog> LoadAll() => BaseDBObject.LoadAll<ChangeLog>().ToList<ChangeLog>();

  public static List<ChangeLog> LoadRecentPublished() => new Monnit.Data.ChangeLog.LoadRecentPublished().Result;
}
