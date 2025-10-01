// Decompiled with JetBrains decompiler
// Type: Monnit.Data.CustomerFavorite
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class CustomerFavorite
{
  [DBMethod("CustomerFavorite_LoadByCustomerIDAndAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\n            SELECT\r\n              *\r\n            FROM dbo.[CustomerFavorite] \r\n\t            WITH (NOLOCK)\r\n            WHERE\tCustomerID = @CustomerID\r\n\t            AND AccountID = @AccountID\r\n            ORDER BY OrderNum;\r\n             ")]
  internal class LoadByCustomerIDAndAccountID : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<Monnit.CustomerFavorite> Result { get; private set; }

    public LoadByCustomerIDAndAccountID(long customerID, long accountID)
    {
      this.CustomerID = customerID;
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.CustomerFavorite>(this.ToDataTable());
    }
  }
}
