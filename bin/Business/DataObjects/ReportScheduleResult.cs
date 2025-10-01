// Decompiled with JetBrains decompiler
// Type: Monnit.ReportScheduleResult
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("ReportScheduleResult")]
public class ReportScheduleResult : BaseDBObject
{
  private long _ReportScheduleResultID = long.MinValue;
  private long _ReportScheduleID = long.MinValue;
  private DateTime _RunDate = DateTime.MinValue;
  private eReportScheduleResultType _ResultType = eReportScheduleResultType.Success;
  private string _Result = string.Empty;
  private TimeSpan _QueryRuntime = TimeSpan.MinValue;
  private TimeSpan _ProcessingRuntime = TimeSpan.MinValue;
  private string _Recipients = string.Empty;
  private List<ScheduledReportsToStorage> _ReportFiles;

  [DBProp("ReportScheduleResultID", IsPrimaryKey = true)]
  public long ReportScheduleResultID
  {
    get => this._ReportScheduleResultID;
    set => this._ReportScheduleResultID = value;
  }

  [DBProp("ReportScheduleID")]
  [DBForeignKey("ReportSchedule", "ReportScheduleID")]
  public long ReportScheduleID
  {
    get => this._ReportScheduleID;
    set => this._ReportScheduleID = value;
  }

  [DBProp("RunDate")]
  public DateTime RunDate
  {
    get => this._RunDate;
    set => this._RunDate = value;
  }

  [DBProp("ResultType")]
  public eReportScheduleResultType ResultType
  {
    get => this._ResultType;
    set => this._ResultType = value;
  }

  [DBProp("Result", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Result
  {
    get => this._Result;
    set => this._Result = value;
  }

  [DBProp("QueryRuntime")]
  public TimeSpan QueryRuntime
  {
    get => this._QueryRuntime;
    set => this._QueryRuntime = value;
  }

  [DBProp("ProcessingRuntime")]
  public TimeSpan ProcessingRuntime
  {
    get => this._ProcessingRuntime;
    set => this._ProcessingRuntime = value;
  }

  [DBProp("Recipients", MaxLength = 2000, AllowNull = true)]
  public string Recipients
  {
    get => this._Recipients;
    set => this._Recipients = value;
  }

  public List<ScheduledReportsToStorage> ReportFiles
  {
    get
    {
      if (this._ReportFiles == null)
        this._ReportFiles = ScheduledReportsToStorage.LoadByReportScheduleResultID(this.ReportScheduleResultID);
      return this._ReportFiles;
    }
  }

  public static ReportScheduleResult Load(long id) => BaseDBObject.Load<ReportScheduleResult>(id);

  public static (List<ReportScheduleResult> ReportScheduleResultList, List<ScheduledReportsToStorage> ScheduledReportsToStorageList) ReportHistoryByReportScheduleID(
    long reportScheduleID)
  {
    return new Monnit.Data.ReportScheduleResult.ReportHistoryByReportScheduleID(reportScheduleID).Result;
  }
}
