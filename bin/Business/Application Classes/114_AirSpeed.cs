// Decompiled with JetBrains decompiler
// Type: Monnit.AirSpeed
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

public class AirSpeed : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _ShowFullDataValue;
  private SensorAttribute _DuctAreaValue;
  private SensorAttribute _Units;
  private SensorAttribute _CorF;

  public static long MonnitApplicationID => 114;

  public static string ApplicationName => "Air Speed";

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.AirSpeedData, "Air Speed", this.Airspeed),
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
      AirSpeed.GetUnits(sensorID).ToString(),
      AirSpeed.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    });
  }

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => AirSpeed.MonnitApplicationID;

  public double Airspeed { get; set; }

  public double Temperature { get; set; }

  public int stsState { get; set; }

  public override string Serialize()
  {
    return $"{this.Airspeed.ToString()}|{this.Temperature.ToString()}|{this.stsState.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) AirSpeed.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      if (this.stsState == 16 /*0x10*/)
        return "Sensor read error";
      string notificationString;
      if (this.ShowFullDataValue.Value.ToLower() == "true")
      {
        string str = this.CorF == null || !(this.CorF.Value == "C") ? this.PlotTemperatureValue.ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture) : this.PlotTemperatureValue.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
        notificationString = $"{this.PlotValue.ToString()} {AirSpeed.AbreviatedMesaurement(this.Units)}, Temperature: {str}";
      }
      else
        notificationString = $"{(string.IsNullOrEmpty(this.PlotValue.ToString()) ? (object) "0" : (object) this.PlotValue.ToString())} {AirSpeed.AbreviatedMesaurement(this.Units)}";
      return notificationString;
    }
  }

  public override object PlotValue
  {
    get
    {
      if (this.Airspeed == double.MinValue)
        return (object) null;
      switch (this.Units)
      {
        case AirSpeed.SpeedUnits.mph:
          return (object) (this.Airspeed * 2.23694).ToString("0.#");
        case AirSpeed.SpeedUnits.knot:
          return (object) (this.Airspeed * 1.94384).ToString("0.#");
        case AirSpeed.SpeedUnits.kmph:
          return (object) (this.Airspeed * 3.6).ToString("0.#");
        case AirSpeed.SpeedUnits.CFM:
          return (object) (this.Airspeed * this.DuctAreaValue.Value.ToDouble() * 2118.88).ToString("0.#");
        case AirSpeed.SpeedUnits.CMH:
          return (object) (this.Airspeed * this.DuctAreaValue.Value.ToDouble() * 3600.0).ToString("0.#");
        case AirSpeed.SpeedUnits.CMM:
          return (object) (this.Airspeed * this.DuctAreaValue.Value.ToDouble() * 60.0).ToString("0.#");
        default:
          return (object) this.Airspeed.ToString("0.#");
      }
    }
  }

  public double PlotTemperatureValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? this.Temperature : this.Temperature.ToFahrenheit();
    }
  }

  public static AirSpeed Deserialize(string version, string serialized)
  {
    AirSpeed airSpeed = new AirSpeed();
    if (string.IsNullOrEmpty(serialized))
    {
      airSpeed.Airspeed = 0.0;
      airSpeed.Temperature = 0.0;
      airSpeed.stsState = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      airSpeed.Airspeed = strArray[0].ToDouble();
      if (strArray.Length > 1)
      {
        airSpeed.Temperature = strArray[1].ToDouble();
        try
        {
          airSpeed.stsState = strArray[2].ToInt();
        }
        catch
        {
          airSpeed.stsState = 0;
        }
      }
      else
        airSpeed.Temperature = strArray[0].ToDouble();
    }
    return airSpeed;
  }

  public static AirSpeed Create(byte[] sdm, int startIndex)
  {
    return new AirSpeed()
    {
      stsState = (int) sdm[startIndex - 1] & 240 /*0xF0*/,
      Airspeed = BitConverter.ToInt16(sdm, startIndex).ToDouble() / 10.0,
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

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    Notification notification = new Notification();
    notification.CompareValue = "0";
    notification.AccountID = sensor.AccountID;
    notification.CompareType = eCompareType.Greater_Than;
    notification.NotificationClass = eNotificationClass.Application;
    notification.ApplicationID = AirSpeed.MonnitApplicationID;
    notification.SnoozeDuration = 60;
    notification.Version = MonnitApplicationBase.NotificationVersion;
    switch (AirSpeed.GetUnits(sensor.SensorID))
    {
      case AirSpeed.SpeedUnits.mph:
        notification.Scale = "mph";
        break;
      case AirSpeed.SpeedUnits.knot:
        notification.Scale = "knot";
        break;
      case AirSpeed.SpeedUnits.kmph:
        notification.Scale = "kmph";
        break;
      default:
        notification.Scale = "mps";
        break;
    }
    return notification;
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    AirSpeed.IsFahrenheit(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["BasicThreshold"]) && !string.IsNullOrEmpty(collection["BasicThresholdDirection"]) && collection["BasicThresholdDirection"] != "-1")
    {
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
      if (collection["BasicThresholdDirection"].ToInt() == 0)
      {
        sensor.MinimumThreshold = (long) collection["BasicThreshold"].ToInt();
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      }
      else if (collection["BasicThresholdDirection"].ToInt() == 1)
      {
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
        sensor.MaximumThreshold = (long) collection["BasicThreshold"].ToInt();
      }
      sensor.MeasurementsPerTransmission = 6;
    }
    else
    {
      double result;
      if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
        sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
      if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      string str = AirSpeed.GetUnits(sensor.SensorID).ToString();
      double ductAreaValue = AirSpeed.GetDuctAreaValue(sensor.SensorID);
      double num1 = collection["MaximumThreshold_Manual"].ToDouble();
      double num2 = collection["MinimumThreshold_Manual"].ToDouble();
      double num3 = collection["Hysteresis_Manual"].ToDouble();
      if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
      {
        long num4 = (collection["Hysteresis_Manual"].ToDouble() * 10.0).ToLong();
        switch (str)
        {
          case "cfm":
            num4 = (num3 / (2118.88 * ductAreaValue) * 10.0).ToLong();
            break;
          case "cmh":
            num4 = (num3 / (3600.0 * ductAreaValue) * 10.0).ToLong();
            break;
          case "cmm":
            num4 = (num3 / (60.0 * ductAreaValue) * 10.0).ToLong();
            break;
          case "kmph":
            num4 = ((double) num4 * 0.2778).ToLong();
            break;
          case "knot":
            num4 = ((double) num4 * 0.5144).ToLong();
            break;
          case "mph":
            num4 = ((double) num4 * 0.447).ToLong();
            break;
        }
        if (num4 < 0L || num4 > 100L)
          num4 = sensor.DefaultValue<long>("DefaultHysteresis");
        sensor.Hysteresis = num4;
      }
      if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) && !string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
      {
        long num5 = (collection["MinimumThreshold_Manual"].ToDouble() * 10.0).ToLong();
        long num6 = (collection["MaximumThreshold_Manual"].ToDouble() * 10.0).ToLong();
        switch (str.ToLower())
        {
          case "cfm":
            num5 = (num2 / (2118.88 * ductAreaValue) * 10.0).ToLong();
            num6 = (num1 / (2118.88 * ductAreaValue) * 10.0).ToLong();
            break;
          case "cmh":
            num5 = (num2 / (3600.0 * ductAreaValue) * 10.0).ToLong();
            num6 = (num1 / (3600.0 * ductAreaValue) * 10.0).ToLong();
            break;
          case "cmm":
            num5 = (num2 / (60.0 * ductAreaValue) * 10.0).ToLong();
            num6 = (num1 / (60.0 * ductAreaValue) * 10.0).ToLong();
            break;
          case "kmph":
            num5 = ((double) num5 * 0.2778).ToLong();
            num6 = ((double) num6 * 0.2778).ToLong();
            break;
          case "knot":
            num5 = ((double) num5 * 0.5144).ToLong();
            num6 = ((double) num6 * 0.5144).ToLong();
            break;
          case "mph":
            num5 = ((double) num5 * 0.447).ToLong();
            num6 = ((double) num6 * 0.447).ToLong();
            break;
        }
        if (num5 < -500L)
          num5 = -500L;
        if (num5 > 500L)
          num5 = 500L;
        if (num6 < -500L)
          num6 = -500L;
        if (num6 > 500L)
          num6 = 500L;
        if (num5 > num6)
          num6 = num5;
        sensor.MinimumThreshold = num5;
        sensor.MaximumThreshold = num6;
      }
      if (!string.IsNullOrEmpty(collection["DefaultCalibration4"]))
      {
        long num7 = collection["DefaultCalibration4"].ToLong();
        if (num7 > 9000L)
          num7 = 9000L;
        if (num7 < 0L)
          num7 = 0L;
        sensor.Calibration4 = num7;
      }
      bool showFullData = collection["FullNotiString"].ToString() == "1" || collection["FullNotiString"].ToString().ToLower() == "true";
      AirSpeed.SetShowFullDataValue(sensor.SensorID, showFullData);
    }
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection returnData)
  {
    if (collection["TempScale"] == "on")
    {
      returnData["TempScale"] = "F";
      AirSpeed.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      returnData["TempScale"] = "C";
      AirSpeed.MakeCelsius(sensor.SensorID);
    }
    if (string.IsNullOrEmpty(collection["MeasurementScale"]))
      return;
    if (collection["MeasurementScale"] == "mps")
    {
      returnData["MeasurementScale"] = "mps";
      AirSpeed.SetUnits(sensor.SensorID, AirSpeed.SpeedUnits.mps);
    }
    if (collection["MeasurementScale"] == "mph")
    {
      returnData["MeasurementScale"] = "mph";
      AirSpeed.SetUnits(sensor.SensorID, AirSpeed.SpeedUnits.mph);
    }
    if (collection["MeasurementScale"] == "kmph")
    {
      returnData["MeasurementScale"] = "kmph";
      AirSpeed.SetUnits(sensor.SensorID, AirSpeed.SpeedUnits.kmph);
    }
    if (collection["MeasurementScale"] == "knot")
    {
      returnData["MeasurementScale"] = "knot";
      AirSpeed.SetUnits(sensor.SensorID, AirSpeed.SpeedUnits.knot);
    }
    if (collection["MeasurementScale"].ToLower() == "cfm")
    {
      returnData["MeasurementScale"] = "cfm";
      AirSpeed.SetUnits(sensor.SensorID, AirSpeed.SpeedUnits.CFM);
      if (!string.IsNullOrEmpty(collection["ductAreaValue"]))
      {
        double areaValue = collection["ductAreaValue"].ToDouble() / 10.7639;
        AirSpeed.SetDuctAreaValue(sensor.SensorID, areaValue);
      }
    }
    if (collection["MeasurementScale"].ToLower() == "cmh")
    {
      returnData["MeasurementScale"] = "cmh";
      AirSpeed.SetUnits(sensor.SensorID, AirSpeed.SpeedUnits.CMH);
      if (!string.IsNullOrEmpty(collection["ductAreaValue"]))
      {
        double areaValue = collection["ductAreaValue"].ToDouble();
        AirSpeed.SetDuctAreaValue(sensor.SensorID, areaValue);
      }
    }
    if (collection["MeasurementScale"].ToLower() == "cmm")
    {
      returnData["MeasurementScale"] = "cmm";
      AirSpeed.SetUnits(sensor.SensorID, AirSpeed.SpeedUnits.CMM);
      if (!string.IsNullOrEmpty(collection["ductAreaValue"]))
      {
        double areaValue = collection["ductAreaValue"].ToDouble();
        AirSpeed.SetDuctAreaValue(sensor.SensorID, areaValue);
      }
    }
  }

  public static Dictionary<string, string> NotificationScaleValues()
  {
    return new Dictionary<string, string>()
    {
      {
        "mps",
        "mps"
      },
      {
        "mph",
        "mph"
      },
      {
        "knot",
        "knot"
      },
      {
        "kmph",
        "kmph"
      },
      {
        "CFM",
        "CFM"
      },
      {
        "CMH",
        "CMH"
      },
      {
        "CMM",
        "CMM"
      }
    };
  }

  public static string HystForUI(Sensor sensor)
  {
    double num = sensor.Hysteresis == (long) uint.MaxValue ? 0.0 : Convert.ToDouble(sensor.Hysteresis);
    AirSpeed.GetDuctAreaValue(sensor.SensorID);
    switch (AirSpeed.AbreviatedMesaurement(AirSpeed.GetUnits(sensor.SensorID)).ToLower())
    {
      case "cfm":
        return (num / 10.0).ToString("0.#");
      case "cmh":
        return (num / 10.0).ToString("0.#");
      case "cmm":
        return (num / 10.0).ToString("0.#");
      case "kmph":
        return (num / 10.0 / 0.2778).ToLong().ToString();
      case "knot":
        return (num / 10.0 / 0.5144).ToLong().ToString();
      case "mph":
        return (num / 10.0 / 0.447).ToLong().ToString();
      case "mps":
        return (num / 10.0).ToString("0.#");
      default:
        return (num / 10.0).ToString("0.#");
    }
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    double num = sensor.MinimumThreshold == (long) uint.MaxValue ? 0.0 : Convert.ToDouble(sensor.MinimumThreshold) / 10.0;
    double ductAreaValue = AirSpeed.GetDuctAreaValue(sensor.SensorID);
    switch (AirSpeed.AbreviatedMesaurement(AirSpeed.GetUnits(sensor.SensorID)).ToLower())
    {
      case "cfm":
        return (num * ductAreaValue * 2118.88).ToString("0.#");
      case "cmh":
        return (num * ductAreaValue * 3600.0).ToString("0.#");
      case "cmm":
        return (num * ductAreaValue * 60.0).ToString("0.#");
      case "kmph":
        return (num * 3.6).ToString();
      case "knot":
        return (num * 1.94384).ToString();
      case "mph":
        return (num * 2.23694).ToString();
      default:
        return num.ToString();
    }
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    double num = sensor.MaximumThreshold == (long) uint.MaxValue ? 0.0 : Convert.ToDouble(sensor.MaximumThreshold) / 10.0;
    double ductAreaValue = AirSpeed.GetDuctAreaValue(sensor.SensorID);
    switch (AirSpeed.AbreviatedMesaurement(AirSpeed.GetUnits(sensor.SensorID)).ToLower())
    {
      case "cfm":
        return (num * ductAreaValue * 2118.88).ToString("0.#");
      case "cmh":
        return (num * ductAreaValue * 3600.0).ToString("0.#");
      case "cmm":
        return (num * ductAreaValue * 60.0).ToString("0.#");
      case "kmph":
        return (num * 3.6).ToString();
      case "knot":
        return (num * 1.94384).ToString();
      case "mph":
        return (num * 2.23694).ToString();
      default:
        return num.ToString();
    }
  }

  public static string TemperatureHystForUI(Sensor sensor)
  {
    double num = sensor.Calibration1 == (long) uint.MaxValue ? 0.0 : Convert.ToDouble(sensor.Calibration1) / 10.0;
    return AirSpeed.IsFahrenheit(sensor.SensorID) ? (num * 1.8).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string TemperatureMinThreshForUI(Sensor sensor)
  {
    double Celsius = sensor.Calibration3 == (long) uint.MaxValue ? Convert.ToDouble(sensor.DefaultValue<long>("DefaultMinimumThreshold")) / 10.0 : Convert.ToDouble(sensor.MinimumThreshold) / 10.0;
    return AirSpeed.IsFahrenheit(sensor.SensorID) ? Celsius.ToFahrenheit().ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : Celsius.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string TemperatureMaxThreshForUI(Sensor sensor)
  {
    double Celsius = sensor.MaximumThreshold == (long) uint.MaxValue ? Convert.ToDouble(sensor.DefaultValue<long>("DefaultMaximumThreshold")) / 10.0 : Convert.ToDouble(sensor.MaximumThreshold) / 10.0;
    return AirSpeed.IsFahrenheit(sensor.SensorID) ? Celsius.ToFahrenheit().ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : Celsius.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "SpeedUnits")
        this._Units = sensorAttribute;
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
      if (sensorAttribute.Name == "DuctAreaValue")
        this._DuctAreaValue = sensorAttribute;
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

  public static bool GetShowFullDataValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ShowFullDataValue")
        return sensorAttribute.Value.ToBool();
    }
    return false;
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

  public SensorAttribute DuctAreaValue
  {
    get
    {
      if (this._DuctAreaValue == null)
        this._DuctAreaValue = new SensorAttribute()
        {
          Value = "1"
        };
      return this._DuctAreaValue;
    }
  }

  public static void SetDuctAreaValue(long sensorID, double areaValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "DuctAreaValue")
      {
        sensorAttribute.Value = areaValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "DuctAreaValue",
        Value = areaValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static double GetDuctAreaValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "DuctAreaValue")
        return sensorAttribute.Value.ToDouble();
    }
    return 1.0;
  }

  public static AirSpeed.SpeedUnits GetUnits(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "SpeedUnits")
        return (AirSpeed.SpeedUnits) Enum.Parse(typeof (AirSpeed.SpeedUnits), sensorAttribute.Value);
    }
    return AirSpeed.SpeedUnits.mps;
  }

  public static void SetUnits(long sensorID, AirSpeed.SpeedUnits units)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "SpeedUnits")
      {
        if (sensorAttribute.Value != units.ToString())
        {
          sensorAttribute.Value = units.ToString();
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "SpeedUnits",
        Value = units.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute UnitsAttribute => this._Units;

  public AirSpeed.SpeedUnits Units
  {
    get
    {
      return this._Units != null ? (AirSpeed.SpeedUnits) Enum.Parse(typeof (AirSpeed.SpeedUnits), this._Units.Value) : AirSpeed.SpeedUnits.mps;
    }
  }

  public static string AbreviatedMesaurement(AirSpeed.SpeedUnits unit)
  {
    string str;
    switch (unit)
    {
      case AirSpeed.SpeedUnits.mph:
        str = "mph";
        break;
      case AirSpeed.SpeedUnits.knot:
        str = "knot";
        break;
      case AirSpeed.SpeedUnits.kmph:
        str = "kmph";
        break;
      case AirSpeed.SpeedUnits.CFM:
        str = "CFM";
        break;
      case AirSpeed.SpeedUnits.CMH:
        str = "CMH";
        break;
      case AirSpeed.SpeedUnits.CMM:
        str = "CMM";
        break;
      default:
        str = "mps";
        break;
    }
    return str;
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

  public static void MakeMetersPerSecond(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "MpsOrMPH")
      {
        if (sensorAttribute.Value != "m/s")
        {
          sensorAttribute.Value = "m/s";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "MpsOrMPH",
        Value = "m/s",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute CorF => this._CorF;

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(AirSpeed left, AirSpeed right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(AirSpeed left, AirSpeed right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(AirSpeed left, AirSpeed right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(AirSpeed left, AirSpeed right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(AirSpeed left, AirSpeed right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(AirSpeed left, AirSpeed right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is AirSpeed && this.Equals((MonnitApplicationBase) (obj as AirSpeed));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is AirSpeed && this.Airspeed == (right as AirSpeed).Airspeed;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is AirSpeed && this.Airspeed != (right as AirSpeed).Airspeed;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is AirSpeed && this.Airspeed < (right as AirSpeed).Airspeed;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is AirSpeed && this.Airspeed <= (right as AirSpeed).Airspeed;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is AirSpeed && this.Airspeed > (right as AirSpeed).Airspeed;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is AirSpeed && this.Airspeed >= (right as AirSpeed).Airspeed;
  }

  public static long DefaultMinThreshold => -50;

  public static long DefaultMaxThreshold => 50;

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
        "mps",
        "mps"
      },
      {
        "mph",
        "mph"
      },
      {
        "knot",
        "knot"
      },
      {
        "kmph",
        "kmph"
      },
      {
        "CFM",
        "CFM"
      },
      {
        "CMH",
        "CMH"
      },
      {
        "CMM",
        "CMM"
      }
    };
  }

  public enum SpeedUnits
  {
    mph,
    mps,
    knot,
    kmph,
    CFM,
    CMH,
    CMM,
  }
}
