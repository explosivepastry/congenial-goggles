// Decompiled with JetBrains decompiler
// Type: Monnit.Preference
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("Preference")]
public class Preference : BaseDBObject
{
  private long _PreferenceID = long.MinValue;
  private long _PreferenceTypeID = long.MinValue;
  private long _AccountID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private string _Value = string.Empty;

  [DBProp("PreferenceID", IsPrimaryKey = true)]
  public long PreferenceID
  {
    get => this._PreferenceID;
    set => this._PreferenceID = value;
  }

  [DBProp("PreferenceTypeID")]
  [DBForeignKey("PreferenceType", "PreferenceTypeID")]
  public long PreferenceTypeID
  {
    get => this._PreferenceTypeID;
    set => this._PreferenceTypeID = value;
  }

  [DBProp("AccountID")]
  [DBForeignKey("Account", "AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("CustomerID")]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("Value", MaxLength = 2000, AllowNull = true)]
  public string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  public static Preference Load(long id) => BaseDBObject.Load<Preference>(id);

  public static List<Preference> LoadByAccountID(long accountID)
  {
    return BaseDBObject.LoadByForeignKey<Preference>("AccountID", (object) accountID);
  }

  public static List<Preference> LoadByCustomerID(long customerID)
  {
    return BaseDBObject.LoadByForeignKey<Preference>("CustomerID", (object) customerID);
  }

  public static List<Preference> LoadByPreferenceTypeID(long preferenceTypeID)
  {
    return BaseDBObject.LoadByForeignKey<Preference>("PreferenceTypeID", (object) preferenceTypeID);
  }

  public static Preference LoadByPreferenceTypeIDandCustomerID(
    long preferenceTypeID,
    long customerID)
  {
    return BaseDBObject.LoadByForeignKeys<Preference>(new string[2]
    {
      "PreferenceTypeID",
      "CustomerID"
    }, new object[2]
    {
      (object) preferenceTypeID,
      (object) customerID
    }).FirstOrDefault<Preference>();
  }

  public static Preference LoadByPreferenceTypeIDandAccountID(long preferenceTypeID, long accountID)
  {
    return BaseDBObject.LoadByForeignKeys<Preference>(new string[2]
    {
      "PreferenceTypeID",
      "AccountID"
    }, new object[2]
    {
      (object) preferenceTypeID,
      (object) accountID
    }).FirstOrDefault<Preference>();
  }

  public static Dictionary<string, string> LoadPreferences(
    long accountThemeID,
    long accountID,
    long customerID)
  {
    return new Monnit.Data.Preference.LoadPreferences(accountThemeID, accountID, customerID).Result;
  }
}
