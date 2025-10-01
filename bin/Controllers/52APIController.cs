// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APISensorGroup
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APISensorGroup
{
  public APISensorGroup()
  {
  }

  public APISensorGroup(SensorGroup group)
  {
    this.Name = group.Name;
    this.SensorGroupID = group.SensorGroupID;
    this.ParentID = group.ParentID;
  }

  public string Name { get; set; }

  public long SensorGroupID { get; set; }

  public long ParentID { get; set; }
}
