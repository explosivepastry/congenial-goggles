// Decompiled with JetBrains decompiler
// Type: Monnit.Data.NotificationCredit
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class NotificationCredit
{
  [DBMethod("NotificationCredit_LoadRemaingCreditsForAccount")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  RemainingCredits  = (SUM(nc.ActivatedCredits) - SUM(nc.UsedCredits))\r\nFROM dbo.[NotificationCredit] nc\r\nINNER JOIN dbo.[NotificationCreditType] nct ON nct.NotificationCreditTypeID = nc.NotificationCreditTypeID\r\nWHERE nc.AccountID      = @AccountID\r\n  AND nc.ActivationDate < DATEADD(DAY, 1, GETUTCDATE())\r\n  AND nc.ExpirationDate > GETUTCDATE()\r\n  AND nc.UsedCredits    < nc.ActivatedCredits\r\n  AND (nc.IsDeleted IS NULL OR nc.IsDeleted = 0);\r\n")]
  internal class LoadRemaingCreditsForAccount : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public int Result { get; private set; }

    public LoadRemaingCreditsForAccount(long accountID)
    {
      this.AccountID = accountID;
      this.Result = this.ToScalarValue<int>();
    }

    public static int Exec(long accountID)
    {
      return new NotificationCredit.LoadRemaingCreditsForAccount(accountID).Result;
    }
  }

  [DBMethod("NotificationCredit_LoadAvailable")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  nc.* \r\nFROM dbo.[NotificationCredit] nc\r\nINNER JOIN dbo.[NotificationCreditType] nct ON nct.NotificationCreditTypeID = nc.NotificationCreditTypeID\r\nWHERE nc.AccountID      = @AccountID \r\n  AND nc.ActivationDate < DATEADD(DAY,1,GETUTCDATE())\r\n  AND nc.ExpirationDate > GETUTCDATE()\r\n  AND nc.UsedCredits    < nc.ActivatedCredits\r\n  AND (nc.IsDeleted IS NULL OR nc.IsDeleted = 0)\r\nORDER BY\r\n  nct.Rank,\r\n  ExpirationDate,\r\n  ActivationDate;\r\n")]
  internal class LoadAvailable : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<Monnit.NotificationCredit> Result { get; private set; }

    public LoadAvailable(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.NotificationCredit>(this.ToDataTable());
    }

    public static List<Monnit.NotificationCredit> Exec(long accountID)
    {
      return new NotificationCredit.LoadAvailable(accountID).Result;
    }
  }
}
