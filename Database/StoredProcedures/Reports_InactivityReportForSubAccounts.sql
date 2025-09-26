USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_InactivityReportForSubAccounts]    Script Date: 6/28/2024 4:03:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_InactivityReportForSubAccounts]
  @Default_OwnerAccountID BIGINT,
  @Minute INT,
  @IncludeSensor BIT,
  @IncludeGateway BIT
AS

/*Required Autovariables*/
--DECLARE @Default_OwnerAccountID BIGINT = 3420
--DECLARE @Minute INT = 5000
--DECLARE @IncludeSensor BIT = 1;
--DECLARE @IncludeGateway BIT = 1;

DECLARE @FromDate DATETIME = DATEADD(MINUTE, -1 * @Minute, GETUTCDATE());
DECLARE @TimezoneIdentifier VARCHAR(255);
DECLARE @OffsetMinutes INT;

CREATE TABLE #DeviceAccount
( 
  RowID INT IDENTITY (1,1),
  AccountID BIGINT,
  AccountNumber VARCHAR(100),
  GatewayID   BIGINT,
  SensorID BIGINT,
  LastCommunicationDate DATETIME,
  AccountIDTree VARCHAR(100)
)

SELECT @TimezoneIdentifier = tz.TimeZoneIDString 
FROM dbo.TimeZone tz WITH(NOLOCK) 
INNER JOIN dbo.Account a  WITH(NOLOCK) ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID

SET @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));

INSERT INTO #DeviceAccount
(
  AccountID,
  AccountNumber,
  SensorID,
  LastCommunicationDate,
  AccountIDTree
)
SELECT
  a.[AccountID],
  a.[AccountNumber],
  s.[SensorID],
  s.[LastCommunicationDate],
  a.[AccountIDTree]
FROM Account a WITH (NOLOCK)
INNER JOIN Sensor s WITH (NOLOCK) ON a.AccountID = s.AccountID
WHERE a.AccountIDTree LIKE '%*'+CONVERT(VARCHAR(30), @Default_OwnerAccountID)+'*%'
and s.IsDeleted != 1
and s.LastCommunicationDate < @FromDate

INSERT INTO #DeviceAccount
(
  AccountID,
  AccountNumber,
  GatewayID,
  LastCommunicationDate,
  AccountIDTree
)
SELECT
  a.[AccountID],
  a.[AccountNumber],
  g.[GatewayID],
  ISNULL(gs.LastCommunicationDate, g.LastCommunicationDate),
  a.[AccountIDTree]
FROM Account a WITH (NOLOCK)
INNER JOIN CSNet c on a.AccountID = c.AccountID
INNER JOIN Gateway g WITH (NOLOCK) ON c.CSNetID = g.CSNetID
LEFT JOIN dbo.GatewayStatus gs WITH (NOLOCK) on gs.GatewayID = g.GatewayID
WHERE a.AccountIDTree LIKE '%*'+CONVERT(VARCHAR(30), @Default_OwnerAccountID)+'*%'
and g.IsDeleted != 1
and ISNULL(gs.LastCommunicationDate, g.LastCommunicationDate) < @FromDate


IF @IncludeGateway = 1 AND @IncludeSensor = 1
BEGIN

  SELECT
    AccountID,
    AccountNumber,
    GatewayID,
    SensorID,
    LastCommunicationDate = DATEADD(MINUTE, @OffsetMinutes, LastCommunicationDate),
    [Minutes Since Last Checkin] = DATEDIFF(MINUTE, LastCommunicationDate, GETUTCDATE())
  FROM #DeviceAccount
  order by DATEDIFF(MINUTE, LastCommunicationDate, GETUTCDATE()) desc

END ELSE
IF @IncludeGateway = 0 AND @IncludeSensor = 1
BEGIN

  SELECT
    AccountID,
    AccountNumber,
    GatewayID,
    SensorID,
    LastCommunicationDate,
    [Minutes Since Last Checkin] = DATEDIFF(MINUTE, LastCommunicationDate, GETUTCDATE())
  FROM #DeviceAccount
  WHERE SensorID IS NOT NULL
  order by  DATEDIFF(MINUTE, LastCommunicationDate, GETUTCDATE()) desc

END ELSE
IF @IncludeGateway = 1 AND @IncludeSensor = 0
BEGIN

  SELECT
    AccountID,
    AccountNumber,
    GatewayID,
    SensorID,
    LastCommunicationDate,
    [Minutes Since Last Checkin] = DATEDIFF(MINUTE, LastCommunicationDate, GETUTCDATE())
  FROM #DeviceAccount
  WHERE GatewayID IS NOT NULL
  order by DATEDIFF(MINUTE, LastCommunicationDate, GETUTCDATE()) desc

END

drop table #DeviceAccount


GO


