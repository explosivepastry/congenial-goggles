// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.FirmwareDataModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

#nullable disable
namespace iMonnit.Models;

public class FirmwareDataModel
{
  public FirmwareDataModel()
  {
  }

  public FirmwareDataModel(
    long firmwareID,
    string name,
    string version,
    int sizeInBytes,
    bool isDeleted,
    string status,
    long deviceTypeID,
    long firmwareBaseID,
    long radioBandID)
  {
    this.FirmwareID = firmwareID;
    this.Name = name;
    this.Version = version;
    this.SizeInBytes = sizeInBytes;
    this.IsDeleted = isDeleted;
    this.Status = status;
    this.DeviceTypeID = deviceTypeID;
    this.FirmwareBaseID = firmwareBaseID;
    this.RadioBandID = radioBandID;
  }

  public long FirmwareID { get; set; }

  public string Name { get; set; }

  public string Version { get; set; }

  public int SizeInBytes { get; set; }

  public bool IsDeleted { get; set; }

  public string Status { get; set; }

  public long DeviceTypeID { get; set; }

  public long FirmwareBaseID { get; set; }

  public long RadioBandID { get; set; }

  public string FullFirmwareName => $"{this.Name} {this.Version}";
}
