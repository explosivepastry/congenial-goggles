// Decompiled with JetBrains decompiler
// Type: iMonnit.MvcApplication
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.Binder;
using Monnit;
using MvcApplication1;
using RedefineImpossible;
using System;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

#nullable disable
namespace iMonnit;

public class MvcApplication : HttpApplication
{
  protected void Application_Start()
  {
    AreaRegistration.RegisterAllAreas();
    WebApiConfig.Register(GlobalConfiguration.Configuration);
    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    RouteConfig.RegisterRoutes(RouteTable.Routes);
    ViewEngines.Engines.Clear();
    ViewEngines.Engines.Add((IViewEngine) new MonnitViewEngine());
    ModelBinders.Binders.DefaultBinder = (IModelBinder) new AntiXssEncoderCustomBinder();
    ControllerBuilder.Current.SetControllerFactory((IControllerFactory) new MonnitControllerFactory());
  }

  protected void Session_Start(object sender, EventArgs e)
  {
    string sessionId = this.Session.SessionID;
  }

  protected void Application_Error(object sender, EventArgs e)
  {
    HttpContext current = HttpContext.Current;
    Exception lastError = this.Context.Server.GetLastError();
    if ((lastError.Message.StartsWith("The controller for path") || lastError.Message.StartsWith("A public action method")) && !this.LogBadPathAsException)
      return;
    new ExceptionLog(lastError)
    {
      Message = $"iMonnit.MvcApplication.Application_Error(sender = {sender}, e = {e}) : {lastError.Message}"
    }.Save();
  }

  private bool LogBadPathAsException
  {
    get
    {
      try
      {
        return ConfigData.AppSettings(nameof (LogBadPathAsException), "False").ToBool();
      }
      catch
      {
        return false;
      }
    }
  }

  private void Application_BeginRequest(object sender, EventArgs e)
  {
    if (!(HttpContext.Current.Request.ServerVariables["HTTPS"] != "on") || !HttpContext.Current.Request.Url.AbsolutePath.ToLower().StartsWith("/account/logon") || !ConfigurationManager.AppSettings["Https"].ToBool())
      return;
    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.Replace("http://", "https://"));
  }

  private void Application_EndRequest(object sender, EventArgs e)
  {
    if (!ConfigurationManager.AppSettings["LoadBalancerHttps"].ToBool() || this.Request.IsLocal || this.Response.Cookies.Count <= 0)
      return;
    foreach (string allKey in this.Response.Cookies.AllKeys)
      this.Response.Cookies[allKey].Secure = true;
  }
}
