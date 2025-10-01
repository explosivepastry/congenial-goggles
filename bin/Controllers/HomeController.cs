// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.HomeController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.ControllerBase;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

[HandleError]
public class HomeController : HomeControllerBase
{
  [NoCache]
  [Authorize]
  public ActionResult Index(long? id)
  {
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = nameof (Index),
      controller = "Overview"
    });
  }

  [NoCache]
  [Authorize]
  public ActionResult IndexOV(long? id)
  {
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    });
  }
}
