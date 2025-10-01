// Decompiled with JetBrains decompiler
// Type: Monnit.TwoInputPulseCounter
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class TwoInputPulseCounter : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _Channel1TransformValue;
  private SensorAttribute _Channel2TransformValue;
  private SensorAttribute _Channel1Label;
  private SensorAttribute _Channel2Label;

  public static long MonnitApplicationID => 153;

  public static string ApplicationName => "Two Input Pulse Counter";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => TwoInputPulseCounter.MonnitApplicationID;

  public ulong Channel1 { get; set; }

  public ulong Channel2 { get; set; }

  public override string Serialize()
  {
    ulong num = this.Channel1;
    string str1 = num.ToString();
    num = this.Channel2;
    string str2 = num.ToString();
    return $"{str1}|{str2}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.Count, "Channel1", (double) this.Channel1),
        new AppDatum(eDatumType.Count, "Channel2", (double) this.Channel2)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[2]
    {
      this.PlotValue,
      (object) this.PlotValue_Channel2
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[2]
    {
      TwoInputPulseCounter.GetLabel_Channel1(sensorID),
      TwoInputPulseCounter.GetLabel_Channel2(sensorID)
    });
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) TwoInputPulseCounter.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      return $"Channel 1: {this.PlotValue} {this.Channel1Label.Value}, Channel 2: {this.PlotValue_Channel2} {this.Channel2Label.Value}";
    }
  }

  public override object PlotValue
  {
    get
    {
      return this.Channel1TransformValue != null && this.Channel1TransformValue.Value != "0" && this.Channel1TransformValue.Value != "1" ? (object) ((double) this.Channel1 * this.Channel1TransformValue.Value.ToDouble()) : (object) this.Channel1;
    }
  }

  public double PlotValue_Channel2
  {
    get
    {
      return this.Channel2TransformValue != null && this.Channel2TransformValue.Value != "0" && this.Channel2TransformValue.Value != "1" ? (double) this.Channel2 * this.Channel2TransformValue.Value.ToDouble() : (double) this.Channel2;
    }
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Channel1TransformValue")
        this._Channel1TransformValue = sensorAttribute;
      if (sensorAttribute.Name == "Channel2TransformValue")
        this._Channel2TransformValue = sensorAttribute;
      if (sensorAttribute.Name == "Channel1Label")
        this._Channel1Label = sensorAttribute;
      if (sensorAttribute.Name == "Channel2Label")
        this._Channel2Label = sensorAttribute;
    }
  }

  public SensorAttribute Channel1TransformValue
  {
    get
    {
      if (this._Channel1TransformValue == null)
        this._Channel1TransformValue = new SensorAttribute()
        {
          Value = "1"
        };
      return this._Channel1TransformValue;
    }
  }

  public SensorAttribute Channel2TransformValue
  {
    get
    {
      if (this._Channel2TransformValue == null)
        this._Channel2TransformValue = new SensorAttribute()
        {
          Value = "1"
        };
      return this._Channel2TransformValue;
    }
  }

  public SensorAttribute Channel1Label
  {
    get
    {
      if (this._Channel1Label == null)
        this._Channel1Label = new SensorAttribute()
        {
          Value = "Pulses",
          Name = nameof (Channel1Label)
        };
      return this._Channel1Label;
    }
  }

  public SensorAttribute Channel2Label
  {
    get
    {
      if (this._Channel2Label == null)
        this._Channel2Label = new SensorAttribute()
        {
          Value = "Pulses",
          Name = nameof (Channel2Label)
        };
      return this._Channel2Label;
    }
  }

  public static double GetTransform_Channel1(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Channel1TransformValue")
        return sensorAttribute.Value.ToDouble();
    }
    return 1.0;
  }

  public static void SetTransform_Channel1(long sensorID, double transformValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Channel1TransformValue")
      {
        sensorAttribute.Value = transformValue > 0.0 ? transformValue.ToString() : "1";
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "Channel1TransformValue",
        Value = (transformValue > 0.0 ? transformValue.ToString() : "1"),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static double GetTransform_Channel2(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Channel2TransformValue")
        return sensorAttribute.Value.ToDouble();
    }
    return 1.0;
  }

  public static void SetTransform_Channel2(long sensorID, double transformValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Channel2TransformValue")
      {
        sensorAttribute.Value = transformValue > 0.0 ? transformValue.ToString() : "1";
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "Channel2TransformValue",
        Value = (transformValue > 0.0 ? transformValue.ToString() : "1"),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetLabel_Channel1(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Channel1Label")
        return sensorAttribute.Value;
    }
    return "pulses";
  }

  public static void SetLabel_Channel1(long sensorID, string label)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Channel1Label")
      {
        sensorAttribute.Value = label;
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "Channel1Label",
        Value = label,
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetLabel_Channel2(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Channel2Label")
        return sensorAttribute.Value;
    }
    return "pulses";
  }

  public static void SetLabel_Channel2(long sensorID, string label)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Channel2Label")
      {
        sensorAttribute.Value = label;
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "Channel2Label",
        Value = label,
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string SpecialExportValue(MonnitApplicationBase app)
  {
    return $"\"{((TwoInputPulseCounter) app).Channel1}\",\"{((TwoInputPulseCounter) app).Channel2}\"";
  }

  public static TwoInputPulseCounter Deserialize(string version, string serialized)
  {
    TwoInputPulseCounter inputPulseCounter = new TwoInputPulseCounter();
    if (string.IsNullOrEmpty(serialized))
    {
      inputPulseCounter.Channel1 = 0UL;
      inputPulseCounter.Channel2 = 0UL;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      inputPulseCounter.Channel1 = strArray[0].ToUInt64();
      inputPulseCounter.Channel2 = strArray.Length != 2 ? strArray[0].ToUInt64() : strArray[1].ToUInt64();
    }
    return inputPulseCounter;
  }

  public static TwoInputPulseCounter Create(byte[] sdm, int startIndex)
  {
    return new TwoInputPulseCounter()
    {
      Channel1 = BitConverter.ToUInt64(sdm, startIndex),
      Channel2 = BitConverter.ToUInt64(sdm, startIndex + 8)
    };
  }

  public new static void DefaultConfigurationSettings(Sensor sensor)
  {
    TwoInputPulseCounterBase.GetDefaults(new Version(sensor.FirmwareVersion), sensor.GenerationType);
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
    if (!string.IsNullOrEmpty(collection["label_Channel1"]))
      TwoInputPulseCounter.SetLabel_Channel1(sensor.SensorID, collection["label_Channel1"]);
    if (!string.IsNullOrEmpty(collection["label_Channel2"]))
      TwoInputPulseCounter.SetLabel_Channel2(sensor.SensorID, collection["label_Channel2"]);
    if (!string.IsNullOrEmpty(collection["transformValue_Channel1"]))
      TwoInputPulseCounter.SetTransform_Channel1(sensor.SensorID, collection["transformValue_Channel1"].ToDouble());
    if (string.IsNullOrEmpty(collection["transformValue_Channel2"]))
      return;
    TwoInputPulseCounter.SetTransform_Channel2(sensor.SensorID, collection["transformValue_Channel2"].ToDouble());
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = TwoInputPulseCounter.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["OverflowControl_channel1"]))
    {
      int num = collection["OverflowControl_channel1"].ToInt();
      if (num < 0)
        num = 0;
      if (num > 1)
        num = 1;
      TwoInputPulseCounter.SetOverflowControl_channel1(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["ReportImmediatelyOnOverflow_channel1"]))
    {
      int o = collection["ReportImmediatelyOnOverflow_channel1"].ToInt();
      if (o < 0)
        o = 0;
      if (o > 1)
        o = 1;
      TwoInputPulseCounter.SetReportImmediatelyOnOverflow_channel1(sensor, o.ToBool());
    }
    if (!string.IsNullOrEmpty(collection["GoAwareOnOverflow_channel1"]))
    {
      int o = collection["GoAwareOnOverflow_channel1"].ToInt();
      if (o < 0)
        o = 0;
      if (o > 1)
        o = 1;
      TwoInputPulseCounter.SetGoAwareOnOverflow_channel1(sensor, o.ToBool());
    }
    double num1 = TwoInputPulseCounter.GetTransform_Channel1(sensor.SensorID) <= 0.0 ? 1.0 : TwoInputPulseCounter.GetTransform_Channel1(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["overflowCount_channel1"]))
    {
      long num2 = (collection["overflowCount_channel1"].ToDouble() / num1).ToLong();
      if (num2 < 0L)
        num2 = 0L;
      if (num2 > (long) uint.MaxValue)
        num2 = (long) uint.MaxValue;
      sensor.MinimumThreshold = num2;
    }
    if (!string.IsNullOrEmpty(collection["Accumulate_channel1"]))
    {
      int num3 = collection["Accumulate_channel1"].ToInt();
      if (num3 < 0)
        num3 = 0;
      if (num3 > 1)
        num3 = 1;
      TwoInputPulseCounter.SetAccumulate_channel1(sensor, num3);
    }
    if (!string.IsNullOrEmpty(collection["TriggerType_channel1"]))
    {
      int num4 = collection["TriggerType_channel1"].ToInt();
      if (num4 < 0)
        num4 = 0;
      if (num4 > 2)
        num4 = 2;
      TwoInputPulseCounter.SetTriggerType_channel1(sensor, num4);
    }
    if (!string.IsNullOrEmpty(collection["FilterFrequency_channel1"]))
    {
      int num5 = collection["FilterFrequency_channel1"].ToInt();
      if (num5 < 0)
        num5 = 0;
      if (num5 > 2000)
        num5 = 2000;
      TwoInputPulseCounter.SetFilterFrequency_channel1(sensor, num5);
    }
    if (!string.IsNullOrEmpty(collection["ReArmTime_channel1"]) && !string.IsNullOrEmpty(collection["ActiveStateInterval"]))
    {
      int num6 = collection["ReArmTime_channel1"].ToInt();
      double o = collection["ActiveStateInterval"].ToDouble() * 60.0;
      if (num6 < 1)
        num6 = 1;
      if ((double) num6 > o)
        num6 = o.ToInt();
      TwoInputPulseCounter.SetReArmTime_channel1(sensor, num6);
    }
    if (!string.IsNullOrEmpty(collection["OverflowControl_channel2"]))
    {
      int num7 = collection["OverflowControl_channel2"].ToInt();
      if (num7 < 0)
        num7 = 0;
      if (num7 > 1)
        num7 = 1;
      TwoInputPulseCounter.SetOverflowControl_channel2(sensor, num7);
    }
    if (!string.IsNullOrEmpty(collection["ReportImmediatelyOnOverflow_channel2"]))
    {
      int o = collection["ReportImmediatelyOnOverflow_channel2"].ToInt();
      if (o < 0)
        o = 0;
      if (o > 1)
        o = 1;
      TwoInputPulseCounter.SetReportImmediatelyOnOverflow_channel2(sensor, o.ToBool());
    }
    if (!string.IsNullOrEmpty(collection["GoAwareOnOverflow_channel2"]))
    {
      int o = collection["GoAwareOnOverflow_channel2"].ToInt();
      if (o < 0)
        o = 0;
      if (o > 1)
        o = 1;
      TwoInputPulseCounter.SetGoAwareOnOverflow_channel2(sensor, o.ToBool());
    }
    double num8 = TwoInputPulseCounter.GetTransform_Channel2(sensor.SensorID) <= 0.0 ? 1.0 : TwoInputPulseCounter.GetTransform_Channel2(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["overflowCount_channel2"]))
    {
      long num9 = (collection["overflowCount_channel2"].ToDouble() / num8).ToLong();
      if (num9 < 0L)
        num9 = 0L;
      if (num9 > (long) uint.MaxValue)
        num9 = (long) uint.MaxValue;
      sensor.MaximumThreshold = num9;
    }
    if (!string.IsNullOrEmpty(collection["Accumulate_channel2"]))
    {
      int num10 = collection["Accumulate_channel2"].ToInt();
      if (num10 < 0)
        num10 = 0;
      if (num10 > 1)
        num10 = 1;
      TwoInputPulseCounter.SetAccumulate_channel2(sensor, num10);
    }
    if (!string.IsNullOrEmpty(collection["TriggerType_channel2"]))
    {
      int num11 = collection["TriggerType_channel2"].ToInt();
      if (num11 < 0)
        num11 = 0;
      if (num11 > 2)
        num11 = 2;
      TwoInputPulseCounter.SetTriggerType_channel2(sensor, num11);
    }
    if (!string.IsNullOrEmpty(collection["FilterFrequency_channel2"]))
    {
      int num12 = collection["FilterFrequency_channel2"].ToInt();
      if (num12 < 0)
        num12 = 0;
      if (num12 > 2000)
        num12 = 2000;
      TwoInputPulseCounter.SetFilterFrequency_channel2(sensor, num12);
    }
    if (string.IsNullOrEmpty(collection["ReArmTime_channel2"]) || string.IsNullOrEmpty(collection["ActiveStateInterval"]))
      return;
    int num13 = collection["ReArmTime_channel2"].ToInt();
    double o1 = collection["ActiveStateInterval"].ToDouble() * 60.0;
    if (num13 < 1)
      num13 = 1;
    if ((double) num13 > o1)
      num13 = o1.ToInt();
    TwoInputPulseCounter.SetReArmTime_channel2(sensor, num13);
  }

  public static string HystForUI(Sensor sensor) => "Not Used";

  public static string GetOverflowCount_channel1(Sensor sensor)
  {
    return ((double) sensor.MinimumThreshold * TwoInputPulseCounter.GetTransform_Channel1(sensor.SensorID)).ToString();
  }

  public static void SetAccumulate_channel1(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4294967040U;
    sensor.Calibration1 = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetAccumulate_channel1(Sensor sensor)
  {
    return (int) Convert.ToByte(Convert.ToUInt32(sensor.Calibration1) & (uint) byte.MaxValue);
  }

  public static void SetTriggerType_channel1(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4294902015U;
    sensor.Calibration1 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static int GetTriggerType_channel1(Sensor sensor)
  {
    return (int) Convert.ToUInt16((Convert.ToUInt32(sensor.Calibration1) & 65280U) >> 8);
  }

  public static void SetFilterFrequency_channel1(Sensor sensor, int value)
  {
    uint num = (uint) sensor.Calibration1 & (uint) ushort.MaxValue;
    sensor.Calibration1 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int GetFilterFrequency_channel1(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.Calibration1) >> 16 /*0x10*/);
  }

  public static void SetReArmTime_channel1(Sensor sensor, int value)
  {
    long num = (long) ((uint) sensor.Calibration2 & 4294901760U);
    sensor.Calibration2 = num | (long) (uint) value;
  }

  public static int GetReArmTime_channel1(Sensor sensor)
  {
    return (int) (sensor.Calibration2 & (long) ushort.MaxValue);
  }

  public static void SetOverflowControl_channel1(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration2) & 4278255615U;
    sensor.Calibration2 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static int GetOverflowControl_channel1(Sensor sensor)
  {
    return (int) Convert.ToUInt16((Convert.ToUInt32(sensor.Calibration2) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetReportImmediatelyOnOverflow_channel1(Sensor sensor, bool value)
  {
    sensor.Calibration2 = (long) Convert.ToUInt32(sensor.Calibration2).SetBit(value, 24);
  }

  public static int GetReportImmediatelyOnOverflow_channel1(Sensor sensor)
  {
    return Convert.ToUInt32(sensor.Calibration2).GetBit(24).ToInt();
  }

  public static void SetGoAwareOnOverflow_channel1(Sensor sensor, bool value)
  {
    sensor.Calibration2 = (long) Convert.ToUInt32(sensor.Calibration2).SetBit(value, 25);
  }

  public static int GetGoAwareOnOverflow_channel1(Sensor sensor)
  {
    return Convert.ToUInt32(sensor.Calibration2).GetBit(25).ToInt();
  }

  public static string GetOverflowCount_channel2(Sensor sensor)
  {
    return ((double) sensor.MaximumThreshold * TwoInputPulseCounter.GetTransform_Channel2(sensor.SensorID)).ToString();
  }

  public static void SetAccumulate_channel2(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration3) & 4294967040U;
    sensor.Calibration3 = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetAccumulate_channel2(Sensor sensor)
  {
    return (int) Convert.ToUInt16(Convert.ToUInt32(sensor.Calibration3 & (long) byte.MaxValue));
  }

  public static void SetTriggerType_channel2(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration3) & 4294902015U;
    sensor.Calibration3 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static int GetTriggerType_channel2(Sensor sensor)
  {
    return (int) Convert.ToUInt16((Convert.ToUInt32(sensor.Calibration3) & 65280U) >> 8);
  }

  public static void SetFilterFrequency_channel2(Sensor sensor, int value)
  {
    uint num = (uint) sensor.Calibration3 & (uint) ushort.MaxValue;
    sensor.Calibration3 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int GetFilterFrequency_channel2(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.Calibration3) >> 16 /*0x10*/);
  }

  public static void SetReArmTime_channel2(Sensor sensor, int value)
  {
    long num = (long) ((uint) sensor.Calibration4 & 4294901760U);
    sensor.Calibration4 = num | (long) (uint) value;
  }

  public static int GetReArmTime_channel2(Sensor sensor)
  {
    return (int) (sensor.Calibration4 & (long) ushort.MaxValue);
  }

  public static void SetOverflowControl_channel2(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration4) & 4278255615U;
    sensor.Calibration4 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static int GetOverflowControl_channel2(Sensor sensor)
  {
    return (int) Convert.ToUInt16((Convert.ToUInt32(sensor.Calibration4) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetReportImmediatelyOnOverflow_channel2(Sensor sensor, bool value)
  {
    sensor.Calibration4 = (long) Convert.ToUInt32(sensor.Calibration4).SetBit(value, 24);
  }

  public static int GetReportImmediatelyOnOverflow_channel2(Sensor sensor)
  {
    return Convert.ToUInt32(sensor.Calibration4).GetBit(24).ToInt();
  }

  public static void SetGoAwareOnOverflow_channel2(Sensor sensor, bool value)
  {
    sensor.Calibration4 = (long) Convert.ToUInt32(sensor.Calibration4).SetBit(value, 25);
  }

  public static int GetGoAwareOnOverflow_channel2(Sensor sensor)
  {
    return Convert.ToUInt32(sensor.Calibration4).GetBit(25).ToInt();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(TwoInputPulseCounter left, TwoInputPulseCounter right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(TwoInputPulseCounter left, TwoInputPulseCounter right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(TwoInputPulseCounter left, TwoInputPulseCounter right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(TwoInputPulseCounter left, TwoInputPulseCounter right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(TwoInputPulseCounter left, TwoInputPulseCounter right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(TwoInputPulseCounter left, TwoInputPulseCounter right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is TwoInputPulseCounter && this.Equals((MonnitApplicationBase) (obj as TwoInputPulseCounter));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is TwoInputPulseCounter && (long) this.Channel1 == (long) (right as TwoInputPulseCounter).Channel1;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is TwoInputPulseCounter && (long) this.Channel1 != (long) (right as TwoInputPulseCounter).Channel1;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is TwoInputPulseCounter && this.Channel1 < (right as TwoInputPulseCounter).Channel1;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is TwoInputPulseCounter && this.Channel1 <= (right as TwoInputPulseCounter).Channel1;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is TwoInputPulseCounter && this.Channel1 > (right as TwoInputPulseCounter).Channel1;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is TwoInputPulseCounter && this.Channel1 >= (right as TwoInputPulseCounter).Channel1;
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    sensor.ProfileConfigDirty = false;
    sensor.ProfileConfig2Dirty = false;
    sensor.PendingActionControlCommand = true;
    string str = collection["acc"];
    NonCachedAttribute nonCachedAttribute = NonCachedAttribute.LoadBySensorIDAndName(sensor.SensorID, "ResetAccumulatorACC");
    if (nonCachedAttribute != null)
    {
      if ((nonCachedAttribute.Value1 == "3" || nonCachedAttribute.Value1 == "4") && str != nonCachedAttribute.Value1)
        nonCachedAttribute.Value1 = "5";
    }
    else
      nonCachedAttribute = new NonCachedAttribute()
      {
        Name = "ResetAccumulatorACC",
        SensorID = sensor.SensorID,
        Value1 = str
      };
    nonCachedAttribute.Save();
    sensor.Save();
  }

  public static void UseSerializedRecipientProperties(
    Sensor sens,
    int queueID,
    string serializedRecipientProperties)
  {
    TwoInputPulseCounter.CalibrateSensor(sens, new NameValueCollection()
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

  public static string CreateSerializedRecipientProperties(int state1, int state2, int acc)
  {
    return $"{state1}|{state2}|{acc}";
  }

  public static void ParseSerializedRecipientProperties(
    string serialized,
    out int state1,
    out int state2,
    out int acc)
  {
    state1 = 0;
    state2 = 0;
    acc = 0;
    try
    {
      string[] strArray = serialized.Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      state1 = strArray[0].ToInt();
      state2 = strArray[1].ToInt();
      acc = strArray[2].ToInt();
    }
    catch
    {
    }
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    NonCachedAttribute nonCachedAttribute = NonCachedAttribute.LoadBySensorIDAndName(sensor.SensorID, "ResetAccumulatorACC");
    List<byte[]> numArrayList = new List<byte[]>();
    if (nonCachedAttribute != null)
    {
      byte[] bytes = BitConverter.GetBytes(nonCachedAttribute.Value1.ToInt());
      numArrayList.Add(TwoInputPulseCounterBase.CalibrateFrame(sensor.SensorID, bytes[0]));
    }
    return numArrayList;
  }

  public new static bool ActionControlCommandIsUrgent(Sensor sensor, string version) => false;

  public new static void ClearPendingActionControlCommand(
    Sensor sensor,
    string version,
    int actionCommand,
    bool success,
    byte[] data)
  {
    NonCachedAttribute.LoadBySensorIDAndName(sensor.SensorID, "ResetAccumulatorACC")?.Delete();
    sensor.PendingActionControlCommand = false;
  }
}
