// Decompiled with JetBrains decompiler
// Type: Monnit.Current
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

#nullable disable
namespace Monnit;

public class Current : MonnitApplicationBase
{
  public static long MonnitApplicationID => CurrentBase.MonnitApplicationID;

  public static string ApplicationName => "Current Transducer";

  public static eApplicationProfileType ProfileType => CurrentBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => Current.MonnitApplicationID;

  public double AmpHours { get; set; }

  public double AvgCurrent { get; set; }

  public double MaxCurrent { get; set; }

  public double MinCurrent { get; set; }

  public override string Serialize()
  {
    return $"{this.AmpHours.ToString()},{this.AvgCurrent.ToString()},{this.MaxCurrent.ToString()},{this.MinCurrent.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[4]
      {
        new AppDatum(eDatumType.AmpHours, "AmpHours", this.AmpHours),
        new AppDatum(eDatumType.Amps, "AvgCurrent", this.AvgCurrent),
        new AppDatum(eDatumType.Amps, "MaxCurrent", this.MaxCurrent),
        new AppDatum(eDatumType.Amps, "MinCurrent", this.MinCurrent)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>(this.Datums.Select<AppDatum, object>((Func<AppDatum, object>) (da => da.data.compvalue)));
  }

  public new static NotificationScaleInfoModel GetScalingInfo(Sensor sens)
  {
    NotificationScaleInfoModel scalingInfo = new NotificationScaleInfoModel();
    Current current = new Current();
    List<SensorAttribute> source = SensorAttribute.LoadBySensorID(sens.SensorID);
    SensorAttribute sensorAttribute1 = source.Where<SensorAttribute>((Func<SensorAttribute, bool>) (attr => attr.Name.ToLower() == "lowvalue")).FirstOrDefault<SensorAttribute>();
    SensorAttribute sensorAttribute2 = source.Where<SensorAttribute>((Func<SensorAttribute, bool>) (attr => attr.Name.ToLower() == "highvalue")).FirstOrDefault<SensorAttribute>();
    scalingInfo.Label = "Ah";
    scalingInfo.profileLow = sensorAttribute1 == null ? 4.0 : sensorAttribute1.Value.ToDouble();
    scalingInfo.profileHigh = sensorAttribute2 == null ? 20.0 : sensorAttribute2.Value.ToDouble();
    scalingInfo.baseLow = 4.0;
    scalingInfo.baseHigh = 20.0;
    return scalingInfo;
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Current.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      return $"Amp Hours: {this.AmpHours} | Average Current: {this.AvgCurrent} Amps | Maximum Current: {this.MaxCurrent} Amps | Minimum Current: {this.MinCurrent}";
    }
  }

  public override object PlotValue => (object) this.AmpHours;

  public static string SpecialExportValue(MonnitApplicationBase app)
  {
    return $"\"{((Current) app).AmpHours}\",\"{((Current) app).AvgCurrent}\",\"{((Current) app).MaxCurrent}\",\"{((Current) app).MinCurrent}\"";
  }

  public static Current Deserialize(string version, string serialized)
  {
    Current current = new Current();
    if (string.IsNullOrEmpty(serialized))
    {
      current.AmpHours = 0.0;
      current.AvgCurrent = 0.0;
      current.MaxCurrent = 0.0;
      current.MinCurrent = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      current.AmpHours = strArray[0].ToDouble();
      if (strArray.Length == 4)
      {
        current.AvgCurrent = strArray[1].ToDouble();
        current.MaxCurrent = strArray[2].ToDouble();
        current.MinCurrent = strArray[3].ToDouble();
      }
      else
      {
        current.AvgCurrent = strArray[0].ToDouble();
        current.MaxCurrent = strArray[0].ToDouble();
        current.MinCurrent = strArray[0].ToDouble();
      }
    }
    return current;
  }

  public static Current Create(byte[] sdm, int startIndex)
  {
    return new Current()
    {
      AmpHours = BitConverter.ToUInt16(sdm, startIndex).ToDouble() / 100.0,
      AvgCurrent = BitConverter.ToUInt16(sdm, startIndex + 2).ToDouble() / 100.0,
      MaxCurrent = BitConverter.ToUInt16(sdm, startIndex + 4).ToDouble() / 100.0,
      MinCurrent = BitConverter.ToUInt16(sdm, startIndex + 6).ToDouble() / 100.0
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
      CompareValue = "0",
      AccountID = sensor.AccountID,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = Current.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    double result = 0.0;
    if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
    {
      if (double.TryParse(collection["Hysteresis_Manual"], out result))
      {
        double num = collection["Hysteresis_Manual"].ToDouble();
        Current.SetHystLower(sensor, Convert.ToUInt16(num * 100.0));
      }
      else
        sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    }
    else
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      if (double.TryParse(collection["MinimumThreshold_Manual"], out result))
      {
        double num = collection["MinimumThreshold_Manual"].ToDouble();
        sensor.MinimumThreshold = num >= (double) sensor.DefaultValue<long>("DefaultMinimumThreshold") ? (num <= (double) sensor.DefaultValue<long>("DefaultMaximumThreshold") ? (num * 100.0).ToLong() : sensor.DefaultValue<long>("DefaultMaximumThreshold")) : sensor.DefaultValue<long>("DefaultMinimumThreshold");
      }
      else
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    }
    else
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      if (double.TryParse(collection["MaximumThreshold_Manual"], out result))
      {
        double o = collection["MaximumThreshold_Manual"].ToDouble();
        sensor.MaximumThreshold = o >= (double) sensor.DefaultValue<long>("DefaultMinimumThreshold") ? (o <= (double) sensor.DefaultValue<long>("DefaultMaximumThreshold") ? o.ToLong() * 100L : sensor.DefaultValue<long>("DefaultMaximumThreshold")) : sensor.DefaultValue<long>("DefaultMinimumThreshold");
        if (sensor.MinimumThreshold > sensor.MaximumThreshold)
          sensor.MaximumThreshold = sensor.MinimumThreshold;
      }
      else
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    }
    else
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    int num1 = collection["ctSize"].ToInt();
    Current.SetCalVal3Upper(sensor, Convert.ToUInt16(num1));
    Current.SetCalVal3Lower(sensor, (collection["avgInterval"].ToDouble() * 1000.0).ToInt());
    sensor.Calibration4 = (collection["currentShift"].ToDouble() * 100.0).ToLong();
    if (string.IsNullOrEmpty(collection["BasicThreshold"]) || string.IsNullOrEmpty(collection["BasicThresholdDirection"]) || !(collection["BasicThresholdDirection"] != "-1"))
      return;
    if (collection["BasicThresholdDirection"].ToInt() == 0)
    {
      sensor.MinimumThreshold = (collection["BasicThreshold"].ToDouble() * 100.0).ToLong();
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    }
    else if (collection["BasicThresholdDirection"].ToInt() == 1)
    {
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      sensor.MaximumThreshold = (collection["BasicThreshold"].ToDouble() * 100.0).ToLong();
    }
    sensor.MeasurementsPerTransmission = 6;
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (collection["spanZero"].ToString().ToLower() == "zero")
    {
      Current.SetCalVal2Lower(sensor, (int) Convert.ToUInt16(collection["spanZeroTxt"].ToDouble() * 100.0));
      Current.SetHystUpper(sensor, (ushort) 3);
    }
    else
    {
      double num = collection["spanZeroTxt"].ToDouble() * 100.0;
      Current.SetCalVal2Upper(sensor, Convert.ToUInt16(num));
      Current.SetHystUpper(sensor, (ushort) 4);
    }
    sensor.ProfileConfigDirty = false;
    sensor.ProfileConfig2Dirty = false;
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
    target.Calibration3 = source.Calibration3;
    target.Calibration4 = source.Calibration4;
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
      if (Current.GetHystUpper(sensor) == (ushort) 4)
      {
        numArrayList.Add(CurrentBase.CalibrateFrame(sensor.SensorID, (int) Current.GetCalVal2Upper(sensor), 4));
        numArrayList.Add(sensor.ReadProfileConfig(29));
      }
      else
      {
        numArrayList.Add(CurrentBase.CalibrateFrame(sensor.SensorID, (int) Current.GetCalVal2Lower(sensor), 3));
        numArrayList.Add(sensor.ReadProfileConfig(29));
      }
    }
    return numArrayList;
  }

  public static void SetHystUpper(Sensor sensor, ushort value)
  {
    uint num = (uint) (Convert.ToInt32(sensor.Hysteresis) & (int) ushort.MaxValue);
    sensor.Hysteresis = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static ushort GetHystUpper(Sensor sensor)
  {
    return Convert.ToUInt16(sensor.Hysteresis >> 16 /*0x10*/);
  }

  public static void SetHystLower(Sensor sensor, ushort value)
  {
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.Calibration2) & 4294901760UL);
    sensor.Hysteresis = (long) (num | (uint) value);
  }

  public static ushort GetHystLower(Sensor sensor)
  {
    return (ushort) ((ulong) sensor.Hysteresis & (ulong) ushort.MaxValue);
  }

  public static void SetCalVal2Upper(Sensor sensor, ushort value)
  {
    uint num = (uint) (Convert.ToInt32(sensor.Calibration2) & (int) ushort.MaxValue);
    sensor.Calibration2 = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static ushort GetCalVal2Upper(Sensor sensor)
  {
    return Convert.ToUInt16(sensor.Calibration2 >> 16 /*0x10*/);
  }

  public static void SetCalVal2Lower(Sensor sensor, int value)
  {
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.Calibration2) & 4294901760UL);
    sensor.Calibration2 = (long) (num | (uint) value);
  }

  public static ushort GetCalVal2Lower(Sensor sensor)
  {
    return (ushort) ((ulong) sensor.Calibration2 & (ulong) ushort.MaxValue);
  }

  public static void SetCalVal3Upper(Sensor sensor, ushort value)
  {
    uint num = (uint) (Convert.ToInt32(sensor.Calibration3) & (int) ushort.MaxValue);
    sensor.Calibration3 = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static ushort GetCalVal3Upper(Sensor sensor)
  {
    return Convert.ToUInt16(sensor.Calibration3 >> 16 /*0x10*/);
  }

  public static void SetCalVal3Lower(Sensor sensor, int value)
  {
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.Calibration3) & 4294901760UL);
    sensor.Calibration3 = (long) (num | (uint) value);
  }

  public static ushort GetCalVal3Lower(Sensor sensor)
  {
    return (ushort) ((ulong) sensor.Calibration3 & (ulong) ushort.MaxValue);
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : ((double) Current.GetHystLower(sensor) / 100.0).ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return (sensor.MinimumThreshold.ToDouble() / 100.0).ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return (sensor.MaximumThreshold.ToDouble() / 100.0).ToString();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Current left, Current right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Current left, Current right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Current left, Current right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Current left, Current right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Current left, Current right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Current left, Current right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Current && this.Equals((MonnitApplicationBase) (obj as Current));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Current && this.AvgCurrent == (right as Current).AvgCurrent;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Current && this.AvgCurrent != (right as Current).AvgCurrent;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Current && this.AvgCurrent < (right as Current).AvgCurrent;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Current && this.AvgCurrent <= (right as Current).AvgCurrent;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Current && this.AvgCurrent > (right as Current).AvgCurrent;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Current && this.AvgCurrent >= (right as Current).AvgCurrent;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 20000;

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
  }

  public static string GetLabel(long sensorID) => "";
}
