// Decompiled with JetBrains decompiler
// Type: Monnit.MaintenanceRecipient
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

public class MaintenanceRecipient : BaseDBObject
{
  private Account _Account;

  [DBProp("CustomerID")]
  public long CustomerID { get; set; }

  [DBProp("AccountThemeID")]
  public long AccountThemeID { get; set; }

  [DBProp("AccountID")]
  public long AccountID { get; set; }

  [DBProp("FirstName")]
  public string FirstName { get; set; }

  [DBProp("LastName")]
  public string LastName { get; set; }

  public string FullName => $"{this.FirstName} {this.LastName}";

  public Account Account
  {
    get
    {
      if (this._Account == null)
        this._Account = Account.Load(this.AccountID);
      return this._Account;
    }
    set => this._Account = value;
  }

  [DBProp("NotificationEmail")]
  public string NotificationEmail { get; set; }

  [DBProp("NotificationPhone")]
  public string NotificationPhone { get; set; }

  [DBProp("SMSCarrierID")]
  public long SMSCarrierID { get; set; }

  [DBProp("DirectSMS")]
  public bool DirectSMS { get; set; }

  [DBProp("MaintenanceWindowCustomerID")]
  public long MaintenanceWindowCustomerID { get; set; }

  public static List<MaintenanceRecipient> LoadRecipients(long maintenanceWindowID)
  {
    return new Monnit.Data.MaintenanceRecipient.LoadRecipients(maintenanceWindowID).Result;
  }
}
