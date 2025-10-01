// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.GatewayInformation
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System.ComponentModel;

#nullable disable
namespace iMonnit.Models;

public class GatewayInformation
{
  public Gateway Gateway { get; set; }

  public Account Account { get; set; }

  public CSNet Network { get; set; }

  [DisplayName("GatewayID")]
  public long GatewayID { get; set; }

  [DisplayName("Name")]
  public string Name { get; set; }

  public GatewayInformation(long id)
  {
    this.GatewayID = id;
    this.Name = "Not Found";
  }

  public GatewayInformation(Gateway gateway)
  {
    this.Gateway = gateway;
    this.GatewayID = this.Gateway.GatewayID;
    this.Name = this.Gateway.Name;
    if (gateway.CSNetID <= 0L)
      return;
    this.Network = CSNet.Load(this.Gateway.CSNetID);
    this.Account = Account.Load(this.Network.AccountID);
  }
}
