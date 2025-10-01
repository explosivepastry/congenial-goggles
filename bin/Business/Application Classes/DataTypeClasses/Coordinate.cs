// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.Coordinate
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

internal class Coordinate : DataTypeBase
{
  public double value;

  public override eDatumType datatype => eDatumType.Coordinate;

  public override object compvalue => (object) this.value;

  public Coordinate(double v) => this.value = v;

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    return new List<UnitConversion>()
    {
      new UnitConversion(1.0, 0.0, "", "", "")
    };
  }

  public static string ShouldShowEqualSelectOption() => "true";
}
