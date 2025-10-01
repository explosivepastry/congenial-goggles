// Decompiled with JetBrains decompiler
// Type: Monnit.AdvancedVibration
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

public class AdvancedVibration : MonnitApplicationBase, ISensorAttribute
{
  internal SensorAttribute _CorF;
  internal SensorAttribute _HZorRPM;
  private SensorAttribute _DataDisplayMode;

  public static long MonnitApplicationID => 111;

  public static string ApplicationName => "Advanced Vibration";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => AdvancedVibration.MonnitApplicationID;

  public int Mode { get; set; }

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

  public override List<object> GetPlotValues(long sensorID)
  {
    switch (this.Mode)
    {
      case 0:
        return new List<object>((IEnumerable<object>) new object[21]
        {
          (object) this.Mode,
          (object) (AdvancedVibration.IsHertz(sensorID) ? this.FrequencyAxisX : this.FrequencyAxisX.ToRPM()),
          (object) (AdvancedVibration.IsHertz(sensorID) ? this.FrequencyAxisY : this.FrequencyAxisY.ToRPM()),
          (object) (AdvancedVibration.IsHertz(sensorID) ? this.FrequencyAxisZ : this.FrequencyAxisZ.ToRPM()),
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
          (object) (AdvancedVibration.IsHertz(sensorID) ? this.FrequencyAxisX : this.FrequencyAxisX.ToRPM()),
          (object) (AdvancedVibration.IsHertz(sensorID) ? this.FrequencyAxisY : this.FrequencyAxisY.ToRPM()),
          (object) (AdvancedVibration.IsHertz(sensorID) ? this.FrequencyAxisZ : this.FrequencyAxisZ.ToRPM()),
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
          (object) (AdvancedVibration.IsHertz(sensorID) ? this.FrequencyAxisX : this.FrequencyAxisX.ToRPM()),
          (object) (AdvancedVibration.IsHertz(sensorID) ? this.FrequencyAxisY : this.FrequencyAxisY.ToRPM()),
          (object) (AdvancedVibration.IsHertz(sensorID) ? this.FrequencyAxisZ : this.FrequencyAxisZ.ToRPM()),
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
          (object) (AdvancedVibration.IsHertz(sensorID) ? this.FrequencyAxisX : this.FrequencyAxisX.ToRPM()),
          (object) (AdvancedVibration.IsHertz(sensorID) ? this.FrequencyAxisY : this.FrequencyAxisY.ToRPM()),
          (object) (AdvancedVibration.IsHertz(sensorID) ? this.FrequencyAxisZ : this.FrequencyAxisZ.ToRPM()),
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
        collection[1] = (object) (AdvancedVibration.IsHertz(sensorID) ? this.FrequencyAxisX : this.FrequencyAxisX.ToRPM());
        collection[2] = (object) (AdvancedVibration.IsHertz(sensorID) ? this.FrequencyAxisY : this.FrequencyAxisY.ToRPM());
        collection[3] = (object) (AdvancedVibration.IsHertz(sensorID) ? this.FrequencyAxisZ : this.FrequencyAxisZ.ToRPM());
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
          AdvancedVibration.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration.IsHertz(sensorID) ? "Hz" : "rpm",
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
          AdvancedVibration.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
        });
      case 1:
        return new List<string>((IEnumerable<string>) new string[21]
        {
          " ",
          AdvancedVibration.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration.IsHertz(sensorID) ? "Hz" : "rpm",
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
          AdvancedVibration.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
        });
      case 2:
        return new List<string>((IEnumerable<string>) new string[21]
        {
          " ",
          AdvancedVibration.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration.IsHertz(sensorID) ? "Hz" : "rpm",
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
          AdvancedVibration.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
        });
      case 4:
        return new List<string>((IEnumerable<string>) new string[21]
        {
          " ",
          AdvancedVibration.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration.IsHertz(sensorID) ? "Hz" : "rpm",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          " ",
          "mm",
          "mm",
          "mm",
          " ",
          " ",
          " ",
          "%",
          AdvancedVibration.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
        });
      default:
        return new List<string>((IEnumerable<string>) new string[21]
        {
          " ",
          AdvancedVibration.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration.IsHertz(sensorID) ? "Hz" : "rpm",
          AdvancedVibration.IsHertz(sensorID) ? "Hz" : "rpm",
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
          AdvancedVibration.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
        });
    }
  }

  public override string Serialize()
  {
    return $"{this.Mode.ToString()}|{this.FrequencyAxisX.ToString()}|{this.FrequencyAxisY.ToString()}|{this.FrequencyAxisZ.ToString()}|{this.VelocityRmsAxisX.ToString()}|{this.VelocityRmsAxisY.ToString()}|{this.VelocityRmsAxisZ.ToString()}|{this.AccelerationRmsAxisX.ToString()}|{this.AccelerationRmsAxisY.ToString()}|{this.AccelerationRmsAxisZ.ToString()}|{this.AccelerationPeakAxisX.ToString()}|{this.AccelerationPeakAxisY.ToString()}|{this.AccelerationPeakAxisZ.ToString()}|{this.DisplacementAxisX.ToString()}|{this.DisplacementAxisY.ToString()}|{this.DisplacementAxisZ.ToString()}|{this.CrestFactorAxisX.ToString()}|{this.CrestFactorAxisY.ToString()}|{this.CrestFactorAxisZ.ToString()}|{this.DutyCycle.ToString()}|{this.Temperature.ToString()}|{this.stsStatus.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) AdvancedVibration.Deserialize(version, serialized);
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
    }
  }

  public static string GetLabel(long sensorID)
  {
    string label = "Hz";
    if (!AdvancedVibration.IsHertz(sensorID))
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
        stringBuilder1.Append($"Frequency X: {(flag ? this.FrequencyAxisX : this.FrequencyAxisX.ToRPM()).ToString("0.#")}{(flag ? " Hz" : " RPM")} Y: {(flag ? this.FrequencyAxisY : this.FrequencyAxisY.ToRPM()).ToString("0.#")}{(flag ? " Hz" : " RPM")} Z: {(flag ? this.FrequencyAxisZ : this.FrequencyAxisZ.ToRPM()).ToString("0.#")}{(flag ? " Hz" : " RPM")} , ");
        double num1;
        switch (this.Mode)
        {
          case 0:
            StringBuilder stringBuilder2 = stringBuilder1;
            string[] strArray1 = new string[7];
            strArray1[0] = "Velocity rms X: ";
            num1 = this.VelocityRmsAxisX;
            strArray1[1] = num1.ToString("0.##");
            strArray1[2] = " mm/s Y: ";
            num1 = this.VelocityRmsAxisY;
            strArray1[3] = num1.ToString("0.##");
            strArray1[4] = " mm/s Z: ";
            num1 = this.VelocityRmsAxisZ;
            strArray1[5] = num1.ToString("0.##");
            strArray1[6] = " mm/s , ";
            string str1 = string.Concat(strArray1);
            stringBuilder2.Append(str1);
            break;
          case 1:
            StringBuilder stringBuilder3 = stringBuilder1;
            string[] strArray2 = new string[7];
            strArray2[0] = "Acceleration rms X: ";
            long num2 = this.AccelerationRmsAxisX;
            strArray2[1] = num2.ToString();
            strArray2[2] = " mm/s^2 Y: ";
            num2 = this.AccelerationRmsAxisY;
            strArray2[3] = num2.ToString();
            strArray2[4] = " mm/s^2 Z: ";
            num2 = this.AccelerationRmsAxisZ;
            strArray2[5] = num2.ToString();
            strArray2[6] = " mm/s^2 , ";
            string str2 = string.Concat(strArray2);
            stringBuilder3.Append(str2);
            break;
          case 2:
            StringBuilder stringBuilder4 = stringBuilder1;
            string[] strArray3 = new string[7];
            strArray3[0] = "Acceleration Peak X: ";
            num1 = this.AccelerationPeakAxisX;
            strArray3[1] = num1.ToString();
            strArray3[2] = " mm/s^2 Y: ";
            num1 = this.AccelerationPeakAxisY;
            strArray3[3] = num1.ToString();
            strArray3[4] = " mm/s^2 Z: ";
            num1 = this.AccelerationPeakAxisZ;
            strArray3[5] = num1.ToString();
            strArray3[6] = " mm/s^2 , ";
            string str3 = string.Concat(strArray3);
            stringBuilder4.Append(str3);
            break;
          case 4:
            StringBuilder stringBuilder5 = stringBuilder1;
            string[] strArray4 = new string[7];
            strArray4[0] = "Displacement X: ";
            num1 = this.DisplacementAxisX;
            strArray4[1] = num1.ToString("0.##");
            strArray4[2] = " mm Y: ";
            num1 = this.DisplacementAxisY;
            strArray4[3] = num1.ToString("0.##");
            strArray4[4] = " mm Z: ";
            num1 = this.DisplacementAxisZ;
            strArray4[5] = num1.ToString("0.##");
            strArray4[6] = " mm , ";
            string str4 = string.Concat(strArray4);
            stringBuilder5.Append(str4);
            break;
        }
        StringBuilder stringBuilder6 = stringBuilder1;
        string[] strArray5 = new string[7];
        strArray5[0] = "Crest Factor  X: ";
        num1 = this.CrestFactorAxisX;
        strArray5[1] = num1.ToString();
        strArray5[2] = " Y: ";
        num1 = this.CrestFactorAxisY;
        strArray5[3] = num1.ToString();
        strArray5[4] = " Z: ";
        num1 = this.CrestFactorAxisZ;
        strArray5[5] = num1.ToString();
        strArray5[6] = " , ";
        string str5 = string.Concat(strArray5);
        stringBuilder6.Append(str5);
        stringBuilder1.Append($"Duty Cycle:{this.DutyCycle.ToString()} % , ");
        string empty = string.Empty;
        string str6;
        if (this.CorF != null && this.CorF.Value == "C")
        {
          num1 = this.Temperature;
          str6 = num1.ToString("##.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
        }
        else
        {
          num1 = this.Temperature.ToFahrenheit();
          str6 = num1.ToString("##.#° F", (IFormatProvider) CultureInfo.InvariantCulture);
        }
        stringBuilder1.Append("Temperature " + str6);
      }
      else if (this.stsStatus == 9)
        stringBuilder1.Append("Bad Temperature Reading");
      else
        stringBuilder1.Append("Hardware Error.");
      return stringBuilder1.ToString();
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

  public static AdvancedVibration Deserialize(string version, string serialized)
  {
    AdvancedVibration advancedVibration = new AdvancedVibration();
    if (string.IsNullOrEmpty(serialized))
    {
      advancedVibration.stsStatus = 0;
      advancedVibration.Mode = 0;
      advancedVibration.FrequencyAxisX = 0.0;
      advancedVibration.FrequencyAxisY = 0.0;
      advancedVibration.FrequencyAxisZ = 0.0;
      advancedVibration.VelocityRmsAxisX = 0.0;
      advancedVibration.VelocityRmsAxisY = 0.0;
      advancedVibration.VelocityRmsAxisZ = 0.0;
      advancedVibration.AccelerationRmsAxisX = 0L;
      advancedVibration.AccelerationRmsAxisY = 0L;
      advancedVibration.AccelerationRmsAxisZ = 0L;
      advancedVibration.AccelerationPeakAxisX = 0.0;
      advancedVibration.AccelerationPeakAxisY = 0.0;
      advancedVibration.AccelerationPeakAxisZ = 0.0;
      advancedVibration.DisplacementAxisX = 0.0;
      advancedVibration.DisplacementAxisY = 0.0;
      advancedVibration.DisplacementAxisZ = 0.0;
      advancedVibration.CrestFactorAxisX = 0.0;
      advancedVibration.CrestFactorAxisY = 0.0;
      advancedVibration.CrestFactorAxisZ = 0.0;
      advancedVibration.DutyCycle = 0;
      advancedVibration.Temperature = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length > 1)
      {
        try
        {
          advancedVibration.Mode = strArray[0].ToInt();
          advancedVibration.FrequencyAxisX = strArray[1].ToDouble();
          advancedVibration.FrequencyAxisY = strArray[2].ToDouble();
          advancedVibration.FrequencyAxisZ = strArray[3].ToDouble();
          advancedVibration.VelocityRmsAxisX = strArray[4].ToDouble();
          advancedVibration.VelocityRmsAxisY = strArray[5].ToDouble();
          advancedVibration.VelocityRmsAxisZ = strArray[6].ToDouble();
          advancedVibration.AccelerationRmsAxisX = strArray[7].ToLong();
          advancedVibration.AccelerationRmsAxisY = strArray[8].ToLong();
          advancedVibration.AccelerationRmsAxisZ = strArray[9].ToLong();
          advancedVibration.AccelerationPeakAxisX = strArray[10].ToDouble();
          advancedVibration.AccelerationPeakAxisY = strArray[11].ToDouble();
          advancedVibration.AccelerationPeakAxisZ = strArray[12].ToDouble();
          advancedVibration.DisplacementAxisX = strArray[13].ToDouble();
          advancedVibration.DisplacementAxisY = strArray[14].ToDouble();
          advancedVibration.DisplacementAxisZ = strArray[15].ToDouble();
          advancedVibration.CrestFactorAxisX = strArray[16 /*0x10*/].ToDouble();
          advancedVibration.CrestFactorAxisY = strArray[17].ToDouble();
          advancedVibration.CrestFactorAxisZ = strArray[18].ToDouble();
          advancedVibration.DutyCycle = strArray[19].ToInt();
          advancedVibration.Temperature = strArray[20].ToDouble();
          try
          {
            advancedVibration.stsStatus = strArray[21].ToInt();
          }
          catch
          {
            advancedVibration.stsStatus = 0;
          }
        }
        catch
        {
          advancedVibration.stsStatus = 0;
          advancedVibration.Mode = 0;
          advancedVibration.FrequencyAxisX = 0.0;
          advancedVibration.FrequencyAxisY = 0.0;
          advancedVibration.FrequencyAxisZ = 0.0;
          advancedVibration.VelocityRmsAxisX = 0.0;
          advancedVibration.VelocityRmsAxisY = 0.0;
          advancedVibration.VelocityRmsAxisZ = 0.0;
          advancedVibration.AccelerationRmsAxisX = 0L;
          advancedVibration.AccelerationRmsAxisY = 0L;
          advancedVibration.AccelerationRmsAxisZ = 0L;
          advancedVibration.AccelerationPeakAxisX = 0.0;
          advancedVibration.AccelerationPeakAxisY = 0.0;
          advancedVibration.AccelerationPeakAxisZ = 0.0;
          advancedVibration.DisplacementAxisX = 0.0;
          advancedVibration.DisplacementAxisY = 0.0;
          advancedVibration.DisplacementAxisZ = 0.0;
          advancedVibration.CrestFactorAxisX = 0.0;
          advancedVibration.CrestFactorAxisY = 0.0;
          advancedVibration.CrestFactorAxisZ = 0.0;
          advancedVibration.DutyCycle = 0;
          advancedVibration.Temperature = 0.0;
        }
      }
      else
      {
        advancedVibration.Mode = strArray[0].ToInt();
        advancedVibration.FrequencyAxisX = strArray[0].ToDouble();
        advancedVibration.FrequencyAxisY = strArray[0].ToDouble();
        advancedVibration.FrequencyAxisZ = strArray[0].ToDouble();
        advancedVibration.VelocityRmsAxisX = strArray[0].ToDouble();
        advancedVibration.VelocityRmsAxisY = strArray[0].ToDouble();
        advancedVibration.VelocityRmsAxisZ = strArray[0].ToDouble();
        advancedVibration.AccelerationRmsAxisX = strArray[0].ToLong();
        advancedVibration.AccelerationRmsAxisY = strArray[0].ToLong();
        advancedVibration.AccelerationRmsAxisZ = strArray[0].ToLong();
        advancedVibration.AccelerationPeakAxisX = strArray[0].ToDouble();
        advancedVibration.AccelerationPeakAxisY = strArray[0].ToDouble();
        advancedVibration.AccelerationPeakAxisZ = strArray[0].ToDouble();
        advancedVibration.DisplacementAxisX = strArray[0].ToDouble();
        advancedVibration.DisplacementAxisY = strArray[0].ToDouble();
        advancedVibration.DisplacementAxisZ = strArray[0].ToDouble();
        advancedVibration.CrestFactorAxisX = strArray[0].ToDouble();
        advancedVibration.CrestFactorAxisY = strArray[0].ToDouble();
        advancedVibration.CrestFactorAxisZ = strArray[0].ToDouble();
        advancedVibration.DutyCycle = strArray[0].ToInt();
        advancedVibration.Temperature = strArray[0].ToDouble();
      }
    }
    return advancedVibration;
  }

  public static AdvancedVibration Create(byte[] sdm, int startIndex)
  {
    AdvancedVibration advancedVibration = new AdvancedVibration();
    advancedVibration.stsStatus = (int) sdm[startIndex - 1] >> 4;
    if (advancedVibration.stsStatus >= 8)
    {
      advancedVibration.Mode = 0;
      advancedVibration.FrequencyAxisX = 0.0;
      advancedVibration.FrequencyAxisY = 0.0;
      advancedVibration.FrequencyAxisZ = 0.0;
      advancedVibration.VelocityRmsAxisX = 0.0;
      advancedVibration.VelocityRmsAxisY = 0.0;
      advancedVibration.VelocityRmsAxisZ = 0.0;
      advancedVibration.AccelerationRmsAxisX = 0L;
      advancedVibration.AccelerationRmsAxisY = 0L;
      advancedVibration.AccelerationRmsAxisZ = 0L;
      advancedVibration.AccelerationPeakAxisX = 0.0;
      advancedVibration.AccelerationPeakAxisY = 0.0;
      advancedVibration.AccelerationPeakAxisZ = 0.0;
      advancedVibration.DisplacementAxisX = 0.0;
      advancedVibration.DisplacementAxisY = 0.0;
      advancedVibration.DisplacementAxisZ = 0.0;
      advancedVibration.CrestFactorAxisX = 0.0;
      advancedVibration.CrestFactorAxisY = 0.0;
      advancedVibration.CrestFactorAxisZ = 0.0;
      advancedVibration.DutyCycle = 0;
      advancedVibration.Temperature = 0.0;
    }
    else
    {
      advancedVibration.Mode = (int) sdm[startIndex];
      advancedVibration.FrequencyAxisX = BitConverter.ToUInt16(sdm, startIndex + 1).ToDouble() / 5.0;
      advancedVibration.FrequencyAxisY = BitConverter.ToUInt16(sdm, startIndex + 3).ToDouble() / 5.0;
      advancedVibration.FrequencyAxisZ = BitConverter.ToUInt16(sdm, startIndex + 5).ToDouble() / 5.0;
      switch (advancedVibration.Mode)
      {
        case 0:
          advancedVibration.VelocityRmsAxisX = BitConverter.ToUInt16(sdm, startIndex + 7).ToDouble() / 100.0;
          advancedVibration.VelocityRmsAxisY = BitConverter.ToUInt16(sdm, startIndex + 9).ToDouble() / 100.0;
          advancedVibration.VelocityRmsAxisZ = BitConverter.ToUInt16(sdm, startIndex + 11).ToDouble() / 100.0;
          break;
        case 1:
          advancedVibration.AccelerationRmsAxisX = BitConverter.ToUInt16(sdm, startIndex + 7).ToLong() * 10L;
          advancedVibration.AccelerationRmsAxisY = BitConverter.ToUInt16(sdm, startIndex + 9).ToLong() * 10L;
          advancedVibration.AccelerationRmsAxisZ = BitConverter.ToUInt16(sdm, startIndex + 11).ToLong() * 10L;
          break;
        case 2:
          advancedVibration.AccelerationPeakAxisX = BitConverter.ToUInt16(sdm, startIndex + 7).ToDouble() * 10.0;
          advancedVibration.AccelerationPeakAxisY = BitConverter.ToUInt16(sdm, startIndex + 9).ToDouble() * 10.0;
          advancedVibration.AccelerationPeakAxisZ = BitConverter.ToUInt16(sdm, startIndex + 11).ToDouble() * 10.0;
          break;
        case 4:
          advancedVibration.DisplacementAxisX = BitConverter.ToUInt16(sdm, startIndex + 7).ToDouble() / 100.0;
          advancedVibration.DisplacementAxisY = BitConverter.ToUInt16(sdm, startIndex + 9).ToDouble() / 100.0;
          advancedVibration.DisplacementAxisZ = BitConverter.ToUInt16(sdm, startIndex + 11).ToDouble() / 100.0;
          break;
      }
      advancedVibration.CrestFactorAxisX = sdm[startIndex + 13].ToDouble() / 100.0 + 1.41;
      advancedVibration.CrestFactorAxisY = sdm[startIndex + 14].ToDouble() / 100.0 + 1.41;
      advancedVibration.CrestFactorAxisZ = sdm[startIndex + 15].ToDouble() / 100.0 + 1.41;
      advancedVibration.DutyCycle = sdm[startIndex + 16 /*0x10*/].ToInt();
      advancedVibration.Temperature = BitConverter.ToInt16(sdm, startIndex + 17).ToDouble() / 10.0;
    }
    return advancedVibration;
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
      ApplicationID = AdvancedVibration.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion,
      Scale = AdvancedVibration.IsHertz(sensor.SensorID) ? "Hz" : "rpm"
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
    bool flag = AdvancedVibration.IsHertz(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["VibrationMode_Manual"]))
    {
      int o = collection["VibrationMode_Manual"].ToInt();
      if (o < 0)
        o = 0;
      if (o > 4)
        o = 4;
      AdvancedVibration.SetVibrationMode(sensor, o.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["VibrationHysteresis_Manual"]))
    {
      double o = collection["VibrationHysteresis_Manual"].ToDouble();
      if (new Version(sensor.FirmwareVersion) >= new Version("10.23.13.9"))
      {
        switch (AdvancedVibration.GetVibrationMode(sensor))
        {
          case 1:
          case 2:
            o /= 10.0;
            break;
        }
      }
      if (o < 0.0)
        o = 0.0;
      if (o > (double) ushort.MaxValue)
        o = (double) ushort.MaxValue;
      AdvancedVibration.SetVibrationHysteresis(sensor, o.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["VibrationAwareThreshold_Manual"]))
    {
      double num = collection["VibrationAwareThreshold_Manual"].ToDouble();
      switch (AdvancedVibration.GetVibrationMode(sensor))
      {
        case 0:
        case 4:
          num *= 100.0;
          break;
        case 1:
        case 2:
          if (new Version(sensor.FirmwareVersion) >= new Version("10.23.13.9"))
          {
            num /= 10.0;
            break;
          }
          break;
      }
      if (num < 0.0)
        num = 0.0;
      if (num > (double) ushort.MaxValue)
        num = (double) ushort.MaxValue;
      AdvancedVibration.SetVibrationAwareThreshold(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["VibrationSensitivityThreshold_Manual"]))
    {
      double num = collection["VibrationSensitivityThreshold_Manual"].ToDouble();
      if (num < 0.0)
        num = 0.0;
      if (num > 2.55)
        num = 2.55;
      AdvancedVibration.SetVibrationSensitivityThreshold(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["SampleRate_Manual"]))
    {
      int o = collection["SampleRate_Manual"].ToInt();
      if (o < 0)
        o = 0;
      if (o > 16 /*0x10*/)
        o = 16 /*0x10*/;
      AdvancedVibration.SetSampleRate(sensor, o.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["WindowFunction_Manual"]))
    {
      int o = collection["WindowFunction_Manual"].ToInt();
      if (o < 0)
        o = 0;
      if (o > 2)
        o = 2;
      AdvancedVibration.SetWindowFunction(sensor, o.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["AccelerometerRange_Manual"]))
    {
      int o = collection["AccelerometerRange_Manual"].ToInt();
      if (o < 0)
        o = 0;
      if (o > 2)
        o = 2;
      AdvancedVibration.SetAccelerometerRange(sensor, o.ToInt());
    }
    if (!string.IsNullOrEmpty(collection["MeasurementInterval_Manual"]))
    {
      double num = collection["MeasurementInterval_Manual"].ToDouble();
      if (num < 1.0)
        num = 1.0;
      if (num > 43200.0)
        num = 43200.0;
      AdvancedVibration.SetMeasurementInterval(sensor, num);
    }
    if (string.IsNullOrEmpty(collection["MinFrequency_Manual"]) || string.IsNullOrEmpty(collection["MaxFrequency_Manual"]))
      return;
    int sampleRate = AdvancedVibration.GetSampleRate(sensor);
    double num1 = flag ? collection["MinFrequency_Manual"].ToDouble() : collection["MinFrequency_Manual"].ToDouble().ToHertz();
    double num2 = flag ? collection["MaxFrequency_Manual"].ToDouble() : collection["MaxFrequency_Manual"].ToDouble().ToHertz();
    double num3;
    double num4;
    switch (AdvancedVibration.GetVibrationMode(sensor))
    {
      case 0:
        switch (sampleRate)
        {
          case 1:
            num3 = 75.0 / 128.0;
            num4 = 9.375;
            break;
          case 2:
            num3 = 75.0 / 64.0;
            num4 = 18.75;
            break;
          case 3:
            num3 = 75.0 / 32.0;
            num4 = 37.5;
            break;
          case 4:
            num3 = 75.0 / 16.0;
            num4 = 75.0;
            break;
          case 5:
            num3 = 9.375;
            num4 = 150.0;
            break;
          case 6:
            num3 = 18.75;
            num4 = 300.0;
            break;
          case 7:
            num3 = 37.5;
            num4 = 600.0;
            break;
          case 13:
            num3 = 150.0;
            num4 = 2400.0;
            break;
          case 14:
            num3 = 300.0;
            num4 = 4800.0;
            break;
          case 15:
            num3 = 600.0;
            num4 = 6500.0;
            break;
          default:
            num3 = 75.0;
            num4 = 1200.0;
            break;
        }
        break;
      case 1:
      case 2:
        switch (sampleRate)
        {
          case 1:
            num3 = 25.0 / 64.0;
            num4 = 9.375;
            break;
          case 2:
            num3 = 25.0 / 32.0;
            num4 = 18.75;
            break;
          case 3:
            num3 = 25.0 / 16.0;
            num4 = 37.5;
            break;
          case 4:
            num3 = 3.125;
            num4 = 75.0;
            break;
          case 5:
            num3 = 6.25;
            num4 = 150.0;
            break;
          case 6:
            num3 = 12.5;
            num4 = 300.0;
            break;
          case 7:
            num3 = 25.0;
            num4 = 600.0;
            break;
          case 13:
            num3 = 100.0;
            num4 = 2400.0;
            break;
          case 14:
            num3 = 200.0;
            num4 = 4800.0;
            break;
          case 15:
            num3 = 400.0;
            num4 = 6500.0;
            break;
          default:
            num3 = 50.0;
            num4 = 1200.0;
            break;
        }
        break;
      case 4:
        switch (sampleRate)
        {
          case 1:
            num3 = 25.0 / 32.0;
            num4 = 9.375;
            break;
          case 2:
            num3 = 25.0 / 16.0;
            num4 = 18.75;
            break;
          case 3:
            num3 = 3.125;
            num4 = 37.5;
            break;
          case 4:
            num3 = 6.25;
            num4 = 75.0;
            break;
          case 5:
            num3 = 12.5;
            num4 = 150.0;
            break;
          case 6:
            num3 = 25.0;
            num4 = 300.0;
            break;
          case 7:
            num3 = 50.0;
            num4 = 600.0;
            break;
          case 13:
            num3 = 200.0;
            num4 = 2400.0;
            break;
          case 14:
            num3 = 400.0;
            num4 = 4800.0;
            break;
          case 15:
            num3 = 800.0;
            num4 = 6500.0;
            break;
          default:
            num3 = 100.0;
            num4 = 1200.0;
            break;
        }
        break;
      default:
        switch (sampleRate)
        {
          case 1:
            num3 = 25.0 / 128.0;
            num4 = 9.375;
            break;
          case 2:
            num3 = 25.0 / 64.0;
            num4 = 18.75;
            break;
          case 3:
            num3 = 25.0 / 32.0;
            num4 = 37.5;
            break;
          case 4:
            num3 = 25.0 / 16.0;
            num4 = 75.0;
            break;
          case 5:
            num3 = 3.125;
            num4 = 150.0;
            break;
          case 6:
            num3 = 6.25;
            num4 = 300.0;
            break;
          case 7:
            num3 = 12.5;
            num4 = 600.0;
            break;
          case 13:
            num3 = 50.0;
            num4 = 2400.0;
            break;
          case 14:
            num3 = 100.0;
            num4 = 4800.0;
            break;
          case 15:
            num3 = 200.0;
            num4 = 9600.0;
            break;
          default:
            num3 = 25.0;
            num4 = 1200.0;
            break;
        }
        break;
    }
    if (num1 < num3)
      num1 = num3;
    if (num1 > num4)
      num1 = num4;
    if (num2 < num3)
      num2 = num3;
    if (num2 > num4)
      num2 = num4;
    if (num2 < num1)
      num2 = num1;
    if (num1 > num2)
      num1 = num2;
    AdvancedVibration.SetMinFrequency(sensor, num1);
    AdvancedVibration.SetMaxFrequency(sensor, num2);
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
      numArrayList.Add(AdvancedVibrationBase.CalibrateFrame(sensor.SensorID, (double) nonCachedAttribute.Value2.ToInt()));
    else if (nonCachedAttribute.Value1 == "2")
      numArrayList.Add(AdvancedVibrationBase.BaselinesCalibrateFrame(sensor.SensorID));
    numArrayList.Add(sensor.ReadProfileConfig(29));
    return numArrayList;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(AdvancedVibration left, AdvancedVibration right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(AdvancedVibration left, AdvancedVibration right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(AdvancedVibration left, AdvancedVibration right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(AdvancedVibration left, AdvancedVibration right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(AdvancedVibration left, AdvancedVibration right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(AdvancedVibration left, AdvancedVibration right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is AdvancedVibration && this.Equals((MonnitApplicationBase) (obj as AdvancedVibration));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is AdvancedVibration && this.FrequencyAxisX == (right as AdvancedVibration).FrequencyAxisX;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is AdvancedVibration && this.FrequencyAxisX != (right as AdvancedVibration).FrequencyAxisX;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is AdvancedVibration && this.FrequencyAxisX < (right as AdvancedVibration).FrequencyAxisX;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is AdvancedVibration && this.FrequencyAxisX <= (right as AdvancedVibration).FrequencyAxisX;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is AdvancedVibration && this.FrequencyAxisX > (right as AdvancedVibration).FrequencyAxisX;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is AdvancedVibration && this.FrequencyAxisX >= (right as AdvancedVibration).FrequencyAxisX;
  }

  public static void SetVibrationHysteresis(Sensor sens, int value)
  {
    uint num = (uint) sens.Hysteresis & 4294901760U;
    sens.Hysteresis = (long) (num | (uint) (ushort) value);
  }

  public static int GetVibrationHysteresis(Sensor sens)
  {
    int vibrationMode = AdvancedVibration.GetVibrationMode(sens);
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
    int vibrationMode = AdvancedVibration.GetVibrationMode(sens);
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

  public static void SetMinFrequency(Sensor sens, double value)
  {
    value *= 10.0;
    uint num = (uint) sens.Calibration3 & 4294901760U;
    sens.Calibration3 = (long) (num | (uint) (ushort) value);
  }

  public static double GetMinFrequency(Sensor sens)
  {
    double Hz = ((double) (sens.Calibration3 & (long) ushort.MaxValue) / 10.0).ToDouble();
    return AdvancedVibration.IsHertz(sens.SensorID) ? Hz : Hz.ToRPM();
  }

  public static void SetMaxFrequency(Sensor sens, double value)
  {
    value *= 10.0;
    int num = (int) sens.Calibration3 & (int) ushort.MaxValue;
    sens.Calibration3 = (long) (num | (int) (ushort) value << 16 /*0x10*/);
  }

  public static double GetMaxFrequency(Sensor sens)
  {
    double Hz = (((uint) sens.Calibration3 >> 16 /*0x10*/) / 10U).ToDouble();
    return AdvancedVibration.IsHertz(sens.SensorID) ? Hz : Hz.ToRPM();
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
    AdvancedVibrationBase.GetDefaults(new Version(sensor.FirmwareVersion), sensor.GenerationType);
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
      AdvancedVibration.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      viewData["TempScale"] = "C";
      AdvancedVibration.MakeCelsius(sensor.SensorID);
    }
    if (string.IsNullOrEmpty(collection["measurementscale"]))
      return;
    if (collection["measurementscale"] == "on")
    {
      viewData["measurementscale"] = "Hz";
      AdvancedVibration.MakeHertz(sensor.SensorID);
    }
    else
    {
      viewData["measurementscale"] = "RPM";
      AdvancedVibration.MakeRPM(sensor.SensorID);
    }
  }
}
