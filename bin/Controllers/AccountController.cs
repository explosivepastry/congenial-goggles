// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.AccountController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.ControllerBase;
using iMonnit.Models;
using Microsoft.CSharp.RuntimeBinder;
using Monnit;
using OtpNet;
using RedefineImpossible;
using Saml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

#nullable disable
namespace iMonnit.Controllers;

[NoCache]
[HandleError]
public class AccountController : AccountControllerBase
{
  public ActionResult NoKey()
  {
    return this.PermissionError(ThemeController.PermissionErrorMessage.MissingOrInvalidKey(), methodName: nameof (NoKey), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\AccountController.cs", sourceLineNumber: 30);
  }

  public ActionResult LogOnOV()
  {
    try
    {
      if (this.ProgramLevel() == eProgramLevel.NKR)
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "NoKey",
          controller = "Account"
        });
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
    }
    LogOnModel model = new LogOnModel();
    this.CheckUserNameCookie(model);
    this.CheckRememberMeCookie(model);
    return model.RememberMe ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      Controller = "Overview",
      action = "Index"
    }) : (ActionResult) this.View((object) model);
  }

  [HttpPost]
  public ActionResult LogOnOV(LogOnModel model, string returnUrl)
  {
    try
    {
      AntiForgery.Validate();
    }
    catch
    {
      this.ModelState.AddModelError("", "Unexpected error");
    }
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (returnUrl), typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AccountController.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__2.\u003C\u003Ep__0, this.ViewBag, returnUrl);
    return this.InternalLogOnOV(model, nameof (LogOnOV));
  }

  public ActionResult TwoFactorAuth(Customer model, string returnUrl)
  {
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (returnUrl), typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = AccountController.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__3.\u003C\u003Ep__0, this.ViewBag, returnUrl);
    switch (model.TFAMethod)
    {
      case 1:
      case 3:
        return this.TwoFactorAuthResponse(model.TFAMethod.ToString());
      case 2:
        if (!UnsubscribedNotificationEmail.EmailIsUnsubscribed(model.NotificationEmail))
          return this.TwoFactorAuthResponse(model.TFAMethod.ToString());
        break;
    }
    if (!string.IsNullOrWhiteSpace(AccountTheme.Find(MonnitSession.CurrentTheme.AccountID).SMTPFriendlyName))
    {
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "smtp", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = AccountController.\u003C\u003Eo__3.\u003C\u003Ep__1.Target((CallSite) AccountController.\u003C\u003Eo__3.\u003C\u003Ep__1, this.ViewBag, true);
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__3.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__3.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "smtp", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = AccountController.\u003C\u003Eo__3.\u003C\u003Ep__2.Target((CallSite) AccountController.\u003C\u003Eo__3.\u003C\u003Ep__2, this.ViewBag, false);
    }
    if (MonnitSession.CurrentTheme.Theme == "Default")
    {
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__3.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__3.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "show", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = AccountController.\u003C\u003Eo__3.\u003C\u003Ep__3.Target((CallSite) AccountController.\u003C\u003Eo__3.\u003C\u003Ep__3, this.ViewBag, true);
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__3.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__3.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "show", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj5 = AccountController.\u003C\u003Eo__3.\u003C\u003Ep__4.Target((CallSite) AccountController.\u003C\u003Eo__3.\u003C\u003Ep__4, this.ViewBag, false);
    }
    return (ActionResult) this.View(nameof (TwoFactorAuth), (object) model);
  }

  public ActionResult ChangeTwoFactorMethod(string returnUrl)
  {
    Customer model = (Customer) this.Session["TempCustomer"];
    model.TFAMethod = 0;
    if (!string.IsNullOrWhiteSpace(AccountTheme.Find(MonnitSession.CurrentTheme.AccountID).SMTPFriendlyName))
    {
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "smtp", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AccountController.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__4.\u003C\u003Ep__0, this.ViewBag, true);
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__4.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__4.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "smtp", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AccountController.\u003C\u003Eo__4.\u003C\u003Ep__1.Target((CallSite) AccountController.\u003C\u003Eo__4.\u003C\u003Ep__1, this.ViewBag, false);
    }
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__4.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__4.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (returnUrl), typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = AccountController.\u003C\u003Eo__4.\u003C\u003Ep__2.Target((CallSite) AccountController.\u003C\u003Eo__4.\u003C\u003Ep__2, this.ViewBag, returnUrl);
    return (ActionResult) this.View("TwoFactorAuth", (object) model);
  }

  public ActionResult TOTPSetup(Customer customer, string returnUrl)
  {
    Customer customer1 = MonnitSession.CurrentCustomer != null ? customer : (Customer) this.Session["TempCustomer"];
    byte[] bytes = Encoding.ASCII.GetBytes(MonnitUtil.CreateKeyValue(15));
    string code = OtpNet.Base32Encoding.ToString(bytes);
    this.Session["TwoFactorAuthSecret"] = (object) code;
    Totp totp = new Totp(bytes);
    AccountControllerBase.SetTwoFactorAuthCodeCookie(code, customer1, System.Web.HttpContext.Current);
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "secretCode", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = AccountController.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__5.\u003C\u003Ep__0, this.ViewBag, code);
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__5.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__5.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "qr", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = AccountController.\u003C\u003Eo__5.\u003C\u003Ep__1.Target((CallSite) AccountController.\u003C\u003Eo__5.\u003C\u003Ep__1, this.ViewBag, $"otpauth://totp/{MonnitSession.CurrentTheme.Domain}?secret={code}");
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__5.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__5.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (returnUrl), typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = AccountController.\u003C\u003Eo__5.\u003C\u003Ep__2.Target((CallSite) AccountController.\u003C\u003Eo__5.\u003C\u003Ep__2, this.ViewBag, returnUrl);
    return (ActionResult) this.View(nameof (TOTPSetup));
  }

  private void SetTwoFactorMethodCookie(string method)
  {
    HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["TwoFactorAuthMethod"] ?? new HttpCookie("TwoFactorAuthMethod");
    cookie.Value = method;
    cookie.Expires = DateTime.Now.AddYears(1);
    cookie.HttpOnly = true;
    this.Response.Cookies.Add(cookie);
  }

  public void LogOnAttempts(IPBlacklist ip)
  {
  }

  public ActionResult TwoFactorAuthResponse(string type)
  {
    Customer customer = (Customer) this.Session["TempCustomer"];
    if (customer == null)
      return (ActionResult) this.Content("Error");
    Account account = Account.Load(customer.AccountID);
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__8.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__8.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (type), typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = AccountController.\u003C\u003Eo__8.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__8.\u003C\u003Ep__0, this.ViewBag, type);
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__8.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__8.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Customer, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "customer", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = AccountController.\u003C\u003Eo__8.\u003C\u003Ep__1.Target((CallSite) AccountController.\u003C\u003Eo__8.\u003C\u003Ep__1, this.ViewBag, customer);
    int num = Credit.LoadRemainingCreditsForAccount(account.AccountID, eCreditClassification.Notification);
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__8.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__8.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SMSError", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = AccountController.\u003C\u003Eo__8.\u003C\u003Ep__2.Target((CallSite) AccountController.\u003C\u003Eo__8.\u003C\u003Ep__2, this.ViewBag, (object) null);
    if (type == "1" || type == "SMS")
    {
      if (customer.NotificationPhone.Equals(""))
      {
        string str = ", please verify with email.";
        type = "2";
        // ISSUE: reference to a compiler-generated field
        if (AccountController.\u003C\u003Eo__8.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountController.\u003C\u003Eo__8.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SMSError", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj4 = AccountController.\u003C\u003Eo__8.\u003C\u003Ep__3.Target((CallSite) AccountController.\u003C\u003Eo__8.\u003C\u003Ep__3, this.ViewBag, "SMS number has been previously removed" + str);
        customer.TFAMethod = 2;
        customer.Save();
      }
      else if (customer.DirectSMS && num < 1)
      {
        string str = ", please verify with email.";
        type = "2";
        // ISSUE: reference to a compiler-generated field
        if (AccountController.\u003C\u003Eo__8.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountController.\u003C\u003Eo__8.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SMSError", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj5 = AccountController.\u003C\u003Eo__8.\u003C\u003Ep__4.Target((CallSite) AccountController.\u003C\u003Eo__8.\u003C\u003Ep__4, this.ViewBag, "Not enough credits to send SMS" + str);
        customer.TFAMethod = 2;
        customer.Save();
      }
    }
    if (type != "TOTP" && type != "0" && type != "3")
    {
      string code = "" + new Random().Next(100000, 999999).ToString("D5");
      System.Web.HttpContext.Current.Session["TwoFactorAuthSession"] = (object) code;
      object obj6 = System.Web.HttpContext.Current.Session["TwoFactorAuthSession"];
      if (type == "SMS" || type == "1")
      {
        string notificationPhone = customer.NotificationPhone;
        long ID = customer.SMSCarrierID.ToLong();
        if (!string.IsNullOrEmpty(customer.NotificationPhone))
        {
          try
          {
            Country byIsoCodeOrNumber = Country.FindByISOCodeOrNumber(customer.NotificationPhoneISOCode, notificationPhone);
            string fromNumber = MonnitUtil.GetFromNumber(customer.Account, byIsoCodeOrNumber);
            if (ID > 0L)
            {
              SMSCarrier smsCarrier = SMSCarrier.Load(ID);
              if (smsCarrier != null)
              {
                using (MailMessage mail = new MailMessage())
                {
                  using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, MonnitSession.CurrentTheme))
                  {
                    mail.To.Add(string.Format(smsCarrier.SMSFormatString, (object) notificationPhone.RemoveNonNumeric()));
                    mail.Subject = "";
                    mail.Body = "Your Portal authentication code is " + code;
                    mail.IsBodyHtml = false;
                    this.Session["TFAMethod"] = (object) 1;
                    if (MonnitUtil.SendMail(mail, smtpClient))
                      return type.ToInt() > 0 ? (ActionResult) this.View("TwoFactorAuthSubmit", (object) customer) : (ActionResult) this.Content("Success");
                  }
                }
              }
              return (ActionResult) this.Content("Unable to send test");
            }
            if (string.IsNullOrEmpty(fromNumber))
              return (ActionResult) this.Content("Unrecognized format");
            if (!NotificationCredit.Charge(MonnitSession.CurrentCustomer.AccountID, 1))
              return (ActionResult) this.Content("Insufficient Credits");
            string ToNumber = notificationPhone.Replace(" ", "").Replace("-", "");
            string body = "Your Portal authentication code is " + code;
            MonnitUtil.SendTwilioSMS(fromNumber, ToNumber, body);
            return (ActionResult) this.Content("Success");
          }
          catch (Exception ex)
          {
            ex.Log("CustomerController.TestSMS ");
            return (ActionResult) this.Content(ex.Message);
          }
        }
      }
      if (type == "email" || type == "optIn" || type == "2")
      {
        if (customer.NotificationOptOut > DateTime.MinValue)
        {
          customer.NotificationOptOut = DateTime.MinValue;
          customer.Save();
        }
        if (UnsubscribedNotificationEmail.EmailIsUnsubscribed(customer.NotificationEmail))
          UnsubscribedNotificationEmail.OptIn(customer.NotificationEmail);
        this.SendTFAEmailCode(account, customer, code);
        if (type.ToInt() > 0)
          return (ActionResult) this.View("TwoFactorAuthSubmit", (object) customer);
      }
      return (ActionResult) this.Content("Success");
    }
    if (!(type == "TOTP") && !(type == "3"))
      return (ActionResult) this.View();
    this.Session["TFAMethod"] = (object) 3;
    System.Collections.Generic.List<AuthenticateDevice> authenticateDeviceList = AuthenticateDevice.LoadByCustomerID(customer.CustomerID);
    string clientMachineName = "Unknown";
    try
    {
      clientMachineName = Dns.GetHostEntry(this.Request.ServerVariables["remote_addr"]).HostName;
    }
    catch (Exception ex)
    {
    }
    AccountControllerBase.GetTwoFactorAuthCodeCookie(customer, System.Web.HttpContext.Current);
    bool flag = false;
    if (authenticateDeviceList.Count > 0)
      flag = authenticateDeviceList.Find((Predicate<AuthenticateDevice>) (e => e.DeviceName == clientMachineName)) != null;
    return type.ToInt() == 3 ? (ActionResult) this.View("TwoFactorAuthSubmit", (object) customer) : (ActionResult) this.Content(flag.ToString());
  }

  public ActionResult VerifyTwoFactorAuthCode(
    string code,
    string returnUrl,
    bool? totp,
    bool? sms)
  {
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__9.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__9.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (returnUrl), typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = AccountController.\u003C\u003Eo__9.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__9.\u003C\u003Ep__0, this.ViewBag, returnUrl);
    string str = (string) null;
    int minValue = int.MinValue;
    Customer customer;
    try
    {
      customer = (Customer) this.Session["TempCustomer"];
      str = System.Web.HttpContext.Current.Session["TwoFactorAuthSession"].ToStringSafe();
      minValue = this.Session["TFAMethod"].ToInt();
    }
    catch
    {
      customer = (Customer) null;
    }
    if (customer == null)
    {
      FormsAuthentication.SignOut();
      this.Session.Clear();
      this.Session.Abandon();
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    }
    if (totp.GetValueOrDefault())
    {
      customer.TFAMethod = 3;
      if (!this.VerifyTotpLogin(code, customer))
      {
        // ISSUE: reference to a compiler-generated field
        if (AccountController.\u003C\u003Eo__9.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountController.\u003C\u003Eo__9.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "error", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = AccountController.\u003C\u003Eo__9.\u003C\u003Ep__1.Target((CallSite) AccountController.\u003C\u003Eo__9.\u003C\u003Ep__1, this.ViewBag, "Code Expired");
        return (ActionResult) this.View("TwoFactorAuthSubmit", (object) customer);
      }
    }
    else
    {
      customer.TFAMethod = minValue;
      if (code != str)
      {
        if (!string.IsNullOrWhiteSpace(AccountTheme.Find(MonnitSession.CurrentTheme.AccountID).SMTPFriendlyName))
        {
          // ISSUE: reference to a compiler-generated field
          if (AccountController.\u003C\u003Eo__9.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            AccountController.\u003C\u003Eo__9.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "smtp", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj3 = AccountController.\u003C\u003Eo__9.\u003C\u003Ep__2.Target((CallSite) AccountController.\u003C\u003Eo__9.\u003C\u003Ep__2, this.ViewBag, true);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if (AccountController.\u003C\u003Eo__9.\u003C\u003Ep__3 == null)
          {
            // ISSUE: reference to a compiler-generated field
            AccountController.\u003C\u003Eo__9.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "smtp", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj4 = AccountController.\u003C\u003Eo__9.\u003C\u003Ep__3.Target((CallSite) AccountController.\u003C\u003Eo__9.\u003C\u003Ep__3, this.ViewBag, false);
        }
        // ISSUE: reference to a compiler-generated field
        if (AccountController.\u003C\u003Eo__9.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountController.\u003C\u003Eo__9.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "type", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj5 = AccountController.\u003C\u003Eo__9.\u003C\u003Ep__4.Target((CallSite) AccountController.\u003C\u003Eo__9.\u003C\u003Ep__4, this.ViewBag, sms.ToBool() || customer.TFAMethod == 1 ? nameof (sms) : "email");
        // ISSUE: reference to a compiler-generated field
        if (AccountController.\u003C\u003Eo__9.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountController.\u003C\u003Eo__9.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SMSError", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj6 = AccountController.\u003C\u003Eo__9.\u003C\u003Ep__5.Target((CallSite) AccountController.\u003C\u003Eo__9.\u003C\u003Ep__5, this.ViewBag, (object) null);
        return (ActionResult) this.View("TwoFactorAuth", (object) customer);
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__9.\u003C\u003Ep__6 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__9.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "error", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj7 = AccountController.\u003C\u003Eo__9.\u003C\u003Ep__6.Target((CallSite) AccountController.\u003C\u003Eo__9.\u003C\u003Ep__6, this.ViewBag, "");
    customer.Save();
    string keyValue = MonnitUtil.CreateKeyValue(20);
    string userPlatform = this.GetUserPlatform(this.Request);
    AuthenticateDevice authenticateDevice = new AuthenticateDevice()
    {
      CustomerID = customer.CustomerID,
      DeviceName = $"{userPlatform} [Agent: {this.Request.UserAgent}]",
      DisplayName = userPlatform,
      Salt = MonnitUtil.GenerateSalt(),
      WorkFactor = 1,
      CreateDate = DateTime.UtcNow,
      LastLoginDate = DateTime.UtcNow
    };
    authenticateDevice.DeviceHash = MonnitUtil.GenerateHash(keyValue, authenticateDevice.Salt, authenticateDevice.WorkFactor);
    authenticateDevice.Save();
    AccountControllerBase.SetTwoFactorAuthCodeCookie(keyValue, customer, System.Web.HttpContext.Current);
    return this.FinalizeLogin(returnUrl);
  }

  private string GetUserPlatform(HttpRequestBase request)
  {
    try
    {
      string userAgent = request.UserAgent;
      if (userAgent.Contains("Android"))
        return "Android";
      if (userAgent.Contains("iPad"))
        return "iPad";
      if (userAgent.Contains("iPhone"))
        return "iPhone";
      if (userAgent.Contains("Linux") && userAgent.Contains("KFAPWI"))
        return "Kindle Fire";
      if (userAgent.Contains("RIM Tablet") || userAgent.Contains("BB") && userAgent.Contains("Mobile"))
        return "Black Berry";
      if (userAgent.Contains("Windows Phone"))
        return "Windows Phone";
      if (userAgent.Contains("Mac OS"))
        return "Mac OS";
      return userAgent.Contains("Windows") ? "Windows" : request.Browser.Platform + (userAgent.Contains("Mobile") ? " Mobile" : "");
    }
    catch
    {
      return "Unknown";
    }
  }

  public bool VerifyTotpLogin(string code, Customer customer)
  {
    return !string.IsNullOrEmpty(customer.TOTPSecret) && new Totp(OtpNet.Base32Encoding.ToBytes(customer.TOTPSecret)).VerifyTotp(code, out long _, VerificationWindow.RfcSpecifiedNetworkDelay);
  }

  private IPBlacklist ValidateIPBlacklist()
  {
    IPBlacklist ipBlacklist1 = (IPBlacklist) null;
    foreach (IPBlacklist ipBlacklist2 in IPBlacklist.LoadByIP(this.Request.ClientIP()))
    {
      DateTime dateTime1 = ipBlacklist2.FirstFailedAttempt;
      dateTime1 = dateTime1.AddMinutes((double) ConfigData.AppSettings("IPBlacklistTime", "5").ToInt());
      double totalSeconds = dateTime1.Subtract(DateTime.UtcNow).TotalSeconds;
      if (totalSeconds > 0.0)
      {
        if (ipBlacklist2.FailedAttempts > ConfigData.AppSettings("IPBlacklistCount", "5").ToInt())
        {
          this.ModelState.AddModelError("", $"You've exceeded the login limit, please wait {Math.Ceiling(totalSeconds / 60.0).ToString()} minute(s) before trying to log in again.");
          ++ipBlacklist2.FailedAttempts;
          if (ipBlacklist2.FailedAttempts > ConfigData.AppSettings("IPBlacklistCount", "5").ToInt() * 3)
          {
            IPBlacklist ipBlacklist3 = ipBlacklist2;
            dateTime1 = DateTime.UtcNow;
            DateTime dateTime2 = dateTime1.AddMinutes(60.0);
            ipBlacklist3.FirstFailedAttempt = dateTime2;
          }
          ipBlacklist2.Save();
        }
      }
      else if (totalSeconds < 0.0)
        ipBlacklist2.Delete();
      else if (ipBlacklist1 == null || ipBlacklist1.FailedAttempts < ipBlacklist2.FailedAttempts)
        ipBlacklist1 = ipBlacklist2;
    }
    if (ipBlacklist1 == null)
    {
      ipBlacklist1 = new IPBlacklist();
      ipBlacklist1.FirstFailedAttempt = DateTime.UtcNow;
      ipBlacklist1.IPAddress = this.Request.ClientIP();
    }
    return ipBlacklist1;
  }

  private ActionResult InternalLogOnOV(LogOnModel model, string viewName)
  {
    if (!this.ModelState.IsValid)
      return (ActionResult) this.View((object) model);
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__13.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__13.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__2.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p2 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__2;
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__13.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__13.\u003C\u003Ep__1 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IsNullOrWhiteSpace", (IEnumerable<Type>) null, typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, Type, object, object> target2 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, Type, object, object>> p1 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__1;
    Type type = typeof (string);
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__13.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__13.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "returnUrl", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__13.\u003C\u003Ep__0, this.ViewBag);
    object obj2 = target2((CallSite) p1, type, obj1);
    if (target1((CallSite) p2, obj2))
    {
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__13.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__13.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnUrl", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__3.Target((CallSite) AccountController.\u003C\u003Eo__13.\u003C\u003Ep__3, this.ViewBag, "/Overview");
    }
    IPBlacklist ipBlacklist = IPBlacklist.LoadByIP(this.Request.ClientIP()).FirstOrDefault<IPBlacklist>();
    Customer customer1 = Customer.Authenticate(model.UserName, model.Password, "Web Login", this.Request.ClientIP(), MonnitSession.UseEncryption);
    if (this.ModelState.IsValid && ipBlacklist != null && !ipBlacklist.BlacklistAttempt())
      this.ModelState.AddModelError("", $"You've exceeded the login limit, please wait {ipBlacklist?.ToString()} before trying to log in again.");
    if (this.ModelState.IsValid)
    {
      if (customer1 == null)
      {
        this.ModelState.AddModelError("", "The user name or password provided is incorrect.");
        customer1 = Customer.LoadFromUsername(model.UserName);
        if (customer1 != null)
        {
          ++customer1.FailedLoginCount;
          customer1.Save();
        }
      }
      else if (customer1.isLocked())
      {
        // ISSUE: reference to a compiler-generated field
        if (AccountController.\u003C\u003Eo__13.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountController.\u003C\u003Eo__13.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "CustomerID", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj4 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__4.Target((CallSite) AccountController.\u003C\u003Eo__13.\u003C\u003Ep__4, this.ViewBag, customer1.CustomerID);
        this.ModelState.AddModelError("", "User locked for excessive login attempts.");
        this.SendLockedMail(customer1);
        customer1.Save();
      }
    }
    if (this.ModelState.IsValid && !customer1.CanViewTheme(MonnitSession.CurrentTheme))
      this.ModelState.AddModelError("", "User does not have access to domain.");
    if (this.ModelState.IsValid && !customer1.IsActive)
      this.ModelState.AddModelError("Inactive", "This user is inactive.  Multiple users only available with premium subscription.");
    if (this.ModelState.IsValid)
    {
      ipBlacklist?.Delete();
      this.SetUserNameCookie(customer1);
      this.Session["RememberMe"] = (object) model.RememberMe.ToString();
      HttpCookie factorAuthCodeCookie = AccountControllerBase.GetTwoFactorAuthCodeCookie(customer1, System.Web.HttpContext.Current);
      if (this.Requires2FA(customer1, factorAuthCodeCookie))
      {
        this.Session["TempCustomer"] = (object) customer1;
        if (MonnitSession.CurrentTheme.SMTP != "http://smtp.yourhostname.com/" && factorAuthCodeCookie != null && customer1.TOTPSecret == null)
        {
          // ISSUE: reference to a compiler-generated field
          if (AccountController.\u003C\u003Eo__13.\u003C\u003Ep__7 == null)
          {
            // ISSUE: reference to a compiler-generated field
            AccountController.\u003C\u003Eo__13.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, ActionResult>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (ActionResult), typeof (AccountController)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, ActionResult> target3 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__7.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, ActionResult>> p7 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__7;
          // ISSUE: reference to a compiler-generated field
          if (AccountController.\u003C\u003Eo__13.\u003C\u003Ep__6 == null)
          {
            // ISSUE: reference to a compiler-generated field
            AccountController.\u003C\u003Eo__13.\u003C\u003Ep__6 = CallSite<Func<CallSite, AccountController, Customer, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "TOTPSetup", (IEnumerable<Type>) null, typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, AccountController, Customer, object, object> target4 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__6.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, AccountController, Customer, object, object>> p6 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__6;
          Customer customer2 = customer1;
          // ISSUE: reference to a compiler-generated field
          if (AccountController.\u003C\u003Eo__13.\u003C\u003Ep__5 == null)
          {
            // ISSUE: reference to a compiler-generated field
            AccountController.\u003C\u003Eo__13.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "returnUrl", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj5 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__5.Target((CallSite) AccountController.\u003C\u003Eo__13.\u003C\u003Ep__5, this.ViewBag);
          object obj6 = target4((CallSite) p6, this, customer2, obj5);
          return target3((CallSite) p7, obj6);
        }
        // ISSUE: reference to a compiler-generated field
        if (AccountController.\u003C\u003Eo__13.\u003C\u003Ep__10 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountController.\u003C\u003Eo__13.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, ActionResult>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (ActionResult), typeof (AccountController)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, ActionResult> target5 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__10.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, ActionResult>> p10 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__10;
        // ISSUE: reference to a compiler-generated field
        if (AccountController.\u003C\u003Eo__13.\u003C\u003Ep__9 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountController.\u003C\u003Eo__13.\u003C\u003Ep__9 = CallSite<Func<CallSite, AccountController, Customer, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "TwoFactorAuth", (IEnumerable<Type>) null, typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, AccountController, Customer, object, object> target6 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__9.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, AccountController, Customer, object, object>> p9 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__9;
        Customer customer3 = customer1;
        // ISSUE: reference to a compiler-generated field
        if (AccountController.\u003C\u003Eo__13.\u003C\u003Ep__8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountController.\u003C\u003Eo__13.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "returnUrl", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj7 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__8.Target((CallSite) AccountController.\u003C\u003Eo__13.\u003C\u003Ep__8, this.ViewBag);
        object obj8 = target6((CallSite) p9, this, customer3, obj7);
        return target5((CallSite) p10, obj8);
      }
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__13.\u003C\u003Ep__13 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__13.\u003C\u003Ep__13 = CallSite<Func<CallSite, object, ActionResult>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (ActionResult), typeof (AccountController)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, ActionResult> target7 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__13.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, ActionResult>> p13 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__13;
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__13.\u003C\u003Ep__12 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__13.\u003C\u003Ep__12 = CallSite<Func<CallSite, AccountController, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "FinalizeLogin", (IEnumerable<Type>) null, typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, AccountController, object, object> target8 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__12.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, AccountController, object, object>> p12 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__12;
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__13.\u003C\u003Ep__11 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__13.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "returnUrl", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj9 = AccountController.\u003C\u003Eo__13.\u003C\u003Ep__11.Target((CallSite) AccountController.\u003C\u003Eo__13.\u003C\u003Ep__11, this.ViewBag);
      object obj10 = target8((CallSite) p12, this, obj9);
      return target7((CallSite) p13, obj10);
    }
    if (ipBlacklist == null)
    {
      ipBlacklist = new IPBlacklist();
      ipBlacklist.FirstFailedAttempt = DateTime.UtcNow;
      ++ipBlacklist.FailedAttempts;
      ipBlacklist.IPAddress = this.Request.ClientIP();
    }
    ipBlacklist.Save();
    Customer customer4 = Customer.LoadFromUsername(model.UserName);
    if (customer4 != null)
    {
      ++customer4.FailedLoginCount;
      customer4.Save();
    }
    return (ActionResult) this.View(viewName, (object) model);
  }

  public bool Requires2FA(Customer CurrentCust, HttpCookie cookie)
  {
    if (MonnitSession.CurrentTheme.IsTFAEnabled)
    {
      if (CurrentCust.ByPass2FA)
      {
        // ISSUE: reference to a compiler-generated field
        if (AccountController.\u003C\u003Eo__14.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AccountController.\u003C\u003Eo__14.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnUrl", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = AccountController.\u003C\u003Eo__14.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__14.\u003C\u003Ep__0, this.ViewBag, "/Settings/UserTwoFactorAuth/");
        CurrentCust.ByPass2FA = false;
        CurrentCust.Save();
      }
      else if (!CurrentCust.ByPass2FAPermanent && !CurrentCust.OutdatedTwoFactorAuth(cookie, CurrentCust.CustomerID))
        return true;
    }
    return false;
  }

  private void UpdateRememberedDeviceLoginDate()
  {
    Customer currentCustomer = MonnitSession.CurrentCustomer;
    HttpCookie factorAuthCodeCookie = AccountControllerBase.GetTwoFactorAuthCodeCookie(currentCustomer, System.Web.HttpContext.Current);
    if (factorAuthCodeCookie == null)
      return;
    string str = factorAuthCodeCookie.Value;
    foreach (AuthenticateDevice authenticateDevice in AuthenticateDevice.LoadByCustomerID(currentCustomer.CustomerID))
    {
      if (StructuralComparisons.StructuralEqualityComparer.Equals((object) MonnitUtil.GenerateHash(str.ToString(), authenticateDevice.Salt, authenticateDevice.WorkFactor), (object) authenticateDevice.DeviceHash))
      {
        authenticateDevice.LastLoginDate = DateTime.UtcNow;
        authenticateDevice.Save();
      }
    }
  }

  public ActionResult FinalizeLogin(string returnUrl, bool isSAML = false)
  {
    Customer CurrentCust = Customer.LoadFromUsername(System.Web.HttpContext.Current.Request.Cookies["UserName"].Value);
    MonnitSession.CustomerIDLoggedInAsProxy = long.MinValue;
    MonnitSession.CurrentCustomer = CurrentCust;
    if (MonnitSession.CurrentTheme.CurrentEULAVersion != Version.Parse("0.0.0.0") && (CurrentCust.Account.EULAVersion != MonnitSession.CurrentTheme.CurrentEULA || CurrentCust.Account.EULADate == DateTime.MinValue))
      return (ActionResult) this.View("/Views/Overview/UpdateEula.aspx", (object) new UpdateEulas());
    FormsAuthentication.SetAuthCookie(CurrentCust.UserName, false);
    this.SetRememberMeCookie(CurrentCust, this.Session["RememberMe"].ToBool());
    this.UpdateRememberedDeviceLoginDate();
    CurrentCust.LastLoginDate = DateTime.UtcNow;
    CurrentCust.FailedLoginCount = 0;
    CurrentCust.Save();
    if (CurrentCust.PasswordIsExpired() && !isSAML)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "ChangePassword",
        controller = "Overview"
      });
    if (!string.IsNullOrEmpty(CurrentCust.HomepageLink) && (returnUrl == "/Overview" || returnUrl == "/Overview/"))
      returnUrl = CurrentCust.HomepageLink;
    if (!string.IsNullOrEmpty(returnUrl) && (returnUrl == "/" || returnUrl.ToLower().IndexOf("/account/logonOV") > -1 || returnUrl.ToLower().IndexOf("/account/logoff") > -1 || !this.IsLocalUrl(returnUrl)))
      returnUrl = "/Overview";
    if (this.Session["TOTPSetup"] != null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "UserTwoFactorAuth",
        Controller = "Settings",
        id = CurrentCust.CustomerID
      });
    if (!string.IsNullOrEmpty(returnUrl) && returnUrl != "/Overview")
      return (ActionResult) this.Redirect(returnUrl);
    HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["portaltype"];
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    });
  }

  public ActionResult PasswordReset(string id, string VerificationCode)
  {
    AccountVerification accountVerification = AccountVerification.LoadByCode((id ?? VerificationCode) ?? "");
    if (accountVerification != null)
    {
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__17.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__17.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Email", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = AccountController.\u003C\u003Eo__17.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__17.\u003C\u003Ep__0, this.ViewBag, accountVerification.EmailAddress);
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__17.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__17.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Username", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = AccountController.\u003C\u003Eo__17.\u003C\u003Ep__1.Target((CallSite) AccountController.\u003C\u003Eo__17.\u003C\u003Ep__1, this.ViewBag, accountVerification.Name);
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__17.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__17.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Verification", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = AccountController.\u003C\u003Eo__17.\u003C\u003Ep__2.Target((CallSite) AccountController.\u003C\u003Eo__17.\u003C\u003Ep__2, this.ViewBag, accountVerification.VerificationCode);
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__17.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__17.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Email", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = AccountController.\u003C\u003Eo__17.\u003C\u003Ep__3.Target((CallSite) AccountController.\u003C\u003Eo__17.\u003C\u003Ep__3, this.ViewBag, "Unknown");
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__17.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__17.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Username", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj5 = AccountController.\u003C\u003Eo__17.\u003C\u003Ep__4.Target((CallSite) AccountController.\u003C\u003Eo__17.\u003C\u003Ep__4, this.ViewBag, "Unknown");
      this.ModelState.AddModelError(nameof (VerificationCode), "Invalid Recovery Code");
    }
    this.ViewData["PasswordLength"] = (object) MonnitUtil.MinPasswordLength;
    return (ActionResult) this.View();
  }

  [HttpPost]
  public ActionResult PasswordReset(ForgotCredentialModel model)
  {
    try
    {
      AntiForgery.Validate();
    }
    catch
    {
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "LogOnOV",
        controller = "Account"
      });
    }
    if (!MonnitUtil.IsValidPassword(model.NewPassword, MonnitSession.CurrentTheme))
      this.ModelState.AddModelError("Password", MonnitUtil.PasswordHelperString(MonnitSession.CurrentTheme));
    Customer customer = Customer.LoadAllByEmail(model.EmailAddress).Find((Predicate<Customer>) (e => e.NotificationEmail.ToLower() == model.EmailAddress.ToLower()));
    AccountVerification accountVerification = AccountVerification.LoadByEmailAddress(model.EmailAddress);
    if (customer == null || accountVerification == null)
      this.ModelState.AddModelError("VerificationCode", "Invalid Recovery Code");
    if (this.ModelState.IsValid)
    {
      customer.PasswordExpired = false;
      customer.PasswordChangeDate = DateTime.UtcNow;
      customer.Salt = MonnitUtil.GenerateSalt();
      customer.WorkFactor = ConfigData.AppSettings("WorkFactor").ToInt();
      customer.Password2 = MonnitUtil.GenerateHash(model.ConfirmPassword, customer.Salt, customer.WorkFactor);
      customer.ForceLogoutDate = DateTime.UtcNow;
      customer.Save();
      accountVerification.Delete();
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "LogOnOV",
        controller = "Account"
      });
    }
    this.ViewData["PasswordLength"] = (object) MonnitUtil.MinPasswordLength;
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__18.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__18.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserName", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = AccountController.\u003C\u003Eo__18.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__18.\u003C\u003Ep__0, this.ViewBag, model.Username);
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__18.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__18.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Email", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = AccountController.\u003C\u003Eo__18.\u003C\u003Ep__1.Target((CallSite) AccountController.\u003C\u003Eo__18.\u003C\u003Ep__1, this.ViewBag, model.EmailAddress);
    return (ActionResult) this.View(nameof (PasswordReset));
  }

  public ActionResult AccountRecovery(long id)
  {
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__19.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__19.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "errMessage", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = AccountController.\u003C\u003Eo__19.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__19.\u003C\u003Ep__0, this.ViewBag, "");
    Customer cust = Customer.Load(id);
    Account account = Account.Load(cust.AccountID);
    int num = new Random().Next(100000, 999999);
    account.VerificationCode = num.ToString();
    account.VerificationDate = DateTime.UtcNow.AddMinutes(180.0);
    account.Save();
    if (string.IsNullOrEmpty(account.RecoveryEmail))
    {
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__19.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__19.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "errMessage", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = AccountController.\u003C\u003Eo__19.\u003C\u003Ep__1.Target((CallSite) AccountController.\u003C\u003Eo__19.\u003C\u003Ep__1, this.ViewBag, "No account recovery email set. Email sent to primary contact only.");
    }
    this.SendRecoveryEmail(account, cust);
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__19.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__19.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, Customer, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Customer", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = AccountController.\u003C\u003Eo__19.\u003C\u003Ep__2.Target((CallSite) AccountController.\u003C\u003Eo__19.\u003C\u003Ep__2, this.ViewBag, cust);
    return (ActionResult) this.View((object) account);
  }

  [HttpPost]
  public ActionResult AccountRecovery(string code, long customerID)
  {
    ActionResult actionResult1 = (ActionResult) this.Content("");
    Customer model = Customer.Load(customerID);
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__20.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__20.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "errMessage", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AccountController.\u003C\u003Eo__20.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__20.\u003C\u003Ep__0, this.ViewBag, "");
    ActionResult actionResult2;
    try
    {
      Account account = Account.LoadByVerificationCode(code.ToLong());
      if (account != null)
      {
        if (account.VerificationDate > DateTime.UtcNow)
          return (ActionResult) this.View("_RecoveryForm", (object) model);
        actionResult2 = (ActionResult) this.Content("Error: Verification Code has expired.");
      }
      else
        actionResult2 = (ActionResult) this.Content("Error: Invalid Verification Code");
    }
    catch (Exception ex)
    {
      ex.Log("AccountController.AccountRecovery.VerificationCodeFail");
      actionResult2 = (ActionResult) this.Content("Error: " + ex?.ToString());
    }
    return actionResult2;
  }

  public void SendRecoveryEmail(Account account, Customer cust)
  {
    string str = $"\r\nThis email was sent to recover {account.CompanyName} account. \r\nEnter the code below to the Location Account page to verify this account:\r\n<br/><br/>\r\n<font size='6'>{account.VerificationCode}</font> \r\n<br/><br/>\r\nThis code will expire after 3 hours after this email was sent.\r\n<br/>\r\nIf you did not request to recover this account, please disregard this message.\r\n<br/>\r\n";
    using (MailMessage mail = new MailMessage())
    {
      using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, account))
      {
        mail.Subject = "iMonnit Account Recovery";
        mail.Body = str;
        mail.IsBodyHtml = true;
        try
        {
          mail.To.Clear();
          mail.To.Add(new MailAddress(account.PrimaryContact.NotificationEmail, account.PrimaryContact.FullName));
          if (!string.IsNullOrEmpty(account.RecoveryEmail))
            mail.To.Add(new MailAddress(account.RecoveryEmail));
          MonnitUtil.SendMail(mail, smtpClient);
        }
        catch (Exception ex)
        {
          ex.Log("AccountController.SendRecoveryEmail");
        }
      }
    }
  }

  public ActionResult RecoveryForm(long id)
  {
    Customer customer = Customer.Load(id);
    try
    {
      Account account = Account.Load(customer.AccountID);
      if (account.PrimaryContact.CustomerID != customer.CustomerID)
      {
        if (!account.IsPremium)
        {
          account.PrimaryContact.IsActive = false;
          account.PrimaryContact.ForceLogoutDate = DateTime.UtcNow;
          account.PrimaryContact.Save();
          account.PrimaryContact = customer;
          account.PrimaryContact.IsActive = true;
          account.PrimaryContactID = customer.CustomerID;
          account.Save();
        }
        account.PrimaryContact.Save();
      }
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "LogOnOV",
        controller = "Account"
      });
    }
    catch (Exception ex)
    {
      ex.Log("AccountController.RecoveryForm.FailedToSwitchPrimaryContact");
      this.ViewData["Results"] = (object) ("Error: " + ex?.ToString());
    }
    return (ActionResult) this.View("_RecoveryForm");
  }

  public ActionResult LogOnForgot(string id, string VerificationCode)
  {
    string VerificationCode1 = id ?? VerificationCode;
    if (VerificationCode1 == null)
      return (ActionResult) this.View();
    AccountVerification accountVerification = AccountVerification.LoadByCode(VerificationCode1);
    if (accountVerification != null && DateTime.UtcNow - accountVerification.CreateDate > new TimeSpan(0, ConfigData.AppSettings("CredSessionLength", "20").ToInt(), 0))
    {
      accountVerification.Delete();
      accountVerification = (AccountVerification) null;
    }
    if (accountVerification == null)
      this.ModelState.AddModelError(nameof (VerificationCode), "Invalid Recovery Code");
    if (this.ModelState.IsValid)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "PasswordReset",
        controller = "Account",
        VerificationCode = accountVerification.VerificationCode
      });
    return (ActionResult) this.View(nameof (LogOnForgot), (object) new LogOnForgotModel()
    {
      VerificationCode = VerificationCode,
      SendCodePage = true
    });
  }

  public ActionResult CredentialLookup(LogOnForgotModel model)
  {
    if (this.Session["LookupTryCount"].ToInt() > 5)
      this.ModelState.AddModelError("EmailAddress", "Too many code requests");
    if (!this.ModelState.IsValid)
      return (ActionResult) this.Content(string.Join("\n", this.ModelState.SelectMany<KeyValuePair<string, System.Web.Mvc.ModelState>, ModelError>((Func<KeyValuePair<string, System.Web.Mvc.ModelState>, IEnumerable<ModelError>>) (x => (IEnumerable<ModelError>) x.Value.Errors)).Select<ModelError, string>((Func<ModelError, string>) (x => x.ErrorMessage))));
    if (this.Session["LookupTryCount"] == null)
      this.Session["LookupTryCount"] = (object) 0;
    this.Session["LookupTryCount"] = (object) (this.Session["LookupTryCount"].ToInt() + 1);
    AccountVerification accountVerification1 = AccountVerification.LoadByEmailAddress(model.EmailAddress);
    if (accountVerification1 != null && DateTime.UtcNow - accountVerification1.CreateDate > new TimeSpan(0, ConfigData.AppSettings("CredSessionLength").ToInt(), 0))
      accountVerification1.Delete();
    AccountVerification accountVerification2 = new AccountVerification();
    accountVerification2.EmailAddress = model.EmailAddress;
    accountVerification2.VerificationCode = AccountControllerBase.CredentialRecoveryAuthKey();
    Customer customer = AccountControllerBase.TrySendPasswordResetAuth(model.EmailAddress, accountVerification2.VerificationCode);
    if (customer != null)
    {
      accountVerification2.CreateDate = DateTime.UtcNow;
      accountVerification2.EmailAddress = model.EmailAddress;
      accountVerification2.Name = customer.UserName;
      accountVerification2.WorkFactor = 0;
      accountVerification2.Save();
    }
    return (ActionResult) this.Content("Success");
  }

  public ActionResult LogOn()
  {
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "LogOnOV",
      controller = "Account"
    });
  }

  [HttpGet]
  public ActionResult Unlock(long customerid, string guid)
  {
    Customer model = Customer.Load(customerid);
    string str = "oneview";
    try
    {
      str = this.Request.Cookies["portaltype"].Value;
    }
    catch
    {
    }
    if (model != null && model.GUID == guid)
    {
      model.unlock();
      model.Save();
    }
    return str == "oneview" ? (ActionResult) this.View("/Views/Account/UnlockAccount.aspx", (object) model) : (ActionResult) this.View((object) model);
  }

  public ActionResult RegisterToAccount(long customerid, string guid)
  {
    if (this.ControllerContext.HttpContext.Request.HttpMethod.ToLower() == "get")
    {
      System.Collections.Generic.List<CustomerAccountLink> customerAccountLinkList = CustomerAccountLink.LoadAllByCustomerID(customerid);
      Customer customer = Customer.Load(customerid);
      if (customerAccountLinkList.Count == 0)
        return (ActionResult) null;
      CustomerAccountLink customerAccountLink1 = new CustomerAccountLink();
      foreach (CustomerAccountLink customerAccountLink2 in customerAccountLinkList)
      {
        if (customerAccountLink2.GUID == guid && customerAccountLink2.AccountID != customer.AccountID)
        {
          customerAccountLink2.RequestConfirmed = true;
          customerAccountLink2.Save();
          customerAccountLink1 = customerAccountLink2;
        }
      }
      return (ActionResult) this.View("RegisterAccountLink");
    }
    System.Collections.Generic.List<CustomerAccountLink> customerAccountLinkList1 = CustomerAccountLink.LoadAllByCustomerID(customerid);
    Customer customer1 = Customer.Load(customerid);
    if (customerAccountLinkList1.Count == 0)
      return (ActionResult) null;
    CustomerAccountLink customerAccountLink3 = new CustomerAccountLink();
    foreach (CustomerAccountLink customerAccountLink4 in customerAccountLinkList1)
    {
      if (customerAccountLink4.GUID == guid && customerAccountLink4.AccountID != customer1.AccountID)
      {
        customerAccountLink4.RequestConfirmed = true;
        customerAccountLink4.Save();
        customerAccountLink3 = customerAccountLink4;
      }
    }
    return (ActionResult) this.Content("Success");
  }

  public ActionResult RegisterAccountLink() => (ActionResult) this.View();

  public ActionResult DeclineAccountLink(long customerid, string guid)
  {
    System.Collections.Generic.List<CustomerAccountLink> customerAccountLinkList = CustomerAccountLink.LoadAllByCustomerID(customerid);
    Customer.Load(customerid);
    if (customerAccountLinkList.Count == 0)
      return (ActionResult) null;
    CustomerAccountLink customerAccountLink1 = new CustomerAccountLink();
    foreach (CustomerAccountLink customerAccountLink2 in customerAccountLinkList)
    {
      if (customerAccountLink2.GUID == guid)
        customerAccountLink2.Delete();
    }
    return (ActionResult) this.Content("Success");
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

  public ActionResult SSO(string authToken, string returnUrl, long? accountID)
  {
    if (!string.IsNullOrEmpty(authToken))
    {
      string[] strArray = this.SSOCredentialsFromAuthToken(authToken);
      if (strArray != null && strArray.Length == 2 && !string.IsNullOrEmpty(strArray[0]) && !string.IsNullOrEmpty(strArray[1]))
        return this.SSO(strArray[0], strArray[1], returnUrl.ToStringSafe(), accountID);
    }
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    });
  }

  [HttpPost]
  public ActionResult SSO(string username, string password, string returnUrl, long? accountID)
  {
    if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
    {
      ActionResult actionResult = this.InternalLogOnOV(new LogOnModel()
      {
        UserName = username,
        Password = password,
        RememberMe = false
      }, returnUrl.ToStringSafe());
      long? nullable = accountID;
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue && this.ModelState.IsValid && (this.ProxySubAccount(accountID.GetValueOrDefault()) as ContentResult).Content == "Success" || this.ModelState.IsValid)
        return actionResult;
    }
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Overview",
      controller = "Account"
    });
  }

  public ActionResult ForgotUsername() => (ActionResult) this.View((object) new RetrieveUserName());

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ForgotUsername(RetrieveUserName model)
  {
    if (this.ModelState.IsValid)
      model.Result = AccountControllerBase.SendUserNameReminder(model.NotificationEmail);
    return (ActionResult) this.View((object) model);
  }

  [NoCache]
  public ActionResult ForgotPassword()
  {
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__35.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__35.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AccountController.\u003C\u003Eo__35.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__35.\u003C\u003Ep__0, this.ViewBag, "");
    return (ActionResult) this.View();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ForgotPassword(string username)
  {
    if (string.IsNullOrWhiteSpace(username))
    {
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__36.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__36.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AccountController.\u003C\u003Eo__36.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__36.\u003C\u003Ep__0, this.ViewBag, "User name is required.");
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__36.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__36.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AccountController.\u003C\u003Eo__36.\u003C\u003Ep__1.Target((CallSite) AccountController.\u003C\u003Eo__36.\u003C\u003Ep__1, this.ViewBag, AccountControllerBase.SendPasswordReminder(username));
    }
    return (ActionResult) this.View();
  }

  public ActionResult LogOff()
  {
    if (MonnitSession.CurrentCustomer != null)
      Customer.SetForceLogoutDate(Customer.Load(MonnitSession.CustomerIDLoggedInAsProxy > 0L ? MonnitSession.CustomerIDLoggedInAsProxy : MonnitSession.CurrentCustomer.CustomerID).CustomerID);
    this.ClearRememberMeCookie();
    FormsAuthentication.SignOut();
    this.Session.Clear();
    this.Session.Abandon();
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    });
  }

  [Authorize]
  [HttpPost]
  public ActionResult UpdateAccountSamlValues(
    long accountID,
    string name,
    string url,
    string certificate)
  {
    Account DBObject = Account.Load(accountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
      return (ActionResult) this.Content("Unauthorized: Invalid account");
    if (string.IsNullOrWhiteSpace(name) && (url.Length > 0 || certificate.Length > 0) || string.IsNullOrWhiteSpace(url) && (name.Length > 0 || certificate.Length > 0) || string.IsNullOrWhiteSpace(certificate) && (name.Length > 0 || url.Length > 0))
      return (ActionResult) this.Content("Failed: All fields must be filled, otherwise leave empty.");
    try
    {
      SamlEndpoint samlEndpoint = (DBObject.SamlEndpointID > 0L ? SamlEndpoint.Load(DBObject.SamlEndpointID) : (SamlEndpoint) null) ?? new SamlEndpoint();
      if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(url) && string.IsNullOrWhiteSpace(certificate) && samlEndpoint.SamlEndpointID > 0L)
      {
        Account.RemoveSamlEndPoint(accountID, samlEndpoint.SamlEndpointID);
        DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Deleted Saml entry");
      }
      else
      {
        samlEndpoint.Name = name;
        samlEndpoint.EndpointURL = url;
        samlEndpoint.Certificate = certificate;
        samlEndpoint.Save();
        DBObject.SamlEndpointID = samlEndpoint.SamlEndpointID;
        DBObject.Save();
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Updated Saml entry");
      }
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log($"AccountController.UpdateAccountSamlValues[Post][AccountID: {accountID.ToString()}][Url: {url}][Certificate: {certificate}] ");
    }
    return (ActionResult) this.Content("Failed");
  }

  [HttpPost]
  public ActionResult PrelogonOV(string username, string returnURL)
  {
    ActionResult actionResult = (ActionResult) this.Content("ShowPassword");
    try
    {
      Customer customer = Customer.LoadFromUsername(username);
      if (customer != null)
      {
        long accountId = customer.AccountID;
        Account account = Account.Load(accountId);
        if (account != null)
        {
          SamlEndpoint samlEndpoint = SamlEndpoint.Load(account.SamlEndpointID);
          if (samlEndpoint != null)
            actionResult = this.SAMLLogin(samlEndpoint.EndpointURL, accountId, returnURL);
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("AccountController.PrelogonOV[Get] ");
    }
    return actionResult;
  }

  private ActionResult SAMLLogin(string endpointUrl, long accountID, string returnURL)
  {
    string content = "ShowPassword";
    try
    {
      if (!string.IsNullOrWhiteSpace(endpointUrl) && accountID > 0L)
        content = new AuthRequest(ConfigData.AppSettings("SamlEntityID").Replace("{Domain}", MonnitSession.CurrentTheme.Domain), ConfigData.AppSettings("SamlConsumeURL").Replace("{Domain}", MonnitSession.CurrentTheme.Domain) + accountID.ToString()).GetRedirectUrl(endpointUrl, returnURL);
    }
    catch (Exception ex)
    {
      ex.Log($"AccountController.SAMLLogin[SamlEndpointURL: {endpointUrl}] ");
    }
    return (ActionResult) this.Content(content);
  }

  [HttpPost]
  public ActionResult SAMLConsume(string id)
  {
    LogOnModel model = new LogOnModel();
    try
    {
      long num = id.ToLong();
      Account account = Account.Load(num);
      if (account != null && account.SamlEndpointID > 0L)
      {
        Saml.Response response = new Saml.Response(SamlEndpoint.Load(account.SamlEndpointID).Certificate, this.Request.Form["SAMLResponse"]);
        if (response.IsValid())
        {
          Customer CurrentCust = Customer.LoadByAccountAndSamlNameID(num, response.GetNameID());
          if (CurrentCust != null)
          {
            bool flag = CurrentCust.isLocked();
            ActionResult actionResult;
            if (CurrentCust.IsActive && !flag)
            {
              this.SetUserNameCookie(CurrentCust);
              actionResult = this.FinalizeLogin(this.Request.Form["RelayState"], true);
            }
            else
            {
              model.UserName = CurrentCust.UserName;
              this.ModelState.AddModelError("UserName", "This user is inactive.  Multiple users only available with premium subscription.");
              actionResult = (ActionResult) this.View("LogonOv", (object) model);
            }
            return actionResult;
          }
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("AccountController.SAMLConsume[Post] ");
    }
    return (ActionResult) this.View("LogonOv", (object) model);
  }

  public static Account InsertAccount(
    CreateAccountAPIModel model,
    bool sendEmail,
    long defaultAccountSubscriptionTypeID)
  {
    Account account1 = new Account();
    account1.AccountNumber = model.AccountNumber;
    account1.CompanyName = model.CompanyName;
    account1.TimeZoneID = model.TimeZoneID.ToLong();
    account1.EULAVersion = MonnitSession.CurrentTheme.CurrentEULA;
    account1.EULADate = DateTime.UtcNow;
    account1.CreateDate = DateTime.UtcNow;
    Account account2 = Account.Load(model.ResellerID.ToLong()) ?? Account.Load(MonnitSession.CurrentTheme.AccountID) ?? Account.Load(ConfigData.AppSettings("AdminAccountID").ToLong());
    account1.RetailAccountID = account2.AccountID;
    account1.Save();
    account1.AccountIDTree = $"{account2.AccountIDTree}{account1.AccountID.ToString()}*";
    account1.Save();
    if (model.IsPremium)
      new AccountSubscription()
      {
        AccountID = account1.AccountID,
        AccountSubscriptionTypeID = (defaultAccountSubscriptionTypeID < 0L ? 5L : defaultAccountSubscriptionTypeID),
        ExpirationDate = DateTime.Today.AddDays((double) MonnitUtil.DefaultPremiumLength(MonnitSession.CurrentTheme))
      }.Save();
    Customer customer = new Customer()
    {
      AccountID = account1.AccountID,
      UserName = model.UserName,
      Salt = MonnitUtil.GenerateSalt(),
      WorkFactor = ConfigData.AppSettings("WorkFactor").ToInt()
    };
    customer.Password2 = MonnitUtil.GenerateHash(model.Password, customer.Salt, customer.WorkFactor);
    if (MonnitSession.CurrentCustomer != null)
      customer.PasswordExpired = true;
    customer.FirstName = model.FirstName;
    customer.LastName = model.LastName;
    customer.NotificationEmail = model.NotificationEmail;
    customer.NotificationPhone = model.NotificationPhone;
    customer.UISMSCarrierID = new long?(model.SMSCarrierID.ToLong());
    customer.IsAdmin = true;
    customer.Save();
    account1.PrimaryContactID = customer.CustomerID;
    account1.Save();
    new AccountSubscription()
    {
      AccountID = account1.AccountID,
      AccountSubscriptionTypeID = 1L,
      ExpirationDate = DateTime.Today.AddYears(100)
    }.Save();
    AccountAddress accountAddress = new AccountAddress();
    accountAddress.AccountAddressType = eAccountAddressType.Primary;
    accountAddress.AccountID = account1.AccountID;
    accountAddress.Address = model.Address;
    accountAddress.Address2 = model.Address2;
    accountAddress.City = model.City;
    accountAddress.State = model.State;
    accountAddress.PostalCode = model.PostalCode;
    accountAddress.Country = model.Country;
    accountAddress.Save();
    account1.ClearSubscritions();
    if (sendEmail)
    {
      string str = $"\r\nYour new account has just been created!<br />\r\nAccount details are as follows<br />\r\n<br />\r\nAccount: {account1.CompanyName}<br />\r\nTimeZone: {account1.TimeZoneDisplayName}<br />\r\nAddress: {accountAddress.ToString()}<br />\r\n<br />\r\nContact: {customer.FullName}<br />\r\nUsername: {customer.UserName}<br />\r\nEmail: {customer.NotificationEmail}<br />\r\nPhone: {customer.NotificationPhone}<br />\r\n";
      using (MailMessage mail = new MailMessage())
      {
        using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, account1))
        {
          mail.Subject = "New Account";
          mail.Body = str;
          mail.IsBodyHtml = true;
          try
          {
            mail.To.Clear();
            mail.To.SafeAdd(customer.NotificationEmail, customer.FullName);
            MonnitUtil.SendMail(mail, smtpClient);
          }
          catch (Exception ex)
          {
            ex.Log("AccountControler.CreateAccount.SendUserEmail");
          }
          str += $"\r\n                <br />\r\n                Created on: {DateTime.Now.ToShortDateString()}<br />\r\n                Reseller: {(account2 != null ? (object) account2.AccountNumber : (object) "")}<br />\r\n                45 day Trial: {model.IsPremium}<br />\r\n                Subscription Expiration: {account1.PremiumValidUntil.ToShortDateString()}<br />\r\n                <br />\r\n                ";
          mail.Body = str;
          mail.IsBodyHtml = true;
        }
      }
      using (MailMessage mail = new MailMessage())
      {
        using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, account1))
        {
          try
          {
            mail.Body = str;
            mail.IsBodyHtml = true;
            mail.Subject = "New Account";
            mail.To.Clear();
            if (account2 == null || account2.AccountID == ConfigData.AppSettings("AdminAccountID").ToLong())
            {
              mail.To.Add(new MailAddress(ConfigData.AppSettings("NewAccountNotificationEmail"), "New Account Notification"));
            }
            else
            {
              mail.To.Add(new MailAddress(account2.PrimaryContact.NotificationEmail, account2.PrimaryContact.FullName));
              mail.Bcc.Add(new MailAddress(ConfigData.AppSettings("NewAccountNotificationEmail"), "New Account Notification"));
            }
            MonnitUtil.SendMail(mail, smtpClient);
          }
          catch (Exception ex)
          {
            ex.Log("AccountControler.CreateAccount.SendAdminEmail");
          }
        }
      }
    }
    long customerID = MonnitSession.CurrentCustomer == null ? long.MinValue : MonnitSession.CurrentCustomer.CustomerID;
    account1.LogAuditData(eAuditAction.Create, eAuditObject.Account, customerID, account1.AccountID, "Created new account");
    return account1;
  }

  public static Account InsertAccount2(
    CreateAccountAPIModel2 model,
    bool sendEmail,
    long defaultAccountSubscriptionTypeID)
  {
    Account account1 = new Account();
    account1.AccountNumber = model.AccountNumber;
    account1.CompanyName = model.CompanyName;
    account1.TimeZoneID = model.TimeZoneID;
    account1.PrimaryContactID = model.CustomerID;
    account1.EULAVersion = MonnitSession.CurrentTheme.CurrentEULA;
    account1.EULADate = DateTime.UtcNow;
    account1.CreateDate = DateTime.UtcNow;
    Account account2 = Account.Load(model.ResellerID.ToLong()) ?? Account.Load(MonnitSession.CurrentTheme.AccountID) ?? Account.Load(ConfigData.AppSettings("AdminAccountID").ToLong());
    account1.RetailAccountID = account2.AccountID;
    account1.Save();
    account1.AccountIDTree = $"{account2.AccountIDTree}{account1.AccountID.ToString()}*";
    account1.Save();
    if (model.IsPremium)
      new AccountSubscription()
      {
        AccountID = account1.AccountID,
        AccountSubscriptionTypeID = (defaultAccountSubscriptionTypeID < 0L ? 5L : defaultAccountSubscriptionTypeID),
        ExpirationDate = DateTime.Today.AddDays((double) MonnitUtil.DefaultPremiumLength(MonnitSession.CurrentTheme))
      }.Save();
    new AccountSubscription()
    {
      AccountID = account1.AccountID,
      AccountSubscriptionTypeID = 1L,
      ExpirationDate = DateTime.Today.AddYears(100)
    }.Save();
    AccountAddress accountAddress = new AccountAddress();
    accountAddress.AccountAddressType = eAccountAddressType.Primary;
    accountAddress.AccountID = account1.AccountID;
    accountAddress.Address = model.Address;
    accountAddress.Address2 = model.Address2;
    accountAddress.City = model.City;
    accountAddress.State = model.State;
    accountAddress.PostalCode = model.PostalCode;
    accountAddress.Country = model.Country;
    accountAddress.Save();
    account1.ClearSubscritions();
    if (sendEmail)
    {
      Customer customer = Customer.Load(model.CustomerID);
      string str = $"\r\nYour new account has just been created!<br />\r\nAccount details are as follows<br />\r\n<br />\r\nAccount: {account1.CompanyName}<br />\r\nTimeZone: {account1.TimeZoneDisplayName}<br />\r\nAddress: {accountAddress.ToString()}<br />\r\n<br />\r\nContact: {customer.FullName}<br />\r\nUsername: {customer.UserName}<br />\r\nEmail: {customer.NotificationEmail}<br />\r\nPhone: {customer.NotificationPhone}<br />\r\n";
      using (MailMessage mail = new MailMessage())
      {
        using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, account1))
        {
          mail.Subject = "New Account";
          mail.Body = str;
          mail.IsBodyHtml = true;
          try
          {
            mail.To.Clear();
            mail.To.SafeAdd(customer.NotificationEmail, customer.FullName);
            MonnitUtil.SendMail(mail, smtpClient);
          }
          catch (Exception ex)
          {
            ex.Log("AccountControler.InsertAccount2.SendUserEmail");
          }
          str += $"\r\n                <br />\r\n                Created on: {DateTime.Now.ToShortDateString()}<br />\r\n                Reseller: {(account2 != null ? (object) account2.AccountNumber : (object) "")}<br />\r\n                45 day Trial: {model.IsPremium}<br />\r\n                Subscription Expiration: {account1.PremiumValidUntil.ToShortDateString()}<br />\r\n                <br />\r\n                ";
          mail.Body = str;
          mail.IsBodyHtml = true;
        }
      }
      using (MailMessage mail = new MailMessage())
      {
        using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, account1))
        {
          try
          {
            mail.Body = str;
            mail.IsBodyHtml = true;
            mail.Subject = "New Account";
            mail.To.Clear();
            if (account2 == null || account2.AccountID == ConfigData.AppSettings("AdminAccountID").ToLong())
            {
              mail.To.Add(new MailAddress(ConfigData.AppSettings("NewAccountNotificationEmail"), "New Account Notification"));
            }
            else
            {
              mail.To.Add(new MailAddress(account2.PrimaryContact.NotificationEmail, account2.PrimaryContact.FullName));
              mail.Bcc.Add(new MailAddress(ConfigData.AppSettings("NewAccountNotificationEmail"), "New Account Notification"));
            }
            MonnitUtil.SendMail(mail, smtpClient);
          }
          catch (Exception ex)
          {
            ex.Log("AccountControler.InsertAccount2.SendAdminEmail");
          }
        }
      }
    }
    account1.LogAuditData(eAuditAction.Create, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, account1.AccountID, "Created new account");
    return account1;
  }

  public ActionResult CreateAccount()
  {
    this.ViewData["IndustryType"] = (object) new SelectList((IEnumerable) IndustryClassification.LoadByParent(new long?()), "IndustryClassificationID", "Name");
    this.ViewData["TimeZones"] = (object) new SelectList((IEnumerable) Monnit.TimeZone.LoadAll(), "TimeZoneID", "DisplayName");
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CustomerCan("Navigation_View_Administration"))
      return (ActionResult) this.View((object) new CreateAccountModel());
    return (ActionResult) this.View(nameof (CreateAccount), "Administration", (object) new CreateAccountModel()
    {
      ResellerID = MonnitSession.CurrentCustomer.AccountID.ToStringSafe()
    });
  }

  public ActionResult GetBusinessType(long? id)
  {
    return (ActionResult) this.Json((object) IndustryClassification.LoadByParent(id), JsonRequestBehavior.AllowGet);
  }

  [HttpPost]
  public ActionResult CheckSubscriptionCode(string subscriptionCode)
  {
    if (this.Session["TryCount"] == null)
    {
      this.Session["TryCount"] = (object) 0;
    }
    else
    {
      if ((int) this.Session["TryCount"] > 19)
        return (ActionResult) this.Content("over try");
      this.Session["TryCount"] = (object) ((int) this.Session["TryCount"] + 1);
    }
    if (string.IsNullOrEmpty(subscriptionCode))
      return (ActionResult) this.Content("Failed");
    AccountSubscriptionType subscriptionType = AccountSubscriptionType.LoadByKeyType(OverviewController.CheckSubscriptionCode(subscriptionCode));
    if (subscriptionType != null)
    {
      this.Session["subscriptionType"] = (object) subscriptionType;
      this.Session[nameof (subscriptionCode)] = (object) subscriptionCode;
      this.Session["TryCount"] = (object) 0;
      return (ActionResult) this.Content(subscriptionType.Name);
    }
    this.Session["subscriptionType"] = (object) "";
    this.Session[nameof (subscriptionCode)] = (object) "";
    return (ActionResult) this.Content("Failed");
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult CreateAccount(CreateAccountModel model)
  {
    this.ViewData["IndustryType"] = (object) new SelectList((IEnumerable) IndustryClassification.LoadByParent(new long?()), "IndustryClassificationID", "Name");
    this.ViewData["TimeZones"] = (object) new SelectList((IEnumerable) Monnit.TimeZone.LoadAll(), "TimeZoneID", "DisplayName", (object) model.TimeZoneID);
    if (string.IsNullOrEmpty(model.AccountNumber))
      model.AccountNumber = model.CompanyName;
    if (!Account.CheckAccountNumberIsUnique(model.AccountNumber))
      this.ModelState.AddModelError("CompanyName", "Account already exists");
    if (!model.EULA)
      this.ModelState.AddModelError("EULA", "You must acknowledge you have read and agree to the Terms and Conditions");
    this.Session["CreateAccountModel"] = (object) model;
    if (!string.IsNullOrEmpty(model.Subscription) && model.Subscription.ToLower() == "premiere" && string.IsNullOrEmpty(model.SubscriptionActivationCode))
      this.ModelState.AddModelError("SubscriptionActivationCode", "The Activation Code is Required");
    if (model.TimeZoneID < 1L)
      this.ModelState.AddModelError("TimeZoneID", "Time Zone Must Be Selected.");
    if (this.ModelState.IsValid)
    {
      try
      {
        Account account1 = new Account();
        account1.AccountNumber = model.AccountNumber;
        account1.CompanyName = model.CompanyName;
        account1.TimeZoneID = model.TimeZoneID;
        account1.MaxFailedLogins = model.MaxFailedLogins;
        account1.IndustryTypeID = model.IndustryTypeID;
        account1.BusinessTypeID = model.BusinessTypeID;
        account1.PurchaseLocation = model.PurchaseLocation;
        account1.PrimaryContactID = MonnitSession.CurrentCustomer.CustomerID;
        account1.EULAVersion = MonnitSession.CurrentTheme.CurrentEULA;
        account1.EULADate = DateTime.UtcNow;
        account1.CreateDate = DateTime.UtcNow;
        account1.IsCFRCompliant = false;
        Account account2 = Account.Load(model.ResellerID.ToLong()) ?? Account.Load(MonnitSession.CurrentTheme.AccountID) ?? Account.Load(ConfigData.AppSettings("AdminAccountID").ToLong());
        account1.RetailAccountID = account2.AccountID;
        account1.Save();
        account1.AccountIDTree = $"{account2.AccountIDTree}{account1.AccountID.ToString()}*";
        account1.Save();
        this.Session["NewAccount"] = (object) account1;
        this.Session["NewAccountAddress"] = (object) new AccountAddress()
        {
          AccountAddressType = eAccountAddressType.Primary,
          Address = model.Address,
          Address2 = model.Address2,
          City = model.City,
          State = model.State,
          PostalCode = model.PostalCode,
          Country = model.Country
        };
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "CreatePrimaryContact",
          controller = "Account"
        });
      }
      catch (Exception ex)
      {
        this.ModelState.AddModelError("", ex.Message);
        ViewDataDictionary viewData = this.ViewData;
        viewData["Exception"] = (object) (viewData["Exception"]?.ToString() + ex.Message);
      }
    }
    return MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.View(nameof (CreateAccount), "Administration", (object) model) : (ActionResult) this.View((object) model);
  }

  public ActionResult CreatePrimaryContact()
  {
    if (!(this.Session["NewAccount"] is Account))
      return (ActionResult) this.Redirect("CreateAccount");
    this.ViewData["Carriers"] = (object) new SelectList((IEnumerable) SMSAccount.SMSList(MonnitSession.CurrentTheme.AccountThemeID), "SMSCarrierID", "SMSCarrierName");
    this.ViewData["PasswordLength"] = (object) MonnitUtil.MinPasswordLength;
    return MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.View(nameof (CreatePrimaryContact), "Administration") : (ActionResult) this.View();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult CreatePrimaryContact(CreatePrimaryContactModel model)
  {
    if (!(this.Session["NewAccount"] is Account account))
      return (ActionResult) this.Redirect("CreateAccount");
    this.ViewData["Carriers"] = (object) new SelectList((IEnumerable) SMSCarrier.LoadAll(), "SMSCarrierID", "SMSCarrierName", (object) model.SMSCarrierID);
    if (!Customer.CheckUsernameIsUnique(model.UserName))
      this.ModelState.AddModelError("Username", "Username not available");
    if (!Customer.CheckNotificationEmailIsUnique(model.NotificationEmail))
      this.ModelState.AddModelError("NotificationEmail", "Email address not available");
    if (!MonnitUtil.IsValidPassword(model.Password, MonnitSession.CurrentTheme))
      this.ModelState.AddModelError("Password", MonnitUtil.PasswordHelperString(MonnitSession.CurrentTheme));
    if (this.ModelState.IsValid)
    {
      try
      {
        Customer customer = new Customer()
        {
          UserName = model.UserName,
          PasswordChangeDate = DateTime.UtcNow,
          Salt = MonnitUtil.GenerateSalt(),
          WorkFactor = ConfigData.AppSettings("WorkFactor").ToInt()
        };
        customer.Password2 = MonnitUtil.GenerateHash(model.Password, customer.Salt, customer.WorkFactor);
        customer.ConfirmPassword = MonnitSession.UseEncryption ? model.Password.Encrypt() : model.Password;
        if (MonnitSession.CurrentCustomer != null)
          customer.PasswordExpired = true;
        customer.FirstName = model.FirstName;
        customer.LastName = model.LastName;
        customer.NotificationEmail = model.NotificationEmail;
        customer.NotificationPhone = model.NotificationPhone;
        customer.UISMSCarrierID = new long?(model.SMSCarrierID.ToLong());
        customer.IsAdmin = true;
        account.Save();
        new AccountSubscription()
        {
          AccountID = account.AccountID,
          AccountSubscriptionTypeID = 1L,
          ExpirationDate = DateTime.Today.AddYears(100)
        }.Save();
        CreateAccountModel createAccountModel = (CreateAccountModel) this.Session["CreateAccountModel"];
        if (!string.IsNullOrEmpty(createAccountModel.Subscription) && createAccountModel.Subscription.ToLower() == "premiere")
        {
          AccountSubscriptionType subscriptionType = (AccountSubscriptionType) this.Session["subscriptionType"];
          if (subscriptionType != null)
          {
            if (subscriptionType.KeyType == "Premiere_Standard_365")
            {
              try
              {
                if (!PurchaseBase.RetreiveActivationKey(createAccountModel.SubscriptionActivationCode, account.CompanyName).Contains("Failed"))
                  new AccountSubscription()
                  {
                    AccountID = account.AccountID,
                    AccountSubscriptionTypeID = subscriptionType.AccountSubscriptionTypeID,
                    ExpirationDate = DateTime.Today.AddDays(365.0),
                    LastKeyUsed = createAccountModel.SubscriptionActivationCode.ToGuid()
                  }.Save();
              }
              catch
              {
              }
            }
            else
            {
              try
              {
                DateTime expirationDateFromMea = OverviewController.GetExpirationDateFromMEA(createAccountModel.SubscriptionActivationCode, account.AccountID, subscriptionType.KeyType, account.CurrentSubscription.AccountSubscriptionType.KeyType, DateTime.UtcNow);
                if (expirationDateFromMea != DateTime.MinValue)
                  new AccountSubscription()
                  {
                    AccountID = account.AccountID,
                    AccountSubscriptionTypeID = subscriptionType.AccountSubscriptionTypeID,
                    ExpirationDate = expirationDateFromMea,
                    LastKeyUsed = createAccountModel.SubscriptionActivationCode.ToGuid()
                  }.Save();
              }
              catch
              {
              }
            }
          }
        }
        else if (!string.IsNullOrEmpty(createAccountModel.Subscription) && createAccountModel.Subscription.ToLower() == "trial")
          new AccountSubscription()
          {
            AccountID = account.AccountID,
            AccountSubscriptionTypeID = 10L,
            ExpirationDate = DateTime.Today.AddDays(45.0)
          }.Save();
        else if (string.IsNullOrEmpty(createAccountModel.Subscription))
        {
          createAccountModel.PremiumDays = MonnitUtil.DefaultPremiumLength(MonnitSession.CurrentTheme);
          createAccountModel.IsPremium = createAccountModel.PremiumDays > 0;
          if (createAccountModel.IsPremium)
            new AccountSubscription()
            {
              AccountID = account.AccountID,
              AccountSubscriptionTypeID = 10L,
              ExpirationDate = DateTime.Today.AddDays((double) createAccountModel.PremiumDays)
            }.Save();
        }
        customer.AccountID = account.AccountID;
        customer.Save();
        AccountAddress address = this.Session["NewAccountAddress"] as AccountAddress;
        address.AccountID = account.AccountID;
        address.Save();
        account.PrimaryContactID = customer.CustomerID;
        account.ClearSubscritions();
        account.Save();
        account.LogAuditData(eAuditAction.Create, eAuditObject.Account, customer.CustomerID, account.AccountID, "Created primary contact for account");
        Account.UpdateAccountTree(account.AccountID);
        this.SendNewAccountEmail(account, customer, address);
        FormsAuthentication.SetAuthCookie(model.UserName, false);
        MonnitSession.CurrentCustomer = customer;
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "CreateNetwork",
          controller = "CSNet",
          id = account.AccountID
        });
      }
      catch (Exception ex)
      {
        this.ModelState.AddModelError("", ex.Message);
        ViewDataDictionary viewData = this.ViewData;
        viewData["Exception"] = (object) (viewData["Exception"]?.ToString() + ex.Message);
      }
    }
    return MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.View(nameof (CreatePrimaryContact), "Administration", (object) model) : (ActionResult) this.View((object) model);
  }

  public void SendTFAEmailCode(Account account, Customer customer, string code)
  {
    string str = $"\r\n                  <div style='max-width: 700px;'>\r\n      <div style='color: black!important;'>\r\n        <div>\r\n          <h3 style='font-weight: 100;'>\r\n            To complete the login to your account (or complete two-step\r\n            verification setup) for your portal, you must enter the following\r\n            code:\r\n          </h3>\r\n          <div\r\n            style='font-size: 1.45rem;color: #E8AA2F; text-align: center;'\r\n          >\r\n            <h3 style='font-weight:300;'>YOUR VERIFICATION CODE</h3>\r\n          </div>\r\n          <div\r\n            style='background: rgb(199, 199, 199);width: 250px; margin: 0 auto;text-align: center;'\r\n          >\r\n            <h3 style='padding: 10px;margin: 0;'>{code}</h3>\r\n          </div>\r\n          <br />\r\n          <div>\r\n            <h3 style='font-weight: 100;'>\r\n              This code is valid for 20 minutes.\r\n            </h3>\r\n          </div>\r\n          <h3 style='font-weight: 100;'>\r\n            The login was requested for your account. If you did not submit this\r\n            request, you should change your password.\r\n          </h3>\r\n        </div>\r\n      </div>\r\n    </div>";
    using (MailMessage mail = new MailMessage())
    {
      using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, account))
      {
        mail.Subject = "Two-Factor Authentication Code";
        mail.Body = str;
        mail.IsBodyHtml = true;
        try
        {
          mail.To.Clear();
          mail.To.SafeAdd(customer.NotificationEmail, customer.FullName);
          MonnitUtil.SendMail(mail, smtpClient);
          this.Session["TFAMethod"] = (object) 2;
        }
        catch (Exception ex)
        {
          ex.Log("AccountController.TwoFactorAuth.SendTFAEmailCode");
        }
      }
    }
  }

  public void SendNewAccountEmail(Account account, Customer customer, AccountAddress address)
  {
    string str1 = $"\r\n                Your new account has just been created!<br />\r\n                Account details are as follows<br />\r\n                <br />\r\n                Account: {account.CompanyName}<br />\r\n                TimeZone: {account.TimeZoneDisplayName}<br />\r\n                Address: {address.ToString()}<br />\r\n                <br />\r\n                Contact: {customer.FullName}<br />\r\n                Username: {customer.UserName}<br />\r\n                Email: {customer.NotificationEmail}<br />\r\n                Phone: {customer.NotificationPhone}<br />\r\n                ";
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
          ex.Log("AccountControler.CreateAccount.SendUserEmail");
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
          ex.Log("AccountController.CreateAccount.SendAdminEmail ");
        }
      }
    }
  }

  public ActionResult ChangePremiumDate(long id, DateTime date)
  {
    if (!MonnitSession.CustomerCan("Account_Set_Premium"))
      return (ActionResult) this.Content("Not Authorized!");
    try
    {
      Account account = Account.Load(id);
      if (account == null || !MonnitSession.IsAuthorizedForAccount(account.AccountID))
        return (ActionResult) this.Content($"AccountID: {id} could not be updated");
      AccountSubscription accountSubscription = account.CurrentSubscription;
      if (accountSubscription.AccountSubscriptionTypeID == 1L || accountSubscription.AccountSubscriptionTypeID == 10L)
      {
        accountSubscription = (AccountSubscription) null;
        foreach (AccountSubscription subscription in account.Subscriptions)
        {
          if (subscription.AccountSubscriptionTypeID != 1L && subscription.AccountSubscriptionTypeID != 10L && (accountSubscription == null || subscription.ExpirationDate > accountSubscription.ExpirationDate))
            accountSubscription = subscription;
        }
        if (accountSubscription == null)
        {
          accountSubscription = new AccountSubscription();
          accountSubscription.AccountID = account.AccountID;
          accountSubscription.AccountSubscriptionTypeID = 5L;
          accountSubscription.CSNetID = long.MinValue;
        }
      }
      accountSubscription.UpdateAccountSubscriptionDate(date, MonnitSession.CurrentCustomer, "Admin Screen");
      AccountSubscription.AccountSubscriptionFeatureChange(account, account.CurrentSubscription);
      if (account.CurrentSubscription.IsActive)
      {
        using (MailMessage mail = new MailMessage())
        {
          using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, account))
          {
            mail.To.Clear();
            mail.To.SafeAdd(ConfigData.AppSettings("NewAccountNotificationEmail"), "Monnit Accounting");
            mail.Subject = "New Premiere Account";
            mail.Body = $"Account: {account.CompanyName} has just been created as a Premiere Account by {MonnitSession.CurrentCustomer.Account.CompanyName}";
            mail.IsBodyHtml = true;
            MonnitUtil.SendMail(mail, smtpClient);
          }
        }
      }
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content($"AccountID: {id} could not be updated");
    }
    return (ActionResult) this.Content("Success!");
  }

  [Authorize]
  public ActionResult ChangePassword()
  {
    this.ViewData["PasswordLength"] = (object) MonnitUtil.MinPasswordLength;
    return (ActionResult) this.View();
  }

  [Authorize]
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
          user.PasswordExpired = false;
          user.PasswordChangeDate = DateTime.UtcNow;
          user.Salt = MonnitUtil.GenerateSalt();
          user.WorkFactor = ConfigData.AppSettings("WorkFactor").ToInt();
          user.Password2 = MonnitUtil.GenerateHash(model.NewPassword, user.Salt, user.WorkFactor);
          user.Password = string.Empty;
          user.Save();
          MonnitSession.CurrentCustomer = user;
          return (ActionResult) this.RedirectToRoute("Default", (object) new
          {
            action = "ChangePasswordSuccess",
            controller = "Account"
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

  [Authorize]
  public ActionResult Index()
  {
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = nameof (Index),
      controller = "Overview"
    });
  }

  [Authorize]
  public ActionResult List(string searchCriteria)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_Administration") && !MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    searchCriteria = WebUtility.HtmlEncode(searchCriteria);
    if (searchCriteria.Length < 1)
      return (ActionResult) this.View((object) new System.Collections.Generic.List<AccountSearchModel>());
    if (searchCriteria.ToLower() == "Show All".ToLower())
      searchCriteria = "";
    return (ActionResult) this.View((object) AccountSearchModel.Search(MonnitSession.CurrentCustomer.CustomerID, searchCriteria, 100));
  }

  [Authorize]
  public ActionResult AccountPreference(long id)
  {
    if (!MonnitSession.CustomerCan("Account_Edit") || !MonnitSession.IsAuthorizedForAccount(id))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    Account account = Account.Load(id);
    if (account == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__58.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__58.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AccountController.\u003C\u003Eo__58.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__58.\u003C\u003Ep__0, this.ViewBag, id);
    return (ActionResult) this.View((object) PreferenceType.LoadAccountAllowedByAccountThemeID(account.GetThemeID()));
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult EditPreference(long id, FormCollection collection)
  {
    if (!MonnitSession.CustomerCan("Account_Edit") || !MonnitSession.IsAuthorizedForAccount(id))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    Account account = Account.Load(id);
    if (account == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    try
    {
      foreach (PreferenceType preferenceType in PreferenceType.LoadAccountAllowedByAccountThemeID(account.GetThemeID()))
      {
        Preference preference = Preference.LoadByPreferenceTypeIDandAccountID(preferenceType.PreferenceTypeID, account.AccountID);
        if (preference == null)
        {
          preference = new Preference();
          preference.AccountID = account.AccountID;
          preference.PreferenceTypeID = preferenceType.PreferenceTypeID;
        }
        preference.Value = preferenceType.DefaultValue;
        if (!string.IsNullOrEmpty(collection[preferenceType.Name]))
          preference.Value = collection[preferenceType.Name];
        preference.Save();
      }
    }
    catch (Exception ex)
    {
      ex.Log("Account preference edit failed: accountID: " + account.AccountID.ToString());
      return (ActionResult) this.Content("Failed");
    }
    return (ActionResult) this.Content("Success");
  }

  public JsonResult GetTimeZones(string Region)
  {
    return this.Json((object) Monnit.TimeZone.LoadByRegion(Region).Select<Monnit.TimeZone, string>((Func<Monnit.TimeZone, string>) (zone => $"{zone.TimeZoneID.ToString()}|{zone.DisplayName}")), JsonRequestBehavior.AllowGet);
  }

  public JsonResult GetTimeZonesWithCurrentTime(string Region)
  {
    return this.Json((object) Monnit.TimeZone.LoadByRegion(Region).Select<Monnit.TimeZone, string>((Func<Monnit.TimeZone, string>) (zone =>
    {
      string str1 = zone.TimeZoneID.ToString();
      string str2;
      if (!zone.DisplayName.Contains("GMT"))
        str2 = $"{zone.DisplayName}|{zone.CurrentTimeWithName.Split('|')[1]}";
      else
        str2 = $"{zone.TimeZoneIDString} {zone.DisplayName.Split(')')[0]})|{zone.CurrentTimeWithName.Split('|')[1]}";
      return $"{str1}|{str2}";
    })), JsonRequestBehavior.AllowGet);
  }

  [Authorize]
  public ActionResult Edit(long id)
  {
    if (!MonnitSession.CustomerCan("Account_Edit") || !MonnitSession.IsAuthorizedForAccount(id))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    Account model = Account.Load(id);
    Monnit.TimeZone timeZone = Monnit.TimeZone.Load(model.TimeZoneID);
    this.ViewData["Region"] = (object) timeZone.Region;
    this.ViewData["Regions"] = (object) new SelectList((IEnumerable) Monnit.TimeZone.LoadRegions(), (object) timeZone.Region);
    this.ViewData["TimeZones"] = (object) new SelectList((IEnumerable) Monnit.TimeZone.LoadByRegion(timeZone.Region), "TimeZoneID", "DisplayName");
    this.ViewData["CSNetList"] = (object) CSNet.LoadByAccountID(id).OrderBy<CSNet, string>((Func<CSNet, string>) (item => item.Name)).ToList<CSNet>();
    if (model == null)
      model = MonnitSession.CurrentCustomer.Account;
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult Delete(long id)
  {
    return !MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Account"
    }) : (ActionResult) this.View((object) Account.Load(id));
  }

  [Authorize]
  public ActionResult Overview(long? id)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_My_Account"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = nameof (Overview)
      });
    int num;
    if (id.HasValue)
    {
      long accountId = MonnitSession.CurrentCustomer.AccountID;
      long? nullable = id;
      long valueOrDefault = nullable.GetValueOrDefault();
      num = accountId == valueOrDefault & nullable.HasValue ? 1 : 0;
    }
    else
      num = 1;
    if (num != 0)
      return (ActionResult) this.View((object) MonnitSession.CurrentCustomer.Account);
    if (MonnitSession.CustomerCan("Navigation_View_Administration"))
    {
      Account model = Account.Load(id ?? long.MinValue);
      if (model != null && MonnitSession.IsAuthorizedForAccount(model.AccountID))
        return (ActionResult) this.View(nameof (Overview), "Administration", (object) model);
    }
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = nameof (Overview)
    });
  }

  [Authorize]
  public ActionResult accountLookUp(long accountID)
  {
    Account account = Account.Load(accountID);
    return account == null ? (ActionResult) this.Content("No Account Found") : (ActionResult) this.Content(account.AccountNumber);
  }

  [Authorize]
  public ActionResult UpdateAccountParent(long accountID, long parentID)
  {
    Account DBObject = Account.Load(accountID);
    if (DBObject == null)
      return (ActionResult) this.Content("Invalid Account ID");
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(accountID))
      return (ActionResult) this.Content(ThemeController.PermissionErrorMessage.IsAuthorizedForAccount(accountID));
    if (!MonnitSession.CurrentCustomer.CanAssignParent(accountID))
      return (ActionResult) this.Content(ThemeController.PermissionErrorMessage.IsAdmin());
    Account account = Account.Load(parentID);
    if (account == null)
      return (ActionResult) this.Content("Invalid Parent Account ID");
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(parentID))
      return (ActionResult) this.Content(ThemeController.PermissionErrorMessage.IsAuthorizedForAccount(parentID));
    if (accountID == parentID)
      return (ActionResult) this.Content(((HtmlHelper) null).TranslateTag("Account cannot be its own parent"));
    if (DBObject.AccountID == ConfigData.AppSettings("AdminAccountID").ToLong())
      return (ActionResult) this.Content(((HtmlHelper) null).TranslateTag("Admin account cannot have a parent account"));
    try
    {
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Moved subaccount under different account");
      foreach (Account ancestor in Account.Ancestors(MonnitSession.CurrentCustomer.CustomerID, account.AccountID))
      {
        if (ancestor.AccountID == DBObject.AccountID)
          return (ActionResult) this.Content("Unauthorized: Invalid parent request");
      }
      DBObject.RetailAccountID = account.AccountID;
      DBObject.Save();
      Account.UpdateAccountTree(DBObject.AccountID);
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log($"Error editing account parent. AccountID: {accountID.ToString()}ParentID: {parentID.ToString()}");
    }
    return (ActionResult) null;
  }

  [Authorize]
  [HttpPost]
  public ActionResult UpdateSubscription(long id, string subscriptionActivationCode)
  {
    Account account = Account.Load(id);
    if (this.Session["TryCount"] == null)
    {
      this.Session["TryCount"] = (object) 0;
    }
    else
    {
      if ((int) this.Session["TryCount"] > 19)
        return (ActionResult) this.Content("Account Update Locked: Too Many Attempts!");
      this.Session["TryCount"] = (object) ((int) this.Session["TryCount"] + 1);
    }
    string str1 = OverviewController.CheckSubscriptionCode(subscriptionActivationCode);
    AccountSubscriptionType subscriptionType = AccountSubscriptionType.LoadByKeyType(str1);
    if (subscriptionType == null)
    {
      this.ModelState.AddModelError(nameof (subscriptionActivationCode), str1);
      return (ActionResult) this.Content("Failed");
    }
    AccountSubscription accountSubscription = (AccountSubscription) null;
    DateTime expirationDate = account.CurrentSubscription.ExpirationDate;
    if (subscriptionType.KeyType == "Premiere_Standard_365")
    {
      string str2 = CheckoutController.RetreiveActivationKey(subscriptionActivationCode, account.CompanyName, str1, account.CurrentSubscription.AccountSubscriptionType.KeyType, expirationDate);
      if (str2.Contains("Failed"))
        return (ActionResult) this.Content(str2);
      string[] strArray = Encoding.UTF8.GetString(Convert.FromBase64String(str2)).Split('|')[0].Split('_');
      DateTime date = DateTime.UtcNow.AddDays((double) strArray[2].ToInt());
      account.LogAuditData(eAuditAction.Update, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Updated account subscription");
      foreach (AccountSubscription subscription in account.Subscriptions)
      {
        if (subscription.AccountSubscriptionTypeID == subscriptionType.AccountSubscriptionTypeID)
        {
          accountSubscription = subscription;
          break;
        }
      }
      if (accountSubscription != null)
      {
        if (accountSubscription.ExpirationDate > DateTime.UtcNow)
          date = accountSubscription.ExpirationDate.AddDays((double) strArray[2].ToInt());
        accountSubscription.LastKeyUsed = subscriptionActivationCode.ToGuid();
        accountSubscription.UpdateAccountSubscriptionDate(date, MonnitSession.CurrentCustomer, "Key Entered: " + subscriptionActivationCode);
      }
      else
        new AccountSubscription()
        {
          AccountID = account.AccountID,
          AccountSubscriptionTypeID = subscriptionType.AccountSubscriptionTypeID,
          ExpirationDate = date,
          LastKeyUsed = subscriptionActivationCode.ToGuid()
        }.Save();
      account.ClearSubscritions();
      AccountSubscription.AccountSubscriptionFeatureChange(account, account.CurrentSubscription);
      if (account.AccountID == MonnitSession.CurrentCustomer.AccountID)
      {
        MonnitSession.CurrentSubscription = (AccountSubscription) null;
        MonnitSession.CurrentCustomer.Account = (Account) null;
      }
      this.Session["TryCount"] = (object) 0;
      account.LogAuditData(eAuditAction.Update, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Updated Subscription using Activation Code:" + subscriptionActivationCode);
      return (ActionResult) this.Content("Success");
    }
    try
    {
      DateTime expirationDateFromMea = OverviewController.GetExpirationDateFromMEA(subscriptionActivationCode, account.AccountID, subscriptionType.KeyType, account.CurrentSubscription.AccountSubscriptionType.KeyType, expirationDate);
      if (expirationDateFromMea == DateTime.MinValue)
        throw new Exception("Update Subscription Failed; Invalid Subscription Activation Key: " + subscriptionActivationCode);
      account.LogAuditData(eAuditAction.Update, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Updated account subscription");
      foreach (AccountSubscription subscription in account.Subscriptions)
      {
        if (subscription.AccountSubscriptionTypeID == subscriptionType.AccountSubscriptionTypeID)
        {
          accountSubscription = subscription;
          break;
        }
      }
      if (accountSubscription != null)
      {
        accountSubscription.LastKeyUsed = subscriptionActivationCode.ToGuid();
        accountSubscription.UpdateAccountSubscriptionDate(expirationDateFromMea, MonnitSession.CurrentCustomer, "Key Entered: " + subscriptionActivationCode);
      }
      else
        new AccountSubscription()
        {
          AccountID = account.AccountID,
          AccountSubscriptionTypeID = subscriptionType.AccountSubscriptionTypeID,
          ExpirationDate = expirationDateFromMea,
          LastKeyUsed = subscriptionActivationCode.ToGuid()
        }.Save();
      account.ClearSubscritions();
      AccountSubscription.AccountSubscriptionFeatureChange(account, account.CurrentSubscription);
      if (account.AccountID == MonnitSession.CurrentCustomer.AccountID)
      {
        MonnitSession.CurrentSubscription = (AccountSubscription) null;
        MonnitSession.CurrentCustomer.Account = (Account) null;
      }
      this.Session["TryCount"] = (object) 0;
      account.LogAuditData(eAuditAction.Update, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Updated Subscription using Activation Code:" + subscriptionActivationCode);
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log("Update Subscription Failed, message: " + ex.Message);
    }
    return (ActionResult) this.Content("Failed");
  }

  [Authorize]
  public ActionResult NotificationCreditList(long? id)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_My_Account"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    int num;
    if (id.HasValue)
    {
      long accountId = MonnitSession.CurrentCustomer.AccountID;
      long? nullable = id;
      long valueOrDefault = nullable.GetValueOrDefault();
      num = accountId == valueOrDefault & nullable.HasValue ? 1 : 0;
    }
    else
      num = 1;
    if (num != 0)
      return (ActionResult) this.View((object) MonnitSession.CurrentCustomer.Account);
    if (MonnitSession.CustomerCan("Navigation_View_Administration"))
    {
      Account model = Account.Load(id ?? long.MinValue);
      if (model != null && MonnitSession.IsAuthorizedForAccount(model.AccountID))
        return (ActionResult) this.View(nameof (NotificationCreditList), "Administration", (object) model);
    }
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    });
  }

  [Authorize]
  public ActionResult AccountUserList(long? id)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_My_Account"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    int num;
    if (id.HasValue)
    {
      long accountId = MonnitSession.CurrentCustomer.AccountID;
      long? nullable = id;
      long valueOrDefault = nullable.GetValueOrDefault();
      num = accountId == valueOrDefault & nullable.HasValue ? 1 : 0;
    }
    else
      num = 1;
    if (num != 0)
      return (ActionResult) this.View((object) MonnitSession.CurrentCustomer.Account);
    if (MonnitSession.CustomerCan("Navigation_View_Administration"))
    {
      Account model = Account.Load(id ?? long.MinValue);
      if (model != null && MonnitSession.IsAuthorizedForAccount(model.AccountID))
        return (ActionResult) this.View(nameof (AccountUserList), "Administration", (object) model);
    }
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    });
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult CreditSetting(long? id, FormCollection collection)
  {
    Account account = Account.Load(collection["accountID"].ToLong());
    if (!this.ModelState.IsValid)
      return (ActionResult) this.Content("Failed");
    Monnit.CreditSetting creditSetting = Monnit.CreditSetting.Load(id ?? long.MinValue) ?? new Monnit.CreditSetting();
    creditSetting.AccountID = collection["accountID"].ToLong();
    creditSetting.CreditCompareValue = collection["creditCompareValue"].ToLong();
    creditSetting.UserId = collection["UserId"].ToLong();
    creditSetting.CreditClassification = eCreditClassification.Notification;
    account.AutoPurchase = collection["EnableAutoPurchase"].ToBool();
    creditSetting.Save();
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__70.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__70.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Save", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = AccountController.\u003C\u003Eo__70.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__70.\u003C\u003Ep__0, this.ViewBag, "true");
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__70.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__70.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Monnit.CreditSetting, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (CreditSetting), typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = AccountController.\u003C\u003Eo__70.\u003C\u003Ep__1.Target((CallSite) AccountController.\u003C\u003Eo__70.\u003C\u003Ep__1, this.ViewBag, Monnit.CreditSetting.LoadByAccountID(collection["accountID"].ToLong()));
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  [HttpPost]
  public ActionResult ExternalDataPreferenceSettings(long? id, FormCollection collection)
  {
    if (collection["UsersBrokenCountLimit"].ToInt() < 1)
      this.ModelState.AddModelError("UsersBrokenCountLimit", "Threshold must be at least 1.");
    if (!this.ModelState.IsValid)
      return (ActionResult) this.Content("Failed");
    ExternalSubscriptionPreference subscriptionPreference = ExternalSubscriptionPreference.Load(id ?? long.MinValue);
    if (subscriptionPreference == null)
    {
      subscriptionPreference = new ExternalSubscriptionPreference();
      subscriptionPreference.AccountID = collection["AccountID"].ToLong();
    }
    if (subscriptionPreference.UserId != collection["UserId"].ToLong())
    {
      subscriptionPreference.UserId = collection["UserId"].ToLong();
      subscriptionPreference.LastEmailDate = DateTime.MinValue;
      subscriptionPreference.KillSendUserNotifiedDate = DateTime.MinValue;
    }
    subscriptionPreference.UsersBrokenCountLimit = collection["UsersBrokenCountLimit"].ToInt();
    subscriptionPreference.Save();
    Account account1 = Account.Load(subscriptionPreference.AccountID);
    if (account1 != null)
      AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, collection["AccountID"].ToLong(), eAuditAction.Related_Modify, eAuditObject.Account, subscriptionPreference.JsonStringify(), account1.AccountID, "Edited webhook notification settings");
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__71.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__71.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Save", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = AccountController.\u003C\u003Eo__71.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__71.\u003C\u003Ep__0, this.ViewBag, "true");
    Account account2 = Account.Load(collection["AccountID"].ToLong());
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__71.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__71.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, ExternalSubscriptionPreference, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExternalSubscriptionPreference", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = AccountController.\u003C\u003Eo__71.\u003C\u003Ep__1.Target((CallSite) AccountController.\u003C\u003Eo__71.\u003C\u003Ep__1, this.ViewBag, ExternalSubscriptionPreference.LoadByAccountId(account2.AccountID));
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  public ActionResult UserList(long id)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_My_Account"))
      return (ActionResult) this.Content("Not Authorized");
    if (MonnitSession.CurrentCustomer.AccountID == id)
      return (ActionResult) this.View((object) MonnitSession.CurrentCustomer.Account);
    if (MonnitSession.CustomerCan("Navigation_View_Administration"))
    {
      Account model = Account.Load(id);
      if (model != null && MonnitSession.IsAuthorizedForAccount(model.AccountID))
        return (ActionResult) this.View((object) model);
    }
    return (ActionResult) this.View((object) MonnitSession.CurrentCustomer.Account);
  }

  [Authorize]
  public ActionResult NetworkSettings(long? id, long? networkID)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_Settings"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    Account account1 = (Account) null;
    long? nullable;
    int num1;
    if (id.HasValue)
    {
      long accountId = MonnitSession.CurrentCustomer.AccountID;
      nullable = id;
      long valueOrDefault = nullable.GetValueOrDefault();
      num1 = accountId == valueOrDefault & nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 1;
    if (num1 != 0)
      account1 = MonnitSession.CurrentCustomer.Account;
    else if (MonnitSession.CustomerCan("Navigation_View_Administration"))
    {
      Account account2 = Account.Load(id ?? long.MinValue);
      account1 = account2 == null || !MonnitSession.IsAuthorizedForAccount(account2.AccountID) ? MonnitSession.CurrentCustomer.Account : account2;
    }
    if (!networkID.HasValue && MonnitSession.SensorListFilters.CSNetID > 0L)
      networkID = new long?(MonnitSession.SensorListFilters.CSNetID);
    nullable = networkID;
    long num2 = 0;
    if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
    {
      CSNet csNet = CSNet.Load(networkID.ToLong());
      MonnitSession.SensorListFilters.CSNetID = csNet == null || csNet.AccountID != account1.AccountID ? long.MinValue : networkID.ToLong();
    }
    else
      MonnitSession.SensorListFilters.CSNetID = long.MinValue;
    // ISSUE: reference to a compiler-generated field
    if (AccountController.\u003C\u003Eo__73.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AccountController.\u003C\u003Eo__73.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NetworkID", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AccountController.\u003C\u003Eo__73.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__73.\u003C\u003Ep__0, this.ViewBag, networkID);
    return MonnitSession.CurrentCustomer.AccountID != account1.AccountID ? (ActionResult) this.View(nameof (NetworkSettings), "Administration", (object) id) : (ActionResult) this.View((object) account1.AccountID);
  }

  [Authorize]
  public ActionResult SubscriptionExpiring() => (ActionResult) this.View();

  [Authorize]
  public ActionResult CreateSensorGroup(long id)
  {
    return (ActionResult) this.PartialView("EditSensorGroup", (object) new SensorGroup()
    {
      AccountID = id
    });
  }

  [Authorize]
  public ActionResult EditSensorGroup(long id)
  {
    return (ActionResult) this.PartialView((object) SensorGroup.Load(id));
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult EditSensorGroup(SensorGroup model)
  {
    if (string.IsNullOrEmpty(model.Name))
      this.ModelState.AddModelError("Name", "Required");
    if (!this.ModelState.IsValid)
      return (ActionResult) this.PartialView((object) model);
    if (model.AccountID < 0L)
      model.AccountID = MonnitSession.CurrentCustomer.AccountID;
    model.PrimaryKeyValue = model.SensorGroupID;
    model.Save();
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  public ActionResult DeleteSensorGroup(long id)
  {
    SensorGroup sensorGroup = SensorGroup.Load(id);
    foreach (SensorGroupSensor sensor in sensorGroup.Sensors)
      sensorGroup.RemoveSensor(sensor.SensorID);
    sensorGroup.Delete();
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult AddSensorToGroup(long id, long groupID, int position)
  {
    foreach (BaseDBObject baseDbObject in SensorGroupSensor.LoadBySensor(id))
      baseDbObject.Delete();
    int num = 1;
    SensorGroup sensorGroup = SensorGroup.Load(groupID);
    if (sensorGroup != null)
    {
      foreach (SensorGroupSensor sensor in sensorGroup.Sensors)
      {
        if (num == position)
          ++num;
        sensor.Position = num;
        sensor.Save();
        ++num;
      }
      sensorGroup.AddSensor(id, position);
    }
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult AddSensorToGroupToo(long id, long groupID)
  {
    foreach (BaseDBObject baseDbObject in SensorGroupSensor.LoadBySensor(id))
      baseDbObject.Delete();
    SensorGroup.Load(groupID)?.AddSensor(id);
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult RemoveSensorFromGroup(long id)
  {
    foreach (BaseDBObject baseDbObject in SensorGroupSensor.LoadBySensor(id))
      baseDbObject.Delete();
    return (ActionResult) this.Content("Success");
  }

  public ActionResult Cookie() => (ActionResult) this.View(nameof (Cookie));

  public ActionResult Data() => (ActionResult) this.View(nameof (Data));

  public ActionResult Privacy() => (ActionResult) this.View();

  [Authorize]
  public ActionResult Export()
  {
    return (ActionResult) this.View((object) MonnitSession.CurrentCustomer.Account);
  }

  [Authorize]
  public ActionResult AccountLink()
  {
    System.Collections.Generic.List<CustomerAccountLink> model = CustomerAccountLink.LoadAllByCustomerID(MonnitSession.CurrentCustomer.CustomerID);
    if (MonnitSession.CurrentCustomer.AccountID == MonnitSession.CurrentCustomer.DefaultAccount.AccountID)
    {
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__86.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__86.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "showMainAcct", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AccountController.\u003C\u003Eo__86.\u003C\u003Ep__0.Target((CallSite) AccountController.\u003C\u003Eo__86.\u003C\u003Ep__0, this.ViewBag, false);
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      if (AccountController.\u003C\u003Eo__86.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AccountController.\u003C\u003Eo__86.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "showMainAcct", typeof (AccountController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AccountController.\u003C\u003Eo__86.\u003C\u003Ep__1.Target((CallSite) AccountController.\u003C\u003Eo__86.\u003C\u003Ep__1, this.ViewBag, true);
    }
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult DeleteAccountLink(long customerID, long accountID)
  {
    try
    {
      CustomerAccountLink customerAccountLink = CustomerAccountLink.Load(customerID, accountID);
      customerAccountLink.CustomerDeleted = true;
      customerAccountLink.DateAccountDeleted = DateTime.UtcNow;
      customerAccountLink.Save();
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  public void SendLockedMail(Customer cust)
  {
    if (cust.GUID != null && cust.GUID.Length > 0)
      return;
    Account account = Account.Load(cust.AccountID);
    EmailTemplate emailTemplate = (EmailTemplate) null;
    if (account != null)
    {
      emailTemplate = EmailTemplate.LoadBest(account, eEmailTemplateFlag.Generic) ?? new EmailTemplate();
      if (emailTemplate == null)
        return;
    }
    NotificationRecorded notificationRecorded = new NotificationRecorded();
    notificationRecorded.NotificationType = eNotificationType.Email;
    notificationRecorded.CustomerID = cust.CustomerID;
    notificationRecorded.NotificationDate = DateTime.UtcNow;
    string str1 = "Your account has been locked due to excessive failed login attempts. Click this link to {UNLOCKLINK} your account, if you're having problems with the above link please paste the below link into your address bar.<br /> <br /> <text>{hrdlnk}</text>";
    string str2 = "User Locked";
    notificationRecorded.NotificationSubject = str2;
    cust.GenerateGUID();
    AccountTheme accountTheme = account.getTheme();
    if (accountTheme == null)
    {
      accountTheme = new AccountTheme();
      accountTheme.Domain = "localhost:50098";
    }
    string newValue = $"http://{accountTheme.Domain}/Account/Unlock?customerid={cust.CustomerID}&guid={cust.GUID}";
    string Content = str1.Replace("{UNLOCKLINK}", $"<a href=\"{newValue}\">unlock</a>").Replace("{hrdlnk}", newValue);
    notificationRecorded.NotificationText = emailTemplate.MailMerge(Content, cust.NotificationEmail);
    Notification.SendNotification(notificationRecorded, new PacketCache());
  }

  [Authorize]
  public ActionResult CreateAccountThemeContact(long id)
  {
    AccountTheme accountTheme = AccountTheme.Find(MonnitSession.CurrentCustomer.Account);
    AccountThemeContact model = new AccountThemeContact();
    if ((accountTheme == null || model == null || accountTheme.AccountThemeID != model.AccountThemeID) && !MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.Redirect("/");
    model.AccountThemeID = id;
    return (ActionResult) this.View("EditAccountThemeContact", (object) model);
  }

  [Authorize]
  public ActionResult DeleteAccountThemeContact(long id)
  {
    AccountTheme accountTheme = AccountTheme.Find(MonnitSession.CurrentCustomer.Account);
    AccountThemeContact accountThemeContact = AccountThemeContact.Load(id);
    if ((accountTheme == null || accountThemeContact == null || accountTheme.AccountThemeID != accountThemeContact.AccountThemeID) && !MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.Redirect("/");
    long accountThemeId = accountThemeContact.AccountThemeID;
    accountThemeContact.Delete();
    return (ActionResult) this.Redirect("/Admin/AccountTheme/" + accountThemeId.ToString());
  }

  [Authorize]
  [HttpPost]
  public ActionResult SaveAccountThemeContact(AccountThemeContact model)
  {
    AccountTheme accountTheme = AccountTheme.Find(MonnitSession.CurrentCustomer.Account);
    if ((accountTheme == null || model == null || accountTheme.AccountThemeID != model.AccountThemeID) && !MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.Redirect("/");
    if (!this.ModelState.IsValid)
      return (ActionResult) this.View("EditAccountThemeContact", (object) model);
    model.Save();
    return (ActionResult) this.Redirect("/Admin/AccountTheme/" + model.AccountThemeID.ToString());
  }

  [Authorize]
  public ActionResult UserSettings(long id) => (ActionResult) this.View((object) Customer.Load(id));

  public ActionResult ProxyCustomer(long id)
  {
    if (MonnitSession.CurrentCustomer.CustomerID == id)
      return (ActionResult) this.Content("Success");
    if (!MonnitSession.CustomerCan("Proxy_Login"))
      return (ActionResult) this.Content("Access Denied");
    Customer curCust = MonnitSession.CurrentCustomer;
    Customer newCustomer = Customer.Load(id);
    if (newCustomer != null)
    {
      if (Debugger.IsAttached)
      {
        Account.Ancestors(curCust.CustomerID, newCustomer.AccountID);
        Account account = Account.Ancestors(curCust.CustomerID, newCustomer.AccountID).FirstOrDefault<Account>((Func<Account, bool>) (a => a.AccountID == curCust.AccountID));
        bool flag1 = curCust.CanSeeAccount(newCustomer.AccountID);
        bool flag2 = Account.IsSubAccount(curCust.AccountID, newCustomer.AccountID);
        if (account == null || !flag1 || !flag2)
          throw new Exception("Account.Ancestors != Account.IsSubAccount");
      }
      bool flag = false;
      foreach (Account ancestor in Account.Ancestors(curCust.CustomerID, newCustomer.AccountID))
      {
        if (ancestor.AccountID == curCust.AccountID)
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        MonnitSession.ProxyUser(newCustomer);
        MonnitSession.CurrentCustomerFavorites.Invalidate();
        return (ActionResult) this.Content("Success");
      }
    }
    return (ActionResult) this.Content("Proxy login failed");
  }

  public ActionResult ReturnSuccess() => (ActionResult) this.Content("Success");

  public ActionResult ReturnError() => (ActionResult) this.Content("Error");

  public ActionResult StopProxyGoHome() => this.UnProxyAndRedirect("/Settings/AdminSearch");

  public ActionResult UnProxy()
  {
    return MonnitSession.UnProxySimple() ? (ActionResult) this.Content("Success") : (ActionResult) this.Content("Unproxy Error: Session Timeout");
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ProxyDeviceSubAccount(long deviceID, string deviceType)
  {
    long id = long.MinValue;
    try
    {
      if (deviceType.ToLower() == "sensor")
        id = Sensor.Load(deviceID).AccountID;
      if (deviceType.ToLower() == "gateway")
        id = CSNet.Load(Gateway.Load(deviceID).CSNetID).AccountID;
    }
    catch (Exception ex)
    {
      ex.Log(nameof (ProxyDeviceSubAccount));
    }
    return this.ProxySubAccount(id);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ProxySubAccount(long id)
  {
    try
    {
      if (MonnitSession.CurrentCustomer == null)
        return (ActionResult) this.Content("logoff");
      if (!MonnitSession.CurrentCustomer.Can("Account_View"))
        return (ActionResult) this.Content(ThemeController.PermissionErrorMessage.MissingCustomerPermission("Account_View"));
      if (!MonnitSession.CurrentCustomer.CanSeeAccount(id))
        return (ActionResult) this.Content(ThemeController.PermissionErrorMessage.IsAuthorizedForAccount(id));
      if (MonnitSession.CurrentCustomer.CustomerAccountLinkList.FirstOrDefault<CustomerAccountLink>((Func<CustomerAccountLink, bool>) (cal => cal.AccountID == id)) != null)
        MonnitSession.UserIsAccountProxied = true;
      return this.AccountViewProxy(id);
    }
    catch
    {
      return (ActionResult) this.Content("Error");
    }
  }

  public ActionResult ProxyCustomerAccountLink(long accountID)
  {
    try
    {
      if (CustomerAccountLink.LoadAllByCustomerID(MonnitSession.CurrentCustomer.CustomerID).FirstOrDefault<CustomerAccountLink>((Func<CustomerAccountLink, bool>) (cal => cal.AccountID == accountID)) == null)
        return (ActionResult) this.Content("Customer-Account Link not found");
      MonnitSession.UserIsAccountProxied = true;
      return this.AccountViewProxy(accountID);
    }
    catch
    {
      return (ActionResult) this.Content("Error");
    }
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult AccountViewProxy(long accountID)
  {
    if (MonnitSession.CurrentCustomer.AccountID == accountID)
      return (ActionResult) this.Content("Success");
    try
    {
      return !MonnitSession.AccountViewProxy(accountID) ? (ActionResult) this.Content("Error") : (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Error");
    }
  }

  public ActionResult UnProxyAndRedirect(string redirectUrl)
  {
    if (redirectUrl == null)
      redirectUrl = "/Overview/Index";
    return MonnitSession.UnProxySimple() ? (ActionResult) this.Redirect(redirectUrl) : (ActionResult) this.Redirect("/Overview/LogOff");
  }

  public ActionResult UnproxyUserAndGoToLocationOverview()
  {
    return MonnitSession.UnProxySimple() ? (ActionResult) this.Redirect("/Settings/LocationOverview/" + MonnitSession.CurrentCustomer.AccountID.ToString()) : (ActionResult) this.Redirect("/Overview/LogOff");
  }

  public ActionResult UnProxyCustomer() => this.UnProxyAndRedirect("/Overview/Index");

  public ActionResult UnProxyCustomerAccountLink()
  {
    return MonnitSession.UnProxySimple() ? (ActionResult) this.Content("Success") : (ActionResult) this.Content("Error");
  }
}
