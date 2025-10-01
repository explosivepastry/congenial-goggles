USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_CanteenDistrict_DeviceSummary]    Script Date: 6/24/2024 2:45:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_CanteenDistrict_DeviceSummary]
  @Default_OwnerAccountID BIGINT
AS

DECLARE @Results TABLE
(
  [AccountNumber] VARCHAR(300),
  [SensorCount] INT,
  [SensorsOffline] INT,
  [Sensors_Online_LowBattery] INT,
  [Sensors_Online_LowSignal] INT,
  [SensorsAlerting_24Hrs] INT,
  [GatewayCount] INT,
  [GatewaysOffline] INT,
  [Type] INT
)

SELECT
  a.[AccountNumber], 
  s.[SensorID], 
  s.[ReportInterval], 
  s.[LastCommunicationDate], 
  s.[LastDataMessageGUID], 
  s.[GenerationType], 
  s.[SensorTypeID],
  District          = a3.[AccountNumber]
INTO #Sensors
FROM dbo.[Account] a       WITH (NOLOCK)
LEFT JOIN dbo.[Sensor] s  WITH (NOLOCK)  ON a.AccountID = s.AccountID
LEFT JOIN dbo.[Account] a3 WITH (NOLOCK)  ON a.RetailAccountID = a3.AccountID                  
WHERE a.AccountidTree like '%*' + CONVERT(VARCHAR(30), @Default_OwnerAccountID) + '*%'


select
  a.[AccountNumber], 
  g.[GatewayID], 
  g.[ReportInterval], 
  ISNULL(gs.LastCommunicationDate, g.LastCommunicationDate) 'LastCommunicationDate', 
  District = a3.[AccountNumber]
INTO #Gateways
from dbo.[Account] a       WITH (NOLOCK)
INNER JOIN dbo.[CSNet] c   WITH (NOLOCK) ON a.AccountID = c.AccountID
INNER JOIN dbo.[Gateway] g WITH (NOLOCK) ON c.CSNetID = g.CSNetID
LEFT JOIN dbo.GatewayStatus gs WITH (NOLOCK) on gs.GatewayID = g.GatewayID
LEFT JOIN dbo.[Account] a3 WITH (NOLOCK) ON a.RetailAccountID = a3.AccountID          
WHERE a.AccountidTree like '%*' + CONVERT(VARCHAR(30), @Default_OwnerAccountID) + '*%'

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


INSERT INTO @Results
SELECT
	s.AccountNumber,
	--District,
	SensorCount                 = (SELECT ISNULL(COUNT(*), 0) from #Sensors  s2 where s2.AccountNumber = s.AccountNumber AND s2.SensorID IS NOT NULL group by s2.AccountNumber),
	SensorsOffline              = (SELECT ISNULL(COUNT(*), 0) from #Sensors  s2 where s2.AccountNumber = s.AccountNumber AND s2.SensorID IS NOT NULL AND s2.LastCommunicationDate not between dateadd(minute, -2.0*s2.reportinterval, getutcdate()) and getutcdate() group by s2.AccountNumber),
	[Sensors_Online_LowBattery] = (SELECT ISNULL(COUNT(*), 0) from #DMSensor s2 where s2.AccountNumber = s.AccountNumber AND s2.SensorID IS NOT NULL AND  s2.Battery >= 0 and s2.Battery < 25 and s2.LastCommunicationDate  between dateadd(minute, -2.0*s2.reportinterval, getutcdate()) and getutcdate() group by s2.AccountNumber ),
	[Sensor_Online_LowSignal]   = (SELECT ISNULL(COUNT(*), 0) from #DMSensor s2 where s2.accountnumber = s.accountnumber AND s2.SensorID IS NOT NULL and  s2.LastSignalStrength < 25 and s2.LastCommunicationDate  between dateadd(minute, -2.0*s2.reportinterval, getutcdate()) and getutcdate() ),
  SensorAlerts_24Hrs          = (SELECT SUM(HasAlert) FROM #DMSensor s2 WHERE s2.AccountNumber = s.AccountNumber),
	GatewayCount                = g2.gatewaycount,
	GatewaysOffline             = g3.gatewaycount,
  Type = 1
FROM #Sensors s
left join (
            select
              accountnumber, 
              gatewaycount = count(*) 
            from #Gateways g 
            group by g.AccountNumber
          ) g2 on s.accountnumber = g2.accountnumber
left join (
          select 
            accountnumber, 
            gatewaycount = count(*) 
            from #Gateways g 
            where LastCommunicationDate is null 
               or LastCommunicationDate < dateadd(minute, -2*reportinterval, getutcdate()) 
               or LastCommunicationDate > getutcdate() 
            group by g.AccountNumber
          ) g3 on s.accountnumber = g3.accountnumber
GROUP BY s.AccountNUmber, g2.gatewaycount, g3.gatewaycount--, District


INSERT INTO @Results
SELECT
AccountNumber,
0,
0,
0,
0,
0,
COUNT(*),
(SELECT COUNT(*) FROM #Gateways g2 where g2.AccountNumber= g.accountnumber and (LastCommunicationDate is null 
               or LastCommunicationDate < dateadd(minute, -2*reportinterval, getutcdate()) 
               or LastCommunicationDate > getutcdate() )),
               1
FROM #Gateways g
where accountnumber not in (select accountnumber from #Sensors)
GROUP BY AccountNumber

INSERT INTO @Results
SELECT
	AccountNumber = 'Total',
	--district = '',
	SensorCount                 = (SELECT ISNULL(COUNT(*), 0) from #Sensors  s2),
	SensorsOffline              = (SELECT ISNULL(COUNT(*), 0) from #Sensors  s2 where  s2.LastCommunicationDate not between dateadd(minute, -2.0*s2.reportinterval, getutcdate()) and getutcdate() AND s2.SensorID IS NOT NULL),
	[Sensors_Online_LowBattery] = (SELECT ISNULL(COUNT(*), 0) from #DMSensor s2 where  s2.Battery >= 0 and s2.Battery < 25 AND s2.SensorID IS NOT NULL and s2.LastCommunicationDate  between dateadd(minute, -2.0*s2.reportinterval, getutcdate()) and getutcdate()  ),
	[Sensor_Online_LowSignal]   = (SELECT ISNULL(COUNT(*), 0) from #DMSensor s2 where  s2.LastSignalStrength < 25 AND s2.SensorID IS NOT NULL and s2.LastCommunicationDate  between dateadd(minute, -2.0*s2.reportinterval, getutcdate()) and getutcdate() ),
  SensorAlerts_24Hrs          = (SELECT SUM(HasAlert) FROM #DMSensor s2),
	GatewayCount                = (select GatewayCount = count(*) from #Gateways g),
	GatewaysOffline             = (select GatewayCount = count(*) from #Gateways g where LastCommunicationDate is null or LastCommunicationDate < dateadd(minute, -2.0*reportinterval, getutcdate()) or LastCommunicationDate > getutcdate()),
  Type = 2

SELECT 
  AccountNumber,
  SensorCount                 = ISNULL(SensorCount, 0),
  SensorsOffline              = ISNULL(SensorsOffline, 0),
  Sensors_Online_LowBattery   = ISNULL(Sensors_Online_LowBattery, 0),
  Sensors_Online_LowSignal    = ISNULL(Sensors_Online_LowSignal, 0),
  SensorAlerts_24Hrs          = ISNULL(SensorsAlerting_24Hrs, 0),
  GatewayCount                = ISNULL(GatewayCount, 0),
  GatewaysOffline             = ISNULL(GatewaysOffline, 0)
FROM @Results 
ORDER BY 
  Type, AccountNumber
GO


