// Decompiled with JetBrains decompiler
// Type: Monnit.ThreePhasePowerMeter
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class ThreePhasePowerMeter : MonnitApplicationBase
{
  public static long MonnitApplicationID => 108;

  public static string ApplicationName => "Three Phase Power Meter";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => ThreePhasePowerMeter.MonnitApplicationID;

  public double V1 { get; set; }

  public double V2 { get; set; }

  public double V3 { get; set; }

  public double I1 { get; set; }

  public double I2 { get; set; }

  public double I3 { get; set; }

  public double TotalPowerAccumulation { get; set; }

  public static ThreePhasePowerMeter Create(byte[] sdm, int startIndex)
  {
    ThreePhasePowerMeter threePhasePowerMeter = new ThreePhasePowerMeter();
    try
    {
      threePhasePowerMeter.V1 = (double) ((int) BitConverter.ToUInt16(sdm, startIndex) / 10);
      threePhasePowerMeter.V2 = (double) ((int) BitConverter.ToUInt16(sdm, startIndex + 2) / 10);
      threePhasePowerMeter.V3 = (double) ((int) BitConverter.ToUInt16(sdm, startIndex + 4) / 10);
      threePhasePowerMeter.I1 = (double) ((int) BitConverter.ToUInt16(sdm, startIndex + 6) / 100);
      threePhasePowerMeter.I2 = (double) ((int) BitConverter.ToUInt16(sdm, startIndex + 8) / 100);
      threePhasePowerMeter.I3 = (double) ((int) BitConverter.ToUInt16(sdm, startIndex + 10) / 100);
      byte[] destinationArray = new byte[8];
      Array.Copy((Array) sdm, startIndex + 12, (Array) destinationArray, 0, 6);
      threePhasePowerMeter.TotalPowerAccumulation = (double) (BitConverter.ToUInt64(destinationArray, 0) / 1000UL);
    }
    catch
    {
      threePhasePowerMeter.V1 = 0.0;
      threePhasePowerMeter.V2 = 0.0;
      threePhasePowerMeter.V3 = 0.0;
      threePhasePowerMeter.I1 = 0.0;
      threePhasePowerMeter.I2 = 0.0;
      threePhasePowerMeter.I3 = 0.0;
      threePhasePowerMeter.TotalPowerAccumulation = 0.0;
    }
    return threePhasePowerMeter;
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[7]
      {
        new AppDatum(eDatumType.Voltage, "V1", this.V1),
        new AppDatum(eDatumType.Voltage, "V2", this.V2),
        new AppDatum(eDatumType.Voltage, "V3", this.V3),
        new AppDatum(eDatumType.Voltage, "I1", this.I1),
        new AppDatum(eDatumType.Voltage, "I2", this.I2),
        new AppDatum(eDatumType.Voltage, "I3", this.I3),
        new AppDatum(eDatumType.WattHours, "TotalPowerAccumulation", this.TotalPowerAccumulation)
      });
    }
  }

  public override string Serialize()
  {
    return $"{this.V1.ToString()}|{this.V2.ToString()}|{this.V3.ToString()}|{this.I1.ToString()}|{this.I2.ToString()}|{this.I3.ToString()}|{this.TotalPowerAccumulation.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) ThreePhasePowerMeter.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      return $"V1 {this.V1.ToString("0.0#")} VRMS,  V2 {this.V2.ToString("0.0#")} VRMS, V3 {this.V3.ToString("0.0#")} VRMS, I1 {this.I1.ToString("0.00#")} VRMS, I2 {this.I2.ToString("0.00#")} VRMS,  I3 {this.I3.ToString("0.00#")} VRMS,  Total Power Accumulation {this.TotalPowerAccumulation.ToString("0.000#")} KWH";
    }
  }

  public override object PlotValue
  {
    get
    {
      return (object) new List<object>((IEnumerable<object>) new object[7]
      {
        (object) this.V1,
        (object) this.V2,
        (object) this.V3,
        (object) this.I1,
        (object) this.I2,
        (object) this.I3,
        (object) this.TotalPowerAccumulation
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[7]
    {
      (object) this.V1,
      (object) this.V2,
      (object) this.V3,
      (object) this.I1,
      (object) this.I2,
      (object) this.I3,
      (object) this.TotalPowerAccumulation
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[7]
    {
      "V1",
      "V2",
      "V3",
      "I1",
      "I2",
      "I3",
      "Total Power Accumulation"
    });
  }

  public static ThreePhasePowerMeter Deserialize(string version, string serialized)
  {
    ThreePhasePowerMeter threePhasePowerMeter = new ThreePhasePowerMeter();
    if (string.IsNullOrEmpty(serialized))
    {
      threePhasePowerMeter.V1 = 0.0;
      threePhasePowerMeter.V2 = 0.0;
      threePhasePowerMeter.V3 = 0.0;
      threePhasePowerMeter.I1 = 0.0;
      threePhasePowerMeter.I2 = 0.0;
      threePhasePowerMeter.I3 = 0.0;
      threePhasePowerMeter.TotalPowerAccumulation = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      try
      {
        threePhasePowerMeter.V1 = strArray[0].ToDouble();
        threePhasePowerMeter.V2 = strArray[1].ToDouble();
        threePhasePowerMeter.V3 = strArray[2].ToDouble();
        threePhasePowerMeter.I1 = strArray[3].ToDouble();
        threePhasePowerMeter.I2 = strArray[4].ToDouble();
        threePhasePowerMeter.I3 = strArray[5].ToDouble();
        threePhasePowerMeter.TotalPowerAccumulation = strArray[6].ToDouble();
      }
      catch
      {
        threePhasePowerMeter.V1 = 0.0;
        threePhasePowerMeter.V2 = 0.0;
        threePhasePowerMeter.V3 = 0.0;
        threePhasePowerMeter.I1 = 0.0;
        threePhasePowerMeter.I2 = 0.0;
        threePhasePowerMeter.I3 = 0.0;
        threePhasePowerMeter.TotalPowerAccumulation = 0.0;
      }
    }
    return threePhasePowerMeter;
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
      ApplicationID = ThreePhasePowerMeter.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["V123Hyst_Manual"]))
    {
      int num = collection["V123Hyst_Manual"].ToInt();
      if (num < 0)
        num = 0;
      if (num > 250)
        num = 250;
      ThreePhasePowerMeter.SetV123Hyst(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["I1Hyst_Manual"]))
    {
      int num = collection["I1Hyst_Manual"].ToInt();
      if (num < 0)
        num = 0;
      if (num > 250)
        num = 250;
      ThreePhasePowerMeter.SetI1Hyst(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["I2Hyst_Manual"]))
    {
      int num = collection["I2Hyst_Manual"].ToInt();
      if (num < 0)
        num = 0;
      if (num > 250)
        num = 250;
      ThreePhasePowerMeter.SetI2Hyst(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["I3Hyst_Manual"]))
    {
      int num = collection["I3Hyst_Manual"].ToInt();
      if (num < 0)
        num = 0;
      if (num > 250)
        num = 250;
      ThreePhasePowerMeter.SetI3Hyst(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["V123ThreshMin_Manual"]) && !string.IsNullOrEmpty(collection["V123ThreshMax_Manual"]))
    {
      int o1 = collection["V123ThreshMin_Manual"].ToInt();
      if (o1 < 0)
        o1 = 0;
      if (o1 > 750)
        o1 = 750;
      int o2 = collection["V123ThreshMax_Manual"].ToInt();
      if (o2 < 0)
        o2 = 0;
      if (o2 > 750)
        o2 = 750;
      if (o2 < o1)
        o2 = o1;
      if (o1 > o2)
        o1 = o2;
      ThreePhasePowerMeter.SetV123ThresVMin(sensor, o1.ToInt());
      ThreePhasePowerMeter.SetV123ThresVMax(sensor, o2.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["I1ThreshMin_Manual"]) && !string.IsNullOrEmpty(collection["I1ThreshMax_Manual"]))
    {
      int num1 = collection["I1ThreshMin_Manual"].ToInt();
      if (num1 < 0)
        num1 = 0;
      if (num1 > (int) byte.MaxValue)
        num1 = (int) byte.MaxValue;
      int num2 = collection["I1ThreshMax_Manual"].ToInt();
      if (num2 < 0)
        num2 = 0;
      if (num2 > (int) byte.MaxValue)
        num2 = (int) byte.MaxValue;
      if (num2 < num1)
        num2 = num1;
      if (num1 > num2)
        num1 = num2;
      ThreePhasePowerMeter.SetI1ThresVMin(sensor, num1);
      ThreePhasePowerMeter.SetI1ThresVMax(sensor, num2);
    }
    if (!string.IsNullOrEmpty(collection["I2ThreshMin_Manual"]) && !string.IsNullOrEmpty(collection["I2ThreshMax_Manual"]))
    {
      int num3 = collection["I2ThreshMin_Manual"].ToInt();
      if (num3 < 0)
        num3 = 0;
      if (num3 > (int) byte.MaxValue)
        num3 = (int) byte.MaxValue;
      int num4 = collection["I2ThreshMax_Manual"].ToInt();
      if (num4 < 0)
        num4 = 0;
      if (num4 > (int) byte.MaxValue)
        num4 = (int) byte.MaxValue;
      if (num4 < num3)
        num4 = num3;
      if (num3 > num4)
        num3 = num4;
      ThreePhasePowerMeter.SetI2ThresVMin(sensor, num3);
      ThreePhasePowerMeter.SetI2ThresVMax(sensor, num4);
    }
    if (!string.IsNullOrEmpty(collection["I3ThreshMin_Manual"]) && !string.IsNullOrEmpty(collection["I3ThreshMax_Manual"]))
    {
      int num5 = collection["I3ThreshMin_Manual"].ToInt();
      if (num5 < 0)
        num5 = 0;
      if (num5 > (int) byte.MaxValue)
        num5 = (int) byte.MaxValue;
      int num6 = collection["I3ThreshMax_Manual"].ToInt();
      if (num6 < 0)
        num6 = 0;
      if (num6 > (int) byte.MaxValue)
        num6 = (int) byte.MaxValue;
      if (num6 < num5)
        num6 = num5;
      if (num5 > num6)
        num5 = num6;
      ThreePhasePowerMeter.SetI3ThresVMin(sensor, num5);
      ThreePhasePowerMeter.SetI3ThresVMax(sensor, num6);
    }
    if (!string.IsNullOrEmpty(collection["MeasurementInterval_Manual"]))
    {
      double num7 = collection["MeasurementInterval_Manual"].ToDouble();
      double num8 = collection["ActiveStateInterval"].ToDouble();
      if (num7 < 0.017)
        num7 = 0.017;
      if (num7 > 720.0)
        num7 = 720.0;
      if (num8 > 0.0)
      {
        if (num8 < 0.017)
          num8 = 0.017;
        if (num8 > 720.0)
          num8 = 720.0;
        if (num7 > num8)
          num7 = num8;
      }
      else
        num7 = 1.0;
      ThreePhasePowerMeter.setMeasurementInterval(sensor, (num7 * 60.0).ToInt());
    }
    if (string.IsNullOrEmpty(collection["Accumulate_Manual"]))
      return;
    int num9 = collection["Accumulate_Manual"].ToInt();
    if (num9 > 1 || num9 < 0)
      num9 = 1;
    ThreePhasePowerMeter.SetAccumulate(sensor, num9);
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    NonCachedAttribute.LoadBySensorIDAndName(sensor.SensorID, "Calibration").Save();
    sensor.ProfileConfigDirty = false;
    sensor.ProfileConfig2Dirty = false;
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public static void SetI3Hyst(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 16777215U /*0xFFFFFF*/;
    sensor.Hysteresis = (long) (num | (uint) ((value & (int) byte.MaxValue) << 24));
  }

  public static int GetI3Hyst(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.Hysteresis & 4278190080L /*0xFF000000*/) >> 24);
  }

  public static void SetI2Hyst(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4278255615U;
    sensor.Hysteresis = (long) (num | (uint) ((value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static int GetI2Hyst(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.Hysteresis & 16711680L /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetI1Hyst(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4294902015U;
    sensor.Hysteresis = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static int GetI1Hyst(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.Hysteresis & 65280L) >> 8);
  }

  public static void SetV123Hyst(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4294967040U;
    sensor.Hysteresis = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetV123Hyst(Sensor sensor)
  {
    return (int) Convert.ToUInt32(sensor.Hysteresis & (long) byte.MaxValue);
  }

  public static void SetI3ThresVMin(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 16777215U /*0xFFFFFF*/;
    sensor.MinimumThreshold = (long) (num | (uint) ((value & (int) byte.MaxValue) << 24));
  }

  public static int GetI3ThresVMin(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.MinimumThreshold & 4278190080L /*0xFF000000*/) >> 24);
  }

  public static void SetI2ThresVMin(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4278255615U;
    sensor.MinimumThreshold = (long) (num | (uint) ((value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static int GetI2ThresVMin(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.MinimumThreshold & 16711680L /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetI1ThresVMin(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4294902015U;
    sensor.MinimumThreshold = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static int GetI1ThresVMin(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.MinimumThreshold & 65280L) >> 8);
  }

  public static void SetV123ThresVMin(Sensor sensor, int value)
  {
    value /= 3;
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4294967040U;
    sensor.MinimumThreshold = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetV123ThresVMin(Sensor sensor)
  {
    return (int) Convert.ToUInt32(sensor.MinimumThreshold & (long) byte.MaxValue) * 3;
  }

  public static void SetI3ThresVMax(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 16777215U /*0xFFFFFF*/;
    sensor.MaximumThreshold = (long) (num | (uint) ((value & (int) byte.MaxValue) << 24));
  }

  public static int GetI3ThresVMax(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.MaximumThreshold & 4278190080L /*0xFF000000*/) >> 24);
  }

  public static void SetI2ThresVMax(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 4278255615U;
    sensor.MaximumThreshold = (long) (num | (uint) ((value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static int GetI2ThresVMax(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.MaximumThreshold & 16711680L /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetI1ThresVMax(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 4294902015U;
    sensor.MaximumThreshold = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static int GetI1ThresVMax(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.MaximumThreshold & 65280L) >> 8);
  }

  public static void SetV123ThresVMax(Sensor sensor, int value)
  {
    value /= 3;
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 4294967040U;
    sensor.MaximumThreshold = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetV123ThresVMax(Sensor sensor)
  {
    return (int) Convert.ToUInt32(sensor.MaximumThreshold & (long) byte.MaxValue) * 3;
  }

  public static void SetCurrentCalibrationChannel1(Sensor sens, int value)
  {
    int num = (int) ((long) (int) sens.Calibration1 & 4294901760L);
    sens.Calibration1 = (long) (num | (int) (ushort) value);
  }

  public static int GetCurrentCalibrationChannel1(Sensor sens)
  {
    return (int) (ushort) (int) (sens.Calibration1 & (long) ushort.MaxValue);
  }

  public static void SetVoltageCalibrationChannel1(Sensor sens, int value)
  {
    int num = (int) sens.Calibration1 & (int) ushort.MaxValue;
    sens.Calibration1 = (long) (num | (int) (ushort) value << 16 /*0x10*/);
  }

  public static int GetVoltageCalibrationChannel1(Sensor sens)
  {
    return (int) (ushort) ((uint) sens.Calibration1 >> 16 /*0x10*/).ToInt();
  }

  public static void SetCurrentCalibrationChannel2(Sensor sens, int value)
  {
    int num = (int) ((long) (int) sens.Calibration2 & 4294901760L);
    sens.Calibration2 = (long) (num | (int) (ushort) value);
  }

  public static int GetCurrentCalibrationChannel2(Sensor sens)
  {
    return (int) (ushort) (int) (sens.Calibration2 & (long) ushort.MaxValue);
  }

  public static void SetVoltageCalibrationChannel2(Sensor sens, int value)
  {
    int num = (int) sens.Calibration2 & (int) ushort.MaxValue;
    sens.Calibration2 = (long) (num | (int) (ushort) value << 16 /*0x10*/);
  }

  public static int GetVoltageCalibrationChannel2(Sensor sens)
  {
    return (int) (ushort) ((uint) sens.Calibration2 >> 16 /*0x10*/).ToInt();
  }

  public static void SetCurrentCalibrationChannel3(Sensor sens, int value)
  {
    int num = (int) ((long) (int) sens.Calibration3 & 4294901760L);
    sens.Calibration3 = (long) (num | (int) (ushort) value);
  }

  public static int GetCurrentCalibrationChannel3(Sensor sens)
  {
    return (int) (ushort) (int) (sens.Calibration3 & (long) ushort.MaxValue);
  }

  public static void SetVoltageCalibrationChannel3(Sensor sens, int value)
  {
    int num = (int) sens.Calibration3 & (int) ushort.MaxValue;
    sens.Calibration3 = (long) (num | (int) (ushort) value << 16 /*0x10*/);
  }

  public static int GetVoltageCalibrationChannel3(Sensor sens)
  {
    return (int) (ushort) ((uint) sens.Calibration3 >> 16 /*0x10*/).ToInt();
  }

  public static void setMeasurementInterval(Sensor sens, int value)
  {
    int num = (int) ((long) (int) sens.Calibration4 & 4294901760L);
    sens.Calibration4 = (long) (num | (int) (ushort) value);
  }

  public static int GetMeasurementInterval(Sensor sens)
  {
    return ((uint) ((int) (uint) sens.Calibration4 & (int) ushort.MaxValue)).ToInt();
  }

  public static void SetAccumulate(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration4) & 4278255615U;
    sensor.Calibration4 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static int GetAccumulate(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.Calibration4 & 16711680L /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(ThreePhasePowerMeter left, ThreePhasePowerMeter right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(ThreePhasePowerMeter left, ThreePhasePowerMeter right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(ThreePhasePowerMeter left, ThreePhasePowerMeter right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(ThreePhasePowerMeter left, ThreePhasePowerMeter right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(ThreePhasePowerMeter left, ThreePhasePowerMeter right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(ThreePhasePowerMeter left, ThreePhasePowerMeter right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is ThreePhasePowerMeter && this.Equals((MonnitApplicationBase) (obj as ThreePhasePowerMeter));
  }

  public override bool Equals(MonnitApplicationBase right) => false;

  public override bool NotEqual(MonnitApplicationBase right) => false;

  public override bool LessThan(MonnitApplicationBase right) => false;

  public override bool LessThanEqual(MonnitApplicationBase right) => false;

  public override bool GreaterThan(MonnitApplicationBase right) => false;

  public override bool GreaterThanEqual(MonnitApplicationBase right) => false;

  public static long DefaultMinThreshold => (long) uint.MaxValue;

  public static long DefaultMaxThreshold => (long) uint.MaxValue;

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
