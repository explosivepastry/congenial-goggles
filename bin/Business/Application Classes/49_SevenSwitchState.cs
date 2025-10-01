// Decompiled with JetBrains decompiler
// Type: Monnit.SevenSwitchState
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

internal class SevenSwitchState : MonnitApplicationBase
{
  public static long MonnitApplicationID => 49;

  public static string ApplicationName => "Pill Box";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => SevenSwitchState.MonnitApplicationID;

  public int Status { get; set; }

  public override string Serialize() => this.Status.ToString();

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.Status, "SwitchStatus", this.Status)
      });
    }
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) SevenSwitchState.Deserialize(version, serialized);
  }

  public override string NotificationString => "0x" + this.Status.ToString("X1");

  public override object PlotValue => (object) this.Status;

  public static SevenSwitchState Deserialize(string version, string serialized)
  {
    return new SevenSwitchState()
    {
      Status = serialized.ToInt()
    };
  }

  public static SevenSwitchState Create(byte[] sdm, int startIndex)
  {
    return new SevenSwitchState()
    {
      Status = (int) sdm[startIndex]
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
      ApplicationID = SevenSwitchState.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
  }

  public static string HystForUI(Sensor sensor) => "Not Used";

  public static string MinThreshForUI(Sensor sensor) => "Not Used";

  public static string MaxThreshForUI(Sensor sensor) => "Not Used";

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(SevenSwitchState left, SevenSwitchState right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(SevenSwitchState left, SevenSwitchState right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(SevenSwitchState left, SevenSwitchState right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(SevenSwitchState left, SevenSwitchState right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(SevenSwitchState left, SevenSwitchState right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(SevenSwitchState left, SevenSwitchState right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is SevenSwitchState && this.Equals((MonnitApplicationBase) (obj as SevenSwitchState));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is SevenSwitchState && this.Status == (right as SevenSwitchState).Status;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is SevenSwitchState && this.Status != (right as SevenSwitchState).Status;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is SevenSwitchState && this.Status < (right as SevenSwitchState).Status;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is SevenSwitchState && this.Status <= (right as SevenSwitchState).Status;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is SevenSwitchState && this.Status > (right as SevenSwitchState).Status;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is SevenSwitchState && this.Status >= (right as SevenSwitchState).Status;
  }
}
