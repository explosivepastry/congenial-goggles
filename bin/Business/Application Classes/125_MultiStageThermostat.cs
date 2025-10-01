// Decompiled with JetBrains decompiler
// Type: Monnit.MultiStageThermostat
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

public class MultiStageThermostat : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;
  private SensorAttribute _OccupiedBuffer;
  private SensorAttribute _UnoccupiedBuffer;
  private SensorAttribute _TroubleShootingModeActive;
  private SensorAttribute _CalibrationValue;

  public static long MonnitApplicationID => MultiStageThermostatBase.MonnitApplicationID;

  public static string ApplicationName => "MultiStage Thermostat";

  public static eApplicationProfileType ProfileType => MultiStageThermostatBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => MultiStageThermostat.MonnitApplicationID;

  public double Temperature { get; set; }

  public double Humidity { get; set; }

  public bool Heat1 { get; set; }

  public bool Heat2 { get; set; }

  public bool Heat3 { get; set; }

  public bool Cool1 { get; set; }

  public bool Cool2 { get; set; }

  public bool Fan { get; set; }

  public bool HeatPump1 { get; set; }

  public bool HeatPump2 { get; set; }

  public int ReversingValve { get; set; }

  public bool AuxHeat { get; set; }

  public bool EmergencyHeat { get; set; }

  public bool OccupancyState { get; set; }

  public bool FanOverrideMode { get; set; }

  public bool OccupiedOverride { get; set; }

  public bool UnoccupiedOverride { get; set; }

  public bool SettingsLockout { get; set; }

  public int SystemType { get; set; }

  public string Mode { get; set; }

  public int HvacStatus { get; set; }

  public int stsState { get; set; }

  public static MultiStageThermostat Create(byte[] sdm, int startIndex)
  {
    MultiStageThermostat multiStageThermostat = new MultiStageThermostat();
    multiStageThermostat.stsState = (int) sdm[startIndex - 1] >> 4;
    multiStageThermostat.Temperature = (double) BitConverter.ToInt16(sdm, startIndex) / 100.0;
    multiStageThermostat.Humidity = (double) BitConverter.ToInt16(sdm, startIndex + 2) / 100.0;
    byte b1 = sdm[startIndex + 4];
    byte b2 = sdm[startIndex + 5];
    byte b3 = sdm[startIndex + 6];
    multiStageThermostat.Heat1 = MultiStageThermostatBase.GetBit(b1, 0);
    multiStageThermostat.Heat2 = MultiStageThermostatBase.GetBit(b1, 1);
    multiStageThermostat.Heat3 = MultiStageThermostatBase.GetBit(b1, 2);
    multiStageThermostat.Cool1 = MultiStageThermostatBase.GetBit(b1, 3);
    multiStageThermostat.Cool2 = MultiStageThermostatBase.GetBit(b1, 4);
    multiStageThermostat.Fan = MultiStageThermostatBase.GetBit(b1, 5);
    multiStageThermostat.HeatPump1 = MultiStageThermostatBase.GetBit(b1, 6);
    multiStageThermostat.HeatPump2 = MultiStageThermostatBase.GetBit(b1, 7);
    multiStageThermostat.ReversingValve = (int) b2 & 7;
    multiStageThermostat.AuxHeat = MultiStageThermostatBase.GetBit(b2, 3);
    multiStageThermostat.EmergencyHeat = MultiStageThermostatBase.GetBit(b2, 4);
    multiStageThermostat.OccupancyState = MultiStageThermostatBase.GetBit(b2, 5);
    multiStageThermostat.FanOverrideMode = MultiStageThermostatBase.GetBit(b2, 6);
    multiStageThermostat.OccupiedOverride = MultiStageThermostatBase.GetBit(b2, 7);
    multiStageThermostat.UnoccupiedOverride = MultiStageThermostatBase.GetBit(b3, 0);
    multiStageThermostat.SettingsLockout = MultiStageThermostatBase.GetBit(b3, 1);
    multiStageThermostat.SystemType = (int) sdm[startIndex + 8];
    multiStageThermostat.Mode = MultiStageThermostat.ModeStringValue((int) sdm[startIndex + 9]);
    multiStageThermostat.HvacStatus = (int) sdm[startIndex + 10];
    return multiStageThermostat;
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[6]
      {
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temperature),
        new AppDatum(eDatumType.Percentage, "Humidity", this.Humidity),
        new AppDatum(eDatumType.BooleanData, "Heating", this.HvacStatus == 1),
        new AppDatum(eDatumType.BooleanData, "Cooling", this.HvacStatus == 2),
        new AppDatum(eDatumType.BooleanData, "Fan", this.Fan),
        new AppDatum(eDatumType.BooleanData, "OccupancyState", this.OccupancyState)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[1]
    {
      MultiStageThermostat.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    });
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>() { this.PlotValue };
  }

  public override string Serialize()
  {
    return $"{this.Temperature.ToString()}|{this.Humidity.ToString()}|{this.Heat1.ToString()}|{this.Heat2.ToString()}|{this.Heat3.ToString()}|{this.Cool1.ToString()}|{this.Cool2.ToString()}|{this.Fan.ToString()}|{this.HeatPump1.ToString()}|{this.HeatPump2.ToString()}|{this.ReversingValve.ToString()}|{this.AuxHeat.ToString()}|{this.EmergencyHeat.ToString()}|{this.OccupancyState.ToString()}|{this.FanOverrideMode.ToString()}|{this.OccupiedOverride.ToString()}|{this.UnoccupiedOverride.ToString()}|{this.SettingsLockout.ToString()}|{this.SystemType.ToString()}|{this.Mode}|{this.HvacStatus.ToString()}|{this.stsState.ToString()}";
  }

  public static MultiStageThermostat Deserialize(string version, string serialized)
  {
    MultiStageThermostat multiStageThermostat = new MultiStageThermostat();
    if (string.IsNullOrWhiteSpace(serialized))
    {
      multiStageThermostat.Temperature = 0.0;
      multiStageThermostat.Humidity = 0.0;
      multiStageThermostat.Heat1 = false;
      multiStageThermostat.Heat2 = false;
      multiStageThermostat.Heat3 = false;
      multiStageThermostat.Cool1 = false;
      multiStageThermostat.Cool2 = false;
      multiStageThermostat.Fan = false;
      multiStageThermostat.HeatPump1 = false;
      multiStageThermostat.HeatPump2 = false;
      multiStageThermostat.ReversingValve = 0;
      multiStageThermostat.AuxHeat = false;
      multiStageThermostat.EmergencyHeat = false;
      multiStageThermostat.OccupancyState = false;
      multiStageThermostat.FanOverrideMode = false;
      multiStageThermostat.OccupiedOverride = false;
      multiStageThermostat.UnoccupiedOverride = false;
      multiStageThermostat.SettingsLockout = false;
      multiStageThermostat.SystemType = 0;
      multiStageThermostat.Mode = "";
      multiStageThermostat.HvacStatus = 0;
      multiStageThermostat.stsState = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length > 1)
      {
        try
        {
          multiStageThermostat.Temperature = strArray[0].ToDouble();
          multiStageThermostat.Humidity = strArray[1].ToDouble();
          multiStageThermostat.Heat1 = strArray[2].ToBool();
          multiStageThermostat.Heat2 = strArray[3].ToBool();
          multiStageThermostat.Heat3 = strArray[4].ToBool();
          multiStageThermostat.Cool1 = strArray[5].ToBool();
          multiStageThermostat.Cool2 = strArray[6].ToBool();
          multiStageThermostat.Fan = strArray[7].ToBool();
          multiStageThermostat.HeatPump1 = strArray[8].ToBool();
          multiStageThermostat.HeatPump2 = strArray[9].ToBool();
          multiStageThermostat.ReversingValve = strArray[10].ToInt();
          multiStageThermostat.AuxHeat = strArray[11].ToBool();
          multiStageThermostat.EmergencyHeat = strArray[12].ToBool();
          multiStageThermostat.OccupancyState = strArray[13].ToBool();
          multiStageThermostat.FanOverrideMode = strArray[14].ToBool();
          multiStageThermostat.OccupiedOverride = strArray[15].ToBool();
          multiStageThermostat.UnoccupiedOverride = strArray[16 /*0x10*/].ToBool();
          multiStageThermostat.SettingsLockout = strArray[17].ToBool();
          multiStageThermostat.SystemType = strArray[18].ToInt();
          multiStageThermostat.Mode = strArray[19].ToString();
          multiStageThermostat.HvacStatus = strArray[20].ToInt();
          try
          {
            multiStageThermostat.stsState = strArray[21].ToInt();
          }
          catch
          {
            multiStageThermostat.stsState = 0;
          }
        }
        catch
        {
          multiStageThermostat.Temperature = 0.0;
          multiStageThermostat.Humidity = 0.0;
          multiStageThermostat.Heat1 = false;
          multiStageThermostat.Heat2 = false;
          multiStageThermostat.Heat3 = false;
          multiStageThermostat.Cool1 = false;
          multiStageThermostat.Cool2 = false;
          multiStageThermostat.Fan = false;
          multiStageThermostat.HeatPump1 = false;
          multiStageThermostat.HeatPump2 = false;
          multiStageThermostat.ReversingValve = 0;
          multiStageThermostat.AuxHeat = false;
          multiStageThermostat.EmergencyHeat = false;
          multiStageThermostat.OccupancyState = false;
          multiStageThermostat.FanOverrideMode = false;
          multiStageThermostat.OccupiedOverride = false;
          multiStageThermostat.UnoccupiedOverride = false;
          multiStageThermostat.SettingsLockout = false;
          multiStageThermostat.SystemType = 0;
          multiStageThermostat.Mode = "";
          multiStageThermostat.HvacStatus = 0;
          multiStageThermostat.stsState = 0;
        }
      }
      else
      {
        multiStageThermostat.Temperature = strArray[0].ToDouble();
        multiStageThermostat.Humidity = strArray[0].ToDouble();
        multiStageThermostat.Heat1 = strArray[0].ToBool();
        multiStageThermostat.Heat2 = strArray[0].ToBool();
        multiStageThermostat.Heat3 = strArray[0].ToBool();
        multiStageThermostat.Cool1 = strArray[0].ToBool();
        multiStageThermostat.Cool2 = strArray[0].ToBool();
        multiStageThermostat.Fan = strArray[0].ToBool();
        multiStageThermostat.HeatPump1 = strArray[0].ToBool();
        multiStageThermostat.HeatPump2 = strArray[0].ToBool();
        multiStageThermostat.ReversingValve = strArray[0].ToInt();
        multiStageThermostat.AuxHeat = strArray[0].ToBool();
        multiStageThermostat.EmergencyHeat = strArray[0].ToBool();
        multiStageThermostat.OccupancyState = strArray[0].ToBool();
        multiStageThermostat.FanOverrideMode = strArray[0].ToBool();
        multiStageThermostat.OccupiedOverride = strArray[0].ToBool();
        multiStageThermostat.UnoccupiedOverride = strArray[0].ToBool();
        multiStageThermostat.SettingsLockout = strArray[0].ToBool();
        multiStageThermostat.SystemType = strArray[0].ToInt();
        multiStageThermostat.Mode = "";
        multiStageThermostat.HvacStatus = strArray[0].ToInt();
        multiStageThermostat.stsState = 0;
      }
    }
    return multiStageThermostat;
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) MultiStageThermostat.Deserialize(version, serialized);
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

  public SensorAttribute TroubleShootingModeActive
  {
    get
    {
      if (this._TroubleShootingModeActive == null)
        this._TroubleShootingModeActive = new SensorAttribute()
        {
          Name = nameof (TroubleShootingModeActive),
          Value = "false"
        };
      return this._TroubleShootingModeActive;
    }
  }

  public static bool GetTroubleShootingModeValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "TroubleShootingModeActive")
        return sensorAttribute.Value.ToBool();
    }
    return false;
  }

  public static void SetTroubleShootingModeValue(long sensorID, bool troubleShootingModeActive)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "TroubleShootingModeActive")
      {
        sensorAttribute.Value = troubleShootingModeActive.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "TroubleShootingModeActive",
        Value = troubleShootingModeActive.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

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
      if (sensorAttribute.Name == "CalibrationValue")
        this._CalibrationValue = sensorAttribute;
      if (sensorAttribute.Name == "TroubleShootingModeActive")
        this._TroubleShootingModeActive = sensorAttribute;
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
      if (this.Temperature < -40.0 || this.Temperature > 125.0)
        return (object) null;
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.Temperature : (object) this.Temperature.ToFahrenheit();
    }
  }

  public double PlotHumidityValue
  {
    get => this.Humidity >= 0.0 && this.Humidity < 300.0 ? this.Humidity : -1.0;
  }

  public static string ModeStringValue(int mode) => MultiStageThermostatBase.ModeStringValue(mode);

  public static string ReversingValveStringValue(int val)
  {
    return MultiStageThermostatBase.ReversingValveStringValue(val);
  }

  public static string SystemTypeStringValue(int systemType)
  {
    return MultiStageThermostatBase.SystemTypeStringValue(systemType);
  }

  public override string NotificationString
  {
    get
    {
      string str1 = string.Empty;
      if ((this.stsState & 1) == 1)
        return "Sensor Read Error";
      double num;
      if (this.Temperature >= -40.0 && this.Temperature <= 125.0)
      {
        string empty = string.Empty;
        string str2;
        if (this.CorF != null && this.CorF.Value == "C")
        {
          num = this.Temperature;
          str2 = num.ToString("#0.##° C", (IFormatProvider) CultureInfo.InvariantCulture);
        }
        else
        {
          num = this.Temperature.ToFahrenheit();
          str2 = num.ToString("#0.##° F", (IFormatProvider) CultureInfo.InvariantCulture);
        }
        str1 = $"{str1}Temperature: {str2}";
      }
      if (this.Humidity >= 0.0 && this.Humidity <= 100.0)
      {
        string str3 = str1;
        num = this.Humidity;
        string str4 = $", {num.ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture)}% ";
        str1 = str3 + str4;
      }
      if (this.TroubleShootingModeActive.Value.ToLower() == "false")
      {
        string str5 = "Off";
        if (this.HvacStatus == 1)
          str5 = " Heating";
        if (this.HvacStatus == 2)
          str5 = " Cooling";
        return $"{$"{str1}, Status: {str5}"}, Fan: {(this.Fan ? "On" : "Off")}" + (this.OccupancyState ? ", Occupied" : ", Unoccupied");
      }
      return $"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{str1}, Heat 1: {(this.Heat1 ? "On" : "Off")}"}, Heat 2: {(this.Heat2 ? "On" : "Off")}"}, Heat 3: {(this.Heat3 ? "On" : "Off")}"}, Cool 1: {(this.Cool1 ? "On" : "Off")}"}, Cool 2: {(this.Cool2 ? "On" : "Off")}"}, Fan: {(this.Fan ? "On" : "Off")}"}, HeatPump 1: {(this.HeatPump1 ? "On" : "Off")}"}, HeatPump 2: {(this.HeatPump2 ? "On" : "Off")}"}, Reversing Valve: {MultiStageThermostat.ReversingValveStringValue(this.ReversingValve)}"}, Aux Heat: {(this.AuxHeat ? "On" : "Off")}"}, Emergency Heat: {(this.EmergencyHeat ? "On" : "Off")}"}, Occupancy State: {(this.OccupancyState ? "Occupied" : "Unoccupied")}"}, Fan Override Mode: {(this.FanOverrideMode ? "On" : "Off")}"}, Occupied Override: {(this.OccupiedOverride ? "On" : "Off")}"}, Unoccupied Override: {(this.UnoccupiedOverride ? "On" : "Off")}"}, Settings Lockout: {(this.SettingsLockout ? "Locked" : "Unlocked")}"}, System Type: {MultiStageThermostat.SystemTypeStringValue(this.SystemType)}"}, Mode: {this.Mode}"}, HvacStatus: ({this.HvacStatus.ToString()}) {(this.HvacStatus <= 0 ? "Off" : (this.HvacStatus == 1 ? "Heating" : "Cooling"))}";
    }
  }

  public static void SetProfileSettings(
    Sensor sens,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    bool flag = MultiStageThermostat.IsFahrenheit(sens.SensorID);
    sens.ActiveStateInterval = sens.ReportInterval;
    if (!string.IsNullOrEmpty(collection["SystemType"]))
      MultiStageThermostat.SetSystemType(sens, collection["SystemType"].ToInt());
    if (!string.IsNullOrEmpty(collection["HeatCoolMode"]))
      MultiStageThermostat.SetHeatCoolMode(sens, collection["HeatCoolMode"].ToInt());
    if (!string.IsNullOrEmpty(collection["OccupiedSetpoint"]))
    {
      double Fahrenheit = collection["OccupiedSetpoint"].ToDouble();
      if (flag)
        Fahrenheit = Fahrenheit.ToCelsius();
      if (Fahrenheit < 10.0)
        Fahrenheit = 10.0;
      if (Fahrenheit > 37.0)
        Fahrenheit = 37.0;
      MultiStageThermostat.SetOccupiedSetPoint(sens, Fahrenheit);
    }
    if (!string.IsNullOrEmpty(collection["TempDelta"]))
    {
      double num = collection["TempDelta"].ToDouble();
      if (flag)
        num /= 1.8;
      if (num < 1.0)
        num = 1.0;
      if (num > 5.0)
        num = 5.0;
      MultiStageThermostat.SetTempDelta(sens, num);
    }
    int num1 = Math.Round(MultiStageThermostat.GetTempDelta(sens), 0).ToInt();
    if (!string.IsNullOrEmpty(collection["OccupiedTimeout"]))
    {
      double num2 = collection["OccupiedTimeout"].ToDouble();
      if (num2 < 5.0)
        num2 = 5.0;
      if (num2 > 720.0)
        num2 = 720.0;
      MultiStageThermostat.SetOccupiedTimeout(sens, num2);
    }
    if (!string.IsNullOrEmpty(collection["UnoccupiedHeatingSetpoint"]) && !string.IsNullOrEmpty(collection["UnoccupiedCoolingSetpoint"]))
    {
      double Fahrenheit1 = collection["UnoccupiedHeatingSetpoint"].ToDouble();
      double Fahrenheit2 = collection["UnoccupiedCoolingSetpoint"].ToDouble();
      if (flag)
      {
        Fahrenheit1 = Fahrenheit1.ToCelsius();
        Fahrenheit2 = Fahrenheit2.ToCelsius();
      }
      if (Fahrenheit1 > Fahrenheit2)
        Fahrenheit1 = Fahrenheit2 - 2.0;
      if (Fahrenheit1 < 10.0)
        Fahrenheit1 = 10.0;
      if (Fahrenheit2 < 10.0)
        Fahrenheit2 = 10.0;
      if (Fahrenheit1 > 37.0)
        Fahrenheit1 = 37.0;
      if (Fahrenheit2 > 37.0)
        Fahrenheit2 = 37.0;
      if (Fahrenheit2 == Fahrenheit1)
        Fahrenheit2 = Fahrenheit1 + 2.0;
      MultiStageThermostat.SetUnoccupiedHeatingSetpoint(sens, Fahrenheit1);
      MultiStageThermostat.SetUnoccupiedCoolingSetpoint(sens, Fahrenheit2);
    }
    if (!string.IsNullOrEmpty(collection["AwareWhenOccupied"]))
      MultiStageThermostat.SetAwareWhenOccupied(sens, collection["AwareWhenOccupied"].ToInt().ToBool());
    if (!string.IsNullOrEmpty(collection["AwareOnStateChange"]))
      MultiStageThermostat.SetAwareOnStateChange(sens, collection["AwareOnStateChange"].ToInt().ToBool());
    if (!string.IsNullOrEmpty(collection["Stage2CoolingActivationTime"]))
    {
      double num3 = collection["Stage2CoolingActivationTime"].ToDouble();
      if (num3 < 0.0)
        num3 = 0.0;
      if (num3 > 240.0)
        num3 = 240.0;
      MultiStageThermostat.SetStage2CoolingActivationTime(sens, num3);
    }
    if (!string.IsNullOrEmpty(collection["Stage2CoolingActivationThreshold"]))
    {
      double num4 = collection["Stage2CoolingActivationThreshold"].ToDouble();
      if (flag)
        num4 /= 1.8;
      if (num4 < (double) num1)
        num4 = (double) num1;
      if (num4 > 10.0)
        num4 = 10.0;
      MultiStageThermostat.SetStage2CoolingActivationThreshold(sens, num4);
    }
    if (!string.IsNullOrEmpty(collection["Stage2HeatingActivationTime"]))
    {
      double num5 = collection["Stage2HeatingActivationTime"].ToDouble();
      if (num5 < 0.0)
        num5 = 0.0;
      if (num5 > 240.0)
        num5 = 240.0;
      MultiStageThermostat.SetStage2HeatingActivationTime(sens, num5);
    }
    double heatingActivationTime1 = MultiStageThermostat.GetStage2HeatingActivationTime(sens);
    if (!string.IsNullOrEmpty(collection["Stage2HeatingActivationThreshold"]))
    {
      double num6 = collection["Stage2HeatingActivationThreshold"].ToDouble();
      if (flag)
        num6 /= 1.8;
      if (num6 < (double) num1)
        num6 = (double) num1;
      if (num6 > 10.0)
        num6 = 10.0;
      MultiStageThermostat.SetStage2HeatingActivationThreshold(sens, num6);
    }
    double activationThreshold1 = MultiStageThermostat.GetStage2HeatingActivationThreshold(sens);
    if (!string.IsNullOrEmpty(collection["Stage3HeatingActivationTime"]))
    {
      double num7 = collection["Stage3HeatingActivationTime"].ToDouble();
      if (num7 < heatingActivationTime1)
        num7 = heatingActivationTime1;
      if (num7 > 240.0)
        num7 = 240.0;
      MultiStageThermostat.SetStage3HeatingActivationTime(sens, num7);
    }
    double heatingActivationTime2 = MultiStageThermostat.GetStage3HeatingActivationTime(sens);
    if (!string.IsNullOrEmpty(collection["Stage3HeatingActivationThreshold"]))
    {
      double num8 = collection["Stage3HeatingActivationThreshold"].ToDouble();
      if (flag)
        num8 /= 1.8;
      if (num8 < activationThreshold1)
        num8 = activationThreshold1;
      if (num8 > 10.0)
        num8 = 10.0;
      MultiStageThermostat.SetStage3HeatingActivationThreshold(sens, num8);
    }
    double activationThreshold2 = MultiStageThermostat.GetStage3HeatingActivationThreshold(sens);
    if (!string.IsNullOrEmpty(collection["Stage4HeatingActivationTime"]))
    {
      double num9 = collection["Stage4HeatingActivationTime"].ToDouble();
      if (num9 < heatingActivationTime2)
        num9 = heatingActivationTime2;
      if (num9 > 240.0)
        num9 = 240.0;
      MultiStageThermostat.SetStage4HeatingActivationTime(sens, num9);
    }
    if (!string.IsNullOrEmpty(collection["Stage4HeatingActivationThreshold"]))
    {
      double num10 = collection["Stage4HeatingActivationThreshold"].ToDouble();
      if (flag)
        num10 /= 1.8;
      if (num10 < activationThreshold2)
        num10 = activationThreshold2;
      if (num10 > 10.0)
        num10 = 10.0;
      MultiStageThermostat.SetStage4HeatingActivationThreshold(sens, num10);
    }
    if (!string.IsNullOrEmpty(collection["EnableLoadBalancingHeater"]))
      MultiStageThermostat.SetEnableLoadBalancingHeater(sens, collection["EnableLoadBalancingHeater"].ToInt().ToBool());
    if (!string.IsNullOrEmpty(collection["EnableLoadBalancingCooler"]))
      MultiStageThermostat.SetEnableLoadBalancingCooler(sens, collection["EnableLoadBalancingCooler"].ToInt().ToBool());
    if (!string.IsNullOrEmpty(collection["ReversingValve"]))
      MultiStageThermostat.SetReversingValve(sens, collection["ReversingValve"].ToInt());
    if (!string.IsNullOrWhiteSpace(collection["FanControl"]))
    {
      int num11 = collection["FanControl"].ToInt();
      if (num11 < 0)
        num11 = 0;
      if (num11 > 4)
        num11 = 4;
      MultiStageThermostat.SetFanControl(sens, num11);
    }
    if (!string.IsNullOrWhiteSpace(collection["FanOnPeriod"]))
    {
      int num12 = collection["FanOnPeriod"].ToInt();
      if (num12 < 1)
        num12 = 1;
      if (num12 > 240 /*0xF0*/)
        num12 = 240 /*0xF0*/;
      MultiStageThermostat.SetFanOnPeriod(sens, num12);
    }
    if (!string.IsNullOrWhiteSpace(collection["FanOnInterval"]))
    {
      int num13 = collection["FanOnInterval"].ToInt();
      if (num13 < 5)
        num13 = 5;
      if (num13 > 720)
        num13 = 720;
      MultiStageThermostat.SetFanOnInterval(sens, (double) num13);
    }
    if (!string.IsNullOrWhiteSpace(collection["FanStartTimeForHeater"]))
    {
      int num14 = collection["FanStartTimeForHeater"].ToInt();
      if (num14 < -300)
        num14 = -300;
      if (num14 > 300)
        num14 = 300;
      MultiStageThermostat.SetFanStartTimeForHeater(sens, num14);
    }
    if (!string.IsNullOrWhiteSpace(collection["FanStopTimeForHeater"]))
    {
      int num15 = (int) Convert.ToInt16(collection["FanStopTimeForHeater"]);
      if (num15 < -300)
        num15 = -300;
      if (num15 > 300)
        num15 = 300;
      MultiStageThermostat.SetFanStopTimeForHeater(sens, num15);
    }
    if (!string.IsNullOrWhiteSpace(collection["FanStartDelayForCooler"]))
    {
      int num16 = (int) Convert.ToInt16(collection["FanStartDelayForCooler"]);
      if (num16 < -300)
        num16 = -300;
      if (num16 > 300)
        num16 = 300;
      MultiStageThermostat.SetFanStartDelayForCooler(sens, num16);
    }
    if (string.IsNullOrWhiteSpace(collection["FanStopDelayForCooler"]))
      return;
    int num17 = (int) Convert.ToInt16(collection["FanStopDelayForCooler"]);
    if (num17 < -300)
      num17 = -300;
    if (num17 > 300)
      num17 = 300;
    MultiStageThermostat.SetFanStopDelayForCooler(sens, num17);
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (collection["TempScale"] == "on")
    {
      viewData["TempScale"] = "F";
      MultiStageThermostat.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      viewData["TempScale"] = "C";
      MultiStageThermostat.MakeCelsius(sensor.SensorID);
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    switch (collection["CalibrationType"].ToInt())
    {
      case 4:
        MultiStageThermostat.SetCalibrationValue(sensor.SensorID, 4, collection["myval"].ToInt());
        sensor.PendingActionControlCommand = true;
        sensor.ProfileConfigDirty = false;
        sensor.ProfileConfig2Dirty = false;
        sensor.GeneralConfigDirty = false;
        sensor.GeneralConfig2Dirty = false;
        sensor.GeneralConfig3Dirty = false;
        break;
      case 6:
        MultiStageThermostat.SetCalibrationValue(sensor.SensorID, 6, collection["myval"].ToInt());
        sensor.PendingActionControlCommand = true;
        sensor.ProfileConfigDirty = false;
        sensor.ProfileConfig2Dirty = false;
        sensor.GeneralConfigDirty = false;
        sensor.GeneralConfig2Dirty = false;
        sensor.GeneralConfig3Dirty = false;
        break;
      case 7:
        MultiStageThermostat.SetCalibrationValue(sensor.SensorID, 7, collection["myval"].ToInt());
        sensor.PendingActionControlCommand = true;
        sensor.ProfileConfigDirty = false;
        sensor.ProfileConfig2Dirty = false;
        sensor.GeneralConfigDirty = false;
        sensor.GeneralConfig2Dirty = false;
        sensor.GeneralConfig3Dirty = false;
        break;
      case 10:
        int num1 = MultiStageThermostat.IsFahrenheit(sensor.SensorID) ? (collection["myval"].ToDouble() / 1.8 * 10.0).ToInt() : (collection["myval"].ToDouble() * 10.0).ToInt();
        if (num1 >= (int) sbyte.MinValue && num1 <= (int) sbyte.MaxValue)
        {
          MultiStageThermostat.SetTemperatureOffset(sensor, (double) Convert.ToSByte(num1));
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
          MultiStageThermostat.SetHumidityOffset(sensor, (double) num2);
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

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    SensorAttribute.ResetAttributeList(sensor.SensorID);
    List<byte[]> numArrayList = new List<byte[]>();
    string[] strArray = MultiStageThermostat.GetCalibrationValue(sensor.SensorID).Split('|');
    numArrayList.Add(MultiStageThermostatBase.Calibration(sensor.SensorID, Convert.ToByte(strArray[0].ToInt()), strArray[1].ToInt()));
    numArrayList.Add(sensor.ReadProfileConfig(29));
    return numArrayList;
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
          MultiStageThermostat.SetCalibrationValue(sensor.SensorID, 7, storedValue);
          sensor.PendingActionControlCommand = true;
          sensor.ProfileConfigDirty = false;
          sensor.ProfileConfig2Dirty = false;
          sensor.GeneralConfigDirty = false;
          sensor.GeneralConfig2Dirty = false;
          sensor.GeneralConfig3Dirty = false;
          break;
        case 1:
          MultiStageThermostat.SetCalibrationValue(sensor.SensorID, 6, storedValue);
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
    MultiStageThermostat.SetTemperatureOffset(sensor, 0.0);
    MultiStageThermostat.SetHumidityOffset(sensor, 0.0);
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
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
      ApplicationID = MultiStageThermostat.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion,
      Scale = MultiStageThermostat.IsFahrenheit(sensor.SensorID) ? "F" : "C"
    };
  }

  public static void SetOccupiedSetPoint(Sensor sensor, double value)
  {
    value *= 4.0;
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4294967040U;
    sensor.MinimumThreshold = (long) (num | (uint) value & (uint) byte.MaxValue);
  }

  public static double GetOccupiedSetPoint(Sensor sensor)
  {
    return (double) (Convert.ToUInt32(sensor.MinimumThreshold) & (uint) byte.MaxValue) / 4.0;
  }

  public static void SetTempDelta(Sensor sensor, double value)
  {
    value *= 4.0;
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4294902015U;
    sensor.MinimumThreshold = (long) (num | (uint) value << 8);
  }

  public static double GetTempDelta(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.MinimumThreshold) & 65280U) >> 8) / 4.0;
  }

  public static void SetUnoccupiedCoolingSetpoint(Sensor sensor, double value)
  {
    value *= 4.0;
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4278255615U;
    sensor.MinimumThreshold = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static double GetUnoccupiedCoolingSetpoint(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.MinimumThreshold) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/) / 4.0;
  }

  public static void SetUnoccupiedHeatingSetpoint(Sensor sensor, double value)
  {
    value *= 4.0;
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 16777215U /*0xFFFFFF*/;
    sensor.MinimumThreshold = (long) (num | (uint) value << 24);
  }

  public static double GetUnoccupiedHeatingSetpoint(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.MinimumThreshold) & 4278190080U /*0xFF000000*/) >> 24) / 4.0;
  }

  public static void SetStage2CoolingActivationTime(Sensor sensor, double value)
  {
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 4294967040U;
    sensor.MaximumThreshold = (long) (num | (uint) value & (uint) byte.MaxValue);
  }

  public static double GetStage2CoolingActivationTime(Sensor sensor)
  {
    return (double) (Convert.ToUInt32(sensor.MaximumThreshold) & (uint) byte.MaxValue);
  }

  public static void SetStage2CoolingActivationThreshold(Sensor sensor, double value)
  {
    value *= 4.0;
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 4294902015U;
    sensor.MaximumThreshold = (long) (num | (uint) value << 8);
  }

  public static double GetStage2CoolingActivationThreshold(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.MaximumThreshold) & 65280U) >> 8) / 4.0;
  }

  public static void SetStage2HeatingActivationTime(Sensor sensor, double value)
  {
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 4278255615U;
    sensor.MaximumThreshold = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static double GetStage2HeatingActivationTime(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.MaximumThreshold) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetStage2HeatingActivationThreshold(Sensor sensor, double value)
  {
    value *= 4.0;
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 16777215U /*0xFFFFFF*/;
    sensor.MaximumThreshold = (long) (num | (uint) value << 24);
  }

  public static double GetStage2HeatingActivationThreshold(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.MaximumThreshold) & 4278190080U /*0xFF000000*/) >> 24) / 4.0;
  }

  public static void SetStage3HeatingActivationTime(Sensor sensor, double value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4294967040U;
    sensor.Hysteresis = (long) (num | (uint) value & (uint) byte.MaxValue);
  }

  public static double GetStage3HeatingActivationTime(Sensor sensor)
  {
    return (double) (Convert.ToUInt32(sensor.Hysteresis) & (uint) byte.MaxValue);
  }

  public static void SetStage3HeatingActivationThreshold(Sensor sensor, double value)
  {
    value *= 4.0;
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4294902015U;
    sensor.Hysteresis = (long) (num | (uint) value << 8);
  }

  public static double GetStage3HeatingActivationThreshold(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.Hysteresis) & 65280U) >> 8) / 4.0;
  }

  public static void SetStage4HeatingActivationTime(Sensor sensor, double value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4278255615U;
    sensor.Hysteresis = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static double GetStage4HeatingActivationTime(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.Hysteresis) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetStage4HeatingActivationThreshold(Sensor sensor, double value)
  {
    value *= 4.0;
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 16777215U /*0xFFFFFF*/;
    sensor.Hysteresis = (long) (num | (uint) value << 24);
  }

  public static double GetStage4HeatingActivationThreshold(Sensor sensor)
  {
    return (double) ((Convert.ToUInt32(sensor.Hysteresis) & 4278190080U /*0xFF000000*/) >> 24) / 4.0;
  }

  public static void SetTemperatureOffset(Sensor sensor, double value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4294967040U;
    sensor.Calibration1 = (long) (num | (uint) value & (uint) byte.MaxValue);
  }

  public static double GetTemperatureOffset(Sensor sensor)
  {
    return (double) (short) ((double) (sbyte) ((int) Convert.ToUInt32(sensor.Calibration1) & (int) byte.MaxValue) / 10.0);
  }

  public static void SetHumidityOffset(Sensor sensor, double value)
  {
    int num = (int) ((long) Convert.ToInt32(sensor.Calibration1) & 4294902015L);
    sensor.Calibration1 = (long) (num | (int) value << 8);
  }

  public static double GetHumidityOffset(Sensor sensor)
  {
    return (double) ((Convert.ToInt32(sensor.Calibration1) & 65280) >> 8) / 10.0;
  }

  public static void SetAwareWhenOccupied(Sensor sensor, bool value)
  {
    sensor.Calibration1 = (long) Convert.ToUInt32(sensor.Calibration1).SetBit(value, 16 /*0x10*/);
  }

  public static bool GetAwareWhenOccupied(Sensor sensor)
  {
    return Convert.ToUInt32(sensor.Calibration1).GetBit(16 /*0x10*/);
  }

  public static void SetAwareOnStateChange(Sensor sensor, bool value)
  {
    sensor.Calibration1 = (long) Convert.ToUInt32(sensor.Calibration1).SetBit(value, 17);
  }

  public static bool GetAwareOnStateChange(Sensor sensor)
  {
    return Convert.ToUInt32(sensor.Calibration1).GetBit(17);
  }

  public static void SetEnableLoadBalancingHeater(Sensor sensor, bool value)
  {
    sensor.Calibration1 = (long) Convert.ToUInt32(sensor.Calibration1).SetBit(value, 18);
  }

  public static bool GetEnableLoadBalancingHeater(Sensor sensor)
  {
    return Convert.ToUInt32(sensor.Calibration1).GetBit(18);
  }

  public static void SetEnableLoadBalancingCooler(Sensor sensor, bool value)
  {
    sensor.Calibration1 = (long) Convert.ToUInt32(sensor.Calibration1).SetBit(value, 19);
  }

  public static bool GetEnableLoadBalancingCooler(Sensor sensor)
  {
    return Convert.ToUInt32(sensor.Calibration1).GetBit(19);
  }

  public static void SetEnableOccupancyDetection(Sensor sensor, bool value)
  {
    sensor.Calibration1 = (long) Convert.ToUInt32(sensor.Calibration1).SetBit(value, 20);
  }

  public static bool GetEnableOccupancyDetection(Sensor sensor)
  {
    return Convert.ToUInt32(sensor.Calibration1).GetBit(20);
  }

  public static void SetOccupiedTimeout(Sensor sensor, double value)
  {
    value /= 5.0;
    uint num = Convert.ToUInt32(sensor.Calibration2) & 4294967040U;
    sensor.Calibration2 = (long) (num | (uint) value & (uint) byte.MaxValue);
  }

  public static double GetOccupiedTimeout(Sensor sensor)
  {
    return (double) (Convert.ToInt32(sensor.Calibration2) & (int) byte.MaxValue) * 5.0;
  }

  public static void SetSystemType(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration2) & 4294902015U;
    sensor.Calibration2 = (long) (num | (uint) (value << 8));
  }

  public static int GetSystemType(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration2) & 65280U) >> 8);
  }

  public static void SetReversingValve(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration2) & 4278255615U;
    sensor.Calibration2 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int GetReversingValve(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration2) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetMinimumOffStateTime(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration2) & 16777215U /*0xFFFFFF*/;
    sensor.Calibration2 = (long) (num | (uint) (value << 24));
  }

  public static int GetMinimumOffStateTime(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration2) & 4278190080U /*0xFF000000*/) >> 24);
  }

  public static void SetFanControl(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration3) & 4294967040U;
    sensor.Calibration3 = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetFanControl(Sensor sensor)
  {
    return (int) (sbyte) ((int) Convert.ToUInt32(sensor.Calibration3) & (int) byte.MaxValue);
  }

  public static void SetFanOnPeriod(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration3) & 4294902015U;
    sensor.Calibration3 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static int GetFanOnPeriod(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration3) & 65280U) >> 8);
  }

  public static void SetFanOnInterval(Sensor sensor, double value)
  {
    value /= 5.0;
    uint num = Convert.ToUInt32(sensor.Calibration3) & 4278255615U;
    sensor.Calibration3 = (long) (num | (uint) (((int) (uint) value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static int GetFanOnInterval(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration3) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/) * 5;
  }

  public static void SetHeatCoolMode(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration3) & 16777215U /*0xFFFFFF*/;
    sensor.Calibration3 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 24));
  }

  public static sbyte GetHeatCoolMode(Sensor sensor)
  {
    return (sbyte) ((Convert.ToUInt32(sensor.Calibration3) & 4278190080U /*0xFF000000*/) >> 24);
  }

  public static void SetFanStartTimeForHeater(Sensor sensor, int value)
  {
    value /= 5;
    uint num = Convert.ToUInt32(sensor.Calibration4) & 4294967040U;
    sensor.Calibration4 = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetFanStartTimeForHeater(Sensor sensor)
  {
    return (int) (short) ((int) (sbyte) ((int) Convert.ToUInt32(sensor.Calibration4) & (int) byte.MaxValue) * 5);
  }

  public static void SetFanStopTimeForHeater(Sensor sensor, int value)
  {
    value /= 5;
    uint num = Convert.ToUInt32(sensor.Calibration4) & 4294902015U;
    sensor.Calibration4 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static short GetFanStopTimeForHeater(Sensor sensor)
  {
    return (short) ((int) (sbyte) ((Convert.ToUInt32(sensor.Calibration4) & 65280U) >> 8) * 5);
  }

  public static void SetFanStartDelayForCooler(Sensor sensor, int value)
  {
    value /= 5;
    uint num = Convert.ToUInt32(sensor.Calibration4) & 4278255615U;
    sensor.Calibration4 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static short GetFanStartDelayForCooler(Sensor sensor)
  {
    return (short) ((int) (sbyte) ((Convert.ToUInt32(sensor.Calibration4) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/) * 5);
  }

  public static void SetFanStopDelayForCooler(Sensor sensor, int value)
  {
    value /= 5;
    uint num = Convert.ToUInt32(sensor.Calibration4) & 16777215U /*0xFFFFFF*/;
    sensor.Calibration4 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 24));
  }

  public static short GetFanStopDelayForCooler(Sensor sensor)
  {
    return (short) ((int) (sbyte) ((Convert.ToUInt32(sensor.Calibration4) & 4278190080U /*0xFF000000*/) >> 24) * 5);
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(MultiStageThermostat left, MultiStageThermostat right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(MultiStageThermostat left, MultiStageThermostat right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(MultiStageThermostat left, MultiStageThermostat right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(MultiStageThermostat left, MultiStageThermostat right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(MultiStageThermostat left, MultiStageThermostat right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(MultiStageThermostat left, MultiStageThermostat right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is MultiStageThermostat && this.Equals((MonnitApplicationBase) (obj as MultiStageThermostat));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is MultiStageThermostat && this.Temperature == (right as MultiStageThermostat).Temperature;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is MultiStageThermostat && this.Temperature != (right as MultiStageThermostat).Temperature;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is MultiStageThermostat && this.Temperature < (right as MultiStageThermostat).Temperature;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is MultiStageThermostat && this.Temperature <= (right as MultiStageThermostat).Temperature;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is MultiStageThermostat && this.Temperature > (right as MultiStageThermostat).Temperature;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is MultiStageThermostat && this.Temperature >= (right as MultiStageThermostat).Temperature;
  }
}
