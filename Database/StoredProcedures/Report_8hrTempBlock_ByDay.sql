USE [iMonnit]
GO
/****** Object:  StoredProcedure [dbo].[Report_8hrTempBlock_ByDay]    Script Date: 6/4/2024 8:43:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Report_8hrTempBlock_ByDay]
  @Default_OwnerAccountID BIGINT,
  @Date DATETIME
AS

DECLARE @FromDate         DATETIME;
DECLARE @ToDate           DATETIME;
DECLARE @TimeZoneIDString VARCHAR(300);

CREATE TABLE #Calendar
(
  SensorID    BIGINT,
  SensorName  VARCHAR(300),
  Date        DATE
);

CREATE TABLE #SensorList
(
  SensorID BIGINT,
  ApplicationID INT,
  SensorName VARCHAR(300)
);

SET @FromDate = @Date;
SET @ToDate   = DATEADD(Day, 1, @Date);

SELECT 
  @TimeZoneIDString = tz.TimeZoneIDString,
  @FromDate         = DATEADD(MINUTE, DATEDIFF(MINUTE, dbo.GetLocalTime(@FromDate, TimeZoneIDSTring),@FromDate), @FromDate),
  @ToDate           = DATEADD(MINUTE, DATEDIFF(MINUTE, dbo.GetLocalTime(@ToDate, TimeZoneIDSTring), @ToDate), @ToDate)
FROM dbo.[TimeZone] tz      WITH(NOLOCK) 
INNER JOIN dbo.[Account] a  WITH(NOLOCK) ON a.[TimeZoneID] = tz.[TimeZoneID]
WHERE a.[AccountID] = @Default_OwnerAccountID;

INSERT INTO #SensorList (SensorID, ApplicationID, SensorName)
SELECT 
  SensorID,
  ApplicationID,
  SensorName
FROM dbo.[Sensor]
WHERE AccountID =@Default_OwnerAccountID
  AND ApplicationID IN (2, 43, 46);

INSERT INTO #Calendar
SELECT DISTINCT
  s.SensorID, 
  s.SensorName, 
  d.TheDate
FROM #SensorList s
INNER JOIN mea.dbo.[dimDate] d ON s.sensoriD = s.sensorid
WHERE d.thedate = @Date;

WITH CTE_Results AS
(
  SELECT
    d.sensorID, 
    SensorName,
    MessageDate = dbo.GetLocalTime(d.MessageDate, @TimeZoneIDString), 
    d.Data, 
    s.ApplicationID
  FROM dbo.[DataMessage] d WITH (NOLOCK)
  INNER JOIN #SensorList s on d.SensorID = s.SensorID
  WHERE d.MessageDate >= @FromDate
    AND d.MessageDate <= @ToDate
)
SELECT 
  SensorID, 
  SensorName, 
  Date          = CONVERT(Date, MessageDate), 
  Hour          = DATEPART(HOUR, MessageDate) / 8, 
  Data          = CASE WHEN ApplicationID = 43 THEN CONVERT(DECIMAL(18,2), SUBSTRING(DATA,  CHARINDEX(',', DATA,  0   )+1, CHARINDEX(',', DATA, CHARINDEX(',', DATA,  0   ) +1  )-CHARINDEX(',', DATA,  0   )-1))  ELSE CONVERT(DECIMAL(18,2), DATA) END,
  ApplicationID
INTO #tempResults
FROM CTE_Results
order by SensorID, CONVERT(Date, MessageDate);

--temps less than 990 are hardware failures and shouldn't be aggregated
DELETE #tempResults WHERE Data < -990;

WITH CTE_Results2 AS
(
  SELECT
    SensorID,
    SensorName,
    Date,
    Hour = CASE WHEN Hour = 0 THEN 'Morning' WHEN HOUR =1 THEN 'Midday'  ELSE 'Evening' END,
    Min = MIN(Data),
    Max = Max(Data),
    Avg = CONVERT(DECIMAL(18,2), Avg(Data))
  FROM #tempResults t
  GROUP BY SensorID,SensorName,Date,Hour
)
SELECT
  SensorID, 
  SensorName, 
  Date, 
  Special = Hour + ' '+ Vals, 
  Temp
INTO #Final
FROM CTE_Results2 c
UNPIVOT
(
  Temp for Vals in ([Min],[Max],[Avg])
) t;

SELECT CompanyName as Name from Account where AccountID = @Default_OwnerAccountID;

SELECT c.SensorName, c.Date,
 [Min1] = [Morning Min],
 [Max1] = [Morning Max],
 [Avg1] = [Morning Avg],
 [Min2] = [Midday Min],
 [Max2] = [Midday Max],
 [Avg2] = [Midday Avg],
 [Min3] = [Evening Min],
 [Max3] = [Evening Max],
 [Avg3] = [Evening Avg]
from #Calendar C LEFT JOIN (
SELECT SensorID, SensorName, Date,
  [Morning Min],
  [Morning Max],
  [Morning Avg],
  [Midday Min],
  [Midday Max],
  [Midday Avg],
  [Evening Min],
  [Evening Max],
  [Evening Avg]
FROM #Final 
PIVOT
(
  SUM(Temp) FOR Special in (
  [Midday Min],
  [Midday Max],
  [Midday Avg],
  [Evening Min],
  [Evening Max],
  [Evening Avg],
  [Morning Min],
  [Morning Max],
  [Morning Avg]
  )
) t2 ) f on c.SensorID = f.SensorID and c.Date = f.Date
ORDER BY c.Date, c.SensorName;

drop table #SensorList
drop table #tempResults
drop table #Final
drop table #Calendar

