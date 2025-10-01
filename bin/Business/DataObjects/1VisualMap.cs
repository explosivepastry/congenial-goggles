// Decompiled with JetBrains decompiler
// Type: Monnit.Data.VisualMap
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class VisualMap
{
  [DBMethod("VisualMap_LoadByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  [VisualMapID],\r\n  [AccountID],\r\n  [Name],\r\n  [Width],\r\n  [Height],\r\n  [image] = NULL,\r\n  [MapType]\r\nFROM dbo.[VisualMap]\r\nWHERE [AccountID] = @AccountID;\r\n")]
  internal class LoadByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<Monnit.VisualMap> Result { get; private set; }

    public LoadByAccountID(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.VisualMap>(this.ToDataTable());
    }
  }

  [DBMethod("VisualMap_LoadGatewaysByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  DeviceID = g.GatewayID,\r\n  VisualMapID,\r\n  Name = g.Name,\r\n  ApplicationID = NULL,\r\n  GatewayTypeID\r\nFROM dbo.[Gateway] g WITH (NOLOCK)\r\nINNER JOIN dbo.[CSNet] c WITH (NOLOCK) on g.CSNetID = c.CSNetID\r\nLEFT JOIN dbo.[VisualMapGateway] v  WITH (NOLOCK) ON g.GatewayID = v.GatewayID AND v.VisualMapID = @VisualMapID\r\nWHERE c.AccountID       = @AccountID\r\n  AND g.isGPSUnlocked   = @isGPSUnlocked\r\n  AND g.CSNetID         = ISNULL(@CSNetID, g.CSNetID)\r\n  AND g.Name            = ISNULL(@Name, g.Name);\r\n")]
  internal class LoadGateways : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("VisualMapID", typeof (long))]
    public long VisualMapID { get; private set; }

    [DBMethodParam("IsGPSUnlocked", typeof (bool))]
    public bool IsGPSUnlocked { get; private set; }

    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

    [DBMethodParam("Name", typeof (string))]
    public string Name { get; private set; }

    public DataTable Result { get; private set; }

    public LoadGateways(
      long accountID,
      long visualMapID,
      bool isGPSUnlocked,
      long csNetID,
      string name)
    {
      this.AccountID = accountID;
      this.VisualMapID = visualMapID;
      this.IsGPSUnlocked = isGPSUnlocked;
      this.CSNetID = csNetID;
      this.Name = name;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("VisualMap_Delete")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE FROM dbo.[CustomerFavorite]\r\nWHERE VisualMapID = @VisualMapID\r\n\r\nDELETE s FROM dbo.[VisualMapSensor] s\r\nWHERE s.VisualMapID = @VisualMapID\r\n\r\nDELETE s FROM dbo.[VisualMapGateway] s\r\nWHERE s.VisualMapID = @VisualMapID\r\n\r\nDELETE s FROM dbo.[VisualMap] s\r\nWHERE s.VisualMapID = @VisualMapID\r\n")]
  internal class VisualMapDelete : BaseDBMethod
  {
    [DBMethodParam("VisualMapID", typeof (long))]
    public long VisualMapID { get; private set; }

    public List<Monnit.VisualMap> Result { get; private set; }

    public VisualMapDelete(long visualMapID)
    {
      this.VisualMapID = visualMapID;
      this.Result = BaseDBObject.Load<Monnit.VisualMap>(this.ToDataTable());
    }
  }
}
