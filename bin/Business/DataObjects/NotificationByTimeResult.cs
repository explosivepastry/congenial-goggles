// Decompiled with JetBrains decompiler
// Type: Monnit.NotificationByTimeResult
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace Monnit;

public class NotificationByTimeResult : Notification
{
  private long _SelectedSensorID = long.MinValue;
  private long _SensorNotificationID = long.MinValue;
  private DateTime _NextEvaluationDate = DateTime.MinValue;
  private long _TimeZoneID = long.MinValue;
  private int _ScheduledHour = int.MinValue;
  private int _ScheduledMinute = int.MinValue;

  public NotificationByTimeResult()
  {
  }

  public NotificationByTimeResult(Notification ntfc)
  {
    foreach (PropertyInfo property1 in ntfc.GetType().GetProperties())
    {
      if (!(property1.PropertyType == typeof (Guid)))
      {
        PropertyInfo property2 = this.GetType().GetProperty(property1.Name);
        if (property2 != (PropertyInfo) null && property2.CanWrite)
          property2.SetValue((object) this, property1.GetValue((object) ntfc, (object[]) null), (object[]) null);
      }
    }
  }

  [DBProp("SelectedSensorID", AutoUpdate = false)]
  public long SelectedSensorID
  {
    get => this._SelectedSensorID;
    set => this._SelectedSensorID = value;
  }

  [DBProp("SensorNotificationID", AutoUpdate = false)]
  public long SensorNotificationID
  {
    get => this._SensorNotificationID;
    set => this._SensorNotificationID = value;
  }

  [DBProp("NextEvaluationDate", AutoUpdate = false)]
  public DateTime NextEvaluationDate
  {
    get => this._NextEvaluationDate;
    set => this._NextEvaluationDate = value;
  }

  [DBProp("TimeZoneID", AutoUpdate = false)]
  public long TimeZoneID
  {
    get => this._TimeZoneID;
    set => this._TimeZoneID = value;
  }

  [DBProp("ScheduledHour", AutoUpdate = false)]
  public int ScheduledHour
  {
    get => this._ScheduledHour;
    set => this._ScheduledHour = value;
  }

  [DBProp("ScheduledMinute", AutoUpdate = false)]
  public int ScheduledMinute
  {
    get => this._ScheduledMinute;
    set => this._ScheduledMinute = value;
  }

  public static List<NotificationByTimeResult> LoadTimedNotification(DateTime dateNow)
  {
    return new Monnit.Data.NotificationByTimeResult.LoadTimedNotification(dateNow).Result;
  }
}
