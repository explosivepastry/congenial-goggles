// Decompiled with JetBrains decompiler
// Type: Monnit.OrderItem
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("OrderItem")]
public class OrderItem : BaseDBObject
{
  private long _OrderItemID = long.MinValue;
  private long _OrderID = long.MinValue;
  private string _ItemName = string.Empty;
  private string _ItemDescription = string.Empty;
  private double _ItemPrice = double.MinValue;
  private int _ItemQty = int.MinValue;
  private double _ItemTotalAdjustment = double.MinValue;
  private string _AdjustmentNotes = string.Empty;
  private long _ProductID = long.MinValue;
  private Product _Product;

  [DBProp("OrderItemID", IsPrimaryKey = true)]
  public long OrderItemID
  {
    get => this._OrderItemID;
    set => this._OrderItemID = value;
  }

  [DBForeignKey("OnlineOrder", "OrderID")]
  [DBProp("OrderID", AllowNull = false)]
  public long OrderID
  {
    get => this._OrderID;
    set => this._OrderID = value;
  }

  [DBProp("ItemName", MaxLength = 255 /*0xFF*/)]
  public string ItemName
  {
    get => this._ItemName;
    set
    {
      if (value == null)
        this._ItemName = string.Empty;
      else
        this._ItemName = value;
    }
  }

  [DBProp("ItemDescription", MaxLength = 255 /*0xFF*/)]
  public string ItemDescription
  {
    get => this._ItemDescription;
    set
    {
      if (value == null)
        this._ItemDescription = string.Empty;
      else
        this._ItemDescription = value;
    }
  }

  [DBProp("ItemPrice")]
  public double ItemPrice
  {
    get => this._ItemPrice;
    set => this._ItemPrice = value;
  }

  [DBProp("ItemQty")]
  public int ItemQty
  {
    get => this._ItemQty;
    set => this._ItemQty = value;
  }

  [DBProp("ItemTotalAdjustment")]
  public double ItemTotalAdjustment
  {
    get => this._ItemTotalAdjustment;
    set => this._ItemTotalAdjustment = value;
  }

  [DBProp("AdjustmentNotes", MaxLength = 2000)]
  public string AdjustmentNotes
  {
    get => this._AdjustmentNotes;
    set
    {
      if (value == null)
        this._AdjustmentNotes = string.Empty;
      else
        this._AdjustmentNotes = value;
    }
  }

  [DBProp("ProductID")]
  [DBForeignKey("Product", "ProductID")]
  public long ProductID
  {
    get => this._ProductID;
    set => this._ProductID = value;
  }

  public Product Product
  {
    get
    {
      if (this._Product == null)
        this._Product = Product.Load(this.ProductID);
      return this._Product;
    }
  }

  public static OrderItem Load(long ID) => BaseDBObject.Load<OrderItem>(ID);

  internal static List<OrderItem> LoadByOrderID(long orderID)
  {
    return BaseDBObject.LoadByForeignKey<OrderItem>("OrderID", (object) orderID);
  }
}
