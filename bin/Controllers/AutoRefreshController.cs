// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.AutoRefreshController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.ControllerBase;
using iMonnit.Models;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;

#nullable disable
namespace iMonnit.Controllers;

[System.Web.Mvc.SessionState(SessionStateBehavior.ReadOnly)]
public class AutoRefreshController : Controller
{
  [NoCache]
  [Authorize]
  public ActionResult AtAGlanceRefresh()
  {
    return (ActionResult) this.View((object) SensorControllerBase.GetSensorList());
  }

  [NoCache]
  [AuthorizeDefault]
  public JsonResult AtATestingSensorRefresh(long id)
  {
    if (!MonnitSession.CustomerCan("Can_Access_Testing"))
      return this.Json((object) "Unauthorized");
    List<Sensor> sensorList = Sensor.LoadByCsNetID(id);
    List<RefreshTestingSensorModel> data = new List<RefreshTestingSensorModel>();
    foreach (Sensor sensor in sensorList)
      data.Add(new RefreshTestingSensorModel(sensor.SensorID)
      {
        Status = AutoRefreshController.SensorStatusTestString(sensor),
        DisplayDate = sensor.LastCommunicationDate.OVTestingElapsedLastMessageString(),
        FirmwareVersion = sensor.FirmwareVersion,
        isPaused = sensor.isPaused(),
        isDirty = !sensor.CanUpdate,
        SignalStrength = AutoRefreshController.SensorSignalStrengthTestString(sensor),
        SignalStrengthPercent = DataMessage.GetSignalStrengthPercent(sensor.GenerationType, sensor.SensorTypeID, sensor.LastDataMessage == null ? 0 : sensor.LastDataMessage.SignalStrength).ToString() + "%",
        BatteryLevel = AutoRefreshController.SensorPowerTestString(sensor),
        Reading = AutoRefreshController.SensorReadingTestString(sensor),
        ReadingTitle = AutoRefreshController.SensorReadingTitleTestString(sensor),
        GeneralConfig3DirtyColor = sensor.GeneralConfig3Dirty ? "radial-gradient(circle at 11px 4px, red, #000000b8)" : "radial-gradient(circle at 11px 4px, #11AD3D, #000000b8)",
        GeneralConfig2DirtyColor = sensor.GeneralConfig2Dirty ? "radial-gradient(circle at 11px 4px, red, #000000b8)" : "radial-gradient(circle at 11px 4px, #11AD3D, #000000b8)",
        ProfileConfigDirtyColor = sensor.ProfileConfigDirty ? "radial-gradient(circle at 11px 4px, red, #000000b8)" : "radial-gradient(circle at 11px 4px, #11AD3D, #000000b8)",
        ProfileConfig2DirtyColor = sensor.ProfileConfig2Dirty ? "radial-gradient(circle at 11px 4px, red, #000000b8)" : "radial-gradient(circle at 11px 4px, #11AD3D, #000000b8)",
        GeneralConfigDirtyColor = sensor.GeneralConfigDirty ? "radial-gradient(circle at 11px 4px, red, #000000b8)" : "radial-gradient(circle at 11px 4px, #11AD3D, #000000b8)",
        PendingActionControlCommandColor = sensor.PendingActionControlCommand ? "radial-gradient(circle at 11px 4px, red, #000000b8)" : "radial-gradient(circle at 11px 4px, #11AD3D, #000000b8)"
      });
    return this.Json((object) data, JsonRequestBehavior.AllowGet);
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult AtATestingGatewayRefresh(long id)
  {
    if (!MonnitSession.CustomerCan("Can_Access_Testing"))
      return (ActionResult) this.Content("Unauthorized");
    List<Gateway> gatewayList = Gateway.LoadByCSNetID(id);
    List<RefreshTestingGatewayModel> data = new List<RefreshTestingGatewayModel>();
    foreach (Gateway gateway in gatewayList)
    {
      if (gateway.GatewayTypeID != 10L && gateway.GatewayTypeID != 11L && gateway.GatewayTypeID != 35L && gateway.GatewayTypeID != 36L)
        data.Add(new RefreshTestingGatewayModel(gateway.GatewayID)
        {
          Status = AutoRefreshController.GatewayStatusTestString(gateway),
          DisplayDate = gateway.LastCommunicationDate.OVTestingElapsedLastMessageString(),
          FirmwareVersion = gateway.GatewayFirmwareVersion,
          DeviceCount = gateway.LastKnownDeviceCount,
          isPaused = gateway.isPaused(),
          isDirty = gateway.IsDirty,
          SignalStrength = AutoRefreshController.GatewaySignalStrengthTestString(gateway),
          PowerTest = AutoRefreshController.GatewayCurrentPowerTestString(gateway)
        });
    }
    return (ActionResult) this.Json((object) data, JsonRequestBehavior.AllowGet);
  }

  public static string SensorStatusTestString(Sensor item)
  {
    return $"<div class='sensorStatus-sidebar deviceStatus{item.Status}' title='{item.Status}'></div>";
  }

  public static string GatewayStatusTestString(Gateway item)
  {
    return $"<div class='gatewayStatus-sidebar deviceStatus{item.Status}' title='{item.Status}'></div>";
  }

  public static string SensorPowerTestString(Sensor item)
  {
    return AutoRefreshController.SensorPowerTestString(item.LastDataMessage);
  }

  public static string SensorPowerTestString(DataMessage message)
  {
    if (message == null)
      return "";
    int num = 75;
    string str1 = "<svg id=\"fail-icon\" xmlns=\"http://www.w3.org/2000/svg\"  viewBox=\"0 0 512 512\" style=\"fill:#ca0005\"><path d=\"M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM175 175c9.4-9.4 24.6-9.4 33.9 0l47 47 47-47c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9l-47 47 47 47c9.4 9.4 9.4 24.6 0 33.9s-24.6 9.4-33.9 0l-47-47-47 47c-9.4 9.4-24.6 9.4-33.9 0s-9.4-24.6 0-33.9l47-47-47-47c-9.4-9.4-9.4-24.6 0-33.9z\"/></svg>";
    string str2 = "#CA0005";
    string str3 = "FAIL";
    if (message.Battery >= num)
    {
      str1 = "<svg id=\"green-pass\" xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 512 512\" style=\"fill:green\"><path d=\"M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM369 209L241 337c-9.4 9.4-24.6 9.4-33.9 0l-64-64c-9.4-9.4-9.4-24.6 0-33.9s24.6-9.4 33.9 0l47 47L335 175c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9z\"/></svg>";
      str2 = "#004000";
      str3 = "PASS";
    }
    string str4 = $": {message.Battery.ToString()}%  |  {message.Voltage.ToString()} V ";
    return $"<span style = \"color: {str2}; font-weight: Bold;\" > {str1}{str3}{str4} </span>";
  }

  public static string SensorSignalStrengthTestString(Sensor item)
  {
    return item == null ? "" : AutoRefreshController.SensorSignalStrengthTestString(item, item.LastDataMessage);
  }

  public static string SensorSignalStrengthTestString(Sensor sensor, DataMessage message)
  {
    if (sensor == null)
      return "";
    if (message == null)
      message = sensor.LastDataMessage;
    return AutoRefreshController.SensorSignalStrengthTestString(sensor.GenerationType, sensor.RadioBand, message);
  }

  public static string SensorSignalStrengthTestString(
    string sensorGenerationType,
    string sensorRadioBand,
    DataMessage message)
  {
    if (message == null)
      return "";
    int num = -40;
    switch (sensorGenerationType)
    {
      case "Gen2":
        num = !sensorRadioBand.Contains("433") ? -35 : -40;
        break;
      case "MOWI":
        num = -650;
        break;
    }
    string str1 = "<svg id=\"fail-icon\" xmlns=\"http://www.w3.org/2000/svg\"  viewBox=\"0 0 512 512\" style=\"fill:#ca0005\"><path d=\"M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM175 175c9.4-9.4 24.6-9.4 33.9 0l47 47 47-47c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9l-47 47 47 47c9.4 9.4 9.4 24.6 0 33.9s-24.6 9.4-33.9 0l-47-47-47 47c-9.4 9.4-24.6 9.4-33.9 0s-9.4-24.6 0-33.9l47-47-47-47c-9.4-9.4-9.4-24.6 0-33.9z\"/></svg>";
    string str2 = "FAIL:";
    string str3 = "#ca0005";
    if (num <= message.SignalStrength)
    {
      str1 = "<svg id=\"green-pass\" xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 512 512\" style=\"fill:green\"><path d=\"M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM369 209L241 337c-9.4 9.4-24.6 9.4-33.9 0l-64-64c-9.4-9.4-9.4-24.6 0-33.9s24.6-9.4 33.9 0l47 47L335 175c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9z\"/></svg>";
      str2 = "PASS:";
      str3 = "#004000";
    }
    string str4 = $" [{message.SignalStrength.ToString()}/{num.ToString()}] ";
    return $"<span style=\"color:{str3}; font-weight: Bold; \">{str1}{str2}{str4}<span>";
  }

  public static string GatewaySignalStrengthTestString(Gateway item)
  {
    return item == null ? "" : AutoRefreshController.GatewaySignalStrengthTestString(item.GatewayTypeID, item.CurrentSignalStrength);
  }

  public static string GatewaySignalStrengthTestString(long gatewayTypeID, string signalStrength)
  {
    try
    {
      return AutoRefreshController.GatewaySignalStrengthTestString(gatewayTypeID, signalStrength.ToInt());
    }
    catch
    {
      return "";
    }
  }

  public static string GatewaySignalStrengthTestString(long gatewayTypeID, int signalStrength)
  {
    if (signalStrength <= 0)
      return "";
    string str1 = "<svg id=\"fail-icon\" xmlns=\"http://www.w3.org/2000/svg\"  viewBox=\"0 0 512 512\" style=\"fill:#ca0005\"><path d=\"M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM175 175c9.4-9.4 24.6-9.4 33.9 0l47 47 47-47c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9l-47 47 47 47c9.4 9.4 9.4 24.6 0 33.9s-24.6 9.4-33.9 0l-47-47-47 47c-9.4 9.4-24.6 9.4-33.9 0s-9.4-24.6 0-33.9l47-47-47-47c-9.4-9.4-9.4-24.6 0-33.9z\"/></svg>";
    string str2 = "FAIL:";
    string str3 = "#ca0005";
    if (signalStrength > 14)
    {
      str1 = "<svg id=\"green-pass\" xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 512 512\" style=\"fill:green\"><path d=\"M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM369 209L241 337c-9.4 9.4-24.6 9.4-33.9 0l-64-64c-9.4-9.4-9.4-24.6 0-33.9s24.6-9.4 33.9 0l47 47L335 175c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9z\"/></svg>";
      str2 = "PASS:";
      str3 = "#004000";
    }
    string str4 = $"{signalStrength.ToString()} ({GatewayMessage.GetSignalStrengthPercent(gatewayTypeID, signalStrength).ToString()}%)";
    return $"<span style='color:{str3}; font-weight: Bold; '>{str1}{str2}<span>" + $"<span class='ms-2' style=\"color:{str3}; font-weight: Bold; \">{str4}<span>";
  }

  public static string GatewayCurrentPowerTestString(Gateway item)
  {
    return item == null ? "" : AutoRefreshController.GatewayPowerTestString(item.CurrentPower, item.Battery);
  }

  public static string GatewayPowerTestString(GatewayMessage lastGatewayMessage)
  {
    return lastGatewayMessage == null ? "" : AutoRefreshController.GatewayPowerTestString(lastGatewayMessage.Power, lastGatewayMessage.Battery);
  }

  public static string GatewayPowerTestString(string Power, string Battery)
  {
    try
    {
      return AutoRefreshController.GatewayPowerTestString(Power.ToInt(), Battery.ToInt());
    }
    catch
    {
      return "";
    }
  }

  public static string GatewayPowerTestString(int Power, int Battery)
  {
    int num = 75;
    string str1;
    string str2;
    switch (Power)
    {
      case 0:
        str1 = "green";
        str2 = "Line Powered";
        break;
      case 1:
        str1 = "green";
        str2 = "PoE";
        break;
      case 2:
        str1 = "orangered";
        str2 = "Charge Fault";
        break;
      default:
        if ((Power & 32768 /*0x8000*/) == 32768 /*0x8000*/)
        {
          str1 = "blue";
          str2 = $"Charging ({Battery.ToString()}%) </span>";
        }
        else
        {
          str1 = "black";
          str2 = Battery.ToString() + " % ";
        }
        break;
    }
    string str3 = "<svg id=\"fail-icon\" xmlns=\"http://www.w3.org/2000/svg\"  viewBox=\"0 0 512 512\" style=\"fill:#ca0005\"><path d=\"M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM175 175c9.4-9.4 24.6-9.4 33.9 0l47 47 47-47c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9l-47 47 47 47c9.4 9.4 9.4 24.6 0 33.9s-24.6 9.4-33.9 0l-47-47-47 47c-9.4 9.4-24.6 9.4-33.9 0s-9.4-24.6 0-33.9l47-47-47-47c-9.4-9.4-9.4-24.6 0-33.9z\"/></svg>";
    string str4 = "#CA0005";
    string str5 = "FAIL";
    if (Battery >= num)
    {
      str3 = "<svg id=\"green-pass\" xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 512 512\" style=\"fill:green\"><path d=\"M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM369 209L241 337c-9.4 9.4-24.6 9.4-33.9 0l-64-64c-9.4-9.4-9.4-24.6 0-33.9s24.6-9.4 33.9 0l47 47L335 175c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9z\"/></svg>";
      str4 = "#004000";
      str5 = "PASS";
    }
    return $"<span style='color: {str4}; font-weight: Bold;'>{str3}{str5}:</span>" + $"<span class='ms-2' style='color: {str1}; font-weight: Bold;'>{str2}</span>";
  }

  public static string SensorReadingTestString(Sensor item)
  {
    return item == null || item.LastDataMessage == null ? "No Message" : item.LastDataMessage.DisplayData;
  }

  public static string SensorReadingTitleTestString(Sensor item)
  {
    return item == null || item.LastDataMessage == null ? "No Message" : item.LastDataMessage.DisplayData;
  }

  public static string SensorRadioBandTestString(Sensor item)
  {
    string str1 = "inherit";
    string str2 = "inherit";
    string radioBand = item.RadioBand;
    if (radioBand != null)
    {
      if (radioBand.Contains("433"))
      {
        str1 = "#FFFFFF";
        str2 = "#1A8019";
      }
      else if (radioBand.Contains("800"))
      {
        str1 = "#1E1E1E";
        str2 = "#FFFF00";
      }
      else if (radioBand.Contains("868"))
      {
        str1 = "#1E1E1E";
        str2 = "#FFFF00";
      }
      else if (radioBand.Contains("900"))
      {
        str1 = "#F7FCFD";
        str2 = "#3F6E9A";
      }
      else if (radioBand.Contains("920"))
      {
        str1 = "#F5F9FC";
        str2 = "#AE123A";
      }
      else if (radioBand.Contains("940"))
      {
        str1 = "#F5F9FC";
        str2 = "#AE123A";
      }
      else if (radioBand.Contains("PoE"))
      {
        str1 = "#EF7924";
        str2 = "#333333";
      }
    }
    return $"<span style=\"color: {str1}; background: {str2};  font-weight: 400; padding: 0px 3px; border-radius:3px;\"> {item.RadioBand} </span>";
  }

  public static string GatewayRadioBandTestString(Gateway item)
  {
    string str1 = "inherit";
    string str2 = "inherit";
    string radioBand = item.RadioBand;
    if (radioBand != null)
    {
      if (radioBand.Contains("900"))
      {
        str1 = "#F7FCFD";
        str2 = "#3F6E9A";
      }
      else if (radioBand.Contains("433"))
      {
        str1 = "#FFFFFF";
        str2 = "#1A8019";
      }
      else
      {
        string str3 = radioBand;
        if (str3.Contains("920") || str3.Contains("940"))
        {
          str1 = "#F5F9FC";
          str2 = "#AE123A";
        }
        else
        {
          string str4 = radioBand;
          if (str4.Contains("800") || str4.Contains("868"))
          {
            str1 = "#1E1E1E";
            str2 = "#FFFF00";
          }
          else if (radioBand.Contains("PoE"))
          {
            str1 = "#EF7924";
            str2 = "#333333";
          }
          else if (radioBand.Contains("LTE"))
          {
            str1 = "#000000";
            str2 = "#FD7E14";
          }
          else if (radioBand.Contains("WiFi"))
          {
            str1 = "#000000";
            str2 = "#74ABD6";
          }
        }
      }
    }
    return $"<span style=\"color: {str1}; background: {str2}; font-weight: 400; padding: 0px 3px; border-radius:3px;\"> {item.RadioBand} </span>";
  }

  public ActionResult ServerSessionKeepAlive() => (ActionResult) this.Content("");

  [AuthorizeDefault]
  public ActionResult TopBarNotiRefresh()
  {
    if (MonnitSession.CurrentCustomer == null)
      return (ActionResult) this.Content("{\"activeCount\" : 0 , \"pendingCount\": 0 , \"showIcon\": 0 }");
    List<NotificationTriggered> source = NotificationTriggered.LoadActiveByAccountID(MonnitSession.CurrentCustomer.AccountID);
    if (source.Count<NotificationTriggered>() == 0)
      return (ActionResult) this.Content("{\"activeCount\" : 0 , \"pendingCount\": 0 , \"showIcon\": 0 }");
    List<NotificationTriggered> list1 = source.Where<NotificationTriggered>((Func<NotificationTriggered, bool>) (c => c.AcknowledgedTime == DateTime.MinValue)).ToList<NotificationTriggered>();
    List<NotificationTriggered> list2 = source.Where<NotificationTriggered>((Func<NotificationTriggered, bool>) (c => c.AcknowledgedTime != DateTime.MinValue && c.resetTime == DateTime.MinValue)).ToList<NotificationTriggered>();
    bool o = false;
    if (list1.Count<NotificationTriggered>() > 0 || list2.Count > 0)
      o = true;
    return (ActionResult) this.Content($"{{\"activeCount\" :{list1.Count<NotificationTriggered>().ToString()}, \"pendingCount\": {list2.Count<NotificationTriggered>().ToString()} , \"showIcon\":{o.ToInt().ToString()} }}");
  }
}
