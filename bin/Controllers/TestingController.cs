// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.TestingController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.ControllerBase;
using iMonnit.Models;
using Microsoft.CSharp.RuntimeBinder;
using Monnit;
using Newtonsoft.Json;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

public class TestingController : Controller
{
  private ObservableCollection<Sensor> _sensorList = new ObservableCollection<Sensor>();
  private static HashSet<Guid> gatewayHistoryGuids;

  public Sensor FindSensor(long sensorID)
  {
    foreach (Sensor sensor in (Collection<Sensor>) this.SensorList)
    {
      if (sensor.SensorID == sensorID)
        return sensor;
    }
    return Sensor.Load(sensorID);
  }

  public ActionResult Unknow(long networkID, long ID)
  {
    ModelStateDictionary modelStateDictionary = new ModelStateDictionary();
    AssignObjectModel model = new AssignObjectModel()
    {
      NetworkID = networkID,
      ObjectID = ID.ToLong()
    };
    model.Code = "IM" + MonnitUtil.CheckDigit(model.ObjectID);
    if (!MonnitSession.CustomerCan("Can_Access_Testing"))
      return (ActionResult) this.Content("Unauthorized");
    string str = "Successfully added " + ID.ToString();
    try
    {
      Sensor sensor1 = Sensor.Load(model.ObjectID);
      CSNet csNet1 = CSNet.Load(model.NetworkID);
      if (sensor1 != null && sensor1.IsDeleted)
      {
        str = $"SensorID: {model.ObjectID} Not Found";
        modelStateDictionary.AddModelError("ObjectID", str);
      }
      else
      {
        if (sensor1 == null)
        {
          sensor1 = CSNetControllerBase.LookUpSensor(MonnitSession.CurrentCustomer.AccountID, ID, MonnitUtil.CheckDigit(ID), sensor1);
          if (sensor1 != null)
            MonnitSession.ProgramLevel();
        }
        if (sensor1 == null)
        {
          Gateway gateway = Gateway.Load(model.ObjectID);
          if (gateway == null || gateway != null && gateway.IsDeleted)
          {
            try
            {
              gateway = CSNetControllerBase.LookUpGateway(model);
              if (gateway != null)
              {
                gateway.CSNetID = csNet1.CSNetID;
                gateway.ForceInsert();
              }
              else
                modelStateDictionary.AddModelError("ObjectID", $"GatewayID: {model.ObjectID} could not be transfered to new network");
            }
            catch
            {
            }
          }
          if (gateway == null || gateway != null && gateway.IsDeleted)
            modelStateDictionary.AddModelError("ObjectID", "No Device Found");
          if (csNet1 == null)
            modelStateDictionary.AddModelError("ObjectID", $"GatewayID: {model.ObjectID} could not be transfered, No network Found");
          if (modelStateDictionary.IsValid)
          {
            CSNet csNet2 = CSNet.Load(gateway.CSNetID);
            long? oldAccountID = new long?();
            if (csNet2 == null || csNet2.HoldingOnlyNetwork || gateway.CSNetID == ConfigData.AppSettings("DefaultCSNetID").ToLong() || MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID))
            {
              if (csNet1 != null && csNet2 != null && csNet1.AccountID != csNet2.AccountID)
              {
                foreach (Notification notification in GatewayNotification.LoadByGatewayID(gateway.GatewayID))
                  notification.RemoveGateway(gateway);
                oldAccountID = new long?(csNet2.AccountID);
              }
              NetworkAudit.LogNetworkChange(MonnitSession.CurrentCustomer.CustomerID, gateway, oldAccountID);
              bool flag1 = false;
              bool flag2 = false;
              if (csNet2 == null)
                flag2 = true;
              if (!flag2)
              {
                Account account = Account.Load(csNet2.AccountID);
                gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Assign gateway to existing network");
                flag1 = true;
              }
              if (gateway.SensorID > 0L)
              {
                Sensor sensor2 = Sensor.Load(gateway.SensorID);
                if (sensor2 == null)
                {
                  eProgramLevel o = MonnitSession.ProgramLevel();
                  if (ThemeController.SensorCount() <= o.ToInt())
                    CSNetControllerBase.LookUpSensor(MonnitSession.CurrentCustomer.AccountID, ID, MonnitUtil.CheckDigit(ID), sensor1);
                }
                if (sensor2 != null)
                {
                  sensor2.AccountID = csNet1.AccountID;
                  sensor2.CSNetID = csNet1.CSNetID;
                  sensor2.IsActive = true;
                  sensor2.IsNewToNetwork = true;
                  if (csNet1.AccountID != csNet2.AccountID)
                  {
                    sensor2.LastCommunicationDate = DateTime.MinValue;
                    sensor2.LastDataMessageGUID = Guid.Empty;
                    sensor2.StartDate = DateTime.UtcNow;
                  }
                  Account account = Account.Load(csNet2.AccountID);
                  sensor2.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Assign sensor to existing network");
                  sensor2.Save();
                  sensor2.ResetLastCommunicationDate();
                  if (csNet2 == null || csNet1.AccountID != csNet2.AccountID)
                  {
                    foreach (Notification notification in Notification.LoadBySensorID(sensor2.SensorID))
                      notification.RemoveSensor(sensor2);
                  }
                  return (ActionResult) this.Redirect("/Setup/SensorEdit/" + sensor2.SensorID.ToString());
                }
              }
              gateway.CSNetID = csNet1.CSNetID;
              gateway.LastCommunicationDate = DateTime.MinValue;
              gateway.StartDate = DateTime.UtcNow;
              gateway.Save();
              if (!flag1)
              {
                Account account = Account.Load(CSNet.Load(gateway.CSNetID).AccountID);
                gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Assign gateway to new network");
              }
              // ISSUE: reference to a compiler-generated field
              if (TestingController.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
              {
                // ISSUE: reference to a compiler-generated field
                TestingController.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NetworkID", typeof (TestingController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              object obj = TestingController.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) TestingController.\u003C\u003Eo__2.\u003C\u003Ep__0, this.ViewBag, model.NetworkID);
            }
            return (ActionResult) this.Content(str);
          }
        }
        else
        {
          if (sensor1 == null)
            modelStateDictionary.AddModelError("ObjectID", $"SensorID: {model.ObjectID} Not Found");
          if (csNet1 == null)
            modelStateDictionary.AddModelError("ObjectID", $"SensorID: {model.ObjectID} could not be transfered to new network");
          else if (csNet1.Sensors.Where<Sensor>((Func<Sensor, bool>) (s => s.SensorTypeID != 4L)).Count<Sensor>() >= 500 && sensor1.SensorTypeID != 4L)
            modelStateDictionary.AddModelError("ObjectID", $"SensorID: {model.ObjectID} could not be transfered to new network.  Network sensor limit has been reached.");
          Account account = Account.Load(csNet1.AccountID);
          if (csNet1.AccountID != sensor1.AccountID && Sensor.LoadByAccountID(account.AccountID).Count >= account.CurrentSubscription.AccountSubscriptionType.AllowedSensors)
            return (ActionResult) this.Redirect($"/Testing/AddDevice/{ID.ToString()}?networkID={model.NetworkID.ToString()}&failed={$"SensorID: {model.ObjectID} could not be added.  Sensor limit has been reached."}");
          if (sensor1.Network == null || sensor1.Network.HoldingOnlyNetwork || sensor1.Network.CSNetID == ConfigData.AppSettings("DefaultCSNetID").ToLong() || MonnitSession.CurrentCustomer.CanSeeNetwork(sensor1.CSNetID))
          {
            sensor1.ReadyToShip();
            sensor1.ResetLastCommunicationDate();
            foreach (Notification notification in Notification.LoadBySensorID(sensor1.SensorID))
            {
              if (MonnitSession.CurrentCustomer.AccountID != notification.AccountID)
                notification.RemoveSensor(sensor1);
            }
            if (sensor1.AccountID != csNet1.AccountID)
            {
              sensor1.LastDataMessageGUID = Guid.Empty;
              sensor1.StartDate = DateTime.UtcNow;
            }
            sensor1.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Assigned  sensor to account");
            sensor1.Save();
            sensor1.AccountID = csNet1.AccountID;
            sensor1.CSNetID = model.NetworkID;
            sensor1.IsActive = true;
            sensor1.IsNewToNetwork = true;
            sensor1.SensorName = $"{sensor1.MonnitApplication.ApplicationName.ToString()} - {sensor1.SensorID.ToString()}";
            sensor1.SensorApplicationID = long.MinValue;
            sensor1.Save();
            TimedCache.RemoveObject("SensorCount");
            MonnitSession.AccountSensorTotal = int.MinValue;
            // ISSUE: reference to a compiler-generated field
            if (TestingController.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              TestingController.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NetworkID", typeof (TestingController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = TestingController.\u003C\u003Eo__2.\u003C\u003Ep__1.Target((CallSite) TestingController.\u003C\u003Eo__2.\u003C\u003Ep__1, this.ViewBag, model.NetworkID);
          }
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log($"TestingController.AddSensor[Post][SensorID: {ID.ToString()}] ");
      str = "Failed: " + ex.Message;
    }
    return (ActionResult) this.Content(str);
  }

  [AuthorizeDefault]
  public ActionResult Index(long? id)
  {
    if (!MonnitSession.CustomerCan("Can_Access_Testing"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Testing",
        ErrorTranslateTag = "Testing|",
        ErrorTitle = "Unauthorized Access to Testing Page",
        ErrorMessage = "You do not have permission to access this page."
      });
    // ISSUE: reference to a compiler-generated field
    if (TestingController.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      TestingController.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Networkid", typeof (TestingController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = TestingController.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) TestingController.\u003C\u003Eo__3.\u003C\u003Ep__0, this.ViewBag, id.GetValueOrDefault());
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ResetSingleSensorForShipping(long id)
  {
    string content = "Success";
    try
    {
      Sensor DBObject = TestingController.TestingToolException.AuthorizeSensorAccess(id);
      TestingController.TestingToolException.AuthorizeTestingToolAccess();
      TestingController.TestingToolException.AuthorizeNetworkAccess(DBObject.CSNetID);
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "ResetSensorForShipping " + DBObject.SensorID.ToString());
      DBObject.ResetSensorForShipping();
    }
    catch (Exception ex)
    {
      if (ex.GetType() != typeof (TestingController.TestingToolException))
        ex.Log("TestingController.ResetSingleSensorForShipping");
      content = "Failed: " + ex.Message;
    }
    return (ActionResult) this.Content(content);
  }

  public ActionResult ResetSingleGatewayForShipping(long id)
  {
    string content = "Success";
    try
    {
      TestingController.TestingToolException.AuthorizeTestingToolAccess();
      Gateway DBObject = TestingController.TestingToolException.AuthorizeGatewayAccess(id);
      TestingController.TestingToolException.AuthorizeNetworkAccess(DBObject.CSNetID);
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Reset Gateway " + DBObject.GatewayID.ToString());
      DBObject.ResetToDefault(true);
    }
    catch (TestingController.TestingToolException ex)
    {
      content = "Failed: " + ex.Message;
    }
    catch (Exception ex)
    {
      ex.Log("TestingController.ResetSingleGatewayForShipping");
      content = "Failed: " + ex.Message;
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ResetAllGatewaysForShipping(long id)
  {
    string content = "Success";
    try
    {
      TestingController.TestingToolException.AuthorizeTestingToolAccess();
      CSNet csNet = TestingController.TestingToolException.AuthorizeNetworkAccess(id);
      foreach (Gateway DBObject in Gateway.LoadByCSNetID(csNet.CSNetID))
      {
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, csNet.AccountID, "Reset Gateway " + DBObject.GatewayID.ToString());
        DBObject.ResetToDefault(true);
      }
    }
    catch (Exception ex)
    {
      if (ex.GetType() != typeof (TestingController.TestingToolException))
        ex.Log("TestingController.ResetAllGatewaysForShipping");
      content = ex.Message;
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ResetAllSensorsForShipping(long id)
  {
    string content = "Success";
    try
    {
      TestingController.TestingToolException.AuthorizeTestingToolAccess();
      foreach (Sensor DBObject in Sensor.LoadByCsNetID(TestingController.TestingToolException.AuthorizeNetworkAccess(id).CSNetID))
      {
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "ResetSensorForShipping " + DBObject.SensorID.ToString());
        DBObject.ResetSensorForShipping();
      }
    }
    catch (Exception ex)
    {
      if (ex.GetType() != typeof (TestingController.TestingToolException))
        ex.Log("TestingController.ResetAllSensorsForShipping");
      content = ex.Message;
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult SetAllSensorsTo10SecHeartBeat(long id)
  {
    string content = "Success";
    try
    {
      TestingController.TestingToolException.AuthorizeTestingToolAccess();
      foreach (Sensor DBObject in Sensor.LoadByCsNetID(TestingController.TestingToolException.AuthorizeNetworkAccess(id).CSNetID))
      {
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Reset Sensor Heartbeats " + DBObject.SensorID.ToString());
        DBObject.ReportInterval = 0.166;
        DBObject.ActiveStateInterval = 0.166;
        DBObject.GeneralConfigDirty = true;
        DBObject.Save();
      }
    }
    catch (Exception ex)
    {
      if (ex.GetType() != typeof (TestingController.TestingToolException))
        ex.Log("TestingController.SetAllSensorsTo10SecHeartBeat");
      content = "Failed: " + ex.Message;
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ClearSensors(long id)
  {
    string content = "Success";
    try
    {
      TestingController.TestingToolException.AuthorizeTestingToolAccess();
      CSNet csNet = TestingController.TestingToolException.AuthorizeNetworkAccess(id);
      long id1 = ConfigData.AppSettings("DefaultCSNetID").ToLong();
      foreach (Sensor sensor in Sensor.LoadByCsNetID(csNet.CSNetID))
      {
        if (!sensor.IsDirty && !sensor.PendingActionControlCommand)
          NetworkController.TryMoveSensor(id1, sensor.SensorID);
      }
    }
    catch (Exception ex)
    {
      if (ex.GetType() != typeof (TestingController.TestingToolException))
        ex.Log("TestingController.ClearSensors");
      content = "Failed: " + ex.Message;
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ClearGateways(long id)
  {
    string content = "Success";
    try
    {
      TestingController.TestingToolException.AuthorizeTestingToolAccess();
      CSNet csNet = TestingController.TestingToolException.AuthorizeNetworkAccess(id);
      long id1 = ConfigData.AppSettings("DefaultCSNetID").ToLong();
      foreach (Gateway gateway in Gateway.LoadByCSNetID(csNet.CSNetID))
        NetworkController.TryMoveGateway(id1, gateway);
    }
    catch (Exception ex)
    {
      if (ex.GetType() != typeof (TestingController.TestingToolException))
        ex.Log("TestingController.ClearGateways");
      content = "Failed: " + ex.Message;
    }
    return (ActionResult) this.Content(content);
  }

  public ObservableCollection<Sensor> SensorList => this._sensorList;

  [AuthorizeDefault]
  public ActionResult TestingSensorList(long id)
  {
    if (!MonnitSession.CustomerCan("Can_Access_Testing"))
      return (ActionResult) this.Content("Unauthorized");
    MonnitSession.TestingToolSession.DeviceType = "sensor";
    List<CSNet> networksUserCanSee = CSNetControllerBase.GetListOfNetworksUserCanSee(new long?(MonnitSession.CurrentCustomer.AccountID));
    if (id.ToLong() != -1L && networksUserCanSee.Find((Predicate<CSNet>) (n => n.CSNetID == id)) == null)
    {
      if (MonnitSession.SensorListFilters.CSNetID < 0L)
      {
        if (networksUserCanSee.Count > 0)
          id = networksUserCanSee[0].CSNetID;
      }
      else if (networksUserCanSee.Find((Predicate<CSNet>) (n => n.CSNetID == MonnitSession.SensorListFilters.CSNetID)) != null)
        id = MonnitSession.SensorListFilters.CSNetID;
      else if (networksUserCanSee.Count > 0)
        id = networksUserCanSee[0].CSNetID;
    }
    if (id > 0L)
    {
      CSNet csNet = networksUserCanSee.Find((Predicate<CSNet>) (n => n.CSNetID == id));
      MonnitSession.SensorListFilters.CSNetID = csNet == null || csNet.AccountID != MonnitSession.CurrentCustomer.AccountID ? long.MinValue : id.ToLong();
    }
    else
      MonnitSession.SensorListFilters.CSNetID = long.MinValue;
    MonnitSession.SensorListFilters.ApplicationID = long.MinValue;
    MonnitSession.SensorListFilters.Status = int.MinValue;
    MonnitSession.SensorListFilters.Name = "";
    MonnitSession.SensorListFilters.Custom = "";
    // ISSUE: reference to a compiler-generated field
    if (TestingController.\u003C\u003Eo__14.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      TestingController.\u003C\u003Eo__14.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "netID", typeof (TestingController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = TestingController.\u003C\u003Eo__14.\u003C\u003Ep__0.Target((CallSite) TestingController.\u003C\u003Eo__14.\u003C\u003Ep__0, this.ViewBag, id);
    // ISSUE: reference to a compiler-generated field
    if (TestingController.\u003C\u003Eo__14.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      TestingController.\u003C\u003Eo__14.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "LeftNavSelection", typeof (TestingController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = TestingController.\u003C\u003Eo__14.\u003C\u003Ep__1.Target((CallSite) TestingController.\u003C\u003Eo__14.\u003C\u003Ep__1, this.ViewBag, "Sensor");
    IEnumerable<Sensor> model = (IEnumerable<Sensor>) Sensor.LoadByCsNetID(id).OrderBy<Sensor, DateTime>((Func<Sensor, DateTime>) (s => s.StartDate));
    Dictionary<string, string> LatestVersions = new Dictionary<string, string>();
    // ISSUE: reference to a compiler-generated field
    if (TestingController.\u003C\u003Eo__14.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      TestingController.\u003C\u003Eo__14.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UpdateableSensorIds", typeof (TestingController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = TestingController.\u003C\u003Eo__14.\u003C\u003Ep__2.Target((CallSite) TestingController.\u003C\u003Eo__14.\u003C\u003Ep__2, this.ViewBag, string.Join<long>(",", NetworkController.UpdateableSensors(MonnitSession.CurrentCustomer.AccountID, MonnitSession.CustomerCan("Support_Advanced"), ref LatestVersions).Select<Sensor, long>((Func<Sensor, long>) (s => s.SensorID))));
    if (MonnitSession.SensorListFilters.CSNetID == long.MinValue)
      this.ViewData["AppList"] = (object) MonnitApplication.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
    else
      this.ViewData["AppList"] = (object) MonnitApplication.LoadByCSNetID(MonnitSession.SensorListFilters.CSNetID);
    return (ActionResult) this.PartialView((object) model);
  }

  [AuthorizeDefault]
  public ActionResult LoadSensorDetails(long id)
  {
    if (!MonnitSession.CustomerCan("Can_Access_Testing"))
      return (ActionResult) this.Content("Unauthorized");
    Sensor model = (Sensor) null;
    try
    {
      model = Sensor.Load(id);
    }
    catch (Exception ex)
    {
      ex.Log("TestingController.LoadSensorDetails[Get] ");
    }
    return (ActionResult) this.PartialView((object) model);
  }

  [AuthorizeDefault]
  public ActionResult LoadSensorHistory(long id)
  {
    if (!MonnitSession.CustomerCan("Can_Access_Testing"))
      return (ActionResult) this.Content("Unauthorized");
    List<DataMessage> model = (List<DataMessage>) null;
    try
    {
      Sensor sensor = Sensor.Load(id);
      DateTime toDate = DateTime.UtcNow.AddMinutes(1.0);
      DateTime fromDate = DateTime.UtcNow.AddDays(-1.0);
      model = DataMessage.LoadBySensorAndDateRange(id, fromDate, toDate, 100, new Guid?());
      // ISSUE: reference to a compiler-generated field
      if (TestingController.\u003C\u003Eo__16.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TestingController.\u003C\u003Eo__16.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Sensor, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Sensor", typeof (TestingController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = TestingController.\u003C\u003Eo__16.\u003C\u003Ep__0.Target((CallSite) TestingController.\u003C\u003Eo__16.\u003C\u003Ep__0, this.ViewBag, sensor);
    }
    catch (Exception ex)
    {
      ex.Log($"TestingController.LoadSensorHistory[Get][SensorID: {id.ToString()}] ");
    }
    return (ActionResult) this.PartialView((object) model);
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult Control(long id)
  {
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex"
      });
    Sensor model = Sensor.Load(id);
    return model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID) || model.ApplicationID != 12L && model.ApplicationID != 76L && model.ApplicationID != 125L && model.ApplicationID != 158L ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "SensorIndex",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [AuthorizeDefault]
  [ValidateAntiForgeryToken]
  public ActionResult Control(long id, FormCollection collection)
  {
    if (!MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Sensor_Edit")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    Sensor sensor = Sensor.Load(id);
    if (sensor == null || !MonnitSession.IsAuthorizedForAccount(sensor.AccountID) || sensor.ApplicationID != 12L && sensor.ApplicationID != 76L && sensor.ApplicationID != 158L)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "SensorIndex",
        controller = "Overview"
      });
    MonnitApplicationBase.ControlSensor((NameValueCollection) collection, sensor);
    return (ActionResult) this.PartialView((object) sensor);
  }

  public static string TestingSensorHistoryRow(Sensor sensor, DataMessage item)
  {
    string str = $"<div class=\"row testingHistoryRow\" title=\"{item.DataMessageGUID}\" data-guid=\"{item.DataMessageGUID}\" data-timestamp=\"{item.MessageDate.Ticks}\">";
    return (item.DisplayData.Length <= 12 ? $"{$"{$"{$"{str}<div class=\"col-3\">{item.MessageDate.OVToLocalDateTimeShort()}</div>"}<div class=\"col-3\">{AutoRefreshController.SensorSignalStrengthTestString(sensor, item)}</div>"}<div class=\"col-3\">{AutoRefreshController.SensorPowerTestString(item)}</div>"}<div class=\"col-3\">{item.DisplayData}</div>" : $"{$"{$"{$"{str}<div class=\"col-4\">{item.MessageDate.OVToLocalDateTimeShort()}</div>"}<div class=\"col-4\">{AutoRefreshController.SensorSignalStrengthTestString(sensor, item)}</div>"}<div class=\"col-4\">{AutoRefreshController.SensorPowerTestString(item)}</div>"}<div class=\"col-12\">{item.DisplayData}</div>") + "</div>";
  }

  public static string TestingGatewayHistoryRow(Gateway gw, GatewayTestingMessage testingMessage)
  {
    string str = $"{$"{"" + $"<div class=\"row testingHistoryRow\" title=\"{testingMessage.Guid}\" data-timestamp=\"{testingMessage.MessageDate.Ticks}\" data-iconString=\"{testingMessage.IconString}\">"}<div class=\"col-3\">{testingMessage.MessageDate.OVToLocalDateTimeShort()}</div>"}<div class=\"col-2\">{testingMessage.IconString}</div>" + $"<div class=\"col-2\">{testingMessage.DeviceID}</div>" + "<div class=\"col-5\">";
    string[] strArray = testingMessage.Content.Split('|');
    if (testingMessage.IconString == "gateway")
      str = $"{$"{$"{str}<div>Signal: {AutoRefreshController.GatewaySignalStrengthTestString(gw.GatewayTypeID, strArray[0])}</div>"}<div>Power: {AutoRefreshController.GatewayPowerTestString(strArray[1], strArray[2])}</div>"}<div>Message Count: {strArray[3]}</div>";
    else if (testingMessage.IconString == "sensor")
      str = $"{$"{str}<div>Signal: {strArray[0]}</div>"}<div>Data: {strArray[1]}</div>";
    else if (testingMessage.IconString == "location")
      str = $"{$"{str}<div>Lat: {strArray[0]}</div>"}<div>Long: {strArray[1]}</div>";
    return str + "</div></div>";
  }

  public ActionResult UpdateSensorHistory(long id, long timestamp)
  {
    Sensor sensor = Sensor.Load(id);
    DateTime utcNow = DateTime.UtcNow;
    DateTime fromDate = new DateTime(timestamp).AddSeconds(1.0);
    List<DataMessage> source = DataMessage.LoadBySensorAndDateRange(id, fromDate, utcNow, 100, new Guid?());
    source.Reverse();
    return (ActionResult) this.Json((object) source.Select<DataMessage, string>((Func<DataMessage, string>) (r => TestingController.TestingSensorHistoryRow(sensor, r))).ToList<string>(), JsonRequestBehavior.AllowGet);
  }

  public ActionResult UpdateGatewayHistory(long id, long timestamp)
  {
    Gateway gw = Gateway.Load(id);
    DateTime utcNow = DateTime.UtcNow;
    DateTime fromDate = new DateTime(timestamp).AddMilliseconds(100.0);
    List<string> data = new List<string>();
    List<GatewayTestingMessage> source = GatewayTestingModel.LoadByGatewayID(id, fromDate, utcNow).Messages;
    if (TestingController.gatewayHistoryGuids != null)
      source = source.Where<GatewayTestingMessage>((Func<GatewayTestingMessage, bool>) (m => !TestingController.gatewayHistoryGuids.Contains(m.Guid))).ToList<GatewayTestingMessage>();
    TestingController.gatewayHistoryGuids = new HashSet<Guid>(source.Select<GatewayTestingMessage, Guid>((Func<GatewayTestingMessage, Guid>) (r => r.Guid)));
    source.Reverse();
    foreach (GatewayTestingMessage testingMessage in source)
      data.Add(TestingController.TestingGatewayHistoryRow(gw, testingMessage));
    return (ActionResult) this.Json((object) data, JsonRequestBehavior.AllowGet);
  }

  public ActionResult LoadMoreSensorHistory(long id, long timestamp)
  {
    Sensor sensor = Sensor.Load(id);
    DateTime toDate = new DateTime(timestamp).AddSeconds(-1.0);
    DateTime startDate1 = sensor.StartDate;
    DateTime startDate2 = sensor.StartDate;
    List<DataMessage> source = DataMessage.LoadBySensorAndDateRange(id, startDate2, toDate, 20, new Guid?());
    source.Reverse();
    return (ActionResult) this.Json((object) source.Select<DataMessage, string>((Func<DataMessage, string>) (r => TestingController.TestingSensorHistoryRow(sensor, r))).ToList<string>(), JsonRequestBehavior.AllowGet);
  }

  public ActionResult LoadMoreGatewayHistory(long id, long timestamp)
  {
    Gateway gateway = Gateway.Load(id);
    DateTime toDate = new DateTime(timestamp).AddSeconds(-1.0);
    DateTime fromDate = toDate.AddSeconds(-300.0);
    return (ActionResult) this.Json((object) GatewayTestingModel.LoadByGatewayID(id, fromDate, toDate).Messages.Select<GatewayTestingMessage, string>((Func<GatewayTestingMessage, string>) (msg => TestingController.TestingGatewayHistoryRow(gateway, msg))).ToList<string>(), JsonRequestBehavior.AllowGet);
  }

  [AuthorizeDefault]
  public ActionResult LoadSensorEdit(long id)
  {
    if (!MonnitSession.CustomerCan("Can_Access_Testing"))
      return (ActionResult) this.Content("Unauthorized");
    Sensor model = (Sensor) null;
    try
    {
      model = Sensor.Load(id);
    }
    catch (Exception ex)
    {
      ex.Log($"TestingController.LoadSensorEdit[Get][SensorID: {id.ToString()}] ");
    }
    return (ActionResult) this.PartialView((object) model);
  }

  [AuthorizeDefault]
  public ActionResult LoadSensorCalibrate(long id)
  {
    if (!MonnitSession.CustomerCan("Can_Access_Testing"))
      return (ActionResult) this.Content("Unauthorized");
    Sensor model = (Sensor) null;
    try
    {
      model = Sensor.Load(id);
    }
    catch (Exception ex)
    {
      ex.Log($"TestingController.LoadSensorCalibrate[Get][SensorID: {id.ToString()}] ");
    }
    return (ActionResult) this.PartialView((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateInput(false)]
  public ActionResult GatewayEdit(long GatewayID, string Name, int ReportInterval)
  {
    if (!MonnitSession.CustomerCan("Can_Access_Testing"))
      return (ActionResult) this.Content("Unauthorized");
    string content;
    try
    {
      Gateway gateway = Gateway.Load(GatewayID);
      gateway.Name = Name;
      gateway.ReportInterval = (double) ReportInterval;
      gateway.Save();
      content = "Success";
    }
    catch (Exception ex)
    {
      ex.Log("TestingController.GatewayEdit[Get] ");
      content = "Failed: " + ex.Message;
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public ActionResult TestingGatewayList(long? id)
  {
    MonnitSession.TestingToolSession.DeviceType = "gateway";
    long num = id ?? long.MinValue;
    try
    {
      TestingController.TestingToolException.AuthorizeTestingToolAccess();
      TestingController.TestingToolException.AuthorizeNetworkAccess(num);
      IEnumerable<Gateway> model = (IEnumerable<Gateway>) Gateway.LoadByCSNetID(num).OrderBy<Gateway, DateTime>((Func<Gateway, DateTime>) (g => g.StartDate));
      // ISSUE: reference to a compiler-generated field
      if (TestingController.\u003C\u003Eo__29.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TestingController.\u003C\u003Eo__29.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UpdateableGatewayIds", typeof (TestingController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = TestingController.\u003C\u003Eo__29.\u003C\u003Ep__0.Target((CallSite) TestingController.\u003C\u003Eo__29.\u003C\u003Ep__0, this.ViewBag, string.Join<long>(",", NetworkController.UpdateableGateways(MonnitSession.CurrentCustomer.AccountID).Select<Gateway, long>((Func<Gateway, long>) (g => g.GatewayID))));
      return (ActionResult) this.PartialView((object) model);
    }
    catch (TestingController.TestingToolException ex)
    {
      return (ActionResult) this.Content("Failed: " + ex.Message);
    }
    catch (Exception ex)
    {
      ex.Log("TestingController.TestingGatewayList");
      return (ActionResult) this.Content("Failed: " + ex.Message);
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult SetTestingToolFontSizePx(int fontSize)
  {
    string str = fontSize.ToString();
    MonnitSession.TestingToolSession.FontSize = str;
    return (ActionResult) this.Content(str + "px");
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Reform(long id)
  {
    try
    {
      TestingController.TestingToolException.AuthorizeTestingToolAccess();
      Gateway DBObject = TestingController.TestingToolException.AuthorizeGatewayAccess(id);
      Account account = Account.Load(CSNet.Load(DBObject.CSNetID).AccountID);
      ConfigData.AppSettings("DefaultCSNetID").ToLong();
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Reformed Gateway");
      DBObject.SendResetNetworkRequest = DBObject.GatewayType.SupportsRemoteNetworkReset;
      DBObject.Save();
      // ISSUE: reference to a compiler-generated field
      if (TestingController.\u003C\u003Eo__31.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TestingController.\u003C\u003Eo__31.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (TestingController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = TestingController.\u003C\u003Eo__31.\u003C\u003Ep__0.Target((CallSite) TestingController.\u003C\u003Eo__31.\u003C\u003Ep__0, this.ViewBag, "Gateway Reform Pending");
      return (ActionResult) this.Content("Success");
    }
    catch (TestingController.TestingToolException ex)
    {
      return (ActionResult) this.Content(ex.Message);
    }
    catch (Exception ex)
    {
      ex.Log("Testing Tool Reform Gateway");
      return (ActionResult) this.Content(((HtmlHelper) null).TranslateTag("Reform Gateway Failed: ") + id.ToString());
    }
  }

  [AuthorizeDefault]
  public ActionResult LoadGatewayDetails(long id)
  {
    if (!MonnitSession.CustomerCan("Can_Access_Testing"))
      return (ActionResult) this.Content("Unauthorized");
    Gateway model = (Gateway) null;
    try
    {
      model = Gateway.Load(id);
    }
    catch (Exception ex)
    {
      ex.Log("TestingController.LoadGatewayDetails[Get] ");
    }
    return (ActionResult) this.PartialView((object) model);
  }

  [AuthorizeDefault]
  public ActionResult LoadGatewayHistory(long id)
  {
    if (!MonnitSession.CustomerCan("Can_Access_Testing"))
      return (ActionResult) this.Content("Unauthorized");
    GatewayTestingModel model = (GatewayTestingModel) null;
    try
    {
      Gateway gateway = Gateway.Load(id);
      DateTime utcNow = DateTime.UtcNow;
      DateTime fromDate = utcNow.AddSeconds(-300.0);
      model = GatewayTestingModel.LoadByGatewayID(id, fromDate, utcNow);
      TestingController.gatewayHistoryGuids = new HashSet<Guid>(model.Messages.Select<GatewayTestingMessage, Guid>((Func<GatewayTestingMessage, Guid>) (r => r.Guid)));
      // ISSUE: reference to a compiler-generated field
      if (TestingController.\u003C\u003Eo__33.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TestingController.\u003C\u003Eo__33.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Gateway, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Gateway", typeof (TestingController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = TestingController.\u003C\u003Eo__33.\u003C\u003Ep__0.Target((CallSite) TestingController.\u003C\u003Eo__33.\u003C\u003Ep__0, this.ViewBag, gateway);
    }
    catch (Exception ex)
    {
      ex.Log($"TestingController.LoadGatewayHistory[Get][GatewayID: {id.ToString()}] ");
    }
    return (ActionResult) this.PartialView((object) model);
  }

  [AuthorizeDefault]
  public ActionResult LoadGatewayEdit(long id)
  {
    if (!MonnitSession.CustomerCan("Can_Access_Testing"))
      return (ActionResult) this.Content("Unauthorized");
    Gateway model = (Gateway) null;
    try
    {
      model = Gateway.Load(id);
    }
    catch (Exception ex)
    {
      ex.Log($"TestingController.LoadGatewayEdit[Get][GatewayID: {id.ToString()}] ");
    }
    return (ActionResult) this.PartialView((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult CreateOTARequest(long id, string sensorIDs)
  {
    Account account = Account.Load(id);
    if (!MonnitSession.CustomerCan("Customer_Can_Update_Firmware") || account == null || !MonnitSession.IsAuthorizedForAccount(account.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    long[] numArray = Array.ConvertAll<string, long>(sensorIDs.Split(new char[1]
    {
      '|'
    }, StringSplitOptions.RemoveEmptyEntries), TestingController.\u003C\u003EO.\u003C0\u003E__Parse ?? (TestingController.\u003C\u003EO.\u003C0\u003E__Parse = new Converter<string, long>(long.Parse)));
    HashSet<long> longSet1 = new HashSet<long>();
    HashSet<long> longSet2 = new HashSet<long>();
    Dictionary<long, OTARequest> dictionary = new Dictionary<long, OTARequest>();
    long ID1 = long.MinValue;
    long ID2 = long.MinValue;
    string sensorSKU = "";
    List<TestingController.SensorOTAResult> first = new List<TestingController.SensorOTAResult>();
    List<TestingController.SensorOTAResult> second = new List<TestingController.SensorOTAResult>();
    foreach (long num in numArray)
    {
      try
      {
        ID1 = long.MinValue;
        ID2 = long.MinValue;
        sensorSKU = "";
        if (longSet1.Add(num))
        {
          ID1 = num;
          Sensor sensor = Sensor.Load(ID1);
          if (sensor != null)
          {
            if (sensor.LastDataMessage != null)
            {
              ID2 = sensor.LastDataMessage.GatewayID;
              sensorSKU = sensor.SKU;
              SKUFirmware skuFirmware = SKUFirmware.LatestFirmware(sensor.SKU);
              if (skuFirmware != null)
              {
                OTARequest otaRequest = new OTARequest()
                {
                  ApplicationID = sensor.ApplicationID,
                  AccountID = account.AccountID,
                  GatewayID = sensor.LastDataMessage.GatewayID,
                  CreatedByID = MonnitSession.CurrentCustomer.CustomerID,
                  CreateDate = DateTime.UtcNow,
                  FirmwareID = skuFirmware.FirmwareID,
                  Version = skuFirmware.Version,
                  SKU = sensor.SKU,
                  Status = eOTAStatus.New
                };
                otaRequest.Save();
                new OTARequestSensor()
                {
                  OTARequestID = otaRequest.OTARequestID,
                  SensorID = ID1,
                  Status = eOTAStatus.New
                }.Save();
                if (longSet2.Add(ID2))
                {
                  Gateway gateway = Gateway.Load(ID2);
                  if (gateway != null && !gateway.OTARequestActive)
                  {
                    gateway.OTARequestActive = true;
                    gateway.Save();
                  }
                }
                first.Add(new TestingController.SensorOTAResult(true, ID2.ToString(), ID1.ToString(), sensorSKU));
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionLog.Log(ex);
        second.Add(new TestingController.SensorOTAResult(false, ID2.ToString(), ID1.ToString(), sensorSKU));
      }
    }
    return (ActionResult) this.Content(JsonConvert.SerializeObject((object) first.Union<TestingController.SensorOTAResult>((IEnumerable<TestingController.SensorOTAResult>) second)));
  }

  [AuthorizeDefault]
  public ActionResult GPSTestingDetails(long id)
  {
    if (!MonnitSession.CustomerCan("SupportAdvanced"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Testing",
        controller = "Testing"
      });
    Gateway model = Gateway.Load(id);
    if (model == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Testing",
        controller = "Testing"
      });
    CSNet csNet = CSNet.Load(model.CSNetID);
    return csNet == null || !MonnitSession.IsAuthorizedForAccount(csNet.AccountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Overview",
      controller = "Account"
    }) : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult GPSTestingLocationMessages(long id)
  {
    if (!MonnitSession.CustomerCan("Support_Advanced"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Testing",
        controller = "Testing"
      });
    Gateway gateway = Gateway.Load(id);
    if (gateway == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Testing",
        controller = "Testing"
      });
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    return csNet == null || !MonnitSession.IsAuthorizedForAccount(csNet.AccountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Overview",
      controller = "Account"
    }) : (ActionResult) this.PartialView("GatewayHistory", (object) LocationMessage.LoadByDeviceID(gateway.GatewayID).OrderByDescending<LocationMessage, DateTime>((Func<LocationMessage, DateTime>) (c => c.LocationDate)).ToList<LocationMessage>());
  }

  [AuthorizeDefault]
  public ActionResult SendGPSTestingMessage(long id)
  {
    if (!MonnitSession.CustomerCan("Support_Advanced"))
      return (ActionResult) this.Content("Failed");
    Gateway gateway = Gateway.Load(id);
    if (gateway == null)
      return (ActionResult) this.Content("Failed");
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    if (csNet == null || !MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return (ActionResult) this.Content("Failed");
    gateway.GPSPing = true;
    gateway.Save();
    return (ActionResult) this.PartialView("gtwTesting");
  }

  [AuthorizeDefault]
  public ActionResult MoveSensor(long id, string sensorID)
  {
    sensorID = sensorID.Split(':')[0];
    long sensorID1;
    try
    {
      sensorID1 = sensorID.ToLong();
    }
    catch
    {
      return (ActionResult) this.Content($"Invalid SensorID: {sensorID}");
    }
    try
    {
      if (!NetworkController.TryMoveSensor(id, sensorID1))
        return (ActionResult) this.Content($"SensorID: {sensorID1} could not be transfered to new network");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content($"Error trying to move SensorID {sensorID1}, \r\nDetails: {ex.Message}");
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult MoveGateway(long id, string gatewayID)
  {
    gatewayID = gatewayID.Split(':')[0];
    long gatewayID1;
    try
    {
      gatewayID1 = gatewayID.ToLong();
    }
    catch
    {
      return (ActionResult) this.Content($"Invalid gatewayID: {gatewayID}");
    }
    try
    {
      if (!NetworkController.TryMoveGateway(id, gatewayID1))
        return (ActionResult) this.Content($"GatewayID: {gatewayID1} could not be transfered to new network");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content($"Error trying to move GatewayID {gatewayID1}, \r\nDetails: {ex.Message}");
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult RemoveSensor(long id)
  {
    string content = "Success";
    try
    {
      TestingController.TestingToolException.AuthorizeTestingToolAccess();
      Sensor sensor = TestingController.TestingToolException.AuthorizeSensorAccess(id);
      long id1 = ConfigData.AppSettings("DefaultCSNetID").ToLong();
      if (!sensor.IsDirty && !sensor.PendingActionControlCommand)
        NetworkController.TryMoveSensor(id1, sensor.SensorID);
    }
    catch (Exception ex)
    {
      if (ex.GetType() != typeof (TestingController.TestingToolException))
        ex.Log("TestingController.ClearSensors");
      content = "Failed: " + ex.Message;
    }
    return (ActionResult) this.Content(content);
  }

  public class EHClass
  {
    private void ReadFile(int index)
    {
      string path = "c:\\users\\public\\test.txt";
      StreamReader streamReader = new StreamReader(path);
      char[] buffer = new char[10];
      try
      {
        streamReader.ReadBlock(buffer, index, buffer.Length);
      }
      catch (IOException ex)
      {
        Console.WriteLine("Error reading from {0}. Message = {1}", (object) path, (object) ex.Message);
      }
      finally
      {
        streamReader?.Close();
      }
    }
  }

  private struct SensorOTAResult(
    bool success,
    string gatewayID,
    string sensorID,
    string sensorSKU)
  {
    private const string successIcon = "<svg id=\"green-pass\" xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 512 512\" style=\"fill:green\"><path d=\"M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM369 209L241 337c-9.4 9.4-24.6 9.4-33.9 0l-64-64c-9.4-9.4-9.4-24.6 0-33.9s24.6-9.4 33.9 0l47 47L335 175c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9z\"/></svg>";
    private const string failureIcon = "<svg id=\"fail-icon\" xmlns=\"http://www.w3.org/2000/svg\"  viewBox=\"0 0 512 512\" style=\"fill:#ca0005\"><path d=\"M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM175 175c9.4-9.4 24.6-9.4 33.9 0l47 47 47-47c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9l-47 47 47 47c9.4 9.4 9.4 24.6 0 33.9s-24.6 9.4-33.9 0l-47-47-47 47c-9.4 9.4-24.6 9.4-33.9 0s-9.4-24.6 0-33.9l47-47-47-47c-9.4-9.4-9.4-24.6 0-33.9z\"/></svg>";

    public string ResultIcon { get; private set; } = success ? "<svg id=\"green-pass\" xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 512 512\" style=\"fill:green\"><path d=\"M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM369 209L241 337c-9.4 9.4-24.6 9.4-33.9 0l-64-64c-9.4-9.4-9.4-24.6 0-33.9s24.6-9.4 33.9 0l47 47L335 175c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9z\"/></svg>" : "<svg id=\"fail-icon\" xmlns=\"http://www.w3.org/2000/svg\"  viewBox=\"0 0 512 512\" style=\"fill:#ca0005\"><path d=\"M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM175 175c9.4-9.4 24.6-9.4 33.9 0l47 47 47-47c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9l-47 47 47 47c9.4 9.4 9.4 24.6 0 33.9s-24.6 9.4-33.9 0l-47-47-47 47c-9.4 9.4-24.6 9.4-33.9 0s-9.4-24.6 0-33.9l47-47-47-47c-9.4-9.4-9.4-24.6 0-33.9z\"/></svg>";

    public string GatewayID { get; private set; } = gatewayID;

    public string SensorID { get; private set; } = sensorID;

    public string SensorSKU { get; private set; } = sensorSKU;
  }

  internal class TestingToolException : Exception
  {
    public TestingToolException()
    {
    }

    public TestingToolException(string message)
      : base(message)
    {
    }

    public static void AuthorizeTestingToolAccess()
    {
      if (!MonnitSession.CustomerCan("Can_Access_Testing"))
        throw new TestingController.TestingToolException(((HtmlHelper) null).TranslateTag("Unauthorized Access to Testing Tool"));
    }

    public static CSNet AuthorizeNetworkAccess(long networkID)
    {
      return TestingController.TestingToolException.AuthorizeNetworkAccess(networkID, CSNet.Load(networkID));
    }

    public static CSNet AuthorizeNetworkAccess(long networkID, CSNet network)
    {
      if (network == null)
        throw new TestingController.TestingToolException(((HtmlHelper) null).TranslateTag("Unknown Network: ") + networkID.ToString());
      return MonnitSession.CurrentCustomer.CanSeeNetwork(network) ? network : throw new TestingController.TestingToolException(((HtmlHelper) null).TranslateTag("Unauthorized Access to Network: ") + network.CSNetID.ToString());
    }

    public static Account AuthorizeAccountAccess(long accountID)
    {
      return TestingController.TestingToolException.AuthorizeAccountAccess(accountID, Account.Load(accountID));
    }

    public static Account AuthorizeAccountAccess(long accountID, Account account)
    {
      if (account == null)
        throw new TestingController.TestingToolException(((HtmlHelper) null).TranslateTag("Unknown Account: ") + accountID.ToString());
      return MonnitSession.CurrentCustomer.CanSeeAccount(account) ? account : throw new TestingController.TestingToolException(((HtmlHelper) null).TranslateTag("Unauthorized Access to Account: ") + account.AccountID.ToString());
    }

    public static Sensor AuthorizeSensorAccess(long sensorID)
    {
      return TestingController.TestingToolException.AuthorizeSensorAccess(sensorID, Sensor.Load(sensorID));
    }

    public static Sensor AuthorizeSensorAccess(long sensorID, Sensor sensor)
    {
      if (sensor == null)
        throw new TestingController.TestingToolException(((HtmlHelper) null).TranslateTag("Unknown Sensor: ") + sensorID.ToString());
      if (!MonnitSession.CurrentCustomer.CanSeeSensor(sensor.SensorID))
        throw new TestingController.TestingToolException(((HtmlHelper) null).TranslateTag("Unauthorized Access to Sensor: ") + sensor.SensorID.ToString());
      return sensor;
    }

    public static Gateway AuthorizeGatewayAccess(long gatewayID)
    {
      return TestingController.TestingToolException.AuthorizeGatewayAccess(gatewayID, Gateway.Load(gatewayID));
    }

    public static Gateway AuthorizeGatewayAccess(long gatewayID, Gateway gateway)
    {
      if (gateway == null)
        throw new TestingController.TestingToolException(((HtmlHelper) null).TranslateTag("Unknown Gateway: ") + gatewayID.ToString());
      if (!MonnitSession.CurrentCustomer.CanSeeGateway(gateway.GatewayID))
        throw new TestingController.TestingToolException(((HtmlHelper) null).TranslateTag("Unauthorized Access to Gateway: ") + gateway.GatewayID.ToString());
      return gateway;
    }
  }
}
