// Decompiled with JetBrains decompiler
// Type: Monnit.ReportParameterType
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("ReportParameterType")]
public class ReportParameterType : BaseDBObject
{
  private long _ReportParameterTypeID = long.MinValue;
  private string _Name = string.Empty;
  private string _Description = string.Empty;
  private string _UIHint = string.Empty;
  private string _ValueType = string.Empty;

  [DBProp("ReportParameterTypeID", IsPrimaryKey = true)]
  public long ReportParameterTypeID
  {
    get => this._ReportParameterTypeID;
    set => this._ReportParameterTypeID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Description", MaxLength = 2000, AllowNull = true)]
  public string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [DBProp("UIHint", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string UIHint
  {
    get => this._UIHint;
    set => this._UIHint = value;
  }

  [DBProp("ValueType", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string ValueType
  {
    get => this._ValueType;
    set => this._ValueType = value;
  }

  public static ReportParameterType Load(long id) => BaseDBObject.Load<ReportParameterType>(id);

  public static List<ReportParameterType> LoadAll() => BaseDBObject.LoadAll<ReportParameterType>();
}
