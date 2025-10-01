USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_TimeInMotion]    Script Date: 7/1/2024 9:35:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_TimeInMotion]
  @NetworkID                BIGINT ,
  @Default_OwnerAccountID   BIGINT,
  @Default_ReportScheduleID BIGINT
AS
DECLARE @FromDate           DATETIME,
        @OffsetMinutes      INT,
        @TimezoneIdentifier VARCHAR(255),
        @ToDate             DATETIME,
        @InMotionCount      INT,
        @SQL                VARCHAR(MAX);

CREATE TABLE #TempDM
(
  MessageDate DATETIME,
  Data VARCHAR(40),
  SensorID bigint
)

SELECT 
  @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),tz.[TimeZoneIDString]))
FROM dbo.TimeZone tz
INNER JOIN dbo.Account a ON a.[TimeZoneID] = tz.[TimeZoneID]
WHERE a.AccountID = @Default_OwnerAccountID;


SET @ToDate = DATEADD(MINUTE, -@OffsetMinutes, GETUTCDATE())
SET @FromDate = Convert(datetime,Convert(varchar,DatePart(Year,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))) +'-'+ Convert(varchar,DatePart(month ,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate())))+'-'+ Convert(varchar,DatePart(Day,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))));
SET @FromDate = DATEADD(MINUTE,-@OffsetMinutes, @FromDate);

SELECT @FromDate =
      CASE 
         WHEN [ScheduleType] = 3 THEN DATEADD(MONTH,-1,@FromDate)
         WHEN [ScheduleType] = 2 THEN DATEADD(WEEK,-1,@FromDate)
         WHEN [ScheduleType] = 1 THEN DATEADD(DAY,-1,@FromDate)
         WHEN [ScheduleType] = 0 THEN DATEADD(YEAR,-1,@FromDate)
         ELSE DATEADD(MONTH,-1,@FromDate)
      END
   FROM dbo.ReportSchedule 
   WHERE [ReportScheduleID] = @Default_ReportScheduleID;


SET @SQL = 
'
SELECT
  MessageDate,
  Data,
  s.sensorid
FROM DataMessage dm WITH (NOLOCK)
INNER JOIN Sensor s WITH (NOLOCK) ON s.sensorid = dm.sensorid
WHERE dm.MessageDate >= ''' + CONVERT(VARCHAR(30), @FromDate, 120) + '''
 AND dm.MessageDate <= ''' + CONVERT(VARCHAR(30), @ToDate, 120) + '''
 AND s.CSNetID = ' + CONVERT(VARCHAR(12), @NetworkID) +
''

INSERT INTO #TempDM (MessageDate, Data, sensorid)
EXEC (@SQL);

WITH CTE_Motion AS
(
SELECT 
  [RowID]     = ROW_NUMBER() OVER (PARTITION BY s.[SensorID] ORDER BY dm.[MessageDate] ASC),
  s.[SensorID],
  s.[SensorName],
  dm.[MessageDate],
  dm.[Data]
FROM dbo.Sensor s WITH (NOLOCK)
INNER JOIN #TempDM dm WITH (NOLOCK) ON dm.[SensorID] = s.[SensorID]
WHERE s.[CSNetID] = @networkid
  AND s.[ApplicationID] = 5
  AND dm.[MessageDate] BETWEEN @FromDate AND @ToDate 
)
SELECT
  c.SensorID,
  c.[SensorName],
  [md1]           = c.MessageDate,
  [md2]           = c2.MessageDate,
  [TimeInMotion]  = c2.messagedate - c.MessageDate,
  c.[data],
  [r1]            = c.RowID,
  [r2]            = c2.RowID,
  [Row2ID]        = ROW_NUMBER() OVER (PARTITION BY c.[SensorID] ORDER BY c.[RowID])
INTO #TempHolding
FROM CTE_Motion c
LEFT JOIN CTE_Motion c2 ON c2.[RowID]-1 = c.[RowID] AND c2.[SensorID] = c.[SensorID]
WHERE c.[Data] = 'True'
ORDER BY c.[SensorID], c.[RowID]

SELECT 
t.[SensorID],
t.[SensorName],
[Reading Date]            = DATEADD(MINUTE,  @OffsetMinutes, t.[md1]),
[TimeInMotion (HH:MM:SS)] = ISNULL(CONVERT(VARCHAR(50), t.[TimeInMotion], 108), ''),
[Action] = CASE WHEN t.[r2] = t2.[r1] THEN 'Continued' WHEN t.[TimeInMotion] IS NULL THEN 'Last Reading: In Motion' ELSE 'Stopped' END
FROM #TempHolding t
LEFT JOIN #TempHolding t2 ON t2.[Row2ID] -1 = t.[Row2ID] AND t2.[SensorID] = t.[SensorID]


SET @InMotionCount = (SELECT 
                      count(*)
                      FROM #TempHolding 
                      WHERE [TimeInMotion] IS NULL)

IF @InMotionCount >=1
BEGIN

SELECT 
  s.[SensorID],
  s.[SensorName],
  [Reading Date]  = DATEADD(MINUTE, @OffsetMinutes, dm.[MessageDate]),
  s.ActiveStateInterval,
  [ScheduledCheckIn] = DATEADD(MINUTE, s.[ActiveStateInterval], DATEADD(MINUTE, @OffsetMinutes, dm.[MessageDate]))
FROM dbo.Sensor s WITH (NOLOCK)
INNER JOIN dbo.DataMessage dm WITH (NOLOCK) ON dm.[MessageDate] = s.[lastcommunicationdate] AND dm.[SensorID] = s.[SensorID] AND dm.[DataMessageGUID] = s.[LastDataMessageGUID]
INNER JOIN #TempHolding t on t.[SensorID] = s.[SensorID]
WHERE t.[TimeInMotion] IS NULL

END ELSE
BEGIN

  SELECT
    0,
    'No Sensors currently in motion',
    NULL,
    0.000,
    null

  END
  


GO


