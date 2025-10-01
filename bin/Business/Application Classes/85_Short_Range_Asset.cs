// Decompiled with JetBrains decompiler
// Type: Monnit.Short_Range_Asset
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class Short_Range_Asset : ActiveID
{
  public new static long MonnitApplicationID => 85;

  public new static string ApplicationName => "Short Range Asset";

  public new static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public static Short_Range_Asset Deserialize(string version, string serialized)
  {
    return new Short_Range_Asset();
  }

  public static Short_Range_Asset Create(byte[] sdm, int startIndex) => new Short_Range_Asset();

  public override List<AppDatum> Datums => new List<AppDatum>();

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
      CompareValue = "60",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = Short_Range_Asset.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public new static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
      return;
    if (collection["Hysteresis_Manual"].ToLower().Replace(" ", "").StartsWith("channel"))
    {
      int num1 = collection["Hysteresis_Manual"].ToLower().Replace(" ", "").Replace("channel", "").ToInt();
      int num2 = num1 < 0 ? 0 : (num1 <= 24 ? 1 : 0);
      sensor.Hysteresis = num2 == 0 ? (long) uint.MaxValue : (long) num1;
    }
    else
      sensor.Hysteresis = (long) uint.MaxValue;
  }

  public new static string HystForUI(Sensor sensor) => ActiveID.HystForUI(sensor);

  public new static string MinThreshForUI(Sensor sensor) => ActiveID.MinThreshForUI(sensor);

  public new static string MaxThreshForUI(Sensor sensor) => ActiveID.MaxThreshForUI(sensor);

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Short_Range_Asset left, Short_Range_Asset right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Short_Range_Asset left, Short_Range_Asset right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Short_Range_Asset left, Short_Range_Asset right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Short_Range_Asset left, Short_Range_Asset right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Short_Range_Asset left, Short_Range_Asset right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Short_Range_Asset left, Short_Range_Asset right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Short_Range_Asset && this.Equals((MonnitApplicationBase) (obj as Short_Range_Asset));
  }
}
