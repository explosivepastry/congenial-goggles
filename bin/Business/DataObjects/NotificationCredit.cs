// Decompiled with JetBrains decompiler
// Type: Monnit.NotificationCredit
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using Monnit.Data;
using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("NotificationCredit")]
public class NotificationCredit : BaseDBObject
{
  private long _NotificationCreditID = long.MinValue;
  private long _AccountID = long.MinValue;
  private long _NotificationCreditTypeID = long.MinValue;
  private long _ActivatedByCustomerID = long.MinValue;
  private DateTime _ActivationDate = DateTime.MinValue;
  private DateTime _ExpirationDate = DateTime.MinValue;
  private string _ActivationCode = string.Empty;
  private int _ActivatedCredits = 0;
  private int _UsedCredits = 0;
  private DateTime _ExhaustedDate = DateTime.MinValue;
  private bool _IsDeleted = false;

  [DBProp("NotificationCreditID", IsPrimaryKey = true)]
  public long NotificationCreditID
  {
    get => this._NotificationCreditID;
    set => this._NotificationCreditID = value;
  }

  [DBProp("AccountID")]
  [DBForeignKey("Account", "AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("NotificationCreditTypeID")]
  [DBForeignKey("NotificationCreditType", "NotificationCreditTypeID")]
  public long NotificationCreditTypeID
  {
    get => this._NotificationCreditTypeID;
    set => this._NotificationCreditTypeID = value;
  }

  public NotificationCreditType NotificationCreditType
  {
    get => NotificationCreditType.Load(this.NotificationCreditTypeID);
  }

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

  public static NotificationCredit Load(long id) => BaseDBObject.Load<NotificationCredit>(id);

  public override void Delete()
  {
    this.IsDeleted = true;
    this.Save();
  }

  public static List<NotificationCredit> LoadByAccount(long accountID)
  {
    return BaseDBObject.LoadByForeignKey<NotificationCredit>("AccountID", (object) accountID);
  }

  public static List<NotificationCredit> LoadAvailable(long accountID)
  {
    return Monnit.Data.NotificationCredit.LoadAvailable.Exec(accountID);
  }

  public static bool Charge(long accountID, int creditsToCharge)
  {
    bool flag = false;
    List<NotificationCredit> notificationCreditList = new List<NotificationCredit>();
    foreach (NotificationCredit notificationCredit in NotificationCredit.LoadAvailable(accountID))
    {
      if (notificationCredit.RemainingCredits >= creditsToCharge)
      {
        notificationCreditList.Add(notificationCredit);
        notificationCredit.UsedCredits += creditsToCharge;
        flag = true;
        break;
      }
      notificationCreditList.Add(notificationCredit);
      int remainingCredits = notificationCredit.RemainingCredits;
      notificationCredit.UsedCredits += remainingCredits;
      creditsToCharge -= remainingCredits;
    }
    if (flag)
    {
      foreach (NotificationCredit notificationCredit in notificationCreditList)
      {
        if (notificationCredit.RemainingCredits <= 0)
          notificationCredit.ExhaustedDate = DateTime.UtcNow;
        notificationCredit.Save();
      }
    }
    return flag;
  }

  public static int LoadRemainingCreditsForAccount(long accountID)
  {
    return Monnit.Data.NotificationCredit.LoadRemaingCreditsForAccount.Exec(accountID);
  }

  public static List<NotificationCredit> LoadExhaustedNotificationCreditsByAccountID(long accountID)
  {
    return new LoadExhaustedByAccountID(accountID).Result;
  }
}
