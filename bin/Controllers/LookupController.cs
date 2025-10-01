// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.LookupController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.Models;
using Monnit;
using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;

#nullable disable
namespace iMonnit.Controllers;

public class LookupController : ThemeController
{
  [Authorize]
  public ActionResult MonnitApplication(string q, string limit)
  {
    return (ActionResult) this.Content(string.Join("\n", Monnit.MonnitApplication.LoadAll().Where<Monnit.MonnitApplication>((Func<Monnit.MonnitApplication, bool>) (app => app.ApplicationName.ToLower().Contains(q.ToLower()))).Select<Monnit.MonnitApplication, string>((Func<Monnit.MonnitApplication, string>) (app => app.ApplicationName)).Take<string>(limit.ToInt()).ToArray<string>()));
  }

  [Authorize]
  public ActionResult MonnitApplicationID(string name)
  {
    long num = Monnit.MonnitApplication.LoadAll().Where<Monnit.MonnitApplication>((Func<Monnit.MonnitApplication, bool>) (app => app.ApplicationName.ToLower().Trim() == name.ToLower().Trim())).Select<Monnit.MonnitApplication, long>((Func<Monnit.MonnitApplication, long>) (app => app.ApplicationID)).FirstOrDefault<long>();
    return num == 0L ? (ActionResult) this.Content("Not Found") : (ActionResult) this.Content(num.ToString());
  }

  [Authorize]
  public ActionResult Customer(string q, string limit)
  {
    return this.AccountCustomer(q, limit, MonnitSession.CurrentCustomer.AccountID.ToString());
  }

  [Authorize]
  public ActionResult CustomerID(string name)
  {
    return this.AccountCustomerID(name, MonnitSession.CurrentCustomer.AccountID.ToString());
  }

  [Authorize]
  public ActionResult AccountCustomer(string q, string limit, string accountID)
  {
    return !MonnitSession.IsAuthorizedForAccount(accountID.ToLong()) ? (ActionResult) this.Content("Unauthorized Access") : (ActionResult) this.Content(string.Join("\n", Monnit.Customer.LoadAllByAccount(accountID.ToLong()).Where<Monnit.Customer>((Func<Monnit.Customer, bool>) (cust => cust.UniqueName.ToLower().Contains(q.ToLower()))).Select<Monnit.Customer, string>((Func<Monnit.Customer, string>) (cust => cust.UniqueName)).Take<string>(limit.ToInt()).ToArray<string>()));
  }

  [Authorize]
  public ActionResult AccountCustomerID(string name, string accountID)
  {
    if (!MonnitSession.IsAuthorizedForAccount(accountID.ToLong()))
      return (ActionResult) this.Content("Unauthorized Access");
    long num = Monnit.Customer.LoadAllByAccount(accountID.ToLong()).Where<Monnit.Customer>((Func<Monnit.Customer, bool>) (cust => cust.UniqueName.ToLower().Trim() == name.ToLower().Trim())).Select<Monnit.Customer, long>((Func<Monnit.Customer, long>) (cust => cust.CustomerID)).FirstOrDefault<long>();
    return num == 0L ? (ActionResult) this.Content("Not Found") : (ActionResult) this.Content(num.ToString());
  }

  [Authorize]
  public ActionResult SMSCarrier(string q, string limit)
  {
    return (ActionResult) this.Content(string.Join("\n", Monnit.SMSCarrier.LoadAll().Where<Monnit.SMSCarrier>((Func<Monnit.SMSCarrier, bool>) (cust => cust.SMSCarrierName.ToLower().Contains(q.ToLower()))).Select<Monnit.SMSCarrier, string>((Func<Monnit.SMSCarrier, string>) (cust => cust.SMSCarrierName)).Take<string>(limit.ToInt()).ToArray<string>()));
  }

  [Authorize]
  public ActionResult SMSCarrierID(string name)
  {
    long num = Monnit.SMSCarrier.LoadAll().Where<Monnit.SMSCarrier>((Func<Monnit.SMSCarrier, bool>) (carrier => carrier.SMSCarrierName.ToLower().Trim() == name.ToLower().Trim())).Select<Monnit.SMSCarrier, long>((Func<Monnit.SMSCarrier, long>) (carrier => carrier.SMSCarrierID)).FirstOrDefault<long>();
    return num == 0L ? (ActionResult) this.Content("Not Found") : (ActionResult) this.Content(num.ToString());
  }

  [Authorize]
  public ActionResult EmailTemplate(string q, string limit)
  {
    return !MonnitSession.IsAuthorizedForAccount(MonnitSession.CurrentCustomer.AccountID) ? (ActionResult) this.Content("Unauthorized Access") : (ActionResult) this.Content(string.Join("\n", Monnit.EmailTemplate.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where<Monnit.EmailTemplate>((Func<Monnit.EmailTemplate, bool>) (temp => temp.Name.ToLower().Contains(q.ToLower()))).Select<Monnit.EmailTemplate, string>((Func<Monnit.EmailTemplate, string>) (temp => temp.Name)).Take<string>(limit.ToInt()).ToArray<string>()));
  }

  [Authorize]
  public ActionResult EmailTemplateID(string name)
  {
    if (!MonnitSession.IsAuthorizedForAccount(MonnitSession.CurrentCustomer.AccountID))
      return (ActionResult) this.Content("Unauthorized Access");
    long num = Monnit.EmailTemplate.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where<Monnit.EmailTemplate>((Func<Monnit.EmailTemplate, bool>) (temp => temp.Name.ToLower().Trim() == name.ToLower().Trim())).Select<Monnit.EmailTemplate, long>((Func<Monnit.EmailTemplate, long>) (temp => temp.EmailTemplateID)).FirstOrDefault<long>();
    return num == 0L ? (ActionResult) this.Content("Not Found") : (ActionResult) this.Content(num.ToString());
  }

  public ActionResult SensorXmlData(long id) => (ActionResult) this.View((object) Sensor.Load(id));

  public ActionResult WifiXmlData(long id) => (ActionResult) this.View((object) Sensor.Load(id));

  [AuthorizeDefault]
  public ActionResult NetworkDevicesXmlData(long id)
  {
    CSNet model = CSNet.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID) || !MonnitSession.CurrentCustomer.CanSeeNetwork(model.CSNetID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (MonnitSession.CustomerCan("Network_Edit"))
      return (ActionResult) this.View((object) model);
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
    {
      ErrorLocation = "Network",
      ErrorTranslateTag = "Network/Edit|",
      ErrorTitle = "Unauthorized access to edit networks",
      ErrorMessage = "You do not have permission to access this page."
    });
  }

  public ActionResult GatewayXmlData(long id)
  {
    return (ActionResult) this.View((object) Gateway.Load(id));
  }

  [Authorize]
  public ActionResult SensorName(string q, string limit)
  {
    return (ActionResult) this.Content(string.Join("\n", Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where<Sensor>((Func<Sensor, bool>) (temp => temp.SensorName.ToLower().Contains(q.ToLower()))).Select<Sensor, string>((Func<Sensor, string>) (temp => temp.SensorName)).Take<string>(limit.ToInt()).ToArray<string>()));
  }

  [Authorize]
  public ActionResult NetworkName(string q, string limit)
  {
    return (ActionResult) this.Content(string.Join("\n", CSNet.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where<CSNet>((Func<CSNet, bool>) (temp => temp.Name.ToLower().Contains(q.ToLower()))).Select<CSNet, string>((Func<CSNet, string>) (temp => temp.Name)).Take<string>(limit.ToInt()).ToArray<string>()));
  }

  public ActionResult ProductActivation() => (ActionResult) this.View();

  public ActionResult ProductActivationKey(string key)
  {
    DateTime utcNow = DateTime.UtcNow;
    string uri = "";
    string str1 = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str1) && bool.Parse(str1);
    try
    {
      string str2 = ConfigData.FindValue("MEA_API_Location");
      string str3 = ConfigData.FindValue("MEA_API_Auth_Guid");
      string str4 = XDocument.Load(string.Format(str2 + "xml/CreateAuthToken/?applicationAuthGuid={0}", (object) str3)).Descendants((XName) "object").Attributes((XName) "auth").First<XAttribute>().Value.ToString();
      string str5 = Encoding.UTF8.GetString(Convert.FromBase64String(key)).Split('|')[0];
      uri = string.Format($"{str2}xml/IsPremiereValid/{str4}?guid={{0}}", (object) str5);
      XDocument xdocument = XDocument.Load(uri);
      string str6 = xdocument.Root.Value.ToString().Replace("IsPremiereValid", "");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-ProductActivationKey.IsPremiereValid",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = xdocument.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      if (str6.Contains("Failed"))
        return (ActionResult) this.Content("Invalid Activation Key");
      return !str6.Contains("Premiere") ? (ActionResult) this.Content(XDocument.Load(string.Format(str2 + "xml/RetreiveEnterpriseActivationKey/?key={0}", (object) key)).Root.Value.ToString().Replace("RetreiveEnterpriseActivationKey", "")) : (ActionResult) this.Content("Incorrect Key Activation Type");
    }
    catch (Exception ex)
    {
      ex.Log($"LookupController.ProductActivationKey[Key: {key}] ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-ProductActivationKey",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
      return (ActionResult) this.Content("Invalid Activation Key");
    }
  }
}
