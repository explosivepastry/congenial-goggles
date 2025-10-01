// Decompiled with JetBrains decompiler
// Type: Monnit.CustomerAccountLink
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("CustomerAccountLink")]
public class CustomerAccountLink : BaseDBObject
{
  private long _CustomerAccountLinkID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private long _AccountID = long.MinValue;
  private bool _RequestConfirmed = false;
  private bool _CustomerDeleted = false;
  private bool _AccountDeleted = false;
  private DateTime _DateUserAdded = DateTime.MinValue;
  private DateTime _DateUserDeleted = DateTime.MinValue;
  private DateTime _DateAccountDeleted = DateTime.MinValue;
  private string _guid = "";

  [DBProp("CustomerAccountLinkID", IsPrimaryKey = true)]
  public long CustomerAccountLinkID
  {
    get => this._CustomerAccountLinkID;
    set => this._CustomerAccountLinkID = value;
  }

  [DBProp("CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("RequestConfirmed")]
  public bool RequestConfirmed
  {
    get => this._RequestConfirmed;
    set => this._RequestConfirmed = value;
  }

  [DBProp("CustomerDeleted")]
  public bool CustomerDeleted
  {
    get => this._CustomerDeleted;
    set => this._CustomerDeleted = value;
  }

  [DBProp("AccountDeleted")]
  public bool AccountDeleted
  {
    get => this._AccountDeleted;
    set => this._AccountDeleted = value;
  }

  [DBProp("DateUserAdded")]
  public DateTime DateUserAdded
  {
    get => this._DateUserAdded;
    set => this._DateUserAdded = value;
  }

  [DBProp("DateUserDeleted")]
  public DateTime DateUserDeleted
  {
    get => this._DateUserDeleted;
    set => this._DateUserDeleted = value;
  }

  [DBProp("DateAccountDeleted")]
  public DateTime DateAccountDeleted
  {
    get => this._DateAccountDeleted;
    set => this._DateAccountDeleted = value;
  }

  [DBProp("GUID")]
  public string GUID
  {
    get => this._guid;
    set => this._guid = value;
  }

  public static CustomerAccountLink Load(long customerID, long accountID)
  {
    return BaseDBObject.LoadByForeignKeys<CustomerAccountLink>(new string[2]
    {
      "AccountID",
      "CustomerID"
    }, new object[2]
    {
      (object) accountID,
      (object) customerID
    }).FirstOrDefault<CustomerAccountLink>();
  }

  public static List<CustomerAccountLink> LoadAllByCustomerID(long customerID)
  {
    return BaseDBObject.LoadByForeignKey<CustomerAccountLink>("CustomerID", (object) customerID);
  }

  public static List<CustomerAccountLink> LoadAllByAccountID(long accountID)
  {
    return BaseDBObject.LoadByForeignKey<CustomerAccountLink>("AccountID", (object) accountID);
  }
}
