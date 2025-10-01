// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.AccountSearchModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class AccountSearchModel : BaseDBObject
{
  [DBProp("AccountID")]
  public long AccountID { get; set; }

  [DBProp("RetailAccountID")]
  public long RetailAccountID { get; set; }

  [DBProp("AccountNumber")]
  public string AccountNumber { get; set; }

  [DBProp("CompanyName")]
  public string CompanyName { get; set; }

  [DBProp("AccountSubscriptionType")]
  public string AccountSubscriptionType { get; set; }

  [DBProp("SubscriptionExpiration")]
  public DateTime SubscriptionExpiration { get; set; }

  [DBProp("Tree")]
  public string Tree { get; set; }

  public string[] AccountTree => this.Tree.Split('^');

  [DBProp("CustomerID")]
  public long CustomerID { get; set; }

  [DBProp("UserName")]
  public string UserName { get; set; }

  [DBProp("FirstName")]
  public string FirstName { get; set; }

  [DBProp("LastName")]
  public string LastName { get; set; }

  [DBProp("NotificationEmail")]
  public string NotificationEmail { get; set; }

  public static List<AccountSearchModel> Search(long customerID, string query, int limit)
  {
    return BaseDBObject.Load<AccountSearchModel>(Account.ModelSearch(customerID, query, limit));
  }
}
