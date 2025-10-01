// Decompiled with JetBrains decompiler
// Type: iMonnit.ControllerBase.OverviewControllerBase
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.Models;
using Microsoft.CSharp.RuntimeBinder;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.ControllerBase;

public class OverviewControllerBase : ThemeController
{
  protected List<AccountThemeReleaseNoteModel> CheckReleaseNotes(Version codeVersion)
  {
    List<AccountThemeReleaseNoteModel> releaseNoteModelList = new List<AccountThemeReleaseNoteModel>();
    if (MonnitSession.CurrentCustomer != null && !MonnitSession.UserIsCustomerProxied && (MonnitSession.CurrentCustomer.LastViewedReleaseNote(MonnitSession.CurrentCustomer.CustomerID) == null || new Version(MonnitSession.LastReleaseNoteViewed.Version) < codeVersion))
    {
      List<ReleaseNote> releaseNoteList = ReleaseNote.LoadActiveReleaseNotesByDateAndVersion(codeVersion, DateTime.Now.AddYears(-1));
      DateTime createDate = MonnitSession.CurrentCustomer.CreateDate;
      foreach (ReleaseNote releaseNote in releaseNoteList)
      {
        if (releaseNote.ReleaseDate >= createDate && !MonnitSession.CurrentCustomer.HasSeenReleaseNote(releaseNote))
        {
          MonnitSession.CurrentCustomer.AcknowledgeReleaseNote(releaseNote.ReleaseNoteID, MonnitSession.CurrentCustomer.CustomerID, false);
          releaseNoteModelList.Add(new AccountThemeReleaseNoteModel(releaseNote, MonnitSession.CurrentTheme.AccountThemeID));
        }
      }
    }
    return releaseNoteModelList;
  }

  public static void ClearRememberMeCookie(HttpRequest request)
  {
    HttpCookie cookie = request.Cookies["Preferences"];
    if (cookie == null)
      return;
    cookie.Expires = DateTime.Now.AddDays(-1.0);
    cookie.HttpOnly = true;
    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
    if (MonnitSession.CurrentCustomer != null)
      MonnitSession.CurrentCustomer.RemovePreferenceCookie(cookie);
  }

  public static void AddNewAccountToOVCookie(HttpRequest request)
  {
    HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["portaltype"] ?? new HttpCookie("portaltype");
    cookie.Expires = DateTime.Now.AddYears(2);
    cookie.Value = "oneview";
    cookie.HttpOnly = true;
    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
  }

  protected void PrimaryContactNotifications()
  {
    if (MonnitSession.CurrentCustomer == null || MonnitSession.CurrentCustomer.Account == null || MonnitSession.CurrentCustomer.Account.PrimaryContactID != MonnitSession.CurrentCustomer.CustomerID || !(this.Request.UrlReferrer != (Uri) null) || !this.Request.UrlReferrer.LocalPath.ToLower().Contains("logon"))
      return;
    List<CSNet> csNetList = CSNet.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
    foreach (CSNet csNet in csNetList)
    {
      if (!csNet.SendNotifications && csNet.AlertNotificationsAreOff)
      {
        this.TempData["networks"] = (object) csNetList;
        this.ViewData["showAlertNotification"] = (object) true;
      }
    }
    DateTime now;
    int num;
    if (MonnitSession.CurrentCustomer.Account.IsPremium)
    {
      DateTime premiumValidUntil = MonnitSession.CurrentCustomer.Account.PremiumValidUntil;
      now = DateTime.Now;
      DateTime dateTime = now.AddDays(30.0);
      if (premiumValidUntil < dateTime)
      {
        num = 1;
        goto label_15;
      }
    }
    if (!MonnitSession.CurrentCustomer.Account.IsPremium)
    {
      DateTime premiumValidUntil = MonnitSession.CurrentCustomer.Account.PremiumValidUntil;
      now = DateTime.Now;
      DateTime dateTime = now.AddDays(-15.0);
      num = premiumValidUntil > dateTime ? 1 : 0;
    }
    else
      num = 0;
label_15:
    if (num != 0)
    {
      this.ViewData["showAlertNotification"] = (object) false;
      this.ViewData["ExpiredNotification"] = (object) true;
    }
  }

  public static string GetTempAuthToken() => PurchaseBase.GetTempAuthToken();

  protected TimeSpan GetStartTime(FormCollection collection)
  {
    try
    {
      int hours = collection["ActiveStartTimeHour"].ToInt();
      if (hours == 12)
        hours = 0;
      if (collection["ActiveStartTimeAM"] == "PM")
        hours += 12;
      int minutes = collection["ActiveStartTimeMinute"].ToInt();
      return new TimeSpan(hours, minutes, 0);
    }
    catch
    {
      return new TimeSpan();
    }
  }

  protected TimeSpan GetEndTime(FormCollection collection)
  {
    try
    {
      int hours = collection["ActiveEndTimeHour"].ToInt();
      if (hours == 12)
        hours = 0;
      if (collection["ActiveEndTimeAM"] == "PM")
        hours += 12;
      int minutes = collection["ActiveEndTimeMinute"].ToInt();
      return new TimeSpan(hours, minutes, 0);
    }
    catch
    {
      return new TimeSpan();
    }
  }

  protected Gateway SaveWIFISettings(Sensor sensor, FormCollection collection)
  {
    Gateway DBObject = Gateway.LoadBySensorID(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["GatewayIP"]) && collection["GatewayIP"] != "0.0.0.0")
    {
      if (collection["GatewaySubnet"] == "" || collection["GatewaySubnet"] == "0.0.0.0")
        this.ModelState.AddModelError("GatewaySubnet", "Subnet Required when not using DHCP");
      if (collection["GatewayDNS"] == "" || collection["GatewayDNS"] == "0.0.0.0")
        this.ModelState.AddModelError("GatewayDNS", "Default DNS Required when not using DHCP");
      if (DBObject.GatewayType.SupportsDefaultRouterIP && (collection["DefaultRouterIP"] == "" || collection["DefaultRouterIP"] == "0.0.0.0"))
        this.ModelState.AddModelError("DefaultRouterIP", "Default Gateway Required when not using DHCP");
      if (!collection["GatewayIP"].IsIPAddress())
        this.ModelState.AddModelError("GatewayIP", "Must be valid IP Address (ie 192.168.0.10)");
      if (!collection["GatewaySubnet"].IsIPAddress())
        this.ModelState.AddModelError("GatewaySubnet", "Must be valid Subnet Mask (ie 255.255.255.0)");
      if (!collection["GatewayDNS"].IsIPAddress())
        this.ModelState.AddModelError("GatewayDNS", "Must be valid IP Address (ie 192.168.0.1)");
      if (DBObject.GatewayType.SupportsDefaultRouterIP && !collection["DefaultRouterIP"].IsIPAddress())
        this.ModelState.AddModelError("DefaultRouterIP", "Must be valid IP Address (ie 192.168.0.1)");
      if (string.IsNullOrWhiteSpace(collection["SSID1"]))
        this.ModelState.AddModelError("SSID1", "Primary WIFI Network Required");
    }
    if (string.IsNullOrWhiteSpace(collection["SSID1"]))
      this.ModelState.AddModelError("SSID1", "Primary WIFI Network Required");
    if (this.ModelState.IsValid)
    {
      Account account = Account.Load(sensor.AccountID);
      AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, sensor.SensorID, eAuditAction.Update, eAuditObject.Sensor, $"{{\"DeviceID\" : \"{sensor.SensorID}\", \"DeviceType\": \"{"Sensor"}\", \"Date\": \"{DateTime.UtcNow}\" }} ", account.AccountID, "Updated gateway Wifi settings");
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Saved Wifi settings");
      try
      {
        DBObject.Name = $"WiFi Gateway({collection["SensorName"]})";
        DBObject.ReportInterval = collection["ReportInterval"].ToDouble();
        if (collection["LedActive"] != null)
          DBObject.LedActiveTime = collection["LedActive"].ToStringSafe().ToLower().Contains("on") ? 1 : 0;
        if (sensor.DataLogChanged)
          DBObject.IsDirty = true;
        int num = 0;
        if (DBObject.GatewayTypeID > long.MinValue)
        {
          GatewayType gatewayType = GatewayType.Load(DBObject.GatewayTypeID);
          if (gatewayType.SupportsHostAddress && !string.IsNullOrEmpty(collection["GatewayServerHostAddress"]))
            DBObject.ServerHostAddress = collection["GatewayServerHostAddress"];
          if (gatewayType.SupportsHostPort && collection["GatewayPort"].ToInt() > int.MinValue)
            DBObject.Port = collection["GatewayPort"].ToInt();
          if (gatewayType.SupportsGatewayIP && !string.IsNullOrEmpty(collection["GatewayIP"]))
            DBObject.GatewayIP = collection["GatewayIP"];
          if (gatewayType.SupportsGatewayIP && !string.IsNullOrEmpty(collection["GatewaySubnet"]))
            DBObject.GatewaySubnet = collection["GatewaySubnet"];
          if (gatewayType.SupportsGatewayIP && !string.IsNullOrEmpty(collection["GatewayDNS"]))
            DBObject.GatewayDNS = collection["GatewayDNS"];
          if (gatewayType.SupportsDefaultRouterIP && !string.IsNullOrEmpty(collection["DefaultRouterIP"]))
            DBObject.DefaultRouterIP = collection["DefaultRouterIP"];
          GatewayWIFICredential gatewayWifiCredential1 = DBObject.WifiCredential(1);
          if (!string.IsNullOrEmpty(collection["SSID1"]))
          {
            num = 1;
            gatewayWifiCredential1.SSID = collection["SSID1"];
            gatewayWifiCredential1.WIFISecurityMode = (eWIFI_SecurityMode) Enum.Parse(typeof (eWIFI_SecurityMode), collection["WIFISecurityMode"]);
            if (!string.IsNullOrEmpty(collection["PassPhrase1"]))
              gatewayWifiCredential1.PassPhrase = MonnitSession.UseEncryption ? collection["PassPhrase1"].Encrypt() : collection["PassPhrase1"];
            if (gatewayWifiCredential1.IsDirty || gatewayWifiCredential1.GatewayWIFICredentialID == long.MinValue)
            {
              DBObject.IsDirty = true;
              gatewayWifiCredential1.IsDirty = false;
              gatewayWifiCredential1.Save();
            }
          }
          else if (gatewayWifiCredential1.GatewayWIFICredentialID > 0L)
          {
            gatewayWifiCredential1.Delete();
            DBObject.IsDirty = true;
          }
          GatewayWIFICredential gatewayWifiCredential2 = DBObject.WifiCredential(2);
          if (!string.IsNullOrWhiteSpace(collection["SSID2"]))
          {
            num = 2;
            gatewayWifiCredential2.SSID = collection["SSID2"];
            gatewayWifiCredential2.WIFISecurityMode = (eWIFI_SecurityMode) Enum.Parse(typeof (eWIFI_SecurityMode), collection["WIFISecurityMode2"]);
            if (!string.IsNullOrEmpty(collection["PassPhrase2"]))
              gatewayWifiCredential2.PassPhrase = MonnitSession.UseEncryption ? collection["PassPhrase2"].Encrypt() : collection["PassPhrase2"];
            if (gatewayWifiCredential2.IsDirty || gatewayWifiCredential2.GatewayWIFICredentialID == long.MinValue)
            {
              DBObject.IsDirty = true;
              gatewayWifiCredential2.IsDirty = false;
              gatewayWifiCredential2.Save();
            }
          }
          else if (gatewayWifiCredential2.GatewayWIFICredentialID > 0L)
          {
            gatewayWifiCredential2.Delete();
            DBObject.IsDirty = true;
          }
          GatewayWIFICredential gatewayWifiCredential3 = DBObject.WifiCredential(3);
          if (num == 2 && !string.IsNullOrWhiteSpace(collection["SSID3"]))
          {
            num = 3;
            gatewayWifiCredential3.SSID = collection["SSID3"];
            gatewayWifiCredential3.WIFISecurityMode = (eWIFI_SecurityMode) Enum.Parse(typeof (eWIFI_SecurityMode), collection["WIFISecurityMode3"]);
            if (!string.IsNullOrEmpty(collection["PassPhrase3"]))
              gatewayWifiCredential3.PassPhrase = MonnitSession.UseEncryption ? collection["PassPhrase3"].Encrypt() : collection["PassPhrase3"];
            if (gatewayWifiCredential3.IsDirty || gatewayWifiCredential3.GatewayWIFICredentialID == long.MinValue)
            {
              DBObject.IsDirty = true;
              gatewayWifiCredential3.IsDirty = false;
              gatewayWifiCredential3.Save();
            }
          }
          else if (gatewayWifiCredential3.GatewayWIFICredentialID > 0L)
          {
            gatewayWifiCredential3.Delete();
            DBObject.IsDirty = true;
          }
          if (gatewayType.SupportsSecondaryDNS)
            DBObject.GatewayDNS = string.IsNullOrEmpty(collection["GatewayDNS"]) ? gatewayType.DefaultGatewayDNS : collection["GatewayDNS"];
          if (gatewayType.SupportsSecondaryDNS)
            DBObject.SecondaryDNS = string.IsNullOrEmpty(collection["SecondaryDNS"]) ? gatewayType.DefaultSecondaryDNS : collection["SecondaryDNS"];
        }
        DBObject.WiFiNetworkCount = num;
        DBObject.Save();
      }
      catch (Exception ex)
      {
        ex.Log($"OVerviewController.SaveWIFISettings sensorID: {sensor.SensorID.ToString()} Message: ");
      }
    }
    return DBObject;
  }

  protected void Unsubscribe(string address, string reason)
  {
    if (UnsubscribedNotificationEmail.EmailIsUnsubscribed(address))
      return;
    new UnsubscribedNotificationEmail()
    {
      EmailAddress = address,
      OptOutDate = DateTime.UtcNow,
      Reason = reason
    }.Save();
    UnsubscribedNotificationEmail.ClearCachedList();
  }

  public bool BoolCorrection(string value)
  {
    return !string.IsNullOrEmpty(value) && value.ToLower() == "on";
  }

  public string SensorEditBase(Sensor sensor, FormCollection collection)
  {
    Gateway gateway1 = (Gateway) null;
    string str1 = "~/Views/Overview/ErrorDisplay.aspx";
    try
    {
      bool flag = sensor != null ? sensor.GeneralConfigDirty : throw new ArgumentNullException("sensor == null");
      bool generalConfig2Dirty = sensor.GeneralConfig2Dirty;
      bool profileConfigDirty = sensor.ProfileConfigDirty;
      bool profileConfig2Dirty = sensor.ProfileConfig2Dirty;
      try
      {
        foreach (string allKey in collection.AllKeys)
        {
          if (allKey.ToLower().Contains("hysteresis_manual") || allKey.ToLower().Contains("minimumthreshold_manual") || allKey.ToLower().Contains("maximumthreshold_manual"))
          {
            if (string.IsNullOrEmpty(collection[allKey]))
              throw new Exception($"Minimum, Maximum, and Hysteresis fields are required. SensorID = {sensor.SensorID}, Key = {allKey}");
            if (!double.TryParse(collection[allKey].ToString(), out double _))
              throw new Exception($"Minimum, Maximum, and Hysteresis fields must be numeric. SensorID = {sensor.SensorID}, Key = {allKey}");
          }
          if (allKey.ToLower().Contains("datumname"))
          {
            int datumindex = !string.IsNullOrEmpty(collection[allKey]) ? Convert.ToInt32(allKey.ToLower().Replace("datumname", "")) : throw new Exception($"Data Names cannot be blank. SensorID = {sensor.SensorID}, Key = {allKey}");
            sensor.SetDatumName(datumindex, collection[allKey].ToStringSafe());
          }
          if (allKey.ToLower().Contains("sensorprint_manual"))
          {
            string str2 = collection[allKey];
            if (str2.Length > 0 && str2.Length != 64 /*0x40*/)
              throw new Exception("Sensor Print must be 32 bytes long");
          }
        }
        if (!string.IsNullOrEmpty(collection["UseRepeater"]))
        {
          if (collection["UseRepeater"].Split(',')[0].ToBool())
          {
            if (sensor.StandardMessageDelay < 100)
            {
              sensor.StandardMessageDelay = 300;
              sensor.MaximumNetworkHops = 2;
            }
          }
          else
          {
            sensor.StandardMessageDelay = sensor.DefaultValue<int>("DefaultStandardMessageDelay");
            sensor.MaximumNetworkHops = sensor.DefaultValue<int>("DefaultMaximumNetworkHops");
          }
        }
        this.UpdateModel<Sensor>(sensor);
        if (!string.IsNullOrEmpty(collection["SensorName"]))
          sensor.SensorName = collection["SensorName"];
        bool sensorPrintDirty = sensor.SensorPrintDirty;
        string s = collection["SensorPrint_Manual"];
        if (!string.IsNullOrEmpty(s))
          sensor.SensorPrint = s.FormatStringToByteArray();
        sensor.SensorPrintDirty = sensorPrintDirty;
        if (new Version(sensor.FirmwareVersion) >= new Version("1.2.009") && sensor.GenerationType.ToUpper().Contains("GEN1"))
        {
          TimeSpan timeSpan = this.GetStartTime(collection);
          TimeSpan startTime = timeSpan.Subtract(MonnitSession.UTCOffset);
          if (startTime.TotalMinutes < 0.0)
            startTime = startTime.Add(new TimeSpan(1, 0, 0, 0));
          if (startTime.Days > 0)
            startTime = startTime.Subtract(new TimeSpan(1, 0, 0, 0));
          timeSpan = this.GetEndTime(collection);
          TimeSpan endTime = timeSpan.Subtract(MonnitSession.UTCOffset);
          if (endTime.TotalMinutes < 0.0)
            endTime = endTime.Add(new TimeSpan(1, 0, 0, 0));
          if (endTime.Days > 0)
            endTime = endTime.Subtract(new TimeSpan(1, 0, 0, 0));
          sensor.SetTimeOfDayActive(startTime, endTime);
        }
        if (sensor.Recovery < 0)
          sensor.Recovery = 0;
        if (sensor.Recovery == 0)
          sensor.RetryCount = 0;
        if (sensor.Recovery > 10 && sensor.Recovery != 101)
          sensor.Recovery = 10;
        double num = MonnitSession.CurrentCustomer.Account.MinHeartBeat();
        if (sensor.ReportInterval < num)
          sensor.ReportInterval = num;
        if (sensor.ActiveStateInterval < num)
          sensor.ActiveStateInterval = num;
        if (sensor.SensorTypeID == 4L || sensor.SensorTypeID == 8L && !string.IsNullOrEmpty(collection["GatewayIP"]))
        {
          sensor.DataLog = collection["DataLog_Manual"].ToStringSafe().ToLower().Contains("on");
          if (collection["WIFISecurityMode"] != null && collection["WIFISecurityMode"].Equals("1") && collection["PassPhrase1"].Length != 10 && collection["PassPhrase1"].Length != 26)
          {
            if (collection["PassPhrase1"].Length == 5 || collection["PassPhrase1"].Length == 13)
            {
              char[] charArray = collection["PassPhrase1"].ToStringSafe().ToCharArray();
              string str3 = "";
              foreach (char ch in charArray)
              {
                int int32 = Convert.ToInt32(ch);
                str3 += $"{int32:X}";
              }
              collection["PassPhrase1"] = str3;
            }
            else
              this.ModelState.AddModelError("PassPhrase1", "Security type is WEP. and the passphrase is incorrect try retyping it. Wep Keys are generally 10 or 26 alphanumeric characters long i.e. (A-F, 0-9), but on occasion they can be a 5 or 13 letter passphrase.");
          }
          if (collection["WIFISecurityMode2"] != null && collection["WIFISecurityMode2"].Equals("1") && collection["PassPhrase2"].Length != 10 && collection["PassPhrase2"].Length != 26)
          {
            if (collection["PassPhrase2"].Length == 5 || collection["PassPhrase2"].Length == 13)
            {
              char[] charArray = collection["PassPhrase2"].ToStringSafe().ToCharArray();
              string str4 = "";
              foreach (char ch in charArray)
              {
                int int32 = Convert.ToInt32(ch);
                str4 += $"{int32:X}";
              }
              collection["PassPhrase2"] = str4;
            }
            else
              this.ModelState.AddModelError("PassPhrase2", "Security type is WEP. and the passphrase is incorrect try retyping it. Wep Keys are generally 10 or 26 alphanumeric characters long i.e. (A-F, 0-9), but on occasion they can be a 5 or 13 letter passphrase.");
          }
          if (collection["WIFISecurityMode3"] != null && collection["WIFISecurityMode3"].Equals("1") && collection["PassPhrase3"].Length != 10 && collection["PassPhrase3"].Length != 26)
          {
            if (collection["PassPhrase3"].Length == 5 || collection["PassPhrase3"].Length == 13)
            {
              char[] charArray = collection["PassPhrase3"].ToStringSafe().ToCharArray();
              string str5 = "";
              foreach (char ch in charArray)
              {
                int int32 = Convert.ToInt32(ch);
                str5 += $"{int32:X}";
              }
              collection["PassPhrase3"] = str5;
            }
            else
              this.ModelState.AddModelError("PassPhrase3", "Security type is WEP. and the passphrase is incorrect try retyping it. Wep Keys are generally 10 or 26 alphanumeric characters long i.e. (A-F, 0-9), but on occasion they can be a 5 or 13 letter passphrase.");
          }
          gateway1 = this.SaveWIFISettings(sensor, collection);
        }
        if (sensor.SensorTypeID == 6L)
        {
          try
          {
            Gateway gateway2 = Gateway.LoadBySensorID(sensor.SensorID);
            gateway2.ReportInterval = sensor.ReportInterval;
            gateway2.NetworkListInterval = this.BoolCorrection(collection["NetworkListInterval"]) ? 1.0 : 0.0;
            if (!string.IsNullOrEmpty(collection["GatewayIP"]))
              gateway2.GatewayIP = collection["GatewayIP"];
            if (!string.IsNullOrEmpty(collection["GatewaySubnet"]))
              gateway2.GatewaySubnet = collection["GatewaySubnet"];
            if (!string.IsNullOrEmpty(collection["GatewayDNS"]))
              gateway2.GatewayDNS = collection["GatewayDNS"];
            if (!string.IsNullOrEmpty(collection["DefaultRouterIP"]))
              gateway2.DefaultRouterIP = collection["DefaultRouterIP"];
            if (gateway2.IsUnlocked)
            {
              if (!string.IsNullOrEmpty(collection["GatewayServerHostAddress"]))
                gateway2.ServerHostAddress = collection["GatewayServerHostAddress"];
              if (collection["GatewayPort"].ToInt() > int.MinValue)
                gateway2.Port = collection["GatewayPort"].ToInt();
            }
            gateway2.Save();
          }
          catch (Exception ex)
          {
            ex.Log($"OverviewControllerBase.SensorEditBase(Sensor sensor, FormCollection collection), (sensor.SensorTypeID == 6) && (Gateway.LoadBySensorID(sensor.SensorID) == null), sensor.SensorID=[{sensor.SensorID}]");
            throw;
          }
        }
        if (sensor.SensorTypeID == 7L)
        {
          try
          {
            Gateway gateway3 = Gateway.LoadBySensorID(sensor.SensorID);
            gateway3.ReportInterval = sensor.ReportInterval;
            if (gateway3.IsUnlocked)
            {
              if (!string.IsNullOrEmpty(collection["GatewayServerHostAddress"]))
                gateway3.ServerHostAddress = collection["GatewayServerHostAddress"];
              if (collection["GatewayPort"].ToInt() > int.MinValue)
                gateway3.Port = collection["GatewayPort"].ToInt();
            }
            gateway3.Save();
          }
          catch (Exception ex)
          {
            throw;
          }
        }
        NameValueCollection returnValues;
        MonnitApplicationBase.SetProfileSettings(sensor, (NameValueCollection) collection, out returnValues);
        foreach (string key in returnValues.Keys)
          this.ViewData[key] = (object) returnValues[key];
        if ((sensor.MinimumThreshold == (long) uint.MaxValue || sensor.MaximumThreshold == (long) uint.MaxValue) && sensor.MeasurementsPerTransmission > 1)
        {
          sensor.SetDefaultCalibration();
          this.ModelState.AddModelError("MeasurementsPerTransmission", "M+");
        }
        if (this.ModelState.IsValid)
        {
          Account account = Account.Load(sensor.AccountID);
          sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited advanced sensor settings");
          sensor.Save();
          if (sensor.IsDirty)
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__10.\u003C\u003Ep__0 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__10.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__10.\u003C\u003Ep__0.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__10.\u003C\u003Ep__0, this.ViewBag, "Sensor Edit Pending");
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__10.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__10.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__10.\u003C\u003Ep__1.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__10.\u003C\u003Ep__1, this.ViewBag, "Sensor Edit Success");
          }
          // ISSUE: reference to a compiler-generated field
          if (OverviewControllerBase.\u003C\u003Eo__10.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OverviewControllerBase.\u003C\u003Eo__10.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = OverviewControllerBase.\u003C\u003Eo__10.\u003C\u003Ep__2.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__10.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/Overview/SensorEdit/" + sensor.SensorID.ToString() : collection["returns"]);
          return "EditConfirmation";
        }
        sensor.GeneralConfigDirty = flag;
        sensor.ProfileConfigDirty = profileConfigDirty;
      }
      catch (Exception ex)
      {
        this.ModelState.AddModelError("", ex.Message);
        ex.Log("OverviewControllerBase.SensorEditBase(Sensor sensor, FormCollection collection)");
      }
      sensor.GeneralConfigDirty = flag;
      sensor.GeneralConfig2Dirty = generalConfig2Dirty;
      sensor.ProfileConfigDirty = profileConfigDirty;
      sensor.ProfileConfig2Dirty = profileConfig2Dirty;
      str1 = $"SensorEdit\\ApplicationCustomization\\app_{sensor.ApplicationID.ToString("D3")}\\Edit";
    }
    catch (Exception ex)
    {
      ex.Log("OverviewControllerBase.SensorEditBase(Sensor sensor, FormCollection collection)");
    }
    return str1;
  }

  public string GatewayEditBase(Gateway gateway, FormCollection collection)
  {
    long gatewayTypeId = gateway.GatewayTypeID;
    if (gatewayTypeId <= 7L)
    {
      if (gatewayTypeId == 2L)
        return this.GatewayTwoEdit(gateway, collection);
      if (gatewayTypeId == 7L)
        return this.GatewaySevenEdit(gateway, collection);
    }
    else
    {
      if (gatewayTypeId == 17L)
        return this.GatewaySeventeenEdit(gateway, collection);
      long num = gatewayTypeId - 25L;
      if ((ulong) num <= 11UL)
      {
        switch ((uint) num)
        {
          case 0:
            return this.GatewayTwentyfiveEdit(gateway, collection);
          case 1:
            return this.GatewayTwentysixEdit(gateway, collection);
          case 3:
            return this.GatewayTwentyeightEdit(gateway, collection);
          case 5:
            return this.GatewayThirtyEdit(gateway, collection);
          case 7:
            return this.GatewayThirtyTwoEdit(gateway, collection);
          case 8:
            return this.GatewayThirtyThreeEdit(gateway, collection);
          case 11:
            return this.GatewayThirtySixEdit(gateway, collection);
        }
      }
    }
    return this.GatewayDefaultEdit(gateway, collection);
  }

  public string GatewayDefaultEdit(Gateway gateway, FormCollection collection)
  {
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    if (string.IsNullOrEmpty(collection["Name"]))
      this.ModelState.AddModelError("Name", "Required");
    uint? nullable1 = new uint?();
    if (!string.IsNullOrEmpty(this.Request["ManualChannelMask"]))
    {
      try
      {
        nullable1 = new uint?(Convert.ToUInt32(this.Request["ManualChannelMask"]));
      }
      catch (Exception ex)
      {
        ex.Log($"OverviewControllerBase.GatewayDefaultEdit | GatewayID = {(gateway == null ? "null" : gateway.GatewayID.ToString())}, Channel Mask, Invalid Channel");
        this.ModelState.AddModelError("ChannelMask", "Invalid Channel");
      }
    }
    if (!string.IsNullOrEmpty(collection["GatewayIP"]) && collection["GatewayIP"] != "" && collection["GatewayIP"] != "0.0.0.0")
    {
      if (collection["GatewaySubnet"] == "" || collection["GatewaySubnet"] == "0.0.0.0")
        this.ModelState.AddModelError("GatewaySubnet", "Subnet Required when not using DHCP");
      if (collection["GatewayDNS"] == "" || collection["GatewayDNS"] == "0.0.0.0")
        this.ModelState.AddModelError("GatewayDNS", "Default DNS Required when not using DHCP");
      if (gateway.GatewayType.SupportsDefaultRouterIP && (collection["DefaultRouterIP"] == "" || collection["DefaultRouterIP"] == "0.0.0.0"))
        this.ModelState.AddModelError("DefaultRouterIP", "Default Gateway Required when not using DHCP");
      if (!collection["GatewayIP"].IsIPAddress())
        this.ModelState.AddModelError("GatewayIP", "Must be valid IP Address (ie 192.168.0.10)");
      if (string.IsNullOrEmpty(collection["GatewaySubnet"]) || !collection["GatewaySubnet"].IsIPAddress())
        this.ModelState.AddModelError("GatewaySubnet", "Must be valid Subnet Mask (ie 255.255.255.0)");
      if (string.IsNullOrEmpty(collection["GatewayDNS"]) || !collection["GatewayDNS"].IsIPAddress())
        this.ModelState.AddModelError("GatewayDNS", "Must be valid IP Address (ie 192.168.0.1)");
      if (gateway.GatewayType.SupportsDefaultRouterIP && (string.IsNullOrEmpty(collection["DefaultRouterIP"]) || !collection["DefaultRouterIP"].IsIPAddress()))
        this.ModelState.AddModelError("DefaultRouterIP", "Must be valid IP Address (ie 192.168.0.1)");
    }
    if (this.ModelState.IsValid)
    {
      try
      {
        Account account = Account.Load(csNet.AccountID);
        gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway settings");
        gateway.Name = collection["Name"];
        bool flag = false;
        if (gateway.GatewayTypeID > long.MinValue && gateway.GatewayTypeID != gateway.GatewayTypeID)
        {
          gateway.GatewayTypeID = gateway.GatewayTypeID;
          flag = true;
        }
        if (gateway.GatewayTypeID > long.MinValue)
        {
          GatewayType gatewayType = GatewayType.Load(gateway.GatewayTypeID);
          if (gatewayType.SupportsHeartbeat && !string.IsNullOrEmpty(collection["ReportInterval"]))
            gateway.ReportInterval = collection["ReportInterval"].ToDouble();
          if (gatewayType.SupportsHostAddress && !string.IsNullOrEmpty(collection["ServerHostAddress"]) && collection["ServerHostAddress"].Length <= 32 /*0x20*/)
            gateway.ServerHostAddress = collection["ServerHostAddress"];
          if (gatewayType.SupportsHostPort && collection["Port"].ToInt() > int.MinValue)
            gateway.Port = collection["Port"].ToInt();
          if (gatewayType.SupportsHostAddress && !gatewayType.SupportsHostAddress2)
            gateway.ServerHostAddress2 = gateway.ServerHostAddress;
          if (gatewayType.SupportsHostPort && !gatewayType.SupportsHostPort2)
            gateway.Port2 = gateway.Port;
          if (nullable1.HasValue)
          {
            long channelMask = gateway.ChannelMask;
            uint? nullable2 = nullable1;
            long? nullable3 = nullable2.HasValue ? new long?((long) nullable2.GetValueOrDefault()) : new long?();
            long valueOrDefault = nullable3.GetValueOrDefault();
            if (!(channelMask == valueOrDefault & nullable3.HasValue))
            {
              gateway.ChannelMask = (long) (nullable1 ?? uint.MaxValue);
              gateway.SendResetNetworkRequest = true;
            }
          }
          else if (gatewayType.SupportsChannel && gateway.ChannelMask > (long) int.MinValue)
            gateway.ChannelMask = gateway.ChannelMask;
          if (gatewayType.SupportsNetworkIDFilter && collection["NetworkIDFilter"].ToInt() > int.MinValue)
            gateway.NetworkIDFilter = collection["NetworkIDFilter"].ToInt();
          if (gatewayType.SupportsGatewayIP && !string.IsNullOrEmpty(collection["GatewayIP"]))
            gateway.GatewayIP = collection["GatewayIP"];
          if (gatewayType.SupportsGatewayIP && !string.IsNullOrEmpty(collection["GatewaySubnet"]))
            gateway.GatewaySubnet = collection["GatewaySubnet"];
          if (gatewayType.SupportsGatewayIP && !string.IsNullOrEmpty(collection["GatewayDNS"]))
            gateway.GatewayDNS = collection["GatewayDNS"];
          if (gatewayType.SupportsDefaultRouterIP && !string.IsNullOrEmpty(collection["DefaultRouterIP"]))
            gateway.DefaultRouterIP = collection["DefaultRouterIP"];
          if (gatewayType.SupportsNetworkListInterval)
          {
            if (collection["NetworkListInterval"].ToInt() > 0 && collection["NetworkListInterval"].ToInt() <= 720)
              gateway.NetworkListInterval = (double) collection["NetworkListInterval"].ToInt();
            else if (!string.IsNullOrWhiteSpace(collection["SetDefaults"]) && collection["SetDefaults"].ToBool())
              gateway.NetworkListInterval = gatewayType.DefaultNetworkListInterval;
          }
          if (gatewayType.SupportsObserveAware)
            gateway.ObserveAware = !string.IsNullOrEmpty(this.Request["ObserveAware"]);
          if (gatewayType.SupportsCellAPNName)
            gateway.CellAPNName = string.IsNullOrEmpty(collection["CellAPNName"]) ? (flag ? gatewayType.DefaultCellAPNName : "") : collection["CellAPNName"];
          if (gatewayType.SupportsUsername)
            gateway.Username = string.IsNullOrEmpty(collection["Username"]) ? (flag ? gatewayType.DefaultUsername : "") : collection["Username"];
          if (gatewayType.SupportsPassword)
            gateway.Password = string.IsNullOrEmpty(collection["Password"]) ? (flag ? gatewayType.DefaultPassword : "") : (MonnitSession.UseEncryption ? collection["Password"].Encrypt() : collection["Password"]);
          if (gatewayType.SupportsSecondaryDNS)
            gateway.GatewayDNS = string.IsNullOrEmpty(collection["GatewayDNS"]) ? gatewayType.DefaultGatewayDNS : collection["GatewayDNS"];
          if (gatewayType.SupportsSecondaryDNS)
            gateway.SecondaryDNS = string.IsNullOrEmpty(collection["SecondaryDNS"]) ? gatewayType.DefaultSecondaryDNS : collection["SecondaryDNS"];
          if (gatewayType.SupportsErrorHeartbeat)
            gateway.ErrorHeartbeat = gateway.ReportInterval == double.MinValue ? gatewayType.DefaultErrorHeartbeat : gateway.ErrorHeartbeat;
          if (gatewayType.SupportsPollInterval)
            gateway.PollInterval = collection["PollInterval"].ToDouble() == double.MinValue ? gatewayType.DefaultPollInterval : collection["PollInterval"].ToDouble();
          if (gatewayType.SupportsGPSReportInterval)
            gateway.GPSReportInterval = collection["GPSReportInterval"].ToDouble() == double.MinValue ? gatewayType.DefaultGPSReportInterval : collection["GPSReportInterval"].ToDouble();
          if (gatewayType.SupportsForceLowPower && !string.IsNullOrEmpty(this.Request["GatewayPowerMode"]))
            gateway.GatewayPowerMode = (eGatewayPowerMode) this.Request["GatewayPowerMode"].ToInt();
          gateway.isEnterpriseHost = false;
        }
        gateway.Save();
        if (gateway.IsDirty)
        {
          // ISSUE: reference to a compiler-generated field
          if (OverviewControllerBase.\u003C\u003Eo__12.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OverviewControllerBase.\u003C\u003Eo__12.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = OverviewControllerBase.\u003C\u003Eo__12.\u003C\u003Ep__0.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__12.\u003C\u003Ep__0, this.ViewBag, "Gateway Edit Pending");
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if (OverviewControllerBase.\u003C\u003Eo__12.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OverviewControllerBase.\u003C\u003Eo__12.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = OverviewControllerBase.\u003C\u003Eo__12.\u003C\u003Ep__1.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__12.\u003C\u003Ep__1, this.ViewBag, "Gateway Edit Success");
        }
        // ISSUE: reference to a compiler-generated field
        if (OverviewControllerBase.\u003C\u003Eo__12.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OverviewControllerBase.\u003C\u003Eo__12.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = OverviewControllerBase.\u003C\u003Eo__12.\u003C\u003Ep__2.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__12.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/Overview/GatewayEdit/" + gateway.GatewayID.ToString() : collection["returns"]);
        return "EditConfirmation";
      }
      catch
      {
      }
    }
    return $"GatewayEdit\\type_{gateway.GatewayTypeID.ToString("D3")}\\Edit";
  }

  public string GatewayTwoEdit(Gateway gateway, FormCollection collection)
  {
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    if (string.IsNullOrEmpty(collection["Name"]))
      this.ModelState.AddModelError("Name", "Required");
    else
      gateway.Name = collection["Name"];
    if (collection["ReportInterval"].ToInt() < 0 || collection["ReportInterval"].ToInt() > 720)
      this.ModelState.AddModelError("ReportInterval", "Must be between 1 and 720");
    gateway.ReportInterval = (double) collection["ReportInterval"].ToInt();
    gateway.ObserveAware = !string.IsNullOrEmpty(this.Request["ObserveAware"]);
    if (this.ModelState.IsValid)
    {
      try
      {
        Account account = Account.Load(csNet.AccountID);
        gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway settings");
        gateway.isEnterpriseHost = false;
        gateway.Save();
        if (gateway.IsDirty)
        {
          // ISSUE: reference to a compiler-generated field
          if (OverviewControllerBase.\u003C\u003Eo__13.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OverviewControllerBase.\u003C\u003Eo__13.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = OverviewControllerBase.\u003C\u003Eo__13.\u003C\u003Ep__0.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__13.\u003C\u003Ep__0, this.ViewBag, "Gateway Edit Pending");
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if (OverviewControllerBase.\u003C\u003Eo__13.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OverviewControllerBase.\u003C\u003Eo__13.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = OverviewControllerBase.\u003C\u003Eo__13.\u003C\u003Ep__1.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__13.\u003C\u003Ep__1, this.ViewBag, "Gateway Edit Success");
        }
        // ISSUE: reference to a compiler-generated field
        if (OverviewControllerBase.\u003C\u003Eo__13.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OverviewControllerBase.\u003C\u003Eo__13.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = OverviewControllerBase.\u003C\u003Eo__13.\u003C\u003Ep__2.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__13.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/Overview/GatewayEdit/" + gateway.GatewayID.ToString() : collection["returns"]);
        return "EditConfirmation";
      }
      catch (Exception ex)
      {
        this.ModelState.AddModelError("", ex.Message);
      }
    }
    return $"GatewayEdit\\type_{gateway.GatewayTypeID.ToString("D3")}\\Edit";
  }

  public string GatewaySevenEdit(Gateway gateway, FormCollection collection)
  {
    GatewayType gatewayType = GatewayType.Load(gateway.GatewayTypeID);
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    if (gatewayType != null)
    {
      if (string.IsNullOrEmpty(collection["Name"]))
      {
        this.ModelState.AddModelError("Name", "Required");
        this.ModelState.AddModelError("", "General -> Name");
      }
      else
        gateway.Name = collection["Name"];
      if (collection["ReportInterval"].ToInt() < 0 || collection["ReportInterval"].ToInt() > 720)
      {
        this.ModelState.AddModelError("ReportInterval", "Must be between 1 and 720");
        this.ModelState.AddModelError("", "General -> Heartbeat");
      }
      if (new Version(gateway.GatewayFirmwareVersion) >= new Version("3.1.0.0") && collection["PollInterval"] != null && (collection["PollInterval"].ToInt() < 0 || collection["PollInterval"].ToInt() > 720))
      {
        this.ModelState.AddModelError("PollInterval", "Must be between 0 and 720");
        this.ModelState.AddModelError("", "General -> Poll Rate");
      }
      if (collection["SingleQueueExpiration"] != null && (collection["SingleQueueExpiration"].ToDouble() < 1.0 || collection["SingleQueueExpiration"].ToDouble() > 720.0))
      {
        this.ModelState.AddModelError("SingleQueueExpiration", "between 1 and 720");
        this.ModelState.AddModelError("", "Interface Activation -> Queue Expiration");
      }
      if (!string.IsNullOrEmpty(collection["SingleQueueExpiration"]))
        gateway.SingleQueueExpiration = (double) collection["SingleQueueExpiration"].ToInt();
      else if (!string.IsNullOrWhiteSpace(collection["SetDefaults"]) && collection["SetDefaults"].ToBool())
        gateway.SingleQueueExpiration = gatewayType.DefaultSingleQueueExpiration;
      if (!string.IsNullOrEmpty(collection["PollInterval"]))
        gateway.PollInterval = (double) collection["PollInterval"].ToInt();
      else if (!string.IsNullOrWhiteSpace(collection["SetDefaults"]) && collection["SetDefaults"].ToBool())
        gateway.PollInterval = gatewayType.DefaultPollInterval;
      gateway.ReportInterval = collection["ReportInterval"].ToDouble();
      gateway.ObserveAware = !string.IsNullOrEmpty(this.Request["ObserveAware"]);
      if (collection["DHCPorStatic"] == "static")
      {
        if (string.IsNullOrEmpty(collection["GatewayIP"]) || collection["GatewayIP"] == "0.0.0.0" || !collection["GatewayIP"].IsIPAddress())
        {
          this.ModelState.AddModelError("GatewayIP", "Must be valid IP Address (ie 192.168.0.10)");
          this.ModelState.AddModelError("", "Local Area Network -> IP Address");
        }
        if (string.IsNullOrEmpty(collection["GatewaySubnet"]) || collection["GatewaySubnet"] == "0.0.0.0" || !collection["GatewaySubnet"].IsIPAddress())
        {
          this.ModelState.AddModelError("GatewaySubnet", "Must be valid subnet mask (ie 255.255.255.0)");
          this.ModelState.AddModelError("", "Local Area Network -> Subnet Mask");
        }
        if (string.IsNullOrEmpty(collection["GatewayDNS"]) || collection["GatewayDNS"] == "0.0.0.0" || !collection["GatewayDNS"].IsIPAddress())
        {
          this.ModelState.AddModelError("GatewayDNS", "Must be valid IP Address (ie 192.168.0.1)");
          this.ModelState.AddModelError("", "Local Area Network -> DNS Server");
        }
        if (string.IsNullOrEmpty(collection["DefaultRouterIP"]) || collection["DefaultRouterIP"] == "0.0.0.0" || !collection["DefaultRouterIP"].IsIPAddress())
        {
          this.ModelState.AddModelError("DefaultRouterIP", "Must be valid IP Address (ie 192.168.0.1)");
          this.ModelState.AddModelError("", "Local Area Network -> Default Gateway");
        }
        gateway.GatewayIP = collection["GatewayIP"];
        gateway.GatewaySubnet = collection["GatewaySubnet"];
        gateway.DefaultRouterIP = collection["DefaultRouterIP"];
        gateway.GatewayDNS = collection["GatewayDNS"];
      }
      else
      {
        gateway.GatewayIP = gatewayType.DefaultGatewayIP;
        gateway.GatewaySubnet = gatewayType.DefaultGatewaySubnet;
        gateway.DefaultRouterIP = gatewayType.DefaultRouterIP;
        gateway.GatewayDNS = gatewayType.DefaultGatewayDNS;
      }
      if (!string.IsNullOrEmpty(collection["ModbusInterfaceActive"]))
      {
        if (collection["ModbusInterfaceTimeout"].ToDouble() < 1.0 || collection["ModbusInterfaceTimeout"].ToDouble() > 60.0)
        {
          this.ModelState.AddModelError("RealTimeInterfaceTimeout", "between 1 and 60");
          this.ModelState.AddModelError("", "Modbus Interface -> TCP Timeout");
        }
        if (collection["ModbusInterfacePort"].ToInt() < 1 || collection["ModbusInterfacePort"].ToInt() > (int) ushort.MaxValue)
        {
          this.ModelState.AddModelError("ModbusInterfacePort", "Invalid Port");
          this.ModelState.AddModelError("", "Modbus Interface -> Port");
        }
        gateway.ModbusInterfaceActive = true;
        gateway.ModbusInterfaceTimeout = collection["ModbusInterfaceTimeout"].ToDouble();
        gateway.ModbusInterfacePort = collection["ModbusInterfacePort"].ToInt();
      }
      else
      {
        gateway.ModbusInterfaceActive = false;
        gateway.ModbusInterfaceTimeout = gatewayType.DefaultModbusInterfaceTimeout;
        gateway.ModbusInterfacePort = gatewayType.DefaultModbusInterfacePort;
      }
      if (!string.IsNullOrEmpty(collection["SNMPInterface1Active"]))
      {
        if (string.IsNullOrEmpty(collection["SNMPInterfaceAddress1"]) || !collection["SNMPInterfaceAddress1"].IsIPAddress())
        {
          this.ModelState.AddModelError("SNMPInterfaceAddress1", "Invalid IP Address");
          this.ModelState.AddModelError("", "SNMP Interface 1 -> SNMP Address");
        }
        if (collection["SNMPInterfacePort1"].ToInt() < 1 || collection["SNMPInterfacePort1"].ToInt() > (int) ushort.MaxValue)
        {
          this.ModelState.AddModelError("SNMPInterfacePort1", "Invalid Port");
          this.ModelState.AddModelError("", "SNMP Interface 1 -> Port");
        }
        if (!string.IsNullOrEmpty(collection["SNMPTrap1Active"]) && (collection["SNMPTrapPort1"].ToInt() < 1 || collection["SNMPTrapPort1"].ToInt() > (int) ushort.MaxValue))
        {
          this.ModelState.AddModelError("SNMPTrapPort1", "Invalid Port");
          this.ModelState.AddModelError("", "SNMP Interface 1 -> Trap Port");
        }
        gateway.SNMPInterface1Active = true;
        gateway.SNMPInterfaceAddress1 = collection["SNMPInterfaceAddress1"];
        gateway.SNMPInterfacePort1 = collection["SNMPInterfacePort1"].ToInt();
        gateway.SNMPInterfaceAddress2 = collection["SNMPInterfaceAddress2"];
        gateway.SNMPTrap1Active = gateway.SNMPInterfaceAddress2.Length > 0;
        gateway.SNMPTrapPort1 = collection["SNMPTrapPort1"].ToInt();
        gateway.SNMPCommunityString = collection["SNMPCommunityString"].ToStringSafe();
      }
      else
      {
        gateway.SNMPInterface1Active = false;
        gateway.SNMPInterfaceAddress1 = "0.0.0.0";
        gateway.SNMPInterfacePort1 = gatewayType.DefaultSNMPInterfacePort;
        gateway.SNMPInterfaceAddress2 = "0.0.0.0";
        gateway.SNMPTrap1Active = false;
        gateway.SNMPTrapPort1 = gatewayType.DefaultSNMPTrapPort;
        gateway.SNMPCommunityString = string.Empty;
      }
      if (!string.IsNullOrEmpty(collection["SNMPInterface2Active"]))
      {
        if (string.IsNullOrEmpty(collection["SNMPInterfaceAddress2"]) || !collection["SNMPInterfaceAddress2"].IsIPAddress())
        {
          this.ModelState.AddModelError("SNMPInterfaceAddress2", "Invalid IP Address");
          this.ModelState.AddModelError("", "SNMP Interface 2 -> SNMP Address");
        }
        if (collection["SNMPInterfacePort2"].ToInt() < 1 || collection["SNMPInterfacePort2"].ToInt() > (int) ushort.MaxValue)
        {
          this.ModelState.AddModelError("SNMPInterfacePort2", "Invalid Port");
          this.ModelState.AddModelError("", "SNMP Interface 2 -> Port");
        }
        if (!string.IsNullOrEmpty(collection["SNMPTrap2Active"]) && (collection["SNMPTrapPort2"].ToInt() < 1 || collection["SNMPTrapPort2"].ToInt() > (int) ushort.MaxValue))
        {
          this.ModelState.AddModelError("SNMPTrapPort2", "Invalid Port");
          this.ModelState.AddModelError("", "SNMP Interface 2 -> Trap Port");
        }
        gateway.SNMPInterface2Active = true;
        gateway.SNMPInterfaceAddress2 = collection["SNMPInterfaceAddress2"];
        gateway.SNMPInterfacePort2 = collection["SNMPInterfacePort2"].ToInt();
        gateway.SNMPTrap2Active = !string.IsNullOrEmpty(collection["SNMPTrap2Active"]);
        gateway.SNMPTrapPort2 = !gateway.SNMPTrap2Active ? gatewayType.DefaultSNMPTrapPort : collection["SNMPTrapPort2"].ToInt();
      }
      else
      {
        gateway.SNMPInterface2Active = false;
        gateway.SNMPInterfaceAddress2 = "0.0.0.0";
        gateway.SNMPInterfacePort2 = gatewayType.DefaultSNMPInterfacePort;
        gateway.SNMPTrap2Active = false;
        gateway.SNMPTrapPort2 = gatewayType.DefaultSNMPTrapPort;
      }
      if (!string.IsNullOrEmpty(collection["SNMPInterface3Active"]))
      {
        if (string.IsNullOrEmpty(collection["SNMPInterfaceAddress3"]) || !gateway.SNMPInterfaceAddress3.IsIPAddress())
        {
          this.ModelState.AddModelError("SNMPInterfaceAddress3", "Invalid IP Address");
          this.ModelState.AddModelError("", "SNMP Interface 3 -> SNMP Address");
        }
        if (collection["SNMPInterfacePort3"].ToInt() < 1 || collection["SNMPInterfacePort3"].ToInt() > (int) ushort.MaxValue)
        {
          this.ModelState.AddModelError("SNMPInterfacePort3", "Invalid Port");
          this.ModelState.AddModelError("", "SNMP Interface 3 -> Port");
        }
        if (!string.IsNullOrEmpty(collection["SNMPTrap3Active"]) && (collection["SNMPTrapPort3"].ToInt() < 1 || collection["SNMPTrapPort3"].ToInt() > (int) ushort.MaxValue))
        {
          this.ModelState.AddModelError("SNMPTrapPort3", "Invalid Port");
          this.ModelState.AddModelError("", "SNMP Interface 3 -> Trap Port");
        }
        gateway.SNMPInterface3Active = true;
        gateway.SNMPInterfaceAddress3 = collection["SNMPInterfaceAddress3"];
        gateway.SNMPInterfacePort3 = collection["SNMPInterfacePort3"].ToInt();
        gateway.SNMPTrap3Active = !string.IsNullOrEmpty(collection["SNMPTrap3Active"]);
        gateway.SNMPTrapPort3 = !gateway.SNMPTrap3Active ? gatewayType.DefaultSNMPTrapPort : collection["SNMPTrapPort3"].ToInt();
      }
      else
      {
        gateway.SNMPInterface3Active = false;
        gateway.SNMPInterfaceAddress3 = "0.0.0.0";
        gateway.SNMPInterfacePort3 = gatewayType.DefaultSNMPInterfacePort;
        gateway.SNMPTrap3Active = false;
        gateway.SNMPTrapPort3 = gatewayType.DefaultSNMPTrapPort;
      }
      if (!string.IsNullOrEmpty(collection["SNMPInterface4Active"]))
      {
        if (string.IsNullOrEmpty(collection["SNMPInterfaceAddress4"]) || !gateway.SNMPInterfaceAddress4.IsIPAddress())
        {
          this.ModelState.AddModelError("SNMPInterfaceAddress4", "Invalid IP Address");
          this.ModelState.AddModelError("", "SNMP Interface 4 -> SNMP Address");
        }
        if (collection["SNMPInterfacePort4"].ToInt() < 1 || collection["SNMPInterfacePort4"].ToInt() > (int) ushort.MaxValue)
        {
          this.ModelState.AddModelError("SNMPInterfacePort4", "Invalid Port");
          this.ModelState.AddModelError("", "SNMP Interface 4 -> Port");
        }
        if (!string.IsNullOrEmpty(collection["SNMPTrap4Active"]) && (collection["SNMPTrapPort4"].ToInt() < 1 || collection["SNMPTrapPort4"].ToInt() > (int) ushort.MaxValue))
        {
          this.ModelState.AddModelError("SNMPTrapPort4", "Invalid Port");
          this.ModelState.AddModelError("", "SNMP Interface 4 -> Trap Port");
        }
        gateway.SNMPInterface4Active = true;
        gateway.SNMPInterfaceAddress4 = collection["SNMPInterfaceAddress4"];
        gateway.SNMPInterfacePort4 = collection["SNMPInterfacePort4"].ToInt();
        gateway.SNMPTrap4Active = !string.IsNullOrEmpty(collection["SNMPTrap4Active"]);
        gateway.SNMPTrapPort4 = !gateway.SNMPTrap4Active ? gatewayType.DefaultSNMPTrapPort : collection["SNMPTrapPort4"].ToInt();
      }
      else
      {
        gateway.SNMPInterface4Active = false;
        gateway.SNMPInterfaceAddress4 = "0.0.0.0";
        gateway.SNMPInterfacePort4 = gatewayType.DefaultSNMPInterfacePort;
        gateway.SNMPTrap4Active = false;
        gateway.SNMPTrapPort4 = gatewayType.DefaultSNMPTrapPort;
      }
      if (!string.IsNullOrEmpty(collection["RealTimeInterfaceActive"]))
      {
        if (collection["RealTimeInterfaceTimeout"].ToDouble() < 1.0 || collection["RealTimeInterfaceTimeout"].ToDouble() > 3600.0)
        {
          this.ModelState.AddModelError("RealTimeInterfaceTimeout", "between 1 and 3600");
          this.ModelState.AddModelError("", "Real Time Interface -> TCP Timeout");
        }
        if (collection["RealTimeInterfacePort"].ToInt() < 1 || collection["RealTimeInterfacePort"].ToInt() > (int) ushort.MaxValue)
        {
          this.ModelState.AddModelError("RealTimeInterfacePort", "Invalid Port");
          this.ModelState.AddModelError("", "Real Time Interface -> Port");
        }
        gateway.RealTimeInterfaceActive = true;
        gateway.RealTimeInterfaceTimeout = collection["RealTimeInterfaceTimeout"].ToDouble();
        gateway.RealTimeInterfacePort = collection["RealTimeInterfacePort"].ToInt();
      }
      else
      {
        gateway.RealTimeInterfaceActive = false;
        gateway.RealTimeInterfaceTimeout = gatewayType.DefaultRealTimeInterfaceTimeout;
        gateway.RealTimeInterfacePort = gatewayType.DefaultRealTimeInterfacePort;
      }
      if (this.ModelState.IsValid)
      {
        try
        {
          Account account = Account.Load(csNet.AccountID);
          gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway settings");
          if (gateway.isEnterpriseHost)
          {
            gateway.ServerHostAddress = gatewayType.DefaultServerHostAddress;
            gateway.ServerHostAddress2 = gatewayType.DefaultServerHostAddress2;
            gateway.Port = gatewayType.DefaultPort;
            gateway.Port2 = gatewayType.DefaultPort2;
            gateway.isEnterpriseHost = false;
          }
          gateway.Save();
          if (gateway.IsDirty)
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__14.\u003C\u003Ep__0 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__14.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__14.\u003C\u003Ep__0.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__14.\u003C\u003Ep__0, this.ViewBag, "Gateway Edit Pending");
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__14.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__14.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__14.\u003C\u003Ep__1.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__14.\u003C\u003Ep__1, this.ViewBag, "Gateway Edit Success");
          }
          // ISSUE: reference to a compiler-generated field
          if (OverviewControllerBase.\u003C\u003Eo__14.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OverviewControllerBase.\u003C\u003Eo__14.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = OverviewControllerBase.\u003C\u003Eo__14.\u003C\u003Ep__2.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__14.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/Overview/GatewayEdit/" + gateway.GatewayID.ToString() : collection["returns"]);
          return "EditConfirmation";
        }
        catch (Exception ex)
        {
          this.ModelState.AddModelError("", ex.Message);
        }
      }
    }
    else
      this.ModelState.AddModelError("", "Invalid Gateway Type");
    return $"GatewayEdit\\type_{gateway.GatewayTypeID.ToString("D3")}\\Edit";
  }

  public string GatewaySeventeenEdit(Gateway gateway, FormCollection collection)
  {
    GatewayType gatewayType = GatewayType.Load(gateway.GatewayTypeID);
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    if (gatewayType != null)
    {
      if (string.IsNullOrEmpty(collection["Name"]))
        this.ModelState.AddModelError("Name", "Required");
      else
        gateway.Name = collection["Name"];
      if (collection["ReportInterval"].ToInt() < 0 || collection["ReportInterval"].ToInt() > 720)
        this.ModelState.AddModelError("ReportInterval", "Must be between 1 and 720");
      if (new Version(gateway.GatewayFirmwareVersion) >= new Version("3.1.0.0") && collection["PollInterval"] != null && (collection["PollInterval"].ToInt() < 0 || collection["PollInterval"].ToInt() > 720))
        this.ModelState.AddModelError("PollInterval", "Must be between 0 and 720");
      gateway.PollInterval = string.IsNullOrEmpty(collection["PollInterval"]) ? gatewayType.DefaultPollInterval : (double) collection["PollInterval"].ToInt();
      gateway.ReportInterval = (double) collection["ReportInterval"].ToInt();
      gateway.ObserveAware = !string.IsNullOrEmpty(collection["ObserveAware"]);
      gateway.GatewayPowerMode = !string.IsNullOrEmpty(collection["GatewayPowerMode"]) ? (eGatewayPowerMode) Enum.Parse(typeof (eGatewayPowerMode), collection["GatewayPowerMode"]) : eGatewayPowerMode.Standard;
      if (this.ModelState.IsValid)
      {
        try
        {
          Account account = Account.Load(csNet.AccountID);
          gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway settings");
          gateway.isEnterpriseHost = false;
          gateway.Save();
          if (gateway.IsDirty)
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__15.\u003C\u003Ep__0 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__15.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__15.\u003C\u003Ep__0.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__15.\u003C\u003Ep__0, this.ViewBag, "Gateway Edit Pending");
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__15.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__15.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__15.\u003C\u003Ep__1.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__15.\u003C\u003Ep__1, this.ViewBag, "Gateway Edit Success");
          }
          // ISSUE: reference to a compiler-generated field
          if (OverviewControllerBase.\u003C\u003Eo__15.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OverviewControllerBase.\u003C\u003Eo__15.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = OverviewControllerBase.\u003C\u003Eo__15.\u003C\u003Ep__2.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__15.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/Overview/GatewayEdit/" + gateway.GatewayID.ToString() : collection["returns"]);
          return "EditConfirmation";
        }
        catch (Exception ex)
        {
          this.ModelState.AddModelError("", ex.Message);
        }
      }
    }
    else
      this.ModelState.AddModelError("", "Invalid Gateway Type");
    return $"GatewayEdit\\type_{gateway.GatewayTypeID.ToString("D3")}\\Edit";
  }

  public string GatewayTwentyfiveEdit(Gateway gateway, FormCollection collection)
  {
    GatewayType gatewayType = GatewayType.Load(gateway.GatewayTypeID);
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    if (gatewayType != null)
    {
      if (string.IsNullOrEmpty(collection["Name"]))
        this.ModelState.AddModelError("Name", "Required");
      else
        gateway.Name = collection["Name"];
      if (collection["ReportInterval"].ToInt() < 0 || collection["ReportInterval"].ToInt() > 720)
        this.ModelState.AddModelError("ReportInterval", "Must be between 1 and 720");
      if (new Version(gateway.GatewayFirmwareVersion) >= new Version("3.1.0.0") && collection["PollInterval"] != null && (collection["PollInterval"].ToInt() < 0 || collection["PollInterval"].ToInt() > 720))
        this.ModelState.AddModelError("PollInterval", "Must be between 0 and 720");
      gateway.PollInterval = string.IsNullOrEmpty(collection["PollInterval"]) ? gatewayType.DefaultPollInterval : (double) collection["PollInterval"].ToInt();
      gateway.ReportInterval = (double) collection["ReportInterval"].ToInt();
      gateway.ObserveAware = !string.IsNullOrEmpty(collection["ObserveAware"]);
      gateway.GatewayPowerMode = !string.IsNullOrEmpty(collection["GatewayPowerMode"]) ? (eGatewayPowerMode) Enum.Parse(typeof (eGatewayPowerMode), collection["GatewayPowerMode"]) : eGatewayPowerMode.Standard;
      if (this.ModelState.IsValid)
      {
        try
        {
          Account account = Account.Load(csNet.AccountID);
          gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway settings");
          gateway.isEnterpriseHost = false;
          gateway.Save();
          if (gateway.IsDirty)
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__16.\u003C\u003Ep__0 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__16.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__16.\u003C\u003Ep__0.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__16.\u003C\u003Ep__0, this.ViewBag, "Gateway Edit Pending");
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__16.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__16.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__16.\u003C\u003Ep__1.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__16.\u003C\u003Ep__1, this.ViewBag, "Gateway Edit Success");
          }
          // ISSUE: reference to a compiler-generated field
          if (OverviewControllerBase.\u003C\u003Eo__16.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OverviewControllerBase.\u003C\u003Eo__16.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = OverviewControllerBase.\u003C\u003Eo__16.\u003C\u003Ep__2.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__16.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/Overview/GatewayEdit/" + gateway.GatewayID.ToString() : collection["returns"]);
          return "EditConfirmation";
        }
        catch (Exception ex)
        {
          this.ModelState.AddModelError("", ex.Message);
        }
      }
    }
    else
      this.ModelState.AddModelError("", "Invalid Gateway Type");
    return $"GatewayEdit\\type_{gateway.GatewayTypeID.ToString("D3")}\\Edit";
  }

  public string GatewayTwentysixEdit(Gateway gateway, FormCollection collection)
  {
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    GatewayType gatewayType = GatewayType.Load(gateway.GatewayTypeID);
    if (gatewayType != null)
    {
      if (string.IsNullOrEmpty(collection["Name"]))
        this.ModelState.AddModelError("Name", "Required");
      else
        gateway.Name = collection["Name"];
      if (collection["ReportInterval"].ToInt() < 0 || collection["ReportInterval"].ToInt() > 720)
        this.ModelState.AddModelError("ReportInterval", "Must be between 1 and 720");
      if (new Version(gateway.GatewayFirmwareVersion) >= new Version("3.1.0.0") && collection["PollInterval"] != null && (collection["PollInterval"].ToInt() < 0 || collection["PollInterval"].ToInt() > 720))
        this.ModelState.AddModelError("PollInterval", "Must be between 0 and 720");
      gateway.PollInterval = string.IsNullOrEmpty(collection["PollInterval"]) ? gatewayType.DefaultPollInterval : (double) collection["PollInterval"].ToInt();
      gateway.ReportInterval = (double) collection["ReportInterval"].ToInt();
      gateway.ObserveAware = !string.IsNullOrEmpty(collection["ObserveAware"]);
      gateway.GatewayPowerMode = !string.IsNullOrEmpty(collection["GatewayPowerMode"]) ? (eGatewayPowerMode) Enum.Parse(typeof (eGatewayPowerMode), collection["GatewayPowerMode"]) : eGatewayPowerMode.Standard;
      if (this.ModelState.IsValid)
      {
        try
        {
          Account account = Account.Load(csNet.AccountID);
          gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway settings");
          gateway.isEnterpriseHost = false;
          gateway.Save();
          if (gateway.IsDirty)
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__17.\u003C\u003Ep__0 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__17.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__17.\u003C\u003Ep__0.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__17.\u003C\u003Ep__0, this.ViewBag, "Gateway Edit Pending");
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__17.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__17.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__17.\u003C\u003Ep__1.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__17.\u003C\u003Ep__1, this.ViewBag, "Gateway Edit Success");
          }
          // ISSUE: reference to a compiler-generated field
          if (OverviewControllerBase.\u003C\u003Eo__17.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OverviewControllerBase.\u003C\u003Eo__17.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = OverviewControllerBase.\u003C\u003Eo__17.\u003C\u003Ep__2.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__17.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/Overview/GatewayEdit/" + gateway.GatewayID.ToString() : collection["returns"]);
          return "EditConfirmation";
        }
        catch (Exception ex)
        {
          this.ModelState.AddModelError("", ex.Message);
        }
      }
    }
    else
      this.ModelState.AddModelError("", "Invalid Gateway Type");
    return $"GatewayEdit\\type_{gateway.GatewayTypeID.ToString("D3")}\\Edit";
  }

  public string GatewayTwentyeightEdit(Gateway gateway, FormCollection collection)
  {
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    if (string.IsNullOrEmpty(collection["Name"]))
      this.ModelState.AddModelError("Name", "Required");
    else
      gateway.Name = collection["Name"];
    if (collection["ReportInterval"].ToInt() < 0 || collection["ReportInterval"].ToInt() > 720)
      this.ModelState.AddModelError("ReportInterval", "Must be between 1 and 720");
    gateway.ReportInterval = (double) collection["ReportInterval"].ToInt();
    gateway.ObserveAware = !string.IsNullOrEmpty(collection["ObserveAware"]);
    if (this.ModelState.IsValid)
    {
      try
      {
        Account account = Account.Load(csNet.AccountID);
        gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway settings");
        gateway.isEnterpriseHost = false;
        gateway.Save();
        if (gateway.IsDirty)
        {
          // ISSUE: reference to a compiler-generated field
          if (OverviewControllerBase.\u003C\u003Eo__18.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OverviewControllerBase.\u003C\u003Eo__18.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = OverviewControllerBase.\u003C\u003Eo__18.\u003C\u003Ep__0.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__18.\u003C\u003Ep__0, this.ViewBag, "Gateway Edit Pending");
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if (OverviewControllerBase.\u003C\u003Eo__18.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OverviewControllerBase.\u003C\u003Eo__18.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = OverviewControllerBase.\u003C\u003Eo__18.\u003C\u003Ep__1.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__18.\u003C\u003Ep__1, this.ViewBag, "Gateway Edit Success");
        }
        // ISSUE: reference to a compiler-generated field
        if (OverviewControllerBase.\u003C\u003Eo__18.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OverviewControllerBase.\u003C\u003Eo__18.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = OverviewControllerBase.\u003C\u003Eo__18.\u003C\u003Ep__2.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__18.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/Overview/GatewayEdit/" + gateway.GatewayID.ToString() : collection["returns"]);
        return "EditConfirmation";
      }
      catch (Exception ex)
      {
        this.ModelState.AddModelError("", ex.Message);
      }
    }
    return $"GatewayEdit\\type_{gateway.GatewayTypeID.ToString("D3")}\\Edit";
  }

  public string GatewayThirtyEdit(Gateway gateway, FormCollection collection)
  {
    GatewayType gatewayType = GatewayType.Load(gateway.GatewayTypeID);
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    if (gatewayType != null)
    {
      if (string.IsNullOrEmpty(collection["Name"]))
      {
        this.ModelState.AddModelError("Name", "Required");
        this.ModelState.AddModelError("", "General -> Name");
      }
      else
        gateway.Name = collection["Name"];
      if (collection["ReportInterval"].ToInt() < 0 || collection["ReportInterval"].ToInt() > 720)
      {
        this.ModelState.AddModelError("ReportInterval", "Must be between 1 and 720");
        this.ModelState.AddModelError("", "General -> Heartbeat");
      }
      gateway.ReportInterval = collection["ReportInterval"].ToDouble();
      gateway.GatewayCommunicationPreference = (eGatewayCommunicationPreference) collection["GatewayCommunicationPreference"].ToInt();
      gateway.ObserveAware = !string.IsNullOrEmpty(this.Request["ObserveAware"]);
      gateway.DisableNetworkOnServerError = !string.IsNullOrEmpty(this.Request["DisableNetworkOnServerError"]);
      gateway.GatewayPowerMode = !string.IsNullOrEmpty(collection["GatewayPowerMode"]) ? (eGatewayPowerMode) Enum.Parse(typeof (eGatewayPowerMode), collection["GatewayPowerMode"]) : eGatewayPowerMode.Standard;
      if (!string.IsNullOrEmpty(this.Request["GPSReportInterval"]))
      {
        if (collection["GPSReportInterval"].ToInt() < 0 || collection["GPSReportInterval"].ToInt() > 720)
        {
          this.ModelState.AddModelError("ReportInterval", "Must be between 1 and 720");
          this.ModelState.AddModelError("", "General -> Heartbeat");
        }
        gateway.GPSReportInterval = collection["GPSReportInterval"].ToDouble();
      }
      else
        gateway.GPSReportInterval = 0.0;
      if (!string.IsNullOrEmpty(collection["PollInterval"]))
      {
        if (collection["PollInterval"].ToInt() < 0 || collection["PollInterval"].ToInt() > 720)
        {
          this.ModelState.AddModelError("PollInterval", "Must be between 0 and 720");
          this.ModelState.AddModelError("", "General -> Poll Rate");
        }
        gateway.PollInterval = (double) collection["PollInterval"].ToInt();
      }
      else
        gateway.PollInterval = gatewayType.DefaultPollInterval;
      if (collection["DHCPorStatic"] == "static")
      {
        if (string.IsNullOrEmpty(collection["GatewayIP"]) || collection["GatewayIP"] == "0.0.0.0" || !collection["GatewayIP"].IsIPAddress())
        {
          this.ModelState.AddModelError("GatewayIP", "Must be valid IP Address (ie 192.168.0.10)");
          this.ModelState.AddModelError("", "Local Area Network -> IP Address");
        }
        if (string.IsNullOrEmpty(collection["GatewaySubnet"]) || collection["GatewaySubnet"] == "0.0.0.0" || !collection["GatewaySubnet"].IsIPAddress())
        {
          this.ModelState.AddModelError("GatewaySubnet", "Must be valid subnet mask (ie 255.255.255.0)");
          this.ModelState.AddModelError("", "Local Area Network -> Subnet Mask");
        }
        if (string.IsNullOrEmpty(collection["GatewayDNS"]) || collection["GatewayDNS"] == "0.0.0.0" || !collection["GatewayDNS"].IsIPAddress())
        {
          this.ModelState.AddModelError("GatewayDNS", "Must be valid IP Address (ie 192.168.0.1)");
          this.ModelState.AddModelError("", "Local Area Network -> DNS Server");
        }
        if (string.IsNullOrEmpty(collection["DefaultRouterIP"]) || collection["DefaultRouterIP"] == "0.0.0.0" || !collection["DefaultRouterIP"].IsIPAddress())
        {
          this.ModelState.AddModelError("DefaultRouterIP", "Must be valid IP Address (ie 192.168.0.1)");
          this.ModelState.AddModelError("", "Local Area Network -> Default Gateway");
        }
        gateway.GatewayIP = collection["GatewayIP"];
        gateway.GatewaySubnet = collection["GatewaySubnet"];
        gateway.DefaultRouterIP = collection["DefaultRouterIP"];
        gateway.GatewayDNS = collection["GatewayDNS"];
      }
      else
      {
        gateway.GatewayIP = gatewayType.DefaultGatewayIP;
        gateway.GatewaySubnet = gatewayType.DefaultGatewaySubnet;
        gateway.DefaultRouterIP = gatewayType.DefaultRouterIP;
        gateway.GatewayDNS = gatewayType.DefaultGatewayDNS;
      }
      gateway.UMNOProf = collection["UMNOProf"].ToInt();
      if (gateway.UMNOProf == 100)
      {
        gateway.CellAPNName = collection["CellAPNName"];
        gateway.SIMAuthType = collection["SIMAuthType"].ToInt();
        gateway.Username = collection["Username"];
        gateway.Password = collection["Password"];
        gateway.M1BandMask = (long) collection["M1BandMask"].ToInt();
        gateway.NB1BandMask = (long) collection["NB1BandMask"].ToInt();
      }
      else
      {
        gateway.CellAPNName = "";
        gateway.SIMAuthType = 0;
        gateway.Username = "";
        gateway.Password = "";
        gateway.M1BandMask = 0L;
        gateway.NB1BandMask = 0L;
      }
      if (collection["ResetInterval"] != null && (collection["ResetInterval"].ToInt() < 0 || collection["ResetInterval"].ToInt() > 8760))
      {
        this.ModelState.AddModelError("ResetInterval", "Must be between 0 and 8760");
        this.ModelState.AddModelError("", "Commands -> Reset Interval");
      }
      gateway.ResetInterval = string.IsNullOrEmpty(collection["ResetInterval"]) ? 168 : collection["ResetInterval"].ToInt();
      gateway.HTTPInterfaceActive = collection["HTTPInterfaceActive"].ToBool();
      gateway.HTTPServiceTimeout = (double) collection["HTTPServiceTimeout"].ToInt();
      gateway.SNMPInterfaceAddress2 = "0.0.0.0";
      gateway.SNMPInterfaceAddress3 = "255.255.255.255";
      gateway.NTPServerIP = "0.0.0.0";
      gateway.NTPMinSampleRate = 10.0;
      if (this.ModelState.IsValid)
      {
        try
        {
          Account account = Account.Load(csNet.AccountID);
          gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway settings");
          if (gateway.isEnterpriseHost)
          {
            gateway.ServerHostAddress = gatewayType.DefaultServerHostAddress;
            gateway.ServerHostAddress2 = gatewayType.DefaultServerHostAddress2;
            gateway.Port = gatewayType.DefaultPort;
            gateway.Port2 = gatewayType.DefaultPort2;
            gateway.isEnterpriseHost = false;
          }
          gateway.Save();
          if (gateway.IsDirty)
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__19.\u003C\u003Ep__0 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__19.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__19.\u003C\u003Ep__0.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__19.\u003C\u003Ep__0, this.ViewBag, "Gateway Edit Pending");
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__19.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__19.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__19.\u003C\u003Ep__1.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__19.\u003C\u003Ep__1, this.ViewBag, "Gateway Edit Success");
          }
          // ISSUE: reference to a compiler-generated field
          if (OverviewControllerBase.\u003C\u003Eo__19.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OverviewControllerBase.\u003C\u003Eo__19.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = OverviewControllerBase.\u003C\u003Eo__19.\u003C\u003Ep__2.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__19.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/Overview/GatewayEdit/" + gateway.GatewayID.ToString() : collection["returns"]);
          return "EditConfirmation";
        }
        catch (Exception ex)
        {
          this.ModelState.AddModelError("", ex.Message);
        }
      }
    }
    else
      this.ModelState.AddModelError("", "Invalid Gateway Type");
    return $"GatewayEdit\\type_{gateway.GatewayTypeID.ToString("D3")}\\Edit";
  }

  public string GatewayThirtyTwoEdit(Gateway gateway, FormCollection collection)
  {
    GatewayType gatewayType = GatewayType.Load(gateway.GatewayTypeID);
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    if (gatewayType != null)
    {
      if (string.IsNullOrEmpty(collection["Name"]))
        this.ModelState.AddModelError("Name", "Required");
      else
        gateway.Name = collection["Name"];
      if (collection["ReportInterval"].ToInt() < 0 || collection["ReportInterval"].ToInt() > 720)
        this.ModelState.AddModelError("ReportInterval", "Must be between 1 and 720");
      if (new Version(gateway.GatewayFirmwareVersion) >= new Version("3.1.0.0") && collection["PollInterval"] != null && (collection["PollInterval"].ToInt() < 0 || collection["PollInterval"].ToInt() > 720))
        this.ModelState.AddModelError("PollInterval", "Must be between 0 and 720");
      if (new Version(gateway.GatewayFirmwareVersion) > new Version("1.0.2.0") && collection["ResetInterval"] != null && (collection["ResetInterval"].ToInt() < 0 || collection["ResetInterval"].ToInt() > 8760))
      {
        this.ModelState.AddModelError("ResetInterval", "Must be between 0 and 8760");
        this.ModelState.AddModelError("", "Commands -> Reset Interval");
      }
      gateway.ResetInterval = string.IsNullOrEmpty(collection["ResetInterval"]) ? 168 : collection["ResetInterval"].ToInt();
      if (!string.IsNullOrEmpty(collection["PollInterval"]))
      {
        if (collection["PollInterval"].ToInt() < 0 || collection["PollInterval"].ToInt() > 720)
        {
          this.ModelState.AddModelError("PollInterval", "Must be between 0 and 720");
          this.ModelState.AddModelError("", "General -> Poll Rate");
        }
        gateway.PollInterval = (double) collection["PollInterval"].ToInt();
      }
      else
        gateway.PollInterval = gatewayType.DefaultPollInterval;
      gateway.ReportInterval = collection["ReportInterval"].ToDouble();
      gateway.ObserveAware = !string.IsNullOrEmpty(collection["ObserveAware"]);
      gateway.GatewayPowerMode = !string.IsNullOrEmpty(collection["GatewayPowerMode"]) ? (eGatewayPowerMode) Enum.Parse(typeof (eGatewayPowerMode), collection["GatewayPowerMode"]) : eGatewayPowerMode.Standard;
      if (this.ModelState.IsValid)
      {
        try
        {
          Account account = Account.Load(csNet.AccountID);
          gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway settings");
          gateway.isEnterpriseHost = false;
          gateway.Save();
          if (gateway.IsDirty)
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__20.\u003C\u003Ep__0 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__20.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__20.\u003C\u003Ep__0.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__20.\u003C\u003Ep__0, this.ViewBag, "Gateway Edit Pending");
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__20.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__20.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__20.\u003C\u003Ep__1.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__20.\u003C\u003Ep__1, this.ViewBag, "Gateway Edit Success");
          }
          // ISSUE: reference to a compiler-generated field
          if (OverviewControllerBase.\u003C\u003Eo__20.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OverviewControllerBase.\u003C\u003Eo__20.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = OverviewControllerBase.\u003C\u003Eo__20.\u003C\u003Ep__2.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__20.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/Overview/GatewayEdit/" + gateway.GatewayID.ToString() : collection["returns"]);
          return "EditConfirmation";
        }
        catch (Exception ex)
        {
          this.ModelState.AddModelError("", ex.Message);
        }
      }
    }
    else
      this.ModelState.AddModelError("", "Invalid Gateway Type");
    return $"GatewayEdit\\type_{gateway.GatewayTypeID.ToString("D3")}\\Edit";
  }

  public string GatewayThirtyThreeEdit(Gateway gateway, FormCollection collection)
  {
    GatewayType gatewayType = GatewayType.Load(gateway.GatewayTypeID);
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    if (gatewayType != null)
    {
      if (string.IsNullOrEmpty(collection["Name"]))
      {
        this.ModelState.AddModelError("Name", "Required");
        this.ModelState.AddModelError("", "General -> Name");
      }
      else
        gateway.Name = collection["Name"];
      if (collection["ReportInterval"].ToInt() < 0 || collection["ReportInterval"].ToInt() > 720)
      {
        this.ModelState.AddModelError("ReportInterval", "Must be between 1 and 720");
        this.ModelState.AddModelError("", "General -> Heartbeat");
      }
      if (new Version(gateway.GatewayFirmwareVersion) >= new Version("3.1.0.0") && collection["PollInterval"] != null && (collection["PollInterval"].ToInt() < 0 || collection["PollInterval"].ToInt() > 720))
      {
        this.ModelState.AddModelError("PollInterval", "Must be between 0 and 720");
        this.ModelState.AddModelError("", "General -> Poll Rate");
      }
      if (new Version(gateway.GatewayFirmwareVersion) > new Version("1.0.2.0") && collection["ResetInterval"] != null && (collection["ResetInterval"].ToInt() < 0 || collection["ResetInterval"].ToInt() > 8760))
      {
        this.ModelState.AddModelError("ResetInterval", "Must be between 0 and 8760");
        this.ModelState.AddModelError("", "Commands -> Reset Interval");
      }
      if (new Version(gateway.GatewayFirmwareVersion) > new Version("1.0.2.0") && collection["SingleQueueExpiration"] != null && collection["SingleQueueExpiration"] != null && (collection["SingleQueueExpiration"].ToInt() < 1 || collection["SingleQueueExpiration"].ToInt() > (int) ushort.MaxValue))
      {
        this.ModelState.AddModelError("SingleQueueExpiration", "Must be between 1 and 65535");
        this.ModelState.AddModelError("", "Commands -> Will Call Expiration");
      }
      gateway.SingleQueueExpiration = string.IsNullOrEmpty(collection["SingleQueueExpiration"]) ? gatewayType.DefaultSingleQueueExpiration : (double) collection["SingleQueueExpiration"].ToInt();
      gateway.ResetInterval = string.IsNullOrEmpty(collection["ResetInterval"]) ? 168 : collection["ResetInterval"].ToInt();
      if (!string.IsNullOrEmpty(collection["PollInterval"]))
      {
        if (collection["PollInterval"].ToInt() < 0 || collection["PollInterval"].ToInt() > 720)
        {
          this.ModelState.AddModelError("PollInterval", "Must be between 0 and 720");
          this.ModelState.AddModelError("", "General -> Poll Rate");
        }
        gateway.PollInterval = (double) collection["PollInterval"].ToInt();
      }
      else
        gateway.PollInterval = gatewayType.DefaultPollInterval;
      gateway.ReportInterval = collection["ReportInterval"].ToDouble();
      gateway.ObserveAware = !string.IsNullOrEmpty(this.Request["ObserveAware"]);
      gateway.DisableNetworkOnServerError = !string.IsNullOrEmpty(this.Request["DisableNetworkOnServerError"]);
      if (collection["DHCPorStatic"] == "static")
      {
        if (string.IsNullOrEmpty(collection["GatewayIP"]) || collection["GatewayIP"] == "0.0.0.0" || !collection["GatewayIP"].IsIPAddress())
        {
          this.ModelState.AddModelError("GatewayIP", "Must be valid IP Address (ie 192.168.0.10)");
          this.ModelState.AddModelError("", "Local Area Network -> IP Address");
        }
        if (string.IsNullOrEmpty(collection["GatewaySubnet"]) || collection["GatewaySubnet"] == "0.0.0.0" || !collection["GatewaySubnet"].IsIPAddress())
        {
          this.ModelState.AddModelError("GatewaySubnet", "Must be valid subnet mask (ie 255.255.255.0)");
          this.ModelState.AddModelError("", "Local Area Network -> Subnet Mask");
        }
        if (string.IsNullOrEmpty(collection["GatewayDNS"]) || collection["GatewayDNS"] == "0.0.0.0" || !collection["GatewayDNS"].IsIPAddress())
        {
          this.ModelState.AddModelError("GatewayDNS", "Must be valid IP Address (ie 192.168.0.1)");
          this.ModelState.AddModelError("", "Local Area Network -> DNS Server");
        }
        if (string.IsNullOrEmpty(collection["DefaultRouterIP"]) || collection["DefaultRouterIP"] == "0.0.0.0" || !collection["DefaultRouterIP"].IsIPAddress())
        {
          this.ModelState.AddModelError("DefaultRouterIP", "Must be valid IP Address (ie 192.168.0.1)");
          this.ModelState.AddModelError("", "Local Area Network -> Default Gateway");
        }
        gateway.GatewayIP = collection["GatewayIP"];
        gateway.GatewaySubnet = collection["GatewaySubnet"];
        gateway.DefaultRouterIP = collection["DefaultRouterIP"];
        gateway.GatewayDNS = collection["GatewayDNS"];
      }
      else
      {
        gateway.GatewayIP = gatewayType.DefaultGatewayIP;
        gateway.GatewaySubnet = gatewayType.DefaultGatewaySubnet;
        gateway.DefaultRouterIP = gatewayType.DefaultRouterIP;
        gateway.GatewayDNS = gatewayType.DefaultGatewayDNS;
      }
      if (!string.IsNullOrEmpty(collection["ModbusInterfaceActive"]))
      {
        if (collection["ModbusInterfaceTimeout"].ToDouble() < 1.0 || collection["ModbusInterfaceTimeout"].ToDouble() > 60.0)
        {
          this.ModelState.AddModelError("ModbusInterfaceTimeout", "between 1 and 60");
          this.ModelState.AddModelError("", "Modbus Interface -> TCP Timeout between 1 and 60");
        }
        if (collection["ModbusInterfacePort"].ToInt() < 1 || collection["ModbusInterfacePort"].ToInt() > (int) ushort.MaxValue)
        {
          this.ModelState.AddModelError("ModbusInterfacePort", "Invalid Port");
          this.ModelState.AddModelError("", "Modbus Interface -> Invalid Port");
        }
        gateway.ModbusInterfaceActive = true;
        gateway.ModbusInterfaceTimeout = collection["ModbusInterfaceTimeout"].ToDouble();
        gateway.ModbusInterfacePort = collection["ModbusInterfacePort"].ToInt();
      }
      else
      {
        gateway.ModbusInterfaceActive = false;
        gateway.ModbusInterfaceTimeout = gatewayType.DefaultModbusInterfaceTimeout;
        gateway.ModbusInterfacePort = gatewayType.DefaultModbusInterfacePort;
      }
      if (!string.IsNullOrEmpty(collection["SNMPInterface1Active"]))
      {
        bool flag = true;
        if (string.IsNullOrEmpty(collection["SNMPInterfaceAddress1"]) || !collection["SNMPInterfaceAddress1"].IsIPAddress())
        {
          flag = false;
          this.ModelState.AddModelError("SNMPInterfaceAddress1", "Invalid IP Address");
          this.ModelState.AddModelError("", "SNMP Interface -> Inbound IP Range Start");
        }
        if (string.IsNullOrEmpty(collection["SNMPInterfaceAddress3"]) || !collection["SNMPInterfaceAddress3"].IsIPAddress())
        {
          flag = false;
          this.ModelState.AddModelError("SNMPInterfaceAddress3", "Invalid IP Address");
          this.ModelState.AddModelError("", "SNMP Interface -> Inbound IP Range End");
        }
        if (flag && new Version(collection["SNMPInterfaceAddress1"]) >= new Version(collection["SNMPInterfaceAddress3"]))
        {
          this.ModelState.AddModelError("SNMPInterfaceAddress3", "Invalid IP Address");
          this.ModelState.AddModelError("", "SNMP Interface -> Inbound IP Range Error");
        }
        if (collection["SNMPInterfacePort1"].ToInt() < 1 || collection["SNMPInterfacePort1"].ToInt() > (int) ushort.MaxValue)
        {
          this.ModelState.AddModelError("SNMPInterfacePort1", "Invalid Port");
          this.ModelState.AddModelError("", "SNMP Interface -> Port");
        }
        if (collection["SNMPCommunityString"].ToStringSafe().Length > 32 /*0x20*/)
        {
          this.ModelState.AddModelError("SNMPCommunityString", "Invalid Community String");
          this.ModelState.AddModelError("", "SNMP Interface -> SNMP Community String");
        }
        if (!string.IsNullOrEmpty(collection["SNMPTrap1Active"]))
        {
          if (string.IsNullOrEmpty(collection["SNMPInterfaceAddress2"]) || !collection["SNMPInterfaceAddress2"].IsIPAddress())
          {
            this.ModelState.AddModelError("SNMPInterfaceAddress2", "Invalid IP Address");
            this.ModelState.AddModelError("", "SNMP Interface -> Trap Address");
          }
          if (!string.IsNullOrEmpty(collection["SNMPTrapPort1"]) && (collection["SNMPTrapPort1"].ToInt() < 1 || collection["SNMPTrapPort1"].ToInt() > (int) ushort.MaxValue))
          {
            this.ModelState.AddModelError("SNMPTrapPort1", "Invalid Port");
            this.ModelState.AddModelError("", "SNMP Interface -> Trap Port");
          }
          gateway.SNMPTrap1Active = true;
          gateway.SNMPInterfaceAddress2 = collection["SNMPInterfaceAddress2"];
          gateway.SNMPTrapPort1 = collection["SNMPTrapPort1"].ToInt();
          gateway.SNMPTrap2Active = !string.IsNullOrEmpty(collection["SNMPTrap2Active"]);
          gateway.SNMPTrap3Active = !string.IsNullOrEmpty(collection["SNMPTrap3Active"]);
          gateway.SNMPTrap4Active = !string.IsNullOrEmpty(collection["SNMPTrap4Active"]);
        }
        else
        {
          gateway.SNMPInterfaceAddress2 = "0.0.0.0";
          gateway.SNMPTrapPort1 = gatewayType.DefaultSNMPTrapPort;
          gateway.SNMPTrap1Active = false;
          gateway.SNMPTrap2Active = false;
          gateway.SNMPTrap3Active = false;
          gateway.SNMPTrap4Active = false;
        }
        gateway.SNMPInterface1Active = true;
        gateway.SNMPInterfaceAddress1 = collection["SNMPInterfaceAddress1"];
        gateway.SNMPInterfaceAddress3 = collection["SNMPInterfaceAddress3"];
        gateway.SNMPInterfacePort1 = collection["SNMPInterfacePort1"].ToInt();
        gateway.SNMPCommunityString = collection["SNMPCommunityString"].ToStringSafe();
      }
      else
      {
        gateway.SNMPInterface1Active = false;
        gateway.SNMPInterfaceAddress1 = "0.0.0.0";
        gateway.SNMPInterfaceAddress3 = "255.255.255.255";
        gateway.SNMPInterfacePort1 = gatewayType.DefaultSNMPInterfacePort;
        gateway.SNMPInterfaceAddress2 = "0.0.0.0";
        gateway.SNMPTrap1Active = false;
        gateway.SNMPTrap2Active = false;
        gateway.SNMPTrap3Active = false;
        gateway.SNMPTrap4Active = false;
        gateway.SNMPTrapPort1 = gatewayType.DefaultSNMPTrapPort;
        gateway.SNMPCommunityString = string.Empty;
      }
      if (!string.IsNullOrEmpty(collection["NTPInterfaceActive"]))
      {
        if (string.IsNullOrEmpty(collection["NTPServerIP"]) || !collection["NTPServerIP"].IsIPAddress())
        {
          this.ModelState.AddModelError("NTPServerIP", "Invalid IP Address");
          this.ModelState.AddModelError("", "SNTP Interface -> SNTP Server IP Address Invalid");
        }
        if (collection["NTPMinSampleRate"].ToDouble() == 0.0 || collection["NTPMinSampleRate"].ToDouble() * 60.0 > (double) ushort.MaxValue)
        {
          this.ModelState.AddModelError("NTPMinSampleRate", "Invalid Update Interval");
          this.ModelState.AddModelError("", "NTP Interface -> Update Interval Invalid");
        }
        gateway.NTPInterfaceActive = true;
        gateway.NTPServerIP = collection["NTPServerIP"];
        gateway.NTPMinSampleRate = collection["NTPMinSampleRate"].ToDouble();
      }
      else
      {
        gateway.NTPInterfaceActive = false;
        gateway.NTPServerIP = "0.0.0.0";
        gateway.NTPMinSampleRate = 30.0;
      }
      if (!string.IsNullOrEmpty(collection["HTTPInterfaceActive"]))
      {
        if (collection["HTTPServiceTimeout"].ToDouble() < 0.0 || collection["HTTPServiceTimeout"].ToDouble() * 60.0 > (double) ushort.MaxValue)
        {
          this.ModelState.AddModelError("HTTPServiceTimeout", "Invalid Timeout");
          this.ModelState.AddModelError("", "HTTP Interface -> Service Timeout");
        }
        gateway.HTTPInterfaceActive = true;
        gateway.HTTPServiceTimeout = collection["HTTPServiceTimeout"].ToDouble();
      }
      else
      {
        gateway.HTTPInterfaceActive = false;
        gateway.HTTPServiceTimeout = 5.0;
      }
      gateway.BacnetInterfaceActive = !string.IsNullOrEmpty(collection["BacnetInterfaceActive"]);
      if (this.ModelState.IsValid)
      {
        try
        {
          Account account = Account.Load(csNet.AccountID);
          gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway settings");
          if (gateway.isEnterpriseHost)
          {
            gateway.ServerHostAddress = gatewayType.DefaultServerHostAddress;
            gateway.ServerHostAddress2 = gatewayType.DefaultServerHostAddress2;
            gateway.Port = gatewayType.DefaultPort;
            gateway.Port2 = gatewayType.DefaultPort2;
            gateway.isEnterpriseHost = false;
          }
          gateway.Save();
          if (gateway.IsDirty)
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__21.\u003C\u003Ep__0 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__21.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__21.\u003C\u003Ep__0.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__21.\u003C\u003Ep__0, this.ViewBag, "Gateway Edit Pending");
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__21.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__21.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__21.\u003C\u003Ep__1.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__21.\u003C\u003Ep__1, this.ViewBag, "Gateway Edit Success");
          }
          // ISSUE: reference to a compiler-generated field
          if (OverviewControllerBase.\u003C\u003Eo__21.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OverviewControllerBase.\u003C\u003Eo__21.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = OverviewControllerBase.\u003C\u003Eo__21.\u003C\u003Ep__2.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__21.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/Overview/GatewayEdit/" + gateway.GatewayID.ToString() : collection["returns"]);
          return "EditConfirmation";
        }
        catch (Exception ex)
        {
          this.ModelState.AddModelError("", ex.Message);
        }
      }
    }
    else
      this.ModelState.AddModelError("", "Invalid Gateway Type");
    return $"GatewayEdit\\type_{gateway.GatewayTypeID.ToString("D3")}\\Edit";
  }

  public string GatewayThirtySixEdit(Gateway gateway, FormCollection collection)
  {
    GatewayType gatewayType = GatewayType.Load(gateway.GatewayTypeID);
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    if (gatewayType != null)
    {
      if (string.IsNullOrEmpty(collection["Name"]))
      {
        this.ModelState.AddModelError("Name", "Required");
        this.ModelState.AddModelError("", "General -> Name");
      }
      else
        gateway.Name = collection["Name"];
      if (collection["ReportInterval"].ToInt() < 0 || collection["ReportInterval"].ToInt() > 720)
      {
        this.ModelState.AddModelError("ReportInterval", "Must be between 1 and 720");
        this.ModelState.AddModelError("", "General -> Heartbeat");
      }
      gateway.ReportInterval = collection["ReportInterval"].ToDouble();
      gateway.GatewayCommunicationPreference = (eGatewayCommunicationPreference) collection["GatewayCommunicationPreference"].ToInt();
      gateway.ObserveAware = !string.IsNullOrEmpty(this.Request["ObserveAware"]);
      gateway.DisableNetworkOnServerError = !string.IsNullOrEmpty(this.Request["DisableNetworkOnServerError"]);
      gateway.GatewayPowerMode = !string.IsNullOrEmpty(collection["GatewayPowerMode"]) ? (eGatewayPowerMode) Enum.Parse(typeof (eGatewayPowerMode), collection["GatewayPowerMode"]) : eGatewayPowerMode.Standard;
      if (!string.IsNullOrEmpty(this.Request["GPSReportInterval"]))
      {
        if (collection["GPSReportInterval"].ToInt() < 0 || collection["GPSReportInterval"].ToInt() > 720)
        {
          this.ModelState.AddModelError("ReportInterval", "Must be between 1 and 720");
          this.ModelState.AddModelError("", "General -> Heartbeat");
        }
        gateway.GPSReportInterval = collection["GPSReportInterval"].ToDouble();
      }
      else
        gateway.GPSReportInterval = 0.0;
      if (!string.IsNullOrEmpty(collection["PollInterval"]))
      {
        if (collection["PollInterval"].ToInt() < 0 || collection["PollInterval"].ToInt() > 720)
        {
          this.ModelState.AddModelError("PollInterval", "Must be between 0 and 720");
          this.ModelState.AddModelError("", "General -> Poll Rate");
        }
        gateway.PollInterval = (double) collection["PollInterval"].ToInt();
      }
      else
        gateway.PollInterval = gatewayType.DefaultPollInterval;
      if (collection["DHCPorStatic"] == "static")
      {
        if (string.IsNullOrEmpty(collection["GatewayIP"]) || collection["GatewayIP"] == "0.0.0.0" || !collection["GatewayIP"].IsIPAddress())
        {
          this.ModelState.AddModelError("GatewayIP", "Must be valid IP Address (ie 192.168.0.10)");
          this.ModelState.AddModelError("", "Local Area Network -> IP Address");
        }
        if (string.IsNullOrEmpty(collection["GatewaySubnet"]) || collection["GatewaySubnet"] == "0.0.0.0" || !collection["GatewaySubnet"].IsIPAddress())
        {
          this.ModelState.AddModelError("GatewaySubnet", "Must be valid subnet mask (ie 255.255.255.0)");
          this.ModelState.AddModelError("", "Local Area Network -> Subnet Mask");
        }
        if (string.IsNullOrEmpty(collection["GatewayDNS"]) || collection["GatewayDNS"] == "0.0.0.0" || !collection["GatewayDNS"].IsIPAddress())
        {
          this.ModelState.AddModelError("GatewayDNS", "Must be valid IP Address (ie 192.168.0.1)");
          this.ModelState.AddModelError("", "Local Area Network -> DNS Server");
        }
        if (string.IsNullOrEmpty(collection["DefaultRouterIP"]) || collection["DefaultRouterIP"] == "0.0.0.0" || !collection["DefaultRouterIP"].IsIPAddress())
        {
          this.ModelState.AddModelError("DefaultRouterIP", "Must be valid IP Address (ie 192.168.0.1)");
          this.ModelState.AddModelError("", "Local Area Network -> Default Gateway");
        }
        gateway.GatewayIP = collection["GatewayIP"];
        gateway.GatewaySubnet = collection["GatewaySubnet"];
        gateway.DefaultRouterIP = collection["DefaultRouterIP"];
        gateway.GatewayDNS = collection["GatewayDNS"];
      }
      else
      {
        gateway.GatewayIP = gatewayType.DefaultGatewayIP;
        gateway.GatewaySubnet = gatewayType.DefaultGatewaySubnet;
        gateway.DefaultRouterIP = gatewayType.DefaultRouterIP;
        gateway.GatewayDNS = gatewayType.DefaultGatewayDNS;
      }
      gateway.UMNOProf = collection["UMNOProf"].ToInt();
      if (gateway.UMNOProf == 100)
      {
        gateway.CellAPNName = collection["CellAPNName"];
        gateway.SIMAuthType = collection["SIMAuthType"].ToInt();
        gateway.Username = collection["Username"];
        gateway.Password = collection["Password"];
        gateway.M1BandMask = (long) collection["M1BandMask"].ToInt();
        gateway.NB1BandMask = (long) collection["NB1BandMask"].ToInt();
      }
      else
      {
        gateway.CellAPNName = "";
        gateway.SIMAuthType = 0;
        gateway.Username = "";
        gateway.Password = "";
        gateway.M1BandMask = 0L;
        gateway.NB1BandMask = 0L;
      }
      if (collection["ResetInterval"] != null && (collection["ResetInterval"].ToInt() < 0 || collection["ResetInterval"].ToInt() > 8760))
      {
        this.ModelState.AddModelError("ResetInterval", "Must be between 0 and 8760");
        this.ModelState.AddModelError("", "Commands -> Reset Interval");
      }
      gateway.ResetInterval = string.IsNullOrEmpty(collection["ResetInterval"]) ? 168 : collection["ResetInterval"].ToInt();
      gateway.HTTPInterfaceActive = collection["HTTPInterfaceActive"].ToBool();
      gateway.HTTPServiceTimeout = (double) collection["HTTPServiceTimeout"].ToInt();
      gateway.SNMPInterfaceAddress2 = "0.0.0.0";
      gateway.SNMPInterfaceAddress3 = "255.255.255.255";
      gateway.NTPServerIP = "0.0.0.0";
      gateway.NTPMinSampleRate = 10.0;
      if (this.ModelState.IsValid)
      {
        try
        {
          Account account = Account.Load(csNet.AccountID);
          gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway settings");
          if (gateway.isEnterpriseHost)
          {
            gateway.ServerHostAddress = gatewayType.DefaultServerHostAddress;
            gateway.ServerHostAddress2 = gatewayType.DefaultServerHostAddress2;
            gateway.Port = gatewayType.DefaultPort;
            gateway.Port2 = gatewayType.DefaultPort2;
            gateway.isEnterpriseHost = false;
          }
          gateway.Save();
          if (gateway.IsDirty)
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__22.\u003C\u003Ep__0 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__22.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__22.\u003C\u003Ep__0.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__22.\u003C\u003Ep__0, this.ViewBag, "Gateway Edit Pending");
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (OverviewControllerBase.\u003C\u003Eo__22.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              OverviewControllerBase.\u003C\u003Eo__22.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = OverviewControllerBase.\u003C\u003Eo__22.\u003C\u003Ep__1.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__22.\u003C\u003Ep__1, this.ViewBag, "Gateway Edit Success");
          }
          // ISSUE: reference to a compiler-generated field
          if (OverviewControllerBase.\u003C\u003Eo__22.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OverviewControllerBase.\u003C\u003Eo__22.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewControllerBase), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = OverviewControllerBase.\u003C\u003Eo__22.\u003C\u003Ep__2.Target((CallSite) OverviewControllerBase.\u003C\u003Eo__22.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/Overview/GatewayEdit/" + gateway.GatewayID.ToString() : collection["returns"]);
          return "EditConfirmation";
        }
        catch (Exception ex)
        {
          this.ModelState.AddModelError("", ex.Message);
        }
      }
    }
    else
      this.ModelState.AddModelError("", "Invalid Gateway Type");
    return $"GatewayEdit\\type_{gateway.GatewayTypeID.ToString("D3")}\\Edit";
  }
}
