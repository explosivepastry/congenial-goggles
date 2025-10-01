// Decompiled with JetBrains decompiler
// Type: Monnit.VehicleSpeed
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class VehicleSpeed : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _MorF;
  private SensorAttribute _Distance;

  public static long MonnitApplicationID => 40;

  public static string ApplicationName => "Vehicle Detector";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Point";

  public override long ApplicationID => VehicleSpeed.MonnitApplicationID;

  public int State { get; set; }

  public int Strength { get; set; }

  public int TimeMs { get; set; }

  public bool Direction { get; set; }

  public override string Serialize()
  {
    return $"{$"{$"{(this.State == 0 ? "ONLINE" : "SENSOR FAILURE")},{this.Strength.ToString()}"},{(!this.Direction ? "L->R" : "R->L")}"},{this.TimeMs.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[4]
      {
        new AppDatum(eDatumType.Time, "TimeMs", this.TimeMs),
        new AppDatum(eDatumType.State, "State", this.State),
        new AppDatum(eDatumType.Magnitude, "Strength", this.Strength),
        new AppDatum(eDatumType.BooleanData, "Direction", this.Direction)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      this.PlotValue,
      (object) this.State,
      (object) this.Strength,
      (object) this.Direction
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) VehicleSpeed.Deserialize(version, serialized);
  }

  public static bool IsMeters(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "MorF" && sensorAttribute.Value == "F")
        return false;
    }
    return true;
  }

  public static void MakeMeters(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "MorF")
        sensorAttribute.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void MakeFeet(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "MorF")
      {
        if (sensorAttribute.Value != "F")
        {
          sensorAttribute.Value = "F";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "MorF",
        Value = "F",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute MorF => this._MorF;

  public static double GetDistance(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Distance")
        return sensorAttribute.Value.ToDouble();
    }
    return 0.0;
  }

  public static void SetDistance(long sensorID, double distance)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Distance")
      {
        sensorAttribute.Value = distance.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "Distance",
        Value = distance.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute Distance => this._Distance;

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "MorF")
        this._MorF = sensorAttribute;
      if (sensorAttribute.Name == "Distance")
        this._Distance = sensorAttribute;
    }
  }

  public override string NotificationString
  {
    get
    {
      return (this.State >> 4 & 12) == 0 ? (this.Strength != 0 ? (this.Distance != null && this.Distance.Value.ToDouble() > 0.0 ? (this.MorF == null || !(this.MorF.Value == "F") ? $"{this.PlotValue} KPH" : $"{this.PlotValue} MPH") : $"TIME: {this.TimeMs}") : "No Vehicles Detected.") : "Internal Sensor Failure!!";
    }
  }

  public override object PlotValue
  {
    get
    {
      if (this.TimeMs == 0)
        return (object) 0;
      if (this.Distance == null || this.Distance.Value.ToDouble() <= 0.0)
        return (object) this.TimeMs;
      double num = this.Distance.Value.ToDouble() / ((double) this.TimeMs / 1000.0);
      return this.MorF != null && this.MorF.Value == "F" ? (object) Math.Round(num * (15.0 / 22.0), 1) : (object) Math.Round(num * 3.6, 1);
    }
  }

  public static VehicleSpeed Deserialize(string version, string serialized)
  {
    VehicleSpeed vehicleSpeed = new VehicleSpeed();
    if (string.IsNullOrEmpty(serialized))
    {
      vehicleSpeed.State = 0;
      vehicleSpeed.Strength = 0;
      vehicleSpeed.Direction = false;
      vehicleSpeed.TimeMs = 0;
    }
    else
    {
      string[] o = serialized.Split(',');
      if (o.Length == 4)
      {
        vehicleSpeed.State = o[0] == "ONLINE" ? 0 : 15;
        vehicleSpeed.Strength = o[1].ToInt();
        vehicleSpeed.Direction = !(o[2] == "L->R");
        vehicleSpeed.TimeMs = o[3].ToInt();
      }
      else if (o.Length == 1)
      {
        vehicleSpeed.State = 0;
        vehicleSpeed.Strength = 0;
        vehicleSpeed.Direction = false;
        vehicleSpeed.TimeMs = o.ToInt();
      }
      else
      {
        vehicleSpeed.State = 0;
        vehicleSpeed.Strength = 0;
        vehicleSpeed.Direction = false;
        vehicleSpeed.TimeMs = 0;
      }
    }
    return vehicleSpeed;
  }

  public static VehicleSpeed Create(byte[] sdm, int startIndex)
  {
    VehicleSpeed vehicleSpeed = new VehicleSpeed();
    byte[] destinationArray = new byte[2];
    vehicleSpeed.State = (int) sdm[startIndex - 1] >> 4;
    Array.Copy((Array) sdm, startIndex, (Array) destinationArray, 0, 2);
    if (((int) destinationArray[1] & 128 /*0x80*/) == 128 /*0x80*/)
    {
      vehicleSpeed.Direction = true;
      destinationArray[1] &= (byte) 127 /*0x7F*/;
    }
    vehicleSpeed.Strength = (int) BitConverter.ToUInt16(destinationArray, 0);
    Array.Copy((Array) sdm, startIndex + 2, (Array) destinationArray, 0, 2);
    vehicleSpeed.TimeMs = (int) BitConverter.ToUInt16(destinationArray, 0);
    return vehicleSpeed;
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
      CompareType = eCompareType.Greater_Than_or_Equal,
      CompareValue = "1000",
      AccountID = sensor.AccountID,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = VehicleSpeed.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
      return;
    if (collection["Hysteresis_Manual"].ToLower().Replace(" ", "").StartsWith("channel"))
    {
      int num1 = collection["Hysteresis_Manual"].ToLower().Replace(" ", "").Replace("channel", "").ToInt();
      int num2 = num1 < 0 ? 0 : (num1 <= 24 ? 1 : 0);
      sensor.Hysteresis = num2 == 0 ? (long) uint.MaxValue : (long) num1;
    }
    else
      sensor.Hysteresis = (long) uint.MaxValue;
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis >= 0L && sensor.Hysteresis <= 24L ? $"Channel {sensor.Hysteresis}" : "No Channel Selected";
  }

  public static string MinThreshForUI(Sensor sensor) => "Not Used";

  public static string MaxThreshForUI(Sensor sensor) => "Not Used";

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (collection["distanceScale"] == "F")
      VehicleSpeed.MakeFeet(sensor.SensorID);
    else
      VehicleSpeed.MakeMeters(sensor.SensorID);
    VehicleSpeed.SetDistance(sensor.SensorID, collection["distance"].ToDouble());
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      numArrayList.Add(LightBoolBase.CalibrateFrame(sensor.SensorID));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(VehicleSpeed left, VehicleSpeed right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(VehicleSpeed left, VehicleSpeed right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(VehicleSpeed left, VehicleSpeed right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(VehicleSpeed left, VehicleSpeed right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(VehicleSpeed left, VehicleSpeed right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(VehicleSpeed left, VehicleSpeed right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is VehicleSpeed && this.Equals((MonnitApplicationBase) (obj as VehicleSpeed));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is VehicleSpeed && this.TimeMs == (right as VehicleSpeed).TimeMs;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is VehicleSpeed && this.TimeMs != (right as VehicleSpeed).TimeMs;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is VehicleSpeed && this.TimeMs < (right as VehicleSpeed).TimeMs;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is VehicleSpeed && this.TimeMs <= (right as VehicleSpeed).TimeMs;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is VehicleSpeed && this.TimeMs > (right as VehicleSpeed).TimeMs;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is VehicleSpeed && this.TimeMs >= (right as VehicleSpeed).TimeMs;
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
}
