// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.DifferentialPressureData
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

public class DifferentialPressureData : DataTypeBase
{
  public double value;

  public override eDatumType datatype => eDatumType.Pressure;

  public override object compvalue => (object) this.value;

  public DifferentialPressureData(double v) => this.value = v;

  public static NotificationScaleInfoModel GetScalingInfo(long i)
  {
    return i <= 0L ? DifferentialPressureData.getScales()[(int) (-1L * i)] : MonnitApplicationBase.GetScalingInfo(Sensor.Load(i));
  }

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    List<UnitConversion> scales = new List<UnitConversion>();
    scales.Add(new UnitConversion(1.0, 0.0, "Pascal", "Pascal", "Pascal"));
    scales.Add(new UnitConversion(133.322, 0.0, "Torr", "Pascal", "Torr"));
    scales.Add(new UnitConversion(6894.76, 0.0, "psi", "Pascal", "psi"));
    scales.Add(new UnitConversion(249.08891, 0.0, "InH2O", "Pascal", "InH2O"));
    scales.Add(new UnitConversion(3386.388, 0.0, "inHg", "Pascal", "inHg"));
    scales.Add(new UnitConversion(133.322, 0.0, "mmHg", "Pascal", "mmHg"));
    scales.Add(new UnitConversion(9.80665, 0.0, "mmwc", "Pascal", "mmwc"));
    UnitConversion unitConversion1 = new UnitConversion();
    if (sens == null)
    {
      UnitConversion unitConversion2 = new UnitConversion(1.0, 0.0, "Pascal", "Pascal", "Custom");
      scales.Add(unitConversion2);
      return scales;
    }
    try
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sens.SensorID))
      {
        if (sensorAttribute.Name == "LowValue")
          empty1 = sensorAttribute.Value;
        if (sensorAttribute.Name == "HighValue")
          empty2 = sensorAttribute.Value;
        if (sensorAttribute.Name == "CustomLabel")
          empty3 = sensorAttribute.Value;
      }
      double to1 = (double) sens.DefaultValue<long>("DefaultMinimumThreshold") / 10.0;
      double to2 = (double) sens.DefaultValue<long>("DefaultMaximumThreshold") / 10.0;
      (double num1, double num2) = ExtentionMethods.LinearInterpolation(empty1.ToDouble(), empty2.ToDouble(), to1, to2);
      if (double.IsInfinity(num1) || double.IsInfinity(num2))
      {
        num1 = 1.0;
        num2 = 0.0;
      }
      unitConversion1 = new UnitConversion(num1, num2, empty3, "Pascal", "Custom");
    }
    catch
    {
      unitConversion1.Slope = 1.0;
      unitConversion1.Intercept = 0.0;
      unitConversion1.UnitFrom = "";
      unitConversion1.UnitTo = "Pascal";
      unitConversion1.UnitLabel = "Custom";
    }
    scales.Add(unitConversion1);
    return scales;
  }

  public static List<NotificationScaleInfoModel> getScales()
  {
    return new List<NotificationScaleInfoModel>()
    {
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 1.0, 0.0, "Pascal", "Pascal"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 9E-06, 0.0, "atm", "atm"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 1E-05, 0.0, "bar", "bar"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 0.001, 0.0, "kPA", "kPA"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 3.0 / 400.0, 0.0, "Torr", "Torr"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 0.00401, 0.0, "inAq", "inAq"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 0.00029, 0.0, "inHg", "inHg"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 3.0 / 400.0, 0.0, "mmHg", "mmHg"),
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 0.10197, 0.0, "mmwc", "mmwc")
    };
  }
}
