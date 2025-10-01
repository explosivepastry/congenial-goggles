// Decompiled with JetBrains decompiler
// Type: Monnit.NotificationScaleInfoModel
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

#nullable disable
namespace Monnit;

public class NotificationScaleInfoModel
{
  public Sensor sensor { get; set; }

  public double profileLow { get; set; }

  public double profileHigh { get; set; }

  public double baseLow { get; set; }

  public double baseHigh { get; set; }

  public double transformValue { get; set; }

  public string Label { get; set; }

  public string CustomLabel { get; set; }

  public NotificationScaleInfoModel(
    Sensor s,
    double pl,
    double ph,
    double bl,
    double bh,
    double tv,
    string l,
    string cl)
  {
    this.sensor = s;
    this.profileLow = pl;
    this.profileHigh = ph;
    this.baseLow = bl;
    this.baseHigh = bh;
    this.transformValue = tv;
    this.Label = l;
    this.CustomLabel = cl;
  }

  public NotificationScaleInfoModel()
  {
    this.profileHigh = 1.0;
    this.baseHigh = 1.0;
  }
}
