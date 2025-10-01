// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.Angle
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

internal class Angle : DataTypeBase
{
  public double value;

  public override eDatumType datatype => eDatumType.Angle;

  public override object compvalue => (object) this.value;

  public Angle(double v) => this.value = Math.Round(v, 2);

  public Angle(int v) => this.value = Convert.ToDouble(v);

  public static Angle operator +(Angle a1, Angle a2) => new Angle(a1.value + a2.value);

  public static Angle operator -(Angle a1, Angle a2) => new Angle(a1.value - a2.value);

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    return new List<UnitConversion>()
    {
      new UnitConversion(1.0, 0.0, "Degrees", "", "Degrees")
    };
  }
}
