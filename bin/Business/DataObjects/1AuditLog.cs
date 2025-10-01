// Decompiled with JetBrains decompiler
// Type: Monnit.Data.AuditLog
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class AuditLog
{
  [DBMethod("AuditLog_LoadAllByAccountIDAndMonthRange")]
  [DBMethodBody(DBMS.SqlServer, "\r\n  SELECT *\r\n  FROM dbo.[AuditLog]\r\n  WHERE AccountID = @AccountID\r\n    AND TimeStamp >= DATEADD(MONTH, -1, GETDATE())\r\n\tAND ActionDescription = 'Disabled Account For AutoBill';\r\n")]
  internal class LoadAllByAccountIDAndMonthRange : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<Monnit.AuditLog> Result { get; private set; }

    public LoadAllByAccountIDAndMonthRange(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.AuditLog>(this.ToDataTable());
    }
  }
}
