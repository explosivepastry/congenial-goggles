// Decompiled with JetBrains decompiler
// Type: Monnit.NonCachedAttribute
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("NonCachedAttribute")]
public class NonCachedAttribute : BaseDBObject
{
  private long _NonCachedAttributeID = long.MinValue;
  private long _GatewayID = long.MinValue;
  private long _SensorID = long.MinValue;
  private string _Name = string.Empty;
  private string _Value1 = string.Empty;
  private string _Value2 = string.Empty;
  private string _Value3 = string.Empty;
  private string _Value4 = string.Empty;
  private string _Value5 = string.Empty;

  [DBProp("NonCachedAttributeID", IsPrimaryKey = true)]
  public long NonCachedAttributeID
  {
    get => this._NonCachedAttributeID;
    set => this._NonCachedAttributeID = value;
  }

  [DBForeignKey("Gateway", "GatewayID")]
  [DBProp("GatewayID", AllowNull = true)]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBForeignKey("Sensor", "SensorID")]
  [DBProp("SensorID", AllowNull = true)]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Value1", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Value1
  {
    get => this._Value1;
    set => this._Value1 = value;
  }

  [DBProp("Value2", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Value2
  {
    get => this._Value2;
    set => this._Value2 = value;
  }

  [DBProp("Value3", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Value3
  {
    get => this._Value3;
    set => this._Value3 = value;
  }

  [DBProp("Value4", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Value4
  {
    get => this._Value4;
    set => this._Value4 = value;
  }

  [DBProp("Value5", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Value5
  {
    get => this._Value5;
    set => this._Value5 = value;
  }

  public static NonCachedAttribute Load(long ID) => BaseDBObject.Load<NonCachedAttribute>(ID);

  public static List<NonCachedAttribute> LoadBySensorID(long SensorID)
  {
    return BaseDBObject.LoadByForeignKey<NonCachedAttribute>(nameof (SensorID), (object) SensorID);
  }

  public static List<NonCachedAttribute> LoadByGatewayID(long GatewayID)
  {
    return BaseDBObject.LoadByForeignKey<NonCachedAttribute>(nameof (GatewayID), (object) GatewayID);
  }

  public static NonCachedAttribute LoadBySensorIDAndName(long sensorID, string name)
  {
    return NonCachedAttribute.LoadBySensorID(sensorID).FirstOrDefault<NonCachedAttribute>((Func<NonCachedAttribute, bool>) (nca => nca.Name.ToLower() == name.ToLower()));
  }

  public static NonCachedAttribute LoadByGatewayIDAndName(long gatewayID, string name)
  {
    return NonCachedAttribute.LoadBySensorID(gatewayID).FirstOrDefault<NonCachedAttribute>((Func<NonCachedAttribute, bool>) (nca => nca.Name.ToLower() == name.ToLower()));
  }
}
