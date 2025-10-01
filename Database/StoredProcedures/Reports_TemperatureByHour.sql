USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_TemperatureByHour]    Script Date: 7/1/2024 9:04:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_TemperatureByHour]
  @Default_OwnerAccountID BIGINT,
  @NetworkID INT
AS
/* ----| Temperature By Hour Report |---- */

/*
----| Maintenance Log |----
Created 2016-06-25 Ala
Modified 2016-07-05 Ala
Modified 2016-10-07 NLN - Query text had errors. Fixed. Edited to run for previous month and then changed back for the monthly schedule. Added NoLocks to DataMessage queries
*/

/*Required AutoVariables*/
--DECLARE @Default_OwnerAccountID INT = 3154
--DECLARE @NetworkID INT = 6511


/* ----| Sql Script |---- */
DECLARE @Temp_Cooler_Table TABLE 
(   
	ID BIGINT IDENTITY(1,1) PRIMARY KEY,
	SensorID BIGINT NOT NULL,
	YearMonthDay VARCHAR(10) NOT NULL,
	Twelve_AM   VARCHAR(2000) NULL	,
	One_AM     VARCHAR(2000) NULL	,
	Two_AM     VARCHAR(2000) NULL	,
	Three_AM   VARCHAR(2000) NULL	,
	Four_AM    VARCHAR(2000) NULL	,
	Five_AM    VARCHAR(2000) NULL	,
	Six_AM     VARCHAR(2000) NULL	,
	Seven_AM   VARCHAR(2000) NULL	,
	Eight_AM   VARCHAR(2000) NULL	,
	Nine_AM    VARCHAR(2000) NULL	,
	Ten_AM     VARCHAR(2000) NULL	,
	Eleven_AM  VARCHAR(2000) NULL	,
	Twelve_PM  VARCHAR(2000) NULL	,
	One_PM     VARCHAR(2000) NULL	,
	Two_PM     VARCHAR(2000) NULL	,
	Three_PM   VARCHAR(2000) NULL	,
	Four_PM    VARCHAR(2000) NULL	,
	Five_PM    VARCHAR(2000) NULL	,
	Six_PM     VARCHAR(2000) NULL	,
	Seven_PM   VARCHAR(2000) NULL	,
	Eight_PM   VARCHAR(2000) NULL	,
	Nine_PM    VARCHAR(2000) NULL	,
	Ten_PM     VARCHAR(2000) NULL	,
	Eleven_PM  VARCHAR(2000) NULL	
)

Declare @Temp_Sensors table 
(   
	ID BIGINT IDENTITY(1,1) PRIMARY KEY,
	SensorID BIGINT NOT NULL,
	SensorName VARCHAR(250) NOT NULL
)
INSERT INTO @Temp_Sensors
SELECT s.SensorID, s.SensorName
	FROM dbo.Sensor s WITH(NOLOCK)
	INNER JOIN dbo.CSNet c WITH(NOLOCK) ON c.CSNetID = s.CSNetID
	INNER JOIN dbo.Account a WITH(NOLOCK)  ON a.AccountID = c.AccountID
	WHERE s.CSNetID = @NetworkID
	AND s.ApplicationID IN (2, 35, 46,65)
	ORDER BY s.SensorName
	
DECLARE @OffsetMinutes INT, @TimezoneIdentifier VARCHAR(255), @BeginDate DATETIME, @DayDate DATETIME, @EndDate DATETIME, @SensorID BIGINT, @TempID BIGINT, @MaxID BIGINT

-- Get the TimezoneIdentifier for the account
SELECT @TimezoneIdentifier = tz.TimeZoneIDString 
	FROM dbo.TimeZone tz WITH(NOLOCK) 
	INNER JOIN dbo.Account a WITH(NOLOCK)  ON a.TimeZoneID = tz.TimeZoneID
	WHERE a.AccountID = @Default_OwnerAccountID
  
-- Get the current local date
SET @BeginDate = dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier)

-- Get the offset minutes
-- Get the first day of the MONTH that yesterday belonged to (first day of MONTH returns last months results)
SELECT @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),@BeginDate)
	,@BeginDate = DATEADD(MONTH, DATEDIFF(MONTH, 0, DATEADD(day, -1, @BeginDate)), 0);

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
	FROM dbo.CSNet c WITH(NOLOCK) 
	INNER JOIN dbo.Account a WITH(NOLOCK)  ON a.AccountID = c.AccountID
	WHERE c.CSNetID = @NetworkID

WHILE @TempID <= @MaxID
BEGIN
	-- SELECT sensor
	SELECT @SensorID = SensorID FROM @Temp_Sensors WHERE ID = @TempID
	print CONVERT(VARCHAR,@TempID) + ': ' + CONVERT(VARCHAR,@SensorID)
	if(@SensorID is null)
		CONTINUE

	IF EXISTS (SELECT SensorAttributeID FROM SensorAttribute WHERE SensorID = @SensorID AND Name like 'CorF' AND Value like 'C')
	BEGIN
		WHILE @DayDate < @EndDate
		BEGIN
			INSERT INTO @Temp_Cooler_Table
			(SensorID, YearMonthDay, Twelve_AM, One_AM,Two_AM,Three_AM,Four_AM,Five_AM,Six_AM,Seven_AM,Eight_AM,Nine_AM,Ten_AM,Eleven_AM,Twelve_PM,One_PM,Two_PM,Three_PM,Four_PM,Five_PM,Six_PM,Seven_PM,Eight_PM,Nine_PM,Ten_PM,Eleven_PM)

			SELECT
				 (SELECT TOP 1 SensorID FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID)
				,(SELECT CONVERT(VARCHAR(10),@DayDate,111)) 
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN @DayDate AND  DATEADD(HOUR,1,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,1,@DayDate)  AND  DATEADD(HOUR,2,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,2,@DayDate)  AND  DATEADD(HOUR,3,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,3,@DayDate)  AND  DATEADD(HOUR,4,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,4,@DayDate)  AND  DATEADD(HOUR,5,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,5,@DayDate)  AND  DATEADD(HOUR,6,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,6,@DayDate)  AND  DATEADD(HOUR,7,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,7,@DayDate)  AND  DATEADD(HOUR,8,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,8,@DayDate)  AND  DATEADD(HOUR,9,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,9,@DayDate)  AND  DATEADD(HOUR,10,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,10,@DayDate) AND  DATEADD(HOUR,11,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,11,@DayDate) AND  DATEADD(HOUR,12,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,12,@DayDate) AND  DATEADD(HOUR,13,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,13,@DayDate) AND  DATEADD(HOUR,14,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,14,@DayDate) AND  DATEADD(HOUR,15,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,15,@DayDate) AND  DATEADD(HOUR,16,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,16,@DayDate) AND  DATEADD(HOUR,17,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,17,@DayDate) AND  DATEADD(HOUR,18,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,18,@DayDate) AND  DATEADD(HOUR,19,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,19,@DayDate) AND  DATEADD(HOUR,20,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,20,@DayDate) AND  DATEADD(HOUR,21,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,21,@DayDate) AND  DATEADD(HOUR,22,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,22,@DayDate) AND  DATEADD(HOUR,23,@DayDate))
				,(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,23,@DayDate) AND  DATEADD(DAY,1, @DayDate))

			SET @DayDate = DATEADD(DAY, 1, @DayDate)
		END
	END
	ELSE
	BEGIN
		WHILE @DayDate < @EndDate
		BEGIN
			INSERT INTO @Temp_Cooler_Table
			(SensorID, YearMonthDay, Twelve_AM, One_AM,Two_AM,Three_AM,Four_AM,Five_AM,Six_AM,Seven_AM,Eight_AM,Nine_AM,Ten_AM,Eleven_AM,Twelve_PM,One_PM,Two_PM,Three_PM,Four_PM,Five_PM,Six_PM,Seven_PM,Eight_PM,Nine_PM,Ten_PM,Eleven_PM)
				SELECT
				 (SELECT @SensorID)
				,(SELECT CONVERT(VARCHAR(10),@DayDate,111)) 
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN @DayDate AND  DATEADD(HOUR,1,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,1,@DayDate)  AND  DATEADD(HOUR,2,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,2,@DayDate)  AND  DATEADD(HOUR,3,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,3,@DayDate)  AND  DATEADD(HOUR,4,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,4,@DayDate)  AND  DATEADD(HOUR,5,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,5,@DayDate)  AND  DATEADD(HOUR,6,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,6,@DayDate)  AND  DATEADD(HOUR,7,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,7,@DayDate)  AND  DATEADD(HOUR,8,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,8,@DayDate)  AND  DATEADD(HOUR,9,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,9,@DayDate)  AND  DATEADD(HOUR,10,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,10,@DayDate) AND  DATEADD(HOUR,11,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,11,@DayDate) AND  DATEADD(HOUR,12,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,12,@DayDate) AND  DATEADD(HOUR,13,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,13,@DayDate) AND  DATEADD(HOUR,14,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,14,@DayDate) AND  DATEADD(HOUR,15,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,15,@DayDate) AND  DATEADD(HOUR,16,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,16,@DayDate) AND  DATEADD(HOUR,17,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,17,@DayDate) AND  DATEADD(HOUR,18,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,18,@DayDate) AND  DATEADD(HOUR,19,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,19,@DayDate) AND  DATEADD(HOUR,20,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,20,@DayDate) AND  DATEADD(HOUR,21,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,21,@DayDate) AND  DATEADD(HOUR,22,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,22,@DayDate) AND  DATEADD(HOUR,23,@DayDate)))*9.0/5.0+32.0)
				,(CONVERT(DECIMAL(18,2),(SELECT TOP 1 Data FROM DataMessage WITH(NOLOCK) WHERE SensorID = @SensorID AND MessageDate BETWEEN DATEADD(HOUR,23,@DayDate) AND  DATEADD(DAY,1, @DayDate)))*9.0/5.0+32.0)

			SET @DayDate = DATEADD(DAY, 1, @DayDate)
		END
	END

	-- Return table of sensor information
	SELECT SensorID, SensorName
	FROM @Temp_Sensors
	WHERE SensorID = @SensorID

	-- Return Table of Data
	SELECT CAST(Month(YearMonthDay) AS VARCHAR(2)) + '/' + CAST(DAY(YearMonthDay) AS VARCHAR(2)) 'Date',
		Twelve_AM  '12A',
		One_AM     '1A',
		Two_AM     '2A',
		Three_AM   '3A',
		Four_AM    '4A',
		Five_AM    '5A',
		Six_AM     '6A',
		Seven_AM   '7A',
		Eight_AM   '8A',
		Nine_AM    '9A',
		Ten_AM     '10A',
		Eleven_AM  '11A',
		Twelve_PM  '12P',
		One_PM     '1P',
		Two_PM     '2P',
		Three_PM   '3P',
		Four_PM    '4P',
		Five_PM    '5P',
		Six_PM     '6P',
		Seven_PM   '7P',
		Eight_PM   '8P',
		Nine_PM    '9P',
		Ten_PM     '10P',
		Eleven_PM  '11P'
	FROM @Temp_Cooler_Table;

	-- Reset UTC Time for StartDate
	SET @DayDate = DATEADD(MINUTE, @OffsetMinutes * -1 , @BeginDate)
	--Clear data FROM Temp Table
	DELETE FROM @Temp_Cooler_Table;
	-- Move to next sensor record
	SELECT @TempID = @TempID + 1;
END


GO


