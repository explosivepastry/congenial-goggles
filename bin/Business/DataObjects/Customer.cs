// Decompiled with JetBrains decompiler
// Type: Monnit.Customer
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

#nullable disable
namespace Monnit;

[MetadataType(typeof (CustomerMetadata))]
[DBClass("Customer")]
public class Customer : BaseDBObject
{
  private long _CustomerID = long.MinValue;
  private long _AccountID = long.MinValue;
  private string _UserName = string.Empty;
  private string _Password = string.Empty;
  private byte[] _Password2 = (byte[]) null;
  private byte[] _Salt = (byte[]) null;
  private int _WorkFactor = 0;
  private string _TOTPSecret = string.Empty;
  private int _TFAMethod = 0;
  private bool _ByPass2FA = true;
  private bool _ByPass2FAPermanent = true;
  private string _FirstName = string.Empty;
  private string _LastName = string.Empty;
  private string _NotificationEmail = string.Empty;
  private string _NotificationPhone = string.Empty;
  private string _NotificationPhoneISOCode = string.Empty;
  private string _NotificationPhone2 = string.Empty;
  private string _NotificationPhone2ISOCode = string.Empty;
  private long _SMSCarrierID = long.MinValue;
  private bool _PasswordExpired = false;
  private bool _IsAdmin = false;
  private bool _IsDeleted = false;
  private DateTime _CreateDate = DateTime.UtcNow;
  private DateTime _LastLoginDate = DateTime.MinValue;
  private bool _IsActive = true;
  private bool _DirectSMS = false;
  private bool _SendSensorNotificationToText = false;
  private bool _SendSensorNotificationToVoice = false;
  private DateTime _PasswordChangeDate = DateTime.UtcNow;
  private int _FailedLoginCount = 0;
  private string _GUID = string.Empty;
  private long _MondayScheduleID = long.MinValue;
  private CustomerSchedule _MondaySchedule = (CustomerSchedule) null;
  private long _TuesdayScheduleID = long.MinValue;
  private CustomerSchedule _TuesdaySchedule = (CustomerSchedule) null;
  private long _WednesdayScheduleID = long.MinValue;
  private CustomerSchedule _WednesdaySchedule = (CustomerSchedule) null;
  private long _ThursdayScheduleID = long.MinValue;
  private CustomerSchedule _ThursdaySchedule = (CustomerSchedule) null;
  private long _FridayScheduleID = long.MinValue;
  private CustomerSchedule _FridaySchedule = (CustomerSchedule) null;
  private long _SaturdayScheduleID = long.MinValue;
  private CustomerSchedule _SaturdaySchedule = (CustomerSchedule) null;
  private long _SundayScheduleID = long.MinValue;
  private CustomerSchedule _SundaySchedule = (CustomerSchedule) null;
  private bool _AlwaysSend = true;
  private bool _AllowPushNotification = false;
  private DateTime _NotificationOptIn = DateTime.MinValue;
  private DateTime _NotificationOptOut = DateTime.MinValue;
  private DateTime _CookieAcceptanceDate = DateTime.MinValue;
  private DateTime _DeleteDate = DateTime.MinValue;
  private string _HomepageLink = string.Empty;
  private string _SamlNameID = string.Empty;
  private bool _ShowPopupNotice = true;
  private byte[] _Image = (byte[]) null;
  private string _ImageName = string.Empty;
  private int _ImageWidth = int.MinValue;
  private int _ImageHeight = int.MinValue;
  private string _Title = string.Empty;
  private DateTime _ForceLogoutDate = DateTime.MinValue;
  private DateTime _PWACreateDate = DateTime.MinValue;
  private Account _Account;
  private Account _DefaultAccount;
  private Dictionary<string, string> _Preferences;
  private SMSCarrier _SMSCarrier;
  private List<CustomerPermission> _Permissions;
  private Dictionary<long, bool> CanSeeNetworkDict;
  private List<CustomerAccountLink> _CustomerAccountLinkList = (List<CustomerAccountLink>) null;
  private List<ReleaseNoteViewed> _Acknowledged = (List<ReleaseNoteViewed>) null;
  private MaintenanceWindowCustomer _MaintWindowAcked;

  [DBProp("CustomerID", IsPrimaryKey = true)]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBForeignKey("Account", "AccountID")]
  [DBProp("AccountID", AllowNull = true)]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("UserName", AllowNull = true, MaxLength = 255 /*0xFF*/, International = true)]
  public string UserName
  {
    get => this._UserName;
    set
    {
      if (value == null)
        this._UserName = string.Empty;
      else
        this._UserName = value;
    }
  }

  [DBProp("Password", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string Password
  {
    get => this._Password;
    set
    {
      if (value == null)
        this._Password = string.Empty;
      else
        this._Password = value;
    }
  }

  [DBProp("Password2", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public byte[] Password2
  {
    get => this._Password2;
    set => this._Password2 = value;
  }

  [DBProp("Salt", AllowNull = true, MaxLength = 25)]
  public byte[] Salt
  {
    get => this._Salt;
    set => this._Salt = value;
  }

  [DBProp("WorkFactor", AllowNull = false, DefaultValue = 0)]
  public int WorkFactor
  {
    get => this._WorkFactor;
    set => this._WorkFactor = value;
  }

  [DBProp("TOTPSecret", AllowNull = true, MaxLength = 24)]
  public string TOTPSecret
  {
    get => this._TOTPSecret;
    set => this._TOTPSecret = value;
  }

  [DBProp("TFAMethod", AllowNull = false, DefaultValue = 0)]
  public int TFAMethod
  {
    get => this._TFAMethod;
    set => this._TFAMethod = value;
  }

  [DBProp("ByPass2FA", AllowNull = false, DefaultValue = true)]
  public bool ByPass2FA
  {
    get => this._ByPass2FA;
    set => this._ByPass2FA = value;
  }

  [DBProp("ByPass2FAPermanent", AllowNull = false, DefaultValue = false)]
  public bool ByPass2FAPermanent
  {
    get => this._ByPass2FAPermanent;
    set => this._ByPass2FAPermanent = value;
  }

  [DBProp("FirstName", AllowNull = true, MaxLength = 255 /*0xFF*/, International = true)]
  public string FirstName
  {
    get => this._FirstName;
    set
    {
      if (value == null)
        this._FirstName = string.Empty;
      else
        this._FirstName = value;
    }
  }

  [DBProp("LastName", AllowNull = true, MaxLength = 255 /*0xFF*/, International = true)]
  public string LastName
  {
    get => this._LastName;
    set
    {
      if (value == null)
        this._LastName = string.Empty;
      else
        this._LastName = value;
    }
  }

  [DBProp("NotificationEmail", MaxLength = 255 /*0xFF*/)]
  public string NotificationEmail
  {
    get => this._NotificationEmail;
    set
    {
      if (value == null)
        this._NotificationEmail = string.Empty;
      else
        this._NotificationEmail = value;
    }
  }

  [DBProp("NotificationPhone", MaxLength = 255 /*0xFF*/)]
  public string NotificationPhone
  {
    get => this._NotificationPhone;
    set
    {
      if (value == null)
        this._NotificationPhone = string.Empty;
      else
        this._NotificationPhone = value;
    }
  }

  [DBProp("NotificationPhoneISOCode", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string NotificationPhoneISOCode
  {
    get => this._NotificationPhoneISOCode;
    set => this._NotificationPhoneISOCode = value;
  }

  [DBProp("NotificationPhone2", MaxLength = 255 /*0xFF*/)]
  public string NotificationPhone2
  {
    get => this._NotificationPhone2;
    set
    {
      if (value == null)
        this._NotificationPhone2 = string.Empty;
      else
        this._NotificationPhone2 = value;
    }
  }

  [DBProp("NotificationPhone2ISOCode", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string NotificationPhone2ISOCode
  {
    get => this._NotificationPhone2ISOCode;
    set => this._NotificationPhone2ISOCode = value;
  }

  [DBForeignKey("SMSCarrier", "SMSCarrierID")]
  [DBProp("SMSCarrierID")]
  public long SMSCarrierID
  {
    get => this._SMSCarrierID;
    set => this._SMSCarrierID = value;
  }

  [DBProp("PasswordExpired")]
  public bool PasswordExpired
  {
    get => this._PasswordExpired;
    set => this._PasswordExpired = value;
  }

  [DBProp("IsAdmin", AllowNull = true)]
  public bool IsAdmin
  {
    get => this._IsAdmin;
    set => this._IsAdmin = value;
  }

  [DBProp("IsDeleted", AllowNull = true)]
  public bool IsDeleted
  {
    get => this._IsDeleted;
    set => this._IsDeleted = value;
  }

  [DBProp("CreateDate", AllowNull = true)]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("LastLoginDate", AllowNull = true)]
  public DateTime LastLoginDate
  {
    get => this._LastLoginDate;
    set => this._LastLoginDate = value;
  }

  [DBProp("IsActive", AllowNull = false, DefaultValue = true)]
  public bool IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [DBProp("DirectSMS", AllowNull = false, DefaultValue = false)]
  public bool DirectSMS
  {
    get => this._DirectSMS;
    set => this._DirectSMS = value;
  }

  [DBProp("SendSensorNotificationToText", DefaultValue = true)]
  public bool SendSensorNotificationToText
  {
    get => this._SendSensorNotificationToText;
    set => this._SendSensorNotificationToText = value;
  }

  [DBProp("SendSensorNotificationToVoice")]
  public bool SendSensorNotificationToVoice
  {
    get => this._SendSensorNotificationToVoice;
    set => this._SendSensorNotificationToVoice = value;
  }

  [DBProp("PasswordChangeDate")]
  public DateTime PasswordChangeDate
  {
    get
    {
      DateTime passwordChangeDate = this._PasswordChangeDate;
      if (false)
        this._PasswordChangeDate = DateTime.UtcNow;
      return this._PasswordChangeDate;
    }
    set => this._PasswordChangeDate = value;
  }

  [DBProp("FailedLoginCount", AllowNull = false, DefaultValue = 0)]
  public int FailedLoginCount
  {
    get => this._FailedLoginCount;
    set => this._FailedLoginCount = value;
  }

  [DBProp("GUID", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string GUID
  {
    get => this._GUID;
    set => this._GUID = value;
  }

  public void GenerateGUID() => this.GUID = Guid.NewGuid().ToString();

  [DBForeignKey("CustomerSchedule", "CustomerScheduleID")]
  [DBProp("MondayScheduleID", AllowNull = true)]
  public long MondayScheduleID
  {
    get => this._MondayScheduleID;
    set => this._MondayScheduleID = value;
  }

  public CustomerSchedule MondaySchedule
  {
    get
    {
      if (this._MondaySchedule == null)
      {
        this._MondaySchedule = CustomerSchedule.Load(this._MondayScheduleID);
        if (this._MondaySchedule == null)
        {
          this._MondaySchedule = new CustomerSchedule();
          this._MondaySchedule.DayofWeek = DayOfWeek.Monday;
          this._MondaySchedule.CustomerDaySchedule = eNotificationDaySchedule.All_Day;
        }
      }
      return this._MondaySchedule;
    }
    set => this._MondaySchedule = value;
  }

  [DBForeignKey("CustomerSchedule", "CustomerScheduleID")]
  [DBProp("TuesdayScheduleID", AllowNull = true)]
  public long TuesdayScheduleID
  {
    get => this._TuesdayScheduleID;
    set => this._TuesdayScheduleID = value;
  }

  public CustomerSchedule TuesdaySchedule
  {
    get
    {
      if (this._TuesdaySchedule == null)
      {
        this._TuesdaySchedule = CustomerSchedule.Load(this._TuesdayScheduleID);
        if (this._TuesdaySchedule == null)
        {
          this._TuesdaySchedule = new CustomerSchedule();
          this._TuesdaySchedule.DayofWeek = DayOfWeek.Tuesday;
          this._TuesdaySchedule.CustomerDaySchedule = eNotificationDaySchedule.All_Day;
        }
      }
      return this._TuesdaySchedule;
    }
    set => this._TuesdaySchedule = value;
  }

  [DBForeignKey("CustomerSchedule", "CustomerScheduleID")]
  [DBProp("WednesdayScheduleID", AllowNull = true)]
  public long WednesdayScheduleID
  {
    get => this._WednesdayScheduleID;
    set => this._WednesdayScheduleID = value;
  }

  public CustomerSchedule WednesdaySchedule
  {
    get
    {
      if (this._WednesdaySchedule == null)
      {
        this._WednesdaySchedule = CustomerSchedule.Load(this._WednesdayScheduleID);
        if (this._WednesdaySchedule == null)
        {
          this._WednesdaySchedule = new CustomerSchedule();
          this._WednesdaySchedule.DayofWeek = DayOfWeek.Wednesday;
          this._WednesdaySchedule.CustomerDaySchedule = eNotificationDaySchedule.All_Day;
        }
      }
      return this._WednesdaySchedule;
    }
    set => this._WednesdaySchedule = value;
  }

  [DBForeignKey("CustomerSchedule", "CustomerScheduleID")]
  [DBProp("ThursdayScheduleID", AllowNull = true)]
  public long ThursdayScheduleID
  {
    get => this._ThursdayScheduleID;
    set => this._ThursdayScheduleID = value;
  }

  public CustomerSchedule ThursdaySchedule
  {
    get
    {
      if (this._ThursdaySchedule == null)
      {
        this._ThursdaySchedule = CustomerSchedule.Load(this._ThursdayScheduleID);
        if (this._ThursdaySchedule == null)
        {
          this._ThursdaySchedule = new CustomerSchedule();
          this._ThursdaySchedule.DayofWeek = DayOfWeek.Thursday;
          this._ThursdaySchedule.CustomerDaySchedule = eNotificationDaySchedule.All_Day;
        }
      }
      return this._ThursdaySchedule;
    }
    set => this._ThursdaySchedule = value;
  }

  [DBForeignKey("CustomerSchedule", "CustomerScheduleID")]
  [DBProp("FridayScheduleID", AllowNull = true)]
  public long FridayScheduleID
  {
    get => this._FridayScheduleID;
    set => this._FridayScheduleID = value;
  }

  public CustomerSchedule FridaySchedule
  {
    get
    {
      if (this._FridaySchedule == null)
      {
        this._FridaySchedule = CustomerSchedule.Load(this._FridayScheduleID);
        if (this._FridaySchedule == null)
        {
          this._FridaySchedule = new CustomerSchedule();
          this._FridaySchedule.DayofWeek = DayOfWeek.Friday;
          this._FridaySchedule.CustomerDaySchedule = eNotificationDaySchedule.All_Day;
        }
      }
      return this._FridaySchedule;
    }
    set => this._FridaySchedule = value;
  }

  [DBForeignKey("CustomerSchedule", "CustomerScheduleID")]
  [DBProp("SaturdayScheduleID", AllowNull = true)]
  public long SaturdayScheduleID
  {
    get => this._SaturdayScheduleID;
    set => this._SaturdayScheduleID = value;
  }

  public CustomerSchedule SaturdaySchedule
  {
    get
    {
      if (this._SaturdaySchedule == null)
      {
        this._SaturdaySchedule = CustomerSchedule.Load(this._SaturdayScheduleID);
        if (this._SaturdaySchedule == null)
        {
          this._SaturdaySchedule = new CustomerSchedule();
          this._SaturdaySchedule.DayofWeek = DayOfWeek.Saturday;
          this._SaturdaySchedule.CustomerDaySchedule = eNotificationDaySchedule.All_Day;
        }
      }
      return this._SaturdaySchedule;
    }
    set => this._SaturdaySchedule = value;
  }

  [DBForeignKey("CustomerSchedule", "CustomerScheduleID")]
  [DBProp("SundayScheduleID", AllowNull = true)]
  public long SundayScheduleID
  {
    get => this._SundayScheduleID;
    set => this._SundayScheduleID = value;
  }

  public CustomerSchedule SundaySchedule
  {
    get
    {
      if (this._SundaySchedule == null)
      {
        this._SundaySchedule = CustomerSchedule.Load(this._SundayScheduleID);
        if (this._SundaySchedule == null)
        {
          this._SundaySchedule = new CustomerSchedule();
          this._SundaySchedule.DayofWeek = DayOfWeek.Sunday;
          this._SundaySchedule.CustomerDaySchedule = eNotificationDaySchedule.All_Day;
        }
      }
      return this._SundaySchedule;
    }
    set => this._SundaySchedule = value;
  }

  public CustomerSchedule GetCustomerSchedule(DayOfWeek day)
  {
    switch (day)
    {
      case DayOfWeek.Sunday:
        return this.SundaySchedule;
      case DayOfWeek.Monday:
        return this.MondaySchedule;
      case DayOfWeek.Tuesday:
        return this.TuesdaySchedule;
      case DayOfWeek.Wednesday:
        return this.WednesdaySchedule;
      case DayOfWeek.Thursday:
        return this.ThursdaySchedule;
      case DayOfWeek.Friday:
        return this.FridaySchedule;
      case DayOfWeek.Saturday:
        return this.SaturdaySchedule;
      default:
        return (CustomerSchedule) null;
    }
  }

  [DBProp("AlwaysSend", DefaultValue = true)]
  public bool AlwaysSend
  {
    get => this._AlwaysSend;
    set => this._AlwaysSend = value;
  }

  [DBProp("AllowPushNotification", DefaultValue = false)]
  public bool AllowPushNotification
  {
    get => this._AllowPushNotification;
    set => this._AllowPushNotification = value;
  }

  [DBProp("NotificationOptIn", AllowNull = true)]
  public DateTime NotificationOptIn
  {
    get => this._NotificationOptIn;
    set => this._NotificationOptIn = value;
  }

  [DBProp("NotificationOptOut", AllowNull = true)]
  public DateTime NotificationOptOut
  {
    get => this._NotificationOptOut;
    set => this._NotificationOptOut = value;
  }

  [DBProp("CookieAcceptanceDate", AllowNull = true)]
  public DateTime CookieAcceptanceDate
  {
    get => this._CookieAcceptanceDate;
    set => this._CookieAcceptanceDate = value;
  }

  [DBProp("DeleteDate", AllowNull = true)]
  public DateTime DeleteDate
  {
    get => this._DeleteDate;
    set => this._DeleteDate = value;
  }

  [DBProp("HomepageLink", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string HomepageLink
  {
    get => this._HomepageLink;
    set
    {
      if (value == null)
        this._HomepageLink = string.Empty;
      else
        this._HomepageLink = value;
    }
  }

  [DBProp("SamlNameID", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string SamlNameID
  {
    get => this._SamlNameID;
    set => this._SamlNameID = value;
  }

  public bool ShowPopupNotice
  {
    get => this._ShowPopupNotice;
    set => this._ShowPopupNotice = value;
  }

  [DBProp("Image", AllowNull = true)]
  public byte[] Image
  {
    get => this._Image == null ? new byte[0] : this._Image;
    set => this._Image = value;
  }

  [DBProp("ImageName", MaxLength = 50, AllowNull = true)]
  public string ImageName
  {
    get => this._ImageName;
    set => this._ImageName = value;
  }

  [DBProp("ImageWidth", AllowNull = true)]
  public int ImageWidth
  {
    get => this._ImageWidth;
    set => this._ImageWidth = value;
  }

  [DBProp("ImageHeight", AllowNull = true)]
  public int ImageHeight
  {
    get => this._ImageHeight;
    set => this._ImageHeight = value;
  }

  [DBProp("Title", MaxLength = 255 /*0xFF*/, International = true)]
  public string Title
  {
    get => this._Title;
    set
    {
      if (value == null)
        this._Title = string.Empty;
      else
        this._Title = value;
    }
  }

  public List<CustomerInformation> CustomerInformationList
  {
    get
    {
      string key = $"CustomerInformation_{this.CustomerID}";
      List<CustomerInformation> customerInformationList = TimedCache.RetrieveObject<List<CustomerInformation>>(key);
      if (customerInformationList == null || customerInformationList.Count == 0)
      {
        customerInformationList = CustomerInformation.LoadByCustomerID(this.CustomerID);
        TimedCache.AddObjectToCach(key, (object) customerInformationList, new TimeSpan(0, 10, 0));
      }
      return customerInformationList;
    }
  }

  public string FormatDateTime(DateTime dt)
  {
    return dt.ToDateTimeFormatted(this.Preferences["Date Format"], this.Preferences["Time Format"]);
  }

  public string FormatLocalDateTime(DateTime dt)
  {
    return dt.ToLocalDateTimeFormatted(this.Account.TimeZoneID, this.Preferences["Date Format"], this.Preferences["Time Format"]);
  }

  public static byte[] FromBitmap(Bitmap source)
  {
    MemoryStream memoryStream = new MemoryStream();
    source.Save((Stream) memoryStream, ImageFormat.Bmp);
    return memoryStream.ToArray();
  }

  public static Bitmap FromBytes(byte[] bytes)
  {
    using (MemoryStream memoryStream = new MemoryStream(bytes))
      return new Bitmap((Stream) memoryStream);
  }

  public Bitmap Bitmap
  {
    get => Customer.FromBytes(this.Image);
    set
    {
      int width = value.Width;
      int height = value.Height;
      int num = (int) Math.Pow(2.0, (double) ((int) Math.Ceiling(Math.Log((double) Math.Max(width, height), 2.0)) + 1));
      if (width > height)
      {
        this.ImageWidth = num;
        this.ImageHeight = num * height / width;
      }
      else
      {
        this.ImageWidth = num * width / height;
        this.ImageHeight = num;
      }
      this.Image = Customer.FromBitmap(value);
    }
  }

  [DBProp("ForceLogoutDate")]
  public DateTime ForceLogoutDate
  {
    get => this._ForceLogoutDate;
    set => this._ForceLogoutDate = value;
  }

  [DBProp("PWACreateDate")]
  public DateTime PWACreateDate
  {
    get => this._PWACreateDate;
    set => this._PWACreateDate = value;
  }

  public bool IsInCustomerScheduleWindow(DateTime dateTime)
  {
    if (this.AlwaysSend)
      return true;
    CustomerSchedule customerSchedule = this.GetCustomerSchedule(dateTime.DayOfWeek);
    TimeSpan timeSpan = dateTime.Subtract(dateTime.Date);
    if (customerSchedule == null)
      return true;
    switch (customerSchedule.CustomerDaySchedule)
    {
      case eNotificationDaySchedule.All_Day:
        return true;
      case eNotificationDaySchedule.Off:
        return false;
      case eNotificationDaySchedule.Between:
        return timeSpan >= customerSchedule.FirstTime && timeSpan <= customerSchedule.SecondTime;
      case eNotificationDaySchedule.Before_and_After:
        return customerSchedule.FirstTime >= timeSpan || customerSchedule.SecondTime <= timeSpan;
      case eNotificationDaySchedule.Before:
        return customerSchedule.FirstTime >= timeSpan;
      case eNotificationDaySchedule.After:
        return customerSchedule.SecondTime <= timeSpan;
      default:
        return true;
    }
  }

  public bool isLocked()
  {
    Account account = Account.Load(this.AccountID);
    AccountTheme accountTheme = AccountTheme.Load(account.GetThemeID());
    return accountTheme != null && accountTheme.CustomerLocked(this.FailedLoginCount) || account.IsCFRCompliant && this.FailedLoginCount > 10 || account.CustomerLocked(this.FailedLoginCount);
  }

  public void unlock()
  {
    this.FailedLoginCount = 0;
    this.GUID = (string) null;
  }

  public bool PasswordIsExpired()
  {
    Account account = Account.Load(this.AccountID);
    AccountTheme accountTheme = AccountTheme.Load(account.GetThemeID());
    DateTime passwordChangeDate = this.PasswordChangeDate;
    if (false)
      this.PasswordChangeDate = DateTime.UtcNow;
    bool flag = accountTheme != null && accountTheme.PasswordExpired(this.PasswordChangeDate) || account.PasswordExpired(this.PasswordChangeDate);
    if (flag)
    {
      this.PasswordExpired = true;
      this.ForceLogoutDate = DateTime.Now;
    }
    this.Save();
    return flag || this.PasswordExpired;
  }

  public bool OutdatedTwoFactorAuth(HttpCookie cookie, long customerID)
  {
    bool flag = false;
    List<AuthenticateDevice> authenticateDeviceList = AuthenticateDevice.LoadByCustomerID(customerID);
    if (authenticateDeviceList.Count > 0 && cookie != null)
    {
      string str = cookie.Value;
      foreach (AuthenticateDevice authenticateDevice in authenticateDeviceList)
      {
        flag = StructuralComparisons.StructuralEqualityComparer.Equals((object) MonnitUtil.GenerateHash(str.ToString(), authenticateDevice.Salt, authenticateDevice.WorkFactor), (object) authenticateDevice.DeviceHash);
        if (flag)
          break;
      }
    }
    return flag;
  }

  public static Customer Load(long ID) => BaseDBObject.Load<Customer>(ID);

  public static Customer LoadFromUsername(string userName)
  {
    return new Monnit.Data.Customer.LoadFromUsername(userName).Result;
  }

  public override void Delete()
  {
    try
    {
      if (Customer.DeleteByCustomerID(this.CustomerID).Rows[0][0].ToInt() == 0)
        throw new Exception("User is a primary contact");
    }
    catch
    {
      throw;
    }
  }

  public Account Account
  {
    get
    {
      if (this._Account == null)
        this._Account = Account.Load(this.AccountID);
      if (this._Account != null && this._Account.AccountID != this.AccountID)
        this._Account = Account.Load(this.AccountID);
      return this._Account;
    }
    set => this._Account = (Account) null;
  }

  public Account DefaultAccount
  {
    get
    {
      if (this._DefaultAccount == null)
        this._DefaultAccount = Account.Load(Customer.Load(this.CustomerID).AccountID);
      return this._DefaultAccount;
    }
  }

  public void DefaultAccountReload() => this._DefaultAccount = (Account) null;

  public Dictionary<string, string> Preferences
  {
    get
    {
      if (this._Preferences == null)
        this._Preferences = Preference.LoadPreferences(this.Account.GetThemeID(), this.AccountID, this.CustomerID);
      return this._Preferences;
    }
    set => this._Preferences = value;
  }

  public void ClearPreferences() => this._Preferences = (Dictionary<string, string>) null;

  public string FullName => $"{this.FirstName} {this.LastName}";

  public string UniqueName => $"{this.FirstName} {this.LastName} ({this.NotificationEmail})";

  public SMSCarrier SMSCarrier
  {
    get
    {
      if (this._SMSCarrier == null)
        this._SMSCarrier = SMSCarrier.Load(this.SMSCarrierID);
      return this._SMSCarrier;
    }
    set
    {
      this._SMSCarrier = value;
      this.SMSCarrierID = value.SMSCarrierID;
    }
  }

  public long? UISMSCarrierID
  {
    get => new long?(this.SMSCarrierID);
    set
    {
      int num1;
      if (value.HasValue)
      {
        long? nullable = value;
        long num2 = 0;
        num1 = nullable.GetValueOrDefault() == num2 & nullable.HasValue ? 1 : 0;
      }
      else
        num1 = 1;
      if (num1 != 0)
        this.SMSCarrierID = long.MinValue;
      else
        this.SMSCarrierID = value.ToLong();
    }
  }

  public string ConfirmPassword { get; set; }

  public static Customer Authenticate(
    string username,
    string password,
    string application,
    string ipAddress,
    bool useEncryption)
  {
    return new Monnit.Data.Customer.Authenticate(username, password, application, ipAddress, useEncryption).Result;
  }

  public ReleaseNoteViewed LastViewedReleaseNote(long custid)
  {
    return new Monnit.Data.Customer.LastViewedReleaseNote(custid).Result;
  }

  public static List<Customer> LoadAllByAccount(long accountID)
  {
    return new Monnit.Data.Customer.LoadAllByAccountID(accountID).Result;
  }

  public static List<Customer> LoadAllByEmail(string email)
  {
    return new Monnit.Data.Customer.LoadAllByEmail(email).Result;
  }

  public static List<Customer> LoadByCustomerGroupID(long customerGroupID)
  {
    return new Monnit.Data.Customer.LoadByGroupID(customerGroupID).Result;
  }

  public static Customer LoadByAccountAndSamlNameID(long accountID, string SamlNameID)
  {
    return new Monnit.Data.Customer.LoadByAccountAndSamlNameID(accountID, SamlNameID).Result;
  }

  public static List<Customer> LoadAllForReseller(long CustomerID, long TargetAccountID)
  {
    return new Monnit.Data.Customer.LoadAllForReseller(CustomerID, TargetAccountID).Result;
  }

  public static bool CheckUsernameIsUnique(string username)
  {
    return !string.IsNullOrEmpty(username) && new Monnit.Data.Customer.CheckUsernameIsUnique(username).Result;
  }

  public static bool CheckNotificationEmailIsUnique(string NewNotificationEmail, long CustomerID)
  {
    return !string.IsNullOrEmpty(NewNotificationEmail) && new Monnit.Data.Customer.CheckNotificationEmailIsUnique(NewNotificationEmail, new long?(CustomerID)).Result;
  }

  public static bool CheckNotificationEmailIsUnique(string NewNotificationEmail)
  {
    return !string.IsNullOrEmpty(NewNotificationEmail) && new Monnit.Data.Customer.CheckNotificationEmailIsUnique(NewNotificationEmail, new long?()).Result;
  }

  public override void Save()
  {
    if (this.Password2 == null)
      this.Password2 = new byte[1];
    if (this.Salt == null)
      this.Salt = new byte[1];
    Customer customer = Customer.Load(this.CustomerID);
    if (customer != null)
    {
      if (this.AccountID != customer.AccountID)
      {
        try
        {
          throw new Exception("Changing accountID on Customer not permitted");
        }
        catch (Exception ex)
        {
          ex.Log("Customer Save Called with Bad AccountID: ");
        }
        this.AccountID = customer.AccountID;
      }
      if ((this.Password2.Length == 1 || this.Salt.Length == 1) && customer.Password2 != null && customer.Password2.Length > 1)
      {
        this.Password2 = customer.Password2;
        this.Salt = customer.Salt;
      }
    }
    bool flag = this.CustomerID == long.MinValue;
    base.Save();
    if (!flag)
      return;
    this.CreateDefaultPermissions();
  }

  public List<CustomerPermission> Permissions
  {
    get
    {
      if (this._Permissions == null)
        this._Permissions = CustomerPermission.LoadPermissions(this);
      return this._Permissions;
    }
    set => this._Permissions = value;
  }

  public void ClearPermissions() => this._Permissions = (List<CustomerPermission>) null;

  public bool Can(string permissionType)
  {
    try
    {
      return this.Can(CustomerPermissionType.Find(permissionType));
    }
    catch
    {
      return false;
    }
  }

  public bool Can(CustomerPermissionType permissionType) => this.Can(permissionType, long.MinValue);

  public bool Can(CustomerPermissionType permissionType, long networkID)
  {
    CustomerPermission customerPermission = this.Permission(permissionType.Name, networkID);
    if (customerPermission != null)
      return customerPermission.Can;
    return this.AccountID.ToString() == ConfigData.AppSettings("AdminAccountID") || permissionType.NetworkSpecific && this.AccountID != this.DefaultAccount.AccountID;
  }

  public bool CanSeeSensor(long sensorID)
  {
    Sensor sensor = Sensor.Load(sensorID);
    return sensor != null && this.CanSeeSensor(sensor);
  }

  public bool CanSeeSensor(Sensor sensor) => this.CanSeeNetwork(sensor.CSNetID);

  public bool CanSeeGateway(long gatewayID)
  {
    Gateway gateway = Gateway.Load(gatewayID);
    return gateway != null && this.CanSeeGateway(gateway);
  }

  public bool CanSeeGateway(Gateway gateway) => this.CanSeeNetwork(gateway.CSNetID);

  public bool CanSeeNetwork(long csNetID)
  {
    if (this.CanSeeNetworkDict == null)
      this.CanSeeNetworkDict = new Dictionary<long, bool>();
    CSNet ntwk = CSNet.Load(csNetID);
    if (ntwk == null)
      return false;
    if (!this.CanSeeNetworkDict.ContainsKey(csNetID))
    {
      try
      {
        this.CanSeeNetworkDict.Add(csNetID, this.CanSeeNetwork(ntwk));
      }
      catch
      {
      }
    }
    return this.CanSeeNetworkDict[csNetID];
  }

  public bool CanSeeNetwork(CSNet ntwk)
  {
    if (this.CanSeeNetworkDict == null)
      this.CanSeeNetworkDict = new Dictionary<long, bool>();
    if (ntwk == null)
      return false;
    if (!this.CanSeeNetworkDict.ContainsKey(ntwk.CSNetID))
    {
      try
      {
        this.CanSeeNetworkDict.Add(ntwk.CSNetID, this.CanSeeNetworkPrivate(ntwk));
      }
      catch
      {
      }
    }
    return this.CanSeeNetworkDict[ntwk.CSNetID];
  }

  private bool CanSeeNetworkPrivate(CSNet ntwk)
  {
    if (ntwk == null)
      return false;
    Account account = Account.Load(ntwk.AccountID);
    if (account == null)
      return false;
    CustomerPermission customerPermission = this.Permission("Network_View", ntwk.CSNetID);
    if (customerPermission != null)
      return customerPermission.Can;
    if (this.AccountID == ntwk.AccountID && (this.IsAdmin || account.PrimaryContactID == this.CustomerID))
      return true;
    Customer customer = this;
    if (this.DefaultAccount.AccountID != this.AccountID)
    {
      customer = Customer.Load(this.CustomerID);
      if (customer == null)
        return false;
    }
    return customer.AccountID == ntwk.AccountID && (customer.IsAdmin || account.PrimaryContactID == customer.CustomerID) || customer.AccountID.ToString() == ConfigData.AppSettings("AdminAccountID") || account.RetailAccountID == this.AccountID || this.CanProxySubAccount(account.AccountID) || this.CanProxyCustomerAccountLink(account.AccountID);
  }

  public List<CustomerAccountLink> CustomerAccountLinkList
  {
    get
    {
      if (this._CustomerAccountLinkList == null)
        this._CustomerAccountLinkList = CustomerAccountLink.LoadAllByCustomerID(this.CustomerID);
      return this._CustomerAccountLinkList;
    }
  }

  private bool CanProxyCustomerAccountLink(long acctID)
  {
    return this.CustomerAccountLinkList.FirstOrDefault<CustomerAccountLink>((System.Func<CustomerAccountLink, bool>) (cal => cal.AccountID == acctID)) != null;
  }

  private bool CanProxySubAccount(long acctID)
  {
    return this.DefaultAccount.AccountID != acctID && Account.IsSubAccount(this.DefaultAccount.AccountID, acctID);
  }

  public bool CanAssignParent(long accountID) => this.CanAssignParent(Account.Load(accountID));

  public bool CanAssignParent(Account account)
  {
    if (account == null)
      return false;
    if (this.IsMonnitAdmin())
      return true;
    Customer customer = this;
    if (this.DefaultAccount.AccountID != this.AccountID)
      customer = Customer.Load(this.CustomerID);
    return customer.CanSeeAccount(account) && customer.IsAdmin;
  }

  public bool CanSeeAccount(long accountID) => this.CanSeeAccount(Account.Load(accountID));

  public bool CanSeeAccount(Account acct)
  {
    if (acct == null)
      return false;
    Customer customer = this;
    if (this.DefaultAccount.AccountID != this.AccountID)
      customer = Customer.Load(this.CustomerID);
    return customer.AccountID.ToString() == ConfigData.AppSettings("AdminAccountID") || customer.AccountID == acct.AccountID || customer.AccountID == acct.RetailAccountID && customer.IsAdmin || this.CanProxyCustomerAccountLink(acct.AccountID) || this.CanProxySubAccount(acct.AccountID);
  }

  public bool IsMonnitAdmin()
  {
    Customer customer = this;
    if (this.DefaultAccount.AccountID != this.AccountID)
      customer = Customer.Load(this.CustomerID);
    return customer.AccountID.ToString() == ConfigData.AppSettings("AdminAccountID");
  }

  public bool CanViewTheme(AccountTheme theme)
  {
    if (!ConfigData.AppSettings("IsEnterprise").ToBool() && this.Can(CustomerPermissionType.Find("View_All_AccountThemes")) || this.AccountID.ToString() == ConfigData.AppSettings("AdminAccountID") || !ConfigData.AppSettings("CheckTheme").ToBool())
      return true;
    if (this.Account == null)
      return false;
    if (theme.AccountID == this.AccountID || this.Account.RetailAccountID == theme.AccountID || this.Account.RetailAccountID == long.MinValue && theme.Theme == "Default")
      return true;
    for (Account account = Account.Load(this.Account.RetailAccountID); account != null && account.RetailAccountID > 0L; account = Account.Load(account.RetailAccountID))
    {
      AccountTheme accountTheme = AccountTheme.Find(account.AccountID);
      if (accountTheme != null && accountTheme.Theme != theme.Theme)
        return false;
      if (account.RetailAccountID == theme.AccountID)
        return true;
    }
    return false;
  }

  public bool IsThemeAdmin(AccountTheme theme)
  {
    return this.Can(CustomerPermissionType.Find("ThemeAdmin")) && this.CanViewTheme(theme);
  }

  public CustomerPermission Permission(string customerPermissionTypeName)
  {
    return this.Permission(customerPermissionTypeName, long.MinValue);
  }

  public CustomerPermission Permission(string customerPermissionTypeName, long csNetID)
  {
    foreach (CustomerPermission permission in this.Permissions)
    {
      if (permission.Type.Name == customerPermissionTypeName && (!permission.Type.NetworkSpecific || permission.CSNetID == csNetID))
        return permission;
    }
    return (CustomerPermission) null;
  }

  private void CreateDefaultPermissions()
  {
    foreach (CustomerPermissionType PT in CustomerPermissionType.LoadAll())
    {
      if (PT.StandardUser)
      {
        if (PT.NetworkSpecific)
        {
          foreach (CSNet csnet in CSNet.LoadByAccountID(this.AccountID))
            this.CreatePermission(PT, csnet);
        }
        else
          this.CreatePermission(PT, (CSNet) null);
      }
      else if (this.IsAdmin && PT.AdvancedUser)
      {
        if (PT.NetworkSpecific)
        {
          foreach (CSNet csnet in CSNet.LoadByAccountID(this.AccountID))
            this.CreatePermission(PT, csnet);
        }
        else
          this.CreatePermission(PT, (CSNet) null);
      }
    }
  }

  public void ResetPermissions()
  {
    foreach (CustomerPermissionType PT in CustomerPermissionType.LoadAll())
    {
      if (PT.NetworkSpecific)
      {
        foreach (CSNet csnet in CSNet.LoadByAccountID(this.AccountID))
        {
          CustomerPermission customerPermission = this.Permission(PT.Name, csnet.CSNetID);
          if (customerPermission == null)
          {
            if (this.IsAdmin && PT.AdvancedUser || PT.StandardUser)
              this.CreatePermission(PT, csnet);
          }
          else
          {
            customerPermission.Can = this.IsAdmin && PT.AdvancedUser || PT.StandardUser;
            customerPermission.Save();
          }
        }
      }
      else
      {
        CustomerPermission customerPermission = this.Permission(PT.Name);
        if (customerPermission == null)
        {
          if (this.IsAdmin && PT.AdvancedUser || PT.StandardUser)
            this.CreatePermission(PT, (CSNet) null);
        }
        else
        {
          customerPermission.Can = this.IsAdmin && PT.AdvancedUser || PT.StandardUser;
          customerPermission.Save();
        }
      }
    }
  }

  private void CreatePermission(CustomerPermissionType PT, CSNet csnet)
  {
    CustomerPermission customerPermission = new CustomerPermission();
    if (csnet != null)
      customerPermission.CSNetID = csnet.CSNetID;
    customerPermission.CustomerID = this.CustomerID;
    customerPermission.Can = true;
    customerPermission.CustomerPermissionTypeID = PT.CustomerPermissionTypeID;
    customerPermission.Save();
  }

  private List<ReleaseNoteViewed> Acknowledged
  {
    get
    {
      if (this._Acknowledged == null || this._Acknowledged.Count == 0)
        this._Acknowledged = ReleaseNoteViewed.LoadByCustomerID(this.CustomerID);
      return this._Acknowledged;
    }
  }

  public bool HasSeenReleaseNote(ReleaseNote note)
  {
    return note == null || this.HasSeenReleaseNote(note.ReleaseNoteID);
  }

  public bool HasSeenReleaseNote(long releaseNoteID)
  {
    foreach (ReleaseNoteViewed releaseNoteViewed in this.Acknowledged)
    {
      if (releaseNoteViewed.ReleaseNoteID == releaseNoteID)
        return true;
    }
    return false;
  }

  public void AcknowledgeReleaseNote(long releaseNoteID, long customerID, bool viewed)
  {
    try
    {
      ReleaseNoteViewed releaseNoteViewed = new ReleaseNoteViewed();
      releaseNoteViewed.ReleaseNoteID = releaseNoteID;
      releaseNoteViewed.CustomerID = customerID;
      releaseNoteViewed.Viewed = viewed;
      releaseNoteViewed.ViewDate = DateTime.UtcNow;
      releaseNoteViewed.Save();
      this.Acknowledged.Add(releaseNoteViewed);
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
    }
  }

  public MaintenanceWindowCustomer MaintWindowAcked(
    long maintWindowID,
    eMaintenanceWindowCustomerType type)
  {
    if (this._MaintWindowAcked == null || this._MaintWindowAcked.MaintenanceWindowID != maintWindowID)
      this._MaintWindowAcked = MaintenanceWindowCustomer.LoadByWindowAndCustomerAndType(maintWindowID, this.CustomerID, type);
    return this._MaintWindowAcked;
  }

  public int CheckPreferenceCookie(HttpCookie cookie)
  {
    int num = cookie["Sequence"].ToInt();
    if (num > 0)
    {
      foreach (CustomerRememberMe customerRememberMe in CustomerRememberMe.LoadByCustomer(this.CustomerID))
      {
        if (customerRememberMe.SequenceNumber == num)
          return customerRememberMe.Recent == cookie["Recent"].ToInt() ? customerRememberMe.UpdateRecent() : -1;
      }
    }
    return 0;
  }

  public void RemovePreferenceCookie(HttpCookie cookie)
  {
    int num = cookie["Sequence"].ToInt();
    if (num <= 0)
      return;
    foreach (CustomerRememberMe customerRememberMe in CustomerRememberMe.LoadByCustomer(this.CustomerID))
    {
      if (customerRememberMe.SequenceNumber == num)
        customerRememberMe.Delete();
    }
  }

  public void DeleteRememberMe()
  {
    foreach (BaseDBObject baseDbObject in CustomerRememberMe.LoadByCustomer(this.CustomerID))
      baseDbObject.Delete();
  }

  public static DataTable Search(string search) => new Monnit.Data.Customer.Search(search).Result;

  public static DataTable DeleteByCustomerID(long CustomerID)
  {
    return new Monnit.Data.Customer.DeleteByCustomerID(CustomerID).Result;
  }

  public static bool PrimaryContactCheck(long CustomerID)
  {
    return new Monnit.Data.Customer.PrimaryContactCheck(CustomerID).Result;
  }

  public static DataTable SearchPotentialNotificationRecipient(
    long customerID,
    long notificationID,
    string query)
  {
    return new Monnit.Data.Customer.SearchPotentialNotificationRecipient(customerID, notificationID, query).Result;
  }

  public static DateTime CheckForceLogoutDate(long customerID)
  {
    return new Monnit.Data.Customer.CheckForceLogoutDate(customerID).Result;
  }

  public static int SetForceLogoutDate(long customerID)
  {
    return new Monnit.Data.Customer.SetForceLogoutDate(customerID).Result;
  }
}
