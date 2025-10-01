// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.InboundPacketModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class InboundPacketModel
{
  public InboundPacketModel()
  {
    this.ToDate = DateTime.UtcNow.AddHours(1.0);
    this.FromDate = this.ToDate.AddDays(-1.0);
    this.Count = 10;
    this.Response = int.MinValue;
    this.GatewayID = new long?(long.MinValue);
    this.SensorID = new long?(long.MinValue);
    this.TimeOffset = 0.0;
    this.InboundPackets = (List<InboundPacket>) null;
  }

  public DateTime FromDate { get; set; }

  public DateTime ToDate { get; set; }

  public DateTime FromDateOffset => this.FromDate.AddHours(this.TimeOffset);

  public DateTime ToDateOffset => this.ToDate.AddHours(this.TimeOffset);

  public int Count { get; set; }

  public int Response { get; set; }

  public long? GatewayID { get; set; }

  public long? SensorID { get; set; }

  public double TimeOffset { get; set; }

  public List<InboundPacket> InboundPackets { get; set; }
}
