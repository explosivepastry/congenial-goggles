// Decompiled with JetBrains decompiler
// Type: Monnit.GatewayStatus
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit;

[DBClass("GatewayStatus")]
public class GatewayStatus : BaseDBObject
{
  private long _GatewayID = long.MinValue;
  private int _CurrentPower = int.MinValue;
  private DateTime _LastCommunicationDate = DateTime.MinValue;
  private string _LastInboundIPAddress = string.Empty;
  private int _LastSequence = 0;
  private DateTime _LastLocationDate = DateTime.UtcNow;

  [DBProp("GatewayID", IsPrimaryKey = true)]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("CurrentPower")]
  public int CurrentPower
  {
    get => this._CurrentPower;
    set => this._CurrentPower = value;
  }

  [DBProp("LastCommunicationDate")]
  public DateTime LastCommunicationDate
  {
    get => this._LastCommunicationDate;
    set => this._LastCommunicationDate = value;
  }

  [DBProp("LastInboundIPAddress", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string LastInboundIPAddress
  {
    get => this._LastInboundIPAddress;
    set => this._LastInboundIPAddress = value;
  }

  [DBProp("LastSequence")]
  public int LastSequence
  {
    get => this._LastSequence;
    set => this._LastSequence = value;
  }

  [DBProp("LastLocationDate")]
  public DateTime LastLocationDate
  {
    get => this._LastLocationDate;
    set => this._LastLocationDate = value;
  }
}
