// Decompiled with JetBrains decompiler
// Type: Monnit.SensorIDSequence
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit;

[DBClass("SensorIDSequence")]
public class SensorIDSequence : BaseDBObject
{
  private long _SensorIDSequenceID = long.MinValue;
  private DateTime _CreatedOnDate = DateTime.MinValue;

  [DBProp("SensorIDSequenceID", IsPrimaryKey = true)]
  public long SensorIDSequenceID
  {
    get => this._SensorIDSequenceID;
    set => this._SensorIDSequenceID = value;
  }

  [DBProp("CreatedOnDate")]
  public DateTime CreatedOnDate
  {
    get => this._CreatedOnDate;
    set => this._CreatedOnDate = value;
  }

  public static SensorIDSequence Load(long id) => BaseDBObject.Load<SensorIDSequence>(id);
}
