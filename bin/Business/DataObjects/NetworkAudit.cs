// Decompiled with JetBrains decompiler
// Type: Monnit.NetworkAudit
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("NetworkAudit")]
public class NetworkAudit : BaseDBObject
{
  private long _NetworkAuditID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private long _SensorID = long.MinValue;
  private long _GatewayID = long.MinValue;
  private long _FromAccountID = long.MinValue;
  private long _FromNetworkID = long.MinValue;
  private DateTime _ChangeDate = DateTime.UtcNow;

  [DBProp("NetworkAuditID", IsPrimaryKey = true)]
  public long NetworkAuditID
  {
    get => this._NetworkAuditID;
    set => this._NetworkAuditID = value;
  }

  [DBForeignKey("Customer", "CustomerID")]
  [DBProp("CustomerID", AllowNull = false)]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBForeignKey("Sensor", "SensorID")]
  [DBProp("SensorID", AllowNull = true)]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBForeignKey("Gateway", "GatewayID")]
  [DBProp("GatewayID", AllowNull = true)]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("FromAccountID", AllowNull = false)]
  public long FromAccountID
  {
    get => this._FromAccountID;
    set => this._FromAccountID = value;
  }

  [DBProp("FromNetworkID ", AllowNull = false)]
  public long FromNetworkID
  {
    get => this._FromNetworkID;
    set => this._FromNetworkID = value;
  }

  [DBProp("ChangeDate")]
  public DateTime ChangeDate
  {
    get => this._ChangeDate;
    set => this._ChangeDate = value;
  }

  public static void LogNetworkChange(long userID, Sensor sens)
  {
    try
    {
      if (userID <= 0L || sens == null || sens.AccountID <= 0L || sens.CSNetID <= 0L)
        return;
      new NetworkAudit()
      {
        CustomerID = userID,
        FromAccountID = sens.AccountID,
        FromNetworkID = sens.CSNetID,
        SensorID = sens.SensorID,
        GatewayID = long.MinValue
      }.Save();
    }
    catch (Exception ex)
    {
      ex.Log("NetworkAudit.LogNetworkChange Sensor");
    }
  }

  public static void LogNetworkChange(long userID, Gateway gateway, long? oldAccountID)
  {
    try
    {
      if (userID <= 0L || gateway == null || gateway.CSNetID <= 0L)
        return;
      long num = oldAccountID ?? CSNet.Load(gateway.CSNetID).AccountID;
      if (num <= 0L)
        return;
      new NetworkAudit()
      {
        CustomerID = userID,
        GatewayID = gateway.GatewayID,
        SensorID = long.MinValue,
        FromAccountID = num,
        FromNetworkID = gateway.CSNetID
      }.Save();
    }
    catch (Exception ex)
    {
      ex.Log("NetworkAudit.LogNetworkChange Gateway");
    }
  }

  public static NetworkAudit Load(long id) => BaseDBObject.Load<NetworkAudit>(id);

  public static List<NetworkAudit> LoadByCustomerID(long customerID)
  {
    return BaseDBObject.LoadByForeignKey<NetworkAudit>("CustomerID", (object) customerID);
  }
}
