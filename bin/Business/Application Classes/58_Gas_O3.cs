// Decompiled with JetBrains decompiler
// Type: Monnit.Gas_O3
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

public class Gas_O3 : MonnitApplicationBase
{
  private SensorAttribute _CorF;

  public static long MonnitApplicationID => 58;

  public static string ApplicationName => "O3 Gas";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => Gas_O3.MonnitApplicationID;

  public double Temp { get; set; }

  public double PPM { get; set; }

  public static bool IsFahrenheit(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF" && sensorAttribute.Value == "C")
        return false;
    }
    return true;
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      this.PlotValue,
      (object) (Gas_O3.IsFahrenheit(sensorID) ? this.Temp.ToFahrenheit() : this.Temp)
    };
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>()
    {
      "PPM",
      Gas_O3.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    };
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
    }
  }

  public override string Serialize()
  {
    double num = this.PPM;
    string str1 = num.ToString();
    num = this.Temp;
    string str2 = num.ToString();
    return $"{str1},{str2}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.PPM, "PPM", this.PPM),
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temp)
      });
    }
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Gas_O3.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      return this.Temp > -100.0 && this.Temp < 300.0 ? (this.CorF != null && this.CorF.Value == "C" ? this.Temp.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture) : $"{this.PPM.ToString()} PPM, {this.Temp.ToFahrenheit().ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture)}") : (this.Temp == -9998.0 ? "Reset Battery" : "Invalid - HW Problem");
    }
  }

  public override object PlotValue => (object) this.PPM;

  public static Gas_O3 Deserialize(string version, string serialized)
  {
    Gas_O3 gasO3 = new Gas_O3();
    if (string.IsNullOrEmpty(serialized))
    {
      gasO3.PPM = 0.0;
      gasO3.Temp = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      gasO3.PPM = (double) strArray[0].ToInt();
      gasO3.Temp = strArray.Length <= 1 ? 0.0 : strArray[1].ToDouble();
    }
    return gasO3;
  }

  public static Gas_O3 Create(byte[] sdm, int startIndex)
  {
    return new Gas_O3()
    {
      Temp = (double) BitConverter.ToInt16(sdm, startIndex) / 10.0,
      PPM = (double) BitConverter.ToUInt16(sdm, startIndex + 2) / 100.0
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
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = Gas_O3.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void VerifyNotificationValues(Notification notification, string scale)
  {
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    double result;
    if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
      sensor.Hysteresis = collection["Hysteresis_Manual"].ToLong() << 16 /*0x10*/;
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      sensor.MinimumThreshold = (collection["MinimumThreshold_Manual"].ToLong() << 16 /*0x10*/) + 65336L;
      if (sensor.MinimumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MinimumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    }
    if (string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
      return;
    sensor.MaximumThreshold = (collection["MaximumThreshold_Manual"].ToLong() << 16 /*0x10*/) + 500L;
    if (sensor.MaximumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if (sensor.MaximumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if (sensor.MinimumThreshold > sensor.MaximumThreshold)
      sensor.MaximumThreshold = sensor.MinimumThreshold;
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "0" : (sensor.Hysteresis >> 16 /*0x10*/).ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "0" : (sensor.MinimumThreshold >> 16 /*0x10*/).ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "0" : (sensor.MaximumThreshold >> 16 /*0x10*/).ToString();
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    switch (collection["CalibrationType"])
    {
      case "Temperature":
        sensor.Calibration1 = (collection["CalibrationValue"].ToDouble() * 10.0).ToLong() | 2147483648U /*0x80000000*/.ToLong();
        break;
      case "0_PPM":
        sensor.Calibration2 = 2147483648U /*0x80000000*/.ToLong();
        break;
      case "Span_PPM":
        sensor.Calibration3 = (collection["CalibrationValue"].ToDouble() * 100.0).ToLong() | 2147483648U /*0x80000000*/.ToLong();
        break;
    }
    sensor.ProfileConfigDirty = false;
    sensor.ProfileConfig2Dirty = false;
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if ((sensor.Calibration1 & 2147483648U /*0x80000000*/.ToLong()) == 2147483648U /*0x80000000*/.ToLong())
      numArrayList.Add(Gas_O3Base.CalibrationFrameTemperature(sensor.SensorID, (double) (sensor.Calibration1 & 268435455L /*0x0FFFFFFF*/) / 10.0));
    else if ((sensor.Calibration2 & 2147483648U /*0x80000000*/.ToLong()) == 2147483648U /*0x80000000*/.ToLong())
      numArrayList.Add(Gas_O3Base.CalibrationFrameZero(sensor.SensorID));
    else if ((sensor.Calibration3 & 2147483648U /*0x80000000*/.ToLong()) == 2147483648U /*0x80000000*/.ToLong())
      numArrayList.Add(Gas_O3Base.CalibrationFrameSpan(sensor.SensorID, (double) (sensor.Calibration3 & 268435455L /*0x0FFFFFFF*/) / 100.0));
    if (numArrayList.Count > 0)
    {
      numArrayList.Add(sensor.ReadProfileConfig(29));
      return numArrayList;
    }
    sensor.PendingActionControlCommand = false;
    sensor.Save();
    return (List<byte[]>) null;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Gas_O3 left, Gas_O3 right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Gas_O3 left, Gas_O3 right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Gas_O3 left, Gas_O3 right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Gas_O3 left, Gas_O3 right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Gas_O3 left, Gas_O3 right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Gas_O3 left, Gas_O3 right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Gas_O3 && this.Equals((MonnitApplicationBase) (obj as Gas_O3));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Gas_O3 && this.PPM == (right as Gas_O3).PPM;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Gas_O3 && this.PPM != (right as Gas_O3).PPM;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Gas_O3 && this.PPM < (right as Gas_O3).PPM;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Gas_O3 && this.PPM <= (right as Gas_O3).PPM;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Gas_O3 && this.PPM > (right as Gas_O3).PPM;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Gas_O3 && this.PPM >= (right as Gas_O3).PPM;
  }

  public static long DefaultMinThreshold => 65336;

  public static long DefaultMaxThreshold => 131072500;
}
