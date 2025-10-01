USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_DailyMinMaxAvg_ByHour]    Script Date: 6/24/2024 2:54:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_DailyMinMaxAvg_ByHour]
  @Default_OwnerAccountID BIGINT,
  @CSNetID BIGINT,
  @HourBlockSize INT
AS
/*-----[Daily: Min/Max/Avg By Two Hours]-----*/ 
/*

Script is designed to provide a daily a report of low/high/avg temperatures
in 2 hour block windows for the past day

*/
--Script Variables
DECLARE @OffsetMinutes INT 
DECLARE @TimezoneIdentifier VARCHAR(255)
DECLARE @FromDate DATETIME
DECLARE @ToDate DATETIME
DECLARE @SQL VARCHAR(MAX);

--Required AutoVariables
--DECLARE @Default_OwnerAccountID BIGINT;
--SET     @Default_OwnerAccountID = 1;
--DECLARE @CSNetID BIGINT;
--SET     @CSNetID = 14915;

CREATE TABLE #DMResults
(
  [sensorID]            INT,
  [MessageDate]         DATETIME,
  [Data]                DECIMAL(8,5),
  [State]               INT
)


SELECT @TimezoneIdentifier = tz.TimeZoneIDString 
FROM dbo.TimeZone tz WITH(NOLOCK) 
INNER JOIN dbo.Account a  WITH(NOLOCK) ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID



SET @FromDate = DateAdd(Day, Datediff(Day,0, GETUTCDATE() -1), 0)

SET @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));
SET @FromDate = DATEADD(MINUTE, -@OffsetMinutes, @FromDate)

SET @ToDate   = DATEADD(SECOND, -1, DateAdd(Day, 1, @FromDate))

SET @SQL = '';


SELECT a.CompanyName 'Account', DATEADD(MINUTE, -@OffsetMinutes , GETDATE())'Date'
FROM Account a
WHERE a.AccountID = @Default_OwnerAccountID

--Using dynamic sql to hit only the most recent DataMessage tables in the view
SET @SQL = @SQL + 
'SELECT
  s.[SensorID],
  [MessageDate] = DATEADD(MINUTE, '+ CONVERT(varchar(5), @OffsetMinutes) +', d.[MessageDate]),
  [Data]        = d.data,
  d.[State]
FROM dbo.DataMessage d WITH (NOLOCK)
INNER JOIN dbo.Sensor s WITH (NOLOCK) ON s.[sensorid] = d.[sensorid]
WHERE s.[AccountID] = ' + CONVERT(VARCHAR(10), @Default_OwnerAccountID) + '
  and s.[CSNetID] = ' + CONVERT(VARCHAR(10), @CSNetID) + '
  and s.[ApplicationID] = 2
  and d.[MessageDate] >= ''' + CONVERT(VARCHAR(20), @FromDate) + '''
  and d.[MessageDate] <= ''' +  convert(varchar(20), @todate) + '''
order by s.[sensorid], d.[messagedate] asc'
 

insert into #dmresults
(
  [sensorid],
  [messagedate],
  [data],
  [state]
)
exec (@sql)


/******************************************************
                 daily set results
                      
    -result set in f, not c
    -grouped by sets of 4 hours from the past day
    -gets the high, low and average for each set
    -lists the sensor's current min/max thresholds
*******************************************************/
select
  d.[sensorid],
  s.[sensorname],
  [start]             = min(d.[messagedate]),
  [end]               = max(d.[messagedate]),
  [hightemp]          = convert(decimal(6,1), max(d.[data]) * 9 / 5 + 32),
  [lowtemp]           = convert(decimal(6,1), min(d.[data]) * 9 / 5 + 32),
  [avgtemp]           = convert(decimal(6,1), avg(d.[data]) * 9 / 5 + 32),
  [minimum threshold] = convert(decimal(6,1), (s.[minimumthreshold] / 10.0) * 9 / 5 + 32),
  [maximum threshold] = convert(decimal(6,1), (s.[maximumthreshold] / 10.0) * 9 / 5 + 32)
from #dmresults d
inner join dbo.sensor s with (nolock) on s.sensorid = d.sensorid
where s.CSNetID = @CSNetID
group by 
  d.[sensorid], 
  s.[sensorname], 
  s.[minimumthreshold], 
  s.[maximumthreshold],
  datepart(year, d.[messagedate]),
  datepart(month, d.[messagedate]),
  datepart(day, d.[messagedate]),
  datepart(hour, d.[messagedate])/@HourBlockSize
  order by d.[sensorid], min(d.[messagedate]) asc;


/*********************************************************
                  violations- aware state

  this is the section that returns a violation. a violation
  is whenever the sensor goes into an aware state dictated by
  the field 'state' on the DataMessage table.
**********************************************************/
select 
  d.[sensorid],
  s.[sensorname],
  d.[messagedate],
  [temp]              = convert(decimal(10,1), d.[data] * 9 / 5 + 32),
  [minimum threshold] = convert(decimal(10,1), (s.[minimumthreshold] / 10.0) * 9 / 5 + 32),
  [maximum threshold] = convert(decimal(10,1), (s.[maximumthreshold] / 10.0) * 9 / 5 + 32)
from #dmresults d
inner join dbo.sensor s with (nolock) on s.[sensorid] = d.[sensorid]  
where [state] & 0x02 = 0x02 --determine aware state
AND s.CSNetID = @CSNetID

drop table #dmresults 
GO


