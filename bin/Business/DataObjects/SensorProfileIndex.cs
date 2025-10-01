// Decompiled with JetBrains decompiler
// Type: Monnit.SensorProfileIndex
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("SensorProfileIndex")]
public class SensorProfileIndex : BaseDBObject
{
  private long _SensorProfileIndexID = long.MinValue;
  private long _SensorProfileID = long.MinValue;
  private eDataType _DataType;
  private eEncodedType _EncodedType;
  private int _StartIndex = int.MinValue;
  private int _Length = int.MinValue;
  private string _Math = string.Empty;
  private string _PropertyName = string.Empty;

  [DBProp("SensorProfileIndexID", IsPrimaryKey = true)]
  public long SensorProfileIndexID
  {
    get => this._SensorProfileIndexID;
    set => this._SensorProfileIndexID = value;
  }

  [DBProp("SensorProfileID", AllowNull = false)]
  [DBForeignKey("SensorProfile", "SensorProfileID")]
  public long SensorProfileID
  {
    get => this._SensorProfileID;
    set => this._SensorProfileID = value;
  }

  [DBProp("DataType", AllowNull = false)]
  public eDataType DataType
  {
    get => this._DataType;
    set => this._DataType = value;
  }

  [DBProp("EncodedType")]
  public eEncodedType EncodedType
  {
    get => this._EncodedType;
    set => this._EncodedType = value;
  }

  [DBProp("StartIndex")]
  public int StartIndex
  {
    get => this._StartIndex;
    set => this._StartIndex = value;
  }

  [DBProp("Length")]
  public int Length
  {
    get => this._Length;
    set => this._Length = value;
  }

  [DBProp("Math", MaxLength = 255 /*0xFF*/)]
  public string Math
  {
    get => this._Math;
    set
    {
      if (value == null)
        this._Math = string.Empty;
      else
        this._Math = value;
    }
  }

  [DBProp("PropertyName", MaxLength = 255 /*0xFF*/)]
  public string PropertyName
  {
    get => this._PropertyName;
    set
    {
      if (value == null)
        this._PropertyName = string.Empty;
      else
        this._PropertyName = value;
    }
  }

  public static SensorProfileIndex Load(long id) => BaseDBObject.Load<SensorProfileIndex>(id);

  public static List<SensorProfileIndex> LoadAll() => BaseDBObject.LoadAll<SensorProfileIndex>();

  public static List<SensorProfileIndex> LoadBySensorProfileID(long sensorProfileID)
  {
    return new Monnit.Data.SensorProfileIndex.LoadBySensorProfileID(sensorProfileID).Result;
  }
}
