// Decompiled with JetBrains decompiler
// Type: Monnit.VisualMapSensor
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("VisualMapSensor")]
public class VisualMapSensor : BaseDBObject
{
  private long _VisualMapSensorID = long.MinValue;
  private long _VisualMapID = long.MinValue;
  private long _SensorID = long.MinValue;
  private double _X = double.MinValue;
  private double _Y = double.MinValue;
  private double _Width = double.MinValue;
  private double _Height = double.MinValue;
  private string _Rotation = string.Empty;
  private int _Altitude = int.MinValue;
  private bool _Selected = true;
  private Sensor _Sensor;

  [DBProp("VisualMapSensorID", IsPrimaryKey = true)]
  public long VisualMapSensorID
  {
    get => this._VisualMapSensorID;
    set => this._VisualMapSensorID = value;
  }

  [DBForeignKey("VisualMap", "VisualMapID")]
  [DBProp("VisualMapID", AllowNull = false)]
  public long VisualMapID
  {
    get => this._VisualMapID;
    set => this._VisualMapID = value;
  }

  [DBForeignKey("Sensor", "SensorID")]
  [DBProp("SensorID", AllowNull = false)]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("X", AllowNull = false)]
  public double X
  {
    get => this._X;
    set => this._X = value;
  }

  [DBProp("Y", AllowNull = false)]
  public double Y
  {
    get => this._Y;
    set => this._Y = value;
  }

  [DBProp("Width")]
  public double Width
  {
    get
    {
      if (this._Width <= 0.0)
        this._Width = 40.0;
      return this._Width;
    }
    set => this._Width = value;
  }

  [DBProp("Height")]
  public double Height
  {
    get => this._Height <= 0.0 ? 40.0 : this._Height;
    set => this._Height = value;
  }

  [DBProp("Rotation", MaxLength = 10, AllowNull = true)]
  public string Rotation
  {
    get => this._Rotation;
    set => this._Rotation = value;
  }

  [DBProp("Altitude")]
  public int Altitude
  {
    get => this._Altitude;
    set => this._Altitude = value;
  }

  public bool Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  public Sensor Sensor
  {
    get
    {
      if (this._Sensor == null)
        this._Sensor = Sensor.Load(this.SensorID);
      return this._Sensor;
    }
    set
    {
      this._Sensor = value;
      this._SensorID = this._Sensor.SensorID;
    }
  }

  public Tuple<string, long> DeviceTypeAndID => new Tuple<string, long>("sensor", this.SensorID);

  public BaseDBObject Device => (BaseDBObject) this.Sensor;

  public static VisualMapSensor Load(long visualMapSensorID)
  {
    return BaseDBObject.Load<VisualMapSensor>(visualMapSensorID);
  }

  public static List<VisualMapSensor> LoadByVisualMapID(long visualMapID)
  {
    return BaseDBObject.LoadByForeignKey<VisualMapSensor>("VisualMapID", (object) visualMapID);
  }
}
