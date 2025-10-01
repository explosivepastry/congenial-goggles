// Decompiled with JetBrains decompiler
// Type: Monnit.Light
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;

#nullable disable
namespace Monnit;

public class Light : MonnitApplicationBase
{
  public static long MonnitApplicationID => 8;

  public static string ApplicationName => nameof (Light);

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => Light.MonnitApplicationID;

  public double PercentLight { get; set; }

  public override string Serialize() => this.PercentLight.ToString();

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.Percentage, "Percent Light", this.PercentLight)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[1]
    {
      "Percent Light"
    });
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Light.Deserialize(version, serialized);
  }

  public override string NotificationString => this.range.ToString().Replace("_", " ");

  public override object PlotValue => (object) this.PercentLight;

  public static Light Deserialize(string version, string serialized)
  {
    return new Light()
    {
      PercentLight = serialized.ToDouble()
    };
  }

  public static Light Create(byte[] sdm, int startIndex)
  {
    return new Light()
    {
      PercentLight = BitConverter.ToUInt16(sdm, startIndex).ToDouble() / 10.0
    };
  }

  private eLightRange range
  {
    get
    {
      if (this.PercentLight <= 0.0)
        return eLightRange.No_Light;
      if (this.PercentLight <= 33.0)
        return eLightRange.Low_Light;
      return this.PercentLight <= 66.0 ? eLightRange.Medium_Light : eLightRange.Full_Light;
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
    sensor.Calibration1 = 27500L;
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
      CompareValue = eLightRange.No_Light.ToString(),
      AccountID = sensor.AccountID,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = Light.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
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
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
      sensor.Hysteresis = (collection["Hysteresis_Manual"].ToDouble() * 10.0).ToLong();
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
      sensor.MinimumThreshold = (collection["MinimumThreshold_Manual"].ToDouble() * 10.0).ToLong();
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
      sensor.MaximumThreshold = (collection["MaximumThreshold_Manual"].ToDouble() * 10.0).ToLong();
    if (string.IsNullOrEmpty(collection["BasicThreshold"]) || string.IsNullOrEmpty(collection["BasicThresholdDirection"]) || !(collection["BasicThresholdDirection"] != "-1"))
      return;
    if (collection["BasicThresholdDirection"].ToInt() == 0)
    {
      sensor.MinimumThreshold = (collection["BasicThreshold"].ToDouble() * 10.0).ToLong();
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    }
    else if (collection["BasicThresholdDirection"].ToInt() == 1)
    {
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      sensor.MaximumThreshold = (collection["BasicThreshold"].ToDouble() * 10.0).ToLong();
    }
    sensor.MeasurementsPerTransmission = 6;
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      byte[] destinationArray = new byte[6];
      destinationArray[0] = (byte) 116;
      Array.Copy((Array) BitConverter.GetBytes(sensor.SensorID), 0, (Array) destinationArray, 1, 4);
      destinationArray[5] = (byte) 3;
      numArrayList.Add(destinationArray);
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : (Convert.ToDouble(sensor.Hysteresis) / 10.0).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string GetLabel() => "";

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : (Convert.ToDouble(sensor.MinimumThreshold) / 10.0).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : (Convert.ToDouble(sensor.MaximumThreshold) / 10.0).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Light left, Light right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Light left, Light right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Light left, Light right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Light left, Light right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Light left, Light right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Light left, Light right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Light && this.Equals((MonnitApplicationBase) (obj as Light));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Light && this.range == (right as Light).range;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Light && this.range != (right as Light).range;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Light && this.range < (right as Light).range;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Light && this.range <= (right as Light).range;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Light && this.range > (right as Light).range;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Light && this.range >= (right as Light).range;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 100;

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.Calibration1 = 27500L;
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
  }
}
