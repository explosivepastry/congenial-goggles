// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.RetailController
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
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Xml.Linq;

#nullable disable
namespace iMonnit.Controllers;

public class RetailController : CheckoutController
{
  [AuthorizeDefault]
  public ActionResult NotificationCredit(long id)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_My_Account"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    Account account = Account.Load(id);
    if (MonnitSession.CurrentCustomer.AccountID == id)
    {
      RetailController.CreateNetsuiteCustomerSession(MonnitSession.Session.SessionID, account.StoreUserID);
      this.Session["PurchaseLinkStoreModel"] = (object) RetailController.CreatePurchaseLinkStoreModel_WithPaymentAndProductList($"NotificationCredit[ID: {id.ToString()}]", nameof (NotificationCredit), MonnitSession.CurrentCustomer.Account);
      return (ActionResult) this.View((object) MonnitSession.CurrentCustomer.Account);
    }
    if (!MonnitSession.CustomerCan("Navigation_View_Administration") || account == null || !MonnitSession.IsAuthorizedForAccount(account.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    RetailController.CreateNetsuiteCustomerSession(MonnitSession.Session.SessionID, account.StoreUserID);
    this.Session["PurchaseLinkStoreModel"] = (object) RetailController.CreatePurchaseLinkStoreModel_WithPaymentAndProductList($"NotificationCredit[ID: {id.ToString()}]", nameof (NotificationCredit), account);
    return (ActionResult) this.View((object) account);
  }

  public static PurchaseLinkStoreModel CreatePurchaseLinkStoreModel_WithPaymentAndProductList(
    string fromMethod,
    string productType,
    Account account)
  {
    PurchaseLinkStoreModel paymentAndProductList = new PurchaseLinkStoreModel();
    paymentAndProductList.account = account;
    paymentAndProductList.PaymentInfoModelList = new List<PaymentInfoModel>();
    paymentAndProductList.ProductList = new List<ProductInfoModel>();
    try
    {
      if (MonnitSession.UserIsCustomerProxied)
        account = Account.Load(Customer.Load(MonnitSession.CurrentCustomer.CustomerID).AccountID);
      if (account != null && account.AccountID != long.MinValue && RetailController.DoesMEAUserExistForAccount(account))
        paymentAndProductList.PaymentInfoModelList = RetailController.RetrievePaymentInfoList(account);
      paymentAndProductList.ProductList = RetailController.RetrieveProductInfoList(account != null ? account.StoreLinkGuid.ToString() : "", productType);
    }
    catch (Exception ex)
    {
      ex.Log($"RetailController.{fromMethod}.RetrieveInfoList ");
    }
    return paymentAndProductList;
  }

  [AuthorizeDefault]
  public ActionResult CreditTypeLink()
  {
    return !MonnitSession.CustomerCan("Navigation_View_My_Account") ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public new ActionResult UpdateSubscription()
  {
    OnlineOrder onlineOrder = OnlineOrder.LoadCartByAccount(MonnitSession.CurrentCustomer.AccountID);
    foreach (BaseDBObject baseDbObject in onlineOrder.Items)
      baseDbObject.Delete();
    List<Sensor> sensorList = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
    int num = int.MaxValue;
    Product product1 = (Product) null;
    foreach (Product product2 in Product.LoadPremiere())
    {
      if (product2.PremierMaxSensorCount >= sensorList.Count && product2.PremierMaxSensorCount < num)
      {
        product1 = product2;
        num = product2.PremierMaxSensorCount;
      }
    }
    if (product1 != null)
      new OrderItem()
      {
        ProductID = product1.ProductID,
        OrderID = onlineOrder.OrderID,
        ItemQty = 1,
        ItemName = product1.Name,
        ItemDescription = product1.Description,
        ItemPrice = product1.Price,
        ItemTotalAdjustment = 0.0,
        AdjustmentNotes = ""
      }.Save();
    onlineOrder.RefreshItems();
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "PurchaseDetail"
    });
  }

  [AuthorizeDefault]
  public new ActionResult PurchaseNotificationCredits(int count)
  {
    OnlineOrder onlineOrder = OnlineOrder.LoadCartByAccount(MonnitSession.CurrentCustomer.AccountID);
    foreach (BaseDBObject baseDbObject in onlineOrder.Items)
      baseDbObject.Delete();
    Product product1 = (Product) null;
    foreach (Product product2 in (IEnumerable<Product>) Product.LoadNotificationCredits().OrderBy<Product, int>((Func<Product, int>) (p => p.NotificationCredits)))
    {
      if (product2.NotificationCredits <= count)
        product1 = product2;
    }
    if (product1 != null)
      new OrderItem()
      {
        ProductID = product1.ProductID,
        OrderID = onlineOrder.OrderID,
        ItemQty = 1,
        ItemName = product1.Name,
        ItemDescription = product1.Description,
        ItemPrice = product1.Price,
        ItemTotalAdjustment = 0.0,
        AdjustmentNotes = ""
      }.Save();
    onlineOrder.RefreshItems();
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = ""
    });
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult CreditSetting(long id, FormCollection collection)
  {
    Account account = Account.Load(collection["accountID"].ToLong());
    bool flag = collection["EnableAutoPurchase"] == "on";
    try
    {
      if (this.ModelState.IsValid)
      {
        Monnit.CreditSetting creditSetting = Monnit.CreditSetting.Load(id) ?? new Monnit.CreditSetting();
        creditSetting.AccountID = collection["accountID"].ToLong();
        creditSetting.CreditCompareValue = collection["creditCompareValue"].ToLong();
        creditSetting.UserId = collection["UserId"].ToLong();
        creditSetting.CreditClassification = (eCreditClassification) collection["creditClassification"].ToInt();
        account.AutoPurchase = flag;
        if (flag)
        {
          PurchaseLinkStoreModel purchaseLinkStoreModel = new PurchaseLinkStoreModel()
          {
            PaymentInfoModelList = RetailController.RetrievePaymentInfoList(account)
          };
          if (purchaseLinkStoreModel.PaymentInfoModelList.Count < 1)
            return (ActionResult) this.Content("Credit Card Required");
          account.DefaultPaymentID = purchaseLinkStoreModel.PaymentInfoModelList.FirstOrDefault<PaymentInfoModel>().ProfileID;
        }
        account.Save();
        creditSetting.Save();
        // ISSUE: reference to a compiler-generated field
        if (RetailController.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RetailController.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Save", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = RetailController.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) RetailController.\u003C\u003Eo__5.\u003C\u003Ep__0, this.ViewBag, "true");
        // ISSUE: reference to a compiler-generated field
        if (RetailController.\u003C\u003Eo__5.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RetailController.\u003C\u003Eo__5.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, Monnit.CreditSetting, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (CreditSetting), typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = RetailController.\u003C\u003Eo__5.\u003C\u003Ep__2.Target((CallSite) RetailController.\u003C\u003Eo__5.\u003C\u003Ep__2, this.ViewBag, Monnit.CreditSetting.LoadByAccountID(collection["accountID"].ToLong(), new eCreditClassification?((eCreditClassification) collection["creditClassification"].ToInt())));
        return (ActionResult) this.Content("Success");
      }
      // ISSUE: reference to a compiler-generated field
      if (RetailController.\u003C\u003Eo__5.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RetailController.\u003C\u003Eo__5.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Monnit.CreditSetting, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (CreditSetting), typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RetailController.\u003C\u003Eo__5.\u003C\u003Ep__1.Target((CallSite) RetailController.\u003C\u003Eo__5.\u003C\u003Ep__1, this.ViewBag, Monnit.CreditSetting.LoadByAccountID(collection["accountID"].ToLong(), new eCreditClassification?((eCreditClassification) collection["creditClassification"].ToInt())));
      return (ActionResult) this.Content("Failed");
    }
    catch
    {
      // ISSUE: reference to a compiler-generated field
      if (RetailController.\u003C\u003Eo__5.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RetailController.\u003C\u003Eo__5.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, Monnit.CreditSetting, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (CreditSetting), typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RetailController.\u003C\u003Eo__5.\u003C\u003Ep__3.Target((CallSite) RetailController.\u003C\u003Eo__5.\u003C\u003Ep__3, this.ViewBag, new Monnit.CreditSetting());
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ActivateCredits(long id, string activationCode, int creditClassification)
  {
    Account account = Account.Load(id);
    Customer currentCustomer = MonnitSession.CurrentCustomer;
    AccountSubscription currentSubscription = MonnitSession.CurrentSubscription;
    if (account == null)
      return (ActionResult) this.Content("Not Authorized.");
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) && !MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.Content("Not Authorized.");
    account.LogAuditData(eAuditAction.Update, eAuditObject.Account, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, $"Added {((eCreditClassification) creditClassification).ToStringSafe()} Credits To Account");
    this.Session["AppliedActivationCode"] = (object) activationCode;
    this.Session["PurchaseLinkStoreModel"] = (object) null;
    switch (creditClassification)
    {
      case 1:
        return (ActionResult) this.Content(PurchaseBase.ProcessNotificationCreditKey(activationCode, account, currentCustomer, currentSubscription));
      case 2:
        return (ActionResult) this.Content(PurchaseBase.ProcessMessageCreditKey(activationCode, account, currentCustomer));
      case 3:
        return (ActionResult) this.Content(PurchaseBase.ProcessSensorPrintCreditKey(activationCode, account, currentCustomer));
      case 4:
        return (ActionResult) this.Content(PurchaseBase.ProcessGatewayUnlockCreditKey(activationCode, account, currentCustomer) + "_MNA-GW-UL");
      case 5:
        return (ActionResult) this.Content(PurchaseBase.ProcessGatewayUnlockGpsCreditKey(activationCode, account, currentCustomer) + "_MNA-GPS-UL");
      default:
        return (ActionResult) this.Content("Failed");
    }
  }

  private static string AddNotificationCredits(
    Account acnt,
    int notificationCreditsToAssign,
    DateTime? expiration)
  {
    if (acnt == null || !MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
      return "Not Authorized.";
    long num = long.MinValue;
    foreach (NotificationCreditType notificationCreditType in NotificationCreditType.LoadAll())
    {
      if (notificationCreditType.Name.ToLower() == "credit" && expiration.HasValue)
      {
        num = notificationCreditType.NotificationCreditTypeID;
        break;
      }
      if (notificationCreditType.Name.ToLower() == "permanent credit" && !expiration.HasValue)
      {
        num = notificationCreditType.NotificationCreditTypeID;
        break;
      }
    }
    if (num == long.MinValue)
      return "Notification credit type not found.";
    if (notificationCreditsToAssign > 0)
      new Monnit.NotificationCredit()
      {
        NotificationCreditTypeID = num,
        AccountID = acnt.AccountID,
        ActivatedByCustomerID = MonnitSession.CurrentCustomer.CustomerID,
        ActivationDate = DateTime.UtcNow,
        ActivationCode = "",
        ActivatedCredits = notificationCreditsToAssign,
        ExpirationDate = (expiration ?? new DateTime(2099, 1, 1))
      }.Save();
    return "Success";
  }

  [HttpPost]
  [AuthorizeDefault]
  public ActionResult AssignCredits(
    long id,
    int creditsToAssign,
    int creditClassification,
    DateTime? expiration)
  {
    try
    {
      Account acnt = Account.Load(id);
      Customer currentCustomer = MonnitSession.CurrentCustomer;
      if (acnt == null)
        return (ActionResult) this.Content("Not Authorized.");
      if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
        return (ActionResult) this.Content("Not Authorized.");
      switch (creditClassification)
      {
        case 2:
          return (ActionResult) this.Content(PurchaseBase.AddMessageCredits(acnt, currentCustomer, creditsToAssign));
        case 3:
          return (ActionResult) this.Content(PurchaseBase.AddSensorPrintCredits(acnt, currentCustomer, creditsToAssign));
        case 4:
          return (ActionResult) this.Content(PurchaseBase.AddGatewayUnlockCredits(acnt, currentCustomer, creditsToAssign));
        case 5:
          return (ActionResult) this.Content(PurchaseBase.AddGatewayUnlockGpsCredits(acnt, currentCustomer, creditsToAssign));
        default:
          return (ActionResult) this.Content(RetailController.AddNotificationCredits(acnt, creditsToAssign, new DateTime?(expiration ?? new DateTime(2099, 1, 1))));
      }
    }
    catch
    {
    }
    return (ActionResult) this.Content("Failed.");
  }

  [HttpPost]
  [AuthorizeDefault]
  public ActionResult RemoveCredits(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
      return (ActionResult) this.Content("Not Authorized.");
    Credit DBObject = Credit.Load(id);
    if (DBObject == null)
      return (ActionResult) this.Content("Not Found.");
    DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Credit, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Removed Credits");
    DBObject.Delete();
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult MessageCredit(long id)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_My_Account"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Retail",
        ErrorTranslateTag = "Retail/PaymentOption|",
        ErrorTitle = "Unauthorized access to billing pages",
        ErrorMessage = "You do not have permission to access this page."
      });
    Account account = Account.Load(id);
    if (MonnitSession.CurrentCustomer.AccountID == id)
    {
      RetailController.CreateNetsuiteCustomerSession(MonnitSession.Session.SessionID, account.StoreUserID);
      this.Session["PurchaseLinkStoreModel"] = (object) RetailController.CreatePurchaseLinkStoreModel_WithPaymentAndProductList($"MessageCredit[ID: {id.ToString()}]", "HxCredit", MonnitSession.CurrentCustomer.Account);
      return (ActionResult) this.View((object) MonnitSession.CurrentCustomer.Account);
    }
    if (!MonnitSession.CustomerCan("Navigation_View_Administration") || account == null || !MonnitSession.IsAuthorizedForAccount(account.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    RetailController.CreateNetsuiteCustomerSession(MonnitSession.Session.SessionID, account.StoreUserID);
    this.Session["PurchaseLinkStoreModel"] = (object) RetailController.CreatePurchaseLinkStoreModel_WithPaymentAndProductList($"MessageCredit[ID: {id.ToString()}]", "HxCredit", account);
    return (ActionResult) this.View((object) account);
  }

  [AuthorizeDefault]
  public ActionResult PremiereCredit(long? id)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_My_Account") || !MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Retail",
        ErrorTranslateTag = "Retail/PaymentOption|",
        ErrorTitle = "Unauthorized access to billing pages",
        ErrorMessage = "You do not have permission to access this page."
      });
    long ID = id ?? long.MinValue;
    Account account = ID != long.MinValue ? Account.Load(ID) : MonnitSession.CurrentCustomer.Account;
    this.Session["PurchaseLinkStoreModel"] = (object) RetailController.CreatePurchaseLinkStoreModel_WithPaymentAndProductList($"PremiereCredit[ID: {id.ToString()}]", "Subscriptions", account);
    this.Session["Premiere_Prorate"] = (object) null;
    if (account == null)
      return (ActionResult) this.View();
    if (MonnitSession.CurrentCustomer.AccountID == account.AccountID)
    {
      RetailController.CreateNetsuiteCustomerSession(MonnitSession.Session.SessionID, account.StoreUserID);
      return (ActionResult) this.View((object) MonnitSession.CurrentCustomer.Account);
    }
    if (!MonnitSession.CustomerCan("Navigation_View_Administration") || account == null || !MonnitSession.IsAuthorizedForAccount(account.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    RetailController.CreateNetsuiteCustomerSession(MonnitSession.Session.SessionID, account.StoreUserID);
    return (ActionResult) this.View((object) account);
  }

  [AuthorizeDefault]
  public ActionResult ApplySensorPrint(long id, string ids)
  {
    Account acct = Account.Load(id);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct) || !MonnitSession.CustomerCan("Can_Access_Billing") || !MonnitSession.CustomerCan("Sensor_Edit"))
      return (ActionResult) this.Content("Unauthorized");
    string[] strArray = ids.Split(new char[1]{ ',' }, StringSplitOptions.RemoveEmptyEntries);
    if (Credit.LoadRemainingCreditsForAccount(acct.AccountID, eCreditClassification.SensorPrint) < strArray.Length)
      return (ActionResult) this.Content("Insufficient Credits");
    int creditsToCharge = 0;
    foreach (string o in strArray)
    {
      try
      {
        Sensor DBObject = Sensor.Load(o.ToLong());
        if (DBObject != null)
        {
          DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, acct.AccountID, "Applied SensorPrint to sensor");
          byte[] data = new byte[32 /*0x20*/];
          new RNGCryptoServiceProvider().GetBytes(data);
          DBObject.SensorPrint = data;
          DBObject.SensorPrintDirty = true;
          DBObject.Save();
          ++creditsToCharge;
        }
      }
      catch (Exception ex)
      {
        ex.Log($"Retail.ApplySensorPrint Failed, SensorID : {o} Message: ");
      }
    }
    RetailController.ChargeSensorPrintCredit(acct.AccountID, creditsToCharge);
    return (ActionResult) this.Content("Success");
  }

  public static bool ChargeSensorPrintCredit(long accountID, int creditsToCharge)
  {
    bool flag = false;
    List<Credit> creditList = new List<Credit>();
    foreach (Credit credit in Credit.LoadAvailable(accountID, eCreditClassification.SensorPrint))
    {
      if (credit.RemainingCredits >= creditsToCharge)
      {
        creditList.Add(credit);
        credit.UsedCredits += creditsToCharge;
        flag = true;
        break;
      }
      creditList.Add(credit);
      int remainingCredits = credit.RemainingCredits;
      credit.UsedCredits += remainingCredits;
      creditsToCharge -= remainingCredits;
    }
    if (flag)
    {
      foreach (Credit credit in creditList)
      {
        if (credit.RemainingCredits <= 0)
          credit.ExhaustedDate = DateTime.UtcNow;
        credit.Save();
      }
    }
    return flag;
  }

  [AuthorizeDefault]
  public ActionResult CheckoutCheck(long id, string productType, string sku, string qty)
  {
    Account acct = Account.Load(id);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return (ActionResult) this.Content("Redirect|/Overview/Index/");
    if (!MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.Content("Failed|Unauthorized access to billing pages");
    int num;
    if (!MonnitSession.UserIsCustomerProxied)
    {
      Guid storeLinkGuid = acct.StoreLinkGuid;
      num = acct.StoreLinkGuid == Guid.Empty ? 1 : 0;
    }
    else
      num = 0;
    return num != 0 ? (ActionResult) this.Content($"Redirect|/Retail/LoginToStore/{id.ToString()}/?returnURLAfterAdd=/Retail/PaymentOption/{id.ToString()}/?sku={sku}&qty={qty}") : (ActionResult) this.Content($"Success|/Retail/PaymentOption/{id.ToString()}/?sku={sku}&qty={qty}");
  }

  [AuthorizeDefault]
  public ActionResult Checkout(long id, string productType, string returnurl, string sku)
  {
    Account account = Account.Load(id);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Retail",
        ErrorTranslateTag = "Retail/PaymentOption|",
        ErrorTitle = "Unauthorized access to billing pages",
        ErrorMessage = "You do not have permission to access this page."
      });
    if (!MonnitSession.UserIsCustomerProxied)
    {
      Guid storeLinkGuid = account.StoreLinkGuid;
      if (account.StoreLinkGuid == Guid.Empty)
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "LoginToStore",
          controller = "Retail",
          id = account.AccountID
        });
    }
    else
      account = Account.Load(Customer.Load(MonnitSession.CurrentCustomer.CustomerID).AccountID);
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__15.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__15.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnURL", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = RetailController.\u003C\u003Eo__15.\u003C\u003Ep__0.Target((CallSite) RetailController.\u003C\u003Eo__15.\u003C\u003Ep__0, this.ViewBag, returnurl);
    PurchaseLinkStoreModel paymentAndProductList = RetailController.CreatePurchaseLinkStoreModel_WithPaymentAndProductList($"Checkout[ID: {id.ToString()}]", productType, account);
    this.Session["PurchaseLinkStoreModel"] = (object) paymentAndProductList;
    return (ActionResult) this.View((object) paymentAndProductList);
  }

  [AuthorizeDefault]
  public ActionResult PurchasePreview(long id, long profileID, string sku, string qty)
  {
    Account acct = Account.Load(id);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Retail",
        ErrorTranslateTag = "Retail/PaymentOption|",
        ErrorTitle = "Unauthorized access to billing pages",
        ErrorMessage = "You do not have permission to access this page."
      });
    if (!MonnitSession.UserIsCustomerProxied)
    {
      Guid storeLinkGuid = acct.StoreLinkGuid;
      if (acct.StoreLinkGuid == Guid.Empty)
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "PaymentOption",
          controller = "Retail",
          id = acct.AccountID
        });
    }
    else
      acct = Account.Load(Customer.Load(MonnitSession.CurrentCustomer.CustomerID).AccountID);
    PurchaseLinkStoreModel model = (PurchaseLinkStoreModel) this.Session["PurchaseLinkStoreModel"];
    if (model == null || profileID < 0L || string.IsNullOrEmpty(sku))
    {
      string str1 = "Subscriptions";
      string str2 = "PremiereCredit";
      if (sku.Contains("MNW-NC"))
      {
        str1 = "NotificationCredit";
        str2 = "NotificationCredit";
      }
      else if (sku.Contains("MNW-HX"))
      {
        str1 = "HxCredit";
        str2 = "MessageCredit";
      }
      else if (sku.Contains("MNW-SP"))
      {
        str1 = "SensorPrintCredit";
        str2 = "SensorPrints";
      }
      else if (sku.Contains("MNA-GW"))
      {
        str1 = "GatewayUnlockCredit";
        str2 = "GatewayUnlock";
      }
      else if (sku.Contains("MNA-GPS"))
      {
        str1 = "GatewayUnlockGpsCredit";
        str2 = "GatewayUnlockGps";
      }
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = str2,
        controller = "Retail",
        id = acct.AccountID,
        productType = str1
      });
    }
    try
    {
      double num1 = 0.0;
      bool flag = false;
      if (sku.StartsWith("MNW-IP"))
      {
        string keyType = MonnitSession.CurrentCustomer.Account.CurrentSubscription.AccountSubscriptionType.KeyType;
        int num2;
        if (!keyType.StartsWith("Premiere_"))
          num2 = 0;
        else
          num2 = keyType.Split('_')[1].ToInt();
        int num3 = num2;
        int num4 = sku.Split('-')[2].ToInt();
        if (num3 < num4)
        {
          flag = true;
          num1 = this.Session["Premiere_Prorate"] != null ? this.Session["Premiere_Prorate"].ToDouble() : 0.0;
          DateTime expirationDate = MonnitSession.CurrentCustomer.Account.CurrentSubscription.ExpirationDate;
          int num5 = (expirationDate.Year - DateTime.Now.Year) * 12 + expirationDate.Month - DateTime.Now.Month;
          int num6 = num5 > 12 ? 12 : num5;
          if (num1 == 0.0)
          {
            foreach (ProductInfoModel product in model.ProductList)
            {
              int num7 = product.SKU.Split('-')[2].ToInt();
              if (num3 == num7 && expirationDate > DateTime.Now)
              {
                double num8 = (product.Price - product.Discount) / 12.0;
                TimeSpan timeSpan = MonnitSession.CurrentCustomer.Account.CurrentSubscription.ExpirationDate - DateTime.Now;
                num1 = num8 * (double) num6;
                this.Session["Premiere_Prorate"] = (object) num1;
              }
            }
          }
          // ISSUE: reference to a compiler-generated field
          if (RetailController.\u003C\u003Eo__16.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            RetailController.\u003C\u003Eo__16.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, double, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ProrateAmount", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = RetailController.\u003C\u003Eo__16.\u003C\u003Ep__0.Target((CallSite) RetailController.\u003C\u003Eo__16.\u003C\u003Ep__0, this.ViewBag, num1);
          // ISSUE: reference to a compiler-generated field
          if (RetailController.\u003C\u003Eo__16.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            RetailController.\u003C\u003Eo__16.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "MonthDifference", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj2 = RetailController.\u003C\u003Eo__16.\u003C\u003Ep__1.Target((CallSite) RetailController.\u003C\u003Eo__16.\u003C\u003Ep__1, this.ViewBag, num6);
        }
      }
      model.ProductList = model.ProductList.Where<ProductInfoModel>((Func<ProductInfoModel, bool>) (m => m.SKU == sku)).ToList<ProductInfoModel>();
      model.PaymentInfoModelList = model.PaymentInfoModelList.Where<PaymentInfoModel>((Func<PaymentInfoModel, bool>) (m => m.ProfileID == profileID)).ToList<PaymentInfoModel>();
      if (model.PaymentInfoModelList == null || model.PaymentInfoModelList.Count <= 0 || model.ProductList == null || model.ProductList.Count <= 0)
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "PaymentOption",
          controller = "Retail",
          id = acct.AccountID,
          sku = sku,
          qty = qty
        });
      int qty1 = string.IsNullOrWhiteSpace(qty) ? 1 : qty.ToInt();
      if (sku.ToLower().Contains("mnw-ip"))
        qty1 = 1;
      model.Quantity = qty1;
      double num9 = 0.0;
      if (flag)
        num9 = model.ProductList.First<ProductInfoModel>().Price - num1;
      double num10 = MonnitUtil.RetrieveItemTaxValue(sku, qty1, model.PaymentInfoModelList[0].ProfileID, acct.StoreUserID, MonnitSession.Session.SessionID, num9 > 0.0 ? num9 : 0.0);
      model.ProductList[0].Tax = num10;
    }
    catch
    {
    }
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult PurchaseConfirm(long id, string sku, string purchasedItemID)
  {
    Account acct = Account.Load(id);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Retail",
        ErrorTranslateTag = "Retail/PaymentOption|",
        ErrorTitle = "Unauthorized access to billing pages",
        ErrorMessage = "You do not have permission to access this page."
      });
    PurchaseConfirmationModel model = new PurchaseConfirmationModel();
    model.Account = acct;
    model.PurchaseType = "";
    try
    {
      string[] strArray = sku.Split(new char[1]{ '-' }, StringSplitOptions.RemoveEmptyEntries);
      model.PurchaseType = strArray[1].ToString().ToLower();
    }
    catch
    {
    }
    model.PurchaseProduct = "";
    model.PurchaseExpiration = DateTime.MinValue;
    model.PurchasedItemIDs = purchasedItemID;
    model.CardMask = "";
    model.PurchasePrice = 0.0;
    try
    {
      PurchaseLinkStoreModel purchaseLinkStoreModel = (PurchaseLinkStoreModel) this.Session["PurchaseLinkStoreModel"];
      if (purchaseLinkStoreModel != null)
      {
        string cardNumber = purchaseLinkStoreModel.PaymentInfoModelList[0].CardNumber;
        model.CardMask = "xxxxxx " + cardNumber.Substring(cardNumber.Length - 4);
        model.PurchasePrice = (purchaseLinkStoreModel.ProductList[0].Price - purchaseLinkStoreModel.ProductList[0].Discount) * (double) purchaseLinkStoreModel.Quantity + purchaseLinkStoreModel.ProductList[0].Tax;
        if (this.Session["Premiere_Prorate"] != null)
          model.PurchasePrice -= this.Session["Premiere_Prorate"].ToDouble();
        model.PurchaseProduct = purchaseLinkStoreModel.ProductList[0].DisplayName;
      }
    }
    catch
    {
    }
    try
    {
      model.ActivationKey = this.Session["AppliedActivationCode"].ToStringSafe();
      if (!string.IsNullOrWhiteSpace(purchasedItemID))
      {
        string str1 = purchasedItemID;
        char[] chArray = new char[1]{ '|' };
        foreach (object o in str1.Split(chArray))
        {
          long num = o.ToLong();
          string lower = sku.ToLower();
          string oldValue = "";
          if (lower.Contains("-nc-"))
            oldValue = Monnit.NotificationCredit.Load(num).ActivationCode;
          else if (lower.Contains("-hx-") || lower.Contains("-sp-") || lower.Contains("mna-gw-") || lower.Contains("mna-gps-"))
            oldValue = Credit.Load(num).ActivationCode;
          else if (lower.Contains("-ip-"))
            oldValue = AccountSubscription.Load(num).LastKeyUsed.ToString();
          if (!string.IsNullOrEmpty(oldValue))
          {
            string str2 = model.ActivationKey.Replace(oldValue, oldValue + " <b>- Applied</b>");
            model.ActivationKey = str2;
          }
        }
      }
    }
    catch
    {
    }
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult PaymentOption(
    long id,
    string returnURL,
    string returnURLAfterAdd,
    string sku,
    string qty)
  {
    if (!MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Retail",
        ErrorTranslateTag = "Retail/PaymentOption|",
        ErrorTitle = "Unauthorized access to billing pages",
        ErrorMessage = "You do not have permission to access this page."
      });
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__18.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__18.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SKU", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = RetailController.\u003C\u003Eo__18.\u003C\u003Ep__0.Target((CallSite) RetailController.\u003C\u003Eo__18.\u003C\u003Ep__0, this.ViewBag, sku == "undefined" ? "" : sku);
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__18.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__18.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Quantity", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = RetailController.\u003C\u003Eo__18.\u003C\u003Ep__1.Target((CallSite) RetailController.\u003C\u003Eo__18.\u003C\u003Ep__1, this.ViewBag, qty == "undefined" ? "" : qty);
    Account defaultAccount = MonnitSession.CurrentCustomer.DefaultAccount;
    if (defaultAccount == null || !MonnitSession.CurrentCustomer.CanSeeAccount(defaultAccount))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    Guid storeLinkGuid = defaultAccount.StoreLinkGuid;
    if (defaultAccount.StoreLinkGuid == Guid.Empty || !RetailController.DoesMEAUserExistForAccount(defaultAccount))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "LoginToStore",
        controller = "Retail",
        id = defaultAccount.AccountID,
        returnUrl = returnURL,
        returnURLAfterAdd = returnURLAfterAdd
      });
    try
    {
      PurchaseLinkStoreModel model = new PurchaseLinkStoreModel()
      {
        account = defaultAccount,
        PaymentInfoModelList = RetailController.RetrievePaymentInfoList(defaultAccount)
      };
      return !string.IsNullOrEmpty(returnURL) && model.PaymentInfoModelList.Count > 0 ? (ActionResult) this.Redirect(returnURL) : (ActionResult) this.View((object) model);
    }
    catch
    {
      return (ActionResult) this.View("Error");
    }
  }

  [AuthorizeDefault]
  public ActionResult StatesByCountry(string id)
  {
    DateTime utcNow = DateTime.UtcNow;
    string str = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
    string address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/GetStatesForCountry/{0}", (object) OverviewControllerBase.GetTempAuthToken());
    string body = $"{{\"countryID\":\"{id}\"}}";
    List<XElement> model = new List<XElement>();
    try
    {
      XDocument secured = PurchaseBase.SendToSecured(address, body);
      List<XElement> list = secured.Descendants((XName) "IDName").ToList<XElement>();
      model.AddRange((IEnumerable<XElement>) list);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-StatesByCountry",
          MachineName = Environment.MachineName,
          RequestBody = body,
          ResponseBody = secured.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
    }
    catch (Exception ex)
    {
      ex.Log("Retail.StatesByCountry: ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-StatesByCountry",
          MachineName = Environment.MachineName,
          RequestBody = body,
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return (ActionResult) this.PartialView((object) model);
  }

  [AuthorizeDefault]
  public ActionResult CreateAttemptGuid(long id, string type)
  {
    try
    {
      Account acct = Account.Load(id);
      if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct))
        return (ActionResult) this.Content("Failed");
      if (!MonnitSession.CustomerCan("Can_Access_Billing"))
        return (ActionResult) this.Content("Failed");
      this.Session["socialRequest"] = (object) null;
      StoreLinkGuidModel storeLinkGuidModel = new StoreLinkGuidModel();
      storeLinkGuidModel.AccountID = id;
      storeLinkGuidModel.LocalGuid = Guid.NewGuid();
      storeLinkGuidModel.LocalExpirationDate = DateTime.UtcNow.AddMinutes(2.0);
      this.Session["socialRequest"] = (object) storeLinkGuidModel;
      return (ActionResult) this.Redirect($"{ConfigData.AppSettings("MEA_API_Location")}Account/iMonnitExternalLogin?attemptGUID={storeLinkGuidModel.LocalGuid.ToString()}&provider={type}&returnUrl=");
    }
    catch
    {
    }
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
    {
      ErrorLocation = "Retail",
      ErrorTranslateTag = "Retail/PaymentOption|",
      ErrorTitle = "Store Link Failed",
      ErrorMessage = "External account login failed"
    });
  }

  [AuthorizeDefault]
  public ActionResult LinkConfirmation() => (ActionResult) this.View();

  [AuthorizeDefault]
  public ActionResult SocailAuthStoreLogin(string attemptGuid, string linkGuid)
  {
    StoreLinkGuidModel storeLinkGuidModel = (StoreLinkGuidModel) this.Session["socialRequest"];
    if (storeLinkGuidModel == null)
      return (ActionResult) this.Content("Failed");
    Account account = Account.Load(storeLinkGuidModel.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account) || !MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.Content("Failed");
    if (!(storeLinkGuidModel.LocalGuid == attemptGuid.ToGuid()) || !(storeLinkGuidModel.LocalExpirationDate > DateTime.UtcNow))
      return (ActionResult) this.Content("Failed: Link time elapased");
    string str = RetailController.RetrieveStoreLinkKey(string.Empty, string.Empty, linkGuid, account.CompanyName, account.AccountID);
    string o;
    long num;
    try
    {
      string[] strArray = str.Split(new char[1]{ '|' }, StringSplitOptions.RemoveEmptyEntries);
      o = strArray[0].ToString();
      num = strArray[1].ToLong();
    }
    catch
    {
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Retail",
        ErrorTranslateTag = "Retail/PaymentOption|",
        ErrorTitle = "Store Link Failed",
        ErrorMessage = ("External account login failed: " + str)
      });
    }
    if (o.Contains("Failed") || o.ToGuid() == Guid.Empty || num < 0L)
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Retail",
        ErrorTranslateTag = "Retail/PaymentOption|",
        ErrorTitle = "Store Link Failed",
        ErrorMessage = ("External account login failed: " + str)
      });
    try
    {
      account.StoreLinkGuid = o.ToGuid();
      account.StoreUserID = num;
      account.Save();
      MonnitSession.CurrentCustomer.Account = account;
      MonnitSession.CurrentStoreLinkInfo = RetailController.RetrieveStoreAccountInfo(account);
      return (ActionResult) this.View("LinkConfirmation");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Retail",
        ErrorTranslateTag = "Retail/PaymentOption|",
        ErrorTitle = "Store Link Failed",
        ErrorMessage = ("External account login failed: " + ex.Message)
      });
    }
  }

  [AuthorizeDefault]
  public ActionResult LoginToStore(long id, string returnUrl, string returnURLAfterAdd)
  {
    Account acct = Account.Load(id);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Retail",
        ErrorTranslateTag = "Retail/PaymentOption|",
        ErrorTitle = "Unauthorized access to billing pages",
        ErrorMessage = "You do not have permission to access this page."
      });
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__23.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__23.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnURL", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = RetailController.\u003C\u003Eo__23.\u003C\u003Ep__0.Target((CallSite) RetailController.\u003C\u003Eo__23.\u003C\u003Ep__0, this.ViewBag, returnUrl);
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__23.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__23.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ReturnURLAfterAdd", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = RetailController.\u003C\u003Eo__23.\u003C\u003Ep__1.Target((CallSite) RetailController.\u003C\u003Eo__23.\u003C\u003Ep__1, this.ViewBag, returnURLAfterAdd);
    Guid storeLinkGuid = acct.StoreLinkGuid;
    if (acct.StoreLinkGuid != Guid.Empty)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "PaymentOption",
        controller = "Retail",
        id = acct.AccountID,
        returnUrl = returnUrl,
        returnURLAfterAdd = returnURLAfterAdd
      });
    return (ActionResult) this.View((object) new StoreLoginModel()
    {
      account = acct
    });
  }

  [AuthorizeDefault]
  public ActionResult SendLinkCode(long id, string email)
  {
    Account acct = Account.Load(id);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct) || !MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.Content("Unauthorized: User not allowed to access this feature.");
    if (string.IsNullOrEmpty(email))
      return (ActionResult) this.Content("Email Address Required");
    if (UnsubscribedNotificationEmail.EmailIsUnsubscribed(email))
      return (ActionResult) this.Content("This Email address has opted out from receiving Emails. Please try a different Email address, or contact support to change opt out status of this Email.");
    string content = AccountControllerBase.TrySendStoreLinkCode(email);
    if (content.StartsWith("Failed:"))
      return (ActionResult) this.Content(content);
    foreach (CustomerLinkRequest customerLinkRequest in CustomerLinkRequest.LoadByEmail(email))
    {
      customerLinkRequest.IsDeleted = true;
      customerLinkRequest.Save();
    }
    new CustomerLinkRequest()
    {
      EmailAddress = email,
      CustomerID = MonnitSession.CurrentCustomer.CustomerID,
      CreateDate = DateTime.UtcNow,
      LinkCode = content
    }.Save();
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult CheckLinkCode(
    long id,
    string keycode,
    string firstName,
    string lastName,
    string returnUrl)
  {
    if (string.IsNullOrWhiteSpace(firstName))
      return (ActionResult) this.Content("Failed: First name is required");
    if (string.IsNullOrWhiteSpace(lastName))
      return (ActionResult) this.Content("Failed: Last name is required");
    Account account = Account.Load(id);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account) || !MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.Content("Unauthorized: User not allowed to access this feature.");
    CustomerLinkRequest customerLinkRequest = CustomerLinkRequest.LoadActiveByLinkCode(keycode.Trim());
    if (customerLinkRequest == null || customerLinkRequest.CreateDate < DateTime.UtcNow.AddMinutes(-15.0))
      return (ActionResult) this.Content("Unauthorized: Incorrect or expired Link Code.");
    customerLinkRequest.ActivationDate = DateTime.UtcNow;
    customerLinkRequest.Save();
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__25.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__25.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnURL", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = RetailController.\u003C\u003Eo__25.\u003C\u003Ep__0.Target((CallSite) RetailController.\u003C\u003Eo__25.\u003C\u003Ep__0, this.ViewBag, returnUrl);
    string str = RetailController.RetrieveStoreLinkKey(customerLinkRequest.EmailAddress);
    string o;
    long num;
    try
    {
      if (str == "No Account Found")
        str = RetailController.CreateStoreBillingProfile(account, customerLinkRequest.EmailAddress, firstName, lastName);
      string[] strArray = str.Split(new char[1]{ '|' }, StringSplitOptions.RemoveEmptyEntries);
      o = strArray[0].ToString();
      num = strArray[1].ToLong();
    }
    catch
    {
      return (ActionResult) this.Content("Failed: " + str);
    }
    if (o.Contains("Failed") || o.ToGuid() == Guid.Empty || num < 0L)
      return (ActionResult) this.Content("Failed: " + str);
    try
    {
      account.StoreLinkGuid = o.ToGuid();
      account.StoreUserID = num;
      account.Save();
      MonnitSession.CurrentCustomer.Account = account;
      MonnitSession.CurrentCustomer.DefaultAccountReload();
      MonnitSession.CurrentStoreLinkInfo = RetailController.RetrieveStoreAccountInfo(account);
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content("Failed: " + ex.Message);
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult LoginToStore(long id, StoreLoginModel model, string returnURL)
  {
    Account account = Account.Load(id);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    model.account = account;
    if (!MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Retail",
        ErrorTranslateTag = "Retail/PaymentOption|",
        ErrorTitle = "Unauthorized access to billing pages",
        ErrorMessage = "You do not have permission to access this page."
      });
    object obj1;
    if (string.IsNullOrEmpty(model.UserName))
    {
      this.ModelState.AddModelError("username", "Username Required");
      object viewBag = this.ViewBag;
      // ISSUE: reference to a compiler-generated field
      if (RetailController.\u003C\u003Eo__26.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RetailController.\u003C\u003Eo__26.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.IsEvent(CSharpBinderFlags.None, "ErrorMessage", typeof (RetailController)));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      bool flag = RetailController.\u003C\u003Eo__26.\u003C\u003Ep__1.Target((CallSite) RetailController.\u003C\u003Eo__26.\u003C\u003Ep__1, viewBag);
      if (!flag)
      {
        // ISSUE: reference to a compiler-generated field
        if (RetailController.\u003C\u003Eo__26.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RetailController.\u003C\u003Eo__26.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ErrorMessage", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        obj1 = RetailController.\u003C\u003Eo__26.\u003C\u003Ep__0.Target((CallSite) RetailController.\u003C\u003Eo__26.\u003C\u003Ep__0, viewBag);
      }
      if (!flag)
      {
        // ISSUE: reference to a compiler-generated field
        if (RetailController.\u003C\u003Eo__26.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RetailController.\u003C\u003Eo__26.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.ValueFromCompoundAssignment, "ErrorMessage", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object, object> target = RetailController.\u003C\u003Eo__26.\u003C\u003Ep__4.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object, object>> p4 = RetailController.\u003C\u003Eo__26.\u003C\u003Ep__4;
        object obj2 = viewBag;
        // ISSUE: reference to a compiler-generated field
        if (RetailController.\u003C\u003Eo__26.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RetailController.\u003C\u003Eo__26.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.AddAssign, typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = RetailController.\u003C\u003Eo__26.\u003C\u003Ep__3.Target((CallSite) RetailController.\u003C\u003Eo__26.\u003C\u003Ep__3, obj1, "Username Required ");
        object obj4 = target((CallSite) p4, obj2, obj3);
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (RetailController.\u003C\u003Eo__26.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RetailController.\u003C\u003Eo__26.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSpecialName | CSharpBinderFlags.ResultDiscarded, "add_ErrorMessage", (IEnumerable<Type>) null, typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj5 = RetailController.\u003C\u003Eo__26.\u003C\u003Ep__2.Target((CallSite) RetailController.\u003C\u003Eo__26.\u003C\u003Ep__2, viewBag, "Username Required ");
      }
    }
    if (string.IsNullOrEmpty(model.Password))
    {
      this.ModelState.AddModelError("password", "Password Required");
      object viewBag = this.ViewBag;
      // ISSUE: reference to a compiler-generated field
      if (RetailController.\u003C\u003Eo__26.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RetailController.\u003C\u003Eo__26.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, bool>>.Create(Binder.IsEvent(CSharpBinderFlags.None, "ErrorMessage", typeof (RetailController)));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      bool flag = RetailController.\u003C\u003Eo__26.\u003C\u003Ep__6.Target((CallSite) RetailController.\u003C\u003Eo__26.\u003C\u003Ep__6, viewBag);
      if (!flag)
      {
        // ISSUE: reference to a compiler-generated field
        if (RetailController.\u003C\u003Eo__26.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RetailController.\u003C\u003Eo__26.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ErrorMessage", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        obj1 = RetailController.\u003C\u003Eo__26.\u003C\u003Ep__5.Target((CallSite) RetailController.\u003C\u003Eo__26.\u003C\u003Ep__5, viewBag);
      }
      if (!flag)
      {
        // ISSUE: reference to a compiler-generated field
        if (RetailController.\u003C\u003Eo__26.\u003C\u003Ep__9 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RetailController.\u003C\u003Eo__26.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.ValueFromCompoundAssignment, "ErrorMessage", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object, object> target = RetailController.\u003C\u003Eo__26.\u003C\u003Ep__9.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object, object>> p9 = RetailController.\u003C\u003Eo__26.\u003C\u003Ep__9;
        object obj6 = viewBag;
        // ISSUE: reference to a compiler-generated field
        if (RetailController.\u003C\u003Eo__26.\u003C\u003Ep__8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RetailController.\u003C\u003Eo__26.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.AddAssign, typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj7 = RetailController.\u003C\u003Eo__26.\u003C\u003Ep__8.Target((CallSite) RetailController.\u003C\u003Eo__26.\u003C\u003Ep__8, obj1, "Password Required");
        object obj8 = target((CallSite) p9, obj6, obj7);
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (RetailController.\u003C\u003Eo__26.\u003C\u003Ep__7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RetailController.\u003C\u003Eo__26.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSpecialName | CSharpBinderFlags.ResultDiscarded, "add_ErrorMessage", (IEnumerable<Type>) null, typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj9 = RetailController.\u003C\u003Eo__26.\u003C\u003Ep__7.Target((CallSite) RetailController.\u003C\u003Eo__26.\u003C\u003Ep__7, viewBag, "Password Required");
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__26.\u003C\u003Ep__10 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__26.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (returnURL), typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj10 = RetailController.\u003C\u003Eo__26.\u003C\u003Ep__10.Target((CallSite) RetailController.\u003C\u003Eo__26.\u003C\u003Ep__10, this.ViewBag, returnURL);
    if (this.ModelState.IsValid)
    {
      string errorMessage = RetailController.RetrieveStoreLinkKey(model.UserName, model.Password, string.Empty, account.CompanyName, account.AccountID);
      string o;
      long num;
      try
      {
        string[] strArray = errorMessage.Split(new char[1]
        {
          '|'
        }, StringSplitOptions.RemoveEmptyEntries);
        o = strArray[0].ToString();
        num = strArray[1].ToLong();
      }
      catch
      {
        this.ModelState.AddModelError("", errorMessage);
        return (ActionResult) this.View((object) model);
      }
      if (o.Contains("Failed") || o.ToGuid() == Guid.Empty || num < 0L)
      {
        this.ModelState.AddModelError("", errorMessage);
        return (ActionResult) this.View((object) model);
      }
      try
      {
        account.StoreLinkGuid = o.ToGuid();
        account.StoreUserID = num;
        account.Save();
        MonnitSession.CurrentCustomer.Account = account;
        MonnitSession.CurrentStoreLinkInfo = RetailController.RetrieveStoreAccountInfo(account);
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "PaymentOption",
          controller = "Retail",
          id = account.AccountID,
          returnURL = returnURL
        });
      }
      catch (Exception ex)
      {
        this.ModelState.AddModelError("", ex.Message);
      }
    }
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [ValidateAntiForgeryToken]
  public ActionResult SubmitNewPayment(long id, FormCollection collection)
  {
    PurchaseLinkStoreModel model1 = (PurchaseLinkStoreModel) null;
    try
    {
      Account account = Account.Load(id);
      if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index",
          controller = "Overview"
        });
      if (!MonnitSession.CustomerCan("Can_Access_Billing"))
      {
        // ISSUE: reference to a compiler-generated field
        if (RetailController.\u003C\u003Eo__27.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RetailController.\u003C\u003Eo__27.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ErrorMsg", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = RetailController.\u003C\u003Eo__27.\u003C\u003Ep__0.Target((CallSite) RetailController.\u003C\u003Eo__27.\u003C\u003Ep__0, this.ViewBag, "Failed: Not authorized to create a payment");
      }
      else
      {
        PaymentInfoModel model2 = new PaymentInfoModel();
        model2.CustomerName = collection["cardHolder"].SanitizeHTMLEvent();
        model2.CardNumber = collection["cardNumber"];
        model2.ExpMonth = collection["expirationMonth"].ToInt();
        model2.ExpYear = collection["expirationYear"].ToInt();
        model2.Address = collection["address1"].SanitizeHTMLEvent();
        model2.Address2 = collection["address2"].SanitizeHTMLEvent();
        model2.City = collection["city"].SanitizeHTMLEvent();
        model2.State = collection["State"].SanitizeHTMLEvent();
        model2.Country = collection["country"].ToString();
        model2.PostalCode = collection["zipcode"];
        model1 = new PurchaseLinkStoreModel();
        model1.account = account;
        model1.PaymentInfoModelList = new List<PaymentInfoModel>();
        try
        {
          string errorMsg = "";
          model1.PaymentInfoModelList = RetailController.AddCreditCard(model2, account, out errorMsg);
          // ISSUE: reference to a compiler-generated field
          if (RetailController.\u003C\u003Eo__27.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            RetailController.\u003C\u003Eo__27.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ErrorMsg", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = RetailController.\u003C\u003Eo__27.\u003C\u003Ep__1.Target((CallSite) RetailController.\u003C\u003Eo__27.\u003C\u003Ep__1, this.ViewBag, errorMsg);
        }
        catch (Exception ex)
        {
          ex.Log($"RetailController.SubmitNewPayment.AddCreditCard[AccountID: {id.ToString()}] ");
        }
        PaymentInfoModel paymentInfoModel = model1.PaymentInfoModelList.Where<PaymentInfoModel>((Func<PaymentInfoModel, bool>) (x => x.WasSaved)).FirstOrDefault<PaymentInfoModel>();
        if (paymentInfoModel != null)
        {
          string str1 = collection["sku"];
          string str2 = collection["qty"];
          long profileId = paymentInfoModel.ProfileID;
          if (!string.IsNullOrWhiteSpace(str1))
            return (ActionResult) this.Content($"Redirect|/Retail/PurchasePreview/{id.ToString()}/?profileID={profileId.ToString()}&sku={str1}&qty={str2}");
        }
        if (!string.IsNullOrEmpty(collection["returnURL2"]))
          return (ActionResult) this.Content("Redirect|" + collection["returnURL2"].ToString());
      }
    }
    catch (Exception ex)
    {
      ex.Log($"RetailController.SubmitNewPayment[AccountID: {id.ToString()}] ");
    }
    return (ActionResult) this.PartialView("PaymentInfoList", (object) model1);
  }

  [AuthorizeDefault]
  [ValidateAntiForgeryToken]
  public ActionResult SubmitNewPaymentLocations(long id, FormCollection collection)
  {
    string content = "Failed";
    try
    {
      Account account = Account.Load(id);
      if (!MonnitSession.CustomerCan("Can_Access_Billing") || account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account))
      {
        content = "Failed: Not authorized to create a payment";
      }
      else
      {
        PaymentInfoModel model = new PaymentInfoModel();
        model.CustomerName = collection["cardHolder"].SanitizeHTMLEvent();
        model.CardNumber = collection["cardNumber"];
        model.ExpMonth = collection["expirationMonth"].ToInt();
        model.ExpYear = collection["expirationYear"].ToInt();
        model.Address = collection["address1"].SanitizeHTMLEvent();
        model.Address2 = collection["address2"].SanitizeHTMLEvent();
        model.City = collection["city"].SanitizeHTMLEvent();
        model.State = collection["State"].SanitizeHTMLEvent();
        model.Country = collection["country"].ToString();
        model.PostalCode = collection["zipcode"];
        try
        {
          string errorMsg = "";
          List<PaymentInfoModel> source = RetailController.AddCreditCard(model, account, out errorMsg);
          PaymentInfoModel paymentInfoModel1 = source != null ? source.Where<PaymentInfoModel>((Func<PaymentInfoModel, bool>) (x => x.WasSaved)).FirstOrDefault<PaymentInfoModel>() : (PaymentInfoModel) null;
          PaymentInfoModel paymentInfoModel2 = source != null ? source.Where<PaymentInfoModel>((Func<PaymentInfoModel, bool>) (x => x.CustomerName == "MEA-Error")).FirstOrDefault<PaymentInfoModel>() : (PaymentInfoModel) null;
          string monthName = paymentInfoModel1 != null ? CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(paymentInfoModel1.ExpMonth) : "";
          if (!string.IsNullOrWhiteSpace(errorMsg))
            content = errorMsg;
          else if (paymentInfoModel2 != null)
            content = paymentInfoModel2.CardNumber;
          else if (paymentInfoModel1 != null)
            content = $"Success:{paymentInfoModel1.ProfileID.ToString()}|{paymentInfoModel1.CardNumber.Remove(0, paymentInfoModel1.CardNumber.Length - 4)}|{monthName} {paymentInfoModel1.ExpYear.ToString()}|{paymentInfoModel1.CustomerName}";
        }
        catch (Exception ex)
        {
          ex.Log($"RetailController.SubmitNewPaymentLocations.AddCreditCard[AccountID: {id.ToString()}] ");
          content = "Failed: Failed to create payment";
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log($"RetailController.SubmitNewPaymentLocations[AccountID: {id.ToString()}] ");
      content = "Failed: Failed to create payment";
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public static List<PaymentInfoModel> AddCreditCard(
    PaymentInfoModel model,
    Account account,
    out string errorMsg)
  {
    errorMsg = "";
    DateTime utcNow = DateTime.UtcNow;
    string str1 = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str1) && bool.Parse(str1);
    string str2 = model.CardNumber.Replace(" ", "");
    string address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/AddStoreCreditCard/{0}?", (object) OverviewControllerBase.GetTempAuthToken());
    string body = $"{{\"storeUserID\":\"{account.StoreUserID.ToString()}\",\"storeLinkGuid\":\"{account.StoreLinkGuid.ToString()}\",\"name\":\"{model.CustomerName}\",\"cardNumber\":\"{str2}\",\"ExpMonth\":\"{model.ExpMonth.ToString()}\",\"ExpYear\":\"{model.ExpYear.ToString()}\",\"address\":\"{model.Address}\",\"address2\":\"{model.Address2}\",\"city\":\"{model.City}\",\"stateProvince\":\"{model.State}\",\"country\":\"{model.Country}\",\"postalCode\":\"{model.PostalCode}\"}}";
    string str3 = $"{{\"storeUserID\":\"{account.StoreUserID.ToString()}\",\"storeLinkGuid\":\"{account.StoreLinkGuid.ToString()}\",\"name\":\"{model.CustomerName}\",\"cardNumber\":\"************{str2.Substring(str2.Length - 4, 4)}\",\"ExpMonth\":\"{model.ExpMonth.ToString()}\",\"ExpYear\":\"{model.ExpYear.ToString()}\",\"address\":\"{model.Address}\",\"address2\":\"{model.Address2}\",\"city\":\"{model.City}\",\"stateProvince\":\"{model.State}\",\"country\":\"{model.Country}\",\"postalCode\":\"{model.PostalCode}\"}}";
    List<PaymentInfoModel> paymentInfoModelList = new List<PaymentInfoModel>();
    try
    {
      XDocument secured = PurchaseBase.SendToSecured(address, body);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-AddCreditCard",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {str3}",
          ResponseBody = secured.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      foreach (XElement descendant in secured.Descendants((XName) "APIPaymentInfo"))
      {
        string str4 = descendant.Attribute((XName) "CustomerName").Value;
        string str5 = descendant.Attribute((XName) "CardNumber").Value;
        if (str4 == "MEA-Error")
          errorMsg = str5;
        else
          paymentInfoModelList.Add(new PaymentInfoModel()
          {
            CustomerName = str4,
            CardNumber = str5,
            ExpMonth = Convert.ToInt32(descendant.Attribute((XName) "ExpMonth").Value),
            ExpYear = Convert.ToInt32(descendant.Attribute((XName) "ExpYear").Value),
            PostalCode = descendant.Attribute((XName) "PostalCode").Value,
            ProfileID = (long) Convert.ToInt32(descendant.Attribute((XName) "ProfileID").Value),
            WasSaved = Convert.ToBoolean(descendant.Attribute((XName) "WasSaved").Value),
            Address = descendant.Attribute((XName) "Address").Value,
            Address2 = descendant.Attribute((XName) "Address2").Value,
            City = descendant.Attribute((XName) "City").Value,
            Country = descendant.Attribute((XName) "Country").Value,
            State = descendant.Attribute((XName) "StateProvince").Value
          });
      }
    }
    catch (Exception ex)
    {
      ex.Log("RetailController.AddCreditCard ");
      errorMsg = "Failed";
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-AddCreditCard",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {str3}",
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return paymentInfoModelList;
  }

  [AuthorizeDefault]
  public ActionResult RemovePaymentMethod(
    long id,
    long profileID,
    long userID,
    Guid StoreLinkGuid,
    string sku,
    string qty)
  {
    DateTime utcNow = DateTime.UtcNow;
    string str1 = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str1) && bool.Parse(str1);
    Account acct = Account.Load(id);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Retail",
        ErrorTranslateTag = "Retail/SubmitNewPayment|",
        ErrorTitle = "Unauthorized access to billing pages",
        ErrorMessage = "You do not have permission to access this page."
      });
    PurchaseLinkStoreModel model = new PurchaseLinkStoreModel();
    model.account = acct;
    model.PaymentInfoModelList = new List<PaymentInfoModel>();
    string address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/RemoveStoreCreditCard/{0}?", (object) OverviewControllerBase.GetTempAuthToken());
    string body = $"{{\"storeLinkGuid\":\"{StoreLinkGuid.ToString()}\",\"profileID\":\"{profileID.ToString()}\",\"userID\":\"{userID.ToString()}\"}}";
    List<PaymentInfoModel> paymentInfoModelList = new List<PaymentInfoModel>();
    try
    {
      XDocument secured = PurchaseBase.SendToSecured(address, body);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RemovePaymentMethod",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = secured.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      foreach (XElement descendant in secured.Descendants((XName) "APIPaymentInfo"))
      {
        string str2 = descendant.Attribute((XName) "CustomerName").Value;
        string str3 = descendant.Attribute((XName) "CardNumber").Value;
        if (str2 == "MEA-Error")
        {
          // ISSUE: reference to a compiler-generated field
          if (RetailController.\u003C\u003Eo__30.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            RetailController.\u003C\u003Eo__30.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ErrorMsg", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = RetailController.\u003C\u003Eo__30.\u003C\u003Ep__0.Target((CallSite) RetailController.\u003C\u003Eo__30.\u003C\u003Ep__0, this.ViewBag, str3);
        }
        else
          paymentInfoModelList.Add(new PaymentInfoModel()
          {
            CustomerName = str2,
            CardNumber = str3,
            ExpMonth = Convert.ToInt32(descendant.Attribute((XName) "ExpMonth").Value),
            ExpYear = Convert.ToInt32(descendant.Attribute((XName) "ExpYear").Value),
            PostalCode = descendant.Attribute((XName) "PostalCode").Value,
            ProfileID = (long) Convert.ToInt32(descendant.Attribute((XName) "ProfileID").Value),
            Address = descendant.Attribute((XName) "Address").Value,
            Address2 = descendant.Attribute((XName) "Address2").Value,
            City = descendant.Attribute((XName) "City").Value,
            Country = descendant.Attribute((XName) "Country").Value,
            State = descendant.Attribute((XName) "StateProvince").Value
          });
      }
      model.PaymentInfoModelList = paymentInfoModelList;
    }
    catch (Exception ex)
    {
      ex.Log($"RetailController.RemovePaymentMethod[ID: {id.ToString()}] ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RemovePaymentMethod",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__30.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__30.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SKU", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = RetailController.\u003C\u003Eo__30.\u003C\u003Ep__1.Target((CallSite) RetailController.\u003C\u003Eo__30.\u003C\u003Ep__1, this.ViewBag, sku == "undefined" ? "" : sku);
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__30.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__30.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Quantity", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = RetailController.\u003C\u003Eo__30.\u003C\u003Ep__2.Target((CallSite) RetailController.\u003C\u003Eo__30.\u003C\u003Ep__2, this.ViewBag, qty == "undefined" ? "" : qty);
    return (ActionResult) this.PartialView("PaymentInfoList", (object) model);
  }

  [AuthorizeDefault]
  public ActionResult SetDefaultPaymentMethod(long id, long profileID)
  {
    string str = "Failed";
    Account acct = Account.Load(id);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      str = "Failed";
    string content;
    try
    {
      acct.DefaultPaymentID = profileID;
      acct.Save();
      content = "Success";
    }
    catch (Exception ex)
    {
      ex.Log($"RetailController.SetDefaultPaymentMethod[AccountID: {id.ToString()}][ProfileID: {profileID.ToString()}]");
      content = "Failed";
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public ActionResult PaymentInfoList()
  {
    PurchaseLinkStoreModel model = (PurchaseLinkStoreModel) null;
    try
    {
      Account account = !MonnitSession.UserIsCustomerProxied ? MonnitSession.CurrentCustomer.Account : MonnitSession.OldCustomer.Account;
      model = RetailController.CreatePurchaseLinkStoreModel_WithPaymentAndProductList($"PaymentInfoList[ID: {account.AccountID.ToString()}]", "Subscriptions", account);
    }
    catch (Exception ex)
    {
      ex.Log("RetailController.PaymentInfoList[Get] ");
    }
    return (ActionResult) this.PartialView(nameof (PaymentInfoList), (object) model);
  }

  [AuthorizeDefault]
  private static string RetrieveStoreLinkKey(
    string username,
    string password,
    string linkGuid,
    string iMonnitAccountName,
    long accountID)
  {
    DateTime utcNow = DateTime.UtcNow;
    string str = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
    string address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/RetrieveStoreLinkKey/{0}?", (object) OverviewControllerBase.GetTempAuthToken());
    string body = $"{{\"username\":\"{username}\",\"password\":\"{password}\",\"linkGuid\":\"{linkGuid}\",\"iMonnitAccountName\":\"{iMonnitAccountName}\",\"iMonnitAccountID\":\"{accountID.ToString()}\" }}";
    try
    {
      XDocument secured = PurchaseBase.SendToSecured(address, body);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = $"MEA-RetrieveStoreLinkKey[Username: {username}]",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = secured.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      return secured.Descendants((XName) "Result").Single<XElement>().Value.ToString();
    }
    catch (Exception ex)
    {
      ex.Log($"RetailController.RetrieveStoreLinkKey[Username: {username}] ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = $"MEA-RetrieveStoreLinkKey[Username: {username}]",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
      return "Failed";
    }
  }

  [AuthorizeDefault]
  private static string RetrieveStoreLinkKey(string email)
  {
    DateTime utcNow = DateTime.UtcNow;
    string str = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
    string address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/RetrieveStoreLinkKeyByEmail/{0}?", (object) OverviewControllerBase.GetTempAuthToken());
    string body = $"{{\"email\":\"{email}\" }}";
    try
    {
      XDocument secured = PurchaseBase.SendToSecured(address, body);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = $"MEA-RetrieveStoreLinkKey[Email: {email}]",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = secured.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      return secured.Descendants((XName) "Result").Single<XElement>().Value.ToString();
    }
    catch (Exception ex)
    {
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = $"MEA-RetrieveStoreLinkKey[Email: {email}]",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
      return "Failed";
    }
  }

  [AuthorizeDefault]
  public ActionResult RetrieveStoreLoginGuid(long id)
  {
    DateTime utcNow = DateTime.UtcNow;
    string str = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
    Account acct = Account.Load(id);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return (ActionResult) this.Content("Failed: Unauthorized Account");
    if (!MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.Content("Failed: Unauthorized Access");
    string address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/RetrieveLoginGuid/{0}?", (object) OverviewControllerBase.GetTempAuthToken());
    string body = $"{{\"storeLinkGuid\":\"{acct.StoreLinkGuid.ToString()}\"}}";
    string url = acct.CurrentSubscription.AccountSubscriptionType.SubscriptionLink;
    try
    {
      XDocument secured = PurchaseBase.SendToSecured(address, body);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RetrieveStoreLoginGuid",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = secured.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      secured.Descendants((XName) "Result").Single<XElement>().Value.ToString();
      url = $"{ConfigData.AppSettings("MEA_API_Location")}Account/SensorPortalDirectLogIn?linkGuid={acct.StoreLinkGuid.ToString()}&userProfileID={acct.StoreUserID}";
    }
    catch (Exception ex)
    {
      ex.Log($"RetailController.RetrieveStoreLoginGuid[ID: {id.ToString()}]");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RetrieveStoreLoginGuid",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return (ActionResult) this.Redirect(url);
  }

  [AuthorizeDefault]
  public static StoreAccountInfoModel RetrieveStoreAccountInfo(Account a)
  {
    DateTime utcNow = DateTime.UtcNow;
    string str = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
    string address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/RetrieveStoreAccountInfo/{0}?", (object) OverviewControllerBase.GetTempAuthToken());
    string body = $"{{\"storeLinkGuid\":\"{a.StoreLinkGuid.ToString()}\",\"userProfileID\":\"{a.StoreUserID.ToString()}\" }}";
    StoreAccountInfoModel accountInfoModel = new StoreAccountInfoModel();
    try
    {
      XDocument secured = PurchaseBase.SendToSecured(address, body);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RetrieveStoreAccountInfo",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = secured.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      accountInfoModel.AccountID = a.AccountID;
      accountInfoModel.UserName = secured.Descendants((XName) "APIStoreAccountInfo").First<XElement>().Attribute((XName) "UserName").Value;
      accountInfoModel.StoreLinkGuid = secured.Descendants((XName) "APIStoreAccountInfo").First<XElement>().Attribute((XName) "StoreLinkGuid").Value.ToGuid();
      accountInfoModel.StoreUserID = secured.Descendants((XName) "APIStoreAccountInfo").First<XElement>().Attribute((XName) "StoreUserID").Value.ToLong();
    }
    catch (Exception ex)
    {
      ex.Log($"RetailController.RetrieveStoreAccountInfo[AccountID: {(a != null ? a.AccountID.ToString() : "")}] ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RetrieveStoreAccountInfo",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return accountInfoModel;
  }

  [AuthorizeDefault]
  public ActionResult RemoveStoreLink(long id)
  {
    Account acct = Account.Load(id);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return (ActionResult) this.Content("Failed: Unauthorized Account");
    if (!MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.Content("Failed: Unauthorized Access");
    DateTime utcNow = DateTime.UtcNow;
    string str = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
    string address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/RemoveStoreLink/{0}?", (object) OverviewControllerBase.GetTempAuthToken());
    string body = $"{{\"storeLinkGuid\":\"{acct.StoreLinkGuid.ToString()}\",\"iMonnitAccountID\":\"{acct.AccountID.ToString()}\" }}";
    try
    {
      XDocument secured = PurchaseBase.SendToSecured(address, body);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RemoveStoreLink",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = secured.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      string content = secured.Descendants((XName) "Result").Single<XElement>().Value.ToString();
      if (content != "Success")
        return (ActionResult) this.Content(content);
      acct.StoreLinkGuid = Guid.Empty;
      acct.Save();
      MonnitSession.CurrentCustomer.Account = acct;
      MonnitSession.CurrentStoreLinkInfo = (StoreAccountInfoModel) null;
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log($"RetailController.RemoveStoreLink[ID: {id.ToString()}] ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RemoveStoreLink",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ProcessPurchase(
    long id,
    long profileID,
    string sku,
    double tax,
    int qty,
    string toBeEmailed)
  {
    Account account1 = Account.Load(id);
    if (account1 == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account1))
      return (ActionResult) this.Content("Failed: Unauthorized Account");
    if (!MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.Content("Failed: Unauthorized Access");
    DateTime utcNow = DateTime.UtcNow;
    string IPAddress = this.Request.ClientIP();
    string storeLinkGuid = account1.StoreLinkGuid.ToString();
    long storeUserId = account1.StoreUserID;
    Customer currentUser = MonnitSession.CurrentCustomer;
    if (MonnitSession.UserIsCustomerProxied)
    {
      currentUser = Customer.Load(MonnitSession.CurrentCustomer.CustomerID);
      Account account2 = Account.Load(currentUser.AccountID);
      storeLinkGuid = account2.StoreLinkGuid.ToString();
      storeUserId = account2.StoreUserID;
    }
    double prorateAmount = this.Session["Premiere_Prorate"] != null ? this.Session["Premiere_Prorate"].ToDouble() : 0.0;
    ActivationCode activationCode = PurchaseBase.BasePurchase(id, utcNow, IPAddress, storeLinkGuid, storeUserId, currentUser, account1, profileID, sku, tax, qty, toBeEmailed, MonnitSession.Session.SessionID, MonnitSession.CurrentSubscription, prorateAmount);
    this.Session["AppliedActivationCode"] = (object) activationCode.Code;
    return (ActionResult) this.Content(activationCode.Status);
  }

  [AuthorizeDefault]
  private static string CreateStoreBillingProfile(
    Account account,
    string email,
    string firstName,
    string lastName)
  {
    DateTime utcNow = DateTime.UtcNow;
    string str = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
    Customer currentCustomer = MonnitSession.CurrentCustomer;
    if (string.IsNullOrEmpty(email))
      email = currentCustomer.NotificationEmail;
    if (string.IsNullOrEmpty(firstName))
      firstName = currentCustomer.FirstName;
    if (string.IsNullOrEmpty(lastName))
      lastName = currentCustomer.LastName;
    string address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/CreateSubscriptionBillingProfileFromiMonnit/{0}?", (object) OverviewControllerBase.GetTempAuthToken());
    string body = $"{{\"iMonnitAccountID\":\"{account.AccountID.ToString()}\",\"iMonnitAccountName\":\"{account.CompanyName}\",\"email\":\"{email}\",\"address\":\"{account.PrimaryAddress.Address} {account.PrimaryAddress.Address2}\",\"city\":\"{account.PrimaryAddress.City}\",\"stateProvince\":\"{account.PrimaryAddress.State}\",\"postalCode\":\"{account.PrimaryAddress.PostalCode}\",\"country\":\"{account.PrimaryAddress.Country}\",\"phone\":\"{currentCustomer.NotificationPhone}\",\"firstName\":\"{firstName}\",\"lastName\":\"{lastName}\" }}";
    try
    {
      XDocument secured = PurchaseBase.SendToSecured(address, body);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-CreateStoreBillingProfile",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = secured.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      return secured.Descendants((XName) "Result").Single<XElement>().Value.ToString();
    }
    catch (Exception ex)
    {
      ex.Log($"RetailController.CreateStoreBillingProfile[AccountID: {(account != null ? account.AccountID.ToString() : "")}] ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-CreateStoreBillingProfile",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
      return "Failed";
    }
  }

  [AuthorizeDefault]
  private static bool DoesMEAUserExistForAccount(Account account)
  {
    bool flag1 = false;
    if (account != null && account.StoreLinkGuid != Guid.Empty)
    {
      Guid storeLinkGuid = account.StoreLinkGuid;
      DateTime utcNow = DateTime.UtcNow;
      string str = ConfigData.AppSettings("MeaApiLogging");
      bool flag2 = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
      string address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/RetrieveLoginGuid/{0}?", (object) OverviewControllerBase.GetTempAuthToken());
      string body = $"{{\"storeLinkGuid\":\"{storeLinkGuid.ToString()}\" }}";
      try
      {
        XDocument secured = PurchaseBase.SendToSecured(address, body);
        if (flag2)
        {
          TimeSpan timeSpan = DateTime.UtcNow - utcNow;
          new MeaApiLog()
          {
            MethodName = "MEA-DoesMEAUserExistForAccount",
            MachineName = Environment.MachineName,
            RequestBody = $"[URL: {address}] {body}",
            ResponseBody = secured.ToString(),
            CreateDate = utcNow,
            SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
            IsException = false
          }.Save();
        }
        if (secured.Descendants((XName) "Result").FirstOrDefault<XElement>().Value.Contains("Failed"))
        {
          account.StoreLinkGuid = Guid.Empty;
          account.Save();
        }
        else
          flag1 = true;
      }
      catch (Exception ex)
      {
        ex.Log($"Retail.DoesMEAUserExistForAccount[AccountID: {account.AccountID.ToString()}][Guid: {storeLinkGuid.ToString()}]  Message: ");
        if (flag2)
        {
          TimeSpan timeSpan = DateTime.UtcNow - utcNow;
          new MeaApiLog()
          {
            MethodName = "MEA-DoesMEAUserExistForAccount",
            MachineName = Environment.MachineName,
            RequestBody = $"[URL: {address}] {body}",
            ResponseBody = ex.Message,
            CreateDate = utcNow,
            SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
            IsException = true
          }.Save();
        }
      }
    }
    return flag1;
  }

  [AuthorizeDefault]
  private static List<PaymentInfoModel> RetrievePaymentInfoList(Account account)
  {
    DateTime utcNow = DateTime.UtcNow;
    string str = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
    string address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/RetrievePaymentInfoList/{0}?", (object) OverviewControllerBase.GetTempAuthToken());
    string body = $"{{\"storeLinkGuid\":\"{account.StoreLinkGuid.ToString()}\",\"userProfileID\":\"{account.StoreUserID.ToString()}\" }}";
    List<PaymentInfoModel> paymentInfoModelList = new List<PaymentInfoModel>();
    try
    {
      XDocument secured = PurchaseBase.SendToSecured(address, body);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RetrievePaymentInfoList",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = secured.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      foreach (XElement descendant in secured.Descendants((XName) "APIPaymentInfo"))
      {
        if (descendant.Attribute((XName) "CustomerName").Value != "MEA-Error")
          paymentInfoModelList.Add(new PaymentInfoModel()
          {
            CustomerName = descendant.Attribute((XName) "CustomerName").Value,
            CardNumber = descendant.Attribute((XName) "CardNumber").Value,
            ExpMonth = Convert.ToInt32(descendant.Attribute((XName) "ExpMonth").Value),
            ExpYear = Convert.ToInt32(descendant.Attribute((XName) "ExpYear").Value),
            PostalCode = descendant.Attribute((XName) "PostalCode").Value,
            ProfileID = (long) Convert.ToInt32(descendant.Attribute((XName) "ProfileID").Value),
            Address = descendant.Attribute((XName) "Address").Value,
            Address2 = descendant.Attribute((XName) "Address2").Value,
            City = descendant.Attribute((XName) "City").Value,
            Country = descendant.Attribute((XName) "Country").Value,
            State = descendant.Attribute((XName) "StateProvince").Value
          });
      }
    }
    catch (Exception ex)
    {
      ex.Log("Retail.RetrievePaymentInfoList  Message: ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RetrievePaymentInfoList",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return paymentInfoModelList;
  }

  [AuthorizeDefault]
  public static List<ProductInfoModel> RetrieveProductInfoList(
    string StoreLinkGuid,
    string productType)
  {
    DateTime utcNow = DateTime.UtcNow;
    string str = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
    string address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/RetrieveProductInfoList/{0}?", (object) OverviewControllerBase.GetTempAuthToken());
    string body = $"{{\"storeLinkGuid\":\"{StoreLinkGuid}\",\"sessionID\":\"{MonnitSession.Session.SessionID}\",\"productType\":\"{productType}\" }}";
    List<ProductInfoModel> productInfoModelList = new List<ProductInfoModel>();
    try
    {
      XDocument secured = PurchaseBase.SendToSecured(address, body);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RetrieveProductInfoList",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = secured.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      foreach (XElement descendant in secured.Descendants((XName) "APIProductInfo"))
        productInfoModelList.Add(new ProductInfoModel()
        {
          Name = descendant.Attribute((XName) "Name").Value,
          DisplayName = descendant.Attribute((XName) "DisplayName").Value,
          Description = descendant.Attribute((XName) "Description").Value,
          Price = Convert.ToDouble(descendant.Attribute((XName) "Price").Value),
          Discount = Convert.ToDouble(descendant.Attribute((XName) "Discount").Value),
          SKU = descendant.Attribute((XName) "SKU").Value,
          ProductType = productType
        });
    }
    catch (Exception ex)
    {
      ex.Log("Retail.RetrieveProductInfoList  Message: ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RetrieveProductInfoList",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {address}] {body}",
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return productInfoModelList;
  }

  [AuthorizeDefault]
  private static void CreateNetsuiteCustomerSession(string sessionID, long userProfileID)
  {
    DateTime utcNow = DateTime.UtcNow;
    string address = "";
    string body = "";
    string str = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
    try
    {
      address = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/CreateNetsuiteCustomerSession/{0}?", (object) OverviewControllerBase.GetTempAuthToken());
      body = $"{{\"sessionID\":\"{sessionID}\",\"userProfileID\":\"{userProfileID.ToString()}\" }}";
      XDocument secured = PurchaseBase.SendToSecured(address, body);
      if (!flag)
        return;
      TimeSpan timeSpan = DateTime.UtcNow - utcNow;
      new MeaApiLog()
      {
        MethodName = "MEA-CreateNetsuiteCustomerSession",
        MachineName = Environment.MachineName,
        RequestBody = $"[URL: {address}] {body}",
        ResponseBody = secured.ToString(),
        CreateDate = utcNow,
        SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
        IsException = false
      }.Save();
    }
    catch (Exception ex)
    {
      ex.Log($"Retail.CreateNetsuiteCustomerSession[SessionID: {sessionID}][UserProfileID: {userProfileID.ToString()}]  Message: ");
      if (!flag)
        return;
      TimeSpan timeSpan = DateTime.UtcNow - utcNow;
      new MeaApiLog()
      {
        MethodName = "MEA-CreateNetsuiteCustomerSession",
        MachineName = Environment.MachineName,
        RequestBody = $"[URL: {address}] {body}",
        ResponseBody = ex.Message,
        CreateDate = utcNow,
        SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
        IsException = true
      }.Save();
    }
  }

  [AuthorizeDefault]
  public ActionResult RapidCalculator(long id)
  {
    if (MonnitSession.CurrentCustomer.AccountID == id)
      return (ActionResult) this.View((object) MonnitSession.CurrentCustomer.Account);
    if (MonnitSession.CustomerCan("Navigation_View_Administration"))
    {
      Account model = Account.Load(id);
      if (model != null && MonnitSession.IsAuthorizedForAccount(model.AccountID))
        return (ActionResult) this.View((object) model);
    }
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public ActionResult SensorPrints(long id)
  {
    Account account = Account.Load(id);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Retail",
        ErrorTranslateTag = "Retail/PaymentOption|",
        ErrorTitle = "Unauthorized access to billing pages",
        ErrorMessage = "You do not have permission to access this page."
      });
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__45.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__45.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "filterCount", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = RetailController.\u003C\u003Eo__45.\u003C\u003Ep__0.Target((CallSite) RetailController.\u003C\u003Eo__45.\u003C\u003Ep__0, this.ViewBag, "");
    if (MonnitSession.CurrentCustomer.AccountID == id)
    {
      RetailController.CreateNetsuiteCustomerSession(MonnitSession.Session.SessionID, account.StoreUserID);
      this.Session["PurchaseLinkStoreModel"] = (object) RetailController.CreatePurchaseLinkStoreModel_WithPaymentAndProductList($"SensorPrints[ID: {id.ToString()}]", "SensorPrintCredit", MonnitSession.CurrentCustomer.Account);
      return (ActionResult) this.View((object) MonnitSession.CurrentCustomer.Account);
    }
    if (!MonnitSession.CustomerCan("Navigation_View_Administration"))
      return (ActionResult) this.View();
    RetailController.CreateNetsuiteCustomerSession(MonnitSession.Session.SessionID, account.StoreUserID);
    this.Session["PurchaseLinkStoreModel"] = (object) RetailController.CreatePurchaseLinkStoreModel_WithPaymentAndProductList($"SensorPrints[ID: {id.ToString()}]", "SensorPrintCredit", account);
    return (ActionResult) this.View((object) account);
  }

  public ActionResult RetailIndex() => (ActionResult) this.View();

  [AuthorizeDefault]
  public ActionResult SensorPrintSensorList(long id)
  {
    Account acct = Account.Load(id);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct) || !MonnitSession.CustomerCan("Can_Access_Billing") || !MonnitSession.CustomerCan("Sensor_Edit"))
      return (ActionResult) this.Content("Unauthorized");
    List<Sensor> list = SensorControllerBase.GetAccountsSensorList(MonnitSession.CurrentCustomer.AccountID, out int _).Where<Sensor>((Func<Sensor, bool>) (s => new Version(s.FirmwareVersion) >= new Version("25.45.0.0"))).ToList<Sensor>();
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__47.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__47.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "filterCount", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = RetailController.\u003C\u003Eo__47.\u003C\u003Ep__0.Target((CallSite) RetailController.\u003C\u003Eo__47.\u003C\u003Ep__0, this.ViewBag, list.Count<Sensor>());
    return (ActionResult) this.PartialView("_SensorListDetails", (object) list);
  }

  [AuthorizeDefault]
  public ActionResult GatewayUnlock(long id)
  {
    Account account = Account.Load(id);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Retail",
        ErrorTranslateTag = "Retail/PaymentOption|",
        ErrorTitle = "Unauthorized access to billing pages",
        ErrorMessage = "You do not have permission to access this page."
      });
    int totalGateways;
    List<Gateway> gatewaysForUnlockList = this.GetGatewaysForUnlockList(id, false, out totalGateways);
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__48.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__48.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<Gateway>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "GatewayList", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = RetailController.\u003C\u003Eo__48.\u003C\u003Ep__0.Target((CallSite) RetailController.\u003C\u003Eo__48.\u003C\u003Ep__0, this.ViewBag, gatewaysForUnlockList);
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__48.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__48.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalGatewayUnlocks", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = RetailController.\u003C\u003Eo__48.\u003C\u003Ep__1.Target((CallSite) RetailController.\u003C\u003Eo__48.\u003C\u003Ep__1, this.ViewBag, totalGateways);
    if (MonnitSession.CurrentCustomer.AccountID == id)
    {
      RetailController.CreateNetsuiteCustomerSession(MonnitSession.Session.SessionID, account.StoreUserID);
      this.Session["PurchaseLinkStoreModel"] = (object) RetailController.CreatePurchaseLinkStoreModel_WithPaymentAndProductList($"GatewayUnlock[ID: {id.ToString()}]", "GatewayUnlockCredit", MonnitSession.CurrentCustomer.Account);
      return (ActionResult) this.View((object) MonnitSession.CurrentCustomer.Account);
    }
    if (!MonnitSession.CustomerCan("Navigation_View_Administration"))
      return (ActionResult) this.View();
    RetailController.CreateNetsuiteCustomerSession(MonnitSession.Session.SessionID, account.StoreUserID);
    this.Session["PurchaseLinkStoreModel"] = (object) RetailController.CreatePurchaseLinkStoreModel_WithPaymentAndProductList($"GatewayUnlock[ID: {id.ToString()}]", "GatewayUnlockCredit", account);
    return (ActionResult) this.View((object) account);
  }

  [AuthorizeDefault]
  public ActionResult GatewayUnlockGps(long id)
  {
    Account account = Account.Load(id);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CustomerCan("Can_Access_Billing"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Retail",
        ErrorTranslateTag = "Retail/PaymentOption|",
        ErrorTitle = "Unauthorized access to billing pages",
        ErrorMessage = "You do not have permission to access this page."
      });
    int totalGateways;
    List<Gateway> gatewaysForUnlockList = this.GetGatewaysForUnlockList(id, true, out totalGateways);
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__49.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__49.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<Gateway>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "GatewayList", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = RetailController.\u003C\u003Eo__49.\u003C\u003Ep__0.Target((CallSite) RetailController.\u003C\u003Eo__49.\u003C\u003Ep__0, this.ViewBag, gatewaysForUnlockList);
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__49.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__49.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalGatewayUnlocks", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = RetailController.\u003C\u003Eo__49.\u003C\u003Ep__1.Target((CallSite) RetailController.\u003C\u003Eo__49.\u003C\u003Ep__1, this.ViewBag, totalGateways);
    MonnitSession.GatewayListFilters.GatewayTypeID = long.MinValue;
    if (MonnitSession.CurrentCustomer.AccountID == id)
    {
      RetailController.CreateNetsuiteCustomerSession(MonnitSession.Session.SessionID, account.StoreUserID);
      this.Session["PurchaseLinkStoreModel"] = (object) RetailController.CreatePurchaseLinkStoreModel_WithPaymentAndProductList($"GatewayUnlockGps[ID: {id.ToString()}]", "GatewayUnlockGpsCredit", MonnitSession.CurrentCustomer.Account);
      return (ActionResult) this.View((object) MonnitSession.CurrentCustomer.Account);
    }
    if (!MonnitSession.CustomerCan("Navigation_View_Administration"))
      return (ActionResult) this.View();
    RetailController.CreateNetsuiteCustomerSession(MonnitSession.Session.SessionID, account.StoreUserID);
    this.Session["PurchaseLinkStoreModel"] = (object) RetailController.CreatePurchaseLinkStoreModel_WithPaymentAndProductList($"GatewayUnlockGps[ID: {id.ToString()}]", "GatewayUnlockGpsCredit", account);
    return (ActionResult) this.View((object) account);
  }

  [AuthorizeDefault]
  public ActionResult GatewayUnlockList(long id)
  {
    Account acct = Account.Load(id);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct) || !MonnitSession.CustomerCan("Can_Access_Billing") || !MonnitSession.CustomerCan("Sensor_Edit"))
      return (ActionResult) this.Content("Unauthorized");
    int totalGateways;
    List<Gateway> gatewaysForUnlockList = this.GetGatewaysForUnlockList(id, false, out totalGateways);
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__50.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__50.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalGatewayUnlocks", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = RetailController.\u003C\u003Eo__50.\u003C\u003Ep__0.Target((CallSite) RetailController.\u003C\u003Eo__50.\u003C\u003Ep__0, this.ViewBag, totalGateways);
    return (ActionResult) this.PartialView("_GatewayListDetails", (object) gatewaysForUnlockList);
  }

  [AuthorizeDefault]
  public ActionResult GatewayUnlockGpsList(long id)
  {
    Account acct = Account.Load(id);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct) || !MonnitSession.CustomerCan("Can_Access_Billing") || !MonnitSession.CustomerCan("Sensor_Edit"))
      return (ActionResult) this.Content("Unauthorized");
    int totalGateways;
    List<Gateway> gatewaysForUnlockList = this.GetGatewaysForUnlockList(id, true, out totalGateways);
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__51.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__51.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalGatewayUnlocks", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = RetailController.\u003C\u003Eo__51.\u003C\u003Ep__0.Target((CallSite) RetailController.\u003C\u003Eo__51.\u003C\u003Ep__0, this.ViewBag, totalGateways);
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__51.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__51.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "GatewayListType", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = RetailController.\u003C\u003Eo__51.\u003C\u003Ep__1.Target((CallSite) RetailController.\u003C\u003Eo__51.\u003C\u003Ep__1, this.ViewBag, "GatewayUnlockGps");
    // ISSUE: reference to a compiler-generated field
    if (RetailController.\u003C\u003Eo__51.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RetailController.\u003C\u003Eo__51.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ClickSvgIcon", typeof (RetailController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = RetailController.\u003C\u003Eo__51.\u003C\u003Ep__2.Target((CallSite) RetailController.\u003C\u003Eo__51.\u003C\u003Ep__2, this.ViewBag, "gps-pin");
    return (ActionResult) this.PartialView("_GatewayListDetails", (object) gatewaysForUnlockList);
  }

  private List<Gateway> GetGatewaysForUnlockList(
    long AccountID,
    bool unlockGps,
    out int totalGateways)
  {
    totalGateways = 0;
    long CSNetID = MonnitSession.GatewayListFilters.SensorListFiltersCSNetID;
    long GatewayTypeID = MonnitSession.GatewayListFilters.GatewayTypeID;
    int Status = MonnitSession.GatewayListFilters.Status;
    string Name = MonnitSession.GatewayListFilters.Name;
    if (MonnitSession.CurrentCustomer == null)
      return new List<Gateway>();
    IEnumerable<Gateway> source1 = (unlockGps ? (IEnumerable<Gateway>) Gateway.LoadByAccountIDForUnlockGpsList(AccountID) : (IEnumerable<Gateway>) CSNetControllerBase.GetGatewayList(out totalGateways)).Where<Gateway>((Func<Gateway, bool>) (g => (g.CSNetID == CSNetID || CSNetID == long.MinValue) && MonnitSession.CurrentCustomer.CanSeeNetwork(g.CSNetID)));
    totalGateways = source1.Count<Gateway>();
    IEnumerable<Gateway> source2 = (IEnumerable<Gateway>) source1.Where<Gateway>((Func<Gateway, bool>) (g =>
    {
      if (g.Status.ToInt() != Status && Status != int.MinValue || g.GatewayTypeID != GatewayTypeID && GatewayTypeID != long.MinValue)
        return false;
      return g.Name.ToLower().Contains(Name.ToLower()) || Name == "";
    })).OrderBy<Gateway, string>((Func<Gateway, string>) (g => g.Name.Trim()));
    switch (MonnitSession.GatewayListSort.Name)
    {
      case "Type":
        source2 = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Gateway>) source2.OrderBy<Gateway, long>((Func<Gateway, long>) (cs => cs.GatewayTypeID)) : (IEnumerable<Gateway>) source2.OrderByDescending<Gateway, long>((Func<Gateway, long>) (t => t.GatewayTypeID));
        break;
      case "Gateway Name":
        source2 = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Gateway>) source2.OrderBy<Gateway, string>((Func<Gateway, string>) (g => g.Name)) : (IEnumerable<Gateway>) source2.OrderByDescending<Gateway, string>((Func<Gateway, string>) (g => g.Name));
        break;
      case "Last Check In":
        source2 = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Gateway>) source2.OrderBy<Gateway, DateTime>((Func<Gateway, DateTime>) (b =>
        {
          DateTime communicationDate = b.LastCommunicationDate;
          return b.LastCommunicationDate;
        })) : (IEnumerable<Gateway>) source2.OrderByDescending<Gateway, DateTime>((Func<Gateway, DateTime>) (b =>
        {
          DateTime communicationDate = b.LastCommunicationDate;
          return b.LastCommunicationDate;
        }));
        break;
    }
    return source2.ToList<Gateway>();
  }

  [AuthorizeDefault]
  public ActionResult ApplyGatewayUnlock(long id, string deviceIDs)
  {
    Account acct = Account.Load(id);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct) || !MonnitSession.CustomerCan("Can_Access_Billing") || !MonnitSession.CustomerCan("Sensor_Edit"))
      return (ActionResult) this.Content("Unauthorized");
    string[] strArray = deviceIDs.Split(new char[1]{ ',' }, StringSplitOptions.RemoveEmptyEntries);
    if (Credit.LoadRemainingCreditsForAccount(acct.AccountID, eCreditClassification.GatewayUnlock) < strArray.Length)
      return (ActionResult) this.Content("Insufficient Credits");
    int creditsToCharge = 0;
    foreach (string o in strArray)
    {
      try
      {
        Gateway DBObject = Gateway.Load(o.ToLong());
        if (DBObject != null)
        {
          DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, acct.AccountID, "Applied Unlock to gateway");
          DBObject.IsUnlocked = true;
          DBObject.SendUnlockRequest = true;
          DBObject.Save();
          ++creditsToCharge;
        }
      }
      catch (Exception ex)
      {
        ex.Log($"Retail.ApplyGatewayUnlock Failed, GatewayID: {o} Message: ");
      }
    }
    RetailController.ChargeCreditType(acct.AccountID, creditsToCharge, eCreditClassification.GatewayUnlock);
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult ApplyGatewayUnlockGps(long id, string deviceIDs)
  {
    Account acct = Account.Load(id);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct) || !MonnitSession.CustomerCan("Can_Access_Billing") || !MonnitSession.CustomerCan("Sensor_Edit"))
      return (ActionResult) this.Content("Unauthorized");
    string[] strArray = deviceIDs.Split(new char[1]{ ',' }, StringSplitOptions.RemoveEmptyEntries);
    if (Credit.LoadRemainingCreditsForAccount(acct.AccountID, eCreditClassification.GatewayUnlockGps) < strArray.Length)
      return (ActionResult) this.Content("Insufficient Credits");
    int creditsToCharge = 0;
    foreach (string o in strArray)
    {
      try
      {
        Gateway DBObject = Gateway.Load(o.ToLong());
        if (DBObject != null)
        {
          DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, acct.AccountID, "Applied Unlocked Gateway GPS");
          DBObject.SendGPSUnlockRequest = true;
          DBObject.IsGPSUnlocked = true;
          DBObject.Save();
          ++creditsToCharge;
        }
      }
      catch (Exception ex)
      {
        ex.Log($"Retail.ApplyGatewayUnlockGps Failed, GatewayID: {o} Message: ");
      }
    }
    RetailController.ChargeCreditType(acct.AccountID, creditsToCharge, eCreditClassification.GatewayUnlockGps);
    return (ActionResult) this.Content("Success");
  }

  public static bool ChargeCreditType(
    long accountID,
    int creditsToCharge,
    eCreditClassification eCreditClass)
  {
    bool flag = false;
    List<Credit> creditList = new List<Credit>();
    foreach (Credit credit in Credit.LoadAvailable(accountID, eCreditClass))
    {
      if (credit.RemainingCredits >= creditsToCharge)
      {
        creditList.Add(credit);
        credit.UsedCredits += creditsToCharge;
        flag = true;
        break;
      }
      creditList.Add(credit);
      int remainingCredits = credit.RemainingCredits;
      credit.UsedCredits += remainingCredits;
      creditsToCharge -= remainingCredits;
    }
    if (flag)
    {
      foreach (Credit credit in creditList)
      {
        if (credit.RemainingCredits <= 0)
          credit.ExhaustedDate = DateTime.UtcNow;
        credit.Save();
      }
    }
    return flag;
  }
}
