USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_SensorCount]    Script Date: 7/1/2024 8:58:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Report_SensorCount]
  @AccountID BIGINT
AS

SET NOCOUNT ON;

/* ----| Sensor Count By SubAccount: Monnit Use Only |---- */

/*****************************************************************
Report pulls a count of Sensors directly associated with account
as well as the count for all sensors associated with all subaccounts

Note: deleted sensors will not be included in the count
      This script (Monnit Use Only) allows an AccountID parameter
      rather than using the Default_OwnerAccountID
******************************************************************/

/*
----| Maintenance Log |----
Date          Name          Note
04/20/2016    Brandon       Created Script
07/05/2016    Ala           Modified Script
10/21/2016    Nathan        Optimized Script and formatted for reading.
03/08/2017    Nathan        Turned script into a proc.
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
--DECLARE        @AccountID      BIGINT = 3420;


CREATE Table #Temp1
(
  [RowID]               INT,
  [AccountID]           BIGINT,
  [Parent]              VARCHAR(255),
  [SensorCount]         BIGINT,
  [RetailAccountID]     BIGINT,
  [AccountIDTree]       VARCHAR(255),
  [OverallCount]        BIGINT
)

CREATE INDEX [IDX_Temp3] ON #Temp1 ([AccountIDTree]);
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
      ISNULL([SensorCount], 0),
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
      ISNULL([SensorCount], 0),
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

UPDATE #Temp1 SET OverallCount = SensorCount

UPDATE t6 SET t6.OverallCount = t5.OverAllCount  FROM #Temp1 t6 INNER JOIN 
(
select OverAllCount = sum(sensorcount), t3.RetailAccountID from #temp1 t2 INNER JOIN (
select distinct retailaccountid from #Temp1
) t3 on t2.AccountIDtree like '%*'+CONVERT(VARCHAR(100), t3.RetailAccountID) +'*%'
group by t3.RetailAccountID ) t5 on t6.AccountID = t5.RetailAccountID


update t set t.Parent = a2.CompanyName from  #temp1 t 
left join Account a2 with (NOLOCK) on t.RetailAccountID = a2.accountid

select t.accountid, a.CompanyName, t.Parent, t.SensorCount, t.RetailAccountID, t.AccountIDTree, t.OverallCount from #Temp1 t
left join Account a with (NOLOCK) on t.accountid = a.accountid
GO


