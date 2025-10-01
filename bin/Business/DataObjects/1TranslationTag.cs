// Decompiled with JetBrains decompiler
// Type: Monnit.Data.TranslationTag
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class TranslationTag
{
  [DBMethod("TranslationTag_CacheByLanguage")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n tt.TagIDName,\r\n ll.[Text]\r\nFROM dbo.[TranslationTag] tt\r\nINNER JOIN dbo.[TranslationLanguageLink] ll ON tt.TranslationTagID = ll.TranslationTagID\r\nWHERE LanguageID = @LanguageID \r\n  AND [Text] IS NOT NULL\r\n")]
  internal class CacheByLanguage : BaseDBMethod
  {
    [DBMethodParam("LanguageID", typeof (long))]
    public long LanguageID { get; private set; }

    public Dictionary<string, string> result { get; private set; }

    public CacheByLanguage(long languageid)
    {
      this.LanguageID = languageid;
      DataTable dataTable = this.ToDataTable();
      this.result = new Dictionary<string, string>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        try
        {
          this.result.Add((string) row["TagIDName"], (string) row["Text"]);
        }
        catch (Exception ex)
        {
          ex.Log($"Translation Tag-  Duplicate Translation tag: TagIDName=[\"{row["TagIDName"]}\"], Text=[\"{row["Text"]}\"]");
        }
      }
    }
  }

  [DBMethod("TranslationTag_CheckTagExists")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  COUNT(*)\r\nFROM dbo.[TranslationTag]\r\nWHERE TagIDName = @Tag;\r\n")]
  internal class CheckTagExists : BaseDBMethod
  {
    [DBMethodParam("Tag", typeof (string))]
    public string Tag { get; private set; }

    public bool Result { get; private set; }

    public CheckTagExists(string tag)
    {
      this.Tag = tag;
      this.Result = this.ToScalarValue<int>() != 0;
    }
  }

  [DBMethod("UITranslationTag_LoadToBeTranslatedByLanguage")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP (@Count)\r\n tt.TranslationTagID,\r\n ll.TranslationLanguageLinkID,\r\n tt.TagIDName,\r\n ll.[Text]\r\nFROM dbo.[TranslationTag] tt\r\nLeft JOIN dbo.[TranslationLanguageLink] ll ON tt.TranslationTagID = ll.TranslationTagID\r\nWHERE (ll.LanguageID = @LanguageID OR ll.LanguageID IS NULL )\r\n  AND ll.[Text] IS NULL\r\nORDER BY tt.TranslationTagID;\r\n")]
  internal class LoadToBeTranslatedByLanguage : BaseDBMethod
  {
    [DBMethodParam("LanguageID", typeof (long))]
    public long LanguageID { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public int Count { get; private set; }

    public List<UITranslateModel> Result { get; private set; }

    public LoadToBeTranslatedByLanguage(long languageID, int count)
    {
      this.LanguageID = languageID;
      this.Count = count;
      this.Result = BaseDBObject.Load<UITranslateModel>(this.ToDataTable());
    }
  }

  [DBMethod("UITranslationTag_LoadToBeTranslatedByLanguage_Page")]
  [DBMethodBody(DBMS.SqlServer, "\r\nIF (@Count IS NULL OR @Count < 0)\r\n\tSET @Count = 10\r\n\r\nIF (@Page IS NULL OR @Page < 0)\r\n\tSET @Page = 0\r\n\r\nDECLARE @Offset INT = @Count * @Page;\r\n\r\nIF (@TagFilter IS NULL)\r\n\tSET @TagFilter = ''\r\n\r\nDECLARE @SQL NVARCHAR(500)\r\n\r\nSET @SQL = '\r\nSELECT\r\n tt.TranslationTagID,\r\n ll.TranslationLanguageLinkID,\r\n tt.TagIDName,\r\n ll.[Text]\r\nFROM dbo.[TranslationTag] tt\r\nLeft JOIN dbo.[TranslationLanguageLink] ll ON tt.TranslationTagID = ll.TranslationTagID\r\nWHERE (ll.LanguageID = ' + CONVERT(VARCHAR(10), @LanguageID) + ' OR ll.LanguageID IS NULL )\r\n  AND ll.[Text] IS NULL\r\n  AND TagIDName\t\tLIKE ''%' + @TagFilter + '%''\r\nORDER BY tt.TranslationTagID\r\nOFFSET ' + CONVERT(VARCHAR(10), @Offset) + ' ROWS FETCH NEXT ' + CONVERT(VARCHAR(10), @Count) + ' ROWS ONLY\r\n'\r\n\r\nEXEC(@SQL)\r\n")]
  internal class LoadToBeTranslatedByLanguage_Page : BaseDBMethod
  {
    [DBMethodParam("LanguageID", typeof (long))]
    public long LanguageID { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public int Count { get; private set; }

    [DBMethodParam("Page", typeof (int))]
    public int Page { get; private set; }

    [DBMethodParam("TagFilter", typeof (string))]
    public string TagFilter { get; private set; }

    public List<UITranslateModel> Result { get; private set; }

    public LoadToBeTranslatedByLanguage_Page(
      long languageID,
      int count,
      int page,
      string tagFilter)
    {
      this.LanguageID = languageID;
      this.Count = count;
      this.Page = page;
      this.TagFilter = tagFilter;
      this.ToDataSet();
      this.Result = BaseDBObject.Load<UITranslateModel>(this.ToDataTable());
    }
  }

  [DBMethod("TranslationTag_LoadIncompleteByLanguageID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  tt.*\r\nFROM dbo.[TranslationTag] tt\r\nLEFT JOIN dbo.[TranslationLanguageLink] ll on tt.TranslationTagID = ll.TranslationTagID and ll.LanguageID = @LanguageID\r\nWHERE ll.TranslationLanguageLinkID IS NULL;\r\n")]
  internal class LoadIncompleteByLanguageID : BaseDBMethod
  {
    [DBMethodParam("LanguageID", typeof (long))]
    public long LanguageID { get; private set; }

    public List<Monnit.TranslationTag> Result { get; private set; }

    public LoadIncompleteByLanguageID(long languageID)
    {
      this.LanguageID = languageID;
      this.Result = BaseDBObject.Load<Monnit.TranslationTag>(this.ToDataTable());
    }
  }
}
