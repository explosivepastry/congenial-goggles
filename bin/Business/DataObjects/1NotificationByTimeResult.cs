// Decompiled with JetBrains decompiler
// Type: Monnit.Data.NotificationByTimeResult
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

public class NotificationByTimeResult
{
  [DBMethod("Notification_LoadTimedNotification")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @ProcName NVARCHAR(50);\r\n\r\nDECLARE @ErrorNum         INT,          \r\n        @ErrorProcedure   NVARCHAR(50), \r\n        @ErrorSysMsg      NVARCHAR(MAX);\r\n\r\nBEGIN TRY\r\n\r\n    SET @ProcName = OBJECT_NAME(@@PROCID);\r\n\r\n    SELECT\r\n      n.[NotificationID],\r\n      n.[SensorID],\r\n      n.[GatewayID],\r\n      n.[eNotificationType],\r\n      n.[CustomerToNotifyID],\r\n      n.[NotificationText],\r\n      n.[eCompareType],\r\n      n.[Version],\r\n      n.[CompareValue],\r\n      n.[SnoozeDuration],\r\n      n.[ApplySnoozeByTriggerDevice],\r\n      n.[LastSent],\r\n      n.[Name],\r\n      n.[StartTime],\r\n      n.[EndTime],\r\n      n.[IsDeleted],\r\n      n.[IsActive],\r\n      n.[StartTime],\r\n      n.[EndTime],\r\n      n.[AdvancedNotificationID],\r\n      n.[AccountID],\r\n      n.[eNotificationClass],\r\n      n.[ApplicationID],\r\n      n.[Scale],\r\n      n.[AlwaysSend],\r\n      n.[MondayScheduleID],\r\n      n.[TuesdayScheduleID],\r\n      n.[WednesdayScheduleID],\r\n      n.[ThursdayScheduleID],\r\n      n.[FridayScheduleID],\r\n      n.[SaturdayScheduleID],\r\n      n.[SundayScheduleID],\r\n      n.[Subject],\r\n      n.[DatumIndex],\r\n      n.[ScaleID],\r\n      n.[eDatumType],\r\n      n.[SMSText],\r\n      n.[VoiceText],\r\n      [SelectedSensorID]                = sn.[SensorID],\r\n      sn.[SensorNotificationID],\r\n      n.[CanAutoAcknowledge],\r\n      nbt.*,\r\n      a.[TimeZoneID],\r\n      n.[HasUserNotificationAction],\r\n      n.[HasControlAction],\r\n      n.[HasLocalAlertAction],\r\n      n.[HasSystemAction],\r\n      n.[HasResetAccAction],  \r\n      n.[HasThermostatAction],\r\n      n.[LocalAlertText],\r\n\t  n.[PushMsgText],\r\n\t  n.[IgnoreMaintenanceWindow]\r\n    FROM dbo.[Notification] n\t\t\r\n    INNER JOIN dbo.[NotificationByTime] nbt ON nbt.[NotificationByTimeID] = n.[NotificationByTimeID]\r\n    INNER JOIN dbo.[Account] a              ON a.[AccountID] = n.[AccountID]\r\n    LEFT JOIN dbo.[SensorNotification] sn   ON sn.[NotificationID] = n.[NotificationID]\r\n    WHERE ISNULL(nbt.[NextEvaluationDate],'2010-01-01') < @DateNow\r\n      AND (n.[IsActive]  = 1 OR n.[IsActive]  IS NULL) -- Only send notification for 'active' notifications\r\n      AND (n.[IsDeleted] = 0 OR n.[IsDeleted] IS NULL);-- Only send notification for notifications that have not been deleted\r\n\r\nEND TRY\r\nBEGIN CATCH\r\n\r\n\tSET @ErrorNum = ERROR_NUMBER();\r\n\tSET @ErrorProcedure = ERROR_PROCEDURE();\r\n\tSET @ErrorSysMsg = ERROR_MESSAGE();\r\n\r\n\tDECLARE @Recipients varchar(500)\r\n\tDECLARE @Subject varchar(30)\r\n\tDECLARE @Body VARCHAR(2000)\r\n\r\n  DECLARE @Param VARCHAR(1000)\r\n  SET @Param = '@DateNow: ' + CONVERT(VARCHAR(100), @DateNow, 120)\r\n\r\n  INSERT INTO DBErrorLog (ProcName, Date, Urgency, Message, Params)\r\n  VALUES (@ProcName, GETUTCDATE(), 2, @ErrorSysMsg, @Param)\r\n\r\n\tSET @Body = '<p>Team, </p> <p>Critical Procedure Failed: '+@ProcName+'. Please Address. '+CONVERT(VARCHAR(20), GETDATE() )+' </p> \r\n  <p>ErrorMessage: '+CONVERT(VARCHAR(20), @ErrorNum) +' ' + @ErrorSysMsg+'</p>\r\n\t<p>Sincerely,</p><p>-DBA</p>'\r\n\r\n\tSET @Subject = 'MonnitDB ProcFail - Urgency 2'\r\n\tSET @Recipients = (select value from ConfigData where KeyName = 'DB_Procfail_Contacts')\r\n\r\n    EXEC msdb.dbo.sp_send_dbmail \r\n\t  @Profile_name = 'Alerts',\r\n\t  @Recipients = @Recipients , \r\n      @subject = @Subject,  \r\n      @body = @Body,  \r\n      @body_format = 'HTML' ;  \r\n\r\nEND CATCH\r\n")]
  internal class LoadTimedNotification : BaseDBMethod
  {
    [DBMethodParam("DateNow", typeof (DateTime))]
    public DateTime DateNow { get; private set; }

    public List<Monnit.NotificationByTimeResult> Result { get; private set; }

    public LoadTimedNotification(DateTime dateNow)
    {
      this.DateNow = dateNow;
      this.Result = BaseDBObject.Load<Monnit.NotificationByTimeResult>(this.ToDataTable());
    }
  }
}
