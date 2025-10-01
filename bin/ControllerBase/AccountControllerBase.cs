// Decompiled with JetBrains decompiler
// Type: iMonnit.ControllerBase.AccountControllerBase
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.Models;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

#nullable disable
namespace iMonnit.ControllerBase;

public class AccountControllerBase : ThemeController
{
  protected void CheckUserNameCookie(LogOnModel model)
  {
    try
    {
      if (System.Web.HttpContext.Current.Request.Cookies["UserName"] == null)
        return;
      System.Web.HttpContext.Current.Request.Cookies["UserName"].Expires = DateTime.Now.AddYears(1);
      model.UserName = System.Web.HttpContext.Current.Request.Cookies["UserName"].Value;
    }
    catch (Exception ex)
    {
      ex.Log(nameof (CheckUserNameCookie));
    }
  }

  protected void CheckRememberMeCookie(LogOnModel model)
  {
    try
    {
      if (System.Web.HttpContext.Current.Request.Cookies["Preferences"] == null)
        return;
      HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["Preferences"];
      Customer customer = Customer.LoadFromUsername(cookie["LID"]);
      if (customer != null)
      {
        int num = customer.CheckPreferenceCookie(cookie);
        if (customer.PasswordExpired)
          num = -1;
        switch (num)
        {
          case -1:
            customer.DeleteRememberMe();
            goto case 0;
          case 0:
            break;
          default:
            model.UserName = cookie["LID"];
            model.RememberMe = true;
            MonnitSession.CustomerIDLoggedInAsProxy = long.MinValue;
            FormsAuthentication.SetAuthCookie(model.UserName, false);
            customer.LastLoginDate = DateTime.UtcNow;
            customer.FailedLoginCount = 0;
            customer.Save();
            MonnitSession.CurrentCustomer = customer;
            cookie["Recent"] = num.ToString();
            cookie.Expires = DateTime.Now.AddYears(1);
            cookie.HttpOnly = true;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            goto case 0;
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("CheckRememberMeCookie ");
    }
  }

  public static HttpCookie GetTwoFactorAuthCodeCookie(Customer customer, System.Web.HttpContext httpContext)
  {
    HttpCookie factorAuthCodeCookie = (HttpCookie) null;
    try
    {
      factorAuthCodeCookie = System.Web.HttpContext.Current.Request.Cookies["TwoFactorAuthSecret" + customer.CustomerID.ToString()];
      if (factorAuthCodeCookie == null)
      {
        factorAuthCodeCookie = System.Web.HttpContext.Current.Request.Cookies["TwoFactorAuthSecret"];
        if (factorAuthCodeCookie != null)
        {
          System.Web.HttpContext.Current.Response.Cookies.Remove("TwoFactorAuthSecret");
          factorAuthCodeCookie = AccountControllerBase.SetTwoFactorAuthCodeCookie(factorAuthCodeCookie.Value, customer, httpContext);
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("CheckTwoFactorAuthCodeCookie");
    }
    return factorAuthCodeCookie;
  }

  public static HttpCookie SetTwoFactorAuthCodeCookie(
    string code,
    Customer customer,
    System.Web.HttpContext httpContext = null)
  {
    httpContext = httpContext ?? System.Web.HttpContext.Current;
    HttpCookie cookie = httpContext.Request.Cookies["TwoFactorAuthSecret" + customer.CustomerID.ToString()] ?? new HttpCookie("TwoFactorAuthSecret" + customer.CustomerID.ToString());
    cookie.Value = code;
    cookie.Expires = DateTime.UtcNow.AddDays(90.0);
    cookie.HttpOnly = true;
    httpContext.Response.Cookies.Add(cookie);
    return cookie;
  }

  protected void SetUserNameCookie(Customer CurrentCust)
  {
    HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["UserName"] ?? new HttpCookie("UserName");
    cookie.Expires = DateTime.Now.AddYears(1);
    cookie.Value = CurrentCust.UserName;
    cookie.HttpOnly = true;
    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
  }

  protected void SetRememberMeCookie(Customer CurrentCust, bool rememberMe)
  {
    HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["Preferences"];
    if (rememberMe)
    {
      if (cookie == null)
        cookie = new HttpCookie("Preferences");
      CustomerRememberMe customerRememberMe = CustomerRememberMe.Create(CurrentCust.CustomerID);
      cookie["LID"] = CurrentCust.UserName;
      HttpCookie httpCookie1 = cookie;
      int num = customerRememberMe.SequenceNumber;
      string str1 = num.ToString();
      httpCookie1["Sequence"] = str1;
      HttpCookie httpCookie2 = cookie;
      num = customerRememberMe.Recent;
      string str2 = num.ToString();
      httpCookie2["Recent"] = str2;
      cookie.Expires = DateTime.Now.AddYears(1);
      cookie.HttpOnly = true;
      System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
    }
    else if (cookie != null)
      this.ClearRememberMeCookie(CurrentCust, cookie);
  }

  protected void ClearRememberMeCookie(Customer CurrentCust = null, HttpCookie cookie = null)
  {
    cookie = cookie ?? System.Web.HttpContext.Current.Request.Cookies["Preferences"];
    CurrentCust = CurrentCust ?? MonnitSession.CurrentCustomer;
    if (cookie == null)
      return;
    cookie.Expires = DateTime.Now.AddDays(-1.0);
    cookie.HttpOnly = true;
    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
    CurrentCust?.RemovePreferenceCookie(cookie);
  }

  protected string[] SSOCredentialsFromAuthToken(string authToken)
  {
    string[] strArray;
    try
    {
      byte[] bytes = Convert.FromBase64String(authToken);
      strArray = Encoding.ASCII.GetString(bytes).Split(':');
      if (strArray.Length != 2)
        strArray = Encoding.ASCII.GetString(bytes).Split('|');
      if (strArray.Length != 2)
        strArray = Encoding.ASCII.GetString(bytes).Split('-');
    }
    catch (FormatException ex)
    {
      strArray = authToken.Split('-');
    }
    return strArray;
  }

  public static string SendUserNameReminder(string email)
  {
    string str1 = "Email not found";
    List<Customer> list = Customer.LoadAllByEmail(email).Where<Customer>((Func<Customer, bool>) (c => c.NotificationEmail.ToLower() == email.ToLower())).OrderBy<Customer, long>((Func<Customer, long>) (c => c.AccountID)).ToList<Customer>();
    if (list.Count > 0)
    {
      str1 = "Your user-name was sent to your registered email account.";
      foreach (Customer customer in list)
      {
        try
        {
          EmailTemplate emailTemplate = EmailTemplate.LoadBest(customer.Account, eEmailTemplateFlag.Generic);
          if (emailTemplate == null)
          {
            emailTemplate = new EmailTemplate();
            emailTemplate.Subject = "User-name is";
            emailTemplate.Template = "<p>Your user-name is</p><p>{Content}</p>";
          }
          string body = emailTemplate.MailMerge($"User-name: {customer.UserName}<br />", customer.NotificationEmail);
          MonnitUtil.SendMail(customer.NotificationEmail, "Here is your user-name: ", body, customer.Account);
        }
        catch (Exception ex)
        {
          str1 = "Failed! Please contact support for assistance getting your user-name.";
          string str2 = email == null ? "null" : email;
          ex.Log($"AccountControllerBase.SendUserNameReminder | email = {str2} | Result = {str1}");
          break;
        }
      }
    }
    return str1;
  }

  protected static string CredentialRecoveryAuthKey()
  {
    int capacity = 8;
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

  public static string TrySendStoreLinkCode(string email)
  {
    try
    {
      string str = AccountControllerBase.CredentialRecoveryAuthKey();
      EmailTemplate emailTemplate = EmailTemplate.LoadBest(MonnitSession.CurrentCustomer.Account, eEmailTemplateFlag.Generic);
      AccountTheme.Load(MonnitSession.CurrentCustomer.Account.GetThemeID());
      if (emailTemplate == null)
      {
        emailTemplate = new EmailTemplate();
        emailTemplate.Subject = "Sensor Portal to Store Link Code";
        emailTemplate.Template = "<p>Your Link Code </p><p>{Content}</p>";
      }
      string body = emailTemplate.MailMerge($"We received a request to link the online store account associated with this e-mail address. <br /> Purchasing subscription extensions and your special feature credits just got much easier! You can now link your payment information from the online store (www.Monnit.com), This will allow you to update subscriptions and purchase credits without leaving the sensor portal   <br /> <br /> Your Link Code: <strong>{str}</strong> <br /> <br /> Please return to your browser and submit this code within 15 minutes.  <br /> <br />If you do not recognize this account activity , Please contact support as soon as possible.", email);
      if (MonnitUtil.SendMail(email, "Link Sensor Portal and Online Store", body, MonnitSession.CurrentCustomer.Account))
        return str;
    }
    catch (Exception ex)
    {
      ex.Log($"Store Link Code email failed. CustomerID: {MonnitSession.CurrentCustomer.CustomerID.ToString()} EmailToLink: {email}");
    }
    return "Failed: Link Code email failed to send, please try again.";
  }

  public static Customer TrySendPasswordResetAuth(string email, string key)
  {
    Customer customer = Customer.LoadAllByEmail(email).FirstOrDefault<Customer>();
    if (customer == null)
      return (Customer) null;
    try
    {
      EmailTemplate emailTemplate = EmailTemplate.LoadBest(customer.Account, eEmailTemplateFlag.Generic);
      AccountTheme accountTheme = AccountTheme.Load(customer.Account.GetThemeID());
      if (emailTemplate == null)
      {
        emailTemplate = new EmailTemplate();
        emailTemplate.Subject = "Credential Recovery";
        emailTemplate.Template = "<p>Your Credential Recovery Authorization Key </p><p>{Content}</p>";
      }
      string body = emailTemplate.MailMerge(string.Format("We received a request to reset the password or display the user name associated with this e-mail address.  <br /> <br /> Your Credential Recovery Authorization Key: <strong>{0}</strong> <br /> <br /> Please return to your browser and submit this key within {1} minutes. or click this link <a href='{2}'>{2}</a> <br /> <br />If you do not recognize this account activity , Please contact support as soon as possible.", (object) key, (object) ConfigData.AppSettings("CredSessionLength").ToStringSafe(), (object) $"https://{accountTheme.Domain}/Account/PasswordReset/{key}"), customer.NotificationEmail);
      MonnitUtil.SendMail(customer.NotificationEmail, "Credential Recovery", body, customer.Account);
      return customer;
    }
    catch (Exception ex)
    {
      ex.Log("Credential Reset Notification email failed. CustomerID: " + customer.CustomerID.ToString());
      return (Customer) null;
    }
  }

  public static string SendPasswordReminder(string userName)
  {
    string str = "Email not found";
    Customer customer = Customer.LoadFromUsername(userName);
    if (customer != null && customer.NotificationEmail.IsValidEmail())
    {
      str = "Your temporary password has been sent to your registered email.";
      try
      {
        EmailTemplate emailTemplate = EmailTemplate.LoadBest(customer.Account, eEmailTemplateFlag.Generic);
        if (emailTemplate == null)
        {
          emailTemplate = new EmailTemplate();
          emailTemplate.Subject = "Password Reset";
          emailTemplate.Template = "<p>The password for you account has been reset</p><p>{Content}</p>";
        }
        string password = RandomPassword.Generate();
        customer.Salt = MonnitUtil.GenerateSalt();
        customer.WorkFactor = ConfigData.AppSettings("WorkFactor").ToInt();
        customer.Password2 = MonnitUtil.GenerateHash(password, customer.Salt, customer.WorkFactor);
        customer.Password = "";
        customer.PasswordExpired = true;
        customer.ForceLogoutDate = DateTime.UtcNow;
        string body = emailTemplate.MailMerge($"User-name: {customer.UserName}<br />Password: {password}<br />", customer.NotificationEmail);
        MonnitUtil.SendMail(customer.NotificationEmail, "Password Reset", body, customer.Account);
        customer.Save();
      }
      catch
      {
        str = "Failed! Please contact your administrator for assistance resetting your password.";
      }
    }
    return str;
  }
}
