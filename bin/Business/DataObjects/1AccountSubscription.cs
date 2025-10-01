// Decompiled with JetBrains decompiler
// Type: Monnit.Data.AccountSubscription
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class AccountSubscription
{
  [DBMethod("AccountSubscription_LoadLastMonth")]
  [DBMethodBody(DBMS.SqlServer, "\r\n-----------Declare Variables Used------------\r\nDECLARE @Notify TABLE(\r\n  AccountSubscriptionID     BIGINT,\r\n  AccountID                 BIGINT,\r\n  eAccountSubscriptionTypeID  INT,\r\n  CSNetID                   BIGINT,\r\n  ExpirationDate            DATETIME,\r\n  AccountSubscriptionTypeID BIGINT,\r\n  LastKeyUsed               UNIQUEIDENTIFIER\r\n);\r\n\r\nDECLARE @EventDescription VARCHAR(255);\r\nSELECT @EventDescription = 'Subscription Month';\r\n\r\n-----------Select Subscriptions------------\r\nINSERT INTO @Notify \r\nSELECT\r\n  s.*\r\nFROM dbo.[AccountSubscription] s\r\nLEFT JOIN (SELECT \r\n             TableID,\r\n             LastEventDate = MAX(EventDate) \r\n           FROM dbo.[ScheduledTaskRecordedEvent]\r\n           WHERE TableName        = 'AccountSubscription'\r\n             AND EventDescription = @EventDescription\r\n           GROUP BY TableID) re ON re.TableID = s.AccountSubscriptionID\r\nWHERE DATEADD(MONTH, -1,  s.ExpirationDate)               < GETUTCDATE() -- Subscription Ends < 1 month\r\n  AND DATEADD(DAY,   -25, s.ExpirationDate)               > GETUTCDATE() -- Subscription Ends > 25 Days\r\n  AND ISNULL(CASE WHEN DATEADD(DAY, 10, re.LastEventDate) > GETUTCDATE() THEN 0 ELSE 1 END, 1) = 1 -- Snooze is NOT in affect (10 days)\r\n  AND s.AccountSubscriptionTypeID                         != 1; --Basic \r\n\r\n-----------Record Notification (SNOOZE)------------\r\nINSERT INTO dbo.[ScheduledTaskRecordedEvent]\r\nSELECT\r\n  AccountSubscriptionID,\r\n  'AccountSubscription',\r\n  @EventDescription,\r\n  GETUTCDATE() \r\nFROM @Notify n\r\n\r\n  /****************************************************************\r\n           If the account has another subscription \r\n\t\t   that expires greater than 1 month, remove\r\n\t\t   this notification from the list.\r\n\r\n  ****************************************************************/\r\n  DELETE n\r\n  FROM @Notify n\r\n  INNER JOIN dbo.[AccountSubscription] s on n.AccountID = s.AccountID\r\n  WHERE DATEADD(MONTH, -1, s.ExpirationDate) >= GETUTCDATE()\r\n    AND s.AccountSubscriptionTypeID != 1;\r\n\r\nSELECT\r\n  n.*\r\nFROM @Notify n\r\nINNER JOIN (SELECT\r\n              AccountID,\r\n              ExpirationDate = MAX(ExpirationDate)\r\n            FROM @Notify \r\n            GROUP BY \r\n             AccountID) a2 on a2.AccountID = n.AccountID and a2.ExpirationDate = n.ExpirationDate;\r\n")]
  internal class LoadLastMonth : BaseDBMethod
  {
    public List<Monnit.AccountSubscription> Result { get; private set; }

    public LoadLastMonth()
    {
      this.Result = BaseDBObject.Load<Monnit.AccountSubscription>(this.ToDataTable());
    }
  }

  [DBMethod("AccountSubscription_LoadLastWeek")]
  [DBMethodBody(DBMS.SqlServer, "\r\n-----------Declare Variables Used------------\r\nDECLARE @Notify TABLE(\r\n  AccountSubscriptionID     BIGINT,\r\n  AccountID                 BIGINT,\r\n  eAccountSubscriptionTypeID  INT,\r\n  CSNetID                   BIGINT,\r\n  ExpirationDate            DATETIME,\r\n  AccountSubscriptionTypeID BIGINT,\r\n  LastKeyUsed               UNIQUEIDENTIFIER\r\n);\r\n\r\nDECLARE @EventDescription VARCHAR(255);\r\nSELECT @EventDescription = 'Subscription Week';\r\n\r\n-----------Select Subscriptions------------\r\nINSERT INTO @Notify \r\nSELECT\r\n  s.*\r\nFROM dbo.[AccountSubscription] s\r\nLEFT JOIN (SELECT \r\n             TableID,\r\n             LastEventDate = MAX(EventDate)\r\n           FROM dbo.[ScheduledTaskRecordedEvent]\r\n           WHERE TableName = 'AccountSubscription' \r\n             AND EventDescription = @EventDescription\r\n           GROUP BY TableID) re on re.TableID = s.AccountSubscriptionID\r\nWHERE DATEADD(Day, -7, s.ExpirationDate)                < GETUTCDATE() -- Subscription Ends < 1 week\r\n  AND s.ExpirationDate                                  > GETUTCDATE() -- Subscription Ends > Now\r\n  AND ISNULL(CASE WHEN DATEADD(Day,10,re.LastEventDate) > GETUTCDATE() THEN 0 ELSE 1 END,1) = 1 -- Snooze is NOT in affect (10 days)\r\n  AND s.AccountSubscriptionTypeID                       != 1; -- Basic Subscription\r\n\r\n\r\n-----------Record Notification (SNOOZE)------------\r\nINSERT INTO dbo.[ScheduledTaskRecordedEvent]\r\nSELECT\r\n  AccountSubscriptionID,\r\n  'AccountSubscription',\r\n  @EventDescription,\r\n  GETUTCDATE()\r\nFROM @Notify n\r\n\r\n\r\n  DELETE n\r\n  FROM @Notify n\r\n  INNER JOIN dbo.[AccountSubscription] s on n.AccountID = s.AccountID\r\n  where DATEADD(DAY, -7, s.ExpirationDate) >= GETUTCDATE()\r\n    AND s.AccountSubscriptionTypeID != 1;\r\n\r\n\r\nSELECT\r\n  n.*\r\nFROM @Notify n\r\nINNER JOIN (SELECT\r\n              AccountID,\r\n              ExpirationDate = MAX(ExpirationDate)\r\n            FROM @Notify \r\n            GROUP BY \r\n             AccountID) a2 on a2.AccountID = n.AccountID and a2.ExpirationDate = n.ExpirationDate;\r\n")]
  internal class LoadLastWeek : BaseDBMethod
  {
    [DBMethodParam("AccountSubscriptionType", typeof (long))]
    public long AccountSubscriptionType { get; private set; }

    public List<Monnit.AccountSubscription> Result { get; private set; }

    public LoadLastWeek()
    {
      this.Result = BaseDBObject.Load<Monnit.AccountSubscription>(this.ToDataTable());
    }
  }

  [DBMethod("AccountSubscription_LoadExpired")]
  [DBMethodBody(DBMS.SqlServer, "\r\n-----------Declare Variables Used------------\r\nDECLARE @Notify TABLE(\r\n  AccountSubscriptionID     BIGINT,\r\n  AccountID                 BIGINT,\r\n  AccountSubscriptionTypeID BIGINT,\r\n  CSNetID                   BIGINT,\r\n  ExpirationDate            DATETIME,\r\n  LastKeyUsed\t\t\t\tUNIQUEIDENTIFIER\r\n);\r\n\r\nDECLARE @EventDescription VARCHAR(255);\r\nSELECT @EventDescription = 'Subscription Expired';\r\n\r\n-----------Select Subscriptions------------\r\nINSERT INTO @Notify \r\nSELECT \r\n  s.AccountSubscriptionID,\r\n  s.AccountID,\r\n  s.AccountSubscriptionTypeID,\r\n  s.CSNetID,\r\n  s.ExpirationDate,\r\n  s.LastKeyUsed\r\nFROM dbo.[AccountSubscription] s\r\nINNER JOIN dbo.[Account] a ON a.AccountID = s.AccountID\r\nLEFT JOIN (SELECT \r\n             TableID,\r\n             LastEventDate = MAX(EventDate)\r\n           FROM dbo.[ScheduledTaskRecordedEvent]\r\n           WHERE TableName        = 'AccountSubscription' \r\n             AND EventDescription = @EventDescription\r\n           GROUP BY TableID) re ON re.TableID = s.AccountSubscriptionID\r\nWHERE s.ExpirationDate                  < GETUTCDATE() -- Subscription Ends < Now\r\n  AND s.ExpirationDate                  > a.CreateDate -- Subscription Ends After Account Created\r\n  AND ISNULL(CASE WHEN re.LastEventDate > s.ExpirationDate THEN 0 ELSE 1 END, 1) = 1 -- Snooze is NOT in affect\r\n  AND s.AccountSubscriptionTypeID       != 1; -- Basic Subscription\r\n\r\n\r\n-----------Record Notification (SNOOZE)------------\r\nINSERT INTO dbo.[ScheduledTaskRecordedEvent]\r\nSELECT\r\n  AccountSubscriptionID,\r\n  'AccountSubscription',\r\n  @EventDescription,\r\n  GETUTCDATE()\r\nFROM @Notify;\r\n\r\n/**********************************************************\r\n    If they have a second nonbasic acountsubscription \r\n    that is not expired, remove the expired record from\r\n    the notify table; no need to notify.\r\n***********************************************************/\r\nDELETE n\r\nFROM @Notify n\r\nINNER JOIN dbo.[AccountSubscription] s ON n.AccountID = s.AccountID\r\nWHERE s.ExpirationDate >= GETUTCDATE()\r\n  AND s.AccountSubscriptionTypeID != 1;\r\n\r\nSELECT * FROM @Notify;\r\n")]
  internal class LoadExpired : BaseDBMethod
  {
    [DBMethodParam("AccountSubscriptionType", typeof (long))]
    public long AccountSubscriptionType { get; private set; }

    public List<Monnit.AccountSubscription> Result { get; private set; }

    public LoadExpired()
    {
      this.Result = BaseDBObject.Load<Monnit.AccountSubscription>(this.ToDataTable());
    }
  }

  [DBMethod("AccountSubscription_LoadAutoDowngrade")]
  [DBMethodBody(DBMS.SqlServer, "\r\n-----------Declare Variables Used------------\r\nDECLARE @Notify TABLE(\r\n  AccountSubscriptionID     BIGINT,\r\n  AccountID                 BIGINT,\r\n  AccountSubscriptionTypeID BIGINT,\r\n  CSNetID                   BIGINT,\r\n  ExpirationDate            DATETIME,\r\n  LastKeyUsed\t\t\t\tUNIQUEIDENTIFIER\r\n);\r\n\r\nDECLARE @EventDescription VARCHAR(255);\r\nSELECT @EventDescription = 'Expired Account Downgraded';\r\n\r\n-----------Select Subscriptions------------\r\nINSERT INTO @Notify \r\nSELECT \r\n  s.AccountSubscriptionID,\r\n  s.AccountID,\r\n  s.AccountSubscriptionTypeID,\r\n  s.CSNetID,\r\n  s.ExpirationDate,\r\n  s.LastKeyUsed\r\nFROM dbo.[AccountSubscription] s\r\nINNER JOIN dbo.[Account] a on a.AccountID = s.AccountID\r\nLEFT JOIN (SELECT\r\n             TableID,\r\n             LastEventDate = MAX(EventDate)\r\n           FROM dbo.[ScheduledTaskRecordedEvent]\r\n           WHERE TableName        = 'AccountSubscription' \r\n             AND EventDescription = @EventDescription \r\n           GROUP BY TableID) re ON re.TableID = s.AccountSubscriptionID\r\nWHERE DATEADD(Day, 15, s.ExpirationDate) < GETUTCDATE() -- Subscription Ends < 15 days ago\r\n  AND s.ExpirationDate                   > a.CreateDate -- Subscription Ends After Account Created\r\n  AND ISNULL(CASE WHEN re.LastEventDate  > s.ExpirationDate THEN 0 ELSE 1 END, 1) = 1 -- Snooze is NOT in affect\r\n  AND s.AccountSubscriptionTypeID         != 1; -- Basic\r\n\r\n-----------Record Notification (SNOOZE)------------\r\nINSERT INTO dbo.[ScheduledTaskRecordedEvent]\r\nSELECT\r\n  AccountSubscriptionID,\r\n  'AccountSubscription',\r\n  @EventDescription,\r\n  GETUTCDATE()\r\nFROM @Notify;\r\n\r\n/*****************************************************************\r\n        If the Account has two nonbasic subscriptions, but one is\r\n\t\tstill either active, not expired for less than 15\r\n\t\tdays, do not add to the notification\r\n\r\n******************************************************************/\r\nDELETE n\r\nFROM @Notify n\r\nINNER JOIN dbo.[AccountSubscription] s on n.AccountID = s.AccountID\r\nWHERE DATEADD(day, 15, s.ExpirationDate) >= GETUTCDATE()\r\n  AND s.AccountSubscriptionTypeID != 1;\r\n\r\nSELECT * FROM @Notify;\r\n")]
  internal class LoadAutoDowngrade : BaseDBMethod
  {
    public List<Monnit.AccountSubscription> Result { get; private set; }

    public LoadAutoDowngrade()
    {
      this.Result = BaseDBObject.Load<Monnit.AccountSubscription>(this.ToDataTable());
    }
  }

  [DBMethod("AccountSubscription_ExpirationReport")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  s.[AccountSubscriptionID],\r\n  a.[AccountID],\r\n  a.[CompanyName],\r\n  [SubscriptionType]            = t.[Name],\r\n  [NetworkName]                 = c.[Name],\r\n  s.[ExpirationDate],\r\n  s.[CSNetID],\r\n  [Count]                       = (SELECT \r\n                                     COUNT([SensorID])\r\n                                   FROM dbo.[Sensor]\r\n                                   WHERE (c.[CSNetID] IS NOT NULL AND [CSNetID] = c.[CSNetID]) \r\n                                      OR (c.[CSNetID] IS NULL AND [AccountID] = a.[AccountID]))\r\nFROM dbo.[AccountSubscription] s\r\nINNER JOIN dbo.[Account] a                 ON a.[AccountID] = s.[AccountID]\r\nLEFT JOIN  dbo.[CSNet] c                   ON c.[CSNetID] = s.[CSNetID]\r\nINNER JOIN dbo.[AccountSubscriptionType] t ON t.AccountSubscriptionTypeID = s.AccountSubscriptionTypeID\r\nWHERE DATEADD(MONTH, -2, s.[ExpirationDate]) < GETUTCDATE() -- Subscription Ends < 2 months\r\n  AND DATEADD(MONTH, 1,   s.[ExpirationDate]) > GETUTCDATE() -- Subscription Ended < 1 month ago\r\n  AND s.[AccountSubscriptionTypeID]           !=  1          -- Basic Subscription \r\nORDER BY \r\n  [ExpirationDate] DESC;\r\n")]
  internal class ExpirationReport : BaseDBMethod
  {
    public DataTable Result { get; private set; }

    public ExpirationReport() => this.Result = this.ToDataTable();
  }

  [DBMethod("AccountSubscription_ResellerExpirationReport")]
  [DBMethodBody(DBMS.SqlServer, "\r\n\t    SELECT \r\n  s.[AccountSubscriptionID],\r\n  a.[AccountID],\r\n  a.[CompanyName],\r\n  [SubscriptionType]            = t.[Name],\r\n  [NetworkName]                 = c.[Name],\r\n  s.[ExpirationDate],\r\n  s.[CSNetID],\r\n  [Count]                       = (SELECT \r\n                                     COUNT([SensorID])\r\n                                   FROM dbo.[Sensor]\r\n                                   WHERE (c.[CSNetID] IS NOT NULL AND [CSNetID] = c.[CSNetID]) \r\n                                      OR (c.[CSNetID] IS NULL AND [AccountID] = a.[AccountID]))\r\nFROM dbo.[AccountSubscription] s\r\nINNER JOIN dbo.[Account] a ON a.[AccountID] = s.[AccountID]\r\nLEFT  JOIN dbo.[CSNet] c   ON c.[CSNetID]   = s.[CSNetID]\r\nINNER JOIN dbo.[AccountSubscriptionType] t ON t.AccountSubscriptionTypeID = s.AccountSubscriptionTypeID\r\nWHERE DATEADD(MONTH, -2, s.[ExpirationDate]) < GETUTCDATE() -- Subscription Ends < 2 months\r\n  AND DATEADD(MONTH, 1,  s.[ExpirationDate]) > GETUTCDATE() -- Subscription Ended < 1 month ago\r\n  AND (a.AccountID                           = @AccountID \r\n    OR a.RetailAccountID                     = @AccountID)\r\n  AND s.[AccountSubscriptionTypeID]          != 1           -- Basic Subscription\r\nORDER BY \r\n  [ExpirationDate] DESC;\r\n")]
  internal class ResellerExpirationReport : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public DataTable Result { get; private set; }

    public ResellerExpirationReport(long resellerAccountID)
    {
      this.AccountID = resellerAccountID;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("AccountSubscription_Missing")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  a.AccountID, \r\n  a.AccountNumber,\r\n  a.CompanyName,\r\n  NetworkName   = c.Name,\r\n  [First Data]  = MIN(d.MessageDate),\r\n  [Last Data]   = MAX(d.MessageDate)\r\nFROM dbo.[CSNet] c\r\nINNER JOIN dbo.[Account] a              ON a.AccountID = c.AccountID\r\nLEFT JOIN dbo.[AccountSubscription] sub ON c.CSNetID = sub.CSNetID AND sub.AccountSubscriptionTypeID = @AccountSubscriptionTypeID\r\nINNER JOIN dbo.[Sensor] s               ON s.CSNetID = c.CSNetID\r\nLEFT JOIN dbo.[DataMessage] d           ON d.SensorID = s.SensorID\r\nWHERE sub.AccountSubscriptionID IS NULL\r\n  AND a.AccountID NOT IN (1,2,3,4,5,7,8,22,23,24,54,93,123,137)\r\nGROUP BY\r\n  a.AccountID,\r\n  a.AccountNumber,\r\n  a.CompanyName,\r\n  c.Name\r\nORDER BY a.AccountID;\r\n")]
  internal class Missing : BaseDBMethod
  {
    [DBMethodParam("AccountSubscriptionTypeID", typeof (long))]
    public long AccountSubscriptionTypeID { get; private set; }

    public DataTable Result { get; private set; }

    public Missing(long accountSubscriptionTypeID)
    {
      this.AccountSubscriptionTypeID = accountSubscriptionTypeID;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("AccountSubscription_LoadAllForReseller")]
  [DBMethodBody(DBMS.SqlServer, "\r\nIF @FromDate IS NULL\r\n  SET @FromDate = '1900-01-01'\r\n\r\nIF @ToDate IS NULL\r\n  SET @ToDate = '2999-01-01'\r\n\r\nDECLARE @RootAccount varchar(25)\r\n  SELECT @RootAccount = '*' + CONVERT(VARCHAR(25),@AccountID) + '*'\r\n\r\n    SELECT\r\n      a.[AccountID],\r\n      a.[AccountNumber],\r\n      a.[CompanyName],\r\n      [ParentCompany]       = SUBSTRING((SELECT\r\n                                          '^' + a.CompanyName AS 'data()'\r\n                                        FROM dbo.Split(RIGHT(a.AccountIDTree, LEN(a.AccountIDTree) - (CHARINDEX('1', a.AccountIDTree)) + 1),'*') s\r\n\t\t\t                                  INNER JOIN Account a on a.AccountID = s.Item\r\n\t\t\t                                  FOR XML PATH('')),2,1000),\r\n      [Status]              = CASE WHEN s.[ExpirationDate] < GETUTCDATE() THEN 'Expired' ELSE 'Current' END,\r\n      [Type]                = t.[Name],\r\n      [SensorCount]         = (SELECT ISNULL(COUNT(*), 0) FROM dbo.[Sensor] s2 WITH (NOLOCK) WHERE s2.[AccountID] = a.[AccountID]),\r\n      a.[CreateDate],\r\n      s.[ExpirationDate]\r\n    FROM dbo.[Account] a\r\n    LEFT JOIN dbo.[AccountSubscription] s ON a.[AccountID] = s.[AccountID]\r\n    LEFT JOIN dbo.[Account] a2            ON a.[RetailAccountID] = a2.[AccountID]\r\n    INNER JOIN dbo.[AccountSubscriptionType] t ON t.AccountSubscriptionTypeID = s.AccountSubscriptionTypeID\r\n    WHERE DATEDIFF(DAY, a.[CreateDate], s.[ExpirationDate]) > 45\r\n      AND s.ExpirationDate BETWEEN @FromDate and @ToDate\r\n      AND s.AccountSubscriptionTypeID                != 1 -- Basic\r\n      AND a.[AccountIDTree]                        LIKE '%*'+CONVERT(VARCHAR(255), @AccountID)+'*%'\r\n      AND (CONVERT(VARCHAR(255), a.[AccountID])       =      a.[AccountID]\r\n        OR a.[AccountNumber]                       LIKE '%' + a.[AccountNumber]  + '%'\r\n        OR a.[CompanyName]                         LIKE '%' + a.[CompanyName] + '%')\r\n    ORDER BY\r\n      s.[ExpirationDate];\r\n")]
  internal class ResellerActivation : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    public DataTable Result { get; private set; }

    public ResellerActivation(DateTime fromDate, DateTime toDate, long acctID)
    {
      this.AccountID = acctID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Result = this.ToDataTable();
    }
  }
}
