// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.UserLookUpModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System.Data;

#nullable disable
namespace iMonnit.Models;

public class UserLookUpModel
{
  public UserLookUpModel()
  {
  }

  public UserLookUpModel(DataRow dr)
  {
    this.CustomerID = dr[nameof (CustomerID)].ToLong();
    this.IsActive = dr[nameof (IsActive)].ToBool();
    this.UserName = dr[nameof (UserName)].ToStringSafe();
    this.FirstName = dr[nameof (FirstName)].ToStringSafe();
    this.LastName = dr[nameof (LastName)].ToStringSafe();
    this.NotificationEmail = dr[nameof (NotificationEmail)].ToStringSafe();
    this.NotificationPhone = dr["Phone"].ToStringSafe();
    this.AccountNumber = dr[nameof (AccountNumber)].ToStringSafe();
    this.Retail = dr[nameof (Retail)].ToStringSafe();
    this.Domain = dr["Host"].ToStringSafe();
    this.AccountID = dr[nameof (AccountID)].ToLong();
  }

  public long CustomerID { get; set; }

  public bool IsActive { get; set; }

  public string UserName { get; set; }

  public string FirstName { get; set; }

  public string LastName { get; set; }

  public string NotificationEmail { get; set; }

  public string NotificationPhone { get; set; }

  public string AccountNumber { get; set; }

  public string Retail { get; set; }

  public string Domain { get; set; }

  public long AccountID { get; set; }
}
