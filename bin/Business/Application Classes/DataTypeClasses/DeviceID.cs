// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.DeviceID
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

internal class DeviceID : DataTypeBase
{
  public ulong value;

  public override eDatumType datatype => eDatumType.DeviceID;

  public override object compvalue => (object) this.value;

  public DeviceID(int v)
  {
    try
    {
      this.value = Convert.ToUInt64(v);
    }
    catch
    {
      this.value = 0UL;
    }
  }

  public DeviceID(long v)
  {
    try
    {
      this.value = Convert.ToUInt64(v);
    }
    catch
    {
      this.value = 0UL;
    }
  }

  public DeviceID(ulong v) => this.value = v;

  public DeviceID(double v)
  {
    try
    {
      this.value = Convert.ToUInt64(v);
    }
    catch
    {
      this.value = 0UL;
    }
  }

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    return new List<UnitConversion>()
    {
      new UnitConversion(1.0, 0.0, "", "", "")
    };
  }
}
