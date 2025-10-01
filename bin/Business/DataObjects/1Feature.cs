// Decompiled with JetBrains decompiler
// Type: Monnit.Data.Feature
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class Feature
{
  [DBMethod("Feature_LoadBySubscriptionTypeID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  f.*\r\nFROM dbo.[Feature] f\r\nINNER JOIN dbo.[SubscriptionTypeFeatureLink] l ON f.FeatureID = l.FeatureID\r\nWHERE l.AccountSubscriptionTypeID = @SubscriptionTypeID\r\nORDER BY\r\n  FeatureID;\r\n")]
  internal class LoadBySubscriptionTypeID : BaseDBMethod
  {
    [DBMethodParam("SubscriptionTypeID", typeof (long))]
    public long SubscriptionTypeID { get; private set; }

    public List<Monnit.Feature> Result { get; private set; }

    public LoadBySubscriptionTypeID(long subscriptionTypeID)
    {
      this.SubscriptionTypeID = subscriptionTypeID;
      this.Result = BaseDBObject.Load<Monnit.Feature>(this.ToDataTable());
    }
  }
}
