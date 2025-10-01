// Decompiled with JetBrains decompiler
// Type: Monnit.SmartRanger
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class SmartRanger : MonnitApplicationBase
{
  public static long MonnitApplicationID => 45;

  public static string ApplicationName => "Range Extender";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "None";

  public override long ApplicationID => SmartRanger.MonnitApplicationID;

  public int DevCount { get; set; }

  public int RxMsgBySubnet { get; set; }

  public int UplinkFails { get; set; }

  public int MSGQueueCount { get; set; }

  public int SubNetChannel { get; set; }

  public override string Serialize()
  {
    return $"{this.DevCount.ToString()},{this.RxMsgBySubnet.ToString()},{this.UplinkFails.ToString()},{this.MSGQueueCount.ToString()},{this.SubNetChannel.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[5]
      {
        new AppDatum(eDatumType.Count, "DevCount", this.DevCount),
        new AppDatum(eDatumType.Count, "RxMsgBySubnet", this.RxMsgBySubnet),
        new AppDatum(eDatumType.Count, "UplinkFails", this.UplinkFails),
        new AppDatum(eDatumType.Count, "MSGQueueCount", this.MSGQueueCount),
        new AppDatum(eDatumType.Count, "SubNetChannel", this.SubNetChannel)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>()
    {
      "DevCount",
      "RxMsgBySubnet",
      "UplinkFails",
      "MSGQueueCount",
      "SubNetChannel"
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) SmartRanger.Deserialize(version, serialized);
  }

  public static SmartRanger Deserialize(string version, string serialized)
  {
    SmartRanger smartRanger = new SmartRanger();
    if (string.IsNullOrEmpty(serialized))
    {
      smartRanger.DevCount = 0;
      smartRanger.RxMsgBySubnet = 0;
      smartRanger.UplinkFails = 0;
      smartRanger.MSGQueueCount = 0;
      smartRanger.SubNetChannel = 0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      if (strArray.Length == 5)
      {
        smartRanger.DevCount = strArray[0].ToInt();
        smartRanger.RxMsgBySubnet = strArray[1].ToInt();
        smartRanger.UplinkFails = strArray[2].ToInt();
        smartRanger.MSGQueueCount = strArray[3].ToInt();
        smartRanger.SubNetChannel = strArray[4].ToInt();
      }
      else
      {
        smartRanger.DevCount = strArray[0].ToInt();
        smartRanger.RxMsgBySubnet = strArray[0].ToInt();
        smartRanger.UplinkFails = strArray[0].ToInt();
        smartRanger.MSGQueueCount = strArray[0].ToInt();
        smartRanger.SubNetChannel = strArray[0].ToInt();
      }
    }
    return smartRanger;
  }

  public override string NotificationString
  {
    get
    {
      return $"DCnt:{this.DevCount} Rxm:{this.RxMsgBySubnet} Fls:{this.UplinkFails} Qu:{this.MSGQueueCount} Chan:{this.SubNetChannel}";
    }
  }

  public override object PlotValue => (object) this.MSGQueueCount;

  public static SmartRanger Create(byte[] sdm, int startIndex)
  {
    return new SmartRanger()
    {
      DevCount = (int) BitConverter.ToUInt16(sdm, startIndex),
      RxMsgBySubnet = (int) BitConverter.ToUInt16(sdm, startIndex + 2),
      UplinkFails = (int) BitConverter.ToUInt16(sdm, startIndex + 4),
      MSGQueueCount = (int) BitConverter.ToUInt16(sdm, startIndex + 6),
      SubNetChannel = (int) sdm[startIndex + 8]
    };
  }

  public new static void DefaultConfigurationSettings(Sensor sensor)
  {
    if (sensor.ReportInterval < 1.0)
    {
      sensor.ReportInterval = 1.0;
      sensor.ActiveStateInterval = 1.0;
    }
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
      CompareValue = "60",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = SmartRanger.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
  }

  public static string HystForUI(Sensor sensor) => "Not Used";

  public static string MinThreshForUI(Sensor sensor) => "Not Used";

  public static string MaxThreshForUI(Sensor sensor) => "Not Used";

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(SmartRanger left, SmartRanger right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(SmartRanger left, SmartRanger right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(SmartRanger left, SmartRanger right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(SmartRanger left, SmartRanger right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(SmartRanger left, SmartRanger right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(SmartRanger left, SmartRanger right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is SmartRanger && this.Equals((MonnitApplicationBase) (obj as SmartRanger));
  }

  public override bool Equals(MonnitApplicationBase right) => false;

  public override bool NotEqual(MonnitApplicationBase right) => false;

  public override bool LessThan(MonnitApplicationBase right) => false;

  public override bool LessThanEqual(MonnitApplicationBase right) => false;

  public override bool GreaterThan(MonnitApplicationBase right) => false;

  public override bool GreaterThanEqual(MonnitApplicationBase right) => false;

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (!string.IsNullOrEmpty(collection["Reform"]))
      sensor.MeasurementsPerTransmission = 6;
    if (!string.IsNullOrEmpty(collection["SensorList"]))
      sensor.MeasurementsPerTransmission = 4;
    if (!string.IsNullOrEmpty(collection["EraseQue"]))
      sensor.MeasurementsPerTransmission = 3;
    sensor.ProfileConfigDirty = false;
    sensor.ProfileConfig2Dirty = false;
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      numArrayList.Add(SmartRangerBase.CalibrateFrame(sensor.SensorID, (double) sensor.MeasurementsPerTransmission));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public static bool IgnoreActiveStateInSimpleEditScreen => true;
}
