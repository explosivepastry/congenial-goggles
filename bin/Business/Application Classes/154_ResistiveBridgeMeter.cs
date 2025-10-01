// Decompiled with JetBrains decompiler
// Type: Monnit.ResistiveBridgeMeter
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

public class ResistiveBridgeMeter : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;
  private SensorAttribute _Label;
  private SensorAttribute _ScaleMin;
  private SensorAttribute _ScaleMax;
  private SensorAttribute _RatedOutput;
  private SensorAttribute _Precision;
  private SensorAttribute _Capacity;
  private SensorAttribute _ManualCorrection;
  private SensorAttribute _ManualOffset;

  public static long MonnitApplicationID => 154;

  public static string ApplicationName => "Resistive Bridge Meter";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => ResistiveBridgeMeter.MonnitApplicationID;

  public double MilliVoltsPerVolts { get; set; }

  public double Temperature { get; set; }

  public int stsStatus { get; set; }

  public ResistiveBridgeMeter()
  {
  }

  public ResistiveBridgeMeter(long sensorId) => this.SetSensorAttributes(sensorId);

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.MilliVoltsPerVolts, "MilliVoltsPerVolts", this.MilliVoltsPerVolts),
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temperature)
      });
    }
  }

  public double TemperaturePlotValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? this.Temperature : this.Temperature.ToFahrenheit();
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      this.PlotValue,
      (object) this.TemperaturePlotValue
    };
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>()
    {
      ResistiveBridgeMeter.GetLabel(sensorID),
      ResistiveBridgeMeter.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    };
  }

  public override string NotificationString
  {
    get
    {
      if (this.stsStatus == 1)
        return " Hardware Error";
      if (this.stsStatus == 2)
        return " Max Measurement reached, lower gain";
      string empty = string.Empty;
      string str = this.CorF == null || !(this.CorF.Value == "C") ? this.TemperaturePlotValue.ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture) : this.TemperaturePlotValue.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
      return $"{this.PlotValue?.ToString()} {this.Label.Value} , Temperature: {str}";
    }
  }

  public override bool IsValid => this.stsStatus != 1;

  public override object PlotValue
  {
    get
    {
      return (object) this.ApplyUsersManualAdjustments(this.ApplyScale(this.MilliVoltsPerVolts)).ToString("N" + this.Precision.Value);
    }
  }

  public override string Serialize()
  {
    return $"{this.MilliVoltsPerVolts.ToString()}|{this.Temperature.ToString()}|{this.stsStatus.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) ResistiveBridgeMeter.Deserialize(version, serialized);
  }

  public static ResistiveBridgeMeter Deserialize(string version, string serialized)
  {
    ResistiveBridgeMeter resistiveBridgeMeter = new ResistiveBridgeMeter();
    if (string.IsNullOrEmpty(serialized))
    {
      resistiveBridgeMeter.MilliVoltsPerVolts = 0.0;
      resistiveBridgeMeter.Temperature = 0.0;
      resistiveBridgeMeter.stsStatus = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      resistiveBridgeMeter.MilliVoltsPerVolts = strArray[0].ToDouble();
      if (strArray.Length > 1)
      {
        resistiveBridgeMeter.Temperature = strArray[1].ToDouble();
        try
        {
          resistiveBridgeMeter.stsStatus = strArray[2].ToInt();
        }
        catch
        {
          resistiveBridgeMeter.stsStatus = 0;
        }
      }
      else
        resistiveBridgeMeter.Temperature = strArray[0].ToDouble();
    }
    return resistiveBridgeMeter;
  }

  public static ResistiveBridgeMeter Create(byte[] sdm, int startIndex)
  {
    ResistiveBridgeMeter resistiveBridgeMeter = new ResistiveBridgeMeter();
    resistiveBridgeMeter.stsStatus = (int) sdm[startIndex - 1] >> 4;
    double num = BitConverter.ToInt32(sdm, startIndex).ToDouble();
    resistiveBridgeMeter.MilliVoltsPerVolts = num / ResistiveBridgeMeterBase.ConversionFactor;
    DebugLog.CreateLog($"mVpVe6 = {num}, mVpV = {resistiveBridgeMeter.MilliVoltsPerVolts}", "ResistiveBridgeMeter.Create", 1234321);
    resistiveBridgeMeter.Temperature = BitConverter.ToInt16(sdm, startIndex + 4).ToDouble() / 10.0;
    return resistiveBridgeMeter;
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

  public SensorAttribute CorF
  {
    get
    {
      if (this._CorF == null)
      {
        this._CorF = new SensorAttribute();
        this._CorF.Name = nameof (CorF);
        this._CorF.Value = ResistiveBridgeMeterBase.DefaultCorF;
      }
      return this._CorF;
    }
  }

  public SensorAttribute Label
  {
    get
    {
      if (this._Label == null)
      {
        this._Label = new SensorAttribute();
        this._Label.Name = nameof (Label);
        this._Label.Value = ResistiveBridgeMeterBase.DefaultLabel;
      }
      return this._Label;
    }
  }

  public SensorAttribute ScaleMin
  {
    get
    {
      if (this._ScaleMin == null)
      {
        this._ScaleMin = new SensorAttribute();
        this._ScaleMin.Name = nameof (ScaleMin);
        this._ScaleMin.Value = ResistiveBridgeMeterBase.DefaultScaleMin.ToString();
      }
      return this._ScaleMin;
    }
  }

  public SensorAttribute ScaleMax
  {
    get
    {
      if (this._ScaleMax == null)
      {
        this._ScaleMax = new SensorAttribute();
        this._ScaleMax.Name = nameof (ScaleMax);
        this._ScaleMax.Value = ResistiveBridgeMeterBase.DefaultScaleMax.ToString();
      }
      return this._ScaleMax;
    }
  }

  public SensorAttribute RatedOutput
  {
    get
    {
      if (this._RatedOutput == null)
      {
        this._RatedOutput = new SensorAttribute();
        this._RatedOutput.Name = nameof (RatedOutput);
        this._RatedOutput.Value = ResistiveBridgeMeterBase.DefaultRatedOutput.ToString();
      }
      return this._RatedOutput;
    }
  }

  public SensorAttribute Precision
  {
    get
    {
      if (this._Precision == null)
      {
        this._Precision = new SensorAttribute();
        this._Precision.Name = nameof (Precision);
        this._Precision.Value = ResistiveBridgeMeterBase.DefaultPrecision.ToString();
      }
      return this._Precision;
    }
  }

  public SensorAttribute Capacity
  {
    get
    {
      if (this._Capacity == null)
      {
        this._Capacity = new SensorAttribute();
        this._Capacity.Name = nameof (Capacity);
        this._Capacity.Value = ResistiveBridgeMeterBase.DefaultCapacity.ToString();
      }
      return this._Capacity;
    }
  }

  public SensorAttribute ManualCorrection
  {
    get
    {
      if (this._ManualCorrection == null)
      {
        this._ManualCorrection = new SensorAttribute();
        this._ManualCorrection.Name = nameof (ManualCorrection);
        this._ManualCorrection.Value = ResistiveBridgeMeterBase.DefaultManualCorrection.ToString();
      }
      return this._ManualCorrection;
    }
  }

  public SensorAttribute ManualOffset
  {
    get
    {
      if (this._ManualOffset == null)
      {
        this._ManualOffset = new SensorAttribute();
        this._ManualOffset.Name = nameof (ManualOffset);
        this._ManualOffset.Value = ResistiveBridgeMeterBase.DefaultManualOffset.ToString();
      }
      return this._ManualOffset;
    }
  }

  public double ApplyScale(double mVV)
  {
    return mVV / this.RatedOutput.Value.ToDouble() * this.Capacity.Value.ToDouble();
  }

  public double ApplyUsersManualAdjustments(double applyScaleResults)
  {
    return applyScaleResults * this.ManualCorrection.Value.ToDouble() + this.ManualOffset.Value.ToDouble();
  }

  public double UndoScale(double scaledValues)
  {
    return scaledValues * this.RatedOutput.Value.ToDouble() / this.Capacity.Value.ToDouble();
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
      if (sensorAttribute.Name == "Label")
        this._Label = sensorAttribute;
      if (sensorAttribute.Name == "ScaleMin")
        this._ScaleMin = sensorAttribute;
      if (sensorAttribute.Name == "ScaleMax")
        this._ScaleMax = sensorAttribute;
      if (sensorAttribute.Name == "RatedOutput")
        this._RatedOutput = sensorAttribute;
      if (sensorAttribute.Name == "Precision")
        this._Precision = sensorAttribute;
      if (sensorAttribute.Name == "Capacity")
        this._Capacity = sensorAttribute;
      if (sensorAttribute.Name == "ManualCorrection")
        this._ManualCorrection = sensorAttribute;
      if (sensorAttribute.Name == "ManualOffset")
        this._ManualOffset = sensorAttribute;
    }
  }

  public static void SetMinimumThreshold(
    Sensor sensor,
    double threshold,
    ResistiveBridgeMeter appBase)
  {
    if ((object) appBase == null)
      appBase = new ResistiveBridgeMeter(sensor.SensorID);
    long num = (long) (appBase.UndoScale(threshold) * ResistiveBridgeMeterBase.ConversionFactor);
    if (num < (long) ResistiveBridgeMeterBase.MinimumThreshold)
      num = (long) ResistiveBridgeMeterBase.MinimumThreshold;
    if (num > sensor.MaximumThreshold)
      num = sensor.MaximumThreshold;
    sensor.MinimumThreshold = num;
  }

  public static double GetMinimumThreshold(Sensor sensor, ResistiveBridgeMeter appBase)
  {
    if ((object) appBase == null)
      appBase = new ResistiveBridgeMeter(sensor.SensorID);
    return appBase.ApplyScale((double) sensor.MinimumThreshold / ResistiveBridgeMeterBase.ConversionFactor);
  }

  public static void SetMaximumThreshold(
    Sensor sensor,
    double threshold,
    ResistiveBridgeMeter appBase)
  {
    if ((object) appBase == null)
      appBase = new ResistiveBridgeMeter(sensor.SensorID);
    long num = (long) (appBase.UndoScale(threshold) * ResistiveBridgeMeterBase.ConversionFactor);
    if (num > (long) ResistiveBridgeMeterBase.MaximumThreshold)
      num = (long) ResistiveBridgeMeterBase.MaximumThreshold;
    if (num < sensor.MinimumThreshold)
      num = sensor.MinimumThreshold;
    sensor.MaximumThreshold = num;
  }

  public static double GetMaximumThreshold(Sensor sensor, ResistiveBridgeMeter appBase)
  {
    if ((object) appBase == null)
      appBase = new ResistiveBridgeMeter(sensor.SensorID);
    return appBase.ApplyScale((double) sensor.MaximumThreshold / ResistiveBridgeMeterBase.ConversionFactor);
  }

  public static double GetBaseMaximumThreshold(Sensor sensor, ResistiveBridgeMeter appBase)
  {
    if ((object) appBase == null)
      appBase = new ResistiveBridgeMeter(sensor.SensorID);
    return appBase.ApplyScale((double) ResistiveBridgeMeterBase.MaximumThreshold / ResistiveBridgeMeterBase.ConversionFactor);
  }

  public static double GetBaseMinimumThreshold(Sensor sensor, ResistiveBridgeMeter appBase)
  {
    if ((object) appBase == null)
      appBase = new ResistiveBridgeMeter(sensor.SensorID);
    return appBase.ApplyScale((double) ResistiveBridgeMeterBase.MinimumThreshold / ResistiveBridgeMeterBase.ConversionFactor);
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    ResistiveBridgeMeter appBase = new ResistiveBridgeMeter(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
      ResistiveBridgeMeter.SetMinimumThreshold(sensor, collection["MinimumThreshold_Manual"].ToDouble(), appBase);
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
      ResistiveBridgeMeter.SetMaximumThreshold(sensor, collection["MaximumThreshold_Manual"].ToDouble(), appBase);
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
    {
      long num1 = (long) (collection["Hysteresis_Manual"].ToDouble() * ResistiveBridgeMeterBase.ConversionFactor);
      long num2 = Convert.ToInt64(collection["Hysteresis_Manual"].ToDouble() * ResistiveBridgeMeterBase.ConversionFactor);
      if (num2 > (long) ResistiveBridgeMeterBase.HysteresisMaximum)
        num2 = (long) ResistiveBridgeMeterBase.HysteresisMaximum;
      if (num2 < (long) ResistiveBridgeMeterBase.HysteresisMinimum)
        num2 = (long) ResistiveBridgeMeterBase.HysteresisMinimum;
      sensor.Hysteresis = Convert.ToInt64(num2);
    }
    byte[] numArray1 = new byte[2];
    if (!string.IsNullOrEmpty(collection["Gain"]))
    {
      int num = collection["Gain"].ToInt();
      if (num < ResistiveBridgeMeterBase.ADCGainMinimum)
        num = ResistiveBridgeMeterBase.ADCGainMinimum;
      if (num > ResistiveBridgeMeterBase.ADCGainMaximum)
        num = ResistiveBridgeMeterBase.ADCGainMaximum;
      Array.Copy((Array) BitConverter.GetBytes(num), 0, (Array) numArray1, 0, 2);
    }
    byte[] numArray2 = new byte[2];
    if (!string.IsNullOrEmpty(collection["NoiseRejection"]))
    {
      int num = collection["NoiseRejection"].ToInt();
      if (num < ResistiveBridgeMeterBase.NoiseRejectionMinimum)
        num = ResistiveBridgeMeterBase.NoiseRejectionMinimum;
      if (num > ResistiveBridgeMeterBase.NoiseRejectionMaximum)
        num = ResistiveBridgeMeterBase.NoiseRejectionMaximum;
      Array.Copy((Array) BitConverter.GetBytes(num), 0, (Array) numArray2, 0, 2);
    }
    if (!string.IsNullOrEmpty(collection["Gain"]) || !string.IsNullOrEmpty(collection["NoiseRejection"]))
    {
      byte[] destinationArray = new byte[4];
      Array.Copy((Array) numArray1, (Array) destinationArray, 2);
      Array.Copy((Array) numArray2, 0, (Array) destinationArray, 2, 2);
      sensor.Calibration1 = (long) BitConverter.ToUInt32(destinationArray, 0);
    }
    if (string.IsNullOrEmpty(collection["AveragingFilter"]))
      return;
    long num3 = collection["AveragingFilter"].ToLong();
    if (num3 < (long) ResistiveBridgeMeterBase.AveragingFilterMinimum)
      num3 = (long) ResistiveBridgeMeterBase.AveragingFilterMinimum;
    if (num3 > (long) ResistiveBridgeMeterBase.AveragingFilterMaximum)
      num3 = (long) ResistiveBridgeMeterBase.AveragingFilterMaximum;
    sensor.Calibration2 = num3;
  }

  private static double DataRawToPct(long dataRaw)
  {
    return dataRaw.ToDouble() / ResistiveBridgeMeterBase.ConversionFactor;
  }

  private static long DataPctToRaw(double dataPct)
  {
    return (dataPct * ResistiveBridgeMeterBase.ConversionFactor).ToLong();
  }

  public static string GetLabel(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        return sensorAttribute.Value;
    }
    return ResistiveBridgeMeterBase.DefaultLabel;
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

  public static double GetRatedOutput(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "RatedOutput")
        return sensorAttribute.Value.ToDouble();
    }
    return ResistiveBridgeMeterBase.DefaultRatedOutput;
  }

  public static void SetRatedOutput(long sensorID, double val)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "RatedOutput")
      {
        sensorAttribute.Value = val.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "RatedOutput",
        Value = val.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static int GetPrecision(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Precision")
        return sensorAttribute.Value.ToInt();
    }
    return ResistiveBridgeMeterBase.DefaultPrecision;
  }

  public static void SetPrecision(long sensorID, int val)
  {
    if (val < 0)
      val = 0;
    if (val > 10)
      val = 10;
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Precision")
      {
        sensorAttribute.Value = val.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "Precision",
        Value = val.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static double? GetScaleMin(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ScaleMin")
        return new double?(sensorAttribute.Value.ToDouble());
    }
    return ResistiveBridgeMeterBase.DefaultScaleMin;
  }

  public static void SetScaleMin(long sensorID, double? val)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ScaleMin")
      {
        sensorAttribute.Value = val.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "ScaleMin",
        Value = val.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static double? GetScaleMax(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ScaleMax")
        return new double?(sensorAttribute.Value.ToDouble());
    }
    return ResistiveBridgeMeterBase.DefaultScaleMax;
  }

  public static void SetScaleMax(long sensorID, double? val)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ScaleMax")
      {
        sensorAttribute.Value = val.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "ScaleMax",
        Value = val.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void SetScaleTypeOfBridge(long sensorID, string val)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "TypeOfBridge")
      {
        sensorAttribute.Value = val;
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "TypeOfBridge",
        Value = val,
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetScaleTypeOfBridge(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "TypeOfBridge")
        return sensorAttribute.Value;
    }
    return ResistiveBridgeMeterBase.DefaultTypeOfBridge;
  }

  public static void SetScaleCapacity(long sensorID, double val)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Capacity")
      {
        sensorAttribute.Value = val.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "Capacity",
        Value = val.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetScaleCapacity(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Capacity")
        return sensorAttribute.Value;
    }
    return ResistiveBridgeMeterBase.DefaultCapacity.ToString();
  }

  public static int GetGain(Sensor sensor)
  {
    return (int) BitConverter.ToInt16(BitConverter.GetBytes(sensor.Calibration1), 0);
  }

  public static int GetNoiseFilter(Sensor sensor)
  {
    return (int) BitConverter.ToInt16(BitConverter.GetBytes(sensor.Calibration1), 2);
  }

  public static double GetSpanCalibration(Sensor sensor)
  {
    return (double) sensor.Calibration3 / ResistiveBridgeMeterBase.ConversionFactor;
  }

  public static double GetOffsetCalibration(Sensor sensor)
  {
    return (double) sensor.Calibration4 / ResistiveBridgeMeterBase.ConversionFactor;
  }

  public static int GetHysteresis(Sensor sensor)
  {
    return ((double) sensor.Hysteresis / ResistiveBridgeMeterBase.ConversionFactor).ToInt();
  }

  public static void SetCalibrateManualCorrection(long sensorID, double val)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ManualCorrection")
      {
        sensorAttribute.Value = val.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "ManualCorrection",
        Value = val.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetCalibrateManualCorrection(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ManualCorrection")
        return sensorAttribute.Value;
    }
    return "1";
  }

  public static void SetCalibrateManualOffset(long sensorID, double val)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ManualOffset")
      {
        sensorAttribute.Value = val.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "ManualOffset",
        Value = val.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetCalibrateManualOffset(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ManualOffset")
        return sensorAttribute.Value;
    }
    return "0";
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection returnData)
  {
    double defaultRatedOutput = ResistiveBridgeMeterBase.DefaultRatedOutput;
    string defaultLabel = ResistiveBridgeMeterBase.DefaultLabel;
    int defaultPrecision = ResistiveBridgeMeterBase.DefaultPrecision;
    double? defaultScaleMax = ResistiveBridgeMeterBase.DefaultScaleMax;
    double? defaultScaleMin = ResistiveBridgeMeterBase.DefaultScaleMin;
    string defaultCorF = ResistiveBridgeMeterBase.DefaultCorF;
    string defaultTypeOfBridge = ResistiveBridgeMeterBase.DefaultTypeOfBridge;
    double defaultCapacity = ResistiveBridgeMeterBase.DefaultCapacity;
    if (!string.IsNullOrEmpty(collection["Reset"]) && collection["Reset"].ToInt() > 0)
    {
      ResistiveBridgeMeter.SetRatedOutput(sensor.SensorID, defaultRatedOutput);
      ResistiveBridgeMeter.SetLabel(sensor.SensorID, defaultLabel);
      ResistiveBridgeMeter.SetPrecision(sensor.SensorID, defaultPrecision);
      ResistiveBridgeMeter.SetScaleMax(sensor.SensorID, defaultScaleMax);
      ResistiveBridgeMeter.SetScaleMin(sensor.SensorID, defaultScaleMin);
      ResistiveBridgeMeter.MakeFahrenheit(sensor.SensorID);
      ResistiveBridgeMeter.SetScaleTypeOfBridge(sensor.SensorID, defaultTypeOfBridge);
      ResistiveBridgeMeter.SetScaleCapacity(sensor.SensorID, defaultCapacity);
      returnData["TempScale"] = "F";
      returnData["Label"] = defaultLabel;
      returnData["RatedOutput"] = defaultRatedOutput.ToString();
      returnData["Precision"] = defaultPrecision.ToString();
      returnData["ScaleMax"] = defaultScaleMax.ToString();
      returnData["ScaleMin"] = defaultScaleMin.ToString();
      returnData["TypeOfBridge"] = defaultTypeOfBridge;
      returnData["Capacity"] = defaultCapacity.ToString();
    }
    else
    {
      if (!string.IsNullOrEmpty(collection["TempScale"]) && collection["TempScale"] == "on")
      {
        returnData["TempScale"] = "F";
        ResistiveBridgeMeter.MakeFahrenheit(sensor.SensorID);
      }
      else
      {
        returnData["TempScale"] = "C";
        ResistiveBridgeMeter.MakeCelsius(sensor.SensorID);
      }
      if (!string.IsNullOrEmpty(collection["Label"]))
      {
        ResistiveBridgeMeter.SetLabel(sensor.SensorID, collection["Label"]);
        returnData["Label"] = collection["Label"];
      }
      else
      {
        ResistiveBridgeMeter.SetLabel(sensor.SensorID, defaultLabel);
        returnData["Label"] = defaultLabel;
      }
      if (!string.IsNullOrEmpty(collection["RatedOutput"]))
      {
        ResistiveBridgeMeter.SetRatedOutput(sensor.SensorID, collection["RatedOutput"].ToDouble());
        returnData["RatedOutput"] = collection["RatedOutput"];
      }
      else
      {
        ResistiveBridgeMeter.SetRatedOutput(sensor.SensorID, defaultRatedOutput);
        returnData["RatedOutput"] = defaultRatedOutput.ToString();
      }
      if (!string.IsNullOrEmpty(collection["Precision"]))
      {
        ResistiveBridgeMeter.SetPrecision(sensor.SensorID, collection["Precision"].ToInt());
        returnData["Precision"] = collection["Precision"];
      }
      else
      {
        ResistiveBridgeMeter.SetPrecision(sensor.SensorID, defaultPrecision);
        returnData["Precision"] = defaultPrecision.ToString();
      }
      if (!string.IsNullOrEmpty(collection["ScaleMax"]))
      {
        ResistiveBridgeMeter.SetScaleMax(sensor.SensorID, new double?(collection["ScaleMax"].ToDouble()));
        returnData["ScaleMax"] = collection["ScaleMax"];
      }
      else
      {
        ResistiveBridgeMeter.SetScaleMax(sensor.SensorID, defaultScaleMax);
        returnData["ScaleMax"] = defaultScaleMax.ToString();
      }
      if (!string.IsNullOrEmpty(collection["ScaleMin"]))
      {
        ResistiveBridgeMeter.SetScaleMin(sensor.SensorID, new double?(collection["ScaleMin"].ToDouble()));
        returnData["ScaleMin"] = collection["ScaleMin"];
      }
      else
      {
        ResistiveBridgeMeter.SetScaleMin(sensor.SensorID, defaultScaleMin);
        returnData["ScaleMin"] = defaultScaleMin.ToString();
      }
      if (!string.IsNullOrEmpty(collection["TypeOfBridge"]))
      {
        ResistiveBridgeMeter.SetScaleTypeOfBridge(sensor.SensorID, collection["TypeOfBridge"]);
        returnData["TypeOfBridge"] = collection["TypeOfBridge"];
      }
      else
      {
        ResistiveBridgeMeter.SetScaleTypeOfBridge(sensor.SensorID, defaultTypeOfBridge);
        returnData["TypeOfBridge"] = defaultTypeOfBridge;
      }
      if (!string.IsNullOrEmpty(collection["Capacity"]))
      {
        ResistiveBridgeMeter.SetScaleCapacity(sensor.SensorID, collection["Capacity"].ToDouble());
        returnData["Capacity"] = collection["Capacity"];
      }
      else
      {
        ResistiveBridgeMeter.SetScaleCapacity(sensor.SensorID, defaultCapacity);
        returnData["Capacity"] = defaultCapacity.ToString();
      }
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    ResistiveBridgeMeter resistiveBridgeMeter = new ResistiveBridgeMeter(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["Observed"]))
    {
      double scaledValues = collection["Observed"].ToDouble();
      double num1 = resistiveBridgeMeter.UndoScale(scaledValues);
      double num2 = 0.0;
      double pct1 = ResistiveBridgeMeter.DataRawToPct(sensor.Calibration3);
      double pct2 = ResistiveBridgeMeter.DataRawToPct(sensor.Calibration4);
      double dataPct = (num2 - num1) / pct1 + pct2;
      sensor.Calibration3 = (long) ResistiveBridgeMeterBase.DefaultSpanCalibration;
      sensor.Calibration4 = ResistiveBridgeMeter.DataPctToRaw(dataPct);
      sensor.Save();
    }
    else
    {
      if (!string.IsNullOrEmpty(collection["ManualCorrection"]))
        ResistiveBridgeMeter.SetCalibrateManualCorrection(sensor.SensorID, collection["ManualCorrection"].ToDouble());
      else
        ResistiveBridgeMeter.SetCalibrateManualCorrection(sensor.SensorID, 1.0);
      if (!string.IsNullOrEmpty(collection["ManualOffset"]))
        ResistiveBridgeMeter.SetCalibrateManualOffset(sensor.SensorID, collection["ManualOffset"].ToDouble());
      else
        ResistiveBridgeMeter.SetCalibrateManualOffset(sensor.SensorID, 0.0);
    }
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
    target.EventDetectionType = source.EventDetectionType;
    target.EventDetectionPeriod = source.EventDetectionPeriod;
    target.EventDetectionCount = source.EventDetectionCount;
    target.RearmTime = source.RearmTime;
    target.BiStable = source.BiStable;
    target.TagString = source.TagString;
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
    if (sensor.Calibration3 == (long) uint.MaxValue)
    {
      sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
      sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
    }
    sensor.EventDetectionType = sensor.DefaultValue<int>("DefaultEventDetectionType");
    sensor.EventDetectionPeriod = sensor.DefaultValue<int>("DefaultEventDetectionPeriod");
    sensor.EventDetectionCount = sensor.DefaultValue<int>("DefaultEventDetectionCount");
    sensor.RearmTime = sensor.DefaultValue<int>("DefaultRearmTime");
    sensor.BiStable = sensor.DefaultValue<int>("DefaultBiStable");
  }

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensor.SensorID))
    {
      if (sensorAttribute.Name == "ManualCorrection" || sensorAttribute.Name == "ManualOffset")
        sensorAttribute.Delete();
    }
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
      ApplicationID = ResistiveBridgeMeter.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(ResistiveBridgeMeter left, ResistiveBridgeMeter right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(ResistiveBridgeMeter left, ResistiveBridgeMeter right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(ResistiveBridgeMeter left, ResistiveBridgeMeter right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(ResistiveBridgeMeter left, ResistiveBridgeMeter right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(ResistiveBridgeMeter left, ResistiveBridgeMeter right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(ResistiveBridgeMeter left, ResistiveBridgeMeter right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is ResistiveBridgeMeter && this.Equals((MonnitApplicationBase) (obj as ResistiveBridgeMeter));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is ResistiveBridgeMeter && this.PlotValue.ToInt() == (right as ResistiveBridgeMeter).PlotValue.ToInt();
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is ResistiveBridgeMeter && this.PlotValue.ToInt() != (right as ResistiveBridgeMeter).PlotValue.ToInt();
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is ResistiveBridgeMeter && this.PlotValue.ToInt() < (right as ResistiveBridgeMeter).PlotValue.ToInt();
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is ResistiveBridgeMeter && this.PlotValue.ToInt() <= (right as ResistiveBridgeMeter).PlotValue.ToInt();
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is ResistiveBridgeMeter && this.PlotValue.ToInt() > (right as ResistiveBridgeMeter).PlotValue.ToInt();
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is ResistiveBridgeMeter && this.PlotValue.ToInt() >= (right as ResistiveBridgeMeter).PlotValue.ToInt();
  }
}
