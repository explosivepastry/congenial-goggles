// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.Micrograms
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

internal class Micrograms : DataTypeBase
{
  public uint value;

  public override eDatumType datatype => eDatumType.Micrograms;

  public override object compvalue => (object) this.value;

  public Micrograms(uint v) => this.value = v;

  public Micrograms(int v)
  {
    if (v < 0)
      v = 0;
    this.value = Convert.ToUInt32(v);
  }

  public Micrograms(double v)
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
      new UnitConversion(1.0, 0.0, nameof (Micrograms), "", nameof (Micrograms))
    };
  }

  public static string ShouldShowEqualSelectOption() => "true";

  public new static void VerifyNotificationValues(Notification notification, string scale)
  {
    AirQuality.VerifyNotificationValues(notification, scale);
  }
}
