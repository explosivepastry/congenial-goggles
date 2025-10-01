// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.CreatePrimaryContactModel
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
public class CreatePrimaryContactModel
{
  private SMSCarrier _SMSCarrier;

  [Required]
  [DisplayName("User Name")]
  public string UserName { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [DisplayName("Password")]
  public string Password { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [DisplayName("Confirm new password")]
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
}
