// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.CSNetController
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
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

#nullable disable
namespace iMonnit.Controllers;

[NoCache]
public class CSNetController : CSNetControllerBase
{
  public new static List<Gateway> GetGatewayList() => CSNetController.GetGatewayList(out int _);

  public new static List<Gateway> GetGatewayList(out int totalGateways)
  {
    totalGateways = 0;
    long CSNetID = MonnitSession.GatewayListFilters.SensorListFiltersCSNetID;
    long GatewayTypeID = MonnitSession.GatewayListFilters.GatewayTypeID;
    int Status = MonnitSession.GatewayListFilters.Status;
    string Name = MonnitSession.GatewayListFilters.Name;
    if (MonnitSession.CurrentCustomer == null)
      return new List<Gateway>();
    IEnumerable<Gateway> source1 = Gateway.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where<Gateway>((Func<Gateway, bool>) (g => (g.CSNetID == CSNetID || CSNetID == long.MinValue) && MonnitSession.CurrentCustomer.CanSeeNetwork(g.CSNetID) && g.SensorID == long.MinValue));
    totalGateways = source1.Count<Gateway>();
    IEnumerable<Gateway> source2 = (IEnumerable<Gateway>) source1.Where<Gateway>((Func<Gateway, bool>) (g =>
    {
      if (g.Status.ToInt() != Status && Status != int.MinValue || g.GatewayTypeID != GatewayTypeID && GatewayTypeID != long.MinValue)
        return false;
      return g.Name.ToLower().Contains(Name.ToLower()) || Name == "";
    })).OrderBy<Gateway, string>((Func<Gateway, string>) (g => g.Name.Trim()));
    switch (MonnitSession.GatewayListSort.Name)
    {
      case "Type":
        source2 = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Gateway>) source2.OrderBy<Gateway, long>((Func<Gateway, long>) (cs => cs.GatewayTypeID)) : (IEnumerable<Gateway>) source2.OrderByDescending<Gateway, long>((Func<Gateway, long>) (t => t.GatewayTypeID));
        break;
      case "Gateway Name":
        source2 = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Gateway>) source2.OrderBy<Gateway, string>((Func<Gateway, string>) (g => g.Name)) : (IEnumerable<Gateway>) source2.OrderByDescending<Gateway, string>((Func<Gateway, string>) (g => g.Name));
        break;
      case "Signal":
        source2 = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Gateway>) source2.OrderBy<Gateway, int>((Func<Gateway, int>) (s => s.CurrentSignalStrength)) : (IEnumerable<Gateway>) source2.OrderByDescending<Gateway, int>((Func<Gateway, int>) (s => s.CurrentSignalStrength));
        break;
      case "Last Check In":
        source2 = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Gateway>) source2.OrderBy<Gateway, string>((Func<Gateway, string>) (b => b.LastCommunicationDate.ToStringSafe() != null ? b.LastCommunicationDate.ToStringSafe() : "01/01/1900")) : (IEnumerable<Gateway>) source2.OrderByDescending<Gateway, string>((Func<Gateway, string>) (b => b.LastCommunicationDate.ToStringSafe() != null ? b.LastCommunicationDate.ToStringSafe() : "01/01/1900"));
        break;
      case "IsEnterprise":
        source2 = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Gateway>) source2.OrderBy<Gateway, bool>((Func<Gateway, bool>) (cs => cs.isEnterpriseHost)) : (IEnumerable<Gateway>) source2.OrderByDescending<Gateway, bool>((Func<Gateway, bool>) (t => t.isEnterpriseHost));
        break;
    }
    return source2.ToList<Gateway>();
  }

  [NoCache]
  [Authorize]
  public ActionResult GatewayHistory(long id, Guid? dataMessageGUID)
  {
    Gateway gateway = Gateway.Load(id);
    this.ViewData["Gateway"] = (object) gateway;
    List<GatewayMessage> gatewayMessageList = new List<GatewayMessage>();
    DateTime fromDate = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate.Date, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime dateTime = MonnitSession.HistoryToDate;
    dateTime = dateTime.Date;
    DateTime utcFromLocalById = Monnit.TimeZone.GetUTCFromLocalById(dateTime.AddDays(1.0), MonnitSession.CurrentCustomer.Account.TimeZoneID);
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && gateway.StartDate != DateTime.MinValue && gateway.StartDate.Ticks > fromDate.Ticks)
      fromDate = gateway.StartDate;
    Guid dataMessageGUID1 = dataMessageGUID ?? Guid.Empty;
    return (ActionResult) this.PartialView((object) GatewayMessage.LoadByGatewayAndDateRange2(id, fromDate, utcFromLocalById, 500, dataMessageGUID1));
  }

  [Authorize]
  public ActionResult GatewayDataUsage(long id)
  {
    Gateway gateway = Gateway.Load(id);
    if (gateway == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID))
      return (ActionResult) this.Content("Gateway not found.");
    DateTime utcNow = DateTime.UtcNow;
    string uri1 = "";
    string str1 = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str1) && bool.Parse(str1);
    string str2 = ConfigData.FindValue("MEA_API_Location");
    string str3;
    try
    {
      uri1 = $"{str2}xml?applicationAuthGuid={ConfigData.FindValue("MEA_API_Auth_Guid")}";
      XDocument xdocument = XDocument.Load(uri1);
      str3 = xdocument.Descendants((XName) "object").Single<XElement>().Attribute((XName) "auth").Value;
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-GatewayDataUsage.AuthToken",
          MachineName = Environment.MachineName,
          RequestBody = uri1,
          ResponseBody = xdocument.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
    }
    catch (Exception ex)
    {
      ex.Log($"CSNetController.GatewayDataUsage[ID: {id.ToString()} ].AuthToken {ex.ToString()}");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-GatewayDataUsage.AuthToken",
          MachineName = Environment.MachineName,
          RequestBody = uri1,
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
      return (ActionResult) this.Content("Unable to retrieve gateway usage.");
    }
    if (string.IsNullOrEmpty(gateway.MacAddress))
      return (ActionResult) this.Content("Unable to retrieve gateway usage.");
    string str4 = Convert.ToInt64(gateway.MacAddress.Split('|')[0]).ToString("X");
    List<GatewayDataUsageModel> source = new List<GatewayDataUsageModel>();
    string uri2 = "";
    try
    {
      uri2 = string.Format(str2 + "xml/RetreiveCellularUsage/{0}?CellularIdentifier={1}&months=12", (object) str3, (object) str4);
      XDocument xdocument = XDocument.Load(uri2);
      source = xdocument.Descendants((XName) "CellularUsage").Select<XElement, GatewayDataUsageModel>((Func<XElement, GatewayDataUsageModel>) (obj => new GatewayDataUsageModel()
      {
        Year = obj.Attribute((XName) "Year").Value.ToInt(),
        Month = obj.Attribute((XName) "Month").Value.ToInt(),
        MB = obj.Attribute((XName) "Usage").Value.ToDecimal()
      })).ToList<GatewayDataUsageModel>();
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-GatewayDataUsage.RetreiveCellularUsage",
          MachineName = Environment.MachineName,
          RequestBody = uri2,
          ResponseBody = xdocument.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
    }
    catch (Exception ex)
    {
      ex.Log($"CSNetController.GatewayDataUsage[ID: {id.ToString()}].RetreiveCellularUsage ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-GatewayDataUsage.RetreiveCellularUsage",
          MachineName = Environment.MachineName,
          RequestBody = uri2,
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return (ActionResult) this.PartialView((object) source.OrderByDescending<GatewayDataUsageModel, int>((Func<GatewayDataUsageModel, int>) (m => m.Year)).ThenByDescending<GatewayDataUsageModel, int>((Func<GatewayDataUsageModel, int>) (m => m.Month)));
  }

  [Authorize]
  public ActionResult ListOfSensorCheckins(long id)
  {
    return (ActionResult) this.PartialView(nameof (ListOfSensorCheckins), (object) Reporting.GatewaySensorLastCheckIn.Exec(id));
  }

  [Authorize]
  public ActionResult Details(long id)
  {
    CSNet model = CSNet.Load(id);
    return !MonnitSession.IsAuthorizedForAccount(model.AccountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Overview",
      controller = "Account"
    }) : (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult GatewayDetails(long id)
  {
    Gateway model = Gateway.Load(id);
    return !MonnitSession.IsAuthorizedForAccount(CSNet.Load(model.CSNetID).AccountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Overview",
      controller = "Account"
    }) : (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult Edit(long id)
  {
    CSNet model = CSNet.Load(id);
    return !MonnitSession.IsAuthorizedForAccount(model.AccountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Overview",
      controller = "Account"
    }) : (ActionResult) this.View((object) model);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Edit(long id, CSNet csnet)
  {
    CSNet DBObject = CSNet.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(DBObject.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    if (this.ModelState.IsValid)
    {
      Account account = Account.Load(DBObject.AccountID);
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Network, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited network");
      try
      {
        DBObject.Name = this.Request.Form["Name"];
        DBObject.SendNotifications = csnet.SendNotifications;
        DBObject.HoldingOnlyNetwork = csnet.HoldingOnlyNetwork;
        DBObject.ExternalAccessUntil = csnet.ExternalAccessUntil;
        DBObject.Save();
        return (ActionResult) this.Content("Success!<script type=\"text/javascript\">window.location.href = window.location.href;</script>");
      }
      catch
      {
      }
    }
    return (ActionResult) this.View((object) csnet);
  }

  [Authorize]
  public ActionResult Remove(long id)
  {
    try
    {
      Gateway gateway = Gateway.Load(id);
      CSNet csNet = CSNet.Load(gateway.CSNetID);
      if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index"
        });
      if (gateway != null && gateway.IsDeleted)
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index"
        });
      NetworkAudit.LogNetworkChange(MonnitSession.CurrentCustomer.CustomerID, gateway, new long?(csNet.AccountID));
      Account account = Account.Load(csNet.AccountID);
      gateway.LogAuditData(eAuditAction.Delete, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Removed gateway from network");
      foreach (CustomerFavorite customerFavorite in CustomerFavorite.LoadByGatewayID(id))
      {
        customerFavorite.Delete();
        if (customerFavorite.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
          MonnitSession.CurrentCustomerFavorites.Invalidate();
      }
      foreach (Notification notification in GatewayNotification.LoadByGatewayID(gateway.GatewayID))
        notification.RemoveGateway(gateway);
      if (gateway.SensorID > 0L)
      {
        Sensor sensor = Sensor.Load(gateway.SensorID);
        sensor.LogAuditData(eAuditAction.Delete, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Removed sensor from network");
        sensor.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
        sensor.LastCommunicationDate = DateTime.MinValue;
        sensor.LastDataMessageGUID = Guid.Empty;
        sensor.Save();
        sensor.ResetLastCommunicationDate();
        foreach (Notification notification in Notification.LoadBySensorID(sensor.SensorID))
          notification.RemoveSensor(sensor);
      }
      gateway.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
      gateway.ResetToDefault(false);
      gateway.SendResetNetworkRequest = true;
      gateway.Save();
      return (ActionResult) this.Content("Success");
    }
    catch
    {
    }
    return (ActionResult) this.Content("Unable to remove gateway.");
  }

  [Authorize]
  public ActionResult GatewayEditWindow(long id)
  {
    Gateway model = Gateway.Load(id);
    CSNet csNet = CSNet.Load(model.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return (ActionResult) this.RedirectToAction("Overview", "Account");
    // ISSUE: reference to a compiler-generated field
    if (CSNetController.\u003C\u003Eo__10.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CSNetController.\u003C\u003Eo__10.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "netID", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = CSNetController.\u003C\u003Eo__10.\u003C\u003Ep__0.Target((CallSite) CSNetController.\u003C\u003Eo__10.\u003C\u003Ep__0, this.ViewBag, csNet.CSNetID);
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult GatewayEdit(long id)
  {
    Gateway model = Gateway.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(CSNet.Load(model.CSNetID).AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    string str = $"GatewayType\\{model.GatewayTypeID}\\GatewayEdit";
    return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "CSNet", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) model) : (ActionResult) this.PartialView((object) model);
  }

  public ActionResult ResetStartDate(long id)
  {
    ActionResult actionResult = (ActionResult) this.Content("Success");
    try
    {
      Gateway DBObject = Gateway.Load(id);
      Account account = Account.Load(CSNet.Load(DBObject.CSNetID).AccountID);
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Reset gateway start date");
      DBObject.StartDate = DateTime.UtcNow;
      DBObject.Save();
    }
    catch (Exception ex)
    {
      actionResult = (ActionResult) this.Content("Failed to Reset Start Date of GatewayID: " + id.ToString());
      ex.Log("CSNetController.ResetStartDate failed on GatewayID: " + id.ToString());
    }
    return actionResult;
  }

  public ActionResult GatewaySevenEdit(Gateway gateway, FormCollection collection)
  {
    this.UpdateModel<Gateway>(gateway);
    if (string.IsNullOrEmpty(gateway.Name))
    {
      this.ModelState.AddModelError("Name", "Required");
      this.ModelState.AddModelError("", "General -> Name");
    }
    if (gateway.ReportInterval < 0.0 || gateway.ReportInterval > 720.0)
    {
      this.ModelState.AddModelError("ReportInterval", "Must be between 1 and 720");
      this.ModelState.AddModelError("", "General -> Heartbeat");
    }
    if (new Version(gateway.GatewayFirmwareVersion) >= new Version("3.1.0.0") && (gateway.PollInterval < 0.0 || gateway.PollInterval > 720.0))
    {
      this.ModelState.AddModelError("PollInterval", "Must be between 1 and 720");
      this.ModelState.AddModelError("", "General -> Poll Rate");
    }
    if (collection["DHCP"] != "on")
    {
      if (gateway.GatewayIP == "" || gateway.GatewayIP == "0.0.0.0" || !gateway.GatewayIP.IsIPAddress())
      {
        this.ModelState.AddModelError("GatewayIP", "Must be valid IP Address (ie 192.168.0.10)");
        this.ModelState.AddModelError("", "Local Area Network -> IP Address");
      }
      if (gateway.GatewaySubnet == "" || gateway.GatewaySubnet == "0.0.0.0" || !gateway.GatewaySubnet.IsIPAddress())
      {
        this.ModelState.AddModelError("GatewaySubnet", "Must be valid subnet mask (ie 255.255.255.0)");
        this.ModelState.AddModelError("", "Local Area Network -> Subnet Mask");
      }
      if (gateway.GatewayDNS == "" || gateway.GatewayDNS == "0.0.0.0" || !gateway.GatewayDNS.IsIPAddress())
      {
        this.ModelState.AddModelError("GatewayDNS", "Must be valid IP Address (ie 192.168.0.1)");
        this.ModelState.AddModelError("", "Local Area Network -> DNS Server");
      }
      if (gateway.DefaultRouterIP == "" || gateway.DefaultRouterIP == "0.0.0.0" || !gateway.DefaultRouterIP.IsIPAddress())
      {
        this.ModelState.AddModelError("DefaultRouterIP", "Must be valid IP Address (ie 192.168.0.1)");
        this.ModelState.AddModelError("", "Local Area Network -> Default Gateway");
      }
    }
    if (gateway.SingleQueueExpiration < 1.0 || gateway.SingleQueueExpiration > 720.0)
    {
      this.ModelState.AddModelError("SingleQueueExpiration", "between 1 and 720");
      this.ModelState.AddModelError("", "Interface Activation -> Queue Expiration");
    }
    if (gateway.RealTimeInterfaceActive)
    {
      if (gateway.RealTimeInterfaceTimeout < 1.0 || gateway.RealTimeInterfaceTimeout > 3600.0)
      {
        this.ModelState.AddModelError("RealTimeInterfaceTimeout", "between 1 and 3600");
        this.ModelState.AddModelError("", "Real Time Interface -> TCP Timeout");
      }
      if (gateway.RealTimeInterfacePort < 1 || gateway.RealTimeInterfacePort > (int) ushort.MaxValue)
      {
        this.ModelState.AddModelError("RealTimeInterfacePort", "Invalid Port");
        this.ModelState.AddModelError("", "Real Time Interface -> Port");
      }
    }
    if (gateway.ModbusInterfaceActive)
    {
      if (gateway.ModbusInterfaceTimeout < 1.0 || gateway.ModbusInterfaceTimeout > 60.0)
      {
        this.ModelState.AddModelError("RealTimeInterfaceTimeout", "between 1 and 60");
        this.ModelState.AddModelError("", "Modbus Interface -> TCP Timeout");
      }
      if (gateway.ModbusInterfacePort < 1 || gateway.ModbusInterfacePort > (int) ushort.MaxValue)
      {
        this.ModelState.AddModelError("ModbusInterfacePort", "Invalid Port");
        this.ModelState.AddModelError("", "Modbus Interface -> Port");
      }
    }
    if (gateway.SNMPInterface1Active)
    {
      if (!gateway.SNMPInterfaceAddress1.IsIPAddress())
      {
        this.ModelState.AddModelError("SNMPInterfaceAddress1", "Invalid IP Address");
        this.ModelState.AddModelError("", "SNMP Interface 1 -> SNMP Address");
      }
      if (gateway.SNMPInterfacePort1 < 1 || gateway.SNMPInterfacePort1 > (int) ushort.MaxValue)
      {
        this.ModelState.AddModelError("SNMPInterfacePort1", "Invalid Port");
        this.ModelState.AddModelError("", "SNMP Interface 1 -> Port");
      }
      if (gateway.SNMPTrap1Active && (gateway.SNMPTrapPort1 < 1 || gateway.SNMPTrapPort1 > (int) ushort.MaxValue))
      {
        this.ModelState.AddModelError("SNMPTrapPort1", "Invalid Port");
        this.ModelState.AddModelError("", "SNMP Interface 1 -> Trap Port");
      }
    }
    if (gateway.SNMPInterface2Active)
    {
      if (!gateway.SNMPInterfaceAddress2.IsIPAddress())
      {
        this.ModelState.AddModelError("SNMPInterfaceAddress2", "Invalid IP Address");
        this.ModelState.AddModelError("", "SNMP Interface 2 -> SNMP Address");
      }
      if (gateway.SNMPInterfacePort2 < 1 || gateway.SNMPInterfacePort2 > (int) ushort.MaxValue)
      {
        this.ModelState.AddModelError("SNMPInterfacePort2", "Invalid Port");
        this.ModelState.AddModelError("", "SNMP Interface 2 -> Port");
      }
      if (gateway.SNMPTrap2Active && (gateway.SNMPTrapPort2 < 2 || gateway.SNMPTrapPort2 > (int) ushort.MaxValue))
      {
        this.ModelState.AddModelError("SNMPTrapPort2", "Invalid Port");
        this.ModelState.AddModelError("", "SNMP Interface 2 -> Trap Port");
      }
    }
    if (gateway.SNMPInterface3Active)
    {
      if (!gateway.SNMPInterfaceAddress3.IsIPAddress())
      {
        this.ModelState.AddModelError("SNMPInterfaceAddress3", "Invalid IP Address");
        this.ModelState.AddModelError("", "SNMP Interface 3 -> SNMP Address");
      }
      if (gateway.SNMPInterfacePort3 < 1 || gateway.SNMPInterfacePort3 > (int) ushort.MaxValue)
      {
        this.ModelState.AddModelError("SNMPInterfacePort3", "Invalid Port");
        this.ModelState.AddModelError("", "SNMP Interface 3 -> Port");
      }
      if (gateway.SNMPTrap3Active && (gateway.SNMPTrapPort3 < 3 || gateway.SNMPTrapPort3 > (int) ushort.MaxValue))
      {
        this.ModelState.AddModelError("SNMPTrapPort3", "Invalid Port");
        this.ModelState.AddModelError("", "SNMP Interface 3 -> Trap Port");
      }
    }
    if (gateway.SNMPInterface4Active)
    {
      if (!gateway.SNMPInterfaceAddress4.IsIPAddress())
      {
        this.ModelState.AddModelError("SNMPInterfaceAddress4", "Invalid IP Address");
        this.ModelState.AddModelError("", "SNMP Interface 4 -> SNMP Address");
      }
      if (gateway.SNMPInterfacePort4 < 1 || gateway.SNMPInterfacePort4 > (int) ushort.MaxValue)
      {
        this.ModelState.AddModelError("SNMPInterfacePort4", "Invalid Port");
        this.ModelState.AddModelError("", "SNMP Interface 4 -> Port");
      }
      if (gateway.SNMPTrap4Active && (gateway.SNMPTrapPort4 < 4 || gateway.SNMPTrapPort4 > (int) ushort.MaxValue))
      {
        this.ModelState.AddModelError("SNMPTrapPort4", "Invalid Port");
        this.ModelState.AddModelError("", "SNMP Interface 4 -> Trap Port");
      }
    }
    if (this.ModelState.IsValid)
    {
      try
      {
        Account account = Account.Load(CSNet.Load(gateway.CSNetID).AccountID);
        gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway settings");
        GatewayType gatewayType = GatewayType.Load(gateway.GatewayTypeID);
        if (gatewayType != null)
        {
          if (gateway.isEnterpriseHost)
          {
            gateway.ServerHostAddress = gatewayType.DefaultServerHostAddress;
            gateway.ServerHostAddress2 = gatewayType.DefaultServerHostAddress2;
            gateway.Port = gatewayType.DefaultPort;
            gateway.Port2 = gatewayType.DefaultPort2;
            gateway.isEnterpriseHost = false;
          }
          if (collection["DHCP"] == "on")
          {
            gateway.GatewayIP = gatewayType.DefaultGatewayIP;
            gateway.GatewaySubnet = gatewayType.DefaultGatewaySubnet;
            gateway.DefaultRouterIP = gatewayType.DefaultRouterIP;
            gateway.GatewayDNS = gatewayType.DefaultGatewayDNS;
            gateway.SecondaryDNS = gatewayType.DefaultSecondaryDNS;
          }
          if (!gateway.RealTimeInterfaceActive)
          {
            gateway.RealTimeInterfaceTimeout = gatewayType.DefaultRealTimeInterfaceTimeout;
            gateway.RealTimeInterfacePort = gatewayType.DefaultRealTimeInterfacePort;
          }
          if (!gateway.ModbusInterfaceActive)
          {
            gateway.ModbusInterfaceTimeout = gatewayType.DefaultModbusInterfaceTimeout;
            gateway.ModbusInterfacePort = gatewayType.DefaultModbusInterfacePort;
          }
          if (!gateway.SNMPInterface1Active)
          {
            gateway.SNMPInterfaceAddress1 = "0.0.0.0";
            gateway.SNMPInterfacePort1 = gatewayType.DefaultSNMPInterfacePort;
            gateway.SNMPTrap1Active = false;
            gateway.SNMPTrapPort1 = gatewayType.DefaultSNMPTrapPort;
          }
          if (!gateway.SNMPInterface2Active)
          {
            gateway.SNMPInterfaceAddress2 = "0.0.0.0";
            gateway.SNMPInterfacePort2 = gatewayType.DefaultSNMPInterfacePort;
            gateway.SNMPTrap2Active = false;
            gateway.SNMPTrapPort2 = gatewayType.DefaultSNMPTrapPort;
          }
          if (!gateway.SNMPInterface3Active)
          {
            gateway.SNMPInterfaceAddress3 = "0.0.0.0";
            gateway.SNMPInterfacePort3 = gatewayType.DefaultSNMPInterfacePort;
            gateway.SNMPTrap3Active = false;
            gateway.SNMPTrapPort3 = gatewayType.DefaultSNMPTrapPort;
          }
          if (!gateway.SNMPInterface4Active)
          {
            gateway.SNMPInterfaceAddress4 = "0.0.0.0";
            gateway.SNMPInterfacePort4 = gatewayType.DefaultSNMPInterfacePort;
            gateway.SNMPTrap4Active = false;
            gateway.SNMPTrapPort4 = gatewayType.DefaultSNMPTrapPort;
          }
          gateway.Save();
          if (gateway.IsDirty)
          {
            // ISSUE: reference to a compiler-generated field
            if (CSNetController.\u003C\u003Eo__13.\u003C\u003Ep__0 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CSNetController.\u003C\u003Eo__13.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = CSNetController.\u003C\u003Eo__13.\u003C\u003Ep__0.Target((CallSite) CSNetController.\u003C\u003Eo__13.\u003C\u003Ep__0, this.ViewBag, "Gateway Edit Pending");
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (CSNetController.\u003C\u003Eo__13.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CSNetController.\u003C\u003Eo__13.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = CSNetController.\u003C\u003Eo__13.\u003C\u003Ep__1.Target((CallSite) CSNetController.\u003C\u003Eo__13.\u003C\u003Ep__1, this.ViewBag, "Gateway Edit Success");
          }
          // ISSUE: reference to a compiler-generated field
          if (CSNetController.\u003C\u003Eo__13.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CSNetController.\u003C\u003Eo__13.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = CSNetController.\u003C\u003Eo__13.\u003C\u003Ep__2.Target((CallSite) CSNetController.\u003C\u003Eo__13.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/CSNet/GatewayEdit/" + gateway.GatewayID.ToString() : collection["returns"]);
          return (ActionResult) this.PartialView("EditConfirmation");
        }
        this.ModelState.AddModelError("", "Invalid Gateway Type");
      }
      catch (Exception ex)
      {
        this.ModelState.AddModelError("", ex.Message);
      }
    }
    string str = $"GatewayType\\{gateway.GatewayTypeID}\\GatewayEdit";
    return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "CSNet", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) gateway) : (ActionResult) this.View((object) gateway);
  }

  public ActionResult GatewayThirtyEdit(Gateway gateway, FormCollection collection)
  {
    this.UpdateModel<Gateway>(gateway);
    if (string.IsNullOrEmpty(gateway.Name))
    {
      this.ModelState.AddModelError("Name", "Required");
      this.ModelState.AddModelError("", "General -> Name");
    }
    if (gateway.ReportInterval < 0.0 || gateway.ReportInterval > 720.0)
    {
      this.ModelState.AddModelError("ReportInterval", "Must be between 1 and 720");
      this.ModelState.AddModelError("", "General -> Heartbeat");
    }
    if (new Version(gateway.GatewayFirmwareVersion) >= new Version("3.1.0.0") && (gateway.PollInterval < 0.0 || gateway.PollInterval > 720.0))
    {
      this.ModelState.AddModelError("PollInterval", "Must be between 1 and 720");
      this.ModelState.AddModelError("", "General -> Poll Rate");
    }
    if (new Version(gateway.GatewayFirmwareVersion) > new Version("1.0.2.0") && (gateway.ResetInterval < 0 || gateway.ResetInterval > 8760))
    {
      this.ModelState.AddModelError("ResetInterval", "Must be between 0 and 8760");
      this.ModelState.AddModelError("", "Commands -> Reset Interval");
    }
    if (new Version(gateway.GatewayFirmwareVersion) > new Version("1.0.2.0") && (gateway.SingleQueueExpiration.ToInt() < 1 || gateway.SingleQueueExpiration.ToInt() > (int) ushort.MaxValue))
    {
      this.ModelState.AddModelError("SingleQueueExpiration", "Must be between 1 and 65535");
      this.ModelState.AddModelError("", "Commands -> Will Call Expiration");
    }
    if (collection["DHCP"] != "on" && (gateway.SystemOptions & 1) > 0)
    {
      if (gateway.GatewayIP == "" || gateway.GatewayIP == "0.0.0.0" || !gateway.GatewayIP.IsIPAddress())
      {
        this.ModelState.AddModelError("GatewayIP", "Must be valid IP Address (ie 192.168.0.10)");
        this.ModelState.AddModelError("", "Local Area Network -> IP Address");
      }
      if (gateway.GatewaySubnet == "" || gateway.GatewaySubnet == "0.0.0.0" || !gateway.GatewaySubnet.IsIPAddress())
      {
        this.ModelState.AddModelError("GatewaySubnet", "Must be valid subnet mask (ie 255.255.255.0)");
        this.ModelState.AddModelError("", "Local Area Network -> Subnet Mask");
      }
      if (gateway.GatewayDNS == "" || gateway.GatewayDNS == "0.0.0.0" || !gateway.GatewayDNS.IsIPAddress())
      {
        this.ModelState.AddModelError("GatewayDNS", "Must be valid IP Address (ie 192.168.0.1)");
        this.ModelState.AddModelError("", "Local Area Network -> DNS Server");
      }
      if (gateway.DefaultRouterIP == "" || gateway.DefaultRouterIP == "0.0.0.0" || !gateway.DefaultRouterIP.IsIPAddress())
      {
        this.ModelState.AddModelError("DefaultRouterIP", "Must be valid IP Address (ie 192.168.0.1)");
        this.ModelState.AddModelError("", "Local Area Network -> Default Gateway");
      }
    }
    if (!string.IsNullOrEmpty(collection["SingleQueueExpiration"]) && gateway.SingleQueueExpiration < 1.0 || gateway.SingleQueueExpiration > 720.0)
    {
      this.ModelState.AddModelError("SingleQueueExpiration", "between 1 and 720");
      this.ModelState.AddModelError("", "Interface Activation -> Queue Expiration");
    }
    if (gateway.RealTimeInterfaceActive)
    {
      if (gateway.RealTimeInterfaceTimeout < 1.0 || gateway.RealTimeInterfaceTimeout > 3600.0)
      {
        this.ModelState.AddModelError("RealTimeInterfaceTimeout", "between 1 and 3600");
        this.ModelState.AddModelError("", "Real Time Interface -> TCP Timeout");
      }
      if (gateway.RealTimeInterfacePort < 1 || gateway.RealTimeInterfacePort > (int) ushort.MaxValue)
      {
        this.ModelState.AddModelError("RealTimeInterfacePort", "Invalid Port");
        this.ModelState.AddModelError("", "Real Time Interface -> Port");
      }
    }
    if (gateway.ModbusInterfaceActive)
    {
      if (gateway.ModbusInterfaceTimeout < 1.0 || gateway.ModbusInterfaceTimeout > 60.0)
      {
        this.ModelState.AddModelError("RealTimeInterfaceTimeout", "between 1 and 60");
        this.ModelState.AddModelError("", "Modbus Interface -> TCP Timeout");
      }
      if (gateway.ModbusInterfacePort < 1 || gateway.ModbusInterfacePort > (int) ushort.MaxValue)
      {
        this.ModelState.AddModelError("ModbusInterfacePort", "Invalid Port");
        this.ModelState.AddModelError("", "Modbus Interface -> Port");
      }
    }
    if (gateway.SNMPInterface1Active)
    {
      if (!gateway.SNMPInterfaceAddress1.IsIPAddress())
      {
        this.ModelState.AddModelError("SNMPInterfaceAddress1", "Invalid IP Address");
        this.ModelState.AddModelError("", "SNMP Interface 1 -> SNMP Address");
      }
      if (gateway.SNMPInterfacePort1 < 1 || gateway.SNMPInterfacePort1 > (int) ushort.MaxValue)
      {
        this.ModelState.AddModelError("SNMPInterfacePort1", "Invalid Port");
        this.ModelState.AddModelError("", "SNMP Interface 1 -> Port");
      }
      if (gateway.SNMPTrap1Active && (gateway.SNMPTrapPort1 < 1 || gateway.SNMPTrapPort1 > (int) ushort.MaxValue))
      {
        this.ModelState.AddModelError("SNMPTrapPort1", "Invalid Port");
        this.ModelState.AddModelError("", "SNMP Interface 1 -> Trap Port");
      }
    }
    if (gateway.SNMPInterface2Active)
    {
      if (!gateway.SNMPInterfaceAddress2.IsIPAddress())
      {
        this.ModelState.AddModelError("SNMPInterfaceAddress2", "Invalid IP Address");
        this.ModelState.AddModelError("", "SNMP Interface 2 -> SNMP Address");
      }
      if (gateway.SNMPInterfacePort2 < 1 || gateway.SNMPInterfacePort2 > (int) ushort.MaxValue)
      {
        this.ModelState.AddModelError("SNMPInterfacePort2", "Invalid Port");
        this.ModelState.AddModelError("", "SNMP Interface 2 -> Port");
      }
      if (gateway.SNMPTrap2Active && (gateway.SNMPTrapPort2 < 2 || gateway.SNMPTrapPort2 > (int) ushort.MaxValue))
      {
        this.ModelState.AddModelError("SNMPTrapPort2", "Invalid Port");
        this.ModelState.AddModelError("", "SNMP Interface 2 -> Trap Port");
      }
    }
    if (gateway.SNMPInterface3Active)
    {
      if (!gateway.SNMPInterfaceAddress3.IsIPAddress())
      {
        this.ModelState.AddModelError("SNMPInterfaceAddress3", "Invalid IP Address");
        this.ModelState.AddModelError("", "SNMP Interface 3 -> SNMP Address");
      }
      if (gateway.SNMPInterfacePort3 < 1 || gateway.SNMPInterfacePort3 > (int) ushort.MaxValue)
      {
        this.ModelState.AddModelError("SNMPInterfacePort3", "Invalid Port");
        this.ModelState.AddModelError("", "SNMP Interface 3 -> Port");
      }
      if (gateway.SNMPTrap3Active && (gateway.SNMPTrapPort3 < 3 || gateway.SNMPTrapPort3 > (int) ushort.MaxValue))
      {
        this.ModelState.AddModelError("SNMPTrapPort3", "Invalid Port");
        this.ModelState.AddModelError("", "SNMP Interface 3 -> Trap Port");
      }
    }
    if (gateway.SNMPInterface4Active)
    {
      if (!gateway.SNMPInterfaceAddress4.IsIPAddress())
      {
        this.ModelState.AddModelError("SNMPInterfaceAddress4", "Invalid IP Address");
        this.ModelState.AddModelError("", "SNMP Interface 4 -> SNMP Address");
      }
      if (gateway.SNMPInterfacePort4 < 1 || gateway.SNMPInterfacePort4 > (int) ushort.MaxValue)
      {
        this.ModelState.AddModelError("SNMPInterfacePort4", "Invalid Port");
        this.ModelState.AddModelError("", "SNMP Interface 4 -> Port");
      }
      if (gateway.SNMPTrap4Active && (gateway.SNMPTrapPort4 < 4 || gateway.SNMPTrapPort4 > (int) ushort.MaxValue))
      {
        this.ModelState.AddModelError("SNMPTrapPort4", "Invalid Port");
        this.ModelState.AddModelError("", "SNMP Interface 4 -> Trap Port");
      }
    }
    if (this.ModelState.IsValid)
    {
      try
      {
        Account account = Account.Load(CSNet.Load(gateway.CSNetID).AccountID);
        gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway settings");
        GatewayType gatewayType = GatewayType.Load(gateway.GatewayTypeID);
        if (gatewayType != null)
        {
          if (gateway.isEnterpriseHost)
          {
            gateway.ServerHostAddress = gatewayType.DefaultServerHostAddress;
            gateway.ServerHostAddress2 = gatewayType.DefaultServerHostAddress2;
            gateway.Port = gatewayType.DefaultPort;
            gateway.Port2 = gatewayType.DefaultPort2;
            gateway.isEnterpriseHost = false;
          }
          if (collection["DHCP"] == "on")
          {
            gateway.GatewayIP = gatewayType.DefaultGatewayIP;
            gateway.GatewaySubnet = gatewayType.DefaultGatewaySubnet;
            gateway.DefaultRouterIP = gatewayType.DefaultRouterIP;
            gateway.GatewayDNS = gatewayType.DefaultGatewayDNS;
            gateway.SecondaryDNS = gatewayType.DefaultSecondaryDNS;
          }
          if (!gateway.RealTimeInterfaceActive)
          {
            gateway.RealTimeInterfaceTimeout = gatewayType.DefaultRealTimeInterfaceTimeout;
            gateway.RealTimeInterfacePort = gatewayType.DefaultRealTimeInterfacePort;
          }
          if (!gateway.ModbusInterfaceActive)
          {
            gateway.ModbusInterfaceTimeout = gatewayType.DefaultModbusInterfaceTimeout;
            gateway.ModbusInterfacePort = gatewayType.DefaultModbusInterfacePort;
          }
          if (!gateway.SNMPInterface1Active)
          {
            gateway.SNMPInterfaceAddress1 = "0.0.0.0";
            gateway.SNMPInterfacePort1 = gatewayType.DefaultSNMPInterfacePort;
            gateway.SNMPTrap1Active = false;
            gateway.SNMPTrapPort1 = gatewayType.DefaultSNMPTrapPort;
          }
          if (!gateway.SNMPInterface2Active)
          {
            gateway.SNMPInterfaceAddress2 = "0.0.0.0";
            gateway.SNMPInterfacePort2 = gatewayType.DefaultSNMPInterfacePort;
            gateway.SNMPTrap2Active = false;
            gateway.SNMPTrapPort2 = gatewayType.DefaultSNMPTrapPort;
          }
          if (!gateway.SNMPInterface3Active)
          {
            gateway.SNMPInterfaceAddress3 = "0.0.0.0";
            gateway.SNMPInterfacePort3 = gatewayType.DefaultSNMPInterfacePort;
            gateway.SNMPTrap3Active = false;
            gateway.SNMPTrapPort3 = gatewayType.DefaultSNMPTrapPort;
          }
          if (!gateway.SNMPInterface4Active)
          {
            gateway.SNMPInterfaceAddress4 = "0.0.0.0";
            gateway.SNMPInterfacePort4 = gatewayType.DefaultSNMPInterfacePort;
            gateway.SNMPTrap4Active = false;
            gateway.SNMPTrapPort4 = gatewayType.DefaultSNMPTrapPort;
          }
          gateway.Save();
          if (gateway.IsDirty)
          {
            // ISSUE: reference to a compiler-generated field
            if (CSNetController.\u003C\u003Eo__14.\u003C\u003Ep__0 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CSNetController.\u003C\u003Eo__14.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = CSNetController.\u003C\u003Eo__14.\u003C\u003Ep__0.Target((CallSite) CSNetController.\u003C\u003Eo__14.\u003C\u003Ep__0, this.ViewBag, "Gateway Edit Pending");
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (CSNetController.\u003C\u003Eo__14.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CSNetController.\u003C\u003Eo__14.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = CSNetController.\u003C\u003Eo__14.\u003C\u003Ep__1.Target((CallSite) CSNetController.\u003C\u003Eo__14.\u003C\u003Ep__1, this.ViewBag, "Gateway Edit Success");
          }
          // ISSUE: reference to a compiler-generated field
          if (CSNetController.\u003C\u003Eo__14.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CSNetController.\u003C\u003Eo__14.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = CSNetController.\u003C\u003Eo__14.\u003C\u003Ep__2.Target((CallSite) CSNetController.\u003C\u003Eo__14.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/CSNet/GatewayEdit/" + gateway.GatewayID.ToString() : collection["returns"]);
          return (ActionResult) this.PartialView("EditConfirmation");
        }
        this.ModelState.AddModelError("", "Invalid Gateway Type");
      }
      catch (Exception ex)
      {
        this.ModelState.AddModelError("", ex.Message);
      }
    }
    string str = $"GatewayType\\{gateway.GatewayTypeID}\\GatewayEdit";
    return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "CSNet", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) gateway) : (ActionResult) this.View((object) gateway);
  }

  public ActionResult GatewayThirtyThreeEdit(Gateway gateway, FormCollection collection)
  {
    this.UpdateModel<Gateway>(gateway);
    if (string.IsNullOrEmpty(gateway.Name))
    {
      this.ModelState.AddModelError("Name", "Required");
      this.ModelState.AddModelError("", "General -> Name");
    }
    if (gateway.ReportInterval < 0.0 || gateway.ReportInterval > 720.0)
    {
      this.ModelState.AddModelError("ReportInterval", "Must be between 1 and 720");
      this.ModelState.AddModelError("", "General -> Heartbeat");
    }
    if (new Version(gateway.GatewayFirmwareVersion) >= new Version("3.1.0.0") && (gateway.PollInterval < 0.0 || gateway.PollInterval > 720.0))
    {
      this.ModelState.AddModelError("PollInterval", "Must be between 1 and 720");
      this.ModelState.AddModelError("", "General -> Poll Rate");
    }
    if (new Version(gateway.GatewayFirmwareVersion) > new Version("1.0.2.0") && (gateway.ResetInterval < 0 || gateway.ResetInterval > 8760))
    {
      this.ModelState.AddModelError("ResetInterval", "Must be between 0 and 8760");
      this.ModelState.AddModelError("", "Commands -> Reset Interval");
    }
    if (new Version(gateway.GatewayFirmwareVersion) > new Version("1.0.2.0") && (gateway.SingleQueueExpiration.ToInt() < 1 || gateway.SingleQueueExpiration.ToInt() > (int) ushort.MaxValue))
    {
      this.ModelState.AddModelError("SingleQueueExpiration", "Must be between 1 and 65535");
      this.ModelState.AddModelError("", "Commands -> Will Call Expiration");
    }
    if (collection["DHCP"] != "on")
    {
      if (gateway.GatewayIP == "" || gateway.GatewayIP == "0.0.0.0" || !gateway.GatewayIP.IsIPAddress())
      {
        this.ModelState.AddModelError("GatewayIP", "Must be valid IP Address (ie 192.168.0.10)");
        this.ModelState.AddModelError("", "Local Area Network -> IP Address");
      }
      if (gateway.GatewaySubnet == "" || gateway.GatewaySubnet == "0.0.0.0" || !gateway.GatewaySubnet.IsIPAddress())
      {
        this.ModelState.AddModelError("GatewaySubnet", "Must be valid subnet mask (ie 255.255.255.0)");
        this.ModelState.AddModelError("", "Local Area Network -> Subnet Mask");
      }
      if (gateway.GatewayDNS == "" || gateway.GatewayDNS == "0.0.0.0" || !gateway.GatewayDNS.IsIPAddress())
      {
        this.ModelState.AddModelError("GatewayDNS", "Must be valid IP Address (ie 192.168.0.1)");
        this.ModelState.AddModelError("", "Local Area Network -> DNS Server");
      }
      if (gateway.DefaultRouterIP == "" || gateway.DefaultRouterIP == "0.0.0.0" || !gateway.DefaultRouterIP.IsIPAddress())
      {
        this.ModelState.AddModelError("DefaultRouterIP", "Must be valid IP Address (ie 192.168.0.1)");
        this.ModelState.AddModelError("", "Local Area Network -> Default Gateway");
      }
    }
    if (gateway.ModbusInterfaceActive)
    {
      if (gateway.ModbusInterfaceTimeout < 1.0 || gateway.ModbusInterfaceTimeout > 60.0)
      {
        this.ModelState.AddModelError("RealTimeInterfaceTimeout", "between 1 and 60");
        this.ModelState.AddModelError("", "Modbus Interface -> TCP Timeout");
      }
      if (gateway.ModbusInterfacePort < 1 || gateway.ModbusInterfacePort > (int) ushort.MaxValue)
      {
        this.ModelState.AddModelError("ModbusInterfacePort", "Invalid Port");
        this.ModelState.AddModelError("", "Modbus Interface -> Port");
      }
    }
    if (gateway.SNMPInterface1Active)
    {
      bool flag = true;
      if (!gateway.SNMPInterfaceAddress1.IsIPAddress())
      {
        flag = false;
        this.ModelState.AddModelError("SNMPInterfaceAddress1", "Invalid IP Address");
        this.ModelState.AddModelError("", "SNMP Interface -> Inbound IP Range Start");
      }
      if (!gateway.SNMPInterfaceAddress3.IsIPAddress())
      {
        flag = false;
        this.ModelState.AddModelError("SNMPInterfaceAddress3", "Invalid IP Address");
        this.ModelState.AddModelError("", "SNMP Interface -> Inbound IP Range End");
      }
      if (flag && new Version(gateway.SNMPInterfaceAddress1) >= new Version(gateway.SNMPInterfaceAddress3))
      {
        this.ModelState.AddModelError("SNMPInterfaceAddress3", "Invalid IP Address");
        this.ModelState.AddModelError("", "SNMP Interface -> Inbound IP Range Error");
      }
      if (gateway.SNMPInterfacePort1 < 1 || gateway.SNMPInterfacePort1 > (int) ushort.MaxValue)
      {
        this.ModelState.AddModelError("SNMPInterfacePort1", "Invalid Port");
        this.ModelState.AddModelError("", "SNMP Interface -> Port");
      }
      if (gateway.SNMPCommunityString.ToStringSafe().Length > 32 /*0x20*/)
      {
        this.ModelState.AddModelError("SNMPCommunityString", "Invalid Community String");
        this.ModelState.AddModelError("", "SNMP Interface -> SNMP Community String");
      }
      if (gateway.SNMPTrap1Active)
      {
        if (!gateway.SNMPInterfaceAddress2.IsIPAddress())
        {
          this.ModelState.AddModelError("SNMPInterfaceAddress2", "Invalid Trap IP Address");
          this.ModelState.AddModelError("", "SNMP Interface -> SNMP Trap IP Address");
        }
        if (gateway.SNMPTrapPort1 < 1 || gateway.SNMPTrapPort1 > (int) ushort.MaxValue)
        {
          this.ModelState.AddModelError("SNMPTrapPort1", "Invalid Port");
          this.ModelState.AddModelError("", "SNMP Interface -> Trap Port");
        }
      }
    }
    if (gateway.NTPInterfaceActive)
    {
      if (!gateway.NTPServerIP.IsIPAddress())
      {
        this.ModelState.AddModelError("NTPServerIP", "Invalid IP Address");
        this.ModelState.AddModelError("", "NTP Interface -> NTP Address");
      }
      if (gateway.NTPMinSampleRate.ToDouble() == 0.0 || gateway.NTPMinSampleRate.ToDouble() * 60.0 > (double) ushort.MaxValue)
      {
        this.ModelState.AddModelError("NTPMinSampleRate", "Invalid Sample");
        this.ModelState.AddModelError("", "NTP Interface -> Minimum Sample");
      }
    }
    if (gateway.HTTPInterfaceActive && (gateway.HTTPServiceTimeout.ToDouble() == 0.0 || gateway.HTTPServiceTimeout.ToDouble() * 60.0 > (double) ushort.MaxValue))
    {
      this.ModelState.AddModelError("HTTPServiceTimeout", "Invalid Timeout");
      this.ModelState.AddModelError("", "HTTP Interface -> Service Timeout");
    }
    if (this.ModelState.IsValid)
    {
      try
      {
        Account account = Account.Load(CSNet.Load(gateway.CSNetID).AccountID);
        gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway settings");
        GatewayType gatewayType = GatewayType.Load(gateway.GatewayTypeID);
        if (gatewayType != null)
        {
          if (gateway.isEnterpriseHost)
          {
            gateway.ServerHostAddress = gatewayType.DefaultServerHostAddress;
            gateway.ServerHostAddress2 = gatewayType.DefaultServerHostAddress2;
            gateway.Port = gatewayType.DefaultPort;
            gateway.Port2 = gatewayType.DefaultPort2;
            gateway.isEnterpriseHost = false;
          }
          if (string.IsNullOrEmpty(collection["ResetInterval"]))
            gateway.ResetInterval = 168;
          if (collection["DHCP"] == "on")
          {
            gateway.GatewayIP = gatewayType.DefaultGatewayIP;
            gateway.GatewaySubnet = gatewayType.DefaultGatewaySubnet;
            gateway.DefaultRouterIP = gatewayType.DefaultRouterIP;
            gateway.GatewayDNS = gatewayType.DefaultGatewayDNS;
            gateway.SecondaryDNS = gatewayType.DefaultSecondaryDNS;
          }
          if (!gateway.ModbusInterfaceActive)
          {
            gateway.ModbusInterfaceTimeout = gatewayType.DefaultModbusInterfaceTimeout;
            gateway.ModbusInterfacePort = gatewayType.DefaultModbusInterfacePort;
          }
          if (!gateway.SNMPInterface1Active)
          {
            gateway.SNMPInterface1Active = false;
            gateway.SNMPInterfaceAddress1 = "0.0.0.0";
            gateway.SNMPInterfaceAddress3 = "255.255.255.255";
            gateway.SNMPInterfacePort1 = gatewayType.DefaultSNMPInterfacePort;
            gateway.SNMPInterfaceAddress2 = "0.0.0.0";
            gateway.SNMPTrap1Active = false;
            gateway.SNMPTrap2Active = false;
            gateway.SNMPTrap3Active = false;
            gateway.SNMPTrap4Active = false;
            gateway.SNMPTrapPort1 = gatewayType.DefaultSNMPTrapPort;
          }
          if (!gateway.SNMPTrap1Active)
          {
            gateway.SNMPTrap2Active = false;
            gateway.SNMPTrap3Active = false;
            gateway.SNMPTrap4Active = false;
            gateway.SNMPTrapPort1 = gatewayType.DefaultSNMPTrapPort;
            gateway.SNMPInterfaceAddress2 = "0.0.0.0";
          }
          if (!gateway.NTPInterfaceActive)
          {
            gateway.NTPInterfaceActive = false;
            gateway.NTPServerIP = "0.0.0.0";
            gateway.NTPMinSampleRate = 30.0;
          }
          if (!gateway.HTTPInterfaceActive)
          {
            gateway.HTTPInterfaceActive = false;
            gateway.HTTPServiceTimeout = 0.0;
          }
          gateway.Save();
          if (gateway.IsDirty)
          {
            // ISSUE: reference to a compiler-generated field
            if (CSNetController.\u003C\u003Eo__15.\u003C\u003Ep__0 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CSNetController.\u003C\u003Eo__15.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = CSNetController.\u003C\u003Eo__15.\u003C\u003Ep__0.Target((CallSite) CSNetController.\u003C\u003Eo__15.\u003C\u003Ep__0, this.ViewBag, "Gateway Edit Pending");
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            if (CSNetController.\u003C\u003Eo__15.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CSNetController.\u003C\u003Eo__15.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj = CSNetController.\u003C\u003Eo__15.\u003C\u003Ep__1.Target((CallSite) CSNetController.\u003C\u003Eo__15.\u003C\u003Ep__1, this.ViewBag, "Gateway Edit Success");
          }
          // ISSUE: reference to a compiler-generated field
          if (CSNetController.\u003C\u003Eo__15.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CSNetController.\u003C\u003Eo__15.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = CSNetController.\u003C\u003Eo__15.\u003C\u003Ep__2.Target((CallSite) CSNetController.\u003C\u003Eo__15.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/CSNet/GatewayEdit/" + gateway.GatewayID.ToString() : collection["returns"]);
          return (ActionResult) this.PartialView("EditConfirmation");
        }
        this.ModelState.AddModelError("", "Invalid Gateway Type");
      }
      catch (Exception ex)
      {
        this.ModelState.AddModelError("", ex.Message);
      }
    }
    string str = $"GatewayType\\{gateway.GatewayTypeID}\\GatewayEdit";
    return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "CSNet", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) gateway) : (ActionResult) this.View((object) gateway);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult GatewayEdit(long id, Gateway gateway, FormCollection collection)
  {
    Gateway gateway1 = Gateway.Load(id);
    CSNet csNet = CSNet.Load(gateway1.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    if (gateway1.GatewayTypeID == 7L)
      return this.GatewaySevenEdit(gateway1, collection);
    if (gateway1.GatewayTypeID == 30L)
      return this.GatewayThirtyEdit(gateway1, collection);
    if (gateway1.GatewayTypeID == 33L)
      return this.GatewayThirtyThreeEdit(gateway1, collection);
    if ((gateway1.GatewayTypeID == 30L || gateway1.GatewayTypeID == 32L /*0x20*/ || gateway1.GatewayTypeID == 33L) && new Version(gateway1.GatewayFirmwareVersion) > new Version("1.0.2.0"))
    {
      if (collection["ResetInterval"].ToInt() < 1 || collection["ResetInterval"].ToInt() > 8760)
      {
        this.ModelState.AddModelError("ResetInterval", "Must be between 1 and 8760");
        this.ModelState.AddModelError("", "Commands -> Reset Interval");
      }
      if (collection["SingleQueueExpiration"].ToInt() < 1 || collection["SingleQueueExpiration"].ToInt() > (int) ushort.MaxValue)
      {
        this.ModelState.AddModelError("SingleQueueExpiration", "Must be between 1 and 65535");
        this.ModelState.AddModelError("", "Commands -> Will Call Expiration");
      }
    }
    uint? nullable1 = new uint?();
    if (!string.IsNullOrEmpty(this.Request["ManualChannelMask"]))
    {
      try
      {
        nullable1 = new uint?(Convert.ToUInt32(this.Request["ManualChannelMask"]));
      }
      catch (Exception ex)
      {
        this.ModelState.AddModelError("ChannelMask", "Invalid Channel");
      }
    }
    if (gateway.GatewayIP != "" && gateway.GatewayIP != "0.0.0.0")
    {
      if (gateway.GatewaySubnet == "" || gateway.GatewaySubnet == "0.0.0.0")
        this.ModelState.AddModelError("GatewaySubnet", "Subnet Required when not using DHCP");
      if (gateway.GatewayDNS == "" || gateway.GatewayDNS == "0.0.0.0")
        this.ModelState.AddModelError("GatewayDNS", "Default DNS Required when not using DHCP");
      if (gateway1.GatewayType.SupportsDefaultRouterIP && (gateway.DefaultRouterIP == "" || gateway.DefaultRouterIP == "0.0.0.0"))
        this.ModelState.AddModelError("DefaultRouterIP", "Default Gateway Required when not using DHCP");
      if (!gateway.GatewayIP.IsIPAddress())
        this.ModelState.AddModelError("GatewayIP", "Must be valid IP Address (ie 192.168.0.10)");
      if (!gateway.GatewaySubnet.IsIPAddress())
        this.ModelState.AddModelError("GatewaySubnet", "Must be valid Subnet Mask (ie 255.255.255.0)");
      if (!gateway.GatewayDNS.IsIPAddress())
        this.ModelState.AddModelError("GatewayDNS", "Must be valid IP Address (ie 192.168.0.1)");
      if (gateway1.GatewayType.SupportsDefaultRouterIP && !gateway.DefaultRouterIP.IsIPAddress())
        this.ModelState.AddModelError("DefaultRouterIP", "Must be valid IP Address (ie 192.168.0.1)");
    }
    if (this.ModelState.IsValid)
    {
      try
      {
        Account account = Account.Load(csNet.AccountID);
        gateway1.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway settings");
        if (string.IsNullOrEmpty(collection["ResetInterval"]))
          gateway1.ResetInterval = 168;
        gateway1.Name = gateway.Name;
        bool flag = false;
        if (gateway.GatewayTypeID > long.MinValue && gateway.GatewayTypeID != gateway1.GatewayTypeID)
        {
          gateway1.GatewayTypeID = gateway.GatewayTypeID;
          flag = true;
        }
        if (gateway1.GatewayTypeID > long.MinValue)
        {
          GatewayType gatewayType = GatewayType.Load(gateway1.GatewayTypeID);
          if (gatewayType.SupportsHeartbeat && gateway.ReportInterval > double.MinValue)
            gateway1.ReportInterval = gateway.ReportInterval;
          if (gatewayType.GatewayTypeID == 30L || gatewayType.GatewayTypeID == 32L /*0x20*/ || gatewayType.GatewayTypeID == 33L)
            gateway1.ResetInterval = gateway.ResetInterval;
          if (gatewayType.SupportsHostAddress && !string.IsNullOrEmpty(gateway.ServerHostAddress))
            gateway1.ServerHostAddress = gateway.ServerHostAddress;
          if (gatewayType.SupportsHostPort && gateway.Port > int.MinValue)
            gateway1.Port = gateway.Port;
          if (gatewayType.SupportsHostAddress && !gatewayType.SupportsHostAddress2)
            gateway1.ServerHostAddress2 = gateway1.ServerHostAddress;
          if (gatewayType.SupportsHostPort && !gatewayType.SupportsHostPort2)
            gateway1.Port2 = gateway1.Port;
          if (nullable1.HasValue)
          {
            long channelMask = gateway1.ChannelMask;
            uint? nullable2 = nullable1;
            long? nullable3 = nullable2.HasValue ? new long?((long) nullable2.GetValueOrDefault()) : new long?();
            long valueOrDefault = nullable3.GetValueOrDefault();
            if (!(channelMask == valueOrDefault & nullable3.HasValue))
            {
              gateway1.ChannelMask = (long) (nullable1 ?? uint.MaxValue);
              gateway1.SendResetNetworkRequest = true;
            }
          }
          else if (gatewayType.SupportsChannel && gateway.ChannelMask > (long) int.MinValue)
            gateway1.ChannelMask = gateway.ChannelMask;
          if (gatewayType.SupportsNetworkIDFilter && gateway.NetworkIDFilter > int.MinValue)
            gateway1.NetworkIDFilter = gateway.NetworkIDFilter;
          if (gatewayType.SupportsGatewayIP && !string.IsNullOrEmpty(gateway.GatewayIP))
            gateway1.GatewayIP = gateway.GatewayIP;
          if (gatewayType.SupportsGatewayIP && !string.IsNullOrEmpty(gateway.GatewaySubnet))
            gateway1.GatewaySubnet = gateway.GatewaySubnet;
          if (gatewayType.SupportsGatewayIP && !string.IsNullOrEmpty(gateway.GatewayDNS))
            gateway1.GatewayDNS = gateway.GatewayDNS;
          if (gatewayType.SupportsDefaultRouterIP && !string.IsNullOrEmpty(gateway.DefaultRouterIP))
            gateway1.DefaultRouterIP = gateway.DefaultRouterIP;
          if (gatewayType.SupportsNetworkListInterval)
            gateway1.NetworkListInterval = gateway.NetworkListInterval <= 0.0 || gateway.NetworkListInterval > 720.0 ? gatewayType.DefaultNetworkListInterval : gateway.NetworkListInterval;
          if (gatewayType.SupportsObserveAware && !string.IsNullOrEmpty(this.Request["ObserveAware"]))
            gateway1.ObserveAware = gateway.ObserveAware;
          if (gatewayType.SupportsCellAPNName)
            gateway1.CellAPNName = string.IsNullOrEmpty(gateway.CellAPNName) ? (flag ? gatewayType.DefaultCellAPNName : "") : gateway.CellAPNName;
          if (gatewayType.SupportsUsername)
            gateway1.Username = string.IsNullOrEmpty(gateway.Username) ? (flag ? gatewayType.DefaultUsername : "") : gateway.Username;
          if (gatewayType.SupportsPassword)
            gateway1.Password = string.IsNullOrEmpty(gateway.Password) ? (flag ? gatewayType.DefaultPassword : "") : (MonnitSession.UseEncryption ? gateway.Password.Encrypt() : gateway.Password);
          if (gatewayType.SupportsSecondaryDNS)
            gateway1.GatewayDNS = string.IsNullOrEmpty(gateway.GatewayDNS) ? gatewayType.DefaultGatewayDNS : gateway.GatewayDNS;
          if (gatewayType.SupportsSecondaryDNS)
            gateway1.SecondaryDNS = string.IsNullOrEmpty(gateway.SecondaryDNS) ? gatewayType.DefaultSecondaryDNS : gateway.SecondaryDNS;
          if (gatewayType.SupportsErrorHeartbeat)
            gateway1.ErrorHeartbeat = gateway.ReportInterval == double.MinValue ? gatewayType.DefaultErrorHeartbeat : gateway.ErrorHeartbeat;
          if (gatewayType.SupportsPollInterval)
            gateway1.PollInterval = gateway.PollInterval == double.MinValue ? gatewayType.DefaultPollInterval : collection["PollInterval"].ToDouble();
          if (gatewayType.SupportsGPSReportInterval)
            gateway1.GPSReportInterval = gateway.GPSReportInterval == double.MinValue ? gatewayType.DefaultGPSReportInterval : collection["GPSReportInterval"].ToDouble();
          if (gatewayType.SupportsForceLowPower && !string.IsNullOrEmpty(this.Request["GatewayPowerMode"]))
            gateway1.GatewayPowerMode = gateway.GatewayPowerMode;
          if (string.IsNullOrEmpty(collection["SingleQueueExpiration"]))
            gateway1.SingleQueueExpiration = gatewayType.DefaultSingleQueueExpiration;
          gateway1.isEnterpriseHost = false;
        }
        gateway1.Save();
        if (gateway1.IsDirty)
        {
          // ISSUE: reference to a compiler-generated field
          if (CSNetController.\u003C\u003Eo__16.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CSNetController.\u003C\u003Eo__16.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = CSNetController.\u003C\u003Eo__16.\u003C\u003Ep__0.Target((CallSite) CSNetController.\u003C\u003Eo__16.\u003C\u003Ep__0, this.ViewBag, "Gateway Edit Pending");
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if (CSNetController.\u003C\u003Eo__16.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CSNetController.\u003C\u003Eo__16.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = CSNetController.\u003C\u003Eo__16.\u003C\u003Ep__1.Target((CallSite) CSNetController.\u003C\u003Eo__16.\u003C\u003Ep__1, this.ViewBag, "Gateway Edit Success");
        }
        // ISSUE: reference to a compiler-generated field
        if (CSNetController.\u003C\u003Eo__16.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CSNetController.\u003C\u003Eo__16.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = CSNetController.\u003C\u003Eo__16.\u003C\u003Ep__2.Target((CallSite) CSNetController.\u003C\u003Eo__16.\u003C\u003Ep__2, this.ViewBag, string.IsNullOrWhiteSpace(collection["returns"]) || !this.IsLocalUrl(collection["returns"]) ? "/CSNet/GatewayEdit/" + gateway1.GatewayID.ToString() : collection["returns"]);
        return (ActionResult) this.PartialView("EditConfirmation");
      }
      catch
      {
      }
    }
    string str = $"GatewayType\\{gateway.GatewayTypeID}\\GatewayEdit";
    return MonnitViewEngine.CheckPartialViewExists(this.ControllerContext, str, "CSNet", MonnitSession.CurrentTheme.Theme) ? (ActionResult) this.PartialView(str, (object) gateway) : (ActionResult) this.View((object) gateway);
  }

  [NoCache]
  [Authorize]
  public ActionResult Acknowledge(long gatewayID, double heartbeat)
  {
    new AcknowledgementRecorded()
    {
      Acknowledgement = ("Gateway heartbeat of " + heartbeat.ToStringSafe()),
      CustomerID = MonnitSession.CurrentCustomer.CustomerID,
      GatewayID = gatewayID,
      DateRecorded = DateTime.UtcNow
    }.Save();
    return (ActionResult) this.Content("OK");
  }

  [Authorize]
  public ActionResult GatewayInterfaceConfiguration(long id)
  {
    Gateway model = Gateway.Load(id);
    return !MonnitSession.IsAuthorizedForAccount(CSNet.Load(model.CSNetID).AccountID) ? (ActionResult) this.Content("Unauthorized") : (ActionResult) this.PartialView((object) model);
  }

  [Authorize]
  public ActionResult GatewayInterfaceRT(long id)
  {
    Gateway model = Gateway.Load(id);
    return !MonnitSession.IsAuthorizedForAccount(CSNet.Load(model.CSNetID).AccountID) ? (ActionResult) this.Content("Unauthorized") : (ActionResult) this.PartialView((object) model);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult GatewayInterfaceRT(long id, Gateway gateway)
  {
    Gateway DBObject = Gateway.Load(id);
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return (ActionResult) this.Content("Unauthorized");
    try
    {
      if (gateway.RealTimeInterfaceTimeout < 1.0)
        gateway.RealTimeInterfaceTimeout = 1.0;
      if (gateway.RealTimeInterfaceTimeout > 3600.0)
        gateway.RealTimeInterfaceTimeout = 3600.0;
      if (gateway.RealTimeInterfacePort < 1 || gateway.RealTimeInterfacePort > (int) ushort.MaxValue)
        this.ModelState.AddModelError("RealTimeInterfacePort", "Invalid Port");
      if (this.ModelState.IsValid)
      {
        Account account = Account.Load(csNet.AccountID);
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway realtime interface settings");
        DBObject.RealTimeInterfaceActive = gateway.RealTimeInterfaceActive;
        DBObject.RealTimeInterfaceTimeout = gateway.RealTimeInterfaceTimeout / 60.0;
        DBObject.RealTimeInterfacePort = gateway.RealTimeInterfacePort;
        DBObject.Save();
        return (ActionResult) this.Content("Success!<script type=\"text/javascript\">//hideModal();</script>");
      }
    }
    catch (Exception ex)
    {
      ex.Log("CSNetController.GatewayInterfaceRT : Post GatewayID=" + id.ToString());
    }
    return (ActionResult) this.View((object) gateway);
  }

  [Authorize]
  public ActionResult GatewayInterfaceMB(long id)
  {
    Gateway model = Gateway.Load(id);
    return !MonnitSession.IsAuthorizedForAccount(CSNet.Load(model.CSNetID).AccountID) ? (ActionResult) this.Content("Unauthorized") : (ActionResult) this.PartialView((object) model);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult GatewayInterfaceMB(long id, Gateway gateway)
  {
    Gateway DBObject = Gateway.Load(id);
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return (ActionResult) this.Content("Unauthorized");
    try
    {
      if (gateway.SingleQueueExpiration < 1.0)
        gateway.SingleQueueExpiration = 1.0;
      if (gateway.SingleQueueExpiration > 720.0)
        gateway.SingleQueueExpiration = 720.0;
      if (gateway.ModbusInterfaceTimeout < 1.0)
        gateway.ModbusInterfaceTimeout = 1.0;
      if (gateway.ModbusInterfaceTimeout > 60.0)
        gateway.ModbusInterfaceTimeout = 60.0;
      if (gateway.ModbusInterfacePort < 1 || gateway.ModbusInterfacePort > (int) ushort.MaxValue)
        this.ModelState.AddModelError("ModbusInterfacePort", "Invalid Port");
      if (this.ModelState.IsValid)
      {
        Account account = Account.Load(csNet.AccountID);
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway modbus interface settings");
        DBObject.SingleQueueExpiration = gateway.SingleQueueExpiration;
        DBObject.ModbusInterfaceActive = gateway.ModbusInterfaceActive;
        DBObject.ModbusInterfaceTimeout = gateway.ModbusInterfaceTimeout;
        DBObject.ModbusInterfacePort = gateway.ModbusInterfacePort;
        DBObject.Save();
        return (ActionResult) this.Content("Success!<script type=\"text/javascript\">//hideModal();</script>");
      }
    }
    catch (Exception ex)
    {
      ex.Log("CSNetController.GatewayInterfaceMB : Post GatewayID=" + id.ToString());
    }
    return (ActionResult) this.View((object) gateway);
  }

  [Authorize]
  public ActionResult GatewayInterfaceSNMP(long id)
  {
    Gateway model = Gateway.Load(id);
    return !MonnitSession.IsAuthorizedForAccount(CSNet.Load(model.CSNetID).AccountID) ? (ActionResult) this.Content("Unauthorized") : (ActionResult) this.PartialView((object) model);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult GatewayInterfaceSNMP(long id, Gateway gateway)
  {
    Gateway DBObject = Gateway.Load(id);
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return (ActionResult) this.Content("Unauthorized");
    try
    {
      if (gateway.SingleQueueExpiration < 1.0)
        gateway.SingleQueueExpiration = 1.0;
      if (gateway.SingleQueueExpiration > 720.0)
        gateway.SingleQueueExpiration = 720.0;
      if (gateway.SNMPInterfacePort1 < 1 || gateway.SNMPInterfacePort1 > (int) ushort.MaxValue)
        this.ModelState.AddModelError("SNMPInterfacePort1", "Invalid Port");
      if (gateway.SNMPTrapPort1 < 1 || gateway.SNMPTrapPort1 > (int) ushort.MaxValue)
        this.ModelState.AddModelError("SNMPTrapPort1", "Invalid Port");
      if (gateway.SNMPInterfacePort2 < 1 || gateway.SNMPInterfacePort2 > (int) ushort.MaxValue)
        this.ModelState.AddModelError("SNMPInterfacePort2", "Invalid Port");
      if (gateway.SNMPTrapPort2 < 1 || gateway.SNMPTrapPort2 > (int) ushort.MaxValue)
        this.ModelState.AddModelError("SNMPTrapPort2", "Invalid Port");
      if (gateway.SNMPInterfacePort3 < 1 || gateway.SNMPInterfacePort3 > (int) ushort.MaxValue)
        this.ModelState.AddModelError("SNMPInterfacePort3", "Invalid Port");
      if (gateway.SNMPTrapPort3 < 1 || gateway.SNMPTrapPort3 > (int) ushort.MaxValue)
        this.ModelState.AddModelError("SNMPTrapPort3", "Invalid Port");
      if (gateway.SNMPInterfacePort4 < 1 || gateway.SNMPInterfacePort4 > (int) ushort.MaxValue)
        this.ModelState.AddModelError("SNMPInterfacePort4", "Invalid Port");
      if (gateway.SNMPTrapPort4 < 1 || gateway.SNMPTrapPort4 > (int) ushort.MaxValue)
        this.ModelState.AddModelError("SNMPTrapPort4", "Invalid Port");
      if (this.ModelState.IsValid)
      {
        Account account = Account.Load(csNet.AccountID);
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway snmp interface settings");
        DBObject.SingleQueueExpiration = gateway.SingleQueueExpiration;
        DBObject.SNMPInterface1Active = gateway.SNMPInterface1Active;
        DBObject.SNMPInterfaceAddress1 = gateway.SNMPInterfaceAddress1.ToStringSafe();
        DBObject.SNMPInterfacePort1 = gateway.SNMPInterfacePort1;
        DBObject.SNMPTrap1Active = gateway.SNMPTrap1Active;
        DBObject.SNMPTrapPort1 = gateway.SNMPTrapPort1;
        DBObject.SNMPInterface2Active = gateway.SNMPInterface2Active;
        DBObject.SNMPInterfaceAddress2 = gateway.SNMPInterfaceAddress2.ToStringSafe();
        DBObject.SNMPInterfacePort2 = gateway.SNMPInterfacePort2;
        DBObject.SNMPTrap2Active = gateway.SNMPTrap2Active;
        DBObject.SNMPTrapPort2 = gateway.SNMPTrapPort2;
        DBObject.SNMPInterface3Active = gateway.SNMPInterface3Active;
        DBObject.SNMPInterfaceAddress3 = gateway.SNMPInterfaceAddress3.ToStringSafe();
        DBObject.SNMPInterfacePort3 = gateway.SNMPInterfacePort3;
        DBObject.SNMPTrap3Active = gateway.SNMPTrap3Active;
        DBObject.SNMPTrapPort3 = gateway.SNMPTrapPort3;
        DBObject.SNMPInterface4Active = gateway.SNMPInterface4Active;
        DBObject.SNMPInterfaceAddress4 = gateway.SNMPInterfaceAddress4.ToStringSafe();
        DBObject.SNMPInterfacePort4 = gateway.SNMPInterfacePort4;
        DBObject.SNMPTrap4Active = gateway.SNMPTrap4Active;
        DBObject.SNMPTrapPort4 = gateway.SNMPTrapPort4;
        DBObject.Save();
        return (ActionResult) this.Content("Success!<script type=\"text/javascript\">//hideModal();</script>");
      }
    }
    catch (Exception ex)
    {
      ex.Log("CSNetController.GatewayInterfaceSNMP : Post GatewayID=" + id.ToString());
    }
    return (ActionResult) this.View((object) gateway);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Reform(long id, string url)
  {
    Gateway DBObject = Gateway.Load(id);
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    try
    {
      if (DBObject != null && DBObject.GatewayType != null)
      {
        Account account = Account.Load(csNet.AccountID);
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Reformed gateway");
        if (DBObject.GatewayType.SupportsRemoteNetworkReset)
          DBObject.SendResetNetworkRequest = true;
        DBObject.Save();
        // ISSUE: reference to a compiler-generated field
        if (CSNetController.\u003C\u003Eo__25.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CSNetController.\u003C\u003Eo__25.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = CSNetController.\u003C\u003Eo__25.\u003C\u003Ep__0.Target((CallSite) CSNetController.\u003C\u003Eo__25.\u003C\u003Ep__0, this.ViewBag, "Gateway Reform Pending");
        // ISSUE: reference to a compiler-generated field
        if (CSNetController.\u003C\u003Eo__25.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CSNetController.\u003C\u003Eo__25.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = CSNetController.\u003C\u003Eo__25.\u003C\u003Ep__1.Target((CallSite) CSNetController.\u003C\u003Eo__25.\u003C\u003Ep__1, this.ViewBag, this.IsLocalUrl(url) ? url : "/CSNet/GatewayEdit/" + id.ToString());
        return (ActionResult) this.PartialView("EditConfirmation");
      }
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
    }
    return (ActionResult) this.Content("Reform network command failed!");
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult GatewayFirmwareUpdate(long id, string url)
  {
    Gateway DBObject = Gateway.Load(id);
    if (DBObject == null || DBObject.GatewayType == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    try
    {
      bool flag = DBObject.GatewayType.SupportsOTASuite;
      if (!flag)
        flag = !string.IsNullOrWhiteSpace(DBObject.GatewayType.LatestGatewayPath) && !string.IsNullOrEmpty(DBObject.GatewayType.LatestGatewayVersion);
      if (flag)
      {
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, csNet.AccountID, "Updated gateway firmware");
        DBObject.ForceToBootloader = true;
        if (DBObject.IsUnlocked && DBObject.GatewayType.SupportsHostAddress)
          DBObject.SendUnlockRequest = true;
        DBObject.Save();
        // ISSUE: reference to a compiler-generated field
        if (CSNetController.\u003C\u003Eo__26.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CSNetController.\u003C\u003Eo__26.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = CSNetController.\u003C\u003Eo__26.\u003C\u003Ep__0.Target((CallSite) CSNetController.\u003C\u003Eo__26.\u003C\u003Ep__0, this.ViewBag, "Gateway firmware Update Pending");
        // ISSUE: reference to a compiler-generated field
        if (CSNetController.\u003C\u003Eo__26.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CSNetController.\u003C\u003Eo__26.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = CSNetController.\u003C\u003Eo__26.\u003C\u003Ep__1.Target((CallSite) CSNetController.\u003C\u003Eo__26.\u003C\u003Ep__1, this.ViewBag, this.IsLocalUrl(url) ? url : "/CSNet/GatewayEdit/" + id.ToString());
        return (ActionResult) this.PartialView("EditConfirmation");
      }
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
    }
    return (ActionResult) this.Content("Firmware update command failed!");
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Reset(long id, string url)
  {
    Gateway DBObject = Gateway.Load(id);
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    try
    {
      if (DBObject.ResetToDefault(false))
      {
        Account account = Account.Load(csNet.AccountID);
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Reset gateway");
        DBObject.Save();
        // ISSUE: reference to a compiler-generated field
        if (CSNetController.\u003C\u003Eo__27.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CSNetController.\u003C\u003Eo__27.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Response", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = CSNetController.\u003C\u003Eo__27.\u003C\u003Ep__0.Target((CallSite) CSNetController.\u003C\u003Eo__27.\u003C\u003Ep__0, this.ViewBag, "Gateway Reset Pending");
        // ISSUE: reference to a compiler-generated field
        if (CSNetController.\u003C\u003Eo__27.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CSNetController.\u003C\u003Eo__27.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "returnConfirmationURL", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = CSNetController.\u003C\u003Eo__27.\u003C\u003Ep__1.Target((CallSite) CSNetController.\u003C\u003Eo__27.\u003C\u003Ep__1, this.ViewBag, this.IsLocalUrl(url) ? url : "/CSNet/GatewayEdit/" + id.ToString());
        return (ActionResult) this.PartialView("EditConfirmation");
      }
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
    }
    return (ActionResult) this.Content("Reset gateway command failed!");
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ResetDefaults(long id)
  {
    Gateway DBObject = Gateway.Load(id);
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    try
    {
      if (DBObject.ResetToDefault(false))
      {
        Account account = Account.Load(csNet.AccountID);
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Reset gateway");
        DBObject.Save();
        return (ActionResult) this.Content("Gateway Reset Pending");
      }
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
    }
    return (ActionResult) this.Content("Reset gateway command failed!");
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ResetCustomCompanyDefaults(long id, long customCompanyID)
  {
    Gateway DBObject = Gateway.Load(id);
    CSNet csNet = CSNet.Load(DBObject.CSNetID);
    try
    {
      if (DBObject.SetCustomCompanyDefaults(false))
      {
        Account account = Account.Load(csNet.AccountID);
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Reset gateway to custom company defaults");
        DBObject.Save();
        return (ActionResult) this.Content("Gateway Reset Pending");
      }
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
    }
    return (ActionResult) this.Content("Reset gateway command failed!");
  }

  [Authorize]
  public ActionResult ManageCSNet(long id)
  {
    CSNet model = CSNet.Load(id);
    return !MonnitSession.IsAuthorizedForAccount(model.AccountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Overview",
      controller = "Account"
    }) : (ActionResult) this.View((object) model);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult SetSensorActive(long id, bool active)
  {
    try
    {
      Sensor DBObject1 = Sensor.Load(id);
      if (DBObject1 == null || !MonnitSession.IsAuthorizedForAccount(DBObject1.AccountID))
        return (ActionResult) this.Content($"SensorID: {id} could not be updated");
      DBObject1.IsActive = active;
      if (DBObject1.IsActive)
      {
        Account account = Account.Load(CSNet.Load(DBObject1.CSNetID).AccountID);
        DBObject1.LogAuditData(eAuditAction.Related_Modify, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited sensor active status");
        DBObject1.MarkClean(false);
        if (DBObject1.IsWiFiSensor)
        {
          Gateway DBObject2 = Gateway.LoadBySensorID(DBObject1.SensorID);
          DBObject2.LogAuditData(eAuditAction.Related_Modify, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited wifi sensor active status");
          DBObject2.MarkClean(false);
          DBObject2.Save();
        }
      }
      DBObject1.Save();
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content($"SensorID: {id} could not be updated");
    }
    return (ActionResult) this.Content("Success!");
  }

  [Authorize]
  public ActionResult SetAllSensorsActive(long id)
  {
    CSNet csNet = CSNet.Load(id);
    List<Sensor> sensorList = this.GetSensorList(id);
    try
    {
      if (csNet == null || !MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
        return (ActionResult) this.Content("Not Authorized");
      foreach (Sensor sensor in sensorList)
      {
        sensor.IsActive = true;
        sensor.MarkClean(false);
        sensor.Save();
      }
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content(ex.Message);
    }
    return (ActionResult) this.Content("Success!");
  }

  [Authorize]
  public ActionResult MoveSensor(long id, long sensorID)
  {
    this.ViewData[nameof (MoveSensor)] = (object) true;
    try
    {
      if (!CSNetController.TryMoveSensor(id, sensorID))
        return (ActionResult) this.Content($"SensorID: {sensorID} could not be transfered to new network");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content($"SensorID: {sensorID} could not be transfered to new network");
    }
    return (ActionResult) this.Content("Success");
  }

  public static bool TryMoveSensor(long id, long sensorID)
  {
    CSNet csNet = CSNet.Load(id);
    Sensor sensor = Sensor.Load(sensorID);
    long accountId = sensor.AccountID;
    if (sensor == null || csNet == null || sensor != null && sensor.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeNetwork(csNet.CSNetID) && csNet.CSNetID != ConfigData.AppSettings("DefaultCSNetID").ToLong() || sensor.Network != null && !sensor.Network.HoldingOnlyNetwork && sensor.Network.CSNetID != ConfigData.AppSettings("DefaultCSNetID").ToLong() && !MonnitSession.CurrentCustomer.CanSeeNetwork(sensor.CSNetID))
      return false;
    Account account = Account.Load(csNet.AccountID);
    if (csNet.CSNetID != ConfigData.AppSettings("DefaultCSNetID").ToLong() && account.AccountID != accountId && Sensor.LoadByAccountID(account.AccountID).Count >= account.CurrentSubscription.AccountSubscriptionType.AllowedSensors)
      return false;
    eProgramLevel o = MonnitSession.ProgramLevel();
    if (ThemeController.SensorCount() > o.ToInt())
      return false;
    NetworkAudit.LogNetworkChange(MonnitSession.CurrentCustomer.CustomerID, sensor);
    foreach (BaseDBObject baseDbObject in ExternalDataSubscription.LoadBySensorID(sensorID).ToArray())
      baseDbObject.Delete();
    sensor.AccountID = account.AccountID;
    sensor.CSNetID = id;
    sensor.IsActive = true;
    sensor.IsNewToNetwork = true;
    TimedCache.RemoveObject("SensorCount");
    MonnitSession.AccountSensorTotal = int.MinValue;
    if (account.AccountID != accountId)
    {
      sensor.LastDataMessageGUID = Guid.Empty;
      sensor.StartDate = DateTime.UtcNow;
    }
    sensor.Save();
    sensor.ResetLastCommunicationDate();
    Gateway gateway = Gateway.LoadBySensorID(sensorID);
    if (gateway != null)
    {
      if (account.AccountID != accountId)
      {
        foreach (Notification notification in Notification.LoadByGatewayID(gateway.GatewayID))
          notification.RemoveGateway(gateway);
      }
      gateway.CSNetID = id;
      gateway.Save();
    }
    if (account.AccountID != accountId)
    {
      foreach (Notification notification in Notification.LoadBySensorID(sensor.SensorID))
        notification.RemoveSensor(sensor);
    }
    return true;
  }

  [Authorize]
  public ActionResult AssignSensorSuccess(long id, long? sensorID)
  {
    CSNet csNet = CSNet.Load(id);
    // ISSUE: reference to a compiler-generated field
    if (CSNetController.\u003C\u003Eo__35.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CSNetController.\u003C\u003Eo__35.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NetworkID", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = CSNetController.\u003C\u003Eo__35.\u003C\u003Ep__0.Target((CallSite) CSNetController.\u003C\u003Eo__35.\u003C\u003Ep__0, this.ViewBag, csNet.CSNetID);
    // ISSUE: reference to a compiler-generated field
    if (CSNetController.\u003C\u003Eo__35.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CSNetController.\u003C\u003Eo__35.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = CSNetController.\u003C\u003Eo__35.\u003C\u003Ep__1.Target((CallSite) CSNetController.\u003C\u003Eo__35.\u003C\u003Ep__1, this.ViewBag, csNet.AccountID);
    return (ActionResult) this.View();
  }

  [Authorize]
  public ActionResult CheckGatewayMove(long id)
  {
    if (!MonnitSession.CustomerCan("Gateway_Can_Change_Network"))
      return (ActionResult) this.Content("alert('User not authorized to move gateway');");
    try
    {
      Gateway DBObject = Gateway.Load(id);
      if (DBObject == null)
      {
        try
        {
          if (DBObject == null)
          {
            DBObject = CSNetControllerBase.LookUpGateway(MonnitSession.CurrentCustomer.AccountID, id, "IM" + MonnitUtil.CheckDigit(id));
            if (DBObject == null || DBObject.IsDeleted)
              return (ActionResult) this.Content($"alert('GatewayID: {id} could not be found');");
            DBObject.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
            DBObject.ForceInsert();
          }
        }
        catch
        {
          return (ActionResult) this.Content($"alert('GatewayID: {id} could not be assigned');");
        }
      }
      CSNet csNet = CSNet.Load(DBObject.CSNetID);
      if (DBObject == null || DBObject.IsDeleted || csNet != null && !MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
        return (ActionResult) this.Content($"alert('GatewayID: {id} could not be found');");
      string content = "$.get('/CSNet/MoveGateway/' + $('#CSNetID').val() + '?gatewayID=' + $('#QuickGatewayID').val(), function(data) { if (data != 'Success') { $('#GatewayMoveResult').html(data); } else { window.location.href = '/Account/NetworkSettings/' + $('#AccountID').val() + '?networkID=' + $('#CSNetID').val() + '&result=GatewayAdded'; } });";
      long num = ConfigData.AppSettings("DefaultCSNetID").ToLong();
      if (DBObject.CSNetID == num || csNet == null)
        return (ActionResult) this.Content(content);
      Account account = Account.Load(csNet.AccountID);
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Moved gateway to different network");
      return (ActionResult) this.Content($"if(confirm('This gateway currently belongs to Account: {account.CompanyName.Replace("'", "\\'")} Network: {(csNet == null ? (object) "Deleted" : (object) csNet.Name.Replace("'", "\\'"))}')) {{ {content} }}");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content($"alert('GatewayID: {id} could not be transfered to new network');");
    }
  }

  [Authorize]
  public ActionResult MoveGateway(long id, long gatewayID)
  {
    try
    {
      Gateway gateway = Gateway.Load(gatewayID);
      if (!CSNetController.TryMoveGateway(id, gateway))
        return (ActionResult) this.Content($"GatewayID: {gatewayID} could not be transfered to new network");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content($"GatewayID: {gatewayID} could not be transfered to new network");
    }
    return (ActionResult) this.Content("Success");
  }

  public static bool TryMoveGateway(long id, Gateway gateway)
  {
    CSNet csNet1 = CSNet.Load(id);
    CSNet csNet2 = CSNet.Load(gateway.CSNetID);
    foreach (BaseDBObject baseDbObject in ExternalDataSubscription.LoadByGatewayID(gateway.GatewayID).ToArray())
      baseDbObject.Delete();
    if (csNet1 == null || gateway == null || csNet1.CSNetID != ConfigData.AppSettings("DefaultCSNetID").ToLong() && !MonnitSession.CurrentCustomer.CanSeeNetwork(csNet1.CSNetID) || gateway != null && gateway.IsDeleted || csNet2 != null && !csNet2.HoldingOnlyNetwork && gateway.CSNetID != ConfigData.AppSettings("DefaultCSNetID").ToLong() && !MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID))
      return false;
    NetworkAudit.LogNetworkChange(MonnitSession.CurrentCustomer.CustomerID, gateway, new long?());
    if (gateway.SensorID > 0L)
    {
      Sensor sensor = Sensor.Load(gateway.SensorID);
      sensor.AccountID = csNet1.AccountID;
      sensor.CSNetID = id;
      sensor.IsActive = true;
      sensor.IsNewToNetwork = true;
      if (csNet1.AccountID != csNet2.AccountID)
      {
        sensor.LastCommunicationDate = DateTime.MinValue;
        sensor.LastDataMessageGUID = Guid.Empty;
        sensor.StartDate = DateTime.UtcNow;
      }
      sensor.Save();
      sensor.ResetLastCommunicationDate();
      if (csNet2 == null || csNet1.AccountID != csNet2.AccountID)
      {
        foreach (Notification notification in Notification.LoadBySensorID(sensor.SensorID))
          notification.RemoveSensor(sensor);
      }
    }
    gateway.CSNetID = id;
    gateway.LastCommunicationDate = DateTime.MinValue;
    gateway.StartDate = DateTime.UtcNow;
    gateway.Save();
    if (csNet2 == null || csNet1.AccountID != csNet2.AccountID)
    {
      foreach (Notification notification in GatewayNotification.LoadByGatewayID(gateway.GatewayID))
        notification.RemoveGateway(gateway);
    }
    return true;
  }

  public static bool TryRemoveGateway(Gateway gateway)
  {
    try
    {
      CSNet csNet = CSNet.Load(gateway.CSNetID);
      if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID) || gateway != null && gateway.IsDeleted)
        return false;
      NetworkAudit.LogNetworkChange(MonnitSession.CurrentCustomer.CustomerID, gateway, new long?(csNet.AccountID));
      Account account = Account.Load(csNet.AccountID);
      gateway.LogAuditData(eAuditAction.Delete, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Removed gateway from network");
      foreach (Notification notification in GatewayNotification.LoadByGatewayID(gateway.GatewayID))
        notification.RemoveGateway(gateway);
      if (gateway.SensorID > 0L)
      {
        Sensor sensor = Sensor.Load(gateway.SensorID);
        sensor.LogAuditData(eAuditAction.Delete, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Removed sensor from network");
        sensor.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
        sensor.LastCommunicationDate = DateTime.MinValue;
        sensor.LastDataMessageGUID = Guid.Empty;
        sensor.Save();
        sensor.ResetLastCommunicationDate();
        foreach (Notification notification in Notification.LoadBySensorID(sensor.SensorID))
          notification.RemoveSensor(sensor);
      }
      gateway.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
      gateway.ResetToDefault(false);
      gateway.Save();
      return true;
    }
    catch
    {
    }
    return false;
  }

  [Authorize]
  public ActionResult AssignGateway(long id, long? networkID, long? gatewayID, string code)
  {
    AssignObjectModel model = new AssignObjectModel()
    {
      AccountID = id
    };
    model.NetworkID = networkID ?? (model.Networks.Count == 1 ? model.Networks.First<CSNet>().CSNetID : 0L);
    model.ObjectID = gatewayID.GetValueOrDefault();
    model.Code = code;
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult AssignGateway(long id, AssignObjectModel model)
  {
    if (model.NetworkID < 1L)
      this.ModelState.AddModelError("NetworkID", "Network is required");
    if (!MonnitUtil.ValidateCheckDigit(model.ObjectID, model.Code))
      this.ModelState.AddModelError("Code", "Input does not match.");
    CSNet csNet1 = CSNet.Load(model.NetworkID);
    if (csNet1 == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(csNet1.CSNetID))
      this.ModelState.AddModelError("ObjectID", $"GatewayID: {model.ObjectID} could not be transfered to new network");
    Gateway gateway = Gateway.Load(model.ObjectID);
    if (gateway != null && gateway.IsDeleted)
      this.ModelState.AddModelError("ObjectID", $"GatewayID: {model.ObjectID} not authorized to transfer.");
    if (this.ModelState.IsValid)
    {
      try
      {
        if (gateway == null)
        {
          gateway = CSNetControllerBase.LookUpGateway(model);
          if (gateway != null)
          {
            gateway.CSNetID = csNet1.CSNetID;
            gateway.ForceInsert();
          }
          else
            this.ModelState.AddModelError("ObjectID", $"SensorID: {model.ObjectID} could not be transfered to new network");
        }
      }
      catch
      {
      }
      if (gateway != null)
      {
        CSNet csNet2 = CSNet.Load(gateway.CSNetID);
        if (gateway.CSNetID == long.MinValue || csNet2.HoldingOnlyNetwork || gateway.CSNetID == ConfigData.AppSettings("DefaultCSNetID").ToLong() || MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID))
        {
          long? oldAccountID = new long?();
          if (csNet2 != null && csNet1.AccountID != csNet2.AccountID)
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
            Sensor sensor = Sensor.Load(gateway.SensorID) ?? CSNetControllerBase.LookUpSensor(model, sensor);
            if (sensor != null)
            {
              eProgramLevel o = MonnitSession.ProgramLevel();
              if (ThemeController.SensorCount() < o.ToInt())
              {
                sensor.AccountID = csNet1.AccountID;
                sensor.CSNetID = csNet1.CSNetID;
                sensor.IsActive = true;
                sensor.IsNewToNetwork = true;
                if (csNet1.AccountID != csNet2.AccountID)
                {
                  sensor.LastCommunicationDate = DateTime.MinValue;
                  sensor.LastDataMessageGUID = Guid.Empty;
                  sensor.StartDate = DateTime.UtcNow;
                }
                Account account = Account.Load(csNet2.AccountID);
                sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Assign sensor to existing network");
                flag1 = true;
                sensor.Save();
                TimedCache.RemoveObject("SensorCount");
                sensor.ResetLastCommunicationDate();
                if (csNet2 == null || csNet1.AccountID != csNet2.AccountID)
                {
                  foreach (Notification notification in Notification.LoadBySensorID(sensor.SensorID))
                    notification.RemoveSensor(sensor);
                }
              }
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
          if (CSNetController.\u003C\u003Eo__41.\u003C\u003Ep__0 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CSNetController.\u003C\u003Eo__41.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NetworkID", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj1 = CSNetController.\u003C\u003Eo__41.\u003C\u003Ep__0.Target((CallSite) CSNetController.\u003C\u003Eo__41.\u003C\u003Ep__0, this.ViewBag, model.NetworkID);
          // ISSUE: reference to a compiler-generated field
          if (CSNetController.\u003C\u003Eo__41.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            CSNetController.\u003C\u003Eo__41.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj2 = CSNetController.\u003C\u003Eo__41.\u003C\u003Ep__1.Target((CallSite) CSNetController.\u003C\u003Eo__41.\u003C\u003Ep__1, this.ViewBag, csNet1.AccountID);
          return (ActionResult) this.RedirectToRoute("Default", (object) new
          {
            action = "AssignGatewaySuccess",
            controller = "Overview",
            id = model.NetworkID,
            gatewayID = gateway.GatewayID
          });
        }
        this.ModelState.AddModelError("ObjectID", $"GatewayID: {model.ObjectID} not authorized to transfer.");
      }
    }
    else
      this.ModelState.AddModelError("ObjectID", $"GatewayID: {model.ObjectID} could not be transfered to new network");
    model.AccountID = id;
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult AssignGatewaySuccess(long id, long? gatewayID)
  {
    CSNet csNet = CSNet.Load(id);
    // ISSUE: reference to a compiler-generated field
    if (CSNetController.\u003C\u003Eo__42.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CSNetController.\u003C\u003Eo__42.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NetworkID", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = CSNetController.\u003C\u003Eo__42.\u003C\u003Ep__0.Target((CallSite) CSNetController.\u003C\u003Eo__42.\u003C\u003Ep__0, this.ViewBag, csNet.CSNetID);
    // ISSUE: reference to a compiler-generated field
    if (CSNetController.\u003C\u003Eo__42.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CSNetController.\u003C\u003Eo__42.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = CSNetController.\u003C\u003Eo__42.\u003C\u003Ep__1.Target((CallSite) CSNetController.\u003C\u003Eo__42.\u003C\u003Ep__1, this.ViewBag, csNet.AccountID);
    return (ActionResult) this.View();
  }

  [Authorize]
  public ActionResult MoveAll(long id, long csnetID)
  {
    if (!MonnitSession.CustomerCan("Sensor_Can_Change_Network") || !MonnitSession.CustomerCan("Gateway_Can_Change_Network"))
      return (ActionResult) this.Content("");
    try
    {
      List<Sensor> sensorList = Sensor.LoadByCsNetID(id);
      List<Gateway> gatewayList = Gateway.LoadByCSNetID(id);
      CSNet csNet = CSNet.Load(csnetID);
      foreach (Sensor sens in sensorList)
      {
        NetworkAudit.LogNetworkChange(MonnitSession.CurrentCustomer.CustomerID, sens);
        sens.CSNetID = csNet.CSNetID;
        sens.AccountID = csNet.AccountID;
        sens.Save();
      }
      foreach (Gateway gateway in gatewayList)
      {
        NetworkAudit.LogNetworkChange(MonnitSession.CurrentCustomer.CustomerID, gateway, new long?());
        gateway.CSNetID = csNet.CSNetID;
        gateway.Save();
      }
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content("alert('Error transferring to new network')");
    }
    return (ActionResult) this.Content("window.location.href = window.location.href;");
  }

  [Authorize]
  public ActionResult CheckNetworkMove(long id)
  {
    try
    {
      CSNet csNet = CSNet.Load(id);
      if (csNet == null || !MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
        return (ActionResult) this.Content($"alert('NetworkID: {id} could not be transfered to new account');");
      string str = "$.get('/CSNet/MoveNetwork/' + $('#AccountID').val() + '?csnetID=' + $('#QuickCSNetID').val(), function(data) { eval(data); });";
      return (ActionResult) this.Content($"if(confirm('This network currently belongs to Account: {Account.Load(csNet.AccountID).CompanyName}')) {{ {str} }}");
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content($"alert('NetworkID: {id} could not be transfered to new account');");
    }
  }

  [Authorize]
  public ActionResult MoveNetwork(long id, long csnetID)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    CSNet existingNetwork = CSNet.Load(csnetID.ToLong());
    if (existingNetwork == null)
      return (ActionResult) this.Content("alert('Invalid Network ID - Not found in the system.');");
    if (!MonnitSession.IsAuthorizedForAccount(existingNetwork.AccountID))
      return (ActionResult) this.Content("alert('Invalid Network ID - it is assigned to another account.');");
    this.AssignNetwork(id, existingNetwork);
    return (ActionResult) this.Content($"csNetSuccess('{csnetID}','{existingNetwork.Name}');");
  }

  [Authorize]
  public ActionResult CreateNetwork(long id)
  {
    Account account = Account.Load(id);
    if (account == null || !MonnitSession.CustomerCan("Network_Create"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    return CSNet.LoadByAccountID(account.AccountID).Count > 0 && !account.CurrentSubscription.Can(Feature.Find("multiple_networks")) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Overview",
      controller = "Account"
    }) : (ActionResult) this.View();
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult CreateNetwork(long id, CreateNetworkModel model)
  {
    if (CSNet.LoadByAccountID(id).Count >= ConfigData.AppSettings("MaxNetworkCount", "10").ToInt() && !MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.View("Unauthorized");
    if (!this.ModelState.IsValid)
      return (ActionResult) this.View((object) model);
    CSNet DBObject = new CSNet();
    DBObject.AccountID = id;
    DBObject.Name = model.Name;
    DBObject.SendNotifications = true;
    DBObject.Save();
    long permissionTypeId = CustomerPermissionType.Find("Network_View").CustomerPermissionTypeID;
    Account account1 = Account.Load(DBObject.AccountID);
    foreach (Customer customer in Customer.LoadAllByAccount(DBObject.AccountID))
    {
      if (customer.IsAdmin || customer.CustomerID == account1.PrimaryContactID)
        new CustomerPermission()
        {
          CSNetID = DBObject.CSNetID,
          CustomerID = customer.CustomerID,
          CustomerPermissionTypeID = permissionTypeId,
          Can = true
        }.Save();
    }
    Account account2 = Account.Load(DBObject.AccountID);
    DBObject.LogAuditData(eAuditAction.Create, eAuditObject.Network, MonnitSession.CurrentCustomer.CustomerID, account2.AccountID, "Created new network");
    MonnitSession.CurrentCustomer.ClearPermissions();
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "AssignGateway",
      controller = "Overview",
      id = DBObject.AccountID,
      networkID = DBObject.CSNetID
    });
  }

  [Authorize]
  public ActionResult Create(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && !MonnitSession.CustomerCan("Network_Create"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    CSNet csNet = new CSNet() { AccountID = id };
    csNet.Name = $"Network ({csNet.CSNetID})";
    csNet.SendNotifications = true;
    csNet.Save();
    long permissionTypeId = CustomerPermissionType.Find("Network_View").CustomerPermissionTypeID;
    Account account = Account.Load(csNet.AccountID);
    foreach (Customer customer in Customer.LoadAllByAccount(csNet.AccountID))
    {
      if (customer.IsAdmin || customer.CustomerID == account.PrimaryContactID)
        new CustomerPermission()
        {
          CSNetID = csNet.CSNetID,
          CustomerID = customer.CustomerID,
          CustomerPermissionTypeID = permissionTypeId,
          Can = true
        }.Save();
    }
    MonnitSession.CurrentCustomer.ClearPermissions();
    return (ActionResult) this.Content($"csNetSuccess('{csNet.CSNetID}','{csNet.Name}');");
  }

  [Authorize]
  public ActionResult GatewayCreate(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    Gateway model = new Gateway();
    model.CSNetID = id;
    model.GatewayID = 0L;
    model.RadioBand = "";
    model.APNFirmwareVersion = ConfigData.FindValue("DefaultFirmwareVersion");
    model.GatewayFirmwareVersion = " ";
    // ISSUE: reference to a compiler-generated field
    if (CSNetController.\u003C\u003Eo__49.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CSNetController.\u003C\u003Eo__49.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = CSNetController.\u003C\u003Eo__49.\u003C\u003Ep__0.Target((CallSite) CSNetController.\u003C\u003Eo__49.\u003C\u003Ep__0, this.ViewBag, CSNet.Load(id).AccountID);
    // ISSUE: reference to a compiler-generated field
    if (CSNetController.\u003C\u003Eo__49.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CSNetController.\u003C\u003Eo__49.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, SelectList, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "PowerSourceList", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = CSNetController.\u003C\u003Eo__49.\u003C\u003Ep__1.Target((CallSite) CSNetController.\u003C\u003Eo__49.\u003C\u003Ep__1, this.ViewBag, new SelectList((IEnumerable) PowerSource.LoadAll(), "PowerSourceID", "Name"));
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult GatewayCreate(Gateway model)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    if (string.IsNullOrEmpty(model.Name))
      this.ModelState.AddModelError("Name", "Name field is required.");
    if (string.IsNullOrEmpty(model.APNFirmwareVersion))
      this.ModelState.AddModelError("APNFirmwareVersion", "APNFirmwareVersion field is required.");
    if (string.IsNullOrEmpty(model.GatewayFirmwareVersion))
      this.ModelState.AddModelError("GatewayFirmwareVersion", "GatewayFirmwareVersion field is required. Default value has been set.");
    GatewayType gatewayType = GatewayType.Load(model.GatewayTypeID);
    if (string.IsNullOrEmpty(model.GatewayFirmwareVersion))
      model.GatewayFirmwareVersion = gatewayType.LatestGatewayVersion;
    model.RadioBand = model.RadioBand.ToStringSafe().Replace("_", " ").Trim();
    if (this.ModelState.IsValid)
    {
      if (Gateway.Load(model.GatewayID) != null)
      {
        this.ModelState.AddModelError("", "Invalid Access Point ID - it already exists in the Database.");
      }
      else
      {
        try
        {
          Gateway DBObject = new Gateway();
          DBObject.GatewayID = model.GatewayID;
          DBObject.Name = model.Name;
          DBObject.CSNetID = model.CSNetID;
          DBObject.GatewayTypeID = model.GatewayTypeID;
          DBObject.GatewayFirmwareVersion = model.GatewayFirmwareVersion;
          DBObject.RadioBand = model.RadioBand;
          DBObject.APNFirmwareVersion = model.APNFirmwareVersion;
          DBObject.MacAddress = model.MacAddress;
          DBObject.ResetToDefault(false);
          DBObject.IsDirty = false;
          DBObject.ForceInsert();
          Account account = Account.Load(CSNet.Load(DBObject.CSNetID).AccountID);
          DBObject.LogAuditData(eAuditAction.Related_Modify, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Created a new gateway");
          return (ActionResult) this.Content("Success!<script type=\"text/javascript\">window.location.href = window.location.href</script>");
        }
        catch (Exception ex)
        {
          ex.Log("CSNetController.GatewayCreate");
          if (ex.Message.ToLower().Contains("primary key"))
            this.ModelState.AddModelError("", "Access Point ID - it may already exist in the Database.");
          else
            this.ModelState.AddModelError("", "Unable to Save");
        }
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (CSNetController.\u003C\u003Eo__50.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CSNetController.\u003C\u003Eo__50.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = CSNetController.\u003C\u003Eo__50.\u003C\u003Ep__0.Target((CallSite) CSNetController.\u003C\u003Eo__50.\u003C\u003Ep__0, this.ViewBag, CSNet.Load(model.CSNetID).AccountID);
    // ISSUE: reference to a compiler-generated field
    if (CSNetController.\u003C\u003Eo__50.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CSNetController.\u003C\u003Eo__50.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, SelectList, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "PowerSourceList", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = CSNetController.\u003C\u003Eo__50.\u003C\u003Ep__1.Target((CallSite) CSNetController.\u003C\u003Eo__50.\u003C\u003Ep__1, this.ViewBag, new SelectList((IEnumerable) PowerSource.LoadAll(), "PowerSourceID", "Name"));
    return (ActionResult) this.View((object) model);
  }

  [Authorize]
  public ActionResult ReadyToShip(long id)
  {
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.Content("Not Authorized!");
    try
    {
      CSNet csNet = CSNet.Load(id);
      csNet.SendNotifications = false;
      csNet.Save();
      foreach (Sensor sensor in this.GetSensorList(id))
        sensor.ReadyToShip();
      return (ActionResult) this.Content("Success!");
    }
    catch (Exception ex)
    {
      ex.Log("CSNetController.ReadyToShip");
      return (ActionResult) this.Content("Failed!");
    }
  }

  [Authorize]
  [NoCache]
  public ActionResult AlertNotificationsAreOff()
  {
    if (this.TempData["networks"] is List<CSNet> model)
      return (ActionResult) this.View((object) model);
    return MonnitSession.CurrentCustomer.Account.PrimaryContactID == MonnitSession.CurrentCustomer.CustomerID && this.Request.UrlReferrer != (Uri) null && this.Request.UrlReferrer.LocalPath.ToLower().Contains("logon") ? (ActionResult) this.View((object) CSNet.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID)) : (ActionResult) this.Content("<script>hideModal();</script>");
  }

  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult AlertNotificationsAreOff(FormCollection collection)
  {
    bool flag = collection["silence"].Split(',')[0].ToBool();
    foreach (string allKey in collection.AllKeys)
    {
      CSNet DBObject = CSNet.Load(allKey.ToLong());
      if (DBObject != null)
      {
        DBObject.SendNotifications = (collection[allKey].Split(',')[0].ToBool() ? 1 : 0) != 0;
        if (flag)
          DBObject.AlertNotificationsAreOff = false;
        Account account = Account.Load(DBObject.AccountID);
        DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Network, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Toggled notification alert status for network");
        DBObject.Save();
      }
    }
    return (ActionResult) this.Content("<script>hideModal();</script>");
  }

  [Authorize]
  [HttpGet]
  public ActionResult ExportGateway(long id)
  {
    Gateway model = Gateway.Load(id);
    if (model == null)
      return (ActionResult) this.Content("Unauthorized");
    CSNet csNet = CSNet.Load(model.CSNetID);
    return csNet == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(csNet.CSNetID) ? (ActionResult) this.Content("Unauthorized") : (ActionResult) this.PartialView((object) model);
  }

  [NoCache]
  [Authorize]
  [HttpGet]
  public ActionResult Export(long id)
  {
    Gateway model = Gateway.Load(id);
    if (model == null)
      return (ActionResult) this.Content("Unathorized");
    CSNet csNet = CSNet.Load(model.CSNetID);
    return csNet == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(csNet.CSNetID) ? (ActionResult) this.Content("Unathorized") : (ActionResult) this.PartialView((object) model);
  }

  [NoCache]
  [Authorize]
  public ActionResult ExportGatewayData(long id)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    foreach (string allKey in this.Request.QueryString.AllKeys)
      dictionary.Add(allKey, this.Request.QueryString[allKey]);
    dictionary.Remove("uxExportAll");
    dictionary.Remove(nameof (id));
    // ISSUE: reference to a compiler-generated field
    if (CSNetController.\u003C\u003Eo__56.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CSNetController.\u003C\u003Eo__56.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Dictionary<string, string>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Dictionary", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = CSNetController.\u003C\u003Eo__56.\u003C\u003Ep__0.Target((CallSite) CSNetController.\u003C\u003Eo__56.\u003C\u003Ep__0, this.ViewBag, dictionary);
    Gateway model = Gateway.Load(id);
    if (model == null)
      return (ActionResult) this.Content("Unathorized");
    CSNet csNet = CSNet.Load(model.CSNetID);
    return csNet == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(csNet.CSNetID) ? (ActionResult) this.Content("Unauthorized") : (ActionResult) this.View((object) model);
  }

  [Authorize]
  [NoCache]
  public ActionResult ExternalConfiguration()
  {
    ExternalDataSubscription model = new ExternalDataSubscription()
    {
      AccountID = MonnitSession.CurrentCustomer.AccountID
    };
    using (List<ExternalDataSubscription>.Enumerator enumerator = ExternalDataSubscription.LoadAllByAccountID(MonnitSession.CurrentCustomer.AccountID).GetEnumerator())
    {
      if (enumerator.MoveNext())
        model = enumerator.Current;
    }
    return (ActionResult) this.View((object) model);
  }

  [NoCache]
  [Authorize]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ExternalConfiguration(FormCollection collection)
  {
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.Load(collection["ExternalDataSubscriptionID"].ToLong()) ?? new ExternalDataSubscription();
    if (!this.ModelState.IsValid)
      return (ActionResult) this.Content("Unable to save the external data subscription.");
    dataSubscription.AccountID = MonnitSession.CurrentCustomer.AccountID;
    dataSubscription.ConnectionInfo1 = collection["ConnectionInfo1"];
    dataSubscription.ConnectionInfo2 = collection["ConnectionInfo2"];
    dataSubscription.ContentHeaderType = collection["ContentHeaderType"];
    dataSubscription.ExternalID = collection["ExternalID"];
    dataSubscription.verb = collection["verb"];
    if (dataSubscription.ExternalID == string.Empty)
      dataSubscription.ExternalID = "None";
    dataSubscription.Save();
    return (ActionResult) this.Content("Success");
  }

  [NoCache]
  [Authorize]
  public ActionResult TestExternalConfiguration(long id, string formatUrl, string externalID)
  {
    Gateway gateway = Gateway.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(CSNet.Load(gateway.CSNetID).AccountID))
      return (ActionResult) this.Json((object) new
      {
        result = "Error",
        data = "Not Authorized"
      });
    if (!formatUrl.ToLower().StartsWith("http"))
      return (ActionResult) this.Json((object) new
      {
        result = "Error",
        data = "Please specify http or https"
      });
    GatewayMessage gatewayMessage = GatewayMessage.LoadLastByGateway(gateway.GatewayID);
    try
    {
      return (ActionResult) this.Json((object) new
      {
        result = "Success",
        data = string.Format(formatUrl, (object) gateway.ExternalID, (object) gatewayMessage.GatewayMessageGUID.ToUInt64(), (object) gatewayMessage.MessageType, (object) gatewayMessage.Power, (object) gatewayMessage.Battery, (object) gatewayMessage.ReceivedDate, (object) gatewayMessage.MessageCount, (object) gatewayMessage.MeetsNotificationRequirement, (object) gateway.GatewayID, (object) HttpUtility.UrlEncode(gateway.Name), (object) gateway.MacAddress, (object) gateway.CSNetID, (object) gateway.APNFirmwareVersion, (object) gateway.GatewayFirmwareVersion, (object) gateway.IsDirty, (object) gateway.GatewayTypeID, (object) gateway.CurrentSignalStrength, (object) gatewayMessage.GatewayMessageGUID)
      });
    }
    catch (Exception ex)
    {
      ex.Log("SensorController.TestExternalConfiguration ");
      return (ActionResult) this.Json((object) new
      {
        result = "Error",
        data = "Error building test url"
      });
    }
  }

  [NoCache]
  [Authorize]
  public ActionResult TestExternalConfigurationBody(long id, string formatUrl, string externalID)
  {
    Gateway gateway = Gateway.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(CSNet.Load(gateway.CSNetID).AccountID))
      return (ActionResult) this.Json((object) new
      {
        result = "Error",
        data = "Not Authorized"
      });
    GatewayMessage gatewayMessage = GatewayMessage.LoadLastByGateway(gateway.GatewayID);
    try
    {
      return (ActionResult) this.Json((object) new
      {
        result = "Success",
        data = string.Format(formatUrl, (object) gateway.ExternalID, (object) gatewayMessage.GatewayMessageGUID.ToUInt64(), (object) gatewayMessage.MessageType, (object) gatewayMessage.Power, (object) gatewayMessage.Battery, (object) gatewayMessage.ReceivedDate, (object) gatewayMessage.MessageCount, (object) gatewayMessage.MeetsNotificationRequirement, (object) gateway.GatewayID, (object) HttpUtility.UrlEncode(gateway.Name), (object) gateway.MacAddress, (object) gateway.CSNetID, (object) gateway.APNFirmwareVersion, (object) gateway.GatewayFirmwareVersion, (object) gateway.IsDirty, (object) gateway.GatewayTypeID, (object) gateway.CurrentSignalStrength, (object) gatewayMessage.GatewayMessageGUID)
      });
    }
    catch (Exception ex)
    {
      ex.Log("SensorController.TestExternalConfiguration ");
      return (ActionResult) this.Json((object) new
      {
        result = "Error",
        data = "Error building test url"
      });
    }
  }

  [NoCache]
  [Authorize]
  [HttpGet]
  public ActionResult DeleteExternalConfiguration(long id)
  {
    ExternalDataSubscription.Load(id).Delete();
    return (ActionResult) this.Content("Success");
  }

  public ActionResult GatewayServerSettings()
  {
    return (ActionResult) this.View((object) new AssignObjectModel());
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult GatewayServerSettings(FormCollection collection)
  {
    if (!MonnitUtil.ValidateCheckDigit(collection["ObjectID"].ToLong(), collection["Code"].ToString()))
      this.ModelState.AddModelError("DeviceCode didn't Match", new Exception("DeviceCode didn't Match"));
    if (this.ModelState.IsValid)
    {
      long gatewayId = collection["ObjectID"].ToLong();
      string str = collection["Code"].ToString();
      this.Session["quickEditGatewayID"] = (object) collection["ObjectID"].ToLong();
      Gateway gateway = Gateway.Load(collection["ObjectID"].ToLong());
      if (gateway == null)
      {
        Sensor sensor = Sensor.Load(collection["ObjectID"].ToLong());
        if (sensor != null)
        {
          gateway = Gateway.LoadBySensorID(sensor.SensorID);
          if (gateway == null)
          {
            this.ModelState.AddModelError("Sensor does not have gateway", new Exception("Sensor does not have gateway"));
          }
          else
          {
            gatewayId = gateway.GatewayID;
            str = MonnitUtil.CheckDigit(gatewayId);
            this.Session["quickEditGatewayID"] = (object) gatewayId;
          }
        }
      }
      if (gateway != null)
        return gateway.IsUnlocked ? (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "EditGatewayServerSettings",
          controller = "CSNet",
          gatewayID = gatewayId,
          code = str
        }) : (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "UnlockGateway",
          controller = "CSNet",
          gatewayID = gatewayId
        });
    }
    return (ActionResult) this.View();
  }

  public ActionResult UnlockGateway(long gatewayID)
  {
    return (ActionResult) this.View((object) Gateway.Load(gatewayID));
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult UnlockGateway(FormCollection collection)
  {
    DateTime utcNow = DateTime.UtcNow;
    string uri = "";
    string str1 = ConfigData.AppSettings("MeaApiLogging");
    bool flag1 = !string.IsNullOrWhiteSpace(str1) && bool.Parse(str1);
    string str2;
    try
    {
      Gateway gateway = Gateway.Load(collection["GatewayID"].ToLong());
      if (gateway == null)
      {
        this.ModelState.AddModelError("AuthKey", "Failed: No Gateway Found");
        return (ActionResult) this.View();
      }
      string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{collection["AuthKey"]}|{gateway.GatewayID}|{gateway.GatewayType.Name}"));
      uri = string.Format(ConfigData.FindValue("MEA_API_Location") + "xml/RetreiveActivationKey?key={0}&advancedFailResponse={1}", (object) base64String, (object) true);
      XDocument xdocument = XDocument.Load(uri);
      str2 = xdocument.Descendants((XName) "Result").Single<XElement>().Value;
      bool flag2 = !str2.Contains("Failed");
      if (flag1)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-UnlockGateway",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = xdocument.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      if (flag2)
      {
        try
        {
          long customerID = long.MinValue;
          if (MonnitSession.CurrentCustomer != null)
            customerID = MonnitSession.CurrentCustomer.CustomerID;
          CSNet csNet = CSNet.Load(gateway.CSNetID);
          if (csNet != null)
          {
            Account account = Account.Load(csNet.AccountID);
            gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, customerID, account.AccountID, "Unlocked gateway");
          }
        }
        catch (Exception ex)
        {
          ex.Log("CSNetController.UnlockGateway CFR Audit Log Failed. Message: ");
        }
        gateway.IsUnlocked = true;
        gateway.SendUnlockRequest = true;
        gateway.Save();
        return (ActionResult) this.View("~/Views/CSNet/EditGatewayServerSettings.aspx", (object) gateway);
      }
    }
    catch (Exception ex)
    {
      ex.Log("Gateway Unlock Failed. message: " + ex.Message);
      str2 = "Failed: An unlock error occurred";
      if (flag1)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-UnlockGateway",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    this.ModelState.AddModelError("AuthKey", string.IsNullOrWhiteSpace(str2) ? "Activation Failed" : str2);
    return (ActionResult) this.View();
  }

  public ActionResult EditGatewayServerSettings(long gatewayID, string code)
  {
    Gateway model = Gateway.Load(gatewayID);
    if (!MonnitUtil.ValidateCheckDigit(gatewayID, code))
      this.ModelState.AddModelError("GatewayCode didn't Match", new Exception("GatewayCode didn't Match"));
    int num = model.IsUnlocked ? 1 : 0;
    if (!model.IsUnlocked)
      this.ModelState.AddModelError("Gateway not unlocked for point", new Exception("Gateway not unlocked for point"));
    if (!this.ModelState.IsValid || model == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "GatewayServerSettings"
      });
    this.Session["quickEditGatewayID"] = (object) gatewayID;
    // ISSUE: reference to a compiler-generated field
    if (CSNetController.\u003C\u003Eo__66.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CSNetController.\u003C\u003Eo__66.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "isDHCP", typeof (CSNetController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = CSNetController.\u003C\u003Eo__66.\u003C\u003Ep__0.Target((CallSite) CSNetController.\u003C\u003Eo__66.\u003C\u003Ep__0, this.ViewBag, !model.GatewayIP.Equals("0.0.0.0"));
    return (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult EditGatewayServerSettings(FormCollection collection)
  {
    try
    {
      Gateway gateway = Gateway.Load(this.Session["quickEditGatewayID"].ToLong());
      if (gateway == null)
        this.ModelState.AddModelError("", $"The GatewayID {collection["GatewayID"].ToLong()} is incorrect");
      if (gateway.GatewayType.SupportsCustomEncryptionKey)
      {
        if (collection["CurrentEncryptionKey"].ToStringSafe().Length == 32 /*0x20*/ || string.IsNullOrWhiteSpace(collection["CurrentEncryptionKey"].ToStringSafe()))
        {
          byte[] numArray = string.IsNullOrWhiteSpace(collection["CurrentEncryptionKey"].ToStringSafe()) ? "00000000000000000000000000000000".FormatStringToByteArray() : collection["CurrentEncryptionKey"].FormatStringToByteArray();
          gateway.CurrentEncryptionKey = numArray;
        }
        else
          this.ModelState.AddModelError("", string.Format("Custom Encryption Key must be 32 characters long or blank", (object) "Custom Encryption Key must be 32 characters long or blank"));
      }
      if (!this.ModelState.IsValid)
        return (ActionResult) this.View((object) gateway);
      Account account = Account.Load(CSNet.Load(gateway.CSNetID).AccountID);
      long customerID = long.MinValue;
      if (MonnitSession.CurrentCustomer != null)
        customerID = MonnitSession.CurrentCustomer.CustomerID;
      gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, customerID, account.AccountID, "Edited gateway server settings");
      gateway.ServerHostAddress = collection["ServerHostAddress"].ToString();
      gateway.ServerHostAddress2 = collection["ServerHostAddress"].ToString();
      gateway.Port = collection["Port"].ToInt();
      gateway.Port2 = collection["Port"].ToInt();
      if (gateway.GatewayType.SupportsGatewayIP)
      {
        if (string.IsNullOrEmpty(collection["dhcp"]))
        {
          gateway.GatewayIP = collection["GatewayIP"].ToString();
          gateway.DefaultRouterIP = collection["DefaultRouterIP"].ToString();
          gateway.GatewaySubnet = collection["GatewaySubnet"].ToString();
          gateway.GatewayDNS = collection["GatewayDNS"].ToString();
        }
        else
        {
          gateway.GatewayIP = "0.0.0.0";
          gateway.DefaultRouterIP = "0.0.0.0";
          gateway.GatewaySubnet = "0.0.0.0";
          gateway.GatewayDNS = "0.0.0.0";
        }
      }
      gateway.isEnterpriseHost = true;
      gateway.Save();
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "GatewayServerSettings"
      });
    }
    catch (Exception ex)
    {
      return (ActionResult) this.View((object) Gateway.Load(this.Session["quickEditGatewayID"].ToLong()));
    }
  }

  public ActionResult DeviceLookup() => (ActionResult) this.View();

  public ActionResult GatewayInformationDetails(long? gatewayID, string code)
  {
    if ((gatewayID ?? long.MinValue) < 1L)
      return (ActionResult) this.PartialView();
    if (!MonnitUtil.ValidateCheckDigit(gatewayID ?? long.MinValue, code))
      this.ModelState.AddModelError("", "Input is invalid.");
    if (this.ModelState.IsValid)
    {
      Gateway model = Gateway.Load(gatewayID ?? long.MinValue);
      if (model != null)
        return (ActionResult) this.View((object) model);
      this.ModelState.AddModelError("", "Unable to load gateway details");
    }
    return (ActionResult) this.View();
  }

  [Authorize]
  public ActionResult EquipmentInfo(long id) => (ActionResult) this.View((object) Gateway.Load(id));

  [Authorize]
  [HttpPost]
  [ValidateInput(false)]
  [ValidateAntiForgeryToken]
  public ActionResult EquipmentInfo(long id, FormCollection collection)
  {
    Gateway gateway = Gateway.Load(id);
    Account account = Account.Load(CSNet.Load(gateway.CSNetID).AccountID);
    gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited gateway equipment information");
    gateway.Make = collection["Make"];
    gateway.Model = collection["Model"];
    gateway.SerialNumber = collection["SerialNumber"];
    gateway.Description = collection["Description"];
    gateway.Note = collection["Note"];
    gateway.Location = collection["Location"];
    gateway.Save();
    return (ActionResult) this.View((object) gateway);
  }

  [Authorize]
  [HttpPost]
  public ActionResult GatewayDefault(long id, long? customCompanyID)
  {
    try
    {
      Gateway DBObject = Gateway.Load(id);
      Account account = Account.Load(CSNet.Load(DBObject.CSNetID).AccountID);
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Set gateway settings to default");
      if (customCompanyID.HasValue)
        DBObject.SetCustomCompanyDefaults(false);
      else
        DBObject.ResetToDefault(false, DBObject.GatewayType);
      DBObject.Save();
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
    }
    return (ActionResult) this.Content("Failed");
  }
}
