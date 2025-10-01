// Decompiled with JetBrains decompiler
// Type: Monnit.AccountSubscriptionChangeLog
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("AccountSubscriptionChangeLog")]
public class AccountSubscriptionChangeLog : BaseDBObject
{
  private long _AccountSubscriptionChangeLogID = long.MinValue;
  private long _AccountSubscriptionID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private string _ChangeType = string.Empty;
  private string _ChangeNote = string.Empty;
  private DateTime _OldExpirationDate = DateTime.MinValue;
  private DateTime _NewExpirationDate = DateTime.MinValue;
  private DateTime _ChangeDate = DateTime.UtcNow;
  private Customer _Customer = (Customer) null;

  [DBProp("AccountSubscriptionChangeLogID", IsPrimaryKey = true)]
  public long AccountSubscriptionChangeLogID
  {
    get => this._AccountSubscriptionChangeLogID;
    set => this._AccountSubscriptionChangeLogID = value;
  }

  [DBForeignKey("AccountSubscription", "AccountSubscriptionID")]
  [DBProp("AccountSubscriptionID", AllowNull = false)]
  public long AccountSubscriptionID
  {
    get => this._AccountSubscriptionID;
    set => this._AccountSubscriptionID = value;
  }

  [DBForeignKey("Customer", "CustomerID")]
  [DBProp("CustomerID", AllowNull = false)]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  public Customer Customer
  {
    get
    {
      if (this._Customer == null && this._CustomerID > long.MinValue)
        this._Customer = Customer.Load(this.CustomerID);
      return this._Customer;
    }
  }

  [DBProp("ChangeType", MaxLength = 255 /*0xFF*/)]
  public string ChangeType
  {
    get => this._ChangeType;
    set => this._ChangeType = value;
  }

  [DBProp("ChangeNote", MaxLength = 2000)]
  public string ChangeNote
  {
    get => this._ChangeNote;
    set => this._ChangeNote = value;
  }

  [DBProp("OldExpirationDate")]
  public DateTime OldExpirationDate
  {
    get => this._OldExpirationDate;
    set => this._OldExpirationDate = value;
  }

  [DBProp("NewExpirationDate")]
  public DateTime NewExpirationDate
  {
    get => this._NewExpirationDate;
    set => this._NewExpirationDate = value;
  }

  [DBProp("ChangeDate")]
  public DateTime ChangeDate
  {
    get => this._ChangeDate;
    set => this._ChangeDate = value;
  }

  public static AccountSubscriptionChangeLog Load(long ID)
  {
    return BaseDBObject.Load<AccountSubscriptionChangeLog>(ID);
  }

  public static List<AccountSubscriptionChangeLog> LoadBySubscriptionID(long subscriptionID)
  {
    return BaseDBObject.LoadByForeignKey<AccountSubscriptionChangeLog>("AccountSubscriptionID", (object) subscriptionID);
  }
}
