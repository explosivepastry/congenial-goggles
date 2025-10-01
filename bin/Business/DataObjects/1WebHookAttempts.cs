// Decompiled with JetBrains decompiler
// Type: Monnit.Data.WebHookAttempts
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class WebHookAttempts
{
  [DBMethod("WebHookAttempts_LoadAllBySubscriptionAndDate")]
  [DBMethodBody(DBMS.SqlServer, "\r\nIF @errorsOnly = 1\r\nBEGIN\r\n\r\n    SELECT TOP(@Limit)\r\n      [AttemptID]       = e.[ExternalDataSubscriptionAttemptID],\r\n      e.[CreateDate],\r\n      e.[URL],\r\n      e.[ProcessDate],\r\n      [Retry]           = e.[DoRetry],\r\n      e.[AttemptCount],\r\n      e.[Status]\r\n    FROM dbo.[ExternalDataSubscriptionAttempt] e\r\n    WHERE [ExternalDataSubscriptionID] = @ExternalDataSubscriptionID\r\n      AND [CreateDate] BETWEEN @FromDate AND @ToDate\r\n      AND ([Status] = 1 OR [Status] = 3)\r\n      AND (@PaginationAttemptID = 0 OR [ExternalDataSubscriptionAttemptID] < @PaginationAttemptID)\r\n    ORDER BY\r\n      [CreateDate] DESC;\r\n\r\nEND ELSE\r\nBEGIN\r\n\r\n    SELECT TOP(@Limit)\r\n      [AttemptID]       = e.[ExternalDataSubscriptionAttemptID],\r\n      e.[CreateDate],\r\n      e.[URL],\r\n      e.[ProcessDate],\r\n      [Retry]           = e.[DoRetry],\r\n      e.[AttemptCount],\r\n      e.[Status]\r\n    FROM dbo.[ExternalDataSubscriptionAttempt] e\r\n    WHERE [ExternalDataSubscriptionID] = @ExternalDataSubscriptionID\r\n      AND [CreateDate] BETWEEN @FromDate AND @ToDate\r\n      AND (@PaginationAttemptID = 0 OR [ExternalDataSubscriptionAttemptID] < @PaginationAttemptID)\r\n    ORDER BY\r\n      [CreateDate] DESC;\r\n\r\nEND")]
  internal class LoadAllBySubscriptionAndDate : BaseDBMethod
  {
    [DBMethodParam("ExternalDataSubscriptionID", typeof (long))]
    public long ExternalDataSubscriptionID { get; private set; }

    [DBMethodParam("ErrorsOnly", typeof (bool))]
    public bool ErrorsOnly { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("Limit", typeof (int))]
    public int Limit { get; private set; }

    [DBMethodParam("PaginationAttemptID", typeof (long))]
    public long PaginationAttemptID { get; private set; }

    public List<Monnit.WebHookAttempts> Result { get; private set; }

    public LoadAllBySubscriptionAndDate(
      long subscriptionID,
      bool errorsOnly,
      DateTime fromDate,
      DateTime toDate,
      long paginationAttemptID,
      int limit)
    {
      this.ExternalDataSubscriptionID = subscriptionID;
      this.ErrorsOnly = errorsOnly;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.PaginationAttemptID = paginationAttemptID;
      this.Limit = limit;
      this.Result = BaseDBObject.Load<Monnit.WebHookAttempts>(this.ToDataTable());
    }
  }
}
