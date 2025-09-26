USE [iMonnit]
GO
/****** Object:  StoredProcedure [dbo].[Report_Gen1DistrictBattery]    Script Date: 6/28/2024 3:59:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER Procedure [dbo].[Report_Gen1DistrictBattery]
  @Default_OwnerAccountID BIGINT
AS


CREATE TABLE #Results
(
  SensorID              BIGINT,
  SensorName            NVARCHAR(300),
  Market                VARCHAR(300),
  District              VARCHAR(300),
  Division              VARCHAR(300),
  LastCommunicationDate DATETIME,
  PowerSourceID         BIGINT
);

CREATE TABLE #Results_P
(
SensorID              BIGINT,
SensorName            NVARCHAR(300),
Market                VARCHAR(300),
District              VARCHAR(300),
Division              VARCHAR(300),
LastCommunicationDate DATETIME,
PowerSourceID         BIGINT,
Date                  DATETIME,
SensorMessageCounts   INT, 
Avg_Voltage           DECIMAL(18,4),
Avg_Percentage        DECIMAL(18,4)
)

CREATE TABLE #PowerTable
(
  RowID       BIGINT,
  Voltage     DECIMAL(18,4),
  Percentage  INT
)


CREATE TABLE #Calendar
(
--d.TheDate, d.FirstOfWeek, r.SensorID, r.Market, r.District
  TheDate       Date,
  FirstOfWeek   Date,
  SensorID      BIGINT,
  SensorName    NVARCHAR(300),
  Market        VARCHAR(300),
  District      VARCHAR(300)
)

DECLARE @cols NVARCHAR(Max);
DECLARE @query nvarchar(max)

INSERT INTO #PowerTable (RowID, Voltage, Percentage)
SELECT 
  RowID       = Row_Number() OVER (PARTITION BY 1 ORDER BY Voltage),
  Voltage,
  Percentage  = 
          CASE WHEN [Percentage] ='VoltageForZeroPercent'         then 0   else
          CASE WHEN [Percentage] ='VoltageForTenPercent'          then 10  else
          CASE WHEN [Percentage] ='VoltageForTwentyFivePercent'   then 25  else
          CASE WHEN [Percentage] ='VoltageForFiftyPercent'        then 50  else
          CASE WHEN [Percentage] ='VoltageForSeventyFivePercent'  then 75  else
          CASE WHEN [Percentage] ='VoltageForOneHundredPercent'   then 100 else 0
END END END END END END
FROM dbo.[PowerSource] WITH (NOLOCK)
UNPIVOT
(
  Voltage FOR [Percentage] IN (
  [VoltageForZeroPercent],	
  [VoltageForTenPercent],	
  [VoltageForTwentyFivePercent],	
  [VoltageForFiftyPercent],	
  [VoltageForSeventyFivePercent],	
  [VoltageForOneHundredPercent])
) t
WHERE powersourceid = 1

INSERT INTO #PowerTable
VALUES 
(0, 0, 0),
(7, 5, 100)

INSERT INTO #Results 
(
  SensorID,
  SensorName,
  Market,
  District ,
  Division ,
  LastCommunicationDate,
  PowerSourceID
)
SELECT
  s.SensorID, 
  s.SensorName,
  Market    = a.AccountNumber, 
  District  = a2.AccountNumber, 
  Division  = a3.AccountNumber, 
  s.LastCommunicationDate, 
  PowerSourceID
FROM dbo.[Account] a WITH (NOLOCK)
INNER JOIN dbo.[Account] a2 WITH (NOLOCK) ON a.RetailAccountID = a2.AccountID
INNER JOIN dbo.[Account] a3 WITH (NOLOCK) ON a2.RetailAccountID = a3.AccountID
INNER JOIN dbo.[Sensor] s WITH (NOLOCK) ON a.AccountID = s.AccountID
WHERE a.AccountIDTree LIKE '%*'+CONVERT(VARCHAR(30), @Default_OwnerAccountID)+'*%'
  AND a.AccountID  != 28211
  AND a.RetailAccountID != 28211
  AND ISNULL(s.GenerationType, 'Gen1') =  'Gen1'
  AND a.AccountNumber NOT LIKE '%INACTIVE'


insert into #Results_P
( SensorID,
  SensorName,
  Market,
  District ,
  Division ,
  LastCommunicationDate,
  PowerSourceID,
  Date,
  SensorMessageCounts, 
  Avg_Voltage )
SELECT distinct
  r.SensorID,
  r.SensorName,
  r.Market,
  r.District ,
  r.Division ,
  r.LastCommunicationDate,
  r.PowerSourceID,
  p.date, 
  p.SensorMessageCounts,
  p.Avg_Voltage
from #Results r
Left Join dbo.[PreAggregation] p ON r.SensorID = p.SensorID AND p.Date >= DATEADD(month, -6, CONVERT(Date, GETUTCDATE())) AND p.date < convert(date, getutcdate())


update p 
set Avg_Percentage =  CONVERT(DECIMAL(18,2), (((ColumnName_HighPercent - ColumnName_LowPercent) / (TableValue_High - TableValue_Low)) * Avg_Voltage) - (((ColumnName_HighPercent - ColumnName_LowPercent) / (TableValue_High - TableValue_Low) * TableValue_Low) - ColumnName_LowPercent))
from #Results_P p
INNER JOIN (SELECT
              p.RowID, 
              TableValue_Low          = p.Voltage,
              TableValue_High         = p2.Voltage, 
              ColumnName_LowPercent   = p.Percentage,
              ColumnName_HighPercent  = p2.Percentage
            FROM #PowerTable p
            LEFT JOIN #PowerTable p2 on p.RowID = p2.RowID -1
            --order by p.rowid
            ) t on p.Avg_Voltage >= t.TableValue_Low and p.Avg_Voltage < t.TableValue_High



INSERT INTO #Calendar (TheDate, FirstOfweek, Sensorid, SensorName, Market, District)
SELECT DISTINCT
  d.TheDate, 
  d.FirstOfWeek,
  r.SensorID, 
  r.SensorName,
  r.Market, 
  r.District
FROM mea.dbo.[dimDate] d
INNER JOIN #Results r on r.SensorID = r.SensorID
  AND d.TheDate >= DATEADD(MONTH, -6, CONVERT(Date, GETUTCDATE())) 
  AND d.TheDate < CONVERT(Date, GETUTCDATE())
ORDER BY SensorID, TheDate


SELECT @cols = STUFF((SELECT DISTINCT ',' +
                        QUOTENAME(FirstOfWeek) 
                      FROM #Calendar
                        ORDER BY  ',' +
                        QUOTENAME(FirstOfWeek) 
                      FOR XML PATH(''), TYPE
                      ).value('.', 'NVARCHAR(MAX)') 
                        , 1, 1, '');

  SET @Query = 
  'WITH CTE_Results as
  (
    SELECT 
      c.SensorID, 
      c.SensorName,
      c.Market, 
      c.District, 
      FirstOfWeek,
      AvgPercentage_Weekly = CONVERT(DECIMAL(18,1), SUM((Avg_Percentage *SensorMessageCounts))/ (ISNULL(SUM(SensorMessageCounts), 1)))
    FROM #Calendar c
    LEFT JOIN #Results_P p on c.SensorID = p.sensorID and c.TheDate = p.Date
    GROUP BY  c.SensorID,c.SensorName,FirstOfWeek, c.Market, c.District
  )
  SELECT
    *
  FROM CTE_Results c
  PIVOT
  (
    SUM (AvgPercentage_Weekly) FOR FirstOfWeek in ( '+ @Cols + '   )
  ) t  
  ORDER BY District, Market, SensorName;';

  EXEC (@query);

--drop table #Calendar
--drop table #Results_p
--drop table #Results
--drop table #PowerTable

