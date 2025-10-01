// Decompiled with JetBrains decompiler
// Type: iMonnit.XmlResult
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;

#nullable disable
namespace iMonnit;

public class XmlResult : ActionResult
{
  private object objectToSerialize;

  public XmlResult(object objectToSerialize) => this.objectToSerialize = objectToSerialize;

  public object ObjectToSerialize => this.objectToSerialize;

  public override void ExecuteResult(ControllerContext context)
  {
    if (this.objectToSerialize == null)
      return;
    context.HttpContext.Response.Clear();
    context.HttpContext.Response.ContentType = "text/xml";
    if (this.objectToSerialize is SensorRestAPI && (this.objectToSerialize as SensorRestAPI).Result is XElement)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
      stringBuilder.AppendLine("<SensorRestAPI xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
      stringBuilder.Append("  <Method>");
      stringBuilder.Append((this.objectToSerialize as SensorRestAPI).Method);
      stringBuilder.AppendLine("</Method>");
      stringBuilder.Append("  <Result xsi:type=\"xsd:collection\">");
      stringBuilder.Append(((this.objectToSerialize as SensorRestAPI).Result as XElement).ToString());
      stringBuilder.AppendLine("</Result>");
      stringBuilder.AppendLine("</SensorRestAPI>");
      context.HttpContext.Response.Output.Write(stringBuilder.ToString());
    }
    else
      new XmlSerializer(this.objectToSerialize.GetType()).Serialize(context.HttpContext.Response.Output, this.objectToSerialize);
  }
}
