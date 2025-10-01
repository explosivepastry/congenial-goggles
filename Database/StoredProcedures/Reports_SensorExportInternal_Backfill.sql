USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_SensorExportInternal_Backfill]    Script Date: 7/1/2024 9:02:19 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_SensorExportInternal_Backfill]
AS

declare @Default_OwnerAccountID BIGINT		= 22752
declare @SensorID BIGINT					= 688696
declare @FromDate DATETIME					= '2022-11-01'
declare @ToDate DATETIME					= '2022-12-01'

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
[IsAuthenticated] BIT,
[ApplicationID] INT
)

/*----- Testing Parameters ------*/
--DECLARE @SensorID BIGINT = 88559;
--DECLARE @FromDate DATETIME = '2016-01-01 00:00';
--DECLARE @ToDate DATETIME = '2016-03-01 00:00 '
--DECLARE @Default_OwnerAccountID BIGINT = 16737


--Proc Specific
DECLARE @SQL VARCHAR(2000)
DECLARE @AccountID BIGINT
declare @timezoneidentifier varchar(255)
declare @offsetminutes int
DECLARE @StartMonth DATETIME
DECLARE @EndMOnth DATETIME
DECLARE @ApplicationID BIGINT


SET @AccountID = (Select AccountID from Sensor where SensorID = @SensorID)

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


 -- Table 0 type report to run --
SELECT 'Sensor' AS 'Type_Of_Sensor_Report';

SET @ApplicationID = (SELECT ApplicationID FROM dbo.[Sensor] WITH (NOLOCK) WHERE SensorID = @SensorID)

-- Table 1 Basic information needed to run report --
SELECT @FromDate AS 'From'
 ,@ToDate AS 'To'
 ,@TimezoneIdentifier AS 'Time_Zone_StringID'
 ,SensorID AS 'SensorID'
 ,@AccountID AS 'AccountID'
 ,ISNULL(CSNetID, 1) AS 'CSNetID'
FROM Sensor WHERE SensorID = @SensorID;

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


 DECLARE @Year VARCHAR(50)

WHILE @StartMonth <= @EndMonth
BEGIN


    WAITFOR DELAY '00:00:01'

    IF @StartMonth < '2018-01-01'
    BEGIN

        SET @Year = 'iMonnitMessages'

    END ELSE
    BEGIN

        SET @YEar = 'iMonnitMessages' + CONVERT(VARCHAR(30), DATEPART(YEAR, @StartMonth))

    END

    IF @Year IN ('iMonnitMessages', 'iMonnitMessages2018')
    BEGIN

        SET @SQL = 
    '
    select 
      [MessageDate],
      [SensorID],
      [DataMessageGUID],
      [State],
      [SignalStrength],
      [LinkQuality],
      [Battery],
      [Data],
      [Voltage],
      [MeetsNotificationRequirement],
      [InsertDate],
      [GatewayID],
      [HasNote],
      [IsAuthenticated] =  NULL,
      [ApplicationID] = '+CONVERT(VARCHAR(3), @ApplicationID) +'
     from '+ @Year +'.dbo.DataMessage_'+CONVERT(VARCHAR(6), @StartMonth, 112)+' d with (NOLOCK) 
    WHERE SensorID = '+CONVERT(VARCHAR(20), @SensorID)+'
      AND MessageDate BETWEEN '''+CONVERT(VARCHAR(30), @FromDate)+''' and '''+CONVERT(VARCHAR(30),@ToDate)+''''

    INSERT INTO #DMtemp
    EXEC (@SQL)
    PRINT(@SQL)

    END ELSE
    BEGIN

    SET @SQL = 
    '
    select 
      [MessageDate],
      [SensorID],
      [DataMessageGUID],
      [State],
      [SignalStrength],
      [LinkQuality],
      [Battery],
      [Data],
      [Voltage],
      [MeetsNotificationRequirement],
      [InsertDate],
      [GatewayID],
      [HasNote],
      [IsAuthenticated] =  [IsAuthenticated],
      [ApplicationID] = '+CONVERT(VARCHAR(3), @ApplicationID) +'
     from '+ @Year +'.dbo.DataMessage_'+CONVERT(VARCHAR(6), @StartMonth, 112)+' d with (NOLOCK) 
    WHERE SensorID = '+CONVERT(VARCHAR(20), @SensorID)+'
      AND MessageDate BETWEEN '''+CONVERT(VARCHAR(30), @FromDate)+''' and '''+CONVERT(VARCHAR(30),@ToDate)+''''

    INSERT INTO #DMtemp
    EXEC (@SQL)
    PRINT(@SQL)

    END

    SET @StartMonth = DATEADD(MONTH, 1, @StartMonth)

END


 
 --SET @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));

--SELECT 
--    [MessageDate] = DATEADD(MINUTE, @offsetminutes, [MessageDate]),
--    [SensorID],
--    [DataMessageGUID],
--    [State],
--    [SignalStrength],
--    [LinkQuality],
--    [Battery],
--    [Data],
--    [Voltage],
--    [MeetsNotificationRequirement],
--    [InsertDate] = DATEADD(MINUTE, @offsetminutes, [InsertDate]),
--    [GatewayID],
--    [HasNote]
--FROM #DMtemp ORDER BY MessageDate


select dm.MessageDate 'MessageDate', dm.SensorID, dm.DataMessageGUID, dm.State, dm.SignalStrength, dm.LinkQuality, dm.Battery
,dm.Data ,dm.Voltage, dm.MeetsNotificationRequirement, dm.InsertDate, dm.GatewayID, dm.HasNote, dm.IsAuthenticated, dm.ApplicationID  from #DMtemp dm
order by MessageDate


drop table #DMtemp

GO


