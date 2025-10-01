// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.NotificationRecipientData
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class NotificationRecipientData : BaseDBObject
{
  private long _AccountID = long.MinValue;
  private string _AccountNumber = string.Empty;
  private string _CompanyName = string.Empty;
  private long _CustomerID = long.MinValue;
  private string _UserName = string.Empty;
  private string _FirstName = string.Empty;
  private string _LastName = string.Empty;
  private string _NotificationEmail = string.Empty;
  private string _NotificationPhone = string.Empty;
  private string _NotificationPhone2 = string.Empty;
  private bool _SendSensorNotificationToText = false;
  private bool _SendSensorNotificationToVoice = false;
  private int _Level = int.MinValue;
  private bool _EmailActive = false;
  private int _EmailDelay = int.MinValue;
  private bool _TextActive = false;
  private int _TextDelay = int.MinValue;
  private bool _PhoneActive = false;
  private int _PhoneDelay = int.MinValue;
  private bool _GroupActive = false;
  private int _CustomerGroupID = int.MinValue;

  public string NotificationRecipientDataID
  {
    get
    {
      return (this.CustomerID < 0L ? string.Empty : this.CustomerID.ToString()) + (this.CustomerGroupID < 0 ? string.Empty : this.CustomerGroupID.ToString());
    }
  }

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

  [DBProp("NotificationPhone", MaxLength = 255 /*0xFF*/)]
  public string NotificationPhone
  {
    get => this._NotificationPhone;
    set
    {
      if (value == null)
        this._NotificationPhone = string.Empty;
      else
        this._NotificationPhone = value;
    }
  }

  [DBProp("NotificationPhone2", MaxLength = 255 /*0xFF*/)]
  public string NotificationPhone2
  {
    get => this._NotificationPhone2;
    set
    {
      if (value == null)
        this._NotificationPhone2 = string.Empty;
      else
        this._NotificationPhone2 = value;
    }
  }

  [DBProp("SendSensorNotificationToText", DefaultValue = true)]
  public bool SendSensorNotificationToText
  {
    get => this._SendSensorNotificationToText;
    set => this._SendSensorNotificationToText = value;
  }

  [DBProp("SendSensorNotificationToVoice")]
  public bool SendSensorNotificationToVoice
  {
    get => this._SendSensorNotificationToVoice;
    set => this._SendSensorNotificationToVoice = value;
  }

  [DBProp("Level")]
  public int Level
  {
    get => this._Level;
    set => this._Level = value;
  }

  [DBProp("EmailActive")]
  public bool EmailActive
  {
    get => this._EmailActive;
    set => this._EmailActive = value;
  }

  [DBProp("EmailDelay")]
  public int EmailDelay
  {
    get => this._EmailDelay;
    set => this._EmailDelay = value;
  }

  [DBProp("TextActive")]
  public bool TextActive
  {
    get => this._TextActive;
    set => this._TextActive = value;
  }

  [DBProp("TextDelay")]
  public int TextDelay
  {
    get => this._TextDelay;
    set => this._TextDelay = value;
  }

  [DBProp("PhoneActive")]
  public bool PhoneActive
  {
    get => this._PhoneActive;
    set => this._PhoneActive = value;
  }

  [DBProp("PhoneDelay")]
  public int PhoneDelay
  {
    get => this._PhoneDelay;
    set => this._PhoneDelay = value;
  }

  [DBProp("GroupActive")]
  public bool GroupActive
  {
    get => this._GroupActive;
    set => this._GroupActive = value;
  }

  [DBProp("CustomerGroupID")]
  public int CustomerGroupID
  {
    get => this._CustomerGroupID;
    set => this._CustomerGroupID = value;
  }

  public string FullName => $"{this.FirstName} {this.LastName}";

  public static List<NotificationRecipientData> SearchPotentialNotificationRecipient(
    long customerID,
    long notificationID,
    string query)
  {
    return new Data.NotificationRecipientData.SearchPotentialNotificationRecipient(customerID, notificationID, query).Result;
  }

  public static List<NotificationRecipientData> SearchPotentialPushMessageRecipient(
    long customerID,
    long notificationID,
    string query)
  {
    return new Data.NotificationRecipientData.SearchPotentialNotificationRecipient(customerID, notificationID, query, searchPushMsgRecipient: true).Result;
  }

  public static List<NotificationRecipientData> SearchPotentialPushMessageRecipientForAccount(
    long customerID,
    long accountID,
    string query)
  {
    return new Data.NotificationRecipientData.SearchPotentialNotificationRecipient(customerID, long.MinValue, query, accountID, true).Result;
  }

  public static List<NotificationRecipientData> SearchPotentialRecipient(
    long customerID,
    long accountID,
    string query)
  {
    return new Data.NotificationRecipientData.SearchPotentialNotificationRecipient(customerID, long.MinValue, query, accountID).Result;
  }
}
