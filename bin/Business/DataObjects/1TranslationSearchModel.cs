// Decompiled with JetBrains decompiler
// Type: Monnit.Data.TranslationSearchModel
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class TranslationSearchModel
{
  [DBMethod("TranslationSearchModel_Search")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n l.*,\r\n t.TagIDName\r\nFROM dbo.[TranslationLanguageLink] l WITH (NOLOCK)\r\nINNER JOIN TranslationTag t ON l.TranslationTagID = t.TranslationTagID\r\nWHERE t.[TagIDName] LIKE '%'+@Query+'%'\r\n  AND ISNULL(@LanguageID, l.LanguageID) = l.LanguageID\r\n")]
  internal class Search : BaseDBMethod
  {
    [DBMethodParam("LanguageID", typeof (long))]
    public long LanguageID { get; private set; }

    [DBMethodParam("Query", typeof (string))]
    public string Query { get; private set; }

    public List<Monnit.TranslationSearchModel> Result { get; private set; }

    public Search(long? languageID, string query)
    {
      this.LanguageID = languageID ?? long.MinValue;
      this.Query = query;
      this.Result = BaseDBObject.Load<Monnit.TranslationSearchModel>(this.ToDataTable());
    }
  }
}
