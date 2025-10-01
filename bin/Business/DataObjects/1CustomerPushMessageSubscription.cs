// Decompiled with JetBrains decompiler
// Type: Monnit.Data.CustomerPushMessageSubscription
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class CustomerPushMessageSubscription
{
  [DBMethod("CustomerPushMessageSubscription_LoadByCustomerIDandEndpoint")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[CustomerPushMessageSubscription]\r\nWHERE [CustomerID]  = @CustomerID\r\n  AND [EndpointUrl] = @EndpointUrl;\r\n")]
  internal class LoadByCustomerIDandEndpoint : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("EndpointUrl", typeof (string))]
    public string EndpointUrl { get; private set; }

    public Monnit.CustomerPushMessageSubscription Result { get; private set; }

    public LoadByCustomerIDandEndpoint(long customerID, string endpointUrl)
    {
      this.CustomerID = customerID;
      this.EndpointUrl = endpointUrl;
      this.Result = BaseDBObject.Load<Monnit.CustomerPushMessageSubscription>(this.ToDataTable()).FirstOrDefault<Monnit.CustomerPushMessageSubscription>();
    }
  }

  [DBMethod("CustomerPushMessageSubscription_LoadByGuid")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[CustomerPushMessageSubscription]\r\nWHERE [SubscriptionGuid]  = @Guid;\r\n")]
  internal class LoadByGuid : BaseDBMethod
  {
    [DBMethodParam("Guid", typeof (Guid))]
    public Guid Guid { get; private set; }

    public Monnit.CustomerPushMessageSubscription Result { get; private set; }

    public LoadByGuid(Guid guid)
    {
      this.Guid = guid;
      this.Result = BaseDBObject.Load<Monnit.CustomerPushMessageSubscription>(this.ToDataTable()).FirstOrDefault<Monnit.CustomerPushMessageSubscription>();
    }
  }
}
