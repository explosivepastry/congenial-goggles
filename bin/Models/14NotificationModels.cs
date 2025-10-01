// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.NotificationEvent
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using RedefineImpossible;
using System;

#nullable disable
namespace iMonnit.Models;

public class NotificationEvent : BaseDBObject
{
  private long _NotificationTriggeredID = long.MinValue;
  private long _NotificationID = long.MinValue;
  private DateTime _StartTime = DateTime.MinValue;
  private long _SensorNotificationID = long.MinValue;
  private long _GatewayNotificationID = long.MinValue;
  private DateTime _AcknowledgedTime = DateTime.MinValue;
  private string _AcknowledgedBy = string.Empty;
  private string _Reading = string.Empty;
  private DateTime _ReadingDate = DateTime.MinValue;
  private string _AcknowledgeMethod = string.Empty;
  private DateTime _resetTime = DateTime.MinValue;
  private bool _HasNote = false;

  [DBProp("NotificationTriggeredID")]
  public long NotificationTriggeredID
  {
    get => this._NotificationTriggeredID;
    set => this._NotificationTriggeredID = value;
  }

  [DBProp("NotificationID", AllowNull = false)]
  public long NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [DBProp("StartTime", AllowNull = false)]
  public DateTime StartTime
  {
    get => this._StartTime;
    set => this._StartTime = value;
  }

  [DBProp("SensorNotificationID")]
  public long SensorNotificationID
  {
    get => this._SensorNotificationID;
    set => this._SensorNotificationID = value;
  }

  [DBProp("GatewayNotificationID")]
  public long GatewayNotificationID
  {
    get => this._GatewayNotificationID;
    set => this._GatewayNotificationID = value;
  }

  [DBProp("AcknowledgedTime", AllowNull = true)]
  public DateTime AcknowledgedTime
  {
    get => this._AcknowledgedTime;
    set => this._AcknowledgedTime = value;
  }

  [DBProp("AcknowledgedBy", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string AcknowledgedBy
  {
    get => this._AcknowledgedBy;
    set => this._AcknowledgedBy = value;
  }

  [DBProp("Reading", MaxLength = 200, AllowNull = true)]
  public string Reading
  {
    get => this._Reading;
    set => this._Reading = value;
  }

  [DBProp("ReadingDate")]
  public DateTime ReadingDate
  {
    get => this._ReadingDate;
    set => this._ReadingDate = value;
  }

  [DBProp("AcknowledgeMethod", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string AcknowledgeMethod
  {
    get => this._AcknowledgeMethod;
    set => this._AcknowledgeMethod = value;
  }

  [DBProp("resetTime", AllowNull = true)]
  public DateTime resetTime
  {
    get => this._resetTime;
    set => this._resetTime = value;
  }

  [DBProp("HasNote", AllowNull = false)]
  public bool HasNote
  {
    get => this._HasNote;
    set => this._HasNote = value;
  }

  public string ToCSVString(
    NotificationEvent notificationEvent,
    long timeZoneID,
    string dateFormat,
    string timeFormat)
  {
    object[] objArray = new object[12];
    objArray[0] = (object) $"\"{notificationEvent.NotificationTriggeredID.ToString()}\",";
    objArray[1] = (object) $"\"{notificationEvent.NotificationID.ToString()}\",";
    objArray[2] = (object) $"\"{notificationEvent.StartTime.ToLocalDateTimeFormatted(timeZoneID, dateFormat, timeFormat)}\",";
    long num;
    string str1;
    if (notificationEvent.SensorNotificationID >= 0L)
    {
      num = notificationEvent.SensorNotificationID;
      str1 = num.ToString();
    }
    else
      str1 = "";
    objArray[3] = (object) $"\"{str1}\",";
    string str2;
    if (notificationEvent.GatewayNotificationID >= 0L)
    {
      num = notificationEvent.GatewayNotificationID;
      str2 = num.ToString();
    }
    else
      str2 = "";
    objArray[4] = (object) $"\"{str2}\",";
    objArray[5] = (object) $"\"{notificationEvent.AcknowledgedTime.ToLocalDateTimeFormatted(timeZoneID, dateFormat, timeFormat)}\",";
    objArray[6] = (object) $"\"{notificationEvent.AcknowledgedBy}\",";
    objArray[7] = (object) $"\"{notificationEvent.Reading.Replace("\"", "\"\"")}\",";
    objArray[8] = (object) $"\"{notificationEvent.ReadingDate.ToLocalDateTimeFormatted(timeZoneID, dateFormat, timeFormat)}\",";
    objArray[9] = (object) $"\"{notificationEvent.AcknowledgeMethod}\",";
    objArray[10] = (object) $"\"{notificationEvent.resetTime.ToLocalDateTimeFormatted(timeZoneID, dateFormat, timeFormat)}\",";
    objArray[11] = (object) $"\"{notificationEvent.HasNote.ToString()}\"";
    return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}", objArray);
  }
}
