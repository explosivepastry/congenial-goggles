// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.APIController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.API;
using iMonnit.ControllerBase;
using iMonnit.Models;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

#nullable disable
namespace iMonnit.Controllers;

[AllowCrossSiteJson]
public class APIController : MonnitController
{
  protected override void OnException(ExceptionContext filterContext)
  {
    string str = filterContext.RouteData.Values["action"].ToString();
    Exception exception = filterContext.Exception;
    string function = $"APIController.OnException: {filterContext.RouteData.Values["controller"]}Controller.{filterContext.RouteData.Values["action"]}";
    exception.Log(function);
    filterContext.ExceptionHandled = true;
    filterContext.Result = (ActionResult) this.Json((object) new
    {
      Error = exception.Message,
      APIEndpoint = str
    });
    base.OnException(filterContext);
  }

  public ActionResult Index() => (ActionResult) this.View();

  public ActionResult CodeSample(string id) => (ActionResult) this.View(nameof (CodeSample) + id);

  public ActionResult GetAuthToken(string username, string password)
  {
    return this.FormatRequest((object) Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));
  }

  [AuthorizeAPI]
  public ActionResult Logon() => this.FormatRequest((object) "Success");

  [AuthorizeAPI]
  public ActionResult ClearCache()
  {
    this._ClearCache();
    return this.FormatRequest((object) "Success");
  }

  private void _ClearCache()
  {
    try
    {
      string authString = AuthorizeAPIAttribute.GetAuthString(this.ControllerContext.HttpContext, this.ControllerContext.RouteData);
      string secretString = AuthorizeAPIAttribute.GetSecretString(this.ControllerContext.HttpContext);
      TimedCache.RemoveObject($"API_Auth_{authString}{secretString}");
      TimedCache.RemoveObject($"API_Auth_Premium_{authString}{secretString}");
    }
    catch (Exception ex)
    {
      ex.Log("APIController._ClearCache");
    }
  }

  public ActionResult TimeZones()
  {
    try
    {
      return this.FormatRequest((object) Monnit.TimeZone.LoadAll().Select<Monnit.TimeZone, APINameID>((System.Func<Monnit.TimeZone, APINameID>) (obj => new APINameID(obj.TimeZoneID, obj.DisplayName))).ToList<APINameID>());
    }
    catch (Exception ex)
    {
      ex.Log("APIController.TimeZones");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  public ActionResult TimeZones2()
  {
    try
    {
      return this.FormatRequest((object) Monnit.TimeZone.LoadAll().Select<Monnit.TimeZone, APINameID>((System.Func<Monnit.TimeZone, APINameID>) (obj => new APINameID(obj.TimeZoneID, obj.TimeZoneIDString))).ToList<APINameID>());
    }
    catch (Exception ex)
    {
      ex.Log("APIController.TimeZones2");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  public ActionResult TimeZonesWithRegion()
  {
    try
    {
      return this.FormatRequest((object) Monnit.TimeZone.LoadAll().Select<Monnit.TimeZone, APITimeZone>((System.Func<Monnit.TimeZone, APITimeZone>) (obj => new APITimeZone(obj))).ToList<APITimeZone>());
    }
    catch (Exception ex)
    {
      ex.Log("APIController.TimeZones3");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  public ActionResult SMSCarriers()
  {
    try
    {
      return this.FormatRequest((object) SMSCarrier.LoadAll().Select<SMSCarrier, APINameID>((System.Func<SMSCarrier, APINameID>) (obj => new APINameID(obj.SMSCarrierID, obj.SMSCarrierName))).ToList<APINameID>());
    }
    catch (Exception ex)
    {
      ex.Log("APIController.SMSCarrierss");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  public ActionResult GetApplicationID()
  {
    long accountThemeID = MonnitSession.CurrentTheme.AccountThemeID;
    try
    {
      return this.FormatRequest((object) MonnitApplication.LoadAll().Where<MonnitApplication>((System.Func<MonnitApplication, bool>) (at => at.AccountThemeID == accountThemeID || at.AccountThemeID == long.MinValue)).ToList<MonnitApplication>().Select<MonnitApplication, APINameID>((System.Func<MonnitApplication, APINameID>) (obj => new APINameID(obj.ApplicationID, obj.ApplicationName))).ToList<APINameID>());
    }
    catch (Exception ex)
    {
      ex.Log("APIController.GetApplicationID");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  public ActionResult GetDatumByType(long applicationID)
  {
    try
    {
      List<AppDatum> appDatums = MonnitApplicationBase.GetAppDatums(applicationID);
      List<APIDatumType> apiDatumTypeList = new List<APIDatumType>();
      int index = 0;
      foreach (AppDatum datum in appDatums)
      {
        apiDatumTypeList.Add(new APIDatumType(datum, index));
        ++index;
      }
      return this.FormatRequest((object) apiDatumTypeList);
    }
    catch
    {
      return this.FormatRequest((object) "Invalid AppliationID");
    }
  }

  public ActionResult GetGatewayTypeID()
  {
    try
    {
      return this.FormatRequest((object) GatewayType.LoadAll().Select<GatewayType, APINameID>((System.Func<GatewayType, APINameID>) (obj => new APINameID(obj.GatewayTypeID, obj.Name))).ToList<APINameID>());
    }
    catch (Exception ex)
    {
      ex.Log("APIController.GetGatewayTypeID");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  public ActionResult GetBusinessType()
  {
    return this.FormatRequest((object) IndustryClassification.LoadAll().Where<IndustryClassification>((System.Func<IndustryClassification, bool>) (parid =>
    {
      long parent = parid.Parent;
      return parid.Parent == long.MinValue;
    })).ToList<IndustryClassification>().Select<IndustryClassification, APINameID>((System.Func<IndustryClassification, APINameID>) (obj => new APINameID(obj.IndustryClassificationID, obj.Name))).ToList<APINameID>());
  }

  public ActionResult GetIndustryType(long id)
  {
    return this.FormatRequest((object) IndustryClassification.LoadByParent(new long?(id)).Select<IndustryClassification, APINameID>((System.Func<IndustryClassification, APINameID>) (obj => new APINameID(obj.IndustryClassificationID, obj.Name))).ToList<APINameID>());
  }

  public ActionResult UserNameAvailable(string username)
  {
    double minuteToWait;
    if (ThemeController.CheckBlacklist(this.Request.ClientIP(), ConfigData.AppSettings("IPBlacklistTime", "5").ToInt(), 10, out IPBlacklist _, out minuteToWait))
      return this.FormatRequest((object) $"You've exceeded the request limit, please wait {minuteToWait} minute(s) before trying again.");
    return !string.IsNullOrWhiteSpace(username) && Customer.CheckUsernameIsUnique(username.ToLower()) ? this.FormatRequest((object) "true") : this.FormatRequest((object) $"Username: {username} is unavailable");
  }

  public ActionResult NotificationEmailAvailable(string notificationEmail)
  {
    double minuteToWait;
    if (ThemeController.CheckBlacklist(this.Request.ClientIP(), ConfigData.AppSettings("IPBlacklistTime", "5").ToInt(), 10, out IPBlacklist _, out minuteToWait))
      return this.FormatRequest((object) $"You've exceeded the request limit, please wait {minuteToWait} minute(s) before trying again.");
    return !string.IsNullOrWhiteSpace(notificationEmail) && Customer.CheckNotificationEmailIsUnique(notificationEmail) ? this.FormatRequest((object) "true") : this.FormatRequest((object) $"Email: {notificationEmail} is unavailable");
  }

  public ActionResult AccountNumberAvailable(string accountNumber)
  {
    double minuteToWait;
    if (ThemeController.CheckBlacklist(this.Request.ClientIP(), ConfigData.AppSettings("IPBlacklistTime", "5").ToInt(), 10, out IPBlacklist _, out minuteToWait))
      return this.FormatRequest((object) $"You've exceeded the request limit, please wait {minuteToWait} minute(s) before trying again.");
    return !string.IsNullOrWhiteSpace(accountNumber) && Account.CheckAccountNumberIsUnique(accountNumber.ToLower()) ? this.FormatRequest((object) "true") : this.FormatRequest((object) $"Account Number: {accountNumber} is unavailable");
  }

  public ActionResult GatewayLookUp(long gatewayID, string checkDigit)
  {
    if (!MonnitUtil.ValidateCheckDigit(gatewayID, checkDigit))
      return this.FormatRequest((object) "Check digit did not match.");
    Gateway gateway = Gateway.Load(gatewayID);
    return gateway != null && !gateway.IsDeleted ? this.FormatRequest((object) new APIGatewayMetaData(gateway)) : this.FormatRequest((object) "Invalid GatewayID");
  }

  public ActionResult SensorLookUp(long sensorID, string checkDigit)
  {
    if (!MonnitUtil.ValidateCheckDigit(sensorID, checkDigit))
      return this.FormatRequest((object) "Check digit did not match.");
    Sensor sensor = Sensor.Load(sensorID);
    return sensor != null && !sensor.IsDeleted ? this.FormatRequest((object) new APISensorMetaData(sensor)) : this.FormatRequest((object) "Invalid SensorID");
  }

  public ActionResult CableLookUp(long cableID)
  {
    Cable cable = Cable.Load(cableID);
    return cable != null ? this.FormatRequest((object) new APICableMetaData(cable)) : this.FormatRequest((object) "Invalid CableID");
  }

  public ActionResult CreateAccount(CreateAccountAPIModel model, bool? sendEmail)
  {
    List<APIValidationError> apiValidationErrorList = new List<APIValidationError>();
    List<APIValidationError> list = this.ModelState.SelectMany<KeyValuePair<string, System.Web.Mvc.ModelState>, APIValidationError>((System.Func<KeyValuePair<string, System.Web.Mvc.ModelState>, IEnumerable<APIValidationError>>) (kvp => kvp.Value.Errors.Select<ModelError, APIValidationError>((System.Func<ModelError, APIValidationError>) (v => new APIValidationError(kvp.Key, v.ErrorMessage))))).ToList<APIValidationError>();
    if (list.Count != 0)
      return this.FormatRequest((object) list);
    if (!Account.CheckAccountNumberIsUnique(model.AccountNumber))
      list.Add(new APIValidationError("Account Number", "Account Number already exists"));
    if (!Customer.CheckUsernameIsUnique(model.UserName))
      list.Add(new APIValidationError("UserName", "Username not available"));
    if (!Customer.CheckNotificationEmailIsUnique(model.NotificationEmail))
      list.Add(new APIValidationError("NotificationEmail", "Email address not available"));
    if (!MonnitUtil.IsValidPassword(model.Password, MonnitSession.CurrentTheme))
      list.Add(new APIValidationError("Password", MonnitUtil.PasswordHelperString(MonnitSession.CurrentTheme)));
    if (!Monnit.TimeZone.IsValidTimeZone(model.TimeZoneID.ToLong()))
      list.Add(new APIValidationError("TimeZoneID", $"Invalid TimeZoneID: {model.TimeZoneID.ToLong()}"));
    if (list.Count == 0)
    {
      try
      {
        model.PremiumDays = MonnitUtil.DefaultPremiumLength(MonnitSession.CurrentTheme);
        model.ResellerID = MonnitSession.CurrentTheme.AccountID.ToString();
        Account a = AccountController.InsertAccount(model, sendEmail ?? true, MonnitUtil.DefaultAccountSubscription(MonnitSession.CurrentTheme));
        CSNet csNet = new CSNet();
        csNet.AccountID = a.AccountID;
        csNet.Name = !string.IsNullOrEmpty(model.NetworkName) ? model.NetworkName : a.CompanyName + model.GatewayID.ToString();
        csNet.SendNotifications = true;
        csNet.Save();
        new CustomerPermission()
        {
          CSNetID = csNet.CSNetID,
          CustomerID = a.PrimaryContact.CustomerID,
          CustomerPermissionTypeID = CustomerPermissionType.Find("Network_View").CustomerPermissionTypeID,
          Can = true
        }.Save();
        if (!a.IsPremium)
          a.Save();
        this._ClearCache();
        return this.FormatRequest((object) new APIAccount(a));
      }
      catch (Exception ex)
      {
        ex.Log("APIController.CreateAccount");
        list.Add(new APIValidationError("", "Unexpected error. if Problem continues create your account directly through the online portal"));
      }
    }
    return this.FormatRequest((object) list);
  }

  [AuthorizeAPIPremier]
  public ActionResult CreateSubAccount(
    CreateAccountAPIModel model,
    long? parentID,
    bool? isAdmin,
    bool? sendEmail,
    bool? isReseller)
  {
    List<APIValidationError> apiValidationErrorList = new List<APIValidationError>();
    foreach (string key in (IEnumerable<string>) this.ModelState.Keys)
    {
      foreach (ModelError error in (Collection<ModelError>) this.ModelState[key].Errors)
        apiValidationErrorList.Add(new APIValidationError(key, error.ErrorMessage));
    }
    Account acct = (Account) null;
    if (parentID.HasValue)
    {
      acct = Account.Load(parentID.GetValueOrDefault());
      if (acct == null)
        apiValidationErrorList.Add(new APIValidationError("Parent", "Parent account not found"));
      if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
        apiValidationErrorList.Add(new APIValidationError("Parent", "Account Access is unauthorized"));
      if (!MonnitSession.IsAuthorizedForAccount(parentID.GetValueOrDefault()))
        apiValidationErrorList.Add(new APIValidationError("Parent", "Account is unauthorized"));
    }
    if (!Customer.CheckUsernameIsUnique(model.UserName))
      apiValidationErrorList.Add(new APIValidationError("UserName", "Username not available"));
    if (!Customer.CheckNotificationEmailIsUnique(model.NotificationEmail))
      apiValidationErrorList.Add(new APIValidationError("NotificationEmail", "Email address not available"));
    if (!Account.CheckAccountNumberIsUnique(model.AccountNumber))
      apiValidationErrorList.Add(new APIValidationError("AccountNumber", "Account Number already exists"));
    if (!MonnitUtil.IsValidPassword(model.Password, MonnitSession.CurrentTheme))
      apiValidationErrorList.Add(new APIValidationError("Password", MonnitUtil.PasswordHelperString(MonnitSession.CurrentTheme)));
    if (!Monnit.TimeZone.IsValidTimeZone(model.TimeZoneID.ToLong()))
      apiValidationErrorList.Add(new APIValidationError("TimeZoneID", "Invalid TimeZoneID: " + model.TimeZoneID));
    if (apiValidationErrorList.Count == 0)
    {
      try
      {
        model.ResellerID = acct != null ? acct.AccountID.ToString() : MonnitSession.CurrentCustomer.AccountID.ToString();
        model.PremiumDays = MonnitUtil.DefaultPremiumLength(MonnitSession.CurrentTheme);
        Account a = AccountController.InsertAccount(model, sendEmail ?? true, MonnitUtil.DefaultAccountSubscription(MonnitSession.CurrentTheme));
        Customer customer = Customer.Load(a.PrimaryContactID);
        if (!(isAdmin ?? true))
        {
          customer.IsAdmin = false;
          customer.ResetPermissions();
          customer.Save();
        }
        if (MonnitSession.CurrentCustomer.Account.IsPremium)
          a.Save();
        CSNet csNet = new CSNet();
        csNet.AccountID = a.AccountID;
        csNet.Name = !string.IsNullOrEmpty(model.NetworkName) ? model.NetworkName : a.CompanyName + model.GatewayID.ToString();
        csNet.SendNotifications = true;
        csNet.Save();
        new CustomerPermission()
        {
          CSNetID = csNet.CSNetID,
          CustomerID = a.PrimaryContact.CustomerID,
          CustomerPermissionTypeID = CustomerPermissionType.Find("Network_View").CustomerPermissionTypeID,
          Can = true
        }.Save();
        this._ClearCache();
        return this.FormatRequest((object) new APIAccount(a));
      }
      catch (Exception ex)
      {
        ex.Log("APIController.CreateAccount");
        apiValidationErrorList.Add(new APIValidationError("", "Unexpected error. if Problem continues create your account directly through the online portal"));
      }
    }
    return this.FormatRequest((object) apiValidationErrorList);
  }

  [AuthorizeAPIPremier]
  public ActionResult CreateSubAccount2(
    CreateAccountAPIModel2 model,
    long? ParentAccountID,
    bool? isReseller)
  {
    List<APIValidationError> apiValidationErrorList = new List<APIValidationError>();
    foreach (string key in (IEnumerable<string>) this.ModelState.Keys)
    {
      foreach (ModelError error in (Collection<ModelError>) this.ModelState[key].Errors)
        apiValidationErrorList.Add(new APIValidationError(key, error.ErrorMessage));
    }
    Account acct;
    if (ParentAccountID.HasValue)
    {
      acct = Account.Load(ParentAccountID.GetValueOrDefault());
      if (acct == null)
      {
        apiValidationErrorList.Add(new APIValidationError(nameof (ParentAccountID), "Parent account not found"));
        acct = MonnitSession.CurrentCustomer.Account;
      }
    }
    else
      acct = MonnitSession.CurrentCustomer.Account;
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      apiValidationErrorList.Add(new APIValidationError(nameof (ParentAccountID), "Account Access is unauthorized"));
    if (!MonnitSession.IsAuthorizedForAccount(acct.AccountID))
      apiValidationErrorList.Add(new APIValidationError(nameof (ParentAccountID), "Account is unauthorized"));
    Customer customer = Customer.Load(model.CustomerID);
    if (customer != null && !customer.CanSeeAccount(ParentAccountID.GetValueOrDefault()))
      apiValidationErrorList.Add(new APIValidationError("CustomerID", "Invalid CustomerID"));
    if (!Account.CheckAccountNumberIsUnique(model.CompanyName))
      apiValidationErrorList.Add(new APIValidationError("CompanyName", "Truck Number already exists"));
    if (apiValidationErrorList.Count == 0)
    {
      try
      {
        model.ResellerID = acct.AccountID.ToString();
        model.PremiumDays = MonnitUtil.DefaultPremiumLength(MonnitSession.CurrentTheme);
        Account a = AccountController.InsertAccount2(model, false, MonnitUtil.DefaultAccountSubscription(MonnitSession.CurrentTheme));
        new CSNet()
        {
          AccountID = a.AccountID,
          Name = (!string.IsNullOrEmpty(model.NetworkName) ? model.NetworkName : a.CompanyName + model.GatewayID.ToString()),
          SendNotifications = true
        }.Save();
        this._ClearCache();
        return this.FormatRequest((object) new APIAccount(a));
      }
      catch (Exception ex)
      {
        ex.Log("APIController.CreateSubAccount2 ");
        apiValidationErrorList.Add(new APIValidationError("", "Unexpected error. if Problem continues create your account directly through the online portal"));
      }
    }
    return this.FormatRequest((object) apiValidationErrorList);
  }

  [AuthorizeAPIPremier]
  public ActionResult AccountGet(long? accountID)
  {
    Account account = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    return !MonnitSession.CurrentCustomer.CanSeeAccount(account) ? this.FormatRequest((object) "Invalid Request: no account found") : this.FormatRequest((object) new APIAccount(account));
  }

  [AuthorizeAPIPremier]
  public ActionResult RemoveSubAccount(long accountID)
  {
    Account DBObject1 = Account.Load(accountID);
    if (DBObject1 == null || !MonnitSession.CurrentCustomer.CanSeeAccount(DBObject1.AccountID))
      return this.FormatRequest((object) "Account Not Found");
    List<APIValidationError> apiValidationErrorList = new List<APIValidationError>();
    foreach (Sensor DBObject2 in Sensor.LoadByAccountID(accountID))
    {
      try
      {
        DBObject2.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, DBObject1.AccountID, "Removed sensors from subaccount");
        DBObject2.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
        DBObject2.AccountID = long.MinValue;
        DBObject2.Save();
        Gateway DBObject3 = Gateway.LoadBySensorID(DBObject2.SensorID);
        if (DBObject3 != null)
        {
          DBObject3.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, DBObject1.AccountID, "Removed gateway from subaccount");
          DBObject3.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
          DBObject3.ResetToDefault(false);
          DBObject3.Save();
        }
      }
      catch
      {
      }
    }
    if (Sensor.LoadByAccountID(accountID).Count > 0)
      apiValidationErrorList.Add(new APIValidationError("Sensors", "Unable to remove all sensors from account."));
    foreach (Gateway DBObject4 in Gateway.LoadByAccountID(accountID))
    {
      try
      {
        DBObject4.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, DBObject1.AccountID, "Removed gateway from subaccount");
        DBObject4.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
        DBObject4.ResetToDefault(false);
        DBObject4.Save();
      }
      catch
      {
      }
    }
    if (Gateway.LoadByAccountID(accountID).Count > 0)
      apiValidationErrorList.Add(new APIValidationError("Gateways", "Unable to remove all gateways from account."));
    if (apiValidationErrorList.Count != 0)
      return this.FormatRequest((object) apiValidationErrorList);
    try
    {
      DBObject1.LogAuditData(eAuditAction.Delete, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, DBObject1.AccountID, "Removed  subaccount");
      Account.Remove(DBObject1.AccountID);
      return this.FormatRequest((object) "Success");
    }
    catch (Exception ex)
    {
      ex.Log($"APIController.RemoveSubAccount(accountID: {accountID})");
      return this.FormatRequest((object) "Unable to remove account");
    }
  }

  [AuthorizeAPIPremier]
  public ActionResult SubAccountList(string name, long? accountID)
  {
    long accountId;
    if (accountID.HasValue)
    {
      Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
      if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
        return this.FormatRequest((object) "Invalid Request: no account found");
      accountId = acct.AccountID;
    }
    else
      accountId = MonnitSession.CurrentCustomer.AccountID;
    string stringSafe = name.ToStringSafe();
    return this.FormatRequest((object) Account.Search(accountId, stringSafe, 5000).OrderBy<Account, string>((System.Func<Account, string>) (a => a.AccountNumber.Trim())).Select<Account, APISubAccount>((System.Func<Account, APISubAccount>) (a => new APISubAccount(a))).ToList<APISubAccount>());
  }

  [AuthorizeAPIPremier]
  public ActionResult SubAccountTreeList(long accountID)
  {
    Account acct = Account.Load(accountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    List<APIAccountTree> apiAccountTreeList = new List<APIAccountTree>();
    foreach (AccountIDTreeModel model in Account.LoadByUserAccountIDandAccountID(MonnitSession.CurrentCustomer.AccountID, acct.AccountID))
      apiAccountTreeList.Add(new APIAccountTree(model));
    return this.FormatRequest((object) apiAccountTreeList);
  }

  [AuthorizeAPIPremier]
  public ActionResult AccountParentEdit(long accountID, long parentID)
  {
    Account account1 = Account.Load(accountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(account1.AccountID))
      return this.FormatRequest((object) "Invalid account");
    Account account2 = Account.Load(parentID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(account2.AccountID))
      return this.FormatRequest((object) "Invalid account");
    if (accountID == parentID)
      return this.FormatRequest((object) "Invalid account");
    if (accountID == MonnitSession.CurrentCustomer.AccountID)
      return this.FormatRequest((object) "Invalid account");
    Account.Ancestors(MonnitSession.CurrentCustomer.CustomerID, account1.AccountID);
    foreach (Account ancestor in Account.Ancestors(MonnitSession.CurrentCustomer.CustomerID, account2.AccountID))
    {
      if (ancestor.AccountID == account1.AccountID)
        return this.FormatRequest((object) "Invalid account");
    }
    try
    {
      account1.LogAuditData(eAuditAction.Update, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, accountID, "Moved subaccount under different account");
    }
    catch (Exception ex)
    {
      ex.Log("Audit Log Failed: Account Parent Edit accountID:" + accountID.ToString());
    }
    account1.RetailAccountID = account2.AccountID;
    account1.AccountIDTree = $"{account2.AccountIDTree}{account1.AccountID.ToString()}*";
    account1.Save();
    Account.UpdateAccountTree(accountID);
    return this.FormatRequest((object) new APISubAccount(account1));
  }

  [AuthorizeAPIPremier]
  public ActionResult SetExpirationDate(long? accountID)
  {
    Account account = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(account))
      return this.FormatRequest((object) "Invalid Request: no account found");
    if (!MonnitSession.CustomerCan("Account_Set_Premium"))
      return (ActionResult) this.Content("Not Authorized!");
    DateTime date = DateTime.MinValue;
    if (account.PremiumValidUntil <= DateTime.Now)
      date = DateTime.Now.AddYears(1);
    if (account.PremiumValidUntil > DateTime.Now)
      date = account.PremiumValidUntil.AddYears(1);
    AccountSubscription.UpdatePremiumDate(date, account, MonnitSession.CurrentCustomer, "API by: " + MonnitSession.CurrentCustomer.AccountID.ToStringSafe());
    account.LogAuditData(eAuditAction.Update, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Extended accounts premium status");
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
    return this.FormatRequest((object) new APIAccount(account));
  }

  [AuthorizeAPI]
  public ActionResult UpdateUserName(ChangeUserAPIModel model)
  {
    if (!this.ModelState.IsValid)
      return this.FormatRequest((object) "Username already exists.");
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && MonnitSession.CurrentCustomer.UserName != model.UserName)
      return this.FormatRequest((object) "Not authorized.");
    Customer customer = Customer.LoadFromUsername(model.NewUsername);
    if (customer != null)
      return this.FormatRequest((object) "Unable to change username.");
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CurrentCustomer.CanSeeAccount(Account.Load(customer.AccountID)))
      return this.FormatRequest((object) "Not authorized.");
    MonnitSession.CurrentCustomer.UserName = model.NewUsername;
    MonnitSession.CurrentCustomer.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPI]
  public ActionResult UpdatePassword(ChangePasswordAPIModel model)
  {
    if (!MonnitUtil.IsValidPassword(model.NewPassword, MonnitSession.CurrentTheme))
      this.ModelState.AddModelError("Password", MonnitUtil.PasswordHelperString(MonnitSession.CurrentTheme));
    if (!this.ModelState.IsValid)
      return this.FormatRequest((object) "Password does not meet requirements.");
    MonnitSession.CurrentCustomer.Password = MonnitSession.UseEncryption ? model.NewPassword.Encrypt() : model.NewPassword;
    MonnitSession.CurrentCustomer.PasswordExpired = false;
    MonnitSession.CurrentCustomer.ForceLogoutDate = DateTime.UtcNow;
    MonnitSession.CurrentCustomer.Save();
    return this.FormatRequest((object) "Success");
  }

  public ActionResult RetrieveUsername(string email)
  {
    AccountControllerBase.SendUserNameReminder(email);
    return this.FormatRequest((object) ("Sent email to " + email));
  }

  public ActionResult ResetPassword(string username)
  {
    if (string.IsNullOrWhiteSpace(username))
      return this.FormatRequest((object) "Username is required.");
    AccountControllerBase.SendPasswordReminder(username);
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPI]
  public ActionResult ResellerPasswordReset(long userID, ChangePasswordAPIModel model)
  {
    Customer customer = Customer.Load(userID);
    if (customer == null)
      return this.FormatRequest((object) "Customer does not exist.");
    if (customer.Account.RetailAccountID != MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeAccount(customer.Account.AccountID))
      return this.FormatRequest((object) "Customer does not exist.");
    if (!this.ModelState.IsValid)
      return this.FormatRequest((object) "Password does not meet requirements.");
    customer.Password = MonnitSession.UseEncryption ? model.NewPassword.Encrypt() : model.NewPassword;
    customer.PasswordExpired = false;
    customer.ForceLogoutDate = DateTime.Now;
    customer.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPI]
  public ActionResult SetPremiereDate(long accountID, int year, int month, int day)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CustomerCan("Account_Set_Premium"))
      return this.FormatRequest((object) "Not authorized.");
    Account account = Account.Load(accountID);
    if (account == null || !MonnitSession.IsAuthorizedForAccount(account.AccountID))
      return this.FormatRequest((object) "Failed, Not Authorized");
    account.LogAuditData(eAuditAction.Update, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Updated accounts premium status");
    AccountSubscription.UpdatePremiumDate(new DateTime(year, month, day), account, MonnitSession.CurrentCustomer, "Rest API");
    try
    {
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
    catch
    {
    }
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPI]
  public ActionResult EditAccountInformation(long accountID, FormCollection collection)
  {
    Account account = Account.Load(accountID);
    if (account == null)
      return this.FormatRequest((object) "Not a Valid Account.");
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CurrentCustomer.CanSeeAccount(account))
      return this.FormatRequest((object) "Not a Valid Account.");
    if (!MonnitSession.CustomerCan("Account_Edit"))
      return this.FormatRequest((object) "Not authorized.");
    account.LogAuditData(eAuditAction.Update, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited account information");
    account.AccountNumber = ((IEnumerable<string>) collection.AllKeys).Contains<string>("AccountNumber") ? collection["AccountNumber"] : account.AccountNumber;
    account.CompanyName = ((IEnumerable<string>) collection.AllKeys).Contains<string>("CompanyName") ? collection["CompanyName"] : account.CompanyName;
    account.PrimaryAddress.Address = ((IEnumerable<string>) collection.AllKeys).Contains<string>("Address") ? collection["Address"] : account.PrimaryAddress.Address;
    account.PrimaryAddress.Address2 = ((IEnumerable<string>) collection.AllKeys).Contains<string>("Address2") ? collection["Address2"] : account.PrimaryAddress.Address2;
    account.PrimaryAddress.City = ((IEnumerable<string>) collection.AllKeys).Contains<string>("City") ? collection["City"] : account.PrimaryAddress.City;
    account.PrimaryAddress.State = ((IEnumerable<string>) collection.AllKeys).Contains<string>("State") ? collection["State"] : account.PrimaryAddress.State;
    account.PrimaryAddress.PostalCode = ((IEnumerable<string>) collection.AllKeys).Contains<string>("PostalCode") ? collection["PostalCode"] : account.PrimaryAddress.PostalCode;
    account.PrimaryAddress.Country = ((IEnumerable<string>) collection.AllKeys).Contains<string>("Country") ? collection["Country"] : account.PrimaryAddress.Country;
    account.BusinessTypeID = ((IEnumerable<string>) collection.AllKeys).Contains<string>("BusinessTypeID") ? collection["BusinessTypeID"].ToLong() : account.BusinessTypeID;
    account.IndustryTypeID = ((IEnumerable<string>) collection.AllKeys).Contains<string>("IndustryTypeID") ? collection["IndustryTypeID"].ToLong() : account.IndustryTypeID;
    account.TimeZoneID = ((IEnumerable<string>) collection.AllKeys).Contains<string>("TimeZoneID") ? collection["TimeZoneID"].ToLong() : account.TimeZoneID;
    account.PrimaryContactID = ((IEnumerable<string>) collection.AllKeys).Contains<string>("PrimaryContactID") ? collection["PrimaryContactID"].ToLong() : account.PrimaryContactID;
    account.PrimaryAddress.Save();
    account.Save();
    return this.FormatRequest((object) new APIAccount(account));
  }

  [AuthorizeAPI]
  public ActionResult EditCustomerInformation(long custID)
  {
    NameValueCollection queryString = this.Request.QueryString;
    Customer DBObject = Customer.Load(custID);
    if (DBObject == null)
      return this.FormatRequest((object) "Not a Valid Customer.");
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.Account))
      return this.FormatRequest((object) "Not authorized.");
    if (MonnitSession.CurrentCustomer.CustomerID == DBObject.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Self"))
      return this.FormatRequest((object) "Not authorized.");
    if (MonnitSession.CurrentCustomer.CustomerID != DBObject.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Other"))
      return this.FormatRequest((object) "Not authorized.");
    if (((IEnumerable<string>) queryString.AllKeys).Contains<string>("NotificationEmail") && !Customer.CheckNotificationEmailIsUnique(queryString["NotificationEmail"], DBObject.CustomerID))
      return this.FormatRequest((object) "Email address not available");
    Account account = Account.Load(DBObject.AccountID);
    DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited customer information");
    DBObject.FirstName = ((IEnumerable<string>) queryString.AllKeys).Contains<string>("FirstName") ? queryString["FirstName"] : DBObject.FirstName;
    DBObject.LastName = ((IEnumerable<string>) queryString.AllKeys).Contains<string>("LastName") ? queryString["LastName"] : DBObject.LastName;
    DBObject.NotificationEmail = ((IEnumerable<string>) queryString.AllKeys).Contains<string>("NotificationEmail") ? queryString["NotificationEmail"] : DBObject.NotificationEmail;
    DBObject.NotificationPhone = ((IEnumerable<string>) queryString.AllKeys).Contains<string>("NotificationPhone") ? queryString["NotificationPhone"] : DBObject.NotificationPhone;
    DBObject.SMSCarrierID = ((IEnumerable<string>) queryString.AllKeys).Contains<string>("SMSCarrierID") ? queryString["SMSCarrierID"].ToLong() : DBObject.SMSCarrierID;
    DBObject.Save();
    return this.FormatRequest((object) "Successfully Saved.");
  }

  [AuthorizeAPI]
  public ActionResult EditCustomerPermissions()
  {
    NameValueCollection queryString = this.Request.QueryString;
    if (!MonnitSession.CurrentCustomer.IsAdmin)
      return this.FormatRequest((object) "Not authorized.");
    if (!((IEnumerable<string>) queryString.AllKeys).Contains<string>("custID") || string.IsNullOrWhiteSpace(queryString["custID"]))
      return this.FormatRequest((object) "Not a Valid Customer.");
    Customer customer = Customer.Load(queryString["custID"].ToLong());
    if (customer == null)
      return this.FormatRequest((object) "Not a Valid Customer.");
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CurrentCustomer.CanSeeAccount(customer.Account))
      return this.FormatRequest((object) "Not authorized.");
    if (MonnitSession.CurrentCustomer.CustomerID == customer.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Self"))
      return this.FormatRequest((object) "Not authorized.");
    if (MonnitSession.CurrentCustomer.CustomerID != customer.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Other"))
      return this.FormatRequest((object) "Not authorized.");
    List<CustomerPermissionType> customerPermissionTypeList = CustomerPermissionType.CanSee(MonnitSession.CurrentCustomer);
    foreach (string allKey in queryString.AllKeys)
    {
      if (!(allKey == "custID") && !(allKey.ToLower() == "sensor_advanced_configuration") && !(allKey.ToLower() == "sensor_heartbeat_restriction"))
      {
        bool flag = false;
        if (allKey.Contains("Network_View_Net"))
        {
          CSNet csNet = CSNet.Load(allKey.RemoveNonNumeric().ToLong());
          if (csNet == null || !MonnitSession.CurrentCustomer.CanSeeAccount(csNet.AccountID))
            return this.FormatRequest((object) "Network Not authorized");
          flag = true;
        }
        foreach (CustomerPermissionType customerPermissionType in customerPermissionTypeList)
        {
          if (allKey == customerPermissionType.Name)
            flag = true;
        }
        if (!flag)
          return this.FormatRequest((object) "Not authorized.");
      }
    }
    try
    {
      NameValueCollection collection = new NameValueCollection();
      foreach (string allKey in queryString.AllKeys)
      {
        if (!(allKey.ToLower() == "sensor_advanced_configuration") && !(allKey.ToLower() == "sensor_heartbeat_restriction") && allKey != "custID")
          collection.Add("Permission_" + allKey, this.Request.QueryString[allKey]);
      }
      foreach (CustomerPermission loadPermission in CustomerPermission.LoadPermissions(customer))
      {
        if (loadPermission.Type.NetworkSpecific)
        {
          foreach (CustomerPermission customerPermission in CustomerPermission.LoadAllByCustomerID(customer.CustomerID).Where<CustomerPermission>((System.Func<CustomerPermission, bool>) (net => net.CSNetID != long.MinValue)).ToList<CustomerPermission>())
          {
            if (!((IEnumerable<string>) collection.AllKeys).Contains<string>($"Permission_{loadPermission.Type.Name}_Net_{customerPermission.CSNetID}"))
              collection.Add($"Permission_{loadPermission.Type.Name}_Net_{customerPermission.CSNetID}", loadPermission.Can ? "on" : "off");
          }
        }
        if (!((IEnumerable<string>) collection.AllKeys).Contains<string>($"Permission_{loadPermission.Type.Name}"))
          collection.Add($"Permission_{loadPermission.Type.Name}", loadPermission.Can ? "on" : "off");
      }
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
      CustomerController.CustomerPermissionUpdate(collection, customer);
      return this.FormatRequest((object) "Save successful.");
    }
    catch (Exception ex)
    {
      return this.FormatRequest((object) "Save not successful.");
    }
  }

  [AuthorizeAPI]
  public ActionResult GetCustomerPermissions(long? custID)
  {
    Customer customer = Customer.Load(custID ?? MonnitSession.CurrentCustomer.CustomerID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(customer.Account))
      return this.FormatRequest((object) "Invalid Request: no account found");
    IEnumerable<long> CanEditList = CustomerPermissionType.CanSee(MonnitSession.CurrentCustomer).Select<CustomerPermissionType, long>((System.Func<CustomerPermissionType, long>) (type => type.CustomerPermissionTypeID));
    return this.FormatRequest((object) customer.Permissions.Where<CustomerPermission>((System.Func<CustomerPermission, bool>) (p => CanEditList.Contains<long>(p.CustomerPermissionTypeID))).OrderBy<CustomerPermission, string>((System.Func<CustomerPermission, string>) (m => m.Type.Name)).Select<CustomerPermission, APIPermission>((System.Func<CustomerPermission, APIPermission>) (obj => new APIPermission(obj.Type.Name, obj.Can, obj.CSNetID))).ToList<APIPermission>());
  }

  [AuthorizeAPIPremier]
  public ActionResult CreateAccountUser(
    string userName,
    string firstName,
    string lastName,
    string password,
    string confirmPassword,
    string notificationEmail,
    bool isAdmin,
    string title,
    bool? receivesMaintenanceByEmail,
    bool? receivesMaintenanceByPhone,
    bool? receivesNotificationsBySMS,
    bool? receivesNotificationsByVoice,
    string smsNumber,
    string voiceNumber,
    long? smsProviderID,
    long? accountID)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    if (!MonnitSession.CustomerCan("Support_Advanced"))
    {
      if (!acct.CurrentSubscription.Can(Feature.Find("muliple_users")))
        return this.FormatRequest((object) "Invalid Request: Premiere Account Required for multiple users");
      if (!MonnitSession.CustomerCan("Customer_Create"))
        return this.FormatRequest((object) "Invalid Credentials: You are unauthorized to create another user");
    }
    List<APIValidationError> apiValidationErrorList = new List<APIValidationError>();
    foreach (string key in (IEnumerable<string>) this.ModelState.Keys)
    {
      foreach (ModelError error in (Collection<ModelError>) this.ModelState[key].Errors)
        apiValidationErrorList.Add(new APIValidationError(key, error.ErrorMessage));
    }
    if (!Customer.CheckUsernameIsUnique(userName))
      apiValidationErrorList.Add(new APIValidationError("UserName", "Username not available"));
    if (!Customer.CheckNotificationEmailIsUnique(notificationEmail))
      apiValidationErrorList.Add(new APIValidationError("NotificationEmail", "Email address not available"));
    if (!MonnitUtil.IsValidPassword(password, MonnitSession.CurrentTheme))
      apiValidationErrorList.Add(new APIValidationError("Password", "Password invalid"));
    if (password != confirmPassword)
      apiValidationErrorList.Add(new APIValidationError("Password", "Passwords must match"));
    if (apiValidationErrorList.Count == 0)
    {
      try
      {
        Customer customer = new Customer()
        {
          AccountID = acct.AccountID,
          UserName = userName,
          FirstName = firstName,
          LastName = lastName,
          Salt = MonnitUtil.GenerateSalt(),
          WorkFactor = ConfigData.AppSettings("WorkFactor").ToInt()
        };
        customer.Password2 = MonnitUtil.GenerateHash(password, customer.Salt, customer.WorkFactor);
        customer.NotificationEmail = notificationEmail;
        customer.IsAdmin = isAdmin;
        customer.Title = title;
        customer.SendSensorNotificationToText = receivesNotificationsBySMS.GetValueOrDefault();
        customer.SendSensorNotificationToVoice = receivesNotificationsByVoice.GetValueOrDefault();
        long? nullable = smsProviderID;
        long num = 0;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          smsProviderID = new long?(long.MinValue);
        customer.SMSCarrierID = smsProviderID ?? long.MinValue;
        customer.NotificationPhone = smsNumber;
        customer.NotificationPhone2 = voiceNumber;
        customer.Save();
        customer.ResetPermissions();
        customer.LogAuditData(eAuditAction.Create, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, acct.AccountID, "Created new customer");
        this._ClearCache();
        return this.FormatRequest((object) new APIUser(customer));
      }
      catch (Exception ex)
      {
        ex.Log("APIController.CreateAccountUser");
        apiValidationErrorList.Add(new APIValidationError("", "Unexpected error. if Problem continues create your user directly through the online portal"));
      }
    }
    return this.FormatRequest((object) apiValidationErrorList);
  }

  [AuthorizeAPI]
  [HttpPost]
  public ActionResult AccountUserGet(long userID)
  {
    Customer cust = Customer.Load(userID);
    return cust != null && MonnitSession.CurrentCustomer.CanSeeAccount(cust.AccountID) && !cust.IsDeleted ? this.FormatRequest((object) new APIUser(cust)) : this.FormatRequest((object) "Invalid UserID");
  }

  [AuthorizeAPI]
  public ActionResult AccountUserDelete(long userID)
  {
    Customer customer = Customer.Load(userID);
    if (customer == null || !MonnitSession.CurrentCustomer.CanSeeAccount(customer.AccountID))
      return this.FormatRequest((object) "Invalid UserID");
    if (MonnitSession.CurrentCustomer.CustomerID == customer.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Self"))
      return this.FormatRequest((object) "Not authorized.");
    if (MonnitSession.CurrentCustomer.CustomerID != customer.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Other"))
      return this.FormatRequest((object) "Not authorized.");
    if (customer.Account.PrimaryContactID == userID)
      return this.FormatRequest((object) "User is primary contact. Unable to delete");
    Account account = Account.Load(customer.AccountID);
    foreach (Notification DBObject in Notification.LoadByAccountID(customer.AccountID))
    {
      foreach (NotificationRecipient notificationRecipient in DBObject.NotificationRecipients)
      {
        if (notificationRecipient.CustomerToNotifyID == userID)
        {
          DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Removed customer from notification");
          DBObject.RemoveCustomer(customer);
          DBObject.AddCustomer(customer.Account.PrimaryContact, notificationRecipient.NotificationType);
          break;
        }
      }
    }
    foreach (CustomerAccountLink DBObject in CustomerAccountLink.LoadAllByCustomerID(userID))
    {
      if (DBObject.AccountID == customer.AccountID)
      {
        DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Removed customer from linked accounts");
        DBObject.AccountDeleted = true;
        DBObject.DateAccountDeleted = DateTime.UtcNow;
        DBObject.Save();
      }
    }
    customer.LogAuditData(eAuditAction.Delete, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Deleted customer");
    customer.Delete();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPI]
  public ActionResult AccountUserList(long? accountID)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    List<APIUser> apiUserList = new List<APIUser>();
    List<Customer> customerList = Customer.LoadAllByAccount(acct.AccountID);
    if (customerList.Count < 1)
      return this.FormatRequest((object) "Invalid Request: there are no users for this account");
    foreach (Customer cust in customerList)
      apiUserList.Add(new APIUser(cust));
    return this.FormatRequest((object) apiUserList);
  }

  [AuthorizeAPI]
  public ActionResult AccountUserEdit(long userID)
  {
    NameValueCollection queryString = this.Request.QueryString;
    Customer customer = Customer.Load(userID);
    if (customer == null || !MonnitSession.CurrentCustomer.CanSeeAccount(customer.AccountID))
      return this.FormatRequest((object) "Invalid UserID");
    if (MonnitSession.CurrentCustomer.CustomerID == customer.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Self"))
      return this.FormatRequest((object) "Not authorized.");
    if (MonnitSession.CurrentCustomer.CustomerID != customer.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Other"))
      return this.FormatRequest((object) "Not authorized.");
    if (((IEnumerable<string>) queryString.AllKeys).Contains<string>("NotificationEmail") && !Customer.CheckNotificationEmailIsUnique(queryString["NotificationEmail"], customer.CustomerID))
      return this.FormatRequest((object) "Email address not available");
    Account account = Account.Load(customer.AccountID);
    customer.LogAuditData(eAuditAction.Update, eAuditObject.Customer, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited customer information");
    customer.FirstName = ((IEnumerable<string>) queryString.AllKeys).Contains<string>("FirstName") ? queryString["FirstName"] : customer.FirstName;
    customer.LastName = ((IEnumerable<string>) queryString.AllKeys).Contains<string>("LastName") ? queryString["LastName"] : customer.LastName;
    customer.NotificationEmail = ((IEnumerable<string>) queryString.AllKeys).Contains<string>("NotificationEmail") ? queryString["NotificationEmail"] : customer.NotificationEmail;
    customer.NotificationPhone = ((IEnumerable<string>) queryString.AllKeys).Contains<string>("NotificationPhone") ? queryString["NotificationPhone"] : customer.NotificationPhone;
    customer.NotificationPhone2 = ((IEnumerable<string>) queryString.AllKeys).Contains<string>("NotificationPhone2") ? queryString["NotificationPhone2"] : customer.NotificationPhone2;
    customer.SMSCarrierID = ((IEnumerable<string>) queryString.AllKeys).Contains<string>("SMSCarrierID") ? queryString["SMSCarrierID"].ToLong() : customer.SMSCarrierID;
    customer.SendSensorNotificationToText = ((IEnumerable<string>) queryString.AllKeys).Contains<string>("ReceivesNotificationsBySMS") ? queryString["ReceivesNotificationsBySMS"].ToBool() : customer.SendSensorNotificationToText;
    customer.SendSensorNotificationToVoice = ((IEnumerable<string>) queryString.AllKeys).Contains<string>("ReceivesNotificationsByVoice") ? queryString["ReceivesNotificationsByVoice"].ToBool() : customer.SendSensorNotificationToVoice;
    customer.Title = ((IEnumerable<string>) queryString.AllKeys).Contains<string>("Title") ? queryString["Title"] : customer.Title;
    customer.Save();
    return this.FormatRequest((object) new APIUser(customer));
  }

  [AuthorizeAPI]
  public ActionResult AccountUserScheduleGet(long userID)
  {
    Customer customer = Customer.Load(userID);
    if (customer == null || !MonnitSession.CurrentCustomer.CanSeeAccount(customer.AccountID))
      return this.FormatRequest((object) "Invalid UserID");
    return this.FormatRequest((object) new List<APIUserNotificationSchedule>()
    {
      new APIUserNotificationSchedule(customer.MondaySchedule),
      new APIUserNotificationSchedule(customer.TuesdaySchedule),
      new APIUserNotificationSchedule(customer.WednesdaySchedule),
      new APIUserNotificationSchedule(customer.ThursdaySchedule),
      new APIUserNotificationSchedule(customer.FridaySchedule),
      new APIUserNotificationSchedule(customer.SaturdaySchedule),
      new APIUserNotificationSchedule(customer.SundaySchedule)
    });
  }

  [AuthorizeAPI]
  public ActionResult AccountUserScheduleSet(
    long userID,
    string day,
    string notificationSchedule,
    TimeSpan? firstEnteredTime,
    TimeSpan? secondEnteredTime)
  {
    Customer customer = Customer.Load(userID);
    if (customer == null || !MonnitSession.CurrentCustomer.CanSeeAccount(customer.AccountID))
      return this.FormatRequest((object) "Invalid UserID");
    if (MonnitSession.CurrentCustomer.CustomerID == customer.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Self"))
      return this.FormatRequest((object) "Not authorized.");
    if (MonnitSession.CurrentCustomer.CustomerID != customer.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Other"))
      return this.FormatRequest((object) "Not authorized.");
    eNotificationDaySchedule notificationDaySchedule = eNotificationDaySchedule.All_Day;
    CustomerSchedule customerSchedule1 = new CustomerSchedule();
    Account account = Account.Load(customer.AccountID);
    customerSchedule1.LogAuditData(eAuditAction.Update, eAuditObject.Schedule, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited customer notification schedule");
    if (!string.IsNullOrWhiteSpace(day))
    {
      switch (day.ToLower())
      {
        case "friday":
          customerSchedule1 = customer.FridaySchedule;
          break;
        case "monday":
          customerSchedule1 = customer.MondaySchedule;
          break;
        case "saturday":
          customerSchedule1 = customer.SaturdaySchedule;
          break;
        case "sunday":
          customerSchedule1 = customer.SundaySchedule;
          break;
        case "thursday":
          customerSchedule1 = customer.ThursdaySchedule;
          break;
        case "tuesday":
          customerSchedule1 = customer.TuesdaySchedule;
          break;
        case "wednesday":
          customerSchedule1 = customer.WednesdaySchedule;
          break;
        default:
          return this.FormatRequest((object) "Invalid Request");
      }
    }
    if (!string.IsNullOrWhiteSpace(notificationSchedule))
    {
      switch (notificationSchedule)
      {
        case "Off":
          notificationDaySchedule = eNotificationDaySchedule.Off;
          break;
        case "All_Day":
          notificationDaySchedule = eNotificationDaySchedule.All_Day;
          break;
        case "After":
          notificationDaySchedule = eNotificationDaySchedule.After;
          break;
        case "Before":
          notificationDaySchedule = eNotificationDaySchedule.Before;
          break;
        case "Before_and_After":
          notificationDaySchedule = eNotificationDaySchedule.Before_and_After;
          break;
        case "Between":
          notificationDaySchedule = eNotificationDaySchedule.Between;
          break;
        default:
          return this.FormatRequest((object) "Invalid NotificationSchedule");
      }
    }
    CustomerSchedule customerSchedule2 = customerSchedule1;
    TimeSpan? nullable = firstEnteredTime;
    TimeSpan timeSpan1 = nullable ?? TimeSpan.Zero;
    customerSchedule2.FirstTime = timeSpan1;
    CustomerSchedule customerSchedule3 = customerSchedule1;
    nullable = secondEnteredTime;
    TimeSpan timeSpan2 = nullable ?? TimeSpan.Zero;
    customerSchedule3.SecondTime = timeSpan2;
    customerSchedule1.CustomerDaySchedule = notificationDaySchedule;
    customerSchedule1.Save();
    return this.FormatRequest((object) new APIUserNotificationSchedule(customerSchedule1));
  }

  [AuthorizeAPI]
  [HttpPost]
  public ActionResult AccountUserImageUpload(long userID)
  {
    Customer customer = Customer.Load(userID);
    if (customer != null && MonnitSession.CurrentCustomer.CanSeeAccount(customer.AccountID))
    {
      if (MonnitSession.CurrentCustomer.CustomerID == customer.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Self"))
        return this.FormatRequest((object) "Not authorized.");
      if (MonnitSession.CurrentCustomer.CustomerID != customer.CustomerID && !MonnitSession.CustomerCan("Customer_Edit_Other"))
        return this.FormatRequest((object) "Not authorized.");
      HttpPostedFileBase file = this.Request.Files["ImageFile"];
      if (file != null)
      {
        try
        {
          customer.ImageName = file.FileName;
          customer.Bitmap = MonnitUtil.GetCustomerImageBitmap(file.InputStream);
          customer.Save();
          return this.FormatRequest((object) "Success");
        }
        catch
        {
          return this.FormatRequest((object) "upload Failed");
        }
      }
    }
    return this.FormatRequest((object) "Invalid UserID");
  }

  [AuthorizeAPI]
  public ActionResult CreateNetwork(string name)
  {
    List<CSNet> csNetList = CSNet.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
    if (csNetList.Count > 0 && !MonnitSession.AccountCan("multiple_networks"))
      return this.FormatRequest((object) "Multiple networks only available to Premiere accounts");
    if (csNetList.Count >= ConfigData.AppSettings("MaxNetworkCount", "10").ToInt() && !MonnitSession.IsCurrentCustomerMonnitAdmin)
      return this.FormatRequest((object) "Maximum networks for an account has been reached");
    return this.TryCreateNetwork(name, new DateTime?(), new long?(), out CSNet _) ? this.FormatRequest((object) "Success") : this.FormatRequest((object) "Network creation failed.");
  }

  [AuthorizeAPI]
  public ActionResult CreateNetwork2(string name, DateTime? ExternalAccessUntil, long? accountID)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    List<CSNet> csNetList = CSNet.LoadByAccountID(acct.AccountID);
    if (csNetList.Count > 0 && !acct.CurrentSubscription.Can(Feature.Find("multiple_networks")))
      return this.FormatRequest((object) "Multiple networks only available to Premiere accounts");
    if (csNetList.Count >= ConfigData.AppSettings("MaxNetworkCount", "10").ToInt() && !MonnitSession.IsCurrentCustomerMonnitAdmin)
      return this.FormatRequest((object) "Maximum networks for an account has been reached");
    CSNet network;
    return this.TryCreateNetwork(name, ExternalAccessUntil, new long?(acct.AccountID), out network) ? this.FormatRequest((object) new APINetwork(network)) : this.FormatRequest((object) "Network creation failed.");
  }

  private bool TryCreateNetwork(
    string name,
    DateTime? ExternalAccessUntil,
    long? accountID,
    out CSNet network)
  {
    try
    {
      network = new CSNet();
      network.AccountID = accountID ?? MonnitSession.CurrentCustomer.AccountID;
      network.Name = !string.IsNullOrEmpty(name) ? name : MonnitSession.CurrentCustomer.Account.CompanyName;
      network.ExternalAccessUntil = ExternalAccessUntil ?? DateTime.MinValue;
      network.SendNotifications = true;
      network.Save();
      Account account1 = Account.Load(network.AccountID);
      network.LogAuditData(eAuditAction.Create, eAuditObject.Network, MonnitSession.CurrentCustomer.CustomerID, account1.AccountID, "Created new network");
      long permissionTypeId = CustomerPermissionType.Find("Network_View").CustomerPermissionTypeID;
      Account account2 = Account.Load(network.AccountID);
      foreach (Customer customer in Customer.LoadAllByAccount(network.AccountID))
      {
        if (customer.IsAdmin || customer.CustomerID == account2.PrimaryContactID)
          new CustomerPermission()
          {
            CSNetID = network.CSNetID,
            CustomerID = customer.CustomerID,
            CustomerPermissionTypeID = permissionTypeId,
            Can = true
          }.Save();
      }
      this._ClearCache();
      return true;
    }
    catch (Exception ex)
    {
      ex.Log("APIController.CreateNetwork ");
    }
    network = (CSNet) null;
    return false;
  }

  [AuthorizeAPI]
  public ActionResult RemoveNetwork(long networkID)
  {
    CSNet DBObject = CSNet.Load(networkID);
    if (DBObject == null || !MonnitSession.IsAuthorizedForAccount(DBObject.AccountID))
      return this.FormatRequest((object) "Network Not Found");
    List<APIValidationError> apiValidationErrorList = new List<APIValidationError>();
    foreach (Sensor sensor in Sensor.LoadByCsNetID(networkID))
    {
      try
      {
        sensor.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
        sensor.AccountID = long.MinValue;
        sensor.Save();
        Gateway gateway = Gateway.LoadBySensorID(sensor.SensorID);
        if (gateway != null)
        {
          gateway.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
          gateway.ResetToDefault(false);
          gateway.Save();
        }
      }
      catch
      {
      }
    }
    if (Sensor.LoadByCsNetID(networkID).Count > 0)
      apiValidationErrorList.Add(new APIValidationError("Sensors", "Unable to remove all sensors from network."));
    foreach (Gateway gateway in Gateway.LoadByCSNetID(networkID))
    {
      try
      {
        gateway.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
        gateway.ResetToDefault(false);
        gateway.MarkClean(false);
        gateway.Save();
      }
      catch
      {
      }
    }
    if (Gateway.LoadByCSNetID(networkID).Count > 0)
      apiValidationErrorList.Add(new APIValidationError("Gateways", "Unable to remove all gateways from network."));
    if (apiValidationErrorList.Count != 0)
      return this.FormatRequest((object) apiValidationErrorList);
    try
    {
      foreach (Customer customer in Customer.LoadAllByAccount(DBObject.AccountID))
      {
        foreach (CustomerPermission permission in customer.Permissions)
        {
          if (permission.CSNetID == DBObject.CSNetID)
            permission.Delete();
        }
      }
      Account account = Account.Load(DBObject.AccountID);
      DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Network, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Removed network");
      DBObject.Delete();
      return this.FormatRequest((object) "Success");
    }
    catch (Exception ex)
    {
      ex.Log($"APIController.RemoveNetwork(networkID: {networkID})");
      return this.FormatRequest((object) "Unable to remove network");
    }
  }

  [AuthorizeAPI]
  public ActionResult NetworkList(long? accountID)
  {
    long AccID = accountID ?? MonnitSession.CurrentCustomer.AccountID;
    return !MonnitSession.CurrentCustomer.CanSeeAccount(AccID) ? this.FormatRequest((object) "Invalid Account") : this.FormatRequest((object) CSNet.LoadByAccountID(AccID).Where<CSNet>((System.Func<CSNet, bool>) (n => AccID != MonnitSession.CurrentCustomer.AccountID || MonnitSession.CustomerCan("Network_View", n.CSNetID))).OrderBy<CSNet, string>((System.Func<CSNet, string>) (n => n.Name.Trim())).Select<CSNet, APINetwork>((System.Func<CSNet, APINetwork>) (n => new APINetwork(n))).ToList<APINetwork>());
  }

  [AuthorizeAPI]
  public ActionResult GatewayResetDefaults(long? gatewayID)
  {
    try
    {
      Gateway DBObject = Gateway.Load(gatewayID.ToLong());
      if (DBObject == null || DBObject.IsDeleted)
        return this.FormatRequest((object) "Try a differnet gatewayId");
      CSNet csNet = CSNet.Load(DBObject.CSNetID);
      if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index"
        });
      Account account = Account.Load(csNet.AccountID);
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Reset gateway to default");
      DBObject.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
      DBObject.ResetToDefault(false);
      DBObject.Save();
      return this.FormatRequest((object) "Success");
    }
    catch
    {
    }
    return this.FormatRequest((object) "Unable to reset gateway defaults.");
  }

  [AuthorizeAPI]
  public ActionResult GatewayReform(long? gatewayID)
  {
    try
    {
      Gateway DBObject = Gateway.Load(gatewayID.ToLong());
      if (DBObject == null || DBObject.IsDeleted)
        return this.FormatRequest((object) "Try a differnet gatewayId");
      CSNet csNet = CSNet.Load(DBObject.CSNetID);
      if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index"
        });
      Account account = Account.Load(csNet.AccountID);
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Reformed gateway");
      DBObject.SendResetNetworkRequest = true;
      DBObject.Save();
      return this.FormatRequest((object) "Success");
    }
    catch
    {
    }
    return this.FormatRequest((object) "Unable to reform Gateway.");
  }

  public ActionResult GatewaySetHost(
    long gatewayID,
    string checkDigit,
    string hostAddress,
    int port,
    bool? lockDirty)
  {
    if (!MonnitUtil.ValidateCheckDigit(gatewayID, checkDigit))
      return this.FormatRequest((object) "Check digit did not match.");
    if (string.IsNullOrEmpty(hostAddress) || port < 0)
      return this.FormatRequest((object) "hostAddress and port parameters required.");
    Gateway DBObject = Gateway.Load(gatewayID);
    if (DBObject == null || DBObject != null && DBObject.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    int num = DBObject.IsUnlocked ? 1 : 0;
    if (!DBObject.IsUnlocked)
      return this.FormatRequest((object) "Gateway Locked, Please unlock gateway first.");
    if (csNet != null && !csNet.HoldingOnlyNetwork && DBObject.CSNetID != ConfigData.AppSettings("DefaultCSNetID").ToLong())
      return this.FormatRequest((object) "Unable to set host: Gateway belongs to an account");
    Account account = Account.Load(csNet.AccountID);
    if (account != null)
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, account.PrimaryContactID, account.AccountID, "Edited gateway host address and port with checkdigit");
    DBObject.ServerHostAddress = hostAddress;
    DBObject.ServerHostAddress2 = hostAddress;
    DBObject.Port = port;
    DBObject.Port2 = port;
    DBObject.isEnterpriseHost = lockDirty ?? true;
    DBObject.Save();
    return this.FormatRequest((object) "New host set after pending command.");
  }

  public ActionResult GatewayClearHost(long gatewayID, string checkDigit)
  {
    if (!MonnitUtil.ValidateCheckDigit(gatewayID, checkDigit))
      return this.FormatRequest((object) "Check digit did not match.");
    Gateway DBObject = Gateway.Load(gatewayID);
    if (DBObject == null || DBObject != null && DBObject.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    int num = DBObject.IsUnlocked ? 1 : 0;
    if (!DBObject.IsUnlocked)
      return this.FormatRequest((object) "Gateway Locked, Please unlock gateway first.");
    if (csNet != null && !csNet.HoldingOnlyNetwork && DBObject.CSNetID != ConfigData.AppSettings("DefaultCSNetID").ToLong())
      return this.FormatRequest((object) "Unable to set host: Gateway belongs to an account");
    Account account = Account.Load(csNet.AccountID);
    if (account != null)
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, account.PrimaryContactID, account.AccountID, "Edited gateway host address and port with checkdigit");
    GatewayType gatewayType = DBObject.GatewayType;
    DBObject.ServerHostAddress = gatewayType.DefaultServerHostAddress;
    DBObject.ServerHostAddress2 = gatewayType.DefaultServerHostAddress2;
    DBObject.Port = gatewayType.DefaultPort;
    DBObject.Port2 = gatewayType.DefaultPort2;
    DBObject.isEnterpriseHost = false;
    DBObject.Save();
    return this.FormatRequest((object) "Host reset to default after pending command.");
  }

  [AuthorizeAPI]
  public ActionResult GatewayPoint(long gatewayID, string HostAddress, int port)
  {
    Gateway DBObject = Gateway.Load(gatewayID);
    if (DBObject == null || DBObject != null && DBObject.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return this.FormatRequest((object) "Invalid GatewayID");
    if (!DBObject.IsUnlocked)
      return this.FormatRequest((object) "Gateway Locked, Please unlock gateway first.");
    Account account = Account.Load(csNet.AccountID);
    if (account != null)
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway host address and port");
    DBObject.ServerHostAddress = HostAddress;
    DBObject.ServerHostAddress2 = HostAddress;
    DBObject.Port = port;
    DBObject.Port2 = port;
    DBObject.isEnterpriseHost = true;
    DBObject.Save();
    return this.FormatRequest((object) "Set pending command.");
  }

  [AuthorizeAPIPremier]
  public ActionResult GatewaySetIP(
    long gatewayID,
    string ipAddress,
    string networkMask,
    string defaultGateway,
    string dnsServer)
  {
    Gateway DBObject = Gateway.Load(gatewayID);
    if (DBObject == null || DBObject != null && DBObject.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return this.FormatRequest((object) "Unauthorized");
    Account account = Account.Load(csNet.AccountID);
    if (account != null)
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway host address and port");
    if (!ExtensionMethods.ValidateIP(ipAddress))
      return this.FormatRequest((object) "Invalid IP Address");
    if (!ExtensionMethods.ValidateIP(networkMask))
      return this.FormatRequest((object) "Invalid Subnet Mask");
    if (!ExtensionMethods.ValidateIP(defaultGateway))
      return this.FormatRequest((object) "Invalid Default Gateway");
    if (!ExtensionMethods.ValidateIP(dnsServer))
      return this.FormatRequest((object) "Invalid DNS Server");
    DBObject.GatewayIP = ipAddress;
    DBObject.GatewaySubnet = networkMask;
    DBObject.DefaultRouterIP = defaultGateway;
    DBObject.GatewayDNS = dnsServer;
    DBObject.Save();
    return this.FormatRequest((object) "Set pending command.");
  }

  [AuthorizeAPI]
  public ActionResult GatewayCellNetworkConfig(
    long gatewayID,
    double? PollInterval,
    double? GPSReportInterval,
    string CellAPNName = null,
    string Username = null,
    string Password = null,
    string GatewayDNS = null,
    string SecondaryDNS = null)
  {
    Gateway DBObject = Gateway.Load(gatewayID);
    if (DBObject == null || DBObject != null && DBObject.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return this.FormatRequest((object) "Invalid GatewayID");
    Account account = Account.Load(csNet.AccountID);
    DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway cell network settings");
    if (CellAPNName != null)
      DBObject.CellAPNName = CellAPNName.ToString();
    if (Username != null)
      DBObject.Username = Username.ToString();
    if (Password != null)
      DBObject.Password = Password.ToString();
    if (GatewayDNS != null)
      DBObject.GatewayDNS = GatewayDNS.ToString();
    if (SecondaryDNS != null)
      DBObject.SecondaryDNS = SecondaryDNS.ToString();
    if (GPSReportInterval.HasValue)
      DBObject.GPSReportInterval = GPSReportInterval ?? double.MinValue;
    DBObject.PollInterval = PollInterval ?? DBObject.PollInterval;
    DBObject.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPI]
  public ActionResult GatewayAutoConfig(long gatewayID, int? AutoConfigTime)
  {
    Gateway DBObject = Gateway.Load(gatewayID);
    if (DBObject == null || DBObject != null && DBObject.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return this.FormatRequest((object) "Invalid GatewayID");
    Account account = Account.Load(csNet.AccountID);
    DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Set gatway auto config time");
    DBObject.AutoConfigActionCommandTime = AutoConfigTime ?? DBObject.AutoConfigActionCommandTime;
    DBObject.hasActionControlCommand = true;
    DBObject.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPI]
  public ActionResult GatewaySetResetInterval(
    long gatewayID,
    int? ResetInterval,
    string GatewayPowerMode)
  {
    Gateway DBObject = Gateway.Load(gatewayID);
    if (DBObject == null || DBObject != null && DBObject.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return this.FormatRequest((object) "Invalid GatewayID");
    Account account = Account.Load(csNet.AccountID);
    DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Set gateway configs");
    DBObject.ResetInterval = ResetInterval ?? DBObject.ResetInterval;
    if (!string.IsNullOrEmpty(GatewayPowerMode))
    {
      switch (GatewayPowerMode.ToLower())
      {
        case "0":
        case "standard":
          DBObject.GatewayPowerMode = eGatewayPowerMode.Standard;
          break;
        case "1":
        case "force_low_power":
        case "low":
          DBObject.GatewayPowerMode = eGatewayPowerMode.Force_Low_Power;
          break;
        case "2":
        case "force_high_power":
        case "high":
          DBObject.GatewayPowerMode = eGatewayPowerMode.Force_High_Power;
          break;
        default:
          return this.FormatRequest((object) "Invalid GatewayPowerMode");
      }
    }
    DBObject.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPI]
  public ActionResult GatewayStopAutoConfig(long gatewayID)
  {
    Gateway DBObject = Gateway.Load(gatewayID);
    if (DBObject == null || DBObject != null && DBObject.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    Account account = Account.Load(csNet.AccountID);
    DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Stoped gateway auto config");
    DBObject.AutoConfigActionCommandTime = 0;
    DBObject.hasActionControlCommand = true;
    DBObject.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPI]
  public ActionResult RemoveGateway(long? gatewayID)
  {
    try
    {
      Gateway gateway = Gateway.Load(gatewayID.ToLong());
      if (gateway == null || gateway.IsDeleted)
        return this.FormatRequest((object) "try a different gatewayId");
      CSNet csNet = CSNet.Load(gateway.CSNetID);
      if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
        return this.FormatRequest((object) "Not Authorized");
      Account account = Account.Load(csNet.AccountID);
      gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Removed gateway");
      NetworkAudit.LogNetworkChange(MonnitSession.CurrentCustomer.CustomerID, gateway, new long?(csNet.AccountID));
      foreach (Notification notification in GatewayNotification.LoadByGatewayID(gateway.GatewayID))
        notification.RemoveGateway(gateway);
      gateway.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
      foreach (BaseDBObject baseDbObject in ExternalDataSubscription.LoadByGatewayID(gateway.GatewayID).ToArray())
        baseDbObject.Delete();
      if (gateway.SensorID > 0L)
      {
        Sensor sensor = Sensor.Load(gateway.SensorID);
        sensor.LogAuditData(eAuditAction.Delete, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Removed sensor");
        sensor.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
        sensor.LastCommunicationDate = DateTime.MinValue;
        sensor.LastDataMessageGUID = Guid.Empty;
        sensor.Save();
        sensor.ResetLastCommunicationDate();
        foreach (Notification notification in Notification.LoadBySensorID(sensor.SensorID))
          notification.RemoveSensor(sensor);
      }
      gateway.ResetToDefault(false);
      gateway.Save();
      return this.FormatRequest((object) "Success");
    }
    catch
    {
    }
    return this.FormatRequest((object) "Unable to remove sensor.");
  }

  [AuthorizeAPI]
  public ActionResult AssignGateway(long networkID, long gatewayID, string checkDigit)
  {
    if (!MonnitUtil.ValidateCheckDigit(gatewayID, checkDigit))
      return this.FormatRequest((object) "Check digit did not match.");
    try
    {
      Gateway gateway = Gateway.Load(gatewayID);
      if (gateway != null && gateway.IsDeleted)
        return this.FormatRequest((object) $"GatewayID: {gatewayID.ToString()} could not be found");
      CSNet csNet1 = CSNet.Load(networkID);
      if (csNet1 == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(csNet1.CSNetID))
        return this.FormatRequest((object) "Network Not Found");
      if (gateway == null)
      {
        gateway = XDocument.Load(string.Format("{2}/xml/LookUpGateway?GatewayID={0}&checkDigit={1}", (object) gatewayID, (object) checkDigit, (object) ConfigData.FindValue("LookUpHost"))).Descendants((XName) "APILookUpGateway").Select<XElement, Gateway>((System.Func<XElement, Gateway>) (g => new Gateway()
        {
          GatewayID = gatewayID,
          Name = g.Attribute((XName) "GatewayName").Value,
          RadioBand = g.Attribute((XName) "RadioBand").Value,
          APNFirmwareVersion = g.Attribute((XName) "APNFirmwareVersion").Value,
          GatewayFirmwareVersion = g.Attribute((XName) "GatewayFirmwareVersion").Value,
          GatewayTypeID = g.Attribute((XName) "GatewayTypeID").Value.ToLong(),
          MacAddress = g.Attribute((XName) "MacAddress").Value,
          CSNetID = networkID,
          SensorID = g.Attribute((XName) "SensorID").Value.ToLong(),
          GenerationType = g.Attribute((XName) "GenerationType").Value,
          SKU = g.Attribute((XName) "SKU").Value
        })).FirstOrDefault<Gateway>();
        if (gateway == null)
          return this.FormatRequest((object) $"GatewayID: {gatewayID} could not be found.");
        gateway.CSNetID = csNet1.CSNetID;
        gateway.ForceInsert();
      }
      if (gateway.CSNetID > 0L && !MonnitSession.CurrentCustomer.CanSeeAccount(csNet1.AccountID))
        return this.FormatRequest((object) $"GatewayID: {gatewayID} could not be transferred to new network.");
      foreach (BaseDBObject baseDbObject in ExternalDataSubscription.LoadByGatewayID(gateway.GatewayID).ToArray())
        baseDbObject.Delete();
      CSNet csNet2 = CSNet.Load(gateway.CSNetID);
      if (csNet1 != null && gateway != null)
      {
        if (csNet2 != null && !csNet2.HoldingOnlyNetwork && gateway.CSNetID != ConfigData.AppSettings("DefaultCSNetID").ToLong() && !MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID))
          return this.FormatRequest((object) $"GatewayID: {gatewayID} not authorized to  transfer.");
        Account account = Account.Load(csNet1.AccountID);
        gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Assigned gateway to network");
        if (gateway.SensorID > 0L)
        {
          Sensor sensor = Sensor.Load(gateway.SensorID);
          eProgramLevel o = MonnitSession.ProgramLevel();
          if (ThemeController.SensorCount() > o.ToInt())
            return this.FormatRequest((object) $"Only {o.ToString()} sensors allowed for this installation.");
          sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Assigned sensor to network");
          sensor.AccountID = csNet1.AccountID;
          sensor.CSNetID = networkID;
          sensor.IsActive = true;
          sensor.IsNewToNetwork = true;
          if (csNet1.AccountID != csNet2.CSNetID)
          {
            sensor.ResetLastCommunicationDate();
            sensor.LastDataMessageGUID = Guid.Empty;
            sensor.StartDate = DateTime.UtcNow;
          }
          TimedCache.RemoveObject("SensorCount");
          sensor.Save();
          sensor.ResetLastCommunicationDate();
          if (csNet2.CSNetID == long.MinValue || csNet1.AccountID != csNet2.AccountID)
          {
            foreach (Notification notification in Notification.LoadBySensorID(sensor.SensorID))
              notification.RemoveSensor(sensor);
          }
        }
        gateway.CSNetID = networkID;
        gateway.LastCommunicationDate = DateTime.MinValue;
        gateway.StartDate = DateTime.UtcNow;
        gateway.Save();
        if (csNet2.CSNetID == long.MinValue || csNet1.AccountID != csNet2.AccountID)
        {
          foreach (Notification notification in GatewayNotification.LoadByGatewayID(gateway.GatewayID))
            notification.RemoveGateway(gateway);
        }
        return this.FormatRequest((object) "Success");
      }
    }
    catch (Exception ex)
    {
      ex.Log("APIController.AssignGateway");
    }
    return this.FormatRequest((object) $"GatewayID: {gatewayID} could not be transfered to new network.");
  }

  [AuthorizeAPIPremier]
  public ActionResult GatewaySetName(long gatewayID, string gatewayName)
  {
    Gateway DBObject = Gateway.Load(gatewayID);
    if (DBObject == null || DBObject.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (csNet == null || !MonnitSession.CurrentCustomer.CanSeeAccount(csNet.AccountID))
      return this.FormatRequest((object) "Unauthorized");
    if (csNet.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeNetwork(csNet.CSNetID))
      return this.FormatRequest((object) "Unathorized");
    Account account = Account.Load(csNet.AccountID);
    DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway name");
    DBObject.Name = gatewayName;
    DBObject.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult GatewaySetHeartbeat(long gatewayID, double reportInterval)
  {
    Gateway DBObject = Gateway.Load(gatewayID.ToLong());
    if (DBObject == null || DBObject.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return this.FormatRequest((object) "Invalid GatewayID");
    Account account = Account.Load(csNet.AccountID);
    DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Changed heartbeat on gateway ");
    GatewayType gatewayType = GatewayType.Load(DBObject.GatewayTypeID);
    if (gatewayType == null || !gatewayType.SupportsHeartbeat)
      return this.FormatRequest((object) "Invalid Gateway Type");
    if (reportInterval < 0.0)
      return this.FormatRequest((object) "Invalid reportInterval: Must be greater than or equal to 0 minutes.");
    if (reportInterval > 720.0)
      return this.FormatRequest((object) "Invalid reportInterval: Must be less than or equal to 720 minutes.");
    DBObject.ReportInterval = reportInterval;
    DBObject.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult GatewaySetSNMP(
    long gatewayID,
    bool enableSNMP,
    string ipStart,
    string ipEnd,
    int port,
    string communityString,
    bool trapEnabled,
    string ipTrap,
    int portTrap,
    bool trapAuthFail,
    bool trapNewData,
    bool trapAware)
  {
    Gateway DBObject = Gateway.Load(gatewayID.ToLong());
    if (DBObject == null || DBObject.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return this.FormatRequest((object) "Invalid GatewayID");
    Account account = Account.Load(csNet.AccountID);
    DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Changed SNMP on gateway ");
    if (DBObject.GatewayTypeID != 33L)
      return this.FormatRequest((object) "Invalid Gateway Type");
    if (enableSNMP)
    {
      if (!ipStart.IsIPAddress())
        return this.FormatRequest((object) "Invalid ipStart: Must be valid ip address.");
      if (!ipEnd.IsIPAddress())
        return this.FormatRequest((object) "Invalid ipStart: Must be valid ip address.");
      if (port > (int) ushort.MaxValue || port < 1)
        port = 161;
      if (trapEnabled && !ipTrap.IsIPAddress())
        return this.FormatRequest((object) "Invalid ipStart: Must be valid ip address.");
      if (portTrap > (int) ushort.MaxValue || portTrap < 1)
        portTrap = 162;
      DBObject.SNMPInterfaceAddress1 = ipStart;
      DBObject.SNMPInterfaceAddress3 = ipEnd;
      DBObject.SNMPInterfacePort1 = port;
      DBObject.SNMPCommunityString = communityString;
      DBObject.SNMPTrap1Active = trapEnabled;
      DBObject.SNMPInterfaceAddress2 = ipTrap;
      DBObject.SNMPTrapPort1 = portTrap;
      DBObject.SNMPTrap2Active = trapAuthFail;
      DBObject.SNMPTrap3Active = trapNewData;
      DBObject.SNMPTrap4Active = trapAware;
    }
    DBObject.SNMPInterface1Active = enableSNMP;
    DBObject.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult GatewaySetModbus(
    long gatewayID,
    bool enableModbus,
    int tcpTimeout,
    int port)
  {
    Gateway DBObject = Gateway.Load(gatewayID.ToLong());
    if (DBObject == null || DBObject.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return this.FormatRequest((object) "Invalid GatewayID");
    Account account = Account.Load(csNet.AccountID);
    DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Changed Modbus Settings on gateway ");
    if (DBObject.GatewayTypeID != 33L)
      return this.FormatRequest((object) "Invalid Gateway Type");
    if (enableModbus)
    {
      if (tcpTimeout > 700 || tcpTimeout < 1)
        tcpTimeout = 5;
      if (port > (int) ushort.MaxValue || port < 1)
        port = 502;
      DBObject.ModbusInterfaceTimeout = (double) tcpTimeout;
      DBObject.ModbusInterfacePort = port;
    }
    DBObject.ModbusInterfaceActive = enableModbus;
    DBObject.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult GatewaySetSNTP(
    long gatewayID,
    bool enableSNTP,
    string serverIP,
    int interval)
  {
    Gateway DBObject = Gateway.Load(gatewayID.ToLong());
    if (DBObject == null || DBObject.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return this.FormatRequest((object) "Invalid GatewayID");
    Account account = Account.Load(csNet.AccountID);
    DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Changed SNTP Settings on gateway ");
    if (DBObject.GatewayTypeID != 33L)
      return this.FormatRequest((object) "Invalid Gateway Type");
    if (enableSNTP)
    {
      if (!serverIP.IsIPAddress())
        return this.FormatRequest((object) "Invalid serverIP: Must be valid ip address.");
      if (interval > 700 || interval < 1)
        interval = 10;
      DBObject.NTPServerIP = serverIP;
      DBObject.NTPMinSampleRate = (double) interval;
    }
    DBObject.NTPInterfaceActive = enableSNTP;
    DBObject.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult GatewaySetHTTP(
    long gatewayID,
    bool enableHTTP,
    string serverIP,
    double timeout)
  {
    Gateway DBObject = Gateway.Load(gatewayID.ToLong());
    if (DBObject == null || DBObject.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return this.FormatRequest((object) "Invalid GatewayID");
    Account account = Account.Load(csNet.AccountID);
    DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Changed HTTP Settings on gateway ");
    if (DBObject.GatewayTypeID != 33L)
      return this.FormatRequest((object) "Invalid Gateway Type");
    if (enableHTTP)
    {
      if (timeout > 1092.25)
        timeout = 1092.25;
      if (timeout < 0.0)
        timeout = 0.0;
      DBObject.HTTPServiceTimeout = timeout;
    }
    DBObject.HTTPInterfaceActive = enableHTTP;
    DBObject.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult GatewayGetExtendedForFullfillment(long gatewayID)
  {
    return this.GatewayGetExtendedForFulfillment(gatewayID);
  }

  [AuthorizeAPIPremier]
  public ActionResult GatewayGetExtendedForFulfillment(long gatewayID)
  {
    Gateway gateway = Gateway.Load(gatewayID);
    return gateway != null ? this.FormatRequest((object) new APIGatewayGetExtendedForFulfillment(gateway)) : this.FormatRequest((object) "Invalid GatewayID");
  }

  [AuthorizeAPIPremier]
  public ActionResult GetGatewayByMac(string mac)
  {
    return new Gateway() != null ? this.FormatRequest((object) new APIGatewayGetExtendedForFulfillment(Gateway.LoadByMAC(mac))) : this.FormatRequest((object) "Invalid MacAddress");
  }

  [AuthorizeAPIPremier]
  public ActionResult GatewayGet(long gatewayID)
  {
    Gateway g = Gateway.Load(gatewayID);
    if (g == null || g.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet csNet = CSNet.Load(g.CSNetID);
    if (csNet == null || !MonnitSession.CurrentCustomer.CanSeeAccount(csNet.AccountID))
      return this.FormatRequest((object) "Gateway not found");
    return csNet.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeNetwork(csNet.CSNetID) && !MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Network_View"), g.CSNetID) ? this.FormatRequest((object) "Unathorized") : this.FormatRequest((object) new APIGateway(g));
  }

  [AuthorizeAPIPremier]
  public ActionResult GatewayGetServer(long gatewayID)
  {
    Gateway g = Gateway.Load(gatewayID);
    if (g == null || g.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet csNet = CSNet.Load(g.CSNetID);
    if (csNet == null || !MonnitSession.CurrentCustomer.CanSeeAccount(csNet.AccountID))
      return this.FormatRequest((object) "Unauthorized");
    return csNet.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeNetwork(csNet.CSNetID) && !MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Network_View"), g.CSNetID) ? this.FormatRequest((object) "Unathorized") : this.FormatRequest((object) new APIGatewayServer(g));
  }

  [AuthorizeAPIPremier]
  public ActionResult GatewayGetForFullfillment(long gatewayID)
  {
    Gateway gateway = Gateway.Load(gatewayID);
    return gateway != null ? this.FormatRequest((object) new APILookUpGateway(gateway)) : this.FormatRequest((object) "Invalid GatewayID");
  }

  [AuthorizeAPIPremier]
  public ActionResult GatewayList(long? networkID, string name, long? accountID)
  {
    long CSNetID = networkID ?? long.MinValue;
    string Name = name.ToStringSafe();
    long AccID = accountID ?? MonnitSession.CurrentCustomer.AccountID;
    return !MonnitSession.CurrentCustomer.CanSeeAccount(AccID) ? this.FormatRequest((object) "Invalid Account") : this.FormatRequest((object) Gateway.LoadByAccountID(AccID).Where<Gateway>((System.Func<Gateway, bool>) (g =>
    {
      if (g.CSNetID != CSNetID && CSNetID != long.MinValue || g.CSNetID == long.MinValue || AccID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Network_View"), g.CSNetID))
        return false;
      return g.Name.ToLower().Contains(Name.ToLower()) || Name == "";
    })).OrderBy<Gateway, string>((System.Func<Gateway, string>) (g => g.Name.Trim())).Select<Gateway, APIGateway>((System.Func<Gateway, APIGateway>) (g => new APIGateway(g))).ToList<APIGateway>());
  }

  public ActionResult UnlockGateway(long gatewayID, string unlockCode, string machineName)
  {
    DateTime utcNow = DateTime.UtcNow;
    string uri = "";
    string str1 = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str1) && bool.Parse(str1);
    string str2 = "Unable to send firmware update. Please contact support to unlock your gateway.";
    try
    {
      Gateway DBObject = Gateway.Load(gatewayID);
      CSNet csNet = CSNet.Load(DBObject.CSNetID);
      if (DBObject != null && !DBObject.IsDeleted)
      {
        string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{unlockCode}|{machineName}"));
        uri = string.Format(ConfigData.FindValue("MEA_API_Location") + "xml/RetreiveActivationKey?key={0}&advancedFailResponse={1}", (object) base64String, (object) true);
        XDocument xdocument = XDocument.Load(uri);
        str2 = xdocument.Descendants((XName) "Result").Single<XElement>().Value;
        if (flag)
        {
          TimeSpan timeSpan = DateTime.UtcNow - utcNow;
          new MeaApiLog()
          {
            MethodName = "MEA-UnlockGateway",
            MachineName = Environment.MachineName,
            RequestBody = uri,
            ResponseBody = xdocument.ToString(),
            CreateDate = utcNow,
            SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
            IsException = false
          }.Save();
        }
        if (!str2.Contains("Failed"))
        {
          Account account = Account.Load(csNet.AccountID);
          DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Unlocked gatway");
          DBObject.IsUnlocked = true;
          DBObject.SendUnlockRequest = true;
          DBObject.Save();
          return this.FormatRequest((object) "Success");
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("APIController.UnlockGateway ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-UnlockGateway",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return this.FormatRequest((object) str2);
  }

  [AuthorizeAPIPremier]
  public ActionResult AdminUnlockGateway(long gatewayID)
  {
    Gateway DBObject = Gateway.Load(gatewayID);
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.CurrentCustomer.IsAdmin)
      return this.FormatRequest((object) "Unauthorized");
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(csNet.AccountID))
      return this.FormatRequest((object) "Unauthorized");
    if (DBObject == null || DBObject.IsDeleted)
      return this.FormatRequest((object) "Could NOT unlock Gateway");
    Account account = Account.Load(csNet.AccountID);
    DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Admin unlocked gateway");
    DBObject.IsUnlocked = true;
    DBObject.SendUnlockRequest = true;
    DBObject.Save();
    return this.FormatRequest((object) "Success");
  }

  public ActionResult UnlockGatewayGPS(long gatewayID, string unlockCode, string machineName)
  {
    DateTime utcNow = DateTime.UtcNow;
    string uri = "";
    string str1 = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str1) && bool.Parse(str1);
    try
    {
      Gateway DBObject = Gateway.Load(gatewayID);
      CSNet csNet = CSNet.Load(DBObject.CSNetID);
      if (DBObject != null && !DBObject.IsDeleted)
      {
        string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{unlockCode}|{machineName}"));
        uri = string.Format(ConfigData.FindValue("MEA_API_Location") + "xml/RetrieveActivationKey?key={0}", (object) base64String);
        XDocument xdocument = XDocument.Load(uri);
        string str2 = xdocument.Descendants((XName) "Result").Single<XElement>().Value;
        if (flag)
        {
          TimeSpan timeSpan = DateTime.UtcNow - utcNow;
          new MeaApiLog()
          {
            MethodName = "MEA-UnlockGatewayGPS",
            MachineName = Environment.MachineName,
            RequestBody = uri,
            ResponseBody = xdocument.ToString(),
            CreateDate = utcNow,
            SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
            IsException = false
          }.Save();
        }
        if (!str2.Equals("Activation Failed"))
        {
          Account account = Account.Load(csNet.AccountID);
          DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Unlocked Gateway GPS");
          DBObject.SendGPSUnlockRequest = true;
          DBObject.IsGPSUnlocked = true;
          DBObject.Save();
          return this.FormatRequest((object) "Success");
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log($"APIController.UnlockGatewayGPS[ID: {gatewayID.ToString()}] ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-UnlockGatewayGPS",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return this.FormatRequest((object) "Please contact support to unlock your gateway's gps.");
  }

  [AuthorizeAPIPremier]
  public ActionResult GatewayUpdateFirmware(long gatewayID)
  {
    Gateway DBObject = Gateway.Load(gatewayID);
    if (DBObject == null || DBObject.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (csNet == null || !MonnitSession.CurrentCustomer.CanSeeAccount(csNet.AccountID))
      return this.FormatRequest((object) "Unauthorized");
    if (csNet.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeNetwork(csNet.CSNetID))
      return this.FormatRequest((object) "Unathorized");
    Account account = Account.Load(csNet.AccountID);
    DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Updated gateway firmware");
    DBObject.ForceToBootloader = true;
    DBObject.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPI]
  public ActionResult RemoveSensor(long sensorID)
  {
    try
    {
      Sensor sensor = Sensor.Load(sensorID);
      if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
        return this.FormatRequest((object) "This account does not own this Sensor");
      if (sensor == null || sensor.IsDeleted)
        return this.FormatRequest((object) "try a different SensorId");
      long id = ConfigData.AppSettings("DefaultCSNetID").ToLong();
      Account account = Account.Load(sensor.AccountID);
      sensor.LogAuditData(eAuditAction.Delete, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Removed sensor");
      if (CSNetController.TryMoveSensor(id, sensor.SensorID))
      {
        sensor.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
        sensor.Save();
        sensor.ResetLastCommunicationDate();
        Gateway gateway = Gateway.LoadBySensorID(sensor.SensorID);
        if (gateway != null)
        {
          foreach (Notification notification in Notification.LoadByGatewayID(gateway.GatewayID))
            notification.RemoveGateway(gateway);
          gateway.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
          gateway.ResetToDefault(false);
          gateway.Save();
        }
        foreach (Notification notification in Notification.LoadBySensorID(sensor.SensorID))
          notification.RemoveSensor(sensor);
        return this.FormatRequest((object) "Success");
      }
    }
    catch
    {
    }
    return this.FormatRequest((object) "Unable to remove sensor!!");
  }

  [AuthorizeAPI]
  public ActionResult SensorSendControlCommand(
    long sensorID,
    int relayIndex,
    int state,
    int seconds)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return this.FormatRequest((object) "This account does not own this Sensor");
    try
    {
      if (sensor == null || sensor.IsDeleted)
        return this.FormatRequest((object) "try a different SensorId");
      if (seconds < 0)
        seconds = 0;
      if (seconds > (int) ushort.MaxValue)
        seconds = (int) ushort.MaxValue;
      NotificationRecorded notificationRecorded = new NotificationRecorded();
      notificationRecorded.SentToDeviceID = sensor.SensorID;
      notificationRecorded.QueueID = NotificationRecorded.NextQueueID(sensor.SensorID);
      notificationRecorded.NotificationDate = DateTime.UtcNow;
      notificationRecorded.NotificationType = eNotificationType.Control;
      int num = relayIndex == 0 ? 1 : 0;
      notificationRecorded.SerializedRecipientProperties = num == 0 ? $"0|{state}|0|{seconds}" : $"{state}|0|{seconds}|0";
      notificationRecorded.Save();
      sensor.PendingActionControlCommand = true;
      sensor.Save();
      CSNet.SetGatewaysUrgentTrafficFlag(sensor.CSNetID);
      return this.FormatRequest((object) "Control Message Successfull!! Control request will process when the sensor checks in.");
    }
    catch
    {
    }
    return this.FormatRequest((object) "Unable to send Control Message");
  }

  [AuthorizeAPI]
  public ActionResult SensorResetDefaults(long sensorID)
  {
    Sensor DBObject = Sensor.Load(sensorID);
    if (!MonnitSession.IsAuthorizedForAccount(DBObject.AccountID))
      return this.FormatRequest((object) "This account does not own this Sensor");
    if (DBObject == null || DBObject != null && DBObject.IsDeleted)
      return this.FormatRequest((object) "Unable to reset sensor!!");
    try
    {
      Account account = Account.Load(DBObject.AccountID);
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Set sensor settings to default");
      DBObject.SetDefaults(false);
      DBObject.Save();
      Gateway gateway = Gateway.LoadBySensorID(DBObject.SensorID);
      if (gateway != null)
      {
        gateway.ResetToDefault(false);
        gateway.Save();
      }
      return this.FormatRequest((object) "Success");
    }
    catch
    {
    }
    return this.FormatRequest((object) "Unable to reset sensor!!");
  }

  [AuthorizeAPI]
  public ActionResult AssignSensor(
    string sensorName,
    long networkID,
    long sensorID,
    string checkDigit)
  {
    if (!MonnitUtil.ValidateCheckDigit(sensorID, checkDigit))
      return this.FormatRequest((object) "Check digit did not match.");
    try
    {
      Sensor sens = Sensor.Load(sensorID);
      if (sens != null && sens.IsDeleted)
        return this.FormatRequest((object) "Sensor Not Found");
      CSNet csNet1 = CSNet.Load(networkID);
      if (csNet1 == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(csNet1.CSNetID))
        return this.FormatRequest((object) "Network Not Found");
      if (sens == null)
      {
        eProgramLevel o = MonnitSession.ProgramLevel();
        if (Sensor.totalCount() > o.ToInt())
          return this.FormatRequest((object) $"Only {o.ToString()} sensors allowed for this installation.");
        sens = CSNetControllerBase.LookUpSensor(csNet1.AccountID, sensorID, checkDigit, sens);
        if (sens == null)
          return this.FormatRequest((object) "Sensor Not Found");
        sens.ForceInsert();
        TimedCache.RemoveObject("SensorCount");
        Gateway gateway = Gateway.LoadBySensorID(sens.SensorID);
        if (gateway == null)
        {
          gateway = XDocument.Load(string.Format("{2}/xml/LookUpWifiGateway?SensorID={0}&checkDigit={1}", (object) sens.SensorID, (object) checkDigit, (object) ConfigData.FindValue("LookUpHost"))).Descendants((XName) "APILookUpGateway").Select<XElement, Gateway>((System.Func<XElement, Gateway>) (g => new Gateway()
          {
            GatewayID = g.Attribute((XName) "GatewayID").Value.ToLong(),
            Name = g.Attribute((XName) "GatewayName").Value,
            RadioBand = g.Attribute((XName) "RadioBand").Value,
            APNFirmwareVersion = g.Attribute((XName) "APNFirmwareVersion").Value,
            GatewayFirmwareVersion = g.Attribute((XName) "GatewayFirmwareVersion").Value,
            GatewayTypeID = g.Attribute((XName) "GatewayTypeID").Value.ToLong(),
            MacAddress = g.Attribute((XName) "MacAddress").Value,
            CSNetID = sens.CSNetID,
            SensorID = g.Attribute((XName) "SensorID").Value.ToLong(),
            GenerationType = g.Attribute((XName) "GenerationType").Value,
            PowerSourceID = g.Attribute((XName) "PowerSourceID") == null ? 3L : g.Attribute((XName) "PowerSourceID").Value.ToLong(),
            SKU = g.Attribute((XName) "SKU").Value
          })).FirstOrDefault<Gateway>();
          if (gateway != null)
          {
            gateway.CSNetID = csNet1.CSNetID;
            gateway.ForceInsert();
          }
        }
        if (gateway != null)
        {
          CSNet csNet2 = CSNet.Load(gateway.CSNetID);
          if (csNet2 != null && csNet2.AccountID != csNet1.AccountID)
          {
            foreach (Notification notification in Notification.LoadByGatewayID(gateway.GatewayID))
              notification.RemoveGateway(gateway);
          }
          gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, csNet1.AccountID, "Assigned  gateway to account");
          gateway.CSNetID = csNet1.CSNetID;
          gateway.Save();
        }
      }
      foreach (BaseDBObject baseDbObject in ExternalDataSubscription.LoadBySensorID(sensorID).ToArray())
        baseDbObject.Delete();
      if (!MonnitSession.CurrentCustomer.CanSeeAccount(csNet1.AccountID))
        return this.FormatRequest((object) $"SensorID: {sensorID} could not be transfered to new network.");
      if (!csNet1.HoldingOnlyNetwork && csNet1.Sensors.Where<Sensor>((System.Func<Sensor, bool>) (s => s.SensorTypeID != 4L)).Count<Sensor>() >= 500 && sens.SensorTypeID != 4L)
        return this.FormatRequest((object) $"SensorID: {sensorID} could not be transfered to new network.  Network sensor limit has been reached.");
      long accountId = sens.AccountID;
      if (csNet1 != null && sens != null)
      {
        if (sens.Network != null && !sens.Network.HoldingOnlyNetwork && sens.Network.CSNetID != ConfigData.AppSettings("DefaultCSNetID").ToLong() && !MonnitSession.CurrentCustomer.CanSeeNetwork(sens.CSNetID))
          return this.FormatRequest((object) $"SensorID: {sensorID} not authorized to transfer.");
        sens.AccountID = csNet1.AccountID;
        if (!string.IsNullOrEmpty(sensorName))
          sens.SensorName = sensorName;
        sens.CSNetID = networkID;
        sens.IsActive = true;
        sens.IsNewToNetwork = true;
        if (csNet1.AccountID != accountId)
        {
          sens.ResetLastCommunicationDate();
          sens.LastDataMessageGUID = Guid.Empty;
          sens.StartDate = DateTime.UtcNow;
        }
        Account account = Account.Load(csNet1.AccountID);
        sens.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Assigned sensor to network");
        sens.Save();
        Gateway gateway = Gateway.LoadBySensorID(sensorID);
        if (gateway == null)
        {
          gateway = XDocument.Load(string.Format("{2}/xml/LookUpWifiGateway?SensorID={0}&checkDigit={1}", (object) sens.SensorID, (object) checkDigit, (object) ConfigData.FindValue("LookUpHost"))).Descendants((XName) "APILookUpGateway").Select<XElement, Gateway>((System.Func<XElement, Gateway>) (g => new Gateway()
          {
            GatewayID = g.Attribute((XName) "GatewayID").Value.ToLong(),
            Name = g.Attribute((XName) "GatewayName").Value,
            RadioBand = g.Attribute((XName) "RadioBand").Value,
            APNFirmwareVersion = g.Attribute((XName) "APNFirmwareVersion").Value,
            GatewayFirmwareVersion = g.Attribute((XName) "GatewayFirmwareVersion").Value,
            GatewayTypeID = g.Attribute((XName) "GatewayTypeID").Value.ToLong(),
            MacAddress = g.Attribute((XName) "MacAddress").Value,
            CSNetID = sens.CSNetID,
            SensorID = g.Attribute((XName) "SensorID").Value.ToLong(),
            GenerationType = g.Attribute((XName) "GenerationType").Value,
            PowerSourceID = g.Attribute((XName) "PowerSourceID") == null ? 3L : g.Attribute((XName) "PowerSourceID").Value.ToLong(),
            SKU = g.Attribute((XName) "SKU").Value
          })).FirstOrDefault<Gateway>();
          if (gateway != null)
          {
            gateway.CSNetID = csNet1.CSNetID;
            gateway.ForceInsert();
          }
        }
        if (gateway != null)
        {
          if (csNet1.AccountID != accountId)
          {
            foreach (Notification notification in Notification.LoadByGatewayID(gateway.GatewayID))
              notification.RemoveGateway(gateway);
          }
          gateway.CSNetID = networkID;
          gateway.Save();
        }
        if (csNet1.AccountID != accountId)
        {
          foreach (Notification notification in Notification.LoadBySensorID(sens.SensorID))
            notification.RemoveSensor(sens);
        }
        return this.FormatRequest((object) "Success");
      }
    }
    catch (Exception ex)
    {
      ex.Log("APIController.AssignSensor" + ex.Message);
    }
    return this.FormatRequest((object) $"SensorID: {sensorID} could not be transfered to new network.");
  }

  [AuthorizeAPI]
  public ActionResult WIFISensorIDGet(string mac)
  {
    try
    {
      Gateway gateway = Gateway.LoadByMAC(mac.Replace(":", "").Replace("-", ""));
      if (gateway != null && gateway.SensorID > 0L)
        return this.FormatRequest((object) gateway.SensorID);
    }
    catch (Exception ex)
    {
      ex.Log("APIController.WIFISensorIDGet");
    }
    return this.FormatRequest((object) 0);
  }

  [AuthorizeAPI]
  public ActionResult NetworkIDFromSensorGet(long sensorID, string checkDigit)
  {
    try
    {
      Sensor sensor = Sensor.Load(sensorID);
      if (sensor != null && !sensor.IsDeleted)
      {
        if (MonnitSession.CurrentCustomer.CanSeeSensor(sensor) || sensor.CSNetID == ConfigData.AppSettings("DefaultCSNetID").ToLong() || !string.IsNullOrEmpty(checkDigit) && MonnitUtil.ValidateCheckDigit(sensorID, checkDigit))
          return this.FormatRequest((object) sensor.CSNetID);
      }
    }
    catch (Exception ex)
    {
      ex.Log("APIController.NetworkIDFromSensorGet");
    }
    return this.FormatRequest((object) 0);
  }

  [AuthorizeAPI]
  public ActionResult SensorNameGet(long sensorID, string checkDigit)
  {
    try
    {
      Sensor sensor = Sensor.Load(sensorID);
      if (sensor != null && !sensor.IsDeleted)
      {
        if (MonnitSession.CurrentCustomer.CanSeeSensor(sensor) || sensor.CSNetID == ConfigData.AppSettings("DefaultCSNetID").ToLong() || !string.IsNullOrEmpty(checkDigit) && MonnitUtil.ValidateCheckDigit(sensorID, checkDigit))
          return this.FormatRequest((object) sensor.SensorName);
      }
    }
    catch (Exception ex)
    {
      ex.Log("APIController.NetworkIDFromSensorGet");
    }
    return this.FormatRequest((object) "");
  }

  [AuthorizeAPI]
  public ActionResult SensorApplicationIDGet(long sensorID, string checkDigit)
  {
    try
    {
      Sensor sensor = Sensor.Load(sensorID);
      if (sensor != null && !sensor.IsDeleted)
      {
        if (MonnitSession.CurrentCustomer.CanSeeSensor(sensor) || sensor.CSNetID == ConfigData.AppSettings("DefaultCSNetID").ToLong() || !string.IsNullOrEmpty(checkDigit) && MonnitUtil.ValidateCheckDigit(sensorID, checkDigit))
          return this.FormatRequest((object) sensor.ApplicationID);
      }
    }
    catch (Exception ex)
    {
      ex.Log("APIController.NetworkIDFromSensorGet");
    }
    return this.FormatRequest((object) 0);
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorGet(long sensorID)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Invalid SensorID");
    return sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor) ? this.FormatRequest((object) "Unathorized") : this.FormatRequest((object) new APISensor(sensor));
  }

  public ActionResult GetWifi2SigningKeyOffline(long deviceID, long sensorID)
  {
    try
    {
      Gateway gateway = Gateway.Load(deviceID);
      if (gateway == null || gateway.IsDeleted)
        return (ActionResult) this.Json((object) new
        {
          Error = $"GatewayID: {deviceID} could not be found"
        });
      if (gateway.SensorID != sensorID)
        return (ActionResult) this.Json((object) new
        {
          Error = $"SensorID: {sensorID} sensor does not match"
        });
      string str = ((XElement) XDocument.Load(string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/GetWifi2SigningKey/{0}?DeviceID={1}", (object) MonnitUtil.GetMEATempAuthToken(), (object) deviceID)).Descendants((XName) "Result").FirstOrDefault<XElement>().FirstNode).FirstAttribute.Value;
      return !string.IsNullOrEmpty(str) ? this.FormatRequest((object) new
      {
        SigningKey = str
      }) : throw new Exception("Signing key not found");
    }
    catch (Exception ex)
    {
      ex.Log($"APIController.GetWifi2SigningKeyOffline for DeviceID: {deviceID}");
      return this.FormatRequest((object) new
      {
        Error = "Failed to retrieve signing keys",
        Details = ex.Message
      });
    }
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorGetExtended(long sensorID)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Invalid SensorID");
    return sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor) ? this.FormatRequest((object) "Unathorized") : this.FormatRequest((object) new APISensorExteded(sensor));
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorGetExtendedForFulfillment(long sensorID)
  {
    Sensor sensor = Sensor.Load(sensorID);
    return sensor != null && !sensor.IsDeleted ? this.FormatRequest((object) new APISensorGetExtendedForFulfillment(sensor)) : this.FormatRequest((object) "Invalid SensorID");
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorGetExtendedForFullfillment(long sensorID)
  {
    return this.SensorGetExtendedForFulfillment(sensorID);
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorGetCalibration(long sensorID)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Invalid SensorID");
    return sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor) ? this.FormatRequest((object) "Unathorized") : this.FormatRequest((object) new APISensorCalibration(sensor));
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorList(
    long? networkID,
    long? applicationID,
    int? status,
    string name,
    long? accountID)
  {
    long CSNetID = networkID ?? long.MinValue;
    long AppID = applicationID ?? long.MinValue;
    int Status = status ?? int.MinValue;
    string Name = name.ToStringSafe();
    long AccID = accountID ?? MonnitSession.CurrentCustomer.AccountID;
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(AccID))
      return this.FormatRequest((object) "Invalid Account");
    DataMessage.CacheLastByAccount(AccID, new TimeSpan(0, 1, 0));
    return this.FormatRequest((object) Sensor.LoadByAccountID(AccID).Where<Sensor>((System.Func<Sensor, bool>) (s =>
    {
      if (s.CSNetID != CSNetID && CSNetID != long.MinValue || s.CSNetID == long.MinValue || AccID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(s) || s.Status.ToInt() != Status && Status != int.MinValue || s.ApplicationID != AppID && AppID != long.MinValue)
        return false;
      return s.SensorName.ToLower().Contains(Name.ToLower()) || Name == "";
    })).OrderBy<Sensor, string>((System.Func<Sensor, string>) (s => s.SensorName.Trim())).Select<Sensor, APISensor>((System.Func<Sensor, APISensor>) (s => new APISensor(s))).ToList<APISensor>());
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorListExtended(
    long? networkID,
    long? applicationID,
    int? status,
    string name,
    long? accountID)
  {
    long CSNetID = networkID ?? long.MinValue;
    long AppID = applicationID ?? long.MinValue;
    int Status = status ?? int.MinValue;
    string Name = name.ToStringSafe();
    long AccID = accountID ?? MonnitSession.CurrentCustomer.AccountID;
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(AccID))
      return this.FormatRequest((object) "Invalid Account");
    DataMessage.CacheLastByAccount(AccID, new TimeSpan(0, 1, 0));
    return this.FormatRequest((object) Sensor.LoadByAccountID(AccID).Where<Sensor>((System.Func<Sensor, bool>) (s =>
    {
      if (s.CSNetID != CSNetID && CSNetID != long.MinValue || s.CSNetID == long.MinValue || AccID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(s) || s.Status.ToInt() != Status && Status != int.MinValue || s.ApplicationID != AppID && AppID != long.MinValue)
        return false;
      return s.SensorName.ToLower().Contains(Name.ToLower()) || Name == "";
    })).OrderBy<Sensor, string>((System.Func<Sensor, string>) (s => s.SensorName.Trim())).Select<Sensor, APISensorExteded>((System.Func<Sensor, APISensorExteded>) (s => new APISensorExteded(s))).ToList<APISensorExteded>());
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorListFull(
    long? networkID,
    long? applicationID,
    int? status,
    string name,
    long? accountID)
  {
    long CSNetID = networkID ?? long.MinValue;
    long AppID = applicationID ?? long.MinValue;
    int Status = status ?? int.MinValue;
    string Name = name.ToStringSafe();
    long AccID = accountID ?? MonnitSession.CurrentCustomer.AccountID;
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(AccID))
      return this.FormatRequest((object) "Invalid Account");
    DataMessage.CacheLastByAccount(AccID, new TimeSpan(0, 1, 0));
    return this.FormatRequest((object) Sensor.LoadByAccountID(AccID).Where<Sensor>((System.Func<Sensor, bool>) (s =>
    {
      if (s.CSNetID != CSNetID && CSNetID != long.MinValue || s.CSNetID == long.MinValue || AccID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(s) || s.Status.ToInt() != Status && Status != int.MinValue || s.ApplicationID != AppID && AppID != long.MinValue)
        return false;
      return s.SensorName.ToLower().Contains(Name.ToLower()) || Name == "";
    })).OrderBy<Sensor, string>((System.Func<Sensor, string>) (s => s.SensorName.Trim())).Select<Sensor, APISensorFull>((System.Func<Sensor, APISensorFull>) (s => new APISensorFull(s))).ToList<APISensorFull>());
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorSetName(long sensorID, string sensorName)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Invalid SensorID");
    if (sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return this.FormatRequest((object) "Unathorized");
    Account account = Account.Load(sensor.AccountID);
    sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited sensor name");
    sensor.SensorName = sensorName;
    sensor.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorSetHeartbeat(
    long sensorID,
    double reportInterval,
    double activeStateInterval)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Invalid SensorID");
    if (sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return this.FormatRequest((object) "Unauthorized");
    Account account = Account.Load(sensor.AccountID);
    sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited sensor heartbeat");
    double num = MonnitSession.CurrentCustomer.Account.MinHeartBeat();
    if (reportInterval < num)
      return this.FormatRequest((object) $"Invalid reportInterval: Must be greater than or equal to {num.ToString()} minutes.");
    if (reportInterval > 720.0)
      return this.FormatRequest((object) "Invalid reportInterval: Must be less than or equal to 720 minutes.");
    if (reportInterval < activeStateInterval)
      return this.FormatRequest((object) "Invalid reportInterval: Must be greater than activeStateInterval");
    if (activeStateInterval < num)
      return this.FormatRequest((object) $"Invalid activeStateInterval: Must be greater than or equal to {num.ToString()} minutes.");
    if (activeStateInterval > 720.0)
      return this.FormatRequest((object) "Invalid activeStateInterval: Must be less than or equal to 720 minutes.");
    sensor.ReportInterval = reportInterval;
    sensor.ActiveStateInterval = activeStateInterval;
    sensor.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorSetThreshold(
    long sensorID,
    int measurementsPerTransmission,
    long minimumThreshold,
    long maximumThreshold,
    long hysteresis)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Invalid SensorID");
    if (sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return this.FormatRequest((object) "Unauthorized");
    Account account = Account.Load(sensor.AccountID);
    sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited sensor threshold settings");
    sensor.MeasurementsPerTransmission = measurementsPerTransmission;
    sensor.MinimumThreshold = minimumThreshold;
    sensor.MaximumThreshold = maximumThreshold;
    sensor.Hysteresis = hysteresis;
    sensor.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorSetCalibration(
    long sensorID,
    long? calibration1,
    long? calibration2,
    long? calibration3,
    long? calibration4,
    int? eventDetectionType,
    int? eventDetectionPeriod,
    int? eventDetectionCount,
    int? rearmTime,
    int? biStable,
    int? Recovery,
    int? TransmitOffset,
    bool? pushProfileConfig1,
    bool? pushProfileConfig2,
    bool? pushAutoCalibrateCommand)
  {
    Sensor sensor1 = Sensor.Load(sensorID);
    if (sensor1 == null || sensor1.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor1.AccountID))
      return this.FormatRequest((object) "Invalid SensorID");
    if (sensor1.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor1))
      return this.FormatRequest((object) "Unathorized");
    Account account = Account.Load(sensor1.AccountID);
    sensor1.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited sensor calibration settings");
    Sensor sensor2 = sensor1;
    long? nullable1 = calibration1;
    long num1 = nullable1 ?? sensor1.Calibration1;
    sensor2.Calibration1 = num1;
    Sensor sensor3 = sensor1;
    nullable1 = calibration2;
    long num2 = nullable1 ?? sensor1.Calibration2;
    sensor3.Calibration2 = num2;
    Sensor sensor4 = sensor1;
    nullable1 = calibration3;
    long num3 = nullable1 ?? sensor1.Calibration3;
    sensor4.Calibration3 = num3;
    Sensor sensor5 = sensor1;
    nullable1 = calibration4;
    long num4 = nullable1 ?? sensor1.Calibration4;
    sensor5.Calibration4 = num4;
    Sensor sensor6 = sensor1;
    int? nullable2 = eventDetectionType;
    int num5 = nullable2 ?? sensor1.EventDetectionType;
    sensor6.EventDetectionType = num5;
    Sensor sensor7 = sensor1;
    nullable2 = eventDetectionPeriod;
    int num6 = nullable2 ?? sensor1.EventDetectionPeriod;
    sensor7.EventDetectionPeriod = num6;
    Sensor sensor8 = sensor1;
    nullable2 = eventDetectionCount;
    int num7 = nullable2 ?? sensor1.EventDetectionCount;
    sensor8.EventDetectionCount = num7;
    Sensor sensor9 = sensor1;
    nullable2 = rearmTime;
    int num8 = nullable2 ?? sensor1.RearmTime;
    sensor9.RearmTime = num8;
    Sensor sensor10 = sensor1;
    nullable2 = biStable;
    int num9 = nullable2 ?? sensor1.BiStable;
    sensor10.BiStable = num9;
    Sensor sensor11 = sensor1;
    bool? nullable3 = pushProfileConfig1;
    int num10 = (int) nullable3 ?? (sensor1.ProfileConfigDirty ? 1 : 0);
    sensor11.ProfileConfigDirty = num10 != 0;
    Sensor sensor12 = sensor1;
    nullable3 = pushProfileConfig2;
    int num11 = (int) nullable3 ?? (sensor1.ProfileConfig2Dirty ? 1 : 0);
    sensor12.ProfileConfig2Dirty = num11 != 0;
    Sensor sensor13 = sensor1;
    nullable3 = pushAutoCalibrateCommand;
    int num12 = (int) nullable3 ?? (sensor1.PendingActionControlCommand ? 1 : 0);
    sensor13.PendingActionControlCommand = num12 != 0;
    Sensor sensor14 = sensor1;
    nullable2 = TransmitOffset;
    int num13 = nullable2 ?? sensor1.TransmitOffset;
    sensor14.TransmitOffset = num13;
    Sensor sensor15 = sensor1;
    nullable2 = Recovery;
    int num14 = nullable2 ?? sensor1.Recovery;
    sensor15.Recovery = num14;
    sensor1.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorSetAlerts(long sensorID, bool active)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Invalid SensorID");
    if (sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor) && !MonnitSession.CustomerCan("Notification_Disable_Network"))
      return this.FormatRequest((object) "Unathorized");
    CSNet DBObject = CSNet.Load(sensor.CSNetID);
    if (DBObject.SendNotifications != active)
    {
      Account account = Account.Load(DBObject.AccountID);
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Network, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Toggled notification active status for sensor");
      DBObject.SendNotifications = active;
      DBObject.Save();
    }
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorSetIP(
    long sensorID,
    string ipAddress,
    string networkMask,
    string defaultGateway,
    string dnsServer)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor != null && !sensor.IsDeleted && MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
    {
      if (sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
        return this.FormatRequest((object) "Unauthorized");
      Account account = Account.Load(sensor.AccountID);
      sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited sensor calibration settings");
      Gateway gateway = Gateway.LoadBySensorID(sensorID);
      if (gateway == null)
        return this.FormatRequest((object) "Sensor does not contain a Gateway");
      this.GatewaySetIP(gateway.GatewayID, ipAddress, networkMask, defaultGateway, dnsServer);
    }
    return this.FormatRequest((object) "Invalid SensorID");
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorSetTag(long sensorID, string tag)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Invalid SensorID");
    if (sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return this.FormatRequest((object) "Unauthorized");
    Account account = Account.Load(sensor.AccountID);
    sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited sensor tag");
    sensor.TagString = tag;
    sensor.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorAttributes(long sensorID)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Invalid SensorID");
    if (sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return this.FormatRequest((object) "Unathorized");
    return MonnitApplicationBase.LoadMonnitApplication("1", (string) null, sensor.ApplicationID) is ISensorAttribute ? this.FormatRequest((object) SensorAttribute.LoadBySensorID(sensorID).Select<SensorAttribute, APISensorAttribute>((System.Func<SensorAttribute, APISensorAttribute>) (att => new APISensorAttribute(att))).ToList<APISensorAttribute>()) : this.FormatRequest((object) "Sensor does not support attributes.");
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorAttributeSet(long sensorID, string name, string value)
  {
    if (name.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    Sensor sensor = Sensor.Load(sensorID);
    if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(value))
      return this.FormatRequest((object) "Name and/or Value parameters must be included");
    if (sensor == null || sensor.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Invalid SensorID");
    if (sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return this.FormatRequest((object) "Unathorized");
    if (!(MonnitApplicationBase.LoadMonnitApplication("1", (string) null, sensor.ApplicationID) is ISensorAttribute))
      return this.FormatRequest((object) "Sensor does not support attributes.");
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensorID).Where<SensorAttribute>((System.Func<SensorAttribute, bool>) (att => att.Name == name)))
      baseDbObject.Delete();
    SensorAttribute att1 = new SensorAttribute();
    Account account = Account.Load(sensor.AccountID);
    string changeRecord = $"{{\"SensorID\": \"{sensor.SensorID}\", \"AttributeName\" : \"{name}\", \"AttributeValue\" : \"{value}\" }} ";
    sensor.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Edited sensor attribute");
    att1.SensorID = sensorID;
    att1.Name = name;
    att1.Value = value;
    att1.Save();
    SensorAttribute.ResetAttributeList(sensorID);
    return this.FormatRequest((object) new APISensorAttribute(att1));
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorAttributeRemove(long sensorID, string name)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Invalid SensorID");
    if (sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return this.FormatRequest((object) "Unathorized");
    if (!(MonnitApplicationBase.LoadMonnitApplication("1", (string) null, sensor.ApplicationID) is ISensorAttribute))
      return this.FormatRequest((object) "Sensor does not support attributes.");
    SensorAttribute sensorAttribute = SensorAttribute.LoadBySensorID(sensorID).Where<SensorAttribute>((System.Func<SensorAttribute, bool>) (att => att.Name == name)).FirstOrDefault<SensorAttribute>();
    Account account = Account.Load(sensor.AccountID);
    string changeRecord = $"{{\"SensorID\": \"{sensor.SensorID}\", \"AttributeName\" : \"{name}\"}} ";
    sensor.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Delete sensor attribute");
    sensorAttribute?.Delete();
    SensorAttribute.ResetAttributeList(sensorID);
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPI]
  public ActionResult ListenBeforeTalk(long? sensorID, long? networkID, int Value)
  {
    try
    {
      List<Sensor> sensorList = (networkID ?? long.MinValue) <= 0L || !MonnitSession.CurrentCustomer.CanSeeNetwork(networkID ?? long.MinValue) ? new List<Sensor>() : Sensor.LoadByCsNetID(networkID ?? long.MinValue);
      Sensor sensor1 = Sensor.Load(sensorID ?? long.MinValue);
      if (sensor1 != null && MonnitSession.CurrentCustomer.CanSeeSensor(sensor1))
        sensorList.Add(sensor1);
      if (sensorList.Count <= 0)
        return this.FormatRequest((object) "Unable to set LBT.");
      foreach (Sensor sensor2 in sensorList)
      {
        if (Value == 8)
        {
          sensor2.ListenBeforeTalkValue = 8;
        }
        else
        {
          int num = Value < -7 ? 1 : (Value > 8 ? 1 : 0);
          sensor2.ListenBeforeTalkValue = num == 0 ? Value : (int) byte.MaxValue;
        }
        sensor2.Save();
      }
    }
    catch
    {
    }
    return this.FormatRequest((object) "Unable to set LBT.");
  }

  [AuthorizeAPI]
  public ActionResult SetDatumName(long? sensorID, int? index, string name)
  {
    if (name.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    try
    {
      Sensor DBObject = Sensor.Load(sensorID ?? long.MinValue);
      if (DBObject == null || DBObject != null && DBObject.IsDeleted)
        throw new Exception("You do not have permission to SetDatumName for Sensor " + sensorID.ToString());
      if (!MonnitSession.IsAuthorizedForAccount(DBObject.AccountID))
        throw new Exception("You do not have permission to SetDatumName for Sensor " + sensorID.ToString());
      if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
        throw new Exception("You do not have permission to SetDatumName for Sensor " + sensorID.ToString());
      if (!index.HasValue)
        throw new Exception("Unable to update SetDatumName for SensorID " + sensorID.ToString());
      if (string.IsNullOrWhiteSpace(name))
        throw new Exception("Unable to update SetDatumName for SensorID " + sensorID.ToString());
      Account account = Account.Load(DBObject.AccountID);
      if (account != null)
      {
        string changeRecord = $"{{\"SensorID\": \"{DBObject.SensorID}\", \"DatumName\" : \"{name}\", \"DatumIndex\" : \"{index.GetValueOrDefault()}\" }} ";
        DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Edited sensor datum name");
      }
      DBObject.SetDatumName(index.GetValueOrDefault(), name);
      return this.FormatRequest((object) "Success");
    }
    catch (Exception ex)
    {
      return ex.Message.ToLower().Contains("setdatumname") ? this.FormatRequest((object) ex.Message) : this.FormatRequest((object) "unable to set DatumNameChange");
    }
  }

  [AuthorizeAPI]
  public ActionResult GetDatumNameList(long? sensorID)
  {
    try
    {
      Sensor sensor = Sensor.Load(sensorID ?? long.MinValue);
      if (sensor == null || sensor != null && sensor.IsDeleted)
        throw new Exception("There is no datum names list for Sensor " + sensorID.ToString());
      if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
        throw new Exception("You do not have permission to see the datum names list for Sensor " + sensorID.ToString());
      if (!MonnitSession.CurrentCustomer.CanSeeNetwork(sensor.CSNetID))
        throw new Exception("You do not have permission to see the datum names list for Sensor " + sensorID.ToString());
      int count = MonnitApplicationBase.GetAppDatums(sensor.ApplicationID).Count;
      List<APIDatumName> apiDatumNameList = new List<APIDatumName>();
      for (int datumindex = 0; datumindex < count; ++datumindex)
        apiDatumNameList.Add(new APIDatumName()
        {
          SensorID = sensor.SensorID,
          Index = datumindex.ToString(),
          Name = sensor.GetDatumName(datumindex)
        });
      return this.FormatRequest((object) apiDatumNameList);
    }
    catch (Exception ex)
    {
      return ex.Message.ToLower().Contains("datum names") ? this.FormatRequest((object) ex.Message) : this.FormatRequest((object) ("unable to get the datum names list for Sensor " + sensorID.ToString()));
    }
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorEquipmentGet(long sensorID)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Invalid SensorID");
    return sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor) ? this.FormatRequest((object) "Unathorized") : this.FormatRequest((object) new APISensorEquipment(sensor));
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorEquipmentSet(
    long sensorID,
    string make,
    string model,
    string serialNumber,
    string sensorLocation,
    string sensorDescription,
    string note)
  {
    if (make.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (sensorLocation.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (sensorDescription.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (note.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Invalid SensorID");
    if (sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return this.FormatRequest((object) "Unathorized");
    sensor.Make = make == null ? sensor.Make : make;
    sensor.Model = model == null ? sensor.Model : model;
    sensor.SerialNumber = serialNumber == null ? sensor.SerialNumber : serialNumber;
    sensor.Location = sensorLocation == null ? sensor.Location : sensorLocation;
    sensor.Description = sensorDescription == null ? sensor.Description : sensorDescription;
    sensor.Note = note == null ? sensor.Note : note;
    sensor.Save();
    return this.FormatRequest((object) new APISensorEquipment(sensor));
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorGroupSensorList(long sensorGroupID)
  {
    SensorGroup sensorGroup = SensorGroup.Load(sensorGroupID);
    List<APISensorGroupSensor> sensorGroupSensorList = new List<APISensorGroupSensor>();
    if (sensorGroup == null)
      return this.FormatRequest((object) "Invalid Request: no Group found");
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(sensorGroup.AccountID))
      return this.FormatRequest((object) "Invalid Request: no account found");
    if (sensorGroup.Sensors == null || sensorGroup.Sensors.Count<SensorGroupSensor>() == 0)
      return this.FormatRequest((object) "Group has no sensors assigned");
    foreach (SensorGroupSensor sensor in sensorGroup.Sensors)
    {
      APISensorGroupSensor sensorGroupSensor = new APISensorGroupSensor(sensor);
      sensorGroupSensorList.Add(sensorGroupSensor);
    }
    return this.FormatRequest((object) sensorGroupSensorList);
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorGroupList(long? accountID, long? parentID)
  {
    long? nullable = accountID;
    Account acct = Account.Load(nullable ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    long num1 = parentID ?? long.MinValue;
    List<SensorGroup> sensorGroupList = SensorGroup.LoadByAccount(acct.AccountID);
    List<APISensorGroup> apiSensorGroupList = new List<APISensorGroup>();
    foreach (SensorGroup group in sensorGroupList)
    {
      APISensorGroup apiSensorGroup = new APISensorGroup(group);
      nullable = parentID;
      long num2 = 0;
      if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
      {
        long parentId = group.ParentID;
        nullable = parentID;
        long valueOrDefault = nullable.GetValueOrDefault();
        if (parentId == valueOrDefault & nullable.HasValue)
          apiSensorGroupList.Add(apiSensorGroup);
      }
      else
        apiSensorGroupList.Add(apiSensorGroup);
    }
    return this.FormatRequest((object) apiSensorGroupList);
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorGroupCreate(
    long? accountID,
    long? sensorGroupID,
    string Name,
    long? parentID)
  {
    if (Name.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    long? nullable = accountID;
    Account acct = Account.Load(nullable ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    if (!MonnitSession.CustomerCan("Sensor_Group_Edit"))
      return this.FormatRequest((object) "Invalid Request: Invalid Permissions");
    SensorGroup group = new SensorGroup();
    nullable = sensorGroupID;
    long num1 = 0;
    if (nullable.GetValueOrDefault() > num1 & nullable.HasValue)
    {
      group = SensorGroup.Load(sensorGroupID.GetValueOrDefault());
      if (group == null)
        return this.FormatRequest((object) "Invalid Request: Invalid Group");
      if (!MonnitSession.CurrentCustomer.CanSeeAccount(group.AccountID))
        return this.FormatRequest((object) "Invalid Request: Unauthorized Account");
      group.Name = Name == string.Empty ? group.Name : Name;
      nullable = parentID;
      long num2 = 0;
      if (nullable.GetValueOrDefault() >= num2 & nullable.HasValue)
        group.ParentID = parentID ?? long.MinValue;
      group.Save();
    }
    else
    {
      group.AccountID = acct.AccountID;
      group.Name = Name == string.Empty ? "Sensor Group" : Name;
      nullable = parentID;
      long num3 = 0;
      if (nullable.GetValueOrDefault() >= num3 & nullable.HasValue)
        group.ParentID = parentID ?? long.MinValue;
      group.Save();
    }
    return this.FormatRequest((object) new APISensorGroup(group));
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorGroupDelete(long sensorGroupID)
  {
    if (!MonnitSession.CustomerCan("Sensor_Group_Edit"))
      return this.FormatRequest((object) "Invalid Request: Invalid Permissions");
    SensorGroup sensorGroup = SensorGroup.Load(sensorGroupID);
    if (sensorGroup == null)
      return this.FormatRequest((object) "Invalid Request: Group not found");
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(sensorGroup.AccountID))
      return this.FormatRequest((object) "Invalid Request: Unauthorized Group");
    foreach (SensorGroupSensor sensor in sensorGroup.Sensors)
      sensorGroup.RemoveSensor(sensor.SensorID);
    sensorGroup.Delete();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorGroupAssignSensor(long sensorID, long groupID, int? position)
  {
    List<APISensorGroupSensor> sensorGroupSensorList = new List<APISensorGroupSensor>();
    SensorGroup sensorGroup = SensorGroup.Load(groupID);
    if (sensorGroup == null)
      return this.FormatRequest((object) "Invalid Request: no group found");
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(sensorGroup.AccountID))
      return this.FormatRequest((object) "Invalid Request: Unauthorized Account");
    Sensor sensor1 = Sensor.Load(sensorID);
    if (sensor1 == null)
      return this.FormatRequest((object) "Invalid Request: no sensor found");
    if (!MonnitSession.CurrentCustomer.CanSeeSensor(sensor1))
      return this.FormatRequest((object) "Invalid Request: Unauthorized Sensor");
    foreach (SensorGroupSensor sensorGroupSensor in SensorGroupSensor.LoadBySensor(sensorID))
    {
      if (sensorGroupSensor.SensorID == sensorID && sensorGroupSensor.SensorGroupID == groupID)
        sensorGroupSensor.Delete();
    }
    int num1 = 0;
    foreach (SensorGroupSensor sensor2 in sensorGroup.Sensors)
    {
      int num2 = num1;
      int? nullable = position;
      int valueOrDefault = nullable.GetValueOrDefault();
      if (num2 == valueOrDefault & nullable.HasValue)
        ++num1;
      sensor2.Position = num1;
      sensor2.Save();
      ++num1;
    }
    sensorGroup.AddSensor(sensorID, position.GetValueOrDefault());
    foreach (SensorGroupSensor sensor3 in sensorGroup.Sensors)
    {
      APISensorGroupSensor sensorGroupSensor = new APISensorGroupSensor(sensor3);
      sensorGroupSensorList.Add(sensorGroupSensor);
    }
    return this.FormatRequest((object) sensorGroupSensorList);
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorGroupRemoveSensor(long sensorID, long groupID)
  {
    if (!MonnitSession.CustomerCan("Sensor_Group_Edit"))
      return this.FormatRequest((object) "Invalid Request: Invalid Permissions");
    Sensor sensor = Sensor.Load(sensorID);
    SensorGroup sensorGroup = SensorGroup.Load(groupID);
    if (sensor == null)
      return this.FormatRequest((object) "Invalid Request: Sensor not found ");
    if (sensorGroup == null)
      return this.FormatRequest((object) "Invalid Request: group not found ");
    if (!MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return this.FormatRequest((object) "Invalid Request: Unauthorized Sensor");
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(sensorGroup.AccountID))
      return this.FormatRequest((object) "Invalid Request: Unauthorized group");
    List<SensorGroupSensor> sensorGroupSensorList = SensorGroupSensor.LoadBySensor(sensorID);
    if (sensorGroupSensorList.Count > 0)
    {
      foreach (SensorGroupSensor sensorGroupSensor in sensorGroupSensorList)
      {
        if (sensorGroupSensor.SensorID == sensorID && sensorGroupSensor.SensorGroupID == groupID)
          sensorGroupSensor.Delete();
      }
    }
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult AccountDataMessages(
    DateTime fromDate,
    DateTime toDate,
    long? networkID,
    long? accountID)
  {
    long accountID1 = accountID ?? MonnitSession.CurrentCustomer.AccountID;
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(accountID1))
      return this.FormatRequest((object) "Invalid account");
    if (toDate < fromDate || toDate - fromDate > new TimeSpan(7, 0, 0, 1))
      return this.FormatRequest((object) "Invalid Date Range - Only 7 days of data can be returned in one call");
    Dictionary<long, Sensor> dictionary = new Dictionary<long, Sensor>();
    List<APIDataMessage> apiDataMessageList = new List<APIDataMessage>();
    if (!networkID.HasValue)
    {
      foreach (Sensor sensor in Sensor.LoadByAccountID(accountID1))
      {
        if (!dictionary.ContainsKey(sensor.SensorID))
          dictionary.Add(sensor.SensorID, sensor);
      }
      foreach (DataMessage dm in DataMessage.LoadByAccount(accountID1, fromDate, toDate, 5000))
      {
        Sensor s = dictionary[dm.SensorID];
        if (s != null)
          apiDataMessageList.Add(new APIDataMessage(s, dm));
      }
    }
    else
    {
      CSNet csNet = CSNet.Load(networkID.GetValueOrDefault());
      if (csNet == null || !MonnitSession.CurrentCustomer.CanSeeAccount(csNet.AccountID) || !MonnitSession.CurrentCustomer.CanSeeNetwork(csNet.CSNetID))
        return this.FormatRequest((object) "Invalid NetworkID");
      foreach (Sensor sensor in Sensor.LoadByCsNetID(csNet.CSNetID))
      {
        if (!dictionary.ContainsKey(sensor.SensorID))
          dictionary.Add(sensor.SensorID, sensor);
      }
      foreach (DataMessage dm in DataMessage.LoadByNetwork(csNet.CSNetID, fromDate, toDate, 5000))
      {
        Sensor s = dictionary[dm.SensorID];
        if (s != null)
          apiDataMessageList.Add(new APIDataMessage(s, dm));
      }
    }
    return this.FormatRequest((object) apiDataMessageList);
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorDataMessages(long sensorID, DateTime fromDate, DateTime toDate)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null)
      return this.FormatRequest((object) "Invalid SensorID");
    if (toDate < fromDate || toDate - fromDate > new TimeSpan(7, 0, 0, 1))
      return this.FormatRequest((object) "Invalid Date Range - Only 7 days of data can be returned in one call");
    if (fromDate.Ticks == toDate.Ticks || toDate.Hour == 0 && toDate.Minute == 0 && toDate.Second == 0 && toDate.Millisecond == 0)
      toDate = toDate.AddDays(1.0);
    List<APIDataMessage> apiDataMessageList = new List<APIDataMessage>();
    if (!sensor.IsDeleted && MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID) && MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
    {
      if (!MonnitSession.IsCurrentCustomerMonnitAdmin && sensor.StartDate != DateTime.MinValue && sensor.StartDate.Ticks > fromDate.Ticks)
        fromDate = sensor.StartDate;
      if (!MonnitSession.CurrentCustomer.Account.HideData)
      {
        int num = 0;
        foreach (DataMessage dm in DataMessage.LoadBySensorAndDateRange(sensor.SensorID, fromDate, toDate, 5000, new Guid?(Guid.Empty)))
        {
          try
          {
            ++num;
            apiDataMessageList.Add(new APIDataMessage(sensor, dm));
          }
          catch (Exception ex)
          {
            ex.Log($"APIController.SensorDataMessages: list.Count before=[{num}], now=[{apiDataMessageList.Count}], sensorID=[{sensorID}], fromDate=[{fromDate}], toDate=[{toDate}]");
          }
        }
      }
    }
    return this.FormatRequest((object) apiDataMessageList);
  }

  [AuthorizeAPIPremier]
  public ActionResult GatewayMessages(long gatewayID, DateTime fromDate, DateTime toDate)
  {
    if (toDate < fromDate || toDate - fromDate > new TimeSpan(7, 0, 0, 1))
      return this.FormatRequest((object) "Invalid Date Range - Only 7 days of data can be returned in one call");
    List<APIGatewayMessage> apiGatewayMessageList = new List<APIGatewayMessage>();
    Gateway g = Gateway.Load(gatewayID);
    if (g == null || g != null && g.IsDeleted)
      return this.FormatRequest((object) "No gateway found with that ID");
    CSNet csNet = CSNet.Load(g.CSNetID);
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && g.StartDate != DateTime.MinValue && g.StartDate.Ticks > fromDate.Ticks)
      fromDate = g.StartDate;
    if (csNet != null && MonnitSession.CurrentCustomer.CanSeeAccount(csNet.AccountID) && MonnitSession.CurrentCustomer.CanSeeNetwork(csNet.CSNetID))
    {
      foreach (GatewayMessage gm in GatewayMessage.LoadByGatewayAndDateRange(g.GatewayID, fromDate, toDate, 5000))
        apiGatewayMessageList.Add(new APIGatewayMessage(g, gm));
    }
    return this.FormatRequest((object) apiGatewayMessageList);
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorRecentDataMessages(
    long sensorID,
    int minutes,
    Guid? lastDataMessageGUID)
  {
    if (minutes > 1440 || minutes < 0)
      return this.FormatRequest((object) "Invalid Request: Minutes must be positive and less than 1440");
    List<APIDataMessage> apiDataMessageList = new List<APIDataMessage>();
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor != null && !sensor.IsDeleted && MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID) && MonnitSession.CurrentCustomer.CanSeeSensor(sensor) && !MonnitSession.CurrentCustomer.Account.HideData)
    {
      if (minutes == 0)
      {
        DataMessage dm = DataMessage.LoadLastBySensorQuickCached(sensor);
        int num;
        if (dm != null)
        {
          Guid dataMessageGuid = dm.DataMessageGUID;
          Guid? nullable = lastDataMessageGUID;
          num = nullable.HasValue ? (dataMessageGuid != nullable.GetValueOrDefault() ? 1 : 0) : 1;
        }
        else
          num = 0;
        if (num != 0)
          apiDataMessageList.Add(new APIDataMessage(sensor, dm));
      }
      else
      {
        foreach (DataMessage dm in DataMessage.LoadRecentBySensor(sensor.SensorID, minutes, lastDataMessageGUID ?? Guid.Empty))
          apiDataMessageList.Add(new APIDataMessage(sensor, dm));
      }
    }
    return this.FormatRequest((object) apiDataMessageList);
  }

  [AuthorizeAPIPremier]
  public ActionResult AccountRecentDataMessages(
    long? accountID,
    int minutes,
    long? networkID,
    Guid? lastDataMessageGUID)
  {
    if (minutes > 120 || minutes < 0)
      return this.FormatRequest((object) "Invalid Request: Minutes must be positive and less than 120");
    long? nullable1 = accountID;
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(Account.Load(nullable1 ?? MonnitSession.CurrentCustomer.AccountID)))
      return this.FormatRequest((object) "Invalid Request: no account found");
    List<APIDataMessage> apiDataMessageList = new List<APIDataMessage>();
    List<Sensor> source;
    if (!networkID.HasValue)
    {
      nullable1 = accountID;
      source = Sensor.LoadByAccountID(nullable1 ?? MonnitSession.CurrentCustomer.AccountID);
    }
    else
    {
      CSNet csNet = CSNet.Load(networkID.GetValueOrDefault());
      if (csNet == null || !MonnitSession.CurrentCustomer.CanSeeAccount(csNet.AccountID))
        return this.FormatRequest((object) "Invalid NetworkID");
      source = Sensor.LoadByCsNetID(networkID.GetValueOrDefault());
    }
    DateTime dateTime = DateTime.MinValue;
    Guid? nullable2;
    if (lastDataMessageGUID.HasValue)
    {
      nullable2 = lastDataMessageGUID;
      DataMessage msg = DataMessage.LoadByGuid(nullable2 ?? Guid.Empty);
      if (msg != null && source.Where<Sensor>((System.Func<Sensor, bool>) (s => s.SensorID == msg.SensorID)).Count<Sensor>() > 0)
        dateTime = msg.InsertDate;
    }
    foreach (Sensor sensor in source)
    {
      if (!MonnitSession.CurrentCustomer.Account.HideData && sensor != null && MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID) && MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      {
        if (minutes == 0)
        {
          DataMessage lastDataMessage = sensor.LastDataMessage;
          if (lastDataMessage != null && lastDataMessage.InsertDate > dateTime)
            apiDataMessageList.Add(new APIDataMessage(sensor, lastDataMessage));
        }
        else
        {
          long sensorId = sensor.SensorID;
          int minutes1 = minutes;
          nullable2 = lastDataMessageGUID;
          Guid lastDataMessageGUID1 = nullable2 ?? Guid.Empty;
          foreach (DataMessage dm in DataMessage.LoadRecentBySensor(sensorId, minutes1, lastDataMessageGUID1))
            apiDataMessageList.Add(new APIDataMessage(sensor, dm));
        }
      }
    }
    return this.FormatRequest((object) apiDataMessageList);
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorChartMessages(long sensorID, DateTime fromDate, DateTime toDate)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID) || !MonnitSession.CustomerCan("Sensor_View_History"))
      return this.FormatRequest((object) "Invalid SensorID");
    List<APIChartDataPoint> apiChartDataPointList = new List<APIChartDataPoint>();
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && sensor.StartDate != DateTime.MinValue && sensor.StartDate.Ticks > fromDate.Ticks)
      fromDate = sensor.StartDate;
    if (!MonnitSession.CurrentCustomer.Account.HideData)
      apiChartDataPointList = sensor.MonnitApplication.IsTriggerProfile != eApplicationProfileType.Interval ? DataMessage.CountByDay_LoadForChart(sensor.SensorID, fromDate, toDate, 75).Select<DataPoint, APIChartDataPoint>((System.Func<DataPoint, APIChartDataPoint>) (dp => new APIChartDataPoint(dp))).ToList<APIChartDataPoint>() : (sensor.ApplicationID == 14L ? DataMessage.CountByDay_LoadForChart(sensor.SensorID, fromDate, toDate, 75).Select<DataPoint, APIChartDataPoint>((System.Func<DataPoint, APIChartDataPoint>) (dp => new APIChartDataPoint(dp))).ToList<APIChartDataPoint>() : DataMessage.LoadForChart(sensor.SensorID, fromDate, toDate, 75).Select<DataMessage, APIChartDataPoint>((System.Func<DataMessage, APIChartDataPoint>) (dm => new APIChartDataPoint(dm))).ToList<APIChartDataPoint>());
    return this.FormatRequest((object) apiChartDataPointList);
  }

  [AuthorizeAPIPremier]
  public ActionResult ConvergintLocationAggregation(long gatewayID)
  {
    Gateway gateway = Gateway.Load(gatewayID);
    if (gateway == null)
      return this.FormatRequest((object) "Invalid NetworkID");
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    if (csNet == null || csNet.HoldingOnlyNetwork || !MonnitSession.CurrentCustomer.CanSeeAccount(csNet.AccountID) || !MonnitSession.CurrentCustomer.CanSeeNetwork(csNet.CSNetID))
      return this.FormatRequest((object) "Invalid NetworkID");
    Dictionary<long, Sensor> dictionary1 = new Dictionary<long, Sensor>();
    Dictionary<long, List<DataMessage>> dictionary2 = new Dictionary<long, List<DataMessage>>();
    foreach (Sensor sensor in (IEnumerable<Sensor>) Sensor.LoadByCsNetID(csNet.CSNetID).OrderBy<Sensor, string>((System.Func<Sensor, string>) (m => m.SensorName)))
    {
      if (!dictionary1.ContainsKey(sensor.SensorID))
      {
        dictionary1.Add(sensor.SensorID, sensor);
        dictionary2.Add(sensor.SensorID, new List<DataMessage>());
      }
    }
    foreach (DataMessage dataMessage in DataMessage.LoadByNetwork(csNet.CSNetID, DateTime.UtcNow.AddHours(-1.0), DateTime.UtcNow, 5000))
    {
      Sensor sensor = dictionary1[dataMessage.SensorID];
      if (sensor != null)
        dictionary2[sensor.SensorID].Add(dataMessage);
    }
    long timeZoneId = MonnitSession.CurrentCustomer.Account.TimeZoneID;
    SensorRestAPI data = new SensorRestAPI();
    data.Method = this.RouteData.Values["action"].ToString();
    XElement xelement = new XElement((XName) "Network");
    XElement content1 = new XElement((XName) "Gateway");
    content1.Add((object) new XAttribute((XName) "GatewayID", (object) gateway.GatewayID.ToString()));
    content1.Add((object) new XAttribute((XName) "Name", (object) gateway.Name));
    xelement.Add((object) content1);
    XElement content2 = new XElement((XName) "LastCommunicationDate");
    content2.SetValue((object) Monnit.TimeZone.GetLocalTimeById(gateway.LastCommunicationDate, timeZoneId));
    content1.Add((object) content2);
    XElement content3 = new XElement((XName) "LinePower");
    content3.SetValue((object) (gateway.CurrentPower <= 2 ? 1 : 0));
    content1.Add((object) content3);
    XElement content4 = new XElement((XName) "BatteryPercent");
    GatewayMessage gatewayMessage = GatewayMessage.LoadLastByGateway(gateway.GatewayID);
    try
    {
      if (gatewayMessage.Power < 2)
        content4.SetValue((object) 100);
      else if (gatewayMessage.Power == 2)
        content4.SetValue((object) 0);
      else
        content4.SetValue((object) (gatewayMessage.Battery < 100 ? gatewayMessage.Battery : 100));
    }
    catch (Exception ex)
    {
      ex.Log($"APIController.ConvergintLocationAggregation(), GatewayID=[{gatewayID}]" + gatewayMessage?.ToString() == null ? "GatewayMessage.LoadLastByGateway(gateway.GatewayID) == null" : "");
    }
    content1.Add((object) content4);
    XElement content5 = new XElement((XName) "SignalPercent");
    content5.SetValue((object) gateway.CurrentSignalStrength);
    content1.Add((object) content5);
    foreach (long key in dictionary1.Keys)
    {
      Sensor sensor = dictionary1[key];
      XElement content6 = new XElement((XName) "Sensor");
      content6.Add((object) new XAttribute((XName) "SensorID", (object) sensor.SensorID.ToString()));
      content6.Add((object) new XAttribute((XName) "Name", (object) sensor.SensorName));
      xelement.Add((object) content6);
      foreach (DataMessage dataMessage in dictionary2[key])
      {
        XElement content7 = new XElement((XName) "Reading");
        content6.Add((object) content7);
        XElement content8 = new XElement((XName) "MessageDate");
        content8.SetValue((object) Monnit.TimeZone.GetLocalTimeById(dataMessage.MessageDate, timeZoneId));
        content7.Add((object) content8);
        XElement content9 = new XElement((XName) "Reading");
        content9.SetValue((object) dataMessage.PlotValueString);
        content7.Add((object) content9);
        XElement content10 = new XElement((XName) "Display");
        content10.SetValue((object) dataMessage.DisplayData);
        content7.Add((object) content10);
        XElement content11 = new XElement((XName) "BatteryPercent");
        content11.SetValue((object) dataMessage.Battery);
        content7.Add((object) content11);
        XElement content12 = new XElement((XName) "SignalPercent");
        content12.SetValue((object) DataMessage.GetSignalStrengthPercent(sensor.GenerationType, sensor.SensorTypeID, dataMessage.SignalStrength));
        content7.Add((object) content12);
      }
    }
    data.Result = (object) xelement;
    return (ActionResult) this.Xml((object) data);
  }

  [AuthorizeAPI]
  public ActionResult AccountNotificationList(long? accountID)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    List<APINotification> apiNotificationList = new List<APINotification>();
    List<Notification> notificationList = Notification.LoadByAccountID(acct.AccountID);
    if (notificationList.Count < 1)
      return this.FormatRequest((object) "Invalid Request: there are no Notifications for this account");
    foreach (Notification noti in notificationList)
      apiNotificationList.Add(new APINotification(noti));
    return this.FormatRequest((object) apiNotificationList);
  }

  [AuthorizeAPI]
  public ActionResult SensorNotificationList(long sensorID)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor != null && !sensor.IsDeleted && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return this.FormatRequest((object) "Invalid Request: no sensor found");
    List<APINotification> apiNotificationList = new List<APINotification>();
    List<Notification> notificationList = Notification.LoadBySensorID(sensor.SensorID);
    if (notificationList.Count < 1)
      return this.FormatRequest((object) "Invalid Request: there are no Notifications for this sensor");
    foreach (Notification noti in notificationList)
      apiNotificationList.Add(new APINotification(noti));
    return this.FormatRequest((object) apiNotificationList);
  }

  [AuthorizeAPIPremier]
  public ActionResult RecentlySentNotifications(
    int minutes,
    long? lastSentNotificationID,
    long? sensorID)
  {
    if (minutes > 720 || minutes <= 0)
      return this.FormatRequest((object) "Invalid Request: Minutes must be positive and less then 720");
    if (!lastSentNotificationID.HasValue)
      lastSentNotificationID = new long?(0L);
    List<APISentNotification> sentNotificationList = new List<APISentNotification>();
    List<Sensor> sensorList;
    if (!sensorID.HasValue)
    {
      sensorList = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
    }
    else
    {
      sensorList = new List<Sensor>();
      Sensor sensor = Sensor.Load(sensorID ?? long.MinValue);
      if (sensor != null)
        sensorList.Add(sensor);
    }
    foreach (Sensor sensor in sensorList)
    {
      if (MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      {
        DateTime utcNow = DateTime.UtcNow;
        if (minutes == 0)
        {
          NotificationRecorded notiRec = NotificationRecorded.SentNotificationsLoadedBySensorID(sensor.SensorID, utcNow.AddHours(-12.0), utcNow, 1).LastOrDefault<NotificationRecorded>();
          if (notiRec.NotificationRecordedID > lastSentNotificationID.Value)
            sentNotificationList.Add(new APISentNotification(notiRec));
        }
        else
        {
          foreach (NotificationRecorded notiRec in NotificationRecorded.SentNotificationsLoadedBySensorID(sensor.SensorID, utcNow.AddMinutes((double) minutes * -1.0), utcNow, 5000))
          {
            if (notiRec.NotificationRecordedID > lastSentNotificationID.Value)
              sentNotificationList.Add(new APISentNotification(notiRec));
          }
        }
      }
    }
    return this.FormatRequest((object) sentNotificationList);
  }

  [AuthorizeAPIPremier]
  public ActionResult SentNotifications(DateTime from, DateTime to, long? sensorID)
  {
    if (from == DateTime.MinValue)
      return this.FormatRequest((object) "Invalid Request: from has a bad datetime value");
    if (to == DateTime.MinValue)
      return this.FormatRequest((object) "Invalid Request: to has a bad datetime value");
    TimeSpan timeSpan = to.Subtract(from);
    int num;
    if (timeSpan.Days <= 7)
    {
      timeSpan = to.Subtract(from);
      num = timeSpan.Days < 0 ? 1 : 0;
    }
    else
      num = 1;
    if (num != 0)
      return this.FormatRequest((object) "Invalid Request: You can only go back 7 days and the from date must be less then the to date.");
    List<APISentNotification> sentNotificationList = new List<APISentNotification>();
    List<Sensor> sensorList;
    if (!sensorID.HasValue)
    {
      sensorList = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
    }
    else
    {
      sensorList = new List<Sensor>();
      Sensor sensor = Sensor.Load(sensorID ?? long.MinValue);
      if (sensor != null)
        sensorList.Add(sensor);
    }
    foreach (Sensor sensor in sensorList)
    {
      if (MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      {
        foreach (NotificationRecorded notiRec in NotificationRecorded.SentNotificationsLoadedBySensorID(sensor.SensorID, from, to, 5000))
        {
          if (notiRec.NotificationID != long.MinValue)
            sentNotificationList.Add(new APISentNotification(notiRec));
        }
      }
    }
    return this.FormatRequest((object) sentNotificationList);
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationRecipientList(long notificationID)
  {
    if (notificationID == long.MinValue)
      return this.FormatRequest((object) "Invalid Request: must contain a valid notificationID");
    List<APINotificationRecipient> notificationRecipientList = new List<APINotificationRecipient>();
    Notification notification = Notification.Load(notificationID);
    if (notification == null || !MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      return this.FormatRequest((object) "Invalid Request: there are no notifications that have that notificationID");
    if (notification.NotificationRecipients.Count < 1)
      return this.FormatRequest((object) "Invalid Request: there are no notification recipients for this NotificationID");
    foreach (NotificationRecipient notificationRecipient in notification.NotificationRecipients)
    {
      if (notificationRecipient.NotificationType == eNotificationType.Email || notificationRecipient.NotificationType == eNotificationType.Phone || notificationRecipient.NotificationType == eNotificationType.SMS || notificationRecipient.NotificationType == eNotificationType.Both)
        notificationRecipientList.Add(new APINotificationRecipient(notificationRecipient));
    }
    return notificationRecipientList.Count < 1 ? this.FormatRequest((object) "Invalid Request: there are no recipients for this notification") : this.FormatRequest((object) notificationRecipientList);
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationSystemActionList(long notificationID)
  {
    if (notificationID == long.MinValue)
      return this.FormatRequest((object) "Invalid Request: must contain a valid notificationID");
    List<APINotificationSystemAction> notificationSystemActionList = new List<APINotificationSystemAction>();
    Notification notification = Notification.Load(notificationID);
    if (notification == null || !MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      return this.FormatRequest((object) "Invalid Request: there are no notifications that have that notificationID");
    if (notification.NotificationRecipients.Count < 1)
      return this.FormatRequest((object) "Invalid Request: there are no System Actions for this NotificationID");
    foreach (DataRow row in (InternalDataCollectionBase) NotificationRecipient.LoadActionToExecute(notificationID).Rows)
      notificationSystemActionList.Add(new APINotificationSystemAction()
      {
        ActionType = row[0].ToString(),
        DelayMinutes = row[1].ToInt() < 0 ? 0 : row[1].ToInt(),
        TargetNotificationID = row[4].ToString(),
        SystemActionRecipientID = row[5].ToLong()
      });
    return notificationSystemActionList.Count < 1 ? this.FormatRequest((object) "Invalid Request: there are no System Actions for this notification") : this.FormatRequest((object) notificationSystemActionList);
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationControlUnitList(long notificationID)
  {
    if (notificationID == long.MinValue)
      return this.FormatRequest((object) "Invalid Request: must contain a valid notificationID");
    List<APINotificationControlUnit> notificationControlUnitList = new List<APINotificationControlUnit>();
    Notification notification = Notification.Load(notificationID);
    if (notification == null || !MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      return this.FormatRequest((object) "Invalid Request: there are no notifications that have that notificationID");
    if (notification.NotificationRecipients.Count < 1)
      return this.FormatRequest((object) "Invalid Request: there are no Control Units for this NotificationID");
    foreach (NotificationRecipient notificationRecipient in notification.NotificationRecipients)
    {
      if (notificationRecipient.NotificationType == eNotificationType.Control)
        notificationControlUnitList.Add(new APINotificationControlUnit(notificationRecipient));
    }
    return notificationControlUnitList.Count < 1 ? this.FormatRequest((object) "Invalid Request: there are no Control Units for this notification") : this.FormatRequest((object) notificationControlUnitList);
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationLocalNotifierList(long notificationID)
  {
    if (notificationID == long.MinValue)
      return this.FormatRequest((object) "Invalid Request: must contain a valid notificationID");
    List<APINotificationLocalNotifier> notificationLocalNotifierList = new List<APINotificationLocalNotifier>();
    Notification notification = Notification.Load(notificationID);
    if (notification == null || !MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      return this.FormatRequest((object) "Invalid Request: there are no notifications that have that notificationID");
    if (notification.NotificationRecipients.Count < 1)
      return this.FormatRequest((object) "Invalid Request: there are no Local Notifiers for this NotificationID");
    foreach (NotificationRecipient notificationRecipient in notification.NotificationRecipients)
    {
      if (notificationRecipient.NotificationType == eNotificationType.Local_Notifier)
        notificationLocalNotifierList.Add(new APINotificationLocalNotifier(notificationRecipient));
    }
    return notificationLocalNotifierList.Count < 1 ? this.FormatRequest((object) "Invalid Request: there are no Local Notifiers for this notification") : this.FormatRequest((object) notificationLocalNotifierList);
  }

  [AuthorizeAPIPremier]
  public ActionResult SensorAssignedToNotificaiton(long notificationID)
  {
    if (notificationID < 1L)
      return this.FormatRequest((object) "Invalid Request: must contain a valid notificationID");
    List<APISensor> apiSensorList = new List<APISensor>();
    Notification notification = Notification.Load(notificationID);
    if (notification == null || !MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      return this.FormatRequest((object) "Invalid Request: there are no sensors set to that notification");
    if (notification.SensorsThatNotify.Count < 1)
      return this.FormatRequest((object) "Invalid Request: there are no sensors set to that notification");
    foreach (SensorDatum sdat in notification.SensorsThatNotify)
      apiSensorList.Add(new APISensor(sdat));
    return this.FormatRequest((object) apiSensorList);
  }

  [AuthorizeAPIPremier]
  public ActionResult GatewayAssignedToNotificaiton(long notificationID)
  {
    if (notificationID < 1L)
      return this.FormatRequest((object) "Invalid Request: must contain a valid notificationID");
    List<APIGateway> apiGatewayList = new List<APIGateway>();
    Notification notification = Notification.Load(notificationID);
    if (notification == null || !MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      return this.FormatRequest((object) "Invalid Request: there are no gateways set to that notification");
    if (notification.GatewaysThatNotify.Count < 1)
      return this.FormatRequest((object) "Invalid Request: there are no gateways set to that notification");
    foreach (Gateway g in notification.GatewaysThatNotify)
    {
      g.SuppressPropertyChangedEvent = false;
      apiGatewayList.Add(new APIGateway(g));
    }
    return this.FormatRequest((object) apiGatewayList);
  }

  [AuthorizeAPIPremier]
  public ActionResult ToggleNotification(long notificationID, bool on)
  {
    try
    {
      if (notificationID == long.MinValue)
        return this.FormatRequest((object) "Invalid Request: must contain a valid notificationID");
      Notification DBObject = Notification.Load(notificationID);
      if (DBObject == null || !MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
        return this.FormatRequest((object) "Invalid Request: there are no notifications that have that notificationID");
      Account account = Account.Load(DBObject.AccountID);
      if (account != null)
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Toggled rule active status");
      DBObject.IsActive = on;
      DBObject.Save();
      return this.FormatRequest((object) "Success");
    }
    catch
    {
      return this.FormatRequest((object) "Error: failed");
    }
  }

  [AuthorizeAPIPremier]
  public ActionResult RecentResellerNotification(int minutes, long? lastSentNotificationID)
  {
    return minutes > 720 || minutes <= 0 ? this.FormatRequest((object) "Invalid Request: Minutes must be positive and less then 720") : this.FormatRequest((object) NotificationRecorded.LoadRecentForReseller(MonnitSession.CurrentCustomer.AccountID, minutes, lastSentNotificationID.GetValueOrDefault()).Select<NotificationRecorded, APISentNotification>((System.Func<NotificationRecorded, APISentNotification>) (n => new APISentNotification(n))).ToList<APISentNotification>());
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationAssignGateway(long notificationID, long gatewayID)
  {
    Gateway gateway = Gateway.Load(gatewayID);
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    Notification DBObject = Notification.Load(notificationID);
    if (gateway == null || gateway != null && gateway.IsDeleted)
      return this.FormatRequest((object) "Gateway not found");
    if (DBObject == null)
      return this.FormatRequest((object) "Notification not found");
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID) || !MonnitSession.CurrentCustomer.CanSeeAccount(csNet.AccountID))
      return this.FormatRequest((object) "Unauthorized");
    if (DBObject.AccountID != csNet.AccountID)
      return this.FormatRequest((object) "Gateway must be on same account as rule");
    if (DBObject.NotificationClass == eNotificationClass.Timed || DBObject.NotificationClass == eNotificationClass.Application)
      return this.FormatRequest((object) "Incompatible Rule Type.");
    if (DBObject.NotificationClass == eNotificationClass.Low_Battery && (gateway.PowerSource == null || gateway.PowerSource.VoltageForZeroPercent == gateway.PowerSource.VoltageForOneHundredPercent))
      return this.FormatRequest((object) "Incompatible Rule Type.");
    AdvancedNotification advancedNotification = AdvancedNotification.Load(DBObject.AdvancedNotificationID);
    if (advancedNotification != null && !advancedNotification.HasGatewayList)
      return this.FormatRequest((object) "Incompatible Rule Type.");
    if (DBObject.GatewaysThatNotify.Where<Gateway>((System.Func<Gateway, bool>) (g => g.GatewayID == gateway.GatewayID)).Count<Gateway>() < 1)
    {
      Account account = Account.Load(DBObject.AccountID);
      if (account != null)
      {
        string changeRecord = $"{{\"GatewayID\": \"{gateway.GatewayID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
        DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned gateway to rule");
      }
      DBObject.AddGateway(gateway);
    }
    List<APIGateway> apiGatewayList = new List<APIGateway>();
    foreach (Gateway g in DBObject.GatewaysThatNotify)
    {
      g.SuppressPropertyChangedEvent = false;
      apiGatewayList.Add(new APIGateway(g));
    }
    return this.FormatRequest((object) apiGatewayList);
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationRemoveGateway(long notificationID, long gatewayID)
  {
    Gateway gateway = Gateway.Load(gatewayID);
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    Notification DBObject = Notification.Load(notificationID);
    if (gateway == null || gateway != null && gateway.IsDeleted)
      return this.FormatRequest((object) "Gateway not found");
    if (DBObject == null)
      return this.FormatRequest((object) "Notification not found");
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID) || !MonnitSession.CurrentCustomer.CanSeeAccount(csNet.AccountID))
      return this.FormatRequest((object) "Unauthorized");
    if (DBObject.GatewaysThatNotify.Where<Gateway>((System.Func<Gateway, bool>) (g => g.GatewayID == gateway.GatewayID)).Count<Gateway>() > 0)
    {
      Account account = Account.Load(DBObject.AccountID);
      if (account != null)
      {
        string changeRecord = $"{{\"GatewayID\": \"{gateway.GatewayID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
        DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Removed gateway from rule");
      }
      DBObject.RemoveGateway(gateway);
    }
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationAssignSensor(long notificationid, long sensorID, int index)
  {
    Notification DBObject = Notification.Load(notificationid);
    if (DBObject == null)
      return this.FormatRequest((object) "Notification not found");
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor != null && sensor.IsDeleted)
      return this.FormatRequest((object) "Sensor not found");
    CSNet csNet = CSNet.Load(sensor.CSNetID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID) || !MonnitSession.CurrentCustomer.CanSeeAccount(csNet.AccountID))
      return this.FormatRequest((object) "Unauthorized");
    if (DBObject.AccountID != csNet.AccountID)
      return this.FormatRequest((object) "Sensor must be on same account as notification");
    if (DBObject.CanAddSensor(sensorID, index) && DBObject.SensorsThatNotify.Where<SensorDatum>((System.Func<SensorDatum, bool>) (m => m.sens.SensorID == sensorID && m.DatumIndex == index)).Count<SensorDatum>() < 1 && !DBObject.SensorsThatNotify.Contains(new SensorDatum(sensor, index, long.MinValue)))
    {
      Account account = Account.Load(DBObject.AccountID);
      if (account != null)
      {
        string changeRecord = $"{{\"SensorID\": \"{sensor.SensorID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
        DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned sensor to rule");
      }
      DBObject.AddSensor(sensor, index);
    }
    List<APISensor> apiSensorList = new List<APISensor>();
    foreach (SensorDatum sdat in DBObject.SensorsThatNotify)
      apiSensorList.Add(new APISensor(sdat));
    return this.FormatRequest((object) apiSensorList);
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationRemoveSensor(long notificationid, long sensorID, int index)
  {
    Notification DBObject = Notification.Load(notificationid);
    if (DBObject == null)
      return this.FormatRequest((object) "Notification not found");
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor != null && sensor.IsDeleted)
      return this.FormatRequest((object) "Sensor not found");
    CSNet csNet = CSNet.Load(sensor.CSNetID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID) || !MonnitSession.CurrentCustomer.CanSeeAccount(csNet.AccountID))
      return this.FormatRequest((object) "Unauthorized");
    if (DBObject.SensorsThatNotify.Where<SensorDatum>((System.Func<SensorDatum, bool>) (m => m.sens.SensorID == sensorID)).Count<SensorDatum>() < 1)
      return this.FormatRequest((object) "Sensor not assigned to notification");
    if (DBObject.SensorsThatNotify.Where<SensorDatum>((System.Func<SensorDatum, bool>) (m => m.sens.SensorID == sensorID && m.DatumIndex == index)).Count<SensorDatum>() <= 0)
      return this.FormatRequest((object) "Error: Invalid Datum Index");
    Account account = Account.Load(DBObject.AccountID);
    if (account != null)
    {
      string changeRecord = $"{{\"SensorID\": \"{sensor.SensorID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
      DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Remove sensor from rule");
    }
    DBObject.RemoveSensor(sensor, index);
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationAssignControlUnit(
    long notificationID,
    long deviceID,
    string Relay1State,
    string Relay2State,
    int Relay1Time,
    int Relay2Time)
  {
    Notification DBObject = Notification.Load(notificationID);
    if (DBObject == null)
      return this.FormatRequest((object) "Notification not found");
    Sensor sensor = Sensor.Load(deviceID);
    if (sensor == null)
      return this.FormatRequest((object) "Device not found");
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID) || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Unauthorized Notification");
    if (sensor.AccountID != DBObject.AccountID)
      return this.FormatRequest((object) "Control Unit must be on same account as rule");
    if (Relay1Time == int.MinValue)
      Relay1Time = 0;
    if (Relay2Time == int.MinValue)
      Relay2Time = 0;
    int state1 = 0;
    switch (Relay1State.ToLower())
    {
      case "inactive":
        state1 = 0;
        break;
      case "off":
        state1 = 1;
        break;
      case "on":
        state1 = 2;
        break;
      case "toggle":
        state1 = 3;
        break;
    }
    int state2 = 0;
    switch (Relay1State.ToLower())
    {
      case "inactive":
        state2 = 0;
        break;
      case "off":
        state2 = 1;
        break;
      case "on":
        state2 = 2;
        break;
      case "toggle":
        state2 = 3;
        break;
    }
    NotificationRecipient notiRecip = DBObject.NotificationRecipients.Where<NotificationRecipient>((System.Func<NotificationRecipient, bool>) (c => c.DeviceToNotifyID == deviceID)).FirstOrDefault<NotificationRecipient>();
    if (DBObject.NotificationRecipients.Where<NotificationRecipient>((System.Func<NotificationRecipient, bool>) (m => m.DeviceToNotifyID == deviceID)).Count<NotificationRecipient>() < 1)
    {
      Account account = Account.Load(DBObject.AccountID);
      if (account != null)
      {
        string changeRecord = string.Format("{{\"SensorID\": \"{0}\",  ControlUnit : {{ \"State1\" : \"{2}\", \"State2\" : \"{3}\", \"Relay1Time\" : \"{4}\", \"Relay2Time\" : \"{5}\" }}  , \"NotifcationID\" : \"{1}\" }} ", (object) sensor.SensorID, (object) DBObject.NotificationID, (object) state1, (object) state2, (object) Relay1Time, (object) Relay2Time);
        DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned control unit to rule");
      }
      notiRecip = DBObject.AddSensorRecipient(sensor, Control_1.CreateSerializedRecipientProperties(state1, state2, Relay1Time, Relay2Time));
    }
    DBObject.HasControlAction = true;
    DBObject.Save();
    return this.FormatRequest((object) new APINotificationControlUnit(notiRecip));
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationRemoveControlUnit(long controlUnitRecipientID)
  {
    NotificationRecipient recipient = NotificationRecipient.Load(controlUnitRecipientID);
    if (recipient == null)
      return this.FormatRequest((object) "Control Unit not found");
    Notification DBObject = Notification.Load(recipient.NotificationID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
      return this.FormatRequest((object) "Unauthorized Notification");
    Account account = Account.Load(DBObject.AccountID);
    if (account != null)
    {
      string changeRecord = $"{{\"SensorID\": \"{recipient.DeviceToNotifyID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
      DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Remove control unit from rule");
    }
    DBObject.RemoveRecipient(recipient);
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationAssignLocalNotifier(
    long notificationID,
    long deviceID,
    bool LED_ON,
    bool Buzzer_ON,
    bool AutoScroll_ON,
    bool BackLight_ON)
  {
    Notification DBObject = Notification.Load(notificationID);
    if (DBObject == null)
      return this.FormatRequest((object) "Notification not found");
    Sensor sensor = Sensor.Load(deviceID);
    if (sensor == null)
      return this.FormatRequest((object) "Device not found");
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID) || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Unauthorized Notification");
    if (DBObject.AccountID != sensor.AccountID)
      return this.FormatRequest((object) "Local Notifier must be on same account as notification");
    NotificationRecipient notiRecip = DBObject.NotificationRecipients.Where<NotificationRecipient>((System.Func<NotificationRecipient, bool>) (c => c.DeviceToNotifyID == deviceID)).FirstOrDefault<NotificationRecipient>();
    if (DBObject.NotificationRecipients.Where<NotificationRecipient>((System.Func<NotificationRecipient, bool>) (m => m.DeviceToNotifyID == deviceID)).Count<NotificationRecipient>() < 1)
    {
      Account account = Account.Load(DBObject.AccountID);
      if (account != null)
      {
        string changeRecord = string.Format("{{\"SensorID\": \"{0}\",  LocalNotifier : {{ \"LED ON\" : \"{2}\", \"Buzzer ON\" : \"{3}\", \"AutoScroll ON\" : \"{4}\", \"BackLight ON\" : \"{5}\", \"Sensor Name\" : \"{6}\" }}  , \"NotifcationID\" : \"{1}\" }} ", (object) sensor.SensorID, (object) DBObject.NotificationID, (object) LED_ON, (object) Buzzer_ON, (object) AutoScroll_ON, (object) BackLight_ON, (object) sensor.SensorName);
        DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned local notifier to rule");
      }
      notiRecip = DBObject.AddSensorRecipient(sensor, Attention.CreateSerializedRecipientProperties(LED_ON, Buzzer_ON, AutoScroll_ON, BackLight_ON, sensor.SensorName));
    }
    DBObject.HasLocalAlertAction = true;
    DBObject.Save();
    return this.FormatRequest((object) new APINotificationLocalNotifier(notiRecip));
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationRemoveLocalNotifier(long localNotifierRecipientID)
  {
    NotificationRecipient recipient = NotificationRecipient.Load(localNotifierRecipientID);
    if (recipient == null)
      return this.FormatRequest((object) "Local Notifier not found");
    Notification DBObject = Notification.Load(recipient.NotificationID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
      return this.FormatRequest((object) "Unauthorized Notification");
    Account account = Account.Load(DBObject.AccountID);
    if (account != null)
    {
      string changeRecord = $"{{\"SensorID\": \"{recipient.DeviceToNotifyID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
      DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Removed local notifier from rule");
    }
    DBObject.RemoveRecipient(recipient);
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationAssignRecipient(
    long notificationID,
    long userID,
    int delayMinutes,
    string notificationType)
  {
    Notification notification = Notification.Load(notificationID);
    if (notification == null)
      return this.FormatRequest((object) "Notification not found");
    Customer cust = Customer.Load(userID);
    if (cust == null)
      return this.FormatRequest((object) "User not found");
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      return this.FormatRequest((object) "Unauthorized");
    if (cust.AccountID != notification.AccountID && CustomerAccountLink.Load(cust.CustomerID, notification.AccountID) == null)
    {
      bool flag = false;
      foreach (Account ancestor in Account.Ancestors(MonnitSession.CurrentCustomer.CustomerID, notification.AccountID))
      {
        if (ancestor.AccountID == cust.AccountID)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return this.FormatRequest((object) "User not found");
    }
    eNotificationType NotiType = (eNotificationType) System.Enum.Parse(typeof (eNotificationType), notificationType, true);
    if (!cust.SendSensorNotificationToText && (NotiType == eNotificationType.SMS || NotiType == eNotificationType.Both))
      return this.FormatRequest((object) "User unable to receive texts: Check Notification details for the user");
    if (!cust.SendSensorNotificationToVoice && NotiType == eNotificationType.Phone)
      return this.FormatRequest((object) "User unable to receive voice calls: Check Notification details for the user");
    Account account = Account.Load(notification.AccountID);
    if (account != null)
    {
      string changeRecord = $"{{\"CustomerID\": \"{cust.CustomerID}\", \"NotifcationID\" : \"{notification.NotificationID}\", \"DelayMinutes\" : \"{delayMinutes}\", \"NotifcationType\" : \"{NotiType.ToString()}\" }} ";
      notification.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned recipient to notification");
    }
    if (NotiType != eNotificationType.Both)
      return this.FormatRequest((object) new APINotificationRecipient(APIController.addRecipient(delayMinutes, notification, cust, NotiType)));
    return this.FormatRequest((object) new List<APINotificationRecipient>()
    {
      new APINotificationRecipient(APIController.addRecipient(delayMinutes, notification, cust, eNotificationType.Email)),
      new APINotificationRecipient(APIController.addRecipient(delayMinutes, notification, cust, eNotificationType.SMS))
    });
  }

  private static NotificationRecipient addRecipient(
    int delayMinutes,
    Notification noti,
    Customer cust,
    eNotificationType NotiType)
  {
    NotificationRecipient notificationRecipient = noti.NotificationRecipients.Where<NotificationRecipient>((System.Func<NotificationRecipient, bool>) (m => m.CustomerToNotifyID == cust.CustomerID && m.NotificationType == NotiType)).FirstOrDefault<NotificationRecipient>();
    if (notificationRecipient == null)
    {
      notificationRecipient = noti.AddCustomer(cust, NotiType);
      notificationRecipient.NotificationID = noti.NotificationID;
    }
    notificationRecipient.DelayMinutes = delayMinutes;
    notificationRecipient.Save();
    return notificationRecipient;
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationRemoveRecipient(long recipientID)
  {
    NotificationRecipient recipient = NotificationRecipient.Load(recipientID);
    if (recipient == null)
      return this.FormatRequest((object) "Recipient not found");
    Notification DBObject = Notification.Load(recipient.NotificationID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
      return this.FormatRequest((object) "Unauthorized Notification");
    Account account = Account.Load(DBObject.AccountID);
    if (account != null)
    {
      string changeRecord = $"{{\"CustomerID\": \"{recipient.CustomerToNotifyID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
      DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Removed recipient from notification");
    }
    DBObject.RemoveRecipient(recipient);
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationAssignSystemAction(
    long notificationID,
    string actionType,
    long targetNotificationID,
    int delayMinutes)
  {
    Notification DBObject = Notification.Load(notificationID);
    Notification notification = Notification.Load(targetNotificationID);
    if (DBObject == null)
      return this.FormatRequest((object) "Notification not found");
    if (notification == null)
      return this.FormatRequest((object) "Target Notification not found");
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID) || !MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      return this.FormatRequest((object) "Unauthorized");
    if (DBObject.AccountID != notification.AccountID)
      return this.FormatRequest((object) "Invalid Request: Target Notification must be in the same account");
    ActionToExecute action = ActionToExecute.Find(actionType);
    if (action == null)
      return this.FormatRequest((object) "Please enter Valid Action Type");
    NotificationRecipient notiRecip = DBObject.NotificationRecipients.Where<NotificationRecipient>((System.Func<NotificationRecipient, bool>) (m => m.ActionToExecuteID == action.ActionToExecuteID && m.SerializedRecipientProperties.ToLong() == targetNotificationID)).FirstOrDefault<NotificationRecipient>();
    if (notiRecip == null)
    {
      notiRecip = new NotificationRecipient();
      notiRecip.NotificationID = notificationID;
      notiRecip.ActionToExecuteID = action.ActionToExecuteID;
      notiRecip.SerializedRecipientProperties = targetNotificationID.ToString();
      notiRecip.NotificationType = eNotificationType.SystemAction;
    }
    Account account = Account.Load(DBObject.AccountID);
    if (account != null)
    {
      string changeRecord = string.Format("{{\"NotifcationID\" : \"{1}\", \"ActionName\": \"{0}\", \"DelayMinutes\" : \"{2}\", \"TargetNotifcationID\" : \"{3}\" }} ", (object) action.Name, (object) DBObject.NotificationID, (object) delayMinutes, (object) targetNotificationID);
      DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned system action to rule");
    }
    notiRecip.DelayMinutes = delayMinutes;
    notiRecip.Save();
    DBObject.HasSystemAction = true;
    DBObject.Save();
    return this.FormatRequest((object) new APINotificationSystemAction(notiRecip, action.Name, notiRecip.SerializedRecipientProperties));
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationRemoveSystemAction(long systemActionRecipientID)
  {
    NotificationRecipient recipient = NotificationRecipient.Load(systemActionRecipientID);
    if (recipient == null)
      return this.FormatRequest((object) "System Action not found");
    Notification DBObject = Notification.Load(recipient.NotificationID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
      return this.FormatRequest((object) "Unauthorized System Action");
    Account account = Account.Load(DBObject.AccountID);
    if (account != null)
    {
      string changeRecord = $"{{\"systemActionRecipientID\": \"{recipient.NotificationRecipientID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
      DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Removed system action from rule");
    }
    DBObject.RemoveRecipient(recipient);
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationScheduleList(long notificationID, string day)
  {
    Notification notification = Notification.Load(notificationID);
    if (notification == null || !MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      return this.FormatRequest((object) "Invalid Request: no Notification found");
    List<APINotificationSchedule> notificationScheduleList = new List<APINotificationSchedule>();
    if (!string.IsNullOrWhiteSpace(day))
    {
      switch (day.ToLower())
      {
        case "friday":
          notificationScheduleList.Add(new APINotificationSchedule(notification.MondaySchedule));
          break;
        case "monday":
          notificationScheduleList.Add(new APINotificationSchedule(notification.MondaySchedule));
          break;
        case "saturday":
          notificationScheduleList.Add(new APINotificationSchedule(notification.MondaySchedule));
          break;
        case "sunday":
          notificationScheduleList.Add(new APINotificationSchedule(notification.MondaySchedule));
          break;
        case "thursday":
          notificationScheduleList.Add(new APINotificationSchedule(notification.MondaySchedule));
          break;
        case "tuesday":
          notificationScheduleList.Add(new APINotificationSchedule(notification.MondaySchedule));
          break;
        case "wednesday":
          notificationScheduleList.Add(new APINotificationSchedule(notification.MondaySchedule));
          break;
        default:
          return this.FormatRequest((object) ("Invalid Request: there where no Schedule's found for " + day));
      }
    }
    else
    {
      notificationScheduleList.Add(new APINotificationSchedule(notification.MondaySchedule));
      notificationScheduleList.Add(new APINotificationSchedule(notification.TuesdaySchedule));
      notificationScheduleList.Add(new APINotificationSchedule(notification.WednesdaySchedule));
      notificationScheduleList.Add(new APINotificationSchedule(notification.ThursdaySchedule));
      notificationScheduleList.Add(new APINotificationSchedule(notification.FridaySchedule));
      notificationScheduleList.Add(new APINotificationSchedule(notification.SaturdaySchedule));
      notificationScheduleList.Add(new APINotificationSchedule(notification.SundaySchedule));
    }
    return this.FormatRequest((object) notificationScheduleList);
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationDailyScheduleSet(
    long notificationID,
    string day,
    string notificationSchedule,
    TimeSpan? firstEnteredTime,
    TimeSpan? secondEnteredTime)
  {
    Notification DBObject = Notification.Load(notificationID);
    if (DBObject == null || !MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
      return this.FormatRequest((object) "Invalid  notificationID");
    eNotificationDaySchedule notificationDaySchedule = eNotificationDaySchedule.All_Day;
    NotificationSchedule notiSchedule = new NotificationSchedule();
    if (!string.IsNullOrWhiteSpace(day))
    {
      switch (day.ToLower())
      {
        case "friday":
          notiSchedule = DBObject.FridaySchedule;
          break;
        case "monday":
          notiSchedule = DBObject.MondaySchedule;
          break;
        case "saturday":
          notiSchedule = DBObject.SaturdaySchedule;
          break;
        case "sunday":
          notiSchedule = DBObject.SundaySchedule;
          break;
        case "thursday":
          notiSchedule = DBObject.ThursdaySchedule;
          break;
        case "tuesday":
          notiSchedule = DBObject.TuesdaySchedule;
          break;
        case "wednesday":
          notiSchedule = DBObject.WednesdaySchedule;
          break;
        default:
          return this.FormatRequest((object) "Invalid Request");
      }
    }
    if (!string.IsNullOrWhiteSpace(notificationSchedule))
    {
      switch (notificationSchedule.ToLower())
      {
        case "off":
          notificationDaySchedule = eNotificationDaySchedule.Off;
          break;
        case "all_day":
          notificationDaySchedule = eNotificationDaySchedule.All_Day;
          break;
        case "after":
          notificationDaySchedule = eNotificationDaySchedule.After;
          break;
        case "before":
          notificationDaySchedule = eNotificationDaySchedule.Before;
          break;
        case "before_and_after":
          notificationDaySchedule = eNotificationDaySchedule.Before_and_After;
          break;
        case "between":
          notificationDaySchedule = eNotificationDaySchedule.Between;
          break;
        default:
          return this.FormatRequest((object) "Invalid NotificationSchedule");
      }
    }
    Account account = Account.Load(DBObject.AccountID);
    if (account != null)
    {
      string changeRecord = $"{{\"NotifcationID\" : \"{DBObject.NotificationID}\", \"Day\": \"{notiSchedule.DayofWeek}\", \"FirstTime\" : \"{firstEnteredTime.ToStringSafe()}\", \"SecondTime\" : \"{secondEnteredTime.ToStringSafe()}\", \"eNotificationSchedule\" : \"{notificationDaySchedule.ToString()}\" }} ";
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Edited rule active status daily schedule");
    }
    NotificationSchedule notificationSchedule1 = notiSchedule;
    TimeSpan? nullable = firstEnteredTime;
    TimeSpan timeSpan1 = nullable ?? TimeSpan.Zero;
    notificationSchedule1.FirstTime = timeSpan1;
    NotificationSchedule notificationSchedule2 = notiSchedule;
    nullable = secondEnteredTime;
    TimeSpan timeSpan2 = nullable ?? TimeSpan.Zero;
    notificationSchedule2.SecondTime = timeSpan2;
    notiSchedule.NotificationDaySchedule = notificationDaySchedule;
    notiSchedule.Save();
    return this.FormatRequest((object) new APINotificationWeeklySchedule(notiSchedule));
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationYearlyScheduleList(long notificationID)
  {
    Notification notification = Notification.Load(notificationID);
    if (notification == null || !MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      return this.FormatRequest((object) "Invalid  notificationID");
    NotificationScheduleDisableModel scheduleDisableModel = new NotificationScheduleDisableModel();
    scheduleDisableModel.notification = notification;
    scheduleDisableModel.notificationScheduleDisabledList = NotificationScheduleDisabled.LoadByNotificationID(notificationID);
    List<APINotificationYearSchedule> notificationYearScheduleList = new List<APINotificationYearSchedule>();
    for (int index = 1; index <= 12; ++index)
    {
      for (int key = 1; key < 32 /*0x20*/; ++key)
      {
        if (key <= DateTime.DaysInMonth(2016, index))
        {
          Dictionary<int, Dictionary<int, bool>> dictionary = NotificationScheduleDisabled.fillMonthDayDic(scheduleDisableModel.notificationScheduleDisabledList);
          notificationYearScheduleList.Add(new APINotificationYearSchedule()
          {
            Month = index,
            Day = key,
            SendNotifications = dictionary.ContainsKey(index) && dictionary[index].ContainsKey(key) && dictionary[index][key]
          });
        }
      }
    }
    return this.FormatRequest((object) notificationYearScheduleList);
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationYearlyScheduleSet(long notificationID, int month, string days)
  {
    Notification DBObject = Notification.Load(notificationID);
    if (DBObject == null || !MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
      return this.FormatRequest((object) "Invalid  notificationID");
    if (string.IsNullOrEmpty(days))
      return this.FormatRequest((object) "Must include parameter: days");
    if (month == 0 && days == "0")
    {
      NotificationScheduleDisabled.DeleteByNotificationID(notificationID);
      return this.FormatRequest((object) "Notification Schedule Deleted");
    }
    string[] source = days.Split('|');
    if (source.Length > 32 /*0x20*/)
      return this.FormatRequest((object) "Invalid Request: Too long");
    if (((IEnumerable<string>) source).Where<string>((System.Func<string, bool>) (c => c.ToInt() > 31 /*0x1F*/ || c.ToInt() < 0)).Count<string>() > 0)
      return this.FormatRequest((object) "Invalid Request: Invalid day");
    string[] array = ((IEnumerable<string>) source).OrderBy<string, int>((System.Func<string, int>) (item => item.ToInt())).ToArray<string>();
    NotificationScheduleDisabled.DeleteByMonthAndNotificationID(month, notificationID);
    NotificationScheduleDisabled.AddNotificationScheduleDisabled(notificationID, NotificationScheduleDisabled.DayList(array), month);
    NotificationScheduleDisableModel scheduleDisableModel = new NotificationScheduleDisableModel();
    scheduleDisableModel.notification = DBObject;
    scheduleDisableModel.notificationScheduleDisabledList = NotificationScheduleDisabled.LoadByNotificationID(notificationID);
    List<APINotificationYearSchedule> notificationYearScheduleList = new List<APINotificationYearSchedule>();
    Account account = Account.Load(DBObject.AccountID);
    if (account != null)
    {
      string changeRecord = $"{{\"NotifcationID\" : \"{DBObject.NotificationID}\", \"Month\": \"{month}\", \"Days\" : \"{days}\"}} ";
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Edited rule active status yearly schedule ");
    }
    for (int index = 1; index <= 12; ++index)
    {
      for (int key = 1; key < 32 /*0x20*/; ++key)
      {
        if (key <= DateTime.DaysInMonth(2016, index))
        {
          Dictionary<int, Dictionary<int, bool>> dictionary = NotificationScheduleDisabled.fillMonthDayDic(scheduleDisableModel.notificationScheduleDisabledList);
          notificationYearScheduleList.Add(new APINotificationYearSchedule()
          {
            Month = index,
            Day = key,
            SendNotifications = dictionary.ContainsKey(index) && dictionary[index].ContainsKey(key) && dictionary[index][key]
          });
        }
      }
    }
    return this.FormatRequest((object) notificationYearScheduleList);
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationPause(
    long deviceID,
    string deviceType,
    int days,
    int hours,
    int minutes)
  {
    if (string.IsNullOrEmpty(deviceType))
      return this.FormatRequest((object) "Invalid Device Type");
    eAuditObject Auditobj = eAuditObject.Sensor;
    BaseDBObject DBObject;
    long csNetId;
    if (deviceType.ToLower() == "sensor")
    {
      DBObject = (BaseDBObject) Sensor.Load(deviceID);
      if (DBObject == null)
        return this.FormatRequest((object) "Sensor not found");
      if (!MonnitSession.CurrentCustomer.CanSeeSensor((Sensor) DBObject))
        return this.FormatRequest((object) "Unauthorized: Sensor not available");
      csNetId = Sensor.Load(deviceID).CSNetID;
    }
    else
    {
      DBObject = (BaseDBObject) Gateway.Load(deviceID);
      Auditobj = eAuditObject.Gateway;
      if (DBObject == null)
        return this.FormatRequest((object) "Gateway not found");
      if (!MonnitSession.CurrentCustomer.CanSeeNetwork(((Gateway) DBObject).CSNetID))
        return this.FormatRequest((object) "Unauthorized Device");
      csNetId = Gateway.Load(deviceID).CSNetID;
    }
    CSNet csNet = CSNet.Load(csNetId);
    if (days < 0)
      days = 0;
    if (days > 365)
      days = 365;
    if (hours < 0)
      hours = 0;
    if (hours > 72)
      hours = 72;
    if (minutes < 0)
      minutes = 0;
    if (minutes > 1440)
      minutes = 1440;
    DateTime resumeNotifyDate = DateTime.UtcNow;
    DateTime dateTime = resumeNotifyDate.AddDays((double) days);
    dateTime = dateTime.AddHours((double) hours);
    resumeNotifyDate = dateTime.AddMinutes((double) minutes);
    if (days == 0 && hours == 0 && minutes == 0)
      resumeNotifyDate = new DateTime(2099, 1, 1);
    Account account = Account.Load(csNet.AccountID);
    string changeRecord = $"{{\"DeviceID\" : \"{deviceID}\", \"DeviceType\": \"{deviceType}\", \"ResumeNotificationDate\": \"{resumeNotifyDate}\" }} ";
    DBObject.LogAuditData(eAuditAction.Update, Auditobj, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Paused devices effect on notification");
    if (deviceType.ToLower() == "sensor")
    {
      ((Sensor) DBObject).resumeNotificationDate = resumeNotifyDate;
      foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveBySensorID(deviceID))
      {
        notificationTriggered.AcknowledgedBy = MonnitSession.CurrentCustomer.CustomerID;
        notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
        notificationTriggered.AcknowledgeMethod = "API_Pause";
        notificationTriggered.resetTime = DateTime.UtcNow;
        notificationTriggered.Save();
        AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, notificationTriggered.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, notificationTriggered.JsonStringify(), account.AccountID, "Acknowledged rule attached to sensor");
      }
    }
    else
    {
      ((Gateway) DBObject).resumeNotificationDate = resumeNotifyDate;
      foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveByGatewayID(deviceID))
      {
        notificationTriggered.AcknowledgedBy = MonnitSession.CurrentCustomer.CustomerID;
        notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
        notificationTriggered.AcknowledgeMethod = "API_Pause";
        notificationTriggered.resetTime = DateTime.UtcNow;
        notificationTriggered.Save();
        AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, notificationTriggered.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, notificationTriggered.JsonStringify(), account.AccountID, "Acknowledged rule attached to gateway");
      }
    }
    DBObject.Save();
    return this.FormatRequest((object) new APINotificationPause(deviceID, deviceType, resumeNotifyDate));
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationUnpause(long deviceID, string deviceType)
  {
    if (string.IsNullOrEmpty(deviceType))
      return this.FormatRequest((object) "Invalid Device Type");
    BaseDBObject DBObject;
    long csNetId;
    if (deviceType.ToLower() == "sensor")
    {
      DBObject = (BaseDBObject) Sensor.Load(deviceID);
      if (DBObject == null)
        return this.FormatRequest((object) "Sensor not found");
      if (!MonnitSession.CurrentCustomer.CanSeeSensor((Sensor) DBObject))
        return this.FormatRequest((object) "Unauthorized: Sensor not available");
      csNetId = Sensor.Load(deviceID).CSNetID;
    }
    else
    {
      DBObject = (BaseDBObject) Gateway.Load(deviceID);
      if (DBObject == null)
        return this.FormatRequest((object) "Gateway not found");
      if (!MonnitSession.CurrentCustomer.CanSeeNetwork(((Gateway) DBObject).CSNetID))
        return this.FormatRequest((object) "Unauthorized Device");
      csNetId = Gateway.Load(deviceID).CSNetID;
    }
    CSNet csNet = CSNet.Load(csNetId);
    DateTime dateTime = DateTime.UtcNow.AddMinutes(-1.0);
    eAuditObject Auditobj = eAuditObject.Sensor;
    if (deviceType.ToLower() == "sensor")
    {
      ((Sensor) DBObject).resumeNotificationDate = dateTime;
    }
    else
    {
      Auditobj = eAuditObject.Gateway;
      ((Gateway) DBObject).resumeNotificationDate = dateTime;
    }
    Account account = Account.Load(csNet.AccountID);
    if (account != null)
    {
      string changeRecord = $"{{\"DeviceID\" : \"{deviceID}\", \"DeviceType\": \"{deviceType}\", \"ResumeNotificationDate\": \"{dateTime}\" }} ";
      DBObject.LogAuditData(eAuditAction.Update, Auditobj, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Devices effect on notification unpaused");
    }
    DBObject.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationAcknowledge(long notificationID)
  {
    Notification notification = Notification.Load(notificationID);
    if (notification == null || !MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      return this.FormatRequest((object) "Invalid  notificationID");
    List<NotificationTriggered> source = NotificationTriggered.LoadActiveByNotificationID(notificationID);
    if (source.Count < 1 || source.Where<NotificationTriggered>((System.Func<NotificationTriggered, bool>) (c => c.AcknowledgedTime != DateTime.MinValue)).Count<NotificationTriggered>() > 0)
      return this.FormatRequest((object) "Invalid acknowledgement request");
    Account account = Account.Load(notification.AccountID);
    List<APINotificationACK> apiNotificationAckList = new List<APINotificationACK>();
    foreach (NotificationTriggered notiTrig in source)
    {
      notiTrig.AcknowledgeMethod = "API Acknowledge";
      notiTrig.AcknowledgedTime = DateTime.UtcNow;
      notiTrig.AcknowledgedBy = MonnitSession.CurrentCustomer.CustomerID;
      notiTrig.Save();
      AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, notiTrig.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, notiTrig.JsonStringify(), account.AccountID, "Acknowledged rule");
      apiNotificationAckList.Add(new APINotificationACK(notiTrig));
    }
    return this.FormatRequest((object) apiNotificationAckList);
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationFullReset(long notificationID)
  {
    Notification notification = Notification.Load(notificationID);
    if (notification == null || !MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      return this.FormatRequest((object) "Invalid  notificationID");
    Account account = Account.Load(notification.AccountID);
    List<NotificationTriggered> notificationTriggeredList = NotificationTriggered.LoadActiveByNotificationID(notificationID);
    List<APINotificationACK> apiNotificationAckList = new List<APINotificationACK>();
    foreach (NotificationTriggered notiTrig in notificationTriggeredList)
    {
      notiTrig.AcknowledgeMethod = "API Full Reset";
      notiTrig.AcknowledgedTime = DateTime.UtcNow;
      notiTrig.AcknowledgedBy = MonnitSession.CurrentCustomer.CustomerID;
      notiTrig.resetTime = DateTime.UtcNow;
      notiTrig.Save();
      if (account != null)
        AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, notiTrig.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, notiTrig.JsonStringify(), account.AccountID, "Acknowledged and reset rule with conditions still pending");
      apiNotificationAckList.Add(new APINotificationACK(notiTrig));
    }
    return this.FormatRequest((object) apiNotificationAckList);
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationDelete(long notificationID)
  {
    Notification DBObject = Notification.Load(notificationID);
    if (DBObject == null)
      return this.FormatRequest((object) "Invalid Request: there are no notifications that have that notificationID");
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
      return this.FormatRequest((object) "Unauthorized");
    Account account = Account.Load(DBObject.AccountID);
    if (account != null)
      DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Deleted rule");
    DBObject.Delete();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  [ValidateInput(false)]
  public ActionResult ApplicationNotificationCreate(
    long? accountID,
    long? notificationID,
    CreateNotificationAPIModel model,
    string comparer,
    string threshold,
    string scale,
    string datumType)
  {
    if (model.Name.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.Text.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.smsText.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.voiceText.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.pushMsgText.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.subject.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (comparer.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (threshold.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (scale.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (datumType.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    comparer = comparer.SanitizeHTMLEvent();
    threshold = threshold.SanitizeHTMLEvent();
    scale = scale.SanitizeHTMLEvent();
    datumType = datumType.SanitizeHTMLEvent();
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    if (model.snooze == int.MinValue)
      model.snooze = 60;
    if (model.snooze < 0)
      model.snooze = 0;
    if (model.snooze > (int) ushort.MaxValue)
      model.snooze = (int) ushort.MaxValue;
    List<APIValidationError> source = new List<APIValidationError>();
    foreach (string key in (IEnumerable<string>) this.ModelState.Keys)
    {
      foreach (ModelError error in (Collection<ModelError>) this.ModelState[key].Errors)
        source.Add(new APIValidationError(key, error.ErrorMessage));
    }
    if (source.Count<APIValidationError>() != 0)
      return this.FormatRequest((object) source);
    try
    {
      eCompareType o = (eCompareType) System.Enum.Parse(typeof (eCompareType), comparer, true);
      if (string.IsNullOrEmpty(comparer) || o.ToInt() < 1 || o.ToInt() > 4)
        return this.FormatRequest((object) "Please Choose a valid Compare Type");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.PartialView("Invalid Compare Type");
    }
    if (string.IsNullOrEmpty(threshold))
      return this.FormatRequest((object) "Please Choose a valid Threshold");
    try
    {
      eDatumType eDatumType = (eDatumType) System.Enum.Parse(typeof (eDatumType), datumType, true);
    }
    catch (Exception ex)
    {
      return this.FormatRequest((object) "Invalid Datum Type");
    }
    Notification notification1 = new Notification();
    if (notificationID.HasValue)
    {
      notification1 = Notification.Load(notificationID.GetValueOrDefault());
      if (notification1 == null || !MonnitSession.CurrentCustomer.CanSeeAccount(notification1.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
        return this.FormatRequest((object) "Unauthorized");
      if (notification1.eDatumType != (eDatumType) System.Enum.Parse(typeof (eDatumType), datumType, true))
        return this.FormatRequest((object) "You may only edit matching notifications");
      notification1.CompareType = (eCompareType) System.Enum.Parse(typeof (eCompareType), comparer, true);
      notification1.eDatumType = (eDatumType) System.Enum.Parse(typeof (eDatumType), datumType, true);
      notification1.Scale = scale == string.Empty ? notification1.Scale : scale;
      notification1.CompareValue = threshold == string.Empty ? notification1.CompareValue.ToString() : threshold.ToString();
      notification1.IsActive = false;
      notification1.IsDeleted = false;
      notification1.Name = model.Name == string.Empty ? notification1.Name : model.Name;
      notification1.NotificationText = !string.IsNullOrEmpty(model.Text) ? model.Text : "";
      notification1.Subject = !string.IsNullOrEmpty(model.subject) ? model.subject : "";
      notification1.VoiceText = !string.IsNullOrEmpty(model.voiceText) ? model.voiceText : "";
      notification1.SMSText = !string.IsNullOrEmpty(model.smsText) ? model.smsText : "";
      notification1.PushMsgText = !string.IsNullOrEmpty(model.pushMsgText) ? model.pushMsgText : "";
      notification1.Version = "1";
      notification1.SnoozeDuration = model.snooze;
      notification1.AlwaysSend = true;
      Notification notification2 = notification1;
      int num1 = model.autoAcknowledge ? 1 : 0;
      int num2 = model.autoAcknowledge ? 1 : 0;
      notification2.CanAutoAcknowledge = num2 != 0;
      notification1.ApplySnoozeByTriggerDevice = model.jointSnooze;
      DataTypeBase.VerifyNotificationValues(notification1, scale);
      notification1.HasUserNotificationAction = true;
      notification1.Save();
    }
    if (!notificationID.HasValue || notificationID.GetValueOrDefault() == long.MinValue)
    {
      notification1.AccountID = acct.AccountID;
      notification1.CompareType = (eCompareType) System.Enum.Parse(typeof (eCompareType), comparer, true);
      notification1.eDatumType = (eDatumType) System.Enum.Parse(typeof (eDatumType), datumType, true);
      notification1.Scale = scale;
      notification1.CompareValue = threshold;
      notification1.IsActive = false;
      notification1.IsDeleted = false;
      notification1.Name = model.Name == string.Empty ? "Notification_" + notification1.NotificationID.ToString() : model.Name;
      notification1.NotificationClass = eNotificationClass.Application;
      notification1.NotificationText = model.Text;
      notification1.SMSText = model.smsText;
      notification1.Version = "1";
      notification1.SnoozeDuration = model.snooze;
      notification1.Subject = model.subject;
      notification1.VoiceText = model.voiceText;
      notification1.PushMsgText = model.pushMsgText;
      notification1.AlwaysSend = true;
      Notification notification3 = notification1;
      int num3 = model.autoAcknowledge ? 1 : 0;
      int num4 = model.autoAcknowledge ? 1 : 0;
      notification3.CanAutoAcknowledge = num4 != 0;
      Notification notification4 = notification1;
      int num5 = model.jointSnooze ? 1 : 0;
      int num6 = model.jointSnooze ? 1 : 0;
      notification4.ApplySnoozeByTriggerDevice = num6 != 0;
      DataTypeBase.VerifyNotificationValues(notification1, scale);
      notification1.HasUserNotificationAction = true;
      notification1.Save();
    }
    notification1.LogAuditData(eAuditAction.Create, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, acct.AccountID, "Created application rule");
    return this.FormatRequest((object) new APINotification(notification1));
  }

  [AuthorizeAPIPremier]
  [ValidateInput(false)]
  public ActionResult ScheduledNotificationCreate(
    long? accountID,
    long? notificationID,
    long? sensorID,
    long? gatewayID,
    int LocalHour,
    int LocalMinute,
    CreateNotificationAPIModel model)
  {
    if (model.Name.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.Text.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.smsText.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.voiceText.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.pushMsgText.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.subject.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    model.Name = model.Name.SanitizeHTMLEvent();
    model.Text = model.Text.SanitizeHTMLEvent();
    model.smsText = model.smsText.SanitizeHTMLEvent();
    model.voiceText = model.voiceText.SanitizeHTMLEvent();
    model.pushMsgText = model.pushMsgText.SanitizeHTMLEvent();
    model.subject = model.subject.SanitizeHTMLEvent();
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    if (gatewayID.HasValue)
    {
      CSNet csNet = CSNet.Load(Gateway.Load(gatewayID.GetValueOrDefault()).CSNetID);
      if (!MonnitSession.CurrentCustomer.CanSeeAccount(csNet.AccountID) && csNet.AccountID != acct.AccountID)
        return this.FormatRequest((object) "Unauthorized Gateway");
    }
    if (sensorID.GetValueOrDefault() != long.MinValue && !MonnitSession.CurrentCustomer.CanSeeAccount(Sensor.Load(sensorID.GetValueOrDefault()).AccountID) && acct.AccountID != Sensor.Load(sensorID.GetValueOrDefault()).AccountID)
      return this.FormatRequest((object) "Unauthorized Sensor");
    List<APIValidationError> source = new List<APIValidationError>();
    foreach (string key in (IEnumerable<string>) this.ModelState.Keys)
    {
      foreach (ModelError error in (Collection<ModelError>) this.ModelState[key].Errors)
        source.Add(new APIValidationError(key, error.ErrorMessage));
    }
    if (source.Count<APIValidationError>() != 0)
      return this.FormatRequest((object) source);
    Notification notification = new Notification();
    if (notificationID.HasValue)
    {
      notification = Notification.Load(notificationID.GetValueOrDefault());
      if (!MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
        return this.FormatRequest((object) "Unauthorized");
      if (notification.NotificationClass != eNotificationClass.Timed)
        return this.FormatRequest((object) "You may only edit matching notifications");
      notification.SensorID = sensorID ?? long.MinValue;
      notification.GatewayID = gatewayID ?? long.MinValue;
      notification.AccountID = acct.AccountID;
      notification.Name = model.Name == string.Empty ? notification.Name : model.Name;
      notification.NotificationText = !string.IsNullOrEmpty(model.Text) ? model.Text : "";
      notification.Subject = !string.IsNullOrEmpty(model.subject) ? model.subject : "";
      notification.VoiceText = !string.IsNullOrEmpty(model.voiceText) ? model.voiceText : "";
      notification.SMSText = !string.IsNullOrEmpty(model.smsText) ? model.smsText : "";
      notification.PushMsgText = !string.IsNullOrEmpty(model.pushMsgText) ? model.pushMsgText : "";
      notification.CompareType = eCompareType.Equal;
      string compareValue = notification.CompareValue;
      notification.IsActive = false;
      notification.IsDeleted = false;
      notification.AlwaysSend = true;
      notification.Version = "1";
      notification.NotificationByTime.ScheduledHour = LocalHour;
      notification.NotificationByTime.ScheduledMinute = LocalMinute;
      string str = $"{notification.NotificationByTime.ScheduledHour}:{notification.NotificationByTime.ScheduledMinute}";
      if (compareValue != str)
        notification.NotificationByTime.NextEvaluationDate = DateTime.MinValue;
      notification.CompareValue = str;
      notification.SnoozeDuration = 0;
      notification.CanAutoAcknowledge = true;
      notification.ApplySnoozeByTriggerDevice = true;
      notification.NotificationByTime.Save();
      notification.NotificationByTimeID = notification.NotificationByTime.NotificationByTimeID;
      notification.HasUserNotificationAction = true;
      notification.Save();
    }
    if (!notificationID.HasValue || notificationID.GetValueOrDefault() == long.MinValue)
    {
      notification = new Notification()
      {
        AccountID = acct.AccountID,
        SensorID = sensorID ?? long.MinValue,
        GatewayID = gatewayID ?? long.MinValue
      };
      notification.Name = model.Name == string.Empty ? "Notification_" + notification.NotificationID.ToString() : model.Name;
      notification.NotificationText = !string.IsNullOrEmpty(model.Text) ? model.Text : "";
      notification.Subject = !string.IsNullOrEmpty(model.subject) ? model.subject : "";
      notification.VoiceText = !string.IsNullOrEmpty(model.voiceText) ? model.voiceText : "";
      notification.SMSText = !string.IsNullOrEmpty(model.smsText) ? model.smsText : "";
      notification.PushMsgText = !string.IsNullOrEmpty(model.pushMsgText) ? model.pushMsgText : "";
      notification.NotificationClass = eNotificationClass.Timed;
      notification.CompareValue = "0:00";
      notification.IsActive = false;
      notification.IsDeleted = false;
      notification.AlwaysSend = true;
      notification.Version = "1";
      notification.CompareType = eCompareType.Equal;
      notification.NotificationByTime.ScheduledHour = LocalHour;
      notification.NotificationByTime.ScheduledMinute = LocalMinute;
      string str = $"{notification.NotificationByTime.ScheduledHour}:{notification.NotificationByTime.ScheduledMinute}";
      notification.NotificationByTime.NextEvaluationDate = DateTime.MinValue;
      notification.CompareValue = str;
      notification.SnoozeDuration = 0;
      notification.CanAutoAcknowledge = true;
      notification.ApplySnoozeByTriggerDevice = true;
      notification.NotificationByTime.Save();
      notification.NotificationByTimeID = notification.NotificationByTime.NotificationByTimeID;
      notification.HasUserNotificationAction = true;
      notification.Save();
    }
    notification.LogAuditData(eAuditAction.Create, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, acct.AccountID, "Created scheduled rule");
    return this.FormatRequest((object) new APINotification(notification));
  }

  [AuthorizeAPIPremier]
  [ValidateInput(false)]
  public ActionResult InactivityNotificationCreate(
    long? accountID,
    long? notificationID,
    CreateNotificationAPIModel model,
    string threshold)
  {
    if (model.Name.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.Text.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.smsText.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.voiceText.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.pushMsgText.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.subject.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    if (string.IsNullOrEmpty(threshold))
      threshold = "245";
    if (threshold.ToInt() < 1)
      threshold = "1";
    if (threshold.ToInt() > 525600)
      threshold = "525600";
    if (model.snooze == int.MinValue)
      model.snooze = 60;
    if (model.snooze < 0)
      model.snooze = 0;
    if (model.snooze > (int) ushort.MaxValue)
      model.snooze = (int) ushort.MaxValue;
    List<APIValidationError> source = new List<APIValidationError>();
    foreach (string key in (IEnumerable<string>) this.ModelState.Keys)
    {
      foreach (ModelError error in (Collection<ModelError>) this.ModelState[key].Errors)
        source.Add(new APIValidationError(key, error.ErrorMessage));
    }
    if (source.Count<APIValidationError>() != 0)
      return this.FormatRequest((object) source);
    Notification notification1 = new Notification();
    if (notificationID.HasValue)
    {
      notification1 = Notification.Load(notificationID.GetValueOrDefault());
      if (!MonnitSession.CurrentCustomer.CanSeeAccount(notification1.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
        return this.FormatRequest((object) "Unauthorized");
      if (notification1.NotificationClass != eNotificationClass.Inactivity)
        return this.FormatRequest((object) "You may only edit matching notifications");
      notification1.AccountID = acct.AccountID;
      notification1.Name = model.Name == string.Empty ? notification1.Name : model.Name;
      notification1.NotificationText = !string.IsNullOrEmpty(model.Text) ? model.Text : "";
      notification1.Subject = !string.IsNullOrEmpty(model.subject) ? model.subject : "";
      notification1.VoiceText = !string.IsNullOrEmpty(model.voiceText) ? model.voiceText : "";
      notification1.SMSText = !string.IsNullOrEmpty(model.smsText) ? model.smsText : "";
      notification1.PushMsgText = !string.IsNullOrEmpty(model.pushMsgText) ? model.pushMsgText : "";
      notification1.IsActive = false;
      notification1.IsDeleted = false;
      notification1.AlwaysSend = true;
      notification1.Version = "1";
      notification1.CompareType = eCompareType.Greater_Than;
      notification1.CompareValue = threshold;
      notification1.SnoozeDuration = model.snooze;
      Notification notification2 = notification1;
      int num1 = model.autoAcknowledge ? 1 : 0;
      int num2 = model.autoAcknowledge ? 1 : 0;
      notification2.CanAutoAcknowledge = num2 != 0;
      Notification notification3 = notification1;
      int num3 = model.jointSnooze ? 1 : 0;
      int num4 = model.jointSnooze ? 1 : 0;
      notification3.ApplySnoozeByTriggerDevice = num4 != 0;
      notification1.HasUserNotificationAction = true;
      notification1.Save();
    }
    if (!notificationID.HasValue || notificationID.GetValueOrDefault() == long.MinValue)
    {
      notification1 = new Notification()
      {
        AccountID = acct.AccountID
      };
      notification1.Name = model.Name == string.Empty ? "Notification_" + notification1.NotificationID.ToString() : model.Name;
      notification1.NotificationText = !string.IsNullOrEmpty(model.Text) ? model.Text : "";
      notification1.Subject = !string.IsNullOrEmpty(model.subject) ? model.subject : "";
      notification1.VoiceText = !string.IsNullOrEmpty(model.voiceText) ? model.voiceText : "";
      notification1.SMSText = !string.IsNullOrEmpty(model.smsText) ? model.smsText : "";
      notification1.PushMsgText = !string.IsNullOrEmpty(model.pushMsgText) ? model.pushMsgText : "";
      notification1.NotificationClass = eNotificationClass.Inactivity;
      notification1.IsActive = false;
      notification1.IsDeleted = false;
      notification1.AlwaysSend = true;
      notification1.Version = "1";
      notification1.CompareType = eCompareType.Greater_Than;
      notification1.CompareValue = threshold;
      notification1.SnoozeDuration = model.snooze;
      Notification notification4 = notification1;
      int num5 = model.autoAcknowledge ? 1 : 0;
      int num6 = model.autoAcknowledge ? 1 : 0;
      notification4.CanAutoAcknowledge = num6 != 0;
      Notification notification5 = notification1;
      int num7 = model.jointSnooze ? 1 : 0;
      int num8 = model.jointSnooze ? 1 : 0;
      notification5.ApplySnoozeByTriggerDevice = num8 != 0;
      notification1.HasUserNotificationAction = true;
      notification1.Save();
    }
    notification1.LogAuditData(eAuditAction.Create, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, acct.AccountID, "Created inactivity rule");
    return this.FormatRequest((object) new APINotification(notification1));
  }

  [AuthorizeAPIPremier]
  [ValidateInput(false)]
  public ActionResult BatteryNotificationCreate(
    long? accountID,
    long? notificationID,
    CreateNotificationAPIModel model,
    string threshold)
  {
    if (model.Name.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.Text.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.smsText.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.voiceText.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.pushMsgText.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.subject.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    if (string.IsNullOrEmpty(threshold))
      threshold = "15";
    if (threshold.ToInt() < 1)
      threshold = "1";
    if (threshold.ToInt() > 100)
      threshold = "100";
    if (model.snooze == int.MinValue)
      model.snooze = 60;
    if (model.snooze < 0)
      model.snooze = 0;
    if (model.snooze > (int) ushort.MaxValue)
      model.snooze = (int) ushort.MaxValue;
    List<APIValidationError> source = new List<APIValidationError>();
    foreach (string key in (IEnumerable<string>) this.ModelState.Keys)
    {
      foreach (ModelError error in (Collection<ModelError>) this.ModelState[key].Errors)
        source.Add(new APIValidationError(key, error.ErrorMessage));
    }
    if (source.Count<APIValidationError>() != 0)
      return this.FormatRequest((object) source);
    Notification notification1 = new Notification();
    if (notificationID.HasValue)
    {
      notification1 = Notification.Load(notificationID.GetValueOrDefault());
      if (!MonnitSession.CurrentCustomer.CanSeeAccount(notification1.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
        return this.FormatRequest((object) "Unauthorized");
      if (notification1.NotificationClass != eNotificationClass.Low_Battery)
        return this.FormatRequest((object) "You may only edit matching notifications");
      notification1.AccountID = acct.AccountID;
      notification1.Name = model.Name == string.Empty ? notification1.Name : model.Name;
      notification1.NotificationText = !string.IsNullOrEmpty(model.Text) ? model.Text : "";
      notification1.Subject = !string.IsNullOrEmpty(model.subject) ? model.subject : "";
      notification1.VoiceText = !string.IsNullOrEmpty(model.voiceText) ? model.voiceText : "";
      notification1.SMSText = !string.IsNullOrEmpty(model.smsText) ? model.smsText : "";
      notification1.PushMsgText = !string.IsNullOrEmpty(model.pushMsgText) ? model.pushMsgText : "";
      notification1.IsActive = false;
      notification1.IsDeleted = false;
      notification1.AlwaysSend = true;
      notification1.Version = "1";
      notification1.CompareType = eCompareType.Less_Than;
      notification1.CompareValue = threshold;
      notification1.SnoozeDuration = model.snooze;
      Notification notification2 = notification1;
      int num1 = model.autoAcknowledge ? 1 : 0;
      int num2 = model.autoAcknowledge ? 1 : 0;
      notification2.CanAutoAcknowledge = num2 != 0;
      Notification notification3 = notification1;
      int num3 = model.jointSnooze ? 1 : 0;
      int num4 = model.jointSnooze ? 1 : 0;
      notification3.ApplySnoozeByTriggerDevice = num4 != 0;
      notification1.HasUserNotificationAction = true;
      notification1.Save();
    }
    if (!notificationID.HasValue || notificationID.GetValueOrDefault() == long.MinValue)
    {
      notification1 = new Notification()
      {
        AccountID = acct.AccountID
      };
      notification1.Name = model.Name == string.Empty ? notification1.Name : model.Name;
      notification1.NotificationText = !string.IsNullOrEmpty(model.Text) ? model.Text : "";
      notification1.Subject = !string.IsNullOrEmpty(model.subject) ? model.subject : "";
      notification1.VoiceText = !string.IsNullOrEmpty(model.voiceText) ? model.voiceText : "";
      notification1.SMSText = !string.IsNullOrEmpty(model.smsText) ? model.smsText : "";
      notification1.PushMsgText = !string.IsNullOrEmpty(model.pushMsgText) ? model.pushMsgText : "";
      notification1.NotificationClass = eNotificationClass.Low_Battery;
      notification1.IsActive = false;
      notification1.IsDeleted = false;
      notification1.AlwaysSend = true;
      notification1.Version = "1";
      notification1.CompareType = eCompareType.Less_Than;
      notification1.CompareValue = threshold;
      notification1.SnoozeDuration = model.snooze;
      Notification notification4 = notification1;
      int num5 = model.autoAcknowledge ? 1 : 0;
      int num6 = model.autoAcknowledge ? 1 : 0;
      notification4.CanAutoAcknowledge = num6 != 0;
      Notification notification5 = notification1;
      int num7 = model.jointSnooze ? 1 : 0;
      int num8 = model.jointSnooze ? 1 : 0;
      notification5.ApplySnoozeByTriggerDevice = num8 != 0;
      notification1.HasUserNotificationAction = true;
      notification1.Save();
    }
    notification1.LogAuditData(eAuditAction.Create, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, acct.AccountID, "Created battery rule");
    return this.FormatRequest((object) new APINotification(notification1));
  }

  [AuthorizeAPIPremier]
  [ValidateInput(false)]
  public ActionResult AdvancedNotificationCreate(
    long? accountID,
    long? notificationID,
    long advancedNotificationID,
    CreateNotificationAPIModel model)
  {
    if (model.Name.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.Text.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.smsText.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.voiceText.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.pushMsgText.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    if (model.subject.HasScriptTag())
      return this.FormatRequest((object) "Script tags not permitted");
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    if (model.snooze == int.MinValue)
      model.snooze = 60;
    if (model.snooze < 0)
      model.snooze = 0;
    if (model.snooze > (int) ushort.MaxValue)
      model.snooze = (int) ushort.MaxValue;
    List<APIValidationError> source = new List<APIValidationError>();
    foreach (string key in (IEnumerable<string>) this.ModelState.Keys)
    {
      foreach (ModelError error in (Collection<ModelError>) this.ModelState[key].Errors)
        source.Add(new APIValidationError(key, error.ErrorMessage));
    }
    if (source.Count<APIValidationError>() != 0)
      return this.FormatRequest((object) source);
    Notification notification1 = new Notification();
    if (notificationID.HasValue)
    {
      notification1 = Notification.Load(notificationID.GetValueOrDefault());
      if (!MonnitSession.CurrentCustomer.CanSeeAccount(notification1.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
        return this.FormatRequest((object) "Unauthorized");
      if (notification1.NotificationClass != eNotificationClass.Advanced)
        return this.FormatRequest((object) "You may only edit matching notifications");
      notification1.AccountID = acct.AccountID;
      notification1.Name = model.Name == string.Empty ? notification1.Name : model.Name;
      notification1.NotificationText = !string.IsNullOrEmpty(model.Text) ? model.Text : "";
      notification1.Subject = !string.IsNullOrEmpty(model.subject) ? model.subject : "";
      notification1.VoiceText = !string.IsNullOrEmpty(model.voiceText) ? model.voiceText : "";
      notification1.SMSText = !string.IsNullOrEmpty(model.smsText) ? model.smsText : "";
      notification1.PushMsgText = !string.IsNullOrEmpty(model.pushMsgText) ? model.pushMsgText : "";
      notification1.IsActive = false;
      notification1.IsDeleted = false;
      notification1.AlwaysSend = true;
      notification1.Version = "1";
      notification1.CompareType = eCompareType.Equal;
      notification1.CompareValue = "0";
      notification1.SnoozeDuration = model.snooze;
      Notification notification2 = notification1;
      int num1 = model.autoAcknowledge ? 1 : 0;
      int num2 = model.autoAcknowledge ? 1 : 0;
      notification2.CanAutoAcknowledge = num2 != 0;
      Notification notification3 = notification1;
      int num3 = model.jointSnooze ? 1 : 0;
      int num4 = model.jointSnooze ? 1 : 0;
      notification3.ApplySnoozeByTriggerDevice = num4 != 0;
      notification1.Save();
    }
    if (!notificationID.HasValue || notificationID.GetValueOrDefault() == long.MinValue)
    {
      notification1 = new Notification()
      {
        AccountID = acct.AccountID
      };
      notification1.Name = model.Name == string.Empty ? notification1.Name : model.Name;
      notification1.NotificationText = !string.IsNullOrEmpty(model.Text) ? model.Text : "";
      notification1.Subject = !string.IsNullOrEmpty(model.subject) ? model.subject : "";
      notification1.VoiceText = !string.IsNullOrEmpty(model.voiceText) ? model.voiceText : "";
      notification1.SMSText = !string.IsNullOrEmpty(model.smsText) ? model.smsText : "";
      notification1.PushMsgText = !string.IsNullOrEmpty(model.pushMsgText) ? model.pushMsgText : "";
      notification1.NotificationClass = eNotificationClass.Advanced;
      notification1.AdvancedNotificationID = advancedNotificationID;
      notification1.IsActive = false;
      notification1.IsDeleted = false;
      notification1.AlwaysSend = true;
      notification1.Version = "1";
      notification1.CompareType = eCompareType.Equal;
      notification1.CompareValue = "0";
      notification1.SnoozeDuration = model.snooze;
      Notification notification4 = notification1;
      int num5 = model.autoAcknowledge ? 1 : 0;
      int num6 = model.autoAcknowledge ? 1 : 0;
      notification4.CanAutoAcknowledge = num6 != 0;
      Notification notification5 = notification1;
      int num7 = model.jointSnooze ? 1 : 0;
      int num8 = model.jointSnooze ? 1 : 0;
      notification5.ApplySnoozeByTriggerDevice = num8 != 0;
      notification1.Save();
    }
    notification1.LogAuditData(eAuditAction.Create, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, acct.AccountID, "Created advanced rule");
    return this.FormatRequest((object) new APINotification(notification1));
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationListAdvancedTypes(long? accountID)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    List<APIAdvancedNotification> advancedNotificationList = new List<APIAdvancedNotification>();
    foreach (AdvancedNotification AdvNoti in AdvancedNotification.LoadByAccountID(acct.AccountID))
      advancedNotificationList.Add(new APIAdvancedNotification(AdvNoti));
    return this.FormatRequest((object) advancedNotificationList);
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationListAdvancedParameters(
    long? accountID,
    long advancedNotificationID)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    if (AdvancedNotification.LoadByAccountID(acct.AccountID).Where<AdvancedNotification>((System.Func<AdvancedNotification, bool>) (c => c.AdvancedNotificationID == advancedNotificationID)).Count<AdvancedNotification>() < 1)
      return this.FormatRequest((object) "Invalid Request: Unauthorized Notification");
    List<AdvancedNotificationParameter> notificationParameterList1 = AdvancedNotificationParameter.LoadByAdvancedNotificationID(advancedNotificationID);
    List<APIAdvancedNotificationParameter> notificationParameterList2 = new List<APIAdvancedNotificationParameter>();
    foreach (AdvancedNotificationParameter parameter in notificationParameterList1)
      notificationParameterList2.Add(new APIAdvancedNotificationParameter(parameter));
    return this.FormatRequest((object) notificationParameterList2);
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationListAdvancedParameterOptions(long advancedNotificationParameterID)
  {
    List<AdvancedNotificationParameterOption> notificationParameterOptionList1 = AdvancedNotificationParameterOption.LoadByParameter(advancedNotificationParameterID);
    List<APIAdvancedNotificationParameterOption> notificationParameterOptionList2 = new List<APIAdvancedNotificationParameterOption>();
    foreach (AdvancedNotificationParameterOption option in notificationParameterOptionList1)
      notificationParameterOptionList2.Add(new APIAdvancedNotificationParameterOption(option));
    return this.FormatRequest((object) notificationParameterOptionList2);
  }

  [AuthorizeAPIPremier]
  public ActionResult AdvancedNotificationParameterSet(
    long notificationID,
    long advancedNotificationParameterID,
    string parameterValue)
  {
    Notification DBObject = Notification.Load(notificationID);
    if (DBObject == null || !MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
      return this.FormatRequest((object) "Invalid  notificationID");
    if (DBObject.NotificationClass != eNotificationClass.Advanced || DBObject.AdvancedNotificationID == long.MinValue)
      return this.FormatRequest((object) "Invalid notification class");
    if (AdvancedNotificationParameter.LoadByAdvancedNotificationID(DBObject.AdvancedNotificationID).Where<AdvancedNotificationParameter>((System.Func<AdvancedNotificationParameter, bool>) (m => m.AdvancedNotificationParameterID == advancedNotificationParameterID)).Count<AdvancedNotificationParameter>() < 1)
      return this.FormatRequest((object) "Invalid parameter ");
    Account account = Account.Load(DBObject.AccountID);
    if (account != null)
    {
      string changeRecord = $"{{\"NotifcationID\" : \"{DBObject.NotificationID}\",\"advancedNotificationParameterID\": \"{advancedNotificationParameterID}\",\"parameterValue\": \"{parameterValue}\" }} ";
      DBObject.LogAuditData(eAuditAction.Create, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Edited parameter for advanced rule");
    }
    List<APIAdvancedNotificationParameterValue> notificationParameterValueList = new List<APIAdvancedNotificationParameterValue>();
    List<AdvancedNotificationParameterValue> source = AdvancedNotificationParameterValue.LoadByNotificationID(notificationID);
    if (source.Where<AdvancedNotificationParameterValue>((System.Func<AdvancedNotificationParameterValue, bool>) (m => m.AdvancedNotificationParameterID == advancedNotificationParameterID)).Count<AdvancedNotificationParameterValue>() > 0)
    {
      AdvancedNotificationParameterValue notificationParameterValue = source.Where<AdvancedNotificationParameterValue>((System.Func<AdvancedNotificationParameterValue, bool>) (m => m.AdvancedNotificationParameterID == advancedNotificationParameterID)).FirstOrDefault<AdvancedNotificationParameterValue>();
      if (source.Where<AdvancedNotificationParameterValue>((System.Func<AdvancedNotificationParameterValue, bool>) (m => m.AdvancedNotificationParameterID == advancedNotificationParameterID && m.ParameterValue == parameterValue)).Count<AdvancedNotificationParameterValue>() < 1)
      {
        notificationParameterValue.ParameterValue = parameterValue;
        notificationParameterValue.Save();
      }
      notificationParameterValueList.Add(new APIAdvancedNotificationParameterValue(notificationParameterValue));
    }
    else
    {
      AdvancedNotificationParameterValue notificationParameterValue = new AdvancedNotificationParameterValue();
      notificationParameterValue.AdvancedNotificationParameterID = advancedNotificationParameterID;
      notificationParameterValue.NotificationID = notificationID;
      notificationParameterValue.ParameterValue = parameterValue;
      notificationParameterValue.Save();
      notificationParameterValueList.Add(new APIAdvancedNotificationParameterValue(notificationParameterValue));
    }
    return this.FormatRequest((object) notificationParameterValueList);
  }

  [AuthorizeAPIPremier]
  public ActionResult AdvancedNotificationParameterList(long notificationID)
  {
    Notification notification = Notification.Load(notificationID);
    if (notification == null || !MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      return this.FormatRequest((object) "Invalid  notificationID");
    if (notification.NotificationClass != eNotificationClass.Advanced || notification.AdvancedNotificationID == long.MinValue)
      return this.FormatRequest((object) "Invalid notification class");
    List<AdvancedNotificationParameterValue> notificationParameterValueList1 = AdvancedNotificationParameterValue.LoadByNotificationID(notificationID);
    List<AdvancedNotificationParameter> notificationParameterList = AdvancedNotificationParameter.LoadByAdvancedNotificationID(notification.AdvancedNotificationID);
    List<APIAdvancedNotificationParameterValue> notificationParameterValueList2 = new List<APIAdvancedNotificationParameterValue>();
    foreach (AdvancedNotificationParameterValue notificationParameterValue in notificationParameterValueList1)
    {
      foreach (AdvancedNotificationParameter notificationParameter in notificationParameterList)
      {
        if (notificationParameterValue.AdvancedNotificationParameterID == notificationParameter.AdvancedNotificationParameterID)
          notificationParameterValueList2.Add(new APIAdvancedNotificationParameterValue(notificationParameterValue));
      }
    }
    return this.FormatRequest((object) notificationParameterValueList2);
  }

  [AuthorizeAPIPremier]
  public ActionResult NotificationListAdvancedAutomatedSchedule(long notificationID)
  {
    Notification notification = Notification.Load(notificationID);
    if (notification == null || !MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      return this.FormatRequest((object) "Invalid  notificationID");
    if (notification.NotificationClass != eNotificationClass.Advanced || notification.AdvancedNotificationID == long.MinValue)
      return this.FormatRequest((object) "Invalid notification class");
    if (AdvancedNotification.Load(notification.AdvancedNotificationID).AdvancedNotificationType != eAdvancedNotificationType.AutomatedSchedule)
      return this.FormatRequest((object) "Invalid notification");
    AutomatedNotification auto = AutomatedNotification.LoadByExternalIDAndNotificationID(notification.AdvancedNotificationID, notification.NotificationID);
    return auto == null ? this.FormatRequest((object) "No automated schedule found") : this.FormatRequest((object) new APIAdvancedNotificationAutomatedSchedule(auto));
  }

  [AuthorizeAPIPremier]
  public ActionResult AdvancedNotificationAutomatedScheduleSet(
    long notificationID,
    int processFrequency)
  {
    Notification DBObject = Notification.Load(notificationID);
    if (DBObject == null || !MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
      return this.FormatRequest((object) "Invalid  notificationID");
    if (DBObject.NotificationClass != eNotificationClass.Advanced || DBObject.AdvancedNotificationID == long.MinValue)
      return this.FormatRequest((object) "Invalid notification class");
    if (AdvancedNotification.Load(DBObject.AdvancedNotificationID).AdvancedNotificationType != eAdvancedNotificationType.AutomatedSchedule)
      return this.FormatRequest((object) "Invalid notification");
    AutomatedNotification auto = AutomatedNotification.LoadByExternalIDAndNotificationID(DBObject.AdvancedNotificationID, DBObject.NotificationID);
    if (processFrequency > 0)
    {
      if (processFrequency < 60)
        processFrequency = 60;
      if (processFrequency > 1440)
        processFrequency = 1440;
      if (auto == null)
      {
        auto = new AutomatedNotification();
        auto.NotificationID = DBObject.NotificationID;
        auto.ExternalID = DBObject.AdvancedNotificationID;
        auto.Description = DBObject.Name;
      }
      auto.ProcessFrequency = processFrequency;
      auto.Save();
      Account account = Account.Load(DBObject.AccountID);
      if (account != null)
      {
        string changeRecord = $"{{ \"NotifcationID\" : \"{DBObject.NotificationID}\",\"ProcessFrequency\": \"{processFrequency}\" }} ";
        DBObject.LogAuditData(eAuditAction.Create, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Edited process frequency for advanced rule");
      }
    }
    return this.FormatRequest((object) new APIAdvancedNotificationAutomatedSchedule(auto));
  }

  [AuthorizeAPIPremier]
  public ActionResult ExternalDataPush(long sensorID)
  {
    Sensor sensor = Sensor.Load(sensorID);
    return sensor != null && sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && MonnitSession.CurrentCustomer.CanSeeSensor(sensor) ? this.FormatRequest((object) new APIDataPushConfiguration(ExternalDataSubscription.LoadAllByAccountID(sensor.AccountID).FirstOrDefault<ExternalDataSubscription>())) : this.FormatRequest((object) "Invalid SensorID");
  }

  [AuthorizeAPIPremier]
  public ActionResult ExternalDataPushList(long accountID)
  {
    try
    {
      if (accountID == long.MinValue || accountID != MonnitSession.CurrentCustomer.AccountID)
        return this.FormatRequest((object) "Invalid Request: there are no External Data Subscriptions that have that account id.");
      List<ExternalDataSubscription> dataSubscriptionList = ExternalDataSubscription.LoadAllByAccountID(accountID);
      List<APIDataPushConfiguration> pushConfigurationList = new List<APIDataPushConfiguration>();
      foreach (ExternalDataSubscription sub in dataSubscriptionList)
        pushConfigurationList.Add(new APIDataPushConfiguration(sub));
      return this.FormatRequest((object) pushConfigurationList);
    }
    catch
    {
      return this.FormatRequest((object) "Failed");
    }
  }

  [AuthorizeAPIPremier]
  public ActionResult ExternalGatewayDataPushList(long accountID)
  {
    try
    {
      if (accountID == long.MinValue || accountID != MonnitSession.CurrentCustomer.AccountID)
        return this.FormatRequest((object) "Invalid Request: there are no External Data Subscriptions that have that account id.");
      Gateway.LoadByAccountID(accountID);
      List<ExternalDataSubscription> dataSubscriptionList = ExternalDataSubscription.LoadAllByAccountID(accountID);
      List<APIGatewayDataPushConfiguration> pushConfigurationList = new List<APIGatewayDataPushConfiguration>();
      foreach (ExternalDataSubscription sub in dataSubscriptionList)
        pushConfigurationList.Add(new APIGatewayDataPushConfiguration(sub));
      return this.FormatRequest((object) pushConfigurationList);
    }
    catch
    {
      return this.FormatRequest((object) "Failed");
    }
  }

  [AuthorizeAPIPremier]
  public ActionResult ResetExternalDataSubscription(
    long? sensorID,
    long? gatewayID,
    long accountID)
  {
    try
    {
      if (accountID == long.MinValue || !MonnitSession.CurrentCustomer.CanSeeAccount(accountID))
        return this.FormatRequest((object) "Invalid Request: there are no External Data Subscriptions that use that specific account id");
      ExternalDataSubscription dataSubscription = ExternalDataSubscription.LoadAllByAccountID(accountID).FirstOrDefault<ExternalDataSubscription>();
      if (dataSubscription == null || dataSubscription.ExternalDataSubscriptionID == long.MinValue)
        return this.FormatRequest((object) "Invalid Request: The AccountID that you passed in has not been set up for data pushes.");
      if (dataSubscription.BrokenCount < 100)
        return this.FormatRequest((object) "Invalid Request: All retry attempts must be met before you may reset the retry count.");
      dataSubscription.resetBroken();
      return this.FormatRequest((object) "Success");
    }
    catch
    {
      return this.FormatRequest((object) "Failed");
    }
  }

  [AuthorizeAPIPremier]
  public ActionResult ExternalDataPushSet(
    long sensorID,
    string connectionInfo,
    string externalID,
    string verb = "get",
    string connectionBody = "",
    string ContentHeaderType = "")
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor.AccountID != MonnitSession.CurrentCustomer.AccountID || !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return this.FormatRequest((object) "Invalid SensorID");
    if (string.IsNullOrEmpty(externalID))
      externalID = "None";
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.LoadAllByAccountID(sensor.AccountID).FirstOrDefault<ExternalDataSubscription>();
    if (dataSubscription != null)
    {
      dataSubscription.AccountID = MonnitSession.CurrentCustomer.AccountID;
      if (string.IsNullOrEmpty(connectionInfo))
      {
        dataSubscription.Delete();
        sensor.ExternalID = "";
        sensor.Save();
        return this.FormatRequest((object) "Success");
      }
      dataSubscription.ExternalID = externalID;
      dataSubscription.ConnectionInfo1 = connectionInfo.ToStringSafe();
      switch (verb)
      {
        case "post":
          if (!new List<string>((IEnumerable<string>) new string[3]
          {
            "raw",
            "application/json",
            "application/x-www-form-urlencoded"
          }).Contains(ContentHeaderType))
            return this.FormatRequest((object) "Invalid Header Type.");
          dataSubscription.ContentHeaderType = ContentHeaderType;
          dataSubscription.ConnectionInfo2 = connectionBody;
          goto case "get";
        case "get":
          dataSubscription.verb = verb;
          dataSubscription.LastResult = "";
          dataSubscription.BrokenCount = 20;
          dataSubscription.DoRetry = false;
          dataSubscription.DoSend = true;
          dataSubscription.Save();
          break;
        default:
          return this.FormatRequest((object) "Invalid verb - use 'get' or 'post'");
      }
    }
    else if (!string.IsNullOrEmpty(connectionInfo))
    {
      dataSubscription = new ExternalDataSubscription();
      dataSubscription.AccountID = MonnitSession.CurrentCustomer.AccountID;
      dataSubscription.ExternalID = externalID;
      dataSubscription.ConnectionInfo1 = connectionInfo.ToStringSafe();
      switch (verb)
      {
        case "post":
          if (!new List<string>((IEnumerable<string>) new string[3]
          {
            "raw",
            "application/json",
            "application/x-www-form-urlencoded"
          }).Contains(ContentHeaderType))
            return this.FormatRequest((object) "Invalid Header Type.");
          dataSubscription.ContentHeaderType = ContentHeaderType;
          dataSubscription.ConnectionInfo2 = connectionBody;
          goto case "get";
        case "get":
          dataSubscription.verb = verb;
          dataSubscription.LastResult = "";
          dataSubscription.Save();
          break;
        default:
          return this.FormatRequest((object) "Invalid verb - use 'get' or 'post'");
      }
    }
    sensor.ExternalID = dataSubscription.ExternalID;
    sensor.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult ExternalDataPushRemove(long sensorID)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor.AccountID != MonnitSession.CurrentCustomer.AccountID || !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return this.FormatRequest((object) "Invalid SensorID");
    ExternalDataSubscription.LoadAllByAccountID(sensor.AccountID).FirstOrDefault<ExternalDataSubscription>()?.Delete();
    sensor.ExternalID = "";
    sensor.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult ExternalGatewayDataPush(long gatewayID)
  {
    Gateway gateway = Gateway.Load(gatewayID);
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    return gateway != null && !gateway.IsDeleted && csNet.AccountID == MonnitSession.CurrentCustomer.AccountID && MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID) ? this.FormatRequest((object) new APIGatewayDataPushConfiguration(ExternalDataSubscription.LoadByGatewayID(csNet.AccountID).FirstOrDefault<ExternalDataSubscription>())) : this.FormatRequest((object) "Invalid GatewayID");
  }

  [AuthorizeAPIPremier]
  public ActionResult ExternalGatewayDataPushSet(
    long gatewayID,
    string connectionInfo,
    string externalID,
    string verb = "get",
    string connectionBody = "",
    string HeaderType = "")
  {
    Gateway gateway = Gateway.Load(gatewayID);
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    if (gateway == null || gateway.IsDeleted || csNet.AccountID != MonnitSession.CurrentCustomer.AccountID || !MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID))
      return this.FormatRequest((object) "Invalid GatewayID");
    if (string.IsNullOrEmpty(externalID))
      externalID = "None";
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.LoadAllByAccountID(csNet.AccountID).FirstOrDefault<ExternalDataSubscription>();
    dataSubscription.AccountID = MonnitSession.CurrentCustomer.AccountID;
    if (dataSubscription != null)
    {
      if (string.IsNullOrEmpty(connectionInfo))
      {
        dataSubscription.Delete();
        gateway.ExternalID = "";
        gateway.Save();
        return this.FormatRequest((object) "Success");
      }
      dataSubscription.ExternalID = externalID;
      dataSubscription.ConnectionInfo1 = connectionInfo.ToStringSafe();
      switch (verb)
      {
        case "post":
          if (!new List<string>((IEnumerable<string>) new string[3]
          {
            "raw",
            "application/json",
            "application/x-www-form-urlencoded"
          }).Contains(HeaderType))
            return this.FormatRequest((object) "Invalid Header Type.");
          dataSubscription.ContentHeaderType = HeaderType;
          dataSubscription.ConnectionInfo2 = connectionBody;
          goto case "get";
        case "get":
          dataSubscription.verb = verb;
          dataSubscription.LastResult = "";
          dataSubscription.BrokenCount = 20;
          dataSubscription.DoRetry = false;
          dataSubscription.DoSend = true;
          dataSubscription.Save();
          break;
        default:
          return this.FormatRequest((object) "Invalid verb - use 'get' or 'post'");
      }
    }
    else if (!string.IsNullOrEmpty(connectionInfo))
    {
      dataSubscription = new ExternalDataSubscription();
      dataSubscription.AccountID = MonnitSession.CurrentCustomer.AccountID;
      dataSubscription.ExternalID = externalID;
      dataSubscription.ConnectionInfo1 = connectionInfo.ToStringSafe();
      switch (verb)
      {
        case "post":
          if (!new List<string>((IEnumerable<string>) new string[3]
          {
            "raw",
            "application/json",
            "application/x-www-form-urlencoded"
          }).Contains(HeaderType))
            return this.FormatRequest((object) "Invalid Header Type.");
          dataSubscription.ContentHeaderType = HeaderType;
          dataSubscription.ConnectionInfo2 = connectionBody;
          goto case "get";
        case "get":
          dataSubscription.verb = verb;
          dataSubscription.LastResult = "";
          dataSubscription.Save();
          break;
        default:
          return this.FormatRequest((object) "Invalid verb - use 'get' or 'post'");
      }
    }
    gateway.ExternalID = dataSubscription.ExternalID;
    gateway.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult ExternalGatewayDataPushRemove(long gatewayID)
  {
    Gateway gateway = Gateway.Load(gatewayID);
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    if (gateway == null || gateway.IsDeleted || csNet.AccountID != MonnitSession.CurrentCustomer.AccountID || !MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID))
      return this.FormatRequest((object) "Invalid GatewayID");
    ExternalDataSubscription.LoadAllByAccountID(csNet.AccountID).FirstOrDefault<ExternalDataSubscription>()?.Delete();
    gateway.ExternalID = "";
    gateway.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult WebHookGet(long? accountID)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: No account found");
    ExternalDataSubscription exdata = ExternalDataSubscription.byAccount(acct.AccountID);
    return exdata == null || exdata.ExternalDataSubscriptionID < 0L ? this.FormatRequest((object) "Invalid Request: no webhook found") : this.FormatRequest((object) new APIWebHookGet(exdata));
  }

  [AuthorizeAPIPremier]
  public ActionResult WebHookRemove(long? accountID)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    ExternalDataSubscription.LoadAllByAccountID(acct.AccountID).FirstOrDefault<ExternalDataSubscription>().LogAuditData(eAuditAction.Delete, eAuditObject.Webhook, MonnitSession.CurrentCustomer.CustomerID, acct.AccountID, "Removed webhook");
    ExternalDataSubscription.SetDeletedFlagByAccountID(acct.AccountID);
    acct.ClearDataPushes();
    return this.FormatRequest((object) "Success: Webhook Removed");
  }

  [AuthorizeAPIPremier]
  public ActionResult WebHookCreate(
    long? accountID,
    string baseUrl,
    bool? sendWithoutDataMessage,
    bool? sendRawData)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    if (baseUrl == null || string.IsNullOrEmpty(baseUrl))
      return this.FormatRequest((object) "Invalid Request: baseUrl is required.");
    MonnitSession.CurrentCustomer.Account.ExternalDataSubscription = (ExternalDataSubscription) null;
    MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID = long.MinValue;
    acct.ClearDataPushes();
    acct.ExternalDataSubscription = (ExternalDataSubscription) null;
    acct.Save();
    ExternalDataSubscription.SetDeletedFlagByAccountID(acct.AccountID);
    ExternalDataSubscription dataSubscription = new ExternalDataSubscription();
    dataSubscription.AccountID = acct.AccountID;
    dataSubscription.SendWithoutDataMessage = sendWithoutDataMessage ?? true;
    dataSubscription.SendRawData = sendRawData.GetValueOrDefault();
    dataSubscription.ConnectionInfo1 = baseUrl.ToStringSafe();
    dataSubscription.DoSend = true;
    dataSubscription.DoRetry = true;
    dataSubscription.BrokenCount = 0;
    dataSubscription.verb = "HttpPost";
    dataSubscription.ContentHeaderType = "application/json";
    dataSubscription.ExternalID = "none";
    dataSubscription.eExternalDataSubscriptionType = eExternalDataSubscriptionType.webhook;
    dataSubscription.Save();
    acct.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    acct.Save();
    acct.ClearDataPushes();
    dataSubscription.LogAuditData(eAuditAction.Create, eAuditObject.Webhook, MonnitSession.CurrentCustomer.CustomerID, acct.AccountID, "Created webhook");
    return this.FormatRequest((object) new APIWebHookGet(dataSubscription));
  }

  [AuthorizeAPIPremier]
  public ActionResult WebHookSetAuthentication(long? accountID, string username, string password)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: No account found");
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.byAccount(acct.AccountID);
    if (dataSubscription == null || dataSubscription.ExternalDataSubscriptionID < 0L)
      return this.FormatRequest((object) "Invalid Request: No webhook found");
    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
      return this.FormatRequest((object) "Invalid Request: Username and password required for authentication");
    dataSubscription.Username = username;
    dataSubscription.Password = MonnitSession.UseEncryption ? password.Encrypt() : password;
    dataSubscription.Save();
    ExternalDataSubscriptionProperty property1 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((System.Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (username))).FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (property1 == null)
    {
      property1 = new ExternalDataSubscriptionProperty();
      property1.DisplayName = "Webhook Basic Authentication Username";
      property1.Name = nameof (username);
      property1.eExternalDataSubscriptionType = eExternalDataSubscriptionType.webhook;
      property1.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property1.StringValue = username;
    property1.Save();
    ExternalDataSubscriptionProperty property2 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((System.Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (password))).FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (property2 == null)
    {
      property2 = new ExternalDataSubscriptionProperty();
      property2.DisplayName = "Webhook Basic Authentication Password";
      property2.Name = nameof (password);
      property2.eExternalDataSubscriptionType = eExternalDataSubscriptionType.webhook;
      property2.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property2.StringValue = MonnitSession.UseEncryption ? password.Encrypt() : password;
    property2.Save();
    dataSubscription.AssignProperty(property2);
    dataSubscription.AssignProperty(property1);
    if (acct != null)
      dataSubscription.LogAuditData(eAuditAction.Create, eAuditObject.Webhook, MonnitSession.CurrentCustomer.CustomerID, acct.AccountID, "Edited webhook settings");
    return this.FormatRequest((object) new APIWebHookGet(dataSubscription));
  }

  [AuthorizeAPIPremier]
  public ActionResult WebHookRemoveAuthentication(long? accountID)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: No account found");
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.byAccount(acct.AccountID);
    if (dataSubscription == null || dataSubscription.ExternalDataSubscriptionID < 0L)
      return this.FormatRequest((object) "Invalid Request: No webhook found");
    dataSubscription.Username = "";
    dataSubscription.Password = "";
    dataSubscription.Save();
    ExternalDataSubscriptionProperty property1 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((System.Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "username")).FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (property1 != null)
      dataSubscription.RemoveProperty(property1);
    ExternalDataSubscriptionProperty property2 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((System.Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "password")).FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (property2 != null)
      dataSubscription.RemoveProperty(property2);
    if (acct != null)
      dataSubscription.LogAuditData(eAuditAction.Create, eAuditObject.Webhook, MonnitSession.CurrentCustomer.CustomerID, acct.AccountID, "Edited webhook settings");
    return this.FormatRequest((object) new APIWebHookGet(dataSubscription));
  }

  [AuthorizeAPIPremier]
  public ActionResult WebHookAddCookie(long? accountID, string key, string value)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: No account found");
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.byAccount(acct.AccountID);
    if (dataSubscription == null || dataSubscription.ExternalDataSubscriptionID < 0L)
      return this.FormatRequest((object) "Invalid Request: No webhook found");
    if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
      return this.FormatRequest((object) "Invalid Request: Key and value required");
    List<ExternalDataSubscriptionProperty> list = ExternalDataSubscriptionProperty.LoadByExternalDataSubscriptionID(dataSubscription.ExternalDataSubscriptionID).Where<ExternalDataSubscriptionProperty>((System.Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "cookie")).ToList<ExternalDataSubscriptionProperty>();
    List<APIWebHookProperty> apiWebHookPropertyList = new List<APIWebHookProperty>();
    if (list.Where<ExternalDataSubscriptionProperty>((System.Func<ExternalDataSubscriptionProperty, bool>) (m => m.StringValue == key)).Count<ExternalDataSubscriptionProperty>() < 1)
    {
      ExternalDataSubscriptionProperty property = new ExternalDataSubscriptionProperty();
      property.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
      property.eExternalDataSubscriptionType = eExternalDataSubscriptionType.webhook;
      property.DisplayName = "cookie Name=Value";
      property.Name = "cookie";
      property.StringValue = key;
      property.StringValue2 = value;
      property.Save();
      dataSubscription.AssignProperty(property);
      list.Add(property);
    }
    foreach (ExternalDataSubscriptionProperty prop in list)
      apiWebHookPropertyList.Add(new APIWebHookProperty(prop));
    return this.FormatRequest((object) apiWebHookPropertyList);
  }

  [AuthorizeAPIPremier]
  public ActionResult WebHookRemoveCookie(long? accountID, long webHookPropertyID)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: No account found");
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.byAccount(acct.AccountID);
    if (dataSubscription == null || dataSubscription.ExternalDataSubscriptionID < 0L)
      return this.FormatRequest((object) "Invalid Request: No webhook found");
    ExternalDataSubscriptionProperty property = ExternalDataSubscriptionProperty.Load(webHookPropertyID);
    if (property == null || property.ExternalDataSubscriptionID != dataSubscription.ExternalDataSubscriptionID)
      return this.FormatRequest((object) "Invalid Request: No cookie found");
    dataSubscription.RemoveProperty(property);
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult WebHookCookieList(long? accountID)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: No account found");
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.byAccount(acct.AccountID);
    if (dataSubscription == null || dataSubscription.ExternalDataSubscriptionID < 0L)
      return this.FormatRequest((object) "Invalid Request: No webhook found");
    List<ExternalDataSubscriptionProperty> list = ExternalDataSubscriptionProperty.LoadByExternalDataSubscriptionID(dataSubscription.ExternalDataSubscriptionID).Where<ExternalDataSubscriptionProperty>((System.Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "cookie")).ToList<ExternalDataSubscriptionProperty>();
    List<APIWebHookProperty> apiWebHookPropertyList = new List<APIWebHookProperty>();
    foreach (ExternalDataSubscriptionProperty prop in list)
      apiWebHookPropertyList.Add(new APIWebHookProperty(prop));
    return this.FormatRequest((object) apiWebHookPropertyList);
  }

  [AuthorizeAPIPremier]
  public ActionResult WebHookAddHeader(long? accountID, string key, string value)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: No account found");
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.byAccount(acct.AccountID);
    if (dataSubscription == null || dataSubscription.ExternalDataSubscriptionID < 0L)
      return this.FormatRequest((object) "Invalid Request: No webhook found");
    if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
      return this.FormatRequest((object) "Invalid Request: Key and value required");
    List<ExternalDataSubscriptionProperty> list = ExternalDataSubscriptionProperty.LoadByExternalDataSubscriptionID(dataSubscription.ExternalDataSubscriptionID).Where<ExternalDataSubscriptionProperty>((System.Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "header")).ToList<ExternalDataSubscriptionProperty>();
    List<APIWebHookProperty> apiWebHookPropertyList = new List<APIWebHookProperty>();
    if (list.Where<ExternalDataSubscriptionProperty>((System.Func<ExternalDataSubscriptionProperty, bool>) (m => m.StringValue == key)).Count<ExternalDataSubscriptionProperty>() < 1)
    {
      ExternalDataSubscriptionProperty property = new ExternalDataSubscriptionProperty();
      property.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
      property.eExternalDataSubscriptionType = eExternalDataSubscriptionType.webhook;
      property.DisplayName = "header Name=Value";
      property.Name = "header";
      property.StringValue = key;
      property.StringValue2 = value;
      property.Save();
      dataSubscription.AssignProperty(property);
      list.Add(property);
    }
    foreach (ExternalDataSubscriptionProperty prop in list)
      apiWebHookPropertyList.Add(new APIWebHookProperty(prop));
    return this.FormatRequest((object) apiWebHookPropertyList);
  }

  [AuthorizeAPIPremier]
  public ActionResult WebHookRemoveHeader(long? accountID, long webHookPropertyID)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: No account found");
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.byAccount(acct.AccountID);
    if (dataSubscription == null || dataSubscription.ExternalDataSubscriptionID < 0L)
      return this.FormatRequest((object) "Invalid Request: No webhook found");
    ExternalDataSubscriptionProperty property = ExternalDataSubscriptionProperty.Load(webHookPropertyID);
    if (property == null || property.ExternalDataSubscriptionID != dataSubscription.ExternalDataSubscriptionID)
      return this.FormatRequest((object) "Invalid Request: No header found");
    dataSubscription.RemoveProperty(property);
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPIPremier]
  public ActionResult WebHookHeaderList(long? accountID)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: No account found");
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.byAccount(acct.AccountID);
    if (dataSubscription == null || dataSubscription.ExternalDataSubscriptionID < 0L)
      return this.FormatRequest((object) "Invalid Request: No webhook found");
    List<ExternalDataSubscriptionProperty> list = ExternalDataSubscriptionProperty.LoadByExternalDataSubscriptionID(dataSubscription.ExternalDataSubscriptionID).Where<ExternalDataSubscriptionProperty>((System.Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "header")).ToList<ExternalDataSubscriptionProperty>();
    List<APIWebHookProperty> apiWebHookPropertyList = new List<APIWebHookProperty>();
    foreach (ExternalDataSubscriptionProperty prop in list)
      apiWebHookPropertyList.Add(new APIWebHookProperty(prop));
    return this.FormatRequest((object) apiWebHookPropertyList);
  }

  [AuthorizeAPIPremier]
  public ActionResult WebHookResetBroken(long? accountID)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (MonnitSession.CurrentCustomer.CanSeeAccount(acct))
    {
      ExternalDataSubscription DBObject = ExternalDataSubscription.byAccount(acct.AccountID);
      if (DBObject != null && DBObject.ExternalDataSubscriptionID != long.MinValue)
      {
        if (DBObject.BrokenCount < 100)
          return this.FormatRequest((object) "Invalid Request: All retry attempts must be met before you may reset the retry count.");
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Webhook, MonnitSession.CurrentCustomer.CustomerID, acct.AccountID, "Reset broken count for webhook");
        DBObject.resetBroken();
        return this.FormatRequest((object) "Success");
      }
    }
    return this.FormatRequest((object) "Invalid Request:  No account found or Unathorized account  ");
  }

  [AuthorizeAPIPremier]
  public ActionResult WebHookAttempts(
    long? accountID,
    bool showOnlyErrors,
    DateTime fromDate,
    DateTime toDate,
    long? lastAttemptID)
  {
    List<Monnit.WebHookAttempts> webHookAttemptsList1 = new List<Monnit.WebHookAttempts>();
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request:  No account found or Unathorized account  ");
    if (fromDate == toDate)
    {
      fromDate = fromDate.Date;
      toDate = toDate.AddDays(1.0);
    }
    List<Monnit.WebHookAttempts> webHookAttemptsList2 = Monnit.WebHookAttempts.LoadAllBySubscriptionAndDate(acct.ExternalDataSubscriptionID, showOnlyErrors, fromDate, toDate.AddDays(1.0), lastAttemptID.GetValueOrDefault(), 1000);
    List<APIWebHookAttempts> apiWebHookAttemptsList = new List<APIWebHookAttempts>();
    foreach (Monnit.WebHookAttempts webHookAttempts in webHookAttemptsList2)
    {
      long attemptId = webHookAttempts.AttemptID;
      DateTime createDate = webHookAttempts.CreateDate;
      string url = webHookAttempts.Url;
      string stringSafe = webHookAttempts.Status.ToStringSafe();
      int attemptCount = webHookAttempts.AttemptCount;
      bool retry = webHookAttempts.Retry;
      DateTime processDate = webHookAttempts.ProcessDate;
      apiWebHookAttemptsList.Add(new APIWebHookAttempts(attemptId, createDate, url, processDate, stringSafe, attemptCount, retry));
    }
    return this.FormatRequest((object) apiWebHookAttemptsList);
  }

  [AuthorizeAPIPremier]
  public ActionResult WebHookSubAccountFailedAttempts(
    long? accountID,
    DateTime fromDate,
    DateTime toDate)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    if ((toDate - fromDate).TotalHours > 24.0)
      return this.FormatRequest((object) "Invalid Request: Date period too large. 24 hour max");
    if (fromDate == toDate)
    {
      fromDate = fromDate.Date;
      toDate = toDate.AddDays(1.0);
    }
    List<WebHookFailures> webHookFailuresList = WebHookFailures.LoadSubAccountErrorsBySubscriptionAndDate(acct.AccountID, fromDate, toDate, 1000);
    List<APIWebHookFailures> apiWebHookFailuresList = new List<APIWebHookFailures>();
    foreach (WebHookFailures webHookFailures in webHookFailuresList)
    {
      long attemptId = webHookFailures.AttemptID;
      DateTime createDate = webHookFailures.CreateDate;
      string url = webHookFailures.Url;
      string stringSafe = webHookFailures.Status.ToStringSafe();
      int attemptCount = webHookFailures.AttemptCount;
      bool retry = webHookFailures.Retry;
      DateTime processDate = webHookFailures.ProcessDate;
      long accountId = webHookFailures.AccountID;
      apiWebHookFailuresList.Add(new APIWebHookFailures(attemptId, createDate, url, processDate, stringSafe, attemptCount, retry, accountId));
    }
    return this.FormatRequest((object) apiWebHookFailuresList);
  }

  [AuthorizeAPIPremier]
  public ActionResult WebHookAttemptBody(long attemptID)
  {
    ExternalDataSubscriptionAttempt subscriptionAttempt = ExternalDataSubscriptionAttempt.Load(attemptID);
    if (subscriptionAttempt != null)
    {
      if (MonnitSession.CurrentCustomer.CanSeeAccount(Account.LoadByExternalDataSubscriptionID(subscriptionAttempt.ExternalDataSubscriptionID)))
      {
        try
        {
          return this.FormatRequest((object) new APIWebHookAttemptBody(subscriptionAttempt.body));
        }
        catch (Exception ex)
        {
          return (ActionResult) this.Content("Failed");
        }
      }
    }
    return this.FormatRequest((object) "Invalid attemptID ");
  }

  [AuthorizeAPIPremier]
  public ActionResult WebHookResend(long attemptID)
  {
    ExternalDataSubscriptionAttempt subscriptionAttempt = ExternalDataSubscriptionAttempt.Load(attemptID);
    if (subscriptionAttempt == null)
      return this.FormatRequest((object) "Invalid attemptID ");
    Account acct = Account.LoadByExternalDataSubscriptionID(subscriptionAttempt.ExternalDataSubscriptionID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request:  No account found or Unathorized account  ");
    if (subscriptionAttempt.AttemptCount > 9)
    {
      subscriptionAttempt.Status = eExternalDataSubscriptionStatus.NoRetry;
      subscriptionAttempt.Save();
      return this.FormatRequest((object) "This attempt has been reached its maximum retry count of 10");
    }
    try
    {
      if (acct != null)
        AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, subscriptionAttempt.ExternalDataSubscriptionID, eAuditAction.Related_Modify, eAuditObject.Webhook, subscriptionAttempt.JsonStringify(), acct.AccountID, "Queued webhook attempt to retry sending");
      subscriptionAttempt.Status = eExternalDataSubscriptionStatus.Retry;
      subscriptionAttempt.DoRetry = true;
      subscriptionAttempt.Save();
      return this.FormatRequest((object) "Retry attempt queued.");
    }
    catch (Exception ex)
    {
      ex.Log($"WebHookResend,Retry attempt queued, attemptID = {attemptID}");
      return this.FormatRequest((object) "Failed");
    }
  }

  [AuthorizeAPIPremier]
  public ActionResult WebhookListNotificationSettings(long? accountID)
  {
    Account acct = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: no account found");
    ExternalSubscriptionPreference pref = ExternalSubscriptionPreference.LoadByAccountId(acct.AccountID);
    return pref == null ? this.FormatRequest((object) "Invalid Request: No settings found") : this.FormatRequest((object) new APINotificationSettings(pref));
  }

  [AuthorizeAPIPremier]
  public ActionResult WebhookNotificationSettingsSet(
    long? accountID,
    long UserIDToBeNotified,
    int brokenCountThreshold)
  {
    Account account = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(account))
      return this.FormatRequest((object) "Invalid Request: no account found");
    Customer customer = Customer.Load(UserIDToBeNotified);
    if (customer == null || !customer.CanSeeAccount(account))
      return this.FormatRequest((object) "Invalid Request: User to be notified is invalid");
    if (brokenCountThreshold < 1)
      brokenCountThreshold = 1;
    if (brokenCountThreshold > 100)
      brokenCountThreshold = 100;
    ExternalSubscriptionPreference pref = ExternalSubscriptionPreference.LoadByAccountId(account.AccountID);
    if (pref == null)
    {
      pref = new ExternalSubscriptionPreference();
      pref.AccountID = account.AccountID;
    }
    pref.UserId = UserIDToBeNotified;
    pref.UsersBrokenCountLimit = brokenCountThreshold;
    pref.Save();
    account.LogAuditData(eAuditAction.Create, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, pref.JsonStringify(), account.AccountID, "Edited webhook notification settings");
    return this.FormatRequest((object) new APINotificationSettings(pref));
  }

  [AuthorizeAPIPremier]
  public ActionResult GetHHTDeviceUtilization(long accountID, DateTime FromDate, DateTime ToDate)
  {
    if (ToDate.Subtract(FromDate).Days > 366)
      return this.FormatRequest((object) "Invalid Request: Only 365 Days of data allowed");
    Account acct = Account.Load(accountID);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: No account found");
    DataTable dataTable = new DataTable();
    List<APIHHTDeviceUtilization> deviceUtilizationList = new List<APIHHTDeviceUtilization>();
    using (IDbCommand dbCommand = Database.GetDBCommand("Custom_Hadli_DeviceUtilization_Export"))
    {
      dbCommand.CommandType = CommandType.StoredProcedure;
      dbCommand.Parameters.Add((object) Database.GetParameter("@AccountID", (object) acct.AccountID));
      dbCommand.Parameters.Add((object) Database.GetParameter("@FromDate", (object) FromDate));
      dbCommand.Parameters.Add((object) Database.GetParameter("@ToDate", (object) ToDate));
      dbCommand.Parameters.Add((object) Database.GetParameter("@ExcludeSubAccounts", (object) true));
      DataSet dataSet = Database.ExecuteQuery(dbCommand);
      if (dataSet.Tables.Count > 0)
      {
        DataTable table = dataSet.Tables[0];
        if (table.Rows.Count == 0)
          return this.FormatRequest((object) "Invalid Request: No data found");
        foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        {
          APIHHTDeviceUtilization deviceUtilization = new APIHHTDeviceUtilization(row);
          deviceUtilizationList.Add(deviceUtilization);
        }
      }
    }
    return this.FormatRequest((object) deviceUtilizationList);
  }

  [AuthorizeAPIPremier]
  public ActionResult GetHHTHumidityTemperatureHistory(
    long sensorID,
    DateTime FromDate,
    DateTime ToDate)
  {
    if (ToDate.Subtract(FromDate).Days > 366)
      return this.FormatRequest((object) "Invalid Request: Only 365 Days of data allowed");
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Invalid Request: No device found");
    if (sensor.StartDate > FromDate)
      FromDate = Monnit.TimeZone.GetLocalTimeById(sensor.StartDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DataTable dataTable1 = new DataTable();
    DataTable dataTable2 = new DataTable();
    List<APIHHTHumidityDeviceData> humidityDeviceDataList = new List<APIHHTHumidityDeviceData>();
    using (IDbCommand dbCommand = Database.GetDBCommand("Custom_Hadli_DataExport_HTSensor"))
    {
      dbCommand.CommandType = CommandType.StoredProcedure;
      dbCommand.Parameters.Add((object) Database.GetParameter("@SensorID", (object) sensor.SensorID));
      dbCommand.Parameters.Add((object) Database.GetParameter("@FromDate", (object) FromDate));
      dbCommand.Parameters.Add((object) Database.GetParameter("@ToDate", (object) ToDate));
      DataSet dataSet = Database.ExecuteQuery(dbCommand);
      if (dataSet.Tables.Count > 0)
      {
        DataTable table = dataSet.Tables[0];
        if (table.Rows.Count == 0)
          return this.FormatRequest((object) "Invalid Request: No data found");
        foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        {
          APIHHTHumidityDeviceData humidityDeviceData = new APIHHTHumidityDeviceData(row);
          humidityDeviceDataList.Add(humidityDeviceData);
        }
      }
    }
    return this.FormatRequest((object) humidityDeviceDataList);
  }

  [AuthorizeAPIPremier]
  public ActionResult GetHHTPowerEventHistory(long accountID, DateTime FromDate, DateTime ToDate)
  {
    if (ToDate.Subtract(FromDate).Days > 366)
      return this.FormatRequest((object) "Invalid Request: Only 365 Days of data allowed");
    Account acct = Account.Load(accountID);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return this.FormatRequest((object) "Invalid Request: No account found");
    DataTable dataTable = new DataTable();
    List<APIHHTPowerEventDetail> powerEventDetailList = new List<APIHHTPowerEventDetail>();
    using (IDbCommand dbCommand = Database.GetDBCommand("Custom_Hadli_PowerOutageDetails"))
    {
      dbCommand.CommandType = CommandType.StoredProcedure;
      dbCommand.Parameters.Add((object) Database.GetParameter("@AccountID", (object) acct.AccountID));
      dbCommand.Parameters.Add((object) Database.GetParameter("@FromDate", (object) FromDate));
      dbCommand.Parameters.Add((object) Database.GetParameter("@ToDate", (object) ToDate));
      dbCommand.Parameters.Add((object) Database.GetParameter("@ExcludeSubAccounts", (object) true));
      DataSet dataSet = Database.ExecuteQuery(dbCommand);
      if (dataSet.Tables.Count > 0)
      {
        DataTable table = dataSet.Tables[0];
        if (table.Rows.Count == 0)
          return this.FormatRequest((object) "Invalid Request: No data found");
        foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        {
          APIHHTPowerEventDetail powerEventDetail = new APIHHTPowerEventDetail(row);
          powerEventDetailList.Add(powerEventDetail);
        }
      }
    }
    return this.FormatRequest((object) powerEventDetailList);
  }

  public ActionResult ExpressSensorList(long? csnetID, long? gatewayID)
  {
    try
    {
      long? nullable = csnetID;
      long num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue)
        gatewayID = csnetID;
      return this.FormatRequest((object) Sensor.LoadByCsNetID(Gateway.Load(gatewayID.Value).CSNetID).Select<Sensor, APIExpressSensor>((System.Func<Sensor, APIExpressSensor>) (s => new APIExpressSensor(s, gatewayID.Value))).ToList<APIExpressSensor>());
    }
    catch (Exception ex)
    {
      ex.Log("APIController.ExpressSensorList");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  public ActionResult ForceResetForShipping(long sensorID, long? reportInterval)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null)
      return this.FormatRequest((object) "Invalid SensorID");
    sensor.SetDefaults(false);
    if (sensor.ReportInterval == 120.0 && (reportInterval ?? 120L) != 120L)
      sensor.ReportInterval = (double) (reportInterval ?? 120L);
    sensor.MarkClean(false);
    sensor.Save();
    return this.FormatRequest((object) "Success");
  }

  public ActionResult PowerSourceList()
  {
    try
    {
      return this.FormatRequest((object) PowerSource.LoadAll().Select<PowerSource, APINameID>((System.Func<PowerSource, APINameID>) (obj => new APINameID(obj.PowerSourceID, obj.Name))).ToList<APINameID>());
    }
    catch (Exception ex)
    {
      ex.Log("APIController.PowerSources");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  public ActionResult SensorTypeList()
  {
    try
    {
      return this.FormatRequest((object) SensorType.LoadAll().Select<SensorType, APINameID>((System.Func<SensorType, APINameID>) (obj => new APINameID(obj.SensorTypeID, obj.Name))).ToList<APINameID>());
    }
    catch (Exception ex)
    {
      ex.Log("APIController.SensorTypeList");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  [AuthorizeAPI]
  public ActionResult ApplicationList()
  {
    return MonnitSession.IsCurrentCustomerMonnitAdmin ? this.FormatRequest((object) MonnitApplication.LoadAll().Select<MonnitApplication, APINameID>((System.Func<MonnitApplication, APINameID>) (obj => new APINameID(obj.ApplicationID, obj.ApplicationName))).ToList<APINameID>()) : this.FormatRequest((object) "Request Failed");
  }

  public ActionResult SensorExists(long sensorID)
  {
    try
    {
      Sensor sensor = Sensor.Load(sensorID);
      return sensor == null || sensor != null && sensor.IsDeleted ? this.FormatRequest((object) false) : this.FormatRequest((object) true);
    }
    catch (Exception ex)
    {
      ex.Log("APIController.SensorExists");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  public ActionResult CableExists(long cableID)
  {
    try
    {
      return Cable.Load(cableID) == null ? this.FormatRequest((object) false) : this.FormatRequest((object) true);
    }
    catch (Exception ex)
    {
      ex.Log("APIController.CableExists");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  public ActionResult CanReprogram(long sensorID)
  {
    try
    {
      Sensor sensor = Sensor.Load(sensorID);
      if (sensor == null)
        return this.FormatRequest((object) false);
      int num = ConfigData.AppSettings("ReprogramTimeLimit").ToInt();
      return DateTime.UtcNow < sensor.CreateDate.AddHours((double) num) ? this.FormatRequest((object) true) : this.FormatRequest((object) false);
    }
    catch (Exception ex)
    {
      ex.Log("APIController.CanReprogram");
    }
    return this.FormatRequest((object) false);
  }

  [AuthorizeAPI]
  public ActionResult SaveSensor(
    long monnitApplicationID,
    long sensorID,
    string firmwareVersion,
    long powerSourceID,
    string radioBand,
    long sensorTypeID,
    string generation,
    string sku,
    bool? isCableEnabled)
  {
    try
    {
      Sensor DBObject = Sensor.Load(sensorID);
      if (DBObject == null)
      {
        if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Program")))
        {
          Sensor sensor = new Sensor();
          sensor.GenerationType = generation;
          sensor.SensorID = sensorID;
          sensor.ReportInterval = 0.17;
          sensor.ActiveStateInterval = 0.17;
          sensor.MinimumCommunicationFrequency = 0;
          sensor.ApplicationID = monnitApplicationID;
          sensor.FirmwareVersion = firmwareVersion;
          sensor.PowerSourceID = powerSourceID;
          sensor.RadioBand = radioBand;
          sensor.LastCommunicationDate = new DateTime(2099, 1, 1);
          sensor.IsActive = true;
          sensor.IsSleeping = true;
          sensor.SensorTypeID = sensorTypeID;
          sensor.SensorhoodID = 0L;
          sensor.SKU = sku;
          sensor.IsCableEnabled = isCableEnabled.GetValueOrDefault();
          try
          {
            sensor.SetDefaults(false);
          }
          catch (Exception ex)
          {
            ex.Log($"APIController.SaveSensor SetDefaults Failed for SensorID: {sensorID} SensorTypeID: {sensorTypeID}  ");
          }
          sensor.RemoveAttributes(sensor.SensorID);
          sensor.MarkClean(false);
          sensor.SensorName = $"{sensor.MonnitApplication.ApplicationName} - {sensorID}";
          CSNet csNet = CSNet.Load(ConfigData.AppSettings("DefaultCSNetID").ToLong());
          sensor.AccountID = csNet.AccountID;
          sensor.CSNetID = csNet.CSNetID;
          sensor.ForceInsert();
          new ProgrammerAudit()
          {
            CustomerID = MonnitSession.CurrentCustomer.CustomerID,
            SensorID = sensorID,
            ApplicationID = monnitApplicationID,
            ProgramTimeStamp = DateTime.UtcNow,
            Note = "Sensor flash"
          }.Save();
          return this.FormatRequest((object) true);
        }
      }
      else if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_ReProgram")))
      {
        try
        {
          DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "ResetSensorForShipping " + DBObject.SensorID.ToString());
          DBObject.ResetSensorForShipping();
        }
        catch (Exception ex)
        {
          ex.Log($"APIController.SaveSensor(monnitApplicationID=[{monnitApplicationID}], sensorID=[{sensorID}], firmwareVersion=[{firmwareVersion}], powerSourceID=[{powerSourceID}], radioBand=[{radioBand}], sensorTypeID=[{sensorTypeID}], generation=[{generation}], sku=[{sku}], isCableEnabled=[{isCableEnabled}]) -> Sensor.ResetSensorForShipping(sensorID=[{sensorID}])");
        }
        Sensor sensor = new Sensor();
        sensor.SensorID = sensorID;
        sensor.ReportInterval = 0.17;
        sensor.ActiveStateInterval = 0.17;
        sensor.MinimumCommunicationFrequency = 0;
        sensor.ApplicationID = monnitApplicationID;
        sensor.FirmwareVersion = firmwareVersion;
        sensor.PowerSourceID = powerSourceID;
        sensor.RadioBand = radioBand;
        sensor.LastCommunicationDate = new DateTime(2099, 1, 1);
        sensor.IsActive = true;
        sensor.IsSleeping = true;
        sensor.SensorTypeID = sensorTypeID;
        sensor.SensorhoodID = 0L;
        sensor.GenerationType = generation;
        sensor.SKU = sku;
        sensor.IsCableEnabled = isCableEnabled.GetValueOrDefault();
        try
        {
          sensor.SetDefaults(false);
        }
        catch (Exception ex)
        {
          ex.Log($"APIController.SaveSensor SetDefaults Failed for SenosrID: {sensorID} SensorTypeID: {sensorTypeID}  ");
        }
        sensor.RemoveAttributes(sensor.SensorID);
        sensor.MarkClean(false);
        sensor.SensorName = $"{sensor.MonnitApplication.ApplicationName} - {sensorID}";
        CSNet csNet = CSNet.Load(ConfigData.AppSettings("DefaultCSNetID").ToLong());
        sensor.AccountID = csNet.AccountID;
        sensor.CSNetID = csNet.CSNetID;
        DBObject.ResetLastCommunicationDate();
        sensor.ExternalID = DBObject.ExternalID;
        sensor.Save();
        new ProgrammerAudit()
        {
          CustomerID = MonnitSession.CurrentCustomer.CustomerID,
          SensorID = sensorID,
          ApplicationID = monnitApplicationID,
          ProgramTimeStamp = DateTime.UtcNow,
          Note = "Sensor Reflash"
        }.Save();
        return this.FormatRequest((object) true);
      }
    }
    catch (Exception ex)
    {
      ex.Log("APIController.SaveSensor ");
    }
    return this.FormatRequest((object) "Unable to save sensor");
  }

  [AuthorizeAPI]
  public ActionResult SaveCable(
    long applicationID,
    long cableID,
    int cableMinorRevision,
    int cableMajorRevision,
    string sku)
  {
    try
    {
      Cable cable = Cable.Load(cableID);
      if (cable == null)
      {
        if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Program")))
        {
          new Cable()
          {
            CableID = cableID,
            CreateDate = DateTime.UtcNow,
            SKU = sku,
            ApplicationID = applicationID,
            CableMinorRevision = cableMinorRevision,
            CableMajorRevision = cableMajorRevision
          }.ForceInsert();
          new ProgrammerAudit()
          {
            CustomerID = MonnitSession.CurrentCustomer.CustomerID,
            CableID = cableID,
            ApplicationID = applicationID,
            ProgramTimeStamp = DateTime.UtcNow,
            Note = "Cable Flash"
          }.Save();
          return this.FormatRequest((object) true);
        }
      }
      else if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_ReProgram")))
      {
        cable.SKU = sku;
        cable.ApplicationID = applicationID;
        cable.CableMinorRevision = cableMinorRevision;
        cable.CableMajorRevision = cableMajorRevision;
        cable.Save();
        new ProgrammerAudit()
        {
          CustomerID = MonnitSession.CurrentCustomer.CustomerID,
          CableID = cableID,
          ApplicationID = applicationID,
          ProgramTimeStamp = DateTime.UtcNow,
          Note = "Cable Reflash"
        }.Save();
        return this.FormatRequest((object) true);
      }
    }
    catch (Exception ex)
    {
      ex.Log("APIController.SaveCable ");
    }
    return this.FormatRequest((object) "Unable to save cable");
  }

  public ActionResult GatewayTypeList()
  {
    try
    {
      return this.FormatRequest((object) GatewayType.LoadAll().Select<GatewayType, APINameID>((System.Func<GatewayType, APINameID>) (obj => new APINameID(obj.GatewayTypeID, obj.Name))).ToList<APINameID>());
    }
    catch (Exception ex)
    {
      ex.Log("APIController.GatewayTypeList");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  public ActionResult GetGatewayInfo(long gatewayID, string checkDigit)
  {
    if (!MonnitUtil.ValidateCheckDigit(gatewayID, checkDigit))
      return this.FormatRequest((object) "Check digit did not match.");
    Gateway gateway = Gateway.Load(gatewayID);
    if (gateway == null || gateway != null && gateway.IsDeleted)
      return this.FormatRequest((object) "Invalid GatewayID");
    CSNet network = CSNet.Load(gateway.CSNetID);
    if (network == null)
      return this.FormatRequest((object) "Invalid: Network is  null");
    Account account = Account.Load(network.AccountID);
    return account == null ? this.FormatRequest((object) "Invalid: Account is  null") : this.FormatRequest((object) new APIGatewayNetworkAcccount(gateway, network, account));
  }

  public ActionResult GatewayExists(long gatewayID)
  {
    try
    {
      return this.FormatRequest((object) (Gateway.Load(gatewayID) != null));
    }
    catch (Exception ex)
    {
      ex.Log("APIController.GatewayExists");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  [AuthorizeAPI]
  public ActionResult SaveGateway(
    long gatewayID,
    long gatewayTypeID,
    long sensorID,
    string firmwareVersion,
    string radioBand,
    string gatewayFirmwareVersion,
    string macAddress,
    long? powerSourceID,
    string generation,
    string sku)
  {
    try
    {
      Gateway gateway1 = Gateway.Load(gatewayID);
      if (gateway1 == null)
      {
        if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Program")))
        {
          Gateway gateway2 = new Gateway();
          gateway2.GatewayID = gatewayID;
          gateway2.GatewayTypeID = gatewayTypeID;
          gateway2.SensorID = sensorID;
          gateway2.RadioBand = radioBand;
          gateway2.GenerationType = generation;
          gateway2.APNFirmwareVersion = firmwareVersion;
          gateway2.GatewayFirmwareVersion = gatewayFirmwareVersion;
          gateway2.MacAddress = macAddress;
          gateway2.LastCommunicationDate = DateTime.MinValue;
          gateway2.PowerSourceID = powerSourceID ?? 3L;
          gateway2.SKU = sku;
          if (gateway2.PowerSourceID == long.MinValue)
            gateway2.PowerSourceID = 3L;
          if (macAddress != "")
          {
            Gateway gateway3 = Gateway.LoadByMAC(gateway2.MacAddress);
            if (gateway3 != null && (gateway1 == null || gateway1.GatewayID != gateway3.GatewayID))
            {
              gateway3.MacAddress = "";
              gateway3.Save();
            }
          }
          gateway2.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
          GatewayType Type = GatewayType.Load(gateway2.GatewayTypeID);
          gateway2.Name = $"{Type.Name} - {gatewayID}";
          gateway2.ResetToDefault(false, Type);
          gateway2.MarkClean(false);
          gateway2.ForceInsert();
          new ProgrammerAudit()
          {
            CustomerID = MonnitSession.CurrentCustomer.CustomerID,
            GatewayID = gatewayID,
            GatewayTypeID = gatewayTypeID,
            ProgramTimeStamp = DateTime.UtcNow,
            Note = "Gateway flash"
          }.Save();
          return this.FormatRequest((object) true);
        }
      }
      else if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_ReProgram")))
      {
        Gateway gateway4 = new Gateway();
        gateway4.GatewayID = gatewayID;
        gateway4.GatewayTypeID = gatewayTypeID;
        gateway4.SensorID = sensorID;
        gateway4.RadioBand = radioBand;
        gateway4.GenerationType = generation;
        gateway4.APNFirmwareVersion = firmwareVersion;
        gateway4.GatewayFirmwareVersion = gatewayFirmwareVersion;
        gateway4.MacAddress = macAddress;
        gateway4.LastCommunicationDate = DateTime.MinValue;
        gateway4.PowerSourceID = powerSourceID ?? 3L;
        gateway4.SKU = sku;
        if (gateway4.PowerSourceID == long.MinValue)
          gateway4.PowerSourceID = 3L;
        if (macAddress != "")
        {
          Gateway gateway5 = Gateway.LoadByMAC(gateway4.MacAddress);
          if (gateway5 != null && (gateway1 == null || gateway1.GatewayID != gateway5.GatewayID))
          {
            gateway5.MacAddress = "";
            gateway5.Save();
          }
        }
        gateway4.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
        GatewayType Type = GatewayType.Load(gateway4.GatewayTypeID);
        gateway4.Name = $"{Type.Name} - {gatewayID}";
        gateway4.ResetToDefault(false, Type);
        gateway4.MarkClean(false);
        gateway4.PrepFullSave();
        gateway4.Save();
        new ProgrammerAudit()
        {
          CustomerID = MonnitSession.CurrentCustomer.CustomerID,
          GatewayID = gatewayID,
          GatewayTypeID = gatewayTypeID,
          ProgramTimeStamp = DateTime.UtcNow,
          Note = "Gateway Reflash"
        }.Save();
        return this.FormatRequest((object) true);
      }
    }
    catch (Exception ex)
    {
      ex.Log("APIController.SaveGateway ");
    }
    return this.FormatRequest((object) "Unable to save Gateway");
  }

  [AuthorizeAPI]
  public ActionResult UpdateGatewayFirmware(long gatewayID, string firmware)
  {
    try
    {
      Gateway gateway = Gateway.Load(gatewayID);
      if (!MonnitSession.IsAuthorizedForAccount(CSNet.Load(gateway.CSNetID).AccountID))
        return this.FormatRequest((object) "This user does not own this Gateway");
      gateway.APNFirmwareVersion = firmware;
      gateway.MarkClean(true);
      return this.FormatRequest((object) true);
    }
    catch (Exception ex)
    {
      ex.Log("APIController.UpdateGatewayFirmware ");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  [AuthorizeAPI]
  public ActionResult UpdateSensorFirmware(long sensorID, string firmware)
  {
    try
    {
      Sensor sensor = Sensor.Load(sensorID);
      if (sensor != null)
      {
        if (!MonnitSession.IsAuthorizedForAccount(CSNet.Load(sensor.CSNetID).AccountID))
          return this.FormatRequest((object) "This user does not own this sensor");
        sensor.FirmwareVersion = firmware;
        sensor.MarkClean(true);
        return this.FormatRequest((object) true);
      }
    }
    catch (Exception ex)
    {
      ex.Log("APIController.UpdateSensorFirmware ");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  public ActionResult UpdateGatewayTest(
    long gatewayID,
    string macaddress,
    bool? sendResetNetworkRequest,
    bool? isDirty,
    bool? retreiveNetworkInfo,
    bool? sendSensorInterpretor,
    bool? forceToBootLoader,
    bool? requestConfigPage)
  {
    try
    {
      Gateway gateway = Gateway.Load(gatewayID);
      gateway.MacAddress = !string.IsNullOrEmpty(macaddress) ? macaddress : "000000000000";
      if (sendResetNetworkRequest.HasValue)
        gateway.SendResetNetworkRequest = sendResetNetworkRequest.Value;
      if (isDirty.HasValue)
        gateway.IsDirty = isDirty.Value;
      if (retreiveNetworkInfo.HasValue)
        gateway.RetreiveNetworkInfo = retreiveNetworkInfo.Value;
      if (sendSensorInterpretor.HasValue)
        gateway.SendSensorInterpretor = sendSensorInterpretor.Value;
      if (forceToBootLoader.HasValue)
        gateway.ForceToBootloader = forceToBootLoader.Value;
      if (requestConfigPage.HasValue)
        gateway.RequestConfigPage = requestConfigPage.Value;
      gateway.Save();
      return this.FormatRequest((object) "Success");
    }
    catch (Exception ex)
    {
      ex.Log("APIController.UpdateGatewayTest");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  public ActionResult GatewayLatestVersion(long gatewayTypeID)
  {
    try
    {
      return this.FormatRequest((object) GatewayType.Load(gatewayTypeID).LatestGatewayVersion);
    }
    catch (Exception ex)
    {
      ex.Log("APIController.GatewayLatestVersion");
    }
    return this.FormatRequest((object) "");
  }

  [AuthorizeAPI]
  public ActionResult SetGatewayMacAddress(long gatewayID, string mac)
  {
    ProgrammerAudit programmerAudit = new ProgrammerAudit();
    try
    {
      programmerAudit.GatewayID = gatewayID;
      programmerAudit.Note = "set macAddress";
      programmerAudit.ProgramTimeStamp = DateTime.UtcNow;
      Gateway gateway = Gateway.Load(gatewayID);
      gateway.MacAddress = mac;
      gateway.Save();
      return this.FormatRequest((object) true);
    }
    catch (Exception ex)
    {
      ex.Log("APIController.SaveGateway");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  public ActionResult LookUpSensor(long sensorID, string checkDigit)
  {
    if (!MonnitUtil.ValidateCheckDigit(sensorID, checkDigit))
      return this.FormatRequest((object) "Check digit did not match.");
    Sensor sensor = Sensor.Load(sensorID);
    return sensor != null && !sensor.IsDeleted ? this.FormatRequest((object) new APILookUpSensor(sensor)) : this.FormatRequest((object) "Invalid SensorID");
  }

  public ActionResult LookUpGateway(long gatewayID, string checkDigit)
  {
    if (!MonnitUtil.ValidateCheckDigit(gatewayID, checkDigit))
      return this.FormatRequest((object) "Check digit did not match.");
    Gateway gateway = Gateway.Load(gatewayID);
    return gateway != null && !gateway.IsDeleted ? this.FormatRequest((object) new APILookUpGateway(gateway)) : this.FormatRequest((object) "Invalid GatewayID");
  }

  public ActionResult LookUpWiFiGateway(long sensorID, string checkDigit)
  {
    if (!MonnitUtil.ValidateCheckDigit(sensorID, checkDigit))
      return this.FormatRequest((object) "Check digit did not match.");
    Gateway gateway = Gateway.LoadBySensorID(sensorID);
    return gateway != null && !gateway.IsDeleted ? this.FormatRequest((object) new APILookUpGateway(gateway)) : this.FormatRequest((object) "Invalid SensorID");
  }

  public ActionResult LookUpCable(long cableID)
  {
    Cable cable = Cable.Load(cableID);
    return cable != null ? this.FormatRequest((object) new APILookUpCable(cable)) : this.FormatRequest((object) "Invalid CableID");
  }

  public ActionResult TwilioMessageCallback(
    long notificationRecordedID,
    string messageStatus,
    string errorCode)
  {
    NotificationRecorded notificationRecorded = NotificationRecorded.Load(notificationRecordedID);
    if (notificationRecorded == null)
      return (ActionResult) this.Content("Failed to Updated (Not Found)");
    notificationRecorded.Status = $"{messageStatus}: {errorCode}";
    notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
    notificationRecorded.Save();
    return (ActionResult) this.Content("Successfully Updated");
  }

  public ActionResult TwilioCallCallback(
    long notificationRecordedID,
    string callStatus,
    string to,
    int callDuration)
  {
    NotificationRecorded notificationRecorded = NotificationRecorded.Load(notificationRecordedID);
    if (notificationRecorded == null)
      return (ActionResult) this.Content("Failed to Updated (Not Found)");
    notificationRecorded.Status = $"{callStatus}: {to} {callDuration} seconds";
    notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
    notificationRecorded.Save();
    return (ActionResult) this.Content("Successfully Updated");
  }

  public ActionResult PhoneVerificationCallback(
    long notificationRecordedID,
    string callStatus,
    string to,
    int callDuration)
  {
    NotificationRecorded notificationRecorded = NotificationRecorded.Load(notificationRecordedID);
    if (notificationRecorded == null)
      return (ActionResult) this.Content("Failed to Updated (Not Found)");
    notificationRecorded.Status = $"{callStatus}: {to} {callDuration} seconds";
    notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
    notificationRecorded.Save();
    return (ActionResult) this.RedirectPermanent("/Settings/UserNotification/" + notificationRecorded.CustomerID.ToString());
  }

  public ActionResult TestParams()
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (string key in this.Request.Params.Keys)
      stringBuilder.AppendFormat("[{0}:{1}] ", (object) key, (object) this.Request[key]);
    string str = stringBuilder.ToString();
    ExceptionLog.Log(new Exception("Test Params " + this.Request.HttpMethod, new Exception(str)));
    return (ActionResult) this.Content(str);
  }

  public ActionResult CustomCompanyList()
  {
    try
    {
      return this.FormatRequest((object) CustomCompany.LoadAll().Select<CustomCompany, APICustomCompany>((System.Func<CustomCompany, APICustomCompany>) (sens => new APICustomCompany(sens))).ToList<APICustomCompany>());
    }
    catch (Exception ex)
    {
      ex.Log("APIController.CustomCompanyList");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  public ActionResult CustomCompanySensorDefaults(long companyID)
  {
    try
    {
      return this.FormatRequest((object) CustomCompanySensor.LoadSensorByCompanyID(companyID).Select<CustomCompanySensor, APICustomCompanySensor>((System.Func<CustomCompanySensor, APICustomCompanySensor>) (sens => new APICustomCompanySensor(sens))).ToList<APICustomCompanySensor>());
    }
    catch (Exception ex)
    {
      ex.Log("APIController.CustomCompanySensorDefaults");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  public ActionResult CustomCompanyGatewayDefaults(long companyID)
  {
    try
    {
      return this.FormatRequest((object) CustomCompanyGateway.LoadGatewayByCompanyID(companyID).Select<CustomCompanyGateway, APICustomCompanyGateway>((System.Func<CustomCompanyGateway, APICustomCompanyGateway>) (gate => new APICustomCompanyGateway(gate))).ToList<APICustomCompanyGateway>());
    }
    catch (Exception ex)
    {
      ex.Log("APIController.CustomCompanyGatewayDefaults");
    }
    return this.FormatRequest((object) "Request Failed");
  }

  public ActionResult CustomCompanyForceResetForShipping(long sensorID, long companyID)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null)
      return this.FormatRequest((object) "Invalid SensorID");
    sensor.SetCustomCompanyDefaults(false, companyID);
    sensor.MarkClean(false);
    sensor.Save();
    return this.FormatRequest((object) "Success");
  }

  public ActionResult CustomCompanyForceResetForGatewayShipping(long gatewayID, long companyID)
  {
    Gateway gateway = Gateway.Load(gatewayID);
    if (gateway == null)
      return this.FormatRequest((object) "Invalid GatewayID");
    gateway.SetCustomCompanyDefaults(false, gateway.GatewayType, companyID);
    gateway.MarkClean(false);
    gateway.Save();
    return this.FormatRequest((object) "Success");
  }

  public ActionResult NextAvailableSensorID()
  {
    SensorIDSequence sensorIdSequence = new SensorIDSequence();
    sensorIdSequence.CreatedOnDate = DateTime.UtcNow;
    sensorIdSequence.Save();
    return this.FormatRequest((object) sensorIdSequence.SensorIDSequenceID);
  }

  public ActionResult NextAvailableGatewayID()
  {
    GatewayIDSequence gatewayIdSequence = new GatewayIDSequence();
    gatewayIdSequence.CreatedOnDate = DateTime.UtcNow;
    gatewayIdSequence.Save();
    return this.FormatRequest((object) gatewayIdSequence.GatewayIDSequenceID);
  }

  [AuthorizeAPI]
  public ActionResult SensorSetCalibrationCertificate(
    long sensorID,
    string labName,
    string certificationCode,
    DateTime certifiedDate,
    bool? ReadProfileConfig2,
    bool? ReadProfileConfig1)
  {
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null || sensor.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Invalid SensorID");
    if (sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
      return this.FormatRequest((object) "Unathorized");
    if (!string.IsNullOrEmpty(labName))
    {
      CalibrationFacility calibrationFacility = CalibrationFacility.LoadByName(labName);
      if (calibrationFacility != null)
        sensor.CalibrationFacilityID = calibrationFacility.CalibrationFacilityID;
    }
    sensor.CalibrationCertification = string.IsNullOrEmpty(certificationCode) ? sensor.CalibrationCertification : certificationCode;
    sensor.ReadProfileConfig1 = ReadProfileConfig1.GetValueOrDefault();
    sensor.ReadProfileConfig2 = ReadProfileConfig2.GetValueOrDefault();
    sensor.Save();
    try
    {
      this.RemoveSensor(sensor.SensorID);
    }
    catch
    {
      return this.FormatRequest((object) "Failed: Unable to remove sensor from calibration account");
    }
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPI]
  public ActionResult CalibrationCertificateCreate(
    long SensorID,
    DateTime DateCertified,
    DateTime CertificationExpirationDate,
    long CalibrationFacilityID,
    string CalibrationNumber,
    string CertificationType)
  {
    Sensor sensor = Sensor.Load(SensorID);
    if (sensor == null || sensor != null && sensor.IsDeleted)
      return this.FormatRequest((object) "Failed: Sensor does not exist");
    if (Account.Load(sensor.AccountID) == null)
      return this.FormatRequest((object) "Failed: Account does not exist");
    CalibrationFacility calibrationFacility = CalibrationFacility.Load(CalibrationFacilityID);
    if (calibrationFacility == null)
      return this.FormatRequest((object) "Failed: Calibration Facility does not exist");
    if (CertificationType == null)
      CertificationType = "";
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Failed: Account access denied");
    if (!MonnitSession.CurrentCustomer.CanSeeSensor(sensor) || !MonnitSession.CustomerCan("Sensor_Edit"))
      return this.FormatRequest((object) "Failed: Sensor access denied");
    if (!MonnitSession.CurrentCustomer.Can("Can_Create_Certificate"))
      return this.FormatRequest((object) "Failed: Certificate access denied");
    CalibrationCertificate calibrationCertificate = new CalibrationCertificate();
    long customerId = MonnitSession.CurrentCustomer.CustomerID;
    CalibrationCertificate DBObject = CalibrationCertificate.LoadBySensor(sensor);
    if (DBObject != null)
    {
      if (CertificateAcknowledgement.DeleteAcknowledgementByCertificateID(calibrationCertificate.CalibrationCertificateID) != 0)
        return this.FormatRequest((object) "Failed: Delete Acknowledgements failed");
      DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Sensor, customerId, sensor.AccountID, "Edited calibration certificate");
      DBObject.DeletedByUserID = customerId;
      DBObject.DeletedDate = DateTime.UtcNow;
      DBObject.Save();
    }
    calibrationCertificate.LogAuditData(eAuditAction.Create, eAuditObject.Sensor, customerId, sensor.AccountID, "Created calibration certificate");
    calibrationCertificate.CreatedByUserID = customerId;
    calibrationCertificate.DateCreated = DateTime.UtcNow;
    calibrationCertificate.DateCertified = DateCertified;
    calibrationCertificate.CertificationValidUntil = CertificationExpirationDate;
    calibrationCertificate.CalibrationFacilityID = calibrationFacility.CalibrationFacilityID;
    calibrationCertificate.CalibrationNumber = CalibrationNumber;
    calibrationCertificate.CertificationType = CertificationType;
    if (sensor.IsCableEnabled)
      calibrationCertificate.CableID = sensor.CableID;
    else
      calibrationCertificate.SensorID = sensor.SensorID;
    calibrationCertificate.Save();
    sensor.CalibrationCertification = calibrationCertificate.CalibrationNumber;
    sensor.CalibrationFacilityID = calibrationCertificate.CalibrationFacilityID;
    sensor.Save();
    return this.FormatRequest((object) new APICalibrationCertificate(calibrationCertificate));
  }

  [AuthorizeAPI]
  public ActionResult CalibrationCertificateRemove(long CalibrationCertificateID)
  {
    CalibrationCertificate DBObject = CalibrationCertificate.Load(CalibrationCertificateID);
    if (DBObject == null)
      return this.FormatRequest((object) "Failed: Calibration Certificate does not exist");
    Sensor sensor = Sensor.Load(DBObject.SensorID);
    if (sensor == null || sensor != null && sensor.IsDeleted)
      return this.FormatRequest((object) "Failed: Sensor does not exist");
    if (Account.Load(sensor.AccountID) == null)
      return this.FormatRequest((object) "Failed: Account does not exist");
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Failed: Account access denied");
    if (!MonnitSession.CurrentCustomer.CanSeeSensor(sensor) || !MonnitSession.CustomerCan("Sensor_Edit"))
      return this.FormatRequest((object) "Failed: Sensor access denied");
    if (!MonnitSession.CurrentCustomer.Can("Can_Create_Certificate"))
      return this.FormatRequest((object) "Failed: Certificate access denied");
    CalibrationCertificate.LoadBySensor(sensor);
    long customerId = MonnitSession.CurrentCustomer.CustomerID;
    if (CertificateAcknowledgement.DeleteAcknowledgementByCertificateID(DBObject.CalibrationCertificateID) != 0)
      return this.FormatRequest((object) "Failed: Delete Acknowledgements failed");
    DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Sensor, customerId, sensor.AccountID, "Deleted Calibration Certificate");
    DBObject.DeletedByUserID = customerId;
    DBObject.DeletedDate = DateTime.UtcNow;
    DBObject.Save();
    CalibrationCertificate calibrationCertificate = new CalibrationCertificate();
    sensor.CalibrationCertification = calibrationCertificate.CalibrationNumber;
    sensor.CalibrationFacilityID = calibrationCertificate.CalibrationFacilityID;
    sensor.Save();
    return this.FormatRequest((object) "Success");
  }

  [AuthorizeAPI]
  public ActionResult CalibrationCertificateList(long SensorID, bool? ShowDeleted)
  {
    Sensor sensor = Sensor.Load(SensorID);
    if (sensor == null || sensor != null && sensor.IsDeleted)
      return this.FormatRequest((object) "Failed: Sensor does not exist");
    ShowDeleted = new bool?(ShowDeleted.GetValueOrDefault());
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID))
      return this.FormatRequest((object) "Failed: Account access denied");
    if (!MonnitSession.CurrentCustomer.CanSeeSensor(sensor) || !MonnitSession.CustomerCan("Sensor_Edit"))
      return this.FormatRequest((object) "Failed: Sensor access denied");
    List<CalibrationCertificate> source = CalibrationCertificate.LoadAllBySensorID(SensorID);
    if (!ShowDeleted.Value)
      source = source.Where<CalibrationCertificate>((System.Func<CalibrationCertificate, bool>) (c =>
      {
        DateTime deletedDate = c.DeletedDate;
        return c.DeletedDate == DateTime.MinValue && c.DeletedByUserID == long.MinValue;
      })).ToList<CalibrationCertificate>();
    return this.FormatRequest((object) source.Select<CalibrationCertificate, APICalibrationCertificate>((System.Func<CalibrationCertificate, APICalibrationCertificate>) (c => new APICalibrationCertificate(c))).ToList<APICalibrationCertificate>());
  }

  public ActionResult CalibrationFacilityList()
  {
    return this.FormatRequest((object) CalibrationFacility.LoadAll().Select<CalibrationFacility, APICalibrationFacility>((System.Func<CalibrationFacility, APICalibrationFacility>) (f => new APICalibrationFacility(f))).ToList<APICalibrationFacility>());
  }

  public XDocument GetFirmwareList(string SKU)
  {
    DateTime utcNow = DateTime.UtcNow;
    string uri = "";
    string str = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
    try
    {
      uri = $"{ConfigData.AppSettings("MEA_API_Location")}xml/GetFirmwareList?sku={SKU}";
      XDocument firmwareList = XDocument.Load(uri);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-GetFirmwareList",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = firmwareList.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      return firmwareList;
    }
    catch (Exception ex)
    {
      ex.Log("Load Firmware Failed ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-GetFirmwareList",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return (XDocument) null;
  }

  public ActionResult GetLatestSKUFirmware(string sku, bool isGatewayBase)
  {
    try
    {
      return this.FormatRequest((object) MonnitUtil.GetLatestFirmwareVersionFromMEA(sku, isGatewayBase));
    }
    catch (Exception ex)
    {
      ex.Log($"GetLatestSKUFirmware|FormatRequest(MonnitUtil.GetLatestFirmwareVersionFromMEA(sku, isGatewayBase)), sku = {sku}, isGatewayBase = {isGatewayBase}");
      return this.FormatRequest((object) "Failed");
    }
  }

  public ActionResult GenerateEncryptionKeys(long gatewayID, string enc64gwPublicKey)
  {
    try
    {
      Gateway gateway = Gateway.Load(gatewayID);
      if (gateway == null)
        return this.FormatRequest((object) "Failed: Bad Gateway");
      CSNet.Load(gateway.CSNetID);
      byte[] GatewayPublicKey = Convert.FromBase64String(enc64gwPublicKey);
      if (GatewayPublicKey.Length < 64 /*0x40*/)
        return this.FormatRequest((object) "Failed: Bad PublicKey");
      gateway.ServerPublicKey = gateway.GenerateNewEncryptionKeys(GatewayPublicKey);
      gateway.Save();
      MemoryStream fileStream = new MemoryStream(gateway.ServerPublicKey);
      this.Response.AddHeader("content-disposition", "attachment;filename=public.key");
      this.Response.AddHeader("Extension", "bin");
      this.Response.Buffer = true;
      return (ActionResult) new FileStreamResult((Stream) fileStream, "application/octet");
    }
    catch (Exception ex)
    {
      ex.Log(" GenerateEncryptionKeys");
      return (ActionResult) new FileStreamResult((Stream) null, "application/octet");
    }
  }

  public ActionResult GetEncryptedFirmwareBySKU(long gatewayID, string sku, bool isGatewayBase)
  {
    try
    {
      Gateway sensorGateway = Gateway.Load(gatewayID);
      if (sensorGateway == null || sensorGateway.ServerPublicKey.Length == 1)
        return this.FormatRequest((object) "Failed: Bad Gateway or Keyset");
      CSNet.Load(sensorGateway.CSNetID);
      byte[] numArray = (byte[]) null;
      byte[] buffer = (byte[]) null;
      uint num1 = 0;
      int num2 = 0;
      if (buffer == null)
      {
        DateTime utcNow = DateTime.UtcNow;
        string address = "";
        string str = ConfigData.AppSettings("MeaApiLogging");
        bool flag = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
        try
        {
          address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/GetSKUFirmware/{0}?sku={1}&isGatewayBase={2}", (object) MonnitUtil.GetMEATempAuthToken(), (object) sku, (object) isGatewayBase);
          using (WebClient webClient = new WebClient())
            numArray = webClient.DownloadData(address);
          if (flag)
          {
            TimeSpan timeSpan = DateTime.UtcNow - utcNow;
            new MeaApiLog()
            {
              MethodName = "MEA-GetEncryptedFirmwareBySKU",
              MachineName = Environment.MachineName,
              RequestBody = address,
              ResponseBody = ("Binary Length: " + numArray.Length.ToString()),
              CreateDate = utcNow,
              SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
              IsException = false
            }.Save();
          }
        }
        catch (Exception ex)
        {
          ex.Log("APIController.GetEncryptedFirmwareBySKU ");
          if (flag)
          {
            TimeSpan timeSpan = DateTime.UtcNow - utcNow;
            new MeaApiLog()
            {
              MethodName = "MEA-GetEncryptedFirmwareBySKU",
              MachineName = Environment.MachineName,
              RequestBody = address,
              ResponseBody = ex.Message,
              CreateDate = utcNow,
              SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
              IsException = true
            }.Save();
          }
        }
        buffer = APIController.EncryptFirmware(sensorGateway, numArray);
        num1 = numArray.Calc_CRC32();
        num2 = numArray.Length;
      }
      MemoryStream fileStream = new MemoryStream(buffer);
      this.Response.AddHeader("content-disposition", "attachment;filename=file.bin");
      this.Response.AddHeader("Extension", "bin");
      this.Response.AddHeader("CRC", num1.ToString());
      this.Response.AddHeader("Length", num2.ToString());
      this.Response.Buffer = true;
      return (ActionResult) new FileStreamResult((Stream) fileStream, "application/octet");
    }
    catch (Exception ex)
    {
      return this.FormatRequest((object) ("Failed: " + ex.Message));
    }
  }

  public ActionResult GetEncryptedFirmwareByID(long gatewayID, long firmwareID)
  {
    DateTime utcNow = DateTime.UtcNow;
    string address = "";
    string str1 = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str1) && bool.Parse(str1);
    try
    {
      Gateway sensorGateway = Gateway.Load(gatewayID);
      if (sensorGateway == null || sensorGateway.ServerPublicKey.Length == 1)
        return this.FormatRequest((object) "Failed: Bad Gateway or Keyset");
      CSNet.Load(sensorGateway.CSNetID);
      byte[] numArray = (byte[]) null;
      string str2 = ConfigData.AppSettings("MEA_API_Location");
      ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
      string str3 = XDocument.Load($"{str2}xml?applicationAuthGuid={ConfigData.FindValue("MEA_API_Auth_Guid")}").Descendants((XName) "object").Single<XElement>().Attribute((XName) "auth").Value;
      address = string.Format(str2 + "xml/GetOTAFirmware/{0}?firmwareID={1}", (object) str3, (object) firmwareID);
      using (WebClient webClient = new WebClient())
        numArray = webClient.DownloadData(address);
      int length;
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        MeaApiLog meaApiLog1 = new MeaApiLog();
        meaApiLog1.MethodName = "MEA-GetEncryptedFirmwareByID";
        meaApiLog1.MachineName = Environment.MachineName;
        meaApiLog1.RequestBody = address;
        MeaApiLog meaApiLog2 = meaApiLog1;
        length = numArray.Length;
        string str4 = "Binary Length: " + length.ToString();
        meaApiLog2.ResponseBody = str4;
        meaApiLog1.CreateDate = utcNow;
        meaApiLog1.SecondsElapsed = timeSpan.TotalSeconds.ToInt();
        meaApiLog1.IsException = false;
        meaApiLog1.Save();
      }
      MemoryStream fileStream = new MemoryStream(APIController.EncryptFirmware(sensorGateway, numArray));
      this.Response.AddHeader("content-disposition", "attachment;filename=file.bin");
      this.Response.AddHeader("Extension", "bin");
      this.Response.AddHeader("CRC", numArray.Calc_CRC32().ToString());
      HttpResponseBase response = this.Response;
      length = numArray.Length;
      string str5 = length.ToString();
      response.AddHeader("Length", str5);
      this.Response.Buffer = true;
      return (ActionResult) new FileStreamResult((Stream) fileStream, "application/octet");
    }
    catch (Exception ex)
    {
      ex.Log($"GetEncryptedFirmwareByID Failed FirmwareID: {firmwareID.ToString()} Message: ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-GetEncryptedFirmwareByID",
          MachineName = Environment.MachineName,
          RequestBody = address,
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
      return this.FormatRequest((object) ("Failed: " + ex.Message));
    }
  }

  private static byte[] EncryptFirmware(Gateway sensorGateway, byte[] rawBinary)
  {
    try
    {
      int num = 16 /*0x10*/ - rawBinary.Length % 16 /*0x10*/;
      byte[] numArray1 = new byte[num == 16 /*0x10*/ ? 0 : num];
      new Random(DateTime.Now.Millisecond).NextBytes(numArray1);
      byte[] numArray2 = new byte[rawBinary.Length + numArray1.Length];
      Array.Copy((Array) rawBinary, 0, (Array) numArray2, 0, rawBinary.Length);
      Array.Copy((Array) numArray1, 0, (Array) numArray2, rawBinary.Length, numArray1.Length);
      string hex1 = sensorGateway.CurrentEncryptionKey.ToHex();
      string hex2 = sensorGateway.CurrentEncryptionIVCounter.ToHex();
      $"GatewayPublic: {sensorGateway.CurrentPublicKey.ToHex()}\r\nServerPublic: {sensorGateway.ServerPublicKey.ToHex()}\r\nSharedSecret: {hex1}\r\nIVCounter:{hex2}";
      return GW_Encryption.Encrypt128(numArray2, sensorGateway.CurrentEncryptionKey, sensorGateway.CurrentEncryptionIVCounter);
    }
    catch (Exception ex)
    {
      ex.Log("APIControlelr.EncryptFirmware(Gateway sensorGateway, byte[] responseBuffer)");
      return (byte[]) null;
    }
  }

  public ActionResult RestAPI()
  {
    if (MonnitSession.CurrentCustomer != null && MonnitSession.CurrentCustomer.Can("Navigation_View_API"))
      return (ActionResult) this.View();
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
    {
      ErrorLocation = "Events",
      ErrorTranslateTag = "Event/Index|",
      ErrorTitle = "Unauthorized access to API Page",
      ErrorMessage = "You do not have permission to access this page."
    });
  }

  [AuthorizeDefault]
  public ActionResult APIKeys()
  {
    if (MonnitSession.CurrentCustomer.Can("Navigation_View_API"))
      return (ActionResult) this.View((object) APIKey.LoadByAccount(MonnitSession.CurrentCustomer.AccountID));
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
    {
      ErrorLocation = "Events",
      ErrorTranslateTag = "Event/Index|",
      ErrorTitle = "Unauthorized access to API Page",
      ErrorMessage = "You do not have permission to access this page."
    });
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult CreateAPIKey(string name)
  {
    if (!MonnitSession.CurrentCustomer.Can("Navigation_View_API"))
      return (ActionResult) this.Content("Unauthorized");
    APIKey apiKey = new APIKey();
    string keyValue = MonnitUtil.CreateKeyValue(32 /*0x20*/);
    apiKey.AccountID = MonnitSession.CurrentCustomer.AccountID;
    apiKey.Name = name;
    apiKey.Salt = MonnitUtil.GenerateSalt();
    apiKey.KeyValue = MonnitUtil.CreateKeyValue(12);
    apiKey.WorkFactor = ConfigData.AppSettings("WorkFactor").ToInt();
    apiKey.CustomerID = MonnitSession.CurrentCustomer.CustomerID;
    apiKey.HashedSecret = MonnitUtil.GenerateHash(keyValue, apiKey.Salt, apiKey.WorkFactor);
    apiKey.Save();
    return (ActionResult) this.Content($"{apiKey.KeyValue}|{keyValue}");
  }

  [Authorize]
  public ContentResult DeleteAPIKey(string keyValue)
  {
    if (!MonnitSession.CurrentCustomer.Can("Navigation_View_API"))
      return this.Content("Unauthorized");
    APIKey apiKey = APIKey.LoadByKeyValue(keyValue).FirstOrDefault<APIKey>();
    if (apiKey == null || apiKey.AccountID != MonnitSession.CurrentCustomer.AccountID)
      return this.Content("Failed");
    apiKey.Delete();
    return this.Content("Success");
  }
}
