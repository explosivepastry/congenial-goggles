// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APINotificationControlUnit
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APINotificationControlUnit
{
  public APINotificationControlUnit()
  {
  }

  public APINotificationControlUnit(NotificationRecipient notiRecip)
  {
    this.ControlUnitRecipientID = notiRecip.NotificationRecipientID;
    string[] strArray = notiRecip.SerializedRecipientProperties.Split('|');
    string str1 = string.Empty;
    switch (strArray[0].ToString())
    {
      case "0":
        str1 = "Inactive";
        break;
      case "1":
        str1 = "Off";
        break;
      case "2":
        str1 = "On";
        break;
      case "3":
        str1 = "Toggle";
        break;
    }
    this.Relay1State = str1;
    if (strArray.Length < 3)
    {
      this.Relay1Time = strArray[1].ToString() + " Seconds";
      if (this.Relay1State == "Inactive")
        this.Relay1Time = "NULL";
    }
    else
    {
      this.Relay1Time = strArray[2].ToString() + " Seconds";
      if (this.Relay1State == "Inactive")
        this.Relay1Time = "NULL";
    }
    if (strArray.Length > 2)
    {
      string str2 = string.Empty;
      switch (strArray[1].ToString())
      {
        case "1":
          str2 = "Off";
          break;
        case "2":
          str2 = "On";
          break;
        case "3":
          str2 = "Toggle";
          break;
      }
      this.Relay2State = str2;
      this.Relay2Time = strArray[3].ToString() + " Seconds";
    }
    else
    {
      this.Relay2State = "Inactive";
      this.Relay2Time = "NULL";
    }
  }

  public long ControlUnitRecipientID { get; set; }

  public string Relay1State { get; set; }

  public string Relay1Time { get; set; }

  public string Relay2State { get; set; }

  public string Relay2Time { get; set; }
}
