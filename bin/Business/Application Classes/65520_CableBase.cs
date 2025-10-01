// Decompiled with JetBrains decompiler
// Type: Monnit.CableBase
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

#nullable disable
namespace Monnit;

public class CableBase : MonnitApplicationBase
{
  public static long MonnitApplicationID => CableBaseBase.MonnitApplicationID;

  public static string ApplicationName => "Cable Base";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "None";

  public override long ApplicationID => CableBase.MonnitApplicationID;

  public string Data { get; set; }

  public string STSValue { get; set; }

  public static CableBase Create(byte[] sdm, int startIndex)
  {
    return new CableBase()
    {
      STSValue = Convert.ToString(sdm[startIndex - 1]),
      Data = BitConverter.ToString(sdm, startIndex)
    };
  }

  public override string Serialize() => $"{this.Data}|{this.STSValue}";

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.Data, "STSValue", this.STSValue),
        new AppDatum(eDatumType.Data, "Data", this.Data)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>() { "STSValue", "Data" };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) CableBase.Deserialize(version, serialized);
  }

  public static CableBase Deserialize(string version, string serialized)
  {
    CableBase cableBase = new CableBase();
    string[] strArray = serialized.Split(new char[1]{ '|' }, StringSplitOptions.RemoveEmptyEntries);
    cableBase.Data = strArray[0];
    cableBase.STSValue = "0";
    if (strArray.Length > 1)
      cableBase.STSValue = strArray[1];
    return cableBase;
  }

  public override string NotificationString
  {
    get
    {
      return $"STSValue: {(this.STSValue == "0" ? (object) this.STSValue : (object) Encoding.ASCII.GetBytes(this.STSValue).FormatBytesToStringArray())}, Data: {Encoding.ASCII.GetBytes(this.Data).FormatBytesToStringArray()}";
    }
  }

  public override object PlotValue => this.Data != null ? (object) this.Data : (object) null;

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
      CompareValue = "",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Equal,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = CableBase.MonnitApplicationID,
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

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    return new List<byte[]>();
  }

  public new static void ClearPendingActionControlCommand(
    Sensor sensor,
    string version,
    int actionCommand,
    bool success,
    byte[] data)
  {
  }

  public new static bool ActionControlCommandIsUrgent(Sensor sensor, string version) => true;

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(CableBase left, CableBase right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(CableBase left, CableBase right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(CableBase left, CableBase right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(CableBase left, CableBase right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(CableBase left, CableBase right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(CableBase left, CableBase right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is CableBase && this.Equals((MonnitApplicationBase) (obj as CableBase));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is CableBase && this.Data == (right as CableBase).Data;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is CableBase && this.Data != (right as CableBase).Data;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<' cannot be applied to operands of type 'byte[]' and 'byte[]'");
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<=' cannot be applied to operands of type 'byte[]' and 'byte[]'");
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>' cannot be applied to operands of type 'byte[]' and 'byte[]'");
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>=' cannot be applied to operands of type 'byte[]' and 'byte[]'");
  }
}
