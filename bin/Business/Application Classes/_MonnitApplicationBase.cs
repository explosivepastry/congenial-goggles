// Decompiled with JetBrains decompiler
// Type: Monnit.MonnitApplicationBase
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Monnit;

public abstract class MonnitApplicationBase
{
  public static string NotificationVersion = "1";

  public abstract long ApplicationID { get; }

  public abstract string ChartType { get; }

  public abstract string Serialize();

  public abstract MonnitApplicationBase _Deserialize(string version, string serialized);

  public abstract string NotificationString { get; }

  public abstract object PlotValue { get; }

  protected static string GetAttributeValue(
    long sensorID,
    string attributeName,
    string defaultValue)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == attributeName)
        return sensorAttribute.Value;
    }
    return defaultValue;
  }

  protected static void SetAttribute(long sensorID, string attributeName, string attributeValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == attributeName)
      {
        if (string.IsNullOrEmpty(attributeValue))
        {
          sensorAttribute.Delete();
        }
        else
        {
          sensorAttribute.Value = attributeValue;
          sensorAttribute.Save();
        }
        flag = true;
        break;
      }
    }
    if (!flag && !string.IsNullOrEmpty(attributeValue))
      new SensorAttribute()
      {
        Name = attributeName,
        Value = attributeValue,
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string SpecialExportValue(long monnitAppID, MonnitApplicationBase appBase)
  {
    if (!MonnitApplicationBase.HasSpecialExportValue(monnitAppID))
      return "";
    return MonnitApplicationBase.GetType(monnitAppID).CallStaticMethod(nameof (SpecialExportValue), new object[1]
    {
      (object) appBase
    }).ToStringSafe();
  }

  public static bool HasSpecialExportValue(long monnitAppID)
  {
    return MonnitApplicationBase.GetType(monnitAppID).GetMethod("SpecialExportValue", BindingFlags.Static | BindingFlags.Public) != (MethodInfo) null;
  }

  public static bool HasStaticMethod(long monnitAppID, string methodname)
  {
    return MonnitApplicationBase.GetType(monnitAppID).GetMethod(methodname, BindingFlags.Static | BindingFlags.Public) != (MethodInfo) null;
  }

  public virtual bool MonnitApplicationCompare(
    MonnitApplicationBase left,
    eCompareType type,
    MonnitApplicationBase right)
  {
    switch (type)
    {
      case eCompareType.Equal:
        if (left.Equals(right))
          return true;
        break;
      case eCompareType.Not_Equal:
        if (left.NotEqual(right))
          return true;
        break;
      case eCompareType.Greater_Than:
        if (left.GreaterThan(right))
          return true;
        break;
      case eCompareType.Less_Than:
        if (left.LessThan(right))
          return true;
        break;
      case eCompareType.Greater_Than_or_Equal:
        if (left.GreaterThanEqual(right))
          return true;
        break;
      case eCompareType.Less_Than_or_Equal:
        if (left.LessThanEqual(right))
          return true;
        break;
      default:
        return false;
    }
    return false;
  }

  public virtual bool IsValid => true;

  public virtual bool equal(MonnitApplicationBase right, int dIndex = 0)
  {
    if (MonnitApplicationBase.GetType(this.ApplicationID) == MonnitApplicationBase.GetType(right.ApplicationID))
      return this.Datums[dIndex].data == right.Datums[dIndex].data;
    throw new InvalidCastException();
  }

  public virtual bool notEqual(MonnitApplicationBase right, int dIndex = 0)
  {
    if (MonnitApplicationBase.GetType(this.ApplicationID) == MonnitApplicationBase.GetType(right.ApplicationID))
      return this.Datums[dIndex].data == right.Datums[dIndex].data;
    throw new InvalidCastException();
  }

  public static Dictionary<string, object> GetDefaults(Sensor sensor)
  {
    return (Dictionary<string, object>) _ApplicationBase.GetType(sensor.ApplicationID).CallStaticMethod(nameof (GetDefaults), new object[2]
    {
      (object) new Version(sensor.FirmwareVersion),
      (object) sensor.GenerationType
    });
  }

  public abstract bool Equals(MonnitApplicationBase right);

  public abstract bool NotEqual(MonnitApplicationBase right);

  public abstract bool LessThan(MonnitApplicationBase right);

  public abstract bool LessThanEqual(MonnitApplicationBase right);

  public abstract bool GreaterThan(MonnitApplicationBase right);

  public abstract bool GreaterThanEqual(MonnitApplicationBase right);

  public static Type CheckType(long applicationID) => MonnitApplicationBase.GetType(applicationID);

  private static Type GetType(long applicationID)
  {
    long num1 = applicationID;
    long num2 = num1;
    if ((ulong) num2 <= 158UL)
    {
      switch ((uint) num2)
      {
        case 0:
          return typeof (APN);
        case 1:
          return typeof (Analog);
        case 2:
          return typeof (Temperature);
        case 3:
          return typeof (DryContact);
        case 4:
          return typeof (Water);
        case 5:
          return typeof (Vibration);
        case 6:
          return typeof (HallEffect);
        case 7:
          return typeof (VibrationHallEffect);
        case 8:
          return typeof (Light);
        case 9:
          return typeof (OpenClosed);
        case 10:
        case 38:
        case 87:
        case 88:
        case 96 /*0x60*/:
        case 112 /*0x70*/:
        case 133:
        case 146:
        case 148:
        case 149:
        case 155:
        case 156:
        case 157:
          goto label_152;
        case 11:
          return typeof (ButtonLed);
        case 12:
          return typeof (Control_1);
        case 13:
          return typeof (Attention);
        case 14:
          return typeof (ActiveID);
        case 15:
          return typeof (Accelerometer);
        case 16 /*0x10*/:
          return typeof (AccelerometerTriggered);
        case 17:
          return typeof (ActiveID_REN);
        case 18:
          return typeof (Humidity);
        case 19:
          return typeof (Activity_2);
        case 20:
          return typeof (Accel_Espen);
        case 21:
          return typeof (Lux);
        case 22:
          return typeof (ZeroToTwentyMilliamp);
        case 23:
          return typeof (PIR);
        case 24:
          return typeof (Flex);
        case 25:
          return typeof (TempTesting);
        case 26:
          return typeof (LiquidLevel);
        case 27:
          return typeof (LightBool);
        case 28:
          return typeof (Compass);
        case 29:
          return typeof (HAHumidity);
        case 30:
          return typeof (HumidityGPP);
        case 31 /*0x1F*/:
          return typeof (VoltageDetection120);
        case 32 /*0x20*/:
          return typeof (AC_DC_500V);
        case 33:
          return typeof (VehiclePresence);
        case 34:
          return typeof (Gas_CO);
        case 35:
          return typeof (TemperatureRTD);
        case 36:
          return typeof (LiquidLevel24);
        case 37:
          return typeof (Control_2);
        case 39:
          return typeof (VehicleDetector);
        case 40:
          return typeof (VehicleSpeed);
        case 41:
          return typeof (Pressure_4to20ma);
        case 42:
          return typeof (LoggedActivity);
        case 43:
          return typeof (HumiditySHT25);
        case 44:
          return typeof (SerialDataBridge);
        case 45:
          return typeof (SmartRanger);
        case 46:
          return typeof (TemperatureRTDLow);
        case 47:
          return typeof (MultiPulseCounter);
        case 48 /*0x30*/:
          return typeof (PulseCounter);
        case 49:
          return typeof (SevenSwitchState);
        case 50:
          return typeof (CallerID);
        case 51:
          return typeof (Seat);
        case 52:
          return typeof (AirFlow);
        case 53:
          return typeof (TemperatureMulti);
        case 54:
          return typeof (VoltageDetection240);
        case 55:
          return typeof (Current_CT1MA);
        case 56:
          return typeof (Gas_H2S);
        case 57:
          return typeof (Gas_NO2);
        case 58:
          return typeof (Gas_O3);
        case 59:
          return typeof (BatteryHealth);
        case 60:
          return typeof (WeightedActivity);
        case 61:
          return typeof (WeightedPIR);
        case 62:
          return typeof (WeightedSeat);
        case 63 /*0x3F*/:
          return typeof (GPS);
        case 64 /*0x40*/:
          return typeof (ACVoltageDetection);
        case 65:
          return typeof (WaterTemperature);
        case 66:
          return typeof (Asset);
        case 67:
          return typeof (Ultrasonic);
        case 68:
          return typeof (AGPS);
        case 69:
          return typeof (DualPulseCounter);
        case 70:
          return typeof (Resistance);
        case 71:
          return typeof (FiveVoltDetector);
        case 72:
          return typeof (ZeroToFiveVolts);
        case 73:
          return typeof (FilteredPulseCounter);
        case 74:
          return typeof (ZeroToTenVolts);
        case 75:
          return typeof (Tilt);
        case 76:
          return typeof (BasicControl);
        case 77:
          return typeof (PeopleCounter);
        case 78:
          return typeof (WaterArea);
        case 79:
          return typeof (Pressure50PSI);
        case 80 /*0x50*/:
          return typeof (LoggedMultiPulse);
        case 81:
          return typeof (CTPower);
        case 82:
          return typeof (Pressure300PSI);
        case 83:
          return typeof (PressureNPSI);
        case 84:
          return typeof (DuctTemperature);
        case 85:
          return typeof (Short_Range_Asset);
        case 86:
          return typeof (Thermocouple);
        case 89:
          return typeof (Current);
        case 90:
          return typeof (FilteredPulseCounter64);
        case 91:
          return typeof (MotionTemp);
        case 92:
          return typeof (QuadTemperature);
        case 93:
          return typeof (CurrentZeroToTwentyAmp);
        case 94:
          return typeof (CurrentZeroToOneFiftyAmp);
        case 95:
          return typeof (VibrationMeter);
        case 97:
          return typeof (Thermostat);
        case 98:
          return typeof (RiceTilt);
        case 99:
          return typeof (ResistanceDelta);
        case 100:
          return typeof (QTipTemperature);
        case 101:
          return typeof (AltaPIR);
        case 102:
          return typeof (AirQuality);
        case 103:
          return typeof (DifferentialPressure);
        case 104:
          return typeof (Vibration800);
        case 105:
          return typeof (UltrasonicRangerIndustrial);
        case 106:
          return typeof (CO2Meter);
        case 107:
          return typeof (LightSensor);
        case 108:
          return typeof (ThreePhasePowerMeter);
        case 109:
          return typeof (ThreePhaseCurrentMeter);
        case 110:
          return typeof (DwellTime);
        case 111:
          return typeof (AdvancedVibration);
        case 113:
          return typeof (ZeroToTwoHundredVolts);
        case 114:
          return typeof (AirSpeed);
        case 115:
          return typeof (Scale_Controller);
        case 116:
          return typeof (CO_Meter);
        case 117:
          return typeof (AssetLocationRepeater);
        case 118:
          return typeof (AssetLocationTag);
        case 119:
          return typeof (CarDetect);
        case 120:
          return typeof (CurrentZeroTo500Amp);
        case 121:
          return typeof (QuartzdynePressure);
        case 122:
          return typeof (VoltageMeter500VAC);
        case 123:
          return typeof (VoltageDetect200VDC);
        case 124:
          return typeof (WestellPropane);
        case 125:
          return typeof (MultiStageThermostat);
        case 126:
          return typeof (GeforceMaxAvg);
        case (uint) sbyte.MaxValue:
          return typeof (QuadContact);
        case 128 /*0x80*/:
          return typeof (HandheldFoodProbe);
        case 129:
          return typeof (ThreePhase500AmpMeter);
        case 130:
          return typeof (TriggeredTilt);
        case 131:
          return typeof (FilteredQuadTemperature);
        case 132:
          return typeof (FilteredTemperature);
        case 134:
          return typeof (SootBlower);
        case 135:
          return typeof (SoilMoisture);
        case 136:
          return typeof (LCD_Temperature);
        case 137:
          return typeof (ThreePhase20AmpMeter);
        case 138:
          return typeof (PIRHumidity);
        case 139:
          return typeof (LightSensor_PPFD);
        case 140:
          return typeof (SootBlower2);
        case 141:
          return typeof (CurrentZeroToFiveAmp);
        case 142:
          return typeof (AdvancedVibration2);
        case 143:
          return typeof (SiteSurvey);
        case 144 /*0x90*/:
          return typeof (Pressure750PSI);
        case 145:
          return typeof (Pressure3000PSI);
        case 147:
          return typeof (Humidity_5PCal);
        case 150:
          return typeof (Motion_RH_WaterDetect);
        case 151:
          return typeof (FiveInputDryContact);
        case 152:
          return typeof (LN2Level);
        case 153:
          return typeof (TwoInputPulseCounter);
        case 154:
          return typeof (ResistiveBridgeMeter);
        case 158:
          return typeof (LatchedDualControl);
      }
    }
    if (num1 == 65520L)
      return typeof (CableBase);
    if (num1 == 65534L)
      return typeof (EngineerHelper);
label_152:
    throw new NotImplementedException($"ApplicationID: {applicationID.ToString()} is not implemented");
  }

  public virtual List<object> GetPlotValues(long sensorID)
  {
    return new List<object>() { this.PlotValue };
  }

  public virtual List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>(this.Datums.Select<AppDatum, string>((Func<AppDatum, string>) (dat => dat.type)));
  }

  public static object CallStaticMethod(long applicationID, string MethodName, object[] parameters)
  {
    return MonnitApplicationBase.GetType(applicationID).CallStaticMethod(MethodName, parameters);
  }

  public static MonnitApplicationBase LoadMonnitApplication(
    string version,
    string serialized,
    long applicationID)
  {
    return MonnitApplicationBase.LoadMonnitApplication(version, serialized, applicationID, long.MinValue);
  }

  public static MonnitApplicationBase LoadMonnitApplication(
    string version,
    string serialized,
    long applicationID,
    long sensorID)
  {
    MonnitApplicationBase monnitApplicationBase = (MonnitApplicationBase) MonnitApplicationBase.GetType(applicationID).CallStaticMethod("Deserialize", new object[2]
    {
      (object) version,
      (object) serialized
    });
    if (monnitApplicationBase is ISensorAttribute && sensorID > long.MinValue)
      (monnitApplicationBase as ISensorAttribute).SetSensorAttributes(sensorID);
    return monnitApplicationBase;
  }

  public static MonnitApplicationBase CreateMonnitApplication(
    byte[] sdm,
    int startIndex,
    long applicationID)
  {
    return (MonnitApplicationBase) MonnitApplicationBase.GetType(applicationID).CallStaticMethod("Create", new object[2]
    {
      (object) sdm,
      (object) startIndex
    });
  }

  public static string ApplicationName(long applicationID)
  {
    string stringSafe = MonnitApplicationBase.GetType(applicationID).GetStaticPropertyValue(nameof (ApplicationName)).ToStringSafe();
    return string.IsNullOrEmpty(stringSafe) ? "Unknown" : stringSafe;
  }

  public static eApplicationProfileType ProfileType(long monnitApplicationID)
  {
    return (eApplicationProfileType) MonnitApplicationBase.GetType(monnitApplicationID).GetStaticPropertyValue(nameof (ProfileType));
  }

  public static void DefaultConfigurationSettings(Sensor sensor)
  {
    sensor.SuppressPropertyChangedEvent = true;
    MonnitApplicationBase.GetType(sensor.ApplicationID).CallStaticMethod(nameof (DefaultConfigurationSettings), new object[1]
    {
      (object) sensor
    });
    sensor.SuppressPropertyChangedEvent = false;
  }

  public static void DefaultCalibrationSettings(Sensor sensor)
  {
    Type type = MonnitApplicationBase.GetType(sensor.ApplicationID);
    if (!type.HasStaticMethod(nameof (DefaultCalibrationSettings)))
      return;
    type.CallStaticMethod(nameof (DefaultCalibrationSettings), new object[1]
    {
      (object) sensor
    });
  }

  public static void DefaultRFSettings(Sensor sensor)
  {
    sensor.SuppressPropertyChangedEvent = true;
    sensor.TransmitPower = sensor.DefaultValue<int>("DefaultTransmitPower");
    sensor.TransmitPowerOptions = sensor.DefaultValue<int>("DefaultTXPowerOptions");
    sensor.ReceiveSensitivity = sensor.DefaultValue<int>("DefaultReceiveSensitivity");
    Type type = MonnitApplicationBase.GetType(sensor.ApplicationID);
    if (type.HasStaticMethod(nameof (DefaultRFSettings)))
      type.CallStaticMethod(nameof (DefaultRFSettings), new object[1]
      {
        (object) sensor
      });
    sensor.SuppressPropertyChangedEvent = false;
  }

  public static void DuplicateConfiguration(Sensor source, Sensor target)
  {
    if (source.ApplicationID != target.ApplicationID)
      return;
    Type type = MonnitApplicationBase.GetType(source.ApplicationID);
    if (type.HasStaticMethod(nameof (DuplicateConfiguration)))
    {
      type.CallStaticMethod(nameof (DuplicateConfiguration), new object[2]
      {
        (object) source,
        (object) target
      });
    }
    else
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
      target.Calibration1 = source.Calibration1;
      target.Calibration2 = source.Calibration2;
      target.Calibration3 = source.Calibration3;
      target.Calibration4 = source.Calibration4;
      target.EventDetectionType = source.EventDetectionType;
      target.EventDetectionPeriod = source.EventDetectionPeriod;
      target.EventDetectionCount = source.EventDetectionCount;
      target.RearmTime = source.RearmTime;
      target.BiStable = source.BiStable;
      target.TagString = source.TagString;
      if (new Version(source.FirmwareVersion) >= new Version("1.2.009") && source.SensorTypeID != 4L && new Version(target.FirmwareVersion) >= new Version("1.2.009") && target.SensorTypeID != 4L)
        target.TimeOfDayActive = source.TimeOfDayActive;
    }
  }

  public static Notification NotificationByApplicationID(Sensor sensor)
  {
    return (Notification) MonnitApplicationBase.GetType(sensor.ApplicationID).CallStaticMethod(nameof (NotificationByApplicationID), new object[1]
    {
      (object) sensor
    });
  }

  public static Dictionary<string, string> NotificationScaleValues(long applicationID)
  {
    try
    {
      return (Dictionary<string, string>) MonnitApplicationBase.GetType(applicationID).CallStaticMethod(nameof (NotificationScaleValues), new object[0]);
    }
    catch
    {
      return new Dictionary<string, string>();
    }
  }

  public static bool VerifyNotificationValues(Notification notification, string scale)
  {
    try
    {
      if (notification.ApplicationID > 0L)
      {
        Type type = MonnitApplicationBase.GetType(notification.ApplicationID);
        if (type.HasStaticMethod(nameof (VerifyNotificationValues)))
        {
          type.CallStaticMethod(nameof (VerifyNotificationValues), new object[2]
          {
            (object) notification,
            (object) scale
          });
          return true;
        }
      }
      Type type1 = AppDatum.getType(notification.eDatumType);
      if (type1.HasStaticMethod(nameof (VerifyNotificationValues)))
      {
        type1.CallStaticMethod(nameof (VerifyNotificationValues), new object[2]
        {
          (object) notification,
          (object) scale
        });
        return true;
      }
    }
    catch
    {
    }
    return false;
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    out NameValueCollection returnValues)
  {
    returnValues = new NameValueCollection();
    MonnitApplicationBase.GetType(sensor.ApplicationID).CallStaticMethod(nameof (SetProfileSettings), new object[3]
    {
      (object) sensor,
      (object) collection,
      (object) returnValues
    });
  }

  public static NotificationScaleInfoModel GetScalingInfo(Sensor sens)
  {
    Dictionary<string, string> dictionary = new Dictionary<string, string>();
    return MonnitApplicationBase.GetType(sens.ApplicationID).CallStaticMethod(nameof (GetScalingInfo), new object[1]
    {
      (object) sens
    }) as NotificationScaleInfoModel;
  }

  public static List<UnitConversion> GetScales(Sensor sens, eDatumType t)
  {
    List<UnitConversion> scales = new List<UnitConversion>();
    scales.Add(new UnitConversion(1.0, 0.0, "", "", ""));
    if (!AppDatum.HasStaticMethod(t, nameof (GetScales)))
      return scales;
    return AppDatum.getType(t).CallStaticMethod(nameof (GetScales), new object[1]
    {
      (object) sens
    }) as List<UnitConversion>;
  }

  public static List<UnitConversion> GetScales(long sensId, eDatumType t)
  {
    return MonnitApplicationBase.GetScales(Sensor.Load(sensId), t);
  }

  public static string GetNotifyWhenString(eDatumType t)
  {
    string str = "Notify when reading is";
    return AppDatum.HasStaticMethod(t, nameof (GetNotifyWhenString)) ? AppDatum.getType(t).CallStaticMethod(nameof (GetNotifyWhenString), new object[0]) as string : str;
  }

  public static bool ShouldShowEqualSelectOption(eDatumType t)
  {
    bool flag = false;
    bool result;
    return AppDatum.HasStaticMethod(t, nameof (ShouldShowEqualSelectOption)) && AppDatum.getType(t).CallStaticMethod(nameof (ShouldShowEqualSelectOption), new object[0]) is string str && bool.TryParse(str, out result) ? result : flag;
  }

  public static List<DropdownItemForRules> GetRuleDropDownValues(eDatumType t)
  {
    List<DropdownItemForRules> dropdownItemForRulesList = new List<DropdownItemForRules>()
    {
      new DropdownItemForRules()
      {
        DisplayString = "Triggered",
        Value = "True"
      },
      new DropdownItemForRules()
      {
        DisplayString = "Not Triggered",
        Value = "False"
      }
    };
    return AppDatum.HasStaticMethod(t, nameof (GetRuleDropDownValues)) ? AppDatum.getType(t).CallStaticMethod(nameof (GetRuleDropDownValues), new object[0]) as List<DropdownItemForRules> : dropdownItemForRulesList;
  }

  public static void CalibrateSensor(NameValueCollection collection, Sensor sensor)
  {
    MonnitApplicationBase.GetType(sensor.ApplicationID).CallStaticMethod(nameof (CalibrateSensor), new object[2]
    {
      (object) sensor,
      (object) collection
    });
  }

  public static void SensorCalibrate(Sensor sensor, NameValueCollection collection)
  {
    MonnitApplicationBase.GetType(sensor.ApplicationID).CallStaticMethod(nameof (SensorCalibrate), new object[2]
    {
      (object) sensor,
      (object) collection
    });
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    out NameValueCollection returnValues)
  {
    returnValues = new NameValueCollection();
    MonnitApplicationBase.GetType(sensor.ApplicationID).CallStaticMethod(nameof (SensorScale), new object[3]
    {
      (object) sensor,
      (object) collection,
      (object) returnValues
    });
  }

  public static void ControlSensor(NameValueCollection collection, Sensor sensor)
  {
    MonnitApplicationBase.GetType(sensor.ApplicationID).CallStaticMethod("Control", new object[2]
    {
      (object) sensor,
      (object) collection
    });
  }

  public static void TerminalSensor(NameValueCollection collection, Sensor sensor)
  {
    MonnitApplicationBase.GetType(sensor.ApplicationID).CallStaticMethod("Terminal", new object[2]
    {
      (object) sensor,
      (object) collection
    });
  }

  public static bool HasCalibration(Sensor sensor)
  {
    return MonnitApplicationBase.GetType(sensor.ApplicationID).GetMethod("CalibrateSensor", BindingFlags.Static | BindingFlags.Public) != (MethodInfo) null;
  }

  public static bool HasControl(Sensor sensor)
  {
    return MonnitApplicationBase.GetType(sensor.ApplicationID).GetMethod("Control", BindingFlags.Static | BindingFlags.Public) != (MethodInfo) null;
  }

  public static bool HasSensorFile(Sensor sensor)
  {
    return MonnitApplicationBase.GetType(sensor.ApplicationID).GetMethod("SensorFile", BindingFlags.Static | BindingFlags.Public) != (MethodInfo) null;
  }

  public static void ProfileSettingsForIntervalUI(
    Sensor sensor,
    out string Hyst,
    out string Min,
    out string Max)
  {
    Hyst = "";
    Min = "";
    Max = "";
    Type type = MonnitApplicationBase.GetType(sensor.ApplicationID);
    try
    {
      Hyst = type.CallStaticMethod("HystForUI", new object[1]
      {
        (object) sensor
      }).ToStringSafe();
      Min = type.CallStaticMethod("MinThreshForUI", new object[1]
      {
        (object) sensor
      }).ToStringSafe();
      Max = type.CallStaticMethod("MaxThreshForUI", new object[1]
      {
        (object) sensor
      }).ToStringSafe();
    }
    catch (Exception ex)
    {
    }
  }

  public static void ProfileLabelForScale(Sensor sensor, out string label)
  {
    label = "";
    Type type = MonnitApplicationBase.GetType(sensor.ApplicationID);
    try
    {
      label = type.CallStaticMethod("GetLabel", new object[1]
      {
        (object) sensor.SensorID
      }).ToStringSafe();
    }
    catch
    {
      label = "";
    }
  }

  public static void ProfileSettingsForIntervalUI(Sensor sensor, out string Hyst)
  {
    Hyst = "";
    Type type = MonnitApplicationBase.GetType(sensor.ApplicationID);
    try
    {
      Hyst = type.CallStaticMethod("HystForUI", new object[1]
      {
        (object) sensor
      }).ToStringSafe();
    }
    catch (Exception ex)
    {
    }
  }

  public static void ProfileSettingsForIntervalUI(Sensor sensor, out string Min, out string Max)
  {
    Max = "";
    Min = "";
    Type type = MonnitApplicationBase.GetType(sensor.ApplicationID);
    try
    {
      Min = type.CallStaticMethod("MinThreshForUI", new object[1]
      {
        (object) sensor
      }).ToStringSafe();
      Max = type.CallStaticMethod("MaxThreshForUI", new object[1]
      {
        (object) sensor
      }).ToStringSafe();
    }
    catch (Exception ex)
    {
    }
  }

  public static void ProfileSettingsForIntervalUI(
    Sensor sensor,
    out string tempMin,
    out string tempMax,
    out string temphyst,
    out string conMin,
    out string conMax,
    out string conHyst,
    out string twaMin,
    out string twaMax,
    out string twaHyst)
  {
    tempMin = " ";
    conMin = "  ";
    twaMin = " ";
    tempMax = " ";
    conMax = "  ";
    twaMax = " ";
    temphyst = " ";
    conHyst = "  ";
    twaHyst = " ";
    Type type = MonnitApplicationBase.GetType(sensor.ApplicationID);
    try
    {
      tempMin = type.CallStaticMethod("tempMinThreshForUI", new object[1]
      {
        (object) sensor
      }).ToStringSafe();
      conMin = type.CallStaticMethod("conMinThreshForUI", new object[1]
      {
        (object) sensor
      }).ToStringSafe();
      twaMin = type.CallStaticMethod("twaMinThreshForUI", new object[1]
      {
        (object) sensor
      }).ToStringSafe();
      tempMax = type.CallStaticMethod("tempMaxThreshForUI", new object[1]
      {
        (object) sensor
      }).ToStringSafe();
      conMax = type.CallStaticMethod("conMaxThreshForUI", new object[1]
      {
        (object) sensor
      }).ToStringSafe();
      twaMax = type.CallStaticMethod("twaMaxThreshForUI", new object[1]
      {
        (object) sensor
      }).ToStringSafe();
      temphyst = type.CallStaticMethod("tempHystForUI", new object[1]
      {
        (object) sensor
      }).ToStringSafe();
      conHyst = type.CallStaticMethod("conHystForUI", new object[1]
      {
        (object) sensor
      }).ToStringSafe();
      twaHyst = type.CallStaticMethod("twaHystForUI", new object[1]
      {
        (object) sensor
      }).ToStringSafe();
    }
    catch (Exception ex)
    {
    }
  }

  public static void ProfileSettingsForTriggeredUI(
    Sensor sensor,
    out string EventDetectionTypeDescription)
  {
    EventDetectionTypeDescription = "";
    Type type = MonnitApplicationBase.GetType(sensor.ApplicationID);
    try
    {
      EventDetectionTypeDescription = type.GetStaticPropertyValue(nameof (EventDetectionTypeDescription)).ToStringSafe();
    }
    catch (Exception ex)
    {
    }
  }

  public static void ProfileSettingsForTriggeredUI(
    Sensor sensor,
    out string valueForZero,
    out string valueForOne)
  {
    valueForZero = "";
    valueForOne = "";
    Type type = MonnitApplicationBase.GetType(sensor.ApplicationID);
    try
    {
      valueForZero = type.GetStaticPropertyValue("ValueForZero").ToStringSafe();
      valueForOne = type.GetStaticPropertyValue("ValueForOne").ToStringSafe();
    }
    catch (Exception ex)
    {
    }
  }

  public static void ProfileSettingsForTriggeredUI(
    Sensor sensor,
    out string EventDetectionTypeDescription,
    out string valueForZero,
    out string valueForOne)
  {
    EventDetectionTypeDescription = "";
    valueForZero = "";
    valueForOne = "";
    Type type = MonnitApplicationBase.GetType(sensor.ApplicationID);
    try
    {
      EventDetectionTypeDescription = type.GetStaticPropertyValue(nameof (EventDetectionTypeDescription)).ToStringSafe();
      valueForZero = type.GetStaticPropertyValue("ValueForZero").ToStringSafe();
      valueForOne = type.GetStaticPropertyValue("ValueForOne").ToStringSafe();
    }
    catch (Exception ex)
    {
    }
  }

  public static void DefaultThresholds(Sensor sensor, out long profileMin, out long profileMax)
  {
    profileMin = long.MaxValue;
    profileMax = long.MinValue;
    Type type = MonnitApplicationBase.GetType(sensor.ApplicationID);
    try
    {
      profileMax = type.GetStaticPropertyValue("DefaultMaxThreshold").ToLong();
      profileMin = type.GetStaticPropertyValue("DefaultMinThreshold").ToLong();
    }
    catch (Exception ex)
    {
    }
  }

  public static object CustomApplicationValues(Sensor sensor, string propertyName)
  {
    Type type = MonnitApplicationBase.GetType(sensor.ApplicationID);
    try
    {
      return type.GetStaticPropertyValue(propertyName);
    }
    catch (Exception ex)
    {
      return (object) null;
    }
  }

  public static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    try
    {
      return (List<byte[]>) MonnitApplicationBase.GetType(sensor.ApplicationID).CallStaticMethod(nameof (ActionControlCommand), new object[2]
      {
        (object) sensor,
        (object) version
      });
    }
    catch
    {
    }
    return (List<byte[]>) null;
  }

  public static bool ActionControlCommandIsUrgent(Sensor sensor, string version)
  {
    try
    {
      Type type = MonnitApplicationBase.GetType(sensor.ApplicationID);
      if (type.HasStaticMethod(nameof (ActionControlCommandIsUrgent)))
        return (bool) type.CallStaticMethod(nameof (ActionControlCommandIsUrgent), new object[2]
        {
          (object) sensor,
          (object) version
        });
    }
    catch
    {
    }
    return false;
  }

  public static void ClearPendingActionControlCommand(
    Sensor sensor,
    string version,
    int actionCommand,
    bool success,
    byte[] data)
  {
    try
    {
      if (MonnitApplicationBase.GetType(sensor.ApplicationID).HasStaticMethod(nameof (ClearPendingActionControlCommand)))
        MonnitApplicationBase.GetType(sensor.ApplicationID).CallStaticMethod(nameof (ClearPendingActionControlCommand), new object[5]
        {
          (object) sensor,
          (object) version,
          (object) actionCommand,
          (object) success,
          (object) data
        });
      else if (sensor.PendingActionControlCommand)
        sensor.PendingActionControlCommand = false;
    }
    catch
    {
      if (!sensor.PendingActionControlCommand)
        return;
      sensor.PendingActionControlCommand = false;
    }
  }

  public abstract List<AppDatum> Datums { get; }

  public static List<eDatumType> GetDatumTypes(long ApplicationID)
  {
    MonnitApplicationBase monnitApplicationBase = (MonnitApplicationBase) MonnitApplicationBase.GetType(ApplicationID).GetConstructor(new Type[0]).Invoke(new object[0]);
    List<eDatumType> datumTypes = new List<eDatumType>();
    foreach (AppDatum datum in monnitApplicationBase.Datums)
      datumTypes.Add(datum.etype);
    return datumTypes;
  }

  public static List<AppDatum> GetAppDatums(long ApplicationID)
  {
    return ((MonnitApplicationBase) MonnitApplicationBase.GetType(ApplicationID).GetConstructor(new Type[0]).Invoke(new object[0])).Datums;
  }
}
