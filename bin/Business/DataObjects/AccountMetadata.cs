// Decompiled with JetBrains decompiler
// Type: Monnit.AccountMetadata
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace Monnit;

public class AccountMetadata
{
  [DisplayName("Account Identifier")]
  public long AccountID { get; set; }

  [Required]
  [DisplayName("Account Number")]
  public string AccountNumber { get; set; }

  [DisplayName("Company Name")]
  public string CompanyName { get; set; }

  [DisplayName("Primary Contact")]
  public long PrimaryContactID { get; set; }

  [DisplayName("Time Zone")]
  public long TimeZoneID { get; set; }
}
