// Decompiled with JetBrains decompiler
// Type: Monnit.Thermocouple
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

public class Thermocouple : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;

  public static long MonnitApplicationID => ThermocoupleBase.MonnitApplicationID;

  public static string ApplicationName => "Temperature";

  public static eApplicationProfileType ProfileType => TemperatureBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => Thermocouple.MonnitApplicationID;

  public double Temp { get; set; }

  public static Thermocouple Create(byte[] sdm, int startIndex)
  {
    return new Thermocouple()
    {
      Temp = BitConverter.ToInt16(sdm, startIndex).ToDouble() / 10.0
    };
  }

  public override string Serialize() => this.Temp.ToString();

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temp)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[1]
    {
      Thermocouple.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    });
  }

  public static Thermocouple Deserialize(string version, string serialized)
  {
    return new Thermocouple()
    {
      Temp = serialized.ToDouble()
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Thermocouple.Deserialize(version, serialized);
  }

  public static bool IsFahrenheit(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF" && sensorAttribute.Value == "C")
        return false;
    }
    return true;
  }

  public static void MakeFahrenheit(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        sensorAttribute.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void MakeCelsius(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
      {
        if (sensorAttribute.Value != "C")
        {
          sensorAttribute.Value = "C";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "CorF",
        Value = "C",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute CorF => this._CorF;

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
    }
  }

  public override object PlotValue
  {
    get
    {
      if (this.Temp <= -200.0 || this.Temp >= 1350.0)
        return (object) null;
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.Temp : (object) this.Temp.ToFahrenheit();
    }
  }

  public override bool IsValid => this.Temp > -250.0 && this.Temp < 1400.0;

  public override string NotificationString
  {
    get
    {
      if (this.Temp == -999.7)
        return "Hardware Failure (Empty)";
      if (this.Temp == -999.8)
        return "Hardware Failure (Short)";
      if (this.Temp == -999.9)
        return "No Sensor Detected";
      if (this.Temp == -1000.0)
        return "Hardware Failure (Short)";
      if (this.Temp <= -250.0 || this.Temp >= 1400.0)
        return "Invalid";
      return this.CorF != null && this.CorF.Value == "C" ? this.Temp.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture) : this.Temp.ToFahrenheit().ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture);
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
    sensor.MaximumNetworkHops = sensor.DefaultValue<int>("DefaultMaximumNetworkHops");
    sensor.MinimumCommunicationFrequency = (int) (sensor.ReportInterval * 2.0) + 10;
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
      CompareType = eCompareType.Less_Than,
      CompareValue = "0",
      NotificationClass = eNotificationClass.Application,
      ApplicationID = Thermocouple.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion,
      Scale = Thermocouple.IsFahrenheit(sensor.SensorID) ? "F" : "C"
    };
  }

  public static Dictionary<string, string> NotificationScaleValues()
  {
    return new Dictionary<string, string>()
    {
      {
        "F",
        "Fahrenheit"
      },
      {
        "C",
        "Celsius"
      }
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    bool flag = Thermocouple.IsFahrenheit(sensor.SensorID);
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
      double result;
      if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
        sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
      if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]) && double.TryParse(collection["Hysteresis_Manual"], out result))
        sensor.Hysteresis = (collection["Hysteresis_Manual"].ToDouble() * (flag ? 5.0 / 9.0 : 1.0) * 10.0).ToLong();
      if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) && double.TryParse(collection["MinimumThreshold_Manual"], out result))
      {
        sensor.MinimumThreshold = ((flag ? collection["MinimumThreshold_Manual"].ToDouble().ToCelsius() : collection["MinimumThreshold_Manual"].ToDouble()) * 10.0).ToLong();
        if (sensor.MinimumThreshold < -2000L)
          sensor.MinimumThreshold = -2000L;
        if (sensor.MinimumThreshold > 50000L)
          sensor.MinimumThreshold = 50000L;
      }
      if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) && double.TryParse(collection["MaximumThreshold_Manual"], out result))
      {
        sensor.MaximumThreshold = ((flag ? collection["MaximumThreshold_Manual"].ToDouble().ToCelsius() : collection["MaximumThreshold_Manual"].ToDouble()) * 10.0).ToLong();
        if (sensor.MaximumThreshold < -2000L)
          sensor.MaximumThreshold = -2000L;
        if (sensor.MaximumThreshold > 50000L)
          sensor.MaximumThreshold = 50000L;
        if (sensor.MinimumThreshold > sensor.MaximumThreshold)
          sensor.MaximumThreshold = sensor.MinimumThreshold;
      }
    }
  }

  public static string HystForUI(Sensor sensor)
  {
    double num = sensor.Hysteresis == (long) uint.MaxValue ? 0.0 : Convert.ToDouble(sensor.Hysteresis) / 10.0;
    return Temperature.IsFahrenheit(sensor.SensorID) ? (num * 1.8).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    double Celsius = sensor.MinimumThreshold == (long) uint.MaxValue ? Convert.ToDouble(sensor.DefaultValue<long>("DefaultMinimumThreshold")) / 10.0 : Convert.ToDouble(sensor.MinimumThreshold) / 10.0;
    return Temperature.IsFahrenheit(sensor.SensorID) ? Celsius.ToFahrenheit().ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : Celsius.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    double Celsius = sensor.MaximumThreshold == (long) uint.MaxValue ? Convert.ToDouble(sensor.DefaultValue<long>("DefaultMaximumThreshold")) / 10.0 : Convert.ToDouble(sensor.MaximumThreshold) / 10.0;
    return Temperature.IsFahrenheit(sensor.SensorID) ? Celsius.ToFahrenheit().ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : Celsius.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection returnData)
  {
    if (collection["TempScale"] == "on")
    {
      returnData["TempScale"] = "F";
      Thermocouple.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      returnData["TempScale"] = "C";
      Thermocouple.MakeCelsius(sensor.SensorID);
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    double num = collection["offset"].ToDouble();
    if (collection["tempScale"] == "F")
      num /= 1.8;
    if (num < -50.0)
      num = -50.0;
    if (num > 50.0)
      num = 50.0;
    sensor.Calibration1 = (num * 10.0).ToLong();
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
    target.EventDetectionType = source.EventDetectionType;
    target.EventDetectionPeriod = source.EventDetectionPeriod;
    target.EventDetectionCount = source.EventDetectionCount;
    target.RearmTime = source.RearmTime;
    target.BiStable = source.BiStable;
    target.TagString = source.TagString;
  }

  public static string getBasicOffset(Sensor sensor)
  {
    return (sensor.Calibration1.ToDouble() / 10.0).ToString("0.00");
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Thermocouple left, Thermocouple right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Thermocouple left, Thermocouple right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Thermocouple left, Thermocouple right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Thermocouple left, Thermocouple right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Thermocouple left, Thermocouple right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Thermocouple left, Thermocouple right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Thermocouple && this.Equals((MonnitApplicationBase) (obj as Thermocouple));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Thermocouple && this.Temp == (right as Thermocouple).Temp;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Thermocouple && this.Temp != (right as Thermocouple).Temp;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Thermocouple && this.Temp < (right as Thermocouple).Temp;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Thermocouple && this.Temp <= (right as Thermocouple).Temp;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Thermocouple && this.Temp > (right as Thermocouple).Temp;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Thermocouple && this.Temp >= (right as Thermocouple).Temp;
  }

  public static long DefaultMinThreshold => -1000;

  public static long DefaultMaxThreshold => 12000;

  public static string GetLabel(long sensorID) => "";
}
