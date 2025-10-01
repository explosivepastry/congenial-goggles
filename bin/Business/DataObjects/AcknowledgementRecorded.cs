// Decompiled with JetBrains decompiler
// Type: Monnit.AcknowledgementRecorded
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit;

[DBClass("AcknowledgementRecorded")]
public class AcknowledgementRecorded : BaseDBObject
{
  private long _AcknowledgementRecordedID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private long _SensorID = long.MinValue;
  private string _Acknowledgement = string.Empty;
  private DateTime _DateRecorded = DateTime.MinValue;
  private long _GatewayID = long.MinValue;

  [DBProp("AcknowledgementRecordedID", IsPrimaryKey = true)]
  public long AcknowledgementRecordedID
  {
    get => this._AcknowledgementRecordedID;
    set => this._AcknowledgementRecordedID = value;
  }

  [DBProp("CustomerID", AllowNull = true)]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("GatewayID", AllowNull = true)]
  [DBForeignKey("Gateway", "GatewayID")]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("SensorID", AllowNull = true)]
  [DBForeignKey("Sensor", "SensorID")]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("Acknowledgement", MaxLength = 255 /*0xFF*/)]
  public string Acknowledgement
  {
    get => this._Acknowledgement;
    set
    {
      if (value == null)
        this._Acknowledgement = string.Empty;
      else
        this._Acknowledgement = value;
    }
  }

  [DBProp("DateRecorded", AllowNull = false)]
  public DateTime DateRecorded
  {
    get => this._DateRecorded;
    set => this._DateRecorded = value;
  }

  public static AcknowledgementRecorded Load(long ID)
  {
    return BaseDBObject.Load<AcknowledgementRecorded>(ID);
  }
}
