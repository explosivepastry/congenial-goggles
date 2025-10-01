// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.EditCustomerAPIModel
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
public class EditCustomerAPIModel
{
  private SMSCarrier _SMSCarrier;

  [DisplayName("User Name")]
  public string UserName { get; set; }

  [DisplayName("First Name")]
  public string FirstName { get; set; }

  [DisplayName("Last Name")]
  public string LastName { get; set; }

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
