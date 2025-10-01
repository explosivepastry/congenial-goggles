// Decompiled with JetBrains decompiler
// Type: Monnit.VehiclePresence
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class VehiclePresence : MonnitApplicationBase
{
  public static long MonnitApplicationID => 33;

  public static string ApplicationName => "Vehicle Detection";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Point";

  public override long ApplicationID => VehiclePresence.MonnitApplicationID;

  public int Strength { get; set; }

  public int State { get; set; }

  public override string Serialize()
  {
    string str;
    switch (this.State >> 4)
    {
      case 0:
        str = "Unoccupied";
        break;
      case 1:
        str = "Occupied";
        break;
      case 2:
        str = "Unknown State";
        break;
      case 15:
        str = "Hardware Failure";
        break;
      default:
        str = "SENSOR FAILURE";
        break;
    }
    return $"{str},{this.Strength.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.VehicleDetect, "Vehicle State", this.State),
        new AppDatum(eDatumType.Magnitude, "Strength", this.Strength)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      this.PlotValue,
      (object) this.Strength
    };
  }

  public static List<object> GetPlotlabels(long sensorID)
  {
    return new List<object>()
    {
      (object) "Vehicle State",
      (object) "Strength"
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) VehiclePresence.Deserialize(version, serialized);
  }

  public override bool IsValid
  {
    get
    {
      int num = this.State >> 4;
      return num == 0 || num == 1;
    }
  }

  public override string NotificationString
  {
    get
    {
      string notificationString;
      switch (this.State >> 4)
      {
        case 0:
          notificationString = "Unoccupied";
          break;
        case 1:
          notificationString = "Occupied";
          break;
        case 2:
          notificationString = "Unknown State";
          break;
        case 15:
          notificationString = "Hardware Failure";
          break;
        default:
          notificationString = "SENSOR FAILURE";
          break;
      }
      return notificationString;
    }
  }

  public override object PlotValue => (object) this.State;

  public static VehiclePresence Deserialize(string version, string serialized)
  {
    VehiclePresence vehiclePresence = new VehiclePresence();
    if (string.IsNullOrEmpty(serialized))
    {
      vehiclePresence.Strength = 0;
      vehiclePresence.State = 32 /*0x20*/;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      if (strArray.Length == 2)
      {
        vehiclePresence.State = !(strArray[0] == "Unoccupied") ? (!(strArray[0] == "Occupied") ? (!(strArray[0] == "Unknown State") ? (!(strArray[0] == "Hardware Failure") ? 15 : 240 /*0xF0*/) : 32 /*0x20*/) : 16 /*0x10*/) : 0;
        vehiclePresence.Strength = strArray[1].ToInt();
      }
      else
      {
        vehiclePresence.Strength = 0;
        switch (strArray[0])
        {
          case "0":
            vehiclePresence.State = 0;
            break;
          case "1":
          case "16":
            vehiclePresence.State = 16 /*0x10*/;
            break;
          default:
            vehiclePresence.State = 32 /*0x20*/;
            break;
        }
      }
    }
    return vehiclePresence;
  }

  public static VehiclePresence Create(byte[] sdm, int startIndex)
  {
    VehiclePresence vehiclePresence = new VehiclePresence();
    byte[] destinationArray = new byte[2];
    Array.Copy((Array) sdm, startIndex, (Array) destinationArray, 0, 2);
    vehiclePresence.Strength = (int) BitConverter.ToUInt16(destinationArray, 0);
    vehiclePresence.State = (int) sdm[startIndex - 1];
    return vehiclePresence;
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
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = VehiclePresence.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion,
      CompareValue = "True"
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      long num = collection["MaximumThreshold_Manual"].ToLong();
      if (num >= 10L && num <= 3000L)
        sensor.MaximumThreshold = num;
    }
    double result = 0.0;
    if (string.IsNullOrEmpty(collection["SampleInterval_Manual"]) || !double.TryParse(collection["SampleInterval_Manual"], out result))
      return;
    if (result <= 24.0)
    {
      sensor.MinimumCommunicationFrequency = result.ToInt();
      sensor.Calibration1 = (long) result.ToInt();
    }
    else
    {
      sensor.MinimumCommunicationFrequency = 1;
      sensor.Calibration1 = (long) result.ToInt();
    }
  }

  public static string HystForUI(Sensor sensor) => "Not Used";

  public static string MinThreshForUI(Sensor sensor) => "Not Used";

  public static string MaxThreshForUI(Sensor sensor) => sensor.MaximumThreshold.ToString();

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(VehiclePresence left, VehiclePresence right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(VehiclePresence left, VehiclePresence right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(VehiclePresence left, VehiclePresence right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(VehiclePresence left, VehiclePresence right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(VehiclePresence left, VehiclePresence right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(VehiclePresence left, VehiclePresence right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is VehiclePresence && this.Equals((MonnitApplicationBase) (obj as VehiclePresence));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is VehiclePresence && this.State == (right as VehiclePresence).State;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is VehiclePresence && this.State != (right as VehiclePresence).State;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is VehiclePresence && this.State < (right as VehiclePresence).State;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is VehiclePresence && this.State <= (right as VehiclePresence).State;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is VehiclePresence && this.State > (right as VehiclePresence).State;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is VehiclePresence && this.State >= (right as VehiclePresence).State;
  }
}
