// Decompiled with JetBrains decompiler
// Type: Monnit.Data.SMSAccount
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class SMSAccount
{
  [DBMethod("SMSAccount_GetSMSTheme")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  sc.*\r\nINTO #temp\r\nFROM dbo.[SMSCarrier] sc                                            \r\nINNER JOIN dbo.[SMSAccount] sa ON sa.SMSCarrierID = sc.SMSCarrierID\r\nWHERE sa.AccountThemeID = @CurrentThemeID;\r\n\r\nIF @@RowCount = 0 \r\nBEGIN\r\n\r\n\tINSERT INTO #temp\r\n\tSELECT\r\n\t  sc.*\r\n\tFROM dbo.[SMSCarrier] sc                                            \r\n\tWHERE sc.AccountThemeID = @CurrentThemeID OR sc.AccountThemeID is null;\r\n\r\nEND\r\n\r\nSELECT * FROM #temp\r\nOrder by Rank, SMSCarrierName;\r\n\r\nDROP TABLE #temp;\r\n")]
  internal class GetSMSTheme : BaseDBMethod
  {
    [DBMethodParam("CurrentThemeID", typeof (long))]
    public long CurrentThemeID { get; private set; }

    public List<SMSCarrier> Result { get; private set; }

    public GetSMSTheme(long currentThemeID)
    {
      this.CurrentThemeID = currentThemeID;
      this.Result = BaseDBObject.Load<SMSCarrier>(this.ToDataTable());
    }
  }

  [DBMethod("SMSAccount_DeleteByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE dbo.[SMSAccount]\r\nWHERE AccountThemeID = @AccountThemeID;\r\n")]
  internal class DeleteByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountThemeID", typeof (long))]
    public long AccountThemeID { get; private set; }

    public DeleteByAccountID(long accountThemeID)
    {
      this.AccountThemeID = accountThemeID;
      this.Execute();
    }
  }
}
