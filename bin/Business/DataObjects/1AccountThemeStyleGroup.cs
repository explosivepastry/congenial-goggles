// Decompiled with JetBrains decompiler
// Type: Monnit.Data.AccountThemeStyleGroup
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class AccountThemeStyleGroup
{
  [DBMethod("AccountThemeStyleGroup_LoadByAccountThemeID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @Active BIT = 1\r\n\r\nIF @IncludeInactive = 1\r\nSET @Active = NULL\r\n\r\nSELECT\r\n  *\r\nFROM dbo.[AccountThemeStyleGroup]\r\nWHERE AccountThemeID = @AccountThemeID\r\n  AND IsActive = ISNULL(@Active, IsActive);\r\n")]
  internal class LoadByAccountThemeID : BaseDBMethod
  {
    [DBMethodParam("AccountThemeID", typeof (long))]
    public long AccountThemeID { get; private set; }

    [DBMethodParam("IncludeInactive", typeof (bool))]
    public bool IncludeInactive { get; private set; }

    public List<Monnit.AccountThemeStyleGroup> Result { get; private set; }

    public LoadByAccountThemeID(long accountThemeID, bool includeInactive)
    {
      this.AccountThemeID = accountThemeID;
      this.IncludeInactive = includeInactive;
      this.Result = BaseDBObject.Load<Monnit.AccountThemeStyleGroup>(this.ToDataTable());
    }
  }
}
