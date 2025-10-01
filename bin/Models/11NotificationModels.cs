// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.NotificationScheduleDisableModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class NotificationScheduleDisableModel
{
  public Notification notification { get; set; }

  public List<NotificationScheduleDisabled> notificationScheduleDisabledList { get; set; }

  public Dictionary<int, Dictionary<int, bool>> dic { get; set; }
}
