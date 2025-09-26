USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_Gwinnett_MinMaxTemp_Backfill]    Script Date: 6/28/2024 4:02:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Report_Gwinnett_MinMaxTemp_Backfill]

AS

/*******************************************************
  Custom Report for Gwinnett Medical Center (3034)

  Grabs the min/max temperature readings on either
  day, week or monthly date range. 

********************************************************/

declare @Default_OwnerAccountID BIGINT = 3034
declare @Default_ReportScheduleID BIGINT = 14853
declare @CSNetID BIGINT = 20531

DECLARE @TimeZoneIDString VARCHAR(200),
        @FromDate         DATETIME,
        @ToDate           DATETIME


SELECT
  @TimeZoneIDString = t.TimeZoneIDString
FROM dbo.[Account] a WITH (NOLOCK)
INNER JOIN dbo.[Timezone] t WITH (NOLOCK) ON a.TimeZoneID = t.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID;

SET @ToDate = CONVERT(DATETIME, '2024-04-08') -- CONVERT(DATE, dbo.GetLocalTime(GETUTCDATE(), @TimeZoneIDString));

SELECT @FromDate = DATEADD(DAY,-1,@ToDate)
   --   CASE 
   --      WHEN ScheduleType = 3 THEN DATEADD(DAY,-1,@ToDate)
   --      WHEN ScheduleType = 2 THEN DATEADD(WEEK,-1,@ToDate)
   --      WHEN ScheduleType = 1 THEN DATEADD(MONTH,-1,@ToDate)
   --      WHEN ScheduleType = 0 THEN DATEADD(YEAR,-1,@ToDate)
   --      ELSE DATEADD(DAY,-1,@ToDate)
   --   END
   --FROM dbo.[ReportSchedule] WITH (NOLOCK)
   --WHERE ReportScheduleID = @Default_ReportScheduleID;

SELECT 
  p.Date, 
  s.SensorName, 
  [Min (C)] = p.Min, 
  [Max (C)] = p.Max,
  [Min (F)] = CONVERT(DECIMAL(18,2), (p.Min * (9.0/5.0)) +32),
  [Max (F)] = CONVERT(DECIMAL(18,2), (p.Max * (9.0/5.0)) +32)
FROM dbo.[Sensor] s WITH (NOLOCK)
INNER JOIN dbo.[PreAggregation] p WITH (NOLOCK) ON s.SensorID = p.SensorID
WHERE s.CSNetID    = @CSNetID
  AND p.SplitValue = 'Temperature'
  AND Date        >= @FromDate
ORDER BY
  s.SensorID, 
  Date;
GO


