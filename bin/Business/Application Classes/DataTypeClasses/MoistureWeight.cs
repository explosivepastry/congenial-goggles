// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.MoistureWeight
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

public class MoistureWeight : DataTypeBase
{
  public double value;

  public override eDatumType datatype => eDatumType.MoistureWeight;

  public override object compvalue => (object) this.value;

  public MoistureWeight(double v) => this.value = v;

  public double asMetric() => this.value / 7.0;

  public double asStandard() => this.value;

  public static double MetricToStandard(double c) => c * 7.0;

  public static double StandardToMetric(double k) => k / 7.0;

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    return new List<UnitConversion>()
    {
      new UnitConversion(1.0, 0.0, "Grains Per Pound", "Grains Per Pound", "Grains Per Pound"),
      new UnitConversion(7.0, 0.0, "Grams Per Kilogram", "Grains Per Pound", "Grams Per Kilogram")
    };
  }
}
