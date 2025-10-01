USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_DeviceInfo]    Script Date: 6/24/2024 2:57:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_DeviceInfo]
  @AccountID INT
AS
/* ----| Device Info Report  |---- */

/*
----| Maintenance Log |----
Created 2017-01-03 ALA
Modified 2017-03-10 NLN (into proc)
*/

/* ----| Sql Script Sensor's |---- */
DECLARE	@OffsetMinutes INT
SELECT @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),tz.TimeZoneIDString ))
FROM dbo.TimeZone tz WITH(NOLOCK)
INNER JOIN dbo.Account a  WITH(NOLOCK) ON a.TimeZoneID = tz.TimeZoneID
WHERE a.AccountID = @AccountID

SELECT 
  a.AccountNumber 'Account',
  a.CompanyName 'Company', 
  c.Name AS 'Network Name',
  s.SensorName AS 'Sensor Name',
  s.SensorID 'Sensor ID', 
  dbo.MonnitUtil_GetCode(s.SensorID) 'Code'
FROM dbo.Sensor s WITH(NOLOCK)
INNER JOIN dbo.CSNet c WITH(NOLOCK) ON s.CSNetID = c.CSNetID
INNER JOIN dbo.Account a WITH(NOLOCK) ON a.AccountID = c.AccountID
WHERE a.AccountIDTree LIKE ('%*' +  CONVERT(VARCHAR,@AccountID) + '*%')
ORDER BY s.AccountID

/* ----| Sql Script Gateway's |---- */
SELECT 
  a.AccountNumber 'Account',
  a.CompanyName 'Company', 
  c.Name AS 'Network Name',
  g.Name AS 'Gateway Name',
  g.GatewayID 'Gateway ID', 
  dbo.MonnitUtil_GetCode(g.GatewayID) 'Code' 
FROM dbo.Gateway g WITH(NOLOCK)
INNER JOIN dbo.CSNet c WITH(NOLOCK) ON c.CSNetID = g.CSNetID
INNER JOIN dbo.Account a WITH(NOLOCK) ON a.AccountID = c.AccountID
WHERE a.AccountIDTree LIKE ('%*' +  CONVERT(VARCHAR,@AccountID) + '*%')
ORDER BY a.AccountID

GO


