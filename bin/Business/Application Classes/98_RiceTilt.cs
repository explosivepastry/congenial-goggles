// Decompiled with JetBrains decompiler
// Type: Monnit.RiceTilt
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

public class RiceTilt : MonnitApplicationBase, ISensorAttribute
{
  private bool isNotification = false;
  private SensorAttribute _RorD;

  public static long MonnitApplicationID => RiceTiltBase.MonnitApplicationID;

  public static string ApplicationName => "Rice Tilt";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => RiceTilt.MonnitApplicationID;

  public double Pitch { get; set; }

  public long Cycle { get; set; }

  public int RiseFallTime { get; set; }

  public override string Serialize()
  {
    return $"{this.Pitch.ToString()},{this.Cycle.ToString()},{this.RiseFallTime.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[3]
      {
        new AppDatum(eDatumType.Angle, "Pitch", this.Pitch),
        new AppDatum(eDatumType.Count, "Cycle", this.Cycle),
        new AppDatum(eDatumType.Time, "RiseFallTime", this.RiseFallTime)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      this.PlotValue,
      (object) this.Cycle,
      (object) this.RiseFallTime
    };
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>()
    {
      "Pitch",
      "Cycle",
      "RiseFallTime"
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) RiceTilt.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      return $" Pitch: {this.Pitch.ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture)} Rise/Fall Time:{this.RiseFallTime.ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture)}";
    }
  }

  public static RiceTilt Deserialize(string version, string serialized)
  {
    RiceTilt riceTilt = new RiceTilt();
    if (string.IsNullOrEmpty(serialized))
    {
      riceTilt.Pitch = 0.0;
      riceTilt.Cycle = 0L;
      riceTilt.RiseFallTime = 0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      riceTilt.Pitch = Convert.ToDouble(strArray[0]).ToDouble();
      if (strArray.Length > 1)
      {
        try
        {
          riceTilt.Cycle = strArray[1].ToLong();
          riceTilt.RiseFallTime = strArray[2].ToInt();
        }
        catch
        {
          riceTilt.Cycle = 0L;
          riceTilt.RiseFallTime = 0;
        }
      }
    }
    return riceTilt;
  }

  public static RiceTilt Create(byte[] sdm, int startIndex)
  {
    return new RiceTilt()
    {
      Pitch = Convert.ToDouble((double) BitConverter.ToInt16(sdm, startIndex) / 100.0),
      Cycle = Convert.ToInt64(BitConverter.ToUInt32(sdm, startIndex + 2)),
      RiseFallTime = Convert.ToInt32(BitConverter.ToUInt16(sdm, startIndex + 6))
    };
  }

  public static bool IsDegrees(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "RorD" && sensorAttribute.Value == "R")
        return false;
    }
    return true;
  }

  public static void MakeDegree(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "RorD")
        sensorAttribute.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void MakeRadian(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "RorD")
      {
        if (sensorAttribute.Value != "R")
        {
          sensorAttribute.Value = "R";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "RorD",
        Value = "R",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute RorD => this._RorD;

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "RorD")
        this._RorD = sensorAttribute;
    }
  }

  public override object PlotValue => (object) this.Pitch;

  public new static void DefaultConfigurationSettings(Sensor sensor)
  {
    sensor.ReportInterval = (double) sensor.DefaultValue<long>("DefaultReportInterval");
    sensor.ActiveStateInterval = (double) sensor.DefaultValue<long>("DefaultActiveStateInterval");
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
    sensor.BSNUrgentDelay = sensor.DefaultValue<int>("DefaultBSNUrgentDelay");
    sensor.UrgentRetries = sensor.DefaultValue<int>("DefaultUrgentRetries");
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
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = RiceTilt.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static Dictionary<string, string> NotificationScaleValues()
  {
    return new Dictionary<string, string>()
    {
      {
        "D",
        "Degree"
      },
      {
        "R",
        "Radian"
      }
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    RiceTilt.IsDegrees(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["BasicThreshold"]) && !string.IsNullOrEmpty(collection["BasicThresholdDirection"]) && collection["BasicThresholdDirection"] != "-1")
    {
      sensor.MeasurementsPerTransmission = 6;
    }
    else
    {
      if (!string.IsNullOrEmpty(collection["motionThreshold_Manual"]))
      {
        int num = collection["motionThreshold_Manual"].ToInt();
        if (num < 2)
          num = 2;
        if (num > (int) sbyte.MaxValue)
          num = (int) sbyte.MaxValue;
        RiceTilt.SetMotionThreshold(sensor, num);
      }
      if (!string.IsNullOrEmpty(collection["NPmotionThreshold_Manual"]))
      {
        int num = collection["NPmotionThreshold_Manual"].ToInt();
        if (num < 2)
          num = 2;
        if (num > (int) sbyte.MaxValue)
          num = (int) sbyte.MaxValue;
        RiceTilt.SetNoParentMotionThreshold(sensor, num);
      }
      if (!string.IsNullOrEmpty(collection["ChannelMask_Manual"]))
        sensor.ChannelMask = collection["ChannelMask_Manual"].ToLong();
      if (!string.IsNullOrEmpty(collection["timeOut_Manual"]))
      {
        int num = collection["timeOut_Manual"].ToInt();
        if (num < 1000)
          num = 1000;
        if (num > 30000)
          num = 30000;
        RiceTilt.SetTimeout(sensor, num);
      }
      if (!string.IsNullOrEmpty(collection["StuckPollInterval_Manual"]))
      {
        RiceTilt.GetTimeout(sensor);
        int num = collection["StuckPollInterval_Manual"].ToInt();
        if (num < 100)
          num = 100;
        if (num > 30000)
          num = 30000;
        RiceTilt.SetMeasurementFrequency(sensor, num);
      }
      if (!string.IsNullOrEmpty(collection["minThreshold_Manual"]))
      {
        int num = collection["minThreshold_Manual"].ToInt();
        if (num < 0)
          num = 0;
        if (num > 90)
          num = 90;
        sensor.MinimumThreshold = (long) (num * 100);
      }
      if (!string.IsNullOrEmpty(collection["maxThreshold_Manual"]))
      {
        int num = collection["maxThreshold_Manual"].ToInt();
        if (num < 0)
          num = 0;
        if (num > 90)
          num = 90;
        sensor.MaximumThreshold = (long) (num * 100);
      }
      if (sensor.MinimumThreshold > sensor.MaximumThreshold)
        sensor.MaximumThreshold = sensor.MinimumThreshold;
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    double num = collection["lastReading"].ToDouble();
    long hysteresis = sensor.Hysteresis;
    sensor.Hysteresis = ((double) hysteresis + num * -100.0).ToLong();
    sensor.ProfileConfigDirty = true;
    sensor.Save();
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : Convert.ToDouble(sensor.Hysteresis).ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MinThreshForUI(Sensor sensor) => (sensor.MinimumThreshold / 100L).ToString();

  public static string MaxThreshForUI(Sensor sensor) => (sensor.MaximumThreshold / 100L).ToString();

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(RiceTilt left, RiceTilt right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(RiceTilt left, RiceTilt right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(RiceTilt left, RiceTilt right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(RiceTilt left, RiceTilt right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(RiceTilt left, RiceTilt right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(RiceTilt left, RiceTilt right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is RiceTilt && this.Equals((MonnitApplicationBase) (obj as RiceTilt));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is RiceTilt && this.Pitch == (right as RiceTilt).Pitch;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is RiceTilt && this.Pitch != (right as RiceTilt).Pitch;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is RiceTilt && this.Pitch < (right as RiceTilt).Pitch;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is RiceTilt && this.Pitch <= (right as RiceTilt).Pitch;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is RiceTilt && this.Pitch > (right as RiceTilt).Pitch;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is RiceTilt && this.Pitch >= (right as RiceTilt).Pitch;
  }

  public static void SetPitchMin(Sensor sens, int value)
  {
    int num = (int) sens.MinimumThreshold & (int) ushort.MaxValue;
    sens.MinimumThreshold = (long) (num | (int) (ushort) value << 16 /*0x10*/);
  }

  public static int GetPitchMin(Sensor sens)
  {
    return (int) (short) (ushort) ((uint) (int) ((long) (int) sens.MinimumThreshold & 4294901760L) >> 16 /*0x10*/);
  }

  public static void SetPitchMax(Sensor sens, int value)
  {
    int num = (int) ((long) (int) sens.MinimumThreshold & 4294901760L);
    sens.MinimumThreshold = (long) (num | (int) (ushort) value);
  }

  public static int GetPitchMax(Sensor sens)
  {
    return (int) (short) (ushort) ((uint) (int) sens.MinimumThreshold & (uint) ushort.MaxValue);
  }

  public static void SetRollMin(Sensor sens, int value)
  {
    int num = (int) sens.MaximumThreshold & (int) ushort.MaxValue;
    sens.MaximumThreshold = (long) (num | (int) (ushort) value << 16 /*0x10*/);
  }

  public static int GetRollMin(Sensor sens)
  {
    return (int) (short) (ushort) ((uint) (int) ((long) (int) sens.MaximumThreshold & 4294901760L) >> 16 /*0x10*/);
  }

  public static void SetRollMax(Sensor sens, int value)
  {
    int num = (int) ((long) (int) sens.MaximumThreshold & 4294901760L);
    sens.MaximumThreshold = (long) (num | (int) (ushort) value);
  }

  public static int GetRollMax(Sensor sens)
  {
    return (int) (short) (ushort) ((uint) (int) sens.MaximumThreshold & (uint) ushort.MaxValue);
  }

  public static void SetMotionThreshold(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4294967040U;
    sensor.Calibration1 = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetMotionThreshold(Sensor sensor)
  {
    return (int) Convert.ToUInt32(sensor.Calibration1) & (int) byte.MaxValue;
  }

  public static void SetNoParentMotionThreshold(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4294902015U;
    sensor.Calibration1 = (long) (num | (uint) (value << 8));
  }

  public static int GetNoParentMotionThreshold(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration1) & 65280U) >> 8);
  }

  public static void SetControlRegOne(Sensor sensor, double value)
  {
    int num1 = Math.Round(value).ToInt();
    uint num2 = Convert.ToUInt32(sensor.Calibration1) & 4278255615U;
    sensor.Calibration1 = (long) (num2 | (uint) (num1 << 16 /*0x10*/));
  }

  public static int GetControlRegOne(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration1) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetMotionDebounceCount(Sensor sensor, double value)
  {
    int num1 = Math.Round(value).ToInt();
    uint num2 = Convert.ToUInt32(sensor.Calibration1) & 16777215U /*0xFFFFFF*/;
    sensor.Calibration1 = (long) (num2 | (uint) (num1 << 24));
  }

  public static int GetMotionDebounceCount(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration1) & 4278190080U /*0xFF000000*/) >> 24);
  }

  public static void SetMeasurementFrequency(Sensor sens, int value)
  {
    int num = (int) ((long) (int) sens.Calibration2 & 4294901760L);
    sens.Calibration2 = (long) (num | (int) (ushort) value);
  }

  public static int GetMeasurementFrequency(Sensor sens)
  {
    return (int) (short) (int) (sens.Calibration2 & (long) ushort.MaxValue);
  }

  public static void SetTimeout(Sensor sens, int value)
  {
    int num = (int) sens.Calibration2 & (int) ushort.MaxValue;
    sens.Calibration2 = (long) (num | (int) (ushort) value << 16 /*0x10*/);
  }

  public static int GetTimeout(Sensor sens)
  {
    return (int) (short) (ushort) ((uint) (int) ((long) (int) sens.Calibration2 & 4294901760L) >> 16 /*0x10*/);
  }

  public static void SetDownThreshold(Sensor sens, int value)
  {
    int num = (int) sens.Calibration3 & (int) ushort.MaxValue;
    sens.Calibration3 = (long) (num | (int) (ushort) value << 16 /*0x10*/);
  }

  public static int GetDownThreshold(Sensor sens)
  {
    return (int) (short) (ushort) ((uint) (int) ((long) (int) sens.Calibration3 & 4294901760L) >> 16 /*0x10*/);
  }

  public static void SetUpThreshold(Sensor sens, int value)
  {
    int num = (int) ((long) (int) sens.Calibration3 & 4294901760L);
    sens.Calibration3 = (long) (num | (int) (ushort) value);
  }

  public static int GetUpThreshold(Sensor sens)
  {
    return (int) (short) (ushort) ((uint) (int) sens.Calibration3 & (uint) ushort.MaxValue);
  }

  public static void SetStableMinThresh(Sensor sens, int value)
  {
    int num = (int) sens.Calibration4 & (int) ushort.MaxValue;
    sens.Calibration4 = (long) (num | (int) (ushort) value << 16 /*0x10*/);
  }

  public static int GetStableMinThresh(Sensor sens)
  {
    return (int) (short) (ushort) ((uint) (int) ((long) (int) sens.Calibration4 & 4294901760L) >> 16 /*0x10*/);
  }

  public static void SetStableMaxThresh(Sensor sens, int value)
  {
    int num = (int) ((long) (int) sens.Calibration4 & 4294901760L);
    sens.Calibration4 = (long) (num | (int) (ushort) value);
  }

  public static int GetStableMaxThresh(Sensor sens)
  {
    return (int) (short) (ushort) ((uint) (int) sens.Calibration4 & (uint) ushort.MaxValue);
  }

  public static long DefaultMinThreshold => -180;

  public static long DefaultMaxThreshold => 180;

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.Hysteresis = 0L;
    sensor.ProfileConfigDirty = true;
    sensor.Save();
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }
}
