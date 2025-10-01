// Decompiled with JetBrains decompiler
// Type: iMonnit.AuthorizeAPIAttribute
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using RedefineImpossible;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

#nullable disable
namespace iMonnit;

public class AuthorizeAPIAttribute : ActionFilterAttribute
{
  public override void OnActionExecuting(ActionExecutingContext filterContext)
  {
    string authString = AuthorizeAPIAttribute.GetAuthString(filterContext);
    string secretString = AuthorizeAPIAttribute.GetSecretString(filterContext);
    string key = $"API_Auth_{authString}{secretString}";
    Customer cust = TimedCache.RetrieveObject<Customer>(key);
    try
    {
      if (cust != null)
      {
        Account account = AuthorizeAPIAttribute.CachedAccount(cust.AccountID);
        ++account.CurrentAPICounter;
        if (account.APICallLimit > 0 && account.CurrentAPICounter > account.APICallLimit)
          throw new Exception("API call limit exceeded. Contact support for more information.");
        if (account.APIActive && account.LastAPICallDate > DateTime.Now.AddSeconds((double) (-1 * ConfigData.AppSettings("APICallRateLimitInSeconds", "30").ToInt())))
          throw new Exception("API call rate exceeded. Contact support for more information.");
        account.APIActive = true;
        account.LastAPICallDate = DateTime.Now;
        MonnitSession.CurrentCustomer = cust;
      }
      else
      {
        string[] credentials = (string[]) null;
        if (!string.IsNullOrEmpty(authString))
          AuthorizeAPIAttribute.BasicCredCheck(filterContext, authString, ref cust, ref credentials);
        else
          AuthorizeAPIAttribute.SecretCodeCheck(secretString, ref cust);
        Account account = AuthorizeAPIAttribute.CachedAccount(cust.AccountID);
        if (account.APIRevoked)
          throw new Exception("API access has been revoked. Contact support for more information.");
        ++account.CurrentAPICounter;
        if (account.APICallLimit > 0 && account.CurrentAPICounter > account.APICallLimit)
          throw new Exception("API call limit exceeded. Contact support for more information.");
        if (account.APIActive && account.LastAPICallDate > DateTime.Now.AddSeconds((double) (-1 * ConfigData.AppSettings("APICallRateLimitInSeconds", "30").ToInt())))
          throw new Exception("API call rate exceeded. Contact support for more information.");
        account.APIActive = true;
        account.LastAPICallDate = DateTime.Now;
        TimedCache.AddObjectToCach(key, (object) cust, new TimeSpan(0, 15, 0));
        MonnitSession.CurrentCustomer = cust;
      }
      AuthorizeAPIAttribute.TrySetUsageDetail(filterContext, cust == null ? long.MinValue : cust.CustomerID, cust == null ? "Failed Authentication" : "Authentication Successfull");
    }
    catch (Exception ex)
    {
      AuthorizeAPIAttribute.TrySetUsageDetail(filterContext, cust == null ? long.MinValue : cust.CustomerID, ex.Message);
      MonnitSession.CurrentCustomer = (Customer) null;
      if (ex.Message.Contains("API call limit exceeded") || ex.Message.Contains("API call rate exceeded"))
        filterContext.HttpContext.Response.StatusCode = 429;
      SensorRestAPI objectToSerialize = new SensorRestAPI()
      {
        Method = filterContext.RouteData.Values["action"].ToString(),
        Result = (object) ex.Message
      };
      switch (filterContext.RouteData.Values["response"].ToString().ToLower())
      {
        case "xml":
          filterContext.Result = (ActionResult) new XmlResult((object) objectToSerialize);
          break;
        default:
          if (string.IsNullOrEmpty(filterContext.HttpContext.Request["callback"]))
          {
            filterContext.Result = (ActionResult) new JsonResult()
            {
              Data = (object) objectToSerialize,
              JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            break;
          }
          ActionExecutingContext executingContext = filterContext;
          JsonpResult jsonpResult = new JsonpResult();
          jsonpResult.Data = (object) objectToSerialize;
          jsonpResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
          executingContext.Result = (ActionResult) jsonpResult;
          break;
      }
    }
  }

  public static Account CachedAccount(long accountID)
  {
    Account account = TimedCache.RetrieveObject<Account>("API_Account_" + accountID.ToString());
    if (account == null)
    {
      account = Account.Load(accountID);
      TimedCache.AddObjectToCach("API_Account_" + accountID.ToString(), (object) account, new TimeSpan(0, 15, 0));
    }
    return account;
  }

  protected static void BasicCredCheck(
    ActionExecutingContext filterContext,
    string AuthString,
    ref Customer cust,
    ref string[] credentials)
  {
    try
    {
      byte[] bytes = Convert.FromBase64String(AuthString);
      credentials = Encoding.ASCII.GetString(bytes).Split(':');
      if (credentials.Length != 2)
        credentials = Encoding.ASCII.GetString(bytes).Split('|');
      if (credentials.Length != 2)
        credentials = Encoding.ASCII.GetString(bytes).Split('-');
    }
    catch (FormatException ex)
    {
      credentials = AuthString.Split('-');
    }
    if (credentials == null || credentials.Length != 2)
      throw new Exception("Invalid Authorization Token");
    if (credentials[0].Length == 0)
      throw new Exception("Username Required");
    if (credentials[1].Length == 0)
      throw new Exception("Password Required");
    cust = new Customer();
    if (!Account.ValidateUser(credentials[0], credentials[1], "Monnit API", filterContext.HttpContext.Request.ClientIP(), MonnitSession.UseEncryption, out cust))
      throw new Exception("Authorization Failed");
  }

  protected static void SecretCodeCheck(string SecretString, ref Customer cust)
  {
    try
    {
      string[] strArray = SecretString.Split('|');
      string password = strArray[0];
      APIKey apiKey = APIKey.LoadByKeyValue(strArray[1]).FirstOrDefault<APIKey>();
      if (!StructuralComparisons.StructuralEqualityComparer.Equals((object) MonnitUtil.GenerateHash(password, apiKey.Salt, apiKey.WorkFactor), (object) apiKey.HashedSecret))
        throw new Exception("Authorization Failed");
      int num = ConfigData.AppSettings("WorkFactor").ToInt();
      if (apiKey.WorkFactor != num)
      {
        apiKey.WorkFactor = num;
        apiKey.HashedSecret = MonnitUtil.GenerateHash(password, apiKey.Salt, apiKey.WorkFactor);
      }
      apiKey.LastUsedDate = DateTime.UtcNow;
      apiKey.Save();
      cust = Customer.Load(apiKey.CustomerID);
    }
    catch (Exception ex)
    {
      throw new Exception("Authorization Failed");
    }
  }

  protected static void TrySetUsageDetail(
    ActionExecutingContext filterContext,
    long custID,
    string authenticationResult)
  {
    try
    {
      string o = TimedCache.RetrieveObject<string>("logApiUsage");
      if (o == null)
      {
        o = ConfigData.AppSettings("APIUsage");
        TimedCache.AddObjectToCach("logApiUsage", (object) o, new TimeSpan(0, 5, 0));
      }
      if (!o.ToBool())
        return;
      APIUsageDetail.Insert(filterContext.RouteData.Values["action"].ToString(), $"{filterContext.HttpContext.Request.QueryString.ToString()} | {filterContext.HttpContext.Request.Form.ToString()}", custID, authenticationResult);
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(new Exception("AuthorizeAPI breaking when trying to log api usage detail: rest of the error: " + ex.Message));
    }
  }

  public static string GetAuthString(ActionExecutingContext filterContext)
  {
    return AuthorizeAPIAttribute.GetAuthString(filterContext.HttpContext, filterContext.RouteData);
  }

  public static string GetAuthString(HttpContextBase Context, RouteData RouteData)
  {
    string authString = Context.Request["authorizationtoken"];
    if (!string.IsNullOrEmpty(authString))
      return authString;
    string header = Context.Request.Headers["authorizationtoken"];
    return !string.IsNullOrEmpty(header) ? header : RouteData.Values["auth"].ToString();
  }

  public static string GetSecretString(ActionExecutingContext filterContext)
  {
    return AuthorizeAPIAttribute.GetSecretString(filterContext.HttpContext);
  }

  public static string GetSecretString(HttpContextBase Context)
  {
    string header1 = Context.Request.Headers["APISecretKey"];
    string header2 = Context.Request.Headers["APIKeyID"];
    return !string.IsNullOrEmpty(header1) && !string.IsNullOrEmpty(header2) ? $"{header1}|{header2}" : string.Empty;
  }
}
