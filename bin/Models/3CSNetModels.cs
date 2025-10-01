// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.xmlDeviceAddModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class xmlDeviceAddModel
{
  private List<CSNet> _Networks = (List<CSNet>) null;

  public long AccountID { get; set; }

  public List<CSNet> Networks
  {
    get
    {
      if (this._Networks == null || this._Networks.Count == 0)
        this._Networks = CSNet.LoadByAccountID(this.AccountID);
      return this._Networks;
    }
  }

  public long NetworkID { get; set; }
}
