// Decompiled with JetBrains decompiler
// Type: Monnit.SootBlower2
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using BaseApplication;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;

#nullable disable
namespace Monnit;

public class SootBlower2 : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;
  private SensorAttribute _ChartFormat;
  private SensorAttribute _QueueID;
  private SensorAttribute _CalibrationValues;

  public SootBlower2() => this._Base = new SootBlower2Base();

  public static long MonnitApplicationID => SootBlower2Base.MonnitApplicationID;

  public static string ApplicationName => "Soot Blower 2";

  public static eApplicationProfileType ProfileType => SootBlower2Base.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => SootBlower2.MonnitApplicationID;

  private SootBlower2Base _Base { get; set; }

  public double Temperature
  {
    get => this._Base.Temperature;
    set => this._Base.Temperature = value;
  }

  public double Pressure
  {
    get => this._Base.Pressure;
    set => this._Base.Pressure = value;
  }

  public double CurrentA
  {
    get => this._Base.CurrentA;
    set => this._Base.CurrentA = value;
  }

  public double CurrentB
  {
    get => this._Base.CurrentB;
    set => this._Base.CurrentB = value;
  }

  public double CurrentC
  {
    get => this._Base.CurrentC;
    set => this._Base.CurrentC = value;
  }

  public bool Mode
  {
    get => this._Base.Mode;
    set => this._Base.Mode = value;
  }

  public bool AbnormalDB
  {
    get => this._Base.AbnormalDB;
    set => this._Base.AbnormalDB = value;
  }

  public double SoundLevel
  {
    get => this._Base.SoundLevel;
    set => this._Base.SoundLevel = value;
  }

  public int Voltage
  {
    get => this._Base.Voltage;
    set => this._Base.Voltage = value;
  }

  public int stsStatus { get; set; }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[9]
      {
        new AppDatum(eDatumType.TemperatureData, "PVTemperature", this.Temperature),
        new AppDatum(eDatumType.Pressure, "PVPressure", this.Pressure),
        new AppDatum(eDatumType.Amps, "CT1", this.CurrentA),
        new AppDatum(eDatumType.Amps, "CT2", this.CurrentB),
        new AppDatum(eDatumType.Amps, "CT3", this.CurrentC),
        new AppDatum(eDatumType.BooleanData, "InService", this.Mode),
        new AppDatum(eDatumType.BooleanData, "AbnormalDB", this.AbnormalDB),
        new AppDatum(eDatumType.Decible, "SoundLevel", this.SoundLevel),
        new AppDatum(eDatumType.Voltage, "Voltage", this.Voltage)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[9]
    {
      this.PlotTemperatureValue,
      (object) this.Pressure,
      (object) this.CurrentA,
      (object) this.CurrentB,
      (object) this.CurrentC,
      (object) this.Mode,
      (object) this.AbnormalDB,
      (object) this.SoundLevel,
      (object) this.Voltage
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[9]
    {
      SootBlower2.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius",
      "PSI",
      "PSI",
      "PSI",
      "Amps",
      "",
      "",
      "Decibles",
      "Volts"
    });
  }

  public override string NotificationString
  {
    get
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      if ((this.stsStatus & 1) == 1)
        stringBuilder1.Append("Thermocouple COMM Error. ");
      if ((this.stsStatus & 2) == 2)
        stringBuilder1.Append("Thermocouple Probe Not Attached or Cable Break. ");
      if ((this.stsStatus & 4) == 4)
        stringBuilder2.Append("Pressure Sensor Error (Transducer Error or Cable Break). ");
      string empty1 = string.Empty;
      string str1;
      double num;
      if (stringBuilder1.Length > 0)
        str1 = stringBuilder1.ToString();
      else if (this.CorF != null && this.CorF.Value == "C")
      {
        num = this.Temperature;
        str1 = num.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
      }
      else
      {
        num = this.Temperature.ToFahrenheit();
        str1 = num.ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture);
      }
      string empty2 = string.Empty;
      string str2;
      if (stringBuilder2.Length > 0)
      {
        str2 = stringBuilder2.ToString();
      }
      else
      {
        num = this.Pressure;
        str2 = num.ToString("#0.#", (IFormatProvider) CultureInfo.InvariantCulture) + " PSI ";
      }
      num = this.CurrentA;
      string str3 = num.ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture);
      num = this.CurrentB;
      string str4 = num.ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture);
      num = this.CurrentC;
      string str5 = num.ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture);
      string str6 = this.Mode ? "Active" : "Inactive";
      string str7 = this.AbnormalDB ? "Abnormal DB " : "";
      num = this.SoundLevel;
      string str8 = num.ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture);
      string str9 = this.Voltage.ToString();
      return $"Temperature: {str1} , Pressure: {str2} , Current A: {str3} Amps , Current B: {str4} Amps , Current C: {str5} Amps , Mode: {str6}, Sound Level: {str7}{str8}, Voltage: {str9}";
    }
  }

  public override object PlotValue => this.PlotTemperatureValue;

  public object PlotTemperatureValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.Temperature : (object) this.Temperature.ToFahrenheit();
    }
  }

  public static bool IsFahrenheit(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF" && sensorAttribute.Value == "C")
        return false;
    }
    return true;
  }

  public static void MakeFahrenheit(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        sensorAttribute.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void MakeCelsius(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
      {
        if (sensorAttribute.Value != "C")
        {
          sensorAttribute.Value = "C";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "CorF",
        Value = "C",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute CorF => this._CorF;

  public SensorAttribute ChartFormat => this._ChartFormat;

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

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
      if (sensorAttribute.Name == "ChartFormat")
        this._ChartFormat = sensorAttribute;
      if (sensorAttribute.Name == "CalibrationValues")
        this._CalibrationValues = sensorAttribute;
      if (sensorAttribute.Name == "QueueID")
        this._QueueID = sensorAttribute;
    }
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (collection["TempScale"] == "on")
    {
      viewData["TempScale"] = "F";
      SootBlower2.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      viewData["TempScale"] = "C";
      SootBlower2.MakeCelsius(sensor.SensorID);
    }
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) SootBlower2.Deserialize(version, serialized);
  }

  public static SootBlower2 Deserialize(string version, string serialized)
  {
    SootBlower2 sootBlower2 = new SootBlower2();
    if (string.IsNullOrEmpty(serialized))
    {
      sootBlower2.stsStatus = 0;
      sootBlower2.Temperature = 0.0;
      sootBlower2.Pressure = 0.0;
      sootBlower2.CurrentA = 0.0;
      sootBlower2.CurrentB = 0.0;
      sootBlower2.CurrentC = 0.0;
      sootBlower2.Mode = false;
      sootBlower2.AbnormalDB = false;
      sootBlower2.SoundLevel = 0.0;
      sootBlower2.Voltage = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length > 1)
      {
        try
        {
          sootBlower2.Temperature = strArray[0].ToDouble();
          sootBlower2.Pressure = strArray[1].ToDouble();
          sootBlower2.CurrentA = strArray[2].ToDouble();
          sootBlower2.CurrentB = strArray[3].ToDouble();
          sootBlower2.CurrentC = strArray[4].ToDouble();
          sootBlower2.Mode = strArray[5].ToBool();
          sootBlower2.AbnormalDB = strArray[6].ToBool();
          sootBlower2.SoundLevel = strArray[7].ToDouble();
          sootBlower2.Voltage = strArray[8].ToInt();
          try
          {
            sootBlower2.stsStatus = strArray[9].ToInt();
          }
          catch
          {
            sootBlower2.stsStatus = 0;
          }
        }
        catch
        {
          sootBlower2.stsStatus = 0;
          sootBlower2.Temperature = 0.0;
          sootBlower2.Pressure = 0.0;
          sootBlower2.CurrentA = 0.0;
          sootBlower2.CurrentB = 0.0;
          sootBlower2.CurrentC = 0.0;
          sootBlower2.Mode = false;
          sootBlower2.AbnormalDB = false;
          sootBlower2.SoundLevel = 0.0;
          sootBlower2.Voltage = 0;
        }
      }
      else
      {
        sootBlower2.Temperature = strArray[0].ToDouble();
        sootBlower2.Pressure = strArray[0].ToDouble();
        sootBlower2.CurrentA = strArray[0].ToDouble();
        sootBlower2.CurrentB = strArray[0].ToDouble();
        sootBlower2.CurrentC = strArray[0].ToDouble();
        sootBlower2.Mode = strArray[0].ToBool();
        sootBlower2.AbnormalDB = strArray[0].ToBool();
        sootBlower2.SoundLevel = strArray[0].ToDouble();
        sootBlower2.Voltage = strArray[0].ToInt();
      }
    }
    return sootBlower2;
  }

  public override string Serialize()
  {
    return $"{this.Temperature.ToString()}|{this.Pressure.ToString()}|{this.CurrentA.ToString()}|{this.CurrentB.ToString()}|{this.CurrentC.ToString()}|{this.Mode.ToString()}|{this.AbnormalDB.ToString()}|{this.SoundLevel.ToString()}|{this.Voltage.ToString()}|{this.stsStatus.ToString()}";
  }

  public static SootBlower2 Create(byte[] sdm, int startIndex)
  {
    byte[] numArray = new byte[14];
    Array.Copy((Array) sdm, startIndex, (Array) numArray, 0, 14);
    return new SootBlower2()
    {
      _Base = SootBlower2Base.Create((int) sdm[startIndex - 1], numArray),
      stsStatus = (int) sdm[startIndex - 1] >> 4
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
    sensor.ReportInterval = 15.0;
    sensor.ActiveStateInterval = 15.0;
    sensor.MinimumCommunicationFrequency = sensor.ReportInterval.ToInt() * 2 + 5;
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      SnoozeDuration = 60,
      CompareValue = "0",
      AccountID = sensor.AccountID,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = SootBlower2.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static int GetActiveReportInterval(Sensor sensor)
  {
    return SootBlower2Base.GetActiveReportInterval((ISensor) sensor);
  }

  public static void SetActiveReportInterval(Sensor sensor, int value)
  {
    SootBlower2Base.SetActiveReportInterval((ISensor) sensor, value);
  }

  public static double GetActiveTemperatureDelta(Sensor sensor)
  {
    return SootBlower2Base.GetActiveTemperatureDelta((ISensor) sensor);
  }

  public static void SetActiveTemperatureDelta(Sensor sensor, double value)
  {
    SootBlower2Base.SetActiveTemperatureDelta((ISensor) sensor, value);
  }

  public static double GetActiveSoundThreshold(Sensor sensor)
  {
    return SootBlower2Base.GetActiveSoundThreshold((ISensor) sensor);
  }

  public static void SetActiveSoundThreshold(Sensor sensor, double value)
  {
    SootBlower2Base.SetActiveSoundThreshold((ISensor) sensor, value);
  }

  public static double GetActivePressureDelta(Sensor sensor)
  {
    return SootBlower2Base.GetActivePressureDelta((ISensor) sensor);
  }

  public static void SetActivePressureDelta(Sensor sensor, double value)
  {
    SootBlower2Base.SetActivePressureDelta((ISensor) sensor, value);
  }

  public static int GetVoltageThreshold(Sensor sensor)
  {
    return SootBlower2Base.GetVoltageThreshold((ISensor) sensor);
  }

  public static void SetVoltageThreshold(Sensor sensor, int value)
  {
    SootBlower2Base.SetVoltageThreshold((ISensor) sensor, value);
  }

  public static double GetActiveCurrentDelta(Sensor sensor)
  {
    return SootBlower2Base.GetActiveCurrentDelta((ISensor) sensor);
  }

  public static void SetActiveCurrentDelta(Sensor sensor, double value)
  {
    SootBlower2Base.SetActiveCurrentDelta((ISensor) sensor, value);
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    sensor.ActiveStateInterval = sensor.ReportInterval;
    if (!string.IsNullOrEmpty(collection["ActiveReportInterval"]))
    {
      int num = collection["ActiveReportInterval"].ToInt();
      SootBlower2.SetActiveReportInterval(sensor, num);
    }
    else if (sensor.GeneralConfigDirty)
      SootBlower2.SetActiveReportInterval(sensor, SootBlower2.GetActiveReportInterval(sensor));
    if (!string.IsNullOrEmpty(collection["ActiveTempDelta"]))
    {
      double num = collection["ActiveTempDelta"].ToDouble();
      if (SootBlower2.IsFahrenheit(sensor.SensorID))
        num /= 1.8;
      SootBlower2.SetActiveTemperatureDelta(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["ActiveCurrentDelta"]))
    {
      double num = collection["ActiveCurrentDelta"].ToDouble();
      SootBlower2Base.SetActiveCurrentDelta((ISensor) sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["ActivePressureDelta"]))
    {
      double num = collection["ActivePressureDelta"].ToDouble();
      SootBlower2.SetActivePressureDelta(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["ActiveSoundThreshold"]))
    {
      double num = collection["ActiveSoundThreshold"].ToDouble();
      SootBlower2.SetActiveSoundThreshold(sensor, num);
    }
    if (string.IsNullOrEmpty(collection["VoltageThreshold"]))
      return;
    int num1 = collection["VoltageThreshold"].ToInt();
    SootBlower2.SetVoltageThreshold(sensor, num1);
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    double result = 0.0;
    if (string.IsNullOrEmpty(collection["actualTemp"]) || string.IsNullOrEmpty(collection["observedTemp"]) || !double.TryParse(collection["actualTemp"], out result) || !double.TryParse(collection["observedTemp"], out result))
      return;
    int subcommand = 3;
    double num1 = collection["actualTemp"].ToDouble();
    double num2 = collection["observedTemp"].ToDouble();
    double actual;
    double observed;
    if (SootBlower2.IsFahrenheit(sensor.SensorID))
    {
      actual = (num1 - 32.0) * (5.0 / 9.0) * 10.0;
      observed = (num2 - 32.0) * (5.0 / 9.0) * 10.0;
    }
    else
    {
      actual = num1 * 10.0;
      observed = num2 * 10.0;
    }
    SootBlower2.SetQueueID(sensor.SensorID);
    SootBlower2.SetCalibrationValues(sensor.SensorID, actual, observed, subcommand, SootBlower2.GetQueueID(sensor.SensorID));
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    NonCachedAttribute.LoadBySensorIDAndName(sensor.SensorID, "Calibration");
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      string[] strArray = SootBlower2.GetCalibrationValues(sensor.SensorID).Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      numArrayList.Add(SootBlower2Base.CalibrateFrame(sensor.SensorID, strArray[0].ToInt(), strArray[1].ToInt(), strArray[2].ToInt(), strArray[3].ToInt()));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Current left, SootBlower2 right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Current left, SootBlower2 right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Current left, SootBlower2 right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Current left, SootBlower2 right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Current left, SootBlower2 right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Current left, SootBlower2 right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is SootBlower2 && this.Equals((MonnitApplicationBase) (obj as SootBlower2));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is SootBlower2 && this.Temperature == (right as SootBlower2).Temperature;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is SootBlower && this.Temperature != (right as SootBlower).Temperature;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is SootBlower2 && this.Temperature < (right as SootBlower2).Temperature;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is SootBlower2 && this.Temperature <= (right as SootBlower2).Temperature;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is SootBlower2 && this.Temperature > (right as SootBlower2).Temperature;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is SootBlower2 && this.Temperature >= (right as SootBlower2).Temperature;
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
}
