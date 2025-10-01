// Decompiled with JetBrains decompiler
// Type: Monnit.GeforceMaxAvg
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

public class GeforceMaxAvg : MonnitApplicationBase
{
  public static long MonnitApplicationID => 126;

  public static string ApplicationName => "G-force - Max & Avg";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => GeforceMaxAvg.MonnitApplicationID;

  public double XMax { get; set; }

  public double YMax { get; set; }

  public double ZMax { get; set; }

  public double MagnitudeMax { get; set; }

  public double XMean { get; set; }

  public double YMean { get; set; }

  public double ZMean { get; set; }

  public double MagnitudeMean { get; set; }

  public int stsStatus { get; set; }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[8]
      {
        new AppDatum(eDatumType.Geforce, "XMax", this.XMax),
        new AppDatum(eDatumType.Geforce, "YMax", this.YMax),
        new AppDatum(eDatumType.Geforce, "ZMax", this.ZMax),
        new AppDatum(eDatumType.Geforce, "MagnitudeMax", this.MagnitudeMax),
        new AppDatum(eDatumType.Geforce, "XMean", this.XMean),
        new AppDatum(eDatumType.Geforce, "YMean", this.YMean),
        new AppDatum(eDatumType.Geforce, "ZMean", this.ZMean),
        new AppDatum(eDatumType.Geforce, "MagnitudeMean", this.MagnitudeMean)
      });
    }
  }

  public static GeforceMaxAvg Create(byte[] sdm, int startIndex)
  {
    GeforceMaxAvg geforceMaxAvg = new GeforceMaxAvg();
    geforceMaxAvg.stsStatus = (int) sdm[startIndex - 1];
    if ((geforceMaxAvg.stsStatus & 16 /*0x10*/) == 16 /*0x10*/)
    {
      geforceMaxAvg.XMax = 0.0;
      geforceMaxAvg.YMax = 0.0;
      geforceMaxAvg.ZMax = 0.0;
      geforceMaxAvg.MagnitudeMax = 0.0;
      geforceMaxAvg.XMean = 0.0;
      geforceMaxAvg.YMean = 0.0;
      geforceMaxAvg.ZMean = 0.0;
      geforceMaxAvg.MagnitudeMean = 0.0;
    }
    else
    {
      geforceMaxAvg.XMax = Convert.ToDouble((double) BitConverter.ToUInt16(sdm, startIndex) / 1000.0);
      geforceMaxAvg.YMax = Convert.ToDouble((double) BitConverter.ToUInt16(sdm, startIndex + 2) / 1000.0);
      geforceMaxAvg.ZMax = Convert.ToDouble((double) BitConverter.ToUInt16(sdm, startIndex + 4) / 1000.0);
      geforceMaxAvg.MagnitudeMax = Convert.ToDouble((double) BitConverter.ToUInt16(sdm, startIndex + 6) / 1000.0);
      geforceMaxAvg.XMean = Convert.ToDouble((double) BitConverter.ToUInt16(sdm, startIndex + 8) / 1000.0);
      geforceMaxAvg.YMean = Convert.ToDouble((double) BitConverter.ToUInt16(sdm, startIndex + 10) / 1000.0);
      geforceMaxAvg.ZMean = Convert.ToDouble((double) BitConverter.ToUInt16(sdm, startIndex + 12) / 1000.0);
      geforceMaxAvg.MagnitudeMean = Convert.ToDouble((double) BitConverter.ToUInt16(sdm, startIndex + 14) / 1000.0);
    }
    return geforceMaxAvg;
  }

  public override string Serialize()
  {
    return $"{this.XMax.ToString()}|{this.YMax.ToString()}|{this.ZMax.ToString()}|{this.MagnitudeMax.ToString()}|{this.XMean.ToString()}|{this.YMean.ToString()}|{this.ZMean.ToString()}|{this.MagnitudeMean.ToString()}|{this.stsStatus.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) GeforceMaxAvg.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      int stsStatus = this.stsStatus;
      if ((this.stsStatus & 16 /*0x10*/) == 16 /*0x10*/)
        return "Accelerometer Hardware Error";
      string[] strArray = new string[17];
      strArray[0] = "X Max: ";
      double num = this.XMax;
      strArray[1] = num.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
      strArray[2] = " g , Y Max: ";
      num = this.YMax;
      strArray[3] = num.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
      strArray[4] = " g , Z Max: ";
      num = this.ZMax;
      strArray[5] = num.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
      strArray[6] = " g , Magnitude Max: ";
      num = this.MagnitudeMax;
      strArray[7] = num.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
      strArray[8] = " g , X Avg: ";
      num = this.XMean;
      strArray[9] = num.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
      strArray[10] = " g , Y Avg: ";
      num = this.YMean;
      strArray[11] = num.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
      strArray[12] = " g , Z Avg: ";
      num = this.ZMean;
      strArray[13] = num.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
      strArray[14] = " g , Magnitude Mean: ";
      num = this.MagnitudeMean;
      strArray[15] = num.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
      strArray[16 /*0x10*/] = " g";
      return string.Concat(strArray);
    }
  }

  public static GeforceMaxAvg Deserialize(string version, string serialized)
  {
    GeforceMaxAvg geforceMaxAvg = new GeforceMaxAvg();
    if (string.IsNullOrEmpty(serialized))
    {
      geforceMaxAvg.XMax = 0.0;
      geforceMaxAvg.YMax = 0.0;
      geforceMaxAvg.ZMax = 0.0;
      geforceMaxAvg.MagnitudeMax = 0.0;
      geforceMaxAvg.XMean = 0.0;
      geforceMaxAvg.YMean = 0.0;
      geforceMaxAvg.ZMean = 0.0;
      geforceMaxAvg.MagnitudeMean = 0.0;
      geforceMaxAvg.stsStatus = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      geforceMaxAvg.XMax = strArray[0].ToDouble();
      if (strArray.Length > 1)
      {
        geforceMaxAvg.YMax = strArray[1].ToDouble();
        geforceMaxAvg.ZMax = strArray[2].ToDouble();
        geforceMaxAvg.MagnitudeMax = strArray[3].ToDouble();
        geforceMaxAvg.XMean = strArray[4].ToDouble();
        geforceMaxAvg.YMean = strArray[5].ToDouble();
        geforceMaxAvg.ZMean = strArray[6].ToDouble();
        geforceMaxAvg.MagnitudeMean = strArray[7].ToDouble();
        try
        {
          geforceMaxAvg.stsStatus = strArray[8].ToInt();
        }
        catch
        {
          geforceMaxAvg.stsStatus = 0;
        }
      }
      else
      {
        geforceMaxAvg.YMax = strArray[0].ToDouble();
        geforceMaxAvg.ZMax = strArray[0].ToDouble();
        geforceMaxAvg.MagnitudeMax = strArray[0].ToDouble();
        geforceMaxAvg.XMean = strArray[0].ToDouble();
        geforceMaxAvg.YMean = strArray[0].ToDouble();
        geforceMaxAvg.ZMean = strArray[0].ToDouble();
        geforceMaxAvg.MagnitudeMean = strArray[0].ToDouble();
      }
    }
    return geforceMaxAvg;
  }

  public override object PlotValue
  {
    get
    {
      double xmax = this.XMax;
      return true ? (object) this.XMax : (object) null;
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      this.PlotValue,
      (object) this.YMax,
      (object) this.ZMax,
      (object) this.MagnitudeMax,
      (object) this.XMean,
      (object) this.YMean,
      (object) this.ZMean,
      (object) this.MagnitudeMean
    };
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>()
    {
      "XMax",
      "YMax",
      "ZMax",
      "MagnitudeMax",
      "XMean",
      "YMean",
      "ZMean",
      "MagnitudeMean"
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
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = GeforceMaxAvg.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["XDeltaValue"]))
    {
      double num = collection["XDeltaValue"].ToDouble();
      if (num < 0.0)
        num = 0.0;
      if (num > 8.0)
        num = 8.0;
      sensor.MaximumThreshold = (num * 1000.0).ToLong();
    }
    if (!string.IsNullOrEmpty(collection["YDeltaValue"]))
    {
      double num = collection["YDeltaValue"].ToDouble();
      if (num < 0.0)
        num = 0.0;
      if (num > 8.0)
        num = 8.0;
      sensor.MinimumThreshold = (num * 1000.0).ToLong();
    }
    if (!string.IsNullOrEmpty(collection["ZDeltaValue"]))
    {
      double num = collection["ZDeltaValue"].ToDouble();
      if (num < 0.0)
        num = 0.0;
      if (num > 8.0)
        num = 8.0;
      sensor.Hysteresis = (num * 1000.0).ToLong();
    }
    if (!string.IsNullOrEmpty(collection["MagDeltaValue"]))
    {
      double num = collection["MagDeltaValue"].ToDouble();
      if (num < 0.0)
        num = 0.0;
      if (num > 8.0)
        num = 8.0;
      sensor.Calibration4 = (num * 1000.0).ToLong();
    }
    if (!string.IsNullOrEmpty(collection["ReportInterval"]))
      sensor.ActiveStateInterval = collection["ReportInterval"].ToDouble();
    if (!string.IsNullOrEmpty(collection["OutputDataRate"]))
      sensor.Calibration1 = collection["OutputDataRate"].ToLong();
    if (!string.IsNullOrEmpty(collection["Mode"]))
      sensor.Calibration2 = collection["Mode"].ToLong();
    if (string.IsNullOrEmpty(collection["ReArmTime"]))
      return;
    long num1 = collection["ReArmTime"].ToLong();
    if ((double) num1 > sensor.ReportInterval * 60.0)
      num1 = (sensor.ReportInterval * 60.0).ToLong();
    if (num1 > 600L)
      num1 = 600L;
    if (num1 < 1L)
      num1 = 1L;
    sensor.Calibration3 = num1;
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

  public static bool operator ==(GeforceMaxAvg left, GeforceMaxAvg right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(GeforceMaxAvg left, GeforceMaxAvg right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(GeforceMaxAvg left, GeforceMaxAvg right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(GeforceMaxAvg left, GeforceMaxAvg right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(GeforceMaxAvg left, GeforceMaxAvg right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(GeforceMaxAvg left, GeforceMaxAvg right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is GeforceMaxAvg && this.Equals((MonnitApplicationBase) (obj as GeforceMaxAvg));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is GeforceMaxAvg && this.XMax == (right as GeforceMaxAvg).XMax;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is GeforceMaxAvg && this.XMax != (right as GeforceMaxAvg).XMax;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is GeforceMaxAvg && this.XMax < (right as GeforceMaxAvg).XMax;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is GeforceMaxAvg && this.XMax <= (right as GeforceMaxAvg).XMax;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is GeforceMaxAvg && this.XMax > (right as GeforceMaxAvg).XMax;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is GeforceMaxAvg && this.XMax >= (right as GeforceMaxAvg).XMax;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 0;

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
