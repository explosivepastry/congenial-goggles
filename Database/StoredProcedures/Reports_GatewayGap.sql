USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_GatewayGap]    Script Date: 6/28/2024 3:55:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[Report_GatewayGap] 
@StartTime date, @EndTime date, @GatewayID INT, @ThresholdSeconds INT
AS

--DECLARE @StartTime date
--DECLARE @EndTime date
--DECLARE @GatewayID INT
--DECLARE @ThresholdSeconds INT

--SET @StartTime='2023-08-01'
--SET @EndTime='2023-08-05'
--SET @GatewayID=1013
--SET @ThresholdSeconds=310

BEGIN
    WITH TempCTE AS ( --Create CTE to hold the data to process.
        SELECT 
            GatewayID, 
            ReceivedDate, 
            LAG(ReceivedDate) OVER (PARTITION BY GatewayID ORDER BY ReceivedDate) AS PrevHB, --LAG is a function that compares the current data with the previous row avoiding the need for a self join.
            DATEDIFF(SECOND, LAG(ReceivedDate) OVER (PARTITION BY GatewayID ORDER BY ReceivedDate), ReceivedDate) AS ActualHBSeconds 
        FROM gatewaymessage WITH (NOLOCK)
        WHERE 
            GatewayID = @GatewayID AND 
            ReceivedDate BETWEEN @StartTime AND @EndTime
    )
    SELECT 
        GatewayID, 
        PrevHB, 
        ReceivedDate, 
        ActualHBSeconds,
        CONVERT(NVARCHAR, (ActualHBSeconds / 60)) + ':' + RIGHT('0' + CONVERT(NVARCHAR, (ActualHBSeconds % 60)), 2) AS ActualHBTime
    FROM TempCTE
    WHERE 
        ActualHBSeconds > @ThresholdSeconds;

    SELECT @@ROWCOUNT AS TotalRowCount;
END;
GO


