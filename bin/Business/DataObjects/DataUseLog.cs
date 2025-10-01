// Decompiled with JetBrains decompiler
// Type: Monnit.DataUseLog
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Data;

#nullable disable
namespace Monnit;

[DBClass("DataUseLog")]
public class DataUseLog : BaseDBObject
{
  private long _DataUseLogID = long.MinValue;
  private long _GatewayID = long.MinValue;
  public string _CellID = string.Empty;
  private DateTime _Date = DateTime.MinValue;
  private double _MBUsed = double.MinValue;
  public string _DeviceStatus = string.Empty;
  private long _DataUseListID = long.MinValue;

  [DBProp("DataUseLogID", IsPrimaryKey = true)]
  public long DataUseLogID
  {
    get => this._DataUseLogID;
    set => this._DataUseLogID = value;
  }

  [DBProp("GatewayID", AllowNull = true)]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("CellID", MaxLength = 3000)]
  public string CellID
  {
    get => this._CellID;
    set => this._CellID = value;
  }

  [DBProp("Date")]
  public DateTime Date
  {
    get => this._Date;
    set => this._Date = value;
  }

  [DBProp("MBUsed")]
  public double MBUsed
  {
    get => this._MBUsed;
    set => this._MBUsed = value;
  }

  [DBProp("DeviceStatus", MaxLength = 3000)]
  public string DeviceStatus
  {
    get => this._DeviceStatus;
    set => this._DeviceStatus = value;
  }

  [DBProp("DataUseListID", AllowNull = true)]
  public long DataUseListID
  {
    get => this._DataUseListID;
    set => this._DataUseListID = value;
  }

  public static void CalculateDailyUse()
  {
    Monnit.Data.DataUseLog.CalculateDailyUse calculateDailyUse = new Monnit.Data.DataUseLog.CalculateDailyUse();
  }

  public static DataTable LoadDailyByGateway(long gatewayID, DateTime fromDate, DateTime toDate)
  {
    return new Monnit.Data.DataUseLog.LoadDailyByGateway(gatewayID, fromDate, toDate).Result;
  }

  public static DataTable LoadMonthlyByGateway(long gatewayID, DateTime fromDate, DateTime toDate)
  {
    return new Monnit.Data.DataUseLog.LoadMonthlyByGateway(gatewayID, fromDate, toDate).Result;
  }
}
