// Decompiled with JetBrains decompiler
// Type: Monnit.Data.SensorMessageAudit
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class SensorMessageAudit
{
  public class SensorMessageAuditModel
  {
    public string EventType { get; set; }

    public int Success { get; set; }

    public int Total { get; set; }
  }

  [DBMethod("SensorMessageAudit_Search")]
  [DBMethodBody(DBMS.SqlServer, "\t\t\r\nSELECT \r\n  * \r\nFROM dbo.[SensorMessageAudit] WITH (NOLOCK) \r\nWHERE SensorID      = @SensorID\r\n  AND MessageDate   BETWEEN @FromDate AND @ToDate \r\n  AND MessageEvent  = @MessageEvent;\r\n")]
  internal class Search : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("MessageEvent", typeof (string))]
    public string MessageEvent { get; private set; }

    public List<Monnit.SensorMessageAudit> Result { get; private set; }

    public Search(long sensorID, DateTime fromDate, DateTime toDate, string messageEvent)
    {
      this.SensorID = sensorID;
      this.ToDate = toDate;
      this.FromDate = fromDate;
      this.MessageEvent = messageEvent;
      this.Result = BaseDBObject.Load<Monnit.SensorMessageAudit>(this.ToDataTable());
    }
  }

  [DBMethod("SensorMessageAudit_GetCounts")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT   \r\n  MessageEvent, \r\n  IsSuccess     = SUM(CASE WHEN IsSuccess = 1 THEN 1 ELSE 0 END), \r\n  Total         = COUNT(SensorMessageAuditID)\r\nFROM dbo.[SensorMessageAudit] WITH (NOLOCK)\r\nWHERE SensorID    = @SensorID\r\n  AND MessageDate BETWEEN  @FromDate AND @ToDate\r\nGROUP BY \r\n  MessageEvent;\r\n")]
  internal class GetCounts : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    public List<Monnit.SensorMessageAudit.SensorMessageAuditCountModel> Result { get; private set; }

    public GetCounts(long sensorID, DateTime fromDate, DateTime toDate)
    {
      this.SensorID = sensorID;
      this.ToDate = toDate;
      this.FromDate = fromDate;
      DataTable dataTable = this.ToDataTable();
      this.Result = new List<Monnit.SensorMessageAudit.SensorMessageAuditCountModel>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        this.Result.Add(new Monnit.SensorMessageAudit.SensorMessageAuditCountModel()
        {
          EventType = row["MessageEvent"].ToString(),
          Success = row["IsSuccess"].ToInt(),
          Total = row["Total"].ToInt()
        });
    }
  }
}
