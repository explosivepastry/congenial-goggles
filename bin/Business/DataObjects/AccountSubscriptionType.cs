// Decompiled with JetBrains decompiler
// Type: Monnit.AccountSubscriptionType
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("AccountSubscriptionType")]
public class AccountSubscriptionType : BaseDBObject
{
  private long _AccountSubscriptionTypeID = long.MinValue;
  private string _Name = string.Empty;
  private string _KeyType = string.Empty;
  private int _AllowedSensors = int.MinValue;
  private int _Rank = int.MinValue;
  private string _Tags = string.Empty;
  public const long BasicID = 1;
  public const long TrialID = 10;

  [DBProp("AccountSubscriptionTypeID", IsPrimaryKey = true)]
  public long AccountSubscriptionTypeID
  {
    get => this._AccountSubscriptionTypeID;
    set => this._AccountSubscriptionTypeID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("KeyType", MaxLength = 255 /*0xFF*/, AllowNull = false)]
  public string KeyType
  {
    get => this._KeyType;
    set => this._KeyType = value;
  }

  [DBProp("AllowedSensors")]
  public int AllowedSensors
  {
    get => this._AllowedSensors;
    set => this._AllowedSensors = value;
  }

  [DBProp("Rank")]
  public int Rank
  {
    get => this._Rank;
    set => this._Rank = value;
  }

  [DBProp("Tags", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Tags
  {
    get => this._Tags;
    set => this._Tags = value;
  }

  public string[] TagArray
  {
    get => this.Tags.Split(new char[1]{ '|' }, StringSplitOptions.RemoveEmptyEntries);
  }

  public void AddTag(string tag)
  {
    this.Tags = $"{this.Tags}|{tag}";
    this.Save();
  }

  public void RemoveTag(string tag)
  {
    string[] tagArray = this.TagArray;
    this.Tags = string.Empty;
    foreach (string str in tagArray)
    {
      if (str.ToLower().Trim() != tag.ToLower().Trim())
        this.Tags = $"{this.Tags}|{str}";
    }
    this.Save();
  }

  public bool HasTag(string tag)
  {
    foreach (string tag1 in this.TagArray)
    {
      if (tag1.ToLower().Trim() == tag.ToLower().Trim())
        return true;
    }
    return false;
  }

  public string SubscriptionLink
  {
    get
    {
      switch (this.AllowedSensors)
      {
        case 6:
          return "https://www.monnit.com/Product/MNW-IP-006";
        case 12:
          return "https://www.monnit.com/Product/MNW-IP-012";
        case 25:
          return "https://www.monnit.com/Product/MNW-IP-025";
        case 50:
          return "https://www.monnit.com/Product/MNW-IP-050";
        case 100:
          return "https://www.monnit.com/Product/MNW-IP-100";
        case 500:
          return "https://www.monnit.com/Product/MNW-IP-500";
        case 999:
          return "https://www.monnit.com/Product/MNW-IP-999";
        default:
          return "https://www.monnit.com/Product/MNW-IP-006";
      }
    }
  }

  public static AccountSubscriptionType LoadByKeyType(string keyType)
  {
    return new Monnit.Data.AccountSubscriptionType.LoadByKeyType(keyType).Result;
  }

  public bool Can(string keyName)
  {
    Dictionary<string, Feature> dictionary = Feature.AllowedBySubscriptionType(this.AccountSubscriptionTypeID);
    return dictionary != null && dictionary.ContainsKey(keyName);
  }

  public static AccountSubscriptionType Load(long id)
  {
    return BaseDBObject.Load<AccountSubscriptionType>(id);
  }

  public static List<AccountSubscriptionType> LoadAll()
  {
    string key = "AccountSubscriptionTypeList";
    List<AccountSubscriptionType> subscriptionTypeList = TimedCache.RetrieveObject<List<AccountSubscriptionType>>(key);
    if (subscriptionTypeList == null)
    {
      subscriptionTypeList = BaseDBObject.LoadAll<AccountSubscriptionType>();
      if (subscriptionTypeList != null)
        TimedCache.AddObjectToCach(key, (object) subscriptionTypeList, new TimeSpan(2, 0, 0));
    }
    return subscriptionTypeList;
  }

  public static List<AccountSubscriptionType> LoadAllowedForReseller()
  {
    string key = "ResellerAccountSubscriptionTypeList";
    List<AccountSubscriptionType> subscriptionTypeList = TimedCache.RetrieveObject<List<AccountSubscriptionType>>(key);
    if (subscriptionTypeList == null)
    {
      subscriptionTypeList = BaseDBObject.LoadAll<AccountSubscriptionType>().Where<AccountSubscriptionType>((Func<AccountSubscriptionType, bool>) (m => m.HasTag("reseller"))).ToList<AccountSubscriptionType>();
      if (subscriptionTypeList != null)
        TimedCache.AddObjectToCach(key, (object) subscriptionTypeList, new TimeSpan(2, 0, 0));
    }
    return subscriptionTypeList;
  }
}
