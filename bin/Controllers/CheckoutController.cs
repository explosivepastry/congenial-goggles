// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.CheckoutController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using AuthorizeNet;
using iMonnit.ControllerBase;
using iMonnit.Models;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;

#nullable disable
namespace iMonnit.Controllers;

public class CheckoutController : ThemeController
{
  [Authorize]
  public ActionResult Index()
  {
    return (ActionResult) this.View((object) OnlineOrder.LoadCartByAccount(MonnitSession.CurrentCustomer.AccountID));
  }

  [Authorize]
  public ActionResult Summary()
  {
    return (ActionResult) this.View((object) OnlineOrder.LoadCartByAccount(MonnitSession.CurrentCustomer.AccountID));
  }

  [Authorize]
  public ActionResult ItemList()
  {
    return (ActionResult) this.View((object) OnlineOrder.LoadCartByAccount(MonnitSession.CurrentCustomer.AccountID));
  }

  [Authorize]
  public ActionResult Update(long id, int qty)
  {
    OrderItem orderItem = OrderItem.Load(id);
    if (orderItem == null)
      return (ActionResult) this.Content("Unable to update quantity, please refresh your cart.");
    orderItem.ItemQty = qty;
    orderItem.Save();
    OnlineOrder.RefreshItems(orderItem.OrderID);
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  public ActionResult Remove(long id)
  {
    OrderItem orderItem = OrderItem.Load(id);
    if (orderItem == null)
      return (ActionResult) this.Content("Unable to remove item, please refresh your cart.");
    orderItem.Delete();
    OnlineOrder.RefreshItems(orderItem.OrderID);
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  public ActionResult Shipping()
  {
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "PaymentInfo"
    });
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Shipping(FormCollection collection)
  {
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "PaymentInfo"
    });
  }

  [Authorize]
  public ActionResult PaymentInfo()
  {
    this.ViewData["Order"] = (object) OnlineOrder.LoadCartByAccount(MonnitSession.CurrentCustomer.AccountID);
    return (ActionResult) this.View((object) new PaymentInfoModel());
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult PaymentInfo(PaymentInfoModel model)
  {
    ServicePointManager.Expect100Continue = true;
    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
    OnlineOrder onlineOrder = OnlineOrder.LoadCartByAccount(MonnitSession.CurrentCustomer.AccountID);
    if (onlineOrder.OrderTotal <= 0.0)
    {
      CheckoutController.ProcessOnlineDelivery(onlineOrder);
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "ThankYou"
      });
    }
    if (this.ModelState.IsValid)
    {
      string cardNumber = model.CardNumber;
      string expirationMonthAndYear = $"{model.ExpMonth}{model.ExpYear.ToString().Substring(2)}";
      Decimal orderTotal = (Decimal) onlineOrder.OrderTotal;
      string description = "Online purchase";
      bool flag = false;
      AuthorizationRequest request1 = new AuthorizationRequest(cardNumber, expirationMonthAndYear, orderTotal, description, false);
      request1.InvoiceNum = onlineOrder.OrderID.ToString();
      request1.FirstName = model.CustomerName;
      request1.CardCode = model.CardCode;
      try
      {
        Account account = MonnitSession.CurrentCustomer.Account;
        request1.Company = account.CompanyName;
        request1.Email = MonnitSession.CurrentCustomer.NotificationEmail;
        request1.Phone = MonnitSession.CurrentCustomer.NotificationPhone;
        request1.ShipToCompany = account.AccountNumber;
        request1.ShipToFirstName = MonnitSession.CurrentCustomer.FullName;
      }
      catch
      {
      }
      AuthorizeNet.Gateway gateway = new AuthorizeNet.Gateway(ConfigData.AppSettings("AuthorizeID"), ConfigData.AppSettings("AuthorizeKey"), ConfigData.AppSettings("AuthorizeTestMode").ToBool());
      IGatewayResponse gatewayResponse1 = gateway.Send((IGatewayRequest) request1);
      Payment payment1 = new Payment();
      payment1.OrderID = onlineOrder.OrderID;
      payment1.PaymentDate = DateTime.UtcNow;
      payment1.PaymentType = ePaymentType.Charge;
      payment1.Message = gatewayResponse1.Message;
      payment1.CardNumber = gatewayResponse1.CardNumber;
      payment1.PaymentAmount = gatewayResponse1.Amount.ToDouble();
      payment1.Approved = gatewayResponse1.Approved;
      payment1.ResponseCode = gatewayResponse1.ResponseCode;
      payment1.TransactionID = gatewayResponse1.TransactionID;
      payment1.AuthCode = gatewayResponse1.AuthorizationCode;
      payment1.Save();
      List<Payment> list = Payment.LoadByOrderID(onlineOrder.OrderID).Where<Payment>((Func<Payment, bool>) (ap => ap.Approved)).ToList<Payment>();
      if (onlineOrder.OrderTotal <= list.Sum<Payment>((Func<Payment, double>) (ap => ap.PaymentAmount)) && !flag)
      {
        foreach (Payment payment2 in list)
        {
          CaptureRequest request2 = new CaptureRequest((Decimal) payment2.PaymentAmount, payment2.TransactionID, payment2.AuthCode);
          IGatewayResponse gatewayResponse2 = gateway.Send((IGatewayRequest) request2);
          payment2.Captured = gatewayResponse2.Approved;
          payment2.CapturedResponseCode = gatewayResponse2.ResponseCode;
          payment2.Save();
          this.SendReceipt(this.ControllerContext, payment2, onlineOrder);
        }
        onlineOrder.Status = eOrderStatus.Paid;
        onlineOrder.Save();
        CheckoutController.ProcessOnlineDelivery(onlineOrder);
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "ThankYou",
          controller = "Overview",
          id = onlineOrder.OrderID
        });
      }
      if (onlineOrder.Status == eOrderStatus.Cart && list.Count > 0)
      {
        onlineOrder.Status = eOrderStatus.Pending;
        onlineOrder.Save();
      }
      this.ModelState.AddModelError("", payment1.Message);
    }
    this.ModelState.AddModelError("", "All fields required.");
    this.ViewData["Order"] = (object) OnlineOrder.LoadCartByAccount(MonnitSession.CurrentCustomer.AccountID);
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult ThankYou() => (ActionResult) this.View();

  [Authorize]
  private void SendReceipt(ControllerContext context, Payment payment, OnlineOrder order)
  {
    EmailTemplate emailTemplate = EmailTemplate.LoadBest(MonnitSession.CurrentCustomer.Account, eEmailTemplateFlag.Generic);
    if (emailTemplate == null)
    {
      emailTemplate = new EmailTemplate();
      emailTemplate.Subject = "Purchase Receipt";
      emailTemplate.Template = "{Content}";
    }
    else
      emailTemplate.Subject = "Purchase Receipt";
    string Content = $"Thank you for your recent order!<br/> Payment Total: {payment.PaymentAmount:C}<br/> Order confirmation number: IP{payment.PaymentID}";
    using (MailMessage mail = new MailMessage())
    {
      mail.IsBodyHtml = true;
      using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, MonnitSession.CurrentCustomer.Account))
      {
        try
        {
          mail.To.Add(MonnitSession.CurrentCustomer.NotificationEmail);
          mail.Bcc.Add(new MailAddress(ConfigData.AppSettings("NewAccountNotificationEmail"), "Subscription Extended"));
        }
        catch (Exception ex)
        {
          mail.To.Add(new MailAddress(ConfigData.AppSettings("NewAccountNotificationEmail"), "Subscription Extended"));
          ex.Log("CheckoutController.SendReceipt.InvalidEmailAddress");
        }
        mail.Subject = emailTemplate.Subject;
        mail.Body = emailTemplate.MailMerge(Content, mail.To[0].Address);
        try
        {
          MonnitUtil.SendMail(mail, smtpClient);
        }
        catch (Exception ex)
        {
          ex.Log("CheckoutController.SendReceipt.SendMailFailed");
        }
      }
    }
  }

  [Authorize]
  protected string RenderPartialViewToString(string viewName, object model)
  {
    try
    {
      this.ViewData.Model = model;
      using (StringWriter writer = new StringWriter())
      {
        ViewEngineResult partialView = ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);
        ViewContext viewContext = new ViewContext(this.ControllerContext, partialView.View, this.ViewData, this.TempData, (TextWriter) writer);
        partialView.View.Render(viewContext, (TextWriter) writer);
        return writer.ToString();
      }
    }
    catch (Exception ex)
    {
      ex.Log("CheckoutController.RenderPartialViewToString ");
    }
    return new Exception("CheckoutController.RenderPartialViewToString").ToString();
  }

  [Authorize]
  private static void ProcessOnlineDelivery(OnlineOrder cart)
  {
    foreach (OrderItem orderItem in cart.Items)
    {
      if (orderItem.Product.PremierMaxSensorCount > 0)
      {
        int num = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Count<Sensor>();
        DateTime date = MonnitSession.CurrentCustomer.Account.PremiumValidUntil;
        if (orderItem.Product.PremierMaxSensorCount >= num)
        {
          date = !(date > DateTime.UtcNow) ? DateTime.UtcNow.AddYears(orderItem.ItemQty) : date.AddYears(orderItem.ItemQty);
        }
        else
        {
          int months = orderItem.Product.PremierMaxSensorCount * 10 * orderItem.ItemQty / num;
          date = !(date > DateTime.UtcNow) ? DateTime.UtcNow.AddMonths(months) : date.AddMonths(months);
        }
        AccountSubscription.UpdatePremiumDate(date, MonnitSession.CurrentCustomer.Account, MonnitSession.CurrentCustomer, "iMonnit Checkout");
      }
      if (orderItem.Product.NotificationCredits > 0)
      {
        long num = long.MinValue;
        foreach (NotificationCreditType notificationCreditType in NotificationCreditType.LoadAll())
        {
          if (notificationCreditType.Name.ToLower() == "purchased")
          {
            num = notificationCreditType.NotificationCreditTypeID;
            break;
          }
        }
        if (num > 0L)
          new NotificationCredit()
          {
            NotificationCreditTypeID = num,
            AccountID = cart.AccountID,
            ActivatedByCustomerID = MonnitSession.CurrentCustomer.CustomerID,
            ActivationDate = DateTime.UtcNow.AddMinutes(-10.0),
            ActivationCode = "",
            ActivatedCredits = (orderItem.Product.NotificationCredits * orderItem.ItemQty),
            ExpirationDate = new DateTime(2099, 1, 1)
          }.Save();
        else
          new Exception("Notification credit type not found: purchased").Log("Checkout.ProcessOnlineDelivery ");
      }
      if (!orderItem.Product.ExpressDownload)
        ;
    }
  }

  [Authorize]
  public ActionResult UpdateSubscription()
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
      action = ""
    });
  }

  [Authorize]
  public ActionResult PurchaseNotificationCredits(int count)
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

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ActivateNotificationCredits(long id, string activationCode)
  {
    Account account = Account.Load(id);
    if (account == null)
      return (ActionResult) this.Content("Not Authorized.");
    if (account.AccountID != MonnitSession.CurrentCustomer.AccountID && account.RetailAccountID != MonnitSession.CurrentCustomer.AccountID && !MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.Content("Not Authorized.");
    try
    {
      string s = PurchaseBase.RetreiveActivationKey(activationCode, account.AccountID.ToString());
      if (s.Equals("Activation Failed"))
        return (ActionResult) this.Content("Activation Failed");
      foreach (NotificationCredit notificationCredit in NotificationCredit.LoadByAccount(account.AccountID))
      {
        if (notificationCredit.ActivationCode == activationCode)
          return (ActionResult) this.Content("Redemption Code Previously Used");
      }
      string[] strArray = Encoding.UTF8.GetString(Convert.FromBase64String(s)).Split('|')[0].Split('_');
      string lower = strArray[0].ToLower();
      foreach (NotificationCreditType notificationCreditType in NotificationCreditType.LoadAll())
      {
        if (lower == notificationCreditType.Name.ToLower())
        {
          int num = strArray[1].ToInt();
          int months = strArray.Length > 2 ? strArray[2].ToInt() : 0;
          if (num > 0)
          {
            NotificationCredit notificationCredit1 = new NotificationCredit();
            notificationCredit1.NotificationCreditTypeID = notificationCreditType.NotificationCreditTypeID;
            notificationCredit1.AccountID = account.AccountID;
            notificationCredit1.ActivatedByCustomerID = MonnitSession.CurrentCustomer.CustomerID;
            NotificationCredit notificationCredit2 = notificationCredit1;
            DateTime utcNow = DateTime.UtcNow;
            DateTime dateTime1 = utcNow.AddMinutes(-10.0);
            notificationCredit2.ActivationDate = dateTime1;
            notificationCredit1.ActivationCode = activationCode;
            notificationCredit1.ActivatedCredits = num;
            if (months > 0)
            {
              NotificationCredit notificationCredit3 = notificationCredit1;
              utcNow = DateTime.UtcNow;
              DateTime dateTime2 = utcNow.AddMonths(months);
              notificationCredit3.ExpirationDate = dateTime2;
            }
            else
              notificationCredit1.ExpirationDate = new DateTime(2099, 1, 1);
            notificationCredit1.Save();
          }
        }
      }
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log($"CheckoutController.ActivateNotificationCredits AccountID: {id}, ActivationCode: {activationCode}");
      return (ActionResult) this.Content("Activation Failed");
    }
  }

  [Authorize]
  [Authorize]
  public static string RetreiveActivationKey(
    string activationCode,
    string identifier,
    string keyType,
    string activeKeyType,
    DateTime currentEndDate)
  {
    string str1 = "";
    DateTime utcNow = DateTime.UtcNow;
    string uri = "";
    string str2 = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str2) && bool.Parse(str2);
    try
    {
      uri = string.Format(ConfigData.FindValue("MEA_API_Location") + "/xml/RetreiveActivationKey?key={0}&activeKeyType={1}&currentEndDate={2}&advancedFailResponse={3}", (object) Convert.ToBase64String(Encoding.UTF8.GetBytes($"{activationCode}|{identifier}|{keyType}")), (object) activeKeyType, (object) currentEndDate, (object) true);
      XDocument xdocument = XDocument.Load(uri);
      str1 = xdocument.Descendants((XName) "Result").Single<XElement>().Value;
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RetreiveActivationKey2",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = xdocument.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
    }
    catch (Exception ex)
    {
      ex.Log($"CheckoutController.RetreiveActivationKey2[Code: {activationCode}][ID: {identifier}][Type: {keyType}][ActiveKeyType: {activeKeyType}][EndDate: {currentEndDate.ToShortDateString()}] ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RetreiveActivationKey2",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return str1;
  }

  [Authorize]
  public static string AdminUpdateAccountSubscription(
    long accountID,
    string date,
    string subscriptionType,
    Guid? subscriptionCode)
  {
    string str1 = "";
    DateTime utcNow = DateTime.UtcNow;
    string uri = "";
    string str2 = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str2) && bool.Parse(str2);
    try
    {
      string str3 = "&SubscriptionCode=" + subscriptionCode.ToString();
      int num;
      if (subscriptionCode.HasValue)
      {
        Guid? nullable = subscriptionCode;
        Guid empty = Guid.Empty;
        num = nullable.HasValue ? (nullable.GetValueOrDefault() == empty ? 1 : 0) : 0;
      }
      else
        num = 1;
      if (num != 0)
        str3 = "";
      uri = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/AdminUpdateAccountSubscription/{4}?accountID={0}&Date={1}&SubscriptionType={2}{3}", (object) accountID, (object) date, (object) subscriptionType, (object) str3, (object) OverviewControllerBase.GetTempAuthToken());
      XDocument xdocument = XDocument.Load(uri);
      str1 = xdocument.Descendants((XName) "Result").Single<XElement>().Value;
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-AdminUpdateAccountSubscription",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = xdocument.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
    }
    catch (Exception ex)
    {
      ex.Log($"CheckoutController.AdminUpdateAccountSubscription[AccountID: {accountID.ToString()}] ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-AdminUpdateAccountSubscription",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return str1;
  }
}
