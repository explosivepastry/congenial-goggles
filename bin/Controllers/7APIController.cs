// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIGatewayServer
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;

#nullable disable
namespace iMonnit.API;

public class APIGatewayServer
{
  public APIGatewayServer()
  {
  }

  public APIGatewayServer(Gateway g)
  {
    this.GatewayID = g.GatewayID;
    this.GatewayType = g.GatewayType.Name;
    this.Heartbeat = g.ReportInterval;
    this.ServerHost = g.ServerHostAddress;
    this.ServerPort = g.Port;
    this.ServerHost2 = g.ServerHostAddress2;
    this.ServerPort2 = g.Port2;
    this.PollInterval = g.PollInterval;
    this.IsEnterpriseHost = g.isEnterpriseHost;
    this.LastCommunicationDate = DateTime.SpecifyKind(g.LastCommunicationDate, DateTimeKind.Utc);
    this.IsUnlocked = g.IsUnlocked;
  }

  public long GatewayID { get; set; }

  public string GatewayType { get; set; }

  public double Heartbeat { get; set; }

  public double PollInterval { get; set; }

  public string ServerHost { get; set; }

  public int ServerPort { get; set; }

  public string ServerHost2 { get; set; }

  public int ServerPort2 { get; set; }

  public bool IsEnterpriseHost { get; set; }

  public DateTime LastCommunicationDate { get; set; }

  public bool IsUnlocked { get; set; }
}
