// Decompiled with JetBrains decompiler
// Type: Monnit.MaintenanceWindowCustomer
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("MaintenanceWindowCustomer")]
public class MaintenanceWindowCustomer : BaseDBObject
{
  private long _MaintenanceWindowCustomerID = long.MinValue;
  private long _MaintenanceWindowID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private bool _Acknowledged = false;
  private DateTime _CreateDate = DateTime.MinValue;
  private DateTime _ModifiedDate = DateTime.MinValue;
  private eMaintenanceWindowMessageStatus _Status = eMaintenanceWindowMessageStatus.pending;
  private int _AttemptCount = 1;
  private eMaintenanceWindowCustomerType _Type = eMaintenanceWindowCustomerType.Email;

  [DBProp("MaintenanceWindowCustomerID", IsPrimaryKey = true)]
  public long MaintenanceWindowCustomerID
  {
    get => this._MaintenanceWindowCustomerID;
    set => this._MaintenanceWindowCustomerID = value;
  }

  [DBProp("MaintenanceWindowID")]
  [DBForeignKey("MaintenanceWindow", "MaintenanceWindowID")]
  public long MaintenanceWindowID
  {
    get => this._MaintenanceWindowID;
    set => this._MaintenanceWindowID = value;
  }

  [DBProp("CustomerID")]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("Acknowledged")]
  public bool Acknowledged
  {
    get => this._Acknowledged;
    set => this._Acknowledged = value;
  }

  [DBProp("CreateDate")]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("ModifiedDate")]
  public DateTime ModifiedDate
  {
    get => this._ModifiedDate;
    set => this._ModifiedDate = value;
  }

  [DBProp("Status", AllowNull = true, DefaultValue = 1)]
  public eMaintenanceWindowMessageStatus Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [DBProp("AttemptCount", AllowNull = true, DefaultValue = 1)]
  public int AttemptCount
  {
    get => this._AttemptCount;
    set => this._AttemptCount = value;
  }

  [DBProp("Type", AllowNull = false, DefaultValue = 1)]
  public eMaintenanceWindowCustomerType Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  public static MaintenanceWindowCustomer Load(long id)
  {
    return BaseDBObject.Load<MaintenanceWindowCustomer>(id);
  }

  public static List<MaintenanceWindowCustomer> LoadByWindow(long maintenanceWindowID)
  {
    return BaseDBObject.LoadByForeignKey<MaintenanceWindowCustomer>("MaintenanceWindowID", (object) maintenanceWindowID);
  }

  public static MaintenanceWindowCustomer LoadByWindowAndCustomerAndType(
    long maintenanceWindowID,
    long customerID,
    eMaintenanceWindowCustomerType type)
  {
    return BaseDBObject.LoadByForeignKeys<MaintenanceWindowCustomer>(new string[3]
    {
      "MaintenanceWindowID",
      "CustomerID",
      "Type"
    }, new object[3]
    {
      (object) maintenanceWindowID,
      (object) customerID,
      (object) type
    }).FirstOrDefault<MaintenanceWindowCustomer>();
  }

  public static List<Customer> LoadCustomersToNotify(long maintenanceWindowID)
  {
    return Monnit.Data.MaintenanceWindowCustomer.LoadCustomersToNotify.Exec(maintenanceWindowID);
  }

  public static void UpdateRecord(
    long maintenanceWindowCustomerID,
    eMaintenanceWindowMessageStatus status)
  {
    MaintenanceWindowCustomer maintenanceWindowCustomer = MaintenanceWindowCustomer.Load(maintenanceWindowCustomerID);
    maintenanceWindowCustomer.Status = status;
    maintenanceWindowCustomer.Save();
  }
}
