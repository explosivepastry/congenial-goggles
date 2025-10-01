// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.EmailController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

[NoCache]
public class EmailController : ThemeController
{
  [Authorize]
  public ActionResult Index() => (ActionResult) this.View();

  [Authorize]
  public ActionResult EmailTemplateOverview(long? id)
  {
    if (!MonnitSession.CurrentCustomer.IsAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    this.ViewData["Templates"] = (object) EmailTemplate.LoadByAccountID(id ?? MonnitSession.CurrentCustomer.AccountID);
    return (ActionResult) this.View();
  }

  [Authorize]
  public ActionResult TemplateList()
  {
    if (!MonnitSession.CurrentCustomer.IsAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    this.ViewData["Templates"] = (object) EmailTemplate.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
    return (ActionResult) this.View();
  }

  [Authorize]
  public ActionResult TemplateDetails(long id)
  {
    EmailTemplate model = EmailTemplate.Load(id);
    return !MonnitSession.CurrentCustomer.IsAdmin || model == null || model.AccountID != MonnitSession.CurrentCustomer.AccountID && !MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult TemplateEdit(long id)
  {
    EmailTemplate model = EmailTemplate.Load(id);
    return !MonnitSession.CurrentCustomer.IsAdmin || model == null || model.AccountID != MonnitSession.CurrentCustomer.AccountID && !MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [Authorize]
  [ValidateInput(false)]
  [ValidateAntiForgeryToken]
  public ActionResult TemplateEdit(long id, FormCollection collection)
  {
    EmailTemplate model = EmailTemplate.Load(id);
    if (!MonnitSession.CurrentCustomer.IsAdmin || model == null || model.AccountID != MonnitSession.CurrentCustomer.AccountID && !MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    try
    {
      this.UpdateModel<EmailTemplate>(model);
      if (!this.ModelState.IsValid)
        return (ActionResult) this.View((object) model);
      model.Save();
      EmailTemplate.ClearCache(model.AccountID);
      return (ActionResult) this.Content("Success!<script type=\"text/javascript\">window.location.href = window.location.href;</script>");
    }
    catch (InvalidOperationException ex)
    {
      return (ActionResult) this.View((object) model);
    }
  }

  [Authorize]
  public ActionResult TemplateCreate(long? id)
  {
    return !MonnitSession.IsCurrentCustomerMonnitAdmin ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) new EmailTemplate());
  }

  [HttpPost]
  [Authorize]
  [ValidateInput(false)]
  [ValidateAntiForgeryToken]
  public ActionResult TemplateCreate(long? id, EmailTemplate emailTemplate)
  {
    if (!MonnitSession.CurrentCustomer.IsAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (string.IsNullOrEmpty(emailTemplate.Flags))
      this.ModelState.AddModelError("Flags", "One or more flags are required!");
    try
    {
      if (!this.ModelState.IsValid)
        return (ActionResult) this.View((object) emailTemplate);
      emailTemplate.AccountID = id ?? MonnitSession.CurrentCustomer.AccountID;
      emailTemplate.Save();
      EmailTemplate.ClearCache(emailTemplate.AccountID);
      return (ActionResult) this.Content("Success!<script type=\"text/javascript\">window.location.href = window.location.href;</script>");
    }
    catch (InvalidOperationException ex)
    {
      return (ActionResult) this.View((object) emailTemplate);
    }
  }

  [Authorize]
  public ActionResult TemplateDelete(long id)
  {
    EmailTemplate model = EmailTemplate.Load(id);
    return !MonnitSession.CurrentCustomer.IsAdmin || model == null || model.AccountID != MonnitSession.CurrentCustomer.AccountID ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [Authorize]
  [ValidateAntiForgeryToken]
  public ActionResult TemplateDelete(long id, FormCollection collection)
  {
    EmailTemplate emailTemplate = EmailTemplate.Load(id);
    if (!MonnitSession.CurrentCustomer.IsAdmin || emailTemplate == null || emailTemplate.AccountID != MonnitSession.CurrentCustomer.AccountID)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    emailTemplate.Delete();
    EmailTemplate.ClearCache(emailTemplate.AccountID);
    return (ActionResult) this.Content("Success!<script type=\"text/javascript\">window.location.href = window.location.href;</script>");
  }
}
