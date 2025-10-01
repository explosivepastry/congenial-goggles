// Decompiled with JetBrains decompiler
// Type: Monnit.PreAggregatedData
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit;

[DBClass("PreAggregatedData")]
public class PreAggregatedData : BaseDBObject
{
  private Guid _PASensorGUID = Guid.Empty;
  private DateTime _Date = DateTime.MinValue;
  private long _SensorID = long.MinValue;
  private long _AccountID = long.MinValue;
  private string _ApplicationName = string.Empty;
  private int _MonnitApplicationID = int.MinValue;
  private long _CSNetID = long.MinValue;
  private string _SplitValue = string.Empty;
  private int _FalseCount = int.MinValue;
  private double _Min = double.MinValue;
  private double _Max = double.MinValue;
  private double _Avg = double.MinValue;
  private int _TrueCount = int.MinValue;
  private double _Avg_Voltage = double.MinValue;
  private long _SensorMessageCounts = long.MinValue;
  private int _AwareStateCounts = int.MinValue;
  private DateTime _CreateDate = DateTime.MinValue;
  private double _Avg_SignalStrength = double.MinValue;

  [DBProp("PASensorGUID", IsPrimaryKey = true, IsGuidPrimaryKey = true, AllowNull = false)]
  public Guid PASensorGUID
  {
    get => this._PASensorGUID;
    set => this._PASensorGUID = value;
  }

  [DBProp("Date", AllowNull = false)]
  public DateTime Date
  {
    get => this._Date;
    set => this._Date = value;
  }

  [DBProp("SensorID", AllowNull = false)]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("AccountID", AllowNull = true)]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("ApplicationName", MaxLength = 100, AllowNull = true)]
  public string ApplicationName
  {
    get => this._ApplicationName;
    set => this._ApplicationName = value;
  }

  [DBProp("ApplicationID", AllowNull = true)]
  public int MonnitApplicationID
  {
    get => this._MonnitApplicationID;
    set => this._MonnitApplicationID = value;
  }

  [DBProp("CSNetID", AllowNull = true)]
  public long CSNetID
  {
    get => this._CSNetID;
    set => this._CSNetID = value;
  }

  [DBProp("SplitValue", MaxLength = 100, AllowNull = true)]
  public string SplitValue
  {
    get => this._SplitValue;
    set => this._SplitValue = value;
  }

  [DBProp("FalseCount", AllowNull = true)]
  public int FalseCount
  {
    get => this._FalseCount;
    set => this._FalseCount = value;
  }

  [DBProp("Min", AllowNull = true)]
  public double Min
  {
    get => this._Min;
    set => this._Min = value;
  }

  [DBProp("Max", AllowNull = true)]
  public double Max
  {
    get => this._Max;
    set => this._Max = value;
  }

  [DBProp("Avg", AllowNull = true)]
  public double Avg
  {
    get => this._Avg;
    set => this._Avg = value;
  }

  [DBProp("TrueCount", AllowNull = true)]
  public int TrueCount
  {
    get => this._TrueCount;
    set => this._TrueCount = value;
  }

  [DBProp("Avg_Voltage", AllowNull = true)]
  public double Avg_Voltage
  {
    get => this._Avg_Voltage;
    set => this._Avg_Voltage = value;
  }

  [DBProp("SensorMessageCounts", AllowNull = true)]
  public long SensorMessageCounts
  {
    get => this._SensorMessageCounts;
    set => this._SensorMessageCounts = value;
  }

  [DBProp("AwareStateCounts", AllowNull = true)]
  public int AwareStateCounts
  {
    get => this._AwareStateCounts;
    set => this._AwareStateCounts = value;
  }

  [DBProp("CreateDate", AllowNull = true)]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("Avg_SignalStrength", AllowNull = true)]
  public double Avg_SignalStrength
  {
    get => this._Avg_SignalStrength;
    set => this._Avg_SignalStrength = value;
  }

  public static PreAggregatedData Load(long id) => BaseDBObject.Load<PreAggregatedData>(id);

  public static DataSet LoadPageBySensorIDAndDateRange(
    long sensorID,
    DateTime fromDate,
    DateTime toDate)
  {
    return Monnit.Data.PreAggregatedData.LoadPageBySensorIDAndDateRange.Exec(sensorID, fromDate, toDate);
  }

  public static List<PreAggregatedData> PreAggChartByDate(
    long sensorID,
    DateTime utcStartDate,
    DateTime localDate)
  {
    return Monnit.Data.PreAggregatedData.PreAggChartByDate.Exec(sensorID, utcStartDate, localDate);
  }

  public static List<PreAggregatedData> LoadBySensorIDAndDateRange(
    long sensorID,
    DateTime fromDate,
    DateTime toDate)
  {
    return new Monnit.Data.PreAggregatedData.LoadBySensorIDAndDateRange(sensorID, fromDate, toDate).Result;
  }
}
