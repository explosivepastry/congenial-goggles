// Decompiled with JetBrains decompiler
// Type: Monnit.Seat
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

public class Seat : MonnitApplicationBase
{
  public static long MonnitApplicationID => 51;

  public static string ApplicationName => "Seat Occupancy";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Point";

  public override long ApplicationID => Seat.MonnitApplicationID;

  public double KOhms { get; set; }

  public bool Occupied { get; set; }

  public override string Serialize() => $"{this.Occupied.ToString()},{this.KOhms.ToString()}";

  public static string GetLabel(long sensorID) => "";

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.SeatOccupied, "Occupied", this.Occupied),
        new AppDatum(eDatumType.ResistanceData, "KOhms", this.KOhms)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      this.PlotValue,
      (object) this.KOhms
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Seat.Deserialize(version, serialized);
  }

  public override bool IsValid => true;

  public override string NotificationString => this.Occupied ? "Occupied" : "Not Occupied";

  public override object PlotValue => (object) (this.Occupied ? 1 : 0);

  public static Seat Deserialize(string version, string serialized)
  {
    Seat seat = new Seat();
    string[] strArray = serialized.Split(',');
    seat.Occupied = strArray[0].ToBool();
    seat.KOhms = strArray.Length <= 1 ? strArray[0].ToDouble() : strArray[1].ToDouble();
    return seat;
  }

  public static Seat Create(byte[] sdm, int startIndex)
  {
    return new Seat()
    {
      Occupied = Convert.ToBoolean(sdm[startIndex]),
      KOhms = BitConverter.ToUInt32(sdm, startIndex + 1).ToDouble() / 1000.0
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
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Equal,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = Seat.MonnitApplicationID,
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
    Dictionary<string, object> defaults = SeatBase.GetDefaults(new Version(sensor.FirmwareVersion), sensor.GenerationType);
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      sensor.MaximumThreshold = (collection["MaximumThreshold_Manual"].ToDouble() * 1000.0).ToLong();
      if (sensor.MaximumThreshold < defaults["DefaultMinimumThreshold"].ToLong())
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MaximumThreshold > 10000L)
        sensor.MaximumThreshold = 10000L;
    }
    else if (((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = 5000L;
    if (!string.IsNullOrEmpty(collection["Calibration1_Manual"]))
    {
      sensor.Calibration1 = (collection["Calibration1_Manual"].ToDouble() * 1000.0).ToLong();
    }
    else
    {
      if (!((IEnumerable<string>) collection.AllKeys).Contains<string>("Calibration1_Manual"))
        return;
      sensor.Calibration1 = 5000L;
    }
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return (sensor.MaximumThreshold == (long) uint.MaxValue ? 2.0 : sensor.MaximumThreshold.ToDouble() / 1000.0).ToString();
  }

  public static string EventDetectionTypeDescription => "Enter aware state when seat is ";

  public static string ValueForZero => "Not Occupied";

  public static string ValueForOne => "Occupied";

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Seat left, Seat right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Seat left, Seat right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Seat left, Seat right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Seat left, Seat right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Seat left, Seat right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Seat left, Seat right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Seat && this.Equals((MonnitApplicationBase) (obj as Seat));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Seat && this.Occupied == (right as Seat).Occupied;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Seat && this.Occupied != (right as Seat).Occupied;
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
