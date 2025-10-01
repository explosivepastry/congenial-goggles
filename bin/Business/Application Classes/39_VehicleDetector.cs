// Decompiled with JetBrains decompiler
// Type: Monnit.VehicleDetector
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class VehicleDetector : MonnitApplicationBase
{
  private SensorAttribute _MorK;

  public static long MonnitApplicationID => 39;

  public static string ApplicationName => "Vehicle Counter";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Point";

  public override long ApplicationID => VehicleDetector.MonnitApplicationID;

  public int State { get; set; }

  public int Count { get; set; }

  public int Strength { get; set; }

  public int Duration { get; set; }

  public bool Direction { get; set; }

  public static bool IsMPH(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "MorK" && sensorAttribute.Value == "K")
        return false;
    }
    return true;
  }

  public static void MakeMPH(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "MorK")
        sensorAttribute.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void MakeKPH(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "MorK")
      {
        if (sensorAttribute.Value != "K")
        {
          sensorAttribute.Value = "K";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "MorK",
        Value = "K",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute MorK => this._MorK;

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "MorK")
        this._MorK = sensorAttribute;
    }
  }

  public static double GetMaxKPH(Sensor sens)
  {
    return sens.Calibration1 == long.MinValue ? 68.0 : 3389.8305084745762 / (double) sens.Calibration1;
  }

  public static double getMaxMPH(Sensor sens)
  {
    return sens.Calibration1 == long.MinValue ? 42.0 : VehicleDetector.GetMaxKPH(sens) * 0.621371;
  }

  public static double GetMinKPH(Sensor sens)
  {
    return sens.Calibration3 == long.MinValue ? 16.0 : 3389.8305084745762 / (double) sens.Calibration3;
  }

  public static double getMinMPH(Sensor sens)
  {
    return sens.Calibration3 == long.MinValue ? 10.0 : VehicleDetector.GetMinKPH(sens) * 0.621371;
  }

  public static bool IsActiveZeroing(Sensor sens)
  {
    return sens.Calibration4 != long.MinValue && sens.Calibration4 != 0L;
  }

  public override string Serialize()
  {
    string str1 = (this.State & 240 /*0xF0*/) == 0 ? "ONLINE" : "SENSOR FAILURE";
    int num = this.Count;
    string str2 = num.ToString();
    string str3 = $"{str1},{str2}";
    num = this.Strength;
    string str4 = num.ToString();
    string str5 = $"{str3},{str4}";
    num = this.Duration;
    string str6 = num.ToString();
    return $"{$"{str5},{str6}"},{(!this.Direction ? "L->R" : "R->L")}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[5]
      {
        new AppDatum(eDatumType.Count, "Count", this.Count),
        new AppDatum(eDatumType.VehicleDetect, "VehicleState", this.State),
        new AppDatum(eDatumType.Magnitude, "Strength", this.Strength),
        new AppDatum(eDatumType.Time, "Duration", this.Duration),
        new AppDatum(eDatumType.BooleanData, "Direction", this.Direction)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      this.PlotValue,
      (object) this.Count,
      (object) this.Strength,
      (object) this.Duration,
      (object) this.Direction
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) VehicleDetector.Deserialize(version, serialized);
  }

  public override bool IsValid => (this.State & 240 /*0xF0*/) != 240 /*0xF0*/;

  public override string NotificationString
  {
    get
    {
      return (this.State & 240 /*0xF0*/) != 240 /*0xF0*/ ? (this.Count != 0 ? $"CNT: {this.Count}, MAG: {this.Strength}, DIR: {(!this.Direction ? (object) "L->R" : (object) "R->L")}" : $"No Vehicles Detected. MAG:{this.Strength}") : "Internal Sensor Failure!!";
    }
  }

  public override object PlotValue => (object) this.Count;

  public static VehicleDetector Deserialize(string version, string serialized)
  {
    VehicleDetector vehicleDetector = new VehicleDetector();
    if (string.IsNullOrEmpty(serialized))
    {
      vehicleDetector.State = 15;
      vehicleDetector.Count = 0;
      vehicleDetector.Strength = 0;
      vehicleDetector.Duration = 0;
      vehicleDetector.Direction = false;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      if (strArray.Length == 5)
      {
        vehicleDetector.State = strArray[0] == "ONLINE" ? 0 : 18;
        vehicleDetector.Count = strArray[1].ToInt();
        vehicleDetector.Strength = strArray[2].ToInt();
        vehicleDetector.Duration = strArray[3].ToInt();
        vehicleDetector.Direction = !(strArray[4] == "L->R");
      }
      else if (strArray.Length == 1)
      {
        vehicleDetector.State = 0;
        vehicleDetector.Count = strArray[0].ToInt();
        vehicleDetector.Strength = 0;
        vehicleDetector.Duration = 0;
        vehicleDetector.Direction = false;
      }
      else
      {
        vehicleDetector.State = 15;
        vehicleDetector.Count = 0;
        vehicleDetector.Strength = 0;
        vehicleDetector.Duration = 0;
        vehicleDetector.Direction = false;
      }
    }
    return vehicleDetector;
  }

  public static VehicleDetector Create(byte[] sdm, int startIndex)
  {
    VehicleDetector vehicleDetector = new VehicleDetector();
    byte[] destinationArray = new byte[2];
    vehicleDetector.State = (int) sdm[startIndex - 1];
    Array.Copy((Array) sdm, startIndex, (Array) destinationArray, 0, 2);
    if (((int) destinationArray[1] & 128 /*0x80*/) == 128 /*0x80*/)
    {
      vehicleDetector.Direction = true;
      destinationArray[1] &= (byte) 127 /*0x7F*/;
    }
    vehicleDetector.Count = (int) BitConverter.ToUInt16(destinationArray, 0);
    Array.Copy((Array) sdm, startIndex + 2, (Array) destinationArray, 0, 2);
    vehicleDetector.Strength = (int) BitConverter.ToUInt16(destinationArray, 0);
    Array.Copy((Array) sdm, startIndex + 4, (Array) destinationArray, 0, 2);
    vehicleDetector.Duration = (int) BitConverter.ToUInt16(destinationArray, 0);
    return vehicleDetector;
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
      AccountID = sensor.AccountID,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = VehicleDetector.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion,
      CompareType = eCompareType.Greater_Than_or_Equal,
      CompareValue = "1"
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    sensor.ReportInterval = string.IsNullOrWhiteSpace(collection["ReportInterval"]) ? 120.0 : collection["ReportInterval"].ToDouble();
    sensor.ActiveStateInterval = sensor.ReportInterval;
    if (!string.IsNullOrEmpty(collection["Channel"]))
    {
      if (collection["Channel"].ToLower().Replace(" ", "").StartsWith("channel"))
      {
        ushort uint16 = Convert.ToUInt16(collection["Channel"].ToLower().Replace(" ", "").Replace("channel", ""));
        if (uint16 >= (ushort) 0 && uint16 <= (ushort) 24)
          VehicleDetector.HystLower(sensor, (int) uint16);
        else
          VehicleDetector.HystLower(sensor, (int) ushort.MaxValue);
      }
      else
        VehicleDetector.HystLower(sensor, (int) ushort.MaxValue);
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      sensor.MaximumThreshold = collection["MaximumThreshold_Manual"].ToLong();
      if (sensor.MaximumThreshold < 10L)
        sensor.MaximumThreshold = 10L;
      if (sensor.MaximumThreshold > 3000L)
        sensor.MaximumThreshold = 3000L;
    }
    if (!string.IsNullOrWhiteSpace(collection["Hysteresis_Manual"]) && collection["Hysteresis_Manual"].ToInt() >= 0 && collection["Hysteresis_Manual"].ToInt() <= 3000)
    {
      int num = ((double) collection["Hysteresis_Manual"].ToInt() / 100.0 * (double) sensor.MaximumThreshold).ToInt();
      VehicleDetector.HystUpper(sensor, num);
    }
    long result = 0;
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) && long.TryParse(collection["MinimumThreshold_Manual"], out result))
    {
      sensor.MinimumThreshold = result;
      if (sensor.MinimumThreshold < 0L)
        sensor.MinimumThreshold = 0L;
      if (sensor.MinimumThreshold > (long) ushort.MaxValue)
        sensor.MinimumThreshold = (long) ushort.MaxValue;
    }
    if (collection["SpeedScale"] == "on")
    {
      viewData["SpeedScale"] = "M";
      VehicleDetector.MakeMPH(sensor.SensorID);
    }
    else
    {
      viewData["SpeedScale"] = "K";
      VehicleDetector.MakeKPH(sensor.SensorID);
    }
    if (!string.IsNullOrEmpty(collection["MaxSpeed_Manual"]))
    {
      double num1 = collection["MinSpeed_Manual"].ToDouble() * 0.000474;
      double num2 = collection["MinSpeed_Manual"].ToDouble() * 0.000295;
      double num3 = collection["MaxSpeed_Manual"].ToDouble() * 0.000474;
      double num4 = collection["MaxSpeed_Manual"].ToDouble() * 0.000295;
      sensor.Calibration1 = collection["MaxSpeed_Manual"].ToDouble() >= collection["MinSpeed_Manual"].ToDouble() ? (!(viewData["SpeedScale"] == "M") ? Math.Round(1.0 / num4).ToLong() : Math.Round(1.0 / num3).ToLong()) : (!(viewData["SpeedScale"] == "M") ? Math.Round(1.0 / num2).ToLong() : Math.Round(1.0 / num1).ToLong());
    }
    if (!string.IsNullOrEmpty(collection["MinSpeed_Manual"]))
    {
      double num5 = collection["MinSpeed_Manual"].ToDouble() * 0.000474;
      double num6 = collection["MinSpeed_Manual"].ToDouble() * 0.000295;
      sensor.Calibration3 = !(viewData["SpeedScale"] == "M") ? (1.0 / num6).ToLong() : Math.Round(1.0 / num5).ToLong();
    }
    if (!string.IsNullOrEmpty(collection["IsZeroing"]) && collection["IsZeroing"] == "on")
      sensor.Calibration4 = 1L;
    else
      sensor.Calibration4 = 0L;
  }

  public static string MinThreshForUI(Sensor sensor) => sensor.MinimumThreshold.ToString();

  public static string MaxThreshForUI(Sensor sensor) => sensor.MaximumThreshold.ToString();

  public static string GetLabel() => "";

  public static string HystForUI(Sensor sensor)
  {
    return ((double) VehicleDetector.HystUpper(sensor) / sensor.MaximumThreshold.ToDouble() * 100.0).ToInt().ToString();
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public static int HystUpper(Sensor sensor)
  {
    return Convert.ToInt32((sensor.Hysteresis & 4294901760L) >> 16 /*0x10*/);
  }

  public static void HystUpper(Sensor sensor, int value)
  {
    uint num = (uint) (sensor.Hysteresis.ToInt() & (int) ushort.MaxValue);
    sensor.Hysteresis = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int HystLower(Sensor sensor)
  {
    return Convert.ToInt32(sensor.Hysteresis & (long) ushort.MaxValue);
  }

  public static void HystLower(Sensor sensor, int value)
  {
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.Hysteresis) & 4294901760UL);
    sensor.Hysteresis = (long) (num | (uint) value);
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      numArrayList.Add(VehicleDetectorBase.CalibrateFrame(sensor.SensorID));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(VehicleDetector left, VehicleDetector right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(VehicleDetector left, VehicleDetector right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(VehicleDetector left, VehicleDetector right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(VehicleDetector left, VehicleDetector right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(VehicleDetector left, VehicleDetector right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(VehicleDetector left, VehicleDetector right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is VehicleDetector && this.Equals((MonnitApplicationBase) (obj as VehicleDetector));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is VehicleDetector && this.Count == (right as VehicleDetector).Count;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is VehicleDetector && this.Count != (right as VehicleDetector).Count;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is VehicleDetector && this.Count < (right as VehicleDetector).Count;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is VehicleDetector && this.Count <= (right as VehicleDetector).Count;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is VehicleDetector && this.Count > (right as VehicleDetector).Count;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is VehicleDetector && this.Count >= (right as VehicleDetector).Count;
  }
}
