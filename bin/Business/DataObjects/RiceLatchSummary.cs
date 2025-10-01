// Decompiled with JetBrains decompiler
// Type: Monnit.RiceLatchSummary
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

public class RiceLatchSummary : BaseDBObject
{
  private long _RiceLatchSummaryID = long.MinValue;
  private long _AccountID = long.MinValue;
  private long _SensorID = long.MinValue;
  private DateTime _SummaryDate = DateTime.MinValue;
  private int _RiseTimeMin = int.MaxValue;
  private int _RiseTimeMax = int.MinValue;
  private int _RiseTimeAvg = int.MinValue;
  private int _FallTimeMin = int.MaxValue;
  private int _FallTimeMax = int.MinValue;
  private int _FallTimeAvg = int.MinValue;
  private int _CycleCount = 0;

  public long RiceLatchSummaryID
  {
    get => this._RiceLatchSummaryID;
    set => this._RiceLatchSummaryID = value;
  }

  [DBProp("AccountID")]
  [DBForeignKey("Account", "AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("SensorID")]
  [DBForeignKey("Sensor", "SensorID")]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("SummaryDate")]
  public DateTime SummaryDate
  {
    get => this._SummaryDate;
    set => this._SummaryDate = value;
  }

  [DBProp("RiseTimeMin")]
  public int RiseTimeMin
  {
    get => this._RiseTimeMin;
    set => this._RiseTimeMin = value;
  }

  [DBProp("RiseTimeMax")]
  public int RiseTimeMax
  {
    get => this._RiseTimeMax == int.MinValue ? 0 : this._RiseTimeMax;
    set => this._RiseTimeMax = value;
  }

  [DBProp("RiseTimeAvg")]
  public int RiseTimeAvg
  {
    get => this._RiseTimeAvg == int.MinValue ? 0 : this._RiseTimeAvg;
    set => this._RiseTimeAvg = value;
  }

  [DBProp("FallTimeMin")]
  public int FallTimeMin
  {
    get => this._FallTimeMin;
    set => this._FallTimeMin = value;
  }

  [DBProp("FallTimeMax")]
  public int FallTimeMax
  {
    get => this._FallTimeMax == int.MinValue ? 0 : this._FallTimeMax;
    set => this._FallTimeMax = value;
  }

  [DBProp("FallTimeAvg")]
  public int FallTimeAvg
  {
    get => this._FallTimeAvg == int.MinValue ? 0 : this._FallTimeAvg;
    set => this._FallTimeAvg = value;
  }

  [DBProp("CycleCount")]
  public int CycleCount
  {
    get => this._CycleCount;
    set => this._CycleCount = value;
  }

  public static RiceLatchSummary PopulateRiceLatchData(
    Account account,
    List<DataMessage> RiceMessage,
    Sensor RiceSensor,
    DateTime summaryDate)
  {
    RiceLatchSummary riceLatchSummary = RiceLatchSummary.LoadBySensorIDandDate(RiceSensor.SensorID, summaryDate.Date) ?? new RiceLatchSummary();
    riceLatchSummary.AccountID = account.AccountID;
    riceLatchSummary.SensorID = RiceSensor.SensorID;
    riceLatchSummary.SummaryDate = summaryDate.Date;
    riceLatchSummary.RiseTimeMin = int.MaxValue;
    riceLatchSummary.RiseTimeMax = 0;
    riceLatchSummary.RiseTimeAvg = 0;
    riceLatchSummary.FallTimeMin = int.MaxValue;
    riceLatchSummary.FallTimeMax = 0;
    riceLatchSummary.FallTimeAvg = 0;
    if (RiceMessage != null && RiceMessage.Count > 0)
    {
      try
      {
        List<int> source1 = new List<int>();
        List<int> source2 = new List<int>();
        int num = 0;
        foreach (DataMessage dataMessage in RiceMessage)
        {
          RiceTilt riceTilt = RiceTilt.Deserialize(RiceSensor.FirmwareVersion, dataMessage.Data);
          if (riceTilt.RiseFallTime != 0 && riceTilt.RiseFallTime < 10000)
          {
            if (riceTilt.Pitch > 45.0)
            {
              if (riceTilt.RiseFallTime > 0 && riceTilt.RiseFallTime < riceLatchSummary.RiseTimeMin)
                riceLatchSummary.RiseTimeMin = riceTilt.RiseFallTime;
              if (riceTilt.RiseFallTime > riceLatchSummary.RiseTimeMax)
                riceLatchSummary.RiseTimeMax = riceTilt.RiseFallTime;
              source1.Add(riceTilt.RiseFallTime);
            }
            else if (riceTilt.Pitch < 45.0)
            {
              if (riceTilt.RiseFallTime < riceLatchSummary.FallTimeMin)
                riceLatchSummary.FallTimeMin = riceTilt.RiseFallTime;
              if (riceTilt.RiseFallTime > riceLatchSummary.FallTimeMax)
                riceLatchSummary.FallTimeMax = riceTilt.RiseFallTime;
              source2.Add(riceTilt.RiseFallTime);
            }
          }
          num += riceTilt.Cycle.ToInt();
        }
        if (riceLatchSummary.RiseTimeMin == int.MaxValue)
          riceLatchSummary.RiseTimeMin = 0;
        if (riceLatchSummary.FallTimeMin == int.MaxValue)
          riceLatchSummary.FallTimeMin = 0;
        if (source1.Count > 0)
          riceLatchSummary.RiseTimeAvg = source1.Average().ToInt();
        if (source2.Count > 0)
          riceLatchSummary.FallTimeAvg = source2.Average().ToInt();
        riceLatchSummary.CycleCount = num;
      }
      catch (Exception ex)
      {
        ex.Log("RiceLatchSummary.PopulateRiceLatchData + ");
      }
    }
    riceLatchSummary.Save();
    return riceLatchSummary;
  }

  public static RiceLatchSummary Load(long id) => BaseDBObject.Load<RiceLatchSummary>(id);

  public static List<RiceLatchSummary> LoadByAccount(long accountID)
  {
    return BaseDBObject.LoadByForeignKey<RiceLatchSummary>("AccountID", (object) accountID);
  }

  public static List<RiceLatchSummary> LoadBySensorID(long sensorID)
  {
    return BaseDBObject.LoadByForeignKey<RiceLatchSummary>("SensorID", (object) sensorID);
  }

  public static RiceLatchSummary LoadBySensorIDandDate(long sensorID, DateTime summaryDate)
  {
    return new Monnit.Data.RiceLatchSummary.LoadBySensorIDandDate(sensorID, summaryDate).Result;
  }

  public static RiceLatchSummary LoadLastBySensorID(long sensorID)
  {
    return new Monnit.Data.RiceLatchSummary.LoadLastBySensorID(sensorID).Result;
  }

  public static List<RiceLatchSummary> LoadBySensorIDandToAndFromDates(
    long sensorID,
    DateTime ToDate,
    DateTime FromDate)
  {
    return new Monnit.Data.RiceLatchSummary.LoadBySensorIDandToAndFromDates(sensorID, ToDate, FromDate).Result;
  }

  public static int LoadCountbySensorID(long sensorID)
  {
    return Monnit.Data.RiceLatchSummary.LoadCountbySensorID.Exec(sensorID);
  }
}
