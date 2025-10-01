// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.Count
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

internal class Count : DataTypeBase
{
  public ulong value;

  public override eDatumType datatype => eDatumType.Count;

  public override object compvalue => (object) this.value;

  public Count(ulong v) => this.value = v;

  public Count(int v)
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

  public Count(long v)
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

  public Count(double v)
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
    if (sens == null)
      return new List<UnitConversion>()
      {
        new UnitConversion(1.0, 0.0, "", "", "")
      };
    bool flag = sens.ApplicationID == 69L || sens.ApplicationID == 153L || sens.ApplicationID == 47L || sens.ApplicationID == 48L /*0x30*/ || sens.ApplicationID == 73L || sens.ApplicationID == 80L /*0x50*/ || sens.ApplicationID == 90L;
    if (flag)
    {
      List<UnitConversion> scales = new List<UnitConversion>();
      scales.Add(new UnitConversion(1.0, 0.0, "Pulses", "Pulses", "Pulses"));
      UnitConversion unitConversion1 = new UnitConversion();
      UnitConversion unitConversion2 = new UnitConversion();
      try
      {
        string o1 = "1";
        string empty1 = string.Empty;
        string o2 = "1";
        string empty2 = string.Empty;
        foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sens.SensorID))
        {
          if (flag)
          {
            if (sensorAttribute.Name == "transformValue")
              o1 = sensorAttribute.Value;
            if (sensorAttribute.Name == "Label")
              empty1 = sensorAttribute.Value;
          }
          if (sens.ApplicationID == 153L)
          {
            if (sensorAttribute.Name == "Channel1TransformValue")
              o1 = sensorAttribute.Value;
            if (sensorAttribute.Name == "Channel1Label")
              empty1 = sensorAttribute.Value;
            if (sensorAttribute.Name == "Channel2TransformValue")
              o2 = sensorAttribute.Value;
            if (sensorAttribute.Name == "Channel2Label")
              empty2 = sensorAttribute.Value;
          }
        }
        unitConversion1 = new UnitConversion(o1.ToDouble(), 0.0, empty1, "Pulses", "Custom");
        UnitConversion unitConversion3 = new UnitConversion(o2.ToDouble(), 0.0, empty2, "Pulses", "Custom");
        if (sens.ApplicationID == 153L)
          scales.Add(unitConversion3);
      }
      catch
      {
        unitConversion1.Slope = 1.0;
        unitConversion1.Intercept = 0.0;
        unitConversion1.UnitFrom = "Pulses";
        unitConversion1.UnitTo = "Pulses";
        unitConversion1.UnitLabel = "Custom";
      }
      scales.Add(unitConversion1);
      return scales;
    }
    return new List<UnitConversion>()
    {
      new UnitConversion(1.0, 0.0, "", "", "")
    };
  }

  public static string GetNotifyWhenString() => "Notify when count is";
}
