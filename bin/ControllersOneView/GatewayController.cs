// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.GatewayController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.ControllerBase;
using iMonnit.Models;
using Microsoft.CSharp.RuntimeBinder;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;

#nullable disable
namespace iMonnit.Controllers;

public class GatewayController : CSNetControllerBase
{
  public ActionResult Index() => (ActionResult) this.View();

  public ActionResult ServerSettings()
  {
    return (ActionResult) this.View((object) new AssignObjectModel());
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult ServerSettings(FormCollection collection)
  {
    if (!MonnitUtil.ValidateCheckDigit(collection["ObjectID"].ToLong(), collection["Code"].ToString()))
      this.ModelState.AddModelError("GatewayCode didn't Match", new Exception("GatewayCode didn't Match"));
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
          action = "EditServerSettings",
          controller = "Gateway",
          gatewayID = gatewayId,
          code = str
        }) : (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Unlock",
          controller = "Gateway",
          gatewayID = gatewayId
        });
    }
    return (ActionResult) this.View();
  }

  public ActionResult Unlock(long gatewayID)
  {
    return (ActionResult) this.View((object) Gateway.Load(gatewayID));
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Unlock(FormCollection collection)
  {
    DateTime utcNow = DateTime.UtcNow;
    string uri = "";
    string str1 = ConfigData.AppSettings("MeaApiLogging");
    bool flag1 = !string.IsNullOrWhiteSpace(str1) && bool.Parse(str1);
    Gateway gateway = (Gateway) null;
    string str2;
    try
    {
      gateway = Gateway.Load(collection["GatewayID"].ToLong());
      if (gateway == null)
      {
        this.ModelState.AddModelError("AuthKey", "Failed: No Gateway Found");
        // ISSUE: reference to a compiler-generated field
        if (GatewayController.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          GatewayController.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "setHostMessage", typeof (GatewayController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = GatewayController.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) GatewayController.\u003C\u003Eo__4.\u003C\u003Ep__0, this.ViewBag, "Failed");
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
          MethodName = "MEA-Unlock",
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
        // ISSUE: reference to a compiler-generated field
        if (GatewayController.\u003C\u003Eo__4.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          GatewayController.\u003C\u003Eo__4.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "setHostMessage", typeof (GatewayController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = GatewayController.\u003C\u003Eo__4.\u003C\u003Ep__1.Target((CallSite) GatewayController.\u003C\u003Eo__4.\u003C\u003Ep__1, this.ViewBag, "Success");
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
          ex.Log("GatewayController.Unlock CFR Audit Log Failed. Message: ");
        }
        gateway.IsUnlocked = true;
        gateway.SendUnlockRequest = true;
        gateway.Save();
        return (ActionResult) this.View("~/Views/Gateway/EditServerSettings.aspx", (object) gateway);
      }
    }
    catch (Exception ex)
    {
      ex.Log("GatewayController.Unlock ");
      str2 = "Failed: An unlock error occurred";
      if (flag1)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-Unlock",
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
    return (ActionResult) this.View((object) gateway);
  }

  public ActionResult EditServerSettings(long gatewayID, string code)
  {
    Gateway model = Gateway.Load(gatewayID);
    if (!MonnitUtil.ValidateCheckDigit(gatewayID, code))
    {
      this.ModelState.AddModelError("GatewayCode didn't Match", new Exception("GatewayCode didn't Match"));
      // ISSUE: reference to a compiler-generated field
      if (GatewayController.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        GatewayController.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "setHostMessage", typeof (GatewayController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = GatewayController.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) GatewayController.\u003C\u003Eo__5.\u003C\u003Ep__0, this.ViewBag, "Failed");
    }
    int num = model.IsUnlocked ? 1 : 0;
    if (!model.IsUnlocked)
    {
      this.ModelState.AddModelError("Gateway not unlocked for point", new Exception("Gateway not unlocked for point"));
      // ISSUE: reference to a compiler-generated field
      if (GatewayController.\u003C\u003Eo__5.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        GatewayController.\u003C\u003Eo__5.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "setHostMessage", typeof (GatewayController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = GatewayController.\u003C\u003Eo__5.\u003C\u003Ep__1.Target((CallSite) GatewayController.\u003C\u003Eo__5.\u003C\u003Ep__1, this.ViewBag, "Failed");
    }
    if (!this.ModelState.IsValid || model == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "ServerSettings"
      });
    this.Session["quickEditGatewayID"] = (object) gatewayID;
    // ISSUE: reference to a compiler-generated field
    if (GatewayController.\u003C\u003Eo__5.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      GatewayController.\u003C\u003Eo__5.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "setHostMessage", typeof (GatewayController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = GatewayController.\u003C\u003Eo__5.\u003C\u003Ep__2.Target((CallSite) GatewayController.\u003C\u003Eo__5.\u003C\u003Ep__2, this.ViewBag, "Success");
    // ISSUE: reference to a compiler-generated field
    if (GatewayController.\u003C\u003Eo__5.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      GatewayController.\u003C\u003Eo__5.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "isDHCP", typeof (GatewayController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = GatewayController.\u003C\u003Eo__5.\u003C\u003Ep__3.Target((CallSite) GatewayController.\u003C\u003Eo__5.\u003C\u003Ep__3, this.ViewBag, !model.GatewayIP.Equals("0.0.0.0"));
    return (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult EditServerSettings(FormCollection collection)
  {
    try
    {
      Gateway gateway = Gateway.Load(this.Session["quickEditGatewayID"].ToLong());
      if (gateway == null)
        this.ModelState.AddModelError("", $"The GatewayID {collection["GatewayID"].ToLong()} is incorrect");
      if (gateway != null && gateway.GatewayType.SupportsCustomEncryptionKey)
      {
        if (collection["CurrentEncryptionKey"].ToStringSafe().Length == 32 /*0x20*/ || string.IsNullOrWhiteSpace(collection["CurrentEncryptionKey"].ToStringSafe()))
        {
          byte[] numArray = string.IsNullOrWhiteSpace(collection["CurrentEncryptionKey"].ToStringSafe()) ? "00000000000000000000000000000000".FormatStringToByteArray() : collection["CurrentEncryptionKey"].FormatStringToByteArray();
          gateway.CurrentEncryptionKey = numArray;
        }
        else
          this.ModelState.AddModelError("", string.Format("Custom Encryption Key must be 32 characters long or blank", (object) "Custom Encryption Key must be 32 characters long or blank"));
      }
      if (collection["ServerHostAddress"].ToString().Length > 32 /*0x20*/)
        this.ModelState.AddModelError("ServerHostAddress", $"The Server Host Address \"{collection["ServerHostAddress"].ToString()}\" is too large.");
      if (this.ModelState.IsValid)
      {
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
        // ISSUE: reference to a compiler-generated field
        if (GatewayController.\u003C\u003Eo__6.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          GatewayController.\u003C\u003Eo__6.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "setHostMessage", typeof (GatewayController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = GatewayController.\u003C\u003Eo__6.\u003C\u003Ep__0.Target((CallSite) GatewayController.\u003C\u003Eo__6.\u003C\u003Ep__0, this.ViewBag, "Success!");
        return (ActionResult) this.View((object) gateway);
      }
      // ISSUE: reference to a compiler-generated field
      if (GatewayController.\u003C\u003Eo__6.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        GatewayController.\u003C\u003Eo__6.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "setHostMessage", typeof (GatewayController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = GatewayController.\u003C\u003Eo__6.\u003C\u003Ep__1.Target((CallSite) GatewayController.\u003C\u003Eo__6.\u003C\u003Ep__1, this.ViewBag, "Failed!");
      return (ActionResult) this.View((object) gateway);
    }
    catch (Exception ex)
    {
      ex.Log("GatewayController.EditServerSettings | quickEditGatewayID = " + (this.Session["quickEditGatewayID"] == null ? "null" : this.Session["quickEditGatewayID"].ToString()));
      // ISSUE: reference to a compiler-generated field
      if (GatewayController.\u003C\u003Eo__6.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        GatewayController.\u003C\u003Eo__6.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "setHostMessage", typeof (GatewayController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = GatewayController.\u003C\u003Eo__6.\u003C\u003Ep__2.Target((CallSite) GatewayController.\u003C\u003Eo__6.\u003C\u003Ep__2, this.ViewBag, "Failed");
      return (ActionResult) this.View((object) Gateway.Load(this.Session["quickEditGatewayID"].ToLong()));
    }
  }

  [AuthorizeDefault]
  public ActionResult GatewaysOnNetworkCount(long networkID, long deviceID, string code)
  {
    if (!MonnitSession.CurrentCustomer.CanSeeNetwork(networkID))
      return (ActionResult) this.Content("Authorization Denied");
    if (!MonnitUtil.ValidateCheckDigit(deviceID, code.ToUpper()))
      return (ActionResult) this.Content("1");
    int count = Gateway.LoadByCSNetID(networkID).Count;
    if (count == 0)
    {
      if (Gateway.Load(deviceID) != null)
      {
        ++count;
      }
      else
      {
        string str = ConfigData.FindValue("LookUpHost");
        if (!string.IsNullOrEmpty(str) && XDocument.Load(string.Format("{2}/xml/lookupGateway?GatewayID={0}&checkDigit={1}", (object) deviceID, (object) code, (object) str)).Descendants((XName) "APILookUpGateway").Select<XElement, Gateway>((Func<XElement, Gateway>) (g => new Gateway()
        {
          GatewayID = deviceID,
          Name = g.Attribute((XName) "GatewayName").Value,
          RadioBand = g.Attribute((XName) "RadioBand").Value,
          APNFirmwareVersion = g.Attribute((XName) "APNFirmwareVersion").Value,
          GatewayFirmwareVersion = g.Attribute((XName) "GatewayFirmwareVersion").Value,
          GatewayTypeID = g.Attribute((XName) "GatewayTypeID").Value.ToLong(),
          MacAddress = g.Attribute((XName) "MacAddress").Value,
          SensorID = g.Attribute((XName) "SensorID").Value.ToLong(),
          GenerationType = g.Attribute((XName) "GenerationType").Value,
          PowerSourceID = g.Attribute((XName) "PowerSourceID") == null ? 3L : g.Attribute((XName) "PowerSourceID").Value.ToLong(),
          SKU = g.Attribute((XName) "SKU") != null ? g.Attribute((XName) "SKU").Value : ""
        })).FirstOrDefault<Gateway>() != null)
          ++count;
      }
      if (count == 0)
      {
        Sensor sens = Sensor.Load(deviceID);
        if (sens == null)
          sens = CSNetControllerBase.LookUpSensor(new AssignObjectModel()
          {
            ObjectID = deviceID,
            Code = code,
            NetworkID = networkID
          }, sens);
        if (sens != null)
        {
          switch (sens.SensorTypeID - 1L)
          {
            case 3:
            case 5:
            case 6:
            case 7:
              ++count;
              break;
          }
        }
      }
    }
    return (ActionResult) this.Content(count.ToString());
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
        return (ActionResult) this.Content("Success");
      }
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
    }
    return (ActionResult) this.Content("Failed");
  }

  [AuthorizeDefault]
  public ActionResult Details(long id)
  {
    if (!MonnitSession.CustomerCan("Support_Advanced"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    Gateway model = Gateway.Load(id);
    if (model == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    CSNet csNet = CSNet.Load(model.CSNetID);
    return csNet == null || !MonnitSession.IsAuthorizedForAccount(csNet.AccountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Overview",
      controller = "Account"
    }) : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult LoadLocationMessages(long id)
  {
    if (!MonnitSession.CustomerCan("Support_Advanced"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    Gateway gateway = Gateway.Load(id);
    if (gateway == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Overview",
        controller = "Account"
      });
    CSNet csNet = CSNet.Load(gateway.CSNetID);
    return csNet == null || !MonnitSession.IsAuthorizedForAccount(csNet.AccountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Overview",
      controller = "Account"
    }) : (ActionResult) this.PartialView("GatewayHistory", (object) LocationMessage.LoadByDeviceID(gateway.GatewayID).OrderByDescending<LocationMessage, DateTime>((Func<LocationMessage, DateTime>) (c => c.LocationDate)).ToList<LocationMessage>());
  }

  [AuthorizeDefault]
  public ActionResult SendGPSMessage(long id)
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
    return (ActionResult) this.Content("Success");
  }
}
