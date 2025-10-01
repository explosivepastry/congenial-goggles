// Decompiled with JetBrains decompiler
// Type: Monnit.WeightedActivity
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class WeightedActivity : MonnitApplicationBase
{
  public static long MonnitApplicationID => 60;

  public static string ApplicationName => "Weighted Activity";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => WeightedActivity.MonnitApplicationID;

  public bool Standby { get; set; }

  public bool Urgent { get; set; }

  public int Weight { get; set; }

  public int DLCount { get; set; }

  public override string Serialize()
  {
    return $"{this.Weight}|{this.Standby}|{this.DLCount}|{this.Urgent}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[4]
      {
        new AppDatum(eDatumType.Weight, "Weight", this.Weight),
        new AppDatum(eDatumType.BooleanData, "Standby", this.Standby),
        new AppDatum(eDatumType.BooleanData, "Urgent", this.Urgent),
        new AppDatum(eDatumType.Count, "DLCount", this.DLCount)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      this.PlotValue,
      (object) this.Standby,
      (object) this.Urgent,
      (object) this.DLCount
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) WeightedActivity.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      string str = !this.Standby ? this.Weight.ToString() : "Standby";
      return this.DLCount > 0 ? $"{str} Logged:{this.DLCount}" : str;
    }
  }

  public override object PlotValue => this.Standby ? (object) 0 : (object) this.Weight;

  public static WeightedActivity Deserialize(string version, string serialized)
  {
    WeightedActivity weightedActivity = new WeightedActivity();
    if (string.IsNullOrEmpty(serialized))
    {
      weightedActivity.Weight = 0;
    }
    else
    {
      string[] strArray = serialized.Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      weightedActivity.Weight = strArray[0].ToInt();
      weightedActivity.Standby = strArray.Length > 1 ? strArray[1].ToBool() : strArray[0].ToBool();
      weightedActivity.DLCount = strArray.Length > 2 ? strArray[2].ToInt() : strArray[0].ToInt();
      weightedActivity.Urgent = strArray.Length > 3 ? strArray[3].ToBool() : strArray[0].ToBool();
    }
    return weightedActivity;
  }

  public static WeightedActivity Create(byte[] sdm, int startIndex)
  {
    WeightedActivity weightedActivity = new WeightedActivity();
    weightedActivity.Weight = (int) sdm[startIndex];
    try
    {
      byte num = sdm[startIndex - 1];
      weightedActivity.Standby = ((int) num & 16 /*0x10*/) == 16 /*0x10*/;
      weightedActivity.Urgent = ((int) num & 32 /*0x20*/) == 32 /*0x20*/;
      weightedActivity.DLCount = (int) BitConverter.ToUInt16(sdm, startIndex + 1);
    }
    catch
    {
    }
    return weightedActivity;
  }

  public new static void DefaultConfigurationSettings(Sensor sensor)
  {
    sensor.ReportInterval = sensor.DefaultValue<double>("DefaultReportInterval");
    sensor.ActiveStateInterval = sensor.DefaultValue<double>("DefaultActiveStateInterval");
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
      ApplicationID = WeightedActivity.MonnitApplicationID,
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
    if (collection["Calval1_Manual"] != null && !string.IsNullOrEmpty(collection["Calval1_Manual"]))
      sensor.Calibration1 = collection["Calval1_Manual"].ToLong();
    if (sensor.Calibration1 < 20L)
      sensor.Calibration1 = 20L;
    if (sensor.Calibration1 > 600L)
      sensor.Calibration1 = 600L;
    if (collection["Calval2_Manual"] != null && !string.IsNullOrEmpty(collection["Calval2_Manual"]))
      sensor.Calibration2 = collection["Calval2_Manual"].ToLong();
    if (sensor.Calibration2 < 1L)
      sensor.Calibration2 = 1L;
    if (sensor.Calibration2 > 500L)
      sensor.Calibration2 = 500L;
    if (collection["Calval3_Manual"] != null && !string.IsNullOrEmpty(collection["Calval3_Manual"]))
      sensor.Calibration3 = collection["Calval3_Manual"].ToLong();
    if (sensor.Calibration3 < 10L)
      sensor.Calibration3 = 10L;
    if (sensor.Calibration3 <= 250L)
      return;
    sensor.Calibration3 = 250L;
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    switch (collection["command"].ToInt())
    {
      case 201:
        sensor.Hysteresis |= 16L /*0x10*/;
        break;
      case 202:
        sensor.Hysteresis |= 4L;
        sensor.Hysteresis &= 23L;
        break;
      case 203:
        sensor.Hysteresis |= 8L;
        sensor.Hysteresis &= 27L;
        break;
      case 204:
        sensor.Hysteresis |= 1L;
        sensor.Hysteresis &= 29L;
        break;
      case 205:
        sensor.Hysteresis |= 2L;
        sensor.Hysteresis &= 30L;
        break;
    }
    sensor.ProfileConfigDirty = false;
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if ((sensor.Hysteresis & 1L) == 1L)
      numArrayList.Add(WeightedActivityBase.SetStudyMode(sensor.SensorID, true));
    if ((sensor.Hysteresis & 2L) == 2L)
      numArrayList.Add(WeightedActivityBase.SetStudyMode(sensor.SensorID, false));
    if ((sensor.Hysteresis & 4L) == 4L)
      numArrayList.Add(WeightedActivityBase.SetDataLogDeliveryPriority(sensor.SensorID, true));
    if ((sensor.Hysteresis & 8L) == 8L)
      numArrayList.Add(WeightedActivityBase.SetDataLogDeliveryPriority(sensor.SensorID, false));
    if ((sensor.Hysteresis & 16L /*0x10*/) == 16L /*0x10*/)
      numArrayList.Add(WeightedActivityBase.ClearStoredDataFrame(sensor.SensorID));
    if (numArrayList.Count == 0)
    {
      sensor.PendingActionControlCommand = false;
      sensor.Save();
    }
    return numArrayList;
  }

  public new static void ClearPendingActionControlCommand(
    Sensor sensor,
    string version,
    int actionCommand,
    bool success,
    byte[] data)
  {
    try
    {
      switch (actionCommand)
      {
        case 201:
          sensor.Hysteresis &= 15L;
          break;
        case 202:
          sensor.Hysteresis &= 27L;
          sensor.MinimumThreshold |= 4L;
          sensor.MinimumThreshold &= 23L;
          break;
        case 203:
          sensor.Hysteresis &= 23L;
          sensor.MinimumThreshold |= 8L;
          sensor.MinimumThreshold &= 27L;
          break;
        case 204:
          sensor.Hysteresis &= 30L;
          sensor.MinimumThreshold |= 1L;
          sensor.MinimumThreshold &= 29L;
          break;
        case 205:
          sensor.Hysteresis &= 29L;
          sensor.MinimumThreshold |= 2L;
          sensor.MinimumThreshold &= 30L;
          break;
        default:
          sensor.Hysteresis = 0L;
          break;
      }
      sensor.ProfileConfigDirty = false;
      if (sensor.Hysteresis != 0L)
        return;
      sensor.PendingActionControlCommand = false;
    }
    catch (Exception ex)
    {
      if (!ConfigData.AppSettings("LogClearPendingExceptions", "False").ToBool())
        return;
      ex.Log("Monnit.Attention.ClearPendingActionControlCommand(Sensor sensor, string version, int actionCommand, bool success, byte[] data)");
    }
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : sensor.Hysteresis.ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : sensor.MinimumThreshold.ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : sensor.MaximumThreshold.ToString();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(WeightedActivity left, WeightedActivity right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(WeightedActivity left, WeightedActivity right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(WeightedActivity left, WeightedActivity right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(WeightedActivity left, WeightedActivity right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(WeightedActivity left, WeightedActivity right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(WeightedActivity left, WeightedActivity right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is WeightedActivity && this.Equals((MonnitApplicationBase) (obj as WeightedActivity));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is WeightedActivity && this.Weight == (right as WeightedActivity).Weight;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is WeightedActivity && this.Weight != (right as WeightedActivity).Weight;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is WeightedActivity && this.Weight < (right as WeightedActivity).Weight;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is WeightedActivity && this.Weight <= (right as WeightedActivity).Weight;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is WeightedActivity && this.Weight > (right as WeightedActivity).Weight;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is WeightedActivity && this.Weight >= (right as WeightedActivity).Weight;
  }
}
