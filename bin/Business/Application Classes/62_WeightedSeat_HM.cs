// Decompiled with JetBrains decompiler
// Type: Monnit.WeightedSeat
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class WeightedSeat : MonnitApplicationBase
{
  public static long MonnitApplicationID => 62;

  public static string ApplicationName => "Weighted Seat";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => WeightedSeat.MonnitApplicationID;

  public int Weight { get; set; }

  public int Resistance { get; set; }

  public override string Serialize() => $"{this.Weight},{this.Resistance}";

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.Weight, "Weight", this.Weight),
        new AppDatum(eDatumType.ResistanceData, "Resistance", this.Resistance)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      this.PlotValue,
      (object) this.Resistance
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) WeightedSeat.Deserialize(version, serialized);
  }

  public override string NotificationString => $"{this.Weight}";

  public override object PlotValue => (object) this.Weight;

  public static WeightedSeat Deserialize(string version, string serialized)
  {
    WeightedSeat weightedSeat = new WeightedSeat();
    if (string.IsNullOrEmpty(serialized))
    {
      weightedSeat.Weight = 0;
      weightedSeat.Resistance = 0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      weightedSeat.Weight = strArray[0].ToInt();
      if (strArray.Length > 1)
        weightedSeat.Resistance = strArray[1].ToInt();
    }
    return weightedSeat;
  }

  public static WeightedSeat Create(byte[] sdm, int startIndex)
  {
    return new WeightedSeat()
    {
      Weight = (int) sdm[startIndex],
      Resistance = BitConverter.ToInt32(sdm, startIndex + 1)
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
      ApplicationID = WeightedSeat.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (collection["Calval1_Manual"] != null && !string.IsNullOrEmpty(collection["Calval1_Manual"]))
      sensor.Calibration1 = collection["Calval1_Manual"].ToLong();
    if (sensor.Calibration1 < 20L)
      sensor.Calibration1 = 20L;
    if (sensor.Calibration1 > 30000L)
      sensor.Calibration1 = 30000L;
    if (collection["Calval2_Manual"] != null && !string.IsNullOrEmpty(collection["Calval2_Manual"]))
      sensor.Calibration2 = collection["Calval2_Manual"].ToLong();
    if (sensor.Calibration2 < 1000L)
      sensor.Calibration2 = 1000L;
    if (sensor.Calibration2 > 50000L)
      sensor.Calibration2 = 50000L;
    if (collection["Calval3_Manual"] != null && !string.IsNullOrEmpty(collection["Calval3_Manual"]))
      sensor.Calibration3 = collection["Calval3_Manual"].ToLong();
    if (sensor.Calibration3 < 10L)
      sensor.Calibration3 = 10L;
    if (sensor.Calibration3 <= 250L)
      return;
    sensor.Calibration3 = 250L;
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : sensor.Hysteresis.ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : sensor.MinimumThreshold.ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : sensor.MaximumThreshold.ToString();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(WeightedSeat left, WeightedSeat right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(WeightedSeat left, WeightedSeat right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(WeightedSeat left, WeightedSeat right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(WeightedSeat left, WeightedSeat right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(WeightedSeat left, WeightedSeat right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(WeightedSeat left, WeightedSeat right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is WeightedSeat && this.Equals((MonnitApplicationBase) (obj as WeightedSeat));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is WeightedSeat && this.Weight == (right as WeightedSeat).Weight;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is WeightedSeat && this.Weight != (right as WeightedSeat).Weight;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is WeightedSeat && this.Weight < (right as WeightedSeat).Weight;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is WeightedSeat && this.Weight <= (right as WeightedSeat).Weight;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is WeightedSeat && this.Weight > (right as WeightedSeat).Weight;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is WeightedSeat && this.Weight >= (right as WeightedSeat).Weight;
  }
}
