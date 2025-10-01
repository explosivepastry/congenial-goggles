// Decompiled with JetBrains decompiler
// Type: Monnit.Attention
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

#nullable disable
namespace Monnit;

public class Attention : MonnitApplicationBase
{
  public static long MonnitApplicationID => 13;

  public override long ApplicationID => Attention.MonnitApplicationID;

  public static string ApplicationName => nameof (Attention);

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Attention.Deserialize(version, serialized);
  }

  public static Attention Deserialize(string version, string serialized)
  {
    Attention attention = new Attention();
    if (string.IsNullOrEmpty(serialized))
    {
      attention.Notifications = (byte) 0;
      attention.SensorReadings = (byte) 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      attention.Notifications = Convert.ToByte(strArray[0]);
      attention.SensorReadings = strArray.Length != 2 ? Convert.ToByte(strArray[0]) : Convert.ToByte(strArray[1]);
    }
    return attention;
  }

  public static Attention Create(byte[] sdm, int startIndex)
  {
    return new Attention()
    {
      Notifications = sdm[startIndex],
      SensorReadings = sdm[startIndex + 1]
    };
  }

  public new static void DefaultConfigurationSettings(Sensor sensor)
  {
    sensor.ChannelMask = sensor.DefaultValue<long>("DefaultChannelMask");
    sensor.StandardMessageDelay = sensor.DefaultValue<int>("DefaultStandardMessageDelay");
    sensor.TransmitIntervalLink = sensor.DefaultValue<int>("DefaultTransmitIntervalLink");
    sensor.TransmitIntervalTest = sensor.DefaultValue<int>("DefaultTransmitIntervalTest");
    sensor.TestTransmitCount = sensor.DefaultValue<int>("DefaultTestTransmitCount");
    sensor.MaximumNetworkHops = sensor.DefaultValue<int>("DefaultMaximumNetworkHops");
    sensor.RetryCount = sensor.DefaultValue<int>("DefaultRetryCount");
    sensor.Recovery = sensor.DefaultValue<int>("DefaultRecovery");
    sensor.TimeOfDayActive = sensor.DefaultValue<byte[]>("DefaultTimeOfDayActive");
    sensor.MinimumCommunicationFrequency = (int) (sensor.ReportInterval * 2.0) + 10;
    sensor.ListenBeforeTalkValue = sensor.DefaultValue<int>("DefaultListenBeforeTalk");
    sensor.LinkAcceptance = sensor.DefaultValue<int>("DefaultLinkAcceptance");
    sensor.CrystalStartTime = sensor.DefaultValue<int>("DefaultCrystalDefaultTime");
    sensor.DMExchangeDelayMultiple = sensor.DefaultValue<int>("DefaultDMExchangeDelayMultiple");
    sensor.SHID1 = sensor.DefaultValue<long>("DefaultSensorHood1");
    sensor.SHID2 = sensor.DefaultValue<long>("DefaultSensorHood2");
    sensor.CryptRequired = sensor.DefaultValue<int>("DefaultCryptRequired");
    sensor.Pingtime = sensor.DefaultValue<int>("DefaultPingtime");
    sensor.TimeOffset = sensor.DefaultValue<int>("TimeOffset");
    sensor.TimeOfDayControl = sensor.DefaultValue<int>("TimeOfDayControl");
    sensor.MeasurementsPerTransmission = sensor.DefaultValue<int>("DefaultMeasurementsPerTransmission");
    sensor.TransmitOffset = sensor.DefaultValue<int>("DefaultTransmitOffset");
    sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
    sensor.EventDetectionType = sensor.DefaultValue<int>("DefaultEventDetectionType");
    sensor.EventDetectionPeriod = sensor.DefaultValue<int>("DefaultEventDetectionPeriod");
    sensor.EventDetectionCount = sensor.DefaultValue<int>("DefaultEventDetectionCount");
    sensor.RearmTime = sensor.DefaultValue<int>("DefaultRearmTime");
    sensor.BiStable = sensor.DefaultValue<int>("DefaultBiStable");
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = Attention.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    double result = 0.0;
    if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
    {
      double num = collection["Hysteresis_Manual"].ToDouble();
      sensor.Hysteresis = (long) Convert.ToInt32(num * 60.0);
      if (sensor.Hysteresis < 15L)
        sensor.Hysteresis = 15L;
    }
    if (!string.IsNullOrEmpty(collection["Alarm_Time"]) && !string.IsNullOrEmpty(collection["Snooze_Time"]))
    {
      uint num = ((uint) (collection["Snooze_Time"].ToDouble() * 60.0) << 16 /*0x10*/) + (uint) (collection["Alarm_Time"].ToDouble() * 60.0);
      sensor.Calibration1 = (long) num;
    }
    if (!string.IsNullOrEmpty(collection["Scroll"]) && !string.IsNullOrEmpty(collection["Buzzer"]) && !string.IsNullOrEmpty(collection["LED"]) && !string.IsNullOrEmpty(collection["BackLight"]) && !string.IsNullOrEmpty(collection["ScrollSpeed"]))
    {
      uint num = 0;
      if (!string.IsNullOrWhiteSpace(collection["ScrollSpeed"]))
        num = (uint) (collection["ScrollSpeed"].ToInt() * 1000) << 16 /*0x10*/;
      if (collection["BackLight"].Contains("true") || collection["BackLight"].Contains("on"))
        num += 2048U /*0x0800*/;
      if (collection["LED"].Contains("true") || collection["LED"].Contains("on"))
        num += 1024U /*0x0400*/;
      if (collection["Buzzer"].Contains("true") || collection["Buzzer"].Contains("on"))
        num += 512U /*0x0200*/;
      if (collection["Scroll"].Contains("true") || collection["Scroll"].Contains("on"))
        num += 256U /*0x0100*/;
      sensor.Calibration2 = (long) num;
    }
    if (!string.IsNullOrEmpty(collection["TimeZone_OffSet"]))
      sensor.Calibration3 = (long) (uint) collection["TimeZone_OffSet"].ToInt();
    if (!string.IsNullOrEmpty(collection["CalVal4_Manual"]))
      sensor.Calibration4 = (long) collection["CalVal4_Manual"].ToInt();
    if (!string.IsNullOrWhiteSpace(collection["MinimumThreshold_Manual"]))
      sensor.MinimumThreshold = collection["MinimumThreshold_Manual"].ToLong();
    sensor.ActiveStateInterval = sensor.ReportInterval;
  }

  public byte Notifications { get; set; }

  public byte SensorReadings { get; set; }

  public override string Serialize()
  {
    byte num = this.Notifications;
    string str1 = num.ToString();
    num = this.SensorReadings;
    string str2 = num.ToString();
    return $"{str1}|{str2}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.Count, "Notifications", (int) this.Notifications),
        new AppDatum(eDatumType.Count, "Sensor Readings", (int) this.SensorReadings)
      });
    }
  }

  public override string NotificationString
  {
    get
    {
      return string.Format("Notifications:{1} Sensor Readings:{0}", (object) this.SensorReadings, (object) this.Notifications);
    }
  }

  public override object PlotValue
  {
    get => (object) ((int) this.Notifications + (int) this.SensorReadings);
  }

  public static string CreateSerializedRecipientProperties(
    bool led,
    bool buzzer,
    bool autoScroll,
    bool backLight,
    string deviceName)
  {
    return $"{led}|{buzzer}|{autoScroll}|{backLight}|{deviceName}";
  }

  public static void ParseSerializedRecipientProperties(
    string serialized,
    out bool led,
    out bool buzzer,
    out bool autoScroll,
    out bool backLight,
    out string deviceName)
  {
    led = true;
    buzzer = true;
    autoScroll = true;
    backLight = true;
    deviceName = string.Empty;
    try
    {
      string[] strArray = serialized.Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      led = strArray[0].ToBool();
      buzzer = strArray[1].ToBool();
      autoScroll = strArray[2].ToBool();
      backLight = strArray[3].ToBool();
      deviceName = strArray[4];
    }
    catch
    {
    }
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    List<NotificationRecorded> list = NotificationRecorded.LoadGetMessageForLocalNotifier(sensor.SensorID).OrderBy<NotificationRecorded, eNotificationType>((Func<NotificationRecorded, eNotificationType>) (alert => alert.NotificationType)).ThenByDescending<NotificationRecorded, long>((Func<NotificationRecorded, long>) (alert => alert.NotificationRecordedID)).ToList<NotificationRecorded>();
    foreach (NotificationRecorded notificationRecorded in (IEnumerable<NotificationRecorded>) list.Take<NotificationRecorded>(10).OrderBy<NotificationRecorded, eNotificationType>((Func<NotificationRecorded, eNotificationType>) (alert => alert.NotificationType)).ThenBy<NotificationRecorded, long>((Func<NotificationRecorded, long>) (alert => alert.NotificationRecordedID)))
    {
      long num = 0;
      if (notificationRecorded.SensorID > 0L)
        num = notificationRecorded.SensorID;
      if (notificationRecorded.GatewayID > 0L)
        num = notificationRecorded.GatewayID;
      bool led;
      bool buzzer;
      bool autoScroll;
      bool backLight;
      string deviceName;
      Attention.ParseSerializedRecipientProperties(notificationRecorded.SerializedRecipientProperties, out led, out buzzer, out autoScroll, out backLight, out deviceName);
      deviceName = deviceName.Replace(sensor.SensorName, " ").Trim();
      if (notificationRecorded.NotificationType == eNotificationType.Local_Notifier)
      {
        numArrayList.AddRange((IEnumerable<byte[]>) AttentionBase.Message(notificationRecorded.SentToDeviceID, num, deviceName, notificationRecorded.QueueID, true, led, buzzer, autoScroll, backLight, notificationRecorded.NotificationDateLocalTime(Sensor.GetTimeZoneIDBySensorID(sensor.SensorID)), notificationRecorded.NotificationText));
      }
      else
      {
        Sensor sensor1 = Sensor.Load(num);
        if (sensor1 != null)
          numArrayList.AddRange((IEnumerable<byte[]>) AttentionBase.Message(notificationRecorded.SentToDeviceID, num, deviceName, notificationRecorded.QueueID, false, led, buzzer, autoScroll, backLight, notificationRecorded.NotificationDateLocalTime(Sensor.GetTimeZoneIDBySensorID(sensor.SensorID)), sensor1.LastDataMessage.DisplayData));
      }
    }
    foreach (NotificationRecorded notificationRecorded in list.Skip<NotificationRecorded>(10).ToList<NotificationRecorded>())
    {
      notificationRecorded.Status = "Cancelled";
      notificationRecorded.Delivered = true;
      notificationRecorded.Save();
    }
    if (sensor.PendingActionControlCommand && numArrayList.Count == 0)
    {
      sensor.PendingActionControlCommand = false;
      sensor.Save();
    }
    return numArrayList;
  }

  public new static bool ActionControlCommandIsUrgent(Sensor sensor, string version) => true;

  public new static void ClearPendingActionControlCommand(
    Sensor sensor,
    string version,
    int actionCommand,
    bool success,
    byte[] data)
  {
    try
    {
      int queueID = data.Length != 0 ? (int) data[0] : 0;
      switch (actionCommand)
      {
        case 3:
        case 4:
        case 10:
        case 13:
        case 14:
        case 205:
          NotificationRecorded notificationRecorded1 = NotificationRecorded.LoadNotificationForDeviceByQueueID(sensor.SensorID, queueID);
          if (notificationRecorded1 != null)
          {
            notificationRecorded1.Delivered = true;
            notificationRecorded1.Save();
            break;
          }
          if (ConfigData.AppSettings("LogClearPendingExceptions", "False").ToBool())
            new Exception($"Monnit.Attention.ClearPendingActionControlCommand- Delivered message not found for SensorID: {sensor.SensorID.ToString()} QueueID: {queueID.ToString()}").Log();
          break;
        case 5:
        case 7:
          NotificationRecorded notificationRecorded2 = NotificationRecorded.LoadNotificationForDeviceByQueueID(sensor.SensorID, queueID);
          if (notificationRecorded2 == null)
            break;
          notificationRecorded2.AcknowledgedDate = DateTime.UtcNow;
          notificationRecorded2.Save();
          NotificationTriggered notificationTriggered1 = NotificationTriggered.Load(notificationRecorded2.NotificationTriggeredID);
          if (notificationTriggered1 != null && notificationTriggered1.AcknowledgedTime == DateTime.MinValue)
          {
            notificationTriggered1.AcknowledgedTime = notificationRecorded2.AcknowledgedDate;
            notificationTriggered1.AcknowledgeMethod = "Local Alert Manual";
            notificationTriggered1.Save();
          }
          break;
        case 6:
        case 8:
          List<NotificationRecorded> notificationRecordedList = NotificationRecorded.LoadNonAcknowledgedNotificationForDevice(sensor.SensorID);
          NotificationRecorded notificationRecorded3 = NotificationRecorded.LoadNotificationForDeviceByQueueID(sensor.SensorID, queueID);
          using (List<NotificationRecorded>.Enumerator enumerator = notificationRecordedList.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              NotificationRecorded current = enumerator.Current;
              if (current.NotificationDate <= notificationRecorded3.NotificationDate)
              {
                current.AcknowledgedDate = DateTime.UtcNow;
                current.Save();
                NotificationTriggered notificationTriggered2 = NotificationTriggered.Load(current.NotificationTriggeredID);
                if (notificationTriggered2 != null && notificationTriggered2.AcknowledgedTime == DateTime.MinValue)
                {
                  notificationTriggered2.AcknowledgedTime = current.AcknowledgedDate;
                  notificationTriggered2.AcknowledgeMethod = "Local Alert Manual All";
                  notificationTriggered2.Save();
                }
              }
            }
            break;
          }
      }
    }
    catch (Exception ex)
    {
      if (!ConfigData.AppSettings("LogClearPendingExceptions", "False").ToBool())
        return;
      ex.Log("Monnit.Attention.ClearPendingActionControlCommand(Sensor sensor, string version, int actionCommand, bool success, byte[] data)");
    }
  }

  public static string HystForUI(Sensor sensor) => sensor.Hysteresis.ToString();

  public static string MinThreshForUI(Sensor sensor) => sensor.MinimumThreshold.ToString();

  public static string MaxThreshForUI(Sensor sensor) => "Not Used";

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Attention left, Attention right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Attention left, Attention right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Attention left, Attention right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Attention left, Attention right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Attention left, Attention right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Attention left, Attention right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Attention && this.Equals((MonnitApplicationBase) (obj as Attention));
  }

  public override bool Equals(MonnitApplicationBase right) => false;

  public override bool NotEqual(MonnitApplicationBase right) => false;

  public override bool LessThan(MonnitApplicationBase right) => false;

  public override bool LessThanEqual(MonnitApplicationBase right) => false;

  public override bool GreaterThan(MonnitApplicationBase right) => false;

  public override bool GreaterThanEqual(MonnitApplicationBase right) => false;

  public override string ChartType => "Line";
}
