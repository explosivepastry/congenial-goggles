// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.ProbeStatus
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

internal class ProbeStatus : DataTypeBase
{
  public string value;

  public override eDatumType datatype => eDatumType.ProbeStatus;

  public override object compvalue => (object) this.value;

  public ProbeStatus(string v) => this.value = v;

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    return new List<UnitConversion>()
    {
      new UnitConversion(1.0, 0.0, "", "", "")
    };
  }

  public static string GetNotifyWhenString() => "Notify when observation mode";

  public static List<DropdownItemForRules> GetRuleDropDownValues()
  {
    return new List<DropdownItemForRules>()
    {
      new DropdownItemForRules()
      {
        DisplayString = "Begins",
        Value = "start"
      },
      new DropdownItemForRules()
      {
        DisplayString = "Ends",
        Value = "end"
      }
    };
  }
}
