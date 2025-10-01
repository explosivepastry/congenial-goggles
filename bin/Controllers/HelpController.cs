// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.HelpController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

public class HelpController : ThemeController
{
  [NoCache]
  [AuthorizeDefault]
  public ActionResult Index() => (ActionResult) this.View();

  [NoCache]
  [AuthorizeDefault]
  public ActionResult GlossaryOfTerms() => (ActionResult) this.View();
}
