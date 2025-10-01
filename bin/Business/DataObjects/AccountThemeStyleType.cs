// Decompiled with JetBrains decompiler
// Type: Monnit.AccountThemeStyleType
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("AccountThemeStyleType")]
public class AccountThemeStyleType : BaseDBObject
{
  private long _AccountThemeStyleTypeID = long.MinValue;
  private string _Name = string.Empty;
  private string _Property = string.Empty;
  private string _DataType = string.Empty;
  private string _DefaultValue = string.Empty;
  private byte[] _DefaultBinaryValue = (byte[]) null;
  private int _DisplayOrder = int.MinValue;

  [DBProp("AccountThemeStyleTypeID", IsPrimaryKey = true)]
  public long AccountThemeStyleTypeID
  {
    get => this._AccountThemeStyleTypeID;
    set => this._AccountThemeStyleTypeID = value;
  }

  [DBProp("Name", MaxLength = 300, AllowNull = false)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Property", MaxLength = 100, AllowNull = false)]
  public string Property
  {
    get => this._Property;
    set => this._Property = value;
  }

  [DBProp("DataType", MaxLength = 500, AllowNull = false)]
  public string DataType
  {
    get => this._DataType;
    set => this._DataType = value;
  }

  [DBProp("DefaultValue", MaxLength = 500, AllowNull = true)]
  public string DefaultValue
  {
    get => this._DefaultValue;
    set => this._DefaultValue = value;
  }

  [DBProp("DefaultBinaryValue", AllowNull = true)]
  public byte[] DefaultBinaryValue
  {
    get => this._DefaultBinaryValue;
    set => this._DefaultBinaryValue = value;
  }

  [DBProp("DisplayOrder")]
  public int DisplayOrder
  {
    get => this._DisplayOrder;
    set => this._DisplayOrder = value;
  }

  public static AccountThemeStyleType Load(long id)
  {
    Dictionary<long, AccountThemeStyleType> dictionary = TimedCache.RetrieveObject<Dictionary<long, AccountThemeStyleType>>("AccountThemeStyleTypeList");
    if (dictionary == null)
    {
      dictionary = new Dictionary<long, AccountThemeStyleType>();
      foreach (AccountThemeStyleType accountThemeStyleType in AccountThemeStyleType.LoadAll())
        dictionary.Add(accountThemeStyleType.AccountThemeStyleTypeID, accountThemeStyleType);
      TimedCache.AddObjectToCach("AccountThemeStyleTypeList", (object) dictionary);
    }
    return dictionary.ContainsKey(id) ? dictionary[id] : (AccountThemeStyleType) null;
  }

  public static List<AccountThemeStyleType> LoadAll()
  {
    return BaseDBObject.LoadAll<AccountThemeStyleType>();
  }
}
