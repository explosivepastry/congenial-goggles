// Decompiled with JetBrains decompiler
// Type: Monnit.Cable
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("Cable")]
public class Cable : BaseDBObject
{
  private long _CableID = long.MinValue;
  private long _SensorID = long.MinValue;
  private DateTime _CreateDate = DateTime.UtcNow;
  private string _SKU = string.Empty;
  private long _ApplicationID = long.MinValue;
  private int _CableMajorRevision = int.MinValue;
  private int _CableMinorRevision = int.MinValue;

  [DBProp("CableID", IsPrimaryKey = true)]
  public long CableID
  {
    get => this._CableID;
    set => this._CableID = value;
  }

  [DBForeignKey("Sensor", "SensorID")]
  [DBProp("SensorID", AllowNull = true)]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("CreateDate", AllowNull = false)]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("SKU", MaxLength = 255 /*0xFF*/, AllowNull = false)]
  public string SKU
  {
    get => this._SKU;
    set => this._SKU = value;
  }

  [DBForeignKey("Application", "ApplicationID")]
  [DBProp("ApplicationID", AllowNull = false)]
  public long ApplicationID
  {
    get => this._ApplicationID;
    set => this._ApplicationID = value;
  }

  [DBProp("CableMajorRevision")]
  public int CableMajorRevision
  {
    get => this._CableMajorRevision;
    set => this._CableMajorRevision = value;
  }

  [DBProp("CableMinorRevision")]
  public int CableMinorRevision
  {
    get => this._CableMinorRevision;
    set => this._CableMinorRevision = value;
  }

  public static Cable Load(long ID) => BaseDBObject.Load<Cable>(ID);

  public static List<Cable> LoadAll() => BaseDBObject.LoadAll<Cable>();

  public void ForceInsert()
  {
    Monnit.Data.Cable.ForceInsert forceInsert = new Monnit.Data.Cable.ForceInsert(this);
  }
}
