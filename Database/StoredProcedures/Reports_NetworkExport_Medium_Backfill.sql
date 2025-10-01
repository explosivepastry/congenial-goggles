USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_NetworkExport_Medium_Backfill]    Script Date: 7/1/2024 8:46:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_NetworkExport_Medium_Backfill]

AS
declare  @Default_OwnerAccountID INT = 38522
declare  @Default_ReportScheduleID INT = 66294
declare  @NetworkID INT = 64257

/* ----| Network : Data Export (Daily/Weekly/Monthly) Report |---- */

/*
----| Maintenance Log |----
Created 2020-07-10 NLN

Cloned from regular network export for VRL labs
if the sensor count is over 30.
*/

/*Required AutoVariables*/
/*Required AutoVariables*/
--DECLARE @Default_OwnerAccountID INT = 6082
--DECLARE @Hours INT = 1
--DECLARE @Default_ReportScheduleID INT = 2664
--DECLARE @SensorID INT = 1
--DECLARE @Col_Date INT = 1
--DECLARE @Col_GatewayID INT = 1
--DECLARE @Col_Sensor INT = 1
--DECLARE @Col_Sensor_State INT = 1
--DECLARE @Col_Data INT = 1
--DECLARE @Col_Plot_Value INT = 1
--DECLARE @Col_Text INT = 0
--DECLARE @Col_Alert INT = 1
--DECLARE @Col_Signal_Strength INT = 1
--DECLARE @Col_Voltage INT = 1
--DECLARE @Col_Battery INT = 1
--DECLARE @Col_Special INT = 1
--DECLARE @NetworkID INT = 6468


/* ----| Sql Script |---- */
DECLARE @FromDate DATETIME = '2024-03-04'
DECLARE @ToDate DATETIME = '2024-04-01'
DECLARE @OffsetMinutes INT
DECLARE @TimezoneIdentifier VARCHAR(255)
DECLARE @SQL VARCHAR(MAX);

/**************************************************

  SET From/ToDate based on account time zone
          and ScheduleType

**************************************************/
SELECT 
  @TimezoneIdentifier = tz.TimeZoneIDString 
FROM dbo.[TimeZone] tz WITH (NOLOCK)
INNER JOIN dbo.[Account] a WITH (NOLOCK) ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID

set @FromDate = dbo.GetLocalTime(@FromDate, @TimezoneIdentifier)
set @ToDate = dbo.GetLocalTime(@ToDate, @TimezoneIdentifier)

--SET @OffsetMinutes  = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));
--SET @ToDate         = Convert(datetime,Convert(varchar,DatePart(Year,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))) +'-'+ Convert(varchar,DatePart(month ,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate())))+'-'+ Convert(varchar,DatePart(Day,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))));


--SELECT @FromDate =
--      CASE 
--         WHEN ScheduleType = 3 THEN DATEADD(DAY,-1,@ToDate)
--         WHEN ScheduleType = 2 THEN DATEADD(WEEK,-1,@ToDate)
--         WHEN ScheduleType = 1 THEN DATEADD(MONTH,-1,@ToDate)
--         WHEN ScheduleType = 0 THEN DATEADD(YEAR,-1,@ToDate)
--         ELSE DATEADD(DAY,-1,@ToDate)
--      END
--   FROM dbo.[ReportSchedule] WITH (NOLOCK)
--   WHERE ReportScheduleID = @Default_ReportScheduleID


/*****************************************************

        Used for DLL formatting

*****************************************************/
-- TABLE 0 TYPE REPORT TO RUN --
SELECT 'Network' AS 'Type_Of_Sensor_Report';

-- TABLE 1 BASIC INFORMATION NEEDED TO RUN REPORT --
SELECT @FromDate AS 'From'
 ,@ToDate AS 'To'
 ,@TimezoneIdentifier AS 'Time_Zone_StringID'
 ,@Default_OwnerAccountID AS 'AccountID'
 ,CSNetID AS 'CSNetID'
FROM CSNet WITH (NOLOCK) WHERE CSNetID = @NetworkID;


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



-- TABLE 3 DATA FOR REPORT --
DECLARE @SensorCount INT = 0
SELECT @SensorCount = COUNT(SensorID) FROM dbo.[Sensor] WITH (NOLOCK) WHERE CSNetID = @NetworkID;

SELECT
SensorID
INTO #SensorList
FROM dbo.[Sensor] WITH (NOLOCK)
WHERE CSNetID = @NetworkID

IF @SensorCount > 75
BEGIN

	  SELECT
	  0 'DataMessageGUID',
	  0 'SensorID',
	  GETUTCDATE() 'MessageDate',
	  0 'State',
	  0 'SignalStrength',
	  0 'LinkQuality',
	  0 'Battery',
	  'Too many sensors on this network - limit 75' 'Data',
	  0 'Voltage',
	  0 'MeetsNotificationRequirement',
	  0 'InsertDate',
	  0 'GatewayID',
	  0 'HasNote',
    0 'IsAuthenticated', 
    0 'ApplicationID'

END ELSE
BEGIN

  SET @SQL =
  'SELECT 
    DataMessageGUID, 
    dm.SensorID,  
    dm.MessageDate ''MessageDate'',
    State, 
    SignalStrength, 
    LinkQuality, 
    Battery, 
    Data, 
    Voltage, 
    MeetsNotificationRequirement, 
    InsertDate, 
    GatewayID, 
    dm.HasNote,
    dm.IsAuthenticated,
    dm.ApplicationID
	FROM dbo.[DataMessage] dm WITH (NOLOCK)
	INNER JOIN #SensorList s on dm.SensorID = s.SensorID
  WHERE [MessageDate] > '''+ CONVERT(VARCHAR(30), @FromDate, 120) + '''
	  AND [MessageDate] < ''' + CONVERT(VARCHAR(30), @ToDate,120) + '''
	ORDER BY s.SensorID, [MessageDate]
  OPTION (OPTIMIZE FOR UNKNOWN)';

  EXEC (@SQL);

END

GO


