// Decompiled with JetBrains decompiler
// Type: Monnit.TempTesting
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

public class TempTesting : MonnitApplicationBase
{
  public static long MonnitApplicationID => 25;

  public static string ApplicationName => "Temp Testing";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => TempTesting.MonnitApplicationID;

  public int Low { get; set; }

  public int Med { get; set; }

  public int High { get; set; }

  public override string Serialize()
  {
    return $"{this.Low.ToString()},{this.Med.ToString()},{this.High.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) TempTesting.Deserialize(version, serialized);
  }

  public override List<AppDatum> Datums => new List<AppDatum>();

  public override string NotificationString
  {
    get
    {
      return $"Low: {this.Low.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture)} Med: {this.Med.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture)} High: {this.High.ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture)}";
    }
  }

  public static TempTesting Deserialize(string version, string serialized)
  {
    TempTesting tempTesting = new TempTesting();
    if (string.IsNullOrEmpty(serialized))
    {
      tempTesting.Low = 0;
      tempTesting.Med = 0;
      tempTesting.High = 0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      tempTesting.Low = strArray[0].ToInt();
      if (strArray.Length == 3)
      {
        tempTesting.Med = strArray[1].ToInt();
        tempTesting.High = strArray[2].ToInt();
      }
      else
      {
        tempTesting.Med = tempTesting.Low;
        tempTesting.High = tempTesting.Low;
      }
    }
    return tempTesting;
  }

  public static TempTesting Create(byte[] sdm, int startIndex)
  {
    return new TempTesting()
    {
      Low = (int) BitConverter.ToUInt16(sdm, startIndex),
      Med = (int) BitConverter.ToUInt16(sdm, startIndex + 2),
      High = (int) BitConverter.ToUInt16(sdm, startIndex + 4)
    };
  }

  public override object PlotValue => (object) this.Max;

  protected virtual int Max
  {
    get
    {
      int num = this.Low < this.Med ? this.Med : this.Low;
      return num < this.High ? this.High : num;
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
      ApplicationID = TempTesting.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if (string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if (string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
      sensor.Hysteresis = (collection["Hysteresis_Manual"].ToDouble() * 1000.0).ToLong();
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
      sensor.MinimumThreshold = (collection["MinimumThreshold_Manual"].ToDouble() * 1000.0).ToLong();
    if (string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
      return;
    sensor.MaximumThreshold = (collection["MaximumThreshold_Manual"].ToDouble() * 1000.0).ToLong();
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
  }

  public static string HystForUI(Sensor sensor) => "";

  public static string MinThreshForUI(Sensor sensor) => "";

  public static string MaxThreshForUI(Sensor sensor) => "";

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(TempTesting left, TempTesting right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(TempTesting left, TempTesting right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(TempTesting left, TempTesting right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(TempTesting left, TempTesting right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(TempTesting left, TempTesting right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(TempTesting left, TempTesting right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is TempTesting && this.Equals((MonnitApplicationBase) (obj as TempTesting));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is TempTesting && this.Max == (right as TempTesting).Max;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is TempTesting && this.Max != (right as TempTesting).Max;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is TempTesting && this.Max < (right as TempTesting).Max;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is TempTesting && this.Max <= (right as TempTesting).Max;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is TempTesting && this.Max > (right as TempTesting).Max;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is TempTesting && this.Max >= (right as TempTesting).Max;
  }
}
