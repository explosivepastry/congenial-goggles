USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_Sensor24HourBeta]    Script Date: 7/1/2024 8:57:49 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_Sensor24HourBeta]
  @Default_OwnerAccountID INT,
  @Hours INT,
  @Col_Date bit,
  @SensorID int,
  @Col_GatewayID bit,
  @Col_Sensor bit,
  @Col_Sensor_State bit,
  @Col_Data bit,
  @Col_Plot_Value bit,
  @Col_Text bit,
  @Col_Alert bit,
  @Col_Signal_Strength bit,
  @Col_Voltage bit,
  @Col_Battery bit,
  @Col_Special bit
AS
/* ----| Sensor 24 Hour (Beta) Report |---- */

/*
----| Maintenance Log |----
Created 2015-02-20 Brandon
Modified 2016-07-05 Ala
*/

/*Required AutoVariables*/
--DECLARE @Default_OwnerAccountID INT = 6905
--DECLARE @Hours INT = 1
--DECLARE @Col_Date INT = 1
--DECLARE @SensorID INT = 84352
--DECLARE @Col_GatewayID INT = 1
--DECLARE @Col_Sensor INT = 1
--DECLARE @Col_Sensor_State INT = 1
--DECLARE @Col_Data INT = 1
--DECLARE @Col_Plot_Value INT = 1
--DECLARE @Col_Text INT = 1
--DECLARE @Col_Alert INT = 1
--DECLARE @Col_Signal_Strength INT = 1
--DECLARE @Col_Voltage INT = 1
--DECLARE @Col_Battery INT = 1
--DECLARE @Col_Special INT = 1


/* ----| Sql Script |---- */
DECLARE @OffsetMinutes INT, @TimezoneIdentifier VARCHAR(255), @TempDate DATETIME

SELECT @TimezoneIdentifier = tz.TimeZoneIDString 
FROM TimeZone tz
INNER JOIN Account a ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID

SET @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));
SET @TempDate = Convert(datetime,Convert(varchar,DatePart(Year,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))) +'-'+ Convert(varchar,DatePart(month ,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate())))+'-'+ Convert(varchar,DatePart(Day,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))));
SET @TempDate = DATEADD(MINUTE,-@OffsetMinutes, DATEADD(HOUR,@Hours,@TempDate));

-- Table 0 type report to run --
SELECT 'Sensor' AS 'Type_Of_Sensor_Report';

-- Table 1 Basic information needed to run report --
SELECT DATEADD(DAY,-1,@TempDate) AS 'From'
 ,@TempDate AS 'To'
 ,@TimezoneIdentifier AS 'Time_Zone_StringID'
 ,SensorID AS 'SensorID'
 ,@Default_OwnerAccountID AS 'AccountID'
 ,CSNetID AS 'CSNetID'
FROM Sensor WHERE SensorID = @SensorID;

-- Table 2 Column Names for Report --
SELECT 'True' AS 'DataMessageGUID'
 ,@Col_Date AS 'Date'
 ,@Col_GatewayID AS 'GatewayID'
 ,'True' AS 'SensorID'
 ,@Col_Sensor AS 'Sensor_Name'
 ,@Col_Sensor_State AS 'Sensor_State'
 ,@Col_Data AS 'Raw_Data'
 ,@Col_Plot_Value AS 'Value'
 ,@Col_Text AS 'Formatted_Value'
 ,@Col_Alert AS 'Alert_Sent'
 ,@Col_Signal_Strength AS 'Signal_Strength'
 ,@Col_Voltage AS 'Voltage'
 ,@Col_Battery AS 'Battery'
 ,@Col_Special AS 'Special';

-- Table 3 Data for Report --
SELECT dm.* 
FROM DataMessage dm
WHERE dm.SensorID = @SensorID
AND dm.MessageDate > DATEADD(DAY,-1,@TempDate)
AND dm.MessageDate < @TempDate
ORDER BY dm.MessageDate;
GO


