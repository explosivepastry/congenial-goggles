// Decompiled with JetBrains decompiler
// Type: Monnit.Data.ExternalDataSubscription
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class ExternalDataSubscription
{
  [DBMethod("ExternalDataSubscription_LoadByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[ExternalDataSubscription] \r\nWHERE AccountID = @AccountID\r\n  AND IsDeleted = 0;\r\n")]
  internal class LoadByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<Monnit.ExternalDataSubscription> Result { get; private set; }

    public LoadByAccountID(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.ExternalDataSubscription>(this.ToDataTable());
    }
  }

  [DBMethod("ExternalDataSubscription_Remove")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nINTO #EDSA\r\nFROM dbo.[ExternalDataSubscriptionAttempt]\r\nWHERE ExternalDataSubscriptionAttempt.ExternalDataSubscriptionID = @ExternalDataSubscriptionID;\r\n\r\nDELETE dbo.[ExternalDataSubscriptionResponse]\r\nWHERE ExternalDataSubscriptionResponse.ExternalDataSubscriptionAttemptID\r\n   IN (SELECT ExternalDataSubscriptionAttemptID FROM #EDSA);\r\n\r\nDELETE dbo.[ExternalDataSubscriptionAttempt]\r\nWHERE ExternalDataSubscriptionAttempt.ExternalDataSubscriptionAttemptID\r\n   IN (SELECT ExternalDataSubscriptionAttemptID FROM #EDSA);\r\n\r\nDELETE dbo.[ExternalDataSubscription]\r\nWHERE ExternalDataSubscriptionID=@ExternalDataSubscriptionID;\r\n")]
  internal class Remove : BaseDBMethod
  {
    [DBMethodParam("ExternalDataSubscriptionID", typeof (long))]
    public long ExternalDataSubscriptionID { get; private set; }

    public Remove(long externalDataSubscriptionID)
    {
      try
      {
        this.ExternalDataSubscriptionID = externalDataSubscriptionID;
        this.Execute();
      }
      catch (Exception ex)
      {
        ExceptionLog.Log(ex);
      }
    }
  }

  [DBMethod("ExternalDataSubscription_LoadByCSNetID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  ed.*\r\nFROM dbo.[ExternalDataSubscription] ed\r\nINNER JOIN dbo.[Account] a  ON a.ExternalDataSubscriptionID = ed.ExternalDataSubscriptionID\r\nINNER JOIN dbo.[CSNet] c    ON c.AccountID = a.AccountID \r\nWHERE c.CSNetID = @CSNetID;\r\n")]
  internal class LoadByCSNetID : BaseDBMethod
  {
    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

    public Monnit.ExternalDataSubscription Result { get; private set; }

    public LoadByCSNetID(long csnetID)
    {
      this.CSNetID = csnetID;
      this.Result = BaseDBObject.Load<Monnit.ExternalDataSubscription>(this.ToDataTable()).FirstOrDefault<Monnit.ExternalDataSubscription>();
    }
  }

  [DBMethod("ExternalDataSubscription_DeleteByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nUPDATE dbo.[ExternalDataSubscription]\r\n  SET IsDeleted = 1\r\nWHERE AccountID = @AccountID\r\n  AND SensorID  IS NULL\r\n  AND GatewayID IS NULL;\r\n")]
  internal class DeleteByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public DeleteByAccountID(long accountID)
    {
      this.AccountID = accountID;
      this.Execute();
    }
  }

  [DBMethod("ExternalDataSubscription_LoadAllAccountsWithExternalSubscriptions")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT distinct\r\n  CSNetID, A.*\r\nFROM dbo.[ExternalDataSubscription] ed WITH (NOLOCK)\r\nINNER JOIN dbo.[Account] a WITH (NOLOCK) ON a.ExternalDataSubscriptionID = ed.ExternalDataSubscriptionID\r\nINNER JOIN dbo.[CSNet] c WITH (NOLOCK) ON c.AccountID = a.AccountID;\r\n")]
  internal class LoadAllAccountsWithExternalSubscriptions : BaseDBMethod
  {
    public Dictionary<long, Monnit.Account> Result { get; private set; }

    public LoadAllAccountsWithExternalSubscriptions()
    {
      this.Result = new Dictionary<long, Monnit.Account>();
      foreach (DataRow row in (InternalDataCollectionBase) this.ToDataTable().Rows)
      {
        try
        {
          Monnit.Account account = new Monnit.Account();
          account.Load(row);
          this.Result.Add((long) row["CSNetID"], account);
        }
        catch (Exception ex)
        {
          ex.Log("ExternalDataSubscription.LoadAllAccountsWithExternalSubscriptions/");
        }
      }
    }
  }
}
