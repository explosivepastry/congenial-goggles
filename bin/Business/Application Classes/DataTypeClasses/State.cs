// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.State
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

internal class State : DataTypeBase
{
  public int value;

  public override eDatumType datatype => eDatumType.State;

  public override object compvalue => (object) this.value;

  public State(int v) => this.value = v;

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    return new List<UnitConversion>()
    {
      new UnitConversion(1.0, 0.0, "", "", "")
    };
  }
}
