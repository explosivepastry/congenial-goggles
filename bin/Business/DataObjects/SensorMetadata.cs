// Decompiled with JetBrains decompiler
// Type: Monnit.SensorMetadata
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace Monnit;

public class SensorMetadata
{
  [DisplayName("Sensor Profile")]
  public int MonnitApplicationID { get; set; }

  [Required]
  [DisplayName("Sensor Name")]
  public string SensorName { get; set; }

  [DisplayName("Heartbeat (Minutes)")]
  public int ReportInterval { get; set; }

  [DisplayName("Aware State Heartbeat (Minutes)")]
  public int ActiveStateInterval { get; set; }

  [DisplayName("Inactivity Alert (Minutes)")]
  public int MinimumCommunicationFrequency { get; set; }

  [DisplayName("Last Communication")]
  public DateTime LastCommunicationDate { get; set; }

  [DisplayName("Active Between")]
  public DateTime TimeOfDayActive { get; set; }

  [DisplayName("Assessments per Heartbeat (1-250)")]
  public int MeasurementsPerTransmission { get; set; }

  [DisplayName("Synchronization Offset")]
  public int TransmitOffset { get; set; }

  [DisplayName("Hysteresis")]
  public long Hysteresis { get; set; }

  [DisplayName("Minimum Threshold")]
  public long MinimumThreshold { get; set; }

  [DisplayName("Maximum Threshold")]
  public long MaximumThreshold { get; set; }

  [DisplayName("Time to Re-Arm (seconds)")]
  public int RearmTime { get; set; }

  [DisplayName("Power Source")]
  public long PowerSourceID { get; set; }

  [DisplayName("Radio Band")]
  public string RadioBand { get; set; }

  [DisplayName("Firmware Version")]
  public string FirmwareVersion { get; set; }

  [DisplayName("Sensor Type")]
  public long SensorTypeID { get; set; }
}
