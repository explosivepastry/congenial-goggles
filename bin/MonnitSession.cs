// Decompiled with JetBrains decompiler
// Type: iMonnit.MonnitSession
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.ControllerBase;
using iMonnit.Controllers;
using iMonnit.Models;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.SessionState;

#nullable disable
namespace iMonnit;

public static class MonnitSession
{
  private static bool _UseEncryption = true;
  private static bool _UseEncryptionSet = false;
  private static eProgramLevel _ProgramLevel = eProgramLevel.NKR;
  private static DateTime _EnterpriseKillDate = new DateTime(2099, 1, 1);
  private static object lockOTACheckGateways = new object();
  private static object lockOTACheckSensors = new object();
  private static object lockUpdateableCheckGateways = new object();

  public static bool UseEncryption
  {
    get
    {
      if (MonnitSession._UseEncryptionSet)
      {
        try
        {
          MonnitSession._UseEncryption = MonnitUtil.UseEncryption();
          MonnitSession._UseEncryptionSet = true;
        }
        catch
        {
        }
      }
      return MonnitSession._UseEncryption;
    }
  }

  public static HttpSessionState Session => HttpContext.Current.Session;

  public static Customer CurrentCustomer
  {
    get
    {
      if ((MonnitSession.Session[nameof (CurrentCustomer)] == null || !(MonnitSession.Session[nameof (CurrentCustomer)] is Customer)) && HttpContext.Current.User.Identity.IsAuthenticated)
        MonnitSession.Session[nameof (CurrentCustomer)] = (object) Customer.LoadFromUsername(HttpContext.Current.User.Identity.Name);
      return MonnitSession.Session[nameof (CurrentCustomer)] as Customer;
    }
    set => MonnitSession.Session[nameof (CurrentCustomer)] = (object) value;
  }

  public static Customer OldCustomer
  {
    get => MonnitSession.Session[nameof (OldCustomer)] as Customer;
    set => MonnitSession.Session[nameof (OldCustomer)] = (object) value;
  }

  public static void ProxyUser(Customer newCustomer)
  {
    if (MonnitSession.CustomerIDLoggedInAsProxy <= 0L)
    {
      MonnitSession.CustomerIDLoggedInAsProxy = MonnitSession.CurrentCustomer.CustomerID;
      MonnitSession.OldCustomer = Customer.Load(MonnitSession.CurrentCustomer.CustomerID);
    }
    MonnitSession.CurrentCustomer = newCustomer;
    MonnitSession.Session["HistoryFromDate"] = (object) null;
    MonnitSession.Session["HistoryToDate"] = (object) null;
    MonnitSession.Session["chartSensorList"] = (object) null;
    MonnitSession.SensorListFilters.Clear();
  }

  public static bool UnProxySimple()
  {
    Customer newCustomer = Customer.Load(MonnitSession.CustomerIDLoggedInAsProxy);
    if (newCustomer == null)
      return false;
    MonnitSession.UserIsAccountProxied = false;
    MonnitSession.ProxyUser(newCustomer);
    MonnitSession.CustomerIDLoggedInAsProxy = long.MinValue;
    MonnitSession.OldCustomer = (Customer) null;
    return true;
  }

  public static bool AccountViewProxy(long accountID)
  {
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(accountID))
      return false;
    try
    {
      Customer newCustomer = Customer.Load(MonnitSession.CurrentCustomer.CustomerID);
      newCustomer.AccountID = accountID;
      MonnitSession.ProxyUser(newCustomer);
      return true;
    }
    catch
    {
      return false;
    }
  }

  public static bool IsCurrentCustomerMonnitAdmin
  {
    get => MonnitSession.CurrentCustomer != null && MonnitSession.CurrentCustomer.IsMonnitAdmin();
  }

  public static bool IsCurrentCustomerMonnitSuperAdmin
  {
    get
    {
      return MonnitSession.IsCurrentCustomerMonnitAdmin && ConfigData.AppSettings("IsSuperAdmin") == "true";
    }
  }

  public static bool IsCurrentCustomerAccountThemeAdmin
  {
    get
    {
      return MonnitSession.CurrentCustomer != null && MonnitSession.CurrentTheme.Account != null && MonnitSession.CurrentCustomer.IsThemeAdmin(MonnitSession.CurrentTheme);
    }
  }

  public static bool IsServerHosted => ConfigData.AppSettings("IsSuperAdmin") == "true";

  public static bool IsLinkedToThisAccount(long customerID, long accountID)
  {
    foreach (CustomerAccountLink customerAccountLink in CustomerAccountLink.LoadAllByCustomerID(customerID))
    {
      if (accountID == customerAccountLink.AccountID && !customerAccountLink.AccountDeleted)
        return true;
    }
    return false;
  }

  public static bool IsAuthorizedForAccount(long AccountID)
  {
    return MonnitSession.CurrentCustomer.CanSeeAccount(AccountID);
  }

  public static bool IsEnterprise
  {
    get => ConfigData.AppSettings(nameof (IsEnterprise)).ToLower() == "true";
  }

  public static bool IsEnterpriseAdmin
  {
    get => MonnitSession.CurrentCustomer.IsAdmin && MonnitSession.IsEnterprise;
  }

  public static bool IsAccountNew
  {
    get
    {
      if ((MonnitSession.Session[nameof (IsAccountNew)] == null || !(MonnitSession.Session[nameof (IsAccountNew)] is bool)) && HttpContext.Current.User.Identity.IsAuthenticated)
        MonnitSession.Session[nameof (IsAccountNew)] = (object) false;
      return MonnitSession.Session[nameof (IsAccountNew)] != null && MonnitSession.Session[nameof (IsAccountNew)].ToBool();
    }
    set => MonnitSession.Session[nameof (IsAccountNew)] = (object) value;
  }

  public static AnnouncementPopup NewestAnnouncement
  {
    get
    {
      if (MonnitSession.CurrentCustomer != null && (MonnitSession.Session["AnnouncementPopup"] == null || !(MonnitSession.Session["AnnouncementPopup"] is AnnouncementPopup)))
        MonnitSession.Session["AnnouncementPopup"] = (object) Announcement.LoadByCustomerIDProc(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentTheme.AccountThemeID, long.MinValue);
      return MonnitSession.Session["AnnouncementPopup"] as AnnouncementPopup;
    }
    set => MonnitSession.Session["AnnouncementPopup"] = (object) value;
  }

  public static VisualMap CurrentVisualMap
  {
    get => MonnitSession.Session[nameof (CurrentVisualMap)] as VisualMap;
    set => MonnitSession.Session[nameof (CurrentVisualMap)] = (object) value;
  }

  public static long CustomerIDLoggedInAsProxy
  {
    get => MonnitSession.Session[nameof (CustomerIDLoggedInAsProxy)].ToLong();
    set => MonnitSession.Session[nameof (CustomerIDLoggedInAsProxy)] = (object) value;
  }

  public static bool UserIsProxied
  {
    get
    {
      return MonnitSession.OldCustomer != null && MonnitSession.CustomerIDLoggedInAsProxy > 0L && (MonnitSession.CurrentCustomer.CustomerID != MonnitSession.OldCustomer.CustomerID || MonnitSession.CurrentCustomer.AccountID != MonnitSession.OldCustomer.AccountID);
    }
  }

  public static bool UserIsCustomerProxied => MonnitSession.CustomerIDLoggedInAsProxy > 0L;

  public static bool UserIsAccountProxied
  {
    get
    {
      return MonnitSession.Session[nameof (UserIsAccountProxied)] != null && (bool) MonnitSession.Session[nameof (UserIsAccountProxied)];
    }
    set => MonnitSession.Session[nameof (UserIsAccountProxied)] = (object) value;
  }

  public static DateTime HistoryFromDate
  {
    get
    {
      if (MonnitSession.Session[nameof (HistoryFromDate)] == null)
        MonnitSession.Session[nameof (HistoryFromDate)] = !string.IsNullOrEmpty(MonnitSession.CurrentCustomer.Preferences["Days to Load"]) ? (object) MonnitSession.MakeLocal(DateTime.UtcNow.AddDays(-1.0)) : (object) MonnitSession.MakeLocal(DateTime.UtcNow.AddDays((double) -MonnitSession.CurrentCustomer.Preferences["Days to Load"].ToInt()));
      return (DateTime) MonnitSession.Session[nameof (HistoryFromDate)];
    }
    set => MonnitSession.Session[nameof (HistoryFromDate)] = (object) value;
  }

  public static DateTime HistoryToDate
  {
    get
    {
      if (MonnitSession.Session[nameof (HistoryToDate)] == null)
        MonnitSession.Session[nameof (HistoryToDate)] = !string.IsNullOrEmpty(MonnitSession.CurrentCustomer.Preferences["Days to Load"]) ? (object) MonnitSession.MakeLocal(DateTime.UtcNow.AddHours(6.0)) : (object) MonnitSession.MakeLocal(DateTime.UtcNow.AddDays(1.0).AddSeconds(-1.0));
      return (DateTime) MonnitSession.Session[nameof (HistoryToDate)];
    }
    set => MonnitSession.Session[nameof (HistoryToDate)] = (object) value;
  }

  public static string CurrentDateRange
  {
    get
    {
      DateTime dateTime1 = MonnitSession.HistoryFromDate;
      DateTime date1 = dateTime1.Date;
      dateTime1 = MonnitSession.HistoryToDate;
      DateTime date2 = dateTime1.Date;
      if (date1 == date2)
        return MonnitSession.HistoryFromDate.ToShortDateString();
      DateTime dateTime2 = MonnitSession.HistoryFromDate;
      string shortDateString1 = dateTime2.ToShortDateString();
      dateTime2 = MonnitSession.HistoryToDate;
      string shortDateString2 = dateTime2.ToShortDateString();
      return $"{shortDateString1} - {shortDateString2}";
    }
    set
    {
      try
      {
        string[] strArray = value.Split('-');
        MonnitSession.HistoryFromDate = DateTime.Parse(strArray[0]);
        if (strArray.Length > 1)
          MonnitSession.HistoryToDate = DateTime.Parse(strArray[1]);
        else
          MonnitSession.HistoryToDate = DateTime.Parse(strArray[0]);
      }
      catch (Exception ex)
      {
        ex.Log("MonnitSession.CurrentDateRange | values = " + value);
        MonnitSession.HistoryFromDate = DateTime.Today.AddDays(-7.0);
        MonnitSession.HistoryToDate = DateTime.Today;
      }
    }
  }

  public static DateTime MakeUTC(DateTime local)
  {
    return Monnit.TimeZone.GetUTCFromLocalById(local, MonnitSession.CurrentCustomer.Account.TimeZoneID);
  }

  public static DateTime MakeLocal(DateTime utc)
  {
    return Monnit.TimeZone.GetLocalTimeById(utc, MonnitSession.CurrentCustomer.Account.TimeZoneID);
  }

  public static TimeSpan UTCOffset
  {
    get => MonnitSession.MakeLocal(DateTime.UtcNow).Subtract(DateTime.UtcNow);
  }

  public static ReleaseNote LastReleaseNoteViewed
  {
    get
    {
      if (MonnitSession.Session["CurrentReleaseNote"] == null)
        MonnitSession.Session["CurrentReleaseNote"] = (object) ReleaseNote.Load(MonnitSession.CurrentCustomer.LastViewedReleaseNote(MonnitSession.CurrentCustomer.CustomerID).ReleaseNoteID);
      return (ReleaseNote) MonnitSession.Session["CurrentReleaseNote"];
    }
  }

  public static DisplayMessageModel DisplayMessage
  {
    get => (DisplayMessageModel) MonnitSession.Session["CurrentDisplayMessage"];
    set => MonnitSession.Session["CurrentDisplayMessage"] = (object) value;
  }

  public static ErrorModel ErrorDisplayModel
  {
    get
    {
      if (MonnitSession.Session["CurrentErrorDisplayModel"] == null || string.IsNullOrEmpty(((ErrorModel) MonnitSession.Session["CurrentErrorDisplayModel"]).ErrorTitle))
        MonnitSession.Session["CurrentErrorDisplayModel"] = (object) new ErrorModel("Error", "NewError|", "Error", "Unknown", "Unknown");
      return (ErrorModel) MonnitSession.Session["CurrentErrorDisplayModel"];
    }
    set => MonnitSession.Session["CurrentErrorDisplayModel"] = (object) value;
  }

  public static bool CustomerCan(string permissionType)
  {
    try
    {
      return MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find(permissionType));
    }
    catch
    {
      return false;
    }
  }

  public static bool CustomerCan(string permissionType, long csNetID)
  {
    try
    {
      return MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find(permissionType), csNetID);
    }
    catch
    {
      return false;
    }
  }

  public static bool AccountCan(string featureKeyName)
  {
    try
    {
      return MonnitSession.CurrentSubscription.Can(Feature.Find(featureKeyName));
    }
    catch
    {
      return false;
    }
  }

  public static int AccountSensorTotal
  {
    get
    {
      if (MonnitSession.Session[nameof (AccountSensorTotal)] == null || (int) MonnitSession.Session[nameof (AccountSensorTotal)] == int.MinValue)
        MonnitSession.Session[nameof (AccountSensorTotal)] = (object) Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Count;
      return (int) MonnitSession.Session[nameof (AccountSensorTotal)];
    }
    set => MonnitSession.Session[nameof (AccountSensorTotal)] = (object) value;
  }

  public static AccountSubscription CurrentSubscription
  {
    get
    {
      if (MonnitSession.Session[nameof (CurrentSubscription)] == null || (MonnitSession.Session[nameof (CurrentSubscription)] as AccountSubscription).AccountID != MonnitSession.CurrentCustomer.AccountID)
        MonnitSession.Session[nameof (CurrentSubscription)] = (object) MonnitSession.CurrentCustomer.Account.CurrentSubscription;
      return MonnitSession.Session[nameof (CurrentSubscription)] as AccountSubscription;
    }
    set => MonnitSession.Session[nameof (CurrentSubscription)] = (object) value;
  }

  public static Notification NotificationInProgress
  {
    get
    {
      if (MonnitSession.Session[nameof (NotificationInProgress)] == null || (MonnitSession.Session[nameof (NotificationInProgress)] as Notification).AccountID != MonnitSession.CurrentCustomer.AccountID)
        MonnitSession.Session[nameof (NotificationInProgress)] = (object) new Notification()
        {
          AccountID = MonnitSession.CurrentCustomer.AccountID
        };
      return MonnitSession.Session[nameof (NotificationInProgress)] as Notification;
    }
    set => MonnitSession.Session[nameof (NotificationInProgress)] = (object) value;
  }

  public static Notification ScheduledRuleInProgress
  {
    get
    {
      if (MonnitSession.Session[nameof (ScheduledRuleInProgress)] == null || (MonnitSession.Session[nameof (ScheduledRuleInProgress)] as Notification).AccountID != MonnitSession.CurrentCustomer.AccountID)
        MonnitSession.Session[nameof (ScheduledRuleInProgress)] = (object) new Notification();
      return MonnitSession.Session[nameof (ScheduledRuleInProgress)] as Notification;
    }
    set => MonnitSession.Session[nameof (ScheduledRuleInProgress)] = (object) value;
  }

  public static List<NotificationRecipient> NotificationRecipientsInProgress
  {
    get
    {
      if (MonnitSession.Session[nameof (NotificationRecipientsInProgress)] == null)
        MonnitSession.Session[nameof (NotificationRecipientsInProgress)] = (object) new List<NotificationRecipient>();
      return MonnitSession.Session[nameof (NotificationRecipientsInProgress)] as List<NotificationRecipient>;
    }
    set => MonnitSession.Session[nameof (NotificationRecipientsInProgress)] = (object) value;
  }

  public static List<AdvancedNotificationParameterValue> AdvancedNotificationParameterValuesInProgress
  {
    get
    {
      if (MonnitSession.Session["AdvancedNotificationParameterValueList"] == null || (MonnitSession.Session["AdvancedNotificationParameterValueList"] as List<AdvancedNotificationParameterValue>).Count == 0)
        MonnitSession.Session["AdvancedNotificationParameterValueList"] = (object) new List<AdvancedNotificationParameterValue>();
      return MonnitSession.Session["AdvancedNotificationParameterValueList"] as List<AdvancedNotificationParameterValue>;
    }
    set => MonnitSession.Session["AdvancedNotificationParameterValueList"] = (object) value;
  }

  public static AccountTheme CurrentTheme
  {
    get
    {
      if (!string.IsNullOrWhiteSpace(ConfigData.AppSettings("HardTheme")))
      {
        AccountTheme currentTheme = AccountTheme.ThemesCache().Where<AccountTheme>((Func<AccountTheme, bool>) (t => t.Theme == ConfigData.AppSettings("HardTheme"))).FirstOrDefault<AccountTheme>();
        if (currentTheme != null)
          return currentTheme;
      }
      return AccountTheme.Find(HttpContext.Current.Request.Url.DnsSafeHost);
    }
  }

  public static AccountThemeStyleGroup CurrentStyleGroup
  {
    get
    {
      if (MonnitSession.Session[nameof (CurrentStyleGroup)] == null && MonnitSession.CurrentTheme.StyleGroups.Count > 0)
        MonnitSession.Session[nameof (CurrentStyleGroup)] = (object) MonnitSession.CurrentTheme.StyleGroups.FirstOrDefault<AccountThemeStyleGroup>();
      return MonnitSession.Session[nameof (CurrentStyleGroup)] as AccountThemeStyleGroup;
    }
  }

  public static Dictionary<string, AccountThemeStyle> CurrentStyles
  {
    get
    {
      if (MonnitSession.Session[nameof (CurrentStyles)] == null)
      {
        Dictionary<string, AccountThemeStyle> dictionary = new Dictionary<string, AccountThemeStyle>();
        if (MonnitSession.CurrentStyleGroup != null)
        {
          foreach (AccountThemeStyle style in MonnitSession.CurrentStyleGroup.Styles)
            dictionary.Add(style.Type.Property, style);
        }
        MonnitSession.Session[nameof (CurrentStyles)] = (object) dictionary;
      }
      return MonnitSession.Session[nameof (CurrentStyles)] as Dictionary<string, AccountThemeStyle>;
    }
  }

  public static bool ShowPopupNotice(ePopupNoticeType type)
  {
    return MonnitSession.CurrentCustomer.ShowPopupNotice && MonnitSession.GetPopupNotice(type) != null;
  }

  public static PopupNoticeRecord GetPopupNotice(ePopupNoticeType type)
  {
    PopupNoticeRecord popupNoticeRecord = MonnitSession.PopupNoticeRecords.Where<PopupNoticeRecord>((Func<PopupNoticeRecord, bool>) (p => p.PopupNoticeType == type && p.CustomerID == MonnitSession.CurrentCustomer.CustomerID && p.AccountID == MonnitSession.CurrentCustomer.AccountID && string.IsNullOrEmpty(p.SKU) && string.IsNullOrEmpty(p.FirmwareVersionToIgnore))).FirstOrDefault<PopupNoticeRecord>() ?? MonnitSession.GetPopupNotice(type, MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID);
    int num = ConfigData.AppSettings("PopupNoticeHideDays", "7").ToInt();
    return popupNoticeRecord != null && DateTime.UtcNow.Subtract(popupNoticeRecord.DateLastSeen).Days > num ? popupNoticeRecord : (PopupNoticeRecord) null;
  }

  private static PopupNoticeRecord GetPopupNotice(
    ePopupNoticeType popupNotificationType,
    long customerID,
    long accountID)
  {
    PopupNoticeRecord popupNotice = PopupNoticeRecord.LoadPopupNoticeRecordByCustomerIDAccountIDAndType(customerID, accountID, popupNotificationType);
    MonnitSession.PopupNoticeRecords.Add(popupNotice);
    return popupNotice;
  }

  public static List<PopupNoticeRecord> PopupNoticeRecords
  {
    get
    {
      if (MonnitSession.Session[nameof (PopupNoticeRecords)] == null)
        MonnitSession.Session[nameof (PopupNoticeRecords)] = (object) new List<PopupNoticeRecord>();
      return MonnitSession.Session[nameof (PopupNoticeRecords)] as List<PopupNoticeRecord>;
    }
    set => MonnitSession.Session[nameof (PopupNoticeRecords)] = (object) value;
  }

  public static CustomerFavorite LoadFavoriteSensor(long sensorID)
  {
    return CustomerFavorite.LoadFavoriteSensor(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID, sensorID);
  }

  public static CustomerFavorite LoadFavoriteGateway(long gatewayID)
  {
    return CustomerFavorite.LoadFavoriteGateway(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID, gatewayID);
  }

  public static CustomerFavorite LoadFavoriteMap(long mapID)
  {
    return CustomerFavorite.LoadFavoriteMap(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID, mapID);
  }

  public static CustomerFavorite LoadFavoriteNotification(long notificationID)
  {
    return CustomerFavorite.LoadFavoriteNotification(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID, notificationID);
  }

  public static CustomerFavorite LoadFavoriteReport(long reportID)
  {
    return CustomerFavorite.LoadFavoriteReport(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID, reportID);
  }

  public static CustomerFavorite LoadFavoriteLocation(long locationID)
  {
    return CustomerFavorite.LoadFavoriteLocation(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID, locationID);
  }

  public static bool IsSensorFavorite(long sensorID)
  {
    return MonnitSession.CurrentCustomerFavorites.SensorIDs.Contains(sensorID);
  }

  public static bool IsGatewayFavorite(long gatewayID)
  {
    return MonnitSession.CurrentCustomerFavorites.GatewayIDs.Contains(gatewayID);
  }

  public static bool IsVisualMapFavorite(long visualMapID)
  {
    return MonnitSession.CurrentCustomerFavorites.VisualMaps.Find((Predicate<CustomerFavoriteModel>) (x => x.CustomerFavorite.VisualMapID == visualMapID)) != null;
  }

  public static bool IsNotificationFavorite(long notificationID)
  {
    return MonnitSession.CurrentCustomerFavorites.Rules.Find((Predicate<CustomerFavoriteModel>) (x => x.CustomerFavorite.NotificationID == notificationID)) != null;
  }

  public static bool IsReportFavorite(long reportScheduleID)
  {
    return MonnitSession.CurrentCustomerFavorites.ReportSchedules.Find((Predicate<CustomerFavoriteModel>) (x => x.CustomerFavorite.ReportScheduleID == reportScheduleID)) != null;
  }

  public static bool IsLocationFavorite(long locationID)
  {
    return MonnitSession.CurrentCustomerFavorites.Locations.Find((Predicate<CustomerFavoriteModel>) (x => x.CustomerFavorite.LocationID == locationID)) != null;
  }

  public static string CurrentStyle(string property)
  {
    try
    {
      if (MonnitSession.CurrentStyles.ContainsKey(property))
        return MonnitSession.CurrentStyles[property].Value;
    }
    catch (Exception ex)
    {
      ex.Log("MonnitSession.CurrentStyle | property = " + property);
    }
    return "";
  }

  public static byte[] CurrentBinaryStyle(string property)
  {
    try
    {
      if (MonnitSession.CurrentStyles.ContainsKey(property))
        return MonnitSession.CurrentStyles[property].BinaryValue;
    }
    catch (Exception ex)
    {
      ex.Log("MonnitSession.CurrentBinaryStyle | property = " + property);
    }
    return (byte[]) null;
  }

  public static eProgramLevel ProgramLevel()
  {
    if (MonnitSession._ProgramLevel != 0)
      return MonnitSession._ProgramLevel;
    MonnitSession._ProgramLevel = eProgramLevel.NKR;
    string path = $"{HostingEnvironment.ApplicationPhysicalPath}{Convert.ToBase64String(Encoding.UTF8.GetBytes(Environment.MachineName)).TrimEnd('=')}.ky";
    if (File.Exists(path))
    {
      string[] strArray = Encoding.UTF8.GetString(Convert.FromBase64String(File.ReadAllText(path))).Split('|');
      eProgramLevel result;
      if (strArray.Length >= 2 && strArray[1] == Environment.MachineName && Enum.TryParse<eProgramLevel>(strArray[0], out result))
        MonnitSession._ProgramLevel = result;
    }
    else
      MonnitSession._ProgramLevel = eProgramLevel.NKR;
    return MonnitSession._ProgramLevel;
  }

  public static DateTime EnterpriseKillDate()
  {
    if (MonnitSession._EnterpriseKillDate != new DateTime(2099, 1, 1))
      return MonnitSession._EnterpriseKillDate;
    string path = $"{HostingEnvironment.ApplicationPhysicalPath}{Convert.ToBase64String(Encoding.UTF8.GetBytes(Environment.MachineName)).TrimEnd('=')}.ky";
    if (File.Exists(path))
    {
      string[] strArray = Encoding.UTF8.GetString(Convert.FromBase64String(File.ReadAllText(path))).Split('|');
      eProgramLevel result;
      if (strArray.Length >= 3 && strArray[1] == Environment.MachineName && Enum.TryParse<eProgramLevel>(strArray[0], out result))
      {
        DateTime dateTime = Convert.ToDateTime(strArray[2]);
        switch (result)
        {
          case eProgramLevel.EnterpriseTrial:
            MonnitSession._EnterpriseKillDate = dateTime.AddDays(60.0);
            break;
          case eProgramLevel.EnterpriseLite:
            MonnitSession._EnterpriseKillDate = dateTime.AddDays(380.0);
            break;
          case eProgramLevel.EnterpriseBasic:
            MonnitSession._EnterpriseKillDate = dateTime.AddYears(99);
            break;
          case eProgramLevel.Enterprise_1000:
            MonnitSession._EnterpriseKillDate = dateTime.AddYears(99);
            break;
          case eProgramLevel.EnterpriseAdvanced:
            MonnitSession._EnterpriseKillDate = dateTime.AddYears(99);
            break;
          case eProgramLevel.Enterprise_5000:
            MonnitSession._EnterpriseKillDate = dateTime.AddYears(99);
            break;
          case eProgramLevel.EnterpriseUnlimited:
            MonnitSession._EnterpriseKillDate = dateTime.AddYears(99);
            break;
          default:
            MonnitSession._EnterpriseKillDate = DateTime.UtcNow;
            break;
        }
      }
    }
    if (MonnitSession._EnterpriseKillDate == new DateTime(2099, 1, 1))
      MonnitSession._EnterpriseKillDate = DateTime.UtcNow;
    return MonnitSession._EnterpriseKillDate;
  }

  public static StoreAccountInfoModel CurrentStoreLinkInfo
  {
    get
    {
      int num;
      if (MonnitSession.CurrentCustomer != null)
      {
        Guid storeLinkGuid = MonnitSession.CurrentCustomer.Account.StoreLinkGuid;
        num = MonnitSession.CurrentCustomer.Account.StoreLinkGuid == Guid.Empty ? 1 : 0;
      }
      else
        num = 1;
      if (num != 0)
        return (StoreAccountInfoModel) null;
      if (MonnitSession.Session[nameof (CurrentStoreLinkInfo)] == null || (MonnitSession.Session[nameof (CurrentStoreLinkInfo)] as StoreAccountInfoModel).AccountID != MonnitSession.CurrentCustomer.AccountID)
        MonnitSession.Session[nameof (CurrentStoreLinkInfo)] = (object) RetailController.RetrieveStoreAccountInfo(MonnitSession.CurrentCustomer.Account);
      return MonnitSession.Session[nameof (CurrentStoreLinkInfo)] as StoreAccountInfoModel;
    }
    set => MonnitSession.Session[nameof (CurrentStoreLinkInfo)] = (object) value;
  }

  public static bool HasOTASuiteGateways(long accountID)
  {
    return MonnitSession.CachedOTASuiteGateways(accountID).Count > 0;
  }

  public static Dictionary<long, Gateway> CachedOTASuiteGateways(long accountID)
  {
    if (accountID < 0L)
      return new Dictionary<long, Gateway>();
    string CacheKey = "OTASuiteGateways_" + accountID.ToString();
    Dictionary<long, Gateway> OTASuiteGateways = TimedCache.RetrieveObject<Dictionary<long, Gateway>>(CacheKey);
    if (OTASuiteGateways != null)
      return OTASuiteGateways;
    new Thread((ThreadStart) (() =>
    {
      if (!Monitor.TryEnter(MonnitSession.lockOTACheckGateways))
        return;
      try
      {
        OTASuiteGateways = NetworkController.OTASuiteGateways(accountID);
        TimedCache.AddObjectToCach(CacheKey, (object) OTASuiteGateways, new TimeSpan(24, 0, 0));
      }
      catch (Exception ex)
      {
        ex.Log($"MonnitSession.HasOTASuiteGateways | accountID = {accountID}");
        TimedCache.AddObjectToCach(CacheKey, (object) new Dictionary<long, Gateway>());
      }
      finally
      {
        Monitor.Exit(MonnitSession.lockOTACheckGateways);
      }
    }))
    {
      CurrentCulture = CultureInfo.GetCultureInfo("en-US")
    }.Start();
    return new Dictionary<long, Gateway>();
  }

  public static void InvalidateCachedOTASuiteGateways(long accountID)
  {
    TimedCache.RemoveObject("OTASuiteGateways_" + accountID.ToString());
  }

  public static bool HasOTASuiteSensors(long accountID)
  {
    return MonnitSession.CachedOTASuiteSensors(accountID).Count > 0;
  }

  public static void InvalidateCachedOTASuiteSensors(long accountID)
  {
    TimedCache.RemoveObject("OTASuiteSensors_" + accountID.ToString());
  }

  public static List<Sensor> CachedOTASuiteSensors(long accountID)
  {
    if (accountID < 0L)
      return new List<Sensor>();
    bool AdvancedSupport = MonnitSession.CustomerCan("Support_Advanced");
    string CacheKey = $"OTASuiteSensors_{accountID.ToString()}_{AdvancedSupport.ToString()}";
    List<Sensor> OTASuiteSensors = TimedCache.RetrieveObject<List<Sensor>>(CacheKey);
    if (OTASuiteSensors != null)
      return OTASuiteSensors;
    new Thread((ThreadStart) (() =>
    {
      if (!Monitor.TryEnter(MonnitSession.lockOTACheckSensors))
        return;
      try
      {
        Dictionary<string, string> LatestVersions = new Dictionary<string, string>();
        OTASuiteSensors = NetworkController.UpdateableSensors(accountID, AdvancedSupport, ref LatestVersions);
        TimedCache.AddObjectToCach(CacheKey, (object) OTASuiteSensors, new TimeSpan(24, 0, 0));
      }
      catch (Exception ex)
      {
        ex.Log($"MonnitSession.OTASuiteSensors | accountID = {accountID}");
        TimedCache.AddObjectToCach(CacheKey, (object) new List<Sensor>());
      }
      finally
      {
        Monitor.Exit(MonnitSession.lockOTACheckSensors);
      }
    }))
    {
      CurrentCulture = CultureInfo.GetCultureInfo("en-US")
    }.Start();
    return new List<Sensor>();
  }

  public static bool HasUpdateableGateways(long accountID)
  {
    return MonnitSession.CachedUpdateableGateways(accountID).Count > 0;
  }

  public static List<Gateway> CachedUpdateableGateways(long accountID)
  {
    if (accountID < 0L)
      return new List<Gateway>();
    string CacheKey = "UpdateableGateways_" + accountID.ToString();
    List<Gateway> UpdateableGateways = TimedCache.RetrieveObject<List<Gateway>>(CacheKey);
    if (UpdateableGateways != null)
      return UpdateableGateways;
    new Thread((ThreadStart) (() =>
    {
      if (!Monitor.TryEnter(MonnitSession.lockUpdateableCheckGateways))
        return;
      try
      {
        UpdateableGateways = NetworkController.UpdateableGateways(accountID);
        TimedCache.AddObjectToCach(CacheKey, (object) UpdateableGateways, new TimeSpan(24, 0, 0));
      }
      catch (Exception ex)
      {
        ex.Log($"MonnitSession.HasUpdateableGateways | accountID = {accountID}");
        TimedCache.AddObjectToCach(CacheKey, (object) new List<Gateway>());
      }
      finally
      {
        Monitor.Exit(MonnitSession.lockUpdateableCheckGateways);
      }
    }))
    {
      CurrentCulture = CultureInfo.GetCultureInfo("en-US")
    }.Start();
    return new List<Gateway>();
  }

  public static void InvalidateCachedUpdateableGateways(long accountID)
  {
    TimedCache.RemoveObject("UpdateableGateways_" + accountID.ToString());
  }

  public static Version LatestVersion(string SKU, bool isGatewayRadio)
  {
    string version = !MonnitSession.IsEnterprise ? MonnitUtil.GetLatestFirmwareVersionFromMEA(SKU, !isGatewayRadio) : MonnitUtil.GetLatestEncryptedFirmwareVersion(SKU, !isGatewayRadio);
    if (string.IsNullOrEmpty(version) || version.Contains("Failed"))
      return (Version) null;
    try
    {
      return new Version(version);
    }
    catch
    {
      return (Version) null;
    }
  }

  public static class SensorListFilters
  {
    public static void Clear()
    {
      MonnitSession.Session["CurrentCSNet"] = (object) null;
      MonnitSession.Session["CurrentAppID"] = (object) null;
      MonnitSession.Session["CurrentStatus"] = (object) null;
      MonnitSession.Session["CurrentName"] = (object) null;
      MonnitSession.Session["CurrentCustom"] = (object) null;
    }

    public static long CSNetID
    {
      get
      {
        if (MonnitSession.Session["CurrentCSNet"] == null)
          MonnitSession.Session["CurrentCSNet"] = (object) long.MinValue;
        return MonnitSession.Session["CurrentCSNet"].ToLong();
      }
      set => MonnitSession.Session["CurrentCSNet"] = (object) value;
    }

    public static long MonnitApplicationID
    {
      get
      {
        if (MonnitSession.Session["CurrentAppID"] == null)
          MonnitSession.Session["CurrentAppID"] = (object) long.MinValue;
        return MonnitSession.Session["CurrentAppID"].ToLong();
      }
      set => MonnitSession.Session["CurrentAppID"] = (object) value;
    }

    public static long ApplicationID
    {
      get => MonnitSession.SensorListFilters.MonnitApplicationID;
      set => MonnitSession.SensorListFilters.MonnitApplicationID = value;
    }

    public static int Status
    {
      get
      {
        if (MonnitSession.Session["CurrentStatus"] == null)
          MonnitSession.Session["CurrentStatus"] = (object) int.MinValue;
        return MonnitSession.Session["CurrentStatus"].ToInt();
      }
      set => MonnitSession.Session["CurrentStatus"] = (object) value;
    }

    public static string Name
    {
      get
      {
        if (MonnitSession.Session["CurrentName"] == null)
          MonnitSession.Session["CurrentName"] = (object) "";
        return MonnitSession.Session["CurrentName"].ToString().Trim();
      }
      set => MonnitSession.Session["CurrentName"] = (object) value;
    }

    public static string Custom
    {
      get
      {
        if (MonnitSession.Session["CurrentCustom"] == null)
          MonnitSession.Session["CurrentCustom"] = (object) "";
        return MonnitSession.Session["CurrentCustom"].ToString().Trim();
      }
      set => MonnitSession.Session["CurrentCustom"] = (object) value;
    }
  }

  public static class SensorListSort
  {
    public static long SensorID
    {
      get
      {
        if (MonnitSession.Session["Sensor"] == null)
          MonnitSession.Session["Sensor"] = (object) long.MinValue;
        return (long) MonnitSession.Session["Sensor"].ToInt();
      }
      set => MonnitSession.Session["Sensor"] = (object) value;
    }

    public static int Status
    {
      get
      {
        if (MonnitSession.Session["CurrentStatus"] == null)
          MonnitSession.Session["CurrentStatus"] = (object) int.MinValue;
        return MonnitSession.Session["CurrentStatus"].ToInt();
      }
      set => MonnitSession.Session["CurrentStatus"] = (object) value;
    }

    public static long CSNetID
    {
      get
      {
        if (MonnitSession.Session["CSNet"] == null)
          MonnitSession.Session["CSNet"] = (object) long.MinValue;
        return MonnitSession.Session["CSNet"].ToLong();
      }
      set => MonnitSession.Session["CSNet"] = (object) value;
    }

    public static long MonnitApplicationID
    {
      get
      {
        if (MonnitSession.Session["AppID"] == null)
          MonnitSession.Session["AppID"] = (object) long.MinValue;
        return MonnitSession.Session["AppID"].ToLong();
      }
      set => MonnitSession.Session["AppID"] = (object) value;
    }

    public static int SignalStrength
    {
      get
      {
        if (MonnitSession.Session[nameof (SignalStrength)] == null)
          MonnitSession.Session[nameof (SignalStrength)] = (object) int.MinValue;
        return MonnitSession.Session[nameof (SignalStrength)].ToInt();
      }
      set => MonnitSession.Session[nameof (SignalStrength)] = (object) value;
    }

    public static int Battery
    {
      get
      {
        if (MonnitSession.Session[nameof (Battery)] == null)
          MonnitSession.Session[nameof (Battery)] = (object) int.MinValue;
        return MonnitSession.Session[nameof (Battery)].ToInt();
      }
      set => MonnitSession.Session[nameof (Battery)] = (object) value;
    }

    public static string Direction
    {
      get
      {
        if (MonnitSession.Session[nameof (Direction)] == null)
          MonnitSession.Session[nameof (Direction)] = (object) " ";
        return MonnitSession.Session[nameof (Direction)].ToString().Trim();
      }
      set => MonnitSession.Session[nameof (Direction)] = (object) value;
    }

    public static string Name
    {
      get
      {
        if (MonnitSession.Session[nameof (Name)] == null)
          MonnitSession.Session[nameof (Name)] = (object) "";
        return MonnitSession.Session[nameof (Name)].ToString().Trim();
      }
      set => MonnitSession.Session[nameof (Name)] = (object) value;
    }

    public static int Class
    {
      get
      {
        if (MonnitSession.Session[nameof (Class)] == null)
          MonnitSession.Session[nameof (Class)] = (object) int.MinValue;
        return MonnitSession.Session[nameof (Class)].ToInt();
      }
      set => MonnitSession.Session[nameof (Class)] = (object) value;
    }

    public static DateTime LastCheckIn
    {
      get
      {
        if (MonnitSession.Session[nameof (LastCheckIn)] == null)
          MonnitSession.Session[nameof (LastCheckIn)] = (object) DateTime.MinValue;
        return MonnitSession.Session[nameof (LastCheckIn)].ToDateTime();
      }
      set => MonnitSession.Session[nameof (LastCheckIn)] = (object) value;
    }

    public static string Data
    {
      get
      {
        if (MonnitSession.Session[nameof (Data)] == null)
          MonnitSession.Session[nameof (Data)] = (object) "";
        return MonnitSession.Session[nameof (Data)].ToString().Trim();
      }
      set => MonnitSession.Session[nameof (Data)] = (object) value;
    }
  }

  public static class GatewayListFilters
  {
    public static void Clear()
    {
      MonnitSession.Session["GatewayListFilter_CSNetID"] = (object) null;
      MonnitSession.Session["CurrentGatewayTypeID"] = (object) null;
      MonnitSession.Session["CurrentGatewayStatus"] = (object) null;
      MonnitSession.Session["CurrentGatewayName"] = (object) null;
    }

    public static long CSNetID
    {
      get
      {
        if (MonnitSession.Session["GatewayListFilter_CSNetID"] == null)
          MonnitSession.Session["GatewayListFilter_CSNetID"] = (object) long.MinValue;
        return MonnitSession.Session["GatewayListFilter_CSNetID"].ToLong();
      }
      set => MonnitSession.Session["GatewayListFilter_CSNetID"] = (object) value;
    }

    public static long SensorListFiltersCSNetID
    {
      get => MonnitSession.SensorListFilters.CSNetID;
      set => MonnitSession.SensorListFilters.CSNetID = value;
    }

    public static long GatewayTypeID
    {
      get
      {
        if (MonnitSession.Session["CurrentGatewayTypeID"] == null)
          MonnitSession.Session["CurrentGatewayTypeID"] = (object) long.MinValue;
        return MonnitSession.Session["CurrentGatewayTypeID"].ToLong();
      }
      set => MonnitSession.Session["CurrentGatewayTypeID"] = (object) value;
    }

    public static int Status
    {
      get
      {
        if (MonnitSession.Session["CurrentGatewayStatus"] == null)
          MonnitSession.Session["CurrentGatewayStatus"] = (object) int.MinValue;
        return MonnitSession.Session["CurrentGatewayStatus"].ToInt();
      }
      set => MonnitSession.Session["CurrentGatewayStatus"] = (object) value;
    }

    public static string Name
    {
      get
      {
        if (MonnitSession.Session["CurrentGatewayName"] == null)
          MonnitSession.Session["CurrentGatewayName"] = (object) "";
        return MonnitSession.Session["CurrentGatewayName"].ToString().Trim();
      }
      set => MonnitSession.Session["CurrentGatewayName"] = (object) value;
    }
  }

  public static class GatewayListSort
  {
    public static DateTime LastCheckIn
    {
      get
      {
        if (MonnitSession.Session[nameof (LastCheckIn)] == null)
          MonnitSession.Session[nameof (LastCheckIn)] = (object) DateTime.MinValue;
        return MonnitSession.Session[nameof (LastCheckIn)].ToDateTime();
      }
      set => MonnitSession.Session[nameof (LastCheckIn)] = (object) value;
    }

    public static int Power
    {
      get
      {
        if (MonnitSession.Session[nameof (Power)] == null)
          MonnitSession.Session[nameof (Power)] = (object) int.MinValue;
        return MonnitSession.Session[nameof (Power)].ToInt();
      }
      set => MonnitSession.Session[nameof (Power)] = (object) value;
    }

    public static long GatewayID
    {
      get
      {
        if (MonnitSession.Session["Gateway"] == null)
          MonnitSession.Session["Gateway"] = (object) long.MinValue;
        return MonnitSession.Session["Gateway"].ToLong();
      }
      set => MonnitSession.Session["Gateway"] = (object) value;
    }

    public static long CSNetID
    {
      get
      {
        if (MonnitSession.Session["CSNet"] == null)
          MonnitSession.Session["CSNet"] = (object) long.MinValue;
        return MonnitSession.Session["CSNet"].ToLong();
      }
      set => MonnitSession.Session["CSNet"] = (object) value;
    }

    public static string Direction
    {
      get
      {
        if (MonnitSession.Session[nameof (Direction)] == null)
          MonnitSession.Session[nameof (Direction)] = (object) " ";
        return MonnitSession.Session[nameof (Direction)].ToString().Trim();
      }
      set => MonnitSession.Session[nameof (Direction)] = (object) value;
    }

    public static string Name
    {
      get
      {
        if (MonnitSession.Session[nameof (Name)] == null)
          MonnitSession.Session[nameof (Name)] = (object) "";
        return MonnitSession.Session[nameof (Name)].ToString().Trim();
      }
      set => MonnitSession.Session[nameof (Name)] = (object) value;
    }

    public static bool IsEnterprise
    {
      get
      {
        if (MonnitSession.Session[nameof (IsEnterprise)] == null)
          MonnitSession.Session[nameof (IsEnterprise)] = (object) false;
        return MonnitSession.Session[nameof (IsEnterprise)].ToBool();
      }
      set => MonnitSession.Session[nameof (IsEnterprise)] = (object) value;
    }

    public static int Signal
    {
      get
      {
        if (MonnitSession.Session[nameof (Signal)] == null)
          MonnitSession.Session[nameof (Signal)] = (object) int.MinValue;
        return MonnitSession.Session[nameof (Signal)].ToInt();
      }
      set => MonnitSession.Session[nameof (Signal)] = (object) value;
    }
  }

  public static class NotificationListFilters
  {
    public static long MonnitApplicationID
    {
      get
      {
        if (MonnitSession.Session["CurrentAppID"] == null)
          MonnitSession.Session["CurrentAppID"] = (object) long.MinValue;
        return MonnitSession.Session["CurrentAppID"].ToLong();
      }
      set => MonnitSession.Session["CurrentAppID"] = (object) value;
    }

    public static long ApplicationID
    {
      get => MonnitSession.NotificationListFilters.MonnitApplicationID;
      set => MonnitSession.NotificationListFilters.MonnitApplicationID = value;
    }

    public static int Status
    {
      get
      {
        if (MonnitSession.Session["CurrentStatus"] == null)
          MonnitSession.Session["CurrentStatus"] = (object) int.MinValue;
        return MonnitSession.Session["CurrentStatus"].ToInt();
      }
      set => MonnitSession.Session["CurrentStatus"] = (object) value;
    }

    public static string Name
    {
      get
      {
        if (MonnitSession.Session["CurrentName"] == null)
          MonnitSession.Session["CurrentName"] = (object) "";
        return MonnitSession.Session["CurrentName"].ToString().Trim();
      }
      set => MonnitSession.Session["CurrentName"] = (object) value;
    }

    public static int Class
    {
      get
      {
        if (MonnitSession.Session["CurrentClass"] == null)
          MonnitSession.Session["CurrentClass"] = (object) int.MinValue;
        return MonnitSession.Session["CurrentClass"].ToInt();
      }
      set => MonnitSession.Session["CurrentClass"] = (object) value;
    }
  }

  public static class NotificationListSort
  {
    public static string Direction
    {
      get
      {
        if (MonnitSession.Session[nameof (Direction)] == null)
          MonnitSession.Session[nameof (Direction)] = (object) " ";
        return MonnitSession.Session[nameof (Direction)].ToString().Trim();
      }
      set => MonnitSession.Session[nameof (Direction)] = (object) value;
    }

    public static string Column
    {
      get
      {
        if (MonnitSession.Session["Name"] == null)
          MonnitSession.Session["Name"] = (object) "";
        return MonnitSession.Session["Name"].ToString().Trim();
      }
      set => MonnitSession.Session["Name"] = (object) value;
    }
  }

  public static class ReportListFilters
  {
    public static long MonnitApplicationID
    {
      get
      {
        if (MonnitSession.Session["CurrentAppID"] == null)
          MonnitSession.Session["CurrentAppID"] = (object) long.MinValue;
        return MonnitSession.Session["CurrentAppID"].ToLong();
      }
      set => MonnitSession.Session["CurrentAppID"] = (object) value;
    }

    public static long ApplicationID
    {
      get => MonnitSession.ReportListFilters.MonnitApplicationID;
      set => MonnitSession.ReportListFilters.MonnitApplicationID = value;
    }

    public static int Status
    {
      get
      {
        if (MonnitSession.Session["CurrentStatus"] == null)
          MonnitSession.Session["CurrentStatus"] = (object) int.MinValue;
        return MonnitSession.Session["CurrentStatus"].ToInt();
      }
      set => MonnitSession.Session["CurrentStatus"] = (object) value;
    }

    public static string Name
    {
      get
      {
        if (MonnitSession.Session["CurrentName"] == null)
          MonnitSession.Session["CurrentName"] = (object) "";
        return MonnitSession.Session["CurrentName"].ToString().Trim();
      }
      set => MonnitSession.Session["CurrentName"] = (object) value;
    }

    public static int Class
    {
      get
      {
        if (MonnitSession.Session["CurrentClass"] == null)
          MonnitSession.Session["CurrentClass"] = (object) int.MinValue;
        return MonnitSession.Session["CurrentClass"].ToInt();
      }
      set => MonnitSession.Session["CurrentClass"] = (object) value;
    }
  }

  public static class ReportListSort
  {
    public static string Direction
    {
      get
      {
        if (MonnitSession.Session[nameof (Direction)] == null)
          MonnitSession.Session[nameof (Direction)] = (object) " ";
        return MonnitSession.Session[nameof (Direction)].ToString().Trim();
      }
      set => MonnitSession.Session[nameof (Direction)] = (object) value;
    }

    public static string Column
    {
      get
      {
        if (MonnitSession.Session["Name"] == null)
          MonnitSession.Session["Name"] = (object) "";
        return MonnitSession.Session["Name"].ToString().Trim();
      }
      set => MonnitSession.Session["Name"] = (object) value;
    }
  }

  public static class CurrentCustomerFavorites
  {
    public static void Invalidate()
    {
      MonnitSession.CurrentCustomerFavorites.AllFavorites = (List<CustomerFavorite>) null;
      MonnitSession.CurrentCustomerFavorites.SensorIDs = (List<long>) null;
      MonnitSession.CurrentCustomerFavorites.GatewayIDs = (List<long>) null;
      MonnitSession.CurrentCustomerFavorites.VisualMaps = (List<CustomerFavoriteModel>) null;
      MonnitSession.CurrentCustomerFavorites.Rules = (List<CustomerFavoriteModel>) null;
      MonnitSession.CurrentCustomerFavorites.ReportSchedules = (List<CustomerFavoriteModel>) null;
      MonnitSession.CurrentCustomerFavorites.Locations = (List<CustomerFavoriteModel>) null;
    }

    public static List<CustomerFavorite> AllFavorites
    {
      get
      {
        if (MonnitSession.Session["CurrentCustomerFavorites_AllFavorites"] == null)
          MonnitSession.Session["CurrentCustomerFavorites_AllFavorites"] = (object) new Dictionary<long, List<CustomerFavorite>>();
        Dictionary<long, List<CustomerFavorite>> dictionary = MonnitSession.Session["CurrentCustomerFavorites_AllFavorites"] as Dictionary<long, List<CustomerFavorite>>;
        if (!dictionary.ContainsKey(MonnitSession.CurrentCustomer.AccountID))
          dictionary[MonnitSession.CurrentCustomer.AccountID] = CustomerFavorite.LoadByCustomerIDAndAccountID(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID);
        return dictionary[MonnitSession.CurrentCustomer.AccountID];
      }
      private set
      {
        MonnitSession.Session["CurrentCustomerFavorites_AllFavorites"] = (object) value;
      }
    }

    public static List<CustomerFavorite> AllFavoritesForCurrentCustomerAccountID()
    {
      long accountID = MonnitSession.CurrentCustomer.AccountID;
      return MonnitSession.CurrentCustomerFavorites.AllFavorites.Where<CustomerFavorite>((Func<CustomerFavorite, bool>) (x => x.AccountID == accountID)).ToList<CustomerFavorite>();
    }

    public static List<long> SensorIDs
    {
      get
      {
        if (MonnitSession.Session["CurrentCustomerFavorites_SensorIDs"] == null)
          MonnitSession.Session["CurrentCustomerFavorites_SensorIDs"] = (object) new Dictionary<long, List<long>>();
        Dictionary<long, List<long>> dictionary = MonnitSession.Session["CurrentCustomerFavorites_SensorIDs"] as Dictionary<long, List<long>>;
        if (!dictionary.ContainsKey(MonnitSession.CurrentCustomer.AccountID))
        {
          List<long> longList = new List<long>();
          foreach (CustomerFavoriteModel sensor in MonnitSession.CurrentCustomerFavorites.Sensors)
            longList.Add(sensor.Sensor.SensorID);
          dictionary[MonnitSession.CurrentCustomer.AccountID] = longList;
        }
        return dictionary[MonnitSession.CurrentCustomer.AccountID];
      }
      private set => MonnitSession.Session["CurrentCustomerFavorites_SensorIDs"] = (object) value;
    }

    public static List<CustomerFavoriteModel> Sensors
    {
      get
      {
        return CustomerFavoriteModel.LoadSensors(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID);
      }
    }

    public static List<long> GatewayIDs
    {
      get
      {
        if (MonnitSession.Session["CurrentCustomerFavorites_GatewayIDs"] == null)
          MonnitSession.Session["CurrentCustomerFavorites_GatewayIDs"] = (object) new Dictionary<long, List<long>>();
        Dictionary<long, List<long>> dictionary = MonnitSession.Session["CurrentCustomerFavorites_GatewayIDs"] as Dictionary<long, List<long>>;
        if (!dictionary.ContainsKey(MonnitSession.CurrentCustomer.AccountID))
        {
          List<long> longList = new List<long>();
          foreach (CustomerFavoriteModel gateway in MonnitSession.CurrentCustomerFavorites.Gateways)
            longList.Add(gateway.Gateway.GatewayID);
          dictionary[MonnitSession.CurrentCustomer.AccountID] = longList;
        }
        return dictionary[MonnitSession.CurrentCustomer.AccountID];
      }
      private set => MonnitSession.Session["CurrentCustomerFavorites_GatewayIDs"] = (object) value;
    }

    public static List<CustomerFavoriteModel> Gateways
    {
      get
      {
        return CustomerFavoriteModel.LoadGateways(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID);
      }
    }

    public static List<CustomerFavoriteModel> VisualMaps
    {
      get
      {
        if (MonnitSession.Session["CurrentCustomerFavorites_VisualMaps"] == null)
          MonnitSession.Session["CurrentCustomerFavorites_VisualMaps"] = (object) new Dictionary<long, List<CustomerFavoriteModel>>();
        Dictionary<long, List<CustomerFavoriteModel>> dictionary = MonnitSession.Session["CurrentCustomerFavorites_VisualMaps"] as Dictionary<long, List<CustomerFavoriteModel>>;
        if (!dictionary.ContainsKey(MonnitSession.CurrentCustomer.AccountID))
          dictionary[MonnitSession.CurrentCustomer.AccountID] = CustomerFavoriteModel.LoadVisualMaps(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID);
        return dictionary[MonnitSession.CurrentCustomer.AccountID];
      }
      private set => MonnitSession.Session["CurrentCustomerFavorites_VisualMaps"] = (object) value;
    }

    public static List<CustomerFavoriteModel> Rules
    {
      get
      {
        if (MonnitSession.Session["CurrentCustomerFavorites_Notifications"] == null)
          MonnitSession.Session["CurrentCustomerFavorites_Notifications"] = (object) new Dictionary<long, List<CustomerFavoriteModel>>();
        Dictionary<long, List<CustomerFavoriteModel>> dictionary = MonnitSession.Session["CurrentCustomerFavorites_Notifications"] as Dictionary<long, List<CustomerFavoriteModel>>;
        if (!dictionary.ContainsKey(MonnitSession.CurrentCustomer.AccountID))
          dictionary[MonnitSession.CurrentCustomer.AccountID] = CustomerFavoriteModel.LoadNotifications(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID);
        return dictionary[MonnitSession.CurrentCustomer.AccountID];
      }
      private set
      {
        MonnitSession.Session["CurrentCustomerFavorites_Notifications"] = (object) value;
      }
    }

    public static List<CustomerFavoriteModel> ReportSchedules
    {
      get
      {
        if (MonnitSession.Session["CurrentCustomerFavorites_ReportSchedules"] == null)
          MonnitSession.Session["CurrentCustomerFavorites_ReportSchedules"] = (object) new Dictionary<long, List<CustomerFavoriteModel>>();
        Dictionary<long, List<CustomerFavoriteModel>> dictionary = MonnitSession.Session["CurrentCustomerFavorites_ReportSchedules"] as Dictionary<long, List<CustomerFavoriteModel>>;
        if (!dictionary.ContainsKey(MonnitSession.CurrentCustomer.AccountID))
          dictionary[MonnitSession.CurrentCustomer.AccountID] = CustomerFavoriteModel.LoadReportSchedules(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID);
        return dictionary[MonnitSession.CurrentCustomer.AccountID];
      }
      private set
      {
        MonnitSession.Session["CurrentCustomerFavorites_ReportSchedules"] = (object) value;
      }
    }

    public static List<CustomerFavoriteModel> Locations
    {
      get
      {
        if (MonnitSession.Session["CurrentCustomerFavorites_Locations"] == null)
          MonnitSession.Session["CurrentCustomerFavorites_Locations"] = (object) new Dictionary<long, List<CustomerFavoriteModel>>();
        Dictionary<long, List<CustomerFavoriteModel>> dictionary = MonnitSession.Session["CurrentCustomerFavorites_Locations"] as Dictionary<long, List<CustomerFavoriteModel>>;
        if (!dictionary.ContainsKey(MonnitSession.CurrentCustomer.AccountID))
          dictionary[MonnitSession.CurrentCustomer.AccountID] = CustomerFavoriteModel.LoadLocations(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID);
        return dictionary[MonnitSession.CurrentCustomer.AccountID];
      }
      private set => MonnitSession.Session["CurrentCustomerFavorites_Locations"] = (object) value;
    }
  }

  public static class TestingToolSession
  {
    public static string DeviceType
    {
      get
      {
        if (MonnitSession.Session["TTDeviceType"] == null)
          MonnitSession.Session["TTDeviceType"] = (object) "sensor";
        return MonnitSession.Session["TTDeviceType"] as string;
      }
      set
      {
        if (!(value == "sensor") && !(value == "gateway"))
          return;
        MonnitSession.Session["TTDeviceType"] = (object) value;
      }
    }

    public static string FontSize
    {
      get
      {
        if (MonnitSession.Session["TTFontSize"] == null)
          MonnitSession.Session["TTFontSize"] = (object) "14";
        return MonnitSession.Session["TTFontSize"] as string;
      }
      set => MonnitSession.Session["TTFontSize"] = (object) value;
    }
  }

  public static class OverviewHomeModel
  {
    public static void Invalidate()
    {
      string key1 = $"Sensors_AccountID_{MonnitSession.CurrentCustomer.AccountID}";
      string key2 = $"TotalSensors_AccountID_{MonnitSession.CurrentCustomer.AccountID}";
      string key3 = $"Gateways_AccountID_{MonnitSession.CurrentCustomer.AccountID}";
      string key4 = $"TotalGateways_AccountID_{MonnitSession.CurrentCustomer.AccountID}";
      string key5 = $"Notifications_AccountID_{MonnitSession.CurrentCustomer.AccountID}";
      string key6 = $"NotificationsTriggered_AccountID_{MonnitSession.CurrentCustomer.AccountID}";
      TimedCache.RemoveObject(key1);
      TimedCache.RemoveObject(key2);
      TimedCache.RemoveObject(key3);
      TimedCache.RemoveObject(key4);
      TimedCache.RemoveObject(key5);
      TimedCache.RemoveObject(key6);
    }

    public static int TotalSensors
    {
      get
      {
        int num = TimedCache.RetrieveObject<int>($"TotalSensors_AccountID_{MonnitSession.CurrentCustomer.AccountID}");
        return num < 0 ? 0 : num;
      }
    }

    public static List<SensorGroupSensorModel> Sensors
    {
      get
      {
        string key1 = $"Sensors_AccountID_{MonnitSession.CurrentCustomer.AccountID}";
        string key2 = $"TotalSensors_AccountID_{MonnitSession.CurrentCustomer.AccountID}";
        List<SensorGroupSensorModel> sensors = TimedCache.RetrieveObject<List<SensorGroupSensorModel>>(key1);
        int totalSensors = TimedCache.RetrieveObject<int>(key2);
        if (sensors == null)
        {
          sensors = SensorControllerBase.GetSensorList(out totalSensors);
          TimedCache.AddObjectToCach(key1, (object) sensors, new TimeSpan(0, 0, 30));
          TimedCache.AddObjectToCach(key2, (object) totalSensors, new TimeSpan(0, 0, 30));
        }
        return sensors;
      }
    }

    public static int TotalGateways
    {
      get
      {
        int num = TimedCache.RetrieveObject<int>($"TotalGateways_AccountID_{MonnitSession.CurrentCustomer.AccountID}");
        return num < 0 ? 0 : num;
      }
    }

    public static List<Gateway> Gateways
    {
      get
      {
        string key1 = $"Gateways_AccountID_{MonnitSession.CurrentCustomer.AccountID}";
        string key2 = $"TotalGateways_AccountID_{MonnitSession.CurrentCustomer.AccountID}";
        List<Gateway> gateways = TimedCache.RetrieveObject<List<Gateway>>(key1);
        int totalGateways = TimedCache.RetrieveObject<int>(key2);
        if (gateways == null)
        {
          gateways = CSNetControllerBase.GetGatewayList(out totalGateways);
          TimedCache.AddObjectToCach(key1, (object) gateways, new TimeSpan(0, 0, 30));
          TimedCache.AddObjectToCach(key2, (object) totalGateways, new TimeSpan(0, 0, 30));
        }
        return gateways;
      }
    }

    public static List<Notification> Notifications
    {
      get
      {
        string key1 = $"Notifications_AccountID_{MonnitSession.CurrentCustomer.AccountID}";
        string key2 = $"NotificationsTriggered_AccountID_{MonnitSession.CurrentCustomer.AccountID}";
        List<Notification> notifications = TimedCache.RetrieveObject<List<Notification>>(key1);
        if (notifications == null)
        {
          Notification.RuleFilterResult ruleFilterResult = Notification.RuleFilterHomePage(MonnitSession.CurrentCustomer.AccountID);
          notifications = ruleFilterResult.Notifications;
          TimedCache.AddObjectToCach(key1, (object) notifications, new TimeSpan(0, 0, 30));
          TimedCache.AddObjectToCach(key2, (object) ruleFilterResult.NotificationsTriggered, new TimeSpan(0, 0, 30));
        }
        return notifications;
      }
    }

    public static List<NotificationTriggered> NotificationsTriggered
    {
      get
      {
        string key1 = $"Notifications_AccountID_{MonnitSession.CurrentCustomer.AccountID}";
        string key2 = $"NotificationsTriggered_AccountID_{MonnitSession.CurrentCustomer.AccountID}";
        List<NotificationTriggered> notificationsTriggered = TimedCache.RetrieveObject<List<NotificationTriggered>>(key2);
        if (notificationsTriggered == null)
        {
          Notification.RuleFilterResult ruleFilterResult = Notification.RuleFilterHomePage(MonnitSession.CurrentCustomer.AccountID);
          notificationsTriggered = ruleFilterResult.NotificationsTriggered;
          TimedCache.AddObjectToCach(key1, (object) ruleFilterResult.Notifications, new TimeSpan(0, 0, 30));
          TimedCache.AddObjectToCach(key2, (object) notificationsTriggered, new TimeSpan(0, 0, 30));
        }
        return notificationsTriggered;
      }
    }
  }
}
