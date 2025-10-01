// Decompiled with JetBrains decompiler
// Type: Monnit.Data.CableAudit
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class CableAudit
{
  [DBMethod("CableAudit_LoadBySensorAndDateRange")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  ca.CableAuditID, \r\n  ca.CableID, \r\n  ca.SensorID, \r\n  ca.LogDate, \r\n  ca.Type, \r\n  c.SKU, \r\n  c.ApplicationID,\r\n  a.ApplicationName\r\nFROM dbo.[CableAudit] ca WITH (NOLOCK)\r\nINNER JOIN cable c  WITH (NOLOCK) on ca.CableID = c.cableID\r\nINNER JOIN Application a  WITH (NOLOCK) on c.ApplicationID=a.ApplicationID\r\nWHERE ca.SensorID = @SensorID\r\n  AND ca.LogDate >= @FromDate\r\n  AND ca.LogDate <= @ToDate\r\nORDER BY ca.LogDate desc, ca.Type asc;")]
  internal class LoadBySensorAndDateRange : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    public DataTable Result { get; private set; }

    public LoadBySensorAndDateRange(long sensorID, DateTime fromDate, DateTime toDate)
    {
      this.SensorID = sensorID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("CableAudit_LoadRecentByCableID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP 1\r\n  *\r\nFROM dbo.[CableAudit] WITH (NOLOCK)\r\nWHERE CableID = @CableID\r\nORDER BY LogDate DESC;")]
  internal class LoadRecentByCableID : BaseDBMethod
  {
    [DBMethodParam("CableID", typeof (long))]
    public long CableID { get; private set; }

    public List<Monnit.CableAudit> Result { get; private set; }

    public LoadRecentByCableID(long cableID)
    {
      this.CableID = cableID;
      this.Result = BaseDBObject.Load<Monnit.CableAudit>(this.ToDataTable());
    }
  }
}
