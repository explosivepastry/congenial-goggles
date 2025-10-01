// Decompiled with JetBrains decompiler
// Type: iMonnit.NoCache
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;
using System.Web;
using System.Web.Mvc;

#nullable disable
namespace iMonnit;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class NoCache : ActionFilterAttribute
{
  public override void OnResultExecuting(ResultExecutingContext filterContext)
  {
    filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1.0));
    filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
    filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
    filterContext.HttpContext.Response.Cache.SetNoStore();
    base.OnResultExecuting(filterContext);
  }

  public override void OnResultExecuted(ResultExecutedContext filterContext)
  {
    base.OnResultExecuted(filterContext);
  }
}
