// Decompiled with JetBrains decompiler
// Type: Monnit.Data.MaintenanceWindowCustomer
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class MaintenanceWindowCustomer
{
  [DBMethod("MaintenanceWindowCustomer_LoadCustomersToNotify")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM(\r\n      SELECT \r\n        rownumber = ROW_NUMBER() OVER (ORDER BY c.CustomerID),\r\n        c.* \r\n      FROM dbo.[Customer] c WITH (NOLOCK)\r\n      LEFT JOIN dbo.[MaintenanceWindowCustomer] mwc WITH (NOLOCK) ON mwc.CustomerID = c.CustomerID AND mwc.MaintenanceWindowID = @MaintenanceWindowID\r\n      WHERE mwc.MaintenanceWindowCustomerID IS NULL\r\n        AND c.IsActive  = 1 \r\n        AND c.IsDeleted = 0\r\n    ) AS temp\r\nWHERE rownumber <= 600;\r\n")]
  internal class LoadCustomersToNotify : BaseDBMethod
  {
    [DBMethodParam("MaintenanceWindowID", typeof (long))]
    public long MaintenanceWindowID { get; private set; }

    public List<Monnit.Customer> Result { get; private set; }

    public LoadCustomersToNotify(long maintenanceWindowID)
    {
      this.MaintenanceWindowID = maintenanceWindowID;
      this.Result = BaseDBObject.Load<Monnit.Customer>(this.ToDataTable());
    }

    public static List<Monnit.Customer> Exec(long maintenanceWindowID)
    {
      return new MaintenanceWindowCustomer.LoadCustomersToNotify(maintenanceWindowID).Result;
    }
  }

  [DBMethod("MaintenanceWindowCustomer_UpdateRecordStatus")]
  [DBMethodBody(DBMS.SqlServer, "\r\n        BEGIN TRAN\r\n        \r\n          UPDATE dbo.[MaintenanceWindowCustomer]\r\n            SET [Status] = @Status\r\n          WHERE MaintenanceWindowCustomerID = @MaintenanceWindowCustomerID\r\n        \r\n        COMMIT TRAN\r\n        ")]
  internal class UpdateRecordStatus : BaseDBMethod
  {
    [DBMethodParam("MaintenanceWindowCustomerID", typeof (long))]
    public long MaintenanceWindowCustomerID { get; private set; }

    [DBMethodParam("Status", typeof (eMaintenanceWindowMessageStatus))]
    public eMaintenanceWindowMessageStatus Status { get; private set; }

    public UpdateRecordStatus(
      long maintenanceWindowCustomerID,
      eMaintenanceWindowMessageStatus status)
    {
      this.MaintenanceWindowCustomerID = maintenanceWindowCustomerID;
      this.Status = status;
      this.Execute();
    }
  }
}
