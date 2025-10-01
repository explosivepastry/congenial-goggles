// Decompiled with JetBrains decompiler
// Type: Monnit.CustomerPreference
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("CustomerPreference")]
public class CustomerPreference : BaseDBObject
{
  private long _CustomerPreferenceID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private string _Name = string.Empty;
  private string _Value = string.Empty;
  private string _PreferenceGroup = string.Empty;
  private string _DisplayName = string.Empty;

  [DBProp("CustomerPreferenceID", IsPrimaryKey = true)]
  public long CustomerPreferenceID
  {
    get => this._CustomerPreferenceID;
    set => this._CustomerPreferenceID = value;
  }

  [DBProp("CustomerID")]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Value", MaxLength = 2000, AllowNull = true)]
  public string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  [DBProp("PreferenceGroup", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string PreferenceGroup
  {
    get => this._PreferenceGroup;
    set => this._PreferenceGroup = value;
  }

  [DBProp("DisplayName", MaxLength = 2000, AllowNull = true)]
  public string DisplayName
  {
    get => this._DisplayName;
    set => this._DisplayName = value;
  }

  public static List<CustomerPreference> LoadByCustomerIDandGroup(long customerID, string group)
  {
    return new Monnit.Data.CustomerPreference.LoadByCustomerIDandGroup(customerID, group).Result;
  }

  public static CustomerPreference LoadByCustomerIDAndGroupAndName(
    long customerID,
    string group,
    string name)
  {
    return new Monnit.Data.CustomerPreference.LoadByCustomerIDAndGroupAndName(customerID, group, name).Result;
  }

  public static CustomerPreference Load(long id) => BaseDBObject.Load<CustomerPreference>(id);

  public static List<CustomerPreference> LoadByCustomerID(long customerID)
  {
    return BaseDBObject.LoadByForeignKey<CustomerPreference>("Customer", (object) customerID);
  }
}
