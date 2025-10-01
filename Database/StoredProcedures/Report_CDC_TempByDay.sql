USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_CDC_TempByDay]    Script Date: 6/24/2024 2:45:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_CDC_TempByDay]
  @Default_OwnerAccountID BIGINT,
  @NetworkID INT
AS
/* ----| CDC Report |---- */

/*
----| Maintenance Log |----
Created 2016-06-21 Ala
Modified 2016-07-05 Ala
*/

/*Required AutoVariables*/
--DECLARE @Default_OwnerAccountID INT = 2
--DECLARE @NetworkID INT = 100


/* ----| Sql Script |---- */
DECLARE @Temp_Cooler_Table TABLE 
(   
	ID BIGINT IDENTITY(1,1) PRIMARY KEY,
	SensorID BIGINT NOT NULL,
	YearMonthDay VARCHAR(10) NOT NULL,
	Five_AM    VARCHAR(2000) NULL
)

DECLARE @Temp_Sensors TABLE 
(   
	ID BIGINT IDENTITY(1,1) PRIMARY KEY,
	SensorID BIGINT NOT NULL,
	SensorName VARCHAR(250) NOT NULL
)
INSERT INTO @Temp_Sensors
SELECT s.SensorID, s.SensorName
	FROM dbo.Sensor s with(NOLOCK)
	INNER JOIN dbo.CSNet c with(NOLOCK) ON c.CSNetID = s.CSNetID
	INNER JOIN dbo.Account a with(NOLOCK) ON a.AccountID = c.AccountID
	WHERE s.CSNetID = @NetworkID
	AND s.ApplicationID in (2, 35, 46,65)
	ORDER BY s.SensorName
	
DECLARE @OffsetMinutes INT, @TimezoneIdentifier VARCHAR(255), @BeginDate DATETIME, @DayDate DATETIME, @EndDate DATETIME, @SensorID BIGINT, @TempID BIGINT, @MaxID BIGINT

-- Get the TimezoneIdentifier for the account
SELECT @TimezoneIdentifier = tz.TimeZoneIDString 
	FROM dbo.TimeZone tz with(NOLOCK)
	INNER JOIN dbo.Account a with(NOLOCK) ON a.TimeZoneID = tz.TimeZoneID
	WHERE a.AccountID = @Default_OwnerAccountID

-- Get the current local date
SET @BeginDate = dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier)

-- Get the offset minutes
-- Get the first day of the month that yesterday belonged to (first day of month returns last months results)
SELECT @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),@BeginDate)
	,@BeginDate = DATEADD(month, DATEDIFF(MONTH, 0, DATEADD(DAY, -1, @BeginDate)), 0);

--UTC Time for StartDate
SET @DayDate = DATEADD(MINUTE, @OffsetMinutes * -1 , @BeginDate)

--UTC Time for EndDate
SET @EndDate = DATEADD(MONTH, 1, @DayDate)
IF(@EndDate > GETUTCDATE())
BEGIN
	SET @EndDate = GETUTCDATE()
END

SELECT @TempID = MIN(ID) FROM @Temp_Sensors
SELECT @MaxID = MAX(ID) FROM @Temp_Sensors

-- Return table of information
SELECT c.CSNetID, c.Name 'Network', a.AccountID, a.CompanyName, @BeginDate 'BeginDate', DATEADD(MINUTE, @OffsetMinutes , GETUTCDATE()) 'Date'
	FROM dbo.CSNet c with(NOLOCK)
	INNER JOIN dbo.Account a with(NOLOCK) ON a.AccountID = c.AccountID
	WHERE c.CSNetID = @NetworkID

WHILE @TempID <= @MaxID
BEGIN
	-- select sensor
	SELECT @SensorID = SensorID FROM @Temp_Sensors WHERE ID = @TempID
	PRINT CONVERT(VARCHAR,@TempID) + ': ' + CONVERT(VARCHAR,@SensorID)
	IF(@SensorID IS NULL)
		CONTINUE

	IF EXISTS (SELECT SensorAttributeID FROM SensorAttribute WHERE SensorID = @SensorID and Name LIKE 'CorF' AND Value LIKE 'C')
	BEGIN
		WHILE @DayDate < @EndDate
		BEGIN
			INSERT INTO @Temp_Cooler_Table
			(SensorID, YearMonthDay, Five_AM)

			SELECT
				 (SELECT @SensorID)
				,(SELECT CONVERT(VARCHAR(10),@DayDate,111)) 
				,(SELECT TOP 1 Data FROM dbo.DataMessage with(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,5,@DayDate)  AND  DATEADD(Hour,6,@DayDate))
				
			SET @DayDate = DATEADD(DAY, 1, @DayDate)
		END
	END
	ELSE
	BEGIN
		WHILE @DayDate < @EndDate
		BEGIN
			INSERT INTO @Temp_Cooler_Table
			(SensorID, YearMonthDay, Five_AM)
				SELECT
				 (SELECT @SensorID)
				,(SELECT CONVERT(VARCHAR(10),@DayDate,111)) 
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM dbo.DataMessage with(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,5,@DayDate)  AND  DATEADD(HOUR,6,@DayDate)))*9.0/5.0+32.0)
				
			SET @DayDate = DateAdd(DAY, 1, @DayDate)
		END
	END

	-- Return table of sensor information
	SELECT SensorID, SensorName
	FROM @Temp_Sensors
	WHERE SensorID = @SensorID

	-- Return Table of Data
	SELECT CAST(MONTH(YearMonthDay) AS VARCHAR(2)) + '/' + CAST(DAY(YearMonthDay) AS VARCHAR(2)) 'Date',
		Five_AM    '5A'
	FROM @Temp_Cooler_Table;

	-- Reset UTC Time for StartDate
	SET @DayDate = DATEADD(MINUTE, @OffsetMinutes * -1 , @BeginDate)
	--Clear data from Temp Table
	DELETE FROM @Temp_Cooler_Table;
	-- Move to next sensor record
	SELECT @TempID = @TempID + 1;
END
GO


