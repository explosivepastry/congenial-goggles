// Decompiled with JetBrains decompiler
// Type: Monnit.Data.ReportSchedule
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class ReportSchedule
{
  [DBMethod("ReportSchedule_LoadReady")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @ProcName NVARCHAR(50);\r\n\r\nDECLARE @ErrorNum         INT,          \r\n        @ErrorProcedure   NVARCHAR(50), \r\n        @ErrorSysMsg      NVARCHAR(MAX);\r\n\r\nDECLARE @UTC DATETIME;\r\nSET @UTC = GETUTCDATE();\r\n\r\nBEGIN TRY\r\n\r\n    SET @ProcName = OBJECT_NAME(@@PROCID);\r\n\r\n    UPDATE dbo.[ReportSchedule]\r\n      SET ProcessingStartDate = @UTC\r\n    WHERE ReportScheduleID IN ( SELECT\r\n                                  r.ReportScheduleID\r\n                                FROM dbo.[ReportSchedule] r\r\n                                WHERE r.StartDate             < DATEADD(DAY, 1,@UTC)\r\n                                  AND r.EndDate               > DATEADD(DAY,-1,@UTC)\r\n                                  AND (r.ProcessingStartDate IS NULL OR r.ProcessingStartDate < DATEADD(HOUR,-1,@UTC))\r\n                                  AND (r.LastRunDate         IS NULL OR r.LastRunDate         < DATEADD(HOUR,-1,@UTC))\r\n                                  AND (DATEPART(HOUR,r.StartTime) = DATEPART(HOUR,@UTC) OR r.ScheduleType = 4) -- Immediately\r\n                                  AND (r.IsDeleted = 0 OR r.IsDeleted IS NULL)\r\n                                  AND (r.IsActive  = 1 OR r.IsActive  IS NULL)\r\n                                  AND EXISTS (SELECT TOP 1 * FROM dbo.[ReportDistribution] rd WHERE rd.ReportScheduleID = r.ReportScheduleID));\r\n\r\n    SELECT\r\n      *\r\n    FROM dbo.[ReportSchedule]\r\n    WHERE ProcessingStartDate = @UTC;\r\n\r\n\r\nEND TRY\r\nBEGIN CATCH\r\n\r\n\tSET @ErrorNum = ERROR_NUMBER();\r\n\tSET @ErrorProcedure = ERROR_PROCEDURE();\r\n\tSET @ErrorSysMsg = ERROR_MESSAGE();\r\n\r\n\tDECLARE @Recipients varchar(500)\r\n\tDECLARE @Subject varchar(30)\r\n\tDECLARE @Body VARCHAR(2000)\r\n\r\n  INSERT INTO DBErrorLog (ProcName, Date, Urgency, Message, Params)\r\n  VALUES (@ProcName, GETUTCDATE(), 3, @ErrorSysMsg, null)\r\n\r\n\tSET @Body = '<p>Team, </p> <p>Critical Procedure Failed: '+@ProcName+'. Please Address. '+CONVERT(VARCHAR(20), GETDATE() )+' </p> \r\n  <p>ErrorMessage: '+CONVERT(VARCHAR(20), @ErrorNum) +' ' + @ErrorSysMsg+'</p>\r\n\t<p>Sincerely,</p><p>-DBA</p>'\r\n\r\n\tSET @Subject = 'MonnitDB ProcFail - Urgency 3'\r\n\tSET @Recipients = (select value from ConfigData where KeyName = 'DB_Procfail_Contacts')\r\n\r\n    EXEC msdb.dbo.sp_send_dbmail \r\n\t  @Profile_name = 'Alerts',\r\n\t  @Recipients = @Recipients , \r\n      @subject = @Subject,  \r\n      @body = @Body,  \r\n      @body_format = 'HTML' ;  \r\n\r\nEND CATCH\r\n")]
  internal class LoadReady : BaseDBMethod
  {
    public List<Monnit.ReportSchedule> Result { get; private set; }

    public LoadReady() => this.Result = BaseDBObject.Load<Monnit.ReportSchedule>(this.ToDataTable());

    public static List<Monnit.ReportSchedule> Exec() => new ReportSchedule.LoadReady().Result;
  }

  [DBMethod("ReportSchedule_LoadByCustomerID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT s.* FROM ReportSchedule s\r\nINNER JOIN ReportDistribution d\r\n\tON d.ReportScheduleID = s.ReportScheduleID\r\nWHERE d.CustomerID = @CustomerID\t\r\nAND s.IsDeleted = 0\r\n")]
  internal class LoadByCustomerID : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    public List<Monnit.ReportSchedule> Result { get; private set; }

    public LoadByCustomerID(long customerID)
    {
      this.CustomerID = customerID;
      this.Result = BaseDBObject.Load<Monnit.ReportSchedule>(this.ToDataTable());
    }
  }
}
