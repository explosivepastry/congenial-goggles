// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APINotificationSystemAction
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APINotificationSystemAction
{
  public APINotificationSystemAction()
  {
  }

  public APINotificationSystemAction(
    NotificationRecipient notiRecip,
    string actionName,
    string SerializedRecipientProperties)
  {
    this.DelayMinutes = notiRecip.DelayMinutes < 0 ? 0 : notiRecip.DelayMinutes;
    this.SystemActionRecipientID = notiRecip.NotificationRecipientID;
    this.ActionType = actionName;
    this.TargetNotificationID = SerializedRecipientProperties.ToString();
  }

  public long SystemActionRecipientID { get; set; }

  public string ActionType { get; set; }

  public string TargetNotificationID { get; set; }

  public int DelayMinutes { get; set; }
}
