USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_MarineBiologic_3AM]    Script Date: 7/1/2024 8:28:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_MarineBiologic_3AM]
  @Default_OwnerAccountID BIGINT,
  @CSNetID BIGINT,
  @Default_ReportScheduleID BIGINT
AS

----Testing Variables9402, 15372
--DECLARE @Default_OwnerAccountID BIGINT = 9402
--DECLARE @CSNetID                BIGINT = 15372;

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

SET @SQL =
'SELECT
  [MessageDate] = DATEADD(MINUTE, '+CONVERT(VARCHAR(20), @OffsetMinutes)+', dm.[MessageDate]), --adjust stored message date for easier Result Set logic
  dm.[Data],
  dm.[State],
  dm.[SensorID]
FROM dbo.[DataMessage] dm WITH (NOLOCK)
INNER JOIN dbo.[Sensor] s WITH (NOLOCK) on dm.[SensorID] = s.[SensorID]
WHERE s.[CSNetID]             = '+CONVERT(VARCHAR(20), @CSNetID)+'
  AND s.[AccountID]           = '+CONVERT(VARCHAR(20), @Default_OwnerAccountID)+'
  AND s.[ApplicationID] IN (2,35,46,65,84)
  AND dm.[MessageDate]        >= '''+CONVERT(VARCHAR(20), @FromDate)+'''
  AND dm.[MessageDate]        <= '''+CONVERT(VARCHAR(20), @ToDate)+'''
  AND ISNUMERIC(dm.[Data])    = 1';

INSERT INTO @TempDM(
  [MessageDate],
  [Data],
  [State],
  [SensorID]
  )
EXEC (@SQL);

UPDATE t
  SET t.[Data] = CONVERT(DECIMAL(8,2), ((CONVERT(DECIMAL(8,2), t.[Data]) * 9) / 5) + 32)
FROM @TempDM t
LEFT JOIN dbo.[SensorAttribute] s on t.[SensorID] = s.[SensorID]
WHERE s.[SensorAttributeID] IS NULL
   OR (s.[Name] = 'CorF' AND s.[Value] = 'F')

SELECT
  [SensorID],
  [MessageMonth]        = DATEPART(MONTH, d1.[MessageDate]),
  [MessageDay]          = DATEPART(DAY, d1.[MessageDate]),
  [MessageYear]         = DATEPART(YEAR, d1.[MessageDate]),
  [Message Time]        = (SELECT TOP 1 SUBSTRING(CONVERT(VARCHAR(25), d2.[MessageDate], 120), 11, 12) FROM @TempDM d2 WHERE DATEPART(DAY, d2.[MessageDate]) = DATEPART(DAY, d1.[MessageDate]) AND d2.SensorID = d1.SensorID AND DATEPART(HOUR, d2.MessageDate) > 2 ORDER BY d2.MessageDate),
  [3:00 AM Reading]     = (SELECT TOP 1 d2.[Data] FROM @TempDM d2 WHERE DATEPART(DAY, d2.[MessageDate]) = DATEPART(DAY, d1.[MessageDate]) AND d2.SensorID = d1.SensorID AND DATEPART(HOUR, d2.MessageDate) > 2 ORDER BY d2.MessageDate),
  [Temp Min 24 Hours]   = MIN(CONVERT(DECIMAL(8,2), d1.[Data])),
  [Temp Max 24 Hours]   = MAX(CONVERT(DECIMAL(8,2), d1.[Data]))
FROM @TempDM d1
GROUP BY
  [SensorID],
  DATEPART(MONTH, [MessageDate]),
  DATEPART(DAY, [MessageDate]),
  DATEPART(YEAR, [MessageDate])
ORDER BY
  [SensorID],
  DATEPART(YEAR, [MessageDate]),
  DATEPART(MONTH, [MessageDate]),
  DATEPART(DAY, [MessageDate]);


GO


