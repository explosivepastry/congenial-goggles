// Decompiled with JetBrains decompiler
// Type: Monnit.TranslationLanguageLink
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("TranslationLanguageLink")]
public class TranslationLanguageLink : BaseDBObject
{
  private long _TranslationLanguageLinkID = long.MinValue;
  private long _TranslationTagID = long.MinValue;
  private long _LanguageID = long.MinValue;
  private string _Text = string.Empty;

  [DBProp("TranslationLanguageLinkID", IsPrimaryKey = true)]
  public long TranslationLanguageLinkID
  {
    get => this._TranslationLanguageLinkID;
    set => this._TranslationLanguageLinkID = value;
  }

  [DBProp("TranslationTagID")]
  [DBForeignKey("TranslationTag", "TranslationTagID")]
  public long TranslationTagID
  {
    get => this._TranslationTagID;
    set => this._TranslationTagID = value;
  }

  [DBProp("LanguageID")]
  [DBForeignKey("Language", "LanguageID")]
  public long LanguageID
  {
    get => this._LanguageID;
    set => this._LanguageID = value;
  }

  [DBProp("Text", MaxLength = 4000, AllowNull = true, International = true)]
  public string Text
  {
    get => this._Text;
    set => this._Text = value;
  }

  public string EnglishText => TranslationTag.Load(this.TranslationTagID).TagIDName;

  public static TranslationLanguageLink Load(long id)
  {
    return BaseDBObject.Load<TranslationLanguageLink>(id);
  }

  public static List<TranslationLanguageLink> LoadByLanguageID(long id)
  {
    return BaseDBObject.LoadByForeignKey<TranslationLanguageLink>("LanguageID", (object) id);
  }
}
