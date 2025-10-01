// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.ChartSensorDataModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class ChartSensorDataModel
{
  public ChartSensorDataModel(List<Sensor> sensors, DateTime fromDate, DateTime toDate)
  {
    this.Sensors = sensors;
    this.FromDate = fromDate;
    this.ToDate = toDate;
  }

  public List<Sensor> Sensors { get; set; }

  public DateTime FromDate { get; set; }

  public DateTime ToDate { get; set; }
}
