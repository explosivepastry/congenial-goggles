// Decompiled with JetBrains decompiler
// Type: Monnit.LightSensor_PPFD
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

public class LightSensor_PPFD : MonnitApplicationBase
{
  private SensorAttribute _Label;

  public static long MonnitApplicationID => 139;

  public static string ApplicationName => "PPAF Light Meter";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => LightSensor_PPFD.MonnitApplicationID;

  public double PPFDReading { get; set; }

  public double PARDLI { get; set; }

  public double Temperature { get; set; }

  public double RawVoltage { get; set; }

  public int LightState { get; set; }

  public int stsState { get; set; }

  public static LightSensor_PPFD Create(byte[] sdm, int startIndex)
  {
    LightSensor_PPFD lightSensorPpfd = new LightSensor_PPFD();
    lightSensorPpfd.stsState = (int) sdm[startIndex - 1] >> 4;
    lightSensorPpfd.PPFDReading = BitConverter.ToUInt16(sdm, startIndex).ToDouble() / 10.0;
    lightSensorPpfd.LightState = (int) sdm[startIndex + 2];
    lightSensorPpfd.Temperature = BitConverter.ToInt16(sdm, startIndex + 3).ToDouble() / 10.0;
    lightSensorPpfd.RawVoltage = BitConverter.ToInt32(sdm, startIndex + 5).ToDouble() / 1000.0;
    try
    {
      lightSensorPpfd.PARDLI = BitConverter.ToUInt32(sdm, startIndex + 9).ToDouble() / 1000.0;
    }
    catch
    {
      lightSensorPpfd.PARDLI = 0.0;
    }
    return lightSensorPpfd;
  }

  public override string Serialize()
  {
    return $"{this.PPFDReading.ToString()}|{this.LightState.ToString()}|{this.Temperature.ToString()}|{this.RawVoltage.ToString()}|{this.PARDLI.ToString()}|{this.stsState.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) LightSensor_PPFD.Deserialize(version, serialized);
  }

  public static LightSensor_PPFD Deserialize(string version, string serialized)
  {
    LightSensor_PPFD lightSensorPpfd = new LightSensor_PPFD();
    if (string.IsNullOrEmpty(serialized))
    {
      lightSensorPpfd.PPFDReading = 0.0;
      lightSensorPpfd.LightState = 0;
      lightSensorPpfd.Temperature = 0.0;
      lightSensorPpfd.RawVoltage = 0.0;
      lightSensorPpfd.PARDLI = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      lightSensorPpfd.PPFDReading = strArray[0].ToDouble();
      if (strArray.Length > 1)
      {
        lightSensorPpfd.LightState = strArray[1].ToInt();
        try
        {
          lightSensorPpfd.Temperature = strArray[2].ToDouble();
          lightSensorPpfd.RawVoltage = strArray[3].ToDouble();
          if (strArray.Length > 4)
          {
            lightSensorPpfd.PARDLI = strArray[4].ToDouble();
            if (strArray.Length > 5)
              lightSensorPpfd.stsState = strArray[5].ToInt();
          }
        }
        catch
        {
        }
      }
      else
      {
        lightSensorPpfd.Temperature = strArray[0].ToDouble();
        lightSensorPpfd.RawVoltage = strArray[0].ToDouble();
        lightSensorPpfd.LightState = strArray[0].ToInt();
        lightSensorPpfd.PARDLI = strArray[0].ToDouble();
        lightSensorPpfd.stsState = 0;
      }
    }
    return lightSensorPpfd;
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[5]
      {
        new AppDatum(eDatumType.PPFDData, "PPFD", this.PPFDReading),
        new AppDatum(eDatumType.State, "Light State", this.LightState),
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temperature),
        new AppDatum(eDatumType.Voltage, "Voltage", this.RawVoltage),
        new AppDatum(eDatumType.PAR_DLI, "PAR DLI", this.PARDLI)
      });
    }
  }

  public SensorAttribute Label
  {
    get
    {
      if (this._Label == null)
        this._Label = new SensorAttribute()
        {
          Value = "PPFD",
          Name = nameof (Label)
        };
      return this._Label;
    }
  }

  public static string GetLabel(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        return sensorAttribute.Value;
    }
    return "PPFD";
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

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[5]
    {
      (object) this.PPFDReading,
      (object) this.LightState,
      (object) this.Temperature,
      (object) this.RawVoltage,
      (object) this.PARDLI
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[5]
    {
      "PPFD",
      "Light State",
      "Temperature",
      "Voltage",
      "PAR"
    });
  }

  public override object PlotValue => (object) this.PPFDReading;

  public override string NotificationString
  {
    get
    {
      string str1 = string.Empty;
      if ((this.stsState & 1) == 1)
        return "Comm Failure (Hardware Error)";
      if ((this.stsState & 4) == 4)
        return "Sensor Imbalance (Hardware Error)";
      if ((this.stsState & 2) == 2)
        str1 = "Temperature Out of Range - Temperature Compensation Disabled.";
      string str2 = string.Empty;
      if ((this.stsState & 8) == 8)
        str2 = " Daily Max";
      string str3 = "";
      switch (this.LightState)
      {
        case 0:
          str3 = "No Light";
          break;
        case 1:
          str3 = "Light Present";
          break;
        case 2:
          str3 = "Saturated Light";
          break;
      }
      return string.Format("PPFD: {0} μmol/m2/s, {1}, DLI: {3} mol/m2/day {4} {2}", (object) this.PPFDReading, (object) str3, (object) str1, (object) this.PARDLI, (object) str2);
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
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = LightSensor_PPFD.MonnitApplicationID,
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
    if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
    {
      long num = collection["Hysteresis_Manual"].ToLong();
      if (num < 0L)
        num = 0L;
      if (num > 40L)
        num = 40L;
      sensor.Hysteresis = num * 10L;
    }
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      sensor.MinimumThreshold = collection["MinimumThreshold_Manual"].ToLong() * 10L;
      if (sensor.MinimumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MinimumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      sensor.MaximumThreshold = collection["MaximumThreshold_Manual"].ToLong() * 10L;
      if (sensor.MaximumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MaximumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      if (sensor.MinimumThreshold > sensor.MaximumThreshold)
        sensor.MaximumThreshold = sensor.MinimumThreshold;
    }
    if (!string.IsNullOrEmpty(collection["EnableTemperatureCompensation"]))
    {
      bool o = collection["EnableTemperatureCompensation"].ToBool();
      LightSensor_PPFD.SetEnableTempCalibration(sensor, o.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["MeasurementInterval"]))
    {
      double reportInterval = collection["MeasurementInterval"].ToDouble();
      if (reportInterval > sensor.ReportInterval)
        reportInterval = sensor.ReportInterval;
      int num = (int) (reportInterval * 60.0);
      if (num <= 0)
        num = 1;
      LightSensor_PPFD.SetMeasurementInterval(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["MaximumDLIThreshold"]))
    {
      double num = collection["MaximumDLIThreshold"].ToDouble();
      if (num > 500.0)
        num = 500.0;
      if (num < 0.0)
        num = 0.0;
      LightSensor_PPFD.SetMaximumDLIThreshold(sensor, num);
    }
    if (string.IsNullOrEmpty(collection["DLIStartTime"]))
      return;
    int num1 = collection["DLIStartTime"].ToInt();
    if (num1 < 0)
      num1 = 0;
    if (num1 > 95)
      num1 = 95;
    LightSensor_PPFD.SetDLIStartTime(sensor, num1);
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (collection["CalibrationType"].Contains("0"))
      sensor.Calibration2 = (long) collection["lastmVReading"].ToInt();
    else if (sensor.Calibration3.ToBool())
    {
      double num = 1.0 + (collection["tempReading"].ToDouble() - 20.0) * 0.0011;
      sensor.Calibration1 = (collection["lastReading"].ToDouble() / num / collection["expectedReading"].ToDouble() * (double) (sensor.Calibration1 - sensor.Calibration2) + (double) sensor.Calibration2).ToLong();
    }
    else
      sensor.Calibration1 = (collection["lastReading"].ToDouble() / collection["expectedReading"].ToDouble() * (double) (sensor.Calibration1 - sensor.Calibration2) + (double) sensor.Calibration2).ToLong();
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

  public static void SetEnableTempCalibration(Sensor sensor, int value)
  {
    uint num = (uint) sensor.Calibration3 & 4294967040U;
    sensor.Calibration3 = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetEnableTempCalibration(Sensor sensor)
  {
    return (int) (uint) sensor.Calibration3 & (int) byte.MaxValue;
  }

  public static void SetDLIStartTime(Sensor sensor, int value)
  {
    if (value > 95)
      value = 95;
    if (value < 0)
      value = 0;
    uint num = (uint) sensor.Calibration3 & 4294902015U;
    sensor.Calibration3 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static int GetDLIStartTime(Sensor sensor)
  {
    int dliStartTime = (int) (((uint) sensor.Calibration3 & 65280U) >> 8);
    if (dliStartTime > 95)
      dliStartTime = 95;
    if (dliStartTime < 0)
      dliStartTime = 0;
    return dliStartTime;
  }

  public static void SetMeasurementInterval(Sensor sensor, int value)
  {
    uint num = (uint) sensor.Calibration3 & (uint) ushort.MaxValue;
    sensor.Calibration3 = (long) (num | (uint) ((value & (int) ushort.MaxValue) << 16 /*0x10*/));
  }

  public static int GetMeasurementInterval(Sensor sensor)
  {
    return (int) (((uint) sensor.Calibration3 & 4294901760U) >> 16 /*0x10*/);
  }

  public static void SetMaximumDLIThreshold(Sensor sensor, double value)
  {
    sensor.Calibration4 = (long) (uint) (value * 1000.0);
  }

  public static double GetMaximumDLIThreshold(Sensor sensor)
  {
    return (double) sensor.Calibration4 / 1000.0;
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : ((double) sensor.Hysteresis / 10.0).ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : ((double) sensor.MinimumThreshold / 10.0).ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : ((double) sensor.MaximumThreshold / 10.0).ToString();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(LightSensor_PPFD left, LightSensor_PPFD right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(LightSensor_PPFD left, LightSensor_PPFD right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(LightSensor_PPFD left, LightSensor_PPFD right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(LightSensor_PPFD left, LightSensor_PPFD right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(LightSensor_PPFD left, LightSensor_PPFD right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(LightSensor_PPFD left, LightSensor_PPFD right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is LightSensor_PPFD && this.Equals((MonnitApplicationBase) (obj as LightSensor_PPFD));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is LightSensor_PPFD && this.PPFDReading == (right as LightSensor_PPFD).PPFDReading;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is LightSensor_PPFD && this.PPFDReading != (right as LightSensor_PPFD).PPFDReading;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is LightSensor_PPFD && this.PPFDReading < (right as LightSensor_PPFD).PPFDReading;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is LightSensor_PPFD && this.PPFDReading <= (right as LightSensor_PPFD).PPFDReading;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is LightSensor_PPFD && this.PPFDReading > (right as LightSensor_PPFD).PPFDReading;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is LightSensor_PPFD && this.PPFDReading >= (right as LightSensor_PPFD).PPFDReading;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 4000;

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
  }
}
