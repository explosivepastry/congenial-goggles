// Decompiled with JetBrains decompiler
// Type: Monnit.TemperatureMulti
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

public class TemperatureMulti : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;

  public static long MonnitApplicationID => 53;

  public static string ApplicationName => "Aurora Temperature Strip";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => TemperatureMulti.MonnitApplicationID;

  public double[] Temp { get; set; }

  public override string Serialize()
  {
    return $"{this.Temp[0].ToString()},{this.Temp[1].ToString()},{this.Temp[2].ToString()},{this.Temp[3].ToString()},{this.Temp[4].ToString()},{this.Temp[5].ToString()},{this.Temp[6].ToString()},{this.Temp[7].ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[8]
      {
        new AppDatum(eDatumType.TemperatureData, "Temperature1", this.Temp[0]),
        new AppDatum(eDatumType.TemperatureData, "Temperature2", this.Temp[1]),
        new AppDatum(eDatumType.TemperatureData, "Temperature3", this.Temp[2]),
        new AppDatum(eDatumType.TemperatureData, "Temperature4", this.Temp[3]),
        new AppDatum(eDatumType.TemperatureData, "Temperature5", this.Temp[4]),
        new AppDatum(eDatumType.TemperatureData, "Temperature6", this.Temp[5]),
        new AppDatum(eDatumType.TemperatureData, "Temperature7", this.Temp[6]),
        new AppDatum(eDatumType.TemperatureData, "Temperature8", this.Temp[7])
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      (object) ((IEnumerable<double>) this.Temp).Select<double, double>((Func<double, double>) (t => !TemperatureMulti.IsFahrenheit(sensorID) ? t : t.ToFahrenheit()))
    };
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    string[] collection = new string[this.Temp.Length];
    for (int index = 0; index < this.Temp.Length; ++index)
      collection[index] = TemperatureMulti.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius";
    return new List<string>((IEnumerable<string>) collection);
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) TemperatureMulti.Deserialize(version, serialized);
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

  public override string NotificationString
  {
    get
    {
      string[] strArray = new string[8]
      {
        this.Temp[0] == (double) byte.MaxValue ? "BAD" : (this.CorF == null || !(this.CorF.Value == "C") ? this.Temp[0].ToFahrenheit().ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture) : this.Temp[0].ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture)),
        this.Temp[1] == (double) byte.MaxValue ? "BAD" : (this.CorF == null || !(this.CorF.Value == "C") ? this.Temp[1].ToFahrenheit().ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture) : this.Temp[1].ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture)),
        this.Temp[2] == (double) byte.MaxValue ? "BAD" : (this.CorF == null || !(this.CorF.Value == "C") ? this.Temp[2].ToFahrenheit().ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture) : this.Temp[2].ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture)),
        this.Temp[3] == (double) byte.MaxValue ? "BAD" : (this.CorF == null || !(this.CorF.Value == "C") ? this.Temp[3].ToFahrenheit().ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture) : this.Temp[3].ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture)),
        this.Temp[4] == (double) byte.MaxValue ? "BAD" : (this.CorF == null || !(this.CorF.Value == "C") ? this.Temp[4].ToFahrenheit().ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture) : this.Temp[4].ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture)),
        this.Temp[5] == (double) byte.MaxValue ? "BAD" : (this.CorF == null || !(this.CorF.Value == "C") ? this.Temp[5].ToFahrenheit().ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture) : this.Temp[5].ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture)),
        this.Temp[6] == (double) byte.MaxValue ? "BAD" : (this.CorF == null || !(this.CorF.Value == "C") ? this.Temp[6].ToFahrenheit().ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture) : this.Temp[6].ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture)),
        this.Temp[7] == (double) byte.MaxValue ? "BAD" : (this.CorF == null || !(this.CorF.Value == "C") ? this.Temp[7].ToFahrenheit().ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture) : this.Temp[7].ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture))
      };
      return $"{strArray[0]}, {strArray[1]}, {strArray[2]}, {strArray[3]}, {strArray[4]}, {strArray[5]}, {strArray[6]}, {strArray[7]}";
    }
  }

  public static int ColorSection(double tempCelcius)
  {
    if (tempCelcius <= 18.0)
      return 1;
    return tempCelcius >= 32.0 ? 15 : tempCelcius.ToInt() - 17;
  }

  public override object PlotValue => (object) this.MaxTemp;

  public double MaxTemp
  {
    get
    {
      double Celsius = 0.0;
      for (int index = 0; index < 8; ++index)
      {
        if (this.Temp[index] > Celsius && this.Temp[index] != (double) byte.MaxValue && this.Temp[index] < 70.0)
          Celsius = this.Temp[index];
      }
      if (Celsius <= 0.0 || Celsius >= 70.0)
        return 0.0;
      return this.CorF != null && this.CorF.Value == "C" ? Celsius : Celsius.ToFahrenheit();
    }
  }

  public double MinTemp
  {
    get
    {
      double Celsius = 70.0;
      for (int index = 0; index < 8; ++index)
      {
        if (this.Temp[index] < Celsius && this.Temp[index] != (double) byte.MaxValue)
          Celsius = this.Temp[index];
      }
      if (Celsius <= 0.0 || Celsius >= 70.0)
        return 0.0;
      return this.CorF != null && this.CorF.Value == "C" ? Celsius : Celsius.ToFahrenheit();
    }
  }

  public static TemperatureMulti Deserialize(string version, string serialized)
  {
    TemperatureMulti temperatureMulti = new TemperatureMulti();
    string[] strArray = serialized.Split(new char[1]{ ',' }, StringSplitOptions.RemoveEmptyEntries);
    if (strArray.Length == 1)
    {
      double num = strArray[0].ToDouble();
      temperatureMulti.Temp = new double[8]
      {
        num,
        num,
        num,
        num,
        num,
        num,
        num,
        num
      };
    }
    else
      temperatureMulti.Temp = new double[8]
      {
        strArray[0].ToDouble(),
        strArray[1].ToDouble(),
        strArray[2].ToDouble(),
        strArray[3].ToDouble(),
        strArray[4].ToDouble(),
        strArray[5].ToDouble(),
        strArray[6].ToDouble(),
        strArray[7].ToDouble()
      };
    return temperatureMulti;
  }

  public static TemperatureMulti Create(byte[] sdm, int startIndex)
  {
    return new TemperatureMulti()
    {
      Temp = new double[8]
      {
        sdm[startIndex].ToDouble(),
        sdm[startIndex + 1].ToDouble(),
        sdm[startIndex + 2].ToDouble(),
        sdm[startIndex + 3].ToDouble(),
        sdm[startIndex + 4].ToDouble(),
        sdm[startIndex + 5].ToDouble(),
        sdm[startIndex + 6].ToDouble(),
        sdm[startIndex + 7].ToDouble()
      }
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
      ApplicationID = TemperatureMulti.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion,
      Scale = TemperatureMulti.IsFahrenheit(sensor.SensorID) ? "F" : "C"
    };
  }

  public static Dictionary<string, string> NotificationScaleValues()
  {
    return Temperature.NotificationScaleValues();
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    Temperature.SetProfileSettings(sensor, collection, viewData);
  }

  public static string HystForUI(Sensor sensor) => Temperature.HystForUI(sensor);

  public static string MinThreshForUI(Sensor sensor) => Temperature.MinThreshForUI(sensor);

  public static string MaxThreshForUI(Sensor sensor) => Temperature.MaxThreshForUI(sensor);

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    return Temperature.ActionControlCommand(sensor, version);
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(TemperatureMulti left, TemperatureMulti right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(TemperatureMulti left, TemperatureMulti right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(TemperatureMulti left, TemperatureMulti right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(TemperatureMulti left, TemperatureMulti right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(TemperatureMulti left, TemperatureMulti right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(TemperatureMulti left, TemperatureMulti right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is TemperatureMulti && this.MaxTemp == (right as TemperatureMulti).MaxTemp;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is TemperatureMulti && this.MaxTemp != (right as TemperatureMulti).MaxTemp;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is TemperatureMulti && this.MinTemp < (right as TemperatureMulti).MinTemp;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is TemperatureMulti && this.MinTemp <= (right as TemperatureMulti).MinTemp;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is TemperatureMulti && this.MaxTemp > (right as TemperatureMulti).MaxTemp;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is TemperatureMulti && this.MaxTemp >= (right as TemperatureMulti).MaxTemp;
  }

  public static long DefaultMinThreshold => -400;

  public static long DefaultMaxThreshold => 1250;

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (collection["TempScale"] == "on")
    {
      viewData["TempScale"] = "F";
      TemperatureMulti.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      viewData["TempScale"] = "C";
      TemperatureMulti.MakeCelsius(sensor.SensorID);
    }
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
}
