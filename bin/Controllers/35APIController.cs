// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIAdvancedNotificationAutomatedSchedule
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;

#nullable disable
namespace iMonnit.API;

public class APIAdvancedNotificationAutomatedSchedule
{
  public APIAdvancedNotificationAutomatedSchedule()
  {
  }

  public APIAdvancedNotificationAutomatedSchedule(AutomatedNotification auto)
  {
    this.NotificationID = auto.NotificationID;
    this.AdvancedNotificationID = auto.ExternalID;
    this.ProcessFrequency = auto.ProcessFrequency;
    this.Description = auto.Description;
    this.LastProcessDate = auto.LastProcessDate;
  }

  public long NotificationID { get; set; }

  public long AdvancedNotificationID { get; set; }

  public int ProcessFrequency { get; set; }

  public string Description { get; set; }

  public DateTime LastProcessDate { get; set; }
}
