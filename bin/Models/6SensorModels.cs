// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.ChartModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;

#nullable disable
namespace iMonnit.Models;

public class ChartModel
{
  public DateTime FromDate { get; set; }

  public DateTime ToDate { get; set; }

  public Sensor Sensor { get; set; }

  public TimeSpan Tspan => this.ToDate.Subtract(this.FromDate);
}
