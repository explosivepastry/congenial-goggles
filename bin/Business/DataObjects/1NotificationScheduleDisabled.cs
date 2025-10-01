// Decompiled with JetBrains decompiler
// Type: Monnit.Data.NotificationScheduleDisabled
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;

#nullable disable
namespace Monnit.Data;

internal class NotificationScheduleDisabled
{
  [DBMethod("NotificationScheduleDisabled_DeleteByNotificationID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE dbo.[NotificationScheduleDisabled]\r\nWHERE NotificationID = @NotificationID\r\n")]
  [DBMethodBody(DBMS.SQLite, "Delete NotificationScheduleDisabled where NotificationID = @NotificationID")]
  internal class DeleteByNotificationID : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    public DeleteByNotificationID(long notificationID)
    {
      this.NotificationID = notificationID;
      this.Execute();
    }
  }

  [DBMethod("NotificationScheduleDisabled_DeleteByMonthAndNotificationID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE dbo.[NotificationScheduleDisabled]\r\nWHERE NotificationID = @NotificationID \r\n  AND StartMonth     = @Month;\r\n")]
  internal class DeleteByMonthAndNotificationID : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    [DBMethodParam("Month", typeof (int))]
    public int Month { get; private set; }

    public DeleteByMonthAndNotificationID(int month, long notificationID)
    {
      this.NotificationID = notificationID;
      this.Month = month;
      this.Execute();
    }
  }
}
