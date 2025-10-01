// Decompiled with JetBrains decompiler
// Type: Monnit.Data.AccountAddress
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class AccountAddress
{
  [DBMethod("AccountAddress_LoadFirstByType")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[AccountAddress]\r\nWHERE AccountID           = @AccountID\r\n  AND eAccountAddressType = @AccountAddressType;\r\n")]
  internal class LoadFirstByType : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("AccountAddressType", typeof (int))]
    public int AccountAddressType { get; private set; }

    public Monnit.AccountAddress Result { get; private set; }

    public LoadFirstByType(long accountID, eAccountAddressType accountAddressType)
    {
      this.AccountID = accountID;
      this.AccountAddressType = accountAddressType.ToInt();
      this.Result = new Monnit.AccountAddress();
      this.Result.AccountID = accountID;
      this.Result.AccountAddressType = accountAddressType;
      DataTable dataTable = this.ToDataTable();
      if (dataTable.Rows.Count <= 0)
        return;
      this.Result.Load(dataTable.Rows[0]);
    }
  }
}
