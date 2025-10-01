// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.RefreshTestingGatewayModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

#nullable disable
namespace iMonnit.Models;

public class RefreshTestingGatewayModel
{
  public RefreshTestingGatewayModel(long gatewayID)
  {
    this.GatewayID = gatewayID;
    this.Status = "";
    this.DisplayDate = "";
    this.FirmwareVersion = "";
    this.DeviceCount = 0;
    this.isPaused = false;
    this.isDirty = false;
    this.SignalStrength = "";
    this.SignalStrengthPercent = "";
    this.PowerTest = "";
  }

  public long GatewayID { get; set; }

  public string Status { get; set; }

  public string DisplayDate { get; set; }

  public string SignalStrength { get; set; }

  public string SignalStrengthPercent { get; set; }

  public string PowerTest { get; set; }

  public string FirmwareVersion { get; set; }

  public int DeviceCount { get; set; }

  public bool isPaused { get; set; }

  public bool isDirty { get; set; }
}
