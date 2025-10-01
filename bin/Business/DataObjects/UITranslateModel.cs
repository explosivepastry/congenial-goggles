// Decompiled with JetBrains decompiler
// Type: Monnit.UITranslateModel
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

public class UITranslateModel : BaseDBObject
{
  private long _TranslationTagID = long.MinValue;
  private long _TranslationLanguageLinkID = long.MinValue;
  private string _TagIDName = string.Empty;
  private string _Text = string.Empty;

  [DBProp("TranslationTagID")]
  public long TranslationTagID
  {
    get => this._TranslationTagID;
    set => this._TranslationTagID = value;
  }

  [DBProp("TranslationLanguageLinkID", AllowNull = true)]
  public long TranslationLanguageLinkID
  {
    get => this._TranslationLanguageLinkID;
    set => this._TranslationLanguageLinkID = value;
  }

  [DBProp("TagIDName", MaxLength = 2000, AllowNull = true)]
  public string TagIDName
  {
    get => this._TagIDName;
    set => this._TagIDName = value;
  }

  [DBProp("Text", MaxLength = 4000, AllowNull = true, International = true)]
  public string Text
  {
    get => this._Text;
    set => this._Text = value;
  }

  public static List<UITranslateModel> LoadToBeTranslatedByLanguage(long languageID, int count)
  {
    return new Monnit.Data.TranslationTag.LoadToBeTranslatedByLanguage(languageID, count).Result;
  }

  public static List<UITranslateModel> LoadToBeTranslatedByLanguage_Page(
    long languageID,
    int count,
    int page,
    string tagFilter)
  {
    return new Monnit.Data.TranslationTag.LoadToBeTranslatedByLanguage_Page(languageID, count, page, tagFilter).Result;
  }
}
