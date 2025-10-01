// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APINotificationPause
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;

#nullable disable
namespace iMonnit.API;

public class APINotificationPause
{
  public APINotificationPause()
  {
  }

  public APINotificationPause(long deviceID, string deviceType, DateTime resumeNotifyDate)
  {
    this.DeviceID = deviceID;
    this.DeviceType = deviceType;
    this.ResumeNotifyDate = DateTime.SpecifyKind(resumeNotifyDate, DateTimeKind.Utc);
  }

  public long DeviceID { get; set; }

  public string DeviceType { get; set; }

  public DateTime ResumeNotifyDate { get; set; }
}
