// Decompiled with JetBrains decompiler
// Type: Monnit.LiquidLevel
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

public class LiquidLevel : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _StandardOrMetric;

  public static long MonnitApplicationID => 26;

  public static string ApplicationName => "Liquid Level - 8 in";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => LiquidLevel.MonnitApplicationID;

  public double Inch { get; set; }

  public int State { get; set; }

  public override string Serialize() => this.Inch.ToString();

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.Distance, "Inch", this.Inch)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[1]
    {
      LiquidLevel.IsInches(sensorID) ? "Inches" : "Centimeters"
    });
  }

  public static string GetScaleLabel(long sensorID)
  {
    return LiquidLevel.IsInches(sensorID) ? "Inches" : "Centimeters";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) LiquidLevel.Deserialize(version, serialized);
  }

  public static bool IsInches(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "StandardOrMetric" && sensorAttribute.Value == "Metric")
        return false;
    }
    return true;
  }

  public static void MakeInches(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "StandardOrMetric")
        sensorAttribute.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void MakeMetric(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "StandardOrMetric")
      {
        if (sensorAttribute.Value != "Metric")
        {
          sensorAttribute.Value = "Metric";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "StandardOrMetric",
        Value = "Metric",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute StandardOrMetric => this._StandardOrMetric;

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "StandardOrMetric")
        this._StandardOrMetric = sensorAttribute;
    }
  }

  public override bool IsValid
  {
    get => (this.State & 16 /*0x10*/) != 16 /*0x10*/ && (this.State & 32 /*0x20*/) != 32 /*0x20*/;
  }

  public override string NotificationString
  {
    get
    {
      if ((this.State & 48 /*0x30*/) == 48 /*0x30*/)
        return "Sensor Short";
      if ((this.State & 16 /*0x10*/) == 16 /*0x10*/)
        return "Hardware Failure (Short)";
      if ((this.State & 32 /*0x20*/) == 32 /*0x20*/)
        return "Open Circuit";
      return this.StandardOrMetric != null && this.StandardOrMetric.Value == "Metric" ? this.Inch.ToCentimeter().ToString("#0.# cm", (IFormatProvider) CultureInfo.InvariantCulture) : this.Inch.ToString("#0.# in", (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  public override object PlotValue
  {
    get
    {
      if ((this.State >> 7 & 240 /*0xF0*/) == 16 /*0x10*/ || (this.State >> 7 & 240 /*0xF0*/) == 32 /*0x20*/ || (this.State >> 7 & 240 /*0xF0*/) == 48 /*0x30*/ || this.Inch <= -100.0 || this.Inch >= 300.0)
        return (object) null;
      return this.StandardOrMetric != null && this.StandardOrMetric.Value == "Metric" ? (object) this.Inch.ToCentimeter() : (object) this.Inch;
    }
  }

  public static LiquidLevel Deserialize(string version, string serialized)
  {
    LiquidLevel liquidLevel = new LiquidLevel();
    string[] strArray = serialized.Split('|');
    liquidLevel.Inch = strArray[0].ToDouble();
    liquidLevel.State = strArray.Length <= 1 ? 0 : strArray[1].ToInt();
    return liquidLevel;
  }

  public static LiquidLevel Create(byte[] sdm, int startIndex)
  {
    return new LiquidLevel()
    {
      Inch = BitConverter.ToInt16(sdm, startIndex).ToDouble() / 100.0,
      State = (int) sdm[startIndex - 1]
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
      ApplicationID = LiquidLevel.MonnitApplicationID,
      SnoozeDuration = 60,
      Scale = LiquidLevel.IsInches(sensor.SensorID) ? "I" : "C",
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static Dictionary<string, string> NotificationScaleValues()
  {
    return new Dictionary<string, string>()
    {
      {
        "I",
        "Inches"
      },
      {
        "C",
        "Centimeters"
      }
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    sensor.ReportInterval = !string.IsNullOrEmpty(collection["ReportInterval"]) ? collection["ReportInterval"].ToDouble() : 120.0;
    sensor.ActiveStateInterval = !string.IsNullOrEmpty(collection["ActiveStateInterval"]) ? (collection["ActiveStateInterval"].ToDouble() <= collection["ReportInterval"].ToDouble() ? collection["ActiveStateInterval"].ToDouble() : collection["ReportInterval"].ToDouble()) : (!string.IsNullOrEmpty(collection["ReportInterval"]) ? collection["ReportInterval"].ToDouble() : 120.0);
    sensor.MeasurementsPerTransmission = !string.IsNullOrWhiteSpace(collection["MeasurementsPerTransmission"]) ? Math.Round(collection["MeasurementsPerTransmission"].ToDouble()).ToInt() : 6;
    double result = 0.0;
    if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
      sensor.Hysteresis = (collection["Hysteresis_Manual"].ToDouble() * 100.0).ToLong();
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      sensor.MinimumThreshold = (collection["MinimumThreshold_Manual"].ToDouble() * 100.0).ToLong();
      if (sensor.MinimumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MinimumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    }
    if (string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
      return;
    sensor.MaximumThreshold = (collection["MaximumThreshold_Manual"].ToDouble() * 100.0).ToLong();
    if (sensor.MaximumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if (sensor.MaximumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if (sensor.MinimumThreshold > sensor.MaximumThreshold)
      sensor.MaximumThreshold = sensor.MinimumThreshold;
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection returnData)
  {
    if (collection["TempScale"] == "on")
    {
      returnData["TempScale"] = "I";
      LiquidLevel.MakeInches(sensor.SensorID);
    }
    else
    {
      returnData["TempScale"] = "C";
      LiquidLevel.MakeMetric(sensor.SensorID);
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    switch (collection["calibrationStep"])
    {
      case "Empty":
        sensor.Calibration1 = 0L;
        sensor.Calibration4 = 1L;
        sensor.PendingActionControlCommand = true;
        sensor.ProfileConfigDirty = false;
        sensor.ProfileConfig2Dirty = false;
        break;
      case "Full":
        sensor.Calibration2 = 0L;
        sensor.Calibration3 = 8L;
        sensor.PendingActionControlCommand = true;
        sensor.ProfileConfigDirty = false;
        sensor.ProfileConfig2Dirty = false;
        break;
    }
    if (!sensor.PendingActionControlCommand)
      return;
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

  public static string HystForUI(Sensor sensor)
  {
    bool flag = LiquidLevel.IsInches(sensor.SensorID);
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : ((flag ? Convert.ToDouble(sensor.Hysteresis) : Convert.ToDouble(sensor.Hysteresis).ToCentimeter()) / 100.0).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string GetLabel() => "";

  public static string MinThreshForUI(Sensor sensor)
  {
    bool flag = LiquidLevel.IsInches(sensor.SensorID);
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : (flag ? (Convert.ToDouble(sensor.DefaultValue<long>("DefaultMinimumThreshold")) / 100.0).ToString("#0.#") : (Convert.ToDouble(sensor.DefaultValue<long>("DefaultMinimumThreshold")) / 100.0).ToCentimeter().ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture));
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    bool flag = LiquidLevel.IsInches(sensor.SensorID);
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : (flag ? (Convert.ToDouble(sensor.DefaultValue<long>("DefaultMaximumThreshold")) / 100.0).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : (Convert.ToDouble(sensor.DefaultValue<long>("DefaultMinimumThreshold")) / 100.0).ToCentimeter().ToString("#0.#"));
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    byte[] numArray = (byte[]) null;
    if (sensor.Calibration1 == 0L)
      numArray = LiquidLevelBase.EmptyCalibration(sensor.SensorID, (double) sensor.Calibration4);
    else if (sensor.Calibration2 == 0L)
      numArray = LiquidLevelBase.FullCalibration(sensor.SensorID, (double) sensor.Calibration3);
    numArrayList.Add(numArray);
    numArrayList.Add(sensor.ReadProfileConfig(29));
    return numArrayList;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(LiquidLevel left, LiquidLevel right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(LiquidLevel left, LiquidLevel right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(LiquidLevel left, LiquidLevel right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(LiquidLevel left, LiquidLevel right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(LiquidLevel left, LiquidLevel right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(LiquidLevel left, LiquidLevel right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is LiquidLevel && this.Equals((MonnitApplicationBase) (obj as LiquidLevel));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is LiquidLevel && this.Inch == (right as LiquidLevel).Inch;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is LiquidLevel && this.Inch != (right as LiquidLevel).Inch;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is LiquidLevel && this.Inch < (right as LiquidLevel).Inch;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is LiquidLevel && this.Inch <= (right as LiquidLevel).Inch;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is LiquidLevel && this.Inch > (right as LiquidLevel).Inch;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is LiquidLevel && this.Inch >= (right as LiquidLevel).Inch;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 800;

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }
}
