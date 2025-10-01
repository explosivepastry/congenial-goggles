// Decompiled with JetBrains decompiler
// Type: Monnit.Data.Product
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class Product
{
  [DBMethod("Product_LoadPremier")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  * \r\nFROM dbo.[Product]\r\nWHERE IsActive = 1\r\n  AND PremierMaxSensorCount > 0;\r\n")]
  internal class LoadPremiere : BaseDBMethod
  {
    public List<Monnit.Product> Result { get; private set; }

    public LoadPremiere() => this.Result = BaseDBObject.Load<Monnit.Product>(this.ToDataTable());
  }

  [DBMethod("Product_LoadNotificationCredits")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[Product]\r\nWHERE IsActive            = 1\r\n  AND NotificationCredits > 0;\r\n")]
  internal class LoadNotificationCredits : BaseDBMethod
  {
    public List<Monnit.Product> Result { get; private set; }

    public LoadNotificationCredits()
    {
      this.Result = BaseDBObject.Load<Monnit.Product>(this.ToDataTable());
    }
  }
}
