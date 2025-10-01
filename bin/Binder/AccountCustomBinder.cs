// Decompiled with JetBrains decompiler
// Type: iMonnit.Binder.AntiXssEncoderCustomBinder
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Security.AntiXss;

#nullable disable
namespace iMonnit.Binder;

public class AntiXssEncoderCustomBinder : DefaultModelBinder
{
  private string[] removeVal = new string[7]
  {
    "<",
    "/>",
    ">",
    "\"",
    "'",
    "\\",
    "/"
  };

  public override object BindModel(
    ControllerContext controllerContext,
    ModelBindingContext bindingContext)
  {
    object obj = base.BindModel(controllerContext, bindingContext);
    if (bindingContext.ModelType == typeof (string) && bindingContext.ModelMetadata.ContainerType != (Type) null)
    {
      PropertyInfo propertyInfo = ((IEnumerable<PropertyInfo>) bindingContext.ModelMetadata.ContainerType.GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>) (x => x.Name == bindingContext.ModelMetadata.PropertyName)).FirstOrDefault<PropertyInfo>();
      bool flag1 = propertyInfo != (PropertyInfo) null && ((IEnumerable<object>) propertyInfo.GetCustomAttributes(true)).Where<object>((Func<object, bool>) (x => x.GetType() == typeof (AllowHtmlAttribute))).Count<object>() > 0;
      bool flag2 = propertyInfo != (PropertyInfo) null && ((IEnumerable<object>) propertyInfo.GetCustomAttributes(true)).Where<object>((Func<object, bool>) (x => x.GetType() == typeof (NoScanHtmlPropertyAttribute))).Count<object>() > 0;
      if (!flag1)
      {
        if (!flag2)
          obj = (object) this.SanitizeHtmlBeforeEncoding((string) obj);
        obj = (object) AntiXssEncoder.HtmlEncode((string) obj, false);
      }
      else if (!flag2)
        obj = (object) this.SanitizeButAllowSomeHTMLBeforeEncoding((string) obj);
      if (obj.ToString().HasScriptTag())
        return (object) null;
    }
    return obj;
  }

  private string SanitizeHtmlBeforeEncoding(string text) => text.SanitizeHTMLEvent();

  private string SanitizeButAllowSomeHTMLBeforeEncoding(string text)
  {
    return text.SanitizeButAllowSomeHTMLEvent();
  }

  private bool HasScriptTag(string text) => text.HasScriptTag();

  private string removeValue(string text)
  {
    for (int index = 0; index < this.removeVal.Length; ++index)
      text = text.Replace(this.removeVal[index], "").Trim();
    return text;
  }
}
