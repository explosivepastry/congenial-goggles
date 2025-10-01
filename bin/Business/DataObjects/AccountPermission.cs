// Decompiled with JetBrains decompiler
// Type: Monnit.AccountPermission
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("AccountPermission")]
public class AccountPermission : BaseDBObject
{
  private long _AccountPermissionID = long.MinValue;
  private long _AccountID = long.MinValue;
  private long _CSNetID = long.MinValue;
  private bool _Can = false;
  private AccountPermissionType _AccountPermissionType = (AccountPermissionType) null;
  private string _Info = string.Empty;
  private long _AccountPermissionTypeID = long.MinValue;
  private bool _OverrideCustomerPermission = false;

  [DBProp("AccountPermissionID", IsPrimaryKey = true)]
  public long AccountPermissionID
  {
    get => this._AccountPermissionID;
    set => this._AccountPermissionID = value;
  }

  [DBForeignKey("Account", "AccountID")]
  [DBProp("AccountID", AllowNull = false)]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("CSNetID", AllowNull = true)]
  public long CSNetID
  {
    get => this._CSNetID;
    set => this._CSNetID = value;
  }

  [DBProp("Can")]
  public bool Can
  {
    get => this._Can;
    set => this._Can = value;
  }

  [DBProp("Info", MaxLength = 2000)]
  public string Info
  {
    get => this._Info;
    set => this._Info = value;
  }

  [DBProp("AccountPermissionTypeID")]
  [DBForeignKey("AccountPermissionType", "AccountPermissionTypeID")]
  public long AccountPermissionTypeID
  {
    get => this._AccountPermissionTypeID;
    set
    {
      this._AccountPermissionTypeID = value;
      this._AccountPermissionType = (AccountPermissionType) null;
    }
  }

  [DBProp("OverrideCustomerPermission")]
  public bool OverrideCustomerPermission
  {
    get => this._OverrideCustomerPermission;
    set => this._OverrideCustomerPermission = value;
  }

  public AccountPermissionType Type
  {
    get
    {
      if (this._AccountPermissionType == null)
        this._AccountPermissionType = AccountPermissionType.Load(this.AccountPermissionTypeID);
      return this._AccountPermissionType;
    }
  }

  public static AccountPermission Load(long ID) => BaseDBObject.Load<AccountPermission>(ID);

  public static List<AccountPermission> LoadPermissions(long accountID)
  {
    return BaseDBObject.LoadByForeignKey<AccountPermission>("AccountID", (object) accountID);
  }

  public static int IndexOf(List<AccountPermission> list, AccountPermission accountPermission)
  {
    AccountPermissionType type = accountPermission.Type;
    for (int index = 0; index < list.Count; ++index)
    {
      if (list[index].Type.AccountPermissionTypeID == type.AccountPermissionTypeID && (!type.NetworkSpecific || list[index].CSNetID == accountPermission.CSNetID))
        return index;
    }
    return -1;
  }

  public CustomerPermission ToCustomerPermission(long customerID)
  {
    return new CustomerPermission()
    {
      CustomerID = customerID,
      CustomerPermissionTypeID = CustomerPermissionType.Find(this.Type.Name).CustomerPermissionTypeID,
      CSNetID = this.CSNetID,
      Can = this.Can,
      Info = this.Info,
      IsOverriden = true
    };
  }
}
