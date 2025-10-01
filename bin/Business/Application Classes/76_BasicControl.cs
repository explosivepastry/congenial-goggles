// Decompiled with JetBrains decompiler
// Type: Monnit.BasicControl
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

#nullable disable
namespace Monnit;

public class BasicControl : MonnitApplicationBase, ISensorAttribute
{
  public bool RelayState1;
  internal SensorAttribute _Relay1NameAttribute;
  private SensorAttribute _Relay1VisibliityAttribute;

  public static long MonnitApplicationID => 76;

  public static string ApplicationName => nameof (BasicControl);

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "None";

  public override long ApplicationID => BasicControl.MonnitApplicationID;

  public override string Serialize() => $"{this.RelayState1.ToString()}";

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.ControlDetect, "RelayState1", this.RelayState1)
      });
    }
  }

  public static string GetLabel(long sensorID) => "";

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[1]
    {
      (object) this.RelayState1
    });
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) BasicControl.Deserialize(version, serialized);
  }

  public static string Relay1Name(long sensorID)
  {
    return MonnitApplicationBase.GetAttributeValue(sensorID, nameof (Relay1Name), "Relay1");
  }

  public static void SetRelay1Name(long sensorID, string name)
  {
    MonnitApplicationBase.SetAttribute(sensorID, "Relay1Name", name);
  }

  public static bool Relay1Visibility(long sensorID)
  {
    return MonnitApplicationBase.GetAttributeValue(sensorID, nameof (Relay1Visibility), "True").ToBool();
  }

  public static void ShowRelay1(long sensorID)
  {
    MonnitApplicationBase.SetAttribute(sensorID, "Relay1Visibility", (string) null);
  }

  public static void HideRelay1(long sensorID)
  {
    MonnitApplicationBase.SetAttribute(sensorID, "Relay1Visibility", "False");
  }

  public SensorAttribute Relay1NameAttribute
  {
    get
    {
      if (this._Relay1NameAttribute != null)
        return this._Relay1NameAttribute;
      return new SensorAttribute()
      {
        Name = "Relay1Name",
        Value = "Relay 1"
      };
    }
  }

  public SensorAttribute Relay1VisibliityAttribute
  {
    get
    {
      if (this._Relay1VisibliityAttribute != null)
        return this._Relay1VisibliityAttribute;
      return new SensorAttribute()
      {
        Name = "Relay1Visibility",
        Value = "True"
      };
    }
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Relay1Name")
        this._Relay1NameAttribute = sensorAttribute;
      if (sensorAttribute.Name == "Relay1Visibility")
        this._Relay1VisibliityAttribute = sensorAttribute;
      if (sensorAttribute.Name == "DatumIndex|0" && sensorAttribute.Value != "RelayState1" && sensorAttribute.Value != "Relay 1")
        this._Relay1NameAttribute = sensorAttribute;
    }
  }

  public override string NotificationString
  {
    get
    {
      bool flag = this.Relay1VisibliityAttribute.Value != "False";
      StringBuilder stringBuilder = new StringBuilder();
      if (flag)
        stringBuilder.AppendFormat("{0}: {1}", (object) this.Relay1NameAttribute.Value, this.RelayState1 ? (object) "On" : (object) "Off");
      return stringBuilder.ToString();
    }
  }

  public override object PlotValue => (object) (this.RelayState1 ? 1 : 0);

  public static BasicControl Deserialize(string version, string serialized)
  {
    BasicControl basicControl = new BasicControl();
    if (string.IsNullOrEmpty(serialized))
    {
      basicControl.RelayState1 = false;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      basicControl.RelayState1 = strArray[0].ToBool();
    }
    return basicControl;
  }

  public static BasicControl Create(byte[] sdm, int startIndex)
  {
    return new BasicControl()
    {
      RelayState1 = ((int) sdm[startIndex] & 1) != 0
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
      CompareValue = "True",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Equal,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = BasicControl.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    sensor.ActiveStateInterval = sensor.ReportInterval;
    double result = 0.0;
    if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (collection["Hysteresis_Manual"] != null && !string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
      sensor.Hysteresis = (collection["Hysteresis_Manual"].ToDouble() * 60.0).ToLong();
    if (sensor.ReportInterval < (double) sensor.Hysteresis / 60.0)
      sensor.Hysteresis = (sensor.ReportInterval * 60.0).ToLong();
    if (collection["Cal1"] != null)
      sensor.Calibration1 = string.IsNullOrEmpty(collection["Cal1"]) ? sensor.DefaultValue<long>("DefaultCalibration1") : (long) collection["Cal1"].ToInt();
    if (collection["Cal2"] != null)
      sensor.Calibration2 = string.IsNullOrEmpty(collection["Cal2"]) ? sensor.DefaultValue<long>("DefaultCalibration2") : (long) collection["Cal2"].ToInt();
    if (collection["Cal3"] != null)
      sensor.Calibration3 = string.IsNullOrEmpty(collection["Cal3"]) ? sensor.DefaultValue<long>("DefaultCalibration3") : collection["Cal3"].ToLong();
    if (collection["Cal4"] != null)
      sensor.Calibration4 = string.IsNullOrEmpty(collection["Cal4"]) ? sensor.DefaultValue<long>("DefaultCalibration4") : collection["Cal4"].ToLong();
    if (collection["Relay1Visibility"] != null)
    {
      if (collection["Relay1Visibility"] == "on")
      {
        BasicControl.ShowRelay1(sensor.SensorID);
        viewData["Relay1Visibility"] = "True";
      }
      else
      {
        BasicControl.HideRelay1(sensor.SensorID);
        viewData["Relay1Visibility"] = "False";
      }
    }
    string[] allKeys = collection.AllKeys;
    bool flag = false;
    foreach (string str in allKeys)
    {
      if (str.ToLower().Contains("datumname"))
        flag = true;
    }
    if (flag)
    {
      foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensor.SensorID))
      {
        if (sensorAttribute.Name.ToLower() == "relay1name")
          sensorAttribute.Delete();
      }
    }
    new BasicControl().SetSensorAttributes(sensor.SensorID);
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "14" : Math.Round((double) sensor.Hysteresis / 60.0, 2).ToString();
  }

  public static string Cal2ForUI(Sensor sensor)
  {
    return sensor.Calibration2 == (long) uint.MaxValue ? "14" : sensor.Calibration2.ToString();
  }

  public static string MinThreshForUI(Sensor sensor) => "Not Used";

  public static string MaxThreshForUI(Sensor sensor) => "Not Used";

  public static void Control(Sensor sensor, NameValueCollection collection)
  {
    int num1 = collection["State"].ToInt();
    bool flag = collection["Relay"].ToInt() == 1;
    int num2 = !string.IsNullOrEmpty(collection["Time"]) ? collection["Time"].ToInt() : 0;
    if (num2 < 0)
      num2 = 0;
    if (num2 > 43200)
      num2 = 43200;
    try
    {
      new NotificationRecorded()
      {
        SentToDeviceID = sensor.SensorID,
        QueueID = NotificationRecorded.NextQueueID(sensor.SensorID),
        NotificationDate = DateTime.UtcNow,
        NotificationType = eNotificationType.Control,
        SerializedRecipientProperties = $"{num1}|{num2}"
      }.Save();
      sensor.PendingActionControlCommand = true;
      sensor.Save();
      CSNet.SetGatewaysUrgentTrafficFlag(sensor.CSNetID);
    }
    catch
    {
    }
  }

  public static string CreateSerializedRecipientProperties(int state1, int time1)
  {
    if (time1 < 0)
      time1 = 0;
    if (time1 > (int) ushort.MaxValue)
      time1 = (int) ushort.MaxValue;
    return $"{state1}|{time1}";
  }

  public static void ParseSerializedRecipientProperties(
    string serialized,
    out int state1,
    out ushort time1)
  {
    state1 = 0;
    time1 = (ushort) 0;
    try
    {
      string[] strArray = serialized.Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      state1 = strArray[0].ToInt();
      int num = strArray[1].ToInt();
      if (num < 0)
        num = 0;
      if (num > (int) ushort.MaxValue)
        num = (int) ushort.MaxValue;
      time1 = Convert.ToUInt16(num);
    }
    catch
    {
    }
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    foreach (NotificationRecorded notificationRecorded in NotificationRecorded.LoadGetNoneDevliveredMessagesForControl(sensor.SensorID))
    {
      int state1;
      ushort time1;
      BasicControl.ParseSerializedRecipientProperties(notificationRecorded.SerializedRecipientProperties, out state1, out time1);
      numArrayList.Add(BasicControlBase.ControlFrame(sensor.SensorID, state1, time1, notificationRecorded.QueueID));
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
      if (new Version(sensor.FirmwareVersion) < new Version("2.3.0.0"))
      {
        foreach (NotificationRecorded notificationRecorded in NotificationRecorded.LoadNonAcknowledgedNotificationForDevice(sensor.SensorID))
        {
          notificationRecorded.Delivered = true;
          notificationRecorded.Status = "Sent";
          notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
          notificationRecorded.Save();
        }
      }
      else
      {
        int num1 = data.Length != 0 ? (int) data[0] : 0;
        bool flag = false;
        foreach (NotificationRecorded notificationRecorded in NotificationRecorded.LoadNonAcknowledgedNotificationForDevice(sensor.SensorID))
        {
          if (notificationRecorded.QueueID == num1)
          {
            flag = true;
            notificationRecorded.Delivered = true;
            notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
            notificationRecorded.Status = "Sent";
            notificationRecorded.Save();
            if (notificationRecorded.NotificationDate.AddHours(2.0) < DateTime.UtcNow && ConfigData.AppSettings("LogClearPendingExceptions", "False").ToBool())
            {
              string[] strArray = new string[6];
              strArray[0] = "Monnit.BasicControl.ClearPendingActionControlCommand- Delivered acknowledgment was over two hours old for NotificationRecordedID: ";
              long num2 = notificationRecorded.NotificationRecordedID;
              strArray[1] = num2.ToString();
              strArray[2] = " SensorID: ";
              num2 = sensor.SensorID;
              strArray[3] = num2.ToString();
              strArray[4] = " QueueID: ";
              strArray[5] = num1.ToString();
              new Exception(string.Concat(strArray)).Log();
            }
          }
        }
        if (!flag && ConfigData.AppSettings("LogClearPendingExceptions", "False").ToBool())
          new Exception($"Monnit.BasicControl.ClearPendingActionControlCommand- Delivered message not found for SensorID: {sensor.SensorID.ToString()} QueueID: {num1.ToString()}").Log();
      }
    }
    catch (Exception ex)
    {
      if (!ConfigData.AppSettings("LogClearPendingExceptions", "False").ToBool())
        return;
      ex.Log("Monnit.BasicControl.ClearPendingActionControlCommand(Sensor sensor, string version, int actionCommand, bool success, byte[] data)");
    }
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(BasicControl left, BasicControl right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(BasicControl left, BasicControl right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(BasicControl left, BasicControl right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(BasicControl left, BasicControl right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(BasicControl left, BasicControl right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(BasicControl left, BasicControl right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is BasicControl && this.Equals((MonnitApplicationBase) (obj as BasicControl));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is BasicControl && this.RelayState1 == (right as BasicControl).RelayState1;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is BasicControl && this.RelayState1 != (right as BasicControl).RelayState1;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<' cannot be applied to operands of type 'bool' and 'bool'");
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<=' cannot be applied to operands of type 'bool' and 'bool'");
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>' cannot be applied to operands of type 'bool' and 'bool'");
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>=' cannot be applied to operands of type 'bool' and 'bool'");
  }
}
