// Decompiled with JetBrains decompiler
// Type: Monnit.AccelerometerTriggered
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

public class AccelerometerTriggered : MonnitApplicationBase
{
  private SensorAttribute _ChartFormat;

  public static long MonnitApplicationID => 16 /*0x10*/;

  public static string ApplicationName => "Accelerometer - Impact";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Point";

  public override long ApplicationID => AccelerometerTriggered.MonnitApplicationID;

  public bool ForceDetected { get; set; }

  public int Xval { get; set; }

  public int Yval { get; set; }

  public int Zval { get; set; }

  public int state { get; set; }

  public override string Serialize()
  {
    return $"{this.Xval.ToString()},{this.Yval.ToString()},{this.Zval.ToString()},{this.state.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[4]
      {
        new AppDatum(eDatumType.AccelerometerImpact, "ForceDetected", this.ForceDetected),
        new AppDatum(eDatumType.Geforce, "Xval", this.Xval),
        new AppDatum(eDatumType.Geforce, "Yval", this.Yval),
        new AppDatum(eDatumType.Geforce, "Zval", this.Zval)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[4]
    {
      (object) this.ForceDetected,
      (object) this.Xval,
      (object) this.Yval,
      (object) this.Zval
    });
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) AccelerometerTriggered.Deserialize(version, serialized);
  }

  public override bool IsValid => (this.state & 16 /*0x10*/) != 16 /*0x10*/;

  public override string NotificationString
  {
    get
    {
      if ((this.state & 16 /*0x10*/) == 16 /*0x10*/)
        return "Hardware Failure!";
      return this.ForceDetected ? "Force Detected" : "No Force Detected";
    }
  }

  public static AccelerometerTriggered Deserialize(string version, string serialized)
  {
    AccelerometerTriggered accelerometerTriggered = new AccelerometerTriggered();
    if (string.IsNullOrEmpty(serialized))
    {
      accelerometerTriggered.Xval = 0;
      accelerometerTriggered.Yval = 0;
      accelerometerTriggered.Zval = 0;
      accelerometerTriggered.state = 0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      accelerometerTriggered.Xval = strArray[0].ToInt();
      if (strArray.Length == 3 || strArray.Length == 4)
      {
        accelerometerTriggered.Yval = strArray[1].ToInt();
        accelerometerTriggered.Zval = strArray[2].ToInt();
        try
        {
          accelerometerTriggered.state = strArray[3].ToInt();
        }
        catch
        {
          accelerometerTriggered.state = 0;
        }
      }
      else
      {
        accelerometerTriggered.Yval = accelerometerTriggered.Xval;
        accelerometerTriggered.Zval = accelerometerTriggered.Xval;
      }
      if (accelerometerTriggered.Xval != 0 || accelerometerTriggered.Yval != 0 || accelerometerTriggered.Zval != 0)
        accelerometerTriggered.ForceDetected = true;
    }
    return accelerometerTriggered;
  }

  public static AccelerometerTriggered Create(byte[] sdm, int startIndex)
  {
    AccelerometerTriggered accelerometerTriggered = new AccelerometerTriggered();
    accelerometerTriggered.state = (int) sdm[startIndex - 1];
    if ((accelerometerTriggered.state & 16 /*0x10*/) == 16 /*0x10*/)
    {
      accelerometerTriggered.ForceDetected = false;
      accelerometerTriggered.Xval = int.MinValue;
      accelerometerTriggered.Yval = int.MinValue;
      accelerometerTriggered.Zval = int.MinValue;
    }
    else
    {
      accelerometerTriggered.Xval = ((int) sdm[startIndex] & 2) == 2 ? (((int) sdm[startIndex] & 1) == 1 ? -1 : 1) : 0;
      accelerometerTriggered.Yval = ((int) sdm[startIndex] & 8) == 8 ? (((int) sdm[startIndex] & 4) == 4 ? -1 : 1) : 0;
      accelerometerTriggered.Zval = ((int) sdm[startIndex] & 32 /*0x20*/) == 32 /*0x20*/ ? (((int) sdm[startIndex] & 16 /*0x10*/) == 16 /*0x10*/ ? -1 : 1) : 0;
      accelerometerTriggered.ForceDetected = accelerometerTriggered.Xval != 0 || accelerometerTriggered.Yval != 0 || accelerometerTriggered.Zval != 0;
    }
    return accelerometerTriggered;
  }

  public override object PlotValue => (object) this.ForceDetected;

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
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = AccelerometerTriggered.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    sensor.ActiveStateInterval = sensor.ReportInterval;
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    sensor.Calibration1 = !(collection["performance"].ToStringSafe() == "p") ? Convert.ToInt64(1180457) : Convert.ToInt64(1180417);
    byte[] bytes = BitConverter.GetBytes(sensor.Calibration2);
    double num = collection["gForce"].ToDouble();
    if (num > 8.0)
      num = 8.0;
    if (num < 0.063)
      num = 0.063;
    bytes[1] = Convert.ToByte(Math.Round(num / 0.063, 0).ToInt() + 128 /*0x80*/);
    sensor.Calibration2 = BitConverter.ToUInt32(bytes, 0).ToLong();
    sensor.Save();
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : (Convert.ToDouble(sensor.Hysteresis) / 1000.0).ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : (Convert.ToDouble(sensor.MinimumThreshold) / 1000.0).ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : (Convert.ToDouble(sensor.MaximumThreshold) / 1000.0).ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public SensorAttribute ChartFormat
  {
    get
    {
      if (this._ChartFormat == null)
        this._ChartFormat = new SensorAttribute()
        {
          Value = "Line",
          Name = nameof (ChartFormat)
        };
      return this._ChartFormat;
    }
  }

  public static string GetChartFormat(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ChartFormat")
        return sensorAttribute.Value.ToString();
    }
    return "Line";
  }

  public static void SetChartFormat(long sensorID, string chartFormat)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ChartFormat")
      {
        sensorAttribute.Value = chartFormat.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "ChartFormat",
        Value = chartFormat.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ChartFormat")
        this._ChartFormat = sensorAttribute;
    }
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(AccelerometerTriggered left, AccelerometerTriggered right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(AccelerometerTriggered left, AccelerometerTriggered right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(AccelerometerTriggered left, AccelerometerTriggered right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(AccelerometerTriggered left, AccelerometerTriggered right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(AccelerometerTriggered left, AccelerometerTriggered right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(AccelerometerTriggered left, AccelerometerTriggered right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is AccelerometerTriggered && this.Equals((MonnitApplicationBase) (obj as AccelerometerTriggered));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is AccelerometerTriggered && this.ForceDetected == (right as AccelerometerTriggered).ForceDetected;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is AccelerometerTriggered && this.ForceDetected != (right as AccelerometerTriggered).ForceDetected;
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

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
  }
}
