USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_LungLifeMMA]    Script Date: 7/1/2024 8:28:08 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_LungLifeMMA]
  @Default_OwnerAccountID BIGINT,
  @Default_ReportScheduleID BIGINT
AS

--DECLARE @Default_OwnerAccountID BIGINT = 37731
--DECLARE @Default_ReportScheduleID BIGINT = 23394

DECLARE @TimeZoneIDString VARCHAR(300)
DECLARE @FromDate DATETIME
DECLARE @ToDate DATETIME

SET @TimeZoneIDString = (Select TimeZoneIDString FROM dbo.[Account] a INNER JOIN dbo.[TimeZone] t on a.TimeZoneID = t.TimeZoneID where a.AccountID = @Default_OwnerAccountID)

SET @ToDate         = CONVERT(DATE, dbo.GetLocalTime( GETUTCDATE(), @TimeZoneIDString))


SELECT @FromDate =
      CASE 
         WHEN ScheduleType = 3 THEN DATEADD(DAY,-1,@ToDate)
         WHEN ScheduleType = 2 THEN DATEADD(WEEK,-1,@ToDate)
         WHEN ScheduleType = 1 THEN DATEADD(MONTH,-1,@ToDate)
         WHEN ScheduleType = 0 THEN DATEADD(YEAR,-1,@ToDate)
         ELSE DATEADD(DAY,-1,@ToDate)
      END
   FROM dbo.[ReportSchedule] WITH (NOLOCK)
   WHERE ReportScheduleID = @Default_ReportScheduleID

     
SELECT
  SensorID, SensorName
INTO #SensorList
FROM dbo.[Sensor] s WITH (NOLOCK)
WHERE ApplicationID in (2,35,43,46,65,84)
  AND AccountID = @Default_OwnerAccountID;


SELECT
s.SensorID, s.SensorName, p.Date, p.Min, p.Max, p.Avg, p.SplitValue
FROM dbo.[PreAggregation] p 
INNER JOIN #SensorList s on p.SensorID = s.SensorID
WHERE p.Date >= @FromDate 
  AND p.date <= @ToDate
ORDER BY SensorID, SplitValue, Date;

GO


