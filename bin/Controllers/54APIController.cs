// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIDatumName
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APIDatumName
{
  public APIDatumName()
  {
  }

  public APIDatumName(SensorAttribute att)
  {
    this.SensorID = att.SensorID;
    this.Index = att.Name.Split('|')[1];
    this.Name = att.Value;
  }

  public long SensorID { get; set; }

  public string Index { get; set; }

  public string Name { get; set; }
}
