// Decompiled with JetBrains decompiler
// Type: Monnit.VibrationHallEffect
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Xml;

#nullable disable
namespace Monnit;

public class VibrationHallEffect : MonnitApplicationBase
{
  public static long MonnitApplicationID => 7;

  public static string ApplicationName => "Open Closed Wiggle";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Trigger;

  public override string ChartType => "Point";

  public override long ApplicationID => VibrationHallEffect.MonnitApplicationID;

  public bool Moving { get; set; }

  public bool Magnet { get; set; }

  public override string Serialize() => $"<Values motion='{this.Moving}' magnet='{this.Magnet}' />";

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.BooleanData, "Magnet Present", this.Magnet),
        new AppDatum(eDatumType.BooleanData, "Moving", this.Moving)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[2]
    {
      this.PlotValue,
      (object) this.Moving
    });
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) VibrationHallEffect.Deserialize(version, serialized);
  }

  public override string NotificationString => $"Magnet Present: {this.Magnet}";

  public override object PlotValue => (object) (this.Magnet ? 1 : 0);

  public static VibrationHallEffect Deserialize(string version, string serialized)
  {
    VibrationHallEffect vibrationHallEffect = new VibrationHallEffect();
    if (string.IsNullOrEmpty(serialized))
    {
      vibrationHallEffect.Moving = true;
      vibrationHallEffect.Magnet = false;
    }
    else
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load((TextReader) new StringReader(serialized));
      XmlNode firstChild = xmlDocument.FirstChild;
      vibrationHallEffect.Moving = firstChild.Attributes["motion"].Value.ToBool();
      vibrationHallEffect.Magnet = firstChild.Attributes["magnet"].Value.ToBool();
    }
    return vibrationHallEffect;
  }

  public static VibrationHallEffect Create(byte[] sdm, int startIndex)
  {
    return new VibrationHallEffect()
    {
      Moving = ((int) sdm[startIndex] & 1) == 1,
      Magnet = ((int) sdm[startIndex] & 2) == 2
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
    sensor.EventDetectionType = 1;
    sensor.EventDetectionPeriod = 50;
    sensor.EventDetectionCount = 9;
    sensor.RearmTime = 1;
    sensor.BiStable = 1;
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      SnoozeDuration = 60,
      CompareValue = "<Values vibrating='True' magnetPresent='False' />",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Equal,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = VibrationHallEffect.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (string.IsNullOrEmpty(collection["EventDetectionType_Manual"]))
      return;
    sensor.EventDetectionType = collection["EventDetectionType_Manual"] == VibrationHallEffect.ValueForZero ? 0 : 1;
  }

  public static string EventDetectionTypeDescription => "Enter aware state when magnet is ";

  public static string ValueForZero => "Introduced";

  public static string ValueForOne => "Removed";

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(VibrationHallEffect left, VibrationHallEffect right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(VibrationHallEffect left, VibrationHallEffect right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(VibrationHallEffect left, VibrationHallEffect right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(VibrationHallEffect left, VibrationHallEffect right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(VibrationHallEffect left, VibrationHallEffect right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(VibrationHallEffect left, VibrationHallEffect right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is VibrationHallEffect && this.Equals((MonnitApplicationBase) (obj as VibrationHallEffect));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is VibrationHallEffect && this.Moving == (right as VibrationHallEffect).Moving && this.Magnet == (right as VibrationHallEffect).Magnet;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is VibrationHallEffect && (this.Moving != (right as VibrationHallEffect).Moving || this.Magnet != (right as VibrationHallEffect).Magnet);
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
}
