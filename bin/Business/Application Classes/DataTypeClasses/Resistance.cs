// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.ResistanceData
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

internal class ResistanceData : DataTypeBase
{
  public double value;

  public override eDatumType datatype => eDatumType.ResistanceData;

  public override object compvalue => (object) this.value;

  public ResistanceData(double d) => this.value = d;

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    List<UnitConversion> scales = new List<UnitConversion>();
    scales.Add(new UnitConversion(1.0, 0.0, "Ohms", "Ohms", "Ohms"));
    UnitConversion unitConversion1 = new UnitConversion();
    if (sens == null)
    {
      UnitConversion unitConversion2 = new UnitConversion(1.0, 0.0, "Ohms", "Ohms", "Custom");
      scales.Add(unitConversion2);
      return scales;
    }
    if (sens.ApplicationID == 99L || sens.ApplicationID == 70L)
    {
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
          if (sensorAttribute.Name == "Label")
            empty3 = sensorAttribute.Value;
        }
        double to1 = (double) sens.DefaultValue<long>("DefaultMinimumThreshold");
        double to2 = (double) sens.DefaultValue<long>("DefaultMaximumThreshold");
        if (sens.ApplicationID == 70L)
        {
          to1 = 0.0;
          to2 = !sens.GenerationType.ToLower().Contains("gen2") ? 145000.0 : 250000.0;
        }
        else if (sens.ApplicationID == 99L)
        {
          to1 = ResistanceDelta.GetLowValue(long.MinValue);
          to2 = ResistanceDelta.GetHighValue(long.MinValue);
        }
        (double num1, double num2) = ExtentionMethods.LinearInterpolation(empty1.ToDouble(), empty2.ToDouble(), to1, to2);
        if (double.IsInfinity(num1) || double.IsInfinity(num2))
        {
          num1 = 1.0;
          num2 = 0.0;
        }
        unitConversion1 = new UnitConversion(num1, num2, empty3, "Ohms", "Custom");
      }
      catch
      {
        unitConversion1.Slope = 1.0;
        unitConversion1.Intercept = 0.0;
        unitConversion1.UnitFrom = "Ohms";
        unitConversion1.UnitTo = "Ohms";
        unitConversion1.UnitLabel = "Custom";
      }
      scales.Add(unitConversion1);
    }
    return scales;
  }

  public static string ShouldShowEqualSelectOption() => "true";
}
