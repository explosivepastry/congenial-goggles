// Decompiled with JetBrains decompiler
// Type: Monnit.Data.SystemEmail
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class SystemEmail
{
  [DBMethod("SystemEmail_LoadReady")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @ProcName NVARCHAR(50);\r\n\r\nDECLARE @ErrorNum         INT,          \r\n        @ErrorProcedure   NVARCHAR(50), \r\n        @ErrorSysMsg      NVARCHAR(MAX);\r\n\r\nBEGIN TRY\r\n\r\n    SET @ProcName = OBJECT_NAME(@@PROCID);\r\n\r\n    SELECT TOP (@Limit)\r\n      t.*\r\n    INTO #TEMP\r\n    FROM (\r\n          SELECT\r\n            a.*,\r\n            [Rank] = 1 \r\n          FROM dbo.[SystemEmail] a\t\r\n          WHERE a.[Status]    = 'New' \r\n            AND a.[DoRetry]   = 1\r\n            AND (a.[SendDate] < @Date OR a.[SendDate] IS NULL)\r\n\r\n          UNION\r\n          SELECT\r\n            a.*,\r\n            2\r\n          FROM dbo.[SystemEmail] a\r\n          WHERE a.[Status]     = 'Failed' \r\n            AND a.[DoRetry]    = 1  -- true \r\n            AND a.[RetryCount] = 1\r\n            AND DATEDIFF(MINUTE, a.[ProcessDate], @Date) > 2\r\n\r\n          UNION\r\n          SELECT\r\n            a.*,\r\n            2\r\n          FROM dbo.[SystemEmail] a\r\n          WHERE a.[Status]      = 'Failed' \r\n            AND a.[DoRetry]     = 1  -- true \r\n            AND a.[RetryCount]  = 2\r\n            AND DATEDIFF(MINUTE, [ProcessDate], @Date) > 15\r\n\r\n          UNION\r\n          SELECT\r\n            a.*,\r\n            2\r\n          FROM dbo.[SystemEmail] a\r\n          WHERE a.[Status]     = 'Failed' \r\n            AND a.[DoRetry]    = 1  -- true \r\n            AND a.[RetryCount] = 3\r\n            AND DATEDIFF(MINUTE, [ProcessDate], @Date) > 60\t\t\t\r\n\r\n          UNION\r\n          SELECT\r\n            a.*,\r\n            3 \r\n          FROM dbo.[SystemEmail] a\r\n          WHERE a.[Status]      = 'Retry' \r\n            AND a.[DoRetry]     = 1  -- true \r\n            AND a.[RetryCount]  < 10\r\n\r\n          UNION\r\n          SELECT\r\n            a.*,\r\n            1 \r\n          FROM dbo.[SystemEmail] a\r\n          WHERE a.[Status]      = 'Processing' \r\n            AND a.[DoRetry]     = 1  -- true \r\n            AND a.[RetryCount]  < 10\r\n            AND DATEDIFF(MINUTE, [ProcessDate], @Date) > 15\r\n          )t\r\n    ORDER BY\r\n      t.[rank], \r\n      t.[SystemEmailID] DESC;\r\n\t            \t\r\n    --Set [Status] to processing so other servers running don't pick up the same message\r\n    UPDATE dbo.[SystemEmail]\r\n    SET [Status]      ='Processing',\r\n        [ProcessDate] = @Date,\r\n        [RetryCount]  = [RetryCount] + 1\r\n    WHERE [SystemEmailID] IN (SELECT [SystemEmailID] FROM #TEMP);\r\n\r\n    UPDATE #Temp SET ProcessDate = @Date, RetryCount = RetryCount +1, Status = 'Processing';\r\n\r\n    SELECT * FROM #TEMP;\r\n\r\nEND TRY\r\nBEGIN CATCH\r\n\r\n\r\n\tSET @ErrorNum = ERROR_NUMBER();\r\n\tSET @ErrorProcedure = ERROR_PROCEDURE();\r\n\tSET @ErrorSysMsg = ERROR_MESSAGE();\r\n\r\n\tDECLARE @Params VARCHAR(1000)\r\n  SET @Params = 'Limit: ' + CONVERT(VARCHAR(100), @Limit) +\r\n                'Date: ' + CONVERT(VARCHAR(100), @Date, 120)\r\n\r\n  INSERT INTO DBErrorLog (ProcName, Date, Urgency, Message, Params)\r\n  VALUES (@ProcName, GETUTCDATE(), 6, @ErrorSysMsg, @Params)\r\n\r\nEND CATCH\r\n")]
  internal class LoadReady : BaseDBMethod
  {
    [DBMethodParam("Limit", typeof (int))]
    public int Limit { get; private set; }

    [DBMethodParam("Date", typeof (DateTime))]
    public DateTime Date { get; private set; }

    public List<Monnit.SystemEmail> Result { get; private set; }

    public LoadReady(int limit)
    {
      this.Limit = limit;
      this.Date = DateTime.UtcNow;
      this.Result = BaseDBObject.Load<Monnit.SystemEmail>(this.ToDataTable());
    }
  }
}
