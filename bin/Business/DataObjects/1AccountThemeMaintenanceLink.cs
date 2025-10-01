// Decompiled with JetBrains decompiler
// Type: Monnit.Data.AccountThemeMaintenanceLink
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class AccountThemeMaintenanceLink
{
  [DBMethod("AccountThemeMaintenanceLink_LoadByAccountThemeIDAndMaintenanceWindowID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[AccountThemeMaintenanceLink]\r\nWHERE AccountThemeID      = @AccountThemeID \r\n  AND MaintenanceWindowID = @MaintenanceWindowID;\r\n")]
  internal class LoadByAccountThemeIDAndMaintenanceID : BaseDBMethod
  {
    [DBMethodParam("AccountThemeID", typeof (long))]
    public long AccountThemeID { get; private set; }

    [DBMethodParam("MaintenanceWindowID", typeof (long))]
    public long MaintenanceWindowID { get; private set; }

    public Monnit.AccountThemeMaintenanceLink Result { get; private set; }

    public LoadByAccountThemeIDAndMaintenanceID(long accountthemeID, long maintenanceID)
    {
      this.MaintenanceWindowID = maintenanceID;
      this.AccountThemeID = accountthemeID;
      this.Result = BaseDBObject.Load<Monnit.AccountThemeMaintenanceLink>(this.ToDataTable()).FirstOrDefault<Monnit.AccountThemeMaintenanceLink>();
    }
  }
}
