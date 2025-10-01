// Decompiled with JetBrains decompiler
// Type: Monnit.AirQuality
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

public class AirQuality : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _Label;
  private SensorAttribute _ShowFullDataValue;

  public static long MonnitApplicationID => AirQualityBase.MonnitApplicationID;

  public static string ApplicationName => "Air Quality";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => AirQuality.MonnitApplicationID;

  public int PM25 { get; set; }

  public int PM10 { get; set; }

  public int PM1 { get; set; }

  public int stsState { get; set; }

  public static AirQuality Create(byte[] sdm, int startIndex)
  {
    AirQuality airQuality = new AirQuality();
    airQuality.stsState = (int) sdm[startIndex - 1] >> 4;
    if (sdm.Length > 7)
    {
      airQuality.PM1 = BitConverter.ToUInt16(sdm, startIndex).ToInt();
      airQuality.PM25 = BitConverter.ToUInt16(sdm, startIndex + 2).ToInt();
      airQuality.PM10 = BitConverter.ToUInt16(sdm, startIndex + 4).ToInt();
    }
    else
    {
      airQuality.PM25 = BitConverter.ToUInt16(sdm, startIndex).ToInt();
      airQuality.PM10 = BitConverter.ToUInt16(sdm, startIndex + 2).ToInt();
      airQuality.PM1 = BitConverter.ToUInt16(sdm, startIndex + 4).ToInt();
    }
    return airQuality;
  }

  public override string Serialize()
  {
    return $"{this.PM25.ToString()}|{this.PM10.ToString()}|{this.PM1.ToString()}|{this.stsState.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[3]
      {
        new AppDatum(eDatumType.Micrograms, "PM1", this.PM1),
        new AppDatum(eDatumType.Micrograms, "PM2.5", this.PM25),
        new AppDatum(eDatumType.Micrograms, "PM10", this.PM10)
      });
    }
  }

  public static AirQuality Deserialize(string version, string serialized)
  {
    AirQuality airQuality = new AirQuality();
    if (string.IsNullOrEmpty(serialized))
    {
      airQuality.PM25 = 0;
      airQuality.PM10 = 0;
      airQuality.PM1 = 0;
      airQuality.stsState = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      airQuality.PM25 = strArray[0].ToInt();
      if (strArray.Length > 1)
      {
        airQuality.PM10 = strArray[1].ToInt();
        airQuality.PM1 = strArray[2].ToInt();
      }
      else
      {
        airQuality.PM10 = strArray[0].ToInt();
        airQuality.PM1 = strArray[0].ToInt();
      }
      try
      {
        airQuality.stsState = strArray[3].ToInt();
      }
      catch
      {
        airQuality.stsState = 0;
      }
    }
    return airQuality;
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) AirQuality.Deserialize(version, serialized);
  }

  public SensorAttribute Label
  {
    get
    {
      if (this._Label == null)
        this._Label = new SensorAttribute()
        {
          Value = "μg/m^3"
        };
      return this._Label;
    }
  }

  public SensorAttribute ShowFullDataValue
  {
    get
    {
      if (this._ShowFullDataValue == null)
        this._ShowFullDataValue = new SensorAttribute()
        {
          Name = nameof (ShowFullDataValue),
          Value = "false"
        };
      return this._ShowFullDataValue;
    }
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        this._Label = sensorAttribute;
      if (sensorAttribute.Name == "ShowFullDataValue")
        this._ShowFullDataValue = sensorAttribute;
    }
  }

  public static bool GetShowFullDataValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ShowFullDataValue")
        return sensorAttribute.Value.ToBool();
    }
    return false;
  }

  public static void SetShowFullDataValue(long sensorID, bool showFullData)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ShowFullDataValue")
      {
        sensorAttribute.Value = showFullData.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "ShowFullDataValue",
        Value = showFullData.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetLabel(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        return sensorAttribute.Value;
    }
    return "μg/m^3";
  }

  public static void SetLabel(long sensorID, string label)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
      {
        sensorAttribute.Value = label;
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "Label",
        Value = label,
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[4]
    {
      this.PlotValue,
      (object) this.PM1,
      (object) this.PM25,
      (object) this.PM10
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[4]
    {
      AirQuality.GetLabel(sensorID),
      "PM1",
      "PM2.5",
      "PM10"
    });
  }

  public override object PlotValue => (object) this.PM25;

  public override string NotificationString
  {
    get
    {
      if (this.stsState > 0 && this.stsState < 6)
        return "Hardware Failure";
      if (!(this.ShowFullDataValue.Value.ToLower() == "true"))
        return $"PM2.5: {this.PlotValue.ToInt()} {this.Label.Value}";
      return string.Format("PM1: {1} {0} , PM2.5: {2} {0} , PM10: {3} {0}", (object) this.Label.Value, (object) this.PM1, (object) this.PlotValue.ToDouble(), (object) this.PM10);
    }
  }

  public static string SpecialExportValue(MonnitApplicationBase app)
  {
    return $"\"{((AirQuality) app).PM1}\",\"{((AirQuality) app).PM25}\",\"{((AirQuality) app).PM10}\"";
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

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      AccountID = sensor.AccountID,
      CompareValue = "0",
      NotificationClass = eNotificationClass.Application,
      ApplicationID = AirQuality.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void VerifyNotificationValues(Notification notification, string scale)
  {
    int num = notification.CompareValue.ToInt();
    if (num > 1000)
      num = 1000;
    if (num < 0)
      num = 0;
    notification.CompareValue = num.ToString();
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["BasicThreshold"]) && !string.IsNullOrEmpty(collection["BasicThresholdDirection"]) && collection["BasicThresholdDirection"] != "-1")
    {
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
      if (collection["BasicThresholdDirection"].ToInt() == 0)
      {
        sensor.MinimumThreshold = collection["BasicThreshold"].ToDouble().ToLong();
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      }
      else if (collection["BasicThresholdDirection"].ToInt() == 1)
      {
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
        sensor.MaximumThreshold = collection["BasicThreshold"].ToDouble().ToLong();
      }
      sensor.MeasurementsPerTransmission = 1;
    }
    else
    {
      double result;
      if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
        sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
      if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      if (double.TryParse(collection["Hysteresis_Manual"], out result))
      {
        ushort uint16 = collection["Hysteresis_Manual"].ToUInt16();
        sensor.Hysteresis = uint16 >= (ushort) 0 ? (uint16 <= (ushort) 1000 ? (long) uint16.ToUInt16() : 1000L) : 0L;
      }
      if (double.TryParse(collection["MinimumThreshold_Manual"], out result))
      {
        ushort uint16 = collection["MinimumThreshold_Manual"].ToUInt16();
        sensor.MinimumThreshold = uint16 >= (ushort) 0 ? (uint16 <= (ushort) 1000 ? (long) uint16.ToUInt16() : 1000L) : 0L;
      }
      if (double.TryParse(collection["MaximumThreshold_Manual"], out result))
      {
        ushort uint16 = collection["MaximumThreshold_Manual"].ToUInt16();
        sensor.MaximumThreshold = uint16 >= (ushort) 0 ? (uint16 <= (ushort) 1000 ? (long) uint16.ToUInt16() : 1000L) : 0L;
      }
      bool showFullData = !string.IsNullOrEmpty(collection["FullNotiString"]) && (collection["FullNotiString"].ToString() == "1" || collection["FullNotiString"].ToString().ToLower() == "true");
      AirQuality.SetShowFullDataValue(sensor.SensorID, showFullData);
    }
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == 4294967296L /*0x0100000000*/ ? "" : Convert.ToUInt16(sensor.Hysteresis).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == 4294967296L /*0x0100000000*/ ? "" : sensor.MinimumThreshold.ToUInt16().ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == 4294967296L /*0x0100000000*/ ? "" : sensor.MaximumThreshold.ToUInt16().ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection returnData)
  {
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (!(new Version(sensor.FirmwareVersion) > new Version("2.0.0.0")) && sensor.SensorTypeID != 4L)
      return;
    int num1 = collection["observed"].ToInt();
    int num2 = collection["actual"].ToInt();
    if (num1 > 0 && num2 > 0)
    {
      sensor.Calibration1 = (long) num2;
      sensor.Save();
    }
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      numArrayList.Add(AirQualityBase.CalibrateFrame(sensor.SensorID, (double) sensor.Calibration1));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(AirQuality left, AirQuality right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(AirQuality left, AirQuality right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(AirQuality left, AirQuality right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(AirQuality left, AirQuality right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(AirQuality left, AirQuality right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(AirQuality left, AirQuality right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is AirQuality && this.Equals((MonnitApplicationBase) (obj as AirQuality));
  }

  public override bool Equals(MonnitApplicationBase right) => false;

  public override bool NotEqual(MonnitApplicationBase right) => false;

  public override bool LessThan(MonnitApplicationBase right) => false;

  public override bool LessThanEqual(MonnitApplicationBase right) => false;

  public override bool GreaterThan(MonnitApplicationBase right) => false;

  public override bool GreaterThanEqual(MonnitApplicationBase right) => false;

  public static long DefaultMinThreshold => 1000;

  public static long DefaultMaxThreshold => 1000;
}
