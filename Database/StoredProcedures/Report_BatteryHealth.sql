USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_BatteryHealth]    Script Date: 6/24/2024 2:40:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Report_BatteryHealth]
  @Default_OwnerAccountID BIGINT
AS
/* ----| Battery Health Report |---- */

/*
----| Maintenance Log |----
Created 2013-11-02 Brandon
Modified 2016-07-05 Ala
*/

/*Required AutoVariables*/
--DECLARE @Default_OwnerAccountID INT = 1


/* ----| Sql Script |---- */
SELECT s.SensorID, s.SensorName, dm.Battery, dm.Voltage
FROM dbo.sensor s with (NOLOCK)
INNER JOIN dbo.PowerSource  p with (NOLOCK) ON s.PowerSourceID = p.PowerSourceID
INNER JOIN dbo.DataMessage  dm with (NOLOCK) ON s.LastCommunicationDate = dm.MessageDate AND s.LastDataMessageGUID = dm.DataMessageGUID
WHERE s.AccountID = @Default_OwnerAccountID
ORDER BY s.AccountID, dm.Battery

GO


