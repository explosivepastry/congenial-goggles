// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APINotificationLocalNotifier
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APINotificationLocalNotifier
{
  public APINotificationLocalNotifier()
  {
  }

  public APINotificationLocalNotifier(NotificationRecipient notiRecip)
  {
    this.LocalNotifierRecipientID = notiRecip.NotificationRecipientID;
    string[] strArray = notiRecip.SerializedRecipientProperties.Split('|');
    this.LED_ON = strArray[0].ToString();
    this.Buzzer_ON = strArray[1].ToString();
    this.AutoScroll_ON = strArray[2].ToString();
    this.BackLight_ON = strArray[3].ToString();
    this.NotifierName = strArray[4].ToString();
  }

  public long LocalNotifierRecipientID { get; set; }

  public string LED_ON { get; set; }

  public string Buzzer_ON { get; set; }

  public string AutoScroll_ON { get; set; }

  public string BackLight_ON { get; set; }

  public string NotifierName { get; set; }
}
