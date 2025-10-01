USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_NashProduce_HumidityPreAgg]    Script Date: 7/1/2024 8:34:44 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_NashProduce_HumidityPreAgg]
  @Default_ReportScheduleID BIGINT
AS

--declare @default_reportscheduleid bigint = (select top 1 ReportScheduleID from reportschedule where scheduletype = 1)
DECLARE @FromDate DATETIME
DECLARE @ToDate DATETIME
DECLARE @OffsetMinutes INT
DECLARE @TimeZoneIDString VARCHAR(50);
DECLARE @SQL VARCHAR(MAX);

SELECT
  @TimeZoneIDString = t.TimeZoneIDString
FROM dbo.[Account] a WITH (NOLOCK)
INNER JOIN dbo.[TimeZone] t on a.TimeZoneID = t.TimeZoneID
WHERE a.AccountID = 21317;

SET @OffsetMinutes  = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimeZoneIDString));
SET @ToDate         = CONVERT(DATETIME,CONVERT(VARCHAR,DATEPART(YEAR,DATEADD(MINUTE, @OffSetMinutes, GETUTCDATE()))) +'-'+ CONVERT(VARCHAR,DATEPART(MONTH ,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate())))+'-'+ CONVERT(VARCHAR,DATEPART(DAY,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))));

SELECT @FromDate =
      CASE 
         WHEN ScheduleType = 3 THEN DATEADD(DAY,-1,@ToDate)
         WHEN ScheduleType = 2 THEN DATEADD(WEEK,-1,@ToDate)
         WHEN ScheduleType = 1 THEN DATEADD(MONTH,-1,@ToDate)
         WHEN ScheduleType = 0 THEN DATEADD(YEAR,-1,@ToDate)
         ELSE DATEADD(DAY,-1,@ToDate)
      END
   FROM dbo.[ReportSchedule] WITH (NOLOCK)
   WHERE ReportScheduleID = @Default_ReportScheduleID;

   --SET @FromDate = dbo.GetUTCTime(@fromDate, @TimeZoneIDString)
   --SET @ToDate   = dbo.GetUTCTime(@ToDate, @TimeZoneIDString);
   
SELECT
  [Room Number]         = s.SensorName, 
  [Date], 
  [Average Temperature] = CONVERT(VARCHAR(30), CONVERT(DECIMAL(18,2), (p.Avg * (9.0/5.0))+32.0)) + ' Degrees F',
  [Average Humidity]    = CONVERT(VARCHAR(30), CONVERT(DECIMAL(18,2), (SELECT p2.Avg from dbo.[PreAggregation] p2 WHERE p2.SensorID = s.SensorID AND p2.Date = p.Date AND p2.SplitValue != 'Temperature'))) + '%'
from dbo.[Sensor] s WITH (NOLOCK)
INNER JOIN dbo.[PreAggregation] p on s.SensorID = p.SensorID
WHERE s.AccountID = 21317
  AND Date >= @FromDate AND DATE <= @ToDate
  AND SplitValue = 'Temperature' 
  
GO


