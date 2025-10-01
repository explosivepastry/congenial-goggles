// Decompiled with JetBrains decompiler
// Type: Monnit.SensorGroupSensor
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("SensorGroupSensor")]
public class SensorGroupSensor : BaseDBObject
{
  private long _SensorGroupSensorID = long.MinValue;
  private long _SensorGroupID = long.MinValue;
  private long _SensorID = long.MinValue;
  private int _Position = int.MinValue;
  private int _DatumIndex = int.MinValue;
  private string _Alias = string.Empty;
  private Sensor _Sensor = (Sensor) null;

  [DBProp("SensorGroupSensorID", IsPrimaryKey = true)]
  public long SensorGroupSensorID
  {
    get => this._SensorGroupSensorID;
    set => this._SensorGroupSensorID = value;
  }

  [DBProp("SensorGroupID")]
  [DBForeignKey("SensorGroup", "SensorGroupID")]
  public long SensorGroupID
  {
    get => this._SensorGroupID;
    set => this._SensorGroupID = value;
  }

  [DBProp("SensorID")]
  [DBForeignKey("Sensor", "SensorID")]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("Position")]
  public int Position
  {
    get => this._Position;
    set => this._Position = value;
  }

  [DBProp("DatumIndex", AllowNull = true)]
  public int DatumIndex
  {
    get => this._DatumIndex;
    set => this._DatumIndex = value;
  }

  [DBProp("Alias", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Alias
  {
    get => this._Alias;
    set => this._Alias = value;
  }

  public Sensor Sensor
  {
    get
    {
      if (this._Sensor == null)
        this._Sensor = Sensor.Load(this.SensorID);
      return this._Sensor;
    }
    set => this._Sensor = value;
  }

  public static SensorGroupSensor Load(long id) => BaseDBObject.Load<SensorGroupSensor>(id);

  public static List<SensorGroupSensor> LoadBySensor(long sensorId)
  {
    return BaseDBObject.LoadByForeignKey<SensorGroupSensor>("SensorID", (object) sensorId);
  }

  public static SensorGroupSensor LoadBySensorID(long sensorId)
  {
    return BaseDBObject.LoadByForeignKey<SensorGroupSensor>("SensorID", (object) sensorId).FirstOrDefault<SensorGroupSensor>();
  }

  public static List<SensorGroupSensor> LoadBySensorGroupID(long sensorGroupID)
  {
    return BaseDBObject.LoadByForeignKey<SensorGroupSensor>("SensorGroupID", (object) sensorGroupID);
  }

  public static List<SensorGroup> LoadByParentID(long parentID)
  {
    return new Monnit.Data.SensorGroup.LoadByParentID(parentID).Result;
  }
}
