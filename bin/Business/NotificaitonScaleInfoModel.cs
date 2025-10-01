// Decompiled with JetBrains decompiler
// Type: Monnit.UnitConversion
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

#nullable disable
namespace Monnit;

public class UnitConversion
{
  public double Slope { get; set; }

  public double Intercept { get; set; }

  public string UnitFrom { get; set; }

  public string UnitTo { get; set; }

  public string UnitLabel { get; set; }

  public UnitConversion(
    double slope,
    double intercept,
    string fromUnits,
    string toUnits,
    string unitLabel)
  {
    this.Slope = slope;
    this.Intercept = intercept;
    this.UnitFrom = fromUnits;
    this.UnitTo = toUnits;
    this.UnitLabel = unitLabel;
  }

  public UnitConversion()
  {
  }
}
