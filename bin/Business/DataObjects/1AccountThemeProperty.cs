// Decompiled with JetBrains decompiler
// Type: Monnit.Data.AccountThemeProperty
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class AccountThemeProperty
{
  [DBMethod("AccountThemeProperty_LoadByAccountThemeID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  t.Name,\r\n  p.AccountThemePropertyID,\r\n  AccountThemeID,\r\n  t.AccountThemePropertyTypeID,\r\n  Value = ISNULL(p.Value, t.Defaultvalue)\r\nFROM dbo.[AccountThemePropertyType] t WITH (NOLOCK)\r\nLEFT JOIN dbo.[AccountThemeProperty] p WITH (NOLOCK) ON t.AccountThemePropertyTypeID = p.AccountThemePropertyTypeID AND p.AccountThemeID = @AccountThemeID;\r\n")]
  internal class LoadByAccountThemeID : BaseDBMethod
  {
    [DBMethodParam("AccountThemeID", typeof (long))]
    public long AccountThemeID { get; private set; }

    public List<Monnit.AccountThemeProperty> Result { get; private set; }

    public LoadByAccountThemeID(long accountThemeID)
    {
      this.AccountThemeID = accountThemeID;
      this.Result = BaseDBObject.Load<Monnit.AccountThemeProperty>(this.ToDataTable());
    }
  }
}
