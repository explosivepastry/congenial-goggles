// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIAccountTree
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using RedefineImpossible;

#nullable disable
namespace iMonnit.API;

public class APIAccountTree
{
  public APIAccountTree()
  {
  }

  public APIAccountTree(AccountIDTreeModel model)
  {
    this.AccountID = model.AccountID;
    this.AccountNumber = model.AccountName;
    this.ParentID = model.ParentID;
    this.AccountIDTree = model.AccountIDTree.Replace('*', '|');
  }

  [DBProp("AccountID")]
  public long AccountID { get; set; }

  [DBProp("AccountNumber")]
  public string AccountNumber { get; set; }

  [DBProp("ParentID")]
  public long ParentID { get; set; }

  [DBProp("AccountIDTree")]
  public string AccountIDTree { get; set; }
}
