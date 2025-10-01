// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.PPB
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

internal class PPB : DataTypeBase
{
  public uint value;

  public override eDatumType datatype => eDatumType.PPB;

  public override object compvalue => (object) this.value;

  public PPB(uint v) => this.value = v;

  public PPB(int v)
  {
    if (v < 0)
      v = 0;
    this.value = Convert.ToUInt32(v);
  }

  public PPB(double v)
  {
    if (v < 0.0)
      v = 0.0;
    if (v > (double) uint.MaxValue)
      v = (double) uint.MaxValue;
    this.value = Convert.ToUInt32(v);
  }

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    return new List<UnitConversion>()
    {
      new UnitConversion(1.0, 0.0, nameof (PPB), "", nameof (PPB))
    };
  }
}
