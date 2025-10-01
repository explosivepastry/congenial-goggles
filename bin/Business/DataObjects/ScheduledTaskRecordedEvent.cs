// Decompiled with JetBrains decompiler
// Type: Monnit.ScheduledTaskRecordedEvent
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit;

[DBClass("ScheduledTaskRecordedEvent")]
public class ScheduledTaskRecordedEvent : BaseDBObject
{
  private long _ScheduledTaskRecordedEventID = long.MinValue;
  private long _TableID = long.MinValue;
  private string _TableName = string.Empty;
  private string _EventDescription = string.Empty;
  private DateTime _EventDate = DateTime.MinValue;

  [DBProp("ScheduledTaskRecordedEventID", IsPrimaryKey = true)]
  public long ScheduledTaskRecordedEventID
  {
    get => this._ScheduledTaskRecordedEventID;
    set => this._ScheduledTaskRecordedEventID = value;
  }

  [DBProp("TableID")]
  public long TableID
  {
    get => this._TableID;
    set => this._TableID = value;
  }

  [DBProp("TableName", MaxLength = 255 /*0xFF*/)]
  public new string TableName
  {
    get => this._TableName;
    set
    {
      if (value == null)
        this._TableName = string.Empty;
      else
        this._TableName = value;
    }
  }

  [DBProp("EventDescription", MaxLength = 2000)]
  public string EventDescription
  {
    get => this._EventDescription;
    set
    {
      if (value == null)
        this._EventDescription = string.Empty;
      else
        this._EventDescription = value;
    }
  }

  [DBProp("EventDate")]
  public DateTime EventDate
  {
    get => this._EventDate;
    set => this._EventDate = value;
  }

  public static ScheduledTaskRecordedEvent Load(long ID)
  {
    return BaseDBObject.Load<ScheduledTaskRecordedEvent>(ID);
  }
}
