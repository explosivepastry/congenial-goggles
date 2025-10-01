// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.InboundPacketReportModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace iMonnit.Models;

public class InboundPacketReportModel : DateRangeModel
{
  [Required]
  [DisplayName("Network ID")]
  public long CSNetID { get; set; }

  public InboundPacketReportModel()
  {
    this.StartDate = DateTime.UtcNow.AddDays(-1.0);
    this.EndDate = DateTime.UtcNow;
    this.ShowTime = true;
  }
}
