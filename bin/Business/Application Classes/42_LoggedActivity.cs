// Decompiled with JetBrains decompiler
// Type: Monnit.LoggedActivity
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class LoggedActivity : MonnitApplicationBase
{
  public static long MonnitApplicationID => 42;

  public static string ApplicationName => "Activity - Timer";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => LoggedActivity.MonnitApplicationID;

  public int Count { get; set; }

  public int PastCount { get; set; }

  public override string Serialize()
  {
    int num = this.Count;
    string str1 = num.ToString();
    num = this.PastCount;
    string str2 = num.ToString();
    return $"{str1},{str2}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.Count, "Count", this.Count)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>() { "Minutes" };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) LoggedActivity.Deserialize(version, serialized);
  }

  public override string NotificationString => this.Count.ToString() + " Minutes";

  public override object PlotValue => (object) this.Count;

  public static LoggedActivity Deserialize(string version, string serialized)
  {
    LoggedActivity loggedActivity = new LoggedActivity();
    if (string.IsNullOrEmpty(serialized))
    {
      loggedActivity.Count = 0;
      loggedActivity.PastCount = 0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      loggedActivity.Count = strArray[0].ToInt();
      loggedActivity.PastCount = strArray.Length != 2 ? 0 : strArray[1].ToInt();
    }
    return loggedActivity;
  }

  public static LoggedActivity Create(byte[] sdm, int startIndex)
  {
    return new LoggedActivity()
    {
      Count = (int) BitConverter.ToUInt16(sdm, startIndex),
      PastCount = (int) BitConverter.ToUInt16(sdm, startIndex + 2)
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
      ApplicationID = LoggedActivity.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (string.IsNullOrEmpty(collection["Sensitivity"]))
      return;
    switch (Convert.ToInt32(collection["Sensitivity"]))
    {
      case 1:
        sensor.Calibration1 = 500L;
        sensor.Calibration3 = 2L;
        sensor.Calibration4 = 9L;
        break;
      case 2:
        sensor.Calibration1 = 1000L;
        sensor.Calibration3 = 2L;
        sensor.Calibration4 = 8L;
        break;
      case 4:
        sensor.Calibration1 = 4000L;
        sensor.Calibration3 = 2L;
        sensor.Calibration4 = 2L;
        break;
      default:
        sensor.Calibration1 = 2000L;
        sensor.Calibration3 = 2L;
        sensor.Calibration4 = 6L;
        break;
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

  public static bool operator ==(LoggedActivity left, LoggedActivity right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(LoggedActivity left, LoggedActivity right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(LoggedActivity left, LoggedActivity right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(LoggedActivity left, LoggedActivity right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(LoggedActivity left, LoggedActivity right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(LoggedActivity left, LoggedActivity right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is LoggedActivity && this.Equals((MonnitApplicationBase) (obj as LoggedActivity));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is LoggedActivity && this.Count == (right as LoggedActivity).Count;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is LoggedActivity && this.Count != (right as LoggedActivity).Count;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is LoggedActivity && this.Count < (right as LoggedActivity).Count;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is LoggedActivity && this.Count <= (right as LoggedActivity).Count;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is LoggedActivity && this.Count > (right as LoggedActivity).Count;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is LoggedActivity && this.Count >= (right as LoggedActivity).Count;
  }
}
