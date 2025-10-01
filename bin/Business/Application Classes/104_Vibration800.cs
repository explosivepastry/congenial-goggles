// Decompiled with JetBrains decompiler
// Type: Monnit.Vibration800
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;

#nullable disable
namespace Monnit;

public class Vibration800 : MonnitApplicationBase, ISensorAttribute
{
  internal SensorAttribute _CorF;
  private SensorAttribute _ShowFullDataValue;

  public static long MonnitApplicationID => 104;

  public static string ApplicationName => nameof (Vibration800);

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => Vibration800.MonnitApplicationID;

  public int FundamentalFrequency { get; set; }

  public double Vibration { get; set; }

  public double Displacement { get; set; }

  public double CrestFactor { get; set; }

  public double PeakHarmonic { get; set; }

  public double RmsHarmonic { get; set; }

  public int DutyCycle { get; set; }

  public double Temperature { get; set; }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[8]
      {
        new AppDatum(eDatumType.Frequency, "FundamentalFrequency", this.FundamentalFrequency),
        new AppDatum(eDatumType.Frequency, "Vibration", this.Vibration),
        new AppDatum(eDatumType.Millimeter, "Displacement", this.Displacement),
        new AppDatum(eDatumType.CrestFactor, "CrestFactor", this.CrestFactor),
        new AppDatum(eDatumType.Geforce, "PeakHarmonic", this.PeakHarmonic),
        new AppDatum(eDatumType.Geforce, "RmsHarmonic", this.RmsHarmonic),
        new AppDatum(eDatumType.Percentage, "DutyCycle", this.DutyCycle),
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temperature)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[8]
    {
      (object) this.FundamentalFrequency,
      (object) this.Vibration,
      (object) this.Displacement,
      (object) this.CrestFactor,
      (object) this.PeakHarmonic,
      (object) this.RmsHarmonic,
      (object) this.DutyCycle,
      this.PlotTemperatureValue
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[8]
    {
      "Fundamental Frequency",
      "Vibration",
      "Displacement",
      "CrestFactor",
      "PeakHarmonic",
      "RmsHarmonic",
      "DutyCycle",
      Vibration800.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    });
  }

  public override string Serialize()
  {
    return $"{this.FundamentalFrequency.ToString()}|{this.Vibration.ToString()}|{this.Displacement.ToString()}|{this.CrestFactor.ToString()}|{this.PeakHarmonic.ToString()}|{this.RmsHarmonic.ToString()}|{this.DutyCycle.ToString()}|{this.Temperature.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Vibration800.Deserialize(version, serialized);
  }

  public static bool IsFahrenheit(long sensorID) => Monnit.Temperature.IsFahrenheit(sensorID);

  public static void MakeFahrenheit(long sensorID) => Monnit.Temperature.MakeFahrenheit(sensorID);

  public static void MakeCelsius(long sensorID) => Monnit.Temperature.MakeCelsius(sensorID);

  public SensorAttribute CorF => this._CorF;

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
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
      if (sensorAttribute.Name == "ShowFullDataValue")
        this._ShowFullDataValue = sensorAttribute;
    }
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

  public override string NotificationString
  {
    get
    {
      if (!(this.ShowFullDataValue.Value.ToLower() == "true"))
        return $"Fundamental Frequency {this.PlotValue.ToDouble()} Hz";
      string empty = string.Empty;
      string str = this.CorF == null || !(this.CorF.Value == "C") ? this.Temperature.ToFahrenheit().ToString("##.#° F", (IFormatProvider) CultureInfo.InvariantCulture) : this.Temperature.ToString("##.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
      if (this.Temperature.ToString() == "-9999")
        str = "Thermistor Shorted or Open";
      object[] objArray = new object[8];
      objArray[0] = (object) this.PlotValue.ToDouble();
      double num = this.Vibration;
      objArray[1] = (object) num.ToString("0.0#");
      num = this.Displacement;
      objArray[2] = (object) num.ToString("0.00#");
      num = this.CrestFactor;
      objArray[3] = (object) num.ToString("0.0#");
      num = this.PeakHarmonic;
      objArray[4] = (object) num.ToString("0.00#");
      num = this.RmsHarmonic;
      objArray[5] = (object) num.ToString("0.00#");
      objArray[6] = (object) this.DutyCycle;
      objArray[7] = (object) str;
      return string.Format("Fundamental Frequency {0} Hz,  Vibration {1} mm/s, Displacement {2} mm, CrestFactor {3} , PeakHarmonic {4} g,  RmsHarmonic {5} g,  DutyCycle {6} %, Temperature {7}", objArray);
    }
  }

  public override object PlotValue => (object) this.FundamentalFrequency;

  public object PlotTemperatureValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.Temperature : (object) this.Temperature.ToFahrenheit();
    }
  }

  public static Vibration800 Deserialize(string version, string serialized)
  {
    Vibration800 vibration800 = new Vibration800();
    if (string.IsNullOrEmpty(serialized))
    {
      vibration800.FundamentalFrequency = 0;
      vibration800.Vibration = 0.0;
      vibration800.Displacement = 0.0;
      vibration800.CrestFactor = 0.0;
      vibration800.PeakHarmonic = 0.0;
      vibration800.RmsHarmonic = 0.0;
      vibration800.DutyCycle = 0;
      vibration800.Temperature = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      vibration800.FundamentalFrequency = strArray[0].ToInt();
      if (strArray.Length > 1)
      {
        try
        {
          vibration800.Vibration = strArray[1].ToDouble();
          vibration800.Displacement = strArray[2].ToDouble();
          vibration800.CrestFactor = strArray[3].ToDouble();
          vibration800.PeakHarmonic = strArray[4].ToDouble();
          vibration800.RmsHarmonic = strArray[5].ToDouble();
          vibration800.DutyCycle = strArray[6].ToInt();
          vibration800.Temperature = strArray[7].ToDouble();
        }
        catch
        {
          vibration800.Vibration = 0.0;
          vibration800.Displacement = 0.0;
          vibration800.CrestFactor = 0.0;
          vibration800.PeakHarmonic = 0.0;
          vibration800.RmsHarmonic = 0.0;
          vibration800.DutyCycle = 0;
          vibration800.Temperature = 0.0;
        }
      }
    }
    return vibration800;
  }

  public static Vibration800 Create(byte[] sdm, int startIndex)
  {
    Vibration800 vibration800 = new Vibration800();
    int num = (int) sdm[startIndex - 1];
    if ((num & 16 /*0x10*/) == 16 /*0x10*/)
    {
      vibration800.FundamentalFrequency = 0;
      vibration800.Vibration = 0.0;
      vibration800.Displacement = 0.0;
      vibration800.CrestFactor = 0.0;
      vibration800.PeakHarmonic = 0.0;
      vibration800.RmsHarmonic = 0.0;
      vibration800.DutyCycle = 0;
    }
    else
    {
      vibration800.FundamentalFrequency = (int) BitConverter.ToUInt16(sdm, startIndex);
      vibration800.Vibration = BitConverter.ToUInt16(sdm, startIndex + 2).ToDouble() / 100.0;
      vibration800.Displacement = BitConverter.ToInt16(sdm, startIndex + 4).ToDouble() / 100.0;
      vibration800.CrestFactor = BitConverter.ToUInt16(sdm, startIndex + 6).ToDouble() / 100.0;
      vibration800.PeakHarmonic = BitConverter.ToUInt16(sdm, startIndex + 8).ToDouble() / 1000.0;
      vibration800.RmsHarmonic = BitConverter.ToUInt16(sdm, startIndex + 10).ToDouble() / 1000.0;
      vibration800.DutyCycle = (int) sdm[startIndex + 12];
    }
    vibration800.Temperature = (num & 32 /*0x20*/) != 32 /*0x20*/ ? BitConverter.ToInt16(sdm, startIndex + 13).ToDouble() / 10.0 : -9999.0;
    return vibration800;
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
      ApplicationID = Vibration800.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["FundamentalInterval_Manual"]))
    {
      double o = collection["FundamentalInterval_Manual"].ToDouble() * 60.0;
      if (o < 1.0)
        o = 1.0;
      if (o > 43200.0)
        o = 43200.0;
      Vibration800.SetFundamentalInterval(sensor, o.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["VibrationSensitivity_Manual"]))
    {
      double o = collection["VibrationSensitivity_Manual"].ToDouble() * 1000.0;
      if (o < 0.0)
        o = 0.0;
      if (o > 32000.0)
        o = 32000.0;
      Vibration800.SetVibrationSensitivityThreshold(sensor, o.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["AxisMode_Manual"]))
    {
      int o = collection["AxisMode_Manual"].ToInt();
      if (o < 1)
        o = 1;
      if (o > 7)
        o = 7;
      Vibration800.SetAxisMode(sensor, o.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["FundFreqResolution_Manual"]))
    {
      int o = collection["FundFreqResolution_Manual"].ToInt();
      if (o < 0)
        o = 0;
      if (o > 2)
        o = 2;
      Vibration800.SetFundFreqResolution(sensor, o.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["FundWindowing_Manual"]))
    {
      int o = collection["FundWindowing_Manual"].ToInt();
      if (o < 0)
        o = 0;
      if (o > 2)
        o = 2;
      Vibration800.SetFundWindowing(sensor, o.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["HarmonicFreqResolution_Manual"]))
    {
      int o = collection["HarmonicFreqResolution_Manual"].ToInt();
      if (o < 0)
        o = 0;
      if (o > 2)
        o = 2;
      Vibration800.SetHarmonicFreqResolution(sensor, o.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["HarmonicWindowing_Manual"]))
    {
      int o = collection["HarmonicWindowing_Manual"].ToInt();
      if (o < 0)
        o = 0;
      if (o > 2)
        o = 2;
      Vibration800.SetHarmonicWindowing(sensor, o.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["HarmonicMin_Manual"]) && !string.IsNullOrEmpty(collection["HarmonicMax_Manual"]))
    {
      int o1 = collection["HarmonicMin_Manual"].ToInt();
      if (o1 < 50)
        o1 = 50;
      if (o1 > 6000)
        o1 = 6000;
      int o2 = collection["HarmonicMax_Manual"].ToInt();
      if (o2 < 50)
        o2 = 50;
      if (o2 > 6000)
        o2 = 6000;
      if (o2 < o1)
        o2 = o1;
      if (o1 > o2)
        o1 = o2;
      Vibration800.SetHarmonicMin(sensor, o1.ToInt());
      Vibration800.SetHarmonicMax(sensor, o2.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["harmMinThreshold_Manual"]) && !string.IsNullOrEmpty(collection["harmMaxThreshold_Manual"]))
    {
      int o3 = collection["harmMinThreshold_Manual"].ToInt();
      if (o3 < 50)
        o3 = 50;
      if (o3 > 6000)
        o3 = 6000;
      int o4 = collection["harmMaxThreshold_Manual"].ToInt();
      if (o4 < 50)
        o4 = 50;
      if (o4 > 6000)
        o4 = 6000;
      if (o4 < o3)
        o4 = o3;
      if (o3 > o4)
        o3 = o4;
      Vibration800.SetHarmonicMin(sensor, o3.ToInt());
      Vibration800.SetHarmonicMax(sensor, o4.ToInt());
    }
    bool showFullData = false;
    if (!string.IsNullOrEmpty(collection["FullNotiString"]))
      showFullData = collection["FullNotiString"].ToString() == "1" || collection["FullNotiString"].ToString().ToLower() == "true";
    Vibration800.SetShowFullDataValue(sensor.SensorID, showFullData);
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : ((sensor.Hysteresis >> 16 /*0x10*/).ToDouble() / 100.0).ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : ((sensor.MinimumThreshold >> 16 /*0x10*/).ToDouble() / 100.0).ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : ((sensor.MaximumThreshold >> 16 /*0x10*/).ToDouble() / 100.0).ToString();
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    NonCachedAttribute nonCachedAttribute = NonCachedAttribute.LoadBySensorIDAndName(sensor.SensorID, "Calibration");
    if (nonCachedAttribute == null)
    {
      nonCachedAttribute = new NonCachedAttribute();
      nonCachedAttribute.SensorID = sensor.SensorID;
      nonCachedAttribute.Name = "Calibration";
    }
    nonCachedAttribute.Value1 = string.IsNullOrWhiteSpace(collection["calType"]) ? "3" : collection["calType"];
    if (!string.IsNullOrWhiteSpace(collection["actual"]))
    {
      if (collection["tempScale"] == "C")
        nonCachedAttribute.Value2 = collection["actual"].ToDouble() >= -40.0 && collection["actual"].ToDouble() <= 125.0 ? (collection["actual"].ToDouble() * 10.0).ToInt().ToString() : throw new Exception("Calibration value out of range!");
      else
        nonCachedAttribute.Value2 = collection["actual"].ToDouble() >= -40.0.ToFahrenheit() && collection["actual"].ToDouble() <= 125.0.ToFahrenheit() ? ((collection["actual"].ToDouble() - 32.0) * (5.0 / 9.0) * 10.0).ToInt().ToString() : throw new Exception("Calibration value out of range!");
    }
    nonCachedAttribute.Save();
    sensor.ProfileConfig2Dirty = false;
    sensor.ProfileConfigDirty = false;
    sensor.PendingActionControlCommand = true;
    sensor.Save();
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    NonCachedAttribute nonCachedAttribute = NonCachedAttribute.LoadBySensorIDAndName(sensor.SensorID, "Calibration");
    List<byte[]> numArrayList = new List<byte[]>();
    if (nonCachedAttribute.Value1 == "1")
      numArrayList.Add(Vibration800Base.CalibrateFrame(sensor.SensorID, (double) nonCachedAttribute.Value2.ToInt()));
    else if (nonCachedAttribute.Value1 == "2")
      numArrayList.Add(Vibration800Base.BaselinesCalibrateFrame(sensor.SensorID));
    numArrayList.Add(sensor.ReadProfileConfig(29));
    return numArrayList;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Vibration800 left, Vibration800 right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Vibration800 left, Vibration800 right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Vibration800 left, Vibration800 right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Vibration800 left, Vibration800 right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Vibration800 left, Vibration800 right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Vibration800 left, Vibration800 right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Vibration800 && this.Equals((MonnitApplicationBase) (obj as Vibration800));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Vibration800 && this.FundamentalFrequency == (right as Vibration800).FundamentalFrequency;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Vibration800 && this.FundamentalFrequency != (right as Vibration800).FundamentalFrequency;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Vibration800 && this.FundamentalFrequency < (right as Vibration800).FundamentalFrequency;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Vibration800 && this.FundamentalFrequency <= (right as Vibration800).FundamentalFrequency;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Vibration800 && this.FundamentalFrequency > (right as Vibration800).FundamentalFrequency;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Vibration800 && this.FundamentalFrequency >= (right as Vibration800).FundamentalFrequency;
  }

  public static void SetFundamentalInterval(Sensor sens, int value)
  {
    uint num = (uint) sens.Hysteresis & 4294901760U;
    sens.Hysteresis = (long) (num | (uint) (ushort) value);
  }

  public static double GetFundamentalInterval(Sensor sens)
  {
    return Math.Round(((double) (sens.Hysteresis & (long) ushort.MaxValue) / 60.0).ToDouble(), 3);
  }

  public static void SetTempThreshMin(Sensor sens, int value)
  {
    int num = (int) ((long) (int) sens.MinimumThreshold & 4294901760L);
    sens.MinimumThreshold = (long) (num | (int) (ushort) value);
  }

  public static int GetTempThreshMin(Sensor sens)
  {
    return (int) (short) (ushort) (int) (sens.MinimumThreshold & (long) ushort.MaxValue);
  }

  public static void SetTempThreshMax(Sensor sens, int value)
  {
    int num = (int) sens.MinimumThreshold & (int) ushort.MaxValue;
    sens.MinimumThreshold = (long) (num | (int) (ushort) value << 16 /*0x10*/);
  }

  public static int GetTempThreshMax(Sensor sens)
  {
    return (int) (short) (ushort) ((uint) sens.MinimumThreshold >> 16 /*0x10*/).ToInt();
  }

  public static void SetVibrationSensitivityThreshold(Sensor sens, int value)
  {
    uint num = (uint) sens.MaximumThreshold & 4294901760U;
    sens.MaximumThreshold = (long) (num | (uint) (ushort) value);
  }

  public static double GetVibrationSensitivityThreshold(Sensor sens)
  {
    return ((double) (sens.MaximumThreshold & (long) ushort.MaxValue).ToInt() / 1000.0).ToDouble();
  }

  public static void SetAxisMode(Sensor sens, int value)
  {
    uint num = Convert.ToUInt32(sens.MaximumThreshold) & 4278255615U;
    sens.MaximumThreshold = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int GetAxisMode(Sensor sens)
  {
    return (int) ((Convert.ToUInt32(sens.MaximumThreshold) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetFundFreqResolution(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4294967040U;
    sensor.Calibration1 = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetFundFreqResolution(Sensor sensor)
  {
    return (int) Convert.ToUInt32(sensor.Calibration1) & (int) byte.MaxValue;
  }

  public static void SetFundWindowing(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4294902015U;
    sensor.Calibration1 = (long) (num | (uint) (value << 8));
  }

  public static int GetFundWindowing(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration1) & 65280U) >> 8);
  }

  public static void SetHarmonicFreqResolution(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4278255615U;
    sensor.Calibration1 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int GetHarmonicFreqResolution(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration1) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetHarmonicWindowing(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 16777215U /*0xFFFFFF*/;
    sensor.Calibration1 = (long) (num | (uint) (value << 24));
  }

  public static int GetHarmonicWindowing(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration1) & 4278190080U /*0xFF000000*/) >> 24);
  }

  public static void SetHarmonicMin(Sensor sens, int value)
  {
    uint num = (uint) sens.Calibration3 & 4294901760U;
    sens.Calibration3 = (long) (num | (uint) (ushort) value);
  }

  public static int GetHarmonicMin(Sensor sens)
  {
    return (sens.Calibration3 & (long) ushort.MaxValue).ToInt();
  }

  public static void SetHarmonicMax(Sensor sens, int value)
  {
    int num = (int) sens.Calibration3 & (int) ushort.MaxValue;
    sens.Calibration3 = (long) (num | (int) (ushort) value << 16 /*0x10*/);
  }

  public static int GetHarmonicMax(Sensor sens)
  {
    return ((uint) sens.Calibration3 >> 16 /*0x10*/).ToInt();
  }

  public static void SetHarmonicPeakAware(Sensor sens, int value)
  {
    uint num = (uint) sens.Calibration4 & 4294901760U;
    sens.Calibration4 = (long) (num | (uint) (ushort) value);
  }

  public static int GetHarmonicPeakAware(Sensor sens)
  {
    return ((sens.Calibration4 & (long) ushort.MaxValue).ToInt() * 10).ToInt();
  }

  public static void SetHarmonicRMSAware(Sensor sens, int value)
  {
    int num = (int) sens.Calibration4 & (int) ushort.MaxValue;
    sens.Calibration4 = (long) (num | (int) (ushort) value << 16 /*0x10*/);
  }

  public static int GetHarmonicRMSAware(Sensor sens)
  {
    return (((uint) sens.Calibration4 >> 16 /*0x10*/).ToInt() * 10).ToInt();
  }

  public static long DefaultMinThreshold => 81985136;

  public static long DefaultMaxThreshold => 393316;

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    Vibration800Base.GetDefaults(new Version(sensor.FirmwareVersion), sensor.GenerationType);
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (collection["TempScale"] == "on")
    {
      viewData["TempScale"] = "F";
      Vibration800.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      viewData["TempScale"] = "C";
      Vibration800.MakeCelsius(sensor.SensorID);
    }
  }

  public static Dictionary<string, string> NotificationScaleValues()
  {
    return new Dictionary<string, string>()
    {
      {
        "Millimeters",
        "Millimeters"
      },
      {
        "Centimeters",
        "Centimeters"
      },
      {
        "Meters",
        "Meters"
      },
      {
        "Inches",
        "Inches"
      },
      {
        "Feet",
        "Feet"
      },
      {
        "Yards",
        "Yards"
      }
    };
  }
}
