// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.SparkPostController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.ControllerBase;
using iMonnit.Models.CustomModels;
using Monnit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

public class SparkPostController : Controller
{
  public ActionResult Webhook()
  {
    bool flag = true;
    try
    {
      JArray jarray = JArray.Parse(new StreamReader(this.Request.InputStream).ReadToEnd());
      List<SparkPostModel> sparkPostModelList = new List<SparkPostModel>();
      foreach (JToken jtoken in jarray)
      {
        foreach (KeyValuePair<string, JToken> keyValuePair in (JObject) jtoken[(object) "msys"])
        {
          string key = keyValuePair.Key;
          JObject jobject = (JObject) keyValuePair.Value;
          sparkPostModelList.Add(JsonConvert.DeserializeObject<SparkPostModel>(jobject.ToString()));
        }
      }
      foreach (SparkPostModel sparkPostModel in sparkPostModelList)
      {
        long int64_1 = Convert.ToInt64(sparkPostModel.rcpt_meta.CustomerID);
        long int64_2 = Convert.ToInt64(sparkPostModel.rcpt_meta.NotificationRecordedID);
        long int64_3 = Convert.ToInt64(sparkPostModel.rcpt_meta.SystemEmailID);
        string checkAddress = sparkPostModel.rcpt_meta.CheckAddress;
        if (int64_2 > 0L)
        {
          NotificationRecorded notificationRecorded = NotificationRecorded.Load(Convert.ToInt64(int64_2));
          long customerId = notificationRecorded.CustomerID;
          if (notificationRecorded != null && int64_1 == customerId)
          {
            notificationRecorded.Status = sparkPostModel.type;
            switch (sparkPostModel.type.ToLower())
            {
              case "bounce":
              case "out_of_band":
                notificationRecorded.Status = "Bounced";
                break;
              case "delivery":
                notificationRecorded.Status = "Delivered";
                break;
              case "delay":
                notificationRecorded.Status = "Delayed";
                break;
              case "spam_complaint":
              case "policy_rejection":
                notificationRecorded.Status = "Undeliverable";
                Exception ex = new Exception($"{new CultureInfo("en-US").TextInfo.ToTitleCase(sparkPostModel.type)} | NotificationRecordedID: {int64_2.ToString()}");
                ex.Source = "SparkPostWebhook";
                ExceptionLog.Log(ex);
                HomeControllerBase.Unsubscribe(notificationRecorded.SentTo, ex.Message);
                break;
            }
            notificationRecorded.Save();
          }
          else
          {
            ExceptionLog.Log(new Exception($"CustomerID: {int64_1.ToString()} does not match the NotificationRecorded CustomerID. This is to prevent random calls to Sparkpost without permission.")
            {
              Source = "SparkPostWebhook"
            });
            flag = false;
          }
        }
        if (int64_3 > 0L)
        {
          SystemEmail systemEmail = SystemEmail.Load(Convert.ToInt64(int64_3));
          if (systemEmail != null && (systemEmail.ToAddress.Contains(checkAddress) || int64_1 == systemEmail.CustomerID))
          {
            systemEmail.Status = sparkPostModel.type;
            switch (sparkPostModel.type.ToLower())
            {
              case "bounce":
                systemEmail.Status = "Bounced";
                systemEmail.DoRetry = false;
                break;
              case "delivery":
                systemEmail.Status = "Delivered";
                break;
              case "delay":
                systemEmail.Status = "Delayed";
                break;
              case "spam_complaint":
              case "out_of_band":
              case "policy_rejection":
                systemEmail.Status = "Undeliverable";
                systemEmail.DoRetry = false;
                Exception ex = new Exception($"{new CultureInfo("en-US").TextInfo.ToTitleCase(sparkPostModel.type)} | SystemEmailID: {int64_3.ToString()}");
                ex.Source = "SparkPostWebhook";
                ExceptionLog.Log(ex);
                string toAddress = systemEmail.ToAddress;
                char[] separator = new char[1]{ ';' };
                foreach (string str in toAddress.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                  HomeControllerBase.Unsubscribe(str.Trim(), ex.Message);
                break;
            }
            systemEmail.Save();
          }
          else
          {
            ExceptionLog.Log(new Exception($"CheckAddress: {checkAddress} does not match the SystemEmail ToAddress. This is to prevent random calls to Sparkpost without permission.")
            {
              Source = "SparkPostWebhook"
            });
            flag = false;
          }
        }
      }
    }
    catch (Exception ex)
    {
      return (ActionResult) this.Content("Failed" + ex?.ToString());
    }
    return !flag ? (ActionResult) this.Content("You are not authorized for this SparkPost Webhook or either NotificationRecordedID  SystemEmailID is Null") : (ActionResult) this.Content("Success");
  }
}
