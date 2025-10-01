// Decompiled with JetBrains decompiler
// Type: Monnit.UnsubscribedNotificationEmail
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("UnsubscribedNotificationEmail")]
public class UnsubscribedNotificationEmail : BaseDBObject
{
  private long _UnsubscribedNotificationEmailID = long.MinValue;
  private string _EmailAddress = string.Empty;
  private DateTime _OptOutDate = DateTime.MinValue;
  private string _UnsubscribedReason = string.Empty;

  [DBProp("UnsubscribedNotificationEmailID", IsPrimaryKey = true)]
  public long UnsubscribedNotificationEmailID
  {
    get => this._UnsubscribedNotificationEmailID;
    set => this._UnsubscribedNotificationEmailID = value;
  }

  [DBProp("EmailAddress", MaxLength = 250, AllowNull = false)]
  public string EmailAddress
  {
    get => this._EmailAddress;
    set => this._EmailAddress = value;
  }

  [DBProp("OptOutDate", AllowNull = false)]
  public DateTime OptOutDate
  {
    get => this._OptOutDate;
    set => this._OptOutDate = value;
  }

  [DBProp("Reason", AllowNull = true)]
  public string Reason
  {
    get => this._UnsubscribedReason;
    set => this._UnsubscribedReason = value;
  }

  public static UnsubscribedNotificationEmail Load(long ID)
  {
    return BaseDBObject.Load<UnsubscribedNotificationEmail>(ID);
  }

  public static UnsubscribedNotificationEmail LoadByEmailAddress(string emailAddress)
  {
    return new Monnit.Data.UnsubscribedNotificationEmail.LoadByEmail(emailAddress).Result.FirstOrDefault<UnsubscribedNotificationEmail>();
  }

  public static void OptIn(string email)
  {
    try
    {
      List<UnsubscribedNotificationEmail> result = new Monnit.Data.UnsubscribedNotificationEmail.LoadByEmail(email).Result;
      for (int index = 0; index < result.Count; ++index)
      {
        UnsubscribedNotificationEmail notificationEmail = result[index];
        notificationEmail.EmailAddress = $"{notificationEmail.EmailAddress}/OptedBackIn={DateTime.UtcNow.ToString()}";
        result[index].Save();
      }
      UnsubscribedNotificationEmail.ClearCachedList();
    }
    catch
    {
    }
  }

  public static void ClearCachedList()
  {
    string key = string.Format("UnsubscribedNotificationEmailList");
    if (!TimedCache.ContainsKey(key))
      return;
    TimedCache.RemoveObject(key);
  }

  public static bool EmailIsUnsubscribed(string emailAddress)
  {
    string key = string.Format("UnsubscribedNotificationEmailList");
    HashSet<string> stringSet = TimedCache.RetrieveObject<HashSet<string>>(key);
    if (stringSet == null)
    {
      List<UnsubscribedNotificationEmail> notificationEmailList = BaseDBObject.LoadAll<UnsubscribedNotificationEmail>();
      stringSet = new HashSet<string>();
      foreach (UnsubscribedNotificationEmail notificationEmail in notificationEmailList)
      {
        if (!stringSet.Contains(notificationEmail.EmailAddress))
          stringSet.Add(notificationEmail.EmailAddress);
      }
      TimedCache.AddObjectToCach(key, (object) stringSet, DateTime.Now.AddMinutes(10.0));
    }
    return stringSet.Contains(emailAddress);
  }
}
