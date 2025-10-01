// Decompiled with JetBrains decompiler
// Type: Monnit.MotionTemp
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

public class MotionTemp : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;

  public static long MonnitApplicationID => MotionTempBase.MonnitApplicationID;

  public static string ApplicationName => "Motion Temp";

  public static eApplicationProfileType ProfileType => MotionTempBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => MotionTemp.MonnitApplicationID;

  public double Temp { get; set; }

  public bool Motion { get; set; }

  public int State { get; set; }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temp),
        new AppDatum(eDatumType.MotionDetect, "Motion", this.Motion)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[2]
    {
      this.PlotValue,
      (object) this.Motion
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[2]
    {
      MotionTemp.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius",
      "MotionDetect"
    });
  }

  public static MotionTemp Create(byte[] sdm, int startIndex)
  {
    return new MotionTemp()
    {
      State = (int) sdm[startIndex - 1],
      Motion = sdm[startIndex].ToBool(),
      Temp = BitConverter.ToInt16(sdm, startIndex + 1).ToDouble() / 10.0
    };
  }

  public override string Serialize() => $"{this.Motion}|{this.State}|{this.Temp}";

  public static MotionTemp Deserialize(string version, string serialized)
  {
    MotionTemp motionTemp = new MotionTemp();
    if (string.IsNullOrEmpty(serialized))
    {
      motionTemp.Motion = false;
      motionTemp.State = 0;
      motionTemp.Temp = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length == 3)
      {
        motionTemp.Motion = strArray[0].ToBool();
        motionTemp.State = strArray[1].ToInt();
        motionTemp.Temp = strArray[2].ToDouble();
      }
      else
      {
        motionTemp.Motion = strArray[0].ToBool();
        motionTemp.State = strArray[0].ToInt();
        motionTemp.Temp = strArray[0].ToDouble();
      }
    }
    return motionTemp;
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) MotionTemp.Deserialize(version, serialized);
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

  public override object PlotValue
  {
    get
    {
      if (this.Temp <= -100.0 || this.Temp >= 300.0)
        return (object) null;
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.Temp : (object) this.Temp.ToFahrenheit();
    }
  }

  public override string NotificationString
  {
    get
    {
      string empty = string.Empty;
      string str = (this.State & 240 /*0xF0*/) != 0 ? "Low Battery, " : (this.Motion ? "Motion Detected, " : "No Motion, ");
      return this.Temp != -999.7 ? (this.Temp != -999.8 ? (this.Temp != -999.9 ? (this.Temp != -1000.0 && this.Temp != 125.0 ? (this.Temp <= -100.0 || this.Temp >= 300.0 ? str + "Invalid Temperature" : (this.CorF == null || !(this.CorF.Value == "C") ? str + this.Temp.ToFahrenheit().ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture) : str + this.Temp.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture))) : " Hardware Failure (Short)") : " No Sensor Detected") : " Hardware Failure (Short)") : " Hardware Failure (Empty)";
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
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
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
      ApplicationID = MotionTemp.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion,
      Scale = MotionTemp.IsFahrenheit(sensor.SensorID) ? "F" : "C"
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
    bool flag = MotionTemp.IsFahrenheit(sensor.SensorID);
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
      double result;
      if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
        MotionTemp.SetHystLower(sensor, (ushort) 0);
      if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]) && double.TryParse(collection["Hysteresis_Manual"], out result))
        MotionTemp.SetHystLower(sensor, Convert.ToUInt16(collection["Hysteresis_Manual"].ToDouble() * (flag ? 5.0 / 9.0 : 1.0) * 10.0));
      if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) && double.TryParse(collection["MinimumThreshold_Manual"], out result))
      {
        sensor.MinimumThreshold = ((flag ? collection["MinimumThreshold_Manual"].ToDouble().ToCelsius() : collection["MinimumThreshold_Manual"].ToDouble()) * 10.0).ToLong();
        if (sensor.MinimumThreshold < -400L)
          sensor.MinimumThreshold = -400L;
        if (sensor.MinimumThreshold > 1250L)
          sensor.MinimumThreshold = 1250L;
      }
      if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) && double.TryParse(collection["MaximumThreshold_Manual"], out result))
      {
        sensor.MaximumThreshold = ((flag ? collection["MaximumThreshold_Manual"].ToDouble().ToCelsius() : collection["MaximumThreshold_Manual"].ToDouble()) * 10.0).ToLong();
        if (sensor.MaximumThreshold < -400L)
          sensor.MaximumThreshold = -400L;
        if (sensor.MaximumThreshold > 1250L)
          sensor.MaximumThreshold = 1250L;
        if (sensor.MinimumThreshold > sensor.MaximumThreshold)
          sensor.MaximumThreshold = sensor.MinimumThreshold;
      }
      if (!string.IsNullOrWhiteSpace(collection["EventDetectionType_Manual"]))
      {
        switch (collection["EventDetectionType_Manual"])
        {
          case "Motion":
            MotionTemp.SetCalVal1SecondByte(sensor, (ushort) 0);
            break;
          case "No Motion":
            MotionTemp.SetCalVal1SecondByte(sensor, (ushort) 1);
            break;
          case "State Change":
            MotionTemp.SetCalVal1SecondByte(sensor, (ushort) 2);
            break;
        }
      }
      if (!string.IsNullOrWhiteSpace(collection["BiStable"]))
        MotionTemp.SetCalVal1ThirdByte(sensor, Convert.ToUInt16(collection["BiStable"]));
    }
  }

  public static string EventDetectionTypeDescription => "Aware State Determination ";

  public static string ValueForZero => "Motion";

  public static string ValueForOne => "No Motion";

  public static void SetCalVal1ThirdByte(Sensor sensor, ushort value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4278255615U;
    sensor.Calibration1 = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static ushort GetCalVal1ThirdByte(Sensor sensor)
  {
    return Convert.ToUInt16(Convert.ToUInt32(sensor.Calibration1 & 16711680L /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetCalVal1SecondByte(Sensor sensor, ushort value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4294902015U;
    sensor.Calibration1 = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 8));
  }

  public static ushort GetCalVal1TSecondByte(Sensor sensor)
  {
    return Convert.ToUInt16(Convert.ToUInt32(sensor.Calibration1 & 65280L) >> 8);
  }

  public static void SetHystUpper(Sensor sensor, ushort value)
  {
    uint num = (uint) (Convert.ToInt32(sensor.Hysteresis) & (int) ushort.MaxValue);
    sensor.Hysteresis = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static ushort GetHystUpper(Sensor sensor)
  {
    return Convert.ToUInt16(sensor.Hysteresis >> 16 /*0x10*/);
  }

  public static void SetHystLower(Sensor sensor, ushort value)
  {
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.Hysteresis) & 4294901760UL);
    sensor.Hysteresis = (long) (num | (uint) value);
  }

  public static double GetHystLower(Sensor sensor)
  {
    return (double) (sensor.Hysteresis & (long) ushort.MaxValue);
  }

  public static string HystForUI(Sensor sensor)
  {
    double num = sensor.Hysteresis == (long) uint.MaxValue ? 0.0 : Convert.ToDouble(MotionTemp.GetHystLower(sensor)) / 10.0;
    return MotionTemp.IsFahrenheit(sensor.SensorID) ? (num * 1.8).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    double Celsius = sensor.MinimumThreshold == 4294966896L ? -40.0 : Convert.ToDouble(sensor.MinimumThreshold) / 10.0;
    return MotionTemp.IsFahrenheit(sensor.SensorID) ? Celsius.ToFahrenheit().ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : Celsius.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    double Celsius = sensor.MaximumThreshold == (long) uint.MaxValue ? 125.0 : Convert.ToDouble(sensor.MaximumThreshold) / 10.0;
    return MotionTemp.IsFahrenheit(sensor.SensorID) ? Celsius.ToFahrenheit().ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : Celsius.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection returnData)
  {
    if (collection["TempScale"] == "on")
    {
      returnData["TempScale"] = "F";
      MotionTemp.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      returnData["TempScale"] = "C";
      MotionTemp.MakeCelsius(sensor.SensorID);
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (!sensor.IsWiFiSensor)
    {
      sensor.Calibration2 = !(collection["tempScale"] == "C") ? ((collection["actual"].ToDouble() - 32.0) * (5.0 / 9.0) * 10.0).ToLong() : (collection["actual"].ToDouble() * 10.0).ToLong();
      sensor.ProfileConfigDirty = false;
      sensor.ProfileConfig2Dirty = false;
      if (sensor.Calibration1 < sensor.DefaultValue<long>("DefaultMinimumThreshold") || sensor.Calibration1 > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
        throw new Exception("Calibration value out of range!");
      sensor.PendingActionControlCommand = true;
    }
    else if (sensor.LastDataMessage != null)
    {
      double num1 = sensor.LastDataMessage.Data.ToDouble();
      double num2 = !(collection["tempScale"] == "C") ? (collection["actual"].ToDouble() - 32.0) * (5.0 / 9.0) : collection["actual"].ToDouble();
      sensor.Calibration2 = ((num2 - num1) * 10.0).ToLong() + sensor.Calibration1;
    }
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      numArrayList.Add(MotionTempBase.CalibrateFrame(sensor.SensorID, (double) sensor.Calibration2 / 10.0));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(MotionTemp left, MotionTemp right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(MotionTemp left, MotionTemp right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(MotionTemp left, MotionTemp right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(MotionTemp left, MotionTemp right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(MotionTemp left, MotionTemp right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(MotionTemp left, MotionTemp right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is MotionTemp && this.Equals((MonnitApplicationBase) (obj as MotionTemp));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is MotionTemp && this.Temp == (right as MotionTemp).Temp;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is MotionTemp && this.Temp != (right as MotionTemp).Temp;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is MotionTemp && this.Temp < (right as MotionTemp).Temp;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is MotionTemp && this.Temp <= (right as MotionTemp).Temp;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is MotionTemp && this.Temp > (right as MotionTemp).Temp;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is MotionTemp && this.Temp >= (right as MotionTemp).Temp;
  }

  public static long DefaultMinThreshold => -400;

  public static long DefaultMaxThreshold => 1250;
}
