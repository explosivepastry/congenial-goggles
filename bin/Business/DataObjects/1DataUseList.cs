// Decompiled with JetBrains decompiler
// Type: Monnit.Data.DataUseList
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class DataUseList
{
  [DBMethod("DataUseList_LoadAllByCarrier")]
  [DBMethodBody(DBMS.SqlServer, "\r\nIF @ForLogging =1 \r\nBEGIN\r\n\r\n  SELECT\r\n    * \r\n  FROM dbo.[DataUseList] WITH (NOLOCK)\r\n  WHERE ISNULL(Carrier, 'Null') = @Carrier\r\n    AND eMonnitStatus = 1\r\n  ORDER BY Carrier DESC, CellID;\r\n\r\nEND ELSE\r\nBEGIN\r\n\r\n  SELECT\r\n    * \r\n  FROM dbo.[DataUseList] WITH (NOLOCK)\r\n  WHERE ISNULL(Carrier, 'Null') = @Carrier\r\n    AND ISNULL(eMonnitStatus, 0) != 3\r\n  ORDER BY Carrier DESC, CellID;\r\n\r\nEND\r\n")]
  internal class LoadAllByCarrier : BaseDBMethod
  {
    [DBMethodParam("Carrier", typeof (string))]
    public string Carrier { get; private set; }

    [DBMethodParam("ForLogging", typeof (bool))]
    public bool ForLogging { get; private set; }

    public List<Monnit.DataUseList> Result { get; private set; }

    public LoadAllByCarrier(string carrier, bool forLogging)
    {
      this.Carrier = carrier;
      this.ForLogging = forLogging;
      this.Result = BaseDBObject.Load<Monnit.DataUseList>(this.ToDataTable());
    }
  }

  [DBMethod("DataUseList_PopulateNew")]
  [DBMethodBody(DBMS.SqlServer, "\r\nUPDATE d\r\n  Set d.CellID = v.CellID,\r\n  d.eMonnitStatus = 2, \r\n  d.Carrier = CASE WHEN d.Carrier = 'DoNotRun' THEN NULL ELSE d.Carrier end\r\nFROM dbo.[DataUseList] d WITH (NOLOCK)\r\nINNER JOIN Admin.dbo.[vwGatewayMacAddress] v  WITH (NOLOCK) on d.GatewayID = v.GatewayID\r\nWHERE d.CellID != v.CellID\r\n  AND v.LastCommunicationDate > DATEADD(DAY, -3, GETUTCDATE())\r\n\r\nINSERT INTO dbo.[Datauselist] (GatewayID, CellID, TimeZoneIDString, Carrier, Name, Status, eMonnitStatus)\r\nSELECT \r\n  v.GatewayID, \r\n  v.CellID, \r\n  'Mountain Standard Time', \r\n  NULL, \r\n  v.Name, \r\n  null, \r\n  2\r\nFROM Admin.dbo.[vwGatewayMacAddress] v\r\nLEFT JOIN dbo.[DataUseList] l on v.gatewayid = l.gatewayid\r\nwhere v.CellID IS NOT NULL \r\n  AND l.GatewayID IS NULL\r\n  AND v.AccountNumber NOT IN (\r\n'Monnit Default Programmed Devices',\r\n'Monnit - Fulfillment Testing',\r\n'BlackHole')\r\n  AND (v.cellid like '8%')\r\n  AND len(v.cellid) = 20;\r\n\r\nSELECT top 1 * from DataUseList;\r\n")]
  internal class PopulateNew : BaseDBMethod
  {
    public List<Monnit.DataUseList> Result { get; private set; }

    public PopulateNew() => this.Result = BaseDBObject.Load<Monnit.DataUseList>(this.ToDataTable());
  }
}
