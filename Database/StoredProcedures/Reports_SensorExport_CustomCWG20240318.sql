USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_SensorExport_CustomCWG20240318]    Script Date: 7/1/2024 8:59:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Report_SensorExport_CustomCWG20240318]
  @Default_OwnerAccountID INT,
  @Hours INT,
  @Default_ReportScheduleID INT,
  @SensorID INT,
  @Col_Date bit,
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
/* ----| Sensor : Data Export (Daily/Weekly/Monthly) Report |---- */

/*
----| Maintenance Log |----
Created 2016-06-27 Ala
Modified 2016-07-05 Ala
Modified 2016-07-13 Ala
*/

/*Required AutoVariables*/
--DECLARE @Default_OwnerAccountID INT = 1
--DECLARE @Hours INT = 0
--DECLARE @Default_ReportScheduleID INT = 1
--DECLARE @SensorID INT = 101
--DECLARE @Col_Date INT = 1
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
DECLARE @FromDate DATETIME
DECLARE @ToDate DATETIME
DECLARE @OffsetMinutes INT, @TimezoneIdentifier VARCHAR(255), @TempDate DATETIME

SELECT @TimezoneIdentifier = tz.TimeZoneIDString 
FROM TimeZone tz
INNER JOIN Account a ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID

SET @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));
SET @ToDate = Convert(datetime,Convert(varchar,DatePart(Year,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))) +'-'+ Convert(varchar,DatePart(month ,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate())))+'-'+ Convert(varchar,DatePart(Day,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))));
SET @ToDate = DATEADD(MINUTE,-@OffsetMinutes, DATEADD(HOUR,@Hours,@ToDate));

SET @ToDate='2024-03-10'--Craig Added this to get to custom week of 3/3-3/10

SELECT @FromDate =
      CASE 
         WHEN ScheduleType = 3 THEN DATEADD(DAY,-1,@ToDate)
         WHEN ScheduleType = 2 THEN DATEADD(WEEK,-1,@ToDate)
         WHEN ScheduleType = 1 THEN DATEADD(MONTH,-1,@ToDate)
         WHEN ScheduleType = 0 THEN DATEADD(YEAR,-1,@ToDate)
         ELSE DATEADD(DAY,-1,@ToDate)
      END
   FROM ReportSchedule 
   WHERE ReportScheduleID = @Default_ReportScheduleID

SET @FromDate='2024-03-03'--Craig Added this to get to custom week of 3/3-3/10

-- Table 0 type report to run --
SELECT 'Sensor' AS 'Type_Of_Sensor_Report';

-- Table 1 Basic information needed to run report --
SELECT @FromDate AS 'From'
 ,@ToDate AS 'To'
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

 IF @Default_OwnerAccountID = (SELECT AccountID FROM dbo.[Sensor] WITH (NOLOCK) WHERE SensorID = @SensorID)
 BEGIN

-- Table 3 Data for Report --
SELECT dm.MessageDate 'MessageDate', dm.SensorID, dm.DataMessageGUID, dm.State, dm.SignalStrength, dm.LinkQuality, dm.Battery
,dm.Data ,dm.Voltage, dm.MeetsNotificationRequirement, dm.InsertDate, dm.GatewayID, dm.HasNote, dm.IsAuthenticated, dm.ApplicationID
FROM  DataMessage dm
WHERE dm.SensorID= @SensorID
AND [MessageDate] > @FromDate
AND [MessageDate] < @ToDate
ORDER BY dm.MessageDate
OPTION (Optimize For Unknown);

END ELSE
BEGIN

  -- Table 3 Data for Report --
  SELECT TOP 0
  MessageDate = NULL, 
  SensorID = NULL, 
  DataMessageGUID= NULL, 
  State= NULL,
  SignalStrength= NULL, 
  LinkQuality= NULL, 
  Battery= NULL,
  Data= NULL,
  Voltage= NULL,
  MeetsNotificationRequirement= NULL, 
  InsertDate= NULL, 
  GatewayID= NULL, 
  HasNote= NULL, 
  IsAuthenticated= NULL,
  ApplicationID = null

END
GO


