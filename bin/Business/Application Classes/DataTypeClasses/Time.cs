// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.Time
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

internal class Time : DataTypeBase
{
  public int value;

  public override eDatumType datatype => eDatumType.Time;

  public override object compvalue => (object) this.value;

  public DateTime asDateTimeSeconds() => new DateTime((long) (this.value * 10000));

  public TimeSpan asTimeSpanSeconds() => new TimeSpan((long) (this.value * 10000));

  public Time(int t) => this.value = t;

  public Time(double t) => this.value = Convert.ToInt32(t);

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    return new List<UnitConversion>()
    {
      new UnitConversion(1.0, 0.0, "", "", "")
    };
  }

  public static string ShouldShowEqualSelectOption() => "true";
}
