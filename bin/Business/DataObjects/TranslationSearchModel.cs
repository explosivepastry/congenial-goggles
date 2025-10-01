// Decompiled with JetBrains decompiler
// Type: Monnit.TranslationSearchModel
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

public class TranslationSearchModel : BaseDBObject
{
  [DBProp("TranslationLanguageLinkID")]
  public long TranslationLanguageLinkID { get; set; }

  [DBProp("TranslationTagID")]
  public long TranslationTagID { get; set; }

  [DBProp("LanguageID")]
  public long LanguageID { get; set; }

  [DBProp("Text")]
  public string Text { get; set; }

  [DBProp("TagIDName")]
  public string TagIDName { get; set; }

  public static List<TranslationSearchModel> Search(long? languageID, string query)
  {
    return new Monnit.Data.TranslationSearchModel.Search(languageID, query).Result;
  }
}
