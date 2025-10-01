// Decompiled with JetBrains decompiler
// Type: Monnit.Data.LocationMessage
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class LocationMessage
{
  [DBMethod("LocationMessage_LoadByDeviceID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @FromDate DATETIME = CONVERT(Date, GetDate())\r\nDECLARE @ToDate DATETIME = DATEADD(Day, 1, @FromDate)\r\nDECLARE @SQL VARCHAR(1000)\r\n\r\n\r\nSET @SQL =\r\n'select top 5 * from LocationMessage\r\nWHERE LocationDate >= ''' + CONVERT(VARCHAR(30), @FromDate, 120) + ''' \r\n  AND LocationDate <= ''' + CONVERT(VARCHAR(30), @ToDate, 120) + ''' \r\n  AND DeviceID = ''' + CONVERT(VARCHAR(30), @DeviceID) + '''\r\nORDER BY LocationDate DESC;'\r\n\r\nEXEC (@Sql);")]
  internal class LoadByDeviceID : BaseDBMethod
  {
    [DBMethodParam("DeviceID", typeof (long))]
    public long DeviceID { get; private set; }

    public List<Monnit.LocationMessage> Result { get; private set; }

    public LoadByDeviceID(long deviceID)
    {
      this.DeviceID = deviceID;
      this.Result = BaseDBObject.Load<Monnit.LocationMessage>(this.ToDataTable());
    }
  }

  [DBMethod("LocationMessage_LoadByDeviceIDAndRange")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[LocationMessage] l WITH (NOLOCK)\r\nWHERE DeviceID = @DeviceID \r\n  AND LocationDate >= @FromDate\r\n  AND LocationDate <= @ToDate\r\nORDER BY LocationDate DESC;")]
  internal class LoadByDeviceIDAndRange : BaseDBMethod
  {
    [DBMethodParam("DeviceID", typeof (long))]
    public long DeviceID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    public List<Monnit.LocationMessage> Result { get; private set; }

    public LoadByDeviceIDAndRange(long deviceID, DateTime fromDate, DateTime toDate)
    {
      this.DeviceID = deviceID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Result = BaseDBObject.Load<Monnit.LocationMessage>(this.ToDataTable());
    }
  }

  [DBMethod("LocationMessage_LoadRecentByVisualMapID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  l.*\r\nFROM dbo.[VisualMap] v WITH (NOLOCK)\r\nINNER JOIN dbo.[VisualMapGateway] g  WITH (NOLOCK)on v.VisualMapID = g.VisualMapID\r\nINNER JOIN dbo.[Gateway] g2  WITH (NOLOCK) on g.GatewayID = g2.GatewayID\r\nINNER JOIN dbo.[LocationMessage] l  WITH (NOLOCK) on g2.LastLocationDate = l.LocationDate and g2.GatewayID = l.DeviceID\r\nWHERE v.VisualMapID = @VisualMapID\r\nORDER BY l.LocationDate;\r\n")]
  internal class LoadRecentByVisualMapID : BaseDBMethod
  {
    [DBMethodParam("VisualMapID", typeof (long))]
    public long VisualMapID { get; private set; }

    public List<Monnit.LocationMessage> Result { get; private set; }

    public LoadRecentByVisualMapID(long visualMapID)
    {
      this.VisualMapID = visualMapID;
      this.Result = BaseDBObject.Load<Monnit.LocationMessage>(this.ToDataTable());
    }
  }
}
