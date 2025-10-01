// Decompiled with JetBrains decompiler
// Type: Monnit.EmailTemplateMetadata
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace Monnit;

public class EmailTemplateMetadata
{
  [Required]
  [DisplayName("Name")]
  public string Name { get; set; }

  [DisplayName("Subject")]
  public string Subject { get; set; }

  [Required]
  [DisplayName("Template")]
  public string Template { get; set; }
}
