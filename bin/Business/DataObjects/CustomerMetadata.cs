// Decompiled with JetBrains decompiler
// Type: Monnit.CustomerMetadata
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace Monnit;

[PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "The password and confirmation password do not match.")]
public class CustomerMetadata
{
  [Required]
  [DisplayName("User Name")]
  public string UserName { get; set; }

  [Required]
  [DataType(DataType.Text)]
  [DisplayName("Password")]
  public string Password { get; set; }

  [Required]
  [DataType(DataType.Text)]
  [DisplayName("Confirm password")]
  public string ConfirmPassword { get; set; }

  [Required]
  [DisplayName("First Name")]
  public string FirstName { get; set; }

  [Required]
  [DisplayName("Last Name")]
  public string LastName { get; set; }

  [Required]
  [ValidateEmailAddress]
  [DisplayName("Email Address")]
  public string NotificationEmail { get; set; }

  [ValidatePhoneNumber]
  [DisplayName("Cell Phone")]
  public string NotificationPhone { get; set; }

  [DisplayName("Cell Carrier")]
  public long? SMSCarrierID { get; set; }

  [DisplayName("Password Expired")]
  public bool PasswordExpired { get; set; }

  [DisplayName("Is Administrator")]
  public bool IsAdmin { get; set; }
}
