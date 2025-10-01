// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.Percentage
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

internal class Percentage : DataTypeBase
{
  public double value;

  public override eDatumType datatype => eDatumType.Percentage;

  public override object compvalue => (object) this.value;

  public Percentage(double p) => this.value = p;

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    return new List<UnitConversion>()
    {
      new UnitConversion(1.0, 0.0, "Percent", "", "Percent")
    };
  }

  public Percentage(int p) => this.value = (double) p / 100.0;

  public new string ToString() => (this.value * 100.0).ToString() + "%";

  public int asInt() => (int) (this.value * 100.0);
}
