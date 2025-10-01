// Decompiled with JetBrains decompiler
// Type: Monnit.AccountIncrement
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace Monnit;

[MetadataType(typeof (AccountMetadata))]
[DBClass("AccountIncrement")]
public class AccountIncrement : BaseDBObject
{
  private long _AccountIncrementID = long.MinValue;
  private long _AccountID = long.MinValue;
  private string _AccountNumber = string.Empty;
  private DateTime _DateAdded;
  private bool _IsDeleted = false;
  private DateTime _DeletedDate;
  private DateTime _LastRun;
  private string _ApprovedBy = string.Empty;
  private bool _DoTrialUpgrades = false;
  private bool _DoTieredUpgrades = false;
  private long _NetsuiteCustomerID = long.MinValue;

  [DBProp("AccountIncrementID", IsPrimaryKey = true, AllowNull = false)]
  public long AccountIncrementID
  {
    get => this._AccountIncrementID;
    set => this._AccountIncrementID = value;
  }

  [DBForeignKey("Account", "AccountID")]
  [DBProp("AccountID", AllowNull = true)]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("AccountNumber", AllowNull = false, MaxLength = 300)]
  public string AccountNumber
  {
    get => this._AccountNumber;
    set
    {
      if (value == null)
        this._AccountNumber = string.Empty;
      else
        this._AccountNumber = value;
    }
  }

  [DBProp("DateAdded", AllowNull = false)]
  public DateTime DateAdded
  {
    get => this._DateAdded;
    set => this._DateAdded = value;
  }

  [DBProp("IsDeleted", AllowNull = false, DefaultValue = false)]
  public bool IsDeleted
  {
    get => this._IsDeleted;
    set => this._IsDeleted = value;
  }

  [DBProp("DeletedDate", AllowNull = true)]
  public DateTime DeletedDate
  {
    get => this._DeletedDate;
    set => this._DeletedDate = value;
  }

  [DBProp("LastRun", AllowNull = true)]
  public DateTime LastRun
  {
    get => this._LastRun;
    set => this._LastRun = value;
  }

  [DBProp("ApprovedBy", AllowNull = true, MaxLength = 100)]
  public string ApprovedBy
  {
    get => this._ApprovedBy;
    set
    {
      if (value == null)
        this._ApprovedBy = string.Empty;
      else
        this._ApprovedBy = value;
    }
  }

  [DBProp("DoTrialUpgrades", AllowNull = false, DefaultValue = false)]
  public bool DoTrialUpgrades
  {
    get => this._DoTrialUpgrades;
    set => this._DoTrialUpgrades = value;
  }

  [DBProp("DoTieredUpgrades", AllowNull = false, DefaultValue = false)]
  public bool DoTieredUpgrades
  {
    get => this._DoTieredUpgrades;
    set => this._DoTieredUpgrades = value;
  }

  [DBProp("NetsuiteCustomerID", AllowNull = true)]
  public long NetsuiteCustomerID
  {
    get => this._NetsuiteCustomerID;
    set => this._NetsuiteCustomerID = value;
  }

  public static AccountIncrement Load(long ID) => BaseDBObject.Load<AccountIncrement>(ID);

  public static List<AccountIncrement> LoadByAccountID(long ID)
  {
    return BaseDBObject.LoadByForeignKey<AccountIncrement>("AccountID", (object) ID);
  }

  public static List<AccountIncrement> LoadAll() => BaseDBObject.LoadAll<AccountIncrement>();

  public static bool Remove(long AccountId) => new Monnit.Data.Account.Remove(AccountId).Result;
}
