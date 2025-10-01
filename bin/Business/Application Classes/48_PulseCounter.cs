// Decompiled with JetBrains decompiler
// Type: Monnit.PulseCounter
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

public class PulseCounter : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _TransformValue;
  private SensorAttribute _Label;

  public static long MonnitApplicationID => 48 /*0x30*/;

  public static string ApplicationName => "Pulse Counter - 1 Input";

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "transformValue")
        this._TransformValue = sensorAttribute;
      if (sensorAttribute.Name == "Label")
        this._Label = sensorAttribute;
    }
  }

  public SensorAttribute TransformValue
  {
    get
    {
      if (this._TransformValue == null)
        this._TransformValue = new SensorAttribute()
        {
          Value = "1"
        };
      return this._TransformValue;
    }
  }

  public SensorAttribute Label
  {
    get
    {
      if (this._Label == null)
        this._Label = new SensorAttribute()
        {
          Value = "Pulses"
        };
      return this._Label;
    }
  }

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => PulseCounter.MonnitApplicationID;

  public int Count { get; set; }

  public int PastCount { get; set; }

  public override string Serialize()
  {
    int num = this.Count;
    string str1 = num.ToString();
    num = this.PastCount;
    string str2 = num.ToString();
    return $"{str1},{str2}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.Count, "Count", this.Count)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>()
    {
      PulseCounter.GetLabel(sensorID)
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) PulseCounter.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      return $"{this.PlotValue.ToDouble().ToString("#0.######", (IFormatProvider) CultureInfo.InvariantCulture)} {this.Label.Value}";
    }
  }

  public override object PlotValue
  {
    get
    {
      return this.TransformValue != null && this.TransformValue.Value != "0" ? (object) ((double) this.Count * this.TransformValue.Value.ToDouble()) : (object) this.Count;
    }
  }

  public static PulseCounter Deserialize(string version, string serialized)
  {
    PulseCounter pulseCounter = new PulseCounter();
    if (string.IsNullOrEmpty(serialized))
    {
      pulseCounter.Count = 0;
      pulseCounter.PastCount = 0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      pulseCounter.Count = strArray[0].ToInt();
      pulseCounter.PastCount = strArray.Length != 2 ? 0 : strArray[1].ToInt();
    }
    return pulseCounter;
  }

  public static PulseCounter Create(byte[] sdm, int startIndex)
  {
    return new PulseCounter()
    {
      Count = (int) BitConverter.ToUInt16(sdm, startIndex),
      PastCount = (int) BitConverter.ToUInt16(sdm, startIndex + 2)
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
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = PulseCounter.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["label"]))
      PulseCounter.SetLabel(sensor.SensorID, collection["label"]);
    if (string.IsNullOrEmpty(collection["transformValue"]))
      return;
    PulseCounter.SetTransform(sensor.SensorID, collection["transformValue"].ToDouble());
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    sensor.ActiveStateInterval = collection["ReportInterval"].ToDouble();
    if (string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    }
    else
    {
      double num = PulseCounter.GetTransform(sensor.SensorID) <= 0.0 ? 1.0 : PulseCounter.GetTransform(sensor.SensorID);
      sensor.MaximumThreshold = ((double) collection["MaximumThreshold_Manual"].ToLong() / num).ToLong();
      if (sensor.MaximumThreshold < 1L)
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      if (sensor.MaximumThreshold > (long) ushort.MaxValue)
        sensor.MaximumThreshold = (long) ushort.MaxValue;
    }
    sensor.MinimumThreshold = sensor.MaximumThreshold;
    sensor.Calibration1 = (long) collection["edge"].ToInt();
    sensor.Calibration2 = (long) collection["debounce"].ToInt();
    if (sensor.Calibration2 < 12L)
      sensor.Calibration2 = 12L;
    if (sensor.Calibration2 <= 60000L)
      return;
    sensor.Calibration2 = 60000L;
  }

  public static string HystForUI(Sensor sensor) => "Not Used";

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue || sensor.MinimumThreshold == (long) ushort.MaxValue ? ((int) ushort.MaxValue).ToString() : sensor.MinimumThreshold.ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue || sensor.MaximumThreshold == (long) ushort.MaxValue ? ((int) ushort.MaxValue).ToString() : ((double) sensor.MaximumThreshold * PulseCounter.GetTransform(sensor.SensorID)).ToString();
  }

  public static double GetTransform(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "transformValue")
        return sensorAttribute.Value.ToDouble();
    }
    return 1.0;
  }

  public static void SetTransform(long sensorID, double transformValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == nameof (transformValue))
      {
        sensorAttribute.Value = transformValue > 0.0 ? transformValue.ToString() : "1";
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = nameof (transformValue),
        Value = (transformValue > 0.0 ? transformValue.ToString() : "1"),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetLabel(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        return sensorAttribute.Value;
    }
    return "pulses";
  }

  public static void SetLabel(long sensorID, string label)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
      {
        sensorAttribute.Value = label;
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "Label",
        Value = label,
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(PulseCounter left, PulseCounter right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(PulseCounter left, PulseCounter right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(PulseCounter left, PulseCounter right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(PulseCounter left, PulseCounter right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(PulseCounter left, PulseCounter right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(PulseCounter left, PulseCounter right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is PulseCounter && this.Equals((MonnitApplicationBase) (obj as PulseCounter));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is PulseCounter && this.Count == (right as PulseCounter).Count;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is PulseCounter && this.Count != (right as PulseCounter).Count;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is PulseCounter && this.Count < (right as PulseCounter).Count;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is PulseCounter && this.Count <= (right as PulseCounter).Count;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is PulseCounter && this.Count > (right as PulseCounter).Count;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is PulseCounter && this.Count >= (right as PulseCounter).Count;
  }
}
