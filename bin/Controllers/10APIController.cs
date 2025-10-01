// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIAccount
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;

#nullable disable
namespace iMonnit.API;

public class APIAccount
{
  public APIAccount()
  {
  }

  public APIAccount(Account a)
  {
    this.PrimaryContactID = a.PrimaryContactID;
    this.AccountID = a.AccountID;
    this.AccountNumber = a.AccountNumber;
    this.CompanyName = a.CompanyName;
    this.Address = a.PrimaryAddress.Address;
    this.Address2 = a.PrimaryAddress.Address2;
    this.City = a.PrimaryAddress.City;
    this.State = a.PrimaryAddress.State;
    this.PostalCode = a.PrimaryAddress.PostalCode;
    this.Country = a.PrimaryAddress.Country;
    this.BusinessTypeID = a.BusinessTypeID;
    this.IndustryTypeID = a.IndustryTypeID;
    this.TimeZoneID = a.TimeZoneID;
    this.PrimaryContact = a.PrimaryContact.FullName;
    this.ParentID = a.RetailAccountID;
    this.ExpirationDate = a.PremiumValidUntil;
  }

  public long PrimaryContactID { get; set; }

  public long AccountID { get; set; }

  public string AccountNumber { get; set; }

  public string CompanyName { get; set; }

  public string Address { get; set; }

  public string Address2 { get; set; }

  public string City { get; set; }

  public string PostalCode { get; set; }

  public string State { get; set; }

  public string Country { get; set; }

  public long BusinessTypeID { get; set; }

  public long IndustryTypeID { get; set; }

  public long TimeZoneID { get; set; }

  public string PrimaryContact { get; set; }

  public long ParentID { get; set; }

  public DateTime ExpirationDate { get; set; }
}
