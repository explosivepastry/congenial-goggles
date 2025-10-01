// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.GenericController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;
using System.Text;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

[NoCache]
public class GenericController : ThemeController
{
  public ActionResult CurrentPermissions()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("Count: {0}<br />", (object) MonnitSession.CurrentCustomer.Permissions.Count);
    foreach (CustomerPermission permission in MonnitSession.CurrentCustomer.Permissions)
      stringBuilder.AppendFormat("{0}: {1}<br />", (object) permission.Type.Name, permission.Can ? (object) "Can" : (object) "");
    return (ActionResult) this.Content(stringBuilder.ToString());
  }

  [Authorize]
  public ActionResult ClearTimedCache()
  {
    try
    {
      if (MonnitSession.IsCurrentCustomerMonnitAdmin)
      {
        TimedCache.ClearAll();
        TimedCache.LastCacheReset = DateTime.UtcNow;
        ConfigData configData = ConfigData.Find("CacheControl");
        configData.Value = DateTime.UtcNow.Ticks.ToString();
        configData.Save();
      }
      this.Session["CurrentStyles"] = (object) null;
      this.Session["CurrentStyleGroup"] = (object) null;
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content(ex.Message);
    }
  }
}
