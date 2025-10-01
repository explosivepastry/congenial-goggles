// Decompiled with JetBrains decompiler
// Type: Monnit.ReportParameterValue
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("ReportParameterValue")]
public class ReportParameterValue : BaseDBObject
{
  private long _ReportParameterValueID = long.MinValue;
  private long _ReportParameterID = long.MinValue;
  private long _ReportScheduleID = long.MinValue;
  private string _Value = string.Empty;

  [DBProp("ReportParameterValueID", IsPrimaryKey = true)]
  public long ReportParameterValueID
  {
    get => this._ReportParameterValueID;
    set => this._ReportParameterValueID = value;
  }

  [DBProp("ReportParameterID")]
  [DBForeignKey("ReportParameter", "ReportParameterID")]
  public long ReportParameterID
  {
    get => this._ReportParameterID;
    set => this._ReportParameterID = value;
  }

  [DBProp("ReportScheduleID")]
  [DBForeignKey("ReportSchedule", "ReportScheduleID")]
  public long ReportScheduleID
  {
    get => this._ReportScheduleID;
    set => this._ReportScheduleID = value;
  }

  [DBProp("Value", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  public static ReportParameterValue Load(long id) => BaseDBObject.Load<ReportParameterValue>(id);

  public static List<ReportParameterValue> LoadBySchedule(long reportScheduleID)
  {
    return BaseDBObject.LoadByForeignKey<ReportParameterValue>("ReportScheduleID", (object) reportScheduleID);
  }

  public static List<ReportParameterValue> LoadByReportParameterID(long reportParameterID)
  {
    return BaseDBObject.LoadByForeignKey<ReportParameterValue>("ReportParameterID", (object) reportParameterID);
  }
}
