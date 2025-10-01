// Decompiled with JetBrains decompiler
// Type: Monnit.CarDetect
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class CarDetect : MonnitApplicationBase
{
  public static long MonnitApplicationID => 119;

  public static string ApplicationName => "Car Detect";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Point";

  public override long ApplicationID => CarDetect.MonnitApplicationID;

  public int Detected { get; set; }

  public int Count { get; set; }

  public override string Serialize()
  {
    int num = this.Detected;
    string str1 = num.ToString();
    num = this.Count;
    string str2 = num.ToString();
    return $"{str1}|{str2}";
  }

  public static CarDetect Deserialize(string version, string serialized)
  {
    CarDetect carDetect = new CarDetect();
    if (string.IsNullOrEmpty(serialized))
    {
      carDetect.Detected = 0;
      carDetect.Count = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      int num = strArray[0].ToString() == "True" ? 1 : (strArray[0].ToString() == "False" ? 1 : 0);
      carDetect.Detected = num == 0 ? strArray[0].ToInt() : strArray[0].ToBool().ToInt();
      carDetect.Count = strArray.Length <= 1 ? strArray[0].ToInt() : strArray[1].ToInt();
    }
    return carDetect;
  }

  public static CarDetect Create(byte[] sdm, int startIndex)
  {
    return new CarDetect()
    {
      Detected = (int) sdm[startIndex],
      Count = (int) BitConverter.ToUInt16(sdm, startIndex + 1)
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) CarDetect.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      switch (this.Detected)
      {
        case 0:
          return "No Car Detection";
        case 1:
          return "Car Detected";
        case 2:
          return $"{this.Count} Cars";
        default:
          return "Unknown";
      }
    }
  }

  public override object PlotValue => (object) this.Detected;

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.VehicleDetect, nameof (CarDetect), this.Detected),
        new AppDatum(eDatumType.Count, "Count", this.Count)
      });
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
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Less_Than,
      CompareValue = "True",
      NotificationClass = eNotificationClass.Application,
      ApplicationID = CarDetect.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    sensor.ActiveStateInterval = sensor.ReportInterval;
    if (!string.IsNullOrEmpty(collection["Mode"]))
      sensor.Calibration3 = collection["Mode"].ToLong();
    if (!string.IsNullOrEmpty(collection["Detection"]))
    {
      double num = collection["Detection"].ToDouble();
      sensor.Calibration1 = (num * 1000.0).ToLong();
    }
    else
      sensor.Calibration1 = 3000L;
    if (string.IsNullOrEmpty(collection["RearmWindow"]))
      return;
    double num1 = collection["RearmWindow"].ToDouble();
    sensor.Calibration2 = (num1 * 1000.0).ToLong();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(CarDetect left, CarDetect right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(CarDetect left, CarDetect right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(CarDetect left, CarDetect right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(CarDetect left, CarDetect right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(CarDetect left, CarDetect right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(CarDetect left, CarDetect right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is CarDetect && this.Equals((MonnitApplicationBase) (obj as CarDetect));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is CarDetect && this.Count == (right as CarDetect).Count;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is CarDetect && this.Count != (right as CarDetect).Count;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<' cannot be applied to operands of type 'bool' and 'bool'");
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<=' cannot be applied to operands of type 'bool' and 'bool'");
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>' cannot be applied to operands of type 'bool' and 'bool'");
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>=' cannot be applied to operands of type 'bool' and 'bool'");
  }
}
