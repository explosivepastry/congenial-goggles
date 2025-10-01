USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_AccountExportByRange_Horizons]    Script Date: 6/24/2024 2:34:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_AccountExportByRange_Horizons]
  @Default_OwnerAccountID BIGINT,
  @FromDate DATETIME,
  @ToDate DATETIME
AS

DECLARE @SQL VARCHAR(8000)
DECLARE @TimeZoneIdentifier VARCHAR(300)

IF DATEDIFF(Day, @FromDate, @ToDate) > 62
SET @ToDate = DATEADD(Day, 62, @FromDate)

SELECT
  @TimeZoneIdentifier = TimeZoneIDString
FROM dbo.[Account] a WITH (NOLOCK)
INNER JOIN dbo.[TimeZone] t WITH (NOLOCK) ON a.TimeZoneID = t.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID;

SELECT 'Account' AS 'Type_Of_Sensor_Report';

SELECT 
  @FromDate AS 'From'
 ,@ToDate AS 'To'
 ,@TimezoneIdentifier AS 'Time_Zone_StringID'
 ,@Default_OwnerAccountID AS 'AccountID';

 SELECT
  @FromDate = dbo.GetUTCTime( @FromDate, TimeZoneIDString),
  @ToDate   = dbo.GetUTCTime( @ToDate,   TimeZoneIDString)
FROM dbo.[Account] a WITH (NOLOCK)
INNER JOIN dbo.[TimeZone] t WITH (NOLOCK) ON a.TimeZoneID = t.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID;


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

--38399
--39284
--40532
--40534
--40535

SELECT
  SensorID
INTO #SensorList
FROM dbo.[Sensor] s WITH (NOLOCK)
WHERE AccountID = @Default_OwnerAccountID

IF @@RowCount > 200
BEGIN 

  SELECT
    MessageDate                   = NULL,
    SensorID                      = NULL,
    DataMessageGUID               = NULL,
    State                         = NULL,
    SignalStrength                = NULL,
    LinkQuality                   = NULL,
    Battery                       = NULL,
    Data                          = 'Too many sensors',
    Voltage                       = NULL,
    MeetsNotificationRequirement  = NULL,
    InsertDate                    = NULL,
    GatewayID                     = NULL,
    HasNote                       = NULL,
    IsAuthenticated               = NULL,
    Application                   = NULL

END ELSE
BEGIN

    SET @SQL =
    'SELECT
      d.*
    FROM dbo.[DataMessage] d WITH (NOLOCK)
    INNER JOIN #SensorList s on d.SensorID = s.SensorID
    WHERE d.MessageDate >= ''' + CONVERT(VARCHAR(30), @FromDate, 120) + '''
      AND d.MessageDate <= ''' + CONVERT(VARCHAR(30), @ToDate  , 120) + '''
    ORDER BY SensorID, MessageDate'

    EXEC (@SQL)

END

DROP TABLE #SensorList


GO


