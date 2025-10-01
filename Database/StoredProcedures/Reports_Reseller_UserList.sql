USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_Reseller_UserList]    Script Date: 7/1/2024 8:52:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Report_Reseller_UserList]
  @Default_OwnerAccountID BIGINT
AS

SELECT 
  ParentAccount,
  AccountID, 
  AccountNumber, 
  UserName, 
  NotificationEmail, 
  Name
FROM (
      SELECT 
        Type = 0, 
        a.AccountID, 
        ParentAccount = NULL, 
        a.AccountNumber,
        c.UserName, 
        c.NotificationEmail,
        Name = c.FirstName + ' '+ c.LastName,
        a.AccountIDTree
      FROM dbo.[Account] a
      INNER JOIN dbo.[customer] c on a.accountid = c.accountid
      WHERE AccountIDTree LIKE '%*'+CONVERT(VARCHAR(20), @Default_OwnerAccountID)+'*%'
        AND a.accountid = @Default_OwnerAccountID
        AND c.IsDeleted != 1
      UNION ALL
      SELECT 
        Type = 1, 
        a.AccountID, 
        parentaccount = a.RetailAccountID, 
        a.AccountNumber,
        c.UserName, 
        c.NotificationEmail,
        Name = c.FirstName + ' '+ c.LastName,
        a.AccountIDTree
      FROM dbo.[Account] a
      INNER JOIN dbo.[customer] c on a.accountid = c.accountid
      WHERE AccountIDTree LIKE '%*'+CONVERT(VARCHAR(20), @Default_OwnerAccountID)+'*%'
        AND a.accountid != @Default_OwnerAccountID
        AND c.IsDeleted != 1
    ) t
ORDER BY
  type, AccountNumber, Username
GO


