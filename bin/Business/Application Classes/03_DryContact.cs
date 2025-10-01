// Decompiled with JetBrains decompiler
// Type: Monnit.DryContact
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class DryContact : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _ZeroValue;
  private SensorAttribute _OneValue;
  private SensorAttribute _ChartFormat;

  public static long MonnitApplicationID => 3;

  public static string ApplicationName => "Dry Contact";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Trigger;

  public override string ChartType => "Point";

  public override long ApplicationID => DryContact.MonnitApplicationID;

  public bool Pressed { get; set; }

  public override string Serialize() => this.Pressed.ToString();

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) DryContact.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get => this.Pressed ? this.ZeroValue.Value : this.OneValue.Value;
  }

  public override object PlotValue => (object) (this.Pressed ? 1 : 0);

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.DryContact, nameof (DryContact), this.Pressed)
      });
    }
  }

  public static DryContact Deserialize(string version, string serialized)
  {
    return new DryContact()
    {
      Pressed = serialized.ToBool()
    };
  }

  public static DryContact Create(byte[] sdm, int startIndex)
  {
    return new DryContact()
    {
      Pressed = sdm[startIndex].ToBool()
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
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Less_Than,
      CompareValue = "True",
      NotificationClass = eNotificationClass.Application,
      ApplicationID = DryContact.MonnitApplicationID,
      SnoozeDuration = 60,
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
    sensor.EventDetectionType = !(collection["EventDetectionType_Manual"] == DryContact.ValueForZero) ? (!(collection["EventDetectionType_Manual"] == DryContact.ValueForOne) ? 2 : 1) : 0;
  }

  public static string EventDetectionTypeDescription => "Enter aware state when loop is ";

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["zeroValueLabel"]))
      DryContact.SetZeroValue(sensor.SensorID, collection["zeroValueLabel"].ToStringSafe());
    if (string.IsNullOrEmpty(collection["oneValueLabel"]))
      return;
    DryContact.SetOneValue(sensor.SensorID, collection["oneValueLabel"].ToStringSafe());
  }

  public SensorAttribute ZeroValue
  {
    get
    {
      if (this._ZeroValue == null)
        this._ZeroValue = new SensorAttribute()
        {
          Value = "Closed",
          Name = nameof (ZeroValue)
        };
      return this._ZeroValue;
    }
  }

  public SensorAttribute OneValue
  {
    get
    {
      if (this._OneValue == null)
        this._OneValue = new SensorAttribute()
        {
          Value = "Open",
          Name = nameof (OneValue)
        };
      return this._OneValue;
    }
  }

  public SensorAttribute ChartFormat
  {
    get
    {
      if (this._ChartFormat == null)
        this._ChartFormat = new SensorAttribute()
        {
          Value = "Line",
          Name = nameof (ChartFormat)
        };
      return this._ChartFormat;
    }
  }

  public static string GetChartFormat(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ChartFormat")
        return sensorAttribute.Value.ToString();
    }
    return "Line";
  }

  public static void SetChartFormat(long sensorID, string chartFormat)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ChartFormat")
      {
        sensorAttribute.Value = chartFormat.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "ChartFormat",
        Value = chartFormat.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetZeroValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ZeroValue")
        return sensorAttribute.Value.ToStringSafe();
    }
    return "Closed";
  }

  public static void SetZeroValue(long sensorID, string zeroValue)
  {
    if (string.IsNullOrEmpty(zeroValue))
      zeroValue = DryContact.ValueForZero;
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ZeroValue")
      {
        sensorAttribute.Value = zeroValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "ZeroValue",
        Value = zeroValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetOneValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "OneValue")
        return sensorAttribute.Value.ToStringSafe();
    }
    return "Open";
  }

  public static void SetOneValue(long sensorID, string oneValue)
  {
    if (string.IsNullOrEmpty(oneValue))
      oneValue = DryContact.ValueForOne;
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "OneValue")
      {
        sensorAttribute.Value = oneValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "OneValue",
        Value = oneValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ChartFormat")
        this._ChartFormat = sensorAttribute;
      if (sensorAttribute.Name == "ZeroValue")
        this._ZeroValue = sensorAttribute;
      if (sensorAttribute.Name == "OneValue")
        this._OneValue = sensorAttribute;
    }
  }

  public static string ValueForZero => "Closed";

  public static string ValueForOne => "Open";

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(DryContact left, DryContact right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(DryContact left, DryContact right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(DryContact left, DryContact right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(DryContact left, DryContact right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(DryContact left, DryContact right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(DryContact left, DryContact right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is DryContact && this.Equals((MonnitApplicationBase) (obj as DryContact));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is DryContact && this.Pressed == (right as DryContact).Pressed;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is DryContact && this.Pressed != (right as DryContact).Pressed;
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
