// Decompiled with JetBrains decompiler
// Type: Monnit.ProgrammerAudit
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("ProgrammerAudit")]
public class ProgrammerAudit : BaseDBObject
{
  private long _ProgrammerAuditID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private DateTime _TimeStamp;
  private long _SensorID = long.MinValue;
  private long _GatewayID = long.MinValue;
  private long _CableID = long.MinValue;
  private long _MonnitApplicationID = long.MinValue;
  private long _GatewayTypeID = long.MinValue;
  private string _Note = string.Empty;

  [DBProp("ProgrammerAuditID", IsPrimaryKey = true)]
  public long ProgrammerAuditID
  {
    get => this._ProgrammerAuditID;
    set => this._ProgrammerAuditID = value;
  }

  [DBProp("CustomerID")]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("ProgramTimeStamp")]
  public DateTime ProgramTimeStamp
  {
    get => this._TimeStamp;
    set => this._TimeStamp = value;
  }

  [DBProp("SensorID")]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("GatewayID")]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("CableID")]
  public long CableID
  {
    get => this._CableID;
    set => this._CableID = value;
  }

  [DBProp("ApplicationID")]
  public long MonnitApplicationID
  {
    get => this._MonnitApplicationID;
    set => this._MonnitApplicationID = value;
  }

  public long ApplicationID
  {
    get => this.MonnitApplicationID;
    set => this.MonnitApplicationID = value;
  }

  [DBProp("GatewayTypeID")]
  public long GatewayTypeID
  {
    get => this._GatewayTypeID;
    set => this._GatewayTypeID = value;
  }

  [DBProp("Note")]
  public string Note
  {
    get => this._Note;
    set => this._Note = value;
  }

  public static ProgrammerAudit GetBySensorID(long sensorID)
  {
    return BaseDBObject.LoadByForeignKey<ProgrammerAudit>("SensorID", (object) sensorID).SingleOrDefault<ProgrammerAudit>();
  }
}
