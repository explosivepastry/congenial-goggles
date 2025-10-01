USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_GatewayExportInternal]    Script Date: 6/28/2024 3:53:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_GatewayExportInternal]
  @FromDate DATETIME,
  @ToDate DATETIME,
  @GatewayID BIGINT
AS

--Proc Specific
DECLARE @SQL VARCHAR(2000)
DECLARE @AccountID BIGINT
declare @timezoneidentifier varchar(255)
declare @offsetminutes int
DECLARE @StartMonth DATETIME
DECLARE @EndMOnth DATETIME


CREATE TABLE #DMTemp
(
  ReceivedDate DATETIME,
  GatewayID BIGINT,
  GatewayMessageGUID VARCHAR(300),
  Power INT,
  Battery INT,
  MeetsNotificationRequirement BIT,
  MEssageTime INT,
  MessageCount INT,
  SignalStrength INT
)


SET @AccountID = ISNULL((Select AccountID from Gateway g inner join csnet c on c.csnetid = g.csnetid where GatewayID = @GatewayID), 2)

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



  DECLARE @Year VARCHAR(50)

WHILE @StartMonth <= @EndMonth
BEGIN


    WAITFOR DELAY '00:00:01'

    IF @StartMonth < '2018-01-01'
    BEGIN

        SET @Year = 'MonnitMessages'

    END ELSE
    BEGIN

        SET @YEar = 'iMonnitMessages' + CONVERT(VARCHAR(30), DATEPART(YEAR, @StartMonth))

    END

    SET @SQL = 
    '
    select *
     from '+ @Year +'.dbo.gatewaymessage_'+CONVERT(VARCHAR(6), @StartMonth, 112)+' d with (NOLOCK) 
    WHERE GatewayID = '+CONVERT(VARCHAR(20), @GatewayID)+'
      AND ReceivedDate BETWEEN '''+CONVERT(VARCHAR(30), @FromDate)+''' and '''+CONVERT(VARCHAR(30),@ToDate)+''''

    INSERT INTO #DMtemp
    EXEC (@SQL)

    SET @StartMonth = DATEADD(MONTH, 1, @StartMonth)

END

select * from #DMTemp
order by ReceivedDate;
GO


