USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_NetworkExport_Backfill]    Script Date: 7/1/2024 8:43:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--exec [Report_NetworkExport_Backfill]
CREATE PROCEDURE [dbo].[Report_NetworkExport_Backfill]
	--@Default_OwnerAccountID INT,
	--@Default_ReportScheduleID INT
AS

DECLARE @Default_OwnerAccountID INT  = 14426
DECLARE @Default_ReportScheduleID INT  = 6659
DECLARE @Col_Alert bit = 0
DECLARE @Col_Battery bit = 0
DECLARE @Col_Data bit  = 0
DECLARE @Col_Date bit = 1
DECLARE @Col_GatewayID bit  = 0
DECLARE @Col_Plot_Value bit  = 1
DECLARE @Col_Sensor bit  = 1
DECLARE @Col_Sensor_State bit  = 0
DECLARE @Col_Signal_Strength bit = 0
DECLARE @Col_Special bit = 0
DECLARE @Col_Text bit = 0
DECLARE @Col_Voltage bit = 0
DECLARE @Hours INT = 0
DECLARE @NetworkID INT = 23957


/* ----| Network : Data Export (Daily/Weekly/Monthly) Report |---- */

/*
----| Maintenance Log |----
Created 2016-06-14 Ala
Modified 2016-07-05 Ala
Modified 2016-08-15 Brandon //Allow all accounts to use this report
Modified 2016-09-12 Nathan //added HasNotes to return set
Modified 2016-12-02 Nathan // added nolock and dynamic querying in DM results
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
DECLARE @FromDate DATETIME
DECLARE @ToDate DATETIME
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

SET @ToDate = '2024-04-11'

SET @OffsetMinutes  = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));
SET @ToDate         = Convert(datetime,Convert(varchar,DatePart(Year,DATEADD(MINUTE, @OffSetMinutes, @ToDate))) +'-'+ Convert(varchar,DatePart(month ,DATEADD(MINUTE, @OffSetMinutes, @ToDate)))+'-'+ Convert(varchar,DatePart(Day,DATEADD(MINUTE, @OffSetMinutes, @ToDate))));
SET @ToDate         = DATEADD(MINUTE,-@OffsetMinutes, DATEADD(HOUR,@Hours,@ToDate));

SELECT @FromDate = DATEADD(DAY,-1,@ToDate)
   --   CASE 
   --      WHEN ScheduleType = 3 THEN DATEADD(DAY,-1,@ToDate)
   --      WHEN ScheduleType = 2 THEN DATEADD(WEEK,-1,@ToDate)
   --      WHEN ScheduleType = 1 THEN DATEADD(MONTH,-1,@ToDate)
   --      WHEN ScheduleType = 0 THEN DATEADD(YEAR,-1,@ToDate)
   --      ELSE DATEADD(DAY,-1,@ToDate)
   --   END
   --FROM dbo.[ReportSchedule] WITH (NOLOCK)
   --WHERE ReportScheduleID = @Default_ReportScheduleID

SELECT
  SensorID
INTO #SensorList
FROM dbo.[Sensor] WITH (NOLOCK)
WHERE CSNetID = @NetworkID


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



-- TABLE 3 DATA FOR REPORT --
DECLARE @SensorCount INT = 0
SELECT @SensorCount = COUNT(SensorID) FROM dbo.[Sensor] WITH (NOLOCK) WHERE CSNetID = @NetworkID;

IF @SensorCount > 35
BEGIN

	  SELECT
	  0 'DataMessageGUID',
	  0 'SensorID',
	  GETUTCDATE() 'MessageDate',
	  0 'State',
	  0 'SignalStrength',
	  0 'LinkQuality',
	  0 'Battery',
	  'Too many sensors on this network' 'Data',
	  0 'Voltage',
	  0 'MeetsNotificationRequirement',
	  0 'InsertDate',
	  0 'GatewayID',
	  0 'HasNote',
    0 'IsAuthenticated',
    0 'ApplicationID'

END
ELSE
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
	INNER JOIN #SensorList s WITH (NOLOCK) ON s.SensorID = dm.SensorID
	WHERE [MessageDate] > '''+ CONVERT(VARCHAR(20), @FromDate) + '''
	  AND [MessageDate] < ''' + CONVERT(VARCHAR(20), @ToDate) + '''
	ORDER BY s.SensorID, [MessageDate]';

  EXEC (@SQL);

END

GO


