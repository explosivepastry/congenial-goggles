// Decompiled with JetBrains decompiler
// Type: Monnit.Feature
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("Feature")]
public class Feature : BaseDBObject
{
  private long _FeatureID = long.MinValue;
  private string _DisplayName = string.Empty;
  private string _KeyName = string.Empty;
  private string _Description = string.Empty;
  private string _Category = string.Empty;

  [DBProp("FeatureID", IsPrimaryKey = true)]
  public long FeatureID
  {
    get => this._FeatureID;
    set => this._FeatureID = value;
  }

  [DBProp("DisplayName", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string DisplayName
  {
    get => this._DisplayName;
    set => this._DisplayName = value;
  }

  [DBProp("KeyName", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string KeyName
  {
    get => this._KeyName;
    set => this._KeyName = value;
  }

  [DBProp("Description", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [DBProp("Category", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Category
  {
    get => this._Category;
    set => this._Category = value;
  }

  public static Feature Find(string keyName)
  {
    foreach (KeyValuePair<string, Feature> keyValuePair in Feature.LoadAll())
    {
      if (keyValuePair.Key == keyName)
        return keyValuePair.Value;
    }
    return (Feature) null;
  }

  public static Dictionary<string, Feature> LoadAll()
  {
    string key = "FeatureList";
    Dictionary<string, Feature> dictionary = TimedCache.RetrieveObject<Dictionary<string, Feature>>(key);
    if (dictionary == null)
    {
      dictionary = new Dictionary<string, Feature>();
      foreach (Feature feature in BaseDBObject.LoadAll<Feature>())
        dictionary.Add(feature.KeyName, feature);
      TimedCache.AddObjectToCach(key, (object) dictionary);
    }
    return dictionary;
  }

  public static Dictionary<string, Feature> AllowedBySubscriptionType(long subscriptionTypeID)
  {
    string key = "AccountFeatureList_" + subscriptionTypeID.ToString();
    Dictionary<string, Feature> dictionary = TimedCache.RetrieveObject<Dictionary<string, Feature>>(key);
    if (dictionary == null)
    {
      dictionary = new Dictionary<string, Feature>();
      foreach (Feature feature in Feature.LoadBySubscriptionTypeID(subscriptionTypeID))
        dictionary.Add(feature.KeyName, feature);
      TimedCache.AddObjectToCach(key, (object) dictionary);
    }
    return dictionary;
  }

  public static List<Feature> LoadBySubscriptionTypeID(long subscriptionTypeID)
  {
    return new Monnit.Data.Feature.LoadBySubscriptionTypeID(subscriptionTypeID).Result;
  }

  public static Feature Load(long id) => BaseDBObject.Load<Feature>(id);
}
