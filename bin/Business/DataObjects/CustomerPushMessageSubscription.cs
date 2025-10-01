// Decompiled with JetBrains decompiler
// Type: Monnit.CustomerPushMessageSubscription
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("CustomerPushMessageSubscription")]
public class CustomerPushMessageSubscription : BaseDBObject
{
  private long _CustomerPushMessageSubscriptionID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private string _EndpointUrl = string.Empty;
  private string _P256DH = string.Empty;
  private string _Auth = string.Empty;
  private DateTime _CreateDate = DateTime.MinValue;
  private Guid _SubscriptionGuid = Guid.Empty;
  private DateTime _LastSentDate = DateTime.MinValue;
  private string _Name = string.Empty;

  [DBProp("CustomerPushMessageSubscriptionID", IsPrimaryKey = true)]
  public long CustomerPushMessageSubscriptionID
  {
    get => this._CustomerPushMessageSubscriptionID;
    set => this._CustomerPushMessageSubscriptionID = value;
  }

  [DBProp("CustomerID", AllowNull = false)]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("EndpointUrl", MaxLength = 500, AllowNull = false)]
  public string EndpointUrl
  {
    get => this._EndpointUrl;
    set => this._EndpointUrl = value;
  }

  [DBProp("P256DH", MaxLength = 255 /*0xFF*/, AllowNull = false)]
  public string P256DH
  {
    get => this._P256DH;
    set => this._P256DH = value;
  }

  [DBProp("Auth", MaxLength = 255 /*0xFF*/, AllowNull = false)]
  public string Auth
  {
    get => this._Auth;
    set => this._Auth = value;
  }

  [DBProp("CreateDate", AllowNull = false)]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("SubscriptionGuid", AllowNull = true)]
  public Guid SubscriptionGuid
  {
    get => this._SubscriptionGuid;
    set => this._SubscriptionGuid = value;
  }

  [DBProp("LastSentDate", AllowNull = true)]
  public DateTime LastSentDate
  {
    get => this._LastSentDate;
    set => this._LastSentDate = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, International = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  public static List<CustomerPushMessageSubscription> All
  {
    get => BaseDBObject.LoadAll<CustomerPushMessageSubscription>();
  }

  public static CustomerPushMessageSubscription Load(long id)
  {
    return BaseDBObject.Load<CustomerPushMessageSubscription>(id);
  }

  public static List<CustomerPushMessageSubscription> LoadByCustomerID(long customerID)
  {
    return BaseDBObject.LoadByForeignKey<CustomerPushMessageSubscription>("CustomerID", (object) customerID);
  }

  public static CustomerPushMessageSubscription LoadByCustomerIDandEndpoint(
    long accountID,
    string endpointUrl)
  {
    return new Monnit.Data.CustomerPushMessageSubscription.LoadByCustomerIDandEndpoint(accountID, endpointUrl).Result;
  }

  public static CustomerPushMessageSubscription LoadByGuid(Guid guid)
  {
    return new Monnit.Data.CustomerPushMessageSubscription.LoadByGuid(guid).Result;
  }
}
