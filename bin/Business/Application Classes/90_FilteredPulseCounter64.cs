// Decompiled with JetBrains decompiler
// Type: Monnit.FilteredPulseCounter64
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

public class FilteredPulseCounter64 : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _TransformValue;
  private SensorAttribute _Label;

  public static long MonnitApplicationID => 90;

  public static string ApplicationName => "Filtered Pulse Counter 64";

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

  public override long ApplicationID => FilteredPulseCounter64.MonnitApplicationID;

  public ulong Count { get; set; }

  public override string Serialize() => this.Count.ToString();

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.Count, "Count", (double) this.Count)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[1]
    {
      this.PlotValue
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[1]
    {
      FilteredPulseCounter64.GetLabel(sensorID)
    });
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) FilteredPulseCounter64.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      return this.TransformValue != null && this.TransformValue.Value != "0" && this.TransformValue.Value != "1" ? $"{this.PlotValue.ToDouble().ToString("#0.######", (IFormatProvider) CultureInfo.InvariantCulture)} {this.Label.Value}" : $"{this.PlotValue} {this.Label.Value}";
    }
  }

  public override object PlotValue
  {
    get
    {
      return this.TransformValue != null && this.TransformValue.Value != "0" && this.TransformValue.Value != "1" ? (object) ((double) this.Count * this.TransformValue.Value.ToDouble()) : (object) this.Count;
    }
  }

  public static FilteredPulseCounter64 Deserialize(string version, string serialized)
  {
    FilteredPulseCounter64 filteredPulseCounter64 = new FilteredPulseCounter64();
    if (string.IsNullOrEmpty(serialized))
    {
      filteredPulseCounter64.Count = 0UL;
    }
    else
    {
      try
      {
        string[] strArray = serialized.Split(new char[1]
        {
          ','
        }, StringSplitOptions.RemoveEmptyEntries);
        filteredPulseCounter64.Count = strArray[0].ToUInt64();
      }
      catch
      {
        filteredPulseCounter64.Count = 0UL;
      }
    }
    return filteredPulseCounter64;
  }

  public static FilteredPulseCounter64 Create(byte[] sdm, int startIndex)
  {
    return new FilteredPulseCounter64()
    {
      Count = BitConverter.ToUInt64(sdm, startIndex)
    };
  }

  public new static void DefaultConfigurationSettings(Sensor sensor)
  {
    FilteredPulseCounter64Base.GetDefaults(new Version(sensor.FirmwareVersion), sensor.GenerationType);
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

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["label"]))
      FilteredPulseCounter64.SetLabel(sensor.SensorID, collection["label"]);
    if (string.IsNullOrEmpty(collection["transformValue"]))
      return;
    FilteredPulseCounter64.SetTransform(sensor.SensorID, collection["transformValue"].ToDouble());
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = FilteredPulseCounter64.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    double num = FilteredPulseCounter64.GetTransform(sensor.SensorID) <= 0.0 ? 1.0 : FilteredPulseCounter64.GetTransform(sensor.SensorID);
    if (string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    }
    else
    {
      sensor.MaximumThreshold = ((double) collection["MaximumThreshold_Manual"].ToLong() / num).ToLong();
      if (sensor.MaximumThreshold < 1L)
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      if (sensor.MaximumThreshold > (long) uint.MaxValue)
        sensor.MaximumThreshold = (long) uint.MaxValue;
    }
    if (string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      sensor.MinimumThreshold = sensor.MaximumThreshold;
    }
    else
    {
      sensor.MinimumThreshold = ((double) collection["MinimumThreshold_Manual"].ToLong() / num).ToLong();
      if (sensor.MinimumThreshold < 1L)
        sensor.MinimumThreshold = sensor.MaximumThreshold;
      if (sensor.MinimumThreshold > (long) uint.MaxValue)
        sensor.MinimumThreshold = (long) uint.MaxValue;
    }
    sensor.Calibration1 = (long) collection["edge"].ToInt();
    sensor.Calibration4 = (long) collection["filter"].ToInt();
    if (sensor.Calibration4 > 2L)
      sensor.Calibration4 = 2L;
    if (sensor.Calibration4 >= 0L)
      return;
    sensor.Calibration4 = 0L;
  }

  public static string HystForUI(Sensor sensor) => "Not Used";

  public static string MinThreshForUI(Sensor sensor)
  {
    return ((double) sensor.MinimumThreshold * FilteredPulseCounter64.GetTransform(sensor.SensorID)).ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return ((double) sensor.MaximumThreshold * FilteredPulseCounter64.GetTransform(sensor.SensorID)).ToString();
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

  public static bool operator ==(FilteredPulseCounter64 left, FilteredPulseCounter64 right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(FilteredPulseCounter64 left, FilteredPulseCounter64 right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(FilteredPulseCounter64 left, FilteredPulseCounter64 right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(FilteredPulseCounter64 left, FilteredPulseCounter64 right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(FilteredPulseCounter64 left, FilteredPulseCounter64 right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(FilteredPulseCounter64 left, FilteredPulseCounter64 right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is FilteredPulseCounter64 && this.Equals((MonnitApplicationBase) (obj as FilteredPulseCounter64));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is FilteredPulseCounter64 && (long) this.Count == (long) (right as FilteredPulseCounter64).Count;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is FilteredPulseCounter64 && (long) this.Count != (long) (right as FilteredPulseCounter64).Count;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is FilteredPulseCounter64 && this.Count < (right as FilteredPulseCounter64).Count;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is FilteredPulseCounter64 && this.Count <= (right as FilteredPulseCounter64).Count;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is FilteredPulseCounter64 && this.Count > (right as FilteredPulseCounter64).Count;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is FilteredPulseCounter64 && this.Count >= (right as FilteredPulseCounter64).Count;
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    sensor.ProfileConfigDirty = false;
    sensor.ProfileConfig2Dirty = false;
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public static void UseSerializedRecipientProperties(
    Sensor sens,
    int queueID,
    string serializedRecipientProperties)
  {
    FilteredPulseCounter64.CalibrateSensor(sens, new NameValueCollection()
    {
      {
        "acc",
        serializedRecipientProperties
      },
      {
        nameof (queueID),
        queueID.ToString()
      }
    });
  }

  public static string CreateSerializedRecipientProperties(int acc) => $"{acc}";

  public static void ParseSerializedRecipientProperties(string serialized, out int acc)
  {
    acc = 0;
    try
    {
      string[] strArray = serialized.Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      acc = strArray[0].ToInt();
    }
    catch
    {
    }
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    return new List<byte[]>()
    {
      FilteredPulseCounter64Base.CalibrateFrame(sensor.SensorID),
      sensor.ReadProfileConfig(29)
    };
  }

  public new static bool ActionControlCommandIsUrgent(Sensor sensor, string version) => false;

  public new static void ClearPendingActionControlCommand(
    Sensor sensor,
    string version,
    int actionCommand,
    bool success,
    byte[] data)
  {
    List<NotificationRecorded> messagesForControl = NotificationRecorded.LoadGetNoneDevliveredMessagesForControl(sensor.SensorID);
    if (messagesForControl.Count == 0)
      return;
    foreach (NotificationRecorded notificationRecorded in messagesForControl)
    {
      if (notificationRecorded.NotificationType == eNotificationType.ResetAccumulator && !(notificationRecorded.NotificationDate > sensor.LastCommunicationDate))
      {
        notificationRecorded.Delivered = true;
        notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
        notificationRecorded.Status = "Reset Complete";
        notificationRecorded.Save();
      }
    }
  }
}
