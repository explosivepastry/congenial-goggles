// Decompiled with JetBrains decompiler
// Type: iMonnit.MonnitControllerFactory
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

#nullable disable
namespace iMonnit;

public class MonnitControllerFactory : DefaultControllerFactory
{
  protected override Type GetControllerType(RequestContext requestContext, string controllerName)
  {
    Type controllerType = base.GetControllerType(requestContext, controllerName);
    if (controllerType == (Type) null)
    {
      string currentThemeName = MonnitViewEngine.GetCurrentThemeName(requestContext);
      string path = requestContext.HttpContext.Server.MapPath($"~/Themes/{currentThemeName}/bin");
      if (Directory.Exists(path))
      {
        foreach (string file in Directory.GetFiles(path, "*.dll"))
        {
          foreach (Type type in Assembly.Load(File.ReadAllBytes(file)).GetTypes())
          {
            if (type.Name.ToLower() == controllerName.ToLower() || type.Name.ToLower() == controllerName.ToLower() + "controller")
              return type;
          }
        }
      }
    }
    return controllerType;
  }
}
