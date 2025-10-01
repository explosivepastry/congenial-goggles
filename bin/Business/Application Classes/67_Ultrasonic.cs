// Decompiled with JetBrains decompiler
// Type: Monnit.Ultrasonic
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

public class Ultrasonic : MonnitApplicationBase, ISensorAttribute
{
  private double _Depth = double.MinValue;
  private SensorAttribute _Units;
  private SensorAttribute _TankDepth;
  private SensorAttribute _ShowDepth;
  private SensorAttribute _QueueID;
  private SensorAttribute _CalibrationValues;

  public static long MonnitApplicationID => UltrasonicBase.MonnitApplicationID;

  public static string ApplicationName => nameof (Ultrasonic);

  public static eApplicationProfileType ProfileType => UltrasonicBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => Ultrasonic.MonnitApplicationID;

  public double Distance { get; set; }

  public double Depth
  {
    get
    {
      if (this._Depth == double.MinValue)
      {
        double tankDepth = 0.0;
        if (this.TankDepth != null)
          tankDepth = this.TankDepth.Value.ToDouble();
        this._Depth = Ultrasonic.GetDepth(this.Distance, tankDepth);
      }
      return this._Depth;
    }
    set => this._Depth = value;
  }

  public static double GetDepth(double distance, double tankDepth) => tankDepth - distance;

  public int status { get; set; }

  public static Ultrasonic Create(byte[] sdm, int startIndex)
  {
    Ultrasonic ultrasonic = new Ultrasonic();
    int num = (int) sdm[startIndex - 1];
    ultrasonic.Distance = (double) BitConverter.ToUInt16(sdm, startIndex);
    ultrasonic.status = 0;
    if ((num & 16 /*0x10*/) == 16 /*0x10*/)
      ultrasonic.status = 1;
    if ((num & 64 /*0x40*/) == 64 /*0x40*/)
      ultrasonic.status = 4;
    if ((num & 128 /*0x80*/) == 128 /*0x80*/)
      ultrasonic.status = 8;
    return ultrasonic;
  }

  public override string Serialize()
  {
    return $"{this.Distance.ToString()}|{this.Depth.ToString()}|{this.status.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.Centimeter, "Distance", this.Distance),
        new AppDatum(eDatumType.Centimeter, "Depth", this.Depth)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>()
    {
      Ultrasonic.GetUnits(sensorID).ToString()
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Ultrasonic.Deserialize(version, serialized);
  }

  public static Ultrasonic Deserialize(string version, string serialized)
  {
    Ultrasonic ultrasonic = new Ultrasonic();
    if (string.IsNullOrEmpty(serialized))
    {
      ultrasonic.status = 0;
      ultrasonic.Distance = 0.0;
      ultrasonic.Depth = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      ultrasonic.Distance = strArray[0].ToDouble();
      if (strArray.Length > 1)
      {
        ultrasonic.Depth = strArray[1].ToDouble();
        try
        {
          ultrasonic.status = strArray[2].ToInt();
        }
        catch
        {
          ultrasonic.status = 0;
        }
      }
      else
      {
        ultrasonic.Distance = strArray[0].ToDouble();
        ultrasonic.Depth = strArray[0].ToDouble();
        ultrasonic.status = 0;
      }
    }
    return ultrasonic;
  }

  public static Ultrasonic.DistanceUnits GetUnits(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "DistanceUnits")
        return (Ultrasonic.DistanceUnits) Enum.Parse(typeof (Ultrasonic.DistanceUnits), sensorAttribute.Value);
    }
    return Ultrasonic.DistanceUnits.Centimeter;
  }

  public static void SetUnits(long sensorID, Ultrasonic.DistanceUnits units)
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

  public Ultrasonic.DistanceUnits Units
  {
    get
    {
      return this._Units != null ? (Ultrasonic.DistanceUnits) Enum.Parse(typeof (Ultrasonic.DistanceUnits), this._Units.Value) : Ultrasonic.DistanceUnits.Centimeter;
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
        sensorAttribute.Value = TankDepth.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = nameof (TankDepth),
        Value = TankDepth.ToString(),
        SensorID = sensorID
      }.Save();
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
      if (this.Distance > 400.0)
        return (object) null;
      double num = this.Distance;
      if (this.ShowDepth != null && this.ShowDepth.Value.ToBool())
        num = this.TankDepth.Value.ToDouble() - this.Distance;
      switch (this.Units)
      {
        case Ultrasonic.DistanceUnits.Meter:
          return (object) Math.Round(num * 0.01, 1);
        case Ultrasonic.DistanceUnits.Inch:
          return (object) Math.Round(num * 0.393701, 1);
        case Ultrasonic.DistanceUnits.Feet:
          return (object) Math.Round(num * 0.0328084, 1);
        case Ultrasonic.DistanceUnits.Yard:
          return (object) Math.Round(num * 0.0109361, 1);
        default:
          return (object) Math.Round(num, 1);
      }
    }
  }

  public static string AbreviatedMesaurement(Ultrasonic.DistanceUnits unit)
  {
    string str;
    switch (unit)
    {
      case Ultrasonic.DistanceUnits.Meter:
        str = "M";
        break;
      case Ultrasonic.DistanceUnits.Inch:
        str = "in";
        break;
      case Ultrasonic.DistanceUnits.Feet:
        str = "ft";
        break;
      case Ultrasonic.DistanceUnits.Yard:
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
    switch (Ultrasonic.AbreviatedMesaurement(Ultrasonic.GetUnits(sensor.SensorID)))
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
        str = this.Distance > 10.0 ? (this.Distance < 400.0 ? " - Measurement Out of Range:" : " - Measurement Out of Range: Too Far") : " - Measurement Out of Range: Too Close";
      else if (this.status == 8)
        str = " - Readings Unstable";
      return $"{this.PlotValue} {Ultrasonic.AbreviatedMesaurement(this.Units)} {str}";
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
    notification.ApplicationID = Ultrasonic.MonnitApplicationID;
    notification.SnoozeDuration = 60;
    notification.Version = MonnitApplicationBase.NotificationVersion;
    switch (Ultrasonic.GetUnits(sensor.SensorID))
    {
      case Ultrasonic.DistanceUnits.Meter:
        notification.Scale = "Meter";
        break;
      case Ultrasonic.DistanceUnits.Inch:
        notification.Scale = "Inch";
        break;
      case Ultrasonic.DistanceUnits.Feet:
        notification.Scale = "Feet";
        break;
      case Ultrasonic.DistanceUnits.Yard:
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
        "Centimeter",
        "Centimeters"
      },
      {
        "Meter",
        "Meters"
      },
      {
        "Inch",
        "Inches"
      },
      {
        "Feet",
        "Feet"
      },
      {
        "Yard",
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
      string str = Ultrasonic.GetUnits(sensor.SensorID).ToString();
      bool showDepth = Ultrasonic.GetShowDepth(sensor.SensorID);
      double tankDepth = Ultrasonic.GetTankDepth(sensor.SensorID);
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
        Ultrasonic.SetUnits(sensor.SensorID, Ultrasonic.DistanceUnits.Centimeter);
      }
      if (collection["MeasurementScale"] == "in")
      {
        viewData["MeasurementScale"] = "in";
        Ultrasonic.SetUnits(sensor.SensorID, Ultrasonic.DistanceUnits.Inch);
      }
      if (collection["MeasurementScale"] == "ft")
      {
        viewData["MeasurementScale"] = "ft";
        Ultrasonic.SetUnits(sensor.SensorID, Ultrasonic.DistanceUnits.Feet);
      }
      if (collection["MeasurementScale"] == "yrd")
      {
        viewData["MeasurementScale"] = "yd";
        Ultrasonic.SetUnits(sensor.SensorID, Ultrasonic.DistanceUnits.Yard);
      }
      if (collection["MeasurementScale"] == "M")
      {
        viewData["MeasurementScale"] = "M";
        Ultrasonic.SetUnits(sensor.SensorID, Ultrasonic.DistanceUnits.Meter);
      }
    }
    if (!string.IsNullOrEmpty(collection["ShowDepth"]))
      Ultrasonic.SetShowDepth(sensor.SensorID, collection["ShowDepth"].ToBool());
    if (string.IsNullOrEmpty(collection["TankDepth"]))
      return;
    Ultrasonic.DistanceUnits units = Ultrasonic.GetUnits(sensor.SensorID);
    double TankDepth = collection["TankDepth"].ToDouble();
    switch (units)
    {
      case Ultrasonic.DistanceUnits.Meter:
        TankDepth *= 100.0;
        break;
      case Ultrasonic.DistanceUnits.Inch:
        TankDepth *= 2.54;
        break;
      case Ultrasonic.DistanceUnits.Feet:
        TankDepth *= 30.48;
        break;
      case Ultrasonic.DistanceUnits.Yard:
        TankDepth *= 91.44;
        break;
    }
    Ultrasonic.SetTankDepth(sensor.SensorID, TankDepth);
  }

  public static string HystForUI(Sensor sensor)
  {
    switch (Ultrasonic.AbreviatedMesaurement(Ultrasonic.GetUnits(sensor.SensorID)))
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
    switch (Ultrasonic.AbreviatedMesaurement(Ultrasonic.GetUnits(sensor.SensorID)))
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
    switch (Ultrasonic.AbreviatedMesaurement(Ultrasonic.GetUnits(sensor.SensorID)))
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
    double tankDepth = Ultrasonic.GetTankDepth(sensor.SensorID);
    switch (Ultrasonic.AbreviatedMesaurement(Ultrasonic.GetUnits(sensor.SensorID)))
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
    switch (Ultrasonic.AbreviatedMesaurement(Ultrasonic.GetUnits(sensor.SensorID)))
    {
      case "M":
        return (double) Ultrasonic.DefaultMinThreshold * 0.01;
      case "in":
        return (double) Ultrasonic.DefaultMinThreshold * 0.393701;
      case "ft":
        return (double) Ultrasonic.DefaultMinThreshold * 0.0328084;
      case "yrd":
        return (double) Ultrasonic.DefaultMinThreshold * 0.0109361;
      default:
        return (double) Ultrasonic.DefaultMinThreshold;
    }
  }

  public static double DefaultMaxThreshForUI(Sensor sensor)
  {
    switch (Ultrasonic.AbreviatedMesaurement(Ultrasonic.GetUnits(sensor.SensorID)))
    {
      case "M":
        return (double) Ultrasonic.DefaultMaxThreshold * 0.01;
      case "in":
        return (double) Ultrasonic.DefaultMaxThreshold * 0.393701;
      case "ft":
        return (double) Ultrasonic.DefaultMaxThreshold * 0.0328084;
      case "yrd":
        return (double) Ultrasonic.DefaultMaxThreshold * 0.0109361;
      default:
        return (double) Ultrasonic.DefaultMaxThreshold;
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (string.IsNullOrEmpty(collection["actual"]) || string.IsNullOrEmpty(collection["observed"]))
      return;
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
    Ultrasonic.SetQueueID(sensor.SensorID);
    Ultrasonic.SetCalibrationValues(sensor.SensorID, actual, observed, Ultrasonic.GetQueueID(sensor.SensorID));
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
      string[] strArray = Ultrasonic.GetCalibrationValues(sensor.SensorID).Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      numArrayList.Add(UltrasonicBase.CalibrateFrame(sensor.SensorID, strArray[0].ToInt(), strArray[1].ToInt(), strArray[2].ToInt()));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Ultrasonic left, Ultrasonic right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Ultrasonic left, Ultrasonic right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Ultrasonic left, Ultrasonic right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Ultrasonic left, Ultrasonic right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Ultrasonic left, Ultrasonic right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Ultrasonic left, Ultrasonic right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Ultrasonic && this.Equals((MonnitApplicationBase) (obj as Ultrasonic));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Ultrasonic && this.Distance == (right as Ultrasonic).Distance;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Ultrasonic && this.Distance != (right as Ultrasonic).Distance;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Ultrasonic && this.Distance < (right as Ultrasonic).Distance;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Ultrasonic && this.Distance <= (right as Ultrasonic).Distance;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Ultrasonic && this.Distance > (right as Ultrasonic).Distance;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Ultrasonic && this.Distance >= (right as Ultrasonic).Distance;
  }

  public static long DefaultMinThreshold => 10;

  public static long DefaultMaxThreshold => 400;

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
