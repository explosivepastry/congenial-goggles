// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.CreateAccountOVModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace iMonnit.Models;

[PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "The new password and confirmation password do not match.")]
public class CreateAccountOVModel
{
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

  [Required]
  [DisplayName("Company Name")]
  public string CompanyName { get; set; }

  [DisplayName("Account Number")]
  public string AccountNumber { get; set; }

  [Required]
  [DisplayName("Time Zone")]
  public long TimeZoneID { get; set; }

  public int MaxFailedLogins { get; set; }

  [DisplayName("SubscriptionCode")]
  public string SubscriptionCode { get; set; }

  [DisplayName("Reseller")]
  public string ResellerID { get; set; }

  public bool EULA { get; set; }
}
