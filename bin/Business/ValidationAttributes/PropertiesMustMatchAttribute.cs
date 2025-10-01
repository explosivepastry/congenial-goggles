// Decompiled with JetBrains decompiler
// Type: Monnit.PropertiesMustMatchAttribute
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

#nullable disable
namespace Monnit;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public sealed class PropertiesMustMatchAttribute : ValidationAttribute
{
  private const string _defaultErrorMessage = "'{0}' and '{1}' do not match.";
  private readonly object _typeId = new object();

  public PropertiesMustMatchAttribute(string originalProperty, string confirmProperty)
    : base("'{0}' and '{1}' do not match.")
  {
    this.OriginalProperty = originalProperty;
    this.ConfirmProperty = confirmProperty;
  }

  public string ConfirmProperty { get; private set; }

  public string OriginalProperty { get; private set; }

  public override object TypeId => this._typeId;

  public override string FormatErrorMessage(string name)
  {
    return string.Format((IFormatProvider) CultureInfo.CurrentUICulture, this.ErrorMessageString, (object) this.OriginalProperty, (object) this.ConfirmProperty);
  }

  public override bool IsValid(object value)
  {
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
    return object.Equals(properties.Find(this.OriginalProperty, true).GetValue(value), properties.Find(this.ConfirmProperty, true).GetValue(value));
  }
}
