USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_LinkedAccountsByEmail]    Script Date: 6/28/2024 4:06:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_LinkedAccountsByEmail]
  @Email VARCHAR(300)
AS

IF @email = 'All'
SET @Email = NULL;

WITH cte_results AS 
(
  SELECT 
    UserName,
    c.NotificationEmail,
    c.FirstName,
    c.LastName,
    c.LastLoginDate,
    Type      = 'PrimaryAccount', 
    Ordering  = 1, 
    CustomerID, 
    c.AccountID 
  FROM dbo.[Customer] c
  INNER JOIN dbo.[Account] a ON c.AccountID = a.AccountID 
  WHERE a.AccountIDTree LIKE '%*3660*%'
    AND NotificationEmail = ISNULL(@email , NotificationEmail)
    AND IsDeleted         = 0
  UNION ALL 
  SELECT 
    UserName, 
    c.NotificationEmail,
    c.FirstName,
    c.LastName,
    c.LastLoginDate,
    Type      = 'LinkedAccount', 
    Ordering  = 2, 
    c.CustomerID, 
    cal.AccountID 
  FROM dbo.[Customer] c 
  INNER JOIN dbo.[CustomerAccountLink] cal ON c.CustomerID = cal.CustomerID 
  INNER JOIN dbo.[Account] a ON c.AccountID = a.AccountID 
  WHERE a.AccountIDTree LIKE '%*3660*%'
    AND NotificationEmail   = ISNULL(@email, NotificationEmail)
    AND cal.accountid      != c.AccountID 
    AND c.IsDeleted         = 0
    AND cal.AccountDeleted  = 0
    AND cal.CustomerDeleted = 0
)
SELECT 
  c.UserName, 
  Email = c.NotificationEmail,
  c.FirstName,
  c.LastName,
  c.LastLoginDate,
  c.Type, 
  --c.AccountID, 
  a.AccountNumber, 
  a.CompanyName 
FROM cte_results c
INNER JOIN dbo.[Account] a on c.AccountID = a.AccountID
ORDER BY Ordering, UserName;

GO


