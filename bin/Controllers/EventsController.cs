// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.EventsController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

[NoCache]
public class EventsController : ThemeController
{
  [AuthorizeDefault]
  public ActionResult Index()
  {
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = nameof (Index),
      controller = "Rule"
    });
  }
}
