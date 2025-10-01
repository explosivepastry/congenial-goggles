// Decompiled with JetBrains decompiler
// Type: Monnit.Data.Account
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class Account
{
  [DBMethod("Account_Remove")]
  [DBMethodBody(DBMS.SqlServer, "\r\n     DECLARE @ChildAccountCheck BIT -- do not delete if reseller or any subaccounts\r\n      IF (EXISTS(SELECT AccountID FROM Account WHERE RetailAccountID = @AccountID))\r\n      BEGIN\r\n        \r\n        SET @ChildAccountCheck = 1\r\n\r\n      END ELSE\r\n      BEGIN\r\n        \r\n        SET @ChildaccountCheck = 0\r\n          \r\n      END\r\n      IF @ChildAccountCheck = 0\r\n      BEGIN\r\n                IF exists (select * from sys.objects where name = 'advancednotification') \r\n                BEGIN \r\n                    update advancednotification\r\n                      set AccountID = 1\r\n                    WHERE AccountID = @AccountID\r\n                END\r\n\r\n                IF exists (select * from sys.objects where name = 'HVACFaultDetectionLog') AND EXISTS (select * from sys.objects where name = 'EquipmentProfile')\r\n                          AND EXISTS (select * from sys.objects where name = 'EquipmentArea')\r\n                BEGIN \r\n                    delete h FROM HVACFaultDetectionLog h\r\n                      INNER JOIN EquipmentProfile e on h.EquipmentProfileID = e.EquipmentProfileID\r\n                      INNER JOIN EquipmentArea ea on ea.EquipmentAreaID = e.EquipmentAreaID\r\n                      WHERE ea.AccountID = @AccountID\r\n                END\r\n\r\n                IF exists (select * from sys.objects where name = 'HVACOverView') AND EXISTS (select * from sys.objects where name = 'EquipmentProfile')\r\n                            AND EXISTS (select * from sys.objects where name = 'EquipmentArea')\r\n                BEGIN \r\n                    delete h FROM HVACOverView h\r\n                      INNER JOIN EquipmentProfile e on h.EquipmentProfileID = e.EquipmentProfileID\r\n                      INNER JOIN EquipmentArea ea on ea.EquipmentAreaID = e.EquipmentAreaID\r\n                      WHERE ea.AccountID = @AccountID\r\n                END\r\n\r\n                IF exists (select * from sys.objects where name = 'EquipmentProfile') \r\n                BEGIN \r\n\t                  delete from EquipmentProfile WHERE EquipmentAreaID IN (Select EquipmentAreaID From EquipmentArea WHERE AccountID = @AccountID)\r\n                END \r\n                IF exists (select * from sys.objects where name = 'EquipmentArea') \r\n                BEGIN \r\n                    delete from EquipmentArea WHERE AccountID = @AccountID\r\n                END\r\n                IF exists (select * from sys.objects where name = 'CSNet') \r\n                BEGIN\r\n                    delete from CSNet where AccountID = @AccountID \r\n                END\r\n\r\n                  UPDATE dbo.[Customer] \r\n                  SET AccountID                          = NULL,\r\n                      Username                           = NEWID(),\r\n                      Password                           = 'Deleted',\r\n                      FirstName                          = 'Deleted',\r\n                      LastName                           = 'User',\r\n                      NotificationEmail                  = 'invalid@invalid.com',\r\n                      NotificationPhone                  = NULL,\r\n                      NotificationPhone2                 = NULL,\r\n                      --SendMaintenanceNotificationToEmail = 0,\r\n                      --SendMaintenanceNotificationToPhone = 0,\r\n                      SendSensorNotificationToText       = 0,\r\n                      SendSensorNotificationToVoice      = 0,                 \r\n                      PasswordExpired                    = 1,\r\n                      IsAdmin                            = 0,\r\n                      IsDeleted                          = 1,\r\n                      IsActive                           = 0,\r\n                      DirectSMS                          = 0,\r\n                      DeleteDate                         = GETUTCDATE(),\r\n                      SamlNameID                         = NULL,\r\n                      ForceLogoutDate                    = GETUTCDATE()\r\n                WHERE AccountID = @AccountID\r\n\r\n                --delete from AcknowledgementRecorded where CustomerID in (select CustomerID from Customer where AccountID = @AccountID) \r\n               -- delete from CustomerPermission where CustomerID in (select CustomerID from Customer where AccountID = @AccountID)\r\n                UPDATE NotificationCredit\r\n                  SET AccountID = NULL,\r\n                      ActivatedByCustomerID = NULL\r\n                WHERE AccountID = @AccountID\r\n                --delete from NotificationRecorded where CustomerID in (select CustomerID from Customer where AccountID = @AccountID)\r\n                --delete from NotificationRecorded_old where CustomerID in (select CustomerID from Customer where AccountID = @AccountID)\r\n                --delete from AuthenticationLog where CustomerID in (select CustomerID from Customer where AccountID = @AccountID) \r\n                --delete from ReleaseNoteViewed where CustomerID in (select CustomerID from Customer where AccountID = @AccountID) \r\n                update Account set PrimaryContactID = 143737, ExternalDataSubscriptionID = null where AccountID = @AccountID \r\n                delete r from ExternalDataSubscriptionResponse r\r\n\t                inner join ExternalDataSubscriptionAttempt a on a.ExternalDataSubscriptionAttemptID = r.ExternalDataSubscriptionAttemptID\r\n\t                inner join ExternalDataSubscription s on s.ExternalDataSubscriptionID = a.ExternalDataSubscriptionID \r\n\t                where s.AccountID = @AccountID\r\n                delete a from ExternalDataSubscriptionAttempt a\r\n\t                inner join ExternalDataSubscription s on s.ExternalDataSubscriptionID = a.ExternalDataSubscriptionID \r\n\t                where s.AccountID = @AccountID\r\n                delete a from ExternalDataSubscriptionProperty a \r\n\t                inner join ExternalDataSubscription s on s.ExternalDataSubscriptionID = a.ExternalDataSubscriptionID \r\n\t                where s.AccountID = @AccountID \r\n                delete from ExternalDataSubscription where AccountID = @AccountID\r\n                --delete from NotificationRecipient where CustomerToNotifyID in (select CustomerID from Customer where AccountID = @AccountID)  \r\n                \r\n                delete NotificationRecipient where CustomerGroupID in (Select CustomerGroupID from  CustomerGroup cg  where cg.AccountID = @AccountID)\r\n\r\n                delete APIKey where AccountID = @AccountID\r\n                \r\n                delete gr FROM CustomerGroupRecipient gr \r\n                INNER JOIN CustomerGroupLink c on gr.CustomerGroupLinkID = c.CustomerGroupLinkID\r\n                INNER JOIN CustomerGroup cg on c.CustomerGroupID = cg.CustomerGroupID \r\n                where cg.AccountID = @AccountID\r\n\r\n                delete gr from CustomerGroupRecipient gr\r\n                inner join Notification n on gr.NotificationID = n.NotificationID\r\n                where n.AccountID = @AccountID\r\n\r\n                PRINT 'SensorGroupSensor'\r\n\r\n                DELETE sensorGroupSensor where SensorGroupID in (SELECT SensorGroupID from SensorGroup WHERE AccountID = @AccountID)\r\n                DELETE SensorGroupGateway where SensorGroupID in (SELECT SensorGroupID from SensorGroup WHERE AccountID = @AccountID)\r\n\r\n                DELETE SensorGroup where AccountID =@AccountID\r\n\r\n                delete c from CustomerGroupLink c INNER JOIN CustomerGroup cg on c.CustomerGroupID = cg.CustomerGroupID where cg.AccountID = @AccountID\r\n                delete from CustomerGroup where AccountID = @AccountID\r\n\r\n                delete CustomerAccountLink where AccountID = @AccountID\r\n\r\n                delete OTARequestSensor where OTARequestID in (SELECT OTARequestID FROM OTARequest WHERE AccountID = @AccountID)\r\n                delete OTARequest WHERE AccountID = @AccountID\r\n                \r\n                delete from NotificationRecipient where NotificationID in (select NotificationID from Notification where AccountID = @AccountID) \r\n                delete from GatewayNotification where NotificationID in (select NotificationID from Notification where AccountID = @AccountID) \r\n                delete from SensorNotification where NotificationID in (select NotificationID from Notification where AccountID = @AccountID) \r\n                delete from AdvancedNotificationParameterValue where NotificationID in (select NotificationID from Notification where AccountID = @AccountID)\r\n                delete from AutomatedNotification where NotificationID in (select NotificationID From Notification where AccountID = @AccountID)\r\n                delete from NotificationRecorded where NotificationID in (select NotificationID From Notification where AccountID = @AccountID)\r\n                delete from NotificationTriggered where NotificationID in (select NotificationID From Notification where AccountID = @AccountID)\r\n                --DELETE from NotificationTriggered where AcknowledgedBy IN (select CustomerID from customer where AccountID = @AccountID)\r\n                delete from Notification where AccountID = @AccountID \r\n               -- delete from CustomerRememberMe where CustomerID in (select CustomerID from Customer where AccountID = @AccountID) \r\n                --delete from AccountSubscriptionChangeLog where CustomerID in (select CustomerID from Customer where AccountID = @AccountID) \r\n                --delete from MaintenanceWindowCustomer where CustomerID in (select CustomerID from Customer where AccountID = @AccountID) \r\n                --delete from NetworkAudit where CustomerID in (select CustomerID from Customer where AccountID = @AccountID) \r\n                --delete from ReportDistribution where CustomerID in (select CustomerID from Customer where AccountID = @AccountID) \r\n                delete from ReportDistribution where ReportScheduleID in (select ReportScheduleID from ReportSchedule where AccountID = @AccountID) \r\n                --delete from ExternalSubscriptionPreference where UserId in (select CustomerID from Customer where AccountID = @AccountID) \r\n                delete from ExternalSubscriptionPreference where AccountID = @AccountID\r\n                delete from CreditSetting where AccountID = @AccountID\r\n                delete from CreditLog where AccountID = @AccountID\r\n                delete from credit where accountid = @accountid\r\n                --delete From CreditSetting where UserId in (select CustomerID from Customer where AccountID = @AccountID) \r\n                --delete FROM CustomerInformation WHERE CustomerID IN (select CustomerID from customer where AccountID = @AccountID)\r\n                --delete from DataMessageNote where CustomerID in (select CustomerID from Customer where AccountID = @AccountID)\r\n\r\n                delete from AuditLog where AccountID = @AccountID  \r\n                delete from SystemHelp where AccountID = @AccountID\r\n\r\n                --delete from Customer where AccountID = @AccountID \r\n                update ReportSchedule set LastReportScheduleResultID = null where AccountID = @AccountID \r\n                delete from ScheduledReportsToStorage where ReportScheduleResultID in (select ReportScheduleResultID from ReportScheduleResult rsr inner join ReportSchedule rs on rs.ReportScheduleID = rsr.ReportScheduleID where rs.AccountID = @AccountID)\r\n                delete from ReportScheduleResult where ReportScheduleID in (select ReportScheduleID from ReportSchedule where AccountID = @AccountID) \r\n                delete from ReportParameterValue where ReportScheduleID in (select ReportScheduleID from ReportSchedule where AccountID = @AccountID) \r\n                delete from ReportSchedule where AccountID = @AccountID \r\n                delete from AccountAddress where AccountID = @AccountID \r\n                delete from Preference where AccountID = @AccountID\r\n                delete from AccountSubscriptionChangeLog where AccountSubscriptionID in (select AccountSubscriptionID from AccountSubscription where AccountID = @AccountID) \r\n                delete from AccountSubscription where AccountID = @AccountID \r\n                delete from AccountPermission where AccountID = @AccountID \r\n                delete from VisualMapSensor where VisualMapID in (select VisualMapID from VisualMap where AccountID = @AccountID) \r\n                delete from VisualMap where AccountID = @AccountID \r\n\t\t\t\tdelete from CustomerFavorite where AccountID = @AccountID \r\n\t\t\t\tdelete from CustomerFavorite where LocationID = @AccountID \r\n            \r\n            IF exists (select * from sys.objects where name = 'OrderItem') AND exists (select * from sys.objects where name = 'OnlineOrder')\r\n            BEGIN\r\n                delete from OrderItem where OrderID in (select orderID from OnlineOrder where AccountID = @AccountID) \r\n            END\r\n            IF exists (select * from sys.objects where name = 'Payment') AND exists (select * from sys.objects where name = 'OnlineOrder')\r\n            BEGIN\r\n                delete from Payment where OrderID in (select orderID from OnlineOrder where AccountID = @AccountID)\r\n            END\r\n            IF exists (select * from sys.objects where name = 'OnlineOrder') \r\n            BEGIN\r\n                delete from OnlineOrder where AccountID = @AccountID \r\n            END\r\n                delete from Account where AccountID = @AccountID\r\n\r\n      END\r\n")]
  internal class Remove : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public bool Result { get; private set; }

    public Remove(long accountID)
    {
      try
      {
        this.AccountID = accountID;
        this.Execute();
        this.Result = true;
      }
      catch (Exception ex)
      {
        ExceptionLog.Log(ex);
        this.Result = false;
      }
    }
  }

  [DBMethod("Account_Search")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP (@Limit) \r\n  a.* \r\nFROM dbo.[Account] a\r\nINNER JOIN dbo.[Customer] c ON c.CustomerID = a.PrimaryContactID\r\nWHERE (a.RetailAccountID = @ResellerID \r\n   OR  @ResellerID IS NULL)\r\n  AND (@Query IS NULL \r\n   OR  CONVERT(VARCHAR(25),a.AccountID) LIKE '%' + @Query + '%'\r\n   OR  a.AccountNumber                  LIKE '%' + @Query + '%'\r\n   OR  a.CompanyName                    LIKE '%' + @Query + '%'\r\n   OR  c.FirstName + ' ' + c.LastName   LIKE '%' + @Query + '%');\r\n")]
  internal class Search : BaseDBMethod
  {
    [DBMethodParam("ResellerID", typeof (long))]
    public long ResellerID { get; private set; }

    [DBMethodParam("Query", typeof (string))]
    public string Query { get; private set; }

    [DBMethodParam("Limit", typeof (int), DefaultValue = 100)]
    public int Limit { get; private set; }

    public List<Monnit.Account> Result { get; private set; }

    public Search(long resellerID, string query, int limit)
    {
      this.ResellerID = resellerID;
      this.Query = query;
      this.Limit = limit;
      this.Result = BaseDBObject.Load<Monnit.Account>(this.ToDataTable());
    }
  }

  [DBMethod("Account_ModelSearch")]
  [DBMethodBody(DBMS.SqlServer, "\r\n  DECLARE @Results TABLE\r\n(\r\n  [AccountID]           BIGINT, \r\n  [AccountNumber]       VARCHAR(255), \r\n  [CompanyName]         VARCHAR(255), \r\n  [PrimaryContactID]    BIGINT, \r\n  [RetailAccountID]     BIGINT, \r\n  [CustomerID]          BIGINT, \r\n  [UserName]            NVARCHAR(255), \r\n  [FirstName]           NVARCHAR(255), \r\n  [LastName]            NVARCHAR(255), \r\n  [NotificationEmail]   VARCHAR(255),\r\n\t[AccountIDTree]       VARCHAR(500),\r\n  [PremiumValidUntil]   DATETIME,\r\n  [NoSuperAccounts]     VARCHAR(255)\r\n);\r\n\r\nDECLARE @MaxRank TABLE\r\n(\r\n  [AccountID] BIGINT,\r\n  [Rank]      INT\r\n);\r\n\r\n    DECLARE @RootAccount VARCHAR(25);\r\n\t\r\n    SELECT @RootAccount = '*' + CONVERT(VARCHAR(25),[AccountID]) + '*' FROM dbo.[Customer] WITH (NOLOCK) WHERE [CustomerID] = @CustomerID;\r\n\r\n    --Update any blank accountidtree before searching\r\n\t  UPDATE dbo.[Account] \r\n      SET [AccountIDTree] = dbo.MonnitUtil_AccountIDTree([AccountID]) \r\n    WHERE [AccountIDTree] IS NULL \r\n       OR [AccountIDTree] = '';\r\n\r\n    --Search for accounts under customer related to search term\r\n    INSERT INTO @Results(\r\n      [AccountID], \r\n      [AccountNumber], \r\n      [CompanyName], \r\n      [PrimaryContactiD], \r\n      [RetailAccountID], \r\n      [CustomerID], \r\n      [UserName], \r\n      [FirstName], \r\n      [LastName], \r\n      [NotificationEmail],\r\n\t\t  [AccountIDTree],\r\n      [PremiumValidUntil],\r\n      [NoSuperAccounts]\r\n    )    \r\n    SELECT TOP (@Limit) \r\n      a.[AccountID], \r\n      a.[AccountNumber], \r\n      a.[CompanyName], \r\n      a.[PrimaryContactiD], \r\n      a.[RetailAccountID], \r\n      c.[CustomerID], \r\n      c.[UserName], \r\n      c.[FirstName], \r\n      c.[LastName], \r\n      c.[NotificationEmail],\r\n\t\t  a.[AccountIDTree],\r\n      a.[PremiumValidUntil],\r\n      [NoSuperAccounts]       = RIGHT(a.[AccountIDTree], LEN(a.[AccountIDTree]) - (CHARINDEX(@RootAccount, a.[AccountIDTree])) + 1)\r\n    FROM dbo.[Account] a WITH (NOLOCK)\r\n    INNER JOIN dbo.[Customer] c WITH (NOLOCK) ON c.[CustomerID] = a.[PrimaryContactID]\r\n    WHERE (@Query IS NULL \r\n            OR CONVERT(VARCHAR(25),a.[AccountID])          = @Query\r\n            OR a.[AccountNumber]                  LIKE '%' + @Query + '%'\r\n            OR a.[CompanyName]                    LIKE '%' + @Query + '%'\r\n            OR c.[FirstName] + ' ' + c.[LastName] LIKE '%' + @Query + '%'\r\n           )\r\n      AND [AccountIDTree] LIKE '%' + @RootAccount + '%';\r\n\r\n    \r\n    /**************************************************************************\r\n\r\n      Return in priority order:\r\n      1) Unexpired subscription with highest rank or\r\n      2) Most recently expired basic account or\r\n      3) The basic account if no other subscriptions exist\r\n\r\n    **************************************************************************/\r\n    INSERT INTO @MaxRank ([AccountID], [Rank])\r\n    SELECT\r\n      r.[AccountID],\r\n      [Rank]          = MAX(t.[Rank])\r\n    FROM @Results r\r\n    INNER JOIN dbo.[AccountSubscription] sub WITH (NOLOCK) ON sub.[AccountID] = r.[AccountID]\r\n    INNER JOIN dbo.[AccountSubscriptionType] t WITH (NOLOCK) ON sub.[AccountSubscriptionTypeID] = t.[AccountSubscriptionTypeID]\r\n    WHERE (sub.ExpirationDate > GETUTCDATE() AND sub.AccountSubscriptionTypeID != 1)\r\n    GROUP BY r.[AccountID];\r\n\r\n    INSERT INTO @MaxRank ([AccountID], [Rank])\r\n    SELECT i.AccountID, max(i.Rank)\r\n    FROM (\r\n          SELECT \r\n            r.[AccountID],\r\n            [Rank]          = t.[Rank]\r\n          FROM @Results r\r\n          INNER JOIN dbo.[AccountSubscription] sub WITH (NOLOCK) ON sub.[AccountID] = r.[AccountID]\r\n          INNER JOIN dbo.[AccountSubscriptionType] t WITH (NOLOCK) ON sub.[AccountSubscriptionTypeID] = t.[AccountSubscriptionTypeID]\r\n          LEFT JOIN @MaxRank m on r.AccountID = m.AccountID\r\n          WHERE m.AccountID IS NULL\r\n            AND (sub.ExpirationDate < GETUTCDATE() AND sub.AccountSubscriptionTypeID != 1)\r\n          GROUP BY r.[AccountID], t.[Rank], sub.ExpirationDate\r\n          HAVING sub.ExpirationDate = MIN(sub.ExpirationDate)) i\r\n    GROUP BY i.AccountID\r\n\r\n    INSERT INTO @MaxRank ([AccountID], [Rank])\r\n    SELECT \r\n      r.[AccountID],\r\n      [Rank]          = t.[Rank]\r\n    FROM @Results r\r\n    INNER JOIN dbo.[AccountSubscription] sub WITH (NOLOCK) ON sub.[AccountID] = r.[AccountID]\r\n    INNER JOIN dbo.[AccountSubscriptionType] t WITH (NOLOCK) ON sub.[AccountSubscriptionTypeID] = t.[AccountSubscriptionTypeID]\r\n    LEFT JOIN @MaxRank m on r.AccountID = m.AccountID\r\n    WHERE m.AccountID IS NULL\r\n    AND (sub.AccountSubscriptionTypeID = 1);\r\n\r\n   /**************************************************************************\r\n    --------------------------------------------------------------------------\r\n                                FINAL RESULT SET\r\n\r\n    Use CTE table to handle cases where the account has two subscriptions of\r\n    the same type and we need to return the max expiration date. \r\n\r\n    This *shouldn't* happen, but in the off chance it does, this will fix it.\r\n    __________________________________________________________________________\r\n    **************************************************************************/\r\n    WITH CTE_Results AS\r\n    (\r\n      SELECT\r\n        r.[AccountID], \r\n        r.[AccountNumber], \r\n        r.[CompanyName], \r\n        r.[PrimaryContactID], \r\n        r.[RetailAccountID],\r\n        [AccountSubscriptionType] = CASE WHEN t.AccountSubscriptionTypeID = 1 THEN 'Premiere' ELSE ISNULL(t.[Name],'Premiere') END, \r\n        [SubscriptionExpiration]  = CASE WHEN t.AccountSubscriptionTypeID = 1 THEN (SELECT a.createdate from Account a where a.accountid = r.accountid) ELSE ISNULL(MAX(sub.[ExpirationDate]), r.[PremiumValidUntil]) END,  \r\n        r.[CustomerID], \r\n        r.[UserName], \r\n        r.[FirstName], \r\n        r.[LastName], \r\n        r.[NotificationEmail],\r\n\t\t    r.[AccountIDTree],\r\n        r.[NoSuperAccounts]\r\n      FROM @Results r\r\n      LEFT JOIN @MaxRank t2 ON r.[AccountID] = t2.[AccountID]\r\n      LEFT JOIN dbo.AccountSubscription sub WITH (NOLOCK) ON sub.[AccountID] = r.[AccountID]\r\n      LEFT JOIN dbo.AccountSubscriptionType t WITH (NOLOCK) ON t.[AccountSubscriptionTypeID] = sub.[AccountSubscriptionTypeID]\r\n      WHERE (t.[Rank] = t2.[Rank] OR t.[Rank] IS NULL)\r\n      GROUP BY\r\n        r.[AccountID], \r\n        r.[AccountNumber], \r\n        r.[CompanyName], \r\n        r.[PrimaryContactID], \r\n        r.[RetailAccountID],\r\n        ISNULL(t.[Name],'Premiere'),  \r\n        r.[CustomerID], \r\n        r.[UserName], \r\n        r.[FirstName], \r\n        r.[LastName], \r\n        r.[NotificationEmail],\r\n\t\t    r.[AccountIDTree],\r\n        r.[NoSuperAccounts],\r\n        r.[PremiumValidUntil],\r\n        t.AccountSubscriptionTypeID \r\n    )\r\n    select \r\n      r.[AccountID], \r\n      r.[AccountNumber], \r\n      r.[CompanyName], \r\n      r.[PrimaryContactID], \r\n      r.[RetailAccountID],\r\n      r.[AccountSubscriptionType], \r\n      r.[SubscriptionExpiration],  \r\n      r.[CustomerID], \r\n      r.[UserName], \r\n      r.[FirstName], \r\n      r.[LastName], \r\n      r.[NotificationEmail],\r\n      r.[AccountIDTree],\r\n      r.[NoSuperAccounts],\r\n      [Tree]                    = SUBSTRING((SELECT \r\n                                      '^' + a.[CompanyName] AS 'data()' \r\n                                  FROM dbo.[Split](RIGHT(r.[AccountIDTree], LEN(r.[AccountIDTree]) - (CHARINDEX(@RootAccount, r.[AccountIDTree])) + 1),'*') s\r\n                                  INNER JOIN dbo.[Account] a WITH (NOLOCK) ON a.[AccountID] = s.[Item]\r\n                                  FOR XML PATH('')),2,1000)\r\n     from CTE_Results r;\r\n")]
  internal class ModelSearch : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("Query", typeof (string))]
    public string Query { get; private set; }

    [DBMethodParam("Limit", typeof (int), DefaultValue = 100)]
    public int Limit { get; private set; }

    public DataTable Result { get; private set; }

    public ModelSearch(long customerID, string query, int limit)
    {
      this.CustomerID = customerID;
      this.Query = query;
      this.Limit = limit;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("Account_UpdateAccountTree")]
  [DBMethodBody(DBMS.SqlServer, "\r\nUPDATE dbo.[Account]\r\n  SET AccountIDTree = dbo.[MonnitUtil_AccountIDTree](AccountID)\r\nWHERE AccountIDTree LIKE '%*' + CONVERT(VARCHAR, @AccountID) + '*%';\r\n")]
  internal class UpdateAccountTree : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public DataTable Result { get; private set; }

    public UpdateAccountTree(long accountID)
    {
      this.AccountID = accountID;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("Account_LocationOverview")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @AccountLevel BIGINT;\r\nDECLARE @RowCount BIGINT;\r\n\r\nCREATE TABLE #LocationOverview\r\n(\r\n  RowID               INT,\r\n  AccountID           BIGINT,\r\n  AccountNumber       VARCHAR(255),\r\n  RetailAccountID     BIGINT,\r\n  SensorCount         INT,\r\n  OnlineCount         INT,\r\n  OfflineCount        INT,\r\n  SensorLowSig        INT,\r\n  SensorLowBatt       INT,\r\n  AlertCounts         INT,\r\n  AwareCounts         INT,\r\n  GatewayCount        INT,\r\n  GatewayOnline       INT,\r\n  GatewayOffline      INT,\r\n  SubAccountLevel     INT,\r\n  SubAccountCount     INT,\r\n  AccountLevelIssues  INT,\r\n  RetailLevelIssues   INT,\r\n  NotNormal           BIT,\r\n  PremiereMonths      INT\r\n);\r\n\r\n----Commenting out @ViewAll functionality for future use\r\n--DELCARE @ViewAll BIGINT = 1\r\n--IF @ViewAll = 0\r\n--BEGIN\r\n--  SET @ViewAll = @AccountID\r\n--END ELSE\r\n--BEGIN\r\n--  SET @ViewAll = NULL\r\n--END\r\n\r\nSELECT \r\n  a.AccountID, \r\n  AccountNumber, \r\n  RetailAccountID, \r\n  AccountIDTree,\r\n  PremiereExpiration  = MAX(s.ExpirationDate),\r\n  AccountIDTree2      = REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE (AccountIDTree, \r\n                        '0', ''), '1', ''),'2', ''),'3', ''),'4', ''),'5', ''),'6', ''),'7', ''),'8', ''),'9', '')\r\nINTO #AccountList \r\nFROM dbo.[Account] a WITH (NOLOCK)\r\nLEFT JOIN dbo.[AccountSubscription] s  WITH (NOLOCK) ON a.AccountID = s.AccountID AND AccountSubscriptionTypeID != 1\r\nWHERE AccountIDTree LIKE '%*'+CONVERT(VARCHAR(30), @AccountID)+'*%'\r\nGROUP BY \r\n  a.AccountID, \r\n  AccountNumber, \r\n  RetailAccountID, \r\n  AccountIDTree,\r\n  REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE (AccountIDTree, \r\n  '0', ''), '1', ''),'2', ''),'3', ''),'4', ''),'5', ''),'6', ''),'7', ''),'8', ''),'9', '');\r\n\r\nSET @RowCount = @@ROWCOUNT;\r\n\r\nSET @AccountLevel = (SELECT LEN(AccountIDTree2) FROM #AccountList WHERE AccountID = @AccountID);\r\n\r\nIF @RowCount <= 8000\r\nBEGIN\r\n\r\n    SELECT \r\n      a.AccountID, \r\n      s.SensorID, \r\n      s.CSNetID, \r\n      s.LastCommunicationDate, \r\n      s.ReportInterval, \r\n      c.HoldingOnlyNetwork\r\n    INTO #SensorList\r\n    FROM #AccountList a     \r\n    INNER JOIN dbo.[Sensor] s WITH (NOLOCK) ON a.AccountID = s.AccountID\r\n    INNER JOIN dbo.[CSNet] c WITH (NOLOCK) ON s.CSNetID = c.CSNetID;\r\n\r\n    SET @RowCount = @@ROWCOUNT;\r\n\r\n    IF @RowCount <= 15000\r\n    BEGIN\r\n\r\n        SELECT \r\n          a.AccountID, \r\n          g.GatewayID, \r\n          g.CSNetID, \r\n          ISNULL(gs.LastCommunicationDate, g.LastCommunicationDate) 'LastCommunicationDate',\r\n          g.ReportInterval, \r\n          c.HoldingOnlyNetwork\r\n        INTO #GatewayList\r\n        FROM dbo.[Account] a WITH (NOLOCK) \r\n        INNER JOIN dbo.[CSNet] c WITH (NOLOCK) ON a.AccountID = c.AccountID\r\n        INNER JOIN dbo.[Gateway] g WITH (NOLOCK) ON g.CSNetID = c.CSNetID\r\n\t\tLEFT JOIN dbo.GatewayStatus gs on gs.GatewayID = g.GatewayID\r\n        WHERE AccountIDTree LIKE '%*'+CONVERT(VARCHAR(30), @AccountID)+'*%';\r\n\r\n        SELECT \r\n          s.AccountID, \r\n          s.SensorID, \r\n          d.State, \r\n          d.SignalStrength, \r\n          d.Battery\r\n        INTO #DM\r\n        FROM #SensorList s\r\n        INNER JOIN dbo.[Datamessage] d WITH (NOLOCK) ON s.SensorID = d.SensorID AND s.LastCommunicationDate = d.MessageDate;\r\n\r\n        SELECT \r\n          s.AccountID, \r\n          nt.NotificationTriggeredID\r\n        INTO #AlertList\r\n        FROM #SensorList s\r\n        INNER JOIN dbo.[Sensornotification] sn WITH (NOLOCK) ON s.SensorID = sn.SensorID\r\n        INNER JOIN dbo.[NotificationTriggered] nt WITH (NOLOCK) ON nt.NotificationID = sn.NotificationID AND sn.SensorNotificationID = nt.SensorNotificationID\r\n        WHERE nt.StartTime > DATEADD(HOUR, -48, GETUTCDATE())\r\n          AND nt.AcknowledgedTime IS NULL;\r\n\r\n        SELECT\r\n          a.AccountID, \r\n          AccountNumber, \r\n          RetailAccountID, \r\n          AccountIDTree , \r\n          AccountIDTree2,\r\n          PremiereExpiration,\r\n          SensorCount           = ISNULL(t.counts,0),\r\n          OnlineCount           = ISNULL(t9.counts,0),\r\n          OfflineCount          = ISNULL(t4.counts,0),\r\n          AlertCounts           = ISNULL(t2.AlertCounts, 0),\r\n          AwareCounts           = ISNULL(t3.AwareCounts, 0),\r\n          Gateway               = ISNULL(t5.Counts, 0),\r\n          GatewayOnline         = ISNULL(t10.Counts, 0),\r\n          GatewayOffline        = ISNULL(t6.Counts, 0),\r\n          SensorLowSig          = ISNULL(t7.Counts, 0),\r\n          SensorLowBatt         = ISNULL(t8.Counts, 0)\r\n        INTO #Results\r\n        FROM #AccountList a\r\n        LEFT JOIN ( --sensorcount\r\n                    SELECT \r\n                      s.AccountID, \r\n                      Counts = COUNT(*) \r\n                    FROM #SensorList s\r\n                    GROUP BY s.AccountID\r\n                  ) t ON a.AccountID = t.AccountID\r\n        LEFT JOIN ( --SensorOnline\r\n                    SELECT \r\n                      s.AccountID, \r\n                      counts = COUNT(*) \r\n                    FROM #SensorList s\r\n                    WHERE s.LastCommunicationDate BETWEEN DATEADD(MINUTE, -2 * s.reportinterval, GETUTCDATE()) AND GETUTCDATE()\r\n                      AND s.HoldingOnlyNetwork != 1\r\n                    GROUP BY s.AccountID\r\n                  ) t9 ON a.AccountID = t9.AccountID\r\n        LEFT JOIN ( --SensorOffline\r\n                    SELECT \r\n                      s.AccountID, \r\n                      counts = COUNT(*) \r\n                    FROM #SensorList s\r\n                    WHERE s.LastCommunicationDate NOT BETWEEN DATEADD(MINUTE, -2 * s.reportinterval, GETUTCDATE()) AND GETUTCDATE()\r\n                      AND s.HoldingOnlyNetwork != 1\r\n                    GROUP BY s.AccountID\r\n                  ) t4 ON a.AccountID = t4.AccountID\r\n        LEFT JOIN ( --SensorAlerts\r\n                    SELECT \r\n                      s.AccountID, \r\n                      AlertCounts = COUNT(*) \r\n                    FROM #AlertList s\r\n                    GROUP BY s.AccountID\r\n                  ) t2 ON a.AccountID = t2.AccountID\r\n        LEFT JOIN ( --Aware Sensor Count\r\n                    SELECT \r\n                      s.AccountID, \r\n                      AwareCounts = COUNT(*) \r\n                    FROM #DM s\r\n                    WHERE State & 2 =2\r\n                    GROUP BY s.AccountID\r\n                  ) t3 ON a.AccountID = t3.AccountID\r\n        LEFT JOIN ( -- GatewayCount\r\n                    SELECT \r\n                      s.AccountID, \r\n                      counts = COUNT(*) \r\n                    FROM #GatewayList s\r\n                    GROUP BY s.accountid\r\n                  ) t5 ON a.AccountID = t5.AccountID\r\n        LEFT JOIN ( --Online GatewayCount\r\n                    SELECT \r\n                      s.AccountID, \r\n                      counts = COUNT(*) \r\n                    FROM #GatewayList s\r\n                    WHERE ISNULL(s.LastCommunicationDate, '2099-01-01') BETWEEN DATEADD(MINUTE, -2 * s.ReportInterval, GETUTCDATE()) AND GETUTCDATE()\r\n                      AND s.HoldingOnlyNetwork != 1\r\n                    GROUP BY s.AccountID\r\n                  ) t10 ON a.AccountID = t10.AccountID\r\n        LEFT JOIN ( -- Gateway Offline Count\r\n                    SELECT \r\n                      s.AccountID, \r\n                      counts = COUNT(*) \r\n                    FROM #GatewayList s\r\n                    WHERE ISNULL(s.LastCommunicationDate, '2099-01-01') NOT BETWEEN DATEADD(MINUTE, -2 * s.ReportInterval, GETUTCDATE()) AND GETUTCDATE()\r\n                      AND s.HoldingOnlyNetwork != 1\r\n                    GROUP BY s.AccountID\r\n                  ) t6 ON a.AccountID = t6.AccountID\r\n        LEFT JOIN ( --Sensor LowSignal Count\r\n                    SELECT \r\n                      s.AccountID, \r\n                      counts = COUNT(*) \r\n                    FROM #DM s\r\n                    WHERE s.SignalStrength < -80\r\n                    GROUP BY s.AccountID\r\n                  ) t7 ON a.AccountID = t7.AccountID\r\n        LEFT JOIN ( -- Sensor Low Battery Count\r\n                    SELECT \r\n                      s.AccountID, \r\n                      counts = COUNT(*) \r\n                    FROM #DM s\r\n                    WHERE s.Battery < 25\r\n                    GROUP BY s.AccountID\r\n                  ) t8 ON a.AccountID = t8.AccountID\r\n        ORDER BY LEN(AccountIDTree);\r\n\r\n        WITH CTE_Alerts AS\r\n        (\r\n          SELECT\r\n            *,\r\n          Level =   CASE WHEN OfflineCount    > 0 THEN 1 ELSE 0 END \r\n                  + CASE WHEN AlertCounts     > 0 THEN 1 ELSE 0 END \r\n                  + CASE WHEN AwareCounts     > 0 THEN 1 ELSE 0 END \r\n                  + CASE WHEN GatewayOffline  > 0 THEN 1 ELSE 0 END\r\n                  + CASE WHEN SensorLowSig    > 0 THEN 1 ELSE 0 END \r\n                  + CASE WHEN SensorLowBatt   > 0 THEN 1 ELSE 0 END\r\n          FROM #Results r\r\n        )\r\n        INSERT INTO #LocationOverview\r\n        SELECT\r\n          RowID = ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY Len(c.AccountIDTree2), c.AccountNumber),\r\n          c.AccountID,\r\n          c.AccountNumber,\r\n          c.RetailAccountID,\r\n          c.SensorCount,\r\n          c.OnlineCount,\r\n          c.OfflineCount,\r\n          c.SensorLowSig,\r\n          c.SensorLowBatt,\r\n          c.AlertCounts,\r\n          c.AwareCounts,\r\n          c.Gateway,\r\n          c.GatewayOnline,\r\n          c.GatewayOffline,\r\n          SubAccountLevel     = LEN(c.AccountIDTree2) - @AccountLevel,\r\n          SubAccountCount     = CASE WHEN LEN(c.AccountIDTree2) - @AccountLevel = 0 THEN ISNULL(t.SubAccountCount, 0)  ELSE ISNULL(t.SubAccountCount, 0) END,\r\n          AccountLevelIssues  = ISNULL(c.Level,0),\r\n          RetailLevelIssues   = ISNULL(t.Level, 0),\r\n          NotNormal           = CASE WHEN t.Level > 0 or c.Level > 0 THEN 1 ELSE 0 END,\r\n          PremiereMonths      = CASE WHEN PremiereExpiration < GETUTCDATE() OR PremiereExpiration IS NULL THEN -1 ELSE DATEDIFF(MONTH, GETUTCDATE(), PremiereExpiration) END\r\n        FROM CTE_Alerts c \r\n        LEFT JOIN \r\n        (\r\n          SELECT \r\n            c4.RetailAccountID, \r\n            Level               = SUM(CASE WHEN c3.level > 0 THEN 1 ELSE 0 END),\r\n            SubAccountCount     = COUNT(*)\r\n            FROM CTE_Alerts c3 \r\n          INNER JOIN (\r\n                      SELECT DISTINCT \r\n                        RetailAccountID \r\n                      FROM CTE_Alerts\r\n                      ) c4 ON c3.AccountidTree LIKE '%*'+CONVERT(VARCHAR(30), c4.RetailAccountID) + '*%' AND c3.AccountID != c4.RetailAccountID\r\n          GROUP BY c4.RetailAccountID\r\n        ) t ON c.AccountID = t.RetailAccountID\r\n        --WHERE c.RetailAccountID = @ViewAll\r\n        --   OR c.AccountID = ISNULL(@ViewAll, c.Accountid)\r\n        ORDER BY Len(c.AccountIDTree2), AccountIDTree;\r\n        \r\n    END -- If Sensors < 15000\r\n\r\n\r\n    --drop table #SensorList\r\n\r\nEND -- If Accounts < 8000\r\n\r\nSELECT * FROM #LocationOverview ORDER BY RowID;\r\n")]
  internal class LocationOverviewCustom : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<AccountLocationOverviewCustomModel> Result { get; private set; }

    public LocationOverviewCustom(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<AccountLocationOverviewCustomModel>(this.ToDataTable());
    }
  }

  [DBMethod("Account_LocationSearch")]
  [DBMethodBody(DBMS.SqlServer, "\r\n/********************************************************\r\nIf searchterm is entered as null, return all subaccounts\r\n********************************************************/\r\n\t\r\nIF @SearchTerm IS NULL\r\n  SET @SearchTerm = ''\r\n\r\nCREATE TABLE #AccountList\r\n(\r\n  AccountID       BIGINT,\r\n  AccountNumber   VARCHAR(300),\r\n  RetailAccountID BIGINT,\r\n  AccountIDTree   VARCHAR(500),\r\n  District        VARCHAR(300)\r\n)\r\n\r\n\r\n--INSERT INTO #AccountList\r\n--SELECT\r\n--  AccountID       = a.AccountID, \r\n--  AccountNumber   = a.AccountNumber, \r\n--  RetailAccountID = a.RetailAccountID, \r\n--  AccountIDTree   = a.AccountIDTree,\r\n--  District        = dbo.splitindex('*1*'+ a.AccountIDtree, convert(varchar(30), @AccountID) + '*', 1)\r\n--FROM dbo.[Account] a WITH (NOLOCK)\r\n--WHERE a.AccountIDtree LIKE '%*'+CONVERT(VARCHAR(30), @AccountID) +'*%'\r\n--  AND (a.AccountNumber LIKE '%'+@SearchTerm+'%')\r\n\r\nINSERT INTO #AccountList\r\nSELECT\r\n  AccountID       = a.AccountID, \r\n  AccountNumber   = a.AccountNumber, \r\n  RetailAccountID = a.RetailAccountID, \r\n  AccountIDTree   = a.AccountIDTree,\r\n  District = CASE\r\n\tWHEN @ACCOUNTID = 1 THEN DBO.Splitindex(a.Accountidtree,'*', 1)\r\n    ELSE DBO.Splitindex('*1*' + a.Accountidtree,\r\n\t\tCONVERT(VARCHAR(30), @ACCOUNTID) + '*', 1)\r\n    END\t\t\t\t\t \r\nFROM dbo.[Account] a WITH (NOLOCK)\r\nWHERE a.AccountIDtree LIKE '%*'+CONVERT(VARCHAR(30),@accountid) +'*%'\r\n  AND (a.AccountNumber LIKE '%'+''+'%')\r\n\r\nIF @AccountID != 1\r\nBEGIN \r\n\r\nUPDATE #AccountList \r\n  set District = SUBSTRING(District, 0, CHARINDEX('*', DISTRICT, 0))\r\n\r\nEND\r\n\r\nSELECT \r\n  a.AccountID, \r\n  s.SensorID, \r\n  s.CSNetID, \r\n  s.LastCommunicationDate, \r\n  s.ReportInterval, \r\n  c.HoldingOnlyNetwork,\r\n  IsAware = CASE\r\n\tWHEN s.Firstawaredate > s.Lastnormaldate AND s.Firstawaredate < '2099-01-01' \r\n\tTHEN 1\r\n    ELSE 0\r\n    END,\r\n  Battery = ISNULL(d.Battery, 200),\r\n  d.State,\r\n  s.ApplicationID,\r\n  Hardware =  0,\r\n  Alerting = CASE\r\n\tWHEN Isnull(d.Meetsnotificationrequirement, 0) = 1 AND s.Lastcommunicationdate > Dateadd(HOUR, -48, Getutcdate())\r\n    THEN 1\r\n    ELSE 0\r\n    END\r\nINTO #SensorList\r\nFROM #AccountList a     \r\nINNER JOIN dbo.[Sensor] s WITH (NOLOCK) ON a.AccountID = s.AccountID\r\nINNER JOIN dbo.[CSNet] c WITH (NOLOCK) ON s.CSNetID = c.CSNetID\r\nLEFT JOIN dbo.[DataMessage_Last] d WITH (NOLOCK) on s.SensorID = d.SensorID and s.LastDataMessageGUID = d.DataMessageGUID and s.LastCommunicationDate = d.MessageDate\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t \r\n\r\nSELECT \r\n  a.AccountID, \r\n  g.GatewayID, \r\n  g.CSNetID, \r\n  ISNULL(gs.LastCommunicationDate, g.LastCommunicationDate) 'LastCommunicationDate',\r\n  g.ReportInterval, \r\n  c.HoldingOnlyNetwork,\r\n  g.CurrentPower,\r\n  g.GatewayTypeID  \r\nINTO #GatewayList\r\nFROM dbo.[Account] a WITH (NOLOCK) \r\nINNER JOIN dbo.[CSNet] c WITH (NOLOCK) ON a.AccountID = c.AccountID\r\nINNER JOIN dbo.[Gateway] g WITH (NOLOCK) ON g.CSNetID = c.CSNetID\r\nLEFT JOIN dbo.GatewayStatus gs WITH (NOLOCK) on gs.GatewayID = g.GatewayID\r\nWHERE AccountIDTree LIKE '%*'+CONVERT(VARCHAR(30), @AccountID)+'*%';\r\n\r\n--SELECT * FROM #GatewayList WHERE CurrentPower < 3450 and CurrentPower > 5\r\n\r\n--SELECT \r\n--  a.AccountID, \r\n--  nt.NotificationTriggeredID\r\n--INTO #AlertList\r\n--FROM #AccountList a\r\n--INNER JOIN dbo.[Notification] n WITH (NOLOCK) ON a.AccountID = n.AccountID\r\n--INNER JOIN dbo.[NotificationTriggered] nt WITH (NOLOCK) ON nt.NotificationID = n.NotificationID\r\n--WHERE nt.StartTime > DATEADD(HOUR, -48, GETUTCDATE())\r\n--  AND nt.resetTime IS NULL\r\n--  AND n.IsDeleted = 0;\r\n\r\nUPDATE s \r\n  set s.Hardware = 1\r\nFROM #SensorList s\r\nWHERE s.ApplicationID in (2,35,46,65,84)\r\nAND State & 32 = 32;\r\n\r\nSELECT\r\n  a.AccountID, \r\n  a.District,\r\n  AccountNumber, \r\n  RetailAccountID, \r\n  AccountIDTree ,\r\n  SensorCount           = ISNULL(t.Counts,0),\r\n  OfflineCount          = ISNULL(t4.Counts,0),\r\n  --MissingSensor         = CASE WHEN ISNULL(t.Counts,0) = 0 THEN 1 ELSE 0 END,\r\n  SensorAwareCounts     = ISNULL(t7.Counts,0),\r\n  SensorLowBattery      = ISNULL(t8.Counts,0),\r\n  SensorHardwareIssue   = ISNULL(t11.Counts, 0),\r\n  AlertCounts           = ISNULL(t2.AlertCounts, 0),\r\n  Gateway               = ISNULL(t5.Counts, 0),\r\n  GatewayOffline        = ISNULL(t6.Counts, 0),\r\n  GatewayLowPower       = ISNULL(t12.counts, 0)\r\n  --MissingGateway        = CASE WHEN ISNULL(t5.Counts,0) = 0 THEN 1 ELSE 0 END\r\nINTO #Results\r\nFROM #AccountList a\r\nLEFT JOIN ( --sensorcount\r\n            SELECT \r\n              s.AccountID, \r\n              Counts = COUNT(*) \r\n            FROM #SensorList s\r\n            GROUP BY s.AccountID\r\n          ) t ON a.AccountID = t.AccountID\r\nLEFT JOIN ( --SensorOnline\r\n            SELECT \r\n              s.AccountID, \r\n              Counts = COUNT(*) \r\n            FROM #SensorList s\r\n            WHERE s.LastCommunicationDate BETWEEN DATEADD(Hour, -2, DATEADD(MINUTE, -2 * s.reportinterval, GETUTCDATE())) AND DATEADD(HOUR, 2, GETUTCDATE())\r\n              AND s.HoldingOnlyNetwork != 1\r\n\t\t\t\t\t\t\t\t\t \r\n            GROUP BY s.AccountID\r\n          ) t9 ON a.AccountID = t9.AccountID\r\nLEFT JOIN ( --SensorOffline\r\n            SELECT \r\n              s.AccountID, \r\n              Counts = COUNT(*) \r\n            FROM #SensorList s\r\n            WHERE s.LastCommunicationDate NOT BETWEEN DATEADD(Hour, -2, DATEADD(MINUTE, -2 * s.reportinterval, GETUTCDATE())) AND DATEADD(HOUR, 2, GETUTCDATE())\r\n              AND s.HoldingOnlyNetwork != 1\r\n\t\t\t\t\t\t\t\t\t  \r\n              AND s.Alerting = 0\r\n            GROUP BY s.AccountID\r\n          ) t4 ON a.AccountID = t4.AccountID\r\nLEFT JOIN ( --Alerting (last comm Meets Notification Req = 1)\r\n            SELECT \r\n              s.AccountID, \r\n              AlertCounts = COUNT(*) \r\n            FROM #SensorList s\r\n            WHERE s.HoldingOnlyNetwork != 1\r\n              AND s.Alerting = 1\r\n            GROUP BY s.AccountID\r\n          ) t2 ON a.AccountID = t2.AccountID\r\nLEFT JOIN ( -- GatewayCount\r\n            SELECT \r\n              s.AccountID, \r\n              Counts = COUNT(*) \r\n            FROM #GatewayList s\r\n            WHERE GatewayTypeID NOT IN (11,35)\r\n            GROUP BY s.accountid\r\n          ) t5 ON a.AccountID = t5.AccountID\r\nLEFT JOIN ( --Online GatewayCount\r\n            SELECT \r\n              s.AccountID, \r\n              Counts = COUNT(*) \r\n            FROM #GatewayList s\r\n            WHERE ISNULL(s.LastCommunicationDate, '2099-01-01') BETWEEN DATEADD(MINUTE, -1, DATEADD(MINUTE, -2 * s.ReportInterval, GETUTCDATE())) AND GETUTCDATE()\r\n              AND s.HoldingOnlyNetwork != 1\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t  \r\n              AND GatewayTypeID NOT IN (11,35)  \r\n            GROUP BY s.AccountID\r\n          ) t10 ON a.AccountID = t10.AccountID\r\nLEFT JOIN ( -- Gateway Offline Count\r\n            SELECT \r\n              s.AccountID, \r\n              Counts = COUNT(*) \r\n            FROM #GatewayList s\r\n            WHERE ISNULL(s.LastCommunicationDate, '2099-01-01') NOT BETWEEN DATEADD(MINUTE, -1,DATEADD(MINUTE, -2 * s.ReportInterval, GETUTCDATE())) AND GETUTCDATE()\r\n              AND s.HoldingOnlyNetwork != 1\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t  \r\n              AND GatewayTypeID NOT IN (11,35)  \r\n            GROUP BY s.AccountID\r\n          ) t6 ON a.AccountID = t6.AccountID\r\nLEFT JOIN ( -- Gateway Low Power\r\n            SELECT \r\n              s.AccountID, \r\n              Counts = COUNT(*) \r\n            FROM #GatewayList s\r\n            WHERE ISNULL(s.LastCommunicationDate, '2099-01-01')  BETWEEN DATEADD(MINUTE, -1,DATEADD(MINUTE, -2 * s.ReportInterval, GETUTCDATE())) AND GETUTCDATE()\r\n              AND s.HoldingOnlyNetwork != 1\r\n              AND s.CurrentPower < 3450 and s.CurrentPower > 5\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t  \r\n\t\t\t\t\t\t\t\t\t\t\t\t   \r\n              AND GatewayTypeID NOT IN (11,35)  \r\n            GROUP BY s.AccountID\r\n          ) t12 ON a.AccountID = t6.AccountID\r\nLEFT JOIN ( --Aware Counts\r\n            SELECT\r\n              AccountID,\r\n              Counts = COUNT(*)\r\n            FROM #SensorList s\r\n            WHERE IsAware = 1\r\n              AND HoldingOnlyNetwork != 1\r\n              AND s.Hardware = 0\r\n              AND s.Alerting = 0\r\n              AND s.Battery >= 10\r\n              AND s.LastCommunicationDate BETWEEN DATEADD(Hour, -2, DATEADD(MINUTE, -2 * s.reportinterval, GETUTCDATE())) AND DATEADD(HOUR, 2, GETUTCDATE())\r\n\t\t\t\t\t\t\t\t\t\t  \r\n            GROUP BY AccountID\r\n          ) t7 on a.accountid = t7.AccountID\r\nLEFT JOIN ( --sensorcount battery\r\n            SELECT\r\n              AccountID,\r\n              Counts = COUNT(*)\r\n            FROM #SensorList s\r\n            WHERE battery < 10\r\n              AND s.LastCommunicationDate BETWEEN DATEADD(Hour, -2, DATEADD(MINUTE, -2 * s.reportinterval, GETUTCDATE())) AND DATEADD(HOUR, 2, GETUTCDATE())\r\n              AND HoldingOnlyNetwork != 1\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\r\n              AND s.Hardware = 0\r\n              AND s.Alerting = 0\r\n            GROUP BY AccountID\r\n          ) t8 on a.accountid = t8.AccountID\r\nLEFT JOIN ( --sensorcount hardware\r\n            SELECT \r\n              s.AccountID, \r\n              Counts = COUNT(*) \r\n            FROM #SensorList s\r\n            WHERE Hardware = 1\r\n              AND s.LastCommunicationDate BETWEEN DATEADD(Hour, -2, DATEADD(MINUTE, -2 * s.reportinterval, GETUTCDATE())) AND DATEADD(HOUR, 2, GETUTCDATE())\r\n              AND s.HoldingOnlyNetwork != 1\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t  \r\n              AND s.Alerting = 0\r\n            GROUP BY s.AccountID\r\n          ) t11 ON a.AccountID = t11.AccountID\r\nORDER BY LEN(AccountIDTree);\r\n\r\nSELECT  \r\n  AccountID         = District, \r\n  a.AccountNumber, \r\n  SensorCount       = Sum(SensorCount), \r\n  OfflineCount      = SUM(OfflineCount), \r\n  --MissingSensor     = SUM(MissingSensor), \r\n  SensorAwareCounts = SUM(SensorAwareCounts),\r\n  SensorLowBattery  = SUM(SensorLowBattery),\r\n  AlertCounts       = SUM(AlertCounts),\r\n  Hardware          = SUM(SensorHardwareIssue),\r\n  GatewayCount      = SUM(Gateway), \r\n  GatewayOffline    = SUM(GatewayOffline), \r\n  --MissingGateway    = SUM(MissingGateway), \r\n  SubAccounts       = Count(*),\r\n  GatewayLowPower = SUM(GatewayLowPower)\r\n  INTO #Final\r\nFROM #Results r\r\nINNER JOIN dbo.[Account] a WITH (NOLOCK) on r.District = a.AccountID\r\n\t\t\t\t\t\t\t\t\t\t\t  \r\nGROUP BY District, a.AccountNumber;\r\n\t\t\t\t\t\t\t  \r\n\r\nSELECT * FROM #Final\r\n\r\nSELECT\r\n Offline    = SUM(offlineCount) + (select sum(offlineCount) FROM #Results WHERE AccountID = @AccountID) + (select sum(GatewayOffline) FROM #Results WHERE AccountID = @AccountID),\r\n Warning    = SUm(SensorLowBattery) + SUM(Hardware) + (select SUm(SensorLowBattery) + SUM(Hardware) + SUM(GatewayLowPower) FROM #Results WHERE AccountID = @AccountID),\r\n Alert      = SUM(AlertCounts) + (select sum(AlertCounts) FROM #Results WHERE AccountID = @AccountID),\r\n Locations  = SUM(SubAccounts) + 1\r\nFROM #Final\r\n\r\n\r\ndrop table #AccountList\r\n--drop table #AlertList\r\ndrop table #GatewayList\r\ndrop table #Results\r\ndrop table #SensorList\r\ndrop table #Final\r\n")]
  internal class LocationSearch : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("SearchTerm", typeof (string))]
    public string SearchTerm { get; private set; }

    public Tuple<List<AccountLocationSearchModel>, AccountLocationOverviewHeaderModel> Result { get; private set; }

    public LocationSearch(long accountID, string searchTerm)
    {
      this.AccountID = accountID;
      this.SearchTerm = searchTerm;
      DataSet dataSet = this.ToDataSet();
      List<AccountLocationSearchModel> locationSearchModelList = new List<AccountLocationSearchModel>();
      AccountLocationOverviewHeaderModel overviewHeaderModel = new AccountLocationOverviewHeaderModel();
      if (dataSet != null && dataSet.Tables != null && dataSet.Tables.Count == 2)
      {
        locationSearchModelList = BaseDBObject.Load<AccountLocationSearchModel>(dataSet.Tables[0]);
        overviewHeaderModel = BaseDBObject.Load<AccountLocationOverviewHeaderModel>(dataSet.Tables[1]).FirstOrDefault<AccountLocationOverviewHeaderModel>();
      }
      this.Result = new Tuple<List<AccountLocationSearchModel>, AccountLocationOverviewHeaderModel>(locationSearchModelList, overviewHeaderModel);
    }
  }

  [DBMethod("Account_LocationSearchCanteen")]
  [DBMethodBody(DBMS.SqlServer, "\r\n/********************************************************\r\nIf searchterm is entered as null, return all subaccounts\r\n********************************************************/\r\nIF @SearchTerm IS NULL\r\n  SET @SearchTerm = ''\r\n\r\nCREATE TABLE #AccountList\r\n(\r\n  AccountID       BIGINT,\r\n  AccountNumber   VARCHAR(300),\r\n  RetailAccountID BIGINT,\r\n  AccountIDTree   VARCHAR(500),\r\n  District        VARCHAR(300)\r\n)\r\n\r\n\r\n--INSERT INTO #AccountList\r\n--SELECT\r\n--  AccountID       = a.AccountID, \r\n--  AccountNumber   = a.AccountNumber, \r\n--  RetailAccountID = a.RetailAccountID, \r\n--  AccountIDTree   = a.AccountIDTree,\r\n--  District        = dbo.splitindex('*1*'+ a.AccountIDtree, convert(varchar(30), @AccountID) + '*', 1)\r\n--FROM dbo.[Account] a WITH (NOLOCK)\r\n--WHERE a.AccountIDtree LIKE '%*'+CONVERT(VARCHAR(30), @AccountID) +'*%'\r\n--  AND (a.AccountNumber LIKE '%'+@SearchTerm+'%')\r\n\r\nINSERT INTO #AccountList\r\nSELECT\r\n  AccountID       = a.AccountID, \r\n  AccountNumber   = a.AccountNumber, \r\n  RetailAccountID = a.RetailAccountID, \r\n  AccountIDTree   = a.AccountIDTree,\r\n  District        = case when @accountid = 1 then dbo.splitindex(a.AccountIdTree, '*', 1) else dbo.splitindex('*1*'+ a.AccountIDtree, convert(varchar(30), @accountid) + '*', 1) end\r\nFROM dbo.[Account] a WITH (NOLOCK)\r\nWHERE a.AccountIDtree LIKE '%*'+CONVERT(VARCHAR(30),@accountid) +'*%'\r\n  AND (a.AccountNumber LIKE '%'+''+'%')\r\n\r\nIF @AccountID != 1\r\nBEGIN \r\n\r\nUPDATE #AccountList \r\n  set District = SUBSTRING(District, 0, CHARINDEX('*', DISTRICT, 0))\r\n\r\nEND\r\n\r\nSELECT \r\n  a.AccountID, \r\n  s.SensorID, \r\n  s.CSNetID, \r\n  s.LastCommunicationDate, \r\n  s.ReportInterval, \r\n  c.HoldingOnlyNetwork,\r\n  IsAware = CASE WHEN s.FirstAwareDate > s.LastNormalDate and s.FirstAwareDate < '2099-01-01' then 1 else 0 END,\r\n  Battery = ISNULL(d.Battery, 200)\r\nINTO #SensorList\r\nFROM #AccountList a     \r\nINNER JOIN dbo.[Sensor] s WITH (NOLOCK) ON a.AccountID = s.AccountID\r\nINNER JOIN dbo.[CSNet] c WITH (NOLOCK) ON s.CSNetID = c.CSNetID\r\nLEFT JOIN dbo.[DataMessage] d WITH (NOLOCK) on s.SensorID = d.SensorID and s.LastDataMessageGUID = d.DataMessageGUID and s.LastCommunicationDate = d.MessageDate\r\n\r\n\r\nSELECT \r\n  a.AccountID, \r\n  g.GatewayID, \r\n  g.CSNetID, \r\n  ISNULL(gs.LastCommunicationDate, g.LastCommunicationDate) 'LastCommunicationDate',\r\n  g.ReportInterval, \r\n  c.HoldingOnlyNetwork\r\nINTO #GatewayList\r\nFROM dbo.[Account] a WITH (NOLOCK) \r\nINNER JOIN dbo.[CSNet] c WITH (NOLOCK) ON a.AccountID = c.AccountID\r\nINNER JOIN dbo.[Gateway] g WITH (NOLOCK) ON g.CSNetID = c.CSNetID\r\nLEFT JOIN dbo.GatewayStatus gs on gs.GatewayID = g.GatewayID\r\nWHERE AccountIDTree LIKE '%*'+CONVERT(VARCHAR(30), @AccountID)+'*%';\r\n\r\nSELECT \r\n  s.AccountID, \r\n  nt.NotificationTriggeredID\r\nINTO #AlertList\r\nFROM #SensorList s\r\nINNER JOIN dbo.[Sensornotification] sn WITH (NOLOCK) ON s.SensorID = sn.SensorID\r\nINNER JOIN dbo.[NotificationTriggered] nt WITH (NOLOCK) ON nt.NotificationID = sn.NotificationID AND sn.SensorNotificationID = nt.SensorNotificationID\r\nWHERE nt.StartTime > DATEADD(HOUR, -48, GETUTCDATE())\r\n  AND nt.resetTime IS NULL;\r\n\r\nSELECT\r\n  a.AccountID, \r\n  a.District,\r\n  AccountNumber, \r\n  RetailAccountID, \r\n  AccountIDTree ,\r\n  SensorCount           = ISNULL(t.Counts,0),\r\n  OfflineCount          = ISNULL(t4.Counts,0),\r\n  --MissingSensor         = CASE WHEN ISNULL(t.Counts,0) = 0 THEN 1 ELSE 0 END,\r\n  SensorAwareCounts     = ISNULL(t7.Counts,0),\r\n  SensorLowBattery      = ISNULL(t8.Counts,0),\r\n  AlertCounts           = ISNULL(t2.AlertCounts, 0),\r\n  Gateway               = ISNULL(t5.Counts, 0),\r\n  GatewayOffline        = ISNULL(t6.Counts, 0)\r\n  --MissingGateway        = CASE WHEN ISNULL(t5.Counts,0) = 0 THEN 1 ELSE 0 END\r\nINTO #Results\r\nFROM #AccountList a\r\nLEFT JOIN ( --sensorcount\r\n            SELECT \r\n              s.AccountID, \r\n              Counts = COUNT(*) \r\n            FROM #SensorList s\r\n            GROUP BY s.AccountID\r\n          ) t ON a.AccountID = t.AccountID\r\nLEFT JOIN ( --SensorOnline\r\n            SELECT \r\n              s.AccountID, \r\n              Counts = COUNT(*) \r\n            FROM #SensorList s\r\n            WHERE s.LastCommunicationDate BETWEEN DATEADD(MINUTE, -2 * s.reportinterval, GETUTCDATE()) AND GETUTCDATE()\r\n              AND s.HoldingOnlyNetwork != 1\r\n            GROUP BY s.AccountID\r\n          ) t9 ON a.AccountID = t9.AccountID\r\nLEFT JOIN ( --SensorOffline\r\n            SELECT \r\n              s.AccountID, \r\n              Counts = COUNT(*) \r\n            FROM #SensorList s\r\n            WHERE s.LastCommunicationDate NOT BETWEEN DATEADD(MINUTE, -2 * s.reportinterval, GETUTCDATE()) AND GETUTCDATE()\r\n              AND s.HoldingOnlyNetwork != 1\r\n            GROUP BY s.AccountID\r\n          ) t4 ON a.AccountID = t4.AccountID\r\nLEFT JOIN ( --SensorAlerts\r\n            SELECT \r\n              s.AccountID, \r\n              AlertCounts = COUNT(*) \r\n            FROM #AlertList s\r\n            GROUP BY s.AccountID\r\n          ) t2 ON a.AccountID = t2.AccountID\r\nLEFT JOIN ( -- GatewayCount\r\n            SELECT \r\n              s.AccountID, \r\n              Counts = COUNT(*) \r\n            FROM #GatewayList s\r\n            GROUP BY s.accountid\r\n          ) t5 ON a.AccountID = t5.AccountID\r\nLEFT JOIN ( --Online GatewayCount\r\n            SELECT \r\n              s.AccountID, \r\n              Counts = COUNT(*) \r\n            FROM #GatewayList s\r\n            WHERE ISNULL(s.LastCommunicationDate, '2099-01-01') BETWEEN DATEADD(MINUTE, -2 * s.ReportInterval, GETUTCDATE()) AND GETUTCDATE()\r\n              AND s.HoldingOnlyNetwork != 1\r\n            GROUP BY s.AccountID\r\n          ) t10 ON a.AccountID = t10.AccountID\r\nLEFT JOIN ( -- Gateway Offline Count\r\n            SELECT \r\n              s.AccountID, \r\n              Counts = COUNT(*) \r\n            FROM #GatewayList s\r\n            WHERE ISNULL(s.LastCommunicationDate, '2099-01-01') NOT BETWEEN DATEADD(MINUTE, -2 * s.ReportInterval, GETUTCDATE()) AND GETUTCDATE()\r\n              AND s.HoldingOnlyNetwork != 1\r\n            GROUP BY s.AccountID\r\n          ) t6 ON a.AccountID = t6.AccountID\r\nLEFT JOIN (\r\n            SELECT\r\n              AccountID,\r\n              Counts = COUNT(*)\r\n            FROM #SensorList\r\n            WHERE IsAware = 1\r\n            GROUP BY AccountID\r\n          ) t7 on a.accountid = t7.AccountID\r\nLEFT JOIN (\r\n            SELECT\r\n              AccountID,\r\n              Counts = COUNT(*)\r\n            FROM #SensorList\r\n            WHERE battery < 15\r\n            GROUP BY AccountID\r\n          ) t8 on a.accountid = t8.AccountID\r\nORDER BY LEN(AccountIDTree);\r\n\r\nSELECT  \r\n  AccountID         = District, \r\n  a.AccountNumber, \r\n  SensorCount       = Sum(SensorCount), \r\n  OfflineCount      = SUM(OfflineCount), \r\n  --MissingSensor     = SUM(MissingSensor), \r\n  SensorAwareCounts = SUM(SensorAwareCounts),\r\n  SensorLowBattery  = SUM(SensorLowBattery),\r\n  AlertCounts       = SUM(AlertCounts),\r\n  Hardware          = 0,\r\n  GatewayCount      = SUM(Gateway), \r\n  GatewayOffline    = SUM(GatewayOffline), \r\n  --MissingGateway    = SUM(MissingGateway), \r\n  SubAccounts       = CASE WHEN COUNT(*) = 1 and a.AccountNumber LIKE '%'+@SearchTerm+'%' THEN 0 ELSE COUNT(*)  END\r\n  INTO #Final\r\nFROM #Results r\r\nINNER JOIN dbo.[Account] a WITH (NOLOCK) on r.District = a.AccountID\r\nGROUP BY District, a.AccountNumber;\r\n\r\nSELECT * FROM #Final\r\n\r\nSELECT\r\n Offline    = SUM(offlineCount),\r\n Warning    = SUm(SensorLowBattery),\r\n Alert      = SUM(AlertCounts),\r\n Locations  = SUM(SubAccounts) + count(*)\r\nFROM #Final\r\n\r\ndrop table #AccountList\r\ndrop table #AlertList\r\ndrop table #GatewayList\r\ndrop table #Results\r\ndrop table #SensorList\r\ndrop table #Final\r\n")]
  internal class LocationSearchCanteen : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("SearchTerm", typeof (string))]
    public string SearchTerm { get; private set; }

    public Tuple<List<AccountLocationSearchModel>, AccountLocationOverviewHeaderModel> Result { get; private set; }

    public LocationSearchCanteen(long accountID, string searchTerm)
    {
      this.AccountID = accountID;
      this.SearchTerm = searchTerm;
      DataSet dataSet = this.ToDataSet();
      List<AccountLocationSearchModel> locationSearchModelList = new List<AccountLocationSearchModel>();
      AccountLocationOverviewHeaderModel overviewHeaderModel = new AccountLocationOverviewHeaderModel();
      if (dataSet != null && dataSet.Tables != null && dataSet.Tables.Count == 2)
      {
        locationSearchModelList = BaseDBObject.Load<AccountLocationSearchModel>(dataSet.Tables[0]);
        overviewHeaderModel = BaseDBObject.Load<AccountLocationOverviewHeaderModel>(dataSet.Tables[1]).FirstOrDefault<AccountLocationOverviewHeaderModel>();
      }
      this.Result = new Tuple<List<AccountLocationSearchModel>, AccountLocationOverviewHeaderModel>(locationSearchModelList, overviewHeaderModel);
    }
  }

  [DBMethod("Account_LocationList")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @RowID BIGINT;\r\n\r\nWITH CTE_Accounts AS\r\n(\r\n  SELECT \r\n    RowID     = ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY AccountIDTree), \r\n    AccountID = f.Item\r\n  FROM dbo.[Account] a WITH (NOLOCK)\r\n  CROSS APPLY dbo.Split(accountidtree,  '*') f\r\n  WHERE AccountID = @AccountID\r\n)\r\nSELECT\r\n  a.RowID, \r\n  a.AccountID,\r\n  a2.AccountNumber\r\nINTO #Temp1\r\nFROM CTE_Accounts a\r\nINNER JOIN dbo.[Account] a2 WITH (NOLOCK) ON a.AccountID = a2.AccountID\r\nORDER by a.RowID;\r\n\r\n\r\nSELECT\r\n@RowID = ISNULL(RowID, 1)\r\nFROM Customer c\r\nLEFT JOIN #Temp1 t on c.AccountID = t.AccountID\r\nWHERE c.CustomerID = @CustomerID;\r\n\r\nSELECT \r\nRowID = 0,\r\nAccountID = NULL,\r\nAccountNumber = CONVERT(VARCHAR(300), Count(*))\r\nFROM Account \r\nWHERE RetailAccountID = @AccountID\r\nUNION ALL\r\nSELECT\r\n  *\r\nFROM #temp1 \r\nWHERE RowID >= @RowID \r\nORDER BY RowID;\r\n\r\nDROP TABLE #Temp1\r\n")]
  internal class LocationList : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    public List<AccountLocationListModel> Result { get; private set; }

    public LocationList(long accountID, long customerID)
    {
      this.AccountID = accountID;
      this.CustomerID = customerID;
      this.Result = BaseDBObject.Load<AccountLocationListModel>(this.ToDataTable());
    }
  }

  [DBMethod("Account_CheckAccountNumberIsUnique")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  COUNT(*)\r\nFROM dbo.[Account]\r\nWHERE AccountNumber = @AccountNumber;\r\n")]
  internal class CheckAccountNumberIsUnique : BaseDBMethod
  {
    [DBMethodParam("AccountNumber", typeof (string))]
    public string AccountNumber { get; private set; }

    public bool Result { get; private set; }

    public CheckAccountNumberIsUnique(string accountNumber)
    {
      this.AccountNumber = accountNumber;
      this.Result = this.ToScalarValue<int>() == 0;
    }
  }

  [DBMethod("Account_Ancestors")]
  [DBMethodBody(DBMS.SqlServer, "\r\n    DECLARE @RootAccount BIGINT\r\n    ,@RootAccountParent BIGINT\r\n    \r\n    SELECT @RootAccount = AccountID FROM Customer WHERE CustomerID = @CustomerID\r\n    SELECT @RootAccountParent = RetailAccountID FROM Account WHERE AccountID = @RootAccount\r\n    \r\n\r\n    ;WITH\r\n      cteAccounts\r\n      AS\r\n      (\r\n    -->>>>>>>>>>Block 1>>>>>>>>>>>>>>>>>\r\n    -- In a rCTE, this block is called an [Anchor]\r\n    -- The query finds all root nodes as described by WHERE ManagerID IS NULL\r\n        SELECT *, Level = 1\r\n        FROM Account\r\n        WHERE AccountID = @AccountID\r\n    -->>>>>>>>>>Block 1>>>>>>>>>>>>>>>>>\r\n        UNION ALL\r\n    -->>>>>>>>>>Block 2>>>>>>>>>>>>>>>>>    \r\n    -- This is the recursive expression of the rCTE\r\n    -- On the first 'execution' it will query data in [Account],\r\n    -- relative to the [Anchor] above.\r\n    -- This will produce a resultset, we will call it R{1} and it is JOINed to [cteAccounts]\r\n    -- as defined by the hierarchy\r\n    -- Subsequent 'executions' of this block will reference R{n-1}\r\n        SELECT a.*, Level = Level+1\r\n        FROM Account a\r\n\t\tINNER JOIN cteAccounts ctea ON ctea.RetailAccountID = a.AccountID\r\n        WHERE a.AccountID != @RootAccountParent OR @RootAccountParent IS NULL\r\n    -->>>>>>>>>>Block 2>>>>>>>>>>>>>>>>>\r\n      )\r\n      \r\n    SELECT a.*\r\n    FROM cteAccounts a\r\n    ORDER BY a.Level")]
  internal class Ancestors : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public DataTable Result { get; private set; }

    public Ancestors(long customerID, long accountID)
    {
      this.CustomerID = customerID;
      this.AccountID = accountID;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("Account_IsSubAccount")]
  [DBMethodBody(DBMS.SqlServer, "\r\n    DECLARE @RootAccountParent BIGINT\r\n    \r\n    SELECT @RootAccountParent = RetailAccountID FROM Account WHERE AccountID = @ParentAccountID\r\n    \r\n\r\n    ;WITH\r\n      cteAccounts\r\n      AS\r\n      (\r\n    -->>>>>>>>>>Block 1>>>>>>>>>>>>>>>>>\r\n    -- In a rCTE, this block is called an [Anchor]\r\n    -- The query finds all root nodes as described by WHERE ManagerID IS NULL\r\n        SELECT *, Level = 1\r\n        FROM Account\r\n        WHERE AccountID = @SubAccountID\r\n    -->>>>>>>>>>Block 1>>>>>>>>>>>>>>>>>\r\n        UNION ALL\r\n    -->>>>>>>>>>Block 2>>>>>>>>>>>>>>>>>    \r\n    -- This is the recursive expression of the rCTE\r\n    -- On the first 'execution' it will query data in [Account],\r\n    -- relative to the [Anchor] above.\r\n    -- This will produce a resultset, we will call it R{1} and it is JOINed to [cteAccounts]\r\n    -- as defined by the hierarchy\r\n    -- Subsequent 'executions' of this block will reference R{n-1}\r\n        SELECT a.*, Level = Level+1\r\n        FROM Account a\r\n\t\tINNER JOIN cteAccounts ctea ON ctea.RetailAccountID = a.AccountID\r\n        WHERE a.AccountID != @RootAccountParent OR @RootAccountParent IS NULL\r\n    -->>>>>>>>>>Block 2>>>>>>>>>>>>>>>>>\r\n      )\r\n      \r\n    SELECT Count(*)\r\n    FROM cteAccounts a\r\n    Where AccountID = @ParentAccountID")]
  internal class IsSubAccount : BaseDBMethod
  {
    [DBMethodParam("ParentAccountID", typeof (long))]
    public long ParentAccountID { get; private set; }

    [DBMethodParam("SubAccountID", typeof (long))]
    public long SubAccountID { get; private set; }

    public bool Result { get; private set; }

    public IsSubAccount(long parentAccountID, long subAccountID)
    {
      this.ParentAccountID = parentAccountID;
      this.SubAccountID = subAccountID;
      this.Result = this.ToScalarValue<bool>();
    }
  }

  [DBMethod("Account_GetThemeID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  AccountThemeID,\r\n  AccountID\r\nINTO #TEMPTABLE\r\nFROM dbo.[AccountTheme];\r\n            \r\nWHILE @aid NOT IN (SELECT AccountID FROM #TEMPTABLE)\r\nBEGIN\r\n\r\n    SET @aid = (SELECT RetailAccountID FROM dbo.[Account] WHERE AccountID=@aid);\r\n    \r\n    IF @aid IS NULL\r\n    BREAK;\r\n\r\nEND\r\n\r\nSELECT\r\nAccountThemeID\r\nFROM dbo.[AccountTheme]\r\nWHERE AccountID=@aid\r\nORDER BY\r\n  AccountThemeID;\r\n")]
  internal class GetThemeID : BaseDBMethod
  {
    [DBMethodParam("aid", typeof (long))]
    public long AccountID { get; private set; }

    public long Result { get; private set; }

    public GetThemeID(long accountID)
    {
      this.AccountID = accountID;
      try
      {
        this.Result = (long) this.ToDataTable().Rows[0]["AccountThemeID"];
      }
      catch
      {
        this.Result = long.MinValue;
      }
    }
  }

  [DBMethod("Account_NewAccounts")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  a.*\r\nFROM dbo.[Account] a\r\nWHERE a.CreateDate BETWEEN @FromDate AND @ToDate\r\nORDER BY\r\n  CreateDate DESC;\r\n")]
  internal class NewAccounts : BaseDBMethod
  {
    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    public List<Monnit.Account> Result { get; private set; }

    public NewAccounts(DateTime fromDate, DateTime toDate)
    {
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Result = BaseDBObject.Load<Monnit.Account>(this.ToDataTable());
    }
  }

  [DBMethod("Account_loadResellers")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @RootAccount VARCHAR(25);\r\n\r\nSET @Limit = 200\r\n\r\nSELECT\r\n  @RootAccount = '*' + CONVERT(VARCHAR(25),AccountID) + '*'\r\nFROM dbo.[Customer]\r\nWHERE CustomerID = @CustomerID;\r\n\r\nSELECT TOP (@Limit)\r\n  *\r\nFROM dbo.[Account] a\r\nWHERE AccountIDTree LIKE '%' + @RootAccount + '%'\r\n  AND a.IsReseller  = 1\r\nOrder By AccountNumber;\r\n")]
  internal class loadResellers : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("Limit", typeof (int))]
    public int Limit { get; private set; }

    public List<Monnit.Account> Result { get; private set; }

    public loadResellers(long customerID, int limit)
    {
      this.Limit = limit;
      this.CustomerID = customerID;
      this.Result = BaseDBObject.Load<Monnit.Account>(this.ToDataTable());
    }
  }

  [DBMethod("Account_RemoveSamlEndPoint")]
  [DBMethodBody(DBMS.SqlServer, "\r\nUPDATE dbo.[Account]\r\nSET SamlEndpointID = NULL\r\nWHERE AccountID = @AccountID;\r\n\r\nIF NOT EXISTS (SELECT * FROM dbo.[Account] where SamlEndPointID = @SamlEndPointID)\r\nBEGIN\r\n\r\n  DELETE SamlEndPoint \r\n  WHERE SamlEndPointID = @SamlEndPointID;\r\n\r\nEND\r\n")]
  internal class RemoveSamlEndPoint : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("SamlEndPointID", typeof (long))]
    public long SamlEndPointID { get; private set; }

    public List<Monnit.Account> Result { get; private set; }

    public RemoveSamlEndPoint(long accountID, long samlEndPointID)
    {
      this.AccountID = accountID;
      this.SamlEndPointID = samlEndPointID;
      this.Result = BaseDBObject.Load<Monnit.Account>(this.ToDataTable());
    }
  }

  [DBMethod("Account_LoadByAccountIDTree")]
  [DBMethodBody(DBMS.SqlServer, "\r\nIF @Debug = 1\r\nBEGIN\r\n\tSELECT * FROM Customer WHERE CustomerID = @CustomerID\r\nEND\r\n\r\nDECLARE @CustomerAccountID BIGINT = (SELECT TOP 1 AccountID FROM Customer WHERE CustomerID = @CustomerID)\r\nDECLARE @CustomerAccountIDTree VARCHAR(255) = (SELECT TOP 1 AccountIDTree FROM Account WHERE AccountID = @CustomerAccountID)\r\nDECLARE @CurrentAccountIDTree VARCHAR(255) = (SELECT TOP 1 AccountIDTree FROM Account WHERE AccountID = @CurrentAccountID)\r\nDECLARE @TargetAccountIDTree VARCHAR(255) = (SELECT TOP 1 AccountIDTree FROM Account WHERE AccountID = @TargetAccountID)\r\n\r\nIF @Debug = 1\r\nBEGIN\r\n\tSELECT @CustomerID AS CustomerID, @CustomerAccountID AS CustomerAccountID, @CurrentAccountID AS CurrentAccountID, @TargetAccountID AS TargetAccountID\r\n\tSELECT @CustomerAccountIDTree AS CustomerAccountIDTree, @CurrentAccountIDTree AS CurrentAccountIDTree, @TargetAccountIDTree AS TargetAccountIDTree\r\nEND\r\n\r\nDECLARE @PathCustomerAcctToTargetAcct VARCHAR(255) = (SELECT REPLACE((SELECT AccountIDTree FROM Account WHERE AccountID = @TargetAccountID), @CustomerAccountIDTree, ''))\r\n\r\nDECLARE @PathCurrentAcctToTargetAcct VARCHAR(255) = (SELECT REPLACE((SELECT AccountIDTree FROM Account WHERE AccountID = @TargetAccountID), @CurrentAccountIDTree, ''))\r\n\r\nIF @Debug = 1\r\nBEGIN\r\n\tSELECT @PathCustomerAcctToTargetAcct AS PathCustomerAcctToTargetAcct, @PathCurrentAcctToTargetAcct AS PathCurrentAcctToTargetAcct\r\nEND\r\n\r\n\r\nDECLARE @CanSee INT = (\r\n\tSELECT \r\n\t\tCASE WHEN PATINDEX(CONCAT('%', @CustomerAccountID, '*%'), @TargetAccountIDTree) > 0 THEN 1 ELSE 0 END\r\n)\r\n\r\nDECLARE @InTree INT = (\r\n\tSELECT \r\n\t\tCASE WHEN PATINDEX(CONCAT('%', @CurrentAccountID, '*%'), @TargetAccountIDTree) > 0 THEN 1 ELSE 0 END\r\n)\r\n\r\nIF @Debug = 1\r\nBEGIN\r\n\tSELECT @CanSee CanSee, @InTree InTree\r\nEND\r\n\r\n\r\nIF @CanSee > 0 AND @InTree > 0\r\n\tBEGIN\r\n\t\tSELECT a.* \r\n\t\tFROM STRING_SPLIT(@PathCurrentAcctToTargetAcct , '*') ss\r\n\t\tJOIN Account a ON a.AccountID = ss.VALUE\r\n\t\tWHERE VALUE <> ''\r\n\tEND \r\nELSE \r\n\tBEGIN\r\n\t\tSELECT * FROM Account a WHERE a.AccountID = @CustomerAccountID\r\n\tEND\r\n")]
  internal class LoadByAccountIDTree : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("CurrentAccountID", typeof (long))]
    public long CurrentAccountID { get; private set; }

    [DBMethodParam("TargetAccountID", typeof (long))]
    public long TargetAccountID { get; private set; }

    public List<Monnit.Account> Result { get; private set; }

    public LoadByAccountIDTree(long customerID, long currentAccountID, long targetAccountID)
    {
      this.CustomerID = customerID;
      this.CurrentAccountID = currentAccountID;
      this.TargetAccountID = targetAccountID;
      this.Result = BaseDBObject.Load<Monnit.Account>(this.ToDataTable());
    }
  }
}
