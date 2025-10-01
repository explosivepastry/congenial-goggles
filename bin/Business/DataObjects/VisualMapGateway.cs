// Decompiled with JetBrains decompiler
// Type: Monnit.VisualMapGateway
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("VisualMapGateway")]
public class VisualMapGateway : BaseDBObject
{
  private const string deviceType = "gateway";
  private long _VisualMapGatewayID = long.MinValue;
  private long _VisualMapID = long.MinValue;
  private long _GatewayID = long.MinValue;
  private double _X = double.MinValue;
  private double _Y = double.MinValue;
  private double _Width = double.MinValue;
  private double _Height = double.MinValue;
  private string _Rotation = string.Empty;
  private int _Altitude = int.MinValue;
  private bool _Selected = true;
  private Gateway _Gateway;

  [DBProp("VisualMapGatewayID", IsPrimaryKey = true)]
  public long VisualMapGatewayID
  {
    get => this._VisualMapGatewayID;
    set => this._VisualMapGatewayID = value;
  }

  [DBForeignKey("VisualMap", "VisualMapID")]
  [DBProp("VisualMapID", AllowNull = false)]
  public long VisualMapID
  {
    get => this._VisualMapID;
    set => this._VisualMapID = value;
  }

  [DBForeignKey("Gateway", "GatewayID")]
  [DBProp("GatewayID", AllowNull = false)]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("X", AllowNull = true)]
  public double X
  {
    get => this._X;
    set => this._X = value;
  }

  [DBProp("Y", AllowNull = true)]
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

  public Gateway Gateway
  {
    get
    {
      if (this._Gateway == null)
        this._Gateway = Gateway.Load(this.GatewayID);
      return this._Gateway;
    }
    set
    {
      this._Gateway = value;
      this._GatewayID = this._Gateway.GatewayID;
    }
  }

  public string DeviceType => "gateway";

  public long DeviceID => this.GatewayID;

  public Tuple<string, long> DeviceTypeAndID
  {
    get => new Tuple<string, long>(this.DeviceType, this.DeviceID);
  }

  public BaseDBObject Device => (BaseDBObject) this.Gateway;

  public static VisualMapGateway Load(long visualMapGatewayID)
  {
    return BaseDBObject.Load<VisualMapGateway>(visualMapGatewayID);
  }

  public static List<VisualMapGateway> LoadByVisualMapID(long visualMapID)
  {
    return BaseDBObject.LoadByForeignKey<VisualMapGateway>("VisualMapID", (object) visualMapID);
  }

  public static List<VisualMapGateway> LoadByGatewayID(long gatewayID)
  {
    return BaseDBObject.LoadByForeignKey<VisualMapGateway>("GatewayID", (object) gatewayID);
  }
}
