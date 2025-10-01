// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIGatewayMetaData
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APIGatewayMetaData
{
  public APIGatewayMetaData()
  {
  }

  public APIGatewayMetaData(Gateway gateway)
  {
    this.GatewayID = gateway.GatewayID;
    this.GatewayTypeID = gateway.GatewayTypeID;
    this.RadioBand = gateway.RadioBand;
    this.APNFirmwareVersion = gateway.APNFirmwareVersion;
    this.GatewayFirmwareVersion = gateway.GatewayFirmwareVersion;
    this.GenerationType = string.IsNullOrWhiteSpace(gateway.GenerationType) ? "Gen1" : gateway.GenerationType;
  }

  public long GatewayID { get; set; }

  public long GatewayTypeID { get; set; }

  public string RadioBand { get; set; }

  public string APNFirmwareVersion { get; set; }

  public string GatewayFirmwareVersion { get; set; }

  public string GenerationType { get; set; }
}
