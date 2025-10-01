// Decompiled with JetBrains decompiler
// Type: Monnit.VibrationMeter
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class VibrationMeter : MonnitApplicationBase
{
  public static long MonnitApplicationID => VibrationMeterBase.MonnitApplicationID;

  public static string ApplicationName => "Vibration Meter";

  public static eApplicationProfileType ProfileType => VibrationMeterBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => VibrationMeter.MonnitApplicationID;

  public double xSpeed { get; set; }

  public double ySpeed { get; set; }

  public double zSpeed { get; set; }

  public double xFrequency { get; set; }

  public double yFrequency { get; set; }

  public double zFrequency { get; set; }

  public double DutyCycle { get; set; }

  public int state { get; set; }

  public int AvgEnabled { get; set; }

  public int MeasurementMethod { get; set; }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[7]
      {
        new AppDatum(eDatumType.Speed, "X-Axis Speed", this.xSpeed),
        new AppDatum(eDatumType.Speed, "Y-Axis Speed", this.ySpeed),
        new AppDatum(eDatumType.Speed, "Z-Axis Speed", this.zSpeed),
        new AppDatum(eDatumType.Frequency, "X-Axis Frequency", this.xFrequency),
        new AppDatum(eDatumType.Frequency, "Y-Axis Frequency", this.yFrequency),
        new AppDatum(eDatumType.Frequency, "Z-Axis Frequency", this.zFrequency),
        new AppDatum(eDatumType.Percentage, "DutyCycle", this.DutyCycle)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[7]
    {
      this.PlotValue,
      (object) this.ySpeed,
      (object) this.zSpeed,
      (object) this.xFrequency,
      (object) this.yFrequency,
      (object) this.zFrequency,
      (object) this.DutyCycle
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[7]
    {
      "X-Axis Speed",
      "Y-Axis Speed",
      "Z-Axis Speed",
      "X-Axis Frequency",
      "Y-Axis Frequency",
      "Z-Axis Frequency",
      "Duty Cycle"
    });
  }

  public override string Serialize()
  {
    return $"{this.xSpeed.ToString()}|{this.ySpeed.ToString()}|{this.zSpeed.ToString()}|{this.xFrequency.ToString()}|{this.yFrequency.ToString()}|{this.zFrequency.ToString()}|{this.DutyCycle.ToString()}|{this.state.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) VibrationMeter.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      int state = this.state;
      if ((this.state & 16 /*0x10*/) == 16 /*0x10*/)
        return "Hardware Failure";
      return $"X-Axis {this.PlotValue.ToDouble()} mm/s {this.xFrequency} Hz, Y-Axis {this.ySpeed} mm/s {this.yFrequency} Hz, Z-Axis {this.zSpeed} mm/s {this.zFrequency} Hz, Duty Cycle {this.DutyCycle} %";
    }
  }

  public override object PlotValue => (object) this.xSpeed;

  public static string SpecialExportValue(MonnitApplicationBase app) => string.Format("");

  public static VibrationMeter Deserialize(string version, string serialized)
  {
    VibrationMeter vibrationMeter = new VibrationMeter();
    if (string.IsNullOrEmpty(serialized))
    {
      vibrationMeter.xSpeed = 0.0;
      vibrationMeter.ySpeed = 0.0;
      vibrationMeter.zSpeed = 0.0;
      vibrationMeter.xFrequency = 0.0;
      vibrationMeter.yFrequency = 0.0;
      vibrationMeter.zFrequency = 0.0;
      vibrationMeter.DutyCycle = 0.0;
      vibrationMeter.state = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      vibrationMeter.xSpeed = strArray[0].ToDouble();
      if (strArray.Length == 7 || strArray.Length == 8)
      {
        vibrationMeter.ySpeed = strArray[1].ToDouble();
        vibrationMeter.zSpeed = strArray[2].ToDouble();
        vibrationMeter.xFrequency = strArray[3].ToDouble();
        vibrationMeter.yFrequency = strArray[4].ToDouble();
        vibrationMeter.zFrequency = strArray[5].ToDouble();
        vibrationMeter.DutyCycle = strArray[6].ToDouble();
        try
        {
          vibrationMeter.state = strArray[7].ToInt();
        }
        catch
        {
          vibrationMeter.state = 0;
        }
      }
      else
      {
        vibrationMeter.ySpeed = strArray[0].ToDouble();
        vibrationMeter.zSpeed = strArray[0].ToDouble();
        vibrationMeter.xFrequency = strArray[0].ToDouble();
        vibrationMeter.yFrequency = strArray[0].ToDouble();
        vibrationMeter.zFrequency = strArray[0].ToDouble();
        vibrationMeter.DutyCycle = strArray[0].ToDouble();
        vibrationMeter.state = 0;
      }
    }
    return vibrationMeter;
  }

  public static VibrationMeter Create(byte[] sdm, int startIndex)
  {
    VibrationMeter vibrationMeter = new VibrationMeter();
    vibrationMeter.state = (int) sdm[startIndex - 1];
    if (sdm.Length > 8)
    {
      vibrationMeter.xFrequency = sdm[startIndex + 3].ToDouble();
      vibrationMeter.yFrequency = sdm[startIndex + 4].ToDouble();
      vibrationMeter.zFrequency = sdm[startIndex + 5].ToDouble();
      vibrationMeter.DutyCycle = sdm[startIndex + 6].ToDouble();
      vibrationMeter.xSpeed = (double) BitConverter.ToUInt16(sdm, startIndex + 7) / 10.0;
      vibrationMeter.ySpeed = (double) BitConverter.ToUInt16(sdm, startIndex + 9) / 10.0;
      vibrationMeter.zSpeed = (double) BitConverter.ToUInt16(sdm, startIndex + 11) / 10.0;
      byte num = sdm[startIndex + 13];
      vibrationMeter.AvgEnabled = (int) num & 15;
      vibrationMeter.MeasurementMethod = (int) num >> 4;
    }
    else
    {
      vibrationMeter.xSpeed = (double) sdm[startIndex] / 10.0;
      vibrationMeter.ySpeed = (double) sdm[startIndex + 1] / 10.0;
      vibrationMeter.zSpeed = (double) sdm[startIndex + 2] / 10.0;
      vibrationMeter.xFrequency = sdm[startIndex + 3].ToDouble();
      vibrationMeter.yFrequency = sdm[startIndex + 4].ToDouble();
      vibrationMeter.zFrequency = sdm[startIndex + 5].ToDouble();
      vibrationMeter.DutyCycle = sdm[startIndex + 6].ToDouble();
    }
    return vibrationMeter;
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

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      SnoozeDuration = 60,
      CompareValue = "0",
      AccountID = sensor.AccountID,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = VibrationMeter.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    double result1 = 0.0;
    int result2 = 0;
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) && int.TryParse(collection["MinimumThreshold_Manual"], out result2))
    {
      if (result2 < 0)
        result2 = 0;
      if (result2 > 2)
        result2 = 2;
      VibrationMeter.SetWindowFunction(sensor, result2);
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) && int.TryParse(collection["MaximumThreshold_Manual"], out result2))
    {
      if (result2 < 0)
        result2 = 0;
      if (result2 > 2000)
        result2 = 2000;
      sensor.MaximumThreshold = (long) result2;
    }
    if (!string.IsNullOrEmpty(collection["CalVal2_Manual"]) && double.TryParse(collection["CalVal2_Manual"], out result1))
      sensor.Calibration2 = result1 >= 1.0 ? (result1 <= sensor.ReportInterval ? (result1 * 60.0).ToLong() : (sensor.ReportInterval * 60.0).ToLong()) : 60L;
    if (!string.IsNullOrEmpty(collection["CalVal4_Manual"]) && double.TryParse(collection["CalVal4_Manual"], out result1))
      sensor.Calibration4 = result1 >= 1.0 ? (result1 <= (double) sbyte.MaxValue ? result1.ToLong() : (long) sbyte.MaxValue) : 1L;
    if (new Version(sensor.FirmwareVersion) >= new Version("16.34.22.5"))
    {
      if (!string.IsNullOrEmpty(collection["EnableAveraging"]))
      {
        int num = collection["EnableAveraging"].ToInt();
        if (num < 0)
          num = 0;
        if (num > 1)
          num = 1;
        VibrationMeter.SetEnableAveraging(sensor, num);
      }
      if (!string.IsNullOrEmpty(collection["VibrationAwareThreshold"]))
      {
        int num = (collection["VibrationAwareThreshold"].ToDouble() * 10.0).ToInt();
        if (num < 0)
          num = 0;
        if (num > 6000)
          num = 6000;
        VibrationMeter.SetVibrationAwareThreshold(sensor, num);
      }
      if (!string.IsNullOrEmpty(collection["VibrationAwareBuffer"]))
      {
        int num = (collection["VibrationAwareBuffer"].ToDouble() * 10.0).ToInt();
        if (num < 0)
          num = 0;
        if (num > 3000)
          num = 3000;
        VibrationMeter.SetVibrationAwareBuffer(sensor, num);
      }
    }
    if (!string.IsNullOrEmpty(collection["BasicThreshold"]) && !string.IsNullOrEmpty(collection["BasicThresholdDirection"]) && collection["BasicThresholdDirection"] != "-1")
      return;
    if (!string.IsNullOrEmpty(collection["CalVal3_Manual"]) && double.TryParse(collection["CalVal3_Manual"], out result1))
      sensor.Calibration3 = result1 >= 0.0 ? (result1 <= (double) sbyte.MaxValue ? result1.ToLong() : (long) sbyte.MaxValue) : 0L;
    if (!string.IsNullOrEmpty(collection["CalVal1_Manual"]) && double.TryParse(collection["CalVal1_Manual"], out result1))
    {
      if (result1 < 0.0)
        result1 = 0.0;
      if (result1 > 2.0)
        result1 = 2.0;
      VibrationMeter.SetMeasurementMethod(sensor, result1.ToInt());
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    NonCachedAttribute nonCachedAttribute1 = NonCachedAttribute.LoadBySensorIDAndName(sensor.SensorID, "Calibration");
    if (nonCachedAttribute1 == null)
    {
      nonCachedAttribute1 = new NonCachedAttribute();
      nonCachedAttribute1.SensorID = sensor.SensorID;
      nonCachedAttribute1.Name = "Calibration";
    }
    nonCachedAttribute1.Value1 = string.IsNullOrWhiteSpace(collection["acc"]) ? "3" : collection["acc"];
    double num;
    if (!string.IsNullOrWhiteSpace(collection["observed"]))
    {
      NonCachedAttribute nonCachedAttribute2 = nonCachedAttribute1;
      num = collection["observed"].ToDouble() * 100.0;
      string str = num.ToString();
      nonCachedAttribute2.Value2 = str;
    }
    else
      nonCachedAttribute1.Value2 = "-1";
    if (!string.IsNullOrWhiteSpace(collection["actual"]))
    {
      NonCachedAttribute nonCachedAttribute3 = nonCachedAttribute1;
      num = collection["actual"].ToDouble() * 100.0;
      string str = num.ToString();
      nonCachedAttribute3.Value3 = str;
    }
    else
      nonCachedAttribute1.Value3 = "-1";
    nonCachedAttribute1.Save();
    sensor.ProfileConfig2Dirty = false;
    sensor.ProfileConfigDirty = false;
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    NonCachedAttribute nonCachedAttribute = NonCachedAttribute.LoadBySensorIDAndName(sensor.SensorID, "Calibration");
    int acc = nonCachedAttribute.Value1.ToInt();
    int oldVal = nonCachedAttribute.Value2.ToInt();
    int newVal = nonCachedAttribute.Value3.ToInt();
    numArrayList.Add(CurrentZeroToOneFiftyAmpBase.CalibrateFrame(sensor.SensorID, acc, oldVal, newVal));
    numArrayList.Add(sensor.ReadProfileConfig(29));
    return numArrayList;
  }

  public static string HystForUI(Sensor sensor) => "Not Used";

  public static string MinThreshForUI(Sensor sensor) => sensor.MinimumThreshold.ToString();

  public static string MaxThreshForUI(Sensor sensor) => sensor.Calibration4.ToString();

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(VibrationMeter left, VibrationMeter right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(VibrationMeter left, VibrationMeter right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(VibrationMeter left, VibrationMeter right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(VibrationMeter left, VibrationMeter right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(VibrationMeter left, VibrationMeter right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(VibrationMeter left, VibrationMeter right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is VibrationMeter && this.Equals((MonnitApplicationBase) (obj as VibrationMeter));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is VibrationMeter && this.xSpeed == (right as VibrationMeter).xSpeed;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is VibrationMeter && this.xSpeed != (right as VibrationMeter).xSpeed;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is VibrationMeter && this.xSpeed < (right as VibrationMeter).xSpeed;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is VibrationMeter && this.xSpeed <= (right as VibrationMeter).xSpeed;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is VibrationMeter && this.xSpeed > (right as VibrationMeter).xSpeed;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is VibrationMeter && this.xSpeed >= (right as VibrationMeter).xSpeed;
  }

  public static void SetWindowFunction(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4294967040U;
    sensor.MinimumThreshold = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetWindowFunction(Sensor sensor)
  {
    return (int) Convert.ToUInt16(Convert.ToUInt32(sensor.MinimumThreshold & (long) byte.MaxValue));
  }

  public static void SetCompensationFunctionEnable(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4294902015U;
    sensor.MinimumThreshold = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static int GetCompensationFunctionEnable(Sensor sensor)
  {
    return (int) Convert.ToUInt16(Convert.ToUInt32(sensor.MinimumThreshold & 65280L) >> 8);
  }

  public static void SetMeasurementMethod(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4294967040U;
    sensor.Calibration1 = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetMeasurementMethod(Sensor sensor)
  {
    return (int) Convert.ToUInt16(Convert.ToUInt32(sensor.Calibration1 & (long) byte.MaxValue));
  }

  public static void SetEnableAveraging(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4294902015U;
    sensor.Calibration1 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static int GetEnableAveraging(Sensor sensor)
  {
    return (int) Convert.ToUInt16(Convert.ToUInt32(sensor.Calibration1 & 65280L) >> 8);
  }

  public static void SetVibrationAwareThreshold(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & (uint) ushort.MaxValue;
    sensor.Calibration1 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int GetVibrationAwareThreshold(Sensor sensor)
  {
    return Convert.ToInt32((sensor.Calibration1 & 4294901760L) >> 16 /*0x10*/);
  }

  public static void SetVibrationIntensityThreshold(Sensor sensor, int value)
  {
    uint uint32 = Convert.ToUInt32(sensor.Calibration3 & 4294901760L);
    sensor.Calibration3 = (long) (uint32 | (uint) value);
  }

  public static int GetVibrationIntensityThreshold(Sensor sensor)
  {
    return Convert.ToInt32(sensor.Calibration3 & (long) ushort.MaxValue);
  }

  public static void SetVibrationAwareBuffer(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration3) & (uint) ushort.MaxValue;
    sensor.Calibration3 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int GetVibrationAwareBuffer(Sensor sensor)
  {
    return Convert.ToInt32((sensor.Calibration3 & 4294901760L) >> 16 /*0x10*/);
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 0;
}
