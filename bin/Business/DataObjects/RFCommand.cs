// Decompiled with JetBrains decompiler
// Type: Monnit.RFCommand
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

#nullable disable
namespace Monnit;

public class RFCommand
{
  public long GatewayID { get; set; }

  public long DeviceID { get; set; }

  public int TestMode { get; set; }

  public int Modulation { get; set; }

  public double Frequency { get; set; }

  public string Power { get; set; }

  public string ErrorMsg { get; set; }

  public int TestMode24 { get; set; }

  public int Modulation24 { get; set; }

  public double Frequency24 { get; set; }

  public string Power24 { get; set; }
}
