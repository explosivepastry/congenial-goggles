// Decompiled with JetBrains decompiler
// Type: Data.ApplicationTypeShort
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Data;

internal class ApplicationTypeShort : BaseDBObject
{
  [DBMethod("ApplicationTypeShort_LoadAllByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @_AccountID BIGINT\r\nSET @_AccountID = @AccountID\r\n\r\nSELECT \r\n  m.ApplicationID, \r\n  m.ApplicationName\r\nFROM dbo.[Application] m\r\nINNER JOIN (SELECT DISTINCT\r\n              s.ApplicationID\r\n            FROM dbo.[CSNet] c WITH(NOLOCK)\r\n            INNER JOIN dbo.[Sensor] s WITH (NOLOCK) ON c.CSNetID = s.CSNetID\r\n            WHERE s.AccountID = @AccountID\r\n              AND s.IsDeleted = 0) t ON m.ApplicationID = t.ApplicationID\r\nORDER BY m.ApplicationName;\r\n")]
  internal class LoadAllByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<iMonnit.Models.ApplicationTypeShort> Result { get; private set; }

    public LoadAllByAccountID(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<iMonnit.Models.ApplicationTypeShort>(this.ToDataTable());
    }
  }
}
