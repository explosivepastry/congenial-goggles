USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_MonnitUseNetworkExport]    Script Date: 7/1/2024 8:34:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_MonnitUseNetworkExport]
  @AccountID BIGINT,
  @ReportScheduleID INT,
  @ToDate DATETIME
AS
/* ----| Network : Data Export (Daily/Weekly/Monthly) Report |---- */

/*
----| Maintenance Log |----
Created 2016-06-14 Ala
Modified 2016-07-05 Ala
Modified 2016-08-15 Brandon //Allow all accounts to use this report
Modified 2016-09-12 Nathan //added HasNotes to return set
Modified 2016-12-02 Nathan // added nolock and dynamic querying in DM results
Modified 2020-02-20 Nathan // added IsAuthenticated
*/

/*Required AutoVariables*/
/*Required AutoVariables*/
--DECLARE @AccountID INT = 13333
--DECLARE @ReportScheduleID INT = 4514
--DECLARE @ToDate DATETIME = GETUTCDATE()

DECLARE @Hours INT 
DECLARE @Col_Date VARCHAR(5) 
DECLARE @Col_Sensor VARCHAR(5)
DECLARE @Col_GatewayID VARCHAR(5) 
DECLARE @Col_Sensor_State VARCHAR(5)
DECLARE @Col_Data VARCHAR(5)
DECLARE @Col_Plot_Value VARCHAR(5) 
DECLARE @Col_Text VARCHAR(5) 
DECLARE @Col_Alert VARCHAR(5) 
DECLARE @Col_Signal_Strength VARCHAR(5) 
DECLARE @Col_Voltage VARCHAR(5) 
DECLARE @Col_Battery VARCHAR(5)
DECLARE @Col_Special VARCHAR(5)
DECLARE @NetworkID INT 


/* ----| Sql Script |---- */
DECLARE @FromDate Datetime
DECLARE @OffsetMinutes INT
DECLARE @InternalOffsetMinutes INT
DECLARE @TimezoneIdentifier VARCHAR(255)
DECLARE @InternalTimeZone VARCHAR(255)
DECLARE @SQL VARCHAR(MAX);

SELECT @TimezoneIdentifier = tz.TimeZoneIDString 
FROM TimeZone tz
INNER JOIN Account a ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @AccountID

SELECT @InternalTimeZone = tz.TimeZoneIDString 
FROM TimeZone tz
INNER JOIN Account a ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = 1

SET @Hours  =               (SELECT rv.value  FROM dbo.ReportParameter rp INNER JOIN dbo.ReportParameterValue rv ON rv.ReportParameterID = rp.ReportParameterID WHERE rv.ReportScheduleID = @ReportScheduleID AND rp.ParamName = '@Hours')
SET @Col_Date  =            (SELECT [value] = case when rv.value = 1 THEN 'True' ELSE 'False' END  FROM dbo.ReportParameter rp INNER JOIN dbo.ReportParameterValue rv ON rv.ReportParameterID = rp.ReportParameterID WHERE rv.ReportScheduleID = @ReportScheduleID AND rp.ParamName = '@Col_Date')
SET @Col_Sensor =           (SELECT [value] = case when rv.value = 1 THEN 'True' ELSE 'False' END  FROM dbo.ReportParameter rp INNER JOIN dbo.ReportParameterValue rv ON rv.ReportParameterID = rp.ReportParameterID WHERE rv.ReportScheduleID = @ReportScheduleID AND rp.ParamName = '@Col_Sensor')
SET @Col_GatewayID  =       (SELECT [value] = case when rv.value = 1 THEN 'True' ELSE 'False' END  FROM dbo.ReportParameter rp INNER JOIN dbo.ReportParameterValue rv ON rv.ReportParameterID = rp.ReportParameterID WHERE rv.ReportScheduleID = @ReportScheduleID AND rp.ParamName = '@Col_GatewayID')
SET @Col_Sensor_State =     (SELECT [value] = case when rv.value = 1 THEN 'True' ELSE 'False' END  FROM dbo.ReportParameter rp INNER JOIN dbo.ReportParameterValue rv ON rv.ReportParameterID = rp.ReportParameterID WHERE rv.ReportScheduleID = @ReportScheduleID AND rp.ParamName = '@Col_Sensor_State')
SET @Col_Data =             (SELECT [value] = case when rv.value = 1 THEN 'True' ELSE 'False' END  FROM dbo.ReportParameter rp INNER JOIN dbo.ReportParameterValue rv ON rv.ReportParameterID = rp.ReportParameterID WHERE rv.ReportScheduleID = @ReportScheduleID AND rp.ParamName = '@Col_Data')
SET @Col_Plot_Value =       (SELECT [value] = case when rv.value = 1 THEN 'True' ELSE 'False' END  FROM dbo.ReportParameter rp INNER JOIN dbo.ReportParameterValue rv ON rv.ReportParameterID = rp.ReportParameterID WHERE rv.ReportScheduleID = @ReportScheduleID AND rp.ParamName = '@Col_Plot_Value') 
SET @Col_Text  =            (SELECT [value] = case when rv.value = 1 THEN 'True' ELSE 'False' END  FROM dbo.ReportParameter rp INNER JOIN dbo.ReportParameterValue rv ON rv.ReportParameterID = rp.ReportParameterID WHERE rv.ReportScheduleID = @ReportScheduleID AND rp.ParamName = '@Col_Text')
SET @Col_Alert  =           (SELECT [value] = case when rv.value = 1 THEN 'True' ELSE 'False' END  FROM dbo.ReportParameter rp INNER JOIN dbo.ReportParameterValue rv ON rv.ReportParameterID = rp.ReportParameterID WHERE rv.ReportScheduleID = @ReportScheduleID AND rp.ParamName = '@Col_Alert')
SET @Col_Signal_Strength =  (SELECT [value] = case when rv.value = 1 THEN 'True' ELSE 'False' END  FROM dbo.ReportParameter rp INNER JOIN dbo.ReportParameterValue rv ON rv.ReportParameterID = rp.ReportParameterID WHERE rv.ReportScheduleID = @ReportScheduleID AND rp.ParamName = '@Col_Signal_Strength')
SET @Col_Voltage  =         (SELECT [value] = case when rv.value = 1 THEN 'True' ELSE 'False' END  FROM dbo.ReportParameter rp INNER JOIN dbo.ReportParameterValue rv ON rv.ReportParameterID = rp.ReportParameterID WHERE rv.ReportScheduleID = @ReportScheduleID AND rp.ParamName = '@Col_Signal_Strength')
SET @Col_Battery =          (SELECT [value] = case when rv.value = 1 THEN 'True' ELSE 'False' END  FROM dbo.ReportParameter rp INNER JOIN dbo.ReportParameterValue rv ON rv.ReportParameterID = rp.ReportParameterID WHERE rv.ReportScheduleID = @ReportScheduleID AND rp.ParamName = '@Col_Battery')
SET @Col_Special =          (SELECT [value] = case when rv.value = 1 THEN 'True' ELSE 'False' END  FROM dbo.ReportParameter rp INNER JOIN dbo.ReportParameterValue rv ON rv.ReportParameterID = rp.ReportParameterID WHERE rv.ReportScheduleID = @ReportScheduleID AND rp.ParamName = '@Col_Special')
SET @NetworkID  =           (SELECT rv.value FROM dbo.ReportParameter rp INNER JOIN dbo.ReportParameterValue rv ON rv.ReportParameterID = rp.ReportParameterID WHERE rv.ReportScheduleID = @ReportScheduleID AND rp.ParamName = '@NetworkID')



SET @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));
SET @InternalOffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@InternalTimeZone));

SET @InternalOffsetMinutes = @OffsetMinutes - @InternalOffsetMinutes

SET @ToDate = Convert(datetime,Convert(varchar,DatePart(Year,DATEADD(MINUTE, @OffSetMinutes, @ToDate))) +'-'+ Convert(varchar,DatePart(month ,DATEADD(MINUTE, @OffSetMinutes, @ToDate)))+'-'+ Convert(varchar,DatePart(Day,DATEADD(MINUTE, @OffSetMinutes, @ToDate))));
SET @ToDate = DATEADD(MINUTE,-@OffsetMinutes, DATEADD(HOUR,@Hours,@ToDate));

SELECT @FromDate =
      CASE 
         WHEN ScheduleType = 3 THEN DATEADD(DAY,-1,@ToDate)
         WHEN ScheduleType = 2 THEN DATEADD(WEEK,-1,@ToDate)
         WHEN ScheduleType = 1 THEN DATEADD(MONTH,-1,@ToDate)
         WHEN ScheduleType = 0 THEN DATEADD(YEAR,-1,@ToDate)
         ELSE DATEADD(DAY,-1,@ToDate)
      END
   FROM ReportSchedule 
   WHERE ReportScheduleID = @ReportScheduleID



SELECT @TimezoneIdentifier = tz.TimeZoneIDString 
FROM TimeZone tz
INNER JOIN Account a ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @AccountID

-- TABLE 0 TYPE REPORT TO RUN --
SELECT 'Network' AS 'Type_Of_Sensor_Report';

-- TABLE 1 BASIC INFORMATION NEEDED TO RUN REPORT --
SELECT @FromDate AS 'From'
 ,@ToDate AS 'To'
 ,@TimezoneIdentifier AS 'Time_Zone_StringID'
 ,@AccountID AS 'AccountID'
 ,CSNetID AS 'CSNetID'
FROM CSNet WHERE CSNetID = @NetworkID;

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
SELECT @SensorCount = COUNT(SensorID) FROM Sensor WHERE CSNetID = @NetworkID

IF @SensorCount > 50
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
  0, 'ApplicationID'

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
	FROM DataMessage dm WITH (NOLOCK)
	INNER JOIN Sensor s ON s.SensorID = dm.SensorID
	WHERE CSNetID = ' + CONVERT(VARCHAR(20), @NetworkID) + '
	AND [MessageDate] > '''+ CONVERT(VARCHAR(20), @FromDate) + '''
	AND [MessageDate] < ''' + CONVERT(VARCHAR(20), @ToDate) + '''
	ORDER BY s.SensorID, [MessageDate]'

  EXEC (@SQL)
END

GO


