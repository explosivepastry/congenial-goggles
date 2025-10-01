// Decompiled with JetBrains decompiler
// Type: Monnit.Data.Customer
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class Customer
{
  [DBMethod("Customer_Authenticate")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[Customer]\r\nWHERE [Username]  = @Username \r\n  AND [IsDeleted] = 0;\r\n")]
  internal class Authenticate : BaseDBMethod
  {
    [DBMethodParam("Username", typeof (string))]
    public string Username { get; private set; }

    public Monnit.Customer Result { get; private set; }

    public Authenticate(
      string username,
      string password,
      string application,
      string ipAddress,
      bool useEncryption)
    {
      this.Username = username;
      DataTable dataTable = this.ToDataTable();
      this.Result = new Monnit.Customer();
      IEnumerator enumerator = dataTable.Rows.GetEnumerator();
      try
      {
        if (enumerator.MoveNext())
        {
          this.Result.Load((DataRow) enumerator.Current);
          if (this.Result.Password2 == null || this.Result.Password2.Length < 5 && !string.IsNullOrEmpty(this.Result.Password))
          {
            string password1 = useEncryption ? this.Result.Password.Decrypt() : this.Result.Password;
            this.Result.Salt = Monnit.MonnitUtil.GenerateSalt();
            this.Result.WorkFactor = ConfigData.AppSettings("WorkFactor").ToInt();
            this.Result.Password2 = Monnit.MonnitUtil.GenerateHash(password1, this.Result.Salt, this.Result.WorkFactor);
            this.Result.Password = string.Empty;
            this.Result.Save();
          }
          if (StructuralComparisons.StructuralEqualityComparer.Equals((object) this.Result.Password2, (object) Monnit.MonnitUtil.GenerateHash(password, this.Result.Salt, this.Result.WorkFactor)))
          {
            Monnit.AuthenticationLog.Log(this.Username, this.Result.CustomerID, true, application, ipAddress);
            return;
          }
          Monnit.AuthenticationLog.Log(this.Username, this.Result.CustomerID, false, application, ipAddress);
          this.Result = (Monnit.Customer) null;
          return;
        }
      }
      finally
      {
        if (enumerator is IDisposable disposable)
          disposable.Dispose();
      }
      Monnit.AuthenticationLog.Log(this.Username, long.MinValue, false, application, ipAddress);
      this.Result = (Monnit.Customer) null;
    }
  }

  [DBMethod("Customer_LoadAllByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT DISTINCT\r\n  cust.* \r\nFROM dbo.[Customer] cust\r\nLEFT JOIN dbo.[CustomerAccountLink] cal ON cal.CustomerID = cust.CustomerID AND cal.AccountID = @AccountID AND cal.AccountDeleted != 1\r\nWHERE cust.IsDeleted  = 0\r\n  AND (cal.AccountID  = @AccountID \r\n    OR cust.AccountID = @AccountID);\r\n")]
  internal class LoadAllByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<Monnit.Customer> Result { get; private set; }

    public LoadAllByAccountID(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.Customer>(this.ToDataTable());
    }
  }

  [DBMethod("Customer_LoadByAccountAndSamlNameID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT DISTINCT\r\n  cust.* \r\nFROM dbo.[Customer] cust\r\nLEFT JOIN dbo.[CustomerAccountLink] cal ON cal.CustomerID = cust.CustomerID AND cal.AccountID = @AccountID AND cal.AccountDeleted != 1\r\nWHERE cust.IsDeleted  = 0\r\n  AND (cal.AccountID  = @AccountID \r\n    OR cust.AccountID = @AccountID)\r\n  AND cust.SamlNameID = @SamlNameID;\r\n")]
  internal class LoadByAccountAndSamlNameID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("SamlNameID", typeof (string))]
    public string SamlNameID { get; private set; }

    public Monnit.Customer Result { get; private set; }

    public LoadByAccountAndSamlNameID(long accountID, string samlNameID)
    {
      this.AccountID = accountID;
      this.SamlNameID = samlNameID;
      this.Result = BaseDBObject.Load<Monnit.Customer>(this.ToDataTable()).FirstOrDefault<Monnit.Customer>();
    }
  }

  [DBMethod("Customer_DeleteByCustomerID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @ProcName NVARCHAR(50);\r\n\r\nDECLARE @ErrorNum         INT,          \r\n        @ErrorProcedure   NVARCHAR(50), \r\n        @ErrorSysMsg      NVARCHAR(MAX),\r\n        @UserName VARCHAR(64);\r\n\r\n\r\nDECLARE @AccountID BIGINT,\r\n        @PrimaryContactID BIGINT,\r\n        @NotificationEmail VARCHAR(255),\r\n        @NotificationPhone VARCHAR(255),\r\n        @NotificationPhone2 VARCHAR(255),\r\n        @RV INT,\r\n        @IsCFRCompliant BIT\r\n\r\nSET @RV = 0;\r\n\r\nBEGIN TRY\r\n\r\n    SET @ProcName = OBJECT_NAME(@@PROCID);\r\n\r\n    --If this customer is a primary contact, do nothing.\r\n    IF NOT EXISTS (SELECT a.PrimaryContactID FROM dbo.[Account] a WHERE a.PrimaryContactID = @CustomerID)\r\n    BEGIN \r\n\r\n        SET @AccountID        = (SELECT AccountID        FROM dbo.[Customer] WHERE CustomerID = @CustomerID);\r\n        SET @PrimaryContactID = (SELECT PrimaryContactID FROM dbo.[Account]  WHERE AccountID  = @AccountID);\r\n        SET @UserName = NEWID();\r\n\r\n        SELECT\r\n          @NotificationEmail = NotificationEmail,\r\n          @NotificationPhone = NotificationPhone,\r\n          @NotificationPhone2 = NotificationPhone2\r\n        FROM dbo.[Customer] WITH (NOLOCK)\r\n        WHERE CustomerID = @CustomerID\r\n\r\n        /***********************************************************\r\n                          Customer Record Updates\r\n\r\n          Don't hard deleted these records; just remove the\r\n          customer from them. (set to primary on acct in some cases)\r\n        ***********************************************************/\r\n        UPDATE dbo.[Notification] \r\n          SET CustomerToNotifyID = NULL  \r\n        WHERE CustomerToNotifyID = @CustomerID;\r\n    \r\n        UPDATE dbo.[CreditSetting] \r\n          SET UserID = @PrimaryContactID \r\n        WHERE UserId = @CustomerID;\r\n    \r\n        UPDATE dbo.[ExternalSubscriptionPreference] \r\n          SET UserID = @PrimaryContactID \r\n        WHERE UserId = @CustomerID;\r\n    \r\n        UPDATE dbo.[Customer] \r\n          SET Username                           = @UserName,\r\n              Password                           = 'Deleted',\r\n              FirstName                          = 'Deleted',\r\n              LastName                           = 'User',\r\n              NotificationEmail                  = 'invalid@invalid.com',\r\n              NotificationPhone                  = NULL,\r\n              NotificationPhone2                 = NULL,\r\n              SendSensorNotificationToText       = 0,\r\n              SendSensorNotificationToVoice      = 0,                 \r\n              PasswordExpired                    = 1,\r\n              IsAdmin                            = 0,\r\n              IsDeleted                          = 1,\r\n              IsActive                           = 0,\r\n              DirectSMS                          = 0,\r\n              DeleteDate                         = GETUTCDATE(),\r\n              SamlNameID                         = NULL,\r\n              ForceLogoutDate                    = GETUTCDATE()\r\n        WHERE CustomerID = @CustomerID;\r\n\r\n        UPDATE dbo.[AuthenticationLog] SET UserName = @UserName WHERE CustomerID = @CustomerID\r\n        INSERT INTO dbo.[AuthenticationLog] (UserName, CustomerID, LogDate, Success, Application)\r\n        VALUES(@UserName, @CustomerID, GETUTCDATE(), 1, 'DeleteCustomer')\r\n\r\n        /*************************************************\r\n                      Customer Record Deletes\r\n\r\n                These records are ok to hard delete.\r\n        ***************************************************/\r\n        DELETE gr \r\n        FROM dbo.[CustomerGroupRecipient] gr\r\n        INNER JOIN dbo.[CustomerGroupLink] cl ON gr.CustomerGroupLinkID = cl.CustomerGroupLinkID\r\n        WHERE cl.CustomerID = @CustomerID\r\n\r\n        DELETE dbo.[AdvertisementRecorded]  WHERE CustomerID = @CustomerID;\r\n\r\n        DELETE dbo.[CustomerInformation]    WHERE CustomerID = @CustomerID;\r\n\r\n        DELETE dbo.[CustomerMobileDevice]   WHERE CustomerID = @CustomerID;\r\n\r\n        DELETE dbo.[CustomerPermission]     WHERE CustomerID = @CustomerID;\r\n\r\n        DELETE dbo.[CustomerPreference]     WHERE CustomerID = @CustomerID;\r\n\r\n        DELETE dbo.[CustomerRememberMe]     WHERE CustomerID = @CustomerID;\r\n\r\n        DELETE dbo.[NotificationRecipient]  WHERE CustomerToNotifyID = @CustomerID;\r\n        \r\n        DELETE dbo.[CustomerGroupLink]      WHERE CustomerID = @CustomerID;\r\n\r\n        DELETE dbo.[Preference]             WHERE CustomerID = @CustomerID;\r\n\r\n        DELETE dbo.[ReportDistribution]     WHERE CustomerID = @CustomerID;\r\n\r\n        DELETE dbo.[Dashboard]              WHERE UserID     = @CustomerID;\r\n\r\n        /*************************************************************\r\n\r\n          Audit log records store previous customer records in a JSON\r\n          Rather than going through and editing the JSON strings and\r\n          doing string compares, we elected to do hard deletes if the\r\n          customer record was requested to be deleted. - NLN\r\n\r\n          Don't do deletes on AuditLog at the moment; double checking\r\n          CFR law vs GDPR Law -- NLN 2018-05-29\r\n        **************************************************************/\r\n        --SET @IsCFRCompliant = (SELECT ISNULL(IsCFRCompliant, 0) FROM dbo.[Account] where AccountID = @AccountID)\r\n\r\n        --DELETE dbo.[AuditLog]\r\n        --WHERE AuditObject = 6 --Customer record audit\r\n        --  AND ObjectID = @CustomerID\r\n\r\n        SET @RV = 1\r\n        \r\n    END\r\n\r\nEND TRY\r\nBEGIN CATCH\r\n\r\n\r\n\tSET @ErrorNum = ERROR_NUMBER();\r\n\tSET @ErrorProcedure = ERROR_PROCEDURE();\r\n\tSET @ErrorSysMsg = ERROR_MESSAGE();\r\n\r\n  DECLARE @Params NVARCHAR(1000) \r\n  SET @Params = '@CustomerID: ' + CONVERT(VARCHAR(20), @CustomerID)\r\n\r\n  INSERT INTO dbo.[DBErrorLog] (ProcName, Date, Urgency, Message, Params)\r\n  VALUES (@ProcName, GETUTCDATE(), 10, @ErrorSysMsg, @Params)\r\n\r\nEND CATCH\r\n\r\nSELECT @RV;\r\n")]
  internal class DeleteByCustomerID : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    public DataTable Result { get; private set; }

    public DeleteByCustomerID(long customerID)
    {
      this.CustomerID = customerID;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("Customer_PrimaryContactCheck")]
  [DBMethodBody(DBMS.SqlServer, "\r\nALTER PROCEDURE [dbo].[Customer_PrimaryContactCheck]\r\n    @CustomerID INT\r\nAS\r\nBEGIN\r\n    DECLARE @Result BIT;\r\n\r\n    -- Check if any matching record exists\r\n    IF EXISTS (\r\n        SELECT 1\r\n        FROM Account A\r\n        WHERE A.PrimaryContactID = @CustomerID\r\n    )\r\n    BEGIN\r\n        SET @Result = 1; \r\n    END\r\n    ELSE\r\n    BEGIN\r\n        SET @Result = 0; \r\n    END\r\n\r\n    -- Return the result\r\n    SELECT @Result AS IsPrimaryContact;\r\nEND\r\n\r\n")]
  internal class PrimaryContactCheck : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    public bool Result { get; private set; }

    public PrimaryContactCheck(long customerID)
    {
      this.CustomerID = customerID;
      this.Result = this.ToScalarValue<bool>();
    }
  }

  [DBMethod("Customer_LoadAllByEmail")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[Customer]\r\nWHERE NotificationEmail = @Email\r\n  AND IsDeleted         = 0;\r\n")]
  internal class LoadAllByEmail : BaseDBMethod
  {
    [DBMethodParam("Email", typeof (string))]
    public string Email { get; private set; }

    public List<Monnit.Customer> Result { get; private set; }

    public LoadAllByEmail(string email)
    {
      this.Email = email;
      this.Result = BaseDBObject.Load<Monnit.Customer>(this.ToDataTable());
    }
  }

  [DBMethod("Customer_LastViewedReleaseNote")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP 1\r\n  *\r\nFROM dbo.[ReleaseNoteViewed]\r\nWHERE CustomerID = @CustomerID\r\nORDER BY\r\n  ReleaseNoteID DESC;\r\n")]
  internal class LastViewedReleaseNote : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    public ReleaseNoteViewed Result { get; private set; }

    public LastViewedReleaseNote(long custid)
    {
      this.CustomerID = custid;
      this.Result = BaseDBObject.Load<ReleaseNoteViewed>(this.ToDataTable()).FirstOrDefault<ReleaseNoteViewed>();
    }
  }

  [DBMethod("Customer_CheckUsernameIsUnique")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  COUNT(*)\r\nFROM dbo.[Customer]\r\nWHERE Username = @Username;\r\n")]
  internal class CheckUsernameIsUnique : BaseDBMethod
  {
    [DBMethodParam("Username", typeof (string))]
    public string Username { get; private set; }

    public bool Result { get; private set; }

    public CheckUsernameIsUnique(string username)
    {
      this.Username = username;
      this.Result = this.ToScalarValue<int>() == 0;
    }
  }

  [DBMethod("Customer_CheckNotificationEmailIsUnique")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @NCustomers INT;\r\n\r\nSET @NCustomers = (\r\n\t\t\tSELECT COUNT(*)\r\n\t\t\tFROM dbo.[Customer]\r\n\t\t\tWHERE NotificationEmail = @NotificationEmail\r\n\t\t)\r\n\r\nSELECT\r\nCASE\r\n\tWHEN @NCustomers = 0 THEN (\r\n\t-- No records, no additional evaluation needed\r\n\t\t1\r\n\t)\r\n\tWHEN @CustomerID IS NULL AND @NCustomers > 0 THEN (\r\n\t-- Already in use by another customer\r\n\t\t\t0\r\n\t)\r\n\t-- Check if this customer is already tied to this email\r\n\tELSE\r\n\t\t(\r\n\t\t\tSELECT COUNT(*)\r\n\t\t\tFROM dbo.[Customer]\r\n\t\t\tWHERE NotificationEmail = @NotificationEmail\r\n\t\t\tAND CustomerID = @CustomerID\r\n\t\t)\r\nEND\r\n")]
  internal class CheckNotificationEmailIsUnique : BaseDBMethod
  {
    [DBMethodParam("NotificationEmail", typeof (string))]
    public string NotificationEmail { get; private set; }

    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    public bool Result { get; private set; }

    public CheckNotificationEmailIsUnique(string notificationEmail, long? customerID)
    {
      this.NotificationEmail = notificationEmail;
      this.CustomerID = customerID ?? long.MinValue;
      this.Result = this.ToScalarValue<int>() >= 1;
    }
  }

  [DBMethod("Customer_LoadFromUsername")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[Customer] WHERE Username = @Username;\r\n")]
  internal class LoadFromUsername : BaseDBMethod
  {
    [DBMethodParam("Username", typeof (string))]
    public string Username { get; private set; }

    public Monnit.Customer Result { get; private set; }

    public LoadFromUsername(string username)
    {
      this.Username = username;
      this.Result = BaseDBObject.Load<Monnit.Customer>(this.ToDataTable()).FirstOrDefault<Monnit.Customer>();
    }
  }

  [DBMethod("Customer_Search")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  a.[RetailAccountID],\r\n  c.[CustomerID],\r\n  c.[IsActive],\r\n  c.[UserName],\r\n  c.[FirstName],\r\n  c.[LastName],\r\n  c.[NotificationEmail],\r\n  c.[AccountID],\r\n  [Phone]                 = ISNULL(c.[NotificationPhone], ''),\r\n  a.[AccountNumber], \r\n  [Retail]                = ISNULL(r.[AccountNumber], ''),\r\n  [Host]                  = ISNULL(t.[Domain],'')\r\nFROM dbo.[Customer] c\r\nINNER JOIN dbo.[Account] a     ON a.AccountID = c.AccountID\r\nLEFT JOIN dbo.[Account] r      ON r.AccountID = a.RetailAccountID\r\nLEFT JOIN dbo.[AccountTheme] t ON (t.AccountID = a.RetailAccountID OR t.AccountID = a.AccountID)\r\nWHERE c.IsDeleted = 0\r\n  AND (c.NotificationEmail  LIKE '%' + @Query + '%'\r\n\t OR c.FirstName           LIKE '%' + @Query + '%'\r\n\t OR c.LastName            LIKE '%' + @Query + '%'\r\n\t OR c.Username            LIKE '%' + @Query + '%'\r\n\t OR c.NotificationPhone   LIKE '%' + @Query + '%');\r\n")]
  internal class Search : BaseDBMethod
  {
    [DBMethodParam("Query", typeof (string))]
    public string Query { get; private set; }

    public DataTable Result { get; private set; }

    public Search(string query)
    {
      this.Query = query;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("Customer_SearchPotentialNotificationRecipient")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @RootAccount         BIGINT,\r\n        @RootAccountParent   BIGINT,\r\n        @NotificationAccount BIGINT;\r\n\r\nSELECT @RootAccount         = [AccountID] FROM dbo.[Customer] WHERE [CustomerID] = @CustomerID;\r\nSELECT @RootAccountParent   = [RetailAccountID] FROM dbo.[Account] WHERE [AccountID] = @RootAccount;\r\nSELECT @NotificationAccount = [AccountID] FROM dbo.[Notification] WHERE [NotificationID] = @NotificationID;\r\n\r\nWITH cteAccounts AS\r\n(\r\n  -->>>>>>>>>>Block 1>>>>>>>>>>>>>>>>>\r\n  -- In a rCTE, this block is called an [Anchor]\r\n  -- The query finds all root nodes as described by WHERE ManagerID IS NULL\r\n  SELECT\r\n    *,\r\n    [Level] = 1\r\n  FROM dbo.[Account]\r\n  WHERE AccountID = @NotificationAccount\r\n  -->>>>>>>>>>Block 1>>>>>>>>>>>>>>>>>\r\n  UNION ALL\r\n  -->>>>>>>>>>Block 2>>>>>>>>>>>>>>>>>    \r\n  -- This is the recursive expression of the rCTE\r\n  -- On the first 'execution' it will query data in [Account],\r\n  -- relative to the [Anchor] above.\r\n  -- This will produce a resultset, we will call it R{1} and it is JOINed to [cteAccounts]\r\n  -- as defined by the hierarchy\r\n  -- Subsequent 'executions' of this block will reference R{n-1}\r\n  SELECT\r\n    a.*,\r\n    [Level] = [Level]+1\r\n  FROM dbo.[Account] a\r\n  INNER JOIN cteAccounts ctea ON ctea.[RetailAccountID] = a.[AccountID]\r\n  WHERE a.[AccountID] != @RootAccountParent \r\n     OR @RootAccountParent IS NULL\r\n  -->>>>>>>>>>Block 2>>>>>>>>>>>>>>>>>\r\n)\r\n\r\nSELECT DISTINCT \r\n  a.[AccountID],\r\n  a.[AccountNumber],\r\n  a.[CompanyName],\r\n  c.[CustomerID],\r\n  c.[UserName],\r\n  c.[FirstName],\r\n  c.[LastName],\r\n  c.[NotificationEmail],\r\n  c.[SendSensorNotificationToText],\r\n  c.[NotificationPhone],\r\n  c.[SendSensorNotificationToVoice],\r\n  c.[NotificationPhone2],\r\n  a.[Level],\r\n  GroupActive = null,\r\n  CustomerGroupID = null\r\nFROM cteAccounts a\r\nINNER JOIN dbo.[Customer] c ON c.[AccountID] = a.[AccountID]-- OR c.CustomerID = cal.CustomerID\r\nLEFT JOIN dbo.[NotificationRecipient] nr ON nr.[NotificationID] = @NotificationID AND nr.[CustomerToNotifyID] = c.CustomerID\r\nWHERE c.[IsDeleted] = 0\r\n  AND (@Query IS NULL \r\n   OR c.[NotificationEmail]              LIKE '%' + @Query + '%'\r\n   OR c.[Username]                       LIKE '%' + @Query + '%'\r\n   OR c.[NotificationPhone]              LIKE '%' + @Query + '%'\r\n   OR c.[FirstName] + ' ' + c.[LastName] LIKE '%' + @Query + '%')\r\n  AND ISNULL(c.NotificationOptIn, '2018-01-01 12:00:00') > ISNULL(c.NotificationOptOut, '1900-01-01 12:00:00')\r\n  union \r\n  SELECT\r\n  a.AccountID,\r\n  a.AccountNumber,\r\n  a.CompanyName,\r\n  CustomerID = null,\r\n  Username = cg.name,\r\n  FirstName = cg.name,\r\n  LastName = NULL,\r\n  NotificationEmail = NULL,\r\n  [SendSensorNotificationToText] = NULL,\r\n  [NotificationPhone] = NULL,\r\n  [SendSensorNotificationToVoice] = NULL,\r\n  [NotificationPhone2] = NULL,\r\n  [Level]                           = 0,\r\n  GroupActive = CASE WHEN nr.CustomerGroupID IS NOT NULL THEN 1 ELSE 0 END,\r\n  cg.customergroupid\r\nFROM CustomerGroup cg\r\nINNER JOIN Account a on cg.accountid = a.accountid\r\nINNER JOIN cteAccounts ct on a.accountID = ct.AccountID\r\nleft join NotificationRecipient nr on cg.CustomerGroupID = nr.CustomerGroupID and nr.NotificationID = @NotificationID\r\nWHERE (@Query IS NULL \r\n   OR cg.[Name]              LIKE '%' + @Query + '%')\r\nUNION\t\r\nSELECT DISTINCT\r\n  a.[AccountID],\r\n  a.[AccountNumber],\r\n  a.[CompanyName],\r\n  c.[CustomerID],\r\n  c.[UserName],\r\n  c.[FirstName],\r\n  c.[LastName],\r\n  c.[NotificationEmail],\r\n  c.[SendSensorNotificationToText],\r\n  c.[NotificationPhone],\r\n  c.[SendSensorNotificationToVoice],\r\n  c.[NotificationPhone2],\r\n  [Level]                           = 1,\r\n  GroupActive = null,\r\n  CustomerGroupID = null\r\nFROM dbo.[Account] a\r\nINNER JOIN dbo.[CustomerAccountLink] cal ON cal.[AccountID] = a.[AccountID] AND cal.RequestConfirmed = 1 AND cal.[CustomerDeleted] = 0 AND cal.[AccountDeleted] = 0\r\nINNER JOIN dbo.[Customer] c              ON c.[AccountID] = a.[AccountID] OR c.[CustomerID] = cal.[CustomerID]\r\nLEFT JOIN dbo.[NotificationRecipient] nr ON nr.[NotificationID] = @NotificationID AND nr.[CustomerToNotifyID] = c.[CustomerID]\r\nWHERE a.AccountID = @NotificationAccount\r\n  AND c.[IsDeleted] = 0\r\n  AND (@Query IS NULL \r\n   OR c.[NotificationEmail]              LIKE '%' + @Query + '%'\r\n   OR c.[Username]                       LIKE '%' + @Query + '%'\r\n   OR c.[NotificationPhone]              LIKE '%' + @Query + '%'\r\n   OR c.[FirstName] + ' ' + c.[LastName] LIKE '%' + @Query + '%')\t\r\n  AND ISNULL(c.NotificationOptIn, '2018-01-01 12:00:00') > ISNULL(c.NotificationOptOut, '1900-01-01 12:00:00')\t\r\nORDER BY\r\n  a.[Level],\r\n  a.[CompanyName],\r\n  c.[FirstName],\r\n  c.[LastName];\r\n")]
  internal class SearchPotentialNotificationRecipient : BaseDBMethod
  {
    [DBMethodParam("Query", typeof (string))]
    public string Query { get; private set; }

    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    public DataTable Result { get; private set; }

    public SearchPotentialNotificationRecipient(long customerID, long notificationID, string query)
    {
      this.CustomerID = customerID;
      this.NotificationID = notificationID;
      this.Query = query;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("Customer_LoadByCustomerGroupID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT DISTINCT\r\n  c.*\r\nFROM dbo.[CustomerGroupLink] l WITH (NOLOCK)\r\nINNER JOIN dbo.[Customer] c WITH (NOLOCK) ON l.CustomerID = c.CustomerID\r\nWHERE l.CustomerGroupID = @CustomerGroupID;\r\n")]
  internal class LoadByGroupID : BaseDBMethod
  {
    [DBMethodParam("CustomerGroupID", typeof (long))]
    public long CustomerGroupID { get; private set; }

    public List<Monnit.Customer> Result { get; private set; }

    public LoadByGroupID(long customerGroupID)
    {
      this.CustomerGroupID = customerGroupID;
      this.Result = BaseDBObject.Load<Monnit.Customer>(this.ToDataTable());
    }
  }

  [DBMethod("Customer_SetForceLogoutDate")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @RV BIT = 0\r\n\r\nBEGIN TRY\r\n\r\n  UPDATE dbo.[Customer]\r\n  SET ForceLogoutDate = GETUTCDATE()\r\n  WHERE CustomerID = @CustomerID;\r\n\r\n  IF @@ROWCOUNT > 0\r\n    SET @Rv = 1\r\n\r\n  SELECT RV = @RV;\r\n\r\nEND TRY\r\nBEGIN CATCH\r\n\r\n  SELECT RV = @RV;\r\n\r\nEND CATCH\r\n")]
  internal class SetForceLogoutDate : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    public int Result { get; private set; }

    public SetForceLogoutDate(long customerID)
    {
      this.CustomerID = customerID;
      this.Result = this.ToScalarValue<int>();
    }
  }

  [DBMethod("Customer_LoadAllForReseller")]
  [DBMethodBody(DBMS.SqlServer, "\r\n/************************************************\r\n ------------------------------------------------\r\n  @CustomerID      - customer who is logged in\r\n  @TargetAccountID - the account or subaccount \r\n                     that is being accessed \r\n                     by the Customer\r\n\r\n\r\n  The purpose of this proc is to access all \r\n  customer objects belonging to the customer's\r\n  account, the sub account they are accessing (if\r\n  it's on the same tree) and every account \r\n  on the tree in between.\r\n\r\n  Example:\r\n              1\r\n        /           \\\r\n      2               3\r\n    /   \\           /   \\\r\n  4      5         6      7\r\n                            \\\r\n                              10\r\n  If customer on account 1 is accessing account 7,\r\n  load all customers belonging to account 1, 3 and 7;\r\n--------------------------------------------------\r\n**************************************************/\r\n\r\nDECLARE @AccountID VARCHAR(30);\r\nDECLARE @PrimaryContactID BIGINT;\r\n\r\n--Get the account id primarily associated with the customer that is logged in\r\nSELECT \r\n  @AccountID = CONVERT(VARCHAR(30), a.AccountID )\r\nFROM dbo.[Account] a WITH (NOLOCK)\r\nINNER JOIN dbo.[Customer] c WITH (NOLOCK) ON a.AccountID = c.AccountID\r\nWHERE CustomerID = @CustomerID;\r\n\r\nSET @PrimaryContactID = (SELECT PrimaryContactID FROM Account where AccountID = @TargetAccountID)\r\n\r\n\r\n--Check to see if the Target account is under the customer's primary account. If it isn't return an empty customer object\r\nIF EXISTS (SELECT * FROM dbo.[Account] a WHERE AccountID = @TargetAccountID AND AccountIDTree LIKE '%*'+@AccountID+'*%')\r\nBEGIN\r\n\r\n  PRINT 1;\r\n\r\n  WITH cte_results AS\r\n  (\r\n    SELECT \r\n    AccountIDTree =  REPLACE(SUBSTRING(REPLACE(AccountIDTree, '*' + @AccountID + '*', '*|*'), charindex('|',REPLACE(AccountIDTree, '*' + @AccountID + '*', '*|*')), LEN(AccountIDTree)), '|', @AccountID)\r\n      --AccountIDTree =  CASE WHEN @AccountID + '*' + dbo.SplitIndex('       ' + AccountIDTree, '*'+@AccountID+'*', 1) IS NOT NULL THEN @AccountID + '*' + dbo.SplitIndex('       ' + AccountIDTree, '*'+@AccountID+'*', 1) ELSE  dbo.SplitIndex(AccountIDTree, '*'+@AccountID+'*', 0) END\r\n    FROM dbo.[Account] a WITH (NOLOCK)\r\n    WHERE a.AccountID = @TargetAccountID\r\n  )\r\n  SELECT  \r\n    c2.*\r\n  FROM cte_results c\r\n  CROSS APPLY dbo.Split(AccountIDTree, '*') t\r\n  INNER JOIN dbo.[Customer] c2 WITH (NOLOCK) ON t.Item = c2.AccountID\r\n  WHERE c2.IsDeleted = 0\r\n  UNION ALL\r\n  SELECT\r\n    c.*\r\n  FROM Customer c\r\n  WHERE c.CustomerID = @PrimaryContactID\r\n  AND (SELECT AccountIDTree from cte_Results) not like '%*'+CONVERT(varchar(30), c.AccountID) +'*%'\r\n\r\nEND ELSE\r\nBEGIN\r\n\r\n  --if this logic is being thrown it means the subaccount is no\r\n  --directly under the account the customer is on\r\n  SELECT top 0 \r\n  * \r\n  FROM dbo.[Customer] with (NOLOCK);\r\n\r\nEND\r\n")]
  internal class LoadAllForReseller : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("TargetAccountID", typeof (long))]
    public long TargetAccountID { get; private set; }

    public List<Monnit.Customer> Result { get; private set; }

    public LoadAllForReseller(long customerID, long targetAccountID)
    {
      this.CustomerID = customerID;
      this.TargetAccountID = targetAccountID;
      this.Result = BaseDBObject.Load<Monnit.Customer>(this.ToDataTable());
    }
  }

  [DBMethod("Customer_CheckForceLogoutDate")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  c.ForceLogoutDate\r\nFROM dbo.[Customer] c \r\nWHERE c.CustomerID = @CustomerID;\r\n")]
  internal class CheckForceLogoutDate : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    public DateTime Result { get; private set; }

    public CheckForceLogoutDate(long customerID)
    {
      this.CustomerID = customerID;
      this.Result = this.ToScalarValue<DateTime>();
    }
  }
}
