// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.UnscaledVoltage
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

internal class UnscaledVoltage(double v) : Voltage(v)
{
  public new double value;

  public override eDatumType datatype => eDatumType.UnscaledVoltage;

  public new static List<UnitConversion> GetScales(Sensor sens)
  {
    return new List<UnitConversion>()
    {
      new UnitConversion(1.0, 0.0, "", "", "")
    };
  }
}
