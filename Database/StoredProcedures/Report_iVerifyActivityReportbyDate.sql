USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_iVerifyActivityReportbyDate]    Script Date: 6/28/2024 4:05:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_iVerifyActivityReportbyDate]
  @AccountID BIGINT,
  @Date DATETIME
AS


DECLARE @Sensor TABLE (
  SensorID        BIGINT, 
  SensorName      VARCHAR(2000), 
  GemerationType  VARCHAR(100), 
  SensorTypeID    INT,
  ResumeNotificationDate DATETIME
);

DECLARE @Results TABLE (
  DataMessageGUID UNIQUEIDENTIFIER, 
  GatewayID       BIGINT, 
  SensorID        BIGINT, 
  SensorName      VARCHAR(300), 
  MessageDate     DATETIME, 
  Data            BIT, 
  FormattedValue  varchar(50), 
  Battery         INT, 
  SignalStrength  INT, 
  GenerationType  VARCHAR(10), 
  DeviceTypeID    INT,
  ResumeNotificationDate DATETIME
);

DECLARE @DryContactStates TABLE (
  RowID INT IDENTITY(1,1),
  MessageDate DATETIME,
  Data VARCHAR(200),
  State INT)

DECLARE @FromDate   DATETIME,
        @ToDate     DATETIME,
        @TimeOffSet INT,
        @TimeZoneIDString VARCHAR(200),
        @DryContactSensor BIGINT;

SET @FromDate = CONVERT(VARCHAR(10), @Date, 120)
SET @ToDate   = DATEADD(DAY, 1, @FromDate);

SELECT
  @TimeOffSet = -1 * DATEDIFF(MINUTE, @ToDate, dbo.GetLocalTime(@ToDate, TimeZoneIDString)), @TimeZoneIDString = TimeZoneIDString
FROM dbo.[Account] a WITH (NOLOCK)
INNER JOIN dbo.[TimeZone] t WITH (NOLOCK) on a.TimeZoneID = t.TimeZOneID
WHERE a.AccountID = @AccountID;

SET @ToDate = DATEADD(MINUTE, @TimeOffset, @ToDate);

SELECT
  @TimeOffSet = -1 * DATEDIFF(MINUTE, @FromDate, dbo.GetLocalTime(@FromDate, TimeZoneIDString))
FROM dbo.[Account] a WITH (NOLOCK)
INNER JOIN dbo.[TimeZone] t WITH (NOLOCK) on a.TimeZoneID = t.TimeZOneID
WHERE a.AccountID = @AccountID;

SET @FromDate = DATEADD(MINUTE, @TimeOffset, @FromDate);



INSERT INTO @Sensor (SensorID, SensorName, GemerationType, SensorTypeID, ResumeNotificationDate)
SELECT
  sensorid, sensorname, GenerationType, SensorTypeID, ResumeNotificationDate
FROM dbo.[Sensor] s WITH (NOLOCK)
WHERE AccountID = @AccountID
  AND ApplicationID = 16
  AND isdeleted != 1;

SELECT
  @DryContactSensor = sensorid 
FROM dbo.[Sensor] s WITH (NOLOCK)
WHERE AccountID =@AccountID
AND ApplicationID = 3
AND isdeleted != 1


INSERT INTO @Results
SELECT
  DataMessageGUID,
  GatewayID,
  d.SensorID,
  s.SensorName, 
  MessageDate = dbo.GetLocalTime(d.MessageDate, @TimeZoneIDString),
  Data              = 1,
  [FormattedValue]  = 'Force Detected',
  d.Battery,
  d.SignalStrength,
  s.GemerationType,
  s.SensorTypeID,
  s.ResumeNotificationDate
FROM dbo.[DataMessage] d WITH (NOLOCK)
INNER JOIN @Sensor s ON d.SensorID = s.Sensorid
WHERE d.MessageDate BETWEEN @FromDate and @ToDate
  AND d.State & 2 = 2


INSERT INTO @DryContactStates (MessageDate, Data, State)
SELECT
  MessageDate = dbo.GetLocalTime(MessageDate, @TimeZOneIDSTring),
  Data,
  State
FROM dbo.[DataMessage] d WITH (NOLOCK)
WHERE d.SensorID = @DryContactSensor
  AND MessageDate BETWEEN DATEADD(Day, -1, @FromDate) and @ToDate
ORDER BY MessageDate

DELETE @DryContactStates 
WHERE RowID NOT IN (
                    SELECT 
                      RowID 
                    FROM @DryContactStates 
                    WHERE RowID = 1 
                       OR RowID = @@Rowcount 
                       OR state & 02 = 2);



UPDATE @Results
set SignalStrength = (SignalStrength + 90) + 60
where  GenerationType = 'GEN2' AND SignalStrength > -90

UPDATE @Results
Set SignalStrength = ROUND((SignalStrength + 115) * 2.4, 0)
where  GenerationType = 'GEN2' AND SignalStrength <= -90


UPDATE @Results
SET [SignalStrength] = (([SignalStrength]+70)*2)+60
where [DeviceTypeID] = 4 AND GenerationType != 'Gen2' AND SignalStrength > -70

UPDATE @Results
SET [SignalStrength] = ROUND(([SignalStrength]+84) * 4.2857, 0)
where [DeviceTypeID] = 4 AND GenerationType != 'Gen2' AND SignalStrength <= -70

UPDATE @Results
SET [SignalStrength] = (([SignalStrength]+80) * 4.0 / 3.0) + 60
where [DeviceTypeID] != 4 AND GenerationType != 'Gen2' AND SignalStrength > -80

UPDATE @Results
SET [SignalStrength] = ([SignalStrength]+100)*3
where [DeviceTypeID] != 4 AND GenerationType != 'Gen2' AND SignalStrength <= -80


UPDATE @Results
SET [SignalStrength] = 100
where [SignalStrength] > 100;


WITH CTE_DryContact AS
(
  SELECT 
    RowID         = ROW_NUMBER() OVER (PARTITION by 1 ORDER BY MessageDate),
    MessageDate, 
    Data          = CASE WHEN Data = 'False' THEN 'Loop Open' ELSE 'Loop Closed' END, 
    State
  FROM @DryContactStates
)
SELECT
  MessageFrom = c1.MessageDate, 
  MessageTo   = c2.MessageDate, 
  c1.Data
INTO #Temp1
FROM CTE_DryContact c1
INNER JOIN CTE_DryContact c2 ON c1.RowID = c2.RowID - 1

SELECT 
  r.DataMessageGUID, 	
  [Gateway ID]        = r.GatewayID,	
  r.SensorID,	
  [Sensor Name]       = r.SensorName,	
  Date                = CONVERT(DATE, r.MessageDate),	
  Time                = CONVERT(TIME(0), r.MessageDate),
  Value               = r.Data,	
  [Formatted Value]   = r.FormattedValue,
  r.Battery,	
  [Signal Strength]   = r.SignalStrength, 
  Paused              = CASE WHEN r.ResumeNotificationDate > GETUTCDATE() THEN 1 ELSE 0 END,
  DryContactState     = (SELECT TOP 1 ISNULL(data, 'unknown') FROM #Temp1 t WHERE t.MessageFrom <= r.MessageDate ORDER BY t.MessageFrom DESC)
 FROM @Results r
 --LEFT JOIN #Temp1 t on r.MessageDate >= t.MessageFrom and r.MessageDate < t.MessageTo
ORDER BY MessageDate, SensorID

drop table #Temp1
GO


