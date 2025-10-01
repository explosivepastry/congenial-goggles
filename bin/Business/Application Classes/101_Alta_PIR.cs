// Decompiled with JetBrains decompiler
// Type: Monnit.AltaPIR
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using BaseApplication;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class AltaPIR : MonnitApplicationBase
{
  private SensorAttribute _ChartFormat;

  public static long MonnitApplicationID => 101;

  public static string ApplicationName => "Alta Motion Detection";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Point";

  public override long ApplicationID => AltaPIR.MonnitApplicationID;

  public bool Motion { get; set; }

  public int State { get; set; }

  public override string Serialize()
  {
    return this.Motion.ToString() + ((this.State & 240 /*0xF0*/) > 0 ? $",{this.State.ToString()}" : "");
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[1]
      {
        new AppDatum(eDatumType.MotionDetect, "Motion", this.Motion)
      });
    }
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) AltaPIR.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      if ((this.State & 240 /*0xF0*/) == 0)
        return this.Motion ? "Motion Detected" : "No Motion";
      return (this.State & 16 /*0x10*/) == 16 /*0x10*/ ? "Hardware Error" : "Low Battery";
    }
  }

  public override object PlotValue => (object) (this.Motion ? 1 : 0);

  public static AltaPIR Deserialize(string version, string serialized)
  {
    AltaPIR altaPir = new AltaPIR();
    if (string.IsNullOrEmpty(serialized))
    {
      altaPir.Motion = false;
      altaPir.State = 0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      altaPir.Motion = strArray[0].ToBool();
      if (strArray.Length > 1)
        altaPir.State = strArray[1].ToInt();
    }
    return altaPir;
  }

  public static AltaPIR Create(byte[] sdm, int startIndex)
  {
    AltaPIR altaPir = new AltaPIR();
    try
    {
      altaPir.State = (int) sdm[startIndex - 1];
      altaPir.Motion = sdm[startIndex].ToBool();
      return altaPir;
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
      return (AltaPIR) null;
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
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "True",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = AltaPIR.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrWhiteSpace(collection["EnterAwareOn"]))
    {
      switch (collection["EnterAwareOn"])
      {
        case "Motion":
          sensor.Hysteresis = 1L;
          break;
        case "No Motion":
          sensor.Hysteresis = 0L;
          break;
        case "State Change":
          sensor.Hysteresis = 2L;
          break;
      }
    }
    if (!string.IsNullOrWhiteSpace(collection["Sensitivity"]))
    {
      int num = collection["Sensitivity"].ToInt();
      if (num < 20)
        num = 20;
      if (num > 100)
        num = 100;
      sensor.Calibration2 = (long) num;
    }
    if (!string.IsNullOrWhiteSpace(collection["RearmTime"]))
    {
      int num = collection["RearmTime"].ToInt();
      if (num < 1)
        num = 1;
      if (num > 600)
        num = 600;
      AltaPIRBase.SetRearmTime((ISensor) sensor, num);
    }
    if (string.IsNullOrWhiteSpace(collection["ReportImmediatelyOn"]))
      return;
    AltaPIRBase.SetReportImmediatelyOn((ISensor) sensor, collection["ReportImmediatelyOn"].ToInt());
  }

  public SensorAttribute ChartFormat
  {
    get
    {
      if (this._ChartFormat == null)
        this._ChartFormat = new SensorAttribute()
        {
          Value = "Line",
          Name = nameof (ChartFormat)
        };
      return this._ChartFormat;
    }
  }

  public static string GetChartFormat(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ChartFormat")
        return sensorAttribute.Value.ToString();
    }
    return "Line";
  }

  public static void SetChartFormat(long sensorID, string chartFormat)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ChartFormat")
      {
        sensorAttribute.Value = chartFormat.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "ChartFormat",
        Value = chartFormat.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ChartFormat")
        this._ChartFormat = sensorAttribute;
    }
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(AltaPIR left, AltaPIR right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(AltaPIR left, AltaPIR right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(AltaPIR left, AltaPIR right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(AltaPIR left, AltaPIR right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(AltaPIR left, AltaPIR right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(AltaPIR left, AltaPIR right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is AltaPIR && this.Equals((MonnitApplicationBase) (obj as AltaPIR));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is AltaPIR && this.Motion == (right as AltaPIR).Motion;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is AltaPIR && this.Motion != (right as AltaPIR).Motion;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<' cannot be applied to operands of type 'bool' and 'bool'");
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<=' cannot be applied to operands of type 'bool' and 'bool'");
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>' cannot be applied to operands of type 'bool' and 'bool'");
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>=' cannot be applied to operands of type 'bool' and 'bool'");
  }
}
