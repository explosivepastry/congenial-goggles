// Decompiled with JetBrains decompiler
// Type: Monnit.Data.ExternalDataSubscriptionAttempt
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class ExternalDataSubscriptionAttempt
{
  [DBMethod("ExternalDataSubscriptionAttempt_LoadReady")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @ProcName NVARCHAR(50);\r\n\r\nDECLARE @ErrorNum         INT,          \r\n        @ErrorProcedure   NVARCHAR(50), \r\n        @ErrorSysMsg      NVARCHAR(MAX);\r\n\r\nDECLARE @UTCDate DATETIME;\r\n\tSET @UTCDate = GETUTCDATE();\r\n\t\r\nDECLARE @_Limit INT\r\n  SET @_Limit = @Limit\r\n\r\nDECLARE @RowCount INT;\r\n\r\nDECLARE @Temp TABLE ([ExternalDataSubscriptionAttemptID] BIGINT,  [ExternalDataSubscriptionID] BIGINT, [Rank] INT)\r\n\r\nBEGIN TRY\r\n\r\n    SET @ProcName = OBJECT_NAME(@@PROCID);\t\t\r\n  \t\r\n    INSERT INTO @Temp ([ExternalDataSubscriptionAttemptID], [ExternalDataSubscriptionID], [Rank])\r\n      SELECT TOP (@Limit)\r\n\t      a.[ExternalDataSubscriptionAttemptID], s.[ExternalDataSubscriptionID], 1 'rank' \r\n      FROM dbo.[ExternalDataSubscriptionAttempt] a WITH (NOLOCK)\r\n      INNER JOIN dbo.[ExternalDataSubscription] s WITH (NOLOCK) on s.ExternalDataSubscriptionID = a.ExternalDataSubscriptionID\r\n      WHERE a.Status = 4 -- NEW\r\n\t      AND a.DoRetry = 1\r\n\t      AND a.AttemptCount = 0\r\n\t      AND DATEDIFF(MINUTE, a.CreateDate, @UTCDate) < 240\r\n\t      AND s.DoSend = 1\r\n\r\n      SET @RowCount = @@ROWCOUNT\r\n\r\n      INSERT INTO @Temp ([ExternalDataSubscriptionAttemptID], [ExternalDataSubscriptionID], [Rank])\r\n      SELECT TOP (@Limit - @RowCount)\r\n\t      a.[ExternalDataSubscriptionAttemptID], s.[ExternalDataSubscriptionID], 2\r\n      FROM dbo.[ExternalDataSubscriptionAttempt] a WITH (NOLOCK)\r\n      INNER JOIN dbo.[ExternalDataSubscription] s  WITH (NOLOCK) on s.ExternalDataSubscriptionID = a.ExternalDataSubscriptionID\r\n      WHERE a.Status = 1 -- Failed\r\n\t      AND a.DoRetry = 1\r\n\t      AND a.AttemptCount = 1\r\n\t      AND DATEDIFF(MINUTE, CreateDate, @UTCDate) > 2\r\n\t      AND DATEDIFF(MINUTE, a.CreateDate, @UTCDate) < 240\r\n\t      AND s.DoRetry = 1\r\n\r\n      SET @RowCount = @RowCount + @@ROWCOUNT\r\n\r\n      INSERT INTO @Temp ([ExternalDataSubscriptionAttemptID], [ExternalDataSubscriptionID], [Rank])\r\n      SELECT TOP (@Limit - @RowCount)\r\n\t      a.[ExternalDataSubscriptionAttemptID], s.[ExternalDataSubscriptionID], 3\r\n      FROM dbo.[ExternalDataSubscriptionAttempt] a WITH (NOLOCK)\r\n      INNER JOIN dbo.[ExternalDataSubscription] s WITH (NOLOCK) on s.ExternalDataSubscriptionID = a.ExternalDataSubscriptionID\r\n      WHERE a.Status = 1 -- Failed\r\n\t      AND a.DoRetry = 1\r\n\t      AND a.AttemptCount = 2\r\n\t      AND DATEDIFF(MINUTE, CreateDate, @UTCDate) > 15\r\n\t      AND DATEDIFF(MINUTE, a.CreateDate, @UTCDate) < 240\r\n\t      AND s.DoRetry = 1\r\n\r\n      SET @RowCount = @RowCount + @@ROWCOUNT\r\n\r\n      INSERT INTO @Temp ([ExternalDataSubscriptionAttemptID], [ExternalDataSubscriptionID], [Rank])\r\n      SELECT TOP (@Limit - @RowCount)\r\n\t      a.[ExternalDataSubscriptionAttemptID], s.[ExternalDataSubscriptionID], 4\r\n      FROM dbo.[ExternalDataSubscriptionAttempt] a WITH (NOLOCK)\r\n      INNER JOIN dbo.[ExternalDataSubscription] s  WITH (NOLOCK) on s.ExternalDataSubscriptionID = a.ExternalDataSubscriptionID\r\n      WHERE a.Status = 1 -- Failed\r\n\t      AND a.DoRetry = 1\r\n\t      AND a.AttemptCount = 3\r\n\t      AND DATEDIFF(MINUTE, CreateDate, @UTCDate) > 60\r\n\t      AND DATEDIFF(MINUTE, a.CreateDate, @UTCDate) < 240\r\n\t      AND s.DoRetry = 1\r\n\r\n      SET @RowCount = @RowCount + @@ROWCOUNT\r\n\r\n      INSERT INTO @Temp ([ExternalDataSubscriptionAttemptID], [ExternalDataSubscriptionID], [Rank])\r\n      SELECT TOP (@Limit - @RowCount)\r\n\t      a.[ExternalDataSubscriptionAttemptID], s.[ExternalDataSubscriptionID], 5\r\n      FROM dbo.[ExternalDataSubscriptionAttempt] a WITH (NOLOCK)\r\n      INNER JOIN dbo.[ExternalDataSubscription] s WITH (NOLOCK) on s.ExternalDataSubscriptionID = a.ExternalDataSubscriptionID\r\n      WHERE a.Status = 3 --Retry\r\n\t      AND a.DoRetry = 1\r\n\t      AND a.AttemptCount < 10\r\n\t      --AND DATEDIFF(MINUTE, a.CreateDate, @UTCDate) < 240\r\n\t      AND s.DoSend = 1\r\n\r\n      BEGIN TRAN\r\n\r\n          --SELECT * from ExternalDataSubscriptionAttempt\r\n          --WHERE Status = 2\r\n          --  AND DATEDIFF(MINUTE, CreateDate, @UTCDate) > 15\r\n          --  AND CreateDate > '2018-02-01 15:30:54.870'\r\n\r\n          UPDATE dbo.[ExternalDataSubscriptionAttempt] \r\n          SET Status = 1\r\n          WHERE Status = 2\r\n            AND DATEDIFF(MINUTE, CreateDate, @UTCDate) > 15\r\n            AND CreateDate > '2018-02-01 15:30:54.870'\r\n\r\n\r\n          UPDATE dbo.[ExternalDataSubscriptionAttempt]\r\n\t        SET Status=2,\r\n\t\t        ProcessDate = GETUTCDATE(),\r\n\t\t        AttemptCount = AttemptCount + 1\r\n\t        WHERE ExternalDataSubscriptionAttemptID IN (SELECT ExternalDataSubscriptionAttemptID FROM @Temp);\r\n\r\n          SELECT top (@_Limit) \r\n            *\r\n          FROM dbo.[ExternalDataSubscriptionAttempt] a WITH (NOLOCK)\r\n          INNER JOIN @Temp t on a.ExternalDataSubscriptionAttemptID = t.ExternalDataSubscriptionAttemptID\r\n          ORDER BY \tt.rank, t.ExternalDataSubscriptionAttemptID ASC\r\n\r\n          SELECT DISTINCT\r\n            p.*\r\n          FROM dbo.[ExternalDataSubscriptionProperty] p\r\n          INNER JOIN @Temp t on p.ExternalDataSubscriptionID = t.ExternalDataSubscriptionID\r\n          ORDER BY p.ExternalDataSubscriptionID, p.ExternalDataSubscriptionPropertyID\r\n\r\n      COMMIT TRAN\r\n\r\nEND TRY\r\nBEGIN CATCH\r\n\r\n\tSET @ErrorNum = ERROR_NUMBER();\r\n\tSET @ErrorProcedure = ERROR_PROCEDURE();\r\n\tSET @ErrorSysMsg = ERROR_MESSAGE();\r\n\r\n\tDECLARE @Recipients varchar(500)\r\n\tDECLARE @Subject varchar(30)\r\n\tDECLARE @Body VARCHAR(2000)\r\n  DECLARE @Params VARCHAR(1000)\r\n\r\n  SET @Params = '@Limit: ' + CONVERT(VARCHAR(100), @Limit)\r\n\r\n  INSERT INTO DBErrorLog (ProcName, Date, Urgency, Message, Params)\r\n  VALUES (@ProcName, GETUTCDATE(), 2, @ErrorSysMsg, @Params)\r\n\r\n\tSET @Body = '<p>Team, </p> <p>Critical Procedure Failed: '+@ProcName+'. Please Address. '+CONVERT(VARCHAR(20), GETDATE() )+' --- -THIS IS A TEST ONLY PLEASE IGNORE -Nathan</p> \r\n  <p>ErrorMessage: '+CONVERT(VARCHAR(20), @ErrorNum) +' ' + @ErrorSysMsg+'</p>\r\n\t<p>Sincerely,</p><p>-DBA</p>'\r\n\r\n\tSET @Subject = 'MonnitDB ProcFail - Priority 2 -- TEST ONLY'\r\n\tSET @Recipients = (select value from ConfigData where KeyName = 'DB_Procfail_Contacts')\r\n\r\n    EXEC msdb.dbo.sp_send_dbmail \r\n\t  @Profile_name = 'Alerts',\r\n\t  @Recipients = @Recipients , \r\n      @subject = @Subject,  \r\n      @body = @Body,  \r\n      @body_format = 'HTML' ;  \r\n\r\nEND CATCH\r\n")]
  internal class LoadReady : BaseDBMethod
  {
    [DBMethodParam("Limit", typeof (int))]
    public int Limit { get; private set; }

    public List<Monnit.ExternalDataSubscriptionAttempt> Result { get; private set; }

    public LoadReady(int limit)
    {
      try
      {
        this.Limit = limit;
        DataSet dataSet = this.ToDataSet();
        List<Monnit.ExternalDataSubscriptionAttempt> subscriptionAttemptList = BaseDBObject.Load<Monnit.ExternalDataSubscriptionAttempt>(dataSet.Tables[0]);
        if (dataSet.Tables.Count > 1)
        {
          List<Monnit.ExternalDataSubscriptionProperty> list = BaseDBObject.Load<Monnit.ExternalDataSubscriptionProperty>(dataSet.Tables[1]).ToList<Monnit.ExternalDataSubscriptionProperty>();
          foreach (Monnit.ExternalDataSubscriptionAttempt subscriptionAttempt in subscriptionAttemptList)
          {
            Monnit.ExternalDataSubscriptionAttempt att = subscriptionAttempt;
            att.Properties = list.Where<Monnit.ExternalDataSubscriptionProperty>((System.Func<Monnit.ExternalDataSubscriptionProperty, bool>) (m => m.ExternalDataSubscriptionID == att.ExternalDataSubscriptionID)).ToList<Monnit.ExternalDataSubscriptionProperty>();
          }
        }
        this.Result = subscriptionAttemptList;
      }
      catch (Exception ex)
      {
        ex.Log("Exception in ExternalDataSubscriptionAttempt.LoadReady: " + ex.ToString());
        throw ex;
      }
    }
  }

  [DBMethod("ExternalDataSubscriptionAttempt_LoadBySubscriptionAndDate")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP(@Limit)\r\n  * \r\nFROM dbo.[ExternalDataSubscriptionAttempt]\r\nWHERE ExternalDataSubscriptionID = @ExternalDataSubscriptionID\r\n  AND CreateDate BETWEEN @FromDate AND @ToDate\r\n  AND (@PaginationAttemptID = 0 OR ExternalDataSubscriptionAttemptID < @PaginationAttemptID)\r\nORDER BY\r\n  CreateDate DESC;\r\n")]
  internal class LoadBySubscriptionAndDate : BaseDBMethod
  {
    [DBMethodParam("ExternalDataSubscriptionID", typeof (long))]
    public long ExternalDataSubscriptionID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("Limit", typeof (int))]
    public int Limit { get; private set; }

    [DBMethodParam("PaginationAttemptID", typeof (long))]
    public long PaginationAttemptID { get; private set; }

    public List<Monnit.ExternalDataSubscriptionAttempt> Result { get; private set; }

    public LoadBySubscriptionAndDate(
      long subscriptionID,
      DateTime fromDate,
      DateTime toDate,
      long paginationAttemptID,
      int limit)
    {
      this.ExternalDataSubscriptionID = subscriptionID;
      this.Limit = limit;
      this.ToDate = toDate;
      this.FromDate = fromDate;
      this.PaginationAttemptID = paginationAttemptID;
      this.Result = BaseDBObject.Load<Monnit.ExternalDataSubscriptionAttempt>(this.ToDataTable());
    }
  }

  [DBMethod("ExternalDataSubscriptionAttempt_LoadErrorsBySubscriptionAndDate")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP(@Limit)\r\n  * \r\nFROM dbo.[ExternalDataSubscriptionAttempt]\r\nWHERE [ExternalDataSubscriptionID] = @ExternalDataSubscriptionID\r\n  AND [CreateDate] BETWEEN @FromDate AND @ToDate\r\n  AND ([Status] = 1 OR [Status] = 3)\r\n  AND (@PaginationAttemptID = 0 OR [ExternalDataSubscriptionAttemptID] < @PaginationAttemptID)\r\nORDER BY\r\n  [CreateDate] DESC;\r\n")]
  internal class LoadErrorsBySubscriptionAndDate : BaseDBMethod
  {
    [DBMethodParam("ExternalDataSubscriptionID", typeof (long))]
    public long ExternalDataSubscriptionID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("Limit", typeof (int))]
    public int Limit { get; private set; }

    [DBMethodParam("PaginationAttemptID", typeof (long))]
    public long PaginationAttemptID { get; private set; }

    public List<Monnit.ExternalDataSubscriptionAttempt> Result { get; private set; }

    public LoadErrorsBySubscriptionAndDate(
      long subscriptionID,
      DateTime fromDate,
      DateTime toDate,
      long paginationAttemptID,
      int limit)
    {
      this.ExternalDataSubscriptionID = subscriptionID;
      this.Limit = limit;
      this.ToDate = toDate;
      this.FromDate = fromDate;
      this.PaginationAttemptID = paginationAttemptID;
      this.Result = BaseDBObject.Load<Monnit.ExternalDataSubscriptionAttempt>(this.ToDataTable());
    }
  }
}
