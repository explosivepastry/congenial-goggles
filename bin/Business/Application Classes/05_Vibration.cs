// Decompiled with JetBrains decompiler
// Type: Monnit.Vibration
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class Vibration : MonnitApplicationBase
{
  private SensorAttribute _ChartFormat;

  public static long MonnitApplicationID => 5;

  public static string ApplicationName => "Activity Detection";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Trigger;

  public override string ChartType => "Point";

  public override long ApplicationID => Vibration.MonnitApplicationID;

  public bool Vibrating { get; set; }

  public override string Serialize() => this.Vibrating.ToString();

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Vibration.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get => this.Vibrating ? "Motion Detected" : "No Motion Detected";
  }

  public override object PlotValue => (object) (this.Vibrating ? 1 : 0);

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.ActivityDetect, "Vibrating", this.Vibrating)
      });
    }
  }

  public static Vibration Deserialize(string version, string serialized)
  {
    return new Vibration()
    {
      Vibrating = serialized.ToBool()
    };
  }

  public static Vibration Create(byte[] sdm, int startIndex)
  {
    return new Vibration()
    {
      Vibrating = sdm[startIndex].ToBool()
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
      SnoozeDuration = 60,
      CompareValue = "True",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Equal,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = Vibration.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["EventDetectionType_Manual"]))
      sensor.EventDetectionType = !(collection["EventDetectionType_Manual"] == Vibration.ValueForZero) ? (!(collection["EventDetectionType_Manual"] == Vibration.ValueForOne) ? 2 : 1) : 0;
    if (!string.IsNullOrEmpty(collection["Sensitivity"]))
      sensor.EventDetectionPeriod = collection["Sensitivity"].ToInt();
    if (sensor.EventDetectionPeriod < 50)
      sensor.EventDetectionPeriod = 50;
    if (sensor.EventDetectionPeriod <= 2000)
      return;
    sensor.EventDetectionPeriod = 2000;
  }

  public static string EventDetectionTypeDescription => "Enter aware state when activity ";

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

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ChartFormat")
        this._ChartFormat = sensorAttribute;
    }
  }

  public static string ValueForZero => "Starts";

  public static string ValueForOne => "Stops";

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Vibration left, Vibration right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Vibration left, Vibration right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Vibration left, Vibration right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Vibration left, Vibration right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Vibration left, Vibration right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Vibration left, Vibration right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Vibration && this.Equals((MonnitApplicationBase) (obj as Vibration));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Vibration && this.Vibrating == (right as Vibration).Vibrating;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Vibration && this.Vibrating != (right as Vibration).Vibrating;
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
