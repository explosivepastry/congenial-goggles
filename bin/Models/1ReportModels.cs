// Decompiled with JetBrains decompiler
// Type: Data.ReportRecipientData
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Data;

internal class ReportRecipientData
{
  [DBMethod("Customer_SearchPotentialReportRecipient")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @RootAccount         BIGINT,\r\n        @RootAccountParent   BIGINT,\r\n        @NotificationAccount BIGINT;\r\n\r\nDECLARE @RecipientList TABLE(\r\n  [AccountID]                       BIGINT,\r\n  [AccountNumber]                   VARCHAR(255),\r\n  [CompanyName]                     VARCHAR(255),\r\n  [CustomerID]                      BIGINT,\r\n  [UserName]                        NVARCHAR(255),\r\n  [FirstName]                       NVARCHAR(255),\r\n  [LastName]                        NVARCHAR(255),\r\n  [NotificationEmail]               VARCHAR(255),\r\n  [Level]                           INT\r\n);\r\n\r\nSELECT @RootAccount         = [AccountID] FROM dbo.[Customer] WHERE [CustomerID] = @CustomerID;\r\nSELECT @RootAccountParent   = [RetailAccountID] FROM dbo.[Account] WHERE [AccountID] = @RootAccount;\r\nSELECT @NotificationAccount = [AccountID] FROM dbo.ReportSchedule WHERE ReportScheduleID = @ReportScheduleID;\r\n\r\nIF @NotificationAccount IS NULL\r\n  SET @NotificationAccount = @accountid;\r\n\r\nWITH cteAccounts AS\r\n(\r\n  -->>>>>>>>>>Block 1>>>>>>>>>>>>>>>>>\r\n  -- In a rCTE, this block is called an [Anchor]\r\n  -- The query finds all root nodes as described by WHERE ManagerID IS NULL\r\n  SELECT\r\n    *,\r\n    [Level] = 1\r\n  FROM dbo.[Account] WITH (NOLOCK)\r\n  WHERE AccountID = @NotificationAccount\r\n  -->>>>>>>>>>Block 1>>>>>>>>>>>>>>>>>\r\n  UNION ALL\r\n  -->>>>>>>>>>Block 2>>>>>>>>>>>>>>>>>    \r\n  -- This is the recursive expression of the rCTE\r\n  -- On the first 'execution' it will query data in [Account],\r\n  -- relative to the [Anchor] above.\r\n  -- This will produce a resultset, we will call it R{1} and it is JOINed to [cteAccounts]\r\n  -- as defined by the hierarchy\r\n  -- Subsequent 'executions' of this block will reference R{n-1}\r\n  SELECT\r\n    a.*,\r\n    [Level] = [Level]+1\r\n  FROM dbo.[Account] a  WITH (NOLOCK)\r\n  INNER JOIN cteAccounts ctea ON ctea.[RetailAccountID] = a.[AccountID]\r\n  WHERE a.[AccountID] != @RootAccountParent \r\n     OR @RootAccountParent IS NULL\r\n  -->>>>>>>>>>Block 2>>>>>>>>>>>>>>>>>\r\n)\r\nINSERT INTO @RecipientList(\r\n  [AccountID],\r\n  [AccountNumber],\r\n  [CompanyName],\r\n  [CustomerID],\r\n  [UserName],\r\n  [FirstName],\r\n  [LastName],\r\n  [NotificationEmail],\r\n  [Level]\r\n)\r\nSELECT DISTINCT \r\n  a.[AccountID],\r\n  a.[AccountNumber],\r\n  a.[CompanyName],\r\n  c.[CustomerID],\r\n  c.[UserName],\r\n  c.[FirstName],\r\n  c.[LastName],\r\n  c.[NotificationEmail],\r\n  a.[Level] \r\nFROM cteAccounts a\r\nINNER JOIN dbo.[Customer] c              WITH (NOLOCK) ON c.[AccountID] = a.[AccountID]-- OR c.CustomerID = cal.CustomerID\r\nLEFT JOIN dbo.ReportDistribution rd WITH (NOLOCK) ON rd.ReportScheduleID = @ReportScheduleID  and rd.CustomerID = c.CustomerID\r\nWHERE c.[IsDeleted] = 0\r\n  AND (@Query IS NULL \r\n   OR c.[NotificationEmail]              LIKE '%' + @Query + '%'\r\n   OR c.[Username]                       LIKE '%' + @Query + '%'\r\n   OR c.[NotificationPhone]              LIKE '%' + @Query + '%'\r\n   OR c.[FirstName] + ' ' + c.[LastName] LIKE '%' + @Query + '%')\r\n  AND ISNULL(c.NotificationOptIn, '2018-01-01 12:00:00') > ISNULL(c.NotificationOptOut, '1900-01-01 12:00:00')\r\n\r\nUNION \r\nSELECT DISTINCT\r\n  a.[AccountID],\r\n  a.[AccountNumber],\r\n  a.[CompanyName],\r\n  c.[CustomerID],\r\n  c.[UserName],\r\n  c.[FirstName],\r\n  c.[LastName],\r\n  c.[NotificationEmail],\r\n  [Level]           = 1\r\nFROM dbo.[Account] a                     WITH (NOLOCK)\r\nINNER JOIN dbo.[CustomerAccountLink] cal WITH (NOLOCK) ON cal.[AccountID] = a.[AccountID] AND cal.RequestConfirmed = 1 AND cal.[CustomerDeleted] = 0 AND cal.[AccountDeleted] = 0\r\nINNER JOIN dbo.[Customer] c              WITH (NOLOCK) ON c.[AccountID] = a.[AccountID] OR c.[CustomerID] = cal.[CustomerID]\r\nLEFT JOIN dbo.ReportDistribution rd WITH (NOLOCK) ON rd.ReportScheduleID = @ReportScheduleID  and rd.CustomerID = c.CustomerID\r\nWHERE a.AccountID = @NotificationAccount\r\n  AND c.[IsDeleted] = 0\r\n  AND (@Query IS NULL \r\n   OR c.[NotificationEmail]              LIKE '%' + @Query + '%'\r\n   OR c.[Username]                       LIKE '%' + @Query + '%'\r\n   OR c.[NotificationPhone]              LIKE '%' + @Query + '%'\r\n   OR c.[FirstName] + ' ' + c.[LastName] LIKE '%' + @Query + '%')       \r\n  AND ISNULL(c.NotificationOptIn, '2018-01-01 12:00:00') > ISNULL(c.NotificationOptOut, '1900-01-01 12:00:00')     \r\nORDER BY\r\n  a.[Level],\r\n  a.[CompanyName],\r\n  c.[FirstName],\r\n  c.[LastName];\r\n\r\n\r\n\r\nSELECT * FROM @RecipientList r\r\nORDER BY\r\n  r.[Level],\r\n  r.[CompanyName],\r\n  r.[FirstName],\r\n  r.[LastName];\r\n")]
  internal class SearchPotentialReportRecipient : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("ReportScheduleID", typeof (long))]
    public long ReportScheduleID { get; private set; }

    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("Query", typeof (string))]
    public string Query { get; private set; }

    public List<iMonnit.Models.ReportRecipientData> Result { get; private set; }

    public SearchPotentialReportRecipient(
      long customerID,
      long reportScheduleID,
      string query,
      long accountID = -9223372036854775808 /*0x8000000000000000*/)
    {
      this.CustomerID = customerID;
      this.ReportScheduleID = reportScheduleID;
      this.Query = query;
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<iMonnit.Models.ReportRecipientData>(this.ToDataTable());
    }
  }
}
