// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.TemperatureData
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

public class TemperatureData : DataTypeBase
{
  public double value;

  public override eDatumType datatype => eDatumType.TemperatureData;

  public override object compvalue => (object) this.value;

  public TemperatureData(double temp) => this.value = temp;

  public TemperatureData(double temp, eTemperatureType eTemp)
  {
    switch (eTemp)
    {
      case eTemperatureType.Fahrenheit:
        this.value = TemperatureData.FahrenheitToCelsius(temp);
        break;
      case eTemperatureType.Celsius:
        this.value = temp;
        break;
      case eTemperatureType.Kelvin:
        this.value = TemperatureData.KelvinToCelsius(temp);
        break;
    }
  }

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    return new List<UnitConversion>()
    {
      new UnitConversion(1.0, 0.0, "C", "C", "C"),
      new UnitConversion(0.555555, -17.78, "F", "C", "F")
    };
  }

  public double asFahrenheit() => TemperatureData.CelsiusToFahrenheit(this.value);

  public double asCelsius() => this.value;

  public double asKelvin() => TemperatureData.CelsiusToKelvin(this.value);

  public static double FahrenheitToKelvin(double f)
  {
    f = TemperatureData.FahrenheitToCelsius(f);
    return TemperatureData.CelsiusToKelvin(f);
  }

  public static double CelsiusToKelvin(double c) => c + 273.15;

  public static double KelvinToCelsius(double k) => k - 273.15;

  public static double FahrenheitToCelsius(double f) => (f - 32.0) * (5.0 / 9.0);

  public static double CelsiusToFahrenheit(double c) => c * 1.8 + 32.0;
}
