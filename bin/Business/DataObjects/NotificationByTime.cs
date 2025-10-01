// Decompiled with JetBrains decompiler
// Type: Monnit.NotificationByTime
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("NotificationByTime")]
public class NotificationByTime : BaseDBObject
{
  private long _NotificationByTimeID = long.MinValue;
  private DateTime _NextEvaluationDate = DateTime.MinValue;
  private int _ScheduledHour = int.MinValue;
  private int _ScheduledMinute = int.MinValue;

  [DBProp("NotificationByTimeID", IsPrimaryKey = true)]
  public long NotificationByTimeID
  {
    get => this._NotificationByTimeID;
    set => this._NotificationByTimeID = value;
  }

  [DBProp("NextEvaluationDate")]
  public DateTime NextEvaluationDate
  {
    get => this._NextEvaluationDate;
    set => this._NextEvaluationDate = value;
  }

  [DBProp("ScheduledHour")]
  public int ScheduledHour
  {
    get => this._ScheduledHour;
    set => this._ScheduledHour = value;
  }

  [DBProp("ScheduledMinute")]
  public int ScheduledMinute
  {
    get => this._ScheduledMinute;
    set => this._ScheduledMinute = value;
  }

  public static NotificationByTime LoadByNotificationID(long NotificationID)
  {
    return BaseDBObject.LoadByForeignKey<NotificationByTime>(nameof (NotificationID), (object) NotificationID).FirstOrDefault<NotificationByTime>();
  }

  public static NotificationByTime Load(long id) => BaseDBObject.Load<NotificationByTime>(id);

  public void UpdateNextEvaluationDate(long NotificationByTimeID, DateTime NextEvaluationDate)
  {
    Monnit.Data.NotificationByTime.UpdateNextEvaluationDate nextEvaluationDate = new Monnit.Data.NotificationByTime.UpdateNextEvaluationDate(NotificationByTimeID, NextEvaluationDate);
  }
}
