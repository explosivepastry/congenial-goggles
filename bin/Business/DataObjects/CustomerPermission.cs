// Decompiled with JetBrains decompiler
// Type: Monnit.CustomerPermission
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("CustomerPermission")]
public class CustomerPermission : BaseDBObject
{
  private long _CustomerPermissionID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private long _CSNetID = long.MinValue;
  private bool _CanView = false;
  private CustomerPermissionType _CustomerPermissionType = (CustomerPermissionType) null;
  private string _Info = string.Empty;
  private long _CustomerPermissionTypeID = long.MinValue;
  private bool _IsOverriden = false;

  [DBProp("CustomerPermissionID", IsPrimaryKey = true)]
  public long CustomerPermissionID
  {
    get => this._CustomerPermissionID;
    set => this._CustomerPermissionID = value;
  }

  [DBForeignKey("Customer", "CustomerID")]
  [DBProp("CustomerID", AllowNull = false)]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("CSNetID", AllowNull = true)]
  public long CSNetID
  {
    get => this._CSNetID;
    set => this._CSNetID = value;
  }

  [DBProp("CanView")]
  public bool Can
  {
    get => this._CanView;
    set => this._CanView = value;
  }

  [DBProp("Info", MaxLength = 2000)]
  public string Info
  {
    get => this._Info;
    set => this._Info = value;
  }

  [DBProp("CustomerPermissionTypeID")]
  [DBForeignKey("CustomerPermissionType", "CustomerPermissionTypeID")]
  public long CustomerPermissionTypeID
  {
    get => this._CustomerPermissionTypeID;
    set
    {
      this._CustomerPermissionTypeID = value;
      this._CustomerPermissionType = (CustomerPermissionType) null;
    }
  }

  public CustomerPermissionType Type
  {
    get
    {
      if (this._CustomerPermissionType == null)
        this._CustomerPermissionType = CustomerPermissionType.Load(this.CustomerPermissionTypeID);
      return this._CustomerPermissionType;
    }
  }

  public bool IsOverriden
  {
    get => this._IsOverriden;
    set => this._IsOverriden = value;
  }

  public static CustomerPermission Load(long ID) => BaseDBObject.Load<CustomerPermission>(ID);

  public static List<CustomerPermission> LoadAllByCustomerID(long custID)
  {
    return BaseDBObject.LoadByForeignKey<CustomerPermission>("CustomerID", (object) custID);
  }

  public static List<CustomerPermission> LoadPermissions(Customer customer)
  {
    List<CustomerPermission> result = new Monnit.Data.CustomerPermission.LoadAllByCustomerID(customer.CustomerID).Result;
    if (customer.Account != null)
    {
      foreach (AccountPermission permission in customer.Account.Permissions)
      {
        if (permission.OverrideCustomerPermission)
          CustomerPermission.AddOrReplacePermission(result, permission.ToCustomerPermission(customer.CustomerID));
      }
    }
    return result;
  }

  public static int IndexOf(List<CustomerPermission> list, CustomerPermission customerPermission)
  {
    CustomerPermissionType type = customerPermission.Type;
    for (int index = 0; index < list.Count; ++index)
    {
      if (list[index].Type.CustomerPermissionTypeID == type.CustomerPermissionTypeID && (!type.NetworkSpecific || list[index].CSNetID == customerPermission.CSNetID))
        return index;
    }
    return -1;
  }

  public static void AddOrReplacePermission(
    List<CustomerPermission> list,
    CustomerPermission permission)
  {
    for (int index = CustomerPermission.IndexOf(list, permission); index > -1; index = CustomerPermission.IndexOf(list, permission))
      list.RemoveAt(index);
    list.Add(permission);
  }
}
