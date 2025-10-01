// Decompiled with JetBrains decompiler
// Type: Monnit.CustomerRememberMe
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("CustomerRememberMe")]
public class CustomerRememberMe : BaseDBObject
{
  private long _CustomerCustomerRememberMeID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private int _SequenceNumber = int.MinValue;
  private int _Recent = int.MinValue;
  private DateTime _LastUpdated = DateTime.UtcNow;

  [DBProp("CustomerCustomerRememberMeID", IsPrimaryKey = true)]
  public long CustomerCustomerRememberMeID
  {
    get => this._CustomerCustomerRememberMeID;
    set => this._CustomerCustomerRememberMeID = value;
  }

  [DBForeignKey("Customer", "CustomerID")]
  [DBProp("CustomerID", AllowNull = false)]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("SequenceNumber")]
  public int SequenceNumber
  {
    get => this._SequenceNumber;
    set => this._SequenceNumber = value;
  }

  [DBProp("Recent")]
  public int Recent
  {
    get => this._Recent;
    set => this._Recent = value;
  }

  [DBProp("LastUpdated")]
  public DateTime LastUpdated
  {
    get => this._LastUpdated;
    set => this._LastUpdated = value;
  }

  public static CustomerRememberMe Create(long customerID)
  {
    CustomerRememberMe customerRememberMe = new CustomerRememberMe();
    Random random = new Random(DateTime.Now.Millisecond);
    customerRememberMe.SequenceNumber = random.Next();
    customerRememberMe.Recent = random.Next(2, int.MaxValue);
    customerRememberMe.CustomerID = customerID;
    customerRememberMe.Save();
    return customerRememberMe;
  }

  public static List<CustomerRememberMe> LoadByCustomer(long customerID)
  {
    return BaseDBObject.LoadByForeignKey<CustomerRememberMe>("CustomerID", (object) customerID);
  }

  public int UpdateRecent()
  {
    this.Recent = new Random(DateTime.Now.Millisecond).Next(2, int.MaxValue);
    this.LastUpdated = DateTime.UtcNow;
    this.Save();
    return this.Recent;
  }
}
