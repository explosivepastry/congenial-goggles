// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APISentNotification
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;

#nullable disable
namespace iMonnit.API;

public class APISentNotification
{
  public APISentNotification()
  {
  }

  public APISentNotification(NotificationRecorded notiRec)
  {
    Notification notification = Notification.Load(notiRec.NotificationID);
    this.SentNotificationID = notiRec.NotificationRecordedID;
    this.NotificationID = notiRec.NotificationID;
    this.UserID = notiRec.CustomerID == long.MinValue ? 0L : notiRec.CustomerID;
    this.SensorID = notiRec.SensorID == long.MinValue ? 0L : notiRec.SensorID;
    this.GatewayID = notiRec.GatewayID == long.MinValue ? 0L : notiRec.GatewayID;
    this.Name = notification.Name;
    this.Text = notiRec.NotificationText;
    this.Content = Notification.UnFormat(notiRec.NotificationContent);
    this.Status = notiRec.Status;
    this.NotificationDate = DateTime.SpecifyKind(notiRec.NotificationDate, DateTimeKind.Utc);
    NotificationTriggered notificationTriggered = NotificationTriggered.LoadByNotificationRecordedID(notiRec.NotificationRecordedID);
    if (notification.IsActive)
    {
      if (notificationTriggered != null)
      {
        if (notificationTriggered.AcknowledgedTime == DateTime.MinValue)
          this.NotificationStatus = "Alarming";
        else
          this.NotificationStatus = !(notificationTriggered.resetTime > DateTime.MinValue) ? "Acknowledged" : "Armed";
      }
      else
        this.NotificationStatus = "Armed";
    }
    else
      this.NotificationStatus = "Not Active";
  }

  public long SentNotificationID { get; set; }

  public long NotificationID { get; set; }

  public long UserID { get; set; }

  public long SensorID { get; set; }

  public long GatewayID { get; set; }

  public string Name { get; set; }

  public string Text { get; set; }

  public string Content { get; set; }

  public string Status { get; set; }

  public DateTime NotificationDate { get; set; }

  public string NotificationStatus { get; set; }
}
