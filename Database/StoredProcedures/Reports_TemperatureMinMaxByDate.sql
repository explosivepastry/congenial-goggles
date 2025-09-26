USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_TemperatureMinMax]    Script Date: 7/1/2024 9:32:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_TemperatureMinMaxByDate]
  @Default_OwnerAccountID BIGINT,
  @FromDate DATETIME,
  @ToDate DATETIME
AS

/* ----| Sql Script |---- */
--DECLARE @Default_OwnerAccountID BIGINT = 44965
--DECLARE @Default_ReportScheduleID BIGINT = 52650
DECLARE @SQL VARCHAR(MAX);

/**************************************************

  SET From/ToDate based on account time zone
          and ScheduleType

**************************************************/

IF DATEDIFF(DAY, @FromDate, @ToDate) > 370
BEGIN

  SELECT Message = 'Date Range Must Be Within 1 Year'

END ELSE
BEGIN

  SET @SQL = 
  'SELECT DISTINCT
  s.SensorID, s.SensorName, p.Date, a.ApplicationName,
  Min = CASE WHEN ISNULL(sa.Value, ''F'') = ''F'' THEN CONVERT(DECIMAL(18,2), (p.Min * (9.0/5.0))+32.0) else p.Min END, 
  Max = CASE WHEN ISNULL(sa.Value, ''F'') = ''F'' THEN CONVERT(DECIMAL(18,2), (p.Max * (9.0/5.0))+32.0) else p.Max END,
  Avg = CASE WHEN ISNULL(sa.Value, ''F'') = ''F'' THEN CONVERT(DECIMAL(18,2), (p.Avg * (9.0/5.0))+32.0) else p.Avg END
  FROM dbo.[Sensor] s WITH (NOLOCK)
  INNER JOIN dbo.[PreAggregation] p WITH (NOLOCK) ON s.SensorID = p.SensorID
  INNER JOIN dbo.[Application] a WITH (NOLOCK) ON s.ApplicationID = a.ApplicationID
  LEFT JOIN dbo.[SensorAttribute] sa WITH (NOLOCK) on sa.SensorID = s.SensorID and Name = ''CorF''
  WHERE s.AccountID = '+CONVERT(VARCHAR(30), @Default_OwnerAccountID) + '
    AND p.SplitValue = ''Temperature''
    AND p.Date >= '''+CONVERT(VARCHAR(30), @FromDate, 120) + ''' and p.Date <= '''+CONVERT(VARCHAR(30),@ToDate)+'''
  ORDER BY s.SensorID, p.Date';



  EXEC (@SQL)

END
