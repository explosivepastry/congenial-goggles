// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.SensorDatumModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.Models;

public class SensorDatumModel
{
  public Sensor Sensor { get; set; }

  public eDatumStruct datumStruct { get; set; }
}
