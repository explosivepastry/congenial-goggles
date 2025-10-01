// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APINotification
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;
using System.Linq;
using System.Net;

#nullable disable
namespace iMonnit.API;

public class APINotification
{
  public APINotification()
  {
  }

  public APINotification(Notification noti)
  {
    this.AccountID = noti.AccountID;
    this.NotificationID = noti.NotificationID;
    this.Name = WebUtility.HtmlDecode(noti.Name);
    this.Subject = WebUtility.HtmlDecode(noti.Subject);
    this.Text = WebUtility.HtmlDecode(noti.NotificationText);
    this.SMSText = WebUtility.HtmlDecode(noti.SMSText);
    this.VoiceText = WebUtility.HtmlDecode(noti.VoiceText);
    this.PushMsgText = WebUtility.HtmlDecode(noti.PushMsgText);
    this.Active = noti.IsActive;
    this.Comparer = noti.CompareType.ToString();
    this.Threshold = noti.CompareValue;
    this.Scale = noti.Scale;
    this.Snooze = noti.SnoozeDuration;
    this.LastDateSent = DateTime.SpecifyKind(noti.LastSent, DateTimeKind.Utc);
    this.DatumType = noti.eDatumType.ToString() == "Error" ? "" : noti.eDatumType.ToString();
    this.AdvancedNotificationID = noti.AdvancedNotificationID;
    this.NotificationClass = noti.NotificationClass.ToString();
    this.JointSnooze = noti.ApplySnoozeByTriggerDevice;
    this.AutoAcknowledge = noti.CanAutoAcknowledge;
    this.AcknowledgedBy = "";
    this.AcknowledgedTime = DateTime.MinValue;
    this.AcknowledgeMethod = "";
    this.ResetTime = DateTime.MinValue;
    NotificationTriggered notificationTriggered = NotificationTriggered.LoadLastByNotificationID(noti.NotificationID, 1).FirstOrDefault<NotificationTriggered>();
    if (notificationTriggered != null)
    {
      if (notificationTriggered.AcknowledgedTime == DateTime.MinValue)
      {
        this.AcknowledgedBy = "";
        this.AcknowledgedByID = -1L;
      }
      else
      {
        if (notificationTriggered.AcknowledgedBy == long.MinValue)
        {
          this.AcknowledgedBy = string.IsNullOrEmpty(notificationTriggered.AcknowledgeMethod) ? "System_Auto" : notificationTriggered.AcknowledgeMethod;
          this.AcknowledgedByID = 0L;
        }
        else
        {
          this.AcknowledgedBy = Customer.Load(notificationTriggered.AcknowledgedBy).FullName;
          this.AcknowledgedByID = notificationTriggered.AcknowledgedBy;
        }
        this.AcknowledgedTime = DateTime.SpecifyKind(notificationTriggered.AcknowledgedTime, DateTimeKind.Utc);
      }
      this.AcknowledgeMethod = notificationTriggered.AcknowledgeMethod;
      this.ResetTime = notificationTriggered.resetTime;
    }
    if (noti.IsActive)
    {
      if (notificationTriggered != null)
      {
        if (notificationTriggered.AcknowledgedTime == DateTime.MinValue)
          this.Status = "Alarming";
        else
          this.Status = !(notificationTriggered.resetTime > DateTime.MinValue) ? "Acknowledged" : "Armed";
      }
      else
        this.Status = "Armed";
    }
    else
      this.Status = "Not Active";
  }

  public long NotificationID { get; set; }

  public string Name { get; set; }

  public string Subject { get; set; }

  public string Text { get; set; }

  public string SMSText { get; set; }

  public string VoiceText { get; set; }

  public string PushMsgText { get; set; }

  public long AccountID { get; set; }

  public bool Active { get; set; }

  public bool JointSnooze { get; set; }

  public bool AutoAcknowledge { get; set; }

  public string Comparer { get; set; }

  public string Threshold { get; set; }

  public string Scale { get; set; }

  public int Snooze { get; set; }

  public DateTime LastDateSent { get; set; }

  public string DatumType { get; set; }

  public long AdvancedNotificationID { get; set; }

  public string NotificationClass { get; set; }

  public string Status { get; set; }

  public string AcknowledgedBy { get; set; }

  public long AcknowledgedByID { get; set; }

  public DateTime AcknowledgedTime { get; set; }

  public string AcknowledgeMethod { get; set; }

  public DateTime ResetTime { get; set; }
}
