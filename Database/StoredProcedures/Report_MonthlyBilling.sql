USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_MonthlyBilling]    Script Date: 11/11/2024 10:59:07 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_MonthlyBilling]
  @CustomerID BIGINT
AS

  DECLARE @Results TABLE
(
  [AccountID]           BIGINT, 
  [AccountNumber]       VARCHAR(255), 
  [CompanyName]         VARCHAR(255), 
  [RetailAccountID]     BIGINT,
  [CustomerID]          BIGINT, 
  [AccountIDTree]       VARCHAR(500),
  [PremiumValidUntil]   DATETIME,
  [NoSuperAccounts]     VARCHAR(255),
  [AutoBill]            BIGINT
);

DECLARE @MaxRank TABLE
(
  [AccountID] BIGINT,
  [Rank]      INT
);

    DECLARE @RootAccount VARCHAR(25);
	
    SELECT @RootAccount = '*' + CONVERT(VARCHAR(25),[AccountID]) + '*' 
    FROM dbo.[Customer] WITH (NOLOCK) 
    WHERE [CustomerID] = @CustomerID;

    -- Update any blank AccountIDTree before searching
    UPDATE dbo.[Account] 
      SET [AccountIDTree] = dbo.MonnitUtil_AccountIDTree([AccountID]) 
    WHERE [AccountIDTree] IS NULL 
       OR [AccountIDTree] = '';

    -- Fetch all accounts under the customer
    INSERT INTO @Results(
      [AccountID], 
      [AccountNumber], 
      [CompanyName],
      [RetailAccountID], 
      [CustomerID], 
      [AccountIDTree],
      [PremiumValidUntil],
      [NoSuperAccounts],
      [AutoBill]
    )    
    SELECT  
      a.[AccountID], 
      a.[AccountNumber], 
      a.[CompanyName], 
      a.[RetailAccountID], 
      c.[CustomerID], 
      a.[AccountIDTree],
      a.[PremiumValidUntil],
      [NoSuperAccounts] = RIGHT(a.[AccountIDTree], LEN(a.[AccountIDTree]) - (CHARINDEX(@RootAccount, a.[AccountIDTree])) + 1),
      a.[AutoBill]
    FROM dbo.[Account] a WITH (NOLOCK)
    INNER JOIN dbo.[Customer] c WITH (NOLOCK) ON c.[CustomerID] = a.[PrimaryContactID]
    WHERE [AccountIDTree] LIKE '%' + @RootAccount + '%';

    
    /**************************************************************************
      Rank and subscription handling for accounts.
    **************************************************************************/
    INSERT INTO @MaxRank ([AccountID], [Rank])
    SELECT
      r.[AccountID],
      [Rank] = MAX(t.[Rank])
    FROM @Results r
    INNER JOIN dbo.[AccountSubscription] sub WITH (NOLOCK) ON sub.[AccountID] = r.[AccountID]
    INNER JOIN dbo.[AccountSubscriptionType] t WITH (NOLOCK) ON sub.[AccountSubscriptionTypeID] = t.[AccountSubscriptionTypeID]
    WHERE (sub.ExpirationDate > GETUTCDATE() AND sub.AccountSubscriptionTypeID != 1)
    GROUP BY r.[AccountID];

    INSERT INTO @MaxRank ([AccountID], [Rank])
    SELECT i.AccountID, MAX(i.Rank)
    FROM (
          SELECT 
            r.[AccountID],
            [Rank] = t.[Rank]
          FROM @Results r
          INNER JOIN dbo.[AccountSubscription] sub WITH (NOLOCK) ON sub.[AccountID] = r.[AccountID]
          INNER JOIN dbo.[AccountSubscriptionType] t WITH (NOLOCK) ON sub.[AccountSubscriptionTypeID] = t.[AccountSubscriptionTypeID]
          LEFT JOIN @MaxRank m ON r.AccountID = m.AccountID
          WHERE m.AccountID IS NULL
            AND (sub.ExpirationDate < GETUTCDATE() AND sub.AccountSubscriptionTypeID != 1)
          GROUP BY r.[AccountID], t.[Rank], sub.ExpirationDate
          HAVING sub.ExpirationDate = MIN(sub.ExpirationDate)) i
    GROUP BY i.AccountID;

    INSERT INTO @MaxRank ([AccountID], [Rank])
    SELECT 
      r.[AccountID],
      [Rank] = t.[Rank]
    FROM @Results r
    INNER JOIN dbo.[AccountSubscription] sub WITH (NOLOCK) ON sub.[AccountID] = r.[AccountID]
    INNER JOIN dbo.[AccountSubscriptionType] t WITH (NOLOCK) ON sub.[AccountSubscriptionTypeID] = t.[AccountSubscriptionTypeID]
    LEFT JOIN @MaxRank m ON r.AccountID = m.AccountID
    WHERE m.AccountID IS NULL
    AND (sub.AccountSubscriptionTypeID = 1);

   /**************************************************************************
                                FINAL RESULT SET
    **************************************************************************/
    WITH CTE_Results AS
    (
      SELECT
        r.[AccountID], 
        r.[AccountNumber], 
        r.[CompanyName], 
        [AccountSubscriptionType] = CASE WHEN t.AccountSubscriptionTypeID = 1 THEN 'Premiere' ELSE ISNULL(t.[Name],'Premiere') END, 
        [SubscriptionExpiration]  = CASE WHEN t.AccountSubscriptionTypeID = 1 THEN (SELECT a.createdate from Account a where a.accountid = r.accountid) ELSE ISNULL(MAX(sub.[ExpirationDate]), r.[PremiumValidUntil]) END,  
        r.[AutoBill]
      FROM @Results r
      LEFT JOIN @MaxRank t2 ON r.[AccountID] = t2.[AccountID]
      LEFT JOIN dbo.[AccountSubscription] sub WITH (NOLOCK) ON sub.[AccountID] = r.[AccountID]
      LEFT JOIN dbo.[AccountSubscriptionType] t WITH (NOLOCK) ON t.[AccountSubscriptionTypeID] = sub.[AccountSubscriptionTypeID]
      WHERE (t.[Rank] = t2.[Rank] OR t.[Rank] IS NULL)
      GROUP BY
        r.[AccountID], 
        r.[AccountNumber], 
        r.[CompanyName],
        ISNULL(t.[Name],'Premiere'),  
        r.[AutoBill],
        r.[PremiumValidUntil],
        t.AccountSubscriptionTypeID 
    )
    SELECT 
      r.[AccountID], 
      r.[AccountNumber], 
      r.[CompanyName], 
      r.[AccountSubscriptionType], 
      r.[SubscriptionExpiration],  
      r.[AutoBill]
    FROM CTE_Results r;
GO


