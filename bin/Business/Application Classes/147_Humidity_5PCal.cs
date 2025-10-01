// Decompiled with JetBrains decompiler
// Type: Monnit.Humidity_5PCal
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

public class Humidity_5PCal : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;
  private SensorAttribute _CalibrationValues;

  public static long MonnitApplicationID => 147;

  public static string ApplicationName => nameof (Humidity_5PCal);

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => Humidity_5PCal.MonnitApplicationID;

  public double Hum { get; set; }

  public double HumRaw { get; set; }

  public double Temperature { get; set; }

  public int stsState { get; set; }

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

  public override string NotificationString
  {
    get
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      string empty4 = string.Empty;
      string str1 = this.CorF == null || !(this.CorF.Value == "C") ? this.PlotTemperatureValue.ToString("#0.##° F", (IFormatProvider) CultureInfo.InvariantCulture) : this.PlotTemperatureValue.ToString("#0.##° C", (IFormatProvider) CultureInfo.InvariantCulture);
      string str2 = $"{this.PlotValue.ToDouble().ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture)}% @ {str1}";
      string str3 = $"Humidity Raw: {this.RawHumidityPlotValue.ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture)}%";
      if (this.stsState == 1)
        return $"Calibration Success, {str2}, {str3}";
      if (this.stsState == 2)
        return $"CalFail: Raw/Actual Difference Error, {str2}, {str3}";
      if (this.stsState == 3)
        return $"CalFail: Out of Range, {str2}, {str3}";
      if (this.stsState == 4)
        return $"CalFail: Invalid Calibration Point, {str2}, {str3}";
      if (this.stsState == 5)
        return "General Hardware Error";
      if (this.stsState == 6)
        return "CRC error";
      return this.stsState == 0 ? $"{str2}, {str3}" : "";
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[3]
    {
      this.PlotValue,
      (object) this.RawHumidityPlotValue,
      (object) this.PlotTemperatureValue
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[3]
    {
      "Humidity",
      "Raw Humidity",
      Humidity_5PCal.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    });
  }

  public override object PlotValue
  {
    get => this.Hum > -100.0 && this.Hum < 300.0 ? (object) this.Hum : (object) 0;
  }

  public double RawHumidityPlotValue
  {
    get => this.HumRaw > -100.0 && this.HumRaw < 300.0 ? this.HumRaw : 0.0;
  }

  public double PlotTemperatureValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? this.Temperature : this.Temperature.ToFahrenheit();
    }
  }

  public override string Serialize()
  {
    return $"{this.Hum}|{this.HumRaw}|{this.Temperature}|{this.stsState}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Humidity_5PCal.Deserialize(version, serialized);
  }

  public static Humidity_5PCal Deserialize(string version, string serialized)
  {
    Humidity_5PCal humidity5Pcal = new Humidity_5PCal();
    if (string.IsNullOrEmpty(serialized))
    {
      humidity5Pcal.Hum = 0.0;
      humidity5Pcal.HumRaw = 0.0;
      humidity5Pcal.Temperature = 0.0;
      humidity5Pcal.stsState = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      humidity5Pcal.Hum = strArray[0].ToDouble();
      if (strArray.Length > 1)
      {
        humidity5Pcal.HumRaw = strArray[1].ToDouble();
        humidity5Pcal.Temperature = strArray[2].ToDouble();
        try
        {
          humidity5Pcal.stsState = strArray[3].ToInt();
        }
        catch
        {
          humidity5Pcal.stsState = 0;
        }
      }
      else
      {
        humidity5Pcal.Hum = strArray[0].ToDouble();
        humidity5Pcal.HumRaw = strArray[0].ToDouble();
        humidity5Pcal.Temperature = strArray[0].ToDouble();
      }
    }
    return humidity5Pcal;
  }

  public static Humidity_5PCal Create(byte[] sdm, int startIndex)
  {
    return new Humidity_5PCal()
    {
      stsState = (int) sdm[startIndex - 1] >> 4 & 7,
      Temperature = BitConverter.ToInt16(sdm, startIndex).ToDouble() / 100.0,
      Hum = BitConverter.ToInt16(sdm, startIndex + 2).ToDouble() / 100.0,
      HumRaw = BitConverter.ToInt16(sdm, startIndex + 4).ToDouble() / 100.0
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    sensor.ActiveStateInterval = sensor.ReportInterval;
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (collection["TempScale"] == "on")
    {
      viewData["TempScale"] = "F";
      Humidity_5PCal.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      viewData["TempScale"] = "C";
      Humidity_5PCal.MakeCelsius(sensor.SensorID);
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    double num1 = collection["actual"].ToDouble();
    double num2 = collection["observed"].ToDouble();
    double actual = (double) (num1 * 100.0).ToInt();
    double observed = (double) (num2 * 100.0).ToInt();
    Humidity_5PCal.SetCalibrationValues(sensor.SensorID, actual, observed);
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      string[] strArray = Humidity_5PCal.GetCalibrationValues(sensor.SensorID).Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      numArrayList.Add(Humidity_5PCalBase.CalibrateFrame(sensor.SensorID, strArray[0].ToInt(), strArray[1].ToInt()));
      numArrayList.Add(sensor.ReadProfileConfig(28));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public static void SetTableValuesToDefault(Sensor sensor)
  {
    sensor.SuppressPropertyChangedEvent = true;
    sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
    sensor.SuppressPropertyChangedEvent = false;
    sensor.ProfileConfigDirty = true;
    sensor.ProfileConfig2Dirty = true;
    sensor.Save();
  }

  public static void SetLower2MaxThresh_RawHumidity(Sensor sens, double value)
  {
    value *= 100.0;
    uint uint32 = Convert.ToUInt32(sens.MaximumThreshold & 4294901760L);
    sens.MaximumThreshold = (long) (uint32 | (uint) value);
  }

  public static double GetLower2MaxThresh_RawHumidity(Sensor sens)
  {
    return (double) (short) Convert.ToUInt32(sens.MaximumThreshold & (long) ushort.MaxValue) / 100.0;
  }

  public static void SetUpper2MaxThresh_CalHumidity(Sensor sensor, double value)
  {
    value *= 100.0;
    int num = Convert.ToInt32(sensor.MaximumThreshold) & (int) ushort.MaxValue;
    sensor.MaximumThreshold = (long) (num | (int) value << 16 /*0x10*/);
  }

  public static double GetUpper2MaxThresh_CalHumidity(Sensor sensor)
  {
    return (double) Convert.ToInt32((sensor.MaximumThreshold & 4294901760L) >> 16 /*0x10*/) / 100.0;
  }

  public static void SetLower2CalVal1_RawHumidity(Sensor sens, double value)
  {
    value *= 100.0;
    uint uint32 = Convert.ToUInt32(sens.Calibration1 & 4294901760L);
    sens.Calibration1 = (long) (uint32 | (uint) value);
  }

  public static double GetLower2CalVal1_RawHumidity(Sensor sens)
  {
    return (double) (short) Convert.ToUInt32(sens.Calibration1 & (long) ushort.MaxValue) / 100.0;
  }

  public static void SetUpper2CalVal1_CalHumidity(Sensor sensor, double value)
  {
    value *= 100.0;
    int num = Convert.ToInt32(sensor.Calibration1) & (int) ushort.MaxValue;
    sensor.Calibration1 = (long) (num | (int) value << 16 /*0x10*/);
  }

  public static double GetUpper2CalVal1_CalHumidity(Sensor sensor)
  {
    return (double) Convert.ToInt32((sensor.Calibration1 & 4294901760L) >> 16 /*0x10*/) / 100.0;
  }

  public static void SetLower2CalVal2_RawHumidity(Sensor sens, double value)
  {
    value *= 100.0;
    uint uint32 = Convert.ToUInt32(sens.Calibration2 & 4294901760L);
    sens.Calibration2 = (long) (uint32 | (uint) value);
  }

  public static double GetLower2CalVal2_RawHumidity(Sensor sens)
  {
    return (double) (short) Convert.ToUInt32(sens.Calibration2 & (long) ushort.MaxValue) / 100.0;
  }

  public static void SetUpper2CalVal2_CalHumidity(Sensor sensor, double value)
  {
    value *= 100.0;
    int num = Convert.ToInt32(sensor.Calibration2) & (int) ushort.MaxValue;
    sensor.Calibration2 = (long) (num | (int) value << 16 /*0x10*/);
  }

  public static double GetUpper2CalVal2_CalHumidity(Sensor sensor)
  {
    return (double) Convert.ToInt32((sensor.Calibration2 & 4294901760L) >> 16 /*0x10*/) / 100.0;
  }

  public static void SetLower2CalVal3_RawHumidity(Sensor sens, double value)
  {
    value *= 100.0;
    uint uint32 = Convert.ToUInt32(sens.Calibration3 & 4294901760L);
    sens.Calibration3 = (long) (uint32 | (uint) value);
  }

  public static double GetLower2CalVal3_RawHumidity(Sensor sens)
  {
    return (double) (short) Convert.ToUInt32(sens.Calibration3 & (long) ushort.MaxValue) / 100.0;
  }

  public static void SetUpper2CalVal3_CalHumidity(Sensor sensor, double value)
  {
    value *= 100.0;
    int num = Convert.ToInt32(sensor.Calibration3) & (int) ushort.MaxValue;
    sensor.Calibration3 = (long) (num | (int) value << 16 /*0x10*/);
  }

  public static double GetUpper2CalVal3_CalHumidity(Sensor sensor)
  {
    return (double) Convert.ToInt32((sensor.Calibration3 & 4294901760L) >> 16 /*0x10*/) / 100.0;
  }

  public static void SetLower2CalVal4_RawHumidity(Sensor sens, double value)
  {
    value *= 100.0;
    uint uint32 = Convert.ToUInt32(sens.Calibration4 & 4294901760L);
    sens.Calibration4 = (long) (uint32 | (uint) value);
  }

  public static double GetLower2CalVal4_RawHumidity(Sensor sens)
  {
    return (double) (short) Convert.ToUInt32(sens.Calibration4 & (long) ushort.MaxValue) / 100.0;
  }

  public static void SetUpper2CalVal4_CalHumidity(Sensor sensor, double value)
  {
    value *= 100.0;
    int num = Convert.ToInt32(sensor.Calibration4) & (int) ushort.MaxValue;
    sensor.Calibration4 = (long) (num | (int) value << 16 /*0x10*/);
  }

  public static double GetUpper2CalVal4_CalHumidity(Sensor sensor)
  {
    return (double) Convert.ToInt32((sensor.Calibration4 & 4294901760L) >> 16 /*0x10*/) / 100.0;
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

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = Humidity_5PCal.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
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

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    Humidity_5PCalBase.GetDefaults(new Version(sensor.FirmwareVersion), sensor.GenerationType);
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
    Sensor.ClearCache(sensor.SensorID);
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Humidity_5PCal left, Humidity_5PCal right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Humidity_5PCal left, Humidity_5PCal right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Humidity_5PCal left, Humidity_5PCal right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Humidity_5PCal left, Humidity_5PCal right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Humidity_5PCal left, Humidity_5PCal right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Humidity_5PCal left, Humidity_5PCal right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Humidity_5PCal && this.Equals((MonnitApplicationBase) (obj as Humidity_5PCal));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Humidity_5PCal && this.Hum == (right as Humidity_5PCal).Hum;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Humidity_5PCal && this.Hum != (right as Humidity_5PCal).Hum;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Humidity_5PCal && this.Hum < (right as Humidity_5PCal).Hum;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Humidity_5PCal && this.Hum <= (right as Humidity_5PCal).Hum;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Humidity_5PCal && this.Hum > (right as Humidity_5PCal).Hum;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Humidity_5PCal && this.Hum >= (right as Humidity_5PCal).Hum;
  }
}
