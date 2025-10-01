// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.ChartController
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
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

public class ChartController : ThemeController
{
  [NoCache]
  [AuthorizeDefault]
  public ActionResult ChartIndex(long id)
  {
    return (ActionResult) this.View((object) SensorGroup.LoadByAccountIDandType(id, "chart"));
  }

  [AuthorizeDefault]
  public ActionResult ChartEdit(long? id, long? csnetID)
  {
    if (MonnitSession.HistoryToDate.Subtract(MonnitSession.HistoryFromDate).TotalDays > 7.0)
      MonnitSession.HistoryToDate = MonnitSession.HistoryFromDate.AddDays(7.0);
    SensorGroup model = SensorGroup.Load(id ?? long.MinValue);
    if (model == null)
    {
      model = (SensorGroup) this.Session["chartSensorList"];
      if (model != null && model.AccountID != MonnitSession.CurrentCustomer.AccountID)
      {
        model = (SensorGroup) null;
        this.Session["chartSensorList"] = (object) null;
      }
    }
    if (model == null)
    {
      model = new SensorGroup();
      model.Type = "chart";
      model.AccountID = MonnitSession.CurrentCustomer.AccountID;
    }
    if (model.AccountID > 0L && !MonnitSession.CurrentCustomer.CanSeeAccount(model.AccountID) || !MonnitSession.AccountCan("view_multichart"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = nameof (ChartEdit),
        ErrorTranslateTag = "Chart/ChartIndex|",
        ErrorTitle = "Unauthorized access to chart",
        ErrorMessage = "You do not have permission to access this page."
      });
    List<CSNet> networksUserCanSee = CSNetControllerBase.GetListOfNetworksUserCanSee(new long?(MonnitSession.CurrentCustomer.AccountID));
    if (csnetID.HasValue)
    {
      if (csnetID.ToLong() != -1L && networksUserCanSee.Find((Predicate<CSNet>) (n =>
      {
        long csNetId = n.CSNetID;
        long? nullable = csnetID;
        long valueOrDefault = nullable.GetValueOrDefault();
        return csNetId == valueOrDefault & nullable.HasValue;
      })) == null)
      {
        if (MonnitSession.SensorListFilters.CSNetID < 0L)
        {
          if (networksUserCanSee.Count > 0)
            csnetID = new long?(networksUserCanSee[0].CSNetID);
        }
        else if (networksUserCanSee.Find((Predicate<CSNet>) (n => n.CSNetID == MonnitSession.SensorListFilters.CSNetID)) != null)
          csnetID = new long?(MonnitSession.SensorListFilters.CSNetID);
        else if (networksUserCanSee.Count > 0)
          csnetID = new long?(networksUserCanSee[0].CSNetID);
      }
    }
    else if (MonnitSession.SensorListFilters.CSNetID < 0L)
    {
      if (networksUserCanSee.Count > 0)
        csnetID = new long?(networksUserCanSee[0].CSNetID);
    }
    else if (networksUserCanSee.Find((Predicate<CSNet>) (n => n.CSNetID == MonnitSession.SensorListFilters.CSNetID)) != null)
      csnetID = new long?(MonnitSession.SensorListFilters.CSNetID);
    else if (networksUserCanSee.Count > 0)
      csnetID = new long?(networksUserCanSee[0].CSNetID);
    long? nullable1 = csnetID;
    long num = 0;
    if (nullable1.GetValueOrDefault() > num & nullable1.HasValue)
    {
      CSNet csNet = networksUserCanSee.Find((Predicate<CSNet>) (n =>
      {
        long csNetId = n.CSNetID;
        long? nullable2 = csnetID;
        long valueOrDefault = nullable2.GetValueOrDefault();
        return csNetId == valueOrDefault & nullable2.HasValue;
      }));
      MonnitSession.SensorListFilters.CSNetID = csNet == null || csNet.AccountID != MonnitSession.CurrentCustomer.AccountID ? long.MinValue : csnetID.ToLong();
    }
    else
      MonnitSession.SensorListFilters.CSNetID = long.MinValue;
    this.ViewData["Networks"] = (object) new SelectList((IEnumerable) networksUserCanSee, "CSNetID", "Name");
    this.ViewData["SensorList"] = (object) ChartController.GetSensorList(csnetID ?? new long?()).OrderBy<SensorDatumModel, string>((Func<SensorDatumModel, string>) (c => c.Sensor.SensorName)).ToList<SensorDatumModel>();
    this.Session["chartSensorList"] = (object) model;
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult MultiChart(long? id)
  {
    if (MonnitSession.HistoryToDate.Subtract(MonnitSession.HistoryFromDate).TotalDays > 7.0)
      MonnitSession.HistoryToDate = MonnitSession.HistoryFromDate.AddDays(7.0);
    SensorGroup sensorGroup = SensorGroup.Load(id ?? long.MinValue) ?? (SensorGroup) this.Session["chartSensorList"];
    if (sensorGroup == null || sensorGroup.Sensors.Count < 1 || sensorGroup.AccountID != MonnitSession.CurrentCustomer.AccountID)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "ChartEdit",
        controller = "Chart"
      });
    if ((sensorGroup.AccountID <= 0L || MonnitSession.CurrentCustomer.CanSeeAccount(sensorGroup.AccountID)) && MonnitSession.AccountCan("view_multichart"))
      return (ActionResult) this.View();
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
    {
      ErrorLocation = nameof (MultiChart),
      ErrorTranslateTag = "Chart/ChartIndex|",
      ErrorTitle = "Unauthorized access to chart",
      ErrorMessage = "You do not have permission to access this page."
    });
  }

  [AuthorizeDefault]
  public PartialViewResult MultiChartChart(long? id)
  {
    if (MonnitSession.HistoryToDate.Subtract(MonnitSession.HistoryFromDate).TotalDays > 7.0)
      MonnitSession.HistoryToDate = MonnitSession.HistoryFromDate.AddDays(7.0);
    DateTime utcFromLocalById1 = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime utcFromLocalById2 = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    SensorGroup sensorGroup = SensorGroup.Load(id ?? long.MinValue) ?? (SensorGroup) this.Session["chartSensorList"];
    // ISSUE: reference to a compiler-generated field
    if (ChartController.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ChartController.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "sensorCount", typeof (ChartController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = ChartController.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) ChartController.\u003C\u003Eo__3.\u003C\u003Ep__0, this.ViewBag, sensorGroup.Sensors.Count);
    List<MultiChartSensorDataModel> model = new List<MultiChartSensorDataModel>();
    foreach (SensorGroupSensor sensor in sensorGroup.Sensors)
    {
      List<DataMessage> dataMessageList = DataMessage.LoadAllForChart(sensor.SensorID, utcFromLocalById1, utcFromLocalById2);
      StringBuilder stringBuilder = new StringBuilder();
      string label = "";
      foreach (DataMessage dataMessage in dataMessageList)
      {
        stringBuilder.Append($"[new Date({Monnit.TimeZone.GetLocalTimeById(dataMessage.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime().ToString()}),{dataMessage.AppBase.GetPlotValues(sensor.SensorID)[sensor.DatumIndex]?.ToString()}],");
        if (string.IsNullOrEmpty(label))
          label = dataMessage.AppBase.GetPlotLabels(sensor.SensorID)[sensor.DatumIndex];
      }
      model.Add(new MultiChartSensorDataModel(sensor, stringBuilder.ToString(), label));
    }
    return this.PartialView("_MultiChartChart", (object) model);
  }

  [AuthorizeDefault]
  public ActionResult SensorGroupCount(long? id)
  {
    try
    {
      SensorGroup sensorGroup = SensorGroup.Load(id ?? long.MinValue) ?? (SensorGroup) this.Session["chartSensorList"];
      // ISSUE: reference to a compiler-generated field
      if (ChartController.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ChartController.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "sensorCount", typeof (ChartController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = ChartController.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) ChartController.\u003C\u003Eo__4.\u003C\u003Ep__0, this.ViewBag, sensorGroup.Sensors.Count);
      return (ActionResult) this.Content(sensorGroup.Sensors.Count.ToString());
    }
    catch
    {
    }
    return (ActionResult) this.Content("-1");
  }

  private static List<SensorDatumModel> GetSensorList(long? id)
  {
    List<Sensor> list = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where<Sensor>((Func<Sensor, bool>) (s =>
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
    SensorAttribute.CacheByAccount(MonnitSession.CurrentCustomer.AccountID);
    List<SensorDatumModel> sensorList = new List<SensorDatumModel>();
    foreach (Sensor sensor in list)
    {
      Sensor sens = sensor;
      List<eDatumStruct> source = new List<eDatumStruct>();
      int di = 0;
      foreach (eDatumType datumType in MonnitApplicationBase.GetDatumTypes(sens.ApplicationID))
      {
        source.Add(new eDatumStruct(datumType, di));
        ++di;
      }
      if (sens != null)
      {
        int j = 0;
        source = new List<eDatumStruct>(source.Select<eDatumStruct, eDatumStruct>((Func<eDatumStruct, eDatumStruct>) (edat =>
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
      }
      foreach (eDatumStruct eDatumStruct in source)
        sensorList.Add(new SensorDatumModel()
        {
          Sensor = sens,
          datumStruct = eDatumStruct
        });
    }
    return sensorList;
  }

  [AuthorizeDefault]
  [NoCache]
  public ActionResult SensorList(long? id, long? groupID, string query)
  {
    List<SensorDatumModel> sensorDatumModelList1 = new List<SensorDatumModel>();
    List<SensorDatumModel> sensorDatumModelList2 = string.IsNullOrEmpty(query) ? ChartController.GetSensorList(id).OrderBy<SensorDatumModel, string>((Func<SensorDatumModel, string>) (c => c.Sensor.SensorName)).ToList<SensorDatumModel>() : ChartController.GetSensorList(id).Where<SensorDatumModel>((Func<SensorDatumModel, bool>) (c => c.Sensor.SensorName.ToLower().Contains(query.ToLower()) || c.Sensor.SensorID.ToString().Contains(query.ToString()))).OrderBy<SensorDatumModel, string>((Func<SensorDatumModel, string>) (c => c.Sensor.SensorName)).ToList<SensorDatumModel>();
    MonnitSession.SensorListFilters.CSNetID = id ?? long.MinValue;
    this.ViewData[nameof (SensorList)] = (object) sensorDatumModelList2;
    SensorGroup sensorGroup = (SensorGroup) this.Session["chartSensorList"] ?? SensorGroup.Load(groupID ?? long.MinValue);
    if (sensorGroup == null)
    {
      sensorGroup = new SensorGroup();
      sensorGroup.Type = "chart";
      sensorGroup.AccountID = MonnitSession.CurrentCustomer.AccountID;
    }
    this.Session["chartSensorList"] = (object) sensorGroup;
    return (ActionResult) this.View(nameof (SensorList));
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult AddSensorToGroup(long id, long groupID, int datumIndex, string customName)
  {
    SensorGroup sensorGroup = SensorGroup.Load(groupID);
    if (sensorGroup == null)
    {
      sensorGroup = (SensorGroup) this.Session["chartSensorList"];
      if (sensorGroup == null)
      {
        sensorGroup = new SensorGroup();
        sensorGroup.Type = "chart";
        sensorGroup.AccountID = MonnitSession.CurrentCustomer.AccountID;
      }
    }
    sensorGroup.AddSensorByDatum(id, datumIndex, customName, false);
    this.Session["chartSensorList"] = (object) sensorGroup;
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult RemoveSensorFromGroup(long id, long groupID, int datumIndex)
  {
    SensorGroup sensorGroup = SensorGroup.Load(groupID) ?? (SensorGroup) this.Session["chartSensorList"];
    if (sensorGroup == null)
      return (ActionResult) this.Content("Failed");
    sensorGroup.RemoveSensor(id, datumIndex);
    this.Session["chartSensorList"] = (object) sensorGroup;
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ChartClearAll(long groupID)
  {
    SensorGroup sensorGroup = SensorGroup.Load(groupID) ?? (SensorGroup) this.Session["chartSensorList"];
    if (sensorGroup == null)
      return (ActionResult) this.Content("Failed");
    foreach (SensorGroupSensor sensor in sensorGroup.Sensors)
      sensorGroup.RemoveSensor(sensor.SensorID);
    this.Session["chartSensorList"] = (object) sensorGroup;
    return (ActionResult) this.Content("Success");
  }
}
