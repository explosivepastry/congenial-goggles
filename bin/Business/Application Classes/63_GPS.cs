// Decompiled with JetBrains decompiler
// Type: Monnit.GPS
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

public class GPS : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _GPS_Extended;
  private SensorAttribute _GPS_Units;

  public static long MonnitApplicationID => 63 /*0x3F*/;

  public static string ApplicationName => nameof (GPS);

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "None";

  public override long ApplicationID => GPS.MonnitApplicationID;

  public bool Valid { get; set; }

  public double Lat { get; set; }

  public double Lon { get; set; }

  public double Speed { get; set; }

  public double Bearing { get; set; }

  public double Altitude { get; set; }

  public int Satellites { get; set; }

  public int Error { get; set; }

  public override string Serialize()
  {
    string str = "False";
    if (this.Valid)
      str = $"{this.Valid}|{this.Lat}|{this.Lon}|{this.Speed}|{this.Bearing}|{this.Altitude}|{this.Satellites}|{this.Error}";
    return str;
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[8]
      {
        new AppDatum(eDatumType.BooleanData, "Valid", this.Valid),
        new AppDatum(eDatumType.Coordinate, "Lat", this.Lat),
        new AppDatum(eDatumType.Coordinate, "Lon", this.Lon),
        new AppDatum(eDatumType.Speed, "Speed", this.Speed),
        new AppDatum(eDatumType.Angle, "Bearing", this.Bearing),
        new AppDatum(eDatumType.Distance, "Altitude", this.Altitude * 12.0),
        new AppDatum(eDatumType.Count, "Satellites", this.Satellites),
        new AppDatum(eDatumType.Error, "Error", this.Error)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      (object) this.Datums.Select<AppDatum, object>((Func<AppDatum, object>) (da => da.data.compvalue))
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) GPS.Deserialize(version, serialized);
  }

  public static bool Extended(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "GPS_Extended")
        return Convert.ToBoolean(sensorAttribute.Value);
    }
    return false;
  }

  public static void ShowExtended(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "GPS_Extended")
      {
        if (!Convert.ToBoolean(sensorAttribute.Value))
        {
          sensorAttribute.Value = true.ToString();
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "GPS_Extended",
        Value = true.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void HideExtended(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "GPS_Extended")
        sensorAttribute.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute GPS_Extended => this._GPS_Extended;

  public static GPS.Units GetUnits(long sensorID)
  {
    try
    {
      foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
      {
        if (sensorAttribute.Name == "GPS_Units")
          return (GPS.Units) Enum.Parse(typeof (GPS.Units), sensorAttribute.Value);
      }
    }
    catch
    {
    }
    return GPS.Units.Standard;
  }

  public static void SetUnits(long sensorID, GPS.Units unit)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "GPS_Units")
      {
        if (!Convert.ToBoolean(sensorAttribute.Value))
        {
          sensorAttribute.Value = unit.ToString();
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "GPS_Units",
        Value = unit.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute GPS_Units => this._GPS_Units;

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "GPS_Extended")
        this._GPS_Extended = sensorAttribute;
      if (sensorAttribute.Name == "GPS_Units")
        this._GPS_Units = sensorAttribute;
    }
  }

  public override string NotificationString
  {
    get
    {
      if (!this.Valid)
        return "Unknown";
      string notificationString = $"{Math.Abs(this.Lat)} {(this.Lat >= 0.0 ? (object) "N" : (object) "S")}, {Math.Abs(this.Lon)} {(this.Lon >= 0.0 ? (object) "E" : (object) "W")}";
      if (Convert.ToBoolean(this.GPS_Extended.Value))
      {
        switch (this.GPS_Units != null ? this.GPS_Units.Value : "Standard")
        {
          case "Knots":
            notificationString += $"(Speed:{this.Speed} knots Direction:{this.Bearing} Altitude:{this.Altitude * 3.281} ft Error:+-{(double) this.Error * 3.281} ft)";
            break;
          case "Metric":
            notificationString += $"(Speed:{this.Speed * 1.852} kph Direction:{this.Bearing} Altitude:{this.Altitude} meters Error:+-{this.Error} meters)";
            break;
          default:
            notificationString += $"(Speed:{this.Speed * 1.151} mph Direction:{this.Bearing} Altitude:{this.Altitude * 3.281} ft Error:+-{(double) this.Error * 3.281} ft)";
            break;
        }
      }
      return notificationString;
    }
  }

  public override object PlotValue => this.Valid ? (object) $"{this.Lat}, {this.Lon}" : (object) "";

  public static GPS Deserialize(string version, string serialized)
  {
    GPS gps = new GPS();
    if (!string.IsNullOrEmpty(serialized))
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length == 8)
      {
        gps.Valid = strArray[0].ToBool();
        gps.Lat = strArray[1].ToDouble();
        gps.Lon = strArray[2].ToDouble();
        gps.Speed = strArray[3].ToDouble();
        gps.Bearing = strArray[4].ToDouble();
        gps.Altitude = strArray[5].ToDouble();
        gps.Satellites = strArray[6].ToInt();
        gps.Error = strArray[7].ToInt();
      }
      else
      {
        gps.Valid = strArray[0].ToBool();
        gps.Lat = strArray[0].ToDouble();
        gps.Lon = strArray[0].ToDouble();
        gps.Speed = strArray[0].ToDouble();
        gps.Bearing = strArray[0].ToDouble();
        gps.Altitude = strArray[0].ToDouble();
        gps.Satellites = strArray[0].ToInt();
        gps.Error = strArray[0].ToInt();
      }
    }
    return gps;
  }

  public static GPS Create(byte[] sdm, int startIndex)
  {
    byte num = sdm[startIndex - 1];
    return new GPS()
    {
      Valid = num == (byte) 0,
      Lat = (double) BitConverter.ToSingle(sdm, startIndex),
      Lon = (double) BitConverter.ToSingle(sdm, startIndex + 4),
      Speed = (double) BitConverter.ToUInt16(sdm, startIndex + 8) / 10.0,
      Bearing = (double) BitConverter.ToUInt16(sdm, startIndex + 10) / 10.0,
      Altitude = (double) BitConverter.ToUInt16(sdm, startIndex + 12) / 10.0,
      Satellites = (int) sdm[startIndex + 14],
      Error = (int) sdm[startIndex + 15]
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
      SnoozeDuration = 60,
      CompareValue = "60",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = GPS.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
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

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(GPS left, GPS right) => left.Equals((MonnitApplicationBase) right);

  public static bool operator !=(GPS left, GPS right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(GPS left, GPS right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(GPS left, GPS right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(GPS left, GPS right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(GPS left, GPS right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is GPS && this.Equals((MonnitApplicationBase) (obj as GPS));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is GPS && this.Valid && (right as GPS).Valid && this.Satellites == (right as GPS).Satellites && this.Lat == (right as GPS).Lat && this.Lon == (right as GPS).Lon && this.Speed == (right as GPS).Speed && this.Bearing == (right as GPS).Bearing && this.Altitude == (right as GPS).Altitude && this.Error == (right as GPS).Error;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is GPS && (this.Valid != (right as GPS).Valid || this.Satellites != (right as GPS).Satellites || this.Lat != (right as GPS).Lat || this.Lon != (right as GPS).Lon || this.Speed != (right as GPS).Speed || this.Bearing != (right as GPS).Bearing || this.Altitude != (right as GPS).Altitude || this.Error != (right as GPS).Error);
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<' cannot be applied to operands of type 'GPS' and 'GPS'");
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<=' cannot be applied to operands of type 'GPS' and 'GPS'");
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>' cannot be applied to operands of type 'GPS' and 'GPS'");
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>=' cannot be applied to operands of type 'GPS' and 'GPS'");
  }

  public enum Units
  {
    Standard,
    Metric,
    Knots,
  }
}
