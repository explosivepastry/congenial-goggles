// Decompiled with JetBrains decompiler
// Type: Monnit.CTPower
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

public class CTPower : MonnitApplicationBase
{
  private SensorAttribute _showAdvCal;

  public static long MonnitApplicationID => 81;

  public static string ApplicationName => "CT Power - Single";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => CTPower.MonnitApplicationID;

  public int State { get; set; }

  public int Channel { get; set; }

  public double PowerUsageChannelOne { get; set; }

  public int FreqChannelOne { get; set; }

  public double PowerUsageChannelTwo { get; set; }

  public int FreqChannelTwo { get; set; }

  public double PowerUsageChannelThree { get; set; }

  public int FreqChannelThree { get; set; }

  public double AvgPowerChannelOne { get; set; }

  public double PowerFactorChannelOne { get; set; }

  public double AvgPowerChannelTwo { get; set; }

  public double PowerFactorChannelTwo { get; set; }

  public double AvgPowerChannelThree { get; set; }

  public double PowerFactorChannelThree { get; set; }

  public double VoltsChannelOne { get; set; }

  public double CurrentChannelOne { get; set; }

  public int TotalHarmonicDistortionVoltageChannelOne { get; set; }

  public int TotalHarmonicDistortionCurrentChannelOne { get; set; }

  public double VoltsChannelTwo { get; set; }

  public double CurrentChannelTwo { get; set; }

  public int TotalHarmonicDistortionVoltageChannelTwo { get; set; }

  public int TotalHarmonicDistortionCurrentChannelTwo { get; set; }

  public double VoltsChannelThree { get; set; }

  public double CurrentChannelThree { get; set; }

  public int TotalHarmonicDistortionVoltageChannelThree { get; set; }

  public int TotalHarmonicDistortionCurrentChannelThree { get; set; }

  public SensorAttribute showAdvCal
  {
    get
    {
      if (this._showAdvCal == null)
        this._showAdvCal = new SensorAttribute()
        {
          Value = "false"
        };
      return this._showAdvCal;
    }
  }

  public static bool ShowAdvCal(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "showAdvCal" && sensorAttribute.Value == "true")
        return true;
    }
    return false;
  }

  public static void SeeAdvCal(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "showAdvCal")
      {
        if (sensorAttribute.Value != "false")
        {
          sensorAttribute.Value = "true";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "showAdvCal",
        Value = "false",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public override string Serialize()
  {
    return $"{this.State.ToString()}|{this.Channel.ToString()}|{this.PowerUsageChannelOne.ToString()}|{this.FreqChannelOne.ToString()}|{this.PowerUsageChannelTwo.ToString()}|{this.FreqChannelTwo.ToString()}|{this.PowerUsageChannelThree.ToString()}|{this.FreqChannelThree.ToString()}|{this.AvgPowerChannelOne.ToString()}|{this.PowerFactorChannelOne.ToString()}|{this.AvgPowerChannelTwo.ToString()}|{this.PowerFactorChannelTwo.ToString()}|{this.AvgPowerChannelThree.ToString()}|{this.PowerFactorChannelThree.ToString()}|{this.VoltsChannelOne.ToString()}|{this.CurrentChannelOne.ToString()}|{this.TotalHarmonicDistortionVoltageChannelOne.ToString()}|{this.TotalHarmonicDistortionCurrentChannelOne.ToString()}|{this.VoltsChannelTwo.ToString()}|{this.CurrentChannelTwo.ToString()}|{this.TotalHarmonicDistortionVoltageChannelTwo.ToString()}|{this.TotalHarmonicDistortionCurrentChannelTwo.ToString()}|{this.VoltsChannelThree.ToString()}|{this.CurrentChannelThree.ToString()}|{this.TotalHarmonicDistortionVoltageChannelThree.ToString()}|{this.TotalHarmonicDistortionCurrentChannelThree.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[26]
      {
        new AppDatum(eDatumType.State, "State", this.State),
        new AppDatum(eDatumType.Count, "Channel", this.Channel),
        new AppDatum(eDatumType.WattHours, "Power Usage Ch1", this.PowerUsageChannelOne),
        new AppDatum(eDatumType.Frequency, "Freq Ch1", this.FreqChannelOne),
        new AppDatum(eDatumType.WattHours, "Power Usage Ch2", this.PowerUsageChannelTwo),
        new AppDatum(eDatumType.Frequency, "Freq Ch2", this.FreqChannelTwo),
        new AppDatum(eDatumType.WattHours, "Power Usage Ch3", this.PowerUsageChannelThree),
        new AppDatum(eDatumType.Frequency, "Freq Ch3", this.FreqChannelThree),
        new AppDatum(eDatumType.WattHours, "Avg Power Ch1", this.AvgPowerChannelOne),
        new AppDatum(eDatumType.WattHours, "Power Factor Ch1", this.PowerFactorChannelOne),
        new AppDatum(eDatumType.WattHours, "Avg Power Ch2", this.AvgPowerChannelTwo),
        new AppDatum(eDatumType.WattHours, "Power Factor Ch2", this.PowerFactorChannelTwo),
        new AppDatum(eDatumType.WattHours, "Avg Power Ch3", this.AvgPowerChannelThree),
        new AppDatum(eDatumType.WattHours, "Power Factor Ch3", this.PowerFactorChannelThree),
        new AppDatum(eDatumType.Voltage, "Avg Volts RMS Ch1", this.VoltsChannelOne),
        new AppDatum(eDatumType.Amps, "Avg Current RMS Ch1", this.CurrentChannelOne),
        new AppDatum(eDatumType.Percentage, "Total Harmonic Distortion Voltage Ch1", this.TotalHarmonicDistortionVoltageChannelOne),
        new AppDatum(eDatumType.Percentage, "Total Harmonic Distortion Current Ch1", this.TotalHarmonicDistortionCurrentChannelOne),
        new AppDatum(eDatumType.Voltage, "Avg Volts RMS Ch2", this.VoltsChannelTwo),
        new AppDatum(eDatumType.Amps, "Avg Current RMS Ch2", this.CurrentChannelTwo),
        new AppDatum(eDatumType.Percentage, "Total Harmonic Distortion Voltage Ch2", this.TotalHarmonicDistortionVoltageChannelTwo),
        new AppDatum(eDatumType.Percentage, "Total Harmonic Distortion Current Ch2", this.TotalHarmonicDistortionCurrentChannelTwo),
        new AppDatum(eDatumType.Voltage, "Avg Volts RMS Ch3", this.VoltsChannelThree),
        new AppDatum(eDatumType.Amps, "Avg Current RMS Ch3", this.CurrentChannelThree),
        new AppDatum(eDatumType.Percentage, "Total Harmonic Distortion Voltage Ch3", this.TotalHarmonicDistortionVoltageChannelThree),
        new AppDatum(eDatumType.Percentage, "Total Harmonic Distortion Current Ch3", this.TotalHarmonicDistortionCurrentChannelThree)
      });
    }
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) CTPower.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      string format = "Channel {0}, Power Usage {1}, Average {2}, Volts {3}, Current {4}, Freq {5}, PowerFactor{6}, DistortionCurrent {7}, DistortionVoltage {8}";
      if (this.Channel == 0)
        return string.Format(format, (object) "A", (object) this.PowerUsageChannelOne, (object) this.AvgPowerChannelOne, (object) this.VoltsChannelOne, (object) this.CurrentChannelOne, (object) this.FreqChannelOne, (object) this.PowerFactorChannelOne, (object) this.TotalHarmonicDistortionCurrentChannelOne, (object) this.TotalHarmonicDistortionVoltageChannelOne);
      if (this.Channel == 1)
        return string.Format(format, (object) "B", (object) this.PowerUsageChannelTwo, (object) this.AvgPowerChannelTwo, (object) this.VoltsChannelTwo, (object) this.CurrentChannelTwo, (object) this.FreqChannelTwo, (object) this.PowerFactorChannelTwo, (object) this.TotalHarmonicDistortionCurrentChannelTwo, (object) this.TotalHarmonicDistortionVoltageChannelTwo);
      if (this.Channel != 2)
        return string.Format("Type Not Defined");
      return string.Format(format, (object) "C", (object) this.PowerUsageChannelThree, (object) this.AvgPowerChannelThree, (object) this.VoltsChannelThree, (object) this.CurrentChannelThree, (object) this.FreqChannelThree, (object) this.PowerFactorChannelThree, (object) this.TotalHarmonicDistortionCurrentChannelThree, (object) this.TotalHarmonicDistortionVoltageChannelThree);
    }
  }

  public override object PlotValue
  {
    get
    {
      if (this.Channel == 0)
        return (object) this.PowerUsageChannelOne;
      if (this.Channel == 1)
        return (object) this.PowerUsageChannelTwo;
      return this.Channel == 2 ? (object) this.PowerUsageChannelThree : (object) null;
    }
  }

  public static string SpecialExportValue(MonnitApplicationBase app)
  {
    return "Channel: " + ((CTPower) app).Channel.ToString();
  }

  public static CTPower Deserialize(string version, string serialized)
  {
    CTPower ctPower = new CTPower();
    if (string.IsNullOrEmpty(serialized))
    {
      ctPower.State = 0;
      ctPower.Channel = 0;
      ctPower.PowerUsageChannelOne = 0.0;
      ctPower.PowerUsageChannelTwo = 0.0;
      ctPower.PowerUsageChannelThree = 0.0;
      ctPower.FreqChannelOne = 0;
      ctPower.FreqChannelTwo = 0;
      ctPower.FreqChannelThree = 0;
      ctPower.AvgPowerChannelOne = 0.0;
      ctPower.AvgPowerChannelTwo = 0.0;
      ctPower.AvgPowerChannelThree = 0.0;
      ctPower.PowerFactorChannelOne = 0.0;
      ctPower.PowerFactorChannelTwo = 0.0;
      ctPower.PowerFactorChannelThree = 0.0;
      ctPower.VoltsChannelOne = 0.0;
      ctPower.VoltsChannelTwo = 0.0;
      ctPower.VoltsChannelThree = 0.0;
      ctPower.CurrentChannelOne = 0.0;
      ctPower.CurrentChannelTwo = 0.0;
      ctPower.CurrentChannelThree = 0.0;
      ctPower.TotalHarmonicDistortionVoltageChannelOne = 0;
      ctPower.TotalHarmonicDistortionVoltageChannelTwo = 0;
      ctPower.TotalHarmonicDistortionVoltageChannelThree = 0;
      ctPower.TotalHarmonicDistortionCurrentChannelOne = 0;
      ctPower.TotalHarmonicDistortionCurrentChannelTwo = 0;
      ctPower.TotalHarmonicDistortionCurrentChannelThree = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      ctPower.State = strArray[0].ToInt();
      ctPower.Channel = (int) Convert.ToByte(strArray[1]);
      ctPower.PowerUsageChannelOne = (double) strArray[2].ToLong();
      ctPower.PowerUsageChannelTwo = (double) strArray[4].ToLong();
      ctPower.PowerUsageChannelThree = (double) strArray[6].ToLong();
      ctPower.FreqChannelOne = strArray[3].ToInt();
      ctPower.FreqChannelTwo = strArray[5].ToInt();
      ctPower.FreqChannelThree = strArray[7].ToInt();
      ctPower.AvgPowerChannelOne = (double) strArray[8].ToInt();
      ctPower.AvgPowerChannelTwo = (double) strArray[10].ToInt();
      ctPower.AvgPowerChannelThree = (double) strArray[12].ToInt();
      ctPower.PowerFactorChannelOne = (double) (sbyte) strArray[9].ToInt();
      ctPower.PowerFactorChannelTwo = (double) (sbyte) strArray[11].ToInt();
      ctPower.PowerFactorChannelThree = (double) (sbyte) strArray[13].ToInt();
      ctPower.VoltsChannelOne = strArray[14].ToDouble();
      ctPower.VoltsChannelTwo = strArray[18].ToDouble();
      ctPower.VoltsChannelThree = strArray[22].ToDouble();
      ctPower.CurrentChannelOne = strArray[15].ToDouble();
      ctPower.CurrentChannelTwo = strArray[19].ToDouble();
      ctPower.CurrentChannelThree = strArray[23].ToDouble();
      ctPower.TotalHarmonicDistortionVoltageChannelOne = strArray[16 /*0x10*/].ToInt();
      ctPower.TotalHarmonicDistortionVoltageChannelTwo = strArray[20].ToInt();
      ctPower.TotalHarmonicDistortionVoltageChannelThree = strArray[24].ToInt();
      ctPower.TotalHarmonicDistortionCurrentChannelOne = strArray[17].ToInt();
      ctPower.TotalHarmonicDistortionCurrentChannelTwo = strArray[21].ToInt();
      ctPower.TotalHarmonicDistortionCurrentChannelThree = strArray[25].ToInt();
    }
    return ctPower;
  }

  public static CTPower Create(byte[] sdm, int startIndex)
  {
    CTPower ctPower = new CTPower()
    {
      State = (int) sdm[startIndex - 1]
    };
    ctPower.Channel = ctPower.State >> 4;
    switch (ctPower.Channel)
    {
      case 0:
        ctPower.PowerUsageChannelOne = (double) BitConverter.ToInt32(sdm, startIndex);
        ctPower.AvgPowerChannelOne = (double) BitConverter.ToInt32(sdm, startIndex + 4);
        ctPower.VoltsChannelOne = (double) BitConverter.ToUInt16(sdm, startIndex + 8);
        ctPower.CurrentChannelOne = (double) BitConverter.ToUInt16(sdm, startIndex + 10);
        ctPower.FreqChannelOne = (int) sdm[startIndex + 11];
        ctPower.PowerFactorChannelOne = (double) (sbyte) sdm[startIndex + 12];
        ctPower.TotalHarmonicDistortionCurrentChannelOne = (int) sdm[startIndex + 13];
        ctPower.TotalHarmonicDistortionVoltageChannelOne = (int) sdm[startIndex + 14];
        break;
      case 1:
        ctPower.PowerUsageChannelTwo = (double) BitConverter.ToInt32(sdm, startIndex);
        ctPower.AvgPowerChannelTwo = (double) BitConverter.ToInt32(sdm, startIndex + 4);
        ctPower.VoltsChannelTwo = (double) BitConverter.ToUInt16(sdm, startIndex + 8);
        ctPower.CurrentChannelTwo = (double) BitConverter.ToUInt16(sdm, startIndex + 10);
        ctPower.FreqChannelTwo = (int) sdm[startIndex + 11];
        ctPower.PowerFactorChannelTwo = (double) (sbyte) sdm[startIndex + 12];
        ctPower.TotalHarmonicDistortionCurrentChannelTwo = (int) sdm[startIndex + 13];
        ctPower.TotalHarmonicDistortionVoltageChannelTwo = (int) sdm[startIndex + 14];
        break;
      case 2:
        ctPower.PowerUsageChannelThree = (double) BitConverter.ToInt32(sdm, startIndex);
        ctPower.AvgPowerChannelThree = (double) BitConverter.ToInt32(sdm, startIndex + 4);
        ctPower.VoltsChannelThree = (double) BitConverter.ToUInt16(sdm, startIndex + 8);
        ctPower.CurrentChannelThree = (double) BitConverter.ToUInt16(sdm, startIndex + 10);
        ctPower.FreqChannelThree = (int) sdm[startIndex + 911];
        ctPower.PowerFactorChannelThree = (double) (sbyte) sdm[startIndex + 12];
        ctPower.TotalHarmonicDistortionCurrentChannelThree = (int) sdm[startIndex + 13];
        ctPower.TotalHarmonicDistortionVoltageChannelThree = (int) sdm[startIndex + 14];
        break;
    }
    return ctPower;
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
      ApplicationID = CTPower.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    double result;
    if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (!string.IsNullOrEmpty(collection["MintThreshByte1and2"]) && double.TryParse(collection["MintThreshByte1and2"], out result))
      CTPower.SetMinThreshByteOneAndTwo(sensor, (result * 10.0).ToInt());
    if (!string.IsNullOrEmpty(collection["MintThreshByte3and4"]) && double.TryParse(collection["MintThreshByte3and4"], out result))
      CTPower.SetMinThreshByteThreeAndFour(sensor, (result * 100.0).ToInt());
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) && double.TryParse(collection["MaximumThreshold_Manual"], out result))
    {
      sensor.MaximumThreshold = (result * 1000.0).ToLong();
      if (sensor.MaximumThreshold < 1000L)
        sensor.MaximumThreshold = 1000L;
      if (sensor.MaximumThreshold > (long) uint.MaxValue)
        sensor.MaximumThreshold = (long) uint.MaxValue;
    }
    if (!string.IsNullOrWhiteSpace(collection["CalVal3_Manual"]))
      CTPower.SetCalVal3Lower(sensor, (ushort) Convert.ToByte(collection["CalVal3_Manual"]));
    if (!string.IsNullOrEmpty(collection["Byte1OfHyst"]) && double.TryParse(collection["Byte1OfHyst"], out result))
      CTPower.SetHystFirstByte(sensor, (byte) result.ToInt());
    if (!string.IsNullOrEmpty(collection["DefaultFrequency__Manual"]) && double.TryParse(collection["DefaultFrequency__Manual"], out result))
      CTPower.SetHystSecondByte(sensor, (byte) result.ToInt());
    int num = 1;
    foreach (string allKey in collection.AllKeys)
    {
      switch (allKey)
      {
        case "bit1":
          num += 2;
          break;
        case "bit2":
          num += 4;
          break;
        case "bit3":
          num += 8;
          break;
        case "bit4":
          num += 16 /*0x10*/;
          break;
        case "bit5":
          num += 32 /*0x20*/;
          break;
        case "bit6":
          num += 64 /*0x40*/;
          break;
        case "bit7":
          num += 128 /*0x80*/;
          break;
      }
    }
    CTPower.SetHystThirdByte(sensor, (byte) num);
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
    nonCachedAttribute1.Value2 = string.IsNullOrWhiteSpace(collection["channel"]) ? "5" : collection["channel"].ToString();
    double num;
    if (!string.IsNullOrWhiteSpace(collection["voltage"]))
    {
      NonCachedAttribute nonCachedAttribute2 = nonCachedAttribute1;
      num = collection["voltage"].ToDouble() * 10.0;
      string str = num.ToString();
      nonCachedAttribute2.Value3 = str;
    }
    else
      nonCachedAttribute1.Value3 = "-1";
    if (!string.IsNullOrWhiteSpace(collection["current"]))
    {
      NonCachedAttribute nonCachedAttribute3 = nonCachedAttribute1;
      num = collection["current"].ToDouble() * 100.0;
      string str = num.ToString();
      nonCachedAttribute3.Value3 = str;
    }
    else
      nonCachedAttribute1.Value3 = "-1";
    nonCachedAttribute1.Save();
    sensor.ProfileConfigDirty = false;
    sensor.ProfileConfig2Dirty = false;
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    NonCachedAttribute nonCachedAttribute = NonCachedAttribute.LoadBySensorIDAndName(sensor.SensorID, "Calibration");
    int acc = nonCachedAttribute.Value1.ToInt();
    int channel = nonCachedAttribute.Value2.ToInt();
    double voltage = nonCachedAttribute.Value3.ToDouble() / 10.0;
    double current = nonCachedAttribute.Value4.ToDouble() / 100.0;
    numArrayList.Add(CTPowerBase.CalibrateFrame(sensor.SensorID, acc, channel, voltage, current));
    numArrayList.Add(sensor.ReadProfileConfig(29));
    return numArrayList;
  }

  public static void SetCalVal2FourthByte(Sensor sensor, byte value)
  {
    uint num = (uint) (Convert.ToInt32(sensor.Calibration2) & 16777215 /*0xFFFFFF*/);
    sensor.Calibration2 = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 24));
  }

  public static byte GetCalVal2FourthByte(Sensor sensor)
  {
    return (byte) Convert.ToUInt16(Convert.ToUInt32(sensor.Calibration2 & 4278190080L /*0xFF000000*/) >> 24);
  }

  public static void SetCalVal2BottomThirdByte(Sensor sensor, ushort value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration2) & 4278190080U /*0xFF000000*/;
    sensor.Calibration2 = (long) (num | (uint) (((int) value & 16777215 /*0xFFFFFF*/) << 16 /*0x10*/));
  }

  public static ushort GetCalVal2BottomThirdByte(Sensor sensor)
  {
    return Convert.ToUInt16(Convert.ToUInt32(sensor.Calibration2 & 16777215L /*0xFFFFFF*/));
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

  public static void SetHystThirdByte(Sensor sensor, byte value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4278255615U;
    sensor.Hysteresis = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static byte GetHystThirdByte(Sensor sensor)
  {
    return (byte) Convert.ToUInt16(Convert.ToUInt32(sensor.Hysteresis & 16711680L /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetHystSecondByte(Sensor sensor, byte value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4294902015U;
    sensor.Hysteresis = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 8));
  }

  public static byte GetHystSecondByte(Sensor sensor)
  {
    return (byte) Convert.ToUInt16(Convert.ToUInt32(sensor.Hysteresis & 65280L) >> 8);
  }

  public static void SetHystFirstByte(Sensor sensor, byte value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4294967040U;
    sensor.Hysteresis = (long) (num | (uint) value & (uint) byte.MaxValue);
  }

  public static byte GetHystFirstByte(Sensor sensor)
  {
    return (byte) Convert.ToUInt16(Convert.ToUInt32(sensor.Hysteresis & (long) byte.MaxValue));
  }

  public static void SetCalVal3Lower(Sensor sensor, ushort value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration3) & 4294901760U;
    sensor.Calibration3 = (long) (num | (uint) value);
  }

  public static ushort GetCalVal3Lower(Sensor sensor)
  {
    return (ushort) ((ulong) sensor.Calibration3 & (ulong) ushort.MaxValue);
  }

  public static void SetMinThreshByteThreeAndFour(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & (uint) ushort.MaxValue;
    sensor.MinimumThreshold = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int GetMinThreshByteThreeAndFour(Sensor sensor)
  {
    return Convert.ToInt32((sensor.MinimumThreshold & 4294901760L) >> 16 /*0x10*/);
  }

  public static void SetMinThreshByteOneAndTwo(Sensor sensor, int value)
  {
    uint uint32 = Convert.ToUInt32(sensor.MinimumThreshold & 4294901760L);
    sensor.MinimumThreshold = (long) (uint32 | (uint) value);
  }

  public static int GetMinThreshByteOneAndTwo(Sensor sensor)
  {
    return Convert.ToInt32(sensor.MinimumThreshold & (long) ushort.MaxValue);
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : (Convert.ToDouble(sensor.Hysteresis) / 1000.0).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : (Convert.ToDouble(sensor.MinimumThreshold) / 1000.0).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : (Convert.ToDouble(sensor.MaximumThreshold) / 1000.0).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(CTPower left, CTPower right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(CTPower left, CTPower right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(CTPower left, CTPower right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(CTPower left, CTPower right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(CTPower left, CTPower right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(CTPower left, CTPower right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is CTPower && this.Equals((MonnitApplicationBase) (obj as CTPower));
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
