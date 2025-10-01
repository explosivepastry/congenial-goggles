// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APINotificationWeeklySchedule
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;

#nullable disable
namespace iMonnit.API;

public class APINotificationWeeklySchedule
{
  public APINotificationWeeklySchedule()
  {
  }

  public APINotificationWeeklySchedule(Monnit.NotificationSchedule notiSchedule)
  {
    this.DayOfWeek = notiSchedule.DayofWeek.ToString();
    this.FirstEnteredTime = notiSchedule.FirstTime;
    this.SecondEnteredTime = notiSchedule.SecondTime;
    this.NotificationSchedule = notiSchedule.NotificationDaySchedule.ToString();
  }

  public string DayOfWeek { get; set; }

  public TimeSpan FirstEnteredTime { get; set; }

  public TimeSpan SecondEnteredTime { get; set; }

  public string NotificationSchedule { get; set; }
}
