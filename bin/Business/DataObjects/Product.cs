// Decompiled with JetBrains decompiler
// Type: Monnit.Product
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("Product")]
public class Product : BaseDBObject
{
  private long _ProductID = long.MinValue;
  private string _SKU = string.Empty;
  private string _Name = string.Empty;
  private string _Description = string.Empty;
  private string _Thumbnail = string.Empty;
  private bool _RequiresShipping = false;
  private double _AproxCost = double.MinValue;
  private double _Price = double.MinValue;
  private bool _IsActive = false;
  private int _PremierMaxSensorCount = int.MinValue;
  private bool _ExpressDownload = false;
  private int _NotificationCredits = int.MinValue;

  [DBProp("ProductID", IsPrimaryKey = true)]
  public long ProductID
  {
    get => this._ProductID;
    set => this._ProductID = value;
  }

  [DBProp("SKU", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string SKU
  {
    get => this._SKU;
    set => this._SKU = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Description", MaxLength = 2000, AllowNull = true)]
  public string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [DBProp("Thumbnail", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Thumbnail
  {
    get => this._Thumbnail;
    set => this._Thumbnail = value;
  }

  [DBProp("RequiresShipping")]
  public bool RequiresShipping
  {
    get => this._RequiresShipping;
    set => this._RequiresShipping = value;
  }

  [DBProp("AproxCost")]
  public double AproxCost
  {
    get => this._AproxCost;
    set => this._AproxCost = value;
  }

  [DBProp("Price")]
  public double Price
  {
    get => this._Price;
    set => this._Price = value;
  }

  [DBProp("IsActive")]
  public bool IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [DBProp("PremierMaxSensorCount")]
  public int PremierMaxSensorCount
  {
    get => this._PremierMaxSensorCount;
    set => this._PremierMaxSensorCount = value;
  }

  [DBProp("ExpressDownload")]
  public bool ExpressDownload
  {
    get => this._ExpressDownload;
    set => this._ExpressDownload = value;
  }

  [DBProp("NotificationCredits")]
  public int NotificationCredits
  {
    get => this._NotificationCredits;
    set => this._NotificationCredits = value;
  }

  public static Product Load(long id) => BaseDBObject.Load<Product>(id);

  public static List<Product> LoadPremiere() => new Monnit.Data.Product.LoadPremiere().Result;

  public static List<Product> LoadNotificationCredits()
  {
    return new Monnit.Data.Product.LoadNotificationCredits().Result;
  }
}
