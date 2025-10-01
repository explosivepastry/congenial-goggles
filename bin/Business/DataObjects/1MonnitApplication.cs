// Decompiled with JetBrains decompiler
// Type: Monnit.Data.MonnitApplication
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class MonnitApplication
{
  [DBMethod("Application_LoadByThemeID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  ma.ApplicationID,\r\n  ApplicationName           = ISNULL(l.Alias, ma.ApplicationName),\r\n  ma.IsTriggerProfile,\r\n  ma.HasDefaultRanges\r\nFROM dbo.[Application] ma\r\nINNER JOIN dbo.[AccountThemeApplicationLink] l on l.ApplicationID = ma.ApplicationID\r\nWHERE l.AccountThemeID = @AccountThemeID\r\nORDER BY\r\n  ISNULL(l.Alias, ma.ApplicationName);\r\n")]
  internal class LoadByThemeID : BaseDBMethod
  {
    [DBMethodParam("AccountThemeID", typeof (long))]
    public long AccountThemeID { get; private set; }

    public List<Monnit.MonnitApplication> Result { get; private set; }

    public LoadByThemeID(long accountThemeID)
    {
      this.AccountThemeID = accountThemeID;
      this.Result = BaseDBObject.Load<Monnit.MonnitApplication>(this.ToDataTable());
    }
  }
}
