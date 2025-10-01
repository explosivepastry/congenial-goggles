// Decompiled with JetBrains decompiler
// Type: Monnit.Data.RiceLatchSummary
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class RiceLatchSummary
{
  internal class LoadBySensorIDandDate : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("SummaryDate", typeof (DateTime))]
    public DateTime SummaryDate { get; private set; }

    public Monnit.RiceLatchSummary Result { get; private set; }

    public LoadBySensorIDandDate(long sensorID, DateTime summaryDate)
    {
      this.SensorID = sensorID;
      this.SummaryDate = summaryDate.Date;
      this.Result = BaseDBObject.Load<Monnit.RiceLatchSummary>(this.ToDataTable()).FirstOrDefault<Monnit.RiceLatchSummary>();
    }
  }

  internal class LoadLastBySensorID : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    public Monnit.RiceLatchSummary Result { get; private set; }

    public LoadLastBySensorID(long sensorID)
    {
      this.SensorID = sensorID;
      this.Result = BaseDBObject.Load<Monnit.RiceLatchSummary>(this.ToDataTable()).FirstOrDefault<Monnit.RiceLatchSummary>();
    }
  }

  internal class LoadCountbySensorID : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    public int Result { get; private set; }

    public LoadCountbySensorID(long sensorID)
    {
      this.SensorID = sensorID;
      this.Result = this.ToScalarValue<int>();
    }

    public static int Exec(long sensorID)
    {
      return new RiceLatchSummary.LoadCountbySensorID(sensorID).Result;
    }
  }

  internal class LoadBySensorIDandToAndFromDates : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    public List<Monnit.RiceLatchSummary> Result { get; private set; }

    public LoadBySensorIDandToAndFromDates(long sensorID, DateTime toDate, DateTime fromDate)
    {
      this.SensorID = sensorID;
      this.ToDate = toDate;
      this.FromDate = fromDate;
      this.Result = BaseDBObject.Load<Monnit.RiceLatchSummary>(this.ToDataTable());
    }
  }
}
