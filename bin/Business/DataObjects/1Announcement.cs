// Decompiled with JetBrains decompiler
// Type: Monnit.Data.Announcement
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class Announcement : BaseDBObject
{
  [DBMethod("Announcement_MarkReadByCustomerID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @RV               BIT,\r\n        @ProcName         NVARCHAR(50),\r\n        @Date             DATETIME;\r\n        \r\nDECLARE @ErrorSysMsg      NVARCHAR(MAX);\r\n\r\nBEGIN TRAN\r\n\r\nBEGIN TRY\r\n\r\n  SET @Rv       = 1 --1 = success\r\n  SET @ProcName = OBJECT_NAME(@@PROCID);\r\n  SET @Date     = GETUTCDATE();\r\n\r\n  INSERT INTO dbo.[AnnouncementViewed] ([AnnouncementID],[CustomerID],[Viewed],[ViewDate])\r\n  SELECT \r\n    a.AnnouncementID, \r\n    CustomerID        = @CustomerID, \r\n    Viewed            = 1, \r\n    ViewDate          = @Date\r\n  FROM dbo.[Announcement] a\r\n  LEFT JOIN dbo.[AnnouncementViewed] av ON a.AnnouncementID = av.AnnouncementID AND av.CustomerID = @CustomerID\r\n  WHERE a.IsDeleted     = 0\r\n    AND a.ReleaseDate  <= GETUTCDATE()\r\n    AND @AccountThemeID = ISNULL(a.AccountThemeID, @AccountThemeID)\r\n    AND av.[AnnouncementViewedID] IS NULL\r\n  ORDER BY  a.ReleaseDate DESC\r\n\r\n\r\nEND TRY\r\nBEGIN CATCH\r\n\r\n  SET @ErrorSysMsg = ERROR_MESSAGE();\r\n\r\n  IF (@@TRANCOUNT > 0)\r\n  BEGIN\r\n  ROLLBACK TRANSACTION\r\n  END\r\n\r\n  --RAISERROR (@ErrorSysMsg, 11, 1)\r\n  SET @RV = 0\r\n\r\nEND CATCH\r\n\r\nIF (@@TRANCOUNT > 0)\r\nBEGIN\r\n  COMMIT TRANSACTION\r\nEND\r\n\r\nSELECT RV = @RV, ErrorMsg = ISNULL(@ErrorSysMsg, 'Success');\r\n            \r\n")]
  internal class MarkReadByCustomerID : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("AccountThemeID", typeof (long))]
    public long AccountThemeID { get; private set; }

    public DataTable Result { get; private set; }

    public MarkReadByCustomerID(long customerID, long accountThemeID)
    {
      this.CustomerID = customerID;
      this.AccountThemeID = accountThemeID;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("Announcement_LoadByCustomerID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @CustomerDate DATETIME\r\n\r\nSET @CustomerDate = (SELECT ISNULL(CreateDate, '2016-01-01') FROM dbo.[Customer] WITH (NOLOCK) WHERE CustomerID = @CustomerID);\r\n\r\nSELECT TOP 1\r\n  a.*,\r\n  CustomerViewed = CONVERT(BIT,ISNULL(av.Viewed, 0)),\r\n  Prev = (SELECT TOP 1 a2.AnnouncementID FROM dbo.[Announcement] a2 where a2.IsDeleted = 0 AND ISNULL(a2.AccountThemeID, @AccountThemeID) = @AccountThemeID AND a2.ReleaseDate <= a.ReleaseDate and a2.AnnouncementID != ISNULL(@AnnouncementID, a.AnnouncementID) ORDER BY a2.ReleaseDate DESC, a2.AnnouncementID DESC),\r\n  Next = (SELECT TOP 1 a2.AnnouncementID FROM dbo.[Announcement] a2 where a2.IsDeleted = 0 AND ISNULL(a2.AccountThemeID, @AccountThemeID) = @AccountThemeID AND a2.ReleaseDate >= a.ReleaseDate and a2.AnnouncementID != @AnnouncementID and a2.ReleaseDate < GETUTCDATE() ORDER BY a2.ReleaseDate, a2.AnnouncementID)\r\nFROM dbo.[Announcement] a\r\nLEFT JOIN dbo.[AnnouncementViewed] av ON a.AnnouncementID = av.AnnouncementID AND av.CustomerID = @CustomerID\r\nWHERE a.IsDeleted      = 0\r\n  AND a.ReleaseDate   <= GETUTCDATE()\r\n  AND @AccountThemeID  = ISNULL(a.AccountThemeID, @AccountThemeID)\r\n  AND a.AnnouncementID = ISNULL(@AnnouncementID, a.AnnouncementID)\r\n  AND a.ReleaseDate >= @CustomerDate\r\nORDER BY a.ReleaseDate DESC;\r\n")]
  internal class LoadByCustomerID : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("AccountThemeID", typeof (long))]
    public long AccountThemeID { get; private set; }

    [DBMethodParam("AnnouncementID", typeof (long))]
    public long AnnouncementID { get; private set; }

    public AnnouncementPopup Result { get; private set; }

    public LoadByCustomerID(long customerID, long accountThemeID, long announcementid)
    {
      this.CustomerID = customerID;
      this.AccountThemeID = accountThemeID;
      this.AnnouncementID = announcementid;
      AnnouncementPopup announcementPopup = new AnnouncementPopup();
      Monnit.Announcement announcement = new Monnit.Announcement();
      DataRowCollection rows = this.ToDataTable().Rows;
      if (rows != null && rows.Count > 0)
      {
        DataRow dr = rows[0];
        announcement.Load(dr);
        announcementPopup.CustomerViewed = dr["CustomerViewed"].ToBool();
        announcementPopup.Prev = dr["Prev"].ToLong();
        announcementPopup.Next = dr["Next"].ToLong();
        announcementPopup.AnnouncementObj = announcement;
      }
      this.Result = announcementPopup;
    }
  }
}
