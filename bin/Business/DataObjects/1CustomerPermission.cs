// Decompiled with JetBrains decompiler
// Type: Monnit.Data.CustomerPermission
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class CustomerPermission
{
  [DBMethod("CustomerPermission_LoadAllByCustomerID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[CustomerPermission]\r\nWHERE CustomerID = @CustomerID \r\n  AND CustomerPermissionTypeID IS NOT NULL;\r\n")]
  [DBMethodBody(DBMS.SQLite, "Select * from CustomerPermission where CustomerID = @CustomerID AND CustomerPermissionTypeID IS NOT NULL;")]
  internal class LoadAllByCustomerID : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    public List<Monnit.CustomerPermission> Result { get; private set; }

    public LoadAllByCustomerID(long customerID)
    {
      this.CustomerID = customerID;
      this.Result = BaseDBObject.Load<Monnit.CustomerPermission>(this.ToDataTable());
    }
  }
}
