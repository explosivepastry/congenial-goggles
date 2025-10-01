// Decompiled with JetBrains decompiler
// Type: Monnit.Data.AdvancedNotification
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class AdvancedNotification
{
  [DBMethod("AdvancedNotification_LoadByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @RetailAccountID BIGINT;\r\n\r\nDECLARE @AdvancedNotification TABLE(\r\n  AdvancedNotificationID BIGINT\r\n);\r\n\r\nINSERT INTO @AdvancedNotification\r\nSELECT\r\n  AdvancedNotificationID\r\nFROM dbo.[AdvancedNotification]\r\nWHERE AccountID = @AccountID \r\n   OR AccountID IS NULL;\r\n\r\n\r\nSELECT\r\n  @RetailAccountID = RetailAccountID\r\nFROM dbo.[Account]\r\nWHERE AccountID       = @AccountID\r\n  AND RetailAccountID != @AccountID;\r\n\r\n\r\nWHILE @RetailAccountID IS NOT NULL\r\nBEGIN\r\n\r\n    SELECT @AccountID = @RetailAccountID, @RetailAccountID = NULL;\r\n\r\n    INSERT INTO @AdvancedNotification\r\n    SELECT \r\n      AdvancedNotificationID\r\n    FROM dbo.[AdvancedNotification]\r\n    WHERE AccountID = @AccountID;\r\n\t\t\t\r\n    SELECT \r\n      @RetailAccountID = RetailAccountID\r\n    FROM dbo.[Account]\r\n    WHERE AccountID       = @AccountID \r\n      AND RetailAccountID != @AccountID \r\n      AND RetailAccountID IS NOT NULL;\r\n\r\n    IF(@RetailAccountID = @AccountID)\r\n    BEGIN\r\n\r\n        SELECT @RetailAccountID = NULL;\r\n    \r\n    END\r\n\t\t\r\nEND\r\n\r\nSELECT\r\n  *\r\nFROM dbo.[AdvancedNotification] an\r\nINNER JOIN @AdvancedNotification temp ON temp.AdvancedNotificationID = an.AdvancedNotificationID;\r\n")]
  internal class LoadByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<Monnit.AdvancedNotification> Result { get; private set; }

    public LoadByAccountID(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.AdvancedNotification>(this.ToDataTable());
    }
  }
}
