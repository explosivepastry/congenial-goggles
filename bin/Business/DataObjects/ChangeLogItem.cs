// Decompiled with JetBrains decompiler
// Type: Monnit.ChangeLogItem
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

#nullable disable
namespace Monnit;

[DBClass("ChangeLogItem")]
public class ChangeLogItem : BaseDBObject
{
  private long _ChangeLogItemID = long.MinValue;
  private long _ChangeLogID = long.MinValue;
  private string _Type = string.Empty;
  private string _Heading = string.Empty;
  private string _Details = string.Empty;

  [DBProp("ChangeLogItemID", IsPrimaryKey = true)]
  public long ChangeLogItemID
  {
    get => this._ChangeLogItemID;
    set => this._ChangeLogItemID = value;
  }

  [DBForeignKey("ChangeLog", "ChangeLogID")]
  [DBProp("ChangeLogID")]
  public long ChangeLogID
  {
    get => this._ChangeLogID;
    set => this._ChangeLogID = value;
  }

  [DBProp("Type", MaxLength = 50, AllowNull = false)]
  public string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [DBProp("Heading", MaxLength = 50, AllowNull = false)]
  public string Heading
  {
    get => this._Heading;
    set => this._Heading = value;
  }

  [AllowHtml]
  [DBProp("Details", MaxLength = 500, AllowNull = false)]
  public string Details
  {
    get => this._Details;
    set => this._Details = value;
  }

  public static ChangeLogItem Load(long id) => BaseDBObject.Load<ChangeLogItem>(id);

  public static List<ChangeLogItem> LoadAll()
  {
    return BaseDBObject.LoadAll<ChangeLogItem>().ToList<ChangeLogItem>();
  }

  public static List<ChangeLogItem> LoadByChangeLog(long id)
  {
    return BaseDBObject.LoadByForeignKey<ChangeLogItem>("ChangeLogID", (object) id);
  }
}
