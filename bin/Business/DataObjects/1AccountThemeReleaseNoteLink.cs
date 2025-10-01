// Decompiled with JetBrains decompiler
// Type: Monnit.Data.AccountThemeReleaseNoteLink
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class AccountThemeReleaseNoteLink
{
  [DBMethod("AccountThemeReleaseNoteLink_LoadByAccountThemeIDAndReleaseNoteID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[AccountThemeReleaseNoteLink]\r\nWHERE AccountThemeID = @AccountThemeID\r\n  AND ReleaseNoteID  = @ReleaseNoteID;\r\n")]
  internal class LoadByAccountThemeIDAndReleaseNoteID : BaseDBMethod
  {
    [DBMethodParam("AccountThemeID", typeof (long))]
    public long AccountThemeID { get; private set; }

    [DBMethodParam("ReleaseNoteID", typeof (long))]
    public long ReleaseNoteID { get; private set; }

    public List<Monnit.AccountThemeReleaseNoteLink> Result { get; private set; }

    public LoadByAccountThemeIDAndReleaseNoteID(long accountthemeID, long releaseNoteID)
    {
      this.ReleaseNoteID = releaseNoteID;
      this.AccountThemeID = accountthemeID;
      this.Result = BaseDBObject.Load<Monnit.AccountThemeReleaseNoteLink>(this.ToDataTable());
    }
  }
}
