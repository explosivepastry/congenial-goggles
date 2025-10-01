// Decompiled with JetBrains decompiler
// Type: Monnit.AccountLocationListModel
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

public class AccountLocationListModel : BaseDBObject
{
  [DBProp("RowID")]
  public int RowID { get; set; }

  [DBProp("AccountID")]
  public long AccountID { get; set; }

  [DBProp("AccountNumber")]
  public string AccountNumber { get; set; }

  public static Tuple<bool, Stack<AccountLocationListModel>> LocationList(
    long accountID,
    long customerID)
  {
    List<AccountLocationListModel> result = new Monnit.Data.Account.LocationList(accountID, customerID).Result;
    result.Reverse();
    Stack<AccountLocationListModel> locationListModelStack = new Stack<AccountLocationListModel>((IEnumerable<AccountLocationListModel>) result);
    return new Tuple<bool, Stack<AccountLocationListModel>>(locationListModelStack.Pop().AccountNumber.ToLong() == 0L, locationListModelStack);
  }
}
