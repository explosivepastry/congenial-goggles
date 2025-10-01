// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.ExportController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.Models;
using Microsoft.CSharp.RuntimeBinder;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

public class ExportController : ThemeController
{
  public ActionResult Index()
  {
    return (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = nameof (Index),
      controller = "API"
    });
  }

  public ActionResult APIDetail() => (ActionResult) this.View();

  public ActionResult DataPushInfo(long? id)
  {
    HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["portaltype"];
    bool flag = (cookie != null ? cookie.Value : "oneview") == "oneview";
    if (MonnitSession.CurrentTheme.Theme == "Default" && !flag)
      return (ActionResult) this.Redirect("/RestApi/Webhook");
    if (MonnitSession.CurrentCustomer == null)
      return (ActionResult) this.View((object) new ExternalDataSubscription());
    Account account = Account.Load(id ?? long.MinValue) ?? MonnitSession.CurrentCustomer.Account;
    ExternalDataSubscription model = ExternalDataSubscription.Load(account.ExternalDataSubscriptionID);
    if (model == null)
      model = new ExternalDataSubscription()
      {
        AccountID = account.AccountID
      };
    return (ActionResult) this.View((object) model);
  }

  [HttpGet]
  [AuthorizeDefault]
  public ActionResult WebhookHealth()
  {
    ExternalSubscriptionPreference model = ExternalSubscriptionPreference.LoadByAccountId(MonnitSession.CurrentCustomer.AccountID);
    if (model == null)
    {
      model = new ExternalSubscriptionPreference();
      model.AccountID = MonnitSession.CurrentCustomer.AccountID;
      model.UserId = MonnitSession.CurrentCustomer.Account.PrimaryContactID;
    }
    return !MonnitSession.CurrentCustomer.CanSeeAccount(model.AccountID) || !MonnitSession.CustomerCan("Navigation_View_API") || !MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult WebhookHealth(ExternalSubscriptionPreference model)
  {
    if (model.UsersBrokenCountLimit < 1)
      model.UsersBrokenCountLimit = ExternalDataSubscription.killRetryLimit;
    ExternalSubscriptionPreference subscriptionPreference = ExternalSubscriptionPreference.LoadByAccountId(MonnitSession.CurrentCustomer.AccountID);
    for (int index = 0; index < 100; ++index)
      Debug.WriteLine($"{index.ToString()}) typeof(string).Assembly.ImageRuntimeVersion = {typeof (string).Assembly.ImageRuntimeVersion}");
    $"hello {ExternalDataSubscription.killRetryLimit}";
    if (this.ModelState.IsValid)
    {
      if (subscriptionPreference != null)
      {
        subscriptionPreference.AccountID = model.AccountID;
        subscriptionPreference.UsersBrokenCountLimit = model.UsersBrokenCountLimit;
        subscriptionPreference.UserId = model.UserId;
        subscriptionPreference.Save();
        model = subscriptionPreference;
      }
      else
        model.Save();
    }
    return (ActionResult) this.View((object) model);
  }

  [HttpGet]
  [AuthorizeDefault]
  public ActionResult DataPushNotification(long? id)
  {
    HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["portaltype"];
    bool flag = (cookie != null ? cookie.Value : "oneview") == "oneview";
    if (MonnitSession.CurrentTheme.Theme == "Default" && !flag)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "NotificationSettings",
        controller = "API"
      });
    ExternalSubscriptionPreference model = ExternalSubscriptionPreference.Load(id ?? long.MinValue);
    if (model == null)
    {
      model = ExternalSubscriptionPreference.LoadByAccountId(MonnitSession.CurrentCustomer.AccountID);
      if (model == null)
      {
        model = new ExternalSubscriptionPreference();
        model.AccountID = MonnitSession.CurrentCustomer.AccountID;
        model.UserId = MonnitSession.CurrentCustomer.Account.PrimaryContactID;
        model.UsersBrokenCountLimit = ExternalDataSubscription.killRetryLimit;
      }
    }
    return !MonnitSession.CurrentCustomer.CanSeeAccount(model.AccountID) || !MonnitSession.CustomerCan("Navigation_View_API") && !MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult DataPushEdit(long id)
  {
    if (!MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.Load(id);
    if (dataSubscription == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "DataWebhook",
        controller = "Export"
      });
    if (!MonnitSession.CurrentCustomer.CanSeeAccount(dataSubscription.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    switch (dataSubscription.eExternalDataSubscriptionType)
    {
      case eExternalDataSubscriptionType.webhook:
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "ConfigureWebhook",
          controller = "Export",
          id = dataSubscription.ExternalDataSubscriptionID
        });
      case eExternalDataSubscriptionType.watson:
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "ConfigureWatson",
          controller = "Export",
          id = dataSubscription.ExternalDataSubscriptionID
        });
      case eExternalDataSubscriptionType.aws:
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "ConfigureAWS",
          controller = "Export",
          id = dataSubscription.ExternalDataSubscriptionID
        });
      case eExternalDataSubscriptionType.google:
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "ConfigureGoogle",
          controller = "Export",
          id = dataSubscription.ExternalDataSubscriptionID
        });
      case eExternalDataSubscriptionType.azure:
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "ConfigureAzure",
          controller = "Export",
          id = dataSubscription.ExternalDataSubscriptionID
        });
      case eExternalDataSubscriptionType.omf_pi:
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "ConfigurePI",
          controller = "Export",
          id = dataSubscription.ExternalDataSubscriptionID
        });
      case eExternalDataSubscriptionType.azuremqtt:
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "ConfigureAzureMqtt",
          controller = "Export",
          id = dataSubscription.ExternalDataSubscriptionID
        });
      case eExternalDataSubscriptionType.mqtt:
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "ConfigureMqtt",
          controller = "Export",
          id = dataSubscription.ExternalDataSubscriptionID
        });
      default:
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "DataWebhook",
          controller = "Export"
        });
    }
  }

  [AuthorizeDefault]
  public ActionResult DataPushHistory(string id)
  {
    Account account = Account.Load(MonnitSession.CurrentCustomer.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass.ToString() == id.ToLower())).FirstOrDefault<ExternalDataSubscription>();
    if (dataSubscription == null)
      dataSubscription = new ExternalDataSubscription()
      {
        AccountID = account.AccountID
      };
    return !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "WebhookHistory",
      controller = "Export",
      id = dataSubscription.ExternalDataSubscriptionID
    });
  }

  [AuthorizeDefault]
  public ActionResult RetryAttempt(string IDStringList)
  {
    string[] strArray = IDStringList.Split(new char[1]
    {
      '|'
    }, StringSplitOptions.RemoveEmptyEntries);
    List<long> longList = new List<long>();
    foreach (string o in strArray)
      longList.Add(o.ToLong());
    try
    {
      foreach (long id in longList)
      {
        ExternalDataSubscriptionAttempt subscriptionAttempt = ExternalDataSubscriptionAttempt.Load(id);
        ExternalDataSubscription dataSubscription = ExternalDataSubscription.Load(subscriptionAttempt.ExternalDataSubscriptionID);
        Account account = (Account) null;
        if (dataSubscription != null)
          account = Account.Load(dataSubscription.AccountID);
        if (!MonnitSession.CurrentCustomer.CanSeeAccount(dataSubscription.AccountID))
          return (ActionResult) this.RedirectToRoute("Default", (object) new
          {
            action = "Index",
            controller = "Overview"
          });
        if (account != null)
          AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, subscriptionAttempt.ExternalDataSubscriptionID, eAuditAction.Related_Modify, eAuditObject.Webhook, subscriptionAttempt.JsonStringify(), account.AccountID, "Queued webhook attempt to retry sending");
        if (subscriptionAttempt.AttemptCount >= 10)
          return (ActionResult) this.Content("Too many failed attmepts.");
        subscriptionAttempt.Status = eExternalDataSubscriptionStatus.Retry;
        subscriptionAttempt.DoRetry = true;
        subscriptionAttempt.Save();
      }
      return (ActionResult) this.Content("Success");
    }
    catch (Exception ex)
    {
      ex.Log("ExportController.RetryAttempt | IDStringList = " + IDStringList);
      return (ActionResult) this.Content("Failed");
    }
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult ResetBrokenExternalDataSubscription(long id)
  {
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.Load(id);
    if (dataSubscription == null || !MonnitSession.CurrentCustomer.CanSeeAccount(dataSubscription.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    dataSubscription.initialResetBroken();
    dataSubscription.Save();
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult DataPushWebhooks()
  {
    Account account = Account.Load(MonnitSession.CurrentCustomer.AccountID);
    return account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !account.CurrentSubscription.Can(Feature.Find("can_webhook_push")) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) ExternalDataSubscription.LoadAllByAccountID(account.AccountID));
  }

  public ActionResult DataWebhook()
  {
    if (MonnitSession.CurrentCustomer == null || MonnitSession.CurrentCustomer.AccountID < 0L)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "DataWebhookInformation",
        controller = "Export"
      });
    Account account = Account.Load(MonnitSession.CurrentCustomer.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !MonnitSession.CustomerCan("Navigation_View_API"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    DateTime dateTime = MonnitSession.MakeLocal(DateTime.UtcNow);
    MonnitSession.HistoryFromDate = dateTime.Date;
    dateTime = MonnitSession.MakeLocal(DateTime.UtcNow);
    dateTime = dateTime.Date;
    dateTime = dateTime.AddHours(23.0);
    dateTime = dateTime.AddMinutes(59.0);
    MonnitSession.HistoryToDate = dateTime.AddSeconds(59.0);
    ExternalDataSubscription model = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook)).FirstOrDefault<ExternalDataSubscription>();
    if (model == null)
      model = new ExternalDataSubscription()
      {
        AccountID = MonnitSession.CurrentCustomer.AccountID,
        eExternalDataSubscriptionClass = eExternalDataSubscriptionClass.webhook
      };
    return (ActionResult) this.View((object) model);
  }

  public ActionResult DataWebhookInformation()
  {
    return MonnitSession.CurrentCustomer != null ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "DataWebhook",
      controller = "Export"
    }) : (ActionResult) this.View((object) new ExternalDataSubscription());
  }

  public ActionResult NotificationWebhook()
  {
    if (MonnitSession.CurrentCustomer == null || MonnitSession.CurrentCustomer.AccountID < 0L)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    Account account = Account.Load(MonnitSession.CurrentCustomer.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !MonnitSession.CustomerCan("Navigation_View_API"))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    DateTime dateTime = MonnitSession.MakeLocal(DateTime.UtcNow);
    MonnitSession.HistoryFromDate = dateTime.Date;
    dateTime = MonnitSession.MakeLocal(DateTime.UtcNow);
    dateTime = dateTime.Date;
    dateTime = dateTime.AddHours(23.0);
    dateTime = dateTime.AddMinutes(59.0);
    MonnitSession.HistoryToDate = dateTime.AddSeconds(59.0);
    ExternalDataSubscription model = ExternalDataSubscription.LoadAllByAccountID(MonnitSession.CurrentCustomer.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.notification)).FirstOrDefault<ExternalDataSubscription>();
    if (model == null)
      model = new ExternalDataSubscription()
      {
        AccountID = MonnitSession.CurrentCustomer.AccountID,
        eExternalDataSubscriptionClass = eExternalDataSubscriptionClass.notification
      };
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult ConfigureWebhook(string id)
  {
    Account account = Account.Load(MonnitSession.CurrentCustomer.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    ExternalDataSubscription model = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass.ToString() == id.ToLower())).FirstOrDefault<ExternalDataSubscription>();
    if (model != null)
      return (ActionResult) this.View((object) model);
    eExternalDataSubscriptionClass subscriptionClass = (eExternalDataSubscriptionClass) Enum.Parse(typeof (eExternalDataSubscriptionClass), id.ToLower());
    return (ActionResult) this.View((object) new ExternalDataSubscription()
    {
      AccountID = account.AccountID,
      eExternalDataSubscriptionClass = subscriptionClass,
      eExternalDataSubscriptionType = eExternalDataSubscriptionType.webhook
    });
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ConfigureWebhook(
    string id,
    string connectionInfo1,
    bool sendWithoutDataMessage,
    string authenticationType,
    string username,
    string password,
    int eEDSClass,
    bool sendRawData)
  {
    Account account = Account.Load(MonnitSession.CurrentCustomer.Account.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (string.IsNullOrEmpty(connectionInfo1))
    {
      this.ModelState.AddModelError("main", "Base URL required");
      return this.ConfigureWebhook(id);
    }
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass.ToString() == id.ToLower())).FirstOrDefault<ExternalDataSubscription>();
    if (dataSubscription == null)
    {
      dataSubscription = new ExternalDataSubscription();
      dataSubscription.AccountID = account.AccountID;
      dataSubscription.ContentHeaderType = "application/json";
      dataSubscription.verb = "HttpPost";
      dataSubscription.ExternalID = "none";
      dataSubscription.eExternalDataSubscriptionType = eExternalDataSubscriptionType.webhook;
      dataSubscription.eExternalDataSubscriptionClass = (eExternalDataSubscriptionClass) eEDSClass;
    }
    if (authenticationType == "none")
    {
      username = "";
      password = "";
      dataSubscription.Password = "";
      dataSubscription.Username = "";
      ExternalDataSubscriptionProperty property1 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (username))).FirstOrDefault<ExternalDataSubscriptionProperty>();
      if (property1 != null)
        dataSubscription.RemoveProperty(property1);
      ExternalDataSubscriptionProperty property2 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (password))).FirstOrDefault<ExternalDataSubscriptionProperty>();
      if (property2 != null)
        dataSubscription.RemoveProperty(property2);
    }
    if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
    {
      dataSubscription.Username = username;
      dataSubscription.Password = MonnitSession.UseEncryption ? password.Encrypt() : password;
    }
    dataSubscription.ConnectionInfo1 = connectionInfo1;
    dataSubscription.SendWithoutDataMessage = dataSubscription.eExternalDataSubscriptionClass != eExternalDataSubscriptionClass.notification && sendWithoutDataMessage;
    dataSubscription.SendRawData = sendRawData;
    dataSubscription.Save();
    if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
    {
      ExternalDataSubscriptionProperty property3 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (username))).FirstOrDefault<ExternalDataSubscriptionProperty>();
      if (property3 == null)
      {
        property3 = new ExternalDataSubscriptionProperty();
        property3.DisplayName = "Webhook Basic Authentication Username";
        property3.Name = nameof (username);
        property3.eExternalDataSubscriptionType = eExternalDataSubscriptionType.webhook;
        property3.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
      }
      property3.StringValue = username;
      property3.Save();
      ExternalDataSubscriptionProperty property4 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (password))).FirstOrDefault<ExternalDataSubscriptionProperty>();
      if (property4 == null)
      {
        property4 = new ExternalDataSubscriptionProperty();
        property4.DisplayName = "Webhook Basic Authentication Password";
        property4.Name = nameof (password);
        property4.eExternalDataSubscriptionType = eExternalDataSubscriptionType.webhook;
        property4.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
      }
      property4.StringValue = MonnitSession.UseEncryption ? password.Encrypt() : password;
      property4.Save();
      dataSubscription.AssignProperty(property4);
      dataSubscription.AssignProperty(property3);
    }
    dataSubscription.LogAuditData(eAuditAction.Create, eAuditObject.Webhook, MonnitSession.CurrentCustomer.CustomerID, dataSubscription.AccountID, "Edited webhook settings");
    if (dataSubscription.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook && MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID != dataSubscription.ExternalDataSubscriptionID)
    {
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscription = dataSubscription;
      MonnitSession.CurrentCustomer.Account.Save();
    }
    return (ActionResult) this.View((object) dataSubscription);
  }

  [AuthorizeDefault]
  public ActionResult RemoveWebhookProperty(long id, long ExtnDtaSbscPropertyID, string type)
  {
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.Load(id);
    if (dataSubscription == null || !MonnitSession.CurrentCustomer.CanSeeAccount(dataSubscription.AccountID))
      return (ActionResult) this.Content("Failed");
    ExternalDataSubscriptionProperty property = ExternalDataSubscriptionProperty.Load(ExtnDtaSbscPropertyID);
    if (property != null)
      dataSubscription.RemoveProperty(property);
    List<ExternalDataSubscriptionProperty> source = ExternalDataSubscriptionProperty.LoadByExternalDataSubscriptionID(dataSubscription.ExternalDataSubscriptionID);
    switch (type)
    {
      case "cookie":
        return (ActionResult) this.PartialView("WebhookCookie", (object) source.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == type)).ToList<ExternalDataSubscriptionProperty>());
      case "header":
        return (ActionResult) this.PartialView("WebhookHeader", (object) source.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == type)).ToList<ExternalDataSubscriptionProperty>());
      default:
        return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult AssignWebhookProperty(long id, string key, string value, string type)
  {
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.Load(id);
    if (dataSubscription == null || !MonnitSession.CurrentCustomer.CanSeeAccount(dataSubscription.AccountID))
      return (ActionResult) this.Content("Failed");
    List<ExternalDataSubscriptionProperty> source = ExternalDataSubscriptionProperty.LoadByExternalDataSubscriptionID(dataSubscription.ExternalDataSubscriptionID);
    if (source.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == type && m.StringValue == key)).Count<ExternalDataSubscriptionProperty>() < 1)
    {
      ExternalDataSubscriptionProperty property = new ExternalDataSubscriptionProperty();
      property.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
      property.eExternalDataSubscriptionType = eExternalDataSubscriptionType.webhook;
      property.DisplayName = type + " Name=Value";
      property.Name = type;
      property.StringValue = key;
      property.StringValue2 = value;
      property.Save();
      dataSubscription.AssignProperty(property);
      source.Add(property);
    }
    switch (type)
    {
      case "cookie":
        return (ActionResult) this.PartialView("WebhookCookie", (object) source.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == type)).ToList<ExternalDataSubscriptionProperty>());
      case "header":
        return (ActionResult) this.PartialView("WebhookHeader", (object) source.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == type)).ToList<ExternalDataSubscriptionProperty>());
      default:
        return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult WebHookDelete(long id)
  {
    ExternalDataSubscription DBObject = ExternalDataSubscription.Load(id);
    if (DBObject == null)
      return (ActionResult) this.Content("Failed");
    Account acct = Account.Load(DBObject.AccountID);
    if (acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct) || !acct.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.Content("Failed");
    if (DBObject.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.notification)
    {
      foreach (NotificationRecipient recipient in NotificationRecipient.NotificationWebHookRecipient_LoadByAccountID(DBObject.AccountID))
        Notification.Load(recipient.NotificationID).RemoveRecipient(recipient);
    }
    else
    {
      if (DBObject.eExternalDataSubscriptionClass != eExternalDataSubscriptionClass.webhook)
        return (ActionResult) this.Content("Failed");
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscription = (ExternalDataSubscription) null;
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID = long.MinValue;
      acct.ClearDataPushes();
      acct.ExternalDataSubscription = (ExternalDataSubscription) null;
      acct.Save();
    }
    DBObject.LogAuditData(eAuditAction.Delete, eAuditObject.Webhook, MonnitSession.CurrentCustomer.CustomerID, acct.AccountID, "Deleted webhook");
    DBObject.IsDeleted = true;
    DBObject.Save();
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult WebhookHistory(long id)
  {
    ExternalDataSubscription model = ExternalDataSubscription.Load(id);
    if (model == null)
      model = new ExternalDataSubscription()
      {
        AccountID = MonnitSession.CurrentCustomer.AccountID
      };
    Account account = Account.Load(model.AccountID);
    return account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(model.AccountID) || !account.CurrentSubscription.Can(Feature.Find("can_webhook_push")) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult WebhookHistoryRefresh(long id, string sort)
  {
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.Load(id);
    if (dataSubscription == null || !MonnitSession.CurrentCustomer.CanSeeAccount(dataSubscription.AccountID))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    DateTime utcFromLocalById1 = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime utcFromLocalById2 = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    return !string.IsNullOrWhiteSpace(sort) && sort == "Webhook Fails" ? (ActionResult) this.PartialView("_WebhookFailuresList", (object) WebHookFailures.WebhookFilter(MonnitSession.CurrentCustomer.AccountID, utcFromLocalById1, utcFromLocalById2)) : (ActionResult) this.PartialView("_WebhookHistoryList", (object) ExternalDataSubscriptionAttempt.LoadBySubscriptionAndDate(id, utcFromLocalById1, utcFromLocalById2, 0L, 100));
  }

  [AuthorizeDefault]
  public ActionResult WebhookAttempt(long id)
  {
    ExternalDataSubscriptionAttempt model = ExternalDataSubscriptionAttempt.Load(id);
    if (model == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.Load(model.ExternalDataSubscriptionID);
    Account account = Account.Load(dataSubscription.AccountID);
    return account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(dataSubscription.AccountID) || !account.CurrentSubscription.Can(Feature.Find("can_webhook_push")) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult ConfigureMQTT(string id)
  {
    Account account = Account.Load(MonnitSession.CurrentCustomer.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    ExternalDataSubscription model = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass.ToString() == id.ToLower())).FirstOrDefault<ExternalDataSubscription>();
    if (model != null)
      return (ActionResult) this.View((object) model);
    eExternalDataSubscriptionClass subscriptionClass = (eExternalDataSubscriptionClass) Enum.Parse(typeof (eExternalDataSubscriptionClass), id.ToLower());
    return (ActionResult) this.View((object) new ExternalDataSubscription()
    {
      AccountID = account.AccountID,
      eExternalDataSubscriptionClass = subscriptionClass,
      eExternalDataSubscriptionType = eExternalDataSubscriptionType.mqtt
    });
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ConfigureMQTT(
    string id,
    string host,
    string port,
    string clientID,
    string username,
    string password,
    string topic,
    string useSSL,
    int eEDSClass,
    bool sendRawData)
  {
    Account account = Account.Load(MonnitSession.CurrentCustomer.Account.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(port) || string.IsNullOrEmpty(clientID) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(topic))
    {
      this.ModelState.AddModelError("main", "All fields required");
      return this.ConfigureAzure(id);
    }
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass.ToString() == id.ToLower())).FirstOrDefault<ExternalDataSubscription>();
    if (dataSubscription == null)
    {
      dataSubscription = new ExternalDataSubscription();
      dataSubscription.AccountID = MonnitSession.CurrentCustomer.AccountID;
      dataSubscription.ContentHeaderType = "application/json";
      dataSubscription.verb = "Mqtt_Password";
      dataSubscription.ExternalID = "none";
      dataSubscription.eExternalDataSubscriptionType = eExternalDataSubscriptionType.mqtt;
      dataSubscription.eExternalDataSubscriptionClass = (eExternalDataSubscriptionClass) eEDSClass;
    }
    dataSubscription.ConnectionInfo1 = host;
    dataSubscription.ConnectionInfo2 = port;
    dataSubscription.Username = username;
    dataSubscription.Password = password.Encrypt();
    dataSubscription.SendWithoutDataMessage = true;
    dataSubscription.SendRawData = sendRawData;
    dataSubscription.Save();
    ExternalDataSubscriptionProperty property1 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (clientID))).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property2 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (topic))).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property3 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (useSSL))).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (property1 == null)
    {
      property1 = new ExternalDataSubscriptionProperty();
      property1.DisplayName = "Client ID";
      property1.Name = nameof (clientID);
      property1.eExternalDataSubscriptionType = eExternalDataSubscriptionType.mqtt;
      property1.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property1.StringValue = clientID;
    property1.Save();
    if (property2 == null)
    {
      property2 = new ExternalDataSubscriptionProperty();
      property2.DisplayName = "Topic Name";
      property2.Name = nameof (topic);
      property2.eExternalDataSubscriptionType = eExternalDataSubscriptionType.mqtt;
      property2.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property2.StringValue = topic;
    property2.Save();
    if (property3 == null)
    {
      property3 = new ExternalDataSubscriptionProperty();
      property3.DisplayName = "Use SSL";
      property3.Name = nameof (useSSL);
      property3.eExternalDataSubscriptionType = eExternalDataSubscriptionType.mqtt;
      property3.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property3.StringValue = (!string.IsNullOrEmpty(useSSL)).ToString();
    property3.Save();
    dataSubscription.AssignProperty(property1);
    dataSubscription.AssignProperty(property2);
    dataSubscription.AssignProperty(property3);
    dataSubscription.LogAuditData(eAuditAction.Create, eAuditObject.Webhook, MonnitSession.CurrentCustomer.CustomerID, dataSubscription.AccountID, "Edited webhook settings");
    if (dataSubscription.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook && MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID != dataSubscription.ExternalDataSubscriptionID)
    {
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscription = dataSubscription;
      MonnitSession.CurrentCustomer.Account.Save();
    }
    return (ActionResult) this.View((object) dataSubscription);
  }

  [AuthorizeDefault]
  public ActionResult ConfigureMQTTwithCertificate(string id)
  {
    Account account = Account.Load(MonnitSession.CurrentCustomer.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    ExternalDataSubscription model = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass.ToString() == id.ToLower())).FirstOrDefault<ExternalDataSubscription>();
    if (model != null)
      return (ActionResult) this.View((object) model);
    eExternalDataSubscriptionClass subscriptionClass = (eExternalDataSubscriptionClass) Enum.Parse(typeof (eExternalDataSubscriptionClass), id.ToLower());
    return (ActionResult) this.View((object) new ExternalDataSubscription()
    {
      AccountID = account.AccountID,
      eExternalDataSubscriptionClass = subscriptionClass,
      eExternalDataSubscriptionType = eExternalDataSubscriptionType.mqtt
    });
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ConfigureMQTTwithCertificate(string id, FormCollection collection)
  {
    Account account = Account.Load(MonnitSession.CurrentCustomer.Account.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    HttpPostedFileBase file = this.Request.Files["certificate"];
    if (string.IsNullOrEmpty(collection["host"]) || string.IsNullOrEmpty(collection["port"]) || string.IsNullOrEmpty(collection["clientID"]) || file == null || file.FileName.Length == 0 || file.ContentLength == 0 || string.IsNullOrEmpty(collection["topic"]))
    {
      this.ModelState.AddModelError("main", "All fields required");
      return this.ConfigureAzure(id);
    }
    string str1 = collection["host"];
    string str2 = collection["port"];
    string str3 = collection["clientID"];
    string str4 = collection["topic"];
    int num = collection["eEDSClass"].ToInt();
    MemoryStream destination = new MemoryStream();
    file.InputStream.CopyTo((Stream) destination);
    byte[] array = destination.ToArray();
    string o = collection["sendRawData"];
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass.ToString() == id.ToLower())).FirstOrDefault<ExternalDataSubscription>();
    if (dataSubscription == null)
    {
      dataSubscription = new ExternalDataSubscription();
      dataSubscription.AccountID = MonnitSession.CurrentCustomer.AccountID;
      dataSubscription.ContentHeaderType = "application/json";
      dataSubscription.verb = "Mqtt_Certificate";
      dataSubscription.ExternalID = "none";
      dataSubscription.eExternalDataSubscriptionType = eExternalDataSubscriptionType.mqttcert;
      dataSubscription.eExternalDataSubscriptionClass = (eExternalDataSubscriptionClass) num;
    }
    dataSubscription.ConnectionInfo1 = str1;
    dataSubscription.ConnectionInfo2 = str2;
    dataSubscription.SendWithoutDataMessage = true;
    dataSubscription.SendRawData = o.ToBool();
    dataSubscription.Save();
    ExternalDataSubscriptionProperty property1 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "clientID")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property2 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "topic")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property3 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "certificate")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (property1 == null)
    {
      property1 = new ExternalDataSubscriptionProperty();
      property1.DisplayName = "Client ID";
      property1.Name = "clientID";
      property1.eExternalDataSubscriptionType = eExternalDataSubscriptionType.mqtt;
      property1.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property1.StringValue = str3;
    property1.Save();
    if (property2 == null)
    {
      property2 = new ExternalDataSubscriptionProperty();
      property2.DisplayName = "Topic Name";
      property2.Name = "topic";
      property2.eExternalDataSubscriptionType = eExternalDataSubscriptionType.mqtt;
      property2.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property2.StringValue = str4;
    property2.Save();
    if (property3 == null)
    {
      property3 = new ExternalDataSubscriptionProperty();
      property3.DisplayName = "CA Certificate";
      property3.Name = "certificate";
      property3.eExternalDataSubscriptionType = eExternalDataSubscriptionType.mqtt;
      property3.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property3.BinaryValue = array;
    ExternalDataSubscriptionProperty subscriptionProperty = property3;
    DateTime dateTime = DateTime.UtcNow;
    dateTime = dateTime.AddDays(365.0);
    string str5 = dateTime.Date.ToString();
    subscriptionProperty.StringValue2 = str5;
    property3.Save();
    dataSubscription.AssignProperty(property1);
    dataSubscription.AssignProperty(property2);
    dataSubscription.AssignProperty(property3);
    dataSubscription.LogAuditData(eAuditAction.Create, eAuditObject.Webhook, MonnitSession.CurrentCustomer.CustomerID, dataSubscription.AccountID, "Edited webhook settings");
    if (dataSubscription.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook && MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID != dataSubscription.ExternalDataSubscriptionID)
    {
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscription = dataSubscription;
      MonnitSession.CurrentCustomer.Account.Save();
    }
    return (ActionResult) this.View((object) dataSubscription);
  }

  [AuthorizeDefault]
  public ActionResult ConfigureWatson(string id)
  {
    Account account = Account.Load(MonnitSession.CurrentCustomer.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    ExternalDataSubscription model = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass.ToString() == id.ToLower())).FirstOrDefault<ExternalDataSubscription>();
    if (model != null)
      return (ActionResult) this.View((object) model);
    eExternalDataSubscriptionClass subscriptionClass = (eExternalDataSubscriptionClass) Enum.Parse(typeof (eExternalDataSubscriptionClass), id.ToLower());
    return (ActionResult) this.View((object) new ExternalDataSubscription()
    {
      AccountID = account.AccountID,
      eExternalDataSubscriptionClass = subscriptionClass,
      eExternalDataSubscriptionType = eExternalDataSubscriptionType.webhook
    });
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ConfigureWatson(
    string id,
    string OrgID,
    string TypeID,
    string DeviceID,
    string EventName,
    string APIKey,
    string AuthToken,
    int eEDSClass,
    bool sendRawData)
  {
    Account account = Account.Load(MonnitSession.CurrentCustomer.Account.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (string.IsNullOrEmpty(OrgID) || string.IsNullOrEmpty(TypeID) || string.IsNullOrEmpty(DeviceID) || string.IsNullOrEmpty(EventName) || string.IsNullOrEmpty(APIKey) || string.IsNullOrEmpty(AuthToken))
    {
      this.ModelState.AddModelError("main", "All field required");
      return this.ConfigureWatson(id);
    }
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass.ToString() == id.ToLower())).FirstOrDefault<ExternalDataSubscription>();
    if (dataSubscription == null)
    {
      dataSubscription = new ExternalDataSubscription();
      dataSubscription.AccountID = account.AccountID;
      dataSubscription.ContentHeaderType = "application/json";
      dataSubscription.verb = "HttpPost";
      dataSubscription.ExternalID = "none";
      dataSubscription.eExternalDataSubscriptionType = eExternalDataSubscriptionType.watson;
      dataSubscription.eExternalDataSubscriptionClass = (eExternalDataSubscriptionClass) eEDSClass;
    }
    dataSubscription.ConnectionInfo1 = $"https://{OrgID}.messaging.internetofthings.ibmcloud.com:8883/api/v0002/application/types/{TypeID}/devices/{DeviceID}/events/{EventName}";
    dataSubscription.SendWithoutDataMessage = true;
    dataSubscription.SendRawData = sendRawData;
    dataSubscription.Save();
    ExternalDataSubscriptionProperty property1 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "username")).FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (property1 == null)
    {
      property1 = new ExternalDataSubscriptionProperty();
      property1.DisplayName = "Watson Authentication Username";
      property1.Name = "username";
      property1.eExternalDataSubscriptionType = eExternalDataSubscriptionType.watson;
      property1.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property1.StringValue = APIKey;
    property1.Save();
    ExternalDataSubscriptionProperty property2 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "password")).FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (property2 == null)
    {
      property2 = new ExternalDataSubscriptionProperty();
      property2.DisplayName = "Watson Authentication Password";
      property2.Name = "password";
      property2.eExternalDataSubscriptionType = eExternalDataSubscriptionType.watson;
      property2.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property2.StringValue = MonnitSession.UseEncryption ? AuthToken.Encrypt() : AuthToken;
    property2.Save();
    ExternalDataSubscriptionProperty property3 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (OrgID))).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (property3 == null)
    {
      property3 = new ExternalDataSubscriptionProperty();
      property3.DisplayName = "Watson Organization ID";
      property3.Name = nameof (OrgID);
      property3.eExternalDataSubscriptionType = eExternalDataSubscriptionType.watson;
      property3.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property3.StringValue = OrgID;
    property3.Save();
    ExternalDataSubscriptionProperty property4 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (TypeID))).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (property4 == null)
    {
      property4 = new ExternalDataSubscriptionProperty();
      property4.DisplayName = "Watson Type ID";
      property4.Name = nameof (TypeID);
      property4.eExternalDataSubscriptionType = eExternalDataSubscriptionType.watson;
      property4.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property4.StringValue = TypeID;
    property4.Save();
    ExternalDataSubscriptionProperty property5 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (DeviceID))).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (property5 == null)
    {
      property5 = new ExternalDataSubscriptionProperty();
      property5.DisplayName = "Watson Device ID";
      property5.Name = nameof (DeviceID);
      property5.eExternalDataSubscriptionType = eExternalDataSubscriptionType.watson;
      property5.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property5.StringValue = DeviceID;
    property5.Save();
    ExternalDataSubscriptionProperty property6 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (EventName))).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (property6 == null)
    {
      property6 = new ExternalDataSubscriptionProperty();
      property6.DisplayName = "Watson Event Name";
      property6.Name = nameof (EventName);
      property6.eExternalDataSubscriptionType = eExternalDataSubscriptionType.watson;
      property6.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property6.StringValue = EventName;
    property6.Save();
    dataSubscription.AssignProperty(property2);
    dataSubscription.AssignProperty(property1);
    dataSubscription.AssignProperty(property3);
    dataSubscription.AssignProperty(property4);
    dataSubscription.AssignProperty(property5);
    dataSubscription.AssignProperty(property6);
    dataSubscription.LogAuditData(eAuditAction.Create, eAuditObject.Webhook, MonnitSession.CurrentCustomer.CustomerID, dataSubscription.AccountID, "Edited webhook settings");
    if (dataSubscription.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook && MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID != dataSubscription.ExternalDataSubscriptionID)
    {
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscription = dataSubscription;
      MonnitSession.CurrentCustomer.Account.Save();
    }
    return (ActionResult) this.View((object) dataSubscription);
  }

  [AuthorizeDefault]
  public ActionResult ConfigureAWS(string id)
  {
    Account account = Account.Load(MonnitSession.CurrentCustomer.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    ExternalDataSubscription model = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass.ToString() == id.ToLower())).FirstOrDefault<ExternalDataSubscription>();
    if (model != null)
      return (ActionResult) this.View((object) model);
    eExternalDataSubscriptionClass subscriptionClass = (eExternalDataSubscriptionClass) Enum.Parse(typeof (eExternalDataSubscriptionClass), id.ToLower());
    return (ActionResult) this.View((object) new ExternalDataSubscription()
    {
      AccountID = account.AccountID,
      eExternalDataSubscriptionClass = subscriptionClass,
      eExternalDataSubscriptionType = eExternalDataSubscriptionType.aws
    });
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ConfigureAWS(
    string id,
    string ConnectionInfo1,
    bool sendWithoutDataMessage,
    string accessKey,
    string secretKey,
    string apiKey,
    string callMethod,
    int eEDSClass,
    bool sendRawData)
  {
    Account account = Account.Load(MonnitSession.CurrentCustomer.Account.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (string.IsNullOrEmpty(ConnectionInfo1) || string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(apiKey))
    {
      this.ModelState.AddModelError("", "All fields required");
      return (ActionResult) this.View((object) new ExternalDataSubscription());
    }
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass.ToString() == id.ToLower())).FirstOrDefault<ExternalDataSubscription>();
    if (dataSubscription == null)
    {
      dataSubscription = new ExternalDataSubscription();
      dataSubscription.AccountID = account.AccountID;
      dataSubscription.ContentHeaderType = "application/json";
      dataSubscription.verb = "AWS_Version1";
      dataSubscription.ExternalID = "none";
      dataSubscription.eExternalDataSubscriptionType = eExternalDataSubscriptionType.aws;
      dataSubscription.eExternalDataSubscriptionClass = (eExternalDataSubscriptionClass) eEDSClass;
    }
    dataSubscription.SendWithoutDataMessage = sendWithoutDataMessage;
    dataSubscription.SendRawData = sendRawData;
    dataSubscription.ConnectionInfo1 = ConnectionInfo1;
    string[] strArray1 = ConnectionInfo1.Replace("https://", "").Split(new char[1]
    {
      '?'
    }, StringSplitOptions.RemoveEmptyEntries);
    string[] strArray2 = strArray1[0].Split(new char[1]
    {
      '/'
    }, StringSplitOptions.RemoveEmptyEntries);
    string str1 = "";
    string str2;
    string str3;
    string str4;
    string str5;
    string str6;
    try
    {
      str2 = strArray2[0];
      str3 = strArray2[1];
      str4 = strArray1[0].Replace($"{str2}/{str3}", "").TrimStart('/');
      if (strArray1.Length > 1)
        str1 = strArray1[1];
      string[] strArray3 = str2.Split(new char[1]{ '.' }, StringSplitOptions.RemoveEmptyEntries);
      str5 = strArray3[2];
      str6 = strArray3[1];
    }
    catch
    {
      this.ModelState.AddModelError(nameof (ConnectionInfo1), "Invalid Invoke URL");
      return (ActionResult) this.View((object) dataSubscription);
    }
    dataSubscription.Save();
    ExternalDataSubscriptionProperty property1 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "awsHost")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property2 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (accessKey))).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property3 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (secretKey))).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property4 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (apiKey))).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property5 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "region")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property6 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "service")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property7 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "stageName")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property8 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "resourceName")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property9 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "parameterList")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property10 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (callMethod))).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (property1 == null)
    {
      property1 = new ExternalDataSubscriptionProperty();
      property1.DisplayName = "AWS API Gateway Host";
      property1.Name = "awsHost";
      property1.eExternalDataSubscriptionType = eExternalDataSubscriptionType.aws;
      property1.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property1.StringValue = str2;
    property1.Save();
    if (property2 == null)
    {
      property2 = new ExternalDataSubscriptionProperty();
      property2.DisplayName = "Amazon IAM Access Key";
      property2.Name = nameof (accessKey);
      property2.eExternalDataSubscriptionType = eExternalDataSubscriptionType.aws;
      property2.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property2.StringValue = MonnitSession.UseEncryption ? accessKey.Encrypt() : accessKey;
    property2.Save();
    if (property3 == null)
    {
      property3 = new ExternalDataSubscriptionProperty();
      property3.DisplayName = "Amazon IAM Secret Key";
      property3.Name = nameof (secretKey);
      property3.eExternalDataSubscriptionType = eExternalDataSubscriptionType.aws;
      property3.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property3.StringValue = MonnitSession.UseEncryption ? secretKey.Encrypt() : secretKey;
    property3.Save();
    if (property4 == null)
    {
      property4 = new ExternalDataSubscriptionProperty();
      property4.DisplayName = "Amazon API Key";
      property4.Name = nameof (apiKey);
      property4.eExternalDataSubscriptionType = eExternalDataSubscriptionType.aws;
      property4.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property4.StringValue = apiKey;
    property4.Save();
    if (property5 == null)
    {
      property5 = new ExternalDataSubscriptionProperty();
      property5.DisplayName = "Amazon AWS Region";
      property5.Name = "region";
      property5.eExternalDataSubscriptionType = eExternalDataSubscriptionType.aws;
      property5.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property5.StringValue = str5;
    property5.Save();
    if (property6 == null)
    {
      property6 = new ExternalDataSubscriptionProperty();
      property6.DisplayName = "AWS Service Name";
      property6.Name = "service";
      property6.eExternalDataSubscriptionType = eExternalDataSubscriptionType.aws;
      property6.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property6.StringValue = str6;
    property6.Save();
    if (property7 == null)
    {
      property7 = new ExternalDataSubscriptionProperty();
      property7.DisplayName = "AWS API Deployment Stage Name";
      property7.Name = "stageName";
      property7.eExternalDataSubscriptionType = eExternalDataSubscriptionType.aws;
      property7.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property7.StringValue = str3;
    property7.Save();
    if (property10 == null)
    {
      property10 = new ExternalDataSubscriptionProperty();
      property10.DisplayName = "AWS API Http Call Method";
      property10.Name = nameof (callMethod);
      property10.eExternalDataSubscriptionType = eExternalDataSubscriptionType.aws;
      property10.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property10.StringValue = callMethod;
    property10.Save();
    if (property8 == null)
    {
      property8 = new ExternalDataSubscriptionProperty();
      property8.DisplayName = "AWS API Deployment Resource Name";
      property8.Name = "resourceName";
      property8.eExternalDataSubscriptionType = eExternalDataSubscriptionType.aws;
      property8.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property8.StringValue = str4;
    property8.Save();
    if (property9 == null)
    {
      property9 = new ExternalDataSubscriptionProperty();
      property9.DisplayName = "AWS API Call Parameter List";
      property9.Name = "parameterList";
      property9.eExternalDataSubscriptionType = eExternalDataSubscriptionType.aws;
      property9.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property9.StringValue = str1;
    property9.Save();
    dataSubscription.AssignProperty(property1);
    dataSubscription.AssignProperty(property2);
    dataSubscription.AssignProperty(property3);
    dataSubscription.AssignProperty(property4);
    dataSubscription.AssignProperty(property5);
    dataSubscription.AssignProperty(property6);
    dataSubscription.AssignProperty(property7);
    dataSubscription.AssignProperty(property10);
    dataSubscription.AssignProperty(property8);
    dataSubscription.AssignProperty(property9);
    dataSubscription.LogAuditData(eAuditAction.Create, eAuditObject.Webhook, MonnitSession.CurrentCustomer.CustomerID, dataSubscription.AccountID, "Edited webhook settings");
    if (dataSubscription.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook && MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID != dataSubscription.ExternalDataSubscriptionID)
    {
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscription = dataSubscription;
      MonnitSession.CurrentCustomer.Account.Save();
    }
    return (ActionResult) this.View((object) dataSubscription);
  }

  [AuthorizeDefault]
  public ActionResult ConfigureAzure(string id)
  {
    Account account = Account.Load(MonnitSession.CurrentCustomer.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    ExternalDataSubscription model = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass.ToString() == id.ToLower())).FirstOrDefault<ExternalDataSubscription>();
    if (model != null)
      return (ActionResult) this.View((object) model);
    eExternalDataSubscriptionClass subscriptionClass = (eExternalDataSubscriptionClass) Enum.Parse(typeof (eExternalDataSubscriptionClass), id.ToLower());
    return (ActionResult) this.View((object) new ExternalDataSubscription()
    {
      AccountID = account.AccountID,
      eExternalDataSubscriptionClass = subscriptionClass,
      eExternalDataSubscriptionType = eExternalDataSubscriptionType.azure
    });
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ConfigureAzure(
    string id,
    string IOTHub,
    string deviceID,
    string sasToken,
    int eEDSClass,
    bool sendRawData)
  {
    Account account = Account.Load(MonnitSession.CurrentCustomer.Account.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (string.IsNullOrEmpty(IOTHub) || string.IsNullOrEmpty(deviceID) || string.IsNullOrEmpty(sasToken))
    {
      this.ModelState.AddModelError("main", "All fields required");
      return this.ConfigureAzure(id);
    }
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass.ToString() == id.ToLower())).FirstOrDefault<ExternalDataSubscription>();
    if (dataSubscription == null)
    {
      dataSubscription = new ExternalDataSubscription();
      dataSubscription.AccountID = MonnitSession.CurrentCustomer.AccountID;
      dataSubscription.ContentHeaderType = "application/json";
      dataSubscription.verb = "HttpPost";
      dataSubscription.ExternalID = "none";
      dataSubscription.eExternalDataSubscriptionType = eExternalDataSubscriptionType.azure;
      dataSubscription.eExternalDataSubscriptionClass = (eExternalDataSubscriptionClass) eEDSClass;
    }
    dataSubscription.ConnectionInfo1 = $"https://{IOTHub}.azure-devices.net/devices/{deviceID}/messages/events?api-version=2018-04-01";
    dataSubscription.SendWithoutDataMessage = true;
    dataSubscription.SendRawData = sendRawData;
    dataSubscription.Save();
    ExternalDataSubscriptionProperty property1 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "iotHub")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property2 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (deviceID))).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property3 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (sasToken))).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (property1 == null)
    {
      property1 = new ExternalDataSubscriptionProperty();
      property1.DisplayName = "Azure IoT Hub Host Name";
      property1.Name = "iotHub";
      property1.eExternalDataSubscriptionType = eExternalDataSubscriptionType.azure;
      property1.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property1.StringValue = IOTHub;
    property1.Save();
    if (property2 == null)
    {
      property2 = new ExternalDataSubscriptionProperty();
      property2.DisplayName = "Azure Device ID";
      property2.Name = nameof (deviceID);
      property2.eExternalDataSubscriptionType = eExternalDataSubscriptionType.azure;
      property2.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property2.StringValue = deviceID;
    property2.Save();
    if (property3 == null)
    {
      property3 = new ExternalDataSubscriptionProperty();
      property3.DisplayName = "Azure sas Token";
      property3.Name = nameof (sasToken);
      property3.eExternalDataSubscriptionType = eExternalDataSubscriptionType.azure;
      property3.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property3.StringValue = MonnitSession.UseEncryption ? sasToken.Encrypt() : sasToken;
    property3.Save();
    dataSubscription.AssignProperty(property1);
    dataSubscription.AssignProperty(property2);
    dataSubscription.AssignProperty(property3);
    dataSubscription.LogAuditData(eAuditAction.Create, eAuditObject.Webhook, MonnitSession.CurrentCustomer.CustomerID, dataSubscription.AccountID, "Edited webhook settings");
    if (dataSubscription.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook && MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID != dataSubscription.ExternalDataSubscriptionID)
    {
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscription = dataSubscription;
      MonnitSession.CurrentCustomer.Account.Save();
    }
    return (ActionResult) this.View((object) dataSubscription);
  }

  [AuthorizeDefault]
  public ActionResult ConfigureAzureMQTT(string id)
  {
    Account account = Account.Load(MonnitSession.CurrentCustomer.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    ExternalDataSubscription model = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass.ToString() == id.ToLower())).FirstOrDefault<ExternalDataSubscription>();
    if (model != null)
      return (ActionResult) this.View((object) model);
    eExternalDataSubscriptionClass subscriptionClass = (eExternalDataSubscriptionClass) Enum.Parse(typeof (eExternalDataSubscriptionClass), id.ToLower());
    return (ActionResult) this.View((object) new ExternalDataSubscription()
    {
      AccountID = account.AccountID,
      eExternalDataSubscriptionClass = subscriptionClass,
      eExternalDataSubscriptionType = eExternalDataSubscriptionType.azuremqtt
    });
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ConfigureAzureMQTT(
    string id,
    string IOTHub,
    string deviceID,
    string primaryKey,
    int eEDSClass,
    bool sendRawData)
  {
    Account account = Account.Load(MonnitSession.CurrentCustomer.Account.AccountID);
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (string.IsNullOrEmpty(IOTHub) || string.IsNullOrEmpty(deviceID) || string.IsNullOrEmpty(primaryKey))
    {
      this.ModelState.AddModelError("main", "All fields required");
      return this.ConfigureAzure(id);
    }
    if (!IOTHub.Contains(".azure-devices.net"))
    {
      this.ModelState.AddModelError("main", "Host Name in incorrect format: try adding .azure-devices.net to the end");
      return this.ConfigureAzure(id);
    }
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass.ToString() == id.ToLower())).FirstOrDefault<ExternalDataSubscription>();
    if (dataSubscription == null)
    {
      dataSubscription = new ExternalDataSubscription();
      dataSubscription.AccountID = MonnitSession.CurrentCustomer.AccountID;
      dataSubscription.ContentHeaderType = "application/json";
      dataSubscription.verb = "Mqtt";
      dataSubscription.ExternalID = "none";
      dataSubscription.eExternalDataSubscriptionType = eExternalDataSubscriptionType.azuremqtt;
      dataSubscription.eExternalDataSubscriptionClass = (eExternalDataSubscriptionClass) eEDSClass;
    }
    dataSubscription.ConnectionInfo1 = $"{IOTHub}/{deviceID}/api-version=2016-11-14";
    dataSubscription.SendWithoutDataMessage = true;
    dataSubscription.SendRawData = sendRawData;
    dataSubscription.Save();
    ExternalDataSubscriptionProperty property1 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "iotHub")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property2 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (deviceID))).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property3 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == nameof (primaryKey))).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty property4 = dataSubscription.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "sasToken")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (property1 == null)
    {
      property1 = new ExternalDataSubscriptionProperty();
      property1.DisplayName = "Azure IoT Hub Host Name";
      property1.Name = "iotHub";
      property1.eExternalDataSubscriptionType = eExternalDataSubscriptionType.azuremqtt;
      property1.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property1.StringValue = IOTHub;
    property1.Save();
    if (property2 == null)
    {
      property2 = new ExternalDataSubscriptionProperty();
      property2.DisplayName = "Azure Device ID";
      property2.Name = nameof (deviceID);
      property2.eExternalDataSubscriptionType = eExternalDataSubscriptionType.azuremqtt;
      property2.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property2.StringValue = deviceID;
    property2.Save();
    if (property4 == null)
    {
      property4 = new ExternalDataSubscriptionProperty();
      property4.DisplayName = "Azure sas Token";
      property4.Name = "sasToken";
      property4.eExternalDataSubscriptionType = eExternalDataSubscriptionType.azuremqtt;
      property4.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property4.StringValue2 = DateTime.Now.AddDays(-1.0).ToString();
    property4.Save();
    if (property3 == null)
    {
      property3 = new ExternalDataSubscriptionProperty();
      property3.DisplayName = "Azure Primary Key";
      property3.Name = nameof (primaryKey);
      property3.eExternalDataSubscriptionType = eExternalDataSubscriptionType.azuremqtt;
      property3.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
    }
    property3.StringValue = primaryKey;
    property3.Save();
    dataSubscription.AssignProperty(property1);
    dataSubscription.AssignProperty(property2);
    dataSubscription.AssignProperty(property4);
    dataSubscription.AssignProperty(property3);
    dataSubscription.LogAuditData(eAuditAction.Create, eAuditObject.Webhook, MonnitSession.CurrentCustomer.CustomerID, dataSubscription.AccountID, "Edited webhook settings");
    if (dataSubscription.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook && MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID != dataSubscription.ExternalDataSubscriptionID)
    {
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscription = dataSubscription;
      MonnitSession.CurrentCustomer.Account.Save();
    }
    return (ActionResult) this.View((object) dataSubscription);
  }

  [AuthorizeDefault]
  public ActionResult ConfigurePI(string id)
  {
    Account account = MonnitSession.CurrentCustomer.Account;
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    ExternalDataSubscription model = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass.ToString() == id.ToLower())).FirstOrDefault<ExternalDataSubscription>();
    if (model == null)
    {
      eExternalDataSubscriptionClass subscriptionClass = (eExternalDataSubscriptionClass) Enum.Parse(typeof (eExternalDataSubscriptionClass), id.ToLower());
      model = new ExternalDataSubscription()
      {
        AccountID = MonnitSession.CurrentCustomer.AccountID,
        eExternalDataSubscriptionClass = subscriptionClass
      };
    }
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ConfigurePI(
    string id,
    string connectionInfo1,
    string username,
    string password,
    string validateSSL,
    int eEDSClass)
  {
    Account account = MonnitSession.CurrentCustomer.Account;
    if (account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (string.IsNullOrEmpty(connectionInfo1))
    {
      this.ModelState.AddModelError("main", "Base URL required");
      return this.ConfigureWebhook(id);
    }
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.LoadAllByAccountID(account.AccountID).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass.ToString() == id.ToLower())).FirstOrDefault<ExternalDataSubscription>();
    if (dataSubscription == null)
    {
      dataSubscription = new ExternalDataSubscription();
      dataSubscription.AccountID = account.AccountID;
      dataSubscription.ContentHeaderType = "application/json";
      dataSubscription.verb = "HttpPost";
      dataSubscription.ExternalID = "none";
      dataSubscription.eExternalDataSubscriptionType = eExternalDataSubscriptionType.omf_pi;
      dataSubscription.eExternalDataSubscriptionClass = (eExternalDataSubscriptionClass) eEDSClass;
    }
    dataSubscription.ConnectionInfo1 = connectionInfo1;
    dataSubscription.SendWithoutDataMessage = false;
    dataSubscription.IgnoreSSLErrors = !validateSSL.ToBool();
    dataSubscription.Save();
    dataSubscription.SetPropertyValue(nameof (username), username);
    if (!string.IsNullOrEmpty(password))
      dataSubscription.SetPropertyValue(nameof (password), MonnitSession.UseEncryption ? password.Encrypt() : password);
    dataSubscription.SetPropertyValue("header", "x-requested-with", "xmlhttprequest");
    dataSubscription.SetPropertyValue("header", "messagetype", "data");
    dataSubscription.SetPropertyValue("header", "action", "update");
    dataSubscription.SetPropertyValue("header", "messageformat", "JSON");
    dataSubscription.SetPropertyValue("header", "omfversion", "1.1");
    dataSubscription.LogAuditData(eAuditAction.Create, eAuditObject.Webhook, MonnitSession.CurrentCustomer.CustomerID, dataSubscription.AccountID, "Edited webhook settings");
    if (dataSubscription.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook && MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID != dataSubscription.ExternalDataSubscriptionID)
    {
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscriptionID = dataSubscription.ExternalDataSubscriptionID;
      MonnitSession.CurrentCustomer.Account.ExternalDataSubscription = dataSubscription;
      MonnitSession.CurrentCustomer.Account.Save();
    }
    return (ActionResult) this.View((object) dataSubscription);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ConfigurePISensor(long id, long sensorID)
  {
    ExternalDataSubscription sub = ExternalDataSubscription.Load(id);
    Sensor sensor = Sensor.Load(sensorID);
    Account account = Account.Load(sub.AccountID);
    if (sub == null || account == null || sensor == null || sensor.AccountID != sub.AccountID || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")) || !account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.Content("Failed");
    string type = PiIntegrationHelper.GetType(sensor.ApplicationID);
    string container = PiIntegrationHelper.GetContainer(sensor);
    ExternalDataSubscriptionResponse response;
    PiIntegrationHelper.SendCall(sub, type, "type", "create", out response);
    return PiIntegrationHelper.SendCall(sub, container, "container", "create", out response) ? (ActionResult) this.Content("Success") : (ActionResult) this.Content(response.Response);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult ConfigurePISensorMultiple(long id)
  {
    ExternalDataSubscription sub = ExternalDataSubscription.Load(id);
    Account account = Account.Load(sub.AccountID);
    if (sub == null || account == null || !MonnitSession.CurrentCustomer.CanSeeAccount(account.AccountID) || !MonnitSession.CurrentCustomer.Account.CurrentSubscription.Can(Feature.Find("can_webhook_push")) || !account.CurrentSubscription.Can(Feature.Find("can_webhook_push")))
      return (ActionResult) this.Content("Failed");
    Dictionary<long, string> model = new Dictionary<long, string>();
    foreach (Sensor sensor in Sensor.LoadByAccountID(account.AccountID))
    {
      string container = PiIntegrationHelper.GetContainer(sensor);
      if (PiIntegrationHelper.SendCall(sub, container, "container", "create", out ExternalDataSubscriptionResponse _))
        model.Add(sensor.SensorID, "Success");
      else
        model.Add(sensor.SensorID, "Failed");
    }
    return (ActionResult) this.PartialView("_ConfigurePISensorMultiple", (object) model);
  }

  [AuthorizeDefault]
  public ActionResult PIHistory(long id)
  {
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.Load(id);
    if (dataSubscription == null)
      dataSubscription = new ExternalDataSubscription()
      {
        AccountID = MonnitSession.CurrentCustomer.AccountID
      };
    Account acct = Account.Load(dataSubscription.AccountID);
    return acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct) || !acct.CurrentSubscription.Can(Feature.Find("can_webhook_push")) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) ExternalDataSubscriptionAttempt.LoadBySubscriptionAndDate(dataSubscription.ExternalDataSubscriptionID, DateTime.UtcNow.AddDays(-1.0), DateTime.UtcNow, 0L, 100));
  }

  [AuthorizeDefault]
  public ActionResult PIHistoryRefresh(long id)
  {
    DateTime utcFromLocalById1 = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime utcFromLocalById2 = Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
    return (ActionResult) this.PartialView("WatsonHistoryList", (object) ExternalDataSubscriptionAttempt.LoadBySubscriptionAndDate(id, utcFromLocalById1, utcFromLocalById2, 0L, 100));
  }

  [AuthorizeDefault]
  public ActionResult PIAttempt(long id)
  {
    ExternalDataSubscriptionAttempt model = ExternalDataSubscriptionAttempt.Load(id);
    if (model == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    Account acct = Account.Load(ExternalDataSubscription.Load(model.ExternalDataSubscriptionID).AccountID);
    return acct == null || !MonnitSession.CurrentCustomer.CanSeeAccount(acct) || !acct.CurrentSubscription.Can(Feature.Find("can_webhook_push")) ? (ActionResult) this.RedirectToRoute("Default", (object) new
    {
      action = "Index",
      controller = "Overview"
    }) : (ActionResult) this.View((object) model);
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult ExportSensorData(long id)
  {
    // ISSUE: reference to a compiler-generated field
    if (ExportController.\u003C\u003Eo__41.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExportController.\u003C\u003Eo__41.\u003C\u003Ep__0 = CallSite<Action<CallSite, ExportController, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName | CSharpBinderFlags.ResultDiscarded, "ExportSensorDataInit", (IEnumerable<Type>) null, typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ExportController.\u003C\u003Eo__41.\u003C\u003Ep__0.Target((CallSite) ExportController.\u003C\u003Eo__41.\u003C\u003Ep__0, this, this.ViewBag);
    return (ActionResult) this.View((object) id);
  }

  public static void ExportSensorDataInit(object viewbag)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>()
    {
      {
        "Sensor_Name",
        "on"
      },
      {
        "Date",
        "on"
      },
      {
        "Value",
        "on"
      },
      {
        "Formatted_Value",
        "on"
      },
      {
        "Battery",
        "on"
      },
      {
        "Raw_Data",
        "on"
      },
      {
        "Sensor_State",
        "on"
      },
      {
        "GatewayID",
        "on"
      },
      {
        "Alert_Sent",
        "on"
      },
      {
        "Signal_Strength",
        "on"
      },
      {
        "Voltage",
        "on"
      }
    };
    // ISSUE: reference to a compiler-generated field
    if (ExportController.\u003C\u003Eo__42.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExportController.\u003C\u003Eo__42.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Dictionary<string, string>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Dictionary", typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = ExportController.\u003C\u003Eo__42.\u003C\u003Ep__0.Target((CallSite) ExportController.\u003C\u003Eo__42.\u003C\u003Ep__0, viewbag, dictionary);
    // ISSUE: reference to a compiler-generated field
    if (ExportController.\u003C\u003Eo__42.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExportController.\u003C\u003Eo__42.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AppendDateTimeRange", typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = ExportController.\u003C\u003Eo__42.\u003C\u003Ep__1.Target((CallSite) ExportController.\u003C\u003Eo__42.\u003C\u003Ep__1, viewbag, false);
    // ISSUE: reference to a compiler-generated field
    if (ExportController.\u003C\u003Eo__42.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExportController.\u003C\u003Eo__42.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AppendSensorID", typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = ExportController.\u003C\u003Eo__42.\u003C\u003Ep__2.Target((CallSite) ExportController.\u003C\u003Eo__42.\u003C\u003Ep__2, viewbag, false);
    // ISSUE: reference to a compiler-generated field
    if (ExportController.\u003C\u003Eo__42.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExportController.\u003C\u003Eo__42.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "AppendTimestamp", typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj4 = ExportController.\u003C\u003Eo__42.\u003C\u003Ep__3.Target((CallSite) ExportController.\u003C\u003Eo__42.\u003C\u003Ep__3, viewbag, false);
  }

  [NoCache]
  [AuthorizeDefault]
  public ActionResult ExportGatewayData(long id)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    dictionary.Add("Gateway_Name", "on");
    dictionary.Add("ReceivedDate", "on");
    dictionary.Add("Power", "on");
    dictionary.Add("Battery", "on");
    dictionary.Add("MessageType", "on");
    dictionary.Add("MessageCount", "on");
    dictionary.Add("MeetsNotificationRequirement", "on");
    dictionary.Add("Signal_Strength", "on");
    // ISSUE: reference to a compiler-generated field
    if (ExportController.\u003C\u003Eo__43.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExportController.\u003C\u003Eo__43.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, Dictionary<string, string>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Dictionary", typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = ExportController.\u003C\u003Eo__43.\u003C\u003Ep__0.Target((CallSite) ExportController.\u003C\u003Eo__43.\u003C\u003Ep__0, this.ViewBag, dictionary);
    Gateway model = Gateway.Load(id);
    if (model == null)
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Export",
        ErrorTranslateTag = "Export/ExportGatewayData|",
        ErrorTitle = "Unauthorized Access to Gateway",
        ErrorMessage = "You do not have permission to access this page."
      });
    CSNet csNet = CSNet.Load(model.CSNetID);
    if (csNet != null && MonnitSession.CurrentCustomer.CanSeeNetwork(csNet.CSNetID))
      return (ActionResult) this.View((object) model);
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
    {
      ErrorLocation = "Export",
      ErrorTranslateTag = "Export/ExportGatewayData|",
      ErrorTitle = "Unauthorized Access to Gateway",
      ErrorMessage = "You do not have permission to access this page."
    });
  }

  [AuthorizeDefault]
  public ActionResult ExportLocationData(long id)
  {
    Gateway model = Gateway.Load(id);
    if (model != null && MonnitSession.CurrentCustomer.CanSeeNetwork(model.CSNetID))
      return (ActionResult) this.View((object) model);
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
    {
      ErrorLocation = "Export",
      ErrorTranslateTag = "Export/ExportLocationData|",
      ErrorTitle = "Invalid Device Identifier",
      ErrorMessage = "You do not have permission to access this page."
    });
  }

  [AuthorizeDefault]
  public ActionResult ExportNotificationData(long id)
  {
    Notification model = Notification.Load(id);
    if (model != null && MonnitSession.CustomerCan("Sensor_View_Notifications") && MonnitSession.CurrentCustomer.CanSeeAccount(model.AccountID))
      return (ActionResult) this.View((object) model);
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
    {
      ErrorLocation = "Export",
      ErrorTranslateTag = "Export/ExportNotificationData|",
      ErrorTitle = "Invalid Notification Identifier",
      ErrorMessage = "You do not have permission to access this page."
    });
  }

  [AuthorizeDefault]
  public ActionResult ReportIndex()
  {
    if (!MonnitSession.CustomerCan("Navigation_View_Reports"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Reports",
        ErrorTranslateTag = "Report/Index|",
        ErrorTitle = "Unauthorized access to reports",
        ErrorMessage = "You do not have permission to access this page."
      });
    int totalReports = 0;
    List<ReportSchedule> reportList = this.GetReportList(out totalReports);
    // ISSUE: reference to a compiler-generated field
    if (ExportController.\u003C\u003Eo__46.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExportController.\u003C\u003Eo__46.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ReportCount", typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = ExportController.\u003C\u003Eo__46.\u003C\u003Ep__0.Target((CallSite) ExportController.\u003C\u003Eo__46.\u003C\u003Ep__0, this.ViewBag, reportList.Count);
    return (ActionResult) this.View((object) reportList);
  }

  [AuthorizeDefault]
  public ActionResult ReportFilter(bool? isActive, string q)
  {
    try
    {
      int totalReports = 0;
      List<ReportSchedule> reportScheduleList = this.GetReportList(out totalReports);
      if (isActive.HasValue)
        reportScheduleList = reportScheduleList.Where<ReportSchedule>((Func<ReportSchedule, bool>) (m =>
        {
          int num1 = m.IsActive ? 1 : 0;
          bool? nullable = isActive;
          int num2 = nullable.GetValueOrDefault() ? 1 : 0;
          return num1 == num2 & nullable.HasValue;
        })).ToList<ReportSchedule>();
      if (!string.IsNullOrEmpty(q))
        reportScheduleList = reportScheduleList.Where<ReportSchedule>((Func<ReportSchedule, bool>) (s => s.Name.ToLower().Contains(q.ToLower()) || s.Name.ToString().ToLower().Contains(q.ToLower()))).ToList<ReportSchedule>();
      // ISSUE: reference to a compiler-generated field
      if (ExportController.\u003C\u003Eo__47.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExportController.\u003C\u003Eo__47.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ReportCount", typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = ExportController.\u003C\u003Eo__47.\u003C\u003Ep__0.Target((CallSite) ExportController.\u003C\u003Eo__47.\u003C\u003Ep__0, this.ViewBag, reportScheduleList.Count);
      return (ActionResult) this.PartialView("ReportDetails", (object) reportScheduleList);
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  public List<ReportSchedule> GetReportList(out int totalReports)
  {
    int Class = MonnitSession.ReportListFilters.Class;
    int Status = MonnitSession.ReportListFilters.Status;
    long applicationId = MonnitSession.ReportListFilters.ApplicationID;
    string Name = MonnitSession.ReportListFilters.Name;
    List<ReportSchedule> source1 = ReportSchedule.LoadByAccount(MonnitSession.CurrentCustomer.AccountID);
    totalReports = source1.Count<ReportSchedule>();
    IEnumerable<ReportSchedule> source2 = source1.Where<ReportSchedule>((Func<ReportSchedule, bool>) (n =>
    {
      if (n.ScheduleType.ToInt() != Class && Class != int.MinValue || n.IsActive.ToInt() != Status && Status != int.MinValue)
        return false;
      return Name == "" || n.Name.ToLower().Contains(Name.ToLower());
    }));
    IEnumerable<ReportSchedule> source3;
    switch (MonnitSession.ReportListSort.Column)
    {
      case "Schedule":
        source3 = !(MonnitSession.ReportListSort.Direction == "Desc") ? (IEnumerable<ReportSchedule>) source2.OrderBy<ReportSchedule, eReportScheduleType>((Func<ReportSchedule, eReportScheduleType>) (n => n.ScheduleType)) : (IEnumerable<ReportSchedule>) source2.OrderByDescending<ReportSchedule, eReportScheduleType>((Func<ReportSchedule, eReportScheduleType>) (n => n.ScheduleType));
        break;
      case "Last Sent":
        source3 = !(MonnitSession.ReportListSort.Direction == "Desc") ? (IEnumerable<ReportSchedule>) source2.OrderBy<ReportSchedule, DateTime>((Func<ReportSchedule, DateTime>) (n => n.LastRunDate)) : (IEnumerable<ReportSchedule>) source2.OrderByDescending<ReportSchedule, DateTime>((Func<ReportSchedule, DateTime>) (n => n.LastRunDate));
        break;
      case "Status":
        source3 = !(MonnitSession.ReportListSort.Direction == "Desc") ? (IEnumerable<ReportSchedule>) source2.OrderBy<ReportSchedule, bool>((Func<ReportSchedule, bool>) (n => n.IsActive)) : (IEnumerable<ReportSchedule>) source2.OrderByDescending<ReportSchedule, bool>((Func<ReportSchedule, bool>) (n => n.IsActive));
        break;
      case "Type":
        source3 = !(MonnitSession.ReportListSort.Direction == "Desc") ? (IEnumerable<ReportSchedule>) source2.OrderBy<ReportSchedule, string>((Func<ReportSchedule, string>) (n => n.Report.Name)) : (IEnumerable<ReportSchedule>) source2.OrderByDescending<ReportSchedule, string>((Func<ReportSchedule, string>) (n => n.Report.Name));
        break;
      default:
        source3 = !(MonnitSession.ReportListSort.Direction == "Desc") ? (IEnumerable<ReportSchedule>) source2.OrderBy<ReportSchedule, string>((Func<ReportSchedule, string>) (n => n.Name)) : (IEnumerable<ReportSchedule>) source2.OrderByDescending<ReportSchedule, string>((Func<ReportSchedule, string>) (n => n.Name));
        break;
    }
    return source3.ToList<ReportSchedule>();
  }

  [AuthorizeDefault]
  public ActionResult ReportHistory(long id)
  {
    ReportSchedule model = ReportSchedule.Load(id);
    if (model != null && MonnitSession.CustomerCan("Navigation_View_Reports") && MonnitSession.CurrentCustomer.CanSeeAccount(model.AccountID))
      return (ActionResult) this.View((object) model);
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
    {
      ErrorLocation = "Reports",
      ErrorTranslateTag = "Report/Index|",
      ErrorTitle = "Unauthorized access to reports",
      ErrorMessage = "You do not have permission to access this page."
    });
  }

  [AuthorizeDefault]
  public ActionResult ReportEdit(long? id, long? queryID)
  {
    ReportSchedule model = ReportSchedule.Load(id ?? long.MinValue);
    if (model == null)
    {
      if (!queryID.HasValue)
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index",
          controller = "Overview"
        });
      model = new ReportSchedule();
      model.AccountID = MonnitSession.CurrentCustomer.AccountID;
      model.Name = "New Report";
      model.ReportQueryID = queryID ?? long.MinValue;
      model.Schedule = "1";
      model.ScheduleType = eReportScheduleType.Monthly;
      model.StartDate = DateTime.Now.Date;
      model.EndDate = DateTime.Now.Date;
      model.StartTime = new TimeSpan(12, 0, 0);
    }
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult ReportRecipient(long id, string q)
  {
    ReportSchedule model = ReportSchedule.Load(id);
    if (model == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    if (!MonnitSession.IsAuthorizedForAccount(model.AccountID) || !MonnitSession.CustomerCan("Navigation_View_Reports"))
      return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
      {
        ErrorLocation = "Reports",
        ErrorTranslateTag = "Report/Index|",
        ErrorTitle = "Unauthorized access to reports",
        ErrorMessage = "You do not have permission to access this page."
      });
    List<ReportRecipientData> reportRecipientDataList = ReportRecipientData.SearchPotentialReportRecipient(MonnitSession.CurrentCustomer.CustomerID, id, q, MonnitSession.CurrentCustomer.AccountID);
    // ISSUE: reference to a compiler-generated field
    if (ExportController.\u003C\u003Eo__51.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExportController.\u003C\u003Eo__51.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<long>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "RecipientIDs", typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = ExportController.\u003C\u003Eo__51.\u003C\u003Ep__0.Target((CallSite) ExportController.\u003C\u003Eo__51.\u003C\u003Ep__0, this.ViewBag, model.DistributionList.Select<ReportDistribution, long>((Func<ReportDistribution, long>) (rd => rd.CustomerID)).ToList<long>());
    // ISSUE: reference to a compiler-generated field
    if (ExportController.\u003C\u003Eo__51.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExportController.\u003C\u003Eo__51.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<ReportRecipientData>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ReportUsers", typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = ExportController.\u003C\u003Eo__51.\u003C\u003Ep__1.Target((CallSite) ExportController.\u003C\u003Eo__51.\u003C\u003Ep__1, this.ViewBag, reportRecipientDataList);
    // ISSUE: reference to a compiler-generated field
    if (ExportController.\u003C\u003Eo__51.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExportController.\u003C\u003Eo__51.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, ReportSchedule, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Report", typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = ExportController.\u003C\u003Eo__51.\u003C\u003Ep__2.Target((CallSite) ExportController.\u003C\u003Eo__51.\u003C\u003Ep__2, this.ViewBag, model);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult ReportRecipientsList(long id, string q)
  {
    ReportSchedule reportSchedule = ReportSchedule.Load(id);
    if (reportSchedule == null)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index",
        controller = "Overview"
      });
    List<ReportRecipientData> model = ReportRecipientData.SearchPotentialReportRecipient(MonnitSession.CurrentCustomer.CustomerID, id, q, MonnitSession.CurrentCustomer.AccountID);
    // ISSUE: reference to a compiler-generated field
    if (ExportController.\u003C\u003Eo__52.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExportController.\u003C\u003Eo__52.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<long>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "RecipientIDs", typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = ExportController.\u003C\u003Eo__52.\u003C\u003Ep__0.Target((CallSite) ExportController.\u003C\u003Eo__52.\u003C\u003Ep__0, this.ViewBag, reportSchedule.DistributionList.Select<ReportDistribution, long>((Func<ReportDistribution, long>) (rd => rd.CustomerID)).ToList<long>());
    // ISSUE: reference to a compiler-generated field
    if (ExportController.\u003C\u003Eo__52.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExportController.\u003C\u003Eo__52.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<ReportRecipientData>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ReportUsers", typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = ExportController.\u003C\u003Eo__52.\u003C\u003Ep__1.Target((CallSite) ExportController.\u003C\u003Eo__52.\u003C\u003Ep__1, this.ViewBag, model);
    // ISSUE: reference to a compiler-generated field
    if (ExportController.\u003C\u003Eo__52.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExportController.\u003C\u003Eo__52.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, ReportSchedule, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Report", typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = ExportController.\u003C\u003Eo__52.\u003C\u003Ep__2.Target((CallSite) ExportController.\u003C\u003Eo__52.\u003C\u003Ep__2, this.ViewBag, reportSchedule);
    return (ActionResult) this.PartialView("ReportUserList", (object) model);
  }

  [AuthorizeDefault]
  public ActionResult ToggleReportRecipient(long id, long customerID, bool add)
  {
    try
    {
      ReportSchedule reportSchedule = ReportSchedule.Load(id);
      if (reportSchedule == null)
        return (ActionResult) this.Content("Report Schedule not found");
      Customer customer = Customer.Load(customerID);
      if (customer == null)
        return (ActionResult) this.Content("User Not Found");
      if (reportSchedule.AccountID != customer.AccountID && CustomerAccountLink.Load(customer.CustomerID, reportSchedule.AccountID) == null)
      {
        bool flag = false;
        foreach (Account ancestor in Account.Ancestors(MonnitSession.CurrentCustomer.CustomerID, reportSchedule.AccountID))
        {
          if (ancestor.AccountID == customer.AccountID)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          return (ActionResult) this.Content("User not found");
      }
      if (add)
        reportSchedule.AddCustomer(customer);
      else
        reportSchedule.RemoveCustomer(customer);
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.Content("Failed");
    }
  }

  [AuthorizeDefault]
  public ActionResult ReportTemplates(long? ID)
  {
    return (ActionResult) this.View((object) ReportQuery.LoadByAccount(MonnitSession.CurrentTheme.AccountThemeID, MonnitSession.CurrentCustomer.AccountID, MonnitSession.AccountCan("view_maps"), ID ?? long.MinValue));
  }

  [AuthorizeDefault]
  public ActionResult CreateNewReport(long? id, long? queryID)
  {
    ReportSchedule model = ReportSchedule.Load(id ?? long.MinValue);
    if (model == null)
    {
      if (!queryID.HasValue)
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index"
        });
      model = new ReportSchedule();
      model.AccountID = MonnitSession.CurrentCustomer.AccountID;
      model.Name = "New Report";
      model.ReportQueryID = queryID ?? long.MinValue;
      model.Schedule = "1";
      model.ScheduleType = eReportScheduleType.Monthly;
      ReportSchedule reportSchedule1 = model;
      DateTime now = DateTime.Now;
      DateTime date1 = now.Date;
      reportSchedule1.StartDate = date1;
      ReportSchedule reportSchedule2 = model;
      now = DateTime.Now;
      DateTime date2 = now.Date;
      reportSchedule2.EndDate = date2;
      model.StartTime = new TimeSpan(12, 0, 0);
    }
    ReportQuery reportQuery = ReportQuery.Load(model.ReportQueryID);
    // ISSUE: reference to a compiler-generated field
    if (ExportController.\u003C\u003Eo__55.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExportController.\u003C\u003Eo__55.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ReportQueryName", typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = ExportController.\u003C\u003Eo__55.\u003C\u003Ep__0.Target((CallSite) ExportController.\u003C\u003Eo__55.\u003C\u003Ep__0, this.ViewBag, reportQuery == null ? string.Empty : reportQuery.Name);
    // ISSUE: reference to a compiler-generated field
    if (ExportController.\u003C\u003Eo__55.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExportController.\u003C\u003Eo__55.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ReportQueryDescription", typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = ExportController.\u003C\u003Eo__55.\u003C\u003Ep__1.Target((CallSite) ExportController.\u003C\u003Eo__55.\u003C\u003Ep__1, this.ViewBag, reportQuery == null ? string.Empty : reportQuery.Description);
    ReportType reportType = reportQuery == null ? (ReportType) null : ReportType.Load(reportQuery.ReportTypeID);
    // ISSUE: reference to a compiler-generated field
    if (ExportController.\u003C\u003Eo__55.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExportController.\u003C\u003Eo__55.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ReportTypeName", typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = ExportController.\u003C\u003Eo__55.\u003C\u003Ep__2.Target((CallSite) ExportController.\u003C\u003Eo__55.\u003C\u003Ep__2, this.ViewBag, reportType == null ? string.Empty : reportType.Name);
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  public ActionResult Delete(long id)
  {
    ReportSchedule reportSchedule = ReportSchedule.Load(id);
    if (!MonnitSession.IsAuthorizedForAccount(reportSchedule.AccountID))
      return (ActionResult) this.PartialView("Unauthorized");
    try
    {
      if (MonnitSession.IsReportFavorite(id))
      {
        CustomerFavorite customerFavorite = MonnitSession.CurrentCustomerFavorites.AllFavorites.Find((Predicate<CustomerFavorite>) (x => x.ReportScheduleID == id));
        MonnitSession.CurrentCustomerFavorites.Invalidate();
        customerFavorite.Delete();
        reportSchedule.Delete();
        return (ActionResult) this.Content("Success");
      }
      reportSchedule.Delete();
      return (ActionResult) this.Content("Success");
    }
    catch
    {
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "ReportIndex",
        controller = "Export"
      });
    }
  }

  [AuthorizeDefault]
  public ActionResult _BuildReport(long? id, long? queryID)
  {
    ReportSchedule model = ReportSchedule.Load(id ?? long.MinValue);
    if (model == null)
    {
      if (!queryID.HasValue)
        return (ActionResult) this.RedirectToRoute("Default", (object) new
        {
          action = "Index"
        });
      model = new ReportSchedule();
      model.AccountID = MonnitSession.CurrentCustomer.AccountID;
      model.Name = "New Report";
      model.ReportQueryID = queryID ?? long.MinValue;
      model.Schedule = "1";
      model.ScheduleType = eReportScheduleType.Monthly;
      ReportSchedule reportSchedule1 = model;
      DateTime now = DateTime.Now;
      DateTime date1 = now.Date;
      reportSchedule1.StartDate = date1;
      ReportSchedule reportSchedule2 = model;
      now = DateTime.Now;
      DateTime date2 = now.Date;
      reportSchedule2.EndDate = date2;
      model.StartTime = new TimeSpan(12, 0, 0);
    }
    return (ActionResult) this.View((object) model);
  }

  [AuthorizeDefault]
  [HttpPost]
  public ActionResult _BuildReport(ReportSchedule model, FormCollection collection)
  {
    if (model.ReportQueryID < 0L)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    if (model.AccountID != MonnitSession.CurrentCustomer.AccountID)
      return (ActionResult) this.RedirectToRoute("Default", (object) new
      {
        action = "Index"
      });
    if (string.IsNullOrWhiteSpace(model.Name))
      this.ModelState.AddModelError("Name", "Name is required");
    if (string.IsNullOrWhiteSpace(model.Schedule))
      this.ModelState.AddModelError("Schedule", "Schedule is required");
    DateTime date = Monnit.TimeZone.GetLocalTimeById(DateTime.UtcNow, MonnitSession.CurrentCustomer.Account.TimeZoneID).Date;
    DateTime startDate = model.StartDate;
    if (model.StartDate < new DateTime(2014, 1, 1) || model.StartDate > date.AddYears(1))
      this.ModelState.AddModelError("StartDate", "Start Date must be valid date");
    DateTime endDate = model.EndDate;
    if (model.EndDate < date || model.EndDate < model.StartDate)
      this.ModelState.AddModelError("EndDate", "End Date must be valid date in the future");
    TimeSpan startTime = model.StartTime;
    if (false)
      model.StartTime = new TimeSpan(12, 0, 0);
    List<ReportParameterValue> reportParameterValueList = new List<ReportParameterValue>();
    foreach (ReportParameter parameter in model.Report.Parameters)
    {
      FormCollection formCollection1 = collection;
      long reportParameterId = parameter.ReportParameterID;
      string name1 = "Param_" + reportParameterId.ToString();
      if (string.IsNullOrWhiteSpace(formCollection1[name1]))
      {
        ModelStateDictionary modelState = this.ModelState;
        reportParameterId = parameter.ReportParameterID;
        string key = "Param_" + reportParameterId.ToString();
        string errorMessage = parameter.LabelText + " is required.";
        modelState.AddModelError(key, errorMessage);
      }
      else
      {
        FormCollection formCollection2 = collection;
        reportParameterId = parameter.ReportParameterID;
        string name2 = "Param_" + reportParameterId.ToString();
        string dateFormatted = formCollection2[name2];
        ReportParameterValue reportParameterValue = model.FindParameter(parameter.ReportParameterID);
        if (reportParameterValue == null)
        {
          reportParameterValue = new ReportParameterValue();
          reportParameterValue.ReportParameterID = parameter.ReportParameterID;
        }
        if (parameter.Type.Name == "Date")
        {
          try
          {
            dateFormatted = DateTime.ParseExact(dateFormatted, MonnitSession.CurrentCustomer.Preferences["Date Format"].ToString(), (IFormatProvider) CultureInfo.InvariantCulture).ToDateFormatted("yyyy-MM-dd");
          }
          catch
          {
          }
        }
        reportParameterValue.Value = dateFormatted;
        reportParameterValueList.Add(reportParameterValue);
      }
    }
    if (this.ModelState.IsValid)
    {
      bool flag = false;
      if (model.PrimaryKeyValue < 0L)
        flag = true;
      if (collection["SendAsAttachment"].ToBool() || ConfigData.AppSettings("IsEnterprise").ToBool())
        model.SendAsAttachment = true;
      model.Save();
      foreach (ReportParameterValue reportParameterValue in reportParameterValueList)
      {
        reportParameterValue.ReportScheduleID = model.ReportScheduleID;
        reportParameterValue.Save();
      }
      if (flag)
      {
        model.AddCustomer(MonnitSession.CurrentCustomer);
        return (ActionResult) this.Content("Success");
      }
      // ISSUE: reference to a compiler-generated field
      if (ExportController.\u003C\u003Eo__58.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExportController.\u003C\u003Eo__58.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof (ExportController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = ExportController.\u003C\u003Eo__58.\u003C\u003Ep__0.Target((CallSite) ExportController.\u003C\u003Eo__58.\u003C\u003Ep__0, this.ViewBag, "Success");
    }
    return (ActionResult) this.View((object) model);
  }

  public ActionResult GetReportFile(string guid, long ScheduledReportsToStorageID)
  {
    ScheduledReportsToStorage reportsToStorage = ScheduledReportsToStorage.Load(ScheduledReportsToStorageID);
    string searchFileName = reportsToStorage.ReportFileName.Replace(",", "").Replace("/", "_").Replace("\\", "_");
    AzureTempFile azureTempFile = AzureTempFile.Files($"reportfile/{reportsToStorage.GUID}/", searchFileName).FirstOrDefault<AzureTempFile>();
    if (azureTempFile != null && guid == reportsToStorage.GUID)
    {
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + azureTempFile.FileName);
      string[] strArray = azureTempFile.FileName.Split('.');
      switch (strArray[strArray.Length - 1].ToStringSafe().ToLower())
      {
        case "pdf":
          this.Response.ContentType = "application/pdf";
          break;
        case "csv":
          this.Response.ContentType = "text/csv";
          break;
        default:
          this.Response.ContentType = "application/octet-stream";
          break;
      }
      azureTempFile.DownloadToStream(this.Response.OutputStream);
    }
    return (ActionResult) new EmptyResult();
  }

  public ActionResult SetActive(long id, bool active)
  {
    ReportSchedule reportSchedule = ReportSchedule.Load(id);
    if (reportSchedule == null || !MonnitSession.IsAuthorizedForAccount(reportSchedule.AccountID))
      return (ActionResult) this.Content("Unauthorized");
    reportSchedule.IsActive = active;
    reportSchedule.Save();
    return (ActionResult) this.Content("Success");
  }

  [AuthorizeDefault]
  public ActionResult ReportCategory()
  {
    if (MonnitSession.CustomerCan("Navigation_View_Reports"))
      return (ActionResult) this.View((object) ReportType.LoadByAccount(MonnitSession.CurrentTheme.AccountThemeID, MonnitSession.CurrentCustomer.AccountID, MonnitSession.AccountCan("view_maps")));
    return (ActionResult) this.View("~/Views/Overview/ErrorDisplay.aspx", (object) new ErrorModel()
    {
      ErrorLocation = "Reports",
      ErrorTranslateTag = "Report/Index|",
      ErrorTitle = "Unauthorized access to reports",
      ErrorMessage = "You do not have permission to access this page."
    });
  }
}
