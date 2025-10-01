// Decompiled with JetBrains decompiler
// Type: Monnit.CableAudit
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit;

[DBClass("CableAudit")]
public class CableAudit : BaseDBObject
{
  private long _CableAuditID = long.MinValue;
  private long _CableID = long.MinValue;
  private long _SensorID = long.MinValue;
  private DateTime _LogDate = DateTime.MinValue;
  private CableAudit.eCableAuditType _AuditType = CableAudit.eCableAuditType.Unknown;

  [DBProp("CableAuditID", IsPrimaryKey = true)]
  public long CableAuditID
  {
    get => this._CableAuditID;
    set => this._CableAuditID = value;
  }

  [DBProp("CableID", AllowNull = false)]
  [DBForeignKey("Cable", "CableID")]
  public long CableID
  {
    get => this._CableID;
    set => this._CableID = value;
  }

  [DBProp("SensorID", AllowNull = false)]
  [DBForeignKey("Sensor", "SensorID")]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("LogDate", AllowNull = false)]
  public DateTime LogDate
  {
    get => this._LogDate;
    set => this._LogDate = value;
  }

  [DBProp("Type", AllowNull = false)]
  public CableAudit.eCableAuditType AuditType
  {
    get => this._AuditType;
    set => this._AuditType = value;
  }

  public static DataTable LoadBySensorAndDateRange(long sensorID, DateTime from, DateTime to)
  {
    return new Monnit.Data.CableAudit.LoadBySensorAndDateRange(sensorID, from, to).Result;
  }

  public static List<CableAudit> LoadRecentByCableID(long cableID, DateTime from)
  {
    return new Monnit.Data.CableAudit.LoadRecentByCableID(cableID).Result;
  }

  public enum eCableAuditType
  {
    Unknown,
    PlugIn,
    PlugOut,
    AutoRemove,
  }
}
