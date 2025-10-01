// Decompiled with JetBrains decompiler
// Type: Monnit.Data.ChangeLog
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class ChangeLog
{
  [DBMethod("ChangeLog_LoadRecentPublished")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[ChangeLog] WITH(NOLOCK)\r\nWHERE isPublished = 1\r\n  AND ISNULL(ReleaseDate, '2099-01-01') <= GETUTCDATE()\r\nORDER BY ReleaseDate DESC;")]
  internal class LoadRecentPublished : BaseDBMethod
  {
    public List<Monnit.ChangeLog> Result { get; private set; }

    public LoadRecentPublished() => this.Result = BaseDBObject.Load<Monnit.ChangeLog>(this.ToDataTable());
  }
}
