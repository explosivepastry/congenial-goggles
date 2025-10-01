// Decompiled with JetBrains decompiler
// Type: iMonnit.AllowCrossSiteJsonAttribute
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System.Web.Mvc;

#nullable disable
namespace iMonnit;

public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
{
  public override void OnActionExecuting(ActionExecutingContext filterContext)
  {
    filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
    base.OnActionExecuting(filterContext);
  }
}
