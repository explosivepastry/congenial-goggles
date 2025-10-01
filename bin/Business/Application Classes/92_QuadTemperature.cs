// Decompiled with JetBrains decompiler
// Type: Monnit.QuadTemperature
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

public class QuadTemperature : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;
  private SensorAttribute _Probe1;
  private SensorAttribute _Probe2;
  private SensorAttribute _Probe3;
  private SensorAttribute _Probe4;

  public static long MonnitApplicationID => QuadTemperatureBase.MonnitApplicationID;

  public static string ApplicationName => "Quad Temperature";

  public static eApplicationProfileType ProfileType => QuadTemperatureBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => QuadTemperature.MonnitApplicationID;

  public double ProbeOne { get; set; }

  public double ProbeTwo { get; set; }

  public double ProbeThree { get; set; }

  public double ProbeFour { get; set; }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[4]
      {
        new AppDatum(eDatumType.TemperatureData, "Temperature 1", this.ProbeOne),
        new AppDatum(eDatumType.TemperatureData, "Temperature 2", this.ProbeTwo),
        new AppDatum(eDatumType.TemperatureData, "Temperature 3", this.ProbeThree),
        new AppDatum(eDatumType.TemperatureData, "Temperature 4", this.ProbeFour)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return this.CorF != null && this.CorF.Value == "C" ? new List<object>((IEnumerable<object>) new object[4]
    {
      (object) this.ProbeOne,
      (object) this.ProbeTwo,
      (object) this.ProbeThree,
      (object) this.ProbeFour
    }) : new List<object>((IEnumerable<object>) new object[4]
    {
      (object) this.ProbeOne.ToFahrenheit(),
      (object) this.ProbeTwo.ToFahrenheit(),
      (object) this.ProbeThree.ToFahrenheit(),
      (object) this.ProbeFour.ToFahrenheit()
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    string str = QuadTemperature.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius";
    return new List<string>((IEnumerable<string>) new string[4]
    {
      str,
      str,
      str,
      str
    });
  }

  public static QuadTemperature Create(byte[] sdm, int startIndex)
  {
    return new QuadTemperature()
    {
      ProbeOne = Convert.ToDouble(BitConverter.ToInt16(sdm, startIndex)) / 10.0,
      ProbeTwo = Convert.ToDouble(BitConverter.ToInt16(sdm, startIndex + 2)) / 10.0,
      ProbeThree = Convert.ToDouble(BitConverter.ToInt16(sdm, startIndex + 4)) / 10.0,
      ProbeFour = Convert.ToDouble(BitConverter.ToInt16(sdm, startIndex + 6)) / 10.0
    };
  }

  public override string Serialize()
  {
    return $"{this.ProbeOne}, {this.ProbeTwo}, {this.ProbeThree}, {this.ProbeFour}";
  }

  public static QuadTemperature Deserialize(string version, string serialized)
  {
    QuadTemperature quadTemperature = new QuadTemperature();
    if (!string.IsNullOrWhiteSpace(serialized))
    {
      string[] strArray = serialized.Split(',');
      if (strArray.Length < 4)
      {
        quadTemperature.ProbeOne = strArray[0].ToDouble();
        quadTemperature.ProbeTwo = strArray[0].ToDouble();
        quadTemperature.ProbeThree = strArray[0].ToDouble();
        quadTemperature.ProbeFour = strArray[0].ToDouble();
      }
      else
      {
        quadTemperature.ProbeOne = strArray[0].ToDouble();
        quadTemperature.ProbeTwo = strArray[1].ToDouble();
        quadTemperature.ProbeThree = strArray[2].ToDouble();
        quadTemperature.ProbeFour = strArray[3].ToDouble();
      }
    }
    else
    {
      quadTemperature.ProbeOne = 0.0;
      quadTemperature.ProbeTwo = 0.0;
      quadTemperature.ProbeThree = 0.0;
      quadTemperature.ProbeFour = 0.0;
    }
    return quadTemperature;
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) QuadTemperature.Deserialize(version, serialized);
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

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
      if (sensorAttribute.Name == "DatumIndex|0")
        this._Probe1 = sensorAttribute;
      if (sensorAttribute.Name == "DatumIndex|1")
        this._Probe2 = sensorAttribute;
      if (sensorAttribute.Name == "DatumIndex|2")
        this._Probe3 = sensorAttribute;
      if (sensorAttribute.Name == "DatumIndex|3")
        this._Probe4 = sensorAttribute;
    }
  }

  public SensorAttribute Probe1Name
  {
    get
    {
      if (this._Probe1 == null)
        this._Probe1 = new SensorAttribute()
        {
          Name = "DatumIndex|0",
          Value = "Probe One"
        };
      return this._Probe1;
    }
  }

  public SensorAttribute Probe2Name
  {
    get
    {
      if (this._Probe2 == null)
        this._Probe2 = new SensorAttribute()
        {
          Name = "DatumIndex|1",
          Value = "Probe Two"
        };
      return this._Probe2;
    }
  }

  public SensorAttribute Probe3Name
  {
    get
    {
      if (this._Probe3 == null)
        this._Probe3 = new SensorAttribute()
        {
          Name = "DatumIndex|2",
          Value = "Probe Three"
        };
      return this._Probe3;
    }
  }

  public SensorAttribute Probe4Name
  {
    get
    {
      if (this._Probe4 == null)
        this._Probe4 = new SensorAttribute()
        {
          Name = "DatumIndex|3",
          Value = "Probe Four"
        };
      return this._Probe4;
    }
  }

  public static string GetProbe4Label(long sensorID) => Sensor.GetDatumName(sensorID, 3);

  public static string GetProbe3Label(long sensorID) => Sensor.GetDatumName(sensorID, 2);

  public static string GetProbe2Label(long sensorID) => Sensor.GetDatumName(sensorID, 1);

  public static string GetProbe1Label(long sensorID) => Sensor.GetDatumName(sensorID, 0);

  public static void SetProbe4Label(long sensorID, string name)
  {
    Sensor.Load(sensorID).SetDatumName(3, name);
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void SetProbe3Label(long sensorID, string name)
  {
    Sensor.Load(sensorID).SetDatumName(2, name);
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void SetProbe2Label(long sensorID, string name)
  {
    Sensor.Load(sensorID).SetDatumName(1, name);
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void SetProbe1Label(long sensorID, string name)
  {
    Sensor.Load(sensorID).SetDatumName(0, name);
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public override object PlotValue
  {
    get
    {
      if (this.ProbeOne <= -100.0 || this.ProbeOne >= 300.0)
        return (object) null;
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.ProbeOne : (object) this.ProbeOne.ToFahrenheit();
    }
  }

  public object Probe1PlotValue
  {
    get
    {
      if (this.ProbeOne <= -100.0 || this.ProbeOne >= 300.0)
        return (object) null;
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.ProbeOne : (object) this.ProbeOne.ToFahrenheit();
    }
  }

  public object Probe2PlotValue
  {
    get
    {
      if (this.ProbeTwo <= -100.0 || this.ProbeTwo >= 300.0)
        return (object) null;
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.ProbeTwo : (object) this.ProbeTwo.ToFahrenheit();
    }
  }

  public object Probe3PlotValue
  {
    get
    {
      if (this.ProbeThree <= -100.0 || this.ProbeThree >= 300.0)
        return (object) null;
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.ProbeThree : (object) this.ProbeThree.ToFahrenheit();
    }
  }

  public object Probe4PlotValue
  {
    get
    {
      if (this.ProbeFour <= -100.0 || this.ProbeFour >= 300.0)
        return (object) null;
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.ProbeFour : (object) this.ProbeFour.ToFahrenheit();
    }
  }

  public override bool IsValid
  {
    get
    {
      return this.ProbeOne > -100.0 && this.ProbeOne < 300.0 && this.ProbeTwo > -100.0 && this.ProbeTwo < 300.0 && this.ProbeThree > -100.0 && this.ProbeThree < 300.0 && this.ProbeFour > -100.0 && this.ProbeFour < 300.0;
    }
  }

  public override string NotificationString
  {
    get
    {
      string empty = string.Empty;
      string str1;
      double num;
      if (this.ProbeOne > -100.0 && this.ProbeOne < 300.0)
      {
        if (this.CorF != null && this.CorF.Value == "C")
        {
          str1 = empty + $" {this.Probe1Name.Value} {this.ProbeOne.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture)},";
        }
        else
        {
          string str2 = empty;
          string str3 = this.Probe1Name.Value;
          num = this.ProbeOne.ToFahrenheit();
          string str4 = num.ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture);
          string str5 = $" {str3} {str4},";
          str1 = str2 + str5;
        }
      }
      else
        str1 = $"{empty}{this.Probe1Name.Value}: Probe Failure, ";
      string str6;
      if (this.ProbeTwo > -100.0 && this.ProbeTwo < 300.0)
      {
        if (this.CorF != null && this.CorF.Value == "C")
        {
          string str7 = str1;
          string str8 = this.Probe2Name.Value;
          num = this.ProbeTwo;
          string str9 = num.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
          string str10 = $" {str8} {str9},";
          str6 = str7 + str10;
        }
        else
        {
          string str11 = str1;
          string str12 = this.Probe2Name.Value;
          num = this.ProbeTwo.ToFahrenheit();
          string str13 = num.ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture);
          string str14 = $" {str12} {str13},";
          str6 = str11 + str14;
        }
      }
      else
        str6 = $"{str1}{this.Probe2Name.Value}: Probe Failure, ";
      string str15;
      if (this.ProbeThree > -100.0 && this.ProbeThree < 300.0)
      {
        if (this.CorF != null && this.CorF.Value == "C")
        {
          string str16 = str6;
          string str17 = this.Probe3Name.Value;
          num = this.ProbeThree;
          string str18 = num.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
          string str19 = $" {str17} {str18},";
          str15 = str16 + str19;
        }
        else
        {
          string str20 = str6;
          string str21 = this.Probe3Name.Value;
          num = this.ProbeThree.ToFahrenheit();
          string str22 = num.ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture);
          string str23 = $" {str21} {str22},";
          str15 = str20 + str23;
        }
      }
      else
        str15 = $"{str6}{this.Probe3Name.Value}: Probe Failure, ";
      string notificationString;
      if (this.ProbeFour > -100.0 && this.ProbeFour < 300.0)
      {
        if (this.CorF != null && this.CorF.Value == "C")
        {
          string str24 = str15;
          string str25 = this.Probe4Name.Value;
          num = this.ProbeFour;
          string str26 = num.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
          string str27 = $" {str25} {str26}";
          notificationString = str24 + str27;
        }
        else
        {
          string str28 = str15;
          string str29 = this.Probe4Name.Value;
          num = this.ProbeFour.ToFahrenheit();
          string str30 = num.ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture);
          string str31 = $" {str29} {str30}";
          notificationString = str28 + str31;
        }
      }
      else
        notificationString = $"{str15}{this.Probe4Name.Value}: Probe Failure ";
      return notificationString;
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
    sensor.MaximumNetworkHops = sensor.DefaultValue<int>("DefaultMaximumNetworkHops");
    sensor.MinimumCommunicationFrequency = (int) (sensor.ReportInterval * 2.0) + 10;
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }

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

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Less_Than,
      CompareValue = "0",
      NotificationClass = eNotificationClass.Application,
      ApplicationID = QuadTemperature.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion,
      Scale = QuadTemperature.IsFahrenheit(sensor.SensorID) ? "F" : "C"
    };
  }

  public static Dictionary<string, string> NotificationScaleValues()
  {
    return new Dictionary<string, string>()
    {
      {
        "F",
        "Fahrenheit"
      },
      {
        "C",
        "Celsius"
      }
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    bool flag = QuadTemperature.IsFahrenheit(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["BasicThreshold"]) && !string.IsNullOrEmpty(collection["BasicThresholdDirection"]) && collection["BasicThresholdDirection"] != "-1")
    {
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
      if (collection["BasicThresholdDirection"].ToInt() == 0)
      {
        sensor.MinimumThreshold = ((flag ? collection["BasicThreshold"].ToDouble().ToCelsius() : collection["BasicThreshold"].ToDouble()) * 10.0).ToLong();
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      }
      else if (collection["BasicThresholdDirection"].ToInt() == 1)
      {
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
        sensor.MaximumThreshold = ((flag ? collection["BasicThreshold"].ToDouble().ToCelsius() : collection["BasicThreshold"].ToDouble()) * 10.0).ToLong();
      }
      sensor.MeasurementsPerTransmission = 6;
    }
    else
    {
      double result;
      if ((string.IsNullOrEmpty(collection["HystFourthByte_Manual"]) || !double.TryParse(collection["HystFourthByte_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("HystFourthByte_Manual"))
        QuadTemperature.SetHystFourthByte(sensor, (sbyte) 0);
      if ((string.IsNullOrEmpty(collection["HystThirdByte_Manual"]) || !double.TryParse(collection["HystThirdByte_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("HystThirdByte_Manual"))
        QuadTemperature.SetHystThirdByte(sensor, (sbyte) 0);
      if ((string.IsNullOrEmpty(collection["HystSecondByte_Manual"]) || !double.TryParse(collection["HystSecondByte_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("HystSecondByte_Manual"))
        QuadTemperature.SetHystSecondByte(sensor, (sbyte) 0);
      if ((string.IsNullOrEmpty(collection["HystFirstByte_Manual"]) || !double.TryParse(collection["HystFirstByte_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("HystFirstByte_Manual"))
        QuadTemperature.SetHystFirstByte(sensor, (sbyte) 0);
      if ((string.IsNullOrEmpty(collection["MaxFourthByte_Manual"]) || !double.TryParse(collection["MaxFourthByte_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaxFourthByte_Manual"))
        QuadTemperature.SetMaxFourthByte(sensor, (sbyte) 125);
      if ((string.IsNullOrEmpty(collection["MaxThirdByte_Manual"]) || !double.TryParse(collection["MaxThirdByte_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaxThirdByte_Manual"))
        QuadTemperature.SetMaxThirdByte(sensor, (sbyte) 125);
      if ((string.IsNullOrEmpty(collection["MaxSecondByte_Manual"]) || !double.TryParse(collection["MaxSecondByte_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaxSecondByte_Manual"))
        QuadTemperature.SetMaxSecondByte(sensor, (sbyte) 125);
      if ((string.IsNullOrEmpty(collection["MaxFirstByte_Manual"]) || !double.TryParse(collection["MaxFirstByte_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaxFirstByte_Manual"))
        QuadTemperature.SetMaxFirstByte(sensor, (sbyte) 125);
      if ((string.IsNullOrEmpty(collection["MinFourthByte_Manual"]) || !double.TryParse(collection["MinFourthByte_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinFourthByte_Manual"))
        QuadTemperature.SetMinFourthByte(sensor, (sbyte) -40);
      if ((string.IsNullOrEmpty(collection["MinThirdByte_Manual"]) || !double.TryParse(collection["MinThirdByte_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinThirdByte_Manual"))
        QuadTemperature.SetMinThirdByte(sensor, (sbyte) -40);
      if ((string.IsNullOrEmpty(collection["MinSecondByte_Manual"]) || !double.TryParse(collection["MinSecondByte_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinSecondByte_Manual"))
        QuadTemperature.SetMinSecondByte(sensor, (sbyte) -40);
      if ((string.IsNullOrEmpty(collection["MinFirstByte_Manual"]) || !double.TryParse(collection["MinFirstByte_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinFirstByte_Manual"))
        QuadTemperature.SetMinFirstByte(sensor, (sbyte) -40);
      if (!string.IsNullOrEmpty(collection["HystFourthByte_Manual"]) && double.TryParse(collection["HystFourthByte_Manual"], out result))
        QuadTemperature.SetHystFourthByte(sensor, (sbyte) (collection["HystFourthByte_Manual"].ToDouble() * (flag ? 5.0 / 9.0 : 1.0) * 10.0));
      if (!string.IsNullOrEmpty(collection["HystThirdByte_Manual"]) && double.TryParse(collection["HystThirdByte_Manual"], out result))
        QuadTemperature.SetHystThirdByte(sensor, (sbyte) (collection["HystThirdByte_Manual"].ToDouble() * (flag ? 5.0 / 9.0 : 1.0) * 10.0));
      if (!string.IsNullOrEmpty(collection["HystSecondByte_Manual"]) && double.TryParse(collection["HystSecondByte_Manual"], out result))
        QuadTemperature.SetHystSecondByte(sensor, (sbyte) (collection["HystSecondByte_Manual"].ToDouble() * (flag ? 5.0 / 9.0 : 1.0) * 10.0));
      if (!string.IsNullOrEmpty(collection["HystFirstByte_Manual"]) && double.TryParse(collection["HystFirstByte_Manual"], out result))
        QuadTemperature.SetHystFirstByte(sensor, (sbyte) (collection["HystFirstByte_Manual"].ToDouble() * (flag ? 5.0 / 9.0 : 1.0) * 10.0));
      if (!string.IsNullOrEmpty(collection["MinFourthByte_Manual"]) && double.TryParse(collection["MinFourthByte_Manual"], out result))
      {
        if (flag)
        {
          if (collection["MinFourthByte_Manual"].ToInt() < -40)
            collection["MinFourthByte_Manual"] = "-40";
          if (collection["MinFourthByte_Manual"].ToInt() > 257)
            collection["MinFourthByte_Manual"] = "257";
          QuadTemperature.SetMinFourthByte(sensor, (sbyte) collection["MinFourthByte_Manual"].ToDouble().ToCelsius().ToInt());
        }
        else
        {
          if (collection["MinFourthByte_Manual"].ToInt() < -40)
            collection["MinFourthByte_Manual"] = "-40";
          if (collection["MinFourthByte_Manual"].ToInt() > 125)
            collection["MinFourthByte_Manual"] = "125";
          QuadTemperature.SetMinFourthByte(sensor, (sbyte) collection["MinFourthByte_Manual"].ToDouble().ToInt());
        }
      }
      if (!string.IsNullOrEmpty(collection["MinThirdByte_Manual"]) && double.TryParse(collection["MinThirdByte_Manual"], out result))
      {
        if (flag)
        {
          if (collection["MinThirdByte_Manual"].ToInt() < -40)
            collection["MinThirdByte_Manual"] = "-40";
          if (collection["MinThirdByte_Manual"].ToInt() > 257)
            collection["MinThirdByte_Manual"] = "257";
          QuadTemperature.SetMinThirdByte(sensor, (sbyte) collection["MinThirdByte_Manual"].ToDouble().ToCelsius().ToInt());
        }
        else
        {
          if (collection["MinThirdByte_Manual"].ToInt() < -40)
            collection["MinThirdByte_Manual"] = "-40";
          if (collection["MinThirdByte_Manual"].ToInt() > 125)
            collection["MinThirdByte_Manual"] = "125";
          QuadTemperature.SetMinThirdByte(sensor, (sbyte) collection["MinThirdByte_Manual"].ToDouble().ToInt());
        }
      }
      if (!string.IsNullOrEmpty(collection["MinSecondByte_Manual"]) && double.TryParse(collection["MinSecondByte_Manual"], out result))
      {
        if (flag)
        {
          if (collection["MinSecondByte_Manual"].ToInt() < -40)
            collection["MinSecondByte_Manual"] = "-40";
          if (collection["MinSecondByte_Manual"].ToInt() > 257)
            collection["MinSecondByte_Manual"] = "257";
          QuadTemperature.SetMinSecondByte(sensor, (sbyte) collection["MinSecondByte_Manual"].ToDouble().ToCelsius().ToInt());
        }
        else
        {
          if (collection["MinSecondByte_Manual"].ToInt() < -40)
            collection["MinSecondByte_Manual"] = "-40";
          if (collection["MinSecondByte_Manual"].ToInt() > 125)
            collection["MinSecondByte_Manual"] = "125";
          QuadTemperature.SetMinSecondByte(sensor, (sbyte) collection["MinSecondByte_Manual"].ToDouble().ToInt());
        }
      }
      if (!string.IsNullOrEmpty(collection["MinFirstByte_Manual"]) && double.TryParse(collection["MinFirstByte_Manual"], out result))
      {
        if (flag)
        {
          if (collection["MinFirstByte_Manual"].ToInt() < -40)
            collection["MinFirstByte_Manual"] = "-40";
          if (collection["MinFirstByte_Manual"].ToInt() > 257)
            collection["MinFirstByte_Manual"] = "257";
          QuadTemperature.SetMinFirstByte(sensor, (sbyte) collection["MinFirstByte_Manual"].ToDouble().ToCelsius().ToInt());
        }
        else
        {
          if (collection["MinFirstByte_Manual"].ToInt() < -40)
            collection["MinFirstByte_Manual"] = "-40";
          if (collection["MinFirstByte_Manual"].ToInt() > 125)
            collection["MinFirstByte_Manual"] = "125";
          QuadTemperature.SetMinFirstByte(sensor, (sbyte) collection["MinFirstByte_Manual"].ToDouble().ToInt());
        }
      }
      if (!string.IsNullOrEmpty(collection["MaxFourthByte_Manual"]) && double.TryParse(collection["MaxFourthByte_Manual"], out result))
      {
        if (flag)
        {
          if (collection["MaxFourthByte_Manual"].ToInt() < -40)
            collection["MaxFourthByte_Manual"] = "-40";
          if (collection["MaxFourthByte_Manual"].ToInt() > 257)
            collection["MaxFourthByte_Manual"] = "257";
          QuadTemperature.SetMaxFourthByte(sensor, (sbyte) collection["MaxFourthByte_Manual"].ToDouble().ToCelsius().ToInt());
          if ((int) QuadTemperature.GetMaxFourthByte(sensor) < (int) QuadTemperature.GetMinFourthByte(sensor))
            QuadTemperature.SetMaxFourthByte(sensor, QuadTemperature.GetMinFourthByte(sensor));
        }
        else
        {
          if (collection["MaxFourthByte_Manual"].ToInt() < -40)
            collection["MaxFourthByte_Manual"] = "-40";
          if (collection["MaxFourthByte_Manual"].ToInt() > 125)
            collection["MaxFourthByte_Manual"] = "125";
          QuadTemperature.SetMaxFourthByte(sensor, (sbyte) collection["MaxFourthByte_Manual"].ToDouble().ToInt());
          if ((int) QuadTemperature.GetMaxFourthByte(sensor) < (int) QuadTemperature.GetMinFourthByte(sensor))
            QuadTemperature.SetMaxFourthByte(sensor, QuadTemperature.GetMinFourthByte(sensor));
        }
      }
      if (!string.IsNullOrEmpty(collection["MaxThirdByte_Manual"]) && double.TryParse(collection["MaxThirdByte_Manual"], out result))
      {
        if (flag)
        {
          if (collection["MaxThirdByte_Manual"].ToInt() < -40)
            collection["MaxThirdByte_Manual"] = "-40";
          if (collection["MaxThirdByte_Manual"].ToInt() > 257)
            collection["MaxThirdByte_Manual"] = "257";
          QuadTemperature.SetMaxThirdByte(sensor, (sbyte) collection["MaxThirdByte_Manual"].ToDouble().ToCelsius().ToInt());
          if ((int) QuadTemperature.GetMaxThirdByte(sensor) < (int) QuadTemperature.GetMinThirdByte(sensor))
            QuadTemperature.SetMaxThirdByte(sensor, QuadTemperature.GetMinThirdByte(sensor));
        }
        else
        {
          if (collection["MaxThirdByte_Manual"].ToInt() < -40)
            collection["MaxThirdByte_Manual"] = "-40";
          if (collection["MaxThirdByte_Manual"].ToInt() > 125)
            collection["MaxThirdByte_Manual"] = "125";
          QuadTemperature.SetMaxThirdByte(sensor, (sbyte) collection["MaxThirdByte_Manual"].ToDouble().ToInt());
          if ((int) QuadTemperature.GetMaxThirdByte(sensor) < (int) QuadTemperature.GetMinThirdByte(sensor))
            QuadTemperature.SetMaxThirdByte(sensor, QuadTemperature.GetMinThirdByte(sensor));
        }
      }
      if (!string.IsNullOrEmpty(collection["MaxSecondByte_Manual"]) && double.TryParse(collection["MaxSecondByte_Manual"], out result))
      {
        if (flag)
        {
          if (collection["MaxSecondByte_Manual"].ToInt() < -40)
            collection["MaxSecondByte_Manual"] = "-40";
          if (collection["MaxSecondByte_Manual"].ToInt() > 257)
            collection["MaxSecondByte_Manual"] = "257";
          QuadTemperature.SetMaxSecondByte(sensor, (sbyte) collection["MaxSecondByte_Manual"].ToDouble().ToCelsius().ToInt());
          if ((int) QuadTemperature.GetMaxSecondByte(sensor) < (int) QuadTemperature.GetMinSecondByte(sensor))
            QuadTemperature.SetMaxSecondByte(sensor, QuadTemperature.GetMinSecondByte(sensor));
        }
        else
        {
          if (collection["MaxSecondByte_Manual"].ToInt() < -40)
            collection["MaxSecondByte_Manual"] = "-40";
          if (collection["MaxSecondByte_Manual"].ToInt() > 125)
            collection["MaxSecondByte_Manual"] = "125";
          QuadTemperature.SetMaxSecondByte(sensor, (sbyte) collection["MaxSecondByte_Manual"].ToDouble().ToInt());
          if ((int) QuadTemperature.GetMaxSecondByte(sensor) < (int) QuadTemperature.GetMinSecondByte(sensor))
            QuadTemperature.SetMaxSecondByte(sensor, QuadTemperature.GetMinSecondByte(sensor));
        }
      }
      if (!string.IsNullOrEmpty(collection["MaxFirstByte_Manual"]) && double.TryParse(collection["MaxFirstByte_Manual"], out result))
      {
        if (flag)
        {
          if (collection["MaxFirstByte_Manual"].ToInt() < -40)
            collection["MaxFirstByte_Manual"] = "-40";
          if (collection["MaxFirstByte_Manual"].ToInt() > 257)
            collection["MaxFirstByte_Manual"] = "257";
          QuadTemperature.SetMaxFirstByte(sensor, (sbyte) collection["MaxFirstByte_Manual"].ToDouble().ToCelsius().ToInt());
          if ((int) QuadTemperature.GetMaxFirstByte(sensor) < (int) QuadTemperature.GetMinFirstByte(sensor))
            QuadTemperature.SetMaxFirstByte(sensor, QuadTemperature.GetMinFirstByte(sensor));
        }
        else
        {
          if (collection["MaxFirstByte_Manual"].ToInt() < -40)
            collection["MaxFirstByte_Manual"] = "-40";
          if (collection["MaxFirstByte_Manual"].ToInt() > 125)
            collection["MaxFirstByte_Manual"] = "125";
          QuadTemperature.SetMaxFirstByte(sensor, (sbyte) collection["MaxFirstByte_Manual"].ToDouble().ToInt());
          if ((int) QuadTemperature.GetMaxFirstByte(sensor) < (int) QuadTemperature.GetMinFirstByte(sensor))
            QuadTemperature.SetMaxFirstByte(sensor, QuadTemperature.GetMinFirstByte(sensor));
        }
      }
    }
  }

  public static void SetHystFourthByte(Sensor sensor, sbyte value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 16777215U /*0xFFFFFF*/;
    sensor.Hysteresis = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 24));
  }

  public static string GetHystFourthByte(Sensor sensor)
  {
    double num = Convert.ToDouble((Convert.ToUInt32(sensor.Hysteresis) & 4278190080U /*0xFF000000*/) >> 24) / 10.0;
    return Temperature.IsFahrenheit(sensor.SensorID) ? (num * 1.8).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static void SetHystThirdByte(Sensor sensor, sbyte value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4278255615U;
    sensor.Hysteresis = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static string GetHystThirdByte(Sensor sensor)
  {
    double num = Convert.ToDouble((Convert.ToUInt32(sensor.Hysteresis) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/) / 10.0;
    return Temperature.IsFahrenheit(sensor.SensorID) ? (num * 1.8).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static void SetHystSecondByte(Sensor sensor, sbyte value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4294902015U;
    sensor.Hysteresis = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 8));
  }

  public static string GetHystSecondByte(Sensor sensor)
  {
    double num = Convert.ToDouble((Convert.ToUInt32(sensor.Hysteresis) & 65280U) >> 8) / 10.0;
    return Temperature.IsFahrenheit(sensor.SensorID) ? (num * 1.8).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static void SetHystFirstByte(Sensor sensor, sbyte value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4294967040U;
    sensor.Hysteresis = (long) (num | (uint) value & (uint) byte.MaxValue);
  }

  public static string GetHystFirstByte(Sensor sensor)
  {
    double num = Convert.ToDouble(Convert.ToUInt32(sensor.Hysteresis) & (uint) byte.MaxValue) / 10.0;
    return Temperature.IsFahrenheit(sensor.SensorID) ? (num * 1.8).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static void SetMaxFourthByte(Sensor sensor, sbyte value)
  {
    uint uint32 = Convert.ToUInt32(sensor.MaximumThreshold & 16777215L /*0xFFFFFF*/);
    sensor.MaximumThreshold = (long) (uint32 | (uint) (((int) value & (int) byte.MaxValue) << 24));
  }

  public static sbyte GetMaxFourthByte(Sensor sensor)
  {
    return (sbyte) Convert.ToByte(Convert.ToUInt32(sensor.MaximumThreshold & 4278190080L /*0xFF000000*/) >> 24);
  }

  public static void SetMaxThirdByte(Sensor sensor, sbyte value)
  {
    uint uint32 = Convert.ToUInt32(sensor.MaximumThreshold & 4278255615L);
    sensor.MaximumThreshold = (long) (uint32 | (uint) (((int) value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static sbyte GetMaxThirdByte(Sensor sensor)
  {
    return (sbyte) Convert.ToByte((Convert.ToUInt32(sensor.MaximumThreshold) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetMaxSecondByte(Sensor sensor, sbyte value)
  {
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 4294902015U;
    sensor.MaximumThreshold = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 8));
  }

  public static sbyte GetMaxSecondByte(Sensor sensor)
  {
    return (sbyte) Convert.ToByte((Convert.ToUInt32(sensor.MaximumThreshold) & 65280U) >> 8);
  }

  public static void SetMaxFirstByte(Sensor sensor, sbyte value)
  {
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 4294967040U;
    sensor.MaximumThreshold = (long) (num | (uint) value & (uint) byte.MaxValue);
  }

  public static sbyte GetMaxFirstByte(Sensor sensor)
  {
    return (sbyte) Convert.ToByte(Convert.ToUInt32(sensor.MaximumThreshold) & (uint) byte.MaxValue);
  }

  public static void SetMinFourthByte(Sensor sensor, sbyte value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 16777215U /*0xFFFFFF*/;
    sensor.MinimumThreshold = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 24));
  }

  public static sbyte GetMinFourthByte(Sensor sensor)
  {
    return (sbyte) Convert.ToByte((Convert.ToUInt32(sensor.MinimumThreshold) & 4278190080U /*0xFF000000*/) >> 24);
  }

  public static void SetMinThirdByte(Sensor sensor, sbyte value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4278255615U;
    sensor.MinimumThreshold = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static sbyte GetMinThirdByte(Sensor sensor)
  {
    return (sbyte) Convert.ToByte((Convert.ToUInt32(sensor.MinimumThreshold) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetMinSecondByte(Sensor sensor, sbyte value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4294902015U;
    sensor.MinimumThreshold = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 8));
  }

  public static sbyte GetMinSecondByte(Sensor sensor)
  {
    return (sbyte) Convert.ToByte((Convert.ToUInt32(sensor.MinimumThreshold) & 65280U) >> 8);
  }

  public static void SetMinFirstByte(Sensor sensor, sbyte value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4294967040U;
    sensor.MinimumThreshold = (long) (num | (uint) value & (uint) byte.MaxValue);
  }

  public static sbyte GetMinFirstByte(Sensor sensor)
  {
    return (sbyte) Convert.ToByte(Convert.ToUInt32(sensor.MinimumThreshold) & (uint) byte.MaxValue);
  }

  public static void SetCalVal1FourthByte(Sensor sensor, sbyte value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 16777215U /*0xFFFFFF*/;
    sensor.Calibration1 = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 24));
  }

  public static sbyte GetCalVal1FourthByte(Sensor sensor)
  {
    return (sbyte) Convert.ToByte((Convert.ToUInt32(sensor.Calibration1) & 4278190080U /*0xFF000000*/) >> 24);
  }

  public static void SetCalVal1ThirdByte(Sensor sensor, sbyte value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4278255615U;
    sensor.Calibration1 = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static sbyte GetCalVal1ThirdByte(Sensor sensor)
  {
    return (sbyte) Convert.ToByte((Convert.ToUInt32(sensor.Calibration1) & 16711680U /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetCalVal1SecondByte(Sensor sensor, sbyte value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4294902015U;
    sensor.Calibration1 = (long) (num | (uint) (((int) value & (int) byte.MaxValue) << 8));
  }

  public static sbyte GetCalVal1SecondByte(Sensor sensor)
  {
    return (sbyte) Convert.ToByte((Convert.ToUInt32(sensor.Calibration1) & 65280U) >> 8);
  }

  public static void SetCalVal1FirstByte(Sensor sensor, sbyte value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4294967040U;
    sensor.Calibration1 = (long) (num | (uint) value & (uint) byte.MaxValue);
  }

  public static sbyte GetCalVal1FirstByte(Sensor sensor)
  {
    return (sbyte) Convert.ToByte(Convert.ToUInt32(sensor.Calibration1) & (uint) byte.MaxValue);
  }

  public static string HystForUI(Sensor sensor)
  {
    double num = sensor.Hysteresis == (long) uint.MaxValue ? 0.0 : Convert.ToDouble(sensor.Hysteresis) / 10.0;
    return Temperature.IsFahrenheit(sensor.SensorID) ? (num * 1.8).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    double Celsius = sensor.MinimumThreshold == (long) uint.MaxValue ? Convert.ToDouble(sensor.DefaultValue<long>("DefaultMinimumThreshold")) / 10.0 : Convert.ToDouble(sensor.MinimumThreshold) / 10.0;
    return Temperature.IsFahrenheit(sensor.SensorID) ? Celsius.ToFahrenheit().ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : Celsius.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    double Celsius = sensor.MaximumThreshold == (long) uint.MaxValue ? Convert.ToDouble(sensor.DefaultValue<long>("DefaultMaximumThreshold")) / 10.0 : Convert.ToDouble(sensor.MaximumThreshold) / 10.0;
    return Temperature.IsFahrenheit(sensor.SensorID) ? Celsius.ToFahrenheit().ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) : Celsius.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection returnData)
  {
    if (collection["TempScale"] == "on")
    {
      returnData["TempScale"] = "F";
      QuadTemperature.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      returnData["TempScale"] = "C";
      QuadTemperature.MakeCelsius(sensor.SensorID);
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (!sensor.IsWiFiSensor)
    {
      if (collection["type"] == "basic")
      {
        double result = 0.0;
        if (!string.IsNullOrWhiteSpace(collection["probe1"]) && double.TryParse(collection["probe1"], out result))
          QuadTemperature.SetCalVal1FirstByte(sensor, (sbyte) collection["probe1"].ToInt());
        else
          QuadTemperature.SetCalVal1FirstByte(sensor, (sbyte) 0);
        if (!string.IsNullOrWhiteSpace(collection["probe2"]) && double.TryParse(collection["probe2"], out result))
          QuadTemperature.SetCalVal1SecondByte(sensor, (sbyte) collection["probe2"].ToInt());
        else
          QuadTemperature.SetCalVal1SecondByte(sensor, (sbyte) 0);
        if (!string.IsNullOrWhiteSpace(collection["probe3"]) && double.TryParse(collection["probe3"], out result))
          QuadTemperature.SetCalVal1ThirdByte(sensor, (sbyte) collection["probe3"].ToInt());
        else
          QuadTemperature.SetCalVal1ThirdByte(sensor, (sbyte) 0);
        if (!string.IsNullOrWhiteSpace(collection["probe4"]) && double.TryParse(collection["probe4"], out result))
          QuadTemperature.SetCalVal1FourthByte(sensor, (sbyte) collection["probe4"].ToInt());
        else
          QuadTemperature.SetCalVal1FourthByte(sensor, (sbyte) 0);
        sensor.ProfileConfigDirty = false;
        sensor.ProfileConfig2Dirty = true;
        sensor.PendingActionControlCommand = false;
      }
      else
      {
        sensor.Calibration1 = !(collection["tempScale"] == "C") ? ((collection["actual"].ToDouble() - 32.0) * (5.0 / 9.0)).ToLong() : collection["actual"].ToDouble().ToLong();
        sensor.ProfileConfigDirty = false;
        sensor.ProfileConfig2Dirty = false;
        if (sensor.Calibration1 < -40L || sensor.Calibration1 > 125L)
          throw new Exception("Calibration value out of range!");
        sensor.PendingActionControlCommand = true;
      }
    }
    else if (sensor.LastDataMessage != null)
    {
      double num1 = sensor.LastDataMessage.Data.ToDouble();
      double num2 = !(collection["tempScale"] == "C") ? (collection["actual"].ToDouble() - 32.0) * (5.0 / 9.0) : collection["actual"].ToDouble();
      sensor.Calibration1 = ((num2 - num1) * 10.0).ToLong() + sensor.Calibration1;
    }
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
      numArrayList.Add(QuadTemperatureBase.CalibrateFrame(sensor.SensorID, (double) sensor.Calibration1));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(QuadTemperature left, QuadTemperature right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(QuadTemperature left, QuadTemperature right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(QuadTemperature left, QuadTemperature right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(QuadTemperature left, QuadTemperature right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(QuadTemperature left, QuadTemperature right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(QuadTemperature left, QuadTemperature right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is QuadTemperature && this.Equals((MonnitApplicationBase) (obj as QuadTemperature));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is QuadTemperature && this.ProbeOne == (right as QuadTemperature).ProbeOne;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is QuadTemperature && this.ProbeOne != (right as QuadTemperature).ProbeOne;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is QuadTemperature && this.ProbeOne < (right as QuadTemperature).ProbeOne;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is QuadTemperature && this.ProbeOne <= (right as QuadTemperature).ProbeOne;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is QuadTemperature && this.ProbeOne > (right as QuadTemperature).ProbeOne;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is QuadTemperature && this.ProbeOne >= (right as QuadTemperature).ProbeOne;
  }

  public static long DefaultMinThreshold => -400;

  public static long DefaultMaxThreshold => 1250;
}
