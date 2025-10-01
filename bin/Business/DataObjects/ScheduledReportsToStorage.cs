// Decompiled with JetBrains decompiler
// Type: Monnit.ScheduledReportsToStorage
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("ScheduledReportsToStorage")]
public class ScheduledReportsToStorage : BaseDBObject
{
  private long _ScheduledReportsToStorageID = long.MinValue;
  private string _GUID = string.Empty;
  private string _ReportFileName = string.Empty;
  private long _ReportScheduleResultID = long.MinValue;
  private DateTime _DateFileCreated = DateTime.MinValue;

  [DBProp("ScheduledReportsToStorageID", IsPrimaryKey = true)]
  public long ScheduledReportsToStorageID
  {
    get => this._ScheduledReportsToStorageID;
    set => this._ScheduledReportsToStorageID = value;
  }

  [DBProp("GUID")]
  public string GUID
  {
    get => this._GUID;
    set => this._GUID = value;
  }

  [DBProp("ReportFileName")]
  public string ReportFileName
  {
    get => this._ReportFileName;
    set => this._ReportFileName = value;
  }

  [DBProp("ReportScheduleResultID")]
  [DBForeignKey("ReportScheduleResult", "ReportScheduleResultID")]
  public long ReportScheduleResultID
  {
    get => this._ReportScheduleResultID;
    set => this._ReportScheduleResultID = value;
  }

  [DBProp("DateFileCreated")]
  public DateTime DateFileCreated
  {
    get => this._DateFileCreated;
    set => this._DateFileCreated = value;
  }

  public static ScheduledReportsToStorage Load(long ID)
  {
    return BaseDBObject.Load<ScheduledReportsToStorage>(ID);
  }

  public static List<ScheduledReportsToStorage> LoadAll()
  {
    return BaseDBObject.LoadAll<ScheduledReportsToStorage>();
  }

  public static List<ScheduledReportsToStorage> LoadByReportScheduleResultID(
    long reportScheduleResultID)
  {
    return BaseDBObject.LoadByForeignKey<ScheduledReportsToStorage>("ReportScheduleResultID", (object) reportScheduleResultID);
  }

  public static List<ScheduledReportsToStorage> SafeToDeleteList(DateTime lessThanThisDate)
  {
    return new Monnit.Data.ScheduledReportsToStorage.ListOfSafeToDeleteReports(lessThanThisDate).Result;
  }

  public static int DeleteBatchOfReports()
  {
    int num = 0;
    try
    {
      long reportsToStorageId = ConfigData.AppSettings("BatchStartScheduledReportsToStorageID").ToLong();
      foreach (ScheduledReportsToStorage reportsToStorage in ScheduledReportsToStorage.BatchOfReportsToDelete(reportsToStorageId))
      {
        if (AzureTempFile.DeleteFile($"reportfile/{reportsToStorage.GUID.ToString()}/", reportsToStorage.ReportFileName))
        {
          reportsToStorage.Delete();
          ++num;
        }
        else
          reportsToStorageId = reportsToStorage.ScheduledReportsToStorageID;
      }
      ConfigData.SetAppSettings("BatchStartScheduledReportsToStorageID", reportsToStorageId.ToString());
    }
    catch (Exception ex)
    {
      ex.Log("ScheduledReportsToStorage.DeleteBatchOfReports ");
    }
    return num;
  }

  private static List<ScheduledReportsToStorage> BatchOfReportsToDelete(
    long batchStartScheduledReportsToStorageID)
  {
    return new Monnit.Data.ScheduledReportsToStorage.BatchOfReportsToDelete(batchStartScheduledReportsToStorageID).Result;
  }
}
