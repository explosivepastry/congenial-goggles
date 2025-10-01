// Decompiled with JetBrains decompiler
// Type: Monnit.CertificateNotificationModel
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit;

public class CertificateNotificationModel : BaseDBObject
{
  [DBProp("CertificateNotificationID")]
  public long CertificateNotificationID { get; set; }

  [DBProp("AccountID")]
  public long AccountID { get; set; }

  [DBProp("AccountNumber")]
  public string AccountNumber { get; set; }

  [DBProp("SensorID")]
  public long SensorID { get; set; }

  [DBProp("SensorName")]
  public string SensorName { get; set; }

  [DBProp("CertificationValidUntil")]
  public DateTime CertificationValidUntil { get; set; }

  [DBProp("FirstName")]
  public string FirstName { get; set; }

  [DBProp("LastName")]
  public string LastName { get; set; }

  [DBProp("EmailAddress")]
  public string EmailAddress { get; set; }
}
