// Decompiled with JetBrains decompiler
// Type: Monnit.Data.ZipcodeInfo
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class ZipcodeInfo
{
  [DBMethod("ZipcodeInfo_Find")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT * FROM ZipcodeInfo WHERE Zipcode = @Zipcode;\r\n")]
  internal class Find : BaseDBMethod
  {
    [DBMethodParam("Zipcode", typeof (string))]
    public string Zipcode { get; private set; }

    public Monnit.ZipcodeInfo Result { get; private set; }

    public Find(string zipcode)
    {
      this.Zipcode = zipcode;
      this.Result = new Monnit.ZipcodeInfo();
      DataTable dataTable = this.ToDataTable();
      if (dataTable.Rows.Count <= 0)
        return;
      this.Result.Load(dataTable.Rows[0]);
    }
  }
}
