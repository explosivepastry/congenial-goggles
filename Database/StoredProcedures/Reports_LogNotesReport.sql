USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_LogNotesReport]    Script Date: 6/28/2024 4:13:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Report_LogNotesReport]
  @Default_OwnerAccountID INT,
  @FromDate DATETIME,
  @ToDate DATETIME = NULL
AS
/* ----| Log Notes Report |---- */

/*
----| Maintenance Log |----
Created 2014-07-18 Ala
Modified 2017-03-13 NLN (into Store Proc)
Modified 2018-11-2 NLN Performance Optimization
*/

DECLARE @DMTable Table
(
  [MessageDate] DATETIME,
  [SensorID] BIGINT,
  [DataMessageGUID] UNIQUEIDENTIFIER
)

DECLARE @SQL VARCHAR(2000)
DECLARE @HourOffset INT

SELECT
  @HourOffset = DATEDIFF(HOUR, GETUTCDATE(), dbo.GetLocalTime(GETUTCDATE(), TimeZoneIDString))
FROM Account a WITH (NOLOCK)
INNER JOIN TimeZone t with (NOLOCK) on a.TimeZoneID = t.TimeZoneID
WHERE a.AccountID  = @Default_OwnerAccountID

/* ----| Sql Script |---- */
IF @ToDate IS NULL
  SET @ToDate = GETUTCDATE();


BEGIN
	SELECT @ToDate = DATEADD(MINUTE, 1439, @ToDate)
	SELECT @FromDate = DATEADD(HOUR, @HourOffset*-1, @FromDate)
	SELECT @ToDate = DATEADD(HOUR, @HourOffset*-1, @ToDate)
END

SET @SQL = 
'SELECT
  d.messagedate,
  s.sensorID,
  d.DataMessageGUID
FROM dbo.[Sensor] s WITH (NOLOCK)
INNER JOIN dbo.[DataMessage] d WITH (NOLOCK) on s.SensorID = d.SensorID
WHERE d.MessageDate BETWEEN '''+CONVERT(VARCHAR(30), @FromDate)+ ''' AND ''' + CONVERT(VARCHAR(200), @ToDate) + '''
  AND s.AccountID = ' + CONVERT(VARCHAR(20), @Default_OwnerAccountID)+'';

INSERT INTO @DMTable (MessageDate, SensorID, DataMessageGUID)
EXEC (@SQL);

SELECT 
  MessageDate = DATEADD(HOUR, @HourOffset, dm.[MessageDate]), 
  dm.[SensorID], 
  [USER]          = c.[FirstName]+' '+c.[LastName], 
  NoteDate = DATEADD(HOUR, @HourOffset, dmn.[NoteDate]), 
  dmn.[Note]
FROM @DMTable dm
INNER JOIN dbo.[DataMessageNote] dmn WITH (NOLOCK) ON dmn.DataMessageGUID = dm.DataMessageGUID
INNER JOIN dbo.[Customer] c WITH (NOLOCK) ON c.CustomerID = dmn.CustomerID
ORDER BY dm.MessageDate, dmn.NoteDate;
GO


