// Decompiled with JetBrains decompiler
// Type: Monnit.Data.LoadExhaustedByAccountID
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

[DBMethod("NotificationCredit_LoadExhaustedByAccountID")]
[DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP 8\r\n  *\r\nFROM dbo.[NotificationCredit] WITH (NOLOCK)\r\nWHERE AccountID = @AccountID\r\nAND ExhaustedDate IS NOT NULL\r\nORDER BY ExhaustedDate DESC;   \r\n")]
internal class LoadExhaustedByAccountID : BaseDBMethod
{
  [DBMethodParam("AccountID", typeof (long))]
  public long AccountiD { get; private set; }

  public List<Monnit.NotificationCredit> Result { get; private set; }

  public LoadExhaustedByAccountID(long accountID)
  {
    this.AccountiD = accountID;
    this.Result = BaseDBObject.Load<Monnit.NotificationCredit>(this.ToDataTable());
  }
}
