// Decompiled with JetBrains decompiler
// Type: Monnit.AccountPermissionType
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("AccountPermissionType")]
public class AccountPermissionType : BaseDBObject
{
  private long _AccountPermissionTypeID = long.MinValue;
  private string _Name = string.Empty;
  private string _Descripiton = string.Empty;
  private bool _NetworkSpecific = false;
  private bool _RequiresInfo = false;
  private bool _StandardUser = false;
  private bool _AdvancedUser = false;
  private bool _AdminSees = false;
  private bool _ResellerSees = false;
  private bool _CorporateSees = false;

  [DBProp("AccountPermissionTypeID", IsPrimaryKey = true)]
  public long AccountPermissionTypeID
  {
    get => this._AccountPermissionTypeID;
    set => this._AccountPermissionTypeID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Description", MaxLength = 2000)]
  public string Description
  {
    get => this._Descripiton;
    set => this._Descripiton = value;
  }

  [DBProp("NetworkSpecific")]
  public bool NetworkSpecific
  {
    get => this._NetworkSpecific;
    set => this._NetworkSpecific = value;
  }

  [DBProp("RequiresInfo")]
  public bool RequiresInfo
  {
    get => this._RequiresInfo;
    set => this._RequiresInfo = value;
  }

  [DBProp("FreeAccount")]
  public bool StandardUser
  {
    get => this._StandardUser;
    set => this._StandardUser = value;
  }

  [DBProp("PremiumAccount")]
  public bool AdvancedUser
  {
    get => this._AdvancedUser;
    set => this._AdvancedUser = value;
  }

  [DBProp("AdminSees")]
  public bool AdminSees
  {
    get => this._AdminSees;
    set => this._AdminSees = value;
  }

  [DBProp("CorporateSees")]
  public bool CorporateSees
  {
    get => this._CorporateSees;
    set => this._CorporateSees = value;
  }

  public static AccountPermissionType Load(long ID)
  {
    List<AccountPermissionType> accountPermissionTypeList = AccountPermissionType.LoadAll();
    AccountPermissionType accountPermissionType = accountPermissionTypeList.Find((Predicate<AccountPermissionType>) (cpt => cpt.AccountPermissionTypeID == ID));
    if (accountPermissionType == null)
    {
      accountPermissionType = BaseDBObject.Load<AccountPermissionType>(ID);
      if (accountPermissionType != null)
        accountPermissionTypeList.Add(accountPermissionType);
    }
    return accountPermissionType;
  }

  public static AccountPermissionType Find(string name)
  {
    return AccountPermissionType.LoadAll().Find((Predicate<AccountPermissionType>) (cpt => cpt.Name == name));
  }

  public static List<AccountPermissionType> LoadAll()
  {
    string key = "AccountPermissionTypeList";
    List<AccountPermissionType> accountPermissionTypeList = TimedCache.RetrieveObject<List<AccountPermissionType>>(key);
    if (accountPermissionTypeList == null)
    {
      accountPermissionTypeList = BaseDBObject.LoadAll<AccountPermissionType>();
      TimedCache.AddObjectToCach(key, (object) accountPermissionTypeList);
    }
    return accountPermissionTypeList;
  }

  public bool CanEdit(bool isAdmin, bool isCorporateAdmin)
  {
    return this.CorporateSees & isCorporateAdmin || this.AdminSees && isAdmin | isCorporateAdmin;
  }
}
