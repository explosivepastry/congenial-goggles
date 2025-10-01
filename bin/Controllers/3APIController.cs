// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIGatewayGetExtendedForFulfillment
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APIGatewayGetExtendedForFulfillment
{
  public long SensorID;
  public long CSNetID;

  public APIGatewayGetExtendedForFulfillment()
  {
  }

  public APIGatewayGetExtendedForFulfillment(Gateway gateway)
  {
    this.GatewayID = gateway.GatewayID;
    this.GatewayName = gateway.Name;
    this.GatewayTypeID = gateway.GatewayTypeID;
    this.RadioBand = gateway.RadioBand;
    this.APNFirmwareVersion = gateway.APNFirmwareVersion;
    this.GatewayFirmwareVersion = gateway.GatewayFirmwareVersion;
    this.MacAddress = gateway.MacAddress;
    this.SensorID = gateway.SensorID;
    this.CSNetID = gateway.CSNetID;
  }

  public long GatewayID { get; set; }

  public string GatewayName { get; set; }

  public long GatewayTypeID { get; set; }

  public string RadioBand { get; set; }

  public string APNFirmwareVersion { get; set; }

  public string GatewayFirmwareVersion { get; set; }

  public string MacAddress { get; set; }
}
