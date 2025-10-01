// Decompiled with JetBrains decompiler
// Type: iMonnit.AuthorizeAPIPremierAttribute
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using RedefineImpossible;
using System;
using System.Web.Mvc;

#nullable disable
namespace iMonnit;

public class AuthorizeAPIPremierAttribute : AuthorizeAPIAttribute
{
  public override void OnActionExecuting(ActionExecutingContext filterContext)
  {
    string authString = AuthorizeAPIAttribute.GetAuthString(filterContext);
    string secretString = AuthorizeAPIAttribute.GetSecretString(filterContext);
    string key = $"API_Auth_Premium_{authString}{secretString}";
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
        if (!account.CurrentSubscription.Can(Feature.Find("use_api")))
          throw new Exception("This API call only available to premiere subscribers");
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
          filterContext.Result = (ActionResult) new JsonResult()
          {
            Data = (object) objectToSerialize,
            JsonRequestBehavior = JsonRequestBehavior.AllowGet
          };
          break;
      }
    }
  }
}
