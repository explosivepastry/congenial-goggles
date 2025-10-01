// Decompiled with JetBrains decompiler
// Type: Monnit.PreferenceType
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("PreferenceType")]
public class PreferenceType : BaseDBObject
{
  private long _PreferenceTypeID = long.MinValue;
  private string _Name = string.Empty;
  private string _DefaultValue = string.Empty;
  private string _ValueType = string.Empty;
  private string _DisplayName = string.Empty;
  private List<PreferenceTypeOption> _Options;

  [DBProp("PreferenceTypeID", IsPrimaryKey = true)]
  public long PreferenceTypeID
  {
    get => this._PreferenceTypeID;
    set => this._PreferenceTypeID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("DefaultValue", MaxLength = 2000, AllowNull = true)]
  public string DefaultValue
  {
    get => this._DefaultValue;
    set => this._DefaultValue = value;
  }

  [DBProp("ValueType", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string ValueType
  {
    get => this._ValueType;
    set => this._ValueType = value;
  }

  [DBProp("DisplayName", MaxLength = 2000, AllowNull = true)]
  public string DisplayName
  {
    get => this._DisplayName;
    set => this._DisplayName = value;
  }

  public List<PreferenceTypeOption> Options
  {
    get
    {
      if (this._Options == null)
        this._Options = PreferenceTypeOption.LoadByPreferenceTypeID(this.PreferenceTypeID);
      return this._Options;
    }
  }

  public static PreferenceType Find(string name)
  {
    return PreferenceType.LoadAll().Where<PreferenceType>((Func<PreferenceType, bool>) (pt => pt.Name.ToLower().Replace(" ", "") == name.ToLower().Replace(" ", ""))).FirstOrDefault<PreferenceType>();
  }

  public static List<PreferenceType> LoadAll()
  {
    List<PreferenceType> preferenceTypeList = TimedCache.RetrieveObject<List<PreferenceType>>("PreferenceTypes");
    if (preferenceTypeList == null || preferenceTypeList.Count == 0)
    {
      preferenceTypeList = BaseDBObject.LoadAll<PreferenceType>();
      TimedCache.AddObjectToCach("AccountThemes", (object) preferenceTypeList);
    }
    return preferenceTypeList;
  }

  public static List<PreferenceType> LoadUserAllowedByAccountThemeID(long accountThemeID)
  {
    return new Monnit.Data.PreferenceType.LoadUserAllowedByAccountThemeID(accountThemeID).Result;
  }

  public static List<PreferenceType> LoadAccountAllowedByAccountThemeID(long accountThemeID)
  {
    return new Monnit.Data.PreferenceType.LoadAccountAllowedByAccountThemeID(accountThemeID).Result;
  }
}
