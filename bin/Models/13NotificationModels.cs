// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.NotificationAction
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using RedefineImpossible;
using System;

#nullable disable
namespace iMonnit.Models;

public class NotificationAction : BaseDBObject
{
  private long _NotificationTriggeredID = long.MinValue;
  private long _NotificationRecordedID = long.MinValue;
  private DateTime _NotificationDate = DateTime.MinValue;
  private eNotificationType _NotificationType;
  private string _Status = string.Empty;
  private string _SerializedRecipientProperties = string.Empty;
  private string _SentTo = string.Empty;
  private string _RecipientCustomer = string.Empty;
  private long _SentToDeviceID = long.MinValue;
  private string _NotifyingOn = string.Empty;
  private bool _Delivered = false;

  [DBProp("NotificationTriggeredID")]
  public long NotificationTriggeredID
  {
    get => this._NotificationTriggeredID;
    set => this._NotificationTriggeredID = value;
  }

  [DBProp("NotificationRecordedID")]
  public long NotificationRecordedID
  {
    get => this._NotificationRecordedID;
    set => this._NotificationRecordedID = value;
  }

  [DBProp("NotificationDate", AllowNull = false)]
  public DateTime NotificationDate
  {
    get => this._NotificationDate;
    set => this._NotificationDate = value;
  }

  [DBProp("eNotificationType", AllowNull = false)]
  public eNotificationType NotificationType
  {
    get => this._NotificationType;
    set => this._NotificationType = value;
  }

  [DBProp("Status", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [DBProp("SerializedRecipientProperties", MaxLength = 2000, AllowNull = true)]
  public string SerializedRecipientProperties
  {
    get => this._SerializedRecipientProperties;
    set => this._SerializedRecipientProperties = value;
  }

  [DBProp("SentTo", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string SentTo
  {
    get => this._SentTo;
    set => this._SentTo = value;
  }

  [DBProp("RecipientCustomer")]
  public string RecipientCustomer
  {
    get => this._RecipientCustomer;
    set => this._RecipientCustomer = value;
  }

  [DBForeignKey("Sensor", "SensorID")]
  [DBProp("SentToDeviceID", AllowNull = true)]
  public long SentToDeviceID
  {
    get => this._SentToDeviceID;
    set => this._SentToDeviceID = value;
  }

  [DBProp("NotifyingOn", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string NotifyingOn
  {
    get => this._NotifyingOn;
    set => this._NotifyingOn = value;
  }

  [DBProp("Delivered", AllowNull = true)]
  public bool Delivered
  {
    get => this._Delivered;
    set => this._Delivered = value;
  }

  public string ToCSVString(
    NotificationAction action,
    long timeZoneID,
    string dateFormat,
    string timeFormat)
  {
    return $"{$"\"{""}\","}{$"\"{""}\","}{$"\"{""}\","}{$"\"{""}\","}{$"\"{""}\","}{$"\"{""}\","}{$"\"{""}\","}{$"\"{""}\","}{$"\"{""}\","}{$"\"{""}\","}{$"\"{""}\","}{$"\"{""}\","}{$"\"{action.NotificationTriggeredID.ToString()}\","}{$"\"{action.NotificationRecordedID.ToString()}\","}{$"\"{action.NotificationDate.ToLocalDateTimeFormatted(timeZoneID, dateFormat, timeFormat)}\","}{$"\"{action.NotificationType.ToString()}\","}{$"\"{action.Status.ToString()}\","}{$"\"{action.SerializedRecipientProperties.ToString()}\","}{$"\"{action.SentTo.ToString()}\","}{$"\"{action.RecipientCustomer.ToString()}\","}{$"\"{(action.SentToDeviceID < 0L ? (object) "" : (object) action.SentToDeviceID.ToString())}\","}{$"\"{action.NotifyingOn.ToString()}\","}{$"\"{action.Delivered.ToString()}\""}";
  }
}
