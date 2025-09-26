USE [iMonnit]
GO
/****** Object:  StoredProcedure [dbo].[Report_HumidityMinMax]    Script Date: 9/11/2024 11:05:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[Report_HumidityMinMax]
  @Default_OwnerAccountID BIGINT,
  @Default_ReportScheduleID BIGINT
AS

/* ----| Sql Script |---- */
--DECLARE @Default_OwnerAccountID BIGINT = 44965
--DECLARE @Default_ReportScheduleID BIGINT = 52650
DECLARE @FromDate DATETIME
DECLARE @ToDate DATETIME
DECLARE @OffsetMinutes INT
DECLARE @TimezoneIdentifier VARCHAR(255)
DECLARE @SQL VARCHAR(MAX);


/**************************************************

  SET From/ToDate based on account time zone
          and ScheduleType

**************************************************/
SELECT 
  @TimezoneIdentifier = tz.TimeZoneIDString 
FROM dbo.[TimeZone] tz WITH (NOLOCK)
INNER JOIN dbo.[Account] a WITH (NOLOCK) ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID;

SET @OffsetMinutes  = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));
SET @ToDate         = CONVERT(DATETIME,CONVERT(VARCHAR,DATEPART(YEAR,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))) +'-'+ CONVERT(VARCHAR,DATEPART(MONTH ,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate())))+'-'+ Convert(varchar,DatePart(Day,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))));


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

SET @SQL = 
'SELECT DISTINCT
  s.SensorID, s.SensorName, p.Date, a.ApplicationName,
  p.Min,
  p.Max,
  p.Avg
  FROM dbo.[Sensor] s WITH (NOLOCK)
  INNER JOIN dbo.[PreAggregation] p WITH (NOLOCK) ON s.SensorID = p.SensorID
  INNER JOIN dbo.[Application] a WITH (NOLOCK) ON s.ApplicationID = a.ApplicationID
WHERE s.AccountID = '+CONVERT(VARCHAR(30), @Default_OwnerAccountID) + '
  AND p.SplitValue = ''Humidity''
  AND p.Date >= '''+CONVERT(VARCHAR(30), @FromDate, 120) + ''' and p.Date <= '''+CONVERT(VARCHAR(30),@ToDate)+'''
ORDER BY s.SensorID, p.Date';

EXEC (@SQL)

GO