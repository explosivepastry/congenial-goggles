// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.Distance
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

public class Distance : DataTypeBase
{
  public double value;

  public override eDatumType datatype => eDatumType.Distance;

  public override object compvalue => (object) this.value;

  public Distance(double inches) => this.value = inches;

  public static NotificationScaleInfoModel GetScalingInfo(long i)
  {
    return i <= 0L ? Distance.getScales()[(int) (-1L * i)] : MonnitApplicationBase.GetScalingInfo(Sensor.Load(i));
  }

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    List<UnitConversion> scales = new List<UnitConversion>();
    scales.Add(new UnitConversion(1.0, 0.0, "Inches", "Inches", "Inches"));
    scales.Add(new UnitConversion(36.0, 0.0, "Yards", "Inches", "Yards"));
    scales.Add(new UnitConversion(12.0, 0.0, "Feet", "Inches", "Feet"));
    scales.Add(new UnitConversion(0.393701, 0.0, "Centimeters", "Inches", "Centimeters"));
    scales.Add(new UnitConversion(39.3701, 0.0, "Meters", "Inches", "Meters"));
    UnitConversion unitConversion1 = new UnitConversion();
    if (sens == null)
    {
      UnitConversion unitConversion2 = new UnitConversion(1.0, 0.0, "Inches", "Inches", "Custom");
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
      double to1 = (double) sens.DefaultValue<long>("DefaultMinimumThreshold");
      double to2 = (double) sens.DefaultValue<long>("DefaultMaximumThreshold");
      (double num1, double num2) = ExtentionMethods.LinearInterpolation(empty1.ToDouble(), empty2.ToDouble(), to1, to2);
      if (double.IsInfinity(num1) || double.IsInfinity(num2))
      {
        num1 = 1.0;
        num2 = 0.0;
      }
      unitConversion1 = new UnitConversion(num1, num2, empty3, "Inches", "Custom");
    }
    catch
    {
      unitConversion1.Slope = 1.0;
      unitConversion1.Intercept = 0.0;
      unitConversion1.UnitFrom = "Inches";
      unitConversion1.UnitTo = "Inches";
      unitConversion1.UnitLabel = "Custom";
    }
    scales.Add(unitConversion1);
    return scales;
  }

  public static List<NotificationScaleInfoModel> getScales()
  {
    return new List<NotificationScaleInfoModel>()
    {
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 1.0, 0.0, "Inches", "Inches"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 36.0, 0.0, "Yards", "Yards"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 12.0, 0.0, "Feet", "Feet"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 0.393701, 0.0, "cm", "cm"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 0.0393701, 0.0, "mm", "mm"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 39.3701, 0.0, "Meters", "Meters")
    };
  }

  public double toInches() => this.value;

  public static double InchesToFeet(double inches) => inches / 12.0;

  public static double FeetToInches(double d) => d * 12.0;

  public static double InchesToYards(double inches) => inches / 36.0;

  public static double YardsToInches(double d) => d * 36.0;

  public static double InchesToMeter(double inches) => inches / 39.3701;

  public static double MetersToInches(double d) => d * 39.3701;

  public static double InchesToCentimeter(double inches) => inches / 0.393701;

  public static double CentimetersToInches(double d) => d * 0.393701;

  public static double InchesToMillimeters(double inches) => inches / 0.0393701;

  public static double MillimetersToInches(double d) => d * 0.0393701;
}
