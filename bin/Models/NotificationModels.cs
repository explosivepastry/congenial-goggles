// Decompiled with JetBrains decompiler
// Type: Data.NotificationRecipientData
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Data;

internal class NotificationRecipientData : BaseDBObject
{
  [DBMethod("NotificationRecipientData_SearchPotentialNotificationRecipient")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @RootAccount         BIGINT,\r\n        @RootAccountParent   BIGINT,\r\n        @NotificationAccount BIGINT;\r\n\r\nDECLARE @RecipientList TABLE(\r\n  [AccountID]                       BIGINT,\r\n  [AccountNumber]                   VARCHAR(255),\r\n  [CompanyName]                     VARCHAR(255),\r\n  [CustomerID]                      BIGINT,\r\n  [UserName]                        VARCHAR(255),\r\n  [FirstName]                       VARCHAR(255),\r\n  [LastName]                        VARCHAR(255),\r\n  [NotificationEmail]               VARCHAR(255),\r\n  [SendSensorNotificationToText]    BIT,\r\n  [NotificationPhone]               VARCHAR(255),\r\n  [SendSensorNotificationToVoice]   BIT,\r\n  [NotificationPhone2]              VARCHAR(255),\r\n  [Level]                           INT,\r\n  [EmailActive]                     BIT,\r\n  [EmailDelay]                      INT,\r\n  [TextActive]                      BIT,\r\n  [TextDelay]                       INT,\r\n  [PhoneActive]                     BIT,\r\n  [PhoneDelay]                      INT,\r\n  GroupActive               BIT,\r\n  CustomerGroupID\t\t\t\t\tBIGINT\r\n);\r\n\r\n\r\nSELECT @RootAccount         = [AccountID] FROM dbo.[Customer] WHERE [CustomerID] = @CustomerID;\r\nSELECT @RootAccountParent   = [RetailAccountID] FROM dbo.[Account] WHERE [AccountID] = @RootAccount;\r\nSELECT @NotificationAccount = [AccountID] FROM dbo.[Notification] WHERE [NotificationID] = @NotificationID;\r\n\r\nIF @NotificationAccount IS NULL\r\n  SET @NotificationAccount = @accountid;\r\n\r\nWITH cteAccounts AS\r\n(\r\n  -->>>>>>>>>>Block 1>>>>>>>>>>>>>>>>>\r\n  -- In a rCTE, this block is called an [Anchor]\r\n  -- The query finds all root nodes as described by WHERE ManagerID IS NULL\r\n  SELECT\r\n    *,\r\n    [Level] = 1\r\n  FROM dbo.[Account] WITH (NOLOCK)\r\n  WHERE AccountID = @NotificationAccount\r\n  -->>>>>>>>>>Block 1>>>>>>>>>>>>>>>>>\r\n  UNION ALL\r\n  -->>>>>>>>>>Block 2>>>>>>>>>>>>>>>>>    \r\n  -- This is the recursive expression of the rCTE\r\n  -- On the first 'execution' it will query data in [Account],\r\n  -- relative to the [Anchor] above.\r\n  -- This will produce a resultset, we will call it R{1} and it is JOINed to [cteAccounts]\r\n  -- as defined by the hierarchy\r\n  -- Subsequent 'executions' of this block will reference R{n-1}\r\n  SELECT\r\n    a.*,\r\n    [Level] = [Level]+1\r\n  FROM dbo.[Account] a  WITH (NOLOCK)\r\n  INNER JOIN cteAccounts ctea ON ctea.[RetailAccountID] = a.[AccountID]\r\n  WHERE a.[AccountID] != @RootAccountParent \r\n     OR @RootAccountParent IS NULL\r\n  -->>>>>>>>>>Block 2>>>>>>>>>>>>>>>>>\r\n)\r\nINSERT INTO @RecipientList(\r\n  [AccountID],\r\n  [AccountNumber],\r\n  [CompanyName],\r\n  [CustomerID],\r\n  [UserName],\r\n  [FirstName],\r\n  [LastName],\r\n  [NotificationEmail],\r\n  [SendSensorNotificationToText],\r\n  [NotificationPhone],\r\n  [SendSensorNotificationToVoice],\r\n  [NotificationPhone2],\r\n  [Level],\r\n  GroupActive,\r\n  CustomerGroupID\r\n)\r\nSELECT DISTINCT \r\n  a.[AccountID],\r\n  a.[AccountNumber],\r\n  a.[CompanyName],\r\n  c.[CustomerID],\r\n  c.[UserName],\r\n  c.[FirstName],\r\n  c.[LastName],\r\n  c.[NotificationEmail],\r\n  c.[SendSensorNotificationToText],\r\n  c.[NotificationPhone],\r\n  c.[SendSensorNotificationToVoice],\r\n  c.[NotificationPhone2],\r\n  a.[Level],\r\n  GroupActive = 0,\r\n  customergroupid = null\r\nFROM cteAccounts a\r\nINNER JOIN dbo.[Customer] c              WITH (NOLOCK) ON c.[AccountID] = a.[AccountID]-- OR c.CustomerID = cal.CustomerID\r\nLEFT JOIN dbo.[NotificationRecipient] nr WITH (NOLOCK) ON nr.[NotificationID] = @NotificationID AND nr.[CustomerToNotifyID] = c.CustomerID\r\nWHERE c.[IsDeleted] = 0\r\n  AND (@Query IS NULL \r\n   OR c.[NotificationEmail]              LIKE '%' + @Query + '%'\r\n   OR c.[Username]                       LIKE '%' + @Query + '%'\r\n   OR c.[NotificationPhone]              LIKE '%' + @Query + '%'\r\n   OR c.[FirstName] + ' ' + c.[LastName] LIKE '%' + @Query + '%')\r\n  AND ISNULL(c.NotificationOptIn, '2018-01-01 12:00:00') > ISNULL(c.NotificationOptOut, '1900-01-01 12:00:00')\r\nUNION\t\r\nSELECT\r\n  a.AccountID,\r\n  a.AccountNumber,\r\n  a.CompanyName,\r\n  CustomerID = null,\r\n  Username = cg.name,\r\n  FirstName = cg.name,\r\n  LastName = NULL,\r\n  NotificationEmail = NULL,\r\n  [SendSensorNotificationToText] = NULL,\r\n  [NotificationPhone] = NULL,\r\n  [SendSensorNotificationToVoice] = NULL,\r\n  [NotificationPhone2] = NULL,\r\n  [Level]                           = 0,\r\n  GroupActive = CASE WHEN nr.CustomerGroupID IS NULL then 0 ELSE 1 END,\r\n  cg.customergroupid\r\nFROM CustomerGroup cg\r\nINNER JOIN Account a on cg.accountid = a.accountid\r\nINNER JOIN cteAccounts ct on a.accountID = ct.AccountID\r\nLEFT JOIN NotificationRecipient nr on cg.CustomerGroupID = nr.CustomerGroupID and nr.NotificationID = @NotificationID\r\nWHERE (@Query IS NULL \r\n   OR cg.[Name]              LIKE '%' + @Query + '%')\r\nUNION \r\nSELECT DISTINCT\r\n  a.[AccountID],\r\n  a.[AccountNumber],\r\n  a.[CompanyName],\r\n  c.[CustomerID],\r\n  c.[UserName],\r\n  c.[FirstName],\r\n  c.[LastName],\r\n  c.[NotificationEmail],\r\n  c.[SendSensorNotificationToText],\r\n  c.[NotificationPhone],\r\n  c.[SendSensorNotificationToVoice],\r\n  c.[NotificationPhone2],\r\n  [Level]                           = 1,\r\n  GroupActive = 0,\r\n  customergroupid = null\r\nFROM dbo.[Account] a                     WITH (NOLOCK)\r\nINNER JOIN dbo.[CustomerAccountLink] cal WITH (NOLOCK) ON cal.[AccountID] = a.[AccountID] AND cal.RequestConfirmed = 1 AND cal.[CustomerDeleted] = 0 AND cal.[AccountDeleted] = 0\r\nINNER JOIN dbo.[Customer] c              WITH (NOLOCK) ON c.[AccountID] = a.[AccountID] OR c.[CustomerID] = cal.[CustomerID]\r\nLEFT JOIN dbo.[NotificationRecipient] nr WITH (NOLOCK) ON nr.[NotificationID] = @NotificationID AND nr.[CustomerToNotifyID] = c.[CustomerID]\r\nWHERE a.AccountID = @NotificationAccount\r\n  AND c.[IsDeleted] = 0\r\n  AND (@Query IS NULL \r\n   OR c.[NotificationEmail]              LIKE '%' + @Query + '%'\r\n   OR c.[Username]                       LIKE '%' + @Query + '%'\r\n   OR c.[NotificationPhone]              LIKE '%' + @Query + '%'\r\n   OR c.[FirstName] + ' ' + c.[LastName] LIKE '%' + @Query + '%')\t\r\n  AND ISNULL(c.NotificationOptIn, '2018-01-01 12:00:00') > ISNULL(c.NotificationOptOut, '1900-01-01 12:00:00')\t\r\nORDER BY\r\n  a.[Level],\r\n  a.[CompanyName],\r\n  c.[FirstName],\r\n  c.[LastName];\r\n\r\n\r\nUPDATE r \r\n SET r.[EmailActive] = CASE WHEN NotificationRecipientID IS NULL THEN 0 ELSE 1 END,\r\n r.[EmailDelay] = CASE WHEN NotificationRecipientID IS NULL THEN 0 ELSE ISNULL(nr.DelayMinutes, 0) END\r\nFROM @RecipientList r\r\nLEFT join dbo.[NotificationRecipient] nr  WITH (NOLOCK) on nr.[NotificationID] = @NotificationID AND nr.[CustomerToNotifyID] = r.[CustomerID] AND eNotificationType = 1;\r\n\r\nUPDATE r \r\n SET r.[TextActive] = CASE WHEN NotificationRecipientID IS NULL THEN 0 ELSE 1 END,\r\n r.[TextDelay] = CASE WHEN NotificationRecipientID IS NULL THEN 0 ELSE ISNULL(nr.DelayMinutes, 0) END\r\nFROM @RecipientList r\r\nLEFT join dbo.[NotificationRecipient] nr  WITH (NOLOCK) on nr.[NotificationID] = @NotificationID AND nr.[CustomerToNotifyID] = r.[CustomerID] AND eNotificationType = 2;\r\n\r\nUPDATE r \r\n SET r.[PhoneActive] = CASE WHEN NotificationRecipientID IS NULL THEN 0 ELSE 1 END,\r\n r.[PhoneDelay] = CASE WHEN NotificationRecipientID IS NULL THEN 0 ELSE ISNULL(nr.DelayMinutes, 0) END\r\nFROM @RecipientList r\r\nLEFT join dbo.[NotificationRecipient] nr  WITH (NOLOCK) on nr.[NotificationID] = @NotificationID AND nr.[CustomerToNotifyID] = r.[CustomerID] AND eNotificationType = 6;\r\n\r\nIF @SearchPushMsgRecipient IS NOT NULL AND @SearchPushMsgRecipient = 1\t  \r\n\tSELECT r.* FROM @RecipientList r\r\n\t\tINNER JOIN [dbo].[CustomerPushMessageSubscription] cpms ON cpms.CustomerID = r.CustomerID\r\n\tORDER BY\r\n\t  r.[Level],\r\n\t  r.[CompanyName],\r\n\t  r.[FirstName],\r\n\t  r.[LastName];\r\nELSE\r\n\tSELECT * FROM @RecipientList r\r\n\tORDER BY\r\n\t  r.[Level],\r\n\t  r.[CompanyName],\r\n\t  r.[FirstName],\r\n\t  r.[LastName];\r\n")]
  internal class SearchPotentialNotificationRecipient : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("Query", typeof (string))]
    public string Query { get; private set; }

    [DBMethodParam("SearchPushMsgRecipient", typeof (bool))]
    public bool SearchPushMsgRecipient { get; private set; }

    public List<iMonnit.Models.NotificationRecipientData> Result { get; private set; }

    public SearchPotentialNotificationRecipient(
      long customerID,
      long notificationID,
      string query,
      long accountID = -9223372036854775808 /*0x8000000000000000*/,
      bool searchPushMsgRecipient = false)
    {
      this.CustomerID = customerID;
      this.NotificationID = notificationID;
      this.Query = query;
      this.AccountID = accountID;
      this.SearchPushMsgRecipient = searchPushMsgRecipient;
      this.Result = BaseDBObject.Load<iMonnit.Models.NotificationRecipientData>(this.ToDataTable()).GroupBy<iMonnit.Models.NotificationRecipientData, string>((Func<iMonnit.Models.NotificationRecipientData, string>) (x => x.NotificationRecipientDataID)).Select<IGrouping<string, iMonnit.Models.NotificationRecipientData>, iMonnit.Models.NotificationRecipientData>((Func<IGrouping<string, iMonnit.Models.NotificationRecipientData>, iMonnit.Models.NotificationRecipientData>) (x => x.First<iMonnit.Models.NotificationRecipientData>())).ToList<iMonnit.Models.NotificationRecipientData>();
    }
  }
}
