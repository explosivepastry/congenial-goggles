// Decompiled with JetBrains decompiler
// Type: Monnit.AccountIDTreeModel
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;

#nullable disable
namespace Monnit;

public class AccountIDTreeModel : BaseDBObject
{
  public AccountIDTreeModel()
  {
  }

  public AccountIDTreeModel(
    long accountID,
    string accountName,
    long parentID,
    string accountIDTree)
  {
    this.AccountID = accountID;
    this.AccountName = accountName;
    this.ParentID = parentID;
    this.AccountIDTree = accountIDTree;
  }

  [DBProp("AccountID")]
  public long AccountID { get; set; }

  [DBProp("AccountName")]
  public string AccountName { get; set; }

  [DBProp("ParentID")]
  public long ParentID { get; set; }

  [DBProp("AccountIDTree")]
  public string AccountIDTree { get; set; }
}
