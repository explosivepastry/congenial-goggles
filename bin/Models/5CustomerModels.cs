// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.PotentialNotificationRecipient
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class PotentialNotificationRecipient : BaseDBObject
{
  [DBProp("AccountID")]
  public long AccountID { get; set; }

  [DBProp("AccountNumber")]
  public string AccountNumber { get; set; }

  [DBProp("CompanyName")]
  public string CompanyName { get; set; }

  [DBProp("CustomerID")]
  public long CustomerID { get; set; }

  [DBProp("UserName")]
  public string UserName { get; set; }

  [DBProp("FirstName")]
  public string FirstName { get; set; }

  [DBProp("LastName")]
  public string LastName { get; set; }

  public string FullName => $"{this.FirstName} {this.LastName}";

  [DBProp("NotificationEmail")]
  public string NotificationEmail { get; set; }

  [DBProp("SendSensorNotificationToText")]
  public bool SendSensorNotificationToText { get; set; }

  [DBProp("NotificationPhone")]
  public string NotificationPhone { get; set; }

  [DBProp("SendSensorNotificationToVoice")]
  public bool SendSensorNotificationToVoice { get; set; }

  [DBProp("NotificationPhone2")]
  public string NotificationPhone2 { get; set; }

  public static List<PotentialNotificationRecipient> Load(
    long customerID,
    long notificationID,
    string query)
  {
    return BaseDBObject.Load<PotentialNotificationRecipient>(Customer.SearchPotentialNotificationRecipient(customerID, notificationID, query));
  }
}
