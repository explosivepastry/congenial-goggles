// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.SetupController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.ControllerBase;
using iMonnit.Models;
using Microsoft.CSharp.RuntimeBinder;
using Monnit;
using Newtonsoft.Json;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

#nullable disable
namespace iMonnit.Controllers;

public class SetupController : OverviewControllerBase
{
  public ActionResult DeviceInfo(long? id, string code)
  {
    DeviceInfoModel model = new DeviceInfoModel(id, code);
    if (MonnitSession.CurrentCustomer == null)
      return (ActionResult) this.View(nameof (DeviceInfo), (object) model);
    CSNet csNet;
    if (model.Sensor != null)
    {
      csNet = CSNet.Load(model.Sensor.CSNetID);
    }
    else
    {
      if (model.Gateway == null)
        return (ActionResult) this.Content("Error: Invalid Device");
      csNet = CSNet.Load(model.Gateway.CSNetID);
    }
    if (csNet.HoldingOnlyNetwork)
      return (ActionResult) this.View(nameof (DeviceInfo), (object) model);
    return MonnitSession.CurrentCustomer.AccountID == csNet.AccountID ? (ActionResult) this.View("DeviceInfoLoggedIn", (object) model) : (ActionResult) this.View(nameof (DeviceInfo), (object) model);
  }

  [AuthorizeDefault]
  public ActionResult SensorEdit(long id)
  {
    Sensor.ClearCache(id);
    Sensor model = Sensor.Load(id);
    if (model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        Controller = "Overview"
      });
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        Controller = "Overview"
      });
    return !MonnitSession.CurrentCustomer.CanSeeNetwork(model.CSNetID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "SensorIndex",
      Controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult SensorEdit(long id, FormCollection collection)
  {
    Sensor.ClearCache(id);
    Sensor sensor = Sensor.Load(id);
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
    if (MonnitApplicationBase.CheckType(sensor.ApplicationID).HasStaticMethod("SensorScale"))
    {
      NameValueCollection returnValues;
      MonnitApplicationBase.SensorScale(sensor, (NameValueCollection) collection, out returnValues);
      foreach (string key in returnValues.Keys)
        this.ViewData[key] = (object) returnValues[key];
      // ISSUE: reference to a compiler-generated field
      if (SetupController.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SetupController.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (SetupController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SetupController.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) SetupController.\u003C\u003Eo__2.\u003C\u003Ep__0, this.ViewBag, collection["returns"]);
    }
    string str = this.SensorEditBase(sensor, collection);
    if (str == "EditConfirmation")
      return (ActionResult) this.Content($"<script>window.location.href = '/Setup/StatusVerification/{sensor.SensorID.ToString()}';</script>");
    return str.Contains("Failed") ? (ActionResult) this.Content("<script>showSimpleMessageModal('Failed to save device');location.reload();</script>") : (ActionResult) this.Content("<script>showAlertModal('Failed to save device')</script>");
  }

  public ActionResult ValidateAccount() => (ActionResult) this.View();

  [AuthorizeDefault]
  public ActionResult GatewayEdit(long id, string reset)
  {
    Gateway model = Gateway.Load(id);
    if (model == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(model.CSNetID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "GatewayIndex"
      });
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "GatewayIndex"
      });
    if (!string.IsNullOrWhiteSpace(reset))
    {
      // ISSUE: reference to a compiler-generated field
      if (SetupController.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SetupController.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SetDefaults", typeof (SetupController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SetupController.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) SetupController.\u003C\u003Eo__4.\u003C\u003Ep__0, this.ViewBag, reset.ToLower() == "true");
    }
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult GatewayEdit(long id, FormCollection collection)
  {
    Gateway gateway = Gateway.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(CSNet.Load(gateway.CSNetID).AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "GatewayIndex",
        controller = "Overview"
      });
    return this.GatewayEditBase(gateway, collection) == "EditConfirmation" ? (ActionResult) this.Content($"<script>window.location.href = '/Setup/StatusVerification/{gateway.GatewayID.ToString()}{(!string.IsNullOrWhiteSpace(collection["SetDefaults"]) ? "?reset=" + collection["SetDefaults"] : "")}';</script>") : (ActionResult) this.View((object) gateway);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult SensorSetup(long id, FormCollection collection)
  {
    return (ActionResult) this.View((object) Sensor.Load(id));
  }

  [AuthorizeDefault]
  public ActionResult QRScanResultHandler(long id, long networkid, string result)
  {
    if (result.Contains("http"))
    {
      Uri uri = new Uri(result);
      return (ActionResult) this.Content($"{HttpUtility.ParseQueryString(uri.Query).Get(nameof (id))}|{HttpUtility.ParseQueryString(uri.Query).Get("code")}");
    }
    if (!result.Contains(":"))
      return (ActionResult) this.Content("Could not scan the qr code. Try again or press the Continue without scanning button below.");
    string[] strArray = result.Split(':');
    return (ActionResult) this.Content($"{strArray[0]}|{strArray[1]}");
  }

  public ActionResult LoadUseCaseTemplateValues(long id)
  {
    Sensor model = Sensor.Load(id);
    ConfigData.AppSettings("TemplateCSNetID").ToLong();
    string name = $"SensorEdit\\ApplicationCustomization\\app_{model.ApplicationID.ToString("D3")}\\_SimpleEdit_Refresh";
    if (!MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, name, "Sensor", MonnitSession.CurrentTheme.Theme))
      return (ActionResult) this.PartialView("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default_Simple\\_SimpleEdit_Refresh.ascx", (object) model);
    // ISSUE: reference to a compiler-generated field
    if (SetupController.\u003C\u003Eo__8.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SetupController.\u003C\u003Eo__8.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (SetupController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SetupController.\u003C\u003Eo__8.\u003C\u003Ep__0.Target((CallSite) SetupController.\u003C\u003Eo__8.\u003C\u003Ep__0, this.ViewBag, name);
    return (ActionResult) this.PartialView($"~\\Views\\Sensor\\{name}.ascx", (object) model);
  }

  public ActionResult SimpleEditRefresh(Sensor sensor)
  {
    if (!MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return (ActionResult) this.Redirect("/Overview");
    string name = $"SensorEdit\\ApplicationCustomization\\app_{sensor.ApplicationID.ToString("D3")}\\_SimpleEdit_Refresh";
    if (!MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, name, "Sensor", MonnitSession.CurrentTheme.Theme))
      return (ActionResult) this.PartialView("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\_SimpleEdit_Refresh.ascx", (object) sensor);
    // ISSUE: reference to a compiler-generated field
    if (SetupController.\u003C\u003Eo__9.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SetupController.\u003C\u003Eo__9.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (SetupController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SetupController.\u003C\u003Eo__9.\u003C\u003Ep__0.Target((CallSite) SetupController.\u003C\u003Eo__9.\u003C\u003Ep__0, this.ViewBag, name);
    return (ActionResult) this.PartialView($"~\\Views\\Sensor\\{name}.ascx", (object) sensor);
  }

  public ActionResult ApplicationTemplates(long id)
  {
    Sensor.LoadByCsNetID(ConfigData.AppSettings("TemplateCSNetID").ToLong());
    return (ActionResult) this.Content("");
  }

  [HttpPost]
  public ActionResult AddDeviceRedirect(FormCollection collection)
  {
    string str1 = collection["DeviceID"];
    string str2 = collection["DeviceCode"];
    return (ActionResult) this.View((object) new AccountVerification());
  }

  public ActionResult CreateAccount()
  {
    return (ActionResult) this.View((object) new AccountVerification());
  }

  [HttpPost]
  public ActionResult CreateAccount(AccountVerification Model)
  {
    bool flag = false;
    IPBlacklist ipBlacklist = this.Request.RetrieveCurrentIPBlacklist();
    Model.EmailAddress = HttpUtility.HtmlDecode(Model.EmailAddress);
    DateTime dateTime = ipBlacklist.FirstFailedAttempt;
    dateTime = dateTime.AddMinutes((double) ConfigData.AppSettings("IPBlacklistTime", "5").ToInt());
    double totalSeconds = dateTime.Subtract(DateTime.UtcNow).TotalSeconds;
    if (totalSeconds > 0.0 && ipBlacklist.FailedAttempts > ConfigData.AppSettings("IPBlacklistCount", "5").ToInt())
    {
      this.ModelState.AddModelError("Name", $"You've exceeded the create account limit, please wait {Math.Ceiling(totalSeconds / 60.0).ToString()} minute(s) before trying again.");
      ++ipBlacklist.FailedAttempts;
      if (ipBlacklist.FailedAttempts > ConfigData.AppSettings("IPBlacklistCount", "5").ToInt() * 3)
        ipBlacklist.FirstFailedAttempt = DateTime.UtcNow.AddMinutes(60.0);
      ipBlacklist.Save();
      return (ActionResult) this.View((object) Model);
    }
    if (totalSeconds > (double) (ConfigData.AppSettings("IPBlacklistTime", "5").ToInt() * 60))
      ipBlacklist.Delete();
    if (!Customer.CheckUsernameIsUnique(Model.EmailAddress) || !Customer.CheckNotificationEmailIsUnique(Model.EmailAddress))
    {
      flag = true;
      this.ModelState.AddModelError("EmailAddress", "Email address not available");
    }
    if (string.IsNullOrEmpty(Model.EmailAddress) || !Model.EmailAddress.IsValidEmail() || !new EmailAddressAttribute().IsValid((object) Model.EmailAddress))
    {
      flag = true;
      this.ModelState.AddModelError("EmailAddress", "Unrecognized email format");
    }
    if (flag)
    {
      ++ipBlacklist.FailedAttempts;
      ipBlacklist.Save();
      return (ActionResult) this.View((object) Model);
    }
    string str = Model.EmailAddress.Trim();
    if (str.EndsWith("."))
    {
      flag = true;
      this.ModelState.AddModelError("EmailAddress", "Unrecognized email format");
    }
    if (!flag)
    {
      try
      {
        if (!(new MailAddress(Model.EmailAddress).Address == str))
        {
          flag = true;
          this.ModelState.AddModelError("EmailAddress", "Unrecognized email format");
        }
      }
      catch
      {
        flag = true;
        this.ModelState.AddModelError("EmailAddress", "Unrecognized email format");
      }
    }
    if (flag)
    {
      ++ipBlacklist.FailedAttempts;
      ipBlacklist.Save();
      return (ActionResult) this.View((object) Model);
    }
    if (!MonnitUtil.IsValidPassword(Model.Password, MonnitSession.CurrentTheme))
    {
      flag = true;
      this.ModelState.AddModelError("Password", MonnitUtil.PasswordHelperString(MonnitSession.CurrentTheme));
    }
    Random random = new Random();
    int num1 = random.Next(1000, 9999);
    Model.LocationName = $"{Model.EmailAddress.Split('@')[0]}_{num1}";
    while (!Account.CheckAccountNumberIsUnique(Model.LocationName.ToLower()))
      Model.LocationName = $"{Model.LocationName}{random.Next(9)}";
    if (flag)
    {
      ++ipBlacklist.FailedAttempts;
      ipBlacklist.Save();
      return (ActionResult) this.View((object) Model);
    }
    Model.Salt = MonnitUtil.GenerateSalt();
    Model.WorkFactor = ConfigData.AppSettings("WorkFactor").ToInt();
    Model.Password2 = MonnitUtil.GenerateHash(Model.Password, Model.Salt, Model.WorkFactor);
    ipBlacklist?.Delete();
    int num2 = new Random().Next(100000, 999999);
    Model.VerificationCode = num2.ToString();
    Model.Save();
    this.SendVerificationEmail(Model);
    return (ActionResult) this.Redirect("/Setup/ValidateEmail");
  }

  public void SendVerificationEmail(AccountVerification Model)
  {
    string Content = string.Format("\r\nThanks for signing up. Enter the code below on the verification screen to complete account activation or <a href='https://{1}/Setup/ValidateEmail?code={0}'>click here</a>.\r\n<br/><br/>\r\n<font size='6'>{0}</font> \r\n<br/><br/>\r\nYou have 24 hours to verify your account. If trying to verify after 24 hours, please restart the account set up process.\r\n<br/><br/>\r\nIf you did not request to verify this account, please disregard this message.\r\n<br/>\r\n", (object) Model.VerificationCode, (object) MonnitSession.CurrentTheme.Domain);
    using (MailMessage mail = new MailMessage())
    {
      using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, MonnitSession.CurrentTheme))
      {
        EmailTemplate emailTemplate = EmailTemplate.LoadBest(Account.Load(MonnitSession.CurrentTheme.AccountID), eEmailTemplateFlag.Generic);
        if (emailTemplate == null)
        {
          emailTemplate = new EmailTemplate();
          emailTemplate.Template = "{Content}";
        }
        mail.Subject = "Account Verification";
        mail.IsBodyHtml = true;
        mail.Body = emailTemplate.MailMerge(Content, Model.EmailAddress);
        try
        {
          mail.To.Clear();
          mail.To.Add(new MailAddress(Model.EmailAddress, Model.Name));
          MonnitUtil.SendMail(mail, smtpClient);
        }
        catch (Exception ex)
        {
          ex.Log("AccountVerification.SendVerificationEmail");
        }
      }
    }
  }

  public ActionResult ResendCode(string email)
  {
    AccountVerification Model = AccountVerification.LoadByEmailAddress(email);
    if (Model == null)
      return (ActionResult) this.Content("Record not found.");
    this.SendVerificationEmail(Model);
    return (ActionResult) this.Content("Sent");
  }

  public ActionResult ValidateEmail() => (ActionResult) this.View();

  [HttpPost]
  public ActionResult ValidateEmail(string VerificationCode)
  {
    AccountVerification model = (AccountVerification) null;
    VerificationCode = Regex.Replace(VerificationCode, "\\s+", "");
    if (string.IsNullOrEmpty(VerificationCode))
    {
      this.ModelState.AddModelError(nameof (VerificationCode), "The Verification Code is Required");
    }
    else
    {
      model = AccountVerification.LoadByCode(VerificationCode);
      if (model == null)
      {
        this.ModelState.AddModelError(nameof (VerificationCode), "Invalid Verification Code");
        return (ActionResult) this.View((object) model);
      }
    }
    if (!Customer.CheckUsernameIsUnique(model.EmailAddress))
    {
      this.ModelState.AddModelError("Username", "Username not available");
      ViewDataDictionary viewData = this.ViewData;
      viewData["Exception"] = (object) $"{viewData["Exception"]?.ToString()}An account with username {model.EmailAddress} already exists.";
    }
    if (!Customer.CheckNotificationEmailIsUnique(model.EmailAddress))
    {
      this.ModelState.AddModelError("NotificationEmail", "Email address not available");
      ViewDataDictionary viewData = this.ViewData;
      viewData["Exception"] = (object) $"{viewData["Exception"]?.ToString()}An account with email {model.EmailAddress} already exists.";
    }
    Account account1 = new Account();
    if (model != null && this.ModelState.IsValid)
    {
      try
      {
        IANATimeZone ianaTimeZone = IANATimeZone.Find(model.IanaTimeZone);
        account1.CompanyName = model.LocationName;
        account1.AccountNumber = model.LocationName;
        account1.TimeZoneID = ianaTimeZone == null ? 1L : ianaTimeZone.TimeZoneID;
        account1.EULAVersion = MonnitSession.CurrentTheme.CurrentEULA;
        account1.EULADate = DateTime.UtcNow;
        account1.MaxFailedLogins = 10;
        account1.IsCFRCompliant = false;
        account1.CreateDate = DateTime.UtcNow;
        Account account2 = Account.Load(MonnitSession.CurrentTheme.AccountID) ?? Account.Load(ConfigData.AppSettings("AdminAccountID").ToLong());
        account1.RetailAccountID = account2.AccountID;
        account1.Save();
        account1.AccountIDTree = $"{account2.AccountIDTree}{account1.AccountID.ToString()}*";
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
        accountSubscription3.AccountID = account1.AccountID;
        accountSubscription3.AccountSubscriptionTypeID = 10L;
        AccountSubscription accountSubscription4 = accountSubscription3;
        today = DateTime.Today;
        DateTime dateTime2 = today.AddDays(45.0);
        accountSubscription4.ExpirationDate = dateTime2;
        accountSubscription3.Save();
        Customer customer = new Customer();
        string[] strArray = model.Name.Trim().Split(new char[1]
        {
          ' '
        }, 2);
        if (strArray.Length == 1)
        {
          customer.FirstName = "";
          customer.LastName = strArray[0];
        }
        else
        {
          customer.FirstName = strArray[0];
          customer.LastName = strArray[1];
        }
        customer.AccountID = account1.AccountID;
        customer.UserName = model.EmailAddress;
        customer.NotificationEmail = model.EmailAddress;
        customer.Salt = model.Salt;
        customer.WorkFactor = model.WorkFactor;
        customer.Password2 = model.Password2;
        customer.IsAdmin = true;
        customer.Save();
        AccountVerification.LoadByEmailAddress(model.EmailAddress).Delete();
        account1.PrimaryContactID = customer.CustomerID;
        account1.Save();
        CSNet DBObject = new CSNet();
        DBObject.AccountID = account1.AccountID;
        DBObject.Name = model.LocationName;
        DBObject.SendNotifications = true;
        DBObject.Save();
        long permissionTypeId = CustomerPermissionType.Find("Network_View").CustomerPermissionTypeID;
        new CustomerPermission()
        {
          CSNetID = DBObject.CSNetID,
          CustomerID = customer.CustomerID,
          CustomerPermissionTypeID = permissionTypeId,
          Can = true
        }.Save();
        DBObject.LogAuditData(eAuditAction.Create, eAuditObject.Network, customer.CustomerID, account1.AccountID, "Created new network");
        FormsAuthentication.SetAuthCookie(customer.UserName, false);
        MonnitSession.CurrentCustomer = customer;
        new SystemHelp()
        {
          AccountID = customer.AccountID,
          CustomerID = customer.CustomerID,
          Type = "Customer_Setup",
          CreateDate = DateTime.UtcNow
        }.Save();
        return (ActionResult) this.Redirect("/Setup/AccountWelcome/");
      }
      catch (Exception ex)
      {
        this.ModelState.AddModelError("", ex.Message);
        ViewDataDictionary viewData = this.ViewData;
        viewData["Exception"] = (object) (viewData["Exception"]?.ToString() + ex.Message);
      }
    }
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult AssignDevice(long id, long? networkID)
  {
    AssignObjectModel model = new AssignObjectModel();
    model.AccountID = id;
    int num1;
    if (networkID.HasValue)
    {
      long? nullable = networkID;
      long num2 = 0;
      num1 = nullable.GetValueOrDefault() <= num2 & nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 1;
    if (num1 != 0)
      networkID = new long?(model.Networks.Count >= 1 ? model.Networks.First<CSNet>().CSNetID : 0L);
    model.NetworkID = networkID.GetValueOrDefault();
    if (model.Networks.Count == 0)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Create",
        controller = "Network",
        id = id
      });
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID) || !MonnitSession.CurrentCustomer.CanSeeNetwork(model.NetworkID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CustomerCan("Network_Edit"))
      return this.PermissionError(ThemeController.PermissionErrorMessage.MissingCustomerPermission("Network_Edit"), methodName: nameof (AssignDevice), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SetupController.cs", sourceLineNumber: 688);
    Account account = Account.Load(id);
    if (Sensor.LoadByAccountID(account.AccountID).Count >= account.CurrentSubscription.AccountSubscriptionType.AllowedSensors)
      this.ModelState.AddModelError("LimitReached", string.Format("Sensor limit has been reached for your account."));
    if (model.Networks.Count == 0)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Create",
        controller = "Network",
        id = id
      });
    return this.Request.IsSensorCertMobile() ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = nameof (AssignDevice),
      controller = "Network",
      id = id,
      networkID = networkID
    }) : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult StatusVerification(long ID)
  {
    Sensor sensor = Sensor.Load(ID);
    if (sensor != null)
      return !MonnitSession.CurrentCustomer.CanSeeSensor(sensor) ? (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      }) : (ActionResult) this.View((object) sensor);
    Gateway model = Gateway.Load(ID);
    return model == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(model.CSNetID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View("GatewayStatusVerification", (object) model);
  }

  [AuthorizeDefault]
  public ActionResult StatusVerificationSensorRefresh(long ID)
  {
    Sensor sensor = Sensor.Load(ID);
    if (!MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    foreach (Gateway gateway in Gateway.LoadByCSNetID(sensor.CSNetID))
    {
      if (gateway.Status == eSensorStatus.OK)
      {
        flag1 = true;
        if (gateway.LastCommunicationDate > sensor.StartDate)
          flag2 = true;
      }
    }
    if (sensor.Status == eSensorStatus.OK)
      flag3 = true;
    return (ActionResult) this.Content($"{{\"GatewayOnline\":\"{flag1}\", \"GatewayCheckedInFresh\":\"{flag2}\", \"SensorOnline\":\"{flag3}\" }}");
  }

  [AuthorizeDefault]
  public ActionResult StatusVerificationGatewayRefresh(long ID)
  {
    Gateway gateway = Gateway.Load(ID);
    if (!MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    bool flag = false;
    if (gateway.Status == eSensorStatus.OK)
      flag = true;
    return (ActionResult) this.Content($"{{\"GatewayOnline\":\"{flag}\"}}");
  }

  [AuthorizeDefault]
  public ActionResult DefaultActions(long id)
  {
    Sensor sensor = Sensor.Load(id);
    if (sensor != null)
    {
      if (!MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index",
          controller = "Overview"
        });
      // ISSUE: reference to a compiler-generated field
      if (SetupController.\u003C\u003Eo__22.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SetupController.\u003C\u003Eo__22.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Sensor, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Sensor", typeof (SetupController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SetupController.\u003C\u003Eo__22.\u003C\u003Ep__0.Target((CallSite) SetupController.\u003C\u003Eo__22.\u003C\u003Ep__0, this.ViewBag, sensor);
      return (ActionResult) this.View((object) Notification.LoadBySensorID(sensor.TemplateSensorID));
    }
    Gateway gateway = Gateway.Load(id);
    return gateway == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) Notification.LoadByGatewayID(gateway.GatewayID));
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult DefaultActions(
    long id,
    List<long> notificationIDList,
    List<string> sendMethod)
  {
    long accountId = CSNet.Load(ConfigData.AppSettings("TemplateCSNetID").ToLong()).AccountID;
    Sensor sensor = Sensor.Load(id);
    Gateway gateway = Gateway.Load(id);
    bool flag = sensor != null;
    Notification notification1 = new Notification();
    notification1.SensorID = id;
    notification1.AccountID = accountId;
    notification1.NotificationText = "<strong>Low Battery Notification -</strong> The battery in your device is low. Please replace or charge it soon. <hr> <div>Account: {AccountNumber} </div> <br> <div> Device: {Name} ({ID})</div><div> Battery Level: {Reading}</div><br>";
    notification1.CompareType = eCompareType.Less_Than_or_Equal;
    notification1.Version = "1";
    notification1.CompareValue = "10";
    notification1.SnoozeDuration = 4320;
    notification1.Name = "Battery below 10%";
    notification1.IsActive = true;
    notification1.NotificationClass = eNotificationClass.Low_Battery;
    notification1.AlwaysSend = true;
    notification1.Subject = "Sensor Alert - Battery Levels for {Name} ({ID}) are Nearly Depleted";
    notification1.DatumIndex = 0;
    notification1.ScaleID = 0;
    notification1.eDatumType = eDatumType.Error;
    notification1.SMSText = "The batteries in this device {Name} ({ID}) are nearly depleted. Please Replace Immediately.";
    notification1.CanAutoAcknowledge = true;
    notification1.HasUserNotificationAction = true;
    Notification notification2 = new Notification();
    if (flag)
      notification2.SensorID = id;
    notification2.AccountID = accountId;
    notification2.NotificationText = "<strong>Inactivity Notification -</strong> Your device has not communicated within the expected timeframe. Please check its status.<hr> <div> Account: {AccountNumber} </div> <br> <div> Device: {Name} ({ID})</div> <div> Last Communication: {Reading} </div> <br>";
    notification2.CompareType = eCompareType.Greater_Than;
    notification2.Version = "1";
    notification2.CompareValue = flag ? ((sensor.ReportInterval + 10.0) * 2.0).ToString() : ((gateway.ReportInterval + 1.0) * 2.0).ToString();
    notification2.SnoozeDuration = 240 /*0xF0*/;
    notification2.Name = "Device is Inactive";
    notification2.IsActive = true;
    notification2.NotificationClass = eNotificationClass.Inactivity;
    notification2.AlwaysSend = true;
    notification2.Subject = "Device Alert - Device is Inactive - {Name} ({ID})";
    notification2.DatumIndex = 0;
    notification2.ScaleID = 0;
    notification2.eDatumType = eDatumType.Error;
    notification2.SMSText = "Device Alert - Device is Inactive - {Name} ({ID}) -  Account: {AccountNumber}";
    notification2.CanAutoAcknowledge = true;
    notification2.HasUserNotificationAction = true;
    if (notificationIDList != null)
    {
      foreach (long notificationId in notificationIDList)
      {
        Notification notification3 = new Notification();
        Notification notification4;
        switch (notificationId)
        {
          case -2:
            notification4 = notification2;
            break;
          case -1:
            notification4 = notification1;
            break;
          default:
            notification4 = Notification.Load(notificationId);
            break;
        }
        if (notification4.AccountID == accountId)
        {
          Notification notification5 = notification4;
          notification5.NotificationID = long.MinValue;
          notification5.PrimaryKeyValue = long.MinValue;
          notification5.AccountID = MonnitSession.CurrentCustomer.AccountID;
          notification5.Save();
          if (flag)
          {
            SensorNotification sensorNotification1 = SensorNotification.LoadBySensorIDAndNotificationID(sensor.TemplateSensorID, notificationId);
            if (sensorNotification1 != null)
            {
              SensorNotification sensorNotification2 = sensorNotification1;
              sensorNotification2.SensorNotificationID = long.MinValue;
              sensorNotification2.PrimaryKeyValue = long.MinValue;
              sensorNotification2.SensorID = id;
              sensorNotification2.NotificationID = notification5.NotificationID;
              sensorNotification2.Save();
            }
            else
              new SensorNotification()
              {
                SensorID = id,
                NotificationID = notification5.NotificationID,
                DatumIndex = 0,
                AutoReset = false,
                TriggerDate = DateTime.MinValue
              }.Save();
          }
          else
            new GatewayNotification()
            {
              GatewayID = id,
              NotificationID = notification5.NotificationID,
              AutoReset = false,
              TriggerDate = DateTime.MinValue
            }.Save();
          if (sendMethod != null)
          {
            foreach (string str in sendMethod)
            {
              eNotificationType notificationType = (eNotificationType) 0;
              switch (str)
              {
                case "notificationToEmail":
                  notificationType = eNotificationType.Email;
                  break;
                case "notificationToText":
                  notificationType = eNotificationType.SMS;
                  break;
                case "notificationToVoice":
                  notificationType = eNotificationType.Phone;
                  break;
              }
              new NotificationRecipient()
              {
                NotificationID = notification5.NotificationID,
                CustomerToNotifyID = MonnitSession.CurrentCustomer.CustomerID,
                NotificationType = notificationType
              }.Save();
            }
          }
          switch (notification4.NotificationClass)
          {
            case eNotificationClass.Advanced:
              using (List<AdvancedNotificationParameterValue>.Enumerator enumerator = AdvancedNotificationParameterValue.LoadByNotificationID(notificationId).GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  AdvancedNotificationParameterValue current = enumerator.Current;
                  current.AdvancedNotificationParameterValueID = long.MinValue;
                  current.PrimaryKeyValue = long.MinValue;
                  current.NotificationID = notification5.NotificationID;
                  current.Save();
                }
                break;
              }
            case eNotificationClass.Timed:
              NotificationByTime notificationByTime = NotificationByTime.LoadByNotificationID(notificationId);
              notificationByTime.NotificationByTimeID = long.MinValue;
              notificationByTime.PrimaryKeyValue = long.MinValue;
              notificationByTime.Save();
              notification5.NotificationByTimeID = notificationByTime.NotificationByTimeID;
              notification5.Save();
              break;
          }
        }
      }
    }
    return (ActionResult) this.Content("Success");
  }

  public ActionResult GatewayComplete(long id)
  {
    if (!MonnitSession.CustomerCan("Network_Edit"))
      return this.PermissionError(methodName: nameof (GatewayComplete), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SetupController.cs", sourceLineNumber: 1039);
    Gateway model = Gateway.Load(id);
    if (model == null)
      return (ActionResult) this.Redirect("/setup/QASteps/");
    CSNet csNet = CSNet.Load(model.CSNetID);
    if (csNet == null || !MonnitSession.CurrentCustomer.CanSeeAccount(csNet.AccountID))
      return (ActionResult) this.Redirect("/setup/QASteps/");
    // ISSUE: reference to a compiler-generated field
    if (SetupController.\u003C\u003Eo__24.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SetupController.\u003C\u003Eo__24.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (SetupController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SetupController.\u003C\u003Eo__24.\u003C\u003Ep__0.Target((CallSite) SetupController.\u003C\u003Eo__24.\u003C\u003Ep__0, this.ViewBag, csNet.AccountID);
    return (ActionResult) this.View((object) model);
  }

  public ActionResult SensorComplete(long id)
  {
    if (!MonnitSession.CustomerCan("Network_Edit"))
      return this.PermissionError(methodName: nameof (SensorComplete), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SetupController.cs", sourceLineNumber: 1064);
    Sensor model = Sensor.Load(id);
    return model == null || !MonnitSession.CurrentCustomer.CanSeeAccount(model.AccountID) ? (ActionResult) this.Redirect("/setup/QASteps/") : (ActionResult) this.View((object) model);
  }

  public ActionResult InstallApp()
  {
    return MonnitSession.CurrentTheme.AllowPWA && MonnitSession.CurrentStyle("MobileAppName").Length > 0 && MonnitSession.CurrentStyles["MobileAppLogo"].BinaryValue.Length != 0 ? (ActionResult) this.View((object) MonnitSession.CurrentTheme) : (ActionResult) this.Redirect("/Overview");
  }

  public ActionResult PWAManifest()
  {
    return (ActionResult) this.View((object) MonnitSession.CurrentTheme);
  }

  public ActionResult NoNetwork() => (ActionResult) this.View();

  public ActionResult PWAIcon(string image)
  {
    try
    {
      image = image.Replace(".png", "");
      string[] strArray = image.Split('x');
      int width = strArray[0].ToInt();
      int height = strArray[1].ToInt();
      byte[] binaryValue = MonnitSession.CurrentStyles["MobileAppLogo"].BinaryValue;
      if (binaryValue == null)
        throw new Exception("No Icon Found");
      byte[] array;
      using (Bitmap bitmap1 = new Bitmap(width, height))
      {
        using (MemoryStream memoryStream = new MemoryStream(binaryValue))
        {
          using (Bitmap bitmap2 = new Bitmap((Stream) memoryStream))
          {
            using (Graphics graphics = Graphics.FromImage((Image) bitmap1))
            {
              graphics.SmoothingMode = SmoothingMode.AntiAlias;
              graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
              if (width == height)
                graphics.DrawImage((Image) bitmap2, 0, 0, width, height);
              else if (width > height)
              {
                int num = width - height;
                graphics.DrawImage((Image) bitmap2, num / 2, 0, width - num, height);
              }
              else if (width < height)
              {
                int num = height - width;
                graphics.DrawImage((Image) bitmap2, 0, num / 2, width, height - num);
              }
            }
          }
        }
        using (MemoryStream memoryStream = new MemoryStream())
        {
          bitmap1.Save((Stream) memoryStream, ImageFormat.Png);
          array = memoryStream.ToArray();
        }
      }
      return (ActionResult) new FileContentResult(array, "image/png");
    }
    catch (Exception ex)
    {
      ex.Log("SetupController.PWAIcon | image = " + image);
      return (ActionResult) this.View("Error");
    }
  }

  [HttpPost]
  public ActionResult EnablePushMessageSubscription(string subJson)
  {
    string content = "Failed to subscribe.";
    try
    {
      object obj1 = JsonConvert.DeserializeObject(subJson);
      // ISSUE: reference to a compiler-generated field
      if (SetupController.\u003C\u003Eo__30.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SetupController.\u003C\u003Eo__30.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (SetupController)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target1 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p1 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (SetupController.\u003C\u003Eo__30.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SetupController.\u003C\u003Eo__30.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (SetupController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__0.Target((CallSite) SetupController.\u003C\u003Eo__30.\u003C\u003Ep__0, obj1, "guid");
      string input = target1((CallSite) p1, obj2);
      // ISSUE: reference to a compiler-generated field
      if (SetupController.\u003C\u003Eo__30.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SetupController.\u003C\u003Eo__30.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (SetupController)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target2 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p3 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (SetupController.\u003C\u003Eo__30.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SetupController.\u003C\u003Eo__30.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (SetupController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__2.Target((CallSite) SetupController.\u003C\u003Eo__30.\u003C\u003Ep__2, obj1, "endpoint");
      string endpointUrl = target2((CallSite) p3, obj3);
      // ISSUE: reference to a compiler-generated field
      if (SetupController.\u003C\u003Eo__30.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SetupController.\u003C\u003Eo__30.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (SetupController)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target3 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__6.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p6 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__6;
      // ISSUE: reference to a compiler-generated field
      if (SetupController.\u003C\u003Eo__30.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SetupController.\u003C\u003Eo__30.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (SetupController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string, object> target4 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__5.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string, object>> p5 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__5;
      // ISSUE: reference to a compiler-generated field
      if (SetupController.\u003C\u003Eo__30.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SetupController.\u003C\u003Eo__30.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (SetupController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__4.Target((CallSite) SetupController.\u003C\u003Eo__30.\u003C\u003Ep__4, obj1, "keys");
      object obj5 = target4((CallSite) p5, obj4, "p256dh");
      string str1 = target3((CallSite) p6, obj5);
      // ISSUE: reference to a compiler-generated field
      if (SetupController.\u003C\u003Eo__30.\u003C\u003Ep__9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SetupController.\u003C\u003Eo__30.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (SetupController)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target5 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__9.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p9 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__9;
      // ISSUE: reference to a compiler-generated field
      if (SetupController.\u003C\u003Eo__30.\u003C\u003Ep__8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SetupController.\u003C\u003Eo__30.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (SetupController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string, object> target6 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__8.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string, object>> p8 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__8;
      // ISSUE: reference to a compiler-generated field
      if (SetupController.\u003C\u003Eo__30.\u003C\u003Ep__7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SetupController.\u003C\u003Eo__30.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (SetupController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj6 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__7.Target((CallSite) SetupController.\u003C\u003Eo__30.\u003C\u003Ep__7, obj1, "keys");
      object obj7 = target6((CallSite) p8, obj6, "auth");
      string str2 = target5((CallSite) p9, obj7);
      CustomerPushMessageSubscription messageSubscription1 = (CustomerPushMessageSubscription) null;
      if (!string.IsNullOrWhiteSpace(input))
        messageSubscription1 = CustomerPushMessageSubscription.LoadByGuid(Guid.Parse(input));
      if (messageSubscription1 == null && MonnitSession.CurrentCustomer != null)
        messageSubscription1 = CustomerPushMessageSubscription.LoadByCustomerIDandEndpoint(MonnitSession.CurrentCustomer.CustomerID, endpointUrl);
      if (messageSubscription1 == null && MonnitSession.CurrentCustomer != null)
      {
        messageSubscription1 = new CustomerPushMessageSubscription();
        messageSubscription1.CustomerID = MonnitSession.CurrentCustomer.CustomerID;
        messageSubscription1.SubscriptionGuid = Guid.NewGuid();
        messageSubscription1.CreateDate = DateTime.UtcNow;
        CustomerPushMessageSubscription messageSubscription2 = messageSubscription1;
        // ISSUE: reference to a compiler-generated field
        if (SetupController.\u003C\u003Eo__30.\u003C\u003Ep__11 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SetupController.\u003C\u003Eo__30.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (SetupController)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, string> target7 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__11.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, string>> p11 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__11;
        // ISSUE: reference to a compiler-generated field
        if (SetupController.\u003C\u003Eo__30.\u003C\u003Ep__10 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SetupController.\u003C\u003Eo__30.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (SetupController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj8 = SetupController.\u003C\u003Eo__30.\u003C\u003Ep__10.Target((CallSite) SetupController.\u003C\u003Eo__30.\u003C\u003Ep__10, obj1, "name");
        string str3 = target7((CallSite) p11, obj8);
        messageSubscription2.Name = str3;
      }
      if (messageSubscription1 != null)
      {
        messageSubscription1.EndpointUrl = endpointUrl;
        messageSubscription1.P256DH = str1;
        messageSubscription1.Auth = str2;
        messageSubscription1.Save();
        int num;
        if (string.IsNullOrWhiteSpace(input))
        {
          Guid subscriptionGuid = messageSubscription1.SubscriptionGuid;
          if (messageSubscription1.SubscriptionGuid != Guid.Empty)
          {
            num = 1;
            goto label_36;
          }
        }
        num = input.ToLower() != messageSubscription1.SubscriptionGuid.ToString().ToLower() ? 1 : 0;
label_36:
        if (num != 0)
          input = messageSubscription1.SubscriptionGuid.ToString();
        content = "Success|" + input;
      }
    }
    catch (Exception ex)
    {
      ex.Log("SetupController.EnablePushMessageSubscription ");
      content = "Failed";
    }
    return (ActionResult) this.Content(content);
  }

  [HttpPost]
  public ActionResult DeletePushMessageSubscription(string subJson)
  {
    string content;
    try
    {
      long customerId = MonnitSession.CurrentCustomer.CustomerID;
      object obj1 = JsonConvert.DeserializeObject(subJson);
      // ISSUE: reference to a compiler-generated field
      if (SetupController.\u003C\u003Eo__31.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SetupController.\u003C\u003Eo__31.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (SetupController)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target = SetupController.\u003C\u003Eo__31.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p1 = SetupController.\u003C\u003Eo__31.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (SetupController.\u003C\u003Eo__31.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SetupController.\u003C\u003Eo__31.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (SetupController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SetupController.\u003C\u003Eo__31.\u003C\u003Ep__0.Target((CallSite) SetupController.\u003C\u003Eo__31.\u003C\u003Ep__0, obj1, "endpoint");
      string endpointUrl = target((CallSite) p1, obj2);
      CustomerPushMessageSubscription messageSubscription = CustomerPushMessageSubscription.LoadByCustomerIDandEndpoint(customerId, endpointUrl);
      if (messageSubscription != null)
      {
        messageSubscription.Delete();
        content = "Success";
      }
      else
        content = "Failed to find subscription";
    }
    catch (Exception ex)
    {
      ex.Log("SetupController.DeletePushMessageSubscription ");
      content = "Failed";
    }
    return (ActionResult) this.Content(content);
  }

  [HttpPost]
  public ActionResult DeletePushMessageSubscriptionByID(long id)
  {
    string content;
    try
    {
      CustomerPushMessageSubscription messageSubscription = CustomerPushMessageSubscription.Load(id);
      if (messageSubscription != null)
      {
        messageSubscription.Delete();
        content = "Success";
      }
      else
        content = "Failed to find subscription";
    }
    catch (Exception ex)
    {
      ex.Log($"SetupController.DeletePushMessageSubscriptionByID[ID: {id.ToString()}] ");
      content = "Failed";
    }
    return (ActionResult) this.Content(content);
  }

  [HttpPost]
  [ValidateInput(false)]
  public ActionResult SendPushMessage(
    string msg,
    string endpoint,
    string p256dhKey,
    string auth,
    long? custID)
  {
    string content;
    try
    {
      Customer cust = MonnitSession.CurrentCustomer != null ? MonnitSession.CurrentCustomer : Customer.Load(custID ?? 5726L);
      content = Notification.SendPushNotificationTest(msg, cust, endpoint, p256dhKey, auth);
    }
    catch (Exception ex)
    {
      content = ex.Message;
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public ActionResult AccountWelcome()
  {
    MonnitSession.IsAccountNew = true;
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  [HttpGet]
  public ActionResult NewCustomer(long id)
  {
    if (!MonnitSession.IsAuthorizedForAccount(Customer.Load(id).AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = nameof (NewCustomer),
        controller = "Setup"
      });
    this.ViewData["AccountID"] = (object) id;
    return (ActionResult) this.View((object) MonnitSession.CurrentCustomer);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult NewCustomer(long id, FormCollection collection)
  {
    Customer customer = Customer.Load(id);
    if (customer == null || customer.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(customer.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    Account account = Account.Load(customer.AccountID);
    AccountTheme accountTheme = AccountTheme.Find(account.AccountID);
    customer.FirstName = collection["FirstName"];
    customer.LastName = collection["LastName"];
    if (string.IsNullOrEmpty(customer.FirstName))
      this.ModelState.AddModelError("FirstName", "First Name is Required!");
    if (string.IsNullOrEmpty(customer.LastName))
      this.ModelState.AddModelError("LastName", "Last Name is Required!");
    customer.NotificationEmail = collection["NotificationEmail"].Replace(" ", "");
    customer.NotificationPhone = collection["NotificationPhone"].Trim();
    customer.NotificationPhoneISOCode = string.IsNullOrEmpty(collection["NotificationPhoneISOCode"]) ? customer.NotificationPhoneISOCode : collection["NotificationPhoneISOCode"].ToStringSafe();
    customer.NotificationPhone2 = collection["NotificationPhone2"];
    customer.NotificationPhone2ISOCode = string.IsNullOrEmpty(collection["NotificationPhone2ISOCode"]) ? customer.NotificationPhone2ISOCode : collection["NotificationPhone2ISOCode"].ToStringSafe();
    customer.SendSensorNotificationToText = !string.IsNullOrEmpty(customer.NotificationPhone);
    if (string.IsNullOrEmpty(accountTheme.FromPhone))
    {
      customer.DirectSMS = false;
      customer.SendSensorNotificationToVoice = false;
    }
    else
    {
      customer.DirectSMS = !string.IsNullOrEmpty(collection["DirectSMS"]);
      customer.SendSensorNotificationToVoice = !string.IsNullOrEmpty(customer.NotificationPhone2);
    }
    if (!customer.SendSensorNotificationToText)
    {
      foreach (Notification notification in Notification.LoadByAccountID(customer.AccountID))
      {
        foreach (NotificationRecipient notificationRecipient in notification.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.CustomerToNotifyID == customer.CustomerID && m.NotificationType == eNotificationType.SMS)))
        {
          if (notification.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.CustomerToNotifyID == customer.CustomerID && m.NotificationType == eNotificationType.Email)).Count<NotificationRecipient>() > 0)
          {
            notificationRecipient.Delete();
          }
          else
          {
            notificationRecipient.NotificationType = eNotificationType.Email;
            notificationRecipient.Save();
          }
        }
      }
    }
    if (!customer.SendSensorNotificationToVoice)
    {
      foreach (Notification notification in Notification.LoadByAccountID(customer.AccountID))
      {
        foreach (NotificationRecipient notificationRecipient in notification.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.CustomerToNotifyID == customer.CustomerID && m.NotificationType == eNotificationType.Phone)))
        {
          if (notification.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.CustomerToNotifyID == customer.CustomerID && m.NotificationType == eNotificationType.Email)).Count<NotificationRecipient>() > 0)
          {
            notificationRecipient.Delete();
          }
          else
          {
            notificationRecipient.NotificationType = eNotificationType.Email;
            notificationRecipient.Save();
          }
        }
      }
    }
    customer.UISMSCarrierID = customer.DirectSMS ? new long?() : new long?(collection["UISMSCarrierID"].ToLong());
    if (string.IsNullOrEmpty(customer.NotificationEmail))
      this.ModelState.AddModelError("NotificationEmailRequired", "Email is Required!");
    else if (!customer.NotificationEmail.IsValidEmail())
      this.ModelState.AddModelError("NotificationEmailInvalid", "Invalid Email Address");
    else if (!Customer.CheckNotificationEmailIsUnique(customer.NotificationEmail, customer.CustomerID))
      this.ModelState.AddModelError("NotificationEmailNotUnique", "Email/Login Must Be Unique");
    if (customer.UserName != customer.NotificationEmail)
    {
      if (Customer.CheckUsernameIsUnique(customer.NotificationEmail))
        customer.UserName = customer.NotificationEmail;
      else
        this.ModelState.AddModelError("NotificationEmailNotUnique", "Email/Login Must Be Unique");
    }
    if (customer.SendSensorNotificationToText)
    {
      if (customer.DirectSMS)
      {
        if (customer.NotificationPhone.Length < 5)
          this.ModelState.AddModelError("NotificationPhoneInvalid", "Invalid SMS Number");
      }
      else
      {
        int num1;
        if (customer.UISMSCarrierID.HasValue)
        {
          long? uismsCarrierId = customer.UISMSCarrierID;
          long num2 = 0;
          num1 = uismsCarrierId.GetValueOrDefault() <= num2 & uismsCarrierId.HasValue ? 1 : 0;
        }
        else
          num1 = 1;
        if (num1 != 0)
          this.ModelState.AddModelError("UISMSCarrierID", "Carrier selection Required");
        if (customer.SMSCarrier != null && customer.NotificationPhone.RemoveNonNumeric().Count<char>() != customer.SMSCarrier.ExpectedPhoneDigits)
          this.ModelState.AddModelError("NotificationPhoneInvalidLength", "Invalid number of digits for selected provider.");
      }
    }
    if (customer.SendSensorNotificationToVoice && customer.NotificationPhone2.Length < 5)
      this.ModelState.AddModelError("NotificationPhone2Invalid", "Invalid Voice Number");
    customer.AllowPushNotification = accountTheme.AllowPushNotification;
    if (this.ModelState.IsValid)
    {
      try
      {
        customer.Save();
        if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
          MonnitSession.CurrentCustomer = customer;
        customer.LogAuditData(eAuditAction.Update, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited customer details");
        // ISSUE: reference to a compiler-generated field
        if (SetupController.\u003C\u003Eo__36.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SetupController.\u003C\u003Eo__36.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof (SetupController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = SetupController.\u003C\u003Eo__36.\u003C\u003Ep__0.Target((CallSite) SetupController.\u003C\u003Eo__36.\u003C\u003Ep__0, this.ViewBag, "Success");
        return (ActionResult) this.View((object) customer);
      }
      catch (Exception ex)
      {
        this.ViewData.ModelState.AddModelError("Unknown", ex.Message);
        // ISSUE: reference to a compiler-generated field
        if (SetupController.\u003C\u003Eo__36.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SetupController.\u003C\u003Eo__36.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof (SetupController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = SetupController.\u003C\u003Eo__36.\u003C\u003Ep__1.Target((CallSite) SetupController.\u003C\u003Eo__36.\u003C\u003Ep__1, this.ViewBag, ex.Message);
      }
    }
    return (ActionResult) this.View((object) customer);
  }

  [AuthorizeDefault]
  public ActionResult NewRule() => (ActionResult) this.View();

  [AuthorizeDefault]
  public ActionResult QASteps() => (ActionResult) this.View();
}
