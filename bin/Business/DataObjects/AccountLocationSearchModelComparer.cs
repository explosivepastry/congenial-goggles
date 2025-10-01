// Decompiled with JetBrains decompiler
// Type: Monnit.AccountLocationSearchModelComparer
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

public class AccountLocationSearchModelComparer : Comparer<AccountLocationSearchModel>
{
  public override int Compare(AccountLocationSearchModel x, AccountLocationSearchModel y)
  {
    if (x == null || y == null)
      return 0;
    if (x.DevicesAlerting > 0 || y.DevicesAlerting > 0)
      return x.DevicesAlerting > 0 && y.DevicesAlerting > 0 ? x.AccountNumber.CompareTo(y.AccountNumber) : -x.DevicesAlerting.CompareTo(y.DevicesAlerting);
    if (x.DevicesWarning > 0 || y.DevicesWarning > 0)
      return x.DevicesWarning > 0 && y.DevicesWarning > 0 ? x.AccountNumber.CompareTo(y.AccountNumber) : -x.DevicesWarning.CompareTo(y.DevicesWarning);
    if (x.DevicesOffline > 0 || y.DevicesOffline > 0)
      return x.DevicesOffline > 0 && y.DevicesOffline > 0 ? x.AccountNumber.CompareTo(y.AccountNumber) : -x.DevicesOffline.CompareTo(y.DevicesOffline);
    if (x.DevicesAlerting + y.DevicesAlerting + x.DevicesWarning + y.DevicesWarning + x.DevicesOffline + y.DevicesOffline == 0)
      return x.AccountNumber.CompareTo(y.AccountNumber);
    throw new Exception("AccountLocationSearchModelComparer.Compare() Unhandled Case");
  }
}
