// Decompiled with JetBrains decompiler
// Type: Monnit.TranslationTag
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("TranslationTag")]
public class TranslationTag : BaseDBObject
{
  private long _TranslationTagID = long.MinValue;
  private string _TagIDName = string.Empty;

  [DBProp("TranslationTagID", IsPrimaryKey = true)]
  public long TranslationTagID
  {
    get => this._TranslationTagID;
    set => this._TranslationTagID = value;
  }

  [DBProp("TagIDName", MaxLength = 2000, AllowNull = true)]
  public string TagIDName
  {
    get => this._TagIDName;
    set => this._TagIDName = value;
  }

  public static bool CheckTagExists(string tag)
  {
    if (string.IsNullOrEmpty(tag))
      return true;
    Dictionary<string, long> dictionary = TimedCache.RetrieveObject<Dictionary<string, long>>("AllTranslationTags");
    if (dictionary == null)
    {
      dictionary = new Dictionary<string, long>();
      foreach (TranslationTag translationTag in TranslationTag.LoadAll())
      {
        if (!dictionary.ContainsKey(translationTag.TagIDName))
          dictionary.Add(translationTag.TagIDName, translationTag.TranslationTagID);
      }
      TimedCache.AddObjectToCach("AllTranslationTags", (object) dictionary, new TimeSpan(1, 0, 0));
    }
    return dictionary.ContainsKey(tag);
  }

  private static Dictionary<string, string> LoadCache(long languageID)
  {
    string key = "TranslationsDic_" + languageID.ToString();
    Dictionary<string, string> dictionary = TimedCache.RetrieveObject<Dictionary<string, string>>(key);
    if (dictionary == null)
    {
      dictionary = new Monnit.Data.TranslationTag.CacheByLanguage(languageID).result;
      TimedCache.AddObjectToCach(key, (object) dictionary, new TimeSpan(1, 0, 0));
    }
    return dictionary;
  }

  public static string getTranslation(long languageID, string tag, string english = "")
  {
    try
    {
      Dictionary<string, string> dictionary = TranslationTag.LoadCache(languageID);
      if (dictionary.ContainsKey(tag))
      {
        if (dictionary[tag].Contains("'"))
          dictionary[tag].Replace("'", "'");
        return dictionary[tag];
      }
    }
    catch
    {
    }
    return english;
  }

  public static string getTranslationOrDefault(
    long languageID,
    string tag,
    string defaultText,
    string english = "")
  {
    string translation = TranslationTag.getTranslation(languageID, tag, english);
    if (!string.IsNullOrEmpty(translation))
      return translation;
    if (!string.IsNullOrEmpty(ConfigData.AppSettings("TranslateModeActive")) && ConfigData.AppSettings("TranslateModeActive").ToBool())
    {
      try
      {
        if (!TranslationTag.CheckTagExists(tag))
        {
          if (TranslationTag.LoadByTagIDName(tag) == null)
          {
            new TranslationTag() { TagIDName = tag }.Save();
            TimedCache.RemoveObject("TranslationsDic_" + languageID.ToString());
          }
        }
      }
      catch (Exception ex)
      {
        ex.Log($"TranslationTag Creation Failed. Tag:{tag} ,LanguageID:{languageID.ToString()}, Message: {ex.Message}");
      }
    }
    return defaultText;
  }

  public static TranslationTag Load(long id) => BaseDBObject.Load<TranslationTag>(id);

  public static List<TranslationTag> LoadAll() => BaseDBObject.LoadAll<TranslationTag>();

  public static TranslationTag LoadByTagIDName(string name)
  {
    return BaseDBObject.LoadByForeignKey<TranslationTag>("TagIDName", (object) name).FirstOrDefault<TranslationTag>();
  }

  public static List<TranslationTag> LoadIncompleteByLanguageID(long id)
  {
    return new Monnit.Data.TranslationTag.LoadIncompleteByLanguageID(id).Result;
  }
}
