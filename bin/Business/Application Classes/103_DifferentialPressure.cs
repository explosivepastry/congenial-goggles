// Decompiled with JetBrains decompiler
// Type: Monnit.DifferentialPressure
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

public class DifferentialPressure : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _ShowFullDataValue;
  private SensorAttribute _TransformValue;
  private SensorAttribute _SavedValue;
  private SensorAttribute _Label;
  private SensorAttribute _CustomLabel;
  private SensorAttribute _LowValue;
  internal SensorAttribute _HighValue;
  private SensorAttribute _CorF;

  public static long MonnitApplicationID => 103;

  public static string ApplicationName => "Differential Pressure";

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.DifferentialPressureData, nameof (DifferentialPressure), this.Pressure),
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temperature)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[2]
    {
      this.PlotValue,
      (object) this.PlotTemperatureValue
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[2]
    {
      DifferentialPressure.GetLabel(sensorID),
      DifferentialPressure.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    });
  }

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => DifferentialPressure.MonnitApplicationID;

  public double Pressure { get; set; }

  public double Temperature { get; set; }

  public override string Serialize()
  {
    double num = this.Pressure;
    string str1 = num.ToString();
    num = this.Temperature;
    string str2 = num.ToString();
    return $"{str1}|{str2}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) DifferentialPressure.Deserialize(version, serialized);
  }

  public override bool IsValid => true;

  public override string NotificationString
  {
    get
    {
      string str1 = this.Label.Value == "inAq" ? "inH2O" : this.Label.Value;
      if (!(this.ShowFullDataValue.Value.ToLower() == "true"))
        return $"{this.PlotValue.ToDouble()} {str1}";
      double num = this.CorF == null || !(this.CorF.Value == "C") ? this.Temperature * 9.0 / 5.0 + 32.0 : this.Temperature;
      string str2 = this.CorF == null || !(this.CorF.Value == "C") ? "F" : "C";
      return $"{this.PlotValue.ToDouble()} {str1}, Temperature: {num} {str2}";
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
      else if (this.TransformValue != null && this.TransformValue.Value != "0")
        return (object) Math.Round(this.Pressure * this.TransformValue.Value.ToDouble(), 4);
      return (object) this.Pressure;
    }
  }

  public double PlotTemperatureValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? this.Temperature : this.Temperature.ToFahrenheit();
    }
  }

  public static DifferentialPressure Deserialize(string version, string serialized)
  {
    DifferentialPressure differentialPressure = new DifferentialPressure();
    if (string.IsNullOrEmpty(serialized))
    {
      differentialPressure.Pressure = 0.0;
      differentialPressure.Temperature = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      differentialPressure.Pressure = strArray[0].ToDouble();
      differentialPressure.Temperature = strArray.Length <= 1 ? strArray[0].ToDouble() : strArray[1].ToDouble();
    }
    return differentialPressure;
  }

  public static DifferentialPressure Create(byte[] sdm, int startIndex)
  {
    return new DifferentialPressure()
    {
      Pressure = BitConverter.ToInt16(sdm, startIndex).ToDouble() / 10.0,
      Temperature = BitConverter.ToInt16(sdm, startIndex + 2).ToDouble() / 10.0
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
    NameValueCollection returnData)
  {
    if (!string.IsNullOrEmpty(collection["label"]))
    {
      long savedValue = DifferentialPressureBase.GetDefaults(new Version(sensor.FirmwareVersion), sensor.GenerationType)["DefaultMaximumThreshold"].ToLong() / 10L;
      switch (collection["label"])
      {
        case "Custom":
          DifferentialPressure.SetLowValue(sensor.SensorID, collection["lowValue"].ToDouble());
          DifferentialPressure.SetHighValue(sensor.SensorID, collection["highValue"].ToDouble());
          DifferentialPressure.SetLabel(sensor.SensorID, collection["label"]);
          DifferentialPressure.SetCustomLabel(sensor.SensorID, collection["customLabel"]);
          DifferentialPressure.SetDecimalTrunkValue(sensor.SensorID, collection["decimalTrunkValue"].ToDouble());
          DifferentialPressure.SetSavedValue(sensor.SensorID, (double) savedValue);
          break;
        case "inAq":
          DifferentialPressure.SetLabel(sensor.SensorID, "InH2O");
          DifferentialPressure.SetTransform(sensor.SensorID, 0.0040185981);
          DifferentialPressure.SetSavedValue(sensor.SensorID, (double) savedValue);
          break;
        case "inHg":
          DifferentialPressure.SetLabel(sensor.SensorID, "inHg");
          DifferentialPressure.SetTransform(sensor.SensorID, 0.000296134);
          DifferentialPressure.SetSavedValue(sensor.SensorID, (double) savedValue);
          break;
        case "mmHg":
          DifferentialPressure.SetLabel(sensor.SensorID, "mmHg");
          DifferentialPressure.SetTransform(sensor.SensorID, 0.0075006376);
          DifferentialPressure.SetSavedValue(sensor.SensorID, (double) savedValue);
          break;
        case "mmwc":
          DifferentialPressure.SetLabel(sensor.SensorID, "mmwc");
          DifferentialPressure.SetTransform(sensor.SensorID, 0.1019744289);
          DifferentialPressure.SetSavedValue(sensor.SensorID, (double) savedValue);
          break;
        case "psi":
          DifferentialPressure.SetLabel(sensor.SensorID, "psi");
          DifferentialPressure.SetTransform(sensor.SensorID, 0.0001450377);
          DifferentialPressure.SetSavedValue(sensor.SensorID, (double) savedValue);
          break;
        case "torr":
          DifferentialPressure.SetLabel(sensor.SensorID, "torr");
          DifferentialPressure.SetTransform(sensor.SensorID, 0.0075006168);
          DifferentialPressure.SetSavedValue(sensor.SensorID, (double) savedValue);
          break;
        default:
          DifferentialPressure.SetLabel(sensor.SensorID, "Pascal");
          DifferentialPressure.SetTransform(sensor.SensorID, 1.0);
          DifferentialPressure.SetSavedValue(sensor.SensorID, (double) savedValue);
          break;
      }
    }
    if (collection["TempScale"] == "on")
    {
      returnData["TempScale"] = "F";
      DifferentialPressure.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      returnData["TempScale"] = "C";
      DifferentialPressure.MakeCelsius(sensor.SensorID);
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
      ApplicationID = DifferentialPressure.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    string label = DifferentialPressure.GetLabel(sensor.SensorID);
    double result = 0.0;
    double savedValue = DifferentialPressure.GetSavedValue(sensor.SensorID);
    double transform = DifferentialPressure.GetTransform(sensor.SensorID);
    double num1 = 0.0;
    double num2 = 0.0;
    if (label.ToLower() == "custom")
    {
      DifferentialPressure differentialPressure = new DifferentialPressure();
      differentialPressure.SetSensorAttributes(sensor.SensorID);
      num1 = differentialPressure.LowValue.Value.ToDouble();
      num2 = differentialPressure.HighValue(sensor.DefaultValue<long>("DefaultMaximumThreshold").ToInt()).Value.ToDouble();
    }
    if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]) && double.TryParse(collection["Hysteresis_Manual"], out result))
    {
      if (label.ToLower() != "custom")
      {
        double num3 = result / transform;
        sensor.Hysteresis = (num3 * 10.0).ToLong();
      }
      else
      {
        double num4 = Math.Abs((num2 - num1) / 500.0);
        double num5 = collection["Hysteresis_Manual"].ToDouble();
        sensor.Hysteresis = num4 == 0.0 ? sensor.DefaultValue<long>("DefaultHysteresis") : (num5 / num4 * 10.0).ToLong();
      }
    }
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) && double.TryParse(collection["MinimumThreshold_Manual"], out result))
    {
      if (label.ToLower() != "custom")
      {
        double num6 = result / transform;
        sensor.MinimumThreshold = (num6 * 10.0).ToLong();
        if (sensor.MinimumThreshold < -5000L)
          sensor.MinimumThreshold = -5000L;
        if (sensor.MinimumThreshold > 5000L)
          sensor.MinimumThreshold = 5000L;
      }
      else
        sensor.MinimumThreshold = result >= num1 ? (result <= num2 ? (result.LinearInterpolation(num1, 0.0, num2, savedValue) * 10.0).ToLong() : (num2.LinearInterpolation(num1, 0.0, num2, savedValue) * 10.0).ToLong()) : (num1.LinearInterpolation(num1, 0.0, num2, savedValue) * 10.0).ToLong();
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) && double.TryParse(collection["MaximumThreshold_Manual"], out result))
    {
      if (label.ToLower() != "custom")
      {
        double num7 = result / transform;
        sensor.MaximumThreshold = (num7 * 10.0).ToLong();
        if (sensor.MaximumThreshold < -5000L)
          sensor.MaximumThreshold = -5000L;
        if ((double) sensor.MaximumThreshold > savedValue / transform * 10.0)
          sensor.MaximumThreshold = (savedValue / transform * 10.0).ToLong();
        if (sensor.MinimumThreshold > sensor.MaximumThreshold)
          sensor.MaximumThreshold = sensor.MinimumThreshold;
      }
      else
      {
        sensor.MaximumThreshold = result >= num1 ? (result <= num2 ? (result.LinearInterpolation(num1, 0.0, num2, savedValue) * 10.0).ToLong() : (num2.LinearInterpolation(num1, 0.0, num2, savedValue) * 10.0).ToLong()) : (num1.LinearInterpolation(num1, 0.0, num2, savedValue) * 10.0).ToLong();
        if (sensor.MinimumThreshold > sensor.MaximumThreshold)
          sensor.MaximumThreshold = sensor.MinimumThreshold;
      }
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      bool showFullData = collection["FullNotiString"].ToString() == "1" || collection["FullNotiString"].ToString().ToLower() == "true";
      DifferentialPressure.SetShowFullDataValue(sensor.SensorID, showFullData);
    }
    if (string.IsNullOrEmpty(collection["BasicThreshold"]) || string.IsNullOrEmpty(collection["BasicThresholdDirection"]) || !(collection["BasicThresholdDirection"] != "-1"))
      return;
    double num8 = collection["BasicThreshold"].ToDouble() / transform;
    if (collection["BasicThresholdDirection"].ToInt() == 0)
    {
      sensor.MinimumThreshold = (num8 * 10.0).ToLong();
      sensor.MaximumThreshold = savedValue.ToLong();
    }
    else if (collection["BasicThresholdDirection"].ToInt() == 1)
    {
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      sensor.MaximumThreshold = (num8 * 10.0).ToLong();
    }
    sensor.MeasurementsPerTransmission = 6;
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if ((sensor.Calibration1 & 32768L /*0x8000*/) == 32768L /*0x8000*/)
      numArrayList.Add(DifferentialPressureBase.getCalibrationValue(sensor.SensorID, 0.0));
    else if ((sensor.Calibration2 & 32768L /*0x8000*/) == 32768L /*0x8000*/)
      numArrayList.Add(DifferentialPressureBase.getCalibrationValue(sensor.SensorID, (double) (sensor.Calibration2 & (long) short.MaxValue)));
    numArrayList.Add(sensor.ReadProfileConfig(29));
    return numArrayList;
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : ((double) sensor.Hysteresis / 10.0 * DifferentialPressure.GetTransform(sensor.SensorID)).ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : ((double) sensor.MinimumThreshold / 10.0 * DifferentialPressure.GetTransform(sensor.SensorID)).ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : ((double) sensor.MaximumThreshold / 10.0 * DifferentialPressure.GetTransform(sensor.SensorID)).ToString();
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
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
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
      if (sensorAttribute.Name == "ShowFullDataValue")
        this._ShowFullDataValue = sensorAttribute;
    }
  }

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

  public static string GetLabel(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        return sensorAttribute.Value;
    }
    return "Pascal";
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
    return 500.0;
  }

  public static void SetSavedValue(long sensorID, double savedValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "SavedValue")
      {
        sensorAttribute.Value = savedValue > 0.0 ? savedValue.ToString() : "500";
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "SavedValue",
        Value = (savedValue > 0.0 ? savedValue.ToString() : "500"),
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
    return 500.ToDouble();
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
    return "Pascal";
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
        Value = defaultValue.ToString()
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
          Value = "Pascal"
        };
      return this._Label;
    }
  }

  public SensorAttribute CustomLabel
  {
    get
    {
      if (this._Label == null || !(this._Label.Value.ToLower() == "custom"))
        return this._Label;
      if (this._CustomLabel == null)
        this._CustomLabel = new SensorAttribute()
        {
          Value = "Pascal",
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

  public SensorAttribute HighValue(int defaultValue)
  {
    if (this._HighValue == null)
      this._HighValue = new SensorAttribute()
      {
        Value = defaultValue.ToString(),
        Name = nameof (HighValue)
      };
    return this._HighValue;
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

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(DifferentialPressure left, DifferentialPressure right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(DifferentialPressure left, DifferentialPressure right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(DifferentialPressure left, DifferentialPressure right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(DifferentialPressure left, DifferentialPressure right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(DifferentialPressure left, DifferentialPressure right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(DifferentialPressure left, DifferentialPressure right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is DifferentialPressure && this.Equals((MonnitApplicationBase) (obj as DifferentialPressure));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is DifferentialPressure && this.Pressure == (right as DifferentialPressure).Pressure;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is DifferentialPressure && this.Pressure != (right as DifferentialPressure).Pressure;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is DifferentialPressure && this.Pressure < (right as DifferentialPressure).Pressure;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is DifferentialPressure && this.Pressure <= (right as DifferentialPressure).Pressure;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is DifferentialPressure && this.Pressure > (right as DifferentialPressure).Pressure;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is DifferentialPressure && this.Pressure >= (right as DifferentialPressure).Pressure;
  }

  public static long DefaultMinThreshold => -500;

  public static long DefaultMaxThreshold => 500;

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }

  public static Dictionary<string, string> MeasurementScaleValue()
  {
    return new Dictionary<string, string>()
    {
      {
        "Pascal",
        "Pascal"
      },
      {
        "Torr",
        "Torr"
      },
      {
        "psi",
        "psi"
      },
      {
        "inH20",
        "inH20"
      },
      {
        "inHg",
        "inHg"
      },
      {
        "mmHg",
        "mmHg"
      },
      {
        "mm Water Column",
        "mm Water Column"
      }
    };
  }

  public static Dictionary<string, string> NotificationScaleValues()
  {
    return DifferentialPressure.MeasurementScaleValue();
  }

  public new static NotificationScaleInfoModel GetScalingInfo(Sensor sens)
  {
    NotificationScaleInfoModel scalingInfo = new NotificationScaleInfoModel();
    DifferentialPressure differentialPressure = new DifferentialPressure();
    differentialPressure.SetSensorAttributes(sens.SensorID);
    scalingInfo.Label = differentialPressure.Label.Value.ToStringSafe();
    if (scalingInfo.Label == "Custom")
    {
      scalingInfo.CustomLabel = differentialPressure.CustomLabel.Value.ToStringSafe();
      scalingInfo.profileLow = differentialPressure.LowValue.Value.ToDouble();
      scalingInfo.profileHigh = differentialPressure.HighValue((sens.DefaultValue<long>("DefaultMaximumThreshold") / 10L).ToInt()).Value.ToDouble();
      scalingInfo.baseLow = 0.0;
      scalingInfo.baseHigh = 500.0;
    }
    else
      scalingInfo.transformValue = differentialPressure.TransformValue.Value.ToDouble();
    return scalingInfo;
  }
}
