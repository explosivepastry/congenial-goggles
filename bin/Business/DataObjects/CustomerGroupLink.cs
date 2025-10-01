// Decompiled with JetBrains decompiler
// Type: Monnit.CustomerGroupLink
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit;

[DBClass("CustomerGroupLink")]
public class CustomerGroupLink : BaseDBObject
{
  private long _CustomerGroupLinkID = long.MinValue;
  private long _CustomerGroupID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private eNotificationType _NotificationType;
  private int _DelayMinutes = int.MinValue;

  [DBProp("CustomerGroupLinkID", IsPrimaryKey = true)]
  public long CustomerGroupLinkID
  {
    get => this._CustomerGroupLinkID;
    set => this._CustomerGroupLinkID = value;
  }

  [DBProp("CustomerGroupID")]
  [DBForeignKey("CustomerGroup", "CustomerGroupID")]
  public long CustomerGroupID
  {
    get => this._CustomerGroupID;
    set => this._CustomerGroupID = value;
  }

  [DBProp("CustomerID")]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("eNotificationType", AllowNull = false)]
  public eNotificationType NotificationType
  {
    get => this._NotificationType;
    set => this._NotificationType = value;
  }

  [DBProp("DelayMinutes")]
  public int DelayMinutes
  {
    get => this._DelayMinutes;
    set => this._DelayMinutes = value;
  }

  public static CustomerGroupLink Load(long id) => BaseDBObject.Load<CustomerGroupLink>(id);

  public static List<CustomerGroupLink> LoadByCustomerIDandGroupID(
    long customerID,
    long customerGroupID)
  {
    return BaseDBObject.LoadByForeignKeys<CustomerGroupLink>(new string[2]
    {
      "CustomerID",
      "CustomerGroupID"
    }, new object[2]
    {
      (object) customerID,
      (object) customerGroupID
    });
  }

  public static List<CustomerGroupLink> LoadByCustomerGroupID(long customerGroupID)
  {
    return new Monnit.Data.CustomerGroupLink.LoadByCustomerGroupID(customerGroupID).Result;
  }

  public static DataTable DeleteByCustomerGroupLinkID(long customerGroupLinkID)
  {
    return new Monnit.Data.CustomerGroupLink.DeleteByCustomerGroupLinkID(customerGroupLinkID).Result;
  }
}
