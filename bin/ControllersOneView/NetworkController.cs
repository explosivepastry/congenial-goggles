// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.NetworkController
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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

#nullable disable
namespace iMonnit.Controllers;

public class NetworkController : CSNetControllerBase
{
  [AuthorizeDefault]
  public ActionResult Index() => (ActionResult) this.View();

  [AuthorizeDefault]
  public ActionResult DeviceList(long? id, long? networkID)
  {
    if (!MonnitSession.CustomerCan("Navigation_View_Settings"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    Account account1 = (Account) null;
    long? nullable;
    int num1;
    if (id.HasValue)
    {
      long accountId = MonnitSession.CurrentCustomer.AccountID;
      nullable = id;
      long valueOrDefault = nullable.GetValueOrDefault();
      num1 = accountId == valueOrDefault & nullable.HasValue ? 1 : 0;
    }
    else
      num1 = 1;
    if (num1 != 0)
      account1 = MonnitSession.CurrentCustomer.Account;
    else if (MonnitSession.CustomerCan("Navigation_View_Administration"))
    {
      Account account2 = Account.Load(id ?? long.MinValue);
      account1 = account2 == null || !MonnitSession.IsAuthorizedForAccount(account2.AccountID) ? MonnitSession.CurrentCustomer.Account : account2;
    }
    if (!networkID.HasValue && MonnitSession.SensorListFilters.CSNetID > 0L)
      networkID = new long?(MonnitSession.SensorListFilters.CSNetID);
    nullable = networkID;
    long num2 = 0;
    if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
    {
      CSNet csNet = CSNet.Load(networkID.ToLong());
      MonnitSession.SensorListFilters.CSNetID = csNet == null || csNet.AccountID != account1.AccountID ? long.MinValue : networkID.ToLong();
    }
    else
      MonnitSession.SensorListFilters.CSNetID = long.MinValue;
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long?, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NetworkID", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = NetworkController.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__1.\u003C\u003Ep__0, this.ViewBag, networkID);
    return MonnitSession.CurrentCustomer.AccountID != account1.AccountID ? (ActionResult) this.View("NetworkSettings", "Administration", (object) id) : (ActionResult) this.View((object) account1.AccountID);
  }

  [AuthorizeDefault]
  public ActionResult AssignDevice(long id, long? networkID)
  {
    AssignObjectModel model = new AssignObjectModel()
    {
      AccountID = id
    };
    model.NetworkID = networkID ?? (model.Networks.Count == 1 ? model.Networks.First<CSNet>().CSNetID : 0L);
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID) || !MonnitSession.CurrentCustomer.CanSeeNetwork(model.NetworkID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CustomerCan("Network_Edit"))
      return this.PermissionError(methodName: nameof (AssignDevice), sourceFilePath: "C:\\Development\\Enterprise405\\iMonnitRepo\\iMonnit\\ControllersOneView\\NetworkController.cs", sourceLineNumber: 96 /*0x60*/);
    if (model.Networks.Count == 0)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Create",
        controller = "Network",
        accountID = id
      });
    if (model.NetworkID <= 0L)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "NetworkSelect",
        controller = "Network",
        accountID = id,
        networkID = model.NetworkID
      });
    if (MonnitSession.CurrentCustomer.CanSeeNetwork(model.NetworkID))
      return (ActionResult) this.View((object) model);
    return (ActionResult) this.Redirect($"/Setup/AssignDevice/{id.ToString()}?networkID={model.NetworkID.ToString()}&failed=Network access denied");
  }

  [AuthorizeDefault]
  public ActionResult AssignMultipleDevices(long id, long? networkID)
  {
    AssignObjectModel model = new AssignObjectModel()
    {
      AccountID = id
    };
    model.NetworkID = networkID ?? (model.Networks.Count == 1 ? model.Networks.First<CSNet>().CSNetID : 0L);
    return !networkID.HasValue && model.Networks.Count > 1 ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "NetworkSelect",
      controller = "Network",
      accountID = id
    }) : (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [AuthorizeDefault]
  public ActionResult UploadDeviceCSV(HttpPostedFileBase Upload, FormCollection coll)
  {
    ActionResult model = (ActionResult) this.Content("Failed");
    long ID = coll["acctID"].ToLong();
    long num = coll["networkID"].ToLong();
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "acctID", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__0, this.ViewBag, ID);
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "networkID", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__1.Target((CallSite) NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__1, this.ViewBag, num);
    using (StreamReader streamReader = new StreamReader(Upload.InputStream))
    {
      HashSet<string> source = new HashSet<string>();
      System.Collections.Generic.List<string> stringList1 = new System.Collections.Generic.List<string>();
      System.Collections.Generic.List<string> stringList2 = new System.Collections.Generic.List<string>();
      if (Upload.ContentType == "text/csv")
      {
        string str1;
        while ((str1 = streamReader.ReadLine()) != null)
          source.Add(str1.Trim().Replace(",", "|").Replace("\"", " ").Replace(" ", ""));
        try
        {
          foreach (string str2 in source.Skip<string>(1))
          {
            string[] strArray = str2.Split('|');
            long minValue = long.MinValue;
            string checkCode = "";
            for (int index = 0; index < strArray.Length; ++index)
            {
              if (!(strArray[index] == string.Empty))
              {
                if (Regex.IsMatch(strArray[index], "^[0-9]*$"))
                  minValue = strArray[index].ToLong();
                else if (Regex.IsMatch(strArray[index], "^[a-zA-Z]+$"))
                  checkCode = strArray[index];
              }
            }
            string str3 = $"{minValue} - {checkCode}";
            try
            {
              if (!((RedirectResult) this.ManualSubmit(ID, minValue.ToString(), checkCode, new long?(num))).Url.Contains("&failed="))
                stringList1.Add(str3);
              else
                stringList2.Add(str3);
            }
            catch (Exception ex)
            {
              ex.Log("NetworkController/UploadDeviceCSV Failed to process ManualSubmit method");
              stringList2.Add(str3);
            }
          }
        }
        catch
        {
          model = (ActionResult) this.Content("Failed: Cannot upload CSV");
        }
        // ISSUE: reference to a compiler-generated field
        if (NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, System.Collections.Generic.List<string>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SuccessList", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__2.Target((CallSite) NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__2, this.ViewBag, stringList1);
        // ISSUE: reference to a compiler-generated field
        if (NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, System.Collections.Generic.List<string>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "FailedList", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj4 = NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__3.Target((CallSite) NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__3, this.ViewBag, stringList2);
        streamReader.Close();
        model = (ActionResult) this.Content("Success");
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ErrorMessage", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj5 = NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__4.Target((CallSite) NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__4, this.ViewBag, "CSV Document required.");
        stringList2.Add(Upload.ContentType);
        // ISSUE: reference to a compiler-generated field
        if (NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, System.Collections.Generic.List<string>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "SuccessList", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj6 = NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__5.Target((CallSite) NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__5, this.ViewBag, stringList1);
        // ISSUE: reference to a compiler-generated field
        if (NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, System.Collections.Generic.List<string>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "FailedList", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj7 = NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__6.Target((CallSite) NetworkController.\u003C\u003Eo__4.\u003C\u003Ep__6, this.ViewBag, stringList2);
      }
    }
    return (ActionResult) this.View((object) model);
  }

  public ActionResult ExportMultiDeviceTemplate()
  {
    StringBuilder stringBuilder = new StringBuilder();
    ActionResult actionResult;
    try
    {
      stringBuilder.AppendFormat("{0}, {1} \r\n", (object) "DeviceID", (object) "SecurityCode");
      stringBuilder.AppendFormat("{0}, {1} \r\n", (object) "111111", (object) "IMAAAA");
      stringBuilder.AppendFormat("{0}, {1} \r\n", (object) "123456", (object) "IMABCD");
      actionResult = (ActionResult) this.File(new UTF8Encoding().GetBytes(stringBuilder.ToString()), "text/csv", "Sample Multi-Device Template.csv");
    }
    catch (Exception ex)
    {
      ex.Log("NetworkController.ExportMultiDeviceTemplate " + ex.Message);
      stringBuilder.AppendFormat("{0}, {1} \r\n", (object) "Failed", (object) "", (object) ex.Message);
      actionResult = (ActionResult) this.File(new UTF8Encoding().GetBytes(stringBuilder.ToString()), "text/csv", "[FAILED] Sample Multi-Device Template.csv");
    }
    return actionResult;
  }

  public ActionResult ExportXMLDeviceTemplate()
  {
    ActionResult actionResult;
    try
    {
      actionResult = (ActionResult) this.File(new UTF8Encoding().GetBytes("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" + new XElement((XName) "Result", (object) new XElement((XName) "APILookUpSensorList", new object[2]
      {
        (object) new XElement((XName) "APILookUpSensor", new object[29]
        {
          (object) new XAttribute((XName) "ApplicationID", (object) "2"),
          (object) new XAttribute((XName) "SensorID", (object) "111111"),
          (object) new XAttribute((XName) "SensorName", (object) "Temperature"),
          (object) new XAttribute((XName) "SensorTypeID", (object) "8"),
          (object) new XAttribute((XName) "FirmwareVersion", (object) "1.0.0.0"),
          (object) new XAttribute((XName) "PowerSourceID", (object) "20"),
          (object) new XAttribute((XName) "RadioBand", (object) "ALTA 900 MHz"),
          (object) new XAttribute((XName) "ReportInterval", (object) "120"),
          (object) new XAttribute((XName) "ActiveStateInterval", (object) "120"),
          (object) new XAttribute((XName) "CalibrationCertification", (object) ""),
          (object) new XAttribute((XName) "MeasurementsPerTransmission", (object) "1"),
          (object) new XAttribute((XName) "TransmitOffset", (object) "0"),
          (object) new XAttribute((XName) "Hysteresis", (object) "0"),
          (object) new XAttribute((XName) "MinimumThreshold", (object) "-4294967295"),
          (object) new XAttribute((XName) "MaximumThreshold", (object) "4294967295"),
          (object) new XAttribute((XName) "EventDetectionType", (object) "0"),
          (object) new XAttribute((XName) "EventDetectionPeriod", (object) "50"),
          (object) new XAttribute((XName) "EventDetectionCount", (object) "6"),
          (object) new XAttribute((XName) "RearmTime", (object) "1"),
          (object) new XAttribute((XName) "BiStable", (object) "1"),
          (object) new XAttribute((XName) "Calibration1", (object) "4294967295"),
          (object) new XAttribute((XName) "Calibration2", (object) "4294967295"),
          (object) new XAttribute((XName) "Calibration3", (object) "4294967295"),
          (object) new XAttribute((XName) "Calibration4", (object) "4294967295"),
          (object) new XAttribute((XName) "CalibrationCertificationValidUntil", (object) "1/1/0001 12:00:00 AM"),
          (object) new XAttribute((XName) "GenerationType", (object) "Gen1"),
          (object) new XAttribute((XName) "SKU", (object) ""),
          (object) new XAttribute((XName) "CableID", (object) "-9223372036854775808"),
          (object) new XAttribute((XName) "IsCableEnabled", (object) "False")
        }),
        (object) new XElement((XName) "APILookUpSensor", new object[29]
        {
          (object) new XAttribute((XName) "ApplicationID", (object) "46"),
          (object) new XAttribute((XName) "SensorID", (object) "123456"),
          (object) new XAttribute((XName) "SensorName", (object) "Low Temperature"),
          (object) new XAttribute((XName) "SensorTypeID", (object) "7"),
          (object) new XAttribute((XName) "FirmwareVersion", (object) "1.0.0.0"),
          (object) new XAttribute((XName) "PowerSourceID", (object) "10"),
          (object) new XAttribute((XName) "RadioBand", (object) "ALTA 900 MHz"),
          (object) new XAttribute((XName) "ReportInterval", (object) "120"),
          (object) new XAttribute((XName) "ActiveStateInterval", (object) "120"),
          (object) new XAttribute((XName) "CalibrationCertification", (object) ""),
          (object) new XAttribute((XName) "MeasurementsPerTransmission", (object) "1"),
          (object) new XAttribute((XName) "TransmitOffset", (object) "0"),
          (object) new XAttribute((XName) "Hysteresis", (object) "0"),
          (object) new XAttribute((XName) "MinimumThreshold", (object) "6488064"),
          (object) new XAttribute((XName) "MaximumThreshold", (object) "218775168"),
          (object) new XAttribute((XName) "EventDetectionType", (object) "-2147483648"),
          (object) new XAttribute((XName) "EventDetectionPeriod", (object) "-2147483648"),
          (object) new XAttribute((XName) "EventDetectionCount", (object) "-2147483648"),
          (object) new XAttribute((XName) "RearmTime", (object) "0"),
          (object) new XAttribute((XName) "BiStable", (object) "-2147483648"),
          (object) new XAttribute((XName) "Calibration1", (object) "4294967295"),
          (object) new XAttribute((XName) "Calibration2", (object) "4294967295"),
          (object) new XAttribute((XName) "Calibration3", (object) "4294967295"),
          (object) new XAttribute((XName) "Calibration4", (object) "4294967295"),
          (object) new XAttribute((XName) "CalibrationCertificationValidUntil", (object) "1/1/0001 12:00:00 AM"),
          (object) new XAttribute((XName) "GenerationType", (object) "Gen1"),
          (object) new XAttribute((XName) "SKU", (object) ""),
          (object) new XAttribute((XName) "CableID", (object) "-9223372036854775808"),
          (object) new XAttribute((XName) "IsCableEnabled", (object) "False")
        })
      })).ToString()), "text/xml", "Sample_XML_Template.xml");
    }
    catch (Exception ex)
    {
      ex.Log("NetworkController.ExportXMLDeviceTemplate " + ex.Message);
      actionResult = (ActionResult) this.File(new UTF8Encoding().GetBytes("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" + new XElement((XName) "Result", (object) new XElement((XName) "Error", (object) new XAttribute((XName) "Message", (object) ex.Message))).ToString()), "text/xml", "[FAILED]_Sample_XML_Template.xml");
    }
    return actionResult;
  }

  [AuthorizeDefault]
  public ActionResult NetworkSelect(long accountID, long? networkID)
  {
    Account account = Account.Load(accountID);
    CSNet csNet = CSNet.Load(networkID ?? -1L);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (csNet != null && !MonnitSession.CurrentCustomer.CanSeeAccount(csNet.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    System.Collections.Generic.List<CSNet> networksUserCanSee = CSNetControllerBase.GetListOfNetworksUserCanSee(new long?(account.AccountID));
    if (csNet == null)
      csNet = networksUserCanSee.FirstOrDefault<CSNet>();
    if (csNet == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = ("Create/" + accountID.ToString()),
        controller = "Network"
      });
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__7.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__7.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NetworkController.\u003C\u003Eo__7.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__7.\u003C\u003Ep__0, this.ViewBag, accountID);
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__7.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__7.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, CSNet, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Network", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = NetworkController.\u003C\u003Eo__7.\u003C\u003Ep__1.Target((CallSite) NetworkController.\u003C\u003Eo__7.\u003C\u003Ep__1, this.ViewBag, csNet);
    return (ActionResult) this.View((object) networksUserCanSee);
  }

  [AuthorizeDefault]
  public ActionResult RemoveGateway(long id)
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
        return (ActionResult) this.Content("Unable to remove gateway.");
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
      return (ActionResult) this.Content("Success");
    }
    catch
    {
    }
    return (ActionResult) this.Content("Unable to remove gateway.");
  }

  [AuthorizeDefault]
  public ActionResult RemoveSensor(long id)
  {
    Sensor sensor = Sensor.Load(id);
    if (sensor == null || !MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    try
    {
      long id1 = ConfigData.AppSettings("DefaultCSNetID").ToLong();
      Account account = Account.Load(sensor.AccountID);
      sensor.LogAuditData(eAuditAction.Delete, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Removed sensor");
      if (CSNetController.TryMoveSensor(id1, sensor.SensorID))
      {
        Gateway.LoadBySensorID(sensor.SensorID)?.ResetToDefault(true);
        foreach (CustomerFavorite customerFavorite in CustomerFavorite.LoadBySensorID(id))
        {
          customerFavorite.Delete();
          if (customerFavorite.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
            MonnitSession.CurrentCustomerFavorites.Invalidate();
        }
        foreach (Notification notification in Notification.LoadBySensorID(sensor.SensorID))
          notification.RemoveSensor(sensor);
        MonnitSession.AccountSensorTotal = int.MinValue;
        return (ActionResult) this.Content("Success");
      }
    }
    catch
    {
    }
    return (ActionResult) this.Content("Unable to remove sensor.");
  }

  [AuthorizeDefault]
  public ActionResult ManualSubmit(long ID, string deviceID, string checkCode, long? networkID)
  {
    ModelStateDictionary modelStateDictionary = new ModelStateDictionary();
    AssignObjectModel Model = new AssignObjectModel()
    {
      AccountID = ID
    };
    Model.NetworkID = networkID ?? (Model.Networks.Count >= 1 ? Model.Networks.First<CSNet>().CSNetID : 0L);
    Model.ObjectID = deviceID.ToLong();
    Model.Code = checkCode;
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__10.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__10.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NetworkID", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NetworkController.\u003C\u003Eo__10.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__10.\u003C\u003Ep__0, this.ViewBag, Model.NetworkID);
    if (!MonnitUtil.ValidateCheckDigit(Model.ObjectID, Model.Code))
      return (ActionResult) this.Redirect($"/Setup/AssignDevice/{ID.ToString()}?networkID={Model.NetworkID.ToString()}&failed=Check Code invalid");
    if (Model.NetworkID < 1L)
      return (ActionResult) this.Redirect($"/Setup/AssignDevice/{ID.ToString()}?networkID={Model.NetworkID.ToString()}&failed=Network is required");
    if (!MonnitSession.CurrentCustomer.CanSeeNetwork(Model.NetworkID))
      return (ActionResult) this.Redirect($"/Setup/AssignDevice/{ID.ToString()}?networkID={Model.NetworkID.ToString()}&failed=Network access denied");
    Sensor sensor = Sensor.Load(Model.ObjectID);
    Gateway gateway1 = Gateway.Load(Model.ObjectID);
    CSNet csNet = CSNet.Load(Model.NetworkID);
    if (sensor != null && sensor.IsDeleted)
      return (ActionResult) this.Redirect($"/Setup/AssignDevice/{ID.ToString()}?networkID={Model.NetworkID.ToString()}&failed=SensorID {Model.ObjectID.ToString()} not authorized to transfer");
    if (gateway1 != null && gateway1.IsDeleted)
      return (ActionResult) this.Redirect($"/Setup/AssignDevice/{ID.ToString()}?networkID={Model.NetworkID.ToString()}&failed=GatewayID {Model.ObjectID.ToString()} not authorized to transfer");
    if (csNet == null)
      modelStateDictionary.AddModelError("ObjectID", string.Format("Unknown Network " + Model.NetworkID.ToString(), (object) Model.ObjectID));
    else if (ID != csNet.AccountID)
      return (ActionResult) this.Redirect($"/Setup/AssignDevice/{ID.ToString()}?networkID={Model.NetworkID.ToString()}&failed=Network not found on this account");
    if (sensor == null)
    {
      sensor = CSNetControllerBase.LookUpSensor(Model, sensor);
      if (sensor != null)
      {
        eProgramLevel o = MonnitSession.ProgramLevel();
        if (ThemeController.SensorCount() > o.ToInt())
          return (ActionResult) this.Redirect($"/Setup/AssignDevice/{ID.ToString()}?networkID={Model.NetworkID.ToString()}&failed=Only {o.ToString()} sensors allowed for this installation.");
        sensor.ForceInsert();
      }
    }
    if (gateway1 == null && sensor == null)
    {
      try
      {
        gateway1 = CSNetControllerBase.LookUpGateway(Model);
        if (gateway1 != null)
        {
          gateway1.CSNetID = csNet.CSNetID;
          gateway1.ForceInsert();
        }
      }
      catch (Exception ex)
      {
        ex.Log($"NetworkController.ManualSubmit(ID = {ID}, deviceID = {deviceID}, checkCode = {checkCode}, networkID = {networkID}");
      }
    }
    if (sensor == null)
    {
      if (gateway1 == null)
        modelStateDictionary.AddModelError("ObjectID", "No Device Found");
      if (modelStateDictionary.IsValid)
      {
        if (gateway1.Network == null || gateway1.Network.HoldingOnlyNetwork || gateway1.CSNetID == ConfigData.AppSettings("DefaultCSNetID").ToLong() || MonnitSession.CurrentCustomer.CanSeeNetwork(gateway1.CSNetID))
        {
          NetworkController.AssignGateway(Model, gateway1);
          // ISSUE: reference to a compiler-generated field
          if (NetworkController.\u003C\u003Eo__10.\u003C\u003Ep__1 == null)
          {
            // ISSUE: reference to a compiler-generated field
            NetworkController.\u003C\u003Eo__10.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NetworkID", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj2 = NetworkController.\u003C\u003Eo__10.\u003C\u003Ep__1.Target((CallSite) NetworkController.\u003C\u003Eo__10.\u003C\u003Ep__1, this.ViewBag, Model.NetworkID);
          if (MonnitSession.IsEnterprise)
            return (ActionResult) this.Redirect($"/Overview/GatewayHome/{gateway1.GatewayID.ToString()}?newDevice=true");
          return sensor != null ? (ActionResult) this.Redirect("/Setup/SensorEdit/" + sensor.SensorID.ToString()) : (ActionResult) this.Redirect($"/Setup/GatewayEdit/{gateway1.GatewayID.ToString()}?reset={(gateway1.AccountID != csNet.AccountID).ToString()}");
        }
        return (ActionResult) this.Redirect($"/Setup/AssignDevice/{ID.ToString()}?networkID={csNet.CSNetID.ToString()}&failed={$"GatewayID: {Model.ObjectID} not authorized to transfer."}");
      }
      return (ActionResult) this.Redirect($"/Setup/AssignDevice/{ID.ToString()}?networkID={csNet.CSNetID.ToString()}&failed={modelStateDictionary["ObjectID"].Errors[0].ErrorMessage}");
    }
    if (csNet.Sensors.Where<Sensor>((Func<Sensor, bool>) (s => s.SensorTypeID != 4L)).Count<Sensor>() >= 500 && sensor.SensorTypeID != 4L)
      modelStateDictionary.AddModelError("ObjectID", $"SensorID: {Model.ObjectID} could not be transfered to new network.  Network sensor limit has been reached.");
    Account account = Account.Load(csNet.AccountID);
    if (csNet.AccountID != sensor.AccountID && Sensor.LoadByAccountID(account.AccountID).Count >= account.CurrentSubscription.AccountSubscriptionType.AllowedSensors)
      return (ActionResult) this.Redirect($"/Setup/AssignDevice/{ID.ToString()}?networkID={Model.NetworkID.ToString()}&failed={$"SensorID: {Model.ObjectID} could not be added.  Sensor limit has been reached."}");
    if (modelStateDictionary.IsValid)
    {
      if (sensor.Network == null || sensor.Network.HoldingOnlyNetwork || sensor.Network.CSNetID == ConfigData.AppSettings("DefaultCSNetID").ToLong() || MonnitSession.CurrentCustomer.CanSeeNetwork(sensor.CSNetID))
      {
        Gateway gateway2 = Gateway.LoadBySensorID(sensor.SensorID);
        if (gateway2 == null)
        {
          gateway2 = XDocument.Load(string.Format("{2}/xml/LookUpWifiGateway?SensorID={0}&checkDigit={1}", (object) sensor.SensorID, (object) Model.Code, (object) ConfigData.FindValue("LookUpHost"))).Descendants((XName) "APILookUpGateway").Select<XElement, Gateway>((Func<XElement, Gateway>) (g => new Gateway()
          {
            GatewayID = g.Attribute((XName) "GatewayID").Value.ToLong(),
            Name = g.Attribute((XName) "GatewayName").Value,
            RadioBand = g.Attribute((XName) "RadioBand").Value,
            APNFirmwareVersion = g.Attribute((XName) "APNFirmwareVersion").Value,
            GatewayFirmwareVersion = g.Attribute((XName) "GatewayFirmwareVersion").Value,
            GatewayTypeID = g.Attribute((XName) "GatewayTypeID").Value.ToLong(),
            MacAddress = g.Attribute((XName) "MacAddress").Value,
            CSNetID = Model.NetworkID,
            SensorID = g.Attribute((XName) "SensorID").Value.ToLong(),
            GenerationType = g.Attribute((XName) "GenerationType").Value,
            PowerSourceID = g.Attribute((XName) "PowerSourceID") == null ? 3L : g.Attribute((XName) "PowerSourceID").Value.ToLong(),
            SKU = g.Attribute((XName) "SKU").Value
          })).FirstOrDefault<Gateway>();
          if (gateway2 != null)
          {
            gateway2.CSNetID = csNet.CSNetID;
            gateway2.ForceInsert();
          }
        }
        if (gateway2 != null)
          NetworkController.AssignGateway(Model, gateway2);
        NetworkController.AssignSensor(Model, sensor);
        // ISSUE: reference to a compiler-generated field
        if (NetworkController.\u003C\u003Eo__10.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NetworkController.\u003C\u003Eo__10.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NetworkID", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = NetworkController.\u003C\u003Eo__10.\u003C\u003Ep__2.Target((CallSite) NetworkController.\u003C\u003Eo__10.\u003C\u003Ep__2, this.ViewBag, Model.NetworkID);
        return MonnitSession.IsEnterprise ? (ActionResult) this.Redirect($"/Overview/SensorChart/{sensor.SensorID.ToString()}?newDevice=true") : (ActionResult) this.Redirect("/Setup/SensorEdit/" + sensor.SensorID.ToString());
      }
      return (ActionResult) this.Redirect($"/Setup/AssignDevice/{ID.ToString()}?networkID={csNet.CSNetID.ToString()}&failed={$"SensorID: {Model.ObjectID} not authorized to transfer."}");
    }
    return (ActionResult) this.Redirect($"/Setup/AssignDevice/{ID.ToString()}?networkID={csNet.CSNetID.ToString()}&failed={modelStateDictionary["ObjectID"].Errors[0].ErrorMessage}");
  }

  private static Gateway AssignGateway(AssignObjectModel Model, Gateway gateway)
  {
    if (gateway != null)
    {
      if (gateway.AccountID != Model.AccountID)
      {
        foreach (Notification notification in GatewayNotification.LoadByGatewayID(gateway.GatewayID))
          notification.RemoveGateway(gateway);
        gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, Model.AccountID, "Assigned gateway to network on new account.");
      }
      else
        gateway.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, Model.AccountID, "Assigned gateway to newtwork on current account");
      NetworkAudit.LogNetworkChange(MonnitSession.CurrentCustomer.CustomerID, gateway, new long?(gateway.AccountID));
      gateway.CSNetID = Model.NetworkID;
      gateway.LastCommunicationDate = DateTime.MinValue;
      gateway.StartDate = DateTime.UtcNow;
      gateway.Save();
    }
    return gateway;
  }

  private static Sensor AssignSensor(AssignObjectModel Model, Sensor sensor)
  {
    if (sensor != null)
    {
      if (sensor.AccountID != Model.AccountID)
      {
        sensor.ResetLastCommunicationDate();
        sensor.StartDate = DateTime.UtcNow;
        sensor.SensorName = $"{sensor.MonnitApplication.ApplicationName.ToString()} - {sensor.SensorID.ToString()}";
        TimedCache.RemoveObject("SensorCount");
        MonnitSession.AccountSensorTotal = int.MinValue;
        foreach (Notification notification in Notification.LoadBySensorID(sensor.SensorID))
          notification.RemoveSensor(sensor);
        sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, Model.AccountID, "Assigned sensor to new account");
      }
      else
        sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, Model.AccountID, "Assigned sensor to network on current account");
      sensor.AccountID = Model.AccountID;
      sensor.CSNetID = Model.NetworkID;
      sensor.IsActive = true;
      sensor.IsNewToNetwork = true;
      sensor.Save();
    }
    return sensor;
  }

  [AuthorizeDefault]
  public ActionResult showDeviceDetails(long ID, string type)
  {
    if (!string.IsNullOrEmpty(type))
    {
      if (type.ToLower() == "sensor")
      {
        Sensor sensor = Sensor.Load(ID.ToLong());
        if (sensor != null && MonnitSession.CurrentCustomer.CanSeeSensor(sensor))
          return (ActionResult) this.PartialView("SensorDetail", (object) sensor);
      }
      else
      {
        if (!(type.ToLower() == "gateway"))
          return (ActionResult) this.Content("Failed");
        Gateway model = Gateway.Load(ID.ToLong());
        if (model != null && MonnitSession.CurrentCustomer.CanSeeNetwork(model.CSNetID))
          return (ActionResult) this.PartialView("GatewayDetail", (object) model);
      }
    }
    return (ActionResult) null;
  }

  [AuthorizeDefault]
  public ActionResult Create(long id)
  {
    Account account = Account.Load(id);
    if (account == null || !MonnitSession.CustomerCan("Network_Create"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (CSNet.LoadByAccountID(account.AccountID).Count > 0 && !account.CurrentSubscription.Can(Feature.Find("multiple_networks")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__14.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__14.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "EditResponse", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = NetworkController.\u003C\u003Eo__14.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__14.\u003C\u003Ep__0, this.ViewBag, "");
    return (ActionResult) this.View((object) new CreateNetworkModel());
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Create(long id, CreateNetworkModel model)
  {
    if (CSNet.LoadByAccountID(id).Count >= ConfigData.AppSettings("MaxNetworkCount", "10").ToInt() && !MonnitSession.IsCurrentCustomerMonnitAdmin)
    {
      // ISSUE: reference to a compiler-generated field
      if (NetworkController.\u003C\u003Eo__15.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NetworkController.\u003C\u003Eo__15.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "EditResponse", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = NetworkController.\u003C\u003Eo__15.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__15.\u003C\u003Ep__0, this.ViewBag, "Unauthorized");
      return (ActionResult) this.View((object) model);
    }
    if (this.ModelState.IsValid)
    {
      CSNet DBObject = new CSNet();
      DBObject.AccountID = id;
      DBObject.Name = model.Name.Replace("\"", "").Replace("&quot;", "");
      DBObject.SendNotifications = true;
      DBObject.Save();
      long permissionTypeId = CustomerPermissionType.Find("Network_View").CustomerPermissionTypeID;
      Account account1 = Account.Load(DBObject.AccountID);
      foreach (Customer customer in Customer.LoadAllByAccount(DBObject.AccountID))
      {
        if (customer.IsAdmin || customer.CustomerID == account1.PrimaryContactID || customer.CustomerID == MonnitSession.CurrentCustomer.CustomerID)
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
      return MonnitSession.CurrentCustomer.Can("Network_Edit") ? (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Edit",
        controller = "Network",
        id = DBObject.CSNetID
      }) : (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "List",
        controller = "Network",
        id = account2.AccountID
      });
    }
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__15.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__15.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "EditResponse", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NetworkController.\u003C\u003Eo__15.\u003C\u003Ep__1.Target((CallSite) NetworkController.\u003C\u003Eo__15.\u003C\u003Ep__1, this.ViewBag, "Failed");
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult Edit(long id)
  {
    CSNet model = CSNet.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID) || !MonnitSession.CurrentCustomer.CanSeeNetwork(model.CSNetID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CustomerCan("Network_Edit"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Network",
        ErrorTranslateTag = "Network/Edit|",
        ErrorTitle = "Unauthorized access to edit networks",
        ErrorMessage = "You do not have permission to access this page."
      });
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__16.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__16.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, System.Collections.Generic.List<CSNet>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "networks", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NetworkController.\u003C\u003Eo__16.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__16.\u003C\u003Ep__0, this.ViewBag, CSNetControllerBase.GetListOfNetworksUserCanSee(new long?(model.AccountID)));
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__16.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__16.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "EditResponse", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = NetworkController.\u003C\u003Eo__16.\u003C\u003Ep__1.Target((CallSite) NetworkController.\u003C\u003Eo__16.\u003C\u003Ep__1, this.ViewBag, "");
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult Edit(long id, FormCollection collection)
  {
    CSNet csNet = CSNet.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CustomerCan("Network_Edit"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Network",
        ErrorTranslateTag = "Network/Edit|",
        ErrorTitle = "Unauthorized access to edit networks",
        ErrorMessage = "You do not have permission to access this page."
      });
    if (this.ModelState.IsValid)
    {
      Account account = Account.Load(csNet.AccountID);
      csNet.LogAuditData(eAuditAction.Update, eAuditObject.Network, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Edited network");
      try
      {
        csNet.Name = collection["networkName"].Replace("\"", "").Replace("&quot;", "");
        int num = !MonnitSession.CustomerCan("Notification_Disable_Network") ? 0 : (MonnitSession.CustomerCan("Notification_Edit") ? 1 : 0);
        csNet.SendNotifications = num == 0 || collection["sendNotification"].ToBool();
        csNet.HoldingOnlyNetwork = collection["holdingNetwork"].ToBool();
        DateTime LocalTime;
        try
        {
          LocalTime = DateTime.ParseExact(collection["externalAccess"], $"{MonnitSession.CurrentCustomer.Preferences["Date Format"].ToString()} {MonnitSession.CurrentCustomer.Preferences["Time Format"].ToString()}", (IFormatProvider) CultureInfo.InvariantCulture);
        }
        catch
        {
          try
          {
            LocalTime = DateTime.Parse(collection["externalAccess"]);
          }
          catch (Exception ex)
          {
            ex.Log($"1 NetworkController.Edit(id = {id}, collection = {collection})");
            LocalTime = collection["externalAccess"].ToDateTime();
          }
        }
        csNet.ExternalAccessUntil = Monnit.TimeZone.GetUTCFromLocalById(LocalTime, MonnitSession.CurrentCustomer.Account.TimeZoneID);
        csNet.Save();
        // ISSUE: reference to a compiler-generated field
        if (NetworkController.\u003C\u003Eo__17.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NetworkController.\u003C\u003Eo__17.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "EditResponse", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = NetworkController.\u003C\u003Eo__17.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__17.\u003C\u003Ep__0, this.ViewBag, "Success");
      }
      catch (Exception ex)
      {
        ex.Log($"2 NetworkController.Edit(id = {id}, collection = {collection})");
        // ISSUE: reference to a compiler-generated field
        if (NetworkController.\u003C\u003Eo__17.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NetworkController.\u003C\u003Eo__17.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "EditResponse", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = NetworkController.\u003C\u003Eo__17.\u003C\u003Ep__1.Target((CallSite) NetworkController.\u003C\u003Eo__17.\u003C\u003Ep__1, this.ViewBag, "Failed");
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__17.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__17.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, System.Collections.Generic.List<CSNet>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "networks", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NetworkController.\u003C\u003Eo__17.\u003C\u003Ep__2.Target((CallSite) NetworkController.\u003C\u003Eo__17.\u003C\u003Ep__2, this.ViewBag, CSNetControllerBase.GetListOfNetworksUserCanSee(new long?(csNet.AccountID)));
    return (ActionResult) this.View((object) csNet);
  }

  [AuthorizeDefault]
  public ActionResult Delete(long id)
  {
    CSNet DBObject = CSNet.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(DBObject.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "List",
        controller = "Network",
        id = DBObject.AccountID
      });
    long id1 = ConfigData.AppSettings("DefaultCSNetID").ToLong();
    foreach (Gateway gateway in DBObject.Gateways)
    {
      if (!CSNetController.TryRemoveGateway(gateway))
        return (ActionResult) this.Content("Network delete failed, Unable to delete Gateway: " + gateway.GatewayID.ToString());
    }
    foreach (Sensor sensor in DBObject.Sensors)
      CSNetController.TryMoveSensor(id1, sensor.SensorID);
    try
    {
      DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Network, MonnitSession.CurrentCustomer.CustomerID, DBObject.AccountID, "Deleted network");
      DBObject.Delete();
      foreach (Customer customer in Customer.LoadAllByAccount(DBObject.AccountID))
      {
        foreach (CustomerPermission permission in customer.Permissions)
        {
          if (permission.CSNetID == DBObject.CSNetID)
            permission.Delete();
        }
      }
    }
    catch
    {
      return (ActionResult) this.Content("Unable to delete Network.");
    }
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult List(long? accountID)
  {
    Account account = Account.Load(accountID ?? MonnitSession.CurrentCustomer.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__19.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__19.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Account, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "account", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NetworkController.\u003C\u003Eo__19.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__19.\u003C\u003Ep__0, this.ViewBag, account);
    System.Collections.Generic.List<CSNet> networksUserCanSee = CSNetControllerBase.GetListOfNetworksUserCanSee(new long?(account.AccountID));
    System.Collections.Generic.List<CSNet> csNetList = CSNet.LoadByAccountID(account.AccountID);
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__19.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__19.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NetworksUserCanSeeCount", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = NetworkController.\u003C\u003Eo__19.\u003C\u003Ep__1.Target((CallSite) NetworkController.\u003C\u003Eo__19.\u003C\u003Ep__1, this.ViewBag, networksUserCanSee.Count);
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__19.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__19.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AllNetworksForAccount", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = NetworkController.\u003C\u003Eo__19.\u003C\u003Ep__2.Target((CallSite) NetworkController.\u003C\u003Eo__19.\u003C\u003Ep__2, this.ViewBag, csNetList.Count);
    return (ActionResult) this.View((object) networksUserCanSee);
  }

  [AuthorizeDefault]
  public ActionResult NetworkFilter(bool? holdOnly, string q)
  {
    try
    {
      Account account = MonnitSession.CurrentCustomer.Account;
      if (account == null)
        return (ActionResult) this.Content("Failed");
      System.Collections.Generic.List<CSNet> csNetList = CSNetControllerBase.GetListOfNetworksUserCanSee(new long?(account.AccountID));
      if (holdOnly.HasValue)
        csNetList = csNetList.Where<CSNet>((Func<CSNet, bool>) (c =>
        {
          int num1 = c.HoldingOnlyNetwork ? 1 : 0;
          bool? nullable = holdOnly;
          int num2 = nullable.GetValueOrDefault() ? 1 : 0;
          return num1 == num2 & nullable.HasValue;
        })).ToList<CSNet>();
      if (!string.IsNullOrEmpty(q))
        csNetList = csNetList.Where<CSNet>((Func<CSNet, bool>) (s => s.Name.ToLower().Contains(q.ToLower()) || s.Name.ToString().ToLower().Contains(q.ToLower()))).ToList<CSNet>();
      // ISSUE: reference to a compiler-generated field
      if (NetworkController.\u003C\u003Eo__20.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NetworkController.\u003C\u003Eo__20.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "NetworkCount", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = NetworkController.\u003C\u003Eo__20.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__20.\u003C\u003Ep__0, this.ViewBag, csNetList.Count);
      return (ActionResult) this.PartialView("Details", (object) csNetList);
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult CheckSensorMove(long id)
  {
    if (!MonnitSession.CustomerCan("Sensor_Can_Change_Network"))
      return (ActionResult) this.Content("showAlertModal('User not authorized to move sensor');");
    try
    {
      Sensor sensor = Sensor.Load(id);
      if (sensor == null)
      {
        eProgramLevel o = MonnitSession.ProgramLevel();
        if (ThemeController.SensorCount() <= o.ToInt())
        {
          sensor = CSNetControllerBase.LookUpSensor(MonnitSession.CurrentCustomer.AccountID, id, MonnitUtil.CheckDigit(id), sensor);
          if (sensor == null)
            return (ActionResult) this.Content($"showAlertModal('SensorID: {id} could not be assigned.');");
          sensor.ForceInsert();
          TimedCache.RemoveObject("SensorCount");
          Gateway gateway = CSNetControllerBase.LookUpWifiGateway(new AssignObjectModel()
          {
            AccountID = sensor.AccountID,
            NetworkID = sensor.CSNetID,
            ObjectID = sensor.SensorID,
            Code = MonnitUtil.CheckDigit(id)
          }, sensor);
          if (gateway != null)
          {
            gateway.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
            gateway.ForceInsert();
          }
        }
      }
      if (sensor == null || sensor.CSNetID != long.MinValue && !MonnitSession.IsAuthorizedForAccount(sensor.AccountID))
        return (ActionResult) this.Content($"showAlertModal('SensorID: {id} not found.');");
      Account account = Account.Load(sensor.AccountID);
      sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Moved sensor to different network");
      string content = "$.get('/Network/MoveSensor/' + $('#CSNetID').val() + '?sensorID=' + $('#QuickSensorID').val(), function(data) { if (data != 'Success') { $('#SensorMoveResult').html(data); } else { window.location.href = '/Network/Edit/' + $('#CSNetID').val() + '?result=SensorAdded' } });";
      long num = ConfigData.AppSettings("DefaultCSNetID").ToLong();
      if (sensor.CSNetID == num || sensor.CSNetID == long.MinValue)
        return (ActionResult) this.Content(content);
      CSNet csNet = CSNet.Load(sensor.CSNetID);
      return (ActionResult) this.Content($"let values = {{}};\r\n                            values.url = '/Network/MoveSensor/' + $('#CSNetID').val() + '?sensorID=' + $('#QuickSensorID').val();\r\n                            values.text = {$"'This sensor currently belongs to Account: {Account.Load(sensor.AccountID).CompanyName.Replace("'", "").Replace("\"", "").Replace("&#39;", "")} Network: {csNet.Name.Replace("'", "").Replace("\"", "").Replace("&#39;", "")}'"};\r\n                            values.callback = function(data) {{\r\n                                                if (data != 'Success') {{\r\n                                                    $(\"#SensorMoveResult\").html(data);\r\n                                                }} else {{\r\n                                                    window.location.href = '/Network/Edit/' + $('#CSNetID').val() + '?result=SensorAdded';\r\n                                                }}\r\n                                            }};\r\n                                            openConfirm(values); ");
    }
    catch (Exception ex)
    {
      ex.Log($"NetworkController.CheckSensorMove | SensorID: {id} could not be transfered to new network");
      return (ActionResult) this.Content($"showAlertModal('SensorID: {id} could not be transfered to new network');");
    }
  }

  [AuthorizeDefault]
  public ActionResult MoveSensor(long id, long sensorID)
  {
    this.ViewData[nameof (MoveSensor)] = (object) true;
    try
    {
      if (!NetworkController.TryMoveSensor(id, sensorID))
        return (ActionResult) this.Content($"SensorID: {sensorID} could not be transfered to new network");
    }
    catch (Exception ex)
    {
      ex.Log($"NetworkController.MoveSensor | CSNetID = {id}, SensorID: {sensorID} could not be transfered to new network");
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

  [AuthorizeDefault]
  public ActionResult XmlDeviceAdd(long id, long? networkID)
  {
    if (!MonnitSession.CustomerCan("Sensor_Create") || !MonnitSession.CustomerCan("Gateway_Create"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Network",
        ErrorTranslateTag = "Network/XmlDeviceAdd|",
        ErrorTitle = "Unauthorized access to create devices",
        ErrorMessage = "You do not have permission to access this page."
      });
    xmlDeviceAddModel model = new xmlDeviceAddModel()
    {
      AccountID = id
    };
    model.NetworkID = networkID ?? (model.Networks == null || model.Networks.Count != 1 ? 0L : model.Networks.First<CSNet>().CSNetID);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public ActionResult XmlDeviceAdd(xmlDeviceAddModel model)
  {
    if (!MonnitSession.CustomerCan("Sensor_Create") || !MonnitSession.CustomerCan("Gateway_Create"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Network",
        ErrorTranslateTag = "Network/XmlDeviceAdd|",
        ErrorTitle = "Unauthorized access to create devices",
        ErrorMessage = "You do not have permission to access this page."
      });
    if (model.AccountID <= 0L || model.NetworkID <= 0L)
      this.ModelState.AddModelError("NetworkID", "NetworkID required.");
    else if (!MonnitSession.CurrentCustomer.CanSeeNetwork(model.NetworkID))
      this.ModelState.AddModelError("NetworkID", "Network access denied");
    Account account = Account.Load(model.AccountID);
    CSNet csNet = CSNet.Load(model.NetworkID);
    HttpPostedFileBase file = this.Request.Files["xmlMetaData"];
    if (file == null || file.ContentLength < 1 || file.ContentType != "text/xml")
    {
      this.ModelState.AddModelError("xmlMetaData", "XML Document required.");
      return (ActionResult) this.View((object) model);
    }
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load(file.InputStream);
    XDocument xdocument = XDocument.Parse(xmlDocument.OuterXml);
    System.Collections.Generic.List<Sensor> source = new System.Collections.Generic.List<Sensor>();
    System.Collections.Generic.List<Gateway> gatewayList = new System.Collections.Generic.List<Gateway>();
    System.Collections.Generic.List<Cable> cableList = new System.Collections.Generic.List<Cable>();
    try
    {
      foreach (XElement descendant in xdocument.Descendants((XName) "APILookUpSensor"))
      {
        Sensor sensor = NetworkController.SensorXMLParse(model.AccountID, model.NetworkID, descendant);
        if (sensor != null)
          source.Add(sensor);
      }
      foreach (XElement descendant in xdocument.Descendants((XName) "APILookUpCable"))
      {
        Cable cable = NetworkController.CableXMLParse(descendant);
        if (cable != null)
          cableList.Add(cable);
      }
      foreach (XElement descendant in xdocument.Descendants((XName) "APILookUpGateway"))
      {
        Gateway gateway = NetworkController.GatewayXMLParse(model.NetworkID, descendant);
        if (gateway != null)
          gatewayList.Add(gateway);
      }
    }
    catch (Exception ex)
    {
      this.ModelState.AddModelError("", "Error uploading devices" + ex.Message);
      return (ActionResult) this.View((object) model);
    }
    int num = source.Count<Sensor>();
    eProgramLevel o = MonnitSession.ProgramLevel();
    if (ThemeController.SensorCount() + num > o.ToInt())
      this.ModelState.AddModelError("", $"Only {o.ToString()} sensors allowed for this installation.");
    if (Sensor.LoadByAccountID(account.AccountID).Count + num >= account.CurrentSubscription.AccountSubscriptionType.AllowedSensors)
      this.ModelState.AddModelError("", "Sensor could not be added.  Sensor limit has been reached.");
    if (csNet == null || !MonnitSession.CurrentCustomer.CanSeeNetwork(csNet.CSNetID))
      this.ModelState.AddModelError("", "Sensors could not be added to network");
    else if (csNet.Sensors.Where<Sensor>((Func<Sensor, bool>) (s => s.SensorTypeID != 4L)).Count<Sensor>() + source.Where<Sensor>((Func<Sensor, bool>) (m => m.SensorTypeID != 4L)).Count<Sensor>() >= 500)
      this.ModelState.AddModelError("", "Sensor could not be added.  Sensor limit has been reached.");
    if (this.ModelState.IsValid)
    {
      foreach (Sensor DBObject in source)
      {
        DBObject.LogAuditData(eAuditAction.Create, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "New sensor added");
        try
        {
          DBObject.LastCommunicationDate = new DateTime(2099, 1, 1);
          DBObject.ReadyToShip();
          if (Sensor.Load(DBObject.SensorID) != null)
            DBObject.Save();
          else
            DBObject.ForceInsert();
          DBObject.ResetLastCommunicationDate();
        }
        catch (Exception ex)
        {
          this.ModelState.AddModelError("", "Could not add sensor to database. Message: " + ex.Message);
        }
      }
      foreach (Gateway DBObject in gatewayList)
      {
        DBObject.LogAuditData(eAuditAction.Create, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Created a new gateway");
        try
        {
          if (Gateway.Load(DBObject.GatewayID) != null)
            DBObject.Save();
          else
            DBObject.ForceInsert();
        }
        catch (Exception ex)
        {
          this.ModelState.AddModelError("", "Could not add gateway to database.  Message: " + ex.Message);
        }
      }
      TimedCache.RemoveObject("SensorCount");
      MonnitSession.AccountSensorTotal = int.MinValue;
      if (this.ModelState.IsValid)
        return (ActionResult) this.Content($"<script>alert('Success!');window.location.href = '/Network/Edit/{model.NetworkID.ToString()}';</script>");
    }
    return (ActionResult) this.View((object) model);
  }

  private static Sensor SensorXMLParse(long accountID, long csnetID, XElement element)
  {
    if (element == null)
      return (Sensor) null;
    Sensor sensor = new Sensor();
    sensor.AccountID = accountID;
    sensor.SensorID = element.Attribute((XName) "SensorID").Value.ToLong();
    sensor.ApplicationID = element.Attribute((XName) "ApplicationID").Value.ToLong();
    sensor.SensorName = element.Attribute((XName) "SensorName").Value;
    sensor.SensorTypeID = element.Attribute((XName) "SensorTypeID").Value.ToLong();
    sensor.FirmwareVersion = element.Attribute((XName) "FirmwareVersion").Value;
    sensor.PowerSourceID = element.Attribute((XName) "PowerSourceID").Value.ToLong();
    sensor.RadioBand = element.Attribute((XName) "RadioBand").Value;
    sensor.LastCommunicationDate = new DateTime(2099, 1, 1);
    sensor.IsActive = true;
    sensor.IsSleeping = true;
    sensor.CSNetID = csnetID;
    sensor.ActiveStateInterval = element.Attribute((XName) "ActiveStateInterval").Value.ToDouble();
    sensor.ReportInterval = element.Attribute((XName) "ReportInterval").Value.ToDouble();
    sensor.MinimumCommunicationFrequency = 1;
    sensor.CalibrationCertification = element.Attribute((XName) "CalibrationCertification").Value;
    sensor.GenerationType = element.Attribute((XName) "GenerationType").Value;
    sensor.CableID = element.Attribute((XName) "CableID").Value.ToLong();
    sensor.IsCableEnabled = element.Attribute((XName) "IsCableEnabled").Value.ToBool();
    sensor.SetDefaults(false);
    sensor.SetDefaultCalibration();
    if (element.Attribute((XName) "ReportInterval") != null)
    {
      sensor.ReportInterval = element.Attribute((XName) "ReportInterval").Value.ToDouble();
      sensor.MinimumCommunicationFrequency = sensor.ReportInterval.ToInt() * 2 + 5;
    }
    if (element.Attribute((XName) "ActiveStateInterval") != null)
      sensor.ActiveStateInterval = element.Attribute((XName) "ActiveStateInterval").Value.ToDouble();
    if (element.Attribute((XName) "MeasurementsPerTransmission") != null)
      sensor.MeasurementsPerTransmission = element.Attribute((XName) "MeasurementsPerTransmission").Value.ToInt();
    if (element.Attribute((XName) "TransmitOffset") != null)
      sensor.TransmitOffset = element.Attribute((XName) "TransmitOffset").Value.ToInt();
    if (element.Attribute((XName) "Hysteresis") != null)
      sensor.Hysteresis = element.Attribute((XName) "Hysteresis").Value.ToLong();
    if (element.Attribute((XName) "MinimumThreshold") != null)
      sensor.MinimumThreshold = element.Attribute((XName) "MinimumThreshold").Value.ToLong();
    if (element.Attribute((XName) "MaximumThreshold") != null)
      sensor.MaximumThreshold = element.Attribute((XName) "MaximumThreshold").Value.ToLong();
    if (element.Attribute((XName) "Calibration1") != null)
      sensor.Calibration1 = element.Attribute((XName) "Calibration1").Value.ToLong();
    if (element.Attribute((XName) "Calibration2") != null)
      sensor.Calibration2 = element.Attribute((XName) "Calibration2").Value.ToLong();
    if (element.Attribute((XName) "Calibration3") != null)
      sensor.Calibration3 = element.Attribute((XName) "Calibration3").Value.ToLong();
    if (element.Attribute((XName) "Calibration4") != null)
      sensor.Calibration4 = element.Attribute((XName) "Calibration4").Value.ToLong();
    if (element.Attribute((XName) "EventDetectionType") != null)
      sensor.EventDetectionType = element.Attribute((XName) "EventDetectionType").Value.ToInt();
    if (element.Attribute((XName) "EventDetectionPeriod") != null)
      sensor.EventDetectionPeriod = element.Attribute((XName) "EventDetectionPeriod").Value.ToInt();
    if (element.Attribute((XName) "EventDetectionCount") != null)
      sensor.EventDetectionCount = element.Attribute((XName) "EventDetectionCount").Value.ToInt();
    if (element.Attribute((XName) "RearmTime") != null)
      sensor.RearmTime = element.Attribute((XName) "RearmTime").Value.ToInt();
    if (element.Attribute((XName) "BiStable") != null)
      sensor.BiStable = element.Attribute((XName) "BiStable").Value.ToInt();
    if (element.Attribute((XName) "CableID") != null)
      sensor.CableID = element.Attribute((XName) "CableID").Value.ToLong();
    if (element.Attribute((XName) "IsCableEnabled") != null)
      sensor.IsCableEnabled = element.Attribute((XName) "IsCableEnabled").Value.ToBool();
    return sensor;
  }

  private static Cable CableXMLParse(XElement element)
  {
    if (element == null)
      return (Cable) null;
    return new Cable()
    {
      CableID = element.Attribute((XName) "CableID").Value.ToLong(),
      CreateDate = element.Attribute((XName) "CreateDate").Value.ToDateTime(),
      SKU = element.Attribute((XName) "SKU").Value.ToString(),
      ApplicationID = element.Attribute((XName) "ApplicationID").Value.ToLong(),
      CableMinorRevision = element.Attribute((XName) "CableMinorRevision").Value.ToInt(),
      CableMajorRevision = element.Attribute((XName) "CableMajorRevision").Value.ToInt()
    };
  }

  private static Gateway GatewayXMLParse(long csnetID, XElement element)
  {
    if (element == null)
      return (Gateway) null;
    Gateway gateway = new Gateway();
    gateway.GatewayTypeID = element.Attribute((XName) "GatewayTypeID").Value.ToLong();
    gateway.ResetToDefault(false);
    gateway.GatewayID = element.Attribute((XName) "GatewayID").Value.ToLong();
    gateway.Name = element.Attribute((XName) "GatewayName").Value;
    gateway.RadioBand = element.Attribute((XName) "RadioBand").Value;
    gateway.APNFirmwareVersion = element.Attribute((XName) "APNFirmwareVersion").Value;
    gateway.GatewayFirmwareVersion = element.Attribute((XName) "GatewayFirmwareVersion").Value;
    gateway.MacAddress = element.Attribute((XName) "MacAddress").Value;
    gateway.CSNetID = csnetID;
    gateway.SensorID = element.Attribute((XName) "SensorID").Value.ToLong();
    gateway.GenerationType = element.Attribute((XName) "GenerationType").Value;
    gateway.PowerSourceID = element.Attribute((XName) "PowerSourceID") == null ? 3L : element.Attribute((XName) "PowerSourceID").Value.ToLong();
    gateway.SKU = element.Attribute((XName) "SKU") == null ? "" : element.Attribute((XName) "SKU").Value;
    return gateway;
  }

  [AuthorizeDefault]
  public ActionResult ManualAddSensor()
  {
    CSNet csNet = CSNet.Load(CSNetControllerBase.GetListOfNetworksUserCanSee(new long?(MonnitSession.CurrentCustomer.AccountID)).First<CSNet>().AccountID);
    Sensor model = new Sensor();
    model.AccountID = csNet.AccountID;
    model.CSNetID = csNet.CSNetID;
    model.SensorID = 0L;
    model.ReportInterval = 120.0;
    model.ActiveStateInterval = 120.0;
    model.MinimumCommunicationFrequency = 250;
    this.ViewData["Applications"] = (object) new SelectList((IEnumerable) MonnitApplication.LoadAll().OrderBy<MonnitApplication, string>((Func<MonnitApplication, string>) (ma => ma.ApplicationName)), "ApplicationID", "ApplicationName");
    this.ViewData["PowerSource"] = (object) new SelectList((IEnumerable) PowerSource.LoadAll(), "PowerSourceID", "Name");
    this.ViewData["SensorType"] = (object) new SelectList((IEnumerable) SensorType.LoadAll(), "SensorTypeID", "Name");
    string[] names = Enum.GetNames(typeof (eRadioBand));
    for (int index = 0; index < names.Length; ++index)
      names[index] = names[index].Replace("_", " ").Trim();
    this.ViewData["RadioBand"] = (object) new SelectList((IEnumerable) names);
    return (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [AuthorizeDefault]
  public ActionResult ManualAddSensor(Sensor sensor, FormCollection collection)
  {
    sensor.CSNetID = collection["CSNetID"].ToLong();
    this.ViewData["Applications"] = (object) new SelectList((IEnumerable) MonnitApplication.LoadAll(), "ApplicationID", "ApplicationName", (object) sensor.ApplicationID);
    this.ViewData["PowerSource"] = (object) new SelectList((IEnumerable) PowerSource.LoadAll(), "PowerSourceID", "Name", (object) sensor.PowerSourceID);
    this.ViewData["SensorType"] = (object) new SelectList((IEnumerable) SensorType.LoadAll(), "SensorTypeID", "Name", (object) sensor.SensorTypeID);
    string[] names = Enum.GetNames(typeof (eRadioBand));
    for (int index = 0; index < names.Length; ++index)
      names[index] = names[index].Replace("_", " ").Trim();
    this.ViewData["RadioBand"] = (object) new SelectList((IEnumerable) names, (object) sensor.RadioBand);
    this.ViewData["GatewayID"] = (object) collection["GatewayID"];
    this.ViewData["MacAddress"] = (object) collection["MacAddress"].ToStringSafe().Replace(" ", "").Replace(":", "");
    this.ViewData["GatewayFirmwareVersion"] = (object) collection["GatewayFirmwareVersion"];
    if (sensor.SensorTypeID == 4L || sensor.SensorTypeID == 8L)
    {
      if (string.IsNullOrEmpty(collection["GatewayID"]))
        this.ModelState.AddModelError("GatewayID", "Gateway ID required for WIFI Sensor");
      else if (collection["GatewayID"].ToLong() <= 0L)
        this.ModelState.AddModelError("GatewayID", "Gateway ID must be numeric");
      else if (Gateway.Load(collection["GatewayID"].ToLong()) != null)
        this.ModelState.AddModelError("GatewayID", $"Gateway ID: {collection["GatewayID"]} already exists in database");
      if (string.IsNullOrEmpty(collection["MacAddress"]))
        this.ModelState.AddModelError("MacAddress", "MAC Address required for WIFI Sensor");
      else if (this.ViewData["MacAddress"].ToString().Length != 12)
        this.ModelState.AddModelError("MacAddress", "MAC Address must be 12 digit hexadecimal value");
    }
    eProgramLevel o = MonnitSession.ProgramLevel();
    if (ThemeController.SensorCount() > o.ToInt())
      this.ModelState.AddModelError("SensorID", $"Only {o.ToString()} sensors allowed for this installation.");
    Account account = Account.Load(sensor.AccountID);
    if (Sensor.LoadByAccountID(account.AccountID).Count >= account.CurrentSubscription.AccountSubscriptionType.AllowedSensors)
      this.ModelState.AddModelError("SensorID", $"SensorID: {sensor.SensorID} could not be added.  Sensor limit has been reached.");
    if (this.ModelState.IsValid)
    {
      sensor.LogAuditData(eAuditAction.Create, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "New sensor added");
      try
      {
        sensor.LastCommunicationDate = new DateTime(2099, 1, 1);
        sensor.IsActive = true;
        sensor.IsSleeping = true;
        sensor.SetDefaults(false);
        sensor.ForceInsert();
        TimedCache.RemoveObject("SensorCount");
        MonnitSession.AccountSensorTotal = int.MinValue;
        if (sensor.SensorTypeID == 4L || sensor.SensorTypeID == 8L)
        {
          Gateway gateway = new Gateway();
          gateway.GatewayTypeID = 11L;
          gateway.GatewayID = collection["GatewayID"].ToLong();
          gateway.ResetToDefault(false);
          gateway.Name = $"WiFi Gateway ({sensor.SensorName})";
          gateway.CSNetID = sensor.CSNetID;
          gateway.TransmitPower = 14;
          gateway.GatewayFirmwareVersion = collection["GatewayFirmwareVersion"].ToStringSafe();
          gateway.RadioBand = sensor.RadioBand;
          gateway.APNFirmwareVersion = sensor.FirmwareVersion;
          gateway.MacAddress = this.ViewData["MacAddress"].ToString();
          gateway.IsDirty = false;
          gateway.SensorID = sensor.SensorID;
          gateway.ForceInsert();
        }
        this.ViewData["Result"] = (object) $"Success! Sensor: {sensor.SensorID.ToString()} was added to Network: {CSNet.Load(sensor.CSNetID).Name}";
        sensor.SensorID = 0L;
        return (ActionResult) this.View((object) sensor);
      }
      catch (Exception ex)
      {
        this.ModelState.AddModelError("", ex.Message);
        this.ViewData["Result"] = (object) $"Error: Sensor - {sensor.SensorID.ToString()} {ex.Message}";
        return (ActionResult) this.View((object) sensor);
      }
    }
    else
    {
      this.ViewData["LastID"] = (object) "Error: Could not add Sensor";
      return (ActionResult) this.View((object) sensor);
    }
  }

  [AuthorizeDefault]
  public ActionResult CheckGatewayMove(long id)
  {
    if (!MonnitSession.CustomerCan("Gateway_Can_Change_Network"))
      return (ActionResult) this.Content("showAlertModal('User not authorized to move gateway');");
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
              return (ActionResult) this.Content($"showAlertModal('GatewayID: {id} could not be found');");
            DBObject.CSNetID = ConfigData.AppSettings("DefaultCSNetID").ToLong();
            DBObject.ForceInsert();
          }
        }
        catch
        {
          return (ActionResult) this.Content($"showAlertModal('GatewayID: {id} could not be assigned');");
        }
      }
      CSNet csNet = CSNet.Load(DBObject.CSNetID);
      if (DBObject == null || DBObject.IsDeleted || csNet != null && !MonnitSession.IsAuthorizedForAccount(csNet.AccountID))
        return (ActionResult) this.Content($"showAlertModal('GatewayID: {id} could not be found');");
      string content = "$.get('/Network/MoveGateway/' + $('#CSNetID').val() + '?gatewayID=' + $('#QuickGatewayID').val(), function(data) { if (data != 'Success') { $('#GatewayMoveResult').html(data); } else { window.location.href = '/Network/Edit/' + $('#CSNetID').val() + '?result=GatewayAdded'; } });";
      long num = ConfigData.AppSettings("DefaultCSNetID").ToLong();
      if (DBObject.CSNetID == num || csNet == null)
        return (ActionResult) this.Content(content);
      Account account = Account.Load(csNet.AccountID);
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Moved gateway to different network");
      return (ActionResult) this.Content($"let values = {{}};\r\n                            values.url = '/Network/MoveGateway/' + $('#CSNetID').val() + '?gatewayID=' + $('#QuickGatewayID').val();\r\n                            values.text = {$"'This gateway currently belongs to Account: {account.CompanyName.Replace("'", "\\'")} Network: {(csNet == null ? (object) "Deleted" : (object) csNet.Name.Replace("'", "\\'"))}'"};\r\n                            values.callback = function(data) {{ \r\n                                                if (data != 'Success') {{ \r\n                                                    $('#GatewayMoveResult').html(data); \r\n                                                }} else {{ \r\n                                                    window.location.href = '/Network/Edit/' + $('#CSNetID').val() + '?result=GatewayAdded'; \r\n                                                }} \r\n                                            }};\r\n                                            openConfirm(values); ");
    }
    catch (Exception ex)
    {
      ex.Log($"NetworkController.CheckGatewayMove | GatewayID: {id} could not be transfered to new network");
      return (ActionResult) this.Content($"showAlertModal('GatewayID: {id} could not be transfered to new network');");
    }
  }

  [Authorize]
  public ActionResult MoveGateway(long id, long gatewayID)
  {
    try
    {
      Gateway gateway = Gateway.Load(gatewayID);
      if (!NetworkController.TryMoveGateway(id, gateway))
        return (ActionResult) this.Content($"GatewayID: {gatewayID} could not be transfered to new network");
    }
    catch (Exception ex)
    {
      ex.Log($"NetworkController.MoveGateway | GatewayID: {gatewayID} could not be transfered to new network CSNetID: {id}");
      return (ActionResult) this.Content($"GatewayID: {gatewayID} could not be transfered to new network");
    }
    return (ActionResult) this.Content("Success");
  }

  public static bool TryMoveGateway(long id, long gatewayID)
  {
    try
    {
      return NetworkController.TryMoveGateway(id, Gateway.Load(gatewayID));
    }
    catch
    {
      return false;
    }
  }

  public static bool TryMoveGateway(long id, Gateway gateway)
  {
    CSNet csNet1 = CSNet.Load(id);
    CSNet csNet2 = CSNet.Load(gateway.CSNetID);
    if (csNet1 == null || gateway == null || gateway != null && gateway.IsDeleted || !MonnitSession.CurrentCustomer.CanSeeNetwork(csNet1.CSNetID))
      return false;
    foreach (BaseDBObject baseDbObject in ExternalDataSubscription.LoadByGatewayID(gateway.GatewayID).ToArray())
      baseDbObject.Delete();
    if (csNet2 != null && !csNet2.HoldingOnlyNetwork && csNet2.CSNetID != ConfigData.AppSettings("DefaultCSNetID").ToLong() && !MonnitSession.CurrentCustomer.CanSeeNetwork(gateway.CSNetID))
      return false;
    NetworkAudit.LogNetworkChange(MonnitSession.CurrentCustomer.CustomerID, gateway, new long?());
    if (gateway.SensorID > 0L)
    {
      Sensor sensor = Sensor.Load(gateway.SensorID);
      sensor.AccountID = csNet1.AccountID;
      sensor.CSNetID = csNet1.CSNetID;
      sensor.IsActive = true;
      sensor.IsNewToNetwork = true;
      if (csNet1.AccountID != csNet2.AccountID)
        sensor.StartDate = DateTime.UtcNow;
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
    if (csNet2 == null || csNet1.AccountID != csNet2.AccountID)
    {
      gateway.StartDate = DateTime.UtcNow;
      foreach (Notification notification in GatewayNotification.LoadByGatewayID(gateway.GatewayID))
        notification.RemoveGateway(gateway);
    }
    gateway.Save();
    return true;
  }

  [AuthorizeDefault]
  public ActionResult ManualAddGateway()
  {
    long accountId = MonnitSession.CurrentCustomer.AccountID;
    CSNetControllerBase.GetListOfNetworksUserCanSee(new long?(accountId));
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    Gateway model = new Gateway();
    model.CSNetID = accountId;
    model.GatewayID = 0L;
    model.RadioBand = "";
    model.APNFirmwareVersion = ConfigData.FindValue("DefaultFirmwareVersion");
    model.GatewayFirmwareVersion = " ";
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__35.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__35.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NetworkController.\u003C\u003Eo__35.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__35.\u003C\u003Ep__0, this.ViewBag, CSNet.Load(accountId).AccountID);
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__35.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__35.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, SelectList, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "PowerSourceList", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = NetworkController.\u003C\u003Eo__35.\u003C\u003Ep__1.Target((CallSite) NetworkController.\u003C\u003Eo__35.\u003C\u003Ep__1, this.ViewBag, new SelectList((IEnumerable) PowerSource.LoadAll(), "PowerSourceID", "Name"));
    return (ActionResult) this.View((object) model);
  }

  [HttpPost]
  [AuthorizeDefault]
  public ActionResult ManualAddGateway(Gateway model, FormCollection collection)
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
    model.CSNetID = collection["CSNetID"].ToLong();
    GatewayType gatewayType = GatewayType.Load(model.GatewayTypeID);
    if (string.IsNullOrEmpty(model.GatewayFirmwareVersion))
      model.GatewayFirmwareVersion = gatewayType.LatestGatewayVersion;
    model.RadioBand = model.RadioBand.ToStringSafe().Replace("_", " ").Trim();
    if (this.ModelState.IsValid)
    {
      if (Gateway.Load(model.GatewayID) != null)
      {
        this.ModelState.AddModelError("", "Invalid Access Point ID - it already exists in the Database.");
        this.ViewData["Result"] = (object) "Error: Invalid Access Point ID - it already exists in the Database.";
        return (ActionResult) this.View((object) model);
      }
      try
      {
        Gateway DBObject = new Gateway();
        DBObject.GatewayID = model.GatewayID;
        DBObject.GatewayTypeID = model.GatewayTypeID;
        DBObject.ResetToDefault(false);
        DBObject.Name = model.Name;
        DBObject.CSNetID = model.CSNetID;
        DBObject.GatewayFirmwareVersion = model.GatewayFirmwareVersion;
        DBObject.RadioBand = model.RadioBand;
        DBObject.APNFirmwareVersion = model.APNFirmwareVersion;
        DBObject.MacAddress = model.MacAddress;
        DBObject.IsDirty = false;
        DBObject.ForceInsert();
        Account account = Account.Load(CSNet.Load(DBObject.CSNetID).AccountID);
        DBObject.LogAuditData(eAuditAction.Related_Modify, eAuditObject.Sensor, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Created a new gateway");
        this.ViewData["Result"] = (object) $"Success! Gateway: {DBObject.GatewayID.ToString()} was added to Network: {CSNet.Load(DBObject.CSNetID).Name}";
        return (ActionResult) this.View((object) model);
      }
      catch (Exception ex)
      {
        ex.Log("CSNetController.GatewayCreate");
        if (ex.Message.ToLower().Contains("primary key"))
        {
          this.ModelState.AddModelError("", "Access Point ID - it may already exist in the Database.");
          this.ViewData["Result"] = (object) "Error: Access Point ID - it may already exist in the Database.";
        }
        else
        {
          this.ModelState.AddModelError("", "Unable to Save");
          this.ViewData["Result"] = (object) "Error: Unable to Save";
        }
        return (ActionResult) this.View((object) model);
      }
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      if (NetworkController.\u003C\u003Eo__36.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NetworkController.\u003C\u003Eo__36.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = NetworkController.\u003C\u003Eo__36.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__36.\u003C\u003Ep__0, this.ViewBag, CSNet.Load(model.CSNetID).AccountID);
      // ISSUE: reference to a compiler-generated field
      if (NetworkController.\u003C\u003Eo__36.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NetworkController.\u003C\u003Eo__36.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, SelectList, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "PowerSourceList", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = NetworkController.\u003C\u003Eo__36.\u003C\u003Ep__1.Target((CallSite) NetworkController.\u003C\u003Eo__36.\u003C\u003Ep__1, this.ViewBag, new SelectList((IEnumerable) PowerSource.LoadAll(), "PowerSourceID", "Name"));
      this.ViewData["Result"] = (object) "Error: Unable to Save";
      return (ActionResult) this.View((object) model);
    }
  }

  [AuthorizeDefault]
  public ActionResult GatewaysToUpdate(long? id)
  {
    long num = id ?? MonnitSession.CurrentCustomer.AccountID;
    Account account = Account.Load(num);
    if (!MonnitSession.CustomerCan("Customer_Can_Update_Firmware") || account == null || !MonnitSession.IsAuthorizedForAccount(num))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__37.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__37.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = NetworkController.\u003C\u003Eo__37.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__37.\u003C\u003Ep__0, this.ViewBag, num);
    return (ActionResult) this.View((object) NetworkController.UpdateableGateways(num));
  }

  [AuthorizeDefault]
  public ActionResult GatewaysToUpdateRefresh(long id, string nameFilter, long gatewayTypeFilter)
  {
    Account account = Account.Load(id);
    if (!MonnitSession.CustomerCan("Customer_Can_Update_Firmware") || account == null || !MonnitSession.IsAuthorizedForAccount(account.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    int num1 = 0;
    int num2 = 0;
    System.Collections.Generic.List<Gateway> model = new System.Collections.Generic.List<Gateway>();
    foreach (Gateway updateableGateway in NetworkController.UpdateableGateways(account.AccountID))
    {
      ++num1;
      if ((gatewayTypeFilter <= 0L || updateableGateway.GatewayTypeID == gatewayTypeFilter) && (string.IsNullOrEmpty(nameFilter) || updateableGateway.Name.ToUpper().Contains(nameFilter.ToUpper())))
      {
        ++num2;
        model.Add(updateableGateway);
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__38.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__38.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalGateways", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NetworkController.\u003C\u003Eo__38.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__38.\u003C\u003Ep__0, this.ViewBag, num1);
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__38.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__38.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "FilteredGateways", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = NetworkController.\u003C\u003Eo__38.\u003C\u003Ep__1.Target((CallSite) NetworkController.\u003C\u003Eo__38.\u003C\u003Ep__1, this.ViewBag, num2);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult SensorsToUpdate(long? id)
  {
    long num = id ?? MonnitSession.CurrentCustomer.AccountID;
    Account account = Account.Load(num);
    if (!MonnitSession.CustomerCan("Customer_Can_Update_Firmware") || account == null || !MonnitSession.IsAuthorizedForAccount(num))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__39.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__39.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = NetworkController.\u003C\u003Eo__39.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__39.\u003C\u003Ep__0, this.ViewBag, num);
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public ActionResult SensorsToUpdateRefresh(long id, string nameFilter, long applicationFilter)
  {
    Account account = Account.Load(id);
    if (!MonnitSession.CustomerCan("Customer_Can_Update_Firmware") || account == null || !MonnitSession.IsAuthorizedForAccount(account.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    int num1 = 0;
    int num2 = 0;
    System.Collections.Generic.List<Sensor> model = new System.Collections.Generic.List<Sensor>();
    Dictionary<string, string> LatestVersions = new Dictionary<string, string>();
    foreach (Sensor updateableSensor in NetworkController.UpdateableSensors(account.AccountID, MonnitSession.CustomerCan("Support_Advanced"), ref LatestVersions))
    {
      ++num1;
      if ((applicationFilter <= 0L || updateableSensor.ApplicationID == applicationFilter) && (string.IsNullOrEmpty(nameFilter) || updateableSensor.SensorName.ToUpper().Contains(nameFilter.ToUpper())))
      {
        ++num2;
        model.Add(updateableSensor);
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__40.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__40.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TotalSensors", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NetworkController.\u003C\u003Eo__40.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__40.\u003C\u003Ep__0, this.ViewBag, num1);
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__40.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__40.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "FilteredSensors", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = NetworkController.\u003C\u003Eo__40.\u003C\u003Ep__1.Target((CallSite) NetworkController.\u003C\u003Eo__40.\u003C\u003Ep__1, this.ViewBag, num2);
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__40.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__40.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, Dictionary<string, string>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "LatestVersions", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = NetworkController.\u003C\u003Eo__40.\u003C\u003Ep__2.Target((CallSite) NetworkController.\u003C\u003Eo__40.\u003C\u003Ep__2, this.ViewBag, LatestVersions);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public static System.Collections.Generic.List<Sensor> UpdateableSensors(
    long accountID,
    bool AdvancedSupport,
    ref Dictionary<string, string> LatestVersions)
  {
    System.Collections.Generic.List<Sensor> sensorList = new System.Collections.Generic.List<Sensor>();
    Dictionary<long, Gateway> dictionary = NetworkController.OTASuiteGateways(accountID);
    string minVersion = "16.34.0.0";
    if (AdvancedSupport)
      minVersion = "10.0.0.0";
    DataMessage.CacheLastByAccount(accountID, new TimeSpan(0, 1, 0));
    foreach (Sensor sensor in OTARequest.LoadSensorsToUpdate(accountID, minVersion))
    {
      DataMessage dataMessage = DataMessage.LoadLastBySensorQuickCached(sensor);
      if (dataMessage == null || dataMessage.DataMessageGUID == Guid.Empty)
        dataMessage = DataMessage.LoadLastBySensor(sensor);
      if (dataMessage != null && dictionary.ContainsKey(dataMessage.GatewayID))
      {
        if (!LatestVersions.ContainsKey(sensor.SKU))
        {
          SKUFirmware skuFirmware = SKUFirmware.LatestFirmware(sensor.SKU);
          if (skuFirmware != null)
            LatestVersions.Add(sensor.SKU, skuFirmware.Version);
        }
        string str;
        if (LatestVersions.TryGetValue(sensor.SKU, out str) && str != sensor.FirmwareVersion && sensor.FirmwareVersion != "26.55.51.23")
        {
          SKUFirmware skuFirmware = SKUFirmware.LatestFirmware(sensor.SKU);
          if (skuFirmware != null && !skuFirmware.FlashOnlyFirmware.Contains(sensor.FirmwareVersion) && (AdvancedSupport || dataMessage.SignalStrength > -94))
            sensorList.Add(sensor);
        }
      }
    }
    return sensorList;
  }

  public static Dictionary<long, Gateway> OTASuiteGateways(long accountID)
  {
    Dictionary<long, Gateway> dictionary = new Dictionary<long, Gateway>();
    foreach (Gateway gateway in Gateway.LoadByAccountID(accountID))
    {
      if (!string.IsNullOrEmpty(gateway.GatewayType.MinOTAFirmwareVersion) && !string.IsNullOrEmpty(gateway.GatewayFirmwareVersion) && new Version(gateway.GatewayType.MinOTAFirmwareVersion) <= new Version(gateway.GatewayFirmwareVersion) && (new Version("20.40.0.0") <= new Version(gateway.APNFirmwareVersion) || gateway.GatewayTypeID == 38L))
        dictionary.Add(gateway.GatewayID, gateway);
    }
    return dictionary;
  }

  public static System.Collections.Generic.List<Gateway> UpdateableGateways(long accountID)
  {
    System.Collections.Generic.List<Gateway> gatewayList = new System.Collections.Generic.List<Gateway>();
    foreach (Gateway gateway in Gateway.LoadByAccountID(accountID))
    {
      if (!gateway.ForceToBootloader && !gateway.UpdateRadioFirmware && !gateway.RadioFirmwareUpdateInProgress && !string.IsNullOrEmpty(gateway.SKU) && string.IsNullOrEmpty(gateway.GatewayType.LatestGatewayPath) && gateway.GatewayType.SupportsOTASuite)
      {
        Version version1 = MonnitSession.LatestVersion(gateway.SKU, false);
        Version version2 = MonnitSession.LatestVersion(gateway.SKU, true);
        if (!string.IsNullOrEmpty(gateway.GatewayFirmwareVersion))
        {
          try
          {
            if (version1 != (Version) null && version1 > new Version(gateway.GatewayFirmwareVersion))
              gatewayList.Add(gateway);
            else if (gateway.GatewayType.SupportsOTASuiteBSN && version2 != (Version) null)
            {
              Version version3 = new Version(gateway.APNFirmwareVersion);
              if (version2 > version3 && (version3.Build > 1 || version3.Build == 1 && version3.Revision > 0))
                gatewayList.Add(gateway);
            }
          }
          catch
          {
          }
        }
      }
    }
    return gatewayList;
  }

  [AuthorizeDefault]
  public ActionResult PendingGatewayUpdateRefresh(long id)
  {
    Account account = Account.Load(id);
    if (!MonnitSession.CustomerCan("Customer_Can_Update_Firmware") || account == null || !MonnitSession.IsAuthorizedForAccount(id))
      return (ActionResult) this.Content("Unauthorized");
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__44.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__44.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = NetworkController.\u003C\u003Eo__44.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__44.\u003C\u003Ep__0, this.ViewBag, id);
    return (ActionResult) this.View((object) Gateway.LoadByAccountID(account.AccountID).Where<Gateway>((Func<Gateway, bool>) (c => c.ForceToBootloader || c.RadioFirmwareUpdateInProgress || c.UpdateRadioFirmware)).ToList<Gateway>().Where<Gateway>((Func<Gateway, bool>) (c => c.GatewayType.SupportsOTASuite)).ToList<Gateway>());
  }

  [AuthorizeDefault]
  public ActionResult PendingUpdateRefresh(long id)
  {
    Account account = Account.Load(id);
    if (!MonnitSession.CustomerCan("Customer_Can_Update_Firmware") || account == null || !MonnitSession.IsAuthorizedForAccount(id))
      return (ActionResult) this.Content("Unauthorized");
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__45.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__45.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = NetworkController.\u003C\u003Eo__45.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__45.\u003C\u003Ep__0, this.ViewBag, id);
    return (ActionResult) this.View((object) OTARequest.LoadActiveByAccountID(id));
  }

  [AuthorizeDefault]
  public ActionResult PendingNextSensorsUpdateRefresh(long id, string gatewayIDs)
  {
    Account account = Account.Load(id);
    if (!MonnitSession.CustomerCan("Customer_Can_Update_Firmware") || account == null || !MonnitSession.IsAuthorizedForAccount(id))
      return (ActionResult) this.Content("Unauthorized");
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__46.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__46.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = NetworkController.\u003C\u003Eo__46.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__46.\u003C\u003Ep__0, this.ViewBag, id);
    return (ActionResult) this.View((object) Gateway.LoadByAccountID(account.AccountID).Where<Gateway>((Func<Gateway, bool>) (c => c.ForceToBootloader && c.GatewayTypeID == 38L)).ToList<Gateway>().Where<Gateway>((Func<Gateway, bool>) (c => c.GatewayType.SupportsOTASuite)).ToList<Gateway>());
  }

  [AuthorizeDefault]
  public ActionResult CreateGatewayUpdateRequest(long id, string gatewayIDs)
  {
    Account account = Account.Load(id);
    if (!MonnitSession.CustomerCan("Customer_Can_Update_Firmware") || account == null || !MonnitSession.IsAuthorizedForAccount(account.AccountID))
      return (ActionResult) this.Content("Unauthorized");
    new StringBuilder().Append("Succeeded for (GatewayID):");
    StringBuilder stringBuilder1 = new StringBuilder();
    stringBuilder1.Append("Failed for (GatewayID):");
    StringBuilder stringBuilder2 = new StringBuilder();
    bool flag1 = false;
    System.Collections.Generic.List<NetworkController.GatewayOTAResult> first = new System.Collections.Generic.List<NetworkController.GatewayOTAResult>();
    System.Collections.Generic.List<NetworkController.GatewayOTAResult> second = new System.Collections.Generic.List<NetworkController.GatewayOTAResult>();
    long minValue = long.MinValue;
    string[] strArray = gatewayIDs.Split(new char[1]{ '|' }, StringSplitOptions.RemoveEmptyEntries);
    System.Collections.Generic.List<Gateway> gatewayList = new System.Collections.Generic.List<Gateway>();
    foreach (string o in strArray)
    {
      minValue = long.MinValue;
      Gateway DBObject = Gateway.Load(o.ToLong());
      if (DBObject != null)
      {
        long gatewayId = DBObject.GatewayID;
        if (MonnitSession.CurrentCustomer.CanSeeNetwork(DBObject.CSNetID))
        {
          try
          {
            bool flag2 = DBObject.GatewayType.SupportsOTASuite;
            if (!flag2)
              flag2 = !string.IsNullOrWhiteSpace(DBObject.GatewayType.LatestGatewayPath) && !string.IsNullOrEmpty(DBObject.GatewayType.LatestGatewayVersion);
            if (flag2)
            {
              string str1 = DBObject.GatewayType.LatestGatewayVersion;
              if (DBObject.GatewayType.SupportsOTASuite)
                str1 = !MonnitSession.IsEnterprise ? MonnitUtil.GetLatestFirmwareVersionFromMEA(DBObject.SKU, true) : MonnitUtil.GetLatestEncryptedFirmwareVersion(DBObject.SKU, true);
              if (!string.IsNullOrEmpty(str1) && !str1.Contains("Failed") && str1 != DBObject.GatewayFirmwareVersion)
              {
                DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Updated gateway firmware");
                DBObject.ForceToBootloader = true;
                if (DBObject.IsUnlocked && DBObject.GatewayType.SupportsHostAddress)
                  DBObject.SendUnlockRequest = true;
                DBObject.Save();
              }
              if (DBObject.GenerationType.Contains("Gen2") && DBObject.GatewayType.SupportsOTASuite && new Version(DBObject.GatewayFirmwareVersion) >= new Version(DBObject.GatewayType.MinOTAFirmwareVersion))
              {
                Version version = new Version(DBObject.APNFirmwareVersion);
                if (version.Build > 1 || version.Build == 1 && version.Revision > 0)
                {
                  string str2 = !MonnitSession.IsEnterprise ? MonnitUtil.GetLatestFirmwareVersionFromMEA(DBObject.SKU, false) : MonnitUtil.GetLatestEncryptedFirmwareVersion(DBObject.SKU, false);
                  if (!string.IsNullOrEmpty(str2) && !str2.Contains("Failed") && str2 != DBObject.APNFirmwareVersion)
                  {
                    SKUFirmware skuFirmware = SKUFirmware.LatestFirmware(DBObject.SKU);
                    if (skuFirmware != null)
                    {
                      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Updated gateway radio firmware");
                      DBObject.RadioFirmwareUpdateID = skuFirmware.FirmwareID;
                      DBObject.UpdateRadioFirmware = true;
                      DBObject.Save();
                      first.Add(new NetworkController.GatewayOTAResult(true, DBObject.GatewayID.ToString()));
                    }
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            ExceptionLog.Log(ex);
            flag1 = true;
            stringBuilder1.Append($" ({gatewayId})");
            stringBuilder2.Append(ex.Message + "\r\n");
            second.Add(new NetworkController.GatewayOTAResult(false, DBObject.GatewayID.ToString()));
          }
        }
      }
    }
    MonnitSession.InvalidateCachedUpdateableGateways(MonnitSession.CurrentCustomer.AccountID);
    MonnitSession.InvalidateCachedOTASuiteGateways(MonnitSession.CurrentCustomer.AccountID);
    return (ActionResult) this.Content(JsonConvert.SerializeObject((object) first.Union<NetworkController.GatewayOTAResult>((IEnumerable<NetworkController.GatewayOTAResult>) second)));
  }

  [AuthorizeDefault]
  public ActionResult CreateOTARequest(long id, string sensorIDs)
  {
    Account account = Account.Load(id);
    if (!MonnitSession.CustomerCan("Customer_Can_Update_Firmware") || account == null || !MonnitSession.IsAuthorizedForAccount(account.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    string[] strArray = sensorIDs.Split(new char[1]{ '|' }, StringSplitOptions.RemoveEmptyEntries);
    System.Collections.Generic.List<Sensor> sensorList = new System.Collections.Generic.List<Sensor>();
    foreach (object o in strArray)
    {
      Sensor sensor = Sensor.Load(o.ToLong());
      if (sensor != null && sensor.LastDataMessage != null)
        sensorList.Add(sensor);
    }
    Dictionary<long, Dictionary<string, OTARequest>> dictionary = new Dictionary<long, Dictionary<string, OTARequest>>();
    foreach (Sensor sensor in sensorList)
    {
      if (sensor.SensorTypeID != 8L)
      {
        OTARequest otaRequest = (OTARequest) null;
        if (dictionary.ContainsKey(sensor.LastDataMessage.GatewayID) && dictionary[sensor.LastDataMessage.GatewayID].ContainsKey(sensor.SKU))
          otaRequest = dictionary[sensor.LastDataMessage.GatewayID][sensor.SKU];
        if (otaRequest == null)
        {
          SKUFirmware skuFirmware = SKUFirmware.LatestFirmware(sensor.SKU);
          if (skuFirmware != null)
          {
            otaRequest = new OTARequest();
            otaRequest.ApplicationID = sensor.ApplicationID;
            otaRequest.AccountID = account.AccountID;
            otaRequest.GatewayID = sensor.LastDataMessage.GatewayID;
            otaRequest.CreatedByID = MonnitSession.CurrentCustomer.CustomerID;
            otaRequest.CreateDate = DateTime.UtcNow;
            otaRequest.FirmwareID = skuFirmware.FirmwareID;
            otaRequest.Version = skuFirmware.Version;
            otaRequest.SKU = sensor.SKU;
            otaRequest.Status = eOTAStatus.New;
            otaRequest.Save();
            if (!dictionary.ContainsKey(sensor.LastDataMessage.GatewayID))
            {
              dictionary.Add(sensor.LastDataMessage.GatewayID, new Dictionary<string, OTARequest>());
              Gateway gateway = Gateway.Load(sensor.LastDataMessage.GatewayID);
              if (gateway != null && !gateway.OTARequestActive)
              {
                gateway.OTARequestActive = true;
                gateway.Save();
              }
            }
            dictionary[sensor.LastDataMessage.GatewayID].Add(sensor.SKU, otaRequest);
          }
          else
            continue;
        }
        new OTARequestSensor()
        {
          OTARequestID = otaRequest.OTARequestID,
          SensorID = sensor.SensorID,
          Status = eOTAStatus.New
        }.Save();
      }
      else
      {
        Gateway gateway = Gateway.LoadBySensorID(sensor.SensorID);
        gateway.ForceToBootloader = true;
        if (gateway.IsUnlocked)
          gateway.SendUnlockRequest = true;
        gateway.Save();
      }
    }
    MonnitSession.InvalidateCachedOTASuiteSensors(MonnitSession.CurrentCustomer.AccountID);
    return (ActionResult) null;
  }

  [AuthorizeDefault]
  public ActionResult CancelOTAGatewayRequest(long id)
  {
    Gateway gateway = Gateway.Load(id);
    if (gateway == null)
      return (ActionResult) this.Content("Invalid Request");
    Account account = Account.Load(gateway.AccountID);
    if (account == null || !MonnitSession.IsAuthorizedForAccount(account.AccountID))
      return (ActionResult) this.Content("Invalid Request");
    gateway.ForceToBootloader = false;
    gateway.UpdateRadioFirmware = false;
    gateway.RadioFirmwareUpdateInProgress = false;
    gateway.RadioFirmwareUpdateID = long.MinValue;
    gateway.Save();
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult CancelOTARequest(long id)
  {
    OTARequest otaRequest = OTARequest.Load(id);
    if (otaRequest == null)
      return (ActionResult) this.Content("Invalid Request");
    Account account = Account.Load(otaRequest.AccountID);
    if (account == null || !MonnitSession.IsAuthorizedForAccount(account.AccountID))
      return (ActionResult) this.Content("Invalid Request");
    foreach (OTARequestSensor otaRequestSensor in otaRequest.OTARequestSensors)
    {
      switch (otaRequestSensor.Status)
      {
        case eOTAStatus.New:
        case eOTAStatus.Processing:
          otaRequestSensor.Status = eOTAStatus.Canceled;
          otaRequestSensor.Save();
          break;
      }
    }
    otaRequest.Status = eOTAStatus.Canceled;
    otaRequest.Save();
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult RapidUpdate()
  {
    return !MonnitSession.CustomerCan("CanUseSpecialUpdate") ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) this.LoadModels());
  }

  [AuthorizeDefault]
  public ActionResult RapidUpdateRefresh()
  {
    return !MonnitSession.CustomerCan("CanUseSpecialUpdate") ? (ActionResult) this.Content("Failed") : (ActionResult) this.View((object) this.LoadModels());
  }

  [AuthorizeDefault]
  private System.Collections.Generic.List<RapidUpdateSensorModel> LoadModels()
  {
    System.Collections.Generic.List<RapidUpdateSensorModel> updateSensorModelList = new System.Collections.Generic.List<RapidUpdateSensorModel>();
    string str = (this.Session["RapidUpdateGateways"] ?? (object) "").ToString();
    char[] chArray = new char[1]{ '|' };
    foreach (object o in str.Split(chArray))
    {
      long ID = o.ToLong();
      RapidUpdateSensorModel updateSensorModel = new RapidUpdateSensorModel();
      updateSensorModel.Gateway = Gateway.Load(ID);
      if (updateSensorModel.Gateway != null)
      {
        updateSensorModel.Sensor = Sensor.LoadByCsNetID(updateSensorModel.Gateway.CSNetID).FirstOrDefault<Sensor>();
        updateSensorModelList.Add(updateSensorModel);
      }
    }
    return updateSensorModelList;
  }

  [AuthorizeDefault]
  public ActionResult AddRapidUpdateGateway(string gatewayTag)
  {
    if (!MonnitSession.CustomerCan("CanUseSpecialUpdate"))
      return (ActionResult) this.Content("Failed");
    string[] strArray = gatewayTag.Split(':');
    long input = strArray[0].ToLong();
    string checkDigit = strArray[1];
    if (!MonnitUtil.ValidateCheckDigit(input, checkDigit))
      return (ActionResult) this.Content("Failed");
    string str = (this.Session["RapidUpdateGateways"] ?? (object) "").ToString();
    if (str.Length > 0)
      str += "|";
    this.Session["RapidUpdateGateways"] = (object) (str + input.ToString());
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult RemoveRapidUpdateGateway(string gatewayID)
  {
    if (!MonnitSession.CustomerCan("CanUseSpecialUpdate"))
      return (ActionResult) this.Content("Failed");
    this.Session["RapidUpdateGateways"] = (object) (this.Session["RapidUpdateGateways"] ?? (object) "").ToString().Replace(gatewayID, "").Replace("||", "|").Trim('|');
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult AddRapidUpdateSensor(string sensorTag, long networkID)
  {
    if (!MonnitSession.CustomerCan("CanUseSpecialUpdate"))
      return (ActionResult) this.Content("Failed");
    string[] strArray = sensorTag.Split(':');
    long num = strArray[0].ToLong();
    string checkDigit = strArray[1];
    return MonnitUtil.ValidateCheckDigit(num, checkDigit) && RapidUpdateSensorModel.AddSensor(num, networkID) ? (ActionResult) this.Content("Success") : (ActionResult) this.Content("Failed");
  }

  [AuthorizeDefault]
  public ActionResult SensorCertificateExpiring(long id)
  {
    Account acct = Account.Load(id);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    // ISSUE: reference to a compiler-generated field
    if (NetworkController.\u003C\u003Eo__58.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NetworkController.\u003C\u003Eo__58.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, long, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AccountID", typeof (NetworkController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = NetworkController.\u003C\u003Eo__58.\u003C\u003Ep__0.Target((CallSite) NetworkController.\u003C\u003Eo__58.\u003C\u003Ep__0, this.ViewBag, id);
    CertificateNotification.LoadByCustomerID(MonnitSession.CurrentCustomer.CustomerID);
    return (ActionResult) this.View();
  }

  [AuthorizeDefault]
  public ActionResult LoadCertificateExpiringSensors(long id)
  {
    Account acct = Account.Load(id);
    return acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.PartialView("_LoadCertificateExpiringSensors", (object) CertificateNotification.LoadByCustomerID(MonnitSession.CurrentCustomer.CustomerID));
  }

  private struct GatewayOTAResult(bool success, string gatewayID)
  {
    private const string successIcon = "<svg id=\"green-pass\" xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 512 512\" style=\"fill:green\"><path d=\"M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM369 209L241 337c-9.4 9.4-24.6 9.4-33.9 0l-64-64c-9.4-9.4-9.4-24.6 0-33.9s24.6-9.4 33.9 0l47 47L335 175c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9z\"/></svg>";
    private const string failureIcon = "<svg id=\"fail-icon\" xmlns=\"http://www.w3.org/2000/svg\"  viewBox=\"0 0 512 512\" style=\"fill:#ca0005\"><path d=\"M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM175 175c9.4-9.4 24.6-9.4 33.9 0l47 47 47-47c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9l-47 47 47 47c9.4 9.4 9.4 24.6 0 33.9s-24.6 9.4-33.9 0l-47-47-47 47c-9.4 9.4-24.6 9.4-33.9 0s-9.4-24.6 0-33.9l47-47-47-47c-9.4-9.4-9.4-24.6 0-33.9z\"/></svg>";

    public string ResultIcon { get; private set; } = success ? "<svg id=\"green-pass\" xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 512 512\" style=\"fill:green\"><path d=\"M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM369 209L241 337c-9.4 9.4-24.6 9.4-33.9 0l-64-64c-9.4-9.4-9.4-24.6 0-33.9s24.6-9.4 33.9 0l47 47L335 175c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9z\"/></svg>" : "<svg id=\"fail-icon\" xmlns=\"http://www.w3.org/2000/svg\"  viewBox=\"0 0 512 512\" style=\"fill:#ca0005\"><path d=\"M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM175 175c9.4-9.4 24.6-9.4 33.9 0l47 47 47-47c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9l-47 47 47 47c9.4 9.4 9.4 24.6 0 33.9s-24.6 9.4-33.9 0l-47-47-47 47c-9.4 9.4-24.6 9.4-33.9 0s-9.4-24.6 0-33.9l47-47-47-47c-9.4-9.4-9.4-24.6 0-33.9z\"/></svg>";

    public string GatewayID { get; private set; } = gatewayID;
  }
}
