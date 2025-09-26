USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_RecurringLoggedNotes]    Script Date: 7/1/2024 8:51:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Report_RecurringLoggedNotes]
  @Default_OwnerAccountID BIGINT,
  @Default_ReportScheduleID BIGINT
AS


/****************************************************************
   This is a custom report for iotMSS. It's designed to grab
   the last 45 days of notes and get the data and note logged
   for the account and all sub accounts.

   This is currently limited to temp sensors only since we don't
   have a dll file to format the data based on application type.

****************************************************************/

--declare @Default_ReportScheduleID int = (select top 1 ReportScheduleID From ReportSchedule where ScheduleType = 3)
DECLARE @FromDate DATETIME
DECLARE @ToDate DATETIME
DECLARE @OffsetMinutes INT
DECLARE @TimeZoneIDString VARCHAR(50);
DECLARE @SQL VARCHAR(MAX);

SELECT
  @TimeZoneIDString = t.TimeZoneIDString
FROM dbo.[Account] a WITH (NOLOCK)
INNER JOIN dbo.[TimeZone] t on a.TimeZoneID = t.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID;

SET @OffsetMinutes  = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimeZoneIDString));
SET @ToDate         = CONVERT(DATETIME,CONVERT(VARCHAR,DATEPART(YEAR,DATEADD(MINUTE, @OffSetMinutes, GETUTCDATE()))) +'-'+ CONVERT(VARCHAR,DATEPART(MONTH ,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate())))+'-'+ CONVERT(VARCHAR,DATEPART(DAY,DATEADD(MINUTE, @OffSetMinutes, GetUTCDate()))));

SELECT @FromDate =
      CASE 
         WHEN ScheduleType = 3 THEN DATEADD(DAY,-1,@ToDate)
         WHEN ScheduleType = 2 THEN DATEADD(WEEK,-1,@ToDate)
         WHEN ScheduleType = 1 THEN DATEADD(MONTH,-1,@ToDate)
         WHEN ScheduleType = 0 THEN DATEADD(YEAR,-1,@ToDate)
         ELSE DATEADD(DAY,-1,@ToDate)
      END
   FROM dbo.[ReportSchedule] WITH (NOLOCK)
   WHERE ReportScheduleID = @Default_ReportScheduleID;

   SET @FromDate = dbo.GetUTCTime(@fromDate, @TimeZoneIDString)
   SET @ToDate   = dbo.GetUTCTime(@ToDate, @TimeZoneIDString);

DECLARE @MinFrom DATETIME;
DECLARE @MaxTo DATETIME;



SELECT
  @MinFrom = ISNULL(MIN(MessageDate), @FromDate),
  @MaxTo   = ISNULL(MAX(MessageDate), @FromDate)
FROM dbo.[DataMessageNote] n WITH (NOLOCK)
INNER JOIN dbo.[Sensor] s WITH (NOLOCK) ON n.SensorID = s.SensorID
INNER JOIN dbo.[Account] a WITH (NOLOCK) ON a.AccountID = s.AccountID
WHERE a.AccountIDTree LIKE '%*'+ CONVERT(VARCHAR(30), @Default_OwnerAccountID) + '*%'
  AND n.NoteDate >= @FromDate
  AND n.NoteDate <= @ToDate;

SET @SQL = 
'
SELECT
  a.AccountNumber,
  n.SensorID,
  s.SensorName,
  MessageDate   = dbo.GetLocalTime(n.MessageDate, ''' + @TimeZoneIDString + '''),
  n.Note,
  NoteDate      = dbo.GetLocalTime(n.NoteDate, ''' + @TimeZoneIDString + '''),
  CustomerName = c.FirstName + '' '' + c.LastName
FROM dbo.[DataMessageNote] n    WITH (NOLOCK)
INNER JOIN dbo.[Sensor] s       WITH (NOLOCK) ON n.SensorID = s.SensorID
INNER JOIN dbo.[Account] a      WITH (NOLOCK) ON a.AccountID = s.AccountID
INNER JOIN dbo.[Customer] c     WITH (NOLOCK) ON c.CustomerID = n.CustomerID
WHERE a.AccountID = '+ CONVERT(VARCHAR(30), @Default_OwnerAccountID) + '
  AND n.NoteDate >= ''' + CONVERT(VARCHAR(30), @FromDate) + '''
  AND n.NoteDate <= ''' + CONVERT(VARCHAR(30), @ToDate) + '''
ORDER BY NoteDate;
';

EXEC (@SQL);


GO


