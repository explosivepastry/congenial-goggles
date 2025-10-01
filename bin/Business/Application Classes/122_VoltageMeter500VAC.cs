// Decompiled with JetBrains decompiler
// Type: Monnit.VoltageMeter500VAC
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

public class VoltageMeter500VAC : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _LowValue;
  private SensorAttribute _HighValue;
  private SensorAttribute _Label;
  private SensorAttribute _showAdvCal;

  public static long MonnitApplicationID => 122;

  public static string ApplicationName => "Voltage Meter - 500 VAC";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => VoltageMeter500VAC.MonnitApplicationID;

  public double Voltage { get; set; }

  public override string Serialize() => this.Voltage.ToString();

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.Voltage, "Voltage", this.Voltage)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[1]
    {
      VoltageMeter500VAC.GetLabel(sensorID)
    });
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) VoltageMeter500VAC.Deserialize(version, serialized);
  }

  public static double GetLowValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "LowValue")
        return sensorAttribute.Value.ToDouble();
    }
    return 0.0;
  }

  public static void SetLowValue(long sensorID, double lowValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "LowValue")
      {
        sensorAttribute.Value = lowValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "LowValue",
        Value = lowValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static double GetHighValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HighValue")
        return sensorAttribute.Value.ToDouble();
    }
    return 500.0;
  }

  public static void SetHighValue(long sensorID, double highValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HighValue")
      {
        sensorAttribute.Value = highValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "HighValue",
        Value = highValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetLabel(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        return sensorAttribute.Value;
    }
    return "V";
  }

  public static void SetShowAdvCal(long sensorID, string showAdvCal)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == nameof (showAdvCal))
      {
        sensorAttribute.Value = showAdvCal;
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = nameof (showAdvCal),
        Value = showAdvCal,
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void SetLabel(long sensorID, string label)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
      {
        sensorAttribute.Value = label;
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "Label",
        Value = label,
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute LowValue
  {
    get
    {
      if (this._LowValue == null)
        this._LowValue = new SensorAttribute()
        {
          Value = "0",
          Name = nameof (LowValue)
        };
      return this._LowValue;
    }
  }

  public SensorAttribute HighValue
  {
    get
    {
      if (this._HighValue == null)
        this._HighValue = new SensorAttribute()
        {
          Value = "500",
          Name = nameof (HighValue)
        };
      return this._HighValue;
    }
  }

  public SensorAttribute Label
  {
    get
    {
      if (this._Label == null)
        this._Label = new SensorAttribute() { Value = "V" };
      return this._Label;
    }
  }

  public SensorAttribute showAdvCal
  {
    get
    {
      if (this._showAdvCal == null)
        this._showAdvCal = new SensorAttribute()
        {
          Value = "false"
        };
      return this._showAdvCal;
    }
  }

  public static bool ShowAdvCal(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "showAdvCal" && sensorAttribute.Value == "true")
        return true;
    }
    return false;
  }

  public static void SeeAdvCal(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "showAdvCal")
      {
        if (sensorAttribute.Value != "false")
        {
          sensorAttribute.Value = "true";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "showAdvCal",
        Value = "false",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "LowValue")
        this._LowValue = sensorAttribute;
      if (sensorAttribute.Name == "HighValue")
        this._HighValue = sensorAttribute;
      if (sensorAttribute.Name == "Label")
        this._Label = sensorAttribute;
      if (sensorAttribute.Name == "showAdvCal")
        this._showAdvCal = sensorAttribute;
    }
  }

  public override string NotificationString
  {
    get
    {
      return $"{((double) this.PlotValue).ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture)} {this.Label.Value}";
    }
  }

  public override object PlotValue
  {
    get
    {
      return (object) this.Voltage.LinearInterpolation(0.0, this.LowValue.Value.ToDouble(), 500.0, this.HighValue.Value.ToDouble());
    }
  }

  public static VoltageMeter500VAC Deserialize(string version, string serialized)
  {
    return new VoltageMeter500VAC()
    {
      Voltage = serialized.ToDouble()
    };
  }

  public static VoltageMeter500VAC Create(byte[] sdm, int startIndex)
  {
    return new VoltageMeter500VAC()
    {
      Voltage = BitConverter.ToUInt16(sdm, startIndex).ToDouble() / 10.0
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
      ApplicationID = VoltageMeter500VAC.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["lowValue"]))
      VoltageMeter500VAC.SetLowValue(sensor.SensorID, collection["lowValue"].ToDouble());
    if (!string.IsNullOrEmpty(collection["highValue"]))
      VoltageMeter500VAC.SetHighValue(sensor.SensorID, collection["highValue"].ToDouble());
    if (string.IsNullOrEmpty(collection["label"]))
      return;
    VoltageMeter500VAC.SetLabel(sensor.SensorID, collection["label"]);
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
    VoltageMeter500VAC voltageMeter500Vac = new VoltageMeter500VAC();
    voltageMeter500Vac.SetSensorAttributes(sensor.SensorID);
    double num1 = voltageMeter500Vac.LowValue.Value.ToDouble();
    double num2 = voltageMeter500Vac.HighValue.Value.ToDouble();
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
    {
      if (double.TryParse(collection["Hysteresis_Manual"], out result))
      {
        double num3 = Math.Abs((num2 - num1) / (double) (sensor.DefaultValue<long>("DefaultMaximumThreshold") - sensor.DefaultValue<long>("DefaultMinimumThreshold")));
        double num4 = collection["Hysteresis_Manual"].ToDouble();
        sensor.Hysteresis = num3 == 0.0 ? sensor.DefaultValue<long>("DefaultHysteresis") : (num4 / num3 * 10.0).ToLong() / 10L;
      }
      else
        sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    }
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      if (double.TryParse(collection["MinimumThreshold_Manual"], out result))
      {
        double x = collection["MinimumThreshold_Manual"].ToDouble();
        sensor.MinimumThreshold = x >= num1 ? (x <= num2 ? (x.LinearInterpolation(num1, 0.0, num2, 500.0) * 10.0).ToLong() : (num2.LinearInterpolation(num1, 0.0, num2, 500.0) * 10.0).ToLong()) : (num1.LinearInterpolation(num1, 0.0, num2, 500.0) * 10.0).ToLong();
      }
      else
        sensor.MinimumThreshold = (num1.LinearInterpolation(num1, 0.0, num2, 500.0) * 10.0).ToLong();
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      if (double.TryParse(collection["MaximumThreshold_Manual"], out result))
      {
        double x = collection["MaximumThreshold_Manual"].ToDouble();
        sensor.MaximumThreshold = x >= num1 ? (x <= num2 ? (x.LinearInterpolation(num1, 0.0, num2, 500.0) * 10.0).ToLong() : (num2.LinearInterpolation(num1, 0.0, num2, 500.0) * 10.0).ToLong()) : (num1.LinearInterpolation(num1, 0.0, num2, 500.0) * 10.0).ToLong();
        if (sensor.MinimumThreshold > sensor.MaximumThreshold)
          sensor.MaximumThreshold = sensor.MinimumThreshold;
      }
      else
        sensor.MaximumThreshold = (num2.LinearInterpolation(num1, 0.0, num2, 500.0) * 10.0).ToLong();
      if (!string.IsNullOrEmpty(collection["delay"]) && !string.IsNullOrEmpty(collection["power"]))
      {
        sensor.Calibration4 = collection["delay"].ToInt() >= 0 ? (collection["delay"].ToInt() <= 30000 ? collection["delay"].ToLong() : 30000L) : 0L;
        if (collection["power"].ToInt() == 0)
          sensor.Calibration3 = 0L;
        else if (collection["power"].ToInt() == 1)
          sensor.Calibration3 = 1L;
        else if (collection["power"].ToInt() == 2)
          sensor.Calibration3 = 2L;
        else if (collection["power"].ToInt() == 3)
          sensor.Calibration3 = 3L;
      }
      if (collection["showAdvCal"] == "on")
      {
        viewData["showAdvCal"] = "true";
        Analog.SetShowAdvCal(sensor.SensorID, "true");
      }
      else
      {
        viewData["showAdvCal"] = "false";
        Analog.SetShowAdvCal(sensor.SensorID, "false");
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

  public static void SetVoltOffset(Sensor sens, double value)
  {
    uint num = Convert.ToUInt32(sens.Calibration1) & 16777215U /*0xFFFFFF*/;
    sens.Calibration1 = (long) (num | (uint) (((int) (uint) value & (int) byte.MaxValue) << 24));
  }

  public static int GetVoltOffset(Sensor sens)
  {
    return (int) (Convert.ToUInt32(sens.Calibration1 & 4278190080L /*0xFF000000*/) >> 24);
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (sensor.SensorTypeID == 4L || !(new Version(sensor.FirmwareVersion) > new Version("2.0.0.0")))
      return;
    int num1 = (collection["observed"].ToDouble().LinearInterpolation(VoltageMeter500VAC.GetLowValue(sensor.SensorID), 0.0, VoltageMeter500VAC.GetHighValue(sensor.SensorID), 550.0) * 100.0).ToInt();
    int num2 = (collection["actual"].ToDouble().LinearInterpolation(VoltageMeter500VAC.GetLowValue(sensor.SensorID), 0.0, VoltageMeter500VAC.GetHighValue(sensor.SensorID), 550.0) * 100.0).ToInt();
    if (num1 > 4 && num2 > 0)
    {
      if (new Version(sensor.FirmwareVersion).Revision < 3)
      {
        sensor.Calibration1 = sensor.Calibration1 * (long) num2 / (long) num1;
      }
      else
      {
        int voltOffset = VoltageMeter500VAC.GetVoltOffset(sensor);
        sensor.Calibration1 = sensor.Calibration1 * (long) num2 / (long) num1 - (long) voltOffset;
      }
      sensor.ProfileConfigDirty = true;
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
    VoltageMeter500VAC voltageMeter500Vac = new VoltageMeter500VAC();
    voltageMeter500Vac.SetSensorAttributes(sensor.SensorID);
    double num1 = voltageMeter500Vac.LowValue.Value.ToDouble();
    double num2 = Math.Abs((voltageMeter500Vac.HighValue.Value.ToDouble() - num1) / (double) (sensor.DefaultValue<long>("DefaultMaximumThreshold") - sensor.DefaultValue<long>("DefaultMinimumThreshold")));
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : ((double) sensor.Hysteresis * num2).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    VoltageMeter500VAC voltageMeter500Vac = new VoltageMeter500VAC();
    voltageMeter500Vac.SetSensorAttributes(sensor.SensorID);
    double lowY = voltageMeter500Vac.LowValue.Value.ToDouble();
    double highY = voltageMeter500Vac.HighValue.Value.ToDouble();
    return (sensor.MinimumThreshold.ToDouble().LinearInterpolation(0.0, lowY, 500.0, highY) / 10.0).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    VoltageMeter500VAC voltageMeter500Vac = new VoltageMeter500VAC();
    voltageMeter500Vac.SetSensorAttributes(sensor.SensorID);
    double lowY = voltageMeter500Vac.LowValue.Value.ToDouble();
    double highY = voltageMeter500Vac.HighValue.Value.ToDouble();
    return (sensor.MaximumThreshold.ToDouble().LinearInterpolation(0.0, lowY, 500.0, highY) / 10.0).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(VoltageMeter500VAC left, VoltageMeter500VAC right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(VoltageMeter500VAC left, VoltageMeter500VAC right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(VoltageMeter500VAC left, VoltageMeter500VAC right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(VoltageMeter500VAC left, VoltageMeter500VAC right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(VoltageMeter500VAC left, VoltageMeter500VAC right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(VoltageMeter500VAC left, VoltageMeter500VAC right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is VoltageMeter500VAC && this.Equals((MonnitApplicationBase) (obj as VoltageMeter500VAC));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is VoltageMeter500VAC && this.Voltage == (right as VoltageMeter500VAC).Voltage;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is VoltageMeter500VAC && this.Voltage != (right as VoltageMeter500VAC).Voltage;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is VoltageMeter500VAC && this.Voltage < (right as VoltageMeter500VAC).Voltage;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is VoltageMeter500VAC && this.Voltage <= (right as VoltageMeter500VAC).Voltage;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is VoltageMeter500VAC && this.Voltage > (right as VoltageMeter500VAC).Voltage;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is VoltageMeter500VAC && this.Voltage >= (right as VoltageMeter500VAC).Voltage;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 500;

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }

  public new static NotificationScaleInfoModel GetScalingInfo(Sensor sens)
  {
    NotificationScaleInfoModel scalingInfo = new NotificationScaleInfoModel();
    VoltageMeter500VAC voltageMeter500Vac = new VoltageMeter500VAC();
    voltageMeter500Vac.SetSensorAttributes(sens.SensorID);
    scalingInfo.Label = voltageMeter500Vac.Label.Value.ToStringSafe();
    scalingInfo.profileLow = voltageMeter500Vac.LowValue.Value.ToDouble();
    scalingInfo.profileHigh = voltageMeter500Vac.HighValue.Value.ToDouble();
    scalingInfo.baseLow = 0.0;
    scalingInfo.baseHigh = 500.0;
    return scalingInfo;
  }
}
