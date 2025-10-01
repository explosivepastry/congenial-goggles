// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APISensor
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;

#nullable disable
namespace iMonnit.API;

public class APISensor
{
  public APISensor()
  {
  }

  public APISensor(SensorDatum sdat) => this.Make(sdat.sens);

  public APISensor(Sensor s) => this.Make(s);

  public void Make(Sensor s)
  {
    this.SensorID = s.SensorID;
    this.ApplicationID = s.ApplicationID;
    this.CSNetID = s.CSNetID;
    this.SensorName = s.SensorName;
    this.LastCommunicationDate = DateTime.SpecifyKind(s.LastCommunicationDate, DateTimeKind.Utc);
    this.NextCommunicationDate = DateTime.SpecifyKind(s.NextCommunicationDate, DateTimeKind.Utc);
    this.LastDataMessageMessageGUID = s.LastDataMessageGUID;
    this.PowerSourceID = s.PowerSourceID;
    this.Status = (int) s.Status;
    this.CanUpdate = s.CanUpdate;
    this.AlertsActive = s.SendNotification;
    this.AccountID = s.AccountID;
    this.IsCableEnabled = s.IsCableEnabled;
    DataMessage dataMessage = DataMessage.LoadLastBySensorQuickCached(s);
    if (dataMessage != null)
    {
      this.CurrentReading = dataMessage.DisplayData;
      this.BatteryLevel = (double) dataMessage.Battery;
      this.SignalStrength = (double) DataMessage.GetSignalStrengthPercent(s.GenerationType, s.SensorTypeID, dataMessage.SignalStrength);
    }
    else
    {
      this.CurrentReading = "No Reading Available";
      this.BatteryLevel = 0.0;
      this.SignalStrength = 0.0;
    }
  }

  public long SensorID { get; set; }

  public long ApplicationID { get; set; }

  public long CSNetID { get; set; }

  public string SensorName { get; set; }

  public DateTime LastCommunicationDate { get; set; }

  public DateTime NextCommunicationDate { get; set; }

  public Guid LastDataMessageMessageGUID { get; set; }

  public long PowerSourceID { get; set; }

  public int Status { get; set; }

  public bool CanUpdate { get; set; }

  public string CurrentReading { get; set; }

  public double BatteryLevel { get; set; }

  public double SignalStrength { get; set; }

  public bool AlertsActive { get; set; }

  public string CheckDigit => MonnitUtil.CheckDigit(this.SensorID);

  public long AccountID { get; set; }

  public bool IsCableEnabled { get; set; }
}
