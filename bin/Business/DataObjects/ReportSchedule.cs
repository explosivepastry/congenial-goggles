// Decompiled with JetBrains decompiler
// Type: Monnit.ReportSchedule
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("ReportSchedule")]
public class ReportSchedule : BaseDBObject
{
  private long _ReportScheduleID = long.MinValue;
  private long _AccountID = long.MinValue;
  private long _ReportQueryID = long.MinValue;
  private string _Name = string.Empty;
  private DateTime _StartDate = DateTime.MinValue;
  private DateTime _EndDate = DateTime.MinValue;
  private eReportScheduleType _ScheduleType = eReportScheduleType.Monthly;
  private TimeSpan _StartTime = TimeSpan.MinValue;
  private string _Schedule = string.Empty;
  private DateTime _LastRunDate = DateTime.MinValue;
  private long _LastReportScheduleResultID = long.MinValue;
  private bool _IsDeleted = false;
  private bool _IsActive = true;
  private DateTime _ProccessingStartDate = DateTime.MinValue;
  private bool _SendAsAttachment = false;
  private ReportQuery _Report;
  private Account _Account;
  private List<ReportParameterValue> _ParameterValues = (List<ReportParameterValue>) null;
  private List<ReportDistribution> _DistributionList = (List<ReportDistribution>) null;

  [DBProp("ReportScheduleID", IsPrimaryKey = true)]
  public long ReportScheduleID
  {
    get => this._ReportScheduleID;
    set => this._ReportScheduleID = value;
  }

  [DBProp("AccountID")]
  [DBForeignKey("Account", "AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("ReportQueryID")]
  [DBForeignKey("ReportQuery", "ReportQueryID")]
  public long ReportQueryID
  {
    get => this._ReportQueryID;
    set => this._ReportQueryID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("StartDate")]
  public DateTime StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [DBProp("EndDate")]
  public DateTime EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [DBProp("ScheduleType")]
  public eReportScheduleType ScheduleType
  {
    get => this._ScheduleType;
    set => this._ScheduleType = value;
  }

  [DBProp("StartTime")]
  public TimeSpan StartTime
  {
    get => this._StartTime;
    set => this._StartTime = value;
  }

  [DBProp("Schedule", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Schedule
  {
    get => this._Schedule;
    set => this._Schedule = value;
  }

  [DBProp("LastRunDate")]
  public DateTime LastRunDate
  {
    get => this._LastRunDate;
    set => this._LastRunDate = value;
  }

  [DBProp("LastReportScheduleResultID")]
  [DBForeignKey("ReportScheduleResult", "ReportScheduleResultID")]
  public long LastReportScheduleResultID
  {
    get => this._LastReportScheduleResultID;
    set => this._LastReportScheduleResultID = value;
  }

  [DBProp("IsDeleted")]
  public bool IsDeleted
  {
    get => this._IsDeleted;
    set => this._IsDeleted = value;
  }

  [DBProp("IsActive", DefaultValue = true)]
  public bool IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [DBProp("ProcessingStartDate", DefaultValue = true)]
  public DateTime ProccessingStartDate
  {
    get => this._ProccessingStartDate;
    set => this._ProccessingStartDate = value;
  }

  [DBProp("SendAsAttachment", DefaultValue = false)]
  public bool SendAsAttachment
  {
    get => this._SendAsAttachment;
    set => this._SendAsAttachment = value.ToBool();
  }

  public static ReportSchedule Load(long id) => BaseDBObject.Load<ReportSchedule>(id);

  public override void Delete()
  {
    this.IsDeleted = true;
    this.Save();
  }

  public static List<ReportSchedule> LoadByAccount(long accountID)
  {
    return BaseDBObject.LoadByForeignKey<ReportSchedule>("AccountID", (object) accountID).Where<ReportSchedule>((Func<ReportSchedule, bool>) (r => !r.IsDeleted)).ToList<ReportSchedule>();
  }

  public static List<ReportSchedule> LoadByCustomerID(long customerID)
  {
    return new Monnit.Data.ReportSchedule.LoadByCustomerID(customerID).Result;
  }

  public static List<ReportSchedule> LoadReady() => Monnit.Data.ReportSchedule.LoadReady.Exec();

  public ReportQuery Report
  {
    get
    {
      if (this._Report == null)
        this._Report = ReportQuery.Load(this.ReportQueryID);
      return this._Report;
    }
  }

  public Account Account
  {
    get
    {
      if (this._Account == null)
        this._Account = Account.Load(this.AccountID);
      return this._Account;
    }
  }

  public TimeSpan LocalTime
  {
    get
    {
      return new TimeSpan(TimeZone.GetLocalTimeById(DateTime.UtcNow.SetTime(this.StartTime.Hours, 0, 0), this.Account.TimeZoneID).Hour, 0, 0);
    }
    set
    {
      this.StartTime = new TimeSpan(TimeZone.GetUTCFromLocalById(DateTime.SpecifyKind(DateTime.UtcNow.SetTime(value.Hours, 0, 0), DateTimeKind.Unspecified), this.Account.TimeZoneID).Hour, 0, 0);
    }
  }

  public ReportDistribution AddCustomer(Customer cust)
  {
    if (cust == null)
      return (ReportDistribution) null;
    foreach (ReportDistribution distribution in this.DistributionList)
    {
      if (distribution.CustomerID == cust.CustomerID)
        return distribution;
    }
    ReportDistribution reportDistribution = new ReportDistribution();
    reportDistribution.ReportScheduleID = this.ReportScheduleID;
    reportDistribution.CustomerID = cust.CustomerID;
    reportDistribution.Save();
    this.DistributionList.Add(reportDistribution);
    return reportDistribution;
  }

  public void RemoveCustomer(Customer customer)
  {
    if (customer == null)
      return;
    for (int index = this.DistributionList.Count<ReportDistribution>() - 1; index >= 0; --index)
    {
      ReportDistribution distribution = this.DistributionList[index];
      if (distribution.CustomerID == customer.CustomerID)
      {
        distribution.Delete();
        this.DistributionList.Remove(distribution);
      }
    }
  }

  public ReportParameterValue FindParameter(long reportParameterID)
  {
    if (this._ParameterValues == null)
      this._ParameterValues = ReportParameterValue.LoadBySchedule(this.ReportScheduleID);
    return this._ParameterValues.FirstOrDefault<ReportParameterValue>((Func<ReportParameterValue, bool>) (pv => pv.ReportParameterID == reportParameterID));
  }

  public string FindParameterValue(long reportParameterID)
  {
    ReportParameterValue parameter = this.FindParameter(reportParameterID);
    if (parameter != null)
      return parameter.Value.ToStringSafe();
    ReportParameter reportParameter = this.Report.Parameters.FirstOrDefault<ReportParameter>((Func<ReportParameter, bool>) (pv => pv.ReportParameterID == reportParameterID));
    return reportParameter != null ? reportParameter.DefaultValue.ToStringSafe() : "";
  }

  public List<ReportDistribution> DistributionList
  {
    get
    {
      if (this._DistributionList == null)
        this._DistributionList = ReportDistribution.LoadBySchedule(this.ReportScheduleID);
      return this._DistributionList;
    }
  }
}
