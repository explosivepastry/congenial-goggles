// Decompiled with JetBrains decompiler
// Type: Monnit.PIRHumidity
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

public class PIRHumidity : MonnitApplicationBase, ISensorAttribute
{
  internal SensorAttribute _CorF;

  public static long MonnitApplicationID => 138;

  public static string ApplicationName => nameof (PIRHumidity);

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => PIRHumidity.MonnitApplicationID;

  public bool Motion { get; set; }

  public double Hum { get; set; }

  public double Temperature { get; set; }

  public int stsState { get; set; }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[3]
      {
        new AppDatum(eDatumType.MotionDetect, "Motion", this.Motion),
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temperature),
        new AppDatum(eDatumType.Percentage, "Humidity", this.Hum)
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
      string str1 = this.CorF == null || !(this.CorF.Value == "C") ? this.PlotTemperatureValue.ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture) : this.PlotTemperatureValue.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
      string str2 = this.Motion ? "Motion Detected, " : "";
      string str3 = $"{this.HumidityPlotValue.ToDouble().ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture)}% @ {str1}";
      if ((this.stsState & 1) == 1)
        str3 = "Humidity Fault";
      if ((this.stsState & 2) == 2)
        str2 = "Motion Fault, ";
      if ((this.stsState & 4) == 4)
        str3 = "Humidity Range Error: " + $"{this.HumidityPlotValue.ToDouble().ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture)}% @ {str1}";
      if ((this.stsState & 8) == 8)
        str3 = "Temperature Range Error: " + $"{this.HumidityPlotValue.ToDouble().ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture)}% @ {str1}";
      return $"{str2} {str3}";
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[3]
    {
      this.PlotValue,
      (object) this.PlotTemperatureValue,
      (object) this.HumidityPlotValue
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[3]
    {
      "Motion",
      PIRHumidity.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius",
      "Humidity"
    });
  }

  public override object PlotValue => (object) (this.Motion ? 1 : 0);

  public double HumidityPlotValue => this.Hum > -100.0 && this.Hum < 300.0 ? this.Hum : 0.0;

  public double PlotTemperatureValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? this.Temperature : this.Temperature.ToFahrenheit();
    }
  }

  public override string Serialize()
  {
    return $"{this.Motion}|{this.Temperature}|{this.Hum}|{this.stsState}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) PIRHumidity.Deserialize(version, serialized);
  }

  public static PIRHumidity Deserialize(string version, string serialized)
  {
    PIRHumidity pirHumidity = new PIRHumidity();
    if (string.IsNullOrEmpty(serialized))
    {
      pirHumidity.Motion = false;
      pirHumidity.Hum = 0.0;
      pirHumidity.Temperature = 0.0;
      pirHumidity.stsState = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      pirHumidity.Motion = strArray[0].ToBool();
      if (strArray.Length > 1)
      {
        pirHumidity.Temperature = strArray[1].ToDouble();
        pirHumidity.Hum = strArray[2].ToDouble();
        try
        {
          pirHumidity.stsState = strArray[3].ToInt();
        }
        catch
        {
          pirHumidity.stsState = 0;
        }
      }
      else
      {
        pirHumidity.Temperature = strArray[0].ToDouble();
        pirHumidity.Hum = strArray[0].ToDouble();
      }
    }
    return pirHumidity;
  }

  public static PIRHumidity Create(byte[] sdm, int startIndex)
  {
    return new PIRHumidity()
    {
      stsState = (int) sdm[startIndex - 1] >> 4,
      Motion = sdm[startIndex].ToBool(),
      Temperature = BitConverter.ToInt16(sdm, startIndex + 1).ToDouble() / 100.0,
      Hum = BitConverter.ToInt16(sdm, startIndex + 3).ToDouble() / 100.0
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    bool flag = PIRHumidity.IsFahrenheit(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["Hysteresis_Temp"]))
    {
      double num = collection["Hysteresis_Temp"].ToDouble();
      if (flag)
        num /= 1.8;
      if (num < 0.0)
        num = 0.0;
      if (num > 10.0)
        num = 10.0;
      PIRHumidity.SetTemperatureAwareBuffer(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Temp"]))
    {
      double Fahrenheit = collection["MinimumThreshold_Temp"].ToDouble();
      if (flag)
        Fahrenheit = Fahrenheit.ToCelsius();
      if (Fahrenheit < -50.0)
        Fahrenheit = -50.0;
      if (Fahrenheit > 150.0)
        Fahrenheit = 150.0;
      PIRHumidity.SetTemperatureMinThresh(sensor, Fahrenheit);
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Temp"]))
    {
      double Fahrenheit = collection["MaximumThreshold_Temp"].ToDouble();
      double temperatureMinThresh = PIRHumidity.GetTemperatureMinThresh(sensor);
      if (flag)
        Fahrenheit = Fahrenheit.ToCelsius();
      if (Fahrenheit < -50.0)
        Fahrenheit = -50.0;
      if (Fahrenheit > 150.0)
        Fahrenheit = 150.0;
      if (Fahrenheit < temperatureMinThresh)
        Fahrenheit = temperatureMinThresh;
      PIRHumidity.SetTemperatureMaxThresh(sensor, Fahrenheit);
    }
    if (!string.IsNullOrEmpty(collection["Hysteresis_Humidity"]))
    {
      double num = collection["Hysteresis_Humidity"].ToDouble();
      if (num < 0.0)
        num = 0.0;
      if (num > 10.0)
        num = 10.0;
      PIRHumidity.SetHumidityAwareBuffer(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Humidity"]))
    {
      double num = collection["MinimumThreshold_Humidity"].ToDouble();
      if (num < 0.0)
        num = 0.0;
      if (num > 100.0)
        num = 100.0;
      PIRHumidity.SetHumidityMinThresh(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Humidity"]))
    {
      double num = collection["MaximumThreshold_Humidity"].ToDouble();
      double humidityMinThresh = PIRHumidity.GetHumidityMinThresh(sensor);
      if (num < 0.0)
        num = 0.0;
      if (num > 100.0)
        num = 100.0;
      if (num < humidityMinThresh)
        num = humidityMinThresh;
      PIRHumidity.SetHumidityMaxThresh(sensor, num);
    }
    if (!string.IsNullOrWhiteSpace(collection["Sensitivity"]))
    {
      int num = collection["Sensitivity"].ToInt();
      if (num < 20)
        num = 20;
      if (num > 100)
        num = 100;
      PIRHumidity.SetPIRSensitivity(sensor, num);
    }
    if (!string.IsNullOrWhiteSpace(collection["TimeToReArm"]))
    {
      double num = collection["TimeToReArm"].ToDouble();
      double activeStateInterval = sensor.ActiveStateInterval;
      if (num < 0.017)
        num = 0.017;
      if (num > 720.0)
        num = 720.0;
      if (num > activeStateInterval)
        num = activeStateInterval;
      double o = num * 60.0;
      PIRHumidity.SetRearmTime(sensor, o.ToInt());
    }
    if (string.IsNullOrWhiteSpace(collection["ReportImmediatelyOn"]))
      return;
    PIRHumidity.SetAwareOnMotion(sensor, collection["ReportImmediatelyOn"].ToInt());
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (collection["TempScale"] == "on")
    {
      viewData["TempScale"] = "F";
      PIRHumidity.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      viewData["TempScale"] = "C";
      PIRHumidity.MakeCelsius(sensor.SensorID);
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (!string.IsNullOrEmpty(collection["TempOffset"]))
    {
      double num = collection["TempOffset"].ToDouble();
      if (PIRHumidity.IsFahrenheit(sensor.SensorID))
        num /= 1.8;
      if (num < -10.0)
        num = -10.0;
      if (num > 10.0)
        num = 10.0;
      PIRHumidity.SetTemperatureOffset(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["HumOffset"]))
    {
      double num = collection["HumOffset"].ToDouble();
      if (num < -10.0)
        num = -10.0;
      if (num > 10.0)
        num = 10.0;
      PIRHumidity.SetHumidityOffset(sensor, num);
    }
    sensor.Save();
  }

  public static void SetTemperatureAwareBuffer(Sensor sens, double value)
  {
    value *= 100.0;
    int int32 = Convert.ToInt32(sens.Hysteresis & 4294901760L);
    sens.Hysteresis = (long) (int32 | (int) value);
  }

  public static double GetTemperatureAwareBuffer(Sensor sens)
  {
    return (double) Convert.ToInt32(sens.Hysteresis & (long) ushort.MaxValue) / 100.0;
  }

  public static void SetHumidityAwareBuffer(Sensor sensor, double value)
  {
    value *= 100.0;
    uint num = Convert.ToUInt32(sensor.Hysteresis) & (uint) ushort.MaxValue;
    sensor.Hysteresis = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static double GetHumidityAwareBuffer(Sensor sensor)
  {
    return (double) Convert.ToInt32((sensor.Hysteresis & 4294901760L) >> 16 /*0x10*/) / 100.0;
  }

  public static void SetTemperatureMinThresh(Sensor sens, double value)
  {
    value *= 100.0;
    uint uint32 = Convert.ToUInt32(sens.MinimumThreshold & 4294901760L);
    sens.MinimumThreshold = (long) (uint32 | (uint) value);
  }

  public static double GetTemperatureMinThresh(Sensor sens)
  {
    return (double) (short) Convert.ToUInt32(sens.MinimumThreshold & (long) ushort.MaxValue) / 100.0;
  }

  public static void SetHumidityMinThresh(Sensor sensor, double value)
  {
    value *= 100.0;
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & (uint) ushort.MaxValue;
    sensor.MinimumThreshold = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static double GetHumidityMinThresh(Sensor sensor)
  {
    return (double) Convert.ToInt32((sensor.MinimumThreshold & 4294901760L) >> 16 /*0x10*/) / 100.0;
  }

  public static void SetTemperatureMaxThresh(Sensor sens, double value)
  {
    value *= 100.0;
    uint uint32 = Convert.ToUInt32(sens.MaximumThreshold & 4294901760L);
    sens.MaximumThreshold = (long) (uint32 | (uint) value);
  }

  public static double GetTemperatureMaxThresh(Sensor sens)
  {
    return (double) (short) Convert.ToUInt32(sens.MaximumThreshold & (long) ushort.MaxValue) / 100.0;
  }

  public static void SetHumidityMaxThresh(Sensor sensor, double value)
  {
    value *= 100.0;
    int num = Convert.ToInt32(sensor.MaximumThreshold) & (int) ushort.MaxValue;
    sensor.MaximumThreshold = (long) (num | (int) value << 16 /*0x10*/);
  }

  public static double GetHumidityMaxThresh(Sensor sensor)
  {
    return (double) Convert.ToInt32((sensor.MaximumThreshold & 4294901760L) >> 16 /*0x10*/) / 100.0;
  }

  public static void SetTemperatureOffset(Sensor sens, double value)
  {
    value *= 100.0;
    uint uint32 = Convert.ToUInt32(sens.Calibration1 & 4294901760L);
    sens.Calibration1 = (long) (uint32 | (uint) value);
  }

  public static double GetTemperatureOffset(Sensor sens)
  {
    return (double) (short) Convert.ToUInt32(sens.Calibration1 & (long) ushort.MaxValue) / 100.0;
  }

  public static void SetHumidityOffset(Sensor sensor, double value)
  {
    value *= 100.0;
    int int32 = Convert.ToInt32(sensor.Calibration1 & (long) ushort.MaxValue);
    sensor.Calibration1 = (long) (int32 | (int) value << 16 /*0x10*/);
  }

  public static double GetHumidityOffset(Sensor sensor)
  {
    return (double) (short) Convert.ToUInt32((sensor.Calibration1 & 4294901760L) >> 16 /*0x10*/) / 100.0;
  }

  public static void SetRearmTime(Sensor sens, int value)
  {
    uint num = (uint) sens.Calibration2 & 4294901760U;
    sens.Calibration2 = (long) (num | (uint) (ushort) value);
  }

  public static int GetRearmTime(Sensor sens)
  {
    return (int) (ushort) ((uint) sens.Calibration2 & (uint) ushort.MaxValue);
  }

  public static void SetAwareOnMotion(Sensor sens, int value)
  {
    uint num = (uint) sens.Calibration2 & 4278255615U;
    sens.Calibration2 = (long) (num | (uint) (byte) value << 16 /*0x10*/);
  }

  public static int GetAwareOnMotion(Sensor sens)
  {
    return (int) (ushort) (((uint) sens.Calibration2 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetPIRSensitivity(Sensor sensor, int value)
  {
    uint num = (uint) (Convert.ToInt32(sensor.Calibration2) & 16777215 /*0xFFFFFF*/);
    sensor.Calibration2 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 24));
  }

  public static int GetPIRSensitivity(Sensor sensor)
  {
    return (int) Convert.ToUInt16(Convert.ToUInt32(sensor.Calibration2 & 4278190080L /*0xFF000000*/) >> 24);
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
    }
  }

  public static bool IsFahrenheit(long sensorID) => Monnit.Temperature.IsFahrenheit(sensorID);

  public static void MakeFahrenheit(long sensorID) => Monnit.Temperature.MakeFahrenheit(sensorID);

  public static void MakeCelsius(long sensorID) => Monnit.Temperature.MakeCelsius(sensorID);

  public SensorAttribute CorF => this._CorF;

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = PIRHumidity.MonnitApplicationID,
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
    PIRHumidityBase.GetDefaults(new Version(sensor.FirmwareVersion), sensor.GenerationType);
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

  public static bool operator ==(PIRHumidity left, PIRHumidity right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(PIRHumidity left, PIRHumidity right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(PIRHumidity left, PIRHumidity right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(PIRHumidity left, PIRHumidity right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(PIRHumidity left, PIRHumidity right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(PIRHumidity left, PIRHumidity right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is PIRHumidity && this.Equals((MonnitApplicationBase) (obj as PIRHumidity));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is PIRHumidity && this.Motion == (right as PIRHumidity).Motion;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is PIRHumidity && this.Motion != (right as PIRHumidity).Motion;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<' cannot be applied to operands of type 'bool' and 'bool'");
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<=' cannot be applied to operands of type 'bool' and 'bool'");
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>' cannot be applied to operands of type 'bool' and 'bool'");
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>=' cannot be applied to operands of type 'bool' and 'bool'");
  }

  public static string GetLabel(long sensorID) => "";
}
