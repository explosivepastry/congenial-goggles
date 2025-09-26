USE [iMonnit]
GO
/****** Object:  StoredProcedure [dbo].[Report_AutoPurchaseAccounts]    Script Date: 11/14/2024 10:36:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE Report_AutoPurchaseAccounts
AS
BEGIN
    SELECT 
        AccountID,
        AccountNumber,
        CompanyName,
        PrimaryContactID,
        RetailAccountID,
        AutoPurchase,
        DefaultPaymentID
    FROM 
        Account
    WHERE 
        AutoPurchase > 0;
END;



