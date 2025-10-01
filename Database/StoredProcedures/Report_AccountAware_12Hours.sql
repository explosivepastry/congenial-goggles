USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_AccountAware_12Hours]    Script Date: 6/24/2024 2:31:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_AccountAware_12Hours]
  @Default_OwnerAccountID INT 
AS


DECLARE @SQL VARCHAR(3000)
DECLARE @OffsetMinutes INT
DECLARE @TimezoneIdentifier VARCHAR(255)
DECLARE @FromDate DATETIME = DATEADD(HOUR, -12, GETUTCDATE())
DECLARE @ToDate DATETIME = GETUTCDATE()


SELECT 
  @TimezoneIdentifier = tz.TimeZoneIDString 
FROM dbo.[TimeZone] tz WITH (NOLOCK)
INNER JOIN dbo.[Account] a WITH (NOLOCK) ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID

-- TABLE 0 TYPE REPORT TO RUN --
SELECT 'Account' AS 'Type_Of_Sensor_Report';

-- TABLE 1 BASIC INFORMATION NEEDED TO RUN REPORT --
SELECT @FromDate AS 'From'
 ,@ToDate AS 'To'
 ,@TimezoneIdentifier AS 'Time_Zone_StringID'
 ,@Default_OwnerAccountID AS 'AccountID'


-- TABLE 2 COLUMN NAMES FOR REPORT --
SELECT 'True' AS 'DataMessageGUID'
 ,CONVERT(BIT, 1) AS 'Date'
 ,CONVERT(BIT, 1) AS 'GatewayID'
 ,'True' AS 'SensorID'
 ,CONVERT(BIT, 1) AS 'Sensor_Name'
 ,CONVERT(BIT, 1) AS 'Sensor_State'
 ,CONVERT(BIT, 1) AS 'Raw_Data'
 ,CONVERT(BIT, 1) AS 'Value'
 ,CONVERT(BIT, 1) AS 'Formatted_Value'
 ,CONVERT(BIT, 1) AS 'Alert_Sent'
 ,CONVERT(BIT, 1) AS 'Signal_Strength'
 ,CONVERT(BIT, 1) AS 'Voltage'
 ,CONVERT(BIT, 1) AS 'Battery'
 ,CONVERT(BIT, 1) AS 'Special';

SELECT SensorID into #SensorList FROM Sensor WHERE AccountID = @Default_OwnerAccountID

SET @SQL =

'SELECT
  d.*
from #SensorList s
INNER JOIN DataMessage d on s.sensorid = d.sensorid
WHERE d.MessageDate >= ''' + CONVERT(VARCHAR(30), @FromDate , 120) + '''
  AND d.MessageDate <= ''' + CONVERT(VARCHAR(30), @ToDate   , 120) + '''
  AND d.state & 02 = 2;'

EXEC (@SQL)

GO


