// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.GatewayTestingMessage
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;

#nullable disable
namespace iMonnit.Models;

public class GatewayTestingMessage
{
  public Guid Guid { get; set; }

  public string IconString { get; set; }

  public DateTime MessageDate { get; set; }

  public long DeviceID { get; set; }

  public string Content { get; set; }
}
