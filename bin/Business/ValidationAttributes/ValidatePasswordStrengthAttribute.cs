// Decompiled with JetBrains decompiler
// Type: Monnit.ValidatePasswordStrengthNOUSEAttribute
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

#nullable disable
namespace Monnit;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public sealed class ValidatePasswordStrengthNOUSEAttribute : ValidationAttribute
{
  private const string _defaultErrorMessage = "'{0}' must be at least {1} characters long.";
  private readonly int _minCharacters = 8;

  public ValidatePasswordStrengthNOUSEAttribute()
    : base("'{0}' must be at least {1} characters long.")
  {
  }

  public override string FormatErrorMessage(string name)
  {
    return string.Format((IFormatProvider) CultureInfo.CurrentUICulture, this.ErrorMessageString, (object) name, (object) this._minCharacters);
  }

  public override bool IsValid(object value)
  {
    return value is string str && str.Length >= this._minCharacters;
  }
}
