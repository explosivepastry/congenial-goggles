USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_UMD_MinMaxTemp]    Script Date: 7/1/2024 9:36:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_UMD_MinMaxTemp]
  @Default_OwnerAccountID BIGINT,
  @Default_ReportScheduleID BIGINT
AS

/*******************************************************
  Custom Report for University of MD (39739)

  Grabs the min/max temperature readings on either
  day, week or monthly date range. 

********************************************************/

DECLARE @TimeZoneIDString VARCHAR(200),
        @FromDate         DATETIME,
        @ToDate           DATETIME


SELECT
  @TimeZoneIDString = t.TimeZoneIDString
FROM dbo.[Account] a WITH (NOLOCK)
INNER JOIN dbo.[Timezone] t WITH (NOLOCK) ON a.TimeZoneID = t.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID;

SET @ToDate = CONVERT(DATE, dbo.GetLocalTime(GETUTCDATE(), @TimeZoneIDString));

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

SELECT 
  s.SensorID,
  s.SensorName, 
  p.Date, 
  DataType = p.SplitValue,
  [Min (C)] = p.Min, 
  [Max (C)] = p.Max,
  [Avg (C)] = p.Avg,
  d.Battery
FROM dbo.[Sensor] s WITH (NOLOCK)
INNER JOIN dbo.[PreAggregation] p WITH (NOLOCK) ON s.SensorID = p.SensorID
LEFT JOIN dbo.[DataMessage] d WITH (NOLOCK) ON s.LastDataMessageGUID = d.DataMessageGUID and s.SensorID = d.SensorID and s.LastCommunicationDate = d.MessageDate
WHERE s.AccountID = @Default_OwnerAccountID
  AND p.SplitValue IN ('Humidity', 'Temperature')
  AND Date        >= @FromDate
ORDER BY
  p.SplitValue Desc,
  s.SensorID, 
  Date;
GO


