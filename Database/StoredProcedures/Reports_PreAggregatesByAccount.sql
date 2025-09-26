USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_PreAggregatesByAccount]    Script Date: 7/1/2024 8:50:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_PreAggregatesByAccount]
  @Default_OwnerAccountID BIGINT,
  @Default_ReportScheduleID INT
AS

DECLARE @FromDate DATETIME,
        @ToDate DATETIME,     
        @TimeZoneIdentifier VARCHAR(300),
        @Days INT

SELECT @Days =
  CASE 
      WHEN ScheduleType = 3 THEN 1
      WHEN ScheduleType = 2 THEN 7
      WHEN ScheduleType = 1 THEN 31
      ELSE 1
  END
FROM dbo.[ReportSchedule] WITH (NOLOCK)
WHERE ReportScheduleID = @Default_ReportScheduleID

SELECT
  @TimeZoneIdentifier = t.TimeZoneIDString
FROM Account a WITH (NOLOCK)
INNER JOIN TimeZone t with (NOLOCK) on a.timezoneid = t.timezoneid
WHERE a.AccountID = @Default_OwnerAccountID;

SET @FromDate = CONVERT(DATE, DATEADD(DAY, -1*@Days, monnitdb.dbo.getlocaltime(GETUTCDATE(), @TimeZoneIdentifier) ))
SET @ToDate   = CONVERT(DATE,  monnitdb.dbo.getlocaltime(GETUTCDATE(), @TimeZoneIdentifier))

select @FromDate, @todate

SELECT
  p.SensorID,
  s.SensorName,
  p.Date,
  p.CSNetID,
  p.AccountID,
  p.ApplicationName,
  DataType = p.SplitValue,
  p.Min,
  p.Max,
  p.Avg,
  p.FalseCount,
  p.TrueCount,
  p.Avg_Voltage,
  p.SensorMessageCounts,
  p.AwareStateCounts
FROM PreAggregation p WITH (NOLOCK)
INNER JOIN dbo.Sensor s WITH (NOLOCK) on p.sensorID = s.SensorID
WHERE p.AccountID = @Default_OwnerAccountID
  AND p.Date >= @FromDate
  AND p.Date <= @ToDate
ORDER BY p.CSNetID, p.SensorID, p.Date, p.SplitValue
GO


