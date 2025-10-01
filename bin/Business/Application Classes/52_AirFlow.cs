// Decompiled with JetBrains decompiler
// Type: Monnit.AirFlow
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

public class AirFlow : MonnitApplicationBase
{
  public static long MonnitApplicationID => 52;

  public static string ApplicationName => "Air Flow Detection";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => AirFlow.MonnitApplicationID;

  public double KOhms { get; set; }

  public bool Flowing { get; set; }

  public override string Serialize() => $"{this.Flowing.ToString()},{this.KOhms.ToString()}";

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.AirflowDetect, "Flowing", this.Flowing),
        new AppDatum(eDatumType.ResistanceData, "KOhms", this.KOhms)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[2]
    {
      this.PlotValue,
      (object) this.KOhms
    });
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) AirFlow.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      return $"{(this.Flowing ? (object) "Moving Air" : (object) "Still Air")} ({Math.Round(this.KOhms, 2)} KOhms)";
    }
  }

  public override object PlotValue => (object) (this.Flowing ? 1 : 0);

  public static AirFlow Deserialize(string version, string serialized)
  {
    AirFlow airFlow = new AirFlow();
    if (string.IsNullOrEmpty(serialized))
    {
      airFlow.KOhms = 0.0;
      airFlow.Flowing = false;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      airFlow.Flowing = strArray[0].ToBool();
      airFlow.KOhms = strArray.Length <= 1 ? strArray[0].ToDouble() : strArray[1].ToDouble();
    }
    return airFlow;
  }

  public static AirFlow Create(byte[] sdm, int startIndex)
  {
    return new AirFlow()
    {
      Flowing = Convert.ToBoolean(sdm[startIndex]),
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
      ApplicationID = AirFlow.MonnitApplicationID,
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
    double result = 0.0;
    if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      sensor.MinimumThreshold = (collection["MinimumThreshold_Manual"].ToDouble() * 1000.0).ToLong();
      if (sensor.MinimumThreshold < 0L)
        sensor.MinimumThreshold = 0L;
      if (sensor.MinimumThreshold > 100000L)
        sensor.MinimumThreshold = 100000L;
    }
    else if (((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
      sensor.MinimumThreshold = 1500L;
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
      sensor.Hysteresis = collection["Hysteresis_Manual"].ToLong();
    else if (((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
      sensor.Hysteresis = 0L;
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      sensor.MaximumThreshold = (collection["MaximumThreshold_Manual"].ToDouble() * 1000.0).ToLong();
      if (sensor.MaximumThreshold < 0L)
        sensor.MaximumThreshold = 0L;
      if (sensor.MaximumThreshold > 100000L)
        sensor.MaximumThreshold = 100000L;
      if (sensor.MinimumThreshold > sensor.MaximumThreshold)
        sensor.MaximumThreshold = sensor.MinimumThreshold;
    }
    else if (((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = 5000L;
    if (!string.IsNullOrEmpty(collection["Calibration1_Manual"]))
    {
      double o = (double) (collection["Calibration1_Manual"].ToDouble() * 1000.0).ToLong();
      if (o < 0.0)
        o = 0.0;
      if (o > 600000.0)
        o = 600000.0;
      sensor.Calibration1 = o.ToLong();
    }
    else
    {
      if (!((IEnumerable<string>) collection.AllKeys).Contains<string>("Calibration1_Manual"))
        return;
      sensor.Calibration1 = 1000L;
    }
  }

  public static string HystForUI(Sensor sensor) => sensor.Hysteresis.ToString();

  public static string MinThreshForUI(Sensor sensor)
  {
    return ((double) sensor.MinimumThreshold / 1000.0).ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return ((double) sensor.MaximumThreshold / 1000.0).ToString();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(AirFlow left, AirFlow right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(AirFlow left, AirFlow right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(AirFlow left, AirFlow right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(AirFlow left, AirFlow right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(AirFlow left, AirFlow right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(AirFlow left, AirFlow right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is AirFlow && this.Equals((MonnitApplicationBase) (obj as AirFlow));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is AirFlow && this.Flowing == (right as AirFlow).Flowing;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is AirFlow && this.Flowing != (right as AirFlow).Flowing;
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

  public static string GetLabel(long sensorID) => "";
}
