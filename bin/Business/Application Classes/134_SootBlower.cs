// Decompiled with JetBrains decompiler
// Type: Monnit.SootBlower
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;

#nullable disable
namespace Monnit;

public class SootBlower : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;
  private SensorAttribute _ChartFormat;
  private SensorAttribute _QueueID;
  private SensorAttribute _CalibrationValues;

  public static long MonnitApplicationID => SootBlowerBase.MonnitApplicationID;

  public static string ApplicationName => "Soot Blower";

  public static eApplicationProfileType ProfileType => SootBlowerBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => SootBlower.MonnitApplicationID;

  public double Temperature { get; set; }

  public double Pressure { get; set; }

  public double CurrentA { get; set; }

  public double CurrentB { get; set; }

  public double CurrentC { get; set; }

  public bool Mode { get; set; }

  public int stsStatus { get; set; }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[6]
      {
        new AppDatum(eDatumType.TemperatureData, "PVTemperature", this.Temperature),
        new AppDatum(eDatumType.Pressure, "PVPressure", this.Pressure),
        new AppDatum(eDatumType.Amps, "CT1", this.CurrentA),
        new AppDatum(eDatumType.Amps, "CT2", this.CurrentB),
        new AppDatum(eDatumType.Amps, "CT3", this.CurrentC),
        new AppDatum(eDatumType.BooleanData, "InService", this.Mode)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[6]
    {
      this.PlotTemperatureValue,
      (object) this.Pressure,
      (object) this.CurrentA,
      (object) this.CurrentB,
      (object) this.CurrentC,
      (object) this.Mode
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[6]
    {
      SootBlower.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius",
      "PSI",
      "PSI",
      "PSI",
      "Amps",
      ""
    });
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) SootBlower.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      if ((this.stsStatus & 1) == 1)
        stringBuilder1.Append("Thermocouple COMM Error. ");
      if ((this.stsStatus & 2) == 2)
        stringBuilder1.Append("Thermocouple Probe Not Attached or Cable Break. ");
      if ((this.stsStatus & 4) == 4)
        stringBuilder2.Append("Pressure Sensor Error (Transducer Error or Cable Break). ");
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string str1;
      double num;
      if (stringBuilder1.Length > 0)
        str1 = stringBuilder1.ToString();
      else if (this.CorF != null && this.CorF.Value == "C")
      {
        num = this.Temperature;
        str1 = num.ToString("##.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
      }
      else
      {
        num = this.Temperature.ToFahrenheit();
        str1 = num.ToString("##.#° F", (IFormatProvider) CultureInfo.InvariantCulture);
      }
      string str2;
      if (stringBuilder2.Length > 0)
      {
        str2 = stringBuilder2.ToString();
      }
      else
      {
        num = this.Pressure;
        str2 = num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) + " PSI ";
      }
      object[] objArray = new object[6]
      {
        (object) str1,
        (object) str2,
        null,
        null,
        null,
        null
      };
      num = this.CurrentA;
      objArray[2] = (object) num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
      num = this.CurrentB;
      objArray[3] = (object) num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
      num = this.CurrentC;
      objArray[4] = (object) num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
      objArray[5] = this.Mode ? (object) "Active" : (object) "Inactive";
      return string.Format("Temperature: {0} , Pressure: {1} , Current A: {2} Amps , Current B: {3} Amps , Current C: {4} Amps , Mode: {5}", objArray);
    }
  }

  public override object PlotValue => this.PlotTemperatureValue;

  public object PlotTemperatureValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.Temperature : (object) this.Temperature.ToFahrenheit();
    }
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

  public SensorAttribute QueueID => this._QueueID;

  public static int GetQueueID(long sensorID)
  {
    SensorAttribute.ResetAttributeList(sensorID);
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "QueueID")
        return sensorAttribute.Value.ToInt();
    }
    return 1;
  }

  public static void SetQueueID(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "QueueID")
      {
        sensorAttribute.Value = (sensorAttribute.Value.ToInt() + 1).ToString();
        if (sensorAttribute.Value.ToInt() > 254)
          sensorAttribute.Value = "1";
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "QueueID",
        Value = "1",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute CalibrationValues => this._CalibrationValues;

  public static string GetCalibrationValues(long sensorID)
  {
    SensorAttribute.ResetAttributeList(sensorID);
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CalibrationValues")
        return sensorAttribute.Value;
    }
    return "";
  }

  public static void SetCalibrationValues(
    long sensorID,
    double actual,
    double observed,
    int subcommand,
    int queueID)
  {
    string str = $"{actual.ToString()}|{observed.ToString()}|{queueID.ToString()}|{subcommand.ToString()}";
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CalibrationValues")
      {
        sensorAttribute.Value = str.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "CalibrationValues",
        Value = str.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
      if (sensorAttribute.Name == "ChartFormat")
        this._ChartFormat = sensorAttribute;
      if (sensorAttribute.Name == "CalibrationValues")
        this._CalibrationValues = sensorAttribute;
      if (sensorAttribute.Name == "QueueID")
        this._QueueID = sensorAttribute;
    }
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (collection["TempScale"] == "on")
    {
      viewData["TempScale"] = "F";
      SootBlower.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      viewData["TempScale"] = "C";
      SootBlower.MakeCelsius(sensor.SensorID);
    }
  }

  public static SootBlower Deserialize(string version, string serialized)
  {
    SootBlower sootBlower = new SootBlower();
    if (string.IsNullOrEmpty(serialized))
    {
      sootBlower.stsStatus = 0;
      sootBlower.Temperature = 0.0;
      sootBlower.Pressure = 0.0;
      sootBlower.CurrentA = 0.0;
      sootBlower.CurrentB = 0.0;
      sootBlower.CurrentC = 0.0;
      sootBlower.Mode = false;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length > 1)
      {
        try
        {
          sootBlower.Temperature = strArray[0].ToDouble();
          sootBlower.Pressure = strArray[1].ToDouble();
          sootBlower.CurrentA = strArray[2].ToDouble();
          sootBlower.CurrentB = strArray[3].ToDouble();
          sootBlower.CurrentC = strArray[4].ToDouble();
          sootBlower.Mode = strArray[5].ToBool();
          try
          {
            sootBlower.stsStatus = strArray[6].ToInt();
          }
          catch
          {
            sootBlower.stsStatus = 0;
          }
        }
        catch
        {
          sootBlower.stsStatus = 0;
          sootBlower.Temperature = 0.0;
          sootBlower.Pressure = 0.0;
          sootBlower.CurrentA = 0.0;
          sootBlower.CurrentB = 0.0;
          sootBlower.CurrentC = 0.0;
          sootBlower.Mode = false;
        }
      }
      else
      {
        sootBlower.Temperature = strArray[0].ToDouble();
        sootBlower.Pressure = strArray[0].ToDouble();
        sootBlower.CurrentA = strArray[0].ToDouble();
        sootBlower.CurrentB = strArray[0].ToDouble();
        sootBlower.CurrentC = strArray[0].ToDouble();
        sootBlower.Mode = strArray[0].ToBool();
      }
    }
    return sootBlower;
  }

  public override string Serialize()
  {
    return $"{this.Temperature.ToString()}|{this.Pressure.ToString()}|{this.CurrentA.ToString()}|{this.CurrentB.ToString()}|{this.CurrentC.ToString()}|{this.Mode.ToString()}|{this.stsStatus.ToString()}";
  }

  public static SootBlower Create(byte[] sdm, int startIndex)
  {
    return new SootBlower()
    {
      stsStatus = (int) sdm[startIndex - 1] >> 4,
      Temperature = BitConverter.ToInt16(sdm, startIndex).ToDouble() / 10.0,
      Pressure = BitConverter.ToUInt16(sdm, startIndex + 2).ToDouble() / 10.0,
      CurrentA = BitConverter.ToUInt16(sdm, startIndex + 4).ToDouble() / 10.0,
      CurrentB = BitConverter.ToUInt16(sdm, startIndex + 6).ToDouble() / 10.0,
      CurrentC = BitConverter.ToUInt16(sdm, startIndex + 8).ToDouble() / 10.0,
      Mode = Convert.ToBoolean(sdm[startIndex + 10])
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
    sensor.ReportInterval = 15.0;
    sensor.ActiveStateInterval = 15.0;
    sensor.MinimumCommunicationFrequency = sensor.ReportInterval.ToInt() * 2 + 5;
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      SnoozeDuration = 60,
      CompareValue = "0",
      AccountID = sensor.AccountID,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = SootBlower.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    sensor.ActiveStateInterval = sensor.ReportInterval;
    double result = 0.0;
    bool flag = SootBlower.IsFahrenheit(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
    {
      if (double.TryParse(collection["Hysteresis_Manual"], out result))
      {
        double num = collection["Hysteresis_Manual"].ToDouble();
        if (flag)
          num /= 1.8;
        if (num < 1.0)
          num = 1.0;
        if (num > 50.0)
          num = 50.0;
        sensor.Hysteresis = (num * 10.0).ToLong();
      }
      else
        sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    }
    else
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      if (double.TryParse(collection["MaximumThreshold_Manual"], out result))
      {
        double o = collection["MaximumThreshold_Manual"].ToDouble();
        if (o < 1.0)
          o = 1.0;
        if (o > 50.0)
          o = 50.0;
        sensor.MaximumThreshold = o.ToLong() * 10L;
      }
      else
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    }
    else
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      if (double.TryParse(collection["MinimumThreshold_Manual"], out result))
      {
        double o = (double) (collection["MinimumThreshold_Manual"].ToDouble() * 10.0).ToLong();
        if (o < 1.0)
          o = 1.0;
        if (o > 50.0)
          o = 50.0;
        sensor.MinimumThreshold = o.ToLong();
      }
      else
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    }
    else
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if (!string.IsNullOrEmpty(collection["Calibration3_Manual"]))
    {
      if (double.TryParse(collection["Calibration3_Manual"], out result))
      {
        double o = collection["Calibration3_Manual"].ToDouble();
        if (o < 1.0)
          o = 1.0;
        if (o > 120.0)
          o = 120.0;
        if (o > sensor.ReportInterval * 60.0)
          o = sensor.ReportInterval * 60.0;
        sensor.Calibration3 = o.ToLong();
      }
      else
        sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    }
    else
      sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    double result = 0.0;
    if (string.IsNullOrEmpty(collection["actualTemp"]) || string.IsNullOrEmpty(collection["observedTemp"]) || !double.TryParse(collection["actualTemp"], out result) || !double.TryParse(collection["observedTemp"], out result))
      return;
    int subcommand = 3;
    double num1 = collection["actualTemp"].ToDouble();
    double num2 = collection["observedTemp"].ToDouble();
    double actual;
    double observed;
    if (SootBlower.IsFahrenheit(sensor.SensorID))
    {
      actual = (num1 - 32.0) * (5.0 / 9.0) * 10.0;
      observed = (num2 - 32.0) * (5.0 / 9.0) * 10.0;
    }
    else
    {
      actual = num1 * 10.0;
      observed = num2 * 10.0;
    }
    SootBlower.SetQueueID(sensor.SensorID);
    SootBlower.SetCalibrationValues(sensor.SensorID, actual, observed, subcommand, SootBlower.GetQueueID(sensor.SensorID));
    sensor.PendingActionControlCommand = true;
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

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    NonCachedAttribute.LoadBySensorIDAndName(sensor.SensorID, "Calibration");
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      string[] strArray = SootBlower.GetCalibrationValues(sensor.SensorID).Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      numArrayList.Add(SootBlowerBase.CalibrateFrame(sensor.SensorID, strArray[0].ToInt(), strArray[1].ToInt(), strArray[2].ToInt(), strArray[3].ToInt()));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public static string HystForUI(Sensor sensor)
  {
    double num = sensor.Hysteresis == (long) uint.MaxValue ? 0.0 : Convert.ToDouble(sensor.Hysteresis) / 10.0;
    return SootBlower.IsFahrenheit(sensor.SensorID) ? (num * 1.8).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : (sensor.MinimumThreshold.ToDouble() / 10.0).ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : (sensor.MaximumThreshold.ToDouble() / 10.0).ToString();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Current left, SootBlower right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Current left, SootBlower right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Current left, SootBlower right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Current left, SootBlower right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Current left, SootBlower right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Current left, SootBlower right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is SootBlower && this.Equals((MonnitApplicationBase) (obj as SootBlower));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is SootBlower && this.Temperature == (right as SootBlower).Temperature;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is SootBlower && this.Temperature != (right as SootBlower).Temperature;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is SootBlower && this.Temperature < (right as SootBlower).Temperature;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is SootBlower && this.Temperature <= (right as SootBlower).Temperature;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is SootBlower && this.Temperature > (right as SootBlower).Temperature;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is SootBlower && this.Temperature >= (right as SootBlower).Temperature;
  }

  public static long DefaultMinThreshold => 5;

  public static long DefaultMaxThreshold => 50;

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
