// Decompiled with JetBrains decompiler
// Type: Monnit.WestellPropane
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

public class WestellPropane : MonnitApplicationBase
{
  public static long MonnitApplicationID => 124;

  public static string ApplicationName => "Propane Tank Monitor";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => WestellPropane.MonnitApplicationID;

  public int Percentage { get; set; }

  public int Voltage { get; set; }

  public int stsState { get; set; }

  public static WestellPropane Create(byte[] sdm, int startIndex)
  {
    return new WestellPropane()
    {
      stsState = (int) sdm[startIndex - 1] >> 4,
      Percentage = (int) (sbyte) sdm[startIndex],
      Voltage = (int) BitConverter.ToUInt16(sdm, startIndex + 1)
    };
  }

  public override string Serialize()
  {
    return $"{this.Percentage.ToString()}|{this.Voltage.ToString()}|{this.stsState.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) WestellPropane.Deserialize(version, serialized);
  }

  public static WestellPropane Deserialize(string version, string serialized)
  {
    WestellPropane westellPropane = new WestellPropane();
    if (string.IsNullOrEmpty(serialized))
    {
      westellPropane.Percentage = 0;
      westellPropane.Voltage = 0;
      westellPropane.stsState = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      westellPropane.Percentage = strArray[0].ToInt();
      if (strArray.Length > 1)
      {
        westellPropane.Voltage = strArray[1].ToInt();
        try
        {
          westellPropane.stsState = strArray[2].ToInt();
        }
        catch
        {
          westellPropane.stsState = 0;
        }
      }
      else
      {
        westellPropane.Voltage = strArray[0].ToInt();
        westellPropane.stsState = 0;
      }
    }
    return westellPropane;
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.Percentage, "Percentage", this.Percentage),
        new AppDatum(eDatumType.Voltage, "Voltage", this.Voltage)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[2]
    {
      (object) this.Percentage,
      (object) this.Voltage
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[2]
    {
      "Percentage",
      "Voltage"
    });
  }

  public override object PlotValue
  {
    get
    {
      int percentage = this.Percentage;
      return true ? (object) this.Percentage : (object) null;
    }
  }

  public override string NotificationString
  {
    get
    {
      if (this.stsState == 1)
        return "Hardware Failure";
      if (this.Percentage > 80 /*0x50*/)
        return "Full";
      return this.Percentage < 8 ? "Empty" : this.Percentage.ToString() + "%";
    }
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
      ApplicationID = WestellPropane.MonnitApplicationID,
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
    {
      long num = collection["Hysteresis_Manual"].ToLong();
      if (num < 0L)
        num = 0L;
      if (num > 10L)
        num = 10L;
      sensor.Hysteresis = num;
    }
    if (string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
      return;
    long num1 = collection["MinimumThreshold_Manual"].ToLong();
    if (num1 < 10L)
      num1 = 0L;
    if (num1 > 75L)
      num1 = 75L;
    sensor.MinimumThreshold = num1;
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
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

  public static bool operator ==(WestellPropane left, WestellPropane right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(WestellPropane left, WestellPropane right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(WestellPropane left, WestellPropane right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(WestellPropane left, WestellPropane right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(WestellPropane left, WestellPropane right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(WestellPropane left, WestellPropane right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is WestellPropane && this.Equals((MonnitApplicationBase) (obj as WestellPropane));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is WestellPropane && this.Percentage == (right as WestellPropane).Percentage;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is WestellPropane && this.Percentage != (right as WestellPropane).Percentage;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is WestellPropane && this.Percentage < (right as WestellPropane).Percentage;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is WestellPropane && this.Percentage <= (right as WestellPropane).Percentage;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is WestellPropane && this.Percentage > (right as WestellPropane).Percentage;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is WestellPropane && this.Percentage >= (right as WestellPropane).Percentage;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 100;

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }
}
