USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_LoadATTAutoExport_Audit]    Script Date: 6/28/2024 4:08:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_LoadATTAutoExport_Audit] 
	@AccountID BIGINT,
	@AuditDate DateTime
AS

SELECT * 
    FROM ExportStatistics 
    WHERE ExportDate > @AuditDate
    AND ExportDate < DateAdd(day,1,@AuditDate)
	and ExportDate > '2018-06-26 12:29'

GO


