USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_SensorExportMonnitUseOnly]    Script Date: 7/1/2024 9:02:32 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_SensorExportMonnitUseOnly]
  @AccountID  BIGINT, 
  @SensorID   BIGINT,
  @FromDate   DATETIME,
  @ToDate     DATETIME,
  @Hours      INT
AS

SET NOCOUNT ON;

/* ----| Sensor Export Builder (Daily, Monthly, Weekly) |---- */

/*
----| Maintenance Log |----
Created 2016-07-13 Ala
Modified 2017-03-10 NLN (turned into store proc)
*/

DECLARE @OffsetMinutes INT, @TimezoneIdentifier VARCHAR(255), @TempDate DATETIME

SELECT @TimezoneIdentifier = tz.TimeZoneIDString 
FROM TimeZone tz
INNER JOIN Account a ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @AccountID

SET @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));
SET @ToDate = DATEADD(MINUTE,-@OffsetMinutes, DATEADD(HOUR,@Hours,@ToDate));
SET @FromDate = DATEADD(MINUTE,-@OffsetMinutes, DATEADD(HOUR,@Hours,@FromDate));

-- Table 0 type report to run --
SELECT 'Sensor' AS 'Type_Of_Sensor_Report';

-- Table 1 Basic information needed to run report --
SELECT @FromDate AS 'From'
 ,@ToDate AS 'To'
 ,@TimezoneIdentifier AS 'Time_Zone_StringID'
 ,SensorID AS 'SensorID'
 ,@AccountID AS 'AccountID'
 ,CSNetID AS 'CSNetID'
FROM Sensor WHERE SensorID = @SensorID;

-- Table 2 Column Names for Report --
SELECT 'True' AS 'DataMessageGUID'
 ,'True' AS 'Date'
 ,'False' AS 'GatewayID'
 ,'True' AS 'SensorID'
 ,'True' AS 'Sensor_Name'
 ,'True' AS 'Sensor_State'
 ,'False' AS 'Raw_Data'
 ,'True' AS 'Value'
 ,'True' AS 'Formatted_Value'
 ,'False' AS 'Alert_Sent'
 ,'False' AS 'Signal_Strength'
 ,'False' AS 'Voltage'
 ,'True' AS 'Battery'
 ,'False' AS 'Special';

-- Table 3 Data for Report --
SELECT  
  dm.MessageDate ,
  dm.SensorID ,
  dm.DataMessageGUID ,
  dm.State ,
  dm.SignalStrength ,
  dm.LinkQuality ,
  dm.Battery ,
  dm.Data ,
  dm.Voltage ,
  dm.MeetsNotificationRequirement ,
  dm.InsertDate ,
  dm.GatewayID ,
  dm.HasNote,
  dm.IsAuthenticated,
  dm.ApplicationID
FROM  dbo.DataMessage dm WITH (NOLOCK)
WHERE dm.SensorID= @SensorID
AND [MessageDate] > @FromDate
AND [MessageDate] < @ToDate
ORDER BY dm.MessageDate;

GO


