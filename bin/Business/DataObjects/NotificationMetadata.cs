// Decompiled with JetBrains decompiler
// Type: Monnit.NotificationMetadata
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

#nullable disable
namespace Monnit;

public class NotificationMetadata
{
  [DisplayName("Notification Text")]
  public string NotificationText { get; set; }

  [HiddenInput]
  public string Version { get; set; }

  [DisplayName("Comparison")]
  public eCompareType CompareType { get; set; }

  [Required]
  [DisplayName("Value to Compare")]
  public string CompareValue { get; set; }

  [Required]
  [DisplayName("Don't Alert again for (Snooze)")]
  public double SnoozeDuration { get; set; }
}
