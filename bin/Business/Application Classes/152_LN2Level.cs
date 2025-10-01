// Decompiled with JetBrains decompiler
// Type: Monnit.LN2Level
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

public class LN2Level : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _UsableSensorHeightCM;
  private SensorAttribute _UsableSensorHeightDisplay;
  private SensorAttribute _SavedValue;
  private SensorAttribute _Label;
  private SensorAttribute _CustomLabel;
  private SensorAttribute _LowValue;
  internal SensorAttribute _HighValue;

  public static long MonnitApplicationID => 152;

  public static string ApplicationName => "LN2 Level";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => LN2Level.MonnitApplicationID;

  public double Level { get; set; }

  public double Capacitance { get; set; }

  public override string Serialize() => $"{this.Level}|{this.Capacitance}|{this.stsState}";

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.Percentage, "Level", this.Level),
        new AppDatum(eDatumType.Capacitance, "Capacitance", this.Capacitance)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[2]
    {
      this.PlotValue,
      (object) this.Capacitance
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[2]
    {
      this.PlotLabel,
      "Capacitance"
    });
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) LN2Level.Deserialize(version, serialized);
  }

  public SensorAttribute UsableSensorHeightCM
  {
    get
    {
      if (this._UsableSensorHeightCM == null)
        this._UsableSensorHeightCM = new SensorAttribute()
        {
          Value = "950",
          Name = nameof (UsableSensorHeightCM)
        };
      return this._UsableSensorHeightCM;
    }
  }

  public SensorAttribute UsableSensorHeightDisplay
  {
    get
    {
      if (this._UsableSensorHeightDisplay == null)
        this._UsableSensorHeightDisplay = new SensorAttribute()
        {
          Value = "centimeters",
          Name = nameof (UsableSensorHeightDisplay)
        };
      return this._UsableSensorHeightDisplay;
    }
  }

  public SensorAttribute SavedValue
  {
    get
    {
      if (this._SavedValue == null)
        this._SavedValue = new SensorAttribute()
        {
          Value = "0",
          Name = nameof (SavedValue)
        };
      return this._SavedValue;
    }
  }

  public SensorAttribute Label
  {
    get
    {
      if (this._Label == null)
        this._Label = new SensorAttribute()
        {
          Value = "%",
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
          Value = "%",
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

  public SensorAttribute HighValue
  {
    get
    {
      if (this._HighValue == null)
        this._HighValue = new SensorAttribute()
        {
          Value = "100",
          Name = "LowValue"
        };
      return this._HighValue;
    }
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
    return 100.0;
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
    return "%";
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

  public static string GetLabel(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        return sensorAttribute.Value;
    }
    return "%";
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

  public static string GetUsableSensorHeightDisplay(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "usableSensorHeightDisplay")
        return sensorAttribute.Value;
    }
    return "centimeters";
  }

  public static int GetUsableSensorHeightCM(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "usableSensorHeightCM")
        return sensorAttribute.Value.ToInt();
    }
    return 950;
  }

  public static void SetUsableSensorHeightCM(
    long sensorID,
    int usableSensorHeightCM,
    string usableSensorHeightDisplay)
  {
    if (usableSensorHeightCM < 0 || usableSensorHeightCM > 950)
      usableSensorHeightCM = 950;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == nameof (usableSensorHeightCM))
      {
        usableSensorHeightDisplay = "centimeters";
        flag3 = usableSensorHeightCM != sensorAttribute.Value.ToInt();
        sensorAttribute.Value = usableSensorHeightCM.ToString();
        sensorAttribute.Save();
        flag1 = true;
      }
      if (sensorAttribute.Name == nameof (usableSensorHeightDisplay))
      {
        sensorAttribute.Value = usableSensorHeightDisplay;
        sensorAttribute.Save();
        flag2 = true;
      }
    }
    if (!flag1)
    {
      flag3 = true;
      new SensorAttribute()
      {
        Name = nameof (usableSensorHeightCM),
        SensorID = sensorID,
        Value = usableSensorHeightCM.ToString()
      }.Save();
    }
    if (!flag2)
      new SensorAttribute()
      {
        Name = nameof (usableSensorHeightDisplay),
        SensorID = sensorID,
        Value = usableSensorHeightDisplay
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
    if (!flag3)
      return;
    Sensor.Load(sensorID).Calibration2 = (long) (usableSensorHeightCM * 10);
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

  public int stsState { get; set; }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        this._Label = sensorAttribute;
      if (sensorAttribute.Name == "CustomLabel")
        this._CustomLabel = sensorAttribute;
      if (sensorAttribute.Name == "LowValue")
        this._LowValue = sensorAttribute;
      if (sensorAttribute.Name == "HighValue")
        this._HighValue = sensorAttribute;
      if (sensorAttribute.Name == "UsableSensorHeightCM")
        this._UsableSensorHeightCM = sensorAttribute;
      if (sensorAttribute.Name == "UsableSensorHeightDisplay")
        this._UsableSensorHeightDisplay = sensorAttribute;
      if (sensorAttribute.Name == "SavedValue")
        this._SavedValue = sensorAttribute;
    }
  }

  public override bool IsValid
  {
    get => this.stsState != 1 && this.stsState != 2 && this.stsState != 3 && this.stsState != 4;
  }

  public override string NotificationString
  {
    get
    {
      string str1 = string.Empty;
      string str2 = $"{(double) this.PlotValue} {this.PlotLabel}";
      if (this.stsState == 1)
        str1 = " Over Range";
      else if (this.stsState == 2 || this.stsState == 3)
        str1 = " Read Error";
      else if (this.stsState == 4)
        str1 = " Under Range";
      else if (this.stsState == 5)
        str1 = " Semaphore Error";
      else if (this.stsState == 7 && this.Level <= 0.0)
        str1 = " Less than 0%";
      else if (this.stsState == 7 && this.Level >= 100.0)
        str1 = " Greater than 100%";
      return $"{str2}{str1}";
    }
  }

  private string PlotLabel
  {
    get
    {
      string plotLabel;
      switch (this.Label.Value.ToLower())
      {
        case "height":
          plotLabel = !(this.CustomLabel.Value == "centimeters") ? "in" : "cm";
          break;
        case "volume":
          plotLabel = !(this.CustomLabel.Value == "liters") ? "gal" : "L";
          break;
        case "custom":
          plotLabel = this.CustomLabel.Value;
          break;
        default:
          plotLabel = "%";
          break;
      }
      return plotLabel;
    }
  }

  public override object PlotValue
  {
    get
    {
      double num1;
      switch (this.Label.Value.ToLower())
      {
        case "height":
          double num2 = this.SavedValue.Value.ToDouble();
          num1 = !(this.CustomLabel.Value == "centimeters") ? num2 / 2.54 * (this.Level / 100.0) : num2 * (this.Level / 100.0);
          break;
        case "volume":
          double num3 = this.SavedValue.Value.ToDouble();
          num1 = !(this.CustomLabel.Value == "liters") ? num3 / 3.785 * (this.Level / 100.0) : num3 * (this.Level / 100.0);
          break;
        case "custom":
          num1 = this.Level.LinearInterpolation(0.0, this.LowValue.Value.ToDouble(), 100.0, this.HighValue.Value.ToDouble());
          break;
        default:
          num1 = this.Level;
          break;
      }
      return (object) Math.Round(num1, 1);
    }
  }

  public static LN2Level Deserialize(string version, string serialized)
  {
    LN2Level ln2Level = new LN2Level();
    if (string.IsNullOrEmpty(serialized))
    {
      ln2Level.Level = 0.0;
      ln2Level.Capacitance = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      ln2Level.Level = strArray[0].ToDouble();
      if (strArray.Length > 1)
      {
        ln2Level.Capacitance = strArray[1].ToDouble();
        if (strArray.Length > 2)
          ln2Level.stsState = strArray[2].ToInt();
      }
      else
        ln2Level.Capacitance = strArray[0].ToDouble();
    }
    return ln2Level;
  }

  public static LN2Level Create(byte[] sdm, int startIndex)
  {
    int state = (int) sdm[startIndex - 1];
    byte[] numArray = new byte[sdm.Length - startIndex];
    Array.Copy((Array) sdm, startIndex, (Array) numArray, 0, sdm.Length - startIndex);
    LN2LevelBase ln2LevelBase = LN2LevelBase.Create(state, numArray);
    return new LN2Level()
    {
      Level = ln2LevelBase.Level,
      Capacitance = ln2LevelBase.Capacitance,
      stsState = state >> 4
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

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    LN2LevelBase.GetDefaults(new Version(sensor.FirmwareVersion), sensor.GenerationType);
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
      ApplicationID = LN2Level.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    string label = LN2Level.GetLabel(sensor.SensorID);
    double result = 0.0;
    double num = 0.0;
    double highX = 0.0;
    LN2Level.GetSavedValue(sensor.SensorID);
    if (label.ToLower() == "custom")
    {
      LN2Level ln2Level = new LN2Level();
      ln2Level.SetSensorAttributes(sensor.SensorID);
      num = ln2Level.LowValue.Value.ToDouble();
      highX = ln2Level.HighValue.Value.ToDouble();
    }
    List<SensorAttribute> source = SensorAttribute.LoadBySensorID(sensor.SensorID);
    if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (!string.IsNullOrEmpty(collection["tankHeightValue"]) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("tankHeightValue"))
    {
      SensorAttribute sensorAttribute = source.Where<SensorAttribute>((Func<SensorAttribute, bool>) (att => att.Name.ToLower() == "tankHeightValue")).FirstOrDefault<SensorAttribute>();
      if (double.TryParse(collection["tankHeightValue"], out result))
      {
        sensorAttribute.Value = result.ToString();
        sensorAttribute.Save();
      }
      else
        sensorAttribute.Delete();
    }
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      if (label.ToLower() != "custom")
      {
        sensor.MinimumThreshold = collection["MinimumThreshold_Manual"].ToLong() * 100L;
        if (sensor.MinimumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
          sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
        if (sensor.MinimumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
          sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      }
      else
        sensor.MinimumThreshold = (num.LinearInterpolation(num, 0.0, highX, 100.0) * 100.0).ToLong();
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      if (label.ToLower() != "custom")
      {
        sensor.MaximumThreshold = collection["MaximumThreshold_Manual"].ToLong() * 100L;
        if (sensor.MaximumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
          sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
        if (sensor.MaximumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
          sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      }
      else
        sensor.MaximumThreshold = (num.LinearInterpolation(num, 0.0, highX, 100.0) * 100.0).ToLong();
    }
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]) && double.TryParse(collection["Hysteresis_Manual"], out result))
    {
      if (label.ToLower() != "custom")
      {
        sensor.Hysteresis = collection["Hysteresis_Manual"].ToLong();
        if (sensor.Hysteresis < 0L)
          sensor.Hysteresis = 0L;
        if (sensor.Hysteresis > 5L)
          sensor.Hysteresis = 5L;
      }
      else
        sensor.Hysteresis = (num.LinearInterpolation(num, 0.0, highX, 100.0) * 100.0).ToLong();
    }
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }

  public static string HystForUI(Sensor sensor) => sensor.Hysteresis.ToDouble().ToString();

  public static string MinThreshForUI(Sensor sensor)
  {
    return (sensor.MinimumThreshold.ToDouble() / 100.0).ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return (sensor.MaximumThreshold.ToDouble() / 100.0).ToString();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(LN2Level left, LN2Level right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(LN2Level left, LN2Level right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(LN2Level left, LN2Level right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(LN2Level left, LN2Level right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(LN2Level left, LN2Level right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(LN2Level left, LN2Level right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is LN2Level && this.Equals((MonnitApplicationBase) (obj as LN2Level));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is LN2Level && this.Level == (right as LN2Level).Level;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is LN2Level && this.Level != (right as LN2Level).Level;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is LN2Level && this.Level < (right as LN2Level).Level;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is LN2Level && this.Level <= (right as LN2Level).Level;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is LN2Level && this.Level > (right as LN2Level).Level;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is LN2Level && this.Level >= (right as LN2Level).Level;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 10000;

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (string.IsNullOrEmpty(collection["label"]))
      return;
    double savedValue1 = (double) sensor.DefaultValue<long>("DefaultMaximumThreshold");
    LN2Level.SetLabel(sensor.SensorID, collection["label"]);
    switch (collection["label"])
    {
      case "Height":
        double savedValue2 = collection["tankHeightValue"].ToDouble();
        if (collection["heightLabel"] == "centimeters")
        {
          LN2Level.SetCustomLabel(sensor.SensorID, "centimeters");
          LN2Level.SetSavedValue(sensor.SensorID, savedValue2);
          break;
        }
        LN2Level.SetCustomLabel(sensor.SensorID, "inches");
        LN2Level.SetSavedValue(sensor.SensorID, savedValue2 * 2.54);
        break;
      case "Volume":
        double savedValue3 = collection["tankVolumeValue"].ToDouble();
        if (collection["volumeLabel"] == "liters")
        {
          LN2Level.SetCustomLabel(sensor.SensorID, "liters");
          LN2Level.SetSavedValue(sensor.SensorID, savedValue3);
          break;
        }
        LN2Level.SetCustomLabel(sensor.SensorID, "gallons");
        LN2Level.SetSavedValue(sensor.SensorID, savedValue3 * 3.785);
        break;
      case "Custom":
        LN2Level.SetLowValue(sensor.SensorID, collection["lowValue"].ToDouble());
        LN2Level.SetHighValue(sensor.SensorID, collection["highValue"].ToDouble());
        LN2Level.SetCustomLabel(sensor.SensorID, collection["customLabel"]);
        LN2Level.SetDecimalTrunkValue(sensor.SensorID, collection["decimalTrunkValue"].ToDouble());
        break;
      default:
        LN2Level.SetSavedValue(sensor.SensorID, savedValue1);
        break;
    }
    if (!string.IsNullOrEmpty(collection["tankHeightValue"]) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("tankHeightValue"))
    {
      double o = collection["tankHeightValue"].ToDouble();
      string usableSensorHeightDisplay = "centimeters";
      if (collection["heightLabel"] == "inches")
      {
        usableSensorHeightDisplay = "inches";
        o /= 2.54;
      }
      LN2Level.SetUsableSensorHeightCM(sensor.SensorID, o.ToInt(), usableSensorHeightDisplay);
    }
    sensor.Save();
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (collection["calType"] == "1")
    {
      int observed = collection["lastReading"].ToInt();
      double actual = collection["span"].ToDouble();
      if (actual > 120.0)
        actual = 120.0;
      if (actual < 25.0)
        actual = 25.0;
      LN2Level.SetCalibrationValues(sensor.SensorID, actual, observed);
      sensor.PendingActionControlCommand = true;
      sensor.Save();
    }
    else
    {
      if (!(collection["calType"] == "2"))
        return;
      sensor.Calibration1 = (long) collection["empty"].ToInt();
      sensor.Save();
    }
  }

  public static void SetCalibrationValues(long sensorID, double actual, int observed)
  {
    NonCachedAttribute nonCachedAttribute = NonCachedAttribute.LoadBySensorIDAndName(sensorID, "SpanCalibration");
    if (nonCachedAttribute == null)
    {
      nonCachedAttribute = new NonCachedAttribute();
      nonCachedAttribute.SensorID = sensorID;
      nonCachedAttribute.Name = "SpanCalibration";
    }
    nonCachedAttribute.Value1 = actual.ToString();
    nonCachedAttribute.Value2 = observed.ToString();
    nonCachedAttribute.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    NonCachedAttribute nonCachedAttribute = NonCachedAttribute.LoadBySensorIDAndName(sensor.SensorID, "SpanCalibration");
    List<byte[]> numArrayList = new List<byte[]>();
    if (nonCachedAttribute != null)
      numArrayList.Add(LN2LevelBase.CalibrateFrame(sensor.SensorID, nonCachedAttribute.Value1.ToDouble(), nonCachedAttribute.Value2.ToInt()));
    return numArrayList;
  }

  public new static bool ActionControlCommandIsUrgent(Sensor sensor, string version) => false;

  public new static void ClearPendingActionControlCommand(
    Sensor sensor,
    string version,
    int actionCommand,
    bool success,
    byte[] data)
  {
    NonCachedAttribute.LoadBySensorIDAndName(sensor.SensorID, "SpanCalibration")?.Delete();
    sensor.PendingActionControlCommand = false;
  }

  public static void VerifyNotificationValues(Notification notification, string scale)
  {
    double Fahrenheit = notification.CompareValue.ToDouble();
    if (notification.eDatumType == eDatumType.TemperatureData)
    {
      if (!string.IsNullOrEmpty(scale) && scale == "F")
        Fahrenheit = Fahrenheit.ToCelsius();
    }
    else
    {
      if (scale == "percentage")
        Fahrenheit = notification.CompareValue.ToDouble();
      if (scale == "inches")
        Fahrenheit = notification.CompareValue.ToDouble() * 0.3937;
      if (scale == "centimeters")
        Fahrenheit = notification.CompareValue.ToDouble();
      if (scale == "gallons")
        Fahrenheit = notification.CompareValue.ToDouble() * 0.264172;
      if (scale == "liters")
        Fahrenheit = notification.CompareValue.ToDouble();
      if (scale == "Custom")
      {
        LN2Level ln2Level = new LN2Level();
        ln2Level.SetSensorAttributes(notification.SensorID);
        double lowX = ln2Level.LowValue.Value.ToDouble();
        double highX = ln2Level.HighValue.Value.ToDouble();
        double highY = ln2Level.SavedValue.Value.ToDouble();
        Fahrenheit = notification.CompareValue.ToDouble().LinearInterpolation(lowX, 0.0, highX, highY);
      }
    }
    notification.CompareValue = Fahrenheit.ToString();
  }
}
