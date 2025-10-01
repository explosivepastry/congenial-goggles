// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.NetworkInformation
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System.ComponentModel;

#nullable disable
namespace iMonnit.Models;

public class NetworkInformation
{
  public Account Account { get; set; }

  public CSNet Network { get; set; }

  [DisplayName("CSNetID")]
  public long CSNetID { get; set; }

  [DisplayName("Name")]
  public string Name { get; set; }

  public NetworkInformation(long id)
  {
    this.CSNetID = id;
    this.Name = "Not Found";
  }

  public NetworkInformation(CSNet network)
  {
    this.Network = network;
    this.CSNetID = this.Network.CSNetID;
    this.Name = this.Network.Name;
    this.Account = Account.Load(this.Network.AccountID);
  }
}
