// Decompiled with JetBrains decompiler
// Type: Monnit.SVGIcon
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("SVGIcon")]
public class SVGIcon : BaseDBObject
{
  private long _SVGIconID = long.MinValue;
  private string _Name = string.Empty;
  private string _ImageKey = string.Empty;
  private string _Category = string.Empty;
  private string _HTMLCode = string.Empty;
  private long _AccountThemeID = long.MinValue;
  private bool _IsDefault = true;
  private bool _ApplyTheme = false;

  [Required]
  [DBProp("SVGIconID", IsPrimaryKey = true)]
  public long SVGIconID
  {
    get => this._SVGIconID;
    set => this._SVGIconID = value;
  }

  [Required]
  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  [DisplayName("What's My Name?")]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [Required(ErrorMessage = "Image Key is required")]
  [DBProp("ImageKey", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string ImageKey
  {
    get => this._ImageKey;
    set => this._ImageKey = value;
  }

  [Required]
  [DBProp("Category", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Category
  {
    get => this._Category;
    set => this._Category = value;
  }

  [Required]
  [DBProp("HTMLCode", MaxLength = 2147483647 /*0x7FFFFFFF*/, AllowNull = true)]
  public string HTMLCode
  {
    get => this._HTMLCode;
    set => this._HTMLCode = value;
  }

  [DBProp("AccountThemeID")]
  [DBForeignKey("AccountTheme", "AccountThemeID")]
  public long AccountThemeID
  {
    get => this._AccountThemeID;
    set => this._AccountThemeID = value;
  }

  [DBProp("IsDefault")]
  public bool IsDefault
  {
    get => this._IsDefault;
    set => this._IsDefault = value;
  }

  public bool ApplyTheme
  {
    get => this._ApplyTheme;
    set => this._ApplyTheme = value;
  }

  public static SVGIcon Load(long id) => BaseDBObject.Load<SVGIcon>(id);

  public static SVGIcon Load(string imageKey, long accountThemeID)
  {
    return SVGIcon.LoadForTheme(accountThemeID).Where<SVGIcon>((Func<SVGIcon, bool>) (t => t.ImageKey == imageKey)).FirstOrDefault<SVGIcon>();
  }

  public static List<SVGIcon> LoadForTheme(long accountThemeID)
  {
    string key = $"SVGIconList_ThemeID{accountThemeID}";
    List<SVGIcon> svgIconList = TimedCache.RetrieveObject<List<SVGIcon>>(key);
    if (svgIconList == null)
    {
      svgIconList = new Monnit.Data.SVGIcon.LoadByTheme(accountThemeID).Result;
      if (svgIconList != null)
        TimedCache.AddObjectToCach(key, (object) svgIconList, new TimeSpan(1, 0, 0));
    }
    return svgIconList;
  }
}
