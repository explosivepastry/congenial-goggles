// Decompiled with JetBrains decompiler
// Type: iMonnit.MonnitViewEngine
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;

#nullable disable
namespace iMonnit;

public class MonnitViewEngine : WebFormViewEngine
{
  private static readonly string[] _emptyLocations;

  public static string[] MasterLocations
  {
    get
    {
      return new string[4]
      {
        "~/Themes/{2}/{3}Views/{1}/{0}.master",
        "~/Themes/{2}/{3}Views/Shared/{0}.master",
        "~/{3}Views/{1}/{0}.master",
        "~/{3}Views/Shared/{0}.master"
      };
    }
  }

  public static string[] ViewLocations
  {
    get
    {
      return new string[8]
      {
        "~/Themes/{2}/{3}Views/{1}/{0}.aspx",
        "~/Themes/{2}/{3}Views/{1}/{0}.ascx",
        "~/Themes/{2}/{3}Views/Shared/{0}.aspx",
        "~/Themes/{2}/{3}Views/Shared/{0}.ascx",
        "~/{3}Views/{1}/{0}.aspx",
        "~/{3}Views/{1}/{0}.ascx",
        "~/{3}Views/Shared/{0}.aspx",
        "~/{3}Views/Shared/{0}.ascx"
      };
    }
  }

  public static string[] PartialViewLocations
  {
    get
    {
      return new string[8]
      {
        "~/Themes/{2}/{3}Views/{1}/{0}.aspx",
        "~/Themes/{2}/{3}Views/{1}/{0}.ascx",
        "~/Themes/{2}/{3}Views/Shared/{0}.aspx",
        "~/Themes/{2}/{3}Views/Shared/{0}.ascx",
        "~/{3}Views/{1}/{0}.aspx",
        "~/{3}Views/{1}/{0}.ascx",
        "~/{3}Views/Shared/{0}.aspx",
        "~/{3}Views/Shared/{0}.ascx"
      };
    }
  }

  public MonnitViewEngine()
  {
    this.MasterLocationFormats = MonnitViewEngine.MasterLocations;
    this.ViewLocationFormats = MonnitViewEngine.ViewLocations;
    this.PartialViewLocationFormats = MonnitViewEngine.PartialViewLocations;
  }

  protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
  {
    return MonnitViewEngine.DoesFileExists(controllerContext, virtualPath);
  }

  public override ViewEngineResult FindView(
    ControllerContext controllerContext,
    string viewName,
    string masterName,
    bool useCache)
  {
    if (controllerContext == null)
      throw new ArgumentNullException(nameof (controllerContext));
    AccountTheme accountTheme = AccountTheme.Find(controllerContext.HttpContext.Request.Url.DnsSafeHost);
    if (accountTheme != null && !accountTheme.IsActive)
      return new ViewEngineResult(this.CreateView(controllerContext, "~/Views/Shared/PortalInactive.aspx", ""), (IViewEngine) this);
    if (string.IsNullOrEmpty(viewName))
      throw new ArgumentException("viewName must be specified.", nameof (viewName));
    string currentThemeName = MonnitViewEngine.GetCurrentThemeName(controllerContext);
    string requiredString = controllerContext.RouteData.GetRequiredString("controller");
    string[] searchedLocations1;
    string path1 = this.GetPath(controllerContext, this.ViewLocationFormats, "ViewLocationFormats", viewName, currentThemeName, requiredString, "View", useCache, out searchedLocations1);
    string[] searchedLocations2;
    string path2 = this.GetPath(controllerContext, this.MasterLocationFormats, "MasterLocationFormats", masterName, currentThemeName, requiredString, "Master", useCache, out searchedLocations2);
    if (!string.IsNullOrEmpty(path1) && (!string.IsNullOrEmpty(path2) || string.IsNullOrEmpty(masterName)))
      return new ViewEngineResult(this.CreateView(controllerContext, path1, path2), (IViewEngine) this);
    return searchedLocations2 != null ? new ViewEngineResult(((IEnumerable<string>) searchedLocations1).Union<string>((IEnumerable<string>) searchedLocations2)) : new ViewEngineResult((IEnumerable<string>) searchedLocations1);
  }

  public override ViewEngineResult FindPartialView(
    ControllerContext controllerContext,
    string partialViewName,
    bool useCache)
  {
    if (controllerContext == null)
      throw new ArgumentNullException(nameof (controllerContext));
    if (string.IsNullOrEmpty(partialViewName))
      throw new ArgumentException("partialViewName must be specified.", nameof (partialViewName));
    string currentThemeName = MonnitViewEngine.GetCurrentThemeName(controllerContext);
    string requiredString = controllerContext.RouteData.GetRequiredString("controller");
    string[] searchedLocations;
    string path = this.GetPath(controllerContext, this.PartialViewLocationFormats, "PartialViewLocationFormats", partialViewName, currentThemeName, requiredString, "Partial", useCache, out searchedLocations);
    return string.IsNullOrEmpty(path) ? new ViewEngineResult((IEnumerable<string>) searchedLocations) : new ViewEngineResult(this.CreatePartialView(controllerContext, path), (IViewEngine) this);
  }

  public static bool IsMobileRoute(RouteData routeData)
  {
    return routeData.DataTokens["Namespaces"] is string[] && ((string[]) routeData.DataTokens["Namespaces"])[0].Contains("Mobile");
  }

  public static bool DoesFileExists(ControllerContext controllerContext, string virtualPath)
  {
    try
    {
      return File.Exists(controllerContext.HttpContext.Server.MapPath(virtualPath));
    }
    catch (HttpException ex)
    {
      if (ex.GetHttpCode() == 404)
        return false;
      throw;
    }
    catch
    {
      return false;
    }
  }

  public static bool DoesFileExists(HttpRequest request, string virtualPath)
  {
    try
    {
      return File.Exists(request.RequestContext.HttpContext.Server.MapPath(virtualPath));
    }
    catch (HttpException ex)
    {
      if (ex.GetHttpCode() == 404)
        return false;
      throw;
    }
    catch
    {
      return false;
    }
  }

  public static bool CheckViewExists(
    ControllerContext controllerContext,
    string name,
    string controllerName,
    string theme)
  {
    string str = MonnitViewEngine.IsMobileRoute(controllerContext.RouteData) ? "Mobile/" : "";
    for (int index = 0; index < MonnitViewEngine.ViewLocations.Length; ++index)
    {
      string virtualPath = string.Format((IFormatProvider) CultureInfo.InvariantCulture, MonnitViewEngine.ViewLocations[index], (object) name, (object) controllerName, (object) theme, (object) str);
      if (MonnitViewEngine.DoesFileExists(controllerContext, virtualPath))
        return true;
    }
    return false;
  }

  public static bool CheckPartialViewExists(
    ControllerContext controllerContext,
    string name,
    string controllerName,
    string theme)
  {
    string str = MonnitViewEngine.IsMobileRoute(controllerContext.RouteData) ? "Mobile/" : "";
    for (int index = 0; index < MonnitViewEngine.PartialViewLocations.Length; ++index)
    {
      string virtualPath = string.Format((IFormatProvider) CultureInfo.InvariantCulture, MonnitViewEngine.PartialViewLocations[index], (object) name, (object) controllerName, (object) theme, (object) str);
      if (MonnitViewEngine.DoesFileExists(controllerContext, virtualPath))
        return true;
    }
    return false;
  }

  public static string FindViewPath(
    ControllerContext controllerContext,
    string name,
    string controllerName,
    string theme)
  {
    for (int index = 0; index < MonnitViewEngine.PartialViewLocations.Length; ++index)
    {
      string virtualPath = string.Format((IFormatProvider) CultureInfo.InvariantCulture, MonnitViewEngine.PartialViewLocations[index], (object) name, (object) controllerName, (object) theme, (object) "");
      if (MonnitViewEngine.DoesFileExists(controllerContext, virtualPath))
        return virtualPath;
    }
    return "";
  }

  public static bool CheckPartialViewExists(
    HttpRequest request,
    string name,
    string controllerName,
    string theme)
  {
    try
    {
      string str = MonnitViewEngine.IsMobileRoute(request.RequestContext.RouteData) ? "Mobile/" : "";
      for (int index = 0; index < MonnitViewEngine.PartialViewLocations.Length; ++index)
      {
        string virtualPath = string.Format((IFormatProvider) CultureInfo.InvariantCulture, MonnitViewEngine.PartialViewLocations[index], (object) name, (object) controllerName, (object) theme, (object) str);
        if (MonnitViewEngine.DoesFileExists(request, virtualPath))
          return true;
      }
      return false;
    }
    catch (Exception ex)
    {
      ex.Log($"CheckPartialViewExists(request = {request}, name = {name}, controllerName = {controllerName}, theme = {theme}");
      return false;
    }
  }

  public static string GetCurrentThemeName(RequestContext requestContext)
  {
    return MonnitViewEngine.GetCurrentThemeName(requestContext.HttpContext.Request.Url.DnsSafeHost);
  }

  public static string GetCurrentThemeName(ControllerContext controllerContext)
  {
    return MonnitViewEngine.GetCurrentThemeName(controllerContext.HttpContext.Request.Url.DnsSafeHost);
  }

  public static string GetCurrentThemeName(HttpContextBase httpContext)
  {
    return MonnitViewEngine.GetCurrentThemeName(httpContext.Request.Url.DnsSafeHost);
  }

  public static string GetCurrentThemeName(HttpContext httpContext)
  {
    return MonnitViewEngine.GetCurrentThemeName(httpContext.Request.Url.DnsSafeHost);
  }

  public static string GetCurrentThemeName(string host)
  {
    if (!string.IsNullOrWhiteSpace(ConfigData.AppSettings("HardTheme")))
      return ConfigData.AppSettings("HardTheme");
    AccountTheme accountTheme = AccountTheme.Find(host);
    return accountTheme == null ? "Default" : accountTheme.Theme;
  }

  public static string GetBestViewPath(
    string controller,
    string view,
    bool isPartial,
    bool isMobile,
    string theme)
  {
    string str1 = isPartial ? "ascx" : "aspx";
    string str2 = isMobile ? "Mobile/" : "";
    string virtualPath1 = string.Format("~/Themes/{3}/{4}Views/{0}/{1}.{2}", (object) controller, (object) view, (object) str1, (object) theme, (object) str2);
    if (File.Exists(HostingEnvironment.MapPath(virtualPath1)))
      return virtualPath1;
    string virtualPath2 = string.Format("~/Themes/{3}/{4}Views/Shared/{1}.{2}", (object) controller, (object) view, (object) str1, (object) theme, (object) str2);
    if (File.Exists(HostingEnvironment.MapPath(virtualPath2)))
      return virtualPath2;
    string virtualPath3 = string.Format("~/{4}Views/{0}/{1}.{2}", (object) controller, (object) view, (object) str1, (object) theme, (object) str2);
    if (File.Exists(HostingEnvironment.MapPath(virtualPath3)))
      return virtualPath3;
    string virtualPath4 = string.Format("~/{4}Views/Shared/{1}.{2}", (object) controller, (object) view, (object) str1, (object) theme, (object) str2);
    return File.Exists(HostingEnvironment.MapPath(virtualPath4)) ? virtualPath4 : (string) null;
  }

  private string GetPath(
    ControllerContext controllerContext,
    string[] locations,
    string locationsPropertyName,
    string name,
    string themeName,
    string controllerName,
    string cacheKeyPrefix,
    bool useCache,
    out string[] searchedLocations)
  {
    searchedLocations = MonnitViewEngine._emptyLocations;
    if (string.IsNullOrEmpty(name))
      return string.Empty;
    if (locations == null || locations.Length == 0)
      throw new InvalidOperationException("locations must not be null or empty.");
    bool isMobile = MonnitViewEngine.IsMobileRoute(controllerContext.RouteData);
    bool flag = MonnitViewEngine.IsSpecificPath(name);
    string cacheKey = this.CreateCacheKey(cacheKeyPrefix, name, flag ? string.Empty : controllerName, themeName, isMobile);
    if (useCache)
    {
      string viewLocation = this.ViewLocationCache.GetViewLocation(controllerContext.HttpContext, cacheKey);
      if (viewLocation != null)
        return viewLocation;
    }
    return !flag ? this.GetPathFromGeneralName(controllerContext, locations, name, controllerName, themeName, isMobile, cacheKey, ref searchedLocations) : this.GetPathFromSpecificName(controllerContext, name, cacheKey, ref searchedLocations);
  }

  private static bool IsSpecificPath(string name)
  {
    char ch = name[0];
    return ch == '~' || ch == '/';
  }

  private string CreateCacheKey(
    string prefix,
    string name,
    string controllerName,
    string themeName,
    bool isMobile)
  {
    return string.Format((IFormatProvider) CultureInfo.InvariantCulture, ":ViewCacheEntry:{0}:{1}:{2}:{3}:{4}:{5}", (object) this.GetType().AssemblyQualifiedName, (object) prefix, (object) name, (object) controllerName, (object) themeName, (object) isMobile);
  }

  private string GetPathFromGeneralName(
    ControllerContext controllerContext,
    string[] locations,
    string name,
    string controllerName,
    string themeName,
    bool isMobile,
    string cacheKey,
    ref string[] searchedLocations)
  {
    string empty = string.Empty;
    searchedLocations = new string[locations.Length];
    for (int index = 0; index < locations.Length; ++index)
    {
      string virtualPath1 = string.Format((IFormatProvider) CultureInfo.InvariantCulture, locations[index], (object) name, (object) controllerName, (object) themeName, isMobile ? (object) "Mobile/" : (object) "");
      if (this.FileExists(controllerContext, virtualPath1))
      {
        searchedLocations = MonnitViewEngine._emptyLocations;
        string virtualPath2 = virtualPath1;
        this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, virtualPath2);
        return virtualPath2;
      }
      searchedLocations[index] = virtualPath1;
    }
    return empty;
  }

  private string GetPathFromSpecificName(
    ControllerContext controllerContext,
    string name,
    string cacheKey,
    ref string[] searchedLocations)
  {
    string virtualPath = name;
    if (!this.FileExists(controllerContext, name))
    {
      virtualPath = string.Empty;
      searchedLocations = new string[1]{ name };
    }
    this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, virtualPath);
    return virtualPath;
  }
}
