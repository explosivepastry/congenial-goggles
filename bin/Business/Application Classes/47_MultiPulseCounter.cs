// Decompiled with JetBrains decompiler
// Type: Monnit.MultiPulseCounter
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class MultiPulseCounter : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _TransformValue;
  private SensorAttribute _Label;

  public static long MonnitApplicationID => 47;

  public static string ApplicationName => "Pulse Counter - 4 Input";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => MultiPulseCounter.MonnitApplicationID;

  public int Count1 { get; set; }

  public int Count2 { get; set; }

  public int Count3 { get; set; }

  public int Count4 { get; set; }

  public override string Serialize()
  {
    return $"{this.Count1.ToString()},{this.Count2.ToString()},{this.Count3.ToString()},{this.Count4.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[4]
      {
        new AppDatum(eDatumType.Count, "Count1", this.Count1),
        new AppDatum(eDatumType.Count, "Count2", this.Count2),
        new AppDatum(eDatumType.Count, "Count3", this.Count3),
        new AppDatum(eDatumType.Count, "Count4", this.Count4)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[4]
    {
      (object) this.Count1,
      (object) this.Count2,
      (object) this.Count3,
      (object) this.Count4
    });
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) MultiPulseCounter.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      return $"{this.Count1.ToString()},{this.Count2.ToString()},{this.Count3.ToString()},{this.Count4.ToString()}";
    }
  }

  public override object PlotValue => (object) this.Count1;

  public static string SpecialExportValue(MonnitApplicationBase app)
  {
    return $"\"{((MultiPulseCounter) app).Count1}\",\"{((MultiPulseCounter) app).Count2}\",\"{((MultiPulseCounter) app).Count3}\",\"{((MultiPulseCounter) app).Count4}\"";
  }

  public static MultiPulseCounter Deserialize(string version, string serialized)
  {
    MultiPulseCounter multiPulseCounter = new MultiPulseCounter();
    if (string.IsNullOrEmpty(serialized))
    {
      multiPulseCounter.Count1 = 0;
      multiPulseCounter.Count2 = 0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      multiPulseCounter.Count1 = strArray[0].ToInt();
      if (strArray.Length == 4)
      {
        multiPulseCounter.Count2 = strArray[1].ToInt();
        multiPulseCounter.Count3 = strArray[2].ToInt();
        multiPulseCounter.Count4 = strArray[3].ToInt();
      }
      else
      {
        multiPulseCounter.Count2 = multiPulseCounter.Count1;
        multiPulseCounter.Count3 = multiPulseCounter.Count1;
        multiPulseCounter.Count4 = multiPulseCounter.Count1;
      }
    }
    return multiPulseCounter;
  }

  public static MultiPulseCounter Create(byte[] sdm, int startIndex)
  {
    return new MultiPulseCounter()
    {
      Count1 = (int) BitConverter.ToUInt16(sdm, startIndex),
      Count2 = (int) BitConverter.ToUInt16(sdm, startIndex + 2),
      Count3 = (int) BitConverter.ToUInt16(sdm, startIndex + 4),
      Count4 = (int) BitConverter.ToUInt16(sdm, startIndex + 6)
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
      ApplicationID = MultiPulseCounter.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

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

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["label"]))
      MultiPulseCounter.SetLabel(sensor.SensorID, collection["label"]);
    if (string.IsNullOrEmpty(collection["transformValue"]))
      return;
    MultiPulseCounter.SetTransform(sensor.SensorID, collection["transformValue"].ToDouble());
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrWhiteSpace(collection["ReportInterval"]))
      sensor.ActiveStateInterval = collection["ReportInterval"].ToDouble();
    if (string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    }
    else
    {
      sensor.MaximumThreshold = collection["MaximumThreshold_Manual"].ToLong();
      if (sensor.MaximumThreshold < 1L)
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      if (sensor.MaximumThreshold > (long) ushort.MaxValue)
        sensor.MaximumThreshold = (long) ushort.MaxValue;
    }
    sensor.MinimumThreshold = sensor.MaximumThreshold;
    sensor.Calibration1 = (long) collection["edge"].ToInt();
    sensor.Calibration2 = (long) collection["debounce"].ToInt();
    if (sensor.Calibration2 < 1L)
      sensor.Calibration2 = 1L;
    if (sensor.Calibration2 <= 30L)
      return;
    sensor.Calibration2 = 30L;
  }

  public static string HystForUI(Sensor sensor) => "Not Used";

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue || sensor.MinimumThreshold == (long) ushort.MaxValue ? ((int) ushort.MaxValue).ToString() : sensor.MinimumThreshold.ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue || sensor.MaximumThreshold == (long) ushort.MaxValue ? ((int) ushort.MaxValue).ToString() : sensor.MaximumThreshold.ToString();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(MultiPulseCounter left, MultiPulseCounter right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(MultiPulseCounter left, MultiPulseCounter right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(MultiPulseCounter left, MultiPulseCounter right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(MultiPulseCounter left, MultiPulseCounter right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(MultiPulseCounter left, MultiPulseCounter right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(MultiPulseCounter left, MultiPulseCounter right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is MultiPulseCounter && this.Equals((MonnitApplicationBase) (obj as MultiPulseCounter));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is MultiPulseCounter && this.Count1 == (right as MultiPulseCounter).Count1;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is MultiPulseCounter && this.Count1 != (right as MultiPulseCounter).Count1;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is MultiPulseCounter && this.Count1 < (right as MultiPulseCounter).Count1;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is MultiPulseCounter && this.Count1 <= (right as MultiPulseCounter).Count1;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is MultiPulseCounter && this.Count1 > (right as MultiPulseCounter).Count1;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is MultiPulseCounter && this.Count1 >= (right as MultiPulseCounter).Count1;
  }
}
