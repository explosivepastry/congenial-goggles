USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_DataExportEstimate]    Script Date: 6/24/2024 2:55:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_DataExportEstimate] 
  @AccountID BIGINT,
  @FromDate DATETIME,
  @ToDate DATETIME
AS

/*********************************************
This report is to help customer service
mitigate the cases where a customer is 
requesting a large data export and whether
it's something they can take care of on their own
or if it's something they should forward to us
to make sure we don't overtax anything
*********************************************/

SELECT
  SensorID, c.CSNetID, c.Name
INTO #SensorList
FROM Sensor s WITH (NOLOCK)
INNER JOIN CSNet c WITH (NOLOCK) on s.csnetid = c.csnetid
WHERE s.AccountID = @AccountID;

WITH cte_results AS 
(
    SELECT DISTINCT 
      p.SensorID, 
      p.Date, 
      s.CSNetID,
      s.Name,
      p.SensorMessageCounts 
    FROM PreAggregation p WITH (NOLOCK)
    INNER JOIN #SensorList s WITH (NOLOCK) ON p.SensorID = s.SensorID
    WHERE Date >= @FromDate
      AND Date <= @ToDate
)
SELECT 
  SensorID, 
  CSNetID, 
  NetworkName = Name,
  EstimatedSensorMessages = SUM(SensorMessageCounts),
  EarliestPreAgDateInRange = Min(Date)
FROM cte_results 
GROUP BY SensorID, CSNetID, Name
ORDER BY CSNetID, Name, SensorID;
GO


