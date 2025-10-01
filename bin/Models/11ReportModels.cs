// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.ResellerActivationModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System;
using System.Data;

#nullable disable
namespace iMonnit.Models;

public class ResellerActivationModel
{
  public ResellerActivationModel()
  {
  }

  public ResellerActivationModel(DataRow dr)
  {
    this.AccountID = dr[nameof (AccountID)].ToLong();
    this.CompanyName = dr[nameof (CompanyName)].ToStringSafe();
    this.ParentCompany = dr[nameof (ParentCompany)].ToStringSafe();
    this.CreateDate = dr[nameof (CreateDate)].ToDateTime();
    this.ExpirationDate = dr[nameof (ExpirationDate)].ToDateTime();
    this.SensorCount = dr[nameof (SensorCount)].ToLong();
  }

  public long AccountID { get; set; }

  public string CompanyName { get; set; }

  public string ParentCompany { get; set; }

  public DateTime CreateDate { get; set; }

  public DateTime ExpirationDate { get; set; }

  public long SensorCount { get; set; }
}
