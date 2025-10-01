// Decompiled with JetBrains decompiler
// Type: Monnit.CustomerInformationType
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("CustomerInformationType")]
public class CustomerInformationType : BaseDBObject
{
  private long _CustomerInformationTypeID = long.MinValue;
  private string _Name = string.Empty;
  private string _Validation = string.Empty;
  private eCustomerInformationType _Type;

  [DBProp("CustomerInformationTypeID", IsPrimaryKey = true)]
  public long CustomerInformationTypeID
  {
    get => this._CustomerInformationTypeID;
    set => this._CustomerInformationTypeID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Validation", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Validation
  {
    get => this._Validation;
    set => this._Validation = value;
  }

  [DBProp("Type", AllowNull = true)]
  public eCustomerInformationType Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  public static List<CustomerInformationType> LoadAll()
  {
    string key = "CustomerInformationTypeList";
    List<CustomerInformationType> customerInformationTypeList = TimedCache.RetrieveObject<List<CustomerInformationType>>(key);
    if (customerInformationTypeList == null)
    {
      customerInformationTypeList = BaseDBObject.LoadAll<CustomerInformationType>();
      TimedCache.AddObjectToCach(key, (object) customerInformationTypeList);
    }
    return customerInformationTypeList;
  }

  public static CustomerInformationType Find(string name)
  {
    return CustomerInformationType.LoadAll().Find((Predicate<CustomerInformationType>) (cpt => cpt.Name == name));
  }

  public static CustomerInformationType Load(long id)
  {
    List<CustomerInformationType> customerInformationTypeList = CustomerInformationType.LoadAll();
    CustomerInformationType customerInformationType = customerInformationTypeList.Find((Predicate<CustomerInformationType>) (cpt => cpt.CustomerInformationTypeID == id));
    if (customerInformationType == null)
    {
      customerInformationType = BaseDBObject.Load<CustomerInformationType>(id);
      if (customerInformationType != null)
        customerInformationTypeList.Add(customerInformationType);
    }
    return customerInformationType;
  }
}
