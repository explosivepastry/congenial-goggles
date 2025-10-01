// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.SensorController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.ControllerBase;
using iMonnit.Models;
using Microsoft.CSharp.RuntimeBinder;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

[NoCache]
[HandleError]
public class SensorController : SensorControllerBase
{
  [NoCache]
  [Authorize]
  public ActionResult DetailsSmallOneview(long id)
  {
    Sensor model = Sensor.Load(id);
    string str = $"ApplicationSpecific\\{model.ApplicationID}\\DetailsSmallOneview";
    return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "Sensor", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) model) : (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult Edit(long id)
  {
    if (MonnitSession.AccountCan("sensor_advanced_edit") && MonnitSession.CustomerCan("Sensor_Advanced_Configuration"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "AdvancedEdit",
        controller = "Sensor",
        id = id
      });
    Sensor.ClearCache(id);
    Sensor model = Sensor.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    string str = $"ApplicationSpecific\\{model.ApplicationID}\\Edit";
    if (MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "Sensor", MonnitSession.CurrentTheme.Theme))
    {
      // ISSUE: reference to a compiler-generated field
      if (SensorController.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SensorController.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SensorController.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) SensorController.\u003C\u003Eo__1.\u003C\u003Ep__0, this.ViewBag, str);
      return (ActionResult) this.PartialView(str, (object) model);
    }
    // ISSUE: reference to a compiler-generated field
    if (SensorController.\u003C\u003Eo__1.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SensorController.\u003C\u003Eo__1.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SensorController.\u003C\u003Eo__1.\u003C\u003Ep__1.Target((CallSite) SensorController.\u003C\u003Eo__1.\u003C\u003Ep__1, this.ViewBag, nameof (Edit));
    return (ActionResult) this.PartialView((object) model);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Edit(long id, FormCollection collection)
  {
    Sensor.ClearCache(id);
    Sensor sensor = Sensor.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    Gateway gateway = (Gateway) null;
    bool generalConfigDirty = sensor.GeneralConfigDirty;
    bool profileConfigDirty = sensor.ProfileConfigDirty;
    try
    {
      if (!sensor.IsActive)
      {
        sensor.MarkClean(false);
        sensor.IsActive = true;
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
      if (sensor.SensorTypeID == 4L || sensor.SensorTypeID == 8L)
        gateway = this.SaveWIFISettings(sensor, collection);
      this.UpdateModel<Sensor>(sensor);
      double num = MonnitSession.CurrentCustomer.Account.MinHeartBeat();
      double result = 0.0;
      foreach (string allKey in collection.AllKeys)
      {
        if (allKey == "ReportInterval" && !double.TryParse(collection["ReportInterval"], out result))
        {
          collection["ReportInterval"] = num.ToString();
          sensor.ReportInterval = num;
        }
        if (allKey == "ActiveStateInterval" && !double.TryParse(collection["ActiveStateInterval"], out result))
        {
          collection["ActiveStateInterval"] = num.ToString();
          sensor.ActiveStateInterval = num;
        }
      }
      if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Heartbeat_Restriction")) && sensor.ReportInterval < num)
        sensor.ReportInterval = num;
      if (!MonnitApplicationBase.CustomApplicationValues(sensor, "IgnoreActiveStateInSimpleEditScreen").ToBool())
        sensor.ActiveStateInterval = sensor.ReportInterval;
      NameValueCollection returnValues;
      MonnitApplicationBase.SetProfileSettings(sensor, (NameValueCollection) collection, out returnValues);
      foreach (string key in returnValues.Keys)
        this.ViewData[key] = (object) returnValues[key];
      if (this.ModelState.IsValid)
      {
        sensor.Save();
        if (sensor.IsDirty || gateway != null && gateway.IsDirty)
        {
          // ISSUE: reference to a compiler-generated field
          if (SensorController.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            SensorController.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = SensorController.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) SensorController.\u003C\u003Eo__2.\u003C\u003Ep__0, this.ViewBag, "Sensor Edit Pending");
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if (SensorController.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            SensorController.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = SensorController.\u003C\u003Eo__2.\u003C\u003Ep__1.Target((CallSite) SensorController.\u003C\u003Eo__2.\u003C\u003Ep__1, this.ViewBag, "Sensor Edit Success");
        }
        // ISSUE: reference to a compiler-generated field
        if (SensorController.\u003C\u003Eo__2.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SensorController.\u003C\u003Eo__2.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = SensorController.\u003C\u003Eo__2.\u003C\u003Ep__2.Target((CallSite) SensorController.\u003C\u003Eo__2.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/Sensor/Edit/" + sensor.SensorID.ToString() : collection["returns"]);
        return (ActionResult) this.PartialView("EditConfirmation");
      }
      sensor.GeneralConfigDirty = generalConfigDirty;
      sensor.ProfileConfigDirty = profileConfigDirty;
    }
    catch (Exception ex)
    {
      this.ModelState.AddModelError("", "Error Saving Sensor");
      ex.Log("SensorController.Edit SensorID:" + id.ToString());
      sensor.GeneralConfigDirty = generalConfigDirty;
      sensor.ProfileConfigDirty = profileConfigDirty;
    }
    string str = $"ApplicationSpecific\\{sensor.ApplicationID}\\Edit";
    return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "Sensor", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) sensor) : (ActionResult) this.PartialView((object) sensor);
  }

  [NoCache]
  [Authorize]
  public ActionResult AdvancedEdit(long id)
  {
    Sensor.ClearCache(id);
    Sensor sensor = Sensor.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    this.AddTimeDropDowns(sensor);
    string str = $"ApplicationSpecific\\{sensor.ApplicationID}\\AdvancedEdit";
    return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "Sensor", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) sensor) : (ActionResult) this.PartialView((object) sensor);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult AdvancedEdit(long id, FormCollection collection)
  {
    Sensor.ClearCache(id);
    Sensor sensor = Sensor.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    Gateway gateway = (Gateway) null;
    bool generalConfigDirty = sensor.GeneralConfigDirty;
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
          int datumindex = !string.IsNullOrEmpty(collection[allKey]) ? Convert.ToInt32(allKey.ToLower().Replace("datumname", "")) : throw new Exception($"Data Names cannot be blank.  SensorID = {sensor.SensorID}, Key = {allKey}");
          sensor.SetDatumName(datumindex, collection[allKey].ToStringSafe());
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
      if (new Version(sensor.FirmwareVersion) >= new Version("1.2.009") && sensor.GenerationType.ToUpper().Contains("GEN1"))
      {
        TimeSpan startTime = this.GetStartTime(collection).Subtract(MonnitSession.UTCOffset);
        if (startTime.TotalMinutes < 0.0)
          startTime = startTime.Add(new TimeSpan(1, 0, 0, 0));
        if (startTime.Days > 0)
          startTime = startTime.Subtract(new TimeSpan(1, 0, 0, 0));
        TimeSpan endTime = this.GetEndTime(collection).Subtract(MonnitSession.UTCOffset);
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
      if (sensor.Recovery > 10)
        sensor.Recovery = 10;
      double num = MonnitSession.CurrentCustomer.Account.MinHeartBeat();
      if (sensor.ReportInterval < num)
        sensor.ReportInterval = num;
      if (sensor.ActiveStateInterval < num)
        sensor.ActiveStateInterval = num;
      if (sensor.SensorTypeID == 4L || sensor.SensorTypeID == 8L && !string.IsNullOrEmpty(collection["GatewayIP"]))
      {
        if (collection["WIFISecurityMode"].Equals("WEP") && collection["PassPhrase1"].Length != 10 && collection["PassPhrase1"].Length != 26)
        {
          if (collection["PassPhrase1"].Length == 5 || collection["PassPhrase1"].Length == 13)
          {
            char[] charArray = collection["PassPhrase1"].ToStringSafe().ToCharArray();
            string str = "";
            foreach (char ch in charArray)
            {
              int int32 = Convert.ToInt32(ch);
              str += $"{int32:X}";
            }
            collection["PassPhrase1"] = str;
          }
          else
            this.ModelState.AddModelError("PassPhrase1", "Security type is WEP. and the passphrase is incorrect try retyping it. Wep Keys are generally 10 or 26 alphanumeric characters long i.e. (A-F, 0-9), but on occasion they can be a 5 or 13 letter passphrase.");
        }
        if (collection["WIFISecurityMode2"].Equals("WEP") && collection["PassPhrase2"].Length != 10 && collection["PassPhrase2"].Length != 26)
        {
          if (collection["PassPhrase2"].Length == 5 || collection["PassPhrase2"].Length == 13)
          {
            char[] charArray = collection["PassPhrase2"].ToStringSafe().ToCharArray();
            string str = "";
            foreach (char ch in charArray)
            {
              int int32 = Convert.ToInt32(ch);
              str += $"{int32:X}";
            }
            collection["PassPhrase2"] = str;
          }
          else
            this.ModelState.AddModelError("PassPhrase2", "Security type is WEP. and the passphrase is incorrect try retyping it. Wep Keys are generally 10 or 26 alphanumeric characters long i.e. (A-F, 0-9), but on occasion they can be a 5 or 13 letter passphrase.");
        }
        if (collection["WIFISecurityMode3"].Equals("WEP") && collection["PassPhrase3"].Length != 10 && collection["PassPhrase3"].Length != 26)
        {
          if (collection["PassPhrase3"].Length == 5 || collection["PassPhrase3"].Length == 13)
          {
            char[] charArray = collection["PassPhrase3"].ToStringSafe().ToCharArray();
            string str = "";
            foreach (char ch in charArray)
            {
              int int32 = Convert.ToInt32(ch);
              str += $"{int32:X}";
            }
            collection["PassPhrase3"] = str;
          }
          else
            this.ModelState.AddModelError("PassPhrase3", "Security type is WEP. and the passphrase is incorrect try retyping it. Wep Keys are generally 10 or 26 alphanumeric characters long i.e. (A-F, 0-9), but on occasion they can be a 5 or 13 letter passphrase.");
        }
        gateway = this.SaveWIFISettings(sensor, collection);
      }
      NameValueCollection returnValues;
      MonnitApplicationBase.SetProfileSettings(sensor, (NameValueCollection) collection, out returnValues);
      foreach (string key in returnValues.Keys)
        this.ViewData[key] = (object) returnValues[key];
      if ((sensor.MinimumThreshold == (long) uint.MaxValue || sensor.MaximumThreshold == (long) uint.MaxValue) && sensor.MeasurementsPerTransmission > 1)
      {
        sensor.SetDefaultCalibration();
        this.ModelState.AddModelError("MeasurementsPerTransmission", "Multiple measurements not supported without thresholds set.");
      }
      if (this.ModelState.IsValid)
      {
        Account account = Account.Load(sensor.AccountID);
        sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited advanced sensor settings");
        sensor.Save();
        if (sensor.IsDirty)
        {
          // ISSUE: reference to a compiler-generated field
          if (SensorController.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            SensorController.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = SensorController.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) SensorController.\u003C\u003Eo__4.\u003C\u003Ep__0, this.ViewBag, "Sensor Edit Pending");
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if (SensorController.\u003C\u003Eo__4.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            SensorController.\u003C\u003Eo__4.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = SensorController.\u003C\u003Eo__4.\u003C\u003Ep__1.Target((CallSite) SensorController.\u003C\u003Eo__4.\u003C\u003Ep__1, this.ViewBag, "Sensor Edit Success");
        }
        // ISSUE: reference to a compiler-generated field
        if (SensorController.\u003C\u003Eo__4.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SensorController.\u003C\u003Eo__4.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = SensorController.\u003C\u003Eo__4.\u003C\u003Ep__2.Target((CallSite) SensorController.\u003C\u003Eo__4.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/Sensor/AdvancedEdit/" + sensor.SensorID.ToString() : collection["returns"]);
        return (ActionResult) this.PartialView("EditConfirmation");
      }
      this.AddTimeDropDowns(sensor);
      sensor.GeneralConfigDirty = generalConfigDirty;
      sensor.ProfileConfigDirty = profileConfigDirty;
    }
    catch (Exception ex)
    {
      this.AddTimeDropDowns(sensor);
      this.ModelState.AddModelError("", ex.Message);
      ex.Log("SensorController.AdvancedEdit");
      sensor.GeneralConfigDirty = generalConfigDirty;
      sensor.ProfileConfigDirty = profileConfigDirty;
    }
    sensor.GeneralConfigDirty = generalConfigDirty;
    sensor.GeneralConfig2Dirty = generalConfig2Dirty;
    sensor.ProfileConfigDirty = profileConfigDirty;
    sensor.ProfileConfig2Dirty = profileConfig2Dirty;
    string str1 = $"ApplicationSpecific\\{sensor.ApplicationID}\\AdvancedEdit";
    return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str1, "Sensor", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str1, (object) sensor) : (ActionResult) this.PartialView((object) sensor);
  }

  [Authorize]
  public ActionResult Default(long id)
  {
    try
    {
      Sensor DBObject = Sensor.Load(id);
      Account account = Account.Load(DBObject.AccountID);
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Set sensor settings to default");
      DBObject.SetDefaults(false);
      DBObject.Save();
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [Authorize]
  public ActionResult CustomDefault(long id, long customCompanyID)
  {
    try
    {
      Sensor sensor = Sensor.Load(id);
      sensor.SetCustomCompanyDefaults(false);
      sensor.Save();
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log(ex.Message ?? "");
      return (ActionResult) this.Content("Failed");
    }
  }

  [Authorize]
  public ActionResult SetDefaultCalibration(long id, string url)
  {
    try
    {
      Sensor DBObject = Sensor.Load(id);
      Account account = Account.Load(DBObject.AccountID);
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Sensor calibration reset to defaults");
      DBObject.SetDefaultCalibration();
      DBObject.Save();
      // ISSUE: reference to a compiler-generated field
      if (SensorController.\u003C\u003Eo__7.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SensorController.\u003C\u003Eo__7.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SensorController.\u003C\u003Eo__7.\u003C\u003Ep__0.Target((CallSite) SensorController.\u003C\u003Eo__7.\u003C\u003Ep__0, this.ViewBag, "Sensor Calibration Reset to Defaults Pending");
      // ISSUE: reference to a compiler-generated field
      if (SensorController.\u003C\u003Eo__7.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SensorController.\u003C\u003Eo__7.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SensorController.\u003C\u003Eo__7.\u003C\u003Ep__1.Target((CallSite) SensorController.\u003C\u003Eo__7.\u003C\u003Ep__1, this.ViewBag, string.IsNullOrWhiteSpace(url) || !this.IsLocalUrl(url) ? (MonnitSession.AccountCan("sensor_calibrate") ? "/Sensor/Calibrate/" + DBObject.SensorID.ToString() : "/Sensor/Edit/" + DBObject.SensorID.ToString()) : url);
      return (ActionResult) this.PartialView("EditConfirmation");
    }
    catch
    {
      return (ActionResult) this.Json((object) new
      {
        success = false,
        response = "Failed"
      });
    }
  }

  [Authorize]
  public ActionResult MultiEdit(long id)
  {
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    return !MonnitSession.AccountCan("sensor_multiple_edit") ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index"
    }) : (ActionResult) this.View((object) Sensor.Load(id));
  }

  [Authorize]
  public ActionResult Tags(long id)
  {
    Sensor model = Sensor.Load(id);
    return !MonnitSession.IsAuthorizedForAccount(model.AccountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index"
    }) : (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult AddTag(long id, string tag)
  {
    Sensor model = Sensor.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    model.AddTag(tag);
    return (ActionResult) this.View("Tags", (object) model);
  }

  [Authorize]
  public ActionResult RemoveTag(long id, string tag)
  {
    Sensor model = Sensor.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    model.RemoveTag(tag);
    return (ActionResult) this.View("Tags", (object) model);
  }

  [Authorize]
  public ActionResult Remove(long id)
  {
    Sensor sensor = Sensor.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    if (!MonnitSession.CustomerCan("Network_Edit"))
      return (ActionResult) this.Content("Unauthorized to remove sensors.");
    try
    {
      long id1 = ConfigData.AppSettings("DefaultCSNetID").ToLong();
      Account account = Account.Load(sensor.AccountID);
      sensor.LogAuditData(eAuditAction.Delete, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Removed sensor");
      if (CSNetController.TryMoveSensor(id1, sensor.SensorID))
      {
        sensor.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
        sensor.Save();
        sensor.ResetLastCommunicationDate();
        Gateway gateway = Gateway.LoadBySensorID(sensor.SensorID);
        if (gateway != null)
        {
          gateway.ResetToDefault(false);
          gateway.Save();
        }
        if (MonnitSession.IsSensorFavorite(id))
        {
          CustomerFavorite customerFavorite = MonnitSession.CurrentCustomerFavorites.AllFavorites.Find((Predicate<CustomerFavorite>) (x => x.SensorID == id));
          MonnitSession.CurrentCustomerFavorites.Invalidate();
          customerFavorite.Delete();
          foreach (Notification notification in Notification.LoadBySensorID(sensor.SensorID))
            notification.RemoveSensor(sensor);
          MonnitSession.AccountSensorTotal = int.MinValue;
          return (ActionResult) this.Content("Success");
        }
        foreach (Notification notification in Notification.LoadBySensorID(sensor.SensorID))
          notification.RemoveSensor(sensor);
        MonnitSession.AccountSensorTotal = int.MinValue;
        return (ActionResult) this.Content("Success");
      }
    }
    catch
    {
    }
    return (ActionResult) this.Content("Unable to remove sensor.");
  }

  [AuthorizeDefault]
  public ActionResult RemoveFromAllNotifications(long id)
  {
    Sensor sensor = Sensor.Load(id);
    if (sensor == null)
      return (ActionResult) this.Content("Unkown SensorID");
    if (!MonnitSession.CurrentCustomer.CanSeeSensor(id))
      return (ActionResult) this.Content(ThemeController.PermissionErrorMessage.MissingCustomerPermission("CanSeeSensor SensorID " + id.ToString()));
    foreach (Notification notification in Notification.LoadBySensorID(sensor.SensorID))
      notification.RemoveSensor(sensor);
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult DataMessageNote(Guid id, string note)
  {
    if (!MonnitSession.CustomerCan("Sensor_View_History"))
      return (ActionResult) this.Content("Unauthorized");
    DataMessage dataMessage = DataMessage.LoadByGuid(id);
    if (dataMessage != null)
    {
      Sensor sensor = Sensor.Load(dataMessage.SensorID);
      if (sensor != null && MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
        return (ActionResult) this.Content("Success");
    }
    return (ActionResult) this.Content("Invalid Message");
  }

  [Authorize]
  public ActionResult DataMessageNoteLog(Guid id)
  {
    if (!MonnitSession.CustomerCan("Sensor_View_History"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    DataMessage dataMessage = DataMessage.LoadByGuid(id);
    if (dataMessage != null)
    {
      Sensor sensor = Sensor.Load(dataMessage.SensorID);
      if (sensor != null && MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
        return (ActionResult) this.PartialView((object) Monnit.DataMessageNote.LoadByDataMessageGUID(id));
    }
    return (ActionResult) this.Content("Unauthorized");
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult DataMessageNoteLog(Guid id, string note)
  {
    if (!MonnitSession.CustomerCan("Sensor_View_History"))
      return (ActionResult) this.Content("Unauthorized");
    if (note.HasScriptTag())
      return (ActionResult) this.Content("Failed: Notes may not contain script tags.");
    DataMessage dataMessage = DataMessage.LoadByGuid(id);
    if (dataMessage != null)
    {
      Sensor sensor = Sensor.Load(dataMessage.SensorID);
      if (sensor != null && MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      {
        Monnit.DataMessageNote dataMessageNote = new Monnit.DataMessageNote();
        dataMessageNote.Note = note;
        if (string.IsNullOrWhiteSpace(note))
          return (ActionResult) this.Content("Success");
        dataMessageNote.DataMessageGUID = id;
        dataMessageNote.CustomerID = MonnitSession.CurrentCustomer.CustomerID;
        dataMessageNote.NoteDate = DateTime.UtcNow;
        dataMessageNote.SensorID = dataMessage.SensorID;
        dataMessageNote.MessageDate = dataMessage.MessageDate;
        dataMessageNote.Save();
        if (!dataMessage.HasNote)
        {
          dataMessage.HasNote = true;
          dataMessage.Save();
        }
        return (ActionResult) this.Content("Success");
      }
    }
    return (ActionResult) this.Content("Unable to Save Note!");
  }

  [Authorize]
  public ActionResult Notify(long id)
  {
    Sensor model = Sensor.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID) || !MonnitSession.CustomerCan("Sensor_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    string str = $"ApplicationSpecific\\{model.ApplicationID}\\Notify";
    return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "Sensor", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) model) : (ActionResult) this.PartialView((object) model);
  }

  [Authorize]
  public ActionResult DeleteNoteHistoryLog(long id)
  {
    if (MonnitSession.CustomerCan("Sensor_View_History"))
    {
      try
      {
        Monnit.DataMessageNote DBObject = Monnit.DataMessageNote.Load(id);
        if (DBObject != null)
        {
          DataMessage dataMessage = DataMessage.LoadByGuid(DBObject.DataMessageGUID);
          if (dataMessage == null)
            return (ActionResult) this.Content("Failed");
          Sensor sensor = Sensor.Load(dataMessage.SensorID);
          if (sensor == null)
            return (ActionResult) this.Content("Failed");
          if (!MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
            return (ActionResult) this.Content("Unauthorized");
          DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.DataMessageNote, MonnitSession.CurrentCustomer.CustomerID, sensor.AccountID, "Deleted DataMessage Note");
          DBObject.Delete();
          if (Monnit.DataMessageNote.LoadByDataMessageGUID(DBObject.DataMessageGUID).Count == 0 && dataMessage != null)
          {
            dataMessage.HasNote = false;
            dataMessage.Save();
          }
          return (ActionResult) this.Content("Success");
        }
      }
      catch (Exception ex)
      {
        ex.Log($"DataMessageNote.Delete ID: {id.ToString()} unable to delete note  ");
      }
    }
    return (ActionResult) this.Content("Failed");
  }

  [Authorize]
  public ActionResult SensorList(long id, long NotiID, string q)
  {
    List<NotifySensorDataModel> sensorList = new ConfigureNotifierSensorDataModel(Sensor.Load(NotiID)).SensorList;
    if (sensorList.Count == 0)
      return (ActionResult) this.Content("No sensors found for this account.");
    List<NotifySensorDataModel> source = sensorList.Where<NotifySensorDataModel>((System.Func<NotifySensorDataModel, bool>) (c => c.sens.SensorName.ToLower().Contains(q.ToLower()) || c.sens.SensorID.ToString().ToLower().Contains(q.ToLower()) || c.sens.SensorType.Name.ToLower().Contains(q.ToLower()))).ToList<NotifySensorDataModel>();
    if (source.Count == 0 && q.Length > 0)
      source = sensorList;
    string viewName = $"ApplicationSpecific\\{13}\\SensorList";
    // ISSUE: reference to a compiler-generated field
    if (SensorController.\u003C\u003Eo__19.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SensorController.\u003C\u003Eo__19.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<NotifySensorDataModel>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiSensorlist", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SensorController.\u003C\u003Eo__19.\u003C\u003Ep__0.Target((CallSite) SensorController.\u003C\u003Eo__19.\u003C\u003Ep__0, this.ViewBag, source);
    return (ActionResult) this.PartialView(viewName, (object) source.OrderByDescending<NotifySensorDataModel, bool>((System.Func<NotifySensorDataModel, bool>) (passData => passData.isSendingSensorData)));
  }

  [Authorize]
  public ActionResult ToggleSensor(long id, long notiferID, bool add)
  {
    try
    {
      Sensor sensor = Sensor.Load(id);
      if (add)
      {
        if (sensor == null)
          return (ActionResult) this.Content("Sensor not found");
        if (sensor.AccountID != MonnitSession.CurrentCustomer.AccountID)
          return (ActionResult) this.Content("Sensor not found");
        if (NotifierToSensorData.GetAllSensorByNotifier(notiferID).Count >= 10)
          return (ActionResult) this.Content("Unable to add: Only 10 Sensors allowed");
        new NotifierToSensorData()
        {
          NotifierID = notiferID,
          SensorID = id
        }.Save();
        return (ActionResult) this.Content("Success");
      }
      NotifierToSensorData.GetAllSensorByNotifier(notiferID).Where<NotifierToSensorData>((System.Func<NotifierToSensorData, bool>) (notif => notif.SensorID == id)).FirstOrDefault<NotifierToSensorData>().Delete();
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Unable to remove");
    }
  }

  [NoCache]
  [Authorize]
  public ActionResult HistoryMissed(long id)
  {
    this.ViewData["Sensor"] = (object) Sensor.Load(id);
    long sensorID = id;
    DateTime dateTime = MonnitSession.HistoryFromDate;
    DateTime utcFromLocalById1 = Monnit.TimeZone.GetUTCFromLocalById(dateTime.Date, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    dateTime = MonnitSession.HistoryToDate;
    dateTime = dateTime.Date;
    DateTime utcFromLocalById2 = Monnit.TimeZone.GetUTCFromLocalById(dateTime.AddDays(1.0), MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DataTable dataTable = DataMessage.LoadMissedBySensorAndDateRange(sensorID, utcFromLocalById1, utcFromLocalById2);
    List<MissedSensorMessages> source = new List<MissedSensorMessages>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      source.Add(new MissedSensorMessages(row));
    return (ActionResult) this.PartialView((object) source.OrderByDescending<MissedSensorMessages, DateTime>((System.Func<MissedSensorMessages, DateTime>) (date => date.ExpectedCheckIn)).ToList<MissedSensorMessages>());
  }

  [NoCache]
  [Authorize]
  public ActionResult HistoryMissedOV(long id)
  {
    this.ViewData["Sensor"] = (object) Sensor.Load(id);
    DataTable dataTable = DataMessage.LoadMissedBySensorAndDateRange(id, Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID), Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID));
    List<MissedSensorMessages> source = new List<MissedSensorMessages>();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      source.Add(new MissedSensorMessages(row));
    return (ActionResult) this.PartialView((object) source.OrderByDescending<MissedSensorMessages, DateTime>((System.Func<MissedSensorMessages, DateTime>) (date => date.ExpectedCheckIn)).ToList<MissedSensorMessages>());
  }

  [NoCache]
  [Authorize]
  [HttpGet]
  public ActionResult Export(long id)
  {
    Sensor sensor = Sensor.Load(id);
    return sensor == null || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID) || sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor) ? (ActionResult) this.Content("Unathorized") : (ActionResult) this.PartialView((object) sensor);
  }

  [NoCache]
  [Authorize]
  public ActionResult ExportData(long id)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    foreach (string allKey in this.Request.QueryString.AllKeys)
      dictionary.Add(allKey, this.Request.QueryString[allKey]);
    dictionary.Remove("uxExportAll");
    dictionary.Remove(nameof (id));
    // ISSUE: reference to a compiler-generated field
    if (SensorController.\u003C\u003Eo__24.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SensorController.\u003C\u003Eo__24.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Dictionary<string, string>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Dictionary", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SensorController.\u003C\u003Eo__24.\u003C\u003Ep__0.Target((CallSite) SensorController.\u003C\u003Eo__24.\u003C\u003Ep__0, this.ViewBag, dictionary);
    Sensor sensor = Sensor.Load(id);
    return sensor == null || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID) || sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor) ? (ActionResult) this.View("Unathorized") : (ActionResult) this.View((object) sensor);
  }

  [NoCache]
  [Authorize]
  [HttpGet]
  public ActionResult Calibrate(long id)
  {
    Sensor model = Sensor.Load(id);
    string str = $"ApplicationSpecific\\{model.ApplicationID}\\Calibrate";
    return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "Sensor", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) model) : (ActionResult) this.PartialView((object) model);
  }

  [Authorize]
  [HttpGet]
  public ActionResult CalibrationCertificate(long id)
  {
    return (ActionResult) this.PartialView((object) Sensor.Load(id));
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult DefaultCalibrate(long id, string url)
  {
    try
    {
      Sensor sensor = Sensor.Load(id.ToLong());
      sensor.SetDefaults(false);
      sensor.Save();
      // ISSUE: reference to a compiler-generated field
      if (SensorController.\u003C\u003Eo__27.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SensorController.\u003C\u003Eo__27.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SensorController.\u003C\u003Eo__27.\u003C\u003Ep__0.Target((CallSite) SensorController.\u003C\u003Eo__27.\u003C\u003Ep__0, this.ViewBag, "Sensor Reset Defaults Pending");
      // ISSUE: reference to a compiler-generated field
      if (SensorController.\u003C\u003Eo__27.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SensorController.\u003C\u003Eo__27.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SensorController.\u003C\u003Eo__27.\u003C\u003Ep__1.Target((CallSite) SensorController.\u003C\u003Eo__27.\u003C\u003Ep__1, this.ViewBag, string.IsNullOrWhiteSpace(url) || !this.IsLocalUrl(url) ? (MonnitSession.AccountCan("sensor_calibrate") ? "/Sensor/Calibrate/" + sensor.SensorID.ToString() : "/Sensor/Edit/" + sensor.SensorID.ToString()) : url);
      return (ActionResult) this.PartialView("EditConfirmation");
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
      return (ActionResult) this.Content("was unable to set back to defaults");
    }
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult resetCounter(FormCollection collection)
  {
    try
    {
      Sensor sensor = Sensor.Load(collection["id"].ToLong());
      MonnitApplicationBase.CalibrateSensor((NameValueCollection) collection, sensor);
      // ISSUE: reference to a compiler-generated field
      if (SensorController.\u003C\u003Eo__28.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SensorController.\u003C\u003Eo__28.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SensorController.\u003C\u003Eo__28.\u003C\u003Ep__0.Target((CallSite) SensorController.\u003C\u003Eo__28.\u003C\u003Ep__0, this.ViewBag, "Sensor Reset Counter Pending");
      // ISSUE: reference to a compiler-generated field
      if (SensorController.\u003C\u003Eo__28.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SensorController.\u003C\u003Eo__28.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SensorController.\u003C\u003Eo__28.\u003C\u003Ep__1.Target((CallSite) SensorController.\u003C\u003Eo__28.\u003C\u003Ep__1, this.ViewBag, collection["url"]);
      return (ActionResult) this.PartialView("EditConfirmation");
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
      return (ActionResult) this.Content("Was unable to reset counter");
    }
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Calibrate(long id, FormCollection collection)
  {
    Sensor sensor = Sensor.Load(id);
    Account account = Account.Load(sensor.AccountID);
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    try
    {
      sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Calibrated sensor");
      MonnitApplicationBase.CalibrateSensor((NameValueCollection) collection, sensor);
      // ISSUE: reference to a compiler-generated field
      if (SensorController.\u003C\u003Eo__29.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SensorController.\u003C\u003Eo__29.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SensorController.\u003C\u003Eo__29.\u003C\u003Ep__0.Target((CallSite) SensorController.\u003C\u003Eo__29.\u003C\u003Ep__0, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/Sensor/Calibrate/" + sensor.SensorID.ToString() : collection["returns"]);
      if (!sensor.CanUpdate)
      {
        // ISSUE: reference to a compiler-generated field
        if (SensorController.\u003C\u003Eo__29.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SensorController.\u003C\u003Eo__29.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = SensorController.\u003C\u003Eo__29.\u003C\u003Ep__1.Target((CallSite) SensorController.\u003C\u003Eo__29.\u003C\u003Ep__1, this.ViewBag, "Sensor Calibration Pending");
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (SensorController.\u003C\u003Eo__29.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SensorController.\u003C\u003Eo__29.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = SensorController.\u003C\u003Eo__29.\u003C\u003Ep__2.Target((CallSite) SensorController.\u003C\u003Eo__29.\u003C\u003Ep__2, this.ViewBag, "Sensor Calibration Success");
      }
      return (ActionResult) this.PartialView("EditConfirmation");
    }
    catch (Exception ex)
    {
      Exception exception = ex;
      while (exception.InnerException != null)
        exception = exception.InnerException;
      // ISSUE: reference to a compiler-generated field
      if (SensorController.\u003C\u003Eo__29.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SensorController.\u003C\u003Eo__29.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ErrorMessage", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SensorController.\u003C\u003Eo__29.\u003C\u003Ep__3.Target((CallSite) SensorController.\u003C\u003Eo__29.\u003C\u003Ep__3, this.ViewBag, exception.Message);
      string str = $"ApplicationSpecific\\{sensor.ApplicationID}\\Calibrate";
      return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "Sensor", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) sensor) : (ActionResult) this.PartialView((object) sensor);
    }
  }

  [Authorize]
  public ActionResult ClearPendingTerminalHistory(long sensorID)
  {
    Sensor model = Sensor.Load(sensorID);
    if (model == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    try
    {
      NotificationRecorded.ClearMessagesBySentToDeviceID(model.SensorID);
      model.MarkClean(true);
    }
    catch (Exception ex)
    {
      ex.Log($"SensorController.ClearPendingTerminalHistory | SensorID = {sensorID}");
      return (ActionResult) this.Content("Not Found");
    }
    string str = $"ApplicationSpecific\\{model.ApplicationID}\\Terminal";
    return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "Sensor", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) model) : (ActionResult) this.Content("Not Found");
  }

  [Authorize]
  public ActionResult ClearPendingControlHistory(long sensorID)
  {
    Sensor model = Sensor.Load(sensorID);
    if (model == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    try
    {
      NotificationRecorded.ClearMessagesBySentToDeviceID(model.SensorID);
    }
    catch (Exception ex)
    {
      ex.Log($"SensorController.ClearPendingControlHistory | Not Found | SensorID = {sensorID}");
      return (ActionResult) this.Content("Not Found");
    }
    string str = $"ApplicationSpecific\\{model.ApplicationID}\\Control";
    return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "Sensor", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) model) : (ActionResult) this.Content("Not Found");
  }

  [Authorize]
  public ActionResult ClearPendingNotifierHistory(long sensorID, bool? newLook)
  {
    Sensor model = Sensor.Load(sensorID);
    if (model == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    try
    {
      NotificationRecorded.ClearMessagesBySentToDeviceID(model.SensorID);
    }
    catch (Exception ex)
    {
      string str = !newLook.HasValue ? "null" : newLook.ToString();
      ex.Log($"SensorController.ClearPendingNotifierHistory | Not Found | SensorID = {sensorID}, newLook = {str}");
      return (ActionResult) this.Content("Not Found");
    }
    string str1 = $"ApplicationSpecific\\{model.ApplicationID}\\Notify";
    if (!MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str1, "Sensor", MonnitSession.CurrentTheme.Theme))
      return (ActionResult) this.Content("Not Found");
    return newLook ?? true ? (ActionResult) this.Content("Success") : (ActionResult) this.PartialView(str1, (object) model);
  }

  [NoCache]
  [Authorize]
  public ActionResult Control(long id)
  {
    Sensor model = Sensor.Load(id);
    string str = $"ApplicationSpecific\\{model.ApplicationID}\\Control";
    return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "Sensor", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) model) : (ActionResult) this.Content("Not Found");
  }

  [HttpPost]
  [Authorize]
  [ValidateAntiForgeryToken]
  public ActionResult Control(long id, FormCollection collection)
  {
    Sensor sensor = Sensor.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    try
    {
      MonnitApplicationBase.ControlSensor((NameValueCollection) collection, sensor);
      string str = $"ApplicationSpecific\\{sensor.ApplicationID}\\Control";
      return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "Sensor", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) sensor) : (ActionResult) this.Content("Not Found");
    }
    catch
    {
      string str = $"ApplicationSpecific\\{sensor.ApplicationID}\\Control";
      return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "Sensor", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) sensor) : (ActionResult) this.Content("Not Found");
    }
  }

  [NoCache]
  [Authorize]
  public ActionResult Terminal(long id)
  {
    Sensor model = Sensor.Load(id);
    string str = $"ApplicationSpecific\\{model.ApplicationID}\\Terminal";
    return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "Sensor", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) model) : (ActionResult) this.Content("Not Found");
  }

  [HttpPost]
  [Authorize]
  [ValidateAntiForgeryToken]
  public ActionResult Terminal(long id, FormCollection collection)
  {
    Sensor sensor = Sensor.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    try
    {
      MonnitApplicationBase.TerminalSensor((NameValueCollection) collection, sensor);
      return (ActionResult) this.Content("Terminal request will process when the sensor checks in.");
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (SensorController.\u003C\u003Eo__36.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SensorController.\u003C\u003Eo__36.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ErrorMessage", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SensorController.\u003C\u003Eo__36.\u003C\u003Ep__0.Target((CallSite) SensorController.\u003C\u003Eo__36.\u003C\u003Ep__0, this.ViewBag, ex.Message);
      string str = $"ApplicationSpecific\\{sensor.ApplicationID}\\Terminal";
      return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "Sensor", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) sensor) : (ActionResult) this.Content("Not Found");
    }
  }

  [NoCache]
  [Authorize]
  public ActionResult Acknowledge(long sensorID, double heartbeat)
  {
    new AcknowledgementRecorded()
    {
      Acknowledgement = ("Sensor heartbeat of " + heartbeat.ToStringSafe()),
      CustomerID = MonnitSession.CurrentCustomer.CustomerID,
      SensorID = sensorID,
      DateRecorded = DateTime.UtcNow
    }.Save();
    return (ActionResult) this.Content("OK");
  }

  [NoCache]
  [Authorize]
  public ActionResult ExternalConfiguration(long id)
  {
    ExternalDataSubscription model = ExternalDataSubscription.LoadAllByAccountID(MonnitSession.CurrentCustomer.AccountID).FirstOrDefault<ExternalDataSubscription>();
    if (model == null)
      model = new ExternalDataSubscription()
      {
        AccountID = MonnitSession.CurrentCustomer.AccountID
      };
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ExternalConfiguration(FormCollection collection)
  {
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.Load(collection["ExternalDataSubscriptionID"].ToLong()) ?? new ExternalDataSubscription();
    if (!this.ModelState.IsValid)
      return (ActionResult) this.Content("Unable to save the external data subscription.");
    dataSubscription.AccountID = MonnitSession.CurrentCustomer.AccountID;
    dataSubscription.ConnectionInfo1 = collection["ConnectionInfo1"].ToStringSafe();
    dataSubscription.ConnectionInfo2 = collection["ConnectionInfo2"].ToStringSafe();
    dataSubscription.ContentHeaderType = collection["ContentHeaderType"].ToStringSafe();
    dataSubscription.ExternalID = collection["ExternalID"].ToStringSafe();
    dataSubscription.verb = collection["verb"].ToStringSafe();
    if (dataSubscription.ExternalID == string.Empty)
      dataSubscription.ExternalID = "None";
    dataSubscription.Save();
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ExternalWebhookConfiguration(FormCollection collection)
  {
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.Load(collection["ExternalDataSubscriptionID"].ToLong()) ?? new ExternalDataSubscription();
    if (!this.ModelState.IsValid)
      return (ActionResult) this.Content("Unable to save the external data subscription.");
    dataSubscription.AccountID = MonnitSession.CurrentCustomer.AccountID;
    dataSubscription.ConnectionInfo1 = collection["ConnectionInfo1"].ToStringSafe();
    dataSubscription.Save();
    return (ActionResult) this.Content("Success");
  }

  [NoCache]
  [Authorize]
  public ActionResult TestExternalConfiguration(long id, string formatUrl, string externalID)
  {
    Sensor sensor = Sensor.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.Json((object) new
      {
        result = "Error",
        data = "Not Authorized"
      });
    if (!formatUrl.ToLower().StartsWith("http"))
      return (ActionResult) this.Json((object) new
      {
        result = "Error",
        data = "Please specify http or https"
      });
    DataMessage lastDataMessage = sensor.LastDataMessage;
    try
    {
      return (ActionResult) this.Json((object) new
      {
        result = "Success",
        data = string.Format(formatUrl, (object) externalID.ToStringSafe(), (object) lastDataMessage.DataMessageGUID, (object) HttpUtility.UrlEncode(lastDataMessage.Data), (object) HttpUtility.UrlEncode(lastDataMessage.DisplayData), (object) lastDataMessage.MessageDate, (object) lastDataMessage.Battery, (object) DataMessage.GetSignalStrengthPercent(sensor.GenerationType, sensor.SensorTypeID, lastDataMessage.SignalStrength), (object) lastDataMessage.State, (object) sensor.SensorID, (object) HttpUtility.UrlEncode(sensor.SensorName), (object) sensor.AccountID, (object) sensor.CSNetID, (object) sensor.FirmwareVersion, (object) sensor.CanUpdate, (object) sensor.IsActive, (object) sensor.IsSleeping, (object) sensor.ApplicationID, (object) lastDataMessage.GatewayID, (object) lastDataMessage.PlotValueString)
      });
    }
    catch (Exception ex)
    {
      ex.Log("SensorController.TestExternalConfiguration ");
      return (ActionResult) this.Json((object) new
      {
        result = "Error",
        data = "Error building test url"
      });
    }
  }

  [NoCache]
  [Authorize]
  public ActionResult TestExternalConfigurationBody(long id, string formatUrl, string externalID)
  {
    Sensor sensor = Sensor.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.Json((object) new
      {
        result = "Error",
        data = "Not Authorized"
      });
    DataMessage lastDataMessage = sensor.LastDataMessage;
    try
    {
      return (ActionResult) this.Json((object) new
      {
        result = "Success",
        data = string.Format(formatUrl, (object) externalID, (object) lastDataMessage.DataMessageGUID, (object) HttpUtility.UrlEncode(lastDataMessage.Data), (object) HttpUtility.UrlEncode(lastDataMessage.DisplayData), (object) lastDataMessage.MessageDate.ToString(), (object) lastDataMessage.Battery, (object) DataMessage.GetSignalStrengthPercent(sensor.GenerationType, sensor.SensorTypeID, lastDataMessage.SignalStrength), (object) lastDataMessage.State, (object) sensor.SensorID, (object) HttpUtility.UrlEncode(sensor.SensorName), (object) sensor.AccountID, (object) sensor.CSNetID, (object) sensor.FirmwareVersion, (object) sensor.CanUpdate, (object) sensor.IsActive, (object) sensor.IsSleeping, (object) sensor.ApplicationID, (object) lastDataMessage.GatewayID, (object) lastDataMessage.PlotValueString)
      });
    }
    catch (Exception ex)
    {
      ex.Log("SensorController.TestExternalConfiguration ");
      return (ActionResult) this.Json((object) new
      {
        result = "Error",
        data = "Error building test url"
      });
    }
  }

  [NoCache]
  [Authorize]
  public ActionResult DeleteExternalConfiguration(long id)
  {
    ExternalDataSubscription.Load(id).Delete();
    MonnitSession.CurrentCustomer.Account.ExternalDataSubscription = (ExternalDataSubscription) null;
    return (ActionResult) this.Content("Success");
  }

  [NoCache]
  [Authorize]
  public ActionResult ResetBrokenExternalDataSubscription(long id)
  {
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.Load(id);
    dataSubscription.initialResetBroken();
    dataSubscription.Save();
    return (ActionResult) this.Content("Success");
  }

  [NoCache]
  [Authorize]
  public ActionResult ReadyToShip(long id)
  {
    if (!MonnitSession.CustomerCan("Sensor_Set_Shipping"))
      return (ActionResult) this.Content("Not Authorized");
    Sensor DBObject = Sensor.Load(id);
    Account account = Account.Load(DBObject.AccountID);
    DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Set sensor to ready to ship");
    DBObject.ReadyToShip();
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  public ActionResult ClearSensorHistory(long id)
  {
    try
    {
      Account.Load(Sensor.Load(id).AccountID);
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log($"SensorController.ClearSensorHistory | Unable to complete request | SensorID = {id}");
      return (ActionResult) this.Content("Unable to complete request");
    }
  }

  [NoCache]
  [Authorize]
  public ActionResult SetDateRange(string range)
  {
    MonnitSession.CurrentDateRange = range;
    return (ActionResult) this.Content("Success");
  }

  public ActionResult ClearWIFISettings(long id)
  {
    Sensor sensor = Sensor.Load(id);
    sensor.SetDefaults(false);
    sensor.MarkClean(false);
    sensor.Save();
    Gateway gateway = Gateway.LoadBySensorID(id);
    if (gateway != null)
    {
      gateway.IsDirty = false;
      gateway.WiFiNetworkCount = 0;
      gateway.SendResetNetworkRequest = true;
      gateway.DeleteAllWiFiCredentials();
      gateway.Save();
    }
    return (ActionResult) this.Content("Success");
  }

  [NoCache]
  [Authorize]
  public ActionResult FileList(long id)
  {
    return !MonnitSession.IsAuthorizedForAccount(Sensor.Load(id).AccountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index"
    }) : (ActionResult) this.PartialView((object) SensorFile.LoadBySensor(id));
  }

  [Authorize]
  public ActionResult DownloadFile(long id)
  {
    SensorFile sensorFile = SensorFile.Load(id);
    if (sensorFile == null)
      return (ActionResult) this.View();
    if (!MonnitSession.IsAuthorizedForAccount(Sensor.Load(sensorFile.SensorID).AccountID))
      return (ActionResult) this.View();
    string fileDownloadName = $"{id}-{MonnitSession.MakeLocal(sensorFile.Date)}.jpg";
    fileDownloadName.Replace("/", "-").Replace(" ", "");
    return (ActionResult) this.File(sensorFile.Data, "image/jpeg", fileDownloadName);
  }

  [Authorize]
  public ActionResult Scale(long ID)
  {
    Sensor model = Sensor.Load(ID);
    string str = $"ApplicationSpecific\\{model.ApplicationID}\\Scale";
    return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "Sensor", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) model) : (ActionResult) this.PartialView((object) model);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Scale(long id, FormCollection collection)
  {
    Sensor sensor = Sensor.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    string record;
    try
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append($"{{\"DeviceID\" : \"{sensor.SensorID}\", \"DeviceType\": \"{"Sensor"}\", \"Date\": \"{DateTime.UtcNow}\" ,");
      foreach (string name in (NameObjectCollectionBase) collection)
      {
        if (!(name == "__RequestVerificationToken") && !(name == "returns"))
          stringBuilder.Append($"\"{name}\" : \" {collection[name]} \",");
      }
      record = stringBuilder.ToString().TrimEnd(',') + "}";
    }
    catch
    {
      record = $"{{\"DeviceID\" : \"{sensor.SensorID}\", \"DeviceType\": \"{"Sensor"}\", \"Scale\": \"{"Unable to log Scale changes"}\", \"Date\": \"{DateTime.UtcNow}\" }} ";
    }
    Account account = Account.Load(sensor.AccountID);
    if (account != null)
      AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, sensor.SensorID, eAuditAction.Update, eAuditObject.Sensor, record, account.AccountID, "Edited sensor scale");
    try
    {
      foreach (string allKey in collection.AllKeys)
      {
        if (!allKey.ToLower().Contains("label") && !allKey.ToLower().Contains("datum") && !allKey.ToLower().Contains("tempscale") && !allKey.ToLower().Contains("scale") && !allKey.ToLower().Contains("tankdepth") && !allKey.ToLower().Contains("showdepth") && !allKey.ToLower().Contains("returns") && !allKey.ToLower().Contains("__requestverificationtoken") && !allKey.Contains("APorEP_ManualChkBx") && !double.TryParse(collection[allKey].ToString(), out double _))
          throw new Exception("All fields except for the label field must be numeric");
      }
      NameValueCollection returnValues;
      MonnitApplicationBase.SensorScale(sensor, (NameValueCollection) collection, out returnValues);
      foreach (string key in returnValues.Keys)
        this.ViewData[key] = (object) returnValues[key];
      // ISSUE: reference to a compiler-generated field
      if (SensorController.\u003C\u003Eo__52.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SensorController.\u003C\u003Eo__52.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SensorController.\u003C\u003Eo__52.\u003C\u003Ep__0.Target((CallSite) SensorController.\u003C\u003Eo__52.\u003C\u003Ep__0, this.ViewBag, collection["returns"]);
      if (!sensor.CanUpdate)
      {
        // ISSUE: reference to a compiler-generated field
        if (SensorController.\u003C\u003Eo__52.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SensorController.\u003C\u003Eo__52.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = SensorController.\u003C\u003Eo__52.\u003C\u003Ep__1.Target((CallSite) SensorController.\u003C\u003Eo__52.\u003C\u003Ep__1, this.ViewBag, "Sensor Scale Change Pending");
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (SensorController.\u003C\u003Eo__52.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SensorController.\u003C\u003Eo__52.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = SensorController.\u003C\u003Eo__52.\u003C\u003Ep__2.Target((CallSite) SensorController.\u003C\u003Eo__52.\u003C\u003Ep__2, this.ViewBag, "Sensor Scale Change Success");
      }
      return (ActionResult) this.PartialView("EditConfirmation");
    }
    catch (Exception ex)
    {
      Exception exception = ex;
      while (exception.InnerException != null)
        exception = exception.InnerException;
      // ISSUE: reference to a compiler-generated field
      if (SensorController.\u003C\u003Eo__52.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SensorController.\u003C\u003Eo__52.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ErrorMessage", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SensorController.\u003C\u003Eo__52.\u003C\u003Ep__3.Target((CallSite) SensorController.\u003C\u003Eo__52.\u003C\u003Ep__3, this.ViewBag, exception.Message);
    }
    string str = $"ApplicationSpecific\\{sensor.ApplicationID}\\Scale";
    return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "Sensor", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) sensor) : (ActionResult) this.PartialView((object) sensor);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ClearPendingMsg(long id)
  {
    Sensor sensor = Sensor.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    try
    {
      return NotificationRecorded.ClearMessagesBySentToDeviceID(sensor.SensorID) ? (ActionResult) this.Content(string.Format("Sucessfully cleared messages from the sensor.")) : (ActionResult) this.Content(string.Format("Failed To clear messages."));
    }
    catch
    {
      return (ActionResult) this.Content("Failed To clear messages.");
    }
  }

  public ActionResult SensorInformationDetails(long? sensorID, string code)
  {
    if ((sensorID ?? long.MinValue) < 1L)
      return (ActionResult) this.PartialView();
    if (!MonnitUtil.ValidateCheckDigit(sensorID ?? long.MinValue, code))
      this.ModelState.AddModelError("", "Input is invalid.");
    if (this.ModelState.IsValid)
    {
      Sensor model = Sensor.Load(sensorID ?? long.MinValue);
      if (model != null)
        return (ActionResult) this.PartialView((object) model);
      this.ModelState.AddModelError("", "Unable to load sensor details");
    }
    return (ActionResult) this.PartialView();
  }

  [AuthorizeDefault]
  public ActionResult EquipmentInfo(long id)
  {
    Sensor model = Sensor.Load(id);
    if (model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    // ISSUE: reference to a compiler-generated field
    if (SensorController.\u003C\u003Eo__55.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SensorController.\u003C\u003Eo__55.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ErrorMessage", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SensorController.\u003C\u003Eo__55.\u003C\u003Ep__0.Target((CallSite) SensorController.\u003C\u003Eo__55.\u003C\u003Ep__0, this.ViewBag, "");
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult EquipmentInfo(long id, FormCollection collection)
  {
    Sensor model = Sensor.Load(id);
    if (model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    if (string.IsNullOrEmpty(collection["Make"]) || string.IsNullOrEmpty(collection["SerialNumber"]))
    {
      // ISSUE: reference to a compiler-generated field
      if (SensorController.\u003C\u003Eo__56.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SensorController.\u003C\u003Eo__56.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ErrorMessage", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SensorController.\u003C\u003Eo__56.\u003C\u003Ep__0.Target((CallSite) SensorController.\u003C\u003Eo__56.\u003C\u003Ep__0, this.ViewBag, "All fields are required");
      return (ActionResult) this.View((object) model);
    }
    // ISSUE: reference to a compiler-generated field
    if (SensorController.\u003C\u003Eo__56.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SensorController.\u003C\u003Eo__56.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ErrorMessage", typeof (SensorController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SensorController.\u003C\u003Eo__56.\u003C\u003Ep__1.Target((CallSite) SensorController.\u003C\u003Eo__56.\u003C\u003Ep__1, this.ViewBag, "Success");
    model.Make = collection["Make"];
    model.SerialNumber = collection["SerialNumber"];
    model.Save();
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult SetDatumName(long sensorID, int datumindex, string name)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    sensor.SetDatumName(datumindex, name);
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  public ActionResult GetDatumName(long sensorID, int datumindex)
  {
    Sensor sensor = Sensor.Load(sensorID);
    return !MonnitSession.IsAuthorizedForAccount(sensor.AccountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index"
    }) : (ActionResult) this.Content(sensor.GetDatumName(datumindex));
  }

  public ActionResult IncrementThresh(string ids, int incval)
  {
    Sensor sensor = new Sensor();
    try
    {
      string str = ids;
      char[] chArray = new char[1]{ '|' };
      foreach (string o in str.Split(chArray))
      {
        if (!string.IsNullOrWhiteSpace(o))
        {
          Sensor sens = Sensor.Load(o.ToLong());
          if (SensorController.MinThreashInRange(sens, incval))
          {
            sens.Save();
          }
          else
          {
            sensor = sens;
            throw new Exception($"Sensor: {sens.SensorName} failed to save");
          }
        }
      }
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content(ex.Message);
    }
  }

  public static bool MinThreashInRange(Sensor sens, int incval)
  {
    try
    {
      sens.MinimumThreshold += (long) incval;
      if (sens.MinimumThreshold < -400L)
        sens.MinimumThreshold = -400L;
      if (sens.MinimumThreshold > 1250L)
        sens.MinimumThreshold = 1250L;
      if (sens.MinimumThreshold > sens.MaximumThreshold)
        sens.MaximumThreshold = sens.MinimumThreshold;
      return true;
    }
    catch
    {
    }
    return false;
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult SetShowFullDataValue(string sensorID, bool showFullData)
  {
    sensorID = sensorID.Split(':')[0];
    long num;
    try
    {
      num = long.Parse(sensorID);
    }
    catch
    {
      return (ActionResult) this.Content($"Invalid SensorID: {sensorID}");
    }
    Sensor sensor = Sensor.Load(num);
    string content = "Success";
    try
    {
      if (showFullData)
      {
        long monnitApplicationId = sensor.MonnitApplicationID;
        if (monnitApplicationId <= 109L)
        {
          if (monnitApplicationId <= 93L)
          {
            if (monnitApplicationId != 34L)
            {
              if (monnitApplicationId == 93L)
                CurrentZeroToTwentyAmp.SetShowFullDataValue(sensor.SensorID, true);
            }
            else
              Gas_CO.SetDisplay(sensor.SensorID, eCOGasDisplay.All);
          }
          else if (monnitApplicationId != 94L)
          {
            switch (monnitApplicationId - 102L)
            {
              case 0:
                AirQuality.SetShowFullDataValue(sensor.SensorID, true);
                break;
              case 1:
                DifferentialPressure.SetShowFullDataValue(sensor.SensorID, true);
                break;
              case 2:
                Vibration800.SetShowFullDataValue(sensor.SensorID, true);
                break;
              default:
                if (monnitApplicationId == 109L)
                {
                  ThreePhaseCurrentMeter.SetDataViewOption(sensor.SensorID, 3);
                  break;
                }
                break;
            }
          }
          else
            CurrentZeroToOneFiftyAmp.SetShowFullDataValue(sensor.SensorID, true);
        }
        else if (monnitApplicationId <= 129L)
        {
          if (monnitApplicationId != 114L)
          {
            if (monnitApplicationId != 120L)
            {
              if (monnitApplicationId == 129L)
                ThreePhase500AmpMeter.SetDataViewOption(sensor.SensorID, 3);
            }
            else
              CurrentZeroTo500Amp.SetShowFullDataValue(sensor.SensorID, true);
          }
          else
            AirSpeed.SetShowFullDataValue(sensor.SensorID, true);
        }
        else if (monnitApplicationId != 137L)
        {
          if (monnitApplicationId != 141L)
          {
            if (monnitApplicationId == 143L)
              SiteSurvey.SetShowFullDataValue(sensor.SensorID, true);
          }
          else
            CurrentZeroToFiveAmp.SetShowFullDataValue(sensor.SensorID, true);
        }
        else
          ThreePhase20AmpMeter.SetDataViewOption(sensor.SensorID, 3);
      }
    }
    catch
    {
      content = "Error";
    }
    SensorAttribute.ResetAttributeList(num);
    return (ActionResult) this.Content(content);
  }

  [HttpGet]
  [AuthorizeDefault]
  public ActionResult CanUpdate(long id)
  {
    Sensor sensor = Sensor.Load(id);
    return (ActionResult) this.Content((sensor != null && sensor.CanUpdate).ToString());
  }

  [NoCache]
  public ActionResult FilterName(string name)
  {
    try
    {
      MonnitSession.SensorListFilters.Name = name;
    }
    catch
    {
      MonnitSession.SensorListFilters.Name = "";
    }
    return (ActionResult) this.Content("Success");
  }
}
