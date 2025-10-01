// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.ReportRecipientData
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class ReportRecipientData : BaseDBObject
{
  private long _AccountID = long.MinValue;
  private string _AccountNumber = string.Empty;
  private string _CompanyName = string.Empty;
  private long _CustomerID = long.MinValue;
  private string _UserName = string.Empty;
  private string _FirstName = string.Empty;
  private string _LastName = string.Empty;
  private string _NotificationEmail = string.Empty;
  private int _Level = int.MinValue;

  [DBProp("AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("AccountNumber", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string AccountNumber
  {
    get => this._AccountNumber;
    set => this._AccountNumber = value;
  }

  [DBProp("CompanyName", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string CompanyName
  {
    get => this._CompanyName;
    set => this._CompanyName = value;
  }

  [DBProp("CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("UserName", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string UserName
  {
    get => this._UserName;
    set => this._UserName = value;
  }

  [DBProp("FirstName", AllowNull = false, MaxLength = 255 /*0xFF*/)]
  public string FirstName
  {
    get => this._FirstName;
    set
    {
      if (value == null)
        this._FirstName = string.Empty;
      else
        this._FirstName = value;
    }
  }

  [DBProp("LastName", AllowNull = false, MaxLength = 255 /*0xFF*/)]
  public string LastName
  {
    get => this._LastName;
    set
    {
      if (value == null)
        this._LastName = string.Empty;
      else
        this._LastName = value;
    }
  }

  [DBProp("NotificationEmail", MaxLength = 255 /*0xFF*/)]
  public string NotificationEmail
  {
    get => this._NotificationEmail;
    set
    {
      if (value == null)
        this._NotificationEmail = string.Empty;
      else
        this._NotificationEmail = value;
    }
  }

  [DBProp("Level")]
  public int Level
  {
    get => this._Level;
    set => this._Level = value;
  }

  public string FullName => $"{this.FirstName} {this.LastName}";

  public static List<ReportRecipientData> SearchPotentialReportRecipient(
    long customerID,
    long reportScheduleID,
    string query,
    long accountID)
  {
    return new Data.ReportRecipientData.SearchPotentialReportRecipient(customerID, reportScheduleID, query, accountID).Result;
  }
}
