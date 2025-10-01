// Decompiled with JetBrains decompiler
// Type: Monnit.Data.CertificateAcknowledgement
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;

#nullable disable
namespace Monnit.Data;

internal class CertificateAcknowledgement
{
  [DBMethod("CertificateNotification_CreateAcknowledgement")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @RowID INT = 1,\r\n        @MaxRowID INT,\r\n        @CertificateNotificationID BIGINT\r\n\r\nBEGIN TRY\r\n\r\nSELECT\r\n  a.AccountID,\r\n  a.AccountNumber,\r\n  c.CalibrationCertificateID,\r\n  s.SensorID,\r\n  s.SensorName,\r\n  c.CertificationValidUntil,\r\n  c2.FirstName,\r\n  c2.LastName,\r\n  EmailAddress = c2.NotificationEmail\r\nINTO #temp1\r\nFROM dbo.[CalibrationCertificate] c\r\nINNER JOIN dbo.[Sensor] s ON c.SensorID = s.SensorID\r\nINNER JOIN dbo.[Account] a ON s.AccountID = a.AccountID\r\nINNER JOIN dbo.[Customer] c2 ON a.PrimaryContactID = c2.CustomerID\r\nLEFT JOIN (\r\nSELECT\r\nc.CalibrationCertificateID\r\nFROM CalibrationCertificate c\r\nINNER JOIN CertificateNotificationLink cl on c.CalibrationCertificateID = cl.CalibrationCertificateID\r\nINNER JOIN CertificateNotification cn on cl.CertificateNotificationID = cn.CertificateNotificationID \r\nWHERE cn.Type = 2\r\n) t on t.CalibrationCertificateID = c.CalibrationCertificateID \r\nWHERE CertificationValidUntil >= GETUTCDATE()\r\n  AND CertificationValidUntil <= DATEADD(WEEK,2, GETUTCDATE())\r\n  AND DeletedByUserID IS NULL\r\n  AND t.CalibrationCertificateID IS NULL\r\n  AND a.AllowCertificateNotifications = 1;\r\n\r\n\r\nWITH CTE_Results AS (SELECT DISTINCT AccountID FROM #temp1)\r\nSELECT\r\n RowID = ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY AccountID), \r\n AccountID\r\nINTO #Rows\r\nFROM CTE_Results c;\r\n\r\nSET @MaxRowID = @@ROWCOUNT;\r\n\r\nWHILE @RowID <= @MaxRowID\r\nBEGIN\r\n\r\n    INSERT INTO dbo.[CertificateNotification] (AccountID, CreateDate, IsResolved, Type)\r\n    SELECT \r\n      AccountID, \r\n      GETUTCDATE(), \r\n      0, \r\n      2 \r\n    FROM #Rows \r\n    WHERE RowID = @RowID;\r\n\r\n    SET @CertificateNotificationID = @@IDENTITY;\r\n\r\n    INSERT INTO dbo.[CertificateAcknowledgement]\r\n    SELECT\r\n      @CertificateNotificationID,\r\n      CustomerID,\r\n      NULL\r\n    FROM #Rows r\r\n    INNER JOIN Customer c on r.AccountID = c.AccountID\r\n    WHERE c.IsDeleted = 0\r\n      AND r.RowID = @RowID\r\n\r\n    INSERT INTO dbo.[CertificateNotificationLink] (CertificateNotificationID, CalibrationCertificateID)\r\n    SELECT\r\n      @CertificateNotificationID,\r\n      CalibrationCertificateID\r\n    FROM #temp1 t\r\n    INNER JOIN #Rows r on t.AccountID = r.AccountID\r\n    WHERE r.RowID = @RowID;\r\n\r\n    SET @RowID = @RowID + 1;\r\n\r\nEND\r\n\r\n  SELECT RV = 1;\r\n\r\nEND TRY\r\nBEGIN CATCH\r\n\r\n  SELECT RV = 0;\r\n\r\nEND CATCH\r\n")]
  internal class CreateAcknowledgement : BaseDBMethod
  {
    public int Result { get; private set; }

    public CreateAcknowledgement() => this.Result = this.ToScalarValue<int>();
  }

  [DBMethod("CertificateAcknowledgement_DeleteByCertificateID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nBEGIN TRY \r\n\r\n\r\n  IF(\r\n      SELECT COUNT(*) \r\n      FROM dbo.[CertificateNotificationLink] c\r\n      INNER JOIN (SELECT\r\n                    CertificateNotificationID\r\n                  FROM dbo.[CertificateNotificationLink] cl\r\n                  WHERE CalibrationCertificateID = @CalibrationCertificateID) cl on cl.CertificateNotificationID = c.CertificateNotificationID\r\n  ) = 1\r\n  BEGIN\r\n\r\n    DELETE ca\r\n    FROM dbo.[CertificateAcknowledgement] ca\r\n    INNER JOIN dbo.[CertificateNotification] cn ON ca.CertificateNotificationID = cn.CertificateNotificationID\r\n    INNER JOIN dbo.[CertificateNotificationLink] cl ON cn.CertificateNotificationID = cl.CertificateNotificationID\r\n    WHERE CalibrationCertificateID = @CalibrationCertificateID\r\n\r\n    \r\n  END\r\n\r\n  DELETE cl\r\n  FROM dbo.[CertificateNotificationLink] cl\r\n  WHERE CalibrationCertificateID = @CalibrationCertificateID\r\n\r\n\r\n  SELECT\r\n    RV = 0;\r\n\r\nEND TRY\r\nBEGIN CATCH\r\n\r\n  SELECT\r\n    RV = 1;\r\n\r\nEND CATCH\r\n")]
  internal class DeleteAcknowledgementByCertificateID : BaseDBMethod
  {
    public int Result { get; private set; }

    [DBMethodParam("CalibrationCertificateID", typeof (long))]
    public long CalibrationCertificateID { get; private set; }

    public DeleteAcknowledgementByCertificateID(long calibrationCertificateID)
    {
      this.CalibrationCertificateID = calibrationCertificateID;
      this.Result = this.ToScalarValue<int>();
    }
  }
}
