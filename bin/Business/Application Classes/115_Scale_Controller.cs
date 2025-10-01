// Decompiled with JetBrains decompiler
// Type: Monnit.Scale_Controller
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class Scale_Controller : MonnitApplicationBase, ISensorAttribute
{
  public static long MonnitApplicationID => Scale_ControllerBase.MonnitApplicationID;

  public static string ApplicationName => "Scale Controller";

  public static eApplicationProfileType ProfileType => Scale_ControllerBase.ProfileType;

  public override string ChartType => "Line";

  public override long ApplicationID => Scale_Controller.MonnitApplicationID;

  public int Type { get; set; }

  public double Weight { get; set; }

  public int Position { get; set; }

  public int stsStatus { get; set; }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
      ;
  }

  public static Scale_Controller Create(byte[] sdm, int startIndex)
  {
    Scale_Controller scaleController = new Scale_Controller();
    scaleController.stsStatus = (int) sdm[startIndex - 1] >> 4;
    try
    {
      scaleController.Type = (int) sdm[startIndex];
      scaleController.Weight = BitConverter.ToUInt16(sdm, startIndex + 1).ToDouble() / 10.0;
      scaleController.Position = (int) sdm[startIndex + 3];
    }
    catch
    {
      scaleController.Type = 0;
      scaleController.Weight = 0.0;
      scaleController.Position = 0;
    }
    return scaleController;
  }

  public override string Serialize()
  {
    return $"{this.Position.ToString()}|{this.Weight.ToString()}|{this.Type.ToString()}|{this.stsStatus.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.Weight, "Kg", this.Weight),
        new AppDatum(eDatumType.Count, "Position", this.Position)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[1]
    {
      "Kg"
    });
  }

  public static Scale_Controller Deserialize(string version, string serialized)
  {
    Scale_Controller scaleController = new Scale_Controller();
    if (string.IsNullOrEmpty(serialized))
    {
      scaleController.stsStatus = 0;
      scaleController.Position = 0;
      scaleController.Weight = 0.0;
      scaleController.Type = 0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length > 2)
      {
        scaleController.Position = strArray[0].ToInt();
        scaleController.Weight = (double) strArray[1].ToUInt16();
        scaleController.Type = strArray[2].ToInt();
        scaleController.stsStatus = strArray[3].ToInt();
      }
      else
      {
        scaleController.stsStatus = (int) strArray[0].ToUInt16();
        scaleController.Position = strArray[0].ToInt();
        scaleController.Weight = (double) strArray[0].ToUInt16();
        scaleController.Type = strArray[0].ToInt();
      }
    }
    return scaleController;
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Scale_Controller.Deserialize(version, serialized);
  }

  public override object PlotValue => new object();

  public override string NotificationString => this.stsStatus == 1 ? " No Scales Reporting" : "";

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
      ApplicationID = Scale_Controller.MonnitApplicationID,
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
    if (string.IsNullOrEmpty(collection["Display"]))
      return;
    eCOGasDisplay result;
    if (!Enum.TryParse<eCOGasDisplay>(collection["Display"], out result))
      result = eCOGasDisplay.Concentration;
    viewData["TypeScale"] = result.ToString();
  }

  public static double GetHysteresis(Sensor sensor)
  {
    return (double) Convert.ToInt32((sensor.Hysteresis & 4294901760L) >> 16 /*0x10*/) / 10.0;
  }

  public static void SetHysteresis(Sensor sensor, double value)
  {
    value *= 10.0;
    uint num = (uint) (Convert.ToInt32(sensor.Hysteresis) & (int) ushort.MaxValue);
    sensor.Hysteresis = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static double GetMinimumThreshold(Sensor sensor)
  {
    return (double) Convert.ToInt32((sensor.MinimumThreshold & 4294901760L) >> 16 /*0x10*/) / 10.0;
  }

  public static void SetMinimumThreshold(Sensor sensor, double value)
  {
    value *= 10.0;
    uint num = (uint) (Convert.ToInt32(sensor.MinimumThreshold) & (int) ushort.MaxValue);
    sensor.MinimumThreshold = (long) (num | (uint) value << 16 /*0x10*/);
  }

  public static double GetMaximumThreshold(Sensor sensor)
  {
    return (double) Convert.ToInt32((sensor.MaximumThreshold & 4294901760L) >> 16 /*0x10*/) / 10.0;
  }

  public static void SetMaximumThreshold(Sensor sensor, double value)
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

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Scale_Controller left, Scale_Controller right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Scale_Controller left, Scale_Controller right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Scale_Controller left, Scale_Controller right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Scale_Controller left, Scale_Controller right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Scale_Controller left, Scale_Controller right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Scale_Controller left, Scale_Controller right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Scale_Controller && this.Equals((MonnitApplicationBase) (obj as Scale_Controller));
  }

  public override bool Equals(MonnitApplicationBase right) => false;

  public override bool NotEqual(MonnitApplicationBase right) => false;

  public override bool LessThan(MonnitApplicationBase right) => false;

  public override bool LessThanEqual(MonnitApplicationBase right) => false;

  public override bool GreaterThan(MonnitApplicationBase right) => false;

  public override bool GreaterThanEqual(MonnitApplicationBase right) => false;

  public static long DefaultMinThreshold => 0;

  public static long DefaultMaxThreshold => 500;
}
