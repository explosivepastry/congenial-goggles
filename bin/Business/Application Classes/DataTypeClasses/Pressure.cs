// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.Pressure
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

public class Pressure : DataTypeBase
{
  public double value;

  public override eDatumType datatype => eDatumType.Pressure;

  public override object compvalue => (object) this.value;

  public Pressure(double v) => this.value = v;

  public static NotificationScaleInfoModel GetScalingInfo(long i)
  {
    return i <= 0L ? Pressure.getScales()[(int) (-1L * i)] : MonnitApplicationBase.GetScalingInfo(Sensor.Load(i));
  }

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    List<UnitConversion> scales = new List<UnitConversion>();
    scales.Add(new UnitConversion(1.0, 0.0, "PSI", "PSI", "PSI"));
    scales.Add(new UnitConversion(14.6959488, 0.0, "atm", "PSI", "atm"));
    scales.Add(new UnitConversion(14.503773772954, 0.0, "bar", "PSI", "bar"));
    scales.Add(new UnitConversion(0.14503773773020923, 0.0, "kPA", "PSI", "kPA"));
    scales.Add(new UnitConversion(0.0193367747, 0.0, "Torr", "PSI", "Torr"));
    UnitConversion unitConversion1 = new UnitConversion();
    if (sens == null)
    {
      UnitConversion unitConversion2 = new UnitConversion(1.0, 0.0, "PSI", "PSI", "Custom");
      scales.Add(unitConversion2);
      return scales;
    }
    try
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sens.SensorID))
      {
        if (sensorAttribute.Name == "LowValue")
          empty1 = sensorAttribute.Value;
        if (sensorAttribute.Name == "HighValue")
          empty2 = sensorAttribute.Value;
        if (sensorAttribute.Name == "CustomLabel")
          empty3 = sensorAttribute.Value;
      }
      double to1 = (double) sens.DefaultValue<long>("DefaultMinimumThreshold") / 10.0;
      double to2 = (double) sens.DefaultValue<long>("DefaultMaximumThreshold") / 10.0;
      (double num1, double num2) = ExtentionMethods.LinearInterpolation(empty1.ToDouble(), empty2.ToDouble(), to1, to2);
      if (double.IsInfinity(num1) || double.IsInfinity(num2))
      {
        num1 = 1.0;
        num2 = 0.0;
      }
      unitConversion1 = new UnitConversion(num1, num2, empty3, "PSI", "Custom");
    }
    catch
    {
      unitConversion1.Slope = 1.0;
      unitConversion1.Intercept = 0.0;
      unitConversion1.UnitFrom = "PSI";
      unitConversion1.UnitTo = "PSI";
      unitConversion1.UnitLabel = "Custom";
    }
    scales.Add(unitConversion1);
    return scales;
  }

  public static List<eCompareType> GetCompareType(Sensor sens)
  {
    return new List<eCompareType>()
    {
      eCompareType.Greater_Than,
      eCompareType.Less_Than
    };
  }

  public static List<NotificationScaleInfoModel> getScales()
  {
    return new List<NotificationScaleInfoModel>()
    {
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 1.0, 0.0, "PSI", "PSI"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 14.6959488, 0.0, "atm", "atm"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 0.0689475729, 0.0, "bar", "bar"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 6.89475729, 0.0, "kPA", "kPA"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 0.0193367747, 0.0, "Torr", "Torr")
    };
  }

  public static double FromPSI(double fromValue, string toUnit)
  {
    switch (toUnit.ToLower())
    {
      case "atm":
        return fromValue * 0.0680459639;
      case "bar":
        return fromValue * 0.0689475729;
      case "kpa":
        return fromValue * 6.8947572932;
      case "torr":
        return fromValue * 51.7149325716;
      case "psi":
        return fromValue;
      default:
        throw new InvalidOperationException("Pressure.FromPSI unknown toUnit: $" + toUnit);
    }
  }

  public static double ToPSI(double fromValue, string fromUnit)
  {
    switch (fromUnit.ToLower())
    {
      case "atm":
        return fromValue * 14.6959487755;
      case "bar":
        return fromValue * 14.503773773;
      case "kpa":
        return fromValue * 0.1450377377;
      case "torr":
        return fromValue * 0.0193367747;
      case "psi":
        return fromValue;
      default:
        throw new InvalidOperationException("Pressure.ToPSI unknown fromUnit: $" + fromUnit);
    }
  }
}
