// Decompiled with JetBrains decompiler
// Type: Monnit.Data.NotificationRecipient
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class NotificationRecipient
{
  [DBMethod("NotificationRecipient_RemoveCustomer")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE dbo.[NotificationRecipient]\r\nWHERE NotificationID     = @NotificationID\r\n  AND CustomerToNotifyID = @CustomerID;\r\n")]
  internal class RemoveCustomer : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    public RemoveCustomer(long notificationID, long customerID)
    {
      this.NotificationID = notificationID;
      this.CustomerID = customerID;
      this.Execute();
    }
  }

  [DBMethod("NotificationRecipient_LoadByNotificationID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @NotificationAccount BIGINT;\r\nSELECT @NotificationAccount = AccountID FROM dbo.[Notification] WHERE NotificationID = @NotificationID;\r\n\r\nWITH cteAccounts AS\r\n(\r\n  -->>>>>>>>>>Block 1>>>>>>>>>>>>>>>>>\r\n  -- In a rCTE, this block is called an [Anchor]\r\n  -- The query finds all root nodes as described by WHERE ManagerID IS NULL\r\n  SELECT\r\n    *,\r\n    [Level] = 1\r\n  FROM dbo.[Account]\r\n  WHERE [AccountID] = @NotificationAccount\r\n  -->>>>>>>>>>Block 1>>>>>>>>>>>>>>>>>\r\n  UNION ALL\r\n  -->>>>>>>>>>Block 2>>>>>>>>>>>>>>>>>    \r\n  -- This is the recursive expression of the rCTE\r\n  -- On the first 'execution' it will query data in [Account],\r\n  -- relative to the [Anchor] above.\r\n  -- This will produce a resultset, we will call it R{1} and it is JOINed to [cteAccounts]\r\n  -- as defined by the hierarchy\r\n  -- Subsequent 'executions' of this block will reference R{n-1}\r\n  SELECT \r\n    a.*,\r\n    [Level] = [Level]+1\r\n  FROM dbo.[Account] a\r\n  INNER JOIN cteAccounts ctea ON ctea.[RetailAccountID] = a.[AccountID]\r\n  -->>>>>>>>>>Block 2>>>>>>>>>>>>>>>>>\r\n)\r\n\r\n--People to Notify (on Account/ Linked to Account/ Direct assigned from parent account)\r\nSELECT\r\n  nr.* -- For Debuging (Note: if user is in parent account and linked they will show up twice with these added)  a.Level, a.AccountID, a.AccountNumber, a.CompanyName, c.FirstName, c.LastName\r\nFROM cteAccounts a\r\nLEFT JOIN dbo.[CustomerAccountLink] cal   ON cal.[AccountID] = a.[AccountID] AND cal.[RequestConfirmed] = 1 AND cal.[CustomerDeleted] = 0 AND cal.[AccountDeleted] = 0\r\nINNER JOIN dbo.[Customer] c               ON c.[AccountID] = a.[AccountID] OR c.[CustomerID] = cal.[CustomerID]\r\nLEFT JOIN dbo.[Sensor] sens               ON sens.[AccountID] = a.[AccountID]\r\nINNER JOIN dbo.[NotificationRecipient] nr ON nr.[NotificationID] = @NotificationID AND nr.[CustomerToNotifyID] = c.[CustomerID]  \r\nWHERE c.[IsDeleted] = 0\t\r\nUNION\r\n--Devices to notify (Control/AVNotifier) from same account\r\nSELECT \r\n  nr.* -- For Debuging  0, a.AccountID, a.AccountNumber, a.CompanyName, s.SensorName, s.SensorName, \r\nFROM dbo.[NotificationRecipient] nr\r\nINNER JOIN dbo.[Sensor] s         ON s.[SensorID] = nr.[DeviceToNotifyID]\r\nINNER JOIN dbo.[CSNet] net        ON net.[CSNetID] = s.[CSNetID]\r\nINNER JOIN dbo.[Account] a        ON a.[AccountID] = net.[AccountID]\r\nINNER JOIN dbo.[Notification] n   ON n.[NotificationID] = nr.[NotificationID]\r\nWHERE nr.[NotificationID] = @NotificationID\r\n  AND n.[AccountID]       = net.[AccountID]\r\n--ORDER BY a.Level, a.CompanyName, c.FirstName, c.LastName\r\nUNION --CustomerGroups\r\nSELECT\r\n  n.*\r\nFROM cteAccounts c\r\nINNER JOIN CustomerGroup cg on c.AccountID = cg.AccountID\r\nINNER JOIN NotificationRecipient n on cg.CustomerGroupID = n.CustomerGroupID\r\nWHERE n.NotificationID = @NotificationID\r\nUNION  --ActionsToExecute\r\nSELECT \r\n  nr.* \r\nFROM dbo.[NotificationRecipient] nr\r\nWHERE nr.eNotificationType in (12,7)\r\n  AND nr.[NotificationID]    = @NotificationID;\r\n")]
  internal class LoadByNotificationID : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    public List<Monnit.NotificationRecipient> Result { get; private set; }

    public LoadByNotificationID(long notificationID)
    {
      this.NotificationID = notificationID;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecipient>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecipient_LoadVisibleByNotificationID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @RootAccount          BIGINT,\r\n        @RootAccountParent    BIGINT,\r\n        @NotificationAccount  BIGINT;\r\n\r\nSELECT @RootAccount         = [AccountID] FROM dbo.[Customer] WHERE [CustomerID] = @CustomerID;\r\nSELECT @RootAccountParent   = [RetailAccountID] FROM dbo.[Account] WHERE [AccountID] = @RootAccount;\r\nSELECT @NotificationAccount = [AccountID] FROM dbo.[Notification] WHERE [NotificationID] = @NotificationID;\r\n\r\nWITH cteAccounts AS\r\n(\r\n  -->>>>>>>>>>Block 1>>>>>>>>>>>>>>>>>\r\n  -- In a rCTE, this block is called an [Anchor]\r\n  -- The query finds all root nodes as described by WHERE ManagerID IS NULL\r\n  SELECT\r\n    *,\r\n    [Level] = 1\r\n  FROM dbo.[Account]\r\n  WHERE AccountID = @NotificationAccount\r\n  -->>>>>>>>>>Block 1>>>>>>>>>>>>>>>>>\r\n  UNION ALL\r\n  -->>>>>>>>>>Block 2>>>>>>>>>>>>>>>>>    \r\n  -- This is the recursive expression of the rCTE\r\n  -- On the first 'execution' it will query data in [Account],\r\n  -- relative to the [Anchor] above.\r\n  -- This will produce a resultset, we will call it R{1} and it is JOINed to [cteAccounts]\r\n  -- as defined by the hierarchy\r\n  -- Subsequent 'executions' of this block will reference R{n-1}\r\n  SELECT\r\n    a.*,\r\n    [Level] = [Level]+1\r\n  FROM dbo.[Account] a\r\n  INNER JOIN cteAccounts ctea ON ctea.[RetailAccountID] = a.[AccountID]\r\n  WHERE a.[AccountID] != @RootAccountParent OR @RootAccountParent IS NULL\r\n  -->>>>>>>>>>Block 2>>>>>>>>>>>>>>>>>\r\n)      \r\nSELECT DISTINCT\r\n  a.[Level],\r\n  a.[AccountID],\r\n  a.[AccountNumber],\r\n  a.[CompanyName],\r\n  c.*,\r\n  nr.*\r\nFROM cteAccounts a\r\nLEFT JOIN dbo.[CustomerAccountLink] cal   ON cal.[AccountID] = a.[AccountID] AND cal.[RequestConfirmed] = 1 AND cal.[CustomerDeleted] = 0 AND cal.[AccountDeleted] = 0\r\nINNER JOIN dbo.[Customer] c               ON c.[AccountID] = a.[AccountID] OR c.[CustomerID] = cal.[CustomerID]\r\nINNER JOIN dbo.[NotificationRecipient] nr ON nr.[NotificationID] = @NotificationID AND nr.[CustomerToNotifyID] = c.[CustomerID]\r\nWHERE c.[IsDeleted] = 0\r\nORDER BY a.[Level], a.[CompanyName], c.[FirstName], c.[LastName];\r\n")]
  internal class LoadVisibleByNotificationID : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    public List<Monnit.NotificationRecipient> Result { get; private set; }

    public LoadVisibleByNotificationID(long customerID, long notificationID)
    {
      this.CustomerID = customerID;
      this.NotificationID = notificationID;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecipient>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecipient_LoadActionToExecute")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  a.[Name],\r\n  nr.[DelayMinutes],\r\n  nr.[eNotificationType],\r\n  nr.[NotificationID],\r\n  nr.[SerializedRecipientProperties],\r\n  nr.[NotificationRecipientID]\r\nFROM dbo.[ActionToExecute] a\r\nINNER JOIN dbo.[NotificationRecipient] nr ON a.[ActionToExecuteID] = nr.[ActionToExecuteID]\r\nWHERE nr.[NotificationID] = @NotificationID;\r\n")]
  internal class LoadActionToExecute : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    public DataTable Result { get; private set; }

    public LoadActionToExecute(long notificationID)
    {
      this.NotificationID = notificationID;
      this.Result = this.ToDataTable();
    }

    public static DataTable Exec(long notificationID)
    {
      return new NotificationRecipient.LoadActionToExecute(notificationID).Result;
    }
  }

  [DBMethod("NotificationWebHookRecipient_LoadByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  nr.*\r\nFROM dbo.[NotificationRecipient] nr\r\nINNER JOIN dbo.[Notification] n ON nr.NotificationID = n.NotificationID\r\nWHERE n.AccountID         = @AccountID\r\n  AND n.IsDeleted         = 0\r\n  AND n.eNotificationType = 7;\r\n")]
  internal class NotificationWebHookRecipient_LoadByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<Monnit.NotificationRecipient> Result { get; private set; }

    public NotificationWebHookRecipient_LoadByAccountID(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecipient>(this.ToDataTable());
    }
  }
}
