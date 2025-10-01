// Decompiled with JetBrains decompiler
// Type: Monnit.ValidatePhoneNumberAttribute
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

#nullable disable
namespace Monnit;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public sealed class ValidatePhoneNumberAttribute : ValidationAttribute
{
  private const string _defaultErrorMessage = "{0} is not a recognized format.";

  public ValidatePhoneNumberAttribute()
    : base("{0} is not a recognized format.")
  {
  }

  public override string FormatErrorMessage(string name)
  {
    return string.Format((IFormatProvider) CultureInfo.CurrentUICulture, this.ErrorMessageString, (object) name);
  }

  public override bool IsValid(object value)
  {
    if (!(value is string str) || !(str != string.Empty))
      return true;
    int num = 0;
    foreach (char c in str.ToCharArray())
    {
      if (char.IsNumber(c))
        ++num;
    }
    return num > 5;
  }
}
