USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_CFR_AlertsSentAcknowledged]    Script Date: 6/24/2024 2:48:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_CFR_AlertsSentAcknowledged]
  @SensorID BIGINT,
  @FromDate DATETIME,
  @ToDate DATETIME,
  @Default_OwnerAccountID BIGINT
AS

DECLARE @TimeZoneIDString VARCHAR(100);

DECLARE @Results TABLE (
  SensorID                BIGINT,
  Type                    VARCHAR(30),
  NotificationTriggeredID BIGINT,
  Date                    DATETIME,
  CustomerID              BIGINT,
  CustomerName            VARCHAR(100),
  SentTo                  VARCHAR(100),
  Method                  VARCHAR(50)
);

IF DATEDIFF(DAY, @FromDate, @ToDate) > 7
BEGIN

    SET @ToDate = DATEADD(DAY, 7, @FromDate);

END

SELECT
  @TimeZoneIDString = t.TimeZoneIDString
FROM dbo.[Account] a WITH (NOLOCK)
INNER JOIN dbo.[TimeZone] t WITH (NOLOCK) on a.TimeZoneID = t.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID;

SET @FromDate = DATEADD(MINUTE, DATEDIFF(MINUTE, dbo.GetLocalTime(@FromDate, @TimeZoneIDSTring), @FromDate) ,@FromDate)
SET @ToDate = DATEADD(MINUTE, DATEDIFF(MINUTE, dbo.GetLocalTime(@ToDate, @TimeZoneIDSTring), @ToDate) ,@ToDate)
 
IF (SELECT AccountID FROM dbo.[Sensor] WHERE SensorID = @SensorID) = @Default_OwnerAccountID
BEGIN

    INSERT INTO @Results
    (
        SensorID,
        Type,
        NotificationTriggeredID,
        Date,
        CustomerID,
        CustomerName,
        SentTo,
        Method
    )
    SELECT
      nr.SensorID,
      Type                          = 'Alert Sent', 
      nr.NotificationTriggeredID, 
      Date                          = dbo.GetLocalTime(NotificationDate, @TimeZoneIDString), 
      nr.CustomerID, 
      Name                          = c.FirstName + ' ' + c.LastName, 
      SentTo, 
      Method                        = t.Value
    FROM dbo.[NotificationRecorded] nr WITH (NOLOCK)
    INNER JOIN dbo.[Customer] c        WITH (NOLOCK) ON nr.CustomerID = c.CustomerID
    INNER JOIN dbo.[TypeLookup] t      WITH (NOLOCK) ON nr.eNotificationType = t.TypeID AND t.TableName = 'NotificationRecorded' AND t.ColumnName = 'eNotificationType'
    INNER JOIN dbo.[Notification] n    WITH (NOLOCK) ON nr.notificationid = n.notificationid
    WHERE nr.SensorID = @SensorID
      AND NotificationDate BETWEEN @FromDate and @ToDate
      AND n.AccountID = @Default_OwnerAccountID;
      
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

END ELSE
BEGIN

SELECT 'SensorID does not belong to Account'

END
GO


