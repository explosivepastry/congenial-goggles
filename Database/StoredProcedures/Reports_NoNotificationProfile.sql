USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_NoNotificationProfile]    Script Date: 7/1/2024 8:48:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_NoNotificationProfile]
 @Default_OwnerAccountID BIGINT
AS
/*-------[Sensor: No Notification Profile]-------*/
/*

The purpose of this script is to return a list of sensors (whether active or inactive) that don't
have notifications attached to them. 

If an inactive notification is attached to the sensor, that sensor
still will not be returned. 

*/
/*
                CHANGE HISTORY
------------------------------------------------
Created By    Created ON    Descritption
Nathan Novak  10/17/2016    Created Script
*/

----DefaultVariables
--DECLARE @Default_OwnerAccountID BIGINT
--SET @Default_OwnerAccountID = 1

IF (OBJECT_ID('tempdb..#Accounts') IS NOT NULL)
BEGIN
                DROP TABLE #Accounts;
END

IF (OBJECT_ID('tempdb..#SensorTemp') IS NOT NULL)
BEGIN
                DROP TABLE #SensorTemp;
END

CREATE TABLE #Accounts
(
  [AccountID] BIGINT,
  [CompanyName] VARCHAR(255)
)

CREATE TABLE #SensorTemp
(
  [AccountID] BIGINT,
  [CompanyName] VARCHAR(255),
  [SensorID] BIGINT,
  [SensorName] NVARCHAR(255),
  [ActiveSensor] BIT
)

/*
  Get this accounts and accounts under it
*/
INSERT INTO #Accounts ([AccountID], [CompanyName])
SELECT
  [AccountID],
  [CompanyName]
FROM dbo.Account WITH (NOLOCK)
WHERE [AccountIDTree] LIKE '%*' + CONVERT(NVARCHAR(15),@Default_OwnerAccountID) +'*%'


/*
  Get all sensors under the accounts that haven't been put into a deleted state
*/
INSERT INTO #SensorTemp
(
  [AccountID],
  [CompanyName],
  [SensorID],
  [SensorName],
  [ActiveSensor]
)
SELECT
  a.[AccountID],
  a.[CompanyName],
  s.[SensorID],
  s.[SensorName],
  s.[IsActive]
FROM dbo.Sensor s WITH (NOLOCK)
INNER JOIN #Accounts a ON s.AccountID = a.AccountID
WHERE ISNULL(s.IsDeleted, 0) = 0


/*********************************************************************
                              RESULT SET
----------------------------------------------------------------------
  A list of CompanyName and SensorID/SensorName where SensorID is not attached
  to a SensorNotification or if it is, the Notification was deleted

  Distinct required if sensor is attached to two sensor notifications
  that have both been deleted.

  [AccountID]   |   [SensorID]    |   [ActiveSensor]
**********************************************************************/

SELECT DISTINCT
  s.AccountID,
  CompanyName   = ISNULL(s.CompanyName, ''),
  s.SensorID,
  s.SensorName,
  s.ActiveSensor
FROM #SensorTemp s
WHERE s.SensorID NOT IN (
SELECT
  s.SensorID
FROM #SensorTemp s
LEFT JOIN dbo.SensorNotification sn WITH (NOLOCK) ON s.SensorID = sn.SensorID
LEFT JOIN dbo.Notification n        WITH (NOLOCK) ON sn.NotificationID = n.NotificationID
WHERE sn.NotificationID IS NOT NULL
  AND ISNULL(n.IsDeleted, 0) = 0 )
ORDER BY CompanyName, s.SensorID

GO


