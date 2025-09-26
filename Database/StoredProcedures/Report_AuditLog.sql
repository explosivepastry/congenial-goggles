USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_AuditLog]    Script Date: 6/24/2024 2:35:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_AuditLog]
  @Default_OwnerAccountID BIGINT,
  @AuditObject INT,
  @ObjectID VARCHAR(255),
  @FromDate DATETIME,
  @ToDate DATETIME
AS

/*Required AutoVariables*/
--Declare @Default_OwnerAccountID BIGINT,
--        @AuditObject            INT,
--        @ObjectID               VARCHAR(255),
--        @FromDate               DATETIME,
--        @ToDate                 DATETIME
--SET @Default_OwnerAccountID = 4
--SET @AuditObject = 1
--SET @ObjectID = -1
--SET @FromDate = '2017-05-01 02:24:42.317'
--SET @ToDate = '2017-08-23 02:24:42.317'

DECLARE @ObjectInt INT;

IF @ObjectID = ''
  SET @ObjectID =  NULL;


--If @ObjectID can't be converted to int, set to null and return all IDs
BEGIN TRY

  SET @ObjectInt = CONVERT(INT, @ObjectID);

END TRY
BEGIN CATCH

  SET @ObjectInt = NULL;

END CATCH

DECLARE @IDLookup TABLE
(
  [ColumnName] VARCHAR(255),
  [ID]         INT,
  [Value]      VARCHAR(255)
);

INSERT INTO @IDLookup ([ColumnName], [ID], [Value])
SELECT
  [ColumnName],
  [TypeID],
  [Value]
FROM dbo.[TypeLookup]
WHERE [TableName] = 'AuditLog';

--Result Set
SELECT
  a.[AuditLogGUID],
  a.[AccountID],
  a.[CustomerID],
  [CustomerName]          = c.[FirstName] + ' ' + c.[LastName],
  [AuditObject]           = t1.[Value],
  a.[ObjectID],
  [AuditAction]           = t2.[value],
  a.[ActionDescription],
  a.[TimeStamp],
  DetailedRecord = a.Record
FROM dbo.[AuditLog] a WITH (NOLOCK)
INNER JOIN dbo.[Customer] c WITH (NOLOCK) ON a.[CustomerID] = c.[CustomerID]
LEFT JOIN @IDLookup t2 ON a.[AuditAction] = t2.[ID] AND t2.[ColumnName] = 'AuditAction'
LEFT JOIN @IDLookup t1 ON a.[AuditObject] = t1.[ID] AND t1.[ColumnName] = 'AuditObject'
WHERE a.[AccountID]   = @Default_OwnerAccountID
  AND a.[AuditObject] = @AuditObject 
  AND a.[ObjectID]    = ISNULL(@ObjectInt, a.ObjectID)
  AND a.[TimeStamp]   >= @FromDate
  AND a.[TimeStamp]   <= @ToDate
ORDER BY
  a.[TimeStamp];
GO


