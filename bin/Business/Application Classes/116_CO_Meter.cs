// Decompiled with JetBrains decompiler
// Type: Monnit.CO_Meter
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

public class CO_Meter : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;
  private SensorAttribute _ChartFormat;
  private SensorAttribute _QueueID;
  private SensorAttribute _CalibrationValues;
  private SensorAttribute _Display;

  public static long MonnitApplicationID => CO_MeterBase.MonnitApplicationID;

  public static string ApplicationName => "CO Meter";

  public static eApplicationProfileType ProfileType => CO_MeterBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => CO_Meter.MonnitApplicationID;

  public double Temp { get; set; }

  public double CO_Instantaneous { get; set; }

  public double CO_TWA { get; set; }

  public int RawADC { get; set; }

  public int stsStatus { get; set; }

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

  public static eCOGasDisplay GetDisplay(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Display")
        return (eCOGasDisplay) Enum.Parse(typeof (eCOGasDisplay), sensorAttribute.Value);
    }
    return eCOGasDisplay.Concentration;
  }

  public static void SetDisplay(long sensorID, eCOGasDisplay display)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Display")
      {
        if (sensorAttribute.Value != display.ToString())
        {
          sensorAttribute.Value = display.ToString();
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "Display",
        Value = display.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute Display
  {
    get
    {
      if (this._Display == null)
      {
        this._Display = new SensorAttribute();
        this._Display.Name = nameof (Display);
        this._Display.Value = eCOGasDisplay.Concentration.ToString();
      }
      return this._Display;
    }
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CalibrationValues")
        this._CalibrationValues = sensorAttribute;
      if (sensorAttribute.Name == "QueueID")
        this._QueueID = sensorAttribute;
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
      if (sensorAttribute.Name == "ChartFormat")
        this._ChartFormat = sensorAttribute;
    }
  }

  public static CO_Meter Create(byte[] sdm, int startIndex)
  {
    CO_Meter coMeter = new CO_Meter();
    coMeter.stsStatus = (int) sdm[startIndex - 1] >> 4;
    try
    {
      coMeter.Temp = BitConverter.ToInt16(sdm, startIndex).ToDouble() / 10.0;
      coMeter.CO_Instantaneous = BitConverter.ToUInt16(sdm, startIndex + 2).ToDouble() / 10.0;
      coMeter.CO_TWA = BitConverter.ToUInt16(sdm, startIndex + 4).ToDouble() / 10.0;
      coMeter.RawADC = (int) BitConverter.ToUInt16(sdm, startIndex + 6);
    }
    catch
    {
      coMeter.Temp = -9999.0;
      coMeter.CO_Instantaneous = 0.0;
      coMeter.CO_TWA = 0.0;
      coMeter.RawADC = 0;
    }
    return coMeter;
  }

  public override string Serialize()
  {
    return $"{this.CO_Instantaneous.ToString()}|{this.CO_TWA.ToString()}|{this.Temp.ToString()}|{this.RawADC.ToString()}|{this.stsStatus.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[3]
      {
        new AppDatum(eDatumType.PPM, "PPM", this.CO_Instantaneous),
        new AppDatum(eDatumType.PPM, "TWA", this.CO_TWA),
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temp)
      });
    }
  }

  public double PlotTemperatureValue
  {
    get => this.CorF != null && this.CorF.Value == "C" ? this.Temp : this.Temp.ToFahrenheit();
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      (object) this.CO_Instantaneous,
      (object) this.CO_TWA,
      (object) this.PlotTemperatureValue
    };
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[3]
    {
      "Instantaneous",
      "TWA",
      CO_Meter.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    });
  }

  public static CO_Meter Deserialize(string version, string serialized)
  {
    CO_Meter coMeter = new CO_Meter();
    if (string.IsNullOrEmpty(serialized))
    {
      coMeter.stsStatus = 0;
      coMeter.CO_Instantaneous = 0.0;
      coMeter.CO_TWA = 0.0;
      coMeter.RawADC = 0;
      coMeter.Temp = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length > 2)
      {
        coMeter.CO_Instantaneous = strArray[0].ToDouble();
        coMeter.CO_TWA = strArray[1].ToDouble();
        coMeter.Temp = strArray[2].ToDouble();
        coMeter.RawADC = strArray[3].ToInt();
        try
        {
          coMeter.stsStatus = strArray[4].ToInt();
        }
        catch
        {
          coMeter.stsStatus = 0;
        }
      }
      else
      {
        coMeter.stsStatus = (int) strArray[0].ToUInt16();
        coMeter.CO_Instantaneous = strArray[0].ToDouble();
        coMeter.CO_TWA = strArray[0].ToDouble();
        coMeter.Temp = strArray[0].ToDouble();
        coMeter.RawADC = strArray[0].ToInt();
      }
    }
    return coMeter;
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) CO_Meter.Deserialize(version, serialized);
  }

  public override object PlotValue
  {
    get
    {
      return this.Display.Value == eCOGasDisplay.Time_Weighted_Average.ToString() ? (object) this.CO_TWA : (object) this.CO_Instantaneous;
    }
  }

  public override string NotificationString
  {
    get
    {
      string str;
      switch (this.stsStatus)
      {
        case 1:
        case 2:
          str = " CONCENTRATION WARNING";
          break;
        case 3:
        case 4:
          str = " SHORT TERM EXPOSURE LIMIT";
          break;
        case 5:
          str = " Failure Due To Unstable Readings";
          break;
        case 12:
          return "Gas Overrange Error";
        case 13:
          return "Temperature Range Exceeded";
        case 14:
          return "Hardware Error";
        case 15:
          return "Sensor Initializing";
        default:
          str = "";
          break;
      }
      if (this.Temp > -100.0 && this.Temp < 300.0)
      {
        if (this.CorF != null && this.CorF.Value == "C")
        {
          string[] strArray = new string[6]
          {
            Math.Round(this.CO_Instantaneous, 0).ToString(),
            " PPM, ",
            null,
            null,
            null,
            null
          };
          double num = Math.Round(this.CO_TWA, 0);
          strArray[2] = num.ToString();
          strArray[3] = " TWA-PPM, ";
          num = this.Temp;
          strArray[4] = num.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
          strArray[5] = str;
          return string.Concat(strArray);
        }
        string[] strArray1 = new string[6]
        {
          Math.Round(this.CO_Instantaneous, 0).ToString(),
          " PPM, ",
          null,
          null,
          null,
          null
        };
        double num1 = Math.Round(this.CO_TWA, 0);
        strArray1[2] = num1.ToString();
        strArray1[3] = " TWA-PPM, ";
        num1 = this.Temp.ToFahrenheit();
        strArray1[4] = num1.ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture);
        strArray1[5] = str;
        return string.Concat(strArray1);
      }
      return this.Temp == -9998.0 ? "Reset Battery" : "Invalid - HW Problem";
    }
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection returnData)
  {
    if (collection["TempScale"] == "on")
    {
      returnData["TempScale"] = "F";
      CO_Meter.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      returnData["TempScale"] = "C";
      CO_Meter.MakeCelsius(sensor.SensorID);
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
    try
    {
      if (sensor.Calibration1 == (long) uint.MaxValue)
        sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    }
    catch
    {
      sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    }
    try
    {
      if (sensor.Calibration2 == (long) uint.MaxValue)
        sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    }
    catch
    {
      sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    }
    try
    {
      if (sensor.Calibration3 == (long) uint.MaxValue)
        sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    }
    catch
    {
      sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    }
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
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = CO_Meter.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["Display"]))
    {
      eCOGasDisplay result;
      if (!Enum.TryParse<eCOGasDisplay>(collection["Display"], out result))
        result = eCOGasDisplay.Concentration;
      viewData["TempScale"] = result.ToString();
      CO_Meter.SetDisplay(sensor.SensorID, result);
    }
    if (!string.IsNullOrEmpty(collection["twaMaximumThreshold_Manual"]))
    {
      int num = collection["twaMaximumThreshold_Manual"].ToInt();
      if (num < 0)
        num = 0;
      if (num > 400)
        num = 400;
      CO_Meter.SetTWAMaxThreshold(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["conMaximumThreshold_Manual"]))
    {
      int num = collection["conMaximumThreshold_Manual"].ToInt();
      if (num < 0)
        num = 0;
      if (num > 50)
        num = 50;
      CO_Meter.SetConcentrationMaximumThreshold(sensor, num);
    }
    if (string.IsNullOrEmpty(collection["conHysteresis_Manual"]))
      return;
    int num1 = collection["conHysteresis_Manual"].ToInt();
    if (num1 < 0)
      num1 = 0;
    if (num1 > 10)
      num1 = 10;
    CO_Meter.SetConcentrationHysteresis(sensor, (double) num1);
  }

  public static double GetConcentrationHysteresis(Sensor sensor)
  {
    return (double) Convert.ToInt32(sensor.Hysteresis & (long) ushort.MaxValue) / 10.0;
  }

  public static void SetConcentrationHysteresis(Sensor sensor, double value)
  {
    value *= 10.0;
    int num = (int) ((long) Convert.ToInt32(sensor.Hysteresis) & 4294901760L);
    sensor.Hysteresis = (long) (num | (int) value);
  }

  public static int GetTWAMaxThreshold(Sensor sensor)
  {
    return Convert.ToInt32(sensor.MinimumThreshold & (long) ushort.MaxValue) / 10;
  }

  public static void SetTWAMaxThreshold(Sensor sensor, int value)
  {
    value *= 10;
    uint num = (uint) sensor.MinimumThreshold & 4294901760U;
    sensor.MinimumThreshold = (long) (num | (uint) (ushort) value);
  }

  public static int GetConcentrationMaximumThreshold(Sensor sensor)
  {
    return Convert.ToInt32(sensor.MaximumThreshold & (long) ushort.MaxValue) / 10;
  }

  public static void SetConcentrationMaximumThreshold(Sensor sensor, int value)
  {
    value *= 10;
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 4294901760U;
    sensor.MaximumThreshold = (long) (num | (uint) value);
  }

  public static int GetSampleRate(Sensor sensor)
  {
    return Convert.ToInt32(sensor.Calibration2 & (long) ushort.MaxValue);
  }

  public static void SetSampleRate(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration2) & 4294901760U;
    sensor.Calibration2 = (long) (num | (uint) value);
  }

  public static double GetTemperatureHysteresis(Sensor sensor)
  {
    return (double) Convert.ToInt32((sensor.Hysteresis & 4294901760L) >> 16 /*0x10*/) / 10.0;
  }

  public static void SetTemperatureHysteresis(Sensor sensor, double value)
  {
    value *= 10.0;
    uint num = (uint) (Convert.ToInt32(sensor.Hysteresis) & (int) ushort.MaxValue);
    sensor.Hysteresis = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static double GetTemperatureMinimumThreshold(Sensor sensor)
  {
    return (double) Convert.ToInt32((sensor.MinimumThreshold & 4294901760L) >> 16 /*0x10*/) / 10.0;
  }

  public static void SetTemperatureMinimumThreshold(Sensor sensor, double value)
  {
    value *= 10.0;
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & (uint) ushort.MaxValue;
    sensor.MinimumThreshold = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static double GetTemperatureMaximumThreshold(Sensor sensor)
  {
    return (double) Convert.ToInt32((sensor.MaximumThreshold & 4294901760L) >> 16 /*0x10*/) / 10.0;
  }

  public static void SetTemperatureMaximumThreshold(Sensor sensor, double value)
  {
    value *= 10.0;
    uint num = (uint) (Convert.ToInt32(sensor.MaximumThreshold) & (int) ushort.MaxValue);
    sensor.MaximumThreshold = (long) (num | (uint) value << 16 /*0x10*/);
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

  public static int CalibrateCalValOneUpper(Sensor sensor)
  {
    return Convert.ToInt32((sensor.Calibration1 & 4294901760L) >> 16 /*0x10*/);
  }

  public static void CalibrateCalValOneUpper(Sensor sensor, int value)
  {
    uint num = (uint) (Convert.ToInt32(sensor.Calibration1) & (int) ushort.MaxValue);
    sensor.Calibration1 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int CalibrateCalValOneLower(Sensor sensor)
  {
    return Convert.ToInt32(sensor.Calibration1 & (long) ushort.MaxValue);
  }

  public static void CalibrateCalValOneLower(Sensor sensor, int value)
  {
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.Calibration1) & 4294901760UL);
    sensor.Calibration1 = (long) (num | (uint) value);
  }

  public static int CalibrateCalValTwoUpper(Sensor sensor)
  {
    return Convert.ToInt32((sensor.Calibration2 & 4294901760L) >> 16 /*0x10*/);
  }

  public static void CalibrateCalValTwoUpper(Sensor sensor, int value)
  {
    uint num = (uint) (Convert.ToInt32(sensor.Calibration2) & (int) ushort.MaxValue);
    sensor.Calibration2 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int CalibrateCalValTwoLower(Sensor sensor)
  {
    return Convert.ToInt32(sensor.Calibration2 & (long) ushort.MaxValue);
  }

  public static void CalibrateCalValTwoLower(Sensor sensor, int value)
  {
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.Calibration2) & 4294901760UL);
    sensor.Calibration2 = (long) (num | (uint) value);
  }

  public static int CalibrateCalValThreeTop(Sensor sensor)
  {
    return Convert.ToInt32((sensor.Calibration3 & 4293918720L /*0xFFF00000*/) >> 20);
  }

  public static void CalibrateCalValThreeTop(Sensor sensor, int value)
  {
    uint num = (uint) (Convert.ToInt32(sensor.Calibration3) & 1048575 /*0x0FFFFF*/);
    sensor.Calibration3 = (long) (num | (uint) (value << 20));
  }

  public static int CalibrateCalValThreeMiddle(Sensor sensor)
  {
    return Convert.ToInt32((sensor.Calibration3 & 1048320L) >> 8);
  }

  public static void CalibrateCalValThreeMiddle(Sensor sensor, int value)
  {
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.Calibration3) & 4293918975UL);
    sensor.Calibration3 = (long) (num | (uint) (value << 8));
  }

  public static int CalibrateCalValThreeBottom(Sensor sensor)
  {
    return Convert.ToInt32(sensor.Calibration3 & (long) byte.MaxValue);
  }

  public static void CalibrateCalValThreeBottom(Sensor sensor, int value)
  {
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.Calibration3) & 4294967040UL);
    sensor.Calibration3 = (long) (num | (uint) value);
  }

  public static int CalibrateCalValFourTop(Sensor sensor)
  {
    return Convert.ToInt32((sensor.Calibration4 & 4293918720L /*0xFFF00000*/) >> 20);
  }

  public static void CalibrateCalValFourTop(Sensor sensor, int value)
  {
    uint num = (uint) (Convert.ToInt32(sensor.Calibration4) & 1048575 /*0x0FFFFF*/);
    sensor.Calibration4 = (long) (num | (uint) (value << 20));
  }

  public static int CalibrateCalValFourMiddle(Sensor sensor)
  {
    return Convert.ToInt32((sensor.Calibration4 & 1048320L) >> 8);
  }

  public static void CalibrateCalValFourMiddle(Sensor sensor, int value)
  {
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.Calibration4) & 4293918975UL);
    sensor.Calibration3 = (long) (num | (uint) (value << 8));
  }

  public static int CalibrateCalValFourBottom(Sensor sensor)
  {
    return Convert.ToInt32(sensor.Calibration4 & (long) byte.MaxValue);
  }

  public static void CalibrateCalValFourBottom(Sensor sensor, int value)
  {
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.Calibration4) & 4294967040UL);
    sensor.Calibration4 = (long) (num | (uint) value);
  }

  public static int GetSamplingFrequency(Sensor sensor)
  {
    return Convert.ToInt32(sensor.Calibration2 & (long) ushort.MaxValue);
  }

  public static void SetSamplingFrequency(Sensor sensor, int value)
  {
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.Calibration2) & 4294901760UL);
    sensor.Calibration2 = (long) (num | (uint) value);
  }

  public static int SycronizationOffSet(Sensor sensor) => sensor.TransmitOffset;

  public static void SycronizationOffSet(Sensor sensor, int value)
  {
    if (value < 0)
      value = 0;
    if (value > 30)
      value = 30;
    sensor.TransmitOffset = value;
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    int actual = 0;
    int observed = 0;
    int subcommand = 0;
    if (collection["calType"] == "1")
    {
      subcommand = 5;
      actual = collection["actual"].ToInt();
      observed = collection["observed"].ToInt();
      if (actual < 1 || actual > 1000)
        throw new Exception("Calibration value out of range!");
    }
    else if (collection["calType"] == "2")
    {
      subcommand = 3;
      double num = collection["actualTemp"].ToDouble();
      observed = collection["observedTemp"].ToInt();
      sensor.Calibration1 = num >= -40.0 && num <= 125.0 ? (num * 10.0).ToLong() : throw new Exception("Calibration value out of range!");
      actual = (num * 10.0).ToInt();
      sensor.ProfileConfigDirty = false;
      sensor.ProfileConfig2Dirty = false;
      sensor.PendingActionControlCommand = true;
    }
    else if (collection["calType"] == "3")
    {
      subcommand = 4;
      actual = 0;
      observed = 0;
    }
    CO_Meter.SetQueueID(sensor.SensorID);
    CO_Meter.SetCalibrationValues(sensor.SensorID, (double) actual, (double) observed, subcommand, CO_Meter.GetQueueID(sensor.SensorID));
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      string[] strArray = CO_Meter.GetCalibrationValues(sensor.SensorID).Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      switch (strArray[3].ToInt())
      {
        case 3:
          numArrayList.Add(TemperatureBase.CalibrateFrame(sensor.SensorID, (double) sensor.Calibration1 / 10.0));
          break;
        case 4:
          numArrayList.Add(CO_MeterBase.CalibrateFrame(sensor.SensorID, strArray[0].ToInt(), strArray[1].ToInt(), strArray[2].ToInt(), strArray[3].ToInt()));
          break;
        case 5:
          numArrayList.Add(CO_MeterBase.CalibrateFrame(sensor.SensorID, strArray[0].ToInt(), strArray[1].ToInt(), strArray[2].ToInt(), strArray[3].ToInt()));
          break;
      }
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(CO_Meter left, CO_Meter right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(CO_Meter left, CO_Meter right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(CO_Meter left, CO_Meter right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(CO_Meter left, CO_Meter right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(CO_Meter left, CO_Meter right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(CO_Meter left, CO_Meter right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is CO_Meter && this.Equals((MonnitApplicationBase) (obj as CO_Meter));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is CO_Meter && this.PlotValue.ToInt() == (right as CO_Meter).PlotValue.ToInt();
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is CO_Meter && this.PlotValue.ToInt() != (right as CO_Meter).PlotValue.ToInt();
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is CO_Meter && this.PlotValue.ToInt() < (right as CO_Meter).PlotValue.ToInt();
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is CO_Meter && this.PlotValue.ToInt() <= (right as CO_Meter).PlotValue.ToInt();
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is CO_Meter && this.PlotValue.ToInt() > (right as CO_Meter).PlotValue.ToInt();
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is CO_Meter && this.PlotValue.ToInt() >= (right as CO_Meter).PlotValue.ToInt();
  }

  public static long TemperatureDefaultMinThreshold => -400;

  public static long TemperatureDefaultMaxThreshold => 1250;

  public static long ConcentrationDefaultMinThreshold => 0;

  public static long ConcentrationDefaultMaxThreshold => 1000;

  public static long AverageDefaultMinThreshold => 0;

  public static long AverageDefaultMaxThreshold => 50;
}
