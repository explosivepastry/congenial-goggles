// Decompiled with JetBrains decompiler
// Type: Monnit.EngineerHelper
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

#nullable disable
namespace Monnit;

public class EngineerHelper : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CalibrationValues;

  public static long MonnitApplicationID => EngineerHelperBase.MonnitApplicationID;

  public static string ApplicationName => "Engineer Helper";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "None";

  public override long ApplicationID => EngineerHelper.MonnitApplicationID;

  public string Data { get; set; }

  public string STSValue { get; set; }

  public static EngineerHelper Create(byte[] sdm, int startIndex)
  {
    return new EngineerHelper()
    {
      STSValue = Convert.ToString(sdm[startIndex - 1]),
      Data = BitConverter.ToString(sdm, startIndex)
    };
  }

  public override string Serialize() => $"{this.Data}|{this.STSValue}";

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.Data, "STSValue", this.STSValue),
        new AppDatum(eDatumType.Data, "Data", this.Data)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>() { "STSValue", "Data" };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) EngineerHelper.Deserialize(version, serialized);
  }

  public static EngineerHelper Deserialize(string version, string serialized)
  {
    EngineerHelper engineerHelper = new EngineerHelper();
    string[] strArray = serialized.Split(new char[1]{ '|' }, StringSplitOptions.RemoveEmptyEntries);
    engineerHelper.Data = strArray[0];
    engineerHelper.STSValue = "0";
    if (strArray.Length > 1)
      engineerHelper.STSValue = strArray[1];
    return engineerHelper;
  }

  public override string NotificationString
  {
    get
    {
      return $"STSValue: {(this.STSValue == "0" ? (object) this.STSValue : (object) Encoding.ASCII.GetBytes(this.STSValue).FormatBytesToStringArray())}, Data: {Encoding.ASCII.GetBytes(this.Data).FormatBytesToStringArray()}";
    }
  }

  public override object PlotValue => this.Data != null ? (object) this.Data : (object) null;

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

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CalibrationValues")
        this._CalibrationValues = sensorAttribute;
    }
  }

  public static void SetCalibrationValues(long sensorID, string CalibrationValues)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == nameof (CalibrationValues))
      {
        sensorAttribute.Value = CalibrationValues.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = nameof (CalibrationValues),
        Value = CalibrationValues.ToString(),
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
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Equal,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = EngineerHelper.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    string calibrationValues = EngineerHelper.GetCalibrationValues(sensor.SensorID);
    string[] strArray = calibrationValues.Split(new char[1]
    {
      '|'
    }, StringSplitOptions.RemoveEmptyEntries);
    if (calibrationValues.Length > 0)
    {
      if (strArray.Length > 1)
        numArrayList.Add(EngineerHelperBase.BuiltFrame(sensor.SensorID, strArray[0].ToInt(), strArray[1]));
      else
        numArrayList.Add(EngineerHelperBase.RawFrame(strArray[0]));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public new static void ClearPendingActionControlCommand(
    Sensor sensor,
    string version,
    int actionCommand,
    bool success,
    byte[] data)
  {
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    string CalibrationValues = "";
    if (!string.IsNullOrEmpty(collection["raw"]))
    {
      CalibrationValues = collection["raw"];
      sensor.PendingActionControlCommand = true;
    }
    if (!string.IsNullOrEmpty(collection["messageType"]) && !string.IsNullOrEmpty(collection["body"]))
    {
      CalibrationValues = $"{collection["messageType"]}|{collection["body"]}";
      sensor.PendingActionControlCommand = true;
    }
    EngineerHelper.SetCalibrationValues(sensor.SensorID, CalibrationValues);
    sensor.Save();
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : sensor.Hysteresis.ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : sensor.MinimumThreshold.ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : sensor.MaximumThreshold.ToString();
  }

  public new static bool ActionControlCommandIsUrgent(Sensor sensor, string version) => true;

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(EngineerHelper left, EngineerHelper right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(EngineerHelper left, EngineerHelper right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(EngineerHelper left, EngineerHelper right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(EngineerHelper left, EngineerHelper right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(EngineerHelper left, EngineerHelper right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(EngineerHelper left, EngineerHelper right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is EngineerHelper && this.Equals((MonnitApplicationBase) (obj as EngineerHelper));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is EngineerHelper && this.Data == (right as EngineerHelper).Data;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is EngineerHelper && this.Data != (right as EngineerHelper).Data;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<' cannot be applied to operands of type 'byte[]' and 'byte[]'");
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<=' cannot be applied to operands of type 'byte[]' and 'byte[]'");
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>' cannot be applied to operands of type 'byte[]' and 'byte[]'");
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>=' cannot be applied to operands of type 'byte[]' and 'byte[]'");
  }

  public static Dictionary<string, string> MeasurementScaleValue()
  {
    return new Dictionary<string, string>()
    {
      {
        "hex",
        "Hex"
      }
    };
  }

  public new static NotificationScaleInfoModel GetScalingInfo(Sensor sens)
  {
    NotificationScaleInfoModel scalingInfo = new NotificationScaleInfoModel();
    new EngineerHelper().SetSensorAttributes(sens.SensorID);
    scalingInfo.Label = "Hex";
    scalingInfo.sensor = sens;
    return scalingInfo;
  }
}
