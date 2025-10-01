// Decompiled with JetBrains decompiler
// Type: Monnit.SeverityLevel
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("SeverityLevel")]
public class SeverityLevel : BaseDBObject
{
  private long _ID = long.MinValue;
  private long _SeverityLevelNumber = long.MinValue;
  private string _SeverityLevelLabel = string.Empty;

  [DBProp("ID", IsPrimaryKey = true)]
  public long ID
  {
    get => this._ID;
    private set => this._ID = value;
  }

  [DBProp("SeverityLevelNumber", AllowNull = false)]
  public long SeverityLevelNumber
  {
    get => this._SeverityLevelNumber;
    private set => this._SeverityLevelNumber = value;
  }

  [DBProp("SeverityLevelLabel", MaxLength = 100, International = true)]
  public string SeverityLevelLabel
  {
    get => this._SeverityLevelLabel;
    private set => this._SeverityLevelLabel = value;
  }

  public static SeverityLevel Find(string label)
  {
    return SeverityLevel.LoadAll().Where<SeverityLevel>((Func<SeverityLevel, bool>) (m => m.SeverityLevelLabel.ToLower() == label.ToLower())).FirstOrDefault<SeverityLevel>();
  }

  public static SeverityLevel Find(long number)
  {
    return SeverityLevel.LoadAll().Where<SeverityLevel>((Func<SeverityLevel, bool>) (m => m.SeverityLevelNumber == number)).FirstOrDefault<SeverityLevel>();
  }

  public static SeverityLevel Load(long id) => BaseDBObject.Load<SeverityLevel>(id);

  public static List<SeverityLevel> LoadAll() => BaseDBObject.LoadAll<SeverityLevel>();
}
