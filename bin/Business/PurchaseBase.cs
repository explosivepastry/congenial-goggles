// Decompiled with JetBrains decompiler
// Type: Monnit.PurchaseBase
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml.Linq;

#nullable disable
namespace Monnit;

public static class PurchaseBase
{
  public static ActivationCode BasePurchase(
    long id,
    DateTime startTime,
    string IPAddress,
    string storeLinkGuid,
    long storeUserID,
    Customer currentUser,
    Account account,
    long profileID,
    string sku,
    double tax,
    int qty,
    string toBeEmailed,
    string sessionID,
    AccountSubscription sub,
    double prorateAmount = 0.0)
  {
    string MeaApiLogFlag = ConfigData.AppSettings("MeaApiLogging");
    bool MeaApiLogging = !string.IsNullOrWhiteSpace(MeaApiLogFlag) && bool.Parse(MeaApiLogFlag);
    string requestURL = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/iMonnitPurchaseProduct/{0}", (object) PurchaseBase.GetTempAuthToken());
    string requestBody = $"{{\"storeLinkGuid\":\"{storeLinkGuid}\",\"userID\":\"{storeUserID.ToString()}\",\"userAddressID\":\"{profileID.ToString()}\",\"prorate\":\"{prorateAmount.ToString()}\",\"tax\":\"{tax.ToString()}\",\"sessionID\":\"{sessionID}\",\"qty\":\"{qty.ToString()}\",\"SKU\":\"{sku}\",\"toBeEmailed\":\"{toBeEmailed}\" }}";
    return PurchaseBase.BasePurchase(profileID, sku, tax, qty, toBeEmailed, account, startTime, requestURL, requestBody, storeLinkGuid, storeUserID, MeaApiLogFlag, MeaApiLogging, currentUser, sub);
  }

  public static ActivationCode BasePurchase(
    long profileID,
    string sku,
    double tax,
    int qty,
    string toBeEmailed,
    Account account,
    DateTime startTime,
    string requestURL,
    string requestBody,
    string storeLinkGuid,
    long storeUserID,
    string MeaApiLogFlag,
    bool MeaApiLogging,
    Customer currentUser,
    AccountSubscription sub)
  {
    ActivationCode activationCode1 = new ActivationCode();
    try
    {
      XDocument secured = PurchaseBase.SendToSecured(requestURL, requestBody);
      if (MeaApiLogging)
      {
        TimeSpan timeSpan = DateTime.UtcNow - startTime;
        new MeaApiLog()
        {
          MethodName = "MEA-ProcessPurchase",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {requestURL}] {requestBody}",
          ResponseBody = secured.ToString(),
          CreateDate = startTime,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      string subscriptionActivationCode = secured.Descendants((XName) "Result").Single<XElement>().Value.ToString();
      string[] strArray1 = subscriptionActivationCode.Split('~');
      string o = strArray1 == null || strArray1.Length == 0 ? "" : strArray1[0];
      if (subscriptionActivationCode.Contains("Failed") || o.ToGuid() == Guid.Empty)
      {
        activationCode1.Status = subscriptionActivationCode;
        return activationCode1;
      }
      activationCode1.Code = subscriptionActivationCode;
      string str1 = "";
      if (sku.Contains("MNW-HX"))
      {
        for (int index = 0; index < strArray1.Length; ++index)
        {
          string activationCode2 = strArray1[index];
          str1 = str1 + PurchaseBase.ProcessMessageCreditKey(activationCode2, account, currentUser) + (index == strArray1.Length - 1 ? "" : "~");
        }
      }
      else if (sku.Contains("MNW-SP"))
      {
        for (int index = 0; index < strArray1.Length; ++index)
        {
          string activationCode3 = strArray1[index];
          str1 = str1 + PurchaseBase.ProcessSensorPrintCreditKey(activationCode3, account, currentUser) + (index == strArray1.Length - 1 ? "" : "~");
        }
      }
      else if (sku.Contains("MNW-NC"))
      {
        for (int index = 0; index < strArray1.Length; ++index)
        {
          string activationCode4 = strArray1[index];
          str1 = str1 + PurchaseBase.ProcessNotificationCreditKey(activationCode4, account, currentUser, sub) + (index == strArray1.Length - 1 ? "" : "~");
        }
      }
      else
      {
        switch (sku)
        {
          case "MNA-GW-UL":
            for (int index = 0; index < strArray1.Length; ++index)
            {
              string activationCode5 = strArray1[index];
              str1 = str1 + PurchaseBase.ProcessGatewayUnlockCreditKey(activationCode5, account, currentUser) + (index == strArray1.Length - 1 ? "" : "~");
            }
            break;
          case "MNA-GPS-UL":
            for (int index = 0; index < strArray1.Length; ++index)
            {
              string activationCode6 = strArray1[index];
              str1 = str1 + PurchaseBase.ProcessGatewayUnlockGpsCreditKey(activationCode6, account, currentUser) + (index == strArray1.Length - 1 ? "" : "~");
            }
            break;
          default:
            str1 = PurchaseBase.TryUpdateSubscription(account.AccountID, subscriptionActivationCode, currentUser, sub);
            if (str1.Contains("Success"))
            {
              string notificationEmail = currentUser.NotificationEmail;
              try
              {
                string subject = "iMonnit Renewal Confirmation";
                EmailTemplate emailTemplate = EmailTemplate.LoadBest(account, eEmailTemplateFlag.Generic);
                AccountTheme.Load(account.GetThemeID());
                if (emailTemplate == null)
                {
                  emailTemplate = new EmailTemplate();
                  emailTemplate.Subject = subject;
                  emailTemplate.Template = "<p>Thank you.  We appreciate your business.</p><p>{Content}</p>";
                }
                string[] strArray2 = str1.Split('|');
                string s = strArray2 == null || strArray2.Length <= 1 ? "" : strArray2[1];
                long ID = string.IsNullOrWhiteSpace(s) ? long.MinValue : long.Parse(s);
                AccountSubscription accountSubscription = ID > 0L ? AccountSubscription.Load(ID) : (AccountSubscription) null;
                string str2 = "";
                if (accountSubscription != null)
                  str2 = $"<p>The license renewed is for <b>{accountSubscription.AccountSubscriptionType.Name}</b> and will expire on <b>{accountSubscription.ExpirationDate.ToShortDateString()}.</b></p>";
                string body = "<p style=\"display:none;\">Thank you.  We appreciate your business.</p>" + emailTemplate.MailMerge($"<p>Thank you for renewing your subscription of iMonnit Premiere for <b>{account.AccountNumber}({account.AccountID.ToString()})</b>. Your payment will be processed today and a receipt will be sent to the email address you entered when the order was placed.</p>{str2}<p>If there's anything else we can assit with, please contact us at <a href=\"mailto:sales@monnit.com\">sales@monnit.com</a>.  For technical help, contact us at <a href=\"mailto:support@monnit.com\">support@monnit.com</a>.</p><br/> Sincerely,<br/> The Monnit Team", notificationEmail);
                MonnitUtil.SendMail(notificationEmail, subject, body, account);
              }
              catch (Exception ex)
              {
                ex.Log($"RetailController.ProcessPurchase[POST][PremiereRenewConfirmationEmail][AccountID: {account.AccountID.ToString()}][SKU: {sku}][ToEmail: {notificationEmail}] ");
              }
            }
            break;
        }
      }
      activationCode1.Status = str1;
      return activationCode1;
    }
    catch (Exception ex)
    {
      ex.Log($"ProcessPurchase Failed SKU: {sku} Message: ");
      if (MeaApiLogging)
      {
        TimeSpan timeSpan = DateTime.UtcNow - startTime;
        new MeaApiLog()
        {
          MethodName = "MEA-ProcessPurchase",
          MachineName = Environment.MachineName,
          RequestBody = $"[URL: {requestURL}] {requestBody}",
          ResponseBody = ex.Message,
          CreateDate = startTime,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
      activationCode1.Status = "Failed";
      return activationCode1;
    }
  }

  public static XDocument SendToSecured(string address, string body, int timeout = 30000)
  {
    try
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(address);
      httpWebRequest.Timeout = timeout;
      httpWebRequest.Method = "POST";
      httpWebRequest.ContentType = "application/json";
      byte[] bytes = new ASCIIEncoding().GetBytes(body);
      httpWebRequest.ContentLength = (long) bytes.Length;
      using (Stream requestStream = httpWebRequest.GetRequestStream())
      {
        requestStream.Write(bytes, 0, bytes.Length);
        requestStream.Close();
      }
      string text = "";
      using (HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse())
      {
        using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
        {
          text = streamReader.ReadToEnd();
          streamReader.Close();
        }
        response.Close();
      }
      return XDocument.Parse(text);
    }
    catch (Exception ex)
    {
      ex.Log("Retail.SendToSecured ");
      throw ex;
    }
  }

  public static string ProcessMessageCreditKey(string activationCode, Account acnt, Customer cust)
  {
    try
    {
      string s = PurchaseBase.RetreiveActivationKey(activationCode, acnt.AccountID.ToString());
      if (s.Equals("Activation Failed"))
        return "Activation Failed";
      foreach (Credit credit in Credit.LoadByAccount(acnt.AccountID))
      {
        if (credit.ActivationCode == activationCode)
          return "HX credit code previously used.";
      }
      int messageCreditsToAssign = Encoding.UTF8.GetString(Convert.FromBase64String(s)).Split('|')[0].Split('_')[1].ToInt();
      return PurchaseBase.AddMessageCredits(acnt, cust, messageCreditsToAssign, activationCode);
    }
    catch (Exception ex)
    {
      ex.Log($"RetailController.ProcessMessageCreditKey AccountID: {acnt.AccountID}, ActivationCode: {activationCode}");
      return "Activation Failed";
    }
  }

  public static string ProcessSensorPrintCreditKey(
    string activationCode,
    Account acnt,
    Customer cust)
  {
    try
    {
      string s = PurchaseBase.RetreiveActivationKey(activationCode, acnt.AccountID.ToString());
      if (s.Equals("Activation Failed"))
        return "Activation Failed";
      foreach (Credit credit in Credit.LoadByAccount(acnt.AccountID))
      {
        if (credit.ActivationCode == activationCode)
          return "Credit code previously used.";
      }
      int creditsToAssign = Encoding.UTF8.GetString(Convert.FromBase64String(s)).Split('|')[0].Split('_')[1].ToInt();
      return PurchaseBase.AddSensorPrintCredits(acnt, cust, creditsToAssign, activationCode);
    }
    catch (Exception ex)
    {
      ex.Log($"PurchaseBase.ProcessSensorPrintCreditKey AccountID: {acnt.AccountID}, ActivationCode: {activationCode}");
      return "Activation Failed";
    }
  }

  public static string AddSensorPrintCredits(
    Account acnt,
    Customer cust,
    int creditsToAssign,
    string activationCode = "")
  {
    try
    {
      long num = long.MinValue;
      foreach (NotificationCreditType notificationCreditType in NotificationCreditType.LoadByClassification(eCreditClassification.SensorPrint))
      {
        if (notificationCreditType.Name.ToLower() == "sensorprint credit")
          num = notificationCreditType.NotificationCreditTypeID;
      }
      if (num == long.MinValue)
        return "Credit type not found.";
      Credit credit = (Credit) null;
      if (creditsToAssign > 0)
      {
        credit = new Credit();
        credit.CreditTypeID = num;
        credit.AccountID = acnt.AccountID;
        credit.ActivatedByCustomerID = cust.CustomerID;
        credit.ActivationDate = DateTime.UtcNow;
        credit.ActivationCode = activationCode;
        credit.ActivatedCredits = creditsToAssign;
        credit.ExpirationDate = new DateTime(2099, 1, 1);
        credit.UsedCredits = 0;
        credit.Save();
      }
      acnt.Save();
      return "Success" + (credit != null ? "|" + credit.CreditID.ToString() : "");
    }
    catch (Exception ex)
    {
      ex.Log("Add SensorPrint  Credits Failed, Message: ");
      return "Failed";
    }
  }

  public static string RetreiveActivationKey(string activationCode, string identifier)
  {
    string str1 = "";
    DateTime utcNow = DateTime.UtcNow;
    string uri = "";
    string str2 = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str2) && bool.Parse(str2);
    try
    {
      uri = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/RetreiveActivationKey?key={0}&advancedFailResponse={1}", (object) Convert.ToBase64String(Encoding.UTF8.GetBytes($"{activationCode}|{identifier}")), (object) true);
      XDocument xdocument = XDocument.Load(uri);
      str1 = xdocument.Descendants((XName) "Result").Single<XElement>().Value;
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RetreiveActivationKey",
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
      ex.Log($"CheckoutController.RetreiveActivationKey[Code: {activationCode}][ID: {identifier}] ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-RetreiveActivationKey",
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

  public static string AddMessageCredits(
    Account acnt,
    Customer cust,
    int messageCreditsToAssign,
    string activationCode = "")
  {
    try
    {
      long num1 = long.MinValue;
      foreach (NotificationCreditType notificationCreditType in NotificationCreditType.LoadByClassification(eCreditClassification.Message))
      {
        if (notificationCreditType.Name.ToLower() == "purchased messages")
          num1 = notificationCreditType.NotificationCreditTypeID;
      }
      if (num1 == long.MinValue)
        return "Credit type not found.";
      int num2 = 0;
      foreach (Credit credit in Credit.LoadOverdraft(acnt.AccountID, eCreditClassification.Message))
      {
        int num3 = Math.Abs(credit.ActivatedCredits - credit.UsedCredits);
        num2 += num3;
        credit.Delete();
      }
      bool flag = num2 - messageCreditsToAssign > 0;
      Credit credit1 = (Credit) null;
      if (messageCreditsToAssign > 0)
      {
        credit1 = new Credit();
        credit1.CreditTypeID = num1;
        credit1.AccountID = acnt.AccountID;
        credit1.ActivatedByCustomerID = cust.CustomerID;
        credit1.ActivationDate = DateTime.UtcNow;
        credit1.ActivationCode = activationCode;
        credit1.ActivatedCredits = messageCreditsToAssign;
        credit1.ExpirationDate = new DateTime(2099, 1, 1);
        if (flag)
        {
          credit1.UsedCredits = messageCreditsToAssign;
          credit1.ExhaustedDate = credit1.ActivationDate;
        }
        else
          credit1.UsedCredits = num2;
        credit1.Save();
        acnt.LogAuditData(eAuditAction.Update, eAuditObject.Account, cust.CustomerID, acnt.AccountID, "Activated HX Message  To Account");
      }
      if (flag)
      {
        credit1 = new Credit();
        credit1.CreditTypeID = 7L;
        credit1.AccountID = acnt.AccountID;
        credit1.ActivationDate = DateTime.UtcNow;
        credit1.ActivationCode = (string) null;
        credit1.ActivatedCredits = 0;
        credit1.UsedCredits = num2 - messageCreditsToAssign;
        credit1.ExpirationDate = new DateTime(2099, 1, 1);
        credit1.Save();
      }
      else
      {
        acnt.IsHxEnabled = true;
        acnt.HideData = false;
        acnt.Save();
      }
      foreach (ExternalDataSubscription dataSubscription in ExternalDataSubscription.LoadAllByAccountID(acnt.AccountID))
      {
        if (!dataSubscription.IsDeleted)
        {
          dataSubscription.DoSend = true;
          dataSubscription.DoRetry = dataSubscription.BrokenCount < 20;
          dataSubscription.Save();
        }
      }
      return "Success" + (credit1 != null ? "|" + credit1.CreditID.ToString() : "");
    }
    catch (Exception ex)
    {
      ex.Log("Add Message Credits Failed, Message: ");
      return "Failed";
    }
  }

  public static string ProcessNotificationCreditKey(
    string activationCode,
    Account acnt,
    Customer cust,
    AccountSubscription sub)
  {
    try
    {
      if (PurchaseBase.CheckSubscriptionCode(activationCode).Split(new char[1]
      {
        '_'
      }, StringSplitOptions.RemoveEmptyEntries)[0].Equals("Premiere"))
        return PurchaseBase.TryUpdateSubscription(acnt.AccountID, activationCode, cust, sub) + "_MNW-IP";
      string s = PurchaseBase.RetreiveActivationKey(activationCode, acnt.AccountID.ToString());
      if (s.Equals("Activation Failed"))
        return "Activation Failed";
      foreach (Credit credit in Credit.LoadByAccount(acnt.AccountID))
      {
        if (credit.ActivationCode == activationCode)
          return "Credit code previously used.";
      }
      string[] typeArray = Encoding.UTF8.GetString(Convert.FromBase64String(s)).Split('|')[0].Split('_');
      string lower = typeArray[0].ToLower();
      int num = typeArray[1].ToInt();
      switch (lower.ToLower())
      {
        case "rapidheartbeat":
          return PurchaseBase.AddMessageCredits(acnt, cust, num) + "_MNW-HX-006";
        case "sensorprint":
          return PurchaseBase.AddSensorPrintCredits(acnt, cust, num) + "_MNW-SP-006";
        case "purchased":
          return PurchaseBase.AddNotificationCredits(activationCode, acnt, cust, typeArray, lower, num) + "_MNW-NC-006";
        default:
          return "Failed";
      }
    }
    catch (Exception ex)
    {
      ex.Log($"RetailController.ProcessCreditKey AccountID: {acnt.AccountID}, ActivationCode: {activationCode}");
      return "Activation Failed";
    }
  }

  public static string TryUpdateSubscription(
    long id,
    string subscriptionActivationCode,
    Customer cust,
    AccountSubscription sub)
  {
    Account account = Account.Load(id);
    AccountSubscriptionType subscriptionType = AccountSubscriptionType.LoadByKeyType(PurchaseBase.CheckSubscriptionCode(subscriptionActivationCode));
    if (subscriptionType == null)
      return "Failed: Invalid Code";
    List<Sensor> sensorList = Sensor.LoadByAccountID(account.AccountID);
    if (sensorList.Count > subscriptionType.AllowedSensors)
      return $"Failed: Invalid Code for {sensorList.Count.ToString()} sensors";
    AccountSubscription accountSubscription1 = (AccountSubscription) null;
    DateTime expirationDate = account.CurrentSubscription.ExpirationDate;
    try
    {
      DateTime expirationDateFromMea = PurchaseBase.GetExpirationDateFromMEA(subscriptionActivationCode, account.AccountID, subscriptionType.KeyType, account.CurrentSubscription.AccountSubscriptionType.KeyType, expirationDate);
      if (expirationDateFromMea == DateTime.MinValue)
        throw new Exception("Update Subscription Failed; Invalid Subscription Activation Key: " + subscriptionActivationCode);
      foreach (AccountSubscription subscription in account.Subscriptions)
      {
        if (subscription.AccountSubscriptionTypeID == subscriptionType.AccountSubscriptionTypeID)
        {
          accountSubscription1 = subscription;
          break;
        }
      }
      if (accountSubscription1 != null)
      {
        accountSubscription1.LastKeyUsed = subscriptionActivationCode.ToGuid();
        accountSubscription1.UpdateAccountSubscriptionDate(expirationDateFromMea, cust, "Key Entered: " + subscriptionActivationCode);
      }
      else
      {
        AccountSubscription accountSubscription2 = new AccountSubscription();
        accountSubscription2.AccountID = account.AccountID;
        accountSubscription2.AccountSubscriptionTypeID = subscriptionType.AccountSubscriptionTypeID;
        accountSubscription2.ExpirationDate = expirationDateFromMea;
        accountSubscription2.LastKeyUsed = subscriptionActivationCode.ToGuid();
        accountSubscription2.Save();
        accountSubscription1 = accountSubscription2;
      }
      account.ClearSubscritions();
      AccountSubscription.AccountSubscriptionFeatureChange(account, account.CurrentSubscription);
      if (account.AccountID == cust.AccountID)
      {
        sub = (AccountSubscription) null;
        cust.Account = (Account) null;
      }
      account.LogAuditData(eAuditAction.Update, eAuditObject.Account, cust.CustomerID, account.AccountID, "Updated Subscription using Activation Code:" + subscriptionActivationCode);
      return "Success" + (accountSubscription1 != null ? "|" + accountSubscription1.AccountSubscriptionID.ToString() : "");
    }
    catch (Exception ex)
    {
      ex.Log("RetailController.TryUpdateSubscription[Update Subscription Failed], message: ");
    }
    return "Failed";
  }

  private static string AddNotificationCredits(
    string activationCode,
    Account acnt,
    Customer cust,
    string[] typeArray,
    string TypeName,
    int purchasedCredits)
  {
    NotificationCredit notificationCredit1 = (NotificationCredit) null;
    foreach (NotificationCreditType notificationCreditType in NotificationCreditType.LoadAll())
    {
      if (TypeName == notificationCreditType.Name.ToLower())
      {
        int months = typeArray.Length > 2 ? typeArray[2].ToInt() : 0;
        if (purchasedCredits > 0)
        {
          notificationCredit1 = new NotificationCredit();
          notificationCredit1.NotificationCreditTypeID = notificationCreditType.NotificationCreditTypeID;
          notificationCredit1.AccountID = acnt.AccountID;
          notificationCredit1.ActivatedByCustomerID = cust.CustomerID;
          NotificationCredit notificationCredit2 = notificationCredit1;
          DateTime utcNow = DateTime.UtcNow;
          DateTime dateTime1 = utcNow.AddMinutes(-10.0);
          notificationCredit2.ActivationDate = dateTime1;
          notificationCredit1.ActivationCode = activationCode;
          notificationCredit1.ActivatedCredits = purchasedCredits;
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
    if (notificationCredit1 != null && acnt.AutoPurchase)
    {
      string notificationEmail = cust.NotificationEmail;
      try
      {
        string subject = "Confirmation of Your Purchase of Notification Credits";
        EmailTemplate emailTemplate = EmailTemplate.LoadBest(acnt, eEmailTemplateFlag.Generic);
        AccountTheme.Load(acnt.GetThemeID());
        if (emailTemplate == null)
        {
          emailTemplate = new EmailTemplate();
          emailTemplate.Subject = subject;
          emailTemplate.Template = "<p>Thank you.  We appreciate your business.</p><p>{Content}</p>";
        }
        string str = "You have successfully purchased 1 pack of 100 Notification Credits.";
        string body = "<p style=\"display:none;\">Thank you.  We appreciate your business.</p>" + emailTemplate.MailMerge($"<p>Thank you for using Auto Purchase for Notification Credits <b>{acnt.AccountNumber}({acnt.AccountID.ToString()})</b>. Your payment will be processed today.</p>{str}<p>If you have any questions or need further assistance, please feel free to reach out to us at <a href=\"mailto:sales@monnit.com\">sales@monnit.com</a>.  For technical support, contact us at <a href=\"mailto:support@monnit.com\">support@monnit.com</a>.</p><br/> Regards,<br/> The Monnit Team", notificationEmail);
        MonnitUtil.SendMail(notificationEmail, subject, body, acnt);
      }
      catch (Exception ex)
      {
        ex.Log($"PurchaseBase.AddNotificationCredits[POST][AutoPurchaseConfirmationEmail][AccountID: {acnt.AccountID.ToString()}][SKU: MNW-NC][ToEmail: {notificationEmail}] ");
      }
    }
    return "Success" + (notificationCredit1 != null ? "|" + notificationCredit1.NotificationCreditID.ToString() : "");
  }

  public static string ProcessGatewayUnlockCreditKey(
    string activationCode,
    Account acnt,
    Customer cust)
  {
    try
    {
      string s = PurchaseBase.RetreiveActivationKey(activationCode, acnt.AccountID.ToString());
      if (s.Equals("Activation Failed"))
        return "Activation Failed";
      NotificationCreditType creditType = NotificationCreditType.LoadByClassification(eCreditClassification.GatewayUnlock).FirstOrDefault<NotificationCreditType>();
      List<Credit> source = Credit.LoadByAccount(acnt.AccountID);
      if (creditType != null)
        source = source.Where<Credit>((Func<Credit, bool>) (x => x.CreditTypeID == creditType.NotificationCreditTypeID)).ToList<Credit>();
      foreach (Credit credit in source)
      {
        if (credit.ActivationCode == activationCode)
          return "Credit code previously used.";
      }
      Encoding.UTF8.GetString(Convert.FromBase64String(s)).Split('|');
      int creditsToAssign = 1;
      return PurchaseBase.AddGatewayUnlockCredits(acnt, cust, creditsToAssign, activationCode);
    }
    catch (Exception ex)
    {
      ex.Log($"RetailController.ProcessGatewayUnlockCreditKey AccountID: {acnt.AccountID}, ActivationCode: {activationCode}");
      return "Activation Failed";
    }
  }

  public static string AddGatewayUnlockCredits(
    Account acnt,
    Customer cust,
    int creditsToAssign,
    string activationCode = "")
  {
    try
    {
      long num = long.MinValue;
      foreach (NotificationCreditType notificationCreditType in NotificationCreditType.LoadByClassification(eCreditClassification.GatewayUnlock))
      {
        if (notificationCreditType.Name.ToLower() == "gatewayunlock credit")
          num = notificationCreditType.NotificationCreditTypeID;
      }
      if (num == long.MinValue)
        return "Credit type not found.";
      Credit credit = (Credit) null;
      if (creditsToAssign > 0)
      {
        credit = new Credit();
        credit.CreditTypeID = num;
        credit.AccountID = acnt.AccountID;
        credit.ActivatedByCustomerID = cust.CustomerID;
        credit.ActivationDate = DateTime.UtcNow;
        credit.ActivationCode = activationCode;
        credit.ActivatedCredits = creditsToAssign;
        credit.ExpirationDate = new DateTime(2099, 1, 1);
        credit.UsedCredits = 0;
        credit.Save();
      }
      acnt.Save();
      return "Success" + (credit != null ? "|" + credit.CreditID.ToString() : "");
    }
    catch (Exception ex)
    {
      ex.Log("Add GatewayUnlock Credits Failed, Message: ");
      return "Failed";
    }
  }

  public static string ProcessGatewayUnlockGpsCreditKey(
    string activationCode,
    Account acnt,
    Customer cust)
  {
    try
    {
      string s = PurchaseBase.RetreiveActivationKey(activationCode, acnt.AccountID.ToString());
      if (s.Equals("Activation Failed"))
        return "Activation Failed";
      NotificationCreditType creditType = NotificationCreditType.LoadByClassification(eCreditClassification.GatewayUnlockGps).FirstOrDefault<NotificationCreditType>();
      List<Credit> source = Credit.LoadByAccount(acnt.AccountID);
      if (creditType != null)
        source = source.Where<Credit>((Func<Credit, bool>) (x => x.CreditTypeID == creditType.NotificationCreditTypeID)).ToList<Credit>();
      foreach (Credit credit in source)
      {
        if (credit.ActivationCode == activationCode)
          return "Credit code previously used.";
      }
      Encoding.UTF8.GetString(Convert.FromBase64String(s)).Split('|');
      int creditsToAssign = 1;
      return PurchaseBase.AddGatewayUnlockGpsCredits(acnt, cust, creditsToAssign, activationCode);
    }
    catch (Exception ex)
    {
      ex.Log($"RetailController.ProcessGatewayUnlockGpsCreditKey AccountID: {acnt.AccountID}, ActivationCode: {activationCode}");
      return "Activation Failed";
    }
  }

  public static string AddGatewayUnlockGpsCredits(
    Account acnt,
    Customer cust,
    int creditsToAssign,
    string activationCode = "")
  {
    try
    {
      long num = long.MinValue;
      foreach (NotificationCreditType notificationCreditType in NotificationCreditType.LoadByClassification(eCreditClassification.GatewayUnlockGps))
      {
        if (notificationCreditType.Name.ToLower() == "gps unlock credit")
          num = notificationCreditType.NotificationCreditTypeID;
      }
      if (num == long.MinValue)
        return "Credit type not found.";
      Credit credit = (Credit) null;
      if (creditsToAssign > 0)
      {
        credit = new Credit();
        credit.CreditTypeID = num;
        credit.AccountID = acnt.AccountID;
        credit.ActivatedByCustomerID = cust.CustomerID;
        credit.ActivationDate = DateTime.UtcNow;
        credit.ActivationCode = activationCode;
        credit.ActivatedCredits = creditsToAssign;
        credit.ExpirationDate = new DateTime(2099, 1, 1);
        credit.UsedCredits = 0;
        credit.Save();
      }
      acnt.Save();
      return "Success" + (credit != null ? "|" + credit.CreditID.ToString() : "");
    }
    catch (Exception ex)
    {
      ex.Log("Add GatewayUnlockGps Credits Failed, Message: ");
      return "Failed";
    }
  }

  public static string CheckSubscriptionCode(string subscriptionCode)
  {
    DateTime utcNow = DateTime.UtcNow;
    string uri = "";
    string str = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
    try
    {
      uri = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/IsPremiereValid/{1}?guid={0}", (object) subscriptionCode, (object) PurchaseBase.GetTempAuthToken());
      XDocument xdocument = XDocument.Load(uri);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-CheckSubscriptionCode",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = xdocument.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      return xdocument.Descendants((XName) "Result").Single<XElement>().Value.ToString();
    }
    catch (Exception ex)
    {
      ex.Log($"OverviewController.CheckSubscriptionCode[Code: {subscriptionCode}] ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-CheckSubscriptionCode",
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

  public static DateTime GetExpirationDateFromMEA(
    string subscriptionCode,
    long accountID,
    string keyType,
    string activeKeyType,
    DateTime date)
  {
    DateTime utcNow = DateTime.UtcNow;
    string uri = "";
    string str = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str) && bool.Parse(str);
    try
    {
      string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{subscriptionCode}|{accountID}|{keyType}"));
      uri = string.Format(ConfigData.AppSettings("MEA_API_Location") + "xml/RetreiveActivationKey/{1}?key={0}&activeKeyType={2}&currentEndDate={3}", (object) base64String, (object) PurchaseBase.GetTempAuthToken(), (object) activeKeyType, (object) date);
      XDocument xdocument = XDocument.Load(uri);
      DateTime dateTime = xdocument.Descendants((XName) "Result").Single<XElement>().Value.ToDateTime();
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-GetExpirationDateFromMEA",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = xdocument.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      return dateTime;
    }
    catch (Exception ex)
    {
      ex.Log($"OverviewController.GetExpirationDateFromMEA[Code: {subscriptionCode}][AccountID: {accountID.ToString()}] ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-GetExpirationDateFromMEA",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return DateTime.MinValue;
  }

  public static string GetTempAuthToken()
  {
    string str = ConfigData.AppSettings("MEA_API_Location");
    string tempAuthToken = "";
    try
    {
      tempAuthToken = XDocument.Load($"{str}xml?applicationAuthGuid={ConfigData.FindValue("MEA_API_Auth_Guid")}").Descendants((XName) "object").Single<XElement>().Attribute((XName) "auth").Value;
    }
    catch
    {
    }
    return tempAuthToken;
  }

  public static string ClientIP(HttpRequest request)
  {
    return !string.IsNullOrEmpty(request.Headers["X-Forwarded-For"]) ? request.Headers["X-Forwarded-For"] : request.UserHostAddress;
  }
}
