// Decompiled with JetBrains decompiler
// Type: Monnit.DataTableStatistics
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit;

public class DataTableStatistics : BaseDBObject
{
  private long _DataTableStatisticsID = long.MinValue;
  private string _TableName = string.Empty;
  private DateTime _EvaluationDate = DateTime.MinValue;
  private int _TotalRowCount = int.MinValue;
  private int _SecondsSinceLastEvaluation = int.MinValue;
  private int _RowsSincelastEvaluation = int.MinValue;
  private double _Rate = double.MinValue;

  [DBProp("DataTableStatisticsID", IsPrimaryKey = true)]
  public long DataTableStatisticsID
  {
    get => this._DataTableStatisticsID;
    set => this._DataTableStatisticsID = value;
  }

  [DBProp("TableName", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public new string TableName
  {
    get => this._TableName;
    set => this._TableName = value;
  }

  [DBProp("EvaluationDate")]
  public DateTime EvaluationDate
  {
    get => this._EvaluationDate;
    set => this._EvaluationDate = value;
  }

  [DBProp("TotalRowCount")]
  public int TotalRowCount
  {
    get => this._TotalRowCount;
    set => this._TotalRowCount = value;
  }

  [DBProp("SecondsSinceLastEvaluation")]
  public int SecondsSinceLastEvaluation
  {
    get => this._SecondsSinceLastEvaluation;
    set => this._SecondsSinceLastEvaluation = value;
  }

  [DBProp("RowsSincelastEvaluation")]
  public int RowsSincelastEvaluation
  {
    get => this._RowsSincelastEvaluation;
    set => this._RowsSincelastEvaluation = value;
  }

  [DBProp("Rate")]
  public double Rate
  {
    get => this._Rate;
    set => this._Rate = value;
  }

  public static bool InsertData() => new Monnit.Data.DataTableStatistics.InsertData().Result;
}
