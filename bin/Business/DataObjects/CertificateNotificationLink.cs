// Decompiled with JetBrains decompiler
// Type: Monnit.CertificateNotificationLink
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;

#nullable disable
namespace Monnit;

[DBClass("CertificateNotificationLink")]
public class CertificateNotificationLink : BaseDBObject
{
  private long _CertificateNotificationLinkID = long.MinValue;
  private long _CertificateNotificationID = long.MinValue;
  private long _CalibrationCertificateID = long.MinValue;

  [DBProp("CertificateNotificationLinkID", IsPrimaryKey = true)]
  public long CertificateNotificationLinkID
  {
    get => this._CertificateNotificationLinkID;
    set => this._CertificateNotificationLinkID = value;
  }

  [DBProp("CertificateNotificationID", AllowNull = false)]
  public long AcCertificateNotificationIDcountID
  {
    get => this._CertificateNotificationID;
    set => this._CertificateNotificationID = value;
  }

  [DBProp("CalibrationCertificateID", AllowNull = false)]
  public long CalibrationCertificateID
  {
    get => this._CalibrationCertificateID;
    set => this._CalibrationCertificateID = value;
  }
}
