// Decompiled with JetBrains decompiler
// Type: Monnit.Account
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;

#nullable disable
namespace Monnit;

[MetadataType(typeof (AccountMetadata))]
[DBClass("Account")]
public class Account : BaseDBObject
{
  private long _AccountID = long.MinValue;
  private string _AccountNumber = string.Empty;
  private string _CompanyName = string.Empty;
  private string _EULAVersion = string.Empty;
  private DateTime _EULADate = DateTime.MinValue;
  private long _PrimaryContactID = long.MinValue;
  private long _TimeZoneID = long.MinValue;
  private bool _IsReseller = false;
  private bool _IsCFRCompliant = false;
  private long _RetailAccountID = long.MinValue;
  private bool _IsAdvertiser = false;
  private bool _IsPremium = false;
  private bool _HideData = false;
  private bool _AutoPurchase = false;
  private long _AutoBill = long.MinValue;
  private Account _RetailAccount = (Account) null;
  private DateTime _PremiumValidUntil;
  private DateTime _CreateDate;
  private long _IndustryTypeID = long.MinValue;
  private long _BusinessTypeID = long.MinValue;
  private string _PurchaseLocation = string.Empty;
  private string _CustomField_01 = string.Empty;
  private string _CustomField_02 = string.Empty;
  private string _CustomField_03 = string.Empty;
  private string _CustomField_04 = string.Empty;
  private string _CustomField_05 = string.Empty;
  private int _PasswordLifetime = int.MinValue;
  private int _MaxFailedLogins = -1;
  private long _ExternalDataSubscriptionID = long.MinValue;
  private string _AccountIDTree = string.Empty;
  private string _Tags = string.Empty;
  private string _AccessToken = string.Empty;
  private DateTime _AccessTokenExpirationDate = DateTime.MinValue;
  private bool _APIRevoked = false;
  private Guid _StoreLinkGuid = Guid.Empty;
  private long _StoreUserID = long.MinValue;
  private string _RecoveryEmail = string.Empty;
  private string _VerificationCode = string.Empty;
  private DateTime _VerificationDate = DateTime.MinValue;
  private int _APICallLimit = int.MaxValue;
  private bool _IsHxEnabled = false;
  private long _SamlEndpointID = long.MinValue;
  private bool _AllowCertificateNotifications = true;
  private int _CurrentAPICounter = 0;
  private bool _APIActive = false;
  private DateTime _LastAPICallDate = DateTime.MinValue;
  private Customer _PrimaryContact;
  private AccountAddress _PrimaryAddress;
  private string _TimeZoneDisplayName;
  private long _DefaultPaymentID = long.MinValue;
  private DateTime _AutoBillStartDate;
  private DateTime _AutoBillCancelDate;
  private List<AccountPermission> _Permissions;
  private long _LanguageID = 1;
  private List<AccountSubscription> _Subscriptions = (List<AccountSubscription>) null;
  private ExternalDataSubscription _ExternalDataSubscription = (ExternalDataSubscription) null;

  [DBProp("AccountID", IsPrimaryKey = true)]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("EULAVersion")]
  public string EULAVersion
  {
    get => this._EULAVersion;
    set => this._EULAVersion = value;
  }

  [DBProp("EULADate")]
  public DateTime EULADate
  {
    get => this._EULADate;
    set => this._EULADate = value;
  }

  [DBProp("AccountNumber", AllowNull = false, MaxLength = 255 /*0xFF*/)]
  public string AccountNumber
  {
    get => this._AccountNumber;
    set
    {
      if (value == null)
        this._AccountNumber = string.Empty;
      else
        this._AccountNumber = value;
    }
  }

  [DBProp("CompanyName", MaxLength = 255 /*0xFF*/)]
  public string CompanyName
  {
    get => this._CompanyName;
    set
    {
      if (value == null)
        this._CompanyName = string.Empty;
      else
        this._CompanyName = value;
    }
  }

  [DBForeignKey("Customer", "CustomerID")]
  [DBProp("PrimaryContactID")]
  public long PrimaryContactID
  {
    get => this._PrimaryContactID;
    set => this._PrimaryContactID = value;
  }

  [DBForeignKey("TimeZone", "TimeZoneID")]
  [DBProp("TimeZoneID", AllowNull = false)]
  public long TimeZoneID
  {
    get => this._TimeZoneID;
    set => this._TimeZoneID = value;
  }

  [DBProp("IsReseller", AllowNull = true)]
  public bool IsReseller
  {
    get => this._IsReseller;
    set => this._IsReseller = value;
  }

  [DBProp("IsCFRCompliant", AllowNull = true)]
  public bool IsCFRCompliant
  {
    get => this._IsCFRCompliant;
    set => this._IsCFRCompliant = value;
  }

  [DBProp("IsAdvertiser", AllowNull = true)]
  public bool IsAdvertiser
  {
    get => this._IsAdvertiser;
    set => this._IsAdvertiser = value;
  }

  [DBProp("IsPremium", AllowNull = true)]
  public bool IsPremium
  {
    get
    {
      return this.CurrentSubscription.AccountSubscriptionTypeID > 1L && this.CurrentSubscription.IsActive;
    }
    set => this._IsPremium = value;
  }

  [DBProp("HideData", AllowNull = true)]
  public bool HideData
  {
    get => this._HideData;
    set => this._HideData = value;
  }

  [DBProp("AutoPurchase", AllowNull = true)]
  public bool AutoPurchase
  {
    get => this._AutoPurchase;
    set => this._AutoPurchase = value;
  }

  [DBForeignKey("Account", "AccountID")]
  [DBProp("RetailAccountID", AllowNull = true)]
  public long RetailAccountID
  {
    get => this._RetailAccountID;
    set => this._RetailAccountID = value;
  }

  public Account RetailAccount
  {
    get
    {
      if (this._RetailAccount == null)
        this._RetailAccount = Account.Load(this.RetailAccountID);
      return this._RetailAccount;
    }
  }

  [DBProp("PremiumValidUntil", AllowNull = true)]
  public DateTime PremiumValidUntil
  {
    get
    {
      return this.CurrentSubscription.AccountSubscriptionTypeID > 1L && this.CurrentSubscription.IsActive ? this.CurrentSubscription.ExpirationDate : DateTime.UtcNow.AddDays(-15.0);
    }
    set => this._PremiumValidUntil = value;
  }

  [DBProp("CreateDate", AllowNull = true)]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("IndustryTypeID")]
  public long IndustryTypeID
  {
    get => this._IndustryTypeID;
    set => this._IndustryTypeID = value;
  }

  [DBProp("BusinessTypeID")]
  public long BusinessTypeID
  {
    get => this._BusinessTypeID;
    set => this._BusinessTypeID = value;
  }

  [DBProp("PurchaseLocation", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string PurchaseLocation
  {
    get => this._PurchaseLocation;
    set => this._PurchaseLocation = value;
  }

  [DBProp("CustomField_01", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string CustomField_01
  {
    get => this._CustomField_01;
    set => this._CustomField_01 = value;
  }

  [DBProp("CustomField_02", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string CustomField_02
  {
    get => this._CustomField_02;
    set => this._CustomField_02 = value;
  }

  [DBProp("CustomField_03", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string CustomField_03
  {
    get => this._CustomField_03;
    set => this._CustomField_03 = value;
  }

  [DBProp("CustomField_04", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string CustomField_04
  {
    get => this._CustomField_04;
    set => this._CustomField_04 = value;
  }

  [DBProp("CustomField_05", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string CustomField_05
  {
    get => this._CustomField_05;
    set => this._CustomField_05 = value;
  }

  [DBProp("PasswordLifetime")]
  public int PasswordLifetime
  {
    get => this._PasswordLifetime;
    set => this._PasswordLifetime = value;
  }

  public bool PasswordLifetimeEnforced() => this.PasswordLifetime > 0;

  public bool PasswordExpired(DateTime LastUpdated)
  {
    return this.PasswordLifetimeEnforced() && LastUpdated.AddDays((double) this.PasswordLifetime) < DateTime.UtcNow;
  }

  [DBProp("MaxFailedLogins")]
  public int MaxFailedLogins
  {
    get
    {
      if (this._MaxFailedLogins < -1)
        this._MaxFailedLogins = -1;
      return this._MaxFailedLogins;
    }
    set => this._MaxFailedLogins = value;
  }

  public bool CustomerLockEnforced() => this.MaxFailedLogins > 0;

  public bool CustomerLocked(int failCount)
  {
    return this.CustomerLockEnforced() && failCount > this.MaxFailedLogins;
  }

  [DBProp("ExternalDataSubscriptionID", AllowNull = true, DefaultValue = null)]
  [DBForeignKey("ExternalDataSubscription", "ExternalDataSubscriptionID")]
  public long ExternalDataSubscriptionID
  {
    get => this._ExternalDataSubscriptionID;
    set => this._ExternalDataSubscriptionID = value;
  }

  [DBProp("AccountIDTree", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string AccountIDTree
  {
    get => this._AccountIDTree;
    set => this._AccountIDTree = value;
  }

  [DBProp("Tags", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Tags
  {
    get => this._Tags;
    set => this._Tags = value;
  }

  public string[] TagArray
  {
    get
    {
      if (this.Tags == null)
        this.Tags = "";
      return this.Tags.Split(new char[1]{ '|' }, StringSplitOptions.RemoveEmptyEntries);
    }
  }

  public void AddTag(string tag)
  {
    this.Tags = $"{this.Tags}|{tag}";
    this.Save();
  }

  public void RemoveTag(string tag)
  {
    string[] tagArray = this.TagArray;
    this.Tags = string.Empty;
    foreach (string str in tagArray)
    {
      if (str.ToLower().Trim() != tag.ToLower().Trim())
        this.Tags = $"{this.Tags}|{str}";
    }
    this.Save();
  }

  public bool HasTag(string tag)
  {
    foreach (string tag1 in this.TagArray)
    {
      if (tag1.ToLower().Trim() == tag.ToLower().Trim())
        return true;
    }
    return false;
  }

  [DBProp("AccessToken", MaxLength = 10, AllowNull = true)]
  public string AccessToken
  {
    get => this._AccessToken;
    set => this._AccessToken = value;
  }

  [DBProp("AccessTokenExpirationDate", AllowNull = true)]
  public DateTime AccessTokenExpirationDate
  {
    get => this._AccessTokenExpirationDate;
    set => this._AccessTokenExpirationDate = value;
  }

  [DBProp("APIRevoked", AllowNull = false, DefaultValue = false)]
  public bool APIRevoked
  {
    get => this._APIRevoked;
    set => this._APIRevoked = value;
  }

  [DBProp("StoreLinkGuid", AllowNull = true)]
  public Guid StoreLinkGuid
  {
    get => this._StoreLinkGuid;
    set => this._StoreLinkGuid = value;
  }

  [DBProp("StoreUserID", AllowNull = true)]
  public long StoreUserID
  {
    get => this._StoreUserID;
    set => this._StoreUserID = value;
  }

  [DBProp("RecoveryEmail", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string RecoveryEmail
  {
    get => this._RecoveryEmail;
    set => this._RecoveryEmail = value;
  }

  [DBProp("VerificationCode", MaxLength = 50, AllowNull = true)]
  public string VerificationCode
  {
    get => this._VerificationCode;
    set => this._VerificationCode = value;
  }

  [DBProp("VerificationDate", AllowNull = true)]
  public DateTime VerificationDate
  {
    get => this._VerificationDate;
    set => this._VerificationDate = value;
  }

  [DBProp("APICallLimit", AllowNull = false, DefaultValue = 2147483647 /*0x7FFFFFFF*/)]
  public int APICallLimit
  {
    get => this._APICallLimit;
    set => this._APICallLimit = value;
  }

  [DBProp("IsHxEnabled", AllowNull = false, DefaultValue = false)]
  public bool IsHxEnabled
  {
    get => this._IsHxEnabled;
    set => this._IsHxEnabled = value;
  }

  [DBProp("SamlEndpointID")]
  [DBForeignKey("SamlEndpoint", "SamlEndpointID")]
  public long SamlEndpointID
  {
    get => this._SamlEndpointID;
    set => this._SamlEndpointID = value;
  }

  [DBProp("AllowCertificateNotifications", AllowNull = false, DefaultValue = true)]
  public bool AllowCertificateNotifications
  {
    get => this._AllowCertificateNotifications;
    set => this._AllowCertificateNotifications = value;
  }

  [DBForeignKey("Account", "AccountID")]
  [DBProp("AutoBill")]
  public long AutoBill
  {
    get => this._AutoBill;
    set => this._AutoBill = value;
  }

  public int CurrentAPICounter
  {
    get => this._CurrentAPICounter;
    set => this._CurrentAPICounter = value;
  }

  public bool APIActive
  {
    get => this._APIActive;
    set => this._APIActive = value;
  }

  public DateTime LastAPICallDate
  {
    get => this._LastAPICallDate;
    set => this._LastAPICallDate = value;
  }

  public Customer PrimaryContact
  {
    get
    {
      if (this._PrimaryContact == null)
        this._PrimaryContact = Customer.Load(this.PrimaryContactID);
      return this._PrimaryContact;
    }
    set
    {
      this.PrimaryContactID = this._PrimaryContact.CustomerID;
      this._PrimaryContact = value;
    }
  }

  public AccountAddress PrimaryAddress
  {
    get
    {
      if (this._PrimaryAddress == null)
        this._PrimaryAddress = AccountAddress.LoadFirstByType(this.AccountID, eAccountAddressType.Primary);
      return this._PrimaryAddress;
    }
  }

  public string TimeZoneDisplayName
  {
    get
    {
      if (this._TimeZoneDisplayName == null)
        this._TimeZoneDisplayName = TimeZone.Load(this.TimeZoneID).DisplayName;
      return this._TimeZoneDisplayName;
    }
  }

  [DBProp("DefaultPaymentID")]
  [DBForeignKey("DefaultPaymentID", "DefaultPaymentID")]
  public long DefaultPaymentID
  {
    get => this._DefaultPaymentID;
    set => this._DefaultPaymentID = value;
  }

  [DBProp("AutoBillStartDate", AllowNull = true)]
  public DateTime AutoBillStartDate
  {
    get => this._AutoBillStartDate;
    set => this._AutoBillStartDate = value;
  }

  [DBProp("AutoBillCancelDate", AllowNull = true)]
  public DateTime AutoBillCancelDate
  {
    get => this._AutoBillCancelDate;
    set => this._AutoBillCancelDate = value;
  }

  public List<AccountPermission> Permissions
  {
    get
    {
      if (this._Permissions == null)
      {
        this._Permissions = AccountPermission.LoadPermissions(this.AccountID);
        this.GetDefaultPermissions();
      }
      return this._Permissions;
    }
    set => this._Permissions = value;
  }

  public AccountPermission Permission(string accountPermissionTypeName)
  {
    foreach (AccountPermission permission in this.Permissions)
    {
      if (permission.Type.Name == accountPermissionTypeName)
        return permission;
    }
    return (AccountPermission) null;
  }

  public AccountPermission Permission(string accountPermissionTypeName, long csNetID)
  {
    foreach (AccountPermission permission in this.Permissions)
    {
      if (permission.Type.Name == accountPermissionTypeName && (!permission.Type.NetworkSpecific || permission.CSNetID == csNetID))
        return permission;
    }
    return (AccountPermission) null;
  }

  [DBProp("LanguageID", AllowNull = false, DefaultValue = 1)]
  [DBForeignKey("Language", "LanguageID")]
  public long LanguageID
  {
    get => this._LanguageID;
    set => this._LanguageID = value;
  }

  public void GetDefaultPermissions()
  {
    if (this.IsPremium)
    {
      AccountPermission accountPermission = this.Permissions.Find((Predicate<AccountPermission>) (p => p.AccountPermissionTypeID == AccountPermissionType.Find("Sensor_Heartbeat_Restriction").AccountPermissionTypeID));
      if (accountPermission == null)
      {
        accountPermission = new AccountPermission();
        accountPermission.AccountID = this.AccountID;
        accountPermission.AccountPermissionTypeID = AccountPermissionType.Find("Sensor_Heartbeat_Restriction").AccountPermissionTypeID;
        accountPermission.Info = "10";
        accountPermission.Can = true;
        accountPermission.OverrideCustomerPermission = true;
        this.Permissions.Add(accountPermission);
      }
      if (accountPermission.Info.ToInt() > 10)
        Account.InsertOrOverride(this.Permissions, this.AccountID, AccountPermissionType.Find("Sensor_Heartbeat_Restriction"), "10", true, true);
      Account.InsertOrOverride(this.Permissions, this.AccountID, AccountPermissionType.Find("Sensor_Advanced_Configuration"), "", true, false);
      Account.InsertOrOverride(this.Permissions, this.AccountID, AccountPermissionType.Find("Customer_Create"), "", true, false);
      Account.InsertOrOverride(this.Permissions, this.AccountID, AccountPermissionType.Find("Navigation_View_Maps"), "", true, false);
      Account.InsertOrOverride(this.Permissions, this.AccountID, AccountPermissionType.Find("Sensor_Configure_Multiple"), "", true, false);
    }
    else
    {
      Account.InsertOrOverride(this.Permissions, this.AccountID, AccountPermissionType.Find("Sensor_Heartbeat_Restriction"), "120", true, true);
      Account.InsertOrOverride(this.Permissions, this.AccountID, AccountPermissionType.Find("Sensor_Advanced_Configuration"), "", false, true);
      Account.InsertOrOverride(this.Permissions, this.AccountID, AccountPermissionType.Find("Customer_Create"), "", false, true);
      Account.InsertOrOverride(this.Permissions, this.AccountID, AccountPermissionType.Find("Navigation_View_Maps"), "", false, true);
      Account.InsertOrOverride(this.Permissions, this.AccountID, AccountPermissionType.Find("Sensor_Configure_Multiple"), "", false, true);
    }
  }

  public static void InsertOrOverride(
    List<AccountPermission> permissions,
    long accountID,
    AccountPermissionType type,
    string info,
    bool can,
    bool force)
  {
    AccountPermission accountPermission = permissions.Find((Predicate<AccountPermission>) (p => p.AccountPermissionTypeID == type.AccountPermissionTypeID));
    if (accountPermission == null)
    {
      accountPermission = new AccountPermission();
      accountPermission.AccountID = accountID;
      accountPermission.AccountPermissionTypeID = type.AccountPermissionTypeID;
      permissions.Add(accountPermission);
    }
    accountPermission.Info = info;
    accountPermission.Can = can;
    accountPermission.OverrideCustomerPermission = force;
  }

  public List<AccountSubscription> Subscriptions
  {
    get
    {
      if (this._Subscriptions == null)
        this._Subscriptions = AccountSubscription.LoadByAccountID(this.AccountID);
      return this._Subscriptions;
    }
  }

  public void ClearSubscritions() => this._Subscriptions = (List<AccountSubscription>) null;

  public void ClearDataPushes()
  {
    this._ExternalDataSubscriptionID = long.MinValue;
    this._ExternalDataSubscription = (ExternalDataSubscription) null;
  }

  public double MinHeartBeat()
  {
    try
    {
      double num = 120.0;
      if (this.CurrentSubscription.Can("sensor_heartbeat_10"))
        num = 10.0;
      if (this.CurrentSubscription.Can("sensor_heartbeat_01"))
        num = 1.0;
      if (this.CurrentSubscription.Can("sensor_heartbeat_00"))
        num = 0.0;
      if (num < 120.0 && num > 1.0 && this.IsHxEnabled)
        num = 1.0;
      return num;
    }
    catch
    {
    }
    return 120.0;
  }

  public AccountSubscription CurrentSubscription
  {
    get
    {
      AccountSubscription currentSubscription = (AccountSubscription) null;
      foreach (AccountSubscription subscription in this.Subscriptions)
      {
        if (subscription.ExpirationDate >= DateTime.Now.Date)
        {
          if (currentSubscription == null)
            currentSubscription = subscription;
          if (AccountSubscriptionType.Load(subscription.AccountSubscriptionTypeID).Rank > currentSubscription.AccountSubscriptionType.Rank)
            currentSubscription = subscription;
        }
      }
      if (currentSubscription == null)
      {
        currentSubscription = new AccountSubscription();
        currentSubscription.AccountID = this.AccountID;
        currentSubscription.AccountSubscriptionTypeID = 1L;
        currentSubscription.CSNetID = long.MinValue;
        currentSubscription.ExpirationDate = DateTime.Today.AddYears(99);
        this.Subscriptions.Add(currentSubscription);
      }
      return currentSubscription;
    }
  }

  public static Account Load(long ID) => BaseDBObject.Load<Account>(ID);

  public static List<Account> LoadAll() => BaseDBObject.LoadAll<Account>();

  public static bool ValidateUser(
    string username,
    string password,
    string application,
    string ipAddress,
    bool useEncryption,
    out Customer user)
  {
    user = Customer.Authenticate(username, password, application, ipAddress, useEncryption);
    return user != null;
  }

  public static List<Account> LoadByRetailAccountID(long accountID)
  {
    return BaseDBObject.LoadByForeignKey<Account>("RetailAccountID", (object) accountID);
  }

  public static List<Account> Search(long resellerID, string query, int limit)
  {
    return new Monnit.Data.Account.Search(resellerID, query, limit).Result;
  }

  public static List<Account> LoadByAccountIDTree(
    long customerID,
    long currentAccountID,
    long targetAccountID)
  {
    return new Monnit.Data.Account.LoadByAccountIDTree(customerID, currentAccountID, targetAccountID).Result;
  }

  public static List<Account> LoadResellers(long customerID, int limit)
  {
    return new Monnit.Data.Account.loadResellers(customerID, limit).Result;
  }

  public static DataTable ModelSearch(long customerID, string query, int limit)
  {
    return new Monnit.Data.Account.ModelSearch(customerID, query, limit).Result;
  }

  public static DataTable UpdateAccountTree(long accountID)
  {
    return new Monnit.Data.Account.UpdateAccountTree(accountID).Result;
  }

  public static List<Account> Ancestors(long customerID, long accountID)
  {
    return BaseDBObject.Load<Account>(new Monnit.Data.Account.Ancestors(customerID, accountID).Result);
  }

  public static bool CheckAccountNumberIsUnique(string accountnumber)
  {
    return !string.IsNullOrEmpty(accountnumber) && new Monnit.Data.Account.CheckAccountNumberIsUnique(accountnumber).Result;
  }

  public static List<Account> NewAccounts(DateTime utcStart, DateTime utcEnd)
  {
    return new Monnit.Data.Account.NewAccounts(utcStart, utcEnd).Result;
  }

  public static bool IsSubAccount(long ParentAccID, long SubAccountID)
  {
    return new Monnit.Data.Account.IsSubAccount(ParentAccID, SubAccountID).Result;
  }

  public static List<Account> ResellerNewAccounts(
    DateTime utcStart,
    DateTime utcEnd,
    long resellerID)
  {
    return Account.NewAccounts(utcStart, utcEnd).Where<Account>((System.Func<Account, bool>) (a => a.RetailAccountID == resellerID)).ToList<Account>();
  }

  public static bool Remove(long AccountId) => new Monnit.Data.Account.Remove(AccountId).Result;

  public AccountTheme getTheme() => AccountTheme.Find(this);

  public static List<AccountIDTreeModel> LoadByUserAccountIDandAccountID(
    long userAccountID,
    long accountID)
  {
    return new Monnit.Data.AccountIDTreeModel.LoadByUserAccountIDandAccountID(userAccountID, accountID).Result;
  }

  public long GetThemeID() => new Monnit.Data.Account.GetThemeID(this.AccountID).Result;

  public ExternalDataSubscription ExternalDataSubscription
  {
    get
    {
      if (this._ExternalDataSubscription == null)
        this._ExternalDataSubscription = ExternalDataSubscription.Load(this.ExternalDataSubscriptionID);
      return this._ExternalDataSubscription;
    }
    set
    {
      this._ExternalDataSubscription = value;
      if (this._ExternalDataSubscription != null)
        this.ExternalDataSubscriptionID = this._ExternalDataSubscription.ExternalDataSubscriptionID;
      else
        this.ExternalDataSubscriptionID = long.MinValue;
    }
  }

  public static Account LoadByExternalDataSubscriptionID(long externalDataSubscriptionID)
  {
    return BaseDBObject.LoadByForeignKey<Account>("ExternalDataSubscriptionID", (object) externalDataSubscriptionID).FirstOrDefault<Account>();
  }

  public static Account LoadByVerificationCode(long code)
  {
    return BaseDBObject.LoadByForeignKey<Account>("VerificationCode", (object) code).FirstOrDefault<Account>();
  }

  public static void RemoveSamlEndPoint(long accountID, long samlEndpointID)
  {
    Monnit.Data.Account.RemoveSamlEndPoint removeSamlEndPoint = new Monnit.Data.Account.RemoveSamlEndPoint(accountID, samlEndpointID);
  }
}
