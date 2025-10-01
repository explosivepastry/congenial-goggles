// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APINetwork
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;

#nullable disable
namespace iMonnit.API;

public class APINetwork
{
  public APINetwork()
  {
  }

  public APINetwork(CSNet c)
  {
    this.NetworkID = c.CSNetID;
    this.NetworkName = c.Name;
    this.SendNotifications = c.SendNotifications;
    this.ExternalAccessUntil = c.ExternalAccessUntil;
  }

  public long NetworkID { get; set; }

  public string NetworkName { get; set; }

  public bool SendNotifications { get; set; }

  public DateTime ExternalAccessUntil { get; set; }
}
