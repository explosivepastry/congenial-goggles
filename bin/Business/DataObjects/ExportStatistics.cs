// Decompiled with JetBrains decompiler
// Type: Monnit.ExportStatistics
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit;

[DBClass("ExportStatistics")]
public class ExportStatistics : BaseDBObject
{
  private long _ExportStatisticsID = long.MinValue;
  private DateTime _ExportDate = DateTime.MinValue;
  private long _FirstMessageID = long.MinValue;
  private long _LastMessageID = long.MinValue;
  private long _MessageCount = long.MinValue;
  private TimeSpan _QueryTime = TimeSpan.MinValue;
  private TimeSpan _ProcessingTime = TimeSpan.MinValue;
  private DateTime _FirstMessageDate = DateTime.MinValue;
  private DateTime _LastMessageDate = DateTime.MinValue;

  [DBProp("ExportStatisticsID", IsPrimaryKey = true)]
  public long ExportStatisticsID
  {
    get => this._ExportStatisticsID;
    set => this._ExportStatisticsID = value;
  }

  [DBProp("ExportDate", AllowNull = false)]
  public DateTime ExportDate
  {
    get => this._ExportDate;
    set => this._ExportDate = value;
  }

  [DBProp("MessageCount", AllowNull = false)]
  public long MessageCount
  {
    get => this._MessageCount;
    set => this._MessageCount = value;
  }

  [DBProp("QueryTime")]
  public TimeSpan QueryTime
  {
    get => this._QueryTime;
    set => this._QueryTime = value;
  }

  [DBProp("ProcessingTime")]
  public TimeSpan ProcessingTime
  {
    get => this._ProcessingTime;
    set => this._ProcessingTime = value;
  }

  [DBProp("FirstMessageDate", AllowNull = true)]
  public DateTime FirstMessageDate
  {
    get => this._FirstMessageDate;
    set => this._FirstMessageDate = value;
  }

  [DBProp("LastMessageDate", AllowNull = true)]
  public DateTime LastMessageDate
  {
    get => this._LastMessageDate;
    set => this._LastMessageDate = value;
  }
}
