// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.CreateLocationAccountModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace iMonnit.Models;

public class CreateLocationAccountModel
{
  [Required]
  [DisplayName("Parent Account")]
  public long ParentAccountID { get; set; }

  [Required]
  [DisplayName("Company Name")]
  public string CompanyName { get; set; }

  [Required]
  [DisplayName("Time Zone")]
  public long TimeZoneID { get; set; }

  [DisplayName("SubscriptionCode")]
  public string SubscriptionCode { get; set; }

  [DisplayName("Subscription SKU")]
  public string SubscriptionSKU { get; set; }

  [DisplayName("PaymentID")]
  public long PaymentID { get; set; }

  [DisplayName("Tax Amount")]
  public Decimal TaxAmount { get; set; }

  public long SalesOrderID { get; set; }

  public long SalesOrderItemID { get; set; }

  public Decimal Total { get; set; }

  public string MaskedCardNumber { get; set; }

  public string CardExpDate { get; set; }

  public string CardOwnerName { get; set; }
}
