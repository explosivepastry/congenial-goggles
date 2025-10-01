// Decompiled with JetBrains decompiler
// Type: Monnit.Data.ReportScheduleResult
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class ReportScheduleResult
{
  [DBMethod("ReportScheduleResult_ReportHistoryByReportScheduleID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP 20\r\n\t* INTO #rsr_tmp\r\nFROM dbo.[ReportScheduleResult] rsr WITH (NOLOCK)\r\nWHERE rsr.ReportScheduleID = @ReportScheduleID\r\nAND rsr.RunDate > DATEADD(MONTH, -1, GETDATE())\r\nORDER BY rsr.RunDate DESC;\r\n\r\nSELECT\r\n\tsrs.* INTO #srs_tmp\r\nFROM #rsr_tmp\r\nJOIN ScheduledReportsToStorage srs WITH (NOLOCK)\r\n\tON srs.ReportScheduleResultID = #rsr_tmp.ReportScheduleResultID\r\n\t\tAND srs.DateFileCreated > DATEADD(MONTH, -1, GETDATE())\r\nORDER BY #rsr_tmp.RunDate DESC;\r\n\r\nSELECT\r\n\t*\r\nFROM #rsr_tmp;\r\n\r\nSELECT\r\n\t*\r\nFROM #srs_tmp;\r\n\r\nDROP TABLE #rsr_tmp;\r\nDROP TABLE #srs_tmp;\r\n")]
  internal class ReportHistoryByReportScheduleID : BaseDBMethod
  {
    [DBMethodParam("ReportScheduleID", typeof (long))]
    public long ReportScheduleID { get; private set; }

    public (List<Monnit.ReportScheduleResult> ReportScheduleResultList, List<Monnit.ScheduledReportsToStorage> ScheduledReportsToStorageList) Result { get; private set; }

    public ReportHistoryByReportScheduleID(long reportScheduleID)
    {
      this.ReportScheduleID = reportScheduleID;
      List<Monnit.ReportScheduleResult> reportScheduleResultList = new List<Monnit.ReportScheduleResult>();
      List<Monnit.ScheduledReportsToStorage> reportsToStorageList = new List<Monnit.ScheduledReportsToStorage>();
      DataSet dataSet = this.ToDataSet();
      if (dataSet != null && dataSet.Tables.Count > 0)
        reportScheduleResultList.AddRange((IEnumerable<Monnit.ReportScheduleResult>) BaseDBObject.Load<Monnit.ReportScheduleResult>(dataSet.Tables[0]));
      if (dataSet != null && dataSet.Tables.Count > 1)
        reportsToStorageList.AddRange((IEnumerable<Monnit.ScheduledReportsToStorage>) BaseDBObject.Load<Monnit.ScheduledReportsToStorage>(dataSet.Tables[1]));
      this.Result = (reportScheduleResultList, reportsToStorageList);
    }
  }
}
