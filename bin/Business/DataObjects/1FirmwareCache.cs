// Decompiled with JetBrains decompiler
// Type: Monnit.Data.FirmwareCache
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class FirmwareCache
{
  [DBMethod("FirmwareCache_FindBySKU")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE dbo.[FirmwareCache]\r\nWHERE CacheDate < DATEADD(HOUR,-1,GETUTCDATE());\r\n\r\nSELECT\r\n  *\r\nFROM dbo.[FirmwareCache]\r\nWHERE GatewayID     = @GatewayID\r\n  AND SKU           = @SKU\r\n  AND IsGatewayBase = @IsGatewayBase;\r\n")]
  internal class FindBySKU : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    [DBMethodParam("SKU", typeof (string))]
    public string SKU { get; private set; }

    [DBMethodParam("IsGatewayBase", typeof (bool))]
    public bool IsGatewayBase { get; private set; }

    public Monnit.FirmwareCache Result { get; private set; }

    public FindBySKU(long gatewayID, string sku, bool isGatewayBase)
    {
      this.GatewayID = gatewayID;
      this.SKU = sku;
      this.IsGatewayBase = isGatewayBase;
      this.Result = BaseDBObject.Load<Monnit.FirmwareCache>(this.ToDataTable()).FirstOrDefault<Monnit.FirmwareCache>();
    }
  }

  [DBMethod("FirmwareCache_FindByFirmware")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE dbo.[FirmwareCache]\r\nWHERE CacheDate < DATEADD(HOUR,-1,GETUTCDATE());\r\n\r\nSELECT\r\n  *\r\nFROM dbo.[FirmwareCache]\r\nWHERE GatewayID     = @GatewayID\r\n  AND FirmwareID    = @FirmwareID;\r\n")]
  internal class FindByFirmware : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    [DBMethodParam("FirmwareID", typeof (long))]
    public long FirmwareID { get; private set; }

    public Monnit.FirmwareCache Result { get; private set; }

    public FindByFirmware(long gatewayID, long firmwareID)
    {
      this.GatewayID = gatewayID;
      this.FirmwareID = firmwareID;
      this.Result = BaseDBObject.Load<Monnit.FirmwareCache>(this.ToDataTable()).FirstOrDefault<Monnit.FirmwareCache>();
    }
  }

  [DBMethod("FirmwareCache_ClearForGateway")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE dbo.[FirmwareCache]\r\nWHERE GatewayID = @GatewayID\r\n   OR CacheDate < DATEADD(HOUR,-1,GETUTCDATE());\r\n")]
  internal class ClearForGateway : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    public ClearForGateway(long gatewayID)
    {
      this.GatewayID = gatewayID;
      this.Execute();
    }
  }
}
