// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APINotificationYearSchedule
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

#nullable disable
namespace iMonnit.API;

public class APINotificationYearSchedule
{
  public APINotificationYearSchedule()
  {
  }

  public APINotificationYearSchedule(int month, int day, bool send)
  {
    this.Month = month;
    this.Day = day;
    this.SendNotifications = send;
  }

  public int Month { get; set; }

  public int Day { get; set; }

  public bool SendNotifications { get; set; }
}
