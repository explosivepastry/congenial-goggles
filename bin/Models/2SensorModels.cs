// Decompiled with JetBrains decompiler
// Type: Data.GatewayTypeShort
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Data;

internal class GatewayTypeShort : BaseDBObject
{
  [DBMethod("GatewayTypeShort_LoadAllByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\n--Get distinct list of GatewayTypes in the inner join reduces reads/io\r\nSELECT\r\n  gt.GatewayTypeID,\r\n  gt.Name\r\nFROM dbo.[GatewayType] gt WITH (NOLOCK)\r\nINNER JOIN (SELECT DISTINCT\r\n              g.GatewayTypeID\r\n            FROM dbo.[Account] a        WITH (NOLOCK) \r\n            INNER JOIN dbo.[CSNet] c    WITH (NOLOCK) ON a.AccountID = c.AccountID\r\n            INNER JOIN dbo.[Gateway] g  WITH (NOLOCK) ON c.CSNetID   = g.CSNetID\r\n            WHERE a.AccountID = @AccountID\r\n              AND g.IsDeleted != 1) g ON g.GatewayTypeID = gt.GatewayTypeID\r\nORDER BY gt.Name;\r\n")]
  internal class LoadAllByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<iMonnit.Models.GatewayTypeShort> Result { get; private set; }

    public LoadAllByAccountID(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<iMonnit.Models.GatewayTypeShort>(this.ToDataTable());
    }
  }
}
