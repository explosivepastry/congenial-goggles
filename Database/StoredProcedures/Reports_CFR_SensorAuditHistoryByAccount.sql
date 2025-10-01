USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_CFR_SensorAuditHistoryByAccount]    Script Date: 6/24/2024 2:50:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_CFR_SensorAuditHistoryByAccount]
  @Default_OwnerAccountID BIGINT
AS

DECLARE @TimeZoneIDSTring VARCHAR(200)


SELECT
  @TimeZoneIDString = t.TimeZoneIDString
FROM dbo.[Account] a WITH (NOLOCK)
INNER JOIN dbo.[TimeZone] t WITH (NOLOCK) on a.TimeZoneID = t.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID;


    WITH CTE_SensorEdit as
    (
      SELECT
      a.accountid,
        RowID         = Row_Number() OVER (PARTITION BY ObjectID ORDER BY TimeStamp), 
        a.customerid, 
        Name          = c.FirstName + ' ' + c.LastName,
        TimeStamp ,
        SensorID      = a.ObjectID, 
        AuditAction   = t.Value, 
        Record, 
        ActionDescription
      FROM dbo.[AuditLog] a WITH (NOLOCK)
      INNER JOIN dbo.[Customer] c WITH (NOLOCK) ON a.CustomerID = c.CustomerID
      INNER JOIN dbo.[TypeLookup] t WITH (NOLOCK) ON a.AuditAction = t.TypeID AND t.TableName = 'AuditLog' AND t.ColumnName = 'AuditAction'
      INNER JOIN dbo.Sensor s WITH (NOLOCK) on s.AccountID = a.AccountID and s.SensorID = a.ObjectID
      WHERE AuditObject = 1
        and a.Accountid = @Default_OwnerAccountID
        and Record not like '%"DeviceID%'
    )
    SELECT
      c.SensorID,
      c.TimeStamp,
      c.CustomerID,
      c.Name,
      c.AuditAction,
      c.ActionDescription,
      PreviousValue     = c2.Record,
      ChangeToValue     = c.Record
    FROM CTE_SensorEdit c
    LEFT JOIN CTE_SensorEdit c2 on c.rowID = c2.RowID+1 and c.SensorID = c2.SensorID
    ORDER BY sensorid, TimeStamp DESC

GO


