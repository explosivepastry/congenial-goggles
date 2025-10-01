// Decompiled with JetBrains decompiler
// Type: Monnit.CertificateAcknowledgement
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("CertificateAcknowledgement")]
public class CertificateAcknowledgement : BaseDBObject
{
  private long _CertificateAcknowledgementID = long.MinValue;
  private long _CertificateNotificationID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private DateTime _AcknowledgeDate = DateTime.UtcNow;

  [DBProp("CertificateAcknowledgementID", IsPrimaryKey = true)]
  public long CertificateAcknowledgementID
  {
    get => this._CertificateAcknowledgementID;
    set => this._CertificateAcknowledgementID = value;
  }

  [DBProp("CertificateNotificationID", AllowNull = false)]
  public long AcCertificateNotificationIDcountID
  {
    get => this._CertificateNotificationID;
    set => this._CertificateNotificationID = value;
  }

  [DBProp("CustomerID", AllowNull = false)]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("AcknowledgeDate")]
  public DateTime AcknowledgeDate
  {
    get => this._AcknowledgeDate;
    set => this._AcknowledgeDate = value;
  }

  public static CertificateAcknowledgement Load(long ID)
  {
    return BaseDBObject.Load<CertificateAcknowledgement>(ID);
  }

  public static List<CertificateAcknowledgement> LoadAll()
  {
    return BaseDBObject.LoadAll<CertificateAcknowledgement>();
  }

  public static int CreateAcknowledgement()
  {
    return new Monnit.Data.CertificateAcknowledgement.CreateAcknowledgement().Result;
  }

  public static int DeleteAcknowledgementByCertificateID(long calibrationCertificateID)
  {
    return new Monnit.Data.CertificateAcknowledgement.DeleteAcknowledgementByCertificateID(calibrationCertificateID).Result;
  }
}
