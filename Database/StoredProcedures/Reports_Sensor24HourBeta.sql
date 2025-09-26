USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_Schmidt_PulseCounter]    Script Date: 7/1/2024 8:57:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_Schmidt_PulseCounter]
  @Default_OwnerAccountID BIGINT
AS

DECLARE @FromDate DATETIME;
DECLARE @ToDate DATETIME;
DECLARE @TimeZoneIDSTring VARCHAR(300);

DECLARE @Date DATETIME = GETUTCDATE();

SELECT
  @ToDate           = dbo.GetUTCTime( CONVERT(VARCHAR(11),                    dbo.getLocalTime(@Date, TimeZoneIDString) ,120)+ '00:00' , TimeZoneIDSTring),
  @FromDate         = dbo.GetUTCTime( CONVERT(VARCHAR(11), DATEADD(HOUR, -24, dbo.getLocalTime(@Date, TimeZoneIDString)),120)+ '00:00' , TimeZoneIDSTring),
  @TimeZoneIDSTring = TimeZoneIDString
FROM dbo.[Account] a
INNER JOIN dbo.[TimeZone] t on a.TimeZoneID = t.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID;

SELECT 
  s.[SensorID], 
  [Date]  = CONVERT(DATE, dbo.GetLocalTime(MessageDate, @TimeZoneIDSTring)), 
  [Label] = ISNULL(sa2.Value, 'Pulses'),
  [Data]  = SUM(Data * ISNULL(CONVERT(int, sa.Value), 1))
FROM dbo.[Sensor] s
INNER JOIN dbo.[DataMessage] d WITH (NOLOCK) on s.SensorID = d.SensorID
LEFT JOIN dbo.[SensorAttribute] sa WITH (NOLOCK) on s.SensorID = sa.SensorID and sa.Name = 'transformValue'
LEFT JOIN dbo.[SensorAttribute] sa2 WITH (NOLOCK) on s.SensorID = sa2.SensorID and sa2.Name = 'label'
WHERE AccountID     = @Default_OwnerAccountID
  AND MessageDate   >= @FromDate 
  AND MessageDate   < @ToDate
  AND ApplicationID = 90
GROUP BY s.SensorID, CONVERT(DATE, dbo.GetLocalTime(MessageDate, @TimeZoneIDSTring)),  sa2.Value
ORDER BY CONVERT(DATE, dbo.GetLocalTime(MessageDate, @TimeZoneIDSTring));


GO


