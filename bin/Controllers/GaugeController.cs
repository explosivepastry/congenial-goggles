// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.GaugeController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

public class GaugeController : ThemeController
{
  public ActionResult Battery(long id)
  {
    return (ActionResult) this.View((object) Sensor.Load(id).LastDataMessage);
  }

  public ActionResult SignalStrength(long id)
  {
    Sensor sensor = Sensor.Load(id);
    this.ViewData["Sensor"] = (object) sensor;
    return (ActionResult) this.View((object) sensor.LastDataMessage);
  }

  public ActionResult SignalQuality(long id)
  {
    return (ActionResult) this.View((object) Sensor.Load(id).LastDataMessage);
  }
}
