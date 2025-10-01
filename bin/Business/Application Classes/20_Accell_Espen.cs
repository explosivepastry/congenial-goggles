// Decompiled with JetBrains decompiler
// Type: Monnit.Accel_Espen
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

public class Accel_Espen : MonnitApplicationBase
{
  public static long MonnitApplicationID => 20;

  public static string ApplicationName => "Accelerometer - Max & Avg";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => Accel_Espen.MonnitApplicationID;

  public double MaxXval { get; set; }

  public double MaxYval { get; set; }

  public double MaxZval { get; set; }

  public double AvgXval { get; set; }

  public double AvgYval { get; set; }

  public double AvgZval { get; set; }

  public int state { get; set; }

  public override string Serialize()
  {
    return $"{this.MaxXval.ToString()},{this.MaxYval.ToString()},{this.MaxZval.ToString()},{this.AvgXval.ToString()},{this.AvgYval.ToString()},{this.AvgZval.ToString()},{this.state.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[7]
      {
        new AppDatum(eDatumType.Geforce, "Max", this.Max),
        new AppDatum(eDatumType.Geforce, "MaxXval", this.MaxXval),
        new AppDatum(eDatumType.Geforce, "MaxYval", this.MaxYval),
        new AppDatum(eDatumType.Geforce, "MaxZval", this.MaxZval),
        new AppDatum(eDatumType.Geforce, "AvgXval", this.AvgXval),
        new AppDatum(eDatumType.Geforce, "AvgYval", this.AvgYval),
        new AppDatum(eDatumType.Geforce, "AvgZval", this.AvgZval)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>(this.Datums.Select<AppDatum, object>((Func<AppDatum, object>) (da => da.data.compvalue)));
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Accel_Espen.Deserialize(version, serialized);
  }

  public override bool IsValid => (this.state & 16 /*0x10*/) != 16 /*0x10*/;

  public override string NotificationString
  {
    get
    {
      int state = this.state;
      if ((this.state & 16 /*0x10*/) == 16 /*0x10*/)
        return "Hardware Failure!";
      string[] strArray = new string[12];
      strArray[0] = "Max X: ";
      double num = this.MaxXval;
      strArray[1] = num.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
      strArray[2] = " Max Y: ";
      num = this.MaxYval;
      strArray[3] = num.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
      strArray[4] = " Max Z: ";
      num = this.MaxZval;
      strArray[5] = num.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
      strArray[6] = " Avg X: ";
      num = this.AvgXval;
      strArray[7] = num.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
      strArray[8] = " Avg Y: ";
      num = this.AvgYval;
      strArray[9] = num.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
      strArray[10] = " Avg Z: ";
      num = this.AvgZval;
      strArray[11] = num.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
      return string.Concat(strArray);
    }
  }

  public static Accel_Espen Deserialize(string version, string serialized)
  {
    Accel_Espen accelEspen = new Accel_Espen();
    if (string.IsNullOrEmpty(serialized))
    {
      accelEspen.MaxXval = 0.0;
      accelEspen.MaxYval = 0.0;
      accelEspen.MaxZval = 0.0;
      accelEspen.AvgXval = 0.0;
      accelEspen.AvgYval = 0.0;
      accelEspen.AvgZval = 0.0;
      accelEspen.state = 0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      accelEspen.MaxXval = strArray[0].ToDouble();
      if (strArray.Length == 6 || strArray.Length == 7)
      {
        accelEspen.MaxYval = strArray[1].ToDouble();
        accelEspen.MaxZval = strArray[2].ToDouble();
        accelEspen.AvgXval = strArray[3].ToDouble();
        accelEspen.AvgYval = strArray[4].ToDouble();
        accelEspen.AvgZval = strArray[5].ToDouble();
        try
        {
          accelEspen.state = strArray[6].ToInt();
        }
        catch
        {
          accelEspen.state = 0;
        }
      }
      else
      {
        accelEspen.MaxYval = accelEspen.MaxXval;
        accelEspen.MaxZval = accelEspen.MaxXval;
        accelEspen.AvgXval = accelEspen.MaxXval;
        accelEspen.AvgYval = accelEspen.MaxXval;
        accelEspen.AvgZval = accelEspen.MaxXval;
        accelEspen.state = 0;
      }
    }
    return accelEspen;
  }

  public static Accel_Espen Create(byte[] sdm, int startIndex)
  {
    Accel_Espen accelEspen = new Accel_Espen();
    accelEspen.state = (int) sdm[startIndex - 1];
    if ((accelEspen.state & 16 /*0x10*/) == 16 /*0x10*/)
    {
      accelEspen.MaxXval = 0.0;
      accelEspen.MaxYval = 0.0;
      accelEspen.MaxZval = 0.0;
      accelEspen.AvgXval = 0.0;
      accelEspen.AvgYval = 0.0;
      accelEspen.AvgZval = 0.0;
    }
    else
    {
      accelEspen.MaxXval = Convert.ToDouble(BitConverter.ToInt16(sdm, startIndex)) / 1000.0;
      accelEspen.MaxYval = Convert.ToDouble(BitConverter.ToInt16(sdm, startIndex + 2)) / 1000.0;
      accelEspen.MaxZval = Convert.ToDouble(BitConverter.ToInt16(sdm, startIndex + 4)) / 1000.0;
      accelEspen.AvgXval = Convert.ToDouble(BitConverter.ToInt16(sdm, startIndex + 6)) / 1000.0;
      accelEspen.AvgYval = Convert.ToDouble(BitConverter.ToInt16(sdm, startIndex + 8)) / 1000.0;
      accelEspen.AvgZval = Convert.ToDouble(BitConverter.ToInt16(sdm, startIndex + 10)) / 1000.0;
    }
    return accelEspen;
  }

  public override object PlotValue => (object) this.Max;

  protected virtual double Max
  {
    get
    {
      double num = this.MaxXval < this.MaxYval ? this.MaxYval : this.MaxXval;
      return num < this.MaxZval ? this.MaxZval : num;
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
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = Accel_Espen.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    sensor.Calibration1 = string.IsNullOrWhiteSpace(collection["isSleep"]) ? sensor.DefaultValue<long>("DefaultCalibration1") : (!collection["isSleep"].ToInt().ToBool() ? 0L : 1L);
    int result = 0;
    sensor.Calibration2 = string.IsNullOrWhiteSpace(collection["Time"]) || !int.TryParse(collection["Time"], out result) ? sensor.DefaultValue<long>("DefaultCalibration2") : ((collection["Time"].ToInt() * 2).ToLong() == 0L ? 2L : (collection["Time"].ToInt() * 2).ToLong());
    int num = string.IsNullOrWhiteSpace(collection["perCycle"]) ? 0 : (int.TryParse(collection["perCycle"], out result) ? 1 : 0);
    sensor.Calibration3 = num == 0 ? sensor.DefaultValue<long>("DefaultCalibration3") : (long) collection["perCycle"].ToInt();
    if (!string.IsNullOrWhiteSpace(collection["mode"]) && int.TryParse(collection["mode"], out result))
      sensor.Calibration4 = (long) collection["mode"].ToInt();
    else
      sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
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

  public static bool operator ==(Accel_Espen left, Accel_Espen right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Accel_Espen left, Accel_Espen right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Accel_Espen left, Accel_Espen right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Accel_Espen left, Accel_Espen right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Accel_Espen left, Accel_Espen right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Accel_Espen left, Accel_Espen right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Accel_Espen && this.Equals((MonnitApplicationBase) (obj as Accel_Espen));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Accel_Espen && this.Max == (right as Accel_Espen).Max;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Accel_Espen && this.Max != (right as Accel_Espen).Max;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Accel_Espen && this.Max < (right as Accel_Espen).Max;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Accel_Espen && this.Max <= (right as Accel_Espen).Max;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Accel_Espen && this.Max > (right as Accel_Espen).Max;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Accel_Espen && this.Max >= (right as Accel_Espen).Max;
  }

  public static long DefaultMinThreshold => (long) uint.MaxValue;

  public static long DefaultMaxThreshold => (long) uint.MaxValue;

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
