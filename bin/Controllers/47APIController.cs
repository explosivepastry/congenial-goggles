// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APITimeZone
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APITimeZone
{
  public APITimeZone(TimeZone t)
  {
    this.ID = t.TimeZoneID;
    this.StandardName = t.TimeZoneIDString;
    this.DisplayName = t.DisplayName;
    this.Region = t.Region;
  }

  public long ID { get; set; }

  public string StandardName { get; set; }

  public string DisplayName { get; set; }

  public string Region { get; set; }
}
