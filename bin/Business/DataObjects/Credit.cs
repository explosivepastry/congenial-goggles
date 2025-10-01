// Decompiled with JetBrains decompiler
// Type: Monnit.Credit
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit;

[DBClass("Credit")]
public class Credit : BaseDBObject
{
  private long _CreditID = long.MinValue;
  private long _AccountID = long.MinValue;
  private long _CreditTypeID = long.MinValue;
  private long _ActivatedByCustomerID = long.MinValue;
  private DateTime _ActivationDate = DateTime.MinValue;
  private DateTime _ExpirationDate = DateTime.MinValue;
  private string _ActivationCode = string.Empty;
  private int _ActivatedCredits = 0;
  private int _UsedCredits = 0;
  private DateTime _ExhaustedDate = DateTime.MinValue;
  private bool _IsDeleted = false;

  [DBProp("CreditID", IsPrimaryKey = true)]
  public long CreditID
  {
    get => this._CreditID;
    set => this._CreditID = value;
  }

  [DBProp("AccountID")]
  [DBForeignKey("Account", "AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("CreditTypeID")]
  [DBForeignKey("NotificationCreditType", "NotificationCreditTypeID")]
  public long CreditTypeID
  {
    get => this._CreditTypeID;
    set => this._CreditTypeID = value;
  }

  public NotificationCreditType CreditType => NotificationCreditType.Load(this.CreditTypeID);

  [DBProp("ActivatedByCustomerID")]
  [DBForeignKey("Customer", "CustomerID")]
  public long ActivatedByCustomerID
  {
    get => this._ActivatedByCustomerID;
    set => this._ActivatedByCustomerID = value;
  }

  [DBProp("ActivationDate")]
  public DateTime ActivationDate
  {
    get => this._ActivationDate;
    set => this._ActivationDate = value;
  }

  [DBProp("ExpirationDate")]
  public DateTime ExpirationDate
  {
    get => this._ExpirationDate;
    set => this._ExpirationDate = value;
  }

  [DBProp("ActivationCode", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string ActivationCode
  {
    get => this._ActivationCode;
    set => this._ActivationCode = value;
  }

  [DBProp("ActivatedCredits")]
  public int ActivatedCredits
  {
    get => this._ActivatedCredits;
    set => this._ActivatedCredits = value;
  }

  [DBProp("UsedCredits")]
  public int UsedCredits
  {
    get => this._UsedCredits;
    set => this._UsedCredits = value;
  }

  public int RemainingCredits => this.ActivatedCredits - this.UsedCredits;

  [DBProp("ExhaustedDate")]
  public DateTime ExhaustedDate
  {
    get => this._ExhaustedDate;
    set => this._ExhaustedDate = value;
  }

  [DBProp("IsDeleted")]
  public bool IsDeleted
  {
    get => this._IsDeleted;
    set => this._IsDeleted = value;
  }

  public static Credit Load(long id) => BaseDBObject.Load<Credit>(id);

  public override void Delete()
  {
    this.IsDeleted = true;
    this.Save();
  }

  public static List<Credit> LoadByAccount(long accountID)
  {
    return BaseDBObject.LoadByForeignKey<Credit>("AccountID", (object) accountID);
  }

  public static List<Credit> LoadAvailable(
    long accountID,
    eCreditClassification creditClassification)
  {
    return new Monnit.Data.Credit.LoadAvailable(accountID, creditClassification.ToInt()).Result;
  }

  public static List<Credit> LoadExhaustedCreditsByAccountID(long accountID)
  {
    return new Monnit.Data.Credit.LoadExhaustedCreditsByAccountID(accountID).Result;
  }

  public static List<Credit> LoadOverdraft(
    long accountID,
    eCreditClassification creditClassification)
  {
    return new Monnit.Data.Credit.LoadOverdraft(accountID, creditClassification.ToInt()).Result;
  }

  public static DataSet LogHXUsage() => Monnit.Data.Credit.LogHXUsage.Exec();

  public static int LoadRemainingCreditsForAccount(
    long accountID,
    eCreditClassification creditClassification)
  {
    return new Monnit.Data.Credit.LoadRemaingCreditsForAccount(accountID, creditClassification).Result;
  }
}
