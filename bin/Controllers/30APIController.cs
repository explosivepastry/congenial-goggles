// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APINotificationRecipient
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APINotificationRecipient
{
  public APINotificationRecipient()
  {
  }

  public APINotificationRecipient(NotificationRecipient notiRecip)
  {
    this.UserID = notiRecip.CustomerToNotifyID;
    this.Type = notiRecip.NotificationType.ToString();
    this.DelayMinutes = notiRecip.DelayMinutes < 0 ? 0 : notiRecip.DelayMinutes;
    this.RecipientID = notiRecip.NotificationRecipientID;
  }

  public long UserID { get; set; }

  public string Type { get; set; }

  public int DelayMinutes { get; set; }

  public long RecipientID { get; set; }
}
