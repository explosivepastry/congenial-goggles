// Decompiled with JetBrains decompiler
// Type: Monnit.Data.PreferenceType
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class PreferenceType
{
  [DBMethod("PreferenceType_LoadUserAllowedByAccountThemeID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  p.*\r\nFROM dbo.[PreferenceType] p WITH (NOLOCK)\r\nINNER JOIN dbo.[AccountThemePreferenceTypeLink] al WITH (NOLOCK) ON p.PreferenceTypeID = al.PreferenceTypeID\r\nWHERE al.CustomerCanOverride  = 1\r\n  AND al.AccountThemeID       = @AccountThemeID;\r\n")]
  internal class LoadUserAllowedByAccountThemeID : BaseDBMethod
  {
    [DBMethodParam("AccountThemeID", typeof (long))]
    public long AccountThemeID { get; private set; }

    public List<Monnit.PreferenceType> Result { get; private set; }

    public LoadUserAllowedByAccountThemeID(long accountThemeID)
    {
      this.AccountThemeID = accountThemeID;
      this.Result = BaseDBObject.Load<Monnit.PreferenceType>(this.ToDataTable());
    }
  }

  [DBMethod("PreferenceType_LoadAccountAllowedByAccountThemeID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  p.*\r\nFROM dbo.[PreferenceType] p WITH (NOLOCK)\r\nINNER JOIN dbo.[AccountThemePreferenceTypeLink] al WITH (NOLOCK) ON p.PreferenceTypeID = al.PreferenceTypeID\r\nWHERE al.AccountCanOverride = 1\r\n  AND al.AccountThemeID     = @AccountThemeID;\r\n")]
  internal class LoadAccountAllowedByAccountThemeID : BaseDBMethod
  {
    [DBMethodParam("AccountThemeID", typeof (long))]
    public long AccountThemeID { get; private set; }

    public List<Monnit.PreferenceType> Result { get; private set; }

    public LoadAccountAllowedByAccountThemeID(long accountThemeID)
    {
      this.AccountThemeID = accountThemeID;
      this.Result = BaseDBObject.Load<Monnit.PreferenceType>(this.ToDataTable());
    }
  }
}
