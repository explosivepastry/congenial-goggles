// Decompiled with JetBrains decompiler
// Type: Monnit.Data.GatewayLocation
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class GatewayLocation
{
  [DBMethod("GatewayLocation_LoadByGatewayID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP 10\r\n    *\r\nFROM dbo.[GatewayLocation] g WITH (NOLOCK)\r\nWHERE g.GatewayID = @GatewayID\r\nORDER BY\r\n  Date DESC;\r\n")]
  internal class LoadByGatewayID : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    public List<Monnit.GatewayLocation> Result { get; private set; }

    public LoadByGatewayID(long gatewayID)
    {
      this.GatewayID = gatewayID;
      this.Result = BaseDBObject.Load<Monnit.GatewayLocation>(this.ToDataTable());
    }
  }
}
