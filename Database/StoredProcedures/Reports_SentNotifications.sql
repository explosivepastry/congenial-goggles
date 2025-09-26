USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_SentNotifications]    Script Date: 7/1/2024 9:04:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_SentNotifications]
  @Default_OwnerAccountID BIGINT,
  @FromDate               DATETIME,
  @ToDate                 DATETIME,
  @Count                  INT
AS

/* ----| Sent Notifications Report |---- */

/*
----| Maintenance Log |----
Created 2016-04-20 Brandon
Modified 2016-07-05 Ala
Modified 2016-09-30 NLN - Optimize for unknown
*/

/*Required AutoVariables*/
--DECLARE @Default_OwnerAccountID INT = 8662
--DECLARE @ToDate DATETIME = '09-29-2016 00:00:00'
--DECLARE @FromDate DATETIME = '09-01-2016 00:00:00'
--DECLARE @Count INT = 50

/* ----| Sql Script |---- */
DECLARE @OffsetMinutes INT

SELECT 
  @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),tz.TimeZoneIDString ))
FROM dbo.TimeZone tz     WITH(NOLOCK)
INNER JOIN dbo.Account a WITH(NOLOCK) ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID

BEGIN
	SELECT @ToDate = DATEADD(MINUTE, 1439, @ToDate)
	SELECT @FromDate = DATEADD(MINUTE, @OffsetMinutes, @FromDate)
	SELECT @ToDate = DATEADD(MINUTE, @OffsetMinutes, @ToDate)
END

SELECT top (@Count)
	nr.NotificationRecordedID,
  nr.SensorID, 
  nr.GatewayID,
  CustomerName = c.FirstName + ' ' + c.LastName,
  nr.SentTo,
  cs.Name 'NetworkName', 
  s.SensorName, 
  nr.Reading, 
  nr.NotificationSubject, 
  DATEADD(MINUTE, @OffsetMinutes, nr.NotificationDate)'NotificationDate',
  CASE(nr.eNotificationType)
	    WHEN 1 THEN 'Email'
	    WHEN 2 THEN 'SMS'
      WHEN 5 THEN 'Control'
	    WHEN 6 THEN 'Phone'
      WHEN 10 THEN 'Local Notifier'
      WHEN 12 THEN 'System Action'
      WHEN 13 THEN 'Thermostat'
    END AS 'NotificationType',
  CASE(n.eNotificationClass)
	    WHEN 1 THEN 'Application'
	    WHEN 2 THEN 'Inactivity'
	    WHEN 3 THEN 'Low_Battery'
	    WHEN 5 THEN 'Advanced'
	    WHEN 6 THEN 'Credit'
    END AS 'Notification Class', 
  nr.Status
FROM dbo.NotificationRecorded nr  WITH(NOLOCK)
INNER JOIN dbo.Notification n     WITH(NOLOCK) ON n.NotificationID = nr.NotificationID
LEFT JOIN dbo.Sensor s            WITH(NOLOCK) ON s.SensorID = nr.SensorID AND s.AccountID = n.AccountID
LEFT JOIN dbo.CSNet cs            WITH(NOLOCK) ON cs.CSNetID = s.CSNetID AND cs.AccountID = n.AccountID
LEFT JOIN dbo.Customer c          WITH(NOLOCK) ON nr.CustomerID = c.CustomerID
WHERE n.AccountID = @Default_OwnerAccountID
	AND (nr.NotificationDate BETWEEN @FromDate AND @ToDate)
ORDER BY NotificationDate DESC
GO


