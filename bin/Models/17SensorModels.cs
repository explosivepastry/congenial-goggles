// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.GatewayTypeShort
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class GatewayTypeShort : BaseDBObject
{
  private long _GatewayTypeID = long.MinValue;
  private string _Name = string.Empty;

  [DBProp("GatewayTypeID")]
  public long GatewayTypeID
  {
    get => this._GatewayTypeID;
    set => this._GatewayTypeID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/)]
  public string Name
  {
    get => this._Name;
    set
    {
      if (value == null)
        this._Name = string.Empty;
      else
        this._Name = value;
    }
  }

  public static List<GatewayTypeShort> LoadAllByAccountID(long accountID)
  {
    return new Data.GatewayTypeShort.LoadAllByAccountID(accountID).Result;
  }
}
