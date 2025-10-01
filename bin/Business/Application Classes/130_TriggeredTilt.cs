// Decompiled with JetBrains decompiler
// Type: Monnit.TriggeredTilt
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

public class TriggeredTilt : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _RorD;

  public static long MonnitApplicationID => TriggeredTiltBase.MonnitApplicationID;

  public static string ApplicationName => "Accelerometer - Tilt Detection";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => TriggeredTilt.MonnitApplicationID;

  public double Pitch { get; set; }

  public int RiseFallTime { get; set; }

  public string DataState { get; set; }

  public int stsState { get; set; }

  public override string Serialize()
  {
    return $"{this.Pitch.ToString()}|{this.RiseFallTime.ToString()}|{this.DataState.ToString()}|{this.stsState.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.Angle, "Pitch", this.Pitch),
        new AppDatum(eDatumType.Time, "RiseFallTime", this.RiseFallTime)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      this.PlotValue,
      (object) this.RiseFallTime
    };
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>() { "Pitch", "RiseFallTime" };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) TriggeredTilt.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      string notificationString = $" Pitch: {this.Pitch.ToString("#0.##°", (IFormatProvider) CultureInfo.InvariantCulture)}, Rise/Fall Time:{this.RiseFallTime.ToString("#0 ms", (IFormatProvider) CultureInfo.InvariantCulture)}";
      if (!string.IsNullOrEmpty(this.DataState))
        notificationString = $"{notificationString}, Data State: {this.DataState}";
      return notificationString;
    }
  }

  public static TriggeredTilt Deserialize(string version, string serialized)
  {
    TriggeredTilt triggeredTilt = new TriggeredTilt();
    if (string.IsNullOrEmpty(serialized))
    {
      triggeredTilt.Pitch = 0.0;
      triggeredTilt.RiseFallTime = 0;
      triggeredTilt.DataState = "";
      triggeredTilt.stsState = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length > 1)
      {
        try
        {
          if (strArray.Length > 3)
          {
            triggeredTilt.Pitch = strArray[0].ToDouble();
            triggeredTilt.RiseFallTime = strArray[1].ToInt();
            triggeredTilt.DataState = strArray[2].ToString();
            try
            {
              triggeredTilt.stsState = strArray[3].ToInt();
            }
            catch
            {
              triggeredTilt.stsState = 0;
            }
          }
          else
          {
            triggeredTilt.Pitch = strArray[0].ToDouble();
            triggeredTilt.RiseFallTime = strArray[1].ToInt();
            try
            {
              triggeredTilt.stsState = strArray[2].ToInt();
            }
            catch
            {
              triggeredTilt.stsState = 0;
            }
          }
        }
        catch
        {
          triggeredTilt.stsState = 0;
          triggeredTilt.Pitch = 0.0;
          triggeredTilt.RiseFallTime = 0;
          triggeredTilt.DataState = "";
        }
      }
      else
      {
        triggeredTilt.Pitch = strArray[0].ToDouble();
        triggeredTilt.RiseFallTime = strArray[0].ToInt();
        triggeredTilt.DataState = "";
        triggeredTilt.stsState = 0;
      }
    }
    return triggeredTilt;
  }

  public static TriggeredTilt Create(byte[] sdm, int startIndex)
  {
    TriggeredTilt triggeredTilt = new TriggeredTilt();
    triggeredTilt.stsState = (int) sdm[startIndex - 1] >> 4;
    triggeredTilt.Pitch = Convert.ToDouble((double) BitConverter.ToInt16(sdm, startIndex) / 100.0);
    triggeredTilt.RiseFallTime = Convert.ToInt32(BitConverter.ToUInt16(sdm, startIndex + 2));
    triggeredTilt.DataState = "";
    if (sdm.Length > 4)
      triggeredTilt.DataState = TriggeredTilt.DataStateStringValue((int) sdm[startIndex + 4]);
    return triggeredTilt;
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

  public static string DataStateStringValue(int dataState)
  {
    return TriggeredTiltBase.DataStateStringValue(dataState);
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
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = TriggeredTilt.MonnitApplicationID,
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
    bool flag = TriggeredTilt.IsDegrees(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["BasicThreshold"]) && !string.IsNullOrEmpty(collection["BasicThresholdDirection"]) && collection["BasicThresholdDirection"] != "-1")
    {
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
      if (collection["BasicThresholdDirection"].ToInt() == 0)
        sensor.MinimumThreshold = ((flag ? collection["BasicThreshold"].ToDouble().ToRadian() : collection["BasicThreshold"].ToDouble()) * 100.0).ToLong();
      else if (collection["BasicThresholdDirection"].ToInt() == 1)
        sensor.MaximumThreshold = ((flag ? collection["BasicThreshold"].ToDouble().ToRadian() : collection["BasicThreshold"].ToDouble()) * 100.0).ToLong();
      sensor.MeasurementsPerTransmission = 6;
    }
    else
    {
      if (!string.IsNullOrEmpty(collection["minThreshold_Manual"]))
      {
        double num1 = collection["minThreshold_Manual"].ToDouble();
        double num2 = collection["maxThreshold_Manual"].ToDouble();
        if (num1 < -180.0)
          num1 = -180.0;
        if (num1 > 180.0)
          num1 = 180.0;
        if (num1 >= num2)
          num1 = num2 - 1.0;
        TriggeredTilt.SetDownThreshold(sensor, num1);
      }
      if (!string.IsNullOrEmpty(collection["maxThreshold_Manual"]))
      {
        double num3 = collection["minThreshold_Manual"].ToDouble();
        double num4 = collection["maxThreshold_Manual"].ToDouble();
        if (num4 < -180.0)
          num4 = -180.0;
        if (num4 > 180.0)
          num4 = 180.0;
        if (num4 <= num3)
          num4 = num3 + 1.0;
        TriggeredTilt.SetUpThreshold(sensor, num4);
      }
      if (!string.IsNullOrEmpty(collection["measurementStability_Manual"]))
      {
        int num = collection["measurementStability_Manual"].ToInt();
        if (num < 1)
          num = 1;
        if (num > 49)
          num = 49;
        TriggeredTilt.SetMeasurementStability(sensor, num);
      }
      if (!string.IsNullOrEmpty(collection["timeOut_Manual"]))
      {
        int num = collection["timeOut_Manual"].ToInt();
        if (num < 1)
          num = 1;
        if (num > 30)
          num = 30;
        TriggeredTilt.SetTimeout(sensor, num);
      }
      if (!string.IsNullOrEmpty(collection["AxisMode_Manual"]))
      {
        int num = collection["AxisMode_Manual"].ToInt();
        if (num < 0)
          num = 0;
        if (num > 5)
          num = 5;
        TriggeredTilt.SetAxisMode(sensor, num);
      }
      if (!string.IsNullOrEmpty(collection["DataMode_Manual"]))
      {
        int num = collection["DataMode_Manual"].ToInt();
        if (num < 0)
          num = 0;
        if (num > 1)
          num = 1;
        TriggeredTilt.SetDataMode(sensor, num);
      }
      if (!string.IsNullOrEmpty(collection["AwareMode_Manual"]))
      {
        int num = collection["AwareMode_Manual"].ToInt();
        if (num < 0)
          num = 0;
        if (num > 4)
          num = 4;
        TriggeredTilt.SetAwareMode(sensor, num);
      }
      if (!string.IsNullOrEmpty(collection["RearmTime_Manual"]))
      {
        int num = collection["RearmTime_Manual"].ToInt();
        if (num < 0)
          num = 0;
        if (num > 60)
          num = 60;
        TriggeredTilt.SetReArmTime(sensor, num);
      }
      if (!string.IsNullOrEmpty(collection["DeltaValue_Manual"]))
      {
        double num = collection["DeltaValue_Manual"].ToDouble();
        if (num < 0.5)
          num = 0.5;
        if (num > 90.0)
          num = 90.0;
        TriggeredTilt.SetDeltaValue(sensor, num);
      }
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    double num = collection["PitchOffset"].ToDouble();
    if (num < -179.99)
      num = -179.99;
    if (num > 179.99)
      num = 179.99;
    TriggeredTilt.SetOffset(sensor, num);
    sensor.ProfileConfigDirty = true;
    sensor.Save();
  }

  public static bool UseAppVersion2Settings(Sensor sensor)
  {
    return TriggeredTiltBase.UseAppVersion2Settings(new Version(sensor.FirmwareVersion));
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(TriggeredTilt left, TriggeredTilt right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(TriggeredTilt left, TriggeredTilt right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(TriggeredTilt left, TriggeredTilt right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(TriggeredTilt left, TriggeredTilt right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(TriggeredTilt left, TriggeredTilt right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(TriggeredTilt left, TriggeredTilt right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is TriggeredTilt && this.Equals((MonnitApplicationBase) (obj as TriggeredTilt));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is TriggeredTilt && this.Pitch == (right as TriggeredTilt).Pitch;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is TriggeredTilt && this.Pitch != (right as TriggeredTilt).Pitch;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is TriggeredTilt && this.Pitch < (right as TriggeredTilt).Pitch;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is TriggeredTilt && this.Pitch <= (right as TriggeredTilt).Pitch;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is TriggeredTilt && this.Pitch > (right as TriggeredTilt).Pitch;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is TriggeredTilt && this.Pitch >= (right as TriggeredTilt).Pitch;
  }

  public static double GetOffset(Sensor sens)
  {
    return Math.Round((double) (short) (sens.Hysteresis & (long) ushort.MaxValue) / 100.0, 2);
  }

  public static void SetOffset(Sensor sens, double value)
  {
    value *= 100.0;
    int int32 = Convert.ToInt32(sens.Hysteresis & 4294901760L);
    sens.Hysteresis = (long) (int32 | (int) value & (int) ushort.MaxValue);
  }

  public static void SetDeltaValue(Sensor sens, double value)
  {
    value *= 10.0;
    uint num = Convert.ToUInt32(sens.Hysteresis) & (uint) ushort.MaxValue;
    sens.Hysteresis = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static double GetDeltaValue(Sensor sens)
  {
    return Convert.ToDouble((double) Convert.ToInt32((sens.Hysteresis & 4294901760L) >> 16 /*0x10*/) / 10.0);
  }

  public static void SetDownThreshold(Sensor sens, double value)
  {
    sens.MinimumThreshold = (value * 100.0).ToLong();
  }

  public static double GetDownThreshold(Sensor sens)
  {
    return Math.Round((double) sens.MinimumThreshold / 100.0, 2);
  }

  public static void SetUpThreshold(Sensor sens, double value)
  {
    sens.MaximumThreshold = (value * 100.0).ToLong();
  }

  public static double GetUpThreshold(Sensor sens)
  {
    return Math.Round((double) sens.MaximumThreshold / 100.0, 2);
  }

  public static int GetMeasurementStability(Sensor sens)
  {
    return Convert.ToInt32(sens.Calibration2 & (long) ushort.MaxValue);
  }

  public static void SetMeasurementStability(Sensor sens, int value)
  {
    uint uint32 = Convert.ToUInt32(sens.Calibration2 & 4294901760L);
    sens.Calibration2 = (long) (uint32 | (uint) value);
  }

  public static void SetTimeout(Sensor sens, int value)
  {
    value *= 1000;
    uint num = Convert.ToUInt32(sens.Calibration2) & (uint) ushort.MaxValue;
    sens.Calibration2 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int GetTimeout(Sensor sens)
  {
    return Convert.ToInt32((sens.Calibration2 & 4294901760L) >> 16 /*0x10*/) / 1000;
  }

  public static int GetAxisMode(Sensor sens)
  {
    return Convert.ToInt32(sens.Calibration3 & (long) ushort.MaxValue);
  }

  public static void SetAxisMode(Sensor sens, int value)
  {
    uint uint32 = Convert.ToUInt32(sens.Calibration3 & 4294901760L);
    sens.Calibration3 = (long) (uint32 | (uint) value);
  }

  public static void SetAwareMode(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration4) & 4294967040U;
    sensor.Calibration4 = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetAwareMode(Sensor sensor)
  {
    return (int) Convert.ToUInt16(Convert.ToUInt32(sensor.Calibration4 & (long) byte.MaxValue));
  }

  public static void SetDataMode(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration4) & 4294902015U;
    sensor.Calibration4 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static int GetDataMode(Sensor sensor)
  {
    return (int) Convert.ToUInt16(Convert.ToUInt32(sensor.Calibration4 & 65280L) >> 8);
  }

  public static void SetReArmTime(Sensor sens, int value)
  {
    uint num = Convert.ToUInt32(sens.Calibration4) & (uint) ushort.MaxValue;
    sens.Calibration4 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int GetReArmTime(Sensor sens)
  {
    return (int) Convert.ToUInt32((sens.Calibration4 & 4294901760L) >> 16 /*0x10*/);
  }

  public static long DefaultMinThreshold => -180;

  public static long DefaultMaxThreshold => 180;

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
