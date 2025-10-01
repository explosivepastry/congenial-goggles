// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIDataMessage
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace iMonnit.API;

public class APIDataMessage
{
  public APIDataMessage()
  {
  }

  public APIDataMessage(Sensor s, DataMessage dm)
  {
    this.DataMessageGUID = dm.DataMessageGUID;
    this.SensorID = dm.SensorID;
    this.MessageDate = DateTime.SpecifyKind(dm.MessageDate, DateTimeKind.Utc);
    this.State = dm.State;
    this.SignalStrength = DataMessage.GetSignalStrengthPercent(s.GenerationType, s.SensorTypeID, dm.SignalStrength);
    this.Voltage = dm.Voltage;
    this.Battery = dm.Battery;
    this.Data = dm.Data;
    this.DisplayData = dm.DisplayData;
    this.PlotValue = dm.PlotValueString;
    this.MetNotificationRequirements = dm.MeetsNotificationRequirement;
    this.GatewayID = dm.GatewayID;
    this.DataTypes = string.Join<eDatumType>("|", dm.AppBase.Datums.Select<AppDatum, eDatumType>((Func<AppDatum, eDatumType>) (dat => dat.data.datatype)));
    this.DataValues = string.Join<object>("|", dm.AppBase.Datums.Select<AppDatum, object>((Func<AppDatum, object>) (dat => dat.data.compvalue)));
    this.PlotValues = string.Join<object>("|", (IEnumerable<object>) dm.AppBase.GetPlotValues(s.SensorID));
    this.PlotLabels = string.Join("|", (IEnumerable<string>) dm.AppBase.GetPlotLabels(s.SensorID));
    this.PlotLabels = this.PlotLabels == "" ? this.DataTypes : this.PlotLabels;
  }

  public Guid DataMessageGUID { get; set; }

  public long SensorID { get; set; }

  public DateTime MessageDate { get; set; }

  public int State { get; set; }

  public int SignalStrength { get; set; }

  public double Voltage { get; set; }

  public int Battery { get; set; }

  public string Data { get; set; }

  public string DisplayData { get; set; }

  public string PlotValue { get; set; }

  public bool MetNotificationRequirements { get; set; }

  public long GatewayID { get; set; }

  public string DataValues { get; set; }

  public string DataTypes { get; set; }

  public string PlotValues { get; set; }

  public string PlotLabels { get; set; }
}
