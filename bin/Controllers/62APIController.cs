// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APICalibrationCertificate
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;

#nullable disable
namespace iMonnit.API;

public class APICalibrationCertificate
{
  public APICalibrationCertificate()
  {
  }

  public APICalibrationCertificate(CalibrationCertificate cert)
  {
    this.CalibrationCertificateID = cert.CalibrationCertificateID;
    this.CreatedByUserID = cert.CreatedByUserID;
    this.SensorID = cert.SensorID;
    this.DateCreated = cert.DateCreated;
    this.DateCertified = cert.DateCertified;
    this.CertificationValidUntil = cert.CertificationValidUntil;
    this.CalibrationNumber = cert.CalibrationNumber;
    this.CalibrationFacilityID = cert.CalibrationFacilityID;
    this.CertificationType = cert.CertificationType;
    this.DeletedByUserID = cert.DeletedByUserID;
    this.DeletedDate = cert.DeletedDate;
  }

  public long CalibrationCertificateID { get; set; }

  public long CreatedByUserID { get; set; }

  public long SensorID { get; set; }

  public DateTime DateCreated { get; set; }

  public DateTime DateCertified { get; set; }

  public DateTime CertificationValidUntil { get; set; }

  public string CalibrationNumber { get; set; }

  public long CalibrationFacilityID { get; set; }

  public string CertificationType { get; set; }

  public long DeletedByUserID { get; set; }

  public DateTime DeletedDate { get; set; }
}
