// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIUser
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APIUser
{
  public APIUser()
  {
  }

  public APIUser(Customer cust)
  {
    this.UserID = cust.CustomerID;
    this.UserName = cust.UserName;
    this.Title = cust.Title;
    this.FirstName = cust.FirstName;
    this.LastName = cust.LastName;
    this.EmailAddress = cust.NotificationEmail;
    this.SMSNumber = cust.NotificationPhone;
    this.ReceivesNotificationsBySMS = cust.SendSensorNotificationToText;
    this.DirectSMS = cust.DirectSMS;
    this.ExternalSMSProviderID = cust.SMSCarrierID > 0L ? cust.SMSCarrierID : 0L;
    this.VoiceNumber = cust.NotificationPhone2;
    this.ReceivesNotificationsByVoice = cust.SendSensorNotificationToVoice;
    this.Active = cust.IsActive = true;
    this.Admin = cust.IsAdmin;
    this.AccountID = cust.AccountID;
  }

  public long UserID { get; set; }

  public string Title { get; set; }

  public string FirstName { get; set; }

  public string LastName { get; set; }

  public string EmailAddress { get; set; }

  public string SMSNumber { get; set; }

  public string VoiceNumber { get; set; }

  public bool DirectSMS { get; set; }

  public bool ReceivesNotificationsBySMS { get; set; }

  public bool ReceivesNotificationsByVoice { get; set; }

  public bool Active { get; set; }

  public bool Admin { get; set; }

  public bool ReceivesMaintenanceByEmail { get; set; }

  public bool ReceivesMaintenanceBySMS { get; set; }

  public long ExternalSMSProviderID { get; set; }

  public string UserName { get; set; }

  public long AccountID { get; set; }
}
