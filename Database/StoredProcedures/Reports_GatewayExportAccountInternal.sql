USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_GatewayExportAccountInternal]    Script Date: 6/28/2024 3:51:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_GatewayExportAccountInternal]
  @Default_OwnerAccountID BIGINT,
  @AccountID BIGINT,
  @FromDate DATETIME,
  @ToDate DATETIME
AS

CREATE TABLE #GMtemp(
[ReceivedDate]	datetime,
[GatewayID]	bigint,
[GatewayMessageGUID]	uniqueidentifier,
[Power]	int,
[Battery]	int,
[MeetsNotificationRequirement]	bit,
[MessageType]	int,
[MessageCount]	int,
[SignalStrength]	int
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


 SELECT
 gatewayid, StartDate = createdate
 INTO #GatewayList
 FROM Gateway where gatewayid in (104437,
106736,
198727,
104197,
104227)

 DECLARE @Year VARCHAR(50)

WHILE @StartMonth <= @EndMonth
BEGIN


    WAITFOR DELAY '00:00:01'

    IF @StartMonth < '2018-01-01'
    BEGIN

        SET @Year = 'iMonnitMessages'

    END ELSE
    BEGIN

        SET @YEar = 'iMonnitMessages2018'

    END

    SET @SQL = 
    '
    select d.*
     from '+ @Year +'.dbo.GatewayMessage_'+CONVERT(VARCHAR(6), @StartMonth, 112)+' d with (NOLOCK) 
    INNER JOIN #GatewayList s on d.GatewayID = s.GatewayID 
    WHERE ReceivedDate BETWEEN '''+CONVERT(VARCHAR(30), @FromDate)+''' and '''+CONVERT(VARCHAR(30),@ToDate)+''''

    INSERT INTO #GMtemp
    EXEC (@SQL)

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


select  
ReceivedDate = dbo.GetLocalTime(ReceivedDate, @timezoneidentifier),
GatewayID,
GatewayMessageGUID,
Power,
Battery,
MeetsNotificationRequirement,
MessageType,
MessageCount,
SignalStrength from #GMtemp dm
order by GatewayID, ReceivedDate
GO


