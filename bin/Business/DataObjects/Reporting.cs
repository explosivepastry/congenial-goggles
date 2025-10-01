// Decompiled with JetBrains decompiler
// Type: Monnit.Reporting
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit;

public class Reporting
{
  [DBMethod("Reporting_GatewaySensorLastCheckIn")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  s.*\r\nFROM dbo.[Gateway] g WITH (NOLOCK)\r\nINNER JOIN dbo.[CSNet] n WITH (NOLOCK) ON n.CSNetID = g.CSNetID\r\nINNER JOIN dbo.[sensor] s WITH (NOLOCK) ON s.AccountID = n.AccountID\r\nINNER JOIN dbo.[DataMessage_Last] m WITH (NOLOCK) ON m.SensorID = s.SensorID\r\nWHERE g.GatewayID = @GatewayID\r\n  AND m.GatewayID = @GatewayID\r\nORDER BY s.SensorID DESC;\r\n")]
  public class GatewaySensorLastCheckIn : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    public List<Sensor> Result { get; private set; }

    public GatewaySensorLastCheckIn(long gatewayID)
    {
      this.GatewayID = gatewayID;
      this.Result = BaseDBObject.Load<Sensor>(this.ToDataTable());
    }

    public static List<Sensor> Exec(long gatewayID)
    {
      return new Reporting.GatewaySensorLastCheckIn(gatewayID).Result;
    }
  }

  [DBMethod("Reporting_BatteryHealthReport")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  s.SensorID,\r\n  s.SensorName,\r\n  dm.Battery,\r\n  dm.Voltage\r\nFROM dbo.[sensor] s\r\nINNER JOIN dbo.[DataMessage_Last] dm ON s.SensorID = dm.SensorID \r\nWHERE s.AccountID = @AccountID\r\nORDER BY \r\n  s.AccountID,\r\n  dm.Battery;\r\n")]
  public class BatteryHealthReport : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public DataTable Result { get; private set; }

    public BatteryHealthReport(long accountID)
    {
      this.AccountID = accountID;
      this.Result = this.ToDataTable();
    }

    public static DataTable Exec(long accountID)
    {
      return new Reporting.BatteryHealthReport(accountID).Result;
    }
  }

  [DBMethod("Reporting_IndividualAccountReport")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  s.[SensorID],\r\n  [Sensor Name]       = s.[SensorName],\r\n  [Sensor Type]       = m.[ApplicationName],\r\n  [Network Name]      = c.[Name]\r\nFROM dbo.[Sensor] s\r\nINNER JOIN dbo.[CSNet] c              ON s.CSNetID = c.CSNetID\r\nINNER JOIN dbo.[Application] m  ON s.ApplicationID = m.ApplicationID\r\nINNER JOIN dbo.[Account] a            ON s.AccountID = a.AccountID\r\nWHERE a.AccountID = @AccountID \r\nORDER BY\r\n  s.AccountID,\r\n  s.SensorNAme,\r\n  c.CSNetID;\r\n")]
  public class IndividualAccountReport : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public DataTable Result { get; private set; }

    public IndividualAccountReport(long accountID)
    {
      this.AccountID = accountID;
      this.Result = this.ToDataTable();
    }

    public static DataTable Exec(long accountID)
    {
      return new Reporting.IndividualAccountReport(accountID).Result;
    }
  }

  [DBMethod("Reporting_ResellerAccountReport")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  [Company Name]      = a.[CompanyName],\r\n  s.[SensorID],\r\n  [Sensor Name]       = s.[SensorName],\r\n  [Sensor Type]       = m.[ApplicationName],\r\n  [Network Name]      = c.Name \r\nFROM dbo.[Sensor] s\r\nINNER JOIN dbo.[CSNet] c              ON s.[CSNetID] = c.[CSNetID]\r\nINNER JOIN dbo.[Application] m  ON s.[ApplicationID] = m.[ApplicationID]\r\nINNER JOIN dbo.[Account] a            ON s.[AccountID] = a.[AccountID]\r\nWHERE a.[AccountID]       = @AccountID \r\n   OR a.[RetailAccountID] = @AccountID\r\nORDER BY\r\n  s.[AccountID],\r\n  s.[SensorName],\r\n  c.[CSNetID];\r\n")]
  public class ResellerAccountReport : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public DataTable Result { get; private set; }

    public ResellerAccountReport(long accountID)
    {
      this.AccountID = accountID;
      this.Result = this.ToDataTable();
    }

    public static DataTable Exec(long accountID)
    {
      return new Reporting.ResellerAccountReport(accountID).Result;
    }
  }
}
