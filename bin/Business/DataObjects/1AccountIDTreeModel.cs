// Decompiled with JetBrains decompiler
// Type: Monnit.Data.AccountIDTreeModel
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class AccountIDTreeModel
{
  [DBMethod("AccountIDTreeModel_LoadByUserAccountIDandAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @tree VARCHAR(255);\r\n\r\nSELECT\r\n  @tree = AccountIDTree\r\nFROM dbo.[Account] ac WITH (NOLOCK)\r\nWHERE ac.AccountID = @UserAccountID;\r\n\r\nSELECT\r\n  a.AccountID,\r\n  AccountName     = a.AccountNumber,\r\n  ParentID        = a.RetailAccountID,\r\n  AccountIDTree   = REPLACE(a.AccountIDTree, @tree, '*' +  CONVERT(VARCHAR,@UserAccountID) + '*' )\r\nFROM dbo.[Account] a WITH (NOLOCK)\r\nWHERE a.AccountIDTree LIKE '%*' + CONVERT(VARCHAR,@AccountID) + '*%';\r\n")]
  internal class LoadByUserAccountIDandAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("UserAccountID", typeof (long))]
    public long UserAccountID { get; private set; }

    public List<Monnit.AccountIDTreeModel> Result { get; private set; }

    public LoadByUserAccountIDandAccountID(long userAccountID, long accountID)
    {
      this.UserAccountID = userAccountID;
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.AccountIDTreeModel>(this.ToDataTable());
    }
  }
}
