// Decompiled with JetBrains decompiler
// Type: Monnit.ResistanceDelta
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;

#nullable disable
namespace Monnit;

public class ResistanceDelta : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _LowValue;
  private SensorAttribute _HighValue;
  private SensorAttribute _Label;

  public static long MonnitApplicationID => ResistanceBase.MonnitApplicationID;

  public static string ApplicationName => nameof (ResistanceDelta);

  public static eApplicationProfileType ProfileType => ResistanceDeltaBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => ResistanceDelta.MonnitApplicationID;

  public double Ohms { get; set; }

  public static ResistanceDelta Create(byte[] sdm, int startIndex)
  {
    byte[] numArray = new byte[sdm.Length - startIndex];
    Array.Copy((Array) sdm, startIndex, (Array) numArray, 0, numArray.Length);
    return new ResistanceDelta()
    {
      Ohms = ResistanceDeltaBase.GetResistance(numArray)
    };
  }

  public override string Serialize() => this.Ohms.ToString();

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.ResistanceData, "Ohms", this.Ohms)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>() { this.Label.Value };
  }

  public static ResistanceDelta Deserialize(string version, string serialized)
  {
    return new ResistanceDelta()
    {
      Ohms = serialized.ToDouble()
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) ResistanceDelta.Deserialize(version, serialized);
  }

  public static double GetLowValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "LowValue")
        return sensorAttribute.Value.ToDouble();
    }
    return 0.0;
  }

  public static void SetLowValue(long sensorID, double lowValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "LowValue")
      {
        sensorAttribute.Value = lowValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "LowValue",
        Value = lowValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static double GetHighValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HighValue")
        return sensorAttribute.Value.ToDouble();
    }
    return 145000.0;
  }

  public static void SetHighValue(long sensorID, double highValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HighValue")
      {
        sensorAttribute.Value = highValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "HighValue",
        Value = highValue.ToString(),
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
    return "Ohms";
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

  public SensorAttribute LowValue
  {
    get
    {
      if (this._LowValue == null)
        this._LowValue = new SensorAttribute()
        {
          Value = "0",
          Name = nameof (LowValue)
        };
      return this._LowValue;
    }
  }

  public SensorAttribute HighValue
  {
    get
    {
      if (this._HighValue == null)
        this._HighValue = new SensorAttribute()
        {
          Value = "145000",
          Name = nameof (HighValue)
        };
      return this._HighValue;
    }
  }

  public SensorAttribute Label
  {
    get
    {
      if (this._Label == null)
        this._Label = new SensorAttribute()
        {
          Value = "Ohms",
          Name = nameof (Label)
        };
      return this._Label;
    }
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "LowValue")
        this._LowValue = sensorAttribute;
      if (sensorAttribute.Name == "HighValue")
        this._HighValue = sensorAttribute;
      if (sensorAttribute.Name == "Label")
        this._Label = sensorAttribute;
    }
  }

  public override object PlotValue
  {
    get
    {
      return this.Ohms >= 0.0 && this.Ohms < 2000000.0 ? (object) this.Ohms.LinearInterpolation(0.0, this.LowValue.Value.ToDouble(), 145000.0, this.HighValue.Value.ToDouble()) : (object) null;
    }
  }

  public override string NotificationString
  {
    get
    {
      if (this.Ohms == 4000000.0)
        return "Unbalanced Sensor/Battery Short";
      if (this.Ohms == 2500000.0 || this.Ohms == 2000000.0)
        return "Open Circuit/Max Resistance";
      if (this.Ohms < 0.0)
        return "Invalid Reading";
      return this.PlotValue != null ? $"{((double) this.PlotValue).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture)} {this.Label.Value.ToString()}" : "Unknown";
    }
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
    sensor.MaximumNetworkHops = sensor.DefaultValue<int>("DefaultMaximumNetworkHops");
    sensor.MinimumCommunicationFrequency = (int) (sensor.ReportInterval * 2.0) + 10;
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      CompareValue = "0",
      NotificationClass = eNotificationClass.Application,
      ApplicationID = ResistanceDelta.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void VerifyNotificationValues(Notification notification, string scale)
  {
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["BasicThreshold"]) && !string.IsNullOrEmpty(collection["BasicThresholdDirection"]) && collection["BasicThresholdDirection"] != "-1")
    {
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
      if (collection["BasicThresholdDirection"].ToInt() == 0)
      {
        sensor.MinimumThreshold = (collection["BasicThreshold"].ToDouble() * 10.0).ToLong();
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      }
      else if (collection["BasicThresholdDirection"].ToInt() == 1)
      {
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
        sensor.MaximumThreshold = (collection["BasicThreshold"].ToDouble() * 10.0).ToLong();
      }
      sensor.MeasurementsPerTransmission = 1;
    }
    else
    {
      ResistanceDelta resistanceDelta = new ResistanceDelta();
      resistanceDelta.SetSensorAttributes(sensor.SensorID);
      resistanceDelta.LowValue.Value.ToDouble();
      resistanceDelta.HighValue.Value.ToDouble();
      double result = 0.0;
      if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      if (!string.IsNullOrEmpty(collection["NegativeChange_Manual"]))
      {
        double o = collection["NegativeChange_Manual"].ToDouble();
        if (o < 0.0)
          o = 0.0;
        if (o > 5000.0)
          o = 5000.0;
        ResistanceDelta.SetNegativeChange(sensor, o.ToInt());
      }
      if (!string.IsNullOrEmpty(collection["PositiveChange_Manual"]))
      {
        double o = collection["PositiveChange_Manual"].ToDouble();
        double num;
        if (o < 0.0)
          num = 0.0;
        if (o > 5000.0)
          num = 5000.0;
        ResistanceDelta.SetPositiveChange(sensor, o.ToInt());
      }
      if (double.TryParse(collection["MinimumThreshold_Manual"], out result))
      {
        double num = collection["MinimumThreshold_Manual"].ToDouble();
        sensor.MinimumThreshold = num >= 0.1 ? (num <= 43200.0 ? (num * 1000.0).ToLong() : 43200000.ToLong()) : 100.0.ToLong();
      }
      if (double.TryParse(collection["MaximumThreshold_Manual"], out result))
      {
        double num = collection["MaximumThreshold_Manual"].ToDouble();
        sensor.MaximumThreshold = num >= 0.1 ? (num <= 43200.0 ? (num * 1000.0).ToLong() : 43200000.ToLong()) : 100.0.ToLong();
      }
    }
  }

  public static void SetPositiveChange(Sensor sens, int value)
  {
    value = (value * 10).ToInt();
    int num = (int) sens.Hysteresis & (int) ushort.MaxValue;
    sens.Hysteresis = (long) (num | (int) (ushort) value << 16 /*0x10*/);
  }

  public static int GetPositiveChange(Sensor sens)
  {
    return ((double) (short) (ushort) ((uint) (int) ((long) (int) sens.Hysteresis & 4294901760L) >> 16 /*0x10*/) * 0.1).ToInt();
  }

  public static void SetNegativeChange(Sensor sens, int value)
  {
    value = (value * 10).ToInt();
    int num = (int) ((long) (int) sens.Hysteresis & 4294901760L);
    sens.Hysteresis = (long) (num | (int) (ushort) value);
  }

  public static int GetNegativeChange(Sensor sens)
  {
    return ((double) (short) (ushort) ((uint) (int) sens.Hysteresis & (uint) ushort.MaxValue) * 0.1).ToInt();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : (sensor.MinimumThreshold.ToDouble() / 1000.0).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : (sensor.MaximumThreshold.ToDouble() / 1000.0).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["lowValue"]))
      ResistanceDelta.SetLowValue(sensor.SensorID, collection["lowValue"].ToDouble());
    if (!string.IsNullOrEmpty(collection["highValue"]))
      ResistanceDelta.SetHighValue(sensor.SensorID, collection["highValue"].ToDouble());
    if (string.IsNullOrEmpty(collection["label"]))
      return;
    ResistanceDelta.SetLabel(sensor.SensorID, collection["label"]);
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    collection["observed"].ToDouble().LinearInterpolation(ResistanceDelta.GetLowValue(sensor.SensorID), 0.0, ResistanceDelta.GetHighValue(sensor.SensorID), 145000.0);
    double num = collection["actual"].ToDouble().LinearInterpolation(ResistanceDelta.GetLowValue(sensor.SensorID), 0.0, ResistanceDelta.GetHighValue(sensor.SensorID), 145000.0) * 1000.0;
    sensor.Calibration1 = (num / 1000.0).ToLong();
    sensor.MarkClean(false);
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static void DuplicateConfiguration(Sensor source, Sensor target)
  {
    target.ReportInterval = source.ReportInterval;
    target.ActiveStateInterval = source.ActiveStateInterval;
    target.MinimumCommunicationFrequency = source.MinimumCommunicationFrequency;
    target.ChannelMask = source.ChannelMask;
    target.StandardMessageDelay = source.StandardMessageDelay;
    target.TransmitIntervalLink = source.TransmitIntervalLink;
    target.TransmitIntervalTest = source.TransmitIntervalTest;
    target.TestTransmitCount = source.TestTransmitCount;
    target.MaximumNetworkHops = source.MaximumNetworkHops;
    target.RetryCount = source.RetryCount;
    target.Recovery = source.Recovery;
    target.MeasurementsPerTransmission = source.MeasurementsPerTransmission;
    target.TransmitOffset = source.TransmitOffset;
    target.Hysteresis = source.Hysteresis;
    target.MinimumThreshold = source.MinimumThreshold;
    target.MaximumThreshold = source.MaximumThreshold;
    target.EventDetectionType = source.EventDetectionType;
    target.EventDetectionPeriod = source.EventDetectionPeriod;
    target.EventDetectionCount = source.EventDetectionCount;
    target.RearmTime = source.RearmTime;
    target.BiStable = source.BiStable;
    target.TagString = source.TagString;
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      numArrayList.Add(ResistanceDeltaBase.CalibrateFrame(sensor.SensorID, (double) sensor.Calibration1));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(ResistanceDelta left, ResistanceDelta right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(ResistanceDelta left, ResistanceDelta right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(ResistanceDelta left, ResistanceDelta right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(ResistanceDelta left, ResistanceDelta right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(ResistanceDelta left, ResistanceDelta right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(ResistanceDelta left, ResistanceDelta right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is ResistanceDelta && this.Equals((MonnitApplicationBase) (obj as ResistanceDelta));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is ResistanceDelta && this.Ohms == (right as ResistanceDelta).Ohms;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is ResistanceDelta && this.Ohms != (right as ResistanceDelta).Ohms;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is ResistanceDelta && this.Ohms < (right as ResistanceDelta).Ohms;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is ResistanceDelta && this.Ohms <= (right as ResistanceDelta).Ohms;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is ResistanceDelta && this.Ohms > (right as ResistanceDelta).Ohms;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is ResistanceDelta && this.Ohms >= (right as ResistanceDelta).Ohms;
  }

  public static long DefaultMinThreshold => 30000;

  public static long DefaultMaxThreshold => 30000;

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
    sensor.MeasurementsPerTransmission = 1;
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }

  public new static NotificationScaleInfoModel GetScalingInfo(Sensor sens)
  {
    NotificationScaleInfoModel scalingInfo = new NotificationScaleInfoModel();
    ResistanceDelta resistanceDelta = new ResistanceDelta();
    resistanceDelta.SetSensorAttributes(sens.SensorID);
    scalingInfo.Label = resistanceDelta.Label.Value.ToStringSafe();
    scalingInfo.profileLow = resistanceDelta.LowValue.Value.ToDouble();
    scalingInfo.profileHigh = resistanceDelta.HighValue.Value.ToDouble();
    scalingInfo.baseLow = 0.0;
    scalingInfo.baseHigh = 145000.0;
    return scalingInfo;
  }
}
