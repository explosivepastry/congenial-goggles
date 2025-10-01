// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.CreateAccountAPIModel2
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace iMonnit.Models;

public class CreateAccountAPIModel2
{
  private string _AccountNumber = (string) null;
  private int _PremiumDays = 45;

  [Required]
  [DisplayName("Company Name")]
  public string CompanyName { get; set; }

  [DisplayName("AccountNumber")]
  public string AccountNumber
  {
    get => string.IsNullOrEmpty(this._AccountNumber) ? this.CompanyName : this._AccountNumber;
    set => this._AccountNumber = value;
  }

  [DisplayName("Is Premium")]
  public bool IsPremium { get; set; }

  [DisplayName("Premium Subscription Length (Days)")]
  public int PremiumDays
  {
    get => this._PremiumDays;
    set => this._PremiumDays = value;
  }

  [Required]
  [DisplayName("Time Zone")]
  public long TimeZoneID { get; set; }

  [DisplayName("Street")]
  public string Address { get; set; }

  [DisplayName("")]
  public string Address2 { get; set; }

  [DisplayName("City")]
  public string City { get; set; }

  [DisplayName("State")]
  public string State { get; set; }

  [DisplayName("Zip Code")]
  public string PostalCode { get; set; }

  [DisplayName("Country")]
  public string Country { get; set; }

  [DisplayName("Network Name")]
  public string NetworkName { get; set; }

  [DisplayName("Gateway ID")]
  public long? GatewayID { get; set; }

  [DisplayName("Security Code")]
  public string CheckDigit { get; set; }

  [DisplayName("Reseller")]
  public string ResellerID { get; set; }

  [Required]
  [DisplayName("Customer")]
  public long CustomerID { get; set; }
}
