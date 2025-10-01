// Decompiled with JetBrains decompiler
// Type: MvcApplication1.FilterConfig
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System.Web.Mvc;

#nullable disable
namespace MvcApplication1;

public class FilterConfig
{
  public static void RegisterGlobalFilters(GlobalFilterCollection filters)
  {
    filters.Add((object) new HandleErrorAttribute());
  }
}
