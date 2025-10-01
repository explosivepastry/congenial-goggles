// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.Pascal
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

public class Pascal : DataTypeBase
{
  public double value;

  public override eDatumType datatype => eDatumType.Pressure;

  public override object compvalue => (object) this.value;

  public Pascal(double v) => this.value = v;

  public static NotificationScaleInfoModel GetScalingInfo(long i)
  {
    return i <= 0L ? Pascal.getScales()[(int) (-1L * i)] : MonnitApplicationBase.GetScalingInfo(Sensor.Load(i));
  }

  public static List<NotificationScaleInfoModel> getScales()
  {
    return new List<NotificationScaleInfoModel>()
    {
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 1.0, 0.0, nameof (Pascal), nameof (Pascal))
    };
  }

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    return new List<UnitConversion>()
    {
      new UnitConversion(1.0, 0.0, nameof (Pascal), "", nameof (Pascal))
    };
  }
}
