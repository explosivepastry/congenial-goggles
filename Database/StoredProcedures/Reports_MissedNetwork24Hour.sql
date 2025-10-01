USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_MissedNetwork24Hour]    Script Date: 7/1/2024 8:33:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Report_MissedNetwork24Hour]
  @AcctID BIGINT,
  @CSNetID BIGINT,
  @FromDate DATETIME,
  @ToDate DATETIME,
  @Hours INT,
  @Col_Sensor BIT,
  @Col_Date BIT,
  @Col_Plot_Value BIT,
  @Col_Text BIT,
  @Col_Battery BIT,
  @Col_Data BIT,
  @Col_Sensor_State BIT,
  @Col_GatewayID BIT,
  @Col_Alert BIT,
  @Col_Signal_Strength BIT,
  @Col_Voltage BIT,
  @Col_Special BIT
AS

SET NOCOUNT ON;

/* ----| Missed Network 24 Hour (Beta) Report |---- */

/*
----| Maintenance Log |----
Created 2016-07-05 Ala
*/

/*Required AutoVariables*/
--DECLARE @SensorID INT = 0 --//Auto value except for when testing
--DECLARE @Default_OwnerAccountID INT = 4293 --//Auto value except for when testing

--DECLARE @AcctID INT = 4293
--DECLARE @CSNetID INT = 7808
--DECLARE @Hours INT = 1
--DECLARE @Col_Sensor INT = 1
--DECLARE @Col_Date INT = 1
--DECLARE @Col_Plot_Value INT = 1
--DECLARE @Col_Text INT = 1
--DECLARE @Col_Battery INT = 1

--DECLARE @Col_Data INT = 0
--DECLARE @Col_Sensor_State INT = 0
--DECLARE @Col_GatewayID INT = 0
--DECLARE @Col_Alert INT = 0
--DECLARE @Col_Signal_Strength INT = 0
--DECLARE @Col_Voltage INT = 0
--DECLARE @Col_Special INT = 0

--DECLARE @FromDate DATETIME = '11/27/2016 06:00:00'
--DECLARE @ToDate	  DATETIME = '11/28/2016 06:00:00'



/* ----| Sql Script |---- */
DECLARE @OffsetMinutes INT, @TimezoneIdentifier VARCHAR(255), @TempDate DATETIME

SELECT @TimezoneIdentifier = tz.TimeZoneIDString 
FROM TimeZone tz
INNER JOIN Account a ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @AcctID

SET @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));
SET @FromDate = DATEADD(MINUTE, -@OffsetMinutes, DATEADD(HOUR, @Hours,@FromDate));
SET @ToDate = DATEADD(MINUTE, -@OffsetMinutes, DATEADD(HOUR, @Hours,@ToDate));

-- Table 0 type report to run --
SELECT 'Network' AS 'Type_Of_Sensor_Report';

-- Table 1 Basic information needed to run report --
SELECT @FromDate AS 'From'
 ,@ToDate AS 'To'
 ,@TimezoneIdentifier AS 'Time_Zone_StringID'
 ,Null AS 'SesnorID'
 ,@AcctID AS 'AccountID'
 ,@CSNetID AS 'CSNetID';

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
dm.HasNote ,
dm.IsAuthenticated,
dm.ApplicationID
FROM  DataMessage dm WITH (NOLOCK)
INNER JOIN Sensor s ON s.SensorID = dm.SensorID
WHERE s.CSNetID = @CSNetID
AND dm.MessageDate > @FromDate
AND dm.MessageDate < @ToDate
ORDER BY s.SensorID, dm.MessageDate;
GO


