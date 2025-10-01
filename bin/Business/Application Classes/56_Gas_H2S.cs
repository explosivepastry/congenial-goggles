// Decompiled with JetBrains decompiler
// Type: Monnit.Gas_H2S
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

public class Gas_H2S : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _QueueID;
  private SensorAttribute _CalibrationValues;
  private SensorAttribute _CorF;

  public static long MonnitApplicationID => 56;

  public static string ApplicationName => "H2S Gas";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => Gas_H2S.MonnitApplicationID;

  public double Temp { get; set; }

  public double Instantaneous { get; set; }

  public double TWA { get; set; }

  public int RawADC { get; set; }

  public int stsState { get; set; }

  public override string Serialize()
  {
    return $"{this.Instantaneous.ToString()}|{this.TWA.ToString()}|{this.Temp.ToString()}|{this.RawADC.ToString()}|{this.stsState.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[3]
      {
        new AppDatum(eDatumType.PPM, "PPM", this.Instantaneous),
        new AppDatum(eDatumType.PPM, "PPM_TWA", this.TWA),
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temp)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      this.PlotValue,
      (object) this.TWA,
      (object) (Gas_H2S.IsFahrenheit(sensorID) ? this.Temp.ToFahrenheit() : this.Temp)
    };
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>()
    {
      "Instantaneous (ppm)",
      "TWA (ppm)",
      Gas_H2S.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Gas_H2S.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      string str1;
      double num;
      if (this.CorF != null && this.CorF.Value == "C")
      {
        str1 = this.Temp.ToString("#0.#° C", (IFormatProvider) CultureInfo.InvariantCulture);
      }
      else
      {
        num = this.Temp.ToFahrenheit();
        str1 = num.ToString("#0.#° F", (IFormatProvider) CultureInfo.InvariantCulture);
      }
      if (this.stsState > 0)
      {
        switch (this.stsState)
        {
          case 1:
            num = Math.Round(this.Instantaneous, 1);
            string str2 = num.ToString("#0.#");
            num = Math.Round(this.TWA, 1);
            string str3 = num.ToString("#0.#");
            string str4 = str1;
            return $"Concentration Warning! H2S Instantaneous: {str2} ppm, H2S TWA: {str3} ppm, Temperature: {str4}";
          case 5:
            num = Math.Round(this.Instantaneous, 1);
            string str5 = num.ToString("#0.#");
            num = Math.Round(this.TWA, 1);
            string str6 = num.ToString("#0.#");
            string str7 = str1;
            return $"Calibration Failed - Readings Not Stable Enough. H2S Instantaneous: {str5} ppm, H2S TWA: {str6} ppm, Temperature: {str7}";
          case 6:
            num = Math.Round(this.Instantaneous, 1);
            string str8 = num.ToString("#0.#");
            num = Math.Round(this.TWA, 1);
            string str9 = num.ToString("#0.#");
            string str10 = str1;
            return $"Calibration QueueID Repeat. H2S Instantaneous: {str8} ppm, H2S TWA: {str9} ppm, Temperature: {str10}";
          case 7:
            num = Math.Round(this.Instantaneous, 1);
            string str11 = num.ToString("#0.#");
            num = Math.Round(this.TWA, 1);
            string str12 = num.ToString("#0.#");
            string str13 = str1;
            return $"Calibration Failed. H2S Instantaneous: {str11} ppm, H2S TWA: {str12} ppm, Temperature: {str13}";
          case 8:
            num = Math.Round(this.Instantaneous, 1);
            string str14 = num.ToString("#0.#");
            num = Math.Round(this.TWA, 1);
            string str15 = num.ToString("#0.#");
            string str16 = str1;
            return $"Calibration Passed. H2S Instantaneous: {str14} ppm, H2S TWA: {str15} ppm, Temperature: {str16}";
          case 9:
            num = Math.Round(this.Instantaneous, 1);
            string str17 = num.ToString("#0.#");
            num = Math.Round(this.TWA, 1);
            string str18 = num.ToString("#0.#");
            string str19 = str1;
            return $"Calibration Failed - Span Limit Exceeded. H2S Instantaneous: {str17} ppm, H2S TWA: {str18} ppm, Temperature: {str19}";
          case 12:
            return "Gas Overrange Error!";
          case 13:
            return "Temperature Error!";
          case 14:
            return "Hardware Error!";
          case 15:
            return "Sensor Initializing.";
        }
      }
      num = Math.Round(this.Instantaneous, 1);
      string str20 = num.ToString("#0.#");
      num = Math.Round(this.TWA, 1);
      string str21 = num.ToString("#0.#");
      string str22 = str1;
      return $"H2S Instantaneous: {str20} ppm, H2S TWA: {str21} ppm, Temperature: {str22}";
    }
  }

  public override object PlotValue => (object) this.Instantaneous;

  public static Gas_H2S Deserialize(string version, string serialized)
  {
    Gas_H2S gasH2S = new Gas_H2S();
    if (string.IsNullOrEmpty(serialized))
    {
      gasH2S.stsState = 0;
      gasH2S.Instantaneous = 0.0;
      gasH2S.TWA = 0.0;
      gasH2S.RawADC = 0;
      gasH2S.Temp = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length > 1)
      {
        try
        {
          gasH2S.Instantaneous = strArray[0].ToDouble();
          gasH2S.TWA = strArray[1].ToDouble();
          gasH2S.Temp = strArray[2].ToDouble();
          gasH2S.RawADC = strArray[3].ToInt();
          try
          {
            gasH2S.stsState = strArray[4].ToInt();
          }
          catch
          {
            gasH2S.stsState = 0;
          }
        }
        catch
        {
          gasH2S.stsState = 0;
          gasH2S.Instantaneous = 0.0;
          gasH2S.TWA = 0.0;
          gasH2S.Temp = 0.0;
          gasH2S.RawADC = 0;
        }
      }
      else
      {
        gasH2S.Instantaneous = strArray[0].ToDouble();
        gasH2S.TWA = strArray[0].ToDouble();
        gasH2S.Temp = strArray[0].ToDouble();
        gasH2S.RawADC = strArray[0].ToInt();
      }
    }
    return gasH2S;
  }

  public static Gas_H2S Create(byte[] sdm, int startIndex)
  {
    Gas_H2S gasH2S = new Gas_H2S();
    gasH2S.stsState = (int) sdm[startIndex - 1] >> 4;
    try
    {
      gasH2S.Temp = BitConverter.ToInt16(sdm, startIndex).ToDouble() / 10.0;
      gasH2S.Instantaneous = BitConverter.ToUInt16(sdm, startIndex + 2).ToDouble() / 1000.0;
      gasH2S.TWA = BitConverter.ToUInt16(sdm, startIndex + 4).ToDouble() / 1000.0;
      gasH2S.RawADC = (int) BitConverter.ToUInt16(sdm, startIndex + 6);
    }
    catch
    {
      gasH2S.Temp = -9999.0;
      gasH2S.Instantaneous = 0.0;
      gasH2S.TWA = 0.0;
      gasH2S.RawADC = 0;
    }
    return gasH2S;
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

  public SensorAttribute CorF => this._CorF;

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
      ApplicationID = Gas_H2S.MonnitApplicationID,
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
    double result;
    if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    if (!string.IsNullOrEmpty(collection["Hysteresis_Manual"]))
    {
      double num = collection["Hysteresis_Manual"].ToDouble();
      if (num < 0.0)
        num = 0.0;
      if (num > 10.0)
        num = 10.0;
      Gas_H2S.SetGasHysteresis(sensor, num);
    }
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      double num = collection["MaximumThreshold_Manual"].ToDouble();
      if (num < 0.0)
        num = 0.0;
      if (num > 50.0)
        num = 50.0;
      Gas_H2S.SetGasMaxThresh(sensor, num);
    }
    if (string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]))
      return;
    double num1 = collection["MinimumThreshold_Manual"].ToDouble();
    if (num1 < 0.0)
      num1 = 0.0;
    if (num1 > 50.0)
      num1 = 50.0;
    Gas_H2S.SetGasMinThresh(sensor, num1);
  }

  public static void SetGasHysteresis(Sensor sensor, double value)
  {
    value *= 1000.0;
    uint uint32 = Convert.ToUInt32(sensor.Hysteresis & 4294901760L);
    sensor.Hysteresis = (long) (uint32 | (uint) value);
  }

  public static double GetGasHysteresis(Sensor sensor)
  {
    return Convert.ToDouble(sensor.Hysteresis & (long) ushort.MaxValue) / 1000.0;
  }

  public static void SetGasMinThresh(Sensor sensor, double value)
  {
    value *= 1000.0;
    uint uint32 = Convert.ToUInt32(sensor.MinimumThreshold & 4294901760L);
    sensor.MinimumThreshold = (long) (uint32 | (uint) value);
  }

  public static double GetGasMinThresh(Sensor sensor)
  {
    return Convert.ToDouble(sensor.MinimumThreshold & (long) ushort.MaxValue) / 1000.0;
  }

  public static void SetGasMaxThresh(Sensor sensor, double value)
  {
    value *= 1000.0;
    uint uint32 = Convert.ToUInt32(sensor.MaximumThreshold & 4294901760L);
    sensor.MaximumThreshold = (long) (uint32 | (uint) value);
  }

  public static double GetGasMaxThresh(Sensor sensor)
  {
    return Convert.ToDouble(sensor.MaximumThreshold & (long) ushort.MaxValue) / 1000.0;
  }

  public static int GetTempHysteresis(Sensor sensor)
  {
    return Convert.ToInt32((sensor.Hysteresis & 4294901760L) >> 16 /*0x10*/) / 10;
  }

  public static void SetTempHysteresis(Sensor sensor, int value)
  {
    if (value < 0)
      value = 0;
    if (value > 5)
      value = 5;
    value *= 10;
    uint num = Convert.ToUInt32(sensor.Hysteresis) & (uint) ushort.MaxValue;
    sensor.Hysteresis = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static double GetTempMinThresh(Sensor sensor)
  {
    return (double) Convert.ToInt32((sensor.MinimumThreshold & 4294901760L) >> 16 /*0x10*/) / 10.0;
  }

  public static void SetTempMinThresh(Sensor sensor, double value)
  {
    if (value < -40.0)
      value = -40.0;
    if (value > 125.0)
      value = 125.0;
    value *= 10.0;
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & (uint) ushort.MaxValue;
    sensor.MinimumThreshold = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static double GetTempMaxThresh(Sensor sensor)
  {
    return (double) Convert.ToInt32((sensor.MaximumThreshold & 4294901760L) >> 16 /*0x10*/) / 10.0;
  }

  public static void SetTempMaxThresh(Sensor sensor, double value)
  {
    if (value < -40.0)
      value = -40.0;
    if (value > 125.0)
      value = 125.0;
    double tempMinThresh = Gas_H2S.GetTempMinThresh(sensor);
    if (tempMinThresh > value)
      value = tempMinThresh;
    value *= 10.0;
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & (uint) ushort.MaxValue;
    sensor.MaximumThreshold = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static void SetSampleRate(Sensor sensor, int value)
  {
    if (value < 0)
      value = 0;
    if (value > 30)
      value = 30;
    uint uint32 = Convert.ToUInt32(sensor.Calibration2 & 4294901760L);
    sensor.Calibration2 = (long) (uint32 | (uint) value);
  }

  public static int GetSampleRate(Sensor sensor)
  {
    return Convert.ToInt32(sensor.Calibration2 & (long) ushort.MaxValue);
  }

  public static int GetZeroADC(Sensor sensor)
  {
    return Convert.ToInt32((sensor.Calibration2 & 4294901760L) >> 16 /*0x10*/);
  }

  public static void SetZeroADC(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration2) & (uint) ushort.MaxValue;
    sensor.Calibration2 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static double GetSpan(Sensor sensor) => (double) sensor.Calibration3 / 10.0;

  public static void SetSpan(Sensor sensor, double value)
  {
    sensor.Calibration3 = (value * 10.0).ToLong();
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    int actual = 0;
    int observed = 0;
    int subcommand = 0;
    if (collection["calType"] == "1")
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
    else if (collection["calType"] == "2")
    {
      subcommand = 5;
      actual = collection["altitude"].ToDouble() >= 0.1 && collection["altitude"].ToDouble() <= 50.0 ? (collection["altitude"].ToDouble() * 10.0).ToInt() : throw new Exception("Calibration value out of range!");
      observed = (collection["observedAltitude"].ToDouble() * 10.0).ToInt();
    }
    else if (collection["calType"] == "3")
    {
      try
      {
        long customerID = collection["userID"].ToLong();
        string dateFormat = collection["datePrefFormat"];
        CalibrationCertificate DBObject1 = CalibrationCertificate.LoadBySensor(sensor);
        if (DBObject1 != null)
        {
          CertificateAcknowledgement.DeleteAcknowledgementByCertificateID(DBObject1.CalibrationCertificateID);
          DBObject1.LogAuditData(eAuditAction.Related_Modify, eAuditObject.Sensor, customerID, sensor.AccountID, "Deleted Calibration Certificate");
          DBObject1.DeletedByUserID = customerID;
          DBObject1.DeletedDate = DateTime.UtcNow;
          DBObject1.Save();
        }
        CalibrationCertificate DBObject2 = new CalibrationCertificate();
        DBObject2.CreatedByUserID = customerID;
        DBObject2.SensorID = sensor.SensorID;
        DBObject2.DateCreated = DateTime.UtcNow;
        DBObject2.DateCertified = DateTime.UtcNow.Date;
        CalibrationCertificate calibrationCertificate = DBObject2;
        DateTime dateTime1 = DateTime.UtcNow;
        dateTime1 = dateTime1.Date;
        DateTime dateTime2 = dateTime1.AddDays(90.0);
        calibrationCertificate.CertificationValidUntil = dateTime2;
        DBObject2.CalibrationFacilityID = 4L;
        DBObject2.CalibrationNumber = "H2S Zeroed " + DateTime.UtcNow.Date.ToDateFormatted(dateFormat);
        DBObject2.CertificationType = "InternalSensorCert|H2S_Zero";
        DBObject2.Save();
        DBObject2.LogAuditData(eAuditAction.Related_Modify, eAuditObject.Sensor, customerID, sensor.AccountID, "Created Calibration Certificate");
        sensor.LogAuditData(eAuditAction.Update, eAuditObject.Sensor, customerID, sensor.AccountID, "Edited Sensor Calibration Certificate Info");
        sensor.CalibrationCertification = DBObject2.CalibrationNumber;
        sensor.CalibrationFacilityID = DBObject2.CalibrationFacilityID;
      }
      catch
      {
      }
      subcommand = 4;
      actual = 0;
      observed = 0;
    }
    Gas_H2S.SetQueueID(sensor.SensorID);
    Gas_H2S.SetCalibrationValues(sensor.SensorID, (double) actual, (double) observed, subcommand, Gas_H2S.GetQueueID(sensor.SensorID));
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      string[] strArray = Gas_H2S.GetCalibrationValues(sensor.SensorID).Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      switch (strArray[3].ToInt())
      {
        case 3:
          numArrayList.Add(TemperatureBase.CalibrateFrame(sensor.SensorID, (double) sensor.Calibration1 / 10.0));
          break;
        case 4:
          numArrayList.Add(Gas_H2SBase.CalibrateFrame(sensor.SensorID, strArray[0].ToDouble(), strArray[1].ToDouble(), strArray[2].ToInt(), strArray[3].ToInt()));
          break;
        case 5:
          numArrayList.Add(Gas_H2SBase.CalibrateFrame(sensor.SensorID, strArray[0].ToDouble(), strArray[1].ToDouble(), strArray[2].ToInt(), strArray[3].ToInt()));
          break;
      }
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection returnData)
  {
    if (collection["TempScale"] == "on")
    {
      returnData["TempScale"] = "F";
      Gas_H2S.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      returnData["TempScale"] = "C";
      Gas_H2S.MakeCelsius(sensor.SensorID);
    }
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Gas_H2S left, Gas_H2S right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Gas_H2S left, Gas_H2S right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Gas_H2S left, Gas_H2S right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Gas_H2S left, Gas_H2S right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Gas_H2S left, Gas_H2S right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Gas_H2S left, Gas_H2S right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Gas_H2S && this.Equals((MonnitApplicationBase) (obj as Gas_H2S));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Gas_H2S && this.Instantaneous == (right as Gas_H2S).Instantaneous;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Gas_H2S && this.Instantaneous != (right as Gas_H2S).Instantaneous;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Gas_H2S && this.Instantaneous < (right as Gas_H2S).Instantaneous;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Gas_H2S && this.Instantaneous <= (right as Gas_H2S).Instantaneous;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Gas_H2S && this.Instantaneous > (right as Gas_H2S).Instantaneous;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Gas_H2S && this.Instantaneous >= (right as Gas_H2S).Instantaneous;
  }
}
