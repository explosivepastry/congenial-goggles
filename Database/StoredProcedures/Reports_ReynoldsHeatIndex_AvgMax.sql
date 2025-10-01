USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_ReynoldsHeatIndex_AvgMax]    Script Date: 7/1/2024 8:55:26 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Report_ReynoldsHeatIndex_AvgMax]
  @Default_OwnerAccountID INT ,
  @Hours INT,
  @Default_ReportScheduleID INT 
AS
/* |Reynolds Heat Index:  All Networks AVG/Max|

  This report returns the Avg and Max Heat indexes for each network,
  and the sensor/datetime that max was triggered
*/

/*
----| Maintenance Log |----
  DATE        NAME        NOTES
  2017-06-26  Nathan      Created Proc
*/

/* ----| Sql Script |---- */
DECLARE @FromDate DATETIME
DECLARE @ToDate DATETIME
DECLARE @OffsetMinutes INT
DECLARE @TimezoneIdentifier VARCHAR(255)
DECLARE @SQL VARCHAR(MAX);

DECLARE @HeatIndex TABLE
(
  CSNetID       BIGINT,
  NetworkName   VARCHAR(255),
  SensorID      BIGINT,
  SensorName    VARCHAR(255),
  MessageDate   DATETIME,
  HeatIndex     DECIMAL(6,3)
);


SELECT @TimezoneIdentifier = tz.TimeZoneIDString 
FROM TimeZone tz
INNER JOIN Account a ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID

SET @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));
SET @ToDate = Convert(datetime,Convert(varchar,DatePart(Year,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))) +'-'+ Convert(varchar,DatePart(month ,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate())))+'-'+ Convert(varchar,DatePart(Day,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))));
SET @ToDate = DATEADD(MINUTE,-@OffsetMinutes, DATEADD(HOUR,@Hours,@ToDate));

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

INSERT INTO @HeatIndex
(
  CSNetID,     
  NetworkName, 
  SensorID,  
  SensorName, 
  MessageDate, 
  HeatIndex     
)

SELECT
  c.CSNetID,
  c.name,
  s.sensorid,
  s.sensorname,
  dm.MessageDate,
  HeatIndex = dbo.GetHeatIndex(data)
from DataMessage dm with (NOLOCK)
INNER JOIN Sensor s WITH (NOLOCK) on dm.SensorID = s.SensorID
INNER JOIN CSNet c WITH (NOLOCK) on c.CSNetID =s.CSNetID
where c.AccountID = @Default_OwnerAccountID
 and s.ApplicationID= 43
 and dm.MessageDate BETWEEN @FromDate AND @ToDate


select
h1.CSNetID,
h1.NetworkName,
[Avg HeatIndex] = AVG(h1.HeatIndex),
[Max HeatIndex] = MAX(h1.HeatIndex),
[Max SensorName] = (Select SensorName from @HeatIndex h2 where h2.HeatIndex = MAX(h1.heatindex)),
[Max MessageDate] = (Select DATEADD(MINUTE, @OffsetMinutes, MessageDate) from @HeatIndex h2 where h2.HeatIndex = MAX(h1.heatindex))
FROM @HeatIndex h1
GROUP BY h1.CSNetID, h1.NetworkName
ORDER BY h1.CSNetID

GO


