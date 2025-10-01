// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.OverviewController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using BaseApplication;
using iMonnit.ControllerBase;
using iMonnit.Models;
using Microsoft.CSharp.RuntimeBinder;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Linq;

#nullable disable
namespace iMonnit.Controllers;

public class OverviewController : OverviewControllerBase
{
  [AuthorizeDefault]
  public ActionResult Index()
  {
    string homepageLink = MonnitSession.CurrentCustomer.HomepageLink;
    if (!string.IsNullOrEmpty(homepageLink) && this.Request.Path == "/")
      return (ActionResult) this.Redirect(homepageLink);
    long accountId = MonnitSession.CurrentCustomer.AccountID;
    if (Account.Load(accountId) == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Logoff",
        controller = "Overview"
      });
    List<DeviceStatusModel> source = DeviceStatusModel.DeviceStatusModel_LoadByAccountID(accountId);
    DeviceStatusModel deviceStatusModel1 = source.Where<DeviceStatusModel>((System.Func<DeviceStatusModel, bool>) (c => c.Type.ToLower() == "gateway")).FirstOrDefault<DeviceStatusModel>();
    DeviceStatusModel deviceStatusModel2 = source.Where<DeviceStatusModel>((System.Func<DeviceStatusModel, bool>) (c => c.Type.ToLower() == "sensor")).FirstOrDefault<DeviceStatusModel>();
    int total1 = deviceStatusModel2 == null ? 0 : deviceStatusModel2.Total;
    int total2 = deviceStatusModel1 == null ? 0 : deviceStatusModel1.Total;
    string str = "";
    if (CSNetControllerBase.GetListOfNetworksUserCanSee(new long?(accountId)).Count > 0)
      str = $"/Setup/AssignDevice/{accountId.ToString()}?networkID={CSNetControllerBase.GetListOfNetworksUserCanSee(new long?(accountId))[0].CSNetID.ToString()}";
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__0.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__0.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "accountID", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = OverviewController.\u003C\u003Eo__0.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__0.\u003C\u003Ep__0, this.ViewBag, accountId);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__0.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__0.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "sensCount", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = OverviewController.\u003C\u003Eo__0.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__0.\u003C\u003Ep__1, this.ViewBag, total1);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__0.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__0.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "gwCount", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = OverviewController.\u003C\u003Eo__0.\u003C\u003Ep__2.Target((CallSite) OverviewController.\u003C\u003Eo__0.\u003C\u003Ep__2, this.ViewBag, total2);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__0.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__0.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "simpleSetupAssignDevice", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj4 = OverviewController.\u003C\u003Eo__0.\u003C\u003Ep__3.Target((CallSite) OverviewController.\u003C\u003Eo__0.\u003C\u003Ep__3, this.ViewBag, str);
    MonnitSession.SensorListFilters.CSNetID = long.MinValue;
    MonnitSession.SensorListFilters.ApplicationID = long.MinValue;
    MonnitSession.SensorListFilters.Status = int.MinValue;
    MonnitSession.SensorListFilters.Name = string.Empty;
    MonnitSession.SensorListFilters.Custom = string.Empty;
    MonnitSession.GatewayListFilters.CSNetID = long.MinValue;
    MonnitSession.GatewayListFilters.GatewayTypeID = long.MinValue;
    MonnitSession.GatewayListFilters.Status = int.MinValue;
    MonnitSession.GatewayListFilters.Name = string.Empty;
    DataMessage.CacheLastByAccount(MonnitSession.CurrentCustomer.AccountID, new TimeSpan(0, 1, 0));
    return (ActionResult) this.View();
  }

  public ActionResult HealthCheck()
  {
    List<string> stringList = new List<string>();
    try
    {
      if (!MonnitUtil.CheckDBConnection())
        stringList.Add("DB Connection Failed");
      if (ConfigData.Find("DefaultCSNetID") == null)
        stringList.Add("Config Data Failure");
      if (ConfigData.Find("DefaultCSNetID") == null)
        stringList.Add("Session Failure - Theme");
    }
    catch (Exception ex)
    {
      ex.Log("OverviewController.HealthCheck");
      stringList.Add("Unexpected Error");
    }
    this.TempData.Add("Errors", (object) stringList);
    return (ActionResult) this.View();
  }

  public ActionResult Logo()
  {
    byte[] fileContents = MonnitSession.CurrentBinaryStyle(nameof (Logo));
    return fileContents != null && fileContents.Length != 0 ? (ActionResult) this.File(fileContents, "image/png") : (ActionResult) this.View("Error");
  }

  public ActionResult Favicon()
  {
    byte[] fileContents = MonnitSession.CurrentBinaryStyle(nameof (Favicon));
    return fileContents != null && fileContents.Length != 0 ? (ActionResult) this.File(fileContents, "image/x-icon") : (ActionResult) this.View("Error");
  }

  public ActionResult LogOff()
  {
    if (MonnitSession.CurrentCustomer != null)
      Customer.SetForceLogoutDate(Customer.Load(MonnitSession.UserIsCustomerProxied ? MonnitSession.CustomerIDLoggedInAsProxy : MonnitSession.CurrentCustomer.CustomerID).CustomerID);
    OverviewControllerBase.ClearRememberMeCookie(System.Web.HttpContext.Current.Request);
    FormsAuthentication.SignOut();
    this.Session.Clear();
    this.Session.Abandon();
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    });
  }

  [AuthorizeDefault]
  public ActionResult ChangePassword()
  {
    this.ViewData["PasswordLength"] = (object) MonnitUtil.MinPasswordLength;
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ChangePassword(ChangePasswordModel model)
  {
    if (!MonnitUtil.IsValidPassword(model.NewPassword, MonnitSession.CurrentTheme))
      this.ModelState.AddModelError("Password", MonnitUtil.PasswordHelperString(MonnitSession.CurrentTheme));
    if (this.ModelState.IsValid)
    {
      Customer user = (Customer) null;
      if (Account.ValidateUser(this.User.Identity.Name, model.OldPassword, "Web Password Change", this.Request.ClientIP(), MonnitSession.UseEncryption, out user))
      {
        if (user.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
        {
          Customer currentCustomer = MonnitSession.CurrentCustomer;
          currentCustomer.PasswordExpired = false;
          currentCustomer.PasswordChangeDate = DateTime.UtcNow;
          currentCustomer.Salt = MonnitUtil.GenerateSalt();
          currentCustomer.WorkFactor = ConfigData.AppSettings("WorkFactor").ToInt();
          currentCustomer.Password2 = MonnitUtil.GenerateHash(model.ConfirmPassword, currentCustomer.Salt, currentCustomer.WorkFactor);
          currentCustomer.Save();
          return (ActionResult) this.RedirectToRoute("Default", (object) new
          {
            action = "ChangePasswordSuccess",
            controller = "Overview"
          });
        }
        this.ModelState.AddModelError("", "The password can only be reset for the current user.");
      }
      else
        this.ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
    }
    this.ViewData["PasswordLength"] = (object) MonnitUtil.MinPasswordLength;
    return MonnitSession.CurrentTheme.Theme == "Downloads" ? (ActionResult) this.View(nameof (ChangePassword), "Download") : (ActionResult) this.View((object) model);
  }

  public ActionResult ChangePasswordSuccess()
  {
    return MonnitSession.CurrentTheme.Theme == "Downloads" ? (ActionResult) this.View(nameof (ChangePasswordSuccess), "Download") : (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public ActionResult ErrorDisplay(ErrorModel model)
  {
    if (model == null)
      model = MonnitSession.ErrorDisplayModel;
    return (ActionResult) this.View((object) model);
  }

  public ActionResult SetHomepage(long customerid, string link)
  {
    try
    {
      Customer customer = Customer.Load(customerid);
      if (!string.IsNullOrEmpty(link))
        link = OverviewController.ConvertToLocalURL(link);
      customer.HomepageLink = link;
      customer.Save();
      if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
        MonnitSession.CurrentCustomer = customer;
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  public static string ConvertToLocalURL(string link)
  {
    Uri result;
    bool flag = Uri.TryCreate(link, UriKind.Absolute, out result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
    return result.PathAndQuery.ToString();
  }

  [HttpGet]
  [AuthorizeDefault]
  public ActionResult _IndexRules()
  {
    return (ActionResult) this.PartialView("~/Views/Overview/_IndexRules.ascx");
  }

  [HttpGet]
  [AuthorizeDefault]
  public ActionResult _IndexDevices()
  {
    return (ActionResult) this.PartialView("~/Views/Overview/_IndexDevices.ascx");
  }

  [HttpGet]
  [AuthorizeDefault]
  public ActionResult _IndexFavorites()
  {
    return (ActionResult) this.PartialView("~/Views/Overview/_IndexFavorites.ascx");
  }

  [HttpGet]
  [AuthorizeDefault]
  public ActionResult _IndexSensorsChart()
  {
    return (ActionResult) this.PartialView("~/Views/Overview/_IndexSensorsChart.ascx");
  }

  [HttpGet]
  [AuthorizeDefault]
  public ActionResult _IndexGatewaysChart()
  {
    return (ActionResult) this.PartialView("~/Views/Overview/_IndexGatewaysChart.ascx");
  }

  [HttpGet]
  [AuthorizeDefault]
  public ActionResult _IndexAccount()
  {
    return (ActionResult) this.PartialView("~/Views/Overview/_IndexAccount.ascx");
  }

  [HttpGet]
  [AuthorizeDefault]
  public ActionResult _IndexReports()
  {
    List<ReportSchedule> reportScheduleList = ReportSchedule.LoadByAccount(MonnitSession.CurrentCustomer.AccountID);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__17.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__17.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<ReportSchedule>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Reports", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = OverviewController.\u003C\u003Eo__17.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__17.\u003C\u003Ep__0, this.ViewBag, reportScheduleList);
    return (ActionResult) this.PartialView("~/Views/Overview/_IndexReports.ascx");
  }

  [HttpPost]
  [AuthorizeDefault]
  public ActionResult FavoritesToggle(long id, bool isFav, string type)
  {
    string content = "Success";
    try
    {
      if (isFav)
      {
        CustomerFavorite customerFavorite = (CustomerFavorite) null;
        bool flag = false;
        switch (type)
        {
          case "sensor":
            customerFavorite = MonnitSession.CurrentCustomerFavorites.AllFavorites.Find((Predicate<CustomerFavorite>) (x => x.SensorID == id));
            flag = true;
            break;
          case "gateway":
            customerFavorite = MonnitSession.CurrentCustomerFavorites.AllFavorites.Find((Predicate<CustomerFavorite>) (x => x.GatewayID == id));
            flag = true;
            break;
          case "visualmap":
            customerFavorite = MonnitSession.CurrentCustomerFavorites.AllFavorites.Find((Predicate<CustomerFavorite>) (x => x.VisualMapID == id));
            flag = true;
            break;
          case "notification":
            customerFavorite = MonnitSession.CurrentCustomerFavorites.AllFavorites.Find((Predicate<CustomerFavorite>) (x => x.NotificationID == id));
            flag = true;
            break;
          case "reportschedule":
            customerFavorite = MonnitSession.CurrentCustomerFavorites.AllFavorites.Find((Predicate<CustomerFavorite>) (x => x.ReportScheduleID == id));
            flag = true;
            break;
          case "location":
            customerFavorite = MonnitSession.CurrentCustomerFavorites.AllFavorites.Find((Predicate<CustomerFavorite>) (x => x.LocationID == id));
            flag = true;
            break;
        }
        MonnitSession.CurrentCustomerFavorites.Invalidate();
        if (!flag)
          return (ActionResult) this.Content("Failed to toggle favorite. Invalid CustomerFavorite Type");
        if (customerFavorite == null)
          return (ActionResult) this.Content("Failed to toggle favorite. Invalid CustomerFavoriteID");
        customerFavorite.Delete();
      }
      else
      {
        int num = MonnitSession.IsCurrentCustomerMonnitAdmin ? ConfigData.AppSettings("MaxMonnitAdminFavorites", "100").ToInt() : ConfigData.AppSettings("MaxCustomerFavorites", "25").ToInt();
        if (MonnitSession.CurrentCustomerFavorites.AllFavorites.Count > num)
          return (ActionResult) this.Content($"Failed to add favorite. Favorite limit of ${num} has been reached.");
        CustomerFavorite customerFavorite = new CustomerFavorite();
        customerFavorite.AccountID = MonnitSession.CurrentCustomer.AccountID;
        customerFavorite.CustomerID = MonnitSession.CurrentCustomer.CustomerID;
        bool flag1 = false;
        bool flag2 = false;
        switch (type)
        {
          case "sensor":
            customerFavorite.SensorID = id;
            flag2 = MonnitSession.CurrentCustomerFavorites.AllFavorites.Where<CustomerFavorite>((System.Func<CustomerFavorite, bool>) (x => x.SensorID == customerFavorite.SensorID)).Count<CustomerFavorite>() > 0;
            flag1 = true;
            break;
          case "gateway":
            customerFavorite.GatewayID = id;
            flag2 = MonnitSession.CurrentCustomerFavorites.AllFavorites.Where<CustomerFavorite>((System.Func<CustomerFavorite, bool>) (x => x.GatewayID == customerFavorite.GatewayID)).Count<CustomerFavorite>() > 0;
            flag1 = true;
            break;
          case "visualmap":
            customerFavorite.VisualMapID = id;
            flag2 = MonnitSession.CurrentCustomerFavorites.AllFavorites.Where<CustomerFavorite>((System.Func<CustomerFavorite, bool>) (x => x.VisualMapID == customerFavorite.VisualMapID)).Count<CustomerFavorite>() > 0;
            flag1 = true;
            break;
          case "notification":
            customerFavorite.NotificationID = id;
            flag2 = MonnitSession.CurrentCustomerFavorites.AllFavorites.Where<CustomerFavorite>((System.Func<CustomerFavorite, bool>) (x => x.NotificationID == customerFavorite.NotificationID)).Count<CustomerFavorite>() > 0;
            flag1 = true;
            break;
          case "reportschedule":
            customerFavorite.ReportScheduleID = id;
            flag2 = MonnitSession.CurrentCustomerFavorites.AllFavorites.Where<CustomerFavorite>((System.Func<CustomerFavorite, bool>) (x => x.ReportScheduleID == customerFavorite.ReportScheduleID)).Count<CustomerFavorite>() > 0;
            flag1 = true;
            break;
          case "location":
            customerFavorite.LocationID = id;
            flag2 = MonnitSession.CurrentCustomerFavorites.AllFavorites.Where<CustomerFavorite>((System.Func<CustomerFavorite, bool>) (x => x.LocationID == customerFavorite.LocationID)).Count<CustomerFavorite>() > 0;
            flag1 = true;
            break;
          default:
            content = "Failed to toggle favorite. Invalid CustomerFavorite Type";
            break;
        }
        MonnitSession.CurrentCustomerFavorites.Invalidate();
        if (!flag1)
          return (ActionResult) this.Content("Failed to toggle favorite. Invalid CustomerFavorite Type");
        if (flag2)
          return (ActionResult) this.Content("Failed to toggle favorite. Duplicate CustomerFavorite");
        customerFavorite.Save();
        customerFavorite.OrderNum = customerFavorite.CustomerFavoriteID;
        customerFavorite.Save();
      }
    }
    catch (Exception ex)
    {
      try
      {
        MonnitSession.CurrentCustomerFavorites.Invalidate();
      }
      catch
      {
      }
      ex.Log($"OverviewController.FavoritesAssign[ID: {id.ToString()}][Type: {type}] ");
      return (ActionResult) this.Content("Failed to toggle favorite. Please refresh the page and try again.");
    }
    return (ActionResult) this.Content(content);
  }

  [HttpPost]
  [AuthorizeDefault]
  public ActionResult UpdateFavoritesOrder(string favOrders)
  {
    bool flag = false;
    string str1 = favOrders;
    char[] chArray = new char[1]{ '|' };
    foreach (string str2 in str1.Split(chArray))
    {
      try
      {
        string[] strArray = str2.Split('_');
        long favId = strArray[0].ToLong();
        long num = strArray[1].ToLong();
        CustomerFavorite customerFavorite = MonnitSession.CurrentCustomerFavorites.AllFavorites.Where<CustomerFavorite>((System.Func<CustomerFavorite, bool>) (x => x.CustomerFavoriteID == favId)).FirstOrDefault<CustomerFavorite>();
        customerFavorite.OrderNum = num;
        customerFavorite.Save();
      }
      catch
      {
        flag = true;
      }
    }
    try
    {
      MonnitSession.CurrentCustomerFavorites.Invalidate();
    }
    catch
    {
      flag = true;
    }
    return flag ? (ActionResult) this.Content("Error") : (ActionResult) this.Content("Success");
  }

  [HttpPost]
  [AuthorizeDefault]
  public ActionResult FavoritesSwapOrder(long id, int index, long id2, int index2, string type)
  {
    string content;
    try
    {
      CustomerFavorite customerFavorite1 = CustomerFavorite.Load(id);
      CustomerFavorite customerFavorite2 = CustomerFavorite.Load(id2);
      customerFavorite1.OrderNum = (long) index;
      customerFavorite2.OrderNum = (long) index2;
      customerFavorite1.Save();
      customerFavorite2.Save();
      MonnitSession.CurrentCustomerFavorites.Invalidate();
      content = "Success";
    }
    catch (Exception ex)
    {
      ex.Log($"OverviewController.FavoritesSwapOrder[ID: {id.ToString()}][ID2: {id2.ToString()}][Index: {index.ToString()}][Index2: {index2.ToString()}] ");
      content = "Failed to swap positions.  Please try again.";
    }
    return (ActionResult) this.Content(content);
  }

  public static List<NotificationRecorded> indexNotiList()
  {
    long csNetId = MonnitSession.SensorListFilters.CSNetID;
    if (csNetId == long.MinValue)
    {
      CSNet csNet = CSNetControllerBase.GetListOfNetworksUserCanSee(new long?()).FirstOrDefault<CSNet>();
      if (csNet != null)
        csNetId = csNet.CSNetID;
    }
    int count = 10;
    return NotificationRecorded.LoadRecentByCSNetID(csNetId, count);
  }

  public ActionResult LoadIndexDetails(long csnetid)
  {
    MonnitSession.SensorListFilters.CSNetID = csnetid;
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__22.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__22.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<NotificationRecorded>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiList", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = OverviewController.\u003C\u003Eo__22.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__22.\u003C\u003Ep__0, this.ViewBag, OverviewController.indexNotiList());
    return (ActionResult) this.PartialView("_IndexDetails", (object) CSNet.Load(csnetid));
  }

  public ActionResult ReleaseNoteWindows()
  {
    return (ActionResult) this.View((object) (List<AccountThemeReleaseNoteModel>) this.TempData["rnList"]);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ReleaseNoteWindows(FormCollection collection)
  {
    foreach (string allKey in collection.AllKeys)
      MonnitSession.CurrentCustomer.AcknowledgeReleaseNote(collection[allKey].ToLong(), MonnitSession.CurrentCustomer.CustomerID, true);
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    });
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult UpdateEULA(UpdateEulas model)
  {
    if (MonnitSession.CurrentCustomer == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "LogOnOV",
        controller = "Account"
      });
    if (!model.EULAUpdate)
      this.ModelState.AddModelError("Required", "Required");
    if (!this.ModelState.IsValid)
      return (ActionResult) this.View((object) model);
    this.RecordEULA();
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    });
  }

  public ActionResult Legal() => (ActionResult) this.View();

  public ActionResult About() => (ActionResult) this.View();

  [NoCache]
  [AuthorizeDefault]
  public ActionResult SensorDisplay(long? id)
  {
    long? nullable;
    int num1;
    if (id.HasValue)
    {
      nullable = id;
      long num2 = 0;
      num1 = nullable.GetValueOrDefault() > num2 & nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    if (num1 != 0)
    {
      MonnitSession.SensorListFilters.CSNetID = id.Value;
    }
    else
    {
      nullable = id;
      long num3 = -1;
      if (nullable.GetValueOrDefault() == num3 & nullable.HasValue)
        MonnitSession.SensorListFilters.CSNetID = long.MinValue;
    }
    MonnitSession.SensorListFilters.ApplicationID = long.MinValue;
    int totalSensors;
    List<SensorGroupSensorModel> sensorList = SensorControllerBase.GetSensorList(out totalSensors);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__28.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__28.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalSensors", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = OverviewController.\u003C\u003Eo__28.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__28.\u003C\u003Ep__0, this.ViewBag, totalSensors);
    return (ActionResult) this.View((object) sensorList);
  }

  public ActionResult LoadDisplayData(long id, long? appid)
  {
    MonnitSession.SensorListFilters.CSNetID = id > 0L ? id : long.MinValue;
    MonnitSession.SensorListFilters.ApplicationID = !appid.HasValue ? long.MinValue : (appid.Value <= 0L ? long.MinValue : appid.Value);
    int totalSensors;
    List<SensorGroupSensorModel> sensorList = SensorControllerBase.GetSensorList(out totalSensors);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__29.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__29.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalSensors", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = OverviewController.\u003C\u003Eo__29.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__29.\u003C\u003Ep__0, this.ViewBag, totalSensors);
    return (ActionResult) this.PartialView("_DataDisplay", (object) sensorList);
  }

  public ActionResult SensorProfiles(long? id)
  {
    List<MonnitApplication> monnitApplicationList = new List<MonnitApplication>();
    return (ActionResult) this.PartialView("_SensorProfiles", !id.HasValue ? (object) MonnitApplication.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID) : (id.Value <= 0L ? (object) MonnitApplication.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID) : (object) MonnitApplication.LoadByCSNetID(id.Value)));
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult SensorIndex(
    long? id,
    string name,
    string profile,
    string status,
    string sort)
  {
    if (MonnitSession.CurrentCustomer == null)
      return (ActionResult) this.Redirect("/Overview/LogOff");
    List<CSNet> networksUserCanSee = CSNetControllerBase.GetListOfNetworksUserCanSee(new long?(MonnitSession.CurrentCustomer.AccountID));
    if (id.HasValue)
    {
      if (id.ToLong() != -1L && networksUserCanSee.Find((Predicate<CSNet>) (n =>
      {
        long csNetId = n.CSNetID;
        long? nullable = id;
        long valueOrDefault = nullable.GetValueOrDefault();
        return csNetId == valueOrDefault & nullable.HasValue;
      })) == null)
      {
        if (MonnitSession.SensorListFilters.CSNetID < 0L)
        {
          if (networksUserCanSee.Count > 0)
            id = new long?(networksUserCanSee[0].CSNetID);
        }
        else if (networksUserCanSee.Find((Predicate<CSNet>) (n => n.CSNetID == MonnitSession.SensorListFilters.CSNetID)) != null)
          id = new long?(MonnitSession.SensorListFilters.CSNetID);
        else if (networksUserCanSee.Count > 0)
          id = new long?(networksUserCanSee[0].CSNetID);
      }
    }
    else if (MonnitSession.SensorListFilters.CSNetID < 0L)
    {
      if (networksUserCanSee.Count > 0)
        id = new long?(networksUserCanSee[0].CSNetID);
    }
    else if (networksUserCanSee.Find((Predicate<CSNet>) (n => n.CSNetID == MonnitSession.SensorListFilters.CSNetID)) != null)
      id = new long?(MonnitSession.SensorListFilters.CSNetID);
    else if (networksUserCanSee.Count > 0)
      id = new long?(networksUserCanSee[0].CSNetID);
    long? nullable1 = id;
    long num = 0;
    if (nullable1.GetValueOrDefault() > num & nullable1.HasValue)
    {
      CSNet csNet = networksUserCanSee.Find((Predicate<CSNet>) (n =>
      {
        long csNetId = n.CSNetID;
        long? nullable2 = id;
        long valueOrDefault = nullable2.GetValueOrDefault();
        return csNetId == valueOrDefault & nullable2.HasValue;
      }));
      MonnitSession.SensorListFilters.CSNetID = csNet == null || csNet.AccountID != MonnitSession.CurrentCustomer.AccountID ? long.MinValue : id.ToLong();
    }
    else
      MonnitSession.SensorListFilters.CSNetID = long.MinValue;
    this.PrimaryContactNotifications();
    MonnitSession.SensorListFilters.ApplicationID = long.MinValue;
    MonnitSession.SensorListFilters.Status = int.MinValue;
    MonnitSession.SensorListFilters.Name = "";
    MonnitSession.SensorListFilters.Custom = "";
    if (!string.IsNullOrEmpty(status) && status.ToLower() != "all")
      MonnitSession.SensorListFilters.Status = (int) Enum.Parse(typeof (eSensorStatus), status);
    MonnitSession.SensorListFilters.ApplicationID = string.IsNullOrEmpty(profile) || profile.ToLong() == -1L ? long.MinValue : profile.ToLong();
    MonnitSession.SensorListFilters.Name = string.IsNullOrEmpty(name) ? this.Session["CurrentName"].ToString() : name.ToString();
    MonnitSession.SensorListFilters.Custom = string.IsNullOrEmpty(sort) ? this.Session["CurrentCustom"].ToString() : sort.ToString();
    if (MonnitSession.SensorListFilters.CSNetID == long.MinValue)
      this.ViewData["AppList"] = (object) MonnitApplication.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
    else
      this.ViewData["AppList"] = (object) MonnitApplication.LoadByCSNetID(MonnitSession.SensorListFilters.CSNetID);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__31.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__31.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "netID", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = OverviewController.\u003C\u003Eo__31.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__31.\u003C\u003Ep__0, this.ViewBag, id);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__31.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__31.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "LeftNavSelection", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = OverviewController.\u003C\u003Eo__31.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__31.\u003C\u003Ep__1, this.ViewBag, "Sensor");
    int totalSensors;
    List<SensorGroupSensorModel> sensorList = SensorControllerBase.GetSensorList(out totalSensors);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__31.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__31.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalSensors", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = OverviewController.\u003C\u003Eo__31.\u003C\u003Ep__2.Target((CallSite) OverviewController.\u003C\u003Eo__31.\u003C\u003Ep__2, this.ViewBag, totalSensors);
    DataMessage.CacheLastByAccount(MonnitSession.CurrentCustomer.AccountID, new TimeSpan(0, 1, 0));
    return (ActionResult) this.View((object) sensorList);
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult AtAGlanceOV()
  {
    int totalSensors;
    List<SensorGroupSensorModel> sensorList = SensorControllerBase.GetSensorList(out totalSensors);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__32.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__32.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalSensors", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = OverviewController.\u003C\u003Eo__32.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__32.\u003C\u003Ep__0, this.ViewBag, totalSensors);
    return (ActionResult) this.View((object) sensorList);
  }

  [NoCache]
  [Authorize]
  public ActionResult AtAGlanceRefresh()
  {
    return (ActionResult) this.View((object) SensorControllerBase.GetSensorList());
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult AtAGlanceGrid()
  {
    int totalSensors;
    List<SensorGroupSensorModel> sensorList = SensorControllerBase.GetSensorList(out totalSensors);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__34.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__34.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalSensors", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = OverviewController.\u003C\u003Eo__34.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__34.\u003C\u003Ep__0, this.ViewBag, totalSensors);
    return (ActionResult) this.View((object) sensorList);
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult SensorHome(long id)
  {
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    if (!MonnitSession.CustomerCan("Sensor_View_History") || sensor == null || !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    return MonnitSession.CurrentCustomer.Account.HideData ? (ActionResult) this.View("HideData", (object) sensor) : (ActionResult) this.View((object) sensor);
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult CableLog(long id)
  {
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    if (!MonnitSession.CustomerCan("Sensor_View_History") || sensor == null || !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    return MonnitSession.CurrentCustomer.Account.HideData ? (ActionResult) this.View("HideData", (object) sensor) : (ActionResult) this.View((object) sensor);
  }

  [NoCache]
  [AuthorizeDefault]
  [HttpPost]
  public ActionResult CableHistoryData(long sensorID)
  {
    Monnit.Sensor sensor = Monnit.Sensor.Load(sensorID);
    if (!MonnitSession.CustomerCan("Sensor_View_History") || sensor == null || !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return (ActionResult) this.Content("Unauthorized");
    DateTime startDate = sensor.StartDate;
    DateTime utcNow = DateTime.UtcNow;
    DataTable model = CableAudit.LoadBySensorAndDateRange(sensorID, startDate, utcNow);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__37.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__37.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Monnit.Sensor, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Sensor", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = OverviewController.\u003C\u003Eo__37.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__37.\u003C\u003Ep__0, this.ViewBag, sensor);
    return model == null ? (ActionResult) this.Content("No Data") : (ActionResult) this.PartialView(nameof (CableHistoryData), (object) model);
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult SensorHistoryRefresh(long id)
  {
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    return !MonnitSession.CustomerCan("Sensor_View_History") || sensor == null || !MonnitSession.CurrentCustomer.CanSeeSensor(sensor) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "SensorIndex",
      controller = "Overview"
    }) : (ActionResult) this.PartialView("SensorHistoryList", (object) sensor);
  }

  [NoCache]
  [AuthorizeDefault]
  [HttpPost]
  public ActionResult SensorHistoryData(long sensorID, string dataMsg)
  {
    Monnit.Sensor sensor = Monnit.Sensor.Load(sensorID);
    DateTime fromDate = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime utcFromLocalById = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    if (sensor.StartDate != DateTime.MinValue && sensor.StartDate.Ticks > fromDate.Ticks)
      fromDate = sensor.StartDate;
    List<DataMessage> list = DataMessage.LoadBySensorAndDateRange(sensorID, fromDate, utcFromLocalById, 100, new Guid?(dataMsg.ToGuid())).OrderByDescending<DataMessage, DateTime>((System.Func<DataMessage, DateTime>) (c => c.MessageDate)).ToList<DataMessage>();
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Monnit.Sensor, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Sensor", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__0, this.ViewBag, sensor);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<DataMessage>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "dm", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__1, this.ViewBag, list.Where<DataMessage>((System.Func<DataMessage, bool>) (c => c.DataMessageGUID != dataMsg.ToGuid())).ToList<DataMessage>());
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__6 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__6.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p6 = OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__6;
    bool flag = !string.IsNullOrEmpty(dataMsg);
    object obj3;
    if (flag)
    {
      // ISSUE: reference to a compiler-generated field
      if (OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__5 = CallSite<Func<CallSite, bool, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, bool, object, object> target2 = OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__5.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, bool, object, object>> p5 = OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__5;
      int num = flag ? 1 : 0;
      // ISSUE: reference to a compiler-generated field
      if (OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int, object> target3 = OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__4.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int, object>> p4 = OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__4;
      // ISSUE: reference to a compiler-generated field
      if (OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Count", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target4 = OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p3 = OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "dm", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__2.Target((CallSite) OverviewController.\u003C\u003Eo__39.\u003C\u003Ep__2, this.ViewBag);
      object obj5 = target4((CallSite) p3, obj4);
      object obj6 = target3((CallSite) p4, obj5, 0);
      obj3 = target2((CallSite) p5, num != 0, obj6);
    }
    else
      obj3 = (object) flag;
    if (target1((CallSite) p6, obj3))
      return (ActionResult) this.Content("No Data");
    return !MonnitSession.CustomerCan("Sensor_View_History") || sensor == null || !MonnitSession.CurrentCustomer.CanSeeSensor(sensor) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "SensorIndex",
      controller = "Overview"
    }) : (ActionResult) this.PartialView(nameof (SensorHistoryData));
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult EasyThermostatEdit(long id, FormCollection collection)
  {
    Monnit.Sensor.ClearCache(id);
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    if (sensor == null || !MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.Content("Failed: No Sensor Found");
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.Content("Failed: Not Authorized");
    Account account = Account.Load(sensor.AccountID);
    sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited sensor settings using EasyThermostatEdit");
    try
    {
      bool flag = Thermostat.IsFahrenheit(sensor.SensorID);
      double Fahrenheit1 = !string.IsNullOrWhiteSpace(collection["OccupiedHeatingThreshold"]) ? collection["OccupiedHeatingThreshold"].ToDouble() : (flag ? Thermostat.GetOccupiedHeatingThreshold(sensor).ToFahrenheit() : Thermostat.GetOccupiedHeatingThreshold(sensor));
      double Fahrenheit2 = !string.IsNullOrWhiteSpace(collection["OccupiedCoolingThreshold"]) ? collection["OccupiedCoolingThreshold"].ToDouble() : (flag ? Thermostat.GetOccupiedCoolingThreshold(sensor).ToFahrenheit() : Thermostat.GetOccupiedCoolingThreshold(sensor));
      double Fahrenheit3 = !string.IsNullOrWhiteSpace(collection["OccupiedCoolingBuffer"]) ? collection["OccupiedCoolingBuffer"].ToDouble() : (flag ? Thermostat.GetOccupiedCoolingBuffer(sensor).ToFahrenheit() : Thermostat.GetOccupiedCoolingBuffer(sensor));
      double Fahrenheit4 = !string.IsNullOrWhiteSpace(collection["OccupiedHeatingBuffer"]) ? collection["OccupiedHeatingBuffer"].ToDouble() : (flag ? Thermostat.GetOccupiedHeatingBuffer(sensor).ToFahrenheit() : Thermostat.GetOccupiedHeatingBuffer(sensor));
      if (flag)
      {
        Fahrenheit1 = Fahrenheit1.ToCelsius();
        Fahrenheit2 = Fahrenheit2.ToCelsius();
        Fahrenheit3 = Fahrenheit3.ToCelsius();
        Fahrenheit4 = Fahrenheit4.ToCelsius();
      }
      if (Fahrenheit1 < 10.0)
        Fahrenheit1 = 10.0;
      if (Fahrenheit1 > 36.0)
        Fahrenheit1 = 36.0;
      if (Fahrenheit2 < 14.0)
        Fahrenheit2 = 14.0;
      if (Fahrenheit2 > 40.0)
        Fahrenheit2 = 40.0;
      if (Fahrenheit2 - Fahrenheit1 >= 2.0)
      {
        Thermostat.SetOccupiedHeatingThreshold(sensor, Convert.ToInt32(Math.Round(Fahrenheit1 * 4.0)));
        Thermostat.SetOccupiedCoolingThreshold(sensor, Convert.ToInt32(Math.Round(Fahrenheit2 * 4.0)));
      }
      else
      {
        double num1 = Fahrenheit2 - Fahrenheit1;
        while (num1 < 2.0)
          ++num1;
        double num2 = Fahrenheit2 + num1;
        Thermostat.SetOccupiedHeatingThreshold(sensor, Convert.ToInt32(Math.Round(Fahrenheit1 * 4.0)));
        Thermostat.SetOccupiedCoolingThreshold(sensor, Convert.ToInt32(Math.Round(num2 * 4.0)));
      }
      if (Fahrenheit4 <= Fahrenheit3)
      {
        Thermostat.SetOccupiedHeatingBuffer(sensor, Convert.ToInt32(Math.Round(Fahrenheit4 * 4.0)));
        Thermostat.SetOccupiedCoolingBuffer(sensor, Convert.ToInt32(Math.Round(Fahrenheit3 * 4.0)));
      }
      else
      {
        double num = Fahrenheit3;
        Thermostat.SetOccupiedHeatingBuffer(sensor, Convert.ToInt32(Math.Round(num * 4.0)));
        Thermostat.SetOccupiedCoolingBuffer(sensor, Convert.ToInt32(Math.Round(Fahrenheit3 * 4.0)));
      }
      sensor.ProfileConfigDirty = true;
      sensor.Save();
      return (ActionResult) this.PartialView("_ThermostatEasy", (object) sensor);
    }
    catch (Exception ex)
    {
      ex.Log($"OverviewController.SensorEdit, SensorID=[{id}] ");
    }
    return (ActionResult) this.Content("Failed: Save Error");
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult SensorChart(long id, bool? newDevice)
  {
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    if (!MonnitSession.CustomerCan("Sensor_View_Chart") || sensor == null || !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorHome",
        controller = "Overview",
        id = id
      });
    if (MonnitSession.CurrentCustomer.Account.HideData)
      return (ActionResult) this.View("HideData", (object) sensor);
    int num = MonnitSession.CurrentCustomer.Preferences["Days to Load"].ToInt();
    DateTime date = Monnit.TimeZone.GetLocalTimeById(DateTime.UtcNow, MonnitSession.CurrentCustomer.Account.TimeZoneID).Date;
    ChartModel model = new ChartModel();
    model.Sensor = sensor;
    ChartModel chartModel1 = model;
    DateTime dateTime1 = date.AddDays(1.0);
    DateTime dateTime2 = dateTime1.AddSeconds(-1.0);
    chartModel1.ToDate = dateTime2;
    ChartModel chartModel2 = model;
    dateTime1 = date.AddDays((double) -num);
    DateTime dateTime3 = dateTime1.AddDays(1.0);
    chartModel2.FromDate = dateTime3;
    sensor.StartDate = Monnit.TimeZone.GetLocalTimeById(sensor.StartDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    if (sensor.StartDate > model.FromDate)
      model.FromDate = Monnit.TimeZone.GetLocalTimeById(sensor.StartDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    MonnitSession.HistoryFromDate = model.FromDate;
    MonnitSession.HistoryToDate = model.ToDate;
    return (ActionResult) this.View((object) model);
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult refreshNotedata(long id)
  {
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    if (!MonnitSession.CustomerCan("Sensor_View_Chart") || sensor == null || !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    if (MonnitSession.CurrentCustomer.Account.HideData)
      return (ActionResult) this.Content("");
    sensor.StartDate = Monnit.TimeZone.GetLocalTimeById(sensor.StartDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime dateTime = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime utcFromLocalById = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    if (sensor.StartDate > dateTime)
      dateTime = sensor.StartDate;
    return (ActionResult) this.PartialView("_SensorNoteList", (object) new ChartModel()
    {
      FromDate = dateTime,
      ToDate = utcFromLocalById,
      Sensor = sensor
    });
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult RefreshPeopleCounterChartData(long id, string selectedSensors)
  {
    List<Monnit.Sensor> sensors1 = new List<Monnit.Sensor>();
    string str = selectedSensors;
    char[] separator = new char[1]{ '|' };
    foreach (object o in str.Split(separator, StringSplitOptions.RemoveEmptyEntries))
    {
      Monnit.Sensor sensor = Monnit.Sensor.Load(o.ToLong());
      if (sensor != null)
        sensors1.Add(sensor);
    }
    this.ViewData[nameof (selectedSensors)] = (object) sensors1;
    Monnit.Sensor sensor1 = Monnit.Sensor.Load(id);
    DateTime utcFromLocalById1 = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime utcFromLocalById2 = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    string name = $"SensorEdit\\ApplicationCustomization\\app_{sensor1.ApplicationID.ToString("D3")}\\Chart";
    if (MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, name, "Sensor", MonnitSession.CurrentTheme.Theme))
    {
      List<Monnit.Sensor> sensors2 = new List<Monnit.Sensor>();
      sensors2.Add(sensor1);
      ChartSensorDataModel model1 = new ChartSensorDataModel(sensors1, utcFromLocalById1, utcFromLocalById2);
      try
      {
        return (ActionResult) this.PartialView($"~\\Views\\Sensor\\{name}.ascx", (object) model1);
      }
      catch (Exception ex)
      {
        ex.Log(nameof (RefreshPeopleCounterChartData));
      }
      ChartSensorDataModel model2 = new ChartSensorDataModel(sensors2, utcFromLocalById1, utcFromLocalById2);
      return (ActionResult) this.PartialView($"~\\Views\\Sensor\\{name}.ascx", (object) model2);
    }
    return (ActionResult) this.PartialView("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Chart.ascx", (object) new ChartSensorDataModel(new List<Monnit.Sensor>()
    {
      sensor1
    }, utcFromLocalById1, utcFromLocalById2));
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult RefreshChartData(long id, bool isBatteryChart)
  {
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    if (!MonnitSession.CustomerCan("Sensor_View_Chart") || sensor == null || !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    if (MonnitSession.CurrentCustomer.Account.HideData)
      return (ActionResult) this.Content("");
    DateTime localTimeById = Monnit.TimeZone.GetLocalTimeById(DateTime.UtcNow.AddDays(-366.0), MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime dateTime;
    if (MonnitSession.CustomerCan("Support_Advanced"))
    {
      dateTime = DateTime.UtcNow;
      localTimeById = Monnit.TimeZone.GetLocalTimeById(dateTime.AddDays(-1098.0), MonnitSession.CurrentCustomer.Account.TimeZoneID);
    }
    if (!MonnitSession.IsEnterprise && MonnitSession.HistoryFromDate < localTimeById)
      MonnitSession.HistoryFromDate = localTimeById;
    if (sensor.StartDate > Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID) && !MonnitSession.CustomerCan("Support_Advanced"))
      MonnitSession.HistoryFromDate = sensor.StartDate;
    if (MonnitSession.HistoryFromDate > MonnitSession.HistoryToDate)
    {
      dateTime = MonnitSession.HistoryFromDate;
      MonnitSession.HistoryToDate = dateTime.AddDays(1.0);
    }
    dateTime = MonnitSession.HistoryToDate;
    if (dateTime.Subtract(MonnitSession.HistoryFromDate).TotalDays > 365.0)
    {
      dateTime = MonnitSession.HistoryToDate;
      MonnitSession.HistoryFromDate = dateTime.AddDays(-365.0);
    }
    DateTime utcFromLocalById1 = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime utcFromLocalById2 = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    int days = utcFromLocalById2.Subtract(utcFromLocalById1).Days;
    string name1 = $"SensorEdit\\ApplicationCustomization\\app_{sensor.ApplicationID.ToString("D3")}\\PreAggChart";
    if (!ConfigData.AppSettings("IsEnterprise").ToBool() && days >= 7 && MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, name1, "Sensor", MonnitSession.CurrentTheme.Theme))
    {
      PreAggregatePageModel model = new PreAggregatePageModel(PreAggregatedData.LoadBySensorIDAndDateRange(id, utcFromLocalById1, utcFromLocalById2), sensor);
      DateTime utcNow = DateTime.UtcNow;
      DateTime UTCTime = utcNow.AddDays(-1.0);
      Monnit.TimeZone.GetLocalTimeById(utcNow, MonnitSession.CurrentCustomer.Account.TimeZoneID);
      DateTime yesterdayLocalDate = Monnit.TimeZone.GetLocalTimeById(UTCTime, MonnitSession.CurrentCustomer.Account.TimeZoneID).Date;
      if (MonnitSession.HistoryFromDate <= yesterdayLocalDate && MonnitSession.HistoryToDate >= yesterdayLocalDate && model.PreAggregateList.Where<PreAggregatedData>((System.Func<PreAggregatedData, bool>) (pag => pag.Date == yesterdayLocalDate)).Count<PreAggregatedData>() == 0)
      {
        DateTime utcFromLocalById3 = Monnit.TimeZone.GetUTCFromLocalById(yesterdayLocalDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
        List<PreAggregatedData> collection = PreAggregatedData.PreAggChartByDate(id, utcFromLocalById3, yesterdayLocalDate);
        model.PreAggregateList.AddRange((IEnumerable<PreAggregatedData>) collection);
      }
      if (MonnitSession.HistoryFromDate <= yesterdayLocalDate && MonnitSession.HistoryToDate >= yesterdayLocalDate && model.PreAggregateList.Where<PreAggregatedData>((System.Func<PreAggregatedData, bool>) (pag => pag.Date == yesterdayLocalDate)).Count<PreAggregatedData>() == 0)
      {
        DateTime utcFromLocalById4 = Monnit.TimeZone.GetUTCFromLocalById(yesterdayLocalDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
        List<PreAggregatedData> collection = PreAggregatedData.PreAggChartByDate(id, utcFromLocalById4, yesterdayLocalDate);
        model.PreAggregateList.AddRange((IEnumerable<PreAggregatedData>) collection);
      }
      return isBatteryChart ? (ActionResult) this.PartialView("~\\Views\\Overview\\_PreAggBatteryChart.ascx", (object) model) : (ActionResult) this.PartialView($"~\\Views\\Sensor\\{name1}.ascx", (object) model);
    }
    if (isBatteryChart)
      return (ActionResult) this.PartialView("~\\Views\\Overview\\_BatteryChart.ascx", (object) new ChartSensorDataModel(new List<Monnit.Sensor>()
      {
        sensor
      }, utcFromLocalById1, utcFromLocalById2));
    string name2 = $"SensorEdit\\ApplicationCustomization\\app_{sensor.ApplicationID.ToString("D3")}\\Chart";
    if (MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, name2, "Sensor", MonnitSession.CurrentTheme.Theme))
    {
      ChartSensorDataModel model = new ChartSensorDataModel(new List<Monnit.Sensor>()
      {
        sensor
      }, utcFromLocalById1, utcFromLocalById2);
      return (ActionResult) this.PartialView($"~\\Views\\Sensor\\{name2}.ascx", (object) model);
    }
    return (ActionResult) this.PartialView("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Chart.ascx", (object) new ChartSensorDataModel(new List<Monnit.Sensor>()
    {
      sensor
    }, utcFromLocalById1, utcFromLocalById2));
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult SetSensorActive(long id)
  {
    try
    {
      Monnit.Sensor DBObject1 = Monnit.Sensor.Load(id);
      if (DBObject1 == null || !MonnitSession.IsAuthorizedForAccount(DBObject1.AccountID))
        return (ActionResult) this.Content($"SensorID: {id} could not be updated");
      Account account = Account.Load(CSNet.Load(DBObject1.CSNetID).AccountID);
      DBObject1.LogAuditData(eAuditAction.Related_Modify, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited sensor active status");
      DBObject1.IsActive = true;
      DBObject1.MarkClean(false);
      if (DBObject1.IsWiFiSensor)
      {
        Monnit.Gateway DBObject2 = Monnit.Gateway.LoadBySensorID(DBObject1.SensorID);
        DBObject2.LogAuditData(eAuditAction.Related_Modify, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited wifi sensor active status");
        DBObject2.MarkClean(false);
        DBObject2.Save();
      }
      DBObject1.Save();
    }
    catch (Exception ex)
    {
      ex.Log($"NetworkController.SetSensorActive | SensorID: {id} could not be transfered to new network");
      return (ActionResult) this.Content($"SensorID: {id} could not be updated");
    }
    return (ActionResult) this.Content("Success!");
  }

  [AuthorizeDefault]
  public ActionResult SensorNotification(long id)
  {
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.PartialView("Unauthorized");
    this.ViewData["Sensor"] = (object) sensor;
    this.ViewData["DatumIndexFilter"] = (object) -1;
    this.ViewData["Notifications"] = (object) NotificationRecorded.LoadBySensorAndDateRange(id, Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID), Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID), 100);
    return (ActionResult) this.View((object) Notification.LoadBySensorID(id).Where<Notification>((System.Func<Notification, bool>) (noti => noti.AccountID == sensor.AccountID)).ToList<Notification>());
  }

  [AuthorizeDefault]
  public ActionResult AvailableRulesForGateway(
    long id,
    string eventType,
    string nameFilter,
    string status,
    string assigned)
  {
    Monnit.Gateway gtwy = Monnit.Gateway.Load(id);
    IEnumerable<AvailableNotificationByGatewayModel> source = (IEnumerable<AvailableNotificationByGatewayModel>) AvailableNotificationByGatewayModel.Load(gtwy);
    int num = source.Count<AvailableNotificationByGatewayModel>();
    if (!string.IsNullOrEmpty(eventType))
      source = source.Where<AvailableNotificationByGatewayModel>((System.Func<AvailableNotificationByGatewayModel, bool>) (n => n.Notification.NotificationClass.ToString() == eventType));
    if (!string.IsNullOrEmpty(nameFilter))
      source = source.Where<AvailableNotificationByGatewayModel>((System.Func<AvailableNotificationByGatewayModel, bool>) (n => n.Notification.Name.ToLower().Contains(nameFilter.ToLower())));
    if (!string.IsNullOrEmpty(status))
      source = source.Where<AvailableNotificationByGatewayModel>((System.Func<AvailableNotificationByGatewayModel, bool>) (n => n.Notification.IsActive == status.ToBool()));
    if (!string.IsNullOrEmpty(assigned))
      source = source.Where<AvailableNotificationByGatewayModel>((System.Func<AvailableNotificationByGatewayModel, bool>) (n => n.GatewayNotificationID > 0L == assigned.ToBool()));
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__47.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__47.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "FilteredRules", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = OverviewController.\u003C\u003Eo__47.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__47.\u003C\u003Ep__0, this.ViewBag, source.Count<AvailableNotificationByGatewayModel>());
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__47.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__47.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalRules", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = OverviewController.\u003C\u003Eo__47.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__47.\u003C\u003Ep__1, this.ViewBag, num);
    this.ViewData["Gateway"] = (object) gtwy;
    return (ActionResult) this.PartialView("AvailableGatewayActions", (object) source.ToList<AvailableNotificationByGatewayModel>());
  }

  [AuthorizeDefault]
  public ActionResult AvailableRulesForSensor(
    long id,
    string eventType,
    string datumIndex,
    string nameFilter,
    string status,
    string assigned)
  {
    Monnit.Sensor sens = Monnit.Sensor.Load(id);
    IEnumerable<AvailableNotificationBySensorModel> source = (IEnumerable<AvailableNotificationBySensorModel>) AvailableNotificationBySensorModel.Load(sens);
    int DatumIndexFilter = -1;
    int num = source.Count<AvailableNotificationBySensorModel>();
    if (!string.IsNullOrEmpty(eventType))
      source = source.Where<AvailableNotificationBySensorModel>((System.Func<AvailableNotificationBySensorModel, bool>) (n => n.Notification.NotificationClass.ToString() == eventType));
    if (eventType == "Application" && !string.IsNullOrEmpty(datumIndex))
    {
      DatumIndexFilter = datumIndex.Split('&')[1].ToInt();
      source = source.Select<AvailableNotificationBySensorModel, AvailableNotificationBySensorModel>((System.Func<AvailableNotificationBySensorModel, AvailableNotificationBySensorModel>) (a => new AvailableNotificationBySensorModel()
      {
        Notification = a.Notification,
        DetailsList = a.DetailsList.FindAll((Predicate<AvailableNotificationBySensorDetailsModel>) (d => d.DatumIndex == DatumIndexFilter))
      }));
    }
    if (!string.IsNullOrEmpty(nameFilter))
      source = source.Where<AvailableNotificationBySensorModel>((System.Func<AvailableNotificationBySensorModel, bool>) (n => n.Notification.Name.ToLower().Contains(nameFilter.ToLower())));
    if (!string.IsNullOrEmpty(status))
      source = source.Where<AvailableNotificationBySensorModel>((System.Func<AvailableNotificationBySensorModel, bool>) (n => n.Notification.IsActive == status.ToBool()));
    if (!string.IsNullOrEmpty(assigned))
      source = source.Select<AvailableNotificationBySensorModel, AvailableNotificationBySensorModel>((System.Func<AvailableNotificationBySensorModel, AvailableNotificationBySensorModel>) (a => new AvailableNotificationBySensorModel()
      {
        Notification = a.Notification,
        DetailsList = a.DetailsList.FindAll((Predicate<AvailableNotificationBySensorDetailsModel>) (d => d.SensorNotificationID > 0L == assigned.ToBool()))
      }));
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__48.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__48.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "FilteredRules", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = OverviewController.\u003C\u003Eo__48.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__48.\u003C\u003Ep__0, this.ViewBag, source.Count<AvailableNotificationBySensorModel>());
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__48.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__48.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalRules", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = OverviewController.\u003C\u003Eo__48.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__48.\u003C\u003Ep__1, this.ViewBag, num);
    this.ViewData["Sensor"] = (object) sens;
    return (ActionResult) this.PartialView("AvailableSensorActions", (object) source.ToList<AvailableNotificationBySensorModel>());
  }

  [AuthorizeDefault]
  public ActionResult RemoveExistingNotificationFromSensor(
    long sensorID,
    long notificationID,
    int datumIndex = 0)
  {
    try
    {
      Monnit.Sensor sensor = Monnit.Sensor.Load(sensorID);
      CSNet csNet = sensor != null ? CSNet.Load(sensor.CSNetID) : throw new Exception("Unknown Sensor");
      Account account = csNet != null ? Account.Load(csNet.AccountID) : throw new Exception("Unknown Network");
      if (account == null)
        throw new Exception("Unknown Account");
      Notification DBObject = Notification.Load(notificationID);
      if (DBObject == null)
        throw new Exception("Unknown Notification");
      if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID) || !MonnitSession.CustomerCan("Notification_Edit") || DBObject.AccountID != csNet.AccountID)
        throw new Exception("Unauthorized");
      string changeRecord = $"{{\"SensorID\": \"{sensor.SensorID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
      DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Removed sensor from noti");
      DBObject.RemoveSensor(sensor, datumIndex);
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content(ex.Message);
    }
  }

  [AuthorizeDefault]
  public ActionResult RemoveExistingNotificationFromGateway(
    long gatewayID,
    long notificationID,
    int datumIndex = 0)
  {
    try
    {
      Monnit.Gateway gateway = Monnit.Gateway.Load(gatewayID);
      CSNet csNet = gateway != null ? CSNet.Load(gateway.CSNetID) : throw new Exception("Unknown Gateway");
      Account account = csNet != null ? Account.Load(csNet.AccountID) : throw new Exception("Unknown Network");
      if (account == null)
        throw new Exception("Unknown Account");
      Notification DBObject = Notification.Load(notificationID);
      if (DBObject == null)
        throw new Exception("Unknown Notification");
      if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID) || !MonnitSession.CustomerCan("Notification_Edit") || DBObject.AccountID != csNet.AccountID)
        throw new Exception("Unauthorized");
      string changeRecord = $"{{\"GatewayID\": \"{gateway.GatewayID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
      DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Removed gateway from noti");
      DBObject.RemoveGateway(gateway);
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content(ex.Message);
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult AddExistingNotificationToSensor(
    long sensorID,
    long notificationID,
    int datumIndex = 0)
  {
    try
    {
      Monnit.Sensor sensor = Monnit.Sensor.Load(sensorID);
      CSNet csNet = sensor != null ? CSNet.Load(sensor.CSNetID) : throw new Exception("Unknown Sensor");
      Account account = csNet != null ? Account.Load(csNet.AccountID) : throw new Exception("Unknown Network");
      if (account == null)
        throw new Exception("Unknown Account");
      Notification DBObject = Notification.Load(notificationID);
      if (DBObject == null)
        throw new Exception("Unknown Notification");
      if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID) || !MonnitSession.CustomerCan("Notification_Edit") || DBObject.AccountID != csNet.AccountID)
        throw new Exception("Unauthorized");
      if (DBObject.NotificationClass == eNotificationClass.Application)
      {
        string changeRecord = $"{{\"SensorID\": \"{sensor.SensorID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
        DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned sensor datum to noti");
      }
      else
      {
        string changeRecord = $"{{\"SensorID\": \"{sensor.SensorID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
        DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned sensor to noti");
      }
      DBObject.AddSensor(sensor, datumIndex);
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content(ex.Message);
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult AddExistingNotificationToGateway(long gatewayID, long notificationID)
  {
    try
    {
      Monnit.Gateway gateway = Monnit.Gateway.Load(gatewayID);
      CSNet csNet = gateway != null ? CSNet.Load(gateway.CSNetID) : throw new Exception("Unknown Gateway");
      Account account = csNet != null ? Account.Load(csNet.AccountID) : throw new Exception("Unknown Network");
      if (account == null)
        throw new Exception("Unknown Account");
      Notification DBObject = Notification.Load(notificationID);
      if (DBObject == null)
        throw new Exception("Unknown Notification");
      if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID) || !MonnitSession.CustomerCan("Notification_Edit") || DBObject.AccountID != csNet.AccountID)
        throw new Exception("Unauthorized");
      string changeRecord = $"{{\"GatewayID\": \"{gateway.GatewayID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
      DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned gateway to noti");
      DBObject.AddGateway(gateway);
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content(ex.Message);
    }
  }

  protected void AddTimeDropDowns(Monnit.Sensor sensor)
  {
    if (new Version(sensor.FirmwareVersion) >= new Version("1.2.009") && sensor.SensorTypeID != 4L)
    {
      this.ViewData["ShowTimeOfDay"] = (object) true;
      TimeSpan timeSpan1 = sensor.ActiveStartTime.Add(MonnitSession.UTCOffset);
      TimeSpan timeSpan2 = sensor.ActiveEndTime.Add(MonnitSession.UTCOffset);
      if (timeSpan1 == timeSpan2)
      {
        timeSpan1 = new TimeSpan(0L);
        timeSpan2 = new TimeSpan(0L);
      }
      else
      {
        if (timeSpan1.TotalMinutes < 0.0)
          timeSpan1 = timeSpan1.Add(new TimeSpan(1, 0, 0, 0));
        if (timeSpan1.Days > 0)
          timeSpan1 = timeSpan1.Subtract(new TimeSpan(1, 0, 0, 0));
        if (timeSpan2.TotalMinutes < 0.0)
          timeSpan2 = timeSpan2.Add(new TimeSpan(1, 0, 0, 0));
        if (timeSpan2.Days > 0)
          timeSpan2 = timeSpan2.Subtract(new TimeSpan(1, 0, 0, 0));
      }
      this.ViewData["StartHours"] = (object) new SelectList((IEnumerable) new string[12]
      {
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        "10",
        "11",
        "12"
      }, (object) (timeSpan1.Hours > 12 ? timeSpan1.Hours - 12 : (timeSpan1.Hours == 0 ? 12 : timeSpan1.Hours)).ToString());
      ViewDataDictionary viewData1 = this.ViewData;
      string[] items1 = new string[4]
      {
        "00",
        "15",
        "30",
        "45"
      };
      int num = timeSpan1.Minutes;
      SelectList selectList1 = new SelectList((IEnumerable) items1, (object) num.ToString());
      viewData1["StartMinutes"] = (object) selectList1;
      this.ViewData["StartAM"] = (object) new SelectList((IEnumerable) new string[2]
      {
        "AM",
        "PM"
      }, timeSpan1.Hours < 12 ? (object) "AM" : (object) "PM");
      ViewDataDictionary viewData2 = this.ViewData;
      string[] items2 = new string[12]
      {
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        "10",
        "11",
        "12"
      };
      num = timeSpan2.Hours > 12 ? timeSpan2.Hours - 12 : (timeSpan2.Hours == 0 ? 12 : timeSpan2.Hours);
      SelectList selectList2 = new SelectList((IEnumerable) items2, (object) num.ToString());
      viewData2["EndHours"] = (object) selectList2;
      ViewDataDictionary viewData3 = this.ViewData;
      string[] items3 = new string[4]
      {
        "00",
        "15",
        "30",
        "45"
      };
      num = timeSpan2.Minutes;
      SelectList selectList3 = new SelectList((IEnumerable) items3, (object) num.ToString());
      viewData3["EndMinutes"] = (object) selectList3;
      this.ViewData["EndAM"] = (object) new SelectList((IEnumerable) new string[2]
      {
        "AM",
        "PM"
      }, timeSpan2.Hours < 12 ? (object) "AM" : (object) "PM");
    }
    else
      this.ViewData["ShowTimeOfDay"] = (object) false;
  }

  protected new TimeSpan GetStartTime(FormCollection collection)
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

  protected new TimeSpan GetEndTime(FormCollection collection)
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

  [AuthorizeDefault]
  public ActionResult SensorEdit(long id)
  {
    Monnit.Sensor.ClearCache(id);
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    if (sensor == null || !MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
      });
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
      });
    this.AddTimeDropDowns(sensor);
    return (ActionResult) this.View((object) sensor);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult SensorEdit(long id, FormCollection collection)
  {
    Monnit.Sensor.ClearCache(id);
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    if (sensor == null || !MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
      });
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
      });
    string name = this.SensorEditBase(sensor, collection);
    if (name == "EditConfirmation")
      return (ActionResult) this.PartialView("EditConfirmation");
    if (!MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, name, "Sensor", MonnitSession.CurrentTheme.Theme))
      return (ActionResult) this.PartialView("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Edit.ascx", (object) sensor);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__57.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__57.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = OverviewController.\u003C\u003Eo__57.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__57.\u003C\u003Ep__0, this.ViewBag, name);
    return (ActionResult) this.PartialView($"~\\Views\\Sensor\\{name}.ascx", (object) sensor);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult MultiSensorEdit(FormCollection collection)
  {
    ActionResult actionResult = (ActionResult) null;
    if (string.IsNullOrWhiteSpace(collection["ids"]))
    {
      actionResult = (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
      });
    }
    else
    {
      string[] strArray = collection["ids"].Split(',');
      long ID1 = collection["originalID"].ToLong();
      Monnit.Sensor sensor1 = (Monnit.Sensor) null;
      foreach (object o in strArray)
      {
        long ID2 = o.ToLong();
        Monnit.Sensor.ClearCache(ID2);
        Monnit.Sensor sensor2 = Monnit.Sensor.Load(ID2);
        if (ID2 == ID1)
          sensor1 = sensor2;
        if (sensor2 == null || !MonnitSession.IsAuthorizedForAccount(sensor2.AccountID))
        {
          actionResult = (ActionResult) this.RedirectToRoute("Default", (object) new
          {
            action = "SensorIndex"
          });
          break;
        }
        if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
        {
          actionResult = (ActionResult) this.RedirectToRoute("Default", (object) new
          {
            action = "SensorIndex"
          });
          break;
        }
        string name = this.SensorEditBase(sensor2, collection);
        if (name == "EditConfirmation")
        {
          // ISSUE: reference to a compiler-generated field
          if (OverviewController.\u003C\u003Eo__58.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OverviewController.\u003C\u003Eo__58.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = OverviewController.\u003C\u003Eo__58.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__58.\u003C\u003Ep__0, this.ViewBag, "Multi Sensor Edit Pending");
          // ISSUE: reference to a compiler-generated field
          if (OverviewController.\u003C\u003Eo__58.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OverviewController.\u003C\u003Eo__58.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SensorID", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj2 = OverviewController.\u003C\u003Eo__58.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__58.\u003C\u003Ep__1, this.ViewBag, ID1.ToString());
          actionResult = (ActionResult) this.PartialView("EditConfirmation");
        }
        else
        {
          if (MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, name, "Sensor", MonnitSession.CurrentTheme.Theme))
          {
            Monnit.Sensor model = sensor1 == null ? Monnit.Sensor.Load(ID1) : sensor1;
            actionResult = (ActionResult) this.PartialView($"~\\Views\\Sensor\\{name}.ascx", (object) model);
            break;
          }
          actionResult = (ActionResult) this.PartialView("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Edit.ascx", sensor1 == null ? (object) Monnit.Sensor.Load(ID1) : (object) sensor1);
          break;
        }
      }
    }
    return actionResult;
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult MultiThermostatUiEdit(long id, FormCollection collection)
  {
    string content = "Failed to save";
    try
    {
      Monnit.Sensor.ClearCache(id);
      Monnit.Sensor sens = Monnit.Sensor.Load(id);
      int storedValue1 = collection["FanOverride"].ToInt();
      int storedValue2 = collection["OccupancyOverride"].ToInt();
      if (sens == null || !MonnitSession.IsAuthorizedForAccount(sens.AccountID))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "SensorIndex"
        });
      if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "SensorIndex"
        });
      if (storedValue1 > 0)
      {
        MultiStageThermostat.SetCalibrationValue(sens.SensorID, 4, storedValue1);
        sens.PendingActionControlCommand = true;
      }
      if (storedValue2 > 0)
      {
        MultiStageThermostat.SetCalibrationValue(sens.SensorID, 6, storedValue2);
        sens.PendingActionControlCommand = true;
      }
      if (storedValue2 < 0 && storedValue2 > int.MinValue)
      {
        MultiStageThermostat.SetCalibrationValue(sens.SensorID, 7, storedValue2 * -1);
        sens.PendingActionControlCommand = true;
      }
      MultiStageThermostat.SetProfileSettings(sens, (NameValueCollection) collection, (NameValueCollection) null);
      sens.Save();
      content = "Success";
    }
    catch (Exception ex)
    {
      ex.Log($"OverviewController.MultiThermostatUiEdit[ID: {id.ToString()}] ");
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ThermostatUiEdit(long id, FormCollection collection)
  {
    string content = "Failed to save";
    try
    {
      Monnit.Sensor.ClearCache(id);
      Monnit.Sensor sens = Monnit.Sensor.Load(id);
      if (sens == null || !MonnitSession.IsAuthorizedForAccount(sens.AccountID))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "SensorIndex"
        });
      if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "SensorIndex"
        });
      int storedValue1 = collection["FanOverride"].ToInt();
      int storedValue2 = collection["OccupancyOverride"].ToInt();
      if (storedValue1 > 0)
      {
        Thermostat.SetCalibrationValue(sens.SensorID, 4, storedValue1);
        sens.PendingActionControlCommand = true;
      }
      if (storedValue2 > 0)
      {
        Thermostat.SetCalibrationValue(sens.SensorID, 6, storedValue2);
        sens.PendingActionControlCommand = true;
      }
      if (storedValue2 < 0 && storedValue2 > int.MinValue)
      {
        Thermostat.SetCalibrationValue(sens.SensorID, 7, storedValue2 * -1);
        sens.PendingActionControlCommand = true;
      }
      double num1 = -1.0;
      double num2 = -1.0;
      double num3 = -1.0;
      double num4 = -1.0;
      if (!string.IsNullOrWhiteSpace(collection["OccupiedCoolingThreshold"]))
        num1 = collection["OccupiedCoolingThreshold"].ToDouble();
      if (!string.IsNullOrWhiteSpace(collection["OccupiedHeatingThreshold"]))
        num2 = collection["OccupiedHeatingThreshold"].ToDouble();
      if (!string.IsNullOrWhiteSpace(collection["UnoccupiedCoolingThreshold"]))
        num3 = collection["UnoccupiedCoolingThreshold"].ToDouble();
      if (!string.IsNullOrWhiteSpace(collection["UnoccupiedHeatingThreshold"]))
        num4 = collection["UnoccupiedHeatingThreshold"].ToDouble();
      ThermostatBase.SensorEdit((ISensor) sens, new double?(), new double?(), num3 < 0.0 ? new double?() : new double?(num3), num4 < 0.0 ? new double?() : new double?(num4), new double?(), new double?(), num1 < 0.0 ? new double?() : new double?(num1), num2 < 0.0 ? new double?() : new double?(num2), new double?(), new double?(), new bool?(), new int?(), new int?(), new int?(), new int?(), new int?(), new int?(), new int?(), new int?(), out bool _);
      Thermostat.SetProfileSettings(sens, (NameValueCollection) collection, (NameValueCollection) null);
      sens.Save();
      content = "Success";
    }
    catch (Exception ex)
    {
      ex.Log($"OverviewController.ThermostatUiEdit[ID: {id.ToString()}] ");
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult SensorEditNewReturn(long id, FormCollection collection)
  {
    Monnit.Sensor.ClearCache(id);
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    if (sensor == null || !MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.Content("Unauthorized");
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.Content("Unauthorized");
    string name = this.SensorEditBase(sensor, collection);
    DisplayMessageModel displayMessageModel = new DisplayMessageModel();
    displayMessageModel.Title = "Sensor Settings";
    if (name == "EditConfirmation")
    {
      displayMessageModel.Text = "Sensor settings have been successfully saved!";
      MonnitSession.DisplayMessage = displayMessageModel;
      return (ActionResult) this.PartialView("_SensorEditReload", (object) id.ToLong());
    }
    displayMessageModel.Text = "Sensor settings failed to save!";
    MonnitSession.DisplayMessage = displayMessageModel;
    if (!MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, name, "Sensor", MonnitSession.CurrentTheme.Theme))
      return (ActionResult) this.PartialView("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Edit.ascx", (object) sensor);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__61.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__61.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = OverviewController.\u003C\u003Eo__61.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__61.\u003C\u003Ep__0, this.ViewBag, name);
    return (ActionResult) this.PartialView($"~\\Views\\Sensor\\{name}.ascx", (object) sensor);
  }

  [AuthorizeDefault]
  public ActionResult InterfaceEdit(long id)
  {
    Monnit.Sensor.ClearCache(id);
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    if (sensor == null || !MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
      });
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
      });
    this.AddTimeDropDowns(sensor);
    return (ActionResult) this.PartialView((object) sensor);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult InterfaceEdit(long id, FormCollection collection)
  {
    Monnit.Sensor.ClearCache(id);
    try
    {
      Monnit.Sensor sensor = Monnit.Sensor.Load(id);
      Monnit.Gateway gateway = Monnit.Gateway.LoadBySensorID(id);
      if (gateway == null || !MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "SensorIndex"
        });
      if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "SensorIndex"
        });
      if (sensor.IsLTESensor)
        return this.InterfaceEditLTE(sensor, gateway, collection);
      if (sensor.IsPoESensor)
        return this.InterfaceEditPOE(sensor, gateway, collection);
      throw new Exception("Unkown sensor type.");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content("Failed. " + ex?.ToString());
    }
  }

  private ActionResult InterfaceEditLTE(Monnit.Sensor sensor, Monnit.Gateway gateway, FormCollection collection)
  {
    gateway.Name = collection["Name"];
    gateway.ReportInterval = collection["ReportInterval"].ToDouble();
    gateway.ObserveAware = collection["ObserveAware"].ToBool();
    gateway.GatewayPowerMode = (eGatewayPowerMode) collection["GatewayPowerMode"].ToInt();
    gateway.ResetInterval = collection["ResetInterval"].ToInt();
    gateway.ServerHostAddress = collection["ServerHostAddress"];
    gateway.Port = collection["Port"].ToInt();
    gateway.GPSReportInterval = collection["GPSReportInterval"].ToDouble();
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
    gateway.Save();
    return (ActionResult) this.PartialView("_InterfaceEditFormLTE", (object) sensor);
  }

  private ActionResult InterfaceEditPOE(Monnit.Sensor sensor, Monnit.Gateway gateway, FormCollection collection)
  {
    gateway.SingleQueueExpiration = collection["SingleQueueExpiration"].ToDouble();
    gateway.SNMPInterface1Active = this.BoolCorrection(collection["SNMPInterface1Active"]);
    if (gateway.SNMPInterface1Active)
    {
      gateway.SNMPInterfaceAddress1 = collection["SNMPInterfaceAddress1"];
      gateway.SNMPInterfaceAddress3 = collection["SNMPInterfaceAddress3"];
      gateway.SNMPInterfacePort1 = collection["SNMPInterfacePort1"].ToInt();
      gateway.SNMPCommunityString = collection["SNMPCommunityString"];
      gateway.SNMPTrap1Active = this.BoolCorrection(collection["SNMPTrap1Active"]);
      if (gateway.SNMPTrap1Active)
      {
        gateway.SNMPInterfaceAddress2 = collection["SNMPInterfaceAddress2"];
        gateway.SNMPTrapPort1 = collection["SNMPTrapPort1"].ToInt();
        gateway.SNMPTrap2Active = this.BoolCorrection(collection["SNMPTrap2Active"]);
        gateway.SNMPTrap3Active = this.BoolCorrection(collection["SNMPTrap3Active"]);
        gateway.SNMPTrap4Active = this.BoolCorrection(collection["SNMPTrap4Active"]);
      }
    }
    gateway.ModbusInterfaceActive = this.BoolCorrection(collection["ModbusInterfaceActive"]);
    if (gateway.ModbusInterfaceActive)
    {
      gateway.ModbusInterfaceTimeout = collection["ModbusInterfaceTimeout"].ToDouble();
      gateway.ModbusInterfacePort = collection["ModbusInterfacePort"].ToInt();
    }
    gateway.NTPInterfaceActive = this.BoolCorrection(collection["NTPInterfaceActive"]);
    if (gateway.NTPInterfaceActive)
    {
      gateway.NTPServerIP = collection["NTPServerIP"];
      gateway.NTPMinSampleRate = collection["NTPMinSampleRate"].ToDouble();
    }
    gateway.HTTPInterfaceActive = this.BoolCorrection(collection["HTTPInterfaceActive"]);
    if (gateway.HTTPInterfaceActive)
      gateway.HTTPServiceTimeout = collection["HTTPServiceTimeout"].ToDouble();
    gateway.Save();
    return (ActionResult) this.PartialView("_InterfaceEditForm", (object) sensor);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult OTAUpdate(long id)
  {
    string content = "Success";
    try
    {
      Monnit.Gateway gateway = Monnit.Gateway.LoadBySensorID(id);
      gateway.ForceToBootloader = true;
      gateway.Save();
    }
    catch (Exception ex)
    {
      ex.Log("OverviewController.OTAUpdate[Post] ");
      content = "Failed";
    }
    return (ActionResult) this.Content(content);
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult Control(long id)
  {
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
      });
    Monnit.Sensor model = Monnit.Sensor.Load(id);
    return model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID) || model.ApplicationID != 12L && model.ApplicationID != 76L && model.ApplicationID != 125L && model.ApplicationID != 158L ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "SensorIndex",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [AuthorizeDefault]
  [ValidateAntiForgeryToken]
  public ActionResult Control(long id, FormCollection collection)
  {
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    if (sensor == null || !MonnitSession.IsAuthorizedForAccount(sensor.AccountID) || sensor.ApplicationID != 12L && sensor.ApplicationID != 76L && sensor.ApplicationID != 158L)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    MonnitApplicationBase.ControlSensor((NameValueCollection) collection, sensor);
    return (ActionResult) this.View((object) sensor);
  }

  [AuthorizeDefault]
  public ActionResult ClearPendingControlHistory(long sensorID)
  {
    Monnit.Sensor sensor = Monnit.Sensor.Load(sensorID);
    if (sensor == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    try
    {
      NotificationRecorded.ClearMessagesBySentToDeviceID(sensor.SensorID);
    }
    catch (Exception ex)
    {
      ex.Log($"SensorController.ClearPendingControlHistory | Not Found | SensorID = {sensorID}");
      return (ActionResult) this.Content("Not Found");
    }
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Control",
      controller = "Overview",
      id = sensor.SensorID
    });
  }

  [AuthorizeDefault]
  public ActionResult SensorScale(long id)
  {
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
      });
    Monnit.Sensor model = Monnit.Sensor.Load(id);
    return model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "SensorIndex",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult SensorScale(long id, FormCollection collection)
  {
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
      });
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    if (sensor == null || !MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
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
        if (!allKey.ToLower().Contains("label") && !allKey.ToLower().Contains("datum") && !allKey.ToLower().Contains("volumeLabel") && !allKey.ToLower().Contains("heightLabel") && !allKey.ToLower().Contains("manualchkbx") && !allKey.ToLower().Contains("tempscale") && !allKey.ToLower().Contains("scale") && !allKey.ToLower().Contains("measurementscale") && !allKey.ToLower().Contains("tankdepth") && !allKey.ToLower().Contains("showdepth") && !allKey.ToLower().Contains("returns") && !allKey.ToLower().Contains("__requestverificationtoken") && !allKey.ToLower().Contains("scalemin") && !allKey.ToLower().Contains("scalemax") && !allKey.ToLower().Contains("typeofbridge") && !double.TryParse(collection[allKey].ToString(), out double _))
          throw new Exception("All fields except for the label field must be numeric");
      }
      NameValueCollection returnValues;
      MonnitApplicationBase.SensorScale(sensor, (NameValueCollection) collection, out returnValues);
      foreach (string key in returnValues.Keys)
        this.ViewData[key] = (object) returnValues[key];
      // ISSUE: reference to a compiler-generated field
      if (OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__0, this.ViewBag, collection["returns"]);
      if (!sensor.CanUpdate)
      {
        // ISSUE: reference to a compiler-generated field
        if (OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__1, this.ViewBag, "Sensor Scale Change Pending");
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__2.Target((CallSite) OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__2, this.ViewBag, "Sensor Scale Change Success");
      }
      return (ActionResult) this.PartialView("EditConfirmation");
    }
    catch (Exception ex)
    {
      Exception exception = ex;
      while (exception.InnerException != null)
        exception = exception.InnerException;
      // ISSUE: reference to a compiler-generated field
      if (OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ErrorMessage", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__3.Target((CallSite) OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__3, this.ViewBag, exception.Message);
    }
    string name1 = $"SensorEdit\\ApplicationCustomization\\app_{sensor.ApplicationID.ToString("D3")}\\Scale";
    if (!MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, name1, "Sensor", MonnitSession.CurrentTheme.Theme))
      return (ActionResult) this.PartialView("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Scale.ascx", (object) sensor);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj4 = OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__4.Target((CallSite) OverviewController.\u003C\u003Eo__71.\u003C\u003Ep__4, this.ViewBag, name1);
    return (ActionResult) this.PartialView($"~\\Views\\Sensor\\{name1}.ascx", (object) sensor);
  }

  [AuthorizeDefault]
  public ActionResult SensorDefault(long id)
  {
    try
    {
      Monnit.Sensor DBObject = Monnit.Sensor.Load(id);
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

  [AuthorizeDefault]
  public ActionResult SetTableValsToDefault(long id)
  {
    try
    {
      Monnit.Sensor DBObject = Monnit.Sensor.Load(id);
      if (DBObject == null || !MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
        return (ActionResult) this.Content("Unauthorized");
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Set Humidity_5PCal sensor settings to default");
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult CustomDefault(long id, long customCompanyID)
  {
    try
    {
      Monnit.Sensor sensor = Monnit.Sensor.Load(id);
      sensor.SetCustomCompanyDefaults(false, customCompanyID);
      sensor.Save();
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult SensorSetDefaultCalibration(long id, string url)
  {
    try
    {
      Monnit.Sensor DBObject = Monnit.Sensor.Load(id);
      Account account = Account.Load(DBObject.AccountID);
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Sensor calibration reset to defaults");
      if (DBObject.IsCableEnabled)
        DBObject.ForceOverwrite = true;
      DBObject.SetDefaultCalibration();
      DBObject.Save();
      // ISSUE: reference to a compiler-generated field
      if (OverviewController.\u003C\u003Eo__75.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OverviewController.\u003C\u003Eo__75.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = OverviewController.\u003C\u003Eo__75.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__75.\u003C\u003Ep__0, this.ViewBag, "Sensor Calibration Reset to Defaults Pending");
      // ISSUE: reference to a compiler-generated field
      if (OverviewController.\u003C\u003Eo__75.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OverviewController.\u003C\u003Eo__75.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = OverviewController.\u003C\u003Eo__75.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__75.\u003C\u003Ep__1, this.ViewBag, string.IsNullOrWhiteSpace(url) || !this.IsLocalUrl(url) ? (MonnitSession.AccountCan("sensor_calibrate") ? "/Sensor/Calibrate/" + DBObject.SensorID.ToString() : "/Sensor/Edit/" + DBObject.SensorID.ToString()) : url);
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

  [NoCache]
  [AuthorizeDefault]
  public ActionResult SensorCalibrate(long id)
  {
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
      });
    Monnit.Sensor model = Monnit.Sensor.Load(id);
    return model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "SensorIndex",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult SensorCalibrate(long id, FormCollection collection)
  {
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")) || sensor == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
      });
    Account account = Account.Load(sensor.AccountID);
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
      });
    try
    {
      sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Calibrated sensor");
      MonnitApplicationBase.CalibrateSensor((NameValueCollection) collection, sensor);
      // ISSUE: reference to a compiler-generated field
      if (OverviewController.\u003C\u003Eo__77.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OverviewController.\u003C\u003Eo__77.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = OverviewController.\u003C\u003Eo__77.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__77.\u003C\u003Ep__0, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/Overview/SensorCalibrate/" + sensor.SensorID.ToString() : collection["returns"]);
      if (!sensor.CanUpdate)
      {
        // ISSUE: reference to a compiler-generated field
        if (OverviewController.\u003C\u003Eo__77.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OverviewController.\u003C\u003Eo__77.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = OverviewController.\u003C\u003Eo__77.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__77.\u003C\u003Ep__1, this.ViewBag, "Sensor Calibration Pending");
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (OverviewController.\u003C\u003Eo__77.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OverviewController.\u003C\u003Eo__77.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = OverviewController.\u003C\u003Eo__77.\u003C\u003Ep__2.Target((CallSite) OverviewController.\u003C\u003Eo__77.\u003C\u003Ep__2, this.ViewBag, "Sensor Calibration Success");
      }
      return (ActionResult) this.PartialView("EditConfirmation");
    }
    catch (Exception ex)
    {
      Exception exception = ex;
      while (exception.InnerException != null)
        exception = exception.InnerException;
      // ISSUE: reference to a compiler-generated field
      if (OverviewController.\u003C\u003Eo__77.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OverviewController.\u003C\u003Eo__77.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ErrorMessage", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = OverviewController.\u003C\u003Eo__77.\u003C\u003Ep__3.Target((CallSite) OverviewController.\u003C\u003Eo__77.\u003C\u003Ep__3, this.ViewBag, exception.Message);
      string str = $"ApplicationSpecific\\{sensor.ApplicationID}\\Calibrate";
      return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "Sensor", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) sensor) : (ActionResult) this.PartialView((object) sensor);
    }
  }

  [AuthorizeDefault]
  public ActionResult SensorCertificate(long id)
  {
    return (ActionResult) this.View((object) Monnit.Sensor.Load(id));
  }

  [AuthorizeDefault]
  public ActionResult CalibrationCertificateCreate(long id)
  {
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    if (sensor == null || !MonnitSession.CurrentCustomer.CanSeeSensor(sensor) || !MonnitSession.CustomerCan("Sensor_Edit") || !MonnitSession.CustomerCan("Can_Create_Certificate"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__79.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__79.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Monnit.Sensor, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "sensor", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = OverviewController.\u003C\u003Eo__79.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__79.\u003C\u003Ep__0, this.ViewBag, sensor);
    CalibrationCertificate calibrationCertificate = CalibrationCertificate.LoadBySensor(sensor);
    return calibrationCertificate != null && calibrationCertificate.CertificationValidUntil > DateTime.UtcNow ? (ActionResult) this.Redirect("/Overview/CalibrationCertificateEdit/" + calibrationCertificate.CalibrationCertificateID.ToString()) : (ActionResult) this.View((object) new CalibrationCertificate());
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult CalibrationCertificateCreate(long id, FormCollection coll)
  {
    CalibrationCertificate calibrationCertificate = new CalibrationCertificate();
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    if (sensor == null || !MonnitSession.CurrentCustomer.CanSeeSensor(sensor) || !MonnitSession.CustomerCan("Sensor_Edit") || !MonnitSession.CustomerCan("Can_Create_Certificate"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__80.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__80.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Monnit.Sensor, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "sensor", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = OverviewController.\u003C\u003Eo__80.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__80.\u003C\u003Ep__0, this.ViewBag, sensor);
    try
    {
      if (sensor.IsCableEnabled)
        calibrationCertificate.CableID = sensor.CableID;
      else
        calibrationCertificate.SensorID = id;
      calibrationCertificate.CreatedByUserID = coll["userID"].ToLong();
      calibrationCertificate.DateCreated = DateTime.UtcNow;
      DateTime exact1 = DateTime.ParseExact(coll["DateCertified"].ToString(), MonnitSession.CurrentCustomer.Preferences["Date Format"].ToString(), (IFormatProvider) CultureInfo.InvariantCulture);
      calibrationCertificate.DateCertified = Monnit.TimeZone.GetUTCFromLocalById(exact1, MonnitSession.CurrentCustomer.Account.TimeZoneID);
      DateTime exact2 = DateTime.ParseExact(coll["expirationDate"].ToString(), MonnitSession.CurrentCustomer.Preferences["Date Format"].ToString(), (IFormatProvider) CultureInfo.InvariantCulture);
      calibrationCertificate.CertificationValidUntil = Monnit.TimeZone.GetUTCFromLocalById(exact2, MonnitSession.CurrentCustomer.Account.TimeZoneID);
      calibrationCertificate.CalibrationNumber = string.IsNullOrEmpty(coll["calCert"]) ? "No Certification ID Provided" : coll["calCert"];
      calibrationCertificate.CalibrationFacilityID = string.IsNullOrEmpty(coll["calFaciltyID"]) ? 4L : coll["calFaciltyID"].ToLong();
      calibrationCertificate.CertificationType = string.IsNullOrEmpty(coll["certType"]) ? "" : coll["certType"];
      calibrationCertificate.LogAuditData(eAuditAction.Related_Modify, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, sensor.AccountID, "Created Calibration Certificate");
      sensor.CalibrationCertification = calibrationCertificate.CalibrationNumber;
      sensor.CalibrationFacilityID = calibrationCertificate.CalibrationFacilityID;
      sensor.Save();
      int num = coll["reportInterval"].ToInt();
      if (num > 0 && (MonnitSession.CustomerCan("Support_Advanced") || MonnitSession.IsCurrentCustomerMonnitSuperAdmin))
      {
        try
        {
          Account account = Account.Load(sensor.AccountID);
          sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited sensor heartbeat");
          if (num > 720)
            num = 720;
          sensor.ReportInterval = (double) num;
          sensor.ActiveStateInterval = (double) num;
          sensor.Save();
        }
        catch (Exception ex)
        {
          ex.Log("CalibrationCertificateCreate. Report Interval Update Failed. Message: ");
        }
      }
      if (calibrationCertificate.CertificationValidUntil < DateTime.Now)
      {
        this.ViewData["Results"] = (object) "Failed: Please Enter Valid Date.";
      }
      else
      {
        this.ViewData["Results"] = (object) "Success";
        calibrationCertificate.Save();
      }
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
      this.ViewData["Results"] = (object) ("Failed: Cannot save certificate." + ex?.ToString());
    }
    return (ActionResult) this.View((object) calibrationCertificate);
  }

  [AuthorizeDefault]
  public ActionResult CalibrationCertificateEdit(long id)
  {
    CalibrationCertificate model = CalibrationCertificate.Load(id);
    if (model == null || !MonnitSession.CustomerCan("Can_Create_Certificate"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    Monnit.Sensor sensor = (Monnit.Sensor) null;
    if (model.SensorID > 0L)
      sensor = Monnit.Sensor.Load(model.SensorID);
    else if (model.CableID > 0L)
      sensor = Monnit.Sensor.LoadByCableID(model.CableID);
    return sensor == null || !MonnitSession.CurrentCustomer.CanSeeSensor(sensor.SensorID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "SensorIndex",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult CalibrationCertificateEdit(long id, FormCollection coll)
  {
    CalibrationCertificate DBObject = CalibrationCertificate.Load(id);
    if (DBObject == null || !MonnitSession.CurrentCustomer.CanSeeSensor(DBObject.SensorID) || !MonnitSession.CustomerCan("Can_Create_Certificate"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    Monnit.Sensor sensor = (Monnit.Sensor) null;
    try
    {
      if (DBObject.CableID > 0L)
      {
        DBObject.CableID = coll["cableID"].ToLong();
        sensor = Monnit.Sensor.LoadByCableID(DBObject.CableID);
      }
      else
      {
        DBObject.SensorID = coll["sensorID"].ToLong();
        sensor = Monnit.Sensor.Load(DBObject.SensorID);
      }
      DBObject.LogAuditData(eAuditAction.Related_Modify, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, sensor.AccountID, "Edited Calibration Certificate");
      DBObject.CreatedByUserID = coll["userID"].ToLong();
      DBObject.DateCreated = DateTime.UtcNow;
      DBObject.DateCertified = DateTime.ParseExact(coll["DateCertified"].ToString(), MonnitSession.CurrentCustomer.Preferences["Date Format"].ToString(), (IFormatProvider) CultureInfo.InvariantCulture);
      DBObject.CertificationValidUntil = DateTime.ParseExact(coll["expirationDate"].ToString(), MonnitSession.CurrentCustomer.Preferences["Date Format"].ToString(), (IFormatProvider) CultureInfo.InvariantCulture);
      DBObject.CalibrationNumber = string.IsNullOrEmpty(coll["calCert"]) ? "No Certification ID Provided" : coll["calCert"];
      DBObject.CalibrationFacilityID = string.IsNullOrEmpty(coll["calFaciltyID"]) ? 4L : coll["calFaciltyID"].ToLong();
      if (!string.IsNullOrEmpty(coll["certType"]))
        DBObject.CertificationType = coll["certType"];
      DBObject.Save();
      sensor.LogAuditData(eAuditAction.Related_Modify, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, sensor.AccountID, "Edited Calibration Certificate");
      sensor.CalibrationCertification = DBObject.CalibrationNumber;
      sensor.CalibrationFacilityID = DBObject.CalibrationFacilityID;
      sensor.Save();
      int num = coll["reportInterval"].ToInt();
      if (num > 0 && (MonnitSession.CustomerCan("Support_Advanced") || MonnitSession.IsCurrentCustomerMonnitSuperAdmin))
      {
        try
        {
          Account account = Account.Load(sensor.AccountID);
          sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited sensor heartbeat");
          if (num > 720)
            num = 720;
          sensor.ReportInterval = (double) num;
          sensor.ActiveStateInterval = (double) num;
          sensor.Save();
        }
        catch (Exception ex)
        {
          ex.Log("CalibrationCertificateCreate. Report Interval Update Failed. Message: ");
        }
      }
      this.ViewData["Results"] = (object) "Success! Calibration Certificate was modified";
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
      this.ViewData["Results"] = (object) ("Failed: Cannot save certificate." + ex?.ToString());
    }
    return (ActionResult) this.View("SensorCertificate", (object) sensor);
  }

  [AuthorizeDefault]
  public ActionResult CalibrationCertificateRemove(long id)
  {
    CalibrationCertificate calibrationCertificate = CalibrationCertificate.Load(id);
    if (calibrationCertificate == null || !MonnitSession.CustomerCan("Can_Create_Certificate"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    Monnit.Sensor model = (Monnit.Sensor) null;
    if (calibrationCertificate.SensorID > 0L)
      model = Monnit.Sensor.Load(calibrationCertificate.SensorID);
    else if (calibrationCertificate.CableID > 0L)
      model = Monnit.Sensor.LoadByCableID(calibrationCertificate.CableID);
    if (model == null || !MonnitSession.CurrentCustomer.CanSeeSensor(model.SensorID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    try
    {
      if (model.IsCableEnabled)
        calibrationCertificate.CableID = model.CableID > 0L ? model.CableID.ToLong() : throw new Exception("No cable avaliable");
      else
        calibrationCertificate.SensorID = id;
      calibrationCertificate.DeletedByUserID = MonnitSession.CurrentCustomer.CustomerID;
      calibrationCertificate.DeletedDate = DateTime.UtcNow;
      calibrationCertificate.CertificationValidUntil = calibrationCertificate.DeletedDate.AddDays(-2.0);
      calibrationCertificate.Save();
      model.CalibrationCertification = string.Empty;
      model.CalibrationFacilityID = long.MinValue;
      model.Save();
      this.ViewData["Results"] = (object) "Success! Calibration Certificate was removed";
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
      this.ViewData["Results"] = (object) ("Failed: " + ex?.ToString());
    }
    return (ActionResult) this.View("SensorCalibrate", (object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult DefaultCalibrate(long id, string url)
  {
    try
    {
      Monnit.Sensor sensor = Monnit.Sensor.Load(id.ToLong());
      sensor.SetDefaults(false);
      sensor.Save();
      // ISSUE: reference to a compiler-generated field
      if (OverviewController.\u003C\u003Eo__84.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OverviewController.\u003C\u003Eo__84.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = OverviewController.\u003C\u003Eo__84.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__84.\u003C\u003Ep__0, this.ViewBag, "Sensor Reset Defaults Pending");
      // ISSUE: reference to a compiler-generated field
      if (OverviewController.\u003C\u003Eo__84.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OverviewController.\u003C\u003Eo__84.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = OverviewController.\u003C\u003Eo__84.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__84.\u003C\u003Ep__1, this.ViewBag, string.IsNullOrWhiteSpace(url) || !this.IsLocalUrl(url) ? (MonnitSession.AccountCan("sensor_calibrate") ? "/Sensor/Calibrate/" + sensor.SensorID.ToString() : "/Sensor/Edit/" + sensor.SensorID.ToString()) : url);
      return (ActionResult) this.PartialView("EditConfirmation");
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
      return (ActionResult) this.Content("was unable to set back to defaults");
    }
  }

  [AuthorizeDefault]
  public ActionResult SetDefaultCalibration(long id, string url)
  {
    try
    {
      Monnit.Sensor DBObject = Monnit.Sensor.Load(id);
      Account account = Account.Load(DBObject.AccountID);
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Sensor calibration reset to defaults");
      DBObject.SetDefaultCalibration();
      if (DBObject.IsCableEnabled)
        DBObject.ForceOverwrite = true;
      DBObject.Save();
      // ISSUE: reference to a compiler-generated field
      if (OverviewController.\u003C\u003Eo__85.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OverviewController.\u003C\u003Eo__85.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = OverviewController.\u003C\u003Eo__85.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__85.\u003C\u003Ep__0, this.ViewBag, "Sensor Calibration Reset to Defaults Pending");
      // ISSUE: reference to a compiler-generated field
      if (OverviewController.\u003C\u003Eo__85.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OverviewController.\u003C\u003Eo__85.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = OverviewController.\u003C\u003Eo__85.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__85.\u003C\u003Ep__1, this.ViewBag, string.IsNullOrWhiteSpace(url) || !this.IsLocalUrl(url) ? (MonnitSession.AccountCan("sensor_calibrate") ? "/Overview/SensorCalibrate/" + DBObject.SensorID.ToString() : "/Overview/SensorEdit/" + DBObject.SensorID.ToString()) : url);
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

  [AuthorizeDefault]
  public ActionResult SensorNote(Guid id)
  {
    if (!MonnitSession.CustomerCan("Sensor_View_History"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    DataMessage model = DataMessage.LoadByGuid(id);
    if (model != null)
    {
      Monnit.Sensor sensor = Monnit.Sensor.Load(model.SensorID);
      if (sensor != null && MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      {
        // ISSUE: reference to a compiler-generated field
        if (OverviewController.\u003C\u003Eo__86.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OverviewController.\u003C\u003Eo__86.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Monnit.Sensor, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Sensor", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = OverviewController.\u003C\u003Eo__86.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__86.\u003C\u003Ep__0, this.ViewBag, sensor);
        return (ActionResult) this.View((object) model);
      }
    }
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "SensorIndex",
      controller = "Overview"
    });
  }

  [AuthorizeDefault]
  public ActionResult MessageNoteList(Guid id)
  {
    if (!MonnitSession.CustomerCan("Sensor_View_History"))
      return (ActionResult) this.Content("Not Authorized");
    List<DataMessageNote> model = DataMessageNote.LoadByDataMessageGUID(id);
    return model == null ? (ActionResult) this.Content("Not found") : (ActionResult) this.PartialView("DataMessageNoteList", (object) model);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult AddMessageNote(Guid id, FormCollection collection)
  {
    if (!MonnitSession.CustomerCan("Sensor_View_History"))
      return (ActionResult) this.Content("Unauthorized");
    if (string.IsNullOrEmpty(collection["note"].ToString()))
      return (ActionResult) this.Content("No Note!");
    string str = collection["note"].ToString();
    if (collection["note"].HasScriptTag())
      return (ActionResult) this.Content("Failed: Notes may not contain script tags.");
    DataMessage dataMessage = DataMessage.LoadByGuid(id);
    if (dataMessage != null)
    {
      Monnit.Sensor sensor = Monnit.Sensor.Load(dataMessage.SensorID);
      if (sensor != null && MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      {
        DataMessageNote DBObject = new DataMessageNote();
        DBObject.Note = str;
        if (string.IsNullOrWhiteSpace(str))
          return (ActionResult) this.Content("Success");
        DBObject.DataMessageGUID = id;
        DBObject.CustomerID = MonnitSession.CurrentCustomer.CustomerID;
        DBObject.NoteDate = DateTime.UtcNow;
        DBObject.SensorID = dataMessage.SensorID;
        DBObject.MessageDate = dataMessage.MessageDate;
        DBObject.Save();
        if (!dataMessage.HasNote)
        {
          dataMessage.HasNote = true;
          dataMessage.Save();
        }
        DBObject.LogAuditData(eAuditAction.Create, eAuditObject.DataMessageNote, MonnitSession.CurrentCustomer.CustomerID, sensor.AccountID, "Created a DataMessage Note");
        return (ActionResult) this.Content("Success");
      }
    }
    return (ActionResult) this.Content("Unable to Save Note!");
  }

  [AuthorizeDefault]
  public ActionResult DeleteMessageNote(long id)
  {
    if (MonnitSession.CustomerCan("Sensor_View_History"))
    {
      try
      {
        DataMessageNote DBObject = DataMessageNote.Load(id);
        if (DBObject != null)
        {
          DataMessage dataMessage = DataMessage.LoadByGuid(DBObject.DataMessageGUID);
          if (dataMessage == null)
            return (ActionResult) this.Content("Failed");
          Monnit.Sensor sensor = Monnit.Sensor.Load(dataMessage.SensorID);
          if (sensor == null)
            return (ActionResult) this.Content("Failed");
          if (!MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
            return (ActionResult) this.Content("Unauthorized");
          DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.DataMessageNote, MonnitSession.CurrentCustomer.CustomerID, sensor.AccountID, "Deleted DataMessage Note");
          DBObject.Delete();
          if (DataMessageNote.LoadByDataMessageGUID(DBObject.DataMessageGUID).Count == 0 && dataMessage != null)
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

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult SensorResetCounter(FormCollection collection, long? id)
  {
    try
    {
      Monnit.Sensor sensor = Monnit.Sensor.Load(id.ToLong());
      MonnitApplicationBase.CalibrateSensor((NameValueCollection) collection, sensor);
      // ISSUE: reference to a compiler-generated field
      if (OverviewController.\u003C\u003Eo__90.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OverviewController.\u003C\u003Eo__90.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = OverviewController.\u003C\u003Eo__90.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__90.\u003C\u003Ep__0, this.ViewBag, "Sensor Reset Counter Pending");
      // ISSUE: reference to a compiler-generated field
      if (OverviewController.\u003C\u003Eo__90.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OverviewController.\u003C\u003Eo__90.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = OverviewController.\u003C\u003Eo__90.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__90.\u003C\u003Ep__1, this.ViewBag, collection["url"]);
      return (ActionResult) this.PartialView("EditConfirmation");
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
      return (ActionResult) this.Content("Was unable to reset counter");
    }
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult GatewayIndex(
    long? id,
    string view,
    string name,
    string typeID,
    string status)
  {
    List<CSNet> networksUserCanSee = CSNetControllerBase.GetListOfNetworksUserCanSee(new long?(MonnitSession.CurrentCustomer.AccountID));
    if (id.HasValue)
    {
      if (id.ToLong() != -1L && networksUserCanSee.Find((Predicate<CSNet>) (n =>
      {
        long csNetId = n.CSNetID;
        long? nullable = id;
        long valueOrDefault = nullable.GetValueOrDefault();
        return csNetId == valueOrDefault & nullable.HasValue;
      })) == null)
      {
        if (MonnitSession.SensorListFilters.CSNetID < 0L)
        {
          if (networksUserCanSee.Count > 0)
            id = new long?(networksUserCanSee[0].CSNetID);
        }
        else if (networksUserCanSee.Find((Predicate<CSNet>) (n => n.CSNetID == MonnitSession.SensorListFilters.CSNetID)) != null)
          id = new long?(MonnitSession.SensorListFilters.CSNetID);
        else if (networksUserCanSee.Count > 0)
          id = new long?(networksUserCanSee[0].CSNetID);
      }
    }
    else if (MonnitSession.SensorListFilters.CSNetID < 0L)
    {
      if (networksUserCanSee.Count > 0)
        id = new long?(networksUserCanSee[0].CSNetID);
    }
    else if (networksUserCanSee.Find((Predicate<CSNet>) (n => n.CSNetID == MonnitSession.SensorListFilters.CSNetID)) != null)
      id = new long?(MonnitSession.SensorListFilters.CSNetID);
    else if (networksUserCanSee.Count > 0)
      id = new long?(networksUserCanSee[0].CSNetID);
    long? nullable1 = id;
    long num = 0;
    if (nullable1.GetValueOrDefault() > num & nullable1.HasValue)
    {
      CSNet csNet = networksUserCanSee.Find((Predicate<CSNet>) (n =>
      {
        long csNetId = n.CSNetID;
        long? nullable2 = id;
        long valueOrDefault = nullable2.GetValueOrDefault();
        return csNetId == valueOrDefault & nullable2.HasValue;
      }));
      MonnitSession.SensorListFilters.CSNetID = csNet == null || csNet.AccountID != MonnitSession.CurrentCustomer.AccountID ? long.MinValue : id.ToLong();
    }
    else
      MonnitSession.SensorListFilters.CSNetID = long.MinValue;
    MonnitSession.GatewayListFilters.CSNetID = MonnitSession.SensorListFilters.CSNetID;
    MonnitSession.GatewayListFilters.GatewayTypeID = long.MinValue;
    MonnitSession.GatewayListFilters.Status = int.MinValue;
    MonnitSession.GatewayListFilters.Name = "";
    if (!string.IsNullOrEmpty(status) && status.ToLower() != "all")
      MonnitSession.GatewayListFilters.Status = (int) Enum.Parse(typeof (eSensorStatus), status);
    MonnitSession.GatewayListFilters.GatewayTypeID = string.IsNullOrEmpty(typeID) || typeID.ToLong() == -1L ? long.MinValue : typeID.ToLong();
    MonnitSession.GatewayListFilters.Name = string.IsNullOrEmpty(name) ? "" : name.ToString();
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__91.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__91.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "netID", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = OverviewController.\u003C\u003Eo__91.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__91.\u003C\u003Ep__0, this.ViewBag, id);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__91.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__91.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "LeftNavSelection", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = OverviewController.\u003C\u003Eo__91.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__91.\u003C\u003Ep__1, this.ViewBag, "Gateway");
    int totalGateways;
    List<Monnit.Gateway> gatewayList = CSNetControllerBase.GetGatewayList(out totalGateways);
    this.ViewData["GatewayTypeList"] = (object) gatewayList.Select<Monnit.Gateway, GatewayType>((System.Func<Monnit.Gateway, GatewayType>) (gl => GatewayType.Load(gl.GatewayTypeID))).Distinct<GatewayType>();
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__91.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__91.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalGateways", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = OverviewController.\u003C\u003Eo__91.\u003C\u003Ep__2.Target((CallSite) OverviewController.\u003C\u003Eo__91.\u003C\u003Ep__2, this.ViewBag, totalGateways);
    return (ActionResult) this.View((object) gatewayList);
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult Gateways()
  {
    int totalGateways;
    List<Monnit.Gateway> gatewayList = CSNetControllerBase.GetGatewayList(out totalGateways);
    this.ViewData["GatewayTypeList"] = (object) gatewayList.Select<Monnit.Gateway, GatewayType>((System.Func<Monnit.Gateway, GatewayType>) (gl => GatewayType.Load(gl.GatewayTypeID))).Distinct<GatewayType>();
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__92.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__92.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalGateways", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = OverviewController.\u003C\u003Eo__92.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__92.\u003C\u003Ep__0, this.ViewBag, totalGateways);
    return (ActionResult) this.View((object) gatewayList);
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult GatewaysGrid()
  {
    int totalGateways;
    List<Monnit.Gateway> gatewayList = CSNetControllerBase.GetGatewayList(out totalGateways);
    this.ViewData["GatewayTypeList"] = (object) gatewayList.Select<Monnit.Gateway, GatewayType>((System.Func<Monnit.Gateway, GatewayType>) (gl => GatewayType.Load(gl.GatewayTypeID))).Distinct<GatewayType>();
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__93.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__93.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalGateways", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = OverviewController.\u003C\u003Eo__93.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__93.\u003C\u003Ep__0, this.ViewBag, totalGateways);
    return (ActionResult) this.View((object) gatewayList);
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult GatewaysRefresh()
  {
    return (ActionResult) this.View((object) CSNetControllerBase.GetGatewayList());
  }

  [NoCache]
  public ActionResult GatewaySortBy(string column, string direction)
  {
    MonnitSession.GatewayListSort.Name = column.ToStringSafe();
    MonnitSession.GatewayListSort.Direction = direction.ToStringSafe();
    return (ActionResult) this.Content("Success");
  }

  [NoCache]
  public ActionResult SensorSortBy(string column, string direction)
  {
    MonnitSession.SensorListSort.Name = column.ToStringSafe();
    MonnitSession.SensorListSort.Direction = direction.ToStringSafe();
    return (ActionResult) this.Content("Success");
  }

  [NoCache]
  public ActionResult FilterCSNetID(long csnetID)
  {
    MonnitSession.SensorListFilters.CSNetID = csnetID >= 0L ? csnetID : long.MinValue;
    return (ActionResult) this.Content("Success");
  }

  [NoCache]
  public ActionResult FilterAppID(long appID)
  {
    MonnitSession.SensorListFilters.ApplicationID = appID >= 0L ? appID : long.MinValue;
    return (ActionResult) this.Content("Success");
  }

  [NoCache]
  public ActionResult FilterStatus(string status)
  {
    try
    {
      MonnitSession.SensorListFilters.Status = ((eSensorStatus) Enum.Parse(typeof (eSensorStatus), status)).ToInt();
      MonnitSession.SensorListFilters.Custom = "";
    }
    catch
    {
      MonnitSession.SensorListFilters.Status = int.MinValue;
      MonnitSession.SensorListFilters.Custom = status;
    }
    return (ActionResult) this.Content("Success");
  }

  [NoCache]
  public ActionResult FilterStatusID(int sensorStatusID)
  {
    MonnitSession.SensorListFilters.Status = sensorStatusID >= 0 ? sensorStatusID : int.MinValue;
    return (ActionResult) this.Content("Success");
  }

  [NoCache]
  [Authorize]
  public ActionResult GetSensorStatus(long id)
  {
    try
    {
      return (ActionResult) this.Content(Enum.GetName(typeof (eSensorStatus), (object) Monnit.Sensor.Load(id).Status).ToLower());
    }
    catch
    {
      return (ActionResult) this.Content("unknown");
    }
  }

  [AuthorizeDefault]
  public static Dictionary<string, int> GetSensorStatuses()
  {
    Type enumType = typeof (eSensorStatus);
    return Enum.GetValues(enumType).Cast<int>().ToDictionary<int, string, int>((System.Func<int, string>) (x => Enum.GetName(enumType, (object) x)), (System.Func<int, int>) (x => x));
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

  [NoCache]
  public ActionResult FilterCustom(FormCollection collection)
  {
    try
    {
      MonnitSession.SensorListFilters.Custom = "";
      foreach (string allKey in collection.AllKeys)
        MonnitSession.SensorListFilters.Custom += $"{allKey}:{collection[allKey]}|";
    }
    catch
    {
      MonnitSession.SensorListFilters.Custom = "";
    }
    return (ActionResult) this.Content("Success");
  }

  [NoCache]
  public ActionResult FilterGatewayTypeID(long gatewayTypeID)
  {
    MonnitSession.GatewayListFilters.GatewayTypeID = gatewayTypeID >= 0L ? gatewayTypeID : long.MinValue;
    return (ActionResult) this.Content("Success");
  }

  [NoCache]
  public ActionResult FilterGatewayStatus(string status)
  {
    try
    {
      MonnitSession.GatewayListFilters.Status = ((eSensorStatus) Enum.Parse(typeof (eSensorStatus), status)).ToInt();
    }
    catch
    {
      MonnitSession.GatewayListFilters.Status = int.MinValue;
    }
    return (ActionResult) this.Content("Success");
  }

  [NoCache]
  public ActionResult FilterGatewayStatusID(int sensorStatusID)
  {
    MonnitSession.SensorListFilters.Status = sensorStatusID >= 0 ? sensorStatusID : int.MinValue;
    return (ActionResult) this.Content("Success");
  }

  [NoCache]
  [Authorize]
  public ActionResult GetGatewayStatus(long id)
  {
    try
    {
      return (ActionResult) this.Content(Enum.GetName(typeof (eSensorStatus), (object) Monnit.Gateway.Load(id).Status).ToLower());
    }
    catch
    {
      return (ActionResult) this.Content("unknown");
    }
  }

  [NoCache]
  public ActionResult FilterGatewayName(string name)
  {
    try
    {
      MonnitSession.GatewayListFilters.Name = name;
    }
    catch
    {
      MonnitSession.GatewayListFilters.Name = "";
    }
    return (ActionResult) this.Content("Success");
  }

  public static List<Monnit.Gateway> GetGatewayList()
  {
    long CSNetID = MonnitSession.GatewayListFilters.SensorListFiltersCSNetID;
    long GatewayTypeID = MonnitSession.GatewayListFilters.GatewayTypeID;
    int Status = MonnitSession.GatewayListFilters.Status;
    string Name = MonnitSession.GatewayListFilters.Name;
    if (MonnitSession.CurrentCustomer == null)
      return new List<Monnit.Gateway>();
    IEnumerable<Monnit.Gateway> source = (IEnumerable<Monnit.Gateway>) Monnit.Gateway.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where<Monnit.Gateway>((System.Func<Monnit.Gateway, bool>) (g =>
    {
      if (g.CSNetID != CSNetID && CSNetID != long.MinValue || !MonnitSession.CurrentCustomer.CanSeeNetwork(g.CSNetID))
        return false;
      return g.GatewayTypeID != 10L || g.GatewayTypeID != 11L;
    })).Where<Monnit.Gateway>((System.Func<Monnit.Gateway, bool>) (g =>
    {
      if (g.Status.ToInt() != Status && Status != int.MinValue || g.GatewayTypeID != GatewayTypeID && GatewayTypeID != long.MinValue)
        return false;
      return g.Name.ToLower().Contains(Name.ToLower()) || Name == "";
    })).OrderBy<Monnit.Gateway, string>((System.Func<Monnit.Gateway, string>) (g => g.Name.Trim()));
    switch (MonnitSession.GatewayListSort.Name)
    {
      case "Type":
        source = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Monnit.Gateway>) source.OrderBy<Monnit.Gateway, long>((System.Func<Monnit.Gateway, long>) (cs => cs.GatewayTypeID)) : (IEnumerable<Monnit.Gateway>) source.OrderByDescending<Monnit.Gateway, long>((System.Func<Monnit.Gateway, long>) (t => t.GatewayTypeID));
        break;
      case "Gateway Name":
        source = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Monnit.Gateway>) source.OrderBy<Monnit.Gateway, string>((System.Func<Monnit.Gateway, string>) (g => g.Name)) : (IEnumerable<Monnit.Gateway>) source.OrderByDescending<Monnit.Gateway, string>((System.Func<Monnit.Gateway, string>) (g => g.Name));
        break;
      case "Signal":
        source = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Monnit.Gateway>) source.OrderBy<Monnit.Gateway, int>((System.Func<Monnit.Gateway, int>) (s => s.CurrentSignalStrength)) : (IEnumerable<Monnit.Gateway>) source.OrderByDescending<Monnit.Gateway, int>((System.Func<Monnit.Gateway, int>) (s => s.CurrentSignalStrength));
        break;
      case "Last Check In":
        source = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Monnit.Gateway>) source.OrderBy<Monnit.Gateway, DateTime>((System.Func<Monnit.Gateway, DateTime>) (b =>
        {
          DateTime communicationDate = b.LastCommunicationDate;
          return b.LastCommunicationDate;
        })) : (IEnumerable<Monnit.Gateway>) source.OrderByDescending<Monnit.Gateway, DateTime>((System.Func<Monnit.Gateway, DateTime>) (b =>
        {
          DateTime communicationDate = b.LastCommunicationDate;
          return b.LastCommunicationDate;
        }));
        break;
      case "IsEnterprise":
        source = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Monnit.Gateway>) source.OrderBy<Monnit.Gateway, bool>((System.Func<Monnit.Gateway, bool>) (cs => cs.isEnterpriseHost)) : (IEnumerable<Monnit.Gateway>) source.OrderByDescending<Monnit.Gateway, bool>((System.Func<Monnit.Gateway, bool>) (t => t.isEnterpriseHost));
        break;
    }
    return source.ToList<Monnit.Gateway>();
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult GatewayHome(long id, Guid? dataMessageGUID, bool? newDevice)
  {
    Monnit.Gateway model = Monnit.Gateway.Load(id);
    if (model == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(model.CSNetID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "GatewayIndex",
        controller = "Overview"
      });
    DateTime dateTime = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && model.StartDate != DateTime.MinValue && model.StartDate.Ticks > dateTime.Ticks)
      dateTime = model.StartDate;
    return (ActionResult) this.View((object) model);
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult GatewayHistoryData(long gatewayID, string dataMsg)
  {
    Monnit.Gateway gateway = Monnit.Gateway.Load(gatewayID);
    DateTime fromDate = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime utcFromLocalById = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && gateway.StartDate != DateTime.MinValue && gateway.StartDate.Ticks > fromDate.Ticks)
      fromDate = gateway.StartDate;
    List<GatewayMessage> list = GatewayMessage.LoadByGatewayAndDateRange2(gatewayID, fromDate, utcFromLocalById, 100, dataMsg.ToGuid()).OrderByDescending<GatewayMessage, DateTime>((System.Func<GatewayMessage, DateTime>) (c => c.ReceivedDate)).ToList<GatewayMessage>();
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__112.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__112.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Monnit.Gateway, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Gateway", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = OverviewController.\u003C\u003Eo__112.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__112.\u003C\u003Ep__0, this.ViewBag, gateway);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__112.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__112.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<GatewayMessage>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "dm", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = OverviewController.\u003C\u003Eo__112.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__112.\u003C\u003Ep__1, this.ViewBag, list);
    return !MonnitSession.CustomerCan("Sensor_View_History") || gateway == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "GatewayIndex",
      controller = "Overview"
    }) : (ActionResult) this.PartialView("GatewayMessageData", (object) list);
  }

  [AuthorizeDefault]
  public ActionResult GatewayMessageRefresh(long id)
  {
    Monnit.Gateway model = Monnit.Gateway.Load(id);
    if (model == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(model.CSNetID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "GatewayIndex",
        controller = "Overview"
      });
    return !MonnitSession.IsAuthorizedForAccount(CSNet.Load(model.CSNetID).AccountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "GatewayIndex",
      controller = "Overview"
    }) : (ActionResult) this.PartialView("GatewayMessageList", (object) model);
  }

  [AuthorizeDefault]
  public ActionResult GatewayDataUseChart(long id, string range)
  {
    Monnit.Gateway gateway = Monnit.Gateway.Load(id);
    if (gateway == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID))
      return (ActionResult) this.Content("Unauthorized");
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    if (csNet == null || !MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return (ActionResult) this.Content("Unauthorized");
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__114.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__114.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Range", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = OverviewController.\u003C\u003Eo__114.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__114.\u003C\u003Ep__0, this.ViewBag, range);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__114.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__114.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Monnit.Gateway, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Gateway", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = OverviewController.\u003C\u003Eo__114.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__114.\u003C\u003Ep__1, this.ViewBag, gateway);
    DateTime fromDate1 = DateTime.MinValue;
    DataTable dt;
    switch (range)
    {
      case "Year":
        DateTime dateTime1 = DateTime.UtcNow;
        int year1 = dateTime1.Year;
        dateTime1 = DateTime.UtcNow;
        int month1 = dateTime1.Month;
        dateTime1 = new DateTime(year1, month1, 1);
        DateTime dateTime2 = dateTime1.AddMonths(-12);
        long gatewayId1 = gateway.GatewayID;
        DateTime fromDate2 = dateTime2;
        dateTime1 = dateTime2.AddMonths(13);
        DateTime toDate1 = dateTime1.AddDays(-1.0);
        dt = DataUseLog.LoadMonthlyByGateway(gatewayId1, fromDate2, toDate1);
        break;
      case "Last":
        DateTime dateTime3 = DateTime.UtcNow;
        int year2 = dateTime3.Year;
        dateTime3 = DateTime.UtcNow;
        int month2 = dateTime3.Month;
        dateTime3 = new DateTime(year2, month2, 1);
        DateTime dateTime4 = dateTime3.AddMonths(-1);
        long gatewayId2 = gateway.GatewayID;
        DateTime fromDate3 = dateTime4;
        dateTime3 = dateTime4.AddMonths(1);
        DateTime toDate2 = dateTime3.AddDays(-1.0);
        dt = DataUseLog.LoadDailyByGateway(gatewayId2, fromDate3, toDate2);
        break;
      default:
        fromDate1 = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
        dt = DataUseLog.LoadDailyByGateway(gateway.GatewayID, fromDate1, fromDate1.AddMonths(1).AddDays(-1.0));
        break;
    }
    return (ActionResult) this.PartialView(nameof (GatewayDataUseChart), (object) CellDataUseModel.LoadFromDataTable(dt));
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult GatewayGPS(long id, DateTime? fromDate, DateTime? toDate)
  {
    if (this.Request.IsSensorCertMobile())
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Overview",
        ErrorTranslateTag = "Overview/GatewayGPS|",
        ErrorTitle = "Page Unavailable",
        ErrorMessage = "Location Messages and GPS Mapping not available in Mobile Application"
      });
    Monnit.Gateway gateway = Monnit.Gateway.Load(id);
    if (gateway == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "GatewayIndex",
        controller = "Overview"
      });
    if (fromDate.HasValue)
      MonnitSession.HistoryFromDate = DateTime.SpecifyKind(fromDate.Value, DateTimeKind.Unspecified);
    if (toDate.HasValue)
      MonnitSession.HistoryToDate = DateTime.SpecifyKind(toDate.Value, DateTimeKind.Unspecified);
    DateTime dateTime;
    int num;
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && gateway.StartDate != DateTime.MinValue)
    {
      long ticks1 = gateway.StartDate.Ticks;
      dateTime = MonnitSession.HistoryFromDate;
      long ticks2 = dateTime.Ticks;
      num = ticks1 > ticks2 ? 1 : 0;
    }
    else
      num = 0;
    if (num != 0)
      MonnitSession.HistoryFromDate = gateway.StartDate;
    TimeSpan timeSpan = MonnitSession.HistoryToDate - MonnitSession.HistoryFromDate;
    if (timeSpan.TotalDays > 7.0)
    {
      dateTime = MonnitSession.HistoryToDate;
      MonnitSession.HistoryFromDate = dateTime.AddDays(-7.0);
    }
    else
    {
      timeSpan = MonnitSession.HistoryToDate - MonnitSession.HistoryFromDate;
      if (timeSpan.TotalDays < 0.0)
      {
        dateTime = MonnitSession.HistoryToDate;
        MonnitSession.HistoryFromDate = dateTime.AddDays(-1.0);
      }
    }
    List<LocationMessage> model = LocationMessage.LoadByGatewayIDAndDateRange(gateway.GatewayID, Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID), Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID));
    this.ViewData["Gateway"] = (object) gateway;
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult GatewayEdit(long id)
  {
    Monnit.Gateway model = Monnit.Gateway.Load(id);
    if (model == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(model.CSNetID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "GatewayIndex",
        controller = "Overview"
      });
    return !MonnitSession.IsAuthorizedForAccount(CSNet.Load(model.CSNetID).AccountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "GatewayIndex",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult GatewayEdit(long id, FormCollection collection)
  {
    Monnit.Gateway gateway = Monnit.Gateway.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(CSNet.Load(gateway.CSNetID).AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "GatewayIndex",
        controller = "Overview"
      });
    string name = this.GatewayEditBase(gateway, collection);
    if (name == "EditConfirmation")
      return (ActionResult) this.PartialView("EditConfirmation");
    if (!MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, name, "Gateway", MonnitSession.CurrentTheme.Theme))
      return (ActionResult) this.PartialView("~\\Views\\Gateway\\GatewayEdit\\Default\\Edit.ascx", (object) gateway);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__117.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__117.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = OverviewController.\u003C\u003Eo__117.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__117.\u003C\u003Ep__0, this.ViewBag, name);
    return (ActionResult) this.PartialView($"~\\Views\\Gateway\\{name}.ascx", (object) gateway);
  }

  public ActionResult Blank() => (ActionResult) this.Content("");

  [NoCache]
  [AuthorizeDefault]
  public ActionResult Acknowledge(int gatewayID, double heartbeat)
  {
    new AcknowledgementRecorded()
    {
      Acknowledgement = ("Gateway heartbeat of " + heartbeat.ToStringSafe()),
      CustomerID = MonnitSession.CurrentCustomer.CustomerID,
      GatewayID = ((long) gatewayID),
      DateRecorded = DateTime.UtcNow
    }.Save();
    return (ActionResult) this.Content("OK");
  }

  [AuthorizeDefault]
  public ActionResult GatewayNotification(long id)
  {
    Monnit.Gateway gateway = Monnit.Gateway.Load(id);
    if (gateway == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "GatewayIndex",
        controller = "Overview"
      });
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    if (csNet == null || !MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return (ActionResult) this.PartialView("Unauthorized");
    this.ViewData["Gateway"] = (object) gateway;
    ViewDataDictionary viewData = this.ViewData;
    long gatewayID = id;
    DateTime dateTime = MonnitSession.HistoryFromDate;
    DateTime utcFromLocalById1 = Monnit.TimeZone.GetUTCFromLocalById(dateTime.Date, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    dateTime = MonnitSession.HistoryToDate;
    dateTime = dateTime.Date;
    DateTime utcFromLocalById2 = Monnit.TimeZone.GetUTCFromLocalById(dateTime.AddDays(1.0), MonnitSession.CurrentCustomer.Account.TimeZoneID);
    List<NotificationRecorded> notificationRecordedList = NotificationRecorded.LoadByGatewayAndDateRange(gatewayID, utcFromLocalById1, utcFromLocalById2, 10);
    viewData["Notifications"] = (object) notificationRecordedList;
    return (ActionResult) this.View((object) Notification.LoadByGatewayID(id));
  }

  [AuthorizeDefault]
  public ActionResult RemoveExistingGatewayNotification(
    long gatewayID,
    long notificationID,
    bool? showNotificationEdit,
    int datumindex = 0)
  {
    Notification DBObject = Notification.Load(notificationID);
    Monnit.Gateway gateway = Monnit.Gateway.Load(gatewayID);
    if (gateway == null || DBObject == null || !MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    string changeRecord = $"{{\"GatewayID\": \"{gateway.GatewayID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
    DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, DBObject.AccountID, "Removed gateway from notification");
    DBObject.RemoveGateway(gateway);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__121.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__121.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingNotificaitons", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = OverviewController.\u003C\u003Eo__121.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__121.\u003C\u003Ep__0, this.ViewBag, Notification.LoadByGatewayID(gateway.GatewayID));
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__121.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__121.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ShowNotificationEdit", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = OverviewController.\u003C\u003Eo__121.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__121.\u003C\u003Ep__1, this.ViewBag, showNotificationEdit.GetValueOrDefault());
    return (ActionResult) this.View("AddExistingNotification");
  }

  [AuthorizeDefault]
  public ActionResult GatewaySensorList(long id)
  {
    Monnit.Gateway gateway = Monnit.Gateway.Load(id);
    if (gateway == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__122.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__122.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Monnit.Gateway, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Gateway", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = OverviewController.\u003C\u003Eo__122.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__122.\u003C\u003Ep__0, this.ViewBag, gateway);
    return (ActionResult) this.View((object) Reporting.GatewaySensorLastCheckIn.Exec(id));
  }

  [AuthorizeDefault]
  public ActionResult GatewayCellDataUsage(long id)
  {
    Monnit.Gateway model = Monnit.Gateway.Load(id);
    if (model == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(model.CSNetID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__123.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__123.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Monnit.Gateway, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Gateway", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = OverviewController.\u003C\u003Eo__123.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__123.\u003C\u003Ep__0, this.ViewBag, model);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult GatewayDataUsage(long id)
  {
    DateTime utcNow = DateTime.UtcNow;
    string uri = "";
    string str1 = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str1) && bool.Parse(str1);
    Monnit.Gateway gateway = Monnit.Gateway.Load(id);
    if (gateway == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID))
      return (ActionResult) this.Content("Gateway not found.");
    string str2 = ConfigData.FindValue("MEA_API_Location");
    string str3;
    try
    {
      str3 = XDocument.Load($"{str2}xml?applicationAuthGuid={ConfigData.FindValue("MEA_API_Auth_Guid")}").Descendants((XName) "object").Single<XElement>().Attribute((XName) "auth").Value;
    }
    catch (Exception ex)
    {
      ex.Log("CSNetController.GatewayDataUsage.AuthToken " + ex.ToString());
      return (ActionResult) this.View("ErrorDisplay", (object) new ErrorModel()
      {
        ErrorLocation = "Gateway Data Usage",
        ErrorTranslateTag = "ErrorDisplay/GatewayDataUsage|",
        ErrorTitle = "Unable to Display Data Usage",
        ErrorMessage = "Unable to retrieve gateway usage."
      });
    }
    if (string.IsNullOrEmpty(gateway.MacAddress))
      return (ActionResult) this.View("ErrorDisplay", (object) new ErrorModel()
      {
        ErrorLocation = "Gateway Data Usage",
        ErrorTitle = "Unable to Display Data Usage",
        ErrorTranslateTag = "ErrorDisplay/GatewayDataUsage|",
        ErrorMessage = "Unable to retrieve gateway usage."
      });
    string str4 = Convert.ToInt64(gateway.MacAddress.Split('|')[0]).ToString("X");
    List<GatewayDataUsageModel> source = new List<GatewayDataUsageModel>();
    try
    {
      uri = string.Format(str2 + "xml/RetreiveCellularUsage/{0}?CellularIdentifier={1}&months=12", (object) str3, (object) str4);
      XDocument xdocument = XDocument.Load(uri);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-GatewayDataUsage",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = xdocument.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      source = xdocument.Descendants((XName) "CellularUsage").Select<XElement, GatewayDataUsageModel>((System.Func<XElement, GatewayDataUsageModel>) (obj => new GatewayDataUsageModel()
      {
        Year = obj.Attribute((XName) "Year").Value.ToInt(),
        Month = obj.Attribute((XName) "Month").Value.ToInt(),
        MB = obj.Attribute((XName) "Usage").Value.ToDecimal()
      })).ToList<GatewayDataUsageModel>();
    }
    catch (Exception ex)
    {
      ex.Log($"OverviewController.GatewayDataUsage[ID: {id.ToString()}] ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-GatewayDataUsage",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__124.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__124.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Monnit.Gateway, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Gateway", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = OverviewController.\u003C\u003Eo__124.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__124.\u003C\u003Ep__0, this.ViewBag, gateway);
    return (ActionResult) this.View((object) source.OrderByDescending<GatewayDataUsageModel, int>((System.Func<GatewayDataUsageModel, int>) (m => m.Year)).ThenByDescending<GatewayDataUsageModel, int>((System.Func<GatewayDataUsageModel, int>) (m => m.Month)));
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult GatewayReform(long id, string url)
  {
    Monnit.Gateway DBObject = Monnit.Gateway.Load(id);
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    try
    {
      if (DBObject != null && DBObject.GatewayType != null)
      {
        Account account = Account.Load(csNet.AccountID);
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Reformed gateway");
        if (DBObject.GatewayType.SupportsRemoteNetworkReset)
          DBObject.SendResetNetworkRequest = true;
        DBObject.Save();
        // ISSUE: reference to a compiler-generated field
        if (OverviewController.\u003C\u003Eo__125.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OverviewController.\u003C\u003Eo__125.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = OverviewController.\u003C\u003Eo__125.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__125.\u003C\u003Ep__0, this.ViewBag, "Gateway Reform Pending");
        // ISSUE: reference to a compiler-generated field
        if (OverviewController.\u003C\u003Eo__125.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OverviewController.\u003C\u003Eo__125.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = OverviewController.\u003C\u003Eo__125.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__125.\u003C\u003Ep__1, this.ViewBag, this.IsLocalUrl(url) ? url : "/Overview/GatewayEdit/" + id.ToString());
        return (ActionResult) this.PartialView("EditConfirmation");
      }
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
    }
    return (ActionResult) this.Content("Reform network command failed!");
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult GatewayFirmwareUpdate(long id, string url)
  {
    Monnit.Gateway DBObject = Monnit.Gateway.Load(id);
    if (DBObject == null || DBObject.GatewayType == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    try
    {
      bool flag = DBObject.GatewayType.SupportsOTASuite;
      if (!flag)
        flag = !string.IsNullOrWhiteSpace(DBObject.GatewayType.LatestGatewayPath) && !string.IsNullOrEmpty(DBObject.GatewayType.LatestGatewayVersion);
      if (flag)
      {
        string str1 = "";
        string str2 = DBObject.GatewayType.LatestGatewayVersion;
        if (DBObject.GatewayType.SupportsOTASuite)
          str2 = !MonnitSession.IsEnterprise ? MonnitUtil.GetLatestFirmwareVersionFromMEA(DBObject.SKU, true) : MonnitUtil.GetLatestEncryptedFirmwareVersion(DBObject.SKU, true);
        if (!string.IsNullOrEmpty(str2) && !str2.Contains("Failed") && str2 != DBObject.GatewayFirmwareVersion)
        {
          DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, csNet.AccountID, "Updated gateway firmware");
          DBObject.ForceToBootloader = true;
          if (DBObject.IsUnlocked && DBObject.GatewayType.SupportsHostAddress)
            DBObject.SendUnlockRequest = true;
          DBObject.Save();
          str1 = "Gateway firmware Update Pending";
        }
        if (DBObject.GenerationType.Contains("Gen2") && DBObject.GatewayType.SupportsOTASuite && new Version(DBObject.GatewayFirmwareVersion) >= new Version(DBObject.GatewayType.MinOTAFirmwareVersion))
        {
          Version version = new Version(DBObject.APNFirmwareVersion);
          if (version.Build > 1 || version.Build == 1 && version.Revision > 0)
          {
            string str3 = !MonnitSession.IsEnterprise ? MonnitUtil.GetLatestFirmwareVersionFromMEA(DBObject.SKU, false) : MonnitUtil.GetLatestEncryptedFirmwareVersion(DBObject.SKU, false);
            if (!string.IsNullOrEmpty(str3) && !str3.Contains("Failed") && str3 != DBObject.APNFirmwareVersion)
            {
              SKUFirmware skuFirmware = SKUFirmware.LatestFirmware(DBObject.SKU);
              if (skuFirmware != null)
              {
                DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, csNet.AccountID, "Updated gateway radio firmware");
                DBObject.RadioFirmwareUpdateID = skuFirmware.FirmwareID;
                DBObject.UpdateRadioFirmware = true;
                DBObject.Save();
                if (!string.IsNullOrEmpty(str1))
                  str1 += "<br/>";
                str1 += "Gateway radio firmware Update Pending";
              }
            }
          }
        }
        if (string.IsNullOrEmpty(str1))
          str1 = "No Update Found";
        // ISSUE: reference to a compiler-generated field
        if (OverviewController.\u003C\u003Eo__126.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OverviewController.\u003C\u003Eo__126.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = OverviewController.\u003C\u003Eo__126.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__126.\u003C\u003Ep__0, this.ViewBag, str1);
        // ISSUE: reference to a compiler-generated field
        if (OverviewController.\u003C\u003Eo__126.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OverviewController.\u003C\u003Eo__126.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = OverviewController.\u003C\u003Eo__126.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__126.\u003C\u003Ep__1, this.ViewBag, this.IsLocalUrl(url) ? url : "/Overview/GatewayEdit/" + id.ToString());
        return (ActionResult) this.PartialView("EditConfirmation");
      }
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
    }
    return (ActionResult) this.Content("Firmware update command failed!");
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult GatewayReset(long id, string url)
  {
    Monnit.Gateway DBObject = Monnit.Gateway.Load(id);
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    try
    {
      if (DBObject.ResetToDefault(false))
      {
        Account account = Account.Load(csNet.AccountID);
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Reset gateway");
        DBObject.Save();
        // ISSUE: reference to a compiler-generated field
        if (OverviewController.\u003C\u003Eo__127.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OverviewController.\u003C\u003Eo__127.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = OverviewController.\u003C\u003Eo__127.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__127.\u003C\u003Ep__0, this.ViewBag, "Gateway Reset Pending");
        // ISSUE: reference to a compiler-generated field
        if (OverviewController.\u003C\u003Eo__127.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OverviewController.\u003C\u003Eo__127.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = OverviewController.\u003C\u003Eo__127.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__127.\u003C\u003Ep__1, this.ViewBag, this.IsLocalUrl(url) ? url : "/Overview/GatewayEdit/" + id.ToString());
        return (ActionResult) this.PartialView("EditConfirmation");
      }
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
    }
    return (ActionResult) this.Content("Reset gateway command failed!");
  }

  public void SendNewAccountEmail(Account account, Customer customer)
  {
    string str1 = $"\r\n                Your new account has just been created!<br />\r\n                Account details are as follows<br />\r\n                <br />\r\n                Account: {account.CompanyName}<br />\r\n                TimeZone: {account.TimeZoneDisplayName}<br />\r\n                Address: {""}<br />\r\n                <br />\r\n                Contact: {customer.FullName}<br />\r\n                Username: {customer.UserName}<br />\r\n                Email: {customer.NotificationEmail}<br />\r\n                Phone: {customer.NotificationPhone}<br />\r\n                ";
    using (MailMessage mail = new MailMessage())
    {
      using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, account))
      {
        mail.Subject = "New Account";
        mail.Body = str1;
        mail.IsBodyHtml = true;
        try
        {
          mail.To.Clear();
          mail.To.SafeAdd(customer.NotificationEmail, customer.FullName);
          MonnitUtil.SendMail(mail, smtpClient);
        }
        catch (Exception ex)
        {
          ex.Log("OverviewController.CreateAccountOV.SendUserEmail");
        }
      }
    }
    using (MailMessage mail = new MailMessage())
    {
      using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, account))
      {
        mail.Subject = "New Account";
        mail.Body = str1;
        mail.IsBodyHtml = true;
        Account account1 = Account.Load(account.RetailAccountID);
        string str2 = str1 + $"\r\n                <br />\r\n                Created on: {DateTime.Now.ToShortDateString()}<br />\r\n                Reseller: {(account1 != null ? (object) account1.AccountNumber : (object) "")}<br />\r\n                45 day Trial: {account.IsPremium}<br />\r\n                Subscription Expiration: {account.PremiumValidUntil.ToShortDateString()}<br />\r\n                <br />\r\n                ";
        mail.Body = str2;
        try
        {
          mail.To.Clear();
          if (account1 == null || account1.AccountID == ConfigData.AppSettings("AdminAccountID").ToLong())
          {
            mail.To.Add(new MailAddress(ConfigData.AppSettings("NewAccountNotificationEmail"), "New Account Notification"));
          }
          else
          {
            mail.To.Add(new MailAddress(account1.PrimaryContact.NotificationEmail, account1.PrimaryContact.FullName));
            mail.Bcc.Add(new MailAddress(ConfigData.AppSettings("NewAccountNotificationEmail"), "New Account Notification"));
          }
          MonnitUtil.SendMail(mail, smtpClient);
        }
        catch (Exception ex)
        {
          ex.Log("OverviewController.CreateAccountOV.SendAdminEmail");
        }
      }
    }
  }

  public static string CheckSubscriptionCode(string subscriptionCode)
  {
    return PurchaseBase.CheckSubscriptionCode(subscriptionCode);
  }

  public static DateTime GetExpirationDateFromMEA(
    string subscriptionCode,
    long accountID,
    string keyType,
    string activeKeyType,
    DateTime date)
  {
    return PurchaseBase.GetExpirationDateFromMEA(subscriptionCode, accountID, keyType, activeKeyType, date);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult CreateAccountOV(CreateAccountOVModel model)
  {
    if (this.Session["TryCount"] == null)
    {
      this.Session["TryCount"] = (object) 0;
    }
    else
    {
      if ((int) this.Session["TryCount"] > 19)
      {
        this.ModelState.AddModelError("", "Account Create Locked: Too Many Attempts!");
        ViewDataDictionary viewData = this.ViewData;
        viewData["Exception"] = (object) (viewData["Exception"]?.ToString() + "Account Create Locked: Too Many Attempts!");
      }
      this.Session["TryCount"] = (object) ((int) this.Session["TryCount"] + 1);
    }
    if (!Customer.CheckUsernameIsUnique(model.UserName))
      this.ModelState.AddModelError("Username", "Username not available");
    if (!Customer.CheckNotificationEmailIsUnique(model.NotificationEmail))
      this.ModelState.AddModelError("NotificationEmail", "Email address not available");
    this.ViewData["PasswordLength"] = (object) MonnitUtil.MinPasswordLength;
    if (!MonnitUtil.IsValidPassword(model.Password, MonnitSession.CurrentTheme))
      this.ModelState.AddModelError("Password", MonnitUtil.PasswordHelperString(MonnitSession.CurrentTheme));
    this.ViewData["TimeZones"] = (object) new SelectList((IEnumerable) Monnit.TimeZone.LoadAll(), "TimeZoneID", "DisplayName", (object) model.TimeZoneID);
    if (string.IsNullOrEmpty(model.AccountNumber))
      model.AccountNumber = model.CompanyName;
    if (!Account.CheckAccountNumberIsUnique(model.AccountNumber))
      this.ModelState.AddModelError("CompanyName", "Account already exists");
    if (!model.EULA)
      this.ModelState.AddModelError("EULA", "You must acknowledge you have read and agree to the Terms and Conditions");
    if (model.TimeZoneID < 1L)
      this.ModelState.AddModelError("TimeZoneID", "Time Zone Must Be Selected.");
    AccountSubscriptionType subscriptionType = (AccountSubscriptionType) null;
    if (!string.IsNullOrEmpty(model.SubscriptionCode))
      subscriptionType = AccountSubscriptionType.LoadByKeyType(OverviewController.CheckSubscriptionCode(model.SubscriptionCode));
    if (this.ModelState.IsValid)
    {
      try
      {
        Account account1 = new Account();
        account1.AccountNumber = model.AccountNumber;
        account1.CompanyName = model.CompanyName;
        account1.TimeZoneID = model.TimeZoneID;
        account1.EULAVersion = MonnitSession.CurrentTheme.CurrentEULA;
        account1.EULADate = DateTime.UtcNow;
        account1.CreateDate = DateTime.UtcNow;
        account1.MaxFailedLogins = model.MaxFailedLogins;
        account1.IsCFRCompliant = false;
        Account account2 = Account.Load(model.ResellerID.ToLong()) ?? Account.Load(MonnitSession.CurrentTheme.AccountID) ?? Account.Load(ConfigData.AppSettings("AdminAccountID").ToLong());
        account1.RetailAccountID = account2.AccountID;
        account1.Save();
        account1.AccountIDTree = $"{account2.AccountIDTree}{account1.AccountID.ToString()}*";
        account1.Save();
        this.Session["NewAccount"] = (object) account1;
        Customer customer = new Customer();
        customer.UserName = model.UserName;
        customer.Password = MonnitSession.UseEncryption ? model.Password.Encrypt() : model.Password;
        customer.ConfirmPassword = MonnitSession.UseEncryption ? model.Password.Encrypt() : model.Password;
        if (MonnitSession.CurrentCustomer != null)
          customer.PasswordExpired = true;
        customer.FirstName = model.FirstName;
        customer.LastName = model.LastName;
        customer.NotificationEmail = model.NotificationEmail;
        customer.IsAdmin = true;
        account1.Save();
        AccountSubscription accountSubscription1 = new AccountSubscription();
        accountSubscription1.AccountID = account1.AccountID;
        accountSubscription1.AccountSubscriptionTypeID = 1L;
        AccountSubscription accountSubscription2 = accountSubscription1;
        DateTime today = DateTime.Today;
        DateTime dateTime1 = today.AddYears(100);
        accountSubscription2.ExpirationDate = dateTime1;
        accountSubscription1.Save();
        AccountSubscription accountSubscription3 = new AccountSubscription();
        if (subscriptionType != null)
        {
          try
          {
            DateTime expirationDateFromMea = OverviewController.GetExpirationDateFromMEA(model.SubscriptionCode, account1.AccountID, subscriptionType.KeyType, account1.CurrentSubscription.AccountSubscriptionType.KeyType, DateTime.UtcNow);
            if (expirationDateFromMea == DateTime.MinValue)
              throw new Exception("Activation Code Failed");
            accountSubscription3.AccountID = account1.AccountID;
            accountSubscription3.AccountSubscriptionTypeID = subscriptionType.AccountSubscriptionTypeID;
            accountSubscription3.ExpirationDate = expirationDateFromMea;
            accountSubscription3.LastKeyUsed = model.SubscriptionCode.ToGuid();
            accountSubscription3.Save();
          }
          catch (Exception ex)
          {
          }
        }
        else
        {
          accountSubscription3.AccountSubscriptionTypeID = MonnitSession.CurrentTheme.DefaultAccountSubscriptionTypeID;
          AccountSubscription accountSubscription4 = accountSubscription3;
          today = DateTime.Today;
          DateTime dateTime2 = today.AddDays((double) MonnitSession.CurrentTheme.DefaultPremiumDays);
          accountSubscription4.ExpirationDate = dateTime2;
          accountSubscription3.AccountID = account1.AccountID;
          accountSubscription3.Save();
        }
        customer.AccountID = account1.AccountID;
        customer.Save();
        account1.PrimaryContactID = customer.CustomerID;
        account1.Save();
        this.Session["TryCount"] = (object) 0;
        Account.UpdateAccountTree(account1.AccountID);
        if (!string.IsNullOrEmpty(MonnitSession.CurrentTheme.SMTP) && MonnitSession.CurrentTheme.SMTP != "smtp.yourhostname.com")
          this.SendNewAccountEmail(account1, customer);
        if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
        {
          MonnitSession.CurrentCustomer = customer;
          FormsAuthentication.SetAuthCookie(model.UserName, false);
          if (!ConfigData.AppSettings("IsEnterprise").ToBool())
            OverviewControllerBase.AddNewAccountToOVCookie(System.Web.HttpContext.Current.Request);
        }
        CSNet csNet = new CSNet();
        csNet.AccountID = account1.AccountID;
        csNet.Name = account1.CompanyName + " Network";
        csNet.SendNotifications = true;
        csNet.Save();
        new CustomerPermission()
        {
          CSNetID = csNet.CSNetID,
          CustomerID = account1.PrimaryContact.CustomerID,
          CustomerPermissionTypeID = CustomerPermissionType.Find("Network_View").CustomerPermissionTypeID,
          Can = true
        }.Save();
        return MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "AdminSearch",
          controller = "Settings",
          id = MonnitSession.CurrentCustomer.AccountID,
          q = account1.AccountID
        }) : (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "AssignDevice",
          controller = "Network",
          id = account1.AccountID
        });
      }
      catch (Exception ex)
      {
        this.ModelState.AddModelError("", ex.Message);
        ViewDataDictionary viewData = this.ViewData;
        viewData["Exception"] = (object) (viewData["Exception"]?.ToString() + ex.Message);
        return MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.View(nameof (CreateAccountOV), (object) model) : (ActionResult) this.View((object) model);
      }
    }
    else
      return MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.View(nameof (CreateAccountOV), (object) model) : (ActionResult) this.View((object) model);
  }

  public ActionResult CreateAccountOV()
  {
    this.ViewData["TimeZones"] = (object) new SelectList((IEnumerable) Monnit.TimeZone.LoadAll(), "TimeZoneID", "DisplayName");
    if (MonnitSession.CurrentCustomer == null || !MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CustomerCan("Navigation_View_Administration"))
      return (ActionResult) this.View((object) new CreateAccountOVModel());
    return (ActionResult) this.View(nameof (CreateAccountOV), (object) new CreateAccountOVModel()
    {
      ResellerID = MonnitSession.CurrentCustomer.AccountID.ToStringSafe()
    });
  }

  public ActionResult CheckAccountNumber(string accountnumber)
  {
    return !string.IsNullOrEmpty(accountnumber) ? (ActionResult) this.Content(Account.CheckAccountNumberIsUnique(accountnumber.ToLower()).ToString()) : (ActionResult) this.Content("False");
  }

  public ActionResult CheckUserName(string username)
  {
    return !string.IsNullOrEmpty(username) ? (ActionResult) this.Content(Customer.CheckUsernameIsUnique(username.ToLower()).ToString()) : (ActionResult) this.Content("False");
  }

  public ActionResult CheckEmailAddress(string emailAddress)
  {
    return !string.IsNullOrEmpty(emailAddress) ? (ActionResult) this.Content((emailAddress.IsValidEmail() && Customer.CheckNotificationEmailIsUnique(emailAddress)).ToString()) : (ActionResult) this.Content("False");
  }

  public ActionResult CheckPassword(string password)
  {
    if (string.IsNullOrEmpty(password))
      return (ActionResult) this.Content("");
    return !MonnitUtil.IsValidPassword(password, MonnitSession.CurrentTheme) ? (ActionResult) this.Content("False") : (ActionResult) this.Content("True");
  }

  [AuthorizeDefault]
  public ActionResult CustomAccountSetup(long? id)
  {
    if (!MonnitSession.CurrentCustomer.IsAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!(MonnitSession.CurrentTheme.Theme.ToLower() == "canteen") || !id.HasValue)
      return (ActionResult) this.View((object) new CustomAccountSetupModel());
    CustomAccountSetupModel model = new CustomAccountSetupModel();
    model.AccountType = "";
    Account account = Account.Load(id.GetValueOrDefault());
    if (account == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    model.AccountName = account.AccountNumber;
    string[] strArray = account.AccountIDTree.Split(new char[1]
    {
      '*'
    }, StringSplitOptions.RemoveEmptyEntries);
    if (strArray.Length > 3)
    {
      model.DivisionAccountID = strArray[strArray.Length - 3].ToLong();
      model.DistrictAccountID = strArray[strArray.Length - 2].ToLong();
    }
    List<Monnit.Gateway> gatewayList = Monnit.Gateway.LoadByAccountID(id.GetValueOrDefault());
    if (gatewayList.Count > 0)
    {
      model.GatewayID = gatewayList[0].GatewayID.ToString();
      model.GatewayCode = "IM" + MonnitUtil.CheckDigit(model.GatewayID.ToLong());
    }
    List<Monnit.Sensor> sensorList = Monnit.Sensor.LoadByAccountID(id.GetValueOrDefault());
    if (sensorList.Count > 0)
    {
      model.SensorID1 = sensorList[0].SensorID;
      model.SensorName1 = sensorList[0].SensorName;
      model.SensorCode1 = "IM" + MonnitUtil.CheckDigit(model.SensorID1);
      model.SensorType1 = sensorList[0].SensorType.ToString();
    }
    long? nullable;
    if (sensorList.Count > 1)
    {
      model.SensorID2 = new long?(sensorList[1].SensorID);
      model.SensorName2 = sensorList[1].SensorName;
      CustomAccountSetupModel accountSetupModel = model;
      nullable = model.SensorID2;
      string str = "IM" + MonnitUtil.CheckDigit(nullable.Value);
      accountSetupModel.SensorCode2 = str;
      model.SensorType1 = sensorList[1].SensorType.ToString();
    }
    if (sensorList.Count > 2)
    {
      model.SensorID3 = new long?(sensorList[2].SensorID);
      model.SensorName3 = sensorList[2].SensorName;
      CustomAccountSetupModel accountSetupModel = model;
      nullable = model.SensorID3;
      string str = "IM" + MonnitUtil.CheckDigit(nullable.Value);
      accountSetupModel.SensorCode3 = str;
      model.SensorType1 = sensorList[2].SensorType.ToString();
    }
    if (sensorList.Count > 3)
    {
      model.SensorID4 = new long?(sensorList[3].SensorID);
      model.SensorName4 = sensorList[3].SensorName;
      CustomAccountSetupModel accountSetupModel = model;
      nullable = model.SensorID4;
      string str = "IM" + MonnitUtil.CheckDigit(nullable.Value);
      accountSetupModel.SensorCode4 = str;
      model.SensorType1 = sensorList[3].SensorType.ToString();
    }
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult CustomAccountSetup(CustomAccountSetupModel model, long? id)
  {
    if (MonnitSession.CurrentTheme.Theme.ToLower() != "canteen")
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CurrentCustomer.IsAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    Account districtAccount = Account.Load(model.DistrictAccountID);
    if (districtAccount == null)
      this.ModelState.AddModelError("DistrictAccountID", "No Account Found");
    if (!Account.CheckAccountNumberIsUnique(model.AccountName) && !id.HasValue)
      this.ModelState.AddModelError("AccountName", "Account already exists");
    string str = model.GatewayID.ToString();
    if (str.Contains(":"))
    {
      try
      {
        string[] strArray = str.Split(new char[1]{ ':' }, StringSplitOptions.RemoveEmptyEntries);
        model.GatewayID = strArray[0];
        model.GatewayCode = strArray[1];
      }
      catch
      {
        model.GatewayID = str;
      }
    }
    if (!MonnitUtil.ValidateCheckDigit(model.GatewayID.ToLong(), model.GatewayCode))
      this.ModelState.AddModelError("GatewayID", "Security code did not match.");
    if (model.SensorID1 < 0L)
      this.ModelState.AddModelError("SensorID1", "At least 1 Sensor Required.");
    if (!MonnitUtil.ValidateCheckDigit(model.SensorID1, model.SensorCode1))
      this.ModelState.AddModelError("SensorID1", "Security code did not match..");
    long? nullable = model.SensorID2;
    long num1 = 0;
    if (nullable.GetValueOrDefault() > num1 & nullable.HasValue)
    {
      nullable = model.SensorID2;
      if (!MonnitUtil.ValidateCheckDigit(nullable.GetValueOrDefault(), model.SensorCode2))
        this.ModelState.AddModelError("SensorID2", "Security code did not match.");
    }
    nullable = model.SensorID3;
    long num2 = 0;
    if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
    {
      nullable = model.SensorID3;
      if (!MonnitUtil.ValidateCheckDigit(nullable.GetValueOrDefault(), model.SensorCode3))
        this.ModelState.AddModelError("SensorID3", "Security code did not match.");
    }
    nullable = model.SensorID4;
    long num3 = 0;
    if (nullable.GetValueOrDefault() > num3 & nullable.HasValue)
    {
      nullable = model.SensorID4;
      if (!MonnitUtil.ValidateCheckDigit(nullable.GetValueOrDefault(), model.SensorCode4))
        this.ModelState.AddModelError("SensorID4", "Security code did not match.");
    }
    Monnit.Gateway gateway1 = Monnit.Gateway.Load(model.GatewayID.ToLong());
    if (gateway1 == null)
    {
      this.ModelState.AddModelError("GatewayID", "Gateway not found");
    }
    else
    {
      CSNet csNet = CSNet.Load(gateway1.CSNetID);
      int num4;
      if (csNet != null)
      {
        long accountId = csNet.AccountID;
        nullable = id;
        long valueOrDefault = nullable.GetValueOrDefault();
        if (!(accountId == valueOrDefault & nullable.HasValue))
        {
          num4 = !csNet.HoldingOnlyNetwork ? 1 : 0;
          goto label_36;
        }
      }
      num4 = 0;
label_36:
      if (num4 != 0)
        this.ModelState.AddModelError("GatewayID", "Gateway cannot be added to account");
    }
    if (this.ModelState.IsValid)
    {
      try
      {
        bool isNewAccount = false;
        Account account1 = Account.Load(id.GetValueOrDefault());
        CSNet network = (CSNet) null;
        if (account1 == null)
        {
          isNewAccount = true;
          account1 = new Account();
          account1.AccountNumber = model.AccountName;
          account1.CompanyName = model.AccountName;
          account1.EULAVersion = districtAccount.EULAVersion;
          account1.EULADate = DateTime.UtcNow;
          account1.CreateDate = DateTime.UtcNow;
          account1.TimeZoneID = districtAccount.TimeZoneID;
          Account account2 = districtAccount;
          account1.RetailAccountID = account2.AccountID;
          account1.Save();
          account1.AccountIDTree = $"{account2.AccountIDTree}{account1.AccountID.ToString()}*";
          account1.Save();
        }
        else
          network = CSNet.LoadByAccountID(account1.AccountID).FirstOrDefault<CSNet>();
        if (network == null)
          network = new CSNet();
        this.CustomAccountSetupUpdateAccountValues(isNewAccount, model, districtAccount, ref account1, ref network);
        long id1 = ConfigData.AppSettings("DefaultCSNetID").ToLong();
        List<Monnit.Gateway> gatewayList = Monnit.Gateway.LoadByAccountID(account1.AccountID);
        if (gatewayList.Count > 0)
        {
          foreach (Monnit.Gateway gateway2 in gatewayList)
          {
            gateway2.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account1.AccountID, "Removed gateway");
            CSNetController.TryMoveGateway(id1, gateway2);
          }
        }
        List<Monnit.Sensor> sensorList = Monnit.Sensor.LoadByAccountID(account1.AccountID);
        if (sensorList.Count > 0)
        {
          foreach (Monnit.Sensor DBObject in sensorList)
          {
            DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account1.AccountID, "Removed sensor");
            CSNetController.TryMoveSensor(id1, DBObject.SensorID);
          }
        }
        List<Notification> notificationList = Notification.LoadByAccountID(account1.AccountID);
        if (notificationList.Count > 0)
        {
          foreach (Notification DBObject in notificationList)
          {
            DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account1.AccountID, "Removed notification");
            DBObject.Delete();
          }
        }
        if (!CSNetController.TryMoveGateway(network.CSNetID, gateway1))
        {
          this.ModelState.AddModelError("GatewayID", "Gateway not found");
          return (ActionResult) this.View((object) model);
        }
        gateway1.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account1.AccountID, "Moved gateway to different network");
        Notification notification1 = new Notification();
        notification1.Name = "Freezer Profile - Temp Outside Range";
        notification1.NotificationText = "<p><strong>*Action Required*</strong></p><p>The temperature of your Freezer is outside the range</p> <p>Reading: {Reading}</p> <p>District: {ParentAccount}</p> <p>Account: {AccountNumber}</p> <p>Sensor: {Name}</p> <p>{Acknowledge}</p> <p>&nbsp;</p> <p>&nbsp;</p>";
        notification1.Subject = "Critical - Action Required {AccountNumber}";
        notification1.HasUserNotificationAction = true;
        notification1.AccountID = account1.AccountID;
        notification1.NotificationClass = eNotificationClass.Advanced;
        notification1.AdvancedNotificationID = 1L;
        notification1.CompareValue = "0";
        notification1.CompareType = eCompareType.Equal;
        notification1.IsActive = true;
        notification1.IsDeleted = false;
        notification1.AlwaysSend = true;
        notification1.Version = "1";
        notification1.SnoozeDuration = 60;
        notification1.CanAutoAcknowledge = true;
        notification1.ApplySnoozeByTriggerDevice = true;
        notification1.Save();
        new AdvancedNotificationParameterValue()
        {
          AdvancedNotificationParameterID = 2L,
          NotificationID = notification1.NotificationID,
          ParameterValue = "44"
        }.Save();
        notification1.LogAuditData(eAuditAction.Create, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account1.AccountID, "Created freezer notification");
        Notification notification2 = new Notification();
        notification2.Name = "Cooler Profile - Temp Outside Range";
        notification2.NotificationText = "<p><strong>*Action Required*</strong></p><p>The temperature of your Cooler is outside the range</p> <p>Reading: {Reading}</p> <p>District: {ParentAccount}</p> <p>Account: {AccountNumber}</p> <p>Sensor: {Name}</p> <p>{Acknowledge}</p> <p>&nbsp;</p> <p>&nbsp;</p>";
        notification2.Subject = "Critical - Action Required {AccountNumber}";
        notification2.HasUserNotificationAction = true;
        notification2.AccountID = account1.AccountID;
        notification2.NotificationClass = eNotificationClass.Advanced;
        notification2.AdvancedNotificationID = 1L;
        notification2.CompareValue = "0";
        notification2.CompareType = eCompareType.Equal;
        notification2.IsActive = true;
        notification2.IsDeleted = false;
        notification2.AlwaysSend = true;
        notification2.Version = "1";
        notification2.SnoozeDuration = 60;
        notification2.CanAutoAcknowledge = true;
        notification2.ApplySnoozeByTriggerDevice = true;
        notification2.Save();
        new AdvancedNotificationParameterValue()
        {
          AdvancedNotificationParameterID = 2L,
          NotificationID = notification2.NotificationID,
          ParameterValue = "44"
        }.Save();
        notification2.LogAuditData(eAuditAction.Create, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account1.AccountID, "Created cooler notification");
        Monnit.Sensor sensor1 = Monnit.Sensor.Load(model.SensorID1);
        string accountNumber = account1.AccountNumber;
        CSNet csNet1 = CSNet.Load(sensor1.CSNetID);
        int num5;
        if (csNet1 != null)
        {
          long accountId = csNet1.AccountID;
          nullable = id;
          long valueOrDefault = nullable.GetValueOrDefault();
          if (!(accountId == valueOrDefault & nullable.HasValue))
          {
            num5 = !csNet1.HoldingOnlyNetwork ? 1 : 0;
            goto label_69;
          }
        }
        num5 = 0;
label_69:
        if (num5 != 0)
        {
          this.ModelState.AddModelError("SensorID1", "Sensor cannot be added to account");
        }
        else
        {
          if (sensor1 == null || !CSNetController.TryMoveSensor(network.CSNetID, sensor1.SensorID))
          {
            this.ModelState.AddModelError("SensorID1", "Sensor 1 not found");
            return (ActionResult) this.View((object) model);
          }
          OverviewController.SetSensorThresholds(accountNumber, model.SensorType1, account1, sensor1, notification2, notification1);
        }
        nullable = model.SensorID2;
        long num6 = 0;
        if (nullable.GetValueOrDefault() > num6 & nullable.HasValue)
        {
          nullable = model.SensorID2;
          Monnit.Sensor sensor2 = Monnit.Sensor.Load(nullable.GetValueOrDefault());
          CSNet csNet2 = CSNet.Load(sensor2.CSNetID);
          int num7;
          if (csNet2 != null)
          {
            long accountId = csNet2.AccountID;
            nullable = id;
            long valueOrDefault = nullable.GetValueOrDefault();
            if (!(accountId == valueOrDefault & nullable.HasValue))
            {
              num7 = !csNet2.HoldingOnlyNetwork ? 1 : 0;
              goto label_79;
            }
          }
          num7 = 0;
label_79:
          if (num7 != 0)
          {
            this.ModelState.AddModelError("GatewayID", "Sensor cannot be added to account");
          }
          else
          {
            if (sensor2 == null || !CSNetController.TryMoveSensor(network.CSNetID, sensor2.SensorID))
            {
              this.ModelState.AddModelError("SensorID2", "Sensor 2 not found");
              return (ActionResult) this.View((object) model);
            }
            OverviewController.SetSensorThresholds(accountNumber, model.SensorType2, account1, sensor2, notification2, notification1);
          }
        }
        nullable = model.SensorID3;
        long num8 = 0;
        if (nullable.GetValueOrDefault() > num8 & nullable.HasValue)
        {
          nullable = model.SensorID3;
          Monnit.Sensor sensor3 = Monnit.Sensor.Load(nullable.GetValueOrDefault());
          CSNet csNet3 = CSNet.Load(sensor3.CSNetID);
          int num9;
          if (csNet3 != null)
          {
            long accountId = csNet3.AccountID;
            nullable = id;
            long valueOrDefault = nullable.GetValueOrDefault();
            if (!(accountId == valueOrDefault & nullable.HasValue))
            {
              num9 = !csNet3.HoldingOnlyNetwork ? 1 : 0;
              goto label_90;
            }
          }
          num9 = 0;
label_90:
          if (num9 != 0)
          {
            this.ModelState.AddModelError("GatewayID", "Sensor cannot be added to account");
          }
          else
          {
            if (sensor3 == null || !CSNetController.TryMoveSensor(network.CSNetID, sensor3.SensorID))
            {
              this.ModelState.AddModelError("SensorID3", "Sensor 3 not found");
              return (ActionResult) this.View((object) model);
            }
            OverviewController.SetSensorThresholds(accountNumber, model.SensorType3, account1, sensor3, notification2, notification1);
          }
        }
        nullable = model.SensorID4;
        long num10 = 0;
        if (nullable.GetValueOrDefault() > num10 & nullable.HasValue)
        {
          nullable = model.SensorID4;
          Monnit.Sensor sensor4 = Monnit.Sensor.Load(nullable.GetValueOrDefault());
          CSNet csNet4 = CSNet.Load(sensor4.CSNetID);
          int num11;
          if (csNet4 != null)
          {
            long accountId = csNet4.AccountID;
            nullable = id;
            long valueOrDefault = nullable.GetValueOrDefault();
            if (!(accountId == valueOrDefault & nullable.HasValue))
            {
              num11 = !csNet4.HoldingOnlyNetwork ? 1 : 0;
              goto label_101;
            }
          }
          num11 = 0;
label_101:
          if (num11 != 0)
            this.ModelState.AddModelError("GatewayID", "Sensor cannot be added to account");
          if (sensor4 == null || !CSNetController.TryMoveSensor(network.CSNetID, sensor4.SensorID))
          {
            this.ModelState.AddModelError("SensorID4", "Sensor 4 not found");
            return (ActionResult) this.View((object) model);
          }
          OverviewController.SetSensorThresholds(accountNumber, model.SensorType4, account1, sensor4, notification2, notification1);
        }
        foreach (CustomerGroup group in CustomerGroup.LoadByAccountID(districtAccount.AccountID))
        {
          try
          {
            notification1.AddCustomerGroup(group);
            notification2.AddCustomerGroup(group);
          }
          catch (Exception ex)
          {
            ex.Log($"Failure adding Customer group to notification freezerNotiID: {notification1.NotificationID.ToString()} coolerNotiID: {notification2.NotificationID.ToString()}");
          }
        }
        foreach (CustomerGroup group in CustomerGroup.LoadByAccountID(model.DivisionAccountID))
        {
          try
          {
            notification1.AddCustomerGroup(group);
            notification2.AddCustomerGroup(group);
          }
          catch (Exception ex)
          {
            ex.Log($"Failure adding Customer group to notification freezerNotiID: {notification1.NotificationID.ToString()} coolerNotiID: {notification2.NotificationID.ToString()}");
          }
        }
        foreach (CustomerGroup group in CustomerGroup.LoadByAccountID(MonnitSession.CurrentTheme.AccountID))
        {
          try
          {
            notification1.AddCustomerGroup(group);
            notification2.AddCustomerGroup(group);
          }
          catch (Exception ex)
          {
            ex.Log($"Failure adding Customer group to notification freezerNotiID: {notification1.NotificationID.ToString()} coolerNotiID: {notification2.NotificationID.ToString()}");
          }
        }
        return (new AccountController().ProxySubAccount(account1.AccountID) as ContentResult).Content == "Success" ? (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index",
          controller = "Overview"
        }) : (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "AccountEdit",
          controller = "Settings",
          id = account1.AccountID
        });
      }
      catch (Exception ex)
      {
        this.ModelState.AddModelError("", ex.Message);
      }
    }
    return (ActionResult) this.View((object) model);
  }

  private void CustomAccountSetupUpdateAccountValues(
    bool isNewAccount,
    CustomAccountSetupModel model,
    Account districtAccount,
    ref Account account,
    ref CSNet network)
  {
    account.AccountNumber = model.AccountName;
    account.CompanyName = model.AccountName;
    account.TimeZoneID = districtAccount.TimeZoneID;
    account.PrimaryContactID = districtAccount.PrimaryContactID;
    Account account1 = districtAccount;
    account.RetailAccountID = account1.AccountID;
    account.Save();
    account.AccountIDTree = $"{account1.AccountIDTree}{account.AccountID.ToString()}*";
    account.Save();
    if (isNewAccount)
    {
      AccountSubscription accountSubscription1 = new AccountSubscription();
      accountSubscription1.AccountID = account.AccountID;
      accountSubscription1.AccountSubscriptionTypeID = 1L;
      AccountSubscription accountSubscription2 = accountSubscription1;
      DateTime today = DateTime.Today;
      DateTime dateTime1 = today.AddYears(100);
      accountSubscription2.ExpirationDate = dateTime1;
      accountSubscription1.Save();
      AccountSubscription accountSubscription3 = new AccountSubscription();
      accountSubscription3.AccountID = account.AccountID;
      accountSubscription3.AccountSubscriptionTypeID = 10L;
      AccountSubscription accountSubscription4 = accountSubscription3;
      today = DateTime.Today;
      DateTime dateTime2 = today.AddDays(45.0);
      accountSubscription4.ExpirationDate = dateTime2;
      accountSubscription3.Save();
      account.PremiumValidUntil = accountSubscription3.ExpirationDate;
      account.ClearSubscritions();
      account.LogAuditData(eAuditAction.Create, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Created new account using CustomAccountSetup");
    }
    else
      account.LogAuditData(eAuditAction.Update, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Updated account using CustomAccountSetup");
    AccountAddress primaryAddress = account.PrimaryAddress;
    primaryAddress.Address = districtAccount.PrimaryAddress.Address;
    primaryAddress.Address2 = districtAccount.PrimaryAddress.Address2;
    primaryAddress.City = districtAccount.PrimaryAddress.City;
    primaryAddress.State = districtAccount.PrimaryAddress.State;
    primaryAddress.PostalCode = districtAccount.PrimaryAddress.PostalCode;
    primaryAddress.Country = districtAccount.PrimaryAddress.Country;
    primaryAddress.Save();
    network.AccountID = account.AccountID;
    network.Name = account.CompanyName + model.GatewayID;
    network.SendNotifications = true;
    network.HoldingOnlyNetwork = false;
    network.Save();
  }

  private static void SetSensorThresholds(
    string name,
    string type,
    Account account,
    Monnit.Sensor sensor,
    Notification coolerNoti,
    Notification freezerNoti)
  {
    sensor.SensorName = $"{name} {type} {sensor.SensorID.ToString()}";
    string lower = type.ToLower();
    if (lower != null)
    {
      if (lower.Contains("cooler"))
      {
        sensor.MinimumThreshold = 17L;
        sensor.MaximumThreshold = 56L;
        sensor.ReportInterval = 60.0;
        sensor.ActiveStateInterval = 15.0;
        sensor.MeasurementsPerTransmission = 12;
        sensor.ProfileConfig2Dirty = true;
        sensor.GeneralConfigDirty = true;
        sensor.Save();
        coolerNoti.AddSensor(sensor);
      }
      else if (lower.Contains("freezer"))
      {
        sensor.MinimumThreshold = -344L;
        sensor.MaximumThreshold = -178L;
        sensor.ReportInterval = 60.0;
        sensor.ActiveStateInterval = 15.0;
        sensor.MeasurementsPerTransmission = 12;
        sensor.ProfileConfig2Dirty = true;
        sensor.GeneralConfigDirty = true;
        sensor.Save();
        freezerNoti.AddSensor(sensor);
      }
    }
    sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Assigned  sensor to account");
  }

  [AuthorizeDefault]
  public ActionResult SubAccountsForAccount(long accountID)
  {
    if (accountID <= 0L)
      return (ActionResult) this.Content("Failed");
    Account acct = Account.Load(accountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
    {
      if (!acct.AccountIDTree.Contains(accountID.ToString()))
        return (ActionResult) this.Content("Failed: no account found");
      List<Account> accountList = Account.LoadByRetailAccountID(acct.AccountID);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("[");
      foreach (Account account in accountList)
        stringBuilder.AppendFormat("{{\"AccountID\" : {0}, \"AccountName\": \"{1}\" }},", (object) account.AccountID, (object) account.AccountNumber);
      return (ActionResult) this.Content(stringBuilder.ToString().TrimEnd(',') + "]");
    }
    List<Account> accountList1 = Account.LoadByRetailAccountID(acct.AccountID);
    StringBuilder stringBuilder1 = new StringBuilder();
    stringBuilder1.Append("[");
    foreach (Account account in accountList1)
      stringBuilder1.AppendFormat("{{\"AccountID\" : {0}, \"AccountName\": \"{1}\" }},", (object) account.AccountID, (object) account.AccountNumber);
    return (ActionResult) this.Content(stringBuilder1.ToString().TrimEnd(',') + "]");
  }

  [AuthorizeDefault]
  public ActionResult SensorSchedule(long id)
  {
    Monnit.Sensor model = Monnit.Sensor.Load(id);
    bool flag = true;
    for (int index = 0; index < 13; ++index)
    {
      if (model.TimeOfDayActive[index] != byte.MaxValue)
      {
        flag = false;
        break;
      }
    }
    if (flag)
    {
      int index1;
      for (int index2 = 0; index2 < 13; index2 = index1 + 1)
      {
        model.TimeOfDayActive[index2] = Convert.ToByte(0);
        index1 = index2 + 1;
        model.TimeOfDayActive[index1] = Convert.ToByte(96 /*0x60*/);
      }
      model.Save();
    }
    return (ActionResult) this.View((object) model);
  }

  public ActionResult SensorProfileCreate()
  {
    return (ActionResult) this.View((object) new SensorProfile());
  }

  [HttpPost]
  public ActionResult SensorProfileCreate(long id, string inputData)
  {
    SensorProfile sensorProfile1 = SensorProfile.Create(id, (object) inputData.StringToByteArray());
    string serialize = sensorProfile1.Serialize();
    SensorProfile sensorProfile2 = SensorProfile.Deserialize(serialize, id.ToLong());
    if (id == 1L || id == 2L)
      return (ActionResult) this.Content($"{sensorProfile1.Datems[0].BooleanVal.ToString()} ~$~ {serialize} ~$~ {sensorProfile2.Datems[0].BooleanVal.ToString()}");
    switch (id)
    {
      case 3:
        return (ActionResult) this.Content($"{sensorProfile1.Datems[0].DoubleVal.ToString()} ~$~ {serialize} ~$~ {sensorProfile2.Datems[0].DoubleVal.ToString()}");
      case 4:
        string[] strArray1 = new string[13];
        strArray1[0] = sensorProfile1.Datems[0].IntVal.ToString();
        strArray1[1] = ", ";
        double doubleVal = sensorProfile1.Datems[1].DoubleVal;
        strArray1[2] = doubleVal.ToString();
        strArray1[3] = " ,";
        int intVal1 = sensorProfile1.Datems[2].IntVal;
        strArray1[4] = intVal1.ToString();
        strArray1[5] = " ~$~ ";
        strArray1[6] = serialize;
        strArray1[7] = " ~$~ ";
        intVal1 = sensorProfile2.Datems[0].IntVal;
        strArray1[8] = intVal1.ToString();
        strArray1[9] = ",";
        doubleVal = sensorProfile2.Datems[1].DoubleVal;
        strArray1[10] = doubleVal.ToString();
        strArray1[11] = ",";
        strArray1[12] = sensorProfile2.Datems[2].IntVal.ToString();
        return (ActionResult) this.Content(string.Concat(strArray1));
      case 119:
        string[] strArray2 = new string[9];
        strArray2[0] = sensorProfile1.Datems[0].IntVal.ToString();
        strArray2[1] = ", ";
        int intVal2 = sensorProfile1.Datems[1].IntVal;
        strArray2[2] = intVal2.ToString();
        strArray2[3] = " ~$~ ";
        strArray2[4] = serialize;
        strArray2[5] = " ~$~ ";
        intVal2 = sensorProfile2.Datems[0].IntVal;
        strArray2[6] = intVal2.ToString();
        strArray2[7] = ",";
        intVal2 = sensorProfile2.Datems[1].IntVal;
        strArray2[8] = intVal2.ToString();
        return (ActionResult) this.Content(string.Concat(strArray2));
      default:
        return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult SaveSchedule(long id, FormCollection collection)
  {
    Monnit.Sensor model = Monnit.Sensor.Load(id);
    if (model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
      });
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
      });
    bool flag = false;
    if (model.GenerationType.ToLower().Contains("gen2"))
    {
      int num1 = collection["rawTime1"].ToInt();
      int num2 = collection["rawTime2"].ToInt();
      int num3 = collection["sunday"].ToInt();
      int num4 = collection["monday"].ToInt();
      int num5 = collection["tuesday"].ToInt();
      int num6 = collection["wednesday"].ToInt();
      int num7 = collection["thursday"].ToInt();
      int num8 = collection["friday"].ToInt();
      int num9 = collection["saturday"].ToInt();
      string str = collection["selectedSchedule"];
      int offset = (TimeZoneInfo.Local.GetUtcOffset(Monnit.TimeZone.GetLocalTimeById(DateTime.UtcNow, MonnitSession.CurrentCustomer.Account.TimeZoneID)).TotalHours * 4.0).ToInt();
      this.SetSensorTimeOffset(id, offset);
      if (num1 == 1439)
        num1 = 1440;
      if (num2 == 1439)
        num2 = 1440;
      if (str == null || str.ToLower() == "all")
      {
        num1 = 0;
        num2 = 1440;
      }
      else if (str.ToLower() == "none")
      {
        num1 = 0;
        num2 = 0;
      }
      else if (str.ToLower() == "before_and_after")
      {
        int num10 = num1;
        num1 = num2;
        num2 = num10;
      }
      int num11 = num1 / 15;
      int num12 = num2 / 15;
      if (num3 == 1)
      {
        if ((int) model.TimeOfDayActive[0] != (int) Convert.ToByte(num11) || (int) model.TimeOfDayActive[1] != (int) Convert.ToByte(num12))
          flag = true;
        model.TimeOfDayActive[0] = Convert.ToByte(num11);
        model.TimeOfDayActive[1] = Convert.ToByte(num12);
      }
      if (num4 == 1)
      {
        if ((int) model.TimeOfDayActive[2] != (int) Convert.ToByte(num11) || (int) model.TimeOfDayActive[3] != (int) Convert.ToByte(num12))
          flag = true;
        model.TimeOfDayActive[2] = Convert.ToByte(num11);
        model.TimeOfDayActive[3] = Convert.ToByte(num12);
      }
      if (num5 == 1)
      {
        if ((int) model.TimeOfDayActive[4] != (int) Convert.ToByte(num11) || (int) model.TimeOfDayActive[5] != (int) Convert.ToByte(num12))
          flag = true;
        model.TimeOfDayActive[4] = Convert.ToByte(num11);
        model.TimeOfDayActive[5] = Convert.ToByte(num12);
      }
      if (num6 == 1)
      {
        if ((int) model.TimeOfDayActive[6] != (int) Convert.ToByte(num11) || (int) model.TimeOfDayActive[7] != (int) Convert.ToByte(num12))
          flag = true;
        model.TimeOfDayActive[6] = Convert.ToByte(num11);
        model.TimeOfDayActive[7] = Convert.ToByte(num12);
      }
      if (num7 == 1)
      {
        if ((int) model.TimeOfDayActive[8] != (int) Convert.ToByte(num11) || (int) model.TimeOfDayActive[9] != (int) Convert.ToByte(num12))
          flag = true;
        model.TimeOfDayActive[8] = Convert.ToByte(num11);
        model.TimeOfDayActive[9] = Convert.ToByte(num12);
      }
      if (num8 == 1)
      {
        if ((int) model.TimeOfDayActive[10] != (int) Convert.ToByte(num11) || (int) model.TimeOfDayActive[11] != (int) Convert.ToByte(num12))
          flag = true;
        model.TimeOfDayActive[10] = Convert.ToByte(num11);
        model.TimeOfDayActive[11] = Convert.ToByte(num12);
      }
      if (num9 == 1)
      {
        if ((int) model.TimeOfDayActive[12] != (int) Convert.ToByte(num11) || (int) model.TimeOfDayActive[13] != (int) Convert.ToByte(num12))
          flag = true;
        model.TimeOfDayActive[12] = Convert.ToByte(num11);
        model.TimeOfDayActive[13] = Convert.ToByte(num12);
      }
      model.TimeOfDayControl = model.TimeOfDayActiveAllOn(model.TimeOfDayActive) ? 0 : collection["TimeOfDayControl"].ToInt();
      if (flag)
      {
        model.GeneralConfig2Dirty = true;
        model.GeneralConfig3Dirty = true;
        model.Save();
      }
    }
    return (ActionResult) this.View("SensorSchedule", (object) model);
  }

  public ActionResult SetSensorTimeOffset(long id, int offset)
  {
    try
    {
      Monnit.Sensor sensor = Monnit.Sensor.Load(id);
      sensor.TimeOffset = offset;
      sensor.Save();
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  public ActionResult ClearSystemHelp(long id)
  {
    try
    {
      SystemHelp systemHelp = SystemHelp.Load(id);
      if (MonnitSession.CurrentCustomer.CanSeeAccount(systemHelp.AccountID))
        systemHelp.Delete();
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult ClearAllCertificationAcknowledgementForCustomer(long id)
  {
    try
    {
      List<CertificationAcknowledgementModel> acknowledgementModelList = CertificateNotification.LoadByCustomerID(id);
      if (acknowledgementModelList.Count > 0)
      {
        foreach (CertificationAcknowledgementModel acknowledgementModel in acknowledgementModelList)
        {
          CertificateAcknowledgement certificateAcknowledgement = CertificateAcknowledgement.Load(acknowledgementModel.CertificateAcknowledgementID);
          certificateAcknowledgement.AcknowledgeDate = DateTime.UtcNow;
          certificateAcknowledgement.Save();
        }
      }
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ClearCertificationAcknowledgement(long id)
  {
    try
    {
      CertificateAcknowledgement certificateAcknowledgement = CertificateAcknowledgement.Load(id);
      if (certificateAcknowledgement != null)
      {
        certificateAcknowledgement.AcknowledgeDate = DateTime.UtcNow;
        certificateAcknowledgement.Save();
      }
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  public ActionResult DeviceInfo() => (ActionResult) this.View();

  [NoCache]
  [AuthorizeDefault]
  public ActionResult DeviceNotify(long id)
  {
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
      });
    Monnit.Sensor model = Monnit.Sensor.Load(id);
    return model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID) || model.ApplicationID != 13L ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "SensorIndex",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [AuthorizeDefault]
  [ValidateAntiForgeryToken]
  public ActionResult DeviceNotify(long id, FormCollection collection)
  {
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    Monnit.Sensor sensor = Monnit.Sensor.Load(id);
    if (sensor == null || !MonnitSession.IsAuthorizedForAccount(sensor.AccountID) || sensor.ApplicationID != 13L)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    MonnitApplicationBase.ControlSensor((NameValueCollection) collection, sensor);
    return (ActionResult) this.View((object) sensor);
  }

  [AuthorizeDefault]
  public ActionResult ToggleSensor(long id, long notiferID, bool add)
  {
    try
    {
      Monnit.Sensor sensor = Monnit.Sensor.Load(id);
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

  [AuthorizeDefault]
  public ActionResult SensorList(long id, long NotiID, string q)
  {
    List<NotifySensorDataModel> sensorList = new ConfigureNotifierSensorDataModel(Monnit.Sensor.Load(NotiID)).SensorList;
    if (sensorList.Count == 0)
      return (ActionResult) this.Content("No sensors found for this account.");
    List<NotifySensorDataModel> source = sensorList.Where<NotifySensorDataModel>((System.Func<NotifySensorDataModel, bool>) (c => c.sens.SensorName.ToLower().Contains(q.ToLower()) || c.sens.SensorID.ToString().ToLower().Contains(q.ToLower()) || c.sens.SensorType.Name.ToLower().Contains(q.ToLower()))).ToList<NotifySensorDataModel>();
    if (source.Count == 0 && q.Length > 0)
      source = sensorList;
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__154.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__154.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "sensID", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = OverviewController.\u003C\u003Eo__154.\u003C\u003Ep__0.Target((CallSite) OverviewController.\u003C\u003Eo__154.\u003C\u003Ep__0, this.ViewBag, NotiID);
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__154.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__154.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<NotifySensorDataModel>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiSensorlist", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = OverviewController.\u003C\u003Eo__154.\u003C\u003Ep__1.Target((CallSite) OverviewController.\u003C\u003Eo__154.\u003C\u003Ep__1, this.ViewBag, source);
    string name = string.Format("SensorEdit\\ApplicationCustomization\\app_013\\_SensorList", (object) 13);
    if (!MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, name, "Sensor", MonnitSession.CurrentTheme.Theme))
      return (ActionResult) this.PartialView("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\_SensorList.ascx", (object) source.OrderByDescending<NotifySensorDataModel, bool>((System.Func<NotifySensorDataModel, bool>) (passData => passData.isSendingSensorData)));
    // ISSUE: reference to a compiler-generated field
    if (OverviewController.\u003C\u003Eo__154.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      OverviewController.\u003C\u003Eo__154.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (OverviewController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = OverviewController.\u003C\u003Eo__154.\u003C\u003Ep__2.Target((CallSite) OverviewController.\u003C\u003Eo__154.\u003C\u003Ep__2, this.ViewBag, name);
    return (ActionResult) this.PartialView($"~\\Views\\Sensor\\{name}.ascx", (object) source.OrderByDescending<NotifySensorDataModel, bool>((System.Func<NotifySensorDataModel, bool>) (passData => passData.isSendingSensorData)));
  }

  [NoCache]
  public ActionResult OptOut() => (ActionResult) this.View(nameof (OptOut));

  [NoCache]
  [ValidateAntiForgeryToken]
  [HttpPost]
  public ActionResult NotificationOptOut(string address, FormCollection collection)
  {
    string reason = string.Empty;
    string[] allKeys = collection.AllKeys;
    for (int index = 0; index < allKeys.Length; ++index)
    {
      if (allKeys[index].ToString() == "frequency" || allKeys[index].ToString() == "nowork" || allKeys[index].ToString() == "text" || allKeys[index].ToString() == "noproduct" || allKeys[index].ToString() == "otherText")
        reason = $"{reason}{collection[allKeys[index]]} ";
    }
    this.Unsubscribe(address, reason);
    return (ActionResult) this.View(nameof (NotificationOptOut));
  }

  [AuthorizeDefault]
  public ActionResult AvailableSensorActionList(
    long id,
    string eventType,
    string datumIndex,
    string nameFilter,
    string status,
    string assigned)
  {
    Monnit.Sensor sens = Monnit.Sensor.Load(id);
    IEnumerable<AvailableNotificationBySensorModel> source = (IEnumerable<AvailableNotificationBySensorModel>) AvailableNotificationBySensorModel.Load(sens);
    int num = -1;
    if (!string.IsNullOrEmpty(eventType))
      source = source.Where<AvailableNotificationBySensorModel>((System.Func<AvailableNotificationBySensorModel, bool>) (n => n.Notification.NotificationClass.ToString() == eventType));
    if (eventType == "Application" && !string.IsNullOrEmpty(datumIndex))
      num = datumIndex.Split('&')[1].ToInt();
    if (!string.IsNullOrEmpty(nameFilter))
      source = source.Where<AvailableNotificationBySensorModel>((System.Func<AvailableNotificationBySensorModel, bool>) (n => n.Notification.Name.ToLower().Contains(nameFilter.ToLower())));
    if (!string.IsNullOrEmpty(status))
      source = source.Where<AvailableNotificationBySensorModel>((System.Func<AvailableNotificationBySensorModel, bool>) (n => n.Notification.IsActive == status.ToBool()));
    this.ViewData["AssignedFilter"] = (object) assigned.ToStringSafe();
    this.ViewData["DatumIndexFilter"] = (object) num;
    this.ViewData["Sensor"] = (object) sens;
    return (ActionResult) this.PartialView("AvailableSensorActions", (object) source.ToList<AvailableNotificationBySensorModel>());
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult SetMobiDates(string fromDate, string toDate)
  {
    DateTime exact1 = DateTime.ParseExact(fromDate.Substring(0, 24), "ddd MMM d yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
    DateTime exact2 = DateTime.ParseExact(toDate.Substring(0, 24), "ddd MMM d yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
    MonnitSession.HistoryFromDate = exact1;
    MonnitSession.HistoryToDate = exact2;
    return (ActionResult) this.Content("Success");
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult GetMobiDates()
  {
    DateTime dateTime = MonnitSession.HistoryFromDate;
    string str1 = dateTime.ToString("yyyy-MM-ddTHH:mm");
    dateTime = MonnitSession.HistoryToDate;
    string str2 = dateTime.ToString("yyyy-MM-ddTHH:mm");
    return (ActionResult) this.Json((object) new
    {
      HistoryFromDate = str1,
      HistoryToDate = str2
    }, JsonRequestBehavior.AllowGet);
  }

  [AuthorizeDefault]
  public ActionResult AcknowledgeAllActiveNotifications(string AckMethod)
  {
    if (!MonnitSession.CustomerCan("Notification_Can_Acknowledge"))
      return (ActionResult) this.Content("Unauthorized");
    try
    {
      Account account = MonnitSession.CurrentCustomer.Account;
      if (account == null)
        ;
      foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveByAccountID(MonnitSession.CurrentCustomer.AccountID))
      {
        notificationTriggered.AcknowledgeMethod = AckMethod ?? "Unknown";
        notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
        notificationTriggered.AcknowledgedBy = MonnitSession.CurrentCustomer.CustomerID;
        notificationTriggered.Save();
        AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, notificationTriggered.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, notificationTriggered.JsonStringify(), account.AccountID, "Acknowledged rule");
      }
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Error occurred");
    }
  }

  [AuthorizeDefault]
  public ActionResult ResetAllPendingConditions()
  {
    if (!MonnitSession.CustomerCan("Notification_Can_Acknowledge"))
      return (ActionResult) this.Content("Unauthorized");
    try
    {
      Account account = MonnitSession.CurrentCustomer.Account;
      if (account == null)
        ;
      foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveByAccountID(MonnitSession.CurrentCustomer.AccountID))
      {
        notificationTriggered.resetTime = DateTime.UtcNow;
        notificationTriggered.Save();
        AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, notificationTriggered.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, notificationTriggered.JsonStringify(), account.AccountID, "Reset notification with conditions still pending");
      }
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Error occurred");
    }
  }

  [AuthorizeDefault]
  public ActionResult OneviewTravelLog(string note)
  {
    try
    {
      AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, 0L, eAuditAction.Create, eAuditObject.OneviewLog, "Oneview customer use log", MonnitSession.CurrentCustomer.AccountID, note);
    }
    catch
    {
    }
    return (ActionResult) this.Content("Success");
  }
}
