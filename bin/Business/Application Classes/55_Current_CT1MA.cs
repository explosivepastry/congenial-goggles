// Decompiled with JetBrains decompiler
// Type: Monnit.Current_CT1MA
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;

#nullable disable
namespace Monnit;

public class Current_CT1MA : MonnitApplicationBase
{
  public static long MonnitApplicationID => 55;

  public static string ApplicationName => "Current Meter - 1mA";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => Current_CT1MA.MonnitApplicationID;

  public double Current { get; set; }

  public override string Serialize() => this.Current.ToString();

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.Amps, "Current", this.Current)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>() { this.PlotValue };
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>() { "Amps" };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Current_CT1MA.Deserialize(version, serialized);
  }

  public override bool IsValid => true;

  public override string NotificationString
  {
    get => this.Current.ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture) + " Amps";
  }

  public override object PlotValue => (object) this.Current;

  public static Current_CT1MA Deserialize(string version, string serialized)
  {
    return new Current_CT1MA()
    {
      Current = serialized.ToDouble()
    };
  }

  public static Current_CT1MA Create(byte[] sdm, int startIndex)
  {
    return new Current_CT1MA()
    {
      Current = BitConverter.ToUInt16(sdm, startIndex).ToDouble() / 10.0
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
      ApplicationID = Current_CT1MA.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    double result;
    if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
      sensor.Hysteresis = (collection["Hysteresis_Manual"].ToDouble() * 10.0).ToLong();
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      sensor.MinimumThreshold = (collection["MinimumThreshold_Manual"].ToDouble() * 10.0).ToLong();
      if (sensor.MinimumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MinimumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      sensor.MaximumThreshold = (collection["MaximumThreshold_Manual"].ToDouble() * 10.0).ToLong();
      if (sensor.MaximumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MaximumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MinimumThreshold > sensor.MaximumThreshold)
        sensor.MaximumThreshold = sensor.MinimumThreshold;
    }
    if (!string.IsNullOrEmpty(collection["Calibration2_Manual"]))
    {
      long num = (collection["Calibration2_Manual"].ToDouble() * 10.0).ToLong();
      if (sensor.Calibration2 != num)
      {
        sensor.Calibration2 = num;
        sensor.Calibration1 = Math.Round((double) num * 1220.0 / 1200.0, 0).ToLong();
      }
    }
    if (string.IsNullOrEmpty(collection["BasicThreshold"]) || string.IsNullOrEmpty(collection["BasicThresholdDirection"]) || !(collection["BasicThresholdDirection"] != "-1"))
      return;
    if (collection["BasicThresholdDirection"].ToInt() == 0)
    {
      sensor.MinimumThreshold = (collection["BasicThreshold"].ToDouble() * 10.0).ToLong();
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    }
    else if (collection["BasicThresholdDirection"].ToInt() == 1)
    {
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      sensor.MaximumThreshold = (collection["BasicThreshold"].ToDouble() * 10.0).ToLong();
    }
    sensor.MeasurementsPerTransmission = 6;
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    sensor.Calibration1 = (collection["actual"].ToDouble() * 10.0).ToLong();
    sensor.ProfileConfigDirty = false;
    sensor.ProfileConfig2Dirty = false;
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static void DuplicateConfiguration(Sensor source, Sensor target)
  {
    target.ReportInterval = source.ReportInterval;
    target.ActiveStateInterval = source.ActiveStateInterval;
    target.MinimumCommunicationFrequency = source.MinimumCommunicationFrequency;
    target.ChannelMask = source.ChannelMask;
    target.StandardMessageDelay = source.StandardMessageDelay;
    target.TransmitIntervalLink = source.TransmitIntervalLink;
    target.TransmitIntervalTest = source.TransmitIntervalTest;
    target.TestTransmitCount = source.TestTransmitCount;
    target.MaximumNetworkHops = source.MaximumNetworkHops;
    target.RetryCount = source.RetryCount;
    target.Recovery = source.Recovery;
    target.MeasurementsPerTransmission = source.MeasurementsPerTransmission;
    target.TransmitOffset = source.TransmitOffset;
    target.Hysteresis = source.Hysteresis;
    target.MinimumThreshold = source.MinimumThreshold;
    target.MaximumThreshold = source.MaximumThreshold;
    target.Calibration2 = source.Calibration2;
    target.Calibration3 = source.Calibration3;
    target.Calibration4 = source.Calibration4;
    target.EventDetectionType = source.EventDetectionType;
    target.EventDetectionPeriod = source.EventDetectionPeriod;
    target.EventDetectionCount = source.EventDetectionCount;
    target.RearmTime = source.RearmTime;
    target.BiStable = source.BiStable;
    target.TagString = source.TagString;
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    return new List<byte[]>()
    {
      Current_CT1MABase.CalibrateFrame(sensor.SensorID, sensor.Calibration1.ToInt()),
      sensor.ReadProfileConfig(29)
    };
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : ((double) sensor.Hysteresis / 10.0).ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : ((double) sensor.MinimumThreshold / 10.0).ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : ((double) sensor.MaximumThreshold / 10.0).ToString();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Current_CT1MA left, Current_CT1MA right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Current_CT1MA left, Current_CT1MA right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Current_CT1MA left, Current_CT1MA right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Current_CT1MA left, Current_CT1MA right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Current_CT1MA left, Current_CT1MA right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Current_CT1MA left, Current_CT1MA right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Current_CT1MA && this.Equals((MonnitApplicationBase) (obj as Current_CT1MA));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Current_CT1MA && this.Current == (right as Current_CT1MA).Current;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Current_CT1MA && this.Current != (right as Current_CT1MA).Current;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Current_CT1MA && this.Current < (right as Current_CT1MA).Current;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Current_CT1MA && this.Current <= (right as Current_CT1MA).Current;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Current_CT1MA && this.Current > (right as Current_CT1MA).Current;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Current_CT1MA && this.Current >= (right as Current_CT1MA).Current;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 65000;

  public static string GetLabel(long sensorID) => "";
}
