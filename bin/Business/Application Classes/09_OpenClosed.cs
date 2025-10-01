// Decompiled with JetBrains decompiler
// Type: Monnit.OpenClosed
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class OpenClosed : MonnitApplicationBase, ISensorAttribute
{
  private bool _Reverse;
  private SensorAttribute _MagnetPresent;
  private SensorAttribute _ChartFormat;

  public static long MonnitApplicationID => 9;

  public static string ApplicationName => "Open / Closed";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Trigger;

  public override string ChartType => "Point";

  public override long ApplicationID => OpenClosed.MonnitApplicationID;

  public bool Magnet { get; set; }

  public bool DoorPosition => !this.Reverse ? this.Magnet : !this.Magnet;

  public override string Serialize() => this.Magnet.ToString();

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.DoorData, "Door Position", this.DoorPosition),
        new AppDatum(eDatumType.MagnetDetect, "Magnet Position", this.Magnet)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[2]
    {
      this.PlotValue,
      (object) (this.Magnet ? 1 : 0)
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[2]
    {
      "Door Position",
      "Magnet Position"
    });
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) OpenClosed.Deserialize(version, serialized);
  }

  public bool Reverse => this._Reverse;

  public void SetSensorAttributes(long sensorID)
  {
    Sensor.Load(sensorID);
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ChartFormat")
        this._ChartFormat = sensorAttribute;
      if (sensorAttribute.Name == "MagnetPresent")
        this._MagnetPresent = sensorAttribute;
    }
    this._Reverse = this.MagnetPresent.Value.ToString() == "Introduced";
  }

  public override string NotificationString => this.DoorPosition ? "Closed" : "Open";

  public override object PlotValue => (object) (this.DoorPosition ? 1 : 0);

  public static OpenClosed Deserialize(string version, string serialized)
  {
    return new OpenClosed() { Magnet = serialized.ToBool() };
  }

  public static OpenClosed Create(byte[] sdm, int startIndex)
  {
    return new OpenClosed()
    {
      Magnet = sdm[startIndex].ToBool()
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
      CompareType = eCompareType.Equal,
      CompareValue = "True",
      NotificationClass = eNotificationClass.Application,
      ApplicationID = OpenClosed.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["EventDetectionType_Manual"]))
      sensor.EventDetectionType = !(collection["EventDetectionType_Manual"] == OpenClosed.ValueForZero) ? (!(collection["EventDetectionType_Manual"] == OpenClosed.ValueForOne) ? 2 : 1) : 0;
    if (string.IsNullOrEmpty(collection["ReportOpen"]))
      return;
    if (collection["ReportOpen"] == OpenClosed.ValueForZero)
      OpenClosed.SetMagnetPresent(sensor.SensorID, "Introduced");
    else if (collection["ReportOpen"] == OpenClosed.ValueForOne)
      OpenClosed.SetMagnetPresent(sensor.SensorID, "Removed");
  }

  public static string EventDetectionTypeDescription => "Enter aware state when magnet is ";

  public SensorAttribute MagnetPresent
  {
    get
    {
      if (this._MagnetPresent == null)
        this._MagnetPresent = new SensorAttribute()
        {
          Value = "Removed",
          Name = nameof (MagnetPresent)
        };
      return this._MagnetPresent;
    }
  }

  public static string GetMagnetPresentValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "MagnetPresent")
        return sensorAttribute.Value.ToStringSafe();
    }
    return "Closed";
  }

  public static void SetMagnetPresent(long sensorID, string magnetPresent)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "MagnetPresent")
      {
        sensorAttribute.Value = magnetPresent.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "MagnetPresent",
        Value = magnetPresent.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
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

  public static string ValueForZero => "Introduced";

  public static string ValueForOne => "Removed";

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(OpenClosed left, OpenClosed right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(OpenClosed left, OpenClosed right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(OpenClosed left, OpenClosed right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(OpenClosed left, OpenClosed right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(OpenClosed left, OpenClosed right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(OpenClosed left, OpenClosed right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is OpenClosed && this.Equals((MonnitApplicationBase) (obj as OpenClosed));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is OpenClosed && this.Magnet == (right as OpenClosed).Magnet;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is OpenClosed && this.Magnet != (right as OpenClosed).Magnet;
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
