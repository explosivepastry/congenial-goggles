// Decompiled with JetBrains decompiler
// Type: Monnit.Pressure50PSI
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

public class Pressure50PSI : PressureNPSI, ISensorAttribute
{
  public new static long MonnitApplicationID => 79;

  public new static string ApplicationName => "Pressure 50 PSI";

  public new static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override long ApplicationID => Pressure50PSI.MonnitApplicationID;

  public static Pressure50PSI Deserialize(string version, string serialized)
  {
    Pressure50PSI pressure50Psi = new Pressure50PSI();
    pressure50Psi.Pressure = serialized.ToDouble();
    return pressure50Psi;
  }

  public static Pressure50PSI Create(byte[] sdm, int startIndex)
  {
    Pressure50PSI pressure50Psi = new Pressure50PSI();
    pressure50Psi.Pressure = BitConverter.ToUInt16(sdm, startIndex).ToDouble() / 10.0;
    return pressure50Psi;
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
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }

  public new static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (string.IsNullOrEmpty(collection["label"]))
      return;
    long savedValue = Pressure50PSIBase.GetDefaults(new Version(sensor.FirmwareVersion), sensor.GenerationType)["DefaultMaximumThreshold"].ToLong() / 10L;
    switch (collection["label"])
    {
      case "atm":
        PressureNPSI.SetLabel(sensor.SensorID, "atm");
        PressureNPSI.SetTransform(sensor.SensorID, 0.068);
        PressureNPSI.SetSavedValue(sensor.SensorID, (double) savedValue * 0.068);
        break;
      case "bar":
        PressureNPSI.SetLabel(sensor.SensorID, "bar");
        PressureNPSI.SetTransform(sensor.SensorID, 0.0689);
        PressureNPSI.SetSavedValue(sensor.SensorID, (double) savedValue * 0.0689);
        break;
      case "kPA":
        PressureNPSI.SetLabel(sensor.SensorID, "kPA");
        PressureNPSI.SetTransform(sensor.SensorID, 6.89);
        PressureNPSI.SetSavedValue(sensor.SensorID, (double) savedValue * 6.89);
        break;
      case "Torr":
        PressureNPSI.SetLabel(sensor.SensorID, "Torr");
        PressureNPSI.SetTransform(sensor.SensorID, 51.71);
        PressureNPSI.SetSavedValue(sensor.SensorID, (double) savedValue * 51.71);
        break;
      case "Custom":
        PressureNPSI.SetLowValue(sensor.SensorID, collection["lowValue"].ToDouble());
        PressureNPSI.SetHighValue(sensor.SensorID, collection["highValue"].ToDouble());
        PressureNPSI.SetLabel(sensor.SensorID, collection["label"]);
        PressureNPSI.SetCustomLabel(sensor.SensorID, collection["customLabel"]);
        PressureNPSI.SetDecimalTrunkValue(sensor.SensorID, collection["decimalTrunkValue"].ToDouble());
        PressureNPSI.SetSavedValue(sensor.SensorID, (double) savedValue);
        break;
      default:
        PressureNPSI.SetLabel(sensor.SensorID, "PSI");
        PressureNPSI.SetTransform(sensor.SensorID, 1.0);
        PressureNPSI.SetSavedValue(sensor.SensorID, (double) savedValue);
        break;
    }
  }

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

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[1]
    {
      this.PlotValue
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[1]
    {
      PressureNPSI.GetLabel(sensorID)
    });
  }

  public new static Dictionary<string, string> MeasurementScaleValue()
  {
    return Pressure50PSI.NotificationScaleValues();
  }

  public new static Dictionary<string, string> NotificationScaleValues()
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

  public override object PlotValue
  {
    get
    {
      if (this.Label == null)
      {
        if (this.TransformValue != null && this.TransformValue.Value != "0")
          return (object) (this.Pressure * this.TransformValue.Value.ToDouble());
      }
      else
      {
        if (!(this.Label.Value.ToLower() != "custom"))
          return (object) this.Pressure.LinearInterpolation(0.0, this.LowValue.Value.ToDouble(), 50.0, this.HighValue(50L).Value.ToDouble());
        if (this.TransformValue != null && this.TransformValue.Value != "0")
          return (object) (this.Pressure * this.TransformValue.Value.ToDouble());
      }
      return (object) this.Pressure;
    }
  }

  public new static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    sensor.DefaultValue<long>("DefaultHysteresis");
    Dictionary<string, object> defaults = sensor.Defaults;
    string label = PressureNPSI.GetLabel(sensor.SensorID);
    double result1 = 0.0;
    double savedValue = PressureNPSI.GetSavedValue(sensor.SensorID);
    double transform = PressureNPSI.GetTransform(sensor.SensorID);
    double lowX = 0.0;
    double highX = 0.0;
    int num1 = PressureNPSI.GetDecimalTrunkValue(sensor.SensorID).ToInt();
    if (label.ToLower() == "custom")
    {
      Pressure50PSI pressure50Psi = new Pressure50PSI();
      pressure50Psi.SetSensorAttributes(sensor.SensorID);
      lowX = pressure50Psi.LowValue.Value.ToDouble();
      highX = pressure50Psi.HighValue((long) (sensor.DefaultValue<long>("DefaultCalibration4") / 10L).ToInt()).Value.ToDouble();
    }
    if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result1)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result1)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result1)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]) && double.TryParse(collection["Hysteresis_Manual"], out result1))
    {
      if (label.ToLower() != "custom")
      {
        double num2 = result1 / transform;
        sensor.Hysteresis = (num2 * 10.0).ToLong();
      }
      else
      {
        double o = Math.Round(collection["Hysteresis_Manual"].ToDouble().LinearInterpolation(lowX, 0.0, highX, savedValue) * Math.Pow(10.0, (double) (num1 + 1)));
        sensor.Hysteresis = o.ToLong();
      }
    }
    double highY = 50.0;
    double num3 = 0.0;
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) && double.TryParse(collection["MinimumThreshold_Manual"], out result1))
    {
      num3 = !(label.ToLower() != "custom") ? result1.LinearInterpolation(lowX, 0.0, highX, highY) : result1 / transform;
      if (num3 < 0.0)
        num3 = 0.0;
      if (num3 > highY)
        num3 = highY;
      sensor.MinimumThreshold = (num3 * 10.0).ToLong();
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) && double.TryParse(collection["MaximumThreshold_Manual"], out result1))
    {
      double num4 = !(label.ToLower() != "custom") ? result1.LinearInterpolation(lowX, 0.0, highX, highY) : result1 / transform;
      if (num4 < 0.0)
        num4 = 0.0;
      if (num4 > highY)
        num4 = highY;
      if (num3 > num4)
        num4 = num3;
      sensor.MaximumThreshold = (num4 * 10.0).ToLong();
    }
    int result2 = 0;
    if (!string.IsNullOrWhiteSpace(collection["delay"]) && int.TryParse(collection["delay"], out result2))
    {
      int num5 = result2;
      if (num5 < 0)
        num5 = 0;
      if (num5 > 60000)
        num5 = 60000;
      Pressure50PSI.SetStabalizationDelay(sensor, num5);
    }
    if (string.IsNullOrEmpty(collection["BasicThreshold"]) || string.IsNullOrEmpty(collection["BasicThresholdDirection"]) || !(collection["BasicThresholdDirection"] != "-1"))
      return;
    double num6 = collection["BasicThreshold"].ToDouble() / transform;
    if (collection["BasicThresholdDirection"].ToInt() == 0)
    {
      sensor.MinimumThreshold = (num6 * 10.0).ToLong();
      sensor.MaximumThreshold = savedValue.ToLong();
    }
    else if (collection["BasicThresholdDirection"].ToInt() == 1)
    {
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      sensor.MaximumThreshold = (num6 * 10.0).ToLong();
    }
    sensor.MeasurementsPerTransmission = 6;
  }

  public new static void SetStabalizationDelay(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration3) & 4294901760U;
    sensor.Calibration3 = (long) (num | (uint) (value & (int) ushort.MaxValue));
  }

  public new static int GetStabalizationDelay(Sensor sensor)
  {
    return (int) Convert.ToUInt32(sensor.Calibration3) & (int) ushort.MaxValue;
  }

  public new static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    PressureNPSI.CalibrateSensor(sensor, collection);
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    return PressureNPSI.ActionControlCommand(sensor, version);
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

  public new static string HystForUI(Sensor sensor) => PressureNPSI.HystForUI(sensor);

  public new static string MinThreshForUI(Sensor sensor) => PressureNPSI.MinThreshForUI(sensor);

  public new static string MaxThreshForUI(Sensor sensor) => PressureNPSI.MaxThreshForUI(sensor);

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Pressure50PSI left, Pressure50PSI right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Pressure50PSI left, Pressure50PSI right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Pressure50PSI left, Pressure50PSI right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Pressure50PSI left, Pressure50PSI right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Pressure50PSI left, Pressure50PSI right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Pressure50PSI left, Pressure50PSI right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Pressure50PSI && this.Equals((MonnitApplicationBase) (obj as Pressure50PSI));
  }

  public new static long DefaultMinThreshold => 0;

  public new static long DefaultMaxThreshold => 500;

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    PressureNPSI.DefaultCalibrationSettings(sensor);
    sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
  }

  public new static NotificationScaleInfoModel GetScalingInfo(Sensor sens)
  {
    NotificationScaleInfoModel scalingInfo = new NotificationScaleInfoModel();
    PressureNPSI pressureNpsi = new PressureNPSI();
    pressureNpsi.SetSensorAttributes(sens.SensorID);
    scalingInfo.Label = pressureNpsi.Label.Value.ToStringSafe();
    if (scalingInfo.Label == "Custom")
    {
      scalingInfo.CustomLabel = pressureNpsi.CustomLabel.Value.ToStringSafe();
      scalingInfo.profileLow = pressureNpsi.LowValue.Value.ToDouble();
      scalingInfo.profileHigh = pressureNpsi.HighValue((long) (sens.DefaultValue<long>("DefaultCalibration4") / 10L).ToInt()).Value.ToDouble();
      scalingInfo.baseLow = 0.0;
      scalingInfo.baseHigh = 50.0;
    }
    else
      scalingInfo.transformValue = pressureNpsi.TransformValue.Value.ToDouble();
    return scalingInfo;
  }
}
