USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_Romark12HourMonthly]    Script Date: 7/1/2024 8:56:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_Romark12HourMonthly]
  @Default_OwnerAccountID BIGINT
AS

DECLARE @FromDate datetime
DECLARE @ToDate datetime
DECLARE @TimeZoneIDString varchar(300)

SELECT
  @TimeZoneIDString = TimeZoneIDString
FROM dbo.TimeZone t WITH (NOLOCK)
INNER JOIN dbo.Account a WITH (NOLOCK) on t.TimeZoneID = a.TimeZoneID
where a.accountid = @Default_OwnerAccountID


select @ToDate = CASE WHEN datepart(hour, dbo.GetLocalTime(GETUTCDATE(), @TimeZoneIDString)) in (20,21,22,23) then convert(varchar(11), dbo.GetLocalTime(GETUTCDATE(), @TimeZoneIDString), 121) + '20:00' 
    else case when datepart(hour, dbo.GetLocalTime(GETUTCDATE(), @TimeZoneIDString)) in (0,1,2,3,4,5,6,7) then convert(varchar(11), dateadd(day, -1, dbo.GetLocalTime(GETUTCDATE(), @TimeZoneIDString)),121)+'20:00' 
    ELSE case when datepart(hour, dbo.GetLocalTime(GETUTCDATE(), @TimeZoneIDString)) in (8,9,10,11,12,13,14,15,16,17,18,19) THEN convert(varchar(11), dbo.GetLocalTime(GETUTCDATE(), @TimeZoneIDString), 121) + '08:00' 
    ELSE NULL END  END END;

SET @FromDate = Dateadd(Month, -1, @ToDate);


SELECT
  s.SensorID
INTO #SensorList
FROM Account a
INNER JOIN Sensor s on a.AccountID = s.AccountID
WHERE a.AccountID = @Default_OwnerAccountID
  and s.ApplicationID = 43

SELECT
s.SensorID,
MessageDateLocal = dbo.GetLocalTime(MessageDate, @TimeZoneIDString),
MessageDateChanged = CASE WHEN datepart(hour, dbo.GetLocalTime(MessageDate, @TimeZoneIDString)) in (20,21,22,23) then convert(varchar(11), dbo.GetLocalTime(MessageDate, @TimeZoneIDString), 121) + '20:00' 
    else case when datepart(hour, dbo.GetLocalTime(MessageDate, @TimeZoneIDString)) in (0,1,2,3,4,5,6,7) then convert(varchar(11), dateadd(day, -1, dbo.GetLocalTime(MessageDate, @TimeZoneIDString)),121)+'20:00' 
    ELSE case when datepart(hour, dbo.GetLocalTime(MessageDate, @TimeZoneIDString)) in (8,9,10,11,12,13,14,15,16,17,18,19) THEN convert(varchar(11), dbo.GetLocalTime(MessageDate, @TimeZoneIDString), 121) + '08:00' 
    ELSE NULL END  END END,
DataState  =       CONVERT(DECIMAL(18,2), SUBSTRING(d.Data, 0, CHARINDEX(',',d.Data, 0))) ,
DataState2 =       CONVERT(DECIMAL(18,2), SUBSTRING(d.Data,    CHARINDEX(',', d.Data,  0   )+1, CHARINDEX(',', d.Data, CHARINDEX(',',d.Data,  0   ) +1  )-CHARINDEX(',',d.Data,  0   )-1))  
INTO #Results
FROM #SensorList s
INNER JOIN DataMessage d WITH (NOLOCK) on s.SensorID = d.SensorID
WHERE d.MessageDate between @FromDate and @ToDate
ORDER BY d.SensorID, d.Messagedate;


select 
  SensorID, 
  FromDate    = CONVERT(DATETIME, MessageDateChanged),
  ToDate      = DATEADD(HOUR, 12, MessageDateChanged),
  AvgHumidity = AVG(DataState),
  AvgTemp     = ((Avg(DataState2) * 9.0)/5.0)+32.0
from #Results
GROUP BY SensorID, 
CONVERT(DATETIME, MessageDateCHanged),DATEADD(HOUR, 12, MessageDateChanged)
ORDER BY 
  SensorID,
  CONVERT(DATETIME, MessageDateCHanged)

GO


