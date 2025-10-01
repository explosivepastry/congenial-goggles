// Decompiled with JetBrains decompiler
// Type: Monnit.HandheldFoodProbe
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;

#nullable disable
namespace Monnit;

public class HandheldFoodProbe : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;
  private SensorAttribute _CalibrationValues;

  public static long MonnitApplicationID => HandheldFoodProbeBase.MonnitApplicationID;

  public static string ApplicationName => "Food Probe";

  public static eApplicationProfileType ProfileType => HandheldFoodProbeBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => HandheldFoodProbe.MonnitApplicationID;

  public double Temp { get; set; }

  public string Mode { get; set; }

  public int stsState { get; set; }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temp),
        new AppDatum(eDatumType.ProbeStatus, "Mode", this.Mode)
      });
    }
  }

  public static HandheldFoodProbe Create(byte[] sdm, int startIndex)
  {
    return new HandheldFoodProbe()
    {
      stsState = (int) sdm[startIndex - 1] >> 4,
      Temp = BitConverter.ToInt16(sdm, startIndex).ToDouble() / 10.0,
      Mode = HandheldFoodProbe.ModeStringValue((int) sdm[startIndex + 2])
    };
  }

  public override string Serialize()
  {
    return $"{this.Temp.ToString()}|{this.Mode}|{this.stsState.ToString()}";
  }

  public static HandheldFoodProbe Deserialize(string version, string serialized)
  {
    HandheldFoodProbe handheldFoodProbe = new HandheldFoodProbe();
    if (string.IsNullOrEmpty(serialized))
    {
      handheldFoodProbe.stsState = 0;
      handheldFoodProbe.Temp = 0.0;
      handheldFoodProbe.Mode = "";
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length > 1)
      {
        handheldFoodProbe.Temp = strArray[0].ToDouble();
        handheldFoodProbe.Mode = strArray[1].ToString();
        try
        {
          handheldFoodProbe.stsState = strArray[2].ToInt();
        }
        catch
        {
          handheldFoodProbe.stsState = 0;
        }
      }
      else
      {
        handheldFoodProbe.Temp = strArray[0].ToDouble();
        handheldFoodProbe.Mode = strArray[0].ToString();
        handheldFoodProbe.stsState = 0;
      }
    }
    return handheldFoodProbe;
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) HandheldFoodProbe.Deserialize(version, serialized);
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[1]
    {
      HandheldFoodProbe.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    });
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>() { this.PlotValue };
  }

  public static string ModeStringValue(int mode) => HandheldFoodProbeBase.ModeStringValue(mode);

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

  public static void SetCalibrationValues(long sensorID, double actual, double observed)
  {
    string str = $"{actual.ToString()}|{observed.ToString()}";
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
      if (sensorAttribute.Name == "CalibrationValues")
        this._CalibrationValues = sensorAttribute;
    }
  }

  public override object PlotValue
  {
    get
    {
      if (this.Temp <= -40.0 || this.Temp >= 260.0)
        return (object) null;
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.Temp : (object) this.Temp.ToFahrenheit();
    }
  }

  public override bool IsValid => this.stsState != 1 && this.stsState != 2 && this.stsState != 3;

  public override string NotificationString
  {
    get
    {
      string str = "";
      if (this.stsState == 1)
        return "No Probe Detected";
      if (this.stsState == 2)
        return "Probe Short Detected";
      if (this.stsState == 3)
        str += "Reading out of range ";
      string notificationString = this.CorF == null || !(this.CorF.Value == "C") ? str + this.Temp.ToFahrenheit().ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture) : str + this.Temp.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
      if (!string.IsNullOrEmpty(this.Mode))
        notificationString = $"{notificationString}  Mode: {this.Mode}";
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
      ApplicationID = HandheldFoodProbe.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion,
      Scale = HandheldFoodProbe.IsFahrenheit(sensor.SensorID) ? "F" : "C"
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
    bool flag = HandheldFoodProbe.IsFahrenheit(sensor.SensorID);
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
      if (!string.IsNullOrEmpty(collection["DisplayTimeOut_Manual"]))
      {
        int num = collection["DisplayTimeOut_Manual"].ToInt();
        if (num < 0)
          num = 0;
        if (num > 1800)
          num = 1800;
        sensor.Calibration1 = (long) num;
      }
      if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
      {
        double o = collection["Hysteresis_Manual"].ToDouble() * 60.0;
        if (o < 0.0)
          o = 0.0;
        if (o > 64800.0)
          o = 64800.0;
        sensor.Hysteresis = o.ToLong();
      }
      if (!string.IsNullOrEmpty(collection["SessionReportInterval"]))
      {
        double o = collection["SessionReportInterval"].ToDouble();
        if (o < 0.0)
          o = 0.0;
        if (o > 720.0)
          o = 720.0;
        sensor.Calibration4 = o.ToLong();
      }
      double result;
      if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) && double.TryParse(collection["MinimumThreshold_Manual"], out result))
      {
        sensor.MinimumThreshold = ((flag ? collection["MinimumThreshold_Manual"].ToDouble().ToCelsius() : collection["MinimumThreshold_Manual"].ToDouble()) * 10.0).ToLong();
        if (sensor.MinimumThreshold < -400L)
          sensor.MinimumThreshold = -400L;
        if (sensor.MinimumThreshold > 2600L)
          sensor.MinimumThreshold = 2600L;
      }
      if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) && double.TryParse(collection["MaximumThreshold_Manual"], out result))
      {
        sensor.MaximumThreshold = ((flag ? collection["MaximumThreshold_Manual"].ToDouble().ToCelsius() : collection["MaximumThreshold_Manual"].ToDouble()) * 10.0).ToLong();
        if (sensor.MaximumThreshold < -400L)
          sensor.MaximumThreshold = -400L;
        if (sensor.MaximumThreshold > 2600L)
          sensor.MaximumThreshold = 2600L;
        if (sensor.MinimumThreshold >= sensor.MaximumThreshold)
          sensor.MaximumThreshold = sensor.MinimumThreshold;
      }
    }
  }

  public static string HystForUI(Sensor sensor)
  {
    return (sensor.Hysteresis == (long) uint.MaxValue ? 0.0 : Convert.ToDouble(sensor.Hysteresis) / 60.0).ToString();
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
      HandheldFoodProbe.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      returnData["TempScale"] = "C";
      HandheldFoodProbe.MakeCelsius(sensor.SensorID);
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    double num1 = collection["actual"].ToDouble();
    double num2 = collection["observed"].ToDouble();
    if (collection["tempScale"] == "F")
    {
      num1 /= 1.8;
      num2 /= 1.8;
    }
    double actual = num1 >= -20.0 && num1 <= 135.0 ? (double) (num1 * 10.0).ToInt() : throw new Exception("Calibration value out of range!");
    double observed = (double) (num2 * 10.0).ToInt();
    HandheldFoodProbe.SetCalibrationValues(sensor.SensorID, actual, observed);
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      string[] strArray = HandheldFoodProbe.GetCalibrationValues(sensor.SensorID).Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      numArrayList.Add(HandheldFoodProbeBase.CalibrateFrame(sensor.SensorID, strArray[0].ToInt(), strArray[1].ToInt()));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public static double GetOffset(Sensor sens)
  {
    return Math.Round((double) Convert.ToInt32(sens.Calibration3 & (long) ushort.MaxValue) / 10.0, 2);
  }

  public static void SetOffset(Sensor sens, double value)
  {
    value *= 10.0;
    int int32 = Convert.ToInt32(sens.Calibration3 & 4294901760L);
    sens.Calibration3 = (long) (int32 | (int) value);
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

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(HandheldFoodProbe left, HandheldFoodProbe right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(HandheldFoodProbe left, HandheldFoodProbe right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(HandheldFoodProbe left, HandheldFoodProbe right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(HandheldFoodProbe left, HandheldFoodProbe right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(HandheldFoodProbe left, HandheldFoodProbe right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(HandheldFoodProbe left, HandheldFoodProbe right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is HandheldFoodProbe && this.Equals((MonnitApplicationBase) (obj as HandheldFoodProbe));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is HandheldFoodProbe && this.Temp == (right as HandheldFoodProbe).Temp;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is HandheldFoodProbe && this.Temp != (right as HandheldFoodProbe).Temp;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is HandheldFoodProbe && this.Temp < (right as HandheldFoodProbe).Temp;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is HandheldFoodProbe && this.Temp <= (right as HandheldFoodProbe).Temp;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is HandheldFoodProbe && this.Temp > (right as HandheldFoodProbe).Temp;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is HandheldFoodProbe && this.Temp >= (right as HandheldFoodProbe).Temp;
  }

  public static long DefaultMinThreshold => -40;

  public static long DefaultMaxThreshold => 260;
}
