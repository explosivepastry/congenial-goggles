// Decompiled with JetBrains decompiler
// Type: Monnit.CalibrationCertificate
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("CalibrationCertificate")]
public class CalibrationCertificate : BaseDBObject
{
  private long _CalibrationCertificateID = long.MinValue;
  private long _CreatedByUserID = long.MinValue;
  private long _SensorID = long.MinValue;
  private long _CableID = long.MinValue;
  private DateTime _DateCreated = DateTime.MinValue;
  private DateTime _DateCertified = DateTime.MinValue;
  private DateTime _CertificationValidUntil = DateTime.MinValue;
  private string _CalibrationNumber = string.Empty;
  private long _CalibrationFacilityID = long.MinValue;
  private string _CertificationType = string.Empty;
  private long _DeletedByUserID = long.MinValue;
  private DateTime _DeletedDate = DateTime.MinValue;

  [DBProp("CalibrationCertificateID", IsPrimaryKey = true)]
  public long CalibrationCertificateID
  {
    get => this._CalibrationCertificateID;
    set => this._CalibrationCertificateID = value;
  }

  [DBProp("CreatedByUserID", AllowNull = false)]
  public long CreatedByUserID
  {
    get => this._CreatedByUserID;
    set => this._CreatedByUserID = value;
  }

  [DBProp("SensorID", AllowNull = true)]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("CableID", AllowNull = true)]
  public long CableID
  {
    get => this._CableID;
    set => this._CableID = value;
  }

  [DBProp("DateCreated", AllowNull = false)]
  public DateTime DateCreated
  {
    get => this._DateCreated;
    set => this._DateCreated = value;
  }

  [DBProp("DateCertified", AllowNull = false)]
  public DateTime DateCertified
  {
    get => this._DateCertified;
    set => this._DateCertified = value;
  }

  [DBProp("CertificationValidUntil", AllowNull = false)]
  public DateTime CertificationValidUntil
  {
    get => this._CertificationValidUntil;
    set => this._CertificationValidUntil = value;
  }

  [DBProp("CalibrationNumber", AllowNull = true)]
  public string CalibrationNumber
  {
    get => this._CalibrationNumber;
    set => this._CalibrationNumber = value;
  }

  [DBProp("CalibrationFacilityID", AllowNull = true)]
  public long CalibrationFacilityID
  {
    get => this._CalibrationFacilityID;
    set => this._CalibrationFacilityID = value;
  }

  [DBProp("CertificationType", AllowNull = true)]
  public string CertificationType
  {
    get => this._CertificationType;
    set => this._CertificationType = value;
  }

  [DBProp("DeletedByUserID", AllowNull = true)]
  public long DeletedByUserID
  {
    get => this._DeletedByUserID;
    set => this._DeletedByUserID = value;
  }

  [DBProp("DeletedDate", AllowNull = true)]
  public DateTime DeletedDate
  {
    get => this._DeletedDate;
    set => this._DeletedDate = value;
  }

  public bool isInternalCert => this.CertificationType.Contains("InternalSensorCert");

  public static CalibrationCertificate Load(long ID)
  {
    return BaseDBObject.Load<CalibrationCertificate>(ID);
  }

  public static List<CalibrationCertificate> LoadAll()
  {
    return BaseDBObject.LoadAll<CalibrationCertificate>();
  }

  public static List<CalibrationCertificate> LoadAllBySensorID(long id)
  {
    return BaseDBObject.LoadByForeignKey<CalibrationCertificate>("SensorID", (object) id).OrderByDescending<CalibrationCertificate, DateTime>((Func<CalibrationCertificate, DateTime>) (cc => cc.DateCreated)).ToList<CalibrationCertificate>();
  }

  private static CalibrationCertificate LoadBySensorID(long id)
  {
    return BaseDBObject.LoadByForeignKey<CalibrationCertificate>("SensorID", (object) id).Where<CalibrationCertificate>((Func<CalibrationCertificate, bool>) (cc => cc.DeletedDate == DateTime.MinValue)).OrderByDescending<CalibrationCertificate, DateTime>((Func<CalibrationCertificate, DateTime>) (cc => cc.DateCreated)).FirstOrDefault<CalibrationCertificate>();
  }

  private static CalibrationCertificate LoadByCableID(long id)
  {
    return BaseDBObject.LoadByForeignKey<CalibrationCertificate>("CableID", (object) id).Where<CalibrationCertificate>((Func<CalibrationCertificate, bool>) (cc => cc.DeletedDate == DateTime.MinValue)).OrderByDescending<CalibrationCertificate, DateTime>((Func<CalibrationCertificate, DateTime>) (cc => cc.DateCreated)).FirstOrDefault<CalibrationCertificate>();
  }

  public static CalibrationCertificate LoadBySensor(Sensor sens)
  {
    return sens.IsCableEnabled ? CalibrationCertificate.LoadByCableID(sens.CableID) : CalibrationCertificate.LoadBySensorID(sens.SensorID);
  }
}
