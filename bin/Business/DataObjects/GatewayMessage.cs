// Decompiled with JetBrains decompiler
// Type: Monnit.GatewayMessage
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

#nullable disable
namespace Monnit;

[DBClass("GatewayMessage")]
public class GatewayMessage : BaseDBObject
{
  private Guid _GatewayMessageGUID = Guid.Empty;
  private long _GatewayID = long.MinValue;
  private Gateway _Gateway = (Gateway) null;
  private int _Power = int.MinValue;
  private int _Battery = int.MinValue;
  private DateTime _ReceivedDate = DateTime.MinValue;
  private bool _MeetsNotificationRequirement = false;
  private int _MessageType = int.MinValue;
  private int _MessageCount = int.MinValue;
  private int _SignalStrength = int.MinValue;

  [DBProp("GatewayMessageGUID", IsGuidPrimaryKey = true)]
  public Guid GatewayMessageGUID
  {
    get => this._GatewayMessageGUID;
    set => this._GatewayMessageGUID = value;
  }

  [DBProp("GatewayID", AllowNull = false)]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  public Gateway Gateway
  {
    get
    {
      if (this._Gateway == null)
        this._Gateway = Gateway.Load(this.GatewayID);
      return this._Gateway;
    }
  }

  [DBProp("Power")]
  public int Power
  {
    get => this._Power;
    set => this._Power = value;
  }

  [DBProp("Battery")]
  public int Battery
  {
    get
    {
      if (this._Battery == int.MinValue)
      {
        if (this.Power > 1 && this.Gateway.PowerSource != null)
        {
          try
          {
            this._Battery = this.Gateway.PowerSource.Percent(((int) Convert.ToUInt16(this.Power) & (int) short.MaxValue).ToDouble() / 1000.0).ToInt();
          }
          catch (Exception ex)
          {
            ex.Log("GatewayMessage.Battery_get: GatewayID " + this.GatewayID.ToString());
          }
        }
        else
          this._Battery = 101;
      }
      return this._Battery;
    }
    set => this._Battery = value;
  }

  [DBProp("ReceivedDate", AllowNull = false)]
  public DateTime ReceivedDate
  {
    get => this._ReceivedDate;
    set => this._ReceivedDate = value;
  }

  [DBProp("MeetsNotificationRequirement")]
  public bool MeetsNotificationRequirement
  {
    get => this._MeetsNotificationRequirement;
    set => this._MeetsNotificationRequirement = value;
  }

  [DBProp("MessageType")]
  public int MessageType
  {
    get => this._MessageType;
    set => this._MessageType = value;
  }

  [DBProp("MessageCount")]
  public int MessageCount
  {
    get => this._MessageCount;
    set => this._MessageCount = value;
  }

  [DBProp("SignalStrength")]
  public int SignalStrength
  {
    get => this._SignalStrength;
    set => this._SignalStrength = value;
  }

  public void CheckNotifications(PacketCache localCache)
  {
    Gateway gateway = Gateway.Load(this.GatewayID);
    DateTime notificationDate = gateway.resumeNotificationDate;
    if (false)
    {
      gateway.resumeNotificationDate = new DateTime(2010, 1, 1);
      gateway.Save();
    }
    if (gateway.resumeNotificationDate > DateTime.UtcNow)
      return;
    foreach (Notification objNotification in Notification.LoadByGatewayID(this.GatewayID))
    {
      if (objNotification.IsActive)
      {
        bool flag = objNotification.IsInNotificationWindow(TimeZone.GetLocalTimeById(DateTime.UtcNow, localCache.LoadAccount(objNotification.AccountID).TimeZoneID));
        GatewayNotification gatewayNotification = GatewayNotification.LoadByGatewayIDAndNotificationID(gateway.GatewayID, objNotification.NotificationID);
        switch (objNotification.NotificationClass)
        {
          case eNotificationClass.Inactivity:
            foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveByNotificationID(objNotification.NotificationID))
            {
              if (notificationTriggered.GatewayNotificationID == gatewayNotification.GatewayNotificationID)
              {
                if (objNotification.CanAutoAcknowledge)
                {
                  notificationTriggered.AcknowledgedBy = long.MinValue;
                  notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
                  notificationTriggered.AcknowledgeMethod = "System_Auto";
                }
                if (objNotification.CanAutoAcknowledge && notificationTriggered.resetTime == DateTime.MinValue)
                  notificationTriggered.resetTime = DateTime.UtcNow;
                notificationTriggered.Save();
              }
            }
            gatewayNotification.TriggerDate = DateTime.MinValue;
            gatewayNotification.Save();
            break;
          case eNotificationClass.Low_Battery:
            if ((double) this.Battery < objNotification.CompareValue.ToDouble())
            {
              try
              {
                this.MeetsNotificationRequirement = true;
                this.Save();
                if (flag)
                {
                  gatewayNotification.TriggerDate = DateTime.UtcNow;
                  gatewayNotification.Save();
                  objNotification.RecordNotification(localCache, false, $"Battery: {this.Battery}%", this.ReceivedDate, (Sensor) null, this.Gateway, long.MinValue, gatewayNotification.GatewayNotificationID, (DataMessage) null);
                  break;
                }
                break;
              }
              catch
              {
                break;
              }
            }
            else
            {
              foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveByNotificationID(objNotification.NotificationID))
              {
                if (notificationTriggered.GatewayNotificationID == gatewayNotification.GatewayNotificationID)
                {
                  if (objNotification.CanAutoAcknowledge)
                  {
                    notificationTriggered.AcknowledgedBy = long.MinValue;
                    notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
                    notificationTriggered.AcknowledgeMethod = "System_Auto";
                  }
                  if (objNotification.CanAutoAcknowledge && notificationTriggered.resetTime == DateTime.MinValue)
                    notificationTriggered.resetTime = DateTime.UtcNow;
                  notificationTriggered.Save();
                }
              }
              gatewayNotification.TriggerDate = DateTime.MinValue;
              gatewayNotification.Save();
              break;
            }
          case eNotificationClass.Advanced:
            if (objNotification.AdvancedNotificationID > long.MinValue)
            {
              AdvancedNotification advancedNotification = AdvancedNotification.Load(objNotification.AdvancedNotificationID);
              string AdvancedNotificationString = string.Empty;
              if (advancedNotification.Evaluate(objNotification, this.GatewayID, this.GatewayMessageGUID, out AdvancedNotificationString))
              {
                try
                {
                  this.MeetsNotificationRequirement = true;
                  this.Save();
                  if (flag && objNotification.IsInNotificationWindow(TimeZone.GetLocalTimeById(DateTime.UtcNow, Account.Load(objNotification.AccountID).TimeZoneID)))
                  {
                    gatewayNotification.TriggerDate = DateTime.UtcNow;
                    gatewayNotification.Save();
                    objNotification.RecordNotification(localCache, false, AdvancedNotificationString, gatewayNotification.TriggerDate, (Sensor) null, this.Gateway, long.MinValue, gatewayNotification.GatewayNotificationID, (DataMessage) null);
                  }
                }
                catch
                {
                }
              }
              else
              {
                foreach (NotificationTriggered notificationTriggered in NotificationTriggered.LoadActiveByNotificationID(objNotification.NotificationID))
                {
                  if (notificationTriggered.GatewayNotificationID == gatewayNotification.GatewayNotificationID)
                  {
                    if (objNotification.CanAutoAcknowledge)
                    {
                      notificationTriggered.AcknowledgedBy = long.MinValue;
                      notificationTriggered.AcknowledgedTime = DateTime.UtcNow;
                      notificationTriggered.AcknowledgeMethod = "System_Auto";
                    }
                    if (objNotification.CanAutoAcknowledge && notificationTriggered.resetTime == DateTime.MinValue)
                      notificationTriggered.resetTime = DateTime.UtcNow;
                    notificationTriggered.Save();
                  }
                }
                gatewayNotification.TriggerDate = DateTime.MinValue;
                gatewayNotification.Save();
              }
              break;
            }
            break;
        }
      }
    }
  }

  public static List<GatewayMessage> LoadByNetwork(
    long networkID,
    DateTime from,
    DateTime to,
    int limit)
  {
    return Monnit.Cassandra.GatewayMessage.CassandraLoadByNetwork(networkID, from, to, limit) ?? new Monnit.Data.GatewayMessage.LoadRangeByNetWork(networkID, from, to, limit).Result;
  }

  public static List<GatewayMessage> LoadByAccount(
    long accountID,
    DateTime from,
    DateTime to,
    int limit)
  {
    return Monnit.Cassandra.GatewayMessage.CassandraLoadByAccount(accountID, from, to, limit) ?? new Monnit.Data.GatewayMessage.LoadRangeByAccount(accountID, from, to, limit).Result;
  }

  public static int GetSignalStrengthPercent(long witType, int rssi)
  {
    int num = 0;
    if (rssi != 0)
      num = witType != 4L ? (rssi <= -80 ? (rssi + 100) * 3 : (rssi + 80 /*0x50*/) * 4 / 3 + 60) : (rssi <= -70 ? ((double) (rssi + 84) * 4.2857).ToInt() : (rssi + 70) * 2 + 60);
    return num > 100 ? 100 : (num < 0 ? 0 : num);
  }

  public static GatewayMessage Load(long id) => BaseDBObject.Load<GatewayMessage>(id);

  public override void Save()
  {
    Guid gatewayMessageGuid = this.GatewayMessageGUID;
    if (this.GatewayMessageGUID == Guid.Empty)
    {
      this.GatewayMessageGUID = Guid.NewGuid();
      Database.ExecuteNonQuery(Database.BuildInsert((BaseDBObject) this));
      Monnit.Cassandra.GatewayMessage.InsertGatewayMessage(this);
    }
    else
    {
      Monnit.Data.GatewayMessage.Update update = new Monnit.Data.GatewayMessage.Update(this.GatewayID, this.Power, this.Battery, this.ReceivedDate, this.MeetsNotificationRequirement, this.MessageType, this.MessageCount, this.SignalStrength, this.GatewayMessageGUID);
      Monnit.Cassandra.GatewayMessage.UpdateGatewayMessage(this);
    }
  }

  public static List<GatewayMessage> LoadByGatewayAndDateRange(
    long gatewayID,
    DateTime fromDate,
    DateTime toDate,
    int limit)
  {
    return Monnit.Cassandra.GatewayMessage.CassandraLoadByGatewayAndDateRange(gatewayID, new DateTime?(fromDate), new DateTime?(toDate), limit, new Guid?(), "GatewayMessage.LoadByGatewayAndDateRange2", DateTime.UtcNow) ?? new Monnit.Data.GatewayMessage.LoadByGatewayAndDateRange(gatewayID, fromDate, toDate, limit).Result;
  }

  public static List<GatewayMessage> LoadByGatewayAndDateRange2(
    long gatewayID,
    DateTime fromDate,
    DateTime toDate,
    int limit,
    Guid dataMessageGUID)
  {
    return Monnit.Cassandra.GatewayMessage.CassandraLoadByGatewayAndDateRange(gatewayID, new DateTime?(fromDate), new DateTime?(toDate), limit, new Guid?(dataMessageGUID), "GatewayMessage.LoadByGatewayAndDateRange2", DateTime.UtcNow) ?? new Monnit.Data.GatewayMessage.LoadByGatewayAndDateRange2(gatewayID, fromDate, toDate, limit, new Guid?(dataMessageGUID)).Result;
  }

  public static GatewayMessage LoadLastByGateway(long gatewayID)
  {
    return Monnit.Cassandra.GatewayMessage.CassandraLoadLastByGateway(gatewayID) ?? new Monnit.Data.GatewayMessage.LoadLastByGateway(gatewayID).Result;
  }

  public string ToCSVStringGateway(
    Gateway gateway,
    long timeZoneID,
    string dateFormat,
    string timeFormat,
    Dictionary<string, string> dic)
  {
    object[] objArray = new object[10]
    {
      !dic.ContainsKey("GatewayMessageGUID") ? (object) $"\"{this.GatewayMessageGUID}\"," : (!dic.ContainsKey("GatewayMessageGUID") || !(dic["GatewayMessageGUID"].ToString() == "true") ? (object) "" : (object) $"\"{this.GatewayMessageGUID}\","),
      !dic.ContainsKey("GatewayID") ? (object) $"\"{this.GatewayID}\"," : (!dic.ContainsKey("GatewayID") || !(dic["GatewayID"].ToString() == "true") ? (object) "" : (object) $"\"{this.GatewayID}\","),
      !dic.ContainsKey("Gateway_Name") || gateway == null ? (object) "" : (object) $"\"{WebUtility.HtmlDecode(gateway.Name).Replace("\"", "\"\"")}\",",
      dic.ContainsKey("ReceivedDate") ? (object) $"\"{this.ReceivedDate.ToLocalDateTimeFormatted(timeZoneID, dateFormat, timeFormat)}\"," : (object) "",
      dic.ContainsKey("Power") ? (object) $"\"{this.Power}\"," : (object) "",
      null,
      null,
      null,
      null,
      null
    };
    int num;
    string str1;
    if (!dic.ContainsKey("Battery"))
    {
      str1 = "";
    }
    else
    {
      num = this.Battery;
      str1 = $"\"{num.ToString()}\",";
    }
    objArray[5] = (object) str1;
    string str2;
    if (!dic.ContainsKey("MessageType"))
    {
      str2 = "";
    }
    else
    {
      num = this.MessageType;
      str2 = $"\"{num.ToString()}\",";
    }
    objArray[6] = (object) str2;
    string str3;
    if (!dic.ContainsKey("MessageCount"))
    {
      str3 = "";
    }
    else
    {
      num = this.MessageCount;
      str3 = $"\"{num.ToString()}\",";
    }
    objArray[7] = (object) str3;
    objArray[8] = dic.ContainsKey("MeetsNotificationRequirement") ? (object) $"\"{this.MeetsNotificationRequirement.ToString()}\"," : (object) "";
    objArray[9] = !dic.ContainsKey("Signal_Strength") || gateway == null ? (object) "" : (object) $"\"{this.SignalStrength}\",";
    return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}", objArray);
  }

  public string ToCSVHeaderStringGateway(Dictionary<string, string> dic)
  {
    return $"{(!dic.ContainsKey("GatewayMessageGUID") ? (object) "\"GatewayMessageGUID\"," : (!dic.ContainsKey("GatewayMessageGUID") || !(dic["GatewayMessageGUID"].ToString() == "true") ? (object) "" : (object) "\"GatewayMessageGUID\","))}{(!dic.ContainsKey("GatewayID") ? (object) "\"GatewayID\"," : (!dic.ContainsKey("GatewayID") || !(dic["GatewayID"].ToString() == "true") ? (object) "" : (object) "\"GatewayID\","))}{(dic.ContainsKey("Gateway_Name") ? (object) "\"Gateway Name\"," : (object) "")}{(dic.ContainsKey("ReceivedDate") ? (object) "\"ReceivedDate\"," : (object) "")}{(dic.ContainsKey("Power") ? (object) "\"Power\"," : (object) "")}{(dic.ContainsKey("Battery") ? (object) "\"Battery\"," : (object) "")}{(dic.ContainsKey("MessageType") ? (object) "\"MessageType\"," : (object) "")}{(dic.ContainsKey("MessageCount") ? (object) "\"MessageCount\"," : (object) "")}{(dic.ContainsKey("MeetsNotificationRequirement") ? (object) "\"MeetsNotificationRequirement\"," : (object) "")}{(dic.ContainsKey("Signal_Strength") ? (object) "\"Signal_Strength\"," : (object) "")}";
  }

  public static string ToCSVFileGateway(
    long gatewayID,
    DateTime fromDate,
    DateTime toDate,
    int limit,
    Dictionary<string, string> dic,
    long timeZoneID,
    string dateFormat,
    string timeFormat)
  {
    Dictionary<string, string> dic1 = new Dictionary<string, string>();
    foreach (string key in dic.Keys)
      dic1.Add(key, "true");
    List<GatewayMessage> gatewayMessageList = GatewayMessage.LoadByGatewayAndDateRange(gatewayID, fromDate, toDate, limit);
    Gateway gateway = Gateway.Load(gatewayID);
    StringBuilder stringBuilder = new StringBuilder();
    GatewayMessage gatewayMessage1 = new GatewayMessage();
    stringBuilder.AppendLine(gatewayMessage1.ToCSVHeaderStringGateway(dic1));
    foreach (GatewayMessage gatewayMessage2 in gatewayMessageList)
      stringBuilder.AppendFormat("{0}\r\n", (object) gatewayMessage2.ToCSVStringGateway(gateway, timeZoneID, dateFormat, timeFormat, dic1));
    return stringBuilder.ToString();
  }

  public static string ToCSVFileNetworkGateway(
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
    List<GatewayMessage> list = GatewayMessage.LoadByNetwork(CSNetID, UTCFrom, UTCTo, limit).OrderBy<GatewayMessage, long>((Func<GatewayMessage, long>) (gatewayID => gatewayID.GatewayID)).ThenBy<GatewayMessage, DateTime>((Func<GatewayMessage, DateTime>) (date => date.ReceivedDate)).ToList<GatewayMessage>();
    List<Gateway> source = Gateway.LoadByCSNetID(CSNetID);
    StringBuilder stringBuilder = new StringBuilder();
    GatewayMessage gatewayMessage1 = new GatewayMessage();
    long num = long.MinValue;
    stringBuilder.AppendLine(gatewayMessage1.ToCSVHeaderStringGateway(dic1));
    foreach (GatewayMessage gatewayMessage2 in list)
    {
      GatewayMessage message = gatewayMessage2;
      Gateway gateway = source.Where<Gateway>((Func<Gateway, bool>) (g => g.GatewayID == message.GatewayID)).FirstOrDefault<Gateway>();
      if (num == long.MinValue)
      {
        num = gateway.GatewayID;
        stringBuilder.AppendFormat("{0}\r\n", (object) message.ToCSVStringGateway(gateway, timeZoneID, dateFormat, timeFormat, dic1));
      }
      else if (num != long.MinValue && gateway.GatewayID == num)
      {
        num = gateway.GatewayID;
        stringBuilder.AppendFormat("{0}\r\n", (object) message.ToCSVStringGateway(gateway, timeZoneID, dateFormat, timeFormat, dic1));
      }
      else if (num != long.MinValue && gateway.GatewayID != num)
      {
        num = gateway.GatewayID;
        stringBuilder.AppendLine();
        stringBuilder.AppendFormat("{0}\r\n", (object) message.ToCSVStringGateway(gateway, timeZoneID, dateFormat, timeFormat, dic1));
      }
    }
    return stringBuilder.ToString();
  }

  public static string ToCSVFileAccountGateway(
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
    List<GatewayMessage> list = GatewayMessage.LoadByAccount(accountID, UTCFrom, UTCTo, limit).OrderBy<GatewayMessage, long>((Func<GatewayMessage, long>) (gatewayID => gatewayID.GatewayID)).ThenBy<GatewayMessage, DateTime>((Func<GatewayMessage, DateTime>) (date => date.ReceivedDate)).ToList<GatewayMessage>();
    List<Gateway> source = Gateway.LoadByAccountID(accountID);
    long num = long.MinValue;
    StringBuilder stringBuilder = new StringBuilder();
    GatewayMessage gatewayMessage1 = new GatewayMessage();
    stringBuilder.AppendLine(gatewayMessage1.ToCSVHeaderStringGateway(dic1));
    foreach (GatewayMessage gatewayMessage2 in list)
    {
      GatewayMessage message = gatewayMessage2;
      Gateway gateway = source.Where<Gateway>((Func<Gateway, bool>) (g => g.GatewayID == message.GatewayID)).FirstOrDefault<Gateway>();
      if (num == long.MinValue)
      {
        num = gateway.GatewayID;
        stringBuilder.AppendFormat("{0}\r\n", (object) message.ToCSVStringGateway(gateway, timeZoneID, dateFormat, timeFormat, dic1));
      }
      else if (num != long.MinValue && gateway.GatewayID == num)
      {
        num = gateway.GatewayID;
        stringBuilder.AppendFormat("{0}\r\n", (object) message.ToCSVStringGateway(gateway, timeZoneID, dateFormat, timeFormat, dic1));
      }
      else if (num != long.MinValue && gateway.GatewayID != num)
      {
        num = gateway.GatewayID;
        stringBuilder.AppendLine();
        stringBuilder.AppendFormat("{0}\r\n", (object) message.ToCSVStringGateway(gateway, timeZoneID, dateFormat, timeFormat, dic1));
      }
    }
    return stringBuilder.ToString();
  }
}
