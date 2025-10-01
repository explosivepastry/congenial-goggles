// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIGatewayNetworkAcccount
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System.Net;

#nullable disable
namespace iMonnit.API;

public class APIGatewayNetworkAcccount
{
  public APIGatewayNetworkAcccount()
  {
  }

  public APIGatewayNetworkAcccount(Gateway gateway, CSNet network, Account account)
  {
    this.GatewayName = WebUtility.HtmlDecode(gateway.Name);
    this.AccountName = WebUtility.HtmlDecode(account.AccountNumber);
    this.NetworkName = WebUtility.HtmlDecode(network.Name);
  }

  public string GatewayName { get; set; }

  public string AccountName { get; set; }

  public string NetworkName { get; set; }
}
