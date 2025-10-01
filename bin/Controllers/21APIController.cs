// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APINotificationSettings
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APINotificationSettings
{
  public APINotificationSettings()
  {
  }

  public APINotificationSettings(ExternalSubscriptionPreference pref)
  {
    this.UserToBeNotified = Customer.Load(pref.UserId).FullName;
    this.UsersBrokenCountLimit = pref.UsersBrokenCountLimit;
    this.LastEmailDate = pref.LastEmailDate.ToLocalDateTimeShort();
    this.LargestBrokenCount = pref.LargestBrokenCount();
  }

  public string UserToBeNotified { get; set; }

  public int UsersBrokenCountLimit { get; set; }

  public string LastEmailDate { get; set; }

  public string LargestBrokenCount { get; set; }
}
