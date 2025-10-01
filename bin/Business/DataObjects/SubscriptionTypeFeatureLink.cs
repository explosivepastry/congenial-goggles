// Decompiled with JetBrains decompiler
// Type: Monnit.SubscriptionTypeFeatureLink
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;

#nullable disable
namespace Monnit;

[DBClass("SubscriptionTypeFeatureLink")]
public class SubscriptionTypeFeatureLink : BaseDBObject
{
  private long _SubscriptionTypeFeatureLinkID = long.MinValue;
  private long _AccountSubscriptionTypeID = long.MinValue;
  private long _FeatureID = long.MinValue;
  private string _Tags = string.Empty;

  [DBProp("SubscriptionTypeFeatureLinkID", IsPrimaryKey = true)]
  public long SubscriptionTypeFeatureLinkID
  {
    get => this._SubscriptionTypeFeatureLinkID;
    set => this._SubscriptionTypeFeatureLinkID = value;
  }

  [DBProp("AccountSubscriptionTypeID")]
  [DBForeignKey("AccountSubscriptionType", "AccountSubscriptionTypeID")]
  public long AccountSubscriptionTypeID
  {
    get => this._AccountSubscriptionTypeID;
    set => this._AccountSubscriptionTypeID = value;
  }

  [DBProp("FeatureID")]
  [DBForeignKey("Feature", "FeatureID")]
  public long FeatureID
  {
    get => this._FeatureID;
    set => this._FeatureID = value;
  }

  [DBProp("Tags", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Tags
  {
    get => this._Tags;
    set => this._Tags = value;
  }

  public static SubscriptionTypeFeatureLink Load(long id)
  {
    return BaseDBObject.Load<SubscriptionTypeFeatureLink>(id);
  }
}
