// Decompiled with JetBrains decompiler
// Type: Monnit.FiveVoltDetector
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class FiveVoltDetector : DryContact
{
  public new static long MonnitApplicationID => 71;

  public new static string ApplicationName => "Voltage Detection - 50 VDC";

  public new static eApplicationProfileType ProfileType => eApplicationProfileType.Trigger;

  public new string ChartType => "Point";

  public override long ApplicationID => FiveVoltDetector.MonnitApplicationID;

  public override string NotificationString
  {
    get => this.Pressed ? "Voltage Present" : "No Voltage Present";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.VoltageDetect, "Voltage Detected", this.Pressed)
      });
    }
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) FiveVoltDetector.Deserialize(version, serialized);
  }

  public static FiveVoltDetector Deserialize(string version, string serialized)
  {
    FiveVoltDetector fiveVoltDetector = new FiveVoltDetector();
    fiveVoltDetector.Pressed = serialized.ToBool();
    return fiveVoltDetector;
  }

  public static FiveVoltDetector Create(byte[] sdm, int startIndex)
  {
    FiveVoltDetector fiveVoltDetector = new FiveVoltDetector();
    fiveVoltDetector.Pressed = sdm[startIndex].ToBool();
    return fiveVoltDetector;
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
      CompareValue = "True",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = FiveVoltDetector.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public new static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (string.IsNullOrEmpty(collection["EventDetectionType_Manual"]))
      return;
    sensor.EventDetectionType = !(collection["EventDetectionType_Manual"] == FiveVoltDetector.ValueForZero) ? (!(collection["EventDetectionType_Manual"] == FiveVoltDetector.ValueForOne) ? 2 : 1) : 0;
  }

  public new static string EventDetectionTypeDescription => "Enter aware state when voltage is ";

  public new static string ValueForZero => "Present";

  public new static string ValueForOne => "Absent";

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(FiveVoltDetector left, FiveVoltDetector right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(FiveVoltDetector left, FiveVoltDetector right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(FiveVoltDetector left, FiveVoltDetector right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(FiveVoltDetector left, FiveVoltDetector right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(FiveVoltDetector left, FiveVoltDetector right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(FiveVoltDetector left, FiveVoltDetector right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is FiveVoltDetector && this.Equals((MonnitApplicationBase) (obj as FiveVoltDetector));
  }
}
