// Decompiled with JetBrains decompiler
// Type: Monnit.Gas_CO
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

public class Gas_CO : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;
  private SensorAttribute _Display;

  public static long MonnitApplicationID => 34;

  public static string ApplicationName => "Carbon Monoxicde (CO)";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => Gas_CO.MonnitApplicationID;

  public double Temp { get; set; }

  public int PPM { get; set; }

  public int PPM_TWA { get; set; }

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

  public SensorAttribute CorF
  {
    get
    {
      if (this._CorF == null)
      {
        this._CorF = new SensorAttribute();
        this._CorF.Name = nameof (CorF);
        this._CorF.Value = "F";
      }
      return this._CorF;
    }
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
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
      if (sensorAttribute.Name == "Display")
        this._Display = sensorAttribute;
    }
  }

  public override string Serialize()
  {
    return $"{this.PPM.ToString()}|{this.PPM_TWA.ToString()}|{this.Temp.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[3]
      {
        new AppDatum(eDatumType.PPM, "PPM", this.PPM),
        new AppDatum(eDatumType.PPM, "PPM_TWA", this.PPM_TWA),
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temp)
      });
    }
  }

  public object TemperaturePlotValue
  {
    get
    {
      if (this.Temp <= -100.0 || this.Temp >= 300.0)
        return (object) null;
      return this.CorF != null && this.CorF.Value == "C" ? (object) this.Temp : (object) this.Temp.ToFahrenheit();
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      (object) this.PPM,
      (object) this.PPM_TWA,
      this.TemperaturePlotValue
    };
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>()
    {
      "PPM",
      "PPM_TWA",
      Gas_CO.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Gas_CO.Deserialize(version, serialized);
  }

  public override bool IsValid => this.Temp > -100.0 && this.Temp < 300.0;

  public override string NotificationString
  {
    get
    {
      if (this.Temp > -100.0 && this.Temp < 300.0)
      {
        switch ((eCOGasDisplay) Enum.Parse(typeof (eCOGasDisplay), this.Display.Value))
        {
          case eCOGasDisplay.Time_Weighted_Average:
            return this.PPM_TWA.ToString() + " TWA-PPM";
          case eCOGasDisplay.All:
            return this.CorF != null && this.CorF.Value == "C" ? $"{this.PPM.ToString()} PPM, {this.PPM_TWA.ToString()} TWA-PPM, {this.Temp.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture)}" : $"{this.PPM.ToString()} PPM, {this.PPM_TWA.ToString()} TWA-PPM, {this.Temp.ToFahrenheit().ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture)}";
          default:
            return this.PPM.ToString() + " PPM";
        }
      }
      else
        return this.Temp == -9998.0 ? "Reset Battery" : "Invalid - HW Problem";
    }
  }

  public override object PlotValue
  {
    get
    {
      return this.Display.Value == eCOGasDisplay.Time_Weighted_Average.ToString() ? (object) this.PPM_TWA : (object) this.PPM;
    }
  }

  public static Gas_CO Deserialize(string version, string serialized)
  {
    Gas_CO gasCo = new Gas_CO();
    if (string.IsNullOrEmpty(serialized))
    {
      gasCo.PPM = 0;
      gasCo.Temp = 0.0;
      gasCo.PPM_TWA = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length > 2)
      {
        gasCo.PPM = (int) strArray[0].ToUInt16();
        gasCo.PPM_TWA = (int) strArray[1].ToUInt16();
        gasCo.Temp = strArray[2].ToDouble();
      }
      else
      {
        gasCo.PPM = (int) strArray[0].ToUInt16();
        gasCo.PPM_TWA = (int) strArray[0].ToUInt16();
        gasCo.Temp = strArray[0].ToDouble();
      }
    }
    return gasCo;
  }

  public static Gas_CO Create(byte[] sdm, int startIndex)
  {
    Gas_CO gasCo = new Gas_CO();
    if (((int) sdm[startIndex - 1] & 240 /*0xF0*/) != 80 /*0x50*/)
    {
      gasCo.Temp = BitConverter.ToInt16(sdm, startIndex).ToDouble() / 10.0;
      gasCo.PPM = (int) BitConverter.ToUInt16(sdm, startIndex + 2);
      gasCo.PPM_TWA = (int) BitConverter.ToUInt16(sdm, startIndex + 4);
    }
    else
    {
      gasCo.Temp = -9999.0;
      gasCo.PPM = 0;
      gasCo.PPM_TWA = 0;
    }
    return gasCo;
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
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = Gas_CO.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    bool flag = Gas_CO.IsFahrenheit(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["Display"]))
    {
      eCOGasDisplay result;
      if (!Enum.TryParse<eCOGasDisplay>(collection["Display"], out result))
        result = eCOGasDisplay.Concentration;
      Gas_CO.SetDisplay(sensor.SensorID, result);
    }
    if (!string.IsNullOrEmpty(collection["conMinimumThreshold_Manual"]))
      Gas_CO.ConcentrationMinimumThreshold(sensor, (int) Convert.ToInt16(collection["conMinimumThreshold_Manual"]));
    if (!string.IsNullOrEmpty(collection["conMaximumThreshold_Manual"]))
      Gas_CO.ConcentrationMaximumThreshold(sensor, (int) Convert.ToInt16(collection["conMaximumThreshold_Manual"]));
    if (!string.IsNullOrEmpty(collection["conHysteresis_Manual"]))
      Gas_CO.ConcentrationHysteresis(sensor, (int) Convert.ToInt16(collection["conHysteresis_Manual"]));
    if (!string.IsNullOrEmpty(collection["twaMinimumThreshold_Manual"]))
      Gas_CO.AverageMinimumThreshold(sensor, collection["twaMinimumThreshold_Manual"].ToInt());
    if (!string.IsNullOrEmpty(collection["twaMaximumThreshold_Manual"]))
      Gas_CO.AverageMaximumThreshold(sensor, collection["twaMaximumThreshold_Manual"].ToInt());
    if (!string.IsNullOrEmpty(collection["twaHysteresis_Manual"]))
      Gas_CO.AverageHysteresis(sensor, collection["twaHysteresis_Manual"].ToInt());
    if (!string.IsNullOrEmpty(collection["tempHysteresis_Manual"]))
    {
      double num = collection["tempHysteresis_Manual"].ToDouble();
      if (flag)
        num *= 5.0 / 9.0;
      Gas_CO.TemperatureHysteresis(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["tempMinimumThreshold_Manual"]))
    {
      double celsius = collection["tempMinimumThreshold_Manual"].ToDouble();
      if (flag)
        celsius = celsius.ToCelsius();
      Gas_CO.TemperatureMinimumThreshold(sensor, celsius);
    }
    if (!string.IsNullOrEmpty(collection["tempMaximumThreshold_Manual"]))
    {
      double celsius = collection["tempMaximumThreshold_Manual"].ToDouble();
      if (flag)
        celsius = celsius.ToCelsius();
      Gas_CO.TemperatureMaximumThreshold(sensor, celsius);
    }
    if (!string.IsNullOrEmpty(collection["MeasurementsPerTransmission"]))
      Gas_CO.MeasrementsPerHeartbeat(sensor, collection["MeasurementsPerTransmission"].ToInt());
    if (string.IsNullOrEmpty(collection["TransmitOffset"]))
      return;
    Gas_CO.SycronizationOffSet(sensor, collection["TransmitOffset"].ToInt());
  }

  public static int ConcentrationMinimumThreshold(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? 0 : (int) Convert.ToInt16((sensor.MinimumThreshold & 1048320L) >> 8);
  }

  public static void ConcentrationMinimumThreshold(Sensor sensor, int value)
  {
    if (value < 0)
      value = 0;
    if (value > 50)
      value = 50;
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4293918975U;
    sensor.MinimumThreshold = (long) (num | (uint) (value << 8));
  }

  public static int ConcentrationMaximumThreshold(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? 50 : (int) Convert.ToInt16((sensor.MaximumThreshold & 1048320L) >> 8);
  }

  public static void ConcentrationMaximumThreshold(Sensor sensor, int value)
  {
    if (value < 0)
      value = 0;
    if (value > 50)
      value = 50;
    if (value < Gas_CO.ConcentrationMinimumThreshold(sensor))
      value = Gas_CO.ConcentrationMinimumThreshold(sensor);
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 4293918975U;
    sensor.MaximumThreshold = (long) (num | (uint) (value << 8));
  }

  public static int ConcentrationHysteresis(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? 0 : Convert.ToInt32((sensor.Hysteresis & 1048320L) >> 8);
  }

  public static void ConcentrationHysteresis(Sensor sensor, int value)
  {
    if (value < 0)
      value = 0;
    if (value > 10)
      value = 10;
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.Hysteresis) & 4293918975UL);
    sensor.Hysteresis = (long) (num | (uint) (value << 8));
  }

  public static int AverageMinimumThreshold(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? 0 : (int) Convert.ToInt16(sensor.MinimumThreshold & (long) byte.MaxValue);
  }

  public static void AverageMinimumThreshold(Sensor sensor, int value)
  {
    if (value < 0)
      value = 0;
    if (value > 50)
      value = 50;
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4294967040U;
    sensor.MinimumThreshold = (long) (num | (uint) value);
  }

  public static int AverageMaximumThreshold(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? 50 : (int) Convert.ToInt16(sensor.MaximumThreshold & (long) byte.MaxValue);
  }

  public static void AverageMaximumThreshold(Sensor sensor, int value)
  {
    if (value < 0)
      value = 0;
    if (value > 50)
      value = 50;
    if (value < Gas_CO.AverageMinimumThreshold(sensor))
      value = Gas_CO.AverageMinimumThreshold(sensor);
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 4294967040U;
    sensor.MaximumThreshold = (long) (num | (uint) value);
  }

  public static int AverageHysteresis(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? 0 : Convert.ToInt32(sensor.Hysteresis & (long) byte.MaxValue);
  }

  public static void AverageHysteresis(Sensor sensor, int value)
  {
    if (value < 0)
      value = 0;
    if (value > 10)
      value = 10;
    uint num = (uint) ((ulong) Convert.ToInt32(sensor.Hysteresis) & 4294967040UL);
    sensor.Hysteresis = (long) (num | (uint) value);
  }

  public static double TemperatureMinimumThreshold(Sensor sensor)
  {
    short num = (short) ((sensor.MinimumThreshold & 4293918720L /*0xFFF00000*/) >> 20);
    if (((uint) num & 2048U /*0x0800*/) > 0U)
      num = (short) (61440 /*0xF000*/ | (int) (ushort) num);
    return (double) num / 10.0;
  }

  public static void TemperatureMinimumThreshold(Sensor sensor, double value)
  {
    if (value < -40.0)
      value = -40.0;
    if (value > 100.0)
      value = 100.0;
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 1048575U /*0x0FFFFF*/;
    sensor.MinimumThreshold = (long) (num | (uint) (value * 10.0) << 20);
  }

  public static double TemperatureMaximumThreshold(Sensor sensor)
  {
    short num = (short) ((sensor.MaximumThreshold & 4293918720L /*0xFFF00000*/) >> 20);
    if (((uint) num & 2048U /*0x0800*/) > 0U)
      num = (short) (61440 /*0xF000*/ | (int) (ushort) num);
    return (double) num / 10.0;
  }

  public static void TemperatureMaximumThreshold(Sensor sensor, double value)
  {
    if (value < -40.0)
      value = -40.0;
    if (value > 100.0)
      value = 100.0;
    if (value < Gas_CO.TemperatureMinimumThreshold(sensor))
      value = Gas_CO.TemperatureMinimumThreshold(sensor);
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 1048575U /*0x0FFFFF*/;
    sensor.MaximumThreshold = (long) (num | (uint) (value * 10.0) << 20);
  }

  public static double TemperatureHysteresis(Sensor sensor)
  {
    int num = Convert.ToInt32((sensor.Hysteresis & 4293918720L /*0xFFF00000*/) >> 20);
    if ((num & 2048 /*0x0800*/) != 0)
      num = (int) (short) (61440 /*0xF000*/ | (int) (ushort) num);
    return (double) num / 10.0;
  }

  public static void TemperatureHysteresis(Sensor sensor, double value)
  {
    if (value < 0.0)
      value = 0.0;
    if (value > 10.0)
      value = 10.0;
    uint num = (uint) (Convert.ToInt32(sensor.Hysteresis) & 1048575 /*0x0FFFFF*/);
    sensor.Hysteresis = (long) (num | (uint) (value * 10.0) << 20);
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection returnData)
  {
    if (collection["TempScale"] == "on")
    {
      returnData["TempScale"] = "F";
      Gas_CO.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      returnData["TempScale"] = "C";
      Gas_CO.MakeCelsius(sensor.SensorID);
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    if (collection["CalibrationType"] == "Temperature")
    {
      sensor.Calibration2 = 0L;
      sensor.Calibration4 = (long) (collection["CalibrationValue"].ToDouble() * 10.0).ToInt();
    }
    else if (collection["CalibrationType"] == "0_PPM")
    {
      sensor.Calibration1 = 0L;
      sensor.Calibration3 = 0L;
    }
    else if (collection["CalibrationType"] == "Span_PPM")
    {
      sensor.Calibration1 = 0L;
      sensor.Calibration3 = (long) collection["CalibrationValue"].ToInt();
    }
    sensor.ProfileConfigDirty = false;
    sensor.ProfileConfig2Dirty = false;
    sensor.PendingActionControlCommand = true;
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

  public static int MeasrementsPerHeartbeat(Sensor sensor) => sensor.MeasurementsPerTransmission;

  public static void MeasrementsPerHeartbeat(Sensor sensor, int value)
  {
    if (value < 1)
      value = 1;
    if (value > 250)
      value = 250;
    sensor.MeasurementsPerTransmission = value;
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

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (sensor.Calibration2 == 0L)
      numArrayList.Add(Gas_COBase.CalibrationFrameTemperature(sensor.SensorID, (double) sensor.Calibration4 / 10.0));
    else if (sensor.Calibration1 == 0L && sensor.Calibration3 == 0L)
      numArrayList.Add(Gas_COBase.CalibrationFrameZero(sensor.SensorID));
    else if (sensor.Calibration1 == 0L && sensor.Calibration3 > 0L)
      numArrayList.Add(Gas_COBase.CalibrationFrameSpan(sensor.SensorID, (double) sensor.Calibration3));
    if (numArrayList.Count > 0)
    {
      numArrayList.Add(sensor.ReadProfileConfig(29));
      return numArrayList;
    }
    sensor.PendingActionControlCommand = false;
    sensor.Save();
    return (List<byte[]>) null;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Gas_CO left, Gas_CO right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Gas_CO left, Gas_CO right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Gas_CO left, Gas_CO right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Gas_CO left, Gas_CO right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Gas_CO left, Gas_CO right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Gas_CO left, Gas_CO right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Gas_CO && this.Equals((MonnitApplicationBase) (obj as Gas_CO));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Gas_CO && this.PlotValue.ToInt() == (right as Gas_CO).PlotValue.ToInt();
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Gas_CO && this.PlotValue.ToInt() != (right as Gas_CO).PlotValue.ToInt();
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Gas_CO && this.PlotValue.ToInt() < (right as Gas_CO).PlotValue.ToInt();
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Gas_CO && this.PlotValue.ToInt() <= (right as Gas_CO).PlotValue.ToInt();
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Gas_CO && this.PlotValue.ToInt() > (right as Gas_CO).PlotValue.ToInt();
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Gas_CO && this.PlotValue.ToInt() >= (right as Gas_CO).PlotValue.ToInt();
  }

  public static long TemperatureDefaultMinThreshold => -200;

  public static long TemperatureDefaultMaxThreshold => 1000;

  public static long ConcentrationDefaultMinThreshold => 0;

  public static long ConcentrationDefaultMaxThreshold => 50;

  public static long AverageDefaultMinThreshold => 0;

  public static long AverageDefaultMaxThreshold => 50;
}
