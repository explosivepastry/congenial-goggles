// Decompiled with JetBrains decompiler
// Type: Monnit.MqttHelper
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Publishing;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Monnit;

public static class MqttHelper
{
  public static async Task Publish_Azure_Application_Message(
    ExternalDataSubscription sub,
    ExternalDataSubscriptionAttempt attempt,
    ExternalDataSubscriptionResponse response)
  {
    ExternalDataSubscriptionProperty hubProp = sub.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "iotHub")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty deviceProp = sub.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "deviceID")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty primaryKeyProp = sub.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "primaryKey")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty sasProp = sub.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "sasToken")).FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (hubProp == null || deviceProp == null || primaryKeyProp == null)
      throw new Exception("Mqtt Client Failed:  Parameters not found.");
    if (sasProp == null || sasProp.StringValue2.ToDateTime() < DateTime.Now)
    {
      if (sasProp == null)
      {
        sasProp = new ExternalDataSubscriptionProperty();
        sasProp.DisplayName = "Azure sas Token";
        sasProp.Name = "sasToken";
        sasProp.eExternalDataSubscriptionType = eExternalDataSubscriptionType.azuremqtt;
        sasProp.ExternalDataSubscriptionID = sub.ExternalDataSubscriptionID;
      }
      string target = $"{hubProp.StringValue}/devices/{deviceProp.StringValue}";
      sasProp.StringValue = MqttHelper.BuildSASSignature(primaryKeyProp.StringValue, target, new TimeSpan(7, 0, 0, 0));
      ExternalDataSubscriptionProperty subscriptionProperty = sasProp;
      DateTime dateTime = DateTime.Now;
      dateTime = dateTime.AddDays(7.0);
      string str = dateTime.ToString();
      subscriptionProperty.StringValue2 = str;
      sasProp.Save();
      target = (string) null;
    }
    MqttFactory mqttFactory = new MqttFactory();
    using (IMqttClient mqttClient = mqttFactory.CreateMqttClient())
    {
      IMqttClientOptions mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer(hubProp.StringValue, new int?(8883)).WithCleanSession().WithTls((Action<MqttClientOptionsBuilderTlsParameters>) (p => p.SslProtocol = SslProtocols.Tls12)).WithCommunicationTimeout(new TimeSpan(0, 0, 15)).WithClientId(deviceProp.StringValue).WithCredentials(sub.ConnectionInfo1, sasProp.StringValue).Build();
      MqttClientConnectResult connectResponse = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
      if (connectResponse.ResultCode != 0)
      {
        response.StatusCode = HttpStatusCode.NotFound;
        response.Response = "Mqtt Connect failed: " + connectResponse.ReasonString;
        throw new Exception(response.Response);
      }
      if (mqttClient.IsConnected)
      {
        attempt.body = attempt.body.Replace("µ", "").Replace("μ", "");
        byte[] message = new ASCIIEncoding().GetBytes(attempt.body);
        string topic = $"devices/{WebUtility.UrlEncode(deviceProp.StringValue)}/messages/events/";
        MqttApplicationMessage applicationMessage = new MqttApplicationMessageBuilder().WithAtLeastOnceQoS().WithTopic(topic + "$.ct=application%2Fjson%3Bcharset%3Dutf-8").WithPayload(message).WithContentType("application/json").Build();
        MqttClientPublishResult publishResult = await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
        if (publishResult.ReasonCode != 0)
        {
          response.StatusCode = HttpStatusCode.NotFound;
          response.Response = "Mqtt Publish failed: " + publishResult.ReasonString;
          throw new Exception(response.Response);
        }
        response.StatusCode = HttpStatusCode.OK;
        response.Response = "Success";
        message = (byte[]) null;
        topic = (string) null;
        applicationMessage = (MqttApplicationMessage) null;
        publishResult = (MqttClientPublishResult) null;
      }
      else
      {
        response.StatusCode = HttpStatusCode.NotFound;
        response.Response = "Mqtt Client not connected";
      }
      await mqttClient.DisconnectAsync(new MqttClientDisconnectOptions(), CancellationToken.None);
      mqttClientOptions = (IMqttClientOptions) null;
      connectResponse = (MqttClientConnectResult) null;
    }
    hubProp = (ExternalDataSubscriptionProperty) null;
    deviceProp = (ExternalDataSubscriptionProperty) null;
    primaryKeyProp = (ExternalDataSubscriptionProperty) null;
    sasProp = (ExternalDataSubscriptionProperty) null;
    mqttFactory = (MqttFactory) null;
  }

  public static async Task Publish_Application_Message(
    ExternalDataSubscription sub,
    ExternalDataSubscriptionAttempt attempt,
    ExternalDataSubscriptionResponse response)
  {
    ExternalDataSubscriptionProperty clientIDProp = sub.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "clientID")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty topicProp = sub.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "topic")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty useSSLProp = sub.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "useSSL")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (clientIDProp == null || topicProp == null || useSSLProp == null || string.IsNullOrEmpty(sub.ConnectionInfo1) || string.IsNullOrEmpty(sub.ConnectionInfo2))
      throw new Exception("Mqtt Client Failed:  Parameters not found.");
    MqttFactory mqttFactory = new MqttFactory();
    bool useSSL = useSSLProp.StringValue.ToBool();
    using (IMqttClient mqttClient = mqttFactory.CreateMqttClient())
    {
      MqttClientOptionsBuilder mqttClientOptions = new MqttClientOptionsBuilder();
      mqttClientOptions.WithTcpServer(sub.ConnectionInfo1, new int?(sub.ConnectionInfo2.ToInt()));
      mqttClientOptions.WithCleanSession();
      mqttClientOptions.WithCommunicationTimeout(new TimeSpan(0, 0, 15));
      mqttClientOptions.WithClientId(clientIDProp.StringValue);
      mqttClientOptions.WithCredentials(sub.Username, sub.Password.Decrypt());
      if (useSSL)
        mqttClientOptions.WithTls();
      MqttClientConnectResult connectResponse = await mqttClient.ConnectAsync(mqttClientOptions.Build(), CancellationToken.None);
      if (connectResponse.ResultCode != 0)
      {
        response.StatusCode = HttpStatusCode.NotFound;
        response.Response = "Mqtt Connect failed: " + connectResponse.ReasonString;
        throw new Exception(response.Response);
      }
      if (mqttClient.IsConnected)
      {
        attempt.body = attempt.body.Replace("µ", "").Replace("μ", "");
        byte[] message = new ASCIIEncoding().GetBytes(attempt.body);
        MqttApplicationMessage applicationMessage = new MqttApplicationMessageBuilder().WithAtLeastOnceQoS().WithTopic(topicProp.StringValue).WithPayload(message).WithContentType("application/json").Build();
        MqttClientPublishResult publishResult = await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
        if (publishResult.ReasonCode != 0)
        {
          response.StatusCode = HttpStatusCode.NotFound;
          response.Response = "Mqtt Publish failed: " + publishResult.ReasonString;
          throw new Exception(response.Response);
        }
        response.StatusCode = HttpStatusCode.OK;
        response.Response = "Success";
        message = (byte[]) null;
        applicationMessage = (MqttApplicationMessage) null;
        publishResult = (MqttClientPublishResult) null;
      }
      else
      {
        response.StatusCode = HttpStatusCode.NotFound;
        response.Response = "Mqtt Client not connected";
      }
      await mqttClient.DisconnectAsync(new MqttClientDisconnectOptions(), CancellationToken.None);
      mqttClientOptions = (MqttClientOptionsBuilder) null;
      connectResponse = (MqttClientConnectResult) null;
    }
    clientIDProp = (ExternalDataSubscriptionProperty) null;
    topicProp = (ExternalDataSubscriptionProperty) null;
    useSSLProp = (ExternalDataSubscriptionProperty) null;
    mqttFactory = (MqttFactory) null;
  }

  public static async Task Publish_Application_MessageWithCert(
    ExternalDataSubscription sub,
    ExternalDataSubscriptionAttempt attempt,
    ExternalDataSubscriptionResponse response)
  {
    ExternalDataSubscriptionProperty clientIDProp = sub.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "clientID")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty topicProp = sub.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "topic")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    ExternalDataSubscriptionProperty certProp = sub.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "certificate")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
    if (clientIDProp == null || topicProp == null || certProp == null || string.IsNullOrEmpty(sub.ConnectionInfo1) || string.IsNullOrEmpty(sub.ConnectionInfo2))
      throw new Exception("Mqtt Client Failed:  Parameters not found.");
    MqttFactory mqttFactory = new MqttFactory();
    List<X509Certificate> certs = new List<X509Certificate>()
    {
      (X509Certificate) new X509Certificate2(certProp.BinaryValue)
    };
    MqttClientOptionsBuilderTlsParameters tlsParams = new MqttClientOptionsBuilderTlsParameters();
    tlsParams.Certificates = (IEnumerable<X509Certificate>) certs;
    tlsParams.UseTls = true;
    using (IMqttClient mqttClient = mqttFactory.CreateMqttClient())
    {
      MqttClientOptionsBuilder mqttClientOptions = new MqttClientOptionsBuilder();
      mqttClientOptions.WithTcpServer(sub.ConnectionInfo1, new int?(sub.ConnectionInfo2.ToInt()));
      mqttClientOptions.WithCleanSession();
      mqttClientOptions.WithCommunicationTimeout(new TimeSpan(0, 0, 15));
      mqttClientOptions.WithClientId(clientIDProp.StringValue);
      mqttClientOptions.WithTls(tlsParams);
      MqttClientConnectResult connectResponse = await mqttClient.ConnectAsync(mqttClientOptions.Build(), CancellationToken.None);
      if (connectResponse.ResultCode != 0)
      {
        response.StatusCode = HttpStatusCode.NotFound;
        response.Response = "Mqtt Connect failed: " + connectResponse.ReasonString;
        throw new Exception(response.Response);
      }
      if (mqttClient.IsConnected)
      {
        attempt.body = attempt.body.Replace("µ", "").Replace("μ", "");
        byte[] message = new ASCIIEncoding().GetBytes(attempt.body);
        MqttApplicationMessage applicationMessage = new MqttApplicationMessageBuilder().WithAtLeastOnceQoS().WithTopic(topicProp.StringValue).WithPayload(message).WithContentType("application/json").Build();
        MqttClientPublishResult publishResult = await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
        if (publishResult.ReasonCode != 0)
        {
          response.StatusCode = HttpStatusCode.NotFound;
          response.Response = "Mqtt Publish failed: " + publishResult.ReasonString;
          throw new Exception(response.Response);
        }
        response.StatusCode = HttpStatusCode.OK;
        response.Response = "Success";
        message = (byte[]) null;
        applicationMessage = (MqttApplicationMessage) null;
        publishResult = (MqttClientPublishResult) null;
      }
      else
      {
        response.StatusCode = HttpStatusCode.NotFound;
        response.Response = "Mqtt Client not connected";
      }
      await mqttClient.DisconnectAsync(new MqttClientDisconnectOptions(), CancellationToken.None);
      mqttClientOptions = (MqttClientOptionsBuilder) null;
      connectResponse = (MqttClientConnectResult) null;
    }
    clientIDProp = (ExternalDataSubscriptionProperty) null;
    topicProp = (ExternalDataSubscriptionProperty) null;
    certProp = (ExternalDataSubscriptionProperty) null;
    mqttFactory = (MqttFactory) null;
    certs = (List<X509Certificate>) null;
    tlsParams = (MqttClientOptionsBuilderTlsParameters) null;
  }

  private static string BuildSASSignature(string key, string target, TimeSpan timeToLive)
  {
    string str1 = MqttHelper.BuildExpiresOn(timeToLive);
    string str2 = WebUtility.UrlEncode(target);
    string str3 = MqttHelper.Sign(string.Join("\n", (IEnumerable<string>) new List<string>()
    {
      str2,
      str1
    }), key);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, "SharedAccessSignature sr={0}&sig={1}&se={2}", (object) str2, (object) WebUtility.UrlEncode(str3), (object) WebUtility.UrlEncode(str1));
    return stringBuilder.ToString();
  }

  public static string BuildExpiresOn(TimeSpan timeToLive)
  {
    return Convert.ToString(Convert.ToInt64((object) DateTime.UtcNow.Add(timeToLive).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds, (IFormatProvider) CultureInfo.InvariantCulture), (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string Sign(string requestString, string key)
  {
    using (HMACSHA256 hmacshA256 = new HMACSHA256(Convert.FromBase64String(key)))
      return Convert.ToBase64String(hmacshA256.ComputeHash(Encoding.UTF8.GetBytes(requestString)));
  }
}
