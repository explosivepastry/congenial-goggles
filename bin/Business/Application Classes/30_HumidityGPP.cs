// Decompiled with JetBrains decompiler
// Type: Monnit.HumidityGPP
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

public class HumidityGPP : HumiditySHT25, ISensorAttribute
{
  private SensorAttribute _Pressure;

  public new static long MonnitApplicationID => 30;

  public new static string ApplicationName => "Grains Per Pound";

  public new static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override long ApplicationID => HumidityGPP.MonnitApplicationID;

  public override string NotificationString
  {
    get
    {
      string str = "GPP";
      if (this.CorF != null && this.CorF.Value == "C")
        str = "G/KG";
      return $"{((double) this.PlotValue).ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture)} {str}";
    }
  }

  public override object PlotValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? (object) (this.GPP / 7.0) : (object) this.GPP;
    }
  }

  public double GPP => HumidityGPP.GrainsPerPound(this.Hum, this.Temperature, this.Pressure);

  public new static double CalcPressure(int altitude)
  {
    return -4.6E-14 * Math.Pow((double) altitude, 3.0) + 7.60649E-09 * Math.Pow((double) altitude, 2.0) - 0.000530833868239 * (double) altitude + 14.6969012115013;
  }

  public static HumidityGPP Deserialize(string version, string serialized)
  {
    HumidityGPP humidityGpp = new HumidityGPP();
    if (string.IsNullOrEmpty(serialized))
    {
      humidityGpp.Hum = 0.0;
      humidityGpp.Temperature = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      if (strArray.Length > 1)
      {
        humidityGpp.Hum = strArray[0].ToDouble();
        humidityGpp.Temperature = strArray[1].ToDouble();
      }
      else
      {
        humidityGpp.Hum = HumidityGPP.CalcSimpleHumidityFromGPP(strArray[0].ToDouble());
        humidityGpp.Temperature = 30.0;
      }
    }
    return humidityGpp;
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[3]
      {
        new AppDatum(eDatumType.Weight, "GPP", this.GPP),
        new AppDatum(eDatumType.Percentage, "Humidity", this.Hum),
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temperature)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[3]
    {
      (object) this.Hum,
      this.PlotValue,
      (object) this.PlotTemperatureValue
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    string str = "GPP";
    if (this.CorF != null && this.CorF.Value == "C")
      str = "G/KG";
    return new List<string>((IEnumerable<string>) new string[3]
    {
      "Humidity",
      str,
      HumidityGPP.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    });
  }

  public new static bool IsFahrenheit(long sensorID) => Monnit.Temperature.IsFahrenheit(sensorID);

  public new static void MakeFahrenheit(long sensorID) => Monnit.Temperature.MakeFahrenheit(sensorID);

  public new static void MakeCelsius(long sensorID) => Monnit.Temperature.MakeCelsius(sensorID);

  public static HumidityGPP Create(byte[] sdm, int startIndex)
  {
    HumidityGPP humidityGpp = new HumidityGPP();
    HumiditySHT25 humidityShT25 = HumiditySHT25.Create(sdm, startIndex);
    humidityGpp.Temperature = humidityShT25.Temperature;
    humidityGpp.Hum = humidityShT25.Hum;
    return humidityGpp;
  }

  public new static void SetPressure(long sensorID, double pressure)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Pressure")
      {
        sensorAttribute.Value = pressure.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "Pressure",
        SensorID = sensorID,
        Value = HumidityGPP.CalcPressure(0).ToString()
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public new static double GetAltitude(long sensorID)
  {
    double num = 0.0;
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Pressure")
      {
        num = sensorAttribute.Value.ToDouble();
        flag = true;
      }
    }
    return !flag ? 0.0 : (double) ((int) Math.Round(Math.Log(num / 14.696) / -0.012 * 328.0 / 10.0) * 10);
  }

  private double Pressure
  {
    get => this._Pressure == null ? HumidityGPP.CalcPressure(0) : this._Pressure.Value.ToDouble();
  }

  public new void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Pressure")
        this._Pressure = sensorAttribute;
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
    }
    if (this._Pressure != null)
      return;
    this._Pressure = new SensorAttribute()
    {
      Name = "Pressure",
      SensorID = sensorID,
      Value = HumidityGPP.CalcPressure(0).ToString()
    };
    this._Pressure.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public new static double GrainsPerPound(double Humidity, double Temperature, double Pressure)
  {
    double num = Math.Pow(10.0, 8.07131 - 1730.63 / (Temperature + 233.426)) * 14.7 / 760.0;
    return 4372.270363951473 * num / (Pressure - num) * (Humidity / 100.0);
  }

  public static double CalcSimpleGPP(double Humidity)
  {
    return HumidityGPP.GrainsPerPound(Humidity, 30.0, HumidityGPP.CalcPressure(0));
  }

  public static double CalcSimpleHumidityFromGPP(double gpp)
  {
    double num1 = 30.0;
    double num2 = HumidityGPP.CalcPressure(0);
    double num3 = Math.Pow(10.0, 8.07131 - 1730.63 / (num1 + 233.426)) * 14.7 / 760.0;
    double num4 = 4372.270363951473 * num3 / (num2 - num3);
    return gpp / num4 * 100.0;
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
      ApplicationID = HumidityGPP.MonnitApplicationID,
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
      sensor.Hysteresis = (HumidityGPP.CalcSimpleHumidityFromGPP(collection["Hysteresis_Manual"].ToDouble()).ToLong() << 16 /*0x10*/) * 100L;
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      sensor.MinimumThreshold = (HumidityGPP.CalcSimpleHumidityFromGPP(collection["MinimumThreshold_Manual"].ToDouble()).ToLong() << 16 /*0x10*/) * 100L;
      sensor.MinimumThreshold += 60536L;
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      sensor.MaximumThreshold = (HumidityGPP.CalcSimpleHumidityFromGPP(collection["MaximumThreshold_Manual"].ToDouble()).ToLong() << 16 /*0x10*/) * 100L;
      sensor.MaximumThreshold += 15000L;
    }
    if (string.IsNullOrEmpty(collection["BasicThreshold"]) || string.IsNullOrEmpty(collection["BasicThresholdDirection"]) || !(collection["BasicThresholdDirection"] != "-1"))
      return;
    if (collection["BasicThresholdDirection"].ToInt() == 0)
    {
      sensor.MinimumThreshold = (HumidityGPP.CalcSimpleHumidityFromGPP(collection["BasicThreshold"].ToDouble()).ToLong() << 16 /*0x10*/) * 100L;
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    }
    else if (collection["BasicThresholdDirection"].ToInt() == 1)
    {
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      sensor.MaximumThreshold = (HumidityGPP.CalcSimpleHumidityFromGPP(collection["BasicThreshold"].ToDouble()).ToLong() << 16 /*0x10*/) * 100L;
    }
    sensor.MinimumThreshold += 60536L;
    sensor.MaximumThreshold += 15000L;
    sensor.MeasurementsPerTransmission = 6;
  }

  public new static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    HumidityGPP.SetPressure(sensor.SensorID, HumidityGPP.CalcPressure(collection["altitude"].ToInt()));
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }

  public new static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : Math.Round(HumidityGPP.CalcSimpleGPP((sensor.Hysteresis >> 16 /*0x10*/).ToDouble() / 100.0), 0).ToString();
  }

  public new static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "0" : Math.Round(HumidityGPP.CalcSimpleGPP((sensor.MinimumThreshold >> 16 /*0x10*/).ToDouble() / 100.0), 0).ToString();
  }

  public new static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "200" : Math.Round(HumidityGPP.CalcSimpleGPP((sensor.MaximumThreshold >> 16 /*0x10*/).ToDouble() / 100.0), 0).ToString();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(HumidityGPP left, HumidityGPP right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(HumidityGPP left, HumidityGPP right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(HumidityGPP left, HumidityGPP right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(HumidityGPP left, HumidityGPP right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(HumidityGPP left, HumidityGPP right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(HumidityGPP left, HumidityGPP right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is HumidityGPP && this.Equals((MonnitApplicationBase) (obj as HumidityGPP));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is HumidityGPP && this.GPP == (right as HumidityGPP).GPP;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is HumidityGPP && this.GPP != (right as HumidityGPP).Hum;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is HumidityGPP && this.GPP < (right as HumidityGPP).GPP;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is HumidityGPP && this.GPP <= (right as HumidityGPP).GPP;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is HumidityGPP && this.GPP > (right as HumidityGPP).GPP;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is HumidityGPP && this.GPP >= (right as HumidityGPP).GPP;
  }

  public new static long DefaultMinThreshold => 0;

  public new static long DefaultMaxThreshold => 100;

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

  public new static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (collection["TempScale"] == "on")
    {
      viewData["TempScale"] = "F";
      HumidityGPP.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      viewData["TempScale"] = "C";
      HumidityGPP.MakeCelsius(sensor.SensorID);
    }
  }
}
