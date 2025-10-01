// Decompiled with JetBrains decompiler
// Type: Monnit.Accelerometer
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

public class Accelerometer : MonnitApplicationBase
{
  public static long MonnitApplicationID => 15;

  public static string ApplicationName => "Accelerometer - Snapshot";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => Accelerometer.MonnitApplicationID;

  public double Xval { get; set; }

  public double Yval { get; set; }

  public double Zval { get; set; }

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
        new AppDatum(eDatumType.Geforce, "Max", this.Max),
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
      (object) this.Max,
      (object) this.Xval,
      (object) this.Yval,
      (object) this.Zval
    });
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Accelerometer.Deserialize(version, serialized);
  }

  public override bool IsValid => (this.state & 16 /*0x10*/) != 16 /*0x10*/;

  public override string NotificationString
  {
    get
    {
      int state = this.state;
      if ((this.state & 16 /*0x10*/) == 16 /*0x10*/)
        return "Hardware Failure!";
      return $"X: {this.Xval.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture)} Y: {this.Yval.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture)} Z: {this.Zval.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture)}";
    }
  }

  public static Accelerometer Deserialize(string version, string serialized)
  {
    Accelerometer accelerometer = new Accelerometer();
    if (string.IsNullOrEmpty(serialized))
    {
      accelerometer.Xval = 0.0;
      accelerometer.Yval = 0.0;
      accelerometer.Zval = 0.0;
      accelerometer.state = 0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      accelerometer.Xval = strArray[0].ToDouble();
      if (strArray.Length == 3 || strArray.Length == 4)
      {
        accelerometer.Yval = strArray[1].ToDouble();
        accelerometer.Zval = strArray[2].ToDouble();
        try
        {
          accelerometer.state = strArray[3].ToInt();
        }
        catch
        {
          accelerometer.state = 0;
        }
      }
      else
      {
        accelerometer.Yval = accelerometer.Xval;
        accelerometer.Zval = accelerometer.Xval;
      }
    }
    return accelerometer;
  }

  public static Accelerometer Create(byte[] sdm, int startIndex)
  {
    Accelerometer accelerometer = new Accelerometer();
    accelerometer.state = (int) sdm[startIndex - 1];
    if ((accelerometer.state & 16 /*0x10*/) == 16 /*0x10*/)
    {
      accelerometer.Xval = 0.0;
      accelerometer.Yval = 0.0;
      accelerometer.Zval = 0.0;
    }
    else
    {
      accelerometer.Xval = Convert.ToDouble(BitConverter.ToInt16(sdm, startIndex)) / 1000.0;
      accelerometer.Yval = Convert.ToDouble(BitConverter.ToInt16(sdm, startIndex + 2)) / 1000.0;
      accelerometer.Zval = Convert.ToDouble(BitConverter.ToInt16(sdm, startIndex + 4)) / 1000.0;
    }
    return accelerometer;
  }

  public override object PlotValue => (object) this.Max;

  protected virtual double Max
  {
    get
    {
      double num = this.Xval < this.Yval ? this.Yval : this.Xval;
      return num < this.Zval ? this.Zval : num;
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
      ApplicationID = Accelerometer.MonnitApplicationID,
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
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      numArrayList.Add(AccelerometerBase.CalibrateFrame(sensor.SensorID));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
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

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Accelerometer left, Accelerometer right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Accelerometer left, Accelerometer right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Accelerometer left, Accelerometer right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Accelerometer left, Accelerometer right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Accelerometer left, Accelerometer right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Accelerometer left, Accelerometer right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Accelerometer && this.Equals((MonnitApplicationBase) (obj as Accelerometer));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Accelerometer && this.Max == (right as Accelerometer).Max;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Accelerometer && this.Max != (right as Accelerometer).Max;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Accelerometer && this.Max < (right as Accelerometer).Max;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Accelerometer && this.Max <= (right as Accelerometer).Max;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Accelerometer && this.Max > (right as Accelerometer).Max;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Accelerometer && this.Max >= (right as Accelerometer).Max;
  }

  public static long DefaultMinThreshold => (long) uint.MaxValue;

  public static long DefaultMaxThreshold => (long) uint.MaxValue;

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
  }
}
