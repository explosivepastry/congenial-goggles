USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_ResellerExpiration]    Script Date: 7/1/2024 8:53:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Report_ResellerExpiration]
  @AccountID INT
AS
/* ----| Reseller Expiration Report: Monnit Use Only |---- */

/*****************************************************************
Report pulls a count of Sensors directly associated with account
as well as the count for all sensors associated with all subaccounts
also includes the reseller expiration date

Note: deleted sensors will not be included in the count
      This script (Monnit Use Only) allows an AccountID parameter
      rather than using the Default_OwnerAccountID
******************************************************************/

/*
----| Maintenance Log |----
Date          Name          Note
01/04/2017    ALA           Created Script
03/10/2017    Nathan        Changed script to proc

*/

IF (OBJECT_ID('tempdb..#Temp1') IS NOT NULL)
BEGIN
                DROP TABLE #Temp1;
END

DECLARE @CurrentRow      INT,
        @MaxRow          INT,
        @RetailAccountID INT,
        @ParentName      VARCHAR(255),
        @SumSensor       INT;


CREATE Table #Temp1
(
  [RowID]               INT,
  [AccountID]           BIGINT,
  [Parent]              VARCHAR(255),
  [SensorCount]         BIGINT,
  [RetailAccountID]     BIGINT,
  [AccountIDTree]       VARCHAR(255),
  [OverallCount]        BIGINT,
)

CREATE TABLE #MaxRank
(
  AccountID BIGINT,
  RANK INT
)

CREATE INDEX [IDX_Temp1] ON #Temp1 ([AccountID]);
CREATE INDEX [IDX_Temp2] ON #Temp1 ([RowID]);

SET @CurrentRow = 1;

/*
    If accountID is 1, we need to return all accounts. Some accounts are not properly listed as subaccounts of 1
    so, if accountID that gets passed is 1, we return records for all existing accounts even if not a subaccount of 1
*/
IF @AccountID != 1
BEGIN 

    INSERT INTO #Temp1
    (
      [RowID],
      [AccountID],
      [SensorCount],
      [RetailAccountID],
      [AccountIDTree]
    )
    SELECT
      [RowID] = ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY a.[AccountID]),
      a.[AccountID],
      [SensorCount],
      a.[RetailAccountID],
      a.[AccountIDTree]
    FROM( 
          SELECT
            s.[AccountID],
            [SensorCount] = COUNT(s.[AccountID])
          FROM dbo.Sensor s WITH (NOLOCK) 
          WHERE ISNULL(s.[IsDeleted], 0) = 0
          GROUP BY s.[AccountID] ) s
    RIGHT JOIN dbo.Account a WITH (NOLOCK) ON s.[AccountID] = a.[AccountID]
    WHERE a.[AccountIDTree] LIKE '%*' + CONVERT(VARCHAR(20), @AccountID) +'*%'
    ORDER BY a.[AccountID]

    SET @MaxRow = @@ROWCOUNT;

END ELSE
BEGIN 

    INSERT INTO #Temp1
    (
      [RowID],
      [AccountID],
      [SensorCount],
      [RetailAccountID],
      [AccountIDTree]
    )
    SELECT
      [RowID] = ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY a.[AccountID]),
      a.[AccountID],
      [SensorCount],
      a.[RetailAccountID],
      a.[AccountIDTree]
    FROM( 
          SELECT
            s.[AccountID],
            [SensorCount] = COUNT(s.[AccountID])
          FROM dbo.Sensor s WITH (NOLOCK) 
          WHERE ISNULL(s.[IsDeleted], 0) = 0
          GROUP BY s.[AccountID] ) s
    RIGHT JOIN dbo.Account a WITH (NOLOCK) ON s.[AccountID] = a.[AccountID]
    ORDER BY a.[AccountID]

    SET @MaxRow = @@ROWCOUNT;


END


/*
Looping through records to get the name of the Parent company and the count of all sensor under the account id passed in
*/
--WHILE @CurrentRow <= @MaxRow
--BEGIN

--    SET @AccountID       = (SELECT [AccountID]			FROM #Temp1 WHERE [RowID] = @CurrentRow)
--    SET @RetailAccountID = (SELECT [RetailAccountID]	FROM dbo.Account WITH (NOLOCK) WHERE [AccountID] = @AccountID)
--    SET @ParentName      = (SELECT [CompanyName]		FROM dbo.Account WITH (NOLOCK) WHERE [AccountID] = @RetailAccountID)

--    SET @SumSensor = (SELECT SUM([sensorcount]) FROM #Temp1 WHERE [AccountIDTree] LIKE '%*'+ CONVERT(VARCHAR(20),@AccountID) +'*%')

--    UPDATE #Temp1
--    SET [OverallCount]    = @SumSensor,
--        [Parent]          = @ParentName
    
--	WHERE [AccountID] = @AccountID

--    SET @CurrentRow = @CurrentRow + 1;

--END

    UPDATE #Temp1 SET OverallCount = SensorCount

    UPDATE t6 SET t6.OverallCount = t5.OverAllCount  FROM #Temp1 t6 INNER JOIN 
    (
    select OverAllCount = sum(sensorcount), t3.RetailAccountID from #temp1 t2 INNER JOIN (
    select distinct retailaccountid from #Temp1
    ) t3 on t2.AccountIDtree like '%*'+CONVERT(VARCHAR(100), t3.RetailAccountID) +'*%'
    group by t3.RetailAccountID ) t5 on t6.AccountID = t5.RetailAccountID

    INSERT INTO #MaxRank ([AccountID], [Rank])
    SELECT
      r.[AccountID],
      [Rank]          = MAX(t.[Rank])
    FROM #Temp1 r
    INNER JOIN dbo.[AccountSubscription] sub WITH (NOLOCK) ON sub.[AccountID] = r.[AccountID]
    INNER JOIN dbo.[AccountSubscriptionType] t WITH (NOLOCK) ON sub.[AccountSubscriptionTypeID] = t.[AccountSubscriptionTypeID]
    WHERE (sub.ExpirationDate > GETUTCDATE() AND sub.AccountSubscriptionTypeID != 1)
    GROUP BY r.[AccountID];


    INSERT INTO #MaxRank ([AccountID], [Rank])
    SELECT i.AccountID, max(i.Rank)
    FROM (
          SELECT 
            r.[AccountID],
            [Rank]          = t.[Rank]
          FROM #Temp1 r
          INNER JOIN dbo.[AccountSubscription] sub WITH (NOLOCK) ON sub.[AccountID] = r.[AccountID]
          INNER JOIN dbo.[AccountSubscriptionType] t WITH (NOLOCK) ON sub.[AccountSubscriptionTypeID] = t.[AccountSubscriptionTypeID]
          LEFT JOIN #MaxRank m on r.AccountID = m.AccountID
          WHERE m.AccountID IS NULL
            AND (sub.ExpirationDate < GETUTCDATE() AND sub.AccountSubscriptionTypeID != 1)
          GROUP BY r.[AccountID], t.[Rank], sub.ExpirationDate
          HAVING sub.ExpirationDate = MIN(sub.ExpirationDate)) i
    GROUP BY i.AccountID

    INSERT INTO #MaxRank ([AccountID], [Rank])
    SELECT 
      r.[AccountID],
      [Rank]          = t.[Rank]
    FROM #Temp1 r
    INNER JOIN dbo.[AccountSubscription] sub WITH (NOLOCK) ON sub.[AccountID] = r.[AccountID]
    INNER JOIN dbo.[AccountSubscriptionType] t WITH (NOLOCK) ON sub.[AccountSubscriptionTypeID] = t.[AccountSubscriptionTypeID]
    LEFT JOIN #MaxRank m on r.AccountID = m.AccountID
    WHERE m.AccountID IS NULL
    AND (sub.AccountSubscriptionTypeID = 1);

    SELECT
      r.[AccountID],  
      CompanyName           = ISNULL(a3.[CompanyName], a3.[AccountNumber]),
      [SensorCount]         = ISNULL(r.[SensorCount], 0),
      [OverallCount]        = ISNULL(r.[OverallCount], 0),
      [AccountSubscriptionType] = CASE WHEN t.AccountSubscriptionTypeID = 1 THEN 'Premiere' ELSE ISNULL(t.[Name],'Premiere') END, 
      [SubscriptionExpiration]  = CASE WHEN t.AccountSubscriptionTypeID = 1 THEN (SELECT a.createdate from Account a where a.accountid = r.accountid) ELSE ISNULL(MAX(sub.[ExpirationDate]), a3.[PremiumValidUntil]) END
    FROM #Temp1 r
    left join dbo.Account a3 on r.AccountID = a3.accountid
    LEFT JOIN #MaxRank t2 ON r.[AccountID] = t2.[AccountID]
    LEFT JOIN dbo.AccountSubscription sub WITH (NOLOCK) ON sub.[AccountID] = r.[AccountID]
    LEFT JOIN dbo.AccountSubscriptionType t WITH (NOLOCK) ON t.[AccountSubscriptionTypeID] = sub.[AccountSubscriptionTypeID]
    WHERE (t.[Rank] = t2.[Rank] OR t.[Rank] IS NULL)
    GROUP BY
      r.[AccountID], 
      ISNULL(a3.[CompanyName], a3.[AccountNumber]),
      r.SensorCount,
      r.OverallCount,
      ISNULL(t.[Name],'Premiere'),  
      a3.[PremiumValidUntil],
      t.AccountSubscriptionTypeID,
      r.AccountIDTree
    ORDER BY r.AccountIDTree

drop table #MaxRank
drop table #Temp1
GO


