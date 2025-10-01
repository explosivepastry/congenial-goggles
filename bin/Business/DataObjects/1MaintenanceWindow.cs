// Decompiled with JetBrains decompiler
// Type: Monnit.Data.MaintenanceWindow
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class MaintenanceWindow
{
  [DBMethod("MaintenanceWindow_LoadActive")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP 1\r\n  *\r\nFROM dbo.[MaintenanceWindow]\r\nWHERE DisplayDate < GETUTCDATE()\r\n  AND HideDate    > GETUTCDATE()\r\nORDER BY DisplayDate;\r\n")]
  internal class LoadActive : BaseDBMethod
  {
    public Monnit.MaintenanceWindow Result { get; private set; }

    public LoadActive()
    {
      this.Result = BaseDBObject.Load<Monnit.MaintenanceWindow>(this.ToDataTable()).FirstOrDefault<Monnit.MaintenanceWindow>();
    }

    public static Monnit.MaintenanceWindow Exec() => new MaintenanceWindow.LoadActive().Result;
  }

  [DBMethod("MaintenanceWindow_LoadFuture")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[MaintenanceWindow]\r\nWHERE HideDate > GETUTCDATE()\r\nORDER BY DisplayDate;\r\n")]
  internal class LoadFuture : BaseDBMethod
  {
    public List<Monnit.MaintenanceWindow> Result { get; private set; }

    public LoadFuture() => this.Result = BaseDBObject.Load<Monnit.MaintenanceWindow>(this.ToDataTable());

    public static List<Monnit.MaintenanceWindow> Exec()
    {
      return new MaintenanceWindow.LoadFuture().Result;
    }
  }
}
