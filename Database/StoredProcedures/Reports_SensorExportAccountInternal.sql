USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_SensorExportAccountInternal]    Script Date: 7/1/2024 9:00:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_SensorExportAccountInternal]
  @Default_OwnerAccountID BIGINT,
  @AccountID BIGINT,
  @FromDate DATETIME,
  @ToDate DATETIME
AS

CREATE TABLE #DMtemp(
[MessageDate] DATETIME,
[SensorID] BIGINT,
[DataMessageGUID] VARCHAR(255),
[State] INT,
[SignalStrength] INT,
[LinkQuality] INT,
[Battery] INT,
[Data] VARCHAR(500),
[Voltage] DECIMAL(5,4),
[MeetsNotificationRequirement] BIT,
[InsertDate] DATETIME,
[GatewayID] BIGINT,
[HasNote] BIT,
[IsAuthenticated] BIT
)

/*----- Testing Parameters ------*/
--DECLARE @SensorID BIGINT = 88559;
--DECLARE @FromDate DATETIME = '2016-01-01 00:00';
--DECLARE @ToDate DATETIME = '2016-03-01 00:00 '
--DECLARE @Default_OwnerAccountID BIGINT = 16737


--Proc Specific
DECLARE @SQL VARCHAR(2000)
declare @timezoneidentifier varchar(255)
declare @offsetminutes int
DECLARE @StartMonth DATETIME
DECLARE @EndMOnth DATETIME

  SELECT 
   @FromDate = DATEADD(minute, DATEDIFF(MINUTE, dbo.GetLocalTime(GetUTCDATE(), TimeZoneIDSTring), GETUTCDATE()), @FromDate),
   @ToDate = DATEADD(minute, DATEDIFF(MINUTE, dbo.GetLocalTime(GetUTCDATE(), TimeZoneIDSTring), GETUTCDATE()), @ToDate)
 FROM dbo.[TimeZone] tz      WITH(NOLOCK) 
 INNER JOIN dbo.[Account] a  WITH(NOLOCK) ON a.[TimeZoneID] = tz.[TimeZoneID]
 WHERE a.[AccountID] = @AccountID


SET @StartMonth = CONVERT(VARCHAR(7), @FromDate, 120)+'-01'
SET @EndMonth = CONVERT(VARCHAR(10), @ToDate, 120)

 SELECT 
   @TimezoneIdentifier = tz.[TimeZoneIDString] 
 FROM dbo.[TimeZone] tz      WITH(NOLOCK) 
 INNER JOIN dbo.[Account] a  WITH(NOLOCK) ON a.[TimeZoneID] = tz.[TimeZoneID]
 WHERE a.[AccountID] = @AccountID


 -- TABLE 0 TYPE REPORT TO RUN --
SELECT 'Account' AS 'Type_Of_Sensor_Report';

-- TABLE 1 BASIC INFORMATION NEEDED TO RUN REPORT --
SELECT @FromDate AS 'From'
 ,@ToDate AS 'To'
 ,@TimezoneIdentifier AS 'Time_Zone_StringID'
 ,@AccountID  AS 'AccountID'
 --,67631 AS 'CSNetID'

-- Table 2 Column Names for Report --
SELECT 
  'True' AS 'DataMessageGUID'
 ,CONVERT(BIT, 1) AS 'Date'
 ,CONVERT(BIT, 1) AS 'GatewayID'
 ,'True' AS 'SensorID'
 ,CONVERT(BIT, 1) AS 'Sensor_Name'
 ,CONVERT(BIT, 1) AS 'Sensor_State'
 ,CONVERT(BIT, 1) AS 'Raw_Data'
 ,CONVERT(BIT, 1) AS 'Value'
 ,CONVERT(BIT, 1) AS 'Formatted_Value'
 ,CONVERT(BIT, 1) AS 'Alert_Sent'
 ,CONVERT(BIT, 1) AS 'Signal_Strength'
 ,CONVERT(BIT, 1) AS 'Voltage'
 ,CONVERT(BIT, 1) AS 'Battery'
 ,CONVERT(BIT, 1) AS 'Special';


-- SELECT
-- SensorID, StartDate = ISNULL( ISNULL(startdate, createdate), '2015-12-01'), accountid
-- INTO #SensorList
-- FROM Sensor where sensorid in 
-- (
--751117,
--807627,
--807633,
--807674,
--839748,
--839752,
--839754,
--839755,
--839756,
--839759,
--839762,
--839764,
--839821,
--839822,
--839823,
--839832,
--839835,
--839837,
--839838,
--839840,
--839842,
--839843,
--839844,
--839845,
--839847,
--839848,
--839849,
--839851,
--840253
--)


-- DECLARE @Year VARCHAR(50)

--WHILE @StartMonth <= @EndMonth
--BEGIN


--    WAITFOR DELAY '00:00:01'

--    IF @StartMonth < '2018-01-01'
--    BEGIN

--        SET @Year = 'iMonnitMessages'

--    END ELSE
--    BEGIN

--        SET @YEar = 'iMonnitMessages' + CONVERT(VARCHAR(4), DATEPART(YEAR, @StartMonth))

--    END

--    IF @Year IN ('iMonnitMessages', 'iMonnitMessages2018')
--    BEGIN

--        SET @SQL = 
--    '
--    select 
--      [MessageDate],
--      d.[SensorID],
--      [DataMessageGUID],
--      [State],
--      [SignalStrength],
--      [LinkQuality],
--      [Battery],
--      [Data],
--      [Voltage],
--      [MeetsNotificationRequirement],
--      [InsertDate],
--      [GatewayID],
--      [HasNote],
--      [IsAuthenticated] =  NULL
--     from '+ @Year +'.dbo.DataMessage_'+CONVERT(VARCHAR(6), @StartMonth, 112)+' d with (NOLOCK) 
--    INNER JOIN #SensorList s on d.sensorID = s.SensorID 
--    WHERE MessageDate BETWEEN '''+CONVERT(VARCHAR(30), @FromDate)+''' and '''+CONVERT(VARCHAR(30),@ToDate)+''''

--    INSERT INTO #DMtemp
--    EXEC (@SQL)
--    PRINT(@SQL)

--    END ELSE
--    BEGIN

--    SET @SQL = 
--    '
--    select 
--      [MessageDate],
--      d.[SensorID],
--      [DataMessageGUID],
--      [State],
--      [SignalStrength],
--      [LinkQuality],
--      [Battery],
--      [Data],
--      [Voltage],
--      [MeetsNotificationRequirement],
--      [InsertDate],
--      [GatewayID],
--      [HasNote],
--      [IsAuthenticated] =  [IsAuthenticated]
--     from '+ @Year +'.dbo.DataMessage_'+CONVERT(VARCHAR(6), @StartMonth, 112)+' d with (NOLOCK) 
--    INNER JOIN #SensorList s on d.sensorID = s.SensorID 
--    WHERE MessageDate BETWEEN '''+CONVERT(VARCHAR(30), @FromDate)+''' and '''+CONVERT(VARCHAR(30),@ToDate)+''''

--    INSERT INTO #DMtemp
--    EXEC (@SQL)
--    PRINT(@SQL)

--    END

--    SET @StartMonth = DATEADD(MONTH, 1, @StartMonth)

--END
 
-- SET @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));

--INSERT INTO TesterDB.dbo.DataMessage_Report
--SELECT 
--    [MessageDate],
--    [SensorID],
--    [DataMessageGUID],
--    [State],
--    [SignalStrength],
--    [LinkQuality],
--    [Battery],
--    [Data],
--    [Voltage],
--    [MeetsNotificationRequirement],
--    [InsertDate],
--    [GatewayID],
--    [HasNote], 
--    IsAuthenticated
--FROM #DMtemp ORDER BY MessageDate



select  DataMessageGUID, 
    dm.SensorID,  
    dm.MessageDate,
    State, 
    SignalStrength, 
    LinkQuality, 
    Battery, 
    Data, 
    Voltage, 
    MeetsNotificationRequirement, 
    InsertDate, 
    GatewayID, 
    dm.HasNote,
    dm.[IsAuthenticated]
from dbo.[DataMessage_Report] dm with (NOLOCK)
ORDER BY SensorID, MessageDate
GO


