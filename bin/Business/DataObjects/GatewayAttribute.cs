// Decompiled with JetBrains decompiler
// Type: Monnit.GatewayAttribute
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("GatewayAttribute")]
public class GatewayAttribute : BaseDBObject
{
  private long _GatewayAttributeID = long.MinValue;
  private long _GatewayID = long.MinValue;
  private string _Name = string.Empty;
  private string _Value = string.Empty;

  [DBProp("GatewayAttributeID", IsPrimaryKey = true)]
  public long GatewayAttributeID
  {
    get => this._GatewayAttributeID;
    set => this._GatewayAttributeID = value;
  }

  [DBForeignKey("Gateway", "GatewayID")]
  [DBProp("GatewayID", AllowNull = false)]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/)]
  public string Name
  {
    get => this._Name;
    set
    {
      if (value == null)
        this._Name = string.Empty;
      else
        this._Name = value;
    }
  }

  [DBProp("Value", MaxLength = 255 /*0xFF*/)]
  public string Value
  {
    get => this._Value;
    set
    {
      if (value == null)
        this._Value = string.Empty;
      else
        this._Value = value;
    }
  }

  public static GatewayAttribute Load(long ID) => BaseDBObject.Load<GatewayAttribute>(ID);

  public static List<GatewayAttribute> LoadByGatewayID(long GatewayID)
  {
    string key = $"GatewayAttribute{GatewayID}";
    List<GatewayAttribute> gatewayAttributeList = TimedCache.RetrieveObject<List<GatewayAttribute>>(key);
    if (gatewayAttributeList == null)
    {
      gatewayAttributeList = BaseDBObject.LoadByForeignKey<GatewayAttribute>(nameof (GatewayID), (object) GatewayID);
      if (gatewayAttributeList != null)
        TimedCache.AddObjectToCach(key, (object) gatewayAttributeList, new TimeSpan(0, 30, 0));
    }
    return gatewayAttributeList;
  }

  public static List<GatewayAttribute> LoadCustomCommands()
  {
    try
    {
      return BaseDBObject.Load<GatewayAttribute>(Database.ExecuteQuery("SELECT * FROM GatewayAttribute WHERE Name = 'CustomCommand'").Tables[0]);
    }
    catch (Exception ex)
    {
      ex.Log("GatewayAttribute.LoadCustomCommands ");
      return new List<GatewayAttribute>();
    }
  }

  public static List<GatewayAttribute> LoadRFCommands()
  {
    try
    {
      return BaseDBObject.Load<GatewayAttribute>(Database.ExecuteQuery("SELECT * FROM GatewayAttribute WHERE Name = 'RFCommand'").Tables[0]);
    }
    catch (Exception ex)
    {
      ex.Log("GatewayAttribute.LoadCustomCommands ");
      return new List<GatewayAttribute>();
    }
  }

  public static void ResetAttributeList(long GatewayID)
  {
    TimedCache.RemoveObject($"GatewayAttribute{GatewayID}");
  }
}
