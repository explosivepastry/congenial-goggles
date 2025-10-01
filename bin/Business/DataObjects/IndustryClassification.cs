// Decompiled with JetBrains decompiler
// Type: Monnit.IndustryClassification
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("IndustryClassification")]
public class IndustryClassification : BaseDBObject
{
  private long _IndustryClassificationID = long.MinValue;
  private string _NAICS = string.Empty;
  private string _Name = string.Empty;
  private long _Parent = long.MinValue;

  [DBProp("IndustryClassificationID", IsPrimaryKey = true)]
  public long IndustryClassificationID
  {
    get => this._IndustryClassificationID;
    set => this._IndustryClassificationID = value;
  }

  [DBProp("NAICS", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string NAICS
  {
    get => this._NAICS;
    set => this._NAICS = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Parent")]
  [DBForeignKey("IndustryClassification", "IndustryClassificationID")]
  public long Parent
  {
    get => this._Parent;
    set => this._Parent = value;
  }

  public static IndustryClassification Load(long id)
  {
    return IndustryClassification.LoadAll().FirstOrDefault<IndustryClassification>((Func<IndustryClassification, bool>) (obj => obj.IndustryClassificationID == id));
  }

  public static List<IndustryClassification> LoadByParent(long? id)
  {
    return IndustryClassification.LoadAll().Where<IndustryClassification>((Func<IndustryClassification, bool>) (obj => obj.Parent == (id ?? long.MinValue))).ToList<IndustryClassification>();
  }

  public static List<IndustryClassification> LoadAll()
  {
    string key = "IndustryClassificationList";
    List<IndustryClassification> industryClassificationList = TimedCache.RetrieveObject<List<IndustryClassification>>(key);
    if (industryClassificationList == null)
    {
      industryClassificationList = BaseDBObject.LoadAll<IndustryClassification>();
      if (industryClassificationList != null)
        TimedCache.AddObjectToCach(key, (object) industryClassificationList, new TimeSpan(6, 0, 0));
    }
    return industryClassificationList;
  }
}
