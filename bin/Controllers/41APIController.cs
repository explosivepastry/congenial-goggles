// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIChartDataPoint
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;

#nullable disable
namespace iMonnit.API;

public class APIChartDataPoint
{
  public APIChartDataPoint()
  {
  }

  public APIChartDataPoint(DataMessage dm)
  {
    this.Date = DateTime.SpecifyKind(dm.MessageDate, DateTimeKind.Utc);
    this.Value = dm.AppBase.PlotValue == null ? string.Empty : dm.AppBase.PlotValue.ToString();
    this.SentNotification = dm.MeetsNotificationRequirement;
  }

  public APIChartDataPoint(DataPoint dp)
  {
    this.Date = DateTime.SpecifyKind(dp.Date, DateTimeKind.Utc);
    this.Value = dp.Value == null ? string.Empty : dp.Value.ToString();
    this.SentNotification = dp.SentNotification;
  }

  public DateTime Date { get; set; }

  public string Value { get; set; }

  public bool SentNotification { get; set; }
}
