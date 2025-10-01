// Decompiled with JetBrains decompiler
// Type: Monnit.Language
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("Language")]
public class Language : BaseDBObject
{
  private long _LanguageID = long.MinValue;
  private string _Name = string.Empty;
  private string _LanguageAttribute = string.Empty;
  private bool _IsActive = false;
  private string _DisplayName = string.Empty;
  public const long EnglishID = 1;

  [DBProp("LanguageID", IsPrimaryKey = true)]
  public long LanguageID
  {
    get => this._LanguageID;
    set => this._LanguageID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("LanguageAttribute", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string LanguageAttribute
  {
    get => this._LanguageAttribute;
    set => this._LanguageAttribute = value;
  }

  [DBProp("IsActive")]
  public bool IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [DBProp("DisplayName", MaxLength = 255 /*0xFF*/, AllowNull = true, International = true)]
  public string DisplayName
  {
    get => this._DisplayName;
    set => this._DisplayName = value;
  }

  public override string ToString() => this.Name;

  public static Language Load(long id) => BaseDBObject.Load<Language>(id);

  public static List<Language> LoadAll() => BaseDBObject.LoadAll<Language>();

  public static List<Language> LoadActive() => new Monnit.Data.Language.LoadActive().Result;

  public static Language LoadByName(string name)
  {
    return Language.LoadAll().Where<Language>((Func<Language, bool>) (lang => lang.Name.ToLower() == name.ToLower())).FirstOrDefault<Language>();
  }
}
