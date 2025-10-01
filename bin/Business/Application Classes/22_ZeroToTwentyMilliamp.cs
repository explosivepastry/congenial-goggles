// Decompiled with JetBrains decompiler
// Type: Monnit.ZeroToTwentyMilliamp
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

public class ZeroToTwentyMilliamp : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _LowValue;
  private SensorAttribute _HighValue;
  private SensorAttribute _Label;
  private SensorAttribute _showAdvCal;

  public static long MonnitApplicationID => 22;

  public static string ApplicationName => "Current Meter - 20mA";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => ZeroToTwentyMilliamp.MonnitApplicationID;

  public double Current { get; set; }

  public override string Serialize() => this.Current.ToString();

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.MilliAmps, "MilliAmps", this.Current)
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
      ZeroToTwentyMilliamp.GetLabel(sensorID)
    });
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) ZeroToTwentyMilliamp.Deserialize(version, serialized);
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

  public static double Get4maValue(long sensorID)
  {
    double highValue = ZeroToTwentyMilliamp.GetHighValue(sensorID);
    return 4.0.LinearInterpolation(0.0, ZeroToTwentyMilliamp.GetLowValue(sensorID), 20.0, highValue);
  }

  public static void Set4maValue(long sensorID, double low4MaValue, double highValue)
  {
    ZeroToTwentyMilliamp.SetLowValue(sensorID, 0.0.LinearInterpolation(4.0, low4MaValue, 20.0, highValue));
  }

  public static double GetHighValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HighValue")
        return sensorAttribute.Value.ToDouble();
    }
    return 20.0;
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

  public static string GetLabel(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        return sensorAttribute.Value;
    }
    return "mA";
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

  public static void SetShowAdvCal(long sensorID, string showAdvCal)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == nameof (showAdvCal))
      {
        sensorAttribute.Value = showAdvCal;
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = nameof (showAdvCal),
        Value = showAdvCal,
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute LowValue
  {
    get
    {
      if (this._LowValue == null)
        this._LowValue = new SensorAttribute()
        {
          Value = "0"
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
          Value = "20"
        };
      return this._HighValue;
    }
  }

  public SensorAttribute Label
  {
    get
    {
      if (this._Label == null)
        this._Label = new SensorAttribute() { Value = "mA" };
      return this._Label;
    }
  }

  public static bool ShowAdvCal(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "showAdvCal" && sensorAttribute.Value == "true")
        return true;
    }
    return false;
  }

  public static void SeeAdvCal(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "showAdvCal")
      {
        if (sensorAttribute.Value != "false")
        {
          sensorAttribute.Value = "true";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "showAdvCal",
        Value = "false",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute showAdvCal
  {
    get
    {
      if (this._showAdvCal == null)
        this._showAdvCal = new SensorAttribute()
        {
          Value = "false"
        };
      return this._showAdvCal;
    }
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "LowValue")
        this._LowValue = sensorAttribute;
      if (sensorAttribute.Name == "HighValue")
        this._HighValue = sensorAttribute;
      if (sensorAttribute.Name == "Label")
        this._Label = sensorAttribute;
      if (sensorAttribute.Name == "showAdvCal")
        this._showAdvCal = sensorAttribute;
    }
  }

  public static string ScaleBadgeText(long sensorID)
  {
    string str1 = ZeroToTwentyMilliamp.Get4maValue(sensorID).ToString();
    string str2 = ZeroToTwentyMilliamp.GetHighValue(sensorID).ToString();
    string str3 = ZeroToTwentyMilliamp.GetLabel(sensorID).ToString();
    if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2) || string.IsNullOrEmpty(str3))
      return string.Empty;
    return $"{str1} to {str2} {str3}";
  }

  public static Dictionary<string, SensorAttribute.SensorScaleBadge> GetApplicationScaleOptions(
    long accountid)
  {
    IEnumerable<Sensor> sensors = Sensor.LoadByAccountID(accountid).Where<Sensor>((Func<Sensor, bool>) (application => application.ApplicationID == ZeroToTwentyMilliamp.MonnitApplicationID));
    Dictionary<string, SensorAttribute.SensorScaleBadge> applicationScaleOptions = new Dictionary<string, SensorAttribute.SensorScaleBadge>();
    foreach (Sensor sensor in sensors)
    {
      SensorAttribute.SensorScaleBadge sensorScaleBadge1 = new SensorAttribute.SensorScaleBadge();
      sensorScaleBadge1.SensorID = sensor.SensorID;
      sensorScaleBadge1.BadgeText = ZeroToTwentyMilliamp.ScaleBadgeText(sensor.SensorID);
      if (!string.IsNullOrEmpty(sensorScaleBadge1.BadgeText))
      {
        SensorAttribute.SensorScaleBadge sensorScaleBadge2 = sensorScaleBadge1;
        double num = ZeroToTwentyMilliamp.Get4maValue(sensor.SensorID);
        string str1 = num.ToString();
        sensorScaleBadge2.Attribute1 = str1;
        SensorAttribute.SensorScaleBadge sensorScaleBadge3 = sensorScaleBadge1;
        num = ZeroToTwentyMilliamp.GetHighValue(sensor.SensorID);
        string str2 = num.ToString();
        sensorScaleBadge3.Attribute2 = str2;
        sensorScaleBadge1.Label = ZeroToTwentyMilliamp.GetLabel(sensor.SensorID).ToString();
        if (!applicationScaleOptions.ContainsKey(sensorScaleBadge1.BadgeText))
          applicationScaleOptions.Add(sensorScaleBadge1.BadgeText, sensorScaleBadge1);
      }
    }
    return applicationScaleOptions;
  }

  public override string NotificationString
  {
    get
    {
      return $"{((double) this.PlotValue).ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture)} {this.Label.Value}";
    }
  }

  public override object PlotValue
  {
    get
    {
      return (object) this.Current.LinearInterpolation(0.0, this.LowValue.Value.ToDouble(), 20.0, this.HighValue.Value.ToDouble());
    }
  }

  public static ZeroToTwentyMilliamp Deserialize(string version, string serialized)
  {
    return new ZeroToTwentyMilliamp()
    {
      Current = serialized.ToDouble()
    };
  }

  public static ZeroToTwentyMilliamp Create(byte[] sdm, int startIndex)
  {
    return new ZeroToTwentyMilliamp()
    {
      Current = BitConverter.ToUInt16(sdm, startIndex).ToDouble() / 100.0
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

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = ZeroToTwentyMilliamp.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void VerifyNotificationValues(Notification notification, string scale)
  {
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    double result;
    if (!string.IsNullOrEmpty(collection["lowValue"]))
    {
      if (!double.TryParse(collection["lowValue"].ToString(), out result))
        throw new Exception("The Low Value can only be numeric");
      ZeroToTwentyMilliamp.Set4maValue(sensor.SensorID, collection["lowValue"].ToDouble(), collection["highValue"].ToDouble());
    }
    if (!string.IsNullOrEmpty(collection["highValue"]))
    {
      if (!double.TryParse(collection["highValue"].ToString(), out result))
        throw new Exception("The High Value can only be numeric");
      ZeroToTwentyMilliamp.SetHighValue(sensor.SensorID, result);
    }
    if (string.IsNullOrEmpty(collection["label"]))
      return;
    ZeroToTwentyMilliamp.SetLabel(sensor.SensorID, collection["label"]);
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    double result1 = 0.0;
    if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result1)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result1)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result1)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    ZeroToTwentyMilliamp toTwentyMilliamp = new ZeroToTwentyMilliamp();
    toTwentyMilliamp.SetSensorAttributes(sensor.SensorID);
    double num1 = toTwentyMilliamp.LowValue.Value.ToDouble();
    double num2 = toTwentyMilliamp.HighValue.Value.ToDouble();
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || double.TryParse(collection["Hysteresis_Manual"], out result1))
    {
      double num3 = Math.Abs((num2 - num1) / 20.0);
      double num4 = collection["Hysteresis_Manual"].ToDouble();
      sensor.Hysteresis = num3 == 0.0 ? sensor.DefaultValue<long>("DefaultHysteresis") : (num4 / num3 * 100.0).ToLong();
    }
    double result2;
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      if (double.TryParse(collection["MinimumThreshold_Manual"].ToString(), out result2))
      {
        double x = collection["MinimumThreshold_Manual"].ToDouble();
        sensor.MinimumThreshold = x >= num1 ? (x <= num2 ? (x.LinearInterpolation(num1, 0.0, num2, 20.0) * 100.0).ToLong() : (num2.LinearInterpolation(num1, 0.0, num2, 20.0) * 100.0).ToLong()) : (num1.LinearInterpolation(num1, 0.0, num2, 20.0) * 100.0).ToLong();
      }
      else
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      if (double.TryParse(collection["MaximumThreshold_Manual"].ToString(), out result2))
      {
        double x = collection["MaximumThreshold_Manual"].ToDouble();
        sensor.MaximumThreshold = x >= num1 ? (x <= num2 ? (x.LinearInterpolation(num1, 0.0, num2, 20.0) * 100.0).ToLong() : (num2.LinearInterpolation(num1, 0.0, num2, 20.0) * 100.0).ToLong()) : (num1.LinearInterpolation(num1, 0.0, num2, 20.0) * 100.0).ToLong();
        if (sensor.MinimumThreshold > sensor.MaximumThreshold)
          sensor.MaximumThreshold = sensor.MinimumThreshold;
      }
      else
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    }
    if (!string.IsNullOrEmpty(collection["power"]))
      sensor.Calibration3 = (long) collection["power"].ToInt();
    if (!string.IsNullOrEmpty(collection["delay"]))
      sensor.Calibration4 = (long) collection["delay"].ToInt();
    if (sensor.Calibration4 < 0L)
      sensor.Calibration4 = 0L;
    if (sensor.Calibration4 > 30000L)
      sensor.Calibration4 = 30000L;
    if (!string.IsNullOrEmpty(collection["BasicThreshold"]) && !string.IsNullOrEmpty(collection["BasicThresholdDirection"]) && collection["BasicThresholdDirection"] != "-1")
    {
      if (collection["BasicThresholdDirection"].ToInt() == 0)
      {
        sensor.MinimumThreshold = (collection["BasicThreshold"].ToDouble() * 100.0).ToLong();
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      }
      else if (collection["BasicThresholdDirection"].ToInt() == 1)
      {
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
        sensor.MaximumThreshold = (collection["BasicThreshold"].ToDouble() * 100.0).ToLong();
      }
    }
    if (!string.IsNullOrEmpty(collection["delay"]) && !string.IsNullOrEmpty(collection["power"]))
    {
      sensor.Calibration4 = collection["delay"].ToInt() >= 0 ? (collection["delay"].ToInt() <= 30000 ? collection["delay"].ToLong() : 30000L) : 0L;
      if (collection["power"].ToInt() == 0)
        sensor.Calibration3 = 0L;
      else if (collection["power"].ToInt() == 1)
        sensor.Calibration3 = 1L;
      else if (collection["power"].ToInt() == 2)
        sensor.Calibration3 = 2L;
      else if (collection["power"].ToInt() == 3)
        sensor.Calibration3 = 3L;
    }
    if (collection["showAdvCal"] == "on")
    {
      viewData["showAdvCal"] = "true";
      ZeroToTwentyMilliamp.SetShowAdvCal(sensor.SensorID, "true");
    }
    else
    {
      viewData["showAdvCal"] = "false";
      ZeroToTwentyMilliamp.SetShowAdvCal(sensor.SensorID, "false");
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (!(new Version(sensor.FirmwareVersion) > new Version("2.0.0.0")))
      return;
    double num1 = collection["observed"].ToDouble().LinearInterpolation(ZeroToTwentyMilliamp.GetLowValue(sensor.SensorID), 0.0, ZeroToTwentyMilliamp.GetHighValue(sensor.SensorID), 20.0);
    double num2 = collection["actual"].ToDouble().LinearInterpolation(ZeroToTwentyMilliamp.GetLowValue(sensor.SensorID), 0.0, ZeroToTwentyMilliamp.GetHighValue(sensor.SensorID), 20.0);
    double o1 = num1 * 100.0 * 51.0 / 100.0;
    double o2 = num2 * 100.0 * 51.0 / 100.0;
    if (o1 > 0.0 && o2 > 0.0)
    {
      int num3 = ((long) (o1.ToInt() * 20470) / sensor.Calibration1).ToInt();
      sensor.Calibration1 = (long) (o2.ToInt() * 20470 / num3);
      sensor.Save();
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

  public static string HystForUI(Sensor sensor)
  {
    ZeroToTwentyMilliamp toTwentyMilliamp = new ZeroToTwentyMilliamp();
    toTwentyMilliamp.SetSensorAttributes(sensor.SensorID);
    double num1 = toTwentyMilliamp.LowValue.Value.ToDouble();
    double num2 = Math.Abs((toTwentyMilliamp.HighValue.Value.ToDouble() - num1) / 20.0);
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : ((double) sensor.Hysteresis / 100.0 * num2).ToString("0.00");
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    ZeroToTwentyMilliamp toTwentyMilliamp = new ZeroToTwentyMilliamp();
    toTwentyMilliamp.SetSensorAttributes(sensor.SensorID);
    double lowY = toTwentyMilliamp.LowValue.Value.ToDouble();
    double highY = toTwentyMilliamp.HighValue.Value.ToDouble();
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : (sensor.MinimumThreshold.ToDouble() / 100.0).LinearInterpolation(0.0, lowY, 20.0, highY).ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    ZeroToTwentyMilliamp toTwentyMilliamp = new ZeroToTwentyMilliamp();
    toTwentyMilliamp.SetSensorAttributes(sensor.SensorID);
    double lowY = toTwentyMilliamp.LowValue.Value.ToDouble();
    double highY = toTwentyMilliamp.HighValue.Value.ToDouble();
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : ((double) sensor.MaximumThreshold / 100.0).LinearInterpolation(0.0, lowY, 20.0, highY).ToString();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(ZeroToTwentyMilliamp left, ZeroToTwentyMilliamp right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(ZeroToTwentyMilliamp left, ZeroToTwentyMilliamp right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(ZeroToTwentyMilliamp left, ZeroToTwentyMilliamp right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(ZeroToTwentyMilliamp left, ZeroToTwentyMilliamp right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(ZeroToTwentyMilliamp left, ZeroToTwentyMilliamp right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(ZeroToTwentyMilliamp left, ZeroToTwentyMilliamp right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is ZeroToTwentyMilliamp && this.Equals((MonnitApplicationBase) (obj as ZeroToTwentyMilliamp));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is ZeroToTwentyMilliamp && this.Current == (right as ZeroToTwentyMilliamp).Current;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is ZeroToTwentyMilliamp && this.Current != (right as ZeroToTwentyMilliamp).Current;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is ZeroToTwentyMilliamp && this.Current < (right as ZeroToTwentyMilliamp).Current;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is ZeroToTwentyMilliamp && this.Current <= (right as ZeroToTwentyMilliamp).Current;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is ZeroToTwentyMilliamp && this.Current > (right as ZeroToTwentyMilliamp).Current;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is ZeroToTwentyMilliamp && this.Current >= (right as ZeroToTwentyMilliamp).Current;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 20;

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

  public new static NotificationScaleInfoModel GetScalingInfo(Sensor sens)
  {
    NotificationScaleInfoModel scalingInfo = new NotificationScaleInfoModel();
    ZeroToTwentyMilliamp toTwentyMilliamp = new ZeroToTwentyMilliamp();
    toTwentyMilliamp.SetSensorAttributes(sens.SensorID);
    scalingInfo.Label = toTwentyMilliamp.Label.Value.ToStringSafe();
    scalingInfo.profileLow = toTwentyMilliamp.LowValue.Value.ToDouble();
    scalingInfo.profileHigh = toTwentyMilliamp.HighValue.Value.ToDouble();
    scalingInfo.baseLow = 0.0;
    scalingInfo.baseHigh = 20.0;
    return scalingInfo;
  }
}
