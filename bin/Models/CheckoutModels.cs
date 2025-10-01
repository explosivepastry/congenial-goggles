// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.PaymentInfoModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System.ComponentModel.DataAnnotations;

#nullable disable
namespace iMonnit.Models;

public class PaymentInfoModel
{
  [Required]
  public string CustomerName { get; set; }

  [Required]
  public string CardNumber { get; set; }

  [Required]
  public int ExpMonth { get; set; }

  [Required]
  public int ExpYear { get; set; }

  [Required]
  public string CardCode { get; set; }

  public string PostalCode { get; set; }

  public string Address { get; set; }

  public string Address2 { get; set; }

  public string City { get; set; }

  public string State { get; set; }

  public string Country { get; set; }

  public long ProfileID { get; set; }

  public bool WasSaved { get; set; }

  public long NetsuiteCreditCardLineNumber { get; set; }
}
