// Decompiled with JetBrains decompiler
// Type: Monnit.NotificationSchedule
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit;

[DBClass("NotificationSchedule")]
public class NotificationSchedule : BaseDBObject
{
  private long _NotificationScheduleID = long.MinValue;
  private DayOfWeek _DayOfWeek;
  private eNotificationDaySchedule _NotificationDaySchedule;
  private TimeSpan _FirstTime = TimeSpan.MinValue;
  private TimeSpan _SecondTime = TimeSpan.MinValue;
  private bool _SuppressPropertyChangedEvent = true;

  [DBProp("NotificationScheduleID", IsPrimaryKey = true)]
  public long NotificationScheduleID
  {
    get => this._NotificationScheduleID;
    set => this._NotificationScheduleID = value;
  }

  [DBProp("DayOfWeek", AllowNull = false)]
  public DayOfWeek DayofWeek
  {
    get => this._DayOfWeek;
    set => this._DayOfWeek = value;
  }

  [DBProp("NotificationDaySchedule", AllowNull = true)]
  public eNotificationDaySchedule NotificationDaySchedule
  {
    get => this._NotificationDaySchedule;
    set => this._NotificationDaySchedule = value;
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

  public static NotificationSchedule Load(long ID)
  {
    NotificationSchedule notificationSchedule = BaseDBObject.Load<NotificationSchedule>(ID);
    if (notificationSchedule != null)
      notificationSchedule.SuppressPropertyChangedEvent = false;
    return notificationSchedule;
  }

  public bool SuppressPropertyChangedEvent
  {
    get => this._SuppressPropertyChangedEvent;
    set => this._SuppressPropertyChangedEvent = value;
  }
}
