// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.Millimeter
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

public class Millimeter : DataTypeBase
{
  public double value;

  public override eDatumType datatype => eDatumType.Millimeter;

  public override object compvalue => (object) this.value;

  public Millimeter(double mms) => this.value = mms;

  public static NotificationScaleInfoModel GetScalingInfo(long i)
  {
    return i <= 0L ? Millimeter.getScales()[(int) (-1L * i)] : MonnitApplicationBase.GetScalingInfo(Sensor.Load(i));
  }

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    List<UnitConversion> scales = new List<UnitConversion>();
    scales.Add(new UnitConversion(1.0, 0.0, "Millimeters", "Millimeters", "Millimeters"));
    scales.Add(new UnitConversion(10.0, 0.0, "Centimeters", "Millimeters", "Centimeters"));
    scales.Add(new UnitConversion(1000.0, 0.0, "Meters", "Millimeters", "Meters"));
    scales.Add(new UnitConversion(25.4, 0.0, "Inches", "Millimeters", "Inches"));
    scales.Add(new UnitConversion(304.8, 0.0, "Feet", "Millimeters", "Feet"));
    scales.Add(new UnitConversion(914.4, 0.0, "Yards", "Millimeters", "Yards"));
    UnitConversion unitConversion1 = new UnitConversion();
    if (sens == null)
    {
      UnitConversion unitConversion2 = new UnitConversion(1.0, 0.0, "Millimeters", "Millimeters", "Custom");
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
      unitConversion1 = new UnitConversion(num1, num2, empty3, "Millimeters", "Custom");
    }
    catch
    {
      unitConversion1.Slope = 1.0;
      unitConversion1.Intercept = 0.0;
      unitConversion1.UnitFrom = "Millimeters";
      unitConversion1.UnitTo = "Millimeters";
      unitConversion1.UnitLabel = "Custom";
    }
    scales.Add(unitConversion1);
    return scales;
  }

  public static List<NotificationScaleInfoModel> getScales()
  {
    return new List<NotificationScaleInfoModel>()
    {
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 1.0, 0.0, "mm", "mm"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 10.0, 0.0, "cm", "cm"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 1000.0, 0.0, "Meters", "Meters"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 25.4, 0.0, "Inches", "Inches"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 304.8, 0.0, "Feet", "Feet"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 914.4, 0.0, "Yards", "Yards")
    };
  }

  public static double MillimeterToFeet(double mms) => mms * 0.0032808399;

  public static double FeetToMillimeter(double d) => d / 0.0032808399;

  public static double MillimeterToYard(double mms) => mms * 0.0032808399 / 3.0;

  public static double YardToMillimeter(double d) => d / 0.0032808399 * 3.0;

  public static double CentimeterToMillimeter(double cm) => cm * 10.0;

  public static double MillimeterToCentimeter(double mm) => mm / 10.0;

  public static double MeterToMillimeter(double m) => m * 1000.0;

  public static double MillimeterToMeter(double mm) => mm / 1000.0;

  public static double InchToMillimeter(double inches) => inches / 0.0393701;

  public static double MillimeterToInch(double d) => d * 0.0393701;
}
