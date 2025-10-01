// Decompiled with JetBrains decompiler
// Type: Monnit.ResellerToSensorApplicationMetaData
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace Monnit;

public class ResellerToSensorApplicationMetaData
{
  [Required]
  [DisplayName("ResellerAccountID")]
  public long ResellerAccountID { get; set; }

  [Required]
  [DisplayName("SensorApplicationID")]
  public long SensorApplicationID { get; set; }
}
