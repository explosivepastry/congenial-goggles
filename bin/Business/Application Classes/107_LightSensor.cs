// Decompiled with JetBrains decompiler
// Type: Monnit.LightSensor
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

public class LightSensor : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _DataDisplay;

  public static long MonnitApplicationID => 107;

  public static string ApplicationName => "Light Sensor";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => LightSensor.MonnitApplicationID;

  public double LuxReading { get; set; }

  public bool Light { get; set; }

  public int stsState { get; set; }

  public static LightSensor Create(byte[] sdm, int startIndex)
  {
    return new LightSensor()
    {
      stsState = (int) sdm[startIndex - 1] >> 4,
      LuxReading = (double) BitConverter.ToUInt32(sdm, startIndex) / 100.0,
      Light = sdm[startIndex + 4].ToBool()
    };
  }

  public override string Serialize()
  {
    return $"{this.LuxReading.ToString()}|{this.Light.ToString()}|{this.stsState.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) LightSensor.Deserialize(version, serialized);
  }

  public static LightSensor Deserialize(string version, string serialized)
  {
    LightSensor lightSensor = new LightSensor();
    if (string.IsNullOrEmpty(serialized))
    {
      lightSensor.LuxReading = 0.0;
      lightSensor.Light = false;
      lightSensor.stsState = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      lightSensor.LuxReading = strArray[0].ToDouble();
      if (strArray.Length > 1)
      {
        lightSensor.Light = strArray[1].ToBool();
        try
        {
          lightSensor.stsState = strArray[2].ToInt();
        }
        catch
        {
          lightSensor.stsState = 0;
        }
      }
      else
      {
        lightSensor.Light = strArray[0].ToBool();
        lightSensor.stsState = 0;
      }
    }
    return lightSensor;
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.LuxData, "Lux", this.LuxReading),
        new AppDatum(eDatumType.LightDetect, "Light", this.Light)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[2]
    {
      (object) this.LuxReading,
      (object) (this.Light ? 1 : 0)
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[2]
    {
      "Lux",
      "Light"
    });
  }

  public override object PlotValue => (object) this.LuxReading;

  public override string NotificationString
  {
    get
    {
      string notificationString;
      switch (this.DataDisplay.Value.ToInt())
      {
        case 0:
          notificationString = this.LuxReading.ToString() + " lux";
          break;
        case 1:
          notificationString = $"{this.LuxReading.ToString()} lux, {(this.Light ? "Light" : "No Light")}";
          break;
        case 2:
          notificationString = this.Light ? "Light" : "No Light";
          break;
        default:
          notificationString = this.LuxReading.ToString() + " lux";
          break;
      }
      if (this.stsState == 1)
        notificationString = "Hardware Failure";
      return notificationString;
    }
  }

  public SensorAttribute DataDisplay
  {
    get
    {
      if (this._DataDisplay == null)
        this._DataDisplay = new SensorAttribute()
        {
          Name = nameof (DataDisplay),
          Value = "0"
        };
      return this._DataDisplay;
    }
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "DataDisplay")
        this._DataDisplay = sensorAttribute;
    }
  }

  public static int GetShowDataValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "DataDisplay")
        return sensorAttribute.Value.ToInt();
    }
    return 0;
  }

  public static void SetShowDataValue(long sensorID, int dataDisplay)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "DataDisplay")
      {
        sensorAttribute.Value = dataDisplay.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "DataDisplay",
        Value = dataDisplay.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
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
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = LightSensor.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    double result;
    if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
    {
      long num = collection["Hysteresis_Manual"].ToLong();
      if (num < 0L)
        num = 0L;
      if (num > 10000L)
        num = 10000L;
      sensor.Hysteresis = num * 100L;
    }
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      sensor.MinimumThreshold = collection["MinimumThreshold_Manual"].ToLong() * 100L;
      if (sensor.MinimumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MinimumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      sensor.MaximumThreshold = collection["MaximumThreshold_Manual"].ToLong() * 100L;
      if (sensor.MaximumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MaximumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      if (sensor.MinimumThreshold > sensor.MaximumThreshold)
        sensor.MaximumThreshold = sensor.MinimumThreshold;
    }
    if (!string.IsNullOrEmpty(collection["DeltaLightReport_Manual"]))
    {
      long num = collection["DeltaLightReport_Manual"].ToLong();
      if (num < 0L)
        num = 0L;
      if (num > 80000L)
        num = 80000L;
      sensor.Calibration2 = num * 100L;
    }
    if (string.IsNullOrEmpty(collection["FullNotiString"]))
      return;
    int dataDisplay = collection["FullNotiString"].ToInt();
    LightSensor.SetShowDataValue(sensor.SensorID, dataDisplay);
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
  }

  public new static void DuplicateConfiguration(Sensor source, Sensor target)
  {
    target.ReportInterval = source.ReportInterval;
    target.ActiveStateInterval = source.ActiveStateInterval;
    target.MinimumCommunicationFrequency = source.MinimumCommunicationFrequency;
    target.ChannelMask = source.ChannelMask;
    target.StandardMessageDelay = source.StandardMessageDelay;
    target.TransmitIntervalLink = source.TransmitIntervalLink;
    target.TransmitIntervalTest = source.TransmitIntervalTest;
    target.TestTransmitCount = source.TestTransmitCount;
    target.MaximumNetworkHops = source.MaximumNetworkHops;
    target.RetryCount = source.RetryCount;
    target.Recovery = source.Recovery;
    target.MeasurementsPerTransmission = source.MeasurementsPerTransmission;
    target.TransmitOffset = source.TransmitOffset;
    target.Hysteresis = source.Hysteresis;
    target.MinimumThreshold = source.MinimumThreshold;
    target.MaximumThreshold = source.MaximumThreshold;
    target.Calibration2 = source.Calibration2;
    target.Calibration3 = source.Calibration3;
    target.Calibration4 = source.Calibration4;
    target.EventDetectionType = source.EventDetectionType;
    target.EventDetectionPeriod = source.EventDetectionPeriod;
    target.EventDetectionCount = source.EventDetectionCount;
    target.RearmTime = source.RearmTime;
    target.BiStable = source.BiStable;
    target.TagString = source.TagString;
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : ((double) sensor.Hysteresis / 100.0).ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : ((double) sensor.MinimumThreshold / 100.0).ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : ((double) sensor.MaximumThreshold / 100.0).ToString();
  }

  public static string DeltaLightReportForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "0" : ((double) sensor.Calibration2 / 100.0).ToString();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(LightSensor left, LightSensor right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(LightSensor left, LightSensor right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(LightSensor left, LightSensor right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(LightSensor left, LightSensor right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(LightSensor left, LightSensor right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(LightSensor left, LightSensor right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is LightSensor && this.Equals((MonnitApplicationBase) (obj as LightSensor));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is LightSensor && this.LuxReading == (right as LightSensor).LuxReading;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is LightSensor && this.LuxReading != (right as LightSensor).LuxReading;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is LightSensor && this.LuxReading < (right as LightSensor).LuxReading;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is LightSensor && this.LuxReading <= (right as LightSensor).LuxReading;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is LightSensor && this.LuxReading > (right as LightSensor).LuxReading;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is LightSensor && this.LuxReading >= (right as LightSensor).LuxReading;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 82000;

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
