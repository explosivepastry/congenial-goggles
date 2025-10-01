// Decompiled with JetBrains decompiler
// Type: Monnit.PressureNPSI
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

public class PressureNPSI : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _QueueID;
  private SensorAttribute _CalibrationValues;
  private SensorAttribute _TransformValue;
  private SensorAttribute _SavedValue;
  private SensorAttribute _Label;
  private SensorAttribute _CustomLabel;
  private SensorAttribute _LowValue;
  internal SensorAttribute _HighValue;
  private double _Pressure = double.MaxValue;

  public static long MonnitApplicationID => 83;

  public static string ApplicationName => "Generic Pressure";

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.Pressure, "Pressure", (DataTypeBase) new Monnit.Application_Classes.DataTypeClasses.Pressure(this.Pressure))
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[1]
    {
      this.PlotValue
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[1]
    {
      PressureNPSI.GetLabel(sensorID)
    });
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CalibrationValues")
        this._CalibrationValues = sensorAttribute;
      if (sensorAttribute.Name == "QueueID")
        this._QueueID = sensorAttribute;
      if (sensorAttribute.Name == "transformValue")
        this._TransformValue = sensorAttribute;
      if (sensorAttribute.Name == "Label")
        this._Label = sensorAttribute;
      if (sensorAttribute.Name == "LowValue")
        this._LowValue = sensorAttribute;
      if (sensorAttribute.Name == "HighValue")
        this._HighValue = sensorAttribute;
      if (sensorAttribute.Name == "CustomLabel")
        this._CustomLabel = sensorAttribute;
      if (sensorAttribute.Name == "SavedValue")
        this._SavedValue = sensorAttribute;
    }
  }

  public SensorAttribute QueueID => this._QueueID;

  public SensorAttribute CalibrationValues => this._CalibrationValues;

  public SensorAttribute TransformValue
  {
    get
    {
      if (this._TransformValue == null)
        this._TransformValue = new SensorAttribute()
        {
          Value = "1"
        };
      return this._TransformValue;
    }
  }

  public SensorAttribute SavedValue(int defaultValue)
  {
    if (this._SavedValue == null)
      this._SavedValue = new SensorAttribute()
      {
        Value = defaultValue.ToString(),
        Name = nameof (SavedValue)
      };
    return this._SavedValue;
  }

  public SensorAttribute Label
  {
    get
    {
      if (this._Label == null)
        this._Label = new SensorAttribute()
        {
          Value = "PSI",
          Name = nameof (Label)
        };
      return this._Label;
    }
  }

  public SensorAttribute CustomLabel
  {
    get
    {
      if (this._CustomLabel == null)
        this._CustomLabel = new SensorAttribute()
        {
          Value = "PSI",
          Name = nameof (CustomLabel)
        };
      return this._CustomLabel;
    }
  }

  public SensorAttribute LowValue
  {
    get
    {
      if (this._LowValue == null)
        this._LowValue = new SensorAttribute()
        {
          Value = "0",
          Name = nameof (LowValue)
        };
      return this._LowValue;
    }
  }

  public SensorAttribute HighValue(long defaultValue)
  {
    if (this._HighValue == null)
      this._HighValue = new SensorAttribute()
      {
        Value = defaultValue.ToString(),
        Name = nameof (HighValue)
      };
    return this._HighValue;
  }

  public static double GetLowValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "LowValue")
        return sensorAttribute.Value.ToDouble();
    }
    return 0.0;
  }

  public static void SetLowValue(long sensorID, double lowValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "LowValue")
      {
        sensorAttribute.Value = lowValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "LowValue",
        Value = lowValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static double GetHighValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HighValue")
        return sensorAttribute.Value.ToDouble();
    }
    Sensor sensor = Sensor.Load(sensorID);
    return sensor == null ? double.MaxValue : (double) sensor.DefaultValue<long>("DefaultMaximumThreshold") / 10.0;
  }

  public static void SetHighValue(long sensorID, double highValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HighValue")
      {
        sensorAttribute.Value = highValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "HighValue",
        Value = highValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetCustomLabel(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CustomLabel")
        return sensorAttribute.Value;
    }
    return "PSI";
  }

  public static void SetCustomLabel(long sensorID, string customLabel)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CustomLabel")
      {
        sensorAttribute.Value = customLabel;
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "CustomLabel",
        Value = customLabel,
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static int GetDecimalTrunkValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "decimalTrunkValue")
        return sensorAttribute.Value.ToInt();
    }
    return 0;
  }

  public static void SetDecimalTrunkValue(long sensorID, double decimalTrunkValue)
  {
    bool flag = false;
    if (decimalTrunkValue < 0.0)
      decimalTrunkValue = 0.0;
    else if (decimalTrunkValue > 5.0)
      decimalTrunkValue = 5.0;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == nameof (decimalTrunkValue))
      {
        sensorAttribute.Value = decimalTrunkValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = nameof (decimalTrunkValue),
        Value = decimalTrunkValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

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
    int queueID)
  {
    string str = $"{actual.ToString()}|{observed.ToString()}|{queueID.ToString()}";
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

  public static string GetLabel(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        return sensorAttribute.Value;
    }
    return "PSI";
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

  public static double GetSavedValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "SavedValue")
        return sensorAttribute.Value.ToDouble();
    }
    Sensor sensor = Sensor.Load(sensorID);
    return sensor == null ? double.MaxValue : (double) sensor.DefaultValue<long>("DefaultMaximumThreshold") / 10.0;
  }

  public static void SetSavedValue(long sensorID, double savedValue)
  {
    double num = double.MaxValue;
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor != null)
      num = (double) sensor.DefaultValue<long>("DefaultMaximumThreshold") / 10.0;
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "SavedValue")
      {
        sensorAttribute.Value = savedValue > 0.0 ? savedValue.ToString() : num.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "SavedValue",
        Value = (savedValue > 0.0 ? savedValue.ToString() : num.ToString()),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static double GetTransform(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "transformValue")
        return sensorAttribute.Value.ToDouble();
    }
    return 1.0;
  }

  public static void SetTransform(long sensorID, double transformValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == nameof (transformValue))
      {
        sensorAttribute.Value = transformValue > 0.0 ? transformValue.ToString() : "1";
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = nameof (transformValue),
        Value = (transformValue > 0.0 ? transformValue.ToString() : "1"),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => PressureNPSI.MonnitApplicationID;

  public double Pressure
  {
    get => this._Pressure;
    set => this._Pressure = value;
  }

  public override string Serialize() => this.Pressure.ToString();

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) PressureNPSI.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      return this.Label.Value.ToLower() != "custom" ? $"{this.PlotValue.ToString()} {this.Label.Value}" : $"{Math.Round(this.PlotValue.ToDouble(), PressureNPSI.GetDecimalTrunkValue(this.Label.SensorID))} {this.CustomLabel.Value}";
    }
  }

  public override object PlotValue
  {
    get
    {
      if (this.Label == null)
      {
        if (this.TransformValue != null && this.TransformValue.Value != "0")
          return (object) (this.Pressure * this.TransformValue.Value.ToDouble());
      }
      else
      {
        if (!(this.Label.Value.ToLower() != "custom"))
          return (object) this.Pressure.LinearInterpolation((double) PressureNPSI.DefaultMinThreshold / 10.0, this.LowValue.Value.ToDouble(), (double) PressureNPSI.DefaultMaxThreshold / 10.0, this.HighValue(PressureNPSI.DefaultMaxThreshold / 10L).Value.ToDouble());
        if (this.TransformValue != null && this.TransformValue.Value != "0")
          return (object) (this.Pressure * this.TransformValue.Value.ToDouble());
      }
      return (object) this.Pressure;
    }
  }

  public static PressureNPSI Deserialize(string version, string serialized)
  {
    return new PressureNPSI()
    {
      Pressure = serialized.ToDouble()
    };
  }

  public static PressureNPSI Create(byte[] sdm, int startIndex)
  {
    return new PressureNPSI()
    {
      Pressure = BitConverter.ToUInt16(sdm, startIndex).ToDouble() / 10.0
    };
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

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (string.IsNullOrWhiteSpace(collection["MaxCapcity"]) || !sensor.CanUpdate)
      return;
    double result = double.MinValue;
    if (double.TryParse(collection["MaxCapcity"], out result))
    {
      switch (collection["label"])
      {
        case "atm":
          PressureNPSI.SetLabel(sensor.SensorID, "atm");
          PressureNPSI.SetTransform(sensor.SensorID, 0.068);
          PressureNPSI.SetSavedValue(sensor.SensorID, result);
          break;
        case "bar":
          PressureNPSI.SetLabel(sensor.SensorID, "bar");
          PressureNPSI.SetTransform(sensor.SensorID, 0.0689);
          PressureNPSI.SetSavedValue(sensor.SensorID, result);
          break;
        case "kPA":
          PressureNPSI.SetLabel(sensor.SensorID, "kPA");
          PressureNPSI.SetTransform(sensor.SensorID, 6.89);
          PressureNPSI.SetSavedValue(sensor.SensorID, result);
          break;
        case "Torr":
          PressureNPSI.SetLabel(sensor.SensorID, "Torr");
          PressureNPSI.SetTransform(sensor.SensorID, 51.71);
          PressureNPSI.SetSavedValue(sensor.SensorID, result);
          break;
        case "Custom":
          PressureNPSI.SetLowValue(sensor.SensorID, collection["lowValue"].ToDouble());
          PressureNPSI.SetHighValue(sensor.SensorID, collection["highValue"].ToDouble());
          PressureNPSI.SetLabel(sensor.SensorID, collection["label"]);
          PressureNPSI.SetCustomLabel(sensor.SensorID, collection["customLabel"]);
          PressureNPSI.SetDecimalTrunkValue(sensor.SensorID, collection["decimalTrunkValue"].ToDouble());
          PressureNPSI.SetSavedValue(sensor.SensorID, result);
          break;
        default:
          PressureNPSI.SetLabel(sensor.SensorID, "PSI");
          PressureNPSI.SetTransform(sensor.SensorID, 1.0);
          PressureNPSI.SetSavedValue(sensor.SensorID, result);
          break;
      }
      result /= 1.0;
      result = Math.Round(result * 10.0);
      sensor.Calibration4 = result.ToLong();
      sensor.Save();
    }
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = PressureNPSI.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    string label = PressureNPSI.GetLabel(sensor.SensorID);
    double result1 = 0.0;
    double savedValue = PressureNPSI.GetSavedValue(sensor.SensorID);
    double num1 = PressureNPSI.GetTransform(sensor.SensorID);
    double lowX = 0.0;
    double highX = 0.0;
    int num2 = PressureNPSI.GetDecimalTrunkValue(sensor.SensorID).ToInt();
    if (label.ToLower() == "custom")
    {
      PressureNPSI pressureNpsi = new PressureNPSI();
      pressureNpsi.SetSensorAttributes(sensor.SensorID);
      lowX = pressureNpsi.LowValue.Value.ToDouble();
      highX = pressureNpsi.HighValue((long) sensor.DefaultValue<long>("DefaultCalibration4").ToInt()).Value.ToDouble();
    }
    if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result1)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result1)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result1)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]) && double.TryParse(collection["Hysteresis_Manual"], out result1))
    {
      if (label.ToLower() != "custom")
      {
        double num3 = result1 / num1;
        sensor.Hysteresis = (num3 * 10.0).ToLong();
      }
      else
      {
        double num4 = Math.Abs((highX - lowX) / (double) (sensor.DefaultValue<long>("DefaultMaximumThreshold") - sensor.DefaultValue<long>("DefaultMinimumThreshold")));
        double num5 = collection["Hysteresis_Manual"].ToDouble();
        if (num4 != 0.0)
        {
          double o = Math.Round(num5 / num4 * Math.Pow(10.0, (double) (num2 + 1)));
          sensor.Hysteresis = o.ToLong();
        }
        else
          sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
      }
    }
    double highY = savedValue / num1;
    double num6 = 0.0;
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) && double.TryParse(collection["MinimumThreshold_Manual"], out result1))
    {
      if (label == "kPA" || label == "Torr")
        num1 = 1.0;
      num6 = !(label.ToLower() != "custom") ? result1.LinearInterpolation(lowX, 0.0, highX, highY) : result1 / num1;
      if (num6 < 0.0)
        num6 = 0.0;
      if ((label == "PSI" || label == "atm" || label == "bar") && num6 > highY)
        num6 = highY;
      sensor.MinimumThreshold = (num6 * 10.0).ToLong();
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) && double.TryParse(collection["MaximumThreshold_Manual"], out result1))
    {
      if (label == "kPA" || label == "Torr")
        num1 = 1.0;
      double num7 = !(label.ToLower() != "custom") ? result1.LinearInterpolation(lowX, 0.0, highX, highY) : result1 / num1;
      if (num7 < 0.0)
        num7 = 0.0;
      if ((label == "PSI" || label == "atm" || label == "bar") && num7 > highY)
        num7 = highY;
      if (num6 > num7)
        num7 = num6;
      sensor.MaximumThreshold = (num7 * 10.0).ToLong();
    }
    int result2 = 0;
    if (!string.IsNullOrWhiteSpace(collection["delay"]) && int.TryParse(collection["delay"], out result2))
    {
      int num8 = result2;
      if (num8 < 0)
        num8 = 0;
      if (num8 > 60000)
        num8 = 60000;
      PressureNPSI.SetStabalizationDelay(sensor, num8);
    }
    if (string.IsNullOrEmpty(collection["BasicThreshold"]) || string.IsNullOrEmpty(collection["BasicThresholdDirection"]) || !(collection["BasicThresholdDirection"] != "-1"))
      return;
    double num9 = collection["BasicThreshold"].ToDouble() / num1;
    if (collection["BasicThresholdDirection"].ToInt() == 0)
    {
      sensor.MinimumThreshold = (num9 * 10.0).ToLong();
      sensor.MaximumThreshold = savedValue.ToLong();
    }
    else if (collection["BasicThresholdDirection"].ToInt() == 1)
    {
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      sensor.MaximumThreshold = (num9 * 10.0).ToLong();
    }
    sensor.MeasurementsPerTransmission = 6;
  }

  public static void SetStabalizationDelay(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration3) & 4294901760U;
    sensor.Calibration3 = (long) (num | (uint) (value & (int) ushort.MaxValue));
  }

  public static int GetStabalizationDelay(Sensor sensor)
  {
    return (int) Convert.ToUInt32(sensor.Calibration3) & (int) ushort.MaxValue;
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (string.IsNullOrWhiteSpace(collection["actual"]))
      return;
    double num1 = collection["actual"].ToDouble();
    double num2 = collection["observed"].ToDouble();
    double actual;
    double observed;
    switch (PressureNPSI.GetLabel(sensor.SensorID))
    {
      case "atm":
        actual = Math.Round(num1 / 0.068 * 10.0);
        observed = Math.Round(num2 / 0.068 * 10.0);
        break;
      case "bar":
        actual = Math.Round(num1 / 0.0689 * 10.0);
        observed = Math.Round(num2 / 0.0689 * 10.0);
        break;
      case "kPA":
        actual = Math.Round(num1 / 6.89 * 10.0);
        observed = Math.Round(num2 / 6.89 * 10.0);
        break;
      case "Torr":
        actual = Math.Round(num1 / 51.71 * 10.0);
        observed = Math.Round(num2 / 51.71 * 10.0);
        break;
      default:
        actual = Math.Round(num1 / 1.0 * 10.0);
        observed = Math.Round(num2 / 1.0 * 10.0);
        break;
    }
    if (new Version(sensor.FirmwareVersion) <= new Version("2.4.0.6"))
      actual = (double) ((long) ((double) (sensor.Calibration2 - sensor.Calibration1) * (observed / actual)) + sensor.Calibration1);
    PressureNPSI.SetQueueID(sensor.SensorID);
    PressureNPSI.SetCalibrationValues(sensor.SensorID, actual, observed, PressureNPSI.GetQueueID(sensor.SensorID));
    sensor.PendingActionControlCommand = true;
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

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      string[] strArray = PressureNPSI.GetCalibrationValues(sensor.SensorID).Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      numArrayList.Add(PressureNPSIBase.CalibrateFrame(sensor.SensorID, strArray[0].ToInt(), strArray[1].ToInt(), strArray[2].ToInt(), new Version(sensor.FirmwareVersion)));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : ((double) sensor.Hysteresis / 100.0 * PressureNPSI.GetTransform(sensor.SensorID)).ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : ((double) sensor.MinimumThreshold / 10.0 * PressureNPSI.GetTransform(sensor.SensorID)).ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : ((double) sensor.MaximumThreshold / 10.0 * PressureNPSI.GetTransform(sensor.SensorID)).ToString();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(PressureNPSI left, PressureNPSI right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(PressureNPSI left, PressureNPSI right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(PressureNPSI left, PressureNPSI right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(PressureNPSI left, PressureNPSI right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(PressureNPSI left, PressureNPSI right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(PressureNPSI left, PressureNPSI right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is PressureNPSI && this.Equals((MonnitApplicationBase) (obj as PressureNPSI));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is PressureNPSI && this.Pressure == (right as PressureNPSI).Pressure;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is PressureNPSI && this.Pressure != (right as PressureNPSI).Pressure;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is PressureNPSI && this.Pressure < (right as PressureNPSI).Pressure;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is PressureNPSI && this.Pressure <= (right as PressureNPSI).Pressure;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is PressureNPSI && this.Pressure > (right as PressureNPSI).Pressure;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is PressureNPSI && this.Pressure >= (right as PressureNPSI).Pressure;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 500;

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }

  public static Dictionary<string, string> MeasurementScaleValue()
  {
    return PressureNPSI.NotificationScaleValues();
  }

  public static Dictionary<string, string> NotificationScaleValues()
  {
    return new Dictionary<string, string>()
    {
      {
        "PSI",
        "PSI"
      },
      {
        "atm",
        "atm"
      },
      {
        "bar",
        "bar"
      },
      {
        "kPA",
        "kPA"
      },
      {
        "Torr",
        "Torr"
      },
      {
        "Custom",
        "Custom"
      }
    };
  }

  public new static NotificationScaleInfoModel GetScalingInfo(Sensor sens)
  {
    NotificationScaleInfoModel scalingInfo = new NotificationScaleInfoModel();
    PressureNPSI pressureNpsi = new PressureNPSI();
    pressureNpsi.SetSensorAttributes(sens.SensorID);
    scalingInfo.Label = pressureNpsi.Label.Value.ToStringSafe();
    if (scalingInfo.Label == "Custom")
    {
      scalingInfo.CustomLabel = pressureNpsi.CustomLabel.Value.ToStringSafe();
      scalingInfo.profileLow = pressureNpsi.LowValue.Value.ToDouble();
      scalingInfo.profileHigh = pressureNpsi.HighValue((long) sens.DefaultValue<long>("DefaultCalibration4").ToInt()).Value.ToDouble();
      scalingInfo.baseLow = 0.0;
      scalingInfo.baseHigh = pressureNpsi.SavedValue(sens.DefaultValue<long>("DefaultCalibration4").ToInt()).Value.ToDouble();
    }
    else
    {
      scalingInfo.transformValue = pressureNpsi.TransformValue.Value.ToDouble();
      scalingInfo.profileHigh = scalingInfo.transformValue;
    }
    return scalingInfo;
  }
}
