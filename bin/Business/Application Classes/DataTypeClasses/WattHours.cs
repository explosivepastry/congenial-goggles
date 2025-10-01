// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.WattHours
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

public class WattHours : DataTypeBase
{
  public double value;

  public override eDatumType datatype => eDatumType.WattHours;

  public override object compvalue => (object) this.value;

  public WattHours(double v) => this.value = v;

  public WattHours(double watts, eWattHoursType eWatts)
  {
    switch (eWatts)
    {
      case eWattHoursType.Watts:
        this.value = watts;
        break;
      case eWattHoursType.KilowattHours:
        this.value = WattHours.WattsToKilowattHours(watts);
        break;
      case eWattHoursType.Megajoule:
        this.value = WattHours.WattsToMegajoule(watts);
        break;
    }
  }

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    List<UnitConversion> scales = new List<UnitConversion>();
    scales.Add(new UnitConversion(1.0, 0.0, "w", "w", "w"));
    scales.Add(new UnitConversion(1000.0, 0.0, "kwh", "kwh", "kwh"));
    scales.Add(new UnitConversion(277.777777, 0.0, "mj", "w", "mj"));
    UnitConversion unitConversion1 = new UnitConversion();
    if (sens == null)
    {
      UnitConversion unitConversion2 = new UnitConversion(1.0, 0.0, "w", "w", "Custom");
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
      if (double.IsInfinity(num1) || double.IsInfinity(num2) || double.IsNaN(num1) || double.IsNaN(num2))
      {
        num1 = 1.0;
        num2 = 0.0;
      }
      unitConversion1 = new UnitConversion(num1, num2, empty3, "w", "Custom");
    }
    catch
    {
      unitConversion1.Slope = 1.0;
      unitConversion1.Intercept = 0.0;
      unitConversion1.UnitFrom = "w";
      unitConversion1.UnitTo = "w";
      unitConversion1.UnitLabel = "Custom";
    }
    scales.Add(unitConversion1);
    return scales;
  }

  public double asKilowattHours() => WattHours.WattsToKilowattHours(this.value);

  public double asMegajoule() => WattHours.WattsToMegajoule(this.value);

  public double asWatts() => this.value;

  public static double WattsToKilowattHours(double w) => w / 1000.0;

  public static double WattsToMegajoule(double w) => w / 1000.0 * 3.6;
}
