// Decompiled with JetBrains decompiler
// Type: iMonnit.JsonpResult
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

#nullable disable
namespace iMonnit;

public class JsonpResult : JsonResult
{
  public string Callback { get; set; }

  public override void ExecuteResult(ControllerContext context)
  {
    if (context == null)
      throw new ArgumentNullException(nameof (context));
    HttpResponseBase response = context.HttpContext.Response;
    response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/javascript" : this.ContentType;
    if (this.ContentEncoding != null)
      response.ContentEncoding = this.ContentEncoding;
    if (this.Callback == null || this.Callback.Length == 0)
      this.Callback = context.HttpContext.Request.QueryString["callback"];
    if (this.Callback == null || this.Callback.Length == 0)
      this.Callback = context.HttpContext.Request.QueryString["jsonp"];
    if (this.Callback == null || this.Callback.Length == 0)
      this.Callback = "callback";
    if (this.Data == null)
      return;
    string str = new JavaScriptSerializer()
    {
      MaxJsonLength = Convert.ToInt32(ConfigData.AppSettings("MaxJsonLength", int.MaxValue.ToString()))
    }.Serialize(this.Data);
    response.Write($"{this.Callback}({str});");
  }
}
