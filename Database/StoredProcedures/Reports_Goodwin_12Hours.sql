USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_Goodwin_12Hours]    Script Date: 6/28/2024 4:00:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_Goodwin_12Hours]
  @Default_OwnerAccountID BIGINT
AS
/****************************************************
  Report is designed be be run twice daily,
  once at 6 AM and once at 6 PM

  It will return the past 12 hours of DataMessage
  readings (temperature), the min/max thresholds
  and the sensor name with a blank field that the 
  customer can fill out on their own.

  Past 12 hours from the top of the previous hour
  in case it runs at 6:15, we then aim for 6:00 - 6:00
  etc

  -NLN

****************************************************/

DECLARE @FromDate DATETIME = DATEADD(HOUR, -12, convert(Datetime, convert(varchar(13), getutcdate(),120) + ':00'))
DECLARE @ToDate DATETIME =  convert(Datetime, convert(varchar(13), getutcdate(),120) + ':00')

SELECT
  s.SensorName, 
  TargetMinimim     = CONVERT(DECIMAL( 18,2), (CONVERT(DECIMAL(18,2), s.MinimumThreshold) /10.0 * (9.0/5.0)) +32.0), 
  TargetMaximum     = CONVERT(DECIMAL( 18,2), (CONVERT(DECIMAL(18,2), s.MaximumThreshold) /10.0 * (9.0/5.0)) +32.0), 
  ActualTemperature = CONVERT(DECIMAL( 18,2), (CONVERT(DECIMAL(18,2), d.data) /10.0 * (9.0/5.0)) +32.0), 
  MessageDate       = dbo.GetLocalTime(d.MessageDate, t.TimeZoneIDString),
  IfAlarm           = ''
FROM dbo.[Sensor] s WITH (NOLOCK)
INNER JOIN dbo.[DataMessage] d WITH (NOLOCK) on s.SensorID = d.SensorID
INNER JOIN dbo.[Account] a WITH (NOLOCK) on s.AccountID    = a.AccountID
INNER JOIN dbo.[TimeZone] t WITH (NOLOCK) on a.TimeZoneID  = t.TimeZoneID
WHERE d.MessageDate >= @FromDate 
  AND d.MessageDate <= @ToDate
  AND s.AccountID = @Default_OwnerAccountID
ORDER BY SensorName, d.MessageDate;



GO

