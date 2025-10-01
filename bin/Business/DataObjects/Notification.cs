// Decompiled with JetBrains decompiler
// Type: Monnit.Notification
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebPush;

#nullable disable
namespace Monnit;

[MetadataType(typeof (NotificationMetadata))]
[DBClass("Notification")]
public class Notification : BaseDBObject
{
  private long _NotificationID = long.MinValue;
  private eNotificationClass _NotificationClass;
  private long _MonnitApplicationID = long.MinValue;
  private long _SensorID = long.MinValue;
  private long _GatewayID = long.MinValue;
  private eNotificationType _NotificationType;
  private string _NotificationText = string.Empty;
  private string _SMSText = string.Empty;
  private string _VoiceText = string.Empty;
  private string _LocalAlertText = string.Empty;
  private eCompareType _CompareType;
  private int _DatumIndex = 0;
  private string _Version = string.Empty;
  private string _CompareValue = string.Empty;
  private int _SnoozeDuration = 240 /*0xF0*/;
  private bool _ApplySnoozeByTriggerDevice = true;
  private DateTime _LastSent = DateTime.MinValue;
  private string _Name = string.Empty;
  private string _Subject = "";
  private bool _AlwaysSend = true;
  private TimeSpan _StartTime = TimeSpan.MinValue;
  private TimeSpan _EndTime = TimeSpan.MinValue;
  private long _MondayScheduleID = long.MinValue;
  private NotificationSchedule _MondaySchedule = (NotificationSchedule) null;
  private long _TuesdayScheduleID = long.MinValue;
  private NotificationSchedule _TuesdaySchedule = (NotificationSchedule) null;
  private long _WednesdayScheduleID = long.MinValue;
  private NotificationSchedule _WednesdaySchedule = (NotificationSchedule) null;
  private long _ThursdayScheduleID = long.MinValue;
  private NotificationSchedule _ThursdaySchedule = (NotificationSchedule) null;
  private long _FridayScheduleID = long.MinValue;
  private NotificationSchedule _FridaySchedule = (NotificationSchedule) null;
  private long _SaturdayScheduleID = long.MinValue;
  private NotificationSchedule _SaturdaySchedule = (NotificationSchedule) null;
  private long _SundayScheduleID = long.MinValue;
  private NotificationSchedule _SundaySchedule = (NotificationSchedule) null;
  private bool _CanAutoAcknowledge = true;
  private bool _IsDeleted = false;
  private bool _IsActive = true;
  private long _AdvancedNotificationID = long.MinValue;
  private long _AccountID = long.MinValue;
  private string _Scale = string.Empty;
  private int _ScaleID = 0;
  private long _NotificationByTimeID = long.MinValue;
  private eDatumType _eDatumType = eDatumType.Error;
  private bool _HasUserNotificationAction = false;
  private bool _HasControlAction = false;
  private bool _HasLocalAlertAction = false;
  private bool _HasSystemAction = false;
  private bool _HasThermostatAction = false;
  private bool _HasResetAccAction = false;
  private string _PushMsgText = string.Empty;
  private bool _IgnoreMaintenanceWindow = false;
  private List<NotificationRecipient> _NotificationRecipients = (List<NotificationRecipient>) null;
  private List<NotificationRecipient> _SystemActionList = (List<NotificationRecipient>) null;
  private NotificationByTime _NotificationByTime = (NotificationByTime) null;
  private List<SensorDatum> _SensorsToNotify = (List<SensorDatum>) null;
  private List<Gateway> _GatewaysThatNotify = (List<Gateway>) null;

  [DBProp("NotificationID", IsPrimaryKey = true)]
  public long NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [DBProp("eNotificationClass")]
  public eNotificationClass NotificationClass
  {
    get => this._NotificationClass;
    set => this._NotificationClass = value;
  }

  [DBProp("ApplicationID", AllowNull = true)]
  [DBForeignKey("Application", "ApplicationID")]
  public long MonnitApplicationID
  {
    get => this._MonnitApplicationID;
    set => this._MonnitApplicationID = value;
  }

  public long ApplicationID
  {
    get => this.MonnitApplicationID;
    set => this.MonnitApplicationID = value;
  }

  [DBForeignKey("Sensor", "SensorID")]
  [DBProp("SensorID")]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBForeignKey("Gateway", "GatewayID")]
  [DBProp("GatewayID")]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  public eNotificationType NotificationType
  {
    get => this._NotificationType;
    set => this._NotificationType = value;
  }

  [AllowHtml]
  [DBProp("NotificationText", MaxLength = 2000, International = true)]
  public string NotificationText
  {
    get => this._NotificationText;
    set
    {
      if (value == null)
        this._NotificationText = string.Empty;
      else
        this._NotificationText = value;
    }
  }

  [AllowHtml]
  [DBProp("SMSText", MaxLength = 255 /*0xFF*/, AllowNull = true, International = true)]
  public string SMSText
  {
    get => this._SMSText;
    set
    {
      if (value == null)
        this._SMSText = string.Empty;
      else
        this._SMSText = value;
    }
  }

  [AllowHtml]
  [DBProp("VoiceText", MaxLength = 2000, AllowNull = true, International = true)]
  public string VoiceText
  {
    get => this._VoiceText;
    set
    {
      if (value == null)
        this._VoiceText = string.Empty;
      else
        this._VoiceText = value;
    }
  }

  [AllowHtml]
  [DBProp("LocalAlertText", MaxLength = 255 /*0xFF*/, AllowNull = true, International = false)]
  public string LocalAlertText
  {
    get => this._LocalAlertText;
    set
    {
      if (value == null)
        this._LocalAlertText = string.Empty;
      else
        this._LocalAlertText = value;
    }
  }

  [DBProp("eCompareType", AllowNull = false)]
  public eCompareType CompareType
  {
    get => this._CompareType;
    set => this._CompareType = value;
  }

  [DBProp("DatumIndex", AllowNull = false, DefaultValue = 0)]
  public int DatumIndex
  {
    get => this._DatumIndex;
    set => this._DatumIndex = value;
  }

  [DBProp("Version", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string Version
  {
    get => this._Version;
    set
    {
      if (value == null)
        this._Version = string.Empty;
      else
        this._Version = value;
    }
  }

  [DBProp("CompareValue", AllowNull = false, MaxLength = -1)]
  public string CompareValue
  {
    get => this._CompareValue;
    set
    {
      if (value == null)
        this._CompareValue = string.Empty;
      else
        this._CompareValue = value;
    }
  }

  [DBProp("SnoozeDuration", AllowNull = false, DefaultValue = 240 /*0xF0*/)]
  public int SnoozeDuration
  {
    get => this._SnoozeDuration;
    set => this._SnoozeDuration = value;
  }

  [DBProp("ApplySnoozeByTriggerDevice")]
  public bool ApplySnoozeByTriggerDevice
  {
    get => this._ApplySnoozeByTriggerDevice;
    set => this._ApplySnoozeByTriggerDevice = value;
  }

  [DBProp("LastSent")]
  public DateTime LastSent
  {
    get => this._LastSent;
    set => this._LastSent = value;
  }

  [AllowHtml]
  [DBProp("Name", AllowNull = true, MaxLength = 255 /*0xFF*/, International = true)]
  public string Name
  {
    get
    {
      if (string.IsNullOrEmpty(this._Name))
        this._Name = "";
      return this._Name;
    }
    set
    {
      if (value == null)
        this._Name = string.Empty;
      else
        this._Name = value;
    }
  }

  [AllowHtml]
  [DBProp("Subject", AllowNull = true, International = true)]
  public string Subject
  {
    get => this._Subject;
    set => this._Subject = value;
  }

  [DBProp("AlwaysSend", DefaultValue = true)]
  public bool AlwaysSend
  {
    get => this._AlwaysSend;
    set => this._AlwaysSend = value;
  }

  [DBProp("StartTime", AllowNull = true)]
  public TimeSpan StartTime
  {
    get => this._StartTime;
    set => this._StartTime = value;
  }

  [DBProp("EndTime", AllowNull = true)]
  public TimeSpan EndTime
  {
    get => this._EndTime;
    set => this._EndTime = value;
  }

  [DBForeignKey("NotificationSchedule", "NotificationScheduleID")]
  [DBProp("MondayScheduleID", AllowNull = true)]
  public long MondayScheduleID
  {
    get => this._MondayScheduleID;
    set => this._MondayScheduleID = value;
  }

  public NotificationSchedule MondaySchedule
  {
    get
    {
      if (this._MondaySchedule == null)
      {
        this._MondaySchedule = NotificationSchedule.Load(this._MondayScheduleID);
        if (this._MondaySchedule == null)
        {
          this._MondaySchedule = new NotificationSchedule();
          this._MondaySchedule.DayofWeek = DayOfWeek.Monday;
          this._MondaySchedule.FirstTime = this.StartTime;
          this._MondaySchedule.SecondTime = this.EndTime;
          if (this.StartTime == this.EndTime)
            this._MondaySchedule.NotificationDaySchedule = eNotificationDaySchedule.All_Day;
          else if (this.StartTime > this.EndTime)
          {
            this._MondaySchedule.NotificationDaySchedule = eNotificationDaySchedule.Before_and_After;
            this._MondaySchedule.FirstTime = this.EndTime;
            this._MondaySchedule.SecondTime = this.StartTime;
          }
          else
            this._MondaySchedule.NotificationDaySchedule = !(this.StartTime < this.EndTime) ? eNotificationDaySchedule.All_Day : eNotificationDaySchedule.Between;
        }
      }
      return this._MondaySchedule;
    }
    set => this._MondaySchedule = value;
  }

  [DBForeignKey("NotificationSchedule", "NotificationScheduleID")]
  [DBProp("TuesdayScheduleID", AllowNull = true)]
  public long TuesdayScheduleID
  {
    get => this._TuesdayScheduleID;
    set => this._TuesdayScheduleID = value;
  }

  public NotificationSchedule TuesdaySchedule
  {
    get
    {
      if (this._TuesdaySchedule == null)
      {
        this._TuesdaySchedule = NotificationSchedule.Load(this._TuesdayScheduleID);
        if (this._TuesdaySchedule == null)
        {
          this._TuesdaySchedule = new NotificationSchedule();
          this._TuesdaySchedule.DayofWeek = DayOfWeek.Tuesday;
          this._TuesdaySchedule.FirstTime = this.StartTime;
          this._TuesdaySchedule.SecondTime = this.EndTime;
          if (this.StartTime == this.EndTime)
            this._TuesdaySchedule.NotificationDaySchedule = eNotificationDaySchedule.All_Day;
          else if (this.StartTime > this.EndTime)
          {
            this._TuesdaySchedule.NotificationDaySchedule = eNotificationDaySchedule.Before_and_After;
            this._TuesdaySchedule.FirstTime = this.EndTime;
            this._TuesdaySchedule.SecondTime = this.StartTime;
          }
          else
            this._TuesdaySchedule.NotificationDaySchedule = !(this.StartTime < this.EndTime) ? eNotificationDaySchedule.All_Day : eNotificationDaySchedule.Between;
        }
      }
      return this._TuesdaySchedule;
    }
    set => this._TuesdaySchedule = value;
  }

  [DBForeignKey("NotificationSchedule", "NotificationScheduleID")]
  [DBProp("WednesdayScheduleID", AllowNull = true)]
  public long WednesdayScheduleID
  {
    get => this._WednesdayScheduleID;
    set => this._WednesdayScheduleID = value;
  }

  public NotificationSchedule WednesdaySchedule
  {
    get
    {
      if (this._WednesdaySchedule == null)
      {
        this._WednesdaySchedule = NotificationSchedule.Load(this._WednesdayScheduleID);
        if (this._WednesdaySchedule == null)
        {
          this._WednesdaySchedule = new NotificationSchedule();
          this._WednesdaySchedule.DayofWeek = DayOfWeek.Wednesday;
          this._WednesdaySchedule.FirstTime = this.StartTime;
          this._WednesdaySchedule.NotificationDaySchedule = eNotificationDaySchedule.All_Day;
          this._WednesdaySchedule.SecondTime = this.EndTime;
          if (this.StartTime == this.EndTime)
            this._WednesdaySchedule.NotificationDaySchedule = eNotificationDaySchedule.All_Day;
          else if (this.StartTime > this.EndTime)
          {
            this._WednesdaySchedule.NotificationDaySchedule = eNotificationDaySchedule.Before_and_After;
            this._WednesdaySchedule.FirstTime = this.EndTime;
            this._WednesdaySchedule.SecondTime = this.StartTime;
          }
          else
            this._WednesdaySchedule.NotificationDaySchedule = !(this.StartTime < this.EndTime) ? eNotificationDaySchedule.All_Day : eNotificationDaySchedule.Between;
        }
      }
      return this._WednesdaySchedule;
    }
    set => this._WednesdaySchedule = value;
  }

  [DBForeignKey("NotificationSchedule", "NotificationScheduleID")]
  [DBProp("ThursdayScheduleID", AllowNull = true)]
  public long ThursdayScheduleID
  {
    get => this._ThursdayScheduleID;
    set => this._ThursdayScheduleID = value;
  }

  public NotificationSchedule ThursdaySchedule
  {
    get
    {
      if (this._ThursdaySchedule == null)
      {
        this._ThursdaySchedule = NotificationSchedule.Load(this._ThursdayScheduleID);
        if (this._ThursdaySchedule == null)
        {
          this._ThursdaySchedule = new NotificationSchedule();
          this._ThursdaySchedule.DayofWeek = DayOfWeek.Thursday;
          this._ThursdaySchedule.FirstTime = this.StartTime;
          this._ThursdaySchedule.SecondTime = this.EndTime;
          if (this.StartTime == this.EndTime)
            this._ThursdaySchedule.NotificationDaySchedule = eNotificationDaySchedule.All_Day;
          else if (this.StartTime > this.EndTime)
          {
            this._ThursdaySchedule.NotificationDaySchedule = eNotificationDaySchedule.Before_and_After;
            this._ThursdaySchedule.FirstTime = this.EndTime;
            this._ThursdaySchedule.SecondTime = this.StartTime;
          }
          else
            this._ThursdaySchedule.NotificationDaySchedule = !(this.StartTime < this.EndTime) ? eNotificationDaySchedule.All_Day : eNotificationDaySchedule.Between;
        }
      }
      return this._ThursdaySchedule;
    }
    set => this._ThursdaySchedule = value;
  }

  [DBForeignKey("NotificationSchedule", "NotificationScheduleID")]
  [DBProp("FridayScheduleID", AllowNull = true)]
  public long FridayScheduleID
  {
    get => this._FridayScheduleID;
    set => this._FridayScheduleID = value;
  }

  public NotificationSchedule FridaySchedule
  {
    get
    {
      if (this._FridaySchedule == null)
      {
        this._FridaySchedule = NotificationSchedule.Load(this._FridayScheduleID);
        if (this._FridaySchedule == null)
        {
          this._FridaySchedule = new NotificationSchedule();
          this._FridaySchedule.DayofWeek = DayOfWeek.Friday;
          this._FridaySchedule.FirstTime = this.StartTime;
          this._FridaySchedule.SecondTime = this.EndTime;
          if (this.StartTime == this.EndTime)
            this._FridaySchedule.NotificationDaySchedule = eNotificationDaySchedule.All_Day;
          else if (this.StartTime > this.EndTime)
          {
            this._FridaySchedule.NotificationDaySchedule = eNotificationDaySchedule.Before_and_After;
            this._FridaySchedule.FirstTime = this.EndTime;
            this._FridaySchedule.SecondTime = this.StartTime;
          }
          else
            this._FridaySchedule.NotificationDaySchedule = !(this.StartTime < this.EndTime) ? eNotificationDaySchedule.All_Day : eNotificationDaySchedule.Between;
        }
      }
      return this._FridaySchedule;
    }
    set => this._FridaySchedule = value;
  }

  [DBForeignKey("NotificationSchedule", "NotificationScheduleID")]
  [DBProp("SaturdayScheduleID", AllowNull = true)]
  public long SaturdayScheduleID
  {
    get => this._SaturdayScheduleID;
    set => this._SaturdayScheduleID = value;
  }

  public NotificationSchedule SaturdaySchedule
  {
    get
    {
      if (this._SaturdaySchedule == null)
      {
        this._SaturdaySchedule = NotificationSchedule.Load(this._SaturdayScheduleID);
        if (this._SaturdaySchedule == null)
        {
          this._SaturdaySchedule = new NotificationSchedule();
          this._SaturdaySchedule.DayofWeek = DayOfWeek.Saturday;
          this._SaturdaySchedule.FirstTime = this.StartTime;
          this._SaturdaySchedule.SecondTime = this.EndTime;
          if (this.StartTime == this.EndTime)
            this._SaturdaySchedule.NotificationDaySchedule = eNotificationDaySchedule.All_Day;
          else if (this.StartTime > this.EndTime)
          {
            this._SaturdaySchedule.NotificationDaySchedule = eNotificationDaySchedule.Before_and_After;
            this._SaturdaySchedule.FirstTime = this.EndTime;
            this._SaturdaySchedule.SecondTime = this.StartTime;
          }
          else
            this._SaturdaySchedule.NotificationDaySchedule = !(this.StartTime < this.EndTime) ? eNotificationDaySchedule.All_Day : eNotificationDaySchedule.Between;
        }
      }
      return this._SaturdaySchedule;
    }
    set => this._SaturdaySchedule = value;
  }

  [DBForeignKey("NotificationSchedule", "NotificationScheduleID")]
  [DBProp("SundayScheduleID", AllowNull = true)]
  public long SundayScheduleID
  {
    get => this._SundayScheduleID;
    set => this._SundayScheduleID = value;
  }

  public NotificationSchedule SundaySchedule
  {
    get
    {
      if (this._SundaySchedule == null)
      {
        this._SundaySchedule = NotificationSchedule.Load(this._SundayScheduleID);
        if (this._SundaySchedule == null)
        {
          this._SundaySchedule = new NotificationSchedule();
          this._SundaySchedule.DayofWeek = DayOfWeek.Sunday;
          this._SundaySchedule.FirstTime = this.StartTime;
          this._SundaySchedule.SecondTime = this.EndTime;
          if (this.StartTime == this.EndTime)
            this._SundaySchedule.NotificationDaySchedule = eNotificationDaySchedule.All_Day;
          else if (this.StartTime > this.EndTime)
          {
            this._SundaySchedule.NotificationDaySchedule = eNotificationDaySchedule.Before_and_After;
            this._SundaySchedule.FirstTime = this.EndTime;
            this._SundaySchedule.SecondTime = this.StartTime;
          }
          else
            this._SundaySchedule.NotificationDaySchedule = !(this.StartTime < this.EndTime) ? eNotificationDaySchedule.All_Day : eNotificationDaySchedule.Between;
        }
      }
      return this._SundaySchedule;
    }
    set => this._SundaySchedule = value;
  }

  [DBProp("CanAutoAcknowledge", DefaultValue = true)]
  public bool CanAutoAcknowledge
  {
    get => this._CanAutoAcknowledge;
    set => this._CanAutoAcknowledge = value;
  }

  [DBProp("IsDeleted", DefaultValue = false)]
  public bool IsDeleted
  {
    get => this._IsDeleted;
    set => this._IsDeleted = value;
  }

  [DBProp("IsActive", DefaultValue = true)]
  public bool IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [DBForeignKey("AdvancedNotification", "AdvancedNotificationID")]
  [DBProp("AdvancedNotificationID", AllowNull = true)]
  public long AdvancedNotificationID
  {
    get => this._AdvancedNotificationID;
    set => this._AdvancedNotificationID = value;
  }

  [DBProp("AccountID")]
  [DBForeignKey("Account", "AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("Scale", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Scale
  {
    get => this._Scale;
    set => this._Scale = value;
  }

  [DBProp("ScaleID")]
  public int ScaleID
  {
    get => this._ScaleID;
    set => this._ScaleID = value;
  }

  [DBProp("NotificationByTimeID")]
  [DBForeignKey("NotificationByTime", "NotificationByTimeID")]
  public long NotificationByTimeID
  {
    get => this._NotificationByTimeID;
    set => this._NotificationByTimeID = value;
  }

  [DBProp("eDatumType", AllowNull = true)]
  public eDatumType eDatumType
  {
    get => this._eDatumType;
    set => this._eDatumType = value;
  }

  [DBProp("HasUserNotificationAction", DefaultValue = false)]
  public bool HasUserNotificationAction
  {
    get => this._HasUserNotificationAction;
    set => this._HasUserNotificationAction = value;
  }

  [DBProp("HasControlAction", DefaultValue = false)]
  public bool HasControlAction
  {
    get => this._HasControlAction;
    set => this._HasControlAction = value;
  }

  [DBProp("HasLocalAlertAction", DefaultValue = false)]
  public bool HasLocalAlertAction
  {
    get => this._HasLocalAlertAction;
    set => this._HasLocalAlertAction = value;
  }

  [DBProp("HasSystemAction", DefaultValue = false)]
  public bool HasSystemAction
  {
    get => this._HasSystemAction;
    set => this._HasSystemAction = value;
  }

  [DBProp("HasThermostatAction", DefaultValue = false)]
  public bool HasThermostatAction
  {
    get => this._HasThermostatAction;
    set => this._HasThermostatAction = value;
  }

  [DBProp("HasResetAccAction", DefaultValue = false)]
  public bool HasResetAccAction
  {
    get => this._HasResetAccAction;
    set => this._HasResetAccAction = value;
  }

  [AllowHtml]
  [DBProp("PushMsgText", MaxLength = 2000, AllowNull = true, International = true)]
  public string PushMsgText
  {
    get => this._PushMsgText;
    set
    {
      if (value == null)
        this._PushMsgText = string.Empty;
      else
        this._PushMsgText = value;
    }
  }

  [DBProp("IgnoreMaintenanceWindow", AllowNull = false, DefaultValue = false)]
  public bool IgnoreMaintenanceWindow
  {
    get => this._IgnoreMaintenanceWindow;
    set => this._IgnoreMaintenanceWindow = value;
  }

  public List<NotificationRecipient> NotificationRecipients
  {
    get
    {
      if (this._NotificationRecipients == null)
        this._NotificationRecipients = NotificationRecipient.LoadByNotificationID(this.NotificationID);
      return this._NotificationRecipients;
    }
  }

  public List<NotificationRecipient> SystemActionList
  {
    get
    {
      if (this._SystemActionList == null)
      {
        List<eNotificationType> SystemActionNotificationTypes = new List<eNotificationType>()
        {
          eNotificationType.SystemAction,
          eNotificationType.HTTP,
          eNotificationType.ResetAccumulator
        };
        this._SystemActionList = this.NotificationRecipients.Where<NotificationRecipient>((System.Func<NotificationRecipient, bool>) (nr => SystemActionNotificationTypes.Contains(nr.NotificationType))).ToList<NotificationRecipient>();
      }
      return this._SystemActionList;
    }
  }

  public NotificationByTime NotificationByTime
  {
    get
    {
      if (this._NotificationByTime == null)
      {
        this._NotificationByTime = NotificationByTime.Load(this.NotificationByTimeID);
        if (this._NotificationByTime == null)
          this._NotificationByTime = new NotificationByTime();
      }
      return this._NotificationByTime;
    }
  }

  public NotificationRecipient AddCustomer(Customer customer, eNotificationType notificationType)
  {
    foreach (NotificationRecipient notificationRecipient in this.NotificationRecipients)
    {
      if (notificationRecipient.CustomerToNotifyID == customer.CustomerID && notificationRecipient.NotificationType == notificationType)
        return notificationRecipient;
    }
    if (this.NotificationID == long.MinValue)
    {
      NotificationRecipient notificationRecipient = new NotificationRecipient()
      {
        CustomerToNotify = customer,
        CustomerToNotifyID = customer.CustomerID,
        NotificationType = notificationType
      };
      if (this._NotificationRecipients == null)
        this._NotificationRecipients = new List<NotificationRecipient>();
      this._NotificationRecipients.Add(notificationRecipient);
      return notificationRecipient;
    }
    NotificationRecipient notificationRecipient1 = NotificationRecipient.AddCustomer(this, customer, notificationType);
    this.NotificationRecipients.Add(notificationRecipient1);
    return notificationRecipient1;
  }

  public void RemoveRecipient(NotificationRecipient recipient)
  {
    recipient.Delete();
    this.NotificationRecipients.Remove(recipient);
  }

  public void RemoveRecipientDevice(NotificationRecipient recipient)
  {
    recipient.Delete();
    for (int index = this.NotificationRecipients.Count - 1; index >= 0; --index)
    {
      if (this.NotificationRecipients[index].NotificationRecipientID == recipient.NotificationRecipientID)
        this.NotificationRecipients.RemoveAt(index);
    }
  }

  public void RemoveCustomer(Customer customer)
  {
    NotificationRecipient.RemoveCustomer(this, customer);
    for (int index = this.NotificationRecipients.Count - 1; index >= 0; --index)
    {
      if (this.NotificationRecipients[index].CustomerToNotifyID == customer.CustomerID)
        this.NotificationRecipients.RemoveAt(index);
    }
  }

  public NotificationRecipient AddCustomerGroup(CustomerGroup group)
  {
    foreach (NotificationRecipient notificationRecipient in this.NotificationRecipients)
    {
      if (notificationRecipient.CustomerGroupID == group.CustomerGroupID && notificationRecipient.NotificationType == eNotificationType.Group)
        return notificationRecipient;
    }
    if (this.NotificationID == long.MinValue)
    {
      NotificationRecipient notificationRecipient = new NotificationRecipient()
      {
        CustomerGroupID = group.CustomerGroupID,
        NotificationType = eNotificationType.Group
      };
      if (this._NotificationRecipients == null)
        this._NotificationRecipients = new List<NotificationRecipient>();
      this._NotificationRecipients.Add(notificationRecipient);
      return notificationRecipient;
    }
    NotificationRecipient notificationRecipient1 = new NotificationRecipient();
    notificationRecipient1.CustomerGroupID = group.CustomerGroupID;
    notificationRecipient1.NotificationID = this.NotificationID;
    notificationRecipient1.NotificationType = eNotificationType.Group;
    notificationRecipient1.Save();
    this.NotificationRecipients.Add(notificationRecipient1);
    return notificationRecipient1;
  }

  public NotificationRecipient AddSensorRecipient(
    Sensor sensor,
    string defaultSerializedRecipientProperties)
  {
    eNotificationType notificationType = sensor.ApplicationID != 13L ? (sensor.ApplicationID != 97L && sensor.ApplicationID != 125L ? (sensor.ApplicationID != 73L && sensor.ApplicationID != 90L && sensor.ApplicationID != 93L && sensor.ApplicationID != 94L && sensor.ApplicationID != 120L && sensor.ApplicationID != 153L ? eNotificationType.Control : eNotificationType.ResetAccumulator) : eNotificationType.Thermostat) : eNotificationType.Local_Notifier;
    foreach (NotificationRecipient notificationRecipient in this.NotificationRecipients)
    {
      if (notificationRecipient.DeviceToNotifyID == sensor.SensorID && notificationRecipient.NotificationType == notificationType)
      {
        if (notificationRecipient.NotificationType == eNotificationType.Local_Notifier || notificationRecipient.NotificationType == eNotificationType.Thermostat)
          return notificationRecipient;
        if (notificationRecipient.NotificationType == eNotificationType.Control)
        {
          if (notificationRecipient.SerializedRecipientProperties.StartsWith(defaultSerializedRecipientProperties.Substring(0, 3)))
            return notificationRecipient;
        }
        else if (notificationRecipient.NotificationType == eNotificationType.ResetAccumulator)
          return notificationRecipient;
      }
    }
    NotificationRecipient notificationRecipient1;
    if (this.NotificationID == long.MinValue)
    {
      notificationRecipient1 = new NotificationRecipient()
      {
        DeviceToNotify = sensor,
        DeviceToNotifyID = sensor.SensorID,
        NotificationType = notificationType,
        SerializedRecipientProperties = defaultSerializedRecipientProperties
      };
      if (this._NotificationRecipients == null)
        this._NotificationRecipients = new List<NotificationRecipient>();
      this._NotificationRecipients.Add(notificationRecipient1);
    }
    else
    {
      notificationRecipient1 = NotificationRecipient.AddDevice(this.NotificationID, sensor.SensorID, notificationType, defaultSerializedRecipientProperties);
      this.NotificationRecipients.Add(notificationRecipient1);
    }
    return notificationRecipient1;
  }

  public List<SensorDatum> SensorsThatNotify
  {
    get
    {
      if (this._SensorsToNotify == null)
        this._SensorsToNotify = SensorNotification.LoadByNotificationID(this.NotificationID);
      return this._SensorsToNotify;
    }
  }

  public void AddSensor(Sensor sensor, int datumindex = 0)
  {
    if (this.NotificationID > long.MinValue)
      SensorNotification.AddSensor(this.NotificationID, sensor.SensorID, datumindex);
    if (this._SensorsToNotify == null)
      this._SensorsToNotify = new List<SensorDatum>();
    this._SensorsToNotify.Add(new SensorDatum(sensor, datumindex, long.MinValue));
  }

  public void RemoveSensor(Sensor sensor, int datumindex)
  {
    SensorNotification.RemoveSensor(this.NotificationID, sensor.SensorID, datumindex);
    this.SensorsThatNotify.Remove(new SensorDatum(sensor, datumindex, long.MinValue));
  }

  public void RemoveSensor(Sensor sensor)
  {
    foreach (SensorDatum sensorDatum in this.SensorsThatNotify)
    {
      if (sensorDatum.sens.SensorID == sensor.SensorID)
      {
        SensorNotification.RemoveSensor(this.NotificationID, sensor.SensorID, sensorDatum.DatumIndex);
        this.SensorsThatNotify.Remove(new SensorDatum(sensor, sensorDatum.DatumIndex, long.MinValue));
      }
    }
  }

  public List<int> DatumsForSensor(long sensorID)
  {
    return SensorNotification.DatumsForSensor(this.NotificationID, sensorID);
  }

  public static List<int> DatumsForSensor(long notificationID, long sensorID)
  {
    return SensorNotification.DatumsForSensor(notificationID, sensorID);
  }

  public List<Gateway> GatewaysThatNotify
  {
    get
    {
      if (this._GatewaysThatNotify == null)
        this._GatewaysThatNotify = GatewayNotification.LoadByNotificationID(this.NotificationID);
      return this._GatewaysThatNotify;
    }
  }

  public void AddGateway(Gateway gateway)
  {
    if (this.NotificationID > long.MinValue)
      GatewayNotification.AddGateway(this.NotificationID, gateway.GatewayID);
    if (this._GatewaysThatNotify == null)
      this._GatewaysThatNotify = new List<Gateway>();
    this._GatewaysThatNotify.Add(gateway);
  }

  public void RemoveGateway(Gateway gateway)
  {
    GatewayNotification.RemoveGateway(this.NotificationID, gateway.GatewayID);
    if (this._GatewaysThatNotify == null)
      return;
    this._GatewaysThatNotify.Remove(gateway);
  }

  public bool FromExpress { get; set; }

  public string Type => this.IsActive ? "Active" : "Inactive";

  public static Notification Load(long ID) => BaseDBObject.Load<Notification>(ID);

  public void RecordNotification(
    PacketCache packetCache,
    bool test,
    string reading,
    DateTime readingDate,
    Sensor sensor,
    Gateway gateway,
    long sensorNotificationID,
    long gatewayNotificationID,
    DataMessage msg)
  {
    CSNet network = (CSNet) null;
    long num1 = long.MinValue;
    long num2 = long.MinValue;
    if (sensor != null)
    {
      num1 = sensor.SensorID;
      this.SensorID = sensor.SensorID;
      network = CSNet.Load(sensor.CSNetID);
      if (network != null && network.AccountID != this.AccountID)
      {
        this.RemoveSensor(sensor);
        return;
      }
    }
    if (gateway != null)
    {
      num2 = gateway.GatewayID;
      this.GatewayID = gateway.GatewayID;
      network = CSNet.Load(gateway.CSNetID);
      if (network != null && network.AccountID != this.AccountID)
      {
        this.RemoveGateway(gateway);
        return;
      }
    }
    NotificationTriggered notificationTriggered1 = NotificationTriggered.LoadActiveByDeviceIDandNotificationID(this.NotificationID, new long?(num1), new long?(num2)).FirstOrDefault<NotificationTriggered>();
    if (notificationTriggered1 != null && this.SnoozeDuration != 0)
    {
      notificationTriggered1.Reading = reading;
      notificationTriggered1.ReadingDate = readingDate;
      notificationTriggered1.Save();
    }
    else
    {
      NotificationTriggered notificationTriggered2 = new NotificationTriggered();
      notificationTriggered2.StartTime = DateTime.UtcNow;
      notificationTriggered2.NotificationID = this.NotificationID;
      notificationTriggered2.SensorNotificationID = sensorNotificationID;
      notificationTriggered2.GatewayNotificationID = gatewayNotificationID;
      notificationTriggered2.Reading = reading;
      notificationTriggered2.ReadingDate = readingDate;
      notificationTriggered2.OriginalReading = reading;
      notificationTriggered2.OriginalReadingDate = readingDate;
      notificationTriggered2.Save();
      DataSet dataSet = NotificationTriggered.LoadImmediate(this.NotificationID);
      Dictionary<long, NotificationRecipient> dictionary = new Dictionary<long, NotificationRecipient>();
      foreach (DataRow row in (InternalDataCollectionBase) dataSet.Tables[1].Rows)
      {
        NotificationRecipient notificationRecipient = new NotificationRecipient();
        try
        {
          notificationRecipient.Load(row);
          dictionary.Add(notificationRecipient.NotificationRecipientID, notificationRecipient);
        }
        catch (Exception ex)
        {
          ex.Log("NotificationEscalation-NotificationRecipientDic NotificationRecipientID : " + notificationRecipient.NotificationRecipientID.ToString());
        }
      }
      List<CustomerGroupRecipient> customerGroupRecipientList = new List<CustomerGroupRecipient>();
      foreach (DataRow row in (InternalDataCollectionBase) dataSet.Tables[2].Rows)
      {
        CustomerGroupRecipient customerGroupRecipient = new CustomerGroupRecipient();
        try
        {
          customerGroupRecipient.Load(row);
          customerGroupRecipientList.Add(customerGroupRecipient);
        }
        catch (Exception ex)
        {
          ex.Log("NotificationRecord-CustomerGroupRecipientsDic ID : " + customerGroupRecipient.CustomerGroupRecipientID.ToString());
        }
      }
      foreach (DataRow row in (InternalDataCollectionBase) dataSet.Tables[0].Rows)
      {
        NotificationRecipient recipient = dictionary[row["NotificationRecipientID"].ToLong()];
        if (recipient.DelayMinutes <= 0)
        {
          if (recipient.NotificationType == eNotificationType.Group)
          {
            recipient.CustomerToNotifyID = row["CustomerID"].ToLong();
            long num3 = row["CustomerGroupLinkID"].ToLong();
            recipient.NotificationType = (eNotificationType) row["eNotificationType"].ToInt();
            CustomerGroupRecipient groupRecipient = (CustomerGroupRecipient) null;
            foreach (CustomerGroupRecipient customerGroupRecipient in customerGroupRecipientList)
            {
              if (customerGroupRecipient.CustomerGroupLinkID == num3 && customerGroupRecipient.NotificationID == this.NotificationID)
              {
                groupRecipient = customerGroupRecipient;
                break;
              }
            }
            if (groupRecipient == null)
            {
              groupRecipient = new CustomerGroupRecipient();
              groupRecipient.NotificationID = this.NotificationID;
              groupRecipient.CustomerGroupLinkID = num3;
            }
            this.RecordNotification(packetCache, test, reading, readingDate, sensor, gateway, sensorNotificationID, gatewayNotificationID, (DataMessage) null, network, notificationTriggered2, recipient, groupRecipient);
            recipient.NotificationType = eNotificationType.Group;
          }
          else
            this.RecordNotification(packetCache, test, reading, readingDate, sensor, gateway, sensorNotificationID, gatewayNotificationID, (DataMessage) null, network, notificationTriggered2, recipient, (CustomerGroupRecipient) null);
        }
      }
    }
  }

  public void RecordNotification(
    PacketCache packetCache,
    bool test,
    string reading,
    DateTime readingDate,
    Sensor sensor,
    Gateway gateway,
    long sensorNotificationID,
    long gatewayNotificationID,
    DataMessage msg,
    CSNet network,
    NotificationTriggered notificationTriggered,
    NotificationRecipient recipient,
    CustomerGroupRecipient groupRecipient)
  {
    try
    {
      if (packetCache == null)
        DebugLog.CreateLog("cache == null", "RecordNotification()", 1, "PacketCache", 4364L);
      if (reading == null)
        DebugLog.CreateLog("reading == null", "RecordNotification()", 2, "string", 4364L);
      if (sensor == null)
        DebugLog.CreateLog("sensor == null", "RecordNotification()", 4, "Sensor", 4364L);
      if (gateway == null)
        DebugLog.CreateLog("gateway == null", "RecordNotification()", 5, "Gateway", 4364L);
      if (network == null)
        DebugLog.CreateLog("Network == null", "RecordNotification()", 7, "CSNet", 4364L);
      if (notificationTriggered == null)
        DebugLog.CreateLog("notificationTriggered == null", "RecordNotification()", 8, "NotificationTriggered", 4364L);
      if (recipient == null)
        DebugLog.CreateLog("Recipient == null", "RecordNotification()", 9, "NotificationRecipient", 4364L);
      Customer cust = (Customer) null;
      if (recipient.CustomerToNotifyID > 0L)
      {
        cust = recipient.CustomerToNotify;
        if (cust == null)
        {
          DebugLog.CreateLog("cust == null", "Notification.RecordNotification()", 10, "Customer", recipient.CustomerToNotifyID);
          return;
        }
        if (!cust.IsActive)
        {
          if (cust.Account == null)
          {
            DebugLog.CreateLog("cust.Account.PrimaryContact == null", "Notification.RecordNotification()", 11, "Account", cust.AccountID);
            return;
          }
          if (cust.Account.PrimaryContact == null)
          {
            DebugLog.CreateLog("cust.Account.PrimaryContact == null", "Notification.RecordNotification()", 12, "Customer", cust.Account.PrimaryContactID);
            return;
          }
          cust = cust.Account.PrimaryContact;
        }
        if (cust.Account == null)
        {
          DebugLog.CreateLog("cust.Account == null", "Notification.RecordNotification()", 13, "Account", cust.AccountID);
          return;
        }
        if (!cust.IsInCustomerScheduleWindow(TimeZone.GetLocalTimeById(DateTime.UtcNow, cust.Account.TimeZoneID)))
          return;
      }
      Sensor sensor1 = (Sensor) null;
      if (recipient.DeviceToNotifyID > 0L)
      {
        sensor1 = packetCache.LoadSensor(recipient.DeviceToNotifyID);
        if (sensor1 == null || sensor1.CSNetID <= 0L || sensor1.AccountID != this.AccountID || !sensor1.IsActive || sensor1.IsDeleted)
          return;
      }
      if (this.NotificationID > 0L && (recipient.CustomerToNotifyID > 0L || recipient.DeviceToNotifyID > 0L || recipient.CustomerGroupID > 0L) && !test)
      {
        this.LastSent = DateTime.UtcNow;
        this.Save();
      }
      switch (recipient.NotificationType)
      {
        case eNotificationType.Email:
          this.RecordEmailNotification(packetCache, reading, readingDate, sensor, gateway, network, cust, sensorNotificationID, notificationTriggered.NotificationTriggeredID, notificationTriggered.OriginalReading, notificationTriggered.OriginalReadingDate);
          break;
        case eNotificationType.SMS:
          this.RecordSMSNotification(packetCache, reading, readingDate, sensor, gateway, network, cust, sensorNotificationID, notificationTriggered.NotificationTriggeredID, notificationTriggered.OriginalReading, notificationTriggered.OriginalReadingDate);
          break;
        case eNotificationType.Both:
          this.RecordEmailNotification(packetCache, reading, readingDate, sensor, gateway, network, cust, sensorNotificationID, notificationTriggered.NotificationTriggeredID, notificationTriggered.OriginalReading, notificationTriggered.OriginalReadingDate);
          this.RecordSMSNotification(packetCache, reading, readingDate, sensor, gateway, network, cust, sensorNotificationID, notificationTriggered.NotificationTriggeredID, notificationTriggered.OriginalReading, notificationTriggered.OriginalReadingDate);
          break;
        case eNotificationType.Local_Notifier:
          this.RecordAVNotification(packetCache, reading, sensor, gateway, network, sensor1, recipient.SerializedRecipientProperties, sensorNotificationID, notificationTriggered.NotificationTriggeredID, readingDate, notificationTriggered.OriginalReading, notificationTriggered.OriginalReadingDate);
          break;
        case eNotificationType.Control:
          this.RecordControlNotification(packetCache, reading, gateway, sensor1, recipient.SerializedRecipientProperties, sensorNotificationID, notificationTriggered.NotificationTriggeredID);
          break;
        case eNotificationType.Phone:
          this.RecordPOTSNotification(packetCache, reading, sensor, gateway, network, cust, sensorNotificationID, notificationTriggered.NotificationTriggeredID, readingDate, notificationTriggered.OriginalReading, notificationTriggered.OriginalReadingDate);
          break;
        case eNotificationType.HTTP:
          this.RecordHTTPNotification(packetCache, reading, readingDate, sensor, gateway, sensorNotificationID, gatewayNotificationID, network, notificationTriggered.NotificationTriggeredID, notificationTriggered.OriginalReading, notificationTriggered.OriginalReadingDate, recipient.SerializedRecipientProperties);
          break;
        case eNotificationType.SystemAction:
          this.RecordSystemAction(packetCache, reading, gateway, sensor1, recipient.SerializedRecipientProperties, sensorNotificationID, notificationTriggered.NotificationTriggeredID, recipient.ActionToExecuteID);
          break;
        case eNotificationType.Thermostat:
          this.RecordThermostatAction(packetCache, reading, gateway, sensor1, recipient.SerializedRecipientProperties, sensorNotificationID, notificationTriggered.NotificationTriggeredID, recipient.ActionToExecuteID);
          break;
        case eNotificationType.Push_Message:
          this.RecordPushNotification(packetCache, reading, readingDate, sensor, gateway, network, cust, sensorNotificationID, notificationTriggered.NotificationTriggeredID, notificationTriggered.OriginalReading, notificationTriggered.OriginalReadingDate);
          break;
        case eNotificationType.ResetAccumulator:
          this.RecordResetAccumulatorNotification(packetCache, gateway, gatewayNotificationID, sensor, sensorNotificationID, reading, sensor1, recipient.SerializedRecipientProperties, notificationTriggered.NotificationTriggeredID);
          break;
        default:
          ExceptionLog.Log(new Exception("Notification.RecordNotification - Unknown Notification Type" + recipient.NotificationType.ToString()));
          break;
      }
      if (recipient.CustomerGroupID > 0L)
      {
        groupRecipient.LastSentTime = DateTime.UtcNow;
        groupRecipient.Save();
      }
      else
      {
        recipient.LastSentTime = DateTime.UtcNow;
        recipient.Save();
      }
    }
    catch (Exception ex)
    {
      ex.Log("RecordNotification(): " + $"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"(packetCache == null)=[{packetCache == null}]" + $", test=[{test}]"}, sensor=[{(sensor != null ? "id=" + sensor.SensorID.ToStringSafe() : "null")}] "}, gateway=[{(gateway != null ? "id=" + gateway.GatewayID.ToStringSafe() : "null")}]" + $", snsrNtfcID=[{sensorNotificationID}]" + $", gtwyNtfcID=[{gatewayNotificationID}]"}, dataMsg=[{(msg != null ? "guid=" + msg.DataMessageGUID.ToStringSafe() : "null")}]"}, network=[{(network != null ? "id=" + network.ToString() : "null")}]"}, ntfcTrgd=[{(notificationTriggered != null ? "id=" + notificationTriggered.NotificationTriggeredID.ToStringSafe() : "null")}]"}, ntfcRecipient=[{(recipient != null ? "id=" + recipient.NotificationRecipientID.ToStringSafe() : "null")}]"}, groupRecipient=[{(groupRecipient != null ? "id=" + groupRecipient.CustomerGroupRecipientID.ToStringSafe() : "null")}]"}, reading=[{(reading != null ? reading.ToStringSafe() : "null")}]"}, readingDate=[{readingDate.ToStringSafe()}]"}, origReading=[{(notificationTriggered != null ? notificationTriggered.OriginalReading.ToStringSafe() : "ntfcTrgd null")}]"}, origReadingDate=[{(notificationTriggered != null ? notificationTriggered.OriginalReadingDate.ToStringSafe() : "ntfcTrgd null")}]");
    }
  }

  private void RecordEmailNotification(
    PacketCache packetCache,
    string reading,
    DateTime readingDate,
    Sensor sensor,
    Gateway gateway,
    CSNet network,
    Customer cust,
    long sensorNotificationID,
    long notificationTriggeredID,
    string originalReading,
    DateTime originalReadingDate)
  {
    try
    {
      if (cust == null)
        return;
      NotificationRecorded notificationRecorded = new NotificationRecorded();
      notificationRecorded.NotificationDate = DateTime.UtcNow;
      notificationRecorded.Save();
      EmailTemplate template = EmailTemplate.LoadBest(cust.Account, eEmailTemplateFlag.Notification);
      if (template == null)
      {
        template = new EmailTemplate();
        template.Subject = nameof (Notification);
        template.Template = "<p>This notification is regarding</p><p>{Content}</p><p>Regards,<br />Your Sensor Monitoring</p>";
      }
      string str = this.BuildSubject(reading, sensor, gateway, network, template, packetCache.LoadAccount(this.AccountID), notificationRecorded.NotificationRecordedID, readingDate, originalReading, originalReadingDate, notificationRecorded.NotificationGUID);
      string Content = this.BuildEmailMessage(cust, reading, readingDate, sensor, gateway, network, packetCache.LoadAccount(this.AccountID), notificationRecorded.NotificationRecordedID, originalReading, originalReadingDate, notificationRecorded.NotificationGUID);
      KeyValuePair<long, string> keyValuePair = packetCache.NotifyingOn.FirstOrDefault<KeyValuePair<long, string>>((System.Func<KeyValuePair<long, string>, bool>) (key => key.Key == sensorNotificationID));
      notificationRecorded.NotifyingOn = !keyValuePair.Equals((object) new KeyValuePair<long, string>()) ? keyValuePair.Value : "";
      notificationRecorded.SensorID = sensor != null ? sensor.SensorID : long.MinValue;
      notificationRecorded.GatewayID = gateway != null ? gateway.GatewayID : long.MinValue;
      notificationRecorded.NotificationID = this.NotificationID;
      notificationRecorded.NotificationType = eNotificationType.Email;
      notificationRecorded.CustomerID = cust.CustomerID;
      notificationRecorded.Reading = reading;
      notificationRecorded.NotificationSubject = str;
      notificationRecorded.NotificationContent = Content;
      notificationRecorded.NotificationText = template.MailMerge(Content, cust.NotificationEmail);
      notificationRecorded.Status = "Queued";
      notificationRecorded.DoRetry = true;
      notificationRecorded.SentTo = cust.NotificationEmail;
      notificationRecorded.NotificationTriggeredID = notificationTriggeredID;
      notificationRecorded.Save();
      packetCache.RecordedNotifications.Add(notificationRecorded);
    }
    catch (Exception ex)
    {
      ex.Log("RecordEmailNotification(): " + $"{$"{$"{$"{$"{$"{$"{$"{$"(packetCache == null)=[{packetCache == null}]"}, sensor=[{(sensor != null ? "id=" + sensor.SensorID.ToStringSafe() : "null")}] "}, gateway=[{(gateway != null ? "id=" + gateway.GatewayID.ToStringSafe() : "null")}]" + $", snsrNtfcID=[{sensorNotificationID}]"}, network=[{(network != null ? "id=" + network.ToString() : "null")}]" + $", ntfcTrgd=[{notificationTriggeredID}]"}, customer=[{(cust != null ? "id=" + cust.CustomerID.ToStringSafe() : "null")}]"}, reading=[{(reading != null ? reading.ToStringSafe() : "null")}]"}, readingDate=[{readingDate.ToStringSafe()}]"}, origReading=[{(originalReading != null ? originalReading.ToStringSafe() : "null")}]"}, origReadingDate=[{originalReadingDate.ToStringSafe()}]");
    }
  }

  private void RecordSMSNotification(
    PacketCache packetCache,
    string reading,
    DateTime readingDate,
    Sensor sensor,
    Gateway gateway,
    CSNet network,
    Customer cust,
    long sensorNotificationID,
    long notificationTriggeredID,
    string originalReading,
    DateTime originalReadingDate)
  {
    try
    {
      if (cust == null)
        return;
      NotificationRecorded notificationRecorded = new NotificationRecorded();
      notificationRecorded.NotificationDate = DateTime.UtcNow;
      notificationRecorded.Save();
      EmailTemplate emailTemplate = EmailTemplate.LoadBest(cust.Account, eEmailTemplateFlag.SMSNotification);
      if (emailTemplate == null || !emailTemplate.ContainsFlag(eEmailTemplateFlag.SMSNotification))
      {
        emailTemplate = new EmailTemplate();
        emailTemplate.Subject = string.Empty;
        emailTemplate.Template = "{Content}";
      }
      string str = string.Empty;
      if (!string.IsNullOrWhiteSpace(emailTemplate.Subject))
        str = emailTemplate.Subject;
      string Content = this.BuildSMSMessage(cust, reading, readingDate, sensor, gateway, network, packetCache.LoadAccount(this.AccountID), notificationRecorded.NotificationRecordedID, originalReading, originalReadingDate, notificationRecorded.NotificationGUID);
      KeyValuePair<long, string> keyValuePair = packetCache.NotifyingOn.FirstOrDefault<KeyValuePair<long, string>>((System.Func<KeyValuePair<long, string>, bool>) (key => key.Key == sensorNotificationID));
      notificationRecorded.NotifyingOn = !keyValuePair.Equals((object) new KeyValuePair<long, string>()) ? keyValuePair.Value : "";
      notificationRecorded.SensorID = this.SensorID;
      notificationRecorded.GatewayID = this.GatewayID;
      notificationRecorded.NotificationID = this.NotificationID;
      notificationRecorded.NotificationType = eNotificationType.SMS;
      notificationRecorded.CustomerID = cust.CustomerID;
      notificationRecorded.Reading = reading;
      notificationRecorded.NotificationSubject = str;
      notificationRecorded.NotificationContent = Content;
      notificationRecorded.NotificationText = emailTemplate.MailMerge(Content, cust.NotificationEmail);
      notificationRecorded.Status = "Queued";
      notificationRecorded.DoRetry = true;
      notificationRecorded.SentTo = cust.NotificationPhone;
      notificationRecorded.NotificationTriggeredID = notificationTriggeredID;
      notificationRecorded.Save();
      packetCache.RecordedNotifications.Add(notificationRecorded);
    }
    catch (Exception ex)
    {
      ex.Log("RecordSMSNotification(): " + $"{$"{$"{$"{$"{$"{$"{$"{$"(packetCache == null)=[{packetCache == null}]"}, sensor=[{(sensor != null ? "id=" + sensor.SensorID.ToStringSafe() : "null")}] "}, gateway=[{(gateway != null ? "id=" + gateway.GatewayID.ToStringSafe() : "null")}]" + $", snsrNtfcID=[{sensorNotificationID}]"}, network=[{(network != null ? "id=" + network.CSNetID.ToString() : "null")}]" + $", ntfcTrgd=[{notificationTriggeredID}]"}, customer=[{(cust != null ? "id=" + cust.CustomerID.ToStringSafe() : "null")}]"}, reading=[{(reading != null ? reading.ToStringSafe() : "null")}]"}, readingDate=[{readingDate.ToStringSafe()}]"}, origReading=[{(originalReading != null ? originalReading.ToStringSafe() : "null")}]"}, origReadingDate=[{originalReadingDate.ToStringSafe()}]");
    }
  }

  private void RecordPushNotification(
    PacketCache packetCache,
    string reading,
    DateTime readingDate,
    Sensor sensor,
    Gateway gateway,
    CSNet network,
    Customer cust,
    long sensorNotificationID,
    long notificationTriggeredID,
    string originalReading,
    DateTime originalReadingDate)
  {
    try
    {
      NotificationRecorded notificationRecorded = new NotificationRecorded();
      notificationRecorded.NotificationDate = DateTime.UtcNow;
      notificationRecorded.Save();
      EmailTemplate emailTemplate = EmailTemplate.LoadBest(cust.Account, eEmailTemplateFlag.SMSNotification);
      if (emailTemplate == null || !emailTemplate.ContainsFlag(eEmailTemplateFlag.SMSNotification))
      {
        emailTemplate = new EmailTemplate();
        emailTemplate.Subject = string.Empty;
        emailTemplate.Template = "{Content}";
      }
      string str = string.Empty;
      if (!string.IsNullOrWhiteSpace(emailTemplate.Subject))
        str = emailTemplate.Subject;
      string Content = this.BuildPushMessage(cust, reading, readingDate, sensor, gateway, network, packetCache.LoadAccount(this.AccountID), notificationRecorded.NotificationRecordedID, originalReading, originalReadingDate, notificationRecorded.NotificationGUID);
      KeyValuePair<long, string> keyValuePair = packetCache.NotifyingOn.FirstOrDefault<KeyValuePair<long, string>>((System.Func<KeyValuePair<long, string>, bool>) (key => key.Key == sensorNotificationID));
      notificationRecorded.NotifyingOn = !keyValuePair.Equals((object) new KeyValuePair<long, string>()) ? keyValuePair.Value : "";
      notificationRecorded.SensorID = this.SensorID;
      notificationRecorded.GatewayID = this.GatewayID;
      notificationRecorded.NotificationID = this.NotificationID;
      notificationRecorded.NotificationType = eNotificationType.Push_Message;
      notificationRecorded.CustomerID = cust.CustomerID;
      notificationRecorded.Reading = reading;
      notificationRecorded.NotificationSubject = str;
      notificationRecorded.NotificationContent = Content;
      notificationRecorded.NotificationText = emailTemplate.MailMerge(Content, cust.NotificationEmail);
      notificationRecorded.Status = "Queued";
      notificationRecorded.DoRetry = true;
      notificationRecorded.SentTo = cust.NotificationPhone;
      notificationRecorded.NotificationTriggeredID = notificationTriggeredID;
      notificationRecorded.Save();
      packetCache.RecordedNotifications.Add(notificationRecorded);
    }
    catch (Exception ex)
    {
      ex.Log("RecordPushNotification(): " + $"{$"{$"{$"{$"{$"{$"{$"{$"(packetCache == null)=[{packetCache == null}]"}, sensor=[{(sensor != null ? "id=" + sensor.SensorID.ToStringSafe() : "null")}] "}, gateway=[{(gateway != null ? "id=" + gateway.GatewayID.ToStringSafe() : "null")}]" + $", snsrNtfcID=[{sensorNotificationID}]"}, network=[{(network != null ? "id=" + network.ToString() : "null")}]" + $", ntfcTrgd=[{notificationTriggeredID}]"}, customer=[{(cust != null ? "id=" + cust.CustomerID.ToStringSafe() : "null")}]"}, reading=[{(reading != null ? reading.ToStringSafe() : "null")}]"}, readingDate=[{readingDate.ToStringSafe()}]"}, origReading=[{(originalReading != null ? originalReading.ToStringSafe() : "null")}]"}, origReadingDate=[{originalReadingDate.ToStringSafe()}]");
    }
  }

  private void RecordAVNotification(
    PacketCache packetCache,
    string reading,
    Sensor sensor,
    Gateway gateway,
    CSNet network,
    Sensor Device,
    string serializedRecipientProperties,
    long sensorNotificationID,
    long notificationTriggeredID,
    DateTime readingDate,
    string originalReading,
    DateTime originalReadingDate)
  {
    try
    {
      NotificationRecorded notificationRecorded1 = new NotificationRecorded();
      notificationRecorded1.NotificationDate = DateTime.UtcNow;
      notificationRecorded1.Save();
      string str1 = this.BuildAVMessage(Device, reading, sensor, gateway, network, packetCache.LoadAccount(this.AccountID), notificationRecorded1.NotificationRecordedID, readingDate, originalReading, originalReadingDate, notificationRecorded1.NotificationGUID);
      string str2 = string.Empty;
      if (sensor != null)
        str2 = sensor.SensorName;
      else if (gateway != null)
        str2 = gateway.Name;
      KeyValuePair<long, string> keyValuePair = packetCache.NotifyingOn.FirstOrDefault<KeyValuePair<long, string>>((System.Func<KeyValuePair<long, string>, bool>) (key => key.Key == sensorNotificationID));
      notificationRecorded1.NotifyingOn = !keyValuePair.Equals((object) new KeyValuePair<long, string>()) ? keyValuePair.Value : "";
      notificationRecorded1.SensorID = this.SensorID;
      notificationRecorded1.GatewayID = this.GatewayID;
      notificationRecorded1.NotificationID = this.NotificationID;
      notificationRecorded1.NotificationType = eNotificationType.Local_Notifier;
      notificationRecorded1.SentToDeviceID = Device.SensorID;
      notificationRecorded1.QueueID = NotificationRecorded.NextQueueID(Device.SensorID);
      notificationRecorded1.SerializedRecipientProperties = serializedRecipientProperties + str2;
      notificationRecorded1.Reading = reading;
      notificationRecorded1.NotificationContent = str1;
      notificationRecorded1.NotificationText = str1;
      notificationRecorded1.NotificationTriggeredID = notificationTriggeredID;
      notificationRecorded1.SentTo = Device.SensorName;
      if (Device.LastCommunicationDate < DateTime.UtcNow.AddDays(-1.0))
      {
        notificationRecorded1.AcknowledgedDate = DateTime.UtcNow;
        notificationRecorded1.Status = "Undeliverable - Device Offline";
        foreach (NotificationRecorded notificationRecorded2 in NotificationRecorded.LoadGetMessageForLocalNotifier(Device.SensorID))
        {
          notificationRecorded2.AcknowledgedDate = DateTime.UtcNow;
          notificationRecorded2.Status = "Undeliverable - Device Offline";
          notificationRecorded2.Save();
        }
      }
      else
      {
        if (!Device.PendingActionControlCommand)
        {
          Device.PendingActionControlCommand = true;
          Device.Save();
        }
        notificationRecorded1.Status = "Command Sent";
        CSNet.SetGatewaysUrgentTrafficFlag(Device.CSNetID);
        if (gateway != null && gateway.CSNetID == Device.CSNetID)
          gateway.UrgentTraffic = true;
      }
      notificationRecorded1.Save();
    }
    catch (Exception ex)
    {
      ex.Log("RecordAVNotification(): " + $"{$"{$"{$"{$"{$"{$"{$"{$"{$"(packetCache == null)=[{packetCache == null}]"}, sensor=[{(sensor != null ? "id=" + sensor.SensorID.ToStringSafe() : "null")}] "}, device=[{(Device != null ? "id=" + Device.SensorID.ToStringSafe() : "null")}] "}, gateway=[{(gateway != null ? "id=" + gateway.GatewayID.ToStringSafe() : "null")}]" + $", snsrNtfcID=[{sensorNotificationID}]"}, network=[{(network != null ? "id=" + network.ToString() : "null")}]" + $", ntfcTrgd=[{notificationTriggeredID}]"}, serializedRcptProps=[{(serializedRecipientProperties != null ? serializedRecipientProperties.ToStringSafe() : "null")}]"}, reading=[{(reading != null ? reading.ToStringSafe() : "null")}]"}, readingDate=[{readingDate.ToStringSafe()}]"}, origReading=[{(originalReading != null ? originalReading.ToStringSafe() : "null")}]"}, origReadingDate=[{originalReadingDate.ToStringSafe()}]");
    }
  }

  private void RecordControlNotification(
    PacketCache packetCache,
    string reading,
    Gateway gateway,
    Sensor Device,
    string serializedRecipientProperties,
    long sensorNotificationID,
    long notificationTriggeredID)
  {
    try
    {
      string str = this.BuildControlMessage(serializedRecipientProperties);
      NotificationRecorded notificationRecorded1 = new NotificationRecorded();
      KeyValuePair<long, string> keyValuePair = packetCache.NotifyingOn.FirstOrDefault<KeyValuePair<long, string>>((System.Func<KeyValuePair<long, string>, bool>) (key => key.Key == sensorNotificationID));
      notificationRecorded1.NotifyingOn = !keyValuePair.Equals((object) new KeyValuePair<long, string>()) ? keyValuePair.Value : "";
      notificationRecorded1.SensorID = this.SensorID;
      notificationRecorded1.GatewayID = this.GatewayID;
      notificationRecorded1.NotificationID = this.NotificationID;
      notificationRecorded1.NotificationType = eNotificationType.Control;
      notificationRecorded1.SentToDeviceID = Device.SensorID;
      notificationRecorded1.QueueID = NotificationRecorded.NextQueueID(Device.SensorID);
      notificationRecorded1.SerializedRecipientProperties = serializedRecipientProperties;
      notificationRecorded1.NotificationContent = str;
      notificationRecorded1.NotificationText = str;
      notificationRecorded1.Reading = reading;
      notificationRecorded1.NotificationDate = DateTime.UtcNow;
      notificationRecorded1.SentTo = Device.SensorName;
      notificationRecorded1.NotificationTriggeredID = notificationTriggeredID;
      if (Device.LastCommunicationDate < DateTime.UtcNow.AddDays(-1.0))
      {
        notificationRecorded1.AcknowledgedDate = DateTime.UtcNow;
        notificationRecorded1.Status = "Undeliverable - Device Offline";
        foreach (NotificationRecorded notificationRecorded2 in NotificationRecorded.LoadGetMessageForLocalNotifier(Device.SensorID))
        {
          notificationRecorded2.AcknowledgedDate = DateTime.UtcNow;
          notificationRecorded2.Status = "Undeliverable - Device Offline";
          notificationRecorded2.Save();
        }
      }
      else
      {
        if (!Device.PendingActionControlCommand)
        {
          Device.PendingActionControlCommand = true;
          Device.Save();
        }
        notificationRecorded1.Status = "Command Sent";
        CSNet.SetGatewaysUrgentTrafficFlag(Device.CSNetID);
        if (gateway != null && gateway.CSNetID == Device.CSNetID)
          gateway.UrgentTraffic = true;
      }
      notificationRecorded1.Save();
    }
    catch (Exception ex)
    {
      ex.Log("RecordControlNotification(): " + $"{$"{$"{$"{$"(packetCache == null)=[{packetCache == null}]"}, device=[{(Device != null ? "id=" + Device.SensorID.ToStringSafe() : "null")}] "}, gateway=[{(gateway != null ? "id=" + gateway.GatewayID.ToStringSafe() : "null")}]" + $", snsrNtfcID=[{sensorNotificationID}]" + $", ntfcTrgd=[{notificationTriggeredID}]"}, serializedRcptProps=[{(serializedRecipientProperties != null ? serializedRecipientProperties.ToStringSafe() : "null")}]"}, reading=[{(reading != null ? reading.ToStringSafe() : "null")}]");
    }
  }

  private void RecordResetAccumulatorNotification(
    PacketCache packetCache,
    Gateway gateway,
    long gatewayNotificationID,
    Sensor sensor,
    long sensorNotificationID,
    string reading,
    Sensor TargetDevice,
    string serializedRecipientProperties,
    long notificationTriggeredID)
  {
    try
    {
      NotificationRecorded notificationRecorded1 = new NotificationRecorded();
      KeyValuePair<long, string> keyValuePair = packetCache.NotifyingOn.FirstOrDefault<KeyValuePair<long, string>>((System.Func<KeyValuePair<long, string>, bool>) (key => key.Key == sensorNotificationID));
      notificationRecorded1.NotifyingOn = !keyValuePair.Equals((object) new KeyValuePair<long, string>()) ? keyValuePair.Value : "";
      notificationRecorded1.SensorID = sensor != null ? sensor.SensorID : long.MinValue;
      notificationRecorded1.GatewayID = gateway != null ? gateway.GatewayID : long.MinValue;
      notificationRecorded1.NotificationID = this.NotificationID;
      notificationRecorded1.NotificationType = eNotificationType.ResetAccumulator;
      notificationRecorded1.SerializedRecipientProperties = serializedRecipientProperties;
      notificationRecorded1.Reading = reading;
      notificationRecorded1.NotificationSubject = "Reset Accumulator";
      notificationRecorded1.NotificationContent = "";
      notificationRecorded1.NotificationText = "";
      notificationRecorded1.SentTo = TargetDevice.SensorName;
      notificationRecorded1.SentToDeviceID = TargetDevice.SensorID;
      notificationRecorded1.SerializedRecipientProperties = serializedRecipientProperties;
      notificationRecorded1.NotificationTriggeredID = notificationTriggeredID;
      notificationRecorded1.QueueID = NotificationRecorded.NextQueueID(TargetDevice.SensorID);
      notificationRecorded1.NotificationDate = DateTime.UtcNow;
      if (TargetDevice.LastCommunicationDate < DateTime.UtcNow.AddDays(-1.0))
      {
        notificationRecorded1.AcknowledgedDate = DateTime.UtcNow;
        notificationRecorded1.Status = "Undeliverable - Device Offline";
        foreach (NotificationRecorded notificationRecorded2 in NotificationRecorded.LoadGetMessageForLocalNotifier(TargetDevice.SensorID))
        {
          notificationRecorded2.AcknowledgedDate = DateTime.UtcNow;
          notificationRecorded2.Status = "Undeliverable - Device Offline";
          notificationRecorded2.Save();
        }
      }
      else
      {
        if (MonnitApplicationBase.HasStaticMethod(TargetDevice.ApplicationID, "UseSerializedRecipientProperties"))
          MonnitApplicationBase.CallStaticMethod(TargetDevice.ApplicationID, "UseSerializedRecipientProperties", new object[3]
          {
            (object) TargetDevice,
            (object) notificationRecorded1.QueueID,
            (object) serializedRecipientProperties
          });
        else if (!TargetDevice.PendingActionControlCommand)
        {
          TargetDevice.PendingActionControlCommand = true;
          TargetDevice.Save();
        }
        notificationRecorded1.Status = "Reset Command Sent";
      }
      notificationRecorded1.Save();
    }
    catch (Exception ex)
    {
      ex.Log("RecordResetAccumulatorNotification(): " + $"{$"{$"{$"{$"{$"(packetCache == null)=[{packetCache == null}]"}, sensor=[{(sensor != null ? "id=" + sensor.SensorID.ToStringSafe() : "null")}] "}, targetDevice=[{(TargetDevice != null ? "id=" + TargetDevice.SensorID.ToStringSafe() : "null")}] "}, gateway=[{(gateway != null ? "id=" + gateway.GatewayID.ToStringSafe() : "null")}]" + $", gtwyNtfcID=[{gatewayNotificationID}]" + $", snsrNtfcID=[{sensorNotificationID}]" + $", ntfcTrgd=[{notificationTriggeredID}]"}, serializedRcptProps=[{(serializedRecipientProperties != null ? serializedRecipientProperties.ToStringSafe() : "null")}]"}, reading=[{(reading != null ? reading.ToStringSafe() : "null")}]");
    }
  }

  private void RecordPOTSNotification(
    PacketCache packetCache,
    string reading,
    Sensor sensor,
    Gateway gateway,
    CSNet network,
    Customer cust,
    long sensorNotificationID,
    long notificationTriggeredID,
    DateTime readingDate,
    string originalReading,
    DateTime originalReadingDate)
  {
    try
    {
      NotificationRecorded notificationRecorded = new NotificationRecorded();
      notificationRecorded.NotificationDate = DateTime.UtcNow;
      notificationRecorded.Save();
      string str = this.BuildPOTSMessage(cust, reading, sensor, gateway, network, packetCache.LoadAccount(this.AccountID), notificationRecorded.NotificationRecordedID, readingDate, originalReading, originalReadingDate, notificationRecorded.NotificationGUID);
      KeyValuePair<long, string> keyValuePair = packetCache.NotifyingOn.FirstOrDefault<KeyValuePair<long, string>>((System.Func<KeyValuePair<long, string>, bool>) (key => key.Key == sensorNotificationID));
      notificationRecorded.NotifyingOn = !keyValuePair.Equals((object) new KeyValuePair<long, string>()) ? keyValuePair.Value : "";
      notificationRecorded.SensorID = this.SensorID;
      notificationRecorded.GatewayID = this.GatewayID;
      notificationRecorded.NotificationID = this.NotificationID;
      notificationRecorded.NotificationType = eNotificationType.Phone;
      notificationRecorded.CustomerID = cust.CustomerID;
      notificationRecorded.Reading = reading;
      notificationRecorded.NotificationSubject = "";
      notificationRecorded.NotificationContent = str;
      notificationRecorded.NotificationText = str;
      notificationRecorded.SentTo = cust.NotificationPhone2;
      notificationRecorded.NotificationTriggeredID = notificationTriggeredID;
      notificationRecorded.Save();
      packetCache.RecordedNotifications.Add(notificationRecorded);
    }
    catch (Exception ex)
    {
      ex.Log("RecordPOTSNotification(): " + $"{$"{$"{$"{$"{$"{$"{$"{$"(packetCache == null)=[{packetCache == null}]"}, sensor=[{(sensor != null ? "id=" + sensor.SensorID.ToStringSafe() : "null")}] "}, gateway=[{(gateway != null ? "id=" + gateway.GatewayID.ToStringSafe() : "null")}]" + $", snsrNtfcID=[{sensorNotificationID}]"}, network=[{(network != null ? "id=" + network.ToString() : "null")}]" + $", ntfcTrgd=[{notificationTriggeredID}]"}, customer=[{(cust != null ? "id=" + cust.CustomerID.ToStringSafe() : "null")}]"}, reading=[{(reading != null ? reading.ToStringSafe() : "null")}]"}, readingDate=[{readingDate.ToStringSafe()}]"}, origReading=[{(originalReading != null ? originalReading.ToStringSafe() : "null")}]"}, origReadingDate=[{originalReadingDate.ToStringSafe()}]");
    }
  }

  private void RecordHTTPNotification(
    PacketCache packetCache,
    string reading,
    DateTime readingDate,
    Sensor sensor,
    Gateway gateway,
    long sensorNotificationID,
    long gatewayNotificationID,
    CSNet network,
    long notificationTriggeredID,
    string originalReading,
    DateTime originalReadingDate,
    string serializedRecipientProperties)
  {
    try
    {
      NotificationRecorded notificationRecorded = new NotificationRecorded();
      notificationRecorded.NotificationDate = DateTime.UtcNow;
      notificationRecorded.Save();
      ExternalDataSubscription dataSubscription = ExternalDataSubscription.Load(serializedRecipientProperties.ToLong());
      if (dataSubscription.IsDeleted)
        dataSubscription = (ExternalDataSubscription) null;
      string str = this.BuildHttpMessage(reading, readingDate, sensor, gateway, network, packetCache.LoadAccount(this.AccountID), notificationRecorded.NotificationRecordedID, originalReading, originalReadingDate, sensorNotificationID, gatewayNotificationID, notificationTriggeredID, serializedRecipientProperties, notificationRecorded.NotificationGUID);
      KeyValuePair<long, string> keyValuePair = packetCache.NotifyingOn.FirstOrDefault<KeyValuePair<long, string>>((System.Func<KeyValuePair<long, string>, bool>) (key => key.Key == sensorNotificationID));
      notificationRecorded.NotifyingOn = !keyValuePair.Equals((object) new KeyValuePair<long, string>()) ? keyValuePair.Value : "";
      notificationRecorded.SensorID = sensor != null ? sensor.SensorID : long.MinValue;
      notificationRecorded.GatewayID = gateway != null ? gateway.GatewayID : long.MinValue;
      notificationRecorded.NotificationID = this.NotificationID;
      notificationRecorded.NotificationType = eNotificationType.HTTP;
      notificationRecorded.SerializedRecipientProperties = serializedRecipientProperties;
      notificationRecorded.Reading = reading;
      notificationRecorded.NotificationSubject = this.Subject;
      notificationRecorded.NotificationContent = str;
      notificationRecorded.NotificationText = this.NotificationText;
      notificationRecorded.SentTo = dataSubscription != null ? dataSubscription.ConnectionInfo1 : "EDS_NULL";
      notificationRecorded.NotificationTriggeredID = notificationTriggeredID;
      notificationRecorded.Save();
      if (dataSubscription == null)
        return;
      packetCache.RecordedNotifications.Add(notificationRecorded);
    }
    catch (Exception ex)
    {
      ex.Log("RecordHTTPNotification(): " + $"{$"{$"{$"{$"{$"{$"{$"{$"(packetCache == null)=[{packetCache == null}]"}, sensor=[{(sensor != null ? "id=" + sensor.SensorID.ToStringSafe() : "null")}] "}, gateway=[{(gateway != null ? "id=" + gateway.GatewayID.ToStringSafe() : "null")}]" + $", snsrNtfcID=[{sensorNotificationID}]" + $", gtwyNtfcID=[{gatewayNotificationID}]"}, network=[{(network != null ? "id=" + network.ToString() : "null")}]" + $", ntfcTrgd=[{notificationTriggeredID}]"}, serializedRcptProps=[{(serializedRecipientProperties != null ? serializedRecipientProperties.ToStringSafe() : "null")}]"}, reading=[{(reading != null ? reading.ToStringSafe() : "null")}]"}, readingDate=[{readingDate.ToStringSafe()}]"}, origReading=[{(originalReading != null ? originalReading.ToStringSafe() : "null")}]"}, origReadingDate=[{originalReadingDate.ToStringSafe()}]");
    }
  }

  private void RecordThermostatAction(
    PacketCache packetCache,
    string reading,
    Gateway gateway,
    Sensor Device,
    string serializedRecipientProperties,
    long sensorNotificationID,
    long notificationTriggeredID,
    long ActionToExecuteID)
  {
    try
    {
      string[] strArray = serializedRecipientProperties.Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      string str1 = strArray[0] == "1" ? "Occupied" : "Unoccupied";
      string str2 = strArray[1];
      string str3 = string.Empty;
      string sensorName = Device != null ? Device.SensorName : "";
      string str4 = string.Empty;
      switch (str1)
      {
        case "Occupied":
          Thermostat.Control(Device, new NameValueCollection()
          {
            {
              "isOccupied",
              "1"
            },
            {
              "Duration",
              str2
            }
          });
          str3 = $"Thermostat : {sensorName} was set to Occupied for {str2} minutes";
          str4 = $"Occupied for {str2} minutes";
          break;
        case "Unoccupied":
          Thermostat.Control(Device, new NameValueCollection()
          {
            {
              "isOccupied",
              "0"
            },
            {
              "Duration",
              str2
            }
          });
          str3 = $"Thermostat : {sensorName} was set to Unoccupied for {str2} minutes";
          str4 = $"Unoccupied for {str2} minutes";
          break;
      }
      NotificationRecorded notificationRecorded = new NotificationRecorded();
      KeyValuePair<long, string> keyValuePair = packetCache.NotifyingOn.FirstOrDefault<KeyValuePair<long, string>>((System.Func<KeyValuePair<long, string>, bool>) (key => key.Key == sensorNotificationID));
      notificationRecorded.NotifyingOn = !keyValuePair.Equals((object) new KeyValuePair<long, string>()) ? keyValuePair.Value : "";
      notificationRecorded.SensorID = this.SensorID;
      notificationRecorded.GatewayID = this.GatewayID;
      notificationRecorded.NotificationID = this.NotificationID;
      notificationRecorded.NotificationType = eNotificationType.Thermostat;
      notificationRecorded.SentToDeviceID = this.SensorID > 0L ? this.SensorID : this.GatewayID;
      notificationRecorded.QueueID = int.MinValue;
      notificationRecorded.Status = str4;
      notificationRecorded.SerializedRecipientProperties = $"{str1}|{serializedRecipientProperties}";
      notificationRecorded.NotificationContent = str3;
      notificationRecorded.NotificationText = str3;
      notificationRecorded.Reading = reading;
      notificationRecorded.NotificationDate = DateTime.UtcNow;
      notificationRecorded.NotificationTriggeredID = notificationTriggeredID;
      notificationRecorded.SentTo = sensorName;
      notificationRecorded.Save();
    }
    catch (Exception ex)
    {
      ex.Log("RecordThermostatAction(): " + $"{$"{$"{$"{$"(packetCache == null)=[{packetCache == null}]"}, device=[{(Device != null ? "id=" + Device.SensorID.ToStringSafe() : "null")}] "}, gateway=[{(gateway != null ? "id=" + gateway.GatewayID.ToStringSafe() : "null")}]" + $", snsrNtfcID=[{sensorNotificationID}]" + $", ntfcTrgd=[{notificationTriggeredID}]"}, serializedRcptProps=[{(serializedRecipientProperties != null ? serializedRecipientProperties.ToStringSafe() : "null")}]"}, reading=[{(reading != null ? reading.ToStringSafe() : "null")}]");
    }
  }

  private void RecordSystemAction(
    PacketCache packetCache,
    string reading,
    Gateway gateway,
    Sensor Device,
    string serializedRecipientProperties,
    long sensorNotificationID,
    long notificationTriggeredID,
    long ActionToExecuteID)
  {
    try
    {
      string name = ActionToExecute.Load(ActionToExecuteID).Name;
      string str1 = string.Empty;
      string str2 = string.Empty;
      string str3 = string.Empty;
      switch (name.ToLower())
      {
        case "acknowledge":
          long num1 = serializedRecipientProperties.ToLong();
          str2 = Notification.Load(num1).Name;
          str1 = $"System Action: Notification {str2} was Acknowledged";
          using (List<NotificationTriggered>.Enumerator enumerator = NotificationTriggered.LoadActiveByNotificationID(num1).GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              NotificationTriggered current = enumerator.Current;
              current.AcknowledgedBy = long.MinValue;
              current.AcknowledgedTime = DateTime.UtcNow;
              current.AcknowledgeMethod = "Acknowledge_Action";
              str3 = "Acknowledged";
              current.Save();
            }
            break;
          }
        case "reset":
          long num2 = serializedRecipientProperties.ToLong();
          str2 = Notification.Load(num2).Name;
          str1 = $"System Action: Notification {str2} was Reset";
          using (List<NotificationTriggered>.Enumerator enumerator = NotificationTriggered.LoadActiveByNotificationID(num2).GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              NotificationTriggered current = enumerator.Current;
              if (current.AcknowledgedTime == DateTime.MinValue)
              {
                current.AcknowledgedBy = long.MinValue;
                current.AcknowledgedTime = DateTime.UtcNow;
                current.AcknowledgeMethod = "Reset _Action";
              }
              current.resetTime = DateTime.UtcNow;
              str3 = "Reset";
              current.Save();
            }
            break;
          }
        case "activate":
          long ID1 = serializedRecipientProperties.ToLong();
          str2 = Notification.Load(ID1).Name;
          str1 = $"System Action: Notification {str2} was Activated";
          Notification notification1 = Notification.Load(ID1);
          str3 = "Activated";
          notification1.IsActive = true;
          notification1.Save();
          break;
        case "deactivate":
          long ID2 = serializedRecipientProperties.ToLong();
          str2 = Notification.Load(ID2).Name;
          str1 = $"System Action: Notification {str2} was Deactivated";
          Notification notification2 = Notification.Load(ID2);
          str3 = "Deactivated";
          notification2.IsActive = false;
          notification2.Save();
          break;
      }
      NotificationRecorded notificationRecorded = new NotificationRecorded();
      KeyValuePair<long, string> keyValuePair = packetCache.NotifyingOn.FirstOrDefault<KeyValuePair<long, string>>((System.Func<KeyValuePair<long, string>, bool>) (key => key.Key == sensorNotificationID));
      notificationRecorded.NotifyingOn = !keyValuePair.Equals((object) new KeyValuePair<long, string>()) ? keyValuePair.Value : "";
      notificationRecorded.SensorID = this.SensorID;
      notificationRecorded.GatewayID = this.GatewayID;
      notificationRecorded.NotificationID = this.NotificationID;
      notificationRecorded.NotificationType = eNotificationType.SystemAction;
      notificationRecorded.SentToDeviceID = Device != null ? Device.SensorID : long.MinValue;
      notificationRecorded.QueueID = int.MinValue;
      notificationRecorded.Status = str3;
      notificationRecorded.SerializedRecipientProperties = $"{name}|{serializedRecipientProperties}";
      notificationRecorded.NotificationContent = str1;
      notificationRecorded.NotificationText = str1;
      notificationRecorded.Reading = reading;
      notificationRecorded.NotificationDate = DateTime.UtcNow;
      notificationRecorded.NotificationTriggeredID = notificationTriggeredID;
      notificationRecorded.SentTo = str2;
      notificationRecorded.Save();
    }
    catch (Exception ex)
    {
      ex.Log("RecordThermostatAction(): " + $"{$"{$"{$"{$"(packetCache == null)=[{packetCache == null}]"}, device=[{(Device != null ? "id=" + Device.SensorID.ToStringSafe() : "null")}] "}, gateway=[{(gateway != null ? "id=" + gateway.GatewayID.ToStringSafe() : "null")}]" + $", snsrNtfcID=[{sensorNotificationID}]" + $", ntfcTrgd=[{notificationTriggeredID}]" + $", actionToExecuteID=[{ActionToExecuteID}]"}, serializedRcptProps=[{(serializedRecipientProperties != null ? serializedRecipientProperties.ToStringSafe() : "null")}]"}, reading=[{(reading != null ? reading.ToStringSafe() : "null")}]");
    }
  }

  public static void SendNotification(
    NotificationRecorded notificationRecorded,
    PacketCache localCache)
  {
    switch (notificationRecorded.NotificationType)
    {
      case eNotificationType.Email:
        Notification.SendEmailNotification(notificationRecorded, localCache);
        break;
      case eNotificationType.SMS:
        Notification.SendSMSNotification(notificationRecorded, localCache);
        break;
      case eNotificationType.Both:
        Notification.SendEmailNotification(notificationRecorded, localCache);
        Notification.SendSMSNotification(notificationRecorded, localCache);
        break;
      case eNotificationType.Phone:
        Notification.SendPOTSNotification(notificationRecorded, localCache);
        break;
      case eNotificationType.HTTP:
        Notification.SendHTTPNotification(notificationRecorded, localCache);
        break;
      case eNotificationType.Push_Message:
        Notification.SendPushNotification(notificationRecorded, localCache);
        break;
    }
    notificationRecorded.Save();
  }

  private static bool SendSMTPNotification(
    Customer cust,
    string emailAddress,
    bool isHtml,
    NotificationRecorded notificationRecorded)
  {
    try
    {
      using (MailMessage mail = new MailMessage())
      {
        using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, cust.Account))
        {
          mail.To.SafeAdd(emailAddress, $"{cust.FirstName} {cust.LastName}");
          mail.Subject = WebUtility.HtmlDecode(notificationRecorded.NotificationSubject);
          mail.Body = WebUtility.HtmlDecode(notificationRecorded.NotificationText);
          mail.IsBodyHtml = isHtml;
          string[] strArray = new string[5]
          {
            "{\"metadata\":{ \"NotificationRecordedID\": \"",
            null,
            null,
            null,
            null
          };
          long num = notificationRecorded.NotificationRecordedID;
          strArray[1] = num.ToString();
          strArray[2] = "\", \"CustomerID\": \"";
          num = notificationRecorded.CustomerID;
          strArray[3] = num.ToString();
          strArray[4] = "\"}}";
          string str1 = string.Concat(strArray);
          mail.Headers.Add("X-MSYS-API", str1);
          string str2 = ConfigData.AppSettings("AwsSesConfigSet");
          if (!string.IsNullOrEmpty(str2))
            mail.Headers.Add("X-SES-CONFIGURATION-SET", str2);
          if (string.IsNullOrWhiteSpace(emailAddress) || mail.To.Count != 0)
            return MonnitUtil.SendMail(mail, smtpClient);
          notificationRecorded.Status = "Opted Out";
          notificationRecorded.DoRetry = false;
          return false;
        }
      }
    }
    catch (Exception ex)
    {
      notificationRecorded.Status = ex.Message;
      return false;
    }
  }

  private static void SendEmailNotification(
    NotificationRecorded notificationRecorded,
    PacketCache localCache)
  {
    try
    {
      Customer cust = localCache.LoadCustomer(notificationRecorded.CustomerID);
      if (Notification.SendSMTPNotification(cust, cust.NotificationEmail, true, notificationRecorded))
      {
        notificationRecorded.Status = "Email Sent";
        notificationRecorded.Delivered = true;
        notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
        notificationRecorded.DoRetry = false;
      }
      else
      {
        notificationRecorded.Status = $"Email Failed: {cust.NotificationEmail} {notificationRecorded.Status}";
        notificationRecorded.Delivered = false;
        notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
        notificationRecorded.DoRetry = true;
        ++notificationRecorded.RetryCount;
      }
    }
    catch (Exception ex)
    {
      NotificationRecorded notificationRecorded1 = notificationRecorded;
      notificationRecorded1.Status = $"{notificationRecorded1.Status} Error: {ex.Message}";
      notificationRecorded.DoRetry = true;
      ++notificationRecorded.RetryCount;
    }
  }

  private static void SendSMSNotification(
    NotificationRecorded notificationRecorded,
    PacketCache localCache)
  {
    try
    {
      Customer customer = localCache.LoadCustomer(notificationRecorded.CustomerID);
      if (!string.IsNullOrEmpty(customer.NotificationPhone) && customer.SendSensorNotificationToText)
      {
        if (customer.DirectSMS && !string.IsNullOrEmpty(ConfigData.AppSettings("TwilioAccountSid")))
        {
          Country byIsoCodeOrNumber = Country.FindByISOCodeOrNumber(customer.NotificationPhoneISOCode, customer.NotificationPhone);
          int creditsToCharge = byIsoCodeOrNumber != null ? byIsoCodeOrNumber.SMSCost : 10;
          if (NotificationCredit.Charge(customer.AccountID, creditsToCharge))
          {
            notificationRecorded.NotificaitonCreditCount = creditsToCharge;
            string fromNumber = MonnitUtil.GetFromNumber(customer.Account, byIsoCodeOrNumber);
            string ToNumber = customer.NotificationPhone.Replace(" ", "").Replace("-", "");
            string body = Notification.ReplaceAngleBracketsWithEncodedText(notificationRecorded.NotificationText);
            MonnitUtil.SendTwilioSMS(notificationRecorded.NotificationRecordedID, customer, fromNumber, ToNumber, body);
            CreditSetting.CheckCreditsRemaining(customer.AccountID, customer);
            notificationRecorded.Status = "SMS Queued";
            notificationRecorded.DoRetry = false;
          }
          else
          {
            notificationRecorded.NotificationText += "\r\nInsufficient credits to send SMS";
            if (Notification.SendSMTPNotification(customer, customer.NotificationEmail, false, notificationRecorded))
            {
              notificationRecorded.Status = "SMS Aborted Insufficient Credits; Email Sent";
              notificationRecorded.Delivered = true;
              notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
              notificationRecorded.DoRetry = false;
            }
            else
            {
              notificationRecorded.Status = "SMS Aborted Insufficient Credits; Email Failed: " + notificationRecorded.Status;
              notificationRecorded.Delivered = false;
              notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
              notificationRecorded.DoRetry = true;
              ++notificationRecorded.RetryCount;
            }
          }
        }
        else
        {
          string emailAddress = string.Empty;
          SMSCarrier smsCarrier = SMSCarrier.Load(customer.SMSCarrierID);
          if (smsCarrier != null)
          {
            string str = smsCarrier.SMSAddress(customer.NotificationPhone);
            if (str != string.Empty)
              emailAddress = str;
          }
          if (string.IsNullOrEmpty(emailAddress))
          {
            emailAddress = customer.NotificationEmail;
            if (!notificationRecorded.NotificationText.Contains("SMS failed due to invalid settings"))
              notificationRecorded.NotificationText += " SMS failed due to invalid settings";
          }
          if (Notification.SendSMTPNotification(customer, emailAddress, false, notificationRecorded))
          {
            notificationRecorded.Status = "SMS via SMTP Sent";
            notificationRecorded.Delivered = true;
            notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
            notificationRecorded.DoRetry = false;
          }
          else
          {
            notificationRecorded.Status = "SMS via SMTP Failed: " + notificationRecorded.Status;
            notificationRecorded.Delivered = false;
            notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
            notificationRecorded.DoRetry = true;
            ++notificationRecorded.RetryCount;
          }
        }
      }
      else
      {
        notificationRecorded.NotificationText += "\r\nNo notification phone to send SMS";
        if (Notification.SendSMTPNotification(customer, customer.NotificationEmail, false, notificationRecorded))
        {
          notificationRecorded.Status = "SMS Aborted invalid notification phone; Email Sent";
          notificationRecorded.Delivered = true;
          notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
          notificationRecorded.DoRetry = false;
        }
        else
        {
          notificationRecorded.Status = "SMS Aborted invalid notification phone; Email Failed: " + notificationRecorded.Status;
          notificationRecorded.Delivered = false;
          notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
          notificationRecorded.DoRetry = true;
          ++notificationRecorded.RetryCount;
        }
      }
    }
    catch (Exception ex)
    {
      NotificationRecorded notificationRecorded1 = notificationRecorded;
      notificationRecorded1.Status = $"{notificationRecorded1.Status} Error Check Mobile Number: {ex.Message}";
      notificationRecorded.DoRetry = true;
      ++notificationRecorded.RetryCount;
    }
  }

  private static void SendPOTSNotification(
    NotificationRecorded notificationRecorded,
    PacketCache localCache)
  {
    if (string.IsNullOrEmpty(ConfigData.AppSettings("TwilioAccountSid")))
    {
      Notification.SendEmailNotification(notificationRecorded, localCache);
    }
    else
    {
      try
      {
        Customer customer = localCache.LoadCustomer(notificationRecorded.CustomerID);
        Account.Load(customer.AccountID);
        if (customer.SendSensorNotificationToVoice && !string.IsNullOrEmpty(customer.NotificationPhone2))
        {
          Country byIsoCodeOrNumber = Country.FindByISOCodeOrNumber(customer.NotificationPhone2ISOCode, customer.NotificationPhone2);
          int creditsToCharge = byIsoCodeOrNumber != null ? byIsoCodeOrNumber.VoiceCost : 30;
          if (NotificationCredit.Charge(customer.AccountID, creditsToCharge))
          {
            notificationRecorded.NotificaitonCreditCount = creditsToCharge;
            string fromNumberForVoice = MonnitUtil.GetFromNumberForVoice(customer.Account);
            string notificationPhone2 = customer.NotificationPhone2;
            MonnitUtil.SendTwilioCallback(notificationRecorded, customer, fromNumberForVoice, notificationPhone2);
            CreditSetting.CheckCreditsRemaining(customer.AccountID, customer);
            notificationRecorded.Status = "Call Queued";
            notificationRecorded.DoRetry = false;
          }
          else
          {
            notificationRecorded.NotificationText += "\r\nInsufficient credits to call phone";
            if (Notification.SendSMTPNotification(customer, customer.NotificationEmail, false, notificationRecorded))
            {
              notificationRecorded.Status = "Call Aborted Insufficient Credits; Email Sent";
              notificationRecorded.Delivered = true;
              notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
              notificationRecorded.DoRetry = false;
            }
            else
            {
              notificationRecorded.Status = "Call Aborted Insufficient Credits; Email Failed: " + notificationRecorded.Status;
              notificationRecorded.Delivered = false;
              notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
              notificationRecorded.DoRetry = true;
              ++notificationRecorded.RetryCount;
            }
          }
        }
        else
        {
          notificationRecorded.NotificationText += "\r\nNo notification phone to call";
          if (Notification.SendSMTPNotification(customer, customer.NotificationEmail, false, notificationRecorded))
          {
            notificationRecorded.Status = "Call Aborted invalid notification phone; Email Sent";
            notificationRecorded.Delivered = true;
            notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
            notificationRecorded.DoRetry = false;
          }
          else
          {
            notificationRecorded.Status = "Call Aborted invalid notification phone; Email Failed: " + notificationRecorded.Status;
            notificationRecorded.Delivered = false;
            notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
            notificationRecorded.DoRetry = true;
            ++notificationRecorded.RetryCount;
          }
        }
      }
      catch (Exception ex)
      {
        notificationRecorded.Status = "Invalid Phone Exception: " + ex.Message;
        notificationRecorded.DoRetry = true;
        ++notificationRecorded.RetryCount;
      }
    }
  }

  private static void SendHTTPNotification(
    NotificationRecorded notificationRecorded,
    PacketCache localCache)
  {
    ExternalDataSubscription dataSubscription = ExternalDataSubscription.Load(notificationRecorded.SerializedRecipientProperties.ToLong());
    if (dataSubscription == null)
      return;
    ExternalDataSubscriptionAttempt attempt = dataSubscription.BuildEDSAttempt(notificationRecorded.NotificationContent);
    if (attempt == null || !ExternalDataSubscription.FirstExternalSubscriptionAttemptImmediate)
      return;
    dataSubscription.Send(attempt);
  }

  public static void SendPushNotification(
    NotificationRecorded notificationRecorded,
    PacketCache localCache)
  {
    bool flag = false;
    string newValue = "";
    Customer customer = (Customer) null;
    try
    {
      customer = localCache.LoadCustomer(notificationRecorded.CustomerID);
      List<CustomerPushMessageSubscription> messageSubscriptionList = CustomerPushMessageSubscription.LoadByCustomerID(notificationRecorded.CustomerID);
      string subject = "mailto:" + customer.NotificationEmail;
      if (customer != null && customer.Account != null && customer.Account.getTheme() != null && customer.Account.getTheme().Domain.Length > 0)
        subject = "https://" + customer.Account.getTheme().Domain;
      string publicKey = ConfigData.AppSettings("Vapid_PublicKey");
      string privateKey = ConfigData.AppSettings("Vapid_PrivateKey");
      foreach (CustomerPushMessageSubscription messageSubscription in messageSubscriptionList)
      {
        PushSubscription subscription = new PushSubscription(messageSubscription.EndpointUrl, messageSubscription.P256DH, messageSubscription.Auth);
        VapidDetails vapidDetails = new VapidDetails(subject, publicKey, privateKey);
        try
        {
          string url = $"https://{customer.Account.getTheme().Domain}/Ack/{notificationRecorded.NotificationRecordedID}/{notificationRecorded.NotificationGUID}";
          using (WebPushClient webPushClient = new WebPushClient())
          {
            string payload = Notification.PushNotificationPayload(notificationRecorded.NotificationSubject, notificationRecorded.NotificationText.Replace("\n", "\\n"), url);
            webPushClient.SendNotification(subscription, payload, vapidDetails);
            messageSubscription.LastSentDate = DateTime.UtcNow;
            messageSubscription.Save();
            notificationRecorded.Status = "Push Sent";
            notificationRecorded.Delivered = true;
            notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
            notificationRecorded.DoRetry = false;
          }
        }
        catch (WebPushException ex1)
        {
          WebPushException ex2 = ex1;
          string[] strArray1 = new string[7]
          {
            "Notification.SendPushNotification[WebPushClient-WebPushException][Name: ",
            messageSubscription.Name,
            "][Status: ",
            null,
            null,
            null,
            null
          };
          HttpStatusCode statusCode = ex1.StatusCode;
          strArray1[3] = statusCode.ToString();
          strArray1[4] = "][Subject: ";
          strArray1[5] = subject;
          strArray1[6] = "] Message: ";
          string function = string.Concat(strArray1);
          ex2.Log(function);
          notificationRecorded.Status = string.Format("{1} Push Failed: {0} |", (object) messageSubscription.Name, (object) notificationRecorded.Status);
          notificationRecorded.Delivered = false;
          notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
          notificationRecorded.DoRetry = true;
          ++notificationRecorded.RetryCount;
          if (ex1.StatusCode == HttpStatusCode.Gone || ex1.StatusCode == HttpStatusCode.NotFound || ex1.StatusCode == HttpStatusCode.Forbidden && ex1.Message.Contains("the VAPID credentials in the authorization header do not correspond to the credentials used to create the subscriptions"))
          {
            flag = true;
            string[] strArray2 = new string[8]
            {
              newValue,
              "[Name: ",
              messageSubscription.Name,
              "][Status: ",
              null,
              null,
              null,
              null
            };
            statusCode = ex1.StatusCode;
            strArray2[4] = statusCode.ToString();
            strArray2[5] = "][Message: ";
            strArray2[6] = ex1.Message;
            strArray2[7] = "]<br/>";
            newValue = string.Concat(strArray2);
          }
        }
        catch (Exception ex)
        {
          notificationRecorded.Status = string.Format("{1} Push Failed: {0} |", (object) messageSubscription.Name, (object) notificationRecorded.Status);
          notificationRecorded.Delivered = false;
          notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
          notificationRecorded.DoRetry = true;
          ++notificationRecorded.RetryCount;
          ex.Log($"Notification.SendPushNotification[WebPushClient-Exception][Name: {messageSubscription.Name}][Subject: {subject}] Message: ");
          flag = true;
          newValue = $"{newValue}[Name: {messageSubscription.Name} Message: {ex.Message}]<br/>";
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("Notification.SendPushNotification ");
      flag = true;
      newValue = $"{newValue}[Message: {ex.Message}]<br/>";
    }
    if (!flag || customer == null)
      return;
    try
    {
      using (MailMessage mail = new MailMessage())
      {
        using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, customer.Account))
        {
          string notificationEmail = customer.NotificationEmail;
          mail.To.SafeAdd(notificationEmail, $"{customer.FirstName} {customer.LastName}");
          mail.Subject = WebUtility.HtmlDecode(notificationRecorded.NotificationSubject);
          mail.Body = WebUtility.HtmlDecode(notificationRecorded.NotificationText);
          mail.Body += "<br/>Please visit PWA iMonnit again to refresh connection information for being able to receive PWA Push Notifications.";
          mail.Body += mail.Body.Replace("{Debug}", newValue);
          mail.IsBodyHtml = true;
          string[] strArray = new string[5]
          {
            "{\"metadata\":{ \"NotificationRecordedID\": \"",
            null,
            null,
            null,
            null
          };
          long num = notificationRecorded.NotificationRecordedID;
          strArray[1] = num.ToString();
          strArray[2] = "\", \"CustomerID\": \"";
          num = notificationRecorded.CustomerID;
          strArray[3] = num.ToString();
          strArray[4] = "\"}}";
          string str1 = string.Concat(strArray);
          mail.Headers.Add("X-MSYS-API", str1);
          string str2 = ConfigData.AppSettings("AwsSesConfigSet");
          if (!string.IsNullOrEmpty(str2))
            mail.Headers.Add("X-SES-CONFIGURATION-SET", str2);
          if (!string.IsNullOrWhiteSpace(notificationEmail) && mail.To.Count == 0)
          {
            notificationRecorded.Status = "Opted Out";
            notificationRecorded.DoRetry = false;
            notificationRecorded.Save();
          }
          MonnitUtil.SendMail(mail, smtpClient);
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log("Notification.SendPushNotification[SendFailedEmail] ");
      notificationRecorded.Status = ex.Message;
      notificationRecorded.Save();
    }
  }

  private static string PushNotificationPayload(string title, string body, string url)
  {
    return $"{{\"title\": \"{title}\",\"body\": \"{body}\",\"icon\": \"/PWAIcon/512x512\",\"tag\": \"Notification\",\"data\": {{ \"clickUrl\": \"{url}\"}}}}";
  }

  public static string SendPushNotificationTest(
    string payload,
    Customer cust,
    string endpoint,
    string p256dhKey,
    string authKey)
  {
    string str1 = "No attempt made";
    bool flag = false;
    try
    {
      string subject = "mailto:" + cust.NotificationEmail;
      if (cust != null && cust.Account != null && cust.Account.getTheme() != null && cust.Account.getTheme().Domain.Length > 0)
        subject = "https://" + cust.Account.getTheme().Domain;
      string publicKey = ConfigData.AppSettings("Vapid_PublicKey");
      string privateKey = ConfigData.AppSettings("Vapid_PrivateKey");
      PushSubscription subscription = new PushSubscription(endpoint, p256dhKey, authKey);
      VapidDetails vapidDetails = new VapidDetails(subject, publicKey, privateKey);
      try
      {
        using (WebPushClient webPushClient = new WebPushClient())
        {
          if (string.IsNullOrWhiteSpace(payload))
            payload = Notification.PushNotificationPayload("iMonnit Push Title", "this is \\n a message", "/");
          webPushClient.SendNotification(subscription, payload, vapidDetails);
          str1 = "Success";
        }
      }
      catch (WebPushException ex)
      {
        ex.Log($"Notification.SendPushNotificationTest[WebPushClient-WebPushException][Name: Test][Status: {ex.StatusCode.ToString()}][Subject: {subject}] Message: ");
        str1 = "Failed to establish client";
        if (ex.StatusCode == HttpStatusCode.Gone || ex.StatusCode == HttpStatusCode.NotFound || ex.StatusCode == HttpStatusCode.Forbidden && ex.Message.Contains("the VAPID credentials in the authorization header do not correspond to the credentials used to create the subscriptions"))
          flag = true;
      }
      catch (Exception ex)
      {
        ex.Log($"Notification.SendPushNotificationTest[WebPushClient-Exception][Name: Test][Subject: {subject}] Message: ");
        str1 = "Failed to establish client";
        flag = true;
      }
    }
    catch (Exception ex)
    {
      ex.Log("Notification.SendPushNotificationTest ");
      str1 = "Failed to send";
      flag = true;
    }
    if (flag)
    {
      try
      {
        using (MailMessage mail = new MailMessage())
        {
          using (SmtpClient smtpClient = MonnitUtil.GetSMTPClient(mail, cust.Account))
          {
            string notificationEmail = cust.NotificationEmail;
            mail.To.SafeAdd(notificationEmail, $"{cust.FirstName} {cust.LastName}");
            mail.Subject = WebUtility.HtmlDecode("iMonnit Push test failed email");
            mail.Body = WebUtility.HtmlDecode("this is a fail test email");
            mail.Body += "<br/>Please visit PWA iMonnit again to refresh connection information for being able to receive PWA Push Notifications.";
            mail.IsBodyHtml = true;
            string str2 = "{\"metadata\":{ \"NotificationRecordedID\": \"1234\", \"CustomerID\": \"1234\"}}";
            mail.Headers.Add("X-MSYS-API", str2);
            string str3 = ConfigData.AppSettings("AwsSesConfigSet");
            if (!string.IsNullOrEmpty(str3))
              mail.Headers.Add("X-SES-CONFIGURATION-SET", str3);
            if (!string.IsNullOrWhiteSpace(notificationEmail) && mail.To.Count == 0)
              str1 = "Opted Out";
            MonnitUtil.SendMail(mail, smtpClient);
          }
        }
      }
      catch (Exception ex)
      {
        ex.Log("Notification.SendPushNotificationTest[SendFailedEmail] ");
        str1 = "Failed to send email from failed push message attempt: " + ex.Message;
      }
    }
    return str1;
  }

  public Notification.SendPushNotificationObj SendPushNotification(
    string serverApiKey,
    string senderId,
    string deviceId,
    string message)
  {
    Notification.SendPushNotificationObj pushNotificationObj = new Notification.SendPushNotificationObj();
    try
    {
      WebRequest webRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
      webRequest.Method = "post";
      webRequest.ContentType = "application/json";
      var data = new
      {
        notification = new
        {
          title = "iMonnit Notification ",
          body = message,
          icon = "https://www.imonnit.com/favicon.ico"
        },
        to = deviceId
      };
      byte[] bytes = Encoding.UTF8.GetBytes(new JavaScriptSerializer().Serialize((object) data));
      webRequest.Headers.Add($"Authorization:key={serverApiKey}");
      webRequest.Headers.Add($"Sender: id={senderId}");
      webRequest.ContentLength = (long) bytes.Length;
      using (Stream requestStream = webRequest.GetRequestStream())
      {
        requestStream.Write(bytes, 0, bytes.Length);
        using (WebResponse response = webRequest.GetResponse())
        {
          using (Stream responseStream = response.GetResponseStream())
          {
            using (StreamReader streamReader = new StreamReader(responseStream))
            {
              string end = streamReader.ReadToEnd();
              pushNotificationObj.Response = end;
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      pushNotificationObj.Successful = false;
      pushNotificationObj.Response = (string) null;
      pushNotificationObj.Error = ex;
    }
    return pushNotificationObj;
  }

  private static void SendAdvertisement(
    NotificationRecorded notificationRecorded,
    PacketCache localCache)
  {
  }

  public static void RecordAVDisplay(
    string reading,
    Sensor sensor,
    Gateway gateway,
    Sensor Device)
  {
    if (Device.LastCommunicationDate < DateTime.UtcNow.AddDays(-1.0))
      return;
    long num1 = long.MinValue;
    long num2 = long.MinValue;
    string str1 = string.Empty;
    string str2 = Notification.BuildAVMessage(Device, reading, sensor, gateway, reading);
    if (sensor != null)
    {
      str1 = sensor.SensorName;
      num1 = sensor.SensorID;
    }
    else if (gateway != null)
    {
      str1 = gateway.Name;
      num2 = gateway.GatewayID;
    }
    new NotificationRecorded()
    {
      SensorID = num1,
      GatewayID = num2,
      NotificationType = eNotificationType.Local_Notifier_Display,
      SentToDeviceID = Device.SensorID,
      QueueID = NotificationRecorded.NextQueueID(Device.SensorID),
      SerializedRecipientProperties = $"{(System.ValueType) false}|{(System.ValueType) false}|{(System.ValueType) false}|{(System.ValueType) false}|{str1}",
      Reading = reading,
      NotificationText = str2,
      NotificationDate = DateTime.UtcNow,
      SentTo = Device.SensorName
    }.Save();
    if (!Device.PendingActionControlCommand)
    {
      Device.PendingActionControlCommand = true;
      Device.Save();
    }
    CSNet.SetGatewaysUrgentTrafficFlag(Device.CSNetID);
    if (gateway == null || gateway.CSNetID != Device.CSNetID)
      return;
    gateway.UrgentTraffic = true;
  }

  private static string BuildAVMessage(
    Sensor Device,
    string reading,
    Sensor sensor,
    Gateway gateway,
    string displayMsg)
  {
    StringBuilder stringBuilder = new StringBuilder();
    int startIndex = 41;
    string str1 = displayMsg;
    str1.Replace("°", "");
    string str2 = Notification.UnFormat(str1);
    if (str2.Length > startIndex)
      str2.Substring(startIndex);
    return str2;
  }

  public string ReplaceVariablesPreview(
    string baseText,
    Sensor sensor,
    Gateway gateway,
    CSNet network,
    Account account)
  {
    string str = sensor == null || sensor.LastDataMessage == null ? "{Reading}" : sensor.LastDataMessage.DisplayData;
    DateTime dateTime = sensor == null || sensor.LastDataMessage == null ? DateTime.UtcNow : sensor.LastDataMessage.MessageDate;
    return this.ReplaceVariables(baseText, str, sensor, gateway, network, account, 0L, dateTime, str, dateTime, Guid.Empty);
  }

  private string ReplaceVariables(
    string baseText,
    string reading,
    Sensor sensor,
    Gateway gateway,
    CSNet network,
    Account account,
    long notificationRecordedID,
    DateTime readingDate,
    string originalReading,
    DateTime originalReadingDate,
    Guid notificationRecordedGuid)
  {
    return this.ReplaceVariables(baseText, reading, sensor, gateway, network, account, notificationRecordedID, readingDate, originalReading, originalReadingDate, notificationRecordedGuid, (Customer) null);
  }

  private string ReplaceVariables(
    string baseText,
    string reading,
    Sensor sensor,
    Gateway gateway,
    CSNet network,
    Account account,
    long notificationRecordedID,
    DateTime readingDate,
    string originalReading,
    DateTime originalReadingDate,
    Guid notificationRecordedGuid,
    Customer customer)
  {
    try
    {
      if (customer == null)
        customer = new Customer();
      if (account == null)
        account = Account.Load(this.AccountID);
      if (account == null)
        account = new Account();
      AccountTheme accountTheme = AccountTheme.Find(this.AccountID);
      Dictionary<string, string> dictionary = Preference.LoadPreferences(accountTheme.AccountThemeID, this.AccountID, customer.CustomerID);
      string format1 = dictionary.ContainsKey("Date Format") ? dictionary["Date Format"] : CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern;
      string format2 = dictionary.ContainsKey("Time Format") ? dictionary["Time Format"] : CultureInfo.InvariantCulture.DateTimeFormat.LongTimePattern;
      DateTime localTimeById1 = TimeZone.GetLocalTimeById(DateTime.UtcNow, account.TimeZoneID);
      DateTime localTimeById2 = TimeZone.GetLocalTimeById(readingDate, account.TimeZoneID);
      DateTime localTimeById3 = TimeZone.GetLocalTimeById(originalReadingDate, account.TimeZoneID);
      string overrideValue = $"https://{accountTheme.Domain}/Ack/{notificationRecordedID}/{notificationRecordedGuid}";
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("<a href='{0}'>Acknowledge</a>", (object) overrideValue);
      StringBuilder Text = Notification.TextReplace(Notification.TextReplace(Notification.TextReplace(Notification.TextReplace(Notification.TextReplace(Notification.TextReplace(Notification.TextReplace(Notification.TextReplace(Notification.TextReplace(Notification.TextReplace(Notification.TextReplace(Notification.TextReplace(Notification.TextReplace(new StringBuilder(baseText), "{Subject}", this.Subject), "{Reading}", reading), "{Notification}", this.Name), "{Date}", localTimeById1.ToString(format1)), "{Time}", localTimeById1.ToString(format2)), "{ReadingDate}", localTimeById2.ToString(format1)), "{ReadingTime}", localTimeById2.ToString(format2)), "{OriginalReadingDate}", localTimeById3.ToString(format1)), "{OriginalReadingTime}", localTimeById3.ToString(format2)), "{OriginalReading}", originalReading), "{AcknowledgeURL}", overrideValue), "{Acknowledge}", stringBuilder.ToString()), "{ParentAccount}", account == null || account.RetailAccount == null ? "" : account.RetailAccount.AccountNumber);
      Notification.ReplaceDeviceInfo(sensor, gateway, Text);
      Notification.ReplaceNetworkInfo(network, Text);
      Notification.ReplaceAccountInfo(account, Text);
      return Text.ToString();
    }
    catch (Exception ex)
    {
      ex.Log("ReplaceVariables(): " + $"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{$"{string.Empty}, sensor=[{(sensor != null ? "id=" + sensor.SensorID.ToStringSafe() : "null")}] "}, baseText=[{(baseText != null ? baseText.ToStringSafe() : "null")}] "}, gateway=[{(gateway != null ? "id=" + gateway.GatewayID.ToStringSafe() : "null")}]"}, network=[{(account != null ? "id=" + account.AccountID.ToString() : "null")}]" + $", ntfcRecordedID=[{notificationRecordedID}]"}, network=[id={notificationRecordedGuid.ToString()}]"}, network=[{(network != null ? "id=" + network.CSNetID.ToString() : "null")}]"}, network=[{(account != null ? "id=" + account.AccountID.ToString() : "null")}]"}, customer=[{(customer != null ? "id=" + customer.CustomerID.ToStringSafe() : "null")}]"}, reading=[{(reading != null ? reading.ToStringSafe() : "null")}]"}, readingDate=[{readingDate.ToStringSafe()}]"}, origReading=[{(originalReading != null ? originalReading.ToStringSafe() : "null")}]"}, origReadingDate=[{originalReadingDate.ToStringSafe()}]");
    }
    return "Error";
  }

  private static StringBuilder TextReplace(
    StringBuilder text,
    string overrideKey,
    string overrideValue)
  {
    text = text.Replace(overrideKey, overrideValue);
    text = text.Replace(overrideKey.ToUpper(), overrideValue);
    text = text.Replace(overrideKey.ToLower(), overrideValue);
    return text;
  }

  private static void ReplaceAccountInfo(Account account, StringBuilder Text)
  {
    if (account != null)
    {
      Text = Notification.TextReplace(Text, "{AccountNumber}", account.AccountNumber);
      Text = Notification.TextReplace(Text, "{CompanyName}", account.CompanyName);
      Text = Notification.TextReplace(Text, "{AccountID}", account.AccountID.ToString());
      Text = Notification.TextReplace(Text, "{CompanyName_20}", Notification.SetMaxLength(account.CompanyName, 20));
    }
    else
    {
      Text = Notification.TextReplace(Text, "{AccountNumber}", string.Empty);
      Text = Notification.TextReplace(Text, "{CompanyName}", string.Empty);
      Text = Notification.TextReplace(Text, "{CompanyName_20}", string.Empty);
    }
    if (account != null && account.PrimaryAddress != null)
    {
      Text = Notification.TextReplace(Text, "{Address}", account.PrimaryAddress.Address);
      Text = Notification.TextReplace(Text, "{Address2}", account.PrimaryAddress.Address2);
      Text = Notification.TextReplace(Text, "{City}", account.PrimaryAddress.City);
      Text = Notification.TextReplace(Text, "{State}", account.PrimaryAddress.State);
      Text = Notification.TextReplace(Text, "{PostalCode}", account.PrimaryAddress.PostalCode);
      Text = Notification.TextReplace(Text, "{Country}", account.PrimaryAddress.Country);
    }
    else
    {
      Text = Notification.TextReplace(Text, "{Address}", string.Empty);
      Text = Notification.TextReplace(Text, "{Address2}", string.Empty);
      Text = Notification.TextReplace(Text, "{City}", string.Empty);
      Text = Notification.TextReplace(Text, "{State}", string.Empty);
      Text = Notification.TextReplace(Text, "{PostalCode}", string.Empty);
      Text = Notification.TextReplace(Text, "{Country}", string.Empty);
    }
  }

  private static void ReplaceNetworkInfo(CSNet network, StringBuilder Text)
  {
    if (network != null)
    {
      Text = Notification.TextReplace(Text, "{Network}", network.Name);
      Text = Notification.TextReplace(Text, "{NetworkID}", network.CSNetID.ToString());
    }
    else
      Text = Notification.TextReplace(Text, "{Network}", string.Empty);
  }

  private static void ReplaceDeviceInfo(Sensor sensor, Gateway gateway, StringBuilder Text)
  {
    if (sensor != null)
    {
      Text = Notification.TextReplace(Text, "{ID}", sensor.SensorID.ToString());
      Text = Notification.TextReplace(Text, "{Name}", sensor.SensorName);
      Text = Notification.TextReplace(Text, "{EquipmentMake}", sensor.Make);
      Text = Notification.TextReplace(Text, "{EquipmentModel}", sensor.Model);
      Text = Notification.TextReplace(Text, "{EquipmentSerialNumber}", sensor.SerialNumber);
      Text = Notification.TextReplace(Text, "{EquipmentSensorLocation}", sensor.Location);
      Text = Notification.TextReplace(Text, "{EquipmentNote}", sensor.Note);
      Text = Notification.TextReplace(Text, "{EquipmentSensorDescription}", sensor.Description);
      Text = Notification.TextReplace(Text, "{EquipmentSensorLocation_20}", Notification.SetMaxLength(sensor.Location, 20));
      Text = Notification.TextReplace(Text, "{EquipmentSensorDescription_20}", Notification.SetMaxLength(sensor.Description, 20));
    }
    else
    {
      if (gateway != null)
      {
        Text = Notification.TextReplace(Text, "{ID}", gateway.GatewayID.ToString());
        Text = Notification.TextReplace(Text, "{Name}", gateway.Name);
      }
      else
      {
        Text = Notification.TextReplace(Text, "{ID}", string.Empty);
        Text = Notification.TextReplace(Text, "{Name}", string.Empty);
      }
      Text = Notification.TextReplace(Text, "{EquipmentMake}", string.Empty);
      Text = Notification.TextReplace(Text, "{EquipmentModel}", string.Empty);
      Text = Notification.TextReplace(Text, "{EquipmentSerialNumber}", string.Empty);
      Text = Notification.TextReplace(Text, "{EquipmentSensorLocation}", string.Empty);
      Text = Notification.TextReplace(Text, "{EquipmentNote}", string.Empty);
      Text = Notification.TextReplace(Text, "{EquipmentSensorDescription}", string.Empty);
      Text = Notification.TextReplace(Text, "{EquipmentSensorLocation_20}", string.Empty);
      Text = Notification.TextReplace(Text, "{EquipmentSensorDescription_20}", string.Empty);
    }
  }

  private static string SetMaxLength(string value, int maxLength)
  {
    string empty = string.Empty;
    return value.Length <= maxLength ? value : value.Substring(0, maxLength);
  }

  public bool CanAddSensor(long sensorID, int datumIndex)
  {
    Sensor sensor = Sensor.Load(sensorID);
    eDatumType datumType = sensor.getDatumType(datumIndex);
    bool flag = false;
    switch (this.NotificationClass)
    {
      case eNotificationClass.Application:
        if (this.eDatumType == datumType)
        {
          flag = true;
          break;
        }
        break;
      case eNotificationClass.Inactivity:
        flag = true;
        break;
      case eNotificationClass.Low_Battery:
        flag = true;
        break;
      case eNotificationClass.Advanced:
        flag = AdvancedNotification.Load(this.AdvancedNotificationID).CanAdd((object) sensor);
        break;
      default:
        flag = false;
        break;
    }
    return flag;
  }

  public static string UnFormat(string value, bool removeNewLine = true)
  {
    string input = Regex.Replace(value, "<[^>]+>|&nbsp;|\\r|\\t", " ").Trim();
    if (removeNewLine)
      input = input.Replace('\n', ' ');
    return new Regex("[ ]{2,}", RegexOptions.None).Replace(input, " ");
  }

  private string BuildSubject(
    string reading,
    Sensor sensor,
    Gateway gateway,
    CSNet network,
    EmailTemplate template,
    Account account,
    long notificationRecordedID,
    DateTime readingDate,
    string originalReading,
    DateTime originalReadingDate,
    Guid notificationRecordedGuid)
  {
    return this.ReplaceVariables(!string.IsNullOrEmpty(this.Subject) ? this.Subject : template.Subject, reading, sensor, gateway, network, account, notificationRecordedID, readingDate, originalReading, originalReadingDate, notificationRecordedGuid);
  }

  private string BuildEmailMessage(
    Customer cust,
    string reading,
    DateTime readingDate,
    Sensor sensor,
    Gateway gateway,
    CSNet network,
    Account account,
    long notificationRecordedID,
    string originalReading,
    DateTime originalReadingDate,
    Guid notificationRecordedGuid)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("{0}<br />", (object) cust.FullName);
    stringBuilder.Append("<br />");
    string str = this.ReplaceVariables(this.NotificationText, reading, sensor, gateway, network, account, notificationRecordedID, readingDate, originalReading, originalReadingDate, notificationRecordedGuid, cust);
    stringBuilder.Append(str);
    stringBuilder.Append("<br />");
    if (str == this.NotificationText)
    {
      stringBuilder.Append("<br />");
      if (sensor != null)
      {
        stringBuilder.AppendFormat("Sensor: {0}<br />", (object) sensor.SensorName);
        stringBuilder.AppendFormat("Sensor Type: {0}<br />", (object) sensor.MonnitApplication.ApplicationName);
        if (network != null)
          stringBuilder.AppendFormat("Network: {0}<br />", (object) network.Name);
      }
      if (gateway != null)
      {
        stringBuilder.AppendFormat("Gateway: {0}<br />", (object) gateway.Name);
        if (network != null)
          stringBuilder.AppendFormat("Network: {0}<br />", (object) network.Name);
      }
      Account account1 = cust.Account ?? Account.Load(this.AccountID);
      stringBuilder.AppendFormat("Date: {0} {1}<br />", (object) TimeZone.GetLocalTimeById(readingDate, account1.TimeZoneID).ToShortDateString(), (object) TimeZone.GetLocalTimeById(readingDate, account1.TimeZoneID).ToShortTimeString());
      stringBuilder.AppendFormat("Reading: {0}", (object) reading);
      stringBuilder.Append("<br />");
    }
    return stringBuilder.ToString();
  }

  private string BuildSMSMessage(
    Customer cust,
    string reading,
    DateTime readingDate,
    Sensor sensor,
    Gateway gateway,
    CSNet network,
    Account account,
    long notificationRecordedID,
    string originalReading,
    DateTime originalReadingDate,
    Guid notificationRecordedGuid)
  {
    try
    {
      int startIndex = 160 /*0xA0*/;
      string baseText = string.IsNullOrWhiteSpace(this.SMSText) ? this.NotificationText : this.SMSText;
      string str1 = this.ReplaceVariables(baseText, reading, sensor, gateway, network, account, notificationRecordedID, readingDate, originalReading, originalReadingDate, notificationRecordedGuid, cust);
      if (str1 == baseText)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(str1);
        if (sensor != null)
        {
          if (stringBuilder.Length < startIndex - (sensor.SensorName.Length + reading.Length + 10))
            stringBuilder.Insert(0, $"Sensor: {sensor.SensorName} {reading} ");
          else if (sensor.SensorName.Length > 15 && stringBuilder.Length < startIndex - (reading.Length + 25))
            stringBuilder.Insert(0, $"Sensor: {sensor.SensorName.Substring(0, 15)} {reading} ");
          else if (stringBuilder.Length < startIndex - (sensor.SensorName.Length + 9))
            stringBuilder.Insert(0, $"Sensor: {sensor.SensorName} ");
          else if (stringBuilder.Length < startIndex - (reading.Length + 10))
            stringBuilder.Insert(0, $"Reading: {reading} ");
          if (stringBuilder.Length < startIndex - 10 && network != null)
            stringBuilder.AppendFormat(" Network: {0}", (object) network.Name);
        }
        else if (gateway != null)
        {
          if (stringBuilder.Length < startIndex - (gateway.Name.Length + reading.Length + 11))
            stringBuilder.Insert(0, $"Gateway: {gateway.Name} {reading} ");
          else if (gateway.Name.Length > 15 && stringBuilder.Length < startIndex - (reading.Length + 26))
            stringBuilder.Insert(0, $"Gateway: {gateway.Name.Substring(0, 15)} {reading} ");
          else if (stringBuilder.Length < startIndex - (gateway.Name.Length + 10))
            stringBuilder.Insert(0, $"Gateway: {gateway.Name} ");
          else if (stringBuilder.Length < startIndex - (reading.Length + 10))
            stringBuilder.Insert(0, $"Reading: {reading} ");
          if (stringBuilder.Length < startIndex - 10 && network != null)
            stringBuilder.AppendFormat(" Network: {0}", (object) network.Name);
        }
        else if (stringBuilder.Length < startIndex - (reading.Length + 10))
          stringBuilder.Insert(0, $"Reading: {reading} ");
        str1 = stringBuilder.ToString();
      }
      string str2 = Notification.ReplaceEncodedTextWithAngleBrackets(Notification.UnFormat(Notification.ReplaceAngleBracketsWithEncodedText(str1.Replace("°", "")), false));
      if (str2.Length > startIndex)
        str2.Substring(startIndex);
      return str2;
    }
    catch (Exception ex)
    {
      string str3 = $"{$"{string.Empty}, sensor=[{(sensor != null ? "id=" + sensor.SensorID.ToStringSafe() : "null")}] "}, gateway=[{(gateway != null ? "id=" + gateway.GatewayID.ToStringSafe() : "null")}]";
      long num;
      string str4;
      if (network == null)
      {
        str4 = "null";
      }
      else
      {
        num = network.CSNetID;
        str4 = "id=" + num.ToString();
      }
      string str5 = $"{str3}, network=[{str4}]";
      string str6;
      if (account == null)
      {
        str6 = "null";
      }
      else
      {
        num = account.AccountID;
        str6 = "id=" + num.ToString();
      }
      string str7 = $"{$"{$"{$"{$"{$"{$"{str5}, network=[{str6}]" + $", ntfcRcrdID=[{notificationRecordedID}]"}, network=[id={notificationRecordedGuid.ToString()}]"}, customer=[{(cust != null ? "id=" + cust.CustomerID.ToStringSafe() : "null")}]"}, reading=[{(reading != null ? reading.ToStringSafe() : "null")}]"}, readingDate=[{readingDate.ToStringSafe()}]"}, origReading=[{(originalReading != null ? originalReading.ToStringSafe() : "null")}]"}, origReadingDate=[{originalReadingDate.ToStringSafe()}]";
      ex.Log("BuildSMSMessage(): " + str7);
    }
    return "Error";
  }

  private string BuildPOTSMessage(
    Customer cust,
    string reading,
    Sensor sensor,
    Gateway gateway,
    CSNet network,
    Account account,
    long notificationRecordedID,
    DateTime readingDate,
    string originalReading,
    DateTime originalReadingDate,
    Guid notificationRecordedGuid)
  {
    int startIndex = 160 /*0xA0*/;
    string str1 = this.ReplaceVariables(string.IsNullOrWhiteSpace(this.VoiceText) ? this.NotificationText : this.VoiceText, reading, sensor, gateway, network, account, notificationRecordedID, readingDate, originalReading, originalReadingDate, notificationRecordedGuid, cust);
    if (str1 == this.NotificationText || !string.IsNullOrWhiteSpace(this.SMSText) && str1 == this.SMSText)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(str1);
      if (sensor != null)
      {
        if (stringBuilder.Length < startIndex - (sensor.SensorName.Length + reading.Length + 10))
          stringBuilder.Insert(0, $"Sensor: {sensor.SensorName} {reading} ");
        else if (sensor.SensorName.Length > 15 && stringBuilder.Length < startIndex - (reading.Length + 25))
          stringBuilder.Insert(0, $"Sensor: {sensor.SensorName.Substring(0, 15)} {reading} ");
        else if (stringBuilder.Length < startIndex - (sensor.SensorName.Length + 9))
          stringBuilder.Insert(0, $"Sensor: {sensor.SensorName} ");
        else if (stringBuilder.Length < startIndex - (reading.Length + 10))
          stringBuilder.Insert(0, $"Reading: {reading} ");
        if (stringBuilder.Length < startIndex - 10 && network != null)
          stringBuilder.AppendFormat(" Network: {0}", (object) network.Name);
      }
      else if (gateway != null)
      {
        if (stringBuilder.Length < startIndex - (gateway.Name.Length + reading.Length + 11))
          stringBuilder.Insert(0, $"Gateway: {gateway.Name} {reading} ");
        else if (gateway.Name.Length > 15 && stringBuilder.Length < startIndex - (reading.Length + 26))
          stringBuilder.Insert(0, $"Gateway: {gateway.Name.Substring(0, 15)} {reading} ");
        else if (stringBuilder.Length < startIndex - (gateway.Name.Length + 10))
          stringBuilder.Insert(0, $"Gateway: {gateway.Name} ");
        else if (stringBuilder.Length < startIndex - (reading.Length + 10))
          stringBuilder.Insert(0, $"Reading: {reading} ");
        if (stringBuilder.Length < startIndex - 10 && network != null)
          stringBuilder.AppendFormat(" Network: {0}", (object) network.Name);
      }
      else if (stringBuilder.Length < startIndex - (reading.Length + 10))
        stringBuilder.Insert(0, $"Reading: {reading} ");
      str1 = stringBuilder.ToString();
    }
    string str2 = Notification.UnFormat(Regex.Replace(Regex.Replace(str1.Replace("°", ""), "(?<=\\s)<(?=\\s)", "less than"), "(?<=\\s)>(?=\\s)", " greater than "));
    if (str2.Length > startIndex)
      str2.Substring(startIndex);
    return str2;
  }

  private string BuildAVMessage(
    Sensor device,
    string reading,
    Sensor sensor,
    Gateway gateway,
    CSNet network,
    Account account,
    long notificationRecordedID,
    DateTime readingDate,
    string originalReading,
    DateTime originalReadingDate,
    Guid notificationRecordedGuid)
  {
    StringBuilder stringBuilder = new StringBuilder();
    int startIndex = 62;
    string str = Notification.UnFormat(this.ReplaceVariables(string.IsNullOrWhiteSpace(this.LocalAlertText) ? (string.IsNullOrWhiteSpace(this.SMSText) ? this.NotificationText : this.SMSText) : this.LocalAlertText, reading, sensor, gateway, network, account, notificationRecordedID, readingDate, originalReading, originalReadingDate, notificationRecordedGuid));
    if (str.Length > startIndex)
      str.Substring(startIndex);
    return str;
  }

  private string BuildPushMessage(
    Customer cust,
    string reading,
    DateTime readingDate,
    Sensor sensor,
    Gateway gateway,
    CSNet network,
    Account account,
    long notificationRecordedID,
    string originalReading,
    DateTime originalReadingDate,
    Guid notificationRecordedGuid)
  {
    int startIndex = 160 /*0xA0*/;
    string baseText = string.IsNullOrWhiteSpace(this.PushMsgText) ? this.NotificationText : this.PushMsgText;
    string str1 = this.ReplaceVariables(baseText, reading, sensor, gateway, network, account, notificationRecordedID, readingDate, originalReading, originalReadingDate, notificationRecordedGuid, cust);
    if (str1 == baseText)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(str1);
      if (sensor != null)
      {
        if (stringBuilder.Length < startIndex - (sensor.SensorName.Length + reading.Length + 10))
          stringBuilder.Insert(0, $"Sensor: {sensor.SensorName} {reading} ");
        else if (sensor.SensorName.Length > 15 && stringBuilder.Length < startIndex - (reading.Length + 25))
          stringBuilder.Insert(0, $"Sensor: {sensor.SensorName.Substring(0, 15)} {reading} ");
        else if (stringBuilder.Length < startIndex - (sensor.SensorName.Length + 9))
          stringBuilder.Insert(0, $"Sensor: {sensor.SensorName} ");
        else if (stringBuilder.Length < startIndex - (reading.Length + 10))
          stringBuilder.Insert(0, $"Reading: {reading} ");
        if (stringBuilder.Length < startIndex - 10 && network != null)
          stringBuilder.AppendFormat(" Network: {0}", (object) network.Name);
      }
      else if (gateway != null)
      {
        if (stringBuilder.Length < startIndex - (gateway.Name.Length + reading.Length + 11))
          stringBuilder.Insert(0, $"Gateway: {gateway.Name} {reading} ");
        else if (gateway.Name.Length > 15 && stringBuilder.Length < startIndex - (reading.Length + 26))
          stringBuilder.Insert(0, $"Gateway: {gateway.Name.Substring(0, 15)} {reading} ");
        else if (stringBuilder.Length < startIndex - (gateway.Name.Length + 10))
          stringBuilder.Insert(0, $"Gateway: {gateway.Name} ");
        else if (stringBuilder.Length < startIndex - (reading.Length + 10))
          stringBuilder.Insert(0, $"Reading: {reading} ");
        if (stringBuilder.Length < startIndex - 10 && network != null)
          stringBuilder.AppendFormat(" Network: {0}", (object) network.Name);
      }
      else if (stringBuilder.Length < startIndex - (reading.Length + 10))
        stringBuilder.Insert(0, $"Reading: {reading} ");
      str1 = stringBuilder.ToString();
    }
    string str2 = Notification.UnFormat(str1.Replace("°", ""), false);
    if (str2.Length > startIndex)
      str2.Substring(startIndex);
    return str2;
  }

  private string BuildThermostatMessage(string serializedRecipientProperties)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (this._MonnitApplicationID == 97L)
    {
      int Occupancy;
      ushort Duration;
      Thermostat.ParseSerializedRecipientProperties(serializedRecipientProperties, out Occupancy, out Duration);
      if (Occupancy > 0)
        stringBuilder.Append("Occupancy");
      switch (Occupancy)
      {
        case 1:
          stringBuilder.Append(" Off");
          break;
        case 2:
          stringBuilder.Append(" On");
          break;
      }
      if (Duration > (ushort) 0)
        stringBuilder.AppendFormat(" {0} minutes", (object) Duration);
    }
    return stringBuilder.ToString();
  }

  private string BuildControlMessage(string serializedRecipientProperties)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (this._MonnitApplicationID == 76L)
    {
      int state1;
      ushort time1;
      BasicControl.ParseSerializedRecipientProperties(serializedRecipientProperties, out state1, out time1);
      if (state1 > 0)
        stringBuilder.Append("Relay 1");
      switch (state1)
      {
        case 1:
          stringBuilder.Append(" Off");
          break;
        case 2:
          stringBuilder.Append(" On");
          break;
        case 3:
          stringBuilder.Append(" Toggle");
          break;
      }
      if (time1 > (ushort) 0)
        stringBuilder.AppendFormat(" {0} seconds", (object) time1);
    }
    else
    {
      int state1;
      int state2;
      ushort time1;
      ushort time2;
      Control_1.ParseSerializedRecipientProperties(serializedRecipientProperties, out state1, out state2, out time1, out time2);
      if (state1 > 0)
      {
        stringBuilder.Append("Relay 1");
        switch (state1)
        {
          case 1:
            stringBuilder.Append(" Off");
            break;
          case 2:
            stringBuilder.Append(" On");
            break;
          case 3:
            stringBuilder.Append(" Toggle");
            break;
        }
        if (time1 > (ushort) 0)
          stringBuilder.AppendFormat(" {0} seconds", (object) time1);
      }
      else
      {
        stringBuilder.Append("Relay 2");
        switch (state2)
        {
          case 1:
            stringBuilder.Append(" Off");
            break;
          case 2:
            stringBuilder.Append(" On");
            break;
          case 3:
            stringBuilder.Append(" Toggle");
            break;
        }
        if (time2 > (ushort) 0)
          stringBuilder.AppendFormat(" {0} seconds", (object) time2);
      }
    }
    return stringBuilder.ToString();
  }

  private string BuildHttpMessage(
    string reading,
    DateTime readingDate,
    Sensor sensor,
    Gateway gateway,
    CSNet network,
    Account account,
    long notificationRecordedID,
    string originalReading,
    DateTime originalReadingDate,
    long sensorNotificationID,
    long gatewayNotificationID,
    long notificationTriggeredID,
    string serializedRecipientProperties,
    Guid notificationRecordedGuid)
  {
    string baseText = "{\r\n\"subject\": \"{Subject}\",\r\n\"reading\": \"{Reading}\",\r\n\"rule\": \"{Notification}\",\r\n\"date\": \"{Date}\",\r\n\"time\": \"{Time}\",\r\n\"readingDate\": \"{ReadingDate}\",\r\n\"readingTime\": \"{ReadingTime}\",\r\n\"originalReadingDate\": \"{OriginalReadingDate}\",\r\n\"originalReadingTime\": \"{OriginalReadingTime}\",\r\n\"originalReading\": \"{OriginalReading}\",\r\n\"acknowledgeURL\": \"{AcknowledgeURL}\",\r\n\"parentAccount\": \"{ParentAccount}\",\r\n\"deviceID\": \"{ID}\",\r\n\"name\": \"{Name}\",\r\n\"networkID\": \"{NetworkID}\",\r\n\"network\": \"{Network}\",\r\n\"accountID\": \"{AccountID}\",\r\n\"accountNumber\": \"{AccountNumber}\",\r\n\"companyName\": \"{CompanyName}\"\r\n}";
    StringBuilder stringBuilder = new StringBuilder();
    string str = this.ReplaceVariables(baseText, reading, sensor, gateway, network, account, notificationRecordedID, readingDate, originalReading, originalReadingDate, notificationRecordedGuid);
    stringBuilder.Append(str);
    return stringBuilder.ToString().Replace("°", "").Replace("µ", "").Replace("μ", "");
  }

  public static List<Notification> LoadBySensorID(long sensorID)
  {
    return new Monnit.Data.Notification.LoadBySensorID(sensorID).Result;
  }

  public static List<Notification> LoadByGatewayID(long gatewayID)
  {
    return new Monnit.Data.Notification.LoadByGatewayID(gatewayID).Result;
  }

  public static List<Notification> LoadByAccountID(long accountID)
  {
    return new Monnit.Data.Notification.LoadByAccountID(accountID).Result;
  }

  public static List<Notification> LoadByCustomerID(long customerID)
  {
    return new Monnit.Data.Notification.LoadByCustomerID(customerID).Result;
  }

  public static Notification.RuleFilterResult RuleFilter(
    long accountID,
    bool? isActive,
    int? eNotificationClass,
    string name,
    bool? isAlertingOnly)
  {
    return new Monnit.Data.Notification.RuleFilter(accountID, isActive, eNotificationClass, string.IsNullOrWhiteSpace(name) ? (string) null : name, isAlertingOnly).Result;
  }

  public static Notification.RuleFilterResult RuleFilterHomePage(long accountID)
  {
    return new Monnit.Data.Notification.RuleFilterHomePage(accountID).Result;
  }

  public static DataTable LoadAutomatedNotification()
  {
    return new Monnit.Data.Notification.LoadAutomatedNotification().Result;
  }

  public static List<Notification> NotificationsForDatum(long sensorID, int datumindex)
  {
    return new Monnit.Data.Notification.NotificationsForDatum(sensorID, datumindex).Result;
  }

  public bool IsInNotificationWindow(DateTime dateTime)
  {
    NotificationSchedule notificationSchedule = this.GetNotificationSchedule(dateTime.DayOfWeek);
    TimeSpan timeSpan = dateTime.Subtract(dateTime.Date);
    try
    {
      bool flag = true;
      List<NotificationScheduleDisabled> scheduleDisabledList = NotificationScheduleDisabled.LoadByNotificationID(this.NotificationID);
      if (scheduleDisabledList.Count > 0)
      {
        foreach (NotificationScheduleDisabled scheduleDisabled in scheduleDisabledList)
        {
          if ((scheduleDisabled.StartMonth < dateTime.Month || scheduleDisabled.StartMonth == dateTime.Month && scheduleDisabled.StartDay <= dateTime.Day) && (scheduleDisabled.EndMonth > dateTime.Month || scheduleDisabled.EndMonth == dateTime.Month && scheduleDisabled.EndDay >= dateTime.Day))
          {
            flag = false;
            break;
          }
        }
        if (!flag)
          return false;
        if (notificationSchedule == null)
          return true;
        switch (notificationSchedule.NotificationDaySchedule)
        {
          case eNotificationDaySchedule.All_Day:
            return true;
          case eNotificationDaySchedule.Off:
            return false;
          case eNotificationDaySchedule.Between:
            return timeSpan >= notificationSchedule.FirstTime && timeSpan <= notificationSchedule.SecondTime;
          case eNotificationDaySchedule.Before_and_After:
            return notificationSchedule.FirstTime >= timeSpan || notificationSchedule.SecondTime <= timeSpan;
          case eNotificationDaySchedule.Before:
            return notificationSchedule.FirstTime >= timeSpan;
          case eNotificationDaySchedule.After:
            return notificationSchedule.SecondTime <= timeSpan;
          default:
            return true;
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
    }
    if (notificationSchedule == null)
      return true;
    switch (notificationSchedule.NotificationDaySchedule)
    {
      case eNotificationDaySchedule.All_Day:
        return true;
      case eNotificationDaySchedule.Off:
        return false;
      case eNotificationDaySchedule.Between:
        return timeSpan >= notificationSchedule.FirstTime && timeSpan <= notificationSchedule.SecondTime;
      case eNotificationDaySchedule.Before_and_After:
        return notificationSchedule.FirstTime >= timeSpan || notificationSchedule.SecondTime <= timeSpan;
      case eNotificationDaySchedule.Before:
        return notificationSchedule.FirstTime >= timeSpan;
      case eNotificationDaySchedule.After:
        return notificationSchedule.SecondTime <= timeSpan;
      default:
        return true;
    }
  }

  public NotificationSchedule GetNotificationSchedule(DayOfWeek day)
  {
    switch (day)
    {
      case DayOfWeek.Sunday:
        return this.SundaySchedule;
      case DayOfWeek.Monday:
        return this.MondaySchedule;
      case DayOfWeek.Tuesday:
        return this.TuesdaySchedule;
      case DayOfWeek.Wednesday:
        return this.WednesdaySchedule;
      case DayOfWeek.Thursday:
        return this.ThursdaySchedule;
      case DayOfWeek.Friday:
        return this.FridaySchedule;
      case DayOfWeek.Saturday:
        return this.SaturdaySchedule;
      default:
        return (NotificationSchedule) null;
    }
  }

  public override void Delete()
  {
    this.IsDeleted = true;
    this.Save();
  }

  public bool CheckSnoozeSensor(long sensorID, long notificationID)
  {
    if (!this.ApplySnoozeByTriggerDevice)
      return this.LastSent.AddMinutes((double) this.SnoozeDuration) < DateTime.UtcNow;
    NotificationRecorded notificationRecorded = NotificationRecorded.LoadRecentBySensorIDAndNotificationID(sensorID, notificationID, 1).FirstOrDefault<NotificationRecorded>();
    return notificationRecorded == null || notificationRecorded.NotificationDate.AddMinutes((double) this.SnoozeDuration) < DateTime.UtcNow;
  }

  public bool CheckSnoozeGateway(long gatewayID, long notificationID)
  {
    if (!this.ApplySnoozeByTriggerDevice)
      return this.LastSent.AddMinutes((double) this.SnoozeDuration) < DateTime.UtcNow;
    NotificationRecorded notificationRecorded = NotificationRecorded.LoadRecentByGatewayIDAndNotificationID(gatewayID, notificationID, 1).FirstOrDefault<NotificationRecorded>();
    return notificationRecorded == null || notificationRecorded.NotificationDate.AddMinutes((double) this.SnoozeDuration) < DateTime.UtcNow;
  }

  public bool CheckJointSnooze()
  {
    return this.LastSent.AddMinutes((double) this.SnoozeDuration) < DateTime.UtcNow;
  }

  public static string ReplaceAngleBracketsWithEncodedText(string input)
  {
    return Regex.Replace(Regex.Replace(input, "(?<=\\s)<(?=\\s)", "&lt"), "(?<=\\s)>(?=\\s)", "&gt");
  }

  public static string ReplaceEncodedTextWithAngleBrackets(string input)
  {
    return Regex.Replace(Regex.Replace(input, "(?<=\\s)&lt(?=\\s)", "<"), "(?<=\\s)&gt(?=\\s)", ">");
  }

  public class SendPushNotificationObj
  {
    public bool Successful { get; set; }

    public string Response { get; set; }

    public Exception Error { get; set; }
  }

  public struct RuleFilterResult(
    long totalNotifications,
    List<Notification> notifications,
    List<NotificationTriggered> notificationsTriggered)
  {
    public long TotalNotifications = totalNotifications;
    public List<Notification> Notifications = notifications;
    public List<NotificationTriggered> NotificationsTriggered = notificationsTriggered;
  }
}
