// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIAdvancedNotificationParameterValue
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APIAdvancedNotificationParameterValue
{
  public APIAdvancedNotificationParameterValue()
  {
  }

  public APIAdvancedNotificationParameterValue(AdvancedNotificationParameterValue value)
  {
    this.AdvancedNotificationParameterID = value.AdvancedNotificationParameterID;
    this.NotificationID = value.NotificationID;
    this.ParameterValue = value.ParameterValue;
    this.ParameterDescription = value.DBParameter.ToString();
  }

  public long NotificationID { get; set; }

  public long AdvancedNotificationParameterID { get; set; }

  public string ParameterValue { get; set; }

  public string ParameterDescription { get; set; }
}
