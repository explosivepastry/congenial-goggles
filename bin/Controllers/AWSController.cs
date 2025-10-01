// Decompiled with JetBrains decompiler
// Type: iMonnit.Controllers.AWSController
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.ControllerBase;
using iMonnit.Models.CustomModels;
using Monnit;
using Newtonsoft.Json;
using RedefineImpossible;
using System;
using System.Globalization;
using System.IO;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Controllers;

public class AWSController : Controller
{
  public ActionResult EmailCallback()
  {
    bool flag = true;
    try
    {
      string end = new StreamReader(this.Request.InputStream).ReadToEnd();
      if (ConfigData.AppSettings("EnableDebugLog").ToBool())
        new DebugLog()
        {
          IdentifierType = "AWSController.EmailCallback",
          IdentifierValue = long.MinValue,
          Severity = long.MinValue,
          Server = "",
          LogDate = DateTime.UtcNow,
          CodePath = "AWSController.EmailCallback",
          Message = end
        }.Save();
      AWSCallbackModel awsCallbackModel = JsonConvert.DeserializeObject<AWSCallbackModel>(end);
      if (awsCallbackModel == null || awsCallbackModel.ParsedMessage == null)
      {
        new Exception("Error content: " + end).Log("AWSController.EmailCallback ");
        return (ActionResult) this.Content("You are not authorized for this resource");
      }
      long num1 = 0;
      long ID = 0;
      long num2 = 0;
      string str1 = string.Empty;
      if (awsCallbackModel.ParsedMessage.mail != null)
      {
        foreach (AWSEmailCallbackMailHeaderModel header in awsCallbackModel.ParsedMessage.mail.headers)
        {
          if (header.name == "X-MSYS-API")
          {
            AWSEmailCallbackMailHeaderMSYSModel mailHeaderMsysModel = JsonConvert.DeserializeObject<AWSEmailCallbackMailHeaderMSYSModel>(header.value.Replace(" ", ""));
            num1 = mailHeaderMsysModel.metadata.CustomerID.ToLong();
            ID = mailHeaderMsysModel.metadata.NotificationRecordedID.ToLong();
            num2 = mailHeaderMsysModel.metadata.SystemEmailID.ToLong();
            str1 = mailHeaderMsysModel.metadata.CheckAddress;
          }
        }
      }
      if (ID > 0L)
      {
        NotificationRecorded notificationRecorded = NotificationRecorded.Load(ID);
        if (notificationRecorded != null && num1 == notificationRecorded.CustomerID)
        {
          notificationRecorded.Status = awsCallbackModel.ParsedMessage.eventType;
          switch (awsCallbackModel.ParsedMessage.eventType.ToLower())
          {
            case "delivery":
              notificationRecorded.Status = "Delivered";
              break;
            case "delay":
              notificationRecorded.Status = "Delayed";
              break;
            case "bounce":
              notificationRecorded.Status = "Hard Bounce";
              notificationRecorded.DoRetry = false;
              string str2 = $"Bounce NotificationRecordedID: {ID}";
              try
              {
                if (awsCallbackModel.ParsedMessage.bounce.bounceType == "Transient")
                {
                  notificationRecorded.Status = "Soft Bounce";
                  str2 = $"Soft Bounce {awsCallbackModel.ParsedMessage.bounce.bounceSubType} NotificationRecordedID: {ID}";
                }
                else
                  str2 = $"Hard Bounce {awsCallbackModel.ParsedMessage.bounce.bounceSubType} NotificationRecordedID: {ID}";
              }
              catch
              {
              }
              if (ConfigData.AppSettings("EnableDebugLog").ToBool())
              {
                new DebugLog()
                {
                  IdentifierType = "AWSController.AWSEmailCallback",
                  IdentifierValue = long.MinValue,
                  Severity = long.MinValue,
                  Server = "",
                  LogDate = DateTime.UtcNow,
                  CodePath = "AWSController.AWSEmailCallback",
                  Message = str2
                }.Save();
                break;
              }
              break;
            case "complaint":
              notificationRecorded.Status = "Spam Complaint";
              notificationRecorded.DoRetry = false;
              string str3 = "Spam Complaint | NotificationRecordedID: " + ID.ToString();
              HomeControllerBase.Unsubscribe(notificationRecorded.SentTo, str3);
              ExceptionLog.Log(new Exception(str3)
              {
                Source = "AWSEmailCallback"
              });
              break;
            default:
              string titleCase = new CultureInfo("en-US").TextInfo.ToTitleCase(awsCallbackModel.ParsedMessage.eventType);
              notificationRecorded.Status = titleCase;
              ExceptionLog.Log(new Exception($"{titleCase} | NotificationRecordedID: {ID.ToString()}")
              {
                Source = "AWSEmailCallback"
              });
              break;
          }
          notificationRecorded.Save();
        }
        else
        {
          ExceptionLog.Log(new Exception($"NotificationRecordedID: {ID.ToString()} CustomerID: {num1.ToString()} does not match the NotificationRecorded CustomerID. This is to prevent random calls to AWS Email Callback without permission.")
          {
            Source = "AWSEmailCallback"
          });
          flag = false;
        }
      }
      if (num2 > 0L)
      {
        SystemEmail systemEmail = SystemEmail.Load(Convert.ToInt64(num2));
        if (systemEmail != null && (systemEmail.ToAddress.Contains(str1) || num1 == systemEmail.CustomerID))
        {
          systemEmail.Status = awsCallbackModel.ParsedMessage.eventType;
          switch (awsCallbackModel.ParsedMessage.eventType.ToLower())
          {
            case "delivery":
              systemEmail.Status = "Delivered";
              break;
            case "delay":
              systemEmail.Status = "Delayed";
              break;
            case "bounce":
              systemEmail.Status = "Hard Bounce";
              systemEmail.DoRetry = false;
              ExceptionLog.Log(new Exception("Hard Bounce | SystemEmailID: " + num2.ToString())
              {
                Source = "AWSEmailCallback"
              });
              break;
            case "complaint":
              systemEmail.Status = "Spam Complaint";
              systemEmail.DoRetry = false;
              Exception ex = new Exception("Spam Complaint | SystemEmailID: " + num2.ToString());
              ex.Source = "AWSEmailCallback";
              ExceptionLog.Log(ex);
              string toAddress = systemEmail.ToAddress;
              char[] separator = new char[1]{ ';' };
              foreach (string str4 in toAddress.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                HomeControllerBase.Unsubscribe(str4.Trim(), ex.Message);
              break;
            default:
              string titleCase = new CultureInfo("en-US").TextInfo.ToTitleCase(awsCallbackModel.ParsedMessage.eventType);
              systemEmail.Status = titleCase;
              ExceptionLog.Log(new Exception($"{titleCase} | SystemEmailID: {num2.ToString()}")
              {
                Source = "AWSEmailCallback"
              });
              break;
          }
          systemEmail.Save();
        }
        else
        {
          ExceptionLog.Log(new Exception($"CheckAddress: {str1} does not match the SystemEmail ToAddress. This is to prevent random calls to AWS Email Callback without permission.")
          {
            Source = "AWSEmailCallback"
          });
          flag = false;
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("AWSController.EmailCallback.MainException: ");
      return (ActionResult) this.Content("Failed");
    }
    return !flag ? (ActionResult) this.Content("You are not authorized for this resource") : (ActionResult) this.Content("Success");
  }
}
