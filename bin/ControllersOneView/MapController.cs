// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.MapController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.ControllerBase;
using iMonnit.Models;
using Microsoft.CSharp.RuntimeBinder;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

public class MapController : ThemeController
{
  [AuthorizeDefault]
  public ActionResult Index()
  {
    if (!MonnitSession.CustomerCan("Navigation_View_Maps"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Maps",
        ErrorTranslateTag = "Map/Index|",
        ErrorTitle = "Unauthorized access to maps",
        ErrorMessage = "You do not have permission to access this page."
      });
    if (MonnitSession.CurrentVisualMap != null && MonnitSession.CurrentVisualMap.AccountID != MonnitSession.CurrentCustomer.AccountID)
      MonnitSession.CurrentVisualMap = (VisualMap) null;
    List<VisualMap> model = VisualMap.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
    this.ViewData["Networks"] = (object) new SelectList((IEnumerable) CSNetControllerBase.GetListOfNetworksUserCanSee(new long?()), "CSNetID", "Name");
    this.ViewData["SensorList"] = (object) MapController.GetSensorList(new long?());
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult NewMap()
  {
    if (MonnitSession.CustomerCan("Navigation_View_Maps") && MonnitSession.AccountCan("view_maps"))
      return (ActionResult) this.View((object) new VisualMap());
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
    {
      ErrorLocation = "Maps",
      ErrorTranslateTag = "Map/Index|",
      ErrorTitle = "Unauthorized access to maps",
      ErrorMessage = "You do not have permission to access this page."
    });
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult NewMap(FormCollection collection)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_Maps") || !MonnitSession.AccountCan("view_maps"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Maps",
        ErrorTranslateTag = "Map/Index|",
        ErrorTitle = "Unauthorized access to maps",
        ErrorMessage = "You do not have permission to access this page."
      });
    VisualMap model = new VisualMap();
    model.AccountID = MonnitSession.CurrentCustomer.AccountID;
    model.Name = collection["Name"];
    if (string.IsNullOrEmpty(model.Name))
      this.ModelState.AddModelError("Name", "Name is required");
    model.MapType = (eMapType) Enum.Parse(typeof (eMapType), collection["mapType"]);
    if (model.MapType == eMapType.StaticMap)
    {
      HttpPostedFileBase file = this.Request.Files["ImageFile"];
      if (file == null || file.FileName.Length == 0 || file.ContentLength == 0)
      {
        this.ModelState.AddModelError("Image", "Image file not found");
      }
      else
      {
        try
        {
          model.Bitmap = this.GetBitmap(file.InputStream);
        }
        catch
        {
          this.ModelState.AddModelError("Image", "Image file type not compatible");
        }
      }
    }
    if (!this.ModelState.IsValid)
      return (ActionResult) this.View((object) model);
    model.Save();
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "/DevicesToShow/",
      controller = "Map",
      id = model.VisualMapID
    });
  }

  public ActionResult EditMap(long? id)
  {
    if (!MonnitSession.CustomerCan("Visual_Map_Edit") || !MonnitSession.AccountCan("view_maps"))
      return this.MapPermissionError();
    long visualMapID = id ?? long.MinValue;
    if (visualMapID < 0L)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "/Index",
        controller = "Map"
      });
    if (MonnitSession.CurrentVisualMap == null || MonnitSession.CurrentVisualMap.VisualMapID != visualMapID)
    {
      MonnitSession.CurrentVisualMap = VisualMap.Load(visualMapID);
      this.ViewData["VisualMapID"] = (object) null;
      this.ViewData["GatewayList"] = (object) null;
      this.ViewData["SensorList"] = (object) null;
    }
    VisualMap currentVisualMap = MonnitSession.CurrentVisualMap;
    this.ViewData["VisualMapID"] = (object) currentVisualMap.VisualMapID;
    if (currentVisualMap == null || !MonnitSession.CurrentCustomer.CanSeeAccount(currentVisualMap.AccountID))
      return this.MapPermissionError();
    // ISSUE: reference to a compiler-generated field
    if (MapController.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MapController.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "GatewayList", typeof (MapController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = MapController.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) MapController.\u003C\u003Eo__3.\u003C\u003Ep__0, this.ViewBag, this.ViewData["GatewayList"] ?? (object) CSNetControllerBase.GetGatewayList().Select<Gateway, Gateway>((Func<Gateway, Gateway>) (g =>
    {
      g.SuppressPropertyChangedEvent = false;
      return g;
    })).ToList<Gateway>());
    // ISSUE: reference to a compiler-generated field
    if (MapController.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MapController.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SensorList", typeof (MapController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = MapController.\u003C\u003Eo__3.\u003C\u003Ep__1.Target((CallSite) MapController.\u003C\u003Eo__3.\u003C\u003Ep__1, this.ViewBag, this.ViewData["SensorList"] ?? (object) SensorControllerBase.GetSensorList(MonnitSession.CurrentCustomer.AccountID).Select<SensorGroupSensorModel, Sensor>((Func<SensorGroupSensorModel, Sensor>) (sgsm => sgsm.Sensor)).ToList<Sensor>());
    return (ActionResult) this.View((object) currentVisualMap);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult EditMap(long id, FormCollection collection)
  {
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Visual_Map_Edit")) || !MonnitSession.AccountCan("view_maps"))
      return (ActionResult) this.Content("Not Authorized to Edit Visual Map");
    VisualMap model = VisualMap.Load(id);
    if (string.IsNullOrEmpty(collection["Name"]))
      this.ModelState.AddModelError("Name", "Name is Required");
    if (collection["Name"] != model.Name)
      model.Name = collection["Name"];
    if (model.MapType == eMapType.StaticMap)
    {
      HttpPostedFileBase file = this.Request.Files["ImageFile"];
      if (file.FileName.Length != 0 && file.ContentLength != 0)
      {
        try
        {
          model.Bitmap = this.GetBitmap(file.InputStream);
        }
        catch
        {
          this.ModelState.AddModelError("Image", "Image file type not compatible");
        }
      }
    }
    if (this.ModelState.IsValid)
    {
      model.Name = collection["Name"];
      if (model.MapType == eMapType.StaticMap)
      {
        HttpPostedFileBase file = this.Request.Files["ImageFile"];
        if (file.FileName != "")
          model.Bitmap = this.GetBitmap(file.InputStream);
      }
      model.Save();
      MonnitSession.CurrentVisualMap = model;
    }
    return (ActionResult) this.View((object) model);
  }

  private ActionResult MapPermissionError() => this.MapPermissionError((string) null);

  private ActionResult MapPermissionError(string errMsg)
  {
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
    {
      ErrorLocation = "Maps",
      ErrorTranslateTag = "Map/MapIndex|",
      ErrorTitle = "Unauthorized access to maps",
      ErrorMessage = (errMsg ?? "You do not have permission to access this page.")
    });
  }

  [AuthorizeDefault]
  public ActionResult DevicesToShow(long? id)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_Maps") || !MonnitSession.CustomerCan("Visual_Map_Edit") || !MonnitSession.AccountCan("view_maps"))
      return this.MapPermissionError();
    ActionResult route = (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "/Index",
      controller = "Map"
    });
    long visualMapID = id ?? long.MinValue;
    if (visualMapID < 0L)
      return route;
    if (MonnitSession.CurrentVisualMap == null || MonnitSession.CurrentVisualMap.VisualMapID != visualMapID)
    {
      MonnitSession.CurrentVisualMap = VisualMap.Load(visualMapID);
      this.ViewData["VisualMapID"] = (object) null;
      this.ViewData["GatewayList"] = (object) null;
      this.ViewData["SensorList"] = (object) null;
    }
    VisualMap currentVisualMap = MonnitSession.CurrentVisualMap;
    this.ViewData["VisualMapID"] = (object) currentVisualMap.VisualMapID;
    if (currentVisualMap == null || !MonnitSession.CurrentCustomer.CanSeeAccount(currentVisualMap.AccountID))
      return this.MapPermissionError();
    MonnitSession.SensorListFilters.Clear();
    MonnitSession.GatewayListFilters.Clear();
    // ISSUE: reference to a compiler-generated field
    if (MapController.\u003C\u003Eo__7.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MapController.\u003C\u003Eo__7.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "GatewayList", typeof (MapController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = MapController.\u003C\u003Eo__7.\u003C\u003Ep__0.Target((CallSite) MapController.\u003C\u003Eo__7.\u003C\u003Ep__0, this.ViewBag, this.ViewData["GatewayList"] ?? (object) CSNetControllerBase.GetGatewayList().Select<Gateway, Gateway>((Func<Gateway, Gateway>) (g =>
    {
      g.SuppressPropertyChangedEvent = false;
      return g;
    })).ToList<Gateway>());
    // ISSUE: reference to a compiler-generated field
    if (MapController.\u003C\u003Eo__7.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MapController.\u003C\u003Eo__7.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SensorList", typeof (MapController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = MapController.\u003C\u003Eo__7.\u003C\u003Ep__1.Target((CallSite) MapController.\u003C\u003Eo__7.\u003C\u003Ep__1, this.ViewBag, this.ViewData["SensorList"] ?? (object) SensorControllerBase.GetSensorList(MonnitSession.CurrentCustomer.AccountID).Select<SensorGroupSensorModel, Sensor>((Func<SensorGroupSensorModel, Sensor>) (sgsm => sgsm.Sensor)).ToList<Sensor>());
    // ISSUE: reference to a compiler-generated field
    if (MapController.\u003C\u003Eo__7.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MapController.\u003C\u003Eo__7.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ShowSensors", typeof (MapController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = MapController.\u003C\u003Eo__7.\u003C\u003Ep__2.Target((CallSite) MapController.\u003C\u003Eo__7.\u003C\u003Ep__2, this.ViewBag, true);
    // ISSUE: reference to a compiler-generated field
    if (MapController.\u003C\u003Eo__7.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MapController.\u003C\u003Eo__7.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ShowGateways", typeof (MapController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj4 = MapController.\u003C\u003Eo__7.\u003C\u003Ep__3.Target((CallSite) MapController.\u003C\u003Eo__7.\u003C\u003Ep__3, this.ViewBag, true);
    return currentVisualMap.MapType != eMapType.GpsMap ? (ActionResult) this.View("DevicesToShowStatic", (object) currentVisualMap) : (ActionResult) this.View("DevicesToShowGPS", (object) currentVisualMap);
  }

  [AuthorizeDefault]
  public ActionResult DeleteMap(long id)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_Maps") || !MonnitSession.CustomerCan("Visual_Map_Edit") || !MonnitSession.AccountCan("view_maps"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Maps",
        ErrorTranslateTag = "Map/Index|",
        ErrorTitle = "Unauthorized access to maps",
        ErrorMessage = "You do not have permission to access this page."
      });
    VisualMap DBObject = VisualMap.Load(id);
    try
    {
      if (MonnitSession.IsVisualMapFavorite(id))
      {
        MonnitSession.CurrentCustomerFavorites.Invalidate();
        VisualMap.DeleteByMapID(id);
        DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Map, MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID, "Deleted Map and all associated customer favorites");
        return (ActionResult) this.Content("Success");
      }
      VisualMap.DeleteByMapID(id);
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content("Failed to delete map.");
    }
  }

  [AuthorizeDefault]
  public ActionResult ViewMap(long? id)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_Maps") || !MonnitSession.AccountCan("view_maps"))
      return this.MapPermissionError();
    ActionResult route = (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "/Index",
      controller = "Map"
    });
    long visualMapID = id ?? long.MinValue;
    if (visualMapID < 0L)
      return route;
    if (MonnitSession.CurrentVisualMap == null || MonnitSession.CurrentVisualMap.VisualMapID != visualMapID)
    {
      MonnitSession.CurrentVisualMap = VisualMap.Load(visualMapID);
      this.ViewData["VisualMapID"] = (object) null;
      this.ViewData["GatewayList"] = (object) null;
      this.ViewData["SensorList"] = (object) null;
    }
    VisualMap currentVisualMap = MonnitSession.CurrentVisualMap;
    this.ViewData["VisualMapID"] = (object) currentVisualMap.VisualMapID;
    if (currentVisualMap == null || !MonnitSession.CurrentCustomer.CanSeeAccount(currentVisualMap.AccountID))
      return this.MapPermissionError();
    MonnitSession.SensorListFilters.Clear();
    MonnitSession.GatewayListFilters.Clear();
    // ISSUE: reference to a compiler-generated field
    if (MapController.\u003C\u003Eo__9.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MapController.\u003C\u003Eo__9.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "GatewayList", typeof (MapController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = MapController.\u003C\u003Eo__9.\u003C\u003Ep__0.Target((CallSite) MapController.\u003C\u003Eo__9.\u003C\u003Ep__0, this.ViewBag, this.ViewData["GatewayList"] ?? (object) CSNetControllerBase.GetGatewayList().Select<Gateway, Gateway>((Func<Gateway, Gateway>) (g =>
    {
      g.SuppressPropertyChangedEvent = false;
      return g;
    })).ToList<Gateway>());
    // ISSUE: reference to a compiler-generated field
    if (MapController.\u003C\u003Eo__9.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MapController.\u003C\u003Eo__9.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SensorList", typeof (MapController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = MapController.\u003C\u003Eo__9.\u003C\u003Ep__1.Target((CallSite) MapController.\u003C\u003Eo__9.\u003C\u003Ep__1, this.ViewBag, this.ViewData["SensorList"] ?? (object) SensorControllerBase.GetSensorList(MonnitSession.CurrentCustomer.AccountID).Select<SensorGroupSensorModel, Sensor>((Func<SensorGroupSensorModel, Sensor>) (sgsm => sgsm.Sensor)).ToList<Sensor>());
    ActionResult actionResult;
    if (currentVisualMap.MapType == eMapType.GpsMap)
    {
      List<LocationMessage> model = LocationMessage.LoadRecentByVisualMapID(visualMapID);
      this.ViewData["GatewayList"] = (object) Gateway.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
      this.ViewData["VisualMapID"] = (object) visualMapID;
      actionResult = (ActionResult) this.View("ViewMapOfGateways", (object) model);
    }
    else
      actionResult = (ActionResult) this.View((object) currentVisualMap);
    return actionResult;
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult GatewayMapResults(long id)
  {
    List<LocationMessage> model = new List<LocationMessage>();
    try
    {
      Gateway gateway = Gateway.Load(id);
      if (gateway == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID))
        return (ActionResult) this.Content("Invalid DeviceID");
      this.ViewData["Gateway"] = (object) gateway;
      DateTime dateTime;
      int num;
      if (!MonnitSession.IsCurrentCustomerMonnitAdmin && gateway.StartDate != DateTime.MinValue)
      {
        dateTime = gateway.StartDate;
        long ticks1 = dateTime.Ticks;
        dateTime = MonnitSession.HistoryFromDate;
        long ticks2 = dateTime.Ticks;
        num = ticks1 > ticks2 ? 1 : 0;
      }
      else
        num = 0;
      if (num != 0)
        MonnitSession.HistoryFromDate = Monnit.TimeZone.GetLocalTimeById(gateway.StartDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
      if ((MonnitSession.HistoryToDate - MonnitSession.HistoryFromDate).TotalDays > 7.0)
      {
        dateTime = MonnitSession.HistoryToDate;
        MonnitSession.HistoryFromDate = dateTime.AddDays(-7.0);
      }
      else if ((MonnitSession.HistoryToDate - MonnitSession.HistoryFromDate).TotalDays < 0.0)
      {
        dateTime = MonnitSession.HistoryToDate;
        MonnitSession.HistoryFromDate = dateTime.AddDays(-1.0);
      }
      model = LocationMessage.LoadByGatewayIDAndDateRange(gateway.GatewayID, Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID), Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID));
    }
    catch (Exception ex)
    {
      ex.Log($"MapController.GatewayMapResults[Get][GatewayID: {id.ToString()}]");
      this.ViewData["Gateway"] = (object) null;
    }
    return (ActionResult) this.PartialView("_GatewayMapResults", (object) model);
  }

  [AuthorizeDefault]
  public ActionResult MapGatewaysResults(long id)
  {
    List<LocationMessage> model = new List<LocationMessage>();
    try
    {
      if (id > 0L)
      {
        model = LocationMessage.LoadRecentByVisualMapID(id);
        this.ViewData["VisualMapID"] = (object) id;
      }
    }
    catch (Exception ex)
    {
      ex.Log($"MapController.MapGatewaysResults[Get][VisualMapID: {id.ToString()}] ");
    }
    return (ActionResult) this.PartialView("_MapGatewaysResults", (object) model);
  }

  [AuthorizeDefault]
  [NoCache]
  public ActionResult SensorList(long? id)
  {
    this.ViewData[nameof (SensorList)] = (object) MapController.GetSensorList(id);
    return (ActionResult) this.View("_SensorList", (object) MonnitSession.CurrentVisualMap);
  }

  [AuthorizeDefault]
  [NoCache]
  public PartialViewResult SensorFilters(long id)
  {
    VisualMap model = VisualMap.Load(id);
    // ISSUE: reference to a compiler-generated field
    if (MapController.\u003C\u003Eo__13.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MapController.\u003C\u003Eo__13.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<ApplicationTypeShort>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SensorTypes", typeof (MapController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = MapController.\u003C\u003Eo__13.\u003C\u003Ep__0.Target((CallSite) MapController.\u003C\u003Eo__13.\u003C\u003Ep__0, this.ViewBag, ApplicationTypeShort.LoadAllByAccountID(MonnitSession.CurrentCustomer.AccountID));
    // ISSUE: reference to a compiler-generated field
    if (MapController.\u003C\u003Eo__13.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MapController.\u003C\u003Eo__13.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<CSNet>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Networks", typeof (MapController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = MapController.\u003C\u003Eo__13.\u003C\u003Ep__1.Target((CallSite) MapController.\u003C\u003Eo__13.\u003C\u003Ep__1, this.ViewBag, CSNetControllerBase.GetListOfNetworksUserCanSee(new long?(MonnitSession.CurrentCustomer.AccountID)));
    return this.PartialView("~/Views/Map/_SensorFilters.ascx", (object) model);
  }

  [AuthorizeDefault]
  [NoCache]
  public PartialViewResult FilteredSensors(long id)
  {
    VisualMap currentVisualMap = MonnitSession.CurrentVisualMap;
    Dictionary<long, VisualMapSensor> dictionary1 = new Dictionary<long, VisualMapSensor>();
    int totalSensors;
    Dictionary<long, Sensor> dictionary2 = SensorControllerBase.GetSensorList(MonnitSession.CurrentCustomer.AccountID, out totalSensors).ToDictionary<SensorGroupSensorModel, long, Sensor>((Func<SensorGroupSensorModel, long>) (sgsm => sgsm.Sensor.SensorID), (Func<SensorGroupSensorModel, Sensor>) (sgsm => sgsm.Sensor));
    List<VisualMapSensor> model = new List<VisualMapSensor>();
    foreach (long key in dictionary2.Keys)
    {
      if (!currentVisualMap.PlacedSensors.ContainsKey(key))
      {
        Sensor sensor = dictionary2[key];
        model.Add(new VisualMapSensor()
        {
          SensorID = sensor.SensorID,
          Sensor = sensor,
          Selected = false
        });
      }
      else
        model.Add(currentVisualMap.PlacedSensors[key]);
    }
    // ISSUE: reference to a compiler-generated field
    if (MapController.\u003C\u003Eo__14.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MapController.\u003C\u003Eo__14.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "FilteredSensorsCount", typeof (MapController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = MapController.\u003C\u003Eo__14.\u003C\u003Ep__0.Target((CallSite) MapController.\u003C\u003Eo__14.\u003C\u003Ep__0, this.ViewBag, model.Count);
    // ISSUE: reference to a compiler-generated field
    if (MapController.\u003C\u003Eo__14.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MapController.\u003C\u003Eo__14.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalSensorsCount", typeof (MapController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = MapController.\u003C\u003Eo__14.\u003C\u003Ep__1.Target((CallSite) MapController.\u003C\u003Eo__14.\u003C\u003Ep__1, this.ViewBag, totalSensors);
    // ISSUE: reference to a compiler-generated field
    if (MapController.\u003C\u003Eo__14.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MapController.\u003C\u003Eo__14.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedSensorsCount", typeof (MapController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = MapController.\u003C\u003Eo__14.\u003C\u003Ep__2.Target((CallSite) MapController.\u003C\u003Eo__14.\u003C\u003Ep__2, this.ViewBag, currentVisualMap.PlacedSensors.Count);
    return this.PartialView("~/Views/Map/_FilteredSensors.ascx", (object) model);
  }

  [AuthorizeDefault]
  [NoCache]
  public PartialViewResult GatewayFilters(long id)
  {
    VisualMap model = VisualMap.Load(id);
    List<Gateway> gatewayList = CSNetController.GetGatewayList();
    // ISSUE: reference to a compiler-generated field
    if (MapController.\u003C\u003Eo__15.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MapController.\u003C\u003Eo__15.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, IEnumerable<GatewayType>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "GatewayTypes", typeof (MapController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = MapController.\u003C\u003Eo__15.\u003C\u003Ep__0.Target((CallSite) MapController.\u003C\u003Eo__15.\u003C\u003Ep__0, this.ViewBag, gatewayList.Select<Gateway, GatewayType>((Func<Gateway, GatewayType>) (gl => GatewayType.Load(gl.GatewayTypeID))).Distinct<GatewayType>());
    return this.PartialView("~/Views/Map/_GatewayFilters.ascx", (object) model);
  }

  [AuthorizeDefault]
  [NoCache]
  public PartialViewResult FilteredGateways(long id)
  {
    VisualMap currentVisualMap = MonnitSession.CurrentVisualMap;
    Dictionary<long, VisualMapGateway> dictionary1 = new Dictionary<long, VisualMapGateway>();
    int totalGateways;
    Dictionary<long, Gateway> dictionary2 = CSNetControllerBase.GetGatewayList(out totalGateways).ToDictionary<Gateway, long, Gateway>((Func<Gateway, long>) (g => g.GatewayID), (Func<Gateway, Gateway>) (g => g));
    List<VisualMapGateway> model = new List<VisualMapGateway>();
    foreach (long key in dictionary2.Keys)
    {
      if (!currentVisualMap.PlacedGateways.ContainsKey(key))
      {
        Gateway gateway = dictionary2[key];
        model.Add(new VisualMapGateway()
        {
          GatewayID = gateway.GatewayID,
          Gateway = gateway,
          Selected = false
        });
      }
      else
        model.Add(currentVisualMap.PlacedGateways[key]);
    }
    // ISSUE: reference to a compiler-generated field
    if (MapController.\u003C\u003Eo__16.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MapController.\u003C\u003Eo__16.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "FilteredGatewaysCount", typeof (MapController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = MapController.\u003C\u003Eo__16.\u003C\u003Ep__0.Target((CallSite) MapController.\u003C\u003Eo__16.\u003C\u003Ep__0, this.ViewBag, model.Count);
    // ISSUE: reference to a compiler-generated field
    if (MapController.\u003C\u003Eo__16.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MapController.\u003C\u003Eo__16.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalGatewaysCount", typeof (MapController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = MapController.\u003C\u003Eo__16.\u003C\u003Ep__1.Target((CallSite) MapController.\u003C\u003Eo__16.\u003C\u003Ep__1, this.ViewBag, totalGateways);
    // ISSUE: reference to a compiler-generated field
    if (MapController.\u003C\u003Eo__16.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MapController.\u003C\u003Eo__16.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SelectedGatewaysCount", typeof (MapController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = MapController.\u003C\u003Eo__16.\u003C\u003Ep__2.Target((CallSite) MapController.\u003C\u003Eo__16.\u003C\u003Ep__2, this.ViewBag, currentVisualMap.PlacedGateways.Count);
    return this.PartialView("~/Views/Map/_FilteredGateways.ascx", (object) model);
  }

  private static List<Sensor> GetSensorList(long? id)
  {
    return Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where<Sensor>((Func<Sensor, bool>) (s =>
    {
      int num;
      if (MonnitSession.CurrentCustomer.CanSeeSensor(s))
      {
        if (id.HasValue)
        {
          long csNetId = s.CSNetID;
          long? nullable = id;
          long valueOrDefault = nullable.GetValueOrDefault();
          num = csNetId == valueOrDefault & nullable.HasValue ? 1 : 0;
        }
        else
          num = 1;
      }
      else
        num = 0;
      return num != 0;
    })).ToList<Sensor>();
  }

  [AuthorizeDefault]
  [NoCache]
  public ActionResult GpsDeviceList(
    long? accountID,
    long? visualMapID,
    long? csNetID,
    string filterQuery)
  {
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(accountID ?? long.MinValue) || !MonnitSession.CustomerCan("Visual_Map_Edit"))
      return (ActionResult) this.Content("Failed: Not Authorized");
    try
    {
      List<MapDeviceModel> mapDeviceModelList = new List<MapDeviceModel>();
      List<MapDeviceModel> model = BaseDBObject.Load<MapDeviceModel>(VisualMap.LoadGatewaysByAccountID(accountID ?? long.MinValue, visualMapID ?? long.MinValue, true, csNetID ?? long.MinValue, filterQuery));
      this.ViewData["VisualMapID"] = (object) (visualMapID ?? MonnitSession.CurrentVisualMap.VisualMapID);
      this.ViewData[nameof (filterQuery)] = (object) filterQuery.ToStringSafe();
      return (ActionResult) this.PartialView("_GpsDeviceList", (object) model);
    }
    catch
    {
      return (ActionResult) this.Content("Failed: Unable to get device list.");
    }
  }

  [AuthorizeDefault]
  [NoCache]
  public ActionResult ToggleGpsDevice(long deviceId, long mapID, bool isGateway, bool add)
  {
    VisualMap visualMap = VisualMap.Load(mapID);
    if (visualMap == null || visualMap.VisualMapID <= 0L)
      return (ActionResult) this.Content("No Map Found.");
    try
    {
      if (!isGateway)
        return (ActionResult) this.Content("Success");
      Gateway gateway = Gateway.Load(deviceId);
      if (gateway == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID) || !MonnitSession.CustomerCan("Visual_Map_Edit"))
        return (ActionResult) this.Content("Not Authorized");
      if (add)
      {
        VisualMapGateway visualMapGateway;
        if (visualMap.PlacedGateways.ContainsKey(gateway.GatewayID))
        {
          visualMapGateway = visualMap.PlacedGateways[gateway.GatewayID];
        }
        else
        {
          visualMapGateway = new VisualMapGateway();
          visualMapGateway.GatewayID = gateway.GatewayID;
          visualMapGateway.VisualMapID = visualMap.VisualMapID;
          visualMap.PlacedGateways.Add(gateway.GatewayID, visualMapGateway);
        }
        visualMapGateway.Save();
      }
      else
      {
        if (!visualMap.PlacedGateways.ContainsKey(gateway.GatewayID))
          return (ActionResult) this.Content("Device Not Found");
        VisualMapGateway placedGateway = visualMap.PlacedGateways[gateway.GatewayID];
        visualMap.PlacedGateways.Remove(placedGateway.GatewayID);
        placedGateway.Delete();
      }
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  private Bitmap GetBitmap(Stream str)
  {
    Bitmap bitmap1 = (Bitmap) System.Drawing.Image.FromStream(str);
    if (bitmap1.Width * bitmap1.Height < 10000000)
      return bitmap1;
    int width = 10000000 / bitmap1.Height;
    int height = 10000000 / bitmap1.Width;
    using (bitmap1)
    {
      Bitmap bitmap2 = new Bitmap(width, height, PixelFormat.Format32bppArgb);
      using (Graphics graphics = Graphics.FromImage((System.Drawing.Image) bitmap2))
      {
        graphics.Clear(Color.Transparent);
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.DrawImage((System.Drawing.Image) bitmap1, new Rectangle(0, 0, width, height), new Rectangle(0, 0, bitmap1.Width, bitmap1.Height), GraphicsUnit.Pixel);
      }
      return bitmap2;
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Placement(FormCollection collection)
  {
    Sensor sensor = Sensor.Load(collection["id"].ToLong());
    if (!MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.Content("Not Authorized to Add to Visual Search");
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Visual_Map_Edit")))
      return (ActionResult) this.Content("Not Authorized to Add to Visual Search");
    if (MonnitSession.CurrentVisualMap != null && MonnitSession.CurrentVisualMap.VisualMapID > long.MinValue)
    {
      VisualMapSensor visualMapSensor;
      if (MonnitSession.CurrentVisualMap.PlacedSensors.ContainsKey(sensor.SensorID))
      {
        visualMapSensor = MonnitSession.CurrentVisualMap.PlacedSensors[sensor.SensorID];
      }
      else
      {
        visualMapSensor = new VisualMapSensor();
        visualMapSensor.SensorID = sensor.SensorID;
        visualMapSensor.VisualMapID = collection["vmid"].ToLong();
        MonnitSession.CurrentVisualMap.PlacedSensors.Add(sensor.SensorID, visualMapSensor);
      }
      if (!string.IsNullOrEmpty(collection["x"]))
      {
        visualMapSensor.X = collection["x"].ToDouble();
        visualMapSensor.Y = collection["y"].ToDouble();
        visualMapSensor.Width = collection["w"].ToDouble();
        visualMapSensor.Height = collection["h"].ToDouble();
      }
      if (!string.IsNullOrEmpty(collection["r"]))
        visualMapSensor.Rotation = collection["r"];
      if (!string.IsNullOrEmpty(collection["a"]))
        visualMapSensor.Altitude = collection["a"].ToInt();
      visualMapSensor.Save();
    }
    return (ActionResult) this.Content("");
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult PlacementStatic(FormCollection collection)
  {
    long visualMapID = collection["visualMapID"].ToLong();
    if (visualMapID < 0L)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "/Index",
        controller = "Map"
      });
    if (MonnitSession.CurrentVisualMap == null || MonnitSession.CurrentVisualMap.VisualMapID != visualMapID)
    {
      MonnitSession.CurrentVisualMap = VisualMap.Load(visualMapID);
      this.ViewData["VisualMapID"] = (object) null;
    }
    VisualMap currentVisualMap = MonnitSession.CurrentVisualMap;
    this.ViewData["VisualMapID"] = (object) currentVisualMap.VisualMapID;
    try
    {
      switch (collection["deviceType"])
      {
        case "sensor":
          Sensor sensor = Sensor.Load(collection["deviceID"].ToLong());
          VisualMapSensor visualMapSensor;
          if (currentVisualMap.PlacedSensors.ContainsKey(sensor.SensorID))
          {
            visualMapSensor = currentVisualMap.PlacedSensors[sensor.SensorID];
          }
          else
          {
            visualMapSensor = new VisualMapSensor();
            visualMapSensor.SensorID = sensor.SensorID;
            visualMapSensor.VisualMapID = currentVisualMap.VisualMapID;
            currentVisualMap.PlacedSensors.Add(sensor.SensorID, visualMapSensor);
          }
          if (!string.IsNullOrEmpty(collection["x"]))
          {
            visualMapSensor.X = collection["x"].ToDouble();
            visualMapSensor.Y = collection["y"].ToDouble();
            visualMapSensor.Width = collection["w"].ToDouble();
            visualMapSensor.Height = collection["h"].ToDouble();
          }
          if (!string.IsNullOrEmpty(collection["r"]))
            visualMapSensor.Rotation = collection["r"];
          if (!string.IsNullOrEmpty(collection["a"]))
            visualMapSensor.Altitude = collection["a"].ToInt();
          visualMapSensor.Save();
          break;
        case "gateway":
          Gateway gateway = Gateway.Load(collection["deviceID"].ToLong());
          VisualMapGateway visualMapGateway;
          if (currentVisualMap.PlacedGateways.ContainsKey(gateway.GatewayID))
          {
            visualMapGateway = currentVisualMap.PlacedGateways[gateway.GatewayID];
          }
          else
          {
            visualMapGateway = new VisualMapGateway();
            visualMapGateway.GatewayID = gateway.GatewayID;
            visualMapGateway.VisualMapID = currentVisualMap.VisualMapID;
            currentVisualMap.PlacedGateways.Add(gateway.GatewayID, visualMapGateway);
          }
          if (!string.IsNullOrEmpty(collection["x"]))
          {
            visualMapGateway.X = collection["x"].ToDouble();
            visualMapGateway.Y = collection["y"].ToDouble();
            visualMapGateway.Width = collection["w"].ToDouble();
            visualMapGateway.Height = collection["h"].ToDouble();
          }
          if (!string.IsNullOrEmpty(collection["r"]))
            visualMapGateway.Rotation = collection["r"];
          if (!string.IsNullOrEmpty(collection["a"]))
            visualMapGateway.Altitude = collection["a"].ToInt();
          visualMapGateway.Save();
          break;
        default:
          return (ActionResult) this.Content("Error");
      }
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return currentVisualMap == null ? (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "/Index",
        controller = "Map"
      }) : (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "/ViewMap/",
        controller = "Map",
        id = currentVisualMap.VisualMapID
      });
    }
  }

  [AuthorizeDefault]
  public ActionResult Remove(long id)
  {
    if (MonnitSession.CurrentVisualMap == null || !MonnitSession.CurrentVisualMap.PlacedSensors.ContainsKey(id))
      return (ActionResult) this.Content("Failed to Remove map");
    VisualMapSensor visualMapSensor = VisualMapSensor.Load(MonnitSession.CurrentVisualMap.PlacedSensors[id].VisualMapSensorID);
    if (!MonnitSession.IsAuthorizedForAccount(Sensor.Load(visualMapSensor.SensorID).AccountID))
      return (ActionResult) this.Content("Not Authorized to Remove from Visual Search");
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Visual_Map_Edit")))
      return (ActionResult) this.Content("Not Authorized to Remove from Visual Search");
    visualMapSensor.Delete();
    MonnitSession.CurrentVisualMap.PlacedSensors.Remove(id);
    return (ActionResult) this.Content("success");
  }

  [AuthorizeDefault]
  public ActionResult RemoveStatic(string deviceType, long deviceID, long visualMapID)
  {
    ActionResult actionResult = this.PermissionsCheck(new string[2]
    {
      "Visual_Map_Edit",
      "Navigation_View_Maps"
    }, new string[1]{ "Navigation_View_Maps" }, new string[1]
    {
      "view_maps"
    });
    if (actionResult != null)
      return actionResult;
    VisualMap currentVisualMap = MonnitSession.CurrentVisualMap;
    try
    {
      switch (deviceType)
      {
        case "sensor":
          VisualMapSensor visualMapSensor = VisualMapSensor.Load(currentVisualMap.PlacedSensors[deviceID].VisualMapSensorID);
          if (visualMapSensor == null)
            return (ActionResult) this.Content("Success");
          if (!MonnitSession.IsAuthorizedForAccount(Sensor.Load(visualMapSensor.SensorID).AccountID))
            return (ActionResult) this.Content("Not Authorized to Remove from Visual Search");
          if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Visual_Map_Edit")))
            return (ActionResult) this.Content("Not Authorized to Remove from Visual Search");
          visualMapSensor.Delete();
          currentVisualMap.PlacedSensors.Remove(visualMapSensor.VisualMapSensorID);
          return (ActionResult) this.Content("Success");
        case "gateway":
          VisualMapGateway visualMapGateway = VisualMapGateway.Load(currentVisualMap.PlacedGateways[deviceID].VisualMapGatewayID);
          if (visualMapGateway == null)
            return (ActionResult) this.Content("Success");
          if (!MonnitSession.IsAuthorizedForAccount(CSNet.Load(Gateway.Load(visualMapGateway.GatewayID).CSNetID).AccountID))
            return (ActionResult) this.Content("Not Authorized to Remove from Visual Search");
          if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Visual_Map_Edit")))
            return (ActionResult) this.Content("Not Authorized to Remove from Visual Search");
          visualMapGateway.Delete();
          currentVisualMap.PlacedGateways.Remove(visualMapGateway.VisualMapGatewayID);
          return (ActionResult) this.Content("Success");
        default:
          return (ActionResult) this.Content("Error");
      }
    }
    catch
    {
      return currentVisualMap == null ? (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "/Index",
        controller = "Map"
      }) : (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "/ViewMap/",
        controller = "Map",
        id = currentVisualMap.VisualMapID
      });
    }
  }

  [AuthorizeDefault]
  public ActionResult Image(string id)
  {
    this.Response.Cache.SetCacheability(HttpCacheability.Public);
    this.Response.Cache.SetMaxAge(new TimeSpan(0, 15, 0));
    int level = int.Parse(this.Request["level"]);
    int x = int.Parse(this.Request["x"]);
    int y = int.Parse(this.Request["y"]);
    int tileSize = 256 /*0x0100*/;
    int tileOverlap = 1;
    if (MonnitSession.CurrentVisualMap != null && MonnitSession.CurrentVisualMap.AccountID != MonnitSession.CurrentCustomer.AccountID)
    {
      MonnitSession.CurrentVisualMap = (VisualMap) null;
      return (ActionResult) new HttpNotFoundResult("Image not found");
    }
    VisualMap visualMap;
    if (MonnitSession.CurrentVisualMap != null && MonnitSession.CurrentVisualMap.VisualMapID == id.ToLong())
    {
      visualMap = MonnitSession.CurrentVisualMap;
    }
    else
    {
      visualMap = VisualMap.Load(id.ToLong());
      MonnitSession.CurrentVisualMap = visualMap;
    }
    return visualMap != null ? (ActionResult) this.File(VisualMap.FromBitmap(this.CreateTileImage(visualMap.Bitmap, level, x, y, tileSize, tileOverlap)), "image/jpeg") : (ActionResult) new HttpNotFoundResult("Image not found");
  }

  [AuthorizeDefault]
  private Bitmap CreateTileImage(
    Bitmap source,
    int level,
    int x,
    int y,
    int tileSize,
    int tileOverlap)
  {
    int num1;
    if (source.Width > source.Height)
    {
      num1 = (int) Math.Pow(2.0, (double) level);
      int num2 = num1 * source.Height / source.Width;
    }
    else
      num1 = (int) Math.Pow(2.0, (double) level) * source.Width / source.Height;
    tileSize = Math.Min(tileSize, (int) Math.Pow(2.0, (double) level));
    int num3 = x * tileSize - tileOverlap;
    int width1 = tileSize + 2 * tileOverlap;
    int num4 = y * tileSize - tileOverlap;
    int height1 = tileSize + 2 * tileOverlap;
    if (num3 < 0)
    {
      num3 += tileOverlap;
      width1 -= tileOverlap;
    }
    if (num4 < 0)
    {
      num4 += tileOverlap;
      height1 -= tileOverlap;
    }
    float num5 = (float) source.Width / (float) num1;
    float x1 = (float) num3 * num5;
    float width2 = (float) width1 * num5;
    float y1 = (float) num4 * num5;
    float height2 = (float) height1 * num5;
    if ((double) x1 + (double) width2 > (double) source.Width)
    {
      width2 = (float) source.Width - x1;
      width1 = (int) Math.Round((double) width2 / (double) num5);
    }
    if ((double) y1 + (double) height2 > (double) source.Height)
    {
      height2 = (float) source.Height - y1;
      height1 = (int) Math.Round((double) height2 / (double) num5);
    }
    Bitmap tileImage = new Bitmap(width1, height1);
    Graphics graphics = Graphics.FromImage((System.Drawing.Image) tileImage);
    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
    graphics.DrawImage((System.Drawing.Image) source, new RectangleF(0.0f, 0.0f, (float) width1, (float) height1), new RectangleF(x1, y1, width2, height2), GraphicsUnit.Pixel);
    graphics.Dispose();
    return tileImage;
  }

  [AuthorizeDefault]
  public ActionResult OverlayUpdate(long id)
  {
    VisualMap model;
    if (MonnitSession.CurrentVisualMap != null && MonnitSession.CurrentVisualMap.VisualMapID == id)
    {
      model = MonnitSession.CurrentVisualMap;
    }
    else
    {
      model = VisualMap.Load(id);
      MonnitSession.CurrentVisualMap = model;
    }
    this.ViewData["SensorList"] = (object) MapController.GetSensorList(new long?());
    return (ActionResult) this.PartialView(nameof (OverlayUpdate), (object) model);
  }

  [AuthorizeDefault]
  public ActionResult LoadSnapshot(long id)
  {
    return (ActionResult) this.PartialView((object) VisualMapSensor.LoadByVisualMapID(VisualMap.Load(id).VisualMapID));
  }

  [AuthorizeDefault]
  public ActionResult GatewayDetailsSmallOneView(long id)
  {
    Gateway gateway = Gateway.Load(id);
    return (ActionResult) this.Content($"<div style='text-align:left; font-size:1.2em; font-weight: 700; color: rgb(51, 51, 51);'>\r\n    {gateway.Name}\r\n</div>\r\n<div style='text-align:left; font-size:.8em; color: rgb(42,63,84); font-family: Helvetica, Arial, sans-serif; line-height: 1.471;'>\r\n\t\tStatus: {Enum.GetName(typeof (eSensorStatus), (object) gateway.Status)}\r\n</div>");
  }

  public ActionResult PermissionsCheck(
    string[] custPerms,
    string[] acctPerms,
    string[] acctFeatures)
  {
    Dictionary<CustomerPermissionType, string> dictionary1 = new Dictionary<CustomerPermissionType, string>();
    dictionary1.Add(CustomerPermissionType.Find("Navigation_View_Maps"), "You do not have permission to view Maps.");
    dictionary1.Add(CustomerPermissionType.Find("Visual_Map_Edit"), "You do not have permission to edit Maps.");
    Dictionary<AccountPermissionType, string> dictionary2 = new Dictionary<AccountPermissionType, string>();
    dictionary2.Add(AccountPermissionType.Find("Navigation_View_Maps"), "This account does noth ave permission to view Maps.");
    Dictionary<Feature, string> dictionary3 = new Dictionary<Feature, string>();
    dictionary3.Add(Feature.Find("view_maps"), "This account does noth ave permission to view Maps.");
    StringBuilder stringBuilder = new StringBuilder();
    foreach (string custPerm in custPerms)
    {
      CustomerPermissionType customerPermissionType = CustomerPermissionType.Find(custPerm);
      if (customerPermissionType == null)
        stringBuilder.AppendLine($"CustomerPermissionType '{custPerm}' does not exist.");
      else if (!dictionary1.Keys.Contains<CustomerPermissionType>(customerPermissionType))
        stringBuilder.AppendLine($"CustomerPermissionType '{custPerm}' is not in the list of this method's permissions to check. Please add it.");
      else if (!MonnitSession.CurrentCustomer.Can(customerPermissionType))
        stringBuilder.AppendLine(dictionary1[customerPermissionType]);
    }
    foreach (string acctPerm in acctPerms)
    {
      AccountPermissionType key = AccountPermissionType.Find(acctPerm);
      if (key == null)
        stringBuilder.AppendLine($"AccountPermissionType '{acctPerm}' does not exist.");
      else if (!dictionary2.Keys.Contains<AccountPermissionType>(key))
        stringBuilder.AppendLine($"AccountPermissionType '{acctPerm}' is not in this method's list of permissions to check. Please add it.");
      else if (!MonnitSession.CurrentCustomer.Account.Permissions.Select<AccountPermission, long>((Func<AccountPermission, long>) (p => p.AccountPermissionTypeID)).Contains<long>(key.AccountPermissionTypeID))
        stringBuilder.AppendLine(dictionary2[key]);
    }
    foreach (string acctFeature in acctFeatures)
    {
      Feature feature = Feature.Find(acctFeature);
      if (feature == null)
        stringBuilder.AppendLine($"Feature '{acctFeature}' does not exist.");
      else if (!dictionary3.Keys.Contains<Feature>(feature))
        stringBuilder.AppendLine($"Feature '{acctFeature}' is not in this method's list of features to check. Please add it.");
      else if (!MonnitSession.CurrentSubscription.Can(feature))
        stringBuilder.AppendLine(dictionary3[feature]);
    }
    if (stringBuilder.Length <= 0)
      return (ActionResult) null;
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
    {
      ErrorLocation = "Maps",
      ErrorTranslateTag = "Map/MapIndex|",
      ErrorTitle = "Unauthorized access",
      ErrorMessage = stringBuilder.ToString()
    });
  }
}
