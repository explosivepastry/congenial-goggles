// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.CustomerController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.ControllerBase;
using iMonnit.Models;
using Microsoft.CSharp.RuntimeBinder;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

[NoCache]
public class CustomerController : ThemeController
{
  [Authorize]
  public ActionResult Index()
  {
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Overview",
      controller = "Account"
    });
  }

  [Authorize]
  public ActionResult AddExistingUser(long id)
  {
    return (ActionResult) this.View((object) new AddExistingUserAccountModel()
    {
      AccountID = id
    });
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult AddExistingUser(AddExistingUserAccountModel model)
  {
    if (!model.email.IsValidEmail())
      this.ModelState.AddModelError("UserName", "Invalid Email");
    Customer customer = Customer.LoadAllByEmail(model.email).FirstOrDefault<Customer>();
    if (customer == null)
      this.ModelState.AddModelError("customer", "Invalid Email");
    if (model.AccountID < 1L)
      this.ModelState.AddModelError("AccountID", "Invalid Email");
    if (!this.ModelState.IsValid)
      return (ActionResult) this.Content("Invalid Email");
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

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult SendEmailInvitation(long accountID, long customerID, long fromID)
  {
    try
    {
      CustomerAccountLink cal = CustomerAccountLink.Load(customerID, accountID);
      if (cal == null)
        return (ActionResult) this.Content("Failed");
      this.SendAddUserToExistingAccountMail(cal, fromID);
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ReAddExistingUser(long accountID, long customerID)
  {
    try
    {
      CustomerAccountLink DBObject = CustomerAccountLink.Load(customerID, accountID);
      Account account = Account.Load(accountID);
      DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Added existing user to linked account");
      if (DBObject == null)
      {
        new CustomerAccountLink()
        {
          AccountDeleted = false,
          AccountID = accountID,
          CustomerDeleted = false,
          CustomerID = customerID,
          DateAccountDeleted = DateTime.MinValue,
          DateUserAdded = DateTime.UtcNow,
          DateUserDeleted = DateTime.MinValue,
          RequestConfirmed = false,
          GUID = Guid.NewGuid().ToString()
        }.Save();
        return (ActionResult) this.Content("Success");
      }
      if (!DBObject.AccountDeleted && !DBObject.CustomerDeleted)
        return (ActionResult) this.Content("User has already been added to account.");
      DBObject.CustomerDeleted = false;
      DBObject.AccountDeleted = false;
      DBObject.RequestConfirmed = false;
      DBObject.Save();
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("failed");
    }
  }

  [Authorize]
  public ActionResult Data(long id, long accountID)
  {
    // ISSUE: reference to a compiler-generated field
    if (CustomerController.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CustomerController.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ActAcctID", typeof (CustomerController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = CustomerController.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) CustomerController.\u003C\u003Eo__5.\u003C\u003Ep__0, this.ViewBag, accountID);
    Customer model = Customer.Load(id);
    return !MonnitSession.IsAuthorizedForAccount(model.AccountID) && !MonnitSession.IsLinkedToThisAccount(model.CustomerID, accountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Overview",
      controller = "Account"
    }) : (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult Details(long id, long accountID)
  {
    // ISSUE: reference to a compiler-generated field
    if (CustomerController.\u003C\u003Eo__6.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CustomerController.\u003C\u003Eo__6.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ActAcctID", typeof (CustomerController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = CustomerController.\u003C\u003Eo__6.\u003C\u003Ep__0.Target((CallSite) CustomerController.\u003C\u003Eo__6.\u003C\u003Ep__0, this.ViewBag, accountID);
    Customer model = Customer.Load(id);
    return !MonnitSession.IsAuthorizedForAccount(model.AccountID) && !MonnitSession.IsLinkedToThisAccount(model.CustomerID, accountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Overview",
      controller = "Account"
    }) : (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult Create(long id)
  {
    Account account = Account.Load(id);
    if (!MonnitSession.CustomerCan("Support_Advanced"))
    {
      if (!account.CurrentSubscription.Can(Feature.Find("muliple_users")) || !MonnitSession.CustomerCan("Customer_Create"))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "AccountUserList",
          controller = "Account",
          id = id
        });
      if (!MonnitSession.CurrentCustomer.CanSeeAccount(id) || !MonnitSession.CurrentCustomer.IsAdmin || !MonnitSession.AccountCan("muliple_users"))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = ("Create/" + MonnitSession.CurrentCustomer.AccountID.ToString())
        });
    }
    this.ViewData["Carriers"] = (object) new SelectList((IEnumerable) SMSAccount.SMSList(MonnitSession.CurrentTheme.AccountThemeID), "SMSCarrierID", "SMSCarrierName");
    this.ViewData["AccountID"] = (object) id;
    return (ActionResult) this.View();
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Create(Customer customer, string returnUrl)
  {
    if (!MonnitSession.IsAuthorizedForAccount(customer.AccountID) || !MonnitSession.CurrentCustomer.IsAdmin || !MonnitSession.AccountCan("muliple_users"))
      return (ActionResult) this.RedirectToRoute("/");
    if (!Customer.CheckUsernameIsUnique(customer.UserName))
      this.ModelState.AddModelError("Username", "Username not available");
    if (!Customer.CheckNotificationEmailIsUnique(customer.NotificationEmail))
      this.ModelState.AddModelError("NotificationEmail", "Email address not available");
    if (!MonnitUtil.IsValidPassword(customer.Password, MonnitSession.CurrentTheme))
      this.ModelState.AddModelError("Password", MonnitUtil.PasswordHelperString(MonnitSession.CurrentTheme));
    if (this.ModelState.IsValid)
    {
      try
      {
        customer.Salt = MonnitUtil.GenerateSalt();
        customer.WorkFactor = ConfigData.AppSettings("WorkFactor").ToInt();
        customer.Password2 = MonnitUtil.GenerateHash(customer.Password, customer.Salt, customer.WorkFactor);
        customer.Password = "";
        customer.Save();
        Account account = Account.Load(customer.AccountID);
        customer.LogAuditData(eAuditAction.Create, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Created a new customer");
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "UserList",
          controller = "Account",
          id = customer.AccountID,
          user = customer.CustomerID,
          tab = "EditNotification"
        });
      }
      catch (Exception ex)
      {
        this.ViewData["Carriers"] = (object) new SelectList((IEnumerable) SMSAccount.SMSList(MonnitSession.CurrentTheme.AccountThemeID), "SMSCarrierID", "SMSCarrierName", (object) customer.SMSCarrierID);
        this.ViewData["AccountID"] = (object) customer.AccountID;
        ViewDataDictionary viewData = this.ViewData;
        viewData["Exception"] = (object) (viewData["Exception"]?.ToString() + ex.Message);
        return (ActionResult) this.View((object) customer);
      }
    }
    else
    {
      this.ViewData["Carriers"] = (object) new SelectList((IEnumerable) SMSAccount.SMSList(MonnitSession.CurrentTheme.AccountThemeID), "SMSCarrierID", "SMSCarrierName", (object) customer.SMSCarrierID);
      this.ViewData["AccountID"] = (object) customer.AccountID;
      return (ActionResult) this.View((object) customer);
    }
  }

  [Authorize]
  public ActionResult Edit(long id)
  {
    CustomerContactInfoModel model = new CustomerContactInfoModel();
    model.Customer = Customer.Load(id);
    model.CustomerInformationList = CustomerInformation.LoadByCustomerID(id);
    return !MonnitSession.IsAuthorizedForAccount(model.Customer.AccountID) ? (ActionResult) this.Content("Not Authorized") : (ActionResult) this.View((object) model);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Edit(long id, FormCollection collection, string returnUrl)
  {
    Customer DBObject = Customer.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(DBObject.AccountID))
      return (ActionResult) this.Content("Not Authorized");
    Account account = Account.Load(DBObject.AccountID);
    DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited customer information");
    if (MonnitSession.CustomerCan("Customer_Change_Username"))
    {
      if (string.IsNullOrEmpty(collection["UserName"]))
        this.ModelState.AddModelError("Username", "Username is Required");
      else if (DBObject.UserName != collection["UserName"] && !Customer.CheckUsernameIsUnique(collection["UserName"]))
        this.ModelState.AddModelError("Username", "Username not available");
      if (DBObject.UserName != collection["UserName"])
        DBObject.ForceLogoutDate = DateTime.UtcNow;
      DBObject.UserName = collection["UserName"];
    }
    DBObject.FirstName = collection["FirstName"];
    DBObject.LastName = collection["LastName"];
    if (MonnitSession.CurrentCustomer.IsAdmin)
    {
      bool isAdmin = DBObject.IsAdmin;
      DBObject.IsAdmin = !string.IsNullOrEmpty(collection["IsAdmin"]);
      if (DBObject.IsAdmin != isAdmin)
      {
        DBObject.ForceLogoutDate = DateTime.UtcNow;
        DBObject.ResetPermissions();
      }
    }
    if (string.IsNullOrEmpty(DBObject.FirstName))
      this.ModelState.AddModelError("FirstName", "First Name is Required!");
    if (string.IsNullOrEmpty(DBObject.LastName))
      this.ModelState.AddModelError("LastName", "Last Name is Required!");
    if (!string.IsNullOrEmpty(collection["Title"]))
      DBObject.Title = collection["Title"];
    if (this.ModelState.IsValid)
    {
      try
      {
        DBObject.Save();
        if (DBObject.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
          MonnitSession.CurrentCustomer = DBObject;
        return (ActionResult) this.Content("Success");
      }
      catch (Exception ex)
      {
        this.ViewData.ModelState.AddModelError("", ex.Message);
      }
    }
    return (ActionResult) this.View((object) new CustomerContactInfoModel()
    {
      Customer = DBObject,
      CustomerInformationList = CustomerInformation.LoadByCustomerID(id)
    });
  }

  [Authorize]
  public ActionResult ExportUserData(long id)
  {
    CustomerContactInfoModel contactInfoModel = new CustomerContactInfoModel();
    Customer customer = Customer.Load(id);
    if (customer == null)
      return (ActionResult) this.Content("<script>alert('No Customer Found');window.location.href = '/Account/AccountUserList';</script>");
    contactInfoModel.Customer = Customer.Load(id);
    contactInfoModel.CustomerInformationList = CustomerInformation.LoadByCustomerID(id);
    if (!MonnitSession.IsAuthorizedForAccount(contactInfoModel.Customer.AccountID))
      return (ActionResult) this.Content("<script>alert('Unauthorized');window.location.href = '/Account/AccountUserList';</script>");
    if (MonnitSession.CurrentCustomer.CustomerID != customer.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Other"))
      return (ActionResult) this.Content("<script>alert('Unauthorized');window.location.href = '/Account/AccountUserList';</script>");
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("User Information as of {0}\r\n", (object) DateTime.Today.ToShortDateString());
    stringBuilder.AppendLine("Key,Value");
    stringBuilder.AppendFormat("FirstName,\"{0}\"\r\n", (object) customer.FirstName);
    stringBuilder.AppendFormat("LastName,\"{0}\"\r\n", (object) customer.LastName);
    stringBuilder.AppendFormat("Title,\"{0}\"\r\n", (object) customer.Title);
    stringBuilder.AppendFormat("UserName,\"{0}\"\r\n", (object) customer.UserName);
    stringBuilder.AppendFormat("NotificaitonEmail,\"{0}\"\r\n", (object) customer.NotificationEmail);
    stringBuilder.AppendFormat("NotificationPhone1,\"{0}\"\r\n", (object) customer.NotificationPhone);
    stringBuilder.AppendFormat("NotificationPhone2,\"{0}\"\r\n", (object) customer.NotificationPhone2);
    if (contactInfoModel.CustomerInformationList.Count > 0)
    {
      foreach (CustomerInformation customerInformation in contactInfoModel.CustomerInformationList)
        stringBuilder.AppendFormat("\"{0}\",\"{1}\"\r\n", (object) customerInformation.InformationType.Name.Trim(), (object) customerInformation.Information.Trim());
    }
    if (customer.SMSCarrier != null)
      stringBuilder.AppendFormat("SMS Carrier Name,\"{0}\"\r\n", (object) customer.SMSCarrier.SMSCarrierName);
    return (ActionResult) this.File(Encoding.ASCII.GetBytes(stringBuilder.ToString()), "text/csv", $"UserData-{DateTime.Now.ToString("yyyyMMdd")}.csv");
  }

  [Authorize]
  public ActionResult UploadImage(long id) => (ActionResult) this.View((object) Customer.Load(id));

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult UploadImage(FormCollection collection)
  {
    Customer model = Customer.Load(collection["CustomerID"].ToLong());
    HttpPostedFileBase file = this.Request.Files["ImageFile"];
    if (file != null)
    {
      try
      {
        model.ImageName = file.FileName;
        model.Bitmap = MonnitUtil.GetCustomerImageBitmap(file.InputStream);
        model.Save();
        // ISSUE: reference to a compiler-generated field
        if (CustomerController.\u003C\u003Eo__13.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomerController.\u003C\u003Eo__13.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "action", typeof (CustomerController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = CustomerController.\u003C\u003Eo__13.\u003C\u003Ep__0.Target((CallSite) CustomerController.\u003C\u003Eo__13.\u003C\u003Ep__0, this.ViewBag, $"window.parent.$('#tabEditCreateContactInfoHolder').load('/Customer/Edit?id={model.CustomerID.ToString()}');");
        // ISSUE: reference to a compiler-generated field
        if (CustomerController.\u003C\u003Eo__13.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomerController.\u003C\u003Eo__13.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "result", typeof (CustomerController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = CustomerController.\u003C\u003Eo__13.\u003C\u003Ep__1.Target((CallSite) CustomerController.\u003C\u003Eo__13.\u003C\u003Ep__1, this.ViewBag, "Image successfully uploaded.");
      }
      catch
      {
        // ISSUE: reference to a compiler-generated field
        if (CustomerController.\u003C\u003Eo__13.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CustomerController.\u003C\u003Eo__13.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "result", typeof (CustomerController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = CustomerController.\u003C\u003Eo__13.\u003C\u003Ep__2.Target((CallSite) CustomerController.\u003C\u003Eo__13.\u003C\u003Ep__2, this.ViewBag, "Image failed to upload.");
      }
    }
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult EditNotification(long id)
  {
    Customer model = Customer.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.Content("Not Authorized");
    this.ViewData["Carriers"] = (object) new SelectList((IEnumerable) SMSAccount.SMSList(MonnitSession.CurrentTheme.AccountThemeID).OrderBy<SMSCarrier, int>((Func<SMSCarrier, int>) (s => s.Rank)).ThenBy<SMSCarrier, string>((Func<SMSCarrier, string>) (s => s.SMSCarrierName)), "SMSCarrierID", "SMSCarrierName", (object) model.SMSCarrierID);
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult EditNotification(long id, FormCollection collection, string returnUrl)
  {
    Customer customer = Customer.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(customer.AccountID))
      return (ActionResult) this.Content("Not Authorized");
    Account account = Account.Load(customer.AccountID);
    customer.LogAuditData(eAuditAction.Update, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited email or phone number for customer notifications");
    customer.NotificationEmail = collection["NotificationEmail"];
    customer.NotificationPhone = collection["NotificationPhone"];
    customer.NotificationPhone2 = collection["NotificationPhone2"];
    customer.SendSensorNotificationToText = !string.IsNullOrEmpty(collection["SendSensorNotificationToText"]);
    if (string.IsNullOrEmpty(MonnitSession.CurrentTheme.FromPhone))
    {
      customer.DirectSMS = false;
      customer.SendSensorNotificationToVoice = false;
    }
    else
    {
      customer.DirectSMS = !string.IsNullOrEmpty(collection["DirectSMS"]);
      customer.SendSensorNotificationToVoice = !string.IsNullOrEmpty(collection["SendSensorNotificationToVoice"]);
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
        if (customer.UISMSCarrierID.HasValue)
        {
          long? uismsCarrierId = customer.UISMSCarrierID;
          long num2 = 0;
          num1 = uismsCarrierId.GetValueOrDefault() <= num2 & uismsCarrierId.HasValue ? 1 : 0;
        }
        else
          num1 = 1;
      }
      else
        num1 = 0;
      if (num1 != 0)
        this.ModelState.AddModelError("UISMSCarrierID", "Required");
      if (customer.SMSCarrier != null && customer.NotificationPhone.RemoveNonNumeric().Count<char>() != customer.SMSCarrier.ExpectedPhoneDigits)
        this.ModelState.AddModelError("NotificationPhone", "Invalid number of digits for selected provider.");
    }
    if (customer.SendSensorNotificationToVoice && customer.NotificationPhone2.Length < 5)
      this.ModelState.AddModelError("NotificationPhone2", "Invalid number");
    customer.AllowPushNotification = collection["AllowPushNotification"] == "on";
    if (this.ModelState.IsValid)
    {
      try
      {
        customer.Save();
        if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
          MonnitSession.CurrentCustomer = customer;
        return (ActionResult) this.Content("Success");
      }
      catch (Exception ex)
      {
        this.ViewData.ModelState.AddModelError("", ex.Message);
      }
    }
    this.ViewData["Carriers"] = (object) new SelectList((IEnumerable) SMSAccount.SMSList(MonnitSession.CurrentTheme.AccountThemeID).OrderBy<SMSCarrier, int>((Func<SMSCarrier, int>) (s => s.Rank)).ThenBy<SMSCarrier, string>((Func<SMSCarrier, string>) (s => s.SMSCarrierName)), "SMSCarrierID", "SMSCarrierName", (object) customer.SMSCarrierID);
    return (ActionResult) this.View((object) customer);
  }

  [Authorize]
  public ActionResult ExternalSMSProviderFormat(long? id, string phone)
  {
    SMSCarrier model = SMSCarrier.Load(id ?? long.MinValue);
    this.ViewData["Phone"] = (object) phone;
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult DirectSMSProviderFormat(string phone)
  {
    this.ViewData["Phone"] = (object) phone;
    return (ActionResult) this.View();
  }

  private bool IsValidEmail(string email)
  {
    try
    {
      return new MailAddress(email).Address == email;
    }
    catch
    {
      return false;
    }
  }

  [Authorize]
  public ActionResult TestEmail(string address)
  {
    if (!string.IsNullOrEmpty(address))
    {
      if (!this.IsValidEmail(address))
        return (ActionResult) this.Content("Email Address Invalid");
      try
      {
        using (MailMessage mail = new MailMessage())
        {
          using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, MonnitSession.CurrentTheme))
          {
            mail.To.Add(string.Format(address));
            mail.Subject = "Test Email";
            mail.Body = "Test Email";
            mail.IsBodyHtml = false;
            if (MonnitUtil.SendMail(mail, smtpClient))
              return (ActionResult) this.Content("Email Sent to " + address);
          }
        }
      }
      catch (Exception ex)
      {
        ex.Log("CustomerController.TestEmail ");
        return (ActionResult) this.Content(ex.Message);
      }
    }
    return (ActionResult) this.Content("Email Address required!");
  }

  [Authorize]
  public ActionResult TestSMS(string phone, string isoCode, long? provider)
  {
    if (string.IsNullOrEmpty(phone))
      return (ActionResult) this.Content("Number required!");
    try
    {
      Country byIsoCodeOrNumber = Country.FindByISOCodeOrNumber(isoCode, phone);
      string fromNumber = MonnitUtil.GetFromNumber(MonnitSession.CurrentCustomer.Account, byIsoCodeOrNumber);
      int num1;
      if (provider.HasValue)
      {
        long? nullable = provider;
        long num2 = 0;
        num1 = nullable.GetValueOrDefault() > num2 & nullable.HasValue ? 1 : 0;
      }
      else
        num1 = 0;
      if (num1 != 0)
      {
        SMSCarrier smsCarrier = SMSCarrier.Load(provider ?? long.MinValue);
        if (smsCarrier != null)
        {
          using (MailMessage mail = new MailMessage())
          {
            using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, MonnitSession.CurrentTheme))
            {
              mail.To.Add(string.Format(smsCarrier.SMSFormatString, (object) phone.RemoveNonNumeric()));
              mail.Subject = "Test SMS";
              mail.Body = "Test SMS";
              mail.IsBodyHtml = false;
              if (MonnitUtil.SendMail(mail, smtpClient))
                return (ActionResult) this.Content("Success");
            }
          }
        }
        return (ActionResult) this.Content("Unable to send test");
      }
      if (string.IsNullOrEmpty(fromNumber))
        return (ActionResult) this.Content("Unrecognized format");
      if (!NotificationCredit.Charge(MonnitSession.CurrentCustomer.AccountID, byIsoCodeOrNumber != null ? byIsoCodeOrNumber.SMSCost : 10))
        return (ActionResult) this.Content("Insufficient Credits");
      string ToNumber = phone.Replace(" ", "").Replace("-", "");
      string body = "Test SMS";
      MonnitUtil.SendTwilioSMS(fromNumber, ToNumber, body);
      CreditSetting.CheckCreditsRemaining(MonnitSession.CurrentCustomer.AccountID, MonnitSession.CurrentCustomer);
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log("CustomerController.TestSMS ");
      return (ActionResult) this.Content(ex.Message);
    }
  }

  [Authorize]
  public ActionResult TestVoice(string phone, string isoCode)
  {
    if (string.IsNullOrEmpty(phone))
      return (ActionResult) this.Content("Number required!");
    try
    {
      if (string.IsNullOrEmpty(MonnitSession.CurrentTheme.FromPhone))
        return (ActionResult) this.Content("Unrecognized format");
      Country byIsoCodeOrNumber = Country.FindByISOCodeOrNumber(isoCode, phone);
      if (!NotificationCredit.Charge(MonnitSession.CurrentCustomer.AccountID, byIsoCodeOrNumber != null ? byIsoCodeOrNumber.VoiceCost : 30))
        return (ActionResult) this.Content("Insufficient Credits");
      MonnitUtil.SendTwilioVoiceMessage(MonnitSession.CurrentTheme.FromPhone, phone.Replace(" ", "").Replace("-", ""), "This is a test");
      NotificationCredit.LoadRemainingCreditsForAccount(MonnitSession.CurrentCustomer.AccountID);
      CreditSetting.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID, new eCreditClassification?(eCreditClassification.Notification));
      CreditSetting.CheckCreditsRemaining(MonnitSession.CurrentCustomer.AccountID, MonnitSession.CurrentCustomer);
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log("CustomerController.TestVoice ");
      return (ActionResult) this.Content(ex.Message);
    }
  }

  [Authorize]
  public ActionResult EditPermissions(long id)
  {
    Customer model = Customer.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID) && !model.IsAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    this.ViewData["CSNetList"] = (object) CSNet.LoadByAccountID(model.AccountID).OrderBy<CSNet, string>((Func<CSNet, string>) (item => item.Name)).ToList<CSNet>();
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult EditPermissions(long id, FormCollection collection, string returnUrl)
  {
    Customer customer = Customer.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(customer.AccountID) && !customer.IsAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
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
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      this.ViewData.ModelState.AddModelError("", ex.Message);
      this.ViewData["CSNetList"] = (object) CSNet.LoadByAccountID(customer.AccountID).OrderBy<CSNet, string>((Func<CSNet, string>) (item => item.Name)).ToList<CSNet>();
      return (ActionResult) this.View((object) customer);
    }
  }

  public static void CustomerPermissionUpdate(NameValueCollection collection, Customer customer)
  {
    List<CSNet> list = CSNet.LoadByAccountID(customer.AccountID).OrderBy<CSNet, string>((Func<CSNet, string>) (item => item.Name)).ToList<CSNet>();
    if (!MonnitSession.CurrentCustomer.IsAdmin)
      return;
    foreach (CustomerPermissionType customerPermissionType in CustomerPermissionType.LoadAll())
    {
      string name = $"Permission_{customerPermissionType.Name.Replace(" ", "_")}";
      if (customerPermissionType.CanEdit(MonnitSession.CurrentCustomer.IsAdmin, MonnitSession.IsCurrentCustomerMonnitAdmin))
      {
        if (customerPermissionType.NetworkSpecific)
        {
          foreach (CSNet csNet in list)
          {
            CustomerPermission customerPermission = new CustomerPermission();
            foreach (CustomerPermission permission in customer.Permissions)
            {
              if (permission.CustomerPermissionTypeID == customerPermissionType.CustomerPermissionTypeID && permission.CSNetID == csNet.CSNetID)
              {
                customerPermission?.Delete();
                customerPermission = permission;
              }
            }
            customerPermission.CSNetID = csNet.CSNetID;
            customerPermission.CustomerID = customer.CustomerID;
            customerPermission.CustomerPermissionTypeID = customerPermissionType.CustomerPermissionTypeID;
            customerPermission.Can = collection[$"{name}_Net_{csNet.CSNetID}"] == "on";
            if (customerPermissionType.RequiresInfo)
              customerPermission.Info = collection[$"{name}_Net_{csNet.CSNetID}_Info"];
            customerPermission.Save();
          }
        }
        else
        {
          bool flag1 = false;
          bool flag2 = false;
          foreach (AccountPermission permission in customer.Account.Permissions)
          {
            if (permission.Type.Name == customerPermissionType.Name)
            {
              flag2 = permission.Can;
              flag1 = permission.OverrideCustomerPermission;
            }
          }
          CustomerPermission customerPermission = (CustomerPermission) null;
          foreach (CustomerPermission permission in customer.Permissions)
          {
            if (permission.CustomerPermissionTypeID == customerPermissionType.CustomerPermissionTypeID)
            {
              customerPermission?.Delete();
              customerPermission = permission;
            }
          }
          if (customerPermission == null)
            customerPermission = new CustomerPermission();
          customerPermission.CustomerID = customer.CustomerID;
          customerPermission.CustomerPermissionTypeID = customerPermissionType.CustomerPermissionTypeID;
          customerPermission.Can = !flag1 ? collection[name] == "on" : flag2;
          if (customerPermissionType.RequiresInfo)
            customerPermission.Info = collection[name + "_Info"];
          customerPermission.Save();
        }
      }
    }
    customer.Permissions = (List<CustomerPermission>) null;
    if (MonnitSession.CurrentCustomer.CustomerID == customer.CustomerID)
    {
      MonnitSession.CurrentCustomer.Permissions = (List<CustomerPermission>) null;
    }
    else
    {
      customer.ForceLogoutDate = DateTime.UtcNow;
      customer.Save();
    }
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult RemoveCustomerLink(long id, long accountid)
  {
    try
    {
      CustomerAccountLink DBObject = CustomerAccountLink.Load(id, accountid);
      Account account = Account.Load(accountid);
      DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Removed customer account link");
      DBObject.AccountDeleted = true;
      DBObject.DateAccountDeleted = DateTime.UtcNow;
      DBObject.Save();
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
      return (ActionResult) this.Content("Failed");
    }
  }

  [Authorize]
  public ActionResult Delete(long id, long accountID)
  {
    // ISSUE: reference to a compiler-generated field
    if (CustomerController.\u003C\u003Eo__26.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CustomerController.\u003C\u003Eo__26.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (CustomerController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = CustomerController.\u003C\u003Eo__26.\u003C\u003Ep__0.Target((CallSite) CustomerController.\u003C\u003Eo__26.\u003C\u003Ep__0, this.ViewBag, accountID);
    Customer model = Customer.Load(id);
    return !MonnitSession.IsAuthorizedForAccount(model.AccountID) && !MonnitSession.IsLinkedToThisAccount(model.CustomerID, accountID) || !MonnitSession.CurrentCustomer.IsAdmin && MonnitSession.CurrentCustomer.CustomerID != model.CustomerID ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Overview",
      controller = "Account"
    }) : (ActionResult) this.View((object) model);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Delete(long id, FormCollection collection)
  {
    try
    {
      Customer DBObject = Customer.Load(id);
      long accountID = collection["AccountID"].ToLong();
      if (!MonnitSession.IsAuthorizedForAccount(DBObject.AccountID) && !MonnitSession.IsLinkedToThisAccount(DBObject.CustomerID, accountID) || !MonnitSession.CurrentCustomer.IsAdmin && MonnitSession.CurrentCustomer.CustomerID != DBObject.CustomerID || MonnitSession.CurrentCustomer.Account.PrimaryContactID == DBObject.CustomerID)
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Overview",
          controller = "Account"
        });
      if (DBObject.AccountID == accountID)
      {
        DBObject.Delete();
        if (MonnitSession.CurrentCustomer.CustomerID != DBObject.CustomerID)
          return (ActionResult) this.Content("Success!<script type=\"text/javascript\">window.location.href = window.location.href;</script>");
        OverviewControllerBase.ClearRememberMeCookie(System.Web.HttpContext.Current.Request);
        this.Session.Clear();
        this.Session.Abandon();
        return (ActionResult) this.Content("Success!<script type=\"text/javascript\">window.location.href = window.location.href;</script>");
      }
      Account account = Account.Load(DBObject.AccountID);
      foreach (CustomerAccountLink customerAccountLink in CustomerAccountLink.LoadAllByCustomerID(DBObject.CustomerID))
      {
        if (customerAccountLink.AccountID == accountID)
        {
          DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Deleted customer from linked accounts");
          customerAccountLink.AccountDeleted = true;
          customerAccountLink.DateAccountDeleted = DateTime.UtcNow;
          customerAccountLink.Save();
          return (ActionResult) this.Content("Success!<script type=\"text/javascript\">window.location.href = window.location.href;</script>");
        }
      }
      return (ActionResult) this.Content("Failed!<script type=\"text/javascript\">window.location.href = window.location.href;</script>");
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
      return (ActionResult) this.View((object) Customer.Load(id));
    }
  }

  [Authorize]
  public ActionResult DefaultPassword(long id)
  {
    if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CurrentCustomer.CustomerID == id || MonnitSession.CustomerCan("Customer_Reset_Password_Other"))
    {
      try
      {
        Customer customer = Customer.Load(id);
        customer.PasswordExpired = true;
        customer.PasswordChangeDate = DateTime.UtcNow;
        customer.Salt = MonnitUtil.GenerateSalt();
        customer.WorkFactor = ConfigData.AppSettings("WorkFactor").ToInt();
        customer.Password2 = MonnitUtil.GenerateHash("password", customer.Salt, customer.WorkFactor);
        customer.Password = "";
        customer.ForceLogoutDate = DateTime.UtcNow;
        customer.Save();
        if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
          MonnitSession.CurrentCustomer = customer;
        return (ActionResult) this.Content("alert('Success! The password has been set to: password');");
      }
      catch
      {
      }
    }
    return (ActionResult) this.Content("alert('Unable to modify password.');");
  }

  [Authorize]
  public ActionResult CalcCredits(string code, string number, bool voice)
  {
    Country byIsoCodeOrNumber = Country.FindByISOCodeOrNumber(code, number);
    if (byIsoCodeOrNumber == null)
      return (ActionResult) this.Content("NaN");
    return voice ? (ActionResult) this.Content(byIsoCodeOrNumber.VoiceCost.ToString()) : (ActionResult) this.Content(byIsoCodeOrNumber.SMSCost.ToString());
  }

  [HttpPost]
  public ActionResult NotificationOptIn(string email)
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
      return (ActionResult) this.Content("Failed");
    }
  }

  [HttpPost]
  public ActionResult NotificationOptOut(string email)
  {
    try
    {
      if (string.IsNullOrWhiteSpace(email))
        return (ActionResult) this.Content("Email hasn't been Opted back in");
      if (!UnsubscribedNotificationEmail.EmailIsUnsubscribed(email))
      {
        new UnsubscribedNotificationEmail()
        {
          EmailAddress = email,
          OptOutDate = DateTime.UtcNow,
          Reason = "Clicked Opt Out button in UI"
        }.Save();
        UnsubscribedNotificationEmail.ClearCachedList();
      }
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  public void SendAddUserToExistingAccountMail(CustomerAccountLink cal, long FromID)
  {
    Customer customer1 = Customer.Load(FromID);
    Account account = Account.Load(customer1.AccountID);
    EmailTemplate emailTemplate = (EmailTemplate) null;
    if (account != null)
    {
      emailTemplate = EmailTemplate.LoadBest(account, eEmailTemplateFlag.Generic) ?? new EmailTemplate();
      if (emailTemplate == null)
        return;
    }
    NotificationRecorded notificationRecorded = new NotificationRecorded();
    notificationRecorded.NotificationType = eNotificationType.Email;
    notificationRecorded.CustomerID = cal.CustomerID;
    notificationRecorded.NotificationDate = DateTime.UtcNow;
    string str1 = $"{customer1.FirstName} {customer1.LastName} would like to add you to their account: {account.CompanyName}.<br />";
    string str2 = "Added to new account";
    notificationRecorded.NotificationSubject = str2;
    AccountTheme accountTheme = account.getTheme();
    if (accountTheme == null)
    {
      accountTheme = new AccountTheme();
      accountTheme.Domain = "localhost:50098";
    }
    string str3 = $"Click <a href=\"{$"http://{accountTheme.Domain}/Account/RegisterToAccount?customerid={cal.CustomerID}&guid={cal.GUID}"}\">accept </a> to continue or you can log into your account and click View Accounts there you can register as well. ";
    string Content = $"{str1} {str3}";
    Customer customer2 = Customer.Load(cal.CustomerID);
    notificationRecorded.NotificationText = emailTemplate.MailMerge(Content, customer2.NotificationEmail);
    Notification.SendNotification(notificationRecorded, new PacketCache());
  }

  [Authorize]
  public ActionResult CustomerNotificationSchedule(long id)
  {
    Customer customer = Customer.Load(id);
    this.AddTimeDropDowns(customer);
    return (ActionResult) this.View((object) customer);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult CustomerNotificationSchedule(long CustomerID, FormCollection collection)
  {
    Customer customer = Customer.Load(CustomerID);
    Account account = Account.Load(customer.AccountID);
    customer.LogAuditData(eAuditAction.Update, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited customer notification schedule");
    CustomerController.SetActivePeriod(customer, collection, this.ModelState);
    if (!this.ModelState.IsValid)
      return (ActionResult) this.Content("failed to save");
    customer.MondaySchedule.Save();
    customer.MondayScheduleID = customer.MondaySchedule.CustomerScheduleID;
    customer.TuesdaySchedule.Save();
    customer.TuesdayScheduleID = customer.TuesdaySchedule.CustomerScheduleID;
    customer.WednesdaySchedule.Save();
    customer.WednesdayScheduleID = customer.WednesdaySchedule.CustomerScheduleID;
    customer.ThursdaySchedule.Save();
    customer.ThursdayScheduleID = customer.ThursdaySchedule.CustomerScheduleID;
    customer.FridaySchedule.Save();
    customer.FridayScheduleID = customer.FridaySchedule.CustomerScheduleID;
    customer.SaturdaySchedule.Save();
    customer.SaturdayScheduleID = customer.SaturdaySchedule.CustomerScheduleID;
    customer.SundaySchedule.Save();
    customer.SundayScheduleID = customer.SundaySchedule.CustomerScheduleID;
    customer.Save();
    if (customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
      MonnitSession.CurrentCustomer = customer;
    return (ActionResult) this.Content($"<script>window.location.href = '{"/Account/AccountUserList"}'</script>");
  }

  [Authorize]
  public ActionResult ContactInfo(long id)
  {
    return (ActionResult) this.View((object) new CustomerContactInfoModel()
    {
      Customer = Customer.Load(id),
      CustomerInformationList = CustomerInformation.LoadByCustomerID(id)
    });
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult CustomerInformationList(long id)
  {
    return (ActionResult) this.View((object) Customer.LoadAllByAccount(id));
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult EditCustomerInformation(
    long customerInformationTypeID,
    string information,
    long customerID)
  {
    try
    {
      new CustomerInformation()
      {
        CustomerInformationTypeID = customerInformationTypeID,
        Information = information,
        CustomerID = customerID
      }.Save();
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult DeleteCustomerInformation(long id)
  {
    try
    {
      CustomerInformation.Load(id).Delete();
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  private void AddTimeDropDowns(Customer customer)
  {
    this.ViewData["ShowSchedule"] = (object) true;
    this.ViewData["ShowTimeOfDay"] = (object) true;
    this.AddTimeDropDowns(customer, DayOfWeek.Monday);
    this.AddTimeDropDowns(customer, DayOfWeek.Tuesday);
    this.AddTimeDropDowns(customer, DayOfWeek.Wednesday);
    this.AddTimeDropDowns(customer, DayOfWeek.Thursday);
    this.AddTimeDropDowns(customer, DayOfWeek.Friday);
    this.AddTimeDropDowns(customer, DayOfWeek.Saturday);
    this.AddTimeDropDowns(customer, DayOfWeek.Sunday);
  }

  private void AddTimeDropDowns(Customer customer, DayOfWeek day)
  {
    try
    {
      CustomerSchedule customerSchedule = customer.GetCustomerSchedule(day);
      this.ViewData[day.ToString() + "Schedule.CustomerDaySchedule"] = (object) customerSchedule.CustomerDaySchedule.ToString();
      TimeSpan timeSpan1 = customerSchedule.FirstTime != TimeSpan.MinValue ? customerSchedule.FirstTime : new TimeSpan(0L);
      TimeSpan timeSpan2 = customerSchedule.SecondTime != TimeSpan.MinValue ? customerSchedule.SecondTime : new TimeSpan(0L);
      string[] strArray = new string[12]
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
      ViewDataDictionary viewData1 = this.ViewData;
      string key1 = day.ToString() + "StartHours";
      string[] items1 = strArray;
      int num = timeSpan1.Hours > 12 ? timeSpan1.Hours - 12 : (timeSpan1.Hours == 0 ? 12 : timeSpan1.Hours);
      string selectedValue1 = num.ToString();
      SelectList selectList1 = new SelectList((IEnumerable) items1, (object) selectedValue1);
      viewData1[key1] = (object) selectList1;
      ViewDataDictionary viewData2 = this.ViewData;
      string key2 = day.ToString() + "StartMinutes";
      string[] items2 = new string[4]
      {
        "00",
        "15",
        "30",
        "45"
      };
      num = timeSpan1.Minutes;
      SelectList selectList2 = new SelectList((IEnumerable) items2, (object) num.ToString());
      viewData2[key2] = (object) selectList2;
      this.ViewData[day.ToString() + "StartAM"] = (object) new SelectList((IEnumerable) new string[2]
      {
        "AM",
        "PM"
      }, timeSpan1.Hours < 12 ? (object) "AM" : (object) "PM");
      ViewDataDictionary viewData3 = this.ViewData;
      string key3 = day.ToString() + "EndHours";
      string[] items3 = strArray;
      num = timeSpan2.Hours > 12 ? timeSpan2.Hours - 12 : (timeSpan2.Hours == 0 ? 12 : timeSpan2.Hours);
      string selectedValue2 = num.ToString();
      SelectList selectList3 = new SelectList((IEnumerable) items3, (object) selectedValue2);
      viewData3[key3] = (object) selectList3;
      ViewDataDictionary viewData4 = this.ViewData;
      string key4 = day.ToString() + "EndMinutes";
      string[] items4 = new string[4]
      {
        "00",
        "15",
        "30",
        "45"
      };
      num = timeSpan2.Minutes;
      SelectList selectList4 = new SelectList((IEnumerable) items4, (object) num.ToString());
      viewData4[key4] = (object) selectList4;
      this.ViewData[day.ToString() + "EndAM"] = (object) new SelectList((IEnumerable) new string[2]
      {
        "AM",
        "PM"
      }, timeSpan2.Hours < 12 ? (object) "AM" : (object) "PM");
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
    }
  }

  private static void SetActivePeriod(
    Customer customer,
    FormCollection collection,
    ModelStateDictionary modelstate)
  {
    customer.MondaySchedule = CustomerController.SetDaySchedule(customer, collection, DayOfWeek.Monday, modelstate);
    customer.TuesdaySchedule = CustomerController.SetDaySchedule(customer, collection, DayOfWeek.Tuesday, modelstate);
    customer.WednesdaySchedule = CustomerController.SetDaySchedule(customer, collection, DayOfWeek.Wednesday, modelstate);
    customer.ThursdaySchedule = CustomerController.SetDaySchedule(customer, collection, DayOfWeek.Thursday, modelstate);
    customer.FridaySchedule = CustomerController.SetDaySchedule(customer, collection, DayOfWeek.Friday, modelstate);
    customer.SaturdaySchedule = CustomerController.SetDaySchedule(customer, collection, DayOfWeek.Saturday, modelstate);
    customer.SundaySchedule = CustomerController.SetDaySchedule(customer, collection, DayOfWeek.Sunday, modelstate);
    customer.AlwaysSend = !string.IsNullOrWhiteSpace(collection["AlwaysSend"]) && collection["AlwaysSend"].Contains("true");
  }

  private static CustomerSchedule SetDaySchedule(
    Customer cust,
    FormCollection collection,
    DayOfWeek day,
    ModelStateDictionary modelstate)
  {
    CustomerSchedule customerSchedule = cust.GetCustomerSchedule(day);
    int hours1 = 0;
    int minutes1 = 0;
    int hours2 = 0;
    int minutes2 = 0;
    switch (collection[day.ToString() + "Schedule.CustomerDaySchedule"])
    {
      case "All_Day":
        customerSchedule.CustomerDaySchedule = eNotificationDaySchedule.All_Day;
        break;
      case "Off":
        customerSchedule.CustomerDaySchedule = eNotificationDaySchedule.Off;
        break;
      case "Between":
        customerSchedule.CustomerDaySchedule = eNotificationDaySchedule.Between;
        hours1 = collection[day.ToString() + "StartTimeHour"].ToInt();
        if (hours1 == 12 && collection[day.ToString() + "StartTimeAM"] == "PM")
          hours1 = hours1;
        else if (hours1 == 12 && collection[day.ToString() + "StartTimeAM"] == "AM")
          hours1 -= 12;
        else if (collection[day.ToString() + "StartTimeAM"] == "PM")
          hours1 += 12;
        minutes1 = collection[day.ToString() + "StartTimeMinute"].ToInt();
        hours2 = collection[day.ToString() + "EndTimeHour"].ToInt();
        if (hours2 == 12 && collection[day.ToString() + "EndTimeAM"] == "PM")
          hours2 = hours2;
        else if (hours2 == 12 && collection[day.ToString() + "EndTimeAM"] == "AM")
          hours2 -= 12;
        else if (collection[day.ToString() + "EndTimeAM"] == "PM")
          hours2 += 12;
        minutes2 = collection[day.ToString() + "EndTimeMinute"].ToInt();
        if (hours1 == hours2 && minutes1 == minutes2 && collection[day.ToString() + "EndTimeAM"].Equals(collection[day.ToString() + "StartTimeAM"]))
        {
          modelstate.AddModelError(day.ToString() + "Schedule.CustomerDaySchedule", "invalid schedule");
          break;
        }
        if (hours1 >= hours2 && minutes1 >= minutes2 && collection[day.ToString() + "EndTimeAM"].Equals(collection[day.ToString() + "StartTimeAM"]))
        {
          modelstate.AddModelError(day.ToString() + "Schedule.CustomerDaySchedule", "invalid schedule");
          break;
        }
        break;
      case "Before_and_After":
        customerSchedule.CustomerDaySchedule = eNotificationDaySchedule.Before_and_After;
        hours1 = collection[day.ToString() + "StartTimeHour"].ToInt();
        if (hours1 == 12 && collection[day.ToString() + "StartTimeAM"] == "PM")
          hours1 = hours1;
        else if (hours1 == 12 && collection[day.ToString() + "StartTimeAM"] == "AM")
          hours1 -= 12;
        else if (collection[day.ToString() + "StartTimeAM"] == "PM")
          hours1 += 12;
        minutes1 = collection[day.ToString() + "StartTimeMinute"].ToInt();
        hours2 = collection[day.ToString() + "EndTimeHour"].ToInt();
        if (hours2 == 12 && collection[day.ToString() + "EndTimeAM"] == "PM")
          hours2 = hours2;
        else if (hours2 == 12 && collection[day.ToString() + "EndTimeAM"] == "AM")
          hours2 -= 12;
        else if (collection[day.ToString() + "EndTimeAM"] == "PM")
          hours2 += 12;
        minutes2 = collection[day.ToString() + "EndTimeMinute"].ToInt();
        if (hours1 == hours2 && minutes1 == minutes2 && collection[day.ToString() + "EndTimeAM"].Equals(collection[day.ToString() + "StartTimeAM"]))
        {
          modelstate.AddModelError(day.ToString() + "Schedule.CustomerDaySchedule", string.Format("invalid schedule"));
          break;
        }
        if (hours1 >= hours2 && minutes1 >= minutes2 && collection[day.ToString() + "EndTimeAM"].Equals(collection[day.ToString() + "StartTimeAM"]))
        {
          modelstate.AddModelError(day.ToString() + "Schedule.CustomerDaySchedule", "invalid schedule");
          break;
        }
        if (hours2 == 0 && minutes2 == 0 && collection[day.ToString() + "EndTimeAM"] == "AM" || hours1 == 0 && minutes1 == 0 && collection[day.ToString() + "StartTimeAM"] == "AM")
        {
          modelstate.AddModelError(day.ToString() + "Schedule.CustomerDaySchedule", string.Format("invalid schedule"));
          modelstate.AddModelError(day.ToString() + "Schedule.CustomerDaySchedule", string.Format("invalid schedule"));
          break;
        }
        break;
      case "Before":
        customerSchedule.CustomerDaySchedule = eNotificationDaySchedule.Before;
        hours2 = 0;
        minutes2 = 0;
        hours1 = collection[day.ToString() + "StartTimeHour"].ToInt();
        if (hours1 == 12 && collection[day.ToString() + "StartTimeAM"] == "PM")
          hours1 = hours1;
        else if (hours1 == 12 && collection[day.ToString() + "StartTimeAM"] == "AM")
          hours1 -= 12;
        else if (collection[day.ToString() + "StartTimeAM"] == "PM")
          hours1 += 12;
        minutes1 = collection[day.ToString() + "StartTimeMinute"].ToInt();
        if (hours1 == 0 && minutes1 == 0 && collection[day.ToString() + "StartTimeAM"] == "AM")
        {
          modelstate.AddModelError(day.ToString() + "Schedule.CustomerDaySchedule", string.Format("invalid schedule"));
          break;
        }
        break;
      case "After":
        customerSchedule.CustomerDaySchedule = eNotificationDaySchedule.After;
        hours1 = 0;
        minutes1 = 0;
        hours2 = collection[day.ToString() + "EndTimeHour"].ToInt();
        if (hours2 == 12 && collection[day.ToString() + "EndTimeAM"] == "PM")
          hours2 = hours2;
        else if (hours2 == 12 && collection[day.ToString() + "EndTimeAM"] == "AM")
          hours2 -= 12;
        else if (collection[day.ToString() + "EndTimeAM"] == "PM")
          hours2 += 12;
        minutes2 = collection[day.ToString() + "EndTimeMinute"].ToInt();
        if (hours2 == 0 && minutes2 == 0 && collection[day.ToString() + "EndTimeAM"] == "AM")
        {
          modelstate.AddModelError(day.ToString() + "Schedule.CustomerDaySchedule", string.Format("invalid schedule"));
          break;
        }
        break;
    }
    customerSchedule.FirstTime = new TimeSpan(hours1, minutes1, 0);
    customerSchedule.SecondTime = new TimeSpan(hours2, minutes2, 0);
    return customerSchedule;
  }

  [Authorize]
  public ActionResult CookieAcceptance()
  {
    try
    {
      MonnitSession.CurrentCustomer.CookieAcceptanceDate = DateTime.UtcNow;
      MonnitSession.CurrentCustomer.Save();
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }
}
