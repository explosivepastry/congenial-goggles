// Decompiled with JetBrains decompiler
// Type: Monnit.CreditSetting
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Linq;
using System.Web;

#nullable disable
namespace Monnit;

[DBClass("CreditSetting")]
public class CreditSetting : BaseDBObject
{
  private long _CreditSettingID = long.MinValue;
  private long _AccountID = long.MinValue;
  private long _UserId = long.MinValue;
  private long _CreditCompareValue = long.MinValue;
  private DateTime _LastEmailDate = DateTime.UtcNow - new TimeSpan(24, 0, 0);
  private eCreditClassification _CreditClassification = eCreditClassification.Notification;

  [DBProp("CreditSettingID", IsPrimaryKey = true)]
  public long CreditSettingID
  {
    get => this._CreditSettingID;
    set => this._CreditSettingID = value;
  }

  [DBProp("AccountID")]
  [DBForeignKey("Account", "AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("UserId")]
  [DBForeignKey("Customer", "CustomerId")]
  public long UserId
  {
    get => this._UserId;
    set => this._UserId = value;
  }

  public long CreditsAvailable
  {
    get
    {
      int creditClassification = (int) this.CreditClassification;
      return this.CreditClassification == eCreditClassification.Notification ? (long) NotificationCredit.LoadRemainingCreditsForAccount(this.AccountID) : (long) Credit.LoadRemainingCreditsForAccount(this.AccountID, this.CreditClassification);
    }
  }

  [DBProp("CreditCompareValue")]
  public long CreditCompareValue
  {
    get => this._CreditCompareValue;
    set => this._CreditCompareValue = value;
  }

  [DBProp("LastEmailDate")]
  public DateTime LastEmailDate
  {
    get => this._LastEmailDate;
    set => this._LastEmailDate = value;
  }

  [DBProp("CreditClassification", AllowNull = false)]
  public eCreditClassification CreditClassification
  {
    get => this._CreditClassification;
    set => this._CreditClassification = value;
  }

  public static void SendCreditNotification(CreditSetting cs)
  {
    cs.LastEmailDate = DateTime.UtcNow;
    cs.Save();
    EmailTemplate template = CreditSetting.FindTemplate(Account.Load(cs.AccountID));
    Customer customer = Customer.Load(cs.UserId);
    PacketCache localCache = new PacketCache();
    NotificationRecorded notificationRecorded = new NotificationRecorded();
    notificationRecorded.NotificationType = eNotificationType.Email;
    notificationRecorded.CustomerID = customer.CustomerID;
    notificationRecorded.NotificationDate = cs.LastEmailDate;
    string str1 = "Your notification credits for your account are running low";
    string newValue = $"Your current notification credit balance is {cs.CreditsAvailable} credits.<br/>You requested to be notified when your credit balance fell below {cs.CreditCompareValue} credits. Please purchase more notification credits for your account to continue receiving enhanced notifications. If your credit balance reaches zero, all notifications will be sent via email only until additional credits are added.";
    string str2 = template.Template.Replace("{Content}", newValue);
    notificationRecorded.NotificationText = str2;
    notificationRecorded.NotificationSubject = str1;
    notificationRecorded.Save();
    Notification.SendNotification(notificationRecorded, localCache);
  }

  public static void CheckCreditsRemaining(long accountID, Customer currentUser)
  {
    CreditSetting cs = CreditSetting.LoadByAccountID(accountID, new eCreditClassification?(eCreditClassification.Notification));
    Account account = Account.Load(accountID);
    int num = NotificationCredit.LoadRemainingCreditsForAccount(accountID);
    DateTime utcNow = DateTime.UtcNow;
    string IPAddress = PurchaseBase.ClientIP(HttpContext.Current.Request);
    string storeLinkGuid = account.StoreLinkGuid.ToString();
    long storeUserId = account.StoreUserID;
    string sku = "MNW-NC-100";
    int qty = 1;
    long defaultPaymentId = account.DefaultPaymentID;
    string sessionId = HttpContext.Current.Session.SessionID;
    AccountSubscription currentSubscription = account.CurrentSubscription;
    double tax = MonnitUtil.RetrieveItemTaxValue(sku, qty, defaultPaymentId, storeUserId, sessionId);
    if (cs.CreditCompareValue <= (long) num)
      return;
    if (!account.AutoPurchase)
    {
      if (cs.LastEmailDate < DateTime.UtcNow.AddDays(-1.0))
        CreditSetting.SendCreditNotification(cs);
    }
    else
      PurchaseBase.BasePurchase(accountID, utcNow, IPAddress, storeLinkGuid, storeUserId, currentUser, account, defaultPaymentId, sku, tax, qty, currentUser.NotificationEmail, sessionId, currentSubscription);
  }

  private static EmailTemplate FindTemplate(Account account)
  {
    return EmailTemplate.LoadBest(account, eEmailTemplateFlag.Generic) ?? new EmailTemplate();
  }

  public static CreditSetting LoadByCustomerID(long customerID)
  {
    return BaseDBObject.LoadByForeignKey<CreditSetting>("UserID", (object) customerID).FirstOrDefault<CreditSetting>();
  }

  public static CreditSetting LoadByAccountID(long accountID)
  {
    return CreditSetting.LoadByAccountID(accountID, new eCreditClassification?(eCreditClassification.Notification));
  }

  public static CreditSetting LoadByAccountID(
    long accountID,
    eCreditClassification? creditClassification)
  {
    if (!creditClassification.HasValue)
      creditClassification = new eCreditClassification?(eCreditClassification.Notification);
    return BaseDBObject.LoadByForeignKey<CreditSetting>("AccountID", (object) accountID).Where<CreditSetting>((Func<CreditSetting, bool>) (c =>
    {
      int creditClassification1 = (int) c.CreditClassification;
      eCreditClassification? nullable = creditClassification;
      int valueOrDefault = (int) nullable.GetValueOrDefault();
      return creditClassification1 == valueOrDefault & nullable.HasValue;
    })).FirstOrDefault<CreditSetting>();
  }

  public static CreditSetting Load(long id)
  {
    if (id == long.MinValue)
      return new CreditSetting();
    return BaseDBObject.Load<CreditSetting>(id);
  }
}
