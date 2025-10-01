// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.CreateAccountModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace iMonnit.Models;

public class CreateAccountModel
{
  private int _PremiumDays = 45;

  [Required]
  [DisplayName("Company Name")]
  public string CompanyName { get; set; }

  [DisplayName("Account Number")]
  public string AccountNumber { get; set; }

  [DisplayName("Is Premium")]
  public bool IsPremium { get; set; }

  [DisplayName("Subscription")]
  public string Subscription { get; set; }

  [DisplayName("Premium Subscription Length (Days)")]
  public int PremiumDays
  {
    get => this._PremiumDays;
    set => this._PremiumDays = value;
  }

  [Required(AllowEmptyStrings = false, ErrorMessage = "Time Zone Must Be Selected")]
  [DisplayName("Time Zone")]
  public long TimeZoneID { get; set; }

  [Required]
  [DisplayName("Industry Type")]
  public long IndustryTypeID { get; set; }

  [Required]
  [DisplayName("Business Type")]
  public long BusinessTypeID { get; set; }

  [DisplayName("Purchase Location")]
  public string PurchaseLocation { get; set; }

  public int MaxFailedLogins { get; set; }

  [Required]
  [DisplayName("Street")]
  public string Address { get; set; }

  [DisplayName("")]
  public string Address2 { get; set; }

  [Required]
  [DisplayName("City")]
  public string City { get; set; }

  [Required]
  [DisplayName("State")]
  public string State { get; set; }

  [Required]
  [DisplayName("Zip Code")]
  public string PostalCode { get; set; }

  [Required]
  [DisplayName("Country")]
  public string Country { get; set; }

  [DisplayName("Reseller")]
  public string ResellerID { get; set; }

  public bool EULA { get; set; }

  [DisplayName("Subscription Activation Code")]
  public string SubscriptionActivationCode { get; set; }
}
