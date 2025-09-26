USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_CreditLog_LoadUsedHxCreditsByAccountID]    Script Date: 6/24/2024 2:50:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Tyler Barr>
-- Create date: <04_11_2024>
-- Description:	<Shows amount of consumed HX credit in a given time>
-- =============================================
-- Usage:
-- exec [Report_CreditLog_LoadUsedHxCreditsByAccountID] @AccountID = 1, @FromDate = '2020-02-17', @ToDate = '2020-02-20'
CREATE PROCEDURE [dbo].[Report_CreditLog_LoadUsedHxCreditsByAccountID]
	@AccountID BIGINT,
	@FromDate DateTime,
	@ToDate DateTime
AS
BEGIN
	SET NOCOUNT ON;

	SELECT a.AccountNumber, cl.SensorID, IsNull(s.SensorName,'Removed') AS SensorName
		, Convert(varchar, DatePart(Year,cl.MessageDate)) +'-'+ RIGHT(CONCAT('00', DatePart(month , cl.MessageDate)),2)+'-'+ RIGHT(CONCAT('00', DatePart(Day,cl.MessageDate)),2) as UTCDate
		, SUM(Consumed0) + SUM(Consumed1) + SUM(Consumed2) + SUM(Consumed3) + SUM(Consumed4) + SUM(Consumed5) + SUM(Consumed6) + SUM(Consumed7) + SUM(Consumed8) + SUM(Consumed9) + SUM(Consumed10) + SUM(Consumed11)
		+ SUM(Consumed12) + SUM(Consumed13) + SUM(Consumed14) + SUM(Consumed15) + SUM(Consumed16) + SUM(Consumed17) + SUM(Consumed18) + SUM(Consumed19) + SUM(Consumed20) + SUM(Consumed21) + SUM(Consumed22) + SUM(Consumed23) AS Consumed
	FROM CreditLog cl
	INNER JOIN Account a ON cl.AccountID = a.AccountID
	LEFT JOIN Sensor s ON cl.SensorID = s.SensorID and s.AccountID = a.AccountID
	WHERE a.AccountID = @AccountID
		AND cl.MessageDate >= @FromDate
		AND cl.MessageDate <= @ToDate
	GROUP BY a.AccountNumber, cl.SensorID, s.SensorName, cl.MessageDate
	ORDER BY MessageDate, s.SensorName;
END
GO


