// Decompiled with JetBrains decompiler
// Type: Monnit.AGPS
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

public class AGPS : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _AGPS_Extended;
  private SensorAttribute _AGPS_Units;

  public static long MonnitApplicationID => 68;

  public static string ApplicationName => "Cellular Assisted Location";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "None";

  public override long ApplicationID => AGPS.MonnitApplicationID;

  public bool Valid { get; set; }

  public double Lat { get; set; }

  public double Lon { get; set; }

  public double Altitude { get; set; }

  public int Error { get; set; }

  public override string Serialize()
  {
    string str = "False";
    if (this.Valid)
      str = $"True|{this.Lat}|{this.Lon}|{this.Altitude}|{this.Error}";
    return str;
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[5]
      {
        new AppDatum(eDatumType.BooleanData, "Valid", this.Valid),
        new AppDatum(eDatumType.Coordinate, "Lat", this.Lat),
        new AppDatum(eDatumType.Coordinate, "Lon", this.Lon),
        new AppDatum(eDatumType.Distance, "Altitude", this.Altitude),
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
    return (MonnitApplicationBase) AGPS.Deserialize(version, serialized);
  }

  public static bool Extended(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "AGPS_Extended")
        return Convert.ToBoolean(sensorAttribute.Value);
    }
    return false;
  }

  public static void ShowExtended(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "AGPS_Extended")
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
        Name = "AGPS_Extended",
        Value = true.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void HideExtended(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "AGPS_Extended")
        sensorAttribute.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute AGPS_Extended
  {
    get
    {
      if (this._AGPS_Extended == null)
        this._AGPS_Extended = new SensorAttribute()
        {
          Name = nameof (AGPS_Extended),
          Value = "false"
        };
      return this._AGPS_Extended;
    }
  }

  public static AGPS.Units GetUnits(long sensorID)
  {
    try
    {
      foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
      {
        if (sensorAttribute.Name == "AGPS_Units")
          return (AGPS.Units) Enum.Parse(typeof (AGPS.Units), sensorAttribute.Value);
      }
    }
    catch
    {
    }
    return AGPS.Units.Standard;
  }

  public static void SetUnits(long sensorID, AGPS.Units unit)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "AGPS_Units")
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
        Name = "AGPS_Units",
        Value = unit.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute AGPS_Units
  {
    get
    {
      if (this._AGPS_Units == null)
        this._AGPS_Units = new SensorAttribute()
        {
          Name = nameof (AGPS_Units),
          Value = "Standard"
        };
      return this._AGPS_Units;
    }
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "AGPS_Extended")
        this._AGPS_Extended = sensorAttribute;
      if (sensorAttribute.Name == "AGPS_Units")
        this._AGPS_Units = sensorAttribute;
    }
  }

  public override string NotificationString
  {
    get
    {
      string notificationString = $"{Math.Abs(this.Lat)} {(this.Lat >= 0.0 ? (object) "N" : (object) "S")}, {Math.Abs(this.Lon)} {(this.Lon >= 0.0 ? (object) "E" : (object) "W")}";
      if (Convert.ToBoolean(this.AGPS_Extended.Value))
      {
        switch (this.AGPS_Units != null ? this.AGPS_Units.Value : "Standard")
        {
          case "Knots":
            notificationString += $"(Altitude:{this.Altitude * 3.281} ft Error:+-{(double) this.Error * 3.281} ft)";
            break;
          case "Metric":
            notificationString += string.Format("(Altitude:{0} meters Error:+-{3} meters)", (object) this.Altitude, (object) this.Error);
            break;
          default:
            notificationString += $"(Altitude:{this.Altitude * 3.281} ft Error:+-{(double) this.Error * 3.281} ft)";
            break;
        }
      }
      return notificationString;
    }
  }

  public override object PlotValue => (object) $"{this.Lat}, {this.Lon}";

  public static AGPS Deserialize(string version, string serialized)
  {
    AGPS agps = new AGPS();
    if (!string.IsNullOrEmpty(serialized))
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length == 5)
      {
        agps.Valid = strArray[0].ToBool();
        agps.Lat = strArray[1].ToDouble();
        agps.Lon = strArray[2].ToDouble();
        agps.Altitude = (double) strArray[3].ToInt();
        agps.Error = strArray[4].ToInt();
      }
      else
      {
        agps.Valid = strArray[0].ToBool();
        agps.Lat = strArray[0].ToDouble();
        agps.Lon = strArray[0].ToDouble();
        agps.Altitude = (double) strArray[0].ToInt();
        agps.Error = strArray[0].ToInt();
      }
    }
    return agps;
  }

  public static AGPS Create(byte[] sdm, int startIndex)
  {
    byte num = sdm[startIndex - 1];
    return new AGPS()
    {
      Valid = (double) BitConverter.ToSingle(sdm, startIndex) < 100.0,
      Lat = (double) BitConverter.ToSingle(sdm, startIndex),
      Lon = (double) BitConverter.ToSingle(sdm, startIndex + 4),
      Altitude = (double) BitConverter.ToInt32(sdm, startIndex + 8),
      Error = BitConverter.ToInt32(sdm, startIndex + 12)
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
      ApplicationID = AGPS.MonnitApplicationID,
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

  public static bool operator ==(AGPS left, AGPS right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(AGPS left, AGPS right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(AGPS left, AGPS right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(AGPS left, AGPS right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(AGPS left, AGPS right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(AGPS left, AGPS right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is AGPS && this.Equals((MonnitApplicationBase) (obj as AGPS));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is AGPS && this.Lat == (right as AGPS).Lat && this.Lon == (right as AGPS).Lon;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is AGPS && (this.Lat != (right as AGPS).Lat || this.Lon != (right as AGPS).Lon);
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<' cannot be applied to operands of type 'AGPS' and 'AGPS'");
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<=' cannot be applied to operands of type 'AGPS' and 'AGPS'");
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>' cannot be applied to operands of type 'AGPS' and 'AGPS'");
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>=' cannot be applied to operands of type 'AGPS' and 'AGPS'");
  }

  public enum Units
  {
    Standard,
    Metric,
    Knots,
  }
}
