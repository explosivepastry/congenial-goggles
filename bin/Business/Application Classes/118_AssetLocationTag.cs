// Decompiled with JetBrains decompiler
// Type: Monnit.AssetLocationTag
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

public class AssetLocationTag : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;

  public static long MonnitApplicationID => 118;

  public static string ApplicationName => "Asset Location Tag";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => AssetLocationTag.MonnitApplicationID;

  public bool CartStatus { get; set; }

  public int DutyCycle { get; set; }

  public double PeakIntensity { get; set; }

  public double AverageIntensity { get; set; }

  public int MessageID { get; set; }

  public double Temperature { get; set; }

  public int stsStatus { get; set; }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[6]
      {
        new AppDatum(eDatumType.BooleanData, "CartStatus", this.CartStatus),
        new AppDatum(eDatumType.Percentage, "DutyCycle", this.DutyCycle),
        new AppDatum(eDatumType.Geforce, "PeakIntensity", this.PeakIntensity),
        new AppDatum(eDatumType.Geforce, "AverageIntensity", this.AverageIntensity),
        new AppDatum(eDatumType.Count, "MessageID", this.MessageID),
        new AppDatum(eDatumType.TemperatureData, "Temperature", this.Temperature)
      });
    }
  }

  public override string Serialize()
  {
    return $"{this.CartStatus.ToString()}|{this.DutyCycle.ToString()}|{this.PeakIntensity.ToString()}|{this.AverageIntensity.ToString()}|{this.MessageID.ToString()}|{this.Temperature.ToString()}|{this.stsStatus.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) AssetLocationTag.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      return $"CartStatus: {this.PlotValue?.ToString()} DutyCycle: {this.DutyCycle.ToString()}% PeakIntesity:{this.PeakIntensity.ToString()}g AverageIntensity: {this.AverageIntensity.ToString()} MessageID: {this.MessageID.ToString()} Temperature: {this.PlotTemperatureValue}";
    }
  }

  public override object PlotValue => this.CartStatus ? (object) "Moving" : (object) "Stationary";

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      this.PlotValue,
      (object) this.DutyCycle,
      (object) this.PeakIntensity,
      (object) this.AverageIntensity,
      (object) this.MessageID,
      (object) (AssetLocationTag.IsFahrenheit(sensorID) ? this.Temperature.ToFahrenheit() : this.Temperature)
    };
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>()
    {
      "CartStatus",
      "DutyCycle",
      "PeakIntensity (g)",
      "AverageIntensity (g)",
      "MessageID",
      AssetLocationTag.IsFahrenheit(sensorID) ? "Fahrenheit" : "Celsius"
    };
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

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
    }
  }

  public string PlotTemperatureValue
  {
    get
    {
      return this.CorF != null && this.CorF.Value == "C" ? this.Temperature.ToString("##.#° C", (IFormatProvider) CultureInfo.InvariantCulture) : this.Temperature.ToFahrenheit().ToString("##.#° F", (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }

  public static AssetLocationTag Deserialize(string version, string serialized)
  {
    AssetLocationTag assetLocationTag = new AssetLocationTag();
    if (string.IsNullOrEmpty(serialized))
    {
      assetLocationTag.stsStatus = 0;
      assetLocationTag.CartStatus = false;
      assetLocationTag.DutyCycle = 0;
      assetLocationTag.PeakIntensity = 0.0;
      assetLocationTag.AverageIntensity = 0.0;
      assetLocationTag.MessageID = 0;
      assetLocationTag.Temperature = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length > 1)
      {
        try
        {
          assetLocationTag.CartStatus = strArray[0].ToBool();
          assetLocationTag.DutyCycle = strArray[1].ToInt();
          assetLocationTag.PeakIntensity = strArray[2].ToDouble();
          assetLocationTag.AverageIntensity = strArray[3].ToDouble();
          assetLocationTag.MessageID = strArray[4].ToInt();
          assetLocationTag.Temperature = strArray[5].ToDouble();
          try
          {
            assetLocationTag.stsStatus = strArray[6].ToInt();
          }
          catch
          {
            assetLocationTag.stsStatus = 0;
          }
        }
        catch
        {
          assetLocationTag.stsStatus = 0;
          assetLocationTag.CartStatus = false;
          assetLocationTag.DutyCycle = 0;
          assetLocationTag.PeakIntensity = 0.0;
          assetLocationTag.AverageIntensity = 0.0;
          assetLocationTag.MessageID = 0;
          assetLocationTag.Temperature = 0.0;
        }
      }
      else
      {
        assetLocationTag.CartStatus = strArray[0].ToBool();
        assetLocationTag.DutyCycle = strArray[0].ToInt();
        assetLocationTag.PeakIntensity = strArray[0].ToDouble();
        assetLocationTag.AverageIntensity = strArray[0].ToDouble();
        assetLocationTag.MessageID = strArray[0].ToInt();
        assetLocationTag.Temperature = strArray[0].ToDouble();
      }
    }
    return assetLocationTag;
  }

  public static AssetLocationTag Create(byte[] sdm, int startIndex)
  {
    return new AssetLocationTag()
    {
      stsStatus = (int) sdm[startIndex - 1] >> 4,
      CartStatus = sdm[startIndex].ToBool(),
      DutyCycle = (int) sdm[startIndex + 1],
      PeakIntensity = BitConverter.ToInt16(sdm, startIndex + 2).ToDouble() / 100.0,
      AverageIntensity = BitConverter.ToInt16(sdm, startIndex + 4).ToDouble() / 100.0,
      MessageID = BitConverter.ToUInt16(sdm, startIndex + 6).ToInt(),
      Temperature = BitConverter.ToUInt16(sdm, startIndex + 8).ToDouble() / 10.0
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
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      SnoozeDuration = 60,
      CompareValue = "60",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = AssetLocationTag.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["SensitivityThreshold_Manual"]))
    {
      long num = collection["SensitivityThreshold_Manual"].ToLong();
      if (num < 0L)
        num = 0L;
      if (num > 10000L)
        num = 10000L;
      sensor.Calibration1 = num;
    }
    if (string.IsNullOrEmpty(collection["StopTimer_Manual"]))
      return;
    long num1 = collection["StopTimer_Manual"].ToLong();
    if (num1 < 0L)
      num1 = 0L;
    if (num1 > 120L)
      num1 = 120L;
    sensor.Calibration2 = num1;
  }

  public static string HystForUI(Sensor sensor) => "Not Used";

  public static string MinThreshForUI(Sensor sensor) => "Not Used";

  public static string MaxThreshForUI(Sensor sensor) => "Not Used";

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (collection["TempScale"] == "on")
    {
      viewData["TempScale"] = "F";
      AssetLocationTag.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      viewData["TempScale"] = "C";
      AssetLocationTag.MakeCelsius(sensor.SensorID);
    }
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(AssetLocationTag left, AssetLocationTag right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(AssetLocationTag left, AssetLocationTag right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(AssetLocationTag left, AssetLocationTag right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(AssetLocationTag left, AssetLocationTag right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(AssetLocationTag left, AssetLocationTag right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(AssetLocationTag left, AssetLocationTag right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is AssetLocationTag && this.Equals((MonnitApplicationBase) (obj as AssetLocationTag));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is AssetLocationTag && this.CartStatus == (right as AssetLocationTag).CartStatus;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is AssetLocationTag && this.CartStatus != (right as AssetLocationTag).CartStatus;
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
