// Decompiled with JetBrains decompiler
// Type: Monnit.SensorGroupGateway
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("SensorGroupGateway")]
public class SensorGroupGateway : BaseDBObject
{
  private long _SensorGroupGatewayID = long.MinValue;
  private long _SensorGroupID = long.MinValue;
  private long _GatewayID = long.MinValue;
  private int _Position = int.MinValue;
  private Gateway _Gateway = (Gateway) null;

  [DBProp("SensorGroupGatewayID", IsPrimaryKey = true)]
  public long SensorGroupGatewayID
  {
    get => this._SensorGroupGatewayID;
    set => this._SensorGroupGatewayID = value;
  }

  [DBProp("SensorGroupID")]
  [DBForeignKey("SensorGroup", "SensorGroupID")]
  public long SensorGroupID
  {
    get => this._SensorGroupID;
    set => this._SensorGroupID = value;
  }

  [DBProp("GatewayID")]
  [DBForeignKey("Gateway", "GatewayID")]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("Position")]
  public int Position
  {
    get => this._Position;
    set => this._Position = value;
  }

  public Gateway Gateway
  {
    get
    {
      if (this._Gateway == null)
        this._Gateway = Gateway.Load(this.GatewayID);
      return this._Gateway;
    }
    set => this._Gateway = value;
  }

  public static SensorGroupGateway Load(long id) => BaseDBObject.Load<SensorGroupGateway>(id);

  public static List<SensorGroupGateway> LoadByGatewayID(long gatewayID)
  {
    return BaseDBObject.LoadByForeignKey<SensorGroupGateway>("GatewayID", (object) gatewayID);
  }

  public static List<SensorGroupGateway> LoadBySensorGroupID(long sensorGroupID)
  {
    return BaseDBObject.LoadByForeignKey<SensorGroupGateway>("SensorGroupID", (object) sensorGroupID);
  }
}
