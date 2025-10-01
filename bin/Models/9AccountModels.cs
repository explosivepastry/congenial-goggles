// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.EditAccountAPIModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System.ComponentModel;

#nullable disable
namespace iMonnit.Models;

public class EditAccountAPIModel
{
  [DisplayName("Account Number")]
  public string AccountNumber { get; set; }

  [DisplayName("Company Name")]
  public string CompanyName { get; set; }

  [DisplayName("Time Zone")]
  public long TimeZoneID { get; set; }

  [DisplayName("Industry Type")]
  public long IndustryTypeID { get; set; }

  [DisplayName("Business Type")]
  public long BusinessTypeID { get; set; }
}
