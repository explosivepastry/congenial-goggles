// Decompiled with JetBrains decompiler
// Type: Monnit.UltrasonicRangerIndustrial
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using BaseApplication;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

#nullable disable
namespace Monnit;

public class UltrasonicRangerIndustrial : MonnitApplicationBase, ISensorAttribute
{
  private double _Depth = double.MinValue;
  private SensorAttribute _Units;
  private SensorAttribute _TankDepth;
  private SensorAttribute _ShowDepth;
  private SensorAttribute _QueueID;
  private SensorAttribute _CalibrationValues;

  public static long MonnitApplicationID => UltrasonicRangerIndustrialBase.MonnitApplicationID;

  public static string ApplicationName => "Ultrasonic Ranger";

  public static eApplicationProfileType ProfileType => UltrasonicRangerIndustrialBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => UltrasonicRangerIndustrial.MonnitApplicationID;

  public double Distance { get; set; }

  public ushort Min { get; set; }

  public ushort Max { get; set; }

  public ushort Mean { get; set; }

  public ushort Median { get; set; }

  public ushort Mode { get; set; }

  public short Temperature { get; set; }

  public byte MeasurementMode { get; set; }

  public double Depth
  {
    get
    {
      if (this._Depth == double.MinValue)
      {
        double tankDepth = 0.0;
        if (this.TankDepth != null)
          tankDepth = this.TankDepth.Value.ToDouble();
        this._Depth = UltrasonicRangerIndustrial.GetDepth(this.Distance, tankDepth);
      }
      return this._Depth;
    }
    set => this._Depth = value;
  }

  public static double GetDepth(double distance, double tankDepth) => tankDepth - distance;

  public int status { get; set; }

  public static UltrasonicRangerIndustrial Create(byte[] sdm, int startIndex)
  {
    UltrasonicRangerIndustrial rangerIndustrial = new UltrasonicRangerIndustrial();
    int num = (int) sdm[startIndex - 1];
    rangerIndustrial.Distance = (double) BitConverter.ToUInt16(sdm, startIndex);
    if (sdm.Length - startIndex > 2)
    {
      rangerIndustrial.Min = BitConverter.ToUInt16(sdm, 2);
      rangerIndustrial.Max = BitConverter.ToUInt16(sdm, 4);
      rangerIndustrial.Mean = BitConverter.ToUInt16(sdm, 6);
      rangerIndustrial.Median = BitConverter.ToUInt16(sdm, 8);
      rangerIndustrial.Mode = BitConverter.ToUInt16(sdm, 10);
      rangerIndustrial.Temperature = BitConverter.ToInt16(sdm, 12);
      rangerIndustrial.MeasurementMode = sdm[14];
    }
    rangerIndustrial.status = 0;
    if ((num & 16 /*0x10*/) == 16 /*0x10*/)
      rangerIndustrial.status = 1;
    if ((num & 32 /*0x20*/) == 32 /*0x20*/)
      rangerIndustrial.status = 2;
    if ((num & 64 /*0x40*/) == 64 /*0x40*/)
      rangerIndustrial.status = 4;
    if ((num & 128 /*0x80*/) == 128 /*0x80*/)
      rangerIndustrial.status = 8;
    return rangerIndustrial;
  }

  public override string Serialize()
  {
    return $"{this.Distance.ToString()}|{this.Depth.ToString()}|{this.status.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[9]
      {
        new AppDatum(eDatumType.Centimeter, "Distance", this.Distance),
        new AppDatum(eDatumType.Centimeter, "CentimeterMin", (int) this.Min),
        new AppDatum(eDatumType.Centimeter, "CentimeterMax", (int) this.Max),
        new AppDatum(eDatumType.Centimeter, "CentimeterMean", (int) this.Mean),
        new AppDatum(eDatumType.Centimeter, "CentimeterMedian", (int) this.Median),
        new AppDatum(eDatumType.Centimeter, "CentimeterMode", (int) this.Mode),
        new AppDatum(eDatumType.TemperatureData, "Temperature", (int) this.Temperature),
        new AppDatum(eDatumType.State, "ReadingMode", (int) this.MeasurementMode),
        new AppDatum(eDatumType.Centimeter, "Depth", this.Depth)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>()
    {
      UltrasonicRangerIndustrial.GetUnits(sensorID).ToString()
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) UltrasonicRangerIndustrial.Deserialize(version, serialized);
  }

  public static UltrasonicRangerIndustrial Deserialize(string version, string serialized)
  {
    UltrasonicRangerIndustrial rangerIndustrial = new UltrasonicRangerIndustrial();
    if (string.IsNullOrEmpty(serialized))
    {
      rangerIndustrial.status = 0;
      rangerIndustrial.Distance = 0.0;
      rangerIndustrial.Depth = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      rangerIndustrial.Distance = strArray[0].ToDouble();
      if (strArray.Length > 1)
      {
        rangerIndustrial.Depth = strArray[1].ToDouble();
        try
        {
          rangerIndustrial.status = strArray[2].ToInt();
        }
        catch
        {
          rangerIndustrial.status = 0;
        }
      }
      else
      {
        rangerIndustrial.Distance = strArray[0].ToDouble();
        rangerIndustrial.Depth = strArray[0].ToDouble();
        rangerIndustrial.status = 0;
      }
    }
    return rangerIndustrial;
  }

  public static UltrasonicRangerIndustrial.DistanceUnits GetUnits(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "DistanceUnits")
        return (UltrasonicRangerIndustrial.DistanceUnits) Enum.Parse(typeof (UltrasonicRangerIndustrial.DistanceUnits), sensorAttribute.Value);
    }
    return UltrasonicRangerIndustrial.DistanceUnits.Centimeter;
  }

  public static void SetUnits(long sensorID, UltrasonicRangerIndustrial.DistanceUnits units)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "DistanceUnits")
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
        Name = "DistanceUnits",
        Value = units.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute UnitsAttribute => this._Units;

  public UltrasonicRangerIndustrial.DistanceUnits Units
  {
    get
    {
      return this._Units != null ? (UltrasonicRangerIndustrial.DistanceUnits) Enum.Parse(typeof (UltrasonicRangerIndustrial.DistanceUnits), this._Units.Value) : UltrasonicRangerIndustrial.DistanceUnits.Centimeter;
    }
  }

  public SensorAttribute TankDepth => this._TankDepth;

  public static double GetTankDepth(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "TankDepth")
        return sensorAttribute.Value.ToDouble();
    }
    return 0.0;
  }

  public static void SetTankDepth(long sensorID, double TankDepth)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == nameof (TankDepth))
      {
        flag = true;
        if (TankDepth < 0.0)
        {
          sensorAttribute.Delete();
        }
        else
        {
          sensorAttribute.Value = TankDepth.ToString();
          sensorAttribute.Save();
        }
      }
    }
    if (!flag && TankDepth >= 0.0)
      new SensorAttribute()
      {
        Name = nameof (TankDepth),
        Value = TankDepth.ToString(),
        SensorID = sensorID
      }.Save();
    if (TankDepth < 0.0 && UltrasonicRangerIndustrial.GetShowDepth(sensorID))
      UltrasonicRangerIndustrial.SetShowDepth(sensorID, false);
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute ShowDepth => this._ShowDepth;

  public static bool GetShowDepth(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ShowDepth")
        return sensorAttribute.Value.ToBool();
    }
    return false;
  }

  public static void SetShowDepth(long sensorID, bool ShowDepth)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == nameof (ShowDepth))
      {
        sensorAttribute.Value = ShowDepth.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = nameof (ShowDepth),
        Value = ShowDepth.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute QueueID => this._QueueID;

  public static int GetQueueID(long sensorID)
  {
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

  public SensorAttribute CalibrationValues => this._CalibrationValues;

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

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "DistanceUnits")
        this._Units = sensorAttribute;
      if (sensorAttribute.Name == "TankDepth")
        this._TankDepth = sensorAttribute;
      if (sensorAttribute.Name == "ShowDepth")
        this._ShowDepth = sensorAttribute;
      if (sensorAttribute.Name == "CalibrationValues")
        this._CalibrationValues = sensorAttribute;
    }
  }

  public override object PlotValue
  {
    get
    {
      if (this.Distance > 750.0)
        return (object) null;
      double num = this.Distance;
      if (this.ShowDepth != null && this.ShowDepth.Value.ToBool() && this.TankDepth != null)
        num = this.TankDepth.Value.ToDouble() - this.Distance;
      switch (this.Units)
      {
        case UltrasonicRangerIndustrial.DistanceUnits.Meter:
          return (object) Math.Round(num * 0.01, 1);
        case UltrasonicRangerIndustrial.DistanceUnits.Inch:
          return (object) Math.Round(num * 0.393701, 1);
        case UltrasonicRangerIndustrial.DistanceUnits.Feet:
          return (object) Math.Round(num * 0.0328084, 1);
        case UltrasonicRangerIndustrial.DistanceUnits.Yard:
          return (object) Math.Round(num * 0.0109361, 1);
        default:
          return (object) Math.Round(num, 1);
      }
    }
  }

  public static string AbreviatedMesaurement(UltrasonicRangerIndustrial.DistanceUnits unit)
  {
    string str;
    switch (unit)
    {
      case UltrasonicRangerIndustrial.DistanceUnits.Meter:
        str = "M";
        break;
      case UltrasonicRangerIndustrial.DistanceUnits.Inch:
        str = "in";
        break;
      case UltrasonicRangerIndustrial.DistanceUnits.Feet:
        str = "ft";
        break;
      case UltrasonicRangerIndustrial.DistanceUnits.Yard:
        str = "yd";
        break;
      default:
        str = "cm";
        break;
    }
    return str;
  }

  public static double ConvertUnits(Sensor sensor, double value)
  {
    switch (UltrasonicRangerIndustrial.AbreviatedMesaurement(UltrasonicRangerIndustrial.GetUnits(sensor.SensorID)))
    {
      case "M":
        return value * 0.01;
      case "in":
        return value * 0.393701;
      case "ft":
        return value * 0.0328084;
      case "yrd":
        return value * 0.0109361;
      default:
        return value;
    }
  }

  public override string NotificationString
  {
    get
    {
      if (this.PlotValue == null)
        return "Hardware Failure";
      string str = string.Empty;
      if (this.Distance == (double) ushort.MaxValue || this.status == 4)
        return "Hardware Failure";
      if (this.status == 1)
        str = this.Distance > 20.0 ? (this.Distance < 750.0 ? " - Measurement Out of Range:" : " - Measurement Out of Range: Too Far") : " - Measurement Out of Range: Too Close";
      else if (this.status == 8)
        str = " - Readings Unstable";
      return $"{this.PlotValue} {UltrasonicRangerIndustrial.AbreviatedMesaurement(this.Units)} {str}";
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
    notification.ApplicationID = UltrasonicRangerIndustrial.MonnitApplicationID;
    notification.SnoozeDuration = 60;
    notification.Version = MonnitApplicationBase.NotificationVersion;
    switch (UltrasonicRangerIndustrial.GetUnits(sensor.SensorID))
    {
      case UltrasonicRangerIndustrial.DistanceUnits.Meter:
        notification.Scale = "Meter";
        break;
      case UltrasonicRangerIndustrial.DistanceUnits.Inch:
        notification.Scale = "Inch";
        break;
      case UltrasonicRangerIndustrial.DistanceUnits.Feet:
        notification.Scale = "Feet";
        break;
      case UltrasonicRangerIndustrial.DistanceUnits.Yard:
        notification.Scale = "Yard";
        break;
      default:
        notification.Scale = "Centimeter";
        break;
    }
    return notification;
  }

  public static Dictionary<string, string> NotificationScaleValues()
  {
    return new Dictionary<string, string>()
    {
      {
        "Centimeters",
        "Centimeters"
      },
      {
        "Meters",
        "Meters"
      },
      {
        "Inches",
        "Inches"
      },
      {
        "Feet",
        "Feet"
      },
      {
        "Yards",
        "Yards"
      }
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
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
      string str = UltrasonicRangerIndustrial.GetUnits(sensor.SensorID).ToString();
      bool showDepth = UltrasonicRangerIndustrial.GetShowDepth(sensor.SensorID);
      double tankDepth = UltrasonicRangerIndustrial.GetTankDepth(sensor.SensorID);
      if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
      {
        if (str == "Millimeter" || str == "Centimeter" || str == "Meter")
          sensor.Hysteresis = collection["Hysteresis_Manual"].ToLong();
        if (str == "Inch" || str == "Feet" || str == "Yard")
          sensor.Hysteresis = (collection["Hysteresis_Manual"].ToDouble() * 2.54).ToLong();
      }
      if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) && !string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
      {
        double o1;
        double o2;
        switch (str)
        {
          case "Meter":
            o1 = collection["MinimumThreshold_Manual"].ToDouble() * 100.0;
            o2 = collection["MaximumThreshold_Manual"].ToDouble() * 100.0;
            break;
          case "Inch":
            o1 = collection["MinimumThreshold_Manual"].ToDouble() * 2.54;
            o2 = collection["MaximumThreshold_Manual"].ToDouble() * 2.54;
            break;
          case "Feet":
            o1 = collection["MinimumThreshold_Manual"].ToDouble() * 30.48;
            o2 = collection["MaximumThreshold_Manual"].ToDouble() * 30.48;
            break;
          case "Yard":
            o1 = collection["MinimumThreshold_Manual"].ToDouble() * 91.44;
            o2 = collection["MaximumThreshold_Manual"].ToDouble() * 91.44;
            break;
          default:
            o1 = collection["MinimumThreshold_Manual"].ToDouble();
            o2 = collection["MaximumThreshold_Manual"].ToDouble();
            break;
        }
        if (showDepth)
        {
          double num = o1;
          o1 = tankDepth - o2;
          o2 = tankDepth - num;
        }
        if (o1 < (double) sensor.DefaultValue<long>("DefaultMinimumThreshold"))
          o1 = (double) sensor.DefaultValue<long>("DefaultMinimumThreshold");
        if (o1 > (double) sensor.DefaultValue<long>("DefaultMaximumThreshold"))
          o1 = (double) sensor.DefaultValue<long>("DefaultMinimumThreshold");
        if (o2 < (double) sensor.DefaultValue<long>("DefaultMinimumThreshold"))
          o2 = (double) sensor.DefaultValue<long>("DefaultMinimumThreshold");
        if (o2 > (double) sensor.DefaultValue<long>("DefaultMaximumThreshold"))
          o2 = (double) sensor.DefaultValue<long>("DefaultMinimumThreshold");
        if (o2 < o1)
          o2 = o1;
        sensor.MinimumThreshold = o1.ToLong();
        sensor.MaximumThreshold = o2.ToLong();
      }
      if (!string.IsNullOrWhiteSpace(collection["MeasurementMode"]))
      {
        int num = collection["MeasurementMode"].ToInt();
        UltrasonicRangerIndustrialBase.SetMeasurementMode((ISensor) sensor, num);
      }
      bool flag = !string.IsNullOrWhiteSpace(collection["AverageData"]) && collection["AverageData"] == "on";
      UltrasonicRangerIndustrialBase.SetAverageData((ISensor) sensor, flag);
    }
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["MeasurementScale"]))
    {
      if (collection["MeasurementScale"] == "cm")
      {
        viewData["MeasurementScale"] = "cm";
        UltrasonicRangerIndustrial.SetUnits(sensor.SensorID, UltrasonicRangerIndustrial.DistanceUnits.Centimeter);
      }
      if (collection["MeasurementScale"] == "in")
      {
        viewData["MeasurementScale"] = "in";
        UltrasonicRangerIndustrial.SetUnits(sensor.SensorID, UltrasonicRangerIndustrial.DistanceUnits.Inch);
      }
      if (collection["MeasurementScale"] == "ft")
      {
        viewData["MeasurementScale"] = "ft";
        UltrasonicRangerIndustrial.SetUnits(sensor.SensorID, UltrasonicRangerIndustrial.DistanceUnits.Feet);
      }
      if (collection["MeasurementScale"] == "yrd")
      {
        viewData["MeasurementScale"] = "yd";
        UltrasonicRangerIndustrial.SetUnits(sensor.SensorID, UltrasonicRangerIndustrial.DistanceUnits.Yard);
      }
      if (collection["MeasurementScale"] == "M")
      {
        viewData["MeasurementScale"] = "M";
        UltrasonicRangerIndustrial.SetUnits(sensor.SensorID, UltrasonicRangerIndustrial.DistanceUnits.Meter);
      }
    }
    if (!string.IsNullOrEmpty(collection["ShowDepth"]))
      UltrasonicRangerIndustrial.SetShowDepth(sensor.SensorID, collection["ShowDepth"].ToBool());
    if (!string.IsNullOrEmpty(collection["TankDepth"]))
    {
      UltrasonicRangerIndustrial.DistanceUnits units = UltrasonicRangerIndustrial.GetUnits(sensor.SensorID);
      double TankDepth = collection["TankDepth"].ToDouble();
      switch (units)
      {
        case UltrasonicRangerIndustrial.DistanceUnits.Meter:
          TankDepth *= 100.0;
          break;
        case UltrasonicRangerIndustrial.DistanceUnits.Inch:
          TankDepth *= 2.54;
          break;
        case UltrasonicRangerIndustrial.DistanceUnits.Feet:
          TankDepth *= 30.48;
          break;
        case UltrasonicRangerIndustrial.DistanceUnits.Yard:
          TankDepth *= 91.44;
          break;
      }
      UltrasonicRangerIndustrial.SetTankDepth(sensor.SensorID, TankDepth);
    }
    else
      UltrasonicRangerIndustrial.SetTankDepth(sensor.SensorID, (double) long.MinValue);
  }

  public static string HystForUI(Sensor sensor)
  {
    switch (UltrasonicRangerIndustrial.AbreviatedMesaurement(UltrasonicRangerIndustrial.GetUnits(sensor.SensorID)))
    {
      case "in":
      case "ft":
      case "yrd":
        return ((double) sensor.Hysteresis * 0.393701).ToString();
      default:
        return sensor.Hysteresis == (long) uint.MaxValue ? "" : sensor.Hysteresis.ToString();
    }
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    switch (UltrasonicRangerIndustrial.AbreviatedMesaurement(UltrasonicRangerIndustrial.GetUnits(sensor.SensorID)))
    {
      case "M":
        return ((double) sensor.MinimumThreshold * 0.01).ToString();
      case "in":
        return ((double) sensor.MinimumThreshold * 0.393701).ToString();
      case "ft":
        return ((double) sensor.MinimumThreshold * 0.0328084).ToString();
      case "yrd":
        return ((double) sensor.MinimumThreshold * 0.0109361).ToString();
      default:
        return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : sensor.MinimumThreshold.ToString();
    }
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    switch (UltrasonicRangerIndustrial.AbreviatedMesaurement(UltrasonicRangerIndustrial.GetUnits(sensor.SensorID)))
    {
      case "M":
        return ((double) sensor.MaximumThreshold * 0.01).ToString();
      case "in":
        return ((double) sensor.MaximumThreshold * 0.393701).ToString();
      case "ft":
        return ((double) sensor.MaximumThreshold * 0.0328084).ToString();
      case "yrd":
        return ((double) sensor.MaximumThreshold * 0.0109361).ToString();
      default:
        return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : sensor.MaximumThreshold.ToString();
    }
  }

  public static double TankDepthForUI(Sensor sensor)
  {
    double tankDepth = UltrasonicRangerIndustrial.GetTankDepth(sensor.SensorID);
    switch (UltrasonicRangerIndustrial.AbreviatedMesaurement(UltrasonicRangerIndustrial.GetUnits(sensor.SensorID)))
    {
      case "M":
        return Math.Round(tankDepth * 0.01, 1);
      case "in":
        return Math.Round(tankDepth * 0.393701, 1);
      case "ft":
        return Math.Round(tankDepth * 0.0328084, 1);
      case "yrd":
        return Math.Round(tankDepth * 0.0109361, 1);
      default:
        return Math.Round(tankDepth, 1);
    }
  }

  public static double DefaultMinThreshForUI(Sensor sensor)
  {
    switch (UltrasonicRangerIndustrial.AbreviatedMesaurement(UltrasonicRangerIndustrial.GetUnits(sensor.SensorID)))
    {
      case "M":
        return (double) UltrasonicRangerIndustrial.DefaultMinThreshold * 0.01;
      case "in":
        return (double) UltrasonicRangerIndustrial.DefaultMinThreshold * 0.393701;
      case "ft":
        return (double) UltrasonicRangerIndustrial.DefaultMinThreshold * 0.0328084;
      case "yrd":
        return (double) UltrasonicRangerIndustrial.DefaultMinThreshold * 0.0109361;
      default:
        return (double) UltrasonicRangerIndustrial.DefaultMinThreshold;
    }
  }

  public static double DefaultMaxThreshForUI(Sensor sensor)
  {
    switch (UltrasonicRangerIndustrial.AbreviatedMesaurement(UltrasonicRangerIndustrial.GetUnits(sensor.SensorID)))
    {
      case "M":
        return (double) UltrasonicRangerIndustrial.DefaultMaxThreshold * 0.01;
      case "in":
        return (double) UltrasonicRangerIndustrial.DefaultMaxThreshold * 0.393701;
      case "ft":
        return (double) UltrasonicRangerIndustrial.DefaultMaxThreshold * 0.0328084;
      case "yrd":
        return (double) UltrasonicRangerIndustrial.DefaultMaxThreshold * 0.0109361;
      default:
        return (double) UltrasonicRangerIndustrial.DefaultMaxThreshold;
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (!string.IsNullOrEmpty(collection["CalibrateType"]) && collection["CalibrateType"] == "on")
    {
      double num = collection["offset"].ToDouble() != double.MinValue ? collection["offset"].ToDouble() : 0.0;
      switch (collection["DistanceUnits"])
      {
        case "Meter":
          num *= 100.0;
          break;
        case "Inch":
          num *= 2.54;
          break;
        case "Feet":
          num *= 30.48;
          break;
        case "Yard":
          num *= 91.44;
          break;
      }
      if (num < -40.0)
        num = -40.0;
      if (num > 40.0)
        num = 40.0;
      sensor.Calibration1 = Math.Round(num, 2).ToLong();
      sensor.ProfileConfig2Dirty = true;
      sensor.Save();
    }
    else if (!string.IsNullOrEmpty(collection["actual"]) && !string.IsNullOrEmpty(collection["observed"]))
    {
      double actual = 0.0;
      double observed = 0.0;
      switch (collection["DistanceUnits"])
      {
        case "Centimeter":
          actual = collection["actual"].ToDouble();
          observed = collection["observed"].ToDouble();
          break;
        case "Meter":
          actual = collection["actual"].ToDouble() * 100.0;
          observed = collection["observed"].ToDouble() * 100.0;
          break;
        case "Inch":
          actual = collection["actual"].ToDouble() * 2.54;
          observed = collection["observed"].ToDouble() * 2.54;
          break;
        case "Feet":
          actual = collection["actual"].ToDouble() * 30.48;
          observed = collection["observed"].ToDouble() * 30.48;
          break;
        case "Yard":
          actual = collection["actual"].ToDouble() * 91.44;
          observed = collection["observed"].ToDouble() * 91.44;
          break;
      }
      UltrasonicRangerIndustrial.SetQueueID(sensor.SensorID);
      UltrasonicRangerIndustrial.SetCalibrationValues(sensor.SensorID, actual, observed, UltrasonicRangerIndustrial.GetQueueID(sensor.SensorID));
      sensor.PendingActionControlCommand = true;
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

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      string[] strArray = UltrasonicRangerIndustrial.GetCalibrationValues(sensor.SensorID).Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      numArrayList.Add(UltrasonicRangerIndustrialBase.CalibrateFrame(sensor.SensorID, strArray[0].ToInt(), strArray[1].ToInt(), strArray[2].ToInt()));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(UltrasonicRangerIndustrial left, UltrasonicRangerIndustrial right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(UltrasonicRangerIndustrial left, UltrasonicRangerIndustrial right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(UltrasonicRangerIndustrial left, UltrasonicRangerIndustrial right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(UltrasonicRangerIndustrial left, UltrasonicRangerIndustrial right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(UltrasonicRangerIndustrial left, UltrasonicRangerIndustrial right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(UltrasonicRangerIndustrial left, UltrasonicRangerIndustrial right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is UltrasonicRangerIndustrial && this.Equals((MonnitApplicationBase) (obj as UltrasonicRangerIndustrial));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is UltrasonicRangerIndustrial && this.Distance == (right as UltrasonicRangerIndustrial).Distance;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is UltrasonicRangerIndustrial && this.Distance != (right as UltrasonicRangerIndustrial).Distance;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is UltrasonicRangerIndustrial && this.Distance < (right as UltrasonicRangerIndustrial).Distance;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is UltrasonicRangerIndustrial && this.Distance <= (right as UltrasonicRangerIndustrial).Distance;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is UltrasonicRangerIndustrial && this.Distance > (right as UltrasonicRangerIndustrial).Distance;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is UltrasonicRangerIndustrial && this.Distance >= (right as UltrasonicRangerIndustrial).Distance;
  }

  public static long DefaultMinThreshold => 20;

  public static long DefaultMaxThreshold => 750;

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
        "cm",
        "Centimeters"
      },
      {
        "in",
        "Inches"
      },
      {
        "ft",
        "feet"
      },
      {
        "yd",
        "Yards"
      },
      {
        "M",
        "Meters"
      }
    };
  }

  public enum DistanceUnits
  {
    Centimeter,
    Meter,
    Inch,
    Feet,
    Yard,
  }
}
