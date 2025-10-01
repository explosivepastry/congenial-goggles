// Decompiled with JetBrains decompiler
// Type: Monnit.CO2Meter
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class CO2Meter : MonnitApplicationBase
{
  private SensorAttribute _QueueID;
  private SensorAttribute _CalibrationValues;

  public static long MonnitApplicationID => 106;

  public static string ApplicationName => "CO2 Meter";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => CO2Meter.MonnitApplicationID;

  public ushort Instantaneous { get; set; }

  public ushort TWA { get; set; }

  public ushort Raw { get; set; }

  public int stsState { get; set; }

  public static void SetDisplay(long sensorID, eCO2Display display)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Display")
      {
        if (sensorAttribute.Value != display.ToString())
        {
          sensorAttribute.Value = display.ToString();
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "Display",
        Value = display.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public override string Serialize()
  {
    return $"{this.Instantaneous.ToString()}|{this.TWA.ToString()}|{this.stsState.ToString()}|{this.Raw.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.PPM, "Instantaneous", (int) this.Instantaneous),
        new AppDatum(eDatumType.PPM, "TWA", (int) this.TWA)
      });
    }
  }

  public override object PlotValue
  {
    get
    {
      int instantaneous = (int) this.Instantaneous;
      return true ? (object) this.Instantaneous : (object) null;
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      (object) this.Instantaneous,
      (object) this.TWA
    };
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>() { "Instantaneous", "TWA" };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) CO2Meter.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      if (this.stsState > 0)
      {
        switch (this.stsState)
        {
          case 1:
            return $"CO2 Module Timeout Error. CO2 Instantaneous: {this.Instantaneous} ppm; CO2 TWA: {this.TWA} ppm";
          case 2:
            return $"CO2 Module Comm Error. CO2 Instantaneous: {this.Instantaneous} ppm; CO2 TWA: {this.TWA} ppm";
          case 3:
            return "CO2 Sensor Initializing";
          case 4:
            return $"Range Error! CO2 Instantaneous: {this.Instantaneous} ppm; CO2 TWA: {this.TWA} ppm";
          case 6:
            return $"Calibration Failure. CO2 Instantaneous: {this.Instantaneous} ppm; CO2 TWA: {this.TWA} ppm";
          case 7:
            return $"Calibration Success. CO2 Instantaneous: {this.Instantaneous} ppm; CO2 TWA: {this.TWA} ppm";
        }
      }
      return $"CO2 Instantaneous: {this.Instantaneous} ppm; CO2 TWA: {this.TWA} ppm";
    }
  }

  public static CO2Meter Deserialize(string version, string serialized)
  {
    CO2Meter co2Meter = new CO2Meter();
    if (string.IsNullOrEmpty(serialized))
    {
      co2Meter.Instantaneous = (ushort) 0;
      co2Meter.TWA = (ushort) 0;
      co2Meter.stsState = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      co2Meter.Instantaneous = strArray[0].ToUInt16();
      if (strArray.Length > 1)
      {
        co2Meter.TWA = strArray[1].ToUInt16();
        try
        {
          co2Meter.stsState = strArray[2].ToInt();
        }
        catch
        {
          co2Meter.stsState = 0;
        }
        if (strArray.Length > 3)
        {
          try
          {
            co2Meter.Raw = strArray[3].ToUInt16();
          }
          catch
          {
            co2Meter.Raw = (ushort) 0;
          }
        }
      }
      else
        co2Meter.TWA = strArray[0].ToUInt16();
    }
    return co2Meter;
  }

  public static CO2Meter Create(byte[] sdm, int startIndex)
  {
    CO2Meter co2Meter = new CO2Meter();
    co2Meter.stsState = (int) sdm[startIndex - 1] >> 4;
    co2Meter.Instantaneous = BitConverter.ToUInt16(sdm, startIndex);
    co2Meter.TWA = BitConverter.ToUInt16(sdm, startIndex + 2);
    if (sdm.Length - startIndex >= 6)
      co2Meter.Raw = BitConverter.ToUInt16(sdm, startIndex + 4);
    return co2Meter;
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
    try
    {
      if (sensor.MaximumThreshold == (long) uint.MaxValue)
      {
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      }
      else
      {
        int num = (int) ((long) (int) sensor.MaximumThreshold & 4294901760L);
        sensor.MaximumThreshold = (long) (num | 10000);
      }
    }
    catch
    {
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    }
    try
    {
      if (sensor.MinimumThreshold == (long) uint.MaxValue)
      {
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      }
      else
      {
        int num = (int) ((long) (int) sensor.MinimumThreshold & 4294901760L);
        sensor.MinimumThreshold = (long) (num | 10000);
      }
    }
    catch
    {
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    }
    try
    {
      if (sensor.Calibration3 == (long) uint.MaxValue)
      {
        sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
      }
      else
      {
        uint num = Convert.ToUInt32(sensor.Calibration3) & (uint) ushort.MaxValue;
        sensor.Calibration3 = (long) (num | 19202048U /*0x01250000*/);
      }
    }
    catch
    {
      sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    }
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
    sensor.EventDetectionType = sensor.DefaultValue<int>("DefaultEventDetectionType");
    sensor.EventDetectionPeriod = sensor.DefaultValue<int>("DefaultEventDetectionPeriod");
    sensor.EventDetectionCount = sensor.DefaultValue<int>("DefaultEventDetectionCount");
    sensor.RearmTime = sensor.DefaultValue<int>("DefaultRearmTime");
    sensor.BiStable = sensor.DefaultValue<int>("DefaultBiStable");
  }

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.ProfileConfigDirty = true;
    CO2Meter.SetSpanCalibrationValue(sensor, 10526);
    CO2Meter.SetC02Offset(sensor, 0);
    CO2Meter.SetAltitude(sensor, 0);
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
      ApplicationID = CO2Meter.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void VerifyNotificationValues(Notification notification, string scale)
  {
    int num = notification.CompareValue.ToInt();
    if (num < 0)
      notification.CompareValue = "0";
    if (num <= 10000)
      return;
    notification.CompareValue = "10000";
  }

  public SensorAttribute QueueID => this._QueueID;

  public static int GetQueueID(long sensorID)
  {
    SensorAttribute.ResetAttributeList(sensorID);
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "QueueID")
        return sensorAttribute.Value.ToInt();
    }
    return 1;
  }

  public static void SetQueueID(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "QueueID")
      {
        sensorAttribute.Value = (sensorAttribute.Value.ToInt() + 1).ToString();
        if (sensorAttribute.Value.ToInt() > 254)
          sensorAttribute.Value = "1";
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "QueueID",
        Value = "1",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute CalibrationValues => this._CalibrationValues;

  public static string GetCalibrationValues(long sensorID)
  {
    SensorAttribute.ResetAttributeList(sensorID);
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CalibrationValues")
        return sensorAttribute.Value;
    }
    return "";
  }

  public static void SetCalibrationValues(
    long sensorID,
    double actual,
    double observed,
    int subcommand,
    int queueID)
  {
    string str = $"{actual.ToString()}|{observed.ToString()}|{queueID.ToString()}|{subcommand.ToString()}";
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CalibrationValues")
      {
        sensorAttribute.Value = str.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "CalibrationValues",
        Value = str.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CalibrationValues")
        this._CalibrationValues = sensorAttribute;
      if (sensorAttribute.Name == "QueueID")
        this._QueueID = sensorAttribute;
    }
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["Display"]))
    {
      eCO2Display result;
      if (!Enum.TryParse<eCO2Display>(collection["Display"], out result))
        result = eCO2Display.Instantaneous;
      viewData["TempScale"] = result.ToString();
      CO2Meter.SetDisplay(sensor.SensorID, result);
    }
    if (!string.IsNullOrEmpty(collection["instantaneousThreshold_Manual"]))
      CO2Meter.SetInstantaneousThreshold(sensor, (int) Convert.ToInt16(collection["instantaneousThreshold_Manual"]));
    if (!string.IsNullOrEmpty(collection["instantaneousBuffer_Manual"]))
      CO2Meter.SetInstantaneousBuffer(sensor, (int) Convert.ToInt16(collection["instantaneousBuffer_Manual"]));
    if (!string.IsNullOrEmpty(collection["twaThreshold_Manual"]))
      CO2Meter.SetTWAThreshold(sensor, collection["twaThreshold_Manual"].ToInt());
    if (!string.IsNullOrEmpty(collection["twaBuffer_Manual"]))
      CO2Meter.SetTWABuffer(sensor, collection["twaBuffer_Manual"].ToInt());
    if (!string.IsNullOrEmpty(collection["measurementInterval_Manual"]))
    {
      double num = collection["measurementInterval_Manual"].ToDouble();
      CO2Meter.SetMeasurementInterval(sensor, num);
      if (sensor.ActiveStateInterval < num)
        sensor.ActiveStateInterval = num;
      if (sensor.ReportInterval < sensor.ActiveStateInterval)
        sensor.ReportInterval = sensor.ActiveStateInterval;
    }
    if (!string.IsNullOrEmpty(collection["enableAutoCalibration_Manual"]))
      CO2Meter.SetEnableAutoCalibration(sensor, collection["enableAutoCalibration_Manual"].ToInt());
    if (string.IsNullOrEmpty(collection["autoZeroInterval_Manual"]))
      return;
    CO2Meter.SetAutoZeroInterval(sensor, collection["autoZeroInterval_Manual"].ToDouble());
  }

  public static void SetInstantaneousBuffer(Sensor sens, int value)
  {
    int num1 = (int) ((long) (int) sens.Hysteresis & 4294901760L);
    int instantaneousThreshold = CO2Meter.GetInstantaneousThreshold(sens);
    if (value < 0)
      value = 0;
    int num2 = instantaneousThreshold / 2 < value ? instantaneousThreshold / 2 : value;
    sens.Hysteresis = (long) (num1 | (int) (ushort) num2);
  }

  public static int GetInstantaneousBuffer(Sensor sens)
  {
    return (int) (short) (ushort) ((uint) (int) sens.Hysteresis & (uint) ushort.MaxValue);
  }

  public static void SetInstantaneousThreshold(Sensor sens, int value)
  {
    if (value < 0)
      value = 0;
    if (value > 10000)
      value = 10000;
    int num = (int) ((long) (int) sens.MinimumThreshold & 4294901760L);
    sens.MinimumThreshold = (long) (num | (int) (ushort) value);
  }

  public static int GetInstantaneousThreshold(Sensor sens)
  {
    return (int) ((uint) ((int) (uint) sens.MinimumThreshold & (int) ushort.MaxValue)).ToInt().ToUInt16();
  }

  public static int GetSpanCalibrationValue(Sensor sensor)
  {
    return Convert.ToInt32((sensor.MinimumThreshold & 4294901760L) >> 16 /*0x10*/);
  }

  public static void SetSpanCalibrationValue(Sensor sensor, int value)
  {
    uint num = (uint) (Convert.ToInt32(sensor.MinimumThreshold) & (int) ushort.MaxValue);
    sensor.MinimumThreshold = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static void SetTWABuffer(Sensor sens, int value)
  {
    int num1 = (int) sens.Hysteresis & (int) ushort.MaxValue;
    int twaThreshold = CO2Meter.GetTWAThreshold(sens);
    if (value < 0)
      value = 0;
    int num2 = twaThreshold / 2 < value ? twaThreshold / 2 : value;
    sens.Hysteresis = (long) (num1 | (int) (ushort) num2 << 16 /*0x10*/);
  }

  public static int GetTWABuffer(Sensor sens)
  {
    return (int) (short) (ushort) ((int) sens.Hysteresis >> 16 /*0x10*/).ToInt();
  }

  public static void SetTWAThreshold(Sensor sens, int value)
  {
    if (value < 0)
      value = 0;
    if (value > 10000)
      value = 10000;
    int num = (int) ((long) (int) sens.MaximumThreshold & 4294901760L);
    sens.MaximumThreshold = (long) (num | (int) (ushort) value);
  }

  public static int GetTWAThreshold(Sensor sens)
  {
    return (int) ((uint) ((int) (uint) sens.MaximumThreshold & (int) ushort.MaxValue)).ToInt().ToUInt16();
  }

  public static int GetC02Offset(Sensor sensor)
  {
    return (int) (short) ((sensor.MaximumThreshold & 4294901760L) >> 16 /*0x10*/);
  }

  public static void SetC02Offset(Sensor sensor, int value)
  {
    uint num = (uint) (Convert.ToInt32(sensor.MaximumThreshold) & (int) ushort.MaxValue);
    sensor.MaximumThreshold = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static void SetMeasurementInterval(Sensor sens, double value)
  {
    if (value < 1.0)
      value = 1.0;
    value = Math.Round(value * 60.0, 0);
    int num = (int) ((long) (int) sens.Calibration1 & 4294901760L);
    sens.Calibration1 = (long) (num | (int) (ushort) value);
  }

  public static double GetMeasurementInterval(Sensor sens)
  {
    return Math.Round(((uint) ((int) (uint) sens.Calibration1 & (int) ushort.MaxValue)).ToDouble() / 60.0, 2);
  }

  public static void SetDigitalFilter(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4278255615U;
    sensor.Calibration1 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static int GetDigitalFilter(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.Calibration1 & 16711680L /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static int GetAutoZeroValue(Sensor sensor)
  {
    return Convert.ToInt32((sensor.Calibration2 & 4294901760L) >> 16 /*0x10*/);
  }

  public static void SetAutoZeroValue(Sensor sensor, int value)
  {
    uint num = (uint) (Convert.ToInt32(sensor.Calibration2) & (int) ushort.MaxValue);
    sensor.Calibration2 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static double GetAutoZeroInterval(Sensor sens)
  {
    return Math.Round(((uint) ((int) (uint) sens.Calibration2 & (int) ushort.MaxValue)).ToDouble() / 24.0, 2);
  }

  public static void SetAutoZeroInterval(Sensor sens, double value)
  {
    value *= 24.0;
    int num = (int) ((long) (int) sens.Calibration2 & 4294901760L);
    sens.Calibration2 = (long) (num | (int) (ushort) value);
  }

  public static int GetTemperatureZeroPoint(Sensor sensor)
  {
    return Convert.ToInt32((sensor.Calibration3 & 4294901760L) >> 16 /*0x10*/);
  }

  public static void SetTemperatureZeroPoint(Sensor sensor, int value)
  {
    uint num = (uint) (Convert.ToInt32(sensor.Calibration3) & (int) ushort.MaxValue);
    sensor.Calibration3 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int GetAltitude(Sensor sensor)
  {
    return (int) (short) Convert.ToInt32(sensor.Calibration3 & (long) ushort.MaxValue);
  }

  public static void SetAltitude(Sensor sensor, int value)
  {
    if (value < -5000)
      value = -5000;
    if (value > 20000)
      value = 20000;
    int num = (int) ((long) Convert.ToInt32(sensor.Calibration3) & 4294901760L);
    sensor.Calibration3 = (long) (num | value);
  }

  public static int GetHardwareFaultTime(Sensor sens)
  {
    return ((uint) ((int) (uint) sens.Calibration4 & (int) ushort.MaxValue)).ToInt();
  }

  public static void SetHardwareFaultTime(Sensor sens, int value)
  {
    int num = (int) ((long) (int) sens.Calibration1 & 4294901760L);
    sens.Calibration4 = (long) (num | (int) (ushort) value);
  }

  public static void SetEnableAutoCalibration(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration4) & 16777215U /*0xFFFFFF*/;
    sensor.Calibration4 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 24));
  }

  public static int GetEnableAutoCalibration(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.Calibration4 & 4278190080L /*0xFF000000*/) >> 24);
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    int actual = 0;
    int observed = 0;
    int subcommand = 0;
    if (collection["calType"] == "1")
    {
      subcommand = 3;
      actual = collection["actual"].ToInt();
      observed = collection["observed"].ToInt();
      if (actual > 10000)
        actual = 10000;
      if (actual < 0)
        actual = 0;
    }
    else if (collection["calType"] == "2")
    {
      subcommand = 4;
      actual = collection["altitude"].ToInt();
      observed = collection["observedAltitude"].ToInt();
      if (actual > 20000)
        actual = 20000;
      if (actual < -5000)
        actual = -5000;
    }
    else if (collection["calType"] == "3")
    {
      subcommand = 5;
      actual = 0;
      observed = 0;
    }
    else if (collection["calType"] == "4")
    {
      subcommand = 6;
      actual = collection["actualC02"].ToInt();
      observed = collection["observedC02"].ToInt();
      if (actual > 10000)
        actual = 10000;
      if (actual < 0)
        actual = 0;
    }
    else if (collection["calType"] == "0")
    {
      CO2Meter.SetC02Offset(sensor, 0);
      sensor.Save();
      return;
    }
    CO2Meter.SetQueueID(sensor.SensorID);
    CO2Meter.SetCalibrationValues(sensor.SensorID, (double) actual, (double) observed, subcommand, CO2Meter.GetQueueID(sensor.SensorID));
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      string[] strArray = CO2Meter.GetCalibrationValues(sensor.SensorID).Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      numArrayList.Add(CO2MeterBase.CalibrateFrame(sensor.SensorID, strArray[0].ToInt(), strArray[1].ToInt(), strArray[2].ToInt(), strArray[3].ToInt()));
      if (strArray[3].ToInt() == 3 || strArray[3].ToInt() == 6)
        numArrayList.Add(sensor.ReadProfileConfig(28));
      else
        numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(CO2Meter left, CO2Meter right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(CO2Meter left, CO2Meter right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(CO2Meter left, CO2Meter right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(CO2Meter left, CO2Meter right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(CO2Meter left, CO2Meter right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(CO2Meter left, CO2Meter right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is CO2Meter && this.Equals((MonnitApplicationBase) (obj as CO2Meter));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is CO2Meter && this.PlotValue.ToInt() == (right as CO2Meter).PlotValue.ToInt();
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is CO2Meter && this.PlotValue.ToInt() != (right as CO2Meter).PlotValue.ToInt();
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is CO2Meter && this.PlotValue.ToInt() < (right as CO2Meter).PlotValue.ToInt();
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is CO2Meter && this.PlotValue.ToInt() <= (right as CO2Meter).PlotValue.ToInt();
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is CO2Meter && this.PlotValue.ToInt() > (right as CO2Meter).PlotValue.ToInt();
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is CO2Meter && this.PlotValue.ToInt() >= (right as CO2Meter).PlotValue.ToInt();
  }

  public static long InstantaneousDefaultMinThreshold => 0;

  public static long InstantaneousDefaultMaxThreshold => 10000;

  public static long AverageDefaultMinThreshold => 0;

  public static long AverageDefaultMaxThreshold => 10000;

  public static long MeasurementIntervalCalVal1 => 60;

  public static long AutozeroIntervalCalVal2 => 3;
}
