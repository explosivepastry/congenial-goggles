USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_ReynoldsHeatIndex]    Script Date: 7/1/2024 8:54:30 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_ReynoldsHeatIndex]
  @Default_OwnerAccountID INT ,
  @Hours INT,
  @Default_ReportScheduleID INT 
AS
/* |Reynolds Heat Index:  All Networks|

  This report returns all data message for the account with the corresponding 
  heat index for a humidity sensor. Temperature/Heat Index is hard coded for F
*/

/*
----| Maintenance Log |----
  DATE        NAME        NOTES
  2017-06-26  Nathan      Created Proc
*/

/* ----| Sql Script |---- */
DECLARE @FromDate DATETIME
DECLARE @ToDate DATETIME
DECLARE @OffsetMinutes INT
DECLARE @TimezoneIdentifier VARCHAR(255)
DECLARE @SQL VARCHAR(MAX);

SELECT @TimezoneIdentifier = tz.TimeZoneIDString 
FROM TimeZone tz
INNER JOIN Account a ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID

SET @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));
SET @ToDate = Convert(datetime,Convert(varchar,DatePart(Year,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))) +'-'+ Convert(varchar,DatePart(month ,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate())))+'-'+ Convert(varchar,DatePart(Day,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))));
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
   WHERE ReportScheduleID = @Default_ReportScheduleID

  SET @SQL =
  'SELECT 
    c.CSNetID,
    c.Name,
    dm.SensorID,  
    s.SensorName,
    DATEADD(MINUTE, '+CONVERT(VARCHAR(4), @OffsetMinutes) +',dm.MessageDate) ''MessageDate'',
    DataMessageGUID, 
    State, 
    SignalStrength, 
    LinkQuality, 
    Battery, 
    Data,
    Temperature = CASE WHEN (SELECT s2.ApplicationID from Sensor s2 WITH (NOLOCK) where s2.SensorID = s.SensorID) = 43 
                        THEN CONVERT(VARCHAR(25), CONVERT(DECIMAL(5,2), CONVERT(DECIMAL(5, 2), SUBSTRING(Data, CHARINDEX('','',Data,0) + 1, LEN(Data) - CHARINDEX('','',Data,0) + 1)) * 9.0 / 5.0  + 32.0)) + '' F''
                        WHEN (SELECT s2.ApplicationID from Sensor s2 WITH (NOLOCK) where s2.SensorID = s.SensorID) = 2
                        THEN CONVERT(VARCHAR(25), CONVERT(DECIMAL(5,2), CONVERT(DECIMAL(5,2), Data) * 9.0 / 5.0 + 32.0)) + '' F''
                        ELSE '''' END,
    Voltage, 
    MeetsNotificationRequirement, 
    InsertDate, 
    GatewayID, 
    dm.HasNote,
    [HeatIndex] = CASE WHEN (SELECT s2.ApplicationID from Sensor s2 WITH (NOLOCK) where s2.SensorID = s.SensorID) = 43 THEN dbo.GetHeatIndex(data) ELSE NULL END
	FROM DataMessage dm WITH (NOLOCK)
	INNER JOIN Sensor s WITH (NOLOCK) ON s.SensorID = dm.SensorID
  INNER JOIN CSNet c WITH (NOLOCK) ON c.CsnetID = s.CSNetID
	WHERE c.AccountID = ' + CONVERT(VARCHAR(20), @Default_OwnerAccountID) + '
	AND [MessageDate] > '''+ CONVERT(VARCHAR(20), @FromDate) + '''
	AND [MessageDate] < ''' + CONVERT(VARCHAR(20), @ToDate) + '''
	ORDER BY c.csnetid, s.SensorID, [MessageDate]'

  EXEC (@SQL)


GO


