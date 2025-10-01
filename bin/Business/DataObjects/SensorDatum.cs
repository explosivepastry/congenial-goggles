// Decompiled with JetBrains decompiler
// Type: Monnit.SensorDatum
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

#nullable disable
namespace Monnit;

public class SensorDatum
{
  public Sensor sens { get; set; }

  public int DatumIndex { get; set; }

  public long SensorNotificationID { get; set; }

  public eDatumType DatumType => this.sens.getDatumType(this.DatumIndex);

  public SensorDatum(Sensor s, int di, long snid)
  {
    this.sens = s;
    this.DatumIndex = di;
    this.SensorNotificationID = snid;
  }
}
