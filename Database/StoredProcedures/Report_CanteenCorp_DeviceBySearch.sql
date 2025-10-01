USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_CanteenCorp_DeviceBySearch]    Script Date: 6/24/2024 2:43:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_CanteenCorp_DeviceBySearch]
  @Default_OwnerAccountID BIGINT,
  @Search VARCHAR(300),
  @SensorProfile INT
AS

DECLARE @SensorName VARCHAR(300)

DECLARE @Results TABLE
(
  AccountID BIGINT,
  [AccountNumber] VARCHAR(300),
  [Division] VARCHAR(300),
  [District] VARCHAR(300),
  [SensorCount] INT,
  [Sensors_Online_LowBattery] INT,
  [Sensors_Online_LowSignal] INT,
  [SensorsAlerting_24Hrs] INT,
  [GatewayCount] INT,
  [GatewaysOffline] INT,
  Sensors_Gen1_Offline INT,
  Sensors_Alta_Offline INT,
  [Type] INT
)

CREATE TABLE #Sensors
(
  AccountID BIGINT,
  [AccountNumber] VARCHAR(300), 
  [SensorName] VARCHAR(300),
  [SensorID] BIGINT, 
  [ReportInterval] DECIMAL(16,4), 
  [LastCommunicationDate] DATETIME, 
  [LastDataMessageGUID] VARCHAR(300), 
  [GenerationType] VARCHAR(300), 
  [SensorTypeID] INT,
  District VARCHAR(300),
    Division VARCHAR(300),
    HoldingOnlyNetwork BIT
)

IF @Search = 'AllSites'
  SET @Search = ''

IF @SensorProfile = 1
  SET @SensorName = NULL

IF @SensorProfile = 2
  SET @SensorName = 'Freezer'

IF @SensorProfile = 3
  SET @SensorName = 'Cooler'

IF @SensorProfile IN (1,2)
BEGIN

INSERT INTO #Sensors
SELECT
  a.AccountID,
  a.[AccountNumber], 
  s.[SensorName],
  s.[SensorID], 
  s.[ReportInterval], 
  s.[LastCommunicationDate], 
  s.[LastDataMessageGUID], 
  GenerationType = ISNULL(s.[GenerationType], 'Gen1'), 
  s.[SensorTypeID],
  District          = (SELECT a4.AccountNumber from Account a4 WHERE a4.AccountID = dbo.SPLITINDEX(REPLACE(a.AccountIDTree, '*1*16836*16737*461*28211*', ''), '*', 1)  ),
  Division          = (SELECT a4.AccountNumber from Account a4 WHERE a4.AccountID = dbo.SPLITINDEX(REPLACE(a.AccountIDTree, '*1*16836*16737*461*28211*', ''), '*', 0)  ),
  c.HoldingOnlyNetwork
FROM dbo.[Account] a       WITH (NOLOCK)
LEFT JOIN dbo.[Sensor] s  WITH (NOLOCK)  ON a.AccountID = s.AccountID --AND s.sensorname    like '%'+ISNULL(@SensorName, '')+'%'
LEFT JOIN dbo.[Account] a3 WITH (NOLOCK)  ON a.RetailAccountID = a3.AccountID                  
LEFT JOIN dbo.[CSNet] c on s.CSNetID = c.CSNetID
WHERE a.AccountidTree like '%*' + CONVERT(VARCHAR(30), @Default_OwnerAccountID) + '*%'
  AND a.accountnumber like '%'+@Search+'%'
  AND a.AccountNumber NOT LIKE '%INACTIVE'
  


END ELSE
IF @SensorProfile IN (3)
BEGIN


INSERT INTO #Sensors
SELECT
  a.AccountID,
  a.[AccountNumber], 
  s.[SensorName],
  s.[SensorID], 
  s.[ReportInterval], 
  s.[LastCommunicationDate], 
  s.[LastDataMessageGUID], 
  s.[GenerationType], 
  s.[SensorTypeID],
  District          = (SELECT a4.AccountNumber from Account a4 WHERE a4.AccountID = dbo.SPLITINDEX(REPLACE(a.AccountIDTree, '*1*16836*16737*461*28211*', ''), '*', 1)  ),
    Division          = (SELECT a4.AccountNumber from Account a4 WHERE a4.AccountID = dbo.SPLITINDEX(REPLACE(a.AccountIDTree, '*1*16836*16737*461*28211*', ''), '*', 0)  ),
    c.HoldingOnlyNetwork
FROM dbo.[Account] a       WITH (NOLOCK)
LEFT JOIN dbo.[Sensor] s  WITH (NOLOCK)  ON a.AccountID = s.AccountID AND (s.sensorname    like '%'+ISNULL(@SensorName, '')+'%'
    OR s.sensorname like '%fresh%')
LEFT JOIN dbo.[Account] a3 WITH (NOLOCK)  ON a.RetailAccountID = a3.AccountID                       
LEFT JOIN dbo.[CSNet] c on s.CSNetID = c.CSNetID             
WHERE a.AccountidTree like '%*' + CONVERT(VARCHAR(30), @Default_OwnerAccountID) + '*%'
  AND a.accountnumber like '%'+@Search+'%'
  AND a.AccountNumber NOT LIKE '%INACTIVE'
  

END ELSE
BEGIN

INSERT INTO #Sensors
SELECT
  a.AccountID,
  a.[AccountNumber], 
  s.[SensorName],
  s.[SensorID], 
  s.[ReportInterval], 
  s.[LastCommunicationDate], 
  s.[LastDataMessageGUID], 
  s.[GenerationType], 
  s.[SensorTypeID],
  District          = (SELECT a4.AccountNumber from Account a4 WHERE a4.AccountID = dbo.SPLITINDEX(REPLACE(a.AccountIDTree, '*1*16836*16737*461*28211*', ''), '*', 1)  ),
    Division        = (SELECT a4.AccountNumber from Account a4 WHERE a4.AccountID = dbo.SPLITINDEX(REPLACE(a.AccountIDTree, '*1*16836*16737*461*28211*', ''), '*', 0)  ),
    c.HoldingOnlyNetwork
FROM dbo.[Account] a       WITH (NOLOCK)
LEFT JOIN dbo.[Sensor] s   WITH (NOLOCK)  ON a.AccountID = s.AccountID   AND s.sensorname not like '%Freezer%'
    AND s.sensorname not like '%Fresh%'
  AND s.sensorname not like '%Cooler%'
LEFT JOIN dbo.[Account] a3 WITH (NOLOCK)  ON a.RetailAccountID = a3.AccountID                          
LEFT JOIN dbo.[CSNet] c on s.CSNetID = c.CSNetID          
WHERE a.AccountidTree  like '%*' + CONVERT(VARCHAR(30), @Default_OwnerAccountID) + '*%'
  AND a.accountnumber  like '%'+@Search+'%'
  AND a.AccountNumber NOT LIKE '%INACTIVE'

END

select
  a.AccountID,
  a.[AccountNumber], 
  g.[GatewayID], 
  g.[ReportInterval], 
  gs.[LastCommunicationDate], 
  District          = (SELECT a4.AccountNumber from Account a4 WHERE a4.AccountID = dbo.SPLITINDEX(REPLACE(a.AccountIDTree, '*1*16836*16737*461*28211*', ''), '*', 1)  ),
    Division          = (SELECT a4.AccountNumber from Account a4 WHERE a4.AccountID = dbo.SPLITINDEX(REPLACE(a.AccountIDTree, '*1*16836*16737*461*28211*', ''), '*', 0)  )
INTO #Gateways
from dbo.[Account] a       WITH (NOLOCK)
INNER JOIN dbo.[CSNet] c   WITH (NOLOCK) ON a.AccountID = c.AccountID
INNER JOIN dbo.[Gateway] g WITH (NOLOCK) ON c.CSNetID = g.CSNetID
INNER JOIN dbo.[GatewayStatus] gs WITH (NOLOCK) ON g.GatewayID = gs.GatewayID
LEFT JOIN dbo.[Account] a3 WITH (NOLOCK) ON a.RetailAccountID = a3.AccountID          
WHERE a.AccountidTree like '%*' + CONVERT(VARCHAR(30), @Default_OwnerAccountID) + '*%'
  and a.accountnumber like '%'+@Search+'%'
  AND a.AccountNumber NOT LIKE '%INACTIVE'


SELECT 
  s.*, 
  d.Battery, 
  d.SignalStrength, 
  LastSignalStrength = NULL,
  HasAlert = (SELECT COUNT(*) from NotificationTriggered nt INNER JOIN SensorNotification sn on nt.SensorNotificationID = sn.SensorNotificationID where sn.SensorID = s.sensorid and nt.StartTime > dateadd(hour, -24, getutcdate()))
INTO #DMSensor
FROM #Sensors s 
INNER JOIN dbo.[DataMessage] d WITH (NOLOCK) ON s.SensorID = d.SensorID AND s.LastDataMessageGUID = d.DataMessageGUID AND s.LastCommunicationDate = d.MessageDate
WHERE s.LastDataMessageGUID IS NOT NULL

UPDATE #DMSensor
SET LastSignalStrength = (SignalStrength + 90) + 60
WHERE GenerationType = 'GEN2' AND SignalStrength > -90

UPDATE #DMSensor
SET LastSignalStrength = ROUND((SignalStrength + 115) * 2.4, 0)
WHERE  GenerationType = 'GEN2' AND SignalStrength <= -90

UPDATE #DMSensor
SET [LastSignalStrength] = ((SignalStrength+70)*2)+60
WHERE  SensorTypeID = 4 AND ISNULL(GenerationType, 'GEN1') != 'Gen2' AND SignalStrength > -70

UPDATE #DMSensor
SET [LastSignalStrength] = ROUND((SignalStrength+84) * 4.2857, 0)
WHERE SensorTypeID = 4 AND ISNULL(GenerationType, 'GEN1') != 'Gen2' AND SignalStrength <= -70

UPDATE #DMSensor
SET [LastSignalStrength] = ((SignalStrength+80) * 4.0 / 3.0) + 60
WHERE SensorTypeID != 4 AND ISNULL(GenerationType, 'GEN1') != 'Gen2' AND SignalStrength > -80

UPDATE #DMSensor
SET [LastSignalStrength] = (SignalStrength+100) * 3
WHERE  SensorTypeID != 4 AND ISNULL(GenerationType, 'GEN1') != 'Gen2' AND SignalStrength <= -80

UPDATE #DMSensor
SET [LastSignalStrength] = 100
WHERE [LastSignalStrength] > 100

--select * from #DMSensor order by accountnumber
--select * from #Gateways
--SELECT accountnumber, COUNT(*) from #Gateways s2 where s2.LastCommunicationDate is null or s2.LastCommunicationDate < dateadd(minute, -2*reportinterval, getutcdate()) group by s2.AccountNumber

INSERT INTO @Results
SELECT
	s.accountID,
  s.AccountNumber,
  Division,
	District,
	SensorCount                 = (SELECT ISNULL(COUNT(*), 0) from #Sensors  s2 where s2.AccountID = s.AccountID AND s2.SensorID IS NOT NULL group by s2.AccountID),
	[Sensors_Online_LowBattery] = (SELECT ISNULL(COUNT(*), 0) from #DMSensor s2 where s2.AccountID = s.AccountID AND s2.SensorID IS NOT NULL AND  s2.Battery >= 0 and s2.Battery < 25 and s2.LastCommunicationDate  between dateadd(minute, -2.0*s2.reportinterval, getutcdate()) and getutcdate() group by s2.AccountID ),
	[Sensor_Online_LowSignal]   = (SELECT ISNULL(COUNT(*), 0) from #DMSensor s2 where s2.AccountID = s.AccountID AND s2.SensorID IS NOT NULL and  s2.LastSignalStrength < 25 and s2.LastCommunicationDate  between dateadd(minute, -2.0*s2.reportinterval, getutcdate()) and getutcdate() ),
  SensorAlerts_24Hrs          = (SELECT SUM(HasAlert) FROM #DMSensor s2 WHERE s2.AccountID = s.AccountID),
	GatewayCount                = g2.gatewaycount,
	GatewaysOffline             = g3.gatewaycount,
  Sensors_Gen1_Offline = (SELECT ISNULL(COUNT(*), 0) from #Sensors  s2 where s2.AccountID = s.AccountID AND s2.SensorID IS NOT NULL AND s2.LastCommunicationDate not between dateadd(minute, -2.0*s2.reportinterval, getutcdate()) and dateadd(hour, 2, getutcdate()) and s2.HoldingOnlyNetwork = 0 and generationtype = 'Gen1' group by s2.AccountID),
  Sensors_Alta_Offline = (SELECT ISNULL(COUNT(*), 0) from #Sensors  s2 where s2.AccountID = s.AccountID AND s2.SensorID IS NOT NULL AND s2.LastCommunicationDate not between dateadd(minute, -2.0*s2.reportinterval, getutcdate()) and dateadd(hour, 2, getutcdate()) and s2.HoldingOnlyNetwork = 0 and generationtype = 'Gen2' group by s2.AccountID),
  Type = 1
FROM #Sensors s
left join (
            select
              AccountID, 
              gatewaycount = count(*) 
            from #Gateways g 
            group by g.AccountID,  g.District, g.Division
          ) g2 on s.AccountID = g2.AccountID
left join (
          select 
            AccountID, 
            gatewaycount = count(*) 
            from #Gateways g 
            where LastCommunicationDate is null 
               or LastCommunicationDate < dateadd(minute, -2*reportinterval, getutcdate()) 
               or LastCommunicationDate > getutcdate() 
            group by g.AccountID
          ) g3 on s.AccountID = g3.AccountID
GROUP BY s.AccountID,s.AccountNUmber, g2.gatewaycount, g3.gatewaycount, District, s.Division

INSERT INTO @Results
SELECT
  AccountID,
  AccountNumber,
  Division,
  District,
  0,
  0,
  0,
  0,
  COUNT(*),
  (SELECT COUNT(*) FROM #Gateways g2 where g2.AccountID= g.AccountID and (LastCommunicationDate is null 
                 or LastCommunicationDate < dateadd(minute, -2*reportinterval, getutcdate()) 
                 or LastCommunicationDate > getutcdate() )),
  0,
  0,
  1
FROM #Gateways g
WHERE AccountID NOT IN (SELECT AccountID from #Sensors)
GROUP BY AccountID, AccountNumber, District, Division

INSERT INTO @Results
SELECT
  AccountID = NULL,
	AccountNumber = 'Total',
  Division = '',
	district = '',
	SensorCount                 = (SELECT ISNULL(COUNT(*), 0) from #Sensors  s2 WHERE SensorID IS NOT NULL),
	[Sensors_Online_LowBattery] = (SELECT ISNULL(COUNT(*), 0) from #DMSensor s2 where  s2.Battery >= 0 and s2.Battery < 25 AND s2.SensorID IS NOT NULL and s2.LastCommunicationDate  between dateadd(minute, -2.0*s2.reportinterval, getutcdate()) and getutcdate()  ),
	[Sensor_Online_LowSignal]   = (SELECT ISNULL(COUNT(*), 0) from #DMSensor s2 where  s2.LastSignalStrength < 25 AND s2.SensorID IS NOT NULL and s2.LastCommunicationDate  between dateadd(minute, -2.0*s2.reportinterval, getutcdate()) and getutcdate() ),
  SensorAlerts_24Hrs          = (SELECT SUM(HasAlert) FROM #DMSensor s2),
	GatewayCount                = (select GatewayCount = count(*) from #Gateways g),
	GatewaysOffline             = (select GatewayCount = count(*) from #Gateways g where LastCommunicationDate is null or LastCommunicationDate < dateadd(minute, -2.0*reportinterval, getutcdate()) or LastCommunicationDate > getutcdate()),
  Sensors_Gen1_Offline = (SELECT ISNULL(COUNT(*), 0) from #Sensors  s2 where  s2.LastCommunicationDate not between dateadd(minute, -2.0*s2.reportinterval, getutcdate()) and dateadd(hour, 2, getutcdate()) and s2.HoldingOnlyNetwork = 0  AND s2.SensorID IS NOT NULL and s2.GenerationType = 'Gen1'),
  Sensors_Alta_Offline = (SELECT ISNULL(COUNT(*), 0) from #Sensors  s2 where  s2.LastCommunicationDate not between dateadd(minute, -2.0*s2.reportinterval, getutcdate()) and dateadd(hour, 2, getutcdate()) and s2.HoldingOnlyNetwork = 0  AND s2.SensorID IS NOT NULL and s2.GenerationType = 'Gen2'),
  Type = 2

IF @SensorProfile = 1
BEGIN

    SELECT 
      AccountID,
      AccountNumber,
      Division,
      District,
      SensorCount                 = ISNULL(SensorCount, 0),
      Sensors_Gen1_Offline        = ISNULL(Sensors_Gen1_Offline, 0),
      Sensors_Alta_Offline        = ISNULL(Sensors_Alta_Offline, 0),
      Sensors_Online_LowBattery   = ISNULL(Sensors_Online_LowBattery, 0),
      Sensors_Online_LowSignal    = ISNULL(Sensors_Online_LowSignal, 0),
      SensorAlerts_24Hrs          = ISNULL(SensorsAlerting_24Hrs, 0),
      GatewayCount                = ISNULL(GatewayCount, 0),
      GatewaysOffline             = ISNULL(GatewaysOffline, 0)
    FROM @Results 
    ORDER BY 
      Type, Division, District, AccountNumber;

END ELSE
BEGIN

    SELECT 
      AccountID,
      AccountNumber,
      Division,
      District,
      SensorCount                 = ISNULL(SensorCount, 0),
      Sensors_Gen1_Offline        = ISNULL(Sensors_Gen1_Offline, 0),
      Sensors_Alta_Offline        = ISNULL(Sensors_Alta_Offline, 0),
      Sensors_Online_LowBattery   = ISNULL(Sensors_Online_LowBattery, 0),
      Sensors_Online_LowSignal    = ISNULL(Sensors_Online_LowSignal, 0),
      SensorAlerts_24Hrs          = ISNULL(SensorsAlerting_24Hrs, 0)
    FROM @Results 
    ORDER BY 
      Type, Division, District, AccountNumber;

END
GO


