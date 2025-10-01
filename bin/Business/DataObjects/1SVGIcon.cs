// Decompiled with JetBrains decompiler
// Type: Monnit.Data.SVGIcon
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class SVGIcon
{
  [DBMethod("SVGIcon_LoadByKeyAndTheme")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP 1 i.*\r\nFROM dbo.[SVGIcon] i WITH (NOLOCK)\r\nWHERE i.ImageKey = @ImageKey\r\n  AND (i.AccountThemeID = @AccountThemeID OR IsDefault = 1)\r\nORDER BY\r\n  IsDefault ASC;\r\n")]
  internal class LoadByKeyAndTheme : BaseDBMethod
  {
    [DBMethodParam("ImageKey", typeof (string))]
    public string ImageKey { get; private set; }

    [DBMethodParam("AccountThemeID", typeof (long))]
    public long AccountThemeID { get; private set; }

    public Monnit.SVGIcon Result { get; private set; }

    public LoadByKeyAndTheme(string imageKey, long accountThemeID)
    {
      this.ImageKey = imageKey;
      this.AccountThemeID = accountThemeID;
      this.Result = new Monnit.SVGIcon();
      DataTable dataTable = this.ToDataTable();
      if (dataTable == null || dataTable.Rows.Count <= 0)
        return;
      this.Result.Load(dataTable.Rows[0]);
    }
  }

  [DBMethod("SVGIcon_LoadByTheme")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  SVGIconID      = ISNULL(t.SVGIconID, i.SVGIconID),\r\n  Name           = ISNULL(t.Name, i.Name),\r\n  ImageKey       = ISNULL(t.ImageKey, i.ImageKey),\r\n  Category       = ISNULL(t.Category, i.Category),\r\n  HTMLCode       = ISNULL(t.HTMLCode, i.HTMLCode),\r\n  AccountThemeID = ISNULL(t.AccountThemeID, i.AccountThemeID),\r\n  IsDefault      = ISNULL(t.IsDefault, i.IsDefault)\r\nFROM dbo.[SVGIcon] i WITH (NOLOCK)\r\nLEFT JOIN \r\n( SELECT\r\n    i.SVGIconID,\r\n    i.Name,\r\n    i.ImageKey,\r\n    i.Category,\r\n    i.HTMLCode,\r\n    i.AccountThemeID,\r\n    i.IsDefault\r\n  FROM dbo.[SVGIcon] i WITH (NOLOCK)\r\n  WHERE (i.AccountThemeID = @AccountThemeID)) t ON i.ImageKey = t.Imagekey\r\nWHERE i.AccountThemeID IS NULL\r\nORDER BY ImageKey;\r\n")]
  internal class LoadByTheme : BaseDBMethod
  {
    [DBMethodParam("AccountThemeID", typeof (long))]
    public long AccountThemeID { get; private set; }

    public List<Monnit.SVGIcon> Result { get; private set; }

    public LoadByTheme(long accountThemeID)
    {
      this.AccountThemeID = accountThemeID;
      this.Result = BaseDBObject.Load<Monnit.SVGIcon>(this.ToDataTable());
    }
  }
}
