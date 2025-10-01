// Decompiled with JetBrains decompiler
// Type: Monnit.LCD_Temperature
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

public class LCD_Temperature : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;
  private SensorAttribute _QueueID;
  private SensorAttribute _CalibrationValues;

  public static long MonnitApplicationID => LCD_TemperatureBase.MonnitApplicationID;

  public static string ApplicationName => "LCD Temperature";

  public static eApplicationProfileType ProfileType => LCD_TemperatureBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => LCD_Temperature.MonnitApplicationID;

  public double Temp { get; set; }

  public string Mode { get; set; }

  public int stsState { get; set; }

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

  public static LCD_Temperature Create(byte[] sdm, int startIndex)
  {
    return new LCD_Temperature()
    {
      stsState = (int) sdm[startIndex - 1] >> 4,
      Temp = BitConverter.ToInt16(sdm, startIndex).ToDouble() / 10.0,
      Mode = LCD_Temperature.ModeStringValue((int) sdm[startIndex + 2])
    };
  }

  public override string Serialize()
  {
    return $"{this.Temp.ToString()}|{this.Mode}|{this.stsState.ToString()}";
  }

  public static LCD_Temperature Deserialize(string version, string serialized)
  {
    LCD_Temperature lcdTemperature = new LCD_Temperature();
    if (string.IsNullOrEmpty(serialized))
    {
      lcdTemperature.stsState = 0;
      lcdTemperature.Temp = 0.0;
      lcdTemperature.Mode = "";
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length > 1)
      {
        lcdTemperature.Temp = strArray[0].ToDouble();
        lcdTemperature.Mode = strArray[1].ToString();
        try
        {
          lcdTemperature.stsState = strArray[2].ToInt();
        }
        catch
        {
          lcdTemperature.stsState = 0;
        }
      }
      else
      {
        lcdTemperature.Temp = strArray[0].ToDouble();
        lcdTemperature.Mode = "";
        lcdTemperature.stsState = 0;
      }
    }
    return lcdTemperature;
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) LCD_Temperature.Deserialize(version, serialized);
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[1]
    {
      LCD_Temperature.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    });
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>() { this.PlotValue };
  }

  public static string ModeStringValue(int mode) => LCD_TemperatureBase.ModeStringValue(mode);

  public static bool IsFahrenheit(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF" && sensorAttribute.Value == "C")
        return false;
    }
    return true;
  }

  public static void AlignLCDandAttributeScale(Sensor sensor)
  {
    if (LCD_Temperature.GetScaleCalval3(sensor) == 0)
      LCD_Temperature.MakeCelsius(sensor.SensorID);
    else
      LCD_Temperature.MakeFahrenheit(sensor.SensorID);
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
      if (sensorAttribute.Name == "CalibrationValues")
        this._CalibrationValues = sensorAttribute;
      if (sensorAttribute.Name == "QueueID")
        this._QueueID = sensorAttribute;
    }
  }

  public override object PlotValue
  {
    get
    {
      if (this.Temp <= -40.0 || this.Temp >= 125.0)
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
        notificationString = $"{notificationString} {this.Mode}";
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
      ApplicationID = LCD_Temperature.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion,
      Scale = LCD_Temperature.IsFahrenheit(sensor.SensorID) ? "F" : "C"
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
    bool flag = LCD_Temperature.IsFahrenheit(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["DisplayTimeOut_Manual"]))
    {
      int num = collection["DisplayTimeOut_Manual"].ToInt();
      if (num < 5)
        num = 5;
      if (num > 30 && num != (int) ushort.MaxValue)
        num = 30;
      LCD_Temperature.SetDisplayTimeout(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["deltaThresh"]))
    {
      double num1 = 0.0;
      if (collection["deltaThreshSlider"] == "on")
      {
        if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
        {
          double num2 = collection["Hysteresis_Manual"].ToDouble();
          if (flag)
            num2 /= 1.8;
          if (num2 < 0.0)
            num2 = 0.0;
          if (num2 > 5.0)
            num2 = 5.0;
          LCD_Temperature.SetHysteresis(sensor, num2);
        }
      }
      else
      {
        num1 = collection["deltaThresh"].ToDouble();
        if (flag && num1 != 0.0)
          num1 /= 1.8;
        if (num1 < 0.0)
          num1 = 0.0;
        if (num1 > 50.0)
          num1 = 50.0;
      }
      LCD_Temperature.SetDeltaThresh(sensor, num1);
    }
    double result;
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) && double.TryParse(collection["MinimumThreshold_Manual"], out result))
    {
      sensor.MinimumThreshold = ((flag ? collection["MinimumThreshold_Manual"].ToDouble().ToCelsius() : collection["MinimumThreshold_Manual"].ToDouble()) * 10.0).ToLong();
      if (sensor.MinimumThreshold < -400L)
        sensor.MinimumThreshold = -400L;
      if (sensor.MinimumThreshold > 1250L)
        sensor.MinimumThreshold = 1250L;
    }
    if (string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result))
      return;
    sensor.MaximumThreshold = ((flag ? collection["MaximumThreshold_Manual"].ToDouble().ToCelsius() : collection["MaximumThreshold_Manual"].ToDouble()) * 10.0).ToLong();
    if (sensor.MaximumThreshold < -400L)
      sensor.MaximumThreshold = -400L;
    if (sensor.MaximumThreshold > 1250L)
      sensor.MaximumThreshold = 1250L;
    if (sensor.MinimumThreshold >= sensor.MaximumThreshold)
      sensor.MaximumThreshold = sensor.MinimumThreshold;
  }

  public static string HystForUI(Sensor sensor)
  {
    double hysteresis = LCD_Temperature.GetHysteresis(sensor);
    return LCD_Temperature.IsFahrenheit(sensor.SensorID) ? Math.Round(hysteresis * 1.8, 1).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : hysteresis.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    double Celsius = sensor.MinimumThreshold == (long) uint.MaxValue ? Convert.ToDouble(sensor.DefaultValue<long>("DefaultMinimumThreshold")) / 10.0 : Convert.ToDouble(sensor.MinimumThreshold) / 10.0;
    return LCD_Temperature.IsFahrenheit(sensor.SensorID) ? Celsius.ToFahrenheit().ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : Celsius.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    double Celsius = sensor.MaximumThreshold == (long) uint.MaxValue ? Convert.ToDouble(sensor.DefaultValue<long>("DefaultMaximumThreshold")) / 10.0 : Convert.ToDouble(sensor.MaximumThreshold) / 10.0;
    return LCD_Temperature.IsFahrenheit(sensor.SensorID) ? Celsius.ToFahrenheit().ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : Celsius.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection returnData)
  {
    int num;
    if (collection["TempScale"] == "on")
    {
      returnData["TempScale"] = "F";
      LCD_Temperature.MakeFahrenheit(sensor.SensorID);
      num = 1;
    }
    else
    {
      returnData["TempScale"] = "C";
      LCD_Temperature.MakeCelsius(sensor.SensorID);
      num = 0;
    }
    LCD_Temperature.SetScaleCalval3(sensor, num);
    sensor.ProfileConfig2Dirty = true;
    sensor.Save();
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    double celsius = collection["actual"].ToDouble();
    double num = collection["observed"].ToDouble();
    if (collection["tempScale"] == "F")
      celsius = celsius.ToCelsius();
    double actual = celsius >= -40.0 && celsius <= 125.0 ? (double) (celsius * 10.0).ToInt() : throw new Exception("Calibration value out of range!");
    double observed = (double) (num * 10.0).ToInt();
    LCD_Temperature.SetQueueID(sensor.SensorID);
    LCD_Temperature.SetCalibrationValues(sensor.SensorID, actual, observed, 3, LCD_Temperature.GetQueueID(sensor.SensorID));
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      string[] strArray = LCD_Temperature.GetCalibrationValues(sensor.SensorID).Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      numArrayList.Add(LCD_TemperatureBase.CalibrateFrame(sensor.SensorID, strArray[0].ToInt(), strArray[1].ToInt(), strArray[2].ToInt(), strArray[3].ToInt()));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public static double GetHysteresis(Sensor sens)
  {
    return Math.Round((double) Convert.ToInt32(sens.Hysteresis & (long) ushort.MaxValue) / 10.0, 2);
  }

  public static void SetHysteresis(Sensor sens, double value)
  {
    value *= 10.0;
    int int32 = Convert.ToInt32(sens.Hysteresis & 4294901760L);
    sens.Hysteresis = (long) (int32 | (int) value);
  }

  public static double GetDeltaThresh(Sensor sens)
  {
    return (double) Convert.ToInt32((sens.Hysteresis & 4294901760L) >> 16 /*0x10*/) / 10.0;
  }

  public static void SetDeltaThresh(Sensor sens, double value)
  {
    value *= 10.0;
    uint num = Convert.ToUInt32(sens.Hysteresis) & (uint) ushort.MaxValue;
    sens.Hysteresis = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static int GetDisplayTimeout(Sensor sens)
  {
    return Convert.ToInt32(sens.Calibration3 & (long) ushort.MaxValue);
  }

  public static void SetDisplayTimeout(Sensor sens, int value)
  {
    int int32 = Convert.ToInt32(sens.Calibration3 & 4294901760L);
    sens.Calibration3 = (long) (int32 | value);
  }

  public static void SetScaleCalval3(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration3) & 16777215U /*0xFFFFFF*/;
    sensor.Calibration3 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 24));
  }

  public static int GetScaleCalval3(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration3) & 4278190080U /*0xFF000000*/) >> 24);
  }

  public static void SetRearm(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration3) & 4278255615U;
    sensor.Calibration3 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static int GetRearm(Sensor sensor)
  {
    return (int) Convert.ToUInt16(Convert.ToUInt32(sensor.Calibration3 & 16711680L /*0xFF0000*/) >> 16 /*0x10*/);
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

  public static bool operator ==(LCD_Temperature left, LCD_Temperature right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(LCD_Temperature left, LCD_Temperature right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(LCD_Temperature left, LCD_Temperature right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(LCD_Temperature left, LCD_Temperature right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(LCD_Temperature left, LCD_Temperature right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(LCD_Temperature left, LCD_Temperature right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is LCD_Temperature && this.Equals((MonnitApplicationBase) (obj as LCD_Temperature));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is LCD_Temperature && this.Temp == (right as LCD_Temperature).Temp;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is LCD_Temperature && this.Temp != (right as LCD_Temperature).Temp;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is LCD_Temperature && this.Temp < (right as LCD_Temperature).Temp;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is LCD_Temperature && this.Temp <= (right as LCD_Temperature).Temp;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is LCD_Temperature && this.Temp > (right as LCD_Temperature).Temp;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is LCD_Temperature && this.Temp >= (right as LCD_Temperature).Temp;
  }

  public static long DefaultMinThreshold => -40;

  public static long DefaultMaxThreshold => 125;

  public static long DefaultDeltaThreshold => 5;
}
