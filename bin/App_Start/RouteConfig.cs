// Decompiled with JetBrains decompiler
// Type: MvcApplication1.RouteConfig
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System.Web.Mvc;
using System.Web.Routing;

#nullable disable
namespace MvcApplication1;

public class RouteConfig
{
  public static void RegisterRoutes(RouteCollection routes)
  {
    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
    RouteCollectionExtensions.MapRoute(routes, "PWAManifest", "pwamanifest", (object) new
    {
      controller = "Setup",
      action = "PWAManifest"
    }, new string[1]{ "iMonnit.Controllers" });
    RouteCollectionExtensions.MapRoute(routes, "PWAIcon", "pwaicon/{image}", (object) new
    {
      controller = "Setup",
      action = "PWAIcon",
      image = "1024x1024"
    }, new string[1]{ "iMonnit.Controllers" });
    RouteCollectionExtensions.MapRoute(routes, "Report", "reportfile/{guid}/{ScheduledReportsToStorageID}", (object) new
    {
      controller = "Report",
      action = "GetReportFile",
      guid = string.Empty,
      ScheduledReportsToStorageID = long.MinValue
    }, new string[1]{ "iMonnit.Controllers" });
    RouteCollectionExtensions.MapRoute(routes, "GatewaySettingInfo", "point", (object) new
    {
      controller = "Gateway",
      action = "ServerSettings"
    }, new string[1]{ "iMonnit.Controllers" });
    RouteCollectionExtensions.MapRoute(routes, "DefaultGatewaySettingInfo", "sethost", (object) new
    {
      controller = "Gateway",
      action = "ServerSettings"
    }, new string[1]{ "iMonnit.Controllers" });
    RouteCollectionExtensions.MapRoute(routes, "Lookup", "lookup", (object) new
    {
      controller = "Overview",
      action = "DeviceInfo"
    }, new string[1]{ "iMonnit.Controllers" });
    RouteCollectionExtensions.MapRoute(routes, "DeviceInfoLookup", "DeviceInfo", (object) new
    {
      controller = "Overview",
      action = "DeviceInfo"
    }, new string[1]{ "iMonnit.Controllers" });
    RouteCollectionExtensions.MapRoute(routes, "QRLookup", "QR/{id}/{code}", (object) new
    {
      controller = "Setup",
      action = "DeviceInfo",
      id = UrlParameter.Optional,
      code = UrlParameter.Optional
    }, new string[1]{ "iMonnit.Controllers" });
    RouteCollectionExtensions.MapRoute(routes, "ProductActivation", "productactivation", (object) new
    {
      controller = "Lookup",
      action = "ProductActivation"
    }, new string[1]{ "iMonnit.Controllers" });
    RouteCollectionExtensions.MapRoute(routes, "ThemeView", "Data/{view}/{id}", (object) new
    {
      controller = "Custom",
      action = "ThemeView",
      view = UrlParameter.Optional,
      id = UrlParameter.Optional
    }, new string[1]{ "iMonnit.Controllers" });
    RouteCollectionExtensions.MapRoute(routes, "JsonAPI", "json/{action}/{auth}", (object) new
    {
      controller = "API",
      action = "Logon",
      response = "json",
      auth = ""
    }, new string[1]{ "iMonnit.Controllers" });
    RouteCollectionExtensions.MapRoute(routes, "XmlAPI", "xml/{action}/{auth}", (object) new
    {
      controller = "API",
      action = "Logon",
      response = "xml",
      auth = ""
    }, new string[1]{ "iMonnit.Controllers" });
    RouteCollectionExtensions.MapRoute(routes, "Acknowledge", "Ack/{id}/{guid}", (object) new
    {
      controller = "Notification",
      action = "Ack",
      id = UrlParameter.Optional,
      guid = UrlParameter.Optional
    }, new string[1]{ "iMonnit.Controllers" });
    RouteCollectionExtensions.MapRoute(routes, "APIversion2", "APIv2/{response}/{controller}/{action}/", (object) new
    {
      controller = "APIv2",
      action = "Index",
      response = "xml"
    }, new string[1]{ "iMonnit.APIControllers" });
    RouteCollectionExtensions.MapRoute(routes, "Default", "{controller}/{action}/{id}", (object) new
    {
      controller = "Overview",
      action = "Index",
      id = UrlParameter.Optional,
      language = UrlParameter.Optional
    }, new string[1]{ "iMonnit.Controllers" });
  }
}
