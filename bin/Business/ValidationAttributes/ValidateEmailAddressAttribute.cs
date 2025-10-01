// Decompiled with JetBrains decompiler
// Type: Monnit.ValidateEmailAddressAttribute
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net.Mail;

#nullable disable
namespace Monnit;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public sealed class ValidateEmailAddressAttribute : ValidationAttribute
{
  private const string _defaultErrorMessage = "{0} is not in a recognized format.";

  public ValidateEmailAddressAttribute()
    : base("{0} is not in a recognized format.")
  {
  }

  public override string FormatErrorMessage(string name)
  {
    return string.Format((IFormatProvider) CultureInfo.CurrentUICulture, this.ErrorMessageString, (object) name);
  }

  public override bool IsValid(object value)
  {
    if (value is string address && address != string.Empty)
    {
      try
      {
        MailAddress mailAddress = new MailAddress(address);
      }
      catch (FormatException ex)
      {
        return false;
      }
    }
    return true;
  }
}
