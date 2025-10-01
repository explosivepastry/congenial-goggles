// Decompiled with JetBrains decompiler
// Type: Monnit.Data.DataUseLog
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class DataUseLog
{
  [DBMethod("CellDataLog_CalculateDailyUse")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @MaxDate DATETIME\r\nDECLARE @SecondMaxDate DATETIME\r\n\r\nSELECT @MaxDate = MAX(CONVERT(DATE, DATEADD(DAY, -1, GETDATE()))) \r\nSELECT @SecondMaxDate = MAX(CONVERT(DATE, date)) from dbo.[DataUseLog] WHERE CONVERT(DATE, Date) < @MaxDate;\r\n\r\nSELECT @MaxDate, @SecondMaxDate;\r\n\r\nWITH cte_results AS \r\n(\r\n  SELECT\r\n    d.*,\r\n    LastValue = (SELECT TOP 1 MBUsed FROM dbo.[DataUseLog] d2 where CONVERT(Date, d2.Date) = @SecondMaxDate and d.DataUseListID = d2.DataUseListID ORDER BY Date DESC)\r\n  FROM dbo.[DataUseLog] d\r\n  INNER JOIN dbo.[DataUseList] dl on d.DataUseListID = dl.DataUseListID\r\n  WHERE dl.Carrier = 'Rogers'\r\n    AND CONVERT(Date, Date) = @MaxDate\r\n)\r\nUPDATE c\r\n  SET c.MB_UsedDaily = CASE WHEN Datepart(Day,@MaxDate) = 25 THEN c2.MBUsed ELSE  c2.MBUsed - lastvalue END\r\nFROM dbo.[DataUseLog] c\r\nINNER JOIN cte_results c2 on c.DataUseLogID = c2.DataUseLogID;\r\n\r\nWITH cte_results AS \r\n(\r\n  SELECT\r\n    d.*,\r\n    LastValue = (SELECT top 1 MBUsed FROM DataUSeLog d2 where CONVERT(Date, d2.Date) = @SecondMaxDate and d.DataUseListID = d2.DataUseListID ORDER BY Date DESC)\r\n  FROM dbo.[DataUseLog] d\r\n  INNER JOIN dbo.[DataUseList] dl on d.DataUseListID = dl.DataUseListID\r\n  WHERE dl.Carrier = 'ATT'\r\n    AND CONVERT(Date, Date) = @MaxDate\r\n)\r\nUPDATE c\r\n  SET c.MB_UsedDaily = CASE WHEN Datepart(Day,@MaxDate) = 18 THEN c2.MBUsed ELSE  c2.MBUsed - lastvalue END\r\nFROM DataUseLog c\r\nINNER JOIN cte_results c2 on c.DataUseLogID = c2.DataUseLogID;\r\n\r\nWITH cte_results AS \r\n(\r\n  SELECT\r\n    d.*,\r\n    LastValue = (SELECT top 1 MBUsed FROM DataUSeLog d2 where CONVERT(Date, d2.Date) = @SecondMaxDate and d.DataUseListID = d2.DataUseListID ORDER BY Date DESC)\r\n  FROM dbo.[DataUseLog] d\r\n  INNER JOIN dbo.[DataUseList] dl on d.DataUseListID = dl.DataUseListID\r\n  WHERE dl.Carrier = 'Twilio'\r\n    AND CONVERT(Date, Date) = @MaxDate\r\n)\r\nUPDATE c\r\n  SET c.MB_UsedDaily = CASE WHEN Datepart(Day,@MaxDate) = 1 THEN c2.MBUsed ELSE  c2.MBUsed - lastvalue END -- Billing periods vary for every Twilio device which is why 1 is the @MaxDate comparison\r\nFROM DataUseLog c\r\nINNER JOIN cte_results c2 on c.DataUseLogID = c2.DataUseLogID;\r\n\r\nUPDATE d \r\n  SET MB_UsedDaily = MBUsed\r\nFROM [DataUseLog]  d\r\nINNER JOIN DataUseList dl on dl.DataUseListID = d.DataUseListID\r\nWHERE CONVERT(Date, Date) = @MaxDate\r\n  AND Carrier = 'Verizon'\r\n  AND d.MB_UsedDaily IS NULL;\r\n")]
  internal class CalculateDailyUse : BaseDBMethod
  {
    public long Result { get; private set; }

    public CalculateDailyUse() => this.Result = (long) this.ToScalarValue<int>();
  }

  [DBMethod("DataUseLog_LoadDailyByGateway")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT DISTINCT \r\n  TheDate \r\nINTO #Calendar \r\nFROM MEA.dbo.[dimDate]\r\nWHERE theDate >= @FromDate \r\n  AND theDate <= @ToDate;\r\n\r\nSELECT \r\n  Date = c.TheDate,\r\n  MB = ISNULL(MBUsed_Daily, 0)\r\nFROM #Calendar c \r\nLEFT JOIN ( SELECT\r\n              dl.Date,\r\n              MBUsed_Daily = ISNULL(dl.Mb_UsedDaily, 0)\r\n            FROM dbo.[DataUseList] l WITH (NOLOCK)\r\n            INNER JOIN dbo.[DataUseLog] dl  WITH (NOLOCK) ON l.DataUseListID = dl.DataUseListID\r\n            WHERE l.GatewayID = @GatewayID\r\n              AND dl.Date >= @FromDate \r\n              AND dl.Date <= @ToDate) t on c.TheDate = t.Date\r\nORDER BY c.TheDate;\r\n")]
  internal class LoadDailyByGateway : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; set; }

    public DataTable Result { get; private set; }

    public LoadDailyByGateway(long gatewayID, DateTime fromDate, DateTime toDate)
    {
      this.GatewayID = gatewayID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("DataUseLog_LoadMonthlyByGateway")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT DISTINCT FirstOfMonth,TheDate INTO #Calendar FROM MEA.dbo.[dimDate]\r\nWHERE theDate >= @FromDate and theDate <= @ToDate;\r\n\r\nSELECT \r\n  Date= c.FirstOfMonth,\r\n  MB = SUM(ISNULL(MBUsed_Daily, 0))\r\nFROM #Calendar c \r\nLEFT JOIN ( SELECT\r\n              dl.Date,\r\n              MBUsed_Daily = ISNULL(dl.Mb_UsedDaily, 0)\r\n            FROM dbo.[DataUseList] l WITH (NOLOCK)\r\n            INNER JOIN dbo.[DataUseLog] dl  WITH (NOLOCK) ON l.DataUseListID = dl.DataUseListID\r\n            WHERE l.GatewayID = @GatewayID\r\n              AND dl.Date >= @FromDate \r\n              AND dl.Date <= @ToDate) t on c.TheDate = t.Date\r\nGROUP BY c.FirstOfMonth\r\nORDER BY c.FirstOfMonth\r\n\r\nDROP TABLE #Calendar\r\n")]
  internal class LoadMonthlyByGateway : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; set; }

    public DataTable Result { get; private set; }

    public LoadMonthlyByGateway(long gatewayID, DateTime fromDate, DateTime toDate)
    {
      this.GatewayID = gatewayID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Result = this.ToDataTable();
    }
  }
}
