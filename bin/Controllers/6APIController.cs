// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIGateway
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using RedefineImpossible;
using System;

#nullable disable
namespace iMonnit.API;

public class APIGateway
{
  public APIGateway()
  {
  }

  public APIGateway(Gateway g)
  {
    this.GatewayID = g.GatewayID;
    this.NetworkID = g.CSNetID;
    this.Name = g.Name;
    this.GatewayType = g.GatewayType.Name;
    this.MacAddress = g.MacAddress;
    this.Heartbeat = g.ReportInterval;
    this.IsDirty = g.IsDirty;
    this.LastCommunicationDate = DateTime.SpecifyKind(g.LastCommunicationDate, DateTimeKind.Utc);
    this.LastInboundIPAddress = g.LastInboundIPAddress;
    this.IsUnlocked = g.IsUnlocked;
    this.SignalStrength = g.CurrentSignalStrength;
    this.BatteryLevel = 101;
    this.ResetInterval = g.ResetInterval;
    this.GatewayPowerMode = g.GatewayPowerMode.ToString();
    if (g.CurrentPower > 1 && g.PowerSource != null)
    {
      int o = (int) Convert.ToUInt16(g.CurrentPower) & (int) short.MaxValue;
      this.BatteryLevel = g.PowerSource.Percent(o.ToDouble() / 1000.0).ToInt();
    }
    CSNet csNet = CSNet.Load(g.CSNetID);
    if (csNet == null)
      return;
    Account account = Account.Load(csNet.AccountID);
    if (account != null)
      this.AccountID = account.AccountID;
  }

  public long GatewayID { get; set; }

  public long NetworkID { get; set; }

  public string Name { get; set; }

  public string GatewayType { get; set; }

  public double Heartbeat { get; set; }

  public bool IsDirty { get; set; }

  public DateTime LastCommunicationDate { get; set; }

  public string LastInboundIPAddress { get; set; }

  public string MacAddress { get; set; }

  public bool IsUnlocked { get; set; }

  public string CheckDigit => MonnitUtil.CheckDigit(this.GatewayID);

  public long AccountID { get; set; }

  public int SignalStrength { get; set; }

  public int BatteryLevel { get; set; }

  public int ResetInterval { get; set; }

  public string GatewayPowerMode { get; set; }
}
