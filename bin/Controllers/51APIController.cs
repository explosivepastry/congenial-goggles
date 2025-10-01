// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APISensorAttribute
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APISensorAttribute
{
  public APISensorAttribute()
  {
  }

  public APISensorAttribute(SensorAttribute att)
  {
    this.SensorID = att.SensorID;
    this.Name = att.Name;
    this.Value = att.Value;
  }

  public long SensorID { get; set; }

  public string Name { get; set; }

  public string Value { get; set; }
}
