// Decompiled with JetBrains decompiler
// Type: Monnit.QuartzdynePressure
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

public class QuartzdynePressure : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;
  private SensorAttribute _ChartFormat;

  public static long MonnitApplicationID => 121;

  public static string ApplicationName => "Quartzdyne Pressure";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => QuartzdynePressure.MonnitApplicationID;

  public double Pressure { get; set; }

  public double Temperature { get; set; }

  public int stsState { get; set; }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.Pressure, "Pressure", this.Pressure),
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temperature)
      });
    }
  }

  public static QuartzdynePressure Create(byte[] sdm, int startIndex)
  {
    return new QuartzdynePressure()
    {
      stsState = (int) sdm[startIndex - 1] >> 4,
      Pressure = (double) BitConverter.ToSingle(sdm, startIndex),
      Temperature = (double) BitConverter.ToSingle(sdm, startIndex + 4)
    };
  }

  public override string Serialize()
  {
    return $"{this.Pressure.ToString()}|{this.Temperature.ToString()}|{this.stsState.ToString()}";
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
      "Pressure",
      QuartzdynePressure.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    });
  }

  public object PlotTemperatureValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.Temperature : (object) this.Temperature.ToFahrenheit();
    }
  }

  public static QuartzdynePressure Deserialize(string version, string serialized)
  {
    QuartzdynePressure quartzdynePressure = new QuartzdynePressure();
    if (string.IsNullOrEmpty(serialized))
    {
      quartzdynePressure.stsState = 0;
      quartzdynePressure.Pressure = 0.0;
      quartzdynePressure.Temperature = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length > 1)
      {
        quartzdynePressure.Pressure = strArray[0].ToDouble();
        quartzdynePressure.Temperature = strArray[1].ToDouble();
        try
        {
          quartzdynePressure.stsState = strArray[2].ToInt();
        }
        catch
        {
          quartzdynePressure.stsState = 0;
        }
      }
      else
      {
        quartzdynePressure.Pressure = strArray[0].ToDouble();
        quartzdynePressure.Temperature = strArray[0].ToDouble();
      }
    }
    return quartzdynePressure;
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) QuartzdynePressure.Deserialize(version, serialized);
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

  public SensorAttribute ChartFormat => this._ChartFormat;

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
      if (sensorAttribute.Name == "ChartFormat")
        this._ChartFormat = sensorAttribute;
    }
  }

  public override object PlotValue
  {
    get
    {
      double pressure = this.Pressure;
      return true ? (object) this.Pressure.To7Digits() : (object) null;
    }
  }

  public override string NotificationString
  {
    get
    {
      string notificationString;
      if (this.stsState == 1)
        notificationString = "I2C Failure";
      else if (this.stsState == 2)
      {
        notificationString = "Temp Failure";
      }
      else
      {
        string empty = string.Empty;
        notificationString = $"{this.PlotValue} PSI, Temperature: {(this.CorF == null || !(this.CorF.Value == "C") ? (object) (this.Temperature.ToFahrenheit().To7Digits() + "° F") : (object) (this.Temperature.To7Digits() + "° C"))}";
      }
      return notificationString;
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
      ApplicationID = QuartzdynePressure.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion,
      Scale = QuartzdynePressure.IsFahrenheit(sensor.SensorID) ? "F" : "C"
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
    double result = 0.0;
    bool flag = QuartzdynePressure.IsFahrenheit(sensor.SensorID);
    if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
    {
      sensor.Hysteresis = collection["Hysteresis_Manual"].ToDouble().ToLong();
      if (sensor.Hysteresis < 0L)
        sensor.Hysteresis = 0L;
      if (sensor.Hysteresis > 1000L)
        sensor.Hysteresis = 1000L;
    }
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      sensor.MinimumThreshold = collection["MinimumThreshold_Manual"].ToDouble().ToLong();
      if (sensor.MinimumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MinimumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      sensor.MaximumThreshold = collection["MaximumThreshold_Manual"].ToDouble().ToLong();
      if (sensor.MaximumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MaximumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MinimumThreshold > sensor.MaximumThreshold)
        sensor.MaximumThreshold = sensor.MinimumThreshold;
    }
    if (!string.IsNullOrEmpty(collection["TempMinThreshold_Manual"]))
    {
      double o = flag ? collection["TempMinThreshold_Manual"].ToDouble().ToCelsius() : collection["TempMinThreshold_Manual"].ToDouble();
      if (o < (double) sensor.DefaultValue<long>("DefaultCalibration1"))
        o = (double) sensor.DefaultValue<long>("DefaultCalibration1");
      if (o > (double) sensor.DefaultValue<long>("DefaultCalibration2"))
        o = (double) sensor.DefaultValue<long>("DefaultCalibration2");
      sensor.Calibration1 = o.ToLong();
    }
    if (!string.IsNullOrEmpty(collection["TempMaxThreshold_Manual"]))
    {
      double o = flag ? collection["TempMaxThreshold_Manual"].ToDouble().ToCelsius() : collection["TempMaxThreshold_Manual"].ToDouble();
      if (o < (double) sensor.DefaultValue<long>("DefaultCalibration1"))
        o = (double) sensor.DefaultValue<long>("DefaultCalibration1");
      if (o > (double) sensor.DefaultValue<long>("DefaultCalibration2"))
        o = (double) sensor.DefaultValue<long>("DefaultCalibration2");
      sensor.Calibration2 = o.ToLong();
      if (sensor.Calibration1 > sensor.Calibration2)
        sensor.Calibration2 = sensor.Calibration1;
    }
    if (!string.IsNullOrEmpty(collection["TempHysteresis_Manual"]))
    {
      sensor.Calibration3 = (collection["TempHysteresis_Manual"].ToDouble() * (flag ? 5.0 / 9.0 : 1.0)).ToLong();
      if (sensor.Calibration3 < 0L)
        sensor.Calibration3 = 0L;
      if (sensor.Calibration3 > 50L)
        sensor.Calibration3 = 50L;
    }
    if (string.IsNullOrEmpty(collection["warmUpTime_Manual"]))
      return;
    sensor.Calibration4 = collection["warmUpTime_Manual"].ToLong();
    if (sensor.Calibration4 < 100L)
      sensor.Calibration4 = 100L;
    if (sensor.Calibration4 > 10000L)
      sensor.Calibration4 = 10000L;
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : Convert.ToInt64(sensor.Hysteresis).ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : Convert.ToInt64(sensor.MinimumThreshold).ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : Convert.ToInt64(sensor.MaximumThreshold).ToString();
  }

  public static void DefaultTempThresholds(Sensor sensor, out long profileMin, out long profileMax)
  {
    profileMin = -40L;
    profileMax = 225L;
    try
    {
      if (!QuartzdynePressure.IsFahrenheit(sensor.SensorID))
        return;
      profileMin = profileMin.ToDouble().ToFahrenheit().ToLong();
      profileMax = profileMax.ToDouble().ToFahrenheit().ToLong();
    }
    catch (Exception ex)
    {
    }
  }

  public static double TemperatureMinThreshForUI(Sensor sensor)
  {
    double Celsius = sensor.Calibration1 == (long) uint.MaxValue ? Convert.ToDouble(sensor.DefaultValue<long>("DefaultCalibration1")) : Convert.ToDouble(sensor.Calibration1);
    return QuartzdynePressure.IsFahrenheit(sensor.SensorID) ? Celsius.ToFahrenheit() : Celsius;
  }

  public static double TemperatureMaxThreshForUI(Sensor sensor)
  {
    double Celsius = sensor.Calibration2 == (long) uint.MaxValue ? Convert.ToDouble(sensor.DefaultValue<long>("DefaultCalibration2")) : Convert.ToDouble(sensor.Calibration2);
    return QuartzdynePressure.IsFahrenheit(sensor.SensorID) ? Celsius.ToFahrenheit() : Celsius;
  }

  public static double TemperatureHystForUI(Sensor sensor)
  {
    double num = sensor.Calibration3 == (long) uint.MaxValue ? Convert.ToDouble(sensor.DefaultValue<long>("DefaultCalibration3")) : Convert.ToDouble(sensor.Calibration3);
    return QuartzdynePressure.IsFahrenheit(sensor.SensorID) ? num * 1.8 : num;
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection returnData)
  {
    if (collection["TempScale"] == "on")
    {
      returnData["TempScale"] = "F";
      QuartzdynePressure.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      returnData["TempScale"] = "C";
      QuartzdynePressure.MakeCelsius(sensor.SensorID);
    }
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
    target.EventDetectionType = source.EventDetectionType;
    target.EventDetectionPeriod = source.EventDetectionPeriod;
    target.EventDetectionCount = source.EventDetectionCount;
    target.RearmTime = source.RearmTime;
    target.BiStable = source.BiStable;
    target.TagString = source.TagString;
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      numArrayList.Add(QuartzdynePressureBase.CalibrateFrame(sensor.SensorID, (double) sensor.Calibration1 / 10.0));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(QuartzdynePressure left, QuartzdynePressure right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(QuartzdynePressure left, QuartzdynePressure right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(QuartzdynePressure left, QuartzdynePressure right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(QuartzdynePressure left, QuartzdynePressure right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(QuartzdynePressure left, QuartzdynePressure right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(QuartzdynePressure left, QuartzdynePressure right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is QuartzdynePressure && this.Equals((MonnitApplicationBase) (obj as QuartzdynePressure));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is QuartzdynePressure && this.Pressure == (right as QuartzdynePressure).Pressure;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is QuartzdynePressure && this.Pressure != (right as QuartzdynePressure).Pressure;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is QuartzdynePressure && this.Pressure < (right as QuartzdynePressure).Pressure;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is QuartzdynePressure && this.Pressure <= (right as QuartzdynePressure).Pressure;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is QuartzdynePressure && this.Pressure > (right as QuartzdynePressure).Pressure;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is QuartzdynePressure && this.Pressure >= (right as QuartzdynePressure).Pressure;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 30000;
}
