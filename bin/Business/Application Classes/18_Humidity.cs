// Decompiled with JetBrains decompiler
// Type: Monnit.Humidity
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

public class Humidity : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;
  private SensorAttribute _D;

  public static long MonnitApplicationID => 18;

  public static string ApplicationName => nameof (Humidity);

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => Humidity.MonnitApplicationID;

  public double Hum { get; set; }

  public double Temperature { get; set; }

  public override string Serialize()
  {
    double num = this.Hum;
    string str1 = num.ToString();
    num = this.Temperature;
    string str2 = num.ToString();
    return $"{str1},{str2}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.Percentage, nameof (Humidity), this.Hum),
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temperature)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[2]
    {
      this.PlotValue,
      this.PlotTemperatureValue
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[2]
    {
      nameof (Humidity),
      Humidity.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    });
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Humidity.Deserialize(version, serialized);
  }

  public static bool IsFahrenheit(long sensorID) => Monnit.Temperature.IsFahrenheit(sensorID);

  public static void MakeFahrenheit(long sensorID) => Monnit.Temperature.MakeFahrenheit(sensorID);

  public static void MakeCelsius(long sensorID) => Monnit.Temperature.MakeCelsius(sensorID);

  public SensorAttribute CorF => this._CorF;

  public static bool ShowDewpoint(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "D" && sensorAttribute.Value == "T")
        return true;
    }
    return false;
  }

  public static void MakeDewpointVisible(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "D")
      {
        if (sensorAttribute.Value != "F")
        {
          sensorAttribute.Value = "T";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "D",
        Value = "T",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void HideDewpoint(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "D")
        sensorAttribute.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute D => this._D;

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
      if (sensorAttribute.Name == "D")
        this._D = sensorAttribute;
    }
  }

  public override bool IsValid => true;

  public override string NotificationString
  {
    get
    {
      string empty = string.Empty;
      string str1 = string.Empty;
      double num;
      string str2;
      if (this.CorF != null && this.CorF.Value == "C")
      {
        num = this.Temperature;
        str2 = num.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
        if (this.D != null && this.D.Value == "T")
        {
          num = Humidity.Dewpoint(this.Hum, this.Temperature);
          str1 = $" D: {num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture)}° C";
        }
      }
      else
      {
        str2 = this.Temperature.ToFahrenheit().ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture);
        if (this.D != null && this.D.Value == "T")
        {
          num = Humidity.Dewpoint(this.Hum, this.Temperature).ToFahrenheit();
          str1 = $" D: {num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture)}° F";
        }
      }
      num = this.Hum;
      return $"{num.ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture)}% @ {str2}{str1}";
    }
  }

  public override object PlotValue
  {
    get => this.Hum > -100.0 && this.Hum < 300.0 ? (object) this.Hum : (object) null;
  }

  public object PlotTemperatureValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.Temperature : (object) this.Temperature.ToFahrenheit();
    }
  }

  public static Humidity Deserialize(string version, string serialized)
  {
    Humidity humidity = new Humidity();
    if (string.IsNullOrEmpty(serialized))
    {
      humidity.Hum = 0.0;
      humidity.Temperature = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      humidity.Hum = strArray[0].ToDouble();
      humidity.Temperature = strArray.Length <= 1 ? strArray[0].ToDouble() : strArray[1].ToDouble();
    }
    return humidity;
  }

  public static Humidity Create(byte[] sdm, int startIndex)
  {
    Humidity humidity = new Humidity();
    double temperatureTicks = BitConverter.ToInt16(sdm, startIndex).ToDouble();
    double humidityTicks = BitConverter.ToInt16(sdm, startIndex + 2).ToDouble();
    humidity.Temperature = Humidity.CalcTemperature(temperatureTicks);
    humidity.Hum = Humidity.CalcHumidity(humidityTicks, humidity.Temperature);
    return humidity;
  }

  public static double CalcHumidity(double humidityTicks, double temperatureC)
  {
    double num1 = humidityTicks;
    double num2 = -2.8E-06 * num1 * num1 + 0.0405 * num1 - 4.0;
    double num3 = (temperatureC - 25.0) * (0.01 + 8E-05 * num1) + num2;
    if (num3 > 100.0)
      num3 = 100.0;
    if (num3 < 0.1)
      num3 = 0.1;
    return num3;
  }

  public static double CalcTemperature(double temperatureTicks) => temperatureTicks * 0.01 - 40.0;

  public static double Dewpoint(double Humidity, double Temperature)
  {
    double num = 0.66077 + 7.5 * Temperature / (237.3 + Temperature) + (Math.Log10(Humidity) - 2.0);
    return (num - 0.66077) * 237.3 / (8.16077 - num);
  }

  public static double CalcSimpleHumidity(double humidityTicks)
  {
    double num = humidityTicks;
    return -2.8E-06 * num * num + 0.0405 * num - 4.0;
  }

  public static double CalcSimpleHumidityTicks(double humidity)
  {
    double num1 = (Math.Sqrt(0.00164025 - -1.12E-05 * (-4.0 - humidity)) - 0.0405) / -5.6E-06;
    double num2 = (-0.0405 - Math.Sqrt(0.00164025 - -1.12E-05 * (-4.0 - humidity))) / -5.6E-06;
    return num1 < num2 ? num1 : num2;
  }

  public static double CalcTemperatureTicks(double temperature) => temperature * 100.0 + 4000.0;

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

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (collection["TempScale"] == "on")
    {
      viewData["TempScale"] = "F";
      Humidity.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      viewData["TempScale"] = "C";
      Humidity.MakeCelsius(sensor.SensorID);
    }
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = Humidity.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
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
      sensor.Hysteresis = (Humidity.CalcSimpleHumidityTicks(collection["Hysteresis_Manual"].ToDouble()).ToLong() << 16 /*0x10*/).ToLong();
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      sensor.MinimumThreshold = (Humidity.CalcSimpleHumidityTicks(collection["MinimumThreshold_Manual"].ToDouble()).ToLong() << 16 /*0x10*/).ToLong() + (long) Convert.ToUInt16(Humidity.CalcTemperatureTicks(-40.0));
      if (sensor.MinimumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MinimumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      sensor.MaximumThreshold = (Humidity.CalcSimpleHumidityTicks(collection["MaximumThreshold_Manual"].ToDouble()).ToLong() << 16 /*0x10*/).ToLong() + (long) Convert.ToUInt16(Humidity.CalcTemperatureTicks(120.0));
      sensor.MaximumThreshold += (long) short.MaxValue;
      if (sensor.MaximumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MaximumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MinimumThreshold > sensor.MaximumThreshold)
        sensor.MaximumThreshold = sensor.MinimumThreshold;
    }
    if (!string.IsNullOrEmpty(collection["BasicThreshold"]) && !string.IsNullOrEmpty(collection["BasicThresholdDirection"]) && collection["BasicThresholdDirection"] != "-1")
    {
      if (collection["BasicThresholdDirection"].ToInt() == 0)
      {
        sensor.MinimumThreshold = (Humidity.CalcSimpleHumidityTicks(collection["BasicThreshold"].ToDouble()).ToLong() << 16 /*0x10*/).ToLong() + (long) Convert.ToUInt16(Humidity.CalcTemperatureTicks(-40.0));
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      }
      else if (collection["BasicThresholdDirection"].ToInt() == 1)
      {
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
        sensor.MaximumThreshold = (Humidity.CalcSimpleHumidityTicks(collection["BasicThreshold"].ToDouble()).ToLong() << 16 /*0x10*/).ToLong() + (long) Convert.ToUInt16(Humidity.CalcTemperatureTicks(120.0));
      }
      sensor.MaximumThreshold += (long) short.MaxValue;
      sensor.MeasurementsPerTransmission = 6;
    }
    if (collection["ShowDewpoint"] == "on")
    {
      viewData["ShowDewpoint"] = "true";
      Humidity.MakeDewpointVisible(sensor.SensorID);
    }
    else
    {
      viewData["ShowDewpoint"] = "false";
      Humidity.HideDewpoint(sensor.SensorID);
    }
  }

  public static string HystForUI(Sensor sensor)
  {
    int num = Math.Round(Humidity.CalcSimpleHumidity((double) (sensor.Hysteresis >> 16 /*0x10*/)), 0).ToInt();
    if (num < 0)
      num = 0;
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : num.ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    int num = Math.Round(Humidity.CalcSimpleHumidity((double) (sensor.MinimumThreshold >> 16 /*0x10*/)), 0).ToInt();
    if (num < 0)
      num = 0;
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : num.ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    int num = Math.Round(Humidity.CalcSimpleHumidity((double) (sensor.MaximumThreshold >> 16 /*0x10*/)), 0).ToInt();
    if (num < 0)
      num = 0;
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : num.ToString();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Humidity left, Humidity right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Humidity left, Humidity right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Humidity left, Humidity right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Humidity left, Humidity right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Humidity left, Humidity right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Humidity left, Humidity right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Humidity && this.Equals((MonnitApplicationBase) (obj as Humidity));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Humidity && this.Hum == (right as Humidity).Hum;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Humidity && this.Hum != (right as Humidity).Hum;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Humidity && this.Hum < (right as Humidity).Hum;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Humidity && this.Hum <= (right as Humidity).Hum;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Humidity && this.Hum > (right as Humidity).Hum;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Humidity && this.Hum >= (right as Humidity).Hum;
  }

  public static long DefaultMinThreshold => 6488064 /*0x630000*/;

  public static long DefaultMaxThreshold => 218775168;
}
