// Decompiled with JetBrains decompiler
// Type: Monnit.Flex
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

public class Flex : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _MaxBend;
  private SensorAttribute _MinBend;

  public static long MonnitApplicationID => 24;

  public static string ApplicationName => nameof (Flex);

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => Flex.MonnitApplicationID;

  public double KOhms { get; set; }

  public override string Serialize() => this.KOhms.ToString();

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.Percentage, "Percent Flexed", Convert.ToDouble(this.PlotValue)),
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
    return (MonnitApplicationBase) Flex.Deserialize(version, serialized);
  }

  public static void SetMaxBend(long sensorID, double maxBend)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "MaxBend")
      {
        sensorAttribute.Value = maxBend.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "MaxBend",
        SensorID = sensorID,
        Value = maxBend.ToString()
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void SetMinBend(long sensorID, double minBend)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "MinBend")
      {
        sensorAttribute.Value = minBend.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "MinBend",
        SensorID = sensorID,
        Value = minBend.ToString()
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  private double MaxBend => this._MaxBend == null ? 35.0 : this._MaxBend.Value.ToDouble();

  private double MinBend => this._MinBend == null ? 1.0 : this._MinBend.Value.ToDouble();

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "MaxBend")
        this._MaxBend = sensorAttribute;
      if (sensorAttribute.Name == "MinBend")
        this._MinBend = sensorAttribute;
    }
    if (this._MaxBend == null)
    {
      this._MaxBend = new SensorAttribute()
      {
        Name = "MaxBend",
        SensorID = sensorID,
        Value = "35"
      };
      this._MaxBend.Save();
      SensorAttribute.ResetAttributeList(sensorID);
    }
    if (this._MinBend != null)
      return;
    this._MinBend = new SensorAttribute()
    {
      Name = "MinBend",
      SensorID = sensorID,
      Value = "1"
    };
    this._MinBend.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public override bool IsValid => (this.state & 16 /*0x10*/) != 16 /*0x10*/;

  public int state { get; set; }

  public override string NotificationString
  {
    get
    {
      if ((this.state & 16 /*0x10*/) == 16 /*0x10*/)
        return "Hardware Failure";
      try
      {
        return this.PlotValue.ToDouble().ToString("#0") + "% Flex";
      }
      catch
      {
        return "Invalid Calibration Levels";
      }
    }
  }

  public override object PlotValue
  {
    get
    {
      if (this.KOhms <= this.MinBend)
        return (object) 0;
      if (this.KOhms >= this.MaxBend)
        return (object) 100;
      try
      {
        return (object) ((this.KOhms - this.MinBend) / (this.MaxBend - this.MinBend) * 100.0).ToInt();
      }
      catch
      {
        return (object) 0;
      }
    }
  }

  public static int ConvertToPercent(Sensor sensor, double kOhms)
  {
    long num1 = sensor.Calibration2 / 1000L;
    long num2 = sensor.Calibration1 / 1000L;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensor.SensorID))
    {
      if (sensorAttribute.Name == "MaxBend")
        num1 = sensorAttribute.Value.ToDouble().ToLong();
      if (sensorAttribute.Name == "MinBend")
        num2 = sensorAttribute.Value.ToDouble().ToLong();
    }
    if (kOhms >= (double) num1)
      return 100;
    if (kOhms <= (double) num2)
      return 0;
    try
    {
      return ((kOhms - (double) num2) / (double) (num1 - num2) * 100.0).ToInt();
    }
    catch
    {
      return 0;
    }
  }

  public static double ConvertFromPercent(Sensor sensor, int input)
  {
    long num1 = sensor.Calibration2 / 1000L;
    long num2 = sensor.Calibration1 / 1000L;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensor.SensorID))
    {
      if (sensorAttribute.Name == "MaxBend")
        num1 = sensorAttribute.Value.ToDouble().ToLong();
      if (sensorAttribute.Name == "MinBend")
        num2 = sensorAttribute.Value.ToDouble().ToLong();
    }
    try
    {
      long num3 = num1 - num2;
      return (double) input / 100.0 * (double) num3 + (double) num2;
    }
    catch
    {
      return 0.0;
    }
  }

  public double KOhm(double flex)
  {
    double num = this.MaxBend - this.MinBend;
    return flex / 100.0 * num + this.MinBend;
  }

  public static Flex Deserialize(string version, string serialized)
  {
    return new Flex() { KOhms = serialized.ToDouble() };
  }

  public static Flex Create(byte[] sdm, int startIndex)
  {
    Flex flex = new Flex();
    flex.state = (int) sdm[startIndex - 1];
    if ((flex.state & 16 /*0x10*/) == 16 /*0x10*/)
    {
      flex.KOhms = 0.0;
      return flex;
    }
    flex.KOhms = BitConverter.ToUInt32(sdm, startIndex).ToDouble() / 1000.0;
    return flex;
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
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = Flex.MonnitApplicationID,
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
      sensor.Hysteresis = (Flex.ConvertFromPercent(sensor, collection["Hysteresis_Manual"].ToInt()) * 1000.0).ToLong();
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
    {
      sensor.MinimumThreshold = (Flex.ConvertFromPercent(sensor, collection["MinimumThreshold_Manual"].ToInt()) * 1000.0).ToLong();
      if (sensor.MinimumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MinimumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      sensor.MaximumThreshold = (Flex.ConvertFromPercent(sensor, collection["MaximumThreshold_Manual"].ToInt()) * 1000.0).ToLong();
      if (sensor.MaximumThreshold < sensor.DefaultValue<long>("DefaultMinimumThreshold"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MaximumThreshold > sensor.DefaultValue<long>("DefaultMaximumThreshold"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if (sensor.MinimumThreshold > sensor.MaximumThreshold)
        sensor.MaximumThreshold = sensor.MinimumThreshold;
    }
    if (string.IsNullOrEmpty(collection["BasicThreshold"]) || string.IsNullOrEmpty(collection["BasicThresholdDirection"]) || !(collection["BasicThresholdDirection"] != "-1"))
      return;
    if (collection["BasicThresholdDirection"].ToInt() == 0)
    {
      sensor.MinimumThreshold = (Flex.ConvertFromPercent(sensor, collection["BasicThreshold"].ToInt()) * 1000.0).ToLong();
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    }
    else if (collection["BasicThresholdDirection"].ToInt() == 1)
    {
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      sensor.MaximumThreshold = (Flex.ConvertFromPercent(sensor, collection["BasicThreshold"].ToInt()) * 1000.0).ToLong();
    }
    sensor.MeasurementsPerTransmission = 6;
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    DataMessage dataMessage = DataMessage.LoadByGuid(collection["messageID"].ToGuid());
    if (collection["limit"].ToInt() == 3)
      Flex.SetMaxBend(sensor.SensorID, dataMessage.Data.ToDouble());
    else
      Flex.SetMinBend(sensor.SensorID, dataMessage.Data.ToDouble());
    SensorAttribute.ResetAttributeList(sensor.SensorID);
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
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : Flex.ConvertToPercent(sensor, (double) sensor.Hysteresis / 1000.0).ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : Flex.ConvertToPercent(sensor, (double) sensor.MinimumThreshold / 1000.0).ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : Flex.ConvertToPercent(sensor, (double) sensor.MaximumThreshold / 1000.0).ToString();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Flex left, Flex right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Flex left, Flex right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Flex left, Flex right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Flex left, Flex right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Flex left, Flex right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Flex left, Flex right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Flex && this.Equals((MonnitApplicationBase) (obj as Flex));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Flex && this.KOhms == (right as Flex).KOhms;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Flex && this.KOhms != (right as Flex).KOhms;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Flex && this.KOhms < (right as Flex).KOhms;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Flex && this.KOhms <= (right as Flex).KOhms;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Flex && this.KOhms > (right as Flex).KOhms;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Flex && this.KOhms >= (right as Flex).KOhms;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 10000000;

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
