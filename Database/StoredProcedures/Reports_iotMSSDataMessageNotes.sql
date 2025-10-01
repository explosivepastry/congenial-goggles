USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_iotMSSDataMessageNotes]    Script Date: 6/28/2024 4:04:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_iotMSSDataMessageNotes]
  @Default_OwnerAccountID BIGINT
AS

/****************************************************************
   This is a custom report for iotMSS. It's designed to grab
   the last 45 days of notes and get the data and note logged
   for the account and all sub accounts.

   This is currently limited to temp sensors only since we don't
   have a dll file to format the data based on application type.

****************************************************************/

DECLARE @FromDate DATETIME = DATEADD(DAY, -46, GETUTCDATE());
DECLARE @ToDate DATETIME = GETUTCDATE();

DECLARE @MinFrom DATETIME;
DECLARE @MaxTo DATETIME;
DECLARE @SQL VARCHAR(2000);
DECLARE @TimeZoneIDString VARCHAR(50);

SELECT
  @TimeZoneIDString = t.TimeZoneIDString
FROM dbo.[Account] a WITH (NOLOCK)
INNER JOIN dbo.[TimeZone] t on a.TimeZoneID = t.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID

SELECT
  @MinFrom = ISNULL(MIN(MessageDate), @FromDate),
  @MaxTo   = ISNULL(MAX(MessageDate), @FromDate)
FROM dbo.[DataMessageNote] n WITH (NOLOCK)
INNER JOIN dbo.[Sensor] s WITH (NOLOCK) ON n.SensorID = s.SensorID
INNER JOIN dbo.[Account] a WITH (NOLOCK) ON a.AccountID = s.AccountID
WHERE a.AccountIDTree LIKE '%*'+ CONVERT(VARCHAR(30), @Default_OwnerAccountID) + '*%'
  AND s.ApplicationID IN (2,35,46, 65)
  AND n.NoteDate >= @FromDate
  AND n.NoteDate <= @ToDate;

SET @SQL = 
'
SELECT
  a.AccountNumber,
  n.SensorID,
  MessageDate   = dbo.GetLocalTime(n.MessageDate, ''' + @TimeZoneIDString + '''),
  d.Data,
  n.Note,
  NoteDate      = dbo.GetLocalTime(n.NoteDate, ''' + @TimeZoneIDString + '''),
  CustomerName = c.FirstName + '' '' + c.LastName
FROM dbo.[DataMessageNote] n    WITH (NOLOCK)
INNER JOIN dbo.[Sensor] s       WITH (NOLOCK) ON n.SensorID = s.SensorID
INNER JOIN dbo.[DataMessage] d  WITH (NOLOCK) ON n.MessageDate = d.MessageDate AND n.SensorID = d.SensorID AND n.DataMessageGuid = d.DataMessageGUID
INNER JOIN dbo.[Account] a      WITH (NOLOCK) ON a.AccountID = s.AccountID
INNER JOIN dbo.[Customer] c     WITH (NOLOCK) ON c.CustomerID = n.CustomerID
WHERE a.AccountIDTree LIKE ''%*'+ CONVERT(VARCHAR(30), @Default_OwnerAccountID) + '*%''
  AND s.ApplicationID in (2,35,46, 65)
  AND n.NoteDate >= ''' + CONVERT(VARCHAR(30), @FromDate) + '''
  AND n.NoteDate <= ''' + CONVERT(VARCHAR(30), @ToDate) + '''
  AND d.MessageDate >= ''' + CONVERT(VARCHAR(30), @MinFrom) + '''
  AND d.MessageDate <= ''' + CONVERT(VARCHAR(30), @MaxTo) + '''
ORDER BY NoteDate;
'

EXEC (@SQL);


GO


