// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APILookUpGateway
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APILookUpGateway
{
  public APILookUpGateway()
  {
  }

  public APILookUpGateway(Gateway gateway)
  {
    this.GatewayID = gateway.GatewayID;
    this.GatewayName = gateway.Name;
    this.GatewayTypeID = gateway.GatewayTypeID;
    this.RadioBand = gateway.RadioBand;
    this.APNFirmwareVersion = gateway.APNFirmwareVersion;
    this.GatewayFirmwareVersion = gateway.GatewayFirmwareVersion;
    this.MacAddress = gateway.MacAddress;
    this.TransmitPower = gateway.TransmitPower;
    this.SensorID = new long?(gateway.SensorID);
    this.GenerationType = string.IsNullOrWhiteSpace(gateway.GenerationType) ? "Gen1" : gateway.GenerationType;
    this.PowerSourceID = gateway.PowerSourceID;
    this.SKU = gateway.SKU;
  }

  public long GatewayID { get; set; }

  public string GatewayName { get; set; }

  public long GatewayTypeID { get; set; }

  public string RadioBand { get; set; }

  public string APNFirmwareVersion { get; set; }

  public string GatewayFirmwareVersion { get; set; }

  public string MacAddress { get; set; }

  public int TransmitPower { get; set; }

  public long? SensorID { get; set; }

  public string GenerationType { get; set; }

  public long PowerSourceID { get; set; }

  public string SKU { get; set; }
}
