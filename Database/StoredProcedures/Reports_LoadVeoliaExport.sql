USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_LoadVeoliaExport]    Script Date: 6/28/2024 4:10:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_LoadVeoliaExport]
  @AccountID BIGINT
AS
 
 --Parameters
	DECLARE 
    @PreviousDataMessage  DATETIME, 
		@MaxCount             INT,
		@FirstDataMessage     DATETIME,
		@LastDatMessage       DATETIME,
		@Count                INT,
    @SQL                  VARCHAR(2000),
    @_AccountID           BIGINT = @AccountID,
    @FromDate             DATETIME,
    @ToDate               DATETIME;
 

  --Temp Tables
  CREATE TABLE #InsertTable 
  (
    SensorID        BIGINT,
    MessageDate     DATETIME,
    DataMessageGUID UNIQUEIDENTIFIER
  );

  CREATE TABLE #SensorList
  (
    SensorID BIGINT
  );

	SELECT @PreviousDataMessage = (SELECT TOP 1 IsNull(LastMessageDate,'2020-01-23') FROM ExportStatistics WHERE AccountID = @_AccountID ORDER BY ExportStatisticsID DESC);


  --message dates within 2 days in order to limit the # of DM tables
  SET @FromDate = DATEADD(HOUR, -48, GETUTCDATE());
  SET @ToDate   = DATEADD(HOUR, 100, GETUTCDATE());

	SELECT 
    @MaxCount = CASE WHEN Floor(MAX(MessageCount) * 1.1) < 2500 THEN 2500 
                     WHEN Floor(MAX(MessageCount) * 1.1) > 15000 THEN 15000 
                     ELSE Floor(MAX(MessageCount) * 1.1) END
	FROM (SELECT TOP 5 
          MessageCount
		    FROM ExportStatistics
        WHERE AccountID = @_AccountID
		    ORDER BY ExportStatisticsID DESC) es;
  
  INSERT INTO #SensorList 
  SELECT
    [SensorID]
  FROM dbo.[Sensor] s
  INNER JOIN dbo.[Account] a WITH (NOLOCK) on s.AccountID = a.AccountID
  WHERE a.AccountIDTree LIKE '%*'+CONVERT(VARCHAR(10), @_AccountID)+'*%';

  --this is the workaround to lookup DMs by insert date for messages within the last 2 days
  SET @SQL = 
  'SELECT TOP '++CONVERT(VARCHAR(30), 14000)+ '
    d.SensorID,
    d.MessageDate,
    d.DataMessageGUID
  FROM #SensorList s
  INNER JOIN dbo.[DataMessage] d WITH (NOLOCK) on s.SensorID = d.SensorID
  where InsertDate >= '''+CONVERT(VARCHAR(30), @PreviousDataMessage, 120)+'''
    AND d.MessageDate >= '''+CONVERT(VARCHAR(30),@FromDate,120)+'''
    AND d.MessageDate <= '''+CONVERT(VARCHAR(30),@ToDate, 120)+'''
  ORDER BY InsertDate';
  
  INSERT INTO #InsertTable (SensorID, MessageDate, DataMessageGUID)
  EXEC (@SQL);
  
  

  --If you need to grab more columns from the sensor object, do so here
  SELECT TOP (IsNull(@MaxCount,500))
    d.*,
    AccountName = a.AccountNumber
  INTO #Temp1
  FROM dbo.[DataMessage] d    WITH (NOLOCK)
  INNER JOIN #InsertTable t ON d.Messagedate = t.MessageDate AND d.SensorID = t.SensorID AND d.DataMessageGUID = t.DataMessageGUID
  INNER JOIN dbo.[Sensor] s   WITH (NOLOCK) ON s.SensorID  = d.SensorID
  INNER JOIN dbo.[Account] A  WITH (NOLOCK) ON s.AccountID = a.AccountID
  WHERE d.MessageDate >= @FromDate
    AND d.MessageDate <= @ToDate;

  --DataMessage
  SELECT 
    * 
  FROM #Temp1 
  ORDER BY 
    GatewayID, 
    MessageDate; 

  --Sensor
  SELECT DISTINCT
  S.*
  FROM Sensor s
  INNER JOIN #Temp1 t on s.SensorID = t.sensorid

  --SensorAttribute
  SELECT DISTINCT
    sa.*
  FROM #Temp1 t
  INNER JOIN dbo.[SensorAttribute] sa on t.SensorId = sa.SensorID;

	SELECT 
    @FirstDataMessage = ISNULL(MIN(InsertDate), @PreviousDataMessage), 
    @LastDatMessage   = ISNULL(MAX(InsertDate), @PreviousDataMessage), 
    @Count            = ISNULL(COUNT([MessageDate]),0)
	FROM #Temp1;

	INSERT INTO ExportStatistics (ExportDate,FirstMessageDate, LastMessageDate,MessageCount, AccountID)
		VALUES (GETUTCDATE(),@FirstDataMessage,@LastDatMessage, @Count, @_AccountID);
	
	SELECT * FROM ExportStatistics WITH (NOLOCK) WHERE ExportStatisticsID = @@IDENTITY;
	
	SELECT PreviousMessageDate = @PreviousDataMessage;

GO


