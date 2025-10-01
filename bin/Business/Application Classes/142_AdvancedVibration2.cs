// Decompiled with JetBrains decompiler
// Type: Monnit.AdvancedVibration2
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;

#nullable disable
namespace Monnit;

public class AdvancedVibration2 : MonnitApplicationBase, ISensorAttribute
{
  internal SensorAttribute _CorF;
  internal SensorAttribute _HZorRPM;
  private SensorAttribute _DataDisplayMode;
  internal SensorAttribute _AccelerationScale;
  internal SensorAttribute _DisplacementScale;
  internal SensorAttribute _VelocityScale;

  public static long MonnitApplicationID => 142;

  public static string ApplicationName => "Advanced Vibration 2";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => AdvancedVibration2.MonnitApplicationID;

  public int Mode { get; set; }

  public int DataMode { get; set; }

  public double FrequencyAxisX { get; set; }

  public double FrequencyAxisY { get; set; }

  public double FrequencyAxisZ { get; set; }

  public double VelocityRmsAxisX { get; set; }

  public double VelocityRmsAxisY { get; set; }

  public double VelocityRmsAxisZ { get; set; }

  public long AccelerationRmsAxisX { get; set; }

  public long AccelerationRmsAxisY { get; set; }

  public long AccelerationRmsAxisZ { get; set; }

  public double AccelerationPeakAxisX { get; set; }

  public double AccelerationPeakAxisY { get; set; }

  public double AccelerationPeakAxisZ { get; set; }

  public double DisplacementAxisX { get; set; }

  public double DisplacementAxisY { get; set; }

  public double DisplacementAxisZ { get; set; }

  public double CrestFactorAxisX { get; set; }

  public double CrestFactorAxisY { get; set; }

  public double CrestFactorAxisZ { get; set; }

  public int DutyCycle { get; set; }

  public double Temperature { get; set; }

  public int stsStatus { get; set; }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[21]
      {
        new AppDatum(eDatumType.State, "Mode", this.Mode),
        new AppDatum(eDatumType.Frequency, "FrequencyAxisX", this.FrequencyAxisX),
        new AppDatum(eDatumType.Frequency, "FrequencyAxisY", this.FrequencyAxisY),
        new AppDatum(eDatumType.Frequency, "FrequencyAxisZ", this.FrequencyAxisZ),
        new AppDatum(eDatumType.Speed, "VelocityRmsAxisX", this.VelocityRmsAxisX),
        new AppDatum(eDatumType.Speed, "VelocityRmsAxisY", this.VelocityRmsAxisY),
        new AppDatum(eDatumType.Speed, "VelocityRmsAxisZ", this.VelocityRmsAxisZ),
        new AppDatum(eDatumType.Geforce, "AccelerationRmsAxisX", this.AccelerationRmsAxisX),
        new AppDatum(eDatumType.Geforce, "AccelerationRmsAxisY", this.AccelerationRmsAxisY),
        new AppDatum(eDatumType.Geforce, "AccelerationRmsAxisZ", this.AccelerationRmsAxisZ),
        new AppDatum(eDatumType.Geforce, "AccelerationPeakAxisX", this.AccelerationPeakAxisX),
        new AppDatum(eDatumType.Geforce, "AccelerationPeakAxisY", this.AccelerationPeakAxisY),
        new AppDatum(eDatumType.Geforce, "AccelerationPeakAxisZ", this.AccelerationPeakAxisZ),
        new AppDatum(eDatumType.Geforce, "DisplacementAxisX", this.DisplacementAxisX),
        new AppDatum(eDatumType.Geforce, "DisplacementAxisY", this.DisplacementAxisY),
        new AppDatum(eDatumType.Geforce, "DisplacementAxisZ", this.DisplacementAxisZ),
        new AppDatum(eDatumType.Percentage, "CrestFactorAxisX", this.CrestFactorAxisX),
        new AppDatum(eDatumType.Percentage, "CrestFactorAxisY", this.CrestFactorAxisY),
        new AppDatum(eDatumType.Percentage, "CrestFactorAxisZ", this.CrestFactorAxisZ),
        new AppDatum(eDatumType.Percentage, "DutyCycle", this.DutyCycle),
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temperature)
      });
    }
  }

  public override bool IsValid => true;

  public override List<object> GetPlotValues(long sensorID)
  {
    switch (this.Mode)
    {
      case 0:
        return new List<object>((IEnumerable<object>) new object[21]
        {
          (object) this.Mode,
          (object) (AdvancedVibration2.IsHertz(sensorID) ? this.FrequencyAxisX : this.FrequencyAxisX.ToRPM()),
          (object) (AdvancedVibration2.IsHertz(sensorID) ? this.FrequencyAxisY : this.FrequencyAxisY.ToRPM()),
          (object) (AdvancedVibration2.IsHertz(sensorID) ? this.FrequencyAxisZ : this.FrequencyAxisZ.ToRPM()),
          (object) this.VelocityRmsAxisX,
          (object) this.VelocityRmsAxisY,
          (object) this.VelocityRmsAxisZ,
          null,
          null,
          null,
          null,
          null,
          null,
          null,
          null,
          null,
          (object) this.CrestFactorAxisX,
          (object) this.CrestFactorAxisY,
          (object) this.CrestFactorAxisZ,
          (object) this.DutyCycle,
          this.PlotTemperatureValue
        });
      case 1:
        return new List<object>((IEnumerable<object>) new object[21]
        {
          (object) this.Mode,
          (object) (AdvancedVibration2.IsHertz(sensorID) ? this.FrequencyAxisX : this.FrequencyAxisX.ToRPM()),
          (object) (AdvancedVibration2.IsHertz(sensorID) ? this.FrequencyAxisY : this.FrequencyAxisY.ToRPM()),
          (object) (AdvancedVibration2.IsHertz(sensorID) ? this.FrequencyAxisZ : this.FrequencyAxisZ.ToRPM()),
          null,
          null,
          null,
          (object) this.AccelerationRmsAxisX,
          (object) this.AccelerationRmsAxisY,
          (object) this.AccelerationRmsAxisZ,
          null,
          null,
          null,
          null,
          null,
          null,
          (object) this.CrestFactorAxisX,
          (object) this.CrestFactorAxisY,
          (object) this.CrestFactorAxisZ,
          (object) this.DutyCycle,
          this.PlotTemperatureValue
        });
      case 2:
        return new List<object>((IEnumerable<object>) new object[21]
        {
          (object) this.Mode,
          (object) (AdvancedVibration2.IsHertz(sensorID) ? this.FrequencyAxisX : this.FrequencyAxisX.ToRPM()),
          (object) (AdvancedVibration2.IsHertz(sensorID) ? this.FrequencyAxisY : this.FrequencyAxisY.ToRPM()),
          (object) (AdvancedVibration2.IsHertz(sensorID) ? this.FrequencyAxisZ : this.FrequencyAxisZ.ToRPM()),
          null,
          null,
          null,
          null,
          null,
          null,
          (object) this.AccelerationPeakAxisX,
          (object) this.AccelerationPeakAxisY,
          (object) this.AccelerationPeakAxisZ,
          null,
          null,
          null,
          (object) this.CrestFactorAxisX,
          (object) this.CrestFactorAxisY,
          (object) this.CrestFactorAxisZ,
          (object) this.DutyCycle,
          this.PlotTemperatureValue
        });
      case 4:
        return new List<object>((IEnumerable<object>) new object[21]
        {
          (object) this.Mode,
          (object) (AdvancedVibration2.IsHertz(sensorID) ? this.FrequencyAxisX : this.FrequencyAxisX.ToRPM()),
          (object) (AdvancedVibration2.IsHertz(sensorID) ? this.FrequencyAxisY : this.FrequencyAxisY.ToRPM()),
          (object) (AdvancedVibration2.IsHertz(sensorID) ? this.FrequencyAxisZ : this.FrequencyAxisZ.ToRPM()),
          null,
          null,
          null,
          null,
          null,
          null,
          null,
          null,
          null,
          (object) this.DisplacementAxisX,
          (object) this.DisplacementAxisY,
          (object) this.DisplacementAxisZ,
          (object) this.CrestFactorAxisX,
          (object) this.CrestFactorAxisY,
          (object) this.CrestFactorAxisZ,
          (object) this.DutyCycle,
          this.PlotTemperatureValue
        });
      default:
        object[] collection = new object[21];
        collection[0] = (object) this.Mode;
        collection[1] = (object) (AdvancedVibration2.IsHertz(sensorID) ? this.FrequencyAxisX : this.FrequencyAxisX.ToRPM());
        collection[2] = (object) (AdvancedVibration2.IsHertz(sensorID) ? this.FrequencyAxisY : this.FrequencyAxisY.ToRPM());
        collection[3] = (object) (AdvancedVibration2.IsHertz(sensorID) ? this.FrequencyAxisZ : this.FrequencyAxisZ.ToRPM());
        collection[16 /*0x10*/] = (object) this.CrestFactorAxisX;
        collection[17] = (object) this.CrestFactorAxisY;
        collection[18] = (object) this.CrestFactorAxisZ;
        collection[19] = (object) this.DutyCycle;
        collection[20] = this.PlotTemperatureValue;
        return new List<object>((IEnumerable<object>) collection);
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    switch (this.Mode)
    {
      case 0:
        return new List<string>((IEnumerable<string>) new string[21]
        {
          " ",
          AdvancedVibration2.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration2.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration2.IsHertz(sensorID) ? "Hz" : "rpm",
          "mm/s",
          "mm/s",
          "mm/s",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          "%",
          AdvancedVibration2.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
        });
      case 1:
        return new List<string>((IEnumerable<string>) new string[21]
        {
          " ",
          AdvancedVibration2.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration2.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration2.IsHertz(sensorID) ? "Hz" : "rpm",
          " ",
          " ",
          " ",
          "mm/s^2",
          "mm/s^2",
          "mm/s^2",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          "%",
          AdvancedVibration2.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
        });
      case 2:
        return new List<string>((IEnumerable<string>) new string[21]
        {
          " ",
          AdvancedVibration2.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration2.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration2.IsHertz(sensorID) ? "Hz" : "rpm",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          "mm/s^2",
          "mm/s^2",
          "mm/s^2",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          "%",
          AdvancedVibration2.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
        });
      case 4:
        return new List<string>((IEnumerable<string>) new string[21]
        {
          " ",
          AdvancedVibration2.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration2.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration2.IsHertz(sensorID) ? "Hz" : "rpm",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          "mm p-p",
          "mm p-p",
          "mm p-p",
          " ",
          " ",
          " ",
          "%",
          AdvancedVibration2.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
        });
      default:
        return new List<string>((IEnumerable<string>) new string[21]
        {
          " ",
          AdvancedVibration2.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration2.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration2.IsHertz(sensorID) ? "Hz" : "rpm",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          "%",
          AdvancedVibration2.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
        });
    }
  }

  public override string NotificationString
  {
    get
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      if (this.stsStatus < 8)
      {
        bool flag = true;
        if (this.HZorRPM != null && this.HZorRPM.Value == "RPM")
          flag = false;
        stringBuilder1.Append("Data Mode: " + this.parseDataMode(this.DataMode));
        stringBuilder1.Append($": Frequency X: {(flag ? this.FrequencyAxisX : this.FrequencyAxisX.ToRPM()).ToString("0.#")}{(flag ? " Hz" : " RPM")} Y: {(flag ? this.FrequencyAxisY : this.FrequencyAxisY.ToRPM()).ToString("0.#")}{(flag ? " Hz" : " RPM")} Z: {(flag ? this.FrequencyAxisZ : this.FrequencyAxisZ.ToRPM()).ToString("0.#")}{(flag ? " Hz" : " RPM")} , ");
        switch (this.Mode)
        {
          case 0:
            stringBuilder1.Append($"Velocity rms X: {this.checkVelocityScale(this.VelocityRmsAxisX)} Y: {this.checkVelocityScale(this.VelocityRmsAxisY)} Z: {this.checkVelocityScale(this.VelocityRmsAxisZ)} , ");
            break;
          case 1:
            stringBuilder1.Append($"Acceleration rms X: {this.checkAccelerationScale((double) this.AccelerationRmsAxisX)} Y: {this.checkAccelerationScale((double) this.AccelerationRmsAxisY)} Z: {this.checkAccelerationScale((double) this.AccelerationRmsAxisZ)} , ");
            break;
          case 2:
            stringBuilder1.Append($"Acceleration Peak X: {this.checkAccelerationScale(this.AccelerationPeakAxisX)} Y: {this.checkAccelerationScale(this.AccelerationPeakAxisY)} Z: {this.checkAccelerationScale(this.AccelerationPeakAxisZ)} , ");
            break;
          case 4:
            stringBuilder1.Append($"Displacement X: {this.checkDisplacementScale(this.DisplacementAxisX)} Y: {this.checkDisplacementScale(this.DisplacementAxisY)} Z: {this.checkDisplacementScale(this.DisplacementAxisZ)} , ");
            break;
        }
        StringBuilder stringBuilder2 = stringBuilder1;
        string[] strArray = new string[7];
        strArray[0] = "Crest Factor  X: ";
        double num = this.CrestFactorAxisX;
        strArray[1] = num.ToString("0.##");
        strArray[2] = " Y: ";
        num = this.CrestFactorAxisY;
        strArray[3] = num.ToString("0.##");
        strArray[4] = " Z: ";
        num = this.CrestFactorAxisZ;
        strArray[5] = num.ToString("0.##");
        strArray[6] = " , ";
        string str1 = string.Concat(strArray);
        stringBuilder2.Append(str1);
        stringBuilder1.Append($"Duty Cycle:{this.DutyCycle.ToString()} % , ");
        string empty = string.Empty;
        string str2;
        if (this.CorF != null && this.CorF.Value == "C")
        {
          num = this.Temperature;
          str2 = num.ToString("0.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
        }
        else
        {
          num = this.Temperature.ToFahrenheit();
          str2 = num.ToString("0.#° F", (IFormatProvider) CultureInfo.InvariantCulture);
        }
        stringBuilder1.Append("Temperature " + str2);
      }
      else if (this.stsStatus == 9)
        stringBuilder1.Append("Bad Temperature Reading");
      else
        stringBuilder1.Append("Hardware Error.");
      return stringBuilder1.ToString();
    }
  }

  public string parseDataMode(int dataMode)
  {
    switch (dataMode)
    {
      case 1:
        return "Average";
      case 2:
        return "Max";
      default:
        return "Most Recent";
    }
  }

  public string checkAccelerationScale(double accelerationValue)
  {
    if (this.AccelerationScale == null)
      return accelerationValue.ToString() + " mm/s^2";
    switch (this.AccelerationScale.Value.ToLower())
    {
      case "g":
        return accelerationValue.ToGs().ToString() + " g";
      case "in/s^2":
        return accelerationValue.ToInchesPerSecondSquared().ToString() + " in/s^2";
      default:
        return accelerationValue.ToString() + " mm/s^2";
    }
  }

  public static double ConvertAccelerationByScale(
    double accelerationValue,
    string accelerationScale)
  {
    if (string.IsNullOrEmpty(accelerationScale))
      return accelerationValue;
    switch (accelerationScale.ToLower())
    {
      case "g":
        return accelerationValue.ToGs();
      case "in/s^2":
        return accelerationValue.ToInchesPerSecondSquared();
      default:
        return accelerationValue;
    }
  }

  public string checkVelocityScale(double velocityValue)
  {
    if (this.VelocityScale == null)
      return velocityValue.ToString("0.##") + " mm/s";
    switch (this.VelocityScale.Value.ToLower())
    {
      case "in/s peak":
        return velocityValue.ToInchesPerSecondPeak().ToString("0.##") + " in/s peak";
      case "in/s rms":
        return velocityValue.ToInchesPerSecondRMS().ToString("0.##") + " in/s rms";
      default:
        return velocityValue.ToString("0.##") + " mm/s";
    }
  }

  public static double ConvertVelocityByScale(double velocityValue, string velocityScale)
  {
    if (string.IsNullOrEmpty(velocityScale))
      return velocityValue;
    switch (velocityScale.ToLower())
    {
      case "in/s peak":
        return velocityValue.ToInchesPerSecondPeak();
      case "in/s rms":
        return velocityValue.ToInchesPerSecondRMS();
      default:
        return velocityValue;
    }
  }

  public string checkDisplacementScale(double displacementValue)
  {
    if (this.DisplacementScale == null)
      return displacementValue.ToString("0.##") + " mm p-p";
    switch (this.DisplacementScale.Value.ToLower())
    {
      case "mil p-p":
        return displacementValue.ToMilliInchesPeaktoPeak().ToString("0.##") + " mil p-p";
      case "in p-p":
        return displacementValue.ToInchesPeaktoPeak().ToString("0.##") + " in p-p";
      default:
        return displacementValue.ToString("0.##") + " mm p-p";
    }
  }

  public static double ConvertDisplacementByScale(
    double displacementValue,
    string displacementScale)
  {
    if (string.IsNullOrEmpty(displacementScale))
      return displacementValue;
    switch (displacementScale.ToLower())
    {
      case "mil p-p":
        return displacementValue.ToMilliInchesPeaktoPeak();
      case "in p-p":
        return displacementValue.ToInchesPeaktoPeak();
      default:
        return displacementValue;
    }
  }

  public override object PlotValue
  {
    get
    {
      return this.HZorRPM != null && this.HZorRPM.Value == "Hz" ? (object) this.FrequencyAxisX : (object) this.FrequencyAxisX.ToRPM();
    }
  }

  public object PlotTemperatureValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.Temperature : (object) this.Temperature.ToFahrenheit();
    }
  }

  public static bool IsFahrenheit(long sensorID) => Monnit.Temperature.IsFahrenheit(sensorID);

  public static void MakeFahrenheit(long sensorID) => Monnit.Temperature.MakeFahrenheit(sensorID);

  public static void MakeCelsius(long sensorID) => Monnit.Temperature.MakeCelsius(sensorID);

  public static bool IsHertz(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HZorRPM" && sensorAttribute.Value == "RPM")
        return false;
    }
    return true;
  }

  public static void MakeHertz(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HZorRPM")
        sensorAttribute.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void MakeRPM(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HZorRPM")
      {
        if (sensorAttribute.Value != "RPM")
        {
          sensorAttribute.Value = "RPM";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "HZorRPM",
        Value = "RPM",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute CorF => this._CorF;

  public SensorAttribute HZorRPM => this._HZorRPM;

  public SensorAttribute DataDisplayMode
  {
    get
    {
      if (this._DataDisplayMode == null)
        this._DataDisplayMode = new SensorAttribute()
        {
          Name = nameof (DataDisplayMode),
          Value = "0"
        };
      return this._DataDisplayMode;
    }
  }

  public SensorAttribute AccelerationScale => this._AccelerationScale;

  public SensorAttribute DisplacementScale => this._DisplacementScale;

  public SensorAttribute VelocityScale => this._VelocityScale;

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
      if (sensorAttribute.Name == "HZorRPM")
        this._HZorRPM = sensorAttribute;
      if (sensorAttribute.Name == "DataDisplayMode")
        this._DataDisplayMode = sensorAttribute;
      if (sensorAttribute.Name == "AccelerationScale")
        this._AccelerationScale = sensorAttribute;
      if (sensorAttribute.Name == "VelocityScale")
        this._VelocityScale = sensorAttribute;
      if (sensorAttribute.Name == "DisplacementScale")
        this._DisplacementScale = sensorAttribute;
    }
  }

  public static string GetLabel(long sensorID)
  {
    string label = "Hz";
    if (!AdvancedVibration2.IsHertz(sensorID))
      label = "Rpm";
    return label;
  }

  public static int GetDataDisplayMode(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "DataDisplayMode")
        return sensorAttribute.Value.ToInt();
    }
    return 0;
  }

  public static void SetDataDisplayMode(long sensorID, int displayMode)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "DataDisplayMode")
      {
        sensorAttribute.Value = displayMode.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "DataDisplayMode",
        Value = displayMode.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetAccelerationScale(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "AccelerationScale")
        return sensorAttribute.Value;
    }
    return "mm/s^2";
  }

  public static void SetAccelerationScale(long sensorID, string accelerationScale)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "AccelerationScale")
      {
        sensorAttribute.Value = accelerationScale.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "AccelerationScale",
        Value = accelerationScale.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetVelocityScale(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "VelocityScale")
        return sensorAttribute.Value;
    }
    return "mm/s";
  }

  public static void SetVelocityScale(long sensorID, string velocityScale)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "VelocityScale")
      {
        sensorAttribute.Value = velocityScale.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "VelocityScale",
        Value = velocityScale.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetDisplacementScale(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "DisplacementScale")
        return sensorAttribute.Value;
    }
    return "mm p-p";
  }

  public static void SetDisplacementScale(long sensorID, string displacementScale)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "DisplacementScale")
      {
        sensorAttribute.Value = displacementScale.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "DisplacementScale",
        Value = displacementScale.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public override string Serialize()
  {
    return $"{this.Mode.ToString()}|{this.FrequencyAxisX.ToString()}|{this.FrequencyAxisY.ToString()}|{this.FrequencyAxisZ.ToString()}|{this.VelocityRmsAxisX.ToString()}|{this.VelocityRmsAxisY.ToString()}|{this.VelocityRmsAxisZ.ToString()}|{this.AccelerationRmsAxisX.ToString()}|{this.AccelerationRmsAxisY.ToString()}|{this.AccelerationRmsAxisZ.ToString()}|{this.AccelerationPeakAxisX.ToString()}|{this.AccelerationPeakAxisY.ToString()}|{this.AccelerationPeakAxisZ.ToString()}|{this.DisplacementAxisX.ToString()}|{this.DisplacementAxisY.ToString()}|{this.DisplacementAxisZ.ToString()}|{this.CrestFactorAxisX.ToString()}|{this.CrestFactorAxisY.ToString()}|{this.CrestFactorAxisZ.ToString()}|{this.DutyCycle.ToString()}|{this.Temperature.ToString()}|{this.DataMode.ToString()}|{this.stsStatus.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) AdvancedVibration2.Deserialize(version, serialized);
  }

  public static AdvancedVibration2 Deserialize(string version, string serialized)
  {
    AdvancedVibration2 advancedVibration2 = new AdvancedVibration2();
    if (string.IsNullOrEmpty(serialized))
    {
      advancedVibration2.stsStatus = 0;
      advancedVibration2.Mode = 0;
      advancedVibration2.FrequencyAxisX = 0.0;
      advancedVibration2.FrequencyAxisY = 0.0;
      advancedVibration2.FrequencyAxisZ = 0.0;
      advancedVibration2.VelocityRmsAxisX = 0.0;
      advancedVibration2.VelocityRmsAxisY = 0.0;
      advancedVibration2.VelocityRmsAxisZ = 0.0;
      advancedVibration2.AccelerationRmsAxisX = 0L;
      advancedVibration2.AccelerationRmsAxisY = 0L;
      advancedVibration2.AccelerationRmsAxisZ = 0L;
      advancedVibration2.AccelerationPeakAxisX = 0.0;
      advancedVibration2.AccelerationPeakAxisY = 0.0;
      advancedVibration2.AccelerationPeakAxisZ = 0.0;
      advancedVibration2.DisplacementAxisX = 0.0;
      advancedVibration2.DisplacementAxisY = 0.0;
      advancedVibration2.DisplacementAxisZ = 0.0;
      advancedVibration2.CrestFactorAxisX = 0.0;
      advancedVibration2.CrestFactorAxisY = 0.0;
      advancedVibration2.CrestFactorAxisZ = 0.0;
      advancedVibration2.DutyCycle = 0;
      advancedVibration2.Temperature = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length > 1)
      {
        try
        {
          advancedVibration2.Mode = strArray[0].ToInt();
          advancedVibration2.FrequencyAxisX = strArray[1].ToDouble();
          advancedVibration2.FrequencyAxisY = strArray[2].ToDouble();
          advancedVibration2.FrequencyAxisZ = strArray[3].ToDouble();
          advancedVibration2.VelocityRmsAxisX = strArray[4].ToDouble();
          advancedVibration2.VelocityRmsAxisY = strArray[5].ToDouble();
          advancedVibration2.VelocityRmsAxisZ = strArray[6].ToDouble();
          advancedVibration2.AccelerationRmsAxisX = strArray[7].ToLong();
          advancedVibration2.AccelerationRmsAxisY = strArray[8].ToLong();
          advancedVibration2.AccelerationRmsAxisZ = strArray[9].ToLong();
          advancedVibration2.AccelerationPeakAxisX = strArray[10].ToDouble();
          advancedVibration2.AccelerationPeakAxisY = strArray[11].ToDouble();
          advancedVibration2.AccelerationPeakAxisZ = strArray[12].ToDouble();
          advancedVibration2.DisplacementAxisX = strArray[13].ToDouble();
          advancedVibration2.DisplacementAxisY = strArray[14].ToDouble();
          advancedVibration2.DisplacementAxisZ = strArray[15].ToDouble();
          advancedVibration2.CrestFactorAxisX = strArray[16 /*0x10*/].ToDouble();
          advancedVibration2.CrestFactorAxisY = strArray[17].ToDouble();
          advancedVibration2.CrestFactorAxisZ = strArray[18].ToDouble();
          advancedVibration2.DutyCycle = strArray[19].ToInt();
          advancedVibration2.Temperature = strArray[20].ToDouble();
          advancedVibration2.DataMode = strArray[21].ToInt();
          try
          {
            advancedVibration2.stsStatus = strArray[22].ToInt();
          }
          catch
          {
            advancedVibration2.stsStatus = 0;
          }
        }
        catch
        {
          advancedVibration2.stsStatus = 0;
          advancedVibration2.Mode = 0;
          advancedVibration2.FrequencyAxisX = 0.0;
          advancedVibration2.FrequencyAxisY = 0.0;
          advancedVibration2.FrequencyAxisZ = 0.0;
          advancedVibration2.VelocityRmsAxisX = 0.0;
          advancedVibration2.VelocityRmsAxisY = 0.0;
          advancedVibration2.VelocityRmsAxisZ = 0.0;
          advancedVibration2.AccelerationRmsAxisX = 0L;
          advancedVibration2.AccelerationRmsAxisY = 0L;
          advancedVibration2.AccelerationRmsAxisZ = 0L;
          advancedVibration2.AccelerationPeakAxisX = 0.0;
          advancedVibration2.AccelerationPeakAxisY = 0.0;
          advancedVibration2.AccelerationPeakAxisZ = 0.0;
          advancedVibration2.DisplacementAxisX = 0.0;
          advancedVibration2.DisplacementAxisY = 0.0;
          advancedVibration2.DisplacementAxisZ = 0.0;
          advancedVibration2.CrestFactorAxisX = 0.0;
          advancedVibration2.CrestFactorAxisY = 0.0;
          advancedVibration2.CrestFactorAxisZ = 0.0;
          advancedVibration2.DutyCycle = 0;
          advancedVibration2.Temperature = 0.0;
          advancedVibration2.DataMode = 0;
        }
      }
      else
      {
        advancedVibration2.Mode = strArray[0].ToInt();
        advancedVibration2.FrequencyAxisX = strArray[0].ToDouble();
        advancedVibration2.FrequencyAxisY = strArray[0].ToDouble();
        advancedVibration2.FrequencyAxisZ = strArray[0].ToDouble();
        advancedVibration2.VelocityRmsAxisX = strArray[0].ToDouble();
        advancedVibration2.VelocityRmsAxisY = strArray[0].ToDouble();
        advancedVibration2.VelocityRmsAxisZ = strArray[0].ToDouble();
        advancedVibration2.AccelerationRmsAxisX = strArray[0].ToLong();
        advancedVibration2.AccelerationRmsAxisY = strArray[0].ToLong();
        advancedVibration2.AccelerationRmsAxisZ = strArray[0].ToLong();
        advancedVibration2.AccelerationPeakAxisX = strArray[0].ToDouble();
        advancedVibration2.AccelerationPeakAxisY = strArray[0].ToDouble();
        advancedVibration2.AccelerationPeakAxisZ = strArray[0].ToDouble();
        advancedVibration2.DisplacementAxisX = strArray[0].ToDouble();
        advancedVibration2.DisplacementAxisY = strArray[0].ToDouble();
        advancedVibration2.DisplacementAxisZ = strArray[0].ToDouble();
        advancedVibration2.CrestFactorAxisX = strArray[0].ToDouble();
        advancedVibration2.CrestFactorAxisY = strArray[0].ToDouble();
        advancedVibration2.CrestFactorAxisZ = strArray[0].ToDouble();
        advancedVibration2.DutyCycle = strArray[0].ToInt();
        advancedVibration2.Temperature = strArray[0].ToDouble();
        advancedVibration2.DataMode = strArray[0].ToInt();
      }
    }
    return advancedVibration2;
  }

  public static AdvancedVibration2 Create(byte[] sdm, int startIndex)
  {
    AdvancedVibration2 advancedVibration2 = new AdvancedVibration2();
    advancedVibration2.stsStatus = (int) sdm[startIndex - 1] >> 4;
    if (advancedVibration2.stsStatus >= 8)
    {
      advancedVibration2.Mode = 0;
      advancedVibration2.FrequencyAxisX = 0.0;
      advancedVibration2.FrequencyAxisY = 0.0;
      advancedVibration2.FrequencyAxisZ = 0.0;
      advancedVibration2.VelocityRmsAxisX = 0.0;
      advancedVibration2.VelocityRmsAxisY = 0.0;
      advancedVibration2.VelocityRmsAxisZ = 0.0;
      advancedVibration2.AccelerationRmsAxisX = 0L;
      advancedVibration2.AccelerationRmsAxisY = 0L;
      advancedVibration2.AccelerationRmsAxisZ = 0L;
      advancedVibration2.AccelerationPeakAxisX = 0.0;
      advancedVibration2.AccelerationPeakAxisY = 0.0;
      advancedVibration2.AccelerationPeakAxisZ = 0.0;
      advancedVibration2.DisplacementAxisX = 0.0;
      advancedVibration2.DisplacementAxisY = 0.0;
      advancedVibration2.DisplacementAxisZ = 0.0;
      advancedVibration2.CrestFactorAxisX = 0.0;
      advancedVibration2.CrestFactorAxisY = 0.0;
      advancedVibration2.CrestFactorAxisZ = 0.0;
      advancedVibration2.DutyCycle = 0;
      advancedVibration2.Temperature = 0.0;
      advancedVibration2.DataMode = 0;
    }
    else
    {
      advancedVibration2.Mode = AdvancedVibration2.GetVibrationMode(sdm[startIndex]);
      advancedVibration2.DataMode = AdvancedVibration2.GetDataMode(sdm[startIndex]);
      advancedVibration2.FrequencyAxisX = BitConverter.ToUInt16(sdm, startIndex + 1).ToDouble() / 10.0;
      advancedVibration2.FrequencyAxisY = BitConverter.ToUInt16(sdm, startIndex + 3).ToDouble() / 10.0;
      advancedVibration2.FrequencyAxisZ = BitConverter.ToUInt16(sdm, startIndex + 5).ToDouble() / 10.0;
      switch (advancedVibration2.Mode)
      {
        case 0:
          advancedVibration2.VelocityRmsAxisX = BitConverter.ToUInt16(sdm, startIndex + 7).ToDouble() / 100.0;
          advancedVibration2.VelocityRmsAxisY = BitConverter.ToUInt16(sdm, startIndex + 9).ToDouble() / 100.0;
          advancedVibration2.VelocityRmsAxisZ = BitConverter.ToUInt16(sdm, startIndex + 11).ToDouble() / 100.0;
          break;
        case 1:
          advancedVibration2.AccelerationRmsAxisX = BitConverter.ToUInt16(sdm, startIndex + 7).ToLong() * 10L;
          advancedVibration2.AccelerationRmsAxisY = BitConverter.ToUInt16(sdm, startIndex + 9).ToLong() * 10L;
          advancedVibration2.AccelerationRmsAxisZ = BitConverter.ToUInt16(sdm, startIndex + 11).ToLong() * 10L;
          break;
        case 2:
          advancedVibration2.AccelerationPeakAxisX = BitConverter.ToUInt16(sdm, startIndex + 7).ToDouble() * 10.0;
          advancedVibration2.AccelerationPeakAxisY = BitConverter.ToUInt16(sdm, startIndex + 9).ToDouble() * 10.0;
          advancedVibration2.AccelerationPeakAxisZ = BitConverter.ToUInt16(sdm, startIndex + 11).ToDouble() * 10.0;
          break;
        case 4:
          advancedVibration2.DisplacementAxisX = BitConverter.ToUInt16(sdm, startIndex + 7).ToDouble() / 100.0;
          advancedVibration2.DisplacementAxisY = BitConverter.ToUInt16(sdm, startIndex + 9).ToDouble() / 100.0;
          advancedVibration2.DisplacementAxisZ = BitConverter.ToUInt16(sdm, startIndex + 11).ToDouble() / 100.0;
          break;
      }
      advancedVibration2.CrestFactorAxisX = ((sbyte) sdm[startIndex + 13]).ToDouble() / 50.0 + 1.41;
      advancedVibration2.CrestFactorAxisY = ((sbyte) sdm[startIndex + 14]).ToDouble() / 50.0 + 1.41;
      advancedVibration2.CrestFactorAxisZ = ((sbyte) sdm[startIndex + 15]).ToDouble() / 50.0 + 1.41;
      advancedVibration2.DutyCycle = sdm[startIndex + 16 /*0x10*/].ToInt();
      advancedVibration2.Temperature = BitConverter.ToInt16(sdm, startIndex + 17).ToDouble() / 10.0;
    }
    return advancedVibration2;
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
      ApplicationID = AdvancedVibration2.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion,
      Scale = AdvancedVibration2.IsHertz(sensor.SensorID) ? "Hz" : "rpm"
    };
  }

  public static Dictionary<string, string> NotificationScaleValues()
  {
    return new Dictionary<string, string>()
    {
      {
        "Hz",
        "Hertz"
      },
      {
        "rpm",
        "rpm"
      }
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    string accelerationScale = AdvancedVibration2.GetAccelerationScale(sensor.SensorID);
    string velocityScale = AdvancedVibration2.GetVelocityScale(sensor.SensorID);
    string displacementScale = AdvancedVibration2.GetDisplacementScale(sensor.SensorID);
    if (string.IsNullOrEmpty(collection["VibrationMode_Manual"]))
      return;
    int o1 = collection["VibrationMode_Manual"].ToInt();
    if (o1 < 0)
      o1 = 0;
    if (o1 > 4)
      o1 = 4;
    AdvancedVibration2.SetVibrationMode(sensor, o1.ToInt());
    if (string.IsNullOrEmpty(collection["SampleRate_Manual"]))
      return;
    int o2 = collection["SampleRate_Manual"].ToInt();
    if (o2 < 0)
      o2 = 0;
    if (o2 > 16 /*0x10*/)
      o2 = 16 /*0x10*/;
    AdvancedVibration2.SetSampleRate(sensor, o2.ToInt());
    if (!string.IsNullOrEmpty(collection["VibrationHysteresis_Manual"]))
    {
      double num = collection["VibrationHysteresis_Manual"].ToDouble();
      if (new Version(sensor.FirmwareVersion) >= new Version("10.23.13.9"))
      {
        switch (AdvancedVibration2.GetVibrationMode(sensor))
        {
          case 0:
            switch (velocityScale)
            {
              case "in/s peak":
                num = num.InchesPerSecondPeakToMillimeterPerSecond();
                break;
              case "in/s rms":
                num = num.InchesPerSecondRmsToMillimeterPerSecond();
                break;
            }
            break;
          case 1:
          case 2:
            switch (accelerationScale)
            {
              case "g":
                num = num.GsToMillimetersPerSecondSquared();
                break;
              case "in/s^2":
                num = num.InchesPerSecondSquaredToMillimetersPerSecondSquared();
                break;
            }
            num /= 10.0;
            break;
          case 4:
            switch (displacementScale)
            {
              case "mil p-p":
                num = num.MilliInchesPeaktoPeakToMillimeterPeakToPeak();
                break;
              case "in p-p":
                num = num.InchesPeaktoPeakToMillimeterPeakToPeak();
                break;
            }
            break;
        }
      }
      if (num < 0.0)
        num = 0.0;
      if (num > (double) ushort.MaxValue)
        num = (double) ushort.MaxValue;
      AdvancedVibration2.SetVibrationHysteresis(sensor, num.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["VibrationAwareThreshold_Manual"]))
    {
      double num = collection["VibrationAwareThreshold_Manual"].ToDouble();
      switch (AdvancedVibration2.GetVibrationMode(sensor))
      {
        case 0:
          switch (velocityScale)
          {
            case "in/s peak":
              num = num.InchesPerSecondPeakToMillimeterPerSecond();
              break;
            case "in/s rms":
              num = num.InchesPerSecondRmsToMillimeterPerSecond();
              break;
          }
          num *= 100.0;
          break;
        case 1:
        case 2:
          switch (accelerationScale)
          {
            case "g":
              num = num.GsToMillimetersPerSecondSquared();
              break;
            case "in/s^2":
              num = num.InchesPerSecondSquaredToMillimetersPerSecondSquared();
              break;
          }
          if (new Version(sensor.FirmwareVersion) >= new Version("10.23.13.9"))
          {
            num /= 10.0;
            break;
          }
          break;
        case 4:
          switch (displacementScale)
          {
            case "mil p-p":
              num = num.MilliInchesPeaktoPeakToMillimeterPeakToPeak();
              break;
            case "in p-p":
              num = num.InchesPeaktoPeakToMillimeterPeakToPeak();
              break;
          }
          num *= 100.0;
          break;
      }
      if (num < 0.0)
        num = 0.0;
      if (num > (double) ushort.MaxValue)
        num = (double) ushort.MaxValue;
      AdvancedVibration2.SetVibrationAwareThreshold(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["VibrationSensitivityThreshold_Manual"]))
    {
      double num = collection["VibrationSensitivityThreshold_Manual"].ToDouble();
      if (num < 0.0)
        num = 0.0;
      if (num > 2.55)
        num = 2.55;
      AdvancedVibration2.SetVibrationSensitivityThreshold(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["WindowFunction_Manual"]))
    {
      int o3 = collection["WindowFunction_Manual"].ToInt();
      if (o3 < 0)
        o3 = 0;
      if (o3 > 2)
        o3 = 2;
      AdvancedVibration2.SetWindowFunction(sensor, o3.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["AccelerometerRange_Manual"]))
    {
      int o4 = collection["AccelerometerRange_Manual"].ToInt();
      if (o4 < 0)
        o4 = 0;
      if (o4 > 3)
        o4 = 3;
      AdvancedVibration2.SetAccelerometerRange(sensor, o4.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["MeasurementInterval_Manual"]))
    {
      double num = collection["MeasurementInterval_Manual"].ToDouble();
      if (num < 1.0)
        num = 1.0;
      if (num > 43200.0)
        num = 43200.0;
      AdvancedVibration2.SetMeasurementInterval(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["MinFrequency_Manual"]) && !string.IsNullOrEmpty(collection["MaxFrequency_Manual"]))
    {
      double num1 = collection["MinFrequency_Manual"].ToDouble();
      double num2 = collection["MaxFrequency_Manual"].ToDouble();
      double num3 = 2.0;
      double num4 = 128.0;
      if (num1 < num3)
        num1 = num3;
      if (num1 > num4 - 3.0)
        num1 = num4 - 3.0;
      if (num2 < num3 + 3.0)
        num2 = num3 + 3.0;
      if (num2 > num4)
        num2 = num4;
      if (num2 - 3.0 < num1)
        num2 = num1 + 3.0;
      if (num1 + 3.0 > num2)
        num1 = num2 - 3.0;
      AdvancedVibration2.SetMinFrequency(sensor, num1);
      AdvancedVibration2.SetMaxFrequency(sensor, num2);
    }
    if (!string.IsNullOrEmpty(collection["PowerMode_Manual"]))
    {
      int o5 = collection["PowerMode_Manual"].ToInt();
      if (o5 < 0)
        o5 = 0;
      if (o5 > 2)
        o5 = 2;
      AdvancedVibration2.SetPowerMode(sensor, o5.ToInt());
    }
    if (string.IsNullOrEmpty(collection["DataMode_Manual"]))
      return;
    int o6 = collection["DataMode_Manual"].ToInt();
    if (o6 < 0)
      o6 = 0;
    if (o6 > 2)
      o6 = 2;
    AdvancedVibration2.SetDataMode(sensor, o6.ToInt());
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (collection["TempScale"] == "on")
    {
      viewData["TempScale"] = "F";
      AdvancedVibration2.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      viewData["TempScale"] = "C";
      AdvancedVibration2.MakeCelsius(sensor.SensorID);
    }
    if (!string.IsNullOrEmpty(collection["measurementscale"]))
    {
      if (collection["measurementscale"] == "on")
      {
        viewData["measurementscale"] = "Hz";
        AdvancedVibration2.MakeHertz(sensor.SensorID);
      }
      else
      {
        viewData["measurementscale"] = "RPM";
        AdvancedVibration2.MakeRPM(sensor.SensorID);
      }
    }
    if (!string.IsNullOrEmpty(collection["AccelerationScale"]))
      AdvancedVibration2.SetAccelerationScale(sensor.SensorID, collection["AccelerationScale"].ToString());
    if (!string.IsNullOrEmpty(collection["VelocityScale"]))
      AdvancedVibration2.SetVelocityScale(sensor.SensorID, collection["VelocityScale"].ToString());
    if (string.IsNullOrEmpty(collection["DisplacementScale"]))
      return;
    AdvancedVibration2.SetDisplacementScale(sensor.SensorID, collection["DisplacementScale"].ToString());
  }

  public static double ConvertVibrationBasedonModeAndScale(Sensor sensor, double vibrationValue)
  {
    int vibrationMode = AdvancedVibration2.GetVibrationMode(sensor);
    string accelerationScale = AdvancedVibration2.GetAccelerationScale(sensor.SensorID);
    string velocityScale = AdvancedVibration2.GetVelocityScale(sensor.SensorID);
    string displacementScale = AdvancedVibration2.GetDisplacementScale(sensor.SensorID);
    switch (vibrationMode)
    {
      case 0:
        switch (velocityScale)
        {
          case "in/s peak":
            vibrationValue = vibrationValue.ToInchesPerSecondPeak();
            break;
          case "in/s rms":
            vibrationValue = vibrationValue.ToInchesPerSecondRMS();
            break;
        }
        break;
      case 1:
      case 2:
        switch (accelerationScale)
        {
          case "g":
            vibrationValue = vibrationValue.ToGs();
            break;
          case "in/s^2":
            vibrationValue = vibrationValue.ToInchesPerSecondSquared();
            break;
        }
        break;
      case 4:
        switch (displacementScale)
        {
          case "mil p-p":
            vibrationValue = vibrationValue.ToMilliInchesPeaktoPeak();
            break;
          case "in p-p":
            vibrationValue = vibrationValue.ToInchesPeaktoPeak();
            break;
        }
        break;
    }
    return vibrationValue;
  }

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    AdvancedVibration2Base.GetDefaults(new Version(sensor.FirmwareVersion), sensor.GenerationType);
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
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
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    NonCachedAttribute nonCachedAttribute = NonCachedAttribute.LoadBySensorIDAndName(sensor.SensorID, "Calibration");
    List<byte[]> numArrayList = new List<byte[]>();
    if (nonCachedAttribute.Value1 == "1")
      numArrayList.Add(AdvancedVibration2Base.CalibrateFrame(sensor.SensorID, (double) nonCachedAttribute.Value2.ToInt()));
    else if (nonCachedAttribute.Value1 == "2")
      numArrayList.Add(AdvancedVibration2Base.BaselinesCalibrateFrame(sensor.SensorID));
    numArrayList.Add(sensor.ReadProfileConfig(29));
    return numArrayList;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(AdvancedVibration2 left, AdvancedVibration2 right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(AdvancedVibration2 left, AdvancedVibration2 right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(AdvancedVibration2 left, AdvancedVibration2 right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(AdvancedVibration2 left, AdvancedVibration2 right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(AdvancedVibration2 left, AdvancedVibration2 right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(AdvancedVibration2 left, AdvancedVibration2 right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is AdvancedVibration2 && this.Equals((MonnitApplicationBase) (obj as AdvancedVibration2));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is AdvancedVibration2 && this.FrequencyAxisX == (right as AdvancedVibration2).FrequencyAxisX;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is AdvancedVibration2 && this.FrequencyAxisX != (right as AdvancedVibration2).FrequencyAxisX;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is AdvancedVibration2 && this.FrequencyAxisX < (right as AdvancedVibration2).FrequencyAxisX;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is AdvancedVibration2 && this.FrequencyAxisX <= (right as AdvancedVibration2).FrequencyAxisX;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is AdvancedVibration2 && this.FrequencyAxisX > (right as AdvancedVibration2).FrequencyAxisX;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is AdvancedVibration2 && this.FrequencyAxisX >= (right as AdvancedVibration2).FrequencyAxisX;
  }

  public static int GetDataMode(byte firstByte) => (int) firstByte >> 4;

  public static int GetVibrationMode(byte firstByte) => (int) firstByte & 15;

  public static void SetVibrationHysteresis(Sensor sens, int value)
  {
    uint num = (uint) sens.Hysteresis & 4294901760U;
    sens.Hysteresis = (long) (num | (uint) (ushort) value);
  }

  public static int GetVibrationHysteresis(Sensor sens)
  {
    int vibrationMode = AdvancedVibration2.GetVibrationMode(sens);
    int int32 = Convert.ToInt32(sens.Hysteresis & (long) ushort.MaxValue);
    switch (vibrationMode)
    {
      case 0:
      case 4:
        return int32;
      case 1:
      case 2:
        return new Version(sens.FirmwareVersion) >= new Version("10.23.13.9") ? int32 * 10 : int32;
      default:
        return int32;
    }
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

  public static void SetVibrationAwareThreshold(Sensor sens, double value)
  {
    int num = (int) ((long) (int) sens.MaximumThreshold & 4294901760L);
    sens.MaximumThreshold = (long) (num | (int) (ushort) value);
  }

  public static double GetVibrationAwareThreshold(Sensor sens)
  {
    int vibrationMode = AdvancedVibration2.GetVibrationMode(sens);
    int o = ((uint) ((int) (uint) sens.MaximumThreshold & (int) ushort.MaxValue)).ToInt();
    switch (vibrationMode)
    {
      case 0:
      case 4:
        return ((double) o / 100.0).ToDouble();
      case 1:
      case 2:
        return new Version(sens.FirmwareVersion) >= new Version("10.23.13.9") ? Math.Truncate((o * 10).ToDouble()) : o.ToDouble();
      default:
        return o.ToDouble();
    }
  }

  public static void SetVibrationSensitivityThreshold(Sensor sens, double value)
  {
    value *= 100.0;
    uint num = (uint) sens.MaximumThreshold & 16777215U /*0xFFFFFF*/;
    sens.MaximumThreshold = (long) (num | (uint) ((int) (uint) value << 24 & -16777216 /*0xFF000000*/));
  }

  public static double GetVibrationSensitivityThreshold(Sensor sens)
  {
    return (double) (int) (Convert.ToUInt32(sens.MaximumThreshold & 4278190080L /*0xFF000000*/) >> 24) / 100.0;
  }

  public static void SetSampleRate(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4294967040U;
    sensor.Calibration1 = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetSampleRate(Sensor sensor)
  {
    return (int) Convert.ToUInt32(sensor.Calibration1) & (int) byte.MaxValue;
  }

  public static void SetWindowFunction(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4294902015U;
    sensor.Calibration1 = (long) (num | (uint) (value << 8));
  }

  public static int GetWindowFunction(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration1) & 65280U) >> 8);
  }

  public static void SetVibrationMode(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4278255615U;
    sensor.Calibration1 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int GetVibrationMode(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration1) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetAccelerometerRange(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 16777215U /*0xFFFFFF*/;
    sensor.Calibration1 = (long) (num | (uint) (value << 24));
  }

  public static int GetAccelerometerRange(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration1) & 4278190080U /*0xFF000000*/) >> 24);
  }

  public static void SetMeasurementInterval(Sensor sens, double value)
  {
    uint num = (uint) sens.Calibration2 & 4294901760U;
    sens.Calibration2 = (long) (num | (uint) (ushort) value);
  }

  public static int GetMeasurementInterval(Sensor sens)
  {
    return Convert.ToInt32(sens.Calibration2 & (long) ushort.MaxValue);
  }

  public static double getResolutionValue(int sampleRate)
  {
    switch (sampleRate)
    {
      case 5:
        return 0.1;
      case 6:
        return 0.2;
      case 7:
        return 0.4;
      case 8:
        return 0.8;
      case 9:
        return 1.6;
      case 11:
        return 6.3;
      case 12:
        return 12.5;
      case 13:
        return 25.0;
      case 14:
        return 50.0;
      default:
        return 3.1;
    }
  }

  public static void SetMinFrequency(Sensor sens, double value)
  {
    uint num = (uint) sens.Calibration3 & 4294901760U;
    sens.Calibration3 = (long) (num | (uint) (ushort) value);
  }

  public static double GetMinFrequency(Sensor sens)
  {
    return (double) ((int) Convert.ToUInt32(sens.Calibration3) & (int) byte.MaxValue);
  }

  public static void SetMaxFrequency(Sensor sens, double value)
  {
    uint num = Convert.ToUInt32(sens.Calibration3) & 4294902015U;
    sens.Calibration3 = (long) (num | (uint) value << 8);
  }

  public static double GetMaxFrequency(Sensor sens)
  {
    return (double) (int) ((Convert.ToUInt32(sens.Calibration3) & 65280U) >> 8);
  }

  public static void SetPowerMode(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration3) & 4278255615U;
    sensor.Calibration3 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int GetPowerMode(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration3) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetDataMode(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration3) & 16777215U /*0xFFFFFF*/;
    sensor.Calibration3 = (long) (num | (uint) (value << 24));
  }

  public static int GetDataMode(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration3) & 4278190080U /*0xFF000000*/) >> 24);
  }

  public static void SetBitRate(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration4) & 4294967040U;
    sensor.Calibration4 = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetBitRate(Sensor sensor)
  {
    return (int) Convert.ToUInt32(sensor.Calibration4) & (int) byte.MaxValue;
  }

  public static void SetFIFOSize(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration4) & 4294902015U;
    sensor.Calibration4 = (long) (num | (uint) (value << 8));
  }

  public static int GetFIFOSize(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration4) & 65280U) >> 8);
  }

  public static void SetReadSize(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration4) & 4278255615U;
    sensor.Calibration4 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int GetReadSize(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration4) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetCompensator(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration4) & 16777215U /*0xFFFFFF*/;
    sensor.Calibration4 = (long) (num | (uint) (value << 24));
  }

  public static int GetCompensator(Sensor sensor)
  {
    return (int) ((Convert.ToUInt32(sensor.Calibration4) & 4278190080U /*0xFF000000*/) >> 24);
  }
}
