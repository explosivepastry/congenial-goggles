// Decompiled with JetBrains decompiler
// Type: Monnit.CertificateNotification
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("CertificateNotification")]
public class CertificateNotification : BaseDBObject
{
  private long _CertificateNotificationID = long.MinValue;
  private long _AccountID = long.MinValue;
  private DateTime _CreateDate = DateTime.UtcNow;
  private bool _IsResolved = false;
  private eCertificateNotificationType _Type = eCertificateNotificationType.Email;

  [DBProp("CertificateNotificationID", IsPrimaryKey = true)]
  public long CertificateNotificationID
  {
    get => this._CertificateNotificationID;
    set => this._CertificateNotificationID = value;
  }

  [DBProp("AccountID", AllowNull = false)]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("CreateDate", AllowNull = false)]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("IsResolved", AllowNull = false)]
  public bool IsResolved
  {
    get => this._IsResolved;
    set => this._IsResolved = value;
  }

  [DBProp("Type", AllowNull = false)]
  public eCertificateNotificationType Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  public static List<CertificateNotificationModel> LoadCertNotificationForEmail()
  {
    return new Monnit.Data.CertificateNotification.LoadForEmail().Result;
  }

  public static List<CertificationAcknowledgementModel> LoadByCustomerID(long customerID)
  {
    return new Monnit.Data.CertificateNotification.LoadByCustomerID(customerID).Result;
  }
}
