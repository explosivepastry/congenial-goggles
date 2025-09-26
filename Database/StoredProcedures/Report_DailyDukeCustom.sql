USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_DailyDukeCustom]    Script Date: 6/24/2024 2:51:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_DailyDukeCustom]
	@Default_OwnerAccountID BIGINT = 21974
AS

/*
  7 Table Result set
  ------------------------------------------------------
    Table 1: Accounts
    Table 2: Dry Contact Aware State
    Table 3: Current Min Val = 0
    Table 4: Max Current - Min Current < 1.5
    Table 5: GatewayMessage, Gateway using battery
    Table 6: GatewayMessage, MessageCount >= 10
	Table 7: Inactive Sensors where time between messagedate is 2 hours + 2 heartbeats
	Table 8: Inactive Gateways
*/

--DEBUGGING
/*
declare @Default_OwnerAccountID BIGINT
set @Default_OwnerAccountID=21974
DROP TABLE IF EXISTS #AccountList
DROP TABLE IF EXISTS #Sensors
DROP TABLE IF EXISTS #InactiveSensors
DROP TABLE IF EXISTS #Gateways
DROP TABLE IF EXISTS #InactiveGateways
DROP TABLE IF EXISTS #TempGM
DROP TABLE IF EXISTS #Table1
DROP TABLE IF EXISTS #Table2
DROP TABLE IF EXISTS #Table3
DROP TABLE IF EXISTS #Table4
DROP TABLE IF EXISTS #Table5
DROP TABLE IF EXISTS #Table6
DROP TABLE IF EXISTS #Table7
DROP TABLE IF EXISTS #Table8
*/


DECLARE @FromDate         DATETIME
DECLARE @ToDate           DATETIME
DECLARE @TimeZoneIDString VARCHAR(300)
DECLARE @SQL              VARCHAR(4000)

SELECT
  @TimeZoneIDString = t.TimeZoneIDString
FROM dbo.[Account] a WITH (NOLOCK)
INNER JOIN dbo.[TimeZone] t  WITH (NOLOCK) on a.TimeZoneID = t.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID

SET @FromDate = dbo.GetUTCTime(CONVERT(DATE, DATEADD(DAY, -1, dbo.GetLocalTime(GETUTCDATE(), @TimeZoneIDString))), @TimeZoneIDString)
SET @ToDate   = DATEADD(HOUR, 24, @FromDate);

--SELECT @FromDate, @ToDate


--DROP TABLE IF EXISTS #AccountList

CREATE TABLE #AccountList
(
  AccountID     BIGINT,
  AccountNumber NVARCHAR(255),
  Tree  NVARCHAR(3000)
);


--DROP TABLE IF EXISTS #Sensors

CREATE TABLE #Sensors
(
  AccountID     BIGINT,
  AccountNumber NVARCHAR(255),
  SensorID      BIGINT,
  SensorName    NVARCHAR(255),
  ApplicationID INT,
  MessageDate   DATETIME,
  Data          VARCHAR(255),
  State         INT
);

--DROP TABLE IF EXISTS #TempGM

CREATE TABLE #TempGM
(
  AccountID           BIGINT,
  AccountNumber       VARCHAR(3000),
  GatewayID           BIGINT,
  ReceivedDate        DATETIME,
  GatewayMessageGUID  UNIQUEIDENTIFIER
)

--DROP TABLE IF EXISTS #Gateways

CREATE TABLE #Gateways
(
  AccountID     BIGINT,
  AccountNumber NVARCHAR(255),
  GatewayID     BIGINT,
  ReceivedDate  DATETIME,
  Power         INT,
  Battery       INT,
  MessageCount  INT,
  MessageType	  INT
);

--DROP TABLE IF EXISTS #InactiveSensors

CREATE TABLE #InactiveSensors
(
  AccountID BIGINT,
  SensorID BIGINT,
  MessageDate DATETIME,
  SensorType VARCHAR(20),
  LastMessageDate DATETIME,
  NextMessageDate DATETIME,
  MinutesBetweenMessages INT,
  ActivityStatus VARCHAR(20),
  ApplicationID INT
);


--DROP TABLE IF EXISTS #InactiveGateways

CREATE TABLE #InactiveGateways
(
  AccountID BIGINT,
  GatewayID BIGINT,
  ReceivedDate DATETIME,
  GatewayTypeID INT,
  LastCommunicationDate DATETIME,
  NextMessageDate DATETIME,
  MinutesBetweenMessages INT,
  ActivityStatus VARCHAR(20)
);



INSERT INTO #AccountList
SELECT
 a.AccountID,
 a.AccountNumber,
[Tree]                    = SUBSTRING((SELECT 
                              '^' + a.[CompanyName] AS 'data()' 
                          FROM dbo.[Split](RIGHT(a.[AccountIDTree], LEN(a.[AccountIDTree]) - (CHARINDEX(CONVERT(VARCHAR(300), @Default_OwnerAccountID), a.[AccountIDTree])) + 1),'*') s
                          INNER JOIN dbo.[Account] a WITH (NOLOCK) ON a.[AccountID] = s.[Item]
                          FOR XML PATH('')),2,1000)
FROM dbo.[Account] a WITH (NOLOCK)
WHERE AccountIDTree like '%*'+convert(varchar(30), @Default_OwnerAccountID)+'*%'

--INSERT INTO #Sensors
SET @SQL =
'SELECT
 a.AccountID,
 a.AccountNumber,
 s.SensorID,
 s.SensorName,
 s.ApplicationID,
 d.MessageDate,
 d.Data,
 d.state
FROM dbo.[Account] a WITH (NOLOCK)
INNER JOIN dbo.[Sensor] s WITH (NOLOCK) ON a.AccountID = s.AccountID
INNER JOIN dbo.[DataMessage] d WITH (NOLOCK) ON s.SensorID = d.SensorID
WHERE AccountIDTree like ''%*'+CONVERT(VARCHAR(30), 21974)+'*%''
  AND s.ApplicationID IN (2,3,93)
  AND MessageDate >= '''+CONVERT(VARCHAR(30), @FromDate, 120)+''' AND Messagedate <= '''+CONVERT(VARCHAR(30),@ToDate,120)+''';'
  
  INSERT INTO #Sensors
  EXEC (@SQL)

--select * from #Sensors


  
--INSERT INTO #Gateways
SET @SQL = 
'SELECT
 a.AccountID,
 a.AccountNumber,
 g.GatewayID,
 gm.ReceivedDate,
 gm.GatewayMessageGUID
FROM dbo.[Account] a WITH (NOLOCK)
INNER JOIN dbo.[CSNet] c WITH (NOLOCK)  on a.AccountID = c.AccountID
INNER JOIN dbo.[Gateway] g WITH (NOLOCK) on c.CSNetID = g.CSNetID
INNER JOIN dbo.[GatewayMessage] gm WITH (NOLOCK) on g.GatewayID = gm.GatewayID
WHERE AccountIDTree like ''%*'+convert(varchar(30), @Default_OwnerAccountID)+'*%''
  AND ReceivedDate >= '''+CONVERT(VARCHAR(30), @FromDate, 120)+''' and ReceivedDate <= '''+CONVERT(VARCHAR(30),@ToDate,120)+'''
OPTION(OPTIMIZE FOR UNKNOWN);'

INSERT INTO #TempGM
EXEC (@SQL)

INSERT INTO #Gateways
SELECT
 t.AccountID,
 t.AccountNumber,
 t.GatewayID,
 t.ReceivedDate,
 g.Power,
 g.Battery,
 g.MessageCount,
 g.MessageType
FROM #TempGM t
INNER JOIN dbo.[GatewayMessage] g WITH (NOLOCK) on t.GatewayID = g.GatewayID and t.ReceivedDate = g.ReceivedDate and t.GatewayMessageGUID = g.GatewayMessageGUID;


--Inactive Sensors
SET @SQL = 
'
  ;WITH CTE AS (
  SELECT
    a.accountID,
	s.SensorID,
    s.SensorName,
	st.name as SensorType,
    d.MessageDate,
    s.ReportInterval,
	LAG(d.MessageDate) OVER (PARTITION BY s.SensorID ORDER BY d.MessageDate) AS LastMessageDate,
    LEAD(d.MessageDate) OVER (PARTITION BY s.SensorID ORDER BY d.MessageDate) AS NextMessageDate,
	s.ApplicationID,
	s.LastCommunicationDate
  FROM dbo.[Account] a WITH (NOLOCK)
  INNER JOIN dbo.[Sensor] s WITH (NOLOCK) ON a.AccountID = s.AccountID
  INNER JOIN dbo.[DataMessage] d WITH (NOLOCK) ON s.SensorID = d.SensorID
  INNER JOIN dbo.SensorType st WITH (NOLOCK) on s.SensorTypeID = st.SensorTypeID
	WHERE AccountIDTree like ''%*'+convert(varchar(30), @Default_OwnerAccountID)+'*%''
    AND s.ApplicationID IN (2, 3, 93)
  AND MessageDate >= '''+CONVERT(VARCHAR(30), @FromDate, 120)+''' and MessageDate <= '''+CONVERT(VARCHAR(30),@ToDate,120)+'''
)

INSERT INTO #InactiveSensors
	SELECT
	  AccountID,
	  SensorID,
	  MessageDate,
	  SensorType,
	  LastMessageDate,
	  NextMessageDate,
	  DATEDIFF(MINUTE, MessageDate, NextMessageDate) AS MinutesBetweenMessages,
	  ''Inactive'' as ActivityStatus,
	  ApplicationID
	FROM CTE
	where(DATEDIFF(MINUTE, MessageDate, IsNull(NextMessageDate,'''+CONVERT(VARCHAR(30),@ToDate,120)+''')) >= (2 * 60) + (2 * ReportInterval))
	ORDER BY AccountID, SensorID, MessageDate;
'

EXEC (@SQL)

--select * from #InactiveSensors order by sensorID, messagedate
--select * from iMonnitMessages2024.dbo.datamessage_202403 with (nolock) 
--where sensorID=234704 and messagedate between '2024-03-10 05:28:35.000' and '2024-03-11 11:34:55.000' and sensorID=234704
--order by messagedate 

--return;


--Catch all sensors that have not communicated regardless of the given @FromDate and @ToDate but are still active.  Added 02/07/2024 by Craig
INSERT INTO #InactiveSensors
SELECT 
	MAX(a.accountID) as AccountID, 
	MAX(s.sensorID) as SensorID, 
	MAX(MessageDate) as MessageDate,
	MAX(st.name) as SensorType, 
	MAX(LastCommunicationDate) as LastCommunicationDate, 
	NULL as NextMessageDate,
	datediff(MINUTE,MAX(LastCommunicationDate),GETUTCDATE()) AS MinutesBetweenMessages,
	'Inactive' as ActivityStatus,
	MAX(s.ApplicationID) AS ApplicationID
FROM sensor s WITH (NOLOCK)
INNER JOIN account a WITH (NOLOCK) ON s.AccountID=a.AccountID
INNER JOIN dbo.[DataMessage] d WITH (NOLOCK) ON s.SensorID = d.SensorID
INNER JOIN dbo.SensorType st WITH (NOLOCK) on s.SensorTypeID = st.SensorTypeID
WHERE a.accountIDtree like '%21974%' AND s.ApplicationID IN (2, 3, 93) 
GROUP BY s.sensorID,ReportInterval
HAVING DATEDIFF(MINUTE,MAX(LastCommunicationDate),GETUTCDATE())>(2 * 60) + (2 * ReportInterval)

--Inactive Gateways
SET @SQL = 
'
  ;WITH CTE AS (
  SELECT 
    a.accountID,
	g.GatewayID,
    gm.receiveddate,
	g.gatewayTypeID,
    g.ReportInterval,
	LAG(gm.receiveddate) OVER (PARTITION BY gm.GatewayID ORDER BY gm.receiveddate) AS LastMessageDate,
    LEAD(gm.receiveddate) OVER (PARTITION BY gm.GatewayID ORDER BY gm.receiveddate) AS NextMessageDate,
	ISNULL(gs.LastCommunicationDate, g.LastCommunicationDate) AS LastCommunicationDate
	 FROM dbo.[Account] a WITH (NOLOCK)
	inner join csnet c WITH (NOLOCK) on c.accountID=a.accountID
  INNER JOIN dbo.[gateway] g WITH (NOLOCK) ON c.csnetid= g.csnetid
  LEFT JOIN dbo.GatewayStatus gs WITH (NOLOCK) on gs.GatewayID = g.GatewayID
  INNER JOIN dbo.[gatewayMessage] gm WITH (NOLOCK) ON g.gatewayID= gm.gatewayID
    WHERE AccountIDTree like ''%*'+convert(varchar(30), @Default_OwnerAccountID)+'*%''
  AND receiveddate >= '''+CONVERT(VARCHAR(30), @FromDate, 120)+''' and receiveddate <= '''+CONVERT(VARCHAR(30),@ToDate,120)+'''
)

INSERT INTO #InactiveGateways
	SELECT 
	  AccountID,
	  GatewayID,
	  receiveddate,
	  gatewayTypeID,
	  LastMessageDate,
	  NextMessageDate,
	  DATEDIFF(MINUTE, receiveddate, NextMessageDate) AS MinutesBetweenMessages,
	  ''Inactive'' AS ActivityStatus
	FROM CTE
	where (DATEDIFF(MINUTE, receiveddate, IsNull(NextMessageDate,'''+CONVERT(VARCHAR(30),@ToDate,120)+''')) >= (2 * 60) + (2 * ReportInterval))
'

EXEC (@SQL)

--Catch all gateways that have not communicated regardless of the given @FromDate and @ToDate but are still active.  Added 02/07/2024 by Craig
INSERT INTO #InactiveGateways
SELECT 
	MAX(a.accountID) as AccountID, 
	MAX(g.GatewayID) as GatewayID, 
	MAX(ReceivedDate) as ReceivedDate,
	MAX(g.GatewayTypeID) as GatewayTypeID, 
	MAX(ISNULL(gs.LastCommunicationDate, g.LastCommunicationDate)) as LastCommunicationDate, 
	NULL as LastReceivedDate,
	DATEDIFF(MINUTE,MAX(ISNULL(gs.LastCommunicationDate, g.LastCommunicationDate)),GETUTCDATE()) AS MinutesBetweenMessages,
	'Inactive' as ActivityStatus
FROM account a WITH (NOLOCK) 
INNER JOIN csnet c WITH (NOLOCK) on c.accountID=a.accountID
INNER JOIN dbo.[gateway] g WITH (NOLOCK) ON c.csnetid= g.csnetid
LEFT JOIN dbo.GatewayStatus gs WITH (NOLOCK) on gs.GatewayID = g.GatewayID
INNER JOIN dbo.[gatewayMessage] gm WITH (NOLOCK) ON g.gatewayID= gm.gatewayID
WHERE a.accountIDtree like '%21974%' 
GROUP BY ReportInterval
HAVING DATEDIFF(MINUTE,MAX(ISNULL(gs.LastCommunicationDate, g.LastCommunicationDate)),GETUTCDATE())>(2 * 60) + (2 * ReportInterval)


--Table 1: Accounts
--SELECT * FROM #AccountList;

DROP TABLE IF EXISTS #Table2

--Table 2: Dry Contact Aware State
SELECT 
  AccountID, 
  MessageDate = dbo.GetLocalTime(MessageDate, @TimeZoneIDstring), 
  Data, 
  State 
INTO #Table2
FROM #Sensors 
WHERE ApplicationID = 3 
  AND State & 2 = 2
ORDER BY AccountNumber, MessageDate;

DROP TABLE IF EXISTS #Table3

--Table 3: Current Min Val = 0
SELECT 
  AccountID, 
  MessageDate = dbo.GetLocalTime(MessageDate, @TimeZoneIDString), 
  Data, 
  State
INTO #Table3
FROM #Sensors
WHERE ApplicationID = 93
  AND CONVERT(DECIMAL(18,2), SUBSTRING(DATA,  CHARINDEX(',', DATA,  CHARINDEX(',', DATA, CHARINDEX(',', DATA, 0) +1 ) +1   ) +1,  20)) <= 0
ORDER BY AccountNumber, MessageDate;

DROP TABLE IF EXISTS #Table4

--Table 4: Max Current - Min Current < 1.5
;WITH CTE_Results AS(
SELECT 
  AccountID,
  MessageDate, 
  MaxCurrent = CONVERT(DECIMAL(18,2), SUBSTRING(DATA,  CHARINDEX(',', DATA,  CHARINDEX(',', DATA, 0) +1   ) +1,  CHARINDEX(',', DATA,  CHARINDEX(',', DATA, CHARINDEX(',', DATA, 0) +1  ) +1   )  - CHARINDEX(',', DATA,  CHARINDEX(',', DATA, 0) +1   )-1 )),
  MinCurrent = CONVERT(DECIMAL(18,2), SUBSTRING(DATA,  CHARINDEX(',', DATA,  CHARINDEX(',', DATA, CHARINDEX(',', DATA, 0) +1 ) +1   ) +1,  20))
FROM #Sensors WHERE ApplicationID = 93
)
SELECT
  AccountID,
  MessageDate = dbo.GetLocalTime(MessageDate, @TimeZoneIDString),
  MaxCurrent,
  MinCurrent,
  Comparison = 'False'
INTO #Table4
FROM CTE_Results
WHERE MaxCurrent - MinCurrent < 1.5;

DROP TABLE IF EXISTS #Table5

--Table 5: Gateway using battery
SELECT 
  AccountID, 
  GatewayID, 
  ReceivedDate = dbo.GetLocalTime(ReceivedDate, @TimeZoneIDString), 
  Power,
  Battery
INTO #Table5
FROM #Gateways 
WHERE Battery != 101;

DROP TABLE IF EXISTS #Table6

--Table 6: GatewayMessage, MessageCount >= 10
SELECT  
  AccountID, 
  GatewayID,
  ReceivedDate = dbo.GetLocalTime(ReceivedDate, @TimeZoneIDString),
  MessageCount
INTO #Table6
FROM #Gateways 
WHERE MessageCount >= 10
  AND MessageType = 0;

DROP TABLE IF EXISTS #Table7

--Table 7: Inactive Sensors, sensor is inactive for 2 hours + 2 hearbeats
SELECT
	  AccountID,
	  SensorID,
	  MessageDate,
	  SensorType,
	  NextMessageDate,
	  LastMessageDate,
	  MinutesBetweenMessages,
	  ActivityStatus,
	  ApplicationID
INTO #Table7
FROM #InactiveSensors


--Table 8: Inactive Gateways
SELECT
	AccountID, GatewayID, ReceivedDate, GatewayTypeID, NextMessageDate, MinutesBetweenMessages, ActivityStatus
INTO #Table8
FROM #InactiveGateways
  

  DELETE a 
  FROM #AccountList a WHERE AccountID NOT IN (
  SELECT AccountID FROM #Table2 UNION ALL
  SELECT AccountID FROM #Table3 UNION ALL
  SELECT AccountID FROM #Table4 UNION ALL
  SELECT AccountID FROM #Table5 UNION ALL
  SELECT AccountID FROM #Table6 UNION ALL
  SELECT AccountID from #Table7 UNION ALL
  SELECT AccountID from #Table8)


SELECT 
*
FROM #AccountList
ORDER BY Tree


SELECT
* 
FROM #Table2
ORDER BY AccountID, MessageDate;

SELECT
* 
FROM #Table3
ORDER BY AccountID, MessageDate;

SELECT
* 
FROM #Table4
ORDER BY AccountID, MessageDate;

SELECT
* 
FROM #Table5
ORDER BY AccountID, ReceivedDate;

SELECT
* 
FROM #Table6
ORDER BY AccountID, ReceivedDate;

SELECT
*
FROM #Table7 order by SensorID

SELECT
*
FROM #Table8

DROP TABLE IF EXISTS #AccountList
DROP TABLE IF EXISTS #Sensors
DROP TABLE IF EXISTS #Gateways
DROP TABLE IF EXISTS #Table1
DROP TABLE IF EXISTS #Table2
DROP TABLE IF EXISTS #Table3
DROP TABLE IF EXISTS #Table4
DROP TABLE IF EXISTS #Table5
DROP TABLE IF EXISTS #Table6
DROP TABLE IF EXISTS #Table7
DROP TABLE IF EXISTS #Table8

GO


