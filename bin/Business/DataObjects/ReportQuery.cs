// Decompiled with JetBrains decompiler
// Type: Monnit.ReportQuery
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

#nullable disable
namespace Monnit;

[DBClass("ReportQuery")]
public class ReportQuery : BaseDBObject
{
  private long _ReportQueryID = long.MinValue;
  private string _Name = string.Empty;
  private string _Description = string.Empty;
  private long _AccountID = long.MinValue;
  private long _AccountThemeID = long.MinValue;
  private string _ReportBuilder = string.Empty;
  private string _SQL = string.Empty;
  private bool _IsDeleted = false;
  private bool _RequiresPreAggs = false;
  private bool _ScheduleAnnually = true;
  private bool _ScheduleMonthly = true;
  private bool _ScheduleWeekly = true;
  private bool _ScheduleDaily = true;
  private bool _ScheduleImmediately = true;
  private eCustomerType _CustomerAccess = eCustomerType.Everyone;
  private int _MaxRunTime = 60;
  private int _SensorLimit = int.MinValue;
  private string _Tags = string.Empty;
  private long _ReportTypeID = long.MinValue;
  private ReportType _Type = (ReportType) null;
  private List<ReportParameter> _Parameters;

  [DBProp("ReportQueryID", IsPrimaryKey = true)]
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

  [DBProp("Description", MaxLength = 2000, AllowNull = true)]
  public string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [DBProp("AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value > 0L ? value : long.MinValue;
  }

  [DBProp("AccountThemeID")]
  public long AccountThemeID
  {
    get => this._AccountThemeID;
    set => this._AccountThemeID = value > 0L ? value : long.MinValue;
  }

  [DBProp("ReportBuilder", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string ReportBuilder
  {
    get => this._ReportBuilder;
    set => this._ReportBuilder = value;
  }

  [AllowHtml]
  [NoScanHtmlProperty]
  [DBProp("SQL", MaxLength = 2147483647 /*0x7FFFFFFF*/, AllowNull = true)]
  public string SQL
  {
    get => this._SQL;
    set => this._SQL = value;
  }

  [DBProp("IsDeleted")]
  public bool IsDeleted
  {
    get => this._IsDeleted;
    set => this._IsDeleted = value;
  }

  [DBProp("RequiresPreAggs")]
  public bool RequiresPreAggs
  {
    get => this._RequiresPreAggs;
    set => this._RequiresPreAggs = value;
  }

  [DBProp("ScheduleAnnually")]
  public bool ScheduleAnnually
  {
    get => this._ScheduleAnnually;
    set => this._ScheduleAnnually = value;
  }

  [DBProp("ScheduleMonthly")]
  public bool ScheduleMonthly
  {
    get => this._ScheduleMonthly;
    set => this._ScheduleMonthly = value;
  }

  [DBProp("ScheduleWeekly")]
  public bool ScheduleWeekly
  {
    get => this._ScheduleWeekly;
    set => this._ScheduleWeekly = value;
  }

  [DBProp("ScheduleDaily")]
  public bool ScheduleDaily
  {
    get => this._ScheduleDaily;
    set => this._ScheduleDaily = value;
  }

  [DBProp("ScheduleImmediately")]
  public bool ScheduleImmediately
  {
    get => this._ScheduleImmediately;
    set => this._ScheduleImmediately = value;
  }

  [DBProp("CustomerAccess", DefaultValue = 0, AllowNull = false)]
  public eCustomerType CustomerAccess
  {
    get => this._CustomerAccess;
    set => this._CustomerAccess = value;
  }

  [DBProp("MaxRunTime", DefaultValue = 60)]
  public int MaxRunTime
  {
    get => this._MaxRunTime > 0 ? this._MaxRunTime : 60;
    set => this._MaxRunTime = value;
  }

  [DBProp("SensorLimit")]
  public int SensorLimit
  {
    get => this._SensorLimit > 0 ? this._SensorLimit : 999997;
    set => this._SensorLimit = value;
  }

  [DBProp("Tags", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Tags
  {
    get => this._Tags;
    set => this._Tags = value;
  }

  [DBForeignKey("ReportType", "ReportTypeID")]
  [DBProp("ReportTypeID", AllowNull = true)]
  public long ReportTypeID
  {
    get => this._ReportTypeID;
    set => this._ReportTypeID = value;
  }

  public ReportType Type
  {
    get
    {
      if (this._Type == null)
        this._Type = ReportType.Load(this.ReportTypeID);
      return ReportType.Load(this.ReportTypeID);
    }
  }

  public List<ReportParameter> Parameters
  {
    get
    {
      if (this._Parameters == null)
        this._Parameters = ReportParameter.LoadByReportQuery(this.ReportQueryID).OrderBy<ReportParameter, int>((Func<ReportParameter, int>) (p => p.SortOrder)).ToList<ReportParameter>();
      return this._Parameters;
    }
  }

  public static ReportQuery Load(long id) => BaseDBObject.Load<ReportQuery>(id);

  public static List<ReportQuery> LoadAll() => BaseDBObject.LoadAll<ReportQuery>();

  public static List<ReportQuery> LoadByType(long? id)
  {
    return !id.HasValue ? ReportQuery.LoadAll() : BaseDBObject.LoadByForeignKey<ReportQuery>("ReportTypeID", (object) id.GetValueOrDefault());
  }

  public static List<ReportQuery> LoadAvailable(long accountID, long themeID)
  {
    List<ReportQuery> reportQueryList = new List<ReportQuery>();
    foreach (ReportQuery reportQuery in ReportQuery.LoadAll())
    {
      if (!reportQuery.IsDeleted && (reportQuery.AccountID <= 0L || reportQuery.AccountID == accountID) && (reportQuery.AccountThemeID <= 0L || reportQuery.AccountThemeID == themeID))
        reportQueryList.Add(reportQuery);
    }
    return reportQueryList;
  }

  private static bool CanSeeReport(string[] accountTags, ReportQuery query)
  {
    bool flag = true;
    if (query.Tags.Length > 0)
    {
      flag = false;
      string tags = query.Tags;
      string[] separator = new string[1]{ "|" };
      foreach (string str in tags.Split(separator, StringSplitOptions.RemoveEmptyEntries))
      {
        foreach (string accountTag in accountTags)
        {
          if (accountTag.ToLower() == str.ToLower())
            flag = true;
        }
      }
    }
    return flag;
  }

  public override void Delete()
  {
    this.IsDeleted = true;
    this.Save();
  }

  public static List<ReportQuery> LoadByAccount(
    long accountThemeID,
    long accountID,
    bool isPremiere,
    long reportTypeID)
  {
    return new Monnit.Data.ReportQuery.LoadByAccount(accountThemeID, accountID, isPremiere, reportTypeID).Result;
  }
}
