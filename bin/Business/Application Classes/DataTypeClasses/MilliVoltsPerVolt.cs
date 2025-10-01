// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.MilliVoltsPerVolts
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

internal class MilliVoltsPerVolts : DataTypeBase
{
  public double value;

  public override eDatumType datatype => eDatumType.MilliVoltsPerVolts;

  public override object compvalue => (object) this.value;

  public MilliVoltsPerVolts(double v) => this.value = v;

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    List<UnitConversion> scales = new List<UnitConversion>();
    UnitConversion unitConversion = new UnitConversion();
    if (sens == null)
    {
      scales.Add(new UnitConversion(1.0, 0.0, nameof (MilliVoltsPerVolts), nameof (MilliVoltsPerVolts), "mV/V"));
      return scales;
    }
    try
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string fromUnits = string.Empty;
      string empty3 = string.Empty;
      foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sens.SensorID))
      {
        if (sensorAttribute.Name == "LowValue")
          empty1 = sensorAttribute.Value;
        if (sensorAttribute.Name == "HighValue")
          empty2 = sensorAttribute.Value;
        if (sensorAttribute.Name == "CustomLabel")
          fromUnits = sensorAttribute.Value;
        if (sensorAttribute.Name == "Label")
          empty3 = sensorAttribute.Value;
      }
      double to1 = (double) sens.DefaultValue<long>("DefaultMinimumThreshold");
      double to2 = (double) sens.DefaultValue<long>("DefaultMaximumThreshold");
      bool flag = true;
      if (empty1 == "" && empty2 == "")
        flag = false;
      if (empty1.ToDouble() == to1 && empty2.ToDouble() == to2)
        flag = false;
      if (flag && fromUnits == "")
        fromUnits = empty3;
      if (!flag)
      {
        scales.Add(new UnitConversion(1.0, 0.0, nameof (MilliVoltsPerVolts), "", "mV/V"));
        return scales;
      }
      (double num1, double num2) = ExtentionMethods.LinearInterpolation(empty1.ToDouble(), empty2.ToDouble(), to1, to2);
      if (double.IsInfinity(num1) || double.IsInfinity(num2))
      {
        num1 = 1.0;
        num2 = 0.0;
      }
      unitConversion = new UnitConversion(num1, num2, fromUnits, nameof (MilliVoltsPerVolts), "Custom");
    }
    catch
    {
      unitConversion.Slope = 1.0;
      unitConversion.Intercept = 0.0;
      unitConversion.UnitFrom = "";
      unitConversion.UnitTo = nameof (MilliVoltsPerVolts);
      unitConversion.UnitLabel = "Custom";
    }
    scales.Add(unitConversion);
    return scales;
  }
}
