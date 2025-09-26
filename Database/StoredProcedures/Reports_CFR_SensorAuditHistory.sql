USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_CFR_SensorAuditHistory]    Script Date: 6/24/2024 2:49:18 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_CFR_SensorAuditHistory]
  @Default_OwnerAccountID BIGINT,
  @SensorID BIGINT
AS

DECLARE @AccountID BIGINT
DECLARE @TimeZoneIDSTring VARCHAR(200)

SET @AccountID = (SELECT AccountID From dbo.[Sensor] WITH (NOLOCK) WHERE SensorID = @SensorID);

SELECT
  @TimeZoneIDString = t.TimeZoneIDString
FROM dbo.[Account] a WITH (NOLOCK)
INNER JOIN dbo.[TimeZone] t WITH (NOLOCK) on a.TimeZoneID = t.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID;

IF @AccountID = @Default_OwnerAccountID
BEGIN

    WITH CTE_SensorEdit as
    (
      SELECT
        RowID         = Row_Number() OVER (PARTITION BY ObjectID ORDER BY TimeStamp), 
        a.customerid, 
        Name          = c.FirstName + ' ' + c.LastName,
        TimeStamp     = dbo.GetLocalTime(a.TimeStamp, @TimeZoneIDString), 
        SensorID      = a.ObjectID, 
        AuditAction   = t.Value, 
        Record, 
        ActionDescription
      FROM dbo.[AuditLog] a WITH (NOLOCK)
      INNER JOIN dbo.[Customer] c WITH (NOLOCK) ON a.CustomerID = c.CustomerID
      INNER JOIN dbo.[TypeLookup] t WITH (NOLOCK) ON a.AuditAction = t.TypeID AND t.TableName = 'AuditLog' AND t.ColumnName = 'AuditAction'
      WHERE AuditObject = 1
        AND ObjectID    = @SensorID
    )
    SELECT
      c.CustomerID,
      c.Name,
      c.TimeStamp,
      c.SensorID,
      c.AuditAction,
      PreviousValue     = c2.Record,
      ChangeToValue     = c.Record,
      c.ActionDescription
    FROM CTE_SensorEdit c
    LEFT JOIN CTE_SensorEdit c2 on c.rowID = c2.RowID+1
    ORDER BY TimeStamp DESC

END ELSE
BEGIN

     SELECT
      CustomerID    = NULL,
      TimeStamp     = NULL,
      SensorID      = @SensorID,
      AuditAction   = NULL,
      PreviousValue = NULL,
      ChangeToValue = NULL,
      ActionDescription = 'No Audit Data - Sensor does not belong to account.'

END
GO


