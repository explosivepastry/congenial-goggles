// Decompiled with JetBrains decompiler
// Type: Monnit.Pressure_4to20ma
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

public class Pressure_4to20ma : MonnitApplicationBase
{
  public static long MonnitApplicationID => 41;

  public static string ApplicationName => "Pressure Meter";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => Pressure_4to20ma.MonnitApplicationID;

  public double Pressure { get; set; }

  public override string Serialize() => this.Pressure.ToString();

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.Pressure, "Pressure", this.Pressure)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>() { "PSI" };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Pressure_4to20ma.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get => $"{this.Pressure.ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture)} PSI";
  }

  public override object PlotValue => (object) this.Pressure;

  public static Pressure_4to20ma Deserialize(string version, string serialized)
  {
    return new Pressure_4to20ma()
    {
      Pressure = serialized.ToDouble()
    };
  }

  public static Pressure_4to20ma Create(byte[] sdm, int startIndex)
  {
    return new Pressure_4to20ma()
    {
      Pressure = BitConverter.ToUInt16(sdm, startIndex).ToDouble() / 10.0
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
      ApplicationID = Pressure_4to20ma.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static Dictionary<string, string> MeasurementScaleValue()
  {
    return PressureNPSI.NotificationScaleValues();
  }

  public static Dictionary<string, string> NotificationScaleValues()
  {
    return new Dictionary<string, string>()
    {
      {
        "PSI",
        "PSI"
      },
      {
        "atm",
        "atm"
      },
      {
        "bar",
        "bar"
      },
      {
        "kPA",
        "kPA"
      },
      {
        "Torr",
        "Torr"
      },
      {
        "Custom",
        "Custom"
      }
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
    if (new Version(sensor.FirmwareVersion) < new Version("2.2.0.0"))
    {
      sensor.Calibration4 = (long) (collection["maxPressure"].ToInt() * 10);
      if (sensor.Calibration4 < sensor.Calibration3)
        sensor.Calibration4 = sensor.Calibration3;
      if (sensor.Calibration4 > 50000L)
        sensor.Calibration4 = 50000L;
      sensor.Save();
    }
    else
    {
      bool flag = collection["Cal_Which"] == "low";
      int num = collection["calPressure"].ToInt() * 10;
      if (num < 0)
        num = 0;
      if (num > 5000)
        num = 5000;
      sensor.Calibration3 = !flag ? 4L : 3L;
      sensor.Calibration4 = (long) num;
      sensor.MarkClean(false);
      sensor.PendingActionControlCommand = true;
      sensor.Save();
    }
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
      Pressure_4to20maBase.getCalibrationValue(sensor.SensorID, (Pressure_4to20maBase.HighLowCalibration) sensor.Calibration3, sensor.Calibration4.ToInt()),
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

  public static string GetLabel() => "";

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : ((double) sensor.MaximumThreshold / 10.0).ToString();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Pressure_4to20ma left, Pressure_4to20ma right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Pressure_4to20ma left, Pressure_4to20ma right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Pressure_4to20ma left, Pressure_4to20ma right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Pressure_4to20ma left, Pressure_4to20ma right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Pressure_4to20ma left, Pressure_4to20ma right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Pressure_4to20ma left, Pressure_4to20ma right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Pressure_4to20ma && this.Equals((MonnitApplicationBase) (obj as Pressure_4to20ma));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Pressure_4to20ma && this.Pressure == (right as Pressure_4to20ma).Pressure;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Pressure_4to20ma && this.Pressure != (right as Pressure_4to20ma).Pressure;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Pressure_4to20ma && this.Pressure < (right as Pressure_4to20ma).Pressure;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Pressure_4to20ma && this.Pressure <= (right as Pressure_4to20ma).Pressure;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Pressure_4to20ma && this.Pressure > (right as Pressure_4to20ma).Pressure;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Pressure_4to20ma && this.Pressure >= (right as Pressure_4to20ma).Pressure;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 10000;

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
