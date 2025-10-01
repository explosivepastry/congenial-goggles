// Decompiled with JetBrains decompiler
// Type: Monnit.SMSAccount
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("SMSAccount")]
public class SMSAccount : BaseDBObject
{
  private long _SMSAccountID = long.MinValue;
  private long _AccountThemeID = long.MinValue;
  private long _SMSCarrierID = long.MinValue;

  [DBProp("SMSAccountID", IsPrimaryKey = true)]
  public long SMSAccountID
  {
    get => this._SMSAccountID;
    set => this._SMSAccountID = value;
  }

  [DBProp("AccountThemeID")]
  [DBForeignKey("AccountTheme", "AccountThemeID")]
  public long AccountThemeID
  {
    get => this._AccountThemeID;
    set => this._AccountThemeID = value;
  }

  [DBProp("SMSCarrierID")]
  [DBForeignKey("SMSCarrier", "SMSCarrierID")]
  public long SMSCarrierID
  {
    get => this._SMSCarrierID;
    set => this._SMSCarrierID = value;
  }

  public static SMSAccount Load(long ID)
  {
    return BaseDBObject.LoadAll<SMSAccount>().FirstOrDefault<SMSAccount>((Func<SMSAccount, bool>) (obj => obj.SMSAccountID == ID));
  }

  public static void DeleteByAccountID(long accountID)
  {
    Monnit.Data.SMSAccount.DeleteByAccountID deleteByAccountId = new Monnit.Data.SMSAccount.DeleteByAccountID(accountID);
  }

  public static List<SMSAccount> LoadAllByAccountTheme(long accountThemeID)
  {
    return BaseDBObject.LoadAll<SMSAccount>().Where<SMSAccount>((Func<SMSAccount, bool>) (obj => obj.AccountThemeID == accountThemeID)).ToList<SMSAccount>();
  }

  public static List<SMSCarrier> SMSList(long currentThemeID)
  {
    return new Monnit.Data.SMSAccount.GetSMSTheme(currentThemeID).Result;
  }
}
