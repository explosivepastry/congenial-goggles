// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.CreateAccountAPIModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using RedefineImpossible;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace iMonnit.Models;

[PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "The password and confirmation password do not match.")]
public class CreateAccountAPIModel
{
  private string _AccountNumber = (string) null;
  private int _PremiumDays = 45;
  private SMSCarrier _SMSCarrier;

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
  public string TimeZoneID { get; set; }

  [Required]
  [DisplayName("User Name")]
  public string UserName { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [DisplayName("Password")]
  public string Password { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [DisplayName("Confirm New Password")]
  public string ConfirmPassword { get; set; }

  [Required]
  [DisplayName("First Name")]
  public string FirstName { get; set; }

  [Required]
  [DisplayName("Last Name")]
  public string LastName { get; set; }

  [Required]
  [DisplayName("Email Address")]
  [EmailAddress]
  public string NotificationEmail { get; set; }

  [DisplayName("Cell Phone")]
  public string NotificationPhone { get; set; }

  [DisplayName("Cell Carrier")]
  public string SMSCarrierID { get; set; }

  public SMSCarrier SMSCarrier
  {
    get
    {
      if (this._SMSCarrier == null)
        this._SMSCarrier = SMSCarrier.Load(this.SMSCarrierID.ToLong());
      return this._SMSCarrier;
    }
    set
    {
      this._SMSCarrier = value;
      this.SMSCarrierID = value.SMSCarrierID.ToString();
    }
  }

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
}
