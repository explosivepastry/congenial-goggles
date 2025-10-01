// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.SettingsController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.ControllerBase;
using iMonnit.Models;
using Microsoft.CSharp.RuntimeBinder;
using Monnit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OtpNet;
using RedefineImpossible;
using Saml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Xml.Linq;

#nullable disable
namespace iMonnit.Controllers;

public class SettingsController : ThemeController
{
  [AuthorizeDefault]
  public ActionResult UserPermission(long id)
  {
    Customer model = Customer.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID) && !model.IsAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (MonnitSession.CurrentCustomer.CustomerID == model.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Self") || MonnitSession.CurrentCustomer.CustomerID != model.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Other"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__0.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__0.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "saveMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SettingsController.\u003C\u003Eo__0.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__0.\u003C\u003Ep__0, this.ViewBag, "");
    this.ViewData["CSNetList"] = (object) CSNet.LoadByAccountID(model.AccountID).OrderBy<CSNet, string>((System.Func<CSNet, string>) (item => item.Name)).ToList<CSNet>();
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult UserPermission(long id, FormCollection collection, string returnUrl)
  {
    Customer customer = Customer.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(customer.AccountID) && !customer.IsAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (MonnitSession.CurrentCustomer.CustomerID == customer.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Self") || MonnitSession.CurrentCustomer.CustomerID != customer.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Other"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    Account account = Account.Load(customer.AccountID);
    List<CustomerPermission> customerPermissionList = CustomerPermission.LoadAllByCustomerID(customer.CustomerID);
    StringBuilder stringBuilder1 = new StringBuilder();
    stringBuilder1.Append("{\"CustomerPermissions\":{\"Permissions\":\"");
    foreach (CustomerPermission customerPermission in customerPermissionList)
    {
      if (customerPermission.Can && customerPermission.CustomerPermissionTypeID != 2L && customerPermission.CustomerPermissionTypeID != 29L)
      {
        stringBuilder1.AppendFormat("{0}", (object) customerPermission.CustomerPermissionTypeID);
        stringBuilder1.Append(",");
      }
    }
    --stringBuilder1.Length;
    stringBuilder1.Append("\",");
    StringBuilder stringBuilder2 = new StringBuilder();
    stringBuilder2.Append("\"CustomerNetworkPermissions\":\"");
    foreach (CustomerPermission customerPermission in customerPermissionList)
    {
      if (customerPermission.Can && customerPermission.CustomerPermissionTypeID == 2L)
      {
        stringBuilder2.AppendFormat("{0}", (object) customerPermission.CSNetID);
        stringBuilder2.Append(",");
      }
    }
    --stringBuilder2.Length;
    stringBuilder2.Append("\",");
    StringBuilder stringBuilder3 = new StringBuilder();
    stringBuilder3.Append("\"CustomerHBRestrictionPermissions\":\"");
    foreach (CustomerPermission customerPermission in customerPermissionList)
    {
      if (customerPermission.Can && customerPermission.CustomerPermissionTypeID == 29L && (customerPermission.Info != "" || customerPermission.Info == null))
      {
        stringBuilder3.AppendFormat("{0}", (object) customerPermission.Info);
        stringBuilder3.Append(",");
      }
    }
    --stringBuilder3.Length;
    if (stringBuilder3.Length < 39)
      stringBuilder3.Append("\"\"}}");
    else
      stringBuilder3.Append("\"}}");
    stringBuilder1.AppendFormat("{0}", (object) stringBuilder2.ToString());
    stringBuilder1.AppendFormat("{0}", (object) stringBuilder3.ToString());
    AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, customer.CustomerID, eAuditAction.Update, eAuditObject.Customer, stringBuilder1.ToString(), account.AccountID, "Edited customer permissions");
    try
    {
      CustomerController.CustomerPermissionUpdate((NameValueCollection) collection, customer);
      this.ViewData["CSNetList"] = (object) CSNet.LoadByAccountID(customer.AccountID).OrderBy<CSNet, string>((System.Func<CSNet, string>) (item => item.Name)).ToList<CSNet>();
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "saveMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__1.\u003C\u003Ep__0, this.ViewBag, "Success");
      return (ActionResult) this.View((object) customer);
    }
    catch (Exception ex)
    {
      this.ViewData.ModelState.AddModelError("", ex.Message);
      this.ViewData["CSNetList"] = (object) CSNet.LoadByAccountID(customer.AccountID).OrderBy<CSNet, string>((System.Func<CSNet, string>) (item => item.Name)).ToList<CSNet>();
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__1.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__1.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "saveMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__1.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__1.\u003C\u003Ep__1, this.ViewBag, "Failed");
      return (ActionResult) this.View((object) customer);
    }
  }

  [AuthorizeDefault]
  public ActionResult UserNotification(long id)
  {
    Customer model = Customer.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SettingsController.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__2.\u003C\u003Ep__0, this.ViewBag, "");
    this.ViewData["Carriers"] = (object) new SelectList((IEnumerable) SMSAccount.SMSList(MonnitSession.CurrentTheme.AccountThemeID).OrderBy<SMSCarrier, int>((System.Func<SMSCarrier, int>) (s => s.Rank)).ThenBy<SMSCarrier, string>((System.Func<SMSCarrier, string>) (s => s.SMSCarrierName)), "SMSCarrierID", "SMSCarrierName", (object) model.SMSCarrierID);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult UserTwoFactorAuth(long? id)
  {
    Customer model = !id.HasValue ? MonnitSession.CurrentCustomer : Customer.Load(id.GetValueOrDefault());
    if (model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<AuthenticateDevice>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "devices", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SettingsController.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__3.\u003C\u003Ep__0, this.ViewBag, AuthenticateDevice.LoadByCustomerID(model.CustomerID));
    return (ActionResult) this.View((object) model);
  }

  public ActionResult DeleteRememberedDevice(long id)
  {
    AuthenticateDevice.Load(id).Delete();
    return (ActionResult) this.Content("Success");
  }

  public ActionResult DeleteRememberedDevices(string ids)
  {
    string content = "Failed to delete all";
    try
    {
      if (!string.IsNullOrWhiteSpace(ids))
      {
        string str = ids;
        char[] chArray = new char[1]{ ',' };
        foreach (object o in str.Split(chArray))
          AuthenticateDevice.Load(o.ToLong()).Delete();
        content = "Success";
      }
    }
    catch (Exception ex)
    {
      ex.Log($"SettingsController.DeleteRememberedDevices[IDs: {ids}] ");
    }
    return (ActionResult) this.Content(content);
  }

  public ActionResult UpdateRememberedDevice(long id, string deviceName)
  {
    AuthenticateDevice authenticateDevice = AuthenticateDevice.Load(id);
    authenticateDevice.DisplayName = deviceName;
    authenticateDevice.Save();
    return (ActionResult) this.Content("Success");
  }

  public ActionResult EnableTOTP(bool generate)
  {
    string str = generate ? SettingsController.Base32Encoding.ToString(Encoding.ASCII.GetBytes(MonnitUtil.CreateKeyValue(15))) : MonnitSession.CurrentCustomer.TOTPSecret;
    this.Session["TwoFactorAuthSecret"] = (object) str;
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__7.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__7.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "currentAuthenticatedDevice", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SettingsController.\u003C\u003Eo__7.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__7.\u003C\u003Ep__0, this.ViewBag, (object) null);
    return (ActionResult) this.Content(JsonConvert.SerializeObject((object) new Dictionary<string, string>()
    {
      {
        "qr",
        $"otpauth://totp/{MonnitSession.CurrentTheme.Domain}?secret={str}"
      },
      {
        "secretCode",
        str
      }
    }));
  }

  public ActionResult VerifyTotp(string code)
  {
    string input = (string) this.Session["TwoFactorAuthSecret"];
    if (input == null)
      return (ActionResult) this.View();
    string str = "Unknown";
    try
    {
      str = Dns.GetHostEntry(this.Request.ServerVariables["remote_addr"]).HostName;
    }
    catch (Exception ex)
    {
      ex.Log("SettingsController.VerifyTotp | code = " + code);
    }
    bool flag = new Totp(SettingsController.Base32Encoding.ToBytes(input)).VerifyTotp(code, out long _, VerificationWindow.RfcSpecifiedNetworkDelay);
    Customer customer = (Customer) this.Session["TempCustomer"] != null ? (Customer) this.Session["TempCustomer"] : MonnitSession.CurrentCustomer;
    if (!flag || customer == null)
      return (ActionResult) this.Content("Code Expired");
    string keyValue = MonnitUtil.CreateKeyValue(15);
    AccountControllerBase.SetTwoFactorAuthCodeCookie(keyValue, customer, System.Web.HttpContext.Current);
    AuthenticateDevice authenticateDevice = new AuthenticateDevice()
    {
      CustomerID = customer.CustomerID,
      DeviceName = str,
      DisplayName = str,
      Salt = MonnitUtil.GenerateSalt(),
      WorkFactor = 1,
      CreateDate = DateTime.UtcNow,
      LastLoginDate = DateTime.UtcNow
    };
    authenticateDevice.DeviceHash = MonnitUtil.GenerateHash(keyValue, authenticateDevice.Salt, authenticateDevice.WorkFactor);
    authenticateDevice.Save();
    customer.TFAMethod = 3;
    customer.TOTPSecret = input;
    customer.Save();
    if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
      MonnitSession.CurrentCustomer = customer;
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult UserNotification(long id, FormCollection collection, string returnUrl)
  {
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__10.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__10.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__10.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__10.\u003C\u003Ep__0, this.ViewBag, "");
    Customer customer = Customer.Load(id);
    Customer DBObject1 = customer;
    if (!MonnitSession.IsAuthorizedForAccount(customer.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    Account account = Account.Load(customer.AccountID);
    AccountTheme accountTheme = AccountTheme.Find(account.AccountID);
    if (((IEnumerable<string>) collection.AllKeys).Contains<string>("NotificationEmail") && !Customer.CheckNotificationEmailIsUnique(collection["NotificationEmail"], customer.CustomerID))
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__10.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__10.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SettingsController.\u003C\u003Eo__10.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__10.\u003C\u003Ep__1, this.ViewBag, "Email address already taken");
    }
    customer.NotificationEmail = collection["NotificationEmail"].Replace(" ", "");
    customer.NotificationPhone = collection["NotificationPhone"].Trim();
    customer.NotificationPhoneISOCode = string.IsNullOrEmpty(collection["NotificationPhoneISOCode"]) ? customer.NotificationPhoneISOCode : collection["NotificationPhoneISOCode"].ToStringSafe();
    customer.NotificationPhone2 = collection["NotificationPhone2"];
    customer.NotificationPhone2ISOCode = string.IsNullOrEmpty(collection["NotificationPhone2ISOCode"]) ? customer.NotificationPhone2ISOCode : collection["NotificationPhone2ISOCode"].ToStringSafe();
    foreach (Validation validation in Validation.LoadByCustomerID(id))
    {
      if (validation.Type == "email")
      {
        if (validation.TypeValue != customer.NotificationEmail)
          validation.Delete();
      }
      else if (validation.Type == "sms")
      {
        if (validation.TypeValue != customer.NotificationPhone)
          validation.Delete();
        else if (customer.SMSCarrierID != collection["UISMSCarrierID"].ToLong())
          validation.Delete();
      }
      if (validation.Type == "voice" && validation.TypeValue != customer.NotificationPhone2)
        validation.Delete();
    }
    customer.SendSensorNotificationToText = !string.IsNullOrWhiteSpace(collection["NotificationPhone"]);
    if (string.IsNullOrEmpty(accountTheme.FromPhone))
    {
      customer.DirectSMS = false;
      customer.SendSensorNotificationToVoice = false;
    }
    else
    {
      customer.DirectSMS = !string.IsNullOrEmpty(collection["DirectSMS"]);
      customer.SendSensorNotificationToVoice = !string.IsNullOrEmpty(collection["NotificationPhone2"]);
    }
    if (!customer.SendSensorNotificationToText)
    {
      foreach (Notification DBObject2 in Notification.LoadByAccountID(customer.AccountID))
      {
        foreach (NotificationRecipient DBObject3 in DBObject2.NotificationRecipients.Where<NotificationRecipient>((System.Func<NotificationRecipient, bool>) (m => m.CustomerToNotifyID == customer.CustomerID && m.NotificationType == eNotificationType.SMS)))
        {
          if (DBObject2.NotificationRecipients.Where<NotificationRecipient>((System.Func<NotificationRecipient, bool>) (m => m.CustomerToNotifyID == customer.CustomerID && m.NotificationType == eNotificationType.Email)).Count<NotificationRecipient>() > 0)
          {
            string changeRecord = $"{{\"recipientID\": \"{DBObject3.NotificationRecipientID}\", \"NotifcationID\" : \"{DBObject2.NotificationID}\" }} ";
            DBObject2.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, DBObject2.AccountID, "Removed recipient from notification");
            DBObject3.Delete();
          }
          else
          {
            DBObject3.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, DBObject2.AccountID, "Changed recipient type to email");
            DBObject3.NotificationType = eNotificationType.Email;
            DBObject3.Save();
          }
        }
      }
    }
    if (!customer.SendSensorNotificationToVoice)
    {
      foreach (Notification DBObject4 in Notification.LoadByAccountID(customer.AccountID))
      {
        foreach (NotificationRecipient DBObject5 in DBObject4.NotificationRecipients.Where<NotificationRecipient>((System.Func<NotificationRecipient, bool>) (m => m.CustomerToNotifyID == customer.CustomerID && m.NotificationType == eNotificationType.Phone)))
        {
          if (DBObject4.NotificationRecipients.Where<NotificationRecipient>((System.Func<NotificationRecipient, bool>) (m => m.CustomerToNotifyID == customer.CustomerID && m.NotificationType == eNotificationType.Email)).Count<NotificationRecipient>() > 0)
          {
            string changeRecord = $"{{\"recipientID\": \"{DBObject5.NotificationRecipientID}\", \"NotifcationID\" : \"{DBObject4.NotificationID}\" }} ";
            DBObject4.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, DBObject4.AccountID, "Removed recipient from notification");
            DBObject5.Delete();
          }
          else
          {
            DBObject5.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, DBObject4.AccountID, "Changed recipient type to email");
            DBObject5.NotificationType = eNotificationType.Email;
            DBObject5.Save();
          }
        }
      }
    }
    customer.UISMSCarrierID = customer.DirectSMS ? new long?() : (collection["UISMSCarrierID"].ToLong() == 0L ? new long?() : new long?(collection["UISMSCarrierID"].ToLong()));
    if (string.IsNullOrEmpty(customer.NotificationEmail))
      this.ModelState.AddModelError("NotificationEmail", "Email is Required!");
    if (!new ValidateEmailAddressAttribute().IsValid((object) customer.NotificationEmail))
      this.ModelState.AddModelError("NotificationEmail", new ValidateEmailAddressAttribute().FormatErrorMessage("Email Address"));
    if (customer.DirectSMS)
    {
      if (customer.SendSensorNotificationToText && customer.NotificationPhone.Length < 5)
        this.ModelState.AddModelError("NotificationPhone", "Invalid number");
    }
    else
    {
      int num1;
      if (customer.SendSensorNotificationToText)
      {
        long? uismsCarrierId = customer.UISMSCarrierID;
        if (uismsCarrierId.HasValue)
        {
          uismsCarrierId = customer.UISMSCarrierID;
          long num2 = 0;
          num1 = uismsCarrierId.GetValueOrDefault() <= num2 & uismsCarrierId.HasValue ? 1 : 0;
        }
        else
          num1 = 1;
      }
      else
        num1 = 0;
      if (num1 != 0)
        this.ModelState.AddModelError("UISMSCarrierID", "SMS provider required");
      if (customer.SMSCarrier != null && customer.NotificationPhone.RemoveNonNumeric().Count<char>() != customer.SMSCarrier.ExpectedPhoneDigits)
        this.ModelState.AddModelError("NotificationPhone", "Invalid number of digits for selected provider.");
    }
    if (customer.SendSensorNotificationToVoice && customer.NotificationPhone2.Length < 5)
      this.ModelState.AddModelError("NotificationPhone2", "Invalid voice call number");
    customer.AllowPushNotification = accountTheme.AllowPushNotification;
    foreach (object key in collection.Keys)
    {
      string name = key.ToString();
      if (name.StartsWith("OtherPushMsgName"))
      {
        CustomerPushMessageSubscription messageSubscription = CustomerPushMessageSubscription.Load(long.Parse(name.Split('_')[1]));
        messageSubscription.Name = collection[name];
        messageSubscription.Save();
      }
      else if (name.StartsWith("PushMsgName"))
      {
        CustomerPushMessageSubscription messageSubscription = CustomerPushMessageSubscription.Load(long.Parse(name.Split('_')[1]));
        messageSubscription.Name = collection[name];
        messageSubscription.Save();
      }
    }
    if (this.ModelState.IsValid)
    {
      try
      {
        DBObject1.LogAuditData(eAuditAction.Update, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited email or phone number for customer notifications");
        customer.Save();
        if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
          MonnitSession.CurrentCustomer = customer;
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__10.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__10.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = SettingsController.\u003C\u003Eo__10.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__10.\u003C\u003Ep__2, this.ViewBag, "Success");
      }
      catch (Exception ex)
      {
        this.ViewData.ModelState.AddModelError("", ex.Message);
      }
    }
    this.ViewData["Carriers"] = (object) new SelectList((IEnumerable) SMSAccount.SMSList(accountTheme.AccountThemeID).OrderBy<SMSCarrier, int>((System.Func<SMSCarrier, int>) (s => s.Rank)).ThenBy<SMSCarrier, string>((System.Func<SMSCarrier, string>) (s => s.SMSCarrierName)), "SMSCarrierID", "SMSCarrierName", (object) customer.SMSCarrierID);
    return (ActionResult) this.View((object) customer);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult UserMaintenanceDelivery(long id, FormCollection collection)
  {
    Customer customer = Customer.Load(id);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "PassMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__0, this.ViewBag, "");
    if (!MonnitSession.IsAuthorizedForAccount(customer.AccountID))
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__1, this.ViewBag, "");
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__2, this.ViewBag, "Not Authorized");
      return (ActionResult) this.View("UserDetail", (object) customer);
    }
    Account account = Account.Load(customer.AccountID);
    customer.LogAuditData(eAuditAction.Update, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited email or phone number for customer notifications");
    try
    {
      customer.Save();
      if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
        MonnitSession.CurrentCustomer = customer;
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__3.Target((CallSite) SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__3, this.ViewBag, "");
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj5 = SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__4.Target((CallSite) SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__4, this.ViewBag, "Success");
      return (ActionResult) this.View("UserDetail", (object) customer);
    }
    catch (Exception ex)
    {
      this.ViewData.ModelState.AddModelError("", ex.Message);
    }
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj6 = SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__5.Target((CallSite) SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__5, this.ViewBag, "");
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__6 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj7 = SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__6.Target((CallSite) SettingsController.\u003C\u003Eo__11.\u003C\u003Ep__6, this.ViewBag, "Failed");
    return (ActionResult) this.View("UserDetail", (object) customer);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult UserEdit(long id, FormCollection collection)
  {
    Customer customer = Customer.Load(id);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "PassMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__0, this.ViewBag, "");
    if (!MonnitSession.IsAuthorizedForAccount(customer.AccountID))
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__1, this.ViewBag, "Not Authorized");
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__2, this.ViewBag, "");
      return (ActionResult) this.View("UserDetail", (object) customer);
    }
    if (!MonnitSession.CustomerCan("Customer_Edit_Other") || !MonnitSession.CustomerCan("Customer_Edit_Self") && MonnitSession.CurrentCustomer.CustomerID == customer.CustomerID)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__3.Target((CallSite) SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__3, this.ViewBag, "Not Authorized");
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj5 = SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__4.Target((CallSite) SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__4, this.ViewBag, "");
      return (ActionResult) this.View("UserDetail", (object) customer);
    }
    Account account = Account.Load(customer.AccountID);
    customer.LogAuditData(eAuditAction.Update, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited customer information");
    if (MonnitSession.CustomerCan("Customer_Change_Username"))
    {
      if (string.IsNullOrEmpty(collection["UserName"]))
        this.ModelState.AddModelError("Username", "Username is Required");
      else if (customer.UserName != collection["UserName"] && !Customer.CheckUsernameIsUnique(collection["UserName"]))
        this.ModelState.AddModelError("Username", "Username not available");
      customer.UserName = collection["UserName"];
    }
    customer.ConfirmPassword = customer.Password;
    customer.FirstName = collection["FirstName"];
    customer.LastName = collection["LastName"];
    if (MonnitSession.CurrentCustomer.IsAdmin)
    {
      bool isAdmin = customer.IsAdmin;
      customer.IsAdmin = !string.IsNullOrEmpty(collection["IsAdmin"]);
      if (customer.IsAdmin != isAdmin)
        customer.ResetPermissions();
    }
    if (string.IsNullOrEmpty(customer.FirstName))
      this.ModelState.AddModelError("FirstName", "First Name is Required!");
    if (string.IsNullOrEmpty(customer.LastName))
      this.ModelState.AddModelError("LastName", "Last Name is Required!");
    if (!string.IsNullOrEmpty(collection["Title"]))
      customer.Title = collection["Title"];
    if (this.ModelState.IsValid)
    {
      try
      {
        customer.Save();
        if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
          MonnitSession.CurrentCustomer = customer;
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj6 = SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__5.Target((CallSite) SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__5, this.ViewBag, "Success");
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj7 = SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__6.Target((CallSite) SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__6, this.ViewBag, "");
        return (ActionResult) this.View("UserDetail", (object) customer);
      }
      catch (Exception ex)
      {
        this.ViewData.ModelState.AddModelError("", ex.Message);
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__7 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj8 = SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__7.Target((CallSite) SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__7, this.ViewBag, "Failed");
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__8 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj9 = SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__8.Target((CallSite) SettingsController.\u003C\u003Eo__12.\u003C\u003Ep__8, this.ViewBag, "");
    return (ActionResult) this.View("UserDetail", (object) customer);
  }

  [AuthorizeDefault]
  public ActionResult DefaultPassword(long id)
  {
    if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CurrentCustomer.CustomerID == id || MonnitSession.CustomerCan("Customer_Reset_Password_Other"))
    {
      try
      {
        string str = SettingsController.CredentialRecoveryAuthKey();
        Customer cust = Customer.Load(id);
        if (SettingsController.TrySendTempPassword(cust, str))
        {
          cust.Password = "";
          cust.Salt = MonnitUtil.GenerateSalt();
          cust.WorkFactor = ConfigData.AppSettings("WorkFactor").ToInt();
          cust.Password2 = MonnitUtil.GenerateHash(str, cust.Salt, cust.WorkFactor);
          cust.ForceLogoutDate = DateTime.UtcNow;
          cust.PasswordExpired = true;
          cust.Save();
          if (cust.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
            MonnitSession.CurrentCustomer = cust;
          return (ActionResult) this.Content("Success! A temporary password has been emailed to " + cust.NotificationEmail);
        }
      }
      catch (Exception ex)
      {
        ex.Log($"SettingsController.DefaultPassword | CustomerID = {id}");
      }
    }
    return (ActionResult) this.Content("Unable to modify password.");
  }

  [AuthorizeDefault]
  public ActionResult UnlockCustomer(long customerid, string guid)
  {
    Customer customer = Customer.Load(customerid);
    if (customer == null)
      return (ActionResult) null;
    if (!(customer.GUID == guid))
      return (ActionResult) this.Content("Failed");
    customer.unlock();
    customer.Save();
    if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
      MonnitSession.CurrentCustomer = customer;
    return (ActionResult) this.Content("Success: Customer Login Unlocked");
  }

  public ActionResult UserNotificaitonOptIn(string email)
  {
    try
    {
      if (string.IsNullOrWhiteSpace(email))
        return (ActionResult) this.Content("Email hasn't been Opted back in");
      UnsubscribedNotificationEmail.OptIn(email);
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log("SettingsController.UserNotificaitonOptIn | email = " + email);
      return (ActionResult) this.Content("Failed");
    }
  }

  public static bool TrySendTempPassword(Customer cust, string tempPassword)
  {
    if (cust == null)
      return false;
    try
    {
      EmailTemplate emailTemplate = EmailTemplate.LoadBest(cust.Account, eEmailTemplateFlag.Generic);
      if (emailTemplate == null)
      {
        emailTemplate = new EmailTemplate();
        emailTemplate.Subject = "Password Reset";
        emailTemplate.Template = "<p>Your Temporary Password </p><p>{Content}</p>";
      }
      string body = emailTemplate.MailMerge($"We received a request to reset the password associated with this e-mail address.  <br /> <br /> Your temporary password: <strong>{tempPassword}</strong> <br /> <br /> Please return to your browser and log in, using this password. <br /> <br />If you do not recognize this account activity , Please contact support as soon as possible.", cust.NotificationEmail);
      MonnitUtil.SendMail(cust.NotificationEmail, "Password Reset", body, cust.Account);
      return true;
    }
    catch (Exception ex)
    {
      ex.Log("Password Reset Notification email failed. CustomerID: " + cust.CustomerID.ToString());
      return false;
    }
  }

  protected static string CredentialRecoveryAuthKey()
  {
    int capacity = 10;
    char[] chArray = new char[62];
    char[] charArray = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
    byte[] data = new byte[1];
    using (RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider())
    {
      cryptoServiceProvider.GetNonZeroBytes(data);
      data = new byte[capacity];
      cryptoServiceProvider.GetNonZeroBytes(data);
    }
    StringBuilder stringBuilder = new StringBuilder(capacity);
    foreach (byte num in data)
      stringBuilder.Append(charArray[(int) num % charArray.Length]);
    return stringBuilder.ToString();
  }

  [AuthorizeDefault]
  public ActionResult UserChangePassword()
  {
    this.ViewData["PasswordLength"] = (object) MonnitUtil.MinPasswordLength;
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult UserChangePassword(ChangePasswordModel model)
  {
    if (!MonnitUtil.IsValidPassword(model.NewPassword, MonnitSession.CurrentTheme))
      this.ModelState.AddModelError("NewPassword", MonnitUtil.PasswordHelperString(MonnitSession.CurrentTheme));
    if (this.ModelState.IsValid)
    {
      Customer user = (Customer) null;
      if (Account.ValidateUser(this.User.Identity.Name, model.OldPassword, "Web Password Change", this.Request.ClientIP(), MonnitSession.UseEncryption, out user))
      {
        if (user.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
        {
          user.Salt = MonnitUtil.GenerateSalt();
          user.WorkFactor = ConfigData.AppSettings("WorkFactor").ToInt();
          user.Password2 = MonnitUtil.GenerateHash(model.NewPassword, user.Salt, user.WorkFactor);
          user.PasswordExpired = false;
          user.PasswordChangeDate = DateTime.UtcNow;
          user.Save();
          MonnitSession.CurrentCustomer = user;
          // ISSUE: reference to a compiler-generated field
          if (SettingsController.\u003C\u003Eo__19.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            SettingsController.\u003C\u003Eo__19.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = SettingsController.\u003C\u003Eo__19.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__19.\u003C\u003Ep__0, this.ViewBag, "");
          // ISSUE: reference to a compiler-generated field
          if (SettingsController.\u003C\u003Eo__19.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            SettingsController.\u003C\u003Eo__19.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj2 = SettingsController.\u003C\u003Eo__19.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__19.\u003C\u003Ep__1, this.ViewBag, "");
          // ISSUE: reference to a compiler-generated field
          if (SettingsController.\u003C\u003Eo__19.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            SettingsController.\u003C\u003Eo__19.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "PassMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj3 = SettingsController.\u003C\u003Eo__19.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__19.\u003C\u003Ep__2, this.ViewBag, "Password Updated!");
          return (ActionResult) this.View("UserDetail", (object) user);
        }
        this.ModelState.AddModelError("", "The password can only be reset for the current user.");
      }
      else
        this.ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
    }
    this.ViewData["PasswordLength"] = (object) MonnitUtil.MinPasswordLength;
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult AdminResetPassword(long customerID)
  {
    if (!MonnitSession.CurrentCustomer.IsAdmin)
      return (ActionResult) this.Content("You do not have permissions to view this page");
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(Customer.Load(customerID).AccountID))
      return (ActionResult) this.Content("You do not have permissions to view this page");
    this.ViewData["PasswordLength"] = (object) MonnitUtil.MinPasswordLength;
    return (ActionResult) this.View((object) new AdminResetPasswordModel()
    {
      CustomerID = customerID
    });
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult AdminResetPassword(AdminResetPasswordModel model)
  {
    if (!MonnitSession.CurrentCustomer.IsAdmin)
      return (ActionResult) this.Content("You do not have permissions to view this page");
    if (!MonnitUtil.IsValidPassword(model.NewPassword, MonnitSession.CurrentTheme))
      this.ModelState.AddModelError("Password", MonnitUtil.PasswordHelperString(MonnitSession.CurrentTheme));
    Customer customer = Customer.Load(model.CustomerID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(customer.AccountID))
      return (ActionResult) this.Content("You do not have permissions to view this page");
    if (this.ModelState.IsValid)
    {
      customer.Salt = MonnitUtil.GenerateSalt();
      customer.WorkFactor = ConfigData.AppSettings("WorkFactor").ToInt();
      customer.Password2 = MonnitUtil.GenerateHash(model.NewPassword, customer.Salt, customer.WorkFactor);
      customer.PasswordExpired = false;
      customer.PasswordChangeDate = DateTime.UtcNow;
      customer.ForceLogoutDate = DateTime.UtcNow;
      customer.Save();
      if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
        MonnitSession.CurrentCustomer = customer;
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__21.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__21.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SettingsController.\u003C\u003Eo__21.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__21.\u003C\u003Ep__0, this.ViewBag, "");
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__21.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__21.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SettingsController.\u003C\u003Eo__21.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__21.\u003C\u003Ep__1, this.ViewBag, "");
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__21.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__21.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "PassMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = SettingsController.\u003C\u003Eo__21.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__21.\u003C\u003Ep__2, this.ViewBag, "Password Updated!");
      return (ActionResult) this.Content("Success");
    }
    this.ViewData["PasswordLength"] = (object) MonnitUtil.MinPasswordLength;
    return (ActionResult) this.Content("Password does not meet minimum character length");
  }

  [AuthorizeDefault]
  public ActionResult UserPreference(long id)
  {
    Customer customer = Customer.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(customer.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__22.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__22.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "prefMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__22.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__22.\u003C\u003Ep__0, this.ViewBag, "");
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__22.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__22.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Customer, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "customer", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__22.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__22.\u003C\u003Ep__1, this.ViewBag, customer);
    return (ActionResult) this.View((object) PreferenceType.LoadUserAllowedByAccountThemeID(MonnitSession.CurrentCustomer.Account.GetThemeID()));
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult UserPreference(long id, FormCollection collection)
  {
    Customer customer = Customer.Load(id);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__23.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__23.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Customer, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "customer", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__23.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__23.\u003C\u003Ep__0, this.ViewBag, customer);
    if (!MonnitSession.IsAuthorizedForAccount(customer.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CustomerCan("Customer_Edit_Other") && MonnitSession.CurrentCustomer.CustomerID != customer.CustomerID)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CustomerCan("Customer_Edit_Self") && MonnitSession.CurrentCustomer.CustomerID == customer.CustomerID)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
      MonnitSession.CurrentCustomer.ClearPreferences();
    List<PreferenceType> model = PreferenceType.LoadUserAllowedByAccountThemeID(Account.Load(customer.AccountID).GetThemeID());
    foreach (PreferenceType preferenceType in model)
    {
      try
      {
        Preference preference = Preference.LoadByPreferenceTypeIDandCustomerID(preferenceType.PreferenceTypeID, customer.CustomerID);
        if (preference == null)
        {
          preference = new Preference();
          preference.PreferenceTypeID = preferenceType.PreferenceTypeID;
          preference.Value = preferenceType.DefaultValue;
          preference.CustomerID = customer.CustomerID;
        }
        preference.Value = collection[preferenceType.Name].ToString();
        preference.Save();
      }
      catch (Exception ex)
      {
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__23.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__23.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "prefMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = SettingsController.\u003C\u003Eo__23.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__23.\u003C\u003Ep__1, this.ViewBag, "Preference Failed to save: Preferencetype: " + preferenceType.DisplayName);
        ex.Log("Preference Failed to save: PreferencetypeID: " + preferenceType.PreferenceTypeID.ToString());
        return (ActionResult) this.View((object) model);
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__23.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__23.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "prefMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = SettingsController.\u003C\u003Eo__23.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__23.\u003C\u003Ep__2, this.ViewBag, "Success");
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult LocationSortPreferenceUpdate(int value)
  {
    string content;
    try
    {
      if (!MonnitSession.IsAuthorizedForAccount(MonnitSession.UserIsCustomerProxied ? MonnitSession.OldCustomer.AccountID : MonnitSession.CurrentCustomer.AccountID))
        return (ActionResult) this.Content("Not authorized");
      if (!MonnitSession.CustomerCan("Customer_Edit_Self"))
        return (ActionResult) this.Content("Not authorized");
      PreferenceType preferenceType = PreferenceType.Find("Location Sorting");
      Preference preference = Preference.LoadByPreferenceTypeIDandCustomerID(preferenceType.PreferenceTypeID, MonnitSession.CurrentCustomer.CustomerID);
      if (preference == null)
      {
        preference = new Preference();
        preference.PreferenceTypeID = preferenceType.PreferenceTypeID;
        preference.Value = preferenceType.DefaultValue;
        preference.CustomerID = MonnitSession.CurrentCustomer.CustomerID;
      }
      preference.Value = value.ToString();
      preference.Save();
      MonnitSession.CurrentCustomer.ClearPreferences();
      content = "Success";
    }
    catch (Exception ex)
    {
      content = "Preference failed to save";
      ex.Log($"SettingsController.LocationSortPreferenceUpdate[CustomerID: {MonnitSession.CurrentCustomer.CustomerID.ToString()}] ");
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public ActionResult UserDetail(long id)
  {
    Customer model = Customer.Load(id);
    if (model == null || model.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(model.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__0, this.ViewBag, "");
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__1, this.ViewBag, "");
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "PassMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__2, this.ViewBag, "");
    if (CSNetControllerBase.GetListOfNetworksUserCanSee(new long?(model.AccountID)).Count > 0)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "netID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__3.Target((CallSite) SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__3, this.ViewBag, CSNetControllerBase.GetListOfNetworksUserCanSee(new long?(model.AccountID))[0].CSNetID);
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "netID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj5 = SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__4.Target((CallSite) SettingsController.\u003C\u003Eo__25.\u003C\u003Ep__4, this.ViewBag, "");
    }
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult UserDetail(long id, FormCollection collection)
  {
    Customer customer = Customer.Load(id);
    if (customer == null || customer.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(customer.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__0, this.ViewBag, "");
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "PassMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__1, this.ViewBag, "");
    Account account = Account.Load(customer.AccountID);
    if (account == null)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__2, this.ViewBag, "Account Error");
      return (ActionResult) this.View((object) customer);
    }
    customer.LogAuditData(eAuditAction.Update, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited customer information");
    if (MonnitSession.CustomerCan("Customer_Change_Username"))
    {
      if (string.IsNullOrEmpty(collection["UserName"]))
        this.ModelState.AddModelError("Username", "Username is Required");
      else if (customer.UserName != collection["UserName"] && !Customer.CheckUsernameIsUnique(collection["UserName"]))
        this.ModelState.AddModelError("Username", "Username not available");
      if (customer.UserName != collection["UserName"] && customer.CustomerID != MonnitSession.CurrentCustomer.CustomerID)
        customer.ForceLogoutDate = DateTime.UtcNow;
      customer.UserName = collection["UserName"];
    }
    customer.FirstName = collection["FirstName"].SanitizeHTMLEvent().Trim();
    customer.LastName = collection["LastName"].SanitizeHTMLEvent().Trim();
    if (MonnitSession.CurrentCustomer.IsAdmin)
    {
      bool isAdmin = customer.IsAdmin;
      customer.IsAdmin = !string.IsNullOrEmpty(collection["IsAdmin"]) || customer.CustomerID == account.PrimaryContactID;
      if (customer.IsAdmin != isAdmin)
      {
        customer.ForceLogoutDate = DateTime.UtcNow;
        customer.ResetPermissions();
      }
      if (!customer.IsAdmin && Customer.PrimaryContactCheck(customer.CustomerID))
      {
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj4 = SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__3.Target((CallSite) SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__3, this.ViewBag, "Cannot remove admin while customer is assigned as a primary contact.");
        customer.IsAdmin = true;
        return (ActionResult) this.View((object) customer);
      }
    }
    if (account.SamlEndpointID > 0L)
    {
      this.ModelState.Remove("Password");
      customer.SamlNameID = collection["SamlNameID"];
      if (string.IsNullOrWhiteSpace(customer.SamlNameID))
      {
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj5 = SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__4.Target((CallSite) SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__4, this.ViewBag, "Failed to save, Name ID is Required.");
      }
      return (ActionResult) this.View((object) customer);
    }
    if (string.IsNullOrEmpty(customer.UserName))
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj6 = SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__5.Target((CallSite) SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__5, this.ViewBag, "Failed to save, username is required.");
      return (ActionResult) this.View((object) customer);
    }
    if (string.IsNullOrEmpty(customer.FirstName))
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj7 = SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__6.Target((CallSite) SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__6, this.ViewBag, "Failed to save, first name is required.");
      return (ActionResult) this.View((object) customer);
    }
    if (string.IsNullOrEmpty(customer.LastName))
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj8 = SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__7.Target((CallSite) SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__7, this.ViewBag, "Failed to save, last name is required.");
      return (ActionResult) this.View((object) customer);
    }
    if (this.ModelState.IsValid)
    {
      try
      {
        if (account.SamlEndpointID > 0L)
          customer.SamlNameID = collection["SamlNameID"];
        customer.Save();
        if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
          MonnitSession.CurrentCustomer = customer;
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj9 = SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__8.Target((CallSite) SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__8, this.ViewBag, "Success");
        return (ActionResult) this.View((object) customer);
      }
      catch (Exception ex)
      {
        this.ViewData.ModelState.AddModelError("", ex.Message);
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__9 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj10 = SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__9.Target((CallSite) SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__9, this.ViewBag, "Failed to save");
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__10 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj11 = SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__10.Target((CallSite) SettingsController.\u003C\u003Eo__26.\u003C\u003Ep__10, this.ViewBag, "Failed to save");
    return (ActionResult) this.View((object) customer);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult SAMLLoginTest(string endpointUrl, long accountID)
  {
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(accountID))
      return (ActionResult) this.Content("Failed");
    string content = "Failed";
    try
    {
      if (!string.IsNullOrWhiteSpace(endpointUrl) && accountID > 0L)
        content = new AuthRequest(ConfigData.AppSettings("SamlEntityID").Replace("{Domain}", MonnitSession.CurrentTheme.Domain), ConfigData.AppSettings("SamlConsumeTestURL").Replace("{Domain}", MonnitSession.CurrentTheme.Domain) + accountID.ToString()).GetRedirectUrl(endpointUrl);
    }
    catch (Exception ex)
    {
      ex.Log($"SettingsController.SAMLLoginTest[SamlEndpointURL: {endpointUrl}] ");
    }
    return (ActionResult) this.Content(content);
  }

  [HttpPost]
  public ActionResult SAMLConsumeTest(string id)
  {
    ActionResult actionResult;
    try
    {
      long ID = id.ToLong();
      Account account = Account.Load(ID);
      if (account != null && account.SamlEndpointID > 0L)
      {
        Saml.Response response = new Saml.Response(SamlEndpoint.Load(account.SamlEndpointID).Certificate, this.Request.Form["SAMLResponse"]);
        if (response.IsValid())
          actionResult = (ActionResult) this.Redirect($"/Settings/SamlSettings/{ID.ToString()}?samlTestMessage=Response was valid, the returned NameID was \"{response.GetNameID()}\"");
        else
          actionResult = (ActionResult) this.Redirect($"/Settings/SamlSettings/{ID.ToString()}?samlTestMessage=Response was invalid");
      }
      else
        actionResult = (ActionResult) this.Redirect($"/Settings/SamlSettings/{ID.ToString()}?samlTestMessage=No endpoint found on Account");
    }
    catch (Exception ex)
    {
      ex.Log("SettingsController.SAMLConsumeTest[Post] ");
      actionResult = (ActionResult) this.Redirect($"/Settings/SamlSettings/{id}?samlTestMessage=Failed, please contact support");
    }
    return actionResult;
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult RemoveAccountEndpoint(long id, long accountID)
  {
    string content = "Success";
    try
    {
      SamlEndpoint DBObject = SamlEndpoint.Load(id);
      if (DBObject == null || !MonnitSession.CurrentCustomer.CanSeeAccount(accountID))
        return (ActionResult) this.Content("Failed");
      Account.RemoveSamlEndPoint(accountID, id);
      DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, accountID, "Removed Endpoint from Account");
    }
    catch (Exception ex)
    {
      ex.Log($"SettingsController.RemoveAccountEndpoint[Post][SamlEndpointID: {id.ToString()}][AccountID: {accountID.ToString()}] ");
      content = "Failed: " + ex.Message;
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public ActionResult SamlSettings(long id, string samlTestMessage, string showTestMsg)
  {
    if (!MonnitSession.CustomerCan("Account_Edit") || !MonnitSession.IsAuthorizedForAccount(id))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    Account account = Account.Load(id);
    if (account == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    SamlEndpoint model = account.SamlEndpointID > 0L ? SamlEndpoint.Load(account.SamlEndpointID) : new SamlEndpoint();
    List<Customer> customerList = Customer.LoadAllByAccount(id);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__30.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__30.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<Customer>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "CustomerList", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__30.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__30.\u003C\u003Ep__0, this.ViewBag, customerList);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__30.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__30.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Account, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Account", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__30.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__30.\u003C\u003Ep__1, this.ViewBag, account);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__30.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__30.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SamlTestMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = SettingsController.\u003C\u003Eo__30.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__30.\u003C\u003Ep__2, this.ViewBag, samlTestMessage);
    bool result = false;
    bool.TryParse(showTestMsg, out result);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__30.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__30.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ShowTestMsg", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj4 = SettingsController.\u003C\u003Eo__30.\u003C\u003Ep__3.Target((CallSite) SettingsController.\u003C\u003Eo__30.\u003C\u003Ep__3, this.ViewBag, result);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult SamlSettings(long accountID, string name, string url, string certificate)
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
      ex.Log($"SettingsController.SamlSettings[Post][AccountID: {accountID.ToString()}][Name: {name}][Url: {url}][Certificate: {certificate}] ");
    }
    return (ActionResult) this.Content("Failed");
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult UpdateCustomerSamlNameID(long customerID, string samlNameID)
  {
    try
    {
      Customer DBObject = Customer.Load(customerID);
      if (DBObject != null && !MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
        return (ActionResult) this.Content("Unauthorized access");
      DBObject.SamlNameID = samlNameID;
      DBObject.Save();
      if (DBObject.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
        MonnitSession.CurrentCustomer = DBObject;
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Updated SamlNameID");
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log($"SettingsController.UpdateCustomerSamlNameID[Post][CustomerID: {customerID.ToString()}][SamlNameID: {samlNameID}] ");
    }
    return (ActionResult) this.Content("Failed");
  }

  [AuthorizeDefault]
  public ActionResult ByPass2FA(long id, bool bypass)
  {
    try
    {
      Customer customer = Customer.Load(id);
      if (customer == null || !MonnitSession.IsAuthorizedForAccount(customer.AccountID) || !MonnitSession.CurrentCustomer.IsAdmin)
        return (ActionResult) this.Content("Failed");
      customer.ByPass2FA = bypass;
      customer.Save();
      if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
        MonnitSession.CurrentCustomer = customer;
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult ByPass2FAPermanent(long id, bool bypass)
  {
    try
    {
      Customer customer = Customer.Load(id);
      if (customer == null || !MonnitSession.IsAuthorizedForAccount(customer.AccountID) || !MonnitSession.CurrentCustomer.IsAdmin)
        return (ActionResult) this.Content("Failed");
      customer.ByPass2FAPermanent = bypass;
      customer.Save();
      if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
        MonnitSession.CurrentCustomer = customer;
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult UserExportData(long id)
  {
    CustomerContactInfoModel contactInfoModel = new CustomerContactInfoModel();
    Customer model = Customer.Load(id);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__35.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__35.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__35.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__35.\u003C\u003Ep__0, this.ViewBag, "");
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__35.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__35.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__35.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__35.\u003C\u003Ep__1, this.ViewBag, "");
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__35.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__35.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "PassMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = SettingsController.\u003C\u003Eo__35.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__35.\u003C\u003Ep__2, this.ViewBag, "");
    if (model == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    contactInfoModel.Customer = Customer.Load(id);
    contactInfoModel.CustomerInformationList = CustomerInformation.LoadByCustomerID(id);
    if (!MonnitSession.IsAuthorizedForAccount(contactInfoModel.Customer.AccountID) || MonnitSession.CurrentCustomer.CustomerID != model.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Other"))
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__35.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__35.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = SettingsController.\u003C\u003Eo__35.\u003C\u003Ep__3.Target((CallSite) SettingsController.\u003C\u003Eo__35.\u003C\u003Ep__3, this.ViewBag, "Unauthorized!");
      return (ActionResult) this.View("UserDetail", (object) model);
    }
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("User Information as of {0}\r\n", (object) DateTime.Today.ToShortDateString());
    stringBuilder.AppendLine("Key,Value");
    stringBuilder.AppendFormat("FirstName,\"{0}\"\r\n", (object) model.FirstName);
    stringBuilder.AppendFormat("LastName,\"{0}\"\r\n", (object) model.LastName);
    stringBuilder.AppendFormat("Title,\"{0}\"\r\n", (object) model.Title);
    stringBuilder.AppendFormat("UserName,\"{0}\"\r\n", (object) model.UserName);
    stringBuilder.AppendFormat("NotificaitonEmail,\"{0}\"\r\n", (object) model.NotificationEmail);
    stringBuilder.AppendFormat("NotificationPhone1,\"{0}\"\r\n", (object) model.NotificationPhone);
    stringBuilder.AppendFormat("NotificationPhone2,\"{0}\"\r\n", (object) model.NotificationPhone2);
    if (contactInfoModel.CustomerInformationList.Count > 0)
    {
      foreach (CustomerInformation customerInformation in contactInfoModel.CustomerInformationList)
        stringBuilder.AppendFormat("\"{0}\",\"{1}\"\r\n", (object) customerInformation.InformationType.Name.Trim(), (object) customerInformation.Information.Trim());
    }
    if (model.SMSCarrier != null)
      stringBuilder.AppendFormat("SMS Carrier Name,\"{0}\"\r\n", (object) model.SMSCarrier.SMSCarrierName);
    return (ActionResult) this.File(Encoding.ASCII.GetBytes(stringBuilder.ToString()), "text/csv", $"UserData-{DateTime.Now.ToString("yyyyMMdd")}.csv");
  }

  [AuthorizeDefault]
  public ActionResult UserCreate(long id)
  {
    Account account = Account.Load(id);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(id))
      return this.PermissionError(ThemeController.PermissionErrorMessage.IsAuthorizedForAccount(id), methodName: nameof (UserCreate), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SettingsController.cs", sourceLineNumber: 1574);
    if (Customer.LoadAllByAccount(id).Count >= 1 && !MonnitSession.AccountCan("muliple_users"))
      return this.PermissionError(ThemeController.PermissionErrorMessage.AccountCanMulipleUsers(), methodName: nameof (UserCreate), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SettingsController.cs", sourceLineNumber: 1583);
    this.ViewData["Carriers"] = (object) new SelectList((IEnumerable) SMSAccount.SMSList(MonnitSession.CurrentTheme.AccountThemeID), "SMSCarrierID", "SMSCarrierName");
    this.ViewData["AccountID"] = (object) id;
    if (account != null && account.SamlEndpointID > 0L)
    {
      SamlEndpoint samlEndpoint = SamlEndpoint.Load(account.SamlEndpointID);
      this.ViewData["SamlEndpointID"] = (object) account.SamlEndpointID;
      this.ViewData["SamlEndpointName"] = (object) samlEndpoint.Name;
    }
    return (ActionResult) this.View((object) new Customer());
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult UserCreate(Customer customer)
  {
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(customer.AccountID))
      return this.PermissionError(ThemeController.PermissionErrorMessage.IsAuthorizedForAccount(customer.AccountID), methodName: nameof (UserCreate), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SettingsController.cs", sourceLineNumber: 1607);
    if (Customer.LoadAllByAccount(customer.AccountID).Count >= 1 && !MonnitSession.AccountCan("muliple_users"))
      return this.PermissionError(ThemeController.PermissionErrorMessage.AccountCanMulipleUsers(), methodName: nameof (UserCreate), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SettingsController.cs", sourceLineNumber: 1616);
    if (!Customer.CheckUsernameIsUnique(customer.UserName))
      this.ModelState.AddModelError("Username", "Username not available");
    if (!Customer.CheckNotificationEmailIsUnique(customer.NotificationEmail))
      this.ModelState.AddModelError("NotificationEmail", "Email address not available");
    Account account = Account.Load(customer.AccountID);
    if (account != null && account.SamlEndpointID == long.MinValue && !MonnitUtil.IsValidPassword(customer.Password, MonnitSession.CurrentTheme))
      this.ModelState.AddModelError("Password", MonnitUtil.PasswordHelperString(MonnitSession.CurrentTheme));
    if (account != null && account.SamlEndpointID > 0L)
    {
      this.ModelState.Remove("Password");
      this.ModelState.Remove("ConfirmPassword");
      SamlEndpoint samlEndpoint = SamlEndpoint.Load(account.SamlEndpointID);
      this.ViewData["SamlEndpointID"] = (object) account.SamlEndpointID;
      this.ViewData["SamlEndpointName"] = (object) samlEndpoint.Name;
      if (string.IsNullOrWhiteSpace(customer.SamlNameID))
        this.ModelState.AddModelError("SamlNameID", "Name ID is Required!");
    }
    if (this.ModelState.IsValid)
    {
      try
      {
        if (account != null && account.SamlEndpointID == long.MinValue)
          customer.Password = MonnitSession.UseEncryption ? customer.Password.Encrypt() : customer.Password;
        customer.Save();
        customer.LogAuditData(eAuditAction.Create, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, customer.AccountID, "Created a new customer");
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__37.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__37.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = SettingsController.\u003C\u003Eo__37.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__37.\u003C\u003Ep__0, this.ViewBag, "");
        this.ViewData["Carriers"] = (object) new SelectList((IEnumerable) SMSAccount.SMSList(MonnitSession.CurrentTheme.AccountThemeID), "SMSCarrierID", "SMSCarrierName", (object) customer.SMSCarrierID);
        return (ActionResult) this.View("UserNotification", (object) customer);
      }
      catch (Exception ex)
      {
        ex.Log("SetingsController.UserCreate | CustomerID = " + (customer == null ? "null" : customer.CustomerID.ToString()));
        this.ViewData["AccountID"] = (object) customer.AccountID;
        return (ActionResult) this.View((object) customer);
      }
    }
    else
    {
      this.ViewData["AccountID"] = (object) customer.AccountID;
      return (ActionResult) this.View((object) customer);
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult UserDelete(long id, long accountID)
  {
    try
    {
      Customer DBObject = Customer.Load(id);
      if (!MonnitSession.IsAuthorizedForAccount(DBObject.AccountID) && !MonnitSession.IsLinkedToThisAccount(DBObject.CustomerID, accountID) || !MonnitSession.CurrentCustomer.IsAdmin && MonnitSession.CurrentCustomer.CustomerID != DBObject.CustomerID)
        return (ActionResult) this.Content("Failed");
      Account account = Account.Load(accountID);
      if (account == null)
        return (ActionResult) this.Content("Failed");
      if (DBObject.AccountID == accountID)
      {
        DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Deleted customer");
        DBObject.Delete();
        if (MonnitSession.CurrentCustomer.CustomerID != DBObject.CustomerID)
          return (ActionResult) this.Content("Success");
        OverviewControllerBase.ClearRememberMeCookie(System.Web.HttpContext.Current.Request);
        FormsAuthentication.SignOut();
        this.Session.Clear();
        this.Session.Abandon();
        return (ActionResult) this.Content("Logout");
      }
      foreach (CustomerAccountLink customerAccountLink in CustomerAccountLink.LoadAllByCustomerID(DBObject.CustomerID))
      {
        if (customerAccountLink.AccountID == accountID)
        {
          DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Deleted customer from linked accounts");
          customerAccountLink.AccountDeleted = true;
          customerAccountLink.DateAccountDeleted = DateTime.UtcNow;
          customerAccountLink.Save();
          return (ActionResult) this.Content("Success");
        }
      }
      return (ActionResult) this.Content("Failed");
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
      return (ActionResult) this.Content("Failed: " + ex.Message);
    }
  }

  [AuthorizeDefault]
  public ActionResult UserAccountLinking(long id)
  {
    Customer model = Customer.Load(id);
    return model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID) || !MonnitSession.CustomerCan("Customer_Admin_AccountLink") ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult AdminLinkAccountList(long id, string searchCriteria)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_Administration") && !MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Settings",
        id = MonnitSession.CurrentCustomer.AccountID
      });
    Customer customer = Customer.Load(id);
    if (customer == null || !MonnitSession.IsAuthorizedForAccount(customer.AccountID) || !MonnitSession.CustomerCan("Customer_Admin_AccountLink"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    AdminAccountLinkModel model = new AdminAccountLinkModel();
    model.searchModel = new List<AccountSearchModel>();
    model.customerAccountLink = new List<CustomerAccountLink>();
    searchCriteria = WebUtility.HtmlEncode(searchCriteria);
    if (searchCriteria.Length < 1)
      return (ActionResult) this.View((object) model);
    if (searchCriteria.ToLower() == "Show All".ToLower())
      searchCriteria = "";
    model.searchModel = AccountSearchModel.Search(MonnitSession.CurrentCustomer.CustomerID, searchCriteria, 100).Where<AccountSearchModel>((System.Func<AccountSearchModel, bool>) (c => c.AccountID != customer.AccountID)).ToList<AccountSearchModel>();
    model.customerAccountLink = CustomerAccountLink.LoadAllByCustomerID(id).Where<CustomerAccountLink>((System.Func<CustomerAccountLink, bool>) (c => c.AccountID != customer.AccountID)).ToList<CustomerAccountLink>();
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ToggleAccountLink(long id, long accountID, bool add)
  {
    Customer customer = Customer.Load(id);
    if (customer == null || !MonnitSession.CurrentCustomer.CanSeeAccount(customer.AccountID) || !MonnitSession.CurrentCustomer.CanSeeAccount(accountID))
      return (ActionResult) this.Content("Failed: Unable to Link User.");
    if (add)
    {
      try
      {
        CustomerAccountLink DBObject1 = CustomerAccountLink.Load(customer.CustomerID, accountID);
        Account account = Account.Load(accountID);
        if (DBObject1 == null)
        {
          CustomerAccountLink DBObject2 = new CustomerAccountLink();
          DBObject2.AccountDeleted = false;
          DBObject2.AccountID = accountID;
          DBObject2.CustomerDeleted = false;
          DBObject2.CustomerID = customer.CustomerID;
          DBObject2.DateAccountDeleted = DateTime.MinValue;
          DBObject2.DateUserAdded = DateTime.UtcNow;
          DBObject2.DateUserDeleted = DateTime.MinValue;
          DBObject2.RequestConfirmed = true;
          DBObject2.GUID = Guid.NewGuid().ToString();
          DBObject2.Save();
          DBObject2.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Add existing user to account");
          return (ActionResult) this.Content("Success");
        }
        if (!DBObject1.AccountDeleted && !DBObject1.CustomerDeleted)
          return (ActionResult) this.Content("failed");
        DBObject1.CustomerDeleted = false;
        DBObject1.AccountDeleted = false;
        DBObject1.RequestConfirmed = true;
        DBObject1.Save();
        DBObject1.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Add existing user to account");
        return (ActionResult) this.Content("Success");
      }
      catch
      {
        return (ActionResult) this.Content("failed");
      }
    }
    else
    {
      CustomerAccountLink DBObject3 = CustomerAccountLink.Load(id, accountID);
      try
      {
        DBObject3.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, accountID, "Removed customer account link");
        DBObject3.AccountDeleted = true;
        DBObject3.CustomerDeleted = true;
        DBObject3.DateAccountDeleted = DateTime.UtcNow;
        DBObject3.Save();
        foreach (Notification DBObject4 in Notification.LoadByAccountID(accountID))
        {
          foreach (NotificationRecipient notificationRecipient in DBObject4.NotificationRecipients)
          {
            if (notificationRecipient.CustomerToNotifyID == customer.CustomerID)
            {
              DBObject4.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, accountID, "Removed customer from notification");
              DBObject4.RemoveCustomer(customer);
            }
          }
        }
        return (ActionResult) this.Content("Success");
      }
      catch (Exception ex)
      {
        ExceptionLog.Log(ex);
        return (ActionResult) this.Content("Failed");
      }
    }
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult UserRemoveLink(long id, long accountid)
  {
    try
    {
      CustomerAccountLink DBObject1 = CustomerAccountLink.Load(id, accountid);
      Customer customer = Customer.Load(id);
      Account account = Account.Load(accountid);
      DBObject1.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Removed customer account link");
      DBObject1.AccountDeleted = true;
      DBObject1.DateAccountDeleted = DateTime.UtcNow;
      DBObject1.Save();
      foreach (Notification DBObject2 in Notification.LoadByAccountID(account.AccountID))
      {
        foreach (NotificationRecipient notificationRecipient in DBObject2.NotificationRecipients)
        {
          if (notificationRecipient.CustomerToNotifyID == customer.CustomerID)
          {
            DBObject2.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Removed customer from notification");
            DBObject2.RemoveCustomer(customer);
          }
        }
      }
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult EditAccessToken(long id, string action)
  {
    Account account = Account.Load(id);
    if (account == null || !MonnitSession.IsAuthorizedForAccount(account.AccountID))
      return (ActionResult) this.Content("Failed");
    try
    {
      switch (action)
      {
        case "create":
          account.AccessToken = ThemeController.GenerateAccessToken();
          account.AccessTokenExpirationDate = DateTime.UtcNow.AddDays(1.0);
          break;
        case "revoke":
          account.AccessTokenExpirationDate = DateTime.UtcNow.AddDays(-1.0);
          break;
        case "extend":
          if (DateTime.UtcNow.AddDays(8.0) > account.AccessTokenExpirationDate)
          {
            account.AccessTokenExpirationDate = DateTime.UtcNow.AddDays(8.0);
            break;
          }
          break;
      }
      account.Save();
      MonnitSession.CurrentCustomer.Account = (Account) null;
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log($"Account access Token {action} failed. message: {ex.Message}");
    }
    return (ActionResult) this.Content("Failed");
  }

  [AuthorizeDefault]
  public ActionResult CheckAccessToken(long id, string token)
  {
    Account account = Account.Load(id);
    if (account == null || !MonnitSession.IsAuthorizedForAccount(account.AccountID))
      return (ActionResult) this.Content("Failed");
    try
    {
      token = token.Trim();
      if (token.ToUpper() == account.AccessToken && account.AccessTokenExpirationDate > DateTime.Now)
        return (ActionResult) this.Content("Success");
    }
    catch
    {
    }
    return (ActionResult) this.Content("Failed");
  }

  [AuthorizeDefault]
  public ActionResult AccountUserList(long? id)
  {
    if (!MonnitSession.CustomerCan("Customer_Edit_Other"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__45.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__45.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, long, object> target = SettingsController.\u003C\u003Eo__45.\u003C\u003Ep__0.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, long, object>> p0 = SettingsController.\u003C\u003Eo__45.\u003C\u003Ep__0;
    object viewBag = this.ViewBag;
    long? nullable = id;
    long num = nullable ?? MonnitSession.CurrentCustomer.AccountID;
    object obj1 = target((CallSite) p0, viewBag, num);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__45.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__45.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserCount", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__45.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__45.\u003C\u003Ep__1, this.ViewBag, 0);
    long accountId = MonnitSession.CurrentCustomer.AccountID;
    nullable = id;
    long valueOrDefault = nullable.GetValueOrDefault();
    if (accountId == valueOrDefault & nullable.HasValue)
      return (ActionResult) this.View((object) MonnitSession.CurrentCustomer.Account);
    if (MonnitSession.CustomerCan("Navigation_View_Administration"))
    {
      nullable = id;
      Account model = Account.Load(nullable ?? MonnitSession.CurrentCustomer.AccountID);
      if (model != null && MonnitSession.IsAuthorizedForAccount(model.AccountID))
        return (ActionResult) this.View((object) model);
    }
    return (ActionResult) this.View((object) MonnitSession.CurrentCustomer.Account);
  }

  [AuthorizeDefault]
  public ActionResult UserGroupList(long id)
  {
    Account account = Account.Load(id);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__46.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__46.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Account, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "account", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__46.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__46.\u003C\u003Ep__0, this.ViewBag, account);
    List<CustomerGroup> model = CustomerGroup.LoadByAccountID(account.AccountID);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__46.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__46.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserGroupCount", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__46.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__46.\u003C\u003Ep__1, this.ViewBag, model.Count);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult UserGroupFilter(string q)
  {
    try
    {
      Account account = MonnitSession.CurrentCustomer.Account;
      if (account == null)
        return (ActionResult) this.Content("Failed");
      List<CustomerGroup> customerGroupList = CustomerGroup.LoadByAccountID(account.AccountID);
      if (!string.IsNullOrEmpty(q))
        customerGroupList = customerGroupList.Where<CustomerGroup>((System.Func<CustomerGroup, bool>) (s => s.Name.ToLower().Contains(q.ToLower()) || s.Name.ToString().ToLower().Contains(q.ToLower()))).ToList<CustomerGroup>();
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__47.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__47.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserGroupCount", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__47.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__47.\u003C\u003Ep__0, this.ViewBag, customerGroupList.Count);
      return (ActionResult) this.PartialView("UserGroupDetails", (object) customerGroupList);
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult UserGroupEdit(long? id)
  {
    CustomerGroup model = new CustomerGroup();
    if (id.HasValue)
    {
      model = CustomerGroup.Load(id.Value);
      if (model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index",
          controller = "Overview"
        });
    }
    List<Customer> customerList = new List<Customer>();
    if (model != null)
    {
      customerList = Customer.LoadAllByAccount(model.AccountID);
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__48.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__48.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "GroupID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__48.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__48.\u003C\u003Ep__0, this.ViewBag, model.CustomerGroupID);
    }
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__48.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__48.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<Customer>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AssignedCustomers", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__48.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__48.\u003C\u003Ep__1, this.ViewBag, customerList);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__48.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__48.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "message", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__48.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__48.\u003C\u003Ep__2, this.ViewBag, "");
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__48.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__48.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "GroupID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = SettingsController.\u003C\u003Eo__48.\u003C\u003Ep__3.Target((CallSite) SettingsController.\u003C\u003Eo__48.\u003C\u003Ep__3, this.ViewBag, model.CustomerGroupID);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [ValidateAntiForgeryToken]
  [HttpPost]
  public ActionResult UserGroupEdit(long id, string userGroupName, string jsonValues)
  {
    CustomerGroup customerGroup = CustomerGroup.Load(id);
    if (customerGroup == null)
    {
      customerGroup = new CustomerGroup();
      customerGroup.AccountID = MonnitSession.CurrentCustomer.AccountID;
    }
    foreach (CustomerGroupLink customerGroupLink in CustomerGroupLink.LoadByCustomerGroupID(id))
    {
      foreach (BaseDBObject baseDbObject in CustomerGroupRecipient.LoadByCustomerGroupLinkID(customerGroupLink.CustomerGroupLinkID))
        baseDbObject.Delete();
      customerGroupLink.Delete();
    }
    List<SettingsController.GroupItem> groupItemList = JsonConvert.DeserializeObject<List<SettingsController.GroupItem>>(jsonValues);
    customerGroup.Name = userGroupName;
    foreach (SettingsController.GroupItem groupItem in groupItemList)
    {
      if (groupItem.Email != null && groupItem.Email.ToInt() >= 0)
        customerGroup.AddCustomer(groupItem.Id, groupItem.Email.ToInt(), eNotificationType.Email);
      if (groupItem.Text != null && groupItem.Text.ToInt() >= 0)
        customerGroup.AddCustomer(groupItem.Id, groupItem.Text.ToInt(), eNotificationType.SMS);
      if (groupItem.Voice != null && groupItem.Voice.ToInt() >= 0)
        customerGroup.AddCustomer(groupItem.Id, groupItem.Voice.ToInt(), eNotificationType.Phone);
      if (groupItem.Push != null && groupItem.Push.ToInt() >= 0)
        customerGroup.AddCustomer(groupItem.Id, groupItem.Push.ToInt(), eNotificationType.Push_Message);
    }
    customerGroup.Save();
    List<Customer> customerList = Customer.LoadByCustomerGroupID(customerGroup.CustomerGroupID);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__50.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__50.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<Customer>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AssignedCustomers", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__50.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__50.\u003C\u003Ep__0, this.ViewBag, customerList);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__50.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__50.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "GroupID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__50.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__50.\u003C\u003Ep__1, this.ViewBag, customerGroup.CustomerGroupID);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__50.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__50.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "message", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = SettingsController.\u003C\u003Eo__50.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__50.\u003C\u003Ep__2, this.ViewBag, "Success");
    return (ActionResult) this.Content(customerGroup.CustomerGroupID.ToString());
  }

  [AuthorizeDefault]
  public ActionResult UserGroupDelete(long id)
  {
    try
    {
      CustomerGroup customerGroup = CustomerGroup.Load(id);
      if (customerGroup == null)
        return (ActionResult) this.Content("Success");
      if (!MonnitSession.CurrentCustomer.CanSeeAccount(customerGroup.AccountID))
        return (ActionResult) this.Content("Failed");
      customerGroup.Delete();
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log("Settings.UserGroupDelete | CustomerGroup Failed to Delete");
    }
    return (ActionResult) this.Content("Failed");
  }

  [AuthorizeDefault]
  [ValidateAntiForgeryToken]
  [HttpPost]
  public ActionResult ToggleUserGroupUsers(
    long id,
    long customerID,
    eNotificationType notificationType,
    bool add,
    int? delayMinutes)
  {
    CustomerGroup DBObject = CustomerGroup.Load(id);
    if (DBObject == null)
      return (ActionResult) this.Content("Group not found");
    if (!MonnitSession.IsAuthorizedForAccount(DBObject.AccountID))
      return (ActionResult) this.Content("Failed: Not allowed for this account");
    if (delayMinutes.GetValueOrDefault() == -1)
      add = false;
    int num1;
    if (!add)
    {
      int? nullable = delayMinutes;
      int num2 = 0;
      num1 = nullable.GetValueOrDefault() >= num2 & nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 1;
    if (num1 != 0)
    {
      Customer customer = Customer.Load(customerID);
      if (customer == null)
        return (ActionResult) this.Content("User not found");
      if (DBObject.AccountID != customer.AccountID && CustomerAccountLink.Load(customer.CustomerID, DBObject.AccountID) == null)
      {
        bool flag = false;
        foreach (Account ancestor in Account.Ancestors(MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID))
        {
          if (ancestor.AccountID == customer.AccountID)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          return (ActionResult) this.Content("User not found");
      }
      string changeRecord = $"{{\"CustomerID\": \"{customer.CustomerID}\", \"CustomerGroupID\" : \"{DBObject.CustomerGroupID}\", \"DelayMinutes\" : \"{delayMinutes ?? int.MinValue}\", \"NotifcationType\" : \"{notificationType.ToString()}\" }} ";
      DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.CustomerGroup, MonnitSession.CurrentCustomer.CustomerID, changeRecord, DBObject.AccountID, "Assigned recipient to notification");
      DBObject.AddCustomer(customer.CustomerID, delayMinutes.GetValueOrDefault(), notificationType);
    }
    else
    {
      foreach (CustomerGroupLink customerGroupLink in CustomerGroupLink.LoadByCustomerGroupID(DBObject.CustomerGroupID).Where<CustomerGroupLink>((System.Func<CustomerGroupLink, bool>) (nr => nr.CustomerID == customerID && nr.NotificationType == notificationType)).ToList<CustomerGroupLink>())
      {
        string changeRecord = $"{{\"CustomerID\": \"{customerGroupLink.CustomerID}\", \"CustomerGroupID\" : \"{DBObject.CustomerGroupID}\" }} ";
        DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, DBObject.AccountID, "Removed recipient from notification");
        DBObject.RemoveCustomer(customerGroupLink.CustomerID, notificationType);
      }
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult UserGroupUserFilter(long id, long groupID, int? userType, string q)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_My_Account"))
      return (ActionResult) this.Content("Failed");
    List<Customer> customerList1 = Customer.LoadAllByAccount(id);
    if (customerList1.Count < 1)
      return (ActionResult) this.Content("Failed");
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserTotal", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__0, this.ViewBag, customerList1.Count);
    List<Customer> customerList2 = Customer.LoadByCustomerGroupID(groupID);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<Customer>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AssignedCustomers", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__1, this.ViewBag, customerList2);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "GroupID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__2, this.ViewBag, groupID);
    int num1;
    if (userType.HasValue)
    {
      int? nullable = userType;
      int num2 = 0;
      num1 = nullable.GetValueOrDefault() > num2 & nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    if (num1 != 0)
    {
      int? nullable = userType;
      if (nullable.HasValue)
      {
        switch (nullable.GetValueOrDefault())
        {
          case 1:
            customerList1 = customerList1.Where<Customer>((System.Func<Customer, bool>) (n => n.IsAdmin)).ToList<Customer>();
            break;
          case 2:
            customerList1 = customerList1.Where<Customer>((System.Func<Customer, bool>) (n => !n.IsAdmin)).ToList<Customer>();
            break;
          case 3:
            customerList1 = customerList1.Where<Customer>((System.Func<Customer, bool>) (s => s.AccountID != id)).ToList<Customer>();
            break;
        }
      }
    }
    if (!string.IsNullOrEmpty(q))
      customerList1 = customerList1.Where<Customer>((System.Func<Customer, bool>) (s => s.NotificationEmail.ToLower().Contains(q.ToLower()) || s.FirstName.ToLower().Contains(q.ToLower()) || s.LastName.ToString().ToLower().Contains(q.ToLower()))).ToList<Customer>();
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj4 = SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__3.Target((CallSite) SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__3, this.ViewBag, id);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserCount", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj5 = SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__4.Target((CallSite) SettingsController.\u003C\u003Eo__53.\u003C\u003Ep__4, this.ViewBag, customerList1.Count);
    return (ActionResult) this.PartialView("UserGroupUserListDetails", (object) customerList1);
  }

  [AuthorizeDefault]
  public ActionResult UserFilter(long id, int? userType, string q)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_My_Account"))
      return (ActionResult) this.Content("Failed");
    List<Customer> customerList = Customer.LoadAllByAccount(id);
    if (customerList.Count < 1)
      return (ActionResult) this.Content("Failed");
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__54.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__54.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserTotal", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__54.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__54.\u003C\u003Ep__0, this.ViewBag, customerList.Count);
    int num1;
    if (userType.HasValue)
    {
      int? nullable = userType;
      int num2 = 0;
      num1 = nullable.GetValueOrDefault() > num2 & nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    if (num1 != 0)
    {
      int? nullable = userType;
      if (nullable.HasValue)
      {
        switch (nullable.GetValueOrDefault())
        {
          case 1:
            customerList = customerList.Where<Customer>((System.Func<Customer, bool>) (n => n.IsAdmin)).ToList<Customer>();
            break;
          case 2:
            customerList = customerList.Where<Customer>((System.Func<Customer, bool>) (n => !n.IsAdmin)).ToList<Customer>();
            break;
          case 3:
            customerList = customerList.Where<Customer>((System.Func<Customer, bool>) (s => s.AccountID != id)).ToList<Customer>();
            break;
        }
      }
    }
    if (!string.IsNullOrEmpty(q))
      customerList = customerList.Where<Customer>((System.Func<Customer, bool>) (s => s.NotificationEmail.ToLower().Contains(q.ToLower()) || s.FirstName.ToLower().Contains(q.ToLower()) || s.LastName.ToString().ToLower().Contains(q.ToLower()))).ToList<Customer>();
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__54.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__54.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__54.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__54.\u003C\u003Ep__1, this.ViewBag, id);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__54.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__54.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserCount", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = SettingsController.\u003C\u003Eo__54.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__54.\u003C\u003Ep__2, this.ViewBag, customerList.Count);
    return (ActionResult) this.PartialView("UserListDetails", (object) customerList);
  }

  [AuthorizeDefault]
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

  [AuthorizeDefault]
  public ActionResult AccountLinkUser(long id)
  {
    return (ActionResult) this.View((object) new AddExistingUserAccountModel()
    {
      AccountID = id
    });
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult AccountLinkUser(AddExistingUserAccountModel model)
  {
    Customer customer = Customer.LoadAllByEmail(model.email).FirstOrDefault<Customer>();
    if (!model.email.IsValidEmail())
    {
      this.ModelState.AddModelError("email", "Invalid Email");
      return (ActionResult) this.Content("Input must be an email");
    }
    if (customer == null)
    {
      this.ModelState.AddModelError("email", "Invalid Email");
      return (ActionResult) this.Content("User not found");
    }
    if (!customer.Account.CurrentSubscription.Can(Feature.Find("link_users")))
    {
      this.ModelState.AddModelError("email", "Both accounts must be premiere");
      return (ActionResult) this.Content("Both accounts must be premiere");
    }
    if (model.AccountID < 1L)
    {
      this.ModelState.AddModelError("email", "Unknown Account");
      return (ActionResult) this.Content("Unknown Account Email");
    }
    if (!this.ModelState.IsValid)
      return (ActionResult) this.Content(this.ModelState.Values.SelectMany<System.Web.Mvc.ModelState, ModelError>((System.Func<System.Web.Mvc.ModelState, IEnumerable<ModelError>>) (v => (IEnumerable<ModelError>) v.Errors)).FirstOrDefault<ModelError>()?.ErrorMessage ?? "Invalid Email");
    try
    {
      CustomerAccountLink DBObject1 = CustomerAccountLink.Load(customer.CustomerID, model.AccountID);
      Account account = Account.Load(customer.AccountID);
      if (DBObject1 == null)
      {
        CustomerAccountLink DBObject2 = new CustomerAccountLink();
        DBObject2.AccountDeleted = false;
        DBObject2.AccountID = model.AccountID;
        DBObject2.CustomerDeleted = false;
        DBObject2.CustomerID = customer.CustomerID;
        DBObject2.DateAccountDeleted = DateTime.MinValue;
        DBObject2.DateUserAdded = DateTime.UtcNow;
        DBObject2.DateUserDeleted = DateTime.MinValue;
        DBObject2.RequestConfirmed = false;
        DBObject2.GUID = Guid.NewGuid().ToString();
        DBObject2.Save();
        DBObject2.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Add existing user to account");
        return (ActionResult) this.Content("Success");
      }
      if (!DBObject1.AccountDeleted && !DBObject1.CustomerDeleted)
        return (ActionResult) this.Content("User has already been added to account.");
      DBObject1.CustomerDeleted = false;
      DBObject1.AccountDeleted = false;
      DBObject1.RequestConfirmed = false;
      DBObject1.Save();
      DBObject1.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Add existing user to account");
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult AccountEdit(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin && (!MonnitSession.CustomerCan("Account_Edit") || !MonnitSession.CustomerCan("Navigation_View_My_Account") || !MonnitSession.IsAuthorizedForAccount(id)))
    {
      if (!MonnitSession.CustomerCan("Account_Edit"))
        return this.PermissionError(ThemeController.PermissionErrorMessage.MissingCustomerPermission("Account_Edit"), methodName: nameof (AccountEdit), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SettingsController.cs", sourceLineNumber: 2554);
      if (!MonnitSession.CustomerCan("Navigation_View_My_Account"))
        return this.PermissionError(ThemeController.PermissionErrorMessage.MissingCustomerPermission("Navigation_View_My_Account"), methodName: nameof (AccountEdit), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SettingsController.cs", sourceLineNumber: 2559 /*0x09FF*/);
      return !MonnitSession.IsAuthorizedForAccount(id) ? this.PermissionError(ThemeController.PermissionErrorMessage.IsAuthorizedForAccount(id), methodName: nameof (AccountEdit), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SettingsController.cs", sourceLineNumber: 2564) : (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        controller = "Overview",
        action = "Index"
      });
    }
    Account model = Account.Load(id) ?? MonnitSession.CurrentCustomer.Account;
    Monnit.TimeZone timeZone = Monnit.TimeZone.Load(model.TimeZoneID);
    this.ViewData["Region"] = (object) timeZone.Region;
    this.ViewData["Regions"] = (object) new SelectList((IEnumerable) Monnit.TimeZone.LoadRegions(), (object) timeZone.Region);
    this.ViewData["TimeZones"] = (object) new SelectList((IEnumerable) Monnit.TimeZone.LoadByRegion(timeZone.Region), "TimeZoneID", "DisplayName");
    this.ViewData["PrimaryContacts"] = (object) Customer.LoadAllForReseller(MonnitSession.CurrentCustomer.CustomerID, model.AccountID).Where<Customer>((System.Func<Customer, bool>) (c => c.IsAdmin)).ToList<Customer>();
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult AccountEdit(long id, FormCollection collection)
  {
    try
    {
      if (string.IsNullOrEmpty(collection["CompanyName"].ToString()))
        return (ActionResult) this.Content(((HtmlHelper) null).TranslateTag("Company Name is required"));
      if (string.IsNullOrEmpty(collection["AccountNumber"].ToString()))
        return (ActionResult) this.Content(((HtmlHelper) null).TranslateTag("Account Number is required"));
      Account account = Account.Load(id);
      if (!MonnitSession.CustomerCan("Account_Edit") || !MonnitSession.CustomerCan("Navigation_View_My_Account") || account == null || !MonnitSession.IsAuthorizedForAccount(id))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          controller = "Overview",
          action = "Index"
        });
      Monnit.TimeZone timeZone = Monnit.TimeZone.Load(account.TimeZoneID);
      this.ViewData["Region"] = (object) timeZone.Region;
      this.ViewData["Regions"] = (object) new SelectList((IEnumerable) Monnit.TimeZone.LoadRegions(), (object) timeZone.Region);
      this.ViewData["TimeZones"] = (object) new SelectList((IEnumerable) Monnit.TimeZone.LoadByRegion(timeZone.Region), "TimeZoneID", "DisplayName");
      long primaryContactId = account.PrimaryContactID;
      bool isPremium = account.IsPremium;
      this.UpdateModel<Account>(account);
      if (!this.ModelState.IsValid)
        return (ActionResult) this.Content("Invalid Data");
      try
      {
        account.LogAuditData(eAuditAction.Update, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited account settings");
        if (primaryContactId != account.PrimaryContactID)
        {
          if (!isPremium)
          {
            account.PrimaryContact.IsActive = true;
            Customer customer = Customer.Load(primaryContactId);
            if (customer.AccountID == account.AccountID)
              customer.IsActive = false;
            customer.ForceLogoutDate = DateTime.UtcNow;
            customer.Save();
            if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
              MonnitSession.CurrentCustomer = customer;
          }
          account.PrimaryContact.Save();
          if (account.PrimaryContact.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
            MonnitSession.CurrentCustomer = account.PrimaryContact;
        }
        account.PrimaryAddress.Save();
        account.Save();
        if (account.AccountID == MonnitSession.CurrentCustomer.AccountID)
          MonnitSession.CurrentCustomer.Account = account;
        if (MonnitSession.IsCurrentCustomerMonnitAdmin)
        {
          foreach (AccountPermissionType accountPermissionType in AccountPermissionType.LoadAll())
          {
            string name = $"Permission_{accountPermissionType.Name.Replace(" ", "_")}";
            if (accountPermissionType.CanEdit(MonnitSession.CurrentCustomer.IsAdmin, MonnitSession.IsCurrentCustomerMonnitAdmin))
            {
              AccountPermission accountPermission = new AccountPermission();
              foreach (AccountPermission permission in account.Permissions)
              {
                if (permission.AccountPermissionTypeID == accountPermissionType.AccountPermissionTypeID)
                  accountPermission = permission;
              }
              accountPermission.AccountID = account.AccountID;
              accountPermission.AccountPermissionTypeID = accountPermissionType.AccountPermissionTypeID;
              accountPermission.Can = collection[name] == "on";
              if (accountPermissionType.RequiresInfo)
                accountPermission.Info = collection[name + "_Info"];
              accountPermission.OverrideCustomerPermission = collection[name + "_Override"] == "on";
              accountPermission.Save();
            }
          }
          account.Permissions = (List<AccountPermission>) null;
        }
        return (ActionResult) this.Content("Success");
      }
      catch (Exception ex)
      {
        this.ModelState.AddModelError("", ex.Message);
        return (ActionResult) this.Content(ex.Message);
      }
    }
    catch
    {
      return (ActionResult) this.Content("Unknown Error");
    }
  }

  [AuthorizeDefault]
  public ActionResult AccountPreference(long id)
  {
    if (!MonnitSession.CustomerCan("Account_Edit") || !MonnitSession.IsAuthorizedForAccount(id))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__60.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__60.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "prefMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__60.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__60.\u003C\u003Ep__0, this.ViewBag, "");
    Account account = Account.Load(id);
    if (account == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__60.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__60.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Account, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Account", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__60.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__60.\u003C\u003Ep__1, this.ViewBag, account);
    return (ActionResult) this.View((object) PreferenceType.LoadAccountAllowedByAccountThemeID(account.GetThemeID()));
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult AccountPreference(long id, FormCollection collection)
  {
    if (!MonnitSession.CustomerCan("Account_Edit") || !MonnitSession.IsAuthorizedForAccount(id))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    Account account = Account.Load(id);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__61.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__61.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Account, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Account", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__61.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__61.\u003C\u003Ep__0, this.ViewBag, account);
    if (account == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    List<PreferenceType> model = PreferenceType.LoadAccountAllowedByAccountThemeID(account.GetThemeID());
    foreach (PreferenceType preferenceType in model)
    {
      try
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
      catch (Exception ex)
      {
        long num = account.AccountID;
        string function = "Account preference edit failed: accountID: " + num.ToString();
        ex.Log(function);
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__61.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__61.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "prefMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, string, object> target = SettingsController.\u003C\u003Eo__61.\u003C\u003Ep__1.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, string, object>> p1 = SettingsController.\u003C\u003Eo__61.\u003C\u003Ep__1;
        object viewBag = this.ViewBag;
        num = preferenceType.PreferenceTypeID;
        string str = "Preference Failed to save: PreferencetypeID: " + num.ToString();
        object obj2 = target((CallSite) p1, viewBag, str);
        return (ActionResult) this.View((object) model);
      }
    }
    MonnitSession.CurrentCustomer.ClearPreferences();
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__61.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__61.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "prefMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = SettingsController.\u003C\u003Eo__61.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__61.\u003C\u003Ep__2, this.ViewBag, "Success");
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult AccountLinkList()
  {
    List<CustomerAccountLink> model = CustomerAccountLink.LoadAllByCustomerID(MonnitSession.CurrentCustomer.CustomerID);
    if (MonnitSession.CurrentCustomer.AccountID == MonnitSession.CurrentCustomer.DefaultAccount.AccountID)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__62.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__62.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "showMainAcct", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__62.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__62.\u003C\u003Ep__0, this.ViewBag, false);
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__62.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__62.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "showMainAcct", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__62.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__62.\u003C\u003Ep__1, this.ViewBag, true);
    }
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult DisableAccountCheckList(bool disabled)
  {
    ActionResult actionResult1 = (ActionResult) this.Content("");
    ActionResult actionResult2;
    try
    {
      PreferenceType preferenceType = PreferenceType.LoadAll().Where<PreferenceType>((System.Func<PreferenceType, bool>) (pt => pt.Name.ToLower() == "disable account checklist")).FirstOrDefault<PreferenceType>();
      Preference preference = Preference.LoadByPreferenceTypeIDandCustomerID(preferenceType.PreferenceTypeID, MonnitSession.CurrentCustomer.CustomerID);
      if (preference == null)
      {
        preference = new Preference();
        preference.PreferenceTypeID = preferenceType.PreferenceTypeID;
        preference.Value = preferenceType.DefaultValue;
        preference.CustomerID = MonnitSession.CurrentCustomer.CustomerID;
      }
      preference.Value = disabled.ToString().ToLower();
      preference.Save();
      actionResult2 = (ActionResult) this.Content("Success!");
    }
    catch (Exception ex)
    {
      ex.Log("SettingsController.AccountCheckList.FailedToDisableAccountChecklistWindow");
      actionResult2 = (ActionResult) this.Content("Failed: Cannot disable Account Checklist window");
    }
    MonnitSession.CurrentCustomer.ClearPreferences();
    return actionResult2;
  }

  [AuthorizeDefault]
  public ActionResult AccountRemove(long id)
  {
    try
    {
      Account DBObject = Account.Load(id);
      if (DBObject == null)
        return (ActionResult) this.Content("Account Not Found");
      if (DBObject.AccountID == ConfigData.AppSettings("AdminAccountID").ToLong())
        return (ActionResult) this.Content("Top level account cannot be deleted.");
      if (DBObject.AccountID == MonnitSession.CurrentCustomer.DefaultAccount.AccountID && DBObject.PrimaryContactID != MonnitSession.CurrentCustomer.CustomerID)
        return (ActionResult) this.Content("You must be the primary contact to be able to delete this account.");
      if (!MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
        return (ActionResult) this.Content("You are not authorized to delete this account.");
      int count1 = Account.LoadByRetailAccountID(DBObject.AccountID).Count;
      if (count1 > 0)
        return (ActionResult) this.Content("You must remove all child accounts from the account you wish to delete!\n" + $"This account has {count1} child accounts");
      int count2 = Gateway.LoadByAccountID(DBObject.AccountID).Count;
      bool flag1 = count2 > 0;
      int count3 = Sensor.LoadByAccountID(DBObject.AccountID).Count;
      bool flag2 = count3 > 0;
      if (flag1 | flag2)
      {
        string content = "You must remove all sensors and gateways from the account you wish to delete!";
        string str1 = $"\nThis account has {count2} gateways.";
        string str2 = $"\nThis account has {count3} sensors.";
        if (flag1)
          content += str1;
        if (flag2)
          content += str2;
        return (ActionResult) this.Content(content);
      }
      DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Deleted account");
      if (MonnitSession.UserIsProxied && !MonnitSession.UnProxySimple())
        return (ActionResult) this.Content("Unexpected error occurred in trying to delete the account.");
      if (MonnitSession.IsLocationFavorite(id))
      {
        CustomerFavorite customerFavorite = MonnitSession.CurrentCustomerFavorites.AllFavorites.Find((Predicate<CustomerFavorite>) (x => x.LocationID == id));
        MonnitSession.CurrentCustomerFavorites.Invalidate();
        customerFavorite.Delete();
        Account.Remove(DBObject.AccountID);
        return (ActionResult) this.Content("Success" + (MonnitSession.IsCurrentCustomerMonnitAdmin ? "|Admin" : ""));
      }
      Account.Remove(DBObject.AccountID);
      return (ActionResult) this.Content("Success" + (MonnitSession.IsCurrentCustomerMonnitAdmin ? "|Admin" : ""));
    }
    catch (Exception ex)
    {
      ex.Log($"SettingsController.AccountRemove[AccountID: {id.ToString()}][CustomerID: {MonnitSession.CurrentCustomer.CustomerID.ToString()}] ");
      return (ActionResult) this.Content("Unexpected error occurred in trying to delete the account.");
    }
  }

  [AuthorizeDefault]
  public ActionResult LocationOverview(long? id, string searchTerm)
  {
    long? nullable = id;
    long num = nullable ?? MonnitSession.CurrentCustomer.AccountID;
    if (((IEnumerable<string>) ConfigData.AppSettings("LocationOverviewBlacklist").Split('|')).Contains<string>(num.ToString()))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "AdminSearch",
        controller = "Settings",
        id = num
      });
    Account account = Account.Load(num);
    if (account == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(num) && !MonnitSession.CustomerCan("Account_View"))
      return this.PermissionError(methodName: nameof (LocationOverview), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SettingsController.cs", sourceLineNumber: 2941);
    nullable = id;
    long accountId = MonnitSession.CurrentCustomer.AccountID;
    if (!(nullable.GetValueOrDefault() == accountId & nullable.HasValue) && !MonnitSession.AccountViewProxy(num))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__65.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__65.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountIDTree", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__65.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__65.\u003C\u003Ep__0, this.ViewBag, account != null ? account.AccountIDTree : "");
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__65.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__65.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__65.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__65.\u003C\u003Ep__1, this.ViewBag, account != null ? account.AccountID : long.MinValue);
    return (ActionResult) this.View();
  }

  public ActionResult CreateLocationAccount(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && (!MonnitSession.CustomerCan("Account_View") || !MonnitSession.IsAuthorizedForAccount(id)))
      return this.PermissionError(methodName: nameof (CreateLocationAccount), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SettingsController.cs", sourceLineNumber: 2974);
    CreateLocationAccountModel model = new CreateLocationAccountModel();
    model.ParentAccountID = id;
    model.TimeZoneID = MonnitSession.CurrentCustomer.Account.TimeZoneID;
    Account account = !MonnitSession.UserIsCustomerProxied ? MonnitSession.CurrentCustomer.Account : MonnitSession.OldCustomer.Account;
    this.Session["PurchaseLinkStoreModel"] = (object) RetailController.CreatePurchaseLinkStoreModel_WithPaymentAndProductList($"CreateLocationAccount[ID: {id.ToString()}]", "Subscriptions", account);
    return (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult CreateLocationAccount(CreateLocationAccountModel model)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && (!MonnitSession.CustomerCan("Account_View") || !MonnitSession.IsAuthorizedForAccount(model.ParentAccountID)))
      return this.PermissionError(methodName: nameof (CreateLocationAccount), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SettingsController.cs", sourceLineNumber: 2997);
    HttpSessionStateBase session = this.Session;
    if (session["TryCount"] == null)
    {
      object obj;
      session["TryCount"] = (object) (__Boxed<int>) (obj = (object) 0);
    }
    this.Session["TryCount"] = (object) ((int) this.Session["TryCount"] + 1);
    if ((int) this.Session["TryCount"] > 19)
    {
      this.ModelState.AddModelError("", "Account Create Locked: Too Many Attempts!");
      ViewDataDictionary viewData = this.ViewData;
      viewData["Exception"] = (object) (viewData["Exception"]?.ToString() + "Account Create Locked: Too Many Attempts!");
    }
    if (!Account.CheckAccountNumberIsUnique(model.CompanyName))
      this.ModelState.AddModelError("CompanyName", "Company name already exists");
    if (model.TimeZoneID < 1L)
      this.ModelState.AddModelError("TimeZoneID", "Time Zone Must Be Selected.");
    if (string.IsNullOrWhiteSpace(model.SubscriptionSKU))
      this.ModelState.AddModelError("SubscriptionSKU", "Subscription must be selected");
    if (model.SubscriptionSKU != "Trial" && model.SubscriptionSKU != "Basic" && model.SubscriptionSKU != "Code" && model.PaymentID <= 0L)
      this.ModelState.AddModelError("SubscriptionSKU", "You must set a payment in order to proceed with your selected Subscription option.");
    AccountSubscriptionType subscriptionType = (AccountSubscriptionType) null;
    if (!string.IsNullOrEmpty(model.SubscriptionCode))
    {
      string keyType = OverviewController.CheckSubscriptionCode(model.SubscriptionCode);
      if (keyType == "Failed")
        this.ModelState.AddModelError("SubscriptionCode", "Invalid Subscription Code");
      else
        subscriptionType = AccountSubscriptionType.LoadByKeyType(keyType);
    }
    if (this.ModelState.IsValid)
    {
      Account account1 = MonnitSession.UserIsCustomerProxied ? MonnitSession.OldCustomer.Account : MonnitSession.CurrentCustomer.Account;
      if (account1 != null)
      {
        bool deleteOrder = false;
        Decimal authID = 0M;
        if (!string.IsNullOrWhiteSpace(model.SubscriptionSKU) && model.SubscriptionSKU != "Basic" && model.SubscriptionSKU != "Trial" && model.SubscriptionSKU != "Code")
        {
          string errorMessage = MonnitUtil.AuthorizePayment(account1.StoreLinkGuid.ToString(), account1.StoreUserID, model.PaymentID, model.SubscriptionSKU, 1, model.TaxAmount, MonnitSession.Session.SessionID);
          if (errorMessage.Contains("Failed"))
          {
            if (model.PaymentID > 0L)
              this.ModelState.AddModelError("PaymentID", errorMessage);
            else
              this.ModelState.AddModelError("CompanyName", errorMessage);
            return (ActionResult) this.View(nameof (CreateLocationAccount), (object) model);
          }
          string[] strArray = errorMessage.Split('|');
          Decimal num1 = strArray[1].ToDecimal();
          long num2 = strArray[2].ToLong();
          long num3 = strArray[3].ToLong();
          Decimal num4 = strArray[4].ToDecimal();
          authID = num1;
          model.SalesOrderID = num2;
          model.SalesOrderItemID = num3;
          model.Total = num4;
          deleteOrder = true;
        }
        try
        {
          Account account2 = new Account();
          List<AccountIncrement> source = AccountIncrement.LoadAll();
          if (source.Count > 0)
          {
            string[] strArray = account1.AccountIDTree.Split('*');
            long? nullable = new long?();
            foreach (string s in strArray)
            {
              long accountIDToCheck;
              if (long.TryParse(s, out accountIDToCheck))
              {
                if (source.Any<AccountIncrement>((System.Func<AccountIncrement, bool>) (ai => ai.AccountID == accountIDToCheck)))
                {
                  nullable = new long?(accountIDToCheck);
                  break;
                }
              }
              else
                Console.WriteLine("Invalid Account ID: " + s);
            }
            if (nullable.HasValue)
              account2.AutoBill = nullable.Value;
            else
              Console.WriteLine("No matching AccountID found in the entire list.");
          }
          account2.AccountNumber = model.CompanyName;
          account2.CompanyName = model.CompanyName;
          account2.TimeZoneID = model.TimeZoneID;
          account2.EULAVersion = MonnitSession.CurrentTheme.CurrentEULA;
          account2.EULADate = DateTime.UtcNow;
          account2.MaxFailedLogins = MonnitSession.CurrentTheme.MaxFailedLogins;
          account2.IsCFRCompliant = false;
          account2.PrimaryContactID = MonnitSession.CurrentCustomer.CustomerID;
          account2.CreateDate = DateTime.UtcNow;
          Account account3 = Account.Load(model.ParentAccountID) ?? Account.Load(MonnitSession.CurrentTheme.AccountID) ?? Account.Load(ConfigData.AppSettings("AdminAccountID").ToLong());
          account2.RetailAccountID = account3.AccountID;
          account2.Save();
          account2.AccountIDTree = $"{account3.AccountIDTree}{account2.AccountID.ToString()}*";
          account2.Save();
          this.Session["NewAccount"] = (object) account2;
          new AccountSubscription()
          {
            AccountID = account2.AccountID,
            AccountSubscriptionTypeID = 1L,
            ExpirationDate = DateTime.Today.AddYears(100)
          }.Save();
          AccountSubscription accountSubscription1 = new AccountSubscription();
          if (!string.IsNullOrWhiteSpace(model.SubscriptionSKU) && string.IsNullOrWhiteSpace(model.SubscriptionCode))
          {
            if (model.SubscriptionSKU == "Trial")
            {
              accountSubscription1.AccountID = account2.AccountID;
              accountSubscription1.AccountSubscriptionTypeID = 10L;
              accountSubscription1.ExpirationDate = DateTime.Today.AddDays(45.0);
              accountSubscription1.Save();
            }
            else if (model.SubscriptionSKU != "Basic" && model.SubscriptionSKU != "Code")
            {
              try
              {
                string errorMessage = MonnitUtil.CapturePayment(account1.StoreLinkGuid.ToString(), model.SalesOrderID, model.SalesOrderItemID, authID, model.Total, account1.StoreUserID, model.PaymentID);
                string subscriptionActivationCode = errorMessage;
                string[] strArray1 = subscriptionActivationCode.Split('~');
                string o = strArray1 == null || strArray1.Length == 0 ? "" : strArray1[0];
                if (subscriptionActivationCode.Contains("Failed") || o.ToGuid() == Guid.Empty)
                {
                  this.ModelState.AddModelError("PaymentID", errorMessage);
                  return (ActionResult) this.View(nameof (CreateLocationAccount), (object) model);
                }
                deleteOrder = false;
                Customer currentCustomer = MonnitSession.CurrentCustomer;
                string str1 = PurchaseBase.TryUpdateSubscription(account2.AccountID, subscriptionActivationCode, currentCustomer, MonnitSession.CurrentSubscription);
                if (str1.Contains("Success"))
                {
                  string notificationEmail = MonnitSession.CurrentCustomer.NotificationEmail;
                  try
                  {
                    string subject = "iMonnit Renewal Confirmation";
                    EmailTemplate emailTemplate = EmailTemplate.LoadBest(account1, eEmailTemplateFlag.Generic);
                    AccountTheme.Load(account1.GetThemeID());
                    if (emailTemplate == null)
                    {
                      emailTemplate = new EmailTemplate();
                      emailTemplate.Subject = subject;
                      emailTemplate.Template = "<p>Thank you.  We appreciate your business.</p><p>{Content}</p>";
                    }
                    string[] strArray2 = str1.Split('|');
                    string s = strArray2 == null || strArray2.Length <= 1 ? "" : strArray2[1];
                    long ID = string.IsNullOrWhiteSpace(s) ? long.MinValue : long.Parse(s);
                    AccountSubscription accountSubscription2 = ID > 0L ? AccountSubscription.Load(ID) : (AccountSubscription) null;
                    string str2 = "";
                    if (accountSubscription2 != null)
                      str2 = $"<p>The license renewed is for <b>{accountSubscription2.AccountSubscriptionType.Name}</b> and will expire on <b>{accountSubscription2.ExpirationDate.ToShortDateString()}.</b></p>";
                    string body = "<p style=\"display:none;\">Thank you.  We appreciate your business.</p>" + emailTemplate.MailMerge($"<p>Thank you for renewing your subscription of iMonnit Premiere.  Your payment will be processed today and a receipt will be sent to the email address you entered when the order was placed.</p>{str2}<p>If there's anything else we can assit with, please contact us at <a href=\"mailto:sales@monnit.com\">sales@monnit.com</a>.  For technical help, contact us at <a href=\"mailto:support@monnit.com\">support@monnit.com</a>.</p><br/> Sincerely,<br/> The Monnit Team", notificationEmail);
                    MonnitUtil.SendMail(notificationEmail, subject, body, account1);
                  }
                  catch (Exception ex)
                  {
                    ex.Log($"SettingsController.CreateLocationAccount[POST][PremiereRenewConfirmationEmail][AccountID: {account1.AccountID.ToString()}][SKU: {model.SubscriptionSKU}][ToEmail: {notificationEmail}] ");
                  }
                }
              }
              catch (Exception ex)
              {
                string[] strArray = new string[9];
                strArray[0] = "SettingsController.CreateLocationAccount[Post-CapturePayment][AccountID: ";
                strArray[1] = account1.AccountID.ToString();
                strArray[2] = "][SubaccountID: ";
                long num = account2.AccountID;
                strArray[3] = num.ToString();
                strArray[4] = "][PaymentID: ";
                num = model.PaymentID;
                strArray[5] = num.ToString();
                strArray[6] = "][SKU: ";
                strArray[7] = model.SubscriptionSKU;
                strArray[8] = "] ";
                string function = string.Concat(strArray);
                ex.Log(function);
              }
            }
          }
          else if (subscriptionType != null)
          {
            try
            {
              DateTime expirationDateFromMea = OverviewController.GetExpirationDateFromMEA(model.SubscriptionCode, account2.AccountID, subscriptionType.KeyType, account2.CurrentSubscription.AccountSubscriptionType.KeyType, DateTime.UtcNow);
              if (expirationDateFromMea == DateTime.MinValue)
                throw new Exception("Activation Code Failed");
              accountSubscription1.AccountID = account2.AccountID;
              accountSubscription1.AccountSubscriptionTypeID = subscriptionType.AccountSubscriptionTypeID;
              accountSubscription1.ExpirationDate = expirationDateFromMea;
              accountSubscription1.LastKeyUsed = model.SubscriptionCode.ToGuid();
              accountSubscription1.Save();
            }
            catch (Exception ex)
            {
            }
          }
          else
          {
            accountSubscription1.AccountSubscriptionTypeID = MonnitSession.CurrentTheme.DefaultAccountSubscriptionTypeID;
            accountSubscription1.ExpirationDate = DateTime.Today.AddDays((double) MonnitSession.CurrentTheme.DefaultPremiumDays);
            accountSubscription1.AccountID = account2.AccountID;
            accountSubscription1.Save();
          }
          this.Session["TryCount"] = (object) 0;
          CSNet csNet = new CSNet();
          csNet.AccountID = account2.AccountID;
          csNet.Name = account2.CompanyName + " Network";
          csNet.SendNotifications = true;
          csNet.Save();
          new CustomerPermission()
          {
            CSNetID = csNet.CSNetID,
            CustomerID = account2.PrimaryContact.CustomerID,
            CustomerPermissionTypeID = CustomerPermissionType.Find("Network_View").CustomerPermissionTypeID,
            Can = true
          }.Save();
          return (ActionResult) this.RedirectToRoute("Default", (object) new
          {
            action = "LocationOverview",
            controller = "Settings",
            id = model.ParentAccountID
          });
        }
        catch (Exception ex)
        {
          this.ModelState.AddModelError("", ex.Message);
          ViewDataDictionary viewData = this.ViewData;
          viewData["Exception"] = (object) (viewData["Exception"]?.ToString() + ex.Message);
          if (authID > 0M)
          {
            string errorMessage = MonnitUtil.CancelAuthorization(authID, model.Total, model.SalesOrderID, model.SalesOrderItemID, deleteOrder);
            if (errorMessage != "Success")
            {
              this.ModelState.AddModelError("PaymentID", errorMessage);
              return (ActionResult) this.View(nameof (CreateLocationAccount), (object) model);
            }
          }
          return MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.View(nameof (CreateLocationAccount), (object) model) : (ActionResult) this.View((object) model);
        }
      }
      else
      {
        this.ModelState.AddModelError("ParentAccountID", "Invalid Parent Account");
        return (ActionResult) this.View(nameof (CreateLocationAccount), (object) model);
      }
    }
    else
      return MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.View(nameof (CreateLocationAccount), (object) model) : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult CreateLocationAccountParentList(string searchCriteria)
  {
    if (!MonnitSession.CurrentCustomer.IsAdmin)
      return (ActionResult) this.Content("Unauthorized");
    searchCriteria = WebUtility.HtmlEncode(searchCriteria);
    return (ActionResult) this.View((object) AccountSearchModel.Search(MonnitSession.CurrentCustomer.CustomerID, searchCriteria, 100));
  }

  [AuthorizeDefault]
  public ActionResult RetrieveItemTaxValue(string sku, long paymentProfileID)
  {
    string content = "0";
    try
    {
      if (this.Session["PurchaseLinkStoreModel"] is PurchaseLinkStoreModel purchaseLinkStoreModel && purchaseLinkStoreModel.account != null)
        content = MonnitUtil.RetrieveItemTaxValue(sku, 1, paymentProfileID, purchaseLinkStoreModel.account.StoreUserID, MonnitSession.Session.SessionID).ToString();
    }
    catch (Exception ex)
    {
      ex.Log("SettingsController.RetrieveItemTaxValue[Get] ");
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public ActionResult AdminSearch(long? id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CustomerCan("Navigation_View_Administration"))
      return this.PermissionError(ThemeController.PermissionErrorMessage.MissingCustomerPermission("Navigation_View_Administration"), methodName: nameof (AdminSearch), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SettingsController.cs", sourceLineNumber: 3370);
    Account model = new Account();
    if (id.HasValue)
      model = Account.Load(id.ToLong());
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult AssignParentSearch(long id)
  {
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(id))
      return this.PermissionError(ThemeController.PermissionErrorMessage.IsAuthorizedForAccount(id), methodName: nameof (AssignParentSearch), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SettingsController.cs", sourceLineNumber: 3394);
    return !MonnitSession.CurrentCustomer.CanAssignParent(id) ? this.PermissionError(ThemeController.PermissionErrorMessage.IsAdmin(), methodName: nameof (AssignParentSearch), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SettingsController.cs", sourceLineNumber: 3397) : (ActionResult) this.View((object) Account.Load(id));
  }

  [AuthorizeDefault]
  public ActionResult AssignParentList(long id, string searchCriteria)
  {
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(id))
      return (ActionResult) this.Content(ThemeController.PermissionErrorMessage.IsAuthorizedForAccount(id));
    if (!MonnitSession.CurrentCustomer.CanAssignParent(id))
      return (ActionResult) this.Content(ThemeController.PermissionErrorMessage.IsAdmin());
    searchCriteria = WebUtility.HtmlEncode(searchCriteria);
    Account account = Account.Load(id.ToLong());
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__72.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__72.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Account, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TargetAccount", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SettingsController.\u003C\u003Eo__72.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__72.\u003C\u003Ep__0, this.ViewBag, account);
    return (ActionResult) this.View((object) AccountSearchModel.Search(MonnitSession.CurrentCustomer.CustomerID, searchCriteria, 100));
  }

  [AuthorizeDefault]
  public ActionResult AdminCertCSVUpload()
  {
    return !MonnitSession.IsCurrentCustomerMonnitAdmin && MonnitSession.CurrentCustomer.AccountID != 2301L ? this.PermissionError(methodName: nameof (AdminCertCSVUpload), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SettingsController.cs", sourceLineNumber: 3430) : (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public ActionResult AdminContacts(long id)
  {
    AccountTheme model = AccountTheme.Load(id);
    return !MonnitSession.IsCurrentCustomerMonnitSuperAdmin && MonnitSession.CurrentCustomer.AccountID != model.AccountID && !MonnitSession.CustomerCan("Account_View") ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult AdminUserSearch() => (ActionResult) this.View();

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult DeviceLookup(long id)
  {
    try
    {
      Sensor sensor = Sensor.Load(id);
      if (sensor != null && (!sensor.IsDeleted || MonnitSession.CustomerCan("Support_Advanced")))
      {
        CSNet csNet = CSNet.Load(sensor.CSNetID);
        if (csNet == null || MonnitSession.IsAuthorizedForAccount(csNet.AccountID) || csNet.AccountID == ConfigData.AppSettings("DeviceHolderAccountID").ToLong())
          return (ActionResult) this.PartialView("SensorLookup", (object) new SensorInformation(sensor));
      }
      Gateway gateway = Gateway.Load(id);
      if (gateway != null && (!gateway.IsDeleted || MonnitSession.CustomerCan("Support_Advanced")))
      {
        CSNet csNet = CSNet.Load(gateway.CSNetID);
        if (csNet == null || MonnitSession.IsAuthorizedForAccount(csNet.AccountID) || csNet.AccountID == ConfigData.AppSettings("DeviceHolderAccountID").ToLong())
          return (ActionResult) this.PartialView("GatewayLookup", (object) new GatewayInformation(gateway));
      }
      return (ActionResult) this.Content("No device found for this ID");
    }
    catch (Exception ex)
    {
      ex.Log("Device Look up Failed DeviceID : " + id.ToString());
    }
    return (ActionResult) this.Content("Failed");
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult UserLookup(string id)
  {
    try
    {
      if (MonnitSession.CustomerCan("Navigation_View_Administration"))
      {
        DataTable dataTable = Customer.Search(id);
        List<UserLookUpModel> userLookUpModelList = new List<UserLookUpModel>();
        Dictionary<long, Account> dictionary = new Dictionary<long, Account>();
        this.ViewData["query"] = (object) id;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          try
          {
            if (MonnitSession.IsCurrentCustomerMonnitAdmin)
              userLookUpModelList.Add(new UserLookUpModel(row));
            else if (MonnitSession.CurrentCustomer.AccountID == row["RetailAccountID"].ToLong())
            {
              userLookUpModelList.Add(new UserLookUpModel(row));
            }
            else
            {
              long retailAccountId = row["RetailAccountID"].ToLong();
              Account account;
              do
              {
                if (dictionary.ContainsKey(retailAccountId))
                {
                  account = dictionary[retailAccountId];
                }
                else
                {
                  account = Account.Load(retailAccountId);
                  if (account != null)
                    dictionary.Add(retailAccountId, account);
                }
                if (account != null)
                {
                  retailAccountId = account.RetailAccountID;
                  if (account.RetailAccountID == MonnitSession.CurrentCustomer.AccountID)
                  {
                    userLookUpModelList.Add(new UserLookUpModel(row));
                    break;
                  }
                }
              }
              while (account != null);
            }
          }
          catch (Exception ex)
          {
            ex.Log("User Data Row parse failed,  User Query : " + id);
          }
        }
        List<UserLookUpModel> model = new List<UserLookUpModel>();
        UserLookUpModel userLookUpModel1 = (UserLookUpModel) null;
        if (userLookUpModelList.Count > 0)
        {
          foreach (UserLookUpModel userLookUpModel2 in userLookUpModelList)
          {
            if (model.Count < 1)
            {
              model.Add(userLookUpModel2);
              userLookUpModel1 = userLookUpModel2;
            }
            else if (userLookUpModel1.CustomerID != userLookUpModel2.CustomerID)
            {
              model.Add(userLookUpModel2);
              userLookUpModel1 = userLookUpModel2;
            }
            else
              userLookUpModel1 = userLookUpModel2;
          }
        }
        return (ActionResult) this.PartialView((object) model);
      }
    }
    catch (Exception ex)
    {
      ex.Log("User Look up Failed User Query : " + id);
    }
    return (ActionResult) this.Content("Failed");
  }

  [AuthorizeDefault]
  public ActionResult AdminSubscriptionDetails(long id)
  {
    Account acct = Account.Load(id);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CustomerCan("Account_Set_Premium"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    AccountSubscription currentSubscription = acct.CurrentSubscription;
    List<AccountSubscription> subscriptions = acct.Subscriptions;
    if (subscriptions.Count == 0)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "AdminSearch",
        controller = "Settings"
      });
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__78.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__78.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "subMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SettingsController.\u003C\u003Eo__78.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__78.\u003C\u003Ep__0, this.ViewBag, "");
    return (ActionResult) this.View((object) subscriptions);
  }

  [AuthorizeDefault]
  [ValidateAntiForgeryToken]
  [HttpPost]
  public ActionResult AdminSubscriptionDetails(
    long id,
    long accountsubscriptionTypeID,
    string ExpirationDate)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CustomerCan("Account_Set_Premium"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    try
    {
      Account account = Account.Load(id);
      if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index",
          controller = "Overview"
        });
      AccountSubscriptionType subscriptionType = AccountSubscriptionType.Load(accountsubscriptionTypeID);
      bool flag = false;
      string o = new Guid().ToString();
      if (!MonnitSession.IsEnterprise)
      {
        o = CheckoutController.AdminUpdateAccountSubscription(account.AccountID, ExpirationDate, subscriptionType.KeyType, new Guid?(account.CurrentSubscription.LastKeyUsed));
        if (o == "Update Failed")
        {
          string errorMessage = "Failed: Could not add subscription to MEA ";
          // ISSUE: reference to a compiler-generated field
          if (SettingsController.\u003C\u003Eo__79.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            SettingsController.\u003C\u003Eo__79.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "subMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = SettingsController.\u003C\u003Eo__79.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__79.\u003C\u003Ep__0, this.ViewBag, errorMessage);
          this.ModelState.AddModelError("", errorMessage);
          return (ActionResult) this.View(nameof (AdminSubscriptionDetails), (object) account.Subscriptions);
        }
      }
      foreach (AccountSubscription subscription in account.Subscriptions)
      {
        if (accountsubscriptionTypeID == subscription.AccountSubscriptionTypeID)
        {
          flag = true;
          subscription.UpdateAccountSubscriptionDate(ExpirationDate.ToDateTime(), MonnitSession.CurrentCustomer, "Admin Manual Update", o.ToGuid());
          break;
        }
      }
      if (!flag)
      {
        if ((long) Sensor.LoadByAccountID(account.AccountID).Count > (long) subscriptionType.AllowedSensors)
        {
          string errorMessage = "Failed: Account has too many sensors to use this subscription type.";
          // ISSUE: reference to a compiler-generated field
          if (SettingsController.\u003C\u003Eo__79.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            SettingsController.\u003C\u003Eo__79.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "subMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = SettingsController.\u003C\u003Eo__79.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__79.\u003C\u003Ep__1, this.ViewBag, errorMessage);
          this.ModelState.AddModelError("", errorMessage);
          return (ActionResult) this.View(nameof (AdminSubscriptionDetails), (object) account.Subscriptions);
        }
        AccountSubscription accountSubscription = new AccountSubscription();
        accountSubscription.AccountID = account.AccountID;
        accountSubscription.AccountSubscriptionTypeID = accountsubscriptionTypeID;
        accountSubscription.CSNetID = long.MinValue;
        accountSubscription.ExpirationDate = ExpirationDate.ToDateTime();
        accountSubscription.Save();
        accountSubscription.UpdateAccountSubscriptionDate(ExpirationDate.ToDateTime(), MonnitSession.CurrentCustomer, "Admin Added", o.ToGuid());
      }
      account.ClearSubscritions();
      AccountSubscription.AccountSubscriptionFeatureChange(account, account.CurrentSubscription);
      account.LogAuditData(eAuditAction.Update, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Extended accounts premium status");
      if (account.CurrentSubscription.IsActive && account.CurrentSubscription.AccountSubscriptionID != 1L && account.CurrentSubscription.AccountSubscriptionID != 10L)
      {
        using (MailMessage mail = new MailMessage())
        {
          using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, account))
          {
            mail.To.Clear();
            mail.To.SafeAdd(ConfigData.AppSettings("NewAccountNotificationEmail"), "Monnit Accounting");
            mail.Subject = "New Premiere Account";
            mail.Body = string.Format("Account: {0} has just been created as a {2} Account by {1}", (object) account.CompanyName, (object) MonnitSession.CurrentCustomer.Account.CompanyName, (object) account.CurrentSubscription.AccountSubscriptionType.Name);
            mail.IsBodyHtml = true;
            MonnitUtil.SendMail(mail, smtpClient);
          }
        }
      }
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__79.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__79.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "subMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SettingsController.\u003C\u003Eo__79.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__79.\u003C\u003Ep__2, this.ViewBag, "Success");
      return (ActionResult) this.View(nameof (AdminSubscriptionDetails), (object) account.Subscriptions);
    }
    catch (Exception ex)
    {
      string str = "Update Subscription Failed, message: " + ex.Message;
      ex.Log(str);
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__79.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__79.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "subMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__79.\u003C\u003Ep__3.Target((CallSite) SettingsController.\u003C\u003Eo__79.\u003C\u003Ep__3, this.ViewBag, str);
      this.ModelState.AddModelError("", str);
    }
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = nameof (AdminSubscriptionDetails),
      controller = "Settings",
      id = id
    });
  }

  [AuthorizeDefault]
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
      string str2 = PurchaseBase.RetreiveActivationKey(subscriptionActivationCode, account.CompanyName);
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

  public ActionResult AdminSMSCarriersList(long? currentThemeAccountID)
  {
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__81.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__81.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (currentThemeAccountID), typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SettingsController.\u003C\u003Eo__81.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__81.\u003C\u003Ep__0, this.ViewBag, currentThemeAccountID);
    return !MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) SMSAccount.SMSList(currentThemeAccountID.GetValueOrDefault()).ToList<SMSCarrier>());
  }

  [HttpPost]
  [AuthorizeDefault]
  public ActionResult AdminSMSCarriersList(long? currentThemeAccountID, FormCollection collection)
  {
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__82.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__82.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (currentThemeAccountID), typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SettingsController.\u003C\u003Eo__82.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__82.\u003C\u003Ep__0, this.ViewBag, currentThemeAccountID);
    SMSAccount.DeleteByAccountID(MonnitSession.CurrentTheme.AccountThemeID);
    List<long> longList = new List<long>();
    foreach (string allKey in collection.AllKeys)
    {
      if (allKey.Contains("SMSCarrierID_"))
        longList.Add(collection[allKey].ToLong());
    }
    if (longList.Count > 1)
    {
      for (int index = 0; index < longList.Count; ++index)
        new SMSAccount()
        {
          AccountThemeID = MonnitSession.CurrentTheme.AccountThemeID,
          SMSAccountID = MonnitSession.CurrentTheme.AccountID,
          SMSCarrierID = longList[index]
        }.Save();
    }
    return (ActionResult) this.View((object) SMSAccount.SMSList(currentThemeAccountID.GetValueOrDefault()).ToList<SMSCarrier>());
  }

  [AuthorizeDefault]
  public ActionResult AdminAccountList(string searchCriteria)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_Administration") && !MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Settings",
        id = MonnitSession.CurrentCustomer.AccountID
      });
    searchCriteria = WebUtility.HtmlEncode(searchCriteria);
    if (searchCriteria.Length < 1)
      return (ActionResult) this.View((object) new List<AccountSearchModel>());
    if (searchCriteria.ToLower() == "Show All".ToLower())
      searchCriteria = "";
    return (ActionResult) this.View((object) AccountSearchModel.Search(MonnitSession.CurrentCustomer.CustomerID, searchCriteria, 100));
  }

  [AuthorizeDefault]
  public ActionResult AutoBillAccountList(string searchCriteria)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_Administration") && !MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Settings",
        id = MonnitSession.CurrentCustomer.AccountID
      });
    searchCriteria = WebUtility.HtmlEncode(searchCriteria);
    if (searchCriteria.Length < 1)
      return (ActionResult) this.View((object) new List<AccountIDTreeModel>());
    if (searchCriteria.ToLower() == "Show All".ToLower())
      searchCriteria = "";
    return (ActionResult) this.View((object) AccountSearchModel.Search(MonnitSession.CurrentCustomer.CustomerID, searchCriteria, 100));
  }

  public static string ReNewAccount(string key)
  {
    DateTime utcNow = DateTime.UtcNow;
    string uri = "";
    string str1 = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str1) && bool.Parse(str1);
    try
    {
      uri = string.Format(ConfigData.FindValue("MEA_API_Location") + "xml/RenewExistingKeys/{1}?key={0}", (object) key, (object) OverviewControllerBase.GetTempAuthToken());
      XDocument xdocument = XDocument.Load(uri);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-ReNewAccount",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = xdocument.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      string str2 = xdocument.Descendants((XName) "object").Single<XElement>().Attribute((XName) "Result").Value;
      string str3 = xdocument.Descendants((XName) "object").Single<XElement>().Attribute((XName) "ExpirationDate").Value;
      if (!(str2 == "Success: Key is Reactivated.") || string.IsNullOrEmpty(str3))
        return str2;
      MonnitSession.CurrentSubscription = (AccountSubscription) null;
      return str3;
    }
    catch (Exception ex)
    {
      ex.Log($"SettingsController.ReNewAccount[Key: {key}] ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-ReNewAccount",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return "Failed";
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult SetThemeContact(long id, FormCollection collection)
  {
    try
    {
      AccountTheme model = AccountTheme.Load(id);
      AccountThemeContact accountThemeContact = new AccountThemeContact();
      if (!collection["Email"].IsValidEmail())
        throw new Exception("Failed: Invlaid Email");
      accountThemeContact.AccountThemeID = id;
      accountThemeContact.FirstName = collection["FirstName"];
      accountThemeContact.LastName = collection["LastName"];
      accountThemeContact.Email = collection["Email"];
      accountThemeContact.Other = string.IsNullOrEmpty(collection["Other"]) ? "" : collection["Other"];
      accountThemeContact.Save();
      return (ActionResult) this.PartialView("_AdminThemeContactList", (object) model);
    }
    catch (Exception ex)
    {
      ex.Log("Failed to add account theme contact. AccountThemeID: " + id.ToString());
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult ThemePreference(long? id)
  {
    if (!id.HasValue)
      id = new long?(MonnitSession.CurrentCustomer.Account.GetThemeID());
    List<AccountThemePreferenceTypeLink> model = AccountThemePreferenceTypeLink.LoadByAccountThemeID(id ?? long.MinValue);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__87.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__87.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "accountThemeID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SettingsController.\u003C\u003Eo__87.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__87.\u003C\u003Ep__0, this.ViewBag, id);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult EditThemePreference(long id, FormCollection collection)
  {
    foreach (PreferenceType preferenceType in PreferenceType.LoadAll())
    {
      try
      {
        AccountThemePreferenceTypeLink preferenceTypeLink = AccountThemePreferenceTypeLink.LoadByPreferenceTypeIDandAccountThemeID(preferenceType.PreferenceTypeID, id);
        if (preferenceTypeLink == null)
        {
          preferenceTypeLink = new AccountThemePreferenceTypeLink();
          preferenceTypeLink.PreferenceTypeID = preferenceType.PreferenceTypeID;
          preferenceTypeLink.AccountThemeID = id;
        }
        preferenceTypeLink.AccountCanOverride = !string.IsNullOrEmpty(collection[preferenceType.Name + "_AccountCan"]) && collection[preferenceType.Name + "_AccountCan"].ToLower() == "on";
        preferenceTypeLink.CustomerCanOverride = !string.IsNullOrEmpty(collection[preferenceType.Name + "_CustomerCan"]) && collection[preferenceType.Name + "_CustomerCan"].ToLower() == "on";
        preferenceTypeLink.DefaultValue = string.IsNullOrEmpty(collection[preferenceType.Name + "_DefaultVal"]) ? preferenceType.DefaultValue : collection[preferenceType.Name + "_DefaultVal"];
        preferenceTypeLink.Save();
      }
      catch
      {
        return (ActionResult) this.Content("Failed");
      }
    }
    MonnitSession.CurrentCustomer.ClearPreferences();
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult AdminTheme()
  {
    if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
      return (ActionResult) this.View((object) AccountTheme.LoadAll());
    if (MonnitSession.CurrentCustomer.AccountID == MonnitSession.CurrentTheme.AccountID)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "AccountTheme",
        controller = "Settings",
        id = MonnitSession.CurrentTheme.AccountThemeID
      });
    return ConfigData.AppSettings("IsEnterprise").ToBool() ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "AdminSearch",
      controller = "Settings",
      id = MonnitSession.CurrentTheme.AccountThemeID
    }) : (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Settings"
    });
  }

  [AuthorizeDefault]
  public ActionResult AdminThemeCreate()
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin && !MonnitSession.IsCurrentCustomerAccountThemeAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Settings"
      });
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__90.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__90.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SettingsController.\u003C\u003Eo__90.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__90.\u003C\u003Ep__0, this.ViewBag, "");
    return (ActionResult) this.View((object) new AccountTheme());
  }

  [HttpPost]
  [ValidateInput(false)]
  [AuthorizeDefault]
  public ActionResult AdminThemeCreate(FormCollection collection)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin && !MonnitSession.IsCurrentCustomerAccountThemeAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Settings"
      });
    AccountTheme model = new AccountTheme();
    try
    {
      if (string.IsNullOrEmpty(collection["SMTPFriendlyName"].ToString()))
        this.ModelState.AddModelError("SMTPFriendlyName", "Required");
      if (string.IsNullOrEmpty(collection["SMTPDefaultFrom"].ToString()))
        this.ModelState.AddModelError("SMTPDefaultFrom", "Required");
      if (string.IsNullOrWhiteSpace(collection["WhiteLabelReseller"]))
      {
        if (string.IsNullOrEmpty(collection["Domain"].ToString()))
          this.ModelState.AddModelError("Domain", "Required");
        if (!string.IsNullOrEmpty(collection["SMTP"].ToString()))
        {
          if (string.IsNullOrEmpty(collection["SMTPPasswordPlainText"].ToString()))
            this.ModelState.AddModelError("SMTPPasswordPlainText", "Required");
          if (collection["SMTPPort"].ToInt() < 1 || collection["SMTPPort"].ToInt() > 65000 || string.IsNullOrEmpty(collection["SMTPPort"].ToString()))
            this.ModelState.AddModelError("SMTPPort", "Required");
          if (string.IsNullOrEmpty(collection["SMTPUser"].ToString()))
            this.ModelState.AddModelError("SMTPUser", "Required");
        }
        if (string.IsNullOrEmpty(collection["Theme"].ToString()))
          this.ModelState.AddModelError("Theme", "Required");
      }
      if (this.ModelState.IsValid)
      {
        this.UpdateModel<AccountTheme>(model);
        model.Save();
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__91.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__91.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = SettingsController.\u003C\u003Eo__91.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__91.\u003C\u003Ep__0, this.ViewBag, "Success");
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "AdminThemeEdit",
          controller = "Settings",
          id = model.AccountThemeID
        });
      }
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__91.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__91.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SettingsController.\u003C\u003Eo__91.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__91.\u003C\u003Ep__1, this.ViewBag, "Failed");
      return (ActionResult) this.View((object) model);
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__91.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__91.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__91.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__91.\u003C\u003Ep__2, this.ViewBag, ex.Message);
      return (ActionResult) this.View((object) model);
    }
  }

  [AuthorizeDefault]
  public ActionResult AdminThemeEdit(long id, long? styleGroupID)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin && !MonnitSession.IsCurrentCustomerAccountThemeAdmin && !ConfigData.AppSettings("IsEnterprise").ToBool())
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Settings"
      });
    AccountTheme accountTheme = AccountTheme.Load(id);
    if (accountTheme == null || !MonnitSession.CurrentCustomer.CanSeeAccount(accountTheme.AccountID))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Settings",
        ErrorTranslateTag = "Settings/AdminThemeEdit|",
        ErrorTitle = "Unauthorized",
        ErrorMessage = "You do not have permission to access this page."
      });
    List<AccountThemeStyleGroup> source = AccountThemeStyleGroup.LoadByAccountThemeID(id, true);
    AccountThemeStyleGroup model = source.Where<AccountThemeStyleGroup>((System.Func<AccountThemeStyleGroup, bool>) (g =>
    {
      long themeStyleGroupId = g.AccountThemeStyleGroupID;
      long? nullable = styleGroupID;
      long valueOrDefault = nullable.GetValueOrDefault();
      return themeStyleGroupId == valueOrDefault & nullable.HasValue;
    })).FirstOrDefault<AccountThemeStyleGroup>();
    if (model == null && source.Count > 0)
      model = source[0];
    if (model == null)
    {
      model = new AccountThemeStyleGroup();
      model.Name = accountTheme.Theme;
      model.AccountThemeID = accountTheme.AccountThemeID;
    }
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__92.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__92.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, AccountTheme, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Theme", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__92.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__92.\u003C\u003Ep__0, this.ViewBag, accountTheme);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__92.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__92.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<AccountThemeStyleGroup>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "StyleGroups", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__92.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__92.\u003C\u003Ep__1, this.ViewBag, source);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__92.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__92.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = SettingsController.\u003C\u003Eo__92.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__92.\u003C\u003Ep__2, this.ViewBag, "");
    return (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [ValidateInput(false)]
  [AuthorizeDefault]
  public ActionResult AdminThemeEdit(long id, FormCollection collection)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin && !MonnitSession.IsCurrentCustomerAccountThemeAdmin && !ConfigData.AppSettings("IsEnterprise").ToBool())
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Settings"
      });
    AccountTheme accountTheme = AccountTheme.Load(id);
    if (accountTheme == null || !MonnitSession.CurrentCustomer.CanSeeAccount(accountTheme.AccountID))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Settings",
        ErrorTranslateTag = "Settings/AdminThemeEdit|",
        ErrorTitle = "Unauthorized",
        ErrorMessage = "You do not have permission to access this page."
      });
    List<AccountThemeStyleGroup> source = AccountThemeStyleGroup.LoadByAccountThemeID(id, true);
    AccountThemeStyleGroup model = source.Where<AccountThemeStyleGroup>((System.Func<AccountThemeStyleGroup, bool>) (g => g.AccountThemeStyleGroupID == collection["AccountThemeStyleGroupID"].ToLong())).FirstOrDefault<AccountThemeStyleGroup>();
    if (model == null && source.Count > 0)
      model = source[0];
    if (model == null)
    {
      model = new AccountThemeStyleGroup();
      model.Name = accountTheme.Theme;
      model.AccountThemeID = accountTheme.AccountThemeID;
      model.Save();
    }
    try
    {
      foreach (AccountThemeStyleType accountThemeStyleType in AccountThemeStyleType.LoadAll())
      {
        AccountThemeStyleType type = accountThemeStyleType;
        if (!string.IsNullOrEmpty(collection[type.Property]))
        {
          AccountThemeStyle accountThemeStyle = model.Styles.Where<AccountThemeStyle>((System.Func<AccountThemeStyle, bool>) (s => s.AccountThemeStyleTypeID == type.AccountThemeStyleTypeID)).FirstOrDefault<AccountThemeStyle>();
          if (accountThemeStyle == null)
            accountThemeStyle = new AccountThemeStyle()
            {
              AccountThemeStyleGroupID = model.AccountThemeStyleGroupID,
              AccountThemeStyleTypeID = type.AccountThemeStyleTypeID
            };
          switch (type.DataType)
          {
            case "BinaryImage":
              accountThemeStyle.Save();
              break;
            default:
              accountThemeStyle.Value = collection[type.Property];
              goto case "BinaryImage";
          }
        }
      }
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "AdminAccountThemeEdit",
        controller = "Settings",
        id = accountTheme.AccountThemeID
      });
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__93.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__93.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, AccountTheme, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Theme", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SettingsController.\u003C\u003Eo__93.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__93.\u003C\u003Ep__0, this.ViewBag, accountTheme);
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__93.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__93.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<AccountThemeStyleGroup>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "StyleGroups", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = SettingsController.\u003C\u003Eo__93.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__93.\u003C\u003Ep__1, this.ViewBag, source);
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__93.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__93.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = SettingsController.\u003C\u003Eo__93.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__93.\u003C\u003Ep__2, this.ViewBag, ex.Message);
      return (ActionResult) this.View((object) model);
    }
  }

  public ActionResult GetDefaultValue(long id)
  {
    return (ActionResult) this.Content(AccountThemeStyleType.Load(id).DefaultValue);
  }

  [HttpPost]
  [ValidateInput(false)]
  [AuthorizeDefault]
  public ActionResult AdminAccountThemeUpload(long id, FormCollection collection)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin && !MonnitSession.IsCurrentCustomerAccountThemeAdmin && !MonnitSession.IsEnterprise)
      return (ActionResult) this.Content("UnAuthorized");
    AccountTheme accountTheme = AccountTheme.Load(id);
    if (accountTheme == null || !MonnitSession.CurrentCustomer.CanSeeAccount(accountTheme.AccountID))
      return (ActionResult) this.Content("UnAuthorized");
    string content = "";
    try
    {
      List<AccountThemeStyleGroup> source = AccountThemeStyleGroup.LoadByAccountThemeID(id, true);
      AccountThemeStyleGroup accountThemeStyleGroup = source.Where<AccountThemeStyleGroup>((System.Func<AccountThemeStyleGroup, bool>) (g => g.AccountThemeStyleGroupID == collection["styleGroup"].ToLong())).FirstOrDefault<AccountThemeStyleGroup>();
      if (accountThemeStyleGroup == null && source.Count > 0)
        accountThemeStyleGroup = source[0];
      if (accountThemeStyleGroup == null)
        content += "No Style Group Defined. ";
      AccountThemeStyleType StyleType = AccountThemeStyleType.Load((long) collection["styleType"].ToInt());
      if (StyleType == null)
        content += "No Style Type Defined. ";
      HttpPostedFileBase file = this.Request.Files["file"];
      if (file == null)
      {
        content += "File not found.  ";
      }
      else
      {
        AccountThemeStyle accountThemeStyle = accountThemeStyleGroup.Styles.Where<AccountThemeStyle>((System.Func<AccountThemeStyle, bool>) (s => s.AccountThemeStyleTypeID == StyleType.AccountThemeStyleTypeID)).FirstOrDefault<AccountThemeStyle>();
        if (accountThemeStyle == null)
          accountThemeStyle = new AccountThemeStyle()
          {
            AccountThemeStyleGroupID = accountThemeStyleGroup.AccountThemeStyleGroupID,
            AccountThemeStyleTypeID = StyleType.AccountThemeStyleTypeID
          };
        switch (StyleType.DataType.ToLower().Trim())
        {
          case "image":
          case "logo":
            try
            {
              using (MemoryStream memoryStream = new MemoryStream())
              {
                this.GetImage(file.InputStream, 275, 40).Save((Stream) memoryStream, ImageFormat.Png);
                accountThemeStyle.BinaryValue = memoryStream.ToArray();
                accountThemeStyle.Save();
                break;
              }
            }
            catch (Exception ex)
            {
              content += "File type not compatible, please use .png file type. ";
              break;
            }
          case "appicon":
            try
            {
              using (MemoryStream memoryStream = new MemoryStream())
              {
                this.GetImage(file.InputStream, 512 /*0x0200*/, 512 /*0x0200*/).Save((Stream) memoryStream, ImageFormat.Png);
                accountThemeStyle.BinaryValue = memoryStream.ToArray();
                accountThemeStyle.Save();
                break;
              }
            }
            catch (Exception ex)
            {
              content += "File type not compatible, please use .png file type. ";
              break;
            }
          case "ico":
            try
            {
              using (MemoryStream destination = new MemoryStream())
              {
                file.InputStream.CopyTo((Stream) destination);
                accountThemeStyle.BinaryValue = destination.ToArray();
                destination.Position = 0L;
                Icon icon = new Icon((Stream) destination);
                accountThemeStyle.Save();
                break;
              }
            }
            catch
            {
              content += "File type not compatible, please use .ico file type.";
              break;
            }
          default:
            content += "Unsupported Style ";
            break;
        }
      }
      return content.Length == 0 ? (ActionResult) this.Content("Success") : (ActionResult) this.Content(content);
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__95.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__95.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__95.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__95.\u003C\u003Ep__0, this.ViewBag, ex.Message);
      return (ActionResult) this.Content($"Failed: {content}{ex.Message}");
    }
  }

  private Bitmap GetImage(Stream str, int maxWidth, int maxHeight)
  {
    Bitmap image1 = (Bitmap) Image.FromStream(str);
    if (image1.Width <= maxWidth && image1.Height <= maxHeight)
      return image1;
    int width = image1.Width <= maxWidth ? image1.Width : maxWidth;
    int height = image1.Height <= maxHeight ? image1.Height : maxHeight;
    using (image1)
    {
      Bitmap image2 = new Bitmap(width, height, PixelFormat.Format32bppArgb);
      using (Graphics graphics = Graphics.FromImage((Image) image2))
      {
        graphics.Clear(Color.Transparent);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.DrawImage((Image) image1, new Rectangle(0, 0, width, height), new Rectangle(0, 0, image1.Width, image1.Height), GraphicsUnit.Pixel);
      }
      return image2;
    }
  }

  [AuthorizeDefault]
  public ActionResult AdminEmailTemplate(long id)
  {
    if (!MonnitSession.CurrentCustomer.IsAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    Account model = Account.Load(AccountTheme.Load(id).AccountID);
    this.ViewData["Templates"] = (object) EmailTemplate.LoadByAccountID(model.AccountID);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult showTemplate(long id)
  {
    EmailTemplate model = EmailTemplate.Load(id);
    return !MonnitSession.CurrentCustomer.IsAdmin || model == null || model.AccountID != MonnitSession.CurrentCustomer.AccountID && !MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.IsCurrentCustomerAccountThemeAdmin ? (ActionResult) this.Content("Failed") : (ActionResult) this.PartialView("EmailTemplate", (object) model);
  }

  [AuthorizeDefault]
  public ActionResult TemplateCreate(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.IsCurrentCustomerAccountThemeAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    return (ActionResult) this.View((object) new EmailTemplate()
    {
      AccountID = id
    });
  }

  [HttpPost]
  [AuthorizeDefault]
  [ValidateInput(false)]
  public ActionResult TemplateCreate(long id, EmailTemplate emailTemplate)
  {
    if (!MonnitSession.CurrentCustomer.IsAdmin && !MonnitSession.IsCurrentCustomerAccountThemeAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (string.IsNullOrEmpty(emailTemplate.Flags))
      this.ModelState.AddModelError("Flags", "One or more flags are required!");
    if (emailTemplate.Name.HasScriptTag())
      this.ModelState.AddModelError("Name", "Script tags not permitted");
    if (emailTemplate.Subject.HasScriptTag())
      this.ModelState.AddModelError("Subject", "Script tags not permitted");
    try
    {
      if (!this.ModelState.IsValid)
        return (ActionResult) this.Content("Failed");
      emailTemplate.AccountID = id;
      emailTemplate.Save();
      EmailTemplate.ClearCache(emailTemplate.AccountID);
      return (ActionResult) this.Content("Success");
    }
    catch (InvalidOperationException ex)
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [HttpPost]
  [AuthorizeDefault]
  public ActionResult TemplateDelete(long id)
  {
    EmailTemplate emailTemplate = EmailTemplate.Load(id);
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin || emailTemplate == null)
      return (ActionResult) this.Content("Failed: Not Authorized");
    emailTemplate.Delete();
    EmailTemplate.ClearCache(emailTemplate.AccountID);
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult TemplateEdit(long id)
  {
    EmailTemplate model = EmailTemplate.Load(id);
    return !MonnitSession.CurrentCustomer.IsAdmin || model == null || model.AccountID != MonnitSession.CurrentCustomer.AccountID && !MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.IsCurrentCustomerAccountThemeAdmin ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [AuthorizeDefault]
  [ValidateInput(false)]
  public ActionResult TemplateEdit(long id, FormCollection collection)
  {
    EmailTemplate model = EmailTemplate.Load(id);
    if (!MonnitSession.CurrentCustomer.IsAdmin || model == null || model.AccountID != MonnitSession.CurrentCustomer.AccountID && !MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.IsCurrentCustomerAccountThemeAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    try
    {
      collection["Name"] = collection["Name"].SanitizeHTMLEvent();
      collection["Subject"] = collection["Subject"].SanitizeHTMLEvent();
      collection["Template"] = collection["Template"].SanitizeHTMLEvent();
      collection["Flags"] = collection["Template"].SanitizeHTMLEvent();
      if (!this.ModelState.IsValid)
        return (ActionResult) this.Content("Failed");
      this.UpdateModel<EmailTemplate>(model);
      model.Save();
      EmailTemplate.ClearCache(model.AccountID);
      return (ActionResult) this.Content("Success");
    }
    catch (InvalidOperationException ex)
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult AdminMaintenanceWindows()
  {
    return (ActionResult) this.View((object) MaintenanceWindow.LoadFuture());
  }

  [AuthorizeDefault]
  public ActionResult AdminMaintenanceWindowsEdit(long? id)
  {
    if (!id.HasValue)
      return (ActionResult) this.View((object) new MaintenanceWindow());
    MaintenanceWindow model = MaintenanceWindow.Load(id ?? long.MinValue);
    try
    {
      if (model == null || !MonnitSession.IsCurrentCustomerMonnitAdmin && MonnitSession.CurrentTheme.AccountID != MonnitSession.CurrentCustomer.AccountID)
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index",
          controller = "Overview"
        });
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__105.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__105.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__105.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__105.\u003C\u003Ep__0, this.ViewBag, "Success");
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__105.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__105.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__105.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__105.\u003C\u003Ep__1, this.ViewBag, ex.Message);
    }
    return (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [ValidateInput(false)]
  [AuthorizeDefault]
  public ActionResult AdminMaintenanceWindowsEdit(long? id, FormCollection collection)
  {
    MaintenanceWindow maintenanceWindow1 = !id.HasValue ? new MaintenanceWindow() : MaintenanceWindow.Load(id ?? long.MinValue);
    try
    {
      if (maintenanceWindow1 == null || !MonnitSession.IsCurrentCustomerMonnitAdmin && MonnitSession.CurrentTheme.AccountID != MonnitSession.CurrentCustomer.AccountID)
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index",
          controller = "Overview"
        });
      maintenanceWindow1.Description = collection["Description"];
      maintenanceWindow1.EmailBody = collection["EmailBody"];
      maintenanceWindow1.DisplayDate = collection["displayDateUTC"].ToDateTime() == DateTime.MinValue ? maintenanceWindow1.DisplayDate : collection["displayDateUTC"].ToDateTime();
      maintenanceWindow1.StartDate = collection["startDateUTC"].ToDateTime() == DateTime.MinValue ? maintenanceWindow1.StartDate : collection["startDateUTC"].ToDateTime();
      maintenanceWindow1.HideDate = collection["endDateUTC"].ToDateTime() == DateTime.MinValue ? maintenanceWindow1.HideDate : collection["endDateUTC"].ToDateTime();
      maintenanceWindow1.SeverityLevel = collection["SeverityLevel"].ToLong();
      MaintenanceWindow maintenanceWindow2 = maintenanceWindow1;
      DateTime dateTime1 = maintenanceWindow1.DisplayDate;
      int year1 = dateTime1.Year;
      dateTime1 = maintenanceWindow1.DisplayDate;
      int month1 = dateTime1.Month;
      dateTime1 = maintenanceWindow1.DisplayDate;
      int day1 = dateTime1.Day;
      dateTime1 = maintenanceWindow1.DisplayDate;
      int hour1 = dateTime1.Hour;
      dateTime1 = maintenanceWindow1.DisplayDate;
      int minute1 = dateTime1.Minute;
      DateTime dateTime2 = new DateTime(year1, month1, day1, hour1, minute1, 0);
      maintenanceWindow2.DisplayDate = dateTime2;
      MaintenanceWindow maintenanceWindow3 = maintenanceWindow1;
      dateTime1 = maintenanceWindow1.StartDate;
      int year2 = dateTime1.Year;
      dateTime1 = maintenanceWindow1.StartDate;
      int month2 = dateTime1.Month;
      dateTime1 = maintenanceWindow1.StartDate;
      int day2 = dateTime1.Day;
      dateTime1 = maintenanceWindow1.StartDate;
      int hour2 = dateTime1.Hour;
      dateTime1 = maintenanceWindow1.StartDate;
      int minute2 = dateTime1.Minute;
      DateTime dateTime3 = new DateTime(year2, month2, day2, hour2, minute2, 0);
      maintenanceWindow3.StartDate = dateTime3;
      MaintenanceWindow maintenanceWindow4 = maintenanceWindow1;
      dateTime1 = maintenanceWindow1.HideDate;
      int year3 = dateTime1.Year;
      dateTime1 = maintenanceWindow1.HideDate;
      int month3 = dateTime1.Month;
      dateTime1 = maintenanceWindow1.HideDate;
      int day3 = dateTime1.Day;
      dateTime1 = maintenanceWindow1.HideDate;
      int hour3 = dateTime1.Hour;
      dateTime1 = maintenanceWindow1.HideDate;
      int minute3 = dateTime1.Minute;
      DateTime dateTime4 = new DateTime(year3, month3, day3, hour3, minute3, 0);
      maintenanceWindow4.HideDate = dateTime4;
      maintenanceWindow1.Save();
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__106.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__106.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__106.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__106.\u003C\u003Ep__0, this.ViewBag, "Success");
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__106.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__106.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__106.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__106.\u003C\u003Ep__1, this.ViewBag, ex.Message);
    }
    return (ActionResult) this.Redirect("../AdminMaintenanceWindows");
  }

  [AuthorizeDefault]
  public ActionResult AdminMaintenanceWindowsOverride(long id)
  {
    MaintenanceWindow model = MaintenanceWindow.Load(id);
    try
    {
      if (model == null || !MonnitSession.IsCurrentCustomerMonnitAdmin && MonnitSession.CurrentTheme.AccountID != MonnitSession.CurrentCustomer.AccountID)
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index",
          controller = "Overview"
        });
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__107.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__107.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__107.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__107.\u003C\u003Ep__0, this.ViewBag, "Success");
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__107.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__107.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__107.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__107.\u003C\u003Ep__1, this.ViewBag, ex.Message);
    }
    return (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [ValidateInput(false)]
  [AuthorizeDefault]
  public ActionResult AdminMaintenanceWindowsOverride(long id, FormCollection collection)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && MonnitSession.CurrentTheme.AccountID != MonnitSession.CurrentCustomer.AccountID)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    AccountThemeMaintenanceLink themeMaintenanceLink = AccountThemeMaintenanceLink.Load(collection["AccountThemeMaintenanceLinkID"].ToLong()) ?? new AccountThemeMaintenanceLink();
    themeMaintenanceLink.AccountThemeID = (long) collection["AccountThemeID"].ToInt();
    themeMaintenanceLink.MaintenanceWindowID = (long) collection["MaintenanceWindowID"].ToInt();
    if (!string.IsNullOrEmpty(collection["Description"]))
      themeMaintenanceLink.OverriddenNote = collection["Description"].ToString();
    if (!string.IsNullOrEmpty(collection["EmailBody"]))
      themeMaintenanceLink.OverriddenEmail = collection["EmailBody"].ToString();
    themeMaintenanceLink.Save();
    return (ActionResult) this.View("AdminMaintenanceWindows", (object) MaintenanceWindow.LoadFuture());
  }

  [AuthorizeDefault]
  public ActionResult MaintenanceOverrideRemove(long id)
  {
    try
    {
      AccountThemeMaintenanceLink themeMaintenanceLink = AccountThemeMaintenanceLink.Load(id);
      if (id < 0L)
        return (ActionResult) this.Content("Failed");
      themeMaintenanceLink.Delete();
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content(ex.Message);
    }
  }

  [AuthorizeDefault]
  public ActionResult AdminAnnouncementIndex()
  {
    return (ActionResult) this.View((object) Announcement.LoadAll());
  }

  public ActionResult AdminAnnouncementEdit(long? id)
  {
    Announcement model = new Announcement();
    long? nullable = id;
    long num = 0;
    if (nullable.GetValueOrDefault() > num & nullable.HasValue)
      model = Announcement.Load(id.Value);
    return (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [ValidateInput(false)]
  [AuthorizeDefault]
  public ActionResult AdminAnnouncementEdit(FormCollection collection)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    List<Announcement> source = Announcement.LoadByThemeID(collection["AccountThemeID"].ToLong());
    try
    {
      Announcement model = new Announcement();
      long id = collection["AccouncementID"].ToLong();
      if (id > 0L)
        model = Announcement.Load(id);
      model.AccountThemeID = collection["AccountThemeID"] != string.Empty ? collection["AccountThemeID"].ToLong() : long.MinValue;
      model.Subject = collection["Subject"];
      model.Title = collection["Title"];
      model.Content = collection["Content"];
      model.Image = collection["Image"];
      model.Link = string.IsNullOrEmpty(collection["Link"]) ? (string) null : collection["Link"];
      model.ReleaseDate = collection["ReleaseDate"].ToDateTime();
      model.IsDeleted = false;
      if (source.Any<Announcement>((System.Func<Announcement, bool>) (item => item.ReleaseDate == collection["ReleaseDate"].ToDateTime() && item.AnnouncementID != id)))
      {
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__112.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__112.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "error", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = SettingsController.\u003C\u003Eo__112.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__112.\u003C\u003Ep__0, this.ViewBag, "Failed to add Announcement. Another Announcement has the same date.");
        return (ActionResult) this.View((object) model);
      }
      model.Save();
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__112.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__112.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SettingsController.\u003C\u003Eo__112.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__112.\u003C\u003Ep__1, this.ViewBag, "Success");
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__112.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__112.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__112.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__112.\u003C\u003Ep__2, this.ViewBag, ex.Message);
    }
    return (ActionResult) this.View("AdminAnnouncementIndex", (object) Announcement.LoadAll());
  }

  public ActionResult ReleaseNotesIndex()
  {
    if (MonnitSession.CurrentTheme.Theme != "Default")
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    List<ChangeLog> changeLogList = new List<ChangeLog>();
    List<ChangeLog> model = !MonnitSession.IsCurrentCustomerMonnitSuperAdmin ? ChangeLog.LoadRecentPublished() : ChangeLog.LoadAll();
    if (model.Count > 0)
      model.Sort((Comparison<ChangeLog>) ((x, y) => y.ReleaseDate.CompareTo(x.ReleaseDate)));
    return (ActionResult) this.View((object) model);
  }

  public ActionResult ReleaseNotesEdit(long? id)
  {
    if (!MonnitSession.CustomerCan("Can_Edit_ReleaseNotes"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__114.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__114.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "themeID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__114.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__114.\u003C\u003Ep__0, this.ViewBag, MonnitSession.CurrentTheme.AccountThemeID);
    ChangeLog model = ChangeLog.Load(id ?? long.MinValue);
    if (model == null)
    {
      model = new ChangeLog();
      model.ReleaseDate = DateTime.Today;
      model.ChangeLogID = 0L;
    }
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__114.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__114.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ChangeLogID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__114.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__114.\u003C\u003Ep__1, this.ViewBag, model.ChangeLogID);
    return (ActionResult) this.View((object) model);
  }

  public ActionResult PublishReleaseNote(long id, bool value)
  {
    if (id <= 0L && !MonnitSession.CustomerCan("Can_Edit_ReleaseNotes"))
      return (ActionResult) this.Content("Must create release note before publishing.");
    try
    {
      ChangeLog changeLog = ChangeLog.Load(id);
      changeLog.isPublished = value;
      changeLog.Save();
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content("Unable to publish release note");
    }
  }

  public ActionResult RemoveReleaseNoteItem(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin && !MonnitSession.CustomerCan("Can_Edit_ReleaseNotes"))
      return (ActionResult) this.Content("Failed to remove, you do not have permission.");
    try
    {
      ChangeLogItem.Load(id).Delete();
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content(ex.Message);
    }
  }

  public ActionResult RemoveReleaseNote(long id)
  {
    try
    {
      ChangeLog.Load(id).Delete();
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content(ex.Message);
    }
  }

  [HttpPost]
  [ValidateInput(false)]
  public ActionResult ReleaseNotesForm(FormCollection collection)
  {
    try
    {
      ChangeLog changeLog = new ChangeLog();
      long id = collection["ChangeLogID"].ToLong();
      if (id > 0L)
        changeLog = ChangeLog.Load(id);
      changeLog.Version = collection["Version"];
      changeLog.ReleaseDate = collection["ReleaseDate"].ToDateTime().Date;
      changeLog.isPublished = collection["isPublished"] == "on";
      changeLog.Save();
      return (ActionResult) this.Content($"{changeLog.ChangeLogID}");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content("Note failed to add, " + ex.Message);
    }
  }

  [HttpPost]
  [ValidateInput(false)]
  public ActionResult ReleaseNoteItemForm(FormCollection collection, long id)
  {
    string content = (string) null;
    try
    {
      if (string.IsNullOrEmpty(collection["Details"].ToString()))
        content = "Note failed to add, please provide a value for note.";
      if (string.IsNullOrEmpty(collection["Heading"].ToString()))
        content = "Note failed to add, please provide a value for heading.";
      if (string.IsNullOrEmpty(collection["Type"].ToString()))
        content = "Note failed to add, please provide a value for type.";
      if (content != null)
        return (ActionResult) this.Content(content);
      ChangeLog model = ChangeLog.Load(id);
      ChangeLogItem changeLogItem = new ChangeLogItem();
      long id1 = collection["ChangeLogItemID"].ToLong();
      if (id1 > 0L)
        changeLogItem = ChangeLogItem.Load(id1);
      changeLogItem.ChangeLogID = model.PrimaryKeyValue;
      changeLogItem.Details = collection["Details"];
      changeLogItem.Type = collection["Type"];
      changeLogItem.Heading = collection["Heading"];
      if (!changeLogItem.Details.Contains("data-cke-filler"))
        changeLogItem.Save();
      return (ActionResult) this.View("_ChangeLogsList", (object) model);
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content("Note failed to add, " + ex.Message);
    }
  }

  public ActionResult AdminAnnouncementViewLinkTest(string path)
  {
    return (ActionResult) this.Redirect(path);
  }

  public ActionResult LoadAnnouncementForPopup(
    long customerid,
    long accountthemeid,
    long? announcementid)
  {
    ActionResult actionResult;
    try
    {
      AnnouncementPopup model = Announcement.LoadByCustomerIDProc(customerid, accountthemeid, announcementid.HasValue ? announcementid.Value : long.MinValue);
      model.CustomerViewed = false;
      actionResult = (ActionResult) this.PartialView("_AnnouncementPopup", (object) model);
    }
    catch
    {
      actionResult = (ActionResult) this.Redirect("/Overview/Index/");
    }
    return actionResult;
  }

  public ActionResult AnnouncementPopupDemo(long id)
  {
    Announcement announcement1 = new Announcement();
    AnnouncementPopup model = new AnnouncementPopup();
    ActionResult actionResult;
    try
    {
      Announcement announcement2 = Announcement.Load(id);
      model.AnnouncementObj = announcement2;
      model.CustomerViewed = false;
      model.Prev = long.MinValue;
      model.Next = long.MinValue;
      actionResult = (ActionResult) this.PartialView("_AnnouncementPopup", (object) model);
    }
    catch
    {
      actionResult = (ActionResult) this.Redirect("/Settings/AdminAnnouncementIndex/");
    }
    return actionResult;
  }

  public ActionResult AnnouncementDelete(long id)
  {
    ActionResult actionResult;
    try
    {
      Announcement announcement = Announcement.Load(id);
      foreach (BaseDBObject baseDbObject in AnnouncementViewed.LoadAllByAnnouncementID(id))
        baseDbObject.Delete();
      announcement.Delete();
      actionResult = (ActionResult) this.Content("Success");
    }
    catch
    {
      actionResult = (ActionResult) this.Content("Failed");
    }
    return actionResult;
  }

  public void MarkReadByCustomerID(long id, long accountthemeid)
  {
    Announcement.MarkReadByCustomerID(id, accountthemeid);
    MonnitSession.NewestAnnouncement = (AnnouncementPopup) null;
  }

  [AuthorizeDefault]
  public ActionResult AdminMassEmail()
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
      return (ActionResult) this.RedirectPermanent("/Overview");
    List<SettingsController.MassEmailContact> model = new List<SettingsController.MassEmailContact>();
    foreach (AccountTheme at in AccountTheme.LoadAll().Where<AccountTheme>((System.Func<AccountTheme, bool>) (c => c.IsActive)))
    {
      SettingsController.MassEmailContact massEmailContact = new SettingsController.MassEmailContact(at);
      model.Add(massEmailContact);
    }
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult MassEmailContactList(long ThemeID)
  {
    return !MonnitSession.IsCurrentCustomerMonnitSuperAdmin ? (ActionResult) this.RedirectPermanent("/Overview") : (ActionResult) this.PartialView("_AdminMassEmailContactList", (object) new SettingsController.MassEmailContact(AccountTheme.Load(ThemeID)));
  }

  [HttpPost]
  public ActionResult MassEmailContent(string flag)
  {
    eEmailTemplateFlag flag1 = (eEmailTemplateFlag) System.Enum.Parse(typeof (eEmailTemplateFlag), flag, true);
    return !MonnitSession.IsCurrentCustomerMonnitSuperAdmin ? (ActionResult) this.RedirectPermanent("/Overview") : (ActionResult) this.PartialView("_AdminMassEmailContent", (object) EmailTemplate.LoadByAccountAndFlag(MonnitSession.CurrentCustomer.AccountID, flag1));
  }

  [HttpPost]
  public ActionResult MassEmailBody()
  {
    return (ActionResult) this.PartialView((object) new EmailTemplate());
  }

  [HttpPost]
  [ValidateInput(false)]
  public ActionResult buildEmail(string Subject, string EditorData, string IDs)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
      return (ActionResult) this.Content("Unauthorized");
    IDs = IDs.TrimEnd('|');
    string str = IDs;
    char[] chArray = new char[1]{ '|' };
    foreach (string o in str.Split(chArray))
    {
      try
      {
        SystemEmail systemEmail1 = new SystemEmail();
        SystemEmailContent systemEmailContent = new SystemEmailContent();
        systemEmailContent.ModifiedDate = DateTime.UtcNow;
        systemEmailContent.Subject = Subject;
        systemEmailContent.Body = EditorData.SanitizeButAllowSomeHTMLEvent();
        systemEmailContent.TemplateFlag = "Generic";
        systemEmailContent.Save();
        List<AccountThemeContact> accountThemeContactList = AccountThemeContact.LoadByAccountThemeID((long) o.ToInt());
        if (accountThemeContactList.Count > 0)
        {
          foreach (AccountThemeContact accountThemeContact in accountThemeContactList)
          {
            SystemEmail systemEmail2 = systemEmail1;
            systemEmail2.ToAddress = $"{systemEmail2.ToAddress}{accountThemeContact.Email}; ";
          }
          systemEmail1.CreateDate = DateTime.UtcNow;
          systemEmail1.Status = "New";
          systemEmail1.DoRetry = true;
          systemEmail1.RetryCount = 0;
          systemEmail1.SystemEmailContentID = systemEmailContent.SystemEmailContentID;
          systemEmail1.Save();
        }
      }
      catch (Exception ex)
      {
        ex.Log("AccountControler.buildEmail  Failed to add to SystemEmail");
        return (ActionResult) this.Content("Failed");
      }
    }
    return (ActionResult) this.Content("Success: Email Queued");
  }

  public void AddTimeDropDowns() => this.AddTimeDropDowns(new TimeSpan(0L), new TimeSpan(0L));

  public void AddTimeDropDowns(TimeSpan fromTime, TimeSpan endTime)
  {
    string[] items1 = new string[24]
    {
      "0",
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
      "12",
      "13",
      "14",
      "15",
      "16",
      "17",
      "18",
      "19",
      "20",
      "21",
      "22",
      "23"
    };
    string[] items2 = new string[12]
    {
      "00",
      "05",
      "10",
      "15",
      "20",
      "25",
      "30",
      "35",
      "40",
      "45",
      "50",
      "55"
    };
    this.ViewData["FromHours"] = (object) new SelectList((IEnumerable) items1);
    this.ViewData["FromMinutes"] = (object) new SelectList((IEnumerable) items2, (object) fromTime.Minutes.ToString());
    this.ViewData["ToHours"] = (object) new SelectList((IEnumerable) items1);
    this.ViewData["ToMinutes"] = (object) new SelectList((IEnumerable) items2, (object) endTime.Minutes.ToString());
  }

  [AuthorizeDefault]
  public ActionResult DataPacketIn(long? id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CustomerCan("Support_Advanced"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    InboundPacketModel model = new InboundPacketModel();
    DateTime localTimeById = Monnit.TimeZone.GetLocalTimeById(DateTime.UtcNow, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    model.TimeOffset = (double) localTimeById.Subtract(DateTime.UtcNow).Hours;
    long? nullable = id;
    long num = 0;
    if (nullable.GetValueOrDefault() > num & nullable.HasValue)
    {
      model.GatewayID = id;
      InboundPacketModel inboundPacketModel = model;
      long? gatewayId = model.GatewayID;
      nullable = new long?();
      long? cSNetID = nullable;
      DateTime fromDate = model.FromDate;
      DateTime toDate = model.ToDate;
      int count = model.Count;
      int? response = new int?(model.Response);
      List<InboundPacket> inboundPacketList = InboundPacket.LoadByFilter(gatewayId, cSNetID, fromDate, toDate, count, response);
      inboundPacketModel.InboundPackets = inboundPacketList;
    }
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateInput(false)]
  [ValidateAntiForgeryToken]
  public ActionResult DataPacketIn(InboundPacketModel model, string FromDateStr, string ToDateStr)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CustomerCan("Support_Advanced"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    try
    {
      model.FromDate = DateTime.ParseExact(FromDateStr, $"{MonnitSession.CurrentCustomer.Preferences["Date Format"].ToString()} {MonnitSession.CurrentCustomer.Preferences["Time Format"].ToString()}", (IFormatProvider) CultureInfo.InvariantCulture);
      model.FromDate = model.FromDate.AddHours(-1.0 * model.TimeOffset);
      model.ToDate = DateTime.ParseExact(ToDateStr, $"{MonnitSession.CurrentCustomer.Preferences["Date Format"].ToString()} {MonnitSession.CurrentCustomer.Preferences["Time Format"].ToString()}", (IFormatProvider) CultureInfo.InvariantCulture);
      model.ToDate = model.ToDate.AddHours(-1.0 * model.TimeOffset);
    }
    catch
    {
      InboundPacketModel inboundPacketModel1 = model;
      DateTime dateTime1 = model.FromDate;
      DateTime dateTime2 = dateTime1.AddHours(-1.0 * model.TimeOffset);
      inboundPacketModel1.FromDate = dateTime2;
      InboundPacketModel inboundPacketModel2 = model;
      dateTime1 = model.ToDate;
      DateTime dateTime3 = dateTime1.AddHours(-1.0 * model.TimeOffset);
      inboundPacketModel2.ToDate = dateTime3;
    }
    this.Session["PacketGatewayID"] = (object) model.GatewayID;
    if (this.ModelState.IsValid)
      model.InboundPackets = InboundPacket.LoadByFilter(model.GatewayID, new long?(), model.FromDate, model.ToDate, model.Count, new int?(model.Response));
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult DataPacketOut()
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CustomerCan("Support_Advanced"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    DateTime dateTime1 = DateTime.Now;
    DateTime date = dateTime1.Date;
    dateTime1 = DateTime.Now;
    dateTime1 = dateTime1.Date;
    dateTime1 = dateTime1.AddHours(23.0);
    DateTime dateTime2 = dateTime1.AddMinutes(59.0);
    string str1 = date.ToString("yyyy-MM-dd-hh-mm");
    string str2 = dateTime2.ToString("yyyy-MM-dd-hh-mm");
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "from", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__0, this.ViewBag, str1);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "to", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__1, this.ViewBag, str2);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Displayfrom", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__2, this.ViewBag, date.ToString("MM/dd/yyyy hh:mm tt"));
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Displayto", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj4 = SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__3.Target((CallSite) SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__3, this.ViewBag, dateTime2.ToString("MM/dd/yyyy hh:mm tt"));
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Count", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj5 = SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__4.Target((CallSite) SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__4, this.ViewBag, 10);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj6 = SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__5.Target((CallSite) SettingsController.\u003C\u003Eo__136.\u003C\u003Ep__5, this.ViewBag, int.MinValue);
    this.AddTimeDropDowns();
    return (ActionResult) this.View((object) new List<OutboundPacket>());
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateInput(false)]
  [ValidateAntiForgeryToken]
  public ActionResult DataPacketOut(FormCollection collection)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CustomerCan("Support_Advanced"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    DateTime dateTime1 = new DateTime();
    DateTime dateTime2 = new DateTime();
    DateTime fromDate;
    DateTime toDate;
    if (string.IsNullOrEmpty(collection["startDate"]) || string.IsNullOrEmpty(collection["endDate"]))
    {
      DateTime dateTime3 = DateTime.Now;
      fromDate = dateTime3.Date;
      dateTime3 = DateTime.Now;
      dateTime3 = dateTime3.Date;
      toDate = dateTime3.AddDays(1.0);
    }
    else
    {
      try
      {
        fromDate = DateTime.ParseExact(collection["startDate"], $"{MonnitSession.CurrentCustomer.Preferences["Date Format"].ToString()} {MonnitSession.CurrentCustomer.Preferences["Time Format"].ToString()}", (IFormatProvider) CultureInfo.InvariantCulture);
        toDate = DateTime.ParseExact(collection["endDate"], $"{MonnitSession.CurrentCustomer.Preferences["Date Format"].ToString()} {MonnitSession.CurrentCustomer.Preferences["Time Format"].ToString()}", (IFormatProvider) CultureInfo.InvariantCulture);
      }
      catch
      {
        fromDate = collection["startDate"].ToDateTime();
        toDate = collection["endDate"].ToDateTime();
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Displayfrom", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__0, this.ViewBag, fromDate.ToString("MM/dd/yyyy hh:mm tt"));
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Displayto", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__1, this.ViewBag, toDate.ToString("MM/dd/yyyy hh:mm tt"));
    string str1 = fromDate.ToString("yyyy-MM-dd-hh-mm");
    string str2 = toDate.ToString("yyyy-MM-dd-hh-mm");
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "from", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__2, this.ViewBag, str1);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "to", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj4 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__3.Target((CallSite) SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__3, this.ViewBag, str2);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "GatewayID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj5 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__4.Target((CallSite) SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__4, this.ViewBag, collection["GatewayID"]);
    this.Session["PacketGatewayID"] = (object) collection["GatewayID"];
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Search", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj6 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__5.Target((CallSite) SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__5, this.ViewBag, collection["Search"]);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__6 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Count", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj7 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__6.Target((CallSite) SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__6, this.ViewBag, collection["Count"]);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__7 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj8 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__7.Target((CallSite) SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__7, this.ViewBag, collection["Response"]);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__8 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TimeFormat", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj9 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__8.Target((CallSite) SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__8, this.ViewBag, collection["TimeFormat"].ToInt());
    if (this.ModelState.IsValid)
    {
      try
      {
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__12 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, DateTime>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (DateTime), typeof (SettingsController)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, DateTime> target1 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__12.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, DateTime>> p12 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__12;
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__11 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__11 = CallSite<\u003C\u003EF\u007B00000008\u007D<CallSite, DateTime, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "AddHours", (IEnumerable<Type>) null, typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsRef, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: variable of a compiler-generated type
        \u003C\u003EF\u007B00000008\u007D<CallSite, DateTime, object, object> target2 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__11.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<\u003C\u003EF\u007B00000008\u007D<CallSite, DateTime, object, object>> p11 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__11;
        ref DateTime local1 = ref fromDate;
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__10 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__10 = CallSite<Func<CallSite, int, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Multiply, typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, int, object, object> target3 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__10.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, int, object, object>> p10 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__10;
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__9 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "TimeFormat", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj10 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__9.Target((CallSite) SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__9, this.ViewBag);
        object obj11 = target3((CallSite) p10, -1, obj10);
        object obj12 = target2((CallSite) p11, ref local1, obj11);
        fromDate = target1((CallSite) p12, obj12);
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__16 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__16 = CallSite<Func<CallSite, object, DateTime>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (DateTime), typeof (SettingsController)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, DateTime> target4 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__16.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, DateTime>> p16 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__16;
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__15 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__15 = CallSite<\u003C\u003EF\u007B00000008\u007D<CallSite, DateTime, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "AddHours", (IEnumerable<Type>) null, typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsRef, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: variable of a compiler-generated type
        \u003C\u003EF\u007B00000008\u007D<CallSite, DateTime, object, object> target5 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__15.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<\u003C\u003EF\u007B00000008\u007D<CallSite, DateTime, object, object>> p15 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__15;
        ref DateTime local2 = ref toDate;
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__14 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__14 = CallSite<Func<CallSite, int, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Multiply, typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, int, object, object> target6 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__14.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, int, object, object>> p14 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__14;
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__13 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__13 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "TimeFormat", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj13 = SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__13.Target((CallSite) SettingsController.\u003C\u003Eo__137.\u003C\u003Ep__13, this.ViewBag);
        object obj14 = target6((CallSite) p14, -1, obj13);
        object obj15 = target5((CallSite) p15, ref local2, obj14);
        toDate = target4((CallSite) p16, obj15);
        return (ActionResult) this.View((object) OutboundPacket.LoadByFilter(new long?(collection["GatewayID"].ToLong()), "", fromDate, toDate, collection["Count"].ToInt(), new int?(collection["Response"].ToInt())).ToList<OutboundPacket>());
      }
      catch (Exception ex)
      {
        ex.Log("SettingsController.DataPacketOut(FormCollection collection)");
      }
    }
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateInput(false)]
  [ValidateAntiForgeryToken]
  public ActionResult DataPacketInCSV(InboundPacketModel model)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CustomerCan("Support_Advanced"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    try
    {
      model.FromDate = model.FromDate.AddHours(-1.0 * model.TimeOffset);
      model.ToDate = model.ToDate.AddHours(-1.0 * model.TimeOffset);
      this.Session["PacketGatewayID"] = (object) model.GatewayID;
      if (this.ModelState.IsValid)
        model.InboundPackets = InboundPacket.LoadByFilter(model.GatewayID, new long?(), model.FromDate, model.ToDate, model.Count, new int?(model.Response));
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      StringBuilder stringBuilder3 = new StringBuilder();
      StringBuilder stringBuilder4 = new StringBuilder();
      string str1 = "Gateway Index, Inbound Packet GUID, Received Date, Gateway ID, Type, Count, Power, Security, More, Raw";
      string str2 = "Gateway Index, Sensor Index, Message Date, Sensor ID, Type, Urgent/ParentID, RSSI1, RSSI2, Voltage, ProfileID, State, Message, Raw";
      stringBuilder1.Append(str1 + "\r\n");
      stringBuilder2.Append(str2 + "\r\n");
      stringBuilder3.Append(str1);
      stringBuilder3.Append($",{str2}\r\n");
      stringBuilder4.Append(str1);
      stringBuilder4.Append($",{str2}\r\n");
      int GatewayIndex1 = 0;
      foreach (InboundPacket inboundPacket in model.InboundPackets)
      {
        int SensorIndex1 = 0;
        ++GatewayIndex1;
        int Index = GatewayIndex1;
        Guid inboundPacketGuid = inboundPacket.InboundPacketGUID;
        DateTime dateTime = inboundPacket.ReceivedDate;
        DateTime Date1 = dateTime.AddHours(model.TimeOffset);
        long apnid = inboundPacket.APNID;
        int response = inboundPacket.Response;
        int messageCount = inboundPacket.MessageCount;
        int power = inboundPacket.Power;
        int num1 = inboundPacket.Security > 1L ? 1 : 0;
        int num2 = inboundPacket.More ? 1 : 0;
        string hex1 = inboundPacket.Message.ToHex();
        string str3 = this.GatewayMessageRowCSV(Index, inboundPacketGuid, Date1, apnid, response, messageCount, power, num1 != 0, num2 != 0, hex1);
        stringBuilder1.Append(str3);
        stringBuilder3.Append(str3);
        stringBuilder4.Append(str3);
        if (inboundPacket.Response == 0)
        {
          int sourceIndex = 0;
          while (inboundPacket.Payload.Length > sourceIndex + 5)
          {
            string empty = string.Empty;
            int num3 = inboundPacket.Payload[sourceIndex + 5].ToInt();
            byte[] numArray1 = new byte[num3 + 7];
            if (inboundPacket.Payload.Length >= sourceIndex + num3 + 7)
            {
              Array.Copy((Array) inboundPacket.Payload, sourceIndex, (Array) numArray1, 0, num3 + 7);
              sourceIndex += num3 + 7;
              long num4 = 0;
              if (numArray1.Length >= 12)
                num4 = (long) BitConverter.ToUInt32(numArray1, 8);
              long? sensorId = model.SensorID;
              long minValue = long.MinValue;
              int num5;
              if (sensorId.GetValueOrDefault() > minValue & sensorId.HasValue)
              {
                sensorId = model.SensorID;
                long num6 = num4;
                num5 = !(sensorId.GetValueOrDefault() == num6 & sensorId.HasValue) ? 1 : 0;
              }
              else
                num5 = 0;
              if (num5 == 0 && num3 >= 0)
              {
                dateTime = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime time1 = dateTime.AddSeconds((double) BitConverter.ToUInt32(numArray1, 0));
                ++SensorIndex1;
                empty = string.Empty;
                int num7;
                string str4;
                uint uint32;
                switch (numArray1[7])
                {
                  case 37:
                    str4 = this.SensorMessageRowCSV(GatewayIndex1, SensorIndex1, this.DataPacketTime(time1, model.TimeOffset), this.DataPacketID(numArray1), this.DataPacketType("Join " + (numArray1[14] == (byte) 0 ? "Allowed" : "Prohibited"), numArray1[7]), "", "", "", "", "", "", "", numArray1.ToHex());
                    break;
                  case 82:
                    int GatewayIndex2 = GatewayIndex1;
                    int SensorIndex2 = SensorIndex1;
                    string Date2 = this.DataPacketTime(time1, model.TimeOffset);
                    string SensorID1 = this.DataPacketID(numArray1);
                    string Type1 = this.DataPacketType("Parent Device:", numArray1[7]);
                    uint32 = BitConverter.ToUInt32(numArray1, 12);
                    string Urgent1 = uint32.ToString();
                    string hex2 = numArray1.ToHex();
                    str4 = this.SensorMessageRowCSV(GatewayIndex2, SensorIndex2, Date2, SensorID1, Type1, Urgent1, "", "", "", "", "", "", hex2);
                    break;
                  case 83:
                    byte[] numArray2 = new byte[numArray1.Length - 12];
                    Array.Copy((Array) numArray1, 12, (Array) numArray2, 0, numArray1.Length - 12);
                    str4 = this.SensorMessageRowCSV(GatewayIndex1, SensorIndex1, this.DataPacketTime(time1, model.TimeOffset), this.DataPacketID(numArray1), this.DataPacketType("Sensor Status", numArray1[7]), "", "", "", "", "", "", this.DataPacketMessage(numArray2), numArray1.ToHex());
                    break;
                  case 85:
                    byte[] numArray3 = new byte[num3 - 12];
                    Array.Copy((Array) numArray1, 18, (Array) numArray3, 0, num3 - 12);
                    int GatewayIndex3 = GatewayIndex1;
                    int SensorIndex3 = SensorIndex1;
                    string Date3 = this.DataPacketTime(time1, model.TimeOffset);
                    string SensorID2 = this.DataPacketID(numArray1);
                    string Type2 = this.DataPacketType("Data Message", numArray1[7]);
                    string Urgent2 = this.DataPacketUrgent(numArray1[6]);
                    num7 = (int) numArray1[12] - 256 /*0x0100*/;
                    string RSSI1_1 = num7.ToString();
                    num7 = (int) numArray1[13] - 256 /*0x0100*/;
                    string RSSI2_1 = num7.ToString();
                    string Voltage1 = this.DataPacketVoltage(numArray1[14]);
                    string ProfileID1 = this.DataPacketProfileID(numArray1, 15);
                    string State1 = this.DataPacketState(numArray1[17]);
                    string Message1 = this.DataPacketMessage(numArray3);
                    string hex3 = numArray1.ToHex();
                    str4 = this.SensorMessageRowCSV(GatewayIndex3, SensorIndex3, Date3, SensorID2, Type2, Urgent2, RSSI1_1, RSSI2_1, Voltage1, ProfileID1, State1, Message1, hex3);
                    break;
                  case 86:
                    byte[] numArray4 = new byte[num3 - 16 /*0x10*/];
                    Array.Copy((Array) numArray1, 22, (Array) numArray4, 0, num3 - 16 /*0x10*/);
                    dateTime = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    DateTime time2 = dateTime.AddSeconds((double) BitConverter.ToUInt32(numArray1, 12)).AddHours(model.TimeOffset);
                    int GatewayIndex4 = GatewayIndex1;
                    int SensorIndex4 = SensorIndex1;
                    string Date4 = this.DataPacketTime(time2);
                    string SensorID3 = this.DataPacketID(numArray1);
                    string Type3 = this.DataPacketType("Data Message DL", numArray1[7]);
                    string Urgent3 = this.DataPacketUrgent(numArray1[6]);
                    string RSSI1_2;
                    if (numArray1[16 /*0x10*/] != (byte) 0)
                    {
                      num7 = (int) numArray1[16 /*0x10*/] - 256 /*0x0100*/;
                      RSSI1_2 = num7.ToString();
                    }
                    else
                      RSSI1_2 = "0";
                    string RSSI2_2;
                    if (numArray1[16 /*0x10*/] != (byte) 0)
                    {
                      num7 = (int) numArray1[17] - 256 /*0x0100*/;
                      RSSI2_2 = num7.ToString();
                    }
                    else
                      RSSI2_2 = "0";
                    string Voltage2 = this.DataPacketVoltage(numArray1[18]);
                    string ProfileID2 = this.DataPacketProfileID(numArray1, 19);
                    string State2 = this.DataPacketState(numArray1[21]);
                    string Message2 = this.DataPacketMessage(numArray4);
                    string hex4 = numArray1.ToHex();
                    str4 = this.SensorMessageRowCSV(GatewayIndex4, SensorIndex4, Date4, SensorID3, Type3, Urgent3, RSSI1_2, RSSI2_2, Voltage2, ProfileID2, State2, Message2, hex4);
                    break;
                  case 87:
                    byte[] numArray5 = new byte[num3 - 16 /*0x10*/];
                    Array.Copy((Array) numArray1, 22, (Array) numArray5, 0, num3 - 16 /*0x10*/);
                    dateTime = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    DateTime time3 = dateTime.AddSeconds((double) BitConverter.ToUInt32(numArray1, 12)).AddHours(model.TimeOffset);
                    int GatewayIndex5 = GatewayIndex1;
                    int SensorIndex5 = SensorIndex1;
                    string Date5 = this.DataPacketTime(time3);
                    string SensorID4 = this.DataPacketID(numArray1);
                    string Type4 = this.DataPacketType("Data Message ADL", numArray1[7]);
                    string Urgent4 = this.DataPacketUrgent(numArray1[6]);
                    string RSSI1_3;
                    if (numArray1[20] != (byte) 0)
                    {
                      num7 = (int) numArray1[20] - 256 /*0x0100*/;
                      RSSI1_3 = num7.ToString();
                    }
                    else
                      RSSI1_3 = "0";
                    string Voltage3 = this.DataPacketVoltage(numArray1[21]);
                    string ProfileID3 = this.DataPacketProfileID(numArray1, 22);
                    string State3 = this.DataPacketState(numArray1[24]);
                    string str5 = this.DataPacketMessage(numArray5);
                    uint32 = BitConverter.ToUInt32(numArray1, 16 /*0x10*/);
                    string str6 = uint32.ToString();
                    string Message3 = $"{str5}(Auth Token: {str6})";
                    string hex5 = numArray1.ToHex();
                    str4 = this.SensorMessageRowCSV(GatewayIndex5, SensorIndex5, Date5, SensorID4, Type4, Urgent4, RSSI1_3, "", Voltage3, ProfileID3, State3, Message3, hex5);
                    break;
                  default:
                    byte[] numArray6;
                    if (numArray1.Length - 12 > 0)
                    {
                      numArray6 = new byte[numArray1.Length - 12];
                      Array.Copy((Array) numArray1, 12, (Array) numArray6, 0, numArray1.Length - 12);
                    }
                    else
                      numArray6 = new byte[0];
                    str4 = this.SensorMessageRowCSV(GatewayIndex1, SensorIndex1, this.DataPacketTime(time1, model.TimeOffset), num4.ToString(), this.DataPacketType("Other", numArray1[7]), "", "", "", "", "", "", this.DataPacketMessage(numArray6), numArray1.ToHex());
                    break;
                }
                stringBuilder2.Append(str4 + "\r\n");
                if (SensorIndex1 > 1)
                {
                  stringBuilder3.Append("\r\n");
                  stringBuilder3.Append(str3);
                }
                stringBuilder3.Append(",");
                stringBuilder3.Append(str4);
                if (SensorIndex1 > 1)
                {
                  stringBuilder4.Append("\r\n");
                  stringBuilder4.Append(",,,,,,,,,");
                }
                stringBuilder4.Append(",");
                stringBuilder4.Append(str4);
              }
            }
            else
              break;
          }
        }
        stringBuilder1.Append("\r\n");
        stringBuilder3.Append("\r\n");
        stringBuilder4.Append("\r\n");
      }
      string str7 = $"DataPacketIn-GatewayID({model.GatewayID})-{DateTime.Now.ToString("yyyyMMddHHmmss")}";
      stringBuilder1.Append("\r\n\r\n").Append((object) stringBuilder2);
      return (ActionResult) this.File(Encoding.ASCII.GetBytes(stringBuilder4.ToString()), "text/css", str7 + ".csv");
    }
    catch (Exception ex)
    {
      ex.Log($"SettingsController.DataPacketInCSV[GatewayID: {model.GatewayID.ToString()}] ");
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__138.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__138.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ErrorMessage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__138.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__138.\u003C\u003Ep__0, this.ViewBag, ex.Message);
      return (ActionResult) this.View("Error");
    }
  }

  private string GatewayMessageRowCSV(
    int Index,
    Guid InboundPacketGUID,
    DateTime Date,
    long GatewayID,
    int Type,
    int Count,
    int Power,
    bool Security,
    bool More,
    string Raw)
  {
    return $"{Index},{InboundPacketGUID},{Date},{GatewayID},{Type},{Count},{Power},{Security},{More},{Raw}";
  }

  private string SensorMessageRowCSV(
    int GatewayIndex,
    int SensorIndex,
    string Date,
    string SensorID,
    string Type,
    string Urgent,
    string RSSI1,
    string RSSI2,
    string Voltage,
    string ProfileID,
    string State,
    string Message,
    string Raw)
  {
    return $"{GatewayIndex},{SensorIndex},{Date},{SensorID},{Type},{Urgent},{RSSI1},{RSSI2},{Voltage},{ProfileID},{State},{Message},{Raw}";
  }

  private string DataPacketTime(DateTime time, double offsetHours = 0.0)
  {
    DateTime dateTime = time.AddHours(offsetHours);
    string shortDateString = dateTime.ToShortDateString();
    dateTime = time.AddHours(offsetHours);
    string longTimeString = dateTime.ToLongTimeString();
    return $"{shortDateString} {longTimeString}";
  }

  private string DataPacketID(byte[] messagebuffer)
  {
    return BitConverter.ToUInt32(messagebuffer, 8).ToString();
  }

  private string DataPacketType(string label, byte hex) => $"{label} (0x{hex.ToHex()})";

  private string DataPacketUrgent(byte urgent) => ((int) urgent & 1) == 1 ? "Urgent" : "";

  private string DataPacketVoltage(byte volts) => ((double) ((int) volts + 150) / 100.0).ToString();

  private string DataPacketProfileID(byte[] messagebuffer, int idx)
  {
    return BitConverter.ToUInt16(messagebuffer, idx).ToString();
  }

  private string DataPacketState(byte state) => state.ToString();

  private string DataPacketMessage(byte[] message) => message.ToHex();

  [AuthorizeDefault]
  [HttpPost]
  [ValidateInput(false)]
  [ValidateAntiForgeryToken]
  public ActionResult DataPacketOutCSV(FormCollection collection)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CustomerCan("Support_Advanced"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    DateTime dateTime1 = new DateTime();
    DateTime dateTime2 = new DateTime();
    DateTime dateTime3;
    DateTime dateTime4;
    DateTime dateTime5;
    if (string.IsNullOrEmpty(collection["startDate"]) || string.IsNullOrEmpty(collection["endDate"]))
    {
      dateTime3 = DateTime.Now;
      dateTime4 = dateTime3.Date;
      dateTime3 = DateTime.Now;
      dateTime3 = dateTime3.Date;
      dateTime5 = dateTime3.AddDays(1.0);
    }
    else
    {
      dateTime4 = collection["startDate"].ToDateTime();
      dateTime5 = collection["endDate"].ToDateTime();
    }
    long num1 = collection["GatewayID"].ToLong();
    int num2 = collection["TimeFormat"].ToInt();
    StringBuilder stringBuilder1 = new StringBuilder();
    stringBuilder1.AppendLine("Sent Date, Gateway ID, Type, Count, Payload, Power, More");
    if (this.ModelState.IsValid)
    {
      try
      {
        DateTime fromDate = dateTime4.AddHours((double) (-1 * num2));
        DateTime toDate = dateTime5.AddHours((double) (-1 * num2));
        foreach (OutboundPacket outboundPacket in OutboundPacket.LoadByFilter(new long?(collection["GatewayID"].ToLong()), "", fromDate, toDate, collection["Count"].ToInt(), new int?(collection["Response"].ToInt())))
        {
          int num3 = 0;
          string str = "";
          foreach (byte num4 in outboundPacket.Payload)
          {
            ++num3;
            if (num3 > 2)
              str += num4.ToString("X2").Replace("-", "");
          }
          StringBuilder stringBuilder2 = stringBuilder1;
          object[] objArray = new object[7];
          dateTime3 = outboundPacket.SentDate;
          objArray[0] = (object) dateTime3.AddHours((double) num2);
          objArray[1] = (object) outboundPacket.APNID;
          objArray[2] = (object) outboundPacket.Response;
          objArray[3] = (object) outboundPacket.MessageCount;
          objArray[4] = (object) str;
          objArray[5] = (object) outboundPacket.Power;
          objArray[6] = (object) outboundPacket.More;
          stringBuilder2.AppendFormat("{0},{1},{2},{3},{4},{5},{6}\r\n", objArray);
        }
      }
      catch (Exception ex)
      {
        ex.Log($"SettingsController.DataPacketOutCSV[GatewayID: {collection["GatewayID"]}] ");
      }
    }
    byte[] bytes = Encoding.ASCII.GetBytes(stringBuilder1.ToString());
    // ISSUE: variable of a boxed type
    __Boxed<long> local = (System.ValueType) num1;
    dateTime3 = DateTime.Now;
    string str1 = dateTime3.ToString("yyyyMMdd");
    string fileDownloadName = $"DataPacketOut-GatewayID[{local}]-{str1}.csv";
    return (ActionResult) this.File(bytes, "text/csv", fileDownloadName);
  }

  [AuthorizeDefault]
  [NoCache]
  public ActionResult SensorEdit(long? id)
  {
    if (!MonnitSession.CurrentCustomer.IsAdmin && !MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    Sensor model = Sensor.Load(id ?? long.MinValue);
    return model != null && model.IsDeleted && !MonnitSession.CustomerCan("Support_Advanced") && !MonnitSession.IsAuthorizedForAccount(model.AccountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult SensorEdit(FormCollection collection)
  {
    Sensor sensor = Sensor.Load(collection["SensorID"].ToLong());
    if (sensor == null || !MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
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
    if (sensor != null && sensor.IsDeleted && !MonnitSession.CustomerCan("Support_Advanced"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (sensor != null)
    {
      Account account = Account.Load(sensor.AccountID);
      if (account == null)
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index",
          controller = "Overview"
        });
      if (!string.IsNullOrEmpty(collection["StartDate"]) && !MonnitSession.CustomerCan("Reset_Start_Date") && collection["StartDate"].ToDateTime() != sensor.StartDate)
        collection["StartDate"] = sensor.StartDate.ToString();
      sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited sensor settings");
      string s = collection["SensorPrintHex"];
      if (!string.IsNullOrEmpty(s))
        sensor.SensorPrint = s.FormatStringToByteArray();
      this.UpdateModel<Sensor>(sensor);
      this.UpdateModel<Sensor>(sensor);
      AccountSubscriptionType subscriptionType = account.CurrentSubscription.AccountSubscriptionType;
      double num = MonnitSession.CurrentCustomer.Account.MinHeartBeat();
      if (sensor.ReportInterval < num)
        sensor.ReportInterval = num;
      if (sensor.ActiveStateInterval < num)
        sensor.ActiveStateInterval = num;
      if (sensor.ActiveStateInterval > sensor.ReportInterval)
        sensor.ReportInterval = sensor.ActiveStateInterval;
      sensor.Save();
    }
    return (ActionResult) this.View((object) sensor);
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult SensorDetails(long id)
  {
    Sensor sensor = Sensor.Load(id);
    return !MonnitSession.CustomerCan("Sensor_View_History") || sensor == null || !MonnitSession.CurrentCustomer.CanSeeSensor(sensor) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "SensorIndex",
      controller = "Overview"
    }) : (ActionResult) this.View((object) sensor);
  }

  [Authorize]
  public ActionResult SensorDefault(long id)
  {
    try
    {
      Sensor DBObject = Sensor.Load(id);
      Account account = Account.Load(DBObject.AccountID);
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Set sensor settings to default");
      DBObject.SetDefaults(false);
      DBObject.Save();
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorEdit",
        controller = "Settings",
        id = DBObject.SensorID
      });
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [Authorize]
  public ActionResult PermanentDeleteSensor(long id)
  {
    string content = "Failed";
    try
    {
      if (MonnitSession.CustomerCan("Support_Advanced"))
      {
        Sensor DBObject = Sensor.Load(id);
        DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Permanently delete sensor");
        DBObject.IsDeleted = true;
        CSNet csNet = CSNet.Load(ConfigData.AppSettings("PermanentlyDeletedCSNetID").ToLong());
        DBObject.AccountID = csNet.AccountID;
        DBObject.CSNetID = csNet.CSNetID;
        DBObject.Save();
        content = "Success";
        Sensor.DeleteAncillaryObjects(id);
      }
    }
    catch (Exception ex)
    {
      ex.Log($"SettingsController.PermaneteDeleteSensor[ID: {id.ToString()}] ");
      content = "Failed";
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  [NoCache]
  public ActionResult GatewayEdit(long? id)
  {
    if (!MonnitSession.CurrentCustomer.IsAdmin && !MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    Gateway model = Gateway.Load(id ?? long.MinValue);
    if (model != null)
    {
      if (model.IsDeleted && !MonnitSession.CustomerCan("Support_Advanced"))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index",
          controller = "Overview"
        });
      CSNet csNet = CSNet.Load(model.CSNetID);
      if (csNet != null && !MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index",
          controller = "Overview"
        });
    }
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult GatewayEdit(FormCollection collection)
  {
    Gateway model = Gateway.Load(collection["GatewayID"].ToLong());
    if (model != null)
    {
      CSNet csNet = CSNet.Load(model.CSNetID);
      if (csNet == null && !MonnitSession.IsCurrentCustomerMonnitAdmin || !MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
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
      if (model.IsDeleted && !MonnitSession.CustomerCan("Support_Advanced"))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index",
          controller = "Overview"
        });
    }
    if (model != null)
    {
      try
      {
        string serverHostAddress = model.ServerHostAddress;
        string serverHostAddress2 = model.ServerHostAddress2;
        int port = model.Port;
        int port2 = model.Port2;
        bool isGpsUnlocked = model.IsGPSUnlocked;
        bool gpsUnlockRequest = model.SendGPSUnlockRequest;
        bool gpsPing = model.GPSPing;
        bool isUnlocked = model.IsUnlocked;
        bool sendUnlockRequest = model.SendUnlockRequest;
        this.UpdateModel<Gateway>(model);
        if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin && !model.IsUnlocked)
        {
          model.ServerHostAddress = serverHostAddress;
          model.ServerHostAddress2 = serverHostAddress2;
          model.Port = port;
          model.Port2 = port2;
        }
        if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin && !MonnitSession.CustomerCan("Support_Advanced"))
        {
          model.IsGPSUnlocked = isGpsUnlocked;
          model.SendGPSUnlockRequest = gpsUnlockRequest;
          model.GPSPing = gpsPing;
          model.IsUnlocked = isUnlocked;
          model.SendUnlockRequest = sendUnlockRequest;
        }
        if (!model.ServerInterfaceActive && !model.ModbusInterfaceActive && !model.SNMPInterface1Active && !model.SNMPInterface2Active && !model.SNMPInterface3Active && !model.SNMPInterface4Active)
          model.ServerInterfaceActive = true;
        model.IsDirty = collection["IsDirty"].ToStringSafe().ToLower().Contains("true");
        model.Save();
      }
      catch (Exception ex)
      {
      }
    }
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult PermanentDeleteGateway(long id)
  {
    string content = "Failed";
    try
    {
      if (MonnitSession.CustomerCan("Support_Advanced"))
      {
        Gateway DBObject = Gateway.Load(id);
        DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Permanently delete gateway");
        DBObject.IsDeleted = true;
        DBObject.CSNetID = ConfigData.AppSettings("PermanentlyDeletedCSNetID").ToLong();
        DBObject.Save();
        content = "Success";
        Gateway.DeleteAncillaryObjects(id);
      }
    }
    catch (Exception ex)
    {
      ex.Log($"SettingsController.PermanentDeleteGateway[ID: {id.ToString()}] ");
      content = "Failed";
    }
    return (ActionResult) this.Content(content);
  }

  [Authorize]
  public ActionResult RFCommand(long? id)
  {
    if (!MonnitSession.CustomerCan(nameof (RFCommand)))
      return (ActionResult) this.Redirect("/Overview");
    try
    {
      return (ActionResult) this.View((object) new Monnit.RFCommand()
      {
        GatewayID = (id ?? -1L),
        Frequency = 900.0,
        Power = "0000",
        Frequency24 = 2402.0,
        Power24 = "0000"
      });
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content("Failed to load custom commands.");
    }
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult RFCommand(Monnit.RFCommand command)
  {
    if (!MonnitSession.CustomerCan(nameof (RFCommand)))
      return (ActionResult) this.Redirect("/Overview");
    try
    {
      Gateway gateway = Gateway.Load(command.GatewayID);
      if (gateway == null)
        command.ErrorMsg = "Gateway is not found";
      if (command.GatewayID == command.DeviceID)
      {
        if (gateway.GenerationType != "Gen4" && new Version(gateway.APNFirmwareVersion).Minor < 24)
          command.ErrorMsg = "Gateway invalid";
      }
      else
      {
        Sensor sensor = Sensor.Load(command.DeviceID);
        if (sensor == null || new Version(sensor.FirmwareVersion).Minor < 24)
          command.ErrorMsg = "Sensor invalid";
      }
      if (command.TestMode == 0 || command.TestMode == 1)
      {
        command.Power = "0000";
      }
      else
      {
        command.Power = command.Power.ToUpper();
        if (command.Power.Length != 4)
          command.ErrorMsg = "Invalid Power";
      }
      if (gateway.GenerationType == "Gen4")
      {
        if (command.GatewayID == command.DeviceID)
        {
          if (gateway.GenerationType != "Gen4" && new Version(gateway.APNFirmwareVersion).Minor < 24)
            command.ErrorMsg = "Gateway invalid";
        }
        else
        {
          Sensor sensor = Sensor.Load(command.DeviceID);
          if (sensor == null || new Version(sensor.FirmwareVersion).Minor < 24)
            command.ErrorMsg = "Sensor invalid";
        }
        if (command.TestMode24 == 0 || command.TestMode24 == 1)
        {
          command.Power24 = "0000";
        }
        else
        {
          command.Power24 = command.Power24.ToUpper();
          if (command.Power24.Length != 4)
            command.ErrorMsg = "Invalid Power";
        }
      }
      if (string.IsNullOrEmpty(command.ErrorMsg))
      {
        new GatewayAttribute()
        {
          GatewayID = gateway.GatewayID,
          Name = nameof (RFCommand),
          Value = new JavaScriptSerializer().Serialize((object) command)
        }.Save();
        GatewayAttribute.ResetAttributeList(gateway.GatewayID);
        if (GatewayAttribute.LoadByGatewayID(gateway.GatewayID).Where<GatewayAttribute>((System.Func<GatewayAttribute, bool>) (tga => tga.Name == "RFCommands")).Count<GatewayAttribute>() == 0)
          new GatewayAttribute()
          {
            GatewayID = gateway.GatewayID,
            Name = "RFCommands",
            Value = "True"
          }.Save();
      }
      return (ActionResult) this.View((object) command);
    }
    catch (Exception ex)
    {
      command.ErrorMsg = "Failed to save custom command.";
      return (ActionResult) this.View((object) command);
    }
  }

  [AuthorizeDefault]
  public ActionResult AdminReports(long id) => (ActionResult) this.View((object) Account.Load(id));

  [AuthorizeDefault]
  public ActionResult AdminSettingsList(long id)
  {
    AccountTheme.Load(id);
    return (ActionResult) this.View((object) Account.Load(id));
  }

  [AuthorizeDefault]
  public ActionResult AdminPreferences(long id)
  {
    MonnitSession.CurrentCustomer.ClearPreferences();
    AccountTheme accountTheme = AccountTheme.Load(id);
    if (MonnitSession.CurrentCustomer.AccountID != accountTheme.AccountID && !MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    List<AccountThemePreferenceTypeLink> model = AccountThemePreferenceTypeLink.LoadByAccountThemeID(accountTheme.AccountThemeID);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__162.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__162.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "accountThemeID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SettingsController.\u003C\u003Eo__162.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__162.\u003C\u003Ep__0, this.ViewBag, id);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult AdminAutomaticEmailSettings(long id) => (ActionResult) this.View();

  [AuthorizeDefault]
  public ActionResult AdminSettings(long id) => (ActionResult) this.View();

  [AuthorizeDefault]
  public ActionResult BluetoothTesting(long? id)
  {
    return (ActionResult) this.View((object) Sensor.Load(id.ToLong()));
  }

  [HttpPost]
  [AuthorizeDefault]
  public ActionResult AdminSettings(FormCollection collection, string formName)
  {
    try
    {
      foreach (string allKey in collection.AllKeys)
      {
        if (!string.IsNullOrEmpty(allKey) && (allKey != nameof (formName) || allKey != "__RequestVerificationToken"))
          ConfigData.SetAppSettings(allKey, collection[allKey]);
      }
      return (ActionResult) this.View();
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__166.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__166.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__166.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__166.\u003C\u003Ep__0, this.ViewBag, ex.Message);
    }
    return (ActionResult) this.Content("Failed");
  }

  [AuthorizeDefault]
  public ActionResult AdminSMTPSettings(long? id) => (ActionResult) this.View();

  [HttpPost]
  [ValidateInput(false)]
  [ValidateAntiForgeryToken]
  public ActionResult EditSiteConfigs(FormCollection collection, string formName)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && ConfigData.AppSettings("AdminAccountID").ToInt() > 0)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    foreach (string allKey in collection.AllKeys)
    {
      if (!string.IsNullOrEmpty(allKey) && (allKey != nameof (formName) || allKey != "__RequestVerificationToken"))
        ConfigData.SetAppSettings(allKey, collection[allKey]);
    }
    switch (formName)
    {
      case "AutomatedEmails":
        return (ActionResult) this.View("~/Views/Settings/AdminAutomaticEmailSettings.aspx");
      case "ExceptionLogging":
        return (ActionResult) this.View("~/Views/Settings/AdminExceptionLogging.aspx");
      default:
        return (ActionResult) this.View();
    }
  }

  [HttpPost]
  [AuthorizeDefault]
  public ActionResult AdminSMTPSettings(FormCollection collection, string formName)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && ConfigData.AppSettings("AdminAccountID").ToInt() > 0)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    try
    {
      if (((Customer) null).CustomerID == MonnitSession.CurrentCustomer.CustomerID)
      {
        AccountTheme accountTheme = AccountTheme.Find(MonnitSession.CurrentCustomer.AccountID);
        accountTheme.SMTP = collection["SMTP"];
        accountTheme.SMTPPort = collection["SMTPPort"].ToInt();
        accountTheme.SMTPUser = collection["SMTPUser"];
        accountTheme.SMTPPassword = MonnitSession.UseEncryption ? collection["SMTPPassword"].Encrypt() : collection["SMTPPassword"];
        accountTheme.SMTPDefaultFrom = collection["SMTPDefaultFrom"];
        accountTheme.SMTPFriendlyName = collection["SMTPFriendlyName"];
        accountTheme.oldSMTPFriendlyName = collection["SMTPFriendlyName"];
        accountTheme.SMTPUseSSL = collection["SMTPUseSSL"].ToBool();
        accountTheme.SMTPReturnPath = collection["SMTPReturnPath"];
        accountTheme.Save();
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__169.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__169.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = SettingsController.\u003C\u003Eo__169.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__169.\u003C\u003Ep__0, this.ViewBag, "Success");
      }
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__169.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__169.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__169.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__169.\u003C\u003Ep__1, this.ViewBag, ex.Message);
    }
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public ActionResult AdminExceptionLogging(long id) => (ActionResult) this.View();

  [AuthorizeDefault]
  public ActionResult AdminReportBuilder()
  {
    return !MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) ReportQuery.LoadAll().Where<ReportQuery>((System.Func<ReportQuery, bool>) (r => !r.IsDeleted)).ToList<ReportQuery>());
  }

  [AuthorizeDefault]
  public ActionResult AdminReportBuilderCreate()
  {
    return !MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) new ReportQuery());
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateInput(false)]
  public ActionResult AdminReportBuilderCreate(FormCollection collection)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    try
    {
      ReportQuery reportQuery = new ReportQuery();
      reportQuery.Name = collection["Name"];
      reportQuery.Description = collection["Description"];
      if (collection["AccountID"] != null)
        reportQuery.AccountID = collection["AccountID"].ToLong();
      if (collection["AccountThemeID"] != null)
        reportQuery.AccountThemeID = collection["AccountThemeID"].ToLong();
      if (collection["ReportBuilder"] != null)
        reportQuery.ReportBuilder = collection["ReportBuilder"];
      if (collection["SQL"] != null)
        reportQuery.SQL = collection["SQL"];
      if (collection["Tags"] != null)
        reportQuery.Tags = collection["Tags"];
      if (collection["MaxRunTime"] != null)
        reportQuery.MaxRunTime = collection["MaxRunTime"].ToInt();
      if (collection["SensorLimit"] != null)
        reportQuery.SensorLimit = collection["SensorLimit"].ToInt();
      reportQuery.ReportTypeID = collection["ReportTypeID"].ToLong();
      reportQuery.RequiresPreAggs = !string.IsNullOrEmpty(collection["RequiresPreAggs"]);
      reportQuery.ScheduleAnnually = !string.IsNullOrEmpty(collection["ScheduleAnnually"]);
      reportQuery.ScheduleMonthly = !string.IsNullOrEmpty(collection["ScheduleMonthly"]);
      reportQuery.ScheduleWeekly = !string.IsNullOrEmpty(collection["ScheduleWeekly"]);
      reportQuery.ScheduleDaily = !string.IsNullOrEmpty(collection["ScheduleDaily"]);
      reportQuery.ScheduleImmediately = !string.IsNullOrEmpty(collection["ScheduleImmediately"]);
      eCustomerType eCustomerType = (eCustomerType) System.Enum.Parse(typeof (eCustomerType), collection["CustomerAccess"]);
      reportQuery.CustomerAccess = eCustomerType;
      reportQuery.Save();
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "AdminReportBuilderEdit",
        controller = "Settings",
        id = reportQuery.ReportQueryID
      });
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__173.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__173.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__173.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__173.\u003C\u003Ep__0, this.ViewBag, ex.Message);
    }
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "AdminReportBuilder"
    });
  }

  [AuthorizeDefault]
  public ActionResult AdminReportBuilderEdit(long id)
  {
    return !MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) ReportQuery.Load(id));
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateInput(false)]
  public ActionResult AdminReportBuilderEdit(long id, FormCollection collection)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    ReportQuery model = new ReportQuery();
    try
    {
      model = ReportQuery.Load(id);
      model.Name = collection["Name"];
      model.Description = collection["Description"];
      if (collection["AccountID"] != null)
        model.AccountID = collection["AccountID"].ToLong();
      if (collection["AccountThemeID"] != null)
        model.AccountThemeID = collection["AccountThemeID"].ToLong();
      if (collection["ReportBuilder"] != null)
        model.ReportBuilder = collection["ReportBuilder"];
      if (collection["SQL"] != null)
        model.SQL = collection["SQL"];
      if (collection["Tags"] != null)
        model.Tags = collection["Tags"];
      if (collection["MaxRunTime"] != null)
        model.MaxRunTime = collection["MaxRunTime"].ToInt();
      if (collection["SensorLimit"] != null)
        model.SensorLimit = collection["SensorLimit"].ToInt();
      model.ReportTypeID = collection["ReportTypeID"].ToLong();
      model.RequiresPreAggs = !string.IsNullOrEmpty(collection["RequiresPreAggs"]);
      model.ScheduleAnnually = !string.IsNullOrEmpty(collection["ScheduleAnnually"]);
      model.ScheduleMonthly = !string.IsNullOrEmpty(collection["ScheduleMonthly"]);
      model.ScheduleWeekly = !string.IsNullOrEmpty(collection["ScheduleWeekly"]);
      model.ScheduleDaily = !string.IsNullOrEmpty(collection["ScheduleDaily"]);
      model.ScheduleImmediately = !string.IsNullOrEmpty(collection["ScheduleImmediately"]);
      eCustomerType eCustomerType = (eCustomerType) System.Enum.Parse(typeof (eCustomerType), collection["CustomerAccess"]);
      model.CustomerAccess = eCustomerType;
      model.Save();
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__175.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__175.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__175.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__175.\u003C\u003Ep__0, this.ViewBag, ex.Message);
    }
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult AdminReportBuilderDelete(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    ReportQuery.Load(id).Delete();
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "AdminReportBuilder"
    });
  }

  [AuthorizeDefault]
  public ActionResult AdminReportParameterCreate(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__177.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__177.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ReportQueryID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SettingsController.\u003C\u003Eo__177.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__177.\u003C\u003Ep__0, this.ViewBag, id);
    return (ActionResult) this.View((object) new ReportParameter());
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateInput(false)]
  public ActionResult AdminReportParameterCreate(FormCollection collection)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    try
    {
      ReportParameter reportParameter = new ReportParameter();
      reportParameter.ReportQueryID = collection["ReportQueryID"].ToLong();
      reportParameter.ReportParameterTypeID = collection["ReportParameterTypeID"].ToLong();
      reportParameter.ParamName = collection["ParamName"];
      reportParameter.LabelText = collection["LabelText"];
      reportParameter.HelpText = collection["HelpText"];
      reportParameter.DefaultValue = collection["DefaultValue"];
      reportParameter.PredefinedValues = collection["PredefinedValues"];
      reportParameter.Save();
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "AdminReportBuilderEdit",
        controller = "Settings",
        id = reportParameter.ReportQueryID
      });
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__178.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__178.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__178.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__178.\u003C\u003Ep__0, this.ViewBag, ex.Message);
    }
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "AdminReportBuilderEdit"
    });
  }

  [AuthorizeDefault]
  public ActionResult AdminReportParameterEdit(long id)
  {
    return !MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) ReportParameter.Load(id));
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateInput(false)]
  public ActionResult AdminReportParameterEdit(long id, FormCollection collection)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    try
    {
      ReportParameter reportParameter = ReportParameter.Load(id);
      reportParameter.ReportQueryID = collection["ReportQueryID"].ToLong();
      reportParameter.ReportParameterTypeID = collection["ReportParameterTypeID"].ToLong();
      reportParameter.ParamName = collection["ParamName"];
      reportParameter.LabelText = collection["LabelText"];
      reportParameter.HelpText = collection["HelpText"];
      reportParameter.DefaultValue = collection["DefaultValue"];
      reportParameter.PredefinedValues = collection["PredefinedValues"];
      reportParameter.Save();
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "AdminReportBuilderEdit",
        controller = "Settings",
        id = reportParameter.ReportQueryID
      });
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__180.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__180.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__180.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__180.\u003C\u003Ep__0, this.ViewBag, ex.Message);
    }
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "AdminReportBuilder"
    });
  }

  [AuthorizeDefault]
  public ActionResult AdminReportParameterDelete(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    ReportParameter reportParameter = ReportParameter.Load(id);
    foreach (BaseDBObject baseDbObject in ReportParameterValue.LoadByReportParameterID(reportParameter.ReportParameterID))
      baseDbObject.Delete();
    try
    {
      reportParameter.Delete();
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "AdminReportBuilderEdit",
        controller = "Settings",
        id = reportParameter.ReportQueryID
      });
    }
    catch
    {
      return (ActionResult) this.Content("Unable to delete parameter!");
    }
  }

  [AuthorizeDefault]
  public ActionResult AdminReportParameterSortOrder(long id)
  {
    List<ReportParameter> reportParameterList = ReportParameter.LoadByReportQuery(id);
    bool flag = false;
    if (reportParameterList.Count > 1)
    {
      int num1 = 0;
      foreach (ReportParameter reportParameter in reportParameterList)
      {
        if (reportParameter.SortOrder == 0)
        {
          ++num1;
          if (num1 > 1)
          {
            flag = true;
            break;
          }
        }
      }
      if (flag)
      {
        int num2 = 0;
        foreach (ReportParameter reportParameter in reportParameterList)
        {
          reportParameter.SortOrder = num2;
          ++num2;
          reportParameter.Save();
        }
      }
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult AdminReportParameterMove(long id, long start, long end)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    foreach (ReportParameter reportParameter1 in ReportParameter.LoadByReportQuery(id))
    {
      ReportParameter reportParameter2 = ReportParameter.Load(reportParameter1.ReportParameterID);
      int num1 = start.ToInt();
      int num2 = end.ToInt();
      if (reportParameter2.SortOrder == num1)
      {
        reportParameter2.SortOrder = num2;
        reportParameter2.Save();
      }
      else if (start < end)
      {
        if (reportParameter2.SortOrder >= num1 && reportParameter2.SortOrder <= num2)
        {
          --reportParameter2.SortOrder;
          reportParameter2.Save();
        }
      }
      else if (start > end && reportParameter2.SortOrder <= num1 && reportParameter2.SortOrder >= num2)
      {
        ++reportParameter2.SortOrder;
        reportParameter2.Save();
      }
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult AutoBill(long id)
  {
    AccountIncrement model = id != long.MinValue ? AccountIncrement.LoadByAccountID(id).FirstOrDefault<AccountIncrement>() : (AccountIncrement) null;
    if (model == null || model.AccountID != id)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        controller = "Overview",
        action = "Index"
      });
    return !MonnitSession.CurrentCustomer.CanSeeAccount(id) ? this.PermissionError(ThemeController.PermissionErrorMessage.IsAuthorizedForAccount(id), methodName: nameof (AutoBill), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SettingsController.cs", sourceLineNumber: 7559) : (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [AuthorizeDefault]
  public ActionResult ToggleAutoBill(long id)
  {
    string str = "Failed";
    Account acct = Account.Load(id);
    long accountId = MonnitSession.CurrentCustomer.AccountID;
    long customerId = MonnitSession.CurrentCustomer.CustomerID;
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      str = "Failed";
    List<AccountSubscription> source = AccountSubscription.LoadByAccountID(id);
    string content;
    try
    {
      if (acct.AutoBill >= 1L)
      {
        acct.AutoBill = long.MinValue;
        acct.AutoBillCancelDate = DateTime.UtcNow;
        acct.Save();
        content = "Disabled";
        AuditLog.LogAuditData(customerId, id, eAuditAction.Update, eAuditObject.Account, $"{{\"SubscriptionUpdate\": \"{"AutoBill"}\", \"Date\": \"{DateTime.UtcNow}\" }} ", id, "Disabled Account For AutoBill");
        foreach (AccountSubscription accountSubscription in source.Where<AccountSubscription>((System.Func<AccountSubscription, bool>) (sub => sub.AccountID == id)))
        {
          if (accountSubscription.ExpirationDate > DateTime.UtcNow)
          {
            AccountSubscriptionChangeLog subscriptionChangeLog = AccountSubscriptionChangeLog.LoadBySubscriptionID(accountSubscription.AccountSubscriptionID).OrderByDescending<AccountSubscriptionChangeLog, DateTime>((System.Func<AccountSubscriptionChangeLog, DateTime>) (log => log.ChangeDate)).FirstOrDefault<AccountSubscriptionChangeLog>();
            DateTime dateTime1 = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            DateTime dateTime2 = dateTime1.AddMonths(1);
            if (subscriptionChangeLog != null && subscriptionChangeLog.ChangeNote == "Database Query Update - Auto Increment 1 Month" && subscriptionChangeLog.ChangeDate > dateTime1 && subscriptionChangeLog.ChangeDate < dateTime2)
            {
              accountSubscription.ExpirationDate = dateTime2;
              subscriptionChangeLog.ChangeNote += " Roll Back";
              subscriptionChangeLog.ChangeDate = DateTime.UtcNow;
              subscriptionChangeLog.NewExpirationDate = dateTime2;
              accountSubscription.Save();
              subscriptionChangeLog.Save();
            }
          }
        }
      }
      else
      {
        acct.AutoBill = accountId;
        acct.AutoBillStartDate = DateTime.UtcNow;
        acct.Save();
        content = "Success";
        AuditLog.LogAuditData(accountId, customerId, eAuditAction.Update, eAuditObject.Account, $"{{\"SubscriptionUpdate\": \"{"AutoBill"}\", \"Date\": \"{DateTime.UtcNow}\" }} ", accountId, "Enabled Account For AutoBill");
      }
    }
    catch (Exception ex)
    {
      ex.Log($"SettingsController.ToggleAutoBill[AccountID: {id.ToString()}]");
      content = "Failed";
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public ActionResult AdminAccountTheme()
  {
    if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
      return (ActionResult) this.View((object) AccountTheme.LoadAll());
    if (MonnitSession.IsCurrentCustomerAccountThemeAdmin)
      return (ActionResult) this.View((object) new List<AccountTheme>()
      {
        MonnitSession.CurrentTheme
      });
    return ConfigData.AppSettings("IsEnterprise").ToBool() ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "AdminSearch",
      controller = "Settings",
      id = MonnitSession.CurrentTheme.AccountThemeID
    }) : (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      controller = "Overview",
      action = "Index"
    });
  }

  [AuthorizeDefault]
  public ActionResult AdminAccountThemeCreate()
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin && !MonnitSession.IsCurrentCustomerAccountThemeAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Settings"
      });
    AccountTheme model = new AccountTheme();
    model.SMTP = ConfigData.AppSettings("SMTP");
    model.SMTPPort = ConfigData.AppSettings("SMTPPort").ToInt();
    model.SMTPUser = ConfigData.AppSettings("SMTPUser");
    model.SMTPPasswordPlainText = ConfigData.AppSettings("SMTPPasswordPlainText");
    model.SMTPUseSSL = ConfigData.AppSettings("UseSSL").ToBool();
    model.SMTPFriendlyName = ConfigData.AppSettings("SMTPFreindlyName");
    model.MinPasswordLength = ConfigData.AppSettings("MinPasswordLength").ToInt();
    model.CurrentEULA = ConfigData.AppSettings("CurrentEULA");
    model.DefaultAccountSubscriptionTypeID = (long) ConfigData.AppSettings("DefaultAccountSubscriptionTypeID").ToInt();
    model.AllowAccountCreation = true;
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__187.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__187.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SettingsController.\u003C\u003Eo__187.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__187.\u003C\u003Ep__0, this.ViewBag, "");
    return (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [ValidateInput(false)]
  [AuthorizeDefault]
  public ActionResult AdminAccountThemeCreate(FormCollection collection)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin && !MonnitSession.IsCurrentCustomerAccountThemeAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Settings"
      });
    AccountTheme model = new AccountTheme();
    try
    {
      if (string.IsNullOrEmpty(collection["SMTPFriendlyName"].ToString()))
        this.ModelState.AddModelError("SMTPFriendlyName", "Required");
      if (string.IsNullOrEmpty(collection["SMTPDefaultFrom"].ToString()))
        this.ModelState.AddModelError("SMTPDefaultFrom", "Required");
      if (string.IsNullOrWhiteSpace(collection["WhiteLabelReseller"]))
      {
        if (string.IsNullOrEmpty(collection["Domain"].ToString()))
          this.ModelState.AddModelError("Domain", "Required");
        if (!string.IsNullOrEmpty(collection["SMTP"].ToString()))
        {
          if (string.IsNullOrEmpty(collection["SMTPPasswordPlainText"].ToString()))
            this.ModelState.AddModelError("SMTPPasswordPlainText", "Required");
          if (collection["SMTPPort"].ToInt() < 1 || collection["SMTPPort"].ToInt() > 65000 || string.IsNullOrEmpty(collection["SMTPPort"].ToString()))
            this.ModelState.AddModelError("SMTPPort", "Required");
          if (string.IsNullOrEmpty(collection["SMTPUser"].ToString()))
            this.ModelState.AddModelError("SMTPUser", "Required");
        }
        if (string.IsNullOrEmpty(collection["Theme"].ToString()))
          this.ModelState.AddModelError("Theme", "Required");
      }
      if (this.ModelState.IsValid)
      {
        this.UpdateModel<AccountTheme>(model);
        model.Save();
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__188.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__188.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = SettingsController.\u003C\u003Eo__188.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__188.\u003C\u003Ep__0, this.ViewBag, "Success");
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "AdminAccountThemeEdit",
          controller = "Settings",
          id = model.AccountThemeID
        });
      }
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__188.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__188.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SettingsController.\u003C\u003Eo__188.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__188.\u003C\u003Ep__1, this.ViewBag, "Failed");
      return (ActionResult) this.View((object) model);
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__188.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__188.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__188.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__188.\u003C\u003Ep__2, this.ViewBag, ex.Message);
      return (ActionResult) this.View((object) model);
    }
  }

  [AuthorizeDefault]
  public ActionResult AdminAccountThemeEdit(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin && !MonnitSession.IsCurrentCustomerAccountThemeAdmin && !ConfigData.AppSettings("IsEnterprise").ToBool())
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Settings"
      });
    AccountTheme model = new AccountTheme();
    try
    {
      model = AccountTheme.Load(id);
      if (!MonnitSession.CurrentCustomer.CanSeeAccount(model.AccountID))
        return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
        {
          ErrorLocation = "Settings",
          ErrorTranslateTag = "Settings/AdminAccountThemeEdit|",
          ErrorTitle = "Unauthorized access to edit account theme",
          ErrorMessage = "You do not have permission to access this page."
        });
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__189.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__189.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__189.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__189.\u003C\u003Ep__0, this.ViewBag, "");
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__189.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__189.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__189.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__189.\u003C\u003Ep__1, this.ViewBag, ex.Message);
    }
    return (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [ValidateInput(false)]
  [AuthorizeDefault]
  public ActionResult AdminAccountThemeEdit(long? id, FormCollection collection)
  {
    bool isEnterprise = MonnitSession.IsEnterprise;
    if (((MonnitSession.IsCurrentCustomerMonnitSuperAdmin ? 1 : (MonnitSession.IsCurrentCustomerAccountThemeAdmin ? 1 : 0)) | (isEnterprise ? 1 : 0)) == 0)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Settings"
      });
    AccountTheme model = AccountTheme.Load(id ?? long.MinValue);
    if (model == null)
    {
      model = new AccountTheme();
      model.AccountID = MonnitSession.CurrentCustomer.AccountID;
    }
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(model.AccountID))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Settings",
        ErrorTranslateTag = "Settings/AdminAccountThemeEdit|",
        ErrorTitle = "Unauthorized access to edit account theme",
        ErrorMessage = "You do not have permission to access this page."
      });
    try
    {
      if (string.IsNullOrEmpty(collection["SMTPFriendlyName"].ToString()))
        this.ModelState.AddModelError("SMTPFriendlyName", "Required");
      if (string.IsNullOrEmpty(collection["SMTPDefaultFrom"].ToString()))
        this.ModelState.AddModelError("SMTPDefaultFrom", "Required");
      if (!isEnterprise)
      {
        string input = collection["AlphanumericSenderID"].ToString();
        if (string.IsNullOrWhiteSpace(input) || Regex.IsMatch(input, "[^a-zA-Z0-9 +\\-_&]+") || !Regex.IsMatch(input, "[a-zA-Z]"))
          this.ModelState.AddModelError("AlphanumericSenderID", "Must contain at least 1 letter. Valid characters [a-zA-Z0-9 +-_&]");
      }
      if (string.IsNullOrWhiteSpace(collection["WhiteLabelReseller"]))
      {
        if (string.IsNullOrEmpty(collection["Domain"].ToString()))
          this.ModelState.AddModelError("Domain", "Required");
        if (!string.IsNullOrEmpty(collection["SMTP"].ToString()))
        {
          if (string.IsNullOrEmpty(collection["SMTPPasswordPlainText"].ToString()))
            this.ModelState.AddModelError("SMTPPasswordPlainText", "Required");
          if (collection["SMTPPort"].ToInt() < 1 || collection["SMTPPort"].ToInt() > 65000 || string.IsNullOrEmpty(collection["SMTPPort"].ToString()))
            this.ModelState.AddModelError("SMTPPort", "Required");
          if (string.IsNullOrEmpty(collection["SMTPUser"].ToString()))
            this.ModelState.AddModelError("SMTPUser", "Required");
        }
        if (string.IsNullOrEmpty(collection["Theme"].ToString()))
          this.ModelState.AddModelError("Theme", "Required");
      }
      if (this.ModelState.IsValid | isEnterprise)
      {
        this.UpdateModel<AccountTheme>(model);
        model.Save();
        // ISSUE: reference to a compiler-generated field
        if (SettingsController.\u003C\u003Eo__190.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SettingsController.\u003C\u003Eo__190.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = SettingsController.\u003C\u003Eo__190.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__190.\u003C\u003Ep__0, this.ViewBag, "Success");
        return (ActionResult) this.View((object) model);
      }
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__190.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__190.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = SettingsController.\u003C\u003Eo__190.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__190.\u003C\u003Ep__1, this.ViewBag, "Failed");
      return (ActionResult) this.View((object) model);
    }
    catch (Exception ex)
    {
      // ISSUE: reference to a compiler-generated field
      if (SettingsController.\u003C\u003Eo__190.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        SettingsController.\u003C\u003Eo__190.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Result", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = SettingsController.\u003C\u003Eo__190.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__190.\u003C\u003Ep__2, this.ViewBag, ex.Message);
      return (ActionResult) this.View((object) model);
    }
  }

  [AuthorizeDefault]
  public ActionResult DeleteAccountThemeContact(long id)
  {
    AccountThemeContact accountThemeContact = AccountThemeContact.Load(id);
    if (accountThemeContact == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    AccountTheme model = AccountTheme.Load(accountThemeContact.AccountThemeID);
    if (MonnitSession.CurrentCustomer.AccountID != model.AccountID && !MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.IsCurrentCustomerAccountThemeAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    accountThemeContact.Delete();
    return (ActionResult) this.View("AdminContacts", (object) model);
  }

  [AuthorizeDefault]
  public ActionResult AdminPortalWhiteLabeling(long id) => (ActionResult) this.View((object) id);

  [AuthorizeDefault]
  public ActionResult TranslateHome()
  {
    return MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Can_See_TranslateUI")) ? (ActionResult) this.View() : (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    });
  }

  [AuthorizeDefault]
  public ActionResult TranslateSearch(long id, string textToFind)
  {
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Can_See_TranslateUI")))
      return (ActionResult) this.Content("Not Authorized");
    if (string.IsNullOrEmpty(textToFind))
      return (ActionResult) this.Content("Search Text Required");
    if (id == -1L)
      return (ActionResult) this.Content("Language Required");
    List<TranslationSearchModel> model = TranslationSearchModel.Search(new long?(id), textToFind);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__194.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__194.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "LanguageID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SettingsController.\u003C\u003Eo__194.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__194.\u003C\u003Ep__0, this.ViewBag, id);
    return (ActionResult) this.PartialView("TranslateSearchResult", (object) model);
  }

  [AuthorizeDefault]
  public ActionResult TranslatePick()
  {
    return MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Can_See_TranslateUI")) ? (ActionResult) this.View() : (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    });
  }

  [AuthorizeDefault]
  public ActionResult AdminTranslate(long id, int? page, int? resultsPerPage, string tagFilter)
  {
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Can_See_TranslateUI")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    int valueOrDefault = resultsPerPage.GetValueOrDefault();
    if (!resultsPerPage.HasValue)
      resultsPerPage = new int?(10);
    int? nullable = resultsPerPage;
    int num1 = 10;
    resultsPerPage = nullable.GetValueOrDefault() < num1 & nullable.HasValue ? new int?(10) : resultsPerPage;
    nullable = resultsPerPage;
    int num2 = 30;
    resultsPerPage = nullable.GetValueOrDefault() > num2 & nullable.HasValue ? new int?(30) : resultsPerPage;
    valueOrDefault = page.GetValueOrDefault();
    if (!page.HasValue)
      page = new int?(0);
    nullable = page;
    int num3 = 0;
    page = nullable.GetValueOrDefault() < num3 & nullable.HasValue ? new int?(0) : page;
    tagFilter = tagFilter ?? string.Empty;
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "LanguageID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__0, this.ViewBag, id);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ResultsPerPage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__1, this.ViewBag, resultsPerPage);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Page", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__2, this.ViewBag, page);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TagFilter", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj4 = SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__3.Target((CallSite) SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__3, this.ViewBag, tagFilter);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__9 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, List<UITranslateModel>>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (List<UITranslateModel>), typeof (SettingsController)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, List<UITranslateModel>> target1 = SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__9.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, List<UITranslateModel>>> p9 = SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__9;
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__8 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__8 = CallSite<Func<CallSite, Type, object, object, object, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "LoadToBeTranslatedByLanguage_Page", (IEnumerable<Type>) null, typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[5]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, Type, object, object, object, object, object> target2 = SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__8.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, Type, object, object, object, object, object>> p8 = SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__8;
    Type type = typeof (UITranslateModel);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "LanguageID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj5 = SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__4.Target((CallSite) SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__4, this.ViewBag);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ResultsPerPage", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj6 = SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__5.Target((CallSite) SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__5, this.ViewBag);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__6 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Page", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj7 = SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__6.Target((CallSite) SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__6, this.ViewBag);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__7 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "TagFilter", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj8 = SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__7.Target((CallSite) SettingsController.\u003C\u003Eo__196.\u003C\u003Ep__7, this.ViewBag);
    object obj9 = target2((CallSite) p8, type, obj5, obj6, obj7, obj8);
    return (ActionResult) this.View((object) target1((CallSite) p9, obj9));
  }

  [AuthorizeDefault]
  public ActionResult UpdateTranslationString(long id, string text)
  {
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Can_See_TranslateUI")))
      return (ActionResult) this.Content("Unauthorized");
    if (string.IsNullOrEmpty(text))
      return (ActionResult) this.Content("Text Required");
    TranslationLanguageLink translationLanguageLink = TranslationLanguageLink.Load(id);
    if (translationLanguageLink == null)
      return (ActionResult) this.Content("Failed");
    translationLanguageLink.Text = text;
    translationLanguageLink.Save();
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult CreateTTLEntry(FormCollection form)
  {
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Can_See_TranslateUI")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    int minValue = int.MinValue;
    foreach (string allKey in form.AllKeys)
    {
      if (allKey == "LanguageID")
      {
        minValue = form[allKey].ToInt();
      }
      else
      {
        string str = form[allKey];
        int num = allKey.Split('_')[1].ToInt();
        if (!string.IsNullOrEmpty(str))
          new TranslationLanguageLink()
          {
            LanguageID = ((long) minValue),
            TranslationTagID = ((long) num),
            Text = str
          }.Save();
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__198.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__198.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "LanguageID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = SettingsController.\u003C\u003Eo__198.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__198.\u003C\u003Ep__0, this.ViewBag, minValue);
    UITranslateModel.LoadToBeTranslatedByLanguage((long) minValue, 10);
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "AdminTranslate",
      controller = "Settings",
      id = minValue
    });
  }

  [AuthorizeDefault]
  public ActionResult AutoTranslation()
  {
    return MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Can_See_TranslateUI")) ? (ActionResult) this.View() : (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    });
  }

  [HttpPost]
  [AuthorizeDefault]
  public ActionResult AutoTranslation(long id)
  {
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__200.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__200.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "LanguageID", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = SettingsController.\u003C\u003Eo__200.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__200.\u003C\u003Ep__0, this.ViewBag, id);
    List<TranslationTag> translationTagList1 = TranslationTag.LoadIncompleteByLanguageID(id);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__200.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__200.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "IncompleteNum", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = SettingsController.\u003C\u003Eo__200.\u003C\u003Ep__1.Target((CallSite) SettingsController.\u003C\u003Eo__200.\u003C\u003Ep__1, this.ViewBag, translationTagList1.Count);
    List<TranslationLanguageLink> translationLanguageLinkList = TranslationLanguageLink.LoadByLanguageID(id);
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__200.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__200.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TranslatedNum", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = SettingsController.\u003C\u003Eo__200.\u003C\u003Ep__2.Target((CallSite) SettingsController.\u003C\u003Eo__200.\u003C\u003Ep__2, this.ViewBag, translationLanguageLinkList.Count);
    List<TranslationTag> translationTagList2 = TranslationTag.LoadAll();
    // ISSUE: reference to a compiler-generated field
    if (SettingsController.\u003C\u003Eo__200.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      SettingsController.\u003C\u003Eo__200.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalNum", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj4 = SettingsController.\u003C\u003Eo__200.\u003C\u003Ep__3.Target((CallSite) SettingsController.\u003C\u003Eo__200.\u003C\u003Ep__3, this.ViewBag, translationTagList2.Count);
    return (ActionResult) this.PartialView("_TranslateLanguage");
  }

  public ActionResult TranslateAll(long id)
  {
    ActionResult actionResult1 = (ActionResult) this.Content("");
    ActionResult actionResult2;
    try
    {
      Language language = Language.Load(id);
      foreach (TranslationTag translationTag1 in TranslationTag.LoadIncompleteByLanguageID(id))
      {
        try
        {
          string tagIdName = translationTag1.TagIDName;
          string languageAttribute = language.LanguageAttribute;
          string str1 = "en";
          string str2 = "AIzaSyAsyDfJAKwr5_ES4l7FKtiwfUcLFQoewOU";
          if (tagIdName.Contains("|"))
            tagIdName = tagIdName.Split('|')[1];
          string address = $"https://www.googleapis.com/language/translate/v2?q={tagIdName}&target={languageAttribute}&source={str1}&key={str2}";
          string str3;
          using (WebClient webClient = new WebClient()
          {
            Encoding = Encoding.UTF8
          })
          {
            object obj1 = (object) JObject.Parse(webClient.DownloadString(address));
            // ISSUE: reference to a compiler-generated field
            if (SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__5 == null)
            {
              // ISSUE: reference to a compiler-generated field
              SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (SettingsController)));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, string> target1 = SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__5.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, string>> p5 = SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__5;
            // ISSUE: reference to a compiler-generated field
            if (SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__4 == null)
            {
              // ISSUE: reference to a compiler-generated field
              SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Value", typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, object> target2 = SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__4.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, object>> p4 = SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__4;
            // ISSUE: reference to a compiler-generated field
            if (SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__3 == null)
            {
              // ISSUE: reference to a compiler-generated field
              SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, string, object> target3 = SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__3.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, string, object>> p3 = SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__3;
            // ISSUE: reference to a compiler-generated field
            if (SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__2 == null)
            {
              // ISSUE: reference to a compiler-generated field
              SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, int, object> target4 = SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__2.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, int, object>> p2 = SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__2;
            // ISSUE: reference to a compiler-generated field
            if (SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, string, object> target5 = SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__1.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, string, object>> p1 = SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__1;
            // ISSUE: reference to a compiler-generated field
            if (SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__0 == null)
            {
              // ISSUE: reference to a compiler-generated field
              SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (SettingsController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj2 = SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__0.Target((CallSite) SettingsController.\u003C\u003Eo__201.\u003C\u003Ep__0, obj1, "data");
            object obj3 = target5((CallSite) p1, obj2, "translations");
            object obj4 = target4((CallSite) p2, obj3, 0);
            object obj5 = target3((CallSite) p3, obj4, "translatedText");
            object obj6 = target2((CallSite) p4, obj5);
            str3 = target1((CallSite) p5, obj6);
            if (str3.Contains("&#39;"))
              str3 = str3.Replace("&#39;", "'");
          }
          TranslationTag translationTag2 = TranslationTag.Load(translationTag1.TranslationTagID);
          new TranslationLanguageLink()
          {
            TranslationTagID = translationTag2.TranslationTagID,
            LanguageID = id,
            Text = str3
          }.Save();
        }
        catch (Exception ex)
        {
          ex.Log("SettingsController.TranslateAll " + ex.Message);
        }
      }
      actionResult2 = (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log("SettingsController.TranslateAll " + ex.Message);
      actionResult2 = (ActionResult) this.Content("Failed");
    }
    return actionResult2;
  }

  public ActionResult ExportIncompleteText(long id)
  {
    StringBuilder stringBuilder = new StringBuilder();
    Language language = Language.Load(id);
    ActionResult actionResult;
    try
    {
      stringBuilder.AppendFormat("{0}, {1}, {2} \r\n", (object) "Language", (object) "Text", (object) "Location");
      foreach (TranslationTag translationTag in TranslationTag.LoadIncompleteByLanguageID(id))
      {
        string str1 = "";
        string str2 = translationTag.TagIDName;
        if (str2.Contains("|"))
        {
          string[] strArray = str2.Split('|');
          str1 = strArray[0];
          str2 = strArray[1];
          if (str2.Contains(","))
            str2 = str2.Replace(",", "");
        }
        stringBuilder.AppendFormat("{0}, {1}, {2} \r\n", (object) language.Name, (object) str2, (object) str1);
      }
      actionResult = (ActionResult) this.File(Encoding.Default.GetBytes(stringBuilder.ToString()), "text/csv", $"Incomplete Translation{DateTime.Now.ToString()}.csv");
    }
    catch (Exception ex)
    {
      ex.Log("SettingsController.ExportIncompleteText " + ex.Message);
      stringBuilder.AppendFormat("{0}, {1}, {2} \r\n", (object) "Failed", (object) "", (object) ex.Message);
      actionResult = (ActionResult) this.File(Encoding.Default.GetBytes(stringBuilder.ToString()), "text/csv", $"Incomplete Translation{DateTime.Now.ToString()}.csv");
    }
    return actionResult;
  }

  public ActionResult ExportTranslatedText(long id)
  {
    StringBuilder stringBuilder = new StringBuilder();
    Language language = Language.Load(id);
    ActionResult actionResult;
    try
    {
      stringBuilder.AppendFormat("{0}, {1}, {2}, {3} \r\n", (object) "Language", (object) "Text", (object) "Location", (object) "Translated Text");
      foreach (TranslationLanguageLink translationLanguageLink in TranslationLanguageLink.LoadByLanguageID(id))
      {
        string str1 = TranslationTag.Load(translationLanguageLink.TranslationTagID).TagIDName;
        string str2 = "";
        string text = translationLanguageLink.Text;
        if (str1.Contains("|"))
        {
          string[] strArray = str1.Split('|');
          str2 = strArray[0];
          str1 = strArray[1];
          if (str1.Contains(","))
            str1 = str1.Replace(",", "");
        }
        stringBuilder.AppendFormat("{0}, {1}, {2}, {3} \r\n", (object) language.Name, (object) str1, (object) str2, (object) text);
      }
      actionResult = (ActionResult) this.File(Encoding.Default.GetBytes(stringBuilder.ToString()), "text/csv", $"Translated Text{DateTime.Now.ToString()}.csv");
    }
    catch (Exception ex)
    {
      ex.Log("SettingsController.ExportTranslatedText " + ex.Message);
      stringBuilder.AppendFormat("{0}, {1}, {2} \r\n", (object) "Failed", (object) "", (object) ex.Message);
      actionResult = (ActionResult) this.File(Encoding.Default.GetBytes(stringBuilder.ToString()), "text/csv", $"Translated Text{DateTime.Now.ToString()}.csv");
    }
    return actionResult;
  }

  [AuthorizeDefault]
  public ActionResult ImportSVG()
  {
    return !MonnitSession.IsCurrentCustomerMonnitSuperAdmin ? (ActionResult) this.Redirect("/Overview/Index/") : (ActionResult) this.View((object) new SVGIcon());
  }

  [HttpPost]
  [AuthorizeDefault]
  [ValidateInput(false)]
  public ActionResult ImportSVG(FormCollection collection)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
      return this.PermissionError(methodName: nameof (ImportSVG), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\SettingsController.cs", sourceLineNumber: 8258);
    if (string.IsNullOrEmpty(collection["Name"]))
      this.ModelState.AddModelError("Name", "* required.");
    if (string.IsNullOrEmpty(collection["ImageKey"]))
      this.ModelState.AddModelError("ImageKey", "* required.");
    if (string.IsNullOrEmpty(collection["Category"]))
      this.ModelState.AddModelError("Category", "* required.");
    if (string.IsNullOrEmpty(collection["HTMLcode"]))
      this.ModelState.AddModelError("HTMLcode", "* required.");
    try
    {
      AccountTheme accountTheme = AccountTheme.Find(MonnitSession.CurrentCustomer.AccountID);
      SVGIcon model = new SVGIcon();
      if (this.ModelState.IsValid)
      {
        model.Name = collection["Name"];
        model.ImageKey = collection["ImageKey"];
        model.Category = collection["Category"];
        model.HTMLCode = collection["HTMLcode"];
        model.IsDefault = collection["IsDefault"].Contains("true");
        model.ApplyTheme = collection["applyTheme"].Contains("true");
        if (model.ApplyTheme)
          model.AccountThemeID = accountTheme.AccountThemeID;
        model.Save();
        this.ViewData["Result"] = (object) "Success!";
      }
      else
        this.ViewData["Result"] = (object) "Failed! Are you missing required fields?";
      return (ActionResult) this.View((object) model);
    }
    catch (Exception ex)
    {
      this.ViewData["Result"] = (object) ("Error: " + ex.Message);
    }
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public static Dictionary<int, string> GetSystemIconEnumDict()
  {
    return System.Enum.GetValues(typeof (eSystemIconType)).Cast<eSystemIconType>().ToDictionary<eSystemIconType, int, string>((System.Func<eSystemIconType, int>) (x => x.ToInt()), (System.Func<eSystemIconType, string>) (x => Regex.Replace(x.ToString(), "([A-Z])", " $1", RegexOptions.Compiled).Trim()));
  }

  public class Base32Encoding
  {
    public static byte[] ToBytes(string input)
    {
      input = !string.IsNullOrEmpty(input) ? input.TrimEnd('=') : throw new ArgumentNullException(nameof (input));
      int length = input.Length * 5 / 8;
      byte[] bytes = new byte[length];
      byte num1 = 0;
      byte num2 = 8;
      int index = 0;
      foreach (char c in input)
      {
        int num3 = SettingsController.Base32Encoding.CharToValue(c);
        if (num2 > (byte) 5)
        {
          int num4 = num3 << (int) num2 - 5;
          num1 |= (byte) num4;
          num2 -= (byte) 5;
        }
        else
        {
          int num5 = num3 >> 5 - (int) num2;
          byte num6 = (byte) ((uint) num1 | (uint) num5);
          bytes[index++] = num6;
          num1 = (byte) (num3 << 3 + (int) num2);
          num2 += (byte) 3;
        }
      }
      if (index != length)
        bytes[index] = num1;
      return bytes;
    }

    public static string ToString(byte[] input)
    {
      if (input == null || input.Length == 0)
        throw new ArgumentNullException(nameof (input));
      int length = (int) Math.Ceiling((double) input.Length / 5.0) * 8;
      char[] chArray1 = new char[length];
      byte b1 = 0;
      byte num1 = 5;
      int num2 = 0;
      foreach (byte num3 in input)
      {
        byte b2 = (byte) ((uint) b1 | (uint) num3 >> 8 - (int) num1);
        chArray1[num2++] = SettingsController.Base32Encoding.ValueToChar(b2);
        if (num1 < (byte) 4)
        {
          byte b3 = (byte) ((int) num3 >> 3 - (int) num1 & 31 /*0x1F*/);
          chArray1[num2++] = SettingsController.Base32Encoding.ValueToChar(b3);
          num1 += (byte) 5;
        }
        num1 -= (byte) 3;
        b1 = (byte) ((int) num3 << (int) num1 & 31 /*0x1F*/);
      }
      if (num2 != length)
      {
        char[] chArray2 = chArray1;
        int index = num2;
        int num4 = index + 1;
        int num5 = (int) SettingsController.Base32Encoding.ValueToChar(b1);
        chArray2[index] = (char) num5;
        while (num4 != length)
          chArray1[num4++] = '=';
      }
      return new string(chArray1);
    }

    private static int CharToValue(char c)
    {
      int num = (int) c;
      if (num < 91 && num > 64 /*0x40*/)
        return num - 65;
      if (num < 58 && num > 47)
        return num - 24;
      if (num < 123 && num > 96 /*0x60*/)
        return num - 97;
      throw new ArgumentException("Character is not a Base32 character.", nameof (c));
    }

    private static char ValueToChar(byte b)
    {
      if (b < (byte) 26)
        return (char) ((uint) b + 65U);
      if (b < (byte) 32 /*0x20*/)
        return (char) ((uint) b + 24U);
      throw new ArgumentException("Byte is not a value Base32 value.", nameof (b));
    }
  }

  public class GroupItem
  {
    public long Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Text { get; set; }

    public string Voice { get; set; }

    public string Push { get; set; }
  }

  public class MassEmailContact
  {
    private List<AccountThemeContact> _Contacts;

    public MassEmailContact()
    {
    }

    public MassEmailContact(AccountTheme at)
    {
      AccountThemeContact accountThemeContact = AccountThemeContact.LoadByAccountThemeID(at.AccountThemeID).FirstOrDefault<AccountThemeContact>();
      this.AccountID = at.AccountID;
      this.AccountThemeID = at.AccountThemeID;
      this.AccountName = at.Theme;
      if (accountThemeContact == null)
        return;
      this.ContactName = $"{accountThemeContact.FirstName} {accountThemeContact.LastName}";
      this.ContactEmail = accountThemeContact.Email;
    }

    [DBProp("AccountID")]
    public long AccountID { get; set; }

    [DBProp("AccountThemeID")]
    public long AccountThemeID { get; set; }

    [DBProp("AccountName")]
    public string AccountName { get; set; }

    [DBProp("ContactName")]
    public string ContactName { get; set; }

    [DBProp("ContactEmail")]
    public string ContactEmail { get; set; }

    public List<AccountThemeContact> Contacts
    {
      get
      {
        if (this._Contacts == null)
          this._Contacts = AccountThemeContact.LoadByAccountThemeID(this.AccountThemeID);
        return this._Contacts;
      }
    }
  }

  public class MassEmailContentModel
  {
    public MassEmailContentModel()
    {
    }

    public MassEmailContentModel(string subject, string body)
    {
      this.Subject = subject;
      this.Body = body;
    }

    [DBProp("Subject")]
    public string Subject { get; set; }

    [DBProp("Body")]
    public string Body { get; set; }
  }
}
