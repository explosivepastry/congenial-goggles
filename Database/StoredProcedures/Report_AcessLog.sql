USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_AccessLog]    Script Date: 6/24/2024 2:31:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_AccessLog]
  @Default_OwnerAccountID BIGINT,
  @FromDate DATETIME,
  @ToDate DATETIME
AS
/* ----| Access Log Report |---- */

/*
----| Maintenance Log |----
Created 2014-03-07 Ala
Modified 2016-07-05 Ala
*/

/*Required AutoVariables*/
--DECLARE @Default_OwnerAccountID INT = 1
--DECLARE @FromDate DATETIME ='06-30-2015 00:00:00'
--DECLARE @ToDate DATETIME='07-01-2016 00:00:00'


/* ----| Sql Script |---- */
SELECT al.LogDate 'Date', al.UserName, al.Application, CASE WHEN al.Success = 1 THEN 'Success' ELSE 'Failure' END 'Result'
FROM dbo.AuthenticationLog al WITH (NOLOCK)
INNER JOIN dbo.Customer c WITH (NOLOCK) 
    ON c.CustomerID = al.CustomerID 
WHERE c.AccountID = @Default_OwnerAccountID
AND LogDate BETWEEN @FromDate AND @ToDate
ORDER BY AuthenticationLogID DESC
OPTION (OPTIMIZE FOR UNKNOWN)

GO


