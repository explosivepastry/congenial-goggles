// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APINameID
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

#nullable disable
namespace iMonnit.API;

public class APINameID
{
  public APINameID()
  {
  }

  public APINameID(long id, string name)
  {
    this.ID = id;
    this.Name = name;
  }

  public long ID { get; set; }

  public string Name { get; set; }
}
