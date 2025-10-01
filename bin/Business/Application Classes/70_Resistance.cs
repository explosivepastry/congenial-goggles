// Decompiled with JetBrains decompiler
// Type: Monnit.Resistance
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

public class Resistance : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _QueueID;
  private SensorAttribute _CalibrationValues;
  private SensorAttribute _LowValue;
  private SensorAttribute _HighValue;
  private SensorAttribute _DefaultHighValue;
  private SensorAttribute _Label;

  public static long MonnitApplicationID => ResistanceBase.MonnitApplicationID;

  public static string ApplicationName => nameof (Resistance);

  public static eApplicationProfileType ProfileType => ResistanceBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => Resistance.MonnitApplicationID;

  public double Ohms { get; set; }

  public int stsState { get; set; }

  public static Resistance Create(byte[] sdm, int startIndex)
  {
    Resistance resistance = new Resistance();
    try
    {
      resistance.stsState = (int) sdm[startIndex - 1] >> 4;
    }
    catch
    {
      resistance.stsState = 0;
    }
    resistance.Ohms = BitConverter.ToUInt32(sdm, startIndex).ToDouble() / 10.0;
    return resistance;
  }

  public override string Serialize() => $"{this.Ohms.ToString()}|{this.stsState.ToString()}";

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.ResistanceData, "Ohms", this.Ohms)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>() { this.Label.Value };
  }

  public static Resistance Deserialize(string version, string serialized)
  {
    Resistance resistance = new Resistance();
    string[] strArray = serialized.Split('|');
    resistance.Ohms = strArray[0].ToDouble();
    if (strArray.Length > 1)
    {
      try
      {
        resistance.stsState = strArray[1].ToInt();
      }
      catch
      {
        resistance.stsState = 0;
      }
    }
    return resistance;
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Resistance.Deserialize(version, serialized);
  }

  public SensorAttribute QueueID => this._QueueID;

  public static int GetQueueID(long sensorID)
  {
    SensorAttribute.ResetAttributeList(sensorID);
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "QueueID")
        return sensorAttribute.Value.ToInt();
    }
    return 1;
  }

  public static void SetQueueID(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "QueueID")
      {
        sensorAttribute.Value = (sensorAttribute.Value.ToInt() + 1).ToString();
        if (sensorAttribute.Value.ToInt() > 254)
          sensorAttribute.Value = "1";
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "QueueID",
        Value = "1",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute CalibrationValues => this._CalibrationValues;

  public static string GetCalibrationValues(long sensorID)
  {
    SensorAttribute.ResetAttributeList(sensorID);
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CalibrationValues")
        return sensorAttribute.Value;
    }
    return "";
  }

  public static void SetCalibrationValues(
    long sensorID,
    double actual,
    double observed,
    int subcommand,
    int queueID)
  {
    string str = $"{actual.ToString()}|{observed.ToString()}|{queueID.ToString()}|{subcommand.ToString()}";
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CalibrationValues")
      {
        sensorAttribute.Value = str.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "CalibrationValues",
        Value = str.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static double GetLowValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "LowValue")
        return sensorAttribute.Value.ToDouble();
    }
    return 0.0;
  }

  public static void SetLowValue(long sensorID, double lowValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "LowValue")
      {
        sensorAttribute.Value = lowValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "LowValue",
        Value = lowValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static double GetHighValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HighValue")
        return sensorAttribute.Value.ToDouble();
    }
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor == null)
      return 250000.0;
    if (sensor.GenerationType.ToLower().Contains("gen2"))
    {
      Resistance.SetDefaultHighValue(sensorID, 250000.0);
      return 250000.0;
    }
    Resistance.SetDefaultHighValue(sensorID, 145000.0);
    return 145000.0;
  }

  public static void SetHighValue(long sensorID, double highValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HighValue")
      {
        sensorAttribute.Value = highValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "HighValue",
        Value = highValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void SetDefaultHighValue(long sensorID, double defaultHighValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "DefaultHighValue")
      {
        sensorAttribute.Value = defaultHighValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "DefaultHighValue",
        Value = defaultHighValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static double GetDefaultHighValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "DefaultHighValue")
        return sensorAttribute.Value.ToDouble();
    }
    Sensor sensor = Sensor.Load(sensorID);
    if (sensor != null)
    {
      if (sensor.GenerationType.ToLower().Contains("gen2"))
        Resistance.SetDefaultHighValue(sensorID, 250000.0);
      else
        Resistance.SetDefaultHighValue(sensorID, 145000.0);
    }
    return 250000.0;
  }

  public static string GetLabel(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        return sensorAttribute.Value;
    }
    return "Ohms";
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

  public SensorAttribute LowValue
  {
    get
    {
      if (this._LowValue == null)
        this._LowValue = new SensorAttribute()
        {
          Value = "0",
          Name = nameof (LowValue)
        };
      return this._LowValue;
    }
  }

  public SensorAttribute HighValue
  {
    get
    {
      if (this._HighValue == null)
        this._HighValue = new SensorAttribute()
        {
          Value = this.DefaultHighValue.Value.ToString(),
          Name = nameof (HighValue)
        };
      return this._HighValue;
    }
  }

  public SensorAttribute DefaultHighValue
  {
    get
    {
      if (this._DefaultHighValue == null)
        this._DefaultHighValue = new SensorAttribute()
        {
          Value = "250000",
          Name = nameof (DefaultHighValue)
        };
      return this._DefaultHighValue;
    }
  }

  public SensorAttribute Label
  {
    get
    {
      if (this._Label == null)
        this._Label = new SensorAttribute()
        {
          Value = "Ohms",
          Name = nameof (Label)
        };
      return this._Label;
    }
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "LowValue")
        this._LowValue = sensorAttribute;
      if (sensorAttribute.Name == "HighValue")
        this._HighValue = sensorAttribute;
      if (sensorAttribute.Name == "Label")
        this._Label = sensorAttribute;
      if (sensorAttribute.Name == "DefaultHighValue")
        this._DefaultHighValue = sensorAttribute;
      if (sensorAttribute.Name == "CalibrationValues")
        this._CalibrationValues = sensorAttribute;
      if (sensorAttribute.Name == "QueueID")
        this._QueueID = sensorAttribute;
    }
  }

  public override object PlotValue
  {
    get
    {
      return this.Ohms >= 0.0 && this.Ohms < 250000.0 ? (object) this.Ohms.LinearInterpolation(0.0, this.LowValue.Value.ToDouble(), this.DefaultHighValue.Value.ToDouble(), this.HighValue.Value.ToDouble()) : (object) null;
    }
  }

  public override bool IsValid
  {
    get => this.Ohms != 99999999.9 && this.Ohms < 250000.0 && this.Ohms > 0.0;
  }

  public override string NotificationString
  {
    get
    {
      if (this.Ohms == 99999999.9)
        return "Hardware Failure!";
      if (this.Ohms > 250000.0)
        return "Open Circuit/Max Ohms";
      return this.Ohms < 0.0 ? "Invalid Reading" : $"{((double) this.PlotValue).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture)} {this.Label.Value.ToString()}";
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
    sensor.MaximumNetworkHops = sensor.DefaultValue<int>("DefaultMaximumNetworkHops");
    sensor.MinimumCommunicationFrequency = (int) (sensor.ReportInterval * 2.0) + 10;
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      CompareValue = "0",
      NotificationClass = eNotificationClass.Application,
      ApplicationID = Resistance.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void VerifyNotificationValues(Notification notification, string scale)
  {
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
    else
    {
      Resistance resistance = new Resistance();
      resistance.SetSensorAttributes(sensor.SensorID);
      double num1 = resistance.LowValue.Value.ToDouble();
      double num2 = resistance.HighValue.Value.ToDouble();
      double result = 0.0;
      if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
        sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
      if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      if (double.TryParse(collection["Hysteresis_Manual"], out result))
      {
        double num3 = Math.Abs((num2 - num1) / (double) (sensor.DefaultValue<long>("DefaultMaximumThreshold") - sensor.DefaultValue<long>("DefaultMinimumThreshold")));
        double num4 = collection["Hysteresis_Manual"].ToDouble();
        sensor.Hysteresis = num3 == 0.0 ? sensor.DefaultValue<long>("DefaultHysteresis") : (num4 / num3 * 10.0).ToLong() / 10L;
      }
      if (double.TryParse(collection["MinimumThreshold_Manual"], out result))
      {
        double x = collection["MinimumThreshold_Manual"].ToDouble();
        sensor.MinimumThreshold = x >= num1 ? (x <= num2 ? (x.LinearInterpolation(num1, 0.0, num2, (double) sensor.DefaultValue<long>("DefaultMaximumThreshold") / 10.0) * 10.0).ToLong() : (num2.LinearInterpolation(num1, 0.0, num2, (double) sensor.DefaultValue<long>("DefaultMaximumThreshold") / 10.0) * 10.0).ToLong()) : (num1.LinearInterpolation(num1, 0.0, num2, (double) sensor.DefaultValue<long>("DefaultMaximumThreshold") / 10.0) * 10.0).ToLong();
      }
      if (double.TryParse(collection["MaximumThreshold_Manual"], out result))
      {
        double x = collection["MaximumThreshold_Manual"].ToDouble();
        sensor.MaximumThreshold = x >= num1 ? (x <= num2 ? (x.LinearInterpolation(num1, 0.0, num2, (double) sensor.DefaultValue<long>("DefaultMaximumThreshold") / 10.0) * 10.0).ToLong() : (num2.LinearInterpolation(num1, 0.0, num2, (double) sensor.DefaultValue<long>("DefaultMaximumThreshold") / 10.0) * 10.0).ToLong()) : (num1.LinearInterpolation(num1, 0.0, num2, (double) sensor.DefaultValue<long>("DefaultMaximumThreshold") / 10.0) * 10.0).ToLong();
        if (sensor.MinimumThreshold > sensor.MaximumThreshold)
          sensor.MaximumThreshold = sensor.MinimumThreshold;
      }
      Resistance.SetDefaultHighValue(sensor.SensorID, collection["highValue"].ToDouble());
    }
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["lowValue"]))
      Resistance.SetLowValue(sensor.SensorID, collection["lowValue"].ToDouble());
    if (!string.IsNullOrEmpty(collection["highValue"]))
      Resistance.SetHighValue(sensor.SensorID, collection["highValue"].ToDouble());
    if (string.IsNullOrEmpty(collection["label"]))
      return;
    Resistance.SetLabel(sensor.SensorID, collection["label"]);
  }

  public static string HystForUI(Sensor sensor)
  {
    Resistance resistance = new Resistance();
    resistance.SetSensorAttributes(sensor.SensorID);
    double num1 = resistance.LowValue.Value.ToDouble();
    double num2 = Math.Abs((resistance.HighValue.Value.ToDouble() - num1) / (double) (sensor.DefaultValue<long>("DefaultMaximumThreshold") - sensor.DefaultValue<long>("DefaultMinimumThreshold")));
    return (sensor.Hysteresis == (long) uint.MaxValue ? 0.0 : (double) sensor.Hysteresis * num2).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    Resistance resistance = new Resistance();
    resistance.SetSensorAttributes(sensor.SensorID);
    double lowY = resistance.LowValue.Value.ToDouble();
    double highY = resistance.HighValue.Value.ToDouble();
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : (sensor.MinimumThreshold.ToDouble() / 10.0).LinearInterpolation(0.0, lowY, (double) sensor.DefaultValue<long>("DefaultMaximumThreshold") / 10.0, highY).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    Resistance resistance = new Resistance();
    resistance.SetSensorAttributes(sensor.SensorID);
    double lowY = resistance.LowValue.Value.ToDouble();
    double highY = resistance.HighValue.Value.ToDouble();
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : (sensor.MaximumThreshold.ToDouble() / 10.0).LinearInterpolation(0.0, lowY, (double) sensor.DefaultValue<long>("DefaultMaximumThreshold") / 10.0, highY).ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (sensor.GenerationType.ToUpper().Contains("GEN1"))
    {
      collection["observed"].ToDouble().LinearInterpolation(Resistance.GetLowValue(sensor.SensorID), 0.0, Resistance.GetHighValue(sensor.SensorID), 145000.0);
      double o = collection["actual"].ToDouble().LinearInterpolation(Resistance.GetLowValue(sensor.SensorID), 0.0, Resistance.GetHighValue(sensor.SensorID), 145000.0);
      sensor.Calibration1 = o.ToLong();
      sensor.MarkClean(false);
      sensor.PendingActionControlCommand = true;
    }
    else
    {
      double observed = collection["observed"].ToDouble().LinearInterpolation(Resistance.GetLowValue(sensor.SensorID), 0.0, Resistance.GetHighValue(sensor.SensorID), 250000.0) * 10.0;
      double actual = collection["actual"].ToDouble().LinearInterpolation(Resistance.GetLowValue(sensor.SensorID), 0.0, Resistance.GetHighValue(sensor.SensorID), 250000.0) * 10.0;
      Resistance.SetQueueID(sensor.SensorID);
      Resistance.SetCalibrationValues(sensor.SensorID, actual, observed, 3, Resistance.GetQueueID(sensor.SensorID));
      sensor.PendingActionControlCommand = true;
      sensor.Save();
    }
    sensor.Save();
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
    target.EventDetectionType = source.EventDetectionType;
    target.EventDetectionPeriod = source.EventDetectionPeriod;
    target.EventDetectionCount = source.EventDetectionCount;
    target.RearmTime = source.RearmTime;
    target.BiStable = source.BiStable;
    target.TagString = source.TagString;
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      if (sensor.GenerationType.ToUpper().Contains("GEN1"))
      {
        numArrayList.Add(ResistanceBase.CalibrateFrame(sensor.SensorID, (double) sensor.Calibration1));
      }
      else
      {
        string[] strArray = Resistance.GetCalibrationValues(sensor.SensorID).Split(new char[1]
        {
          '|'
        }, StringSplitOptions.RemoveEmptyEntries);
        numArrayList.Add(ResistanceBase.CalibrateFrameGen2(sensor.SensorID, strArray[0].ToInt(), strArray[1].ToInt(), strArray[2].ToInt(), strArray[3].ToInt()));
      }
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Resistance left, Resistance right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Resistance left, Resistance right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Resistance left, Resistance right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Resistance left, Resistance right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Resistance left, Resistance right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Resistance left, Resistance right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Resistance && this.Equals((MonnitApplicationBase) (obj as Resistance));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Resistance && this.Ohms == (right as Resistance).Ohms;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Resistance && this.Ohms != (right as Resistance).Ohms;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Resistance && this.Ohms < (right as Resistance).Ohms;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Resistance && this.Ohms <= (right as Resistance).Ohms;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Resistance && this.Ohms > (right as Resistance).Ohms;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Resistance && this.Ohms >= (right as Resistance).Ohms;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 1450000;

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

  public new static NotificationScaleInfoModel GetScalingInfo(Sensor sens)
  {
    NotificationScaleInfoModel scalingInfo = new NotificationScaleInfoModel();
    Resistance resistance = new Resistance();
    resistance.SetSensorAttributes(sens.SensorID);
    scalingInfo.Label = resistance.Label.Value.ToStringSafe();
    scalingInfo.profileLow = resistance.LowValue.Value.ToDouble();
    scalingInfo.profileHigh = resistance.HighValue.Value.ToDouble();
    scalingInfo.baseLow = 0.0;
    scalingInfo.baseHigh = (double) sens.DefaultValue<long>("DefaultMaximumThreshold");
    return scalingInfo;
  }
}
