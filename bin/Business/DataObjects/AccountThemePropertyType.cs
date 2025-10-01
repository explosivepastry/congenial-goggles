// Decompiled with JetBrains decompiler
// Type: Monnit.AccountThemePropertyType
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("AccountThemePropertyType")]
public class AccountThemePropertyType : BaseDBObject
{
  private long _AccountThemePropertyTypeID = long.MinValue;
  private string _Name = string.Empty;
  private string _DefaultValue = string.Empty;
  private bool _AdminAccessOnly = true;

  [DBProp("AccountThemePropertyTypeID", IsPrimaryKey = true)]
  public long AccountThemePropertyTypeID
  {
    get => this._AccountThemePropertyTypeID;
    set => this._AccountThemePropertyTypeID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = false)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("DefaultValue", MaxLength = 255 /*0xFF*/, AllowNull = true, International = true)]
  public string DefaultValue
  {
    get => this._DefaultValue;
    set => this._DefaultValue = value;
  }

  [DBProp("AdminAccessOnly", AllowNull = false)]
  public bool AdminAccessOnly
  {
    get => this._AdminAccessOnly;
    set => this._AdminAccessOnly = value;
  }

  public static List<AccountThemePropertyType> LoadAll()
  {
    List<AccountThemePropertyType> themePropertyTypeList = TimedCache.RetrieveObject<List<AccountThemePropertyType>>("AccountThemePropertyTypes");
    if (themePropertyTypeList == null)
    {
      themePropertyTypeList = BaseDBObject.LoadAll<AccountThemePropertyType>();
      TimedCache.AddObjectToCach("AccountThemePropertyTypes", (object) themePropertyTypeList);
    }
    return themePropertyTypeList;
  }

  public static AccountThemePropertyType Find(string name)
  {
    return AccountThemePropertyType.LoadAll().Where<AccountThemePropertyType>((Func<AccountThemePropertyType, bool>) (pt => pt.Name == name)).FirstOrDefault<AccountThemePropertyType>();
  }

  public static AccountThemePropertyType Find(long accountThemePropertyTypeID)
  {
    return AccountThemePropertyType.LoadAll().Where<AccountThemePropertyType>((Func<AccountThemePropertyType, bool>) (pt => pt.AccountThemePropertyTypeID == accountThemePropertyTypeID)).FirstOrDefault<AccountThemePropertyType>();
  }
}
