// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.RuleController
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
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

public class RuleController : ThemeController
{
  [NoCache]
  [AuthorizeDefault]
  public ActionResult Index()
  {
    return !MonnitSession.CustomerCan("Sensor_View_Notifications") ? this.PermissionError(methodName: nameof (Index), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 37) : (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public ActionResult RuleFilter(bool? isActive, int? eventType, string name, string sort)
  {
    if (!MonnitSession.CustomerCan("Sensor_View_Notifications"))
      return (ActionResult) this.Content("Failed");
    Notification.RuleFilterResult model = Notification.RuleFilter(MonnitSession.CurrentCustomer.AccountID, isActive, eventType, name, new bool?(false));
    if (string.IsNullOrWhiteSpace(sort) || !string.IsNullOrWhiteSpace(sort) && sort == "Rule Name - Asc")
    {
      model.Notifications = model.Notifications.OrderBy<Notification, string>((Func<Notification, string>) (m => m.Name)).ToList<Notification>();
    }
    else
    {
      switch (sort)
      {
        case "Rule Name - Desc":
          model.Notifications = model.Notifications.OrderByDescending<Notification, string>((Func<Notification, string>) (m => m.Name)).ToList<Notification>();
          break;
        case "Last Sent Date - Desc":
          model.Notifications = model.Notifications.OrderBy<Notification, DateTime>((Func<Notification, DateTime>) (m => m.LastSent)).ToList<Notification>();
          break;
        case "Last Sent Date - Asc":
          model.Notifications = model.Notifications.OrderByDescending<Notification, DateTime>((Func<Notification, DateTime>) (m => m.LastSent)).ToList<Notification>();
          break;
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalNotis", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = RuleController.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__1.\u003C\u003Ep__0, this.ViewBag, model.TotalNotifications);
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__1.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__1.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "EventCount", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = RuleController.\u003C\u003Eo__1.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__1.\u003C\u003Ep__1, this.ViewBag, model.Notifications.Count);
    return (ActionResult) this.PartialView("_RulesList", (object) model);
  }

  [AuthorizeDefault]
  public ActionResult SensorForRuleList(long? networkID)
  {
    MonnitSession.SensorListFilters.CSNetID = networkID ?? long.MinValue;
    int totalSensors;
    List<SensorGroupSensorModel> model = SensorControllerBase.GetSensorList(MonnitSession.CurrentCustomer.AccountID, out totalSensors);
    if (MonnitSession.NotificationInProgress.NotificationClass == eNotificationClass.Advanced)
    {
      AdvancedNotification advancedNotification = AdvancedNotification.Load(MonnitSession.NotificationInProgress.AdvancedNotificationID);
      List<SensorGroupSensorModel> groupSensorModelList = new List<SensorGroupSensorModel>();
      if (advancedNotification != null)
      {
        foreach (SensorGroupSensorModel groupSensorModel in model)
        {
          if (advancedNotification.CanAdd((object) groupSensorModel.Sensor))
          {
            int num1;
            if (networkID.HasValue)
            {
              long? nullable = networkID;
              long num2 = 0;
              if (!(nullable.GetValueOrDefault() < num2 & nullable.HasValue))
              {
                long csNetId = groupSensorModel.Sensor.CSNetID;
                nullable = networkID;
                long valueOrDefault = nullable.GetValueOrDefault();
                num1 = csNetId == valueOrDefault & nullable.HasValue ? 1 : 0;
                goto label_9;
              }
            }
            num1 = 1;
label_9:
            if (num1 != 0)
              groupSensorModelList.Add(groupSensorModel);
          }
        }
        model = groupSensorModelList;
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalSensors", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = RuleController.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__2.\u003C\u003Ep__0, this.ViewBag, totalSensors);
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "FilteredSensors", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = RuleController.\u003C\u003Eo__2.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__2.\u003C\u003Ep__1, this.ViewBag, model.Count);
    return (ActionResult) this.PartialView("_SensorForRuleList", (object) model);
  }

  [AuthorizeDefault]
  public ActionResult GatewayForRuleList(long? networkID)
  {
    MonnitSession.GatewayListFilters.CSNetID = networkID ?? long.MinValue;
    List<Gateway> model = CSNetControllerBase.GetGatewayList(out int _);
    if (MonnitSession.NotificationInProgress.NotificationClass == eNotificationClass.Advanced)
    {
      AdvancedNotification advancedNotification = AdvancedNotification.Load(MonnitSession.NotificationInProgress.AdvancedNotificationID);
      List<Gateway> gatewayList = new List<Gateway>();
      if (advancedNotification != null)
      {
        foreach (Gateway gateway in model)
        {
          if (advancedNotification.CanAdd((object) gateway))
          {
            int num;
            if (MonnitSession.GatewayListFilters.CSNetID >= 0L)
            {
              long csNetId = gateway.CSNetID;
              long? nullable = networkID;
              long valueOrDefault = nullable.GetValueOrDefault();
              num = csNetId == valueOrDefault & nullable.HasValue ? 1 : 0;
            }
            else
              num = 1;
            if (num != 0)
              gatewayList.Add(gateway);
          }
        }
        model = gatewayList;
      }
    }
    return (ActionResult) this.PartialView("_GatewayForRuleList", (object) model);
  }

  [AuthorizeDefault]
  public ActionResult ChooseType()
  {
    RuleController.ResetNotificationInProgress();
    return !MonnitSession.CustomerCan("Notification_Edit") ? this.PermissionError(methodName: nameof (ChooseType), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 201) : (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public ActionResult ChooseSensorTemplate(long? id, bool isGateway)
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
    {
      RuleController.ResetNotificationInProgress();
      return this.PermissionError(methodName: nameof (ChooseSensorTemplate), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 213);
    }
    if (MonnitSession.NotificationInProgress.AccountID < 1L)
      return (ActionResult) this.Redirect("/Rule/ChooseType/");
    Notification notificationInProgress = MonnitSession.NotificationInProgress;
    Gateway gate = (Gateway) null;
    Sensor sens = (Sensor) null;
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "IsGateway", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__0, this.ViewBag, isGateway);
    if (id.HasValue)
    {
      if (isGateway)
      {
        gate = Gateway.Load(id ?? long.MinValue);
        if (gate == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(gate.CSNetID))
          return this.PermissionError(methodName: nameof (ChooseSensorTemplate), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 231);
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Gateway, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Gateway", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__1, this.ViewBag, gate);
        notificationInProgress.GatewayID = gate.GatewayID;
        if (notificationInProgress.GatewaysThatNotify.Where<Gateway>((Func<Gateway, bool>) (m => m.GatewayID == gate.GatewayID)).Count<Gateway>() < 1)
          notificationInProgress.AddGateway(gate);
      }
      else
      {
        sens = Sensor.Load(id ?? long.MinValue);
        if (sens == null || !MonnitSession.CurrentCustomer.CanSeeAccount(sens.AccountID))
          return this.PermissionError(methodName: nameof (ChooseSensorTemplate), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 245);
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, Sensor, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Sensor", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__2.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__2, this.ViewBag, sens);
        notificationInProgress.SensorID = sens.SensorID;
        notificationInProgress.ApplicationID = sens.ApplicationID;
        if (notificationInProgress.SensorsThatNotify.Where<SensorDatum>((Func<SensorDatum, bool>) (m => m.sens.SensorID == sens.SensorID)).Count<SensorDatum>() < 1)
          notificationInProgress.AddSensor(sens);
      }
    }
    List<Notification> source1 = Notification.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
    switch (notificationInProgress.NotificationClass)
    {
      case eNotificationClass.Application:
        List<eDatumStruct> source2 = new List<eDatumStruct>();
        int di = 0;
        foreach (eDatumType datumType in MonnitApplicationBase.GetDatumTypes(sens.ApplicationID))
        {
          source2.Add(new eDatumStruct(datumType, di));
          ++di;
        }
        int j = 0;
        List<eDatumStruct> eDatumStructList = new List<eDatumStruct>(source2.Select<eDatumStruct, eDatumStruct>((Func<eDatumStruct, eDatumStruct>) (edat =>
        {
          string datumName = sens.GetDatumName(j++);
          eDatumStruct eDatumStruct = edat;
          string customname = eDatumStruct.customname;
          eDatumStruct = edat;
          int datumindex = eDatumStruct.datumindex;
          eDatumStruct = edat;
          string val = eDatumStruct.val;
          return new eDatumStruct(datumName, customname, datumindex, val);
        })));
        MonnitSession.NotificationInProgress.eDatumType = eDatumType.Error;
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, List<eDatumStruct>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "eDatumStructList", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj4 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__3.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__3, this.ViewBag, eDatumStructList);
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, Sensor, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SensorPicked", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj5 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__4.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__4, this.ViewBag, sens);
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "PartialName", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj6 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__5.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__5, this.ViewBag, "_RuleCreateApplication");
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingRulePartialName", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj7 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__6.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__6, this.ViewBag, "_ExistingRuleApplication");
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingRulesList", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj8 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__7.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__7, this.ViewBag, new List<Notification>());
        break;
      case eNotificationClass.Inactivity:
        MonnitSession.NotificationInProgress.CompareValue = "60";
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "PartialName", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj9 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__8.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__8, this.ViewBag, "_RuleCreateInactivity");
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__9 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingRulePartialName", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj10 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__9.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__9, this.ViewBag, "_ExistingRuleOther");
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__10 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingRulesList", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj11 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__10.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__10, this.ViewBag, source1.Where<Notification>((Func<Notification, bool>) (c => c.NotificationClass == eNotificationClass.Inactivity)).ToList<Notification>());
        break;
      case eNotificationClass.Low_Battery:
        MonnitSession.NotificationInProgress.CompareValue = "15";
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__11 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "PartialName", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj12 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__11.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__11, this.ViewBag, "_RuleCreateLowBattery");
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__12 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingRulePartialName", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj13 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__12.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__12, this.ViewBag, "_ExistingRuleOther");
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__13 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__13 = CallSite<Func<CallSite, object, List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingRulesList", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj14 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__13.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__13, this.ViewBag, source1.Where<Notification>((Func<Notification, bool>) (c => c.NotificationClass == eNotificationClass.Low_Battery)).ToList<Notification>());
        break;
      case eNotificationClass.Advanced:
        MonnitSession.NotificationInProgress.CompareValue = "";
        MonnitSession.NotificationInProgress.NotificationClass = eNotificationClass.Advanced;
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__14 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__14 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "PartialName", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj15 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__14.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__14, this.ViewBag, "_RuleCreateAdvanced");
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__15 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__15 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingRulePartialName", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj16 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__15.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__15, this.ViewBag, "_ExistingRuleOther");
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__16 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__16 = CallSite<Func<CallSite, object, List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingRulesList", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj17 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__16.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__16, this.ViewBag, source1.Where<Notification>((Func<Notification, bool>) (c => c.NotificationClass == eNotificationClass.Advanced)).ToList<Notification>());
        this.ViewData["AdvancedNotification"] = (object) AdvancedNotification.Load(notificationInProgress.AdvancedNotificationID);
        List<AdvancedNotificationParameter> notificationParameterList = AdvancedNotificationParameter.LoadByAdvancedNotificationID(notificationInProgress.AdvancedNotificationID);
        this.ViewData["Params"] = (object) notificationParameterList;
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        List<AdvancedNotificationParameterValue> notificationParameterValueList = AdvancedNotificationParameterValue.LoadByNotificationID(notificationInProgress.NotificationID);
        foreach (AdvancedNotificationParameter notificationParameter in notificationParameterList)
        {
          AdvancedNotificationParameter p = notificationParameter;
          if (!(p.ParameterName.ToLower() == "sensorid"))
          {
            try
            {
              string key = p.AdvancedNotificationParameterID.ToString();
              AdvancedNotificationParameterValue notificationParameterValue = notificationParameterValueList.Find((Predicate<AdvancedNotificationParameterValue>) (v => v.AdvancedNotificationParameterID == p.AdvancedNotificationParameterID));
              if (notificationParameterValue != null)
                dictionary.Add(key, notificationParameterValue.ParameterValue);
            }
            catch
            {
            }
          }
        }
        this.Session["AdvancedNotificationParams"] = (object) dictionary;
        break;
      case eNotificationClass.Timed:
        MonnitSession.NotificationInProgress.CompareValue = "0:00";
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__17 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__17 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "PartialName", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj18 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__17.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__17, this.ViewBag, "_RuleCreateTimed");
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__18 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__18 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingRulePartialName", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj19 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__18.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__18, this.ViewBag, "_ExistingRuleScheduled");
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__5.\u003C\u003Ep__19 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__5.\u003C\u003Ep__19 = CallSite<Func<CallSite, object, List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ExistingRulesList", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj20 = RuleController.\u003C\u003Eo__5.\u003C\u003Ep__19.Target((CallSite) RuleController.\u003C\u003Eo__5.\u003C\u003Ep__19, this.ViewBag, source1.Where<Notification>((Func<Notification, bool>) (c => c.NotificationClass == eNotificationClass.Timed)).ToList<Notification>());
        break;
    }
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public ActionResult CreateApplicationRule(string ruleClass, long? advancedNotificationID)
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
    {
      RuleController.ResetNotificationInProgress();
      return this.PermissionError(methodName: nameof (CreateApplicationRule), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 346);
    }
    try
    {
      MonnitSession.SensorListFilters.Clear();
      MonnitSession.GatewayListFilters.Clear();
      if (MonnitSession.NotificationInProgress.NotificationType > (eNotificationType) 0)
        MonnitSession.NotificationInProgress = new Notification();
      if (!string.IsNullOrEmpty(ruleClass))
      {
        eNotificationClass notificationClass = (eNotificationClass) Enum.Parse(typeof (eNotificationClass), ruleClass);
        bool flag1 = true;
        bool flag2 = true;
        switch (notificationClass)
        {
          case eNotificationClass.Application:
            MonnitSession.NotificationInProgress.NotificationClass = eNotificationClass.Application;
            flag2 = false;
            break;
          case eNotificationClass.Inactivity:
            MonnitSession.NotificationInProgress.NotificationClass = eNotificationClass.Inactivity;
            break;
          case eNotificationClass.Low_Battery:
            MonnitSession.NotificationInProgress.NotificationClass = eNotificationClass.Low_Battery;
            break;
          case eNotificationClass.Advanced:
            MonnitSession.AdvancedNotificationParameterValuesInProgress = (List<AdvancedNotificationParameterValue>) null;
            MonnitSession.NotificationInProgress.AdvancedNotificationID = advancedNotificationID ?? long.MinValue;
            MonnitSession.NotificationInProgress.NotificationClass = eNotificationClass.Advanced;
            AdvancedNotification advancedNotification = AdvancedNotification.Load(MonnitSession.NotificationInProgress.AdvancedNotificationID);
            if (advancedNotification != null)
            {
              flag1 = advancedNotification.HasSensorList || advancedNotification.UseDatums;
              flag2 = advancedNotification.HasGatewayList;
              break;
            }
            flag1 = false;
            flag2 = false;
            break;
          case eNotificationClass.Timed:
            MonnitSession.NotificationInProgress.NotificationClass = eNotificationClass.Timed;
            return (ActionResult) this.Redirect("/Rule/ChooseSensorTemplate?isGateway=false");
          default:
            flag2 = false;
            break;
        }
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__6.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__6.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ShowSensors", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = RuleController.\u003C\u003Eo__6.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__6.\u003C\u003Ep__0, this.ViewBag, flag1);
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__6.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__6.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ShowGateways", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = RuleController.\u003C\u003Eo__6.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__6.\u003C\u003Ep__1, this.ViewBag, flag2);
      }
    }
    catch
    {
    }
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  [HttpGet]
  public ActionResult CreateAdvancedRule()
  {
    if (!MonnitSession.CurrentCustomer.Account.CurrentSubscription.AccountSubscriptionType.Can("use_advanced_notifications"))
      return this.PermissionError(methodName: nameof (CreateAdvancedRule), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 426);
    Notification model = new Notification();
    model.NotificationClass = eNotificationClass.Advanced;
    model.AccountID = MonnitSession.CurrentCustomer.AccountID;
    MonnitSession.NotificationInProgress = model;
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult CreateApplicationCondition(int id, int datumIndex = 0)
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return this.PermissionError(methodName: nameof (CreateApplicationCondition), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 444);
    Notification model = MonnitSession.NotificationInProgress;
    if (model == null || model.AccountID < 0L)
      model = new Notification();
    model.eDatumType = (eDatumType) id;
    model.DatumIndex = datumIndex;
    model.AccountID = MonnitSession.CurrentCustomer.AccountID;
    model.SensorsThatNotify[0].DatumIndex = datumIndex;
    MonnitSession.NotificationInProgress = model;
    return (ActionResult) this.PartialView("_CreateApplicationCondition", (object) model);
  }

  [AuthorizeDefault]
  public ActionResult AddRuleConditions(
    string compareType,
    string compareValue,
    string scale,
    long? advancedNotificationID,
    FormCollection collection)
  {
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return (ActionResult) this.Content("Failed");
      }
      Notification notificationInProgress = MonnitSession.NotificationInProgress;
      notificationInProgress.CompareValue = compareValue;
      notificationInProgress.CompareType = (eCompareType) Enum.Parse(typeof (eCompareType), compareType);
      notificationInProgress.Scale = scale;
      switch (notificationInProgress.NotificationClass)
      {
        case eNotificationClass.Application:
          notificationInProgress.NotificationClass = eNotificationClass.Application;
          notificationInProgress.NotificationText = $"{notificationInProgress.Name}<br/>Device: {{Name}} ({{ID}})<br/>Reading: {{Reading}}";
          notificationInProgress.SMSText = notificationInProgress.NotificationText;
          notificationInProgress.VoiceText = notificationInProgress.NotificationText;
          notificationInProgress.PushMsgText = notificationInProgress.NotificationText;
          notificationInProgress.LocalAlertText = $"{notificationInProgress.Name}Device: {{Name}} ({{ID}})Reading: {{Reading}}";
          if (!MonnitApplicationBase.VerifyNotificationValues(notificationInProgress, notificationInProgress.Scale))
          {
            DataTypeBase.VerifyNotificationValues(notificationInProgress, notificationInProgress.Scale);
            break;
          }
          break;
        case eNotificationClass.Inactivity:
          notificationInProgress.NotificationClass = eNotificationClass.Inactivity;
          notificationInProgress.NotificationText = $"{notificationInProgress.Name}<br/>Device: {{Name}} ({{ID}})<br/>Reading: {{Reading}}";
          notificationInProgress.SMSText = notificationInProgress.NotificationText;
          notificationInProgress.VoiceText = notificationInProgress.NotificationText;
          notificationInProgress.PushMsgText = notificationInProgress.NotificationText;
          notificationInProgress.LocalAlertText = $"{notificationInProgress.Name}Device: {{Name}} ({{ID}})Reading: {{Reading}}";
          notificationInProgress.IgnoreMaintenanceWindow = collection["IgnoreMaintenanceWindow"].ToBool();
          break;
        case eNotificationClass.Low_Battery:
          notificationInProgress.NotificationClass = eNotificationClass.Low_Battery;
          notificationInProgress.NotificationText = $"{notificationInProgress.Name}<br/>Device: {{Name}} ({{ID}})<br/>Reading: {{Reading}}";
          notificationInProgress.SMSText = notificationInProgress.NotificationText;
          notificationInProgress.VoiceText = notificationInProgress.NotificationText;
          notificationInProgress.PushMsgText = notificationInProgress.NotificationText;
          notificationInProgress.LocalAlertText = $"{notificationInProgress.Name}Device: {{Name}} ({{ID}})Reading: {{Reading}}";
          break;
        case eNotificationClass.Advanced:
          notificationInProgress.AdvancedNotificationID = advancedNotificationID ?? long.MinValue;
          notificationInProgress.NotificationClass = eNotificationClass.Advanced;
          AdvancedNotification advancedNotification = AdvancedNotification.Load(notificationInProgress.AdvancedNotificationID);
          if (advancedNotification == null)
            return (ActionResult) this.Content("Failed");
          notificationInProgress.NotificationText = $"{notificationInProgress.Name}<br/>Device: {{Name}} ({{ID}})<br/>Reading: {{Reading}}";
          notificationInProgress.SMSText = notificationInProgress.NotificationText;
          notificationInProgress.VoiceText = notificationInProgress.NotificationText;
          notificationInProgress.PushMsgText = notificationInProgress.NotificationText;
          notificationInProgress.LocalAlertText = $"{notificationInProgress.Name}Device: {{Name}} ({{ID}})Reading: {{Reading}}";
          string str1 = "";
          List<AdvancedNotificationParameterValue> notificationParameterValueList = new List<AdvancedNotificationParameterValue>();
          List<AdvancedNotificationParameter> notificationParameterList = AdvancedNotificationParameter.LoadByAdvancedNotificationID(advancedNotification.AdvancedNotificationID);
          Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
          foreach (AdvancedNotificationParameter notificationParameter in notificationParameterList)
          {
            if (!(notificationParameter.ParameterName.ToLower() == "sensorid"))
            {
              FormCollection formCollection = collection;
              long notificationParameterId = notificationParameter.AdvancedNotificationParameterID;
              string name = "ParamID_" + notificationParameterId.ToString();
              string parameterValue = formCollection[name].Split(',')[0];
              AdvancedNotificationParameterValue notificationParameterValue = new AdvancedNotificationParameterValue(notificationParameter.AdvancedNotificationParameterID, notificationInProgress.NotificationID, parameterValue);
              notificationParameterValueList.Add(notificationParameterValue);
              if (notificationParameter.IsValid(notificationParameterValue, parameterValue))
              {
                notificationParameterValue.ParameterValue = parameterValue;
                Dictionary<string, string> dictionary2 = dictionary1;
                notificationParameterId = notificationParameter.AdvancedNotificationParameterID;
                string key = notificationParameterId.ToString();
                string str2 = parameterValue;
                dictionary2.Add(key, str2);
              }
              else
                str1 += $"{notificationParameter.ParameterName.Replace("_", " ")} is not valid. ";
            }
          }
          if (!string.IsNullOrEmpty(str1))
            return (ActionResult) this.Content("Failed: " + str1);
          MonnitSession.AdvancedNotificationParameterValuesInProgress = notificationParameterValueList;
          break;
        case eNotificationClass.Timed:
          string compareValue1 = notificationInProgress.CompareValue;
          notificationInProgress.NotificationByTime.ScheduledHour = collection["ScheduledHour"].ToInt() + collection["AMorPM"].ToInt();
          notificationInProgress.NotificationByTime.ScheduledMinute = collection["ScheduledMinute"].ToInt();
          string str3 = $"{notificationInProgress.NotificationByTime.ScheduledHour}:{notificationInProgress.NotificationByTime.ScheduledMinute}";
          if (compareValue1 != str3)
            notificationInProgress.NotificationByTime.NextEvaluationDate = DateTime.MinValue;
          notificationInProgress.CompareValue = str3;
          MonnitSession.NotificationInProgress.NotificationClass = eNotificationClass.Timed;
          notificationInProgress.NotificationText = $"{notificationInProgress.Name}<br/>Device: {{Name}} ({{ID}})<br/>Reading: {{Reading}}";
          notificationInProgress.SMSText = notificationInProgress.NotificationText;
          notificationInProgress.VoiceText = notificationInProgress.NotificationText;
          notificationInProgress.PushMsgText = notificationInProgress.NotificationText;
          notificationInProgress.LocalAlertText = $"{notificationInProgress.Name}Device: {{Name}} ({{ID}})Reading: {{Reading}}";
          notificationInProgress.NotificationByTime.Save();
          notificationInProgress.NotificationByTimeID = notificationInProgress.NotificationByTime.NotificationByTimeID;
          break;
      }
      MonnitSession.NotificationInProgress = notificationInProgress;
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult CheckAppDatum(long id)
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    List<eDatumStruct> eDatumStructList = new List<eDatumStruct>();
    Sensor sensor = Sensor.LoadByApplicationID(id, 1).FirstOrDefault<Sensor>();
    int di = 0;
    foreach (eDatumType datumType in MonnitApplicationBase.GetDatumTypes(id))
    {
      eDatumStructList.Add(new eDatumStruct(datumType, di));
      ++di;
    }
    if (sensor != null)
    {
      int j = 0;
      eDatumStructList = new List<eDatumStruct>(eDatumStructList.Select<eDatumStruct, eDatumStruct>((Func<eDatumStruct, eDatumStruct>) (edat =>
      {
        string datumName = sensor.GetDatumName(j++);
        eDatumStruct eDatumStruct = edat;
        string customname = eDatumStruct.customname;
        eDatumStruct = edat;
        int datumindex = eDatumStruct.datumindex;
        eDatumStruct = edat;
        string val = eDatumStruct.val;
        return new eDatumStruct(datumName, customname, datumindex, val);
      })));
    }
    return (ActionResult) this.View((object) eDatumStructList);
  }

  [AuthorizeDefault]
  public ActionResult AvailableRulesBySensorDatum(long id, int datumType)
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
    {
      RuleController.ResetNotificationInProgress();
      return (ActionResult) this.Content("Failed: Unauthorized");
    }
    if (MonnitSession.NotificationInProgress.AccountID < 1L)
      return (ActionResult) this.Content("Failed");
    Sensor sens = Sensor.Load(id);
    if (sens == null)
      return (ActionResult) this.Content("Failed: No Sensor found");
    this.ViewData["Sensor"] = (object) sens;
    return (ActionResult) this.PartialView("AvailableRules", (object) AvailableNotificationBySensorModel.Load(sens).Where<AvailableNotificationBySensorModel>((Func<AvailableNotificationBySensorModel, bool>) (c => c.Notification.eDatumType == (eDatumType) datumType)).ToList<AvailableNotificationBySensorModel>());
  }

  [AuthorizeDefault]
  public ActionResult AddDevicetoExistingNotification(
    long? sensorID,
    long? gatewayID,
    long[] notificationIDs,
    int[] datumindex = null)
  {
    if (datumindex == null)
    {
      datumindex = new int[notificationIDs.Length];
      for (int index = 0; index < notificationIDs.Length; ++index)
        datumindex[index] = 0;
    }
    Sensor sensor = (Sensor) null;
    Gateway gateway = (Gateway) null;
    long ID = long.MinValue;
    long? nullable = sensorID;
    long num1 = 0;
    if (nullable.GetValueOrDefault() > num1 & nullable.HasValue)
      sensor = Sensor.Load(sensorID ?? long.MinValue);
    nullable = gatewayID;
    long num2 = 0;
    if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
      gateway = Gateway.Load(gatewayID ?? long.MinValue);
    if (sensor != null)
      ID = sensor.CSNetID;
    if (gateway != null)
      ID = gateway.CSNetID;
    if (ID <= 0L)
      return (ActionResult) this.Content("Unauthorized");
    CSNet csNet = CSNet.Load(ID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.Content("Unauthorized");
    Account account = Account.Load(csNet.AccountID);
    int num3 = 0;
    foreach (long notificationId in notificationIDs)
    {
      Notification DBObject = Notification.Load(notificationId);
      if (DBObject != null && DBObject.AccountID == csNet.AccountID)
      {
        if (sensor != null && DBObject.NotificationClass == eNotificationClass.Application)
        {
          string changeRecord = $"{{\"SensorID\": \"{sensor.SensorID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
          DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned sensor datum to notification");
          DBObject.AddSensor(sensor, datumindex[num3++]);
        }
        else if (sensor != null)
        {
          string changeRecord = $"{{\"SensorID\": \"{sensor.SensorID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
          DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned sensor to notification");
          DBObject.AddSensor(sensor);
        }
        if (gateway != null)
        {
          string changeRecord = $"{{\"GatewayID\": \"{gateway.GatewayID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
          DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, account.AccountID, "Assigned gateway to notification");
          DBObject.AddGateway(gateway);
        }
      }
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult RuleComplete(long id)
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
    {
      RuleController.ResetNotificationInProgress();
      return this.PermissionError(methodName: nameof (RuleComplete), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 712);
    }
    Notification notification = Notification.Load(id);
    if (notification == null || !MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
      return (ActionResult) this.Redirect("/Rule/ChooseType/");
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__13.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__13.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiID", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = RuleController.\u003C\u003Eo__13.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__13.\u003C\u003Ep__0, this.ViewBag, id);
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__13.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__13.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "RuleName", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = RuleController.\u003C\u003Eo__13.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__13.\u003C\u003Ep__1, this.ViewBag, notification.Name);
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public ActionResult NameRule()
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
    {
      RuleController.ResetNotificationInProgress();
      return this.PermissionError(methodName: nameof (NameRule), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 738);
    }
    return MonnitSession.NotificationInProgress.NotificationClass.ToInt() == 0 || MonnitSession.NotificationInProgress.NotificationClass == eNotificationClass.Application && string.IsNullOrEmpty(MonnitSession.NotificationInProgress.CompareValue) ? (ActionResult) this.Redirect("/Rule/ChooseType/") : (ActionResult) this.View();
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult NameRule(string name)
  {
    string content = "";
    if (!MonnitSession.CustomerCan("Notification_Edit"))
    {
      RuleController.ResetNotificationInProgress();
      content = "Failed: Insufficient permissions";
    }
    if (MonnitSession.NotificationInProgress.AccountID < 1L)
      content = "Failed: Session timed out";
    if (!string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(content))
    {
      try
      {
        name = name.SanitizeHTMLEvent();
        MonnitSession.NotificationInProgress.Name = name;
        MonnitSession.NotificationInProgress.Save();
        Account account = Account.Load(MonnitSession.NotificationInProgress.AccountID);
        long notificationId = MonnitSession.NotificationInProgress.NotificationID;
        if (account != null)
          MonnitSession.NotificationInProgress.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Created notification settings");
        this.SaveNotificationAndCreateSchedule(MonnitSession.NotificationInProgress);
        if (MonnitSession.NotificationInProgress.AdvancedNotificationID > 0L)
        {
          List<AdvancedNotificationParameterValue> valuesInProgress = MonnitSession.AdvancedNotificationParameterValuesInProgress;
          if (valuesInProgress.Count > 0)
          {
            foreach (AdvancedNotificationParameterValue notificationParameterValue in valuesInProgress)
            {
              notificationParameterValue.NotificationID = notificationId;
              notificationParameterValue.Save();
            }
            MonnitSession.NotificationInProgress.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID, "Edited notification");
          }
        }
        foreach (NotificationRecipient notificationRecipient in MonnitSession.NotificationRecipientsInProgress)
        {
          if (account != null)
          {
            if (notificationRecipient.NotificationType == eNotificationType.SystemAction && notificationRecipient.SerializedRecipientProperties == "-1")
              notificationRecipient.SerializedRecipientProperties = MonnitSession.NotificationInProgress.NotificationID.ToString();
            notificationRecipient.NotificationID = notificationId;
            notificationRecipient.Save();
            switch (notificationRecipient.NotificationType)
            {
              case eNotificationType.Email:
              case eNotificationType.SMS:
              case eNotificationType.Both:
              case eNotificationType.Phone:
              case eNotificationType.Android:
              case eNotificationType.Apple:
              case eNotificationType.Group:
                MonnitSession.NotificationInProgress.HasUserNotificationAction = true;
                break;
              case eNotificationType.Local_Notifier:
              case eNotificationType.Local_Notifier_Display:
                MonnitSession.NotificationInProgress.HasLocalAlertAction = true;
                break;
              case eNotificationType.Control:
                MonnitSession.NotificationInProgress.HasControlAction = true;
                break;
              case eNotificationType.HTTP:
              case eNotificationType.SystemAction:
                MonnitSession.NotificationInProgress.HasSystemAction = true;
                break;
              case eNotificationType.Thermostat:
                MonnitSession.NotificationInProgress.HasThermostatAction = true;
                break;
              case eNotificationType.ResetAccumulator:
                MonnitSession.NotificationInProgress.HasResetAccAction = true;
                break;
            }
            MonnitSession.NotificationInProgress.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, string.Format("{{\"CustomerID\": \"{0}\",\"DeviceID\": \"{4}\", \"NotifcationID\" : \"{1}\", \"DelayMinutes\" : \"{2}\", \"NotifcationType\" : \"{3}\" }} ", (object) notificationRecipient.CustomerToNotifyID.ToStringSafe(), (object) notificationId, (object) notificationRecipient.DelayMinutes, (object) notificationRecipient.NotificationType.ToString(), (object) notificationRecipient.DeviceToNotifyID.ToStringSafe()), account.AccountID, "Assigned recipient to notification");
          }
        }
        MonnitSession.NotificationInProgress.Save();
        foreach (SensorDatum sensorDatum in MonnitSession.NotificationInProgress.SensorsThatNotify)
        {
          MonnitSession.NotificationInProgress.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, $"{{\"SensorID\": \"{sensorDatum.sens.SensorID}\", \"NotifcationID\" : \"{notificationId}\" }} ", account.AccountID, "Assigned sensor datum to notification");
          SensorNotification.AddSensor(MonnitSession.NotificationInProgress.NotificationID, sensorDatum.sens.SensorID, sensorDatum.DatumIndex);
        }
        foreach (Gateway gateway in MonnitSession.NotificationInProgress.GatewaysThatNotify)
        {
          MonnitSession.NotificationInProgress.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, $"{{\"GatewayID\": \"{gateway.GatewayID}\", \"NotifcationID\" : \"{notificationId}\" }} ", account.AccountID, "Assigned gateway to notification");
          GatewayNotification.AddGateway(notificationId, gateway.GatewayID);
        }
        content = "Success|" + notificationId.ToString();
        RuleController.ResetNotificationInProgress();
      }
      catch (Exception ex)
      {
        ex.Log($"RuleController.NameRule[name: {name}] ");
        content = "Failed";
      }
    }
    return (ActionResult) this.Content(content);
  }

  private void SaveNotificationAndCreateSchedule(Notification Model)
  {
    try
    {
      if (Model.MondayScheduleID == long.MinValue)
      {
        Model.MondaySchedule.Save();
        Model.MondayScheduleID = Model.MondaySchedule.NotificationScheduleID;
        Model.TuesdaySchedule.Save();
        Model.TuesdayScheduleID = Model.TuesdaySchedule.NotificationScheduleID;
        Model.WednesdaySchedule.Save();
        Model.WednesdayScheduleID = Model.WednesdaySchedule.NotificationScheduleID;
        Model.ThursdaySchedule.Save();
        Model.ThursdayScheduleID = Model.ThursdaySchedule.NotificationScheduleID;
        Model.FridaySchedule.Save();
        Model.FridayScheduleID = Model.FridaySchedule.NotificationScheduleID;
        Model.SaturdaySchedule.Save();
        Model.SaturdayScheduleID = Model.SaturdaySchedule.NotificationScheduleID;
        Model.SundaySchedule.Save();
        Model.SundayScheduleID = Model.SundaySchedule.NotificationScheduleID;
      }
      Model.Version = "2";
      Model.Save();
      if (MonnitSession.CurrentCustomer.Account == null)
        return;
      Model.LogAuditData(eAuditAction.Create, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, Model.AccountID, "Create notification schedule");
    }
    catch (Exception ex)
    {
      ex.Log($"RuleController.SaveNotificationAndCreateSchedule[NotificationID: {Model.NotificationID.ToString()}] ");
    }
  }

  private static void ResetNotificationInProgress()
  {
    MonnitSession.AdvancedNotificationParameterValuesInProgress = (List<AdvancedNotificationParameterValue>) null;
    MonnitSession.NotificationRecipientsInProgress = (List<NotificationRecipient>) null;
    MonnitSession.NotificationInProgress = (Notification) null;
  }

  [AuthorizeDefault]
  [ValidateInput(false)]
  [HttpPost]
  public ActionResult SaveAlertNotificationSettings(FormCollection collection)
  {
    string str1 = "";
    string content;
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(MonnitSession.NotificationInProgress.AccountID))
      content = "misc: Insufficient permissions for account access.";
    else if (!MonnitSession.CustomerCan("Notification_Edit"))
    {
      content = "misc: Insufficient permissions for editing notifications.";
    }
    else
    {
      try
      {
        if (string.IsNullOrWhiteSpace(str1))
        {
          MonnitSession.NotificationInProgress.Subject = collection["subject"];
          MonnitSession.NotificationInProgress.NotificationText = collection["emailMsg"];
          MonnitSession.NotificationInProgress.SMSText = "";
          MonnitSession.NotificationInProgress.VoiceText = "";
        }
        content = "Success";
      }
      catch (Exception ex)
      {
        ex.Log("RuleController.SaveAlertNotificationSettings");
        string str2 = "misc:Failed";
        content = string.IsNullOrWhiteSpace(str1) ? str2 : $"{str1}|{str2}";
      }
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public ActionResult SendEmailNotification()
  {
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (SendEmailNotification), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 1024 /*0x0400*/);
      }
      if (MonnitSession.NotificationInProgress.AccountID < 1L)
        return (ActionResult) this.Redirect("/Rule/ChooseType/");
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__19.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__19.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, eNotificationType, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationType", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = RuleController.\u003C\u003Eo__19.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__19.\u003C\u003Ep__0, this.ViewBag, eNotificationType.Email);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__19.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__19.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = RuleController.\u003C\u003Eo__19.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__19.\u003C\u003Ep__1, this.ViewBag, MonnitSession.NotificationRecipientsInProgress);
      MonnitSession.NotificationInProgress.NotificationType = eNotificationType.Email;
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.SendEmailNotification ");
    }
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public ActionResult SendAlert()
  {
    try
    {
      List<CustomerGroup> customerGroupList = CustomerGroup.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__20.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__20.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<CustomerGroup>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserGroups", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RuleController.\u003C\u003Eo__20.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__20.\u003C\u003Ep__0, this.ViewBag, customerGroupList);
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.SendAlert ");
    }
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public ActionResult GetUserGroupDictionary(long groupId)
  {
    return (ActionResult) this.Json((object) CustomerGroupLink.LoadByCustomerGroupID(groupId), JsonRequestBehavior.AllowGet);
  }

  [AuthorizeDefault]
  [ValidateInput(false)]
  [HttpPost]
  public ActionResult SaveEmailNotificationSettings(FormCollection collection)
  {
    string content1 = "";
    string content2;
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(MonnitSession.NotificationInProgress.AccountID))
      content2 = "misc: Insufficient permissions for account access.";
    else if (!MonnitSession.CustomerCan("Notification_Edit"))
    {
      content2 = "misc: Insufficient permissions for editing notifications.";
    }
    else
    {
      try
      {
        if (!string.IsNullOrEmpty(collection["subject"]) && collection["subject"].HasScriptTag())
        {
          content1 = "subject: Script tags not permitted.";
          return (ActionResult) this.Content(content1);
        }
        if (string.IsNullOrEmpty(collection["emailMsg"]) || collection["emailMsg"] == "<p><br></p>")
        {
          MonnitSession.NotificationInProgress.NotificationText = "Device: {Name} ({ID})Reading: {Reading}";
          content1 = "Success";
          return (ActionResult) this.Content(content1);
        }
        collection["subject"] = collection["subject"].SanitizeHTMLEvent();
        collection["emailMsg"] = collection["emailMsg"].SanitizeButAllowSomeHTMLEvent();
        MonnitSession.NotificationInProgress.Subject = collection["subject"];
        MonnitSession.NotificationInProgress.NotificationText = collection["emailMsg"];
        content2 = "Success";
      }
      catch (Exception ex)
      {
        ex.Log("RuleController.SaveEmailNotification ");
        string str = "misc:Failed";
        content2 = string.IsNullOrWhiteSpace(content1) ? str : $"{content1}|{str}";
      }
    }
    return (ActionResult) this.Content(content2);
  }

  [AuthorizeDefault]
  public ActionResult SendTextNotification()
  {
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (SendTextNotification), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 1146);
      }
      if (MonnitSession.NotificationInProgress.AccountID < 1L)
        return (ActionResult) this.Redirect("/Rule/ChooseType/");
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__23.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__23.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, eNotificationType, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationType", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = RuleController.\u003C\u003Eo__23.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__23.\u003C\u003Ep__0, this.ViewBag, eNotificationType.SMS);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__23.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__23.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = RuleController.\u003C\u003Eo__23.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__23.\u003C\u003Ep__1, this.ViewBag, MonnitSession.NotificationRecipientsInProgress);
      MonnitSession.NotificationInProgress.NotificationType = eNotificationType.SMS;
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.SendTextNotification ");
    }
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  [ValidateInput(false)]
  [HttpPost]
  public ActionResult SaveTextNotificationSettings(FormCollection collection)
  {
    string str1 = "";
    string content;
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(MonnitSession.NotificationInProgress.AccountID))
      content = "misc: Insufficient permissions for account access.";
    else if (!MonnitSession.CustomerCan("Notification_Edit"))
    {
      content = "misc: Insufficient permissions for editing notifications.";
    }
    else
    {
      try
      {
        if (!string.IsNullOrEmpty(collection["SMSText"]) && collection["SMSText"].HasScriptTag())
          str1 = "SMSText: Script tags not permitted.";
        collection["SMSText"] = collection["SMSText"].SanitizeHTMLEvent();
        MonnitSession.NotificationInProgress.SMSText = collection["SMSText"];
        content = "Success";
      }
      catch (Exception ex)
      {
        ex.Log("RuleController.SaveTextNotification ");
        string str2 = "misc:Failed";
        content = string.IsNullOrWhiteSpace(str1) ? str2 : $"{str1}|{str2}";
      }
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public ActionResult SendPushNotification()
  {
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (SendPushNotification), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 1215);
      }
      if (MonnitSession.NotificationInProgress.AccountID < 1L)
        return (ActionResult) this.Redirect("/Rule/ChooseType/");
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__25.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__25.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, eNotificationType, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationType", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = RuleController.\u003C\u003Eo__25.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__25.\u003C\u003Ep__0, this.ViewBag, eNotificationType.Push_Message);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__25.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__25.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = RuleController.\u003C\u003Eo__25.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__25.\u003C\u003Ep__1, this.ViewBag, MonnitSession.NotificationRecipientsInProgress);
      MonnitSession.NotificationInProgress.NotificationType = eNotificationType.Push_Message;
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.SendPushNotification ");
    }
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  [ValidateInput(false)]
  [HttpPost]
  public ActionResult SavePushNotificationSettings(FormCollection collection)
  {
    string str1 = "";
    string content;
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(MonnitSession.NotificationInProgress.AccountID))
      content = "misc: Insufficient permissions for account access.";
    else if (!MonnitSession.CustomerCan("Notification_Edit"))
    {
      content = "misc: Insufficient permissions for editing notifications.";
    }
    else
    {
      try
      {
        if (!string.IsNullOrEmpty(collection["PushText"]) && collection["PushText"].HasScriptTag())
          str1 = "PushText: Script tags not permitted.";
        if (string.IsNullOrEmpty(collection["PushText"]))
        {
          string str2 = "PushText: Message can't be empty.";
          str1 = string.IsNullOrWhiteSpace(str1) ? str2 : $"{str1}|{str2}";
        }
        if (string.IsNullOrWhiteSpace(str1))
        {
          collection["PushText"] = collection["PushText"].SanitizeHTMLEvent();
          MonnitSession.NotificationInProgress.SMSText = collection["PushText"];
        }
        content = "Success";
      }
      catch (Exception ex)
      {
        ex.Log("RuleController.SavePushNotificationSettings ");
        string str3 = "misc:Failed";
        content = string.IsNullOrWhiteSpace(str1) ? str3 : $"{str1}|{str3}";
      }
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public ActionResult SendVoiceNotification()
  {
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (SendVoiceNotification), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 1293);
      }
      if (MonnitSession.NotificationInProgress.AccountID < 1L)
        return (ActionResult) this.Redirect("/Rule/ChooseType/");
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__27.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__27.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, eNotificationType, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationType", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = RuleController.\u003C\u003Eo__27.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__27.\u003C\u003Ep__0, this.ViewBag, eNotificationType.Phone);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__27.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__27.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = RuleController.\u003C\u003Eo__27.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__27.\u003C\u003Ep__1, this.ViewBag, MonnitSession.NotificationRecipientsInProgress);
      MonnitSession.NotificationInProgress.NotificationType = eNotificationType.Phone;
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.SendVoiceNotification ");
    }
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  [ValidateInput(false)]
  [HttpPost]
  public ActionResult SaveVoiceNotificationSettings(FormCollection collection)
  {
    string str1 = "";
    string content;
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(MonnitSession.NotificationInProgress.AccountID))
      content = "misc: Insufficient permissions for account access.";
    else if (!MonnitSession.CustomerCan("Notification_Edit"))
    {
      content = "misc: Insufficient permissions for editing notifications.";
    }
    else
    {
      try
      {
        if (!string.IsNullOrEmpty(collection["VoiceText"]) && collection["VoiceText"].HasScriptTag())
          str1 = "VoiceText: Script tags not permitted.";
        collection["VoiceText"] = collection["VoiceText"].SanitizeHTMLEvent();
        MonnitSession.NotificationInProgress.VoiceText = collection["VoiceText"];
        content = "Success";
      }
      catch (Exception ex)
      {
        ex.Log("RuleController.SaveVoiceNotification ");
        string str2 = "misc:Failed";
        content = string.IsNullOrWhiteSpace(str1) ? str2 : $"{str1}|{str2}";
      }
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public ActionResult SaveRecipients(FormCollection collection)
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
    {
      RuleController.ResetNotificationInProgress();
      return (ActionResult) this.Content("Failed: Unauthorized");
    }
    if (MonnitSession.NotificationInProgress.AccountID < 1L)
      return (ActionResult) this.Content("Failed: No Rule Found");
    MonnitSession.NotificationInProgress.AccountID = MonnitSession.CurrentCustomer.AccountID;
    Notification notificationInProgress = MonnitSession.NotificationInProgress;
    if (string.IsNullOrEmpty(collection["type"]))
      return (ActionResult) this.Content("Failed: No type Found");
    eNotificationType eNotiType = (eNotificationType) collection["type"].ToInt();
    try
    {
      string[] strArray1 = collection["recipient"].Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries);
      string str1 = "";
      if (eNotiType == eNotificationType.Email)
        MonnitSession.NotificationRecipientsInProgress = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType != eNotificationType.Group)).ToList<NotificationRecipient>();
      MonnitSession.NotificationRecipientsInProgress = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType != eNotiType)).ToList<NotificationRecipient>();
      foreach (string str2 in strArray1)
      {
        string[] strArray2 = str2.Split(new char[1]{ '|' }, StringSplitOptions.RemoveEmptyEntries);
        if (strArray2.Length != 0)
        {
          long customerID = strArray2[0].ToLong();
          string str3 = strArray2[1];
          int num = strArray2[2].ToInt();
          long customerGroupID = strArray2.Length > 3 ? strArray2[3].ToLong() : long.MinValue;
          if (customerGroupID > 0L)
          {
            if (CustomerGroup.Load(customerGroupID) == null)
            {
              string str4 = $"misc:Failed to assign [{str3}] to rule.";
              str1 = string.IsNullOrWhiteSpace(str1) ? str4 : $"{str1}|{str4}";
            }
          }
          else
          {
            Customer customer = Customer.Load(customerID);
            if (notificationInProgress.AccountID != customer.AccountID && CustomerAccountLink.Load(customer.CustomerID, notificationInProgress.AccountID) == null)
            {
              bool flag = false;
              foreach (Account ancestor in Account.Ancestors(MonnitSession.CurrentCustomer.CustomerID, notificationInProgress.AccountID))
              {
                if (ancestor.AccountID == customer.AccountID)
                {
                  flag = true;
                  break;
                }
              }
              if (!flag)
              {
                string str5 = $"misc:Failed to assign [{customer.FullName}] to rule.";
                str1 = string.IsNullOrWhiteSpace(str1) ? str5 : $"{str1}|{str5}";
              }
            }
          }
          if (string.IsNullOrWhiteSpace(str1))
          {
            try
            {
              NotificationRecipient notificationRecipient;
              if (customerGroupID > 0L)
              {
                notificationRecipient = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.CustomerGroupID == customerGroupID && m.NotificationType == eNotificationType.Group)).FirstOrDefault<NotificationRecipient>();
                if (notificationRecipient == null)
                {
                  notificationRecipient = new NotificationRecipient()
                  {
                    CustomerGroupID = customerGroupID,
                    NotificationType = eNotificationType.Group
                  };
                  MonnitSession.NotificationRecipientsInProgress.Add(notificationRecipient);
                }
              }
              else
              {
                notificationRecipient = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.CustomerToNotifyID == customerID && m.NotificationType == eNotiType)).FirstOrDefault<NotificationRecipient>();
                if (notificationRecipient == null)
                {
                  notificationRecipient = new NotificationRecipient()
                  {
                    CustomerToNotifyID = customerID,
                    NotificationType = eNotiType
                  };
                  MonnitSession.NotificationRecipientsInProgress.Add(notificationRecipient);
                }
              }
              notificationRecipient.DelayMinutes = num;
            }
            catch (Exception ex)
            {
              ex.Log($"RuleController.SendNotification[Post][Add Customer][CustomerID: {customerID.ToString()}] ");
              string str6 = $"misc:Failed to assign [{str3}] to rule.";
              str1 = string.IsNullOrWhiteSpace(str1) ? str6 : $"{str1}|{str6}";
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.SaveRecipients ");
    }
    List<NotificationRecipient> list = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotiType)).ToList<NotificationRecipient>();
    if (eNotiType == eNotificationType.Email)
      list.AddRange((IEnumerable<NotificationRecipient>) MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.Group)).ToList<NotificationRecipient>());
    return (ActionResult) this.PartialView("_SendToNamesList", (object) list);
  }

  [AuthorizeDefault]
  public ActionResult SaveAllRecipients(FormCollection collection)
  {
    Notification notificationInProgress = MonnitSession.NotificationInProgress;
    foreach (NotificationRecipient notificationRecipient in MonnitSession.NotificationRecipientsInProgress)
    {
      if (notificationRecipient.NotificationType == eNotificationType.Email || notificationRecipient.NotificationType == eNotificationType.SMS || notificationRecipient.NotificationType == eNotificationType.Both || notificationRecipient.NotificationType == eNotificationType.Phone || notificationRecipient.NotificationType == eNotificationType.Group)
        MonnitSession.NotificationRecipientsInProgress.Remove(notificationRecipient);
    }
    var data1 = new
    {
      Type = collection[0],
      Recipient = collection[1]
    };
    var data2 = new
    {
      Type = collection[2],
      Recipient = collection[3]
    };
    var data3 = new
    {
      Type = collection[4],
      Recipient = collection[5]
    };
    var data4 = new
    {
      Type = collection[6],
      Recipient = collection[7]
    };
    var data5 = new
    {
      Type = collection[8],
      Recipient = collection[9]
    };
    List<\u003C\u003Ef__AnonymousType7<string, string>> list1 = new \u003C\u003Ef__AnonymousType7<string, string>[4]
    {
      data1,
      data2,
      data3,
      data5
    }.ToList();
    if (!MonnitSession.CustomerCan("Notification_Edit"))
    {
      RuleController.ResetNotificationInProgress();
      return (ActionResult) this.Content("Failed: Unauthorized");
    }
    if (MonnitSession.NotificationInProgress.AccountID < 1L)
      return (ActionResult) this.Content("Failed: No Rule Found");
    MonnitSession.NotificationInProgress.AccountID = MonnitSession.CurrentCustomer.AccountID;
    string recipient = data4.Recipient;
    char[] separator = new char[1]{ ',' };
    foreach (string str in recipient.Split(separator, StringSplitOptions.RemoveEmptyEntries))
    {
      string ID = str;
      if (MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.CustomerGroupID == ID.ToLong() && m.NotificationType == eNotificationType.Group)).FirstOrDefault<NotificationRecipient>() == null)
        MonnitSession.NotificationRecipientsInProgress.Add(new NotificationRecipient()
        {
          CustomerGroupID = ID.ToLong(),
          NotificationType = eNotificationType.Group
        });
    }
    foreach (var data6 in list1)
    {
      if (string.IsNullOrEmpty(data6.Type))
        return (ActionResult) this.Content("Failed: No type Found");
      eNotificationType eNotiType = (eNotificationType) data6.Type.ToInt();
      try
      {
        string[] strArray1 = data6.Recipient.Split(new char[1]
        {
          ','
        }, StringSplitOptions.RemoveEmptyEntries);
        string str1 = "";
        MonnitSession.NotificationRecipientsInProgress = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType != eNotiType)).ToList<NotificationRecipient>();
        foreach (string str2 in strArray1)
        {
          string[] strArray2 = str2.Split(new char[1]{ '|' }, StringSplitOptions.RemoveEmptyEntries);
          if (strArray2.Length != 0)
          {
            long customerID = strArray2[0].ToLong();
            string str3 = strArray2[1];
            int num = strArray2[2].ToInt();
            Customer customer = Customer.Load(customerID);
            if (notificationInProgress.AccountID != customer.AccountID && CustomerAccountLink.Load(customer.CustomerID, notificationInProgress.AccountID) == null)
            {
              bool flag = false;
              foreach (Account ancestor in Account.Ancestors(MonnitSession.CurrentCustomer.CustomerID, notificationInProgress.AccountID))
              {
                if (ancestor.AccountID == customer.AccountID)
                {
                  flag = true;
                  break;
                }
              }
              if (!flag)
              {
                string str4 = $"misc:Failed to assign [{customer.FullName}] to rule.";
                str1 = string.IsNullOrWhiteSpace(str1) ? str4 : $"{str1}|{str4}";
              }
            }
            if (string.IsNullOrWhiteSpace(str1))
            {
              try
              {
                NotificationRecipient notificationRecipient = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.CustomerToNotifyID == customerID && m.NotificationType == eNotiType)).FirstOrDefault<NotificationRecipient>();
                if (notificationRecipient == null)
                {
                  notificationRecipient = new NotificationRecipient()
                  {
                    CustomerToNotifyID = customerID,
                    NotificationType = eNotiType
                  };
                  MonnitSession.NotificationRecipientsInProgress.Add(notificationRecipient);
                }
                notificationRecipient.DelayMinutes = num;
              }
              catch (Exception ex)
              {
                ex.Log($"RuleController.SendNotification[Post][Add Customer][CustomerID: {customerID.ToString()}] ");
                string str5 = $"misc:Failed to assign [{str3}] to rule.";
                str1 = string.IsNullOrWhiteSpace(str1) ? str5 : $"{str1}|{str5}";
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        ex.Log("RuleController.SaveRecipients ");
      }
      List<NotificationRecipient> list2 = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotiType)).ToList<NotificationRecipient>();
      if (eNotiType == eNotificationType.Email)
        list2.AddRange((IEnumerable<NotificationRecipient>) MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.Group)).ToList<NotificationRecipient>());
    }
    return (ActionResult) this.Content("Success, saved recipients");
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult RuleRecipientList(string q)
  {
    List<NotificationRecipientData> model = NotificationRecipientData.SearchPotentialRecipient(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.NotificationInProgress.AccountID, q);
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__31.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__31.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, eNotificationType, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationType", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = RuleController.\u003C\u003Eo__31.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__31.\u003C\u003Ep__0, this.ViewBag, MonnitSession.NotificationInProgress.NotificationType);
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__31.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__31.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = RuleController.\u003C\u003Eo__31.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__31.\u003C\u003Ep__1, this.ViewBag, MonnitSession.NotificationRecipientsInProgress);
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__31.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__31.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, eNotificationType, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationType", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = RuleController.\u003C\u003Eo__31.\u003C\u003Ep__2.Target((CallSite) RuleController.\u003C\u003Eo__31.\u003C\u003Ep__2, this.ViewBag, eNotificationType.Email);
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__31.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__31.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj4 = RuleController.\u003C\u003Eo__31.\u003C\u003Ep__3.Target((CallSite) RuleController.\u003C\u003Eo__31.\u003C\u003Ep__3, this.ViewBag, MonnitSession.NotificationRecipientsInProgress);
    MonnitSession.NotificationInProgress.NotificationType = eNotificationType.Email;
    return (ActionResult) this.PartialView("RecipientList", (object) model);
  }

  [AuthorizeDefault]
  [ValidateInput(false)]
  [HttpPost]
  public ActionResult NotifyBodyMsgPreview(string msg)
  {
    string content = "N/A";
    try
    {
      Notification notificationInProgress = MonnitSession.NotificationInProgress;
      if (notificationInProgress != null)
      {
        Sensor sensor = Sensor.Load(notificationInProgress.SensorID);
        Gateway gateway = Gateway.Load(notificationInProgress.GatewayID);
        CSNet network = sensor != null ? CSNet.Load(sensor.CSNetID) : (CSNet) null;
        Account account = Account.Load(notificationInProgress.AccountID);
        content = notificationInProgress.ReplaceVariablesPreview(msg, sensor, gateway, network, account);
      }
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.NotifyBodyMsgPreview ");
      content = "Failed to generate preview";
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public ActionResult RemoveNotifyTask(string NotiType)
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
    {
      RuleController.ResetNotificationInProgress();
      return (ActionResult) this.Content("Failed: Unauthorized");
    }
    if (MonnitSession.NotificationInProgress.AccountID < 1L)
      return (ActionResult) this.Content("Failed: No Rule Found");
    MonnitSession.NotificationInProgress.AccountID = MonnitSession.CurrentCustomer.AccountID;
    eNotificationType notificationType = (eNotificationType) NotiType.ToInt();
    try
    {
      List<NotificationRecipient> notificationRecipientList = new List<NotificationRecipient>();
      switch (notificationType)
      {
        case eNotificationType.SMS:
          MonnitSession.NotificationRecipientsInProgress = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType != eNotificationType.SMS)).ToList<NotificationRecipient>();
          MonnitSession.NotificationInProgress.SMSText = "";
          break;
        case eNotificationType.Phone:
          MonnitSession.NotificationRecipientsInProgress = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType != eNotificationType.Phone)).ToList<NotificationRecipient>();
          MonnitSession.NotificationInProgress.VoiceText = "";
          break;
        case eNotificationType.Push_Message:
          MonnitSession.NotificationRecipientsInProgress = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType != eNotificationType.Push_Message)).ToList<NotificationRecipient>();
          MonnitSession.NotificationInProgress.PushMsgText = "";
          break;
        default:
          MonnitSession.NotificationRecipientsInProgress = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType != eNotificationType.Group && m.NotificationType != eNotificationType.Email)).ToList<NotificationRecipient>();
          MonnitSession.NotificationInProgress.NotificationText = "";
          MonnitSession.NotificationInProgress.Subject = "";
          break;
      }
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.RemoveNotifyTask | NotiType = " + NotiType);
      return (ActionResult) this.Content("Failed: Could not remove Task");
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult RemoveNotifyTask()
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
    {
      RuleController.ResetNotificationInProgress();
      return (ActionResult) this.Content("Failed: Unauthorized");
    }
    if (MonnitSession.NotificationInProgress.AccountID < 1L)
      return (ActionResult) this.Content("Failed: No Rule Found");
    MonnitSession.NotificationInProgress.AccountID = MonnitSession.CurrentCustomer.AccountID;
    try
    {
      List<NotificationRecipient> notificationRecipientList = new List<NotificationRecipient>();
      MonnitSession.NotificationRecipientsInProgress = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType != eNotificationType.SMS)).ToList<NotificationRecipient>();
      MonnitSession.NotificationInProgress.SMSText = "";
      MonnitSession.NotificationRecipientsInProgress = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType != eNotificationType.Phone)).ToList<NotificationRecipient>();
      MonnitSession.NotificationInProgress.VoiceText = "";
      MonnitSession.NotificationRecipientsInProgress = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType != eNotificationType.Group && m.NotificationType != eNotificationType.Email)).ToList<NotificationRecipient>();
      MonnitSession.NotificationInProgress.NotificationText = "";
      MonnitSession.NotificationInProgress.Subject = "";
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.RemoveNotifyTask | NotiType = new alert clears all delivery methods.");
      return (ActionResult) this.Content("Failed: Could not remove Task");
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult RemoveNotifyTaskFromEdit(long id)
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
    {
      RuleController.ResetNotificationInProgress();
      return (ActionResult) this.Content("Failed: Unauthorized");
    }
    Notification notification = Notification.Load(id);
    List<NotificationRecipient> notificationRecipients = notification.NotificationRecipients;
    if (notification == null)
      return (ActionResult) this.Content("Failed: No Rule Found");
    try
    {
      notification.SMSText = "";
      notification.VoiceText = "";
      notification.Subject = "";
      notification.NotificationText = "";
      notification.Save();
      for (int index = notificationRecipients.Count - 1; index >= 0; --index)
      {
        NotificationRecipient recipient = notificationRecipients[index];
        if (recipient.NotificationType == eNotificationType.Email || recipient.NotificationType == eNotificationType.SMS || recipient.NotificationType == eNotificationType.Phone || recipient.NotificationType == eNotificationType.Group || recipient.NotificationType == eNotificationType.Push_Message)
          notification.RemoveRecipient(recipient);
      }
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.RemoveNotifyTask | NotiType = new alert clears all delivery methods.");
      return (ActionResult) this.Content("Failed: Could not remove Task");
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult ChooseTask()
  {
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (ChooseTask), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 1835);
      }
      if (MonnitSession.NotificationInProgress.AccountID < 1L)
        return (ActionResult) this.Redirect("/Rule/ChooseType/");
      NotificationRecipientDevice notificationRecipientDevice = NotificationRecipientDevice.LoadByAccountID(MonnitSession.NotificationInProgress.AccountID);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__36.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__36.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, NotificationRecipientDevice, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "RecipientDevices", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RuleController.\u003C\u003Eo__36.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__36.\u003C\u003Ep__0, this.ViewBag, notificationRecipientDevice);
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.ChooseTask ");
    }
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public ActionResult CommandControlUnit()
  {
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (CommandControlUnit), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 1869);
      }
      if (MonnitSession.NotificationInProgress.AccountID < 1L)
        return (ActionResult) this.Redirect("/Rule/ChooseType/");
      MonnitSession.NotificationInProgress.NotificationType = eNotificationType.Control;
      List<Sensor> controlUnitList = NotificationRecipientDevice.LoadByAccountID(MonnitSession.NotificationInProgress.AccountID).ControlUnitList;
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__37.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__37.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RuleController.\u003C\u003Eo__37.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__37.\u003C\u003Ep__0, this.ViewBag, MonnitSession.NotificationRecipientsInProgress);
      return (ActionResult) this.View(nameof (CommandControlUnit), (object) controlUnitList);
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.CommandControlUnit ");
    }
    return (ActionResult) this.Redirect("/Rule/ChooseType/");
  }

  [AuthorizeDefault]
  public ActionResult SaveControlUnitSettings(FormCollection collection)
  {
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
        return (ActionResult) this.Content("Unauthorized");
      if (MonnitSession.NotificationInProgress.AccountID < 1L)
        return (ActionResult) this.Content("No Rule Found");
      MonnitSession.NotificationInProgress.NotificationType = eNotificationType.Control;
      List<NotificationRecipient> list = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.Control)).ToList<NotificationRecipient>();
      MonnitSession.NotificationRecipientsInProgress = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType != eNotificationType.Control)).ToList<NotificationRecipient>();
      for (int index = 0; index < list.Count; ++index)
      {
        NotificationRecipient notificationRecipient1 = list[index];
        int num1 = 0;
        int num2 = 0;
        int state1_1 = 0;
        int num3 = 0;
        int num4 = 0;
        int state2_1 = 0;
        int state1_2;
        int state2_2;
        Control_1.ParseSerializedRecipientProperties(notificationRecipient1.SerializedRecipientProperties, out state1_2, out state2_2, out ushort _, out ushort _);
        long deviceToNotifyId;
        if (state1_2 > 0)
        {
          FormCollection formCollection1 = collection;
          deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
          string name1 = "relay1Toggle_" + deviceToNotifyId.ToString();
          state1_1 = formCollection1[name1].ToBool() ? 2 : 1;
          FormCollection formCollection2 = collection;
          deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
          string name2 = "relay1Minute_" + deviceToNotifyId.ToString();
          num1 = formCollection2[name2].ToInt();
          FormCollection formCollection3 = collection;
          deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
          string name3 = "relay1Second_" + deviceToNotifyId.ToString();
          num2 = formCollection3[name3].ToInt();
          NotificationRecipient notificationRecipient2 = notificationRecipient1;
          FormCollection formCollection4 = collection;
          deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
          string name4 = "relay1delayMinutes_" + deviceToNotifyId.ToString();
          int num5 = formCollection4[name4].ToInt();
          notificationRecipient2.DelayMinutes = num5;
        }
        else if (state2_2 > 0)
        {
          FormCollection formCollection5 = collection;
          deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
          string name5 = "relay2Toggle_" + deviceToNotifyId.ToString();
          state2_1 = formCollection5[name5].ToBool() ? 2 : 1;
          FormCollection formCollection6 = collection;
          deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
          string name6 = "relay2Minute_" + deviceToNotifyId.ToString();
          num3 = formCollection6[name6].ToInt();
          FormCollection formCollection7 = collection;
          deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
          string name7 = "relay2Second_" + deviceToNotifyId.ToString();
          num4 = formCollection7[name7].ToInt();
          NotificationRecipient notificationRecipient3 = notificationRecipient1;
          FormCollection formCollection8 = collection;
          deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
          string name8 = "relay2delayMinutes_" + deviceToNotifyId.ToString();
          int num6 = formCollection8[name8].ToInt();
          notificationRecipient3.DelayMinutes = num6;
        }
        notificationRecipient1.SerializedRecipientProperties = Control_1.CreateSerializedRecipientProperties(state1_1, state2_1, num1 * 60 + num2, num3 * 60 + num4);
        MonnitSession.NotificationRecipientsInProgress.Add(notificationRecipient1);
      }
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.SaveControlUnitSettings ");
      return (ActionResult) this.Content(ex.Message);
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ToggleControlUnitDevice(long deviceID, string deviceType, bool add)
  {
    try
    {
      Notification notificationInProgress = MonnitSession.NotificationInProgress;
      if (notificationInProgress == null)
        return (ActionResult) this.Content("Rule not found");
      Sensor sensor = Sensor.Load(deviceID);
      if (sensor == null)
        return (ActionResult) this.Content("Device not found");
      if (add)
      {
        if (notificationInProgress.AccountID != sensor.AccountID)
          return (ActionResult) this.Content("Device not found");
        switch (deviceType)
        {
          case "Control1":
            this.AddSensorRecipient(sensor, Control_1.CreateSerializedRecipientProperties(1, 0, 0, 0));
            notificationInProgress.HasControlAction = true;
            break;
          case "Control2":
            this.AddSensorRecipient(sensor, Control_1.CreateSerializedRecipientProperties(0, 1, 0, 0));
            notificationInProgress.HasControlAction = true;
            break;
        }
      }
      else
      {
        int foundIndex;
        if (this.FindNotificationRecipient(deviceID, deviceType, out foundIndex) == null)
          return (ActionResult) this.Content("Not found");
        if (!this.RemoveRecipient(foundIndex))
        {
          if (MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.Control)).Count<NotificationRecipient>() == 0)
            MonnitSession.NotificationInProgress.HasControlAction = false;
          return (ActionResult) this.Content("Failed to remove recipient");
        }
      }
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__39.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__39.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RuleController.\u003C\u003Eo__39.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__39.\u003C\u003Ep__0, this.ViewBag, MonnitSession.NotificationRecipientsInProgress);
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Unable to add device");
    }
  }

  private NotificationRecipient AddSensorRecipient(
    Sensor sensor,
    string defaultSerializedRecipientProperties)
  {
    eNotificationType notificationType = sensor.ApplicationID != 13L ? (sensor.ApplicationID != 97L && sensor.ApplicationID != 125L ? (sensor.ApplicationID != 73L && sensor.ApplicationID != 90L && sensor.ApplicationID != 93L && sensor.ApplicationID != 94L && sensor.ApplicationID != 120L && sensor.ApplicationID != 153L ? eNotificationType.Control : eNotificationType.ResetAccumulator) : eNotificationType.Thermostat) : eNotificationType.Local_Notifier;
    foreach (NotificationRecipient notificationRecipient in MonnitSession.NotificationRecipientsInProgress)
    {
      if (notificationRecipient.DeviceToNotifyID == sensor.SensorID && notificationRecipient.NotificationType == notificationType)
      {
        if (notificationRecipient.NotificationType == eNotificationType.Local_Notifier || notificationRecipient.NotificationType == eNotificationType.Thermostat)
          return notificationRecipient;
        if (notificationRecipient.NotificationType == eNotificationType.Control)
        {
          if (notificationRecipient.SerializedRecipientProperties.StartsWith(defaultSerializedRecipientProperties.Substring(0, 3)))
            return notificationRecipient;
        }
        else if (notificationRecipient.NotificationType == eNotificationType.ResetAccumulator)
          return notificationRecipient;
      }
    }
    NotificationRecipient notificationRecipient1 = new NotificationRecipient()
    {
      DeviceToNotify = sensor,
      DeviceToNotifyID = sensor.SensorID,
      NotificationType = notificationType,
      SerializedRecipientProperties = defaultSerializedRecipientProperties
    };
    MonnitSession.NotificationRecipientsInProgress.Add(notificationRecipient1);
    return notificationRecipient1;
  }

  private bool RemoveRecipient(int removeIndex)
  {
    int count = MonnitSession.NotificationRecipientsInProgress.Count;
    MonnitSession.NotificationRecipientsInProgress.RemoveAt(removeIndex);
    return count > MonnitSession.NotificationRecipientsInProgress.Count;
  }

  private bool RemoveRecipient(NotificationRecipient recipient)
  {
    int count = MonnitSession.NotificationRecipientsInProgress.Count;
    MonnitSession.NotificationRecipientsInProgress.Remove(recipient);
    return count > MonnitSession.NotificationRecipientsInProgress.Count;
  }

  private NotificationRecipient FindNotificationRecipient(
    long deviceID,
    string deviceType,
    out int foundIndex)
  {
    NotificationRecipient notificationRecipient1 = (NotificationRecipient) null;
    foundIndex = -1;
    for (int index = 0; index < MonnitSession.NotificationRecipientsInProgress.Count; ++index)
    {
      NotificationRecipient notificationRecipient2 = MonnitSession.NotificationRecipientsInProgress[index];
      if (notificationRecipient2.DeviceToNotifyID == deviceID)
      {
        if (notificationRecipient2.NotificationType == eNotificationType.Local_Notifier || notificationRecipient2.NotificationType == eNotificationType.Thermostat)
        {
          notificationRecipient1 = notificationRecipient2;
          foundIndex = index;
          break;
        }
        if (notificationRecipient2.NotificationType == eNotificationType.ResetAccumulator)
        {
          notificationRecipient1 = notificationRecipient2;
          foundIndex = index;
          break;
        }
        if (notificationRecipient2.NotificationType == eNotificationType.Control)
        {
          int state1 = 0;
          int state2 = 0;
          ushort time1 = 0;
          ushort time2 = 0;
          if (deviceType == "BasicControl")
          {
            BasicControl.ParseSerializedRecipientProperties(notificationRecipient2.SerializedRecipientProperties, out state1, out time1);
            notificationRecipient1 = notificationRecipient2;
            foundIndex = index;
            break;
          }
          Control_1.ParseSerializedRecipientProperties(notificationRecipient2.SerializedRecipientProperties, out state1, out state2, out time1, out time2);
          if (deviceType == "Control1" && state1 > 0)
          {
            notificationRecipient1 = notificationRecipient2;
            foundIndex = index;
            break;
          }
          if (deviceType == "Control2" && state2 > 0)
          {
            notificationRecipient1 = notificationRecipient2;
            foundIndex = index;
            break;
          }
        }
      }
    }
    return notificationRecipient1;
  }

  [AuthorizeDefault]
  public ActionResult CommandThermostat()
  {
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (CommandThermostat), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 2145);
      }
      if (MonnitSession.NotificationInProgress.AccountID < 1L)
        return (ActionResult) this.Redirect("/Rule/ChooseType/");
      MonnitSession.NotificationInProgress.NotificationType = eNotificationType.Thermostat;
      List<Sensor> thermostatList = NotificationRecipientDevice.LoadByAccountID(MonnitSession.NotificationInProgress.AccountID).ThermostatList;
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__44.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__44.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RuleController.\u003C\u003Eo__44.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__44.\u003C\u003Ep__0, this.ViewBag, MonnitSession.NotificationRecipientsInProgress);
      return (ActionResult) this.View(nameof (CommandThermostat), (object) thermostatList);
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.CommandThermostat ");
    }
    return (ActionResult) this.Redirect("/Rule/ChooseType/");
  }

  [AuthorizeDefault]
  public ActionResult SaveThermostatSettings(FormCollection collection)
  {
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
        return (ActionResult) this.Content("Unauthorized");
      if (MonnitSession.NotificationInProgress.AccountID < 1L)
        return (ActionResult) this.Content("No Rule Found");
      List<NotificationRecipient> list = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.Thermostat)).ToList<NotificationRecipient>();
      MonnitSession.NotificationRecipientsInProgress = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType != eNotificationType.Thermostat)).ToList<NotificationRecipient>();
      foreach (NotificationRecipient notificationRecipient in list)
      {
        Sensor sensor = Sensor.Load(notificationRecipient.DeviceToNotifyID);
        long deviceToNotifyId;
        if (sensor.ApplicationID == 97L)
        {
          FormCollection formCollection1 = collection;
          deviceToNotifyId = notificationRecipient.DeviceToNotifyID;
          string name1 = "occupancy_" + deviceToNotifyId.ToString();
          bool o = !string.IsNullOrEmpty(formCollection1[name1]);
          FormCollection formCollection2 = collection;
          deviceToNotifyId = notificationRecipient.DeviceToNotifyID;
          string name2 = "occupancyDuration_" + deviceToNotifyId.ToString();
          ushort uint16 = Convert.ToUInt16(formCollection2[name2]);
          notificationRecipient.SerializedRecipientProperties = Thermostat.CreateSerializedRecipientProperties(o.ToInt(), uint16);
        }
        if (sensor.ApplicationID == 125L)
        {
          FormCollection formCollection3 = collection;
          deviceToNotifyId = notificationRecipient.DeviceToNotifyID;
          string name3 = "occupancy_" + deviceToNotifyId.ToString();
          bool o = !string.IsNullOrEmpty(formCollection3[name3]);
          FormCollection formCollection4 = collection;
          deviceToNotifyId = notificationRecipient.DeviceToNotifyID;
          string name4 = "occupancyDuration_" + deviceToNotifyId.ToString();
          ushort uint16 = Convert.ToUInt16(formCollection4[name4]);
          notificationRecipient.SerializedRecipientProperties = MultiStageThermostat.CreateSerializedRecipientProperties(o.ToInt(), uint16);
        }
        MonnitSession.NotificationRecipientsInProgress.Add(notificationRecipient);
      }
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.SaveThermostatSettings ");
      return (ActionResult) this.Content(ex.Message);
    }
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  [HttpPost]
  public ActionResult ToggleThermostatDevice(long deviceID, bool add)
  {
    try
    {
      Notification notificationInProgress = MonnitSession.NotificationInProgress;
      if (notificationInProgress == null)
        return (ActionResult) this.Content("Notification not found");
      Sensor sensor = Sensor.Load(deviceID);
      if (sensor == null)
        return (ActionResult) this.Content("Device not found");
      if (add)
      {
        if (notificationInProgress.AccountID != sensor.AccountID)
          return (ActionResult) this.Content("Device not found");
        this.AddSensorRecipient(sensor, Thermostat.CreateSerializedRecipientProperties(1, (ushort) 30));
        notificationInProgress.HasThermostatAction = true;
      }
      else
      {
        int foundIndex;
        if (this.FindNotificationRecipient(deviceID, "Thermostat", out foundIndex) == null)
          return (ActionResult) this.Content("Not found");
        if (!this.RemoveRecipient(foundIndex))
        {
          if (MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.Thermostat)).Count<NotificationRecipient>() == 0)
            MonnitSession.NotificationInProgress.HasThermostatAction = false;
          return (ActionResult) this.Content("Failed to remove recipient");
        }
      }
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__46.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__46.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RuleController.\u003C\u003Eo__46.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__46.\u003C\u003Ep__0, this.ViewBag, MonnitSession.NotificationRecipientsInProgress);
      return (ActionResult) this.PartialView("CommandThermostatDetails", (object) sensor);
    }
    catch
    {
      return (ActionResult) this.Content("Unable to add device");
    }
  }

  [AuthorizeDefault]
  public ActionResult CommandLocalAlert()
  {
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (CommandLocalAlert), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 2285);
      }
      if (MonnitSession.NotificationInProgress.AccountID < 1L)
        return (ActionResult) this.Redirect("/Rule/ChooseType/");
      MonnitSession.NotificationInProgress.NotificationType = eNotificationType.Local_Notifier;
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__47.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__47.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "LocalAlertText", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = RuleController.\u003C\u003Eo__47.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__47.\u003C\u003Ep__0, this.ViewBag, string.IsNullOrEmpty(MonnitSession.NotificationInProgress.LocalAlertText) ? string.Format("Local alert Message") : MonnitSession.NotificationInProgress.LocalAlertText);
      List<Sensor> localAlertList = NotificationRecipientDevice.LoadByAccountID(MonnitSession.NotificationInProgress.AccountID).LocalAlertList;
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__47.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__47.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = RuleController.\u003C\u003Eo__47.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__47.\u003C\u003Ep__1, this.ViewBag, MonnitSession.NotificationRecipientsInProgress);
      return (ActionResult) this.View(nameof (CommandLocalAlert), (object) localAlertList);
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.CommandLocalAlert ");
    }
    return (ActionResult) this.Redirect("/Rule/ChooseType/");
  }

  [AuthorizeDefault]
  public ActionResult SaveLocalAlertSettings(FormCollection collection)
  {
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
        return (ActionResult) this.Content("Unauthorized");
      if (MonnitSession.NotificationInProgress.AccountID < 1L)
        return (ActionResult) this.Content("No Rule Found");
      MonnitSession.NotificationInProgress.NotificationType = eNotificationType.Local_Notifier;
      if (string.IsNullOrEmpty(collection["LocalAlertText"]))
        return (ActionResult) this.Content("Local Alert Text Required");
      if (!string.IsNullOrEmpty(collection["LocalAlertText"]) && collection["LocalAlertText"].HasScriptTag())
        return (ActionResult) this.Content("Script tags not permitted.");
      collection["LocalAlertText"] = collection["LocalAlertText"].SanitizeHTMLEvent();
      MonnitSession.NotificationInProgress.LocalAlertText = collection["LocalAlertText"];
      List<NotificationRecipient> list = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.Local_Notifier)).ToList<NotificationRecipient>();
      MonnitSession.NotificationRecipientsInProgress = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType != eNotificationType.Local_Notifier)).ToList<NotificationRecipient>();
      foreach (NotificationRecipient notificationRecipient1 in list)
      {
        FormCollection formCollection1 = collection;
        long deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
        string name1 = "led_" + deviceToNotifyId.ToString();
        bool led = !string.IsNullOrEmpty(formCollection1[name1]);
        FormCollection formCollection2 = collection;
        deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
        string name2 = "buzzer_" + deviceToNotifyId.ToString();
        bool buzzer = !string.IsNullOrEmpty(formCollection2[name2]);
        FormCollection formCollection3 = collection;
        deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
        string name3 = "backLight_" + deviceToNotifyId.ToString();
        bool flag = !string.IsNullOrEmpty(formCollection3[name3]);
        notificationRecipient1.SerializedRecipientProperties = Attention.CreateSerializedRecipientProperties(led, buzzer, flag, flag, notificationRecipient1.DeviceToNotify.SensorName);
        NotificationRecipient notificationRecipient2 = notificationRecipient1;
        FormCollection formCollection4 = collection;
        deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
        string name4 = "delayMinutes_" + deviceToNotifyId.ToString();
        int num = formCollection4[name4].ToInt();
        notificationRecipient2.DelayMinutes = num;
        MonnitSession.NotificationRecipientsInProgress.Add(notificationRecipient1);
      }
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.AssignLocalAlertText ");
      return (ActionResult) this.Content(ex.Message);
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ToggleLocalAlertDevice(long deviceID, bool add)
  {
    try
    {
      Notification notificationInProgress = MonnitSession.NotificationInProgress;
      if (notificationInProgress == null)
        return (ActionResult) this.Content("Notification not found");
      Sensor sensor = Sensor.Load(deviceID);
      if (sensor == null)
        return (ActionResult) this.Content("Device not found");
      if (add)
      {
        if (notificationInProgress.AccountID != sensor.AccountID)
          return (ActionResult) this.Content("Device not found");
        this.AddSensorRecipient(sensor, Attention.CreateSerializedRecipientProperties(true, true, true, true, sensor.SensorName));
        MonnitSession.NotificationInProgress.HasLocalAlertAction = true;
      }
      else
      {
        int foundIndex;
        if (this.FindNotificationRecipient(deviceID, "Notifier", out foundIndex) == null)
          return (ActionResult) this.Content("Not found");
        if (!this.RemoveRecipient(foundIndex))
          return (ActionResult) this.Content("Failed to remove recipient");
      }
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__49.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__49.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RuleController.\u003C\u003Eo__49.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__49.\u003C\u003Ep__0, this.ViewBag, MonnitSession.NotificationRecipientsInProgress);
      return (ActionResult) this.PartialView("CommandLocalAlertDetails", (object) sensor);
    }
    catch
    {
      return (ActionResult) this.Content("Unable to add device");
    }
  }

  [AuthorizeDefault]
  public ActionResult CreateSystemAction()
  {
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (CreateSystemAction), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 2421);
      }
      if (MonnitSession.NotificationInProgress.AccountID < 1L)
        return (ActionResult) this.Redirect("/Rule/ChooseType/");
      MonnitSession.NotificationInProgress.NotificationType = eNotificationType.SystemAction;
      List<Notification> list = Notification.LoadByAccountID(MonnitSession.NotificationInProgress.AccountID).OrderBy<Notification, string>((Func<Notification, string>) (c => c.Name)).ToList<Notification>();
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__50.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__50.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationList", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RuleController.\u003C\u003Eo__50.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__50.\u003C\u003Ep__0, this.ViewBag, list);
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.CreateSystemAction ");
    }
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public ActionResult SystemActionList()
  {
    return MonnitSession.NotificationInProgress == null ? (ActionResult) this.Content("Rule not found") : (ActionResult) this.PartialView("CreateSystemActionList", (object) MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (nr => nr.NotificationType == eNotificationType.SystemAction || nr.NotificationType == eNotificationType.HTTP || nr.NotificationType == eNotificationType.ResetAccumulator)).ToList<NotificationRecipient>());
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult SetSystemAction(FormCollection collection)
  {
    try
    {
      Notification notificationInProgress = MonnitSession.NotificationInProgress;
      if (!MonnitSession.IsAuthorizedForAccount(notificationInProgress.AccountID))
        return (ActionResult) this.Content("Unauthorized");
      NotificationRecipient notificationRecipient = new NotificationRecipient();
      if (collection["action"].ToLong() == -1L)
      {
        notificationRecipient.NotificationType = eNotificationType.HTTP;
        notificationRecipient.SerializedRecipientProperties = collection["NotificationWebhookID"];
        notificationRecipient.DelayMinutes = collection["DelayMinutes"].ToInt();
      }
      else if (collection["action"].ToLong() == -2L)
      {
        notificationRecipient = notificationInProgress.AddSensorRecipient(Sensor.Load(collection["TargetAccumulatorID"].ToLong()), "");
        notificationRecipient.NotificationType = eNotificationType.ResetAccumulator;
        notificationRecipient.DelayMinutes = collection["DelayMinutes"].ToInt();
      }
      else
      {
        notificationRecipient.NotificationType = eNotificationType.SystemAction;
        notificationRecipient.SerializedRecipientProperties = collection["TargetRule"];
        notificationRecipient.DelayMinutes = collection["DelayMinutes"].ToInt();
        notificationRecipient.ActionToExecuteID = collection["action"].ToLong();
      }
      MonnitSession.NotificationRecipientsInProgress.Add(notificationRecipient);
      return (ActionResult) this.Content("Success|" + (MonnitSession.NotificationRecipientsInProgress.Count - 1).ToString());
    }
    catch (Exception ex)
    {
      ex.Log("Failed to create system action");
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult SystemActionDelete(
    eNotificationType notificationType,
    string properties,
    int delay,
    long executeID)
  {
    if (!MonnitSession.IsAuthorizedForAccount(MonnitSession.NotificationInProgress.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.Content("Unauthorized");
    try
    {
      int removeIndex = -1;
      for (int index = 0; index < MonnitSession.NotificationRecipientsInProgress.Count; ++index)
      {
        NotificationRecipient notificationRecipient = MonnitSession.NotificationRecipientsInProgress[index];
        if (notificationRecipient.NotificationType == notificationType && notificationRecipient.SerializedRecipientProperties == properties && notificationRecipient.DelayMinutes == delay && notificationRecipient.ActionToExecuteID == executeID)
        {
          removeIndex = index;
          break;
        }
      }
      return removeIndex >= 0 && this.RemoveRecipient(removeIndex) ? (ActionResult) this.Content("Success") : (ActionResult) this.Content("Failed to remove recipient");
    }
    catch
    {
      return (ActionResult) this.Content("Unable to remove");
    }
  }

  [AuthorizeDefault]
  public ActionResult EditResetAccumulator(long id)
  {
    Notification notification = Notification.Load(id);
    ErrorModel errorModel = new ErrorModel();
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (EditResetAccumulator), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 2547);
      }
      if (notification == null || !MonnitSession.IsAuthorizedForAccount(notification.AccountID))
        return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("You do not have permission to access this page."));
      List<Sensor> resetAccList = NotificationRecipientDevice.LoadByAccountID(notification.AccountID).ResetAccList;
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__54.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__54.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationID", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = RuleController.\u003C\u003Eo__54.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__54.\u003C\u003Ep__0, this.ViewBag, notification.NotificationID);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__54.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__54.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Notification, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Notification", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = RuleController.\u003C\u003Eo__54.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__54.\u003C\u003Ep__1, this.ViewBag, notification);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__54.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__54.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = RuleController.\u003C\u003Eo__54.\u003C\u003Ep__2.Target((CallSite) RuleController.\u003C\u003Eo__54.\u003C\u003Ep__2, this.ViewBag, notification.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (r => r.NotificationType == eNotificationType.ResetAccumulator)).ToList<NotificationRecipient>());
      return (ActionResult) this.View((object) resetAccList);
    }
    catch
    {
    }
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("Edit Reset Accumulator Failed to Load"));
  }

  [AuthorizeDefault]
  public ActionResult EditResetAccSettings(long id, FormCollection collection)
  {
    Notification notification = Notification.Load(id);
    ErrorModel errorModel = new ErrorModel();
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (EditResetAccSettings), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 2578);
      }
      if (notification == null || !MonnitSession.IsAuthorizedForAccount(notification.AccountID))
        return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("You do not have permission to access this page."));
      List<NotificationRecipient> list = notification.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.ResetAccumulator)).ToList<NotificationRecipient>();
      for (int index = 0; index < list.Count; ++index)
      {
        NotificationRecipient notificationRecipient1 = list[index];
        Sensor sensor = Sensor.Load(notificationRecipient1.DeviceToNotifyID);
        if (sensor != null)
        {
          int num1 = 0;
          int num2 = 0;
          int acc;
          long deviceToNotifyId;
          if (sensor.ApplicationID == 93L)
          {
            CurrentZeroToTwentyAmp.ParseSerializedRecipientProperties(notificationRecipient1.SerializedRecipientProperties, out acc);
            FormCollection formCollection1 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name1 = "relay1Toggle_" + deviceToNotifyId.ToString();
            num1 = formCollection1[name1].ToBool() ? 2 : 1;
            NotificationRecipient notificationRecipient2 = notificationRecipient1;
            FormCollection formCollection2 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name2 = "relay1delayMinutes_" + deviceToNotifyId.ToString();
            int num3 = formCollection2[name2].ToInt();
            notificationRecipient2.DelayMinutes = num3;
            notificationRecipient1.SerializedRecipientProperties = CurrentZeroToTwentyAmp.CreateSerializedRecipientProperties(acc);
          }
          if (sensor.ApplicationID == 94L)
          {
            CurrentZeroToOneFiftyAmp.ParseSerializedRecipientProperties(notificationRecipient1.SerializedRecipientProperties, out acc);
            FormCollection formCollection3 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name3 = "relay1Toggle_" + deviceToNotifyId.ToString();
            num1 = formCollection3[name3].ToBool() ? 2 : 1;
            NotificationRecipient notificationRecipient3 = notificationRecipient1;
            FormCollection formCollection4 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name4 = "relay1delayMinutes_" + deviceToNotifyId.ToString();
            int num4 = formCollection4[name4].ToInt();
            notificationRecipient3.DelayMinutes = num4;
            notificationRecipient1.SerializedRecipientProperties = CurrentZeroToOneFiftyAmp.CreateSerializedRecipientProperties(acc);
          }
          if (sensor.ApplicationID == 120L)
          {
            CurrentZeroTo500Amp.ParseSerializedRecipientProperties(notificationRecipient1.SerializedRecipientProperties, out acc);
            FormCollection formCollection5 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name5 = "relay1Toggle_" + deviceToNotifyId.ToString();
            num1 = formCollection5[name5].ToBool() ? 2 : 1;
            NotificationRecipient notificationRecipient4 = notificationRecipient1;
            FormCollection formCollection6 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name6 = "relay1delayMinutes_" + deviceToNotifyId.ToString();
            int num5 = formCollection6[name6].ToInt();
            notificationRecipient4.DelayMinutes = num5;
            notificationRecipient1.SerializedRecipientProperties = CurrentZeroTo500Amp.CreateSerializedRecipientProperties(acc);
          }
          if (sensor.ApplicationID == 73L)
          {
            FilteredPulseCounter.ParseSerializedRecipientProperties(notificationRecipient1.SerializedRecipientProperties, out acc);
            FormCollection formCollection7 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name7 = "relay1Toggle_" + deviceToNotifyId.ToString();
            num1 = formCollection7[name7].ToBool() ? 2 : 1;
            NotificationRecipient notificationRecipient5 = notificationRecipient1;
            FormCollection formCollection8 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name8 = "relay1delayMinutes_" + deviceToNotifyId.ToString();
            int num6 = formCollection8[name8].ToInt();
            notificationRecipient5.DelayMinutes = num6;
            notificationRecipient1.SerializedRecipientProperties = FilteredPulseCounter.CreateSerializedRecipientProperties(acc);
          }
          if (sensor.ApplicationID == 90L)
          {
            FilteredPulseCounter64.ParseSerializedRecipientProperties(notificationRecipient1.SerializedRecipientProperties, out acc);
            FormCollection formCollection9 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name9 = "relay1Toggle_" + deviceToNotifyId.ToString();
            num1 = formCollection9[name9].ToBool() ? 2 : 1;
            NotificationRecipient notificationRecipient6 = notificationRecipient1;
            FormCollection formCollection10 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name10 = "relay1delayMinutes_" + deviceToNotifyId.ToString();
            int num7 = formCollection10[name10].ToInt();
            notificationRecipient6.DelayMinutes = num7;
            notificationRecipient1.SerializedRecipientProperties = FilteredPulseCounter64.CreateSerializedRecipientProperties(acc);
          }
          if (sensor.ApplicationID == 153L)
          {
            int state1;
            int state2;
            TwoInputPulseCounter.ParseSerializedRecipientProperties(notificationRecipient1.SerializedRecipientProperties, out state1, out state2, out acc);
            if (state1 > 0)
            {
              FormCollection formCollection11 = collection;
              deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
              string name11 = "relay1Toggle_" + deviceToNotifyId.ToString();
              num1 = formCollection11[name11].ToBool() ? 2 : 1;
              NotificationRecipient notificationRecipient7 = notificationRecipient1;
              FormCollection formCollection12 = collection;
              deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
              string name12 = "relay1delayMinutes_" + deviceToNotifyId.ToString();
              int num8 = formCollection12[name12].ToInt();
              notificationRecipient7.DelayMinutes = num8;
              notificationRecipient1.SerializedRecipientProperties = TwoInputPulseCounter.CreateSerializedRecipientProperties(state1, state2, acc);
            }
            else if (state2 > 0)
            {
              FormCollection formCollection13 = collection;
              deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
              string name13 = "relay2Toggle_" + deviceToNotifyId.ToString();
              num2 = formCollection13[name13].ToBool() ? 2 : 1;
              NotificationRecipient notificationRecipient8 = notificationRecipient1;
              FormCollection formCollection14 = collection;
              deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
              string name14 = "relay2delayMinutes_" + deviceToNotifyId.ToString();
              int num9 = formCollection14[name14].ToInt();
              notificationRecipient8.DelayMinutes = num9;
              notificationRecipient1.SerializedRecipientProperties = TwoInputPulseCounter.CreateSerializedRecipientProperties(state1, state2, acc);
            }
          }
          notificationRecipient1.Save();
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.EditResetAccSettings ");
      return (ActionResult) this.Content(ex.Message);
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ToggleResetAccDeviceEdit(
    long id,
    long deviceID,
    string deviceType,
    bool add)
  {
    try
    {
      Notification notification = Notification.Load(id);
      if (notification == null)
        return (ActionResult) this.Content("Failed: Notification not found");
      Sensor sensor = Sensor.Load(deviceID);
      if (sensor == null)
        return (ActionResult) this.Content("Failed: Device not found");
      if (notification.AccountID != sensor.AccountID)
        return (ActionResult) this.Content("Failed: Device not found");
      if (add)
      {
        string changeRecord = string.Format("{{\"SensorID\": \"{0}\",  ResetAccumulator : {{ \"State1\" : \"{2}\", \"State2\" : \"{3}\", \"Relay1Time\" : \"{4}\", \"Relay2Time\" : \"{5}\" }}  , \"NotifcationID\" : \"{1}\" }} ", (object) sensor.SensorID, (object) notification.NotificationID, (object) 1, (object) 0, (object) 0, (object) 0);
        notification.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, notification.AccountID, "Reset Accumulator applied to notification");
        switch (deviceType)
        {
          case "CurrentZeroTo500Amp":
            notification.AddSensorRecipient(sensor, CurrentZeroTo500Amp.CreateSerializedRecipientProperties(5)).Save();
            notification.HasResetAccAction = true;
            break;
          case "CurrentZeroToOneFiftyAmp":
            notification.AddSensorRecipient(sensor, CurrentZeroToOneFiftyAmp.CreateSerializedRecipientProperties(5)).Save();
            notification.HasResetAccAction = true;
            break;
          case "CurrentZeroToTwentyAmp":
            notification.AddSensorRecipient(sensor, CurrentZeroToTwentyAmp.CreateSerializedRecipientProperties(5)).Save();
            notification.HasResetAccAction = true;
            break;
          case "FilteredPulse":
            notification.AddSensorRecipient(sensor, FilteredPulseCounter.CreateSerializedRecipientProperties(3)).Save();
            notification.HasResetAccAction = true;
            break;
          case "FilteredPulse64":
            notification.AddSensorRecipient(sensor, FilteredPulseCounter64.CreateSerializedRecipientProperties(3)).Save();
            notification.HasResetAccAction = true;
            break;
          case "TwoInputPulseRelay1":
            notification.AddSensorRecipient(sensor, TwoInputPulseCounter.CreateSerializedRecipientProperties(1, 0, 3));
            notification.HasResetAccAction = true;
            break;
          case "TwoInputPulseRelay2":
            notification.AddSensorRecipient(sensor, TwoInputPulseCounter.CreateSerializedRecipientProperties(0, 1, 3));
            notification.HasResetAccAction = true;
            break;
        }
        notification.Save();
      }
      else
      {
        NotificationRecipient notificationRecipient = NotificationControllerBase.FindNotificationRecipient(deviceID, deviceType, notification);
        if (notificationRecipient == null)
          return (ActionResult) this.Content("Not found");
        string changeRecord = $"{{\"NotificationID\" : \"{notification.NotificationID}\", \"DeviceID\": \"{deviceID}\", \"DeviceType\" : \"{deviceType}\", \"Add\" : \"{(ValueType) false}\" }} ";
        notification.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, notification.AccountID, "Removed device from notification");
        notification.RemoveRecipientDevice(notificationRecipient);
        if (notification.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.ResetAccumulator)).Count<NotificationRecipient>() == 0)
          notification.HasResetAccAction = false;
        notification.Save();
      }
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__56.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__56.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RuleController.\u003C\u003Eo__56.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__56.\u003C\u003Ep__0, this.ViewBag, notification.NotificationRecipients);
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log("ToggleResetAccDeviceEdit Failed");
      return (ActionResult) this.Content("Unable to add device");
    }
  }

  [AuthorizeDefault]
  public ActionResult ConfigureResetAccumulator()
  {
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (ConfigureResetAccumulator), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 2797);
      }
      if (MonnitSession.NotificationInProgress.AccountID < 1L)
        return (ActionResult) this.Redirect("/Rule/ChooseType/");
      MonnitSession.NotificationInProgress.NotificationType = eNotificationType.ResetAccumulator;
      List<Sensor> resetAccList = NotificationRecipientDevice.LoadByAccountID(MonnitSession.NotificationInProgress.AccountID).ResetAccList;
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__57.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__57.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RuleController.\u003C\u003Eo__57.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__57.\u003C\u003Ep__0, this.ViewBag, MonnitSession.NotificationRecipientsInProgress);
      return (ActionResult) this.View(nameof (ConfigureResetAccumulator), (object) resetAccList);
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.ConfigureResetAccumulator ");
    }
    return (ActionResult) this.Redirect("/Rule/ChooseType/");
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ToggleResetAccDevice(long deviceID, string deviceType, bool add)
  {
    try
    {
      Notification notificationInProgress = MonnitSession.NotificationInProgress;
      if (notificationInProgress == null)
        return (ActionResult) this.Content("Rule not found");
      Sensor sensor = Sensor.Load(deviceID);
      if (sensor == null)
        return (ActionResult) this.Content("Device not found");
      if (add)
      {
        if (notificationInProgress.AccountID != sensor.AccountID)
          return (ActionResult) this.Content("Device not found");
        switch (deviceType)
        {
          case "CurrentZeroTo500Amp":
            this.AddSensorRecipient(sensor, CurrentZeroTo500Amp.CreateSerializedRecipientProperties(5));
            notificationInProgress.HasResetAccAction = true;
            break;
          case "CurrentZeroToOneFiftyAmp":
            this.AddSensorRecipient(sensor, CurrentZeroToOneFiftyAmp.CreateSerializedRecipientProperties(5));
            notificationInProgress.HasResetAccAction = true;
            break;
          case "CurrentZeroToTwentyAmp":
            this.AddSensorRecipient(sensor, CurrentZeroToTwentyAmp.CreateSerializedRecipientProperties(5));
            notificationInProgress.HasResetAccAction = true;
            break;
          case "FilteredPulse":
            this.AddSensorRecipient(sensor, FilteredPulseCounter.CreateSerializedRecipientProperties(3));
            notificationInProgress.HasResetAccAction = true;
            break;
          case "FilteredPulse64":
            this.AddSensorRecipient(sensor, FilteredPulseCounter64.CreateSerializedRecipientProperties(3));
            notificationInProgress.HasResetAccAction = true;
            break;
          case "TwoInputPulseRelay1":
            this.AddSensorRecipient(sensor, TwoInputPulseCounter.CreateSerializedRecipientProperties(1, 0, 3));
            notificationInProgress.HasResetAccAction = true;
            break;
          case "TwoInputPulseRelay2":
            this.AddSensorRecipient(sensor, TwoInputPulseCounter.CreateSerializedRecipientProperties(0, 1, 3));
            notificationInProgress.HasResetAccAction = true;
            break;
        }
      }
      else
      {
        int foundIndex;
        if (this.FindNotificationRecipient(deviceID, deviceType, out foundIndex) == null)
          return (ActionResult) this.Content("Not found");
        if (!this.RemoveRecipient(foundIndex))
        {
          if (MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.ResetAccumulator)).Count<NotificationRecipient>() == 0)
            MonnitSession.NotificationInProgress.HasResetAccAction = false;
          return (ActionResult) this.Content("Failed to remove recipient");
        }
      }
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__58.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__58.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RuleController.\u003C\u003Eo__58.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__58.\u003C\u003Ep__0, this.ViewBag, MonnitSession.NotificationRecipientsInProgress);
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Unable to add device");
    }
  }

  [AuthorizeDefault]
  public ActionResult SaveResetAccSettings(FormCollection collection)
  {
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
        return (ActionResult) this.Content("Unauthorized");
      if (MonnitSession.NotificationInProgress.AccountID < 1L)
        return (ActionResult) this.Content("No Rule Found");
      MonnitSession.NotificationInProgress.NotificationType = eNotificationType.ResetAccumulator;
      List<NotificationRecipient> list = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.ResetAccumulator)).ToList<NotificationRecipient>();
      MonnitSession.NotificationRecipientsInProgress = MonnitSession.NotificationRecipientsInProgress.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType != eNotificationType.ResetAccumulator)).ToList<NotificationRecipient>();
      for (int index = 0; index < list.Count; ++index)
      {
        NotificationRecipient notificationRecipient1 = list[index];
        Sensor sensor = Sensor.Load(notificationRecipient1.DeviceToNotifyID);
        if (sensor != null)
        {
          int state1_1 = 0;
          int state2_1 = 0;
          int acc;
          long deviceToNotifyId;
          if (sensor.ApplicationID == 93L)
          {
            CurrentZeroToTwentyAmp.ParseSerializedRecipientProperties(notificationRecipient1.SerializedRecipientProperties, out acc);
            FormCollection formCollection1 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name1 = "relay1Toggle_" + deviceToNotifyId.ToString();
            state1_1 = formCollection1[name1].ToBool() ? 2 : 1;
            NotificationRecipient notificationRecipient2 = notificationRecipient1;
            FormCollection formCollection2 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name2 = "relay1delayMinutes_" + deviceToNotifyId.ToString();
            int num = formCollection2[name2].ToInt();
            notificationRecipient2.DelayMinutes = num;
            notificationRecipient1.SerializedRecipientProperties = CurrentZeroToTwentyAmp.CreateSerializedRecipientProperties(acc);
          }
          if (sensor.ApplicationID == 94L)
          {
            CurrentZeroToOneFiftyAmp.ParseSerializedRecipientProperties(notificationRecipient1.SerializedRecipientProperties, out acc);
            FormCollection formCollection3 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name3 = "relay1Toggle_" + deviceToNotifyId.ToString();
            state1_1 = formCollection3[name3].ToBool() ? 2 : 1;
            NotificationRecipient notificationRecipient3 = notificationRecipient1;
            FormCollection formCollection4 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name4 = "relay1delayMinutes_" + deviceToNotifyId.ToString();
            int num = formCollection4[name4].ToInt();
            notificationRecipient3.DelayMinutes = num;
            notificationRecipient1.SerializedRecipientProperties = CurrentZeroToOneFiftyAmp.CreateSerializedRecipientProperties(acc);
          }
          if (sensor.ApplicationID == 120L)
          {
            CurrentZeroTo500Amp.ParseSerializedRecipientProperties(notificationRecipient1.SerializedRecipientProperties, out acc);
            FormCollection formCollection5 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name5 = "relay1Toggle_" + deviceToNotifyId.ToString();
            state1_1 = formCollection5[name5].ToBool() ? 2 : 1;
            NotificationRecipient notificationRecipient4 = notificationRecipient1;
            FormCollection formCollection6 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name6 = "relay1delayMinutes_" + deviceToNotifyId.ToString();
            int num = formCollection6[name6].ToInt();
            notificationRecipient4.DelayMinutes = num;
            notificationRecipient1.SerializedRecipientProperties = CurrentZeroTo500Amp.CreateSerializedRecipientProperties(acc);
          }
          if (sensor.ApplicationID == 73L)
          {
            FilteredPulseCounter.ParseSerializedRecipientProperties(notificationRecipient1.SerializedRecipientProperties, out acc);
            FormCollection formCollection7 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name7 = "relay1Toggle_" + deviceToNotifyId.ToString();
            state1_1 = formCollection7[name7].ToBool() ? 2 : 1;
            NotificationRecipient notificationRecipient5 = notificationRecipient1;
            FormCollection formCollection8 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name8 = "relay1delayMinutes_" + deviceToNotifyId.ToString();
            int num = formCollection8[name8].ToInt();
            notificationRecipient5.DelayMinutes = num;
            notificationRecipient1.SerializedRecipientProperties = FilteredPulseCounter.CreateSerializedRecipientProperties(acc);
          }
          if (sensor.ApplicationID == 90L)
          {
            FilteredPulseCounter64.ParseSerializedRecipientProperties(notificationRecipient1.SerializedRecipientProperties, out acc);
            FormCollection formCollection9 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name9 = "relay1Toggle_" + deviceToNotifyId.ToString();
            state1_1 = formCollection9[name9].ToBool() ? 2 : 1;
            NotificationRecipient notificationRecipient6 = notificationRecipient1;
            FormCollection formCollection10 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name10 = "relay1delayMinutes_" + deviceToNotifyId.ToString();
            int num = formCollection10[name10].ToInt();
            notificationRecipient6.DelayMinutes = num;
            notificationRecipient1.SerializedRecipientProperties = FilteredPulseCounter64.CreateSerializedRecipientProperties(acc);
          }
          if (sensor.ApplicationID == 153L)
          {
            int state1_2;
            int state2_2;
            TwoInputPulseCounter.ParseSerializedRecipientProperties(notificationRecipient1.SerializedRecipientProperties, out state1_2, out state2_2, out acc);
            if (state1_2 > 0)
            {
              FormCollection formCollection11 = collection;
              deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
              string name11 = "relay1Toggle_" + deviceToNotifyId.ToString();
              state1_1 = formCollection11[name11].ToBool() ? 2 : 1;
              NotificationRecipient notificationRecipient7 = notificationRecipient1;
              FormCollection formCollection12 = collection;
              deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
              string name12 = "relay1delayMinutes_" + deviceToNotifyId.ToString();
              int num = formCollection12[name12].ToInt();
              notificationRecipient7.DelayMinutes = num;
            }
            else if (state2_2 > 0)
            {
              FormCollection formCollection13 = collection;
              deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
              string name13 = "relay2Toggle_" + deviceToNotifyId.ToString();
              state2_1 = formCollection13[name13].ToBool() ? 2 : 1;
              NotificationRecipient notificationRecipient8 = notificationRecipient1;
              FormCollection formCollection14 = collection;
              deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
              string name14 = "relay2delayMinutes_" + deviceToNotifyId.ToString();
              int num = formCollection14[name14].ToInt();
              notificationRecipient8.DelayMinutes = num;
            }
            notificationRecipient1.SerializedRecipientProperties = TwoInputPulseCounter.CreateSerializedRecipientProperties(state1_1, state2_1, acc);
          }
          MonnitSession.NotificationRecipientsInProgress.Add(notificationRecipient1);
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.SaveResetAccSettings ");
      return (ActionResult) this.Content(ex.Message);
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult ToggleRule(long id, bool active)
  {
    Notification DBObject = Notification.Load(id);
    if (DBObject == null)
      return (ActionResult) this.Content("No Notification Found");
    if (!this.HttpContext.User.Identity.IsAuthenticated || !MonnitSession.IsAuthorizedForAccount(DBObject.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.Content("Unauthorized");
    string str = " ";
    if (!active)
      str = " not ";
    Account account = Account.Load(DBObject.AccountID);
    if (account != null)
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, $"Notification set to{str}active");
    DBObject.IsActive = active;
    if (!active)
    {
      List<NotificationTriggered> notificationTriggeredList = NotificationTriggered.LoadActiveByNotificationID(DBObject.NotificationID);
      if (notificationTriggeredList.Count > 0)
      {
        foreach (NotificationTriggered notificationTriggered in notificationTriggeredList)
        {
          notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
          notificationTriggered.AcknowledgedBy = MonnitSession.CurrentCustomer.CustomerID;
          notificationTriggered.resetTime = DateTime.UtcNow;
          notificationTriggered.Save();
          AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, notificationTriggered.NotificationID, eAuditAction.Related_Modify, eAuditObject.Notification, notificationTriggered.JsonStringify(), account.AccountID, "Acknowledged rule");
        }
      }
    }
    DBObject.Save();
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult EditSendAlert(long id)
  {
    Notification model = Notification.Load(id);
    ErrorModel errorModel = new ErrorModel();
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (EditSendAlert), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 3087);
      }
      if (model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID))
        return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("You do not have permission to access this page."));
      List<CustomerGroup> customerGroupList = CustomerGroup.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__61.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__61.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<CustomerGroup>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "UserGroups", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RuleController.\u003C\u003Eo__61.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__61.\u003C\u003Ep__0, this.ViewBag, customerGroupList);
      return (ActionResult) this.View((object) model);
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.EditSendAlert");
    }
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("Edit Email Page Failed to Load"));
  }

  [AuthorizeDefault]
  public ActionResult EditEmailNotification(long id)
  {
    Notification model = Notification.Load(id);
    ErrorModel errorModel = new ErrorModel();
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (EditEmailNotification), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 3131);
      }
      if (model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID))
        return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("You do not have permission to access this page."));
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__62.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__62.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, eNotificationType, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationType", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = RuleController.\u003C\u003Eo__62.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__62.\u003C\u003Ep__0, this.ViewBag, eNotificationType.Email);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__62.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__62.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = RuleController.\u003C\u003Eo__62.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__62.\u003C\u003Ep__1, this.ViewBag, model.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (r => r.NotificationType == eNotificationType.Email || r.NotificationType == eNotificationType.Group)).ToList<NotificationRecipient>());
      return (ActionResult) this.View((object) model);
    }
    catch
    {
    }
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("Edit Email Page Failed to Load"));
  }

  [AuthorizeDefault]
  [ValidateInput(false)]
  [HttpPost]
  public ActionResult EditEmailNotificationSettings(long id, FormCollection collection)
  {
    string content1 = "";
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.Content("Failed: Unauthorized");
    Notification DBObject = Notification.Load(id);
    if (DBObject == null || !MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
      return (ActionResult) this.Content("Failed: No Rule Found");
    string content2;
    try
    {
      if (!string.IsNullOrEmpty(collection["subject"]) && collection["subject"].HasScriptTag())
      {
        content1 = "subject: Script tags not permitted.";
        return (ActionResult) this.Content(content1);
      }
      if (string.IsNullOrEmpty(collection["emailMsg"]) || collection["emailMsg"] == "<p><br></p>")
      {
        DBObject.NotificationText = "Device: {Name}({ID}) Reading: {Reading}";
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Edited notification settings Email");
        DBObject.Save();
      }
      if (string.IsNullOrWhiteSpace(content1))
      {
        collection["subject"] = collection["subject"].SanitizeHTMLEvent();
        collection["emailMsg"] = collection["emailMsg"].SanitizeButAllowSomeHTMLEvent();
        DBObject.Subject = collection["subject"];
        DBObject.NotificationText = collection["emailMsg"];
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Edited notification settings");
        DBObject.Save();
      }
      content2 = "Success";
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.SaveEmailNotification ");
      string str = "misc:Failed";
      content2 = string.IsNullOrWhiteSpace(content1) ? str : $"{content1}|{str}";
    }
    return (ActionResult) this.Content(content2);
  }

  [AuthorizeDefault]
  [ValidateInput(false)]
  [HttpPost]
  public ActionResult EditAlertNotificationSettings(long id, FormCollection collection)
  {
    string content1 = "";
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.Content("Failed: Unauthorized");
    Notification DBObject = Notification.Load(id);
    if (DBObject == null || !MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
      return (ActionResult) this.Content("Failed: No Rule Found");
    string content2;
    try
    {
      if (!string.IsNullOrEmpty(collection["subject"]) && collection["subject"].HasScriptTag())
      {
        content1 = "subject: Script tags not permitted.";
        return (ActionResult) this.Content(content1);
      }
      if (string.IsNullOrEmpty(collection["emailMsg"]) || collection["emailMsg"] == "<p><br></p>")
      {
        string str = "AlertMsg: Message can't be empty.";
        content1 = string.IsNullOrWhiteSpace(content1) ? str : $"{content1}|{str}";
        return (ActionResult) this.Content(content1);
      }
      if (string.IsNullOrWhiteSpace(content1))
      {
        collection["subject"] = collection["subject"].SanitizeHTMLEvent();
        collection["emailMsg"] = collection["emailMsg"].SanitizeButAllowSomeHTMLEvent();
        DBObject.Subject = collection["subject"];
        DBObject.NotificationText = collection["emailMsg"];
        DBObject.SMSText = collection["subject"] + Environment.NewLine + collection["emailMsg"];
        DBObject.VoiceText = collection["subject"] + Environment.NewLine + collection["emailMsg"];
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Edited notification settings");
        DBObject.Save();
      }
      content2 = "Success";
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.SaveEmailNotification ");
      string str = "misc:Failed";
      content2 = string.IsNullOrWhiteSpace(content1) ? str : $"{content1}|{str}";
    }
    return (ActionResult) this.Content(content2);
  }

  [AuthorizeDefault]
  public ActionResult EditTextNotification(long id)
  {
    Notification model = Notification.Load(id);
    ErrorModel errorModel = new ErrorModel();
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (EditTextNotification), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 3278);
      }
      if (model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID))
        return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("You do not have permission to access this page."));
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__65.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__65.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, eNotificationType, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationType", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = RuleController.\u003C\u003Eo__65.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__65.\u003C\u003Ep__0, this.ViewBag, eNotificationType.SMS);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__65.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__65.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = RuleController.\u003C\u003Eo__65.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__65.\u003C\u003Ep__1, this.ViewBag, model.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (r => r.NotificationType == eNotificationType.SMS)).ToList<NotificationRecipient>());
      return (ActionResult) this.View((object) model);
    }
    catch
    {
    }
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("Edit Email Page Failed to Load"));
  }

  [AuthorizeDefault]
  [ValidateInput(false)]
  [HttpPost]
  public ActionResult EditTextNotificationSettings(long id, FormCollection collection)
  {
    string str1 = "";
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.Content("Failed: Unauthorized");
    Notification DBObject = Notification.Load(id);
    if (DBObject == null || !MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
      return (ActionResult) this.Content("Failed: No Rule Found");
    string content;
    try
    {
      if (!string.IsNullOrEmpty(collection["SMSText"]) && collection["SMSText"].HasScriptTag())
        str1 = "SMSText: Script tags not permitted.";
      if (string.IsNullOrEmpty(collection["SMSText"]))
      {
        DBObject.SMSText = collection["SMSText"];
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Edited rule settings SMS");
        DBObject.Save();
      }
      if (string.IsNullOrWhiteSpace(str1))
      {
        collection["SMSText"] = collection["SMSText"].SanitizeHTMLEvent();
        DBObject.SMSText = collection["SMSText"];
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Edited rule settings");
        DBObject.Save();
      }
      content = "Success";
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.EditTextNotification ");
      string str2 = "misc:Failed";
      content = string.IsNullOrWhiteSpace(str1) ? str2 : $"{str1}|{str2}";
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public ActionResult EditPushNotification(long id)
  {
    Notification model = Notification.Load(id);
    ErrorModel errorModel = new ErrorModel();
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (EditPushNotification), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 3362);
      }
      if (model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID))
        return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("You do not have permission to access this page."));
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__67.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__67.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, eNotificationType, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationType", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = RuleController.\u003C\u003Eo__67.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__67.\u003C\u003Ep__0, this.ViewBag, eNotificationType.Push_Message);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__67.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__67.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = RuleController.\u003C\u003Eo__67.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__67.\u003C\u003Ep__1, this.ViewBag, model.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (r => r.NotificationType == eNotificationType.Push_Message)).ToList<NotificationRecipient>());
      return (ActionResult) this.View((object) model);
    }
    catch
    {
    }
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("Edit Email Page Failed to Load"));
  }

  [AuthorizeDefault]
  [ValidateInput(false)]
  [HttpPost]
  public ActionResult EditPushNotificationSettings(long id, FormCollection collection)
  {
    string str1 = "";
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.Content("Failed: Unauthorized");
    Notification DBObject = Notification.Load(id);
    if (DBObject == null || !MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
      return (ActionResult) this.Content("Failed: No Rule Found");
    string content;
    try
    {
      if (!string.IsNullOrEmpty(collection["PushText"]) && collection["PushText"].HasScriptTag())
        str1 = "PushText: Script tags not permitted.";
      if (string.IsNullOrEmpty(collection["PushText"]))
      {
        string str2 = "PushText: Message can't be empty.";
        str1 = string.IsNullOrWhiteSpace(str1) ? str2 : $"{str1}|{str2}";
      }
      if (string.IsNullOrWhiteSpace(str1))
      {
        collection["PushText"] = collection["PushText"].SanitizeHTMLEvent();
        DBObject.PushMsgText = collection["PushText"];
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Edited rule settings");
        DBObject.Save();
      }
      content = "Success";
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.EditPushNotificationSettings ");
      string str3 = "misc:Failed";
      content = string.IsNullOrWhiteSpace(str1) ? str3 : $"{str1}|{str3}";
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public ActionResult EditVoiceNotification(long id)
  {
    Notification model = Notification.Load(id);
    ErrorModel errorModel = new ErrorModel();
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (EditVoiceNotification), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 3440);
      }
      if (model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID))
        return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("You do not have permission to access this page."));
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__69.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__69.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, eNotificationType, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationType", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = RuleController.\u003C\u003Eo__69.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__69.\u003C\u003Ep__0, this.ViewBag, eNotificationType.Phone);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__69.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__69.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = RuleController.\u003C\u003Eo__69.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__69.\u003C\u003Ep__1, this.ViewBag, model.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (r => r.NotificationType == eNotificationType.Phone)).ToList<NotificationRecipient>());
      return (ActionResult) this.View((object) model);
    }
    catch
    {
    }
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("Edit Email Page Failed to Load"));
  }

  [AuthorizeDefault]
  [ValidateInput(false)]
  [HttpPost]
  public ActionResult EditVoiceNotificationSettings(long id, FormCollection collection)
  {
    string str1 = "";
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.Content("Failed: Unauthorized");
    Notification DBObject = Notification.Load(id);
    if (DBObject == null || !MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
      return (ActionResult) this.Content("Failed: No Rule Found");
    string content;
    try
    {
      if (!string.IsNullOrEmpty(collection["VoiceText"]) && collection["VoiceText"].HasScriptTag())
        str1 = "VoiceText: Script tags not permitted.";
      if (string.IsNullOrEmpty(collection["VoiceText"]))
      {
        DBObject.VoiceText = collection["VoiceText"];
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Edited notification settings Email");
        DBObject.Save();
      }
      if (string.IsNullOrWhiteSpace(str1))
      {
        collection["VoiceText"] = collection["VoiceText"].SanitizeHTMLEvent();
        DBObject.VoiceText = collection["VoiceText"];
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Edited rule settings");
        DBObject.Save();
      }
      content = "Success";
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.EditVoiceNotification ");
      string str2 = "misc:Failed";
      content = string.IsNullOrWhiteSpace(str1) ? str2 : $"{str1}|{str2}";
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public ActionResult RemoveSelfFromRecipientsList(
    long notificationID,
    long customerID,
    long notiType)
  {
    foreach (NotificationRecipient notificationRecipient in NotificationRecipient.LoadByNotificationID(notificationID))
    {
      if (notificationRecipient.CustomerToNotifyID == customerID && notificationRecipient.NotificationType == (eNotificationType) notiType && notificationRecipient.NotificationID == notificationID)
        notificationRecipient.Delete();
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult AddSelfFromRecipientsList(
    long notificationID,
    long customerID,
    long notiType)
  {
    new NotificationRecipient()
    {
      NotificationID = notificationID,
      CustomerToNotifyID = customerID,
      NotificationType = ((eNotificationType) notiType)
    }.Save();
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult EditPageSaveRecipients(long id, FormCollection collection)
  {
    Notification notification = Notification.Load(id);
    ErrorModel errorModel = new ErrorModel();
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.Content("Failed: Unauthorized");
    if (notification == null || !MonnitSession.IsAuthorizedForAccount(notification.AccountID))
      return (ActionResult) this.Content("Failed: No Rule Found");
    if (string.IsNullOrEmpty(collection["type"]))
      return (ActionResult) this.Content("Failed: No type Found");
    eNotificationType eNotiType = (eNotificationType) collection["type"].ToInt();
    List<NotificationRecipient> notificationRecipients = notification.NotificationRecipients;
    try
    {
      string[] strArray1 = collection["recipient"].Split(',');
      string str1 = "";
      if (eNotiType == eNotificationType.Email)
      {
        for (int index = notificationRecipients.Count - 1; index >= 0; --index)
        {
          try
          {
            if (notificationRecipients[index].NotificationType == eNotificationType.Group)
            {
              notificationRecipients[index].Delete();
              notificationRecipients.RemoveAt(index);
            }
          }
          catch
          {
            string str2 = "Failed: Could Not Empty Group Recipient List";
            str1 = string.IsNullOrWhiteSpace(str1) ? str2 : $"{str1}|{str2}";
          }
        }
      }
      for (int index = notificationRecipients.Count - 1; index >= 0; --index)
      {
        try
        {
          if (notificationRecipients[index].NotificationType == eNotiType)
          {
            notificationRecipients[index].Delete();
            notificationRecipients.RemoveAt(index);
          }
        }
        catch
        {
          string str3 = "Failed: Could Not Empty Recipient List";
          str1 = string.IsNullOrWhiteSpace(str1) ? str3 : $"{str1}|{str3}";
        }
      }
      foreach (string str4 in strArray1)
      {
        string[] strArray2 = str4.Split('|');
        long customerID = strArray2[0].ToLong();
        string str5 = strArray2.Length > 1 ? strArray2[1].ToString() : "";
        int num = strArray2.Length > 2 ? strArray2[2].ToInt() : 0;
        long customerGroupID = strArray2.Length > 3 ? strArray2[3].ToLong() : long.MinValue;
        if (customerGroupID > 0L)
        {
          if (CustomerGroup.Load(customerGroupID) == null)
          {
            string str6 = $"misc:Failed to assign [{str5}] to rule.";
            str1 = string.IsNullOrWhiteSpace(str1) ? str6 : $"{str1}|{str6}";
          }
        }
        else
        {
          Customer customer = Customer.Load(customerID);
          if (customer != null)
          {
            try
            {
              if (notification.AccountID != customer.AccountID)
              {
                if (CustomerAccountLink.Load(customer.CustomerID, notification.AccountID) == null)
                {
                  bool flag = false;
                  foreach (Account ancestor in Account.Ancestors(MonnitSession.CurrentCustomer.CustomerID, notification.AccountID))
                  {
                    if (ancestor.AccountID == customer.AccountID)
                    {
                      flag = true;
                      break;
                    }
                  }
                  if (!flag)
                  {
                    string str7 = $"misc:Failed to assign [{customer.FullName}] to rule.";
                    str1 = string.IsNullOrWhiteSpace(str1) ? str7 : $"{str1}|{str7}";
                  }
                }
              }
            }
            catch (Exception ex)
            {
              ex.Log($"RuleController.EditPageSaveRecipients[CustomerID: {customerID.ToString()}] Failed to assign customer to rule.");
            }
          }
        }
        if (string.IsNullOrWhiteSpace(str1))
        {
          try
          {
            NotificationRecipient DBObject;
            if (customerGroupID > 0L)
            {
              DBObject = notificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.CustomerGroupID == customerGroupID && m.NotificationType == eNotificationType.Group)).FirstOrDefault<NotificationRecipient>();
              if (DBObject == null)
              {
                DBObject = new NotificationRecipient()
                {
                  CustomerGroupID = customerGroupID,
                  NotificationID = notification.NotificationID,
                  NotificationType = eNotificationType.Group
                };
                notificationRecipients.Add(DBObject);
              }
            }
            else
            {
              DBObject = notificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.CustomerToNotifyID == customerID && m.NotificationType == eNotiType)).FirstOrDefault<NotificationRecipient>();
              if (DBObject == null)
              {
                DBObject = new NotificationRecipient()
                {
                  CustomerToNotifyID = customerID,
                  NotificationID = notification.NotificationID,
                  NotificationType = eNotiType
                };
                notificationRecipients.Add(DBObject);
              }
            }
            DBObject.DelayMinutes = num;
            DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, notification.AccountID, "Assigned recipient to notification");
            DBObject.Save();
          }
          catch (Exception ex)
          {
            ex.Log($"RuleController.EditPageSaveRecipients[Post][Add Customer][CustomerID: {customerID.ToString()}] ");
            string str8 = $"misc:Failed to assign [{str5}] to rule.";
            str1 = string.IsNullOrWhiteSpace(str1) ? str8 : $"{str1}|{str8}";
          }
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.EditPageSaveRecipients ");
    }
    List<NotificationRecipient> list = notificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotiType)).ToList<NotificationRecipient>();
    if (eNotiType == eNotificationType.Email)
      list.AddRange((IEnumerable<NotificationRecipient>) notificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.Group)).ToList<NotificationRecipient>());
    return (ActionResult) this.PartialView("_SendToNamesList", (object) list);
  }

  [AuthorizeDefault]
  public ActionResult EditPageSaveAllRecipients(long id, FormCollection collection)
  {
    Notification notification = Notification.Load(id);
    List<NotificationRecipient> notificationRecipients = notification.NotificationRecipients;
    for (int index = notificationRecipients.Count - 1; index >= 0; --index)
    {
      NotificationRecipient recipient = notificationRecipients[index];
      if (recipient.NotificationType == eNotificationType.Email || recipient.NotificationType == eNotificationType.SMS || recipient.NotificationType == eNotificationType.Both || recipient.NotificationType == eNotificationType.Phone || recipient.NotificationType == eNotificationType.Group || recipient.NotificationType == eNotificationType.Push_Message)
        notification.RemoveRecipient(recipient);
    }
    List<NotificationRecipient> model = new List<NotificationRecipient>();
    var data1 = new
    {
      Type = collection[0],
      Recipient = collection[1]
    };
    var data2 = new
    {
      Type = collection[2],
      Recipient = collection[3]
    };
    var data3 = new
    {
      Type = collection[4],
      Recipient = collection[5]
    };
    var data4 = new
    {
      Type = collection[6],
      Recipient = collection[7]
    };
    var data5 = new
    {
      Type = collection[8],
      Recipient = collection[9]
    };
    List<\u003C\u003Ef__AnonymousType7<string, string>> list = new \u003C\u003Ef__AnonymousType7<string, string>[4]
    {
      data1,
      data2,
      data3,
      data5
    }.ToList();
    ErrorModel errorModel = new ErrorModel();
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.Content("Failed: Unauthorized");
    if (notification == null || !MonnitSession.IsAuthorizedForAccount(notification.AccountID))
      return (ActionResult) this.Content("Failed: No Rule Found");
    string str1 = "";
    string recipient1 = data4.Recipient;
    char[] separator1 = new char[1]{ ',' };
    foreach (string str2 in recipient1.Split(separator1, StringSplitOptions.RemoveEmptyEntries))
    {
      string ID = str2;
      if (ID.ToLong() > 0L)
      {
        CustomerGroup customerGroup = CustomerGroup.Load(ID.ToLong());
        if (customerGroup == null)
        {
          string str3 = $"misc:Failed to assign [{customerGroup.Name}] to rule.";
          str1 = string.IsNullOrWhiteSpace(str1) ? str3 : $"{str1}|{str3}";
        }
        if (str1.Length == 0)
        {
          try
          {
            NotificationRecipient DBObject = notificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.CustomerGroupID == ID.ToLong() && m.NotificationType == eNotificationType.Group)).FirstOrDefault<NotificationRecipient>();
            if (DBObject == null)
            {
              DBObject = new NotificationRecipient()
              {
                CustomerGroupID = ID.ToLong(),
                NotificationID = notification.NotificationID,
                NotificationType = eNotificationType.Group
              };
              notificationRecipients.Add(DBObject);
            }
            DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, notification.AccountID, "Assigned recipient to notification");
            model.Add(DBObject);
            DBObject.Save();
          }
          catch (Exception ex)
          {
            ex.Log($"RuleController.EditPageSaveRecipients[Post][Add Customer][CustomerID: {ID}] ");
            string str4 = $"misc:Failed to assign [{customerGroup.Name}] to rule.";
            str1 = string.IsNullOrWhiteSpace(str1) ? str4 : $"{str1}|{str4}";
          }
        }
      }
    }
    foreach (var data6 in list)
    {
      eNotificationType eNotiType = (eNotificationType) data6.Type.ToInt();
      try
      {
        if (string.IsNullOrEmpty(data6.Type))
          return (ActionResult) this.Content("Failed: No type Found");
        string recipient2 = data6.Recipient;
        char[] separator2 = new char[1]{ ',' };
        foreach (string str5 in recipient2.Split(separator2, StringSplitOptions.RemoveEmptyEntries))
        {
          string[] strArray = str5.Split('|');
          long customerID = strArray[0].ToLong();
          string str6 = strArray.Length > 1 ? strArray[1].ToString() : "";
          int num = strArray.Length > 2 ? strArray[2].ToInt() : 0;
          if (string.IsNullOrWhiteSpace(str1))
          {
            try
            {
              NotificationRecipient DBObject = notificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.CustomerToNotifyID == customerID && m.NotificationType == eNotiType)).FirstOrDefault<NotificationRecipient>();
              if (DBObject == null)
              {
                DBObject = new NotificationRecipient()
                {
                  CustomerToNotifyID = customerID,
                  NotificationID = notification.NotificationID,
                  NotificationType = eNotiType
                };
                notificationRecipients.Add(DBObject);
              }
              DBObject.DelayMinutes = num;
              DBObject.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, notification.AccountID, "Assigned recipient to notification");
              model.Add(DBObject);
              DBObject.Save();
            }
            catch (Exception ex)
            {
              ex.Log($"RuleController.EditPageSaveRecipients[Post][Add Customer][CustomerID: {customerID.ToString()}] ");
              string str7 = $"misc:Failed to assign [{str6}] to rule.";
              str1 = string.IsNullOrWhiteSpace(str1) ? str7 : $"{str1}|{str7}";
            }
          }
        }
      }
      catch (Exception ex)
      {
        ex.Log("RuleController.EditPageSaveRecipients ");
      }
    }
    return (ActionResult) this.PartialView("_SendToNamesList", (object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult EditPagesRecipientList(long id, string q)
  {
    Notification notification = Notification.Load(id);
    if (notification == null)
      return (ActionResult) this.Content("Failed");
    List<NotificationRecipientData> model = NotificationRecipientData.SearchPotentialNotificationRecipient(MonnitSession.CurrentCustomer.CustomerID, id, q);
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__75.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__75.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, eNotificationType, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationType", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = RuleController.\u003C\u003Eo__75.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__75.\u003C\u003Ep__0, this.ViewBag, notification.NotificationType);
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__75.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__75.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = RuleController.\u003C\u003Eo__75.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__75.\u003C\u003Ep__1, this.ViewBag, notification.NotificationRecipients);
    return (ActionResult) this.PartialView("RecipientList", (object) model);
  }

  [AuthorizeDefault]
  [ValidateInput(false)]
  [HttpPost]
  public ActionResult EditPageMsgPreview(long id, string msg)
  {
    string content;
    try
    {
      Notification notification = Notification.Load(id);
      if (notification == null || !MonnitSession.CurrentCustomer.CanSeeAccount(notification.AccountID))
        return (ActionResult) this.Content("No Rule Found");
      Sensor sensor = Sensor.Load(notification.SensorID);
      Gateway gateway = Gateway.Load(notification.GatewayID);
      CSNet network = sensor != null ? CSNet.Load(sensor.CSNetID) : (CSNet) null;
      Account account = Account.Load(notification.AccountID);
      content = notification.ReplaceVariablesPreview(msg, sensor, gateway, network, account);
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.EditPageMsgPreview ");
      content = "Failed to generate preview";
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public ActionResult EditPageRemoveNotifyTask(long id, string NotiType)
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.Content("Failed: Unauthorized");
    Notification DBObject = Notification.Load(id);
    if (DBObject == null || !MonnitSession.CurrentCustomer.CanSeeAccount(DBObject.AccountID))
      return (ActionResult) this.Content("Failed: No Rule Found");
    eNotificationType notificationType = (eNotificationType) NotiType.ToInt();
    try
    {
      List<NotificationRecipient> notificationRecipientList = new List<NotificationRecipient>();
      string str1 = "";
      List<NotificationRecipient> list;
      switch (notificationType)
      {
        case eNotificationType.SMS:
          list = DBObject.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.SMS)).ToList<NotificationRecipient>();
          DBObject.SMSText = "";
          break;
        case eNotificationType.Phone:
          list = DBObject.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.Phone)).ToList<NotificationRecipient>();
          DBObject.VoiceText = "";
          break;
        case eNotificationType.Push_Message:
          list = DBObject.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.Push_Message)).ToList<NotificationRecipient>();
          DBObject.PushMsgText = "";
          break;
        default:
          list = DBObject.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.Group || m.NotificationType == eNotificationType.Email)).ToList<NotificationRecipient>();
          DBObject.NotificationText = "";
          DBObject.Subject = "";
          break;
      }
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Edited Rule settings");
      DBObject.Save();
      foreach (NotificationRecipient notificationRecipient in list)
      {
        try
        {
          string changeRecord = $"{{\"recipientID\": \"{notificationRecipient.NotificationRecipientID}\", \"NotifcationID\" : \"{DBObject.NotificationID}\" }} ";
          DBObject.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, DBObject.AccountID, "Removed recipient from notification");
          DBObject.NotificationRecipients.Remove(notificationRecipient);
          notificationRecipient.Delete();
        }
        catch
        {
          string str2 = "Failed: Could Not Empty Recipient List";
          str1 = string.IsNullOrWhiteSpace(str1) ? str2 : $"{str1}|{str2}";
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log($"RuleController.EditPageRemoveNotifyTask | NotificationID = {id}, NotiType = {NotiType}");
      return (ActionResult) this.Content("Failed: Could not remove Task");
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult ChooseTaskToEdit(long id)
  {
    Notification model = Notification.Load(id);
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit") || model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID))
        return this.PermissionError(methodName: nameof (ChooseTaskToEdit), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 3997);
      NotificationRecipientDevice notificationRecipientDevice = NotificationRecipientDevice.LoadByAccountID(model.AccountID);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__78.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__78.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, NotificationRecipientDevice, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "RecipientDevices", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RuleController.\u003C\u003Eo__78.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__78.\u003C\u003Ep__0, this.ViewBag, notificationRecipientDevice);
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.ChooseTaskToEdit ");
    }
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult EditCommandControlUnit(long id)
  {
    Notification notification = Notification.Load(id);
    ErrorModel errorModel = new ErrorModel();
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (EditCommandControlUnit), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 4028);
      }
      if (notification == null || !MonnitSession.IsAuthorizedForAccount(notification.AccountID))
        return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("You do not have permission to access this page."));
      List<Sensor> controlUnitList = NotificationRecipientDevice.LoadByAccountID(notification.AccountID).ControlUnitList;
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__79.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__79.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationID", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = RuleController.\u003C\u003Eo__79.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__79.\u003C\u003Ep__0, this.ViewBag, notification.NotificationID);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__79.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__79.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Notification, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Notification", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = RuleController.\u003C\u003Eo__79.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__79.\u003C\u003Ep__1, this.ViewBag, notification);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__79.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__79.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = RuleController.\u003C\u003Eo__79.\u003C\u003Ep__2.Target((CallSite) RuleController.\u003C\u003Eo__79.\u003C\u003Ep__2, this.ViewBag, notification.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (r => r.NotificationType == eNotificationType.Control)).ToList<NotificationRecipient>());
      return (ActionResult) this.View((object) controlUnitList);
    }
    catch
    {
    }
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("Edit Command Control Unit Failed to Load"));
  }

  [AuthorizeDefault]
  public ActionResult EditControlUnitSettings(long id, FormCollection collection)
  {
    Notification notification = Notification.Load(id);
    ErrorModel errorModel = new ErrorModel();
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (EditControlUnitSettings), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 4057);
      }
      if (notification == null || !MonnitSession.IsAuthorizedForAccount(notification.AccountID))
        return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("You do not have permission to access this page."));
      List<NotificationRecipient> list = notification.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.Control)).ToList<NotificationRecipient>();
      for (int index = 0; index < list.Count; ++index)
      {
        NotificationRecipient notificationRecipient1 = list[index];
        try
        {
          int num1 = 0;
          int num2 = 0;
          int state1_1 = 0;
          int num3 = 0;
          int num4 = 0;
          int state2_1 = 0;
          int state1_2;
          int state2_2;
          Control_1.ParseSerializedRecipientProperties(notificationRecipient1.SerializedRecipientProperties, out state1_2, out state2_2, out ushort _, out ushort _);
          long deviceToNotifyId;
          if (state1_2 > 0)
          {
            FormCollection formCollection1 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name1 = "relay1Toggle_" + deviceToNotifyId.ToString();
            state1_1 = formCollection1[name1].ToBool() ? 2 : 1;
            FormCollection formCollection2 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name2 = "relay1Minute_" + deviceToNotifyId.ToString();
            num1 = formCollection2[name2].ToInt();
            FormCollection formCollection3 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name3 = "relay1Second_" + deviceToNotifyId.ToString();
            num2 = formCollection3[name3].ToInt();
            NotificationRecipient notificationRecipient2 = notificationRecipient1;
            FormCollection formCollection4 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name4 = "relay1delayMinutes_" + deviceToNotifyId.ToString();
            int num5 = formCollection4[name4].ToInt();
            notificationRecipient2.DelayMinutes = num5;
          }
          else if (state2_2 > 0)
          {
            FormCollection formCollection5 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name5 = "relay2Toggle_" + deviceToNotifyId.ToString();
            state2_1 = formCollection5[name5].ToBool() ? 2 : 1;
            FormCollection formCollection6 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name6 = "relay2Minute_" + deviceToNotifyId.ToString();
            num3 = formCollection6[name6].ToInt();
            FormCollection formCollection7 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name7 = "relay2Second_" + deviceToNotifyId.ToString();
            num4 = formCollection7[name7].ToInt();
            NotificationRecipient notificationRecipient3 = notificationRecipient1;
            FormCollection formCollection8 = collection;
            deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
            string name8 = "relay2delayMinutes_" + deviceToNotifyId.ToString();
            int num6 = formCollection8[name8].ToInt();
            notificationRecipient3.DelayMinutes = num6;
          }
          notificationRecipient1.SerializedRecipientProperties = Control_1.CreateSerializedRecipientProperties(state1_1, state2_1, num1 * 60 + num2, num3 * 60 + num4);
          notificationRecipient1.Save();
        }
        catch (Exception ex)
        {
          ex.Log("RuleController.EditControlUnitSettings Failed editing Control unit Settings DeviceID: " + notificationRecipient1.DeviceToNotifyID.ToString());
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.EditControlUnitSettings ");
      return (ActionResult) this.Content(ex.Message);
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ToggleControlUnitDeviceEdit(
    long id,
    long deviceID,
    string deviceType,
    bool add)
  {
    try
    {
      Notification notification = Notification.Load(id);
      if (notification == null)
        return (ActionResult) this.Content("Failed: Notification not found");
      Sensor sensor = Sensor.Load(deviceID);
      if (sensor == null)
        return (ActionResult) this.Content("Failed: Device not found");
      if (notification.AccountID != sensor.AccountID)
        return (ActionResult) this.Content("Failed: Device not found");
      if (add)
      {
        string changeRecord = string.Format("{{\"SensorID\": \"{0}\",  ControlUnit : {{ \"State1\" : \"{2}\", \"State2\" : \"{3}\", \"Relay1Time\" : \"{4}\", \"Relay2Time\" : \"{5}\" }}  , \"NotifcationID\" : \"{1}\" }} ", (object) sensor.SensorID, (object) notification.NotificationID, (object) 1, (object) 0, (object) 0, (object) 0);
        notification.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, notification.AccountID, "Assigned control  unit 1 to notification");
        switch (deviceType)
        {
          case "Control1":
            notification.AddSensorRecipient(sensor, Control_1.CreateSerializedRecipientProperties(1, 0, 0, 0)).Save();
            notification.HasControlAction = true;
            break;
          case "Control2":
            notification.AddSensorRecipient(sensor, Control_1.CreateSerializedRecipientProperties(0, 1, 0, 0)).Save();
            notification.HasControlAction = true;
            break;
        }
        notification.Save();
      }
      else
      {
        NotificationRecipient notificationRecipient = NotificationControllerBase.FindNotificationRecipient(deviceID, deviceType, notification);
        if (notificationRecipient == null)
          return (ActionResult) this.Content("Not found");
        string changeRecord = $"{{\"NotificationID\" : \"{notification.NotificationID}\", \"DeviceID\": \"{deviceID}\", \"DeviceType\" : \"{deviceType}\", \"Add\" : \"{(ValueType) false}\" }} ";
        notification.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, notification.AccountID, "Removed device from notification");
        notification.RemoveRecipientDevice(notificationRecipient);
        if (notification.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.Control)).Count<NotificationRecipient>() == 0)
          notification.HasControlAction = false;
        notification.Save();
      }
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__81.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__81.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RuleController.\u003C\u003Eo__81.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__81.\u003C\u003Ep__0, this.ViewBag, notification.NotificationRecipients);
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log("ToggleControlUnitDeviceEdit Failed");
      return (ActionResult) this.Content("Unable to add device");
    }
  }

  [AuthorizeDefault]
  public ActionResult EditCommandThermostat(long id)
  {
    Notification notification = Notification.Load(id);
    ErrorModel errorModel = new ErrorModel();
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (EditCommandThermostat), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 4208);
      }
      if (notification == null || !MonnitSession.IsAuthorizedForAccount(notification.AccountID))
        return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("You do not have permission to access this page."));
      List<Sensor> thermostatList = NotificationRecipientDevice.LoadByAccountID(notification.AccountID).ThermostatList;
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__82.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__82.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationID", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = RuleController.\u003C\u003Eo__82.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__82.\u003C\u003Ep__0, this.ViewBag, notification.NotificationID);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__82.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__82.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Notification, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Notification", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = RuleController.\u003C\u003Eo__82.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__82.\u003C\u003Ep__1, this.ViewBag, notification);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__82.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__82.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = RuleController.\u003C\u003Eo__82.\u003C\u003Ep__2.Target((CallSite) RuleController.\u003C\u003Eo__82.\u003C\u003Ep__2, this.ViewBag, notification.NotificationRecipients);
      return (ActionResult) this.View((object) thermostatList);
    }
    catch
    {
    }
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("Edit Command Thermostat Failed to Load"));
  }

  [AuthorizeDefault]
  public ActionResult EditThermostatSettings(long id, FormCollection collection)
  {
    Notification notification = Notification.Load(id);
    ErrorModel errorModel = new ErrorModel();
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (EditThermostatSettings), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 4237);
      }
      if (notification == null || !MonnitSession.IsAuthorizedForAccount(notification.AccountID))
        return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("You do not have permission to access this page."));
      List<NotificationRecipient> list = notification.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.Thermostat)).ToList<NotificationRecipient>();
      foreach (NotificationRecipient notificationRecipient in list)
      {
        Sensor sensor = Sensor.Load(notificationRecipient.DeviceToNotifyID);
        try
        {
          long deviceToNotifyId;
          if (sensor.ApplicationID == 97L)
          {
            FormCollection formCollection1 = collection;
            deviceToNotifyId = notificationRecipient.DeviceToNotifyID;
            string name1 = "occupancy_" + deviceToNotifyId.ToString();
            bool o = !string.IsNullOrEmpty(formCollection1[name1]);
            FormCollection formCollection2 = collection;
            deviceToNotifyId = notificationRecipient.DeviceToNotifyID;
            string name2 = "occupancyDuration_" + deviceToNotifyId.ToString();
            ushort uint16 = Convert.ToUInt16(formCollection2[name2]);
            notificationRecipient.SerializedRecipientProperties = Thermostat.CreateSerializedRecipientProperties(o.ToInt(), uint16);
          }
          if (sensor.ApplicationID == 125L)
          {
            FormCollection formCollection3 = collection;
            deviceToNotifyId = notificationRecipient.DeviceToNotifyID;
            string name3 = "occupancy_" + deviceToNotifyId.ToString();
            bool o = !string.IsNullOrEmpty(formCollection3[name3]);
            FormCollection formCollection4 = collection;
            deviceToNotifyId = notificationRecipient.DeviceToNotifyID;
            string name4 = "occupancyDuration_" + deviceToNotifyId.ToString();
            ushort uint16 = Convert.ToUInt16(formCollection4[name4]);
            notificationRecipient.SerializedRecipientProperties = MultiStageThermostat.CreateSerializedRecipientProperties(o.ToInt(), uint16);
          }
          notificationRecipient.Save();
        }
        catch (Exception ex)
        {
          ex.Log("RuleController.EditThermostatSettings Failed editing Thermostat Settings DeviceID: " + notificationRecipient.DeviceToNotifyID.ToString());
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.EditThermostatSettings ");
      return (ActionResult) this.Content(ex.Message);
    }
    return (ActionResult) this.Content("Success");
  }

  [Authorize]
  [HttpPost]
  public ActionResult ToggleThermostatDeviceEdit(long id, long deviceID, bool add)
  {
    try
    {
      Notification notification = Notification.Load(id);
      if (notification == null)
        return (ActionResult) this.Content("Notification not found");
      Sensor sensor = Sensor.Load(deviceID);
      if (sensor == null)
        return (ActionResult) this.Content("Device not found");
      if (notification.AccountID != sensor.AccountID)
        return (ActionResult) this.Content("Device not found");
      if (add)
      {
        string changeRecord = string.Format("{{\"SensorID\": \"{0}\",  Thermostat : {{ \"Occupancy\" : \"{2}\", \"Duration\" : \"{3}\" }}  , \"NotifcationID\" : \"{1}\" }} ", (object) deviceID, (object) notification.NotificationID, (object) 1, (object) 30);
        notification.LogAuditData(eAuditAction.Related_Assign, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, notification.AccountID, "Edited thermostat settings");
        notification.AddSensorRecipient(sensor, Thermostat.CreateSerializedRecipientProperties(1, (ushort) 30));
        notification.HasThermostatAction = true;
        notification.Save();
      }
      else
      {
        NotificationRecipient notificationRecipient = NotificationControllerBase.FindNotificationRecipient(deviceID, "Thermostat", notification);
        if (notificationRecipient == null)
          return (ActionResult) this.Content("Not found");
        string changeRecord = $"{{\"SensorID\": \"{notificationRecipient.DeviceToNotifyID}\", \"NotifcationID\" : \"{notification.NotificationID}\" }} ";
        notification.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, notification.AccountID, "Removed thermostat from rule");
        notification.RemoveRecipientDevice(notificationRecipient);
        if (notification.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.Thermostat)).Count<NotificationRecipient>() == 0)
          notification.HasThermostatAction = false;
        notification.Save();
      }
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__84.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__84.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RuleController.\u003C\u003Eo__84.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__84.\u003C\u003Ep__0, this.ViewBag, notification.NotificationRecipients);
      return (ActionResult) this.PartialView("CommandThermostatDetails", (object) sensor);
    }
    catch
    {
      return (ActionResult) this.Content("Unable to add device");
    }
  }

  [AuthorizeDefault]
  public ActionResult EditCommandLocalAlert(long id)
  {
    Notification notification = Notification.Load(id);
    ErrorModel errorModel = new ErrorModel();
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (EditCommandLocalAlert), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 4355);
      }
      if (notification == null || !MonnitSession.IsAuthorizedForAccount(notification.AccountID))
        return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("You do not have permission to access this page."));
      List<Sensor> localAlertList = NotificationRecipientDevice.LoadByAccountID(notification.AccountID).LocalAlertList;
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__85.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__85.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationID", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = RuleController.\u003C\u003Eo__85.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__85.\u003C\u003Ep__0, this.ViewBag, notification.NotificationID);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__85.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__85.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Notification, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Notification", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = RuleController.\u003C\u003Eo__85.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__85.\u003C\u003Ep__1, this.ViewBag, notification);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__85.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__85.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = RuleController.\u003C\u003Eo__85.\u003C\u003Ep__2.Target((CallSite) RuleController.\u003C\u003Eo__85.\u003C\u003Ep__2, this.ViewBag, notification.NotificationRecipients);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__85.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__85.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "LocalAlertText", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = RuleController.\u003C\u003Eo__85.\u003C\u003Ep__3.Target((CallSite) RuleController.\u003C\u003Eo__85.\u003C\u003Ep__3, this.ViewBag, string.IsNullOrEmpty(notification.LocalAlertText) ? "Local alert Message" : notification.LocalAlertText);
      return (ActionResult) this.View((object) localAlertList);
    }
    catch
    {
    }
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("Edit Command Local Alert Failed to Load"));
  }

  [AuthorizeDefault]
  public ActionResult EditLocalAlertSettings(long id, FormCollection collection)
  {
    Notification DBObject = Notification.Load(id);
    ErrorModel errorModel = new ErrorModel();
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (EditLocalAlertSettings), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 4385);
      }
      if (DBObject == null || !MonnitSession.IsAuthorizedForAccount(DBObject.AccountID))
        return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("You do not have permission to access this page."));
      if (string.IsNullOrEmpty(collection["LocalAlertText"]))
        return (ActionResult) this.Content("Local Alert Text Required");
      if (!string.IsNullOrEmpty(collection["LocalAlertText"]) && collection["LocalAlertText"].HasScriptTag())
        return (ActionResult) this.Content("Script tags not permitted.");
      collection["LocalAlertText"] = collection["LocalAlertText"].SanitizeHTMLEvent();
      DBObject.LocalAlertText = collection["LocalAlertText"];
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Edited notification");
      DBObject.Save();
      List<NotificationRecipient> list = DBObject.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.Local_Notifier)).ToList<NotificationRecipient>();
      foreach (NotificationRecipient notificationRecipient1 in list)
      {
        try
        {
          FormCollection formCollection1 = collection;
          long deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
          string name1 = "led_" + deviceToNotifyId.ToString();
          bool led = !string.IsNullOrEmpty(formCollection1[name1]);
          FormCollection formCollection2 = collection;
          deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
          string name2 = "buzzer_" + deviceToNotifyId.ToString();
          bool buzzer = !string.IsNullOrEmpty(formCollection2[name2]);
          FormCollection formCollection3 = collection;
          deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
          string name3 = "backLight_" + deviceToNotifyId.ToString();
          bool flag = !string.IsNullOrEmpty(formCollection3[name3]);
          notificationRecipient1.SerializedRecipientProperties = Attention.CreateSerializedRecipientProperties(led, buzzer, flag, flag, notificationRecipient1.DeviceToNotify.SensorName);
          NotificationRecipient notificationRecipient2 = notificationRecipient1;
          FormCollection formCollection4 = collection;
          deviceToNotifyId = notificationRecipient1.DeviceToNotifyID;
          string name4 = "delayMinutes_" + deviceToNotifyId.ToString();
          int num = formCollection4[name4].ToInt();
          notificationRecipient2.DelayMinutes = num;
          notificationRecipient1.Save();
        }
        catch (Exception ex)
        {
          ex.Log("RuleController.EditControlUnitSettings Failed editing Local Alert Settings DeviceID: " + notificationRecipient1.DeviceToNotifyID.ToString());
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("RuleController.EditLocalAlertSettings ");
      return (ActionResult) this.Content(ex.Message);
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ToggleLocalAlertDeviceEdit(long id, long deviceID, bool add)
  {
    try
    {
      Notification notification = Notification.Load(id);
      if (notification == null)
        return (ActionResult) this.Content("Notification not found");
      Sensor sensor = Sensor.Load(deviceID);
      if (sensor == null)
        return (ActionResult) this.Content("Device not found");
      if (notification.AccountID != sensor.AccountID)
        return (ActionResult) this.Content("Device not found");
      if (add)
      {
        string changeRecord = string.Format("{{\"SensorID\": \"{0}\",  LocalNotifier : {{ \"LED ON\" : \"{2}\", \"Buzzer ON\" : \"{3}\", \"AutoScroll ON\" : \"{4}\", \"BackLight ON\" : \"{5}\", \"Sensor Name\" : \"{6}\" }}  , \"NotifcationID\" : \"{1}\" }} ", (object) deviceID, (object) notification.NotificationID, (object) true, (object) true, (object) true, (object) true, (object) sensor.SensorName);
        notification.LogAuditData(eAuditAction.Related_Modify, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, notification.AccountID, "Edited local notifier settings");
        notification.AddSensorRecipient(sensor, Attention.CreateSerializedRecipientProperties(true, true, true, true, sensor.SensorName));
        notification.HasLocalAlertAction = true;
        notification.Save();
      }
      else
      {
        NotificationRecipient notificationRecipient = NotificationControllerBase.FindNotificationRecipient(deviceID, "Notifier", notification);
        if (notificationRecipient == null)
          return (ActionResult) this.Content("Not found");
        string changeRecord = $"{{\"SensorID\": \"{notificationRecipient.DeviceToNotifyID}\", \"NotifcationID\" : \"{notification.NotificationID}\" }} ";
        notification.LogAuditData(eAuditAction.Related_Remove, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, changeRecord, notification.AccountID, "Removed Local Alert from rule");
        notification.RemoveRecipientDevice(notificationRecipient);
        if (notification.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (m => m.NotificationType == eNotificationType.Thermostat)).Count<NotificationRecipient>() == 0)
          notification.HasLocalAlertAction = false;
        notification.Save();
      }
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__87.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__87.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = RuleController.\u003C\u003Eo__87.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__87.\u003C\u003Ep__0, this.ViewBag, notification.NotificationRecipients);
      return (ActionResult) this.PartialView("CommandLocalAlertDetails", (object) sensor);
    }
    catch
    {
      return (ActionResult) this.Content("Unable to add device");
    }
  }

  [AuthorizeDefault]
  public ActionResult EditSystemAction(long id)
  {
    Notification model = Notification.Load(id);
    ErrorModel errorModel = new ErrorModel();
    try
    {
      if (!MonnitSession.CustomerCan("Notification_Edit"))
      {
        RuleController.ResetNotificationInProgress();
        return this.PermissionError(methodName: nameof (EditSystemAction), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\Controllers\\RuleController.cs", sourceLineNumber: 4507);
      }
      if (model == null || !MonnitSession.IsAuthorizedForAccount(model.AccountID))
        return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("You do not have permission to access this page."));
      List<Notification> list = Notification.LoadByAccountID(model.AccountID).OrderBy<Notification, string>((Func<Notification, string>) (c => c.Name)).ToList<Notification>();
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__88.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__88.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<Notification>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotificationList", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = RuleController.\u003C\u003Eo__88.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__88.\u003C\u003Ep__0, this.ViewBag, list);
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__88.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__88.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SystemActionList", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = RuleController.\u003C\u003Eo__88.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__88.\u003C\u003Ep__1, this.ViewBag, model.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (nr => nr.NotificationType == eNotificationType.SystemAction || nr.NotificationType == eNotificationType.HTTP || nr.NotificationType == eNotificationType.ResetAccumulator)).ToList<NotificationRecipient>());
      // ISSUE: reference to a compiler-generated field
      if (RuleController.\u003C\u003Eo__88.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        RuleController.\u003C\u003Eo__88.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, List<NotificationRecipient>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "notificationRecipients", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = RuleController.\u003C\u003Eo__88.\u003C\u003Ep__2.Target((CallSite) RuleController.\u003C\u003Eo__88.\u003C\u003Ep__2, this.ViewBag, model.NotificationRecipients);
      return (ActionResult) this.View((object) model);
    }
    catch
    {
    }
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel("Edit System Action Failed to Load"));
  }

  [AuthorizeDefault]
  public ActionResult EditSystemActionList(long id)
  {
    Notification notification = Notification.Load(id);
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.Content("Not Authorized");
    return notification == null || !MonnitSession.IsAuthorizedForAccount(notification.AccountID) ? (ActionResult) this.Content("Rule not found") : (ActionResult) this.PartialView("CreateSystemActionList", (object) notification.NotificationRecipients.Where<NotificationRecipient>((Func<NotificationRecipient, bool>) (nr => nr.NotificationType == eNotificationType.SystemAction || nr.NotificationType == eNotificationType.HTTP || nr.NotificationType == eNotificationType.ResetAccumulator)).ToList<NotificationRecipient>());
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult EditPageSetSystemAction(long id, FormCollection collection)
  {
    try
    {
      Notification notification = Notification.Load(id);
      if (!MonnitSession.CustomerCan("Notification_Edit"))
        return (ActionResult) this.Content("Failed");
      if (notification == null || !MonnitSession.IsAuthorizedForAccount(notification.AccountID))
        return (ActionResult) this.Content("Failed");
      NotificationRecipient notificationRecipient = new NotificationRecipient();
      notificationRecipient.NotificationID = notification.NotificationID;
      if (collection["action"].ToLong() == -1L)
      {
        notificationRecipient.NotificationType = eNotificationType.HTTP;
        notificationRecipient.SerializedRecipientProperties = collection["NotificationWebhookID"];
        notificationRecipient.DelayMinutes = collection["DelayMinutes"].ToInt();
      }
      else if (collection["action"].ToLong() == -2L)
      {
        notificationRecipient = notification.AddSensorRecipient(Sensor.Load(collection["TargetAccumulatorID"].ToLong()), "");
        notificationRecipient.NotificationType = eNotificationType.ResetAccumulator;
        notificationRecipient.DelayMinutes = collection["DelayMinutes"].ToInt();
      }
      else
      {
        notificationRecipient.NotificationType = eNotificationType.SystemAction;
        notificationRecipient.SerializedRecipientProperties = collection["TargetRule"];
        notificationRecipient.DelayMinutes = collection["DelayMinutes"].ToInt();
        notificationRecipient.ActionToExecuteID = collection["action"].ToLong();
        if (notificationRecipient.SerializedRecipientProperties == "-1")
          notificationRecipient.SerializedRecipientProperties = notification.NotificationID.ToString();
      }
      notificationRecipient.Save();
      notification.NotificationRecipients.Add(notificationRecipient);
      return (ActionResult) this.Content("Success|" + (notification.NotificationRecipients.Count - 1).ToString());
    }
    catch (Exception ex)
    {
      ex.Log("Failed to create system action");
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult EditPageSystemActionDelete(
    long id,
    eNotificationType notificationType,
    string properties,
    int delay,
    long executeID)
  {
    Notification notification = Notification.Load(id);
    string content = string.Empty;
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.Content("Failed");
    if (notification == null || !MonnitSession.IsAuthorizedForAccount(notification.AccountID))
      return (ActionResult) this.Content("Failed");
    try
    {
      List<NotificationRecipient> notificationRecipients = notification.NotificationRecipients;
      for (int index = notificationRecipients.Count - 1; index >= 0; --index)
      {
        try
        {
          NotificationRecipient notificationRecipient = notification.NotificationRecipients[index];
          if (notificationRecipient.NotificationType == notificationType && notificationRecipient.SerializedRecipientProperties == properties && notificationRecipient.DelayMinutes == delay && notificationRecipient.ActionToExecuteID == executeID)
          {
            notificationRecipients[index].Delete();
            notificationRecipients.RemoveAt(index);
            break;
          }
        }
        catch
        {
          content = "Failed: Could Not Delete System Action ";
        }
      }
      return !string.IsNullOrEmpty(content) ? (ActionResult) this.Content(content) : (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Unable to remove");
    }
  }

  [AuthorizeDefault]
  public ActionResult History(long id, bool? showTriggered)
  {
    if (!showTriggered.HasValue)
      showTriggered = new bool?(false);
    Notification model = Notification.Load(id);
    if (!MonnitSession.CustomerCan("Sensor_View_Notifications") || !MonnitSession.CurrentCustomer.CanSeeAccount(model.AccountID) || !MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Event History",
        ErrorTranslateTag = "Event/Index|",
        ErrorTitle = "Unauthorized access to Rule History",
        ErrorMessage = "You do not have permission to access this page."
      });
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__92.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__92.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, bool?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (showTriggered), typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = RuleController.\u003C\u003Eo__92.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__92.\u003C\u003Ep__0, this.ViewBag, showTriggered);
    return (ActionResult) this.View((object) model);
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult RuleHistoryRefresh(long id, bool showTriggered)
  {
    Notification notification = Notification.Load(id);
    if (notification == null || !MonnitSession.IsAuthorizedForAccount(notification.AccountID))
      return (ActionResult) this.Content("Failed: Notification not found.");
    DateTime utcFromLocalById1 = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime utcFromLocalById2 = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    Dictionary<long, NotificationHistory> dictionary = new Dictionary<long, NotificationHistory>();
    return (ActionResult) this.PartialView("RuleHistoryList", !showTriggered ? (object) NotificationHistory.LoadByNotificationIDandDateRange(notification.NotificationID, utcFromLocalById1, utcFromLocalById2, 15) : (object) NotificationHistory.LoadOngoingByNotificationID(notification.NotificationID, DateTime.UtcNow.AddYears(-1), DateTime.UtcNow.AddMinutes(60.0), 15));
  }

  [AuthorizeDefault]
  public ActionResult Delete(long id)
  {
    Notification DBObject = Notification.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(DBObject.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Events"
      });
    try
    {
      if (MonnitSession.IsNotificationFavorite(id))
      {
        CustomerFavorite customerFavorite = MonnitSession.CurrentCustomerFavorites.AllFavorites.Find((Predicate<CustomerFavorite>) (x => x.NotificationID == id));
        MonnitSession.CurrentCustomerFavorites.Invalidate();
        customerFavorite.Delete();
        DBObject.Delete();
        return (ActionResult) this.Content("Success");
      }
      Account account = Account.Load(DBObject.AccountID);
      if (account != null)
        DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Deleted a notification");
      DBObject.Delete();
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log($"NotificationController.Delete ID: {id.ToString()} unable to delete notification  ");
    }
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Rule"
    });
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult EditApplicationTrigger(long id, FormCollection coll)
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    Notification notification = Notification.Load(id);
    if (notification == null)
      return (ActionResult) this.PartialView("Unauthorized");
    this.UpdateModel<Notification>(notification, (IValueProvider) coll);
    try
    {
      if (!MonnitApplicationBase.VerifyNotificationValues(notification, notification.Scale))
        DataTypeBase.VerifyNotificationValues(notification, notification.Scale);
    }
    catch
    {
    }
    if (this.ModelState.IsValid)
    {
      notification.Save();
      if (MonnitSession.CurrentCustomer.Account != null)
        notification.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID, "Edited notification");
    }
    return this.NotificationSaveResult(notification.NotificationID, new bool?(false));
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult EditAdvancedTrigger(long id, FormCollection coll)
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    Notification Model = Notification.Load(id);
    if (Model == null)
      return (ActionResult) this.PartialView("Unauthorized");
    AdvancedNotification advancedNotification = AdvancedNotification.Load(Model.AdvancedNotificationID);
    if (advancedNotification == null)
      return (ActionResult) this.Content("Failed");
    this.UpdateModel<Notification>(Model, (IValueProvider) coll);
    List<AdvancedNotificationParameterValue> notificationParameterValueList = AdvancedNotificationParameterValue.LoadByNotificationID(id);
    foreach (AdvancedNotificationParameter notificationParameter in AdvancedNotificationParameter.LoadByAdvancedNotificationID(advancedNotification.AdvancedNotificationID))
    {
      AdvancedNotificationParameter p = notificationParameter;
      if (!(p.ParameterName.ToLower() == "sensorid"))
      {
        string parameterValue = coll["ParamID_" + p.AdvancedNotificationParameterID.ToString()].Split(',')[0];
        AdvancedNotificationParameterValue notificationParameterValue = notificationParameterValueList.Find((Predicate<AdvancedNotificationParameterValue>) (v => v.NotificationID == Model.NotificationID && v.AdvancedNotificationParameterID == p.AdvancedNotificationParameterID));
        if (notificationParameterValue == null)
        {
          notificationParameterValue = new AdvancedNotificationParameterValue(p.AdvancedNotificationParameterID, Model.NotificationID, parameterValue);
          notificationParameterValueList.Add(notificationParameterValue);
        }
        if (p.IsValid(notificationParameterValue, parameterValue))
          notificationParameterValue.ParameterValue = parameterValue;
        else
          this.ViewData.ModelState.AddModelError("", $"{p.ParameterName.Replace("_", " ")} is not valid.");
      }
    }
    if (this.ModelState.IsValid)
    {
      Model.Save();
      foreach (AdvancedNotificationParameterValue notificationParameterValue in notificationParameterValueList)
      {
        notificationParameterValue.NotificationID = Model.NotificationID;
        notificationParameterValue.Save();
      }
      if (MonnitSession.CurrentCustomer.Account != null)
        Model.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID, "Edited notification");
    }
    return this.NotificationSaveResult(Model.NotificationID, new bool?(false));
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult EditBatteryTrigger(long id, FormCollection coll)
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    Notification notification = Notification.Load(id);
    if (notification == null)
      return (ActionResult) this.PartialView("Unauthorized");
    this.UpdateModel<Notification>(notification, (IValueProvider) coll);
    if (this.ModelState.IsValid)
    {
      notification.Save();
      if (MonnitSession.CurrentCustomer.Account != null)
        notification.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID, "Edit notification");
    }
    return this.NotificationSaveResult(notification.NotificationID, new bool?(false));
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult EditInactivityTrigger(long id, FormCollection coll)
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    Notification notification = Notification.Load(id);
    if (notification == null)
      return (ActionResult) this.PartialView("Unauthorized");
    this.UpdateModel<Notification>(notification, (IValueProvider) coll);
    if (this.ModelState.IsValid)
    {
      notification.Save();
      if (MonnitSession.CurrentCustomer.Account != null)
        notification.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID, "Edited notification");
    }
    return this.NotificationSaveResult(notification.NotificationID, new bool?(false));
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult EditScheduledTrigger(long id, FormCollection coll)
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    Notification notification = Notification.Load(id);
    if (notification == null)
      return (ActionResult) this.PartialView("Unauthorized");
    string compareValue = notification.CompareValue;
    this.UpdateModel<Notification>(notification, (IValueProvider) coll);
    notification.NotificationByTime.ScheduledHour = coll["ScheduledHour"].ToInt() + coll["AMorPM"].ToInt();
    notification.NotificationByTime.ScheduledMinute = coll["ScheduledMinute"].ToInt();
    string str = $"{notification.NotificationByTime.ScheduledHour}:{notification.NotificationByTime.ScheduledMinute}";
    if (compareValue != str)
      notification.NotificationByTime.NextEvaluationDate = DateTime.MinValue;
    notification.CompareValue = str;
    if (this.ModelState.IsValid)
    {
      notification.NotificationByTime.Save();
      notification.NotificationByTimeID = notification.NotificationByTime.NotificationByTimeID;
      notification.Save();
      if (MonnitSession.CurrentCustomer.Account != null)
        notification.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID, "Edited notification");
    }
    return this.NotificationSaveResult(notification.NotificationID, new bool?(false));
  }

  private ActionResult NotificationSaveResult(long notificationID, bool? newEvent)
  {
    if (this.ModelState.IsValid)
      return notificationID > 0L ? (ActionResult) this.Content("Success") : (ActionResult) this.PartialView("_UnknownError");
    StringBuilder stringBuilder = new StringBuilder();
    foreach (string key in (IEnumerable<string>) this.ModelState.Keys)
    {
      foreach (ModelError error in (Collection<ModelError>) this.ModelState[key].Errors)
        stringBuilder.AppendFormat("{0}<br/>", (object) error.ErrorMessage);
    }
    return (ActionResult) this.Content(stringBuilder.ToString());
  }

  [AuthorizeDefault]
  public ActionResult Triggers(long id)
  {
    MonnitSession.SensorListFilters.Clear();
    MonnitSession.GatewayListFilters.Clear();
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    Notification model = Notification.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.PartialView("Unauthorized");
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__101.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__101.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AdvancedNoti", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = RuleController.\u003C\u003Eo__101.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__101.\u003C\u003Ep__0, this.ViewBag, (object) null);
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__101.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__101.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiID", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = RuleController.\u003C\u003Eo__101.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__101.\u003C\u003Ep__1, this.ViewBag, id);
    bool flag1 = true;
    bool flag2 = true;
    switch (model.NotificationClass)
    {
      case eNotificationClass.Application:
        flag2 = false;
        break;
      case eNotificationClass.Advanced:
        AdvancedNotification advancedNotification = AdvancedNotification.Load(model.AdvancedNotificationID);
        if (advancedNotification != null)
        {
          // ISSUE: reference to a compiler-generated field
          if (RuleController.\u003C\u003Eo__101.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            RuleController.\u003C\u003Eo__101.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, AdvancedNotification, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AdvancedNoti", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj3 = RuleController.\u003C\u003Eo__101.\u003C\u003Ep__2.Target((CallSite) RuleController.\u003C\u003Eo__101.\u003C\u003Ep__2, this.ViewBag, advancedNotification);
          flag1 = advancedNotification.HasSensorList;
          flag2 = advancedNotification.HasGatewayList;
          break;
        }
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__101.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__101.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AdvancedNoti", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj4 = RuleController.\u003C\u003Eo__101.\u003C\u003Ep__3.Target((CallSite) RuleController.\u003C\u003Eo__101.\u003C\u003Ep__3, this.ViewBag, (object) null);
        flag1 = false;
        flag2 = false;
        break;
      case eNotificationClass.Timed:
        flag2 = false;
        break;
    }
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__101.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__101.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ShowSensors", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj5 = RuleController.\u003C\u003Eo__101.\u003C\u003Ep__4.Target((CallSite) RuleController.\u003C\u003Eo__101.\u003C\u003Ep__4, this.ViewBag, flag1);
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__101.\u003C\u003Ep__5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__101.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ShowGateways", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj6 = RuleController.\u003C\u003Eo__101.\u003C\u003Ep__5.Target((CallSite) RuleController.\u003C\u003Eo__101.\u003C\u003Ep__5, this.ViewBag, flag2);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult TriggerSensorList(long id, long? networkID)
  {
    MonnitSession.SensorListFilters.CSNetID = networkID ?? long.MinValue;
    List<SensorNoficationModel> source = RuleController.TriggerSensors(id, networkID);
    List<SensorNoficationModel> model = new List<SensorNoficationModel>();
    foreach (SensorGroupSensorModel sensor in SensorControllerBase.GetSensorList(MonnitSession.CurrentCustomer.AccountID, out int _))
    {
      SensorGroupSensorModel sgsm = sensor;
      foreach (SensorNoficationModel sensorNoficationModel in source.Where<SensorNoficationModel>((Func<SensorNoficationModel, bool>) (m => m.Sensor.SensorID == sgsm.Sensor.SensorID)))
        model.Add(sensorNoficationModel);
    }
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__102.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__102.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiID", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = RuleController.\u003C\u003Eo__102.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__102.\u003C\u003Ep__0, this.ViewBag, id);
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__102.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__102.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<SensorNoficationModel>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TriggerSensors", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = RuleController.\u003C\u003Eo__102.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__102.\u003C\u003Ep__1, this.ViewBag, source);
    return (ActionResult) this.PartialView("_TriggerSensors", (object) model);
  }

  [AuthorizeDefault]
  public ActionResult TriggerGatewayList(long id, long? networkID)
  {
    MonnitSession.SensorListFilters.CSNetID = networkID ?? long.MinValue;
    List<GatewayNoficationModel> source = RuleController.TriggerGateways(id, networkID);
    List<GatewayNoficationModel> model = new List<GatewayNoficationModel>();
    int totalGateways;
    List<Gateway> gatewayList = CSNetControllerBase.GetGatewayList(out totalGateways);
    this.ViewData["GatewayTypeList"] = (object) gatewayList.Select<Gateway, GatewayType>((Func<Gateway, GatewayType>) (gl => GatewayType.Load(gl.GatewayTypeID))).Distinct<GatewayType>();
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__103.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__103.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalGateways", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = RuleController.\u003C\u003Eo__103.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__103.\u003C\u003Ep__0, this.ViewBag, totalGateways);
    foreach (Gateway gateway in gatewayList)
    {
      Gateway g = gateway;
      GatewayNoficationModel gatewayNoficationModel1 = new GatewayNoficationModel();
      GatewayNoficationModel gatewayNoficationModel2 = source.Where<GatewayNoficationModel>((Func<GatewayNoficationModel, bool>) (m => m.Gateway.GatewayID == g.GatewayID)).FirstOrDefault<GatewayNoficationModel>();
      if (gatewayNoficationModel2 != null)
        model.Add(gatewayNoficationModel2);
    }
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__103.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__103.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NotiID", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = RuleController.\u003C\u003Eo__103.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__103.\u003C\u003Ep__1, this.ViewBag, id);
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__103.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__103.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, List<GatewayNoficationModel>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TriggerGateways", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = RuleController.\u003C\u003Eo__103.\u003C\u003Ep__2.Target((CallSite) RuleController.\u003C\u003Eo__103.\u003C\u003Ep__2, this.ViewBag, source);
    return (ActionResult) this.PartialView("_TriggerGateways", (object) model);
  }

  private static List<SensorNoficationModel> TriggerSensors(long id, long? networkID)
  {
    Notification noti = Notification.Load(id);
    ConfigureSensorNotificationModel notificationModel = new ConfigureSensorNotificationModel(noti);
    AdvancedNotification advancedNotification = AdvancedNotification.Load(noti.AdvancedNotificationID);
    List<SensorNoficationModel> sensorNoficationModelList = new List<SensorNoficationModel>();
    foreach (SensorNoficationModel sensor in notificationModel.SensorList)
    {
      if (advancedNotification != null)
      {
        if (advancedNotification.CanAdd((object) sensor.Sensor))
        {
          int num1;
          if (networkID.HasValue)
          {
            long? nullable1 = networkID;
            long num2 = 0;
            if (!(nullable1.GetValueOrDefault() < num2 & nullable1.HasValue))
            {
              long csNetId = sensor.Sensor.CSNetID;
              long? nullable2 = networkID;
              long valueOrDefault = nullable2.GetValueOrDefault();
              num1 = csNetId == valueOrDefault & nullable2.HasValue ? 1 : 0;
              goto label_8;
            }
          }
          num1 = 1;
label_8:
          if (num1 != 0)
            sensorNoficationModelList.Add(sensor);
        }
      }
      else
        sensorNoficationModelList.Add(sensor);
    }
    return sensorNoficationModelList;
  }

  [AuthorizeDefault]
  public ActionResult TriggerSensors()
  {
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__105.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__105.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, ActionResult>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (ActionResult), typeof (RuleController)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, ActionResult> target1 = RuleController.\u003C\u003Eo__105.\u003C\u003Ep__2.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, ActionResult>> p2 = RuleController.\u003C\u003Eo__105.\u003C\u003Ep__2;
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__105.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__105.\u003C\u003Ep__1 = CallSite<Func<CallSite, RuleController, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "View", (IEnumerable<Type>) null, typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, RuleController, object, object> target2 = RuleController.\u003C\u003Eo__105.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, RuleController, object, object>> p1 = RuleController.\u003C\u003Eo__105.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__105.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__105.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, nameof (TriggerSensors), typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = RuleController.\u003C\u003Eo__105.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__105.\u003C\u003Ep__0, this.ViewBag);
    object obj2 = target2((CallSite) p1, this, obj1);
    return target1((CallSite) p2, obj2);
  }

  private static List<GatewayNoficationModel> TriggerGateways(long id, long? networkID)
  {
    List<GatewayNoficationModel> gatewayNoficationModelList = new List<GatewayNoficationModel>();
    foreach (GatewayNoficationModel gateway in new ConfigureSensorNotificationModel(Notification.Load(id)).GatewayList)
    {
      int num1;
      if (networkID.HasValue)
      {
        long? nullable = networkID;
        long num2 = -1;
        if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
        {
          nullable = networkID;
          long num3 = 0;
          if (nullable.GetValueOrDefault() > num3 & nullable.HasValue)
          {
            long csNetId = gateway.Gateway.CSNetID;
            nullable = networkID;
            long valueOrDefault = nullable.GetValueOrDefault();
            num1 = csNetId == valueOrDefault & nullable.HasValue ? 1 : 0;
            goto label_8;
          }
          num1 = 0;
          goto label_8;
        }
      }
      num1 = 1;
label_8:
      if (num1 != 0)
        gatewayNoficationModelList.Add(gateway);
    }
    return gatewayNoficationModelList;
  }

  [AuthorizeDefault]
  public ActionResult RuleEdit(long id)
  {
    if (!MonnitSession.CustomerCan("Notification_Edit"))
      return (ActionResult) this.PartialView("Unauthorized");
    Notification model = Notification.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID))
      return (ActionResult) this.PartialView("Unauthorized");
    // ISSUE: reference to a compiler-generated field
    if (RuleController.\u003C\u003Eo__107.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      RuleController.\u003C\u003Eo__107.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Notification, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Notification", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = RuleController.\u003C\u003Eo__107.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__107.\u003C\u003Ep__0, this.ViewBag, model);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult RuleEdit(long id, FormCollection collection)
  {
    string content;
    try
    {
      Notification DBObject = Notification.Load(id);
      if (DBObject == null || DBObject != null && !MonnitSession.IsAuthorizedForAccount(DBObject.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
        return (ActionResult) this.Content("Failed");
      if (string.IsNullOrEmpty(collection["name"]))
        return (ActionResult) this.Content("Name:  Field can't be left empty.");
      int num = collection["SnoozeDuration"].ToInt();
      if (num < 0 || num > 720)
        return (ActionResult) this.Content("Invalid Snooze Duration");
      collection["name"] = collection["name"].SanitizeHTMLEvent();
      DBObject.Name = collection["name"];
      DBObject.SnoozeDuration = num;
      DBObject.CanAutoAcknowledge = !string.IsNullOrEmpty(collection["_CanAutoAcknowledge"]);
      DBObject.ApplySnoozeByTriggerDevice = !string.IsNullOrEmpty(collection["_ApplySnoozeByTriggerDevice"]);
      if (this.ModelState.IsValid)
      {
        Account account = Account.Load(DBObject.AccountID);
        if (account != null)
          DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited notification settings");
        DBObject.Save();
        content = "Success";
      }
      else
        content = "Failed";
    }
    catch (Exception ex)
    {
      content = "Failed";
    }
    return (ActionResult) this.Content(content);
  }

  [AuthorizeDefault]
  public ActionResult Calendar(long id)
  {
    NotificationScheduleDisableModel model = new NotificationScheduleDisableModel()
    {
      notification = Notification.Load(id),
      notificationScheduleDisabledList = NotificationScheduleDisabled.LoadByNotificationID(id)
    };
    model.dic = NotificationScheduleDisabled.fillMonthDayDic(model.notificationScheduleDisabledList);
    this.ViewData["ShowAdvancedScheduleSchedule"] = model.notificationScheduleDisabledList.Count > 0 ? (object) "false" : (object) "true";
    RuleController.AddTimeDropDowns(model.notification, this.ViewData);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateInput(false)]
  public ActionResult Calendar(
    FormCollection collection,
    string[] Jan,
    string[] Feb,
    string[] Mar,
    string[] Apr,
    string[] May,
    string[] June,
    string[] July,
    string[] Aug,
    string[] Sept,
    string[] Oct,
    string[] Nov,
    string[] Dec)
  {
    try
    {
      long notificationID = collection["id"].ToLong();
      if (notificationID > 0L)
      {
        NotificationScheduleDisabled.DeleteByNotificationID(notificationID);
        if (Jan != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Jan), 1);
        if (Feb != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Feb), 2);
        if (Mar != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Mar), 3);
        if (Apr != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Apr), 4);
        if (May != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(May), 5);
        if (June != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(June), 6);
        if (July != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(July), 7);
        if (Aug != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Aug), 8);
        if (Sept != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Sept), 9);
        if (Oct != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Oct), 10);
        if (Nov != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Nov), 11);
        if (Dec != null)
          this.AddNotificationScheduleDisabled(notificationID, this.DayList(Dec), 12);
        NotificationScheduleDisableModel scheduleDisableModel = new NotificationScheduleDisableModel()
        {
          notification = Notification.Load(collection["id"].ToLong())
        };
        scheduleDisableModel.notificationScheduleDisabledList = NotificationScheduleDisabled.LoadByNotificationID(scheduleDisableModel.notification.NotificationID);
        scheduleDisableModel.dic = NotificationScheduleDisabled.fillMonthDayDic(scheduleDisableModel.notificationScheduleDisabledList);
        this.ViewData["ShowAdvancedScheduleSchedule"] = (object) (((IEnumerable<string>) collection.AllKeys).Contains<string>("ShowAdvancedScheduleSchedule") && collection["ShowAdvancedScheduleSchedule"].ToLower() == "true,false");
        scheduleDisableModel.notification.AlwaysSend = ((IEnumerable<string>) collection.AllKeys).Contains<string>("AlwaysSend") && collection["AlwaysSend"].ToLower().Contains("true");
        NotificationController.SetActivePeriod(scheduleDisableModel.notification, collection, this.ModelState);
        RuleController.AddTimeDropDowns(scheduleDisableModel.notification, this.ViewData);
        scheduleDisableModel.notification.MondaySchedule.Save();
        scheduleDisableModel.notification.TuesdaySchedule.Save();
        scheduleDisableModel.notification.WednesdaySchedule.Save();
        scheduleDisableModel.notification.ThursdaySchedule.Save();
        scheduleDisableModel.notification.FridaySchedule.Save();
        scheduleDisableModel.notification.SaturdaySchedule.Save();
        scheduleDisableModel.notification.SundaySchedule.Save();
        scheduleDisableModel.notification.Save();
        string str = "/Rule/Calendar/" + scheduleDisableModel.notification.NotificationID.ToString();
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__110.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__110.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = RuleController.\u003C\u003Eo__110.\u003C\u003Ep__0.Target((CallSite) RuleController.\u003C\u003Eo__110.\u003C\u003Ep__0, this.ViewBag, "Event Schedule Edit Success");
        // ISSUE: reference to a compiler-generated field
        if (RuleController.\u003C\u003Eo__110.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          RuleController.\u003C\u003Eo__110.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (RuleController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = RuleController.\u003C\u003Eo__110.\u003C\u003Ep__1.Target((CallSite) RuleController.\u003C\u003Eo__110.\u003C\u003Ep__1, this.ViewBag, str);
        return (ActionResult) this.Content("Event Schedule Edit Success");
      }
    }
    catch (Exception ex)
    {
    }
    return (ActionResult) this.Content("Failed");
  }

  public static void AddTimeDropDowns(Notification notification, ViewDataDictionary viewData)
  {
    viewData["ShowSchedule"] = (object) true;
    viewData["ShowTimeOfDay"] = (object) true;
    foreach (int day in Enum.GetValues(typeof (DayOfWeek)))
    {
      try
      {
        NotificationSchedule notificationSchedule = notification.GetNotificationSchedule((DayOfWeek) day);
        viewData[day.ToString() + "Schedule.NotificationDaySchedule"] = (object) notificationSchedule.NotificationDaySchedule.ToString();
        TimeSpan timeSpan1 = notificationSchedule.FirstTime != TimeSpan.MinValue ? notificationSchedule.FirstTime : new TimeSpan(0L);
        TimeSpan timeSpan2 = notificationSchedule.SecondTime != TimeSpan.MinValue ? notificationSchedule.SecondTime : new TimeSpan(0L);
        string[] items = new string[12]
        {
          "1",
          "2",
          "3",
          "4",
          "5",
          "6",
          "7",
          "8",
          "9",
          "10",
          "11",
          "12"
        };
        viewData[((DayOfWeek) day).ToString() + "StartHours"] = (object) new SelectList((IEnumerable) items, (object) (timeSpan1.Hours > 12 ? timeSpan1.Hours - 12 : (timeSpan1.Hours == 0 ? 12 : timeSpan1.Hours)).ToString());
        viewData[((DayOfWeek) day).ToString() + "StartMinutes"] = (object) new SelectList((IEnumerable) new string[4]
        {
          "00",
          "15",
          "30",
          "45"
        }, (object) timeSpan1.Minutes.ToString());
        viewData[((DayOfWeek) day).ToString() + "StartAM"] = (object) new SelectList((IEnumerable) new string[2]
        {
          "AM",
          "PM"
        }, timeSpan1.Hours < 12 ? (object) "AM" : (object) "PM");
        ViewDataDictionary viewDataDictionary1 = viewData;
        DayOfWeek dayOfWeek = (DayOfWeek) day;
        string key1 = dayOfWeek.ToString() + "EndHours";
        SelectList selectList1 = new SelectList((IEnumerable) items, (object) (timeSpan2.Hours > 12 ? timeSpan2.Hours - 12 : (timeSpan2.Hours == 0 ? 12 : timeSpan2.Hours)).ToString());
        viewDataDictionary1[key1] = (object) selectList1;
        ViewDataDictionary viewDataDictionary2 = viewData;
        dayOfWeek = (DayOfWeek) day;
        string key2 = dayOfWeek.ToString() + "EndMinutes";
        SelectList selectList2 = new SelectList((IEnumerable) new string[4]
        {
          "00",
          "15",
          "30",
          "45"
        }, (object) timeSpan2.Minutes.ToString());
        viewDataDictionary2[key2] = (object) selectList2;
        ViewDataDictionary viewDataDictionary3 = viewData;
        dayOfWeek = (DayOfWeek) day;
        string key3 = dayOfWeek.ToString() + "EndAM";
        SelectList selectList3 = new SelectList((IEnumerable) new string[2]
        {
          "AM",
          "PM"
        }, timeSpan2.Hours < 12 ? (object) "AM" : (object) "PM");
        viewDataDictionary3[key3] = (object) selectList3;
      }
      catch (Exception ex)
      {
        ExceptionLog.Log(ex);
      }
    }
  }

  private List<int> DayList(string[] days)
  {
    List<int> source = new List<int>();
    for (int index = 0; index < days.Length; ++index)
    {
      if (source.Count > 0 && days[index].ToInt() - 1 != source.Last<int>())
        source.Add(-1);
      source.Add(days[index].ToInt());
      if (days.Length - 1 == index)
        source.Add(-1);
    }
    return source;
  }

  private void AddNotificationScheduleDisabled(long notificationID, List<int> monthList, int month)
  {
    int index1 = 0;
    bool flag1 = true;
    bool flag2 = false;
    NotificationScheduleDisabled scheduleDisabled = new NotificationScheduleDisabled();
    for (int index2 = 0; index2 < monthList.Count; ++index2)
    {
      if (monthList[index2] != -1)
        index1 = index2;
      else
        flag2 = true;
      if (flag1)
      {
        scheduleDisabled.NotificationID = notificationID;
        scheduleDisabled.StartMonth = month;
        scheduleDisabled.StartDay = monthList[index2];
        flag1 = false;
      }
      if (flag2)
      {
        scheduleDisabled.EndMonth = month;
        scheduleDisabled.EndDay = monthList[index1];
        scheduleDisabled.Save();
        scheduleDisabled = new NotificationScheduleDisabled();
        flag1 = true;
        flag2 = false;
      }
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateInput(false)]
  public ActionResult RuleNameEdit(long id, int? scaleID)
  {
    try
    {
      Notification notification = Notification.Load(id);
      if (notification == null || notification != null && !MonnitSession.IsAuthorizedForAccount(notification.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
        return (ActionResult) this.Content("Failed");
      Account account = Account.Load(notification.AccountID);
      if (account != null)
        notification.LogAuditData(eAuditAction.Update, eAuditObject.Notification, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited notification settings");
      if (scaleID.HasValue)
        notification.ScaleID = scaleID.GetValueOrDefault();
      this.UpdateModel<Notification>(notification);
      notification.Save();
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult RulePrimarySensorEdit()
  {
    try
    {
      long ID1 = long.Parse(this.Request.Form["sensorId"]);
      long ID2 = long.Parse(this.Request.Form["ruleId"]);
      long num = long.Parse(this.Request.Form["datumIndex"]);
      Sensor.Load(ID1);
      Notification notification = Notification.Load(ID2);
      if (notification == null || notification != null && !MonnitSession.IsAuthorizedForAccount(notification.AccountID) || !MonnitSession.CustomerCan("Notification_Edit"))
        return (ActionResult) this.Content("Sorry! User permissions won't allow this change.");
      List<UnitConversion> scales = MonnitApplicationBase.GetScales(Sensor.Load(ID1), notification.eDatumType);
      List<UnitConversion> list = scales.Where<UnitConversion>((Func<UnitConversion, bool>) (c => c.UnitLabel == "Custom")).ToList<UnitConversion>();
      if (list.Count > 1)
      {
        if (num == 1L)
          scales.Remove(list.Last<UnitConversion>());
        else
          scales.Remove(list.First<UnitConversion>());
      }
      notification.SensorID = ID1;
      notification.Save();
      return (ActionResult) this.Json((object) scales);
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
      return (ActionResult) this.Content("Sorry! Unable to set as primary Sensor for this Rule.");
    }
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult GetStringForRuleCard(long id)
  {
    return (ActionResult) this.PartialView("_RuleConditionAsSentenceMain", (object) Notification.Load(id));
  }
}
