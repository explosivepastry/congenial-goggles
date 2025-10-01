USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_PPFD_ByNetworkandDate]    Script Date: 7/1/2024 8:49:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_PPFD_ByNetworkandDate]
  @Default_OwnerAccountID BIGINT ,
  @NetworkID BIGINT, 
  @FromDate DATETIME
AS

/*****************************************************
    Report expects a network and date to be passed

    Grab all readings for day plus 2x heartbeat for 
    previous day. The idea is for the FromDate 
    selected, there is a integral (sumation) that
    essentially uses a Reimanns sum of Y (first index
    value of the reading) times X (number of seconds 
    between points) between a range of Midnight local
    to account plus 24 hours (including DST). 

    Y value for first midnight reading is either the closest
    data point previous to midnight (within 2 HB)
    or the first data point in the day after midnight.

    After taking sumation for the day, divide by 1000000.
    0 value for sensor if no reading for day.

*****************************************************/

/********************************
        Report Parameters
*********************************/
--DECLARE @Default_OwnerAccountID BIGINT = 1
--DECLARE @NetworkID BIGINT = 12010  --12056
--DECLARE @FromDate DATETIME = '2021-03-15 00:00:00'


/********************************
  Query Parameters/Temp Tables
*********************************/
DECLARE @TimeZoneIDString VARCHAR(50)
DECLARE @ToDate DATETIME
DECLARE @SQL VARCHAR(2000)


CREATE TABLE #TempREsults
(
  RowID         BIGINT,
  SensorID      BIGINT,
  MessageDate   DATETIME,
  Data          VARCHAR(255),
  PPFD          DECIMAL(18,2)
);


/*********************************
         Report Script
**********************************/

SELECT
  @TimeZoneIDString = s.TimeZoneIDString
from dbo.[TimeZone] s
INNER JOIN dbo.[Account] a ON s.TimeZOneID = a.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID;

SET @FromDate   = dbo.GetUTCTime( CONVERT(DATETIME, CONVERT(Date, @FromDate)), @TimeZoneIDString);
SET @ToDate     = DATEADD(Hour, 24, @FromDate);

SELECT
  s.SensorID, 
  ReportInterval,
  StartDate
INTO #SensorList
from dbo.[Sensor] s
INNER JOIN dbo.[Account] a ON s.AccountID = a.AccountID
WHERE s.AccountID     = @Default_OwnerAccountID
  AND s.ApplicationID = 139
  AND s.CSNetID       = @NetworkID;


SET @SQL = '
SELECT
  RowID = Row_Number() OVER (PARTITION BY d.SensorID ORDER BY MessageDate),
  d.SensorID,
  d.MessageDate,
  Data,
  PPFD = SUBSTRING(Data, 0, CHARINDEX(''|'', data, 0))
FROM dbo.[DataMessage] d WITH (NOLOCK)
INNER JOIN #SensorList s ON d.SensorID = s.SensorID
WHERE d.MessageDate >= DATEADD(MINUTE, -2.0 * REportInterval, ''' + CONVERT(VARCHAR(30), @FromDate, 120) + ''')
  AND MessageDate < ''' + CONVERT(VARCHAR(30), @ToDate, 120) + '''
  AND MessageDate >= StartDate
ORDER BY SensorID, MessageDate
';

PRINT (@SQL);

INSERT INTO #TempREsults
EXEC (@SQL);

SELECT
  RowID = Row_Number() OVER (PARTITION BY SensorID ORDER BY MessageDate), 
  *
INTO #Final FROM (
                  SELECT
                    t.SensorID,
                    t.MessageDate,
                    t.Data,
                    t.PPFD
                  FROM #TempResults t
                  INNER JOIN (
                              SELECT 
                                SensorID, 
                                MessageDate = MAX(MessageDate) FROM #TempREsults 
                              WHERE MessageDate <= @FromDate
                              GROUP BY SensorID
                  ) t2 on t.SensorID = t2.SensorId AND t2.MessageDate = t.MessageDate
                  UNION ALL
                  SELECT 
                    SensorID, 
                    MEssageDate, 
                    Data, 
                    PPFD 
                  FROM #tempResults 
                  WHERE MEssageDate > @FromDate
                  )t
WHERE SensorID in (SELECT distinct SensorID FROM #tempResults WHERE MessageDate >= @FromDate)
ORDER BY sensorID, MessageDate;

UPDATE #Final Set MessageDate = @FromDate where RowID = 1;

WITH CTE_Results as 
(
  SELECT
    f.SensorID, 
    FromMessageDate = f.MessageDate, 
    ToMessageDate   = ISNULL(f2.MessageDate, @ToDate), 
    Seconds         = DATEDIFF(SECOND, f.MessageDate, ISNULL(f2.MessageDate, @ToDate)),  
    f.PPFD, 
    [Sum]           = DATEDIFF(SECOND, f.MessageDate, ISNULL(f2.MessageDate, @ToDate)) * f.PPFD
  FROM #Final f
  LEFt JOIN #Final f2 on f.SensorID = f2.SensorID AND f.rowID = f2.RowID - 1
)
SELECT 
  Date = dbo.GetLocalTime(@FromDate, @TimeZoneIDString),
  s.SensorID, 
  DLI = convert(decimal(18,7), ISNULL(Sum([Sum])/1000000, 0))
FROM #SensorList s
LEFT JOIN Cte_Results c on s.SensorID = c.SensorID
GROUP BY s.SensorID;

GO


