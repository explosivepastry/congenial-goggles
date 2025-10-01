// Decompiled with JetBrains decompiler
// Type: Monnit.Data.WebHookFailures
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class WebHookFailures
{
  [DBMethod("WebHookFailures_LoadSubAccountErrorsBySubscriptionAndDate")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP (@Limit)\r\n  a.[AccountID],\r\n  [AttemptID]       = e.[ExternalDataSubscriptionAttemptID],\r\n  e.[CreateDate],\r\n  e.[URL],\r\n  e.[ProcessDate],\r\n  [Retry]           = e.[DoRetry],\r\n  e.[AttemptCount],\r\n  e.[Status]\r\nFROM dbo.[Account] a WITH (NOLOCK)\r\nINNER JOIN dbo.[ExternalDataSubscriptionAttempt] e WITH (NOLOCK) ON a.[ExternalDataSubscriptionID] = e.[ExternalDataSubscriptionID]\r\nWHERE a.[AccountIDTree] like '%*'+CONVERT(VARCHAR(10), @AccountID)+'*%'\r\n  AND e.[Status]        in (1,3,5) --(failed/NoRetry)\r\n  AND e.[CreateDate]    BETWEEN @FromDate AND @ToDate\r\nORDER BY \r\n  e.[CreateDate] DESC;\r\n")]
  internal class LoadSubAccountErrorsBySubscriptionAndDate : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("Limit", typeof (int))]
    public int Limit { get; private set; }

    public List<Monnit.WebHookFailures> Result { get; private set; }

    public LoadSubAccountErrorsBySubscriptionAndDate(
      long accountID,
      DateTime fromDate,
      DateTime toDate,
      int limit)
    {
      this.AccountID = accountID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Limit = limit;
      this.Result = BaseDBObject.Load<Monnit.WebHookFailures>(this.ToDataTable());
    }
  }

  [DBMethod("WebHookFailures_WebhookFilter")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP 5\r\n  a.[AccountID],\r\n  [AttemptID]       = e.[ExternalDataSubscriptionAttemptID],\r\n  e.[CreateDate],\r\n  e.[URL],\r\n  e.[ProcessDate],\r\n  [Retry]           = e.[DoRetry],\r\n  e.[AttemptCount],\r\n  e.[Status]\r\nFROM dbo.[Account] a WITH (NOLOCK)\r\nINNER JOIN dbo.[ExternalDataSubscriptionAttempt] e WITH (NOLOCK) ON a.[ExternalDataSubscriptionID] = e.[ExternalDataSubscriptionID]\r\nWHERE a.[AccountIDTree] like '%*'+CONVERT(VARCHAR(10), @AccountID)+'*%'\r\n  AND e.[Status]        in (1,3,5) --(failed/NoRetry)\r\n  AND e.[CreateDate]    BETWEEN @FromDate AND @ToDate\r\nORDER BY \r\n  e.[CreateDate] DESC;\r\n")]
  internal class WebhookFilter : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    public List<Monnit.WebHookFailures> Result { get; private set; }

    public WebhookFilter(long accountID, DateTime fromDate, DateTime toDate)
    {
      this.AccountID = accountID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      DataSet dataSet = this.ToDataSet();
      if (dataSet.Tables.Count == 0)
      {
        this.Result = new List<Monnit.WebHookFailures>();
      }
      else
      {
        DataTable table = dataSet.Tables[0];
        this.Result = new List<Monnit.WebHookFailures>();
        if (table == null || table.Rows == null || table.Rows.Count <= 0 || table.Rows[0] == null || table.Rows[0].ItemArray == null || table.Rows[0].ItemArray[0] == null)
          return;
        this.Result = BaseDBObject.Load<Monnit.WebHookFailures>(table);
      }
    }
  }
}
