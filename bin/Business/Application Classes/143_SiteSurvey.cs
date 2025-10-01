// Decompiled with JetBrains decompiler
// Type: Monnit.SiteSurvey
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class SiteSurvey : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _ShowFullDataValue;
  private SensorAttribute _Label;

  public static long MonnitApplicationID => 143;

  public static string ApplicationName => "Site Survey";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => SiteSurvey.MonnitApplicationID;

  public int TestMode { get; set; }

  public string TestResult { get; set; }

  public int TrueSignal { get; set; }

  public int AverageMargin { get; set; }

  public int TotalTestCount { get; set; }

  public int SuccessfulTestCount { get; set; }

  public long GatewayID { get; set; }

  public int stsStatus { get; set; }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[6]
      {
        new AppDatum(eDatumType.State, "TestMode", this.TestMode),
        new AppDatum(eDatumType.Data, "TestResult", this.TestResult),
        new AppDatum(eDatumType.Percentage, "TrueSignal", this.TrueSignal),
        new AppDatum(eDatumType.Decibel, "AverageMargin", this.AverageMargin),
        new AppDatum(eDatumType.Count, "TotalTestCount", this.TotalTestCount),
        new AppDatum(eDatumType.Count, "SuccessfulTestCount", this.SuccessfulTestCount)
      });
    }
  }

  public static SiteSurvey Create(byte[] sdm, int startIndex)
  {
    return new SiteSurvey()
    {
      stsStatus = (int) sdm[startIndex - 1] >> 4,
      TestMode = (int) sdm[startIndex],
      TestResult = SiteSurveyBase.ResultStringValue((int) sdm[startIndex + 1]),
      TrueSignal = (int) sdm[startIndex + 2],
      AverageMargin = (int) (sbyte) sdm[startIndex + 3],
      TotalTestCount = (int) BitConverter.ToUInt16(sdm, startIndex + 4),
      SuccessfulTestCount = (int) BitConverter.ToUInt16(sdm, startIndex + 6),
      GatewayID = sdm.Length <= startIndex + 8 ? 0L : (long) BitConverter.ToUInt32(sdm, startIndex + 8)
    };
  }

  public override string Serialize()
  {
    return $"{this.TestMode.ToString()}|{this.TestResult.ToString()}|{this.TrueSignal.ToString()}|{this.AverageMargin.ToString()}|{this.TotalTestCount.ToString()}|{this.SuccessfulTestCount.ToString()}|{this.GatewayID.ToString()}|{this.stsStatus.ToString()}";
  }

  public static SiteSurvey Deserialize(string version, string serialized)
  {
    SiteSurvey siteSurvey = new SiteSurvey();
    if (string.IsNullOrEmpty(serialized))
    {
      siteSurvey.stsStatus = 0;
      siteSurvey.TestMode = 0;
      siteSurvey.TestResult = "";
      siteSurvey.TrueSignal = 0;
      siteSurvey.AverageMargin = 0;
      siteSurvey.TotalTestCount = 0;
      siteSurvey.SuccessfulTestCount = 0;
    }
    else
    {
      string[] strArray = serialized.Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      siteSurvey.TestMode = strArray[0].ToInt();
      if (strArray.Length > 1)
      {
        siteSurvey.TestResult = strArray[1].ToString();
        siteSurvey.TrueSignal = strArray[2].ToInt();
        siteSurvey.AverageMargin = strArray[3].ToInt();
        siteSurvey.TotalTestCount = strArray[4].ToInt();
        siteSurvey.SuccessfulTestCount = strArray[5].ToInt();
        siteSurvey.GatewayID = strArray[6].ToLong();
        try
        {
          siteSurvey.stsStatus = strArray[7].ToInt();
        }
        catch
        {
          siteSurvey.stsStatus = 0;
        }
      }
      else
      {
        siteSurvey.TestResult = strArray[0].ToString();
        siteSurvey.TrueSignal = strArray[0].ToInt();
        siteSurvey.AverageMargin = strArray[0].ToInt();
        siteSurvey.TotalTestCount = strArray[0].ToInt();
        siteSurvey.SuccessfulTestCount = strArray[0].ToInt();
        siteSurvey.GatewayID = strArray[0].ToLong();
      }
    }
    return siteSurvey;
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) SiteSurvey.Deserialize(version, serialized);
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

  public SensorAttribute Label
  {
    get
    {
      if (this._Label == null)
        this._Label = new SensorAttribute()
        {
          Value = "Percentage",
          Name = "label"
        };
      return this._Label;
    }
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
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

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[7]
    {
      "TestMode",
      "TestResult",
      "TrueSignal",
      "AverageMargin",
      "TotalTestCount",
      "SuccessfulTestCount",
      "GatewayID"
    });
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      (object) this.TestMode,
      (object) this.TestResult,
      this.PlotValue,
      (object) this.AverageMargin,
      (object) this.TotalTestCount,
      (object) this.SuccessfulTestCount,
      (object) this.GatewayID
    };
  }

  public static string ResultStringValue(int result) => SiteSurveyBase.ResultStringValue(result);

  public static string ModeStringValue(int mode) => SiteSurveyBase.ModeStringValue(mode);

  public override object PlotValue => this.TrueSignal > -1 ? (object) this.TrueSignal : (object) 0;

  public override string NotificationString
  {
    get
    {
      if (this.stsStatus > 0)
        return this.GatewayID > 0L ? "Connected to Gateway, ID: " + this.GatewayID.ToString() : "";
      string notificationString = $"{this.TestResult} {this.TrueSignal.ToString()}%";
      if (this.ShowFullDataValue.Value.ToLower() == "true")
      {
        string[] strArray = new string[5]
        {
          $"{notificationString} ({this.AverageMargin.ToString()}dB),",
          " Packets Sent: ",
          null,
          null,
          null
        };
        int num = this.TotalTestCount;
        strArray[2] = num.ToString();
        strArray[3] = " Received: ";
        num = this.SuccessfulTestCount;
        strArray[4] = num.ToString();
        notificationString = string.Concat(strArray);
      }
      return notificationString;
    }
  }

  public static string GetLabel(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        return sensorAttribute.Value;
    }
    return "%";
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
      CompareType = eCompareType.Less_Than,
      CompareValue = "0",
      NotificationClass = eNotificationClass.Application,
      ApplicationID = SiteSurvey.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    sensor.Recovery = 101;
    if (!string.IsNullOrEmpty(collection["signalTestDuration"]) && !string.IsNullOrEmpty(collection["autoShutoffTime"]))
    {
      int num1 = (int) Convert.ToUInt16(collection["signalTestDuration"]);
      int num2 = (int) Convert.ToUInt16(collection["autoShutoffTime"]);
      if (num1 < 5)
        num1 = 5;
      if (num1 > 240 /*0xF0*/)
        num1 = 240 /*0xF0*/;
      if (num2 < 60)
        num2 = 60;
      if (num2 > 3600)
        num2 = 3600;
      if (num1 >= num2)
        num1 = num2 - 5;
      sensor.Calibration1 = (long) num1;
      sensor.Calibration2 = (long) num2;
    }
    if (!string.IsNullOrEmpty(collection["signalReliabilityLevel"]))
    {
      int num = (int) Convert.ToUInt16(collection["signalReliabilityLevel"]);
      if (num < 0)
        num = 0;
      if (num > 2)
        num = 2;
      switch (num)
      {
        case 0:
          sensor.Calibration4 = 20L;
          break;
        case 2:
          sensor.Calibration4 = 60L;
          break;
        default:
          sensor.Calibration4 = 40L;
          break;
      }
    }
    bool showFullData = collection["FullNotiString"].ToString() == "1" || collection["FullNotiString"].ToString().ToLower() == "true";
    SiteSurvey.SetShowFullDataValue(sensor.SensorID, showFullData);
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

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(SiteSurvey left, SiteSurvey right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(SiteSurvey left, SiteSurvey right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(SiteSurvey left, SiteSurvey right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(SiteSurvey left, SiteSurvey right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(SiteSurvey left, SiteSurvey right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(SiteSurvey left, SiteSurvey right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is SiteSurvey && this.Equals((MonnitApplicationBase) (obj as SiteSurvey));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is SiteSurvey && this.TrueSignal == (right as SiteSurvey).TrueSignal;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is SiteSurvey && this.TrueSignal != (right as SiteSurvey).TrueSignal;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is SiteSurvey && this.TrueSignal < (right as SiteSurvey).TrueSignal;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is SiteSurvey && this.TrueSignal <= (right as SiteSurvey).TrueSignal;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is SiteSurvey && this.TrueSignal > (right as SiteSurvey).TrueSignal;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is SiteSurvey && this.TrueSignal >= (right as SiteSurvey).TrueSignal;
  }

  public static string GetSignalReliabilityLevel(Sensor sensor)
  {
    int num = 1;
    if (sensor.Calibration4 >= 50L)
      num = 2;
    else if (sensor.Calibration4 >= 30L)
      num = 1;
    else if (sensor.Calibration4 < 30L)
      num = 0;
    return num.ToString();
  }

  public static string HystForUI(Sensor sensor) => SiteSurvey.GetHysteresis(sensor).ToString();

  public static string MinThreshForUI(Sensor sensor)
  {
    return SiteSurvey.GetMinimumThreshold(sensor).ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return SiteSurvey.GetMaximumThreshold(sensor).ToString();
  }

  public static long GetMinimumThreshold(Sensor sensor) => sensor.MinimumThreshold;

  public static long GetMaximumThreshold(Sensor sensor) => sensor.MaximumThreshold;

  public static long GetHysteresis(Sensor sensor) => sensor.Hysteresis;

  public static long DefaultMinThreshold => 90;

  public static long DefaultMaxThreshold => 0;

  public static long DefaultHysteresis => 10;
}
