// Decompiled with JetBrains decompiler
// Type: Monnit.Data.AuthenticationLog
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class AuthenticationLog
{
  [DBMethod("AuthenticationLog_LoadByAccountAndDateRange")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  al.*\r\nFROM dbo.[AuthenticationLog] al\r\nINNER JOIN dbo.[Customer] c ON c.CustomerID = al.CustomerID\r\nWHERE c.AccountID = @AccountID\r\n  AND al.LogDate BETWEEN @FromDate AND @ToDate\r\nORDER BY\r\n  al.AuthenticationLogID DESC;\r\n")]
  internal class LoadByAccountAndDateRange : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    public List<Monnit.AuthenticationLog> Result { get; private set; }

    public LoadByAccountAndDateRange(long accountID, DateTime fromDate, DateTime toDate)
    {
      this.AccountID = accountID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Result = BaseDBObject.Load<Monnit.AuthenticationLog>(this.ToDataTable());
    }
  }
}
