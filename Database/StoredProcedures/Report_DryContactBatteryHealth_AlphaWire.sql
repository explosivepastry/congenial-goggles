USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_DryContactBatteryHealth_AlphaWire]    Script Date: 6/24/2024 3:06:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Report_DryContactBatteryHealth_AlphaWire]
  @Default_OwnerAccountID BIGINT,
  @Hour INT
AS

----uncomment for testing
--declare @default_owneraccountid bigint = 1

DECLARE	@HourCount INT,
        @OffsetMinutes_From INT,
        @OffsetMinutes_To INT,
        @Date2 DATETIME = DATEADD(HOUR, @Hour + 1, CONVERT(DATETIME, CONVERT(VARCHAR(10), GETUTCDATE(),120)));

DECLARE @Date1 DATETIME = DATEADD(HOUR, -25, @Date2);

--get the time offset for the account running the report based on the from date
SET @OffsetMinutes_From = (SELECT DATEDIFF (MINUTE, @date1, dbo.GetLocalTime(@Date1, (SELECT
			                                                                                  t.[TimeZoneIDString]
		                                                                                  FROM dbo.[Account] a
		                                                                                  INNER JOIN dbo.[TimeZone] t on a.[TimeZoneID] = t.[TimeZoneID]
		                                                                                  WHERE [AccountID] = @Default_OwnerAccountID))));

SET @Date1     = DATEADD(MINUTE, @OffsetMinutes_From * -1, @Date1);
SET @Date2     = DATEADD(MINUTE, @OffsetMinutes_From * -1, @Date2);
SET @HourCount = DATEDIFF(HOUR, @Date1, @Date2);



/***************************************************************
                      25 Hour Calandar

  Create a 25 hour temp table. This will allow 0 values for 
  any hourly time period that didn't check in by doing a left 
  join onto the data

***************************************************************/
DECLARE @SensorCalander TABLE
(
  [Date] DATETIME,
  [SensorID] BIGINT,
  [SensorName] VARCHAR(255),
  [ApplicationID] BIGINT,
  [LowValue] VARCHAR(255),
  [HighValue] VARCHAR(255)
);

/***************************************************************
                      25 Hour Calandar

  Create a 25 hour temp table. This will allow 0 values for 
  any hourly time period that didn't check in by doing a left 
  join onto the data. This calander ultimately is an hourly
  incrementing table of 25 rows. 

  When we join in the sensors, it will be 25 rows per sensor.

***************************************************************/
WITH CTE_Calender AS
(
  SELECT
    [Date] = @Date1
  UNION ALL
  SELECT
    [Date] = DATEADD(HOUR, 1, [Date])
  FROM CTE_Calender
)
SELECT TOP (@HourCount) --recursive call based on number hours (in case DST)
  *
INTO #Alphawire
FROM CTE_Calender c;

--for ordering purposes, we need the sensor name with the calandar. 
INSERT INTO @SensorCalander ([Date], [SensorID], [SensorName], [ApplicationID], [LowValue], [HighValue])
SELECT
  a.[Date],
  s.[SensorID],
  s.[SensorName],
  s.[ApplicationID],
  [LowValue]  = sa2.Value,
  [HighValue] = sa.Value
FROM #Alphawire a
INNER JOIN dbo.[Sensor] s WITH (NOLOCK) ON s.SensorID = s.SensorID
LEFT JOIN dbo.[SensorAttribute] sa WITH (NOLOCK) ON s.sensorid = sa.SensorID and sa.Name = 'HighValue'
LEFT JOIN dbo.[SensorAttribute] sa2 WITH (NOLOCK) ON s.sensorid = sa2.SensorID and sa2.Name = 'LowValue'
WHERE s.AccountID = @Default_OwnerAccountID
  AND s.ApplicationID in (3,59);

/**********************************************
              FINAL RESULT SET

  Take the 25 hour calander and left join onto
  the hourly grouping of data. Left join will
  allow for 0 values for time periods where
  there were no data messages. 

  Alphawire will have a structure to their 
  sensor names. This will allow a method
  of ordering the way they want. 

  Return the counts of the dry contacts by
  hour and the avg data for battery health
  per hour. If no checkin, then 0.
***********************************************/
SELECT 
  [Date]        = DATEADD(MINUTE, @OffsetMinutes_From, t.[Date]),
  t.[SensorID],
  [Value]       = CONVERT(DECIMAL(8,3), ISNULL(t2.[Value], 0)),
  t.[SensorName],
  [LowValue]    = CASE WHEN t.ApplicationID = 59 THEN ISNULL(t.[LowValue], 0) ELSE NULL END,
  [HighValue]   = CASE WHEN t.ApplicationID = 59 THEN ISNULL(t.[HighValue], 50) ELSE NULL END
FROM @SensorCalander t
LEFT JOIN ( SELECT
              [Value]         = CONVERT(VARCHAR(20), COUNT(*)), 
              [Hour]          = DATEPART(HOUR, DATEADD(MINUTE, @OffsetMinutes_From, [MessageDate])), 
              s.[SensorID], 
              s.[SensorName]
            FROM dbo.[DataMessage] d WITH (NOLOCK)
            INNER JOIN dbo.[Sensor] s WITH (NOLOCK) ON d.[SensorID] = s.[SensorID]
            WHERE s.[AccountID] = @Default_OwnerAccountID
              AND s.[ApplicationID] = 3 --Dry Contact
              AND [MessageDate] BETWEEN @Date1 AND @Date2
              AND d.[Data] = 'True'
            GROUP BY 
              DATEPART(HOUR, DATEADD(MINUTE, @OffsetMinutes_From, MessageDate)), 
              s.[SensorID], 
              s.[SensorName], 
              s.[ApplicationID]
            UNION ALL
            SELECT
              [Value]         = CONVERT(VARCHAR(20), AVG(CONVERT(DECIMAL(8,3), [Data]))), 
              [Hour]          = DATEPART(HOUR, DATEADD(MINUTE, @OffsetMinutes_From, [MessageDate])), 
              s.[SensorID], 
              s.[SensorName]
            FROM dbo.[DataMessage] d WITH (NOLOCK)
            INNER JOIN dbo.[Sensor] s WITH (NOLOCK) ON d.[SensorID] = s.[SensorID]
            WHERE s.[AccountID] = @Default_OwnerAccountID
              AND s.[ApplicationID] = 59 --Battery Health
              AND [MessageDate] BETWEEN @date1 AND @date2
            GROUP BY 
              DATEPART(HOUR, DATEADD(MINUTE, @OffsetMinutes_From, MessageDate)), 
              s.[SensorID], 
              s.[SensorName], 
              s.[ApplicationID]
          ) t2 ON DATEPART(HOUR, DATEADD(MINUTE, @OffsetMinutes_From, t.[Date])) = t2.[Hour] AND t.[SensorID] = t2.[SensorID]
ORDER BY SUBSTRING(t.sensorname, 0, charindex('-', t.sensorname, 0)), t.[ApplicationID], t.[SensorID], DATEADD(Minute, @OffsetMinutes_From, t.[Date]);

drop table #Alphawire;


GO


