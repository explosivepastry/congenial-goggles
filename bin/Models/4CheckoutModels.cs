// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.ProductInfoModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

#nullable disable
namespace iMonnit.Models;

public class ProductInfoModel
{
  [System.ComponentModel.DisplayName("Name")]
  public string Name { get; set; }

  [System.ComponentModel.DisplayName("Display Name")]
  public string DisplayName { get; set; }

  [System.ComponentModel.DisplayName("Description")]
  public string Description { get; set; }

  [System.ComponentModel.DisplayName("SKU")]
  public string SKU { get; set; }

  [System.ComponentModel.DisplayName("Price")]
  public double Price { get; set; }

  [System.ComponentModel.DisplayName("Discount")]
  public double Discount { get; set; }

  [System.ComponentModel.DisplayName("Tax")]
  public double Tax { get; set; }

  [System.ComponentModel.DisplayName("Product Type")]
  public string ProductType { get; set; }
}
