// Decompiled with JetBrains decompiler
// Type: Monnit.CurrentZeroToTwentyAmp
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

public class CurrentZeroToTwentyAmp : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _Label;
  private SensorAttribute _VoltValue;
  private SensorAttribute _ShowFullDataValue;

  public static long MonnitApplicationID => CurrentZeroToTwentyAmpBase.MonnitApplicationID;

  public static string ApplicationName => "Current Meter - 20A";

  public static eApplicationProfileType ProfileType => CurrentBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => CurrentZeroToTwentyAmp.MonnitApplicationID;

  public double AmpHours { get; set; }

  public double AvgCurrent { get; set; }

  public double MaxCurrent { get; set; }

  public double MinCurrent { get; set; }

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
    return new List<object>((IEnumerable<object>) new object[4]
    {
      this.PlotValue,
      (object) this.AvgCurrent,
      (object) this.MaxCurrent,
      (object) this.MinCurrent
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[4]
    {
      CurrentZeroToTwentyAmp.GetLabel(sensorID),
      "AvgCurrent",
      "MaxCurrent",
      "MinCurrent"
    });
  }

  public new static NotificationScaleInfoModel GetScalingInfo(Sensor sens)
  {
    NotificationScaleInfoModel scalingInfo = new NotificationScaleInfoModel();
    CurrentZeroToTwentyAmp currentZeroToTwentyAmp = new CurrentZeroToTwentyAmp();
    currentZeroToTwentyAmp.SetSensorAttributes(sens.SensorID);
    scalingInfo.Label = currentZeroToTwentyAmp.Label.Value.ToStringSafe();
    scalingInfo.profileLow = 0.0;
    scalingInfo.profileHigh = !(currentZeroToTwentyAmp.Label.Value == "Wh") ? (!(currentZeroToTwentyAmp.Label.Value == "kWh") ? 20.0 : currentZeroToTwentyAmp.VoltValue.Value.ToDouble() * 20.0 / 1000.0) : currentZeroToTwentyAmp.VoltValue.Value.ToDouble() * 20.0;
    scalingInfo.baseLow = 0.0;
    scalingInfo.baseHigh = 20.0;
    return scalingInfo;
  }

  public SensorAttribute Label
  {
    get
    {
      if (this._Label == null)
        this._Label = new SensorAttribute() { Value = "Ah" };
      return this._Label;
    }
  }

  public SensorAttribute VoltValue
  {
    get
    {
      if (this._VoltValue == null)
        this._VoltValue = new SensorAttribute()
        {
          Value = "0"
        };
      return this._VoltValue;
    }
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        this._Label = sensorAttribute;
      if (sensorAttribute.Name == "VoltValue")
        this._VoltValue = sensorAttribute;
      if (sensorAttribute.Name == "ShowFullDataValue")
        this._ShowFullDataValue = sensorAttribute;
    }
  }

  public SensorAttribute ShowFullDataValue
  {
    get
    {
      if (this._ShowFullDataValue == null)
        this._ShowFullDataValue = new SensorAttribute()
        {
          Value = "false"
        };
      return this._ShowFullDataValue;
    }
  }

  public static void SetShowFullDataValue(long sensorID, bool showFullData)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ShowFullDataValue")
      {
        sensorAttribute.Value = showFullData.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "ShowFullDataValue",
        Value = showFullData.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static bool GetShowFullDataValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ShowFullDataValue")
        return sensorAttribute.Value.ToBool();
    }
    return false;
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

  public static string GetLabel(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        return sensorAttribute.Value;
    }
    return "Amp Hours";
  }

  public static void SetVoltValue(long sensorID, double lowValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "VoltValue")
      {
        sensorAttribute.Value = lowValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "VoltValue",
        Value = lowValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static double GetVoltValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "VoltValue")
        return sensorAttribute.Value.ToDouble();
    }
    return 0.0;
  }

  public override string Serialize()
  {
    return $"{this.AmpHours.ToString()},{this.AvgCurrent.ToString()},{this.MaxCurrent.ToString()},{this.MinCurrent.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) CurrentZeroToTwentyAmp.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      if (!(this.ShowFullDataValue.Value.ToLower() == "true"))
        return $"{this.PlotValue.ToDouble()} {this.Label.Value}";
      return string.Format("{0} {4}, Average Current: {1} Amps , Maximum Current: {2} Amps , Minimum Current: {3} Amps", (object) this.PlotValue.ToDouble(), (object) this.AvgCurrent, (object) this.MaxCurrent, (object) this.MinCurrent, (object) this.Label.Value);
    }
  }

  public override object PlotValue
  {
    get
    {
      if (this.Label.Value == "Wh")
        return (object) (this.AmpHours * this.VoltValue.Value.ToDouble());
      return this.Label.Value == "kWh" ? (object) (this.AmpHours * this.VoltValue.Value.ToDouble() / 1000.0) : (object) this.AmpHours;
    }
  }

  public static string SpecialExportValue(MonnitApplicationBase app)
  {
    return $"\"{((CurrentZeroToTwentyAmp) app).AmpHours}\",\"{((CurrentZeroToTwentyAmp) app).AvgCurrent}\",\"{((CurrentZeroToTwentyAmp) app).MaxCurrent}\",\"{((CurrentZeroToTwentyAmp) app).MinCurrent}\"";
  }

  public static CurrentZeroToTwentyAmp Deserialize(string version, string serialized)
  {
    CurrentZeroToTwentyAmp currentZeroToTwentyAmp = new CurrentZeroToTwentyAmp();
    if (string.IsNullOrEmpty(serialized))
    {
      currentZeroToTwentyAmp.AmpHours = 0.0;
      currentZeroToTwentyAmp.AvgCurrent = 0.0;
      currentZeroToTwentyAmp.MaxCurrent = 0.0;
      currentZeroToTwentyAmp.MinCurrent = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      currentZeroToTwentyAmp.AmpHours = strArray[0].ToDouble();
      if (strArray.Length == 4)
      {
        currentZeroToTwentyAmp.AvgCurrent = strArray[1].ToDouble();
        currentZeroToTwentyAmp.MaxCurrent = strArray[2].ToDouble();
        currentZeroToTwentyAmp.MinCurrent = strArray[3].ToDouble();
      }
      else
      {
        currentZeroToTwentyAmp.AvgCurrent = strArray[0].ToDouble();
        currentZeroToTwentyAmp.MaxCurrent = strArray[0].ToDouble();
        currentZeroToTwentyAmp.MinCurrent = strArray[0].ToDouble();
      }
    }
    return currentZeroToTwentyAmp;
  }

  public static CurrentZeroToTwentyAmp Create(byte[] sdm, int startIndex)
  {
    return new CurrentZeroToTwentyAmp()
    {
      AmpHours = BitConverter.ToUInt64(sdm, startIndex).ToDouble() / 100.0,
      AvgCurrent = BitConverter.ToUInt16(sdm, startIndex + 8).ToDouble() / 100.0,
      MaxCurrent = BitConverter.ToUInt16(sdm, startIndex + 10).ToDouble() / 100.0,
      MinCurrent = BitConverter.ToUInt16(sdm, startIndex + 12).ToDouble() / 100.0
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
      ApplicationID = CurrentZeroToTwentyAmp.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["voltValue"]))
      CurrentZeroToTwentyAmp.SetVoltValue(sensor.SensorID, collection["voltValue"].ToDouble());
    if (string.IsNullOrEmpty(collection["label"]))
      return;
    CurrentZeroToTwentyAmp.SetLabel(sensor.SensorID, collection["label"]);
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["BasicThreshold"]) && !string.IsNullOrEmpty(collection["BasicThresholdDirection"]) && collection["BasicThresholdDirection"] != "-1")
    {
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
    else
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
          if (result < 0.0)
            CurrentZeroToTwentyAmp.SetHystLower(sensor, (ushort) 0);
          else if (result > 5.0)
            CurrentZeroToTwentyAmp.SetHystLower(sensor, Convert.ToUInt16(500));
          else
            CurrentZeroToTwentyAmp.SetHystLower(sensor, Convert.ToUInt16(result * 100.0));
        }
        else
          sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
      }
      if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
      {
        if (double.TryParse(collection["MinimumThreshold_Manual"], out result))
        {
          double num = collection["MinimumThreshold_Manual"].ToDouble();
          if (num < 0.0)
            num = 0.0;
          if (num > 30.0)
            num = 30.0;
          sensor.MinimumThreshold = (num * 100.0).ToLong();
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
          double num1 = collection["MaximumThreshold_Manual"].ToDouble();
          double num2 = (double) sensor.MinimumThreshold / 100.0;
          if (num1 < 0.0)
            num1 = 0.0;
          if (num1 > 30.0)
            num1 = 30.0;
          if (num1 < num2)
            num1 = num2;
          sensor.MaximumThreshold = (num1 * 100.0).ToLong();
        }
        else
          sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      }
      if (!string.IsNullOrEmpty(collection["avgInterval"]))
      {
        double o = collection["avgInterval"].ToDouble();
        if (o < 1.0)
          o = 1.0;
        else if (o > 30.0)
          o = 30.0;
        sensor.Calibration3 = !(new Version(sensor.FirmwareVersion) >= new Version("14.26.17.10")) ? (o * 1000.0).ToLong() : o.ToLong();
      }
      if (!string.IsNullOrEmpty(collection["currentShift"]))
      {
        double num = collection["currentShift"].ToDouble();
        sensor.Calibration4 = num >= 0.0 ? (num <= (double) ushort.MaxValue ? (collection["currentShift"].ToDouble() * 100.0).ToLong() : 6553500L) : 0L;
      }
      if (!string.IsNullOrEmpty(collection["Offset_Hidden"]))
        CurrentZeroToTwentyAmp.SetHystThirdByte(sensor, Convert.ToSByte(collection["Offset_Hidden"].ToDouble() * 100.0));
      if (!string.IsNullOrEmpty(collection["Accum"]))
        CurrentZeroToTwentyAmp.SetHystFourthByte(sensor, Convert.ToByte(collection["Accum"]));
      if (!string.IsNullOrEmpty(collection["FullNotiString"]))
      {
        bool showFullData = collection["FullNotiString"].ToString() == "1" || collection["FullNotiString"].ToString().ToLower() == "true";
        CurrentZeroToTwentyAmp.SetShowFullDataValue(sensor.SensorID, showFullData);
      }
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    NonCachedAttribute nonCachedAttribute1 = NonCachedAttribute.LoadBySensorIDAndName(sensor.SensorID, "Calibration");
    if (nonCachedAttribute1 == null)
    {
      nonCachedAttribute1 = new NonCachedAttribute();
      nonCachedAttribute1.SensorID = sensor.SensorID;
      nonCachedAttribute1.Name = "Calibration";
    }
    nonCachedAttribute1.Value1 = string.IsNullOrWhiteSpace(collection["acc"]) ? "3" : collection["acc"];
    double num;
    if (!string.IsNullOrWhiteSpace(collection["observed"]))
    {
      NonCachedAttribute nonCachedAttribute2 = nonCachedAttribute1;
      num = collection["observed"].ToDouble() * 100.0;
      string str = num.ToString();
      nonCachedAttribute2.Value2 = str;
    }
    else
      nonCachedAttribute1.Value2 = "-1";
    if (!string.IsNullOrWhiteSpace(collection["actual"]))
    {
      NonCachedAttribute nonCachedAttribute3 = nonCachedAttribute1;
      num = collection["actual"].ToDouble() * 100.0;
      string str = num.ToString();
      nonCachedAttribute3.Value3 = str;
    }
    else
      nonCachedAttribute1.Value3 = "-1";
    nonCachedAttribute1.Save();
    sensor.ProfileConfig2Dirty = false;
    sensor.ProfileConfigDirty = false;
    sensor.PendingActionControlCommand = true;
    if (!string.IsNullOrWhiteSpace(collection["queueID"]))
      SensorAttribute.SetQueueID(sensor.SensorID, collection["queueID"].ToInt());
    else
      SensorAttribute.SetQueueID(sensor.SensorID);
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
    uint num1 = Convert.ToUInt32(source.Hysteresis) & 4278255615U;
    uint num2 = Convert.ToUInt32(target.Hysteresis) & 16711680U /*0xFF0000*/;
    target.Hysteresis = (long) (num1 | num2);
    target.MinimumThreshold = source.MinimumThreshold;
    target.MaximumThreshold = source.MaximumThreshold;
    target.Calibration1 = source.Calibration1;
    target.Calibration2 = source.Calibration2;
    target.Calibration3 = source.Calibration3;
    target.Calibration4 = source.Calibration4;
    target.EventDetectionType = source.EventDetectionType;
    target.EventDetectionPeriod = source.EventDetectionPeriod;
    target.EventDetectionCount = source.EventDetectionCount;
    target.RearmTime = source.RearmTime;
    target.BiStable = source.BiStable;
    target.TagString = source.TagString;
  }

  public static void UseSerializedRecipientProperties(
    Sensor sens,
    int queueID,
    string serializedRecipientProperties)
  {
    CurrentZeroToTwentyAmp.CalibrateSensor(sens, new NameValueCollection()
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
    List<byte[]> numArrayList = new List<byte[]>();
    NonCachedAttribute nonCachedAttribute = NonCachedAttribute.LoadBySensorIDAndName(sensor.SensorID, "Calibration");
    int acc = nonCachedAttribute.Value1.ToInt();
    int oldVal = nonCachedAttribute.Value2.ToInt();
    int newVal = nonCachedAttribute.Value3.ToInt();
    if (sensor.CableID < 0L)
    {
      numArrayList.Add(CurrentZeroToTwentyAmpBase.CalibrateFrame(sensor.SensorID, acc, oldVal, newVal));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    else
    {
      numArrayList.Add(CurrentZeroToTwentyAmpBase.CalibrateFrame(sensor.SensorID, acc, oldVal, newVal, SensorAttribute.GetQueueID(sensor.SensorID), new long?(sensor.CableID)));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public static void SetHystFourthByte(Sensor sensor, byte value)
  {
    uint num = (uint) (Convert.ToInt32(sensor.Hysteresis) & 16777215 /*0xFFFFFF*/);
    sensor.Hysteresis = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 24));
  }

  public static byte GetHystFourthByte(Sensor sensor)
  {
    return (byte) Convert.ToUInt16(Convert.ToUInt32(sensor.Hysteresis & 4278190080L /*0xFF000000*/) >> 24);
  }

  public static void SetHystThirdByte(Sensor sensor, sbyte value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4278255615U;
    sensor.Hysteresis = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static byte GetHystThirdByte(Sensor sensor)
  {
    return (byte) Convert.ToUInt16(Convert.ToUInt32(sensor.Hysteresis & 16711680L /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetHystLower(Sensor sensor, ushort value)
  {
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.Hysteresis) & 4294901760UL);
    sensor.Hysteresis = (long) (num | (uint) value);
  }

  public static ushort GetHystLower(Sensor sensor)
  {
    return (ushort) ((ulong) sensor.Hysteresis & (ulong) ushort.MaxValue);
  }

  public static void SetCalVal1Upper(Sensor sensor, ushort value)
  {
    uint num = (uint) (Convert.ToInt32(sensor.Calibration1) & (int) ushort.MaxValue);
    sensor.Calibration1 = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static int GetCalVal1Upper(Sensor sensor)
  {
    return Convert.ToInt32(sensor.Calibration1 >> 16 /*0x10*/);
  }

  public static void SetCalVal1Lower(Sensor sensor, ushort value)
  {
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.Calibration1) & 4294901760UL);
    sensor.Calibration1 = (long) (num | (uint) value);
  }

  public static int GetCalVal1Lower(Sensor sensor)
  {
    return (int) (sensor.Calibration1 & (long) ushort.MaxValue);
  }

  public static void SetCalVal3FourthByte(Sensor sensor, sbyte value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration3) & 16777215U /*0xFFFFFF*/;
    sensor.Calibration1 = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 24));
  }

  public static sbyte GetCalVal3FourthByte(Sensor sensor)
  {
    return (sbyte) Convert.ToByte((Convert.ToUInt32(sensor.Calibration3) & 4278190080U /*0xFF000000*/) >> 24);
  }

  public static void SetCalVal3ThirdByte(Sensor sensor, sbyte value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration3) & 4278255615U;
    sensor.Calibration1 = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static sbyte GetCalVal3ThirdByte(Sensor sensor)
  {
    return (sbyte) Convert.ToByte((Convert.ToUInt32(sensor.Calibration3) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
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
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : ((double) CurrentZeroToTwentyAmp.GetHystLower(sensor) / 100.0).ToString();
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

  public static bool operator ==(CurrentZeroToTwentyAmp left, CurrentZeroToTwentyAmp right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(CurrentZeroToTwentyAmp left, CurrentZeroToTwentyAmp right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(CurrentZeroToTwentyAmp left, CurrentZeroToTwentyAmp right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(CurrentZeroToTwentyAmp left, CurrentZeroToTwentyAmp right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(CurrentZeroToTwentyAmp left, CurrentZeroToTwentyAmp right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(CurrentZeroToTwentyAmp left, CurrentZeroToTwentyAmp right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is CurrentZeroToTwentyAmp && this.Equals((MonnitApplicationBase) (obj as CurrentZeroToTwentyAmp));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is CurrentZeroToTwentyAmp && this.AvgCurrent == (right as CurrentZeroToTwentyAmp).AvgCurrent;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is CurrentZeroToTwentyAmp && this.AvgCurrent != (right as CurrentZeroToTwentyAmp).AvgCurrent;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is CurrentZeroToTwentyAmp && this.AvgCurrent < (right as CurrentZeroToTwentyAmp).AvgCurrent;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is CurrentZeroToTwentyAmp && this.AvgCurrent <= (right as CurrentZeroToTwentyAmp).AvgCurrent;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is CurrentZeroToTwentyAmp && this.AvgCurrent > (right as CurrentZeroToTwentyAmp).AvgCurrent;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is CurrentZeroToTwentyAmp && this.AvgCurrent >= (right as CurrentZeroToTwentyAmp).AvgCurrent;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 2000;

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
  }
}
