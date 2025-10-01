// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIGatewayMessage
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;

#nullable disable
namespace iMonnit.API;

public class APIGatewayMessage
{
  public APIGatewayMessage()
  {
  }

  public APIGatewayMessage(Gateway g, GatewayMessage gm)
  {
    this.GatewayID = g.GatewayID;
    this.ReceivedDate = DateTime.SpecifyKind(gm.ReceivedDate, DateTimeKind.Utc);
    this.MessageType = gm.MessageType;
    this.MessageCount = gm.MessageCount;
    this.SignalStrength = GatewayMessage.GetSignalStrengthPercent(g.GatewayTypeID, gm.SignalStrength);
    this.Power = (double) gm.Power;
    this.Battery = gm.Battery;
  }

  public DateTime ReceivedDate { get; set; }

  public int MessageCount { get; set; }

  public int SignalStrength { get; set; }

  public double Power { get; set; }

  public int Battery { get; set; }

  public int MessageType { get; set; }

  public long GatewayID { get; set; }
}
