USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_MarineBiologic_OOT]    Script Date: 7/1/2024 8:32:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_MarineBiologic_OOT]
  @Default_OwnerAccountID BIGINT,
  @CSNetID BIGINT,
  @NotificationID BIGINT,
  @Default_ReportScheduleID BIGINT
AS

--Testing Variables
--DECLARE @Default_OwnerAccountID BIGINT = 10955;
--DECLARE @CSNetID                BIGINT = 22766;

--Declare Proc Variables
DECLARE @FromDate         DATETIME;
DECLARE @ToDate           DATETIME;
DECLARE @OffsetMinutes    INT;
DECLARE @TimezoneIdentifier VARCHAR(255);
DECLARE @SQL              VARCHAR(2000);

--Declare Temp table for pre-result set storing
DECLARE @TempDM TABLE(
  [MessageDate] DATETIME,
  [Data]        VARCHAR(255),
  [State]       INT,
  [SensorID]    BIGINT
  );

SELECT 
  @TimezoneIdentifier = tz.TimeZoneIDString 
FROM TimeZone tz
INNER JOIN Account a ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID

SET @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));
SET @ToDate = Convert(datetime,Convert(varchar,DatePart(Year,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))) +'-'+ Convert(varchar,DatePart(month ,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate())))+'-'+ Convert(varchar,DatePart(Day,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))));
SET @ToDate = DATEADD(MINUTE,-@OffsetMinutes, @ToDate);

SELECT @FromDate =
      CASE 
         WHEN ScheduleType = 3 THEN DATEADD(DAY,-1,@ToDate)
         WHEN ScheduleType = 2 THEN DATEADD(WEEK,-1,@ToDate)
         WHEN ScheduleType = 1 THEN DATEADD(MONTH,-1,@ToDate)
         WHEN ScheduleType = 0 THEN DATEADD(YEAR,-1,@ToDate)
         ELSE DATEADD(DAY,-1,@ToDate)
      END
   FROM ReportSchedule 
   WHERE ReportScheduleID = @Default_ReportScheduleID


SELECT DISTINCT
  nr.SensorID,
  s.SensorName,
  nr.NotificationDate,
  [TimeInAwareState] = REPLACE(SUBSTRING(nr.Reading, 0, CHARINDEX('<', nr.Reading, 0)), 'Time In Aware State', ''),
  [CurrentReading] = REPLACE(SUBSTRING(nr.Reading, CHARINDEX('>', nr.Reading, 0) + 1, LEN(nr.Reading)), 'Current Reading: ', '')
FROM dbo.[NotificationRecorded] nr WITH (NOLOCK)
INNER JOIN (
            SELECT
              nr.SensorID,
              nr.NotificationID,
              nr.NotificationTriggeredID,
              NotificationDate          = MAX(nr.NotificationDate)
            FROM dbo.[NotificationRecorded] nr WITH (NOLOCK)
            INNER JOIN dbo.[Sensor] s on nr.SensorID = s.SensorID
            WHERE NotificationID = @NotificationID 
              AND NotificationDate between @FromDate and @ToDate
              AND s.CSNetID = @CSNetID
            GROUP BY nr.SensorID, nr.NotificationID, nr.NotificationTriggeredID
            --order by sensorid, notificationdate 
            ) ij on nr.NotificationID = ij.NotificationID and nr.SensorID = ij.SensorID and nr.NotificationDate = ij.NotificationDate and nr.NotificationTriggeredID = ij.NotificationTriggeredID
inner join dbo.[Sensor] s on ij.SensorID = s.SensorID
ORDER BY nr.sensorid, nr.NotificationDate

--SET @SQL =
--'SELECT
--  [MessageDate] = DATEADD(MINUTE, '+CONVERT(VARCHAR(20), @OffsetMinutes)+', dm.[MessageDate]), --adjust stored message date for easier Result Set logic
--  dm.[Data],
--  dm.[State],
--  dm.[SensorID]
--FROM dbo.[DataMessage] dm WITH (NOLOCK)
--INNER JOIN dbo.[Sensor] s WITH (NOLOCK) on dm.[SensorID] = s.[SensorID]
--WHERE s.[CSNetID]             = '+CONVERT(VARCHAR(20), @CSNetID)+'
--  AND s.[AccountID]           = '+CONVERT(VARCHAR(20), @Default_OwnerAccountID)+'
--  AND s.[ApplicationID] IN (2,35,46,65,84)
--  AND dm.[MessageDate]        >= '''+CONVERT(VARCHAR(20), @FromDate)+'''
--  AND dm.[MessageDate]        <= '''+CONVERT(VARCHAR(20), @ToDate)+'''
--  AND ISNUMERIC(dm.[Data])    = 1';

--INSERT INTO @TempDM(
--  [MessageDate],
--  [Data],
--  [State],
--  [SensorID]
--  )
--EXEC (@SQL);

--WITH CTE_OOT AS
--(
--SELECT
--  [RowID]       = ROW_NUMBER() OVER (PARTITION BY [SensorID] ORDER BY [MessageDate]),
--  [MessageDate],
--  [Data],
--  [State],
--  [SensorID]
--FROM @TempDM
--)
--SELECT
--  d1.[SensorID],
--  [MessageMonth]                = DATEPART(MONTH, d1.[MessageDate]),
--  [MessageDay]                  = DATEPART(DAY, d1.[MessageDate]),
--  [MessageYear]                 = DATEPART(YEAR, d1.[MessageDate]),
--  [MessageTime]                 = SUBSTRING(CONVERT(VARCHAR(25), d1.[MessageDate], 120), 11, 12),
--  [TimeInAwareState HH:MM:SS]   = SUBSTRING(CONVERT(VARCHAR(25), DATEADD(SECOND, DATEDIFF(SECOND, d1.[MessageDate], d2.[MessageDate]), '1900-01-01 00:00:00'), 120), 12, 13),
--  [CurrentReading]              = d1.[Data]
--INTO #TempDM2
--FROM CTE_OOT d1
--INNER JOIN CTE_OOT d2 ON d1.[SensorID] = d2.[SensorID] and d1.[RowID] = d2.[RowID] - 1
--WHERE d1.[State] & 02 = 2;

--UPDATE t
--  SET t.[CurrentReading] = CONVERT(DECIMAL(8,2), ((CONVERT(DECIMAL(8,2), t.[CurrentReading]) * 9) / 5) + 32)
--FROM #TempDM2 t
--LEFT JOIN dbo.[SensorAttribute] s on t.[SensorID] = s.[SensorID]
--WHERE s.[SensorAttributeID] IS NULL
--   OR (s.[Name] = 'CorF' and s.[Value] = 'F')

--SELECT
--  *
--FROM #TempDM2
--ORDER BY SensorID, MessageYear, MessageMonth, MessageDay, MessageTime
GO


