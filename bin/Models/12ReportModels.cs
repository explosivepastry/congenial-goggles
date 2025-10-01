// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.DatabaseStatistics
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Data;
using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class DatabaseStatistics : BaseDBObject
{
  private string _TableName = string.Empty;
  private string _Type = string.Empty;
  private string _Status = string.Empty;
  private int _Counts = 0;
  private int _Rank = 0;
  private double _MinThresh = 0.0;
  private double _MaxThresh = 0.0;

  [DBProp("TableName", MaxLength = 255 /*0xFF*/)]
  public new string TableName
  {
    get => this._TableName;
    set
    {
      if (value == null)
        this._TableName = string.Empty;
      else
        this._TableName = value;
    }
  }

  [DBProp("Type", MaxLength = 255 /*0xFF*/)]
  public string Type
  {
    get => this._Type;
    set
    {
      if (value == null)
        this._Type = string.Empty;
      else
        this._Type = value;
    }
  }

  [DBProp("Status", MaxLength = 255 /*0xFF*/)]
  public string Status
  {
    get => this._Status;
    set
    {
      if (value == null)
        this._Status = string.Empty;
      else
        this._Status = value;
    }
  }

  [DBProp("Counts", AllowNull = true)]
  public int Counts
  {
    get => this._Counts;
    set => this._Counts = value;
  }

  [DBProp("Rank")]
  public int Rank
  {
    get => this._Rank;
    set => this._Rank = value;
  }

  [DBProp("MinThresh", AllowNull = true)]
  public double MinThresh
  {
    get => this._MinThresh;
    set => this._MinThresh = value;
  }

  [DBProp("MaxThresh", AllowNull = true)]
  public double MaxThresh
  {
    get => this._MaxThresh;
    set => this._MaxThresh = value;
  }

  public static List<DatabaseStatistics> CriticalCheck(
    bool runDetails,
    DateTime startDate,
    DateTime endDate)
  {
    return new DatabaseStatisticsModel.CriticalCheck(runDetails, startDate, endDate).Result;
  }
}
