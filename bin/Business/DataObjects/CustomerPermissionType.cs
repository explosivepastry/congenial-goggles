// Decompiled with JetBrains decompiler
// Type: Monnit.CustomerPermissionType
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("CustomerPermissionType")]
public class CustomerPermissionType : BaseDBObject
{
  private long _CustomerPermissionTypeID = long.MinValue;
  private string _Category = string.Empty;
  private string _Name = string.Empty;
  private string _Descripiton = string.Empty;
  private bool _NetworkSpecific = false;
  private bool _RequiresInfo = false;
  private bool _StandardUser = false;
  private bool _AdvancedUser = false;
  private bool _AdminSees = false;
  private bool _ResellerSees = false;
  private bool _CorporateSees = false;
  private long _RetailAccountID = long.MinValue;

  [DBProp("CustomerPermissionTypeID", IsPrimaryKey = true)]
  public long CustomerPermissionTypeID
  {
    get => this._CustomerPermissionTypeID;
    set => this._CustomerPermissionTypeID = value;
  }

  [DBProp("Category", MaxLength = 255 /*0xFF*/)]
  public string Category
  {
    get => this._Category;
    set => this._Category = value;
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

  [DBProp("StandardUser")]
  public bool StandardUser
  {
    get => this._StandardUser;
    set => this._StandardUser = value;
  }

  [DBProp("AdvancedUser")]
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

  [DBProp("RetailAccountID")]
  [DBForeignKey("Account", "AccountID")]
  public long RetailAccountID
  {
    get => this._RetailAccountID;
    set => this._RetailAccountID = value;
  }

  public static CustomerPermissionType Load(long ID)
  {
    List<CustomerPermissionType> customerPermissionTypeList = CustomerPermissionType.LoadAll();
    CustomerPermissionType customerPermissionType = customerPermissionTypeList.Find((Predicate<CustomerPermissionType>) (cpt => cpt.CustomerPermissionTypeID == ID));
    if (customerPermissionType == null)
    {
      customerPermissionType = BaseDBObject.Load<CustomerPermissionType>(ID);
      if (customerPermissionType != null)
        customerPermissionTypeList.Add(customerPermissionType);
    }
    return customerPermissionType;
  }

  public static CustomerPermissionType Find(string name)
  {
    return CustomerPermissionType.LoadAll().Find((Predicate<CustomerPermissionType>) (cpt => cpt.Name == name));
  }

  public static List<CustomerPermissionType> LoadAll()
  {
    string key = "CustomerPermissionTypeList";
    List<CustomerPermissionType> customerPermissionTypeList = TimedCache.RetrieveObject<List<CustomerPermissionType>>(key);
    if (customerPermissionTypeList == null)
    {
      customerPermissionTypeList = BaseDBObject.LoadAll<CustomerPermissionType>();
      TimedCache.AddObjectToCach(key, (object) customerPermissionTypeList);
    }
    return customerPermissionTypeList;
  }

  public static List<CustomerPermissionType> CanSee(Customer cust)
  {
    List<CustomerPermissionType> customerPermissionTypeList1 = CustomerPermissionType.LoadAll();
    List<CustomerPermissionType> customerPermissionTypeList2 = new List<CustomerPermissionType>();
    foreach (CustomerPermissionType customerPermissionType in customerPermissionTypeList1)
    {
      if (customerPermissionType.AdminSees && cust.IsAdmin)
        customerPermissionTypeList2.Add(customerPermissionType);
      else if (customerPermissionType.CorporateSees && cust.AccountID.ToString() == ConfigData.AppSettings("AdminAccountID"))
        customerPermissionTypeList2.Add(customerPermissionType);
      else if (cust.AccountID.ToString() == ConfigData.AppSettings("AdminAccountID") && ConfigData.AppSettings("IsSuperAdmin") == "true")
        customerPermissionTypeList2.Add(customerPermissionType);
    }
    return customerPermissionTypeList2;
  }

  public bool CanEdit(bool isAdmin, bool isCorporateAdmin)
  {
    return this.CorporateSees & isCorporateAdmin || this.AdminSees && isAdmin | isCorporateAdmin;
  }
}
