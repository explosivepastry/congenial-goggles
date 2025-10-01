// Decompiled with JetBrains decompiler
// Type: Monnit.TemperatureRTD
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

public class TemperatureRTD : Temperature
{
  public new static long MonnitApplicationID => 35;

  public new static string ApplicationName => "High Temperature";

  public new static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public new static bool IsFahrenheit(long sensorID) => Temperature.IsFahrenheit(sensorID);

  public new static void MakeFahrenheit(long sensorID) => Temperature.MakeFahrenheit(sensorID);

  public new static void MakeCelsius(long sensorID) => Temperature.MakeCelsius(sensorID);

  public override bool IsValid => this.Temp > -100.0 && this.Temp < 650.0;

  public override string NotificationString
  {
    get
    {
      if (this.Temp == -999.7)
        return "Hardware Failure (Empty)";
      if (this.Temp == -999.8)
        return "No Sensor Detected";
      if (this.Temp == -999.9)
        return "Hardware Failure (Short)";
      if (this.Temp == -1000.0)
        return "Short Detected, Reset Battery";
      if (this.Temp <= -100.0 || this.Temp >= 650.0)
        return "Invalid";
      return this.CorF != null && this.CorF.Value == "C" ? this.Temp.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture) : this.Temp.ToFahrenheit().ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  public override object PlotValue
  {
    get
    {
      if (this.Temp <= -100.0 || this.Temp >= 650.0)
        return (object) null;
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.Temp : (object) this.Temp.ToFahrenheit();
    }
  }

  public static TemperatureRTD Deserialize(string version, string serialized)
  {
    TemperatureRTD temperatureRtd = new TemperatureRTD();
    temperatureRtd.Temp = serialized.ToDouble();
    return temperatureRtd;
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.TemperatureData, "Temp", this.Temp)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>()
    {
      TemperatureRTD.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    };
  }

  public static TemperatureRTD Create(byte[] sdm, int startIndex)
  {
    TemperatureRTD temperatureRtd = new TemperatureRTD();
    temperatureRtd.Temp = BitConverter.ToInt16(sdm, startIndex).ToDouble() / 10.0;
    return temperatureRtd;
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

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      CompareValue = "30",
      NotificationClass = eNotificationClass.Application,
      ApplicationID = TemperatureRTD.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion,
      Scale = TemperatureRTD.IsFahrenheit(sensor.SensorID) ? "F" : "C"
    };
  }

  public new static Dictionary<string, string> NotificationScaleValues()
  {
    return Temperature.NotificationScaleValues();
  }

  public new static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    bool flag = Temperature.IsFahrenheit(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["BasicThreshold"]) && !string.IsNullOrEmpty(collection["BasicThresholdDirection"]) && collection["BasicThresholdDirection"] != "-1")
    {
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
      if (collection["BasicThresholdDirection"].ToInt() == 0)
      {
        sensor.MinimumThreshold = ((flag ? collection["BasicThreshold"].ToDouble().ToCelsius() : collection["BasicThreshold"].ToDouble()) * 10.0).ToLong();
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      }
      else if (collection["BasicThresholdDirection"].ToInt() == 1)
      {
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
        sensor.MaximumThreshold = ((flag ? collection["BasicThreshold"].ToDouble().ToCelsius() : collection["BasicThreshold"].ToDouble()) * 10.0).ToLong();
      }
      sensor.MeasurementsPerTransmission = 6;
    }
    else
    {
      if (string.IsNullOrEmpty(collection["Hysteresis_Manual"]) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
        sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
      if (string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
        sensor.Hysteresis = (collection["Hysteresis_Manual"].ToDouble() * (flag ? 5.0 / 9.0 : 1.0) * 10.0).ToLong();
      if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
      {
        sensor.MinimumThreshold = ((flag ? collection["MinimumThreshold_Manual"].ToDouble().ToCelsius() : collection["MinimumThreshold_Manual"].ToDouble()) * 10.0).ToLong();
        if (sensor.MinimumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
          sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
        if (sensor.MinimumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
          sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      }
      if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
      {
        sensor.MaximumThreshold = ((flag ? collection["MaximumThreshold_Manual"].ToDouble().ToCelsius() : collection["MaximumThreshold_Manual"].ToDouble()) * 10.0).ToLong();
        if (sensor.MaximumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
          sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
        if (sensor.MaximumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
          sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
        if (sensor.MinimumThreshold > sensor.MaximumThreshold)
          sensor.MaximumThreshold = sensor.MinimumThreshold;
      }
    }
  }

  public new static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (!sensor.IsWiFiSensor)
    {
      sensor.Calibration1 = !(collection["tempScale"] == "C") ? ((collection["actual"].ToDouble() - 32.0) * (5.0 / 9.0) * 10.0).ToLong() : (collection["actual"].ToDouble() * 10.0).ToLong();
      sensor.ProfileConfigDirty = false;
      sensor.ProfileConfig2Dirty = false;
      if (sensor.Calibration1 < sensor.DefaultValue<long>("DefaultMinimumThreshold") || sensor.Calibration1 > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
        throw new Exception("Calibration value out of range!");
      sensor.PendingActionControlCommand = true;
    }
    else if (sensor.LastDataMessage != null)
    {
      double num1 = sensor.LastDataMessage.Data.ToDouble();
      double num2 = !(collection["tempScale"] == "C") ? (collection["actual"].ToDouble() - 32.0) * (5.0 / 9.0) : collection["actual"].ToDouble();
      sensor.Calibration1 = ((num2 - num1) * 10.0).ToLong() + sensor.Calibration1;
    }
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
    target.Calibration3 = source.Calibration3;
    target.Calibration4 = source.Calibration4;
    target.EventDetectionType = source.EventDetectionType;
    target.EventDetectionPeriod = source.EventDetectionPeriod;
    target.EventDetectionCount = source.EventDetectionCount;
    target.RearmTime = source.RearmTime;
    target.BiStable = source.BiStable;
    target.TagString = source.TagString;
  }

  public new static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (collection["TempScale"] == "on")
    {
      viewData["TempScale"] = "F";
      TemperatureRTD.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      viewData["TempScale"] = "C";
      TemperatureRTD.MakeCelsius(sensor.SensorID);
    }
  }

  public new static string HystForUI(Sensor sensor)
  {
    double num = sensor.Hysteresis == (long) uint.MaxValue ? 0.0 : Convert.ToDouble(sensor.Hysteresis) / 10.0;
    return Temperature.IsFahrenheit(sensor.SensorID) ? (num * 1.8).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public new static string MinThreshForUI(Sensor sensor)
  {
    double Celsius = sensor.MinimumThreshold == (long) uint.MaxValue ? Convert.ToDouble(sensor.DefaultValue<long>("DefaultMinimumThreshold")) / 10.0 : Convert.ToDouble(sensor.MinimumThreshold) / 10.0;
    return Temperature.IsFahrenheit(sensor.SensorID) ? Celsius.ToFahrenheit().ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : Celsius.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public new static string MaxThreshForUI(Sensor sensor)
  {
    double Celsius = sensor.MaximumThreshold == (long) uint.MaxValue ? Convert.ToDouble(sensor.DefaultValue<long>("DefaultMaximumThreshold")) / 10.0 : Convert.ToDouble(sensor.MaximumThreshold) / 10.0;
    return Temperature.IsFahrenheit(sensor.SensorID) ? Celsius.ToFahrenheit().ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : Celsius.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    return Temperature.ActionControlCommand(sensor, version);
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(TemperatureRTD left, TemperatureRTD right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(TemperatureRTD left, TemperatureRTD right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(TemperatureRTD left, TemperatureRTD right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(TemperatureRTD left, TemperatureRTD right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(TemperatureRTD left, TemperatureRTD right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(TemperatureRTD left, TemperatureRTD right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public new static long DefaultMinThreshold => -1000;

  public new static long DefaultMaxThreshold => 4500;
}
