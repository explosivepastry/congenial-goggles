// Decompiled with JetBrains decompiler
// Type: Monnit.Data.OTARequest
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class OTARequest
{
  [DBMethod("OTARequest_LoadActiveByGatewayID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nUPDATE s SET s.Status = 4\r\nFROM OTARequestSensor s\r\nINNER JOIN OTARequest o on o.OTARequestID = s.OTARequestID\r\nWHERE o.Status in (0,1)\r\nAND o.CreateDate < DateAdd(day,-7,GetUTCDate())\r\n\r\n\r\nUPDATE OTARequest SET Status = 4\r\nWHERE Status in (0,1)\r\nAND CreateDate < DateAdd(day,-7,GetUTCDate())\r\n\r\nSELECT * \r\nFROM OTARequest r\r\nWHERE r.GatewayID = @GatewayID\r\nAND r.Status in (0,1)  --New/InProcess\r\nORDER BY r.Status desc, r.CreateDate;\r\n\r\n")]
  internal class LoadActiveByGatewayID : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    public List<Monnit.OTARequest> Result { get; private set; }

    public LoadActiveByGatewayID(long gatewayID)
    {
      this.GatewayID = gatewayID;
      this.Result = BaseDBObject.Load<Monnit.OTARequest>(this.ToDataTable());
    }

    public static List<Monnit.OTARequest> Exec(long gatewayID)
    {
      return new OTARequest.LoadActiveByGatewayID(gatewayID).Result;
    }
  }

  [DBMethod("OTARequest_LoadActiveByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nUPDATE s SET s.Status = 4\r\nFROM OTARequestSensor s\r\nINNER JOIN OTARequest o on o.OTARequestID = s.OTARequestID\r\nWHERE o.Status in (0,1)\r\nAND o.CreateDate < DateAdd(day,-7,GetUTCDate())\r\n\r\n\r\nUPDATE OTARequest SET Status = 4\r\nWHERE Status in (0,1)\r\nAND CreateDate < DateAdd(day,-7,GetUTCDate())\r\n\r\nSELECT * \r\nFROM OTARequest r\r\nWHERE r.AccountID = @AccountID\r\nAND r.Status in (0,1)  --New/InProcess\r\nORDER BY r.Status desc, r.CreateDate;\r\n")]
  internal class LoadActiveByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<Monnit.OTARequest> Result { get; private set; }

    public LoadActiveByAccountID(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.OTARequest>(this.ToDataTable());
    }

    public static List<Monnit.OTARequest> Exec(long accountID)
    {
      return new OTARequest.LoadActiveByAccountID(accountID).Result;
    }
  }

  [DBMethod("OTARequest_LoadSensorsToUpdate")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @Index0 INT = 16;\r\nDECLARE @Index1 INT = 34;\r\nDECLARE @Index2 INT = 21;\r\nDECLARE @Index3 INT = 0;\r\n\r\nIF @AccountID in (2,2372)\r\n  SET @AccountID = -1;\r\n\r\nSELECT\r\n  s.*\r\nFROM dbo.[Sensor] s WITH (NOLOCK)\r\nLEFT JOIN (\r\n          Select \r\n            SensorID \r\n          FROM dbo.[OTARequestSensor] s WITH (NOLOCK) \r\n          INNER JOIN dbo.[OTARequest] o WITH (NOLOCK) ON s.OTARequestID = o.OTARequestID \r\n          WHERE o.Status in (0,1)\r\n          ) t ON s.SensorID = t.SensorID\r\nWHERE GenerationType LIKE '%Gen2%'\r\n  AND AccountID = @AccountID\r\n  AND (\r\n        dbo.SplitIndex(FirmwareVersion, '.', 0) > @Index0 \r\n        OR (dbo.SplitIndex(FirmwareVersion, '.', 0) = @Index0 AND dbo.SplitIndex(FirmwareVersion, '.', 1) > @Index1  )\r\n        OR (dbo.SplitIndex(FirmwareVersion, '.', 0) = @Index0 AND dbo.SplitIndex(FirmwareVersion, '.', 1) = @Index1 AND dbo.SplitIndex(FirmwareVersion, '.', 2) > @Index2 )\r\n        OR (dbo.SplitIndex(FirmwareVersion, '.', 0) = @Index0 AND dbo.SplitIndex(FirmwareVersion, '.', 1) = @Index1 AND dbo.SplitIndex(FirmwareVersion, '.', 2) = @Index2 AND dbo.SplitIndex(FirmwareVersion, '.', 3) > @Index3)\r\n      )\r\n  AND t.SensorID IS NULL  --isn't part of an OTA Request (status 2/4)\r\n  AND s.sku IS NOT NULL\r\n  AND s.LastDataMessageGUID IS NOT NULL\r\n  ORDER by s.SKU;\r\n")]
  internal class LoadSensorsToUpdate : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public string MinVersion { get; private set; }

    public List<Monnit.Sensor> Result { get; private set; }

    public LoadSensorsToUpdate(long accountID, string minVersion)
    {
      this.AccountID = accountID;
      this.MinVersion = minVersion;
      this.Result = BaseDBObject.Load<Monnit.Sensor>(this.ToDataTable());
    }

    public static List<Monnit.Sensor> Exec(long accountID, string minVersion)
    {
      return new OTARequest.LoadSensorsToUpdate(accountID, minVersion).Result;
    }
  }
}
