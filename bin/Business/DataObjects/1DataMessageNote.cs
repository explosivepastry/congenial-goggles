// Decompiled with JetBrains decompiler
// Type: Monnit.Data.DataMessageNote
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class DataMessageNote
{
  [DBMethod("DataMessageNote_LoadBySensorAndDateRange")]
  [DBMethodBody(DBMS.SqlServer, "SELECT\r\n*\r\nFROM dbo.[DataMessageNote] WITH (NOLOCK)\r\nWHERE SensorID = @SensorID\r\n  AND MessageDate >= @FromDate\r\n  AND MessageDate <= @ToDate;")]
  internal class LoadBySensorAndDateRange : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    public List<Monnit.DataMessageNote> Result { get; private set; }

    public LoadBySensorAndDateRange(long sensorID, DateTime fromDate, DateTime toDate)
    {
      this.SensorID = sensorID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Result = BaseDBObject.Load<Monnit.DataMessageNote>(this.ToDataTable());
    }
  }
}
