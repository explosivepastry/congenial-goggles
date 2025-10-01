// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.SubscriptionController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using RedefineImpossible;
using System;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

public class SubscriptionController : ThemeController
{
  public ActionResult Index() => (ActionResult) this.View();

  private static void SaveChangeLog(FormCollection collection, AccountSubscriptionChangeLog log)
  {
    log.NewExpirationDate = collection["NewExpirationDate"].ToDateTime();
    log.CustomerID = MonnitSession.CurrentCustomer.CustomerID;
    log.ChangeType = collection["ChangeType"];
    log.ChangeNote = collection["ChangeNote"];
    log.ChangeDate = DateTime.UtcNow;
    log.Save();
  }
}
