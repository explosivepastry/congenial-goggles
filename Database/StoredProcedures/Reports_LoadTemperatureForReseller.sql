USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_LoadTemperatureForReseller]    Script Date: 6/28/2024 4:10:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_LoadTemperatureForReseller]
  @Default_OwnerAccountID BIGINT 
AS

DECLARE @SQL VARCHAR(3000)
DECLARE @TimeZoneIdentifier VARCHAR(300)
DECLARE @OffsetMinutes INT

DECLARE @FromDate DATETIME;
DECLARE @ToDate DATETIME

SET @FromDate = Dateadd(Week, -1, GETUTCDATE())
SET @ToDate = GETUTCDATE();

SELECT 
  @TimezoneIdentifier = tz.TimeZoneIDString 
FROM dbo.[TimeZone] tz WITH (NOLOCK)
INNER JOIN dbo.[Account] a WITH (NOLOCK) ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID

SET @OffsetMinutes  = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));
SET @ToDate         = DATEADD(minute, -1 * @offsetminutes, Convert(datetime,Convert(varchar,DatePart(Year,DATEADD(MINUTE, @OffSetMinutes, GETUTCDATE()))) +'-'+ Convert(varchar,DatePart(month ,DATEADD(MINUTE, @OffSetMinutes, GETUTCDATE())))+'-'+ Convert(varchar,DatePart(Day,DATEADD(MINUTE, @OffSetMinutes, GETUTCDATE())))));

SET @FromDate =DATEADD(WEEK,-1,@ToDate)

--SELECT @FromDate, @ToDate, @TimeZoneIdentifier, @OffsetMinutes

SELECT
s.SensorID, SensorName, s.AccountID, a.AccountNumber, s.ApplicationID
INTO #SensorList
FROM dbo.[Sensor] s WITH (NOLOCK)
INNER JOIN dbo.[Account] a WITH (NOLOCK) ON s.AccountID = a.AccountID
WHERE a.AccountIDTree like '%*'+CONVERT(VARCHAR(300), @Default_OwnerAccountID)+'*%'
  AND s.ApplicationID IN (2,43);

IF @@ROWCOUNT <= 300
BEGIN

SET @SQL = 
'SELECT 
  s.*,
  MessageDate = DateAdd(Minute, '+convert(varchar(30), @offsetminutes) + ', MessageDate),
  data = CONVERT(DECIMAL( 18,2), CASE WHEN s.ApplicationID = 2 THEN data ELSE CONVERT(DECIMAL(18,2), substring(data,  charindex('','', data,  0   )+1, charindex('','', data, charindex('','', data,  0   ) +1  )-charindex('','', data,  0   )-1))  END * (9.0 / 5.0) + 32.0)
FROM #SensorList s
INNER JOIN dbo.[DataMessage] d WITH (NOLOCK) on s.SensorID = d.SensorID
WHERE d.MessageDate >= ''' + CONVERT(VARCHAR(30), @FromDate) + ''' 
  AND d.MessageDate <= ''' + CONVERT(VARCHAR(30), @ToDate)   + '''
ORDER BY SensorID, MessageDate;'


EXEC (@SQL);

END ELSE
BEGIN

SELECT
  SensorID = NULL,
  SensorName = 'Over 300 Sensors',
  AccountID = NULL,
  AccountNumber = NULL,
  ApplicationID = NULL,
  MessageDate = NULL, 
  Data = NULL;

END

GO


