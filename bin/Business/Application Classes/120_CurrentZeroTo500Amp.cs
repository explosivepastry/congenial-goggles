// Decompiled with JetBrains decompiler
// Type: Monnit.CurrentZeroTo500Amp
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

public class CurrentZeroTo500Amp : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _Label;
  private SensorAttribute _VoltValue;
  private SensorAttribute _ShowFullDataValue;

  public static long MonnitApplicationID => CurrentZeroTo500AmpBase.MonnitApplicationID;

  public static string ApplicationName => "Current Meter - 500A";

  public static eApplicationProfileType ProfileType => CurrentBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => CurrentZeroTo500Amp.MonnitApplicationID;

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
      CurrentZeroTo500Amp.GetLabel(sensorID),
      "AvgCurrent",
      "MaxCurrent",
      "MinCurrent"
    });
  }

  public new static NotificationScaleInfoModel GetScalingInfo(Sensor sens)
  {
    NotificationScaleInfoModel scalingInfo = new NotificationScaleInfoModel();
    CurrentZeroTo500Amp currentZeroTo500Amp = new CurrentZeroTo500Amp();
    currentZeroTo500Amp.SetSensorAttributes(sens.SensorID);
    scalingInfo.Label = currentZeroTo500Amp.Label.Value.ToStringSafe();
    scalingInfo.profileLow = 0.0;
    scalingInfo.profileHigh = !(currentZeroTo500Amp.Label.Value == "Wh") ? (!(currentZeroTo500Amp.Label.Value == "kWh") ? 500.0 : currentZeroTo500Amp.VoltValue.Value.ToDouble() * 500.0 / 1000.0) : currentZeroTo500Amp.VoltValue.Value.ToDouble() * 500.0;
    scalingInfo.baseLow = 0.0;
    scalingInfo.baseHigh = 500.0;
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

  public SensorAttribute ShowFullDataValue
  {
    get
    {
      if (this._ShowFullDataValue == null)
        this._ShowFullDataValue = new SensorAttribute()
        {
          Name = nameof (ShowFullDataValue),
          Value = "false"
        };
      return this._ShowFullDataValue;
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

  public override string Serialize()
  {
    return $"{this.AmpHours.ToString()},{this.AvgCurrent.ToString()},{this.MaxCurrent.ToString()},{this.MinCurrent.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) CurrentZeroTo500Amp.Deserialize(version, serialized);
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
    return $"\"{((CurrentZeroTo500Amp) app).AmpHours}\",\"{((CurrentZeroTo500Amp) app).AvgCurrent}\",\"{((CurrentZeroTo500Amp) app).MaxCurrent}\",\"{((CurrentZeroTo500Amp) app).MinCurrent}\"";
  }

  public static CurrentZeroTo500Amp Deserialize(string version, string serialized)
  {
    CurrentZeroTo500Amp currentZeroTo500Amp = new CurrentZeroTo500Amp();
    if (string.IsNullOrEmpty(serialized))
    {
      currentZeroTo500Amp.AmpHours = 0.0;
      currentZeroTo500Amp.AvgCurrent = 0.0;
      currentZeroTo500Amp.MaxCurrent = 0.0;
      currentZeroTo500Amp.MinCurrent = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      currentZeroTo500Amp.AmpHours = strArray[0].ToDouble();
      if (strArray.Length == 4)
      {
        currentZeroTo500Amp.AvgCurrent = strArray[1].ToDouble();
        currentZeroTo500Amp.MaxCurrent = strArray[2].ToDouble();
        currentZeroTo500Amp.MinCurrent = strArray[3].ToDouble();
      }
      else
      {
        currentZeroTo500Amp.AvgCurrent = strArray[0].ToDouble();
        currentZeroTo500Amp.MaxCurrent = strArray[0].ToDouble();
        currentZeroTo500Amp.MinCurrent = strArray[0].ToDouble();
      }
    }
    return currentZeroTo500Amp;
  }

  public static CurrentZeroTo500Amp Create(byte[] sdm, int startIndex)
  {
    return new CurrentZeroTo500Amp()
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
      ApplicationID = CurrentZeroTo500Amp.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["voltValue"]))
      CurrentZeroTo500Amp.SetVoltValue(sensor.SensorID, collection["voltValue"].ToDouble());
    if (string.IsNullOrEmpty(collection["label"]))
      return;
    CurrentZeroTo500Amp.SetLabel(sensor.SensorID, collection["label"]);
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
      if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual") || sensor.Hysteresis == (long) uint.MaxValue)
        sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
      if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual") || sensor.MinimumThreshold == (long) uint.MaxValue)
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual") || sensor.MaximumThreshold == (long) uint.MaxValue)
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) && !string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
      {
        double num1 = collection["MinimumThreshold_Manual"].ToDouble();
        double num2 = collection["MaximumThreshold_Manual"].ToDouble();
        if (num1 < 0.0)
          num1 = 0.0;
        if (num1 > 600.0)
          num1 = 600.0;
        if (num1 >= num2)
          num1 = num2 - 1.0;
        sensor.MinimumThreshold = (num1 * 100.0).ToLong();
        if (num2 < 0.0)
          num2 = 0.0;
        if (num2 > 600.0)
          num2 = 600.0;
        if (num2 <= num1)
          num2 = num1 + 1.0;
        sensor.MaximumThreshold = (num2 * 100.0).ToLong();
      }
      if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
      {
        double num3 = collection["Hysteresis_Manual"].ToDouble();
        double num4 = ((double) sensor.MaximumThreshold / 100.0 - (double) sensor.MinimumThreshold / 100.0) * 0.25;
        if (num3 < 0.0)
          num3 = 0.0;
        if (num3 > num4)
          num3 = num4;
        CurrentZeroTo500Amp.SetHystLower(sensor, (num3 * 100.0).ToInt());
      }
      if (!string.IsNullOrEmpty(collection["avgInterval"]))
      {
        double o = collection["avgInterval"].ToDouble();
        if (o < 1.0)
          o = 1.0;
        else if (o > 30.0)
          o = 30.0;
        sensor.Calibration3 = !(new Version(sensor.FirmwareVersion) >= new Version("14.26.17.6")) ? (o * 1000.0).ToLong() : o.ToLong();
      }
      if (!string.IsNullOrEmpty(collection["currentShift"]))
      {
        double num = collection["currentShift"].ToDouble();
        sensor.Calibration4 = num >= 0.0 ? (num <= (double) ushort.MaxValue ? (collection["currentShift"].ToDouble() * 100.0).ToLong() : 6553500L) : 0L;
      }
      if (!string.IsNullOrEmpty(collection["Offset_Hidden"]))
      {
        CurrentZeroTo500Amp.SetHystThirdByte(sensor, Convert.ToSByte(collection["Offset_Hidden"].ToDouble() * 100.0));
        CurrentZeroTo500Amp.SetHystFourthByte(sensor, Convert.ToByte(collection["Accum"]));
      }
      if (!string.IsNullOrEmpty(collection["FullNotiString"]))
      {
        bool showFullData = collection["FullNotiString"].ToString() == "1" || collection["FullNotiString"].ToString().ToLower() == "true";
        CurrentZeroTo500Amp.SetShowFullDataValue(sensor.SensorID, showFullData);
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
    CurrentZeroTo500Amp.CalibrateSensor(sens, new NameValueCollection()
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
      numArrayList.Add(CurrentZeroTo500AmpBase.CalibrateFrame(sensor.SensorID, acc, oldVal, newVal));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    else
    {
      numArrayList.Add(CurrentZeroTo500AmpBase.CalibrateFrame(sensor.SensorID, acc, oldVal, newVal, SensorAttribute.GetQueueID(sensor.SensorID), new long?(sensor.CableID)));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
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

  public static void SetHystLower(Sensor sensor, int value)
  {
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.Hysteresis) & 4294901760UL);
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
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : ((double) CurrentZeroTo500Amp.GetHystLower(sensor) / 100.0).ToString();
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

  public static bool operator ==(CurrentZeroTo500Amp left, CurrentZeroTo500Amp right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(CurrentZeroTo500Amp left, CurrentZeroTo500Amp right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(CurrentZeroTo500Amp left, CurrentZeroTo500Amp right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(CurrentZeroTo500Amp left, CurrentZeroTo500Amp right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(CurrentZeroTo500Amp left, CurrentZeroTo500Amp right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(CurrentZeroTo500Amp left, CurrentZeroTo500Amp right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is CurrentZeroTo500Amp && this.Equals((MonnitApplicationBase) (obj as CurrentZeroTo500Amp));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is CurrentZeroTo500Amp && this.AvgCurrent == (right as CurrentZeroTo500Amp).AvgCurrent;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is CurrentZeroTo500Amp && this.AvgCurrent != (right as CurrentZeroTo500Amp).AvgCurrent;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is CurrentZeroTo500Amp && this.AvgCurrent < (right as CurrentZeroTo500Amp).AvgCurrent;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is CurrentZeroTo500Amp && this.AvgCurrent <= (right as CurrentZeroTo500Amp).AvgCurrent;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is CurrentZeroTo500Amp && this.AvgCurrent > (right as CurrentZeroTo500Amp).AvgCurrent;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is CurrentZeroTo500Amp && this.AvgCurrent >= (right as CurrentZeroTo500Amp).AvgCurrent;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 50000;

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
  }
}
