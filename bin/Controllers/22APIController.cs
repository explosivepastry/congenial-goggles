// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APINotificationACK
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;

#nullable disable
namespace iMonnit.API;

public class APINotificationACK
{
  public APINotificationACK()
  {
  }

  public APINotificationACK(NotificationTriggered notiTrig)
  {
    this.NotificationID = notiTrig.NotificationID;
    this.AcknowledgedBy = Customer.Load(notiTrig.AcknowledgedBy).FullName;
    this.AcknowledgedTime = DateTime.SpecifyKind(notiTrig.AcknowledgedTime, DateTimeKind.Utc);
    this.AcknowledgeMethod = notiTrig.AcknowledgeMethod;
    this.ResetTime = notiTrig.resetTime;
    this.AcknowledgedByID = notiTrig.AcknowledgedBy;
  }

  public long NotificationID { get; set; }

  public string AcknowledgedBy { get; set; }

  public DateTime AcknowledgedTime { get; set; }

  public string AcknowledgeMethod { get; set; }

  public DateTime ResetTime { get; set; }

  public long AcknowledgedByID { get; set; }
}
