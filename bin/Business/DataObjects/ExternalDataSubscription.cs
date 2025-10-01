// Decompiled with JetBrains decompiler
// Type: Monnit.ExternalDataSubscription
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web;

#nullable disable
namespace Monnit;

[DBClass("ExternalDataSubscription")]
public class ExternalDataSubscription : BaseDBObject
{
  private long _ExternalDataSubscriptionID = long.MinValue;
  private long _AccountID = long.MinValue;
  private string _ExternalID = string.Empty;
  private string _ConnectionInfo1 = string.Empty;
  private string _ConnectionInfo2 = string.Empty;
  private string _LastResult = string.Empty;
  private bool _DoSend = true;
  private bool _DoRetry = false;
  private int _BrokenCount = 0;
  private string _ContentHeaderType = "raw";
  private string _verb = "HttpGet";
  private DateTime _LastSuccess = DateTime.MinValue;
  private bool _IsDeleted = false;
  private bool _SendWithoutDataMessage = true;
  private string _Password = string.Empty;
  private string _Username = string.Empty;
  private bool _IgnoreSSLErrors = false;
  private bool _SendRawData = true;
  private eExternalDataSubscriptionType _eExternalDataSubscriptionType;
  private eExternalDataSubscriptionClass _eExternalDataSubscriptionClass;
  private List<ExternalDataSubscriptionProperty> _Properties = (List<ExternalDataSubscriptionProperty>) null;
  public static string DefaultSensorConnectionInfo2 = "\r\n    \"MessageID\" : \"{1}\",\r\n    \"Data\" : \"{2}\",\r\n    \"DisplayData\" : \"{3}\",\r\n    \"MessageDate\" : \"{4}\",\r\n    \"Battery\" : \"{5}\",\r\n    \"SignalStrength\" : \"{6}\",\r\n    \"State\" : \"{7}\",\r\n    \"SensorID\" : \"{8}\",\r\n    \"SensorName\" : \"{9}\",\r\n    \"AccountID\" : \"{10}\",\r\n    \"CSNetID\" : \"{11}\",\r\n    \"FirmwareVersion\" : \"{12}\",\r\n    \"CanUpdate\" : \"{13}\",\r\n    \"IsActive\" : \"{14}\",\r\n    \"ApplicationID\" : \"{16}\",\r\n    \"GatewayID\" : \"{17}\",\r\n    \"PlotValue\" : \"{18}\"\r\n";
  public static string DefaultGatewayConnectionInfo2 = "\r\n    \"MessageID\" : \"{1}\",\r\n    \"MessageType\" : \"{2}\",\r\n    \"Power\" : \"{3}\",\r\n    \"Battery\" : \"{4}\",\r\n    \"ReceivedDate\" : \"{5}\",\r\n    \"MessageCount\" : \"{6}\",\r\n    \"MeetsNotificationRequirement\" : \"{7}\",\r\n    \"GatewayID\" : \"{8}\",\r\n    \"GatewayName\" : \"{9}\",\r\n    \"InternalGatewayIDs\" : \"{10}\",\r\n    \"CSNetID\" : \"{11}\",\r\n    \"FirmwareVersion\" : \"{12}\",\r\n    \"GatewayFirmware\" : \"{13}\",\r\n    \"IsDirty\" : \"{14}\",\r\n    \"GatewayTypeID\" : \"{15}\",\r\n    \"CurrentSignalStrength\" : \"{16}\"\r\n        ";
  public static int killRetryLimit = 20;
  public static int killSendLimit = 100;
  public static TimeSpan TimeBetween = new TimeSpan(24, 0, 0);
  private bool isComplete = false;
  private static object lockobj = new object();

  [DBProp("ExternalDataSubscriptionID", IsPrimaryKey = true)]
  public long ExternalDataSubscriptionID
  {
    get => this._ExternalDataSubscriptionID;
    set => this._ExternalDataSubscriptionID = value;
  }

  [DBProp("AccountID")]
  [DBForeignKey("Account", "AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("ExternalID", AllowNull = false, MaxLength = 255 /*0xFF*/)]
  public string ExternalID
  {
    get => this._ExternalID;
    set
    {
      if (value == null)
        this._ExternalID = string.Empty;
      else
        this._ExternalID = value;
    }
  }

  [DBProp("ConnectionInfo1", AllowNull = true, MaxLength = 1000)]
  public string ConnectionInfo1
  {
    get => this._ConnectionInfo1;
    set
    {
      if (value == null)
        this._ConnectionInfo1 = string.Empty;
      else
        this._ConnectionInfo1 = value;
    }
  }

  [DBProp("ConnectionInfo2", AllowNull = true, MaxLength = 500)]
  public string ConnectionInfo2
  {
    get => this._ConnectionInfo2;
    set
    {
      if (value == null)
        this._ConnectionInfo2 = string.Empty;
      else
        this._ConnectionInfo2 = value;
    }
  }

  [DBProp("LastResult", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string LastResult
  {
    get => this._LastResult;
    set => this._LastResult = value;
  }

  [DBProp("DoSend", AllowNull = false, DefaultValue = true)]
  public bool DoSend
  {
    get => this._DoSend;
    set => this._DoSend = value;
  }

  [DBProp("DoRetry", AllowNull = false, DefaultValue = false)]
  public bool DoRetry
  {
    get => this._DoRetry;
    set => this._DoRetry = value;
  }

  [DBProp("BrokenCount")]
  public int BrokenCount
  {
    get => this._BrokenCount;
    set => this._BrokenCount = value;
  }

  [DBProp("ContentHeaderType")]
  public string ContentHeaderType
  {
    get => this._ContentHeaderType;
    set => this._ContentHeaderType = value;
  }

  [DBProp("verb", MaxLength = 255 /*0xFF*/, AllowNull = false, DefaultValue = "HttpGet")]
  public string verb
  {
    get => this._verb;
    set => this._verb = value;
  }

  [DBProp("LastSuccess")]
  public DateTime LastSuccess
  {
    get => this._LastSuccess;
    set => this._LastSuccess = value;
  }

  [DBProp("IsDeleted", AllowNull = false, DefaultValue = false)]
  public bool IsDeleted
  {
    get => this._IsDeleted;
    set => this._IsDeleted = value;
  }

  [DBProp("SendWithoutDataMessage", AllowNull = false, DefaultValue = true)]
  public bool SendWithoutDataMessage
  {
    get => this._SendWithoutDataMessage;
    set => this._SendWithoutDataMessage = value;
  }

  [DBProp("Password", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Password
  {
    get => this._Password;
    set
    {
      if (value == null)
        this._Password = string.Empty;
      else
        this._Password = value;
    }
  }

  [DBProp("Username", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Username
  {
    get => this._Username;
    set
    {
      if (value == null)
        this._Username = string.Empty;
      else
        this._Username = value;
    }
  }

  [DBProp("IgnoreSSLErrors")]
  public bool IgnoreSSLErrors
  {
    get => this._IgnoreSSLErrors;
    set => this._IgnoreSSLErrors = value;
  }

  [DBProp("SendRawData", AllowNull = false, DefaultValue = true)]
  public bool SendRawData
  {
    get => this._SendRawData;
    set => this._SendRawData = value;
  }

  [DBProp("eExternalDataSubscriptionType", AllowNull = true)]
  public eExternalDataSubscriptionType eExternalDataSubscriptionType
  {
    get => this._eExternalDataSubscriptionType;
    set => this._eExternalDataSubscriptionType = value;
  }

  [DBProp("eExternalDataSubscriptionClass", AllowNull = false, DefaultValue = 1)]
  public eExternalDataSubscriptionClass eExternalDataSubscriptionClass
  {
    get => this._eExternalDataSubscriptionClass;
    set => this._eExternalDataSubscriptionClass = value;
  }

  public List<ExternalDataSubscriptionProperty> Properties
  {
    get
    {
      if (this._Properties == null)
        this._Properties = ExternalDataSubscriptionProperty.LoadByExternalDataSubscriptionID(this.ExternalDataSubscriptionID);
      return this._Properties;
    }
  }

  public void AssignProperty(ExternalDataSubscriptionProperty property)
  {
    foreach (ExternalDataSubscriptionProperty property1 in this.Properties)
    {
      if (property1.Name == property.Name && property1.BinaryValue == property.BinaryValue && property1.StringValue == property.StringValue)
        return;
    }
    if (this.ExternalDataSubscriptionID == long.MinValue)
    {
      if (this._Properties == null)
        this._Properties = new List<ExternalDataSubscriptionProperty>();
      this._Properties.Add(property);
    }
    else
      this.Properties.Add(property);
  }

  public void RemoveProperty(ExternalDataSubscriptionProperty property)
  {
    property.Delete();
    for (int index = this.Properties.Count - 1; index >= 0; --index)
    {
      if (this.Properties[index].ExternalDataSubscriptionPropertyID == property.ExternalDataSubscriptionPropertyID)
        this.Properties.RemoveAt(index);
    }
  }

  public ExternalDataSubscriptionProperty SetPropertyValue(string propertyType, string value)
  {
    return this.SetPropertyValue(propertyType, (string) null, value, (string) null);
  }

  public ExternalDataSubscriptionProperty SetPropertyValue(
    string propertyType,
    string subType,
    string value)
  {
    return this.SetPropertyValue(propertyType, subType, subType, value);
  }

  public ExternalDataSubscriptionProperty SetPropertyValue(
    string propertyType,
    string subType,
    string value1,
    string value2)
  {
    ExternalDataSubscriptionProperty property = this.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m =>
    {
      if (!(m.Name == propertyType))
        return false;
      return string.IsNullOrEmpty(subType) || m.StringValue == subType;
    })).FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (property == null)
      property = new ExternalDataSubscriptionProperty()
      {
        Name = propertyType,
        eExternalDataSubscriptionType = this.eExternalDataSubscriptionType,
        ExternalDataSubscriptionID = this.ExternalDataSubscriptionID
      };
    property.StringValue = value1;
    property.StringValue2 = value2;
    property.Save();
    this.AssignProperty(property);
    return property;
  }

  private string addCurlyBrackets(string s) => $"{{{s}}}";

  public static ExternalDataSubscription byAccount(long accountid)
  {
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.LoadAllByAccountID(accountid).Where<ExternalDataSubscription>((Func<ExternalDataSubscription, bool>) (e => e.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook)).FirstOrDefault<ExternalDataSubscription>();
    if (dataSubscription == null)
    {
      dataSubscription = new ExternalDataSubscription();
      dataSubscription.AccountID = accountid;
      dataSubscription.ContentHeaderType = "application/json";
      dataSubscription.ConnectionInfo1 = "";
      dataSubscription.ConnectionInfo2 = ExternalDataSubscription.DefaultSensorConnectionInfo2;
      dataSubscription.LastResult = (string) null;
      dataSubscription.DoSend = true;
      dataSubscription.DoRetry = false;
      dataSubscription.BrokenCount = 0;
      dataSubscription.verb = "HttpPost";
      dataSubscription.LastSuccess = DateTime.MinValue;
    }
    return dataSubscription;
  }

  public void adjustBroken(ExternalDataSubscriptionAttempt attempt)
  {
    ++this.BrokenCount;
    if (this.BrokenCount > ExternalDataSubscription.killRetryLimit)
      this.DoRetry = false;
    try
    {
      ExternalSubscriptionPreference subscriptionPreference = ExternalSubscriptionPreference.LoadByAccountId(this.AccountID);
      if (subscriptionPreference == null)
      {
        try
        {
          subscriptionPreference = new ExternalSubscriptionPreference()
          {
            AccountID = this.AccountID,
            UserId = Account.Load(this.AccountID).PrimaryContactID,
            UsersBrokenCountLimit = ExternalDataSubscription.killRetryLimit
          };
          subscriptionPreference.Save();
        }
        catch (Exception ex)
        {
          ex.Log("ExternalDataSubscription.AdjustBroken Failed to save new ExternalDataPreference");
        }
      }
      if (this.BrokenCount > ExternalDataSubscription.killSendLimit && DateTime.UtcNow - this.LastSuccess > ExternalDataSubscription.TimeBetween)
      {
        this.DoSend = false;
        if (DateTime.UtcNow - subscriptionPreference.KillSendUserNotifiedDate > ExternalDataSubscription.TimeBetween)
        {
          subscriptionPreference.KillSendUserNotifiedDate = DateTime.UtcNow;
          subscriptionPreference.SendBrokenEmail(this);
          return;
        }
      }
      if (this.BrokenCount <= subscriptionPreference.UsersBrokenCountLimit || !(DateTime.UtcNow - subscriptionPreference.LastEmailDate > ExternalDataSubscription.TimeBetween))
        return;
      subscriptionPreference.SendCustomBrokenEmail(this);
    }
    catch (Exception ex)
    {
      ex.Log($"ExternalDataSubscription.AdjustBroken NotificationEmails, ExternalSubscriptionID: {this.ExternalDataSubscriptionID.ToString()} Stack trace: {ex.StackTrace}");
    }
  }

  public void initialResetBroken()
  {
    this.BrokenCount = 0;
    this.DoRetry = false;
    this.DoSend = true;
  }

  public void resetBroken()
  {
    this.LastSuccess = DateTime.UtcNow;
    this.BrokenCount = 0;
    this.DoRetry = true;
    this.DoSend = true;
    this.Save();
  }

  public ExternalDataSubscriptionAttempt Record(PacketCache packet)
  {
    if (string.IsNullOrEmpty(this.ConnectionInfo1) || !this.DoSend)
      return (ExternalDataSubscriptionAttempt) null;
    ExternalDataSubscriptionAttempt attempt = new ExternalDataSubscriptionAttempt()
    {
      Url = this.ConnectionInfo1,
      CreateDate = DateTime.UtcNow,
      ProcessDate = DateTime.MinValue,
      ExternalDataSubscriptionID = this.ExternalDataSubscriptionID,
      AttemptCount = 0,
      Status = ExternalDataSubscription.FirstExternalSubscriptionAttemptImmediate ? eExternalDataSubscriptionStatus.Processing : eExternalDataSubscriptionStatus.New,
      Verb = this.verb
    };
    try
    {
      switch (this.eExternalDataSubscriptionType)
      {
        case eExternalDataSubscriptionType.omf_pi:
          attempt.body = PiIntegrationHelper.GetData(packet);
          break;
        default:
          attempt.body = this.BuildWebhookBody(packet);
          break;
      }
    }
    catch (Exception ex)
    {
      if (ExternalDataSubscription.LogExternalSubscriptionAsException)
        ex.Log("ExternalDataSubscription.Record: str:" + attempt.Url);
      this.adjustBroken(attempt);
      attempt.body = ex.ToString();
      attempt.Status = eExternalDataSubscriptionStatus.Failed;
      attempt.DoRetry = false;
    }
    finally
    {
      attempt.Save();
      this.Save();
    }
    return attempt;
  }

  public ExternalDataSubscriptionAttempt BuildEDSAttempt(string HttpMessage)
  {
    if (string.IsNullOrEmpty(this.ConnectionInfo1) || !this.DoSend)
      return (ExternalDataSubscriptionAttempt) null;
    ExternalDataSubscriptionAttempt attempt = new ExternalDataSubscriptionAttempt()
    {
      Url = this.ConnectionInfo1,
      CreateDate = DateTime.UtcNow,
      ProcessDate = DateTime.MinValue,
      ExternalDataSubscriptionID = this.ExternalDataSubscriptionID,
      AttemptCount = 0,
      Status = ExternalDataSubscription.FirstExternalSubscriptionAttemptImmediate ? eExternalDataSubscriptionStatus.Processing : eExternalDataSubscriptionStatus.New,
      Verb = this.verb
    };
    try
    {
      attempt.body = HttpMessage;
    }
    catch (Exception ex)
    {
      if (ExternalDataSubscription.LogExternalSubscriptionAsException)
        ex.Log("ExternalDataSubscription.RecordNotification: str:" + attempt.Url);
      this.adjustBroken(attempt);
      attempt.body = ex.ToString();
      attempt.Status = eExternalDataSubscriptionStatus.Failed;
      attempt.DoRetry = false;
    }
    finally
    {
      attempt.Save();
      this.Save();
    }
    return attempt;
  }

  public void Send(ExternalDataSubscriptionAttempt attempt)
  {
    if (!this.DoSend)
    {
      attempt.DoRetry = false;
      attempt.Save();
    }
    else
    {
      if (attempt.AttemptCount == 0)
        ++attempt.AttemptCount;
      attempt.ProcessDate = DateTime.UtcNow;
      ExternalDataSubscriptionResponse response = new ExternalDataSubscriptionResponse()
      {
        DateTime = DateTime.UtcNow,
        ExternalDataSubscriptionAttemptID = attempt.ExternalDataSubscriptionAttemptID
      };
      try
      {
        string lower = this.verb.ToLower();
        if (lower.Contains("mqtt"))
        {
          if (lower.Contains("password"))
            ExternalDataSubscription.ProcessMQTT(this, attempt, response);
          else if (lower.Contains("certificate"))
            ExternalDataSubscription.ProcessMQTTCert(this, attempt, response);
          else
            ExternalDataSubscription.ProcessMQTT_Azure(this, attempt, response);
        }
        else
          this.ProcessWebHook(attempt, response, this.verb);
        if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.NonAuthoritativeInformation || response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.ResetContent)
        {
          attempt.Status = eExternalDataSubscriptionStatus.Success;
          this.resetBroken();
        }
        else
        {
          attempt.Status = eExternalDataSubscriptionStatus.Failed;
          this.adjustBroken(attempt);
        }
      }
      catch (Exception ex)
      {
        if (ExternalDataSubscription.LogExternalSubscriptionAsException)
          ex.Log("ExternalDataSubscription.Send: str:" + attempt.Url);
        this.adjustBroken(attempt);
        attempt.Status = eExternalDataSubscriptionStatus.Failed;
        response.ExceptionOccurred = true;
        response.ExceptionData = ex.ToString();
        response.DateTime = DateTime.UtcNow;
        response.Status = "Exception";
      }
      finally
      {
        if (!this.DoRetry)
          attempt.DoRetry = false;
        attempt.Save();
        response.Save();
        if (this.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.notification && attempt.NotificationRecordedID > 0L)
        {
          NotificationRecorded notificationRecorded = NotificationRecorded.Load(attempt.NotificationRecordedID);
          if (notificationRecorded != null)
          {
            notificationRecorded.Status = attempt.Status.ToString();
            notificationRecorded.Save();
          }
        }
        this.LastResult = response.Response;
        if (response.ExceptionOccurred)
          this.LastResult = !response.ExceptionData.Contains("(404) Not Found") ? (!response.ExceptionData.Contains("the connected party did not properly respond") ? (!response.ExceptionData.Contains("the target machine actively refused it") ? (!response.ExceptionData.Contains("remote name could not be resolved") ? (!response.ExceptionData.Contains("has timed out") ? "Unknown error occurred." : "The operation has timed out. Check Connection Information.") : "The remote name could not be resolved. Check Connection Information.") : "Server connection refused. Check server configuration.") : "Server timeout. Check Connection Information.") : "The remote server returned an error: (404) Not Found. Check Connection Information.";
        else if (attempt.Status == eExternalDataSubscriptionStatus.Failed)
          this.LastResult = "Failed. Check configuration.";
        if (this.LastResult.Length > 250)
          this.LastResult = this.LastResult.Substring(0, 250);
        this.Save();
      }
    }
  }

  private string getServerIP()
  {
    string serverIp = "?";
    foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
    {
      if (address.AddressFamily.ToString() == "InterNetwork")
        serverIp = address.ToString();
    }
    return serverIp;
  }

  private static void ProcessMQTT(
    ExternalDataSubscription sub,
    ExternalDataSubscriptionAttempt attempt,
    ExternalDataSubscriptionResponse response)
  {
    DateTime now1 = DateTime.Now;
    try
    {
      if (MqttHelper.Publish_Application_Message(sub, attempt, response).Wait(20000))
        return;
      response.StatusCode = HttpStatusCode.RequestTimeout;
      response.Status = "408";
      response.Response = "Request Timed Out";
    }
    catch (Exception ex)
    {
      Exception exception = ex;
      if (exception is AggregateException)
        exception = ((AggregateException) exception).InnerExceptions.FirstOrDefault<Exception>();
      response.ExceptionOccurred = true;
      response.ExceptionData = exception.ToString();
      response.Response = exception.Message;
      if (exception.Message == null)
      {
        response.StatusCode = HttpStatusCode.RequestTimeout;
        response.Status = "408";
      }
      else
      {
        response.StatusCode = HttpStatusCode.InternalServerError;
        response.Status = "500";
      }
    }
    finally
    {
      try
      {
        DateTime now2 = DateTime.Now;
        response.ResponseTime = (now2 - now1).TotalMilliseconds.ToInt();
      }
      catch
      {
      }
    }
  }

  private static void ProcessMQTTCert(
    ExternalDataSubscription sub,
    ExternalDataSubscriptionAttempt attempt,
    ExternalDataSubscriptionResponse response)
  {
    DateTime now1 = DateTime.Now;
    try
    {
      if (MqttHelper.Publish_Application_MessageWithCert(sub, attempt, response).Wait(20000))
        return;
      response.StatusCode = HttpStatusCode.RequestTimeout;
      response.Status = "408";
      response.Response = "Request Timed Out";
    }
    catch (Exception ex)
    {
      Exception exception = ex;
      if (exception is AggregateException)
        exception = ((AggregateException) exception).InnerExceptions.FirstOrDefault<Exception>();
      response.ExceptionOccurred = true;
      response.ExceptionData = exception.ToString();
      response.Response = exception.Message;
      if (exception.Message == null)
      {
        response.StatusCode = HttpStatusCode.RequestTimeout;
        response.Status = "408";
      }
      else
      {
        response.StatusCode = HttpStatusCode.InternalServerError;
        response.Status = "500";
      }
    }
    finally
    {
      try
      {
        DateTime now2 = DateTime.Now;
        response.ResponseTime = (now2 - now1).TotalMilliseconds.ToInt();
      }
      catch
      {
      }
    }
  }

  private static void ProcessMQTT_Azure(
    ExternalDataSubscription sub,
    ExternalDataSubscriptionAttempt attempt,
    ExternalDataSubscriptionResponse response)
  {
    DateTime now1 = DateTime.Now;
    try
    {
      if (MqttHelper.Publish_Azure_Application_Message(sub, attempt, response).Wait(20000))
        return;
      response.StatusCode = HttpStatusCode.RequestTimeout;
      response.Status = "408";
      response.Response = "Request Timed Out";
    }
    catch (Exception ex)
    {
      Exception exception = ex;
      if (exception is AggregateException)
        exception = ((AggregateException) exception).InnerExceptions.FirstOrDefault<Exception>();
      response.ExceptionOccurred = true;
      response.ExceptionData = exception.ToString();
      response.Response = exception.Message;
      if (exception.Message == null)
      {
        response.StatusCode = HttpStatusCode.RequestTimeout;
        response.Status = "408";
      }
      else
      {
        response.StatusCode = HttpStatusCode.InternalServerError;
        response.Status = "500";
      }
    }
    finally
    {
      try
      {
        DateTime now2 = DateTime.Now;
        response.ResponseTime = (now2 - now1).TotalMilliseconds.ToInt();
      }
      catch
      {
      }
    }
  }

  private void ProcessWebHook(
    ExternalDataSubscriptionAttempt attempt,
    ExternalDataSubscriptionResponse response,
    string method)
  {
    DateTime now1 = DateTime.Now;
    try
    {
      string[] strArray = this.getServerIP().Split('.');
      if (attempt.Url.Contains($"{strArray[0]}.{strArray[1]}"))
        throw new WebException("has timed out");
      try
      {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
      }
      catch (NotSupportedException ex)
      {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
      }
      HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(attempt.Url);
      webRequest.Timeout = 15000;
      if (this.IgnoreSSLErrors)
        webRequest.ServerCertificateValidationCallback += (RemoteCertificateValidationCallback) ((sender, certificate, chain, sslPolicyErrors) => true);
      if (method.ToLower().Contains("post"))
      {
        webRequest.Method = "POST";
        if (!string.IsNullOrEmpty(this.ContentHeaderType) && this.ContentHeaderType != "raw")
          webRequest.ContentType = this.ContentHeaderType;
        List<ExternalDataSubscriptionProperty> list1 = this.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "header")).ToList<ExternalDataSubscriptionProperty>();
        List<ExternalDataSubscriptionProperty> list2 = this.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "cookie")).ToList<ExternalDataSubscriptionProperty>();
        ExternalDataSubscriptionProperty subscriptionProperty1 = this.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "username")).FirstOrDefault<ExternalDataSubscriptionProperty>();
        ExternalDataSubscriptionProperty subscriptionProperty2 = this.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "password")).FirstOrDefault<ExternalDataSubscriptionProperty>();
        ExternalDataSubscriptionProperty subscriptionProperty3 = this.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "sasToken")).FirstOrDefault<ExternalDataSubscriptionProperty>();
        if (subscriptionProperty1 != null && subscriptionProperty2 != null)
        {
          string stringValue = subscriptionProperty1.StringValue;
          string encrypted = subscriptionProperty2.StringValue;
          try
          {
            if (MonnitUtil.UseEncryption())
              encrypted = encrypted.Decrypt();
          }
          catch
          {
          }
          string base64String = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes($"{stringValue}:{encrypted}"));
          webRequest.Headers.Add("Authorization", "Basic " + base64String);
        }
        if (subscriptionProperty3 != null)
        {
          string encrypted = subscriptionProperty3.StringValue;
          try
          {
            if (MonnitUtil.UseEncryption())
              encrypted = encrypted.Decrypt();
          }
          catch
          {
          }
          try
          {
            webRequest.Headers.Add("Authorization", encrypted);
          }
          catch
          {
          }
        }
        if (list1.Count > 0)
        {
          foreach (ExternalDataSubscriptionProperty subscriptionProperty4 in list1)
          {
            try
            {
              webRequest.Headers.Add(subscriptionProperty4.StringValue, subscriptionProperty4.StringValue2);
            }
            catch
            {
            }
          }
        }
        if (list2.Count > 0)
        {
          Uri uri = new Uri(attempt.Url);
          foreach (ExternalDataSubscriptionProperty subscriptionProperty5 in list2)
          {
            try
            {
              webRequest.TryAddCookie(new Cookie(subscriptionProperty5.StringValue, subscriptionProperty5.StringValue2)
              {
                Domain = uri.Host
              });
            }
            catch
            {
            }
          }
        }
        byte[] bytes = new ASCIIEncoding().GetBytes(attempt.body);
        webRequest.ContentLength = (long) bytes.Length;
        using (Stream requestStream = webRequest.GetRequestStream())
        {
          requestStream.Write(bytes, 0, bytes.Length);
          requestStream.Close();
        }
      }
      else if (method.ToLower().Contains("aws_version1"))
      {
        if (!string.IsNullOrEmpty(this.ContentHeaderType) && this.ContentHeaderType != "raw")
          webRequest.ContentType = this.ContentHeaderType;
        ExternalDataSubscriptionProperty subscriptionProperty6 = this.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "awsHost")).FirstOrDefault<ExternalDataSubscriptionProperty>();
        ExternalDataSubscriptionProperty subscriptionProperty7 = this.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "accessKey")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
        ExternalDataSubscriptionProperty subscriptionProperty8 = this.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "secretKey")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
        ExternalDataSubscriptionProperty subscriptionProperty9 = this.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "apiKey")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
        ExternalDataSubscriptionProperty subscriptionProperty10 = this.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "region")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
        ExternalDataSubscriptionProperty subscriptionProperty11 = this.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "service")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
        ExternalDataSubscriptionProperty subscriptionProperty12 = this.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "stageName")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
        ExternalDataSubscriptionProperty subscriptionProperty13 = this.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "resourceName")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
        ExternalDataSubscriptionProperty subscriptionProperty14 = this.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "parameterList")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
        ExternalDataSubscriptionProperty subscriptionProperty15 = this.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "callMethod")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
        if (subscriptionProperty6 != null && subscriptionProperty7 != null && subscriptionProperty8 != null && subscriptionProperty9 != null && subscriptionProperty10 != null && subscriptionProperty11 != null && subscriptionProperty12 != null && subscriptionProperty15 != null)
        {
          string str1 = subscriptionProperty7.StringValue;
          string str2 = subscriptionProperty8.StringValue;
          try
          {
            if (MonnitUtil.UseEncryption())
            {
              str1 = str1.Decrypt();
              str2 = str2.Decrypt();
            }
          }
          catch
          {
          }
          try
          {
            attempt.body = attempt.body.Replace("µ", "").Replace("μ", "");
            string requestPayload = ExternalDataSubscription.CreateRequestPayload(attempt.body);
            string str3 = ExternalDataSubscription.Sign(requestPayload, subscriptionProperty15.StringValue.ToUpper(), $"/{subscriptionProperty12.StringValue}/{subscriptionProperty13.StringValue}", subscriptionProperty14.StringValue, str1, str2, subscriptionProperty10.StringValue, subscriptionProperty11.StringValue, subscriptionProperty6.StringValue);
            string str4 = DateTime.UtcNow.ToString("yyyyMMddTHHmmss") + "Z";
            webRequest.Method = subscriptionProperty15.StringValue.ToUpper();
            webRequest.Headers.Add("X-Amz-date", str4);
            webRequest.Headers.Add("x-api-key", subscriptionProperty9.StringValue);
            webRequest.Headers.Add("Authorization", str3);
            webRequest.Headers.Add("x-amz-content-sha256", requestPayload);
            webRequest.ContentLength = (long) attempt.body.Length;
            webRequest.Host = subscriptionProperty6.StringValue;
          }
          catch
          {
          }
        }
        byte[] bytes = new ASCIIEncoding().GetBytes(attempt.body);
        webRequest.ContentLength = (long) bytes.Length;
        using (Stream requestStream = webRequest.GetRequestStream())
        {
          requestStream.Write(bytes, 0, bytes.Length);
          requestStream.Close();
        }
      }
      using (HttpWebResponse response1 = (HttpWebResponse) webRequest.GetResponse())
      {
        response.StatusCode = response1.StatusCode;
        response.Status = response1.StatusDescription;
        StringBuilder stringBuilder = new StringBuilder();
        foreach (string header in (NameObjectCollectionBase) response1.Headers)
          stringBuilder.AppendFormat("{0}={1}\r\n", (object) header, (object) response1.GetResponseHeader(header));
        response.Headers = stringBuilder.ToString();
        using (StreamReader streamReader = new StreamReader(response1.GetResponseStream()))
        {
          response.Response = streamReader.ReadToEnd();
          streamReader.Close();
        }
        response1.Close();
      }
    }
    catch (WebException ex)
    {
      response.ExceptionOccurred = true;
      response.ExceptionData = ex.ToString();
      if (ex.Response == null)
      {
        response.StatusCode = HttpStatusCode.RequestTimeout;
        response.Status = ex.Status.ToString();
      }
      else
      {
        response.StatusCode = ((HttpWebResponse) ex.Response).StatusCode;
        response.Status = ((HttpWebResponse) ex.Response).StatusDescription;
        try
        {
          using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
          {
            response.Response = streamReader.ReadToEnd();
            streamReader.Close();
          }
        }
        catch
        {
        }
      }
    }
    finally
    {
      DateTime now2 = DateTime.Now;
      response.ResponseTime = (now2 - now1).TotalMilliseconds.ToInt();
    }
  }

  private string BuildUrl(
    DataMessage msg,
    GatewayMessage gm,
    Sensor sens,
    Gateway gate,
    ExternalDataSubscriptionAttempt attempt,
    string formatStr)
  {
    if (msg != null && sens != null)
      return this.BuildURL(msg, sens, formatStr);
    if (gm != null && gate != null)
      return this.BuildURL(gm, gate, formatStr);
    object[] objArray = new object[4];
    Guid guid;
    string str1;
    if (msg != null)
    {
      guid = msg.DataMessageGUID;
      str1 = guid.ToString();
    }
    else
      str1 = "Null";
    objArray[0] = (object) str1;
    long num;
    string str2;
    if (sens != null)
    {
      num = sens.SensorID;
      str2 = num.ToString();
    }
    else
      str2 = "Null";
    objArray[1] = (object) str2;
    string str3;
    if (gm != null)
    {
      guid = gm.GatewayMessageGUID;
      str3 = guid.ToString();
    }
    else
      str3 = "Null";
    objArray[2] = (object) str3;
    string str4;
    if (gate != null)
    {
      num = gate.GatewayID;
      str4 = num.ToString();
    }
    else
      str4 = "Null";
    objArray[3] = (object) str4;
    throw new ArgumentNullException(string.Format("SensorMessage:{0}  Sensor:{1}  GatewayMessage:{2}  Gateway:{3}", objArray), new Exception());
  }

  private string BuildURL(GatewayMessage msg, Gateway gw, string formatStr)
  {
    formatStr = string.Format(formatStr, (object) this.ExternalID, (object) BitConverter.ToUInt64(msg.GatewayMessageGUID.ToByteArray(), 0), (object) msg.MessageType, (object) msg.Power, (object) msg.Battery, (object) msg.ReceivedDate, (object) msg.MessageCount, (object) msg.MeetsNotificationRequirement, (object) gw.GatewayID, (object) HttpUtility.UrlEncode(gw.Name), (object) gw.MacAddress, (object) gw.CSNetID, (object) gw.APNFirmwareVersion, (object) gw.GatewayFirmwareVersion, (object) gw.IsDirty, (object) gw.GatewayTypeID, (object) gw.CurrentSignalStrength, (object) msg.GatewayMessageGUID);
    return formatStr;
  }

  private string BuildURL(DataMessage msg, Sensor sens, string formatStr)
  {
    formatStr = string.Format(formatStr, (object) this.ExternalID, (object) BitConverter.ToUInt64(msg.DataMessageGUID.ToByteArray(), 0), (object) HttpUtility.UrlEncode(msg.Data), (object) HttpUtility.UrlEncode(msg.DisplayData), (object) msg.MessageDate, (object) msg.Battery, (object) DataMessage.GetSignalStrengthPercent(sens.GenerationType, sens.SensorTypeID, msg.SignalStrength), (object) msg.State, (object) sens.SensorID, (object) HttpUtility.UrlEncode(sens.SensorName), (object) sens.AccountID, (object) sens.CSNetID, (object) sens.FirmwareVersion, (object) sens.CanUpdate, (object) sens.IsActive, (object) sens.IsSleeping, (object) sens.ApplicationID, (object) msg.GatewayID, (object) msg.PlotValueString, (object) msg.DataMessageGUID);
    return formatStr;
  }

  public string BuildWebhookBody(PacketCache packetCache)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("{");
    stringBuilder.AppendFormat("\"gatewayMessage\":{{ \"gatewayID\":\"{0}\" , \"gatewayName\":\"{1}\" , \"accountID\":\"{10}\", \"networkID\":\"{2}\" , \"messageType\":\"{3}\" , \"power\":\"{4}\", \"batteryLevel\": \"{5}\" , \"date\": \"{6}\", \"count\":\"{7}\", \"signalStrength\": \"{8}\", \"pendingChange\": \"{9}\" }}", (object) packetCache.Gateway.GatewayID, (object) packetCache.Gateway.Name.EscapeJson(), (object) packetCache.Gateway.CSNetID, (object) packetCache.GatewayMessage.MessageType, (object) packetCache.GatewayMessage.Power, (object) packetCache.GatewayMessage.Battery, (object) packetCache.GatewayMessage.ReceivedDate.ToString("yyyy-MM-dd HH:mm:ss"), (object) packetCache.GatewayMessage.MessageCount, (object) packetCache.Gateway.CurrentSignalStrength, (object) packetCache.Gateway.IsDirty, (object) this.AccountID);
    if (packetCache.DataMessages.Count > 0)
    {
      stringBuilder.Append(",\"sensorMessages\":[");
      foreach (DataMessage dataMessage in packetCache.DataMessages)
      {
        Sensor sensor = packetCache.LoadSensor(dataMessage.SensorID);
        string str1 = string.Join<eDatumType>("|", dataMessage.AppBase.Datums.Select<AppDatum, eDatumType>((Func<AppDatum, eDatumType>) (dat => dat.data.datatype)));
        string str2 = string.Join<object>("|", dataMessage.AppBase.Datums.Select<AppDatum, object>((Func<AppDatum, object>) (dat => dat.data.compvalue)));
        string str3 = string.Join<object>("|", (IEnumerable<object>) dataMessage.AppBase.GetPlotValues(sensor.SensorID));
        string str4 = string.Join("|", (IEnumerable<string>) dataMessage.AppBase.GetPlotLabels(sensor.SensorID));
        string dirtyString = str4 == "" ? str1 : str4;
        stringBuilder.AppendFormat("{{ \"sensorID\":\"{0}\" , \"sensorName\":\"{1}\", \"applicationID\":\"{2}\", \"networkID\":\"{3}\", \"dataMessageGUID\":\"{4}\", \"state\": \"{5}\", \"messageDate\": \"{6}\", \"rawData\":\"{7}\", \"dataType\": \"{8}\", \"dataValue\": \"{9}\", \"plotValues\": \"{10}\", \"plotLabels\": \"{11}\", \"batteryLevel\": \"{12}\", \"signalStrength\": \"{13}\", \"pendingChange\": \"{14}\", \"voltage\": \"{15}\"}},", (object) sensor.SensorID, (object) sensor.SensorName.EscapeJson(), (object) sensor.ApplicationID, (object) sensor.CSNetID, (object) dataMessage.DataMessageGUID, (object) dataMessage.State, (object) dataMessage.MessageDate.ToString("yyyy-MM-dd HH:mm:ss"), this.SendRawData ? (object) HttpUtility.UrlEncode(dataMessage.Data) : (object) "", (object) str1, (object) str2, (object) str3, (object) dirtyString.EscapeJson(), (object) dataMessage.Battery, (object) DataMessage.GetSignalStrengthPercent(sensor.GenerationType, sensor.SensorTypeID, dataMessage.SignalStrength), (object) sensor.CanUpdate, (object) dataMessage.Voltage);
      }
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
      stringBuilder.Append("]");
    }
    if (packetCache.GatewayLocation != null)
    {
      GatewayLocation gatewayLocation = packetCache.GatewayLocation;
      stringBuilder.Append(",\"gatewayLocation\":");
      stringBuilder.AppendFormat("{{ \"latitude\":\"{0}\", \"longitude\":\"{1}\", \"accuracy\":\"{2}\"}}", (object) gatewayLocation.Latitude, (object) gatewayLocation.Longitude, (object) gatewayLocation.Accuracy);
    }
    if (packetCache.LocationMessages.Count > 0)
    {
      stringBuilder.Append(",\"locationMessages\":[");
      foreach (LocationMessage locationMessage in packetCache.LocationMessages)
        stringBuilder.AppendFormat("{{ \"deviceID\":\"{0}\" , \"locationMessageGUID\":\"{1}\", \"locationDate\":\"{2}\", \"state\":\"{3}\", \"latitude\":\"{4}\", \"longitude\": \"{5}\", \"altitude\": \"{6}\", \"speed\":\"{7}\", \"course\": \"{8}\", \"fixTime\": \"{9}\", \"sateliteCount\": \"{10}\", \"uncertainty\": \"{11}\"}},", (object) locationMessage.DeviceID, (object) locationMessage.LocationMessageGUID, (object) locationMessage.LocationDate, (object) locationMessage.State, (object) locationMessage.Latitude, (object) locationMessage.Longitude, (object) locationMessage.Altitude, (object) locationMessage.Speed, (object) locationMessage.CourseOverGround, (object) locationMessage.FixTime, (object) locationMessage.NumberSatellites, (object) locationMessage.Uncertainty);
      stringBuilder.Remove(stringBuilder.Length - 1, 1);
      stringBuilder.Append("]");
    }
    stringBuilder.Append("}");
    return stringBuilder.ToString();
  }

  public static bool LogExternalSubscriptionAsException
  {
    get => ConfigData.AppSettings(nameof (LogExternalSubscriptionAsException), "False").ToBool();
  }

  public static bool FirstExternalSubscriptionAttemptImmediate
  {
    get
    {
      return ConfigData.AppSettings(nameof (FirstExternalSubscriptionAttemptImmediate), "True").ToBool();
    }
  }

  private static string CreateRequestPayload(string jsonString)
  {
    return ExternalDataSubscription.HexEncode(ExternalDataSubscription.Hash(ExternalDataSubscription.ToBytes(jsonString)));
  }

  private static string Sign(
    string hashedRequestPayload,
    string requestMethod,
    string canonicalUri,
    string canonicalQueryString,
    string accessKey,
    string secretKey,
    string RegionName,
    string ServiceName,
    string Host)
  {
    DateTime utcNow = DateTime.UtcNow;
    string dateStamp = utcNow.ToString("yyyyMMdd");
    string str1 = utcNow.ToString("yyyyMMddTHHmmss") + "Z";
    string str2 = $"{dateStamp}/{RegionName}/{ServiceName}/aws4_request";
    string str3 = string.Join("\n", new SortedDictionary<string, string>()
    {
      {
        "content-type",
        "application/json"
      },
      {
        "host",
        Host
      },
      {
        "x-amz-date",
        str1
      }
    }.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (x => $"{x.Key.ToLowerInvariant()}:{x.Value.Trim()}"))) + "\n";
    string str4 = ExternalDataSubscription.HexEncode(ExternalDataSubscription.Hash(ExternalDataSubscription.ToBytes($"{requestMethod}\n{canonicalUri}\n{canonicalQueryString}\n{str3}\ncontent-type;host;x-amz-date\n{hashedRequestPayload}")));
    string str5 = ExternalDataSubscription.HexEncode(ExternalDataSubscription.HmacSha256($"AWS4-HMAC-SHA256\n{str1}\n{str2}\n{str4}", ExternalDataSubscription.GetSignatureKey(secretKey, dateStamp, RegionName, ServiceName)));
    return $"{"AWS4-HMAC-SHA256"} Credential={accessKey}/{dateStamp}/{RegionName}/{ServiceName}/aws4_request, SignedHeaders={"content-type;host;x-amz-date"}, Signature={str5}";
  }

  private static byte[] GetSignatureKey(
    string key,
    string dateStamp,
    string regionName,
    string serviceName)
  {
    byte[] key1 = ExternalDataSubscription.HmacSha256(dateStamp, ExternalDataSubscription.ToBytes("AWS4" + key));
    byte[] key2 = ExternalDataSubscription.HmacSha256(regionName, key1);
    return ExternalDataSubscription.HmacSha256("aws4_request", ExternalDataSubscription.HmacSha256(serviceName, key2));
  }

  private static byte[] ToBytes(string str) => Encoding.UTF8.GetBytes(str.ToCharArray());

  private static string HexEncode(byte[] bytes)
  {
    return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLowerInvariant();
  }

  private static byte[] Hash(byte[] bytes) => SHA256.Create().ComputeHash(bytes);

  private static byte[] HmacSha256(string data, byte[] key)
  {
    return new HMACSHA256(key).ComputeHash(ExternalDataSubscription.ToBytes(data));
  }

  public static ExternalDataSubscription Load(long externalDataSubscriptionID)
  {
    return BaseDBObject.Load<ExternalDataSubscription>(externalDataSubscriptionID);
  }

  public static List<ExternalDataSubscription> LoadBySensorID(long sensorID)
  {
    return BaseDBObject.LoadByForeignKey<ExternalDataSubscription>("SensorID", (object) sensorID);
  }

  public static List<ExternalDataSubscription> LoadByGatewayID(long gatewayID)
  {
    return BaseDBObject.LoadByForeignKey<ExternalDataSubscription>("GatewayID", (object) gatewayID);
  }

  public static List<ExternalDataSubscription> LoadAllByAccountID(long accountID)
  {
    return new Monnit.Data.ExternalDataSubscription.LoadByAccountID(accountID).Result;
  }

  public static Dictionary<long, Account> LoadAllAccountsWithExternalSubscriptions()
  {
    return new Monnit.Data.ExternalDataSubscription.LoadAllAccountsWithExternalSubscriptions().Result;
  }

  public static Dictionary<long, Account> AccountsWithExternalSubscriptionsByCSNet
  {
    get
    {
      string key = "AccountsWithExternalSubscriptions";
      Dictionary<long, Account> subscriptionsByCsNet = TimedCache.RetrieveObject<Dictionary<long, Account>>(key);
      if (subscriptionsByCsNet == null)
      {
        lock (ExternalDataSubscription.lockobj)
        {
          subscriptionsByCsNet = TimedCache.RetrieveObject<Dictionary<long, Account>>(key);
          if (subscriptionsByCsNet == null)
          {
            subscriptionsByCsNet = ExternalDataSubscription.LoadAllAccountsWithExternalSubscriptions();
            TimedCache.AddObjectToCach(key, (object) subscriptionsByCsNet, new TimeSpan(0, 15, 0));
          }
        }
      }
      return subscriptionsByCsNet;
    }
  }

  public override void Delete()
  {
    Monnit.Data.ExternalDataSubscription.Remove remove = new Monnit.Data.ExternalDataSubscription.Remove(this.ExternalDataSubscriptionID);
  }

  public static ExternalDataSubscription LoadByCSNetID(long csnetID)
  {
    return new Monnit.Data.ExternalDataSubscription.LoadByCSNetID(csnetID).Result;
  }

  public static void SetDeletedFlagByAccountID(long accountID)
  {
    Monnit.Data.ExternalDataSubscription.DeleteByAccountID deleteByAccountId = new Monnit.Data.ExternalDataSubscription.DeleteByAccountID(accountID);
  }
}
