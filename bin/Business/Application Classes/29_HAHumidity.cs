// Decompiled with JetBrains decompiler
// Type: Monnit.HAHumidity
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

public class HAHumidity : Humidity
{
  public new static long MonnitApplicationID => 29;

  public new static string ApplicationName => "HA Humidity";

  public new static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public static HAHumidity Deserialize(string version, string serialized)
  {
    HAHumidity haHumidity = new HAHumidity();
    if (string.IsNullOrEmpty(serialized))
    {
      haHumidity.Hum = 0.0;
      haHumidity.Temperature = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      haHumidity.Hum = strArray[0].ToDouble();
      if (strArray.Length > 1)
        haHumidity.Temperature = strArray[1].ToDouble();
      else
        haHumidity.Temperature = strArray[0].ToDouble();
    }
    return haHumidity;
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.Percentage, "Humidity", this.Hum),
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
      "Humidity",
      Humidity.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    });
  }

  public static HAHumidity Create(byte[] sdm, int startIndex)
  {
    HAHumidity haHumidity = new HAHumidity();
    double temperatureTicks = BitConverter.ToInt16(sdm, startIndex).ToDouble();
    double humidityTicks = BitConverter.ToInt16(sdm, startIndex + 2).ToDouble();
    haHumidity.Temperature = Humidity.CalcTemperature(temperatureTicks);
    haHumidity.Hum = Humidity.CalcHumidity(humidityTicks, haHumidity.Temperature);
    return haHumidity;
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

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = HAHumidity.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public new static void SetProfileSettings(
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
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
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

  public new static string HystForUI(Sensor sensor) => Humidity.HystForUI(sensor);

  public new static string MinThreshForUI(Sensor sensor) => Humidity.MinThreshForUI(sensor);

  public new static string MaxThreshForUI(Sensor sensor) => Humidity.MaxThreshForUI(sensor);

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(HAHumidity left, HAHumidity right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(HAHumidity left, HAHumidity right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(HAHumidity left, HAHumidity right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(HAHumidity left, HAHumidity right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(HAHumidity left, HAHumidity right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(HAHumidity left, HAHumidity right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public new static void SensorScale(
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
}
