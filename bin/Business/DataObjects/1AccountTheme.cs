// Decompiled with JetBrains decompiler
// Type: Monnit.Data.AccountTheme
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class AccountTheme
{
  [DBMethod("AccountTheme_Find")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[AccountTheme]\r\nWHERE Domain = @Domain;\r\n")]
  internal class Find : BaseDBMethod
  {
    [DBMethodParam("Domain", typeof (string))]
    public string Domain { get; private set; }

    public Monnit.AccountTheme Result { get; private set; }

    public Find(string domain)
    {
      this.Domain = domain;
      this.Result = (Monnit.AccountTheme) null;
      DataTable dataTable = this.ToDataTable();
      if (dataTable.Rows.Count <= 0)
        return;
      this.Result = new Monnit.AccountTheme();
      this.Result.Load(dataTable.Rows[0]);
    }
  }
}
