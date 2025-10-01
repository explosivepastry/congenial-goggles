// Decompiled with JetBrains decompiler
// Type: Monnit.PiIntegrationHelper
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
using System.Text;

#nullable disable
namespace Monnit;

public static class PiIntegrationHelper
{
  public static bool SendCall(
    ExternalDataSubscription sub,
    string json,
    string messageType,
    string action,
    out ExternalDataSubscriptionResponse response)
  {
    bool flag = false;
    string[] strArray = PiIntegrationHelper.getServerIP().Split('.');
    if (sub.ConnectionInfo1.Contains($"{strArray[0]}.{strArray[1]}"))
      throw new WebException("has timed out");
    HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(sub.ConnectionInfo1);
    httpWebRequest.Timeout = 15000;
    httpWebRequest.Method = "POST";
    List<ExternalDataSubscriptionProperty> source = ExternalDataSubscriptionProperty.LoadByExternalDataSubscriptionID(sub.ExternalDataSubscriptionID);
    string str = "";
    string encrypted = "";
    if (source.Count > 0)
    {
      ExternalDataSubscriptionProperty subscriptionProperty1 = source.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "username")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
      if (subscriptionProperty1 != null)
        str = subscriptionProperty1.StringValue;
      ExternalDataSubscriptionProperty subscriptionProperty2 = source.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "password")).ToList<ExternalDataSubscriptionProperty>().FirstOrDefault<ExternalDataSubscriptionProperty>();
      if (subscriptionProperty2 != null)
        encrypted = subscriptionProperty2.StringValue;
    }
    if (MonnitUtil.UseEncryption())
    {
      try
      {
        encrypted = encrypted.Decrypt();
      }
      catch
      {
      }
    }
    string base64String = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes($"{str}:{encrypted}"));
    httpWebRequest.Headers.Add("Authorization", "Basic " + base64String);
    httpWebRequest.Headers.Add("x-requested-with", "xmlhttprequest");
    httpWebRequest.Headers.Add("messagetype", messageType);
    httpWebRequest.Headers.Add(nameof (action), action);
    httpWebRequest.Headers.Add("messageformat", "JSON");
    httpWebRequest.Headers.Add("omfversion", "1.1");
    byte[] bytes = new ASCIIEncoding().GetBytes(json);
    httpWebRequest.ContentLength = (long) bytes.Length;
    response = new ExternalDataSubscriptionResponse();
    DateTime now1 = DateTime.Now;
    try
    {
      ExternalDataSubscriptionProperty subscriptionProperty = sub.Properties.Where<ExternalDataSubscriptionProperty>((Func<ExternalDataSubscriptionProperty, bool>) (m => m.Name == "username")).FirstOrDefault<ExternalDataSubscriptionProperty>();
      if (subscriptionProperty != null && !subscriptionProperty.StringValue.ToBool())
        httpWebRequest.ServerCertificateValidationCallback += (RemoteCertificateValidationCallback) ((sender, certificate, chain, sslPolicyErrors) => true);
      using (Stream requestStream = httpWebRequest.GetRequestStream())
      {
        requestStream.Write(bytes, 0, bytes.Length);
        requestStream.Close();
      }
      using (HttpWebResponse response1 = (HttpWebResponse) httpWebRequest.GetResponse())
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
        if (response.StatusCode.ToInt() >= 200 && response.StatusCode.ToInt() < 300)
          flag = true;
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
        using (StreamReader streamReader = new StreamReader(ex.Response.GetResponseStream()))
        {
          response.Response = streamReader.ReadToEnd();
          streamReader.Close();
        }
      }
    }
    finally
    {
      DateTime now2 = DateTime.Now;
      response.ResponseTime = (now2 - now1).TotalMilliseconds.ToInt();
    }
    return flag;
  }

  private static string getServerIP()
  {
    string serverIp = "?";
    foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
    {
      if (address.AddressFamily.ToString() == "InterNetwork")
        serverIp = address.ToString();
    }
    return serverIp;
  }

  public static string GetType(long applicationID)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("[{");
    stringBuilder.AppendFormat("\"id\": \"SensorApplication_{0}_Static\", \"type\": \"object\", \"classification\": \"static\",", (object) applicationID);
    stringBuilder.Append("\"properties\": {");
    stringBuilder.Append("    \"SensorId\": { \"type\": \"string\", \"isindex\": true, \"isname\": true },");
    stringBuilder.Append("    \"SensorName\": { \"type\": \"string\"},");
    stringBuilder.Append("    \"Firmware\": { \"type\": \"string\"},");
    stringBuilder.Append("    \"SKU\": { \"type\": \"string\"}");
    stringBuilder.Append("}");
    stringBuilder.Append("},{");
    stringBuilder.AppendFormat("\"id\": \"SensorApplication_{0}_Dynamic\", \"classification\": \"dynamic\",", (object) applicationID);
    stringBuilder.Append("\"properties\": {");
    stringBuilder.Append("    \"Time\": { \"format\": \"date-time\", \"type\": \"string\", \"isindex\": \"True\" },");
    int num = 0;
    foreach (AppDatum appDatum in MonnitApplicationBase.GetAppDatums(applicationID))
    {
      switch (appDatum.etype)
      {
        case eDatumType.BooleanData:
        case eDatumType.DoorData:
        case eDatumType.WaterDetect:
        case eDatumType.VoltageDetect:
        case eDatumType.ActivityDetect:
        case eDatumType.ControlDetect:
        case eDatumType.ButtonPressed:
        case eDatumType.DryContact:
        case eDatumType.LightDetect:
        case eDatumType.MagnetDetect:
        case eDatumType.MotionDetect:
        case eDatumType.SeatOccupied:
        case eDatumType.AccelerometerImpact:
        case eDatumType.AirflowDetect:
        case eDatumType.VehicleDetect:
        case eDatumType.CarDetected:
          stringBuilder.AppendFormat("    \"{0}_{1}\": {{ \"type\":\"boolean\", \"description\": \"{0}\" }},", (object) num, (object) appDatum.type);
          break;
        case eDatumType.Percentage:
        case eDatumType.TemperatureData:
        case eDatumType.Voltage:
        case eDatumType.ResistanceData:
        case eDatumType.Pressure:
        case eDatumType.Angle:
        case eDatumType.MilliAmps:
        case eDatumType.Distance:
        case eDatumType.Geforce:
        case eDatumType.LuxData:
        case eDatumType.Magnitude:
        case eDatumType.PPM:
        case eDatumType.Data:
        case eDatumType.Weight:
        case eDatumType.Speed:
        case eDatumType.WattHours:
        case eDatumType.Frequency:
        case eDatumType.Amps:
        case eDatumType.AmpHours:
        case eDatumType.UnscaledVoltage:
        case eDatumType.Voltage0To5:
        case eDatumType.Voltage0To10:
        case eDatumType.Voltage0To500:
        case eDatumType.Voltage0To1Point2:
        case eDatumType.Millimeter:
        case eDatumType.Micrograms:
        case eDatumType.Pascal:
        case eDatumType.CrestFactor:
        case eDatumType.Centimeter:
        case eDatumType.Voltage0To200:
        case eDatumType.Acceleration:
        case eDatumType.MoistureWeight:
        case eDatumType.DifferentialPressureData:
        case eDatumType.MoistureTension:
        case eDatumType.AirSpeedData:
        case eDatumType.PPFDData:
        case eDatumType.PAR_DLI:
        case eDatumType.Decible:
        case eDatumType.DeviceID:
        case eDatumType.Decibel:
          stringBuilder.AppendFormat("    \"{0}_{1}\": {{ \"type\": \"number\", \"format\":\"float32\", \"description\": \"{1}\"}},", (object) num, (object) appDatum.type, (object) "");
          break;
        case eDatumType.Count:
          stringBuilder.AppendFormat("    \"{0}_{1}\": {{ \"type\": \"integer\", \"format\":\"int32\", \"description\": \"{1}\"}},", (object) num, (object) appDatum.type, (object) "");
          break;
      }
      ++num;
    }
    stringBuilder.Append("    \"Battery\": { \"type\": \"integer\", \"format\":\"int32\", \"description\": \"Percentage Sensor Battery\", \"uom\":\"%\"},");
    stringBuilder.Append("    \"SignalStrength\": { \"type\": \"integer\", \"format\":\"int32\", \"description\": \"Percentage Sensor Signal Strength\", \"uom\":\"%\"}");
    stringBuilder.Append("}");
    stringBuilder.Append("}]");
    return stringBuilder.ToString();
  }

  public static string GetContainer(Sensor sensor)
  {
    return $"[{{\"id\": \"{sensor.SensorName}\", \"typeid\": \"SensorApplication_{sensor.ApplicationID}_Dynamic\", \"typeVersion\": \"1.0.0.0\"}}]";
  }

  public static string GetData(PacketCache packet)
  {
    Dictionary<long, List<DataMessage>> dictionary = new Dictionary<long, List<DataMessage>>();
    foreach (DataMessage dataMessage in packet.DataMessages)
    {
      if (!dictionary.ContainsKey(dataMessage.SensorID))
        dictionary.Add(dataMessage.SensorID, new List<DataMessage>());
      dictionary[dataMessage.SensorID].Add(dataMessage);
    }
    if (dictionary.Count == 0)
      return "";
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("[");
    foreach (long key in dictionary.Keys)
    {
      Sensor sensor = packet.LoadSensor(key);
      if (sensor != null)
      {
        CSNet csNet = packet.LoadNetwork(packet.Gateway.CSNetID);
        string name = csNet == null ? "" : csNet.Name;
        stringBuilder.Append("{");
        stringBuilder.AppendFormat("\"typeid\": \"SensorApplication_{0}_Static\",", (object) sensor.ApplicationID);
        stringBuilder.Append("\"values\": [");
        stringBuilder.Append("{");
        stringBuilder.AppendFormat("    \"SensorId\": \"{0}\", ", (object) sensor.SensorID);
        stringBuilder.AppendFormat("    \"SensorName\": \"{0}\", ", (object) sensor.SensorName.Trim());
        stringBuilder.AppendFormat("    \"Firmware\": \"{0}\", ", (object) sensor.FirmwareVersion);
        stringBuilder.AppendFormat("    \"SKU\": \"{0}\", ", (object) sensor.SKU);
        stringBuilder.AppendFormat("    \"GatewayID\": \"{0}\", ", (object) packet.Gateway.GatewayID);
        stringBuilder.AppendFormat("    \"NetworkName\": \"{0}\" ", (object) name.Trim());
        stringBuilder.Append("}");
        stringBuilder.Append("]");
        stringBuilder.Append("},{");
        stringBuilder.Append("\"typeid\": \"__Link\",");
        stringBuilder.Append("\"values\": [{");
        stringBuilder.Append("    \"source\": { ");
        stringBuilder.AppendFormat("        \"typeid\": \"SensorApplication_{0}_Static\",", (object) sensor.ApplicationID);
        stringBuilder.AppendFormat("        \"index\": \"{0}\"", (object) sensor.SensorID);
        stringBuilder.Append("    }, ");
        stringBuilder.Append("    \"target\": { ");
        stringBuilder.AppendFormat("\"containerid\": \"{0}\"", (object) sensor.SensorName.Trim());
        stringBuilder.Append("    } ");
        stringBuilder.Append("}]");
        stringBuilder.Append("},{");
        stringBuilder.AppendFormat("\"containerid\": \"{0}\", ", (object) sensor.SensorName.Trim());
        stringBuilder.Append("\"values\": [");
        bool flag = true;
        foreach (DataMessage dataMessage in dictionary[key])
        {
          MonnitApplicationBase monnitApplicationBase = MonnitApplicationBase.LoadMonnitApplication("2", dataMessage.Data, dataMessage.ApplicationID, key);
          if (!flag)
            stringBuilder.Append(",");
          flag = false;
          stringBuilder.Append("{");
          stringBuilder.AppendFormat("\"Time\": \"{0}\",", (object) dataMessage.MessageDate.ToString("yyyy-MM-ddTHH:mm:ssZ"));
          int num = 0;
          foreach (AppDatum datum in monnitApplicationBase.Datums)
          {
            switch (datum.data.datatype)
            {
              case eDatumType.BooleanData:
              case eDatumType.DoorData:
              case eDatumType.WaterDetect:
              case eDatumType.VoltageDetect:
              case eDatumType.ActivityDetect:
              case eDatumType.ControlDetect:
              case eDatumType.ButtonPressed:
              case eDatumType.DryContact:
              case eDatumType.LightDetect:
              case eDatumType.MagnetDetect:
              case eDatumType.MotionDetect:
              case eDatumType.SeatOccupied:
              case eDatumType.AccelerometerImpact:
              case eDatumType.AirflowDetect:
              case eDatumType.VehicleDetect:
              case eDatumType.CarDetected:
                stringBuilder.AppendFormat("\"{0}_{1}\": {2},", (object) num, (object) datum.type, (object) datum.data.compvalue.ToStringSafe().ToLower());
                break;
              case eDatumType.Percentage:
              case eDatumType.TemperatureData:
              case eDatumType.Voltage:
              case eDatumType.ResistanceData:
              case eDatumType.Pressure:
              case eDatumType.Angle:
              case eDatumType.MilliAmps:
              case eDatumType.Coordinate:
              case eDatumType.Distance:
              case eDatumType.Count:
              case eDatumType.Geforce:
              case eDatumType.LuxData:
              case eDatumType.Magnitude:
              case eDatumType.PPM:
              case eDatumType.Weight:
              case eDatumType.Speed:
              case eDatumType.WattHours:
              case eDatumType.Frequency:
              case eDatumType.Amps:
              case eDatumType.AmpHours:
              case eDatumType.UnscaledVoltage:
              case eDatumType.Voltage0To5:
              case eDatumType.Voltage0To10:
              case eDatumType.Voltage0To500:
              case eDatumType.Voltage0To1Point2:
              case eDatumType.Millimeter:
              case eDatumType.Micrograms:
              case eDatumType.Pascal:
              case eDatumType.CrestFactor:
              case eDatumType.Centimeter:
              case eDatumType.Voltage0To200:
              case eDatumType.Acceleration:
              case eDatumType.PPB:
              case eDatumType.MoistureWeight:
              case eDatumType.DifferentialPressureData:
              case eDatumType.MoistureTension:
              case eDatumType.AirSpeedData:
              case eDatumType.PPFDData:
              case eDatumType.PAR_DLI:
              case eDatumType.Decible:
              case eDatumType.DeviceID:
              case eDatumType.Decibel:
                stringBuilder.AppendFormat("\"{0}_{1}\": {2},", (object) num, (object) datum.type, datum.data.compvalue);
                break;
              case eDatumType.Time:
              case eDatumType.Error:
              case eDatumType.State:
              case eDatumType.Status:
              case eDatumType.ProbeStatus:
                stringBuilder.AppendFormat("\"{0}_{1}\": \"{2}\",", (object) num, (object) datum.type, datum.data.compvalue);
                break;
              case eDatumType.Data:
                stringBuilder.AppendFormat("\"{0}_{1}\": \"{2}\",", (object) num, (object) datum.type, datum.data.compvalue);
                break;
              default:
                stringBuilder.AppendFormat("\"{0}_{1}\": \"{2}\",", (object) num, (object) datum.type, datum.data.compvalue);
                break;
            }
            ++num;
          }
          stringBuilder.AppendFormat("\"Battery\":{0},", (object) dataMessage.Battery);
          stringBuilder.AppendFormat("\"SignalStrength\":{0}", (object) dataMessage.SignalStrength);
          stringBuilder.Append("}");
        }
        stringBuilder.Append("]");
        stringBuilder.Append("},");
      }
    }
    --stringBuilder.Length;
    stringBuilder.Append("]");
    return stringBuilder.ToString();
  }
}
