// Decompiled with JetBrains decompiler
// Type: Monnit.SoilMoisture
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;

#nullable disable
namespace Monnit;

public class SoilMoisture : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;
  private SensorAttribute _QueueID;
  private SensorAttribute _CalibrationValues;
  private SensorAttribute _Label;

  public static long MonnitApplicationID => 135;

  public static string ApplicationName => "Soil Moisture";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => SoilMoisture.MonnitApplicationID;

  public double Moisture { get; set; }

  public double Temperature { get; set; }

  public double Resistance { get; set; }

  public int stsState { get; set; }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.MoistureTension, "Moisture", this.Moisture),
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temperature)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[2]
    {
      this.PlotValue,
      (object) this.PlotTemperatureValue
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[2]
    {
      this.Label.Value,
      SoilMoisture.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    });
  }

  public static SoilMoisture Create(byte[] sdm, int startIndex)
  {
    return new SoilMoisture()
    {
      stsState = (int) sdm[startIndex - 1] >> 4,
      Moisture = BitConverter.ToUInt16(sdm, startIndex).ToDouble() / 10.0,
      Temperature = BitConverter.ToInt16(sdm, startIndex + 2).ToDouble() / 10.0,
      Resistance = BitConverter.ToUInt32(sdm, startIndex + 4).ToDouble() / 10.0
    };
  }

  public override string Serialize()
  {
    return $"{this.Moisture}|{this.Temperature}|{this.Resistance}|{this.stsState}";
  }

  public static SoilMoisture Deserialize(string version, string serialized)
  {
    SoilMoisture soilMoisture = new SoilMoisture();
    if (string.IsNullOrEmpty(serialized))
    {
      soilMoisture.Moisture = 0.0;
      soilMoisture.Temperature = 0.0;
      soilMoisture.Resistance = 0.0;
      soilMoisture.stsState = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      soilMoisture.Moisture = strArray[0].ToDouble();
      if (strArray.Length > 1)
      {
        soilMoisture.Temperature = strArray[1].ToDouble();
        soilMoisture.Resistance = strArray[2].ToDouble();
        try
        {
          soilMoisture.stsState = strArray[3].ToInt();
        }
        catch
        {
          soilMoisture.stsState = 0;
        }
      }
      else
      {
        soilMoisture.Temperature = strArray[0].ToDouble();
        soilMoisture.Resistance = strArray[0].ToDouble();
        soilMoisture.stsState = 0;
      }
    }
    return soilMoisture;
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) SoilMoisture.Deserialize(version, serialized);
  }

  public override bool IsValid => this.Temperature >= -40.0 && this.Temperature <= 125.0;

  public static bool IsFahrenheit(long sensorID) => Monnit.Temperature.IsFahrenheit(sensorID);

  public static void MakeFahrenheit(long sensorID) => Monnit.Temperature.MakeFahrenheit(sensorID);

  public static void MakeCelsius(long sensorID) => Monnit.Temperature.MakeCelsius(sensorID);

  public SensorAttribute CorF => this._CorF;

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
    double observedTemp,
    int subcommand,
    int queueID,
    double resistance)
  {
    string str = $"{actual.ToString()}|{observed.ToString()}|{observedTemp.ToString()}|{queueID.ToString()}|{subcommand.ToString()}|{resistance.ToString()}";
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

  public SensorAttribute Label
  {
    get
    {
      if (this._Label == null)
        this._Label = new SensorAttribute()
        {
          Value = "centibars"
        };
      return this._Label;
    }
  }

  public static string GetLabel(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        return sensorAttribute.Value;
    }
    return "centibars";
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

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
      if (sensorAttribute.Name == "Label")
        this._Label = sensorAttribute;
      if (sensorAttribute.Name == "CalibrationValues")
        this._CalibrationValues = sensorAttribute;
      if (sensorAttribute.Name == "QueueID")
        this._QueueID = sensorAttribute;
    }
  }

  public override string NotificationString
  {
    get
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string str1 = $"{this.PlotValue.ToDouble().ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture)} {this.Label.Value}";
      if ((this.stsState & 3) != 0)
        str1 = (this.stsState & 2) != 0 ? ((this.stsState & 1) != 0 ? (this.PlotValue.ToDouble() != 0.0 ? str1 + ": Sensor Dry" : str1 + ": Sensor Saturated") : "Moisture Sensor Open(Cable Break or Completely Dry Unburied Sensor)") : "Moisture Sensor Shorted";
      string str2 = this.CorF == null || !(this.CorF.Value == "C") ? this.PlotTemperatureValue.ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture) : this.PlotTemperatureValue.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
      if ((this.stsState & 12) != 0)
        str2 = (this.stsState & 8) != 0 ? ((this.stsState & 4) != 0 ? $"Temperature out of range: {str2} , Temp compensation disabled." : "Temperature Sensor Open (Possible Cable Break), Temp compensation disabled.") : "Temperature Sensor Shorted, Temp compensation disabled.";
      return $"{str1} , {str2}";
    }
  }

  public override object PlotValue
  {
    get
    {
      if (this.Moisture < 0.0)
        return (object) 0;
      return this.Moisture > 240.0 ? (object) 240 /*0xF0*/ : (object) this.Moisture;
    }
  }

  public double PlotTemperatureValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? this.Temperature : this.Temperature.ToFahrenheit();
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
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    SoilMoistureBase.GetDefaults(new Version(sensor.FirmwareVersion), sensor.GenerationType);
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
    Sensor.ClearCache(sensor.SensorID);
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = SoilMoisture.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["MoistureAwareBuffer"]))
    {
      double num = Convert.ToDouble(collection["MoistureAwareBuffer"]);
      if (num < 0.0)
        num = 0.0;
      if (num > 10.0)
        num = 10.0;
      SoilMoisture.SetMoistureAwareBuffer(sensor, num);
    }
    double result;
    if (!string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) && double.TryParse(collection["MinimumThreshold_Manual"], out result))
    {
      double num = Convert.ToDouble(collection["MinimumThreshold_Manual"]);
      if (num < 0.0)
        num = 0.0;
      if (num > 240.0)
        num = 240.0;
      SoilMoisture.SetMoistureThreshMin(sensor, num);
    }
    if (string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result))
      return;
    double num1 = Convert.ToDouble(collection["MaximumThreshold_Manual"]);
    double moistureThreshMin = SoilMoisture.GetMoistureThreshMin(sensor);
    if (num1 < 0.0)
      num1 = 0.0;
    if (num1 > 240.0)
      num1 = 240.0;
    if (moistureThreshMin >= num1)
      num1 = moistureThreshMin + 1.0;
    SoilMoisture.SetMoistureThreshMax(sensor, num1);
  }

  public static double GetMoistureAwareBuffer(Sensor sens)
  {
    return (double) Convert.ToInt32(sens.Hysteresis & (long) ushort.MaxValue) / 10.0;
  }

  public static void SetMoistureAwareBuffer(Sensor sens, double value)
  {
    value *= 10.0;
    int int32 = Convert.ToInt32(sens.Hysteresis & 4294901760L);
    sens.Hysteresis = (long) (int32 | (int) value & (int) ushort.MaxValue);
  }

  public static double GetTemperatureAwareBuffer(Sensor sens)
  {
    return (double) Convert.ToInt32((sens.Hysteresis & 4294901760L) >> 16 /*0x10*/) / 10.0;
  }

  public static void SetTemperatureAwareBuffer(Sensor sens, double value)
  {
    value *= 10.0;
    uint num = Convert.ToUInt32(sens.Hysteresis) & (uint) ushort.MaxValue;
    sens.Hysteresis = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static double GetMoistureThreshMax(Sensor sens)
  {
    return (double) Convert.ToInt32(sens.MaximumThreshold & (long) ushort.MaxValue) / 10.0;
  }

  public static void SetMoistureThreshMax(Sensor sens, double value)
  {
    value *= 10.0;
    int num = (int) (sens.MaximumThreshold & 4294901760L);
    sens.MaximumThreshold = (long) (num | (int) value & (int) ushort.MaxValue);
  }

  public static double GetTemperatureThreshMax(Sensor sens)
  {
    return (double) Convert.ToInt32((sens.MaximumThreshold & 4294901760L) >> 16 /*0x10*/) / 10.0;
  }

  public static void SetTemperatureThreshMax(Sensor sens, double value)
  {
    value *= 10.0;
    uint num = Convert.ToUInt32(sens.MaximumThreshold) & (uint) ushort.MaxValue;
    sens.MaximumThreshold = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static double GetMoistureThreshMin(Sensor sens)
  {
    return (double) Convert.ToInt32(sens.MinimumThreshold & (long) ushort.MaxValue) / 10.0;
  }

  public static void SetMoistureThreshMin(Sensor sens, double value)
  {
    value *= 10.0;
    int num = (int) (sens.MinimumThreshold & 4294901760L);
    sens.MinimumThreshold = (long) (num | (int) value & (int) ushort.MaxValue);
  }

  public static double GetTemperatureThreshMin(Sensor sens)
  {
    return (double) Convert.ToInt32((sens.MinimumThreshold & 4294901760L) >> 16 /*0x10*/) / 10.0;
  }

  public static void SetTemperatureThreshMin(Sensor sens, double value)
  {
    value *= 10.0;
    uint num = Convert.ToUInt32(sens.MinimumThreshold) & (uint) ushort.MaxValue;
    sens.MinimumThreshold = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(SoilMoisture left, SoilMoisture right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(SoilMoisture left, SoilMoisture right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(SoilMoisture left, SoilMoisture right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(SoilMoisture left, SoilMoisture right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(SoilMoisture left, SoilMoisture right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(SoilMoisture left, SoilMoisture right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is SoilMoisture && this.Equals((MonnitApplicationBase) (obj as SoilMoisture));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is SoilMoisture && this.Moisture == (right as SoilMoisture).Moisture;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is SoilMoisture && this.Moisture != (right as SoilMoisture).Moisture;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is SoilMoisture && this.Moisture < (right as SoilMoisture).Moisture;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is SoilMoisture && this.Moisture <= (right as SoilMoisture).Moisture;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is SoilMoisture && this.Moisture > (right as SoilMoisture).Moisture;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is SoilMoisture && this.Moisture >= (right as SoilMoisture).Moisture;
  }

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 240 /*0xF0*/;

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (collection["TempScale"] == "on")
    {
      viewData["TempScale"] = "F";
      SoilMoisture.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      viewData["TempScale"] = "C";
      SoilMoisture.MakeCelsius(sensor.SensorID);
    }
    if (string.IsNullOrEmpty(collection["label"]))
      return;
    SoilMoisture.SetLabel(sensor.SensorID, collection["label"]);
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    int actual = 0;
    int observed = 0;
    int observedTemp = 0;
    int subcommand = 0;
    long resistance = 0;
    if (collection["calType"] == "1")
    {
      subcommand = 3;
      actual = (collection["actual"].ToDouble() * 10.0).ToInt();
      observed = (collection["observed"].ToDouble() * 10.0).ToInt();
      observedTemp = (collection["obsTemp"].ToDouble() * 10.0).ToInt();
      resistance = (collection["resistance"].ToDouble() * 10.0).ToLong();
      if (actual > 2400)
        actual = 2400;
      if (actual < 0)
        actual = 0;
    }
    else if (collection["calType"] == "2")
    {
      subcommand = 4;
      actual = (collection["actualTemp"].ToDouble() * 10.0).ToInt();
      observed = (collection["observedTemp"].ToDouble() * 10.0).ToInt();
      if (SoilMoisture.IsFahrenheit(sensor.SensorID))
      {
        actual = ((collection["actualTemp"].ToDouble() - 32.0) * (5.0 / 9.0) * 10.0).ToInt();
        observed = ((collection["observedTemp"].ToDouble() - 32.0) * (5.0 / 9.0) * 10.0).ToInt();
      }
      if (actual < -400)
        actual = 400;
      if (actual > 1250)
        actual = 1250;
    }
    SoilMoisture.SetQueueID(sensor.SensorID);
    SoilMoisture.SetCalibrationValues(sensor.SensorID, (double) actual, (double) observed, (double) observedTemp, subcommand, SoilMoisture.GetQueueID(sensor.SensorID), (double) resistance);
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      string[] strArray = SoilMoisture.GetCalibrationValues(sensor.SensorID).Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      numArrayList.Add(SoilMoistureBase.CalibrateFrame(sensor.SensorID, strArray[0].ToDouble(), strArray[1].ToDouble(), strArray[2].ToDouble(), strArray[3].ToInt(), strArray[4].ToInt(), strArray[5].ToDouble()));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }
}
