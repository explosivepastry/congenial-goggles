// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.RefreshTestingSensorModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

#nullable disable
namespace iMonnit.Models;

public class RefreshTestingSensorModel
{
  public RefreshTestingSensorModel(long sensorID)
  {
    this.SensorID = sensorID;
    this.Status = "";
    this.DisplayDate = "";
    this.FirmwareVersion = "";
    this.isDirty = false;
    this.isPaused = false;
    this.SignalStrength = "";
    this.SignalStrengthPercent = "";
    this.BatteryLevel = "";
    this.Reading = "";
    this.ReadingTitle = "";
    this.GeneralConfig3DirtyColor = "";
    this.GeneralConfig2DirtyColor = "";
    this.ProfileConfigDirtyColor = "";
    this.ProfileConfig2DirtyColor = "";
    this.GeneralConfigDirtyColor = "";
    this.PendingActionControlCommandColor = "";
  }

  public long SensorID { get; set; }

  public string Status { get; set; }

  public string Reading { get; set; }

  public string ReadingTitle { get; set; }

  public string DisplayDate { get; set; }

  public string SignalStrength { get; set; }

  public string SignalStrengthPercent { get; set; }

  public string BatteryLevel { get; set; }

  public string FirmwareVersion { get; set; }

  public bool isDirty { get; set; }

  public bool isPaused { get; set; }

  public string GeneralConfig3DirtyColor { get; set; }

  public string GeneralConfig2DirtyColor { get; set; }

  public string ProfileConfigDirtyColor { get; set; }

  public string ProfileConfig2DirtyColor { get; set; }

  public string GeneralConfigDirtyColor { get; set; }

  public string PendingActionControlCommandColor { get; set; }
}
