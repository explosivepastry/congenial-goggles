// Decompiled with JetBrains decompiler
// Type: Monnit.ThreePhase20AmpMeter
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class ThreePhase20AmpMeter : MonnitApplicationBase, ISensorAttribute
{
  private double _WattHours = double.MinValue;
  private SensorAttribute _PowerFactor;
  private SensorAttribute _UseEstimatedPower;
  private SensorAttribute _SelectedCurrentReading;
  private SensorAttribute _Label;
  private SensorAttribute _VoltValue;
  private SensorAttribute _DataViewOption;

  public static long MonnitApplicationID => 137;

  public static string ApplicationName => "Three Phase 20 Amp Meter";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => ThreePhase20AmpMeter.MonnitApplicationID;

  public double TotalCurrentAccumulation { get; set; }

  public double Phase1Average { get; set; }

  public double Phase1Max { get; set; }

  public double Phase1Min { get; set; }

  public double Phase1Duty { get; set; }

  public double Phase2Average { get; set; }

  public double Phase2Max { get; set; }

  public double Phase2Min { get; set; }

  public double Phase2Duty { get; set; }

  public double Phase3Average { get; set; }

  public double Phase3Max { get; set; }

  public double Phase3Min { get; set; }

  public double Phase3Duty { get; set; }

  public double WattHours
  {
    get
    {
      if (this._WattHours <= 0.0)
      {
        switch (this.Label.Value.ToLower())
        {
          case "mj":
          case "kwh":
            this._WattHours = !this.UseEstimatedPower.Value.ToBool() ? Math.Round(this.TotalCurrentAccumulation * this.VoltValue.Value.ToDouble(), 1) : ThreePhase20AmpMeter.CalculatePowerEstimate(this.VoltValue.Value.ToDouble(), this.GetPhaseReading(this.SelectedCurrentReading.Value.ToInt()), this.PowerFactor.Value.ToDouble(), this.GetHoursForCurrentReading());
            break;
          default:
            this._WattHours = 0.0;
            break;
        }
      }
      return this._WattHours;
    }
    set => this._WattHours = value;
  }

  public static ThreePhase20AmpMeter Create(byte[] sdm, int startIndex)
  {
    ThreePhase20AmpMeter threePhase20AmpMeter = new ThreePhase20AmpMeter();
    try
    {
      threePhase20AmpMeter.Phase1Average = BitConverter.ToUInt16(sdm, startIndex).ToDouble() / 100.0;
      threePhase20AmpMeter.Phase1Max = (double) sdm[startIndex + 2] / 10.0;
      threePhase20AmpMeter.Phase1Min = (double) sdm[startIndex + 3] / 10.0;
      threePhase20AmpMeter.Phase1Duty = (double) sdm[startIndex + 4];
      threePhase20AmpMeter.Phase2Average = BitConverter.ToUInt16(sdm, startIndex + 5).ToDouble() / 100.0;
      threePhase20AmpMeter.Phase2Max = (double) sdm[startIndex + 7] / 10.0;
      threePhase20AmpMeter.Phase2Min = (double) sdm[startIndex + 8] / 10.0;
      threePhase20AmpMeter.Phase2Duty = (double) sdm[startIndex + 9];
      threePhase20AmpMeter.Phase3Average = BitConverter.ToUInt16(sdm, startIndex + 10).ToDouble() / 100.0;
      threePhase20AmpMeter.Phase3Max = (double) sdm[startIndex + 12] / 10.0;
      threePhase20AmpMeter.Phase3Min = (double) sdm[startIndex + 13] / 10.0;
      threePhase20AmpMeter.Phase3Duty = (double) sdm[startIndex + 14];
      threePhase20AmpMeter.TotalCurrentAccumulation = BitConverter.ToUInt32(sdm, startIndex + 15).ToDouble() / 10.0;
    }
    catch
    {
      threePhase20AmpMeter.Phase1Average = 0.0;
      threePhase20AmpMeter.Phase1Max = 0.0;
      threePhase20AmpMeter.Phase1Min = 0.0;
      threePhase20AmpMeter.Phase1Duty = 0.0;
      threePhase20AmpMeter.Phase2Average = 0.0;
      threePhase20AmpMeter.Phase2Max = 0.0;
      threePhase20AmpMeter.Phase2Min = 0.0;
      threePhase20AmpMeter.Phase2Duty = 0.0;
      threePhase20AmpMeter.Phase3Average = 0.0;
      threePhase20AmpMeter.Phase3Max = 0.0;
      threePhase20AmpMeter.Phase3Min = 0.0;
      threePhase20AmpMeter.Phase3Duty = 0.0;
      threePhase20AmpMeter.TotalCurrentAccumulation = 0.0;
    }
    return threePhase20AmpMeter;
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[14]
      {
        new AppDatum(eDatumType.Amps, "Phase1Average", this.Phase1Average),
        new AppDatum(eDatumType.Amps, "Phase1Max", this.Phase1Max),
        new AppDatum(eDatumType.Amps, "Phase1Min", this.Phase1Min),
        new AppDatum(eDatumType.Percentage, "Phase1Duty", this.Phase1Duty),
        new AppDatum(eDatumType.Amps, "Phase2Average", this.Phase2Average),
        new AppDatum(eDatumType.Amps, "Phase2Max", this.Phase2Max),
        new AppDatum(eDatumType.Amps, "Phase2Min", this.Phase2Min),
        new AppDatum(eDatumType.Percentage, "Phase2Duty", this.Phase2Duty),
        new AppDatum(eDatumType.Amps, "Phase3Average", this.Phase3Average),
        new AppDatum(eDatumType.Amps, "Phase3Max", this.Phase3Max),
        new AppDatum(eDatumType.Amps, "Phase3Min", this.Phase3Min),
        new AppDatum(eDatumType.Percentage, "Phase3Duty", this.Phase3Duty),
        new AppDatum(eDatumType.AmpHours, "TotalCurrentAccumulation", this.TotalCurrentAccumulation),
        new AppDatum(eDatumType.WattHours, "TotalPowerEstimate", this.WattHours)
      });
    }
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        this._Label = sensorAttribute;
      if (sensorAttribute.Name == "VoltValue")
        this._VoltValue = sensorAttribute;
      if (sensorAttribute.Name == "DataViewOption")
        this._DataViewOption = sensorAttribute;
      if (sensorAttribute.Name == "PowerFactor")
        this._PowerFactor = sensorAttribute;
      if (sensorAttribute.Name == "SelectedCurrentReading")
        this._SelectedCurrentReading = sensorAttribute;
      if (sensorAttribute.Name == "UseEstimatedPower")
        this._UseEstimatedPower = sensorAttribute;
    }
  }

  public SensorAttribute PowerFactor
  {
    get
    {
      if (this._PowerFactor == null)
        this._PowerFactor = new SensorAttribute()
        {
          Value = "0"
        };
      return this._PowerFactor;
    }
  }

  public static void SetPowerFactor(long sensorID, double powerFactor)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "PowerFactor")
      {
        sensorAttribute.Value = powerFactor.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "PowerFactor",
        Value = powerFactor.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static double GetPowerFactor(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "PowerFactor")
        return sensorAttribute.Value.ToDouble();
    }
    return 0.0;
  }

  public SensorAttribute UseEstimatedPower
  {
    get
    {
      if (this._UseEstimatedPower == null)
        this._UseEstimatedPower = new SensorAttribute()
        {
          Value = "false"
        };
      return this._UseEstimatedPower;
    }
  }

  public static void SetUseEstimatedPower(long sensorID, bool useEstimatedPower)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "UseEstimatedPower")
      {
        sensorAttribute.Value = useEstimatedPower.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "UseEstimatedPower",
        Value = useEstimatedPower.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static bool GetUseEstimatedPower(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "UseEstimatedPower")
        return sensorAttribute.Value.ToBool();
    }
    return false;
  }

  public SensorAttribute SelectedCurrentReading
  {
    get
    {
      if (this._SelectedCurrentReading == null)
        this._SelectedCurrentReading = new SensorAttribute()
        {
          Value = "0"
        };
      return this._SelectedCurrentReading;
    }
  }

  public static void SetSelectedCurrentReading(long sensorID, int selectedCurrentReading)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "SelectedCurrentReading")
      {
        sensorAttribute.Value = selectedCurrentReading.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "SelectedCurrentReading",
        Value = selectedCurrentReading.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static int GetSelectedCurrentReading(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "SelectedCurrentReading")
        return sensorAttribute.Value.ToInt();
    }
    return 0;
  }

  public SensorAttribute Label
  {
    get
    {
      if (this._Label == null)
        this._Label = new SensorAttribute() { Value = "Ah" };
      return this._Label;
    }
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

  public static string GetLabel(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        return sensorAttribute.Value;
    }
    return "Amp Hours";
  }

  public SensorAttribute VoltValue
  {
    get
    {
      if (this._VoltValue == null)
        this._VoltValue = new SensorAttribute()
        {
          Value = "0"
        };
      return this._VoltValue;
    }
  }

  public static void SetVoltValue(long sensorID, double lowValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "VoltValue")
      {
        sensorAttribute.Value = lowValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "VoltValue",
        Value = lowValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static double GetVoltValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "VoltValue")
        return sensorAttribute.Value.ToDouble();
    }
    return 0.0;
  }

  public SensorAttribute DataViewOption
  {
    get
    {
      if (this._DataViewOption == null)
        this._DataViewOption = new SensorAttribute()
        {
          Name = nameof (DataViewOption),
          Value = "1"
        };
      return this._DataViewOption;
    }
  }

  public static int GetDataViewOption(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "DataViewOption")
        return sensorAttribute.Value.ToInt();
    }
    return 1;
  }

  public static void SetDataViewOption(long sensorID, int dataOption)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "DataViewOption")
      {
        sensorAttribute.Value = dataOption.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "DataViewOption",
        Value = dataOption.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public override string Serialize()
  {
    return $"{this.Phase1Average.ToString()}|{this.Phase1Max.ToString()}|{this.Phase1Min.ToString()}|{this.Phase1Duty.ToString()}|{this.Phase2Average.ToString()}|{this.Phase2Max.ToString()}|{this.Phase2Min.ToString()}|{this.Phase2Duty.ToString()}|{this.Phase3Average.ToString()}|{this.Phase3Max.ToString()}|{this.Phase3Min.ToString()}|{this.Phase3Duty.ToString()}|{this.TotalCurrentAccumulation.ToString()}|{this.WattHours.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) ThreePhase20AmpMeter.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      string notificationString;
      switch (this.DataViewOption.Value.ToInt())
      {
        case 2:
          object[] objArray1 = new object[5];
          double num1 = this.PlotValue.ToDouble();
          objArray1[0] = (object) num1.ToString("0.0");
          num1 = this.Phase1Average;
          objArray1[1] = (object) num1.ToString("0.00");
          num1 = this.Phase2Average;
          objArray1[2] = (object) num1.ToString("0.00");
          num1 = this.Phase3Average;
          objArray1[3] = (object) num1.ToString("0.00");
          objArray1[4] = (object) this.Label.Value;
          notificationString = string.Format("{0} {4} | Phase 1: {1} | Phase 2: {2} | Phase 3: {3}", objArray1);
          break;
        case 3:
          object[] objArray2 = new object[14];
          double num2 = this.PlotValue.ToDouble();
          objArray2[0] = (object) num2.ToString("0.0");
          objArray2[1] = (object) this.Label.Value;
          num2 = this.Phase1Min;
          objArray2[2] = (object) num2.ToString("0.0");
          num2 = this.Phase1Max;
          objArray2[3] = (object) num2.ToString("0.0");
          num2 = this.Phase1Average;
          objArray2[4] = (object) num2.ToString("0.0");
          objArray2[5] = (object) this.Phase1Duty;
          num2 = this.Phase2Min;
          objArray2[6] = (object) num2.ToString("0.0");
          num2 = this.Phase2Max;
          objArray2[7] = (object) num2.ToString("0.0");
          num2 = this.Phase2Average;
          objArray2[8] = (object) num2.ToString("0.0");
          objArray2[9] = (object) this.Phase2Duty;
          num2 = this.Phase3Min;
          objArray2[10] = (object) num2.ToString("0.0");
          num2 = this.Phase3Max;
          objArray2[11] = (object) num2.ToString("0.0");
          num2 = this.Phase3Average;
          objArray2[12] = (object) num2.ToString("0.0");
          objArray2[13] = (object) this.Phase3Duty;
          notificationString = string.Format("{0} {1} |  Phase 1: Min {2}, Max {3}, Avg {4}, Duty {5}% | Phase 2: Min {6}, Max {7}, Avg {8}, Duty {9}% | Phase 3: Min {10}, Max {11}, Avg {12}, Duty {13}%", objArray2);
          break;
        default:
          notificationString = $"{this.PlotValue.ToDouble().ToString("0.0")} {this.Label.Value}";
          break;
      }
      return notificationString;
    }
  }

  public override object PlotValue
  {
    get
    {
      switch (this.Label.Value.ToLower())
      {
        case "kwh":
          return (object) (this.WattHours / 1000.0);
        case "mj":
          return (object) (this.WattHours / 1000.0 * 3.6);
        case "ah":
          return (object) this.TotalCurrentAccumulation;
        default:
          return (object) this.TotalCurrentAccumulation;
      }
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[14]
    {
      (object) this.Phase1Average,
      (object) this.Phase1Max,
      (object) this.Phase1Min,
      (object) this.Phase1Duty,
      (object) this.Phase2Average,
      (object) this.Phase2Max,
      (object) this.Phase2Min,
      (object) this.Phase2Duty,
      (object) this.Phase3Average,
      (object) this.Phase3Max,
      (object) this.Phase3Min,
      (object) this.Phase3Duty,
      (object) this.TotalCurrentAccumulation,
      (object) this.WattHours
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[14]
    {
      "Phase1Average",
      "Phase1Max",
      "Phase1Min",
      "Phase1Duty",
      "Phase2Average",
      "Phase2Max",
      "Phase2Min",
      "Phase2Duty",
      "Phase3Average",
      "Phase3Max",
      "Phase3Min",
      "Phase3Duty",
      "TotalCurrentAccumulation",
      "WattHours"
    });
  }

  public static ThreePhase20AmpMeter Deserialize(string version, string serialized)
  {
    ThreePhase20AmpMeter threePhase20AmpMeter = new ThreePhase20AmpMeter();
    if (string.IsNullOrEmpty(serialized))
    {
      threePhase20AmpMeter.Phase1Average = 0.0;
      threePhase20AmpMeter.Phase1Max = 0.0;
      threePhase20AmpMeter.Phase1Min = 0.0;
      threePhase20AmpMeter.Phase1Duty = 0.0;
      threePhase20AmpMeter.Phase2Average = 0.0;
      threePhase20AmpMeter.Phase2Max = 0.0;
      threePhase20AmpMeter.Phase2Min = 0.0;
      threePhase20AmpMeter.Phase2Duty = 0.0;
      threePhase20AmpMeter.Phase3Average = 0.0;
      threePhase20AmpMeter.Phase3Max = 0.0;
      threePhase20AmpMeter.Phase3Min = 0.0;
      threePhase20AmpMeter.Phase3Duty = 0.0;
      threePhase20AmpMeter.TotalCurrentAccumulation = 0.0;
      threePhase20AmpMeter.WattHours = 0.0;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      try
      {
        threePhase20AmpMeter.Phase1Average = strArray[0].ToDouble();
        if (strArray.Length > 1)
        {
          threePhase20AmpMeter.Phase1Max = strArray[1].ToDouble();
          threePhase20AmpMeter.Phase1Min = strArray[2].ToDouble();
          threePhase20AmpMeter.Phase1Duty = strArray[3].ToDouble();
          threePhase20AmpMeter.Phase2Average = strArray[4].ToDouble();
          threePhase20AmpMeter.Phase2Max = strArray[5].ToDouble();
          threePhase20AmpMeter.Phase2Min = strArray[6].ToDouble();
          threePhase20AmpMeter.Phase2Duty = strArray[7].ToDouble();
          threePhase20AmpMeter.Phase3Average = strArray[8].ToDouble();
          threePhase20AmpMeter.Phase3Max = strArray[9].ToDouble();
          threePhase20AmpMeter.Phase3Min = strArray[10].ToDouble();
          threePhase20AmpMeter.Phase3Duty = strArray[11].ToDouble();
          threePhase20AmpMeter.TotalCurrentAccumulation = strArray[12].ToDouble();
          if (strArray.Length > 13)
            threePhase20AmpMeter.WattHours = !double.IsNaN(strArray[13].ToDouble()) ? strArray[13].ToDouble() : 0.0;
        }
        else
        {
          threePhase20AmpMeter.Phase1Max = strArray[0].ToDouble();
          threePhase20AmpMeter.Phase1Min = strArray[0].ToDouble();
          threePhase20AmpMeter.Phase1Duty = strArray[0].ToDouble();
          threePhase20AmpMeter.Phase2Average = strArray[0].ToDouble();
          threePhase20AmpMeter.Phase2Max = strArray[0].ToDouble();
          threePhase20AmpMeter.Phase2Min = strArray[0].ToDouble();
          threePhase20AmpMeter.Phase2Duty = strArray[0].ToDouble();
          threePhase20AmpMeter.Phase3Average = strArray[0].ToDouble();
          threePhase20AmpMeter.Phase3Max = strArray[0].ToDouble();
          threePhase20AmpMeter.Phase3Min = strArray[0].ToDouble();
          threePhase20AmpMeter.Phase3Duty = strArray[0].ToDouble();
          threePhase20AmpMeter.TotalCurrentAccumulation = strArray[0].ToDouble();
          threePhase20AmpMeter.WattHours = strArray[0].ToDouble();
        }
      }
      catch
      {
        threePhase20AmpMeter.Phase1Average = 0.0;
        threePhase20AmpMeter.Phase1Max = 0.0;
        threePhase20AmpMeter.Phase1Min = 0.0;
        threePhase20AmpMeter.Phase1Duty = 0.0;
        threePhase20AmpMeter.Phase2Average = 0.0;
        threePhase20AmpMeter.Phase2Max = 0.0;
        threePhase20AmpMeter.Phase2Min = 0.0;
        threePhase20AmpMeter.Phase2Duty = 0.0;
        threePhase20AmpMeter.Phase3Average = 0.0;
        threePhase20AmpMeter.Phase3Max = 0.0;
        threePhase20AmpMeter.Phase3Min = 0.0;
        threePhase20AmpMeter.Phase3Duty = 0.0;
        threePhase20AmpMeter.TotalCurrentAccumulation = 0.0;
        threePhase20AmpMeter.WattHours = 0.0;
      }
    }
    return threePhase20AmpMeter;
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

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = ThreePhase20AmpMeter.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["Phase123Min_Manual"]) && !string.IsNullOrEmpty(collection["Phase123Max_Manual"]))
    {
      int num1 = collection["Phase123Min_Manual"].ToInt();
      if (num1 < 0)
        num1 = 0;
      if (num1 > 25)
        num1 = 25;
      int num2 = collection["Phase123Max_Manual"].ToInt();
      if (num2 < 0)
        num2 = 0;
      if (num2 > 25)
        num2 = 25;
      if (num2 < num1)
        num2 = num1;
      if (num1 > num2)
        num1 = num2;
      int num3 = (num1 * 10).ToInt();
      int num4 = (num2 * 10).ToInt();
      ThreePhase20AmpMeter.SetPhase1ThreshMin(sensor, num3);
      ThreePhase20AmpMeter.SetPhase2ThreshMin(sensor, num3);
      ThreePhase20AmpMeter.SetPhase3ThreshMin(sensor, num3);
      ThreePhase20AmpMeter.SetPhase1ThreshMax(sensor, num4);
      ThreePhase20AmpMeter.SetPhase2ThreshMax(sensor, num4);
      ThreePhase20AmpMeter.SetPhase3ThreshMax(sensor, num4);
    }
    if (!string.IsNullOrEmpty(collection["Phase123Hyst_Manual"]))
    {
      int num5 = collection["Phase123Hyst_Manual"].ToInt();
      if (num5 < 0)
        num5 = 0;
      if (num5 > 25)
        num5 = 25;
      int num6 = (num5 * 10).ToInt();
      ThreePhase20AmpMeter.SetPhase1Hyst(sensor, num6);
      ThreePhase20AmpMeter.SetPhase2Hyst(sensor, num6);
      ThreePhase20AmpMeter.SetPhase3Hyst(sensor, num6);
    }
    if (!string.IsNullOrEmpty(collection["Phase123Duty_Manual"]))
    {
      int num7 = collection["Phase123Duty_Manual"].ToInt();
      if (num7 < 0)
        num7 = 0;
      if (num7 > 25)
        num7 = 25;
      int num8 = (num7 * 10).ToInt();
      ThreePhase20AmpMeter.SetPhase1Duty(sensor, num8);
      ThreePhase20AmpMeter.SetPhase2Duty(sensor, num8);
      ThreePhase20AmpMeter.SetPhase3Duty(sensor, num8);
    }
    if (!string.IsNullOrEmpty(collection["MeasurementInterval_Manual"]))
    {
      double num9 = collection["MeasurementInterval_Manual"].ToDouble();
      double num10 = collection["ActiveStateInterval"].ToDouble();
      if (num9 < 0.017)
        num9 = 0.017;
      if (num9 > 720.0)
        num9 = 720.0;
      if (num10 > 0.0)
      {
        if (num10 < 0.017)
          num10 = 0.017;
        if (num10 > 720.0)
          num10 = 720.0;
        if (num9 > num10)
          num9 = num10;
      }
      else
        num9 = 1.0;
      ThreePhase20AmpMeter.setMeasurementInterval(sensor, num9);
    }
    if (!string.IsNullOrEmpty(collection["Accumulate_Manual"]))
    {
      int num = collection["Accumulate_Manual"].ToInt();
      if (num > 1 || num < 0)
        num = 1;
      ThreePhase20AmpMeter.SetAccumulate(sensor, num);
    }
    if (string.IsNullOrEmpty(collection["DataView_Manual"]))
      return;
    int dataOption = collection["DataView_Manual"].ToInt();
    if (dataOption > 3 || dataOption < 0)
      dataOption = 1;
    ThreePhase20AmpMeter.SetDataViewOption(sensor.SensorID, dataOption);
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["voltValue"]))
      ThreePhase20AmpMeter.SetVoltValue(sensor.SensorID, collection["voltValue"].ToDouble());
    if (!string.IsNullOrEmpty(collection["label"]))
      ThreePhase20AmpMeter.SetLabel(sensor.SensorID, collection["label"]);
    if (!string.IsNullOrEmpty(collection["PowerFactorValue"]))
      ThreePhase20AmpMeter.SetPowerFactor(sensor.SensorID, collection["PowerFactorValue"].ToDouble());
    if (!string.IsNullOrEmpty(collection["SelectedPhase"]))
      ThreePhase20AmpMeter.SetSelectedCurrentReading(sensor.SensorID, collection["SelectedPhase"].ToInt());
    if (string.IsNullOrEmpty(collection["APorEP_Manual"]))
      return;
    ThreePhase20AmpMeter.SetUseEstimatedPower(sensor.SensorID, collection["APorEP_Manual"].ToInt().ToBool());
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    NonCachedAttribute.LoadBySensorIDAndName(sensor.SensorID, "Calibration")?.Save();
    sensor.ProfileConfigDirty = false;
    sensor.ProfileConfig2Dirty = false;
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public static double CalculatePowerEstimate(double V, double A, double COS0, double HB)
  {
    return Math.Round(Math.Sqrt(3.0) * V * A * COS0 * HB, 1);
  }

  public double GetHoursForCurrentReading()
  {
    double num = this.Phase1Average * this.Phase1Duty / 100.0 + this.Phase2Average * this.Phase2Duty / 100.0 + this.Phase3Average * this.Phase3Duty / 100.0;
    return num == 0.0 ? 0.0 : this.TotalCurrentAccumulation / num;
  }

  public double GetPhaseReading(int selectedPhase)
  {
    double phaseReading;
    switch (selectedPhase)
    {
      case 1:
        phaseReading = this.Phase1Average;
        break;
      case 2:
        phaseReading = this.Phase2Average;
        break;
      case 3:
        phaseReading = this.Phase3Average;
        break;
      default:
        phaseReading = (this.Phase1Average + this.Phase2Average + this.Phase3Average) / 3.0;
        break;
    }
    return phaseReading;
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    if (!sensor.IsWiFiSensor)
    {
      numArrayList.Add(ThreePhaseCurrentMeterBase.CalibrateFrame(sensor.SensorID));
      numArrayList.Add(sensor.ReadProfileConfig(29));
    }
    return numArrayList;
  }

  public static void SetPhase1Duty(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 16777215U /*0xFFFFFF*/;
    sensor.Hysteresis = (long) (num | (uint) ((value & (int) byte.MaxValue) << 24));
  }

  public static int GetPhase1Duty(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.Hysteresis & 4278190080L /*0xFF000000*/) >> 24);
  }

  public static void SetPhase1Hyst(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4278255615U;
    sensor.Hysteresis = (long) (num | (uint) ((value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static int GetPhase1Hyst(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.Hysteresis & 16711680L /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetPhase1ThreshMax(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4294902015U;
    sensor.Hysteresis = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static int GetPhase1ThreshMax(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.Hysteresis & 65280L) >> 8);
  }

  public static void SetPhase1ThreshMin(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Hysteresis) & 4294967040U;
    sensor.Hysteresis = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetPhase1ThreshMin(Sensor sensor)
  {
    return (int) Convert.ToUInt32(sensor.Hysteresis & (long) byte.MaxValue);
  }

  public static void SetPhase2Duty(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 16777215U /*0xFFFFFF*/;
    sensor.MinimumThreshold = (long) (num | (uint) ((value & (int) byte.MaxValue) << 24));
  }

  public static int GetPhase2Duty(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.MinimumThreshold & 4278190080L /*0xFF000000*/) >> 24);
  }

  public static void SetPhase2Hyst(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4278255615U;
    sensor.MinimumThreshold = (long) (num | (uint) ((value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static int GetPhase2Hyst(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.MinimumThreshold & 16711680L /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetPhase2ThreshMax(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4294902015U;
    sensor.MinimumThreshold = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static int GetPhase2ThreshMax(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.MinimumThreshold & 65280L) >> 8);
  }

  public static void SetPhase2ThreshMin(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MinimumThreshold) & 4294967040U;
    sensor.MinimumThreshold = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetPhase2ThreshMin(Sensor sensor)
  {
    return (int) Convert.ToUInt32(sensor.MinimumThreshold & (long) byte.MaxValue);
  }

  public static void SetPhase3Duty(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 16777215U /*0xFFFFFF*/;
    sensor.MaximumThreshold = (long) (num | (uint) ((value & (int) byte.MaxValue) << 24));
  }

  public static int GetPhase3Duty(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.MaximumThreshold & 4278190080L /*0xFF000000*/) >> 24);
  }

  public static void SetPhase3Hyst(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 4278255615U;
    sensor.MaximumThreshold = (long) (num | (uint) ((value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static int GetPhase3Hyst(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.MaximumThreshold & 16711680L /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetPhase3ThreshMax(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 4294902015U;
    sensor.MaximumThreshold = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static int GetPhase3ThreshMax(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.MaximumThreshold & 65280L) >> 8);
  }

  public static void SetPhase3ThreshMin(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.MaximumThreshold) & 4294967040U;
    sensor.MaximumThreshold = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetPhase3ThreshMin(Sensor sensor)
  {
    return (int) Convert.ToUInt32(sensor.MaximumThreshold & (long) byte.MaxValue);
  }

  public static void SetCalVal1(Sensor sens, int value) => sens.Calibration1 = (long) value;

  public static int GetCalVal1(Sensor sens) => sens.Calibration1.ToInt();

  public static void SetCalVal2(Sensor sens, int value) => sens.Calibration2 = (long) value;

  public static int GetCalVal2(Sensor sens) => sens.Calibration2.ToInt();

  public static void SetCalVal3(Sensor sens, int value) => sens.Calibration3 = (long) value;

  public static int GetCalVal3(Sensor sens) => sens.Calibration3.ToInt();

  public static void setMeasurementInterval(Sensor sens, double value)
  {
    value *= 60.0;
    int num = (int) ((long) (int) sens.Calibration4 & 4294901760L);
    sens.Calibration4 = (long) (num | (int) (ushort) value);
  }

  public static double GetMeasurementInterval(Sensor sens)
  {
    return Math.Round(((uint) ((int) (uint) sens.Calibration4 & (int) ushort.MaxValue)).ToDouble() / 60.0, 3);
  }

  public static void SetAccumulate(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration4) & 4278255615U;
    sensor.Calibration4 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static int GetAccumulate(Sensor sensor)
  {
    return (int) (Convert.ToUInt32(sensor.Calibration4 & 16711680L /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(ThreePhase20AmpMeter left, ThreePhase20AmpMeter right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(ThreePhase20AmpMeter left, ThreePhase20AmpMeter right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(ThreePhase20AmpMeter left, ThreePhase20AmpMeter right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(ThreePhase20AmpMeter left, ThreePhase20AmpMeter right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(ThreePhase20AmpMeter left, ThreePhase20AmpMeter right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(ThreePhase20AmpMeter left, ThreePhase20AmpMeter right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is ThreePhase20AmpMeter && this.Equals((MonnitApplicationBase) (obj as ThreePhase20AmpMeter));
  }

  public override bool Equals(MonnitApplicationBase right) => false;

  public override bool NotEqual(MonnitApplicationBase right) => false;

  public override bool LessThan(MonnitApplicationBase right) => false;

  public override bool LessThanEqual(MonnitApplicationBase right) => false;

  public override bool GreaterThan(MonnitApplicationBase right) => false;

  public override bool GreaterThanEqual(MonnitApplicationBase right) => false;

  public static long DefaultMinThreshold => (long) uint.MaxValue;

  public static long DefaultMaxThreshold => (long) uint.MaxValue;

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
