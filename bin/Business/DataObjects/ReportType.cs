// Decompiled with JetBrains decompiler
// Type: Monnit.ReportType
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("ReportType")]
public class ReportType : BaseDBObject
{
  private long _ReportTypeID = long.MinValue;
  private string _Name = string.Empty;
  private string _Description = string.Empty;

  [DBProp("ReportTypeID", IsPrimaryKey = true)]
  public long ReportTypeID
  {
    get => this._ReportTypeID;
    set => this._ReportTypeID = value;
  }

  [DBProp("Name", MaxLength = 50)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Description", MaxLength = 4000)]
  public string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  public static ReportType Load(long ID) => BaseDBObject.Load<ReportType>(ID);

  public static List<ReportType> LoadAll() => BaseDBObject.LoadAll<ReportType>();

  public static List<ReportType> LoadByAccount(
    long accountThemeID,
    long accountID,
    bool isPremiere)
  {
    return new Monnit.Data.ReportType.LoadByAccount(accountThemeID, accountID, isPremiere).Result;
  }
}
