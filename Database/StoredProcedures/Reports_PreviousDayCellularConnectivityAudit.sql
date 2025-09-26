USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_PreviousDayCellularConnectivityAudit]    Script Date: 7/1/2024 8:51:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE   PROC [dbo].[Report_PreviousDayCellularConnectivityAudit] AS

--DROP TABLE IF EXISTS ##First
--DROP TABLE IF EXISTS #Base
--DROP TABLE IF EXISTS #Temp
--DROP TABLE If exists #FinalReport

/*
The purpose of this section is to get the raw data which is processed below.  the #First table represents a condensed version of the InboundPacket0X Table.  Using DynamicSQL to always grab the previous day's data.
*/
DECLARE @DayOfWeek INT = DATEPART(WEEKDAY, GETDATE()) - 1; -- 0 for Sunday, 1 for Monday, and so on

DECLARE @DynamicSQL NVARCHAR(MAX);

SET @DynamicSQL = N'
SELECT APNID, Message, ReceivedDate
INTO ##First
FROM InboundPacket0' + CAST(@DayOfWeek AS NVARCHAR(2)) + N' with (nolock)
WHERE Response = 2;';

EXEC sp_executesql @DynamicSQL;

/*
Joining other tables to the smaller dataset to get ultimatley to the message field.
*/
SELECT f.APNID, macaddress, message, F.ReceivedDate
INTO #Base
FROM gateway g with (nolock)
JOIN gatewaymessage gm with (nolock) ON g.GatewayID = gm.GatewayID and GatewayTypeID=30
LEFT JOIN ##First F with (nolock) ON F.APNID=g.GatewayID
--LEFT JOIN InboundPacket00 p with (nolock)  ON p. APNID = g.GatewayID AND p.response = 2 --InboundPacket00=Sunday
WHERE message IS NOT NULL
GROUP BY APNID, macaddress, message, F.ReceivedDate

--Cleanup
--DROP TABLE IF EXISTS ##First

/*
The purpose of this section is to parse out the SIM data FROM requested data FROM the Message table using SUBSTRING.
*/
SELECT
    APNID,
	ReceivedDate, 
	macaddress,
	message,
    CASE --get first 7 characters of the third set of data between vertical bars to extract carrier
		WHEN SUBSTRING(macaddress, CHARINDEX('|', macaddress, CHARINDEX('|', macaddress) + 1) + 1, 7) ='8988307' THEN 'Twillio'
		WHEN SUBSTRING(macaddress, CHARINDEX('|', macaddress, CHARINDEX('|', macaddress) + 1) + 1, 7) ='8914800' THEN 'Verizon'
		WHEN SUBSTRING(macaddress, CHARINDEX('|', macaddress, CHARINDEX('|', macaddress) + 1) + 1, 7) ='8901170' THEN 'ATT'
		ELSE 'OTHER'
	END AS SIM,
	CASE 
		WHEN LEFT(convert(varchar(max), SUBSTRING(message, 39,500)),1)='+'
		THEN convert(varchar(max), SUBSTRING(message, 39,500)) 
		ELSE convert(varchar(max), SUBSTRING(message, 41,500))
	END AS Converted
INTO #Temp
FROM #Base
WHERE CASE 
	WHEN LEFT(convert(varchar(max), SUBSTRING(message, 39,500)),1)='+'
	THEN convert(varchar(max), SUBSTRING(message, 39,500)) 
	ELSE convert(varchar(max), SUBSTRING(message, 41,500))
END NOT LIKE '+COPS: 0|%'

--Cleanup
--DROP TABLE IF EXISTS #Base

/*
Pull the final report using only the items requested. But we have the option to add other fields in if requested.  This is parsing out
the requested items FROM the message hex value in the crossapply statement.  Note we can easily pull other items out if needed in the future.
*/
SELECT 
	APNID AS GWID, 
	ReceivedDate, 
	SIM, 
    MAX(CASE WHEN ItemNumber = 3 THEN Value END) AS Carrier,
	MAX(CASE 
		WHEN ItemNumber = 13 AND Converted like '%+QENG:%' THEN Value
		ELSE NULL
	END) AS Band,
	MAX(CASE 
		WHEN ItemNumber = 23 AND Converted like '%+CGCONTRDP:%' THEN Value
		ELSE NULL
	END) AS [OP-APN],
	CASE
		WHEN Converted LIKE '%+CEREG: 2,5%' THEN 'Yes'
		WHEN Converted LIKE '%+CEREG: 2,1%' THEN 'No'
		ELSE NULL
	END 
	AS Roaming,
	converted
	--,macaddress, message --included these AS a comment in CASE we need to analyze details in the future.
INTO #FinalReport
FROM (
    SELECT 
		APNID, 
		ReceivedDate, 
		sim,
		t.converted, 
        ROW_NUMBER() OVER (PARTITION BY APNID, converted ORDER BY (SELECT NULL)) AS ItemNumber,
        TRIM('"' FROM value) AS Value,
		macaddress, message
    FROM #temp t
    CROSS APPLY STRING_SPLIT(CONVERTED, ',')
) AS SplitValues
GROUP BY APNID, ReceivedDate, SIM,converted,macaddress, message

/*
Report
*/
SELECT * 
FROM #FinalReport

SELECT count(1) AS TotalCountOfSIMS, SIM
FROM #FinalReport
GROUP BY SIM
ORDER BY count(1) DESC

SELECT count(1) AS TotalCountOfCarrier, Carrier
FROM #FinalReport
GROUP BY Carrier
ORDER BY count(1) DESC

SELECT count(1) AS TotalCountOfRoaming, Roaming
FROM #FinalReport
GROUP BY Roaming
ORDER BY count(1) DESC

--Final Cleanup
DROP TABLE IF EXISTS ##First
DROP TABLE IF EXISTS #Base
DROP TABLE IF EXISTS #Temp
DROP TABLE If exists #FinalReport
GO


