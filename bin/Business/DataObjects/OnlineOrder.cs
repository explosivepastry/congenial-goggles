// Decompiled with JetBrains decompiler
// Type: Monnit.OnlineOrder
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("OnlineOrder")]
public class OnlineOrder : BaseDBObject
{
  private long _OrderID = long.MinValue;
  private DateTime _OrderDate = DateTime.UtcNow;
  private double _OrderTotal = 0.0;
  private long _AccountID = long.MinValue;
  private bool _Active = false;
  private eOrderStatus _Status = eOrderStatus.Cart;
  private DateTime _LastUpdated = DateTime.UtcNow;
  private List<OrderItem> _Items;

  [DBProp("OrderID", IsPrimaryKey = true)]
  public long OrderID
  {
    get => this._OrderID;
    set => this._OrderID = value;
  }

  [DBProp("OrderDate")]
  public DateTime OrderDate
  {
    get => this._OrderDate;
    set => this._OrderDate = value;
  }

  [DBProp("OrderTotal")]
  public double OrderTotal
  {
    get => this._OrderTotal;
    set => this._OrderTotal = value;
  }

  [DBForeignKey("Account", "AccountID")]
  [DBProp("AccountID", AllowNull = false)]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("Active")]
  public bool Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  [DBProp("Status")]
  public eOrderStatus Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [DBProp("LastUpdated")]
  public DateTime LastUpdated
  {
    get => this._LastUpdated;
    set => this._LastUpdated = value;
  }

  public List<OrderItem> Items
  {
    get
    {
      if (this._Items == null)
        this._Items = OrderItem.LoadByOrderID(this.OrderID);
      return this._Items;
    }
  }

  public void RefreshItems()
  {
    this._Items = (List<OrderItem>) null;
    this.OrderTotal = Math.Round(this.Items.Sum<OrderItem>((Func<OrderItem, double>) (oi => oi.ItemPrice * (double) oi.ItemQty)), 2);
    this.Save();
  }

  public static void RefreshItems(long ID) => OnlineOrder.Load(ID).RefreshItems();

  public static OnlineOrder Load(long ID) => BaseDBObject.Load<OnlineOrder>(ID);

  public override void Save()
  {
    this.LastUpdated = DateTime.UtcNow;
    base.Save();
  }

  public static List<OnlineOrder> LoadByAccount(long accountID)
  {
    return BaseDBObject.LoadByForeignKey<OnlineOrder>("AccountID", (object) accountID);
  }

  public static OnlineOrder LoadCartByAccount(long accountID)
  {
    List<OnlineOrder> onlineOrderList = OnlineOrder.LoadByAccount(accountID);
    foreach (OnlineOrder onlineOrder in onlineOrderList)
    {
      if (onlineOrder.Status == eOrderStatus.Cart)
        return onlineOrder;
    }
    foreach (OnlineOrder onlineOrder in onlineOrderList)
    {
      if (onlineOrder.Status == eOrderStatus.Pending)
        return onlineOrder;
    }
    OnlineOrder onlineOrder1 = new OnlineOrder();
    onlineOrder1.AccountID = accountID;
    onlineOrder1.Active = true;
    onlineOrder1.Status = eOrderStatus.Cart;
    onlineOrder1.OrderTotal = 0.0;
    onlineOrder1.Save();
    return onlineOrder1;
  }
}
