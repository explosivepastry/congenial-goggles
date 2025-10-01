// Decompiled with JetBrains decompiler
// Type: Monnit.Data.AuthenticateDevice
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class AuthenticateDevice
{
  [DBMethod("AuthenticateDevice_LoadByCustomerID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE dbo.[AuthenticateDevice] \r\nWHERE LastLoginDate <= DATEADD(DAY, -90, GETUTCDATE())\r\n\r\nSELECT\r\n  *\r\nFROM dbo.[AuthenticateDevice] WITH (NOLOCK)\r\nWHERE CustomerID = @CustomerID;\r\n\t")]
  internal class LoadByCustomerID : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long KeyValue { get; private set; }

    public List<Monnit.AuthenticateDevice> Result { get; private set; }

    public LoadByCustomerID(long customerID)
    {
      this.KeyValue = customerID;
      this.Result = BaseDBObject.Load<Monnit.AuthenticateDevice>(this.ToDataTable());
    }
  }
}
