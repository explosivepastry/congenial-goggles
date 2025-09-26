USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_NotificationReseller]    Script Date: 7/1/2024 8:49:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_NotificationReseller]
  @Default_OwnerAccountID BIGINT,
  @Default_ReportScheduleID BIGINT
AS

DECLARE @FromDate DATETIME
DECLARE @ToDate DATETIME
DECLARE @OffsetMinutes INT
DECLARE @TimezoneIdentifier VARCHAR(255)

/**************************************************

  SET From/ToDate based on account time zone
          and ScheduleType

**************************************************/
SELECT 
  @TimezoneIdentifier = tz.TimeZoneIDString 
FROM dbo.[TimeZone] tz WITH (NOLOCK)
INNER JOIN dbo.[Account] a WITH (NOLOCK) ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID;

SET @OffsetMinutes  = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));
SET @ToDate         = CONVERT(DATETIME,CONVERT(VARCHAR,DATEPART(YEAR,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))) +'-'+ CONVERT(VARCHAR,DATEPART(MONTH ,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate())))+'-'+ Convert(varchar,DatePart(Day,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))));


SELECT @FromDate =
      CASE 
         WHEN ScheduleType = 3 THEN DATEADD(DAY,-1,@ToDate)
         WHEN ScheduleType = 2 THEN DATEADD(WEEK,-1,@ToDate)
         WHEN ScheduleType = 1 THEN DATEADD(MONTH,-1,@ToDate)
         WHEN ScheduleType = 0 THEN DATEADD(YEAR,-1,@ToDate)
         ELSE DATEADD(DAY,-1,@ToDate)
      END
FROM dbo.[ReportSchedule] WITH (NOLOCK)
WHERE ReportScheduleID = @Default_ReportScheduleID;

SELECT * FROM (
SELECT
a.AccountID, a.AccountNumber, NotificationName = n.Name, NotificationType = 'Sensor', nt.StartTime, DeviceID = s.SensorID, DeviceName = s.SensorName
FROM Account a
INNER JOIN Notification n  WITH (NOLOCK) on a.AccountID = n.AccountID 
INNER JOIN NotificationTriggered nt  WITH (NOLOCK) on n.NotificationID = nt.NotificationID 
INNER JOIN SensorNotification sn  WITH (NOLOCK) on nt.SensorNotificationID = sn.SensorNotificationID 
INNER JOIN Sensor s  WITH (NOLOCK) on sn.SensorID = s.SensorID
WHERE a.AccountIDTree LIKE '%*'+CONVERT(VARCHAR(300), @Default_OwnerAccountID)+'*%'
AND nt.StartTime >= @FromDate and nt.StartTime <= @ToDate
and n.IsDeleted = 0
UNION ALL
SELECT
a.AccountID, a.AccountNumber, NotificationName = n.Name, NotificationType = 'Gateway', nt.StartTime, DeviceID = s2.GatewayID, DeviceName = s2.Name
FROM Account a
INNER JOIN Notification n  WITH (NOLOCK) on a.AccountID = n.AccountID 
INNER JOIN NotificationTriggered nt  WITH (NOLOCK) on n.NotificationID = nt.NotificationID 
INNER JOIN GatewayNotification sn  WITH (NOLOCK) on nt.GatewayNotificationID = sn.GatewayNotificationID 
INNER JOIN Gateway s2  WITH (NOLOCK) on sn.GatewayID = s2.GatewayID
WHERE a.AccountIDTree LIKE '%*'+CONVERT(VARCHAR(300), @Default_OwnerAccountID)+'*%'
AND nt.StartTime >= @FromDate and nt.StartTime <= @ToDate
and n.IsDeleted = 0) t 
ORDER BY NotificationType DESC, StartTime;

GO


