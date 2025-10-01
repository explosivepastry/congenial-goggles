// Decompiled with JetBrains decompiler
// Type: Monnit.ExternalSubscriptionPreference
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace Monnit;

[DBClass("ExternalSubscriptionPreference")]
public class ExternalSubscriptionPreference : BaseDBObject
{
  private long _ExternalSubscriptionPreferenceID = long.MinValue;
  private long _AccountID = long.MinValue;
  private long _UserId = long.MinValue;
  private DateTime _LastEmailDate = DateTime.MinValue;
  private DateTime _FullStopEmailDate = DateTime.MinValue;
  private int _BrokenCountThreshold = ExternalDataSubscription.killRetryLimit;
  public const string sensorStr = "Sensor ";
  public const string gatewayStr = "Gateway ";
  public const string accountStr = "Account ";
  public const string formatSubject = "Your Webhook has exceeded the possible failed sends";
  public const string formatText = "The reason for this email is to inform you that {1} may have exceeded the possible send failures for Webhook. As a result, your Webhook has been disabled.<br/>Common reasons for this issue include:<br/> &nbsp; &nbsp; &nbsp; 1) Not being able to connect to your server.<br/> &nbsp &nbsp &nbsp 2) The server is responding with something other than 200 status. To see the error that was recorded, login and view the webhook history. After this, you can reset your webhook by visiting the 'Configure Webhook' page and clicking the 'Reset Broken Send'.";
  public const string formatCustomText = "The reason for this email is to inform you that {1} may have exceeded your limit of {3} send failures for Webhook. As a result, your Webhook has been disabled.<br/>Common reasons for this issue include:<br/> &nbsp; &nbsp; &nbsp; 1) Not being able to connect to your server.<br/> &nbsp; &nbsp; &nbsp; 2) The server is responding with something other than 200 status. To see the error that was recorded, login and view the webhook history. After this, you can reset your webhook by visiting the 'Configure Webhook' page and clicking the 'Reset Broken Send'.";

  [DBProp("ExternalSubscriptionPreferenceID", IsPrimaryKey = true)]
  public long ExternalSubscriptionPreferenceID
  {
    get => this._ExternalSubscriptionPreferenceID;
    set => this._ExternalSubscriptionPreferenceID = value;
  }

  [DBProp("AccountID")]
  [DBForeignKey("Account", "AccountID")]
  [Required]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("UserId")]
  [DBForeignKey("Customer", "CustomerId")]
  [Required]
  public long UserId
  {
    get => this._UserId;
    set => this._UserId = value;
  }

  [DBProp("LastEmailDate")]
  public DateTime LastEmailDate
  {
    get => this._LastEmailDate;
    set => this._LastEmailDate = value;
  }

  [DBProp("FullStopEmailDate")]
  public DateTime KillSendUserNotifiedDate
  {
    get => this._FullStopEmailDate;
    set => this._FullStopEmailDate = value;
  }

  [DBProp("BrokenCountThreshold")]
  [Required]
  public int UsersBrokenCountLimit
  {
    get => this._BrokenCountThreshold;
    set => this._BrokenCountThreshold = value;
  }

  public void SendBrokenEmail(ExternalDataSubscription eds, string formatBody = "The reason for this email is to inform you that {1} may have exceeded the possible send failures for Webhook. As a result, your Webhook has been disabled.<br/>Common reasons for this issue include:<br/> &nbsp; &nbsp; &nbsp; 1) Not being able to connect to your server.<br/> &nbsp &nbsp &nbsp 2) The server is responding with something other than 200 status. To see the error that was recorded, login and view the webhook history. After this, you can reset your webhook by visiting the 'Configure Webhook' page and clicking the 'Reset Broken Send'.")
  {
    if (formatBody == "The reason for this email is to inform you that {1} may have exceeded the possible send failures for Webhook. As a result, your Webhook has been disabled.<br/>Common reasons for this issue include:<br/> &nbsp; &nbsp; &nbsp; 1) Not being able to connect to your server.<br/> &nbsp &nbsp &nbsp 2) The server is responding with something other than 200 status. To see the error that was recorded, login and view the webhook history. After this, you can reset your webhook by visiting the 'Configure Webhook' page and clicking the 'Reset Broken Send'.")
      this.KillSendUserNotifiedDate = DateTime.UtcNow;
    else
      this.LastEmailDate = DateTime.UtcNow;
    this.Save();
    Account account1 = Account.Load(this.AccountID);
    if (account1 == null)
      return;
    EmailTemplate template = this.FindTemplate(account1);
    if (template == null)
      return;
    Customer customer = Customer.Load(this.UserId);
    if (!AccountTheme.Find(customer.Account).SendWebhookNotification)
      return;
    PacketCache localCache = new PacketCache();
    NotificationRecorded notificationRecorded = new NotificationRecorded();
    notificationRecorded.NotificationType = eNotificationType.Email;
    notificationRecorded.CustomerID = this.UserId;
    notificationRecorded.NotificationDate = DateTime.UtcNow;
    Account account2 = Account.Load(eds.AccountID);
    notificationRecorded.NotificationSubject = string.Format("Your Webhook has exceeded the possible failed sends", (object) "Account ", (object) account2.CompanyName, (object) eds.BrokenCount, (object) this.UsersBrokenCountLimit);
    string Content = string.Format(formatBody, (object) "Account ", (object) account2.CompanyName, (object) eds.BrokenCount, (object) this.UsersBrokenCountLimit);
    notificationRecorded.NotificationText = template.MailMerge(Content, customer.NotificationEmail);
    notificationRecorded.Save();
    Notification.SendNotification(notificationRecorded, localCache);
  }

  public void SendCustomBrokenEmail(ExternalDataSubscription eds)
  {
    Account.Load(eds.AccountID);
    this.SendBrokenEmail(eds, "The reason for this email is to inform you that {1} may have exceeded your limit of {3} send failures for Webhook. As a result, your Webhook has been disabled.<br/>Common reasons for this issue include:<br/> &nbsp; &nbsp; &nbsp; 1) Not being able to connect to your server.<br/> &nbsp; &nbsp; &nbsp; 2) The server is responding with something other than 200 status. To see the error that was recorded, login and view the webhook history. After this, you can reset your webhook by visiting the 'Configure Webhook' page and clicking the 'Reset Broken Send'.");
  }

  private EmailTemplate FindTemplate(Account account)
  {
    return EmailTemplate.LoadBest(account, eEmailTemplateFlag.Generic) ?? new EmailTemplate();
  }

  public static ExternalSubscriptionPreference Load(long id)
  {
    return BaseDBObject.Load<ExternalSubscriptionPreference>(id);
  }

  public static ExternalSubscriptionPreference LoadByAccountId(long id)
  {
    List<ExternalSubscriptionPreference> subscriptionPreferenceList = BaseDBObject.LoadByForeignKey<ExternalSubscriptionPreference>("AccountID", (object) id);
    return subscriptionPreferenceList.Count == 0 ? (ExternalSubscriptionPreference) null : subscriptionPreferenceList[0];
  }

  public string LargestBrokenCount()
  {
    int num = 0;
    foreach (ExternalDataSubscription dataSubscription in ExternalDataSubscription.LoadAllByAccountID(this.AccountID))
      num += dataSubscription.BrokenCount;
    return num.ToString();
  }
}
