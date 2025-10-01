// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.AirSpeedData
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

internal class AirSpeedData : DataTypeBase
{
  public double value;

  public override eDatumType datatype => eDatumType.Speed;

  public override object compvalue => (object) this.value;

  public AirSpeedData(double v) => this.value = v;

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    List<UnitConversion> scales = new List<UnitConversion>();
    scales.Add(new UnitConversion(1.0, 0.0, "mps", "mps", "mps"));
    scales.Add(new UnitConversion(0.44704, 0.0, "mph", "mps", "mph"));
    scales.Add(new UnitConversion(0.514444, 0.0, "knot", "mps", "knot"));
    scales.Add(new UnitConversion(5.0 / 18.0, 0.0, "kmph", "mps", "kmph"));
    UnitConversion unitConversion = new UnitConversion();
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
      double num1 = 1.0;
      double num2 = 0.0;
      double result1;
      double result2;
      if (double.TryParse(empty1, out result1) && double.TryParse(empty2, out result2))
        (num1, num2) = ExtentionMethods.LinearInterpolation(result1, result2, to1, to2);
      if (double.IsInfinity(num1) || double.IsInfinity(num2))
      {
        num1 = 1.0;
        num2 = 0.0;
      }
      unitConversion = new UnitConversion(num1, num2, empty3, "mps", "Custom");
    }
    catch
    {
      unitConversion.Slope = 1.0;
      unitConversion.Intercept = 0.0;
      unitConversion.UnitFrom = "";
      unitConversion.UnitTo = "mps";
      unitConversion.UnitLabel = "Custom";
    }
    scales.Add(unitConversion);
    return scales;
  }
}
