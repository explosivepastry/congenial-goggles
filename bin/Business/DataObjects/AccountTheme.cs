// Decompiled with JetBrains decompiler
// Type: Monnit.AccountTheme
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("AccountTheme")]
public class AccountTheme : BaseDBObject
{
  private long _AccountThemeID = long.MinValue;
  private long _AccountID = long.MinValue;
  private string _Domain = string.Empty;
  private string _Theme = string.Empty;
  private string _SMTP = string.Empty;
  private int _SMTPPort = 25;
  private string _SMTPUser = string.Empty;
  private string _SMTPPassword = string.Empty;
  private string _SMTPDefaultFrom = string.Empty;
  private string _SMTPFriendlyName = string.Empty;
  private string _oldSMTPFriendlyName = string.Empty;
  private bool _SMTPUseSSL = false;
  private string _SMTPReturnPath = string.Empty;
  private string _CurrentEULA = string.Empty;
  private string _FromPhone = string.Empty;
  private int _PasswordLifetime = -1;
  private int _MaxFailedLogins = -1;
  private bool _SendSubscriptionNotification = true;
  private bool _SendMaintenanceNotification = true;
  private bool _SendWebhookNotification = true;
  private int _MinPasswordLength = int.MinValue;
  private bool _PasswordRequiresMixed = false;
  private bool _IsTFAEnabled = false;
  private bool _PasswordRequiresSpecial = false;
  private bool _PasswordRequiresNumber = false;
  private int _DefaultPremiumDays = -15;
  private bool _EnableDashboard = false;
  private bool _AllowPushNotification = false;
  private bool _IsActive = true;
  private long _DefaultAccountSubscriptionTypeID = 5;
  private bool _EnableClassic = false;
  private bool _AllowAccountCreation = false;
  private bool _SupportsSaml = false;
  private bool _AllowPWA = false;
  private string _AlphanumericSenderID = string.Empty;
  private List<AccountThemeProperty> _Properties = (List<AccountThemeProperty>) null;
  private List<MonnitApplication> _Applications = (List<MonnitApplication>) null;
  private List<AccountThemeStyleGroup> _StyleGroups = (List<AccountThemeStyleGroup>) null;

  [DBProp("AccountThemeID", IsPrimaryKey = true)]
  public long AccountThemeID
  {
    get => this._AccountThemeID;
    set => this._AccountThemeID = value;
  }

  public Account Account => Account.Load(this.AccountID);

  [DBForeignKey("Account", "AccountID")]
  [DBProp("AccountID", AllowNull = false)]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("Domain", AllowNull = false, MaxLength = 255 /*0xFF*/)]
  public string Domain
  {
    get => this._Domain;
    set
    {
      if (value == null)
        this._Domain = string.Empty;
      else
        this._Domain = value;
    }
  }

  [DBProp("Theme", AllowNull = false, MaxLength = 255 /*0xFF*/)]
  public string Theme
  {
    get => this._Theme;
    set
    {
      if (value == null)
        this._Theme = string.Empty;
      else
        this._Theme = value;
    }
  }

  [DBProp("SMTP", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string SMTP
  {
    get => this._SMTP;
    set
    {
      if (value == null)
        this._SMTP = string.Empty;
      else
        this._SMTP = value;
    }
  }

  [DBProp("SMTPPort", AllowNull = false)]
  public int SMTPPort
  {
    get => this._SMTPPort;
    set => this._SMTPPort = value;
  }

  [DBProp("SMTPUser", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string SMTPUser
  {
    get => this._SMTPUser;
    set
    {
      if (value == null)
        this._SMTPUser = string.Empty;
      else
        this._SMTPUser = value;
    }
  }

  [DBProp("SMTPPassword", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string SMTPPassword
  {
    get => this._SMTPPassword;
    set
    {
      if (value == null)
        this._SMTPPassword = string.Empty;
      else
        this._SMTPPassword = value;
    }
  }

  public string SMTPPasswordPlainText
  {
    get
    {
      if (string.IsNullOrEmpty(this.SMTPPassword))
        return "";
      return MonnitUtil.UseEncryption() ? this.SMTPPassword.Decrypt() : this.SMTPPassword;
    }
    set
    {
      if (string.IsNullOrEmpty(value))
        this.SMTPPassword = "";
      else if (MonnitUtil.UseEncryption())
        this.SMTPPassword = value.Encrypt();
      else
        this.SMTPPassword = value;
    }
  }

  [DBProp("SMTPDefaultFrom", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string SMTPDefaultFrom
  {
    get => this._SMTPDefaultFrom;
    set => this._SMTPDefaultFrom = value;
  }

  [DBProp("SMTPFriendlyName", AllowNull = false, MaxLength = 255 /*0xFF*/)]
  public string SMTPFriendlyName
  {
    get => this._SMTPFriendlyName != null ? this._SMTPFriendlyName : this._oldSMTPFriendlyName;
    set
    {
      if (value == null)
        this._SMTPFriendlyName = string.Empty;
      else
        this._SMTPFriendlyName = value;
    }
  }

  [DBProp("SMTPFreindlyName", AllowNull = false, MaxLength = 255 /*0xFF*/)]
  public string oldSMTPFriendlyName
  {
    get => this._SMTPFriendlyName != null ? this._SMTPFriendlyName : this._oldSMTPFriendlyName;
    set
    {
      if (value == null)
        this._oldSMTPFriendlyName = string.Empty;
      else
        this._oldSMTPFriendlyName = value;
    }
  }

  [DBProp("SMTPUseSSL")]
  public bool SMTPUseSSL
  {
    get => this._SMTPUseSSL;
    set => this._SMTPUseSSL = value;
  }

  [DBProp("SMTPReturnPath", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string SMTPReturnPath
  {
    get => this._SMTPReturnPath;
    set => this._SMTPReturnPath = value;
  }

  [DBProp("CurrentEULA", MaxLength = 255 /*0xFF*/)]
  public string CurrentEULA
  {
    get
    {
      if (string.IsNullOrEmpty(this._CurrentEULA))
        this._CurrentEULA = ConfigData.FindValue("EULA");
      return this._CurrentEULA;
    }
    set
    {
      if (value == null)
        this._CurrentEULA = string.Empty;
      else
        this._CurrentEULA = value;
    }
  }

  [DBProp("FromPhone", MaxLength = 20)]
  public string FromPhone
  {
    get => this._FromPhone;
    set => this._FromPhone = value;
  }

  [DBProp("PasswordLifetime")]
  public int PasswordLifetime
  {
    get => this._PasswordLifetime;
    set => this._PasswordLifetime = value;
  }

  public bool PasswordExpired(DateTime LastUpdated)
  {
    return this.PasswordLifetime > 0 && LastUpdated.AddDays((double) this.PasswordLifetime) < DateTime.UtcNow;
  }

  [DBProp("MaxFailedLogins", AllowNull = false, DefaultValue = -1)]
  public int MaxFailedLogins
  {
    get => this._MaxFailedLogins;
    set => this._MaxFailedLogins = value;
  }

  [DBProp("SendSubscriptionNotification", AllowNull = false, DefaultValue = true)]
  public bool SendSubscriptionNotification
  {
    get => this._SendSubscriptionNotification;
    set => this._SendSubscriptionNotification = value;
  }

  [DBProp("SendMaintenanceNotification", AllowNull = false, DefaultValue = true)]
  public bool SendMaintenanceNotification
  {
    get => this._SendMaintenanceNotification;
    set => this._SendMaintenanceNotification = value;
  }

  [DBProp("SendWebhookNotification", AllowNull = false, DefaultValue = true)]
  public bool SendWebhookNotification
  {
    get => this._SendWebhookNotification;
    set => this._SendWebhookNotification = value;
  }

  [DBProp("MinPasswordLength", AllowNull = false, DefaultValue = 8)]
  public int MinPasswordLength
  {
    get => this._MinPasswordLength;
    set => this._MinPasswordLength = value;
  }

  [DBProp("PasswordRequiresMixed", AllowNull = false, DefaultValue = false)]
  public bool PasswordRequiresMixed
  {
    get => this._PasswordRequiresMixed;
    set => this._PasswordRequiresMixed = value;
  }

  [DBProp("IsTFAEnabled", AllowNull = false, DefaultValue = false)]
  public bool IsTFAEnabled
  {
    get => this._IsTFAEnabled;
    set => this._IsTFAEnabled = value;
  }

  [DBProp("PasswordRequiresSpecial", AllowNull = false, DefaultValue = false)]
  public bool PasswordRequiresSpecial
  {
    get => this._PasswordRequiresSpecial;
    set => this._PasswordRequiresSpecial = value;
  }

  [DBProp("PasswordRequiresNumber", AllowNull = false, DefaultValue = false)]
  public bool PasswordRequiresNumber
  {
    get => this._PasswordRequiresNumber;
    set => this._PasswordRequiresNumber = value;
  }

  [DBProp("DefaultPremiumDays", AllowNull = false, DefaultValue = -15)]
  public int DefaultPremiumDays
  {
    get => this._DefaultPremiumDays;
    set => this._DefaultPremiumDays = value;
  }

  [DBProp("EnableDashboard", AllowNull = true, DefaultValue = false)]
  public bool EnableDashboard
  {
    get => this._EnableDashboard;
    set => this._EnableDashboard = value;
  }

  [DBProp("AllowPushNotification", AllowNull = true, DefaultValue = false)]
  public bool AllowPushNotification
  {
    get => this._AllowPushNotification;
    set => this._AllowPushNotification = value;
  }

  [DBProp("IsActive")]
  public bool IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [DBProp("DefaultAccountSubscriptionTypeID", AllowNull = false, DefaultValue = 5)]
  [DBForeignKey("AccountSubscriptionType", "AccountSubscriptionTypeID")]
  public long DefaultAccountSubscriptionTypeID
  {
    get => this._DefaultAccountSubscriptionTypeID;
    set => this._DefaultAccountSubscriptionTypeID = value;
  }

  [DBProp("EnableClassic", AllowNull = false, DefaultValue = true)]
  public bool EnableClassic
  {
    get => this._EnableClassic;
    set => this._EnableClassic = value;
  }

  [DBProp("AllowAccountCreation", AllowNull = true, DefaultValue = true)]
  public bool AllowAccountCreation
  {
    get => this._AllowAccountCreation;
    set => this._AllowAccountCreation = value;
  }

  [DBProp("SupportsSaml", AllowNull = false, DefaultValue = false)]
  public bool SupportsSaml
  {
    get => this._SupportsSaml;
    set => this._SupportsSaml = value;
  }

  [DBProp("AllowPWA", AllowNull = false, DefaultValue = false)]
  public bool AllowPWA
  {
    get => this._AllowPWA;
    set => this._AllowPWA = value;
  }

  [DBProp("AlphanumericSenderID", MaxLength = 11, AllowNull = true)]
  public string AlphanumericSenderID
  {
    get => this._AlphanumericSenderID;
    set => this._AlphanumericSenderID = value;
  }

  public List<AccountThemeProperty> Properties
  {
    get
    {
      if (this._Properties == null)
        this._Properties = AccountThemeProperty.LoadByAccountThemeID(this.AccountThemeID);
      return this._Properties;
    }
  }

  public string PropertyValue(string typeName)
  {
    AccountThemePropertyType themePropertyType = AccountThemePropertyType.Find(typeName);
    return themePropertyType != null ? this.PropertyValue(themePropertyType.AccountThemePropertyTypeID).ToStringSafe() : "";
  }

  public string PropertyValue(long accountThemePropertyTypeID)
  {
    AccountThemeProperty accountThemeProperty = this.Properties.Where<AccountThemeProperty>((Func<AccountThemeProperty, bool>) (p => p.AccountThemePropertyTypeID == accountThemePropertyTypeID)).FirstOrDefault<AccountThemeProperty>();
    return accountThemeProperty != null ? accountThemeProperty.Value.ToStringSafe() : "";
  }

  public bool CustomerLockEnforced() => this.MaxFailedLogins > 0;

  public bool CustomerLocked(int failCount)
  {
    return this.CustomerLockEnforced() && failCount > this.MaxFailedLogins;
  }

  public Version CurrentEULAVersion => Version.Parse(this.CurrentEULA);

  public static AccountTheme Load(long ID) => BaseDBObject.Load<AccountTheme>(ID);

  public static List<AccountTheme> LoadAll() => BaseDBObject.LoadAll<AccountTheme>();

  public static AccountTheme Find(string domain)
  {
    string key = $"AccountTheme_{domain}";
    AccountTheme accountTheme = TimedCache.RetrieveObject<AccountTheme>(key);
    if (accountTheme == null)
    {
      accountTheme = AccountTheme.ThemesCache().Where<AccountTheme>((Func<AccountTheme, bool>) (t => t.Domain.ToLower() == domain.ToLower())).FirstOrDefault<AccountTheme>();
      if (accountTheme == null)
      {
        accountTheme = new Monnit.Data.AccountTheme.Find(domain).Result;
        if (accountTheme == null)
          accountTheme = new AccountTheme()
          {
            Theme = !string.IsNullOrWhiteSpace(ConfigData.AppSettings("HardTheme")) ? ConfigData.AppSettings("HardTheme") : "Default",
            Domain = domain,
            SMTP = ConfigData.AppSettings("SMTP"),
            SMTPPort = ConfigData.AppSettings("SMPTPort").ToInt(),
            SMTPUser = ConfigData.AppSettings("SMTPUser"),
            SMTPPassword = ConfigData.AppSettings("SMTPPassword"),
            SMTPDefaultFrom = ConfigData.AppSettings("SMTPDefaultFrom"),
            SMTPFriendlyName = !string.IsNullOrWhiteSpace(ConfigData.AppSettings("SMTPFriendlyName")) ? ConfigData.AppSettings("SMTPFriendlyName") : ConfigData.AppSettings("SMTPFreindlyName"),
            EnableDashboard = string.IsNullOrWhiteSpace(ConfigData.AppSettings("EnableDashboard")) || ConfigData.AppSettings("EnableDashboard").ToBool()
          };
      }
      TimedCache.AddObjectToCach(key, (object) accountTheme);
    }
    return accountTheme;
  }

  public static AccountTheme Find(Account account)
  {
    List<AccountTheme> source = AccountTheme.ThemesCache();
    AccountTheme accountTheme = source.Where<AccountTheme>((Func<AccountTheme, bool>) (t => t.AccountID == account.AccountID)).FirstOrDefault<AccountTheme>();
    if (accountTheme == null)
    {
      do
      {
        accountTheme = source.Where<AccountTheme>((Func<AccountTheme, bool>) (t => t.AccountID == account.RetailAccountID)).FirstOrDefault<AccountTheme>();
        account = Account.Load(account.RetailAccountID);
      }
      while (accountTheme == null && account != null && account.RetailAccountID != account.AccountID);
    }
    return accountTheme;
  }

  public static AccountTheme Find(long accountID)
  {
    Account account = Account.Load(accountID);
    if (account == null)
      accountID = ConfigData.AppSettings("AdminAccountID").ToLong();
    return AccountTheme.ThemesCache().Where<AccountTheme>((Func<AccountTheme, bool>) (t => t.AccountID == accountID)).FirstOrDefault<AccountTheme>() ?? AccountTheme.Find(account);
  }

  public static string BaseURL(Account account)
  {
    AccountTheme accountTheme = AccountTheme.Find(account);
    return accountTheme != null ? (ConfigData.AppSettings(nameof (BaseURL)) == null || !ConfigData.AppSettings(nameof (BaseURL)).ToLower().StartsWith("https") ? "http://" : "https://") + accountTheme.Domain : (ConfigData.AppSettings(nameof (BaseURL)) != null ? ConfigData.AppSettings(nameof (BaseURL)) : "https://www.imonnit.com");
  }

  public static List<AccountTheme> ThemesCache()
  {
    List<AccountTheme> accountThemeList = TimedCache.RetrieveObject<List<AccountTheme>>("AccountThemes");
    if (accountThemeList == null || accountThemeList.Count == 0)
    {
      accountThemeList = BaseDBObject.LoadAll<AccountTheme>();
      TimedCache.AddObjectToCach("AccountThemes", (object) accountThemeList);
    }
    return accountThemeList;
  }

  public List<MonnitApplication> Applications
  {
    get
    {
      if (this._Applications == null)
        this._Applications = MonnitApplication.LoadByAccountThemeID(this.AccountThemeID);
      return this._Applications;
    }
  }

  public List<AccountThemeStyleGroup> StyleGroups
  {
    get
    {
      if (this._StyleGroups == null)
        this._StyleGroups = AccountThemeStyleGroup.LoadByAccountThemeID(this.AccountThemeID, false);
      return this._StyleGroups;
    }
  }
}
