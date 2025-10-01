// Decompiled with JetBrains decompiler
// Type: Monnit.DataMessage
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

#nullable disable
namespace Monnit;

[DBClass("DataMessage")]
public class DataMessage : BaseDBObject
{
  private Guid _DataMessageGUID = Guid.Empty;
  private long _SensorID = long.MinValue;
  private DateTime _MessageDate = DateTime.MinValue;
  private DateTime _InsertDate = DateTime.UtcNow;
  private int _State = int.MinValue;
  private int _SignalStrength = int.MinValue;
  private int _LinkQuality = int.MinValue;
  private int _Battery = int.MinValue;
  private string _Data = string.Empty;
  private double _Voltage = double.MinValue;
  private bool _MeetsNotificationRequirement;
  private long _GatewayID = long.MinValue;
  private bool _HasNote = false;
  private bool _IsAuthenticated = false;
  private long _ApplicationID = long.MinValue;
  private long _MonnitApplicationID = long.MinValue;
  private MonnitApplicationBase _AppBase;

  [DBProp("MessageDate", AllowNull = false)]
  public DateTime MessageDate
  {
    get => this._MessageDate;
    set => this._MessageDate = value;
  }

  [DBProp("SensorID", AllowNull = false)]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("DataMessageGUID", IsGuidPrimaryKey = true)]
  public Guid DataMessageGUID
  {
    get => this._DataMessageGUID;
    set => this._DataMessageGUID = value;
  }

  [DBProp("State")]
  public int State
  {
    get => this._State;
    set => this._State = value;
  }

  [DBProp("InsertDate")]
  public DateTime InsertDate
  {
    get => this._InsertDate;
    set => this._InsertDate = value;
  }

  [DBProp("SignalStrength")]
  public int SignalStrength
  {
    get => this._SignalStrength == 128 /*0x80*/ ? this._LinkQuality : this._SignalStrength;
    set => this._SignalStrength = value;
  }

  [DBProp("LinkQuality")]
  public int LinkQuality
  {
    get => this._LinkQuality;
    set => this._LinkQuality = value;
  }

  public long PowerSourceID { get; set; }

  [DBProp("Battery")]
  public int Battery
  {
    get
    {
      if (this.Voltage > double.MinValue && this._Battery < 0)
      {
        try
        {
          if (this.Voltage == 0.0)
          {
            this._Battery = 100;
          }
          else
          {
            if (this.PowerSourceID <= 0L)
            {
              Sensor sensor = Sensor.Load(this.SensorID);
              this.PowerSourceID = sensor != null ? sensor.PowerSourceID : 1L;
            }
            this._Battery = PowerSource.Load(this.PowerSourceID).Percent(this.Voltage).ToInt();
          }
        }
        catch (Exception ex)
        {
          if (ConfigData.AppSettings("LogNullPowerSourceExceptions", "False").ToBool())
            ex.Log("DataMessage.Battery_get: SensorID" + this.SensorID.ToString());
          this._Battery = 0;
        }
      }
      return this._Battery;
    }
    set => this._Battery = value;
  }

  [DBProp("Data", MaxLength = 500)]
  public string Data
  {
    get => this._Data;
    set
    {
      if (value == null)
        this._Data = string.Empty;
      else
        this._Data = value;
    }
  }

  [DBProp("Voltage")]
  public double Voltage
  {
    get => this._Voltage;
    set => this._Voltage = value;
  }

  [DBProp("MeetsNotificationRequirement")]
  public bool MeetsNotificationRequirement
  {
    get => this._MeetsNotificationRequirement;
    set => this._MeetsNotificationRequirement = value;
  }

  [DBProp("GatewayID")]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("HasNote", AllowNull = false)]
  public bool HasNote
  {
    get => this._HasNote;
    set => this._HasNote = value;
  }

  [DBProp("IsAuthenticated", AllowNull = true)]
  public bool IsAuthenticated
  {
    get => this._IsAuthenticated;
    set => this._IsAuthenticated = value;
  }

  public static DataMessage Load(Guid ID) => DataMessage.LoadByGuid(ID);

  public static DataMessage LoadByGuid(Guid ID)
  {
    return Monnit.Cassandra.DataMessage.LoadByGuid(ID) ?? BaseDBObject.Load<DataMessage>(ID);
  }

  public static DataMessage LoadLastBySensor(Sensor sensor)
  {
    if (((IEnumerable<StackFrame>) new StackTrace().GetFrames()).Select<StackFrame, string>((System.Func<StackFrame, string>) (f => f.GetMethod().Name)).Where<string>((Func<string, int, bool>) ((n, i) => i > 0 && n.ToLower() == "loadlastbysensor")).Count<string>() > 0)
      throw new StackOverflowException("Recursive call to DataMessage.LoadLastBySensor");
    if (sensor == null || sensor.LastDataMessageGUID == Guid.Empty || sensor.LastCommunicationDate == new DateTime(2099, 1, 1))
      return new DataMessage();
    DataMessage result1 = new Monnit.Data.DataMessage.LoadLastBySensor(sensor.SensorID).Result;
    if (result1 != null && result1.DataMessageGUID != Guid.Empty)
      return result1;
    DataMessage dm = Monnit.Cassandra.DataMessage.LoadLastBySensor(sensor);
    if (dm == null || dm.DataMessageGUID == Guid.Empty)
    {
      dm = DataMessage.LoadByGuid(sensor.LastDataMessageGUID);
      if (dm != null && dm.SensorID != sensor.SensorID)
        dm = (DataMessage) null;
    }
    if (dm == null || dm.DataMessageGUID == Guid.Empty)
      dm = DataMessage.LoadLastBefore(sensor.SensorID, sensor.LastCommunicationDate.AddDays(1.0));
    if (dm == null || dm.DataMessageGUID == Guid.Empty)
    {
      if (dm == null)
        dm = new DataMessage();
    }
    else
    {
      bool result2 = new Monnit.Data.DataMessage.UpdateLast(dm).Result;
    }
    return dm;
  }

  private static DataMessage LoadLastBySensorQuick(Sensor sensor)
  {
    if (((IEnumerable<StackFrame>) new StackTrace().GetFrames()).Select<StackFrame, string>((System.Func<StackFrame, string>) (f => f.GetMethod().Name)).Where<string>((Func<string, int, bool>) ((n, i) => i > 0 && n.ToLower() == "loadlastbysensorquick")).Count<string>() > 0)
      throw new StackOverflowException("Recursive call to DataMessage.LoadLastBySensorQuick");
    if (sensor == null || sensor.LastDataMessageGUID == Guid.Empty || sensor.LastCommunicationDate == new DateTime(2099, 1, 1))
      return new DataMessage();
    DataMessage result1 = new Monnit.Data.DataMessage.QuickLoad(sensor.SensorID, sensor.LastDataMessageGUID, sensor.LastCommunicationDate).Result;
    if (result1 != null && result1.DataMessageGUID != Guid.Empty)
      return result1;
    DataMessage dm = Monnit.Cassandra.DataMessage.QuickLoad(sensor.SensorID, sensor.LastDataMessageGUID, sensor.LastCommunicationDate);
    if (dm != null && dm.DataMessageGUID != Guid.Empty)
      dm = new Monnit.Data.DataMessage.LoadLastBySensor(sensor.SensorID).Result;
    if (dm != null && dm.DataMessageGUID != Guid.Empty)
    {
      bool result2 = new Monnit.Data.DataMessage.UpdateLast(dm).Result;
    }
    else if (dm == null)
      dm = new DataMessage();
    return dm;
  }

  public override void Save()
  {
    if (this.DataMessageGUID != Guid.Empty)
    {
      Monnit.Data.DataMessage.Update update = new Monnit.Data.DataMessage.Update(this);
      Monnit.Cassandra.DataMessage.UpdateDataMessage(this);
    }
    else
    {
      this.DataMessageGUID = Guid.NewGuid();
      if (new Monnit.Data.DataMessage.Insert(this).Result)
        Monnit.Cassandra.DataMessage.BulkInsert(new List<DataMessage>()
        {
          this
        });
      else
        this.DataMessageGUID = Guid.Empty;
    }
    TimedCache.RemoveObject($"Sensor_{this.SensorID}");
  }

  public static void CassandraUpdateDataMessageFlag(DataMessage dm)
  {
    try
    {
      int year = dm.MessageDate.Year;
      CassandraHelper.mapper.UpdateAsync<DataMessage>("SET HasNote = ?, MeetsNotificationRequirement = ? WHERE SensorID = ? and Year = ? and MessageDate = ? and DataMessageGuid = ?", (object) dm.HasNote, (object) dm.MeetsNotificationRequirement, (object) dm.SensorID, (object) year, (object) dm.MessageDate, (object) dm.DataMessageGUID);
    }
    catch (Exception ex)
    {
      ex.Log($"CassandraUpdateDataMessageFlag[sensorID: {dm.SensorID.ToString()}][messageDate: {dm.MessageDate.ToShortDateString()}] ");
    }
  }

  public DateTime MessageDateLocalTime(long TimeZoneID)
  {
    return TimeZone.GetLocalTimeById(this.MessageDate, TimeZoneID);
  }

  public long MonnitApplicationID
  {
    get
    {
      if (this._MonnitApplicationID == long.MinValue)
        this._MonnitApplicationID = Sensor.AppBaseID(this.SensorID);
      return this._MonnitApplicationID;
    }
    set => this._MonnitApplicationID = value;
  }

  [DBProp("ApplicationID", AllowNull = true)]
  public long ApplicationID
  {
    get => this.MonnitApplicationID;
    set => this.MonnitApplicationID = value;
  }

  public MonnitApplicationBase AppBase
  {
    get
    {
      if (this._AppBase == null)
        this._AppBase = MonnitApplicationBase.LoadMonnitApplication("1", this.Data, this.MonnitApplicationID, this.SensorID);
      return this._AppBase;
    }
  }

  public static int GetSignalStrengthPercent(string generation, long sensorTypeID, int rssi)
  {
    int num = 0;
    if (rssi != 0)
      num = !generation.ToUpper().Contains("GEN2") ? (sensorTypeID != 4L && sensorTypeID != 8L ? (rssi <= -80 ? (rssi + 100) * 3 : (rssi + 80 /*0x50*/) * 4 / 3 + 60) : (rssi <= -70 ? ((double) (rssi + 84) * 4.2857).ToInt() : (rssi + 70) * 2 + 60)) : (rssi <= -90 ? ((double) (rssi + 115) * 2.4).ToInt() : rssi + 90 + 60);
    return num > 100 ? 100 : (num < 0 ? 0 : num);
  }

  public string DisplayData => this.AppBase.NotificationString;

  public string PlotValueString => this.AppBase.PlotValue.ToStringSafe();

  public string SpecialExportValue(long monnitappID)
  {
    return MonnitApplicationBase.SpecialExportValue(monnitappID, this.AppBase).ToStringSafe();
  }

  public string Serialize(string version)
  {
    return new Version(version) > new Version("1.2.0") ? this.Serialize_v1_2_1() : this.Serialize_v0_0_1();
  }

  public string Serialize_v0_0_1()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("<DataMessage guid='{0}' sensorID='{1}' messageDate='{2}' state='{3}' signalStrength='{4}' linkQuality='{5}' battery='{6}'>", (object) this.DataMessageGUID, (object) this.SensorID, (object) this.MessageDate, (object) this.State, (object) this.SignalStrength, (object) this.LinkQuality, (object) this.Battery);
    stringBuilder.Append(this.Data);
    stringBuilder.AppendLine("</DataMessage>");
    return stringBuilder.ToString();
  }

  public string Serialize_v1_2_1()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("<DataMessage guid='{0}' sensorID='{1}' messageDate='{2}' state='{3}' signalStrength='{4}' linkQuality='{5}' voltage='{6}'>", (object) this.DataMessageGUID, (object) this.SensorID, (object) this.MessageDate, (object) this.State, (object) this.SignalStrength, (object) this.LinkQuality, (object) this.Voltage);
    stringBuilder.Append(this.Data);
    stringBuilder.AppendLine("</DataMessage>");
    return stringBuilder.ToString();
  }

  public static DataMessage Deserialize(string version, string serializedDataMessage)
  {
    return new Version(version) > new Version("1.2.0") ? DataMessage.Deserialize_v1_2_1(serializedDataMessage) : DataMessage.Deserialize_v0_0_1(serializedDataMessage);
  }

  public static DataMessage Deserialize_v0_0_1(string serializedDataMessage)
  {
    DataMessage dataMessage = new DataMessage();
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load((TextReader) new StringReader(serializedDataMessage));
    XmlNode firstChild = xmlDocument.FirstChild;
    try
    {
      dataMessage.DataMessageGUID = firstChild.Attributes["guid"].Value.ToGuid();
    }
    catch
    {
      dataMessage.DataMessageGUID = Guid.NewGuid();
    }
    dataMessage.SensorID = firstChild.Attributes["sensorID"].Value.ToLong();
    dataMessage.MessageDate = firstChild.Attributes["messageDate"].Value.ToDateTime();
    dataMessage.State = firstChild.Attributes["state"].Value.ToInt();
    dataMessage.SignalStrength = firstChild.Attributes["signalStrength"].Value.ToInt();
    dataMessage.LinkQuality = firstChild.Attributes["linkQuality"].Value.ToInt();
    dataMessage.Battery = firstChild.Attributes["battery"].Value.ToInt();
    dataMessage.Data = firstChild.InnerXml;
    return dataMessage;
  }

  public static DataMessage Deserialize_v1_2_1(string serializedDataMessage)
  {
    DataMessage dataMessage = new DataMessage();
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load((TextReader) new StringReader(serializedDataMessage));
    XmlNode firstChild = xmlDocument.FirstChild;
    try
    {
      dataMessage.DataMessageGUID = firstChild.Attributes["guid"].Value.ToGuid();
    }
    catch
    {
      dataMessage.DataMessageGUID = Guid.NewGuid();
    }
    dataMessage.SensorID = firstChild.Attributes["sensorID"].Value.ToLong();
    dataMessage.MessageDate = firstChild.Attributes["messageDate"].Value.ToDateTime();
    dataMessage.State = firstChild.Attributes["state"].Value.ToInt();
    dataMessage.SignalStrength = firstChild.Attributes["signalStrength"].Value.ToInt();
    dataMessage.LinkQuality = firstChild.Attributes["linkQuality"].Value.ToInt();
    dataMessage.Voltage = firstChild.Attributes["voltage"].Value.ToDouble();
    dataMessage.Data = firstChild.InnerXml;
    return dataMessage;
  }

  public string ToCSVString(
    Sensor sensor,
    long timeZoneID,
    string dateFormat,
    string timeFormat,
    Dictionary<string, string> dic)
  {
    object[] objArray = new object[15];
    objArray[0] = !dic.ContainsKey("DataMessageGUID") ? (object) $"\"{this.DataMessageGUID}\"," : (!dic.ContainsKey("DataMessageGUID") || !(dic["DataMessageGUID"].ToString() == "true") ? (object) "" : (object) $"\"{this.DataMessageGUID}\",");
    objArray[1] = !dic.ContainsKey("SensorID") ? (object) $"\"{this.SensorID}\"," : (!dic.ContainsKey("SensorID") || !(dic["SensorID"].ToString() == "true") ? (object) "" : (object) $"\"{this.SensorID}\",");
    objArray[2] = !dic.ContainsKey("Sensor_Name") || sensor == null ? (object) "" : (object) $"\"{WebUtility.HtmlDecode(sensor.SensorName).Replace("\"", "\"\"")}\",";
    objArray[3] = dic.ContainsKey("Date") ? (object) $"\"{this.MessageDate.ToLocalDateTimeFormatted(timeZoneID, dateFormat, timeFormat)}\"," : (object) "";
    objArray[4] = dic.ContainsKey("Value") ? (object) $"\"{this.PlotValueString}\"," : (object) "";
    objArray[5] = dic.ContainsKey("Formatted_Value") ? (object) $"\"{this.DisplayData.Replace("\"", "\"\"")}\"," : (object) "";
    int num1;
    string str1;
    if (!dic.ContainsKey("Battery"))
    {
      str1 = "";
    }
    else
    {
      num1 = this.Battery;
      str1 = $"\"{num1.ToString()}\",";
    }
    objArray[6] = (object) str1;
    objArray[7] = dic.ContainsKey("Raw_Data") ? (object) $"\"{this.Data.Replace("\"", "\"\"")}\"," : (object) "";
    string str2;
    if (!dic.ContainsKey("Sensor_State"))
    {
      str2 = "";
    }
    else
    {
      num1 = this.State;
      str2 = $"\"{num1.ToString()}\",";
    }
    objArray[8] = (object) str2;
    long num2;
    string str3;
    if (!dic.ContainsKey("GatewayID"))
    {
      str3 = "";
    }
    else
    {
      num2 = this.GatewayID;
      str3 = $"\"{num2.ToString()}\",";
    }
    objArray[9] = (object) str3;
    objArray[10] = dic.ContainsKey("Alert_Sent") ? (object) $"\"{this.MeetsNotificationRequirement.ToString()}\"," : (object) "";
    string str4;
    if (!dic.ContainsKey("Signal_Strength") || sensor == null)
    {
      str4 = "";
    }
    else
    {
      num1 = DataMessage.GetSignalStrengthPercent(sensor.GenerationType, sensor.SensorTypeID, this.SignalStrength);
      str4 = $"\"{num1.ToString()}\",";
    }
    objArray[11] = (object) str4;
    objArray[12] = dic.ContainsKey("Voltage") ? (this.Voltage == double.MinValue ? (object) "" : (object) $"\"{this.Voltage.ToString()}\",") : (object) "";
    string str5;
    if (!dic.ContainsKey("ApplicationID"))
      str5 = "";
    else if ((double) this.ApplicationID != double.MinValue)
    {
      num2 = this.ApplicationID;
      str5 = $"\"{num2.ToString()}\",";
    }
    else
      str5 = "";
    objArray[13] = (object) str5;
    objArray[14] = dic.ContainsKey("Special") ? (object) $"{this.SpecialExportValue(sensor.ApplicationID)}" : (object) "";
    return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}", objArray).TrimEnd(',');
  }

  public static string ToCSVHeaderString(Dictionary<string, string> dic)
  {
    return $"{(!dic.ContainsKey("DataMessageGUID") ? (object) "\"DataMessageGUID\"," : (!dic.ContainsKey("DataMessageGUID") || !(dic["DataMessageGUID"].ToString() == "true") ? (object) "" : (object) "\"DataMessageGUID\","))}{(!dic.ContainsKey("SensorID") ? (object) "\"SensorID\"," : (!dic.ContainsKey("SensorID") || !(dic["SensorID"].ToString() == "true") ? (object) "" : (object) "\"SensorID\","))}{(dic.ContainsKey("Sensor_Name") ? (object) "\"Sensor Name\"," : (object) "")}{(dic.ContainsKey("Date") ? (object) "\"Date\"," : (object) "")}{(dic.ContainsKey("Value") ? (object) "\"Value\"," : (object) "")}{(dic.ContainsKey("Formatted_Value") ? (object) "\"Formatted Value\"," : (object) "")}{(dic.ContainsKey("Battery") ? (object) "\"Battery\"," : (object) "")}{(dic.ContainsKey("Raw_Data") ? (object) "\"Raw Data\"," : (object) "")}{(dic.ContainsKey("Sensor_State") ? (object) "\"Sensor State\"," : (object) "")}{(dic.ContainsKey("GatewayID") ? (object) "\"GatewayID\"," : (object) "")}{(dic.ContainsKey("Alert_Sent") ? (object) "\"Alert Sent\"," : (object) "")}{(dic.ContainsKey("Signal_Strength") ? (object) "\"Signal Strength\"," : (object) "")}{(dic.ContainsKey("Voltage") ? (object) "\"Voltage\"," : (object) "")}{(dic.ContainsKey("ApplicationID") ? (object) "\"ApplicationID\"," : (object) "")}{(dic.ContainsKey("Special") ? (object) "\"Special Export Value\"" : (object) "")}".TrimEnd(',');
  }

  public static string ToCSVFileSensor(
    long sensorID,
    DateTime UTCFrom,
    DateTime UTCTo,
    int limit,
    Dictionary<string, string> dic,
    long timeZoneID,
    string dateFormat,
    string timeFormat)
  {
    Dictionary<string, string> dic1 = new Dictionary<string, string>();
    foreach (string key in dic.Keys)
      dic1.Add(key, "true");
    List<DataMessage> dataMessageList = DataMessage.LoadBySensorAndDateRange(sensorID, UTCFrom, UTCTo, limit, new Guid?());
    Sensor sensor = Sensor.Load(sensorID);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine(DataMessage.ToCSVHeaderString(dic1));
    foreach (DataMessage dataMessage in dataMessageList)
      stringBuilder.AppendFormat("{0}\r\n", (object) dataMessage.ToCSVString(sensor, timeZoneID, dateFormat, timeFormat, dic1));
    return stringBuilder.ToString();
  }

  public static string ToCSVFileNetwork(
    long CSNetID,
    DateTime UTCFrom,
    DateTime UTCTo,
    int limit,
    Dictionary<string, string> dic,
    long timeZoneID,
    string dateFormat,
    string timeFormat)
  {
    Dictionary<string, string> dic1 = new Dictionary<string, string>();
    foreach (string key in dic.Keys)
    {
      if (dic[key].ToBool())
        dic1.Add(key, "true");
      else if (!dic[key].ToBool())
        dic1.Add(key, "false");
    }
    List<DataMessage> list = DataMessage.LoadByNetwork(CSNetID, UTCFrom, UTCTo, limit).OrderBy<DataMessage, long>((System.Func<DataMessage, long>) (sensorID => sensorID.SensorID)).ThenBy<DataMessage, DateTime>((System.Func<DataMessage, DateTime>) (date => date.MessageDate)).ToList<DataMessage>();
    List<Sensor> source = Sensor.LoadByCsNetID(CSNetID);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine(DataMessage.ToCSVHeaderString(dic1));
    long num = long.MinValue;
    foreach (DataMessage dataMessage in list)
    {
      DataMessage message = dataMessage;
      Sensor sensor = source.Where<Sensor>((System.Func<Sensor, bool>) (s => s.SensorID == message.SensorID)).FirstOrDefault<Sensor>();
      if (num == long.MinValue)
      {
        num = sensor.SensorID;
        stringBuilder.AppendFormat("{0}\r\n", (object) message.ToCSVString(sensor, timeZoneID, dateFormat, timeFormat, dic1));
      }
      else if (num != long.MinValue && sensor.SensorID == num)
      {
        num = sensor.SensorID;
        stringBuilder.AppendFormat("{0}\r\n", (object) message.ToCSVString(sensor, timeZoneID, dateFormat, timeFormat, dic1));
      }
      else if (num != long.MinValue && sensor.SensorID != num)
      {
        num = sensor.SensorID;
        stringBuilder.AppendLine();
        stringBuilder.AppendFormat("{0}\r\n", (object) message.ToCSVString(sensor, timeZoneID, dateFormat, timeFormat, dic1));
      }
    }
    return stringBuilder.ToString();
  }

  public static string ToCSVFileAccount(
    long accountID,
    DateTime UTCFrom,
    DateTime UTCTo,
    int limit,
    Dictionary<string, string> dic,
    long timeZoneID,
    string dateFormat,
    string timeFormat)
  {
    Dictionary<string, string> dic1 = new Dictionary<string, string>();
    foreach (string key in dic.Keys)
      dic1.Add(key, "1");
    List<DataMessage> list = DataMessage.LoadByAccount(accountID, UTCFrom, UTCTo, limit).OrderBy<DataMessage, long>((System.Func<DataMessage, long>) (sensorID => sensorID.SensorID)).ThenBy<DataMessage, DateTime>((System.Func<DataMessage, DateTime>) (date => date.MessageDate)).ToList<DataMessage>();
    List<Sensor> source = Sensor.LoadByAccountID(accountID);
    long num = long.MinValue;
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine(DataMessage.ToCSVHeaderString(dic1));
    foreach (DataMessage dataMessage in list)
    {
      DataMessage message = dataMessage;
      Sensor sensor = source.Where<Sensor>((System.Func<Sensor, bool>) (s => s.SensorID == message.SensorID)).FirstOrDefault<Sensor>();
      if (num == long.MinValue)
      {
        num = sensor.SensorID;
        stringBuilder.AppendFormat("{0}\r\n", (object) message.ToCSVString(sensor, timeZoneID, dateFormat, timeFormat, dic1));
      }
      else if (num != long.MinValue && sensor.SensorID == num)
      {
        num = sensor.SensorID;
        stringBuilder.AppendFormat("{0}\r\n", (object) message.ToCSVString(sensor, timeZoneID, dateFormat, timeFormat, dic1));
      }
      else if (num != long.MinValue && sensor.SensorID != num)
      {
        num = sensor.SensorID;
        stringBuilder.AppendLine();
        stringBuilder.AppendFormat("{0}\r\n", (object) message.ToCSVString(sensor, timeZoneID, dateFormat, timeFormat, dic1));
      }
    }
    return stringBuilder.ToString();
  }

  public static List<DataMessage> CacheLastByAccount(long accountID, TimeSpan cacheDuration)
  {
    List<DataMessage> result = new Monnit.Data.DataMessage.LoadLastByAccount(accountID).Result;
    foreach (DataMessage dataMessage in result)
      TimedCache.AddObjectToCach($"LastDataMessage_SID{dataMessage.SensorID}", (object) dataMessage, cacheDuration);
    return result;
  }

  public static List<DataMessage> CacheLastByNetwork(long csNetID, TimeSpan cacheDuration)
  {
    List<DataMessage> result = new Monnit.Data.DataMessage.LoadLastByNetwork(csNetID).Result;
    foreach (DataMessage dataMessage in result)
      TimedCache.AddObjectToCach($"LastDataMessage_SID{dataMessage.SensorID}", (object) dataMessage, cacheDuration);
    return result;
  }

  public void AddToCache(int cacheMinues = 1)
  {
    TimedCache.AddObjectToCach($"LastDataMessage_SID{this.SensorID}", (object) this, new TimeSpan(0, cacheMinues, 0));
  }

  public static DataMessage LoadLastBySensorQuickCached(Sensor sensor)
  {
    if (((IEnumerable<StackFrame>) new StackTrace().GetFrames()).Select<StackFrame, string>((System.Func<StackFrame, string>) (f => f.GetMethod().Name)).Where<string>((Func<string, int, bool>) ((n, i) => i > 0 && n.ToLower() == "loadlastbysensorquickcached")).Count<string>() > 0)
      throw new StackOverflowException("Recursive call to DataMessage.LoadLastBySensorQuickCached");
    if (sensor == null)
      return new DataMessage();
    string key = $"LastDataMessage_SID{sensor.SensorID}";
    DataMessage dataMessage = TimedCache.RetrieveObject<DataMessage>(key);
    if (dataMessage == null && sensor.LastDataMessageGUID != Guid.Empty && sensor.LastCommunicationDate != new DateTime(2099, 1, 1))
    {
      dataMessage = DataMessage.LoadLastBySensorQuick(sensor);
      if (dataMessage != null)
        TimedCache.AddObjectToCach(key, (object) dataMessage, new TimeSpan(0, 0, 30));
    }
    return dataMessage;
  }

  public static void ClearLastBySensorCached(long sensorID)
  {
    TimedCache.RemoveObject($"LastDataMessage_SID{sensorID}");
  }

  public static DataMessage LoadLastBefore(Sensor sensor)
  {
    return DataMessage.LoadLastBefore(sensor.SensorID, sensor.LastCommunicationDate);
  }

  public static DataMessage LoadLastBefore(long sensorID, DateTime startDate)
  {
    return Monnit.Cassandra.DataMessage.LoadLastBefore(sensorID, startDate) ?? new Monnit.Data.DataMessage.LoadLastBefore(sensorID, startDate).Result;
  }

  public static List<DataMessage> LoadForChart(
    long sensorID,
    DateTime fromDate,
    DateTime toDate,
    int maxCount)
  {
    return Monnit.Cassandra.DataMessage.LoadBySensorAndDateRange(sensorID, fromDate, toDate, maxCount, new Guid?(), (string) null, DateTime.UtcNow) ?? new Monnit.Data.DataMessage.LoadForChart(sensorID, fromDate, toDate, maxCount).Result;
  }

  public static List<DataMessage> LoadAllForChart(
    long sensorID,
    DateTime fromDate,
    DateTime toDate)
  {
    return Monnit.Cassandra.DataMessage.LoadBySensorAndDateRange(sensorID, fromDate, toDate, int.MaxValue, new Guid?(), (string) null, DateTime.UtcNow) ?? new Monnit.Data.DataMessage.LoadAllForChart(sensorID, fromDate, toDate).Result;
  }

  public static List<DataPoint> CountByDay_LoadForChart(
    long sensorID,
    DateTime fromDate,
    DateTime toDate,
    int maxCount)
  {
    return Monnit.Cassandra.DataMessage.CountByDate_LoadForChart(sensorID, fromDate, toDate, maxCount) ?? new Monnit.Data.DataMessage.CountByDay_LoadForChart(sensorID, fromDate, toDate, maxCount).Result;
  }

  public static List<DataMessage> LoadBySensorAndDateRange(
    long sensorID,
    DateTime fromDate,
    DateTime toDate,
    int limit,
    Guid? dataMessageGUID)
  {
    Sensor sensor = Sensor.Load(sensorID) ?? new Sensor();
    fromDate = fromDate < sensor.StartDate ? sensor.StartDate : fromDate;
    return Monnit.Cassandra.DataMessage.LoadBySensorAndDateRange(sensorID, fromDate, toDate, limit, dataMessageGUID, (string) null, DateTime.UtcNow) ?? new Monnit.Data.DataMessage.LoadBySensorAndDateRange(sensorID, fromDate, toDate, limit, dataMessageGUID).Result;
  }

  public static DataTable LoadMissedBySensorAndDateRange(
    long sensorID,
    DateTime fromDate,
    DateTime toDate)
  {
    return new Monnit.Data.DataMessage.LoadMissedBySensorAndDateRange(sensorID, fromDate, toDate).Result;
  }

  public static List<DataMessage> LoadByNetwork(
    long networkID,
    DateTime fromDate,
    DateTime toDate,
    int limit)
  {
    return Monnit.Cassandra.DataMessage.LoadByNetwork(networkID, fromDate, toDate, limit) ?? new Monnit.Data.DataMessage.LoadRangeByNetWork(networkID, fromDate, toDate, limit).Result;
  }

  public static List<DataMessage> LoadByAccount(
    long accountID,
    DateTime fromDate,
    DateTime toDate,
    int limit)
  {
    return Monnit.Cassandra.DataMessage.LoadByAccount(accountID, fromDate, toDate, limit) ?? new Monnit.Data.DataMessage.LoadRangeByAccount(accountID, fromDate, toDate, limit).Result;
  }

  public static List<DataMessage> LoadRecentBySensor(
    long sensorID,
    int minutes,
    Guid lastDataMessageGUID)
  {
    return Monnit.Cassandra.DataMessage.LoadRecentBySensor(sensorID, minutes, new Guid?(lastDataMessageGUID)) ?? new Monnit.Data.DataMessage.LoadRecentBySensor(sensorID, minutes, lastDataMessageGUID).Result;
  }

  public static List<DataMessage> BulkInsert(List<DataMessage> messageList)
  {
    if (messageList.Count == 0)
      return messageList;
    if (Database.SupportsTableParameters)
      return new Monnit.Data.DataMessage.BulkInsert(messageList).Result;
    foreach (BaseDBObject message in messageList)
      message.Save();
    for (int index = messageList.Count<DataMessage>() - 1; index >= 0; --index)
    {
      if (messageList[index].DataMessageGUID == Guid.Empty)
        messageList.RemoveAt(index);
    }
    return messageList;
  }
}
