USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_CFR_AlertSentAcknowledgeByAccount]    Script Date: 6/24/2024 2:46:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_CFR_AlertSentAcknowledgeByAccount]
  @Default_OwnerAccountID BIGINT
AS

DECLARE @TimeZoneIDString VARCHAR(100)
DECLARE @FromDate DATETIME
DECLARE @ToDate DATETIME

DECLARE @Results TABLE (
  SensorID                BIGINT,
  GatewayID               BIGINT,
  Type                    VARCHAR(30),
  CustomerID              BIGINT,
  CustomerName            VARCHAR(100),
  Date                    DATETIME,
  SentTo                  VARCHAR(100),
  Method                  VARCHAR(50),
  NotificationTriggeredID BIGINT
);


SET @ToDate = GETUTCDATE()
SET @FromDate = DATEADD(HOUR, -1, DATEADD(DAY, -7, @ToDate));


SELECT
  @TimeZoneIDString = t.TimeZoneIDString
FROM dbo.[Account] a WITH (NOLOCK)
INNER JOIN dbo.[TimeZone] t WITH (NOLOCK) on a.TimeZoneID = t.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID;

--SET @FromDate = DATEADD(MINUTE, DATEDIFF(MINUTE, dbo.GetLocalTime(@FromDate, @TimeZoneIDSTring), @FromDate) ,@FromDate)
--SET @ToDate   = DATEADD(MINUTE, DATEDIFF(MINUTE, dbo.GetLocalTime(@ToDate, @TimeZoneIDSTring), @ToDate) ,@ToDate)
 
    INSERT INTO @Results
    (
      SensorID,
      Type,
      CustomerID,
      CustomerName,
      Date,
      SentTo,
      Method,
      NotificationTriggeredID
    )
    SELECT
      nr.SensorID,
      Type                          = 'Alert Sent', 
      nr.CustomerID, 
      Name                          = c.FirstName + ' ' + c.LastName, 
      Date                          = dbo.GetLocalTime(NotificationDate, @TimeZoneIDString), 
      SentTo, 
      Method                        = t.Value,
      nr.NotificationTriggeredID
    FROM dbo.[NotificationRecorded] nr WITH (NOLOCK)
    LEFT JOIN dbo.[Customer] c        WITH (NOLOCK) ON nr.CustomerID = c.CustomerID
    INNER JOIN dbo.[TypeLookup] t      WITH (NOLOCK) ON nr.eNotificationType = t.TypeID AND t.TableName = 'NotificationRecorded' AND t.ColumnName = 'eNotificationType'
    INNER JOIN dbo.[Notification] n    WITH (NOLOCK) ON nr.notificationid = n.notificationid
    WHERE NotificationDate BETWEEN @FromDate and @ToDate
      AND n.AccountID = @Default_OwnerAccountID
      and n.SensorID IS NOT NULL;

    SELECT 
      t.SensorID,
      s.SensorName,
      m.ApplicationName,
      Type,
      CustomerID,
      CustomerName,
      SentTo,
      Method,
      Date,
      NotificationTriggeredID
    FROM (SELECT 
            SensorID,
            Type,
            CustomerID,
            CustomerName,
            Date,
            SentTo,
            Method,
            NotificationTriggeredID
          FROM @Results
          UNION ALL
          SELECT DISTINCT
            r.SensorID,
            Type        = 'Alert Acknowledged', 
            CustomerID  = nt.AcknowledgedBy,  
            Name        = CASE WHEN nt.AcknowledgeMethod = 'Acknowledge_Action' AND nt.AcknowledgedBy IS NULL THEN 'System' ELSE c.FirstName + ' ' + c.LastName END, 
            Date        = dbo.GetLocalTime(AcknowledgedTime, @TimeZoneIDString), 
            SentTo      = NULL, 
            Method      = nt.AcknowledgeMethod,
            nt.NotificationTriggeredID
          FROM dbo.[NotificationTriggered] nt WITH (NOLOCK)
          INNER JOIN @Results r                             ON nt.NotificationTriggeredID = r.NotificationTriggeredID
          LEFT JOIN dbo.[Customer] c          WITH (NOLOCK) ON nt.AcknowledgedBy = c.CustomerID
          WHERE nt.AcknowledgedTime IS NOT NULL
          ) t 
    INNER JOIN dbo.Sensor s with (NOLOCK) on t.SensorID = s.SensorID
    INNER JOIN dbo.Application m with (NOLOCK) on m.ApplicationID = s.ApplicationID
    ORDER BY Date;




GO


