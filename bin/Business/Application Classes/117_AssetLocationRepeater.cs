// Decompiled with JetBrains decompiler
// Type: Monnit.AssetLocationRepeater
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class AssetLocationRepeater : MonnitApplicationBase
{
  public static long MonnitApplicationID => 117;

  public static string ApplicationName => "Asset Location Repeater";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "None";

  public override long ApplicationID => AssetLocationRepeater.MonnitApplicationID;

  public long TagID { get; set; }

  public int MessageID { get; set; }

  public int RSSI { get; set; }

  public int TagCount { get; set; }

  public int stsStatus { get; set; }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[4]
      {
        new AppDatum(eDatumType.Count, "TagID", this.TagID),
        new AppDatum(eDatumType.Count, "MessageID", this.MessageID),
        new AppDatum(eDatumType.Count, "RSSI", this.RSSI),
        new AppDatum(eDatumType.Count, "TagCount", this.TagCount)
      });
    }
  }

  public override string Serialize()
  {
    return $"{this.TagID.ToString()}|{this.MessageID.ToString()}|{this.RSSI.ToString()}|{this.TagCount.ToString()}|{this.stsStatus.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) AssetLocationRepeater.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      return $"TagID: {this.TagID.ToString()} MessageID: {this.MessageID.ToString()} RSSI: {this.RSSI.ToString()} Tag Count: {this.TagCount.ToString()}";
    }
  }

  public override object PlotValue => (object) this.TagID;

  public static AssetLocationRepeater Deserialize(string version, string serialized)
  {
    AssetLocationRepeater locationRepeater = new AssetLocationRepeater();
    if (string.IsNullOrEmpty(serialized))
    {
      locationRepeater.stsStatus = 0;
      locationRepeater.TagID = 0L;
      locationRepeater.MessageID = 0;
      locationRepeater.RSSI = 0;
      locationRepeater.TagCount = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length > 1)
      {
        try
        {
          locationRepeater.TagID = strArray[0].ToLong();
          locationRepeater.MessageID = strArray[1].ToInt();
          locationRepeater.RSSI = strArray[2].ToInt();
          locationRepeater.TagCount = strArray[3].ToInt();
          try
          {
            locationRepeater.stsStatus = strArray[4].ToInt();
          }
          catch
          {
            locationRepeater.stsStatus = 0;
          }
        }
        catch
        {
          locationRepeater.stsStatus = 0;
          locationRepeater.TagID = 0L;
          locationRepeater.MessageID = 0;
          locationRepeater.RSSI = 0;
          locationRepeater.TagCount = 0;
        }
      }
      else
      {
        locationRepeater.TagID = strArray[0].ToLong();
        locationRepeater.MessageID = strArray[0].ToInt();
        locationRepeater.RSSI = strArray[0].ToInt();
        locationRepeater.TagCount = strArray[0].ToInt();
      }
    }
    return locationRepeater;
  }

  public static AssetLocationRepeater Create(byte[] sdm, int startIndex)
  {
    return new AssetLocationRepeater()
    {
      stsStatus = (int) sdm[startIndex - 1] >> 4,
      TagID = (long) BitConverter.ToUInt32(sdm, startIndex),
      MessageID = (int) BitConverter.ToUInt16(sdm, startIndex + 4),
      RSSI = (int) (sbyte) sdm[startIndex + 6],
      TagCount = (int) BitConverter.ToUInt16(sdm, startIndex + 7)
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
      ApplicationID = AssetLocationRepeater.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
  }

  public static string HystForUI(Sensor sensor) => "Not Used";

  public static string MinThreshForUI(Sensor sensor) => "Not Used";

  public static string MaxThreshForUI(Sensor sensor) => "Not Used";

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(AssetLocationRepeater left, AssetLocationRepeater right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(AssetLocationRepeater left, AssetLocationRepeater right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(AssetLocationRepeater left, AssetLocationRepeater right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(AssetLocationRepeater left, AssetLocationRepeater right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(AssetLocationRepeater left, AssetLocationRepeater right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(AssetLocationRepeater left, AssetLocationRepeater right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is AssetLocationRepeater && this.Equals((MonnitApplicationBase) (obj as AssetLocationRepeater));
  }

  public override bool Equals(MonnitApplicationBase right) => false;

  public override bool NotEqual(MonnitApplicationBase right) => false;

  public override bool LessThan(MonnitApplicationBase right) => false;

  public override bool LessThanEqual(MonnitApplicationBase right) => false;

  public override bool GreaterThan(MonnitApplicationBase right) => false;

  public override bool GreaterThanEqual(MonnitApplicationBase right) => false;
}
