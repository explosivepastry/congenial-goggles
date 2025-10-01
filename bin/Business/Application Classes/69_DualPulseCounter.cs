// Decompiled with JetBrains decompiler
// Type: Monnit.DualPulseCounter
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class DualPulseCounter : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _TransformValue;
  private SensorAttribute _Label;

  public static long MonnitApplicationID => 69;

  public static string ApplicationName => "Dual Pulse Counter";

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
          Value = "0"
        };
      return this._TransformValue;
    }
  }

  public SensorAttribute Label
  {
    get
    {
      if (this._Label == null || string.IsNullOrEmpty(this._Label.Value))
        this._Label = new SensorAttribute()
        {
          Value = "Pulses"
        };
      return this._Label;
    }
  }

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => DualPulseCounter.MonnitApplicationID;

  public uint Count1 { get; set; }

  public uint Count2 { get; set; }

  public uint ActivityTimer { get; set; }

  public override string Serialize()
  {
    return $"{this.Count1.ToString()},{this.Count2.ToString()},{this.ActivityTimer.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[3]
      {
        new AppDatum(eDatumType.Count, "Count1", (int) this.Count1),
        new AppDatum(eDatumType.Count, "Count2", (int) this.Count2),
        new AppDatum(eDatumType.Time, "ActivityTimer", (long) this.ActivityTimer)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      this.PlotValue,
      (object) (this.TransformValue == null || !(this.TransformValue.Value != "0") ? (double) this.Count2 : (double) this.Count2 * this.TransformValue.Value.ToDouble()),
      (object) this.ActivityTimer
    };
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>()
    {
      DualPulseCounter.GetLabel(sensorID),
      DualPulseCounter.GetLabel(sensorID),
      "Activity Timer"
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) DualPulseCounter.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      return $"{this.Count1.ToString()}, {this.Count2.ToString()} {this.Label.Value} | {this.ActivityTimer.ToString()} {"Seconds"}";
    }
  }

  public override object PlotValue
  {
    get
    {
      return this.TransformValue != null && this.TransformValue.Value != "0" ? (object) ((double) this.Count1 * this.TransformValue.Value.ToDouble()) : (object) this.Count1;
    }
  }

  public static string SpecialExportValue(MonnitApplicationBase app)
  {
    return $"\"{((DualPulseCounter) app).Count1}\",\"{((DualPulseCounter) app).Count2}\",\"{((DualPulseCounter) app).ActivityTimer}\"";
  }

  public static DualPulseCounter Deserialize(string version, string serialized)
  {
    DualPulseCounter dualPulseCounter = new DualPulseCounter();
    if (string.IsNullOrEmpty(serialized))
    {
      dualPulseCounter.Count1 = 0U;
      dualPulseCounter.Count2 = 0U;
      dualPulseCounter.ActivityTimer = 0U;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      dualPulseCounter.Count1 = Convert.ToUInt32(strArray[0]);
      if (strArray.Length == 3)
      {
        dualPulseCounter.Count2 = Convert.ToUInt32(strArray[1]);
        dualPulseCounter.ActivityTimer = Convert.ToUInt32(strArray[2]);
      }
      else
      {
        dualPulseCounter.Count2 = 0U;
        dualPulseCounter.ActivityTimer = 0U;
      }
    }
    return dualPulseCounter;
  }

  public static DualPulseCounter Create(byte[] sdm, int startIndex)
  {
    return new DualPulseCounter()
    {
      Count1 = BitConverter.ToUInt32(sdm, startIndex),
      Count2 = BitConverter.ToUInt32(sdm, startIndex + 4),
      ActivityTimer = BitConverter.ToUInt32(sdm, startIndex + 8)
    };
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["label"]))
      DualPulseCounter.SetLabel(sensor.SensorID, collection["label"]);
    if (string.IsNullOrEmpty(collection["transformValue"]))
      return;
    DualPulseCounter.SetTransform(sensor.SensorID, collection["transformValue"].ToDouble());
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
      ApplicationID = DualPulseCounter.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    }
    else
    {
      sensor.MaximumThreshold = collection["MaximumThreshold_Manual"].ToLong();
      if (sensor.MaximumThreshold < 0L)
        sensor.MaximumThreshold = 0L;
      if (sensor.MaximumThreshold > 4294967294L)
        sensor.MaximumThreshold = 4294967294L;
    }
    DualPulseCounter.SetLabel(sensor.SensorID, collection["label"]);
    DualPulseCounter.SetTransform(sensor.SensorID, collection["transformValue"].ToDouble());
    if (string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      sensor.MinimumThreshold = sensor.MaximumThreshold;
    }
    else
    {
      sensor.MinimumThreshold = collection["MinimumThreshold_Manual"].ToLong();
      if (sensor.MinimumThreshold < 0L)
        sensor.MinimumThreshold = 0L;
      if (sensor.MinimumThreshold > 4294967294L)
        sensor.MinimumThreshold = 4294967294L;
    }
    sensor.Calibration1 = (long) collection["edge"].ToInt();
    sensor.Calibration2 = (long) collection["debounce"].ToInt();
    if (sensor.Calibration2 < 1L)
      sensor.Calibration2 = 1L;
    if (sensor.Calibration2 <= 30L)
      return;
    sensor.Calibration2 = 30L;
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    sensor.ProfileConfigDirty = false;
    sensor.ProfileConfig2Dirty = false;
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    return new List<byte[]>()
    {
      DualPulseCounterBase.CalibrateFrame(sensor.SensorID),
      sensor.ReadProfileConfig(29)
    };
  }

  public static string HystForUI(Sensor sensor) => "Not Used";

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == 4294967294L ? "4294967294" : sensor.MinimumThreshold.ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == 4294967294L ? "4294967294" : sensor.MaximumThreshold.ToString();
  }

  public static double GetTransform(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "transformValue")
        return sensorAttribute.Value.ToDouble();
    }
    return 0.0;
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

  public static bool operator ==(DualPulseCounter left, DualPulseCounter right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(DualPulseCounter left, DualPulseCounter right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(DualPulseCounter left, DualPulseCounter right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(DualPulseCounter left, DualPulseCounter right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(DualPulseCounter left, DualPulseCounter right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(DualPulseCounter left, DualPulseCounter right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is PulseCounter && this.Equals((MonnitApplicationBase) (obj as PulseCounter));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is MultiPulseCounter && (long) this.Count1 == (long) (right as MultiPulseCounter).Count1;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is DualPulseCounter && (int) this.Count1 != (int) (right as DualPulseCounter).Count1;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is DualPulseCounter && this.Count1 < (right as DualPulseCounter).Count1;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is DualPulseCounter && this.Count1 <= (right as DualPulseCounter).Count1;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is DualPulseCounter && this.Count1 > (right as DualPulseCounter).Count1;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is DualPulseCounter && this.Count1 >= (right as DualPulseCounter).Count1;
  }
}
