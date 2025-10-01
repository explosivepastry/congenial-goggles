// Decompiled with JetBrains decompiler
// Type: Monnit.CertificationAcknowledgementModel
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit;

public class CertificationAcknowledgementModel : BaseDBObject
{
  [DBProp("CertificateNotificationID")]
  public long CertificateNotificationID { get; set; }

  [DBProp("CreateDate")]
  public DateTime CreateDate { get; set; }

  [DBProp("CertificateAcknowledgementID")]
  public long CertificateAcknowledgementID { get; set; }

  [DBProp("CalibrationCertificateID")]
  public long CalibrationCertificateID { get; set; }

  [DBProp("SensorID")]
  public long SensorID { get; set; }

  [DBProp("SensorName")]
  public string SensorName { get; set; }

  [DBProp("ApplicationID")]
  public string ApplicationID { get; set; }

  [DBProp("CertificationValidUntil")]
  public DateTime CertificationValidUntil { get; set; }
}
