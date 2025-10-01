// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIPermission
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

#nullable disable
namespace iMonnit.API;

public class APIPermission
{
  public APIPermission()
  {
  }

  public APIPermission(string name, bool can, long networkID)
  {
    this.Name = name;
    if (networkID > 0L)
      this.Name = $"{name}_{networkID.ToString()}";
    this.Can = can;
  }

  public string Name { get; set; }

  public bool Can { get; set; }
}
