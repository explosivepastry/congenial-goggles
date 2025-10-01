// Decompiled with JetBrains decompiler
// Type: Monnit.APN
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class APN : MonnitApplicationBase
{
  private SensorAttribute _GPS_Extended;
  private SensorAttribute _GPS_Units;

  public static long MonnitApplicationID => 0;

  public static string ApplicationName => nameof (APN);

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "None";

  public override long ApplicationID => APN.MonnitApplicationID;

  public bool Valid { get; set; }

  public double Lat { get; set; }

  public double Lon { get; set; }

  public double Altitude { get; set; }

  public double Error { get; set; }

  public override string Serialize()
  {
    string str = "False";
    if (this.Valid)
      str = $"{this.Valid}|{this.Lat}|{this.Lon}|{this.Altitude}|{this.Error}";
    return str;
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[5]
      {
        new AppDatum(eDatumType.BooleanData, "Valid", this.Valid),
        new AppDatum(eDatumType.Coordinate, "Latitude", this.Lat),
        new AppDatum(eDatumType.Coordinate, "Longitude", this.Lon),
        new AppDatum(eDatumType.Distance, "Altitude", this.Altitude),
        new AppDatum(eDatumType.Error, "Error", this.Error)
      });
    }
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) APN.Deserialize(version, serialized);
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

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[1]
    {
      APN.GetUnits(sensorID).ToString()
    });
  }

  public static APN.Units GetUnits(long sensorID)
  {
    try
    {
      foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
      {
        if (sensorAttribute.Name == "GPS_Units")
          return (APN.Units) Enum.Parse(typeof (APN.Units), sensorAttribute.Value);
      }
    }
    catch
    {
    }
    return APN.Units.Standard;
  }

  public static void SetUnits(long sensorID, APN.Units unit)
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
            notificationString += $"(Altitude:{this.Altitude * 3.281} ft Error:+-{this.Error * 3.281} ft)";
            break;
          case "Metric":
            notificationString += $"(Altitude:{this.Altitude} meters Error:+-{this.Error} meters)";
            break;
          default:
            notificationString += $"(Altitude:{this.Altitude * 3.281} ft Error:+-{this.Error * 3.281} ft)";
            break;
        }
      }
      return notificationString;
    }
  }

  public override object PlotValue => this.Valid ? (object) $"{this.Lat}, {this.Lon}" : (object) "";

  public static APN Deserialize(string version, string serialized)
  {
    APN apn = new APN();
    if (!string.IsNullOrEmpty(serialized))
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length == 8)
      {
        apn.Valid = strArray[0].ToBool();
        apn.Lat = strArray[1].ToDouble();
        apn.Lon = strArray[2].ToDouble();
        apn.Altitude = strArray[3].ToDouble();
        apn.Error = (double) strArray[4].ToInt();
      }
    }
    return apn;
  }

  public static APN Create(byte[] sdm, int startIndex)
  {
    byte num = sdm[startIndex - 1];
    return new APN()
    {
      Valid = num == (byte) 0,
      Lat = (double) BitConverter.ToSingle(sdm, startIndex),
      Lon = (double) BitConverter.ToSingle(sdm, startIndex + 4),
      Altitude = (double) BitConverter.ToSingle(sdm, startIndex + 8),
      Error = (double) BitConverter.ToSingle(sdm, startIndex + 12)
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
    sensor.BSNUrgentDelay = sensor.DefaultValue<int>("DefaultBSNUrgentDelay");
    sensor.UrgentRetries = sensor.DefaultValue<int>("DefaultUrgentRetries");
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
    sensor.LinkAcceptance = sensor.DefaultValue<int>("DefaultLinkAcceptance");
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
      ApplicationID = APN.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void VerifyNotificationValues(Notification notification, string scale)
  {
    notification.CompareType = eCompareType.Greater_Than;
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
  }

  public new static void ProfileSettingsForIntervalUI(
    Sensor sensor,
    out string Hyst,
    out string Min,
    out string Max)
  {
    Hyst = "";
    Min = "";
    Max = "";
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(APN left, APN right) => left.Equals((MonnitApplicationBase) right);

  public static bool operator !=(APN left, APN right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(APN left, APN right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(APN left, APN right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(APN left, APN right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(APN left, APN right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is APN && this.Equals((MonnitApplicationBase) (obj as APN));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is APN && this.Valid && (right as APN).Valid && this.Lat == (right as APN).Lat && this.Lon == (right as APN).Lon && this.Altitude == (right as APN).Altitude && this.Error == (right as APN).Error;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is APN && (this.Valid != (right as APN).Valid || this.Lat != (right as APN).Lat || this.Lon != (right as APN).Lon || this.Altitude != (right as APN).Altitude || this.Error != (right as APN).Error);
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
