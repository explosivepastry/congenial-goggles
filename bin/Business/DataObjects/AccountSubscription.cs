// Decompiled with JetBrains decompiler
// Type: Monnit.AccountSubscription
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;

#nullable disable
namespace Monnit;

[DBClass("AccountSubscription")]
public class AccountSubscription : BaseDBObject
{
  private long _AccountSubscriptionID = long.MinValue;
  private long _AccountID = long.MinValue;
  private long _CSNetID = long.MinValue;
  private DateTime _ExpirationDate = DateTime.MinValue;
  private long _AccountSubscriptionTypeID = long.MinValue;
  private Guid _LastKeyUsed = Guid.Empty;
  private AccountSubscriptionType _AccountSubscriptionType = (AccountSubscriptionType) null;
  private string _SubscriptionName = (string) null;

  [DBProp("AccountSubscriptionID", IsPrimaryKey = true)]
  public long AccountSubscriptionID
  {
    get => this._AccountSubscriptionID;
    set => this._AccountSubscriptionID = value;
  }

  [DBForeignKey("Account", "AccountID")]
  [DBProp("AccountID", AllowNull = false)]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("CSNetID", AllowNull = true)]
  public long CSNetID
  {
    get => this._CSNetID;
    set => this._CSNetID = value;
  }

  [DBProp("AccountSubscriptionTypeID", AllowNull = false, DefaultValue = 5)]
  [DBForeignKey("AccountSubscriptionType", "AccountSubscriptionTypeID")]
  public long AccountSubscriptionTypeID
  {
    get => this._AccountSubscriptionTypeID;
    set => this._AccountSubscriptionTypeID = value;
  }

  [DBProp("ExpirationDate")]
  public DateTime ExpirationDate
  {
    get => this._ExpirationDate;
    set => this._ExpirationDate = value;
  }

  [DBProp("LastKeyUsed", AllowNull = true)]
  public Guid LastKeyUsed
  {
    get => this._LastKeyUsed;
    set => this._LastKeyUsed = value;
  }

  public AccountSubscriptionType AccountSubscriptionType
  {
    get
    {
      if (this._AccountSubscriptionType == null)
        this._AccountSubscriptionType = AccountSubscriptionType.Load(this.AccountSubscriptionTypeID);
      return this._AccountSubscriptionType;
    }
  }

  public string SubscriptionName
  {
    get
    {
      if (this._SubscriptionName == null)
        this._SubscriptionName = AccountSubscriptionType.Load(this.AccountSubscriptionTypeID).Name;
      return this._SubscriptionName;
    }
  }

  public bool Can(Feature feature)
  {
    Dictionary<string, Feature> dictionary = Feature.AllowedBySubscriptionType(this.AccountSubscriptionTypeID);
    return feature != null && (dictionary.ContainsKey(feature.KeyName) || this.AccountID.ToString() == ConfigData.AppSettings("AdminAccountID"));
  }

  public bool Can(string featureKeyName) => this.Can(Feature.Find(featureKeyName));

  public static AccountSubscription Load(long ID) => BaseDBObject.Load<AccountSubscription>(ID);

  public static AccountSubscription LoadCurrentByAccountID(long accountID)
  {
    return BaseDBObject.LoadByForeignKey<AccountSubscription>("AccountID", (object) accountID).FirstOrDefault<AccountSubscription>();
  }

  public static List<AccountSubscription> LoadByAccountID(long accountID)
  {
    return BaseDBObject.LoadByForeignKey<AccountSubscription>("AccountID", (object) accountID);
  }

  public static List<AccountSubscription> LoadByCSNetID(long cSNetID)
  {
    return BaseDBObject.LoadByForeignKey<AccountSubscription>("CSNetID", (object) cSNetID);
  }

  public AccountSubscriptionChangeLog NewChangeLog
  {
    get
    {
      return new AccountSubscriptionChangeLog()
      {
        AccountSubscriptionID = this.AccountSubscriptionID,
        OldExpirationDate = this.ExpirationDate,
        NewExpirationDate = this.ExpirationDate
      };
    }
  }

  public bool IsActive
  {
    get
    {
      return this.AccountSubscriptionID > long.MinValue && this.ExpirationDate.AddDays(1.0) > DateTime.UtcNow;
    }
  }

  public static List<AccountSubscription> LoadLastMonth()
  {
    return new Monnit.Data.AccountSubscription.LoadLastMonth().Result;
  }

  public static List<AccountSubscription> LoadLastWeek()
  {
    return new Monnit.Data.AccountSubscription.LoadLastWeek().Result;
  }

  public static List<AccountSubscription> LoadExpired()
  {
    return new Monnit.Data.AccountSubscription.LoadExpired().Result;
  }

  public static List<AccountSubscription> LoadAutoDowngrade()
  {
    return new Monnit.Data.AccountSubscription.LoadAutoDowngrade().Result;
  }

  public static DataTable ExpirationReport() => new Monnit.Data.AccountSubscription.ExpirationReport().Result;

  public static DataTable ResellerExpirationReport(long resellerAccountID)
  {
    return new Monnit.Data.AccountSubscription.ResellerExpirationReport(resellerAccountID).Result;
  }

  public static DataTable ResellerActivation(DateTime utcStart, DateTime utcEnd, long acctID)
  {
    return new Monnit.Data.AccountSubscription.ResellerActivation(utcStart, utcEnd, acctID).Result;
  }

  public static DataTable Missing(long accountSubscriptionTypeID)
  {
    return new Monnit.Data.AccountSubscription.Missing(accountSubscriptionTypeID).Result;
  }

  public void SendMail(EmailTemplate template)
  {
    if (template == null)
      return;
    Account account = Account.Load(this.AccountID);
    Customer primaryContact = account.PrimaryContact;
    using (MailMessage mail = new MailMessage())
    {
      mail.Subject = template.Subject;
      mail.To.Add(new MailAddress(primaryContact.NotificationEmail, $"{primaryContact.FirstName} {primaryContact.LastName}"));
      mail.IsBodyHtml = true;
      string subscriptionName = AccountSubscription.LoadCurrentByAccountID(account.AccountID).SubscriptionName;
      string Content = string.Format("Please note your subscription for Account: <b>{0}</b> is scheduled to expire on {1}.", (object) account.CompanyName, (object) this.ExpirationDate.ToShortDateString(), (object) subscriptionName);
      mail.Body = template.MailMerge(Content, primaryContact.NotificationEmail);
      try
      {
        MonnitUtil.SendMail(mail, account);
      }
      catch (Exception ex)
      {
        throw new Exception("Error sending email " + ex.Message);
      }
    }
  }

  public void UpdateAccountSubscriptionDate(DateTime date, Customer currentUser, string note)
  {
    this.UpdateAccountSubscriptionDate(date, currentUser, note, Guid.Empty);
  }

  public void UpdateAccountSubscriptionDate(
    DateTime date,
    Customer currentUser,
    string note,
    Guid lastKeyUsed)
  {
    AccountSubscriptionChangeLog newChangeLog = this.NewChangeLog;
    if (lastKeyUsed != Guid.Empty)
      this.LastKeyUsed = lastKeyUsed;
    this.ExpirationDate = date;
    this.Save();
    Account account = Account.Load(this.AccountID);
    account.PremiumValidUntil = date;
    account.Save();
    newChangeLog.AccountSubscriptionID = this.AccountSubscriptionID;
    newChangeLog.NewExpirationDate = date;
    newChangeLog.CustomerID = currentUser.CustomerID;
    newChangeLog.ChangeType = "Manual";
    newChangeLog.ChangeNote = note;
    newChangeLog.ChangeDate = DateTime.UtcNow;
    newChangeLog.Save();
  }

  public static void UpdatePremiumDate(
    DateTime date,
    Account act,
    Customer currentUser,
    string note)
  {
    act.CurrentSubscription.UpdateAccountSubscriptionDate(date, currentUser, note);
    AccountSubscription.AccountSubscriptionFeatureChange(act, act.CurrentSubscription);
  }

  public static void AccountSubscriptionFeatureChange(
    Account account,
    AccountSubscription accountSubscription)
  {
    if (accountSubscription != null && accountSubscription.AccountSubscriptionType.Can("muliple_users"))
    {
      foreach (Customer customer in Customer.LoadAllByAccount(account.AccountID))
      {
        try
        {
          if (!customer.IsActive)
          {
            customer.IsActive = true;
            customer.Save();
          }
          if (account.PrimaryContactID == customer.CustomerID)
          {
            foreach (AccountPermission permission in account.Permissions)
            {
              CustomerPermission customerPermission = permission.ToCustomerPermission(customer.CustomerID);
              CustomerPermission.AddOrReplacePermission(customer.Permissions, customerPermission);
              customerPermission.Save();
            }
          }
        }
        catch (Exception ex)
        {
          ex.Log($"AccountSubscriptionFeatureChange Customer set Active failed: CustomerID :{customer.CustomerID.ToString()}, Message: ");
        }
      }
    }
    else
    {
      foreach (Customer customer in Customer.LoadAllByAccount(account.AccountID))
      {
        try
        {
          if (customer.AccountID == account.AccountID)
          {
            if (accountSubscription == null || account.PrimaryContactID != customer.CustomerID)
            {
              customer.IsActive = false;
              customer.ForceLogoutDate = DateTime.UtcNow;
              customer.Save();
            }
          }
        }
        catch (Exception ex)
        {
          ex.Log($"AccountSubscriptionFeatureChange Customer set Inactive failed: CustomerID :{customer.CustomerID.ToString()}, Message: ");
        }
      }
    }
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    if (accountSubscription != null)
    {
      flag1 = accountSubscription.AccountSubscriptionType.Can("sensor_heartbeat_10");
      flag2 = accountSubscription.AccountSubscriptionType.Can("sensor_heartbeat_00");
      flag3 = accountSubscription.AccountSubscriptionType.Can("sensor_heartbeat_01");
    }
    if (!flag1 && !flag3 && !flag2)
    {
      foreach (Sensor sensor in Sensor.LoadByAccountID(account.AccountID))
      {
        if (sensor.ReportInterval < 120.0)
          sensor.ReportInterval = 120.0;
        if (sensor.ActiveStateInterval < 120.0)
          sensor.ActiveStateInterval = 120.0;
        if (sensor.MinimumCommunicationFrequency < 250)
          sensor.MinimumCommunicationFrequency = 250;
        if (sensor.MeasurementsPerTransmission > 6)
          sensor.MeasurementsPerTransmission = 6;
        if (sensor.IsDirty)
          sensor.Save();
        if (sensor.SensorTypeID == 4L || sensor.SensorTypeID == 8L)
        {
          Gateway gateway = Gateway.LoadBySensorID(sensor.SensorID);
          if (gateway != null)
          {
            gateway.ReportInterval = sensor.ReportInterval;
            if (gateway.IsDirty)
              gateway.Save();
          }
        }
      }
    }
    else if (!flag3 && !flag2)
    {
      if (!account.IsHxEnabled || account.HideData)
      {
        foreach (Sensor sensor in Sensor.LoadByAccountID(account.AccountID))
        {
          if (sensor.ReportInterval < 10.0)
            sensor.ReportInterval = 10.0;
          if (sensor.ActiveStateInterval < 10.0)
            sensor.ActiveStateInterval = 10.0;
          if (sensor.MinimumCommunicationFrequency < 25)
            sensor.MinimumCommunicationFrequency = 25;
          if (sensor.MeasurementsPerTransmission > 250)
            sensor.MeasurementsPerTransmission = 250;
          if (sensor.IsDirty)
            sensor.Save();
          if (sensor.SensorTypeID == 4L || sensor.SensorTypeID == 8L)
          {
            Gateway gateway = Gateway.LoadBySensorID(sensor.SensorID);
            if (gateway != null)
            {
              gateway.ReportInterval = sensor.ReportInterval;
              if (gateway.IsDirty)
                gateway.Save();
            }
          }
        }
      }
    }
    else if (!flag3)
    {
      foreach (Sensor sensor in Sensor.LoadByAccountID(account.AccountID))
      {
        if (sensor.ReportInterval < 1.0)
          sensor.ReportInterval = 1.0;
        if (sensor.ActiveStateInterval < 1.0)
          sensor.ActiveStateInterval = 1.0;
        if (sensor.MinimumCommunicationFrequency < 7)
          sensor.MinimumCommunicationFrequency = 7;
        if (sensor.MeasurementsPerTransmission > 25)
          sensor.MeasurementsPerTransmission = 25;
        if (sensor.IsDirty)
          sensor.Save();
        if (sensor.SensorTypeID == 4L || sensor.SensorTypeID == 8L)
        {
          Gateway gateway = Gateway.LoadBySensorID(sensor.SensorID);
          if (gateway != null)
          {
            gateway.ReportInterval = sensor.ReportInterval;
            if (gateway.IsDirty)
              gateway.Save();
          }
        }
      }
    }
    if (accountSubscription == null || !accountSubscription.AccountSubscriptionType.Can("can_webhook_push"))
      return;
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.Load(account.ExternalDataSubscriptionID);
    if (dataSubscription != null && !dataSubscription.IsDeleted)
    {
      dataSubscription.DoSend = true;
      dataSubscription.Save();
    }
  }
}
