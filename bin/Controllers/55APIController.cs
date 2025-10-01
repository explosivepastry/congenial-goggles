// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIDatumType
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APIDatumType
{
  public APIDatumType()
  {
  }

  public APIDatumType(AppDatum datum, int index)
  {
    this.DatumType = datum.etype.ToString();
    this.Name = datum.type;
    this.Index = index;
  }

  public string DatumType { get; set; }

  public string Name { get; set; }

  public int Index { get; set; }
}
