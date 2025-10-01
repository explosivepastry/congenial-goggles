// Decompiled with JetBrains decompiler
// Type: Monnit.CustomerSchedule
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit;

[DBClass("CustomerSchedule")]
public class CustomerSchedule : BaseDBObject
{
  private long _CustomerScheduleID = long.MinValue;
  private DayOfWeek _DayOfWeek;
  private eNotificationDaySchedule _CustomerDaySchedule;
  private TimeSpan _FirstTime = TimeSpan.MinValue;
  private TimeSpan _SecondTime = TimeSpan.MinValue;
  private bool _SuppressPropertyChangedEvent = true;

  [DBProp("CustomerScheduleID", IsPrimaryKey = true)]
  public long CustomerScheduleID
  {
    get => this._CustomerScheduleID;
    set => this._CustomerScheduleID = value;
  }

  [DBProp("DayOfWeek", AllowNull = false)]
  public DayOfWeek DayofWeek
  {
    get => this._DayOfWeek;
    set => this._DayOfWeek = value;
  }

  [DBProp("CustomerDaySchedule", AllowNull = true)]
  public eNotificationDaySchedule CustomerDaySchedule
  {
    get => this._CustomerDaySchedule;
    set => this._CustomerDaySchedule = value;
  }

  [DBProp("FirstTime", AllowNull = true)]
  public TimeSpan FirstTime
  {
    get => this._FirstTime;
    set => this._FirstTime = value;
  }

  [DBProp("SecondTime", AllowNull = true)]
  public TimeSpan SecondTime
  {
    get => this._SecondTime;
    set => this._SecondTime = value;
  }

  public static CustomerSchedule Load(long ID)
  {
    CustomerSchedule customerSchedule = BaseDBObject.Load<CustomerSchedule>(ID);
    if (customerSchedule != null)
      customerSchedule.SuppressPropertyChangedEvent = false;
    return customerSchedule;
  }

  public bool SuppressPropertyChangedEvent
  {
    get => this._SuppressPropertyChangedEvent;
    set => this._SuppressPropertyChangedEvent = value;
  }
}
