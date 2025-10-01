// Decompiled with JetBrains decompiler
// Type: Monnit.VersionHelper
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

#nullable disable
namespace Monnit;

public static class VersionHelper
{
  public static bool IsVersion_1_0(Sensor sensor)
  {
    return sensor.SensorID > 1000L && sensor.SensorID < 1082L || sensor.SensorID > 10010L && sensor.SensorID < 10028L || sensor.SensorID > 10118L && sensor.SensorID < 10123L;
  }
}
