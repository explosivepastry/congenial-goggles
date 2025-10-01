// Decompiled with JetBrains decompiler
// Type: Monnit.Data.CertificateNotification
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class CertificateNotification
{
  [DBMethod("CertificateNotification_LoadForEmail")]
  [DBMethodBody(DBMS.SqlServer, "\r\n/***************************************************************\r\n                            NOTES\r\n----------------------------------------------------------------\r\nThe purpose of this store proc is to identifier which calibration\r\ncertificates are about to expire, send and email to the customer\r\nand create a log that tracks those notifications\r\n\r\nThe goal is to try to group as many certification expirations\r\ntogether so we don't send multiple emails if it can be avoided\r\n***************************************************************/\r\n\r\nDECLARE @RowID INT = 1,\r\n        @MaxRowID INT,\r\n        @CertificateNotificationID BIGINT\r\n\r\n/***************************************************************\r\n                      Find Certs to Notify\r\n---------------------------------------------------------------\r\nFind certifications that expire in the next 8 weeks that\r\nhaven't already had notifications sent, where the account allows\r\nus to send Cert Notifications.\r\n\r\nThen grab any other certification belonging to that account\r\nthat expires within 30 days of that expiration (that we also \r\nhaven't already sent out)\r\n\r\n***************************************************************/\r\nSELECT\r\n  a.AccountID,\r\n  a.AccountNumber,\r\n  c.CalibrationCertificateID,\r\n  s.SensorID,\r\n  s.SensorName,\r\n  c.CertificationValidUntil,\r\n  c2.FirstName,\r\n  c2.LastName,\r\n  EmailAddress = c2.NotificationEmail\r\nINTO #temp1\r\nFROM dbo.[CalibrationCertificate] c\r\nINNER JOIN dbo.[Sensor] s ON c.SensorID = s.SensorID\r\nINNER JOIN dbo.[Account] a ON s.AccountID = a.AccountID\r\nINNER JOIN dbo.[Customer] c2 ON a.PrimaryContactID = c2.CustomerID\r\nINNER JOIN \r\n(\r\n  SELECT DISTINCT \r\n    a.AccountID\r\n  FROM dbo.[CalibrationCertificate] c\r\n  INNER JOIN dbo.[Sensor] s ON c.SensorID = s.SensorID\r\n  INNER JOIN dbo.[Account] a ON s.AccountID = a.AccountID\r\n  WHERE CertificationValidUntil >= GETUTCDATE()\r\n    AND CertificationValidUntil <= DATEADD(WEEK, 8, GETUTCDATE())\r\n    AND DeletedByUserID IS NULL\r\n    AND a.AllowCertificateNotifications = 1\r\n) a2 ON a.AccountID = a2.AccountID\r\nLEFT JOIN (\r\n  SELECT\r\n  c.CalibrationCertificateID\r\n  FROM CalibrationCertificate c\r\nINNER JOIN CertificateNotificationLink cl on c.CalibrationCertificateID = cl.CalibrationCertificateID\r\nINNER JOIN CertificateNotification cn on cl.CertificateNotificationID = cn.CertificateNotificationID and cn.Type =1 \r\n) t on t.CalibrationCertificateID = c.CalibrationCertificateID\r\nWHERE CertificationValidUntil >= GETUTCDATE()\r\n  AND CertificationValidUntil <= DATEADD(DAY, 30, DATEADD(WEEK, 8, GETUTCDATE()))\r\n  AND DeletedByUserID IS NULL\r\n  AND t.CalibrationCertificateID IS NULL\r\n  AND a.AllowCertificateNotifications = 1;\r\n\r\n/***************************************************************\r\n                      Group Certifications\r\n---------------------------------------------------------------\r\nCreate a Certificate Notification Container for each account,\r\nand then loop over the above result set to place each certificate\r\nfor the account under it. \r\n\r\nThis will allow us to link all the expirations together for\r\na single email.\r\n***************************************************************/\r\nWITH CTE_Results AS (SELECT DISTINCT AccountID FROM #temp1)\r\nSELECT\r\n RowID = ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY AccountID), \r\n AccountID\r\nINTO #Rows\r\nFROM CTE_Results c;\r\n\r\nSET @MaxRowID = @@ROWCOUNT;\r\n\r\nWHILE @RowID <= @MaxRowID\r\nBEGIN\r\n\r\n    --Create Notification Container\r\n    INSERT INTO dbo.[CertificateNotification] (AccountID, CreateDate, IsResolved, Type)\r\n    SELECT \r\n      AccountID, \r\n      GETUTCDATE(), \r\n      0, \r\n      1 \r\n    FROM #Rows \r\n    WHERE RowID = @RowID;\r\n\r\n    SET @CertificateNotificationID = @@IDENTITY;\r\n\r\n    --Create Certificate Links under the container\r\n    INSERT INTO dbo.[CertificateNotificationLink] (CertificateNotificationID, CalibrationCertificateID)\r\n    SELECT\r\n      @CertificateNotificationID,\r\n      CalibrationCertificateID\r\n    FROM #temp1 t\r\n    INNER JOIN #Rows r on t.AccountID = r.AccountID\r\n    WHERE r.RowID = @RowID;\r\n\r\n    SET @RowID = @RowID + 1;\r\n\r\nEND\r\n\r\n--Result set returned to code base to create the email notification\r\nSELECT\r\n  n.CertificateNotificationID, t.*\r\nFROM #Rows r \r\nINNER JOIN #temp1 t ON r.AccountID = t.AccountID\r\nINNER JOIN dbo.[CertificateNotificationLink] l on t.CalibrationCertificateID = l.CalibrationCertificateID\r\nINNER JOIN dbo.[CertificateNotification] n on l.CertificateNotificationID = n.CertificateNotificationID AND n.Type = 1\r\nORDER BY r.RowID, t.CertificationValidUntil\r\n")]
  internal class LoadForEmail : BaseDBMethod
  {
    public List<CertificateNotificationModel> Result { get; private set; }

    public LoadForEmail()
    {
      this.Result = BaseDBObject.Load<CertificateNotificationModel>(this.ToDataTable());
    }
  }

  [DBMethod("CertificationAcknowledgement_LoadByCustomerID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  cn.CertificateNotificationID, \r\n  cn.CreateDate,\r\n  ca.CertificateAcknowledgementID,\r\n  cl.CalibrationCertificateID,\r\n  s.SensorID,\r\n  s.SensorName,\r\n  c.CertificationValidUntil,\r\n  s.ApplicationID\r\nFROM dbo.[CertificateAcknowledgement] ca WITH (NOLOCK)\r\nINNER JOIN dbo.[CertificateNotification] cn WITH (NOLOCK) ON ca.CertificateNotificationID = cn.CertificateNotificationID\r\nINNER JOIN dbo.[CertificateNotificationLink] cl WITH (NOLOCK) ON cn.CertificateNotificationID = cl.CertificateNotificationID\r\nINNER JOIN dbo.[CalibrationCertificate] c WITH (NOLOCK ) ON cl.CalibrationCertificateID = c.CalibrationCertificateID\r\nINNER JOIN dbo.[Sensor] s WITH (NOLOCK) ON c.SensorID = s.SensorID\r\nWHERE ca.CustomerID = @CustomerID\r\n  AND ca.AcknowledgeDate IS NULL\r\n  AND c.DeletedByUserID IS NULL;\r\n")]
  internal class LoadByCustomerID : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    public List<CertificationAcknowledgementModel> Result { get; private set; }

    public LoadByCustomerID(long customerID)
    {
      this.CustomerID = customerID;
      this.Result = BaseDBObject.Load<CertificationAcknowledgementModel>(this.ToDataTable());
    }
  }
}
