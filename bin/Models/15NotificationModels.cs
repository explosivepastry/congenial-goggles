// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.NotificationHistory
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace iMonnit.Models;

public class NotificationHistory : BaseDBObject
{
  private NotificationEvent _Event = new NotificationEvent();
  private List<NotificationAction> _NotificationActionList = new List<NotificationAction>();
  private bool _HasNote = false;

  [DBProp("Event")]
  public NotificationEvent Event
  {
    get => this._Event;
    set => this._Event = value;
  }

  [DBProp("NotificationActionList")]
  public List<NotificationAction> NotificationActionList
  {
    get => this._NotificationActionList;
    set => this._NotificationActionList = value;
  }

  [DBProp("HasNote", AllowNull = false)]
  public bool HasNote
  {
    get => this._HasNote;
    set => this._HasNote = value;
  }

  public static string ToCSVFile(
    long accountID,
    DateTime UTCFrom,
    DateTime UTCTo,
    int limit,
    long notificationID,
    long timeZoneID,
    string dateFormat,
    string timeFormat)
  {
    Dictionary<long, NotificationHistory> dictionary = NotificationHistory.LoadByNotificationIDandDateRange(notificationID, UTCFrom, UTCTo, limit);
    StringBuilder stringBuilder = new StringBuilder();
    string str = "\"NotificationTriggeredID\",\"NotificationID\",\"StartTime\",\"SensorNotificationID\",\"GatewayNotificationID\",\"AcknowledgedTime\",\"AcknowledgedBy\",\"Reading\",\"ReadingDate\",\"AcknowledgeMethod\",\"resetTime\",\"HasNote\",\"NotificationTriggeredID\",\"NotificationRecordedID\",\"NotificationDate\",\"eNotificationType\",\"Status\",\"SerializedRecipientProperties\",\"SentTo\",\"RecipientCustomer\",\"SentToDeviceID\",\"NotifyingOn\",\"Delivered\"";
    stringBuilder.AppendLine(str);
    foreach (KeyValuePair<long, NotificationHistory> keyValuePair in dictionary)
    {
      stringBuilder.AppendFormat("{0}\r\n", (object) keyValuePair.Value.Event.ToCSVString(keyValuePair.Value.Event, timeZoneID, dateFormat, timeFormat));
      foreach (NotificationAction notificationAction in keyValuePair.Value.NotificationActionList)
        stringBuilder.AppendFormat("{0}\r\n", (object) notificationAction.ToCSVString(notificationAction, timeZoneID, dateFormat, timeFormat));
    }
    return stringBuilder.ToString();
  }

  public static Dictionary<long, NotificationHistory> LoadByNotificationIDandDateRange(
    long notificationID,
    DateTime startDate,
    DateTime endDate,
    int count)
  {
    return new Data.NotificationHistory.LoadByNotificationIDandDateRange(notificationID, startDate, endDate, count).Result;
  }

  public static Dictionary<long, NotificationHistory> LoadOngoingByNotificationID(
    long notificationID,
    DateTime startDate,
    DateTime endDate,
    int count)
  {
    return new Data.NotificationHistory.LoadOngoingByNotificationID(notificationID, startDate, endDate, count).Result;
  }
}
