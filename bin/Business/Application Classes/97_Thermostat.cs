// Decompiled with JetBrains decompiler
// Type: Monnit.Thermostat
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

public class Thermostat : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;
  private SensorAttribute _OccupiedBuffer;
  private SensorAttribute _UnoccupiedBuffer;
  private SensorAttribute _ChartFormat;
  private SensorAttribute _CalibrationValue;

  public static long MonnitApplicationID => ThermostatBase.MonnitApplicationID;

  public static string ApplicationName => nameof (Thermostat);

  public static eApplicationProfileType ProfileType => ThermostatBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => Thermostat.MonnitApplicationID;

  public double Temperature { get; set; }

  public double Humidity { get; set; }

  public bool Heater { get; set; }

  public bool Cooler { get; set; }

  public bool FanState { get; set; }

  public bool OccupancyState { get; set; }

  public bool FanOverride { get; set; }

  public bool OccupiedOverride { get; set; }

  public bool UnoccupiedOverride { get; set; }

  public bool SettingsLockout { get; set; }

  public int stsState { get; set; }

  public static Thermostat Create(byte[] sdm, int startIndex)
  {
    Thermostat thermostat = new Thermostat();
    thermostat.stsState = (int) sdm[startIndex - 1] >> 4;
    thermostat.Temperature = (double) BitConverter.ToInt16(sdm, startIndex) / 100.0;
    thermostat.Humidity = (double) BitConverter.ToInt16(sdm, startIndex + 2) / 100.0;
    byte b = sdm[startIndex + 4];
    thermostat.Heater = ThermostatBase.GetBit(b, 0);
    thermostat.Cooler = ThermostatBase.GetBit(b, 1);
    thermostat.FanState = ThermostatBase.GetBit(b, 2);
    thermostat.OccupancyState = ThermostatBase.GetBit(b, 3);
    thermostat.FanOverride = ThermostatBase.GetBit(b, 4);
    thermostat.OccupiedOverride = ThermostatBase.GetBit(b, 5);
    thermostat.UnoccupiedOverride = ThermostatBase.GetBit(b, 6);
    thermostat.SettingsLockout = ThermostatBase.GetBit(b, 7);
    return thermostat;
  }

  public override string Serialize()
  {
    return $"{this.Temperature.ToString()}|{this.Humidity.ToString()}|{this.Heater.ToString()}|{this.Cooler.ToString()}|{this.FanState.ToString()}|{this.OccupancyState.ToString()}|{this.FanOverride.ToString()}|{this.OccupiedOverride.ToString()}|{this.UnoccupiedOverride.ToString()}|{this.SettingsLockout.ToString()}|{this.stsState.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[10]
      {
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temperature),
        new AppDatum(eDatumType.Percentage, "Humidity", this.Humidity),
        new AppDatum(eDatumType.BooleanData, "Heater", this.Heater),
        new AppDatum(eDatumType.BooleanData, "Cooler", this.Cooler),
        new AppDatum(eDatumType.BooleanData, "FanState", this.FanState),
        new AppDatum(eDatumType.BooleanData, "OccupancyState", this.OccupancyState),
        new AppDatum(eDatumType.BooleanData, "FanOverride", this.FanOverride),
        new AppDatum(eDatumType.BooleanData, "OccupiedOverride", this.OccupiedOverride),
        new AppDatum(eDatumType.BooleanData, "UnoccupiedOverride", this.UnoccupiedOverride),
        new AppDatum(eDatumType.BooleanData, "SettingsLockout", this.SettingsLockout)
      });
    }
  }

  public static Thermostat Deserialize(string version, string serialized)
  {
    Thermostat thermostat = new Thermostat();
    if (string.IsNullOrWhiteSpace(serialized))
    {
      thermostat.Temperature = 0.0;
      thermostat.Humidity = 0.0;
      thermostat.Heater = false;
      thermostat.Cooler = false;
      thermostat.FanState = false;
      thermostat.OccupancyState = false;
      thermostat.FanOverride = false;
      thermostat.OccupiedOverride = false;
      thermostat.UnoccupiedOverride = false;
      thermostat.SettingsLockout = false;
      thermostat.stsState = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length > 1)
      {
        thermostat.Temperature = strArray[0].ToDouble();
        thermostat.Humidity = (double) (byte) strArray[1].ToInt();
        thermostat.Heater = strArray[2].ToBool();
        thermostat.Cooler = strArray[3].ToBool();
        thermostat.FanState = strArray[4].ToBool();
        thermostat.OccupancyState = strArray[5].ToBool();
        thermostat.FanOverride = strArray[6].ToBool();
        thermostat.OccupiedOverride = strArray[7].ToBool();
        thermostat.UnoccupiedOverride = strArray[8].ToBool();
        thermostat.SettingsLockout = strArray[9].ToBool();
        try
        {
          thermostat.stsState = strArray[10].ToInt();
        }
        catch
        {
          thermostat.stsState = 0;
        }
      }
      else
      {
        thermostat.Temperature = strArray[0].ToDouble();
        thermostat.Humidity = (double) (byte) strArray[0].ToInt();
        thermostat.Heater = strArray[0].ToBool();
        thermostat.Cooler = strArray[0].ToBool();
        thermostat.FanState = strArray[0].ToBool();
        thermostat.OccupancyState = strArray[0].ToBool();
        thermostat.FanOverride = strArray[0].ToBool();
        thermostat.OccupiedOverride = strArray[0].ToBool();
        thermostat.UnoccupiedOverride = strArray[0].ToBool();
        thermostat.SettingsLockout = strArray[0].ToBool();
      }
    }
    return thermostat;
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Thermostat.Deserialize(version, serialized);
  }

  public static bool IsFahrenheit(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF" && sensorAttribute.Value == "C")
        return false;
    }
    return true;
  }

  public static void MakeFahrenheit(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        sensorAttribute.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void MakeCelsius(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
      {
        if (sensorAttribute.Value != "C")
        {
          sensorAttribute.Value = "C";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "CorF",
        Value = "C",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute CorF => this._CorF;

  public SensorAttribute OccupiedBuffer => this._OccupiedBuffer;

  public SensorAttribute UnoccupiedBuffer => this._UnoccupiedBuffer;

  public SensorAttribute ChartFormat => this._ChartFormat;

  public SensorAttribute CalibrationValue => this._CalibrationValue;

  public static string GetOccupiedBuffer(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "OccupiedBuffer")
        return sensorAttribute.Value;
    }
    return "5";
  }

  public static void SetOccupiedBuffer(long sensorID, int OccupiedBuffer)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == nameof (OccupiedBuffer))
      {
        sensorAttribute.Value = OccupiedBuffer.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = nameof (OccupiedBuffer),
        Value = OccupiedBuffer.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetUnoccupiedBuffer(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "UnoccupiedBuffer")
        return sensorAttribute.Value;
    }
    return "5";
  }

  public static void SetUnoccupiedBuffer(long sensorID, int UnoccupiedBuffer)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == nameof (UnoccupiedBuffer))
      {
        sensorAttribute.Value = UnoccupiedBuffer.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = nameof (UnoccupiedBuffer),
        Value = UnoccupiedBuffer.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetCalibrationValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CalibrationValue")
        return sensorAttribute.Value;
    }
    return "4|0";
  }

  public static void SetCalibrationValue(long sensorID, int acc, int storedValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CalibrationValue")
      {
        sensorAttribute.Value = $"{acc.ToString()}|{storedValue.ToString()}";
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "CalibrationValue",
        Value = $"{acc.ToString()}|{storedValue.ToString()}",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
      if (sensorAttribute.Name == "ChartFormat")
        this._ChartFormat = sensorAttribute;
      if (sensorAttribute.Name == "CalibrationValue")
        this._CalibrationValue = sensorAttribute;
      if (sensorAttribute.Name == "OccupiedBuffer")
        this._OccupiedBuffer = sensorAttribute;
      if (sensorAttribute.Name == "UnoccupiedBuffer")
        this._UnoccupiedBuffer = sensorAttribute;
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public override object PlotValue
  {
    get
    {
      if (this.Temperature < -20.0 || this.Temperature > 60.0)
        return (object) null;
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.Temperature : (object) this.Temperature.ToFahrenheit();
    }
  }

  public override string NotificationString
  {
    get
    {
      string notificationString = string.Empty;
      if ((this.stsState & 1) == 1)
        return "Sensor Read Error";
      double num;
      if (this.Temperature >= -20.0 && this.Temperature <= 60.0)
      {
        string empty = string.Empty;
        string str;
        if (this.CorF != null && this.CorF.Value == "C")
        {
          num = this.Temperature;
          str = num.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
        }
        else
        {
          num = this.Temperature.ToFahrenheit();
          str = num.ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture);
        }
        notificationString = $"{notificationString}Temperature: {str}";
      }
      if (this.Humidity >= 0.0 && this.Humidity <= 100.0)
      {
        string str1 = notificationString;
        num = this.Humidity;
        string str2 = $", {num.ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture)}% ";
        notificationString = str1 + str2;
      }
      else if (this.Humidity != (double) byte.MaxValue)
        return "Hardware Failure";
      if (this.Heater)
        notificationString += ", Heater: On ";
      if (this.Cooler)
        notificationString += ", Cooler: On ";
      if (this.FanState)
        notificationString += ", Fan State: On ";
      if (this.OccupancyState)
        notificationString += ", Occupancy State: Occupied ";
      if (this.FanOverride)
        notificationString += ", FanOverride: Forced On ";
      if (this.OccupiedOverride)
        notificationString += ", Occupied Override: Forced ";
      if (this.UnoccupiedOverride)
        notificationString += ", Unoccupied Override: Forced ";
      return notificationString;
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

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    Thermostat.SetTemperatureOffset(sensor, (sbyte) 0);
    Thermostat.SetHumidityOffset(sensor, 0);
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }

  public static Dictionary<string, string> NotificationScaleValues()
  {
    return new Dictionary<string, string>()
    {
      {
        "F",
        "Fahrenheit"
      },
      {
        "C",
        "Celsius"
      }
    };
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Less_Than,
      CompareValue = "0",
      NotificationClass = eNotificationClass.Application,
      ApplicationID = Thermostat.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion,
      Scale = Thermostat.IsFahrenheit(sensor.SensorID) ? "F" : "C"
    };
  }

  public static void SetProfileSettings(
    Sensor sens,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    bool flag = Thermostat.IsFahrenheit(sens.SensorID);
    if (!string.IsNullOrEmpty(collection["ReportInterval"]))
      sens.ActiveStateInterval = collection["ReportInterval"].ToDouble();
    if (!string.IsNullOrEmpty(collection["TempBuffer"]))
      Thermostat.SetUnoccupiedBuffer(sens.SensorID, collection["TempBuffer"].ToInt());
    if (!string.IsNullOrEmpty(collection["OccupiedTempBuffer"]))
      Thermostat.SetOccupiedBuffer(sens.SensorID, collection["OccupiedTempBuffer"].ToInt());
    double Fahrenheit1 = !string.IsNullOrWhiteSpace(collection["HeatingThreshold"]) ? collection["HeatingThreshold"].ToDouble() : (flag ? Thermostat.GetHeatingThreshold(sens).ToFahrenheit() : Thermostat.GetHeatingThreshold(sens));
    double Fahrenheit2 = !string.IsNullOrWhiteSpace(collection["CoolingThreshold"]) ? collection["CoolingThreshold"].ToDouble() : (flag ? Thermostat.GetCoolingThreshold(sens).ToFahrenheit() : Thermostat.GetCoolingThreshold(sens));
    double Fahrenheit3 = !string.IsNullOrWhiteSpace(collection["HeatingBuffer"]) ? collection["HeatingBuffer"].ToDouble() : (flag ? Thermostat.GetHeatingBuffer(sens).ToFahrenheit() : Thermostat.GetHeatingBuffer(sens));
    double Fahrenheit4 = !string.IsNullOrWhiteSpace(collection["CoolingBuffer"]) ? collection["CoolingBuffer"].ToDouble() : (flag ? Thermostat.GetCoolingBuffer(sens).ToFahrenheit() : Thermostat.GetCoolingBuffer(sens));
    if (flag)
    {
      Fahrenheit1 = Fahrenheit1.ToCelsius();
      Fahrenheit2 = Fahrenheit2.ToCelsius();
      Fahrenheit3 = Fahrenheit3.ToCelsius();
      Fahrenheit4 = Fahrenheit4.ToCelsius();
    }
    if (Fahrenheit1 > Fahrenheit2)
      Fahrenheit1 = Fahrenheit2 - 2.0;
    if (Fahrenheit1 < 10.0)
      Fahrenheit1 = 10.0;
    if (Fahrenheit1 > 36.0)
      Fahrenheit1 = 36.0;
    if (Fahrenheit2 < 14.0)
      Fahrenheit2 = 14.0;
    if (Fahrenheit2 > 40.0)
      Fahrenheit2 = 40.0;
    if (Fahrenheit2 == Fahrenheit1)
      Fahrenheit2 = Fahrenheit1 + 2.0;
    if (Fahrenheit2 - Fahrenheit1 < 2.0)
      Fahrenheit2 = Fahrenheit1 + 2.0;
    if (Fahrenheit3 < Fahrenheit1 + 1.0)
      Fahrenheit3 = Fahrenheit1 + 1.0;
    if (Fahrenheit4 > Fahrenheit2 - 1.0)
      Fahrenheit4 = Fahrenheit2 - 1.0;
    if (Fahrenheit3 > Fahrenheit4)
    {
      double num = (Fahrenheit2 - Fahrenheit1) / 2.0 + Fahrenheit1;
      Fahrenheit3 = num;
      Fahrenheit4 = num;
    }
    Thermostat.SetHeatingThreshold(sens, Convert.ToInt32(Math.Round(Fahrenheit1 * 4.0)));
    Thermostat.SetCoolingThreshold(sens, Convert.ToInt32(Math.Round(Fahrenheit2 * 4.0)));
    Thermostat.SetHeatingBuffer(sens, Convert.ToInt32(Math.Round(Fahrenheit3 * 4.0)));
    Thermostat.SetCoolingBuffer(sens, Convert.ToInt32(Math.Round(Fahrenheit4 * 4.0)));
    double Fahrenheit5 = !string.IsNullOrWhiteSpace(collection["OccupiedHeatingThreshold"]) ? collection["OccupiedHeatingThreshold"].ToDouble() : (flag ? Thermostat.GetOccupiedHeatingThreshold(sens).ToFahrenheit() : Thermostat.GetOccupiedHeatingThreshold(sens));
    double Fahrenheit6 = !string.IsNullOrWhiteSpace(collection["OccupiedCoolingThreshold"]) ? collection["OccupiedCoolingThreshold"].ToDouble() : (flag ? Thermostat.GetOccupiedCoolingThreshold(sens).ToFahrenheit() : Thermostat.GetOccupiedCoolingThreshold(sens));
    double Fahrenheit7 = !string.IsNullOrWhiteSpace(collection["OccupiedCoolingBuffer"]) ? collection["OccupiedCoolingBuffer"].ToDouble() : (flag ? Thermostat.GetOccupiedCoolingBuffer(sens).ToFahrenheit() : Thermostat.GetOccupiedCoolingBuffer(sens));
    double Fahrenheit8 = !string.IsNullOrWhiteSpace(collection["OccupiedHeatingBuffer"]) ? collection["OccupiedHeatingBuffer"].ToDouble() : (flag ? Thermostat.GetOccupiedHeatingBuffer(sens).ToFahrenheit() : Thermostat.GetOccupiedHeatingBuffer(sens));
    if (flag)
    {
      Fahrenheit5 = Fahrenheit5.ToCelsius();
      Fahrenheit6 = Fahrenheit6.ToCelsius();
      Fahrenheit7 = Fahrenheit7.ToCelsius();
      Fahrenheit8 = Fahrenheit8.ToCelsius();
    }
    if (Fahrenheit5 > Fahrenheit6)
      Fahrenheit5 = Fahrenheit6 - 2.0;
    if (Fahrenheit5 < 10.0)
      Fahrenheit5 = 10.0;
    if (Fahrenheit5 > 36.0)
      Fahrenheit5 = 36.0;
    if (Fahrenheit6 < 14.0)
      Fahrenheit6 = 14.0;
    if (Fahrenheit6 > 40.0)
      Fahrenheit6 = 40.0;
    if (Fahrenheit6 == Fahrenheit5)
      Fahrenheit6 = Fahrenheit5 + 2.0;
    if (Fahrenheit6 - Fahrenheit5 < 2.0)
      Fahrenheit6 = Fahrenheit5 + 2.0;
    if (Fahrenheit8 < Fahrenheit5 + 1.0)
      Fahrenheit8 = Fahrenheit5 + 1.0;
    if (Fahrenheit7 > Fahrenheit6 - 1.0)
      Fahrenheit7 = Fahrenheit6 - 1.0;
    if (Fahrenheit8 > Fahrenheit7)
    {
      double num = (Fahrenheit6 - Fahrenheit5) / 2.0 + Fahrenheit5;
      Fahrenheit8 = num;
      Fahrenheit7 = num;
    }
    Thermostat.SetOccupiedHeatingThreshold(sens, Convert.ToInt32(Math.Round(Fahrenheit5 * 4.0)));
    Thermostat.SetOccupiedCoolingThreshold(sens, Convert.ToInt32(Math.Round(Fahrenheit6 * 4.0)));
    Thermostat.SetOccupiedHeatingBuffer(sens, Convert.ToInt32(Math.Round(Fahrenheit8 * 4.0)));
    Thermostat.SetOccupiedCoolingBuffer(sens, Convert.ToInt32(Math.Round(Fahrenheit7 * 4.0)));
    if (!string.IsNullOrWhiteSpace(collection["AwareStateTrigger"]))
      Thermostat.SetAwareStateTriggers(sens, Convert.ToInt32(collection["AwareStateTrigger"]));
    if (!string.IsNullOrWhiteSpace(collection["OccupiedTimeOut"]))
    {
      int num = collection["OccupiedTimeOut"].ToInt();
      if (num < 5)
        num = 5;
      if (num > 720)
        num = 720;
      Thermostat.SetOccupiedTimeout(sens, num / 5);
    }
    if (!string.IsNullOrWhiteSpace(collection["FanControl"]))
    {
      int num = collection["FanControl"].ToInt();
      if (num < 0)
        num = 0;
      if (num > 3)
        num = 3;
      Thermostat.SetFanControl(sens, num);
    }
    if (!string.IsNullOrWhiteSpace(collection["FanOnPeriod"]))
    {
      int num = collection["FanOnPeriod"].ToInt();
      if (num < 1)
        num = 1;
      if (num > 240 /*0xF0*/)
        num = 240 /*0xF0*/;
      Thermostat.SetFanOnPeriod(sens, num);
    }
    if (!string.IsNullOrWhiteSpace(collection["FanOnInterval"]))
    {
      int num1 = collection["FanOnInterval"].ToInt();
      if (num1 < 5)
        num1 = 5;
      if (num1 > 720)
        num1 = 720;
      int num2 = num1 / 5;
      Thermostat.SetFanOnInterval(sens, num2);
    }
    if (!string.IsNullOrWhiteSpace(collection["FanStartTimeForHeater"]))
    {
      int num3 = collection["FanStartTimeForHeater"].ToInt();
      if (num3 < -300)
        num3 = -300;
      if (num3 > 300)
        num3 = 300;
      int num4 = num3 / 5;
      Thermostat.SetFanStartTimeForHeater(sens, num4);
    }
    if (!string.IsNullOrWhiteSpace(collection["FanStopTimeForHeater"]))
    {
      int num5 = (int) Convert.ToInt16(collection["FanStopTimeForHeater"]);
      if (num5 < -300)
        num5 = -300;
      if (num5 > 300)
        num5 = 300;
      int num6 = num5 / 5;
      Thermostat.SetFanStopTimeForHeater(sens, num6);
    }
    if (!string.IsNullOrWhiteSpace(collection["FanStartDelayForCooler"]))
    {
      int num7 = (int) Convert.ToInt16(collection["FanStartDelayForCooler"]);
      if (num7 < -300)
        num7 = -300;
      if (num7 > 300)
        num7 = 300;
      int num8 = num7 / 5;
      Thermostat.SetFanStartDelayForCooler(sens, num8);
    }
    if (string.IsNullOrWhiteSpace(collection["FanStopDelayForCooler"]))
      return;
    int num9 = (int) Convert.ToInt16(collection["FanStopDelayForCooler"]);
    if (num9 < -300)
      num9 = -300;
    if (num9 > 300)
      num9 = 300;
    int num10 = num9 / 5;
    Thermostat.SetFanStopDelayForCooler(sens, num10);
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    switch (collection["CalibrationType"].ToInt())
    {
      case 4:
        Thermostat.SetCalibrationValue(sensor.SensorID, 4, collection["myval"].ToInt());
        sensor.PendingActionControlCommand = true;
        sensor.ProfileConfigDirty = false;
        sensor.ProfileConfig2Dirty = false;
        sensor.GeneralConfigDirty = false;
        sensor.GeneralConfig2Dirty = false;
        sensor.GeneralConfig3Dirty = false;
        break;
      case 6:
        Thermostat.SetCalibrationValue(sensor.SensorID, 6, collection["myval"].ToInt());
        sensor.PendingActionControlCommand = true;
        sensor.ProfileConfigDirty = false;
        sensor.ProfileConfig2Dirty = false;
        sensor.GeneralConfigDirty = false;
        sensor.GeneralConfig2Dirty = false;
        sensor.GeneralConfig3Dirty = false;
        break;
      case 7:
        Thermostat.SetCalibrationValue(sensor.SensorID, 7, collection["myval"].ToInt());
        sensor.PendingActionControlCommand = true;
        sensor.ProfileConfigDirty = false;
        sensor.ProfileConfig2Dirty = false;
        sensor.GeneralConfigDirty = false;
        sensor.GeneralConfig2Dirty = false;
        sensor.GeneralConfig3Dirty = false;
        break;
      case 10:
        int num1 = Thermostat.IsFahrenheit(sensor.SensorID) ? (collection["myval"].ToDouble() / 1.8 * 10.0).ToInt() : (collection["myval"].ToDouble() * 10.0).ToInt();
        if (num1 >= (int) sbyte.MinValue && num1 <= (int) sbyte.MaxValue)
        {
          Thermostat.SetTemperatureOffset(sensor, Convert.ToSByte(num1));
          sensor.PendingActionControlCommand = false;
          sensor.ProfileConfigDirty = false;
          sensor.ProfileConfig2Dirty = true;
          sensor.GeneralConfigDirty = false;
          sensor.GeneralConfig2Dirty = false;
          sensor.GeneralConfig3Dirty = false;
          break;
        }
        break;
      case 11:
        int num2 = (collection["myval"].ToDouble() * 10.0).ToInt();
        if (num2 >= (int) sbyte.MinValue && num2 <= (int) sbyte.MaxValue)
        {
          Thermostat.SetHumidityOffset(sensor, num2);
          sensor.PendingActionControlCommand = false;
          sensor.ProfileConfigDirty = false;
          sensor.ProfileConfig2Dirty = true;
          sensor.GeneralConfigDirty = false;
          sensor.GeneralConfig2Dirty = false;
          sensor.GeneralConfig3Dirty = false;
          break;
        }
        break;
    }
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
    uint num1 = Convert.ToUInt32(source.Calibration1) & 4294901760U;
    uint num2 = Convert.ToUInt32(target.Calibration1) & (uint) ushort.MaxValue;
    target.Calibration1 = (long) (num1 | num2);
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

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    SensorAttribute.ResetAttributeList(sensor.SensorID);
    List<byte[]> numArrayList = new List<byte[]>();
    string[] strArray = Thermostat.GetCalibrationValue(sensor.SensorID).Split('|');
    numArrayList.Add(ThermostatBase.Calibration(sensor.SensorID, Convert.ToByte(strArray[0].ToInt()), strArray[1].ToInt()));
    numArrayList.Add(sensor.ReadProfileConfig(29));
    return numArrayList;
  }

  public static string Relay1Name(long sensorID)
  {
    return MonnitApplicationBase.GetAttributeValue(sensorID, nameof (Relay1Name), "Relay1");
  }

  public static void SetRelay1Name(long sensorID, string name)
  {
    MonnitApplicationBase.SetAttribute(sensorID, "Relay1Name", name);
  }

  public static bool Relay1Visibility(long sensorID)
  {
    return MonnitApplicationBase.GetAttributeValue(sensorID, nameof (Relay1Visibility), "True").ToBool();
  }

  public static void ShowRelay1(long sensorID)
  {
    MonnitApplicationBase.SetAttribute(sensorID, "Relay1Visibility", (string) null);
  }

  public static void HideRelay1(long sensorID)
  {
    MonnitApplicationBase.SetAttribute(sensorID, "Relay1Visibility", "False");
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "14" : Math.Round((double) sensor.Hysteresis / 60.0, 2).ToString();
  }

  public static string MinThreshForUI(Sensor sensor) => "Not Used";

  public static string MaxThreshForUI(Sensor sensor) => "Not Used";

  public static void Control(Sensor sensor, NameValueCollection collection)
  {
    int num = collection["isOccupied"].ToInt();
    int storedValue = collection["Duration"].ToInt();
    try
    {
      switch (num)
      {
        case 0:
          Thermostat.SetCalibrationValue(sensor.SensorID, 7, storedValue);
          sensor.PendingActionControlCommand = true;
          sensor.ProfileConfigDirty = false;
          sensor.ProfileConfig2Dirty = false;
          sensor.GeneralConfigDirty = false;
          sensor.GeneralConfig2Dirty = false;
          sensor.GeneralConfig3Dirty = false;
          break;
        case 1:
          Thermostat.SetCalibrationValue(sensor.SensorID, 6, storedValue);
          sensor.PendingActionControlCommand = true;
          sensor.ProfileConfigDirty = false;
          sensor.ProfileConfig2Dirty = false;
          sensor.GeneralConfigDirty = false;
          sensor.GeneralConfig2Dirty = false;
          sensor.GeneralConfig3Dirty = false;
          break;
      }
      sensor.Save();
      CSNet.SetGatewaysUrgentTrafficFlag(sensor.CSNetID);
    }
    catch
    {
    }
  }

  public static string CreateSerializedRecipientProperties(int Occupancy, ushort Duration)
  {
    if (Duration < (ushort) 0)
      Duration = (ushort) 0;
    if (Duration > ushort.MaxValue)
      Duration = ushort.MaxValue;
    return $"{Occupancy}|{Duration}";
  }

  public static void ParseSerializedRecipientProperties(
    string serialized,
    out int Occupancy,
    out ushort Duration)
  {
    Occupancy = 0;
    Duration = (ushort) 1800;
    try
    {
      string[] strArray = serialized.Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      Occupancy = strArray[0].ToInt();
      int num = strArray[1].ToInt();
      if (num < 0)
        num = 0;
      if (num > (int) ushort.MaxValue)
        num = (int) ushort.MaxValue;
      Duration = Convert.ToUInt16(num);
    }
    catch
    {
    }
  }

  public static void SetCoolingThreshold(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4294967040U;
    sensor.MinimumThreshold = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static double GetCoolingThreshold(Sensor sensor)
  {
    return (double) (Convert.ToUInt32(sensor.MinimumThreshold) & (uint) byte.MaxValue) / 4.0;
  }

  public static void SetHeatingThreshold(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4294902015U;
    sensor.MinimumThreshold = (long) (num | (uint) (value << 8));
  }

  public static double GetHeatingThreshold(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.MinimumThreshold) & 65280U) >> 8) / 4.0;
  }

  public static void SetCoolingBuffer(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4278255615U;
    sensor.MinimumThreshold = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static double GetCoolingBuffer(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.MinimumThreshold) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/) / 4.0;
  }

  public static void SetHeatingBuffer(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 16777215U /*0xFFFFFF*/;
    sensor.MinimumThreshold = (long) (num | (uint) (value << 24));
  }

  public static double GetHeatingBuffer(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.MinimumThreshold) & 4278190080U /*0xFF000000*/) >> 24) / 4.0;
  }

  public static void SetOccupiedCoolingThreshold(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 4294967040U;
    sensor.MaximumThreshold = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static double GetOccupiedCoolingThreshold(Sensor sensor)
  {
    return (double) (Convert.ToUInt32(sensor.MaximumThreshold) & (uint) byte.MaxValue) / 4.0;
  }

  public static void SetOccupiedHeatingThreshold(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 4294902015U;
    sensor.MaximumThreshold = (long) (num | (uint) (value << 8));
  }

  public static double GetOccupiedHeatingThreshold(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.MaximumThreshold) & 65280U) >> 8) / 4.0;
  }

  public static void SetOccupiedCoolingBuffer(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 4278255615U;
    sensor.MaximumThreshold = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static double GetOccupiedCoolingBuffer(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.MaximumThreshold) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/) / 4.0;
  }

  public static void SetOccupiedHeatingBuffer(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 16777215U /*0xFFFFFF*/;
    sensor.MaximumThreshold = (long) (num | (uint) (value << 24));
  }

  public static double GetOccupiedHeatingBuffer(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.MaximumThreshold) & 4278190080U /*0xFF000000*/) >> 24) / 4.0;
  }

  public static void SetAwareStateTriggers(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4294967040U;
    sensor.Hysteresis = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetAwareStateTriggers(Sensor sensor)
  {
    return (int) Convert.ToUInt32(sensor.Hysteresis) & (int) byte.MaxValue;
  }

  public static void SetTemperatureUpperAwareThreshold(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4294902015U;
    sensor.Hysteresis = (long) (num | (uint) (value << 8));
  }

  public static double GetTemperatureUpperAwareThreshold(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.Hysteresis) & 65280U) >> 8) / 4.0;
  }

  public static void SetTemperatureLowerAwareThreshold(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4278255615U;
    sensor.Hysteresis = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static double GetTemperatureLowerAwareThreshold(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.Hysteresis) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/) / 4.0;
  }

  public static void SetTemperatureAwareStateBuffer(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 16777215U /*0xFFFFFF*/;
    sensor.Hysteresis = (long) (num | (uint) (value << 24));
  }

  public static double GetTemperatureAwareStateBuffer(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.Hysteresis) & 4278190080U /*0xFF000000*/) >> 24) / 4.0;
  }

  public static void SetTemperatureOffset(Sensor sensor, sbyte value)
  {
    uint num = (uint) sensor.Calibration1 & 4294967040U;
    sensor.Calibration1 = (long) (num | (uint) value & (uint) byte.MaxValue);
  }

  public static double GetTemperatureOffset(Sensor sensor)
  {
    return (double) (sbyte) ((int) (uint) sensor.Calibration1 & (int) byte.MaxValue) / 10.0;
  }

  public static void SetHumidityOffset(Sensor sensor, int value)
  {
    uint num = (uint) sensor.Calibration1 & 4294902015U;
    sensor.Calibration1 = (long) (num | (uint) (value << 8 & 65280));
  }

  public static double GetHumidityOffset(Sensor sensor)
  {
    return (double) (sbyte) (((uint) sensor.Calibration1 & 65280U) >> 8) / 10.0;
  }

  public static void SetHumidityUpperAwareThreshold(Sensor sensor, double value)
  {
    int num1 = Math.Round(value).ToInt();
    uint num2 = (uint) sensor.Calibration1 & 4278255615U;
    sensor.Calibration1 = (long) (num2 | (uint) (num1 << 16 /*0x10*/));
  }

  public static sbyte GetHumidityUpperAwareThreshold(Sensor sensor)
  {
    return (sbyte) (((uint) sensor.Calibration1 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetHumidityLowerAwareThreshold(Sensor sensor, double value)
  {
    int num1 = Math.Round(value).ToInt();
    uint num2 = (uint) sensor.Calibration1 & 16777215U /*0xFFFFFF*/;
    sensor.Calibration1 = (long) (num2 | (uint) (num1 << 24));
  }

  public static sbyte GetHumidityLowerAwareThreshold(Sensor sensor)
  {
    return (sbyte) (((uint) sensor.Calibration1 & 4278190080U /*0xFF000000*/) >> 24);
  }

  public static void SetEnableOccupancyDetection(Sensor sensor, int value)
  {
    uint num = (uint) sensor.Calibration2 & 4294967040U;
    sensor.Calibration2 = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static sbyte GetEnableOccupancyDetection(Sensor sensor)
  {
    return (sbyte) ((int) (uint) sensor.Calibration2 & (int) byte.MaxValue);
  }

  public static void SetOccupiedTimeout(Sensor sensor, int value)
  {
    uint num = (uint) sensor.Calibration2 & 4294902015U;
    sensor.Calibration2 = (long) (num | (uint) (value << 8));
  }

  public static int GetOccupiedTimeout(Sensor sensor)
  {
    return (int) (((uint) sensor.Calibration2 & 65280U) >> 8) * 5;
  }

  public static void SetSamplingInterval(Sensor sensor, int value)
  {
    uint num = (uint) sensor.Calibration2 & 4278255615U;
    sensor.Calibration2 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static sbyte GetSamplingInterval(Sensor sensor)
  {
    return (sbyte) (((uint) sensor.Calibration2 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetMinimumOffStateTime(Sensor sensor, int value)
  {
    uint num = (uint) sensor.Calibration2 & 16777215U /*0xFFFFFF*/;
    sensor.Calibration2 = (long) (num | (uint) (value << 24));
  }

  public static sbyte GetMinimumOffStateTime(Sensor sensor)
  {
    return (sbyte) (((uint) sensor.Calibration2 & 4278190080U /*0xFF000000*/) >> 24);
  }

  public static void SetFanControl(Sensor sensor, int value)
  {
    uint num = (uint) sensor.Calibration3 & 4294967040U;
    sensor.Calibration3 = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetFanControl(Sensor sensor)
  {
    return (int) (sbyte) ((int) (uint) sensor.Calibration3 & (int) byte.MaxValue);
  }

  public static void SetFanOnPeriod(Sensor sensor, int value)
  {
    uint num = (uint) sensor.Calibration3 & 4294902015U;
    sensor.Calibration3 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static int GetFanOnPeriod(Sensor sensor)
  {
    return (int) (((uint) sensor.Calibration3 & 65280U) >> 8);
  }

  public static void SetFanOnInterval(Sensor sensor, int value)
  {
    uint num = (uint) sensor.Calibration3 & 4278255615U;
    sensor.Calibration3 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static int GetFanOnInterval(Sensor sensor)
  {
    return (int) (((uint) sensor.Calibration3 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/) * 5;
  }

  public static void SetHumidityAwareStateBuffer(Sensor sensor, int value)
  {
    uint num = (uint) sensor.Calibration3 & 16777215U /*0xFFFFFF*/;
    sensor.Calibration3 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 24));
  }

  public static sbyte GetHumidityAwareStateBuffer(Sensor sensor)
  {
    return (sbyte) (((uint) sensor.Calibration3 & 4278190080U /*0xFF000000*/) >> 24);
  }

  public static void SetFanStartTimeForHeater(Sensor sensor, int value)
  {
    uint num = (uint) sensor.Calibration4 & 4294967040U;
    sensor.Calibration4 = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetFanStartTimeForHeater(Sensor sensor)
  {
    return (int) (short) ((int) (sbyte) ((int) (uint) sensor.Calibration4 & (int) byte.MaxValue) * 5);
  }

  public static void SetFanStopTimeForHeater(Sensor sensor, int value)
  {
    uint num = (uint) sensor.Calibration4 & 4294902015U;
    sensor.Calibration4 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static short GetFanStopTimeForHeater(Sensor sensor)
  {
    return (short) ((int) (sbyte) (((uint) sensor.Calibration4 & 65280U) >> 8) * 5);
  }

  public static void SetFanStartDelayForCooler(Sensor sensor, int value)
  {
    uint num = (uint) sensor.Calibration4 & 4278255615U;
    sensor.Calibration4 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static short GetFanStartDelayForCooler(Sensor sensor)
  {
    return (short) ((int) (sbyte) (((uint) sensor.Calibration4 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/) * 5);
  }

  public static void SetFanStopDelayForCooler(Sensor sensor, int value)
  {
    uint num = (uint) sensor.Calibration4 & 16777215U /*0xFFFFFF*/;
    sensor.Calibration4 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 24));
  }

  public static short GetFanStopDelayForCooler(Sensor sensor)
  {
    return (short) ((int) (sbyte) (((uint) sensor.Calibration4 & 4278190080U /*0xFF000000*/) >> 24) * 5);
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Thermostat left, Thermostat right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Thermostat left, Thermostat right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Thermostat left, Thermostat right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Thermostat left, Thermostat right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Thermostat left, Thermostat right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Thermostat left, Thermostat right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Thermostat && this.Equals((MonnitApplicationBase) (obj as Thermostat));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Thermostat && this.Temperature == (right as Thermostat).Temperature;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Thermostat && this.Temperature != (right as Thermostat).Temperature;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Thermostat && this.Temperature < (right as Thermostat).Temperature;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Thermostat && this.Temperature <= (right as Thermostat).Temperature;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Thermostat && this.Temperature > (right as Thermostat).Temperature;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Thermostat && this.Temperature >= (right as Thermostat).Temperature;
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (collection["TempScale"] == "on")
    {
      viewData["TempScale"] = "F";
      Thermostat.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      viewData["TempScale"] = "C";
      Thermostat.MakeCelsius(sensor.SensorID);
    }
  }
}
