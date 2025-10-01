// Decompiled with JetBrains decompiler
// Type: Monnit.HumiditySHT25
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

public class HumiditySHT25 : MonnitApplicationBase, ISensorAttribute
{
  private double _DewPoint = double.MinValue;
  private double _MoistureWeight = double.MinValue;
  private double _HeatIndex = double.MinValue;
  private double _WetBulb = double.MinValue;
  internal SensorAttribute _CorF;
  private SensorAttribute _D;
  private SensorAttribute _DisplayGPP;
  private SensorAttribute _DisplayHeatIndex;
  private SensorAttribute _DisplayWetBulb;
  internal SensorAttribute _HumOffset;
  internal SensorAttribute _TempOffset;
  private SensorAttribute _Pressure;

  public static long MonnitApplicationID => 43;

  public static string ApplicationName => "Humidity";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => HumiditySHT25.MonnitApplicationID;

  public double Hum { get; set; }

  public double Temperature { get; set; }

  public double DewPoint
  {
    get
    {
      if (this._DewPoint == double.MinValue)
        this._DewPoint = HumiditySHT25.Dewpoint(this.Hum, this.Temperature);
      return this._DewPoint;
    }
    set => this._DewPoint = value;
  }

  public double MoistureWeight
  {
    get
    {
      if (this._MoistureWeight == double.MinValue)
        this._MoistureWeight = HumiditySHT25.GrainsPerPound(this.Hum, this.Temperature, this.Pressure);
      return this._MoistureWeight;
    }
    set => this._MoistureWeight = value;
  }

  public double HeatIndex
  {
    get
    {
      if (this._HeatIndex == double.MinValue)
        this._HeatIndex = HumiditySHT25.CalculateHeatIndex(this.Hum, this.Temperature);
      return this._HeatIndex;
    }
    set => this._HeatIndex = value;
  }

  public double WetBulb
  {
    get
    {
      if (this._WetBulb == double.MinValue)
        this._WetBulb = HumiditySHT25.WetBulbCalculation(this.Hum, this.Temperature);
      return this._WetBulb;
    }
    set => this._WetBulb = value;
  }

  public override string Serialize()
  {
    return $"{this.Hum},{this.Temperature},{this.DewPoint},{this.MoistureWeight},{this.HeatIndex},{this.WetBulb}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[6]
      {
        new AppDatum(eDatumType.Percentage, "Humidity", this.Hum),
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temperature),
        new AppDatum(eDatumType.TemperatureData, "DewPoint", this.DewPoint),
        new AppDatum(eDatumType.MoistureWeight, "MoistureWeight", this.MoistureWeight),
        new AppDatum(eDatumType.TemperatureData, "HeatIndex", this.HeatIndex),
        new AppDatum(eDatumType.TemperatureData, "WetBulb", this.WetBulb)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[6]
    {
      this.PlotValue,
      (object) this.PlotTemperatureValue,
      (object) this.PlotDewPointValue,
      (object) this.PlotMoistureWeightValue,
      (object) this.PlotHeatIndexValue,
      (object) this.PlotWetBulbValue
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[6]
    {
      "Humidity",
      HumiditySHT25.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius",
      "DewPoint",
      HumiditySHT25.IsFahrenheit(sensorID) ? "GrainsPerPound" : "GramsPerKilogram",
      HumiditySHT25.IsFahrenheit(sensorID) ? "HeatIndex_Fahrenheit" : "HeatIndex_Celsius",
      HumiditySHT25.IsFahrenheit(sensorID) ? "WetBulb_Fahrenheit" : "WetBulb_Celsius"
    });
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) HumiditySHT25.Deserialize(version, serialized);
  }

  public static bool IsFahrenheit(long sensorID) => Monnit.Temperature.IsFahrenheit(sensorID);

  public static void MakeFahrenheit(long sensorID) => Monnit.Temperature.MakeFahrenheit(sensorID);

  public static void MakeCelsius(long sensorID) => Monnit.Temperature.MakeCelsius(sensorID);

  public static bool UseInternalCalibration(Sensor sensor)
  {
    return HumiditySHT25Base.UseInternalCalibration(new Version(sensor.FirmwareVersion), sensor.GenerationType);
  }

  public SensorAttribute CorF => this._CorF;

  public SensorAttribute D => this._D;

  public SensorAttribute DisplayGPP => this._DisplayGPP;

  public SensorAttribute DisplayHeatIndex => this._DisplayHeatIndex;

  public SensorAttribute DisplayWetBulb => this._DisplayWetBulb;

  public SensorAttribute HumOffset => this._HumOffset;

  public double HumOffsetValue => this.HumOffset == null ? 0.0 : this.HumOffset.Value.ToDouble();

  public SensorAttribute TempOffset => this._TempOffset;

  public double TempOffsetValue => this.TempOffset == null ? 0.0 : this.TempOffset.Value.ToDouble();

  public static bool ShowHeatIndex(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HI" && sensorAttribute.Value == "T")
        return true;
    }
    return false;
  }

  public static void MakeHeatIndexVisible(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HI")
      {
        if (sensorAttribute.Value != "T")
        {
          sensorAttribute.Value = "T";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "HI",
        Value = "T",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void HideHeatIndex(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HI")
        sensorAttribute.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static bool ShowDewpoint(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "D" && sensorAttribute.Value == "T")
        return true;
    }
    return false;
  }

  public static void MakeDewpointVisible(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "D")
      {
        if (sensorAttribute.Value != "T")
        {
          sensorAttribute.Value = "T";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "D",
        Value = "T",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void HideDewpoint(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "D")
        sensorAttribute.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static bool ShowGPP(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "GPP" && sensorAttribute.Value == "T")
        return true;
    }
    return false;
  }

  public static void MakeGPPVisible(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "GPP")
      {
        if (sensorAttribute.Value != "T")
        {
          sensorAttribute.Value = "T";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "GPP",
        Value = "T",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void HideGPP(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "GPP")
        sensorAttribute.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static bool ShowWetBulb(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "WB" && sensorAttribute.Value == "T")
        return true;
    }
    return false;
  }

  public static void MakeWetBulbVisible(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "WB")
      {
        if (sensorAttribute.Value != "T")
        {
          sensorAttribute.Value = "T";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "WB",
        Value = "T",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void HideWetBulb(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "WB")
        sensorAttribute.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public void SetSensorAttributes(long sensorID)
  {
    bool flag = HumiditySHT25.UseInternalCalibration(Sensor.Load(sensorID));
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
      if (sensorAttribute.Name == "D")
        this._D = sensorAttribute;
      if (sensorAttribute.Name == "HI")
        this._DisplayHeatIndex = sensorAttribute;
      if (sensorAttribute.Name == "GPP")
        this._DisplayGPP = sensorAttribute;
      if (sensorAttribute.Name == "WB")
        this._DisplayWetBulb = sensorAttribute;
      if (sensorAttribute.Name == "Pressure")
        this._Pressure = sensorAttribute;
      if (sensorAttribute.Name == "HumOffset" && !flag)
        this._HumOffset = sensorAttribute;
      if (sensorAttribute.Name == "TempOffset" && !flag)
        this._TempOffset = sensorAttribute;
    }
    if (this._Pressure != null)
      return;
    this._Pressure = new SensorAttribute()
    {
      Name = "Pressure",
      SensorID = sensorID,
      Value = HumiditySHT25.CalcPressure(0).ToString()
    };
    this._Pressure.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public override bool IsValid => this.Hum != -100.0;

  public override string NotificationString
  {
    get
    {
      if (this.Hum == -100.0)
        return "Hardware Error";
      string empty = string.Empty;
      string str1 = string.Empty;
      string str2 = string.Empty;
      string str3 = string.Empty;
      string str4 = string.Empty;
      string str5;
      double num;
      if (this.CorF != null && this.CorF.Value == "C")
      {
        str5 = this.PlotTemperatureValue.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
        if (this.D != null && this.D.Value == "T")
          str1 = $", Dew Point: {this.DewPoint.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture)} °C";
        if (this.DisplayGPP != null && this.DisplayGPP.Value == "T")
          str2 = $", Moisture Weight: {(this.MoistureWeight / 7.0).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture)} g/kg";
        if (this.DisplayHeatIndex != null && this.DisplayHeatIndex.Value == "T")
          str3 = $", Heat Index: {this.HeatIndex.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture)} °C";
        if (this.DisplayWetBulb != null && this.DisplayWetBulb.Value == "T")
          str4 = $", Wet Bulb: {this.WetBulb.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture)} °C";
      }
      else
      {
        str5 = this.PlotTemperatureValue.ToString("#0.# °F", (IFormatProvider) CultureInfo.InvariantCulture);
        if (this.D != null && this.D.Value == "T")
        {
          num = this.DewPoint.ToFahrenheit();
          str1 = $", Dew Point: {num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture)}° F";
        }
        if (this.DisplayGPP != null && this.DisplayGPP.Value == "T")
        {
          num = this.MoistureWeight;
          str2 = $", Moisture Weight: {num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture)} gpp";
        }
        if (this.DisplayHeatIndex != null && this.DisplayHeatIndex.Value == "T")
        {
          num = this.HeatIndex.ToFahrenheit();
          str3 = $", Heat Index: {num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture)} °F";
        }
        if (this.DisplayWetBulb != null && this.DisplayWetBulb.Value == "T")
        {
          num = this.WetBulb.ToFahrenheit();
          str4 = $", Wet Bulb: {num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture)} °F";
        }
      }
      object[] objArray = new object[6];
      num = this.PlotValue.ToDouble();
      objArray[0] = (object) num.ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture);
      objArray[1] = (object) str5;
      objArray[2] = (object) str1;
      objArray[3] = (object) str2;
      objArray[4] = (object) str3;
      objArray[5] = (object) str4;
      return string.Format("{0}% @ {1}{2}{3}{4}{5}", objArray);
    }
  }

  public static string GetLabel(long sensorID)
  {
    return HumiditySHT25.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celcius";
  }

  public override object PlotValue
  {
    get
    {
      return this.Hum > -100.0 && this.Hum < 300.0 ? (object) (this.Hum + this.HumOffsetValue) : (object) 0;
    }
  }

  public double PlotTemperatureValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? this.Temperature + this.TempOffsetValue : (this.Temperature + this.TempOffsetValue).ToFahrenheit();
    }
  }

  public double PlotDewPointValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? this.DewPoint : this.DewPoint.ToFahrenheit();
    }
  }

  public double PlotMoistureWeightValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? this.MoistureWeight / 7.0 : this.MoistureWeight;
    }
  }

  public double PlotHeatIndexValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? this.HeatIndex : this.HeatIndex.ToFahrenheit();
    }
  }

  public double PlotWetBulbValue
  {
    get => this.CorF != null && this.CorF.Value == "C" ? this.WetBulb : this.WetBulb.ToFahrenheit();
  }

  public static HumiditySHT25 Deserialize(string version, string serialized)
  {
    HumiditySHT25 humidityShT25 = new HumiditySHT25();
    if (string.IsNullOrEmpty(serialized))
    {
      humidityShT25.Hum = 0.0;
      humidityShT25.Temperature = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      humidityShT25.Hum = strArray[0].ToDouble();
      if (strArray.Length > 1)
      {
        humidityShT25.Temperature = strArray[1].ToDouble();
        if (strArray.Length > 2)
        {
          humidityShT25.DewPoint = strArray[2].ToDouble();
          humidityShT25.MoistureWeight = strArray[3].ToDouble();
          if (strArray.Length > 4)
          {
            humidityShT25.HeatIndex = strArray[4].ToDouble();
            if (strArray.Length > 5)
              humidityShT25.WetBulb = strArray[5].ToDouble();
          }
        }
      }
      else
      {
        humidityShT25.Temperature = strArray[0].ToDouble();
        humidityShT25.DewPoint = strArray[0].ToDouble();
        humidityShT25.MoistureWeight = strArray[0].ToDouble();
        humidityShT25.HeatIndex = strArray[0].ToDouble();
        humidityShT25.WetBulb = strArray[0].ToDouble();
      }
    }
    return humidityShT25;
  }

  public static HumiditySHT25 Create(byte[] sdm, int startIndex)
  {
    HumiditySHT25 humidityShT25 = new HumiditySHT25();
    if (((int) sdm[startIndex - 1] & 240 /*0xF0*/) == 0)
    {
      humidityShT25.Temperature = BitConverter.ToInt16(sdm, startIndex).ToDouble() / 100.0;
      humidityShT25.Hum = BitConverter.ToInt16(sdm, startIndex + 2).ToDouble() / 100.0;
    }
    else
    {
      humidityShT25.Temperature = 0.0;
      humidityShT25.Hum = -100.0;
    }
    return humidityShT25;
  }

  public static double Dewpoint(double Humidity, double Temperature)
  {
    double num = 0.66077 + 7.5 * Temperature / (237.3 + Temperature) + (Math.Log10(Humidity) - 2.0);
    return Math.Round((num - 0.66077) * 237.3 / (8.16077 - num), 1);
  }

  public static double CalculateHeatIndex(double Humidity, double Temperature)
  {
    double fahrenheit = Temperature.ToFahrenheit();
    double Fahrenheit = 0.5 * (fahrenheit + 61.0 + (fahrenheit - 68.0) * 1.2 + Humidity * 0.094);
    if ((Fahrenheit + fahrenheit) / 2.0 >= 80.0)
    {
      double num1 = (13.0 - Humidity) / 4.0 * Math.Sqrt((17.0 - Math.Abs(fahrenheit - 95.0)) / 17.0);
      double num2 = (Humidity - 85.0) / 10.0 * ((87.0 - fahrenheit) / 5.0);
      Fahrenheit = 2.04901523 * fahrenheit - 42.379 + 10.14333127 * Humidity - 0.22475541 * fahrenheit * Humidity - 0.00683783 * fahrenheit * fahrenheit - 0.05481717 * Humidity * Humidity + 0.00122874 * fahrenheit * fahrenheit * Humidity + 0.00085282 * fahrenheit * Humidity * Humidity - 1.99E-06 * fahrenheit * fahrenheit * Humidity * Humidity;
      if (Humidity < 13.0 && fahrenheit >= 80.0 && fahrenheit <= 112.0)
        Fahrenheit -= num1;
      if (Humidity > 85.0 && fahrenheit >= 80.0 && fahrenheit <= 87.0)
        Fahrenheit += num2;
    }
    return Math.Round(Fahrenheit.ToCelsius(), 1);
  }

  public static double GrainsPerPound(double Humidity, double Temperature, double Pressure)
  {
    double num = Math.Pow(10.0, 8.07131 - 1730.63 / (Temperature + 233.426)) * 14.7 / 760.0;
    return Math.Round(4372.270363951473 * num / (Pressure - num) * (Humidity / 100.0), 1);
  }

  public static void SetPressure(long sensorID, double pressure)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Pressure")
      {
        sensorAttribute.Value = pressure.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "Pressure",
        SensorID = sensorID,
        Value = HumiditySHT25.CalcPressure(0).ToString()
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static double GetAltitude(long sensorID)
  {
    double num = 0.0;
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Pressure")
      {
        num = sensorAttribute.Value.ToDouble();
        flag = true;
      }
    }
    return !flag ? 0.0 : (double) ((int) Math.Round(Math.Log(num / 14.696) / -0.012 * 328.0 / 10.0) * 10);
  }

  private double Pressure
  {
    get => this._Pressure == null ? HumiditySHT25.CalcPressure(0) : this._Pressure.Value.ToDouble();
  }

  public static double CalcPressure(int altitude)
  {
    return -4.6E-14 * Math.Pow((double) altitude, 3.0) + 7.60649E-09 * Math.Pow((double) altitude, 2.0) - 0.000530833868239 * (double) altitude + 14.6969012115013;
  }

  public static double WetBulbCalculation(double relativeHumidity, double dryBulb)
  {
    return Math.Round(Math.Round(dryBulb * Math.Atan(0.151977 * Math.Pow(relativeHumidity + 8.313659, 0.5)) + Math.Atan(dryBulb + relativeHumidity) - Math.Atan(relativeHumidity - 1.676331) + 0.00391838 * Math.Pow(relativeHumidity, 1.5) * Math.Atan(0.023101 * relativeHumidity) - 4.686035, 2), 1);
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

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    HumiditySHT25Base.GetDefaults(new Version(sensor.FirmwareVersion), sensor.GenerationType);
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
    Sensor.ClearCache(sensor.SensorID);
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = HumiditySHT25.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  private static long KeepBitNegative(long number)
  {
    return (long) BitConverter.ToInt16(BitConverter.GetBytes(number), 0);
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
      double num1 = collection["Hysteresis_Manual"].ToDouble();
      if (num1 < 0.0)
        num1 = 0.0;
      if (num1 > 10.0)
        num1 = 10.0;
      int num2 = (num1 * 100.0).ToInt() << 16 /*0x10*/;
      double num3 = collection["Hysteresis_ManualTemp"].ToDouble();
      if (HumiditySHT25.IsFahrenheit(sensor.SensorID))
        num3 = num3 * 5.0 / 9.0;
      if (num3 < 0.0)
        num3 = 0.0;
      if (num3 > 10.0)
        num3 = 10.0;
      int num4 = (num3 * 100.0).ToInt();
      sensor.Hysteresis = (long) ((int) ((long) num2 & 4294901760L) | (int) (ushort) num4);
    }
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      double num5 = collection["MinimumThreshold_ManualTemp"].ToDouble();
      if (HumiditySHT25.IsFahrenheit(sensor.SensorID))
        num5 = (num5 - 32.0) * 5.0 / 9.0;
      int num6 = (collection["MinimumThreshold_Manual"].ToDouble() * 100.0).ToInt() << 16 /*0x10*/;
      int num7 = (num5 * 100.0).ToInt();
      sensor.MinimumThreshold = (long) ((int) ((long) num6 & 4294901760L) | (int) (ushort) num7);
      if (HumiditySHT25.KeepBitNegative(sensor.MinimumThreshold & (long) ushort.MaxValue) < HumiditySHT25.KeepBitNegative(sensor.DefaultValue<long>("DefaultMinimumThreshold") & (long) ushort.MaxValue) || HumiditySHT25.KeepBitNegative(sensor.MinimumThreshold & 4294901760L) < HumiditySHT25.KeepBitNegative(sensor.DefaultValue<long>("DefaultMinimumThreshold") & 4294901760L))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (HumiditySHT25.KeepBitNegative(sensor.MinimumThreshold & (long) ushort.MaxValue) > HumiditySHT25.KeepBitNegative(sensor.DefaultValue<long>("DefaultMaximumThreshold") & (long) ushort.MaxValue) || (long) BitConverter.ToInt16(BitConverter.GetBytes(sensor.MinimumThreshold & 4294901760L), 0) > (sensor.DefaultValue<long>("DefaultMaximumThreshold") & 4294901760L))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    }
    if (HumiditySHT25.KeepBitNegative(sensor.MinimumThreshold & 4294901760L) > HumiditySHT25.KeepBitNegative(sensor.MaximumThreshold & 4294901760L))
      sensor.MinimumThreshold = (sensor.MaximumThreshold & (long) ushort.MaxValue) + (sensor.MinimumThreshold & 4294901760L);
    if (HumiditySHT25.KeepBitNegative(sensor.MinimumThreshold & (long) ushort.MaxValue) > HumiditySHT25.KeepBitNegative(sensor.MaximumThreshold & (long) ushort.MaxValue))
      sensor.MinimumThreshold = (sensor.MaximumThreshold & 4294901760L) + (sensor.MinimumThreshold & (long) ushort.MaxValue);
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      int num8 = (collection["MaximumThreshold_Manual"].ToDouble() * 100.0).ToInt() << 16 /*0x10*/;
      double num9 = collection["MaximumThreshold_ManualTemp"].ToDouble();
      if (HumiditySHT25.IsFahrenheit(sensor.SensorID))
        num9 = (num9 - 32.0) * 5.0 / 9.0;
      int num10 = (num9 * 100.0).ToInt();
      sensor.MaximumThreshold = (long) ((int) ((long) num8 & 4294901760L) | (int) (ushort) num10);
      if (HumiditySHT25.KeepBitNegative(sensor.MaximumThreshold & (long) ushort.MaxValue) < HumiditySHT25.KeepBitNegative(sensor.DefaultValue<long>("DefaultMinimumThreshold") & (long) ushort.MaxValue) || HumiditySHT25.KeepBitNegative(sensor.MaximumThreshold & 4294901760L) < HumiditySHT25.KeepBitNegative(sensor.DefaultValue<long>("DefaultMinimumThreshold") & 4294901760L))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (HumiditySHT25.KeepBitNegative(sensor.MaximumThreshold & (long) ushort.MaxValue) > HumiditySHT25.KeepBitNegative(sensor.DefaultValue<long>("DefaultMaximumThreshold") & (long) ushort.MaxValue) || HumiditySHT25.KeepBitNegative(sensor.MaximumThreshold & 4294901760L) > HumiditySHT25.KeepBitNegative(sensor.DefaultValue<long>("DefaultMaximumThreshold") & 4294901760L))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    }
    if (HumiditySHT25.KeepBitNegative(sensor.MinimumThreshold & 4294901760L) > HumiditySHT25.KeepBitNegative(sensor.MaximumThreshold & 4294901760L))
      sensor.MaximumThreshold = (sensor.MaximumThreshold & (long) ushort.MaxValue) + (sensor.MinimumThreshold & 4294901760L);
    if (HumiditySHT25.KeepBitNegative(sensor.MinimumThreshold & (long) ushort.MaxValue) > HumiditySHT25.KeepBitNegative(sensor.MaximumThreshold & (long) ushort.MaxValue))
      sensor.MaximumThreshold = (sensor.MaximumThreshold & 4294901760L) + (sensor.MinimumThreshold & (long) ushort.MaxValue);
    if (!string.IsNullOrEmpty(collection["BasicThreshold"]) && !string.IsNullOrEmpty(collection["BasicThresholdDirection"]) && collection["BasicThresholdDirection"] != "-1")
    {
      if (collection["BasicThresholdDirection"].ToInt() == 0)
      {
        sensor.MinimumThreshold = (long) ((collection["BasicThreshold"].ToDouble() * 100.0).ToInt() << 16 /*0x10*/);
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      }
      else if (collection["BasicThresholdDirection"].ToInt() == 1)
      {
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
        sensor.MaximumThreshold = (long) ((collection["BasicThreshold"].ToDouble() * 100.0).ToInt() << 16 /*0x10*/);
      }
      sensor.MinimumThreshold += 60536L;
      sensor.MaximumThreshold += 15000L;
      sensor.MeasurementsPerTransmission = 6;
    }
    if (collection["ShowDewpoint"] == "on")
    {
      viewData["ShowDewpoint"] = "true";
      HumiditySHT25.MakeDewpointVisible(sensor.SensorID);
    }
    else
    {
      viewData["ShowDewpoint"] = "false";
      HumiditySHT25.HideDewpoint(sensor.SensorID);
    }
    if (collection["ShowHeatIndex"] == "on")
    {
      viewData["ShowHeatIndex"] = "true";
      HumiditySHT25.MakeHeatIndexVisible(sensor.SensorID);
    }
    else
    {
      viewData["ShowHeatIndex"] = "false";
      HumiditySHT25.HideHeatIndex(sensor.SensorID);
    }
    if (collection["ShowGPP"] == "on")
    {
      viewData["ShowGPP"] = "true";
      HumiditySHT25.MakeGPPVisible(sensor.SensorID);
      if (!string.IsNullOrEmpty(collection["Altitude"]))
        HumiditySHT25.SetPressure(sensor.SensorID, HumiditySHT25.CalcPressure(collection["Altitude"].ToInt()));
    }
    else
    {
      viewData["ShowGPP"] = "false";
      HumiditySHT25.HideGPP(sensor.SensorID);
    }
    if (collection["ShowWetBulb"] == "on")
    {
      viewData["ShowWetBulb"] = "true";
      HumiditySHT25.MakeWetBulbVisible(sensor.SensorID);
    }
    else
    {
      viewData["ShowWetBulb"] = "false";
      HumiditySHT25.HideWetBulb(sensor.SensorID);
    }
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : sensor.Hysteresis.ToDouble().ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : sensor.MinimumThreshold.ToDouble().ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : sensor.MaximumThreshold.ToDouble().ToString();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(HumiditySHT25 left, HumiditySHT25 right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(HumiditySHT25 left, HumiditySHT25 right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(HumiditySHT25 left, HumiditySHT25 right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(HumiditySHT25 left, HumiditySHT25 right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(HumiditySHT25 left, HumiditySHT25 right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(HumiditySHT25 left, HumiditySHT25 right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is HumiditySHT25 && this.Equals((MonnitApplicationBase) (obj as HumiditySHT25));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is HumiditySHT25 && this.Hum == (right as HumiditySHT25).Hum;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is HumiditySHT25 && this.Hum != (right as HumiditySHT25).Hum;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is HumiditySHT25 && this.Hum < (right as HumiditySHT25).Hum;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is HumiditySHT25 && this.Hum <= (right as HumiditySHT25).Hum;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is HumiditySHT25 && this.Hum > (right as HumiditySHT25).Hum;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is HumiditySHT25 && this.Hum >= (right as HumiditySHT25).Hum;
  }

  public static long DefaultMinThreshold => 60536;

  public static long DefaultMaxThreshold => 655375000;

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (collection["TempScale"] == "on")
    {
      viewData["TempScale"] = "F";
      HumiditySHT25.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      viewData["TempScale"] = "C";
      HumiditySHT25.MakeCelsius(sensor.SensorID);
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (HumiditySHT25.UseInternalCalibration(sensor))
    {
      bool flag1 = false;
      if (!string.IsNullOrEmpty(collection["TempOffset"]))
      {
        double num = collection["TempOffset"].ToDouble();
        if (HumiditySHT25.IsFahrenheit(sensor.SensorID))
          num /= 1.8;
        if (num < -50.0)
          num = -50.0;
        if (num > 50.0)
          num = 50.0;
        sensor.Calibration1 = (num * 100.0).ToLong();
        flag1 = true;
      }
      if (!string.IsNullOrEmpty(collection["HumOffset"]))
      {
        double num = collection["HumOffset"].ToDouble();
        if (num < -50.0)
          num = -50.0;
        if (num > 50.0)
          num = 50.0;
        sensor.Calibration2 = (num * 100.0).ToLong();
        flag1 = true;
      }
      if (flag1)
        sensor.Save();
      bool flag2 = false;
      bool flag3 = false;
      foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensor.SensorID))
      {
        if (sensorAttribute.Name == "HumOffset")
        {
          flag2 = true;
          sensorAttribute.Delete();
        }
        if (sensorAttribute.Name == "TempOffset")
        {
          flag3 = true;
          sensorAttribute.Delete();
        }
      }
      if (!(flag2 | flag3))
        return;
      SensorAttribute.ResetAttributeList(sensor.SensorID);
      Sensor.ClearCache(sensor.SensorID);
    }
    else
    {
      bool flag4 = false;
      bool flag5 = false;
      foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensor.SensorID))
      {
        if (sensorAttribute.Name == "HumOffset")
        {
          double num1 = sensorAttribute.Value.ToDouble();
          double num2 = collection["observed"].ToDouble() - num1;
          flag4 = true;
          sensorAttribute.Value = (collection["actual"].ToDouble() - num2).ToString();
          sensorAttribute.Save();
        }
        if (sensorAttribute.Name == "TempOffset")
        {
          double num3 = sensorAttribute.Value.ToDouble();
          double celsius1 = collection["observedTemp"].ToDouble();
          double celsius2 = collection["actualTemp"].ToDouble();
          if (HumiditySHT25.IsFahrenheit(sensor.SensorID))
          {
            celsius1 = celsius1.ToCelsius();
            celsius2 = celsius2.ToCelsius();
          }
          double num4 = celsius1 - num3;
          flag5 = true;
          sensorAttribute.Value = (celsius2 - num4).ToString();
          sensorAttribute.Save();
        }
      }
      if (!flag4)
      {
        string o1 = collection["observed"].ToString();
        string o2 = collection["actual"].ToString();
        if (o1.ToDouble() != double.MinValue)
          new SensorAttribute()
          {
            SensorID = sensor.SensorID,
            Name = "HumOffset",
            Value = (o2.ToDouble() - o1.ToDouble()).ToString()
          }.Save();
      }
      if (!flag5)
      {
        string o3 = collection["observedTemp"].ToString();
        string o4 = collection["actualTemp"].ToString();
        if (o3.ToDouble() != double.MinValue)
          new SensorAttribute()
          {
            SensorID = sensor.SensorID,
            Name = "TempOffset",
            Value = (!HumiditySHT25.IsFahrenheit(sensor.SensorID) ? (o4.ToDouble() - o3.ToDouble()).ToString() : (o4.ToDouble().ToCelsius() - o3.ToDouble().ToCelsius()).ToString())
          }.Save();
      }
      SensorAttribute.ResetAttributeList(sensor.SensorID);
      Sensor.ClearCache(sensor.SensorID);
    }
  }
}
