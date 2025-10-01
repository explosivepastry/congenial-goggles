// Decompiled with JetBrains decompiler
// Type: iMonnit.AuthorizeDefaultAttribute
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;
using System.Web.Mvc;
using System.Web.Security;

#nullable disable
namespace iMonnit;

public class AuthorizeDefaultAttribute : ActionFilterAttribute
{
  public override void OnActionExecuting(ActionExecutingContext filterContext)
  {
    bool flag = true;
    if (filterContext.HttpContext.User.Identity.IsAuthenticated && MonnitSession.CurrentCustomer != null)
    {
      DateTime lastLoginDate = MonnitSession.CurrentCustomer.LastLoginDate;
      if (MonnitSession.OldCustomer != null)
        lastLoginDate = MonnitSession.OldCustomer.LastLoginDate;
      flag = lastLoginDate < Customer.CheckForceLogoutDate(MonnitSession.CurrentCustomer.CustomerID);
    }
    if (!flag)
      return;
    FormsAuthentication.SignOut();
    MonnitSession.Session.Clear();
    MonnitSession.Session.Abandon();
    filterContext.Result = (ActionResult) new RedirectResult($"/Account/LogonOV?ReturnUrl={filterContext.HttpContext.Request.Url.AbsolutePath}");
  }
}
