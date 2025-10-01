// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIUserNotificationSchedule
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;

#nullable disable
namespace iMonnit.API;

public class APIUserNotificationSchedule
{
  public APIUserNotificationSchedule()
  {
  }

  public APIUserNotificationSchedule(CustomerSchedule schedule)
  {
    this.DayOfWeek = schedule.DayofWeek.ToString();
    this.FirstEnteredTime = schedule.FirstTime == TimeSpan.MinValue ? TimeSpan.Zero : schedule.FirstTime;
    this.SecondEnteredTime = schedule.SecondTime == TimeSpan.MinValue ? TimeSpan.Zero : schedule.SecondTime;
    this.NotificationSchedule = schedule.CustomerDaySchedule.ToString();
  }

  public string DayOfWeek { get; set; }

  public TimeSpan FirstEnteredTime { get; set; }

  public TimeSpan SecondEnteredTime { get; set; }

  public string NotificationSchedule { get; set; }
}
