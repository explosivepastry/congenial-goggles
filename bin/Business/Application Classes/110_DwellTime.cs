// Decompiled with JetBrains decompiler
// Type: Monnit.DwellTime
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

public class DwellTime : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;

  public static long MonnitApplicationID => DwellTimeBase.MonnitApplicationID;

  public static string ApplicationName => "GridEye DwellTime";

  public static eApplicationProfileType ProfileType => TemperatureBase.ProfileType;

  public override string ChartType => "None";

  public override long ApplicationID => DwellTime.MonnitApplicationID;

  public int stsState { get; set; }

  public int Dwelltime { get; set; }

  public int EntryRegion { get; set; }

  public int ExitRegion { get; set; }

  public double MaxTemperature { get; set; }

  public double MaxPixelCount { get; set; }

  public static DwellTime Create(byte[] sdm, int startIndex)
  {
    DwellTime dwellTime = new DwellTime();
    dwellTime.stsState = (int) sdm[startIndex - 1] >> 4;
    try
    {
      dwellTime.Dwelltime = (int) BitConverter.ToUInt16(sdm, startIndex);
      dwellTime.EntryRegion = (int) sdm[startIndex + 2];
      dwellTime.ExitRegion = (int) sdm[startIndex + 3];
      dwellTime.MaxTemperature = BitConverter.ToInt16(sdm, startIndex + 4).ToDouble() / 100.0;
      dwellTime.MaxPixelCount = (double) sdm[startIndex + 6];
    }
    catch
    {
      dwellTime.Dwelltime = 0;
      dwellTime.EntryRegion = 0;
      dwellTime.ExitRegion = 0;
      dwellTime.MaxTemperature = 0.0;
      dwellTime.MaxPixelCount = 0.0;
    }
    return dwellTime;
  }

  public override string Serialize()
  {
    return $"{this.Dwelltime}|{this.EntryRegion}|{this.ExitRegion}|{this.MaxTemperature}|{this.MaxPixelCount}|{this.stsState}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[5]
      {
        new AppDatum(eDatumType.Time, nameof (DwellTime), this.Dwelltime),
        new AppDatum(eDatumType.Count, "EntryRegion", this.EntryRegion),
        new AppDatum(eDatumType.Count, "ExitRegion", this.ExitRegion),
        new AppDatum(eDatumType.TemperatureData, "MaxTemperature", this.MaxTemperature),
        new AppDatum(eDatumType.Count, "MaxPixelCount", this.MaxPixelCount)
      });
    }
  }

  public static DwellTime Deserialize(string version, string serialized)
  {
    DwellTime dwellTime = new DwellTime();
    if (string.IsNullOrEmpty(serialized))
    {
      dwellTime.Dwelltime = 0;
      dwellTime.EntryRegion = 0;
      dwellTime.ExitRegion = 0;
      dwellTime.MaxTemperature = 0.0;
      dwellTime.MaxPixelCount = 0.0;
      dwellTime.stsState = 0;
    }
    else
    {
      try
      {
        string[] strArray = serialized.Split('|');
        dwellTime.Dwelltime = strArray[0].ToInt();
        dwellTime.EntryRegion = strArray[1].ToInt();
        dwellTime.ExitRegion = strArray[2].ToInt();
        dwellTime.MaxTemperature = strArray[3].ToDouble();
        dwellTime.MaxPixelCount = (double) strArray[4].ToInt();
        try
        {
          dwellTime.stsState = strArray[5].ToInt();
        }
        catch
        {
          dwellTime.stsState = 0;
        }
      }
      catch
      {
        dwellTime.Dwelltime = 0;
        dwellTime.EntryRegion = 0;
        dwellTime.ExitRegion = 0;
        dwellTime.MaxTemperature = 0.0;
        dwellTime.MaxPixelCount = 0.0;
        dwellTime.stsState = 0;
      }
    }
    return dwellTime;
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) DwellTime.Deserialize(version, serialized);
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
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[5]
    {
      (object) this.Dwelltime,
      (object) this.EntryRegion,
      (object) this.ExitRegion,
      (object) (DwellTime.IsFahrenheit(sensorID) ? this.MaxTemperature.ToFahrenheit() : this.MaxTemperature),
      (object) this.MaxPixelCount
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[5]
    {
      nameof (DwellTime),
      "EntryRegion",
      "ExitRegion",
      "MaxTemperature" + (DwellTime.IsFahrenheit(sensorID) ? " Fahrenheit" : " Celsius"),
      "MaxPixelCount"
    });
  }

  public override string NotificationString
  {
    get
    {
      if (this.stsState > 0)
      {
        switch (this.stsState)
        {
          case 1:
            return "Comm Error";
          case 2:
            return "Sensor Initializing";
          case 3:
            return "Average Not Set";
          case 4:
            return "Bad Pixels Detected";
        }
      }
      return $"Dwell Time: {this.Dwelltime} , Entry Region: {this.EntryRegion} , Exit Region: {this.ExitRegion} , Max Temperature: {(this.CorF == null || !(this.CorF.Value == "C") ? this.MaxTemperature.ToFahrenheit().ToString("#0.00° F", (IFormatProvider) CultureInfo.InvariantCulture) : this.MaxTemperature.ToString("#0.00° C", (IFormatProvider) CultureInfo.InvariantCulture))} , Max Pixel Count: {this.MaxPixelCount}";
    }
  }

  public override object PlotValue
  {
    get
    {
      return (object) new object[5]
      {
        (object) this.Dwelltime,
        (object) this.EntryRegion,
        (object) this.ExitRegion,
        (object) this.MaxTemperature,
        (object) this.MaxPixelCount
      };
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
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      SnoozeDuration = 60,
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = DwellTime.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    DwellTimeBase.GetDefaults(new Version(sensor.FirmwareVersion), sensor.GenerationType);
    bool flag = DwellTime.IsFahrenheit(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["BasicThreshold"]) && !string.IsNullOrEmpty(collection["BasicThresholdDirection"]) && collection["BasicThresholdDirection"] != "-1")
      return;
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      long num = collection["MaximumThreshold_Manual"].ToLong();
      if (num < 0L)
        num = 0L;
      if (num > (long) ushort.MaxValue)
        num = (long) ushort.MaxValue;
      sensor.MaximumThreshold = num;
    }
    if (!string.IsNullOrEmpty(collection["MinDetectionTemp_Manual"]) && !string.IsNullOrEmpty(collection["MaxDetectionTemp_Manual"]))
    {
      double Fahrenheit1 = collection["MinDetectionTemp_Manual"].ToDouble();
      double Fahrenheit2 = collection["MaxDetectionTemp_Manual"].ToDouble();
      if (flag)
      {
        Fahrenheit1 = Fahrenheit1.ToCelsius();
        Fahrenheit2 = Fahrenheit2.ToCelsius();
      }
      if (Fahrenheit1 > 75.0)
        Fahrenheit1 = 75.0;
      if (Fahrenheit1 < 10.0)
        Fahrenheit1 = 10.0;
      if (Fahrenheit2 > 75.0)
        Fahrenheit2 = 75.0;
      if (Fahrenheit2 < 10.0)
        Fahrenheit2 = 10.0;
      if (Fahrenheit2 < Fahrenheit1)
        Fahrenheit2 = Fahrenheit1;
      if (Fahrenheit1 > Fahrenheit2)
        Fahrenheit1 = Fahrenheit2;
      DwellTime.SetCalVal1ByteZeroAndOne(sensor, Fahrenheit1);
      DwellTime.SetCalVal1ByteTwoAndThree(sensor, Fahrenheit2);
    }
    if (!string.IsNullOrEmpty(collection["MinBodyTemp_Manual"]))
    {
      int num = collection["MinBodyTemp_Manual"].ToInt();
      if (num > 64 /*0x40*/)
        num = 64 /*0x40*/;
      if (num < 1)
        num = 1;
      DwellTime.SetCalVal2ByteZero(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["CalVal3_Manual"]))
    {
      double num = collection["CalVal3_Manual"].ToDouble();
      if (num < 0.0)
        num = 0.0;
      if (num > (double) ushort.MaxValue)
        num = (double) ushort.MaxValue;
      DwellTime.SetCalVal3ByteZeroAndOne(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["CalVal4_Manual"]))
    {
      double num1 = collection["CalVal4_Manual"].ToDouble();
      double num2 = flag ? num1 / 1.8 : num1;
      if (num2 < 0.0)
        num2 = 0.0;
      if (num2 > 40.0)
        num2 = 40.0;
      DwellTime.SetDifferentialTemperature(sensor, num2);
    }
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : sensor.Hysteresis.ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : sensor.MinimumThreshold.ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : sensor.MaximumThreshold.ToString();
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection returnData)
  {
    if (collection["TempScale"] == "on")
    {
      returnData["TempScale"] = "F";
      DwellTime.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      returnData["TempScale"] = "C";
      DwellTime.MakeCelsius(sensor.SensorID);
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    sensor.ProfileConfigDirty = false;
    sensor.ProfileConfig2Dirty = false;
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    return new List<byte[]>()
    {
      DwellTimeBase.CalibrateFrame(sensor.SensorID),
      sensor.ReadProfileConfig(29)
    };
  }

  public static double GetCalVal1ByteZeroAndOne(Sensor sensor)
  {
    double Celsius = (double) Convert.ToInt16(sensor.Calibration1 & (long) ushort.MaxValue) / 4.0;
    return DwellTime.IsFahrenheit(sensor.SensorID) ? Celsius.ToFahrenheit() : Celsius;
  }

  public static void SetCalVal1ByteZeroAndOne(Sensor sensor, double value)
  {
    value = Math.Round(value * 4.0, 0);
    uint int32 = (uint) Convert.ToInt32(sensor.Calibration1 & 4294901760L);
    sensor.Calibration1 = (long) (int32 | (uint) (ushort) value);
  }

  public static double GetCalVal1ByteTwoAndThree(Sensor sensor)
  {
    double Celsius = (double) Convert.ToUInt32((sensor.Calibration1 & 4294901760L | (long) ushort.MaxValue) >> 16 /*0x10*/) / 4.0;
    return DwellTime.IsFahrenheit(sensor.SensorID) ? Celsius.ToFahrenheit() : Celsius;
  }

  public static void SetCalVal1ByteTwoAndThree(Sensor sensor, double value)
  {
    value = Math.Round(value * 4.0, 0);
    uint int32 = (uint) Convert.ToInt32(sensor.Calibration1 & (long) ushort.MaxValue);
    sensor.Calibration1 = (long) (int32 | (uint) value << 16 /*0x10*/);
  }

  public static int GetCalVal2ByteZero(Sensor sensor)
  {
    return Convert.ToInt32(sensor.Calibration2 & (long) byte.MaxValue);
  }

  public static void SetCalVal2ByteZero(Sensor sensor, int value)
  {
    uint int32 = (uint) Convert.ToInt32(sensor.Calibration2 & 65280L);
    sensor.Calibration2 = (long) (int32 | (uint) value);
  }

  public static int GetCalVal2ByteOne(Sensor sensor)
  {
    return Convert.ToInt32(Convert.ToUInt32(sensor.Calibration2 & 65280L) >> 8);
  }

  public static void SetCalVal2ByteOne(Sensor sensor, int value)
  {
    int calVal2ByteZero = DwellTime.GetCalVal2ByteZero(sensor);
    if (value < calVal2ByteZero * 2)
      value = calVal2ByteZero * 2;
    if (value > 64 /*0x40*/)
      value = 64 /*0x40*/;
    uint int32 = (uint) Convert.ToInt32(sensor.Calibration2 & (long) byte.MaxValue);
    sensor.Calibration2 = (long) (int32 | (uint) (value << 8));
  }

  public static int GetCalVal3ByteZeroAndOne(Sensor sensor)
  {
    return ((uint) ((int) (uint) sensor.Calibration3 & (int) ushort.MaxValue)).ToInt();
  }

  public static void SetCalVal3ByteZeroAndOne(Sensor sensor, double value)
  {
    int num = (int) ((long) (int) sensor.Calibration3 & 4294901760L);
    sensor.Calibration3 = (long) (num | (int) (ushort) value);
  }

  public static double GetDifferentialTemperature(Sensor sensor)
  {
    return Math.Round((double) sensor.Calibration4 / 4.0 * 4.0 / 4.0, 2);
  }

  public static void SetDifferentialTemperature(Sensor sensor, double value)
  {
    value *= 4.0;
    sensor.Calibration4 = value.ToLong();
  }

  public static int GetMinThreshByteZero(Sensor sensor)
  {
    return Convert.ToInt32(sensor.MinimumThreshold & (long) byte.MaxValue);
  }

  public static void SetMinThreshByteZero(Sensor sensor, int value)
  {
    if (value < 0)
      value = 0;
    if (value > 2)
      value = 2;
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.MinimumThreshold) & 4294967040UL);
    sensor.MinimumThreshold = (long) (num | (uint) value);
  }

  public static int GetMinThreshByteOne(Sensor sensor)
  {
    return Convert.ToInt32((sensor.MinimumThreshold & 65280L) >> 8);
  }

  public static void SetMinThreshByteOne(Sensor sensor, int value)
  {
    if (value < 1)
      value = 1;
    if (value > 8)
      value = 8;
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.MinimumThreshold) & 4294902015UL);
    sensor.MinimumThreshold = (long) (num | (uint) (value << 8));
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(DwellTime left, DwellTime right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(DwellTime left, DwellTime right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(DwellTime left, DwellTime right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(DwellTime left, DwellTime right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(DwellTime left, DwellTime right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(DwellTime left, DwellTime right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is DwellTime && this.Equals((MonnitApplicationBase) (obj as DwellTime));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is DwellTime && this.Dwelltime == (right as DwellTime).Dwelltime;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is DwellTime && this.Dwelltime != (right as DwellTime).Dwelltime;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is DwellTime && this.Dwelltime < (right as DwellTime).Dwelltime;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is DwellTime && this.Dwelltime <= (right as DwellTime).Dwelltime;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is DwellTime && this.Dwelltime > (right as DwellTime).Dwelltime;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is DwellTime && this.Dwelltime >= (right as DwellTime).Dwelltime;
  }
}
