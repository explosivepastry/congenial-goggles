// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.BluetoothController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.ControllerBase;
using System.Text;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

public class BluetoothController : OverviewControllerBase
{
  public ActionResult Index()
  {
    byte[] bytes = this.Request.BinaryRead(this.Request.TotalBytes);
    Encoding.UTF8.GetString(bytes, 0, bytes.Length);
    return (ActionResult) this.Content("Ack");
  }
}
