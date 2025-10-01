// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.ActivityDetect
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

internal class ActivityDetect(bool b) : BooleanData(b)
{
  public override eDatumType datatype => eDatumType.ActivityDetect;

  public new static List<UnitConversion> GetScales(Sensor sens) => BooleanData.GetScales(sens);

  public static string GetNotifyWhenString() => "Notify when activity is";
}
