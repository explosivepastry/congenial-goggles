USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_AccountReport]    Script Date: 6/24/2024 2:34:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Report_AccountReport]
  @Default_OwnerAccountID BIGINT
AS
/*
----| Maintenance Log |----
Created 2013-09-17 Brandon
Modified 2016-07-05 Ala
*/

/*Required AutoVariables*/
--DECLARE @Default_OwnerAccountID INT = 2


/* ----| Sql Script |---- */
SELECT s.SensorID, s.SensorName AS 'Sensor Name', m.ApplicationName AS 'Sensor Type', c.Name AS 'Network Name' 
FROM dbo.Sensor AS s
INNER JOIN dbo.CSNet AS c ON s.CSNetID = c.CSNetID
INNER JOIN dbo.Application AS m ON s.ApplicationID = m.ApplicationID
INNER JOIN dbo.Account AS a ON s.AccountID = a.AccountID
WHERE a.AccountID = @Default_OwnerAccountID
ORDER BY s.AccountID, s.SensorNAme, c.CSNetID

GO


